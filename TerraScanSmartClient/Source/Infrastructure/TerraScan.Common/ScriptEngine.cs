//--------------------------------------------------------------------------------------------
// <copyright file="ScriptEngine.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Login.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		        Description
// ----------		---------		    ---------------------------------------------------------
//                              	     Created
// 05 Dec 07        Karthikeyan V        Form Created for SMO ScriptEngine.
//*********************************************************************************/

namespace TerraScan.Common
{
    using System;
    using System.IO;
    using System.Net;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.SqlServer.Management.Smo.Wmi;
    using System.Security.AccessControl;
    using System.Data.SqlClient;

    /// <summary>
    /// ScriptEngine
    /// </summary>
    public static class ScriptEngine
    {
        #region Variables

        /// <summary>
        /// FieldServerName
        /// </summary>
        // private static string fieldServerName = "KARTHIKEYANV\\SQLEXPRESS";

        /// <summary>
        /// fieldDataBaseName
        /// </summary>
        //  private static string fieldDataBaseName = "TerrascanFieldDB";

        /// <summary>
        /// connectionString
        /// </summary>
        private static string connectionString = "Server = " + Environment.MachineName + "\\TERRASCANFIELD;database=TerrascanFieldDB;uid=sa;pwd=pass@123;Connection Timeout=30;Pooling=false;";

        /// <summary>
        /// connectionStringMaster
        /// </summary>
        private static string connectionStringMaster = "Server = " + Environment.MachineName + "\\TERRASCANFIELD;database=Master;uid=sa;pwd=pass@123;Connection Timeout=30;Pooling=false;";

        /// <summary>
        /// scriptFilePath
        /// </summary>
        private static string scriptFilePath = Environment.CurrentDirectory + @"\DBO.T2FieldScript.sql";

        /// <summary>
        /// mdfFilePath
        /// </summary>
        private static string mdfFilePath = Environment.CurrentDirectory + @"\TerrascanFieldDB_Data.html";

        /// <summary>
        /// ldfFilePath
        /// </summary>
        //  private static string ldfFilePath = Environment.CurrentDirectory + @"\TerrascanFieldDB_log.mdf";

        #endregion

        #region Methods

        /// <summary>
        /// Checks the database.
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsDatabaseAvailable()
        {
            try
            {
                Database dataBaseName = new Database();
                Server serverObject = new Server(TerraScanCommon.FieldServerName);
                dataBaseName = serverObject.Databases[TerraScanCommon.FieldDataBaseName];

                if (dataBaseName != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the server available.
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsServerAvailable()
        {
            try
            {

                ManagedComputer managedComputer = new ManagedComputer();
                //ManagedComputer managedComputers = new ManagedComputer("latha");
                managedComputer.ConnectionSettings.ProviderArchitecture = ProviderArchitecture.Use64bit;
                //ServerInstanceCollection serverInstances = managedComputers.ServerInstances;
                //int count = serverInstances.Count;
                foreach (ServerInstance serverInstance in managedComputer.ServerInstances)
                {
                    if (serverInstance.Name.Equals("TERRASCANFIELD"))
                    {
                        return true;
                    }
                    ////else
                    ////{
                    ////    return false;
                    ////}
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Clears the data base.
        /// </summary>
        public static void DropDataBase()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            ServerConnection serverConnection = new ServerConnection(sqlConnection);
            Server serverObject = new Server(serverConnection);
            try
            {
                serverObject.KillAllProcesses(TerraScanCommon.FieldDataBaseName);
                Database db = serverObject.Databases[TerraScanCommon.FieldDataBaseName];
                if (db != null)
                {
                    db.AutoClose = true;
                    db.Drop();
                }
                SqlConnection.ClearAllPools(); 
            }
            catch (FailedOperationException ex)
            {

            }
            string localFilePathmdf = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TerrascanFieldDB_Data.mdf";
            string localFilePathldf = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TerrascanFieldDB_Data.ldf";

            if (File.Exists(localFilePathmdf))
            {
                File.Delete(localFilePathmdf);
            }
            if (File.Exists(localFilePathldf))
            {
                File.Delete(localFilePathldf);
            }
        }

        /// <summary>
        /// Creates the new field data base.
        /// </summary>
        public static void CreateNewFieldDataBase()
        {
            string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string userName = "NETWORK SERVICE";
            DirectoryInfo dInfo1 = new DirectoryInfo(applicationDataPath);
            DirectorySecurity dSecurity1 = dInfo1.GetAccessControl();
            dSecurity1.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            dInfo1.SetAccessControl(dSecurity1);
            string localFilePath = applicationDataPath + "\\TerrascanFieldDB_Data.bak";
            SqlConnection sqlConnection = new SqlConnection(connectionStringMaster);
            ServerConnection serverConnection = new ServerConnection(sqlConnection);
            Server serverObject = new Server(serverConnection);
            Server svr;
            svr = serverObject;
            Restore rest = new Restore();
            rest.Database = TerraScanCommon.FieldDataBaseName;
            RelocateFile DATAFILE = new RelocateFile();
            RelocateFile LOGFILE = new RelocateFile();
            rest.Action = Microsoft.SqlServer.Management.Smo.RestoreActionType.Database;
            rest.Devices.AddDevice(localFilePath, DeviceType.File);
            string mdf = applicationDataPath + "\\TerrascanFieldDB_Data.mdf";
            DATAFILE.LogicalFileName = rest.ReadFileList(svr).Rows[0][0].ToString();
            DATAFILE.PhysicalFileName = Path.Combine(Path.GetDirectoryName(applicationDataPath + "\\TerrascanFieldDB_Data.mdf"), TerraScanCommon.FieldDataBaseName + Path.GetExtension(mdf));
            string ldf = applicationDataPath + "\\TerrascanFieldDB_Data.ldf";
            LOGFILE.LogicalFileName = rest.ReadFileList(svr).Rows[1][0].ToString();
            LOGFILE.PhysicalFileName = Path.Combine(Path.GetDirectoryName(ldf), TerraScanCommon.FieldDataBaseName + Path.GetExtension(ldf));
            rest.RelocateFiles.Add(DATAFILE);
            rest.RelocateFiles.Add(LOGFILE);
            rest.PercentCompleteNotification = 10;
            rest.ReplaceDatabase = true;
            rest.NoRecovery = false;
            rest.SqlRestore(svr);
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <returns>bool</returns>
        public static bool FileExists()
        {
            if (File.Exists(mdfFilePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Downloads the database file.
        /// </summary>
        public static void DownloadDatabaseFile()
        {
            string localFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TerrascanFieldDB_Data.bak";
            if (File.Exists(localFilePath))
            {
                File.Delete(localFilePath);
            }
            File.Copy(ConfigurationWrapper.ServerMdfFilePath, localFilePath);
        }

        /// <summary>
        /// Datas the file exists.
        /// </summary>
        /// <returns>bool</returns>
        public static bool DataFileExists()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.OpenRead(ConfigurationWrapper.ServerMdfFilePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
