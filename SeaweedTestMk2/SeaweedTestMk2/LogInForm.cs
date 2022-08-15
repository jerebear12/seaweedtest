using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;
using static SeaweedTestMk2.PyAPIResp;

namespace SeaweedTestMk2
{
    public partial class formLogin : Form
    {
        // Bools for text controls
        bool firstEntry1 = true;
        bool firstEntry2 = true;
        bool firstEntry3 = true;
        bool firstEntry4 = true;
        bool firstEntry5 = true;
        // File paths for ip information
        string api_file_path = "apiipaddress.txt";
        string filer_file_path = "fileripaddress.txt";
        string folder_file_path = "folder.txt";
        // Ips from file storage
        string? api_ip;
        string? filer_ip;
        string? folder_name;

        public formLogin()
        {
            InitializeComponent();
            this.ActiveControl = lblLogin;
            pnlError.SendToBack();
            pnlSuccess.SendToBack();
            btnSettings.BringToFront();
        }
        // Form events
        private void formLogin_Load(object sender, EventArgs e)
        {
            RefreshIps();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (firstEntry1 == true)
            {
                txtUsername.Text = "";
                firstEntry1 = false;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (firstEntry2 == true)
            {
                txtPassword.Text = "";
                firstEntry2 = false;
            }
        }

        private void txtSUUsername_Enter(object sender, EventArgs e)
        {
            if (firstEntry3 == true)
            {
                txtSUUsername.Text = "";
                firstEntry3 = false;
            }
        }

        private void txtSUEmail_Enter(object sender, EventArgs e)
        {
            if (firstEntry4 == true)
            {
                txtSUEmail.Text = "";
                firstEntry4 = false;
            }
        }

        private void txtSUPassword_Enter(object sender, EventArgs e)
        {
            if (firstEntry5 == true)
            {
                txtSUPassword.Text = "";
                firstEntry5 = false;
            }
        }

        private void lnkSignUp_Click(object sender, EventArgs e)
        {
            pnlSignUp.BringToFront();
            btnSettings.BringToFront();
        }

        private void lnkLogin_Click(object sender, EventArgs e)
        {
            pnlLogin.BringToFront();
            btnSettings.BringToFront();
        }

        private void btnSUClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnlSignUp.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnlLogin.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                }
            }
        }

        private void pnlLogin_Click(object sender, EventArgs e)
        {
            pnlError.SendToBack();
            pnlSuccess.SendToBack();
        }

        private void pnlSignUp_Click(object sender, EventArgs e)
        {
            pnlError.SendToBack();
            pnlSuccess.SendToBack();
        }

        // Set text and text field booleans to default values
        private void setText()
        {
            txtUsername.Text = "Username";
            txtPassword.Text = "Password";
            txtSUUsername.Text = "Username";
            txtSUEmail.Text = "Email";
            txtSUPassword.Text = "Password";
            firstEntry1 = true;
            firstEntry2 = true;
            firstEntry3 = true;
            firstEntry4 = true;
            firstEntry5 = true;
        }

        public bool testAuthAPI()
        {
            if (!string.IsNullOrEmpty(api_ip))
            {
                var client = new RestClient(api_ip);  // API IP
                var request = new RestRequest();
                // Get response from server
                RestResponse response = null;
                try
                {
                    response = client.Get(request);
                    if (response.IsSuccessful)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Response failed in api test: ");
                        Debug.WriteLine(response.Content);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Request failed in api test:");
                    Debug.WriteLine(e.Message);
                    // Attempt to find response status code after error
                    if (response != null)
                    {
                        Debug.WriteLine(response.StatusCode);
                    }
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("Cannot find api_ip in api test");
                return false;
            }
        }

        public bool testFilerAPI(string token)
        {
            if (!string.IsNullOrEmpty(filer_ip))
            {
                var client = new RestClient(filer_ip);  // API IP
                var request = new RestRequest();
                // Prep headers for filer
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                // Get response from server
                RestResponse response = null;
                try
                {
                    response = client.Get(request);
                    if (response.IsSuccessful)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Filer Response while testing:");
                        Debug.WriteLine(response.Content);
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Request threw an error in filer test:");
                    Debug.WriteLine(e.Message);
                    if (response != null)
                    {
                        Debug.WriteLine(response.StatusCode);
                        if (response.StatusCode == (System.Net.HttpStatusCode)401)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("Cannot find filer_ip in filer test");
                return false;
            }
        }

        private static void SaveRefreshToken(string token)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("refresh.txt"))
                {
                    sw.WriteLine(token);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to create refresh.txt");
                Debug.WriteLine(ex.Message);
            }
        }


        private void btnLogIn_Click(object sender, EventArgs e)
        {
            // Make sure fields are populated
            // Server does advanced error handling and sends back error field to be displayed on client
            if (txtUsername.Text.Length > 0 && txtPassword.Text.Length > 0)
            {
                if (testAuthAPI())
                { 
                    var client = new RestClient(api_ip + "/login");  // API IP
                    var request = new RestRequest();
                    var json = new
                    {
                        // Set json key value pairs
                        username = txtUsername.Text,
                        password = txtPassword.Text

                    };
                    request.AddJsonBody(json);
                    //request.AddFile("file", path);
                    // Get response from server
                    var response = client.Post(request);
                    // response to string
                    var content = response.Content;
                    if (content != null)
                    {
                        //JObject jResponse = JObject.Parse(content);
                        // Instantiate class to hold json data as properties for easy access
                        PyAPIResp responseToken = JsonConvert.DeserializeObject<PyAPIResp>(content);
                        Debug.WriteLine(content);
                        if (responseToken.token != null && responseToken.refresh_token != null)
                        {
                            // Briefly alert user
                            pnlSuccess.BringToFront();
                            lblSuccess.Text = "";
                            lblSuccess.Refresh();
                            lblSuccess.Text = responseToken.success;
                            lblSuccess.Refresh();
                            // Open new page
                            Thread.Sleep(1000);  // For testing and alerting the user
                            FileManager fileManager = new FileManager(responseToken.token, filer_ip, folder_name);
                            ((Form)this.TopLevelControl).Hide();
                            fileManager.Show();
                            SaveRefreshToken(responseToken.refresh_token);
                            return;
                        }
                        if (responseToken.error != null)
                        {
                            // Display server error response
                            // The server handles duplicate emails, usernames, and invalid emails
                            pnlError.BringToFront();
                            lblError.Text = "";
                            lblError.Text = responseToken.error.ToString();
                        }
                    }
                    else
                    {
                        // Alert user of server error
                        pnlError.BringToFront();
                        lblError.Text = "";
                        lblError.Text = "No Response";
                    }
                }
                else
                {
                    pnlError.BringToFront();
                    lblError.Text = "";
                    lblError.Refresh();
                    lblError.Text = "No Connection to API";
                    lblError.Refresh();
                    Thread.Sleep(500);
                    lblError.Refresh();
                    Thread.Sleep(500);
                    lblError.Text = "Have you set the API IP?";
                    lblError.Refresh();
                    Thread.Sleep(2000);
                    lblError.Text = "Look in settings!";
                }
            }
            else
            {
                Debug.WriteLine("No text in form");
            }
            // Clear form
            btnClear_Click(sender, e);
            this.ActiveControl = lblLogin;
            setText();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtSUUsername.Text.Length > 0 && txtSUEmail.Text.Length > 0 && txtSUPassword.Text.Length > 0)
            {
                if (testAuthAPI())
                {
                    var client = new RestClient(api_ip + "/signup");  // API IP
                    // client.Authenticator = new HttpBasicAuthenticator(username, password);
                    var request = new RestRequest();
                    var json = new
                    {
                        username = txtSUUsername.Text,
                        email = txtSUEmail.Text,
                        password = txtSUPassword.Text

                    };
                    request.AddJsonBody(json);
                    //request.AddFile("file", path);
                    RestResponse response;
                    
                    // Post request data
                    response = client.Post(request);
                    // Get response data
                    var content = response.Content;
                    if (content != null)
                    {
                        //JObject jResponse = JObject.Parse(content);
                        PyAPIResp resonseData = JsonConvert.DeserializeObject<PyAPIResp>(content);
                        Debug.WriteLine(content);
                        if (resonseData.error != null)
                        {
                            pnlError.BringToFront();
                            lblError.Text = "";
                            lblError.Text = resonseData.error.ToString();
                        }
                        if (resonseData.success != null)
                        {
                            pnlSuccess.BringToFront();
                            lblSuccess.Text = "";
                            lblSuccess.Text = resonseData.success.ToString();
                        }
                    }
                    else
                    {
                        pnlError.BringToFront();
                        lblError.Text = "";
                        lblError.Text = "No Response";
                    }
                }
                else
                {
                    pnlError.BringToFront();
                    lblError.Text = "";
                    lblError.Text = "No Connection to API";
                }
            }
            else
            {
                pnlError.BringToFront();
                lblError.Text = "";
                lblError.Text = "Fill all Entry Fields";
            }
            // Clear form
            btnClear_Click(sender, e);
            this.ActiveControl = lblLogin;
            setText();
        }

        private void UserAuthCheck()
        {
            string refreshPath = "refresh.txt";
            string storedToken = null;
            string retrievedToken = null;
            // Check to see if refresh token is stored
            pbLoading.PerformStep();
            if (File.Exists(refreshPath))
            {
                pbLoading.PerformStep();
                try
                {
                    // Retrieve refresh token
                    using (StreamReader sr = new StreamReader(refreshPath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            storedToken = line;
                            Debug.WriteLine("Stored Token:");
                            Debug.WriteLine(storedToken);
                        }
                    }
                    pbLoading.PerformStep();
                    if (storedToken != null)
                    {
                        pbLoading.PerformStep();
                        // Request new JWT
                        var client = new RestClient(api_ip + "/refresh");  // API IP
                        var request = new RestRequest();
                        string content = "";
                        RefreshToken token = new RefreshToken();
                        token.refresh_token = storedToken;
                        request.AddJsonBody<RefreshToken>(token);
                        // Get response from server
                        RestResponse response;
                        pbLoading.PerformStep();
                        try
                        {
                            // Call refresh api endpoint to recieve new JWT
                            response = client.Post(request);
                            pbLoading.PerformStep();
                            if (response.IsSuccessful)
                            {
                                pbLoading.PerformStep();
                                // Response to text
                                content = response.Content;
                                if (content != null)
                                {
                                    PyAPIResp resonseToken = JsonConvert.DeserializeObject<PyAPIResp>(content);
                                    if (resonseToken.token != null)
                                    {
                                        // Get token from response
                                        retrievedToken = resonseToken.token;
                                        pbLoading.PerformStep();
                                        pbLoading.PerformStep();
                                        // Open new page
                                        FileManager fileManager = new FileManager(retrievedToken, filer_ip, folder_name);
                                        this.Hide();
                                        fileManager.Show();
                                    }
                                    else
                                    {
                                        // Error in API
                                        Debug.WriteLine("API did not return a token");
                                    }
                                }
                                else
                                {
                                    // Error in API
                                    Debug.WriteLine("API did not return any content");
                                }
                            }
                            else
                            {
                                // Failed API call
                                Debug.WriteLine("Failed to reach API, status code:");
                                Debug.WriteLine(response.StatusCode.ToString());
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("Exception:");
                            Debug.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        if (File.Exists(refreshPath))
                        {
                            File.Delete(refreshPath);
                            if (!File.Exists(refreshPath))
                            {
                                // Send through func to be sent to log in page
                                UserAuthCheck();
                            }
                            else
                            {
                                // Error deleting file
                                Debug.WriteLine("File was not deleted");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                // File has not been created yet
                Debug.WriteLine("File was not created");
                // Go to log in page
                pnlLogin.BringToFront();
                btnSettings.BringToFront();
            }
            pbLoading.PerformStep();
            pnlLoading.SendToBack();
            btnSettings.BringToFront();
        }

        private void formLogin_Activated(object sender, EventArgs e)
        {
            UserAuthCheck();
        }

        /// <summary>
        /// Handling Ip Addresses begins here
        /// </summary>
        private bool WriteIpFile(string filePath, string ip)
        {
            try
            {
                // Open file writer at path
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // Write single line
                    sw.WriteLine(ip);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to create refresh.txt");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private bool SaveIpFile(string filePath, string ip)
        {
            // If file does not exist
            if (!File.Exists(filePath))
            {
                // If file is successfully written
                if (WriteIpFile(filePath, ip))
                {
                    return true;
                }
                else
                {
                    // Tell user about error
                    return false;
                }
            }
            else
            {
                try
                {
                    // File exists
                    // Delete it to easily replace it
                    File.Delete(filePath);
                    // Call this func to retry
                    SaveIpFile(filePath, ip);
                    if (WriteIpFile(filePath, ip))
                    {
                        return true;
                    }
                    else
                    {
                        // Tell user about error
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        private void RefreshIps()
        {
            // Load ips from file
            string loaded_api_ip = LoadIpFromFile(api_file_path);
            if (!string.IsNullOrEmpty(loaded_api_ip))
            {
                // If return value is not null set global variable
                api_ip = loaded_api_ip;
                txtAPIIP.Text = api_ip;
            }
            else
            {
                //btnSettings.Invoke(btnSettings_Click);
            }
            // Same thing for filer IP
            string loaded_filer_ip = LoadIpFromFile(filer_file_path);
            if (!string.IsNullOrEmpty(loaded_filer_ip))
            {
                filer_ip = loaded_filer_ip;
                txtFilerIP.Text = filer_ip;
            }
            // Same thing for folder name
            string loaded_folder_name = LoadIpFromFile(folder_file_path);
            if (!string.IsNullOrEmpty(loaded_folder_name))
            {
                folder_name = loaded_folder_name;
                txtFilerIP.Text = folder_name;
            }
        }

        public string LoadIpFromFile(string path)
        {
            string ip = null;
            // Open file using path
            if (File.Exists(path))
            {
                try
                {
                    // Read ip from file
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        // Read single line
                        line = sr.ReadLine();
                        ip = line;
                        Debug.WriteLine("Stored ip:");
                        Debug.WriteLine(ip);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Failed to read: " + path);
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                Debug.WriteLine("Failed to find: " + path);
            }
            // return ip
            return ip;
        }

        public bool CheckEndpoints(string token)
        {
            bool errors = false;
            // API
            string api_ip = LoadIpFromFile(api_file_path);
            if (!string.IsNullOrEmpty(api_ip))
            {
                var client = new RestClient(api_ip);  // API IP
                var request = new RestRequest();
                // Get response from server
                RestResponse response;
                try
                {
                    response = client.Get(request);
                    if (!response.IsSuccessful)
                    {
                        errors = true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Exception:");
                    Debug.WriteLine(e.Message);
                    errors = true;
                }
            }
            else
            {
                Debug.WriteLine("LoadIpFromFile failed");
                errors = true;
            }
            // Filer
            // Make sure token is provided
            if (!string.IsNullOrEmpty(token))
            {
                string filer_ip = LoadIpFromFile(filer_file_path);
                if (!string.IsNullOrEmpty(filer_ip))
                {
                    var client = new RestClient(filer_ip);
                    var request = new RestRequest();
                    // Prep headers for filer
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Authorization", "Bearer " + token);
                    // Get response from server
                    RestResponse response;
                    try
                    {
                        response = client.Get(request);
                        if (!response.IsSuccessful)
                        {
                            Debug.WriteLine("Server Response: ");
                            Debug.WriteLine(response.Content);
                            errors = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Exception:");
                        Debug.WriteLine(e.Message);
                        errors = true;
                    }
                }
                else
                {
                    Debug.WriteLine("LoadIpFromFile failed");
                    errors = true;
                }
            }
            return errors;
        }
        // Settings form control buttons
        private void ClearSettings()
        {
            txtAPIIP.Text = "";
            txtFilerIP.Text = "";
            txtFolder.Text = "";
        }

        private void btnCloseSettings_Click(object sender, EventArgs e)
        {
            pnlSettings.SendToBack();
            ClearSettings();
            btnSettings.BringToFront();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Bring up pnl settings
            pnlSettings.BringToFront();
            btnSettings.BringToFront();
            // Refresh Ips
            RefreshIps();
            // Load text from global var
            txtAPIIP.Text = api_ip;
            txtFilerIP.Text = filer_ip;
            txtFolder.Text = folder_name;
        }

        private void btnClearSettings_Click(object sender, EventArgs e)
        {
            ClearSettings();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            string api_ip = txtAPIIP.Text;
            string filer_ip = txtFilerIP.Text;
            string folder = txtFolder.Text;

            SaveIpFile(api_file_path, api_ip);
            SaveIpFile(filer_file_path, filer_ip);
            SaveIpFile(folder_file_path, folder);

            pnlSettings.SendToBack();
            ClearSettings();
            btnSettings.BringToFront();
        }
    }
}