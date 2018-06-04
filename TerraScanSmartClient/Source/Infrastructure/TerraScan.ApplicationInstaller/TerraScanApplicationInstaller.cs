//--------------------------------------------------------------------------------------------
// <copyright file="TerraScanApplicationInstaller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the TerraScan ClickOnce Setup.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// -----------		---------		   ---------------------------------------------------------
// 22 May 2006		DINESH	            Created
// 23 May 2006      DINESH              Added UI Interfaces
// 24 May 2006      DINESH              Added Custom Actions
// 11 JAN 2016      Priyadharshini      Custom Installer Code
//*********************************************************************************/

/// <summary>
/// Partial Class
/// </summary>
namespace TerraScan.ApplicationInstaller
{

    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Diagnostics;
    using System.DirectoryServices;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;
    using System.Web.Services.Description;
    using System.Windows.Forms;
    using System.Xml;
   // using Microsoft.Web.Administration;
    using Microsoft.Win32;
    using Microsoft.Win32.SafeHandles;
    /// <summary>
    /// T2 TerraScan Installer Class
    /// </summary>
    [RunInstaller(true)]
    public class TerraScanApplicationInstaller : Installer
    {
        #region Import
        /// <summary>
        /// Import the DLL.
        /// </summary>
        [DllImport("kernel32.dll",
         EntryPoint = "GetStdHandle",
         CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -12;
        private const int MY_CODE_PAGE = 437;
        #endregion

        #region Initialization

        TerraScan.CipherWrapper.CipherWrapper ciphwrp = new TerraScan.CipherWrapper.CipherWrapper();

        string install = string.Empty;
        string dir = string.Empty;
        string loc = string.Empty;
        string iisrootdirectory = string.Empty;
        string webserviceurl = string.Empty;
        string installurl = string.Empty;
        string mswcfurl = string.Empty;

        public byte[] key;
        public byte[] installlocationiv;
        public byte[] wsurliv;
        public byte[] dirlocationiv;

        public byte[] cipherInstallLocation;
        public byte[] cipherWSUrl;
        public byte[] cipherDirLocation;

        string plaintextinstalllocation = string.Empty;
        string plaintextwsurl = string.Empty;
        string plaintextdirlocation = string.Empty;

        public byte[] plainTextInstallLocation;
        public byte[] plainTextWSUrl;
        public byte[] plainTextDirLocation;
        public static string xmlPath = "";
        public static string CMD = "";
        public static string virDir = "";
        public static string dllVersion="2.177.24";
        #endregion

        #region Application Installer Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TerraScanApplicationInstaller"/> class.
        /// </summary>
        public TerraScanApplicationInstaller()
        {
        }
        #endregion

        #region Install Overide
        /// <summary>
        /// Install Override
        /// </summary>
        /// <param name="stateSaver">An <see cref="T:System.Collections.IDictionary"></see> used to save information needed to perform a commit, rollback, or uninstall operation.</param>
        /// <exception cref="T:System.ArgumentException">The stateSaver parameter is null. </exception>
        /// <exception cref="T:System.Exception">An exception occurred in the <see cref="E:System.Configuration.Install.Installer.BeforeInstall"></see> event handler of one of the installers in the collection.-or- An exception occurred in the <see cref="E:System.Configuration.Install.Installer.AfterInstall"></see> event handler of one of the installers in the collection. </exception>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
            string loc = asmbly.Location;
            string dir = Path.GetDirectoryName(loc);
            base.Install(stateSaver);

            #region Variables
            string dirpath = "";
            #endregion
            try
            {
                #region Context Paremeters
                // IIS Root Directory on Server
                xmlPath = Context.Parameters["filepath"];
                iisrootdirectory = this.Context.Parameters["iisrootdirectory"];
                virDir = Context.Parameters["TARGETVDIR"];
                CMD = Context.Parameters["Test"];
                RegistryKey t2UninstalKey = Registry.LocalMachine;
                t2UninstalKey = t2UninstalKey.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "t2.exe");
                t2UninstalKey.SetValue("", "/" + virDir);
                t2UninstalKey.Close();
                t2UninstalKey = null;

                //For Loading XML Document for Manual setup
                XmlDocument xml = new XmlDocument();
                if (xmlPath == "setup.xml" && CMD == "No")
                {
                    RegistryKey ourKey = Registry.LocalMachine;
                    ourKey = ourKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
                    string Directorylocation = Convert.ToString(ourKey.GetValue(""));
                    ourKey.Close();
                    ourKey = null;
                    xml.Load(Directorylocation + "\\setup.xml"); // suppose that myXmlString contains "<Names>...</Names>"
                }
                //For Loading XML Document for Command Prompt setup
                else
                {
                    AllocConsole();
                    IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
                    SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
                    FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
                    Encoding encoding = System.Text.Encoding.GetEncoding(MY_CODE_PAGE);
                    StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
                    standardOutput.AutoFlush = true;
                    standardOutput.Flush();
                    Console.SetOut(standardOutput);
                    Console.WriteLine("Treasurer Installation Started");
                    Console.WriteLine("Please Wait.....");


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
                // For Loading Tresurer Data
                XmlNodeList xnList = xml.SelectNodes("//Treasurer");

                foreach (XmlNode xn in xnList)
                {
                    // Web Service URL
                    webserviceurl = xn.Attributes["WCFServiceURL"].Value;
                    // MS WCF URL
                    mswcfurl = xn.Attributes["MSWCFServiceURL"].Value;
                    // ClickOnce Application Install URL 
                    install = xn.Attributes["InstalURL"].Value;
                }

                installurl = install + "/" + "TerraScan.UI.Application";

                this.EncryptRegistryDetails(install, webserviceurl, iisrootdirectory);
                #endregion

                #region Path And Directory Information

                XmlDocument xmldoc = new XmlDocument();

                // Adding the XML Declaration
                XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmldoc.AppendChild(xmlnode);

                // Adding the Root Element : WwebserviceURL
                XmlElement xmlelem = xmldoc.CreateElement("", "url", "");
                xmldoc.AppendChild(xmlelem);

                XmlElement wcfxmlelem = xmldoc.CreateElement("", "webserviceurl", "");
                XmlText wcfxmltext = xmldoc.CreateTextNode(webserviceurl);
                wcfxmlelem.AppendChild(wcfxmltext);
                xmlelem.AppendChild(wcfxmlelem);

                XmlElement mswcfxmlelem = xmldoc.CreateElement("", "mswcfserviceurl", "");
                XmlText mswcfxmltext = xmldoc.CreateTextNode(mswcfurl);
                mswcfxmlelem.AppendChild(mswcfxmltext);
                xmlelem.AppendChild(mswcfxmlelem);


                xmldoc.AppendChild(xmlelem);

                try
                {
                    xmldoc.Save(dir + "\\" + "webservice.xml");
                }
                catch
                {
                    throw new InstallException("Unable to create " + dir + "\\" + "webservice.xml file");
                }
                #endregion

                #region Copy Setup to Temporary Location
                // Temporary Directory
                string tempdirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
                tempdirectory = tempdirectory + "\\" + "setup";
                Directory.CreateDirectory(tempdirectory);
                System.Threading.Thread.Sleep(2000);
                File.Copy(iisrootdirectory + "setup.exe", tempdirectory + "\\" + "setup.exe", true);
                File.Copy(iisrootdirectory + "webservice.xml", tempdirectory + "\\" + "webservice.xml", true);

                #endregion

                #region Manifest Updation and Signing
                // Update Deployment Manifest with Installtion URL
                // and re-sign the Deployment Manifest
                this.UpdateDeploymentManifest(installurl, dir, dirpath);


                #endregion

            }
            catch (Exception ex)
            {
                if (xmlPath == "setup.xml" && CMD == "No")
                    throw new InstallException(ex.Message);
                else
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Installation Failed! Retry.....");
                    Console.ReadKey(true);
                    throw new InstallException(ex.Message);
                }

            }

        }
        #endregion

