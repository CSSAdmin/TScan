////--------------------------------------------------------------------------------------------
//// <copyright file="F16031.cs" company="Congruent">
////Copyright (c) Congruent Info-Tech.  All rights reserved.
//// </copyright>
//// <summary>
////This file contains UI for F16031 Form Slice - Special District Assesment. 
////</summary>
////----------------------------------------------------------------------------------------------
//// Change History
////**********************************************************************************
//// Date             Author              Description
//// ----------       --------           ---------------------------------------------------------
//// 8-6-2007         Shiva              Created
//// 20/04/2009       Shanmugasundaram.A Modified to implement the CO:6195
//// 07 May 09        Khaja              Made changes to check slice permissions Bugs 4254 and 7229
//// 15 JUL 11        Manoj              Changes in Irrigable text box RP acres  text box Custom Format.
////*********************************************************************************/

namespace D10030
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
    /// F16031 Special District Assesment Slice Functionality.
    /// </summary>
    [SmartPart]
    public partial class F16031 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15005Controll variable.
        /// </summary>
        private F16031Controller form16031Controll;
   
        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData ownerDetailDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// F1410OwnerReceiptingData
        /// </summary>
        private F1410OwnerReceiptingData.ListOwnerStatementTableDataTable ownerReceiptingDataSet = new F1410OwnerReceiptingData.ListOwnerStatementTableDataTable();

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
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// acresEmpty Local variable.
        /// </summary>
        private bool acresEmpty;

        /// <summary>
        /// used to maintain ratesGrid edit status
        /// </summary>
        private bool ratesGridEdited;

        ///<summary>
        ///used to hold OwnerID
        /// </summary>
        private string CurrentOwnerID;


        /// <summary>
        /// propertyInfoDataSet variable is used to get the details of District Assessment Details.
        /// </summary>
        private F1031SpecialDistrictAssessmentData propertyInfoDataSet;

        /// <summary>
        /// used to keep trakc of SelectedRateItem
        /// </summary>
        private BindingSource rateSource = new BindingSource();

        /// <summary>
        /// currentDistrictAssessmentStatementId variable is used to store District Assessment Statement id. 
        /// </summary>       
        private int currentStatementId;

        /// <summary>
        ///  variable is used to store District Assessment Statement id. 
        /// </summary>       
        private bool ispaid;

        /// <summary>
        ///  variable is used to store Special District id. 
        /// </summary>
        private int sadistrictId;

        /// <summary>
        /// variable is used to store amountValue
        /// </summary>
        private decimal amountValue;

        /// <summary>
        /// variable holds the parcel Id value.
        /// </summary>
        private int parcelId;
        private int rollyear1;
        /// <summary>
        /// variable holds the parcel No value.
        /// </summary>
        private string parcelNo;

        /// <summary>
        /// variable holds the OwnerId value.
        /// </summary>
        private int ownerId;

        ///<summary>
        ///vraible holds button click
        ///</summary>
        private bool IsbuttonClick;

        /// <summary>
        /// variable holds the district property details.
        /// </summary>
        private string districtPropertyDetailsXml;

        /// <summary>
        /// variable holds the owner assosiation flag.
        /// </summary>
        private bool ownerFlag;

        /// <summary>
        /// variable holds the destination form number when statement hyperlink clicks.
        /// </summary>
        private int destinationStatementLinkFormNumber;

        /// <summary>
        /// vaiable holds the selectedRateItem value
        /// </summary>
        private int selectedRateItem;

        ///<summary>
        ///Identify new  record 
        /// </summary>
        private bool newRecord = false;

        ///<summary>
        ///Identify loadForm
        /// </summary>
        private bool loadForm = false;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F16031"/> class.
        /// </summary>
        public F16031()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F16031"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if permission is available its set to true</param>
        public F16031(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.propertyInfoDataSet = new F1031SpecialDistrictAssessmentData();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.PropertyInfoPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PropertyInfoPictureBox.Height, this.PropertyInfoPictureBox.Width, "Property Info", red, green, blue);
            this.RatesListingSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.RatesListingSecIndicatorPictureBox.Height, this.RatesListingSecIndicatorPictureBox.Width, "Rates Listing", 0, 51, 0);
            this.midPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.midPictureBox.Height, this.midPictureBox.Width, "", 51, 51, 51);
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

        ///<summary>
        /// event Publication for DeleteSlice Revert
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;
        
        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form16031 controll.
        /// </summary>
        /// <value>The form16031 controll.</value>
        [CreateNew]
        public F16031Controller Form16031Controll
        {
            get { return this.form16031Controll  as F16031Controller; }
            set { this.form16031Controll = value; }
        }

        /// <summary>
        /// Gets or sets the current statement id.
        /// </summary>
        /// <value>The current statement id.</value>
        public int CurrentStatementId
        {
            get
            {
                return this.currentStatementId;
            }

            set
            {
                this.currentStatementId = value;
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_RevertDeleteAlert(DataEventArgs<int> eventArgs)
        {
            if (this.FormSlice_RevertDeleteAlert != null)
            {
                this.FormSlice_RevertDeleteAlert(this, eventArgs);
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if (this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows.Count > 0 && this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > 0)
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
                if (this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows.Count > 0 && this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > 0)
                {
                    this.EnableFormControls(true);
                    this.LockTextBoxControls(false);
                }
                else
                {
                    this.EnableFormControls(false);
                    this.LockTextBoxControls(true);
                }
            }
            else
            {
                ////this.LockControls(true);
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
                if (this.slicePermissionField.newPermission || this.slicePermissionField.editPermission)
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
                    bool status = this.SaveExciseRateRecord();
                    if (status)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.newRecord = false; 
                    }
                }
            }
            else
            {
                this.LockTextBoxControls(true);
                this.EnableFormControls(false);
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
                    this.newRecord = true;
                   // this.keyId=-99; 
                    this.Cursor = Cursors.WaitCursor;
                    this.pageLoadStatus = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ratesGridEdited = false;
                    this.ClearDistrictAssessmentDetails();
                    this.GetYear();
                    this.currentStatementId = 0;
                    this.SpecialDistrictLinkLabel.Tag = 0;
                    this.EnableFormControls(true);
                    this.ParcelNumButton.Enabled = true;
                    this.ParcelNumButton.Focus();
                    this.OwnerNameButton.Enabled = true;
                    this.SpecialDistrictButton.Enabled = true;
                    this.LockTextBoxControls(false);
                    this.pageLoadStatus = false;
                    this.Cursor = Cursors.Default;
                }
                else
                {   ////khaja called clear method to fix Bug#7229
                    this.ClearDistrictAssessmentDetails();
                    this.EnableFormControls(false);
                    this.LockTextBoxControls(true);
                }
                this.writeButton.Enabled = false;
                this.cancelButton.Enabled = false; 
                this.RPAcresCountTextBox.TextCustomFormat = "";
                this.RPAcresCountTextBox.Text = "0.00";
                this.RPAcresCountTextBox.TextCustomFormat = "#,##0.00";
                this.IrrigableAcresTextBox.TextCustomFormat = "";
                this.IrrigableAcresTextBox.Text = "0.00";
                this.IrrigableAcresTextBox.TextCustomFormat = "#,##0.00";
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
                this.EnableFormControls(true);
                this.LockTextBoxControls(false);
            }
            else
            {
                this.EnableFormControls(false);
                this.LockTextBoxControls(true);
            }
            this.newRecord = false;
            this.GetDistrictAssessmentDetails(this.keyId);
            if (this.keyId != -99)
            {
                this.writeButton.Enabled = true;
                this.cancelButton.Enabled = true;
            }
            else
            {
                this.writeButton.Enabled = false;
                this.cancelButton.Enabled = false;
            }
            ////Commented by Biju on 26/Jul/2010 to fix #6497
            ////this.SpecialDistrictButton.Enabled =false ;
            this.acresEmpty = false;
            this.RatesListingGridView.AllowSorting = true;
            this.RatesListingGridView.TabStop = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                if (this.keyId > 0)
                {
                    this.GetDistrictAssessmentDetails(this.keyId);
                    
                    this.pageMode = TerraScanCommon.PageModeTypes.View; 
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockTextBoxControls(false);
                        this.EnableFormControls(true);
                    }
                    else
                    {
                        this.LockTextBoxControls(true);
                        this.EnableFormControls(false);
                    }
                }
                else
                {
                    this.GetDistrictAssessmentDetails(this.keyId);
                    this.LockTextBoxControls(true);
                    this.EnableButtonControls(false);
                    this.EnableFormControls(false);
                }
            }
        }

        //// Added for Deleting the records

        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.PermissionFiled.deletePermission && this.keyId > 0)
            {
                F1031SpecialDistrictAssessmentData outputValue = new F1031SpecialDistrictAssessmentData();
                outputValue = this.form16031Controll.WorkItem.F16031_DeleteDistrictAssessment(this.keyId, TerraScanCommon.UserId);
                if (outputValue.Tables["ListDeleteOutPutValue"].Rows[0]["IsPass"].ToString().Equals("False"))
                {
                    MessageBox.Show(outputValue.Tables["ListDeleteOutPutValue"].Rows[0]["ErrorMessage"].ToString(), "TerraScan – Cannot Delete Assessment", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo)); 
                //this.currentStatementId = returnValue;
                //SliceReloadActiveRecord currentSliceInfo;
                //currentSliceInfo.MasterFormNo = this.masterFormNo;
                //currentSliceInfo.SelectedKeyId = this.keyId;
                //////to refresh the master form with the return keyid
                //this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.Cursor = Cursors.Default;
                }
                else
                {
                   this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.Cursor = Cursors.Default;
                }

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

        #region Form Load Event

        /// <summary>
        /// Handles the Load event of the F16031 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F16031_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.FlagSliceForm = true;
                this.loadForm = false;
                this.CustomizeRatesListingGridView();
                this.GetDistrictAssessmentDetails(this.keyId);
                this.DisplayTotal();
                this.LockTextBoxControls(this.ispaid);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.loadForm = true;
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Gets the district assessment details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        private void GetDistrictAssessmentDetails(int statementId)
        {
            this.pageLoadStatus = true;
            this.propertyInfoDataSet = this.form16031Controll.WorkItem.F16031_ListDistrictAssessmentDetails(statementId);

            if (this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows.Count > 0 && this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > 0)
            {
                //// Fill Property Info
                this.SetControlValues(this.GetSelectedRow(0));

                ////VscrollBar is enabled or disabled based on NumRowsVisible in GridView
                if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > this.RatesListingGridView.NumRowsVisible)
                {
                    this.RatesListingGridVscrollBar.Visible = false;
                }
                else
                {
                    this.RatesListingGridVscrollBar.Visible = true;
                }

                //// Fill Rates Listing Grid
                this.RatesListingGridView.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates.DefaultView;

                this.rateSource.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates.DefaultView;

                //// Calculate the Total Amount.
                ////khaja made changes to fix Bug#6031 & added permissions Bug#4254
                ////decimal.TryParse(this.propertyInfoDataSet.ListDistrictAssessmentRates.Compute("SUM(Total)", "").ToString(), out this.amountValue);
                ////this.TotalAssessmentValueTextBox.Text = this.amountValue.ToString();
                this.DisplayTotal();

                ////Coding added for the issue 3131.first form load will fire after that only set slice permission 
                ////will fire.so this.slicePermissionField.editPermission in form load it will be false.because of this only this came.

                ////if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                if (this.formMasterPermissionEdit)
                {
                    this.EnableFormControls(true);
                    this.OwnerNameButton.Enabled = true;
                    this.LockTextBoxControls(!this.PermissionEdit);
                    this.EnableButtonControls(!this.ispaid);
                }
                else
                {
                    this.EnableFormControls(false);
                    this.OwnerNameButton.Enabled = true;
                    this.LockTextBoxControls(this.PermissionEdit);
                    this.EnableButtonControls(this.ispaid);
                }
                if (this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0]["IsCancelled"].ToString().Equals("False"))
                {
                    this.cancelButton.Enabled = true;
                }
                else
                {
                    this.cancelButton.Enabled = false;
                }
            }
            else
            {
                this.ClearDistrictAssessmentDetails();
                this.PropertyInfoPanel.Enabled = false;
                this.RatesListingGridView.CurrentCell = null;
                this.GridPanel.Enabled = false;
            }
            this.writeButton.Enabled = true; 
            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Gets the selected row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>returns SpecialDistrictAssessmentData dataset</returns>
        private F1031SpecialDistrictAssessmentData.ListDistrictAssessmentPropertyRow GetSelectedRow(int rowIndex)
        {
            return (F1031SpecialDistrictAssessmentData.ListDistrictAssessmentPropertyRow)this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[rowIndex];
        }

        //// check assign your controls with values from the typeddataset row

        /// <summary>
        /// Sets the control values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetControlValues(F1031SpecialDistrictAssessmentData.ListDistrictAssessmentPropertyRow selectedRow)
        {
            //// Fill Property Info
            string stmtstatuscolor = string.Empty;
            string greencolor;
            string redcolor;
            string bluecolor;
            int red;
            int blue;
            int leng;
            int RColor;
            int GColor;
            int BColor;
            if (!selectedRow.IsStatementIDNull())
            {
                this.currentStatementId = selectedRow.StatementID;
            }
            else
            {
                this.currentStatementId = 0;
 
            }
            this.keyId = selectedRow.WorkingFileID;  
            this.ParcelNumberTextBox.Text = selectedRow.ParcelNumber;
            this.parcelId = selectedRow.ParcelID;
            ////Commented & Modified by Biju on 26/Jul/2010 to fix #6497
            this.ispaid = false; ////selectedRow.IsPaid;
            this.TypeTextBox.Text = selectedRow.PostName;
            this.TypeTextBox.Tag = selectedRow.PostTypeID.ToString();
            if (Convert.ToInt32(this.TypeTextBox.Tag.ToString()) == 14)
            {
                this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031CustomerNumber") + ":";
            }
            else
            {
                this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031ParcelNumber") + ":";
            }
            this.IrrigableAcresTextBox.TextCustomFormat = "";
            this.IrrigableAcresTextBox.Text = selectedRow.IrrgAcres.ToString();
            this.Customformat(this.IrrigableAcresTextBox);
            this.IrrigableAcresTextBox.Text = selectedRow.IrrgAcres.ToString();
            this.RPAcresCountTextBox.TextCustomFormat = "";
            this.RPAcresCountTextBox.Text = selectedRow.Acres.ToString();
            this.Customformat(this.RPAcresCountTextBox);
            this.RPAcresCountTextBox.Text = selectedRow.Acres.ToString();
            this.TurnoutTextBox.Text = selectedRow.Turnouts.ToString();
            this.Address1TextBox.Text = selectedRow.Address1 + Environment.NewLine + selectedRow.Address2 + Environment.NewLine + selectedRow.City;
            this.ownerId  = selectedRow.OwnerID;
            this.OwnerNameLinkLabel.Text = selectedRow.Owner_Name;
            this.MinDistFeeTextBox.Text = selectedRow.MinimumDistrictFee.ToString();
            this.RollYearTextBox.Text = selectedRow.RollYear.ToString();
            this.SpecialDistrictLinkLabel.Tag = selectedRow.SADistrictID.ToString();
            this.SpecialDistrictLinkLabel.Text = selectedRow.DistrictName;
            this.StatementNumberLinkLabel.Text = selectedRow.StatementNumber;
            this.LoanTextBox.Text = selectedRow.LoanNumber;
            this.MortgageCoTextBox.Tag = selectedRow.MortgageID.ToString();
            this.MortgageCoTextBox.Text = selectedRow.MortgageName;
            //for StatementTaxText
            if (!selectedRow.IsTaxAmountNull())
            {
                this.StatementTaxTextBox.Text = selectedRow.TaxAmount.ToString();
            }
            if (!selectedRow.IsSitusNull())
            {
                this.SitusTextBox.Text = selectedRow.Situs;
            }

            this.MapTextBox.Text = selectedRow.MapNumber;

            if (!selectedRow.IsLegalNull())
            {
                this.LegalTextBox.Text = selectedRow.Legal;
            }

            ////TSCO - 16031 Special District Assessment - Change Statement Number hyperlink
            ////retireve the recieptForm number for statement hyperlink click to open the form.
            int.TryParse(selectedRow.ReceiptForm, out this.destinationStatementLinkFormNumber);

            //// Coding Added for the CO 2181[Statment Status.Statement Color column is added]by Malliga
            if (!string.IsNullOrEmpty(selectedRow.StatementStatusText))
            {
                this.StatementStatusTextBox.Text = selectedRow.StatementStatusText;
            }
            else
            {
                this.StatementStatusTextBox.Text = string.Empty;
            }
            ////set for StatmentStatusColor
            if (!string.IsNullOrEmpty(selectedRow.StatementStatusText))
            {
                stmtstatuscolor = selectedRow.StatementStatusColor;
                ////To get full length of the statementstauscolor
                leng = stmtstatuscolor.Length;
                ////To get index of the comma
                red = stmtstatuscolor.IndexOf(',');
                blue = stmtstatuscolor.LastIndexOf(',');
                ////To Get Redcolor from Statementstatuscolor
                redcolor = stmtstatuscolor.Substring(0, red);
                int.TryParse(redcolor, out RColor);
                ////To Get Greencolor from Statementstatuscolor
                greencolor = stmtstatuscolor.Substring((red + 1), (blue - (red + 1)));
                int.TryParse(greencolor, out GColor);
                ////To Get Bluecolor from Statementstatuscolor
                bluecolor = stmtstatuscolor.Substring((blue + 1), (leng - (blue + 1)));
                int.TryParse(bluecolor, out BColor);
                ////Assign RGB value to StatementStatusTextBox Forecolor
                this.StatementStatusTextBox.ForeColor = Color.FromArgb(RColor, GColor, BColor);
            }
            ////Coding Ends here
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// Customizes the rates listing grid view.
        /// </summary>
        private void CustomizeRatesListingGridView()
        {
            this.pageLoadStatus = true;
            this.RatesListingGridView.AutoGenerateColumns = false;
            this.RateDescription.HeaderText = "Rate Description";
            this.RateType.HeaderText = "Rate Type";
            this.Minimum.HeaderText = "Minimum";
            this.Amount.HeaderText = "Amount";
            this.Acres.HeaderText = "Acres";
            this.Total.HeaderText = "Total / Item";

            this.RateDescription.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.RateDescriptionColumn.ColumnName;
            this.RateType.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.RateTypeColumn.ColumnName;
            this.Minimum.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.MinimumColumn.ColumnName;
            this.Amount.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.AmountColumn.ColumnName;
            this.Acres.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.AcresColumn.ColumnName;
            this.Total.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.TotalColumn.ColumnName;
            this.RateAcresID.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.RateAcresIDColumn.ColumnName;
            this.SARateItemID.DataPropertyName = this.propertyInfoDataSet.ListDistrictAssessmentRates.SARateItemIDColumn.ColumnName;

            this.RatesListingGridView.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates.DefaultView;

            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Handles the CellFormatting event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if ((e.ColumnIndex == this.RatesListingGridView.Columns[this.propertyInfoDataSet.ListDistrictAssessmentRates.AmountColumn.ColumnName.ToString()].Index) || (e.ColumnIndex == this.RatesListingGridView.Columns[this.propertyInfoDataSet.ListDistrictAssessmentRates.AcresColumn.ColumnName.ToString()].Index) || (e.ColumnIndex == this.RatesListingGridView.Columns[this.propertyInfoDataSet.ListDistrictAssessmentRates.TotalColumn.ColumnName.ToString()].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && (!String.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells[this.propertyInfoDataSet.ListDistrictAssessmentRates.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()) || !String.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells[this.propertyInfoDataSet.ListDistrictAssessmentRates.AcresColumn.ColumnName.ToString()].Value.ToString().Trim()) || !String.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells[this.propertyInfoDataSet.ListDistrictAssessmentRates.TotalColumn.ColumnName.ToString()].Value.ToString().Trim())))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (e.ColumnIndex == 3)
                            {
                                if (outDecimal.ToString().Contains("-"))
                                {
                                    e.Value = "0.00";
                                }
                                else
                                {
                                    e.Value = outDecimal.ToString("#,##0.00##");
                                    e.FormattingApplied = true;
                                }
                            }
                            else if(e.ColumnIndex == 5)
                            {
                                if (outDecimal.ToString().Contains("-"))
                                {
                                    e.Value = "0.00";
                                }
                                else
                                {
                                    e.Value = outDecimal.ToString("#,##0.00##");
                                    e.FormattingApplied = true;
                                }
                            }
                            else
                            {

                                if (outDecimal.ToString().Contains("-"))
                                {
                                    e.Value = "0.00";
                                }
                                else
                                {
                                    e.Value = outDecimal.ToString("#,##0.00");
                                    e.FormattingApplied = true;
                                }
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
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            ////Only paint if desired column
            try
            {
                if (e.ColumnIndex == this.RatesListingGridView.Columns["Acres"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    this.ratesGridEdited = true;
                    decimal outDecimal = 0;
                    // Only paint if text provided, Only paint if desired text is in cell
                    if (!string.IsNullOrEmpty(e.Value.ToString().Trim()))
                    {
                        string tempvalue = e.Value.ToString().Trim();


                        if (decimal.TryParse(tempvalue, out outDecimal))
                        {
                            if (outDecimal > Convert.ToDecimal(999999.99))
                            {
                                outDecimal = 0;
                                MessageBox.Show(SharedFunctions.GetResourceString("1031AcresValidationError"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.SetTotal(e, 0, 0);
                                e.Value = outDecimal;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString();
                                //// If zero validate
                                decimal acresValue = 0;
                                decimal tempAcres, tempAmount;
                                decimal.TryParse(e.Value.ToString(), out tempAcres);
                                decimal.TryParse(this.RatesListingGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString(), out tempAmount);
                                this.RatesListingGridView.RefreshEdit();
                                decimal.TryParse(e.Value.ToString(), out acresValue);
                                if (this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().ToUpper().Equals("RATE") && acresValue < 1 && this.RatesListingGridView.Rows[e.RowIndex].Cells["Minimum"].Value.ToString().Equals("Yes"))
                                {
                                    this.SetTotal(e, 1, tempAmount);
                                }
                                else
                                {
                                    this.SetTotal(e, tempAcres, tempAmount);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("1031InvalidAcresValue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            outDecimal = 0;
                            this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = false;
                            this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows[e.RowIndex]["Total"] = 0;
                            this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = true;
                            e.Value = outDecimal;
                            e.ParsingApplied = true;
                        }

                        e.ParsingApplied = true;
                        this.RatesListingGridView.RefreshEdit();
                    }
                    else if (!this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().ToUpper().Equals("FEE"))
                    {
                        ////Commented by Biju on 28/Jan/2010 to fix the bug while implementing #5598
                        ////MessageBox.Show(SharedFunctions.GetResourceString("1031EmptyAcresValue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ////Added by Biju on 28/Jan/2010 to fix the bug while implementing #5598
                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                        this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = false;
                        this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows[e.RowIndex]["Total"] = 0;
                        this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = true;
                        this.RatesListingGridView.RefreshEdit();
                        decimal acresValue = 0;
                        decimal tempAcres, tempAmount;
                        decimal.TryParse(e.Value.ToString(), out tempAcres);
                        decimal.TryParse(this.RatesListingGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString(), out tempAmount);
                        this.RatesListingGridView.RefreshEdit();
                        decimal.TryParse(e.Value.ToString(), out acresValue);
                        if (this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().ToUpper().Equals("RATE") && acresValue < 1 && this.RatesListingGridView.Rows[e.RowIndex].Cells["Minimum"].Value.ToString().Equals("Yes"))
                        {
                            this.SetTotal(e, 1, tempAmount);
                        }
                        else
                        {
                            this.SetTotal(e, tempAcres, tempAmount);
                        }
                        ////till here
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the total.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        /// <param name="tempAcres">The temp acres.</param>
        /// <param name="tempAmount">The temp amount.</param>
        private void SetTotal(DataGridViewCellParsingEventArgs e, decimal tempAcres, decimal tempAmount)
        {
            try
            {
                if (this.RatesListingGridView.Rows[e.RowIndex].Cells["Minimum"].Value.ToString().ToUpper().Equals("TRUE") && tempAcres < 1)
                {
                    tempAcres = 1;
                }

                if (this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().ToUpper().Equals("FEE"))
                {
                    this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = false;
                    this.RatesListingGridView.Rows[e.RowIndex].Cells["Total"].Value = tempAmount;
                    this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = true;
                }
                else
                {
                    this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = false;
                    decimal tempValue;
                    tempValue = tempAmount * tempAcres;
                    if (tempValue.ToString().Contains("."))
                    {
                      string temp1= tempValue.ToString();
                      
                      string[]array=temp1.Split('.');
                      if (array[1].Length > 0)
                      {
                         string decimalValue= array[1].Substring(0, 2);
                         int finalValue = Convert.ToInt32(decimalValue)%2;
                         if (finalValue == 1)
                         {
                             decimal value = Convert.ToDecimal(decimalValue);
                             value = value + 1;
                             temp1 = array[0] + "." + value;
                             tempValue = Convert.ToDecimal(temp1);
                         }
                         else
                         {
                             decimal value = Convert.ToDecimal(decimalValue);
                             temp1 = array[0] + "." + value;
                         }
                         
                         tempValue = Convert.ToDecimal(temp1);
                      }
                    }
                    this.RatesListingGridView.Rows[e.RowIndex].Cells["Total"].Value = tempValue; 
                    this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = true;
                }

                this.DisplayTotal();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               if (!this.pageLoadStatus)
                {
                    this.SetEditRecord();
                    if (string.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells["Acres"].Value.ToString()) && this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().Equals("RATE"))
                    {
                        this.acresEmpty = true;
                    }
                    else
                    {
                        this.acresEmpty = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the IrrigableAcresTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void IrrigableAcresTextBox_Leave(object sender, EventArgs e)
        {
            ////if (!string.IsNullOrEmpty(this.IrrigableAcresTextBox.Text.Trim()))
            ////{
            ////    byte maxStructValue = byte.MaxValue;
            ////    int tempIrrgAcres;
            ////    int.TryParse(this.IrrigableAcresTextBox.Text.Trim(), out tempIrrgAcres);

            ////    if ((Convert.ToInt32(tempIrrgAcres) > maxStructValue))
            ////    {
            ////        this.IrrigableAcresTextBox.Text = "0";
            ////        MessageBox.Show(SharedFunctions.GetResourceString("Irrigable Acres value should not exceed 255."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////        this.IrrigableAcresTextBox.Focus();
            ////    }
            ////}
            this.Customformat(IrrigableAcresTextBox);
        }

        /// <summary>
        /// Handles the Leave event of the RPAcresCountTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RPAcresCountTextBox_Leave(object sender, EventArgs e)
        {
            ////if (!string.IsNullOrEmpty(this.RPAcresCountTextBox.Text.Trim()))
            ////{
            ////    byte maxStructValue = byte.MaxValue;
            ////    int tempAcres;
            ////    int.TryParse(this.RPAcresCountTextBox.Text.Trim(), out tempAcres);

            ////    if ((Convert.ToInt32(tempAcres) > maxStructValue))
            ////    {
            ////        this.RPAcresCountTextBox.Text = "0";
            ////        MessageBox.Show(SharedFunctions.GetResourceString("RPAcresCount value should not exceed 255."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////        this.RPAcresCountTextBox.Focus();
            ////    }
            ////}
            this.Customformat(RPAcresCountTextBox);
        }
        #endregion

        #region Common Slice Methods

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
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.ispaid.Equals(false) && this.PermissionEdit && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.RatesListingGridView.AllowSorting = false;
                        this.RatesListingGridView.TabStop = false;
                        this.writeButton.Enabled = false;
                        this.cancelButton.Enabled = false; 
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
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

            decimal assessmentTotal;
            decimal maxAssessmentLimit = 922337203685477.5807M;
            decimal.TryParse(this.TotalAssessmentValueTextBox.Text.ToString(), out assessmentTotal);

            if (string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()))
            {
                this.ParcelNumButton.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            if (string.IsNullOrEmpty(this.OwnerNameLinkLabel.Text.Trim()))
            {
                this.ParcelNumButton.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (Convert.ToInt32(this.SpecialDistrictLinkLabel.Tag.ToString()) <= 0)
            {
                this.SpecialDistrictButton.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (this.acresEmpty)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            ////Commented by Biju on 21/Jan/2010 to implement #5598
            ////else if (this.CheckParceDistrictTypeAndRollYear())
            ////{
            ////    sliceValidationFields.RequiredFieldMissing = true;
            ////    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            ////}
            ////till here
            else if (this.CheckTotalItemValidation())
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("1031TotalValidationError");
            }
            else if (assessmentTotal > maxAssessmentLimit)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("1031TotalValidationError");
            }
            else if (this.CheckDuplicateStatementAndOwner())
            {
                this.ParcelNumButton.Focus();
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
            }
            ////khaja commented to fix Bug#6013        
            ////else if (string.IsNullOrEmpty(this.IrrigableAcresTextBox.Text.Trim()) || this.IrrigableAcresTextBox.Text == "0")
            ////{
            ////    sliceValidationFields.RequiredFieldMissing = true;
            ////    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            ////}
            ////else if (string.IsNullOrEmpty(this.RPAcresCountTextBox.Text.Trim()) || this.RPAcresCountTextBox.Text == "0")
            ////{
            ////    sliceValidationFields.RequiredFieldMissing = true;
            ////    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            ////}          

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the save validation.
        /// </summary>
        /// <returns>it returns save validation string</returns>
        private string CheckSaveValidation()
        {
            string validationErrors = string.Empty;
            ////byte maxStructValue = byte.MaxValue;
            ////int tempIrrgAcres, tempAcres;
            ////int.TryParse(this.IrrigableAcresTextBox.Text.Trim(), out tempIrrgAcres);
            ////int.TryParse(this.RPAcresCountTextBox.Text.Trim(), out tempAcres);

            ////if (!string.IsNullOrEmpty(this.IrrigableAcresTextBox.Text.Trim()))
            ////{
            ////    if ((Convert.ToInt32(tempIrrgAcres) > maxStructValue))
            ////    {
            ////        validationErrors = validationErrors + SharedFunctions.GetResourceString("1031IrrigableAcresMaxvalue"); ////"Irrigable Acres value should not exceed 255. \n";
            ////        this.IrrigableAcresTextBox.Text = "0";
            ////    }
            ////}

            ////if (!string.IsNullOrEmpty(this.RPAcresCountTextBox.Text.Trim()))
            ////{
            ////    if (Convert.ToInt32(tempAcres) > maxStructValue)
            ////    {
            ////        validationErrors = validationErrors + SharedFunctions.GetResourceString("1031RPAcresCountMaxvalue"); //// "RPAcresCount value should not exceed 255.";
            ////        this.RPAcresCountTextBox.Text = "0";
            ////    }
            ////}

            return validationErrors;
        }

        /// <summary>
        /// Checks the parce district type and roll year.
        /// </summary>
        /// <returns>boolean value</returns>
        private bool CheckParceDistrictTypeAndRollYear()
        {
            if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > 0)
            {
                DataRow[] filterRows = this.propertyInfoDataSet.ListDistrictAssessmentRates.Select(" RateType = 'Rate' And Acres = 0 ");
                if (filterRows.Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks the total item validation.
        /// </summary>
        /// <returns>Returns boolean value</returns>
        private bool CheckTotalItemValidation()
        {
            if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > 0)
            {
                DataRow[] filterRows = this.propertyInfoDataSet.ListDistrictAssessmentRates.Select(" Total > 999999999999.9999 ");
                if (filterRows.Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks the duplicate district.
        /// </summary>
        /// <returns>duplicate record status</returns>
        private bool CheckDuplicateStatementAndOwner()
        {
            int statementDuplicateStatus = -1;
            int ownerDuplicateStatus = -1;
            this.districtPropertyDetailsXml = this.GetDistrictPropertyInfoXml();
            F1031SpecialDistrictAssessmentData outputValue = new F1031SpecialDistrictAssessmentData(); 
            outputValue= this.form16031Controll.WorkItem.F16031_CheckSpecialAssessment(this.districtPropertyDetailsXml);
            if (outputValue.Tables["ListCheckOutPutValue"].Rows[0]["IsPass"].ToString().Equals("False"))
            {
                MessageBox.Show(outputValue.Tables["ListCheckOutPutValue"].Rows[0]["ErrorMessage"].ToString(), "TerraScan – Cannot Save Assessment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false; 
            }
           
/*            statementDuplicateStatus = this.form16031Controll.WorkItem.F16031_CheckSpecialAssessment(this.districtPropertyDetailsXml);
            if (statementDuplicateStatus.Equals(-2))
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("1031DuplicateParcelAssesssmentPartI") + this.TypeTextBox.Text + SharedFunctions.GetResourceString("1031DuplicateParcelAssesssmentPartII"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("1031DuplicateParcelAssesssmentTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ownerDuplicateStatus = this.form16031Controll.WorkItem.F16031_CheckSpecialAssessment(this.districtPropertyDetailsXml);
                    //Code removed for the Co #10173
                    /* if (ownerDuplicateStatus.Equals(-3))
                    {
                         if (MessageBox.Show(SharedFunctions.GetResourceString("1031AssociateOwnerToStatement"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("CopyOwnersFromParcel"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                         {
                             this.ownerFlag = false;
                         }
                         else
                         {
                             this.ownerFlag = true;
                         }
                     }

                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (statementDuplicateStatus.Equals(-1))
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("F16031ParcelError"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    return true;
                }
            }
            else
            {
            //    ownerDuplicateStatus = this.form16031Controll.WorkItem.F16031_CheckSpecialAssessment(this.districtPropertyDetailsXml);
                //Code removed for the Co #10173
                /*if (ownerDuplicateStatus.Equals(-3))
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("1031AssociateOwnerToStatement"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("CopyOwnersFromParcel"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.ownerFlag = false;
                    }
                    else
                    {
                        this.ownerFlag = true;
                    }
                }

                return false;
            }
    

            return false;*/
        }

        #endregion

        #region Form Private Methods

        /// <summary>
        /// Displays the total.
        /// </summary>
        private void DisplayTotal()
        {
            decimal minDistFee;
            decimal.TryParse(this.propertyInfoDataSet.ListDistrictAssessmentRates.Compute("SUM(Total)", "").ToString(), out this.amountValue);
            decimal.TryParse(this.MinDistFeeTextBox.Text.Replace("$", "").Trim(), out minDistFee);
            if (this.amountValue > minDistFee)
            {
                this.TotalAssessmentValueTextBox.Text = this.amountValue.ToString();
                //  for statement Tax
                //this.StatementTaxTextBox.Text = this.amountValue.ToString();   
            }
            else
            {
                this.TotalAssessmentValueTextBox.Text = this.MinDistFeeTextBox.Text;
            }
        }

        /// <summary>
        /// Sets the state of the default.
        /// </summary>
        private void SetDefaultState()
        {
            this.ClearDistrictAssessmentDetails();
            this.NullRecords = true;
            this.EnableFormControls(false);
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
        }

        /// <summary>
        /// Clears the district assessment details.
        /// </summary>
        private void ClearDistrictAssessmentDetails()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.parcelId = 0;
            this.ownerId  = 0;
            this.TypeTextBox.Text = string.Empty;
            this.TypeTextBox.Tag = string.Empty;
            this.IrrigableAcresTextBox.Text = string.Empty;
            this.RPAcresCountTextBox.Text = string.Empty;
            this.TurnoutTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.OwnerNameLinkLabel.Text = string.Empty;
            this.MinDistFeeTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.SpecialDistrictLinkLabel.Text = string.Empty;
            this.StatementNumberLinkLabel.Text = string.Empty;
            this.LoanTextBox.Text = string.Empty;
            this.MortgageCoTextBox.Text = string.Empty;
            this.SitusTextBox.Text = string.Empty;
            this.StatementTaxTextBox.Text = string.Empty;    
            this.MapTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.TotalAssessmentValueTextBox.Text = string.Empty;
            this.StatementStatusTextBox.Text = string.Empty;
            this.propertyInfoDataSet.ListDistrictAssessmentRates.Clear();
            this.RatesListingGridView.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates.DefaultView;
            this.StatementTaxTextBox.Text = string.Empty;  
        }

        /// <summary>
        /// Enables the form controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void EnableFormControls(bool enableValue)
        {
            this.PropertyInfoPanel.Enabled = enableValue;
            this.GridPanel.Enabled = enableValue;
        }

        /// <summary>
        /// Locks the text box controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockTextBoxControls(bool lockControl)
        {
            // this.ParcelNumberTextBox.LockKeyPress = lockControl;
            this.IrrigableAcresTextBox.LockKeyPress = lockControl;
            this.RPAcresCountTextBox.LockKeyPress = lockControl;
            this.TurnoutTextBox.LockKeyPress = lockControl;
            this.SitusTextBox.LockKeyPress = lockControl;
            this.MapTextBox.LockKeyPress = lockControl;
            this.LegalTextBox.LockKeyPress = lockControl;
            this.StatementTaxTextBox.LockKeyPress = true;  
            this.RatesListingGridView.Columns[4].ReadOnly = lockControl;
        }

        /// <summary>
        /// Enables the button controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableButtonControls(bool enable)
        {
            this.ParcelNumButton.Enabled = enable;
            this.OwnerNameButton.Enabled = enable;
        }
        ///<summary>
        /// Gets the Parcel Details using Parcel Number
        ///</summary> 
        ///<param name="parcelNumber">The parcel Number.</param>
        private void GetParcel(String parcelNumber)
        {
            //int parcelID=0; 
            int Year;
            int.TryParse(this.RollYearTextBox.Text, out Year);
            F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable parcelDetailsDataTable = new F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable();
            parcelDetailsDataTable = this.form16031Controll.WorkItem.F16031_GetDistrictAssessmentParcelId(parcelNumber, null, Year).GetDistrictAssessmentParcelID;
            if (parcelDetailsDataTable.Rows.Count > 0)
            {
                this.ParcelNumberTextBox.Text = this.ParcelNumberTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelNumberColumn.ColumnName].ToString();
                this.parcelId = Convert.ToInt32(parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelIDColumn.ColumnName].ToString());
                if (!string.IsNullOrEmpty(this.TypeTextBox.Tag.ToString()))
                {
                    if (Convert.ToInt32(this.TypeTextBox.Tag.ToString()) == 14)
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031CustomerNumber") + ":";
                        this.SpecialDistrictButton.Enabled = false;
                    }
                    else
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031ParcelNumber") + ":";
                        this.SpecialDistrictButton.Enabled = true;
                    }
                }
                this.CurrentOwnerID = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.OwnerIDColumn.ColumnName].ToString();
                int.TryParse(this.CurrentOwnerID, out this.ownerId);   
                this.OwnerNameLinkLabel.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.Owner_NameColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelRollYearColumn.ColumnName].ToString();
                this.LoanTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LoanNumberColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Tag = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageIDColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageNameColumn.ColumnName].ToString();
                this.SitusTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.SitusColumn.ColumnName].ToString();
                this.MapTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MapNumberColumn.ColumnName].ToString();
                this.LegalTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LegalNotesColumn.ColumnName].ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(this.TypeTextBox.Tag.ToString()))
                {
                    if (Convert.ToInt32(this.TypeTextBox.Tag.ToString()) == 14)
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031CustomerNumber") + ":";
                        this.SpecialDistrictButton.Enabled = false;
                    }
                    else
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031ParcelNumber") + ":";
                        this.SpecialDistrictButton.Enabled = true;
                    }
                }
                this.RollYearTextBox.Text = "";
                this.OwnerNameLinkLabel.Text = "";
                this.LoanTextBox.Text = "";
                this.MortgageCoTextBox.Text = "";
                this.SitusTextBox.Text = "";
                this.MapTextBox.Text = "";
                this.LegalTextBox.Text = "";
            }

        }


        /// <summary>
        /// Gets the parcel details.
        /// </summary>
        /// <param name="parcelIdentity">The parcel identity.</param>
        /// <param name="rollyear""
        private void GetParcelDetails(int parcelIdentity, int rollyear)
        {

            int year;
            int.TryParse(this.RollYearTextBox.Text, out year); 
            F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable parcelDetailsDataTable = new F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable();
            parcelDetailsDataTable = this.form16031Controll.WorkItem.F16031_GetDistrictAssessmentParcelId(string.Empty, parcelIdentity, rollyear).GetDistrictAssessmentParcelID;

            if (parcelDetailsDataTable.Rows.Count > 0)
            {
                //// Fill Property Info
                this.ParcelNumberTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelNumberColumn.ColumnName].ToString();
                ////this.TypeTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.PostNameColumn.ColumnName].ToString();
                ////this.TypeTextBox.Tag = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.PostTypeIDColumn.ColumnName].ToString();
                this.parcelId = Convert.ToInt32(parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelIDColumn.ColumnName].ToString());
                if (!string.IsNullOrEmpty(this.TypeTextBox.Tag.ToString()))
                {
                    if (Convert.ToInt32(this.TypeTextBox.Tag.ToString()) == 14)
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031CustomerNumber") + ":";
                        this.SpecialDistrictButton.Enabled = false;
                    }
                    else
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031ParcelNumber") + ":";
                        this.SpecialDistrictButton.Enabled = true;
                    }
                }
                this.CurrentOwnerID = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.OwnerIDColumn.ColumnName].ToString();
                int.TryParse(this.CurrentOwnerID, out this.ownerId);   
                this.OwnerNameLinkLabel.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.Owner_NameColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelRollYearColumn.ColumnName].ToString();
                this.LoanTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LoanNumberColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Tag = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageIDColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageNameColumn.ColumnName].ToString();
                this.SitusTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.SitusColumn.ColumnName].ToString();
                this.MapTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MapNumberColumn.ColumnName].ToString();
                this.LegalTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LegalNotesColumn.ColumnName].ToString();
            }
        }
        /// <summary>
        /// Gets the parcel details.
        /// </summary>
        /// <param name="parcelIdentity">The parcel identity.</param>
        /// <param name="rollyear""
        private void GetCustomDetails(int parcelIdentity)
        {

            int year;
            int.TryParse(this.RollYearTextBox.Text, out year);
            F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable parcelDetailsDataTable = new F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable();
            parcelDetailsDataTable = this.form16031Controll.WorkItem.F16031_GetDistrictAssessmentParcelId(string.Empty, parcelIdentity, year).GetDistrictAssessmentParcelID;

            if (parcelDetailsDataTable.Rows.Count > 0)
            {
                //// Fill Property Info
                this.ParcelNumberTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelNumberColumn.ColumnName].ToString();
                ////this.TypeTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.PostNameColumn.ColumnName].ToString();
                ////this.TypeTextBox.Tag = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.PostTypeIDColumn.ColumnName].ToString();
                this.parcelId = Convert.ToInt32(parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelIDColumn.ColumnName].ToString());
                if (!string.IsNullOrEmpty(this.TypeTextBox.Tag.ToString()))
                {
                    if (Convert.ToInt32(this.TypeTextBox.Tag.ToString()) == 14)
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031CustomerNumber") + ":";
                        this.SpecialDistrictButton.Enabled = false;
                    }
                    else
                    {
                        this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031ParcelNumber") + ":";
                        this.SpecialDistrictButton.Enabled = true;
                    }
                }

                this.CurrentOwnerID = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.OwnerIDColumn.ColumnName].ToString();
                int.TryParse(this.CurrentOwnerID, out this.ownerId);   
                this.OwnerNameLinkLabel.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.Owner_NameColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelRollYearColumn.ColumnName].ToString();
                this.LoanTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LoanNumberColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Tag = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageIDColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageNameColumn.ColumnName].ToString();
                this.SitusTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.SitusColumn.ColumnName].ToString();
                this.MapTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MapNumberColumn.ColumnName].ToString();
                this.LegalTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LegalNotesColumn.ColumnName].ToString();
            }
        }


        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form16031Controll.WorkItem.GetConfigDetails("TR_RollYear");
            this.RollYearTextBox.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the LinkClicked event of the ParcelNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()) && this.parcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Parcel Detail Form - FormID - 11006
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(11006);
                    formInfo.optionalParameters = new object[] { this.parcelId };
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
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
        /// Handles the LinkClicked event of the OwnerNameLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void OwnerNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (!string.IsNullOrEmpty(this.OwnerNameLinkLabel.Text) && this.ownerId  > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Master Name Address Form - FormID - 91000
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(91000);
                    formInfo.optionalParameters = new object[] { this.ownerId };
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
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
        /// Handles the Click event of the OwnerNameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////check for valid value
                /// 
                //if (this.currentStatementId > 0)
                //{
                this.Cursor = Cursors.WaitCursor;
                string currentOwnerId = string.Empty;
                ////Statement Owner Management Form - FormID - 9110
                //FormInfo formInfo = TerraScanCommon.GetFormInfo(9100);
                //formInfo.optionalParameters = new object[] {  };
                Form parcelF9101 = new Form();
                parcelF9101 = this.form16031Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form16031Controll.WorkItem);

                if (parcelF9101 != null)
                {
                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                    {
                        ////to avoid the Parties Type Combo Selection Changed Event
                        //                        this.avoidPartiesTypeComboSelectionChangedEvent = true;

                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"));
                        this.ownerDetailDataSet = this.form16031Controll.WorkItem.F15010_GetOwnerDetails(this.ownerId);
                        this.OwnerNameLinkLabel.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                        //this.OwnerNameLinkLabel.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                        this.CurrentOwnerID = this.ownerId.ToString(); 
                        //Form formInfo = new Form();
                        //formInfo = TerraScanCommon.GetForm(9110, null, this.form16031Controll.WorkItem);

                        //if (formInfo != null)
                        //{
                        //    if (formInfo.ShowDialog() == DialogResult.Yes)
                        //    {
                        //        //this.checkreadonlyflag = false;
                        //        currentOwnerId = TerraScanCommon.GetValue(formInfo, "CommandResult");
                        //        DataSet currentownerDataTable = new DataSet();
                        //        currentownerDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentOwnerId));


                        //    }
                        //    //this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        //}
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
                //}
               
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
        /// Handles the LinkClicked event of the SpecialDistrictLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(10030);

                if (!string.IsNullOrEmpty(this.SpecialDistrictLinkLabel.Tag.ToString()))
                {
                    formInfo.optionalParameters = new object[] { this.SpecialDistrictLinkLabel.Tag.ToString() };
                }

                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SpecialDistrictButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form specialDistrictSelectionForm;
                //// Modified to Implement CO#6032 on 03 Arp 2009 by Shanmuga Sundaram.A 
                object[] optionalParameters = new object[] { this.RollYearTextBox.Text };
                specialDistrictSelectionForm = this.form16031Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1033, optionalParameters, this.form16031Controll.WorkItem);

                if (specialDistrictSelectionForm != null && specialDistrictSelectionForm.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(TerraScanCommon.GetValue(specialDistrictSelectionForm, "SpecialDistrictId").ToString(), out this.sadistrictId);
                    if (Convert.ToInt32(this.SpecialDistrictLinkLabel.Tag.ToString()) != this.sadistrictId)
                    {
                        if (this.ratesGridEdited.Equals(true) && this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("1031existingAcresValue"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("1031AcresValues"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.LoadDefaultDistrictValues();
                            }
                        }
                        else
                        {
                            this.LoadDefaultDistrictValues();
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
        /// Loads the default district values.
        /// </summary>
        private void LoadDefaultDistrictValues()
        {
            this.ratesGridEdited = false;
            this.propertyInfoDataSet.ListDistrictAssessmentRates.Clear();
            this.propertyInfoDataSet = this.form16031Controll.WorkItem.F16031_ListDistrictAssessment(this.sadistrictId);

            if (this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows.Count > 0)
            {
                this.SpecialDistrictLinkLabel.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.DistrictNameColumn].ToString();
                this.SpecialDistrictLinkLabel.Tag = this.sadistrictId;
                this.TypeTextBox.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.PostNameColumn].ToString();
                this.TypeTextBox.Tag = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.TypeColumn].ToString();
                this.MinDistFeeTextBox.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.MinimumDistrictFeeColumn].ToString();
                this.RollYearTextBox.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.DistrictRollYearColumn].ToString();
                this.RatesListingGridView.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates.DefaultView;
                ////khaja added code to fix Bug#6031
                this.DisplayTotal();
            }
            else
            {
                this.SpecialDistrictLinkLabel.Text = string.Empty;
                this.SpecialDistrictLinkLabel.Tag = string.Empty;
                this.TypeTextBox.Text = string.Empty;
                this.MinDistFeeTextBox.Text = string.Empty;
                this.RollYearTextBox.Text = string.Empty;
            }

            ////VscrollBar is enabled or disabled based on NumRowsVisible in GridView
            if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > this.RatesListingGridView.NumRowsVisible)
            {
                this.RatesListingGridVscrollBar.Visible = false;
            }
            else
            {
                this.RatesListingGridVscrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelNumButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNumButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int postTypeId;
                int rollYear;
                this.IsbuttonClick = true;
                int.TryParse(this.TypeTextBox.Tag.ToString().Trim(), out postTypeId);
                int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
                switch (postTypeId)
                {
                    case 14:
                        {
                            ////Parcel Detail Form - FormID - 1402
                            object[] optionalParameter = new object[] { rollYear };
                            Form customerSelectionForm = this.form16031Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1402, optionalParameter, this.form16031Controll.WorkItem);
                            ////open form in view mode - possible to edit
                            if (customerSelectionForm != null)
                            {
                                if (customerSelectionForm.ShowDialog().Equals(DialogResult.OK))
                                {
                                    this.parcelId = Convert.ToInt32(TerraScanCommon.GetValue(customerSelectionForm, "CustomerID"));
                                    //rollYear = Convert.ToInt32(TerraScanCommon.GetValue(customerSelectionForm, "RollYearId")); 
                                    ////Added by Biju on 26/Jul/2010 to fix #6497
                                    this.LegalTextBox.Text = "";
                                    this.SitusTextBox.Text = "";
                                    ////till here
                                    this.GetCustomDetails(this.parcelId);
                                }
                            }

                            break;
                        }

                    default:
                        {
                            ////Parcel Detail Form - FormID - 1401
                            object[] optionalParameter = new object[] { rollYear };
                            Form parcelSelectionForm = this.form16031Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, optionalParameter, this.form16031Controll.WorkItem);
                            ////open form in view mode - possible to edit
                            if (parcelSelectionForm != null)
                            {
                                if (parcelSelectionForm.ShowDialog().Equals(DialogResult.OK))
                                {
                                    this.parcelId = Convert.ToInt32(TerraScanCommon.GetValue(parcelSelectionForm, "ParcelID"));
                                    rollYear = Convert.ToInt32(TerraScanCommon.GetValue(parcelSelectionForm, "RollYearId"));    
                                    ////Added by Biju on 26/Jul/2010 to fix #6497
                                    this.LegalTextBox.Text = "";
                                    this.SitusTextBox.Text = "";
                                    ////till here
                                    this.GetParcelDetails(this.parcelId, rollYear);
                                }
                            }

                            break;
                        }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the StatementNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void StatementNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.currentStatementId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ////TSCO - 16031 Special District Assessment - Change Statement Number hyperlink
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(this.destinationStatementLinkFormNumber);
                    formInfo.optionalParameters = new object[] { this.currentStatementId };
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
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
        /// Saves the excise rate record.
        /// </summary>
        /// <returns>returns boolean value</returns>
        private bool SaveExciseRateRecord()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string ratesListingDetails = string.Empty;
                int returnValue = -1;

                if (this.RatesListingGridView.DataSource != null)
                {
                    DataTable dt = ((DataView)this.RatesListingGridView.DataSource).Table;
                    ratesListingDetails = Utility.GetXmlString(dt);
                }

                this.acresEmpty = false;
                ///used for enter the OwnerDetails
                this.ownerFlag = true;
                returnValue = this.form16031Controll.WorkItem.F16031_SaveDistrictAssessmentDetails(this.districtPropertyDetailsXml, ratesListingDetails, TerraScanCommon.UserId);

                if (returnValue != -1)
                {
                    this.districtPropertyDetailsXml = string.Empty;
                    this.ownerFlag = false;
                    ////Commented by Biju on 26/Jul/2010 to fix #6497
                    ////this.SpecialDistrictButton.Enabled = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.RatesListingGridView.AllowSorting = true;
                    this.RatesListingGridView.TabStop = true;

                    this.currentStatementId = returnValue;
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

                ////return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Gets the district property info XML.
        /// </summary>
        /// <returns>DistrictPropertyDetails Xml</returns>
        private string GetDistrictPropertyInfoXml()
        {
            F1031SpecialDistrictAssessmentData saveSpecialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            F1031SpecialDistrictAssessmentData.ListDistrictAssessmentPropertyRow dr = saveSpecialDistrictAssessmentData.ListDistrictAssessmentProperty.NewListDistrictAssessmentPropertyRow();
            
            //new field included in typed DataSet
            if (!this.newRecord) 
            {
                dr.WorkingFileID = this.keyId;
            }
            dr.StatementID = this.currentStatementId;  
            ////dr.ParcelNumber = this.ParcelNumberTextBox.Text;
            dr.ParcelNumber = this.ParcelNumberTextBox.Text;
             
            //int OwnerID;
            //int.TryParse(this.CurrentOwnerID, out OwnerID);
            //dr.OwnerID = OwnerID;
            dr.OwnerID = this.ownerId; 
            if (!string.IsNullOrEmpty(this.SpecialDistrictLinkLabel.Tag.ToString()))
            {
                dr.SADistrictID = Convert.ToInt32(this.SpecialDistrictLinkLabel.Tag.ToString());
            }
            else
            {
                dr.SADistrictID = 0;
            }
            if (!string.IsNullOrEmpty(this.MortgageCoTextBox.Tag.ToString()))
            {
                dr.MortgageID = Convert.ToInt32(this.MortgageCoTextBox.Tag.ToString());
            }
            else
            {
                dr.MortgageID = 0;
            }
            dr.StatementNumber = this.StatementNumberLinkLabel.Text;
            //// dr.ParcelID = Convert.ToInt32(this.ParcelIDLinkLabel.Text);
            dr.ParcelID = this.parcelId;

            //// Assign the Values.
            byte ptypeId;
            Int16 ryear;
            decimal irrAcers;
            decimal rppAcers;

            byte.TryParse(this.TypeTextBox.Tag.ToString(), out ptypeId);
            Int16.TryParse(this.RollYearTextBox.Text, out ryear);
            decimal.TryParse(this.IrrigableAcresTextBox.Text.Trim(), out irrAcers);
            decimal.TryParse(this.RPAcresCountTextBox.Text.Trim(), out rppAcers);

            dr.PostTypeID = ptypeId;
            dr.RollYear = ryear;
            dr.IrrgAcres = irrAcers;
            dr.Acres = rppAcers;
            dr.Turnouts = this.TurnoutTextBox.Text.Trim();
            dr.Situs = this.SitusTextBox.Text.Trim();
            dr.MapNumber = this.MapTextBox.Text.Trim();
            dr.Legal = this.LegalTextBox.Text.Trim();

            if (this.currentStatementId > 0)
            {
                dr.StatementID = this.currentStatementId;
            }
            else
            {
                dr.StatementID = 0;
            }

            saveSpecialDistrictAssessmentData.ListDistrictAssessmentProperty.Rows.Add(dr);
            string propertyInfoXml = TerraScanCommon.GetXmlString(saveSpecialDistrictAssessmentData.ListDistrictAssessmentProperty.Copy());
            return propertyInfoXml;
        }

        #endregion

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (RatesListingGridView.CurrentRowIndex >= 0 && this.RatesListingGridView.CurrentCell != null)
                {
                    if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows[RatesListingGridView.CurrentRowIndex][this.propertyInfoDataSet.ListDistrictAssessmentRates.RateAcresIDColumn] != null)
                    {
                        if (!string.IsNullOrEmpty(this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows[RatesListingGridView.CurrentRowIndex][this.propertyInfoDataSet.ListDistrictAssessmentRates.RateAcresIDColumn].ToString()))
                        {
                            this.selectedRateItem = Convert.ToInt32(this.RatesListingGridView.Rows[RatesListingGridView.CurrentRowIndex].Cells[this.RateAcresID.Name].Value.ToString());
                        }
                        else
                        {
                            this.selectedRateItem = 0;
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
        /// Handles the ColumnHeaderMouseClick event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.RatesListingGridView.AllowSorting)
                {
                    int itemFound = this.rateSource.Find("RateAcresID", this.selectedRateItem);
                    TerraScanCommon.SetDataGridViewPosition(this.RatesListingGridView, itemFound);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //// Initialise the rowIndex 
                int rowIndex = -1;

                // Checks the rowindex is valid
                if (e.RowIndex >= 0 && this.RatesListingGridView.CurrentCell != null)
                {
                    //// Set the current RowIndex
                    rowIndex = e.RowIndex;
                }
                else if (this.RatesListingGridView.CurrentRowIndex >= 0 && this.RatesListingGridView.CurrentCell != null)
                {
                    //// Set the Current rowindex
                    rowIndex = RatesListingGridView.CurrentRowIndex;
                }
                //// find the primary key id to locate the RateAcresID
                if (rowIndex >= 0)
                {
                    if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows[rowIndex][this.propertyInfoDataSet.ListDistrictAssessmentRates.RateAcresIDColumn] != null)
                    {
                        if (!string.IsNullOrEmpty(this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows[rowIndex][this.propertyInfoDataSet.ListDistrictAssessmentRates.RateAcresIDColumn].ToString()))
                        {
                            this.selectedRateItem = Convert.ToInt32(this.RatesListingGridView.Rows[rowIndex].Cells[this.RateAcresID.Name].Value.ToString());
                        }
                        else
                        {
                            this.selectedRateItem = 0;
                        }
                    }
                }
                //// Using binding source select the datagridview poistion.
                if (this.RatesListingGridView.AllowSorting && this.selectedRateItem >= 0)
                {
                    int itemFound = this.rateSource.Find("RateAcresID", this.selectedRateItem);
                    TerraScanCommon.SetDataGridViewPosition(this.RatesListingGridView, itemFound);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Custom Format for RPAcres,IrrgabileAcres
        /// <summary>
        /// Customs the format.
        /// </summary>
        /// <param name="textbox">The textbox.</param>
        private void Customformat(TerraScanTextBox textbox)
        {
            string textboxvalue = textbox.DecimalTextBoxValue.ToString();
            int leng = textboxvalue.Length;

            //// to get the decimal position.
            int decPos = textboxvalue.IndexOf(".");
            ////to check if decimal places are avilable.
            if (decPos != -1)
            {
                string wholepart = textboxvalue.Substring(0, decPos);
                int wholepartleng;
                wholepartleng = wholepart.Length;
                if (wholepartleng > 8)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textbox.Text = "0.00";
                    textbox.Focus();
                    return;
                }
                if (leng - (decPos + 1) > 0)
                {
                    //// To get decimal part
                    textboxvalue = textbox.DecimalTextBoxValue.ToString().Substring(decPos + 1, leng - (decPos + 1)).Trim();
                }

                if (textboxvalue.Length >= 5)
                {
                    MessageBox.Show("Precision should not be greater than 4", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ///Changes in Irrigable text box RP acres  text box Custom Format
                    textbox.TextCustomFormat = "#,##0.0000";
                    textbox.Text = textbox.DecimalTextBoxValue.ToString();
                    textbox.Focus();
                    return;
                }
            }
            else
            {
                if (leng > 8)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textbox.Text = "0.00";
                    textbox.Focus();
                    return;
                }
            }

            int zerocount = 0;
            int nonzerocount = 0;
            //// to get how many zero and non-zero are available in decimal part.
            for (int i = textboxvalue.Length; i >= 1; i--)
            {
                string arrChar = Convert.ToString(textboxvalue[i - 1]);
                if (arrChar.Equals("0"))
                {
                    if (nonzerocount >= 1)
                    {
                        nonzerocount++;
                    }
                    else
                    {
                        zerocount++;
                    }
                }
                else
                {
                    nonzerocount++;
                }
            }

            //// To set the customformat of the value.
            if (decPos != -1)
            {
                if (nonzerocount.Equals(0) || nonzerocount.Equals(1))
                {
                    textbox.TextCustomFormat = "#,##0.00";
                }
                else if (nonzerocount.Equals(2))
                {
                    textbox.TextCustomFormat = "#,##0.00";
                }
                else if (nonzerocount.Equals(3))
                {
                    textbox.TextCustomFormat = "#,##0.000";
                }
                else if (nonzerocount.Equals(4))
                {
                    textbox.TextCustomFormat = "#,##0.0000";
                }
                else
                {
                    textbox.TextCustomFormat = "#,##0.0000";
                }
            }
            else
            {
                //// To set the customformat of the text box.
                textbox.TextCustomFormat = "#,##0.00";
            }
        }
        #endregion

        private void RatesListingGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ParcelNumberTextBox_Leave(object sender, EventArgs e)
            {
            if (!this.IsbuttonClick)
            {
                this.parcelNo = this.ParcelNumberTextBox.Text;
                this.LegalTextBox.Text = "";
                this.SitusTextBox.Text = "";
                if (!string.IsNullOrEmpty(this.parcelNo))
                {
                    this.GetParcel(this.parcelNo);
                    this.SetEditRecord();
                }
                
            }

            this.IsbuttonClick = true ;
        }

        private void ParcelNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.loadForm)
            {
                this.IsbuttonClick = false;
            }
            else
            {
                this.IsbuttonClick = true;
            }
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            this.form16031Controll.WorkItem.F16031_ExeWriteStatement(this.keyId, TerraScanCommon.UserId, false);
            this.GetDistrictAssessmentDetails(this.keyId);
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.form16031Controll.WorkItem.F16031_ExeWriteStatement(this.keyId, TerraScanCommon.UserId, true);
            this.GetDistrictAssessmentDetails(this.keyId);
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }
    }
}
