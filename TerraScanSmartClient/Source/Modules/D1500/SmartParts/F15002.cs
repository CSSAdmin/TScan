//--------------------------------------------------------------------------------------------
// <copyright file="F15002.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F15002 Form Slice - DistrictMgmt 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22-12-2006       Shiva              Created
// 16-7-2010        Manoj               modified To implement #7325
// 22-7-2010        Manoj               modified to implement #8095 
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
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;

    /// <summary>
    ///0 F15002 SmartPart
    /// </summary>
    [SmartPart]
    public partial class F15002 : BaseSmartPart
    {
        #region Member Variables


        /// <summary>
        /// edit variable is used to store the grid edit status.
        /// </summary>
        private bool edit = false;

        /// <summary>
        /// Used To StoreExchage RateId
        /// </summary>
        private string exciseRateId;

        /// <summary>
        /// f15005 Controller variable.
        /// </summary>
        private F15002Controller form15002Controll;

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

        ///<summary>
        /// relative position in the grid
        /// </summary>
        private bool upButtonClick = false;



        ///<summary>
        /// relative position in the scroll
        /// </summary>
        private int scrollPosition = 0;

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData districtSelectionDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// datatable contains the formDetails.
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// dataset contains district management details.
        /// </summary>
        private F15002DistMgmtData districtMgmtDataSet = new F15002DistMgmtData();

        /// <summary>
        /// districtId variable is used to store the districtId value default value is Null.
        /// </summary>
        private int? districtId = null;

        /// <summary>
        /// userFund variable is used to store the userFund value default value is Null.
        /// </summary>
        private String userFund = null;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// flagInvalidFunds variable is used to store the InvalidFunds Flag.
        /// </summary>
        private bool flagInvalidFunds;

        private bool isDistrictPanelEnable = false;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15002"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15002(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
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
        public F15002(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
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
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form15002 controll.
        /// </summary>
        /// <value>The form15002 controll.</value>
        [CreateNew]
        public F15002Controller Form15002Controll
        {
            get { return this.form15002Controll as F15002Controller; }
            set { this.form15002Controll = value; }
        }

        /// <summary>
        /// Gets or sets the district identity.
        /// </summary>
        /// <value>The district identity.</value>
        public int? DistrictIdentity
        {
            get
            {
                return this.districtId;
            }

            set
            {
                this.districtId = value;
            }
        }

        /// <summary>
        /// Gets or sets the user fund.
        /// </summary>
        /// <value>The user fund.</value>
        public string UserFund
        {
            get
            {
                return this.userFund;
            }

            set
            {
                this.userFund = value;
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
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    if (this.districtMgmtDataSet.DistrictHeader.Rows.Count > 0)
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
                    if (this.districtMgmtDataSet.DistrictHeader.Rows.Count > 0)
                    {
                        this.LockControls(false);
                        this.DisableDistrictPanel();
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
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.SaveDistrictFundDitails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetGridSortMode(true);
                }
            }
            else
            {
                this.LockControls(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetGridSortMode(true);
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
                        this.SetGridSortMode(false);
                        this.ClearDistrictHeader();
                        this.GetYear();
                        this.districtMgmtDataSet.ListDistrictFunds.Rows.Clear();
                        this.PopulateDisrictandFundsGridView();
                        if (Convert.ToInt32(this.RollYearTextBox.NumericTextBoxValue) > 0)
                        {
                            this.FillAllFundsGridView(null, Convert.ToInt32(this.RollYearTextBox.Text.Trim()));
                        }
                        else
                        {
                            this.districtMgmtDataSet.ListAllFunds.Rows.Clear();
                            this.PopulateAllFundsGridView();
                        }

                        this.LockControls(false);
                        this.pageLoadStatus = false;
                        this.DistrictTextBox.Focus();

                        // Enable/Disable DistrictPanel based on value form DataBase
                        this.DisableDistrictPanel();
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

        private void DisableDistrictPanel()
        {
            this.ExciseRatepanel.Enabled = this.isDistrictPanelEnable;
            this.excisedtlabel.Visible = this.isDistrictPanelEnable;
            this.ExciseRatePictureBox.Visible = this.isDistrictPanelEnable;
            this.Exciselabel.Visible = this.isDistrictPanelEnable;
            this.BaseLevyRateLabel.Visible = this.isDistrictPanelEnable;
            this.BaseLevyRateTextBox.Visible = this.isDistrictPanelEnable;
            this.ExcessLevyRateLabel.Visible = this.isDistrictPanelEnable;
            this.ExcessLevyRateTextBox.Visible = this.isDistrictPanelEnable;
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
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetGridSortMode(true);
            this.DistrictIdentity = this.keyId;
            this.PopulateDistrictFundDetails(this.keyId);
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
                    if (!this.flagInvalidFunds)
                    {
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        this.DistrictIdentity = this.keyId;
                        this.PopulateDistrictFundDetails(Convert.ToInt32(this.DistrictIdentity));
                    }
                    else
                    {
                        this.SetEditRecord();
                        this.flagInvalidFunds = false;
                    }

                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                        this.DisableDistrictPanel();
                    }
                    else
                    {
                        this.LockControls(true);
                    }

                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #region Form Load Events/Methods

        /// <summary>
        /// Handles the Load event of the F15002 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15002_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeDisrictandFundsGridView();
                this.CustomizeAllFundsGridView();
                this.DistrictandFundsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictandFundsPictureBox.Height, this.DistrictandFundsPictureBox.Width, "District and SubFunds", 28, 81, 128);
                this.AllFundsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AllFundsPictureBox.Height, this.AllFundsPictureBox.Width, "All SubFunds", 174, 150, 94);
                this.DistrictIdentity = this.keyId;
                this.PopulateDistrictFundDetails(Convert.ToInt32(this.DistrictIdentity));
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the district fund details.
        /// </summary>
        /// <param name="districtIdentity">The district identity.</param>
        private void PopulateDistrictFundDetails(int districtIdentity)
        {
            this.pageLoadStatus = true;
            this.InitActiveComboBox();
            this.InitDistrictTypeComboBox();
            this.districtMgmtDataSet.Merge(this.Form15002Controll.WorkItem.F15002_GetDistirctFundDetails(districtIdentity));

            if (this.districtMgmtDataSet.DistrictHeader.Rows.Count > 0)
            {
                //this.DistrictTextBox.Text = this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.DistrictColumn.ColumnName].ToString();
                //this.RollYearTextBox.Text = this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.RollYearColumn.ColumnName].ToString();
                //this.DescriptionTextBox.Text = this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.DescriptionColumn.ColumnName].ToString();
                //this.ActiveComboBox.SelectedValue = Convert.ToInt32(this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.IsActiveColumn.ColumnName]);
                //////Coding added for the CO : 4915 by malliga on 27/11/2009
                //this.TotalLevyRateTextBox.Text = this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.TotalLevyColumn.ColumnName].ToString();
                //this.BaseLevyRateTextBox.Text = this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.TotalBaseLevyColumn.ColumnName].ToString();
                //this.ExcessLevyRateTextBox.Text = this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.TotalExcessLevyColumn.ColumnName].ToString();
                //if (!string.IsNullOrEmpty(this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.DistrictTypeIDColumn.ColumnName].ToString()))
                //{
                //    this.DistrictTypeCOmbo.SelectedValue = Convert.ToInt32(this.districtMgmtDataSet.DistrictHeader.Rows[0][this.districtMgmtDataSet.DistrictHeader.DistrictTypeIDColumn.ColumnName].ToString());
                //}
                //else
                //{
                //    this.DistrictTypeCOmbo.Text = string.Empty;  
                //}




                //coding for CO To implement #7325
                //using an instance of an object for Populate DistrictFundDetails assigning in addition to CO
                F15002DistMgmtData.DistrictHeaderRow districtfund = (F15002DistMgmtData.DistrictHeaderRow)this.districtMgmtDataSet.DistrictHeader.Rows[0];
                if (!districtfund.IsDistrictNull())
                {
                    this.DistrictTextBox.Text = districtfund.District;
                }
                else
                {
                    this.DistrictTextBox.Text = string.Empty;
                }
                if (!districtfund.IsRollYearNull())
                {
                    this.RollYearTextBox.Text = districtfund.RollYear.ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }
                this.DescriptionTextBox.Text = districtfund.Description;
                if (!districtfund.IsIsActiveNull())
                {
                    this.ActiveComboBox.SelectedValue = districtfund.IsActive;
                }
                else
                {
                    this.ActiveComboBox.SelectedValue = null;
                }
                if (!districtfund.IsTotalLevyNull())
                {
                    this.TotalLevyRateTextBox.Text = districtfund.TotalLevy.ToString();
                }
                else
                {
                    this.TotalLevyRateTextBox.Text = string.Empty;
                }
                if (!districtfund.IsTotalBaseLevyNull())
                {
                    this.BaseLevyRateTextBox.Text = districtfund.TotalBaseLevy.ToString();
                }
                else
                {
                    this.BaseLevyRateTextBox.Text = string.Empty;
                }
                if (!districtfund.IsTotalExcessLevyNull())
                {
                    this.ExcessLevyRateTextBox.Text = districtfund.TotalExcessLevy.ToString();
                }
                else
                {
                    this.ExcessLevyRateTextBox.Text = string.Empty;
                }
                if (!districtfund.IsExciseRateIDNull())
                {
                    this.exciseRateId = districtfund.ExciseRateID.ToString();
                }
                else
                {
                    this.exciseRateId = string.Empty;
                }
                if (!districtfund.IsExciseDistrictNull())
                {
                    this.Exciselabel.Text = districtfund.ExciseDistrict;

                }
                else
                {
                    this.Exciselabel.Text = string.Empty;
                }


                this.DisableDistrictPanel();
                if (!districtfund.IsDistrictTypeIDNull())
                {
                    this.DistrictTypeCOmbo.SelectedValue = int.Parse(districtfund.DistrictTypeID.ToString());
                }
                else
                {
                    this.DistrictTypeCOmbo.Text = string.Empty;
                }


            }
            else
            {
                this.ClearDistrictHeader();
                this.districtMgmtDataSet.ListDistrictFunds.Clear();
                this.districtMgmtDataSet.ListAllFunds.Clear();
                this.LockControls(true);
            }

            this.PopulateDisrictandFundsGridView();
            this.PopulateAllFundsGridView();
            this.DistrictTextBox.Focus();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetGridSortMode(true);
            this.pageLoadStatus = false;
        }



        /// <summary>
        /// Populates the disrictand funds grid view.
        /// </summary>
        private void PopulateDisrictandFundsGridView()
        {
            int disrictandFundsRowCount;
            this.DisrictandFundsGridView.DataSource = this.districtMgmtDataSet.ListDistrictFunds.DefaultView;
            disrictandFundsRowCount = this.DisrictandFundsGridView.OriginalRowCount;
            if (!this.upButtonClick)
            {
                if (disrictandFundsRowCount > 0)
                {
                    this.DisrictandFundsGridView.CurrentCell = this.DisrictandFundsGridView[1, 0];

                    if (this.DisrictandFundsGridView.CurrentRowIndex > 0)
                    {
                        this.DisrictandFundsGridView.Rows[this.DisrictandFundsGridView.CurrentRowIndex].Selected = false;
                        this.DisrictandFundsGridView.Rows[0].Selected = true;
                    }
                }
                else
                {
                    this.DisrictandFundsGridView.CurrentCell = null;
                    this.DisrictandFundsGridView.Rows[0].Selected = false;
                }
            }
            else
            {
                this.DisrictandFundsGridView.FirstDisplayedScrollingRowIndex = this.scrollPosition;
            }
            if (disrictandFundsRowCount >= this.DisrictandFundsGridView.NumRowsVisible)
            {
                this.DistrictandFundsVScrollBar.Visible = false;
            }
            else
            {
                this.DistrictandFundsVScrollBar.Visible = true;
            }

            if (this.DisrictandFundsGridView.OriginalRowCount >= this.DisrictandFundsGridView.NumRowsVisible)
            {
                if (!Convert.ToBoolean(this.districtMgmtDataSet.ListDistrictFunds.Rows[this.districtMgmtDataSet.ListDistrictFunds.Rows.Count - 1][this.DisrictandFundsGridView.EmptyRecordColumnName]))
                {
                    this.districtMgmtDataSet.ListDistrictFunds.Rows.Add(this.districtMgmtDataSet.ListDistrictFunds.NewRow());
                    this.districtMgmtDataSet.ListDistrictFunds.Rows[this.districtMgmtDataSet.ListDistrictFunds.Rows.Count - 1][this.DisrictandFundsGridView.EmptyRecordColumnName] = true;
                    this.DistrictandFundsVScrollBar.Visible = false;
                }
            }
        }

        /// <summary>
        /// Populates the disrictand funds grid view.
        /// </summary>
        private void PopulateAllFundsGridView()
        {
            int allFundsRowCount;
            this.AllFundsGridView.DataSource = this.districtMgmtDataSet.ListAllFunds.DefaultView;
            allFundsRowCount = this.AllFundsGridView.OriginalRowCount;

            if (allFundsRowCount > 0)
            {
                this.AllFundsGridView.Enabled = true;
            }
            else
            {
                this.AllFundsGridView.CurrentCell = null;
                this.AllFundsGridView.Rows[0].Selected = false;
                this.AllFundsGridView.Enabled = false;
            }

            if (allFundsRowCount > this.AllFundsGridView.NumRowsVisible)
            {
                this.AllFundsVScorrlBar.Visible = false;
            }
            else
            {
                this.AllFundsVScorrlBar.Visible = true;
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// to open the ExciseRate District Form 
        /// </summary>
        /// <param name="sender">exciseRateId</param>
        /// <param name="e"></param>
        private void Exciselabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11013);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.exciseRateId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// <summary>
        /// Handles the Click event of the DistrictPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExciseRatePictureBox_Click(object sender, EventArgs e)
        {
            Form districtF1102 = new Form();
            // TO IMPLEMENT CO:TFS 8095 – Provide Year default for 1102 when called from 11002.
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                //int tempRollYear1 = GetYear1();
                int temprollyear = int.Parse(this.RollYearTextBox.Text);
                if (temprollyear > 0)
                {
                    object[] optionalParameters = new object[] { temprollyear };
                    districtF1102 = this.form15002Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, optionalParameters, this.form15002Controll.WorkItem);
                }
                else
                {
                    int temrollyear = GetYeare();
                    object[] optionalParameters = new object[] { temrollyear };
                    districtF1102 = this.form15002Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, optionalParameters, this.form15002Controll.WorkItem);

                }
            }
            else
            {
                int temrollyear = GetYeare();
                object[] optionalParameters = new object[] { temrollyear };
                districtF1102 = this.form15002Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, optionalParameters, this.form15002Controll.WorkItem);

            }

            DialogResult districtDialog;
            if (districtF1102 != null)
            {
                districtDialog = districtF1102.ShowDialog();

                if (districtDialog == DialogResult.Yes)
                {
                    try
                    {

                        //modified to To implement #7325
                        //new item excise label to show the district

                        this.exciseRateId = TerraScanCommon.GetValue(districtF1102, "ExciseRateDistrictSelectionId");
                        this.districtSelectionDataSet = this.form15002Controll.WorkItem.F15010_GetDistrictSelection(Convert.ToInt32(this.exciseRateId));
                        //this.districtId = int.Parse(this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.DistrictIDColumn].ToString());
                        if (this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows.Count > 0)
                        {
                            F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow districtRow = (F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow)this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0];
                            //if (!districtRow.IsDistrictIDNull())
                            //{
                            //    this.districtId = int.Parse(districtRow.DistrictID);
                            //}
                            //else
                            //{
                            //    this.districtId = null;
                            //}
                            if (!districtRow.IsDistrictNull())
                            {
                                this.Exciselabel.Text = districtRow.District;
                            }
                            else
                            {
                                this.Exciselabel.Text = string.Empty;
                            }
                            //this.Exciselabel.Text = this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.DistrictColumn].ToString();
                        }

                        this.SetEditRecord();
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
                else if (districtDialog == DialogResult.Ignore)
                {
                    try
                    {
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(1101);
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
            }


        }

        /// <summary>
        /// Handles the Click event of the MoveDownButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisAssociateDistrictFund();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// de associate the district fund.
        /// </summary>
        private void DisAssociateDistrictFund()
        {
            if (this.DisrictandFundsGridView.CurrentRowIndex >= 0)
            {
                int currentRowIndex = this.DisrictandFundsGridView.CurrentRowIndex;
                DataGridViewSelectedRowCollection selectedDistrictFundRows;
                this.DisrictandFundsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.DisrictandFundsGridView.Rows[currentRowIndex].Selected = true;
                selectedDistrictFundRows = this.DisrictandFundsGridView.SelectedRows;

                if (selectedDistrictFundRows.Count > 0)
                {
                    for (int i = 0; i < selectedDistrictFundRows.Count; i++)
                    {
                        Int16 tempFund = -1;
                        int recIndex = -1;
                        //// Int16.TryParse(selectedDistrictFundRows[i].Cells[1].Value.ToString(), out tempFund);
                        ////recIndex = this.RetrieveRecordIndex(tempFund);

                        recIndex = this.RetrieveRecordIndex(selectedDistrictFundRows[i].Cells[1].Value.ToString());
                        if (recIndex >= 0)
                        {
                            this.SetEditRecord();
                            this.districtMgmtDataSet.ListDistrictFunds.Rows.RemoveAt(recIndex);
                            this.districtMgmtDataSet.ListDistrictFunds.AcceptChanges();
                            this.PopulateDisrictandFundsGridView();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the MoveUpButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            try
            {
                //used to retain relative position in the grid
                this.upButtonClick = true;
                this.AssociateFundList();
                this.upButtonClick = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Associates the fund list.
        /// </summary>
        private void AssociateFundList()
        {
            DataGridViewSelectedRowCollection selectedAllFundsRows;
            selectedAllFundsRows = this.AllFundsGridView.SelectedRows;
            if (selectedAllFundsRows.Count > 0)
            {
                for (int i = 0; i < selectedAllFundsRows.Count; i++)
                {
                    F15002DistMgmtData.ListDistrictFundsRow tempDistrictFundRow = this.districtMgmtDataSet.ListDistrictFunds.NewListDistrictFundsRow();
                    int tempFundId = -1;
                    ////Int16 tempFund = -1;
                    Int16 tempRollYear = -1;
                    byte tempFundGroupId;

                    Int32.TryParse(selectedAllFundsRows[i].Cells[0].Value.ToString(), out tempFundId);
                    if (tempFundId > 0)
                    {
                        this.SetEditRecord();
                        ////Int16.TryParse(selectedAllFundsRows[i].Cells[1].Value.ToString(), out tempFund);
                        Int16.TryParse(selectedAllFundsRows[i].Cells[3].Value.ToString(), out tempRollYear);
                        Byte.TryParse(selectedAllFundsRows[i].Cells[4].Value.ToString(), out tempFundGroupId);

                        tempDistrictFundRow.SubFundID = tempFundId;

                        ////tempDistrictFundRow.Fund = tempFund.ToString();
                        if (!string.IsNullOrEmpty(selectedAllFundsRows[i].Cells[1].Value.ToString()))
                        {
                            tempDistrictFundRow.SubFund = selectedAllFundsRows[i].Cells[1].Value.ToString();
                        }

                        tempDistrictFundRow.Description = selectedAllFundsRows[i].Cells[2].Value.ToString();
                        tempDistrictFundRow.RollYear = tempRollYear;
                        //tempDistrictFundRow.FundGroupID = tempFundGroupId;
                        tempDistrictFundRow.DistFundID = 0;
                        tempDistrictFundRow.DistrictID = 0;
                        if (!this.CheckExistingFund(tempFundId))
                        {
                            if (this.DisrictandFundsGridView.OriginalRowCount < this.DisrictandFundsGridView.NumRowsVisible)
                            {
                                this.districtMgmtDataSet.ListDistrictFunds.Rows.RemoveAt(this.DisrictandFundsGridView.OriginalRowCount);
                                this.districtMgmtDataSet.ListDistrictFunds.AcceptChanges();
                                this.districtMgmtDataSet.ListDistrictFunds.Rows.InsertAt(tempDistrictFundRow, this.DisrictandFundsGridView.OriginalRowCount);
                                this.districtMgmtDataSet.ListDistrictFunds.AcceptChanges();
                            }
                            else
                            {
                                if (Convert.ToBoolean(this.districtMgmtDataSet.ListDistrictFunds.Rows[this.districtMgmtDataSet.ListDistrictFunds.Rows.Count - 1][this.DisrictandFundsGridView.EmptyRecordColumnName]))
                                {
                                    this.districtMgmtDataSet.ListDistrictFunds.Rows.RemoveAt(this.DisrictandFundsGridView.OriginalRowCount);
                                    this.districtMgmtDataSet.ListDistrictFunds.AcceptChanges();
                                }

                                this.districtMgmtDataSet.ListDistrictFunds.Rows.InsertAt(tempDistrictFundRow, this.DisrictandFundsGridView.OriginalRowCount);
                                this.districtMgmtDataSet.ListDistrictFunds.AcceptChanges();

                            }

                            if (this.DisrictandFundsGridView.OriginalRowCount >= this.DisrictandFundsGridView.NumRowsVisible)
                            {
                                if (!Convert.ToBoolean(this.districtMgmtDataSet.ListDistrictFunds.Rows[this.districtMgmtDataSet.ListDistrictFunds.Rows.Count - 1][this.DisrictandFundsGridView.EmptyRecordColumnName]))
                                {
                                    this.districtMgmtDataSet.ListDistrictFunds.Rows.Add(this.districtMgmtDataSet.ListDistrictFunds.NewRow());
                                    this.districtMgmtDataSet.ListDistrictFunds.Rows[this.districtMgmtDataSet.ListDistrictFunds.Rows.Count - 1][this.DisrictandFundsGridView.EmptyRecordColumnName] = true;
                                    this.DistrictandFundsVScrollBar.Visible = false;
                                }
                            }
                        }
                    }
                }
            }

            this.PopulateDisrictandFundsGridView();
            this.DisrictandFundsGridView.FirstDisplayedScrollingRowIndex = 0;
            this.DisrictandFundsGridView.Refresh();
            this.DisrictandFundsGridView.Update();
            this.DisrictandFundsGridView.FirstDisplayedScrollingRowIndex = this.scrollPosition;
            this.DisrictandFundsGridView.Parent.Refresh();
            this.PopulateAllFundsGridView();

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
                int value = 0;

                if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    if (int.TryParse(this.RollYearTextBox.Text.Trim(), out value))
                    {
                        this.FillAllFundsGridView(null, Convert.ToInt32(this.RollYearTextBox.Text.Trim()));
                    }
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

        #region DisrictandFundsGridView Events/Methods

        /// <summary>
        /// Customizes the disrict and funds grid view.
        /// </summary>
        private void CustomizeDisrictandFundsGridView()
        {
            this.DisrictandFundsGridView.AutoGenerateColumns = false;
            ////this.DisrictandFundsGridView.MultiSelect = true;
            this.DisrictandFundsGridView.PrimaryKeyColumnName = this.districtMgmtDataSet.ListDistrictFunds.PrimaryKeyIDColumn.ColumnName.ToString();
            this.DisrictandFundsGridView.Columns["FundID"].DataPropertyName = this.districtMgmtDataSet.ListDistrictFunds.SubFundIDColumn.ColumnName.ToString();
            this.DisrictandFundsGridView.Columns["Fund"].DataPropertyName = this.districtMgmtDataSet.ListDistrictFunds.SubFundColumn.ColumnName.ToString();
            this.DisrictandFundsGridView.Columns["Description"].DataPropertyName = this.districtMgmtDataSet.ListDistrictFunds.DescriptionColumn.ColumnName.ToString();
            this.DisrictandFundsGridView.Columns["RollYear"].DataPropertyName = this.districtMgmtDataSet.ListDistrictFunds.RollYearColumn.ColumnName.ToString();
            //this.DisrictandFundsGridView.Columns["FundGroupID"].DataPropertyName = this.districtMgmtDataSet.ListDistrictFunds.FundGroupIDColumn.ColumnName.ToString();
            this.DisrictandFundsGridView.Columns["DistFundID"].DataPropertyName = this.districtMgmtDataSet.ListDistrictFunds.DistFundIDColumn.ColumnName.ToString();
            this.DisrictandFundsGridView.Columns["DistrictID"].DataPropertyName = this.districtMgmtDataSet.ListDistrictFunds.DistrictIDColumn.ColumnName.ToString();

            this.DisrictandFundsGridView.Columns["Fund"].HeaderText = "SubFund";
            this.Fund.MaxInputLength = 50;
            this.DisrictandFundsGridView.Columns["Fund"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DisrictandFundsGridView.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// Handles the RowEnter event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.DisrictandFundsGridView.Rows[(e.RowIndex - 1)].Cells["Fund"].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < this.DisrictandFundsGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < this.DisrictandFundsGridView.RowCount; i++)
                            {
                                if (this.DisrictandFundsGridView.Rows[i].Cells["Fund"].Value != null && !String.IsNullOrEmpty(this.DisrictandFundsGridView.Rows[i].Cells["Fund"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.DisrictandFundsGridView["Fund", e.RowIndex].ReadOnly = false;
                                this.DisrictandFundsGridView.Rows[e.RowIndex].Selected = true;
                            }
                            else
                            {
                                this.DisrictandFundsGridView["Fund", e.RowIndex].ReadOnly = true;
                                this.DisrictandFundsGridView.Rows[e.RowIndex].Selected = true;
                            }
                        }
                        else
                        {
                            this.DisrictandFundsGridView["Fund", e.RowIndex].ReadOnly = true;
                            this.DisrictandFundsGridView.Rows[e.RowIndex].Selected = true;
                        }
                    }
                    else
                    {
                        this.DisrictandFundsGridView["Fund", e.RowIndex].ReadOnly = false;
                        this.DisrictandFundsGridView.Rows[e.RowIndex].Selected = true;
                    }
                }
                //// Need to Get Confirmation
                if (e.RowIndex == 0)
                {
                    this.DisrictandFundsGridView["Fund", e.RowIndex].ReadOnly = false;
                    this.DisrictandFundsGridView.Rows[e.RowIndex].Selected = true;
                }

                ////this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.edit = true;
        }

        /// <summary>
        /// Handles the CellValueChanged event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.edit)
            {
                try
                {
                    if (e.ColumnIndex == this.DisrictandFundsGridView.Columns["Fund"].Index)
                    {
                        //// int tempUserFund = -1;
                        int tempFundId = -1;
                        ////string tempFundId = "-1";
                        ////int tempUserFund;
                        F15002DistMgmtData tempDataSet = new F15002DistMgmtData();
                        //// Int32.TryParse(this.DisrictandFundsGridView["Fund", e.RowIndex].Value.ToString(), out tempUserFund);
                        ////this.userFund = tempUserFund.ToString();
                        this.UserFund = this.DisrictandFundsGridView.Rows[e.RowIndex].Cells[1].Value.ToString(); ////["Fund", e.RowIndex].Value.ToString();
                        //// string UserFund;
                        //// string[] str2 = str.Split(chr);
                        ////UserFund = str2[0];

                        ////if (this.userFund != null && this.userFund != 0)
                        ////if(this.userFund!=string.Empty || this.userFund!=null)
                        if (!string.IsNullOrEmpty(this.userFund.Trim()))
                        {
                            tempDataSet = this.form15002Controll.WorkItem.F15002_ListAllFunds(null, this.userFund, Convert.ToInt32(this.RollYearTextBox.Text.Trim()));
                            if (tempDataSet.ListAllFunds.Rows.Count > 0)
                            {
                                Int32.TryParse(tempDataSet.ListAllFunds.Rows[0][tempDataSet.ListAllFunds.SubFundIDColumn.ColumnName].ToString(), out tempFundId);
                                ////Int32.TryParse(out tempFundId);
                                if (!this.CheckExistingFund(tempFundId))
                                {
                                    this.edit = false;
                                    this.DisrictandFundsGridView["FundID", e.RowIndex].Value = tempDataSet.ListAllFunds.Rows[0][tempDataSet.ListAllFunds.SubFundIDColumn.ColumnName].ToString();
                                    this.DisrictandFundsGridView["Description", e.RowIndex].Value = tempDataSet.ListAllFunds.Rows[0][tempDataSet.ListAllFunds.DescriptionColumn.ColumnName].ToString();
                                    this.DisrictandFundsGridView["RollYear", e.RowIndex].Value = tempDataSet.ListAllFunds.Rows[0][tempDataSet.ListAllFunds.RollYearColumn.ColumnName].ToString();
                                }
                                else
                                {
                                    this.edit = false;
                                    MessageBox.Show("The subfund entered already Exists.", "TerraScan T2 - Duplicate subfund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.DisrictandFundsGridView["FundID", e.RowIndex].Value = string.Empty;
                                    this.DisrictandFundsGridView["Description", e.RowIndex].Value = string.Empty;
                                    this.DisrictandFundsGridView["RollYear", e.RowIndex].Value = string.Empty;
                                    if (!Convert.ToBoolean(this.districtMgmtDataSet.ListDistrictFunds.Rows[e.RowIndex][this.DisrictandFundsGridView.EmptyRecordColumnName]) && string.IsNullOrEmpty(this.districtMgmtDataSet.ListDistrictFunds.Rows[e.RowIndex][this.districtMgmtDataSet.ListDistrictFunds.SubFundIDColumn].ToString()))
                                    {
                                        this.districtMgmtDataSet.ListDistrictFunds.Rows[e.RowIndex][this.DisrictandFundsGridView.EmptyRecordColumnName] = true;
                                    }
                                }
                            }
                            else
                            {
                                this.edit = false;
                                this.DisrictandFundsGridView["FundID", e.RowIndex].Value = 0;
                                this.DisrictandFundsGridView["Description", e.RowIndex].Value = SharedFunctions.GetResourceString("F15002InvalidFund"); // SHOULD DISPLAY IN RED
                            }

                            this.DisrictandFundsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            this.districtMgmtDataSet.ListDistrictFunds.AcceptChanges();
                            ////  this.districtMgmtDataSet.ListDistrictFunds.EndLoadData();
                        }
                        else
                        {
                            this.districtMgmtDataSet.ListDistrictFunds.Rows.RemoveAt(this.DisrictandFundsGridView.CurrentRowIndex);
                            this.districtMgmtDataSet.ListDistrictFunds.AcceptChanges();
                        }

                        this.PopulateDisrictandFundsGridView();
                        ////this.edit = false;
                    }

                    if (this.DisrictandFundsGridView.OriginalRowCount >= this.DisrictandFundsGridView.NumRowsVisible)
                    {
                        if (!Convert.ToBoolean(this.districtMgmtDataSet.ListDistrictFunds.Rows[this.districtMgmtDataSet.ListDistrictFunds.Rows.Count - 1][this.DisrictandFundsGridView.EmptyRecordColumnName]))
                        {
                            this.districtMgmtDataSet.ListDistrictFunds.Rows.Add(this.districtMgmtDataSet.ListDistrictFunds.NewRow());
                            this.districtMgmtDataSet.ListDistrictFunds.Rows[this.districtMgmtDataSet.ListDistrictFunds.Rows.Count - 1][this.DisrictandFundsGridView.EmptyRecordColumnName] = true;
                            this.DistrictandFundsVScrollBar.Visible = false;
                        }
                    }
                }
                catch (DataException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                Int32 outInteger;
                ////Int64 outInt;

                //// Only paint if desired column

                if (e.ColumnIndex == this.DisrictandFundsGridView.Columns["Fund"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        Int32.TryParse(tempvalue, System.Globalization.NumberStyles.Integer, null, out outInteger);
                        e.Value = outInteger;
                        e.ParsingApplied = true;
                        this.DisrictandFundsGridView.RefreshEdit();
                    }
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the CellFormatting event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.DisrictandFundsGridView.Columns["Description"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.DisrictandFundsGridView.Rows[e.RowIndex].Cells["Description"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (!string.IsNullOrEmpty(val))
                        {
                            if (string.Equals(val.ToString(), SharedFunctions.GetResourceString("F15002InvalidFund")))
                            {
                                e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                            }
                            else
                            {
                                e.CellStyle.ForeColor = Color.Black;
                                e.FormattingApplied = true;
                            }
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
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && !Convert.ToBoolean(this.districtMgmtDataSet.ListDistrictFunds.Rows[e.RowIndex][this.DisrictandFundsGridView.EmptyRecordColumnName]))
                {
                    this.DisAssociateDistrictFund();
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the DisrictandFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void DisrictandFundsGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.DisrictandFundsGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
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

        #endregion

        #region AllFundsGridView Events/Methods

        /// <summary>
        /// Customizes all funds grid view.
        /// </summary>
        private void CustomizeAllFundsGridView()
        {
            this.AllFundsGridView.AutoGenerateColumns = false;
            this.AllFundsGridView.MultiSelect = true;
            this.AllFundsGridView.PrimaryKeyColumnName = this.districtMgmtDataSet.ListAllFunds.SubFundIDColumn.ColumnName.ToString();


            this.AllFundsGridView.Columns["AFundId"].DataPropertyName = this.districtMgmtDataSet.ListAllFunds.SubFundIDColumn.ColumnName.ToString();
            this.AllFundsGridView.Columns["AFund"].DataPropertyName = this.districtMgmtDataSet.ListAllFunds.SubFundColumn.ColumnName.ToString();
            this.AllFundsGridView.Columns["ADescription"].DataPropertyName = this.districtMgmtDataSet.ListAllFunds.DescriptionColumn.ColumnName.ToString();
            this.AllFundsGridView.Columns["ARollYear"].DataPropertyName = this.districtMgmtDataSet.ListAllFunds.RollYearColumn.ColumnName.ToString();
            this.AllFundsGridView.Columns["AFundGroupId"].DataPropertyName = this.districtMgmtDataSet.ListAllFunds.SubFundTypeIDColumn.ColumnName.ToString();

            this.AllFundsGridView.Columns["AFund"].HeaderText = "SubFund";

            this.AllFundsGridView.Columns["AFund"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.AllFundsGridView.Columns["ADescription"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the AllFundsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AllFundsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && !Convert.ToBoolean(this.districtMgmtDataSet.ListAllFunds.Rows[e.RowIndex][this.AllFundsGridView.EmptyRecordColumnName]))
                {
                    //used to retain relative position in the grid
                    this.upButtonClick = true;
                    this.AssociateFundList();
                    this.upButtonClick = false;
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Common Methods

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            int tempRollYear = GetYeare();
            this.RollYearTextBox.Text = tempRollYear.ToString();
        }
        // TO IMPLEMENT CO:TFS 8095 – Provide Year default for 1102 when called from 11002.
        /// <summary>
        /// GET THE ROLL YEAR
        /// </summary>
        private int GetYeare()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form15002Controll.WorkItem.GetConfigDetails("TR_RollYear");
            int tempRollYear = -1;
            int.TryParse(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString(), out tempRollYear);
            if (tempRollYear.Equals(0))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tempRollYear;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetGridSortMode(false);
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
            this.DistrictTextBox.LockKeyPress = lockValue;
            this.RollYearTextBox.LockKeyPress = lockValue;
            this.DescriptionTextBox.LockKeyPress = lockValue;
            this.ActiveComboBox.Enabled = !lockValue;
            this.DistrictPanel.Enabled = !lockValue;
            this.RoleYearPanel.Enabled = !lockValue;
            this.DescriptionPanel.Enabled = !lockValue;
            this.ActivePanel.Enabled = !lockValue;
            this.DistrictAndFundsGridPanel.Enabled = !lockValue;
            this.AllFundsGridPanel.Enabled = !lockValue;
            this.MoveUpButton.Enabled = !lockValue;
            this.MoveDownButton.Enabled = !lockValue;
            ////Coding added for the co : 4915 by malliga
            this.BaseLevyRateTextBox.Enabled = lockValue;
            this.TotalLevyRateTextBox.Enabled = lockValue;
            this.ExcessLevyRateTextBox.Enabled = lockValue;
            this.DistrictTypeCOmbo.Enabled = !lockValue;
            ////coding added for the co:To implement #7325
            this.ExciseRatepanel.Enabled = !lockValue;

            this.TotalLevyRatePanel.Enabled = !lockValue;
            this.BaseLevyRatePanel.Enabled = !lockValue;
            this.ExcessLevyRatePanel.Enabled = !lockValue;
            this.DistrictTypePanel.Enabled = !lockValue;
        }

        /// <summary>
        /// Initialize the active combo box.
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

        /// <summary>
        /// Inits the district type combo box.
        /// </summary>
        private void InitDistrictTypeComboBox()
        {
            this.districtMgmtDataSet = this.Form15002Controll.WorkItem.F15002_GetDistrictType(TerraScanCommon.UserId);
            this.DistrictTypeCOmbo.DataSource = this.districtMgmtDataSet.F15002_ListDistrictType;
            this.DistrictTypeCOmbo.ValueMember = this.districtMgmtDataSet.F15002_ListDistrictType.DistrictTypeIDColumn.ColumnName;
            this.DistrictTypeCOmbo.DisplayMember = this.districtMgmtDataSet.F15002_ListDistrictType.DistrictTypeColumn.ColumnName;
            byte ratenable;
            if (districtMgmtDataSet.DistrictVisibility.Rows.Count > 0)
            {
                F15002DistMgmtData.DistrictVisibilityRow districtType = (F15002DistMgmtData.DistrictVisibilityRow)this.districtMgmtDataSet.DistrictVisibility.Rows[0];

                if (!districtType.IsIsExciseRateEnabledNull())
                {
                    byte.TryParse(districtType.IsExciseRateEnabled.ToString(), out ratenable);
                    if (ratenable > 0)
                    {
                        this.isDistrictPanelEnable = true;
                    }
                    else
                    {
                        this.isDistrictPanelEnable = false;
                    }
                }
                else
                {
                    this.isDistrictPanelEnable = true;

                }
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
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Clears the sub fund header.
        /// </summary>
        private void ClearDistrictHeader()
        {

            this.districtId = null;

            ////coding added for c0:To implement #7325
            this.Exciselabel.Text = string.Empty;

            this.DistrictTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.ActiveComboBox.SelectedValue = 1;
            ////Coding added for the CO : 4915 by malliga on 24/11/2009
            this.TotalLevyRateTextBox.Text = string.Empty;
            this.BaseLevyRateTextBox.Text = string.Empty;
            this.ExcessLevyRateTextBox.Text = string.Empty;
            this.DistrictTypeCOmbo.SelectedValue = 1;
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
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()))
            {
                this.DistrictTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
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
            ////else if (!this.CheckValidDistrictNumber())
            ////{
            ////    MessageBox.Show("The given input values violates the maximum / minimum data range", "TerraScan – Invalid Field value", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    this.DistrictTextBox.Text = string.Empty;
            ////    this.DistrictTextBox.Focus();
            ////    sliceValidationFields.DisableNewMethod = true;
            ////    sliceValidationFields.RequiredFieldMissing = false;
            ////    sliceValidationFields.ErrorMessage = string.Empty;
            ////}
            else if (!this.CheckDuplicateDistrict())
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("DuplicateDistrictFundExists"), SharedFunctions.GetResourceString("DuplicateRecordTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    this.DistrictTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            else if (this.CheckInvalidFunds())
            {
                DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("F15002InvalidFundsText1") + SharedFunctions.GetResourceString("F15002InvalidFundsText2"), SharedFunctions.GetResourceString("InvalidFundHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
                else
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            else if (string.IsNullOrEmpty(this.DistrictTypeCOmbo.Text.Trim()))
            {
                this.DistrictTypeCOmbo.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Fills all funds grid view.
        /// </summary>
        /// <param name="fund">The fund.</param>
        /// <param name="tempRollYear">The roll year.</param>
        private void FillAllFundsGridView(int? fund, int? tempRollYear)
        {

            if (!fund.HasValue)
            {
                this.districtMgmtDataSet.ListAllFunds.Rows.Clear();
                this.districtMgmtDataSet.Merge(this.form15002Controll.WorkItem.F15002_ListAllFunds(null, null, tempRollYear));
                this.PopulateAllFundsGridView();
            }
            else
            {
                // TODO: Need to check whether ListAllFunds Table Cleared or Not
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
        /// Retrieves the index of the record.
        /// </summary>
        /// <param name="tempFund">The temp fund.</param>
        /// <returns>index of the current record</returns>
        //// private int RetrieveRecordIndex(int tempFund)
        private int RetrieveRecordIndex(string tempFund)
        {
            int tempIndex = -1;

            try
            {
                DataTable tempDataTable = this.districtMgmtDataSet.ListDistrictFunds.Copy();
                string findExp = tempFund.Replace("'", "\''");

                tempDataTable.DefaultView.RowFilter = string.Concat(this.districtMgmtDataSet.ListDistrictFunds.SubFundColumn.ColumnName, " = ", "'" + findExp + "'");
                ////tempDataTable.DefaultView.RowFilter = "(this.districtMgmtDataSet.ListDistrictFunds.FundColumn.ColumnName) like ' " + tempFund + "'";
                ////tempDataTable.DefaultView.RowFilter = "Fund like '" + tempFund + "'";

                if (tempDataTable.DefaultView.Count > 0)
                {
                    tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
                }

                return tempIndex;
            }
            catch (Exception)
            {
                return tempIndex;
            }
        }

        /// <summary>
        /// Checks the existing fund.
        /// </summary>
        /// <param name="tempFundId">The temp fund id.</param>
        /// <returns>returns the Status of FundID Existsance</returns>
        private bool CheckExistingFund(int tempFundId)
        {
            DataRow[] dataRow;
            string findExp = "SubFundID =" + tempFundId.ToString();
            dataRow = this.districtMgmtDataSet.ListDistrictFunds.Select(findExp);
            if (dataRow.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the district fund itmes.
        /// </summary>
        /// <returns>string contains the FundItems Xml</returns>
        private string GetDistrictFundItmesXml()
        {
            DataTable tempDistrictFundItemsDataTable = new DataTable();
            string districtFundItems = string.Empty;

            foreach (DataColumn column in this.districtMgmtDataSet.ListDistrictFunds.Columns)
            {
                if (column.ColumnName == this.districtMgmtDataSet.ListDistrictFunds.SubFundIDColumn.ColumnName)
                {
                    tempDistrictFundItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.districtMgmtDataSet.ListDistrictFunds.RollYearColumn.ColumnName)
                {
                    tempDistrictFundItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }

                if (column.ColumnName == this.districtMgmtDataSet.ListDistrictFunds.DistFundIDColumn.ColumnName)
                {
                    tempDistrictFundItemsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            foreach (DataRow dr in this.districtMgmtDataSet.ListDistrictFunds.Rows)
            {
                DataRow districtFundItemsDataRow = tempDistrictFundItemsDataTable.NewRow();

                ////if (dr["SubFundID"] != DBNull.Value && dr["RollYear"] != DBNull.Value && dr["DistFundID"] != DBNull.Value)
                if (dr["SubFundID"] != DBNull.Value && dr["RollYear"] != DBNull.Value)
                {
                    ////if (!string.IsNullOrEmpty((dr["SubFundID"].ToString())) && !string.IsNullOrEmpty((dr["RollYear"].ToString())) && !string.IsNullOrEmpty((dr["DistFundID"].ToString())))

                    if (!string.IsNullOrEmpty((dr["SubFundID"].ToString())) && !string.IsNullOrEmpty((dr["RollYear"].ToString())))
                    {
                        foreach (DataColumn column in this.districtMgmtDataSet.ListDistrictFunds.Columns)
                        {
                            if (column.ColumnName == this.districtMgmtDataSet.ListDistrictFunds.SubFundIDColumn.ColumnName)
                            {
                                districtFundItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                            }

                            if (column.ColumnName == this.districtMgmtDataSet.ListDistrictFunds.RollYearColumn.ColumnName)
                            {
                                districtFundItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                            }

                            if (column.ColumnName == this.districtMgmtDataSet.ListDistrictFunds.DistFundIDColumn.ColumnName)
                            {
                                districtFundItemsDataRow[column.ColumnName] = dr[column.ColumnName];
                            }
                        }

                        tempDistrictFundItemsDataTable.Rows.Add(districtFundItemsDataRow);
                    }
                }
            }

            districtFundItems = TerraScanCommon.GetXmlString(tempDistrictFundItemsDataTable);
            return districtFundItems;
        }

        /// <summary>
        /// Saves the district fund ditails.
        /// </summary>
        private void SaveDistrictFundDitails()
        {
            int returnValue = -1;

            F15002DistMgmtData districtMgntData = new F15002DistMgmtData();
            districtMgntData.DistrictHeader.Rows.Clear();
            F15002DistMgmtData.DistrictHeaderRow districtmgntRow = districtMgntData.DistrictHeader.NewDistrictHeaderRow();

            districtmgntRow.District = this.DistrictTextBox.Text.Trim();
            short rollyear;
            short.TryParse(this.RollYearTextBox.Text.Trim(), out rollyear);
            districtmgntRow.RollYear = rollyear;
            districtmgntRow.Description = this.DescriptionTextBox.Text;

            if (this.ActiveComboBox.SelectedValue.Equals(0))
            {
                districtmgntRow.IsActive = 0;
            }
            else
            {
                districtmgntRow.IsActive = 1;
            }

            short districtTypeId;
            short.TryParse(this.DistrictTypeCOmbo.SelectedValue.ToString(), out districtTypeId);
            districtmgntRow.DistrictTypeID = districtTypeId;

            //coding for co:To implement #7325
            // to store the value of exciseRateId

            int rateId;
            int.TryParse(this.exciseRateId, out rateId);
            if (rateId > 0)
            {
                districtmgntRow.ExciseRateID = rateId;
            }

            districtMgntData.DistrictHeader.Rows.Add(districtmgntRow);

            string districtDetailsXml = TerraScanCommon.GetXmlString(districtMgntData.DistrictHeader);

            string districtFundItemsXml = this.GetDistrictFundItmesXml();
            returnValue = this.form15002Controll.WorkItem.F15002_CreateOrEditDistrictMgmt(this.DistrictIdentity, districtDetailsXml, districtFundItemsXml, TerraScanCommon.UserId);
            if (returnValue != -1)
            {
                this.DistrictIdentity = returnValue;
            }

            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = returnValue;
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        /// <summary>
        /// Checks the duplicate district.
        /// </summary>
        /// <returns>duplicate record status</returns>
        private bool CheckDuplicateDistrict()
        {
            try
            {
                int errorId = -1;
                errorId = this.form15002Controll.WorkItem.F15002_CheckDistrict(this.DistrictIdentity, this.DistrictTextBox.Text.Trim(), Convert.ToInt32(this.RollYearTextBox.Text.Trim()));
                if (errorId != -1)
                {
                    return true;
                }

                return false;
            }
            catch (SoapException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the invalid funds.
        /// </summary>
        /// <returns>returns the invalid funds status</returns>
        private bool CheckInvalidFunds()
        {
            foreach (DataRow invalidRow in this.districtMgmtDataSet.ListDistrictFunds.Rows)
            {
                if (!string.IsNullOrEmpty(invalidRow[this.districtMgmtDataSet.ListDistrictFunds.RollYearColumn.ColumnName].ToString()))
                {
                    if (!string.Equals(this.RollYearTextBox.Text.Trim(), invalidRow[this.districtMgmtDataSet.ListDistrictFunds.RollYearColumn.ColumnName].ToString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Sets the grid sort mode.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void SetGridSortMode(bool enable)
        {
            this.DisrictandFundsGridView.AllowSorting = enable;
            this.AllFundsGridView.AllowSorting = enable;
        }

        /// <summary>
        /// Checks the valid district number.
        /// </summary>
        /// <returns>the Valid District Number Status</returns>
        private bool CheckValidDistrictNumber()
        {
            try
            {
                if (this.DistrictTextBox.NumericTextBoxValue > 0 && this.DistrictTextBox.NumericTextBoxValue <= Int16.MaxValue)
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

        #endregion

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DistrictTypeCOmbo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DistrictTypeCOmbo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception)
            {
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateRollYear())
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.RollYearTextBox.Focus();
                }
                else
                {

                    Form DistrictCopy = new Form();
                    object[] optionalParameter = new object[] { this.RollYearTextBox.Text, this.DistrictTypeCOmbo.SelectedIndex, this.ActiveComboBox.SelectedIndex, this.Exciselabel.Text, this.DistrictTypeCOmbo.Text, this.exciseRateId, this.districtId };
                    DistrictCopy = this.form15002Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1505, optionalParameter, this.form15002Controll.WorkItem);
                    if (DistrictCopy != null)
                    {
                        if (DistrictCopy.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void DisrictandFundsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.DisrictandFundsGridView != null)
                {
                    if (this.DisrictandFundsGridView.Rows.Count > 0)
                    {
                        this.scrollPosition = this.DisrictandFundsGridView.FirstDisplayedScrollingRowIndex;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }






    }

}


