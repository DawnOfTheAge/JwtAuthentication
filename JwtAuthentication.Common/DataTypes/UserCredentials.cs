namespace JwtAuthentication.Common
{
    public class UserCredentials
    {
        #region Properties

        public int UserId { get; set; }

        public string Username { get; set; }

        public byte[] Hash { get; set; }

        public int HashSize { get; set; }

        public byte[] Salt { get; set; }

        public int SaltSize { get; set; }
        
        public int Iterations { get; set; }

        public DateTime CreationDateTime { get; set; }

        #endregion

        #region Constructor

        public UserCredentials()
        { 
            UserId = Constants.NONE;
            
            Username = string.Empty;

            Hash = new byte[0];
            Salt = new byte[0];
        }

        #endregion
    }
}
