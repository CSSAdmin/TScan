//--------------------------------------------------------------------------------------------
// <copyright file="SmartClientInstaller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the SmartClient Service ClickOnce Setup.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-03-2007       guhan               removed databaseAuthentication
// 11 JAN 2016      Priyadharshini      Custom Installer Code
//*********************************************************************************/

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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;
//using Microsoft.Web.Administration;
using System.Collections;
using System.DirectoryServices;

/// <summary>
/// Partial Class
/// </summary>
namespace TerraScan.WCFService.CustomInstaller
{
    /// <summary>
    /// WCF Installer Class
    /// </summary>
    [RunInstaller(true)]
    public partial class SmartClientServiceInstaller : Installer
    {
        #region Import
        /// <summary>
        /// Import the DLL.
        /// </summary>
        [DllImport("kernel32.dll",
           EntryPoint = "GetStdHandle",
           SetLastError = true,
           CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.StdCall)]

        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -12;
        private const int MY_CODE_PAGE = 437;  

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        IntPtr handle;
        
        #endregion

        #region Initialize
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartClientServiceInstaller"/> class.
        /// </summary>
        public SmartClientServiceInstaller()
        {
            InitializeComponent();
        }
        #endregion

        #region WCF Variables

        /// <summary>
        /// Cipher Wrapper Class
        /// </summary>
        TerraScan.CipherWrapper.CipherWrapper ciphwrp = new TerraScan.CipherWrapper.CipherWrapper();



        static string xmlPath = "";

        public static string dllVersion = "2.177.24";

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
       //// string databaseAuthentication = string.Empty;

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

        
        #endregion

