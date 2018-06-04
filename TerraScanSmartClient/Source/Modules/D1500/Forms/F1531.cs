//--------------------------------------------------------------------------------------------
// <copyright file="F1531.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1531.
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
    /// F1531 Form
    /// </summary>
    public partial class F1531 : BasePage
    {
        #region Private Variables

        /// <summary>
        /// form1531Control Variable
        /// </summary>
        private F1531Controller form1531Control;

        /// <summary>
        /// DataSet Contains Institution Detail 
        /// </summary>
        private F1530CashAccountManagementData cashAccountManagement = new F1530CashAccountManagementData();

        /// <summary>
        /// pageLoadStatus variable is used to find the pageLoadStatus. 
        /// </summary>   
        private bool pageLoadStatus;

        /// <summary>
        /// registerId variable is used to store registerId. - default value - '-999'(invalid value)
        /// </summary>   
        private int registerId = -999;        

        /// <summary>
        /// institutionId variable is used to store institutionId. - default value - '-999'(invalid value)
        /// </summary>   
        private int institutionId = -999;

        /// <summary>
        /// registerCount variable is used to register counts
        /// </summary>   
        private int registerCount;  

        /// <summary>
        /// formId variable is used to store current form id.
        /// </summary>   
        private int formId = 1531;

        /// <summary>
        /// subFundId variable is used to store subFundId. - default value - '-999'(invalid value)
        /// </summary>   
        private int subFundId = -999;

        /// <summary>
        /// subFundModified variable is used to differentiate the load value and entered value
        /// </summary>   
        private bool subFundModified;

        /// <summary>
        /// pageMode variable is find form action mode - whether view, new
        /// </summary>   
        private TerraScanCommon.PageModeTypes pageMode;

        #endregion   

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1531"/> class.
        /// </summary>
        public F1531()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1531"/> class with parameters
        /// </summary>
        /// <param name="parentFormId">The parent form id.</param>
        /// <param name="pageModeType">Type of the page mode.</param>
        /// <param name="institutionid">The institutionid.</param>
        /// <param name="registerId">The register id.</param>
        /// <param name="institutionName">Name of the institution.</param>
        /// <param name="registerCount">The register count.</param>
        public F1531(int parentFormId, TerraScanCommon.PageModeTypes pageModeType, int institutionid, int registerId, string institutionName, int registerCount)
        {
            this.InitializeComponent();                     
            ////assign default value
            this.ParentFormId = parentFormId;
            this.institutionId = institutionid;
            this.pageMode = pageModeType;
            this.registerId = registerId;
            this.registerCount = registerCount;
            ////SetMaxLength for editable fields   
            this.SetMaxLength();   
            ////set short cut key
            this.CancelButton = this.CancelAccountButton;            
            this.SaveMenuToolStripMenuItem.Click += new EventHandler(this.SaveAccountButton_Click);
            ////Set form name
            this.Text = string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("CashAccountName"), institutionName);
        }

        #endregion   

        #region Properties

        /// <summary>
        /// Gets or sets the F1531 control.
        /// </summary>
        /// <value>The F1531 control.</value>
        [CreateNew]
        public F1531Controller F1531Control
        {
            get { return this.form1531Control as F1531Controller; }
            set { this.form1531Control = value; }
        }

        /// <summary>
        /// Gets or sets the register id.
        /// </summary>
        /// <value>The register id.</value>
        public int RegisterId
        {
            get { return this.registerId; }
            set { this.registerId = value; }
        }

        #endregion
    
        #region Form Load   

        /// <summary>
        /// Handles the Load event of the F1531 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1531_Load(object sender, EventArgs e)
        {
            ////set pageLoadStatus - suppress textchanged event
            this.pageLoadStatus = true;
            ////load comboboxes
            this.LoadComboBox();
            ////pagemode will contain add or view
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                ////load in view mode                
                this.GetCashAccountDetail();
                this.SaveAccountButton.Enabled = false;
            }
            else
            {
                ////load in new mode
                this.ClearCashAccount();
                this.SaveAccountButton.Enabled = true;               
            }

            ////reset pageLoadStatus - trigger textchanged event
            this.pageLoadStatus = false;
            ////set default focus
            this.AccountNameTextBox.Focus();
        }

        #endregion   
 
        #region Private Methods

        #region Get Cash Account

        /// <summary>
        /// Gets the cashAccount detail
        /// </summary>
        private void GetCashAccountDetail()
        {
            try
            {                
                this.cashAccountManagement.Clear();
                this.cashAccountManagement = this.F1531Control.WorkItem.F1531_GetCashAccountDetail(this.registerId);

                if (this.cashAccountManagement.GetCashAccount.Rows.Count > 0)
                {
                    this.RegisterIdTextBox.Text = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.RegisterIDColumn].ToString();
                    this.AccountNameTextBox.Text = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.AccountNameColumn].ToString();
                    this.RegisterTypeComboBox.SelectedValue = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.RegisterTypeIDColumn];
                    this.AccountingSubFundTextBox.Text = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.SubFundColumn].ToString();
                    ////check for subfund
                    if (!string.IsNullOrEmpty(this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.SubFundIDColumn].ToString()))
                    {
                        this.subFundId = Convert.ToInt32(this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.SubFundIDColumn]);
                    }

                    this.AccountNumberTextBox.Text = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.AccountNumberColumn].ToString();
                    this.ActiveComboBox.SelectedValue = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.IsActiveColumn].ToString();
                    this.DefaultAccountComboBox.SelectedValue = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.IsDefaultColumn].ToString();
                    this.DescriptionTextBox.Text = this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.DescriptionColumn].ToString();
                    ////set attachment and comments count
                    this.SetAdditionalOperationCount();
                    ////set permission
                    this.SetFieldsPermission();
                }
                else
                {
                    this.ClearCashAccount();
                    ////disable panel
                    this.RegisterPanel.Enabled = false;
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
        }

        #endregion

        #region Clear Cash Account Detail

        /// <summary>
        /// Method will Clear the Cash Account
        /// </summary>       
        private void ClearCashAccount()
        {
            ////Cash Account related fields
            this.RegisterIdTextBox.Text = String.Empty;
            this.AccountNameTextBox.Text = String.Empty;            
            this.AccountingSubFundTextBox.Text = String.Empty;
            this.AccountNumberTextBox.Text = String.Empty;
            this.DescriptionTextBox.Text = String.Empty;
            ////for new record, display with default value
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.RegisterTypeComboBox.SelectedIndex = -1;
                this.ActiveComboBox.SelectedIndex = -1;
                this.DefaultAccountComboBox.SelectedIndex = -1;
            }
            else
            {
                ////if first record - setdefault to yes else no
                if (this.registerCount == 0)
                {
                    this.DefaultAccountComboBox.SelectedValue = 1;
                }
                else
                {
                    this.DefaultAccountComboBox.SelectedValue = 0;
                }
            }

            ////disable attachment and comments
            this.AttachmentButton.Enabled = false;
            this.CommentButton.Enabled = false;
        }

        #endregion

        #region Save 

        /// <summary>
        /// Handles the Click event of the SaveAccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SaveAccountButton.Enabled)
                {
                    this.SaveAccountButton.Focus();
                    ////Check For Required Fields
                    if (String.IsNullOrEmpty(this.AccountNameTextBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.AccountNameTextBox.Focus();
                        return;
                    }

                    if (this.ActiveComboBox.SelectedIndex < 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ActiveComboBox.Focus();
                        return;
                    }

                    if (this.RegisterTypeComboBox.SelectedIndex < 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.RegisterTypeComboBox.Focus();
                        return;
                    }

                    ////Check For Sub Fund Required Fields - description exist but subfund not
                    if (String.IsNullOrEmpty(this.AccountingSubFundTextBox.Text.Trim()) && !String.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
                    {
                        ////prompt warninig else proceed
                        if (MessageBox.Show(SharedFunctions.GetResourceString("SubFundDescriptionRequirement"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            this.AccountingSubFundTextBox.Focus();
                            return;
                        }
                    }

                    this.Cursor = Cursors.WaitCursor;

                    ////insert/update cash account

                    this.cashAccountManagement.SaveCashAccount.Rows.Clear();
                    F1530CashAccountManagementData.SaveCashAccountRow cashAccountDataRow = this.cashAccountManagement.SaveCashAccount.NewSaveCashAccountRow();

                    cashAccountDataRow.InstitutionID = this.institutionId;
                    cashAccountDataRow.AccountName = this.AccountNameTextBox.Text.Trim();
                    ////this.RegisterTypeComboBox.SelectedValue datatype is int
                    cashAccountDataRow.RegisterTypeID = Convert.ToInt32(this.RegisterTypeComboBox.SelectedValue);
                    if (!string.IsNullOrEmpty(this.AccountingSubFundTextBox.Text.Trim()))
                    {
                        cashAccountDataRow.SubFund = this.AccountingSubFundTextBox.Text.Trim();
                        ////check for subfund validation
                        if (this.subFundId != -999)
                        {
                            cashAccountDataRow.SubFundID = this.subFundId;
                        }
                    }

                    cashAccountDataRow.AccountNumber = this.AccountNumberTextBox.Text.Trim();
                    ////SelectedValue datatype is bool
                    cashAccountDataRow.IsActive = Convert.ToByte(this.ActiveComboBox.SelectedValue);
                    cashAccountDataRow.IsDefault = Convert.ToByte(this.DefaultAccountComboBox.SelectedValue);
                    cashAccountDataRow.Description = this.DescriptionTextBox.Text.Trim();

                    this.cashAccountManagement.SaveCashAccount.Rows.Add(cashAccountDataRow);

                    ////save register details and returns the regiterid if save succeed, else return negative value
                    int returnValue = this.form1531Control.WorkItem.F1531_SaveCashAccount(this.registerId, Utility.GetXmlString(this.cashAccountManagement.SaveCashAccount.Copy()), TerraScanCommon.UserId);
                    ////if subfund alreary exist return nagative(invalid) value else register id
                    if (returnValue < 0)
                    {
                        MessageBox.Show(String.Concat(SharedFunctions.GetResourceString("SubFundExistence1"), this.AccountingSubFundTextBox.Text.Trim(), SharedFunctions.GetResourceString("SubFundExistence2")), String.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SubFundExistenceHeader")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.AccountingSubFundTextBox.Focus();
                        return;
                    }

                    ////assign validated registerId
                    this.registerId = returnValue;

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
                ////1531 - current form id
                object[] optionalParameter = new object[] { this.formId, this.registerId, this.formId };

                Form attachmentForm = new Form();
                ////9005 - attachment form no
                attachmentForm = this.form1531Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.form1531Control.WorkItem);
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
                ////1531 - current form id
                object[] optionalParameter = new object[] { this.formId, this.registerId, this.formId };
                ////9075 - comment form no
                Form commentForm = this.form1531Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.form1531Control.WorkItem);
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
                if (this.registerId != -999)
                {
                    additionalOperationCountEntity.AttachmentCount = this.form1531Control.WorkItem.GetAttachmentCount(this.formId, this.registerId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1531Control.WorkItem.GetCommentsCount(this.formId, this.registerId, TerraScanCommon.UserId);
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {                
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
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
            try
            {
                ////customize RegisterType combobox - loads RegisterType to RegisterTypeComboBox
                AccountManagementData accountManagement = new AccountManagementData();
                accountManagement = this.form1531Control.WorkItem.F1500_ListRegisterType();
                this.RegisterTypeComboBox.DataSource = accountManagement.ListRegisterType;
                this.RegisterTypeComboBox.ValueMember = accountManagement.ListRegisterType.RegisterTypeIDColumn.ToString();
                this.RegisterTypeComboBox.DisplayMember = accountManagement.ListRegisterType.RegisterTypeColumn.ToString();

                ////customize active combobox
                CommonData commonData = new CommonData();
                ////which loads yes, no value to the ComboBoxDataTable
                commonData.LoadYesNoValue();
                ////load active combobox
                this.ActiveComboBox.DataSource = commonData.ComboBoxDataTable.Copy();
                this.ActiveComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
                this.ActiveComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
                ////load default combobox
                this.DefaultAccountComboBox.DataSource = commonData.ComboBoxDataTable.Copy();
                this.DefaultAccountComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
                this.DefaultAccountComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {                
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.AccountNameTextBox.MaxLength = this.cashAccountManagement.GetCashAccount.AccountNameColumn.MaxLength;
            this.AccountingSubFundTextBox.MaxLength = this.cashAccountManagement.GetCashAccount.SubFundColumn.MaxLength;
            this.AccountNumberTextBox.MaxLength = this.cashAccountManagement.GetCashAccount.AccountNumberColumn.MaxLength;
            this.DescriptionTextBox.MaxLength = this.cashAccountManagement.GetCashAccount.DescriptionColumn.MaxLength;            
        }

        /// <summary>
        /// Handles the TextChanged event of the EditTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditControl_ValueChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus)
            {
                if (Object.Equals(sender, AccountingSubFundTextBox))
                {
                    this.subFundModified = true;
                }

                this.SaveAccountButton.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Validating event of the AccountingSubFundTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void AccountingSubFundTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ////check with last entered subfund
                if (!String.IsNullOrEmpty(this.AccountingSubFundTextBox.Text.Trim()) && this.subFundModified)
                {
                    this.subFundModified = false;
                    F9503SubFundManagementData subFundManagement = new F9503SubFundManagementData();
                    ////dbcall for subfund - rollyear default 0 for cash account
                    subFundManagement = this.form1531Control.WorkItem.F9503_GetSubFundItems(this.AccountingSubFundTextBox.Text.Trim(), 0);

                    ////get description and sunfund
                    if (subFundManagement.getSubFundItems.Rows.Count > 0)
                    {
                        this.DescriptionTextBox.Text = subFundManagement.getSubFundItems.Rows[0][subFundManagement.getSubFundItems.DescriptionColumn].ToString();
                        this.subFundId = Convert.ToInt32(subFundManagement.getSubFundItems.Rows[0][subFundManagement.getSubFundItems.SubFundIDColumn]);
                    }
                    else
                    {
                        ////assign invalid value
                        this.subFundId = -999;
                    }
                }             
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (InvalidCastException ex)
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

        /// <summary>
        /// Sets the fields permission - set edit permission.
        /// </summary>
        private void SetFieldsPermission()
        {           
            ////lock text box control
            this.AccountNameTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.AccountingSubFundTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.AccountNumberTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.DescriptionTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;                   
            ////enable or disable 
            this.RegisterTypeComboBox.Enabled = this.EditPermissionButton.ActualPermission;
            this.ActiveComboBox.Enabled = this.EditPermissionButton.ActualPermission;
            this.DefaultAccountComboBox.Enabled = this.EditPermissionButton.ActualPermission;
        }

        #endregion   

        #endregion             
    }
}