//--------------------------------------------------------------------------------------------
// <copyright file="F1021.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1021.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Feb 07        RANJANI              Created
//*********************************************************************************/
namespace D11018
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using TerraScan.Common;
    using System.Configuration;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F1021 Form
    /// </summary>
    public partial class F1021 : BasePage
    {
        #region Private Variables

        /// <summary>
        /// form1021Control Variable
        /// </summary>
        private F1021Controller form1021Control;

        /// <summary>
        /// miscReceiptFields used to communicate eith parent forms
        /// </summary>
        private MiscReceiptFields miscReceiptFields;

        /// <summary>
        /// receivedFrom;
        /// </summary>
        private string receivedFrom;
        

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1021"/> class.
        /// </summary>
        public F1021()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1021"/> class.
        /// </summary>
        /// <param name="parentMiscReceiptFields">The parent misc receipt fields.</param>
        public F1021(MiscReceiptFields parentMiscReceiptFields, string receivedFrom)
        {
            this.InitializeComponent();
            ////set misc receipt fields which contain neceaary information
            this.miscReceiptFields = parentMiscReceiptFields;
            this.receivedFrom = receivedFrom;
            
            this.CancelButton = this.CancelMiscTemplateButton;
            this.SaveMenuToolStripMenuItem.Click += new EventHandler(this.SaveMiscTemplateButton_Click);
            ////Set form name
            this.Text = string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SaveMiscellaneousName"));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1021 control.
        /// </summary>
        /// <value>The form1021 control.</value>
        [CreateNew]
        public F1021Controller F1021Control
        {
            get { return this.form1021Control as F1021Controller; }
            set { this.form1021Control = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1021 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1021_Load(object sender, EventArgs e)
        {
            ////set default value on form load
            this.DefaultCommentTextBox.Text = this.miscReceiptFields.DefaultComment;
            this.HighPriorityCheckBox.Checked = this.miscReceiptFields.HighPriority;
            this.TemplateNameTextBox.Focus();
        }

        #endregion

        #region Private Methods    

        #region Save

        /// <summary>
        /// Handles the Click event of the SaveMiscTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveMiscTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SaveMiscTemplateButton.Enabled)
                {
                    this.SaveMiscTemplateButton.Focus();
                    ////Check For Required Fields
                    if (String.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("MiscReceiptMissingField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.TemplateNameTextBox.Focus();
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;

                    ////insert Misc receipt template and items

                    F11018MiscReceiptData miscReceipt = new F11018MiscReceiptData();
                    F11018MiscReceiptData.SaveMiscReceiptTemplateRow miscReceiptDataRow = miscReceipt.SaveMiscReceiptTemplate.NewSaveMiscReceiptTemplateRow();

                    ////update miscreceiptdatarow table - used to save
                    miscReceiptDataRow.TemplateName = this.TemplateNameTextBox.Text.Trim();
                    ////check for existing
                    this.miscReceiptFields.DefaultComment = this.DefaultCommentTextBox.Text.Trim();
                    
                    if (!string.IsNullOrEmpty(this.miscReceiptFields.DefaultComment))
                    {
                        miscReceiptDataRow.DefaultComment = this.miscReceiptFields.DefaultComment;
                    }

                    miscReceiptDataRow.CommentPriority = this.HighPriorityCheckBox.Checked;
                    miscReceiptDataRow.OwnerID = this.miscReceiptFields.OwnerId;
                    miscReceiptDataRow.UserID = TerraScanCommon.UserId;
                    miscReceiptDataRow.ReceivedFrom = this.receivedFrom;

                    miscReceipt.SaveMiscReceiptTemplate.Rows.Add(miscReceiptDataRow);                   
                    ////save misc template details and returns the TemplateId if save succeed, else return negative value(for template name duplication)
                    int returnValue = this.form1021Control.WorkItem.F1021_SaveMiscReceiptTemplate(Utility.GetXmlString(miscReceipt.SaveMiscReceiptTemplate), this.miscReceiptFields.ReceiptItems, TerraScanCommon.UserId);
                    ////if template name alreary exist return 
                    if (returnValue < 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("DuplicateTemplate"), String.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("CannotSave")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.TemplateNameTextBox.Focus();
                        return;
                    }

                    this.Cursor = Cursors.Default;
                    ////modified flag 
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {                
                this.Cursor = Cursors.Default;
            }
        }     

        #endregion 

        #endregion
    }
}