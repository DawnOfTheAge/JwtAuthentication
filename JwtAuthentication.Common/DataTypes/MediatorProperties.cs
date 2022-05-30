namespace JwtAuthentication.Common
{
    public class MediatorProperties
    {
        #region Properties

        public int MediatorId { get; set; }

        public string Name { get; set; }

        public string Secret { get; set; }
        
        public string Algorithm { get; set; }

        public int ExpirationTimeInMinutes { get; set; }

        public DateTime CreationDateTime { get; set; }

        #endregion

        #region Constructor

        public MediatorProperties()
        { 
            MediatorId = Constants.NONE;

            Name = string.Empty;
            Secret = string.Empty;
            Algorithm = string.Empty;

            ExpirationTimeInMinutes = 0;
            
            CreationDateTime = DateTime.MinValue;
        }

        #endregion
    }
}
