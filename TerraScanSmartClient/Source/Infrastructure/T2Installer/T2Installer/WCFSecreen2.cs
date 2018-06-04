//--------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file Screen for WCFScreen2.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// JAN 11 2016      Priyadharshini      Created
//*********************************************************************************/using System;

namespace T2Installer
{
    #region Namespace
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    #endregion
    public partial class WCFSecreen2 : Form
    {
        /// <summary>
        /// WCFScreen2 Constructor
        /// </summary>
        public WCFSecreen2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// next button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!is_validate())
            {
                WCFData.UserName = txtUserName.Text.Trim();
                WCFData.Password = txtPassword.Text.Trim();
                DBScreen DBscreen = new DBScreen();
                DBscreen.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// validation
        /// </summary>
        /// <returns></returns>
        private bool is_validate()
        {
            bool no_error = false;
            errorProvider1.Clear();
            if (txtUserName.Text == string.Empty)
            {
                errorProvider1.SetError(txtUserName, ConstantVariable.TextMissing);
                no_error = true;
            }
            if (txtPassword.Text == string.Empty)
            {
                errorProvider1.SetError(txtPassword, ConstantVariable.TextMissing);
                no_error = true;
            }
            return no_error;
        }

        /// <summary>
        /// back button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            WCFScreen1 WCF1Screen = new WCFScreen1();
            WCF1Screen.Show();
            this.Hide();
        }

        /// <summary>
        /// Cancel button click
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
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WCFSecreen2_Load(object sender, EventArgs e)
        {
            this.Focus();
            txtUserName.Text = txtUserName.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();
            if (DBScreen.variableused == string.Empty)
            {
                txtUserName.Text = WCFData.DomainName + "\\";
            }
            else
            {
                txtUserName.Text = WCFData.UserName;
            }
            if (WCFData.UserName != null)
            {
                txtPassword.Text = WCFData.Password;
            } 
            txtPassword.Text = WCFData.Password != null ? WCFData.Password.Trim() : string.Empty;
        }

        /// <summary>
        /// txtPassword Text Change Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        /// <summary>
        /// txtUserName Chnage Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
