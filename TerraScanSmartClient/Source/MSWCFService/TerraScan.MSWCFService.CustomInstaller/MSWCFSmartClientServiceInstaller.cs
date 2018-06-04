using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Security.Permissions;
using System.Security;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Text;



namespace TerraScan.MSWCFService.CustomInstaller
{
    [RunInstaller(true)]
    public partial class MSWCFSmartClientServiceInstaller : Installer
    {
        public MSWCFSmartClientServiceInstaller()
        {
            InitializeComponent();
        }

        #region MSWCF Variables

        /// <summary>
        /// Cipher Wrapper Class
        /// </summary>
        TerraScan.CipherWrapper.CipherWrapper ciphwrp = new TerraScan.CipherWrapper.CipherWrapper();

        /// <summary>
        /// UserName
        /// </summary>
        string userName = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        string passWord = string.Empty;

        /// <summary>
        /// IIS Root Directory On Server
        /// </summary>
        string iisrootdirectory = string.Empty;

        /// <summary>
        /// Domain Name
        /// </summary>
        string domainName = string.Empty;

        /// <summary>
        /// Light Weight Active Directory Address 
        /// </summary>
        string ldap = string.Empty;

        /// <summary>
        /// Database Server Name.
        /// </summary>
        string databaseServer = string.Empty;

        /// <summary>
        /// Database Name.
        /// </summary>
        string databaseName = string.Empty;

        /// <summary>
        /// sa UserName.
        /// </summary>
        string sqlserversaUserName = string.Empty;

        /// <summary>
        /// sa Password.
        /// </summary>
        string sqlserversaPassword = string.Empty;

        /// <summary>
        /// DB Server Authentication.
        /// </summary>
        string databaseAuthentication = string.Empty;

        /// <summary>
        /// Encryption Key
        /// </summary>
        public byte[] key;

        /// <summary>
        /// UserName Key
        /// </summary>
        public byte[] usernameiv;

        /// <summary>
        /// Password Key
        /// </summary>
        public byte[] passwordiv;

        /// <summary>
        /// Database UserName Key
        /// </summary>
        public byte[] sausernameiv;

        /// <summary>
        /// Database Password Key
        /// </summary>
        public byte[] sapasswordiv;

        /// <summary>
        /// Impersonated UserName Byte Array
        /// </summary>
        public byte[] ByteImpUserName;

        /// <summary>
        /// Impersonated Password Byte Array
        /// </summary>
        public byte[] ByteImpPassword;

        /// <summary>
        /// Database UserName Byte Array
        /// </summary>
        public byte[] ByteDBUserName;

        /// <summary>
        /// Database Password Byte Array
        /// </summary>
        public byte[] ByteDBPassword;

        /// <summary>
        /// Cipher Impersonated UserName
        /// </summary>
        public byte[] cipherImpUserName;

        /// <summary>
        /// Cipher Impersonated Password
        /// </summary>
        public byte[] cipherImpPassword;

        /// <summary>
        /// Cipher Database UserName
        /// </summary>
        public byte[] cipherDBUserName;

        /// <summary>
        /// Cipher Database Password
        /// </summary>
        public byte[] cipherDBPassword;

        /// <summary>
        ///  DataBaseServer
        /// </summary>
        public string databaseServerInstall;
         /// <summary>
        ///  DataBaseServer
        /// </summary>
        public string databaseNameInstall;

        /// <summary>
        /// Database Authentication
        /// </summary>
        public string dbauthencticationInstall;
        


        #endregion

        #region Commit

        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);

            #region Write entries to the configuration

            // Domain Name
           // string domainName = this.Context.Parameters["DomainName"];

            // Light Weight Active Directory Address 
          ////  string ldap = this.Context.Parameters["Ldap"];

            // Database Server Name.
            string databaseServer = this.Context.Parameters["DBServer"];


            // Database Name.
            string databaseName = this.Context.Parameters["Database"];

            // sa UserName.
            string sqlserversaUserName = this.Context.Parameters["saUserName"];

            // sa Password.
            string sqlserversaPassword = this.Context.Parameters["saPassword"];

            // DB Server Authentication.
            string databaseAuthentication = this.Context.Parameters["dbAuthentication"];
            
           
            ////Username 
            string userName = Context.Parameters["USERNAME"];
    
         
            ////Password
            string passWord = Context.Parameters["PASSWORD"];

