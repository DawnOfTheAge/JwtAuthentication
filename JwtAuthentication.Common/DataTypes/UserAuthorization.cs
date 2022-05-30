namespace JwtAuthentication.Common
{
    public class UserAuthorization
    {
        #region Properties

        public int UserId { get; set; }
        
        public List<int> Mediators { get; set; }

        #endregion

        #region Constructor

        public UserAuthorization()
        {
            UserId = Constants.NONE;
            Mediators = new List<int>();
        }

        #endregion
    }
}
