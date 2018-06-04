//--------------------------------------------------------------------------------------------
// <copyright file="Treasurer.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file Screen for Treasurer.
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
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using Microsoft.Win32;    
    #endregion

    /// <summary>
    /// Treasurer class
    /// </summary>
    public partial class Tresurer : Form
    {
        // Getting assembly location
        static System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
        string dir = Path.GetDirectoryName(asmbly.Location);
        public static Process process;

        /// <summary>
        /// Tresurer Constructor
        /// </summary>
        public Tresurer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cancel button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(ConstantVariable.AbortMessage, ConstantVariable.MessageBoxTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (Process process in Process.GetProcesses())
                    {
                        if (process.MainWindowTitle == ConstantVariable.T2SetupName)
                        {
                            process.Kill();
                        }
                    }
                    foreach (Process process in Process.GetProcesses())
                    {
                        if (process.MainWindowTitle == ConstantVariable.SetupName)
                        {
                            process.Kill();
                        }
                    }
                    Application.Exit();
                }
            }
            catch (Exception error)
            {
                ExceptionManager.LogException(error.Message);
            }
        }

        /// <summary>
        /// back button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            DBScreen DB = new DBScreen();
            DB.Show();
            this.Hide();
        }

        /// <summary>
        /// Next button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!is_validate())
            {
                try
                {
                    //start wcf Process
                    process = new Process();
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.StartInfo.FileName = dir + ConstantVariable.WcfMsiName;
                    process.Start();

                    //Assign T2 values to ArrayList
                    TreasurerData.WCFServiceURL = txtWCFService.Text.Trim();
                    TreasurerData.InstallURL = txtInstalURL.Text.Trim();
                    TreasurerData.MSWCFServiceURL = txtMSWCF.Text.Trim();

                    #region Write Content to setup xml
                    XDocument doc = new XDocument();
                    XElement elem1 =
                     new XElement(ConstantVariable.WCF,
                           new XAttribute(ConstantVariable.DomainName, WCFData.DomainName.Trim()),
                           new XAttribute(ConstantVariable.LDAP, WCFData.LDAP.Trim()),
                           new XAttribute(ConstantVariable.UserName, WCFData.UserName.Trim()),
                           new XAttribute(ConstantVariable.Password, WCFData.Password.Trim()),
                           new XAttribute(ConstantVariable.DbServerName, WCFData.DBServerName.Trim()),
                           new XAttribute(ConstantVariable.DbName, WCFData.DBName.Trim()),
                           new XAttribute(ConstantVariable.Handle, WCFData.handle)
                      );

                    XElement elem2 = new XElement(ConstantVariable.Treasurer,
                         new XAttribute(ConstantVariable.WCFServiceURL, TreasurerData.WCFServiceURL.Trim()),
                          new XAttribute(ConstantVariable.InstalURL, TreasurerData.InstallURL.Trim()),
                          new XAttribute(ConstantVariable.MSWCFServiceURL, TreasurerData.MSWCFServiceURL.Trim())
                     );
                    doc = new XDocument(new XElement(ConstantVariable.Installer, elem1, elem2));
                    if (File.Exists(dir + ConstantVariable.SetupXml) == true)
                    {
                        File.Delete(dir + ConstantVariable.SetupXml);
                    }
                    doc.Save(dir + ConstantVariable.SetupXml);
                    #endregion

                    #region write location to registry
                    RegistryKey ourKey = Registry.LocalMachine;
                    ourKey = ourKey.CreateSubKey(ConstantVariable.Terrascan32Registry);
                    ourKey.SetValue(string.Empty, dir);
                    ourKey.Close();
                    ourKey = null;
                    doc = null;

                    RegistryKey terrascan64RegistryKey = Registry.LocalMachine;
                    terrascan64RegistryKey = terrascan64RegistryKey.CreateSubKey(ConstantVariable.Terrascan64Registry);
                    terrascan64RegistryKey.SetValue(string.Empty, dir);
                    terrascan64RegistryKey.Close();
                    terrascan64RegistryKey = null;

                    #endregion
                }

                catch (Exception error)
                {
                    ExceptionManager.LogException(error.Message);
                }
                finally
                {
                    Application.Exit();
                }
            }

        }

        /// <summary>
        /// Validation
        /// </summary>
        /// <returns></returns>
        private bool is_validate()
        {
            bool no_error = false;
            errorProvider1.Clear();
            if (txtInstalURL.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtInstalURL, ConstantVariable.TextMissing);
                no_error = true;
            }
            if (txtMSWCF.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtMSWCF, ConstantVariable.TextMissing);
                no_error = true;
            }
            if (txtWCFService.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtWCFService, ConstantVariable.TextMissing);
                no_error = true;
            }
            return no_error;
        }

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tresurer_Load(object sender, EventArgs e)
        {
            this.Focus();
            txtWCFService.Text = TreasurerData.WCFServiceURL != null ? TreasurerData.WCFServiceURL.Trim() : string.Empty;
            txtInstalURL.Text = TreasurerData.InstallURL != null ? TreasurerData.InstallURL.Trim() : string.Empty;
            txtMSWCF.Text = TreasurerData.MSWCFServiceURL != null ? TreasurerData.MSWCFServiceURL.Trim() : string.Empty;
            txtWCFService.Text = txtWCFService.Text.Trim();
            txtInstalURL.Text = txtInstalURL.Text.Trim();

            txtMSWCF.Text = txtMSWCF.Text.Trim();
            txtWCFService.Text = "http://" + WCFData.DBServerName + "/" + WCFData.DBName + ConstantVariable.SmartClientServiceSVC;
            txtInstalURL.Text = "http://" + WCFData.DBServerName + "/" + WCFData.DBName + ConstantVariable.T2App;
            txtMSWCF.Text = "http://" + WCFData.DBServerName + "/" + WCFData.DBName + ConstantVariable.ServiceSVC;
        }

        /// <summary>
        /// txtInstal text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInstalURL_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        /// <summary>
        /// txtWCFService text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWCFService_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        /// <summary>
        /// txtMSWCF text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMSWCF_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