            // SA Authentication Expression
            
         
            string sqlserversaAuthentication = "Provider=sqloledb;uid=" + sqlserversaUserName + ";pwd=" + sqlserversaPassword + ";Initial Catalog=" + databaseName +";Data Source=" + databaseServer;

            // SA Authentication Expression
            //string sqlserversaAuthentication = "Server=" + databaseServer + ";uid=" + sqlserversaUserName + ";pwd=" + sqlserversaPassword + ";database=" + databaseName;

            // Windows Authentication Expression
            string winAuthentication = "Server=" + databaseServer + ";Database=" + databaseName + ";Trusted_Connection=yes";

            //MessageBox.Show(winAuthentication);
            System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
            string loc = asmbly.Location;
            string dir = Path.GetDirectoryName(loc);
            
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(dir + "\\" + "web.config");
            if (!fileInfo.Exists)
            {
                throw new InstallException("Missing config File");
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fileInfo.FullName);
            XmlNode node; 
            node = xmldoc.DocumentElement;
            foreach (XmlNode node1 in node.ChildNodes)
            {
        
                foreach (XmlNode node2 in node1.ChildNodes)
                {
                    if (node1.Name == "appSettings" && node2.Name == "add")
                    {
                        //if (node2.Attributes.GetNamedItem("key").Value == "DomainName")
                        //{
                        //    node2.Attributes.GetNamedItem("value").Value = "@" + domainName;
                        //}

                        if (node2.Attributes.GetNamedItem("key").Value == "ConnectionString")
                        {   
                            
                            if (databaseAuthentication == "2")
                            {

                                node2.Attributes.GetNamedItem("value").Value = winAuthentication;
                            }
                            else if (databaseAuthentication == "1")
                            {

                                node2.Attributes.GetNamedItem("value").Value = sqlserversaAuthentication;
                            }
                        }

                    }

                    if (node1.Name == "system.web" && node2.Name == "identity")
                      {
                        
                        if (node2.Attributes.GetNamedItem("impersonate").Value == "true")
                        {
                            node2.Attributes.GetNamedItem("userName").Value = userName;
                            node2.Attributes.GetNamedItem("password").Value = passWord;
                        
                        }
                    }
                }


            }

            xmldoc.Save(fileInfo.FullName);

            #endregion Write entries to the configuration

            
            
            #region Register Re7Engine DLL
            
            string re7dllpath = dir + "\\bin\\RE7Engine.dll";
            TerraScan.MSWCF.RegisterComponent.DllRegServer reg = new TerraScan.MSWCF.RegisterComponent.DllRegServer(re7dllpath);
            reg.Register();

            
            #endregion

            #region Register CESTEngine DLL
            
            string ce7dllpath = dir + "\\bin\\CESTEngine.dll";
            TerraScan.MSWCF.RegisterComponent.DllRegServer regCEST = new TerraScan.MSWCF.RegisterComponent.DllRegServer(ce7dllpath);
            regCEST.Register();
             
            
            #endregion
            ////#region Register RDO DLL

            ////string RDOdllpath = dir + "\\bin\\RDOCURS.dll";
            ////TerraScan.MSWCF.RegisterComponent.DllRegServer regRDO = new TerraScan.MSWCF.RegisterComponent.DllRegServer(RDOdllpath);
            ////regRDO.Register();

