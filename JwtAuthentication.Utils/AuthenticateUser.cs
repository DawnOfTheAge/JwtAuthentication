using JwtAuthentication.Common;
using System.Text;

namespace JwtAuthentication.Utils
{
    public class AuthenticateUser
    {
        #region Data Members

        private string username;
        private string password;

        private UserCredentials user;

        #endregion

        #region Constructor

        public AuthenticateUser(string inUsername, string inPassword, UserCredentials inUser)
        {
            username = inUsername;
            password = inPassword;
            user = inUser;
        }

        #endregion

        #region Public Methods

        public bool Verify(out string result)
        {
            result = string.Empty;
            
            try
            {
                if (user == null)
                {
                    result = "User Is Null";

                    return false;
                }

                byte[] bytesPassword = Encoding.ASCII.GetBytes(password);
                byte[] bytesSalt = Convert.FromBase64String(user.Salt);
                byte[] bytesHash = Convert.FromBase64String(user.Hash);

                byte[] createdHash = EncryptionUtils.GenerateHash(bytesPassword, bytesSalt, user.Iterations, user.HashSize);

                return GeneralUtils.ByteArrayCompare(createdHash, bytesHash);
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
