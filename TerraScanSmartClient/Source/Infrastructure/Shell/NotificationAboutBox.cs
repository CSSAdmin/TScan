using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TerraScan.BusinessEntities;
using System.Diagnostics;

namespace TerraScan.UI
{
    partial class NotificationAboutBox : Form
    {
        public static string getPrivacyNotification = string.Empty;
        public NotificationAboutBox()
        {
            InitializeComponent();
            
            CommentsData getPrivacyStmt = new CommentsData();
            getPrivacyStmt = LoginController.LoginConfigDetails("TS_PrivacyStmtURL");
            if (getPrivacyStmt.GetCommentsConfigDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(getPrivacyStmt.GetCommentsConfigDetails.Rows[0][getPrivacyStmt.GetCommentsConfigDetails.ConfigurationValueColumn].ToString()))
                {
                    getPrivacyNotification = Convert.ToString(getPrivacyStmt.GetCommentsConfigDetails.Rows[0][getPrivacyStmt.GetCommentsConfigDetails.ConfigurationValueColumn]);
                    
                }
            }
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void OkButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotificationStmtLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(getPrivacyNotification);
            proc.StartInfo = startInfo;
            proc.Start();
        }
    }
}
