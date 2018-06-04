// -------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationWrapper.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>Contains the values in the configurationfile </summary>
// VERSION  	DESCRIPTION
//
// -------------------------------------------------------------------------------------------------

namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Configuration;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// Class ConfigurationWrapper
    /// </summary>
    public static class ConfigurationWrapper
    {
        #region fields

        /// <summary>
        /// domainNameAdmin
        /// </summary>
        private static string domainNameAdmin;

        /// <summary>
        /// barCodeSessionTimeOut
        /// </summary>
        private static int barCodeSessionTimeOut;

        /// <summary>
        /// domainName
        /// </summary>
        private static string domainName;

        /// <summary>
        /// applicationName
        /// </summary>
        private static string applicationName;

        ///<summary>
        /// applicationSave
        ///</summary>
        private static string applicationSave;

        ///<summary>
        /// applicationDelete
        ///</summary>
        private static string applicationDelete;

        /// <summary>
        /// applicationDuplicateCheck
        /// </summary>
        private static string applicationDuplicateCheck;

        /// <summary>
        /// countyName
        /// </summary>
        private static string countyName;

        /// <summary>
        /// toID
        /// </summary>
        private static string toID;

        /// <summary>
        /// sMTPAddress
        /// </summary>
        private static string sMTPAddress;
        
        /// <summary>
        /// uIPolicyName
        /// </summary>
        private static string uIPolicyName;

        /// <summary>
        /// exceptionMessage
        /// </summary>
        private static string exceptionMessage;

        /// <summary>
        /// reportURL
        /// </summary>
        private static string reportURL;

        /// <summary>
        /// reportPath
        /// </summary>
        private static string reportPath;

        /// <summary>
        /// reportUsername
        /// </summary>
        private static string reportUsername;

        /// <summary>
        /// reportPassword
        /// </summary>
        private static string reportPassword;

        /// <summary>
        /// webServiceUrl
        /// </summary>
        private static string webServiceUrl;

        /// <summary>
        /// reportServiceAsmx
        /// </summary>
        private static string reportServiceAsmx;

        /// <summary>
        /// reportViewerURL
        /// </summary>
        private static string reportViewerURL;

        /// <summary>
        /// activeControlBackRedColor
        /// </summary>
        private static string activeControlBackRedColor;

        /// <summary>
        /// activeControlBackGreenColor
        /// </summary>
        private static string activeControlBackGreenColor;

        /// <summary>
        /// activeControlBackBlueColor
        /// </summary>
        private static string activeControlBackBlueColor;

        /// <summary>
        /// fieldDomainName
        /// </summary>
        private static string fieldDomainName;

        /// <summary>
        /// fieldRequired
        /// </summary>
        private static string fieldRequired;

        /// <summary>
        /// mdfServerFilePath
        /// </summary>
        private static string mdfServerFilePath;

        /// <summary>
        /// connectionString
        /// </summary>
        private static string connectionString;

        #endregion fields

        /// <summary>
        /// Gets the DomainNameAdmin
        /// </summary>
        public static string DomainNameAdmin
        {
            get 
            {
                if (string.IsNullOrEmpty(domainNameAdmin))
                {
                    domainNameAdmin = ConfigurationManager.AppSettings["DomainNameAdmin"];
                }

                return domainNameAdmin;
            }            
        }

        /// <summary>
        /// Gets Bar code Session Time Out
        /// </summary>
        public static int BarCodeSessionTimeOut
        {
            get
            {
                if (barCodeSessionTimeOut.Equals(0))
                {
                    int.TryParse( ConfigurationManager.AppSettings["BarCodeSessionTimeOut"].ToString(), out barCodeSessionTimeOut);
                }

                return barCodeSessionTimeOut;
            }
        }

        /// <summary>
        /// Gets the DomainName
        /// </summary>
        public static string DomainName
        {
            get
            {
                if (string.IsNullOrEmpty(domainName))
                {
                    domainName = ConfigurationManager.AppSettings["DomainName"];
                }

                return domainName;
            }            
        }       

        /// <summary>
        /// Gets the ApplicaionName
        /// </summary>
        public static string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(applicationName))
                {
                    applicationName = ConfigurationManager.AppSettings["ApplicationName"];
                } 

                    return applicationName;
            }
        }
                
        /// <summary>
        /// Gets the ApplicationSave
        /// </summary>
        public static string ApplicationSave
        {
            get
            {
                if (string.IsNullOrEmpty(applicationSave))
                {
                    applicationSave = ConfigurationManager.AppSettings["ApplicationSave"];
                }

                return applicationSave;
            }
        }
                
        /// <summary>
        /// Gets the ApplicationDelete
        /// </summary>
        public static string ApplicationDelete
        {
            get
            {
                if (string.IsNullOrEmpty(applicationDelete))
                {
                    applicationDelete = ConfigurationManager.AppSettings["ApplicationDelete"];
                }

                return applicationDelete;
            }
        }
        
        /// <summary>
        /// Gets the ApplicationDuplicateCheck
        /// </summary>
        public static string ApplicationDuplicateCheck
        {
            get
            {
                if (string.IsNullOrEmpty(applicationDuplicateCheck))
                {
                    applicationDuplicateCheck = ConfigurationManager.AppSettings["ApplicationDuplicateCheck"];
                }

                return applicationDuplicateCheck;
            } 
        }
        
        /// <summary>
        ///  Gets the CountyName
        /// </summary>
        public static string CountyName
        {
            get
            {
                if (string.IsNullOrEmpty(countyName))
                {
                    countyName = ConfigurationManager.AppSettings["CountyName"];
                }

                return countyName;
            }
        }
      
        /// <summary>
        /// Gets the ToID
        /// </summary>
        public static string ToID
        {
            get
            {
                if (string.IsNullOrEmpty(toID))
                {
                    toID = ConfigurationManager.AppSettings["ToID"];
                }

                return toID;
            }
        }
        
        /// <summary>
        /// Gets the SMTPAddress
        /// </summary>
        public static string SMTPAddress
        {
            get
            {
                if (string.IsNullOrEmpty(sMTPAddress))
                {
                    sMTPAddress = ConfigurationManager.AppSettings["SMTPAddress"];
                }

                return sMTPAddress;
            }
        }
       
        /// <summary>
        /// Gets the UIPolicyName 
        /// </summary>
        public static string UIPolicyName
        {
            get
            {
                if (string.IsNullOrEmpty(uIPolicyName))
                {
                    uIPolicyName = ConfigurationManager.AppSettings["UIPolicyName"];
                }

                return uIPolicyName;
            }
        }
                
        /// <summary>
        /// Gets the ExceptionMessage 
        /// </summary>
        public static string ExceptionMessage
        {
            get
            {
                if (string.IsNullOrEmpty(exceptionMessage))
                {
                    exceptionMessage = ConfigurationManager.AppSettings["ExceptionMessage"];
                }

                return exceptionMessage;
            }
        }
        
        /// <summary>
        /// Gets the ReportURL
        /// </summary>
        public static string ReportURL
        {
            get
            {
                if (string.IsNullOrEmpty(reportURL))
                {
                    reportURL = ConfigurationManager.AppSettings["ReportURL"];
                }

                return reportURL;
            }
        }
        
        /// <summary>
        /// Gets the ReportPath
        /// </summary>
        public static string ReportPath
        {
            get
            {
                if (string.IsNullOrEmpty(reportPath))
                {
                    reportPath = ConfigurationManager.AppSettings["ReportPath"];
                }

                return reportPath;
            }
        }
        
        /// <summary>
        /// Gets the ReportUserName
        /// </summary>
        public static string ReportUserName
        {
            get
            {
                if (string.IsNullOrEmpty(reportUsername))
                {
                    reportUsername = ConfigurationManager.AppSettings["ReportUserName"];
                }

                return reportUsername;
            }
        }
        
        /// <summary>
        /// Gets the ReportPassword
        /// </summary>
        public static string ReportPassword
        {
            get
            {
                if (string.IsNullOrEmpty(reportPassword))
                {
                    reportPassword = ConfigurationManager.AppSettings["ReportPassword"];
                }

                return reportPassword;
            }
        }
        
        /// <summary>
        /// Gets the WebServiceUrl
        /// </summary>
        public static string WebServiceUrl
        {
            get
            {
                if (string.IsNullOrEmpty(webServiceUrl))
                {
                    webServiceUrl = ConfigurationManager.AppSettings["WebServiceUrl"];
                }

                return webServiceUrl;
            }
        }
        
        /// <summary>
        /// Gets the ReportServiceAsmx
        /// </summary>
        public static string ReportServiceAsmx
        {
            get
            {
                if (string.IsNullOrEmpty(reportServiceAsmx))
                {
                    reportServiceAsmx = ConfigurationManager.AppSettings["ReportServiceAsmx"];
                }

                return reportServiceAsmx;
            }
        }
                
        /// <summary>
        /// Gets the ReportViewerURL
        /// </summary>
        public static string ReportViewerURL
        {
            get
            {
                if (string.IsNullOrEmpty(reportViewerURL))
                {
                    reportViewerURL = ConfigurationManager.AppSettings["ReportViewerURL"];
                }

                return reportViewerURL;
            }
        }
        
        /// <summary>
        /// Gets the ActiveControlBackRedColor
        /// </summary>
        public static string ActiveControlBackRedColor
        {
            get
            {
                if (string.IsNullOrEmpty(activeControlBackRedColor))
                {
                    activeControlBackRedColor = ConfigurationManager.AppSettings["ActiveControlBackRedColor"];
                }

                return activeControlBackRedColor;
            }
        }
        
        /// <summary>
        /// Gets the ActiveControlBackGreenColor
        /// </summary>
        public static string ActiveControlBackGreenColor
        {
            get
            {
                if (string.IsNullOrEmpty(activeControlBackGreenColor))
                {
                    activeControlBackGreenColor = ConfigurationManager.AppSettings["ActiveControlBackGreenColor"];
                }

                return activeControlBackGreenColor;
            }
        }
        
        /// <summary>
        /// Gets the ActiveControlBackBlueColor
        /// </summary>
        public static string ActiveControlBackBlueColor
        {
            get
            {
                if (string.IsNullOrEmpty(activeControlBackBlueColor))
                {
                    activeControlBackBlueColor = ConfigurationManager.AppSettings["ActiveControlBackBlueColor"];
                }

                return activeControlBackBlueColor;
            }
        }

        /// <summary>
        /// Gets the name of the field domain.
        /// </summary>
        /// <value>The name of the field domain.</value>
        public static string FieldDomainName
        {
            get
            {
                if (string.IsNullOrEmpty(fieldDomainName))
                {
                    fieldDomainName = ConfigurationManager.AppSettings["FieldDomainName"];
                }

                return fieldDomainName;
            }
        }

        /// <summary>
        /// Gets the field required.
        /// </summary>
        /// <value>The field required.</value>
        public static string FieldRequired
        {
            get
            {
                if (string.IsNullOrEmpty(fieldRequired))
                {
                    fieldRequired = ConfigurationManager.AppSettings["FieldRequired"];
                }

                return fieldRequired;
            }
        }

        /// <summary>
        /// Gets the server MDF file path.
        /// </summary>
        /// <value>The server MDF file path.</value>
        public static string ServerMdfFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(mdfServerFilePath))
                {
                    string serverFilePath = string.Empty;
                    CommentsData serverFilePathData = WSHelper.GetConfigDetails("TS_FieldDBPath");
                    mdfServerFilePath = serverFilePathData.GetCommentsConfigDetails[0][0].ToString();
                }

                return mdfServerFilePath;
            }
        }

        /// <summary>
        /// Gets the name of the field domain.
        /// </summary>
        /// <value>The name of the field domain.</value>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                }

                return connectionString;
            }
        }
    }
}
