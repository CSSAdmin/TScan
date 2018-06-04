//--------------------------------------------------------------------------------------------
// <copyright file="F810031.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F810031.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Jan 09        Sadha Shivudu M    Created
//*********************************************************************************/

namespace D81003
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;

    #endregion namespace

    /// <summary>
    /// F810031 class
    /// </summary>
    public partial class F810031 : Form
    {
        #region instance variables

        /// <summary>
        /// instance variable to hold the formula value
        /// </summary>
        private string formula = string.Empty;

        #endregion instance variables

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F810031"/> class.
        /// </summary>
        public F810031()
        {
            InitializeComponent();
        }

        #endregion constructor

        #region property

        /// <summary>
        /// Gets or sets the formula.
        /// </summary>
        /// <value>The formula.</value>
        public string Formula
        {
            get 
            { 
                return this.formula; 
            }

            set 
            { 
                this.formula = value; 
            }
        }

        #endregion property
        
        #region event handler methods

        /// <summary>
        /// Handles the Load event of the F810031 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F810031_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Formula.Trim()))
                {
                    this.FormulaTextBox.Text = this.Formula;
                    this.FormulaTextBox.TabStop = true;
                    this.FormulaTextBox.Focus();
                }
                else
                {
                    this.FormulaTextBox.TabStop = false;
                    this.HelpLink.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the OkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion event handler methods
    }
}