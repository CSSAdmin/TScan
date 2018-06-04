//--------------------------------------------------------------------------------------------
// <copyright file="HelpSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the HelpSmartPart.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Aug 06		JYOTHI		        Created
//*********************************************************************************/
namespace TerraScan.SmartParts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// HelpSmartPart control
    /// </summary>
    public partial class HelpSmartPart : UserControl
    {
        /// <summary>
        /// Assigning Empty to FormId
        /// </summary>
        private string formId = String.Empty;

        private string formName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HelpSmartPart"/> class.
        /// </summary>
        public HelpSmartPart()
        {
            this.InitializeComponent();
            this.HelpLinkLabel.Visible = false;
            this.HelpButton.Visible = true;
        }

        #region Property

        /// <summary>
        /// Gets or sets the FormId
        /// </summary>
        /// <value>The FormId.</value>
        [Description("Display Data based on form Name.")]
        public string FormId
        {
            set { this.formId = value; }
            get { return this.formId; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible help link button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible help link button]; otherwise, <c>false</c>.
        /// </value>
        [Description("Set the visible property of Help Link Button.")]
        public bool VisibleHelpLinkButton
        {
            set { this.HelpLinkLabel.Visible = value; }
            get { return this.HelpLinkLabel.Visible; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible help button].
        /// </summary>
        /// <value><c>true</c> if [visible help button]; otherwise, <c>false</c>.</value>
        [Description("Set the visible property of Help Button")]
        public bool VisibleHelpButton
        {
            set { this.HelpButton.Visible = value; }
            get { return this.HelpButton.Visible; }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the HelpButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelpButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowHelp();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.ShowHelp();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Shows the help.
        /// </summary>
        private void ShowHelp()
        {
            string formName = ParentForm.Text;
            if (!string.IsNullOrEmpty(this.FormId))
            {
                //HelpEngine.getFormName(formName, this.formId);
                HelpEngine.Show(formName, this.formId);
            }
        }

        /// <summary>
        /// HelpMenuItem Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowHelp();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
