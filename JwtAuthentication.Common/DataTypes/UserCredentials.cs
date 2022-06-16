namespace JwtAuthentication.Common
{
    public class UserCredentials
    {
        #region Properties

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Hash { get; set; }

        public int HashSize { get; set; }

        public string Salt { get; set; }

        public int SaltSize { get; set; }
        
        public int Iterations { get; set; }

        public DateTime CreationDateTime { get; set; }

        #endregion

        #region Constructor

        public UserCredentials()
        { 
            UserId = Constants.NONE;
            
            Username = string.Empty;

            Hash = string.Empty;
            Salt = string.Empty;
        }

        #endregion
    }
}
