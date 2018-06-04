//--------------------------------------------------------------------------------------------
// <copyright file="F1532.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1532.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Nov 06       RANJANI              Created
//*********************************************************************************/
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using TerraScan.Common;
    using System.Configuration;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F1532 Form
    /// </summary>
    public partial class F1532 : BasePage
    {
        #region Private Variables

        /// <summary>
        /// form1532Control Variable
        /// </summary>
        private F1532Controller form1532Control;

        /// <summary>
        /// DataSet Contains Institution Detail 
        /// </summary>
        private F1530CashAccountManagementData cashAccountManagement = new F1530CashAccountManagementData();

        /// <summary>
        /// pageLoadStatus variable is used to find the pageLoadStatus. 
        /// </summary>   
        private bool pageLoadStatus = true;

        /// <summary>
        /// contactId variable is used to store contactId. - default value - '-999'(invalid value)
        /// </summary>   
        private int contactId = -999;        

        /// <summary>
        /// institutionId variable is used to store institutionId. - default value - '-999'(invalid value)
        /// </summary>   
        private int institutionId = -999;

        /// <summary>
        /// formId variable is used to store current form id.
        /// </summary>   
        private int formId = 1532;

        /// <summary>
        /// pageMode variable is find form action mode - whether view, new
        /// </summary>   
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to Check Valid Email
        /// </summary>       
        private string validEmail = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

        #endregion   

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1531"/> class.
        /// </summary>
        public F1532()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1531"/> class with parameters
        /// </summary>
        /// <param name="parentFormId">The parent form id.</param>
        /// <param name="pageModeType">Type of the page mode.</param>
        /// <param name="institutionid">The institutionid.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="institutionName">Name of the institution.</param>
        public F1532(int parentFormId, TerraScanCommon.PageModeTypes pageModeType, int institutionid, int contactId, string institutionName)
        {
            this.InitializeComponent();                     
            ////assign default value
            this.ParentFormId = parentFormId;
            this.institutionId = institutionid;
            this.pageMode = pageModeType;
            this.contactId = contactId;            
            ////SetMaxLength for editable fields   
            this.SetMaxLength();   
            ////set short cut key
            this.CancelButton = this.CancelContactButton;
            this.SaveMenuToolStripMenuItem.Click += new EventHandler(this.SaveContactButton_Click);
            ////Set form name
            this.Text = string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("InstitutionContactName"), institutionName);
        }

        #endregion  

        #region Properties

        /// <summary>
        /// Gets or sets the 1532 control.
        /// </summary>
        /// <value>The 1532 control.</value>
        [CreateNew]
        public F1532Controller F1532Control
        {
            get { return this.form1532Control as F1532Controller; }
            set { this.form1532Control = value; }
        }

        /// <summary>
        /// Gets or sets the contact id.
        /// </summary>
        /// <value>The contact id.</value>
        public int ContactId
        {
            get { return this.contactId; }
            set { this.contactId = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1532 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1532_Load(object sender, EventArgs e)
        {
            ////load comboboxes
            this.LoadComboBox();
            ////pagemode will contain add or view
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                ////load in view mode                
                this.GetInstitutionContactDetail();
                this.SaveContactButton.Enabled = false;
            }
            else
            {
                ////load in new mode
                this.ClearInstitutionContact();
                this.SaveContactButton.Enabled = true;
            }

            ////set default focus
            this.NameTextBox.Focus();
        }

        #endregion   

        #region Private Methods

        #region Get Institution Contact

        /// <summary>
        /// Gets the InstitutionContact detail
        /// </summary>
        private void GetInstitutionContactDetail()
        {
            try
            {
                ////set pageLoadStatus - suppress textchanged event
                this.pageLoadStatus = true;
                this.cashAccountManagement.Clear();
                this.cashAccountManagement = this.F1532Control.WorkItem.F1532_GetInstitutionContactDetail(this.contactId);

                if (this.cashAccountManagement.GetInstitutionContact.Rows.Count > 0)
                {
                    this.ContactIdTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.ContactIDColumn].ToString();
                    this.NameTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.NameColumn].ToString();
                    this.Address1TextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.Address1Column].ToString();
                    this.Address2TextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.Address2Column].ToString();
                    this.CityTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.CityColumn].ToString();
                    this.StateTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.StateColumn].ToString();
                    this.ZipTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.ZipColumn].ToString();
                    this.TitleTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.TitleColumn].ToString();
                    this.PhoneNumberTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.PhoneColumn].ToString();
                    this.EmailTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.EmailColumn].ToString();
                    this.NoteTextBox.Text = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.NoteColumn].ToString();
                    this.ActiveComboBox.SelectedValue = this.cashAccountManagement.GetInstitutionContact.Rows[0][this.cashAccountManagement.GetInstitutionContact.IsActiveColumn];
                    ////set attachment and comments count
                    this.SetAdditionalOperationCount();
                    ////set permission
                    this.SetFieldsPermission();                    
                }
                else
                {
                    this.ClearInstitutionContact();
                    ////disable panel
                    this.ContactPanel.Enabled = false;
                }

                ////reset pageLoadStatus - trigger textchanged event
                this.pageLoadStatus = false;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {                
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Clear InstitutionContact Detail

        /// <summary>
        /// Method will Clear the Institution Contact
        /// </summary>       
        private void ClearInstitutionContact()
        {
            this.ContactIdTextBox.Text = String.Empty;
            this.NameTextBox.Text = String.Empty;
            this.Address1TextBox.Text = String.Empty;
            this.Address2TextBox.Text = String.Empty;
            this.CityTextBox.Text = String.Empty;
            this.StateTextBox.Text = String.Empty;
            this.ZipTextBox.Text = String.Empty;
            this.TitleTextBox.Text = String.Empty;
            this.PhoneNumberTextBox.Text = String.Empty;
            this.EmailTextBox.Text = String.Empty;
            this.NoteTextBox.Text = String.Empty;
            ////for new record, display with default value
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.ActiveComboBox.SelectedIndex = -1;
            }

            ////disable attachment and comments
            this.AttachmentButton.Enabled = false;
            this.CommentButton.Enabled = false;
        }

        #endregion

        #region Save

        /// <summary>
        /// Handles the Click event of the SaveContactButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveContactButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SaveContactButton.Enabled)
                {
                    this.SaveContactButton.Focus();
                    ////Check For Required Fields
                    if (String.IsNullOrEmpty(this.NameTextBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.NameTextBox.Focus();
                        return;
                    }

                    if (this.ActiveComboBox.SelectedIndex < 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ActiveComboBox.Focus();
                        return;
                    }

                    if (!String.IsNullOrEmpty(this.EmailTextBox.Text.Trim()) && !Regex.IsMatch(EmailTextBox.Text.Trim(), this.validEmail))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("EmailValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.EmailTextBox.Focus();
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;

                    ////insert/update cash account

                    this.cashAccountManagement.SaveInstitutionContact.Rows.Clear();
                    F1530CashAccountManagementData.SaveInstitutionContactRow institutionContactDataRow = this.cashAccountManagement.SaveInstitutionContact.NewSaveInstitutionContactRow();

                    institutionContactDataRow.InstitutionID = this.institutionId;
                    institutionContactDataRow.Name = this.NameTextBox.Text.Trim();
                    institutionContactDataRow.Address1 = this.Address1TextBox.Text.Trim();
                    institutionContactDataRow.Address2 = this.Address2TextBox.Text.Trim();
                    institutionContactDataRow.City = this.CityTextBox.Text.Trim();
                    institutionContactDataRow.State = this.StateTextBox.Text.Trim();
                    institutionContactDataRow.Zip = this.ZipTextBox.Text.Trim();
                    institutionContactDataRow.Title = this.TitleTextBox.Text.Trim();
                    institutionContactDataRow.Phone = this.PhoneNumberTextBox.Text.Trim();
                    institutionContactDataRow.Email = this.EmailTextBox.Text.Trim();
                    institutionContactDataRow.Note = this.NoteTextBox.Text.Trim();
                    ////SelectedValue datatype is byte
                    institutionContactDataRow.IsActive = Convert.ToByte(this.ActiveComboBox.SelectedValue);

                    this.cashAccountManagement.SaveInstitutionContact.Rows.Add(institutionContactDataRow);

                    ////Save Institution contact record - if -999 then insert else update, validated in dal - returns saved/updated contactId
                    this.contactId = this.form1532Control.WorkItem.F1532_SaveInstitutionContact(this.contactId, Utility.GetXmlString(this.cashAccountManagement.SaveInstitutionContact.Copy()), TerraScanCommon.UserId);

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

        #region Attachment and Comment

        /// <summary>
        /// Handles the Click event of the AttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////1532 - current form id
                object[] optionalParameter = new object[] { this.formId, this.contactId, this.formId };

                Form attachmentForm = new Form();
                ////9005 - attachment form no
                attachmentForm = this.form1532Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.form1532Control.WorkItem);
                if (attachmentForm != null)
                {
                    attachmentForm.ShowDialog();

                    // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                    AdditionalOperationCountEntity additionalOperationCountEnt;
                    additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                    additionalOperationCountEnt.AttachmentCount = Convert.ToInt32(TerraScanCommon.GetValue(attachmentForm, "AttachmentCount"));
                    this.SetText(additionalOperationCountEnt);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the CommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////1532 - current form id
                object[] optionalParameter = new object[] { this.formId, this.contactId, this.formId };
                ////9075 - comment form no
                Form commentForm = this.form1532Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.form1532Control.WorkItem);
                if (commentForm != null)
                {
                    commentForm.ShowDialog();

                    // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                    AdditionalOperationCountEntity additionalOperationCountEnt;
                    additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                    additionalOperationCountEnt.CommentCount = Convert.ToInt32(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                    additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                    this.SetText(additionalOperationCountEnt);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the attachment and comments count text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
            ////if not -999 reset text
            if (additionalOperationCountEntity.AttachmentCount != -999)
            {
                if (additionalOperationCountEntity.AttachmentCount <= 0)
                {
                    this.AttachmentButton.Text = SharedFunctions.GetResourceString("Attachment");
                }
                else
                {
                    this.AttachmentButton.Text = string.Concat(SharedFunctions.GetResourceString("Attachment"), "(", additionalOperationCountEntity.AttachmentCount, ")");
                }
            }

            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentButton.Text = SharedFunctions.GetResourceString("Comment");
                }
                else
                {
                    this.CommentButton.Text = this.CommentButton.Text = string.Concat(SharedFunctions.GetResourceString("Comment"), "(", additionalOperationCountEntity.CommentCount, ")");
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    ////red color for high priority 
                    this.CommentButton.BackColor = Color.FromArgb(255, 0, 0);
                    this.CommentButton.CommentPriority = true;
                }
                else
                {
                    ////default brown color
                    this.CommentButton.BackColor = Color.FromArgb(174, 150, 94);
                    this.CommentButton.CommentPriority = false;
                }
            }
        }

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        private void SetAdditionalOperationCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                ////check for valid registerid
                if (this.contactId != -999)
                {
                    additionalOperationCountEntity.AttachmentCount = this.form1532Control.WorkItem.GetAttachmentCount(this.formId, this.contactId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1532Control.WorkItem.GetCommentsCount(this.formId, this.contactId, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.SetText(additionalOperationCountEntity);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {                
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region User Defined Funtion

        /// <summary>
        /// This Method used to load combobox datasource
        /// LoadComboBox
        /// </summary>
        private void LoadComboBox()
        {
            ////customize active combobox
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            ////load active combobox
            this.ActiveComboBox.DataSource = commonData.ComboBoxDataTable.Copy();
            this.ActiveComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.ActiveComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {           
            this.NameTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.NameColumn.MaxLength;
            this.Address1TextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.Address1Column.MaxLength;
            this.Address2TextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.Address2Column.MaxLength;
            this.CityTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.CityColumn.MaxLength;
            this.StateTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.StateColumn.MaxLength;
            this.ZipTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.ZipColumn.MaxLength;
            this.TitleTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.TitleColumn.MaxLength;
            this.PhoneNumberTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.PhoneColumn.MaxLength;
            this.EmailTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.EmailColumn.MaxLength;
            this.NoteTextBox.MaxLength = this.cashAccountManagement.GetInstitutionContact.NoteColumn.MaxLength;            
        }

        /// <summary>
        /// Handles the TextChanged event of the EditTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditControl_ValueChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus && !this.SaveContactButton.Enabled)
            {
                this.SaveContactButton.Enabled = true;
            }
        }

        /// <summary>
        /// Sets the fields permission - set edit permission.
        /// </summary>
        private void SetFieldsPermission()
        {
            ////lock text box control
            this.NameTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.Address1TextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.Address2TextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.CityTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.StateTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.ZipTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.TitleTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.PhoneNumberTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.EmailTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.NoteTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            ////enable or disable            
            this.ActiveComboBox.Enabled = this.EditPermissionButton.ActualPermission;
        }

        #endregion        

        #endregion      
    }
}