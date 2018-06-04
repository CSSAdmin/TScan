//--------------------------------------------------------------------------------------------
// <copyright file="F16040.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Improvement District Definition.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//20170614          Dhineshkumar       Improvement District Parcels - New Form
//*********************************************************************************/

namespace D10041
{
    using System;
    using System.Windows.Forms;
    using System.Collections;
    using System.ComponentModel;
    using System.Collections.Specialized;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    [SmartPart]
    public partial class F16041 : BaseSmartPart
    {
        /// <summary>
        /// flag to identify form master new permission
        /// </summary>
        private bool flagMasterNew;
        
        /// <summary>
        /// Instance of F14060 Controller to call the WorkItem
        /// </summary>
        private F16041Controller form16041Control;

        /// <summary>
        /// tempParcelRowId
        /// </summary>
        private int tempParcelRowId;

        /// <summary>
        /// Flag for Edit Mode.
        /// </summary>
        private bool isEditable=false;

        /// <summary>
        /// flag to identify the form is being loading
        /// </summary>
        private bool flagFormLoad = true;

        /// <summary>
        /// District parcel property.
        /// </summary>
        private string districtProperty;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Parcel Id Value.
        /// </summary>
        private int parcelIdValue;

        /// <summary>
        ///  Store the Button AffDvt Operation
        /// </summary>
        private bool affdvtRemove;

        /// <summary>
        /// Roll Year Value.
        /// </summary>
        private int rollyearValue=0;

        /// <summary>
        /// Owner pic box click
        /// </summary>
        private bool isOwerPictureBoxClick=false;

        /// <summary>
        /// Parcel Icon click
        /// </summary>
        private bool isParcelRollYearValue = false;

        /// <summary>
        /// Selected Owner Id Value.
        /// </summary>
        private int selectedOwnerIDValue;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        ///  Store the Button AffDvt Operation
        /// </summary>
        private int affdvtButtonOperation;

        /// <summary>
        /// To hold active record
        /// </summary>
        private int activeRecord;

        /// <summary>
        /// used to keep track the parcel grid row
        /// </summary>
        private int parcelRowId;

        /// <summary>
        /// default roll year variable 
        /// </summary>
        private int defaultRollYear;

        /// <summary>
        /// Parcel roll year variable 
        /// </summary>
        private int parcelRollYear;

        /// <summary>
        /// config Roll year Value.
        /// </summary>
        private int configRollYear;

        /// <summary>
        /// Working File Id.
        /// </summary>
        private string workingFileID;

        /// <summary>
        /// parcelHeaderKeyPressed
        /// </summary>
        private bool parcelHeaderKeyPressed;

        /// <summary>
        /// parcelButton 
        /// </summary>
        private int parcelButtonOperation;

        /// <summary>
        /// keyParcelId
        /// </summary>
        private int parcelRecordCount;

        /// <summary>
        /// Flag to identify parcel number field changes
        /// </summary>
        private bool isParcelEdited = false;

        /// <summary>
        /// to store parcelGridClick
        /// </summary>
        private bool parcelGridClick;

        /// <summary>
        /// array list.
        /// </summary>
        ArrayList arrayList;

        /// <summary>
        /// To store formLoaded
        /// </summary>
        private bool formLoaded;

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  Store the Button AffDvt Operation
        /// </summary>
        private int improvementParcelButtonOperation;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// Used To Store partiesRowCount
        /// </summary>
        private int partiesRowCount;

        /// <summary>
        ///  Keep A Track Of Selected Parcel Column Position
        /// </summary>
        private int parcelCoulmnIndex;

        /// <summary>
        /// currentParcelRowId
        /// </summary>
        private int currentParcelRowId;

        /// <summary>
        ///  Used To Store Parcel Grid Column
        /// </summary>
        private int parcelColumnId;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        /// <summary>
        /// distribution details table.s
        /// </summary>
        private F16041ImprovementDistrictParcels.GetParcelgridDetailsDataTable bindedParcelDetailsTable;

        /// <summary>
        /// Improvement District Definition Details.
        /// </summary>
        private F16041ImprovementDistrictParcels getImprovementParceldata = new F16041ImprovementDistrictParcels();
        
        /// <summary>
        /// Get Parcels Grid Value
        /// </summary>
        private F16041ImprovementDistrictParcels.GetParcelgridDetailsDataTable getParcelsGridValue = new F16041ImprovementDistrictParcels.GetParcelgridDetailsDataTable();

        /// <summary>
        /// Initializes the new instances of <see cref="T:F16041"/> class.
        /// </summary>
        public F16041()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the new instances of <see cref=""/> class.
        /// </summary>
        /// <param name="masterForm"></param>
        /// <param name="formNo"></param>
        /// <param name="keyID"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="tabText"></param>
        /// <param name="permissionEdit"></param>
        public F16041(int masterForm, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.bindedParcelDetailsTable = new F16041ImprovementDistrictParcels.GetParcelgridDetailsDataTable();
            this.masterFormNo = masterForm;
            this.Tag = formNo;
            this.activeRecord = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
        }

