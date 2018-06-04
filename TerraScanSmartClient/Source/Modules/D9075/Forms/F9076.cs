//----------------------------------------------------------------------------------
// <copyright file="F9076.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9076.cs.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			 Author		          Description
// ----------	 ---------		      ----------------------------------------------
// 13/12/2008    A.Shanmuga Sundaram  Created// 
//*********************************************************************************/

namespace D9075
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Xml;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using System.IO;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;

    /// <summary>
    /// Class file for F9076
    /// </summary>
    public partial class F9076 : BasePage
    {
        #region Variables
        /// <summary>
        /// F9041Controller
        /// </summary>
        private F9076Controller form9076Control;

        /// <summary>
        /// templateid
        /// </summary>
        private int templateid;

        /// <summary>
        /// templateName
        /// </summary>
        private string templateName;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        /// templateName
        /// </summary>
        private string comments;

        /// <summary>
        /// templateName
        /// </summary>
        private bool commentpriority;

        /// <summary>
        /// templateName
        /// </summary>
        private bool commentwillRoll;

        /// <summary>
        /// templateName
        /// </summary>
        private bool commentprint;

        /// <summary>
        /// templateName
        /// </summary>
        private bool commentpubliccheckbox;

        /// <summary>
        /// Formid
        /// </summary>
        private int formid;

        /// <summary>
        /// commentTemplateData
        /// </summary>
        private F9076NewCommentTemplateData commentTemplateData = new F9076NewCommentTemplateData();

        /// <summary>
        /// Current comment priority
        /// </summary>
        private int commentPriorityId;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9041"/> class.
        /// </summary>
        public F9076()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9610"/> class.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="print">if set to <c>true</c> [print].</param>
        /// <param name="priority">The priority.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="willRoll">if set to <c>true</c> [will roll].</param>
        /// <param name="publiccheckbox">if set to <c>true</c> [publiccheckbox].</param>
        /// <param name="formid">The formid.</param>
        public F9076(int templateId, string templateName, bool print, string priority, string comment, bool willRoll, bool publiccheckbox, int formid, int priorityId)
        {
            this.InitializeComponent();
            this.templateid = templateId;
            this.templateName = templateName;
            this.comments = comment;
            this.commentprint = print;
            this.formid = formid;
            if (priority == "LOW")
            {
                this.commentpriority = false;
            }
            else
            {
                this.commentpriority = true;
            }

            this.commentpubliccheckbox = publiccheckbox;
            this.commentwillRoll = willRoll;
            this.commentPriorityId = priorityId;
        }

        #endregion

        #region Property

        /// <summary>
        /// For F9041Control
        /// </summary>
        [CreateNew]
        public F9076Controller Form9076Control
        {
            get { return this.form9076Control as F9076Controller; }
            set { this.form9076Control = value; }
        }

        /// <summary>
        /// Gets or sets the template ID.
        /// </summary>
        /// <value>The template ID.</value>
        public int TemplateId
        {
            get { return this.templateid; }
            set { this.templateid = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F9041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9076_Load(object sender, EventArgs e)
        {
            try
            {
                this.CancelNewCommentTemplateButton.Enabled = true;
                this.TemplateNameTextBox.Text = this.templateName;
                this.formNo = 9076;
                this.keyField = "TemplateID";
                if (string.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim()))
                {
                    this.SaveNewCommentTemplatebutton.Enabled = false;
                    this.DeleteNewCommentTemplatebutton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Handles the Click event of the CancelNewCommentTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelNewCommentTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteNewCommentTemplatebutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeleteNewCommentTemplatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.templateid > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("DeleteTemplateName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult.Yes == dialogResult)
                    {
                        this.form9076Control.WorkItem.F9076_DeleteNewCommentTemplate(this.templateid);
                        this.DialogResult = DialogResult.OK;
                        Form.ActiveForm.Close();
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("NoTemplateName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.TemplateNameTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveNewCommentTemplatebutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveNewCommentTemplatebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim()))
            {
                int returnValue = 0;
                int isoverWrite = 0;
                int primaryKey = 0;
                this.SaveNewTemplate();
                string commentItemsXml = this.GetCommentItemXmlString();
                returnValue = this.form9076Control.WorkItem.F9076SaveNewCommentTemplate(this.templateid, commentItemsXml, isoverWrite);
                if (this.templateid <= 0 && !WSHelper.IsOnLineMode)
                    TerraScanCommon.AddFieldUseValues(returnValue, this.keyField, this.formNo, null, TerraScanCommon.UserId);
                if ((returnValue != 0))
                {
                    this.DialogResult = DialogResult.OK;
                    //Form.ActiveForm.Close();
                    this.Close();
                }
                else if (returnValue == 0)
                {
                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("UpdateExistTemplate"), SharedFunctions.GetResourceString("HeaderCommentTemplate"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);  // ConfigurationWrapper.ApplicationName
                    if (DialogResult.Yes == dialogResult)
                    {
                        isoverWrite = 1;
                        primaryKey = this.form9076Control.WorkItem.F9076SaveNewCommentTemplate(this.templateid, commentItemsXml, isoverWrite);
                        if (this.templateid <= 0 && !WSHelper.IsOnLineMode)
                            TerraScanCommon.AddFieldUseValues(returnValue, this.keyField, this.formNo, null, TerraScanCommon.UserId);
                        this.DialogResult = DialogResult.OK;
                        //Form.ActiveForm.Close();
                        this.Close();
                    }

                    this.TemplateNameTextBox.Focus();
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("TempalteName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.TemplateNameTextBox.Focus();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TemplateNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TemplateNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TemplateNameTextBox.Text.Trim()))
            {
                this.SaveNewCommentTemplatebutton.Enabled = true;
                this.DeleteNewCommentTemplatebutton.Enabled = true;
            }
            else
            {
                this.DeleteNewCommentTemplatebutton.Enabled = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves the new template.
        /// </summary>
        private void SaveNewTemplate()
        {
            if (this.commentTemplateData.GetCommentTemplate.Rows.Count > 0)
            {
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.FormColumn.ColumnName] = this.formid;
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.TemplateColumn.ColumnName] = this.TemplateNameTextBox.Text.Trim();
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.CommentColumn.ColumnName] = this.comments;
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.WillRollColumn.ColumnName] = this.commentwillRoll;
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.WillPrintColumn.ColumnName] = this.commentprint;
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.IsPublicColumn.ColumnName] = this.commentpubliccheckbox;
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.IsHighPriorityColumn.ColumnName] = this.commentpriority;
                this.commentTemplateData.GetCommentTemplate.Rows[0][this.commentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName] = this.commentPriorityId;
            }
            else
            {
                DataRow newCommentTemplateRow = this.commentTemplateData.GetCommentTemplate.NewRow();
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.FormColumn.ColumnName] = this.formid;
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.TemplateColumn.ColumnName] = this.TemplateNameTextBox.Text.Trim();
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.CommentColumn.ColumnName] = this.comments;
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.WillRollColumn.ColumnName] = this.commentwillRoll;
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.WillPrintColumn.ColumnName] = this.commentprint;
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.IsPublicColumn.ColumnName] = this.commentpubliccheckbox;
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.IsHighPriorityColumn.ColumnName] = this.commentpriority;
                newCommentTemplateRow[this.commentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName] = this.commentPriorityId;
                this.commentTemplateData.GetCommentTemplate.Rows.Add(newCommentTemplateRow);
                this.commentTemplateData.GetCommentTemplate.AcceptChanges();
            }
        }

        /// <summary>
        /// Gets the comment item XML string.
        /// </summary>
        /// <returns>Get the Xml string</returns>
        private string GetCommentItemXmlString()
        {
            string commentXml = string.Empty;
            DataSet commentTemplateDataset = new DataSet("Root");
            commentTemplateDataset.Merge(this.commentTemplateData.GetCommentTemplate);
            commentTemplateDataset.Tables[this.commentTemplateData.GetCommentTemplate.TableName].TableName = "Table";
            commentXml = commentTemplateDataset.GetXml();
            return commentXml;
        }

        #endregion Methods
    }
}