using JwtAuthentication.Common;
using JwtAuthentication.Utils;

namespace JwtAuthentication.Database
{
    public class DatabaseHandler
    {
        #region Properties

        public bool IsConnected { get; set; }

        #endregion

        #region Data members

        private readonly string DatabaseFilePath;

        private DatabaseContence database;

        #endregion

        #region Constructor

        public DatabaseHandler(string inDatabaseFilePath)
        {
            DatabaseFilePath = inDatabaseFilePath;

            database = new DatabaseContence
            {
                Users = new(),
                Mediators = new()
            };

            IsConnected = false;
        }

        #endregion

        #region Startup

        public bool Connect(out string result)
        {
            try
            {
                result = string.Empty;

                if (!File.Exists(DatabaseFilePath))
                {
                    result = $"File[{DatabaseFilePath}] Does Not Exist";

                    return false;
                }

                if (!JsonUtils.Json2Object(DatabaseFilePath, out database, out result))
                {
                    database = new();
                }

                IsConnected = true;

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Public Methods

        #region Create

        public bool Create(UserCredentials newUser, out string result)
        {
            result = string.Empty;

            try
            {
                if (database == null)
                {
                    database = new();
                }

                if (database.Users == null)
                {
                    database.Users = new();
                }

                if (newUser == null)
                {
                    result = "Added User Is Null";

                    return false;
                }

                if (string.IsNullOrEmpty(newUser.Username))
                {
                    result = "New Username Is Null Or Empty";

                    return false;
                }

                int index = database.Users.FindIndex(user => user.Username.Equals(newUser.Username));
                if (index != Constants.NONE)
                {
                    result = $"User[{newUser.Username}] Already Exists";

                    return false;
                }

                newUser.UserId = database.GetNewUserId();

                database.Users.Add(newUser);

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool Create(MediatorProperties newMediator, out string result)
        {
            result = string.Empty;

            try
            {
                if (database == null)
                {
                    database = new();
                }

                if (database.Mediators == null)
                {
                    database.Mediators = new();
                }

                if (newMediator == null)
                {
                    result = "Added Mediator Is Null";

                    return false;
                }

                if (string.IsNullOrEmpty(newMediator.Name))
                {
                    result = "New Mediator Name Is Null Or Empty";

                    return false;
                }

                int index = database.Mediators.FindIndex(mediator => mediator.Name.Equals(newMediator.Name));
                if (index != Constants.NONE)
                {
                    result = $"Mediator[{newMediator.Name}] Already Exists";

                    return false;
                }

                newMediator.MediatorId = database.GetNewMediatorId();

                database.Mediators.Add(newMediator);

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool Create(string username, List<string> mediators, out string result)
        {
            try
            {
                if (database == null)
                {
                    database = new();
                }

                if (database.Authorizations == null)
                {
                    database.Authorizations = new();
                }

                if (string.IsNullOrEmpty(username))
                {
                    result = "Username Is Null Or Empty";

                    return false;
                }

                if (!FindUserIdByUsername(username, out int userId, out result))
                {
                    result = $"User[{username}] Not Found";

                    return false;
                }

                List<int> mediatorsIds = new();
                if ((mediators != null) && (mediators.Count > 0))
                {
                    foreach (string mediatorName in mediators)
                    {
                        if (!FindMediatorIdByMediatorName(mediatorName, out int mediatorId, out result))
                        {
                            continue;
                        }

                        mediatorsIds.Add(mediatorId);
                    }
                }

                UserAuthorization newUserAuthorization = new()
                {
                    UserId = userId,
                    Mediators = mediatorsIds
                };

                database.Authorizations.Add(newUserAuthorization);

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool Create(UserAuthorization newAuthorization, out string result)
        {
            result = string.Empty;

            try
            {
                if (database == null)
                {
                    database = new();
                }

                if (database.Authorizations == null)
                {
                    database.Authorizations = new();
                }

                if (newAuthorization == null)
                {
                    result = "Added Authorization Is Null";

                    return false;
                }

                int index = database.Authorizations.FindIndex(authorization => authorization.UserId == newAuthorization.UserId);
                if (index != Constants.NONE)
                {
                    database.Authorizations[index] = newAuthorization;

                    return true;
                }

                database.Authorizations.Add(newAuthorization);

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool Create(string userName, string mediatorName, string token, out string result)
        {
            result = string.Empty;

            try
            {
                #region Null And Empty Checks

                if (database == null)
                {
                    database = new();
                }

                if (database.Tokens == null)
                {
                    database.Tokens = new();
                }

                if (string.IsNullOrEmpty(mediatorName))
                {
                    result = "Mediator Name Is Null Or Empty";

                    return false;
                }

                if (string.IsNullOrEmpty(userName))
                {
                    result = "Username Is Null Or Empty";

                    return false;
                }

                if (string.IsNullOrEmpty(token))
                {
                    result = "New Token Is Null Or Empty";

                    return false;
                }

                #endregion

                int index = database.Tokens.FindIndex(userMediatorToken => userMediatorToken.Username.Equals(userName));
                if (index == Constants.NONE)
                {
                    //  No Previous Token For This User
                    UserMediatorToken newUserMediatorToken = new()
                    {
                        Username = userName,
                        MediatorToken = new() { [mediatorName] = token }
                    };

                    database.Tokens.Add(newUserMediatorToken);
                }
                else
                {
                    //  Previous Token For Current Mediator Exists
                    if (database.Tokens[index].MediatorToken.ContainsKey(mediatorName))
                    {
                        database.Tokens[index].MediatorToken[mediatorName] = token;
                    }
                    else
                    {
                        database.Tokens[index].MediatorToken.Add(mediatorName, token);
                    }
                }

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Read

        public bool Read(string username, out UserCredentials savedUser, out string result)
        {
            result = string.Empty;

            savedUser = new();

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Users == null) || (database.Users.Count == 0))
                {
                    result = "No Users In Database";

                    return false;
                }

                if (string.IsNullOrEmpty(username))
                {
                    result = "Username Is Null Or Empty";

                    return false;
                }

                foreach (UserCredentials userCredentials in database.Users)
                {
                    if (userCredentials.Username == username)
                    {
                        savedUser = userCredentials;

                        return true;
                    }
                }

                result = $"User[{username}] Not Found";

                return false;
            }
            catch (Exception e)
            {
                result = $"User[{username}]. {e.Message}";

                return false;
            }
        }

        public bool Read(string mediatorName, out MediatorProperties savedMediator, out string result)
        {
            result = string.Empty;

            savedMediator = new();

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Mediators == null) || (database.Mediators.Count == 0))
                {
                    result = "No Mediators In Database";

                    return false;
                }

                if (string.IsNullOrEmpty(mediatorName))
                {
                    result = "Mediator Name Is Null Or Empty";

                    return false;
                }

                foreach (MediatorProperties mediator in database.Mediators)
                {
                    if (mediator.Name == mediatorName)
                    {
                        savedMediator = mediator;

                        return true;
                    }
                }

                result = $"Mediator[{savedMediator}] Not Found";

                return false;
            }
            catch (Exception e)
            {
                result = $"Mediator[{savedMediator}]. {e.Message}";

                return false;
            }
        }

        public bool Read(int userId, out UserAuthorization userAuthorization, out string result)
        {
            result = string.Empty;

            userAuthorization = new();

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Authorizations == null) || (database.Authorizations.Count == 0))
                {
                    result = "No Authorizations In Database";

                    return false;
                }

                foreach (UserAuthorization authorization in database.Authorizations)
                {
                    if (authorization.UserId == userId)
                    {
                        userAuthorization = authorization;

                        return true;
                    }
                }

                result = $"User[{userId}] Not Found";

                return false;
            }
            catch (Exception e)
            {
                result = $"User[{userId}]. {e.Message}";

                return false;
            }
        }

        public bool Read(string username, out UserAuthorization userAuthorization, out string result)
        {
            userAuthorization = new();

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Authorizations == null) || (database.Authorizations.Count == 0))
                {
                    result = "No Authorizations In Database";

                    return false;
                }

                if (!FindUserIdByUsername(username, out int userId, out result))
                {
                    result = $"Can't Find User[{username}]";

                    return false;
                }

                return Read(userId, out userAuthorization, out result);
            }
            catch (Exception e)
            {
                result = $"User[{username}]. {e.Message}";

                return false;
            }
        }

