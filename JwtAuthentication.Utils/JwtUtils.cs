using JwtAuthentication.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthentication.Utils
{
    public static class JwtUtils
    {
        #region Public Methods

        public static bool GenerateToken(string username,
                                         string issuer,
                                         string audience,
                                         string symmetricSecurityKeyString,
                                         string securityAlgorithm,
                                         int expiresInMinutes,
                                         out string token,
                                         out string result)
        {
            token = string.Empty;

            try
            {
                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(symmetricSecurityKeyString));
                SigningCredentials credentials = new(securityKey, securityAlgorithm);

                if (!ToUnixTime(DateTime.UtcNow, out long unixTime, out result))
                {
                    return false;
                }

                JwtSecurityToken secToken = new(
                    signingCredentials: credentials,
                    claims: new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, unixTime.ToString(), ClaimValueTypes.Integer64)
                    },
                    issuer: issuer,
                    audience: audience,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(expiresInMinutes));

                JwtSecurityTokenHandler handler = new();

                token = handler.WriteToken(secToken);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool GenerateToken(string username,
                                         string issuer,
                                         string audience,
                                         string symmetricSecurityKeyString,
                                         string securityAlgorithm,
                                         int expiresInMinutes,
                                         out TokenResponse tokenResponse,
                                         out string result)
        {
            tokenResponse = new();

            try
            {
                if (!JwtUtils.ToUnixTime(DateTime.UtcNow, out long unixTime, out result))
                {
                    return false;
                }

                var claims = new Claim[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, unixTime.ToString(), ClaimValueTypes.Integer64)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(symmetricSecurityKeyString));
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                };

                var jwt = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(expiresInMinutes)),
                    signingCredentials: new SigningCredentials(signingKey, securityAlgorithm)
                );

                string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                tokenResponse = new()
                {
                    Token = encodedJwt,
                    ExpiresInMinutes = (int)TimeSpan.FromMinutes(expiresInMinutes).TotalSeconds,
                    Message = string.Empty
                };

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool IsJwtTokenValid(string symmetricSecurityKeyString, 
                                           string token, 
                                           out bool unauthorized401, 
                                           out string result)
        {
            unauthorized401 = false;

            try
            {
                // position of second period in order to split header+payload and signature
                int index = token.IndexOf('.', token.IndexOf('.') + 1);
                if (index == Constants.NONE)
                {
                    result = $"Can't Parse Token[{token}]";

                    return false;
                }

                // Example: Gtrm2G_35ynyNd1-CjZ1HsvvFFItEsXPvwhaOsN81HQ
                string signature = token[(index + 1)..];
                string headerAndPayload = token[..index];

                // Bytes of header + payload
                // In other words bytes of: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmb29AZ21haWwuY29tIiwiZXhwIjoxNjQ1NzM1MDU2fQ
                byte[] bytesToSign = Encoding.UTF8.GetBytes(headerAndPayload);

                // compute hash
                var hash = new HMACSHA256(Encoding.UTF8.GetBytes(symmetricSecurityKeyString)).ComputeHash(bytesToSign);
                if (!Base64UrlEncode(hash, out string computedSignature, out result))
                {
                    return false;
                }

                bool signaturesMatch = (computedSignature.Length == signature.Length) && computedSignature.SequenceEqual(signature);

                if (!signaturesMatch)
                {
                    unauthorized401 = true;

                    result = "Signatures Don't Match";

                    return false;
                }

                if (!GetJwtTimeFrame(token, out DateTime validFrom, out DateTime validTo, out result))
                {
                    return false;
                }

                //  .................. [valid from] .................. [valit to] ..................
                //  (1) {utc now}                                                                   | (false)
                //  (2)                                 {utc now}                                   | (true)
                //  (3)                                                                {utc now}    | (false)
                if ((validFrom >= DateTime.UtcNow) || (validTo <= DateTime.UtcNow))
                {
                    unauthorized401 = true;

                    result = $"JWT Expired. Time Frame From[{validFrom}] To[{validTo}]";

                    return false;
                }

                // make sure that signatures match
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool GetJwtTimeFrame(string jwtToken, out DateTime validFrom, out DateTime validTo, out string result)
        {
            result = string.Empty;

            validFrom = DateTime.MinValue;
            validTo = DateTime.MinValue;

            try
            {
                JwtSecurityTokenHandler securityTokenHandler = new();
                JwtSecurityToken jwtSecurityToken = securityTokenHandler.ReadJwtToken(jwtToken);

                validFrom = jwtSecurityToken.ValidFrom;
                validTo = jwtSecurityToken.ValidTo;

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Private Methods

        private static bool ToUnixTime(DateTime date, out long unixTime, out string result)
        {
            result = string.Empty;
            unixTime = 0;

            try
            {

                DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                unixTime = Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private static bool Base64UrlEncode(byte[] input, out string output, out string result)
        {
            result = string.Empty;
            output = string.Empty;

            try
            {
                output = Convert.ToBase64String(input);

                output = output.Split('=')[0];      // Remove any trailing '='s
                output = output.Replace('+', '-');  // 62nd char of encoding
                output = output.Replace('/', '_');  // 63rd char of encoding

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }
     
        #endregion
    }
}