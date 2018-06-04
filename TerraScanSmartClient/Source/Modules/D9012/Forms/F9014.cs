//--------------------------------------------------------------------------------------------
// <copyright file="F9014.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9014.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27/02/2006       M.Vijaya Kumar     Created// 
//*********************************************************************************/

namespace D9012
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Helper;

    /// <summary>
    /// F9014 Class file
    /// </summary>
    public partial class F9014 : Form
    {
        #region Variables

        /// <summary>
        /// Used to store the testKeyId
        /// </summary>
        private string testKeyId;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9014"/> class.
        /// </summary>
        public F9014()
        {
            InitializeComponent();
            this.AcceptButton = this.AcceptTestKeyIDButton;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the test key id.
        /// </summary>
        /// <value>The test key id.</value>
        public string TestKeyId
        {
            get { return this.testKeyId; }
            set { this.testKeyId = value; }
        }

        #endregion Properties

        #region Events

        /// <summary>
        /// Handles the Load event of the F9014 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9014_Load(object sender, EventArgs e)
        {
            try
            {
                this.CancelButton = this.NextNumberCloseButton;
                this.TestKeyIDTextBox.Text = string.Empty;
            }         
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Handles the Click event of the AcceptTestKeyIDButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptTestKeyIDButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.TestKeyIDTextBox.Text.Trim()))
                {
                    this.testKeyId = this.TestKeyIDTextBox.Text.Trim();
                }
                else
                {
                    this.testKeyId = string.Empty;
                }

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the F9014 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9014_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (!string.IsNullOrEmpty(this.TestKeyIDTextBox.Text.Trim()))
                        {
                            this.testKeyId = this.TestKeyIDTextBox.Text.Trim();
                        }
                        else
                        {
                            this.testKeyId = string.Empty;
                        }

                        this.DialogResult = DialogResult.Yes;
                    }
                }
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// NextNumberCloseButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void NextNumberCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        #endregion Events
    }
}