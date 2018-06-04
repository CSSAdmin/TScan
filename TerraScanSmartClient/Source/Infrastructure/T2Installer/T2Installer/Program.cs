//--------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Main Method for T2 installer.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// JAN 11 2016      Priyadharshini      Created
//*********************************************************************************/

namespace T2Installer
{
    #region namespace
    using System;
    using System.Diagnostics;
    using System.DirectoryServices;
    using System.IO;
    using System.Management;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
    using System.Linq;
    //using Microsoft.Web.Administration;
    using Microsoft.Win32;
    using System.Collections;
    using System.Collections.Generic;
   
   

   // using Microsoft.Web.Administration;
    #endregion

    static class Program
    {
        /// <summary>
        /// DLL Added For Kernal32 to allocate the console
        /// </summary>
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        /// <summary>
        /// DLL Added For Kernal32 to Free the console
        /// </summary>
        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        /// <summary>
        /// DLL Added For Kernal32 to Attach the console
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern bool AttachConsole(uint dwProcessId);

        public static long siteId = 0;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            try
            {

                #region Getting the Location
                //// Getting the Location of assembly.            
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                string Assemblylocation = assembly.Location;
                string CurrentRunningDirectory = Path.GetDirectoryName(Assemblylocation);
                #endregion

                #region Intialization
                //// Initializing the variables.                                               

                string upgradeCommand = string.Empty;
                //// For Arguments
                string[] commandPromtArguments = Environment.GetCommandLineArgs();
                //// boolean variable for Made console
                bool madeConsole = false;
                string[] virtualDirectorySplits = new string[100];
                bool wcfValidation = false;
                bool trasurerValidation = false;
                string wcfVirturlDirectory = string.Empty;
                string wcfPhysicalDirectory = string.Empty;
                string t2VirtualDirectory = string.Empty;
                string t2PhysialDirectory = string.Empty;
                string OriginalPhysicalDirectory = string.Empty;
                bool IsStatus;
                string[] tempSplits = new string[20];
                Process process = new Process();
                RegistryKey terrascan32RegistryKey = Registry.LocalMachine;
                terrascan32RegistryKey = terrascan32RegistryKey.CreateSubKey(ConstantVariable.Terrascan32Registry);
                terrascan32RegistryKey.SetValue(string.Empty, CurrentRunningDirectory);
                terrascan32RegistryKey.Close();
                terrascan32RegistryKey = null;

                RegistryKey terrascan64RegistryKey = Registry.LocalMachine;
                terrascan64RegistryKey = terrascan64RegistryKey.CreateSubKey(ConstantVariable.Terrascan64Registry);
                terrascan64RegistryKey.SetValue(string.Empty, CurrentRunningDirectory);
                terrascan64RegistryKey.Close();
                terrascan64RegistryKey = null;
                #endregion

                #region Getting Site Id

                DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");
                foreach (DirectoryEntry de in w3svc.Children)
                {
                    if (de.SchemaClassName == "IIsWebServer" && de.Properties["ServerComment"][0].ToString() == ConstantVariable.DefaultWebSiteName)
                    {
                        siteId = Convert.ToInt64(de.Name);
                    }
                }
                #endregion

                //// Run exe as Command Prompt.                
                if (commandPromtArguments.Length > 1)
                {
                    string[] argCharacters = commandPromtArguments[1].Split('=');
                    if (commandPromtArguments.Length > 2)
                    {
                        upgradeCommand = commandPromtArguments[2];
                    }
                    try
                    {
                        //// Attaching Console.                        
                        if (AttachToConsole())
                        {
                            AllocConsole();
                            madeConsole = true;
                        }

                        //// Check Upgrade Command Exists.                   
                        if (upgradeCommand.Contains("u"))
                        {
                            upgradeFunction();
                        }

                        //// Validation For installing EXE.
                        wcfValidation = IsApplictionInstalled(ConstantVariable.SetupName);
                        trasurerValidation = IsApplictionInstalled(ConstantVariable.T2SetupName);
                        if (wcfValidation || trasurerValidation)
                        {
                            if (wcfValidation && !trasurerValidation)
                            {
                                Console.WriteLine(ConstantVariable.WCFExists);
                            }
                            else if (trasurerValidation && !wcfValidation)
                            {
                                Console.WriteLine(ConstantVariable.T2Exists);
                            }
                            else if (trasurerValidation && wcfValidation)
                            {
                                Console.WriteLine(ConstantVariable.T2AndWCFExists);
                            }
                        }
                        else
                        {
                            //// Read data from XML.
                            XmlDocument xmlDocument = new XmlDocument();
                            xmlDocument.Load(argCharacters[1]);
                            XmlNodeList xnList = xmlDocument.SelectNodes("//WCF");
                            foreach (XmlNode xn in xnList)
                            {
                                wcfVirturlDirectory = xn.Attributes["TARGETVDIR"].Value;
                                wcfPhysicalDirectory = xn.Attributes["PhysicalDir"].Value;
                            }

                            XmlNodeList T2xnList = xmlDocument.SelectNodes("//Treasurer");
                            foreach (XmlNode xn in T2xnList)
                            {
                                t2VirtualDirectory = xn.Attributes["TARGETVDIR"].Value;
                                t2PhysialDirectory = xn.Attributes["PhysicalDir"].Value;
                            }


                           wcfPhysicalDirectory= wcfPhysicalDirectory.TrimEnd('\\');
                           t2PhysialDirectory= t2PhysialDirectory.TrimEnd('\\');

                            virtualDirectorySplits = wcfVirturlDirectory.Split('/');
                            tempSplits = wcfPhysicalDirectory.Split('\\');
                            string virtualFolder = "";
                            string[] virtialDirectoryExists = wcfVirturlDirectory.Split('/');
                            if (virtialDirectoryExists.Length > 0)
                            {
                                virtualFolder = virtialDirectoryExists[0];
                            }

                            //Modified for issue 21780 for WCF Validation and implemented

                            string tempcfVirtualDirectory=string.Empty;
                            string tempwcfPhysicalDirectory = string.Empty;
                            if (wcfVirturlDirectory.Contains("/"))
                            {
                                tempcfVirtualDirectory = wcfVirturlDirectory.Replace("/", "\\");
                            }
                            else
                            {
                                tempcfVirtualDirectory = wcfVirturlDirectory;
                            }

                            string tempt2VirtualDirectory = string.Empty;
                            string tempt2PhysicalDirectory = string.Empty;
                            if (t2VirtualDirectory.Contains("/"))
                            {
                                tempt2VirtualDirectory = t2VirtualDirectory.Replace("/", "\\");
                            }
                            else
                            {
                                tempt2VirtualDirectory = t2VirtualDirectory;
                            }

                            //if (wcfPhysicalDirectory.ToLower().Contains("\\" + tempcfVirtualDirectory.ToLower()) && t2PhysialDirectory.ToLower().Contains("\\" + tempt2VirtualDirectory.ToLower()))
                            //{
                            //    IsStatus = true;
                            //    tempwcfPhysicalDirectory = ReplaceString(wcfPhysicalDirectory, "\\" + tempcfVirtualDirectory, "", true);
                            //    //tempwcfPhysicalDirectory = wcfPhysicalDirectory.Replace("\\" + tempcfVirtualDirectory, "");
                            //    tempt2PhysicalDirectory =ReplaceString(t2PhysialDirectory, "\\" + tempt2VirtualDirectory, "", true);
                            //    //tempt2PhysicalDirectory = t2PhysialDirectory.Replace("\\" + tempt2VirtualDirectory, "");
                            //    if (wcfPhysicalDirectory.ToLower().Contains(tempwcfPhysicalDirectory.ToLower()) && t2PhysialDirectory.ToLower().Contains(tempt2PhysicalDirectory.ToLower()))
                            //    {
                            //        IsStatus = true;
                            //        wcfPhysicalDirectory = tempwcfPhysicalDirectory;
                            //        t2PhysialDirectory = tempt2PhysicalDirectory;
                                   

                            //    }
                            //    else
                            //    {
                            //        IsStatus = false;
                                   
                            //    }
                            //}
                            //else
                            //{
                            //    IsStatus = false;
                                
                            //}

                            //Modified for issue 21780 for WCF Validation
                           
                            //if (t2PhysialDirectory.Contains("\\" + tempt2VirtualDirectory))
                            //{
                            //    IsStatus = true;
                            //    tempt2PhysicalDirectory = t2PhysialDirectory.Replace("\\" + tempt2VirtualDirectory, "");
                            //    if (t2PhysialDirectory.Contains(tempt2PhysicalDirectory))
                            //    {
                            //        IsStatus = true;
                            //        t2PhysialDirectory = tempt2PhysicalDirectory;
                                  
                            //    }
                            //    else
                            //    {
                            //        IsStatus = false;
                                   
                            //    }
                                
                            //}
                            //else
                            //{
                            //    IsStatus = false;
                                
                            //}


                            if (tempSplits.Length > 1)
                            {
                                OriginalPhysicalDirectory = string.IsNullOrEmpty(tempSplits[1]) ? wcfPhysicalDirectory + virtualDirectorySplits[0] : wcfPhysicalDirectory + "\\" + virtualDirectorySplits[0];
                            }
                            
                            //created web site
                            //validation

                            if (!string.IsNullOrEmpty(wcfPhysicalDirectory) && string.IsNullOrEmpty(t2PhysialDirectory) || !string.IsNullOrEmpty(wcfVirturlDirectory) && string.IsNullOrEmpty(t2VirtualDirectory))
                            {
                                Console.WriteLine(ConstantVariable.InstallationFailed);
                                Console.WriteLine(ConstantVariable.MismatchPhysicalOrVirtualDir);
                                System.Windows.Forms.Application.Exit();
                            }
                            else if (string.IsNullOrEmpty(wcfPhysicalDirectory) && !string.IsNullOrEmpty(t2PhysialDirectory) || string.IsNullOrEmpty(wcfVirturlDirectory) && !string.IsNullOrEmpty(t2VirtualDirectory))
                            {
                                Console.WriteLine(ConstantVariable.InstallationFailed);
                                Console.WriteLine(ConstantVariable.MismatchPhysicalOrVirtualDir);
                                System.Windows.Forms.Application.Exit();
                            }
                            else if (!string.IsNullOrEmpty(wcfPhysicalDirectory) && !string.IsNullOrEmpty(t2PhysialDirectory) && string.IsNullOrEmpty(wcfVirturlDirectory) && string.IsNullOrEmpty(t2VirtualDirectory))
                            {
                                Console.WriteLine(ConstantVariable.InstallationFailed);
                                Console.WriteLine(ConstantVariable.AddVirtualDir);
                                System.Windows.Forms.Application.Exit();
                            }
                            //else if (!IsStatus && (!string.IsNullOrEmpty(wcfPhysicalDirectory)))
                            //{
                            //    Console.WriteLine(ConstantVariable.InstallationFailed);
                            //    Console.WriteLine(ConstantVariable.MismatchPhysicalOrVirtualDir);
                            //    System.Windows.Forms.Application.Exit();
                            //}
                            //else if (!IsStatus && (!string.IsNullOrEmpty(t2PhysialDirectory)))
                            //{
                            //    Console.WriteLine(ConstantVariable.InstallationFailed);
                            //    Console.WriteLine(ConstantVariable.MismatchPhysicalOrVirtualDir);
                            //    System.Windows.Forms.Application.Exit();
                            //}
                            else if (!string.IsNullOrEmpty(wcfPhysicalDirectory) && !string.IsNullOrEmpty(t2PhysialDirectory) && !string.IsNullOrEmpty(wcfVirturlDirectory) && !string.IsNullOrEmpty(t2VirtualDirectory))
                            {
                                if (virtialDirectoryExists.Length > 1 && !upgradeCommand.Contains("u"))
                                {
                                    try
                                    {
                                        string virtualDirectoryPath = "IIS://localhost/W3SVC/" + siteId + "/ROOT/" + virtualFolder;
                                        DirectoryEntry virtualDirectory = new DirectoryEntry(virtualDirectoryPath);
                                        string folderPath = virtualDirectory.Properties["Path"].Value.ToString();
                                        string[] VirtualDirectories = Directory.GetDirectories(folderPath);
                                        if (VirtualDirectories.Length > 1)
                                        {
                                            Console.WriteLine(ConstantVariable.TargetExists);
                                            System.Environment.Exit(0);
                                        }
                                        //ServerManager serverManager = new ServerManager();
                                        //serverManager.CommitChanges();
                                        //long siteid = serverManager.Sites[ConstantVariable.DefaultWebSiteName].Id;
                                        //string virtualDirectoryPath = "IIS://localhost/W3SVC/" + siteid + "/ROOT/" + virtualFolder;
                                        //DirectoryEntry virtualDirectory = new DirectoryEntry(virtualDirectoryPath);
                                        //string folderPath = virtualDirectory.Properties["Path"].Value.ToString();
                                        //string[] VirtualDirectories = Directory.GetDirectories(folderPath);
                                        //if (VirtualDirectories.Length > 1)
                                        //{
                                        //    Console.WriteLine(ConstantVariable.TargetExists);
                                        //    System.Environment.Exit(0);
                                        //}

                                    }
                                    catch (COMException)
                                    {
                                        //do Nothing . file path does not exists.
                                    }
                                    catch (Exception error)
                                    {
                                        ExceptionManager.LogException(error.Message);

                                    }
                                }
                                if (!string.IsNullOrEmpty(wcfVirturlDirectory))
                                {
                                    // Delete Virtual Directory.
                                    DeleteVirtualDirectory(wcfVirturlDirectory, wcfPhysicalDirectory, virtualDirectorySplits);
                                }

                                //CreateWebSite(virtualDirectorySplits, OriginalPhysicalDirectory);
                                CreateWebSite(virtualDirectorySplits, wcfPhysicalDirectory);

                                /// Calling WCF exe.                                
                                process.StartInfo.FileName = CurrentRunningDirectory + ConstantVariable.WCFExeName;
                                process.StartInfo.Arguments = "/q iisrootdirectory=" + "\"" + OriginalPhysicalDirectory + "\" TARGETVDIR=" + "\"" + wcfVirturlDirectory + "\" filepath=" + "\"" + argCharacters[1] + "\"";
                                process.Start();

                            }
                            else
                            {
                                try
                                {

                                    if (virtialDirectoryExists.Length > 1 && !upgradeCommand.Contains("u"))
                                    {
                                        string virtualDirectoryPath = "IIS://localhost/W3SVC/" + siteId + "/ROOT/" + virtualFolder;
                                        DirectoryEntry virtualDirectory = new DirectoryEntry(virtualDirectoryPath);
                                        string folderPath = virtualDirectory.Properties["Path"].Value.ToString();
                                        string[] VirtualDirectories = Directory.GetDirectories(folderPath);
                                        if (VirtualDirectories.Length > 1)
                                        {
                                            Console.WriteLine(ConstantVariable.TargetExists);
                                            System.Environment.Exit(0);
                                        }
                                    }
                                }
                                catch (COMException)
                                {
                                    //do Nothing . file path does not exists.
                                }
                                catch (Exception error)
                                {
                                    ExceptionManager.LogException(error.Message);

                                }
                                if (!string.IsNullOrEmpty(wcfVirturlDirectory))
                                {
                                    DeleteVirtualDirectory(wcfVirturlDirectory, wcfPhysicalDirectory, virtualDirectorySplits);
                                }

                                process.StartInfo.FileName = CurrentRunningDirectory + ConstantVariable.WCFExeName;
                               // process.StartInfo.Arguments = "/q iisrootdirectory=" + "\"" + OriginalPhysicalDirectory + "\" TARGETVDIR=" + "\"" + wcfVirturlDirectory + "\" filepath=" + "\"" + argCharacters[1] + "\"";
                                process.StartInfo.Arguments = "/q iisrootdirectory=" + "\"" + wcfPhysicalDirectory + "\" TARGETVDIR=" + "\"" + wcfVirturlDirectory + "\" filepath=" + "\"" + argCharacters[1] + "\"";
                                process.Start();

                            }
                        }
                    }
                    catch (Exception errorHandling)
                    {
                        Console.WriteLine(errorHandling.Message);
                    }
                    finally
                    {
                        if (madeConsole)
                        {
                            FreeConsole();
                        }
                    }
                }
                else
                {
                    //// Running application on manual 
                    System.Windows.Forms.Application.EnableVisualStyles();
                    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                    System.Windows.Forms.Application.Run(new WelcomeSetup());
                }

                System.Windows.Forms.Application.Exit();
            }
            catch (Exception error)
            {
                ExceptionManager.LogException(error.Message);
            }
        }

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


