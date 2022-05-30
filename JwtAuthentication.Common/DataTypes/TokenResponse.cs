namespace JwtAuthentication.Common
{
    public class TokenResponse
    {
        #region Properties

        public string Token { get; set; }

        public int ExpiresInMinutes { get; set; }

        public string Message { get; set; }

        #endregion

        #region Constructors

        public TokenResponse()
        {
            Token = string.Empty;
            ExpiresInMinutes = Constants.NONE;
            Message = string.Empty;
        }

        public TokenResponse(string message)
        {
            Token = string.Empty;
            ExpiresInMinutes = Constants.NONE;
            Message = message;
        }

        public TokenResponse(string token, int expiresInMinutes)
        {
            Token = token;
            ExpiresInMinutes = expiresInMinutes;

            Message = string.Empty;
        }

        #endregion
    }
}
