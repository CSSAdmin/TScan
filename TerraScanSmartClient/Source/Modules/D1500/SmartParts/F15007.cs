//--------------------------------------------------------------------------------------------
// <copyright file="F15007.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F15007 Form Slice - Account Management 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22-12-2006       Krishna Abburi      Created
// 12-03-2007       Shiva               Applyed Permissions Properly
// 3rd March        D.Ramya              
//*********************************************************************************/

namespace D1500
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
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    /// <summary>
    /// F15007 FormSlice - Accout Mgmt Functionality
    /// </summary>
    [SmartPart]
    public partial class F15007 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// subFundManagementData
        /// </summary>
        private F9503SubFundManagementData subFundManagementData = new F9503SubFundManagementData();

        /// <summary>
        /// accountMangamentData Variable 
        /// </summary>
        private AccountManagementData getdescriptionDataset = new AccountManagementData();

        /// <summary>
        /// listAccountDetailsDataset Variable 
        /// </summary>
        private AccountManagementData listAccountDetailsDataset = new AccountManagementData();

        /// <summary>
        /// variable Holds the F15005 Controller instance
        /// </summary>
        private F15007Controller form15007Controll;

        /// <summary>
        /// Variable Holds the PageMode Types
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// variable Holds the edit permission value
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// variable holds the masterForm Number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// variable holds the KeyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// variable holds the flag
        /// </summary>
        private int flag;

        /// <summary>
        /// variable holds the slice permissionFields
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Variable Holds the flag Value for PageLoad
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// institutionId variable is used to store institution id. - default value - null
        /// </summary>       
        private int? accountId = null;

        /// <summary>
        /// formload
        /// </summary>
        private bool formload = true;

        ///// <summary>
        ///// configurationValue
        ///// </summary>
        ////private bool configurationValue;

        /// <summary>
        /// rollYear
        /// </summary>
        private short rollYear;

        /// <summary>
        /// subFundId
        /// </summary>
        private int subFundId;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15007"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15007(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.DepositHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DepositHistoryPictureBox.Height, this.DepositHistoryPictureBox.Width, "Account Detail", 28, 81, 128);
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15007"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F15007(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.DepositHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DepositHistoryPictureBox.Height, this.DepositHistoryPictureBox.Width, "Account Detail", 28, 81, 128);
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled; ////ramya D

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /////// <summary>
        /////// Declare the event FormSlice_FormCloseAlert        
        /////// </summary>
        ////[EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /////// <summary>
        /////// event publication for panel link label click
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form15007 controll.
        /// </summary>
        /// <value>The form15007 controll.</value>
        [CreateNew]
        public F15007Controller Form15007Controll
        {
            get { return this.form15007Controll as F15007Controller; }
            set { this.form15007Controll = value; }
        }

        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        /// <value>The account id.</value>
        private int? AccountId
        {
            get
            {
                return this.accountId;
            }

            set
            {
                this.accountId = value;
            }
        }

        /// <summary>
        /// Gets or sets the sub fund id.
        /// </summary>
        /// <value>The sub fund id.</value>
        private int SubFundId
        {
            get
            {
                return this.subFundId;
            }

            set
            {
                this.subFundId = value;
            }
        }

        /// <summary>
        /// Gets or sets the roll year.
        /// </summary>
        /// <value>The roll year.</value>
        private short RollYear
        {
            get
            {
                return this.rollYear;
            }

            set
            {
                this.rollYear = value;
            }
        }

        #endregion

        #region Event SubScription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    ////to check for invalid key id 
                    if (this.keyId != eventArgs.Data.KeyId)
                    {
                        this.keyId = eventArgs.Data.KeyId;
                        this.listAccountDetailsDataset = this.form15007Controll.WorkItem.F1500_ListAccountDetails(this.keyId);
                    }

                    if ((this.listAccountDetailsDataset.ListAccountDetails.Rows.Count > 0) && (this.keyId != 0))
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }

                //// Code Added by Shiva for Permissions
                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    if (this.listAccountDetailsDataset.ListAccountDetails.Rows.Count > 0)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
                else
                {
                    this.LockControls(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit) || (this.pageMode == TerraScanCommon.PageModeTypes.New))
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit) || (this.pageMode == TerraScanCommon.PageModeTypes.New))
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    bool status = this.SaveAccountRecord(false);
                    if (status)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
            }
            else
            {
                //// ToDo : FormLoad Events should happen (refresh)
                this.LockControls(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.pageLoadStatus = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.LockControls(false);
                    this.ClearAccountDetails();
                    this.EnableControls(true);
                    this.IntializeCombo();
                    this.GetYear();
                    this.PendingCombo.SelectedValue = 0;
                    this.ActiveCombo.SelectedValue = 1;
                    this.FunctionTypeCombo.SelectedValue = 2;
                    this.PendingCombo.SelectedValue = 0;
                    this.accountId = null;
                    this.pageLoadStatus = false;
                    this.CopyAccountButton.Enabled = false;  
                    this.RollYearTextBox.Focus();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.LockControls(true);
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
                this.EnableControls(true);
                if (RollYearTextBox.Text == "0")
                {
                    RollYearTextBox.Enabled = false;
                }
                else
                {
                    RollYearTextBox.Enabled = true;
                }
            }
            else
            {
                this.LockControls(true);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.CopyAccountButton.Enabled = true;
            this.accountId = Convert.ToInt32(this.keyId);
            this.GetAccountDetails(this.keyId);
            this.RollYearTextBox.Select();
            if (this.RollYearTextBox.Text == "0" || this.RollYearTextBox.Text == string.Empty)
            {
                this.RollYearTextBox.Enabled = false;
                this.RollYearPanel.BackColor = Color.White;
                this.RollYearTextBox.BackColor = Color.White;
            }
            else
            {
                this.RollYearTextBox.Enabled = true;
                this.RollYearPanel.Focus();
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.GetAccountDetails(this.keyId);

                    //// Code Added By Shiva for Permissions
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                        this.RollYearTextBox.Enabled = false;
                    }
                    else
                    {
                        this.LockControls(true);
                    }

                    if (RollYearTextBox.Text == "0")
                    {
                        this.RollYearTextBox.Enabled = false;
                    }
                    else
                    {
                        this.RollYearTextBox.Enabled = true;
                    }
                    this.CopyAccountButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F15007 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15007_Load(object sender, EventArgs e)
        {
            try
            {
                ////this.RollYearTextBox.Enabled = false;
                this.FlagSliceForm = true;
                this.IntializeCombo();
                this.AccountId = Convert.ToInt32(this.keyId);
                this.GetAccountDetails(this.keyId);
                this.EnableControls(true);
                ////this.DisableButtonBasedOnConfigValues();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetAutoCompleteForSubFund();
                this.AutoCompleteFcBarLineOBj();
                this.flag = 0;
                if (this.RollYearTextBox.Text == "0")
                {
                    RollYearTextBox.Enabled = false;
                }
                else
                {
                    RollYearTextBox.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////if (!string.IsNullOrEmpty(RollYearTextBox.Text))
                ////{
                if (!this.formload)
                {
                    if (this.pageMode != TerraScanCommon.PageModeTypes.New)
                    {
                        this.SeteditrProcess();
                    }

                    this.SubFundTextChanged();
                }

                this.SetColorOfComboBox();
                this.SetAutoCompleteForSubFund();
                ////}                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SubFundButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form subfundForm = new Form();
                short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                object[] optionalParameter = new object[] { this.rollYear };
                subfundForm = this.form15007Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1515, optionalParameter, this.form15007Controll.WorkItem);
                if (subfundForm != null)
                {
                    DialogResult dlgResult;
                    dlgResult = subfundForm.ShowDialog();
                    if (dlgResult.Equals(DialogResult.OK))
                    {
                        this.SubFundTextBox.Text = TerraScanCommon.GetValue(subfundForm, "SubFundItem").ToString();
                        this.SubFundTextBox.ForeColor = Color.Black;
                        if (!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()))
                        {
                            this.subFundManagementData = this.form15007Controll.WorkItem.F9503_GetSubFundItems(this.SubFundTextBox.Text.Trim(), this.rollYear);
                            if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                            {
                                this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                                this.subFundId = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                                if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "False")
                                {
                                    this.AccounTypeTextBox.Text = "Fund";
                                    this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                                    this.RollYearTextBox.Enabled = true;
                                }
                                else if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "True")
                                {
                                    this.AccounTypeTextBox.Text = "Cash";
                                    this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                                    //// Modified By Ramya.D

                                    if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() != this.RollYearTextBox.Text.Trim())
                                    {
                                        if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() == "0")
                                        {
                                            RollYearTextBox.Text = "0";
                                            this.SubFundTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["SubFund"].ToString();
                                            this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                                        }
                                        else
                                        {
                                            this.SubFundTextBox.ForeColor = Color.DarkRed;
                                            this.SubFundDescTextBox.Text = string.Empty;
                                            this.AccounTypeTextBox.Text = string.Empty;
                                            this.subFundId = 0;
                                        }
                                    }
                                    ////---------------End-----------------------
                                }
                                else
                                {
                                    this.AccounTypeTextBox.Text = string.Empty;
                                }
                            }
                            else
                            {
                                this.subFundId = 0;
                                this.SubFundTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                            }
                        }
                    }
                    else 
                    {
                        if (!this.ParentForm.MdiParent.ActiveMdiChild.Text.Equals(this.ParentForm.Text))
                        {
                            SendKeys.Send("^{TAB}");
                            SendKeys.Send("^+{TAB}");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FunctionButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FunctionButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////string keyName = string.Empty;
                Form functionForm = new Form();
                ////object[] optionalParameter = new object[] { keyName };
                functionForm = TerraScanCommon.GetForm(1502, null, this.form15007Controll.WorkItem);
                if (functionForm != null)
                {
                    if (functionForm.ShowDialog() == DialogResult.OK)
                    {
                        ///// Modified by Ramya
                        if (this.FunctionTextBox.Text == TerraScanCommon.GetValue(functionForm, "FunctionIdValue").ToString())
                        {
                            this.flag = 1;
                        }
                        else
                        {
                            this.flag = 0;
                        }

                        this.FunctionTextBox.Text = TerraScanCommon.GetValue(functionForm, "FunctionIdValue").ToString();
                        this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetFunctionItems(this.FunctionTextBox.Text.Trim());
                        if (this.getdescriptionDataset.GetFunctionItems.Rows.Count > 0)
                        {
                            this.FunctionDescTextBox.Text = this.getdescriptionDataset.GetFunctionItems.Rows[0]["Description"].ToString();
                            this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);

                            if (this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"].ToString() != "")
                            {
                                this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);
                                this.FunctionTypeCombo.Enabled = false;
                            }

                            this.FunctionDescTextBox.ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the SubFundTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                ////this.SubFundTextChanged();
                this.SubFundTextLeave();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the FunctionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FunctionTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.FunctionTextBoxChanged();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BarsTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BarsTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string barId = string.Empty;
                barId = this.BarsTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(barId))
                {
                    this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetDescription(barId, "Bar");
                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                    {
                        this.BarsDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.BarsDescTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.BarsDescTextBox.Text = string.Empty;
                        this.BarsDescTextBox.Enabled = true;
                        this.BarsDescTextBox.LockKeyPress = false;
                    }
                }
                else
                {
                    this.BarsDescTextBox.Text = "";
                    this.BarsDescTextBox.ReadOnly = true;
                    this.BarsDescTextBox.LockKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ObjectTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ObjectTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string objectId = string.Empty;
                objectId = this.ObjectTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(objectId))
                {
                    this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetDescription(objectId, "Object");
                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                    {
                        this.ObjectDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.ObjectDescTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.ObjectDescTextBox.Text = "";
                        this.ObjectDescTextBox.Enabled = true;
                        this.ObjectDescTextBox.LockKeyPress = false;
                    }
                }
                else
                {
                    this.ObjectDescTextBox.Text = "";
                    this.ObjectDescTextBox.LockKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LineTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LineTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string lineId = string.Empty;
                lineId = this.LineTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(lineId))
                {
                    this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetDescription(lineId, "Line");
                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                    {
                        this.LineDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.LineDescTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.LineDescTextBox.Text = "";
                        this.LineDescTextBox.Enabled = true;
                        this.LineDescTextBox.LockKeyPress = false;
                    }
                }
                else
                {
                    this.LineDescTextBox.Text = string.Empty;
                    this.LineDescTextBox.LockKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.formload && string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    this.GetYear();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Clears the account details.
        /// </summary>
        private void ClearAccountDetails()
        {
            this.AccountIDTextBox.Text = string.Empty;
            this.AccounTypeTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.PendingCombo.Text = string.Empty;
            this.ActiveCombo.Text = string.Empty;
            this.AccountNoTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.SubFundTextBox.Text = string.Empty;
            this.FunctionTextBox.Text = string.Empty;
            this.BarsTextBox.Text = string.Empty;
            this.ObjectTextBox.Text = string.Empty;
            this.LineTextBox.Text = string.Empty;
            this.SubFundDescTextBox.Text = string.Empty;
            this.FunctionDescTextBox.Text = string.Empty;
            this.BarsDescTextBox.Text = string.Empty;
            this.ObjectDescTextBox.Text = string.Empty;
            this.LineDescTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Intializes the combo.
        /// </summary>
        private void IntializeCombo()
        {
            ////customize active combobox
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValueUpperCase();
            this.PendingCombo.DataSource = commonData.ComboBoxDataTable;
            this.PendingCombo.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.PendingCombo.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
            CommonData commonData2 = new CommonData();
            commonData2.LoadYesNoValueUpperCase();
            this.ActiveCombo.DataSource = commonData2.ComboBoxDataTable;
            this.ActiveCombo.ValueMember = commonData2.ComboBoxDataTable.KeyIdColumn.ToString();
            this.ActiveCombo.DisplayMember = commonData2.ComboBoxDataTable.KeyNameColumn.ToString();
            ////which loads Balancing,Collection,Disbursement  value to the ComboBoxDataTable
            DataTable workTable = new DataTable("FunctionType");
            DataColumn workCol = workTable.Columns.Add("No", typeof(Int32));
            DataColumn workCol2 = workTable.Columns.Add("Name", typeof(String));
            workCol.AllowDBNull = false;
            workCol.Unique = true;
            workTable.Rows.Add(new Object[] { 1, "Balancing" });
            workTable.Rows.Add(new Object[] { 2, "Collection" });
            workTable.Rows.Add(new Object[] { 3, "Disbursement" });
            this.FunctionTypeCombo.DataSource = workTable;
            this.FunctionTypeCombo.ValueMember = workTable.Columns[0].ToString();
            this.FunctionTypeCombo.DisplayMember = workTable.Columns[1].ToString();
            FunctionTypeCombo.SelectedValue = 2;
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form15007Controll.WorkItem.GetConfigDetails("TR_RollYear");
            this.RollYearTextBox.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void EnableControls(bool enableValue)
        {
            //// Account Info EnableControls
            this.AccountIDTextBox.Enabled = true;
            this.AccounTypeTextBox.ReadOnly = true;
            this.RollYearTextBox.Enabled = enableValue;
            this.PendingCombo.Enabled = enableValue;
            this.ActiveCombo.Enabled = enableValue;
            this.AccountNoTextBox.Enabled = true;
            this.DescriptionTextBox.Enabled = enableValue;
            this.DisableButtonBasedOnConfigValues();
            this.FunctionTypeCombo.Enabled = false;
            this.SubFundTextBox.ForeColor = Color.Black;
            this.SubFundDescTextBox.LockKeyPress = true;
            this.FunctionDescTextBox.LockKeyPress = true;
            this.BarsDescTextBox.LockKeyPress = true;
            this.ObjectDescTextBox.LockKeyPress = true;
            this.LineDescTextBox.LockKeyPress = true;
            if (string.Equals(this.AccounTypeTextBox.Text, "Fund"))
            {
                this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                this.RollYearTextBox.Enabled = true;
                this.RollYearTextBox.Focus();
            }
            else if (string.Equals(this.AccounTypeTextBox.Text, "Cash"))
            {
                this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                //// this.RollYearTextBox.Enabled = false;
                this.PendingCombo.Focus();
            }
        }

        /// <summary>
        /// Disables the button based on config values.
        /// </summary>
        private void DisableButtonBasedOnConfigValues()
        {
            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountFunctionsEnabled")))
            {
                this.FunctionButton.Enabled = true;
                this.FunctionTextBox.Enabled = true;
                this.FunctionDescTextBox.Enabled = true;
            }
            else
            {
                this.FunctionButton.Enabled = false;
                this.FunctionTextBox.Enabled = false;
                this.FunctionDescTextBox.Enabled = false;
            }

            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountBarsEnabled")))
            {
                this.BarsButton.Enabled = true;
                this.BarsTextBox.Enabled = true;
                this.BarsDescTextBox.Enabled = true;
            }
            else
            {
                this.BarsButton.Enabled = false;
                this.BarsTextBox.Enabled = false;
                this.BarsDescTextBox.Enabled = false;
            }

            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountObjectsEnabled")))
            {
                this.ObjectButton.Enabled = true;
                this.ObjectTextBox.Enabled = true;
                this.ObjectDescTextBox.Enabled = true;
            }
            else
            {
                this.ObjectButton.Enabled = false;
                this.ObjectTextBox.Enabled = false;
                this.ObjectDescTextBox.Enabled = false;
            }

            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountLinesEnabled")))
            {
                this.LineButton.Enabled = true;
                this.LineTextBox.Enabled = true;
                this.LineDescTextBox.Enabled = true;
            }
            else
            {
                this.LineButton.Enabled = false;
                this.LineTextBox.Enabled = false;
                this.LineDescTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// Gets the config value.
        /// </summary>
        /// <param name="configurationObjectName">Name of the configuration object.</param>
        /// <returns>config value</returns>
        private bool GetConfigValue(string configurationObjectName)
        {
            this.getdescriptionDataset.GetConfiguration.Clear();
            this.getdescriptionDataset.Merge(this.form15007Controll.WorkItem.F1500_GetConfigurationValue(configurationObjectName));
            if (this.getdescriptionDataset.GetConfiguration.Rows.Count > 0)
            {
                return Convert.ToBoolean(this.getdescriptionDataset.GetConfiguration[0][this.getdescriptionDataset.GetConfiguration.ConfigurationValueColumn.ColumnName].ToString());
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Saves the account record.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>true or false</returns>
        private bool SaveAccountRecord(bool onclose)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string accountXml = this.GetAccountXML();
                int errorStatus;

                try
                {
                    if (string.IsNullOrEmpty(this.AccountIDTextBox.Text.Trim()))
                    {
                        errorStatus = this.form15007Controll.WorkItem.F1500_CreateOrEditAccount(0, accountXml, TerraScanCommon.UserId);
                    }
                    else
                    {
                        errorStatus = this.form15007Controll.WorkItem.F1500_CreateOrEditAccount(Convert.ToInt32(this.AccountIDTextBox.Text), accountXml, TerraScanCommon.UserId);
                    }
               
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = errorStatus;
              
                ////to refresh the master form with the return keyid
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("illegal name character"))
                    {
                        MessageBox.Show("Illegal name character.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);       
                    }
                }
                if (onclose)
                {
                    return true;
                }

                this.SetAutoCompleteForSubFund();
                this.AutoCompleteFcBarLineOBj();
                this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.CopyAccountButton.Enabled = true;
                this.RollYearTextBox.Focus();
                return true;
            }
            catch (Exception ex)
            {
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Gets the account XML.
        /// </summary>
        /// <returns>xml string</returns>
        private string GetAccountXML()
        {
            int tempAccountID = 0;
            string tempAccountXml = string.Empty;
            AccountManagementData saveAccountData = new AccountManagementData();
            AccountManagementData.ListAccountDetailsRow dr = saveAccountData.ListAccountDetails.NewListAccountDetailsRow();
            saveAccountData.ListAccountDetails.Clear();
            short tempRollYear;
            Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
            dr.RollYear = tempRollYear;

            if (!tempAccountID.Equals(0))
            {
                dr.AccountID = Convert.ToInt32(this.AccountIDTextBox.Text.Trim());
            }

            dr.IsPending = Convert.ToBoolean(Convert.ToInt16(this.PendingCombo.SelectedValue.ToString()));
            dr.IsActive = Convert.ToBoolean(Convert.ToInt16(this.ActiveCombo.SelectedValue.ToString()));
            dr.AccountName = (this.AccountNoTextBox.Text.Trim());
            dr.AcctDesc = (this.DescriptionTextBox.Text.Trim());
            if (!string.IsNullOrEmpty(this.AccounTypeTextBox.Text.Trim()))
            {
                if (this.AccounTypeTextBox.Text.Trim() == "Fund")
                {
                    dr.IsCash = false;
                }
                else
                {
                    dr.IsCash = true;
                }
            }

            dr.SubFund = this.SubFundTextBox.Text.Trim();
            dr.SubFundID = this.SubFundId;
            dr.SubFundDesc = this.SubFundDescTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(this.FunctionTypeCombo.Text))
            {
                dr.SemiAnnualCode = Convert.ToByte(this.FunctionTypeCombo.SelectedValue);
            }

            dr.FunctionID = this.FunctionTextBox.Text.Trim();
            dr.FunctionDesc = this.FunctionDescTextBox.Text.Trim();
            dr.BarID = this.BarsTextBox.Text.Trim();
            dr.BarsDesc = this.BarsDescTextBox.Text;
            dr.ObjectID = this.ObjectTextBox.Text.Trim();
            dr.ObjectDesc = this.ObjectDescTextBox.Text.Trim();
            dr.LineID = this.LineTextBox.Text.Trim();
            dr.LineDesc = this.LineDescTextBox.Text.Trim();
            saveAccountData.ListAccountDetails.Rows.Add(dr);
            DataSet tempDataSet = new DataSet("Root");

            tempDataSet.Tables.Add(saveAccountData.ListAccountDetails.Copy());
            tempDataSet.Tables[0].TableName = "Table";

            tempAccountXml = tempDataSet.GetXml();
            return tempAccountXml;
        }

        /// <summary>
        /// Gets the account details.
        /// </summary>
        /// <param name="tempAccountId">The temp account id.</param>
        private void GetAccountDetails(int tempAccountId)
        {
            this.formload = true;
            this.listAccountDetailsDataset.ListAccountDetails.Clear();
            this.listAccountDetailsDataset = this.form15007Controll.WorkItem.F1500_ListAccountDetails(tempAccountId);
            if (this.listAccountDetailsDataset.ListAccountDetails.Rows.Count > 0)
            {
                //// Fill District Info
                this.AccountIDTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["AccountID"].ToString();
                this.RollYearTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["RollYear"].ToString();

                if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsPending"].ToString() != null)
                {
                    this.PendingCombo.SelectedValue = Convert.ToInt16(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsPending"]);
                }

                if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsActive"].ToString() != null)
                {
                    this.ActiveCombo.SelectedValue = Convert.ToInt16(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsActive"]);
                }

                this.AccountNoTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["AccountName"].ToString();
                this.DescriptionTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["AcctDesc"].ToString();
                if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsCash"].ToString().Trim() == "False")
                {
                    this.AccounTypeTextBox.Text = "Fund";
                    this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                }
                else
                {
                    this.AccounTypeTextBox.Text = "Cash";
                    this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                }

                if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SemiAnnualCode"].ToString() != "")
                {
                    this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SemiAnnualCode"]);
                }

                this.SubFundTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFund"].ToString();
                if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFundId"].ToString() != null)
                {
                    this.SubFundId = Convert.ToInt32(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFundId"].ToString());
                }

                this.SubFundDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFundDesc"].ToString();
                this.FunctionTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["FunctionID"].ToString();
                this.FunctionDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["FunctionDesc"].ToString();
                this.BarsTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["BarID"].ToString();
                this.BarsDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["BarsDesc"].ToString();
                this.LineTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["LineID"].ToString();
                this.LineDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["LineDesc"].ToString();
                this.ObjectTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["ObjectID"].ToString();
                this.ObjectDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["ObjectDesc"].ToString();
                this.EnableControls(true);
                this.formload = false;
            }
            else
            {
                this.LockControls(true);
                this.ClearAccountDetails();
            }

            if (!this.formMasterPermissionEdit && !this.slicePermissionField.editPermission)
            {
                this.LockControls(true);
            }
        }

        /// <summary>
        /// Sets the color of combo box.
        /// </summary>
        private void SetColorOfComboBox()
        {
            if (this.PendingCombo.Text == "NO")
            {
                this.PendingCombo.ForeColor = Color.Black;
            }
            else
            {
                this.PendingCombo.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }

            if (this.ActiveCombo.Text == "NO")
            {
                this.ActiveCombo.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else
            {
                this.ActiveCombo.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Subs the fund text changed.
        /// </summary>
        private void SubFundTextChanged()
        {
            string validSubFundId = string.Empty;
            validSubFundId = this.SubFundTextBox.Text;
            if (!string.IsNullOrEmpty(validSubFundId))
            {
                short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);

                this.subFundManagementData = this.form15007Controll.WorkItem.F9503_GetSubFundItems(validSubFundId, this.rollYear);
                if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                {
                    this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                    this.subFundId = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                    this.SubFundTextBox.ForeColor = Color.Black;
                    if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "False")
                    {
                        this.AccounTypeTextBox.Text = "Fund";
                        this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                        this.RollYearTextBox.Enabled = true;
                    }
                    else if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "True")
                    {
                        this.AccounTypeTextBox.Text = "Cash";
                        this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                        ////Modified By Ramya.D
                        if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() != this.RollYearTextBox.Text.ToString().Trim())
                        {
                            this.SubFundTextBox.ForeColor = Color.DarkRed;
                            this.SubFundDescTextBox.Text = string.Empty;
                            this.AccounTypeTextBox.Text = string.Empty;
                            this.subFundId = 0;
                        }
                        ////this.RollYearTextBox.Text = "0";
                        ////this.RollYearTextBox.Enabled = false;

                        ////---------End-------------------------
                    }

                    if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "")
                    {
                        this.AccounTypeTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    this.SubFundTextBox.ForeColor = Color.DarkRed;
                    this.SubFundDescTextBox.Text = string.Empty;
                    this.AccounTypeTextBox.Text = string.Empty;
                    this.subFundId = 0;
                }
            }
            else
            {
                this.SubFundTextBox.ForeColor = Color.DarkRed;
                this.SubFundDescTextBox.Text = string.Empty;
                this.AccounTypeTextBox.Text = string.Empty;
                this.SubFundDescTextBox.LockKeyPress = true;
                this.subFundId = 0;
            }
        }

        /// <summary>
        /// SubFundTextLeave
        /// </summary>
        private void SubFundTextLeave()
        {
            string validSubFundId = string.Empty;
            validSubFundId = this.SubFundTextBox.Text;
            if (!string.IsNullOrEmpty(validSubFundId))
            {
                short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);

                this.subFundManagementData = this.form15007Controll.WorkItem.F9503_GetSubFundItems(validSubFundId, this.rollYear);
                if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                {
                    this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                    this.subFundId = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                    this.SubFundTextBox.ForeColor = Color.Black;
                    if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "False")
                    {
                        this.AccounTypeTextBox.Text = "Fund";
                        this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                        this.RollYearTextBox.Enabled = true;
                    }
                    else if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "True")
                    {
                        this.AccounTypeTextBox.Text = "Cash";
                        this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);

                        if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() != this.RollYearTextBox.Text.Trim())
                        {
                            if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() == "0")
                            {
                                RollYearTextBox.Text = "0";
                                this.SubFundTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["SubFund"].ToString();
                                this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                            }
                            else
                            {
                                this.SubFundTextBox.ForeColor = Color.DarkRed;
                                this.SubFundDescTextBox.Text = string.Empty;
                                this.AccounTypeTextBox.Text = string.Empty;
                                this.subFundId = 0;
                            }
                        }
                        ////Modified By Ramya.D

                        ////this.RollYearTextBox.Text = "0";
                        ////this.RollYearTextBox.Enabled = false;

                        ////---------End-------------------------
                    }

                    if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "")
                    {
                        this.AccounTypeTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    this.SubFundTextBox.ForeColor = Color.DarkRed;
                    this.SubFundDescTextBox.Text = string.Empty;
                    this.AccounTypeTextBox.Text = string.Empty;
                    this.subFundId = 0;
                }
            }
            else
            {
                this.SubFundTextBox.ForeColor = Color.DarkRed;
                this.SubFundDescTextBox.Text = string.Empty;
                this.AccounTypeTextBox.Text = string.Empty;
                this.SubFundDescTextBox.LockKeyPress = true;
                this.subFundId = 0;
            }
        }

        /// <summary>
        /// Functions the text box changed.
        /// </summary>
        private void FunctionTextBoxChanged()
        {
            string functionId = string.Empty;
            functionId = this.FunctionTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(functionId))
            {
                this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetFunctionItems(functionId);
                if (this.getdescriptionDataset.GetFunctionItems.Rows.Count > 0)
                {
                    this.FunctionDescTextBox.Text = this.getdescriptionDataset.GetFunctionItems.Rows[0]["Description"].ToString();

                    if (this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"].ToString() != "")
                    {
                        this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);
                        this.FunctionTypeCombo.Enabled = false;
                    }

                    this.FunctionDescTextBox.LockKeyPress = true;
                }
                else
                {
                    this.FunctionDescTextBox.Text = string.Empty;
                    this.FunctionDescTextBox.Enabled = true;
                    this.FunctionDescTextBox.LockKeyPress = false;
                    this.FunctionTypeCombo.Enabled = true;
                    this.FunctionTypeCombo.SelectedValue = 2;
                }
            }
            else
            {
                this.FunctionDescTextBox.Text = "";
                this.FunctionDescTextBox.LockKeyPress = true;
                this.FunctionTypeCombo.SelectedValue = 2;
                this.FunctionTypeCombo.Enabled = false;

                ////if (this.pageMode != TerraScanCommon.PageModeTypes.New)
                ////{
                ////    this.FunctionTypeCombo.Enabled = false;
                ////}
                ////else
                ////{
                ////    this.FunctionTypeCombo.Enabled = true;
                ////}
            }
        }

        /// <summary>
        /// Fields the edit process.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FieldEditProcess(object sender, EventArgs e)
        {
            if (this.PermissionEdit && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (!this.formload)
                    {
                        if ((this.pageMode != TerraScanCommon.PageModeTypes.New))
                        {
                            if (this.flag != 1)
                            {
                                this.SeteditrProcess();
                            }
                        }
                    }
                }
            }

            this.SetColorOfComboBox();
        }

        /// <summary>
        /// Seteditrs the process.
        /// </summary>
        private void SeteditrProcess()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            this.CopyAccountButton.Enabled = false;  
        }

        /// <summary>
        /// Sets the auto complere.
        /// </summary>
        private void SetAutoCompleteForSubFund()
        {
            AutoCompleteStringCollection sourceSubFund = new AutoCompleteStringCollection();
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text) && (!(RollYearTextBox.NumericTextBoxValue < 1900 || RollYearTextBox.NumericTextBoxValue > 2079)))
            {
                this.rollYear = Convert.ToInt16(this.RollYearTextBox.Text.Trim());
                if (this.rollYear != 0)
                {
                    this.subFundManagementData = this.form15007Controll.WorkItem.F9503_GetSubFundItems(null, this.rollYear);
                    if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                    {
                        this.AssignAutoCompletSouce(this.subFundManagementData.getSubFundItems.Rows, this.subFundManagementData.getSubFundItems.SubFundColumn.ColumnName, this.SubFundTextBox, sourceSubFund);
                    }
                    else
                    {
                        this.SubFundTextBox.AutoCompleteCustomSource = null;
                    }
                }
                else
                {
                    this.SubFundTextBox.AutoCompleteCustomSource = null;
                }
            }
        }

        /// <summary>
        /// Autoes the complete function , bar, lineand Obj.
        /// </summary>
        private void AutoCompleteFcBarLineOBj()
        {
            AutoCompleteStringCollection sourceFunction = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceBar = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceLine = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceObject = new AutoCompleteStringCollection();

            F1503GenericManagementData barsList = new F1503GenericManagementData();
            F1503GenericManagementData objectsList = new F1503GenericManagementData();
            F1503GenericManagementData lineLists = new F1503GenericManagementData();

            lineLists = this.form15007Controll.WorkItem.F1500_GetGenericElementMgmt(null, null, "Objects");
            if (lineLists.GetGenericElementMgmt.Rows.Count > 0)
            {
                this.AssignAutoCompletSouce(lineLists.GetGenericElementMgmt.Rows, lineLists.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.ObjectTextBox, sourceObject);
            }
            else
            {
                this.ObjectTextBox.AutoCompleteCustomSource = null;
            }

            this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetFunctionItems(null);
            if (this.getdescriptionDataset.GetFunctionItems.Rows.Count > 0)
            {
                this.AssignAutoCompletSouce(this.getdescriptionDataset.GetFunctionItems.Rows, this.getdescriptionDataset.GetFunctionItems.FunctionValueColumn.ColumnName, this.FunctionTextBox, sourceFunction);
            }
            else
            {
                this.FunctionTextBox.AutoCompleteCustomSource = null;
            }

            barsList = this.form15007Controll.WorkItem.F1500_GetGenericElementMgmt(null, null, "Bars");
            if (barsList.GetGenericElementMgmt.Rows.Count > 0)
            {
                this.AssignAutoCompletSouce(barsList.GetGenericElementMgmt.Rows, barsList.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.BarsTextBox, sourceBar);
            }
            else
            {
                this.BarsTextBox.AutoCompleteCustomSource = null;
            }

            objectsList = this.form15007Controll.WorkItem.F1500_GetGenericElementMgmt(null, null, "Lines");
            if (objectsList.GetGenericElementMgmt.Count > 0)
            {
                this.AssignAutoCompletSouce(objectsList.GetGenericElementMgmt.Rows, objectsList.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.LineTextBox, sourceLine);
            }
            else
            {
                this.LineTextBox.AutoCompleteCustomSource = null;
            }
        }

        /// <summary>
        /// Assigns the auto complet souce.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="textBox">The text box.</param>
        /// <param name="sourceCollection">The source collection.</param>
        private void AssignAutoCompletSouce(DataRowCollection dataRow, string columnName, TerraScanTextBox textBox, AutoCompleteStringCollection sourceCollection)
        {
            for (int count = 0; count < dataRow.Count; count++)
            {
                sourceCollection.Add(dataRow[count][columnName].ToString());
            }

            textBox.AutoCompleteCustomSource = sourceCollection;
        }

        /// <summary>
        /// Calls to selection for bars lines and object buttons
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CallToSelection(object sender, EventArgs e)
        {
            string keyName = string.Empty;
            string keyID = string.Empty;
            Control tempControl = (Control)sender;
            switch (tempControl.Name.ToString())
            {
                case "BarsButton":
                    {
                        keyName = "Bars";
                        break;
                    }

                case "LineButton":
                    {
                        keyName = "Lines";
                        break;
                    }

                case "ObjectButton":
                    {
                        keyName = "Objects";
                        break;
                    }
            }

            Form activeWorkOrderSelectForm = new Form();
            object[] optionalParameter = new object[] { keyName };
            activeWorkOrderSelectForm = TerraScanCommon.GetForm(1503, optionalParameter, this.form15007Controll.WorkItem);
            if (activeWorkOrderSelectForm != null)
            {
                if (activeWorkOrderSelectForm.ShowDialog() == DialogResult.OK)
                {
                    keyID = TerraScanCommon.GetValue(activeWorkOrderSelectForm, "ElementKeyId");

                    switch (tempControl.Name.ToString())
                    {
                        case "BarsButton":
                            {
                                //// Modified By Ramya D
                                if (this.BarsTextBox.Text == TerraScanCommon.GetValue(activeWorkOrderSelectForm, "ElementKeyId"))
                                {
                                    this.flag = 1;
                                }
                                else
                                {
                                    this.flag = 0;
                                }

                                this.BarsTextBox.Text = keyID;
                                if (!string.IsNullOrEmpty(this.BarsTextBox.Text.Trim()))
                                {
                                    this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetDescription(keyID, "Bar");
                                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                                    {
                                        this.BarsDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                                        this.BarsDescTextBox.ReadOnly = true;
                                    }
                                }

                                break;
                            }

                        case "LineButton":
                            {
                                //// Modified By Ramya D
                                if (this.LineTextBox.Text == TerraScanCommon.GetValue(activeWorkOrderSelectForm, "ElementKeyId"))
                                {
                                    this.flag = 1;
                                }
                                else
                                {
                                    this.flag = 0;
                                }

                                this.LineTextBox.Text = keyID;
                                if (!string.IsNullOrEmpty(this.LineTextBox.Text.Trim()))
                                {
                                    this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetDescription(keyID, "Line");
                                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                                    {
                                        this.LineDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                                        this.LineDescTextBox.ReadOnly = true;
                                    }
                                }

                                break;
                            }

                        case "ObjectButton":
                            {
                                //// Modified By Ramya D
                                if (this.ObjectTextBox.Text == TerraScanCommon.GetValue(activeWorkOrderSelectForm, "ElementKeyId"))
                                {
                                    this.flag = 1;
                                }
                                else
                                {
                                    this.flag = 0;
                                }

                                this.ObjectTextBox.Text = keyID;
                                if (!string.IsNullOrEmpty(this.ObjectTextBox.Text.Trim()))
                                {
                                    this.getdescriptionDataset = this.form15007Controll.WorkItem.F1500_GetDescription(keyID, "Object");
                                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                                    {
                                        this.ObjectDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                                        this.ObjectDescTextBox.ReadOnly = true;
                                    }
                                }

                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (this.RollYearTextBox.NumericTextBoxValue != 0 && (this.RollYearTextBox.NumericTextBoxValue < 1900 || this.RollYearTextBox.NumericTextBoxValue > 2079))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidFieldYear");
                this.RollYearTextBox.Focus();
            }
            else if ((string.IsNullOrEmpty(this.SubFundTextBox.Text.ToString().Trim())) || this.CheckAccount() == -1)
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("F15004SubFundMissReq"), "TerraScan T2 - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    this.SubFundTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            else if (this.CheckAccount() == -2)
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("InvalidSunFund"), "TerraScan T2 - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    this.SubFundTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            else if (this.CheckAccount() == -3)
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("DuplicateAccountExists"), "TerraScan T2 - Invalid Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    this.SubFundTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            else if ((string.IsNullOrEmpty(this.FunctionTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.BarsTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.ObjectTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.LineTextBox.Text.ToString().Trim())))
            {
                this.FunctionTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F15004MissReq");
            }

            ////else if (!this.CheckForValidSubFund())
            ////{
            ////    DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("InvalidSunFund"), "TerraScan - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////    if (result == DialogResult.OK)
            ////    {
            ////        this.SubFundTextBox.Focus();
            ////        sliceValidationFields.DisableNewMethod = true;
            ////        sliceValidationFields.RequiredFieldMissing = false;
            ////        sliceValidationFields.ErrorMessage = string.Empty;
            ////    }
            ////}

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the duplicate agency.
        /// </summary>
        /// <returns>true or false</returns>
        private int CheckAccount()
        {
            int errorId = -1;
            int tempValue = -1;
            string accountXml = this.GetAccountXML();
            if (this.AccountIDTextBox.Text != string.Empty && int.TryParse(this.AccountIDTextBox.Text, out tempValue))
            {
                this.accountId = tempValue;
            }

            errorId = this.form15007Controll.WorkItem.F15007_CheckDuplicateAccount(Convert.ToInt32(this.accountId), accountXml);
            return errorId;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.AccountInfoPanel.Enabled = !lockValue;
        }

        /// <summary>
        /// Checks for valid sub fund.
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckForValidSubFund()
        {
            string validKeyId = string.Empty;
            validKeyId = this.SubFundTextBox.Text;
            if (!string.IsNullOrEmpty(validKeyId))
            {
                short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);

                this.subFundManagementData = this.form15007Controll.WorkItem.F9503_GetSubFundItems(validKeyId, this.rollYear);
                if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        #endregion

        /// <summary>
        /// DescriptionTextBox_TextChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            this.FileEdit();
        }

        /// <summary>
        /// FileEdit
        /// </summary>
        private void FileEdit()
        {
            this.flag = 0;
            if (this.PermissionEdit && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (!this.formload)
                    {
                        if ((this.pageMode != TerraScanCommon.PageModeTypes.New))
                        {
                            if (this.flag != 1)
                            {
                                this.SeteditrProcess();
                            }
                        }
                    }
                }
            }

            this.SetColorOfComboBox();
        }

        /// <summary>
        /// SubFundTextBox TextChanged Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void SubFundTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.FileEdit();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// FunctionTextBox TextChanged Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void FunctionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.FileEdit();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// BarsTextBox TextChanged Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void BarsTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.FileEdit();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Object TextBox Text Changed Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ObjectTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.FileEdit();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Copy Account Button

        /// <summary>
        /// Handles the Click event of the CopyAccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CopyAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionEdit && this.formMasterPermissionEdit)
                {
                    Form copyaccountForm = new Form();
                    object[] optionalParameter = new object[] { Convert.ToInt32(this.AccountIDTextBox.Text.Trim())};
                    copyaccountForm = TerraScanCommon.GetForm(1504, optionalParameter, this.form15007Controll.WorkItem);
                    copyaccountForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion
    }
}
