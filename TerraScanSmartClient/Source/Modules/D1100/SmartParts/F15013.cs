//--------------------------------------------------------------------------------------------
// <copyright file="F15013.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15013.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 24 Jan 06        JYOTHI              Created
//*********************************************************************************/
namespace D1100
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    /// <summary>
    /// F15013 class file
    /// </summary>
    [SmartPart]
    public partial class F15013 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15013Control Controller
        /// </summary>
        private F15013Controller form15013Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// exciseRateIds variable is used to store list of exciseRateIds for Excise Tax Rate. 
        /// </summary>       
        private ExciseTaxRateData exciseRateIds = new ExciseTaxRateData();

        /// <summary>
        /// exciseTaxRateDataSet variable is used to get the details of Excise Tax Rate.
        /// </summary>
        private F15013ExciseTaxRateData exciseTaxRateDataSet = new F15013ExciseTaxRateData();

        /// <summary>
        /// Variable accountId 
        /// </summary>
        private int accountId;

        /// <summary>
        /// Variable accountValue 
        /// </summary>
        private string selectedAccountName;

        /// <summary>
        /// used to maintain loadstate
        /// </summary>
        private bool formLoad = true;

        /// <summary>
        /// tempYear
        /// </summary>
        private int tempYear;

        /// <summary>
        /// nonZero
        /// </summary>
        private string nonZero;

        /// <summary>
        /// concadinatedString
        /// </summary>
        private string concadinatedString;

        /// <summary>
        /// districtCopy
        /// </summary>
        private bool districtCopy;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15013"/> class.
        /// </summary>
        public F15013()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15013"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15013(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "District Info", 174, 150, 94);
            this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, this.DistrictInfoSecIndicatorPictureBox.Width, "Account Info", 28, 81, 128);


        }
        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event for raising new operation in form master        
        /// </summary> 
        [EventPublication(EventTopicNames.D9030_F9030_RaiseFormMasterNew, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9030_RaiseFormMasterNew;

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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

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

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F15013 control.
        /// </summary>
        /// <value>The F15013 control.</value>
        [CreateNew]
        public F15013Controller Form15013Control
        {
            get { return this.form15013Control as F15013Controller; }
            set { this.form15013Control = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
            }
        }
        #endregion

        #region EventSubcription

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
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
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));

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
            if (this.slicePermissionField.newPermission)
            {
                if (this.districtCopy)
                {
                    this.DistrictCopy();
                }
                else
                {
                    this.NewButton_Click();
                }
            }
            else
            {
                this.EnableControls(false);
                this.LockControls(true);
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
            this.CancelButton_Click();
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.DeleteButton_Click();
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                this.SaveButton_Click();
            }
        }

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
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                    if (this.exciseTaxRateDataSet.GetExciseTaxRate.Rows.Count > 0)
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
                    this.GetExciseRateDetails();
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
        /// Sets the form cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetFormCancelButton, Thread = ThreadOption.UserInterface)]
        public void SetFormCancelButton(object sender, DataEventArgs<string> e)
        {
            this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
        }

        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
        }

        #endregion

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

        /// <summary>
        /// Called when [D9030_ F9030_ raise form master new].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_RaiseFormMasterNew(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9030_RaiseFormMasterNew != null)
            {
                this.D9030_F9030_RaiseFormMasterNew(this, eventArgs);
            }
        }

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F15013 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15013_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GetLocalType();
                ////flag = 1;
                this.LocationCodelabel.Focus();
                this.GetExciseRateDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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

        #region Get Excise rate details

        /// <summary>
        /// Gets the type of the Local.
        /// </summary>
        private void GetLocalType()
        {
            ExciseTaxRateData.LocalTypeDataTable localTypeDataTable = new ExciseTaxRateData.LocalTypeDataTable();
            localTypeDataTable.Rows.Add(new object[] { 0, "CITY", });
            localTypeDataTable.Rows.Add(new object[] { 1, "COUNTY" });
            this.LocalIsComboBox.DataSource = localTypeDataTable.Copy();
            this.LocalIsComboBox.ValueMember = localTypeDataTable.LocalIDColumn.ColumnName;
            this.LocalIsComboBox.DisplayMember = localTypeDataTable.LocalNameColumn.ColumnName;
            this.LocalIsComboBox.SelectedValue = 1;
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form15013Control.WorkItem.GetConfigDetails("TR_RollYear");
            this.YearTextBox.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        /// <summary>
        /// Gets the excise rate details. int exciseRateId
        /// </summary>
        private void GetExciseRateDetails()
        {
            this.LocationCodeTextBox.Focus();
            this.formLoad = true;
            this.exciseTaxRateDataSet = this.form15013Control.WorkItem.F15013_GetExciseTaxRate(this.keyId);
            if (this.exciseTaxRateDataSet.GetExciseTaxRate.Rows.Count > 0 && this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows.Count > 0)
            {
                //// Fill District Info
                this.LocationCodeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.LocationCodeColumn].ToString();
                this.LocationNameTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.LocationNameColumn].ToString();
                this.RateDistrictIDTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.ExciseRateIDColumn].ToString();
                this.TextLeave(this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.LocalAdminFeeColumn].ToString(), this.AdminFeesTextBox);
                this.TextLeave(this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.StateAdminFeeColumn].ToString(), this.StateAdminTextBox);
                this.TransactionFeesTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.TransFeeColumn].ToString();
                this.TechFeesTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.TechFeeColumn].ToString();
                this.YearTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.YearColumn].ToString();
                this.TextLeave(this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.LocalTaxRateColumn].ToString(), this.LocalRateTextBox);
                this.LocalIsComboBox.SelectedValue = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.IsCountyColumn].ToString();
                this.TotalTaxRateTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.TotalTaxRateColumn].ToString();
                ////this.TaxDistrictLinkLabel.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["District"].ToString();
                //// this.TaxDistrictLinkLabel.Tag = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["DistrictID"].ToString();
                this.DescriptionTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxRate.DescriptionColumn].ToString();
                //// Account Info
                this.AccountStateAdminFeeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateAdminAcctColumn].ToString();
                this.AccountAdminFeeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalAdminAcctColumn].ToString();
                this.AccountTransactionFeeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TransFeeAcctColumn].ToString();
                this.AccountTechnologyFeeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeeAcctColumn].ToString();
                //Added by Purushotham to implement CO TFS#19873 on 22Nov2013
                this.AccountTechnologyFeeBTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeeAcctBColumn].ToString();
                this.TechnologyFeePercentTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeePercentAColumn].ToString();
                this.TechnologyFeeBPercentTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeePercentBColumn].ToString();

                this.AccountLocalTaxTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctAColumn].ToString();
                this.AccountTextLeave(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxPercentAColumn].ToString(), this.LocalTaxPercentTextBox);
                this.LocalTaxPercentTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxPercentAColumn].ToString();

                ////new local tax b text box is added
                this.LocalTaxBTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctBColumn].ToString();
                this.AccountTextLeave(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxPercentBColumn].ToString(), this.LocalTaxBPercentTextBox);
                this.LocalTaxBPercentTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxPercentBColumn].ToString();

                this.AccountLocalInterestTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalIntAcctColumn].ToString();
                this.AccountLocalPenaltyTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalPenAcctColumn].ToString();
                this.AccountStateTaxTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateTaxAcctColumn].ToString();
                this.AccountStateInterestTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateIntAcctColumn].ToString();
                this.AccountStatePenaltyTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StatePenAcctColumn].ToString();

                this.AccountStateAdminFeeTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateAdminAcctIDColumn].ToString();
                this.AccountAdminFeeTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalAdminAcctIDColumn].ToString();
                this.AccountTransactionFeeTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TransFeeAcctIDColumn].ToString();
                this.AccountTechnologyFeeTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeeAcctIDColumn].ToString();
                // Modified for TSBG Excise Rate District - Change to Local Tax B affects Tech Fee B account
                this.AccountTechnologyFeeBTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeeAcctBIDColumn].ToString();

                this.AccountLocalTaxTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctAIDColumn].ToString();
                /////new local tax b textbox is added
                this.LocalTaxBTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctBIDColumn].ToString();
                this.AccountLocalInterestTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalIntAcctIDColumn].ToString();
                this.AccountLocalPenaltyTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalPenAcctIDColumn].ToString();
                this.AccountStateTaxTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateTaxAcctIDColumn].ToString();
                this.AccountStateInterestTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateIntAcctIDColumn].ToString();
                this.AccountStatePenaltyTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.StatePenAcctIdColumn].ToString();

                this.AccountStateAdminFeeTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateAdminAcctPendingColumn].ToString();
                this.AccountAdminFeeTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalAdminAcctPendingColumn].ToString();
                this.AccountTransactionFeeTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TransFeePendingColumn].ToString();
                this.AccountTechnologyFeeTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeePendingColumn].ToString();
                this.AccountLocalTaxTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctAPendingColumn].ToString();
                ////new local tax b text box is added
                this.LocalTaxBTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctBPendingColumn].ToString();
                this.AccountLocalInterestTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalIntPendingColumn].ToString();
                this.AccountLocalPenaltyTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalPenPendingColumn].ToString();
                this.AccountStateTaxTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateTaxPendingColumn].ToString();
                this.AccountStateInterestTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateIntPendingColumn].ToString();
                this.AccountStatePenaltyTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StatePenPendingColumn].ToString();

                bool tempStateAdminFeePending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateAdminAcctPendingColumn].ToString());
                bool tempAdminFeePending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalAdminAcctPendingColumn].ToString());
                bool tempTransFeePending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TransFeePendingColumn].ToString());
                bool tempTechFeePending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeePendingColumn].ToString());
                bool tempLocalTaxPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctAPendingColumn].ToString());
                bool tempLocalTaxBPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxAcctBPendingColumn].ToString());
                bool tempLocalIntPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalIntPendingColumn].ToString());
                bool tempLocalPenPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalPenPendingColumn].ToString());
                bool tempStateTaxPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateTaxPendingColumn].ToString());
                bool tempStateIntPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateIntPendingColumn].ToString());
                bool tempStatePenPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StatePenPendingColumn].ToString());

                this.AccountInfoBackColor(this.AccountStateAdminFeePanel, tempStateAdminFeePending, this.AccountStateAdminFeeTextBox);
                this.AccountInfoBackColor(this.AccountAdminFeePanel, tempAdminFeePending, this.AccountAdminFeeTextBox);
                this.AccountInfoBackColor(this.AccountTransactionFeePanel, tempTransFeePending, this.AccountTransactionFeeTextBox);
                this.AccountInfoBackColor(this.AccountTechnologyFeePanel, tempTechFeePending, this.AccountTechnologyFeeTextBox);
                this.AccountInfoBackColor(this.AccountTotalTaxPanel, tempLocalTaxPending, this.AccountLocalTaxTextBox);
                this.AccountInfoBackColor(this.LocalTaxBTextBoxPanle, tempLocalTaxBPending, this.LocalTaxBTextBox);
                this.AccountInfoBackColor(this.AccountLocalInterestPanel, tempLocalIntPending, this.AccountLocalInterestTextBox);
                this.AccountInfoBackColor(this.AccountLocalPenaltyPanel, tempLocalPenPending, this.AccountLocalPenaltyTextBox);
                this.AccountInfoBackColor(this.AccountStateTaxPanel, tempStateTaxPending, this.AccountStateTaxTextBox);
                this.AccountInfoBackColor(this.AccountStateInterestPanel, tempStateIntPending, this.AccountStateInterestTextBox);
                this.AccountInfoBackColor(this.AccountStatePenaltyPanel, tempStatePenPending, this.AccountStatePenaltyTextBox);
                this.EnableControls(true);
                this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.DistrictCopyButton.Enabled = true;
            }
            else
            {
                this.ClearExciseDetails();
                this.EnableControls(false);
                this.NullRecords = true;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            }

            this.formLoad = false;
        }

        #endregion

        #region New

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
            if (this.PermissionFiled.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.EnableControls(true);
                this.LockControls(false);
                this.DistrictCopyButton.Enabled = false;
                this.ClearExciseDetails();
                ////disable the LocalTaxBPercent values
                this.LocalTaxBPercentTextBox.LockKeyPress = true;
                this.LocalTaxBButton.Enabled = false;

                this.GetYear();
                this.LocationCodeTextBox.Focus();
                ////this.AdminFeesTextBox.Focus();
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.EnableControls(false);
                this.LockControls(true);
                this.DistrictCopyButton.Enabled = false;
                this.ClearExciseDetails();
                this.GetYear();
                this.LocationCodeTextBox.Focus();
                ////this.AdminFeesTextBox.Focus();
            }
        }

        #endregion

        #region Cancel

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.LocationCodeTextBox.Focus();
            this.GetExciseRateDetails();
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        /// <param name="textboxValue">The textbox value.</param>
        /// <returns>textboxValue</returns>
        private int ConvertStringToInt(string textboxValue)
        {
            int outValue = 0;
            if (!string.IsNullOrEmpty(textboxValue.Trim()))
            {
                int.TryParse(textboxValue.Trim(), out outValue);
            }

            return outValue;
        }

        /// <summary>
        /// Converts the string to decimal.
        /// </summary>
        /// <param name="textboxValue">The textbox value.</param>
        /// <returns>textboxValue</returns>
        private decimal ConvertStringToDecimal(string textboxValue)
        {
            decimal outValue = 0;
            if (!string.IsNullOrEmpty(textboxValue.Trim()))
            {
                decimal.TryParse(textboxValue.Trim(), out outValue);
            }

            return outValue;
        }

        /// <summary>
        /// Checks the required fields.
        /// </summary>
        /// <returns>the Reqried Fields String</returns>
        private Control CheckRequiredFields()
        {
            Control requiredControll = null;
            if (string.IsNullOrEmpty(this.LocationCodeTextBox.Text))
            {
                requiredControll = this.LocationCodeTextBox;
            }
            ////else if(this.LocationCodeTextBox.Text == "0")
            ////{
            ////   requiredControll = this.LocationCodeTextBox;
            ////   this.LocationCodeTextBox.Text = string.Empty;
            ////}
            else if (string.IsNullOrEmpty(this.LocationNameTextBox.Text))
            {
                requiredControll = this.LocationNameTextBox;
            }
            else if (string.IsNullOrEmpty(this.StateAdminTextBox.Text.Trim()))
            {
                requiredControll = this.StateAdminTextBox;
            }
            else if (string.IsNullOrEmpty(this.AdminFeesTextBox.Text.Trim()))
            {
                requiredControll = this.AdminFeesTextBox;
            }
            else if (string.IsNullOrEmpty(this.TransactionFeesTextBox.Text.Trim()))
            {
                requiredControll = this.TransactionFeesTextBox;
            }
            else if (string.IsNullOrEmpty(this.TechFeesTextBox.Text.Trim()))
            {
                requiredControll = this.TechFeesTextBox;
            }
            else if (string.IsNullOrEmpty(this.YearTextBox.Text.Trim()))
            {
                requiredControll = this.YearTextBox;
            }
            else if (string.IsNullOrEmpty(this.LocalRateTextBox.Text.Trim()))
            {
                requiredControll = this.LocalRateTextBox;
            }
            else if (string.IsNullOrEmpty(this.LocalIsComboBox.SelectedValue.ToString()))
            {
                requiredControll = this.LocalIsComboBox;
            }
            ////else if (string.IsNullOrEmpty(this.TaxDistrictLinkLabel.Text.Trim()))
            ////{
            ////    requiredControll = this.TaxDistrictButton;
            ////}
            else if (string.IsNullOrEmpty(this.AccountStateAdminFeeTextBox.Text.Trim()))
            {
                requiredControll = this.AcctStateAdminFeeButton;
            }
            else if (string.IsNullOrEmpty(this.AccountAdminFeeTextBox.Text.Trim()))
            {
                requiredControll = this.AcctAdminFeeButton;
            }
            else if (string.IsNullOrEmpty(this.AccountTransactionFeeTextBox.Text.Trim()))
            {
                requiredControll = this.AcctTransFeeButton;
            }
            else if (string.IsNullOrEmpty(this.AccountTechnologyFeeTextBox.Text.Trim()))
            {
                requiredControll = this.AcctTechFeeButton;
            }
            else if (string.IsNullOrEmpty(this.AccountLocalTaxTextBox.Text.Trim()))
            {
                requiredControll = this.AcctTotalTaxButton;
            }

            ////added by purushotham
            else if (this.TechnologyFeePercentTextBox.DecimalTextBoxValue < 100 && string.IsNullOrEmpty(this.TechnologyFeeBPercentTextBox.Text.Trim()))
            {
                requiredControll = this.AccountTechFeeBButton;
            }
            else if (string.IsNullOrEmpty(this.AccountTechnologyFeeBTextBox.Text.Trim()))
            {
                decimal acctext;
                var temp = (this.TechnologyFeeBPercentTextBox.Text);
                decimal.TryParse(this.TechnologyFeeBPercentTextBox.Text, out acctext);
                if (acctext > 0)
                {
                    this.TechnologyAccountFeeBPanel.Enabled = true;
                    requiredControll = this.AccountTechFeeBButton;
                }
            }
            else if (this.LocalTaxPercentTextBox.DecimalTextBoxValue < 100 && string.IsNullOrEmpty(this.LocalTaxBTextBox.Text.Trim()))
            {
                requiredControll = this.LocalTaxBButton;
            }
            else if (string.IsNullOrEmpty(this.AccountLocalInterestTextBox.Text.Trim()))
            {
                requiredControll = this.AcctLocalIntButton;
            }
            else if (string.IsNullOrEmpty(this.AccountLocalPenaltyTextBox.Text.Trim()))
            {
                requiredControll = this.AcctLocalPenButton;
            }
            else if (string.IsNullOrEmpty(this.AccountStateTaxTextBox.Text.Trim()))
            {
                requiredControll = this.AcctStateTaxButton;
            }
            else if (string.IsNullOrEmpty(this.AccountStateInterestTextBox.Text.Trim()))
            {
                requiredControll = this.AcctStateIntButton;
            }
            else if (string.IsNullOrEmpty(this.AccountStatePenaltyTextBox.Text.Trim()))
            {
                requiredControll = this.AcctStatePenButton;
            }

            return requiredControll;
        }

        /// <summary>
        /// Checks the local tax B required.
        /// </summary>
        /// <returns>boolian</returns>
        private bool CheckLocTaxBRequired()
        {
            decimal tempTotalLocaltaxValue;

            tempTotalLocaltaxValue = this.LocalTaxPercentTextBox.DecimalTextBoxValue + this.LocalTaxBPercentTextBox.DecimalTextBoxValue;

            if (this.LocalTaxPercentTextBox.DecimalTextBoxValue == 0 || this.LocalTaxPercentTextBox.DecimalTextBoxValue > 100 || this.LocalTaxBPercentTextBox.DecimalTextBoxValue > 100)
            {
                return false;
            }
            else if (tempTotalLocaltaxValue > 100 || tempTotalLocaltaxValue < 100)
            {
                return false;
            }

            return true;
        }

        private bool CheckTechPercent()
        {
            decimal tempTechpercent;

            tempTechpercent = this.TechnologyFeeBPercentTextBox.DecimalTextBoxValue + this.TechnologyFeePercentTextBox.DecimalTextBoxValue;

            if (this.TechnologyFeeBPercentTextBox.DecimalTextBoxValue > 100 || this.TechnologyFeePercentTextBox.DecimalTextBoxValue > 100 || this.TechnologyFeePercentTextBox.DecimalTextBoxValue == 0)
            {
                return false;
            }
            else if ((tempTechpercent > 100) || (tempTechpercent < 100))
            {
                return false;
            }
            return true;

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

            int.TryParse(this.YearTextBox.Text, out this.tempYear);
            if (this.tempYear == 0)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidFieldYear");
                return sliceValidationFields;
            }

            Control requiredControl;

            requiredControl = this.CheckRequiredFields();

            if (requiredControl != null)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("ExciseRateSaveMissingField");
                requiredControl.Focus();
                return sliceValidationFields;
            }

            ////validation stands for transactionFees and techFees                  
            decimal maxMoneyValue = (decimal)int.MaxValue;

            // checks for - smallmoney datatype range
            maxMoneyValue = maxMoneyValue / 10000;

            if (maxMoneyValue < this.TransactionFeesTextBox.DecimalTextBoxValue)
            {
                sliceValidationFields.ErrorMessage = String.Concat(TransactionFeesTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidAmount")) + "\n";
                this.TransactionFeesTextBox.Text = "0.00";
                this.TransactionFeesTextBox.Focus();
                return sliceValidationFields;
            }

            if (maxMoneyValue < this.TechFeesTextBox.DecimalTextBoxValue)
            {
                sliceValidationFields.ErrorMessage = sliceValidationFields.ErrorMessage + String.Concat(TechFeesTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidAmount"));
                this.TechFeesTextBox.Text = "0.00";
                this.TechFeesTextBox.Focus();
                return sliceValidationFields;
            }

            if (Convert.ToDouble(this.LocalRateTextBox.DecimalTextBoxValue) > 100.00)
            {
                sliceValidationFields.ErrorMessage = sliceValidationFields.ErrorMessage + String.Concat(LocalRateTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidAmount"));
                this.LocalRateTextBox.Text = "0.00";
                this.LocalRateTextBox.Focus();
                return sliceValidationFields;
            }

            if (Convert.ToDouble(this.StateAdminTextBox.DecimalTextBoxValue) > 100.00)
            {
                sliceValidationFields.ErrorMessage = sliceValidationFields.ErrorMessage + String.Concat(StateAdminTextBox.Tag, " ", SharedFunctions.GetResourceString("F15013InvalidFee"));
                StateAdminTextBox.Text = "0.00";
                StateAdminTextBox.Focus();
                return sliceValidationFields;
            }

            if (Convert.ToDouble(this.AdminFeesTextBox.DecimalTextBoxValue) > 100.00)
            {
                sliceValidationFields.ErrorMessage = sliceValidationFields.ErrorMessage + String.Concat(AdminFeesTextBox.Tag, " ", SharedFunctions.GetResourceString("F15013InvalidFee"));
                AdminFeesTextBox.Text = "0.00";
                AdminFeesTextBox.Focus();
                return sliceValidationFields;
            }

            /////to chcek required fields
            if (!this.CheckLocTaxBRequired())
            {
                sliceValidationFields.ErrorMessage = sliceValidationFields.ErrorMessage + string.Concat("Local Tax A should be greater than 0 and Total Local Tax should be equal to 100.0 %");
                this.LocalTaxPercentTextBox.Focus();
                return sliceValidationFields;
            }

            if (!this.CheckTechPercent())
            {
                sliceValidationFields.ErrorMessage = sliceValidationFields.ErrorMessage + string.Concat("Technology Fee A should be greater than 0 and Total Technology Fee should be equal to 100.0%");
                this.TechnologyFeePercentTextBox.Focus();
                return sliceValidationFields;
            }
            ////check the account year are same otherwise alter via messagebox
            if (this.CheckExciseRateRecord() < 0)
            {
                DialogResult currentResult = MessageBox.Show("Some of the Accounts referenced by this record have a different year\nthan this Excise Rate District. Should the\nsame accounts for the appropriate year be identified and used instead?\nAny missing records will be created as Pending.", "TerraScan – Account Year Mismatch", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (DialogResult.Cancel == currentResult)
                {
                    sliceValidationFields.DisableNewMethod = true;
                }
                else if (DialogResult.OK == currentResult)
                {
                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    sliceValidationFields.RequiredFieldMissing = false;
                    return sliceValidationFields;
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            this.SaveExciseRateRecord();
        }

        /// <summary>
        /// Checks the excise rate record.
        /// </summary>
        /// <returns>boolean value</returns>
        private int CheckExciseRateRecord()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int tempExciseRateID;
                int returnValue = -1;
                decimal localTaxApercent;
                decimal localtaxBPercent;
                decimal techFeeA;
                decimal techFeeB;
                F15013ExciseTaxRateData checkExciseTaxRateData = new F15013ExciseTaxRateData();
                F15013ExciseTaxRateData.SaveExciseTaxRateRow dr1 = checkExciseTaxRateData.SaveExciseTaxRate.NewSaveExciseTaxRateRow();
                tempExciseRateID = this.ConvertStringToInt(this.RateDistrictIDTextBox.Text);
                if (tempExciseRateID > 0)
                {
                    dr1.ExciseRateID = this.ConvertStringToInt(this.RateDistrictIDTextBox.Text);
                }

                dr1.LocationCode = this.LocationCodeTextBox.Text.Trim();
                dr1.LocationName = this.LocationNameTextBox.Text.Trim();
                dr1.StateAdminFee = this.ConvertStringToDecimal(this.StateAdminTextBox.Text.Replace("%", string.Empty));
                dr1.LocalAdminFee = this.ConvertStringToDecimal(this.AdminFeesTextBox.Text.Replace("%", string.Empty));
                dr1.TransFee = this.ConvertStringToDecimal(this.TransactionFeesTextBox.Text.Replace("$", string.Empty));
                dr1.TechFee = this.ConvertStringToDecimal(this.TechFeesTextBox.Text.Replace("$", string.Empty));
                dr1.Year = this.tempYear;
                dr1.LocalTaxRate = this.ConvertStringToDecimal(this.LocalRateTextBox.Text.Replace("%", string.Empty));
                dr1.IsCounty = this.ConvertStringToInt(this.LocalIsComboBox.SelectedValue.ToString());
                ////dr.DistrictID = this.ConvertStringToInt(this.TaxDistrictLinkLabel.Tag.ToString());
                dr1.Description = this.DescriptionTextBox.Text;
                dr1.StateAdminAcct = this.ConvertStringToInt(this.AccountStateAdminFeeTextBox.Tag.ToString());
                dr1.LocalAdminAcct = this.ConvertStringToInt(this.AccountAdminFeeTextBox.Tag.ToString());
                dr1.TransFeeAcct = this.ConvertStringToInt(this.AccountTransactionFeeTextBox.Tag.ToString());
                dr1.TechFeeAcct = this.ConvertStringToInt(this.AccountTechnologyFeeTextBox.Tag.ToString());
                dr1.LocalTaxAcctA = this.ConvertStringToInt(this.AccountLocalTaxTextBox.Tag.ToString());

                decimal.TryParse(this.LocalTaxPercentTextBox.Text.Trim(), out localTaxApercent);
                decimal.TryParse(this.LocalTaxBPercentTextBox.Text.Trim(), out localtaxBPercent);
                decimal.TryParse(this.TechnologyFeePercentTextBox.Text.Trim(), out techFeeA);
                decimal.TryParse(this.TechnologyFeeBPercentTextBox.Text.Trim(), out techFeeB);
                dr1.LocalTaxPercentA = localTaxApercent;
                dr1.LocalTaxPercentB = localtaxBPercent;
                dr1.TechFeePercentA = techFeeA;
                dr1.TechFeePercentB = techFeeB;
                dr1.TechFeeAcctB = this.AccountTechnologyFeeBTextBox.Tag.ToString();

                ////dr1.LocalTaxPercentA = this.ConvertStringToDecimal(this.LocalTaxPercentTextBox.Text.Replace("$", string.Empty));
                ////dr1.LocalTaxPercentB = this.ConvertStringToDecimal(this.LocalTaxBPercentTextBox.Text.Replace("$", string.Empty));
                if (this.LocalTaxPercentTextBox.DecimalTextBoxValue < 100)
                {
                    dr1.LocalTaxAcctB = this.ConvertStringToInt(this.LocalTaxBTextBox.Tag.ToString());
                }

                dr1.LocalIntAcct = this.ConvertStringToInt(this.AccountLocalInterestTextBox.Tag.ToString());
                dr1.LocalPenAcct = this.ConvertStringToInt(this.AccountLocalPenaltyTextBox.Tag.ToString());
                dr1.StateTaxAcct = this.ConvertStringToInt(this.AccountStateTaxTextBox.Tag.ToString());
                dr1.StateIntAcct = this.ConvertStringToInt(this.AccountStateInterestTextBox.Tag.ToString());
                dr1.StatePenAcct = this.ConvertStringToInt(this.AccountStatePenaltyTextBox.Tag.ToString());
                dr1.IsSaveProcess = 0;

                checkExciseTaxRateData.SaveExciseTaxRate.Rows.Clear();
                checkExciseTaxRateData.SaveExciseTaxRate.Rows.Add(dr1);
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(checkExciseTaxRateData.SaveExciseTaxRate.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                if (string.IsNullOrEmpty(this.RateDistrictIDTextBox.Text.Trim()))
                {
                    returnValue = this.form15013Control.WorkItem.F15013_SaveExciseTaxRate(0, tempDataSet.GetXml(), TerraScanCommon.UserId);
                }
                else
                {
                    returnValue = this.form15013Control.WorkItem.F15013_SaveExciseTaxRate(Convert.ToInt32(this.RateDistrictIDTextBox.Text), tempDataSet.GetXml(), TerraScanCommon.UserId);
                }

                /////this.keyId = returnValue;

                ////this.pageMode = TerraScanCommon.PageModeTypes.View;
                ////SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                ////sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                ////sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                ////this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                ////if (onclose)
                ////{
                ////    return true;
                ////}

                ////return true;

                return returnValue;
            }
            catch (SoapException ex1)
            {
                ////ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return -1;
            }
            catch (Exception ex)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return -1;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Saves the excise rate record.
        /// </summary>
        private void SaveExciseRateRecord()
        {
            int tempExciseRateID;
            int returnValue = -1;
            decimal techFeeA;
            decimal techFeeB;
            F15013ExciseTaxRateData saveExciseTaxRateData = new F15013ExciseTaxRateData();
            F15013ExciseTaxRateData.SaveExciseTaxRateRow dr = saveExciseTaxRateData.SaveExciseTaxRate.NewSaveExciseTaxRateRow();
            tempExciseRateID = this.ConvertStringToInt(this.RateDistrictIDTextBox.Text);
            if (tempExciseRateID > 0)
            {
                dr.ExciseRateID = this.ConvertStringToInt(this.RateDistrictIDTextBox.Text);
            }

            dr.LocationCode = this.LocationCodeTextBox.Text.Trim();
            dr.LocationName = this.LocationNameTextBox.Text.Trim();
            dr.StateAdminFee = this.ConvertStringToDecimal(this.StateAdminTextBox.Text.Replace("%", string.Empty));
            dr.LocalAdminFee = this.ConvertStringToDecimal(this.AdminFeesTextBox.Text.Replace("%", string.Empty));
            dr.TransFee = this.ConvertStringToDecimal(this.TransactionFeesTextBox.Text.Replace("$", string.Empty));
            dr.TechFee = this.ConvertStringToDecimal(this.TechFeesTextBox.Text.Replace("$", string.Empty));
            dr.Year = this.tempYear;
            dr.LocalTaxRate = this.ConvertStringToDecimal(this.LocalRateTextBox.Text.Replace("%", string.Empty));
            dr.IsCounty = this.ConvertStringToInt(this.LocalIsComboBox.SelectedValue.ToString());
            ////dr.DistrictID = this.ConvertStringToInt(this.TaxDistrictLinkLabel.Tag.ToString());
            dr.Description = this.DescriptionTextBox.Text;
            dr.StateAdminAcct = this.ConvertStringToInt(this.AccountStateAdminFeeTextBox.Tag.ToString());
            dr.LocalAdminAcct = this.ConvertStringToInt(this.AccountAdminFeeTextBox.Tag.ToString());
            dr.TransFeeAcct = this.ConvertStringToInt(this.AccountTransactionFeeTextBox.Tag.ToString());
            dr.TechFeeAcct = this.ConvertStringToInt(this.AccountTechnologyFeeTextBox.Tag.ToString());
            dr.LocalTaxAcctA = this.ConvertStringToInt(this.AccountLocalTaxTextBox.Tag.ToString());
            dr.LocalTaxPercentA = this.ConvertStringToDecimal(this.LocalTaxPercentTextBox.Text.Replace("$", string.Empty));
            dr.LocalTaxPercentB = this.ConvertStringToDecimal(this.LocalTaxBPercentTextBox.Text.Replace("$", string.Empty));
            if (this.LocalTaxPercentTextBox.DecimalTextBoxValue < 100)
            {
                dr.LocalTaxAcctB = this.ConvertStringToInt(this.LocalTaxBTextBox.Tag.ToString());
            }

            dr.LocalIntAcct = this.ConvertStringToInt(this.AccountLocalInterestTextBox.Tag.ToString());
            dr.LocalPenAcct = this.ConvertStringToInt(this.AccountLocalPenaltyTextBox.Tag.ToString());
            dr.StateTaxAcct = this.ConvertStringToInt(this.AccountStateTaxTextBox.Tag.ToString());
            dr.StateIntAcct = this.ConvertStringToInt(this.AccountStateInterestTextBox.Tag.ToString());
            dr.StatePenAcct = this.ConvertStringToInt(this.AccountStatePenaltyTextBox.Tag.ToString());
            decimal.TryParse(this.TechnologyFeePercentTextBox.Text.Trim(), out techFeeA);
            decimal.TryParse(this.TechnologyFeeBPercentTextBox.Text.Trim(), out techFeeB);
            dr.TechFeePercentA = techFeeA;
            dr.TechFeePercentB = techFeeB;
            dr.TechFeeAcctB = this.AccountTechnologyFeeBTextBox.Tag.ToString();
            dr.IsSaveProcess = 1;
            saveExciseTaxRateData.SaveExciseTaxRate.Rows.Clear();
            saveExciseTaxRateData.SaveExciseTaxRate.Rows.Add(dr);
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(saveExciseTaxRateData.SaveExciseTaxRate.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            string xml = tempDataSet.GetXml();
            if (string.IsNullOrEmpty(this.RateDistrictIDTextBox.Text.Trim()))
            {
                returnValue = this.form15013Control.WorkItem.F15013_SaveExciseTaxRate(0, tempDataSet.GetXml(), TerraScanCommon.UserId);
            }
            else
            {
                returnValue = this.form15013Control.WorkItem.F15013_SaveExciseTaxRate(Convert.ToInt32(this.RateDistrictIDTextBox.Text), tempDataSet.GetXml(), TerraScanCommon.UserId);
            }

            this.keyId = returnValue;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButton_Click()
        {
            //// this.Cursor = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(this.RateDistrictIDTextBox.Text.Trim()))
            {
                if (this.form15013Control.WorkItem.F15013_DeleteExciseTaxRate(Convert.ToInt32(this.RateDistrictIDTextBox.Text), TerraScanCommon.UserId) < 0)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("DeleteValidation"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Dont allow to remove keyid from QE Grid
                    this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));
                }

                this.GetExciseRateDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
            }
        }

        #endregion

        #region SetDefaultState

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void EnableControls(bool enableValue)
        {
            this.DistrictInfoPanel.Enabled = enableValue;
            this.AccountInfoPanel.Enabled = enableValue;
            this.DistrictCopyButton.Enabled = enableValue;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockControls(bool lockControl)
        {
            //// District Info LockControls
            this.LocationCodeTextBox.LockKeyPress = lockControl;
            this.LocationNameTextBox.LockKeyPress = lockControl;
            this.StateAdminTextBox.LockKeyPress = lockControl;
            this.AdminFeesTextBox.LockKeyPress = lockControl;
            this.StateAdminTextBox.LockKeyPress = lockControl;
            this.TransactionFeesTextBox.LockKeyPress = lockControl;
            this.TechFeesTextBox.LockKeyPress = lockControl;
            this.YearTextBox.LockKeyPress = lockControl;
            this.LocalRateTextBox.LockKeyPress = lockControl;
            this.LocalIsComboBox.Enabled = !lockControl;
            this.DescriptionTextBox.LockKeyPress = lockControl;
            this.TaxDistrictButton.Enabled = !lockControl;
            this.StateAdminTextBox.LockKeyPress = lockControl;

            ////Percent textbox for Local tax A and Local Tax B
            this.LocalTaxPercentTextBox.LockKeyPress = lockControl;

            ////when the value is zero disable the controls
            if (this.LocalTaxPercentTextBox.DecimalTextBoxValue >= 100)
            {
                this.LocalTaxBTextBox.BackColor = Color.White;
                this.LocalTaxBTextBoxPanle.BackColor = Color.White;
                this.LocalTaxBPercentTextBox.LockKeyPress = true;
                this.LocalTaxBButton.Enabled = false;
            }
            else
            {
                this.LocalTaxBPercentTextBox.LockKeyPress = lockControl;
                this.LocalTaxBButton.Enabled = !lockControl;
            }

            //added bu purushotham
            this.TechnologyFeePercentTextBox.LockKeyPress = lockControl;

            if (this.TechnologyFeePercentTextBox.DecimalTextBoxValue >= 100)
            {
                this.AccountTechnologyFeeBTextBox.BackColor = Color.White;
                this.TechnologyAccountFeeBPanel.BackColor = Color.White;
                this.TechnologyFeeBPercentTextBox.LockKeyPress = true;
                this.AccountTechFeeBButton.Enabled = false;
            }
            else
            {
                this.TechnologyFeeBPercentTextBox.LockKeyPress = lockControl;
                this.AccountTechFeeBButton.Enabled = !lockControl;
            }

            //if (this.TechnologyFeeBPercentTextBox.DecimalTextBoxValue > 0)
            //{
            //    this.TechnologyAccountFeeBPanel.Enabled = true;
            //}
            //else
            //{
            //    this.TechnologyAccountFeeBPanel.Enabled = false;
            //}
            //if (this.TechnologyFeePercentTextBox.DecimalTextBoxValue > 0)
            //{
            //    this.AccountTechnologyFeePanel.Enabled = true;
            //}
            //else
            //{
            //    this.AccountTechnologyFeePanel.Enabled = false;
            //}

            //// Account Info LockControls
            this.AcctStateAdminFeeButton.Enabled = !lockControl;
            this.AcctAdminFeeButton.Enabled = !lockControl;
            this.AcctTransFeeButton.Enabled = !lockControl;
            this.AcctTechFeeButton.Enabled = !lockControl;
            this.AcctTotalTaxButton.Enabled = !lockControl;

            this.AcctLocalIntButton.Enabled = !lockControl;
            this.AcctLocalPenButton.Enabled = !lockControl;
            this.AcctStateTaxButton.Enabled = !lockControl;
            this.AcctStateIntButton.Enabled = !lockControl;
            this.AcctStatePenButton.Enabled = !lockControl;
        }

        private void EnablingTechFeeB()
        {
            if (this.TechnologyFeeBPercentTextBox.DecimalTextBoxValue > 0)
            {
                this.TechnologyAccountFeeBPanel.Enabled = true;
            }
            else
            {
                this.TechnologyAccountFeeBPanel.Enabled = false;
            }
        }
        /// <summary>
        /// Clears the excise details.
        /// </summary>
        private void ClearExciseDetails()
        {
            //// District Info LockControls
            this.StateAdminTextBox.TextCustomFormat = "0.00 %";
            this.AdminFeesTextBox.TextCustomFormat = "0.00 %";
            this.StateAdminTextBox.Text = "0";
            this.AdminFeesTextBox.Text = "0";
            this.StateAdminTextBox.TextCustomFormat = "0.00 %";
            this.StateAdminTextBox.Text = "0";
            this.LocalRateTextBox.TextCustomFormat = "0.00 %";
            this.LocalRateTextBox.Text = "0";
            this.LocalIsComboBox.SelectedValue = 1;
            if (this.exciseRateIds.ListExciseTaxRate.Rows.Count > 0 || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.TransactionFeesTextBox.Text = "5";
                this.TechFeesTextBox.Text = "5";
            }
            else
            {
                this.TransactionFeesTextBox.Text = "0";
                this.TechFeesTextBox.Text = "0";
            }

            this.LocationCodeTextBox.Text = string.Empty;
            this.LocationNameTextBox.Text = string.Empty;
            this.RateDistrictIDTextBox.Text = string.Empty;
            this.YearTextBox.Text = string.Empty;
            this.LocalIsComboBox.Text = string.Empty;
            this.LocalIsTextBox.Text = string.Empty;
            this.TotalTaxRateTextBox.Text = string.Empty;
            this.TaxDistrictLinkLabel.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;

            ////Newly added Loacal Tax A and b percent text Box
            this.LocalTaxPercentTextBox.TextCustomFormat = "0.0";
            this.LocalTaxBPercentTextBox.TextCustomFormat = "0.0";
            this.LocalTaxPercentTextBox.Text = "0";
            this.LocalTaxBPercentTextBox.Text = "0";
            this.LocalTaxPercentTextBox.Text = string.Empty;
            this.LocalTaxBPercentTextBox.Text = string.Empty;

            this.TechnologyFeePercentTextBox.TextCustomFormat = "0.0";
            this.TechnologyFeeBPercentTextBox.TextCustomFormat = "0.0";
            this.TechnologyFeeBPercentTextBox.Text = "50";
            this.TechnologyFeePercentTextBox.Text = "50";
            this.AccountTechnologyFeeTextBox.Text = string.Empty;
            this.AccountTechnologyFeeBTextBox.Text = string.Empty;
            this.EnablingTechFeeB();
            //// Account Info LockControls  
            this.AccountStateAdminFeeTextBox.Text = string.Empty;
            this.AccountAdminFeeTextBox.Text = string.Empty;
            this.AccountTransactionFeeTextBox.Text = string.Empty;
            this.AccountTechnologyFeeTextBox.Text = string.Empty;
            this.AccountLocalTaxTextBox.Text = string.Empty;
            this.LocalTaxPercentTextBox.Text = string.Empty;
            this.LocalTaxBTextBox.Text = string.Empty;
            this.LocalTaxBPercentTextBox.Text = string.Empty;

            this.AccountLocalInterestTextBox.Text = string.Empty;
            this.AccountLocalPenaltyTextBox.Text = string.Empty;
            this.AccountStateTaxTextBox.Text = string.Empty;
            this.AccountStateInterestTextBox.Text = string.Empty;
            this.AccountStatePenaltyTextBox.Text = string.Empty;

            this.AccountStateAdminFeeTextBox.BackColor = Color.White;
            this.AccountAdminFeeTextBox.BackColor = Color.White;
            this.AccountTransactionFeeTextBox.BackColor = Color.White;
            this.AccountTechnologyFeeTextBox.BackColor = Color.White;
            this.AccountLocalTaxTextBox.BackColor = Color.White;
            this.LocalTaxBTextBox.BackColor = Color.White;
            this.AccountLocalInterestTextBox.BackColor = Color.White;
            this.AccountLocalPenaltyTextBox.BackColor = Color.White;
            this.AccountStateTaxTextBox.BackColor = Color.White;
            this.AccountStateInterestTextBox.BackColor = Color.White;
            this.AccountStatePenaltyTextBox.BackColor = Color.White;

            this.AccountStateAdminFeePanel.BackColor = Color.White;
            this.AccountAdminFeePanel.BackColor = Color.White;
            this.AccountTransactionFeePanel.BackColor = Color.White;
            this.AccountTechnologyFeePanel.BackColor = Color.White;
            this.AccountTotalTaxPanel.BackColor = Color.White;
            this.LocalTaxBTextBoxPanle.BackColor = Color.White;
            this.AccountLocalInterestPanel.BackColor = Color.White;
            this.AccountLocalPenaltyPanel.BackColor = Color.White;
            this.AccountStateTaxPanel.BackColor = Color.White;
            this.AccountStateInterestPanel.BackColor = Color.White;
            this.AccountStatePenaltyPanel.BackColor = Color.White;
        }

        #endregion SetDefaultState

        #region Methods

        /// <summary>
        /// Edits the record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditRecord(object sender, EventArgs e)
        {
            if (!this.formLoad)
            {
                this.SetEditRecord();
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.DistrictCopyButton.Enabled = false;
            }
        }

        /// <summary>
        /// Gets the account details.
        /// </summary>
        /// <param name="buttonControl">Name of the control.</param>
        private void GetAccountDetails(Button buttonControl)
        {
            bool tempAccountStatus;
            int currentYear;
            ////here this.accountId i the account type
            this.accountId = 1;
            int.TryParse(this.YearTextBox.Text.Trim(), out currentYear);

            this.selectedAccountName = null;
            ////object[] optionalParameter = new object[] { this.YearTextBox.Text.Trim(), this.accountId };
            object[] optionalParameter = new object[] { currentYear, this.accountId };
            Form accountSelectionForm = this.form15013Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form15013Control.WorkItem);
            if (accountSelectionForm != null)
            {
                if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out this.accountId);
                    F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();
                    accountNameDataSet = this.form15013Control.WorkItem.F15013_GetAccountName(this.accountId);

                    if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                    {
                        tempAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AccountStatusColumn]);
                        this.selectedAccountName = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AccountNameColumn].ToString();
                        if (!string.IsNullOrEmpty(buttonControl.Tag.ToString()))
                        {
                            Control tempTextBoxControl = this.SearchControlWithKey(this.AccountInfoPanel, buttonControl.Tag.ToString());
                            tempTextBoxControl.Text = this.selectedAccountName;
                            tempTextBoxControl.Tag = this.accountId;
                            this.AccountInfoBackColor((Panel)buttonControl.Parent, tempAccountStatus, tempTextBoxControl);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Accounts the color of the info back.
        /// </summary>
        /// <param name="panelControl">The panel control.</param>
        /// <param name="tempAccountStatus">if set to <c>true</c> [temp account status].</param>
        /// <param name="tempTextBoxControl">The temp text box control.</param>
        private void AccountInfoBackColor(Panel panelControl, bool tempAccountStatus, Control tempTextBoxControl)
        {
            if (tempAccountStatus == false)
            {
                panelControl.BackColor = Color.White;
                tempTextBoxControl.BackColor = Color.White;
            }
            else
            {
                tempTextBoxControl.BackColor = Color.FromArgb(187, 222, 173);
                panelControl.BackColor = Color.FromArgb(187, 222, 173);
            }
        }

        /// <summary>
        /// set focus to the next/previous input field
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        /// <param name="key">The key.</param>
        /// <returns>sourceControl and key</returns>
        private Control SearchControlWithKey(Control sourceControl, string key)
        {
            Control requiredControl = sourceControl;

            if (sourceControl != null)
            {
                if (sourceControl.Controls.ContainsKey(key))
                {
                    return sourceControl.Controls[key];
                }

                foreach (Control sampControl in sourceControl.Controls)
                {
                    if (sampControl.Controls.Count > 0)
                    {
                        requiredControl = this.SearchControlWithKey(sampControl, key);
                        if (requiredControl.Name.Equals(key))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                requiredControl = new Control();
            }

            return requiredControl;
        }

        /// <summary>
        /// Check the page Status when the New Reciept is Cancelled
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>boolean Value</returns>
        ////private bool CheckPageStatus(bool onclose)
        ////{
        ////    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
        ////    {
        ////        DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        ////        if (dialogResult == DialogResult.Yes)
        ////        {
        ////            bool status = this.SaveExciseRateRecord(onclose);

        ////            if (status)
        ////            {
        ////                this.pageMode = TerraScanCommon.PageModeTypes.View;
        ////            }

        ////            return status;
        ////        }
        ////        else if (dialogResult == DialogResult.No)
        ////        {
        ////            if (onclose)
        ////            {
        ////                this.pageMode = TerraScanCommon.PageModeTypes.View;
        ////            }
        ////            else
        ////            {
        ////                this.CancelButton_Click();
        ////            }

        ////            return true;
        ////        }
        ////        else
        ////        {
        ////            return false;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        return true;
        ////    }
        ////}

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Click event of the TaxDistrictButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxDistrictButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                int districtId = 0;
                object[] optionalParameter = new object[] { this.YearTextBox.Text.Trim(), this.ParentFormId };
                Form districtSelectionForm = this.form15013Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1512, optionalParameter, this.form15013Control.WorkItem);
                if (districtSelectionForm != null)
                {
                    if (districtSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        int.TryParse(TerraScanCommon.GetValue(districtSelectionForm, "DistrictId").ToString(), out districtId);
                        F15013ExciseTaxRateData districtNameDataSet = new F15013ExciseTaxRateData();
                        districtNameDataSet = this.form15013Control.WorkItem.F15013_GetDistrictName(districtId);

                        if (districtNameDataSet.GetDistrictName.Rows.Count > 0)
                        {
                            this.TaxDistrictLinkLabel.Text = districtNameDataSet.GetDistrictName.Rows[0][districtNameDataSet.GetDistrictName.DistrictNameColumn].ToString();
                            this.TaxDistrictLinkLabel.Tag = districtId;
                        }
                        else
                        {
                            this.TaxDistrictLinkLabel.Text = string.Empty;
                            this.TaxDistrictLinkLabel.Tag = string.Empty;
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
        /// Handles the Click event of the AccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button tempButton = new Button();
                tempButton = (Button)sender;

                this.SetEditRecord();
                if (tempButton != null)
                {
                    this.GetAccountDetails(tempButton);
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
        /// Handles the LinkClicked event of the AccountManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AccountManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11007);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LocalIsComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocalIsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.SetEditRecord();
        }

        /// <summary>
        /// Districts the copy.
        /// </summary>
        private void DistrictCopy()
        {
            this.SetEditRecord();
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.EnableControls(true);
            this.LockControls(false);
            this.DistrictCopyButton.Enabled = false;
            this.RateDistrictIDTextBox.Text = string.Empty;
            this.TotalTaxRateTextBox.Text = string.Empty;
            this.LocationCodeTextBox.Focus();

            // BugID 1153 
            //Begin
            //Get the Parent form Controls (9030) 
            if (this.Parent != null && this.Parent.Parent != null && this.Parent.Parent.Parent != null && this.Parent.Parent.Parent.Controls != null)
            {
                // SharedFunctions.GetResourceString("CancelForm")
                // Get the reportsmartpart 
                Control[] reportControl = this.Parent.Parent.Parent.Controls.Find(SharedFunctions.GetResourceString("F15013reportActionSmartPartWorkSpace"), true);

                // Check controls exist
                if (reportControl.Length > 0)
                {
                    // Disable the control
                    reportControl[0].Enabled = false;
                }

                // Get the OperationSmartPartWorkspace 
                Control[] operationSmartpart = this.Parent.Parent.Parent.Controls.Find(SharedFunctions.GetResourceString("F15013additionalOperationSmartPartWorkspace"), true);

                // Check controls exist
                if (operationSmartpart.Length > 0)
                {   // Disable the control
                    operationSmartpart[0].Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictCopyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictCopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.slicePermissionField.newPermission)
                {
                    MessageBox.Show("This form is now in New Record mode. When you press\nsave, this will result in a completely new record.  Please change\nthis record as necessary (including selecting year-appropriate\naccounts) and then press save.", "TerraScan – District Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.districtCopy = true;
                    this.OnD9030_F9030_RaiseFormMasterNew(new TerraScan.Infrastructure.Interface.EventArgs<int>(this.masterFormNo));
                    this.DistrictCopy();
                    this.districtCopy = false;
                    // End
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the TaxDistrictLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void TaxDistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11002);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.TaxDistrictLinkLabel.Tag;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the TaxDistrictLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxDistrictLinkLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.TaxDistrictLinkLabel.Text))
                {
                    if (this.TaxDistrictLinkLabel.Text.Length > 60)
                    {
                        this.RatesToolTip.SetToolTip(this.TaxDistrictLinkLabel, this.TaxDistrictLinkLabel.Text);
                    }
                    else
                    {
                        this.RatesToolTip.RemoveAll();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// TextLeave
        /// </summary>
        /// <param name="txtValue">txtValue</param>
        /// <param name="txtBoxControl">txtBoxControl</param>
        private void TextLeave(string txtValue, TerraScanTextBox txtBoxControl)
        {
            decimal d = 0;
            txtValue = txtValue.Replace("%", "").Trim();
            decimal.TryParse(txtValue, out d);
            if (d != 0)
            {
                if (txtValue.IndexOf('.') != -1)
                {
                    string replaceStr;
                    replaceStr = txtValue.Replace("%", "").Trim();
                    ////if (txtBoxControl.Text.Substring((txtBoxControl.Text.Replace("%","")).IndexOf('.')).Length > 3)
                    ////if (txtValue.Substring(replaceStr.IndexOf('.')).Length > 3)
                    if (replaceStr.Substring(replaceStr.IndexOf('.')).Length > 3)
                    {
                        ////char[] ch = (txtBoxControl.Text.Substring(replaceStr.IndexOf('.') + 3).ToString()).ToCharArray();
                        char[] ch = (replaceStr.Substring(replaceStr.IndexOf('.') + 3).ToString()).ToCharArray();
                        int flag = 0;
                        this.nonZero = null;
                        for (int i = ch.Length; i > 0; i--)
                        {
                            if (ch[i - 1] == '0')
                            {
                                if (flag == 0)
                                {
                                    ch[i - 1].ToString().Replace("0", "");
                                }
                                else
                                {
                                    this.nonZero = ch[i - 1].ToString() + this.nonZero;
                                }
                            }
                            else
                            {
                                flag = 1;
                                this.nonZero = ch[i - 1].ToString() + this.nonZero;
                            }
                        }
                        ////this.concadinatedString = txtBoxControl.Text.Substring(0, replaceStr.IndexOf('.') + 3).ToString() + this.nonZero;
                        this.concadinatedString = replaceStr.Substring(0, replaceStr.IndexOf('.') + 3).ToString() + this.nonZero;
                        if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 3)
                        {
                            txtBoxControl.TextCustomFormat = "0.000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 4)
                        {
                            txtBoxControl.TextCustomFormat = "0.0000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 5)
                        {
                            txtBoxControl.TextCustomFormat = "0.00000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 6)
                        {
                            txtBoxControl.TextCustomFormat = "0.000000 %";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 7)
                        {
                            txtBoxControl.TextCustomFormat = "0.000000 %";
                        }
                        else
                        {
                            txtBoxControl.TextCustomFormat = "0.00 %";
                        }

                        txtBoxControl.Text = this.concadinatedString;
                    }
                    else
                    {
                        txtBoxControl.TextCustomFormat = "0.00 %";
                        txtBoxControl.Text = txtValue;
                    }
                }
                else
                {
                    txtBoxControl.TextCustomFormat = "0.00 %";
                    txtBoxControl.Text = txtValue;
                }
            }
            else
            {
                txtBoxControl.TextCustomFormat = "0.00 %";
                txtBoxControl.Text = txtValue;
            }
            //// }
        }

        /// <summary>
        /// TextLeave
        /// </summary>
        /// <param name="txtValue">txtValue</param>
        /// <param name="txtBoxControl">txtBoxControl</param>
        private void AccountTextLeave(string txtValue, TerraScanTextBox txtBoxControl)
        {
            decimal d = 0;
            txtValue = txtValue.Replace("%", "").Trim();
            decimal.TryParse(txtValue, out d);
            if (d != 0)
            {
                if (txtValue.IndexOf('.') != -1)
                {
                    string replaceStr;
                    replaceStr = txtValue.Replace("%", "").Trim();
                    ////if (txtBoxControl.Text.Substring((txtBoxControl.Text.Replace("%","")).IndexOf('.')).Length > 3)
                    ////if (txtValue.Substring(replaceStr.IndexOf('.')).Length > 3)
                    if (replaceStr.Substring(replaceStr.IndexOf('.')).Length > 2)
                    {
                        ////char[] ch = (txtBoxControl.Text.Substring(replaceStr.IndexOf('.') + 3).ToString()).ToCharArray();
                        char[] ch = (replaceStr.Substring(replaceStr.IndexOf('.') + 2).ToString()).ToCharArray();
                        int flag = 0;
                        this.nonZero = null;
                        for (int i = ch.Length; i > 0; i--)
                        {
                            if (ch[i - 1] == '0')
                            {
                                if (flag == 0)
                                {
                                    ch[i - 1].ToString().Replace("0", "");
                                }
                                else
                                {
                                    this.nonZero = ch[i - 1].ToString() + this.nonZero;
                                }
                            }
                            else
                            {
                                flag = 1;
                                this.nonZero = ch[i - 1].ToString() + this.nonZero;
                            }
                        }
                        ////this.concadinatedString = txtBoxControl.Text.Substring(0, replaceStr.IndexOf('.') + 3).ToString() + this.nonZero;
                        this.concadinatedString = replaceStr.Substring(0, replaceStr.IndexOf('.') + 2).ToString() + this.nonZero;
                        if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 2)
                        {
                            txtBoxControl.TextCustomFormat = "0.00";
                            return;
                        }

                        if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 3)
                        {
                            txtBoxControl.TextCustomFormat = "0.000";
                            return;
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 4)
                        {
                            txtBoxControl.TextCustomFormat = "0.0000";
                            return;
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 5)
                        {
                            txtBoxControl.TextCustomFormat = "0.00000";
                            return;
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 6)
                        {
                            txtBoxControl.TextCustomFormat = "0.000000";
                        }
                        else if (this.concadinatedString.Substring(this.concadinatedString.IndexOf('.') + 1).Length == 7)
                        {
                            txtBoxControl.TextCustomFormat = "0.000000";
                            return;
                        }
                        else
                        {
                            txtBoxControl.TextCustomFormat = "0.0";
                        }

                        txtBoxControl.Text = this.concadinatedString;
                    }
                    else
                    {
                        txtBoxControl.TextCustomFormat = "0.0";
                        txtBoxControl.Text = txtValue;
                    }
                }
                else
                {
                    txtBoxControl.TextCustomFormat = "0.0";
                    txtBoxControl.Text = txtValue;
                }
            }
            else
            {
                txtBoxControl.TextCustomFormat = "0.0";
                txtBoxControl.Text = txtValue;
            }
            //// }
        }

        /// <summary>
        /// LocalRateTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LocalRateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.TextLeave(this.LocalRateTextBox.Text, this.LocalRateTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// AdminFeesTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdminFeesTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.TextLeave(this.AdminFeesTextBox.Text, this.AdminFeesTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the LocalTaxPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocalTaxPercentTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.formLoad)
                {
                    decimal tempLocalTaxBPercentValue;

                    if (this.LocalTaxPercentTextBox.DecimalTextBoxValue >= 100)
                    {
                        this.LocalTaxBTextBox.BackColor = Color.White;
                        this.LocalTaxBTextBoxPanle.BackColor = Color.White;
                        this.LocalTaxBPercentTextBox.LockKeyPress = true;
                        this.LocalTaxBButton.Enabled = false;
                        this.LocalTaxBPercentTextBox.Text = string.Empty;
                        this.LocalTaxBTextBox.Text = string.Empty;
                    }
                    else
                    {
                        ////check page mode and apply permission to make the Text Box editable
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            this.LocalTaxBPercentTextBox.LockKeyPress = !this.PermissionFiled.newPermission;
                            this.LocalTaxBButton.Enabled = this.PermissionFiled.newPermission;
                        }
                        else
                        {
                            this.LocalTaxBPercentTextBox.LockKeyPress = !this.PermissionFiled.editPermission;
                            this.LocalTaxBButton.Enabled = this.PermissionFiled.editPermission;
                        }

                        tempLocalTaxBPercentValue = 100 - this.LocalTaxPercentTextBox.DecimalTextBoxValue;

                        if (tempLocalTaxBPercentValue >= 0)
                        {
                            this.AccountTextLeave(tempLocalTaxBPercentValue.ToString(), this.LocalTaxBPercentTextBox);
                            this.LocalTaxBPercentTextBox.Text = tempLocalTaxBPercentValue.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the StateAdminTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StateAdminTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.TextLeave(this.StateAdminTextBox.Text, this.StateAdminTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LocalTaxPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocalTaxPercentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.LocalTaxPercentTextBox.Text, this.LocalTaxPercentTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LocalTaxBPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocalTaxBPercentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.LocalTaxBPercentTextBox.Text, this.LocalTaxBPercentTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the LocalIsComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LocalIsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.formLoad)
            {
                this.SetEditRecord();
            }
        }

        #endregion Events

        //private void TechnologyFeePercentTextBox_Leave(object sender, EventArgs e)
        //{           
        //    if (this.TechnologyFeePercentTextBox.DecimalTextBoxValue > 0)
        //    {
        //        this.AccountTechnologyFeePanel.Enabled = true;
        //    }
        //    else
        //    {
        //        this.AccountTechnologyFeeTextBox.Text = string.Empty;
        //        this.AccountTechnologyFeePanel.Enabled = false;
        //    }
        //}

        //private void TechnologyFeeBPercentTextBox_Leave(object sender, EventArgs e)
        //{
        //    if (this.TechnologyFeeBPercentTextBox.DecimalTextBoxValue > 0)
        //    {
        //        this.TechnologyAccountFeeBPanel.Enabled = true;
        //    }
        //    else
        //    {
        //        this.AccountTechnologyFeeBTextBox.Text = string.Empty;
        //        this.TechnologyAccountFeeBPanel.Enabled = false;
        //    }
        //}

        private void TechnologyFeePercentTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.formLoad)
                {
                    decimal tempTechPercentBValue;

                    if (this.TechnologyFeePercentTextBox.DecimalTextBoxValue >= 100)
                    {
                        this.AccountTechnologyFeeBTextBox.BackColor = Color.White;
                        this.TechnologyAccountFeeBPanel.BackColor = Color.White;
                        this.TechnologyFeeBPercentTextBox.LockKeyPress = true;
                        this.AccountTechFeeBButton.Enabled = false;
                        this.TechnologyFeeBPercentTextBox.Text = string.Empty;
                        this.AccountTechnologyFeeBTextBox.Text = string.Empty;
                    }
                    else
                    {
                        ////check page mode and apply permission to make the Text Box editable
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            this.TechnologyFeeBPercentTextBox.LockKeyPress = !this.PermissionFiled.newPermission;
                            this.AccountTechFeeBButton.Enabled = this.PermissionFiled.newPermission;
                        }
                        else
                        {
                            this.TechnologyFeeBPercentTextBox.LockKeyPress = !this.PermissionFiled.editPermission;
                            this.AccountTechFeeBButton.Enabled = this.PermissionFiled.editPermission;
                        }

                        tempTechPercentBValue = 100 - this.TechnologyFeePercentTextBox.DecimalTextBoxValue;

                        if (tempTechPercentBValue >= 0)
                        {
                            this.AccountTextLeave(tempTechPercentBValue.ToString(), this.TechnologyFeeBPercentTextBox);
                            this.TechnologyFeeBPercentTextBox.Text = tempTechPercentBValue.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LocalTaxPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TechnologyFeePercentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.TechnologyFeePercentTextBox.Text, this.TechnologyFeePercentTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LocalTaxBPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TechnologyFeeBPercentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.TechnologyFeeBPercentTextBox.Text, this.TechnologyFeeBPercentTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

    }
}

