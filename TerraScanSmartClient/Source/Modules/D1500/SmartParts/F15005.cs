//----------------------------------------------------------------------------------
// <copyright file="F15005.cs" company="Congruent">
// Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F15005 Form Slice - SubFundMgmt 
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		         Description
// ----------		---------		     -------------------------------------------
// 18-12-2006       Shiva                Created
// 22-04-2009       Shanmuga Sundaram.A  Modified
// 31-08-2009       Malliga              Modified for the CO : 3020
//**********************************************************************************

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
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;

    /// <summary>
    /// F15005 FormSlice - SubFundMgmt Functionality
    /// </summary>
    [SmartPart]
    public partial class F15005 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15005Controll variable.
        /// </summary>
        private F15005Controller form15005Controll;

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit variable used to store form master editpermission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo variable used to store the masterForm Number.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// keyId variable used to srored the keyId value.
        /// </summary>
        private int keyId;

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// datatable contains the formDetails.
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// dataset contains subFund management details.
        /// </summary>
        private F9503SubFundManagementData subFundMgmtDataSet = new F9503SubFundManagementData();

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData ownerDetailDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// subFundId variable is used to store the subfundId value default value is Null.
        /// </summary>
        private int? subFundId = null;

        /// <summary>
        /// accountId variable is used to store the accountId value default value is Null.
        /// </summary>
        private int? accountId = null;

        /// <summary>
        /// fundId variable is used to store the fundId value default value is Null.
        /// </summary>
        private int? fundId = null;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// agencyId variable is used to store the agencyId value default value is Null.
        /// </summary>
        private int? agencyId;
        private string configuredState;

        private int selectedownerId;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15005"/> class.
        /// </summary>
        public F15005()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15005"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15005(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.SubFundsListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SubFundsListPictureBox.Height, this.SubFundsListPictureBox.Width, "Sub Fund", 28, 81, 128);
            this.DisbursementHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DisbursementHistoryPictureBox.Height, this.DisbursementHistoryPictureBox.Width, "Disbursement History", 174, 150, 94);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form15005 controll.
        /// </summary>
        /// <value>The form15005 controll.</value>
        [CreateNew]
        public F15005Controller Form15005Controll
        {
            get { return this.form15005Controll as F15005Controller; }
            set { this.form15005Controll = value; }
        }

        /// <summary>
        /// Gets or sets the sub fund ID.
        /// </summary>
        /// <value>The sub fund ID.</value>
        public int? SubFundIdentity
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
        /// Gets or sets the fund id.
        /// </summary>
        /// <value>The fund id.</value>
        public int? FundId
        {
            get { return this.fundId; }
            set { this.fundId = value; }
        }

        /// <summary>
        /// Gets or sets the agency id.
        /// </summary>
        /// <value>The agency id.</value>
        public int? AgencyId
        {
            get { return this.agencyId; }
            set { this.agencyId = value; }
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
                    if (this.subFundMgmtDataSet.SubFundDetails.Rows.Count > 0)
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

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    if (this.subFundMgmtDataSet.SubFundDetails.Rows.Count > 0)
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
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.ValidateSliceForm(eventArgs);
                }
                else if (this.slicePermissionField.editPermission)
                {
                    this.ValidateSliceForm(eventArgs);
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    bool status = this.SaveSubFundDetails();
                    if (status)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
            }
            else
            {
                this.LockControls(true);
                //// ToDo : FormLoad Events should happen (refresh)
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
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (this.slicePermissionField.newPermission)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.pageLoadStatus = true;
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.ClearSubFundHeader();
                        this.GetYear();
                        this.subFundMgmtDataSet.ListDisbursementHistory.Rows.Clear();
                        this.PopulateDisbursementHistoryGridView();
                        this.subFundId = null;
                        this.LockControls(false);
                        this.pageLoadStatus = false;
                        this.FundButton.Focus();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            this.Cursor = Cursors.WaitCursor;

            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.SubFundIdentity = this.keyId;
            this.GetSubFundDetails(this.keyId);          
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.Cursor = Cursors.Default;
            this.FundButton.Focus();
            
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
                    this.PopulateSubFundDetais();

                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                    this.FundButton.Focus();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #region Form Load Methods/Events

        /// <summary>
        /// Handles the Load event of the F15005 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15005_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeDisbursementHistoryGridView();               
                this.FundButton.Focus();
                this.PopulateSubFundDetais();
                this.SubFundsListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SubFundsListPictureBox.Height, this.SubFundsListPictureBox.Width, "SubFund", 28, 81, 128);
                this.DisbursementHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DisbursementHistoryPictureBox.Height, this.DisbursementHistoryPictureBox.Width, "Disbursement History", 174, 150, 94);
                FundLinkLabel.Focus();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the sub fund header details.
        /// </summary>
        /// <param name="subFundIdentifier">The sub fund identifier.</param>
        private void GetSubFundDetails(int subFundIdentifier)
        {
            try
            {
                this.subFundMgmtDataSet = this.form15005Controll.WorkItem.F9503_GetSubFundManagementDetails(subFundIdentifier);
                this.InitTypeComboBox();
                if (this.subFundMgmtDataSet.ConfiguredState.Rows.Count > 0)
                {
                    this.configuredState = this.subFundMgmtDataSet.ConfiguredState.Rows[0][this.subFundMgmtDataSet.ConfiguredState.StateColumn.ColumnName].ToString();
                }
                if (!string.IsNullOrEmpty(this.configuredState) && this.configuredState.Equals("NE"))
                {
                    this.ContactPanel.Visible = true;
                    this.NELowePanel.Visible = true;
                    this.SubFundsListPictureBox.Height = 157+78;
                    this.DisbursementBalancePanel.Location = new System.Drawing.Point(this.DisbursementBalancePanel.Location.X,244  );
                    this.DisbursementHistoryGridPanel.Location = new System.Drawing.Point(this.DisbursementHistoryGridPanel.Location.X, 301 );
                    this.DisbursementHistoryPictureBox.Location = new System.Drawing.Point(this.DisbursementHistoryPictureBox.Location.X, 301 );
                    this.SubFundMgmtVScorrlBar.Location = new System.Drawing.Point(this.SubFundMgmtVScorrlBar.Location.X, 302);
                    this.SubFundsListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SubFundsListPictureBox.Height, this.SubFundsListPictureBox.Width, "SubFund", 28, 81, 128);
                    //this.DisbursementHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DisbursementHistoryPictureBox.Height, this.DisbursementHistoryPictureBox.Width, "Disbursement History", 174, 150, 94);
                }
                else
                {
                    this.ContactPanel.Visible = false;
                    this.NELowePanel.Visible = false;
                    this.SubFundsListPictureBox.Height = 157;
                    this.DisbursementBalancePanel.Location = new System.Drawing.Point(this.DisbursementBalancePanel.Location.X, 164);
                    this.DisbursementHistoryGridPanel.Location = new System.Drawing.Point(this.DisbursementHistoryGridPanel.Location.X,221 );
                    this.DisbursementHistoryPictureBox.Location = new System.Drawing.Point(this.DisbursementHistoryPictureBox.Location.X, 221);
                    this.SubFundMgmtVScorrlBar.Location = new System.Drawing.Point(this.SubFundMgmtVScorrlBar.Location.X, 222);
                    this.SubFundsListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SubFundsListPictureBox.Height, this.SubFundsListPictureBox.Width, "SubFund", 28, 81, 128);
                }
                if (this.subFundMgmtDataSet.SubFundDetails.Rows.Count > 0)
                {
                    int tempAgencyId = -1;
                    int tempFundId = -1;
                    Int32.TryParse(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.FundIDColumn.ColumnName].ToString(), out tempFundId);
                    Int32.TryParse(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.AgencyIDColumn.ColumnName].ToString(), out tempAgencyId);
                    this.fundId = tempFundId;
                    this.agencyId = tempAgencyId;
                    this.FundLinkLabel.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.FundColumn.ColumnName].ToString();
                    this.RollYearTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.RollYearColumn.ColumnName].ToString();
                    this.txtUnitlcCode.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.UnifiedLearningCommCodeColumn.ColumnName].ToString();
                    this.SubFundTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.SubFundColumn.ColumnName].ToString();
                    this.VoterApprovedComboBox.SelectedValue = Convert.ToInt32(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.IsVoterApprovedColumn.ColumnName]);
                    this.TypeComboBox.SelectedValue = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.SubFundTypeIDColumn.ColumnName];
                    this.LevyRateTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.RateColumn.ColumnName].ToString();
                    this.CommissionRateTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.CommissionRateColumn.ColumnName].ToString();
                    this.AgencyLinkLabel.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.AgencyNameColumn.ColumnName].ToString();
                    this.FactorTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.FactorColumn.ColumnName].ToString();
                    this.DescriptionTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.DescriptionColumn.ColumnName].ToString();
                    this.DisbursementBalanceTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.DisbursementBalanceColumn.ColumnName].ToString();
                    //// Code Added by shiva in Sprint 40 Tim Change Request.
                    this.ActiveComboBox.SelectedValue = Convert.ToInt32(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.IsActiveColumn.ColumnName]);
                    if (!string.IsNullOrEmpty(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.AltPropTaxAcctIDColumn.ColumnName].ToString()))
                    {
                        int tempAccountId;
                        int.TryParse(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.AltPropTaxAcctIDColumn.ColumnName].ToString(), out tempAccountId);
                        this.accountId = tempAccountId;
                        this.AlternatePropertyTaxAccountLinkLabel.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.AltPropTaxAcctNameColumn.ColumnName].ToString();
                    }
                    else
                    {
                        this.accountId = null;
                        this.AlternatePropertyTaxAccountLinkLabel.Text = string.Empty;
                    }
                    this.SchoolLevyLabel.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.SchoolFieldLabelColumn.ColumnName].ToString();
                    ////Coding Added by Malliga for the CO : 3020
                    ////Assigning vales to the ISSchool ComboBox
                    this.SchoolLevyComboBox.SelectedValue = Convert.ToInt32(this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.IsSchoolColumn.ColumnName]);

                    //Added for new CO
                    this.ContactLinkLabel.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.ContactColumn.ColumnName].ToString();
                    this.SequenceNumTextBox.Text = this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.SequenceNumberColumn.ColumnName].ToString();
                   this.SchoolDistClassTextBox.Text= this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.SchoolDistrictClassColumn.ColumnName].ToString();
                   this.CertificateLineNumTextBox.Text= this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.CertificateLineNumberColumn.ColumnName].ToString();
                   this.BaseCodeTextBox.Text= this.subFundMgmtDataSet.SubFundDetails.Rows[0][this.subFundMgmtDataSet.SubFundDetails.BaseCodeColumn.ColumnName].ToString();
                }
                else
                {
                    this.ClearSubFundHeader();
                    this.LockControls(true);
                }

                this.PopulateDisbursementHistoryGridView();
                if (this.ParentForm != null && this.ParentForm.Controls[0] != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                   //this.yaxisPoint = this.yaxisPoint + 25;
                   // ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, 100);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Populates the disbursement history grid view.
        /// </summary>
        private void PopulateDisbursementHistoryGridView()
        {
            try
            {
                int disbursementHistoryRowCount;
                this.DisbursementHistoryGridView.DataSource = this.subFundMgmtDataSet.ListDisbursementHistory.DefaultView;
                disbursementHistoryRowCount = this.DisbursementHistoryGridView.OriginalRowCount;

                if (disbursementHistoryRowCount > 0)
                {
                    this.DisbursementHistoryGridView.Enabled = true;
                    if (this.DisbursementHistoryGridView.CurrentRowIndex > 0)
                    {
                        this.DisbursementHistoryGridView.Rows[this.DisbursementHistoryGridView.CurrentRowIndex].Selected = false;
                        this.DisbursementHistoryGridView.Rows[0].Selected = true;
                    }
                }
                else
                {
                    this.DisbursementHistoryGridView.Rows[0].Selected = false;
                    this.DisbursementHistoryGridView.Enabled = false;
                }

                if (disbursementHistoryRowCount > this.DisbursementHistoryGridView.NumRowsVisible)
                {
                    this.SubFundMgmtVScorrlBar.Visible = false;
                }
                else
                {
                    this.SubFundMgmtVScorrlBar.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Grid Events/Methods

        /// <summary>
        /// Customize the DisbursementHistory gridview.
        /// </summary>
        private void CustomizeDisbursementHistoryGridView()
        {
            this.DisbursementHistoryGridView.AutoGenerateColumns = false;
            this.DisbursementHistoryGridView.PrimaryKeyColumnName = this.subFundMgmtDataSet.ListDisbursementHistory.PrimaryKeyIDColumn.ColumnName.ToString();

            this.AccountName.Width = 225;
            this.PayableTo.Width = 225;
            this.EntryDate.Width = 120;
            this.Amount.Width = 148;
            this.SubFundId.Width = 100;

            this.AccountName.DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.AccountNameColumn.ColumnName.ToString();
            this.PayableTo.DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.PayableToColumn.ColumnName.ToString();
            this.EntryDate.DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.EntryDateColumn.ColumnName.ToString();
            this.Amount.DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.AmountColumn.ColumnName.ToString();
            this.SubFundId.DataPropertyName = this.subFundMgmtDataSet.ListDisbursementHistory.SubFundIDColumn.ColumnName.ToString();
        }

        /// <summary>
        /// Handles the CellFormatting event of the DisbursementHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DisbursementHistoryGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            DateTime outDateTime;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.DisbursementHistoryGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.DisbursementHistoryGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                //// date fomatting

                if (e.ColumnIndex == this.DisbursementHistoryGridView.Columns["EntryDate"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
                    {
                        string val = e.Value.ToString();
                        if (DateTime.TryParse(val, out outDateTime))
                        {
                            e.Value = outDateTime.ToString("M/d/yyyy");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Click event of the FundButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FundButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form fundSelectionForm = new Form();
                object[] optionalParameter = new object[0];
                fundSelectionForm = this.form15005Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1513, optionalParameter, this.Form15005Controll.WorkItem);
                if (fundSelectionForm != null)
                {
                    if (fundSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.fundId = Convert.ToInt32(TerraScanCommon.GetValue(fundSelectionForm, "FundId"));
                        this.FundLinkLabel.Text = TerraScanCommon.GetValue(fundSelectionForm, "FundItem").ToString();
                    }
                    ////Added by Biju to fix #5160
                    else
                    {
                        if (!this.ParentForm.MdiParent.ActiveMdiChild.Text.Equals(this.ParentForm.Text))
                        {
                            SendKeys.Send("^{TAB}");
                            SendKeys.Send("^+{TAB}");
                        }

                    }
                    ////till here
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AgencyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AgencyButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form agencySelectionForm = new Form();
                agencySelectionForm = this.form15005Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1514, null, this.Form15005Controll.WorkItem);
                if (agencySelectionForm != null)
                {
                    if (agencySelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.agencyId = Convert.ToInt32(TerraScanCommon.GetValue(agencySelectionForm, "AgencyId"));
                        this.AgencyLinkLabel.Text = TerraScanCommon.GetValue(agencySelectionForm, "agencyName").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the FundLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void FundLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                if (this.FundId != null)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11003);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.FundId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LevyRateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LevyRateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                TerraScanTextBox tempControl = (TerraScanTextBox)sender;
                if (!String.IsNullOrEmpty(tempControl.Text))
                {
                    tempControl.Text = tempControl.Text;

                    if (this.ValidateDecimalTextBox(tempControl.Text.ToString(), tempControl.AccessibleName.ToString()))
                    {
                        tempControl.Text = string.Empty;
                        tempControl.Focus();
                    }
                }
                else
                {
                    tempControl.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AgencyLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AgencyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                if (this.AgencyId != null)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11004);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.AgencyId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ToolTip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToolTip_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Control tempControl = (Control)sender;
                if (!String.IsNullOrEmpty(tempControl.Text))
                {
                    if (tempControl.Text.Length > 20)
                    {
                        this.SubFundToolTip.RemoveAll();
                        this.SubFundToolTip.SetToolTip(tempControl, tempControl.Text);
                    }
                    else
                    {
                        this.SubFundToolTip.RemoveAll();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the Click event of the TaxAccountImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TaxAccountImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form alternatePropertyTaxAccountForm = new Form();
                object[] optionalParameter = new object[] { this.RollYearTextBox.Text };
                alternatePropertyTaxAccountForm = this.form15005Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.Form15005Controll.WorkItem);
                if (alternatePropertyTaxAccountForm != null)
                {
                    if (alternatePropertyTaxAccountForm.ShowDialog() == DialogResult.OK)
                    {
                        this.accountId = Convert.ToInt32(TerraScanCommon.GetValue(alternatePropertyTaxAccountForm, "AccountId"));
                        this.AlternatePropertyTaxAccountLinkLabel.Text = TerraScanCommon.GetValue(alternatePropertyTaxAccountForm, "SelectedAccountName").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AlternatePropertyTaxAccountLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AlternatePropertyTaxAccountLinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo;
                if (this.accountId != null)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11007);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.accountId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Initializes the voter approved combo box.
        /// </summary>
        private void InitVoterApprovedComboBox()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.VoterApprovedComboBox.DataSource = commonData.ComboBoxDataTable;
            this.VoterApprovedComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.VoterApprovedComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Initializes the type combo box.  
        /// </summary>
        private void InitTypeComboBox()
        {
            this.TypeComboBox.DataSource = null;
            if (this.subFundMgmtDataSet.ListSubFundType.Rows.Count > 0)
            {
                this.TypeComboBox.DataSource = this.subFundMgmtDataSet.ListSubFundType;
                this.TypeComboBox.ValueMember = this.subFundMgmtDataSet.ListSubFundType.SubFundTypeIDColumn.ColumnName;
                this.TypeComboBox.DisplayMember = this.subFundMgmtDataSet.ListSubFundType.SubFundTypeColumn.ColumnName;
            }
        }

        /// <summary>
        /// Initialize the active combo box. //// Code Added by shiva in Sprint 40 Tim Change Request.
        /// </summary>
        private void InitActiveComboBox()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.ActiveComboBox.DataSource = commonData.ComboBoxDataTable;
            this.ActiveComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.ActiveComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        #region Populating ISSchool Combo Box
        /// <summary>
        /// Inits the is school combo box.
        /// </summary>
        private void InitIsSchoolComboBox()
        {
            ////Coding Added by Malliga for the CO : 3020
            ////Populating IsSchool ComboBox
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.SchoolLevyComboBox.DataSource = commonData.ComboBoxDataTable;
            this.SchoolLevyComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.SchoolLevyComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ColumnName;
        }
        #endregion

        /// <summary>
        /// Clears the sub fund header.
        /// </summary>
        private void ClearSubFundHeader()
        {
            this.fundId = null;
            this.agencyId = null;
            this.accountId = null;
            this.FundLinkLabel.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.SubFundTextBox.Text = string.Empty;
            this.VoterApprovedComboBox.SelectedValue = 1;
            this.TypeComboBox.SelectedValue = 1;
            this.LevyRateTextBox.Text = string.Empty;
            this.CommissionRateTextBox.Text = string.Empty;
            this.AgencyLinkLabel.Text = string.Empty;
            this.FactorTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.DisbursementBalanceTextBox.Text = string.Empty;
            //// Code Added by shiva in Sprint 40 Tim Change Request.
            this.ActiveComboBox.SelectedValue = 1;
            this.AlternatePropertyTaxAccountLinkLabel.Text = string.Empty;
            //// Code Added by Malliga for the CO : 3020 on 31/8/2009
            ////Assigning School combobox value as 1.
            this.SchoolLevyComboBox.SelectedValue = 0;
            this.ContactLinkLabel.Text = string.Empty;
            this.SequenceNumTextBox.Text = string.Empty;
            this.SchoolDistClassTextBox.Text = string.Empty;
            this.CertificateLineNumTextBox.Text = string.Empty;
            this.BaseCodeTextBox.Text = string.Empty;
            this.txtUnitlcCode.Text = string.Empty;
        }

        /// <summary>
        /// Sets the text box max lenght.
        /// </summary>
        private void SetTextBoxMaxLenght()
        {
            this.RollYearTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.RollYearColumn.MaxLength;
            this.SubFundTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.SubFundColumn.MaxLength;
            this.LevyRateTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.RateColumn.MaxLength;
            this.CommissionRateTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.CommissionRateColumn.MaxLength;
            this.FactorTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.FactorColumn.MaxLength;
            this.DescriptionTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.DescriptionColumn.MaxLength;
            this.DisbursementBalanceTextBox.MaxLength = this.subFundMgmtDataSet.SubFundDetails.DisbursementBalanceColumn.MaxLength;
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form15005Controll.WorkItem.GetConfigDetails("TR_RollYear");
            this.RollYearTextBox.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.PermissionEdit && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.FundButton.Enabled = !lockValue;
            this.RollYearTextBox.LockKeyPress = lockValue;
            this.SubFundTextBox.LockKeyPress = lockValue;
            this.VoterApprovedComboBox.Enabled = !lockValue;
            this.TypeComboBox.Enabled = !lockValue;
            this.ActiveComboBox.Enabled = !lockValue;
            this.LevyRateTextBox.LockKeyPress = lockValue;
            this.CommissionRateTextBox.LockKeyPress = lockValue;
            this.AgencyButton.Enabled = !lockValue;
            this.FactorTextBox.LockKeyPress = lockValue;
            this.DescriptionTextBox.LockKeyPress = lockValue;
            this.TaxAccountImageButton.Enabled = !lockValue;
            ////Coding Added by Malliga for the CO : 3020
            ////Schoollevy combox enable/disable
            this.SchoolLevyComboBox.Enabled = !lockValue;

            //Added for Implementing new CO
            this.ContactLinkLabel.Enabled = !lockValue;
            this.SequenceNumTextBox.Enabled = !lockValue;
            this.SchoolDistClassTextBox.Enabled = !lockValue;
            this.CertificateLineNumTextBox.Enabled = !lockValue;
            this.BaseCodeTextBox.Enabled = !lockValue;
            this.txtUnitlcCode.Enabled = !lockValue;
        }

        /// <summary>
        /// Validates the roll year.
        /// </summary>
        /// <returns>Validated Status</returns>
        private bool ValidateRollYear()
        {
            try
            {
                short tempRollYear;
                Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
                if (tempRollYear < 1900 || tempRollYear > 2079)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the decimal text box.
        /// </summary>
        /// <param name="validateString">The validate string.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <returns>Valid Status</returns>
        private bool ValidateDecimalTextBox(string validateString, string controlName)
        {
            string[] validString = new string[2];
            validString = validateString.Split('.');
            if (validString[0].Length > 3 || Convert.ToInt16(validString[0].ToString()) > 100)
            {
                MessageBox.Show(controlName + " value should not be greater than 100%", ConfigurationWrapper.ApplicationName + " - " + "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (validString[0].ToString() == "100")
            {
                long tempValue;
                long.TryParse(validString[1].Replace("%", ""), out tempValue);

                if (tempValue > 0)
                {
                    MessageBox.Show(controlName + " value should not be greater than 100%", ConfigurationWrapper.ApplicationName + " - " + "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Saves the sub fund details.
        /// </summary>
        /// <returns>the status</returns>
        private bool SaveSubFundDetails()
        {
            try
            {
                int returnValue = -1;
                this.Cursor = Cursors.WaitCursor;
                F9503SubFundManagementData saveSubFundMgmtDataSet = new F9503SubFundManagementData();
                F9503SubFundManagementData.SubFundDetailsRow dr = saveSubFundMgmtDataSet.SubFundDetails.NewSubFundDetailsRow();
                if (this.FundId.HasValue)
                {
                    dr.FundID = Convert.ToInt32(this.FundId);
                }

                dr.SubFundID = Convert.ToInt32(this.SubFundIdentity);
                dr.RollYear = Convert.ToInt16(this.RollYearTextBox.Text.Trim());
                dr.SubFund = this.SubFundTextBox.Text.Trim();
                dr.Description = this.DescriptionTextBox.Text.Trim();
                dr.IsActive = Convert.ToBoolean(this.ActiveComboBox.SelectedValue); //// Code Added by shiva in Sprint 40 Tim Change Request.
                dr.Rate = this.LevyRateTextBox.DecimalTextBoxValue;
               
                dr.IsVoterApproved = Convert.ToBoolean(Convert.ToInt32(this.VoterApprovedComboBox.SelectedValue.ToString()));
                dr.CommissionRate = this.CommissionRateTextBox.DecimalTextBoxValue;
                dr.IsCash = false;
                dr.Factor = this.FactorTextBox.DecimalTextBoxValue;
                dr.SubFundTypeID = Convert.ToByte(this.TypeComboBox.SelectedValue);
                if (this.AgencyId.HasValue)
                {
                    dr.AgencyID = Convert.ToInt32(this.AgencyId);
                }

                if (this.accountId.HasValue && !string.IsNullOrEmpty(this.AlternatePropertyTaxAccountLinkLabel.Text))
                {
                    int tempAccountID;
                    int.TryParse(this.accountId.ToString(), out tempAccountID);
                    dr.AltPropTaxAcctID = tempAccountID;
                }
                ////Coding Added by Malliga for the CO : 3020
                ////assigning vales to datatable
                dr.IsSchool = Convert.ToBoolean(this.SchoolLevyComboBox.SelectedValue); 
                if(this.configuredState.Equals("NE"))
                {
                    dr.OwnerID = this.selectedownerId;
                    dr.Contact = this.ContactLinkLabel.Text.Trim();
                    dr.SequenceNumber = this.SequenceNumTextBox.Text.Trim();
                    dr.SchoolDistrictClass = this.SchoolDistClassTextBox.Text.Trim();
                    dr.CertificateLineNumber = this.CertificateLineNumTextBox.Text.Trim();
                    dr.BaseCode = this.BaseCodeTextBox.Text.Trim();
                    dr.UnifiedLearningCommCode = this.txtUnitlcCode.Text.Trim();
                }
                
                saveSubFundMgmtDataSet.SubFundDetails.Rows.Add(dr);
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(saveSubFundMgmtDataSet.SubFundDetails.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                string tempxml = string.Empty;
                tempxml = tempDataSet.GetXml();
                returnValue = this.form15005Controll.WorkItem.F9503_CreateOrEditSubFund(this.SubFundIdentity, tempxml, TerraScanCommon.UserId);
                if (returnValue != -1)
                {
                    this.SubFundIdentity = returnValue;
                    SliceReloadActiveRecord currentSliceInfo;
                    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    currentSliceInfo.SelectedKeyId = returnValue;
                    ////to refresh the master form with the return keyid
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SoapException ex)
            {
                //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            catch (Exception ex1)
            {
                //// ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()))
            {
                this.SubFundTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.LevyRateTextBox.Text.Trim()))
            {
                this.LevyRateTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.TypeComboBox.Text.Trim()))
            {
                this.TypeComboBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (this.ValidateRollYear())
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidFieldYear");
            }
            else if (!this.CheckDuplicateSubFund())
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("F15002DuplicateSubFundErrorMessage"), SharedFunctions.GetResourceString("F15002DuplicateSubFundErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    this.SubFundTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Populates the sub fund detais.
        /// </summary>
        private void PopulateSubFundDetais()
        {
            this.SubFundIdentity = this.keyId;
            this.pageLoadStatus = true;
            this.InitVoterApprovedComboBox();
            this.InitActiveComboBox();
            ////Populating School Combo Box
            this.InitIsSchoolComboBox();
            this.GetSubFundDetails(Convert.ToInt32(this.SubFundIdentity));
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;
            //if (this.ParentForm != null && this.ParentForm.Controls[0] != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            //{
            //    //this.yaxisPoint = this.yaxisPoint + 25;
            //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, 25);
            //}
        }

        /// <summary>
        /// Checks the duplicate district.
        /// </summary>
        /// <returns>duplicate record status</returns>
        private bool CheckDuplicateSubFund()
        {
            try
            {
                int errorId = -1;
                errorId = this.form15005Controll.WorkItem.F15005_CheckSubFund(this.SubFundIdentity, this.SubFundTextBox.Text.Trim(), Convert.ToInt32(this.RollYearTextBox.Text.Trim()));
                if (errorId != -1)
                {
                    return true;
                }

                return false;
            }
            catch (SoapException)
            {
                //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
        }

        #endregion


        /// <summary>
        /// ScheduleOwnerPicture_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleOwnerPicture_Click(object sender, EventArgs e)
        {
            try
            {
                Form parcelF9101 = new Form();
                parcelF9101 = TerraScanCommon.GetForm(9101, null, this.form15005Controll.WorkItem);

                if (parcelF9101 != null)
                {
                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                    {
                        Int32 resultownerid;
                        Int32.TryParse((TerraScanCommon.GetValue(parcelF9101, SharedFunctions.GetResourceString("MasterNameOwnerId"))), out resultownerid);
                        this.selectedownerId = resultownerid;
                       
                        this.ownerDetailDataSet = this.form15005Controll.WorkItem.F15010_GetOwnerDetails(this.selectedownerId);
                        if (this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows.Count > 0)
                        {
                            //Issue fixed for TSBG 13002
                            //this.selectedownerName = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString();
                            string ScheduleOwnerField = string.Empty;
                            ScheduleOwnerField = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString();
                            if (ScheduleOwnerField.Contains("&"))
                            {
                                ScheduleOwnerField = ScheduleOwnerField.Replace("&", "&&");
                            }
                            this.ContactLinkLabel.Text = ScheduleOwnerField;
                            //this.ScheduleOwnerLinkLabel.Text = this.selectedownerName;
                        }
                    }
                }

                //this.ActiveControl = this.EditScheduleSaveButton;
                //this.EditScheduleSaveButton.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.ContactLinkLabel.Focus();
            }
        }

        /// <summary>
        /// ScheduleOwnerLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleOwnerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //try
            //{
            //    FormInfo formInfo;
            //    formInfo = TerraScanCommon.GetFormInfo(91000);
            //    formInfo.optionalParameters = new object[1];
            //    formInfo.optionalParameters[0] = this.selectedownerId;
            //    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            //    //this.Close();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void ContactLinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ContactLinkLabel.Text))
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(91000);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.selectedownerId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
                //this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void F15005_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}