        #region FileCopy tyo IIS
        /// <summary>
        /// Copy Files from target directory to IIS Directory
        /// </summary>
        /// <param name="dir">Target Directort</param>
        /// <param name="iisroot">IIS Root</param>
        public void FileCopy(string dir, string iisroot)
        {
            int dircounter = 1;
            int terradircount = 1;
            DirectoryInfo fromdirect = new DirectoryInfo(dir);
            DirectoryInfo[] fromdirectories = fromdirect.GetDirectories();

            // Copy files from root directory 
            foreach (FileInfo thirdparty1 in fromdirect.GetFiles())
            {
                File.Copy(thirdparty1.FullName, iisroot + "\\" + thirdparty1.Name, true);
            }

            foreach (DirectoryInfo fromdir in fromdirectories)
            {
                dircounter++;
                if (fromdir.Name.Contains("Terr"))
                {
                    DirectoryInfo terrdirect = new DirectoryInfo(fromdir.FullName);
                    DirectoryInfo[] terrdirectories = terrdirect.GetDirectories();

                    foreach (DirectoryInfo terrainfo in terrdirectories)
                    {
                        terradircount++;
                        Directory.CreateDirectory(iisroot + "\\" + fromdir + "\\" + terrainfo);
                        foreach (FileInfo thirdparty1 in terrainfo.GetFiles())
                        {
                            File.Copy(thirdparty1.FullName, iisroot + "\\" + fromdir + "\\" + terrainfo + "\\" + thirdparty1.Name, true);
                        }
                    }

                    Directory.CreateDirectory(iisroot + "\\" + fromdir);
                    foreach (FileInfo thirdparty in fromdir.GetFiles())
                    {
                        File.Copy(thirdparty.FullName, iisroot + "\\" + fromdir + "\\" + thirdparty.Name, true);
                    }
                }
                else
                {
                    Directory.CreateDirectory(iisroot + "\\" + fromdir);
                    foreach (FileInfo thirdparty in fromdir.GetFiles())
                    {
                        File.Copy(thirdparty.FullName, iisroot + "\\" + fromdir + "\\" + thirdparty.Name, true);
                    }
                }
            }
        }

