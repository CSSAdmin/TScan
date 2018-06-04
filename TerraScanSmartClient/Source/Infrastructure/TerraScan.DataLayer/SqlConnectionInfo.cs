

//---------------------------------------------------------------------
// <copyright file="SqlConnectionInfo.cs" company="Congruent">
//     Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>Sql connection builder utils</summary>
//---------------------------------------------------------------------
/***********************************************************************************
** Description:	SqlConnectionInfo 
**
** Author:		
** Date:		
************************************************************************************
** Change History
************************************************************************************
** Date			Author     Description
** ----------	---------  ----------------------------------------------------------
** 20 Oct 2005		R.Thilak Raj	New SqlConnectionInfo Class File
***********************************************************************************/

namespace TerraScan.DataLayer
{
    using System;
    using System.Text;

    /// <summary>
    /// This in an object-oriented class representing all the properties that are 
    /// in a SqlServer connection string.  The class is currently subject to 
    /// user-string injection...
    /// </summary>
    [Serializable()]
    public class SqlConnectionInfo
    {
        /// <summary>
        /// The connectionstring information.
        /// </summary>
        private string connectionString;

        /// <summary>
        /// The name of the server where database resides.
        /// </summary>
        private string server;

        /// <summary>
        /// The name of the database
        /// </summary>
        private string database;

        /// <summary>
        /// The userid used to connect to database.
        /// </summary>
        private string userId;

        /// <summary>
        /// The password used to connect to database.
        /// </summary>
        private string password;

        /// <summary>
        /// Flag to specify trusted connection
        /// </summary>
        private bool trustedConnection;

        /// <summary>
        /// Flag to specify wether the connection is trusted connection
        /// </summary>
        private bool ttrustedConnectionEnabled = false;

        /// <summary>
        /// The name of the application.
        /// </summary>
        private string applicationName;

        /// <summary>
        /// The name of the db file to be attached.
        /// </summary>
        private string attachDBFilename;

        /// <summary>
        /// The time duration after which the connection gets timedout.
        /// </summary>
        private int connectionTimeout = -1;

        /// <summary>
        /// The time duaration after which the connection's life goes out.
        /// </summary>
        private int connectionLifetime = -1;

        /// <summary>
        /// Flag to reset the connection.
        /// </summary>
        private bool connectionReset;

        /// <summary>
        /// Flag to identify connection being reseted.
        /// </summary>
        private bool connectionResetEnabled = false;

        /// <summary>
        /// The current language.
        /// </summary>
        private string currentLanguage;

        /// <summary>
        /// flag to enlist a transaction as distributed transaction
        /// </summary>
        private bool enlist;

        /// <summary>
        /// Flag to set wether the transaction is enlisted as distributed transaction
        /// </summary>
        private bool enlistEnabled = false;

        /// <summary>
        /// The maximimun pool size of connection pool.
        /// </summary>
        private int maxPoolSize = -1;

        /// <summary>
        /// The minimum pool size of connection pool.
        /// </summary>
        private int minPoolSize = -1;

        /// <summary>
        /// networklibrary to be used by connection.
        /// </summary>
        private string networkLibrary;

        /// <summary>
        /// The size of packet to be transfered through the connection.
        /// </summary>
        private int packetSize = -1;

        /// <summary>
        /// The information to be persisitant with the connection
        /// </summary>
        private string persistSecurityInfo;

        /// <summary>
        /// Flag specifying the pooling of connection or not.
        /// </summary>
        private bool pooling;

        /// <summary>
        /// Flag specifying wether pooling set for the connection.
        /// </summary>
        private bool poolingEnabled = false;

        /// <summary>
        /// The workstationid in which the database resides.
        /// </summary>
        private string workstationId;

        #region constructors
        /// <summary>
        /// Create an empty SqlConnectionInfo.
        /// </summary>
        public SqlConnectionInfo()
        {
        }

