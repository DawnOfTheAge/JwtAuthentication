namespace JwtAuthentication.Common
{
    public class DatabaseContence
    {
        #region Properties

        public List<UserCredentials> Users { get; set; }

        public List<MediatorProperties> Mediators { get; set; }

        public List<UserAuthorization> Authorizations { get; set; }
        
        public List<UserMediatorToken> Tokens { get; set; }

        #endregion

        #region Constructor

        public DatabaseContence()
        {
            Users = new List<UserCredentials>();
            Mediators = new List<MediatorProperties>();
            Authorizations = new List<UserAuthorization>();
            Tokens = new List<UserMediatorToken>();
        }

        #endregion

        #region Public Methods

        public int GetNewUserId()
        {
            try
            {
                if (Users == null)
                {
                    Users = new();

                    return 1;
                }

                if (Users.Count == 0)
                {
                    return 1;
                }

                UserCredentials? user = Users.MaxBy(t => t.UserId);
                if (user == null)
                {
                    return 1;
                }

                return (user.UserId + 1);
            }
            catch (Exception)
            {
                Users = new();

                return 1;
            }
        }

        public int GetNewMediatorId()
        {
            try
            {
                if (Mediators == null)
                {
                    Mediators = new();

                    return 1;
                }

                if (Mediators.Count == 0)
                {
                    return 1;
                }

                MediatorProperties? mediator = Mediators.MaxBy(t => t.MediatorId);
                if (mediator == null)
                {
                    return 1;
                }

                return (mediator.MediatorId + 1);
            }
            catch (Exception)
            {
                Mediators = new();

                return 1;
            }
        }

        #endregion
    }
}
