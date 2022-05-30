using JwtAuthentication.Common;
using JwtAuthentication.Database;
using JwtAuthentication.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace JwtAuthentication.Gui
{
    public partial class FrmMain : Form
    {
        #region Data Members

        private DatabaseHandler ?database;

        #endregion

        #region Constructor

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Startup

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Location = Cursor.Position;

                if (!Initialize(out string result))
                {
                    MessageBox.Show(result, "Initialize Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Initialize(out string result)
        {
            try
            {
                if (!Prologue(out result))
                {
                    return false;   
                }

                #region Open & Connect Database

                string? root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string file = "Database.json";
                string folder = "JsonFiles";

                if (string.IsNullOrEmpty(root))
                {
                    result = "Root Is Null Or Empty";

                    return false;
                }

                string filePath = Path.Combine(root, folder, file);

                database = new DatabaseHandler(filePath);
                if (!database.Connect(out result))
                {
                    return false;
                }

                #endregion

                #region Gui

                tabMain.SelectedIndex = 0;
                tabMain_SelectedIndexChanged(null, null);

                #endregion

                if (!Epilogue(out result))
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

        private bool Prologue(out string result)
        {
            result = string.Empty;

            try
            {
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private bool Epilogue(out string result)
        {
            result = string.Empty;

            try
            {
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (database != null)
                {
                    if (!database.Save(out string result))
                    {
                        MessageBox.Show(result, "Form Closing Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Form Closing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Gui

        #region Registration Gui

        private void BtnRegistrationSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (database == null)
                {
                    MessageBox.Show("Database Is Null",
                                    "User Registration Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                string username = txtRegistrationUsername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Username Not Given", 
                                    "User Registration Error", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Warning);

                    return;
                }

                string password = txtRegistrationPassword.Text;
                byte[] bytesPassword = Encoding.ASCII.GetBytes(password);

                int iterations = RandomUtils.RandomNumber(Constants.MAX_ITERATIONS);

                int saltSize = RandomUtils.RandomNumber(Constants.MAX_SALT_SIZE);
                byte[] bytesSalt = EncryptionUtils.GenerateSalt(saltSize);

                int hashSize = RandomUtils.RandomNumber(Constants.MAX_HASH_SIZE);
                byte[] hash = EncryptionUtils.GenerateHash(bytesPassword, bytesSalt, iterations, hashSize); 

                UserCredentials newUser = new()
                {
                    Username = username,

                    Iterations = iterations,
                    
                    Salt = bytesSalt,
                    SaltSize = saltSize,

                    Hash = hash,
                    HashSize = hashSize,

                    CreationDateTime = DateTime.UtcNow
                };

                if (!database.Create(newUser, out string result))
                {
                    MessageBox.Show(result, "User Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                List<string> authorizedMediators = new();
                for (int i = 0; i < chklRegistrationMediators.CheckedItems.Count; i++)
                {
                    string ?mediatorName = chklRegistrationMediators.CheckedItems[i].ToString();
                    if (!string.IsNullOrEmpty(mediatorName))
                    {
                        authorizedMediators.Add(mediatorName);
                    }
                }

                if (!database.Create(username , authorizedMediators, out result))
                {
                    MessageBox.Show(result, "User Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show($"User[{username}] Created", "User Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                txtRegistrationUsername.Text = string.Empty;
                txtRegistrationPassword.Text = string.Empty;

                for (int i = 0; i < chklRegistrationMediators.Items.Count; i++)
                {
                    CheckState checkState = chklRegistrationMediators.GetItemCheckState(i);
                    if (checkState == CheckState.Checked)
                    {
                        chklRegistrationMediators.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnRegistrationClear_Click(object sender, EventArgs e)
        {
            txtRegistrationUsername.Text = string.Empty;
            txtRegistrationPassword.Text = string.Empty;

            for (int i = 0; i < chklRegistrationMediators.Items.Count; i++)
            {
                CheckState checkState = chklRegistrationMediators.GetItemCheckState(i);
                if (checkState == CheckState.Checked)
                {
                    chklRegistrationMediators.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        #endregion

        #region Authentication Gui

        private void BtnAuthenticationSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (database == null)
                {
                    MessageBox.Show("Database Is Null",
                                    "User Read Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                string username = txtAuthenticationUsername.Text;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Username Not Given",
                                    "User Read Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                if (!database.Read(username, out UserCredentials user, out string result))
                {
                    MessageBox.Show(result, "User Read Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                AuthenticateUser authenticateUser = new(txtAuthenticationUsername.Text, 
                                                        txtAuthenticationPassword.Text, 
                                                        user);

                bool authenticated = authenticateUser.Verify(out result);
                if (!authenticated && !string.IsNullOrEmpty(result))
                {
                    MessageBox.Show(result, "User Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show($"User[{username}] Authentication Is '{authenticated}'", 
                                "User Authentication", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Exclamation);

                if (!authenticated)
                {
                    return;
                }

                if (chklAuthenticationMediators.CheckedItems.Count > 0)
                {
                    List<string> authorizedMediators = new();
                    for (int i = 0; i < chklAuthenticationMediators.CheckedItems.Count; i++)
                    {
                        string? mediatorName = chklAuthenticationMediators.CheckedItems[i].ToString();
                        if (!string.IsNullOrEmpty(mediatorName))
                        {
                            authorizedMediators.Add(mediatorName);
                        }
                    }

                    //  are there authorized mediators?
                    if (authorizedMediators.Count > 0)
                    {
                        foreach (string mediatorName in authorizedMediators)
                        {
                            //  read mediator properties
                            if (!database.Read(mediatorName, out MediatorProperties mediatorProperties, out result))
                            {
                                MessageBox.Show(result,
                                                $"Can't Read Mediator[{mediatorName}] Properties",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                continue;
                            }

                            //  are they null?
                            if (mediatorProperties == null)
                            {
                                MessageBox.Show("Mediator is Null",
                                                $"Can't Read Mediator[{mediatorName}] Properties",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                continue;
                            }

                            //  generate a new token
                            if (!JwtUtils.GenerateToken(username,
                                                        mediatorName,
                                                        mediatorName,
                                                        mediatorProperties.Secret,
                                                        SecurityAlgorithms.HmacSha256,
                                                        mediatorProperties.ExpirationTimeInMinutes,
                                                        out string token,
                                                        out result))
                            {
                                MessageBox.Show(result,
                                                $"Can't Create Token For Mediator[{mediatorName}]",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);

                                continue;
                            }

                            //  is this mediator authorized for this user?
                            if (!database.IsMediatorAuthorizedForUser(username, mediatorName, out result))
                            {
                                MessageBox.Show(result,
                                                $"Can't Create Token For User[{username}] Mediator[{mediatorName}]",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);

                                continue;
                            }
                            
                            if (!database.Create(username, mediatorName, token, out result))
                            {
                                MessageBox.Show(result,
                                                $"Can't Create Token For User[{username}] Mediator[{mediatorName}]",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);

                                continue;
                            }

                            MessageBox.Show("Success!",
                                            $"Token Created For User[{username}] Mediator[{mediatorName}]",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        }
                    }
                }

                BtnAuthenticationClear_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAuthenticationClear_Click(object sender, EventArgs e)
        {
            txtAuthenticationUsername.Text = string.Empty;
            txtAuthenticationPassword.Text = string.Empty;

            for (int i = 0; i < chklAuthenticationMediators.Items.Count; i++)
            {
                CheckState checkState = chklAuthenticationMediators.GetItemCheckState(i);
                if (checkState == CheckState.Checked)
                {
                    chklAuthenticationMediators.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        #endregion

        #region Mediator Gui

        private void btnMediator_Click(object sender, EventArgs e)
        {
            try
            {
                if (database == null)
                {
                    MessageBox.Show("Database Is Null",
                                    "User Registration Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                string mediatorName = txtMediatorName.Text;
                if (string.IsNullOrEmpty(mediatorName))
                {
                    MessageBox.Show("Mediator Name Not Given",
                                    "Mediator Registration Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                string secret = RandomUtils.RandomString(50);

                MediatorProperties newMediator = new()
                {
                    Name = mediatorName,
                    ExpirationTimeInMinutes = (int)nudExpiration.Value,
                    Secret = secret,
                    Algorithm = cboAlgoritm.Text,
                    CreationDateTime = DateTime.UtcNow
                };

                if (!database.Create(newMediator, out string result))
                {
                    MessageBox.Show(result, "Mediator Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show($"Mediator[{mediatorName}] Created", 
                                "Mediator Registration Success", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Exclamation);

                btnMediatorClear_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMediatorClear_Click(object sender, EventArgs e)
        {
            txtMediatorName.Text = string.Empty;
            nudExpiration.Value = 0;
            cboAlgoritm.Text = SecurityAlgorithms.HmacSha256;
        }

        #endregion

        #region Mediator Authentication Gui
        
        private void btnMediatorAuthentication_Click(object sender, EventArgs e)
        {
            try
            {
                if (database == null)
                {
                    MessageBox.Show("Database Is Null",
                                    "Mediator Authentication Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                string mediatorName = cboMediatorAuthenticationMediatorName.Text;
                if (string.IsNullOrEmpty(mediatorName))
                {
                    MessageBox.Show("Mediator Name Not Given",
                                    "Mediator Authentication Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                string username = cboMediatorAuthenticationUsername.Text;
                if (string.IsNullOrEmpty(mediatorName))
                {
                    MessageBox.Show("Username Not Given",
                                    "Mediator Authentication Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                if (!database.Read(username, mediatorName, out string token, out string result))
                {
                    MessageBox.Show(result,
                                    "Mediator Authentication Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                if (!database.Read(mediatorName, out MediatorProperties mediatorProperties, out result))
                {
                    MessageBox.Show(result,
                                    "Mediator Authentication Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                if (!JwtUtils.IsJwtTokenValid(mediatorProperties.Secret, token, out bool unauthorized401, out result))
                {
                    MessageBox.Show(result,
                                    "Mediator Authentication Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show($"Token For User[{username}] For Mediator[{mediatorName}] Is Valid!",
                                    "Mediator Authentication",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mediator Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblMediatorAuthenticationClear_Click(object sender, EventArgs e)
        {
            cboMediatorAuthenticationMediatorName.Text = string.Empty;
            cboMediatorAuthenticationUsername.Text = string.Empty;
        }

        #endregion

        #region Tabs

        private void tabMain_SelectedIndexChanged(object ?sender, EventArgs ?e)
        {
            try
            {
                if (database == null)
                {
                    MessageBox.Show("Database Is Null",
                                    "Tab Changed Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    return;
                }

                string result;

                List<string> mediatorNames;
                List<string> usernames;

                switch (tabMain.SelectedIndex)
                {
                    case 0:
                        #region User Registration

                        if (!database.ReadAllMediators(out mediatorNames, out result))
                        {
                            MessageBox.Show(result, "Tab Changed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }

                        chklRegistrationMediators.Items.Clear();
                        if ((mediatorNames != null) && (mediatorNames.Count > 0))
                        {
                            foreach (string mediatorName in mediatorNames)
                            {
                                chklRegistrationMediators.Items.Add(mediatorName);
                            }
                        }

                        #endregion
                        break;

                    case 1:
                        #region User Authentication

                        if (!database.ReadAllUsers(out usernames, out result))
                        {
                            MessageBox.Show(result, "Tab Changed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                        
                        txtAuthenticationUsername.Items.Clear();
                        if ((usernames != null) && (usernames.Count > 0))
                        {
                            foreach (string username in usernames)
                            {
                                txtAuthenticationUsername.Items.Add(username);
                            }
                        }
                        
                        if (!database.ReadAllMediators(out mediatorNames, out result))
                        {
                            MessageBox.Show(result, "Tab Changed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }

                        chklAuthenticationMediators.Items.Clear();
                        if ((mediatorNames != null) && (mediatorNames.Count > 0))
                        {
                            foreach (string mediatorName in mediatorNames)
                            {
                                chklAuthenticationMediators.Items.Add(mediatorName);
                            }
                        }

                        #endregion
                        break;

                    case 2:
                        #region Mediator Registration

                        cboAlgoritm.Items.Clear();
                        List<string?> algorithms = GeneralUtils.GetAllPublicConstantValues(typeof(SecurityAlgorithms));
                        foreach (string ?algorithm in algorithms)
                        {
                            if (string.IsNullOrEmpty(algorithm) || (algorithm.ToLower().Contains("http")))
                            {
                                continue;
                            }

                            cboAlgoritm.Items.Add(algorithm);
                        }
                        
                        cboAlgoritm.Text = SecurityAlgorithms.HmacSha256;
                        nudExpiration.Value = 60;

                        #endregion
                        break;

                    case 3:
                        #region Mediator Authentication

                        if (!database.ReadAllUsers(out usernames, out result))
                        {
                            MessageBox.Show(result, "Tab Changed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }

                        cboMediatorAuthenticationUsername.Items.Clear();
                        if ((usernames != null) && (usernames.Count > 0))
                        {
                            foreach (string username in usernames)
                            {
                                cboMediatorAuthenticationUsername.Items.Add(username);
                            }
                        }

                        if (!database.ReadAllMediators(out mediatorNames, out result))
                        {
                            MessageBox.Show(result, "Tab Changed Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }

                        cboMediatorAuthenticationMediatorName.Items.Clear();
                        if ((mediatorNames != null) && (mediatorNames.Count > 0))
                        {
                            foreach (string mediatorName in mediatorNames)
                            {
                                cboMediatorAuthenticationMediatorName.Items.Add(mediatorName);
                            }
                        }

                        #endregion
                        break;

                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tab Changed Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion
    }
}