        /// <summary>
        /// DeleteVirtualDir.
        /// </summary>
        public static void DeleteVirtualDirectory(string wcfVirtualDirectory,
                                            string wcfPhysicalDirectory,
                                            string[] virtualDirectorySplits)
        {


            try
            {
                    string virtualDirectoryPath = "IIS://localhost/W3SVC/" + siteId + "/ROOT/" + virtualDirectorySplits[0];
                    DirectoryEntry virtualDirectory = new DirectoryEntry(virtualDirectoryPath);
                    if (virtualDirectory != null)
                    {
                        virtualDirectory.DeleteTree();
                    }
               
            }
            catch (COMException)
            {
                //// Do Nothing. Virtual directory does not exist.
            }
            catch (Exception error)
            {
                ExceptionManager.LogException(error.Message);
            }
        }

        /// <summary>
        /// Create Website.
        /// </summary>
        /// 
        public static void CreateWebSite(string[] virtualDirectorySplits, string OriginalPhysicalDirectory)
        {
                CreateVDir("IIS://Localhost/W3SVC/"+ siteId +"/Root",virtualDirectorySplits[0], OriginalPhysicalDirectory);
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

        /// <summary>
        /// For Upgrading the Exe.
        /// </summary>
        /// 
        #region Upgrade Functionality

        private static void upgradeFunction()
        {
            //ManagementObjectSearcher wcf = new ManagementObjectSearcher(
            //"SELECT * FROM Win32_Product WHERE Name = '" + programName + "'");
            //ManagementObjectSearcher Tresuerer = new ManagementObjectSearcher(
            //"SELECT * FROM Win32_Product WHERE Name = '" + treasurerName + "'");
            string programName = ConstantVariable.SetupName;

            #region wcf uninstall

            string applicationDisplayName;
            string applicationDisplayVersion;
            string Uninstallstring;
            RegistryKey key;
            // search in: LocalMachine_32
            if (IntPtr.Size == 4)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    applicationDisplayName = subkey.GetValue("DisplayName") as string;
                    applicationDisplayVersion = subkey.GetValue("DisplayVersion") as string;
                    Uninstallstring = subkey.GetValue("UninstallString") as string;

                    if (programName.Equals(applicationDisplayName, StringComparison.OrdinalIgnoreCase) == true )
                    {
                        string extractString = ExtractBetween(Uninstallstring, "{", "}");
                        string unistallExtractString = "/x {" + extractString + "} /quiet";

                        Process process = new Process();
                        process.StartInfo.FileName = "msiexec.exe";
                        process.StartInfo.Arguments = unistallExtractString;
                        process.Start();
                        process.WaitForExit();
                    }
                }
            }

