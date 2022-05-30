namespace JwtAuthentication.Common
{
    public class UserMediatorToken
    {
        #region Properties

        public string Username { get; set; }

        public Dictionary<string, string> MediatorToken { get; set; }

        #endregion

        #region Constructor

        public UserMediatorToken()
        {
            Username = string.Empty;
            MediatorToken = new Dictionary<string, string>();
        }

        #endregion
    }
}