        #endregion

        #region Commit
        /// <summary>
        /// Commit
        /// </summary>
        /// <param name="savedState">savedState</param>
        public override void Commit(System.Collections.IDictionary savedState)
        {
            try
            {
                /// <summary>
                /// For Getting the assembly directory
                /// </summary>
                string Directorylocation = "";
                System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
                loc = asmbly.Location;
                dir = Path.GetDirectoryName(loc);
                string path = dir + "\\" + "setup.exe";
                #region For Custom Action
                /// <summary>
                /// For Assign unsign tool to the setup exe
                /// </summary>
                Process setup = new Process();
                setup.StartInfo.RedirectStandardOutput = true;
                setup.StartInfo.RedirectStandardError = true;
                setup.StartInfo.UseShellExecute = false;
                StringBuilder unsignstring = new StringBuilder("/f /b ");
                unsignstring.Append("\"" + path + "\" ");
                setup.StartInfo.FileName = dir + "\\" + "unsigntool.exe";
                setup.StartInfo.Arguments = unsignstring.ToString();
                setup.Start();
                string stdoutx = setup.StandardOutput.ReadToEnd();
                string stderrx = setup.StandardError.ReadToEnd();
                setup.WaitForExit();
                Console.WriteLine("Exit code : {0}", setup.ExitCode);
                Console.WriteLine("Stdout : {0}", stdoutx);
                Console.WriteLine("Stderr : {0}", stderrx);

                /// <summary>
                /// For Loading the XML document from the registry 
                /// </summary>
                XmlDocument xml = new XmlDocument();

                if (xmlPath == "setup.xml" && CMD == "No")
                {
                    RegistryKey ourKey = Registry.LocalMachine;
                    ourKey = ourKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
                    Directorylocation = Convert.ToString(ourKey.GetValue(""));
                    ourKey.Close();
                    ourKey = null;
                    xml.Load(Directorylocation + "\\setup.xml");
                }// suppose that myXmlString contains "<Names>...</Names>"
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
                    {
                        string tempxmlPath = cmdDirectorylocation + "\\" + xmlPath;
                        xml.Load(tempxmlPath);
                    }
                }
                #endregion

                #region Loading Tresurer
                /// <summary>
                /// For Loading the Tresurer XML
                /// </summary>
                XmlNodeList xnList = xml.SelectNodes("//Treasurer");
                foreach (XmlNode xn in xnList)
                {
                    // Web Service URL
                    webserviceurl = xn.Attributes["WCFServiceURL"].Value;

                    // MS WCF URL
                    mswcfurl = xn.Attributes["MSWCFServiceURL"].Value;

                    // Instal URL
                    install = xn.Attributes["InstalURL"].Value;

                    // ClickOnce Application Install URL 
                    installurl = xn.Attributes["InstalURL"].Value + "/" + "TerraScan.UI.Application";
                }


                bool setupfile = File.Exists(dir + "\\" + "setup.exe");

                Process setupModify = new Process();
                setupModify.StartInfo.RedirectStandardOutput = true;
                setupModify.StartInfo.RedirectStandardError = true;
                setupModify.StartInfo.UseShellExecute = false;

                // signDeploymentManifest.StartInfo.FileName = @"C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\mage.exe";
                setupModify.StartInfo.FileName = dir + "\\" + "setup.exe";
                setupModify.StartInfo.Arguments = "-url=" + install + "  " + "-componentsurl=" + install;
                setupModify.Start();
                StreamReader stdout = setupModify.StandardOutput;
                StreamReader stderr = setupModify.StandardError;
                string s = stdout.ReadToEnd();
                #endregion

                #region To kill the process
                if (!setupModify.HasExited)
                {
                    setupModify.Kill();
                }

                if (xmlPath == "setup.xml" && CMD == "No")
                {

                }
                else
                {
                    Console.WriteLine("Treasurer Installation Completed");
                }
                #endregion

                stdout.Close();
                stderr.Close();
                setupModify.Close();
                setupModify.Dispose();

                base.Commit(savedState);
            }
            catch (Exception ex)
            {
                if (xmlPath == "setup.xml" && CMD == "No")
                    throw new InstallException(ex.Message);
                else
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Installation Failed! Retry.....");
                    Console.ReadKey(true);
                    throw new InstallException(ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the latest version.
        /// </summary>
        /// <returns></returns>
        private string GetLatestVersion()
        {
            System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
            string location = asmbly.Location;
            string directory = Path.GetDirectoryName(location);
            string version = string.Empty;

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(directory + "\\" + "TerraScan.UI.application");

            XmlDocument myxmldocument = new XmlDocument();
            myxmldocument.Load(fileInfo.FullName);

            XmlNode node;
            node = myxmldocument.DocumentElement;

            foreach (XmlNode node1 in node.ChildNodes)
            {
                if (node1.Name == "dependency")
                {
                    foreach (XmlNode node2 in node1.ChildNodes)
                    {
                        if (node2.Name == "dependentAssembly")
                        {
                            foreach (XmlNode node3 in node2.ChildNodes)
                            {
                                if (node3.Name == "assemblyIdentity")
                                {
                                    version = node3.Attributes.GetNamedItem("version").Value;
                                }
                            }
                        }
                    }
                }
            }

            return version;
        }

        #endregion

        #region Uninstall
        /// <summary>
        /// Uninstall
        /// </summary>
        /// <param name="savedState">State</param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {

            /// <summary>
            /// Getting the Assembly location.
            /// </summary>
            System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
            string loc = asmbly.Location;
            string dir = Path.GetDirectoryName(loc);
            string Directorylocation = "";
            string VirFolder = "";
            object physicalpath = "";
            string[] virdir=null;

            #region Getting Registry entries and delete the application in iis
            /// <summary>
            /// Getting the Registry path.
            /// </summary>
            RegistryKey ourKey = Registry.LocalMachine;
            if (ourKey != null)
            {
                ourKey = ourKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "t2.exe", true);
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

            #region To kill the Tresurer Process
            Process application = null;
            foreach (var process in Process.GetProcesses())
            {
                if (!process.ProcessName.ToLower().Contains("Terrascan T2 Treasurer")) continue;
                application = process;
                break;
            }

            if (application != null && application.Responding)
            {
                application.Kill();
                base.Uninstall(savedState);
            }
            bool wsfile = File.Exists(dir + "\\" + "webservice.xml");

            if (wsfile)
            {
                File.Delete(dir + "\\" + "webservice.xml");
            }
            else
            {
                // Do Nothing as file may not have been created
            }
            #endregion

            #region Delete Registry Key

            RegistryKey DeleteKey = Registry.LocalMachine;
            DeleteKey = DeleteKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "t2.exe", true);
            if (DeleteKey != null)
            {
                Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + dllVersion + "t2.exe", true);
                DeleteKey.Close();
                DeleteKey = null;
            }

            //RegistryKey TerrascanKey = Registry.LocalMachine;
            //TerrascanKey = TerrascanKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
            //if (TerrascanKey != null)
            //{
            //    Registry.LocalMachine.DeleteSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe", true);
            //    TerrascanKey.Close();
            //    TerrascanKey = null;
            //}
            #endregion

            base.Uninstall(savedState);


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

            base.Uninstall(savedState);
        }
        #endregion

        #region RollBack Installation
        /// <summary>
        /// Rollback
        /// </summary>
        /// <param name="savedState">savedState</param>
        public override void Rollback(System.Collections.IDictionary savedState)
        {
            System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
            string loc = asmbly.Location;
            string dir = Path.GetDirectoryName(loc);
            bool wsfile = File.Exists(dir + "\\" + "webservice.xml");

            if (wsfile)
            {
                File.Delete(dir + "\\" + "webservice.xml");
            }
            else
            {
                // Do Nothing as file may not have been created
            }

            base.Rollback(savedState);
        }
        #endregion

        #region SignDeploymentManifest
        /// <summary>
        /// SignDeploymentManifest
        /// </summary>
        /// <param name="dir">Directory</param>
        /// <param name="dirpath">Directory path</param>
        /// <returns>Boolean</returns>
        public bool SignDeploymentManifest(string dir, string dirpath)
        {
            try
            {

                Process signDeploymentManifest = new Process();
                signDeploymentManifest.StartInfo.RedirectStandardOutput = true;
                signDeploymentManifest.StartInfo.RedirectStandardError = true;
                signDeploymentManifest.StartInfo.UseShellExecute = false;

                // signDeploymentManifest.StartInfo.FileName = @"C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\mage.exe";
                signDeploymentManifest.StartInfo.FileName = dir + "\\" + "mage.exe";
                // Commented by Dinesh to Fix the Certificate expiry problem on 09 may 2008.
                // Certificate provided ny Client expired on 07 May 2008.
                //signDeploymentManifest.StartInfo.Arguments = "-Sign" + " " + dir + "\\" + "TerraScan.UI.application" + " " + "-CertFile " + dir + "\\" + "TerraT2.pfx" + " " + "-Password" + " " + "nectarine=123pony";
                ////Commented to Update the Certificate 
                // signDeploymentManifest.StartInfo.Arguments = "-Sign" + " " + dir + "\\" + "TerraScan.UI.application" + " " + "-CertFile " + dir + "\\" + "Terra2008.pfx" + " " + "-Password" + " " + "Camaro2Huffy@";
                // Commented by priyadharshini to Fix the Certificate expiry problem on 01 April 2016.
                //signDeploymentManifest.StartInfo.Arguments = "-Sign" + " " + dir + "\\" + "TerraScan.UI.application" + " " + "-CertFile " + dir + "\\" + "T2_2014CodeSigning.pfx" + " " + "-Password" + " " + "CIT1234!";
                // Added  by priyadharshini to Fix the Certificate expiry problem on 01 April 2016.
                //signDeploymentManifest.StartInfo.Arguments = "-Sign" + " " + dir + "\\" + "TerraScan.UI.application" + " " + "-CertFile " + dir + "\\" + "ThomsonReutersT2_2016.pfx" + " " + "-Password" + " " + "GIS=cash$1";
                // Added New Certificate on 26 Mar 2018 By Priyadharshini
                signDeploymentManifest.StartInfo.Arguments = "-Sign" + " " + dir + "\\" + "TerraScan.UI.application" + " " + "-CertFile " + dir + "\\" + "Thomson_Reuters.pfx" + " " + "-Password" + " " + "GIS=cash$1";
                signDeploymentManifest.Start();
                StreamReader stdout = signDeploymentManifest.StandardOutput;
                StreamReader stderr = signDeploymentManifest.StandardError;
                string s = stdout.ReadToEnd();
                if (!signDeploymentManifest.HasExited)
                {
                    signDeploymentManifest.Kill();
                }

                stdout.Close();
                stderr.Close();
                signDeploymentManifest.Close();
                signDeploymentManifest.Dispose();

                return true;
            }
            catch
            {
                throw new InstallException("Not Signed");
            }
        }
        #endregion