        public bool Read(string username, string mediatorName, out string token, out string result)
        {
            result = string.Empty;

            token = string.Empty;

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Tokens == null) || (database.Tokens.Count == 0))
                {
                    result = "No Tokens In Database";

                    return false;
                }

                if (string.IsNullOrEmpty(username))
                {
                    result = "Username Is Null Or Empty";

                    return false;
                }

                if (string.IsNullOrEmpty(mediatorName))
                {
                    result = "Mediator Name Is Null Or Empty";

                    return false;
                }

                foreach (UserMediatorToken userMediatorToken in database.Tokens)
                {
                    if (userMediatorToken.Username == username)
                    {
                        if (userMediatorToken.MediatorToken.ContainsKey(mediatorName))
                        {
                            token = userMediatorToken.MediatorToken[mediatorName];

                            return true;
                        }
                        else
                        {
                            result = $"User[{username}] Has  No Token For Mediator[{mediatorName}]";

                            return false;
                        }
                    }
                }

                result = $"User[{username}] Has No Tokens";

                return false;
            }
            catch (Exception e)
            {
                result = $"Token For User[{username}] Mediator[{mediatorName}] Not Found. {e.Message}";

                return false;
            }
        }

        public bool ReadAllUsers(out List<string> userNames, out string result)
        {
            result = string.Empty;

            userNames = new();

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Users == null) || (database.Users.Count == 0))
                {
                    result = "No Users In Database";

                    return false;
                }

                foreach (UserCredentials userCredentials in database.Users)
                {
                    userNames.Add(userCredentials.Username);
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool ReadAllMediators(out List<string> mediatorNames, out string result)
        {
            result = string.Empty;

            mediatorNames = new();

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Mediators == null) || (database.Mediators.Count == 0))
                {
                    result = "No Mediators In Database";

                    return false;
                }

                foreach (MediatorProperties mediator in database.Mediators)
                {
                    mediatorNames.Add(mediator.Name);
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Delete

        public bool DeleteUser(string username, out string result)
        {
            result = string.Empty;

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Users == null) || (database.Users.Count == 0))
                {
                    result = "No Users In Database";

                    return false;
                }

                if (string.IsNullOrEmpty(username))
                {
                    result = "User Name Is Null Or Empty";

                    return false;
                }

                int index = database.Users.FindIndex(user => user.Username == username);
                if (index == Constants.NONE)
                {
                    result = $"User[{username}] Not Found";

                    return false;
                }

                database.Users.RemoveAt(index);

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = $"User[{username}]. {e.Message}";

                return false;
            }
        }

        public bool DeleteMediator(string mediatorName, out string result)
        {
            result = string.Empty;

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Mediators == null) || (database.Mediators.Count == 0))
                {
                    result = "No Mediators In Database";

                    return false;
                }

                if (string.IsNullOrEmpty(mediatorName))
                {
                    result = "Mediator Name Is Null Or Empty";

                    return false;
                }

                int index = database.Mediators.FindIndex(mediator => mediator.Name == mediatorName);
                if (index == Constants.NONE)
                {
                    result = $"Mediator[{mediatorName}] Not Found";

                    return false;
                }

                database.Mediators.RemoveAt(index);

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = $"Mediator[{mediatorName}]. {e.Message}";

                return false;
            }
        }

        public bool DeleteAuthorization(int userId, out string result)
        {
            result = string.Empty;

            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Authorizations == null) || (database.Authorizations.Count == 0))
                {
                    result = "No Authorizations In Database";

                    return false;
                }                

                int index = database.Authorizations.FindIndex(authrization => authrization.UserId == userId);
                if (index == Constants.NONE)
                {
                    result = $"User[{userId}] Not Found";

                    return false;
                }

                database.Authorizations.RemoveAt(index);

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = $"User[{userId}]. {e.Message}";

                return false;
            }
        }

        public bool DeleteAllUsers(out string result)
        {
            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if (database.Users == null)
                {
                    database.Users = new();
                }
                else
                {
                    database.Users.Clear();
                }

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool DeleteAllMediators(out string result)
        {
            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if (database.Mediators == null)
                {
                    database.Mediators = new();
                }
                else
                {
                    database.Mediators.Clear();
                }

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool DeleteAllAuthorizations(out string result)
        {
            try
            {
                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if (database.Authorizations == null)
                {
                    database.Authorizations = new();
                }
                else
                {
                    database.Authorizations.Clear();
                }

                if (!Save(out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Check Authorization

        public bool IsMediatorAuthorizedForUser(string username, string mediatorName, out string result)
        {
            try
            {
                #region Null And Empty Checks

                if (database == null)
                {
                    result = "Database Is Null";

                    return false;
                }

                if ((database.Authorizations == null) || (database.Authorizations.Count == 0))
                {
                    result = "No Authorizations In Database";

                    return false;
                }

                if (string.IsNullOrEmpty(username))
                {
                    result = "Username Is Null Or Empty";

                    return false;
                }

                if (!FindUserIdByUsername(username, out int userId, out result))
                {
                    result = $"User[{username}] Not Found";

                    return false;
                }

                if (string.IsNullOrEmpty(mediatorName))
                {
                    result = "Username Is Null Or Empty";

                    return false;
                }

                if (!FindMediatorIdByMediatorName(mediatorName, out int mediatorId, out result))
                {
                    result = $"Mediator[{mediatorName}] Not Found";

                    return false;
                }

                #endregion

                foreach (UserAuthorization authorization in database.Authorizations)
                {
                    if (authorization.UserId == userId)
                    {
                        if (authorization.Mediators.IndexOf(mediatorId) != Constants.NONE)
                        {
                            return true;
                        }
                    }
                }

                result = $"User[{username}] Is Not Authorized For Mediator[{mediatorName}]";

                return false;
            }
            catch (Exception e)
            {
                result = $"User[{username}] Is Not Authorized For Mediator[{mediatorName}]. {e.Message}";

                return false;
            }
        }

        #endregion

        public bool Save(out string result)
        {
            try
            {
                if (!JsonUtils.Object2Json(DatabaseFilePath, database, out result))
                {
                    return false;
                }

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

        private bool FindUserIdByUsername(string username, out int userId, out string result)
        {
            result = string.Empty;

            userId = Constants.NONE;

            try
            {
                if (database == null)
                {
                    database = new();
                }

                if ((database.Users == null) || (database.Users.Count == 0))
                {
                    result = "Users Is Null Or Empty";

                    return false;
                }

                if (string.IsNullOrEmpty(username))
                {
                    result = "Username Is Null Or Empty";

                    return false;
                }

                UserCredentials ?userFound = database.Users.Find(user => user.Username == username);
                if (userFound != null)
                {
                    userId = userFound.UserId;

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private bool FindMediatorIdByMediatorName(string mediatorName, out int mediatorId, out string result)
        {
            result = string.Empty;

            mediatorId = Constants.NONE;

            try
            {
                if (database == null)
                {
                    database = new();
                }

                if ((database.Mediators == null) || (database.Mediators.Count == 0))
                {
                    result = "Mediators Is Null Or Empty";

                    return false;
                }

                if (string.IsNullOrEmpty(mediatorName))
                {
                    result = "Mediator Name Is Null Or Empty";

                    return false;
                }

                MediatorProperties? mediatorFound = database.Mediators.Find(mediator => mediator.Name == mediatorName);
                if (mediatorFound != null)
                {
                    mediatorId = mediatorFound.MediatorId;

                    return true;
                }

                return false;
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