        #region Commit
        /// <summary>
        /// When overridden in a derived class, completes the install transaction.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary"/> that contains the state of the computer after all the installers in the collection have run.</param>
        public override void Commit(System.Collections.IDictionary savedState)
        {
            try
            {
            base.Commit(savedState);

            #region Write entries to the configuration
            System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
            string loc = asmbly.Location;
            string dir = Path.GetDirectoryName(loc);
            string domainName = "";
            string ldap = "";
            string databaseServer = "";
            string databaseName = "";
            string sqlserversaUserName = "";
            string sqlserversaPassword = "";
            string userName = "";
            string passWord = "";
            string cmdDirectorylocation = "";
            string Directorylocation = "";
            string virdir = string.Empty;
            XmlDocument xml = new XmlDocument();
            string[] virSplits = new string[100];
            string t2virdir = string.Empty;
            string t2Phydir = string.Empty;
            string OriPhysicalDir = string.Empty;
            //For Manual Setup
            if (string.IsNullOrEmpty(xmlPath))
            {
                RegistryKey ourKey = Registry.LocalMachine;
                ourKey = ourKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
                Directorylocation = ourKey.GetValue("").ToString();
                ourKey.Close();
                ourKey = null;
                xml.Load(Directorylocation + "\\setup.xml");
            }
            //For Command Prompt
            else
            {
                RegistryKey cmdKey = Registry.LocalMachine;
                cmdKey = cmdKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
                cmdDirectorylocation = cmdKey.GetValue("").ToString();
                cmdKey.Close();
                cmdKey = null;

                string[] smlcharacters = xmlPath.Split('\\');
                int a = smlcharacters.Length;
                if (a > 1)
                    xml.Load(xmlPath);
                else
                {
                    string tempxmlPath = cmdDirectorylocation + "\\" + xmlPath;
                    xml.Load(tempxmlPath);
                }

            }

            XmlNodeList xnList = xml.SelectNodes("//WCF");

            foreach (XmlNode xn in xnList)
            {
                ////username
                userName = xn.Attributes["UserName"].Value;

                ////Password
                passWord = xn.Attributes["Password"].Value;

               
                // Domain Name
                domainName = xn.Attributes["DomainName"].Value;


                // Light Weight Active Directory Address 
                ldap = xn.Attributes["LDAP"].Value;

                // Database Server Name.
                databaseServer = xn.Attributes["DbServerName"].Value;

                // Database Name.
                databaseName = xn.Attributes["DbName"].Value;
                //handle
                if (string.IsNullOrEmpty(xmlPath))
                {
                    int ihandle = Int32.Parse(xn.Attributes["Handle"].Value);
                    handle = (IntPtr)ihandle;
                }

            }
            if (!string.IsNullOrEmpty(xmlPath))
            {

                XmlNodeList xnT2List = xml.SelectNodes("//Treasurer");
                foreach (XmlNode xn in xnT2List)
                {
                    t2virdir = xn.Attributes["TARGETVDIR"].Value;
                    t2Phydir = xn.Attributes["PhysicalDir"].Value;
                }
            }
            t2Phydir = t2Phydir.TrimEnd('\\');

            // SA Authentication Expression
            string sqlserversaAuthentication = "Server=" + databaseServer + ";uid=" + sqlserversaUserName + ";pwd=" + sqlserversaPassword + ";database=" + databaseName;

            // Windows Authentication Expression
            string winAuthentication = "Server=" + databaseServer + ";Database=" + databaseName + ";Trusted_Connection=yes";

            //MessageBox.Show(winAuthentication);

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
                        if (node2.Attributes.GetNamedItem("key").Value == "DomainName")
                        {
                            node2.Attributes.GetNamedItem("value").Value = "@" + domainName;
                        }

                        if (node2.Attributes.GetNamedItem("key").Value == "ConnectionString")
                        {
                            node2.Attributes.GetNamedItem("value").Value = winAuthentication;
                        }

                    }

                    if (node1.Name == "connectionStrings" && node2.Name == "add")
                    {
                        if (node2.Attributes.GetNamedItem("name").Value == "TerrascanConnString")
                        {
                            node2.Attributes.GetNamedItem("connectionString").Value = ldap;
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

            #region Treasurer setup calling

            if (string.IsNullOrEmpty(xmlPath))
            {
                Process pr = new Process();
                pr.StartInfo.FileName = Directorylocation + "\\TerraScan.TreasurerSetup\\setup.exe";
                pr.StartInfo.Arguments = "filepath=setup.xml Test=No";
                //pr.StartInfo.Arguments = "/qb filepath=" + xmlPath;
                pr.Start();

            }
            else
            {
                string[] tempSplits=new string[30];
                AllocConsole();
                IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
                SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
                FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
                Encoding encoding = System.Text.Encoding.GetEncoding(MY_CODE_PAGE);
                StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
                standardOutput.AutoFlush = true;
                Console.SetOut(standardOutput);
                Console.WriteLine("WCF Installation Started");
                Console.WriteLine("Please Wait.....");
                virSplits = t2virdir.Split('/');
                tempSplits = t2Phydir.Split('\\');


                string tempt2VirtualDirectory = string.Empty;
                if (t2virdir.Contains("/"))
                {
                    tempt2VirtualDirectory = t2virdir.Replace("/", "\\");
                }
                else
                {
                    tempt2VirtualDirectory = t2virdir;
                }

                //if (t2Phydir.ToLower().Contains("\\" + tempt2VirtualDirectory.ToLower()))
                //{
                //    bool validate = true;
                //    int at1 = 0;
                //    while (validate)
                //    {
                //        if (true)
                //            at1 = t2Phydir.IndexOf(("\\" + tempt2VirtualDirectory), at1, t2Phydir.Length - at1, StringComparison.OrdinalIgnoreCase);
                //        else
                //            at1 = t2Phydir.IndexOf(("\\" + tempt2VirtualDirectory), at1);

                //        if (at1 == -1)
                //        {
                //            validate = false;
                //        }

                //        else
                //        {
                //            t2Phydir = t2Phydir.Substring(0, at1) + "" + t2Phydir.Substring(at1 + ("\\" + tempt2VirtualDirectory).Length);

                //            at1 += string.Empty.Length;
                //        }
                //    }
                    
                //    //t2Phydir = t2Phydir.Replace("\\" + tempt2VirtualDirectory, "");
                //}
                if (virSplits.Length == 1 && !string.IsNullOrEmpty(t2Phydir))
                {
                    if (string.IsNullOrEmpty(tempSplits[1]))
                        OriPhysicalDir = t2Phydir + virSplits[0];
                    else
                        OriPhysicalDir = t2Phydir + "\\" + virSplits[0];
                       

                   //priya
                   long siteId = 0;
                   DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");
                   foreach (DirectoryEntry de in w3svc.Children)
                   {
                       if (de.SchemaClassName == "IIsWebServer" && de.Properties["ServerComment"][0].ToString() == "Default Web Site")
                       {
                           siteId = Convert.ToInt64(de.Name);
                       }
                   }
                   //CreateVDir("IIS://Localhost/W3SVC/" + siteId + "/Root", virSplits[0], OriPhysicalDir);
                   CreateVDir("IIS://Localhost/W3SVC/" + siteId + "/Root", virSplits[0], t2Phydir);
                   //ServerManager iisManager = new ServerManager();
                   //Site mySite = iisManager.Sites["Default Web Site"];
                   //mySite.Applications[0].VirtualDirectories.Add("/" + virSplits[0], OriPhysicalDir);
                   //iisManager.CommitChanges();
                }
                Process pr = new Process();
                pr.StartInfo.FileName = cmdDirectorylocation + "\\TerraScan.TreasurerSetup\\setup.exe";
               // pr.StartInfo.Arguments = "/q Test=Yes iisrootdirectory=" + "\"" + OriPhysicalDir + "\" TARGETVDIR=" + "\"" + t2virdir + "\" filepath=" + "\"" + xmlPath + "\"";
                pr.StartInfo.Arguments = "/q Test=Yes iisrootdirectory=" + "\"" + t2Phydir + "\" TARGETVDIR=" + "\"" + t2virdir + "\" filepath=" + "\"" + xmlPath + "\"";
                // pr.StartInfo.Arguments = "/q  filepath=" + "\"" + xmlPath + "\"";
                Thread.Sleep(30000);
                Console.WriteLine("WCF Installation Completed");
                //pr.StartInfo.Arguments = "/qb filepath=" + xmlPath;
                pr.Start();
            }


            foreach (Process process in Process.GetProcesses())
            {

                if (process.MainWindowTitle == "TerraScan.WCFService.Setup")
                {
                    process.Kill();

                }
            }
            #endregion
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(xmlPath))
                    throw new InstallException(ex.Message);
                else
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                    throw new InstallException(ex.Message);
                }

            }
        }

