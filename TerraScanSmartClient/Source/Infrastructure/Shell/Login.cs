//--------------------------------------------------------------------------------------------
// <copyright file="Login.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Login.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created
// 04 May 06        Jyothi        Form Created for user authentication.
// 26 Dec 26        JAYANTHI      Logout message added on exiting the login form
// 27 Oct 10        Biju I.G.     To implement #8832
// 18 Apr 13        Purushotham   Brand name changed
//*********************************************************************************/
[assembly: System.CLSCompliant(false)]
namespace TerraScan.UI
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.Deployment.Application;
    using System.IO;
    using System.Xml;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;
    using System.Diagnostics;
    using TerraScan.BusinessEntities;


    /// <summary>
    /// Login Form.
    /// </summary>
    public partial class Login : Form
    {
        #region Variable


        private string fieldRequired = string.Empty;
        /// <summary>
        /// Object for dataset created - for getting NetName 
        /// </summary>
        private DataSet netNameDataSet = new DataSet();

        /// <summary>
        /// Object for dataset created
        /// </summary>
        private DataSet loginDataSet = new DataSet();

        /// <summary>
        /// authenticationState
        /// </summary>
        private bool authenticationState;

        /// <summary>
        /// Object for dataset created
        /// </summary>
        private DataSet authenticationStateDataSet = new DataSet();

        /// <summary>
        /// checkDataSet
        /// </summary>
        private DataSet checkDataSet = new DataSet();


        /// <summary>
        /// private variable holds the loginControl
        /// </summary>
        private LoginController myloginControll;

        /// <summary>
        /// Flag to check whether Form closing is called from "ok" or Close
        /// </summary>
        private bool loginSuccess;

        /// <summary>
        /// applicationStatus
        /// </summary>
        private bool applicationFieldStatus;

        /// <summary>
        /// configDataSet
        /// </summary>
        private DataSet configDataSet = new DataSet();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Login"/> class.
        /// </summary>
        public Login()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Set Property for The LoginControl
        /// </summary>
        [CreateNew]
        public LoginController MyLoginControll
        {
            get { return this.myloginControll; }
            set { this.myloginControll = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the LoginButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string domainName = string.Empty;
                this.loginSuccess = true;
                this.Cursor = Cursors.WaitCursor;
                this.DeleteScanFile();
                this.ErrorLabel.Text = string.Empty;

                string machineName = Environment.MachineName;
                TerraScanCommon.FieldServerName = machineName + "\\TERRASCANFIELD";
                TerraScanCommon.FieldDataBaseName = "TerrascanFieldDB";
                WSHelper.IsOnLineMode = true;
                this.GetAuthenticationState();
                if (WSHelper.IsOnLineMode)
                {
                    string domain = string.Empty;
                    //Used for the Login Field User
                    this.netNameDataSet = LoginController.GetUserNetName(this.UserNameTextBox.Text.Trim());
                    if (this.netNameDataSet != null && this.netNameDataSet.Tables.Count > 0 && this.netNameDataSet.Tables[0].Rows.Count > 0)
                    {
                        if (this.netNameDataSet.Tables[0].Rows[0]["IsFieldUser"].Equals(true))
                        {
                            if (!ScriptEngine.IsDatabaseAvailable())
                            {
                                TerraScanCommon.IsDataBaseAvailable = false;  
                                this.OnlineLoginOnly();
                            }
                            else
                            {
                                TerraScanCommon.IsDataBaseAvailable = true;  
                                this.OnLineLogin();
                            }
                        }
                        else
                        {
                            //Online User -> Online DataBase
                            TerraScanCommon.IsFieldUser = false;
                            WSHelper.IsOnLineMode = true;
                            domain = this.netNameDataSet.Tables[0].Rows[0]["Name_Net"].ToString();
                            //                string labelMessage = LoginController.ValidateUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), this.DomainComboBox.Text.Trim(), this.authenticationState);
                            if (!this.authenticationState)
                            {

                                string labelMessage = LoginController.ValidateUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), domain, this.authenticationState);
                                if (labelMessage == "success")
                                {
                                    ////this.DialogResult = DialogResult.OK;
                                    this.FindForm().Visible = false;
                                    ShellForm shellForm = new ShellForm();
                                    this.myloginControll.WorkItem.Items.Add(shellForm, "ShellForm");
                                    shellForm.Show();
                                    CommentsData getPrivacyNotify = new CommentsData();
                                    getPrivacyNotify = LoginController.LoginConfigDetails("TS_PrivacyNotify");

                                    if (getPrivacyNotify.GetCommentsConfigDetails.Rows.Count > 0)
                                    {
                                        if (!string.IsNullOrEmpty(getPrivacyNotify.GetCommentsConfigDetails.Rows[0][getPrivacyNotify.GetCommentsConfigDetails.ConfigurationValueColumn].ToString()))
                                        {
                                            if (getPrivacyNotify.GetCommentsConfigDetails.Rows[0][getPrivacyNotify.GetCommentsConfigDetails.ConfigurationValueColumn].Equals("1"))
                                            {
                                                NotificationAboutBox aboutBox = new NotificationAboutBox();
                                                aboutBox.ShowDialog();
                                                //MessageBox.Show("We’ve updated our Privacy Statement. Before you continue, please read our new Privacy Statement  and familiarize yourself with the terms", ConfigurationWrapper.ApplicationName,
                                                // MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.loginSuccess = false;
                                    this.ErrorLabel.Text = labelMessage;
                                }
                            }
                            else
                            {
                                string labelMessage = string.Empty;
                                if (!string.IsNullOrEmpty(this.PasswordTextBox.Text.Trim()))
                                {
                                    string actualDomain = domain;
                                    domain = domain.Substring(domain.LastIndexOf(@"\")).Replace("\\", "");
                                    labelMessage = DomainAuthorization.CacheUserValidation(domain, this.PasswordTextBox.Text.Trim(), ConfigurationWrapper.FieldDomainName);
                                    MainWorkItem.AuthorizeUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), actualDomain, this.authenticationState);
                                    if (labelMessage == "success")
                                    {
                                        ////this.DialogResult = DialogResult.OK;
                                        this.FindForm().Visible = false;
                                        ShellForm shellForm = new ShellForm();
                                        this.myloginControll.WorkItem.Items.Add(shellForm, "ShellForm");
                                        shellForm.Show();
                                        CommentsData getPrivacyNotify = new CommentsData();
                                        getPrivacyNotify = LoginController.LoginConfigDetails("TS_PrivacyNotify");

                                        if (getPrivacyNotify.GetCommentsConfigDetails.Rows.Count > 0)
                                        {
                                            if (!string.IsNullOrEmpty(getPrivacyNotify.GetCommentsConfigDetails.Rows[0][getPrivacyNotify.GetCommentsConfigDetails.ConfigurationValueColumn].ToString()))
                                            {
                                                if (getPrivacyNotify.GetCommentsConfigDetails.Rows[0][getPrivacyNotify.GetCommentsConfigDetails.ConfigurationValueColumn].Equals("1"))
                                                {
                                                    NotificationAboutBox aboutBox = new NotificationAboutBox();
                                                    aboutBox.ShowDialog();
                                                    //MessageBox.Show("We’ve updated our Privacy Statement. Before you continue, please read our new Privacy Statement  and familiarize yourself with the terms", ConfigurationWrapper.ApplicationName,
                                                    // MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.loginSuccess = false;
                                        this.ErrorLabel.Text = labelMessage;
                                    }
                                }
                                else
                                {
                                    this.loginSuccess = false;
                                    this.ErrorLabel.Text = "Enter password";
                                }
                            }

                        }
                    }
                    else
                    {
                        this.loginSuccess = false;
                        this.ErrorLabel.Text = SharedFunctions.GetResourceString("Invalid FullName");
                    }
                }
                else
                {
                    if (ScriptEngine.IsServerAvailable())
                    {
                        if (ScriptEngine.IsDatabaseAvailable())
                        {
                            TerraScanCommon.IsDataBaseAvailable = true;  
                            this.OnLineLogin();
                        }
                        else
                        {
                            TerraScanCommon.IsDataBaseAvailable = false;
                            MessageBox.Show("No Database exixts.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No sql express instance exists for field manipulation.");
                    }
                }
                //if (!ScriptEngine.IsDatabaseAvailable())
                //{

                //    this.OnlineLoginOnly();
                string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string appDir = Path.GetDirectoryName(appPath);
                FileInfo wsinfo = new FileInfo(appDir + "//" + "TerraScan.UI.exe.config");
                System.Configuration.ExeConfigurationFileMap map = new System.Configuration.ExeConfigurationFileMap();
                map.ExeConfigFilename = wsinfo.FullName;
                System.Configuration.Configuration config =
                System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(map, System.Configuration.ConfigurationUserLevel.None);

                if (config.AppSettings.Settings["LoginName"] != null)
                {
                    config.AppSettings.Settings.Remove("LoginName");
                }

                config.AppSettings.Settings.Add("LoginName", UserNameTextBox.Text.Trim());

                // save the configration Information
                config.Save(System.Configuration.ConfigurationSaveMode.Modified, true);

                //   Refresh the configration section
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                //}
                //else
                //{
                //    WSHelper.IsOnLineMode = false;
                //    this.OnLineLogin();
                //}

                //}

              

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("LoginAuthenticationError"), ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Gets the state of the authentication.
        /// </summary>
        private void GetAuthenticationState()
        {
            this.authenticationStateDataSet = WSHelper.GetAuthenticationState();

            if (this.authenticationStateDataSet != null)
            {
                if (this.authenticationStateDataSet.Tables.Count > 0)
                {
                    if (this.authenticationStateDataSet.Tables[0].Rows.Count > 0)
                    {
                        this.authenticationState = Convert.ToBoolean(this.authenticationStateDataSet.Tables[0].Rows[0]["IsAuthenticationState"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the Login control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                   //MessageBox.Show("First");
                    //try
                    //{
                    //    Process.Start("TerrascanRegistryRightsProvider.exe");
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}
                  //MessageBox.Show("Last"); 
                    if (!EventLog.SourceExists(SharedFunctions.GetResourceString("LogSource")))
                    {
                        EventLog.CreateEventSource(SharedFunctions.GetResourceString("LogSource"), SharedFunctions.GetResourceString("Log"));
                    }
                }
                catch
                {
                    ////using (MailMessage msg = new MailMessage("suganthm@congruentindia.com", "suganthm@congruentindia.com", "ErrorReport", "Error creating log in system"))
                    ////{
                    ////    msg.IsBodyHtml = true;
                    ////    SmtpClient sm = new SmtpClient();
                    ////    sm.Host = "citexchange.cit.congruentindia.com";
                    ////    sm.Send(msg);
                    ////}
                }
             //MessageBox.Show("Second");
                string loginName = "";
                string location = "";
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    try
                    {
                      //MessageBox.Show("Third");
                        location = ApplicationDeployment.CurrentDeployment.DataDirectory;
                        location = location + @"\TerrascanEventLogCreator.exe";
                     // MessageBox.Show(location);
                        //location = @"E:\CO Changes History for TerraScan\TerrascanEventLogCreator\TerrascanEventLogCreator\bin\Debug\TerrascanEventLogCreator.exe";
                        
                        if (!EventLog.Exists("TerraScan"))
                        {
                         // MessageBox.Show("Four");
                            //ProcessStartInfo processinfo = new ProcessStartInfo();
                            //processinfo.Verb = "runas";
                            //processinfo.FileName = location;
                            //processinfo.UseShellExecute = false;
                          Process process = Process.Start(location);
                          process.WaitForExit();
                         
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("TerraScan Event Log Creation operation was cancelled by the user. This would disable the error logging option in the application.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);                            
                        //nothing to display
                    }
                    finally
                    {
                        
                    }
                 //MessageBox.Show("Seven");                        
                    ApplicationDeployment appDeployed = ApplicationDeployment.CurrentDeployment;
                    if (appDeployed != null)
                    {

                        string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        string appDir = Path.GetDirectoryName(appPath);
                        ////bool firstRun = appDeployed.IsFirstRun; 

                        //// if(ConfigurationManager.AppSettings["wsurlupdated"] == "false")
                        //// {
                        ////if (firstRun == true)
                        ////{
                        string webserviceurl = string.Empty;
                        string mswcfserviceurl = string.Empty;
                        //// string activationuri = appDeployed.ActivationUri.AbsoluteUri.ToString();
                        string activationuri = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsoluteUri.ToString();

                        //// Split the Activation URI and get the URL from this string 
                        //// if activationuri = "http://terrascan.click.congruentsoft,net/terrascan.ui.application"
                        //// then URL to be extracted would be "http://terrascan.click.congruentsoft,net"

                        int lastslashindex = activationuri.LastIndexOf("/");
                        string actstr = activationuri.Remove(lastslashindex + 1);
                        string wsstring = actstr + "webservice.xml";

                        // MDF server file path                        
                        string mdfServerFilePath = actstr + "TerraScan.UI_" + ApplicationDeployment.CurrentDeployment.UpdatedVersion.ToString().Replace(".", "_") + @"/TerrascanFieldDB_Data.html";

                        ////string rptstr = activationuri.Remove(lastslashindex);
                        ////int rptlastslashindex = rptstr.LastIndexOf("/");
                        ////string rpturl = rptstr.Remove(rptlastslashindex + 1);

                        FileInfo wsinfo = new FileInfo(appDir + "//" + "TerraScan.UI.exe.config");

                        XmlTextReader wbserviceReader = new XmlTextReader(wsstring);

                        try
                        {

                            while (wbserviceReader.Read())
                            {

                                if (wbserviceReader.NodeType == XmlNodeType.Element)
                                {

                                    if (wbserviceReader.LocalName.Equals("webserviceurl"))
                                    {

                                        webserviceurl = wbserviceReader.ReadString();
                                    }
                                    if (wbserviceReader.LocalName.Equals("mswcfserviceurl"))
                                    {

                                        mswcfserviceurl = wbserviceReader.ReadString();
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }

                        System.Configuration.ExeConfigurationFileMap map = new System.Configuration.ExeConfigurationFileMap();

                        try
                        {
                            map.ExeConfigFilename = wsinfo.FullName;

                            System.Configuration.Configuration config =
                                System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(map, System.Configuration.ConfigurationUserLevel.None);
                            System.Configuration.ConfigurationSection appStrings = config.AppSettings;


                            // If Section information and element info is not protected in App.Config
                            // write into the Appp.Config file.
                            if (!appStrings.SectionInformation.IsProtected)
                            {
                                // check element info. whether locked or not
                                if (!appStrings.ElementInformation.IsLocked)
                                {
                                    // check WebServiceUrl property null or not
                                    if (config.AppSettings.Settings["WebServiceUrl"] != null)
                                    {
                                        // if present remove the WebServiceUrl property
                                        config.AppSettings.Settings.Remove("WebServiceUrl");
                                    }

                                    // Adding the WebServiceUrl property
                                    if (!string.IsNullOrEmpty(webserviceurl))
                                    {
                                        config.AppSettings.Settings.Add("WebServiceUrl", webserviceurl);
                                    }
                                }
                            }

                            if (config.SectionGroups.Count > 0)
                            {
                                if (config.SectionGroups["system.serviceModel"] != null)
                                {
                                    if (config.SectionGroups["system.serviceModel"].Sections.Count > 0)
                                    {
                                        if (config.SectionGroups["system.serviceModel"].Sections["client"] != null && !config.SectionGroups["system.serviceModel"].Sections["client"].IsReadOnly())
                                        {
                                            System.ServiceModel.Configuration.ClientSection clientSection = (System.ServiceModel.Configuration.ClientSection)config.SectionGroups["system.serviceModel"].Sections["client"];

                                            if (!clientSection.IsReadOnly())
                                            {
                                                for (int currentEndPoint = 0; currentEndPoint < clientSection.Endpoints.Count; currentEndPoint++)
                                                {
                                                    if (clientSection.Endpoints[currentEndPoint].Name == "WSHttpBinding_ISmartClientService")
                                                    {
                                                        if (!string.IsNullOrEmpty(webserviceurl))
                                                        {
                                                            clientSection.Endpoints[currentEndPoint].Address = new Uri(webserviceurl);
                                                        }
                                                    }
                                                    if (clientSection.Endpoints[currentEndPoint].Name == "WSHttpBinding_IServiceImplimentation")
                                                    {
                                                        if (!string.IsNullOrEmpty(mswcfserviceurl))
                                                        {
                                                            clientSection.Endpoints[currentEndPoint].Address = new Uri(mswcfserviceurl);
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            try
                            {
                                // save the configration Information
                                config.Save(System.Configuration.ConfigurationSaveMode.Modified, true);

                                // Refresh the configration section
                                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                                // Refresh the configration section
                                System.Configuration.ConfigurationManager.RefreshSection("client");
                            }
                            catch
                            {
                            }
                        }
                        catch (SoapException ex)
                        {
                            ////TODO : Need to find specific exception and handle it.
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        }

                        FileInfo fileInfo = new FileInfo(appDir + "\\" + SharedFunctions.GetResourceString("TerrascanConfig"));

                        try
                        {
                            if (!fileInfo.Exists)
                            {
                                throw new FileNotFoundException(SharedFunctions.GetResourceString("MissingConfig"));
                            }



                            WSHelper.IsOnLineMode = true;

                            try
                            {
                                this.loginDataSet = WSHelper.GetConfigInformation();
                            }
                            catch
                            {
                                //////MessageBox.Show("Server unavilable");
                                //////Application.Exit();
                            }

                            if (this.loginDataSet != null && this.loginDataSet.Tables.Count > 0 && this.loginDataSet.Tables[0].Rows.Count >= 0)
                            {
                                /* XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.Load(fileInfo.FullName);
                                XmlNode node;
                                node = xmlDoc.DocumentElement; */

                                // System.Configuration.ExeConfigurationFileMap map1 = new System.Configuration.ExeConfigurationFileMap();                        
                                map.ExeConfigFilename = wsinfo.FullName;
                                System.Configuration.Configuration config =
                                System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(map, System.Configuration.ConfigurationUserLevel.None);
                                System.Configuration.ConfigurationSection appStrings = config.AppSettings;

                                // If Section information and element info is not protected in App.Config
                                // write into the Appp.Config file.
                                if (!appStrings.SectionInformation.IsProtected)
                                {
                                    // check element info. whether locked or not
                                    if (!appStrings.ElementInformation.IsLocked)
                                    {
                                        string reportURL = this.loginDataSet.Tables[0].Rows[0]["ReportURL"].ToString();
                                        string reportPath = this.loginDataSet.Tables[0].Rows[0]["ReportPath"].ToString();
                                        string reportUserName = this.loginDataSet.Tables[0].Rows[0]["ReportUserName"].ToString();
                                        string reportPassword = this.loginDataSet.Tables[0].Rows[0]["ReportPassword"].ToString();
                                        string domainName = this.loginDataSet.Tables[0].Rows[0]["DomainName"].ToString();
                                        string applicationName = this.loginDataSet.Tables[0].Rows[0]["ApplicationName"].ToString();
                                        string countyName = this.loginDataSet.Tables[0].Rows[0]["CountyName"].ToString();
                                        string smtpAddress = this.loginDataSet.Tables[0].Rows[0]["SMTPAddress"].ToString();
                                        string reportViewerURL = this.loginDataSet.Tables[0].Rows[0]["ReportViewerURL"].ToString();
                                        string exceptionMessage = this.loginDataSet.Tables[0].Rows[0]["ExceptionMessage"].ToString();
                                        string emailId = this.loginDataSet.Tables[0].Rows[0]["ToID"].ToString();
                                        string reportServiceAsmx = this.loginDataSet.Tables[0].Rows[0]["ReportServiceAsmx"].ToString();
                                        string connectionString = "Server = " + Environment.MachineName + "\\TERRASCANFIELD;database=TerrascanFieldDB;uid=sa;pwd=pass@123;Connection Timeout=30;Pooling=false;";
                                        string fieldDomainName = this.loginDataSet.Tables[0].Rows[0]["DomainName"].ToString();
                                        //  string fieldRequired = this.loginDataSet.Tables[0].Rows[0]["IsFieldRequired"].ToString();
                                        /// this variable is declared in main inorder retain the value
                                        fieldRequired = this.loginDataSet.Tables[0].Rows[0]["IsFieldRequired"].ToString();
                                        // ReportURL
                                        // check ReportURL property null or not
                                        if (config.AppSettings.Settings["ReportURL"] != null)
                                        {
                                            // if present remove the ReportURL property
                                            config.AppSettings.Settings.Remove("ReportURL");
                                        }

                                        // Adding the ReportURL property
                                        config.AppSettings.Settings.Add("ReportURL", reportURL);

                                        // ReportPath
                                        // check ReportPath property null or not
                                        if (config.AppSettings.Settings["ReportPath"] != null)
                                        {
                                            // if present remove the ReportPath property
                                            config.AppSettings.Settings.Remove("ReportPath");
                                        }

                                        // Adding the ReportPath property
                                        config.AppSettings.Settings.Add("ReportPath", reportPath);

                                        // ReportUserName
                                        // check ReportUserName property null or not
                                        if (config.AppSettings.Settings["ReportUserName"] != null)
                                        {
                                            // if present remove the ReportUserName property
                                            config.AppSettings.Settings.Remove("ReportUserName");
                                        }

                                        // Adding the ReportUserName property
                                        config.AppSettings.Settings.Add("ReportUserName", reportUserName);

                                        // ReportPassword
                                        // check ReportPassword property null or not
                                        if (config.AppSettings.Settings["ReportPassword"] != null)
                                        {
                                            // if present remove the ReportPassword property
                                            config.AppSettings.Settings.Remove("ReportPassword");
                                        }

                                        // Adding the ReportPassword property
                                        config.AppSettings.Settings.Add("ReportPassword", reportPassword);

                                        // DomainName
                                        // check DomainName property null or not
                                        if (config.AppSettings.Settings["DomainName"] != null)
                                        {
                                            // if present remove the DomainName property
                                            config.AppSettings.Settings.Remove("DomainName");
                                        }

                                        // Adding the DomainName property
                                        config.AppSettings.Settings.Add("DomainName", domainName);

                                        // ApplicationName
                                        // check ApplicationName property null or not
                                        if (config.AppSettings.Settings["ApplicationName"] != null)
                                        {
                                            // if present remove the ApplicationName property
                                            config.AppSettings.Settings.Remove("ApplicationName");
                                        }

                                        // Adding the ApplicationName property
                                        config.AppSettings.Settings.Add("ApplicationName", applicationName);

                                        // CountyName
                                        // check CountyName property null or not
                                        if (config.AppSettings.Settings["CountyName"] != null)
                                        {
                                            // if present remove the CountyName property
                                            config.AppSettings.Settings.Remove("CountyName");
                                        }

                                        // Adding the CountyName property
                                        config.AppSettings.Settings.Add("CountyName", countyName);

                                        // SMTPAddress
                                        // check SMTPAddress property null or not
                                        if (config.AppSettings.Settings["SMTPAddress"] != null)
                                        {
                                            // if present remove the SMTPAddress property
                                            config.AppSettings.Settings.Remove("SMTPAddress");
                                        }

                                        // Adding the SMTPAddress property
                                        config.AppSettings.Settings.Add("SMTPAddress", smtpAddress);

                                        // ReportViewerURL
                                        // check ReportViewerURL property null or not
                                        if (config.AppSettings.Settings["ReportViewerURL"] != null)
                                        {
                                            // if present remove the ReportViewerURL property
                                            config.AppSettings.Settings.Remove("ReportViewerURL");
                                        }

                                        // Adding the ReportViewerURL property
                                        config.AppSettings.Settings.Add("ReportViewerURL", reportViewerURL);

                                        // ExceptionMessage
                                        // check ExceptionMessage property null or not
                                        if (config.AppSettings.Settings["ExceptionMessage"] != null)
                                        {
                                            // if present remove the ExceptionMessage property
                                            config.AppSettings.Settings.Remove("ExceptionMessage");
                                        }

                                        // Adding the ExceptionMessage property
                                        config.AppSettings.Settings.Add("ExceptionMessage", exceptionMessage);

                                        // ToID
                                        // check ToID property null or not
                                        if (config.AppSettings.Settings["ToID"] != null)
                                        {
                                            // if present remove the ToID property
                                            config.AppSettings.Settings.Remove("ToID");
                                        }

                                        // Adding the ToID property
                                        config.AppSettings.Settings.Add("ToID", emailId);

                                        // ReportServiceAsmx
                                        // check ReportServiceAsmx property null or not
                                        if (config.AppSettings.Settings["ReportServiceAsmx"] != null)
                                        {
                                            // if present remove the ToID property
                                            config.AppSettings.Settings.Remove("ReportServiceAsmx");
                                        }

                                        // Adding the ToID property
                                        config.AppSettings.Settings.Add("ReportServiceAsmx", reportServiceAsmx);

                                        // ReportServiceAsmx
                                        // check ReportServiceAsmx property null or not
                                        if (config.AppSettings.Settings["ReportServiceAsmx"] != null)
                                        {
                                            // if present remove the ToID property
                                            config.AppSettings.Settings.Remove("ReportServiceAsmx");
                                        }

                                        // Adding the ToID property
                                        config.AppSettings.Settings.Add("ReportServiceAsmx", reportServiceAsmx);

                                        // check FieldDomainName property null or not
                                        if (config.AppSettings.Settings["FieldDomainName"] != null)
                                        {
                                            // if present remove the WebServiceUrl property
                                            config.AppSettings.Settings.Remove("FieldDomainName");
                                        }

                                        // Adding the FieldDomainName property
                                        config.AppSettings.Settings.Add("FieldDomainName", fieldDomainName);

                                        // check FieldMdfFilePath property null or not
                                        if (config.AppSettings.Settings["FieldMdfFilePath"] != null)
                                        {
                                            // if present remove the WebServiceUrl property
                                            config.AppSettings.Settings.Remove("FieldMdfFilePath");
                                        }

                                        // Adding the FieldMdfFilePath property
                                        config.AppSettings.Settings.Add("FieldMdfFilePath", mdfServerFilePath);

                                        // check FieldRequired property null or not
                                        if (config.AppSettings.Settings["FieldRequired"] != null)
                                        {
                                            // if present remove the WebServiceUrl property
                                            config.AppSettings.Settings.Remove("FieldRequired");
                                        }

                                        // Adding the FieldRequired property
                                        config.AppSettings.Settings.Add("FieldRequired", fieldRequired);

                                        // check ConnectionString property null or not
                                        if (config.AppSettings.Settings["ConnectionString"] != null)
                                        {
                                            // if present remove the WebServiceUrl property
                                            config.AppSettings.Settings.Remove("ConnectionString");
                                        }

                                        // Adding the ConnectionString property
                                        config.AppSettings.Settings.Add("ConnectionString", connectionString);

                                        //// WSURL Flag
                                        //// update wsyrl flag status to true
                                        // if (config.AppSettings.Settings["wsurlupdated"] != null)
                                        // {
                                        //    // if present remove the ToID property
                                        //    config.AppSettings.Settings.Remove("wsurlupdated");
                                        // }

                                        //// Adding the ToID property
                                        // config.AppSettings.Settings.Add("wsurlupdated", "True");

                                        // save the configration Information
                                        config.Save(System.Configuration.ConfigurationSaveMode.Modified, true);

                                        // Refresh the configration section
                                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                                        if (config.AppSettings.Settings["ReportURL"] != null)
                                        {
                                            // if present remove the ReportURL property
                                            config.AppSettings.Settings.Remove("ReportURL");
                                        }
                                        ////Added by Biju on 27-Oct-2010 to implement #8832
                                        if (config.AppSettings.Settings["LoginName"] != null)
                                            loginName = config.AppSettings.Settings["LoginName"].Value;
                                        ////till here
                                        // Adding the ReportURL property
                                        config.AppSettings.Settings.Add("ReportURL", reportURL);

                                        // save the configration Information
                                        config.Save(System.Configuration.ConfigurationSaveMode.Modified, true);

                                        // Refresh the configration section
                                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                                        // Gets the Flag value from Application Configuration


                                    }
                                }
                            }

                            //WSHelper.RefreshReportSettings();
                        }
                        catch (SoapException ex)
                        {
                            ////TODO : Need to find specific exception and handle it.
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        }
                    }
                }
                //// }

                //this.DomainComboBox.Items.Add(ConfigurationWrapper.DomainName);
                //this.DomainComboBox.SelectedIndex = 0;

                WSHelper.RefreshReportSettings();

                this.Activate();

                if (System.Configuration.ConfigurationManager.AppSettings["LoginName"] != null)
                {
                    loginName = System.Configuration.ConfigurationManager.AppSettings["LoginName"];
                }
                this.UserNameTextBox.Text = loginName;

                if (string.IsNullOrEmpty(this.UserNameTextBox.Text.Trim()))
                {
                    this.UserNameTextBox.Focus();
                }
                else
                {
                    this.UserNameTextBox.Select();
                   // this.UserNameTextBox.Focus();
                    //this.PasswordTextBox.Focus();
                }
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this);
            }
            finally
            {
                ShellApplication.prgfrm.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Form Closing event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.loginSuccess)
            {
                this.myloginControll.WorkItem.State["userclosing"] = true;
                if (MessageBox.Show(TerraScan.Utilities.SharedFunctions.GetResourceString("Exit"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.loginSuccess = false;
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the scan file.
        /// </summary>
        private void DeleteScanFile()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");

                if (dirInfo.Exists)
                {
                    FileInfo[] fileList = dirInfo.GetFiles();

                    if (fileList.Length > 0)
                    {
                        foreach (FileInfo file in fileList)
                        {
                            if (file.Name != "Thumbs.db")
                            {
                                System.IO.File.Delete(file.FullName);
                            }
                        }
                    }
                    else
                    {
                        ////Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");
                        ////centalFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "FinalImage.tif";
                    }
                }

                ////Final MODI

                DirectoryInfo dirInfor = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI");

                if (dirInfor.Exists)
                {
                    // Calls the method to Scan.
                    string[] deleteImageFiles = null;
                    string dire = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI";
                    deleteImageFiles = Directory.GetFiles(dire);
                    foreach (string dfilePath in deleteImageFiles)
                    {
                        if (System.IO.File.Exists(dfilePath))
                        {
                            try
                            {
                                System.IO.File.Delete(dfilePath);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Shows the shell form.
        /// </summary>
        private void ShowShellForm()
        {
            this.FindForm().Visible = false;
            ShellForm shellForm = new ShellForm();
            this.myloginControll.WorkItem.Items.Add(shellForm, "ShellForm");
            shellForm.Show();
        }

        /// <summary>
        /// Called when [line login].
        /// </summary>
        private void OnLineLogin()
        {
            WSHelper.IsOnLineMode = false;
            string domain = string.Empty;
            bool checkOutProccessed = false;
            this.checkDataSet = WSHelper.GetConfigInformation();
            if (!string.IsNullOrEmpty(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString()))
            {
                checkOutProccessed = Convert.ToBoolean(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString());
            }
            this.netNameDataSet = LoginController.GetUserNetName(this.UserNameTextBox.Text.Trim());

            if (this.netNameDataSet != null && this.netNameDataSet.Tables.Count > 0 && this.netNameDataSet.Tables[0].Rows.Count > 0)
            {
                if (this.netNameDataSet.Tables[0].Rows[0]["IsFieldUser"].Equals(true))
                {
                    if (this.netNameDataSet != null && this.netNameDataSet.Tables[1].Rows.Count > 0 && this.netNameDataSet.Tables[1].Rows[0][0].Equals(this.netNameDataSet.Tables[0].Rows[0]["UserID"]))
                    {
                        TerraScanCommon.IsFieldUser = true;
                        domain = this.netNameDataSet.Tables[0].Rows[0]["Name_Net"].ToString();
                        string labelMessage = string.Empty;

                        //Check NetWork Availability
                        if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                        {

                            if (!string.IsNullOrEmpty(this.PasswordTextBox.Text.Trim()))
                            {
                                string actualDomain = domain;
                                domain = domain.Substring(domain.LastIndexOf(@"\")).Replace("\\", "");
                                labelMessage = DomainAuthorization.CacheUserValidation(domain, this.PasswordTextBox.Text.Trim(), ConfigurationWrapper.FieldDomainName);
                                MainWorkItem.AuthorizeUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), actualDomain, this.authenticationState);
                                if (labelMessage == "success")
                                {

                                    this.LoginProcess();
                                }
                                else
                                {
                                    this.loginSuccess = false;
                                    this.ErrorLabel.Text = labelMessage;
                                }
                            }
                            else
                            {
                                this.loginSuccess = false;
                                this.ErrorLabel.Text = "Enter password";
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.PasswordTextBox.Text.Trim()))
                            {
                                string actualDomain = domain;
                                domain = domain.Substring(domain.LastIndexOf(@"\")).Replace("\\", "");
                                labelMessage = DomainAuthorization.CacheUserValidation(domain, this.PasswordTextBox.Text.Trim(), ConfigurationWrapper.FieldDomainName);
                                MainWorkItem.AuthorizeUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), actualDomain, this.authenticationState);
                                if (labelMessage == "success")
                                {

                                    this.LoginProcess();
                                }
                                else
                                {
                                    this.loginSuccess = false;
                                    this.ErrorLabel.Text = labelMessage;
                                }
                            }
                            else
                            {
                                this.loginSuccess = false;
                                this.ErrorLabel.Text = "Enter password";
                            }
                        }
                    }
                    else
                    {
                        //Check Out User not Entered
                        this.loginSuccess = false;
                        this.ErrorLabel.Text = "You are not authorized to login";

                    }

                }
                else
                {
                    this.loginSuccess = false;
                    this.ErrorLabel.Text = "Invalid Field User";

                }

            }
            else
            {
                this.loginSuccess = false;
                this.ErrorLabel.Text = SharedFunctions.GetResourceString("InvalidFullName");
            }
        }

        /// <summary>
        /// Logins the process.
        /// </summary>
        private void LoginProcess()
        {
            if (DomainAuthorization.ActiveDirectoryExists(ConfigurationWrapper.FieldDomainName))
            {
                // WSHelper.F9065_UpdateApplicationStatus(TerraScanCommon.CheckOutStatus, WSHelper.IsOnLineMode, TerraScanCommon.UserId);
                this.ShowShellForm();
            }
            else
            {
                if (!this.applicationFieldStatus)
                {
                    //if (MessageBox.Show(SharedFunctions.GetResourceString("SwitchFieldMode"), SharedFunctions.GetResourceString("LoginHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    int returnValue = WSHelper.F9065_UpdateApplicationStatus(TerraScanCommon.CheckOutStatus, false, TerraScanCommon.UserId);
                    this.ShowShellForm();
                    //}
                }
                else
                {
                    this.ShowShellForm();
                }
            }
        }

        /// <summary>
        /// Called when online login only.
        /// </summary>
        private void OnlineLoginOnly()
        {
            WSHelper.IsOnLineMode = true;
            string domain = string.Empty;
            bool checkOutProccessed = true;
            this.checkDataSet = WSHelper.GetConfigInformation();
            if (!string.IsNullOrEmpty(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString()))
            {
                checkOutProccessed = Convert.ToBoolean(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString());
            }

            this.netNameDataSet = LoginController.GetUserNetName(this.UserNameTextBox.Text.Trim());

            if (this.netNameDataSet != null && this.netNameDataSet.Tables[0].Rows.Count > 0)
            {
                //if (this.netNameDataSet.Tables[0].Rows.Count > 0)
                //{
                if (this.netNameDataSet.Tables[0].Rows[0]["IsFieldUser"].Equals(true))
                {
                    TerraScanCommon.IsFieldUser = true;
                    domain = this.netNameDataSet.Tables[0].Rows[0]["Name_Net"].ToString();
                    //                string labelMessage = LoginController.ValidateUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), this.DomainComboBox.Text.Trim(), this.authenticationState);
                    if (!string.IsNullOrEmpty(this.PasswordTextBox.Text.Trim()))
                    {
                        string labelMessage = string.Empty; 
                        string actualDomain = domain;
                        domain = domain.Substring(domain.LastIndexOf(@"\")).Replace("\\", "");
                        labelMessage = DomainAuthorization.CacheUserValidation(domain, this.PasswordTextBox.Text.Trim(), ConfigurationWrapper.FieldDomainName);
                        MainWorkItem.AuthorizeUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), actualDomain, this.authenticationState);
                        // labelMessage = LoginController.ValidateUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), domain, this.authenticationState);
                        if (labelMessage == "success")
                        {
                            ////this.DialogResult = DialogResult.OK;
                            this.FindForm().Visible = false;
                            ShellForm shellForm = new ShellForm();
                            this.myloginControll.WorkItem.Items.Add(shellForm, "ShellForm");
                            shellForm.Show();
                        }
                        else
                        {
                            this.loginSuccess = false;
                            this.ErrorLabel.Text = labelMessage;
                        }
                    }
                    else
                    {
                        this.loginSuccess = false;
                        this.ErrorLabel.Text = "Enter password";
                    }
                }
                else
                {
                    TerraScanCommon.IsFieldUser = false;
                    domain = this.netNameDataSet.Tables[0].Rows[0]["Name_Net"].ToString();
                    //                string labelMessage = LoginController.ValidateUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), this.DomainComboBox.Text.Trim(), this.authenticationState);
                    WSHelper.IsOnLineMode = true;
                     if (!string.IsNullOrEmpty(this.PasswordTextBox.Text.Trim()))
                      {
                                string labelMessage = LoginController.ValidateUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim(), domain, this.authenticationState);
                                if (labelMessage == "success")
                                {
                                    ////this.DialogResult = DialogResult.OK;
                                    this.FindForm().Visible = false;
                                    ShellForm shellForm = new ShellForm();
                                    this.myloginControll.WorkItem.Items.Add(shellForm, "ShellForm");
                                    shellForm.Show();
                                }
                                else
                                {
                                    this.loginSuccess = false;
                                    this.ErrorLabel.Text = labelMessage;
                                }
                     }
                     else
                     {
                         this.loginSuccess = false;
                         this.ErrorLabel.Text = "Enter password";
                     }
                }
                //}
                //else
                //{
                //    this.loginSuccess = false;
                //    this.ErrorLabel.Text = "Invalid Username or Password";
                //}
            }
            else
            {
                this.loginSuccess = false;
                this.ErrorLabel.Text = "Invalid user fullName";
            }
        }

        #endregion

    }
}