        /// <summary>
        /// Gets or sets the form1030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F16041Controller Form10041control
        {
            get { return this.form16041Control; }
            set { this.form16041Control = value; }
        }

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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event PageStatusActivated        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Declare the event for raising new operation in form master        
        /// </summary> 
        [EventPublication(EventTopicNames.D9030_F9030_RaiseFormMasterNew, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9030_RaiseFormMasterNew;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this.slicePermissionField.newPermission && (this.Controls.Count > 0))
                {
                    //this.pageMode = TerraScanCommon.PageModeTypes.New;
                    //this.EnableControls(true);
                    //this.LockControls(false);
                    //this.NewButtonClick();
                }
                else
                {
                    //this.pageMode = TerraScanCommon.PageModeTypes.View;
                    //this.NewButtonClick();
                    //this.LockControls(true);
                    //this.EnableControls(false);
                }
            }
            catch (Exception expe)
            {
            }
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            try
            {
            //    this.flagUpdate = false;
            //    if (this.Controls.Count > 0)
            //    {
            //        this.ClearImprovementDistrictDetails();
            //        if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            //        {
            //            this.LockControls(false);
            //            this.EnableControls(true);
            //        }
            //        else
            //        {
            //            this.EnableControls(false);
            //            this.LockControls(true);
            //        }
            //        this.FillDetails(this.activeRecord);
            //        this.LoadDistributionComboExist();
            //        this.pageMode = TerraScanCommon.PageModeTypes.View;
            //        if (!this.flagUpdate)
            //        {
            //            this.DistributionPanel.Enabled = false;
            //            this.EnableNewMode();
            //        }
                //}
            }
            catch (Exception expr)
            {
            }
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                //if (this.slicePermissionField.newPermission || this.slicePermissionField.editPermission)
                //{
                //    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                //}
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    SliceReloadActiveRecord currentSliceInfo;
                    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    currentSliceInfo.SelectedKeyId = this.activeRecord;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                }
            }
        }

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.flagFormLoad = true;
                this.flagMasterNew = this.GetFormMasterNewPermission();

                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                //this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    //this.EnableControls(true);
                }
                else
                {
                    //.EnableControls(false);
                }

                eventArgs.Data.FlagInvalidSliceKey = false;
            }
            else
            {
                //this.LockControls(true);
                //this.EnableControls(false);
                //// Coding Added for the issue 4212 0n 30/5/2009.
                //// Last Slice does not have a record also it will not return invalid slice
                if (eventArgs.Data.FlagInvalidSliceKey)
                {
                    eventArgs.Data.FlagInvalidSliceKey = true;
                }
            }
            this.flagFormLoad = false;
        }


        /// <summary>
        /// Gets the form master new permission.
        /// </summary>
        /// <returns>bool</returns>
        private bool GetFormMasterNewPermission()
        {
            if ((this.Parent != null) && (this.Parent.Parent != null) && (this.Parent.Parent.Parent != null))
            {
                if (this.Parent.Parent.Parent is BaseSmartPart)
                {
                    return ((BaseSmartPart)this.Parent.Parent.Parent).PermissionFiled.newPermission;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
                    this.flagFormLoad = true;
                    this.formLoaded = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.activeRecord = eventArgs.Data.SelectedKeyId;
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.ClearParcelHeader();
                        this.LoadDistrictParcelDetails();
                        this.LoadParcelHeaderValues();
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                        this.parcelHeaderKeyPressed = false;
                    }
                    else
                    {
                        this.ClearParcelHeader();
                        this.LoadDistrictParcelDetails();
                        this.LoadParcelHeaderValues();
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                        this.parcelHeaderKeyPressed = false;
                    }
                    this.flagFormLoad = false;
                    this.formLoaded = true;
                    this.isParcelEdited = false;
                }
            }
            catch (Exception ex)
            {
            }
        }


        #region Event Subscription

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus())
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form16041Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        #endregion Events Subscription

        #endregion Event Subscription

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
            }
        }

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

        #endregion Protected methods
        /// <summary>
        /// Form load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F16041_Load(object sender, EventArgs e)
        {
            this.flagFormLoad = true;
            this.FlagSliceForm = true;
            //this.EnableControls(false); 
            this.EnableParcelHeaderPanels(false);
            //this.SetDefaultRollYear();
            this.LoadDistrictParcelDetails();
            this.SetParcelGridButtons(ButtonOperation.Empty);
            this.DistrictHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictHeaderPictureBox.Height, this.DistrictHeaderPictureBox.Width, SharedFunctions.GetResourceString("District"), 174, 150, 94);
            this.DistributionItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistributionItemPictureBox.Height, this.DistributionItemPictureBox.Width, SharedFunctions.GetResourceString("Parcels"), 28, 81, 128);
            this.SummaryDetailsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SummaryDetailsPictureBox.Height, this.SummaryDetailsPictureBox.Width, SharedFunctions.GetResourceString("Summary"), 0, 64, 0);
            this.formLoaded = true;
            this.parcelIdValue = 0;
            this.isOwerPictureBoxClick = false;
            this.isParcelRollYearValue = false;
        }

        /// <summary>
        /// Load Parcel header Values.
        /// </summary>
        private void LoadParcelHeaderValues()
        {
            try
            {
                if (getImprovementParceldata.GetParcelgridDetails.Rows.Count > 0)
                {
                    if (!getImprovementParceldata.GetParcelgridDetails[0].IsParcelNumberNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.GetParcelgridDetails[0].ParcelNumber.ToString()))
                        {
                            this.ParcelNumberTextBox.Text = getImprovementParceldata.GetParcelgridDetails[0].ParcelNumber.Trim().ToString();
                        }
                    }
                    else { this.ParcelNumberTextBox.Text = string.Empty; }


                    if (!getImprovementParceldata.GetParcelgridDetails[0].IsLastFirstNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.GetParcelgridDetails[0].LastFirst.ToString()))
                        {
                            this.ParcelOwnerTextBox.Text = getImprovementParceldata.GetParcelgridDetails[0].LastFirst.Trim().ToString();
                        }
                    }
                    else { this.ParcelOwnerTextBox.Text = string.Empty; }


                    if (!getImprovementParceldata.GetParcelgridDetails[0].IsLegalNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.GetParcelgridDetails[0].Legal.ToString()))
                        {
                            this.ParcelLegalTextBox.Text = getImprovementParceldata.GetParcelgridDetails[0].Legal.Trim().ToString();
                        }
                    }
                    else { this.ParcelLegalTextBox.Text = string.Empty; }

                    if (!getImprovementParceldata.GetParcelgridDetails[0].IsPropertyTaxAmountNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.GetParcelgridDetails[0].PropertyTaxAmount.ToString()))
                        {
                            this.ParcelLevyAmountTextBox.Text = getImprovementParceldata.GetParcelgridDetails[0].PropertyTaxAmount.Trim().ToString();
                        }
                    }
                    else { this.ParcelLevyAmountTextBox.Text = string.Empty; }
                }
            }
            catch (Exception exe)
            { }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>the status of the page</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                   // return ;
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    return true;
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// Load district parcel details event.
        /// </summary>
        private void LoadDistrictParcelDetails()
        {
            this.getImprovementParceldata.GetDistrictParcels.Clear();
            this.getImprovementParceldata.GetParcelgridDetails.Clear();
            this.getImprovementParceldata.GetSummaryDetails.Clear();
            this.bindedParcelDetailsTable.Clear();

            getImprovementParceldata = this.form16041Control.WorkItem.GetDistrictParcels(this.activeRecord);

            this.PopulateDistrictParcelDetails();
            this.PopulateParcelGridViewDetails();
            this.PopulateSummaryDetails();
        }

        /// <summary>
        /// Populate Parcel GridView Details.
        /// </summary>
        private void PopulateParcelGridViewDetails()
        {
            this.CustomiseParcelGridView();
            this.SetParcelGrid();
        }

        /// <summary>
        /// Populate District Parcels Details.
        /// </summary>
        private void PopulateDistrictParcelDetails()
        {
            try
            {
                if (getImprovementParceldata.GetDistrictParcels.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(getImprovementParceldata.GetDistrictParcels[0].SADistrictID.ToString()))
                    {
                        this.IDNumberTextBox.Text = getImprovementParceldata.GetDistrictParcels[0].District.Trim().ToString();
                    }

                    if (!getImprovementParceldata.GetDistrictParcels[0].IsLevyDateNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.GetDistrictParcels[0].LevyDate.ToString()))
                        {
                            this.LevyDateTextBox.Text = this.getImprovementParceldata.GetDistrictParcels[0].LevyDate.ToString();
                        }
                    }

                    else { this.LevyDateTextBox.Text = string.Empty; }


                    //priya
                    if (!getImprovementParceldata.GetDistrictParcels[0].IsRollYearNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.GetDistrictParcels[0].RollYear.ToString()))
                        {
                            this.parcelRollYear = Convert.ToInt32( this.getImprovementParceldata.GetDistrictParcels[0].RollYear.ToString());
                        }
                    }

                    else { this.parcelRollYear = 0; }

                    //priya

                    if (!getImprovementParceldata.GetDistrictParcels[0].IsFirstDueNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.GetDistrictParcels[0].FirstDue.ToString()))
                        {
                            this.FirstDueTextBox.Text = this.getImprovementParceldata.GetDistrictParcels[0].FirstDue.ToString();
                        }
                    }
                    else { this.FirstDueTextBox.Text = string.Empty; }

                    if (!string.IsNullOrEmpty(getImprovementParceldata.GetDistrictParcels[0].SAName.ToString()))
                    {
                        this.ImpDistrictNameTextBox.Text = getImprovementParceldata.GetDistrictParcels[0].SAName.Trim().ToString();
                    }

                    if (!string.IsNullOrEmpty(getImprovementParceldata.GetDistrictParcels[0].IDType.ToString()))
                    {
                        this.DistrictTypeTextBox.Text = this.getImprovementParceldata.GetDistrictParcels[0].IDType.Trim().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// load summary details
        /// </summary>
        private void PopulateSummaryDetails()
        {
            try
            {
                decimal levyAmt = 0.0m;
                decimal paidAmt = 0.0m;

                this.NoOfParcelText.Text = getImprovementParceldata.GetSummaryDetails[0]["ParcelCount"].ToString();

                decimal.TryParse(getImprovementParceldata.GetSummaryDetails[0]["TotalLevied"].ToString(), out levyAmt);
                this.TotalLeviedAmtTextBox.Text = levyAmt.ToString();

                decimal.TryParse(getImprovementParceldata.GetSummaryDetails[0]["TotalPaid"].ToString(), out paidAmt);
                this.TotalPaidAmtTextBox.Text = paidAmt.ToString();
            }
            catch (Exception ex)
            {
            }
        }

         /// <summary>
        /// Enables / Disables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool disable)
        {
            //this.ParcelNumberPanel.Enabled = disable;
            //this.OwnerPanel.Enabled = disable;
            //this.LevyAmountPanel.Enabled = disable;
            //this.LegalPanel.Enabled = disable;
            //this.AddButton.Enabled = !disable;
            //this.UpdateButton.Enabled = disable;
            //this.RemoveButton.Enabled = disable;
            //this.CancelButton.Enabled = disable;
            //this.ParcelGridView.ReadOnly = disable;
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomiseParcelGridView()
        {
            this.getParcelsGridValue.Clear();
            this.ParcelGridView.AllowUserToResizeColumns = false;
            this.ParcelGridView.AllowUserToResizeRows = false;
            this.ParcelGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.ParcelGridView.Columns;
            columns["ParcelNumber"].DataPropertyName = "ParcelNumber";
            columns["Owner"].DataPropertyName = "LastFirst";
            columns["LegalDescription"].DataPropertyName = "Legal";
            columns["LevyAmount"].DataPropertyName = "PropertyTaxAmount";
            columns["CurrentFileID"].DataPropertyName = "WorkingFileID";
            columns["OwnerIDVal"].DataPropertyName = "OwnerID";
            columns["RollYear"].DataPropertyName = "RollYear";
            columns["ParcelNumber"].DisplayIndex = 0;
            columns["Owner"].DisplayIndex = 1;
            columns["LegalDescription"].DisplayIndex = 2;
            columns["LevyAmount"].DisplayIndex = 3;
            columns["CurrentFileID"].DisplayIndex = 4;
            columns["OwnerIDVal"].DisplayIndex = 5;
            columns["RollYear"].DisplayIndex = 6;
        }

        /// <summary>
        /// Loads the parcel grid.
        /// </summary>
        private void SetParcelGrid()
        {
            try
            {
                //this.getParcelsGridValue.Clear();
                this.ParcelGridView.NumRowsVisible = 10;
                this.getParcelsGridValue = getImprovementParceldata.GetParcelgridDetails;
                this.parcelRecordCount = this.getImprovementParceldata.GetParcelgridDetails.Rows.Count;

                if (this.parcelRecordCount >= this.ParcelGridView.NumRowsVisible)
                {
                    this.DistrictParcelScrollBar.Enabled = true;
                    this.DistrictParcelScrollBar.Visible = false;
                }
                else
                {
                    this.DistrictParcelScrollBar.Enabled = false;
                    this.DistrictParcelScrollBar.Visible = true;
                }
                if (this.parcelRecordCount > 0)
                {
                    this.EnableParcelHeaderPanels(true);
                }
                else { this.EnableParcelHeaderPanels(false); }

                //if (parcelRecordCount >= this.ParcelGridView.NumRowsVisible)
                //{
                //    //if ((parcelRecordCount) == this.getParcelsGridValue.Rows.Count)
                //    //{
                //        this.getParcelsGridValue.AddGetParcelgridDetailsRow(this.getParcelsGridValue.NewGetParcelgridDetailsRow());
                //    //}
                //}
                this.ParcelGridView.DataSource = this.getParcelsGridValue;
                this.bindedParcelDetailsTable = this.getParcelsGridValue;
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Add Parcel Values.
        /// </summary>
        private void SaveParcelValues()
        {
            try
            {
                if (arrayList != null)
                {
                    this.arrayList.Clear();
                }
                ///// if its New / Update Opeation Then Save 
                if (this.ParcelMantadtoryField())
                {
                    if (this.parcelButtonOperation == (int)ButtonOperation.New)
                    {
                        F16041ImprovementDistrictParcels getImprovementParcelValues = new F16041ImprovementDistrictParcels();
                        F16041ImprovementDistrictParcels.ImprovementParcelRow improvementParcelRow;
                        improvementParcelRow = getImprovementParcelValues.ImprovementParcel.NewImprovementParcelRow();

                        improvementParcelRow.ParcelNumber = this.ParcelNumberTextBox.Text.Trim().ToString();
                        improvementParcelRow.SADistrictID = this.activeRecord.ToString();
                        getImprovementParcelValues.ImprovementParcel.Rows.Clear();
                        getImprovementParcelValues.ImprovementParcel.Rows.Add(improvementParcelRow);
                        getImprovementParcelValues.ImprovementParcel.AcceptChanges();
                        this.parcelRecordCount = getImprovementParcelValues.ImprovementParcel.Rows.Count;

                        DataSet tempDataSet = new DataSet("Root");
                        tempDataSet.Tables.Add(getImprovementParcelValues.ImprovementParcel.Copy());
                        tempDataSet.Tables[0].TableName = "Table";

                        string checkParcelValues = tempDataSet.GetXml();

                        string valueCheck = this.form16041Control.WorkItem.CheckParcelDetails(checkParcelValues);

                        if (valueCheck == "Record cannot be saved. There is already an Assessment for this Parcel and District.")
                        {
                            MessageBox.Show(valueCheck, "TerraScan - Cannot Save Asssessment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else//To Add the Parcel Header Values.
                        {
                            this.Cursor = Cursors.WaitCursor;
                            this.CreateNewParcelRow();
                            this.LoadDistrictParcelDetails();
                            if (this.parcelRecordCount > 0)
                            {
                                this.parcelRowId = this.parcelRecordCount - 1;
                                TerraScanCommon.SetDataGridViewPosition(this.ParcelGridView, this.parcelRowId);
                                this.SetParcelHeaderTextBox(this.parcelRowId);
                                this.tempParcelRowId = this.parcelRowId;
                            }
                            this.ParcelGridView.Focus();

                            if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                            {
                                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            }

                            this.Cursor = Cursors.Default;
                            this.parcelButtonOperation = (int)ButtonOperation.Empty;
                            this.SetParcelGridButtons(ButtonOperation.Empty);
                            this.parcelHeaderKeyPressed = false;
                        }
                    }
                    else //To Update the Parcel Header Values.
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.UpdateParcelHeader(this.parcelRowId);
                        this.LoadDistrictParcelDetails();
                        if (this.parcelRecordCount > 0)
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.ParcelGridView, this.parcelRowId);
                            this.SetParcelHeaderTextBox(this.parcelRowId);
                            this.ParcelGridView.Enabled = true;
                            this.ParcelGridView.Focus();
                        }
                        else
                        {
                            this.ParcelGridView.Enabled = false;
                        }

                        if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
                        }
                        this.Cursor = Cursors.Default;
                        this.parcelButtonOperation = (int)ButtonOperation.Empty;
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                        this.parcelHeaderKeyPressed = false;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Create new Parcel Row
        /// </summary>
        private void CreateNewParcelRow()
        {
            try
            {
                F16041ImprovementDistrictParcels getParcelHeaderData = new F16041ImprovementDistrictParcels();
                F16041ImprovementDistrictParcels.ParcelHeaderRow saveParcelHeaderRow;
                saveParcelHeaderRow = getParcelHeaderData.ParcelHeader.NewParcelHeaderRow();

                if (this.parcelIdValue != 0)
                {
                    saveParcelHeaderRow.ParcelID = this.parcelIdValue.ToString();
                }

                saveParcelHeaderRow.ParcelNumber = this.ParcelNumberTextBox.Text.ToString();

                decimal leviedValue;
                if (!string.IsNullOrEmpty(this.ParcelLevyAmountTextBox.Text.Trim().Replace("$", "")))
                {
                    leviedValue = Decimal.Parse(this.ParcelLevyAmountTextBox.Text.Trim().Replace("$", ""));
                }
                else
                {
                    leviedValue = 0;
                }

                saveParcelHeaderRow.TaxAmount = leviedValue.ToString();


                saveParcelHeaderRow.OwnerID = this.selectedOwnerIDValue.ToString();

                if (this.rollyearValue != 0)
                {
                    saveParcelHeaderRow.RollYear = this.rollyearValue.ToString();
                }

                saveParcelHeaderRow.SADistrictID = this.activeRecord.ToString();

                if (this.ParcelLegalTextBox.Text != string.Empty)
                {
                    saveParcelHeaderRow.Legal = this.ParcelLegalTextBox.Text.ToString();
                }
                else
                {
                    this.ParcelLegalTextBox.Text = string.Empty;
                }

                getParcelHeaderData.ParcelHeader.Rows.Clear();
                getParcelHeaderData.ParcelHeader.Rows.Add(saveParcelHeaderRow);
                getParcelHeaderData.ParcelHeader.AcceptChanges();
                this.parcelRecordCount = getParcelHeaderData.ParcelHeader.Rows.Count;

                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(getParcelHeaderData.ParcelHeader.Copy());
                tempDataSet.Tables[0].TableName = "Table";

                string districtProperty = tempDataSet.GetXml();

                workingFileID = this.form16041Control.WorkItem.F16041_SaveDistrictParcels(districtProperty, TerraScanCommon.UserId);
                this.isOwerPictureBoxClick = false;
                this.isParcelRollYearValue = false;
                this.parcelIdValue = 0;
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Updates the parcel header.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void UpdateParcelHeader(int rowId)
        {
            try
            {
                F16041ImprovementDistrictParcels getUpdateParcelData = new F16041ImprovementDistrictParcels();
                F16041ImprovementDistrictParcels.ParcelHeaderRow updateParcelHeaderRow;
                updateParcelHeaderRow = getUpdateParcelData.ParcelHeader.NewParcelHeaderRow();

                string fileID = this.ParcelGridView.Rows[rowId].Cells["CurrentFileID"].Value.ToString();

                updateParcelHeaderRow.WorkingFileID = fileID;

                if (this.isParcelRollYearValue && parcelIdValue!=0)
                {
                    updateParcelHeaderRow.ParcelID = this.parcelIdValue.ToString();
                }

                updateParcelHeaderRow.ParcelNumber = this.ParcelNumberTextBox.Text.ToString();

                decimal leviedValue;
                if (!string.IsNullOrEmpty(this.ParcelLevyAmountTextBox.Text.Trim().Replace("$", "")))
                {
                    leviedValue = Decimal.Parse(this.ParcelLevyAmountTextBox.Text.Trim().Replace("$", ""));
                }
                else
                {
                    leviedValue = 0;
                }

                updateParcelHeaderRow.TaxAmount = leviedValue.ToString();

                if (this.isOwerPictureBoxClick)
                {
                    updateParcelHeaderRow.OwnerID = this.selectedOwnerIDValue.ToString();
                }
                else
                {
                    updateParcelHeaderRow.OwnerID = this.ParcelGridView.Rows[rowId].Cells["OwnerIDVal"].Value.ToString();
                }

                if (this.isParcelRollYearValue)
                {
                    updateParcelHeaderRow.RollYear = this.rollyearValue.ToString();                    
                }
                else 
                { 
                    updateParcelHeaderRow.RollYear = this.ParcelGridView.Rows[rowId].Cells["RollYear"].Value.ToString() ; 
                }

                updateParcelHeaderRow.SADistrictID = this.activeRecord.ToString();

                if (this.ParcelLegalTextBox.Text != string.Empty)
                {
                    updateParcelHeaderRow.Legal = this.ParcelLegalTextBox.Text.ToString();
                }
                else
                {
                    updateParcelHeaderRow.Legal = string.Empty;
                }

                getUpdateParcelData.ParcelHeader.Rows.Clear();
                getUpdateParcelData.ParcelHeader.Rows.Add(updateParcelHeaderRow);
                getUpdateParcelData.ParcelHeader.AcceptChanges();
                this.parcelRecordCount = getUpdateParcelData.ParcelHeader.Rows.Count;

                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(getUpdateParcelData.ParcelHeader.Copy());
                tempDataSet.Tables[0].TableName = "Table";

                string districtProperty = tempDataSet.GetXml();

                workingFileID = this.form16041Control.WorkItem.F16041_SaveDistrictParcels(districtProperty, TerraScanCommon.UserId);
                this.isOwerPictureBoxClick = false;
                this.isParcelRollYearValue = false;
            }
            catch (Exception exp)
            { 
            }
        }

        /// <summary>
        /// Parcels the mantadtory field.
        /// </summary>
        /// <returns> Return True if Parcel Field is filled else false</returns>
        private bool ParcelMantadtoryField()
        {
            bool parcelMntField;
            decimal leviedValue;
            if (!string.IsNullOrEmpty(this.ParcelLevyAmountTextBox.Text.Trim().Replace("$", "")))
            {
                leviedValue = Decimal.Parse(this.ParcelLevyAmountTextBox.Text.Trim().Replace("$", ""));
            }
            else
            {
                leviedValue = 0;
            }

            if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.ParcelOwnerTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.ParcelLevyAmountTextBox.Text.Trim()))
            {
                parcelMntField = true;
            }
            else
            {
                parcelMntField = false;
            }

            return parcelMntField;
        }

        /// <summary>
        /// Sets the default roll year.
        /// </summary>
        private void SetDefaultRollYear()
        {
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            this.getRollYearConfigurationValue = this.form16041Control.WorkItem.GetConfigDetails(SharedFunctions.GetResourceString("DefaultRollYear"));
            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                if (int.TryParse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.defaultRollYear))
                {
                    this.configRollYear = this.defaultRollYear;
                }
            }
        }

        /// <summary>
        /// To Disable the parcel header panels.
        /// </summary>
        /// <param name="controlDisable">BOOLEAN VALUE</param>
        private void EnableParcelHeaderPanels(bool controlDisable)
        {
            this.ParcelNumberPanel.Enabled = controlDisable;
            this.OwnerPanel.Enabled = controlDisable;
            this.LevyAmountPanel.Enabled = controlDisable;
            this.LegalPanel.Enabled = controlDisable;
        }

        /// <summary>
        /// Button Functionality
        /// </summary>
        public enum ButtonOperation
        {
            /// <summary>
            /// Empty = 0.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// New = 1.
            /// </summary>
            New = 1,

            /// <summary>
            /// Save = 2.
            /// </summary>
            Update = 2,

            /// <summary>
            /// Delete = 3.
            /// </summary>
            Remove = 3,

            /// <summary>
            /// Cancel = 4.
            /// </summary>
            Cancel = 4,

            /// <summary>
            /// For HeaderPartNew
            /// </summary>
            HeaderPartNew = 5,

            /// <summary>
            /// For HeaderPartUpdate
            /// </summary>
            HeaderPartUpdate = 6,

            /// <summary>
            /// For ReceiptidNotExist
            /// </summary>
            ReceiptidNotExist = 9,

            /// <summary>
            /// For NoRecordFound
            /// </summary>
            NoRecordFound = 10,

            /// <summary>
            /// For Permission
            /// </summary>
            NoPermission = 11
        }
            /// <summary>
        /// Sets the parties grid buttons.
        /// </summary>
        /// <param name="buttonOperation">The button operation.</param>
        private void SetParcelGridButtons(ButtonOperation buttonOperation)
        {
            switch (buttonOperation)
            {
                case ButtonOperation.New:
                    {
                        this.ParcelButtonOprNew();
                        break;
                    }

                case ButtonOperation.Empty:
                    {
                        this.ParcelButtonOprEmpty();
                        break;
                    }

                case ButtonOperation.Update:
                    {
                        this.ParcelButtonOprUpdate();
                        break;
                    }

                case ButtonOperation.Remove:
                    {
                        this.ParcelButtonOprRemove();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        this.ParcelButtonOprCancel();
                        break;
                    }
            }
        }

        /// <summary>
        /// Parcels the button opr new.
        /// </summary>
        private void ParcelButtonOprNew()
        {
            if (this.parcelButtonOperation == (int)ButtonOperation.New)
            {
                this.AddButton.Enabled = false;
                this.UpdateButton.Enabled = true;
                this.RemoveButton.Enabled = false;
                this.CancelButton.Enabled = true;
                this.ParcelGridView.Enabled = false;
            }
            else
            {
                this.AddButton.Enabled = true;
                this.UpdateButton.Enabled = false;
                this.RemoveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                this.ParcelGridView.Enabled = false;
            }
        }

        /// <summary>
        /// Parcels the button opr update.
        /// </summary>
        private void ParcelButtonOprUpdate()
        {
            this.AddButton.Enabled = false;
            this.UpdateButton.Enabled = true;
            this.RemoveButton.Enabled = false;
            this.ParcelGridView.Enabled = true;
            this.CancelButton.Enabled = true;
        }

        /// <summary>
        /// Parcels the button opr remove.
        /// </summary>
        private void ParcelButtonOprRemove()
        {
            if (this.partiesRowCount > 0)
            {
                this.RemoveButton.Enabled = true;
                this.ParcelGridView.Enabled = true;
            }
            else
            {
                this.RemoveButton.Enabled = false;
                this.ParcelGridView.Enabled = false;
            }

            this.AddButton.Enabled = true;
            this.UpdateButton.Enabled = false;
            this.CancelButton.Enabled = false;
        }

        /// <summary>
        /// Parcels the button opr cancel.
        /// </summary>
        private void ParcelButtonOprCancel()
        {
            this.AddButton.Enabled = true;
            this.UpdateButton.Enabled = false;
            this.RemoveButton.Enabled = true;
            this.CancelButton.Enabled = false;
            this.ParcelGridView.Enabled = true;
        }

        /// <summary>
        /// Parcels the button opr empty.
        /// </summary>
        private void ParcelButtonOprEmpty()
        {
            if (this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    this.AddButton.Enabled = true;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = true;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = true;
                }
                else
                {
                    this.AddButton.Enabled = true;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = false;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = false;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    this.AddButton.Enabled = true;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = true;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = true;
                }
                else
                {
                    this.AddButton.Enabled = true;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = false;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = false;
                }
            }
            else if (this.PermissionFiled.newPermission)
            {
                if (this.improvementParcelButtonOperation == (int)ButtonOperation.New)
                {
                    this.AddButton.Enabled = true;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = false;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = true;
                }
                else
                {
                    this.AddButton.Enabled = false;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = false;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = true;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    this.AddButton.Enabled = true;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = true;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = true;
                }
                else
                {
                    this.AddButton.Enabled = true;
                    this.UpdateButton.Enabled = false;
                    this.RemoveButton.Enabled = false;
                    this.CancelButton.Enabled = false;
                    this.ParcelGridView.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Clears the parcel header.
        /// </summary>
        private void ClearParcelHeader()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            //this.ParcelLevyAmountTextBox.TextCustomFormat = "#,##";
            this.ParcelOwnerTextBox.Text = string.Empty;
            this.ParcelLevyAmountTextBox.Text = string.Empty;
            this.ParcelLevyAmountTextBox.TextCustomFormat = "$#,##0.00";
            this.ParcelLegalTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Add Button Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.parcelButtonOperation = (int)ButtonOperation.New;
                this.SetParcelGridButtons(ButtonOperation.New);
                this.formLoaded = false;
                this.ClearParcelHeader();
                this.formLoaded = true;
                this.EnableParcelHeaderPanels(true);
                this.Cursor = Cursors.Default;
                this.ParcelNumberTextBox.Focus();
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

        /// <summary>
        /// Update Button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveParcelValues();
                this.EnableParcelHeaderPanels(true);
                if ((arrayList != null) && arrayList.Count > 0)
                {
                    //this.LoadOpenSpaceField(arrayList);
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

        /// <summary>
        /// Remove Button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.parcelRowId >= 0)
                {
                    if (arrayList != null)
                    {
                        this.arrayList.Clear();
                    }
                    DialogResult dlgResult = MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), "TerraScan T2 – Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int currentParcelRowid = this.ParcelGridView.CurrentRowIndex;
                        this.Cursor = Cursors.WaitCursor;
                        int fileID = Convert.ToInt32(this.getImprovementParceldata.GetParcelgridDetails.Rows[currentParcelRowid]["WorkingFileID"].ToString());
                        this.getImprovementParceldata.GetParcelgridDetails.Rows[currentParcelRowid].Delete();
                        var removeVal = this.form16041Control.WorkItem.F16041_DeleteDistrictParcels(fileID, TerraScanCommon.UserId);

                        string rollBackError = "'An unknown error occurred, which prevented the record from being deleted.  If you continue to see this error message, please contact T2 Support.'";
                        if (removeVal.Equals(rollBackError))
                        {
                            MessageBox.Show(rollBackError, "TerraScan T2 – Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string remString = "This assessment is already associated with an existing statement, so it cannot be deleted. However, you can tax roll correct the statement to remove the amount due.";
                            if (removeVal.Equals(remString))
                            {
                                MessageBox.Show(remString, "TerraScan T2 – Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                this.getImprovementParceldata.GetParcelgridDetails.AcceptChanges();
                                this.parcelHeaderKeyPressed = true;
                                this.LoadDistrictParcelDetails();
                                if (this.parcelRecordCount > 0)
                                {
                                    if (currentParcelRowid == this.parcelRecordCount)
                                    {
                                        currentParcelRowid = 0;
                                    }
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelGridView, currentParcelRowid);
                                    this.formLoaded = false;
                                    this.SetParcelHeaderTextBox(currentParcelRowid);
                                    this.formLoaded = true;
                                    this.parcelRowId = currentParcelRowid;
                                }
                                else
                                {
                                    this.ClearParcelHeader();
                                    this.ParcelGridView.Rows[currentParcelRowid].Selected = false;
                                    this.EnableParcelHeaderPanels(false);
                                    this.SetParcelHeaderTextBox(currentParcelRowid);
                                }
                                this.parcelHeaderKeyPressed = false;
                                this.Cursor = Cursors.Default;
                                this.SetParcelGridButtons(ButtonOperation.Empty);
                                this.affdvtRemove = true;
                                if (arrayList != null && arrayList.Count > 0)
                                {
                                    //LoadOpenSpaceField(arrayList);
                                }
                            }
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

        /// <summary>
        /// Parcel PictureBox Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                selectedOwnerIDValue = 0;
                parcelIdValue = 0;
                rollyearValue = 0;
                Form form1401 = new Form();
                object[] optionalParameter = new object[] { this.parcelRollYear,this.masterFormNo.ToString() };
                form1401 = this.form16041Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, optionalParameter, this.form16041Control.WorkItem);
                if (form1401 != null)
                {
                    if (form1401.ShowDialog() == DialogResult.OK)
                    {
                        int.TryParse(TerraScanCommon.GetValue(form1401, "ParcelID"), out parcelIdValue);
                        int.TryParse(TerraScanCommon.GetValue(form1401, "RollYearId"), out rollyearValue);
                        string parcelNumber = TerraScanCommon.GetValue(form1401, "CommandValue");
                        this.ParcelNumberTextBox.Text = parcelNumber;
                        this.ParcelNumberTextBox.Tag = parcelIdValue;
                        this.FillParcelDetails(parcelNumber, parcelIdValue, rollyearValue);
                        this.isOwerPictureBoxClick = true;                        
                        this.isParcelRollYearValue = true;
                        this.isParcelEdited = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Fill Parcel Details Click Event.
        /// </summary>
        /// <param name="parcelval"></param>
        /// <param name="parcelid"></param>
        /// <param name="rollyear"></param>
        private void FillParcelDetails(string parcelval, int? parcelid, int? rollyear)
        {
            this.getImprovementParceldata.ListParcelDetails.Clear();
            getImprovementParceldata = this.form16041Control.WorkItem.ListDistrictParcelsDetails(parcelval, parcelid, rollyear);

            try
            {
                if (getImprovementParceldata.ListParcelDetails.Rows.Count > 0)
                {

                    if (!getImprovementParceldata.ListParcelDetails[0].IsParcelNumberNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.ListParcelDetails[0].ParcelNumber.ToString()))
                        {
                            this.ParcelNumberTextBox.Text = getImprovementParceldata.ListParcelDetails[0].ParcelNumber.Trim().ToString();
                        }
                    }
                    //else { this.ParcelNumberTextBox.Text = string.Empty; }


                    if (!getImprovementParceldata.ListParcelDetails[0].IsOwner_NameNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.ListParcelDetails[0].Owner_Name.ToString()))
                        {
                            this.ParcelOwnerTextBox.Text = getImprovementParceldata.ListParcelDetails[0].Owner_Name.Trim().ToString();
                        }
                    }
                    //else { this.ParcelOwnerTextBox.Text = string.Empty; }


                    if (!getImprovementParceldata.ListParcelDetails[0].IsLegalNotesNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.ListParcelDetails[0].LegalNotes.ToString()))
                        {
                            this.ParcelLegalTextBox.Text = getImprovementParceldata.ListParcelDetails[0].LegalNotes.Trim().ToString();
                        }
                    }
                    //else { this.ParcelLegalTextBox.Text = string.Empty; }


                    if (!getImprovementParceldata.ListParcelDetails[0].IsOwnerIDNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.ListParcelDetails[0].OwnerID.ToString()))
                        {
                            this.selectedOwnerIDValue = Convert.ToInt32(getImprovementParceldata.ListParcelDetails[0].OwnerID.ToString());
                        }
                    }
                    //else { this.selectedOwnerIDValue = 0; }

                    if (!getImprovementParceldata.ListParcelDetails[0].IsParcelIDNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.ListParcelDetails[0].ParcelID.ToString()))
                        {
                            this.parcelIdValue = Convert.ToInt32(getImprovementParceldata.ListParcelDetails[0].ParcelID.ToString());
                        }
                    }
                    else { this.parcelIdValue = 0; }

                    if (!getImprovementParceldata.ListParcelDetails[0].IsParcelRollYearNull())
                    {
                        if (!string.IsNullOrEmpty(getImprovementParceldata.ListParcelDetails[0].ParcelRollYear.ToString()))
                        {
                            this.rollyearValue = Convert.ToInt32(getImprovementParceldata.ListParcelDetails[0].ParcelRollYear.ToString());
                        }
                    }
                    else { this.rollyearValue = 0; }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// District Definition Button Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollDistrictButton_Click(object sender, EventArgs e)
        {
            try 
            { 
                //FormInfo formInfo = TerraScanCommon.GetFormInfo(10040);
                //formInfo.optionalParameters = new object[] { null };
                //this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(10040);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.activeRecord;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                //this.Close();
            }
            catch (Exception ex) 
            { 
            }
        }

        /// <summary>
        /// Parcel Cancel Button Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                int tempCurrentParcelRowId = this.currentParcelRowId;
                this.LoadDistrictParcelDetails();
                //this.SetParcelGrid();
                if (this.parcelRecordCount > 0)
                {
                    this.ParcelGridView.Rows[tempCurrentParcelRowId].Selected = true;
                    this.ParcelGridView.CurrentCell = this.ParcelGridView[2, tempCurrentParcelRowId];
                    this.formLoaded = false;
                    this.SetParcelHeaderTextBox(tempCurrentParcelRowId);
                    this.formLoaded = true;
                }
                else
                {
                    this.ParcelGridView.Enabled = false;
                    this.ParcelGridView.Rows[tempCurrentParcelRowId].Selected = false;
                    this.ClearParcelHeader();
                    this.EnableParcelHeaderPanels(false);
                }

                this.parcelButtonOperation = (int)ButtonOperation.Empty;
                this.SetParcelGridButtons(ButtonOperation.Empty);
                this.parcelHeaderKeyPressed = false;
                if (this.parcelRecordCount > 0)
                {
                    this.ParcelNumberTextBox.Focus();
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

        /// <summary>
        /// Sets the parcel header text box.
        /// </summary>
        /// <param name="parcelRowIdSelect">The parcel row id select.</param>
        private void SetParcelHeaderTextBox(int parcelRowIdSelect)
        {
            try
            {
                if (parcelRowIdSelect >= 0)
                {
                    this.ParcelNumberTextBox.Text = this.ParcelGridView.Rows[parcelRowIdSelect].Cells["ParcelNumber"].Value.ToString();
                    this.ParcelOwnerTextBox.Text = this.ParcelGridView.Rows[parcelRowIdSelect].Cells["Owner"].Value.ToString();
                    this.ParcelLegalTextBox.Text = this.ParcelGridView.Rows[parcelRowIdSelect].Cells["LegalDescription"].Value.ToString();
                    this.ParcelLevyAmountTextBox.Text = this.ParcelGridView.Rows[parcelRowIdSelect].Cells["LevyAmount"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Parcel Number Key Press Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar !=27)
                {
                    this.SetParcelHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the parcel header to update mode.
        /// </summary>
        private void SetParcelHeaderToUpdateMode()
        {
            if (this.PermissionFiled.editPermission || this.PermissionFiled.newPermission)
            {
                this.parcelHeaderKeyPressed = true;
                this.SetParcelGridButtons(ButtonOperation.Update);
            }
        }

        /// <summary>
        /// Parcel Number TextChanged Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.parcelGridClick && this.formLoaded)
                {
                    this.isParcelEdited = true;
                    if (this.parcelButtonOperation != (int)ButtonOperation.New)
                    {
                        this.SetParcelHeaderToUpdateMode();
                        isEditable = true;
                        this.parcelHeaderKeyPressed = false;
                    }
                }
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
        /// Parcel Number Leave Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelNumberTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.isParcelEdited)
                {
                    string parcelNumber=string.Empty;
                    this.formLoaded = false;
                    if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()))
                    {
                         parcelNumber = this.ParcelNumberTextBox.Text;
                    }
                    this.ParcelOwnerTextBox.Text = "";
                    this.ParcelLegalTextBox.Text = "";
                    if (!string.IsNullOrEmpty(parcelNumber))
                    {
                        int? rollyear = this.parcelRollYear;
                        int? parcelid = null;
                        this.FillParcelDetails(parcelNumber, parcelid, rollyear);
                        this.SetParcelGridButtons(ButtonOperation.Update);
                        if (rollyear == defaultRollYear)
                        {
                            this.isParcelRollYearValue = true;
                        }
                    }
                    this.formLoaded = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.isParcelEdited = false;
            }
        }

        /// <summary>
        /// Parcel gridview Cell click      
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelGridClick = true;
                if (e.RowIndex >= 0)
                {
                    this.parcelCoulmnIndex = e.ColumnIndex;
                    if (this.parcelHeaderKeyPressed)
                    {
                        this.parcelRowId = e.RowIndex;    //// Check if its Same Row Else Do
                        if (this.tempParcelRowId != this.parcelRowId)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelParcel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        if (this.ParcelMantadtoryField())
                                        {
                                            this.UpdateParcelHeader(this.tempParcelRowId);
                                            this.parcelRowId = e.RowIndex;
                                            this.tempParcelRowId = e.RowIndex;
                                            this.SetParcelGrid();
                                            this.LoadDistrictParcelDetails();
                                            this.SetParcelHeaderTextBox(this.tempParcelRowId);
                                            this.SetDataGridCoulmn(this.ParcelGridView, this.tempParcelRowId, this.parcelCoulmnIndex);
                                            this.SetParcelGridButtons(ButtonOperation.Empty);
                                            this.parcelHeaderKeyPressed = false;                                            
                                        }
                                        else
                                        {
                                            MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.parcelRowId = e.RowIndex;
                                        this.tempParcelRowId = e.RowIndex;
                                        this.SetParcelGrid();
                                        this.LoadDistrictParcelDetails();
                                        this.SetParcelHeaderTextBox(this.tempParcelRowId);
                                        this.SetDataGridCoulmn(this.ParcelGridView, this.tempParcelRowId, this.parcelCoulmnIndex);
                                        this.SetParcelGridButtons(ButtonOperation.Empty);
                                        this.parcelHeaderKeyPressed = false;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        this.SetDataGridCoulmn(this.ParcelGridView, this.tempParcelRowId, this.parcelCoulmnIndex);
                                        break;
                                    }
                            } //// End Case
                        } ///// End if
                    }
                    else
                    {
                        this.parcelRowId = e.RowIndex;
                        this.tempParcelRowId = e.RowIndex;
                        this.SetParcelHeaderTextBox(e.RowIndex);
                        this.parcelCoulmnIndex = e.ColumnIndex;
                    }
                }

                this.parcelGridClick = false;
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

        /// <summary>
        /// Parcel Gridview Cell Formatting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;
                if (e.ColumnIndex == this.ParcelGridView.Columns["LevyAmount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("$#,##0.00");
                            e.FormattingApplied = true;
                        }
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }
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
        /// Parcel GridView Row Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelGridClick = true;
                if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                {
                    if (!this.parcelHeaderKeyPressed && this.parcelButtonOperation != (int)ButtonOperation.New && this.parcelGridClick)
                    {
                        this.parcelRowId = e.RowIndex;
                        this.tempParcelRowId = e.RowIndex;
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        this.parcelColumnId = e.ColumnIndex;
                    }
                }
                this.currentParcelRowId = e.RowIndex;
                this.parcelGridClick = false;
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

        /// <summary>
        /// Parcel GridView Key Down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.parcelColumnId = ((DataGridView)sender).CurrentCell.ColumnIndex;
                if (this.parcelHeaderKeyPressed)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                this.GridParcelHeaderCancel(e);
                                break;
                            }

                        case Keys.Up:
                            {
                                this.GridParcelHeaderCancel(e);
                                break;
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

        /// <summary>
        /// Grids the parties cancel.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GridParcelHeaderCancel(KeyEventArgs e)
        {
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelParcel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        this.UpdateParcelHeader(this.parcelRowId);
                        this.SetParcelGrid();
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        this.SetDataGridCoulmn(this.ParcelGridView, this.parcelRowId, this.parcelColumnId);
                        e.Handled = false;
                        this.parcelHeaderKeyPressed = false;
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                        break;
                    }

                case DialogResult.No:
                    {
                        this.SetParcelGrid();
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        this.SetDataGridCoulmn(this.ParcelGridView, this.parcelRowId, this.parcelColumnId);
                        this.parcelHeaderKeyPressed = false;
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                        e.Handled = false;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        e.Handled = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Sets the data grid coulmn.
        /// </summary>
        /// <param name="sourceDataGrid">The source data grid.</param>
        /// <param name="sourceRowId">The source row id.</param>
        /// <param name="sourceColumnId">The source column id.</param>
        private void SetDataGridCoulmn(TerraScan.UI.Controls.TerraScanDataGridView sourceDataGrid, int sourceRowId, int sourceColumnId)
        {
            if (sourceDataGrid.Rows.Count > 0 && sourceRowId >= 0 && sourceColumnId >= 0)
            {
                sourceDataGrid.Rows[Convert.ToInt32(sourceRowId)].Selected = false;
                sourceDataGrid.CurrentCell = sourceDataGrid[sourceColumnId, Convert.ToInt32(sourceRowId)];
                sourceDataGrid.CurrentCell.Selected = true;
            }
        }

        /// <summary>
        /// Text Changed Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelOwnerTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.parcelGridClick && this.formLoaded)
                {
                    this.ParcelOwnerTextBox.Tag = null; ;
                    if (this.parcelButtonOperation != (int)ButtonOperation.New)
                    {
                        this.SetParcelHeaderToUpdateMode();
                        isEditable = true;
                    }
                }
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
        /// Text Changed Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelLegalTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.parcelGridClick && this.formLoaded)
                {
                    this.ParcelLegalTextBox.Tag = null; ;
                    if (this.parcelButtonOperation != (int)ButtonOperation.New)
                    {
                        this.SetParcelHeaderToUpdateMode();
                        isEditable = true;
                    }
                }
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
        /// Text Changed Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelLevyAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.parcelGridClick && this.formLoaded)
                {
                    this.ParcelLevyAmountTextBox.Tag = null; ;
                    if (this.parcelButtonOperation != (int)ButtonOperation.New)
                    {
                        this.SetParcelHeaderToUpdateMode();
                        isEditable = true;
                    }
                }
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
        /// Key Press Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelOwnerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetParcelHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Key Press Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelLegalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetParcelHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Key Press Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelLevyAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetParcelHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Details Button Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsButton_Click(object sender, EventArgs e)
        {
            Form formInfo = new Form();
            object[] optionalParameter = null;
            formInfo = this.form16041Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(10042, optionalParameter, this.form16041Control.WorkItem);
            if (formInfo != null)
            {
                if (formInfo.ShowDialog() == DialogResult.OK)
                {
                    //To be done.
                }
            }
        }

        /// <summary>
        /// Owner Picture Box Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OwnerPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form parcelF9101 = new Form();
                parcelF9101 = this.form16041Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form16041Control.WorkItem);
                if (parcelF9101 != null)
                {
                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                    {
                        this.isOwerPictureBoxClick = true;
                        this.selectedOwnerIDValue = Convert.ToInt32(TerraScanCommon.GetValue(parcelF9101, "CommandResult").ToString());
                        this.ParcelOwnerTextBox.Text = TerraScanCommon.GetValue(parcelF9101, "CommandValue").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ParcelGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.RowIndex >= this.ParcelGridView.NumRowsVisible - 1)
            //    {
            //        if ((e.RowIndex + 1) == this.getParcelsGridValue.Rows.Count)
            //        {
            //            this.getParcelsGridValue.AddGetParcelgridDetailsRow(this.getParcelsGridValue.NewGetParcelgridDetailsRow());
            //            //this.DistrictParcelScrollBar.Visible = false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{ 
                  
            //}
        }

        /// <summary>
        /// Parcel Number Key Down Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.SetParcelHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