            ////#endregion
        }

        #endregion Commit

        #region Install

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            #region Write entries to the configuration

            // Domain Name
          //  string domainName = this.Context.Parameters["DomainName"];

            // Light Weight Active Directory Address 
           //// string ldap = this.Context.Parameters["Ldap"];

            // Database Server Name.
             databaseServerInstall = this.Context.Parameters["DBServer"];
            

            // Database Name.
             databaseNameInstall = this.Context.Parameters["Database"];

            // sa UserName.
            string sqlserversaUserName = this.Context.Parameters["saUserName"];

            // sa Password.
            string sqlserversaPassword = this.Context.Parameters["saPassword"];

            // DB Server Authentication.
            string databaseAuthentication = this.Context.Parameters["dbAuthentication"];

            dbauthencticationInstall = this.Context.Parameters["dbAuthentication"];
            
            ////Username 
            string userName = Context.Parameters["USERNAME"];

            ////Password
            string passWord = Context.Parameters["PASSWORD"];

            // SA Authentication Expression
            string sqlserversaAuthentication = "Provider=sqloledb;uid=" + sqlserversaUserName + ";pwd=" + sqlserversaPassword + ";Initial Catalog=" + databaseName + ";Data Source=" + databaseServer;

            // Windows Authentication Expression
            string winAuthentication = "Server=" + databaseServer + ";Database=" + databaseName + ";Trusted_Connection=yes";

            //MessageBox.Show(winAuthentication);
            //System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
            //string loc = asmbly.Location;
            //string dir = Path.GetDirectoryName(loc);
            #endregion Write entries to the configuration
            
            #region Encrypt Registry Details
            this.EncryptRegistryDetails(userName, passWord, sqlserversaUserName, sqlserversaPassword);

            
            #endregion
            #region impersonate
            try
            {

                 
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                string assemblylocation = assembly.Location;
                string dir = Path.GetDirectoryName(assemblylocation);

                ////creating the instance for diagnostics process
                System.Diagnostics.Process run = new System.Diagnostics.Process();

                ////giving startinfo filename from argument 2
                run.StartInfo.FileName = dir + "\\" + "aspnet_setreg.exe";

                ////MessageBox.Show("FileName" + run.StartInfo.FileName);

                ////giving arguments for file start info.
                run.StartInfo.Arguments = @"-k:SOFTWARE\TerrascanMSWCFService\identity -u:" + '"' + userName + '"' + " -p:" + '"' + passWord + '"';

                ////MessageBox.Show("Arguments " + run.StartInfo.Arguments);

                ////TO hide the Command window
                run.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                ////starting the process
                run.Start();

                run.WaitForExit();

                
                Microsoft.Win32.RegistryKey rk;
                RegistryAccessRule rule = new RegistryAccessRule("NETWORK SERVICE", RegistryRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
                rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TerrascanMSWCFService\identity\ASPNET_SETREG", true);
                RegistrySecurity security = new RegistrySecurity();
                security = rk.GetAccessControl();
                security.AddAccessRule(rule);
                rk.SetAccessControl(security);
                rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TerrascanMSWCFService", true);
                security = rk.GetAccessControl();
                security.AddAccessRule(rule);
                rk.SetAccessControl(security);


            }
            catch (Exception ex)
            {
                throw new InstallException(ex.Message);
            }

            #endregion
        }

        #endregion Install

        #region Uninstall

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);

            #region Remove the entries from registry

            try
            {
                Microsoft.Win32.RegistryKey registryKey;
                registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE", true);

                if (registryKey == null)
                {
                    return;
                }
                else
                {
                    registryKey.DeleteSubKeyTree("Terrascan");
                    return;
                }
            }

            catch
            {
                return;
            }

            #region Un-Register Re7Engine DLL
            //string re7dllpath = dir + "\\bin\\RE7Engine.dll";
            //TerraScan.MSWCF.RegisterComponent.DllRegServer reg = new TerraScan.MSWCF.RegisterComponent.DllRegServer(re7dllpath);
            //reg.Register();
            #endregion

            #endregion Remove the entries from registry

        }

        #endregion Uninstall

        #region Encrypt Registry Details
        /// <summary>
        /// Encrypts the registry details.
        /// </summary>
        /// <param name="ImpersonateUserName">Name of the impersonate user.</param>
        /// <param name="ImpersonatePassword">The impersonate password.</param>
        /// <param name="DatabaseUserName">Name of the database user.</param>
        /// <param name="DatabasePassword">The database password.</param>
        public void EncryptRegistryDetails(string ImpersonateUserName, string ImpersonatePassword, string DatabaseUserName, string DatabasePassword)
        {

            key = ciphwrp.Key;
            usernameiv = ciphwrp.AsymmIV;
            passwordiv = ciphwrp.AsymmIV;
            sausernameiv = ciphwrp.AsymmIV;
            sapasswordiv = ciphwrp.AsymmIV;


            RegistryPermission fwrite = new RegistryPermission(RegistryPermissionAccess.Write, "HKLM\\SOFTWARE\\");
            fwrite.AddPathList(RegistryPermissionAccess.Write, "HKLM\\SOFTWARE\\");
     
            RegistryKey OurKey = Registry.LocalMachine; // Create OurKey set to HKEY_LOCAL_MACHINE 
            OurKey = OurKey.OpenSubKey("SOFTWARE", true); // Set it to HKEY_LOCAL_MACHINE\SOFTWARE 
            OurKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            OurKey.CreateSubKey(@"TerraScan\TerraScanWCF"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            OurKey = OurKey.OpenSubKey(@"TerraScan\TerraScanWCF", true);
           // OurKey.SetValue("LDAP", ldap);
          //  OurKey.SetValue("DomainName", domainName);
          //  OurKey.SetValue("InstallDirectory", iisrootdirectory);
            OurKey.SetValue("DatabaseServer", databaseServerInstall);
            OurKey.SetValue("DatabaseName", databaseNameInstall);
            OurKey.SetValue("DatabaseAuthentication", dbauthencticationInstall);
            OurKey.SetValue("Version", "1.0.25.36");
            OurKey.Close();
            OurKey = null;

            RegistryKey ComponentIDKey = Registry.LocalMachine; // Create OurKey set to HKEY_LOCAL_MACHINE 
            ComponentIDKey = ComponentIDKey.OpenSubKey("SOFTWARE", true); // Set it to HKEY_LOCAL_MACHINE\SOFTWARE 
            ComponentIDKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            ComponentIDKey.CreateSubKey(@"TerraScan\ComponentID"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            ComponentIDKey = ComponentIDKey.OpenSubKey(@"TerraScan\ComponentID", true);
            ComponentIDKey.SetValue("ComponentID", "1");
            ComponentIDKey.Close();
            ComponentIDKey = null;

            bool testciphercreate = this.createCiphers(ImpersonateUserName, ImpersonatePassword, DatabaseUserName, DatabasePassword);

            if (testciphercreate == true)
            {
                try
                {
                    cipherImpUserName = ciphwrp.EncryptMessage(ByteImpUserName, key, out usernameiv);
                    cipherImpPassword = ciphwrp.EncryptMessage(ByteImpPassword, key, out passwordiv);
                    cipherDBUserName = ciphwrp.EncryptMessage(ByteDBUserName, key, out sausernameiv);
                    cipherDBPassword = ciphwrp.EncryptMessage(ByteDBPassword, key, out sapasswordiv);
                }
                catch (System.Security.SecurityException sec)
                {
                    MessageBox.Show(sec.Message);
                }
            }

            RegistryKey EncryptOurKey = Registry.LocalMachine; // Create OurKey set to HKEY_LOCAL_MACHINE 
            EncryptOurKey = EncryptOurKey.OpenSubKey("SOFTWARE", true); // Set it to HKEY_LOCAL_MACHINE\SOFTWARE 
            EncryptOurKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            EncryptOurKey.CreateSubKey(@"TerraScan\TerraScanMSWCF"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            EncryptOurKey = EncryptOurKey.OpenSubKey(@"TerraScan\TerraScanMSWCF", true);
            EncryptOurKey.SetValue("ImpersonatedUserName", cipherImpUserName);
            EncryptOurKey.SetValue("ImpersonatedPassword", cipherImpPassword);
            EncryptOurKey.SetValue("DatabaseUserName", cipherDBUserName);
            EncryptOurKey.SetValue("DatabasePassword", cipherDBPassword);
            EncryptOurKey.Close();
            EncryptOurKey = null;

            RegistryKey ClrKey = Registry.ClassesRoot; // Create OurKey set to HKEY_LOCAL_MACHINE 
            ClrKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            ClrKey.CreateSubKey(@"TerraScan\TerraScanMSWCF");
            ClrKey = ClrKey.OpenSubKey(@"TerraScan\TerraScanMSWCF", true);
            ClrKey.SetValue("key", key);
            ClrKey.SetValue("usernameiv", usernameiv);
            ClrKey.SetValue("passwordiv", passwordiv);
            ClrKey.SetValue("sausernameiv", sausernameiv);
            ClrKey.SetValue("sapasswordiv", sapasswordiv);
            ClrKey.Close();
            ClrKey = null;


        }
        #endregion

        #region Create Ciphers
        /// <summary>
        /// Creates the ciphers.
        /// </summary>
        /// <param name="ImpUserName">Name of the impersonated user.</param>
        /// <param name="ImpPassword">The impersonated password.</param>
        /// <param name="DBUserName">Name of the Database user.</param>
        /// <param name="DBPassword">The Database password.</param>
        /// <returns></returns>
        private bool createCiphers(string ImpUserName, string ImpPassword, string DBUserName, string DBPassword)
        {
            try
            {
                ByteImpUserName = Encoding.Unicode.GetBytes(ImpUserName);
                ByteImpPassword = Encoding.Unicode.GetBytes(ImpPassword);
                ByteDBUserName = Encoding.Unicode.GetBytes(DBUserName);
                ByteDBPassword = Encoding.Unicode.GetBytes(DBPassword);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion
    }
}