        #region UpdateDeploymentManifest
        ///<summary>
        ///UpdateDeploymentManifest
        ///</summary>
        ///<param name="installurl">installtion URL</param>
        ///<param name="dir">Directory</param>
        ///<param name="dirpath">Directory Path</param>
        ///<returns>Boolean</returns>
        public bool UpdateDeploymentManifest(string installurl, string dir, string dirpath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(dir + "\\" + "TerraScan.UI.application");

            XmlDocument myxmldocument = new XmlDocument();
            myxmldocument.Load(fileInfo.FullName);
            Boolean foundit = false;

            XmlNode node;
            node = myxmldocument.DocumentElement;

            foreach (XmlNode node1 in node.ChildNodes)
            {
                foreach (XmlNode node2 in node1.ChildNodes)
                {
                    if (node2.Name == "deploymentProvider")
                    {
                        node2.Attributes.GetNamedItem("codebase").Value = installurl;
                        foundit = true;
                    }
                }
            }

            myxmldocument.Save(fileInfo.FullName);

            StreamReader stread = new StreamReader(fileInfo.FullName);
            string readstr = stread.ReadToEnd();
            stread.Close();

            StreamWriter stwrite = new StreamWriter(fileInfo.FullName);
            stwrite.Write(readstr);

            // System.Windows.Forms.MessageBox.Show(readstr);
            stwrite.Flush();
            stwrite.Close();

            // this.SignApplicationManifest(asmbly, dir, dirpath);
            this.SignDeploymentManifest(dir, dirpath);
            return foundit;
        }

