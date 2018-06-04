//--------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file Screen for WCFScreen1.
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
    #region Namespace
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    #endregion 
    public partial class WCFScreen1 : Form
    {
        /// <summary>
        /// WCFScreen1 Constructor
        /// </summary>
        public WCFScreen1()
        {
            InitializeComponent();
            txtLDAP.Text = WCFData.LDAP != null ? WCFData.LDAP.Trim() : string.Empty;
        }

        /// <summary>
        /// cancel button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
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

        /// <summary>
        /// back button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            WelcomeSetup welcomeScreen = new WelcomeSetup();
            welcomeScreen.Show();
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
                DBScreen.variableused = string.Empty;
                WCFData.DomainName = txtDomainName.Text.Trim();
                WCFData.LDAP = txtLDAP.Text.Trim();
                WCFSecreen2 wcf2 = new WCFSecreen2();
                wcf2.Show();
                this.Hide();
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
            if (txtDomainName.Text == string.Empty)
            {
                errorProvider1.SetError(txtDomainName, ConstantVariable.TextMissing);
                no_error = true;
            }

            if (txtLDAP.Text == string.Empty)
            {
                errorProvider1.SetError(txtLDAP, ConstantVariable.TextMissing);
                no_error = true;
            }

            return no_error;
        }

        /// <summary>
        /// txt domain name text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDomainName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            txtLDAP.Text = "LDAP://" + txtDomainName.Text.Trim();

        }

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WCFScreen1_Load(object sender, EventArgs e)
        {
            if (WCFData.DomainName != null)
            {
                txtDomainName.Text = WCFData.DomainName != null ? WCFData.DomainName.Trim() : string.Empty;
                txtLDAP.Text = WCFData.LDAP != null ? WCFData.LDAP.Trim() : string.Empty;
            }

            this.txtDomainName.Text = this.txtDomainName.Text.Trim();
            this.txtDomainName.Focus();
        }

        /// <summary>
        /// txtLDAP change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLDAP_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
