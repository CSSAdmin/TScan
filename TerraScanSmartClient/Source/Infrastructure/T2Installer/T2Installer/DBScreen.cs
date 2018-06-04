//--------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file Screen for DBscreen.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// JAN 11 2016      Priyadharshini      Created
//*********************************************************************************/
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System;
using System.DirectoryServices;


namespace T2Installer
{
    public partial class DBScreen : Form
    {
        /// <summary>
        /// DBScreen Constructor
        /// </summary>
        public static string variableused = string.Empty;
        public DBScreen()
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
        /// Back button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            DirectoryEntry vDir1 = new DirectoryEntry();

            int VirDirChildren = vDir1.Children.Cast<DirectoryEntry>().Count();

            WCFSecreen2 WCF2 = new WCFSecreen2();
            if (variableused == string.Empty)
            {
                variableused = ConstantVariable.used;
                WCF2.txtUserName.Text = WCFData.UserName;
            }
            WCF2.Show();
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
                WCFData.DBServerName = txtServerName.Text.Trim();
                WCFData.DBName = txtDBName.Text.Trim();
                Tresurer treasurer = new Tresurer();
                treasurer.Show();
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
            if (txtServerName.Text == string.Empty)
            {
                errorProvider1.SetError(txtServerName, ConstantVariable.TextMissing);
                no_error = true;
            }
            if (txtDBName.Text == string.Empty)
            {
                errorProvider1.SetError(txtDBName, ConstantVariable.TextMissing);
                no_error = true;
            }

            return no_error;
        }
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBScreen_Load(object sender, EventArgs e)
        {
            txtServerName.Text = WCFData.DBServerName;
            txtDBName.Text = WCFData.DBName;
            txtServerName.Text = txtServerName.Text.Trim();
            txtDBName.Text = txtDBName.Text.Trim();
            txtServerName.Focus();
            
        }
        /// <summary>
        /// Text Change Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDBName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        /// <summary>
        /// txtServerName text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
    }
}