        /// <summary>
        /// Create a SqlConnectionInfo with this connection string.
        /// </summary>
        /// <param name="connectionString">connectionString to parse</param>
        public SqlConnectionInfo(string connectionString)
        {
            // we could parse this into the properties for connection string validation...
            this.connectionString = connectionString;
        }

        #endregion

        #region Properties
        /// <summary>
        /// The name or network address of the instance of SQL Server to which to connect.
        /// </summary>
        /// <value>DB Server Name</value>
        public string Server
        {
            get
            {
                return this.server;
            }

            set
            {
                this.server = value;
            }
        }

        /// <summary>
        /// The name of the database.
        /// </summary>
        /// <value>DB name</value>
        public string Database
        {
            get
            {
                return this.database;
            }

            set
            {
                this.database = value;
            }
        }

        /// <summary>
        /// The SQL Server login account.
        /// </summary>
        /// <value>Login name</value>
        public string UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                this.userId = value;
            }
        }

        /// <summary>
        /// The password for the SQL Server account logging on.
        /// </summary>
        /// <value>Password (clear text)</value>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
            }
        }

        /// <summary>
        /// Whether the connection is to be a secure connection or not. Recognized values 
        /// are 'true', 'false', and 'sspi', which is equivalent to 'true'.
        /// </summary>
        /// <value>True/False</value>
        public bool TrustedConnection
        {
            get
            {
                return this.trustedConnection;
            }

            set
            {
                this.trustedConnection = value;
                this.ttrustedConnectionEnabled = true;
            }
        }

        /// <summary>
        /// The name of the application, or '.Net SqlClient Data Provider' if no application name is provided.
        /// </summary>
        /// <value>App Name</value>
        public string ApplicationName
        {
            get
            {
                return this.applicationName;
            }

            set
            {
                this.applicationName = value;
            }
        }

        /// <summary>
        /// The name of the primary file, including the full path name, of an attachable database. The database 
        /// name must be specified with the keyword 'database'.
        /// </summary>
        /// <value>Attachable DB file name</value>
        public string AttachDBFilename
        {
            get
            {
                return this.attachDBFilename;
            }

            set
            {
                this.attachDBFilename = value;
            }
        }

        /// <summary>
        /// The length of time (in seconds) to wait for a connection to the server before terminating the 
        /// attempt and generating an error.
        /// </summary>
        /// <value>Connection Timeout Value</value>
        public int ConnectionTimeout
        {
            get
            {
                return this.connectionTimeout;
            }

            set
            {
                this.connectionTimeout = value;
            }
        }

        /// <summary>
        /// When a connection is returned to the pool, its creation time is compared with the current time, and 
        /// the connection is destroyed if that time span (in seconds) exceeds the value specified by connection 
        /// lifetime. Useful in clustered configurations to force load balancing between a running server and a 
        /// server just brought on-line.
        /// </summary>
        /// <value>Connection Life time</value>
        public int ConnectionLifetime
        {
            get
            {
                return this.connectionLifetime;
            }

            set
            {
                this.connectionLifetime = value;
            }
        }

        /// <summary>
        /// Determines whether the database connection is reset when being removed from the pool. Setting to 'false' 
        /// avoids making an additional server round-trip when obtaining a connection, but the programmer must be aware 
        /// that the connection state is not being reset.
        /// </summary>
        /// <value>True/False</value>
        public bool ConnectionReset
        {
            get
            {
                return this.connectionReset;
            }

            set
            {
                this.connectionReset = value;
                this.connectionResetEnabled = true;
            }
        }

        /// <summary>
        /// The SQL Server Language record name.
        /// </summary>
        /// <value>String</value>
        public string CurrentLanguage
        {
            get
            {
                return this.currentLanguage;
            }

            set
            {
                this.currentLanguage = value;
            }
        }

        /// <summary>
        /// When true, the pooler automatically enlists the connection in the creation thread's current 
        /// transaction context.
        /// </summary>
        /// <value>True/False</value>
        public bool Enlist
        {
            get
            {
                return this.enlist;
            }

            set
            {
                this.enlist = value;
                this.enlistEnabled = true;
            }
        }

        /// <summary>
        /// The maximum number of connections allowed in the pool.
        /// </summary>
        /// <value>Maximum pool size</value>
        public int MaxPoolSize
        {
            get
            {
                return this.maxPoolSize;
            }

            set
            {
                this.maxPoolSize = value;
            }
        }

        /// <summary>
        /// The minimum number of connections allowed in the pool.
        /// </summary>
        /// <value>Minimum pool size</value>
        public int MinPoolSize
        {
            get
            {
                return this.minPoolSize;
            }

            set
            {
                this.minPoolSize = value;
            }
        }

        /// <summary>
        /// The network library used to establish a connection to an instance of SQL Server. Supported values 
        /// include dbnmpntw (Named Pipes), dbmsrpcn (Multiprotocol), dbmsadsn (Apple Talk), dbmsgnet (VIA), 
        /// dbmsipcn (Shared Memory) and dbmsspxn (IPX/SPX), and dbmssocn (TCP/IP). The corresponding network DLL 
        /// must be installed on the system to which you connect. If you do not specify a network and you use a local 
        /// server (for example, "." or "(local)"), shared memory is used.
        /// </summary>
        /// <value>Network library name</value>
        public string NetworkLibrary
        {
            get
            {
                return this.networkLibrary;
            }

            set
            {
                this.networkLibrary = value;
            }
        }

        /// <summary>
        /// Size in bytes of the network packets used to communicate with an instance of SQL Server.
        /// </summary>
        /// <value>Network Packet size</value>
        public int PacketSize
        {
            get
            {
                return this.packetSize;
            }

            set
            {
                this.packetSize = value;
            }
        }

        /// <summary>
        /// When set to 'false', security-sensitive information, such as the password, is not returned as part of 
        /// the connection if the connection is open or has ever been in an open State. Resetting the connection 
        /// string resets all connection string values including the password.
        /// </summary>
        /// <value>True/False</value>
        public string PersistSecurityInfo
        {
            get
            {
                return this.persistSecurityInfo;
            }

            set
            {
                this.persistSecurityInfo = value;
            }
        }

        /// <summary>
        /// When true, the SQLConnection object is drawn from the appropriate pool, or if necessary, is created
        /// and added to the appropriate pool.
        /// </summary>
        /// <value>True/False</value>
        public bool Pooling
        {
            get
            {
                return this.pooling;
            }

            set
            {
                this.pooling = value;
                this.poolingEnabled = true;
            }
        }

        /// <summary>
        /// The name of the workstation connecting to SQL Server.
        /// </summary>
        /// <value>server workstation ID</value>
        public string WorkstationId
        {
            get
            {
                return this.workstationId;
            }

            set
            {
                this.workstationId = value;
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// A string representation of this.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            // use the passed in string of available
            if (this.connectionString != null)
            {
                return this.connectionString;
            }
            else
            {
                return this.BuildConnectionString();
            }
        }

        /// <summary>
        /// Returns true if the resulting connection strings are the same.
        /// </summary>
        /// <param name="obj">object to compare to</param>
        /// <returns>True/False</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (this == obj)
            {
                return true;
            }
            else
            {
                SqlConnectionInfo info = obj as SqlConnectionInfo;
                if (info == null)
                {
                    return false;
                }
                else
                {
                    return this.ToString().Equals(info.ToString());
                }
            }
        }

        /// <summary>
        /// base's has code
        /// </summary>
        /// <returns>Integer hash code</returns>
        public override int GetHashCode()
        {
            // make the compiler happy
            return base.GetHashCode();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Builds connection string
        /// </summary>
        /// <returns> Constructed connection string</returns>
        private string BuildConnectionString()
        {
            StringBuilder builder = new StringBuilder();

            // Server
            if (this.Server != null)
            {
                builder.Append("Server=");
                builder.Append(this.Server);
                builder.Append(";");
            }

            // Database
            if (this.Database != null)
            {
                builder.Append("Database=");
                builder.Append(this.Database);
                builder.Append(";");
            }

            // Trusted Connnection
            if (this.ttrustedConnectionEnabled && this.TrustedConnection)
            {
                builder.Append("Integrated Security=SSPI");
                builder.Append(";");
            }

           // Commented by v-jsethi 04/24/03 because Trusted_Connection=true/false does
           // does not work with ADO connections
           // if (isTrustedConnectionSet)
           // {
           // builder.Append("Trusted_Connection=");
           // builder.Append(this.TrustedConnection);
           // builder.Append(";");
           // }

            if (this.UserId != null)
            {
                builder.Append("user=");
                builder.Append(this.UserId);
                builder.Append(";");
            }

            // Password
            if (this.Password != null)
            {
                builder.Append("Password=");
                builder.Append(this.Password);
                builder.Append(";");
            }

            // Application Name
            if (this.ApplicationName != null)
            {
                builder.Append("Application Name=");
                builder.Append(this.ApplicationName);
                builder.Append(";");
            }

            // AttachDBFilename
            if (this.AttachDBFilename != null)
            {
                builder.Append("AttachDBFilename=");
                builder.Append(this.AttachDBFilename);
                builder.Append(";");
            }

            // ConnectionTimeout
            if (this.ConnectionTimeout != -1)
            {
                builder.Append("Connection Timeout=");
                builder.Append(this.ConnectionTimeout);
                builder.Append(";");
            }

            // ConnectionLifetime
            if (this.ConnectionLifetime != -1)
            {
                builder.Append("Connection Lifetime=");
                builder.Append(this.ConnectionLifetime);
                builder.Append(";");
            }

            // Connection Reset
            if (this.connectionResetEnabled)
            {
                builder.Append("Connection Reset=");
                builder.Append(this.ConnectionReset);
                builder.Append(";");
            }

            // Current Language
            if (this.CurrentLanguage != null)
            {
                builder.Append("Current Language=");
                builder.Append(this.CurrentLanguage);
                builder.Append(";");
            }

            // Enlist
            if (this.enlistEnabled)
            {
                builder.Append("Enlist=");
                builder.Append(this.Enlist);
                builder.Append(";");
            }

            // Max Pool Size
            if (this.MaxPoolSize != -1)
            {
                builder.Append("Max Pool Size=");
                builder.Append(this.MaxPoolSize);
                builder.Append(";");
            }

            // Min Pool Size
            if (this.MinPoolSize != -1)
            {
                builder.Append("Min Pool Size=");
                builder.Append(this.MinPoolSize);
                builder.Append(";");
            }

            // Network Library 
            if (this.NetworkLibrary != null)
            {
                builder.Append("Network Library =");
                builder.Append(this.NetworkLibrary);
                builder.Append(";");
            }

            // Packet Size
            if (this.PacketSize != -1)
            {
                builder.Append("Packet Size=");
                builder.Append(this.PacketSize);
                builder.Append(";");
            }

            // Persist Security Info
            if (this.PersistSecurityInfo != null)
            {
                builder.Append("Persist Security Info=");
                builder.Append(this.PersistSecurityInfo);
                builder.Append(";");
            }

            // Pooling
            if (this.poolingEnabled)
            {
                builder.Append("Pooling=");
                builder.Append(this.Pooling);
                builder.Append(";");
            }

            // Workstation ID
            if (this.WorkstationId != null)
            {
                builder.Append("Workstation ID=");
                builder.Append(this.WorkstationId);
                builder.Append(";");
            }

            return builder.ToString();
        }
        #endregion
    }
}