            // search in: LocalMachine_64
            if (IntPtr.Size == 8)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    applicationDisplayName = subkey.GetValue("DisplayName") as string;
                    applicationDisplayVersion = subkey.GetValue("DisplayVersion") as string;
                    Uninstallstring = subkey.GetValue("UninstallString") as string;
                    if (programName.Equals(applicationDisplayName, StringComparison.OrdinalIgnoreCase) == true )
                    {
                            string extractString = ExtractBetween(Uninstallstring, "{", "}");
                            string unistallExtractString = "/x {" + extractString + "} /quiet";
                            Process process = new Process();
                            process.StartInfo.FileName = "msiexec.exe";
                            process.StartInfo.Arguments = unistallExtractString;
                            process.Start();
                            process.WaitForExit();
                        
                    }
                }
            }
            #endregion
            string treasurerName = ConstantVariable.T2SetupName;

            #region treasurer uninstall

            // search in: LocalMachine_32
            if (IntPtr.Size == 4)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    applicationDisplayName = subkey.GetValue("DisplayName") as string;
                    applicationDisplayVersion = subkey.GetValue("DisplayVersion") as string;
                    Uninstallstring = subkey.GetValue("UninstallString") as string;

                    if (treasurerName.Equals(applicationDisplayName, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        string extractString = ExtractBetween(Uninstallstring, "{", "}");
                        string unistallExtractString = "/x {" + extractString + "} /quiet";

                        Process process = new Process();
                        process.StartInfo.FileName = "msiexec.exe";
                        process.StartInfo.Arguments = unistallExtractString;
                        process.Start();
                        process.WaitForExit();
                    }
                }
            }

            // search in: LocalMachine_64
            if (IntPtr.Size == 8)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    applicationDisplayName = subkey.GetValue("DisplayName") as string;
                    applicationDisplayVersion = subkey.GetValue("DisplayVersion") as string;
                    Uninstallstring = subkey.GetValue("UninstallString") as string;
                    if (treasurerName.Equals(applicationDisplayName, StringComparison.OrdinalIgnoreCase) == true)
                    {
                        string extractString = ExtractBetween(Uninstallstring, "{", "}");
                        string unistallExtractString = "/x {" + extractString + "} /quiet";
                        Process process = new Process();
                        process.StartInfo.FileName = "msiexec.exe";
                        process.StartInfo.Arguments = unistallExtractString;
                        process.Start();
                        process.WaitForExit();

                    }
                }
            }
            #endregion

            #region code comment

            //return false;


            //foreach (ManagementObject wcfObj in wcf.Get())
            //{
            //    try
            //    {
            //        if (wcfObj["Name"].ToString() == ConstantVariable.SetupName)
            //        {
            //            object wcfInvokeMethod = wcfObj.InvokeMethod("Uninstall", null);
            //        }
            //    }
            //    catch (Exception error)
            //    {
            //        //this program may not have a name property, so an exception will be thrown
            //        ExceptionManager.LogException(error.Message);
            //    }
            //}

            //foreach (ManagementObject tresuerer in Tresuerer.Get())
            //{
            //    try
            //    {
            //        if (tresuerer["Name"].ToString() == ConstantVariable.T2SetupName)
            //        {
            //            object tresuererInvokeMethod = tresuerer.InvokeMethod("Uninstall", null);
            //        }
            //    }
            //    catch (Exception error)
            //    {
            //        //this program may not have a name property, so an exception will be thrown
            //        ExceptionManager.LogException(error.Message);
            //    }
            //}
            #endregion
        }
        #endregion


        // Returns the text between 'start' and 'end'.
        public static string ExtractBetween(string text, string start, string end)
        {
            int iStart = text.IndexOf(start);
            iStart = (iStart == -1) ? 0 : iStart + start.Length;
            int iEnd = text.LastIndexOf(end);
            if (iEnd == -1)
            {
                iEnd = text.Length;
            }
            int len = iEnd - iStart;

            return text.Substring(iStart, len);
        }
        /// <summary>
        /// For Attaching the Console to application.
        /// </summary>
        ///
        #region AttachToConsole
        private static bool AttachToConsole()
        {
            try
            {
                const uint ParentProcess = 0xFFFFFFFF;
                if (!AttachConsole(ParentProcess))
                {
                    return false;
                }

                Console.Clear();
                Console.WriteLine(ConstantVariable.InstallationName);
                return true;
            }
            catch (Exception error)
            {
                ExceptionManager.LogException(error.Message);
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Check the Software installed in the machine or not
        /// </summary>
        /// 
        #region Check Application Installed or not
        private static bool IsApplictionInstalled(string processname)
        {
            try
            {
                string applicationDisplayName;
                string applicationDisplayVersion;
                RegistryKey key;
                // search in: LocalMachine_32
                if (IntPtr.Size == 4)
                {
                    key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        applicationDisplayName = subkey.GetValue("DisplayName") as string;
                        applicationDisplayVersion = subkey.GetValue("DisplayVersion") as string;
                        if (processname.Equals(applicationDisplayName, StringComparison.OrdinalIgnoreCase) == true && applicationDisplayVersion == ConstantVariable.VersionNumber)
                        {
                            return true;
                        }
                    }
                }

                // search in: LocalMachine_64
                if (IntPtr.Size == 8)
                {
                    key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        applicationDisplayName = subkey.GetValue("DisplayName") as string;
                        applicationDisplayVersion = subkey.GetValue("DisplayVersion") as string;
                        if (processname.Equals(applicationDisplayName, StringComparison.OrdinalIgnoreCase) == true && applicationDisplayVersion == ConstantVariable.VersionNumber)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception error)
            {
                ExceptionManager.LogException(error.Message);
                return false;
            }
        }

        #endregion
    }
}
