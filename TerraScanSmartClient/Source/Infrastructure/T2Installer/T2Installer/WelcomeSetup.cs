//--------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file Screen for Welcomesetup.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// JAN 11 2016      Priyadharshini      Created
//*********************************************************************************//
namespace T2Installer
{
    #region Namespace
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    #endregion
    public partial class WelcomeSetup : Form
    {
        /// <summary>
        /// WelcomeSetup Constructor
        /// </summary>
        public WelcomeSetup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Next Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            WCFScreen1 srn1 = new WCFScreen1();
            srn1.Show();
            this.Hide();
        }

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WelcomeSetup_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        /// <summary>
        /// Cancel Button click
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
    }
}