        #endregion


        #region Post Installation
        /// <summary>
        /// Raises the <see cref="E:System.Configuration.Install.Installer.AfterInstall"></see> event.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary"></see> that contains the state of the computer after all the installers contained in the <see cref="P:System.Configuration.Install.Installer.Installers"></see> property have completed their installations.</param>
        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
        }
        #endregion

        #region CodeDOM DLL Creation
        /// <summary>
        /// Test the Web Service URL for Validity
        /// </summary>
        /// <param name="webServiceAsmxUrl">web service url</param>
        /// <param name="serviceName">web service name (asmx)</param>
        /// <param name="methodName">web service method</param>
        /// <param name="args">method args</param>
        /// <returns>object</returns>
        public object Testwsurl(string webServiceAsmxUrl, string serviceName, string methodName, object[] args)
        {
            args = new object[] { "Success" };
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(webServiceAsmxUrl);
            webRequest.Timeout = 300000;
            webRequest.UserAgent = "TerraScan Treasurer";

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(webResponse.GetResponseStream(), enc);

            string response = responseStream.ReadToEnd();

            responseStream.Close();
            webResponse.Close();

            // MessageBox.Show(Response);
            // return webResponse;

            System.Net.WebClient client = new System.Net.WebClient();

            // --Connect To the web service
            System.IO.Stream stream = client.OpenRead(webServiceAsmxUrl + "?wsdl");

            // --Now read the WSDL file describing a // service.
            System.Web.Services.Description.ServiceDescription description = ServiceDescription.Read(stream);

            // /// LOAD THE DOM /////////
            // --Initialize a service description importer.
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap12"; // Use SOAP 1.2.
            importer.AddServiceDescription(description, null, null);

            // --Generate a proxy client. 
            importer.Style = ServiceDescriptionImportStyle.Client;

            // --Generate properties to represent pri // mitive values.
            importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

            // --Initialize a Code-DOM tree into which we will import the service.
            CodeNamespace nmspace = new CodeNamespace();
            CodeCompileUnit unit1 = new CodeCompileUnit();
            unit1.Namespaces.Add(nmspace);

            // --Import the service into the Code-DOM tree. 
            // --This creates proxy code that uses the service.
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);
            if (warning == 0) // --If zero then we are good to go
            {
                // --Generate the proxy code 
                CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

                // --Compile the assembly proxy with the appropriate references
                string[] assemblyReferences = new string[5] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };
                CompilerParameters parms = new CompilerParameters(assemblyReferences);
                CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);

                // -Check For Errors
                if (results.Errors.Count > 0)
                {
                    foreach (CompilerError oops in results.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine("========Compiler error============");
                        System.Diagnostics.Debug.WriteLine(oops.ErrorText);
                    }

                    throw new System.Exception("Compile Error Occured calling webservice. Check Debug ouput window.");
                }

                // --Finally, Invoke the web service method 
                object wsvcClass = results.CompiledAssembly.CreateInstance(serviceName);
                MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                return mi.Invoke(wsvcClass, args);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Encrypt Registry Details

        public void EncryptRegistryDetails(string installlocation, string wsurl, string dirlocation)
        {
            RegistryPermission fwrite = new RegistryPermission(RegistryPermissionAccess.Write, "HKLM\\SOFTWARE\\");
            fwrite.AddPathList(RegistryPermissionAccess.Write, "HKLM\\SOFTWARE\\");

            RegistryKey OurKey = Registry.LocalMachine; // Create OurKey set to HKEY_LOCAL_MACHINE 
            OurKey = OurKey.OpenSubKey("SOFTWARE", true); // Set it to HKEY_LOCAL_MACHINE\SOFTWARE 
            OurKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            OurKey.CreateSubKey(@"TerraScan\TerraScanApplication"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            OurKey = OurKey.OpenSubKey(@"TerraScan\TerraScanApplication", true);
            OurKey.SetValue("InstallLocation", installurl);
            OurKey.SetValue("WebServiceURL", webserviceurl);
            OurKey.SetValue("InstallDirectory", iisrootdirectory);
            OurKey.SetValue("MSWCFURL", mswcfurl);
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


            #region to be used only if necessary
            //key = ciphwrp.Key;
            //installlocationiv = ciphwrp.AsymmIV;
            //wsurliv = ciphwrp.AsymmIV;
            //dirlocationiv = ciphwrp.AsymmIV;

            //bool testciphercreate = this.createCiphers(installlocation, wsurl, dirlocation);

            //if (testciphercreate == true)
            //{
            //    try
            //    {
            //        cipherInstallLocation = ciphwrp.EncryptMessage(plainTextInstallLocation, key, out installlocationiv);
            //        cipherWSUrl = ciphwrp.EncryptMessage(plainTextWSUrl, key, out wsurliv);
            //        cipherDirLocation = ciphwrp.EncryptMessage(plainTextDirLocation, key, out dirlocationiv);
            //    }
            //    catch (System.Security.SecurityException sec)
            //    {
            //        MessageBox.Show(sec.Message);
            //    }
            //}

            //RegistryPermission fwrite = new RegistryPermission(RegistryPermissionAccess.Write, "HKLM\\SOFTWARE\\");
            //fwrite.AddPathList(RegistryPermissionAccess.Write, "HKLM\\SOFTWARE\\");

            //RegistryKey OurKey = Registry.LocalMachine; // Create OurKey set to HKEY_LOCAL_MACHINE 
            //OurKey = OurKey.OpenSubKey("SOFTWARE", true); // Set it to HKEY_LOCAL_MACHINE\SOFTWARE 
            //OurKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            //OurKey.CreateSubKey(@"TerraScan\TerraScanApplication"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            //OurKey = OurKey.OpenSubKey(@"TerraScan\TerraScanApplication", true);
            //OurKey.SetValue("InstallLocation", cipherWSUrl);
            //OurKey.SetValue("WebServiceURL", cipherInstallLocation);
            //OurKey.SetValue("InstallDirectory", cipherDirLocation);
            //OurKey.SetValue("Version", "1.0.25.36");
            //OurKey.Close();
            //OurKey = null;

            //RegistryKey ComponentIDKey = Registry.LocalMachine; // Create OurKey set to HKEY_LOCAL_MACHINE 
            //ComponentIDKey = ComponentIDKey.OpenSubKey("SOFTWARE", true); // Set it to HKEY_LOCAL_MACHINE\SOFTWARE 
            //ComponentIDKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            //ComponentIDKey.CreateSubKey(@"TerraScan\ComponentID"); // Create a sub key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan\TerraScanDatabase 
            //ComponentIDKey = ComponentIDKey.OpenSubKey(@"TerraScan\ComponentID", true);
            //ComponentIDKey.SetValue("ComponentID", "1");


            //RegistryKey ClrKey = Registry.ClassesRoot; // Create OurKey set to HKEY_LOCAL_MACHINE 
            //ClrKey.CreateSubKey("TerraScan"); // Create the key HKEY_LOCAL_MACHINE\SOFTWARE\TerraScan 
            //ClrKey.CreateSubKey(@"TerraScan\TerraScanApplication");
            //ClrKey = ClrKey.OpenSubKey(@"TerraScan\TerraScanApplication", true);
            //ClrKey.SetValue("key", key);
            //ClrKey.SetValue("installlocationiv", installlocationiv);
            //ClrKey.SetValue("wsurliv", wsurliv);
            //ClrKey.SetValue("dirlocationiv", dirlocationiv);
            //ClrKey.Close();
            //ClrKey = null;
            #endregion

        }
        #endregion

        #region Create Ciphers
        /// <summary>
        /// Creates the ciphers.
        /// </summary>
        /// <param name="plaintextdbname">The plaintextdbname.</param>
        /// <param name="plaintextdbservername">The plaintextdbservername.</param>
        /// <param name="plaintextusername">The plaintextusername.</param>
        /// <param name="plaintextpassword">The plaintextpassword.</param>
        /// <returns></returns>
        private bool createCiphers(string plaintextinstalllocation, string plaintextwsurl, string plaintextdirlocation)
        {
            try
            {
                plainTextInstallLocation = Encoding.Unicode.GetBytes(plaintextinstalllocation);
                plainTextWSUrl = Encoding.Unicode.GetBytes(plaintextwsurl);
                plainTextDirLocation = Encoding.Unicode.GetBytes(plaintextdirlocation);
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