        #endregion Commit

        public static string ReplaceString(string OrigString, string FindString,
                                 string ReplaceString, bool CaseInsensitive)
        {
            int at1 = 0;
            while (true)
            {
                if (CaseInsensitive)
                    at1 = OrigString.IndexOf(FindString, at1, OrigString.Length - at1, StringComparison.OrdinalIgnoreCase);
                else
                    at1 = OrigString.IndexOf(FindString, at1);

                if (at1 == -1)
                    return OrigString;

                OrigString = OrigString.Substring(0, at1) + ReplaceString + OrigString.Substring(at1 + FindString.Length);

                at1 += ReplaceString.Length;
            }

            return OrigString;
        }


        #region Install
      /// <summary>
      /// When overridden in a derived class, performs the installation.
      /// </summary>
      /// <param name="stateSaver">An <see cref="T:System.Collections.IDictionary"/> used to save information needed to perform a commit, rollback, or uninstall operation.</param>
       public override void Install(System.Collections.IDictionary stateSaver)
            {
              
                try
                {
                /// <summary>
                /// Getting Assemblt path
                /// </summary>
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                string assemblylocation = assembly.Location;
                string dir = Path.GetDirectoryName(assemblylocation);
                base.Install(stateSaver);

                #region Context Parameters
                xmlPath = Context.Parameters["filepath"]; 
                string virtualdir = Context.Parameters["TARGETVDIR"];
                string targetdir = Context.Parameters["iisrootdirectory"];
                RegistryKey wcfUninstalKey = Registry.LocalMachine;
                wcfUninstalKey = wcfUninstalKey.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "wcf.exe");
                wcfUninstalKey.SetValue("", "/" + virtualdir);
                wcfUninstalKey.Close();
                wcfUninstalKey = null;
                XmlDocument xml = new XmlDocument();

                if (string.IsNullOrEmpty(xmlPath))
                {
                    RegistryKey ourKey = Registry.LocalMachine;
                    ourKey = ourKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
                    string Directorylocation =Convert.ToString(ourKey.GetValue(""));
                    ourKey.Close();
                    ourKey = null;
                    xml.Load(Directorylocation + "\\setup.xml");
                }
                else
                {
                    RegistryKey cmdKey = Registry.LocalMachine;
                    cmdKey = cmdKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
                    string cmdDirectorylocation = Convert.ToString(cmdKey.GetValue(""));
                    cmdKey.Close();
                    cmdKey = null;
                    string[] smlcharacters = xmlPath.Split('\\');
                    int a = smlcharacters.Length;
                    if (a > 1)
                        xml.Load(xmlPath);
                    else
                        xml.Load(cmdDirectorylocation + "\\" + xmlPath);
                }

                XmlNodeList xnList = xml.SelectNodes("//WCF");
                foreach (XmlNode xn in xnList)
                {
                    ////Username 
                    userName = xn.Attributes["UserName"].Value;
                    
                    ////Password
                    passWord =  xn.Attributes["Password"].Value;

                    // IIS Root Directory on Server
                    //iisrootdirectory = this.Context.Parameters["IISRootDirectory"];

                    // Domain Name
                    domainName = xn.Attributes["DomainName"].Value;

                    // Light Weight Active Directory Address 
                    ldap = xn.Attributes["LDAP"].Value;

                    // Database Server Name.
                    databaseServer =  xn.Attributes["DbServerName"].Value;

                    // Database Name.
                    databaseName = xn.Attributes["DbName"].Value;
                }
                #endregion

                #region Encrypt Registry Details
                this.EncryptRegistryDetails(userName, passWord, sqlserversaUserName, sqlserversaPassword);
                #endregion

                #region impersonate
                
                    //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    //string assemblylocation = assembly.Location;
                    //string dir = Path.GetDirectoryName(assemblylocation);

                    ////creating the instance for diagnostics process
                    System.Diagnostics.Process run = new System.Diagnostics.Process();

                    ////giving startinfo filename from argument 2
                    run.StartInfo.FileName = dir + "\\" + "aspnet_setreg.exe";

                    ////MessageBox.Show("FileName" + run.StartInfo.FileName);

                    ////giving arguments for file start info.
                    run.StartInfo.Arguments = @"-k:SOFTWARE\Terrascan\identity -u:" + '"' + userName + '"' + " -p:" + '"' + passWord + '"';

                    ////MessageBox.Show("Arguments " + run.StartInfo.Arguments);

                    ////TO hide the Command window
                    run.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                    ////starting the process
                    run.Start();

                    run.WaitForExit();

                    Microsoft.Win32.RegistryKey rk;
                    RegistryAccessRule rule = new RegistryAccessRule("NETWORK SERVICE", RegistryRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
                    rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Terrascan\identity\ASPNET_SETREG", true);
                    RegistrySecurity security = new RegistrySecurity();
                    security = rk.GetAccessControl();
                    security.AddAccessRule(rule);
                    rk.SetAccessControl(security);
                    rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Terrascan", true);
                    security = rk.GetAccessControl();
                    security.AddAccessRule(rule);
                    rk.SetAccessControl(security);
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(xmlPath))
                        throw new InstallException(ex.Message);
                    else
                    {
                        MessageBox.Show(ex.Message, "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        throw new InstallException(ex.Message);
                    }
                   
                }

                #endregion
            }

       #endregion Install

        #region Uninstall
        /// <summary>
        /// When overridden in a derived class, removes an installation.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary"/> that contains the state of the computer after the installation was complete.</param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            RegistryKey ourKey = Registry.LocalMachine;
            string Directorylocation = "";
            string VirFolder = "";
            string[] virdir = null;
            if (ourKey != null)
            {
                ourKey = ourKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "wcf.exe", true);
                Directorylocation = Convert.ToString(ourKey.GetValue(""));
                if (!string.IsNullOrEmpty(Directorylocation))
                {
                     virdir = Directorylocation.Split('/');
                     if (virdir.Length > 0)
                     {
                         VirFolder = virdir[1];
                     }
                    ourKey.Close();
                    ourKey = null;
                }
            }
            /// <summary>
            /// Delete the application in the iis.
            /// </summary>
            object physicalpath = "";
            if (!string.IsNullOrEmpty(Directorylocation) && Directorylocation != "/")
            {


                long siteId = 0;
                DirectoryEntry VDir = new DirectoryEntry("IIS://localhost/W3SVC");

                foreach (DirectoryEntry de in VDir.Children)
                {

                    if (de.SchemaClassName == "IIsWebServer" && de.Properties["ServerComment"][0].ToString() == "Default Web Site")
                    {
                        siteId = Convert.ToInt64(de.Name);
                        string virtualDirectoryPath = "IIS://localhost/W3SVC/" + siteId + "/ROOT" + Directorylocation;
                        DirectoryEntry virtualDirectory = new DirectoryEntry(virtualDirectoryPath);
                        physicalpath = virtualDirectory.Properties["Path"].Value;
                        virtualDirectory.DeleteTree();
                        //foreach (DirectoryEntry de1 in virtualDirectory.Children)
                        //{

                        //    physicalpath = de1.Properties["path"].Value.ToString();
                        //    //if (de1.Properties["path"].Value.ToString().Equals(Directorylocation, StringComparison.OrdinalIgnoreCase))
                        //    //{
                        //    //    string app = de1.Properties["AppFriendlyName"].Value.ToString();
                        //    //}
                        //}
                    }
                }


                //ServerManager manager = new ServerManager();
                //List<Microsoft.Web.Administration.Application> appsToRemove = new List<Microsoft.Web.Administration.Application>();
                //for (int i = 0; i < manager.Sites["Default Web Site"].Applications.Count; i++)
                //{
                //    if ((manager.Sites["Default Web Site"].Applications[i].Path).Equals(Directorylocation, StringComparison.OrdinalIgnoreCase))
                //    {
                //        appsToRemove.Add(manager.Sites["Default Web Site"].Applications[i]);
                //        physicalpath = manager.Sites["Default Web Site"].Applications[i].VirtualDirectories[0].PhysicalPath;
                //    }
                //}
                //foreach (Microsoft.Web.Administration.Application a in appsToRemove)
                //{
                //    manager.Sites["Default Web Site"].Applications.Remove(a);

                //}
                //manager.CommitChanges();
            }

        #endregion

            base.Uninstall(savedState);

            #region Remove the entries from registry
            try
            {
                Process application = null;
                foreach (var process in Process.GetProcesses())
                {
                    if (!process.ProcessName.ToLower().Contains("Terrascan.WCFService.Setup")) continue;
                    application = process;
                    break;
                }

                if (application != null && application.Responding)
                {
                    application.Kill();
                    base.Uninstall(savedState);
                }
                RegistryKey DeleteKey = Registry.LocalMachine;
                DeleteKey = DeleteKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "wcf.exe", true);
                if (DeleteKey != null)
                {
                    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "wcf.exe", true);
                    DeleteKey.Close();
                    DeleteKey = null;
                }

            }
            catch
            {
                return;
            }
            finally
            {
                #region For deleting Root Tree

                try
                {
                    if (!string.IsNullOrEmpty(physicalpath.ToString()))
                    {
                        physicalpath = Path.GetDirectoryName(physicalpath.ToString());
                        string[] dirs = Directory.GetDirectories(physicalpath.ToString());
                        long siteId = 0;
                        DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");
                        foreach (DirectoryEntry de in w3svc.Children)
                        {
                            if (de.SchemaClassName == "IIsWebServer" && de.Properties["ServerComment"][0].ToString() == "Default Web Site")
                            {
                                siteId = Convert.ToInt64(de.Name);
                            }
                        }

                        long siteId1 = 0;
                        DirectoryEntry VDir1 = new DirectoryEntry("IIS://localhost/W3SVC");
                        List<object> obj = new List<object>();
                        foreach (DirectoryEntry de in VDir1.Children)
                        {
                            if (de.SchemaClassName == "IIsWebServer" && de.Properties["ServerComment"][0].ToString() == "Default Web Site")
                            {
                                siteId1 = Convert.ToInt64(de.Name);
                                string virtualDirectoryPath = "IIS://localhost/W3SVC/" + siteId1 + "/ROOT/" + VirFolder;
                                DirectoryEntry virtualDirectory = new DirectoryEntry(virtualDirectoryPath);
                                foreach (DirectoryEntry de1 in virtualDirectory.Children)
                                {
                                    obj.Add(de1.Properties["Path"].Value);
                                }
                            }
                        }
                        if (obj.Count == 0)
                        {
                            string vDirPath = "IIS://localhost/W3SVC/" + siteId + "/ROOT/" + VirFolder + "";
                            DirectoryEntry virtualDirectory = new DirectoryEntry(vDirPath);
                            if (virtualDirectory != null)
                            {
                                virtualDirectory.DeleteTree();
                            }
                        }
                        //else
                        //{
                        //    ServerManager serverManager = new ServerManager();
                        //    long siteid = serverManager.Sites["Default Web Site"].Id;
                        //    string vDirPath = "IIS://localhost/W3SVC/" + siteid + "/ROOT/" + VirFolder + "";
                        //    DirectoryEntry vDir = new DirectoryEntry(vDirPath);
                        //    vDir.DeleteTree();
                        //}
                    }

                }
                catch (Exception ex)
                {
                }

                #endregion
            }
        }

        public static void CreateVDir(string metabasePath, string vDirName, string physicalPath)
        {
            //  metabasePath is of the form "IIS://<servername>/<service>/<siteID>/Root[/<vdir>]"
            //    for example "IIS://localhost/W3SVC/1/Root" 
            //  vDirName is of the form "<name>", for example, "MyNewVDir"
            //  physicalPath is of the form "<drive>:\<path>", for example, "C:\Inetpub\Wwwroot"
            try
            {
                DirectoryEntry site = new DirectoryEntry(metabasePath);
                string className = site.SchemaClassName.ToString();
                if ((className.EndsWith("Server")) || (className.EndsWith("VirtualDir")))
                {
                    DirectoryEntries vdirs = site.Children;
                    DirectoryEntry newVDir = vdirs.Add(vDirName, (className.Replace("Service", "VirtualDir")));
                    newVDir.Properties["Path"][0] = physicalPath;
                    newVDir.Properties["AccessScript"][0] = true;
                    // These properties are necessary for an application to be created.
                    //newVDir.Properties["AppFriendlyName"][0] = vDirName;
                    //newVDir.Properties["AppIsolated"][0] = "1";
                    //newVDir.Properties["AppRoot"][0] = "/LM" + metabasePath.Substring(metabasePath.IndexOf("/", ("IIS://".Length)));
                    newVDir.CommitChanges();
                }
                else
                    Console.WriteLine(" Failed. A virtual directory can only be created in a site or virtual directory node.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed in CreateVDir with the following exception: \n{0}", ex.Message);
            }

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
            OurKey.SetValue("LDAP", ldap);
            OurKey.SetValue("DomainName", domainName);
            OurKey.SetValue("InstallDirectory", iisrootdirectory);
            OurKey.SetValue("DatabaseServer", databaseServer);
            OurKey.SetValue("DatabaseName", databaseName);
           // OurKey.SetValue("DatabaseAuthentication", databaseAuthentication);
            OurKey.SetValue("Version", "1.0.25.36");
            ////OurKey.SetValue("ImpersonatedUserName", ImpersonateUserName);
            ////OurKey.SetValue("ImpersonatedPassword", ImpersonatePassword);
            ////OurKey.SetValue("DatabaseUserName", DatabaseUserName);
            ////OurKey.SetValue("DatabasePassword", DatabasePassword);
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
            EncryptOurKey.CreateSubKey(@"TerraScan\TerraScanWCF"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            EncryptOurKey = EncryptOurKey.OpenSubKey(@"TerraScan\TerraScanWCF", true);
            EncryptOurKey.SetValue("ImpersonatedUserName", cipherImpUserName);
            EncryptOurKey.SetValue("ImpersonatedPassword", cipherImpPassword);
            EncryptOurKey.SetValue("DatabaseUserName", cipherDBUserName);
            EncryptOurKey.SetValue("DatabasePassword", cipherDBPassword);
            EncryptOurKey.Close();
            EncryptOurKey = null;

            //////RegistryKey ComponentIDKey = Registry.LocalMachine; // Create OurKey set to HKEY_LOCAL_MACHINE 
            //////ComponentIDKey = ComponentIDKey.OpenSubKey("SOFTWARE", true); // Set it to HKEY_LOCAL_MACHINE\SOFTWARE 
            //////ComponentIDKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            //////ComponentIDKey.CreateSubKey(@"TerraScan\ComponentID"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            //////ComponentIDKey = ComponentIDKey.OpenSubKey(@"TerraScan\ComponentID", true);
            //////ComponentIDKey.SetValue("ComponentID", "1");


            RegistryKey ClrKey = Registry.ClassesRoot; // Create OurKey set to HKEY_LOCAL_MACHINE 
            ClrKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            ClrKey.CreateSubKey(@"TerraScan\TerraScanWCF");
            ClrKey = ClrKey.OpenSubKey(@"TerraScan\TerraScanWCF", true);
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