//---------------------------------------------------------------------------------
// <copyright file="F27008.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27008 Treasurer Parcel Ownership.
// </summary>
//---------------------------------------------------------------------------------
// Change History
//*********************************************************************************
// Date			    Author		          Description
// ----------		---------		      -----------------------------------------
//  
//25-11-10          Manoj Kumar             TSCO-9514 Add Dialog Box for save Operation
//*********************************************************************************/


namespace D22008
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
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
    using TerraScan.Infrastructure.Interface.Constants;  
    using TerraScan.Utilities;
   
    /// <summary>
    /// F27008 Class file
    /// </summary>
    
    public partial class F27008 : BaseSmartPart
    {
        #region varaiables

        /// <summary>
        /// Used to store the parcelId(keyid)
        /// </summary>
        private int parcelId;

        /// <summary>
        /// Used to store the parcelOwnerGridClick
        /// </summary>
        private bool parcelOwnerGridClick;

         /// <summary>
        /// Used to store validKeyId
        /// </summary>
        private int validKeyId;

        /// <summary>
        ///  Keep A Track Of UpdateStatus
        /// </summary>
        private bool selectionkeyPressed;
        /// <summary>
        /// used to store primary value
        /// </summary>
        private int primary;
        /// <summary>
        ///  Used to store tempParcelOwnerGridRowId
        /// </summary>
        private int tempParcelOwnerGridRowId;

        /// <summary>
        /// ownerPercent
        /// </summary>
        private decimal ownerPercent;

        /// <summary>
        /// Used to store futurepush
        /// </summary>
        private bool futurePush;

        /// <summary>
        /// Used to store the saveOwnerPercent
        /// </summary>
        private decimal saveOwnerPercent;

        /// <summary>
        /// Used to store avoidParcelGridRowEnter
        /// </summary>
        private bool avoidParcelGridRowEnter;

        /// <summary>
        /// Used to store selectedOwnerId
        /// </summary>
        private int selectedOwnerId;

        /// <summary>
        /// Used to store the rows count of Parcel owners grid
        /// </summary>
        private int parcelOwnersGridCount;

        /// <summary>
        /// Used to store the currentRowIndex
        /// </summary>
        private int currentRowIndex;
        
        /// <summary>
        /// controller F27008
        /// </summary> 
        private F27008Controller form27008Control;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store parcelOwnershipData which is instance of F27008ParcelOwnershipData
        /// </summary>
       // private F27006ParcelOwnershipData parcelOwnershipData = new F27006ParcelOwnershipData();
        private F27008TRParcelOwnershipData parcelOwnershipData = new F27008TRParcelOwnershipData(); 

        /// <summary>
        /// used to show the name/address search form 
        /// </summary>
        private F15010ExciseAffidavitData exciseaffidavitdata = new F15010ExciseAffidavitData();

        /// <summary>
        /// Used to store the listParcelOwnershipDataTableDataTable
        /// </summary>
        //private F27006ParcelOwnershipData.ListParcelOwnershipDataTableDataTable listParcelOwnershipDatatable = new F27006ParcelOwnershipData.ListParcelOwnershipDataTableDataTable();
        private F27008TRParcelOwnershipData.ListParcelOwnershipDatatableDataTable listParcelOwnershipDatatable = new F27008TRParcelOwnershipData.ListParcelOwnershipDatatableDataTable();
        /// <summary>
        /// used to get the listOwnersDataTableDataTable
        /// </summary>
        private F27008TRParcelOwnershipData.ListOwnersDatatableDataTable ListOwnershipDatatable = new F27008TRParcelOwnershipData.ListOwnersDatatableDataTable(); 
        
        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Used to store isgridRowChange
        /// </summary>
        private bool isgridRowChange;


        /// <summary>
        /// Used to save saveRowIndex
        /// </summary>
        private int saveRowIndex;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store flagFormEdited
        /// </summary>
        private bool flagFormEdited;
        
        /// <summary>
        /// Used to store the flagForMessageNo
        /// </summary>
        private bool flagForMessageNo;

        /// <summary>
        /// Used to store validationMessage;
        /// </summary>
        private string validationMessage;
       
        /// <summary>
        /// used to set Yes or No for IsPrimary
        /// </summary>
        private bool primaryid;

        /// <summary>
        /// used to set Yes or No for IsTaxPayer
        /// </summary>
        private bool taxpayerId;


        /// <summary>
        /// Used to store ownerId
        /// </summary>
        private string ownerId;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// extraownerid for mastername search form 
        /// </summary>
        private int extraownerId; 

        /// <summary>
        /// Used to store iseditOn
        /// </summary>
        private bool iseditOn;

        #endregion variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F27008"/> class.
        /// </summary>
        public F27008()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F27008(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.parcelId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.AssociateOwnerPictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociateOwnerPictureBox1.Height, this.AssociateOwnerPictureBox1.Width, "Associated Owners", 28, 81, 128); ////todo remove hard code value
           
        }
       #endregion constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

         /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

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
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region property
        /// <summary>
        /// For F27008Control
        /// </summary>
        /// 
        [CreateNew]
        public F27008Controller Form27008Control
        {
            get { return this.form27008Control as F27008Controller; }
            set { this.form27008Control = value; }
        }

        #endregion property

        #region event subscription
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
                this.flagFormEdited = false;
                this.flagForMessageNo = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                ////For Form funcatonality this is commented
                if (this.validKeyId > 0)
                {
                    this.LockControls(true);
                }
                else
                {
                    this.LockControls(false);
                }

                this.LoadParcelOwnership();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.flagFormEdited = false;
                    this.flagForMessageNo = false;
                    this.parcelId = eventArgs.Data.SelectedKeyId;
                    this.LoadParcelOwnership();
                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.PermissionFiled.editPermission)
                    {
                        ///Modified for the CO #9514  
                        //DialogResult Future = MessageBox.Show("Do you want to push the changes to this Parcel’s Tax Payer to all future Roll Years?", "TerraScan – Push Tax Payer to Future Years", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (Future.Equals(DialogResult.Yes))
                        //{
                        //    this.futurePush = true;
                        //}
                        //else
                        //{
                        //    this.futurePush = false;
                        //}
                        this.SaveParcelOwnership();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    if (this.validKeyId > 0)
                    {
                        this.LockControls(true);
                    }
                    else
                    {
                        this.LockControls(false);
                    }

                    this.ControlLock(false);
                    this.LoadParcelOwnership();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            this.listParcelOwnershipDatatable.AcceptChanges();  
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.PermissionFiled.editPermission)
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
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                    ////to check for invalid key id 
                    if (this.parcelId != eventArgs.Data.KeyId)
                    {
                        this.parcelId = eventArgs.Data.KeyId;
                        this.parcelOwnershipData = this.form27008Control.WorkItem.F27008_ListParcelOwnership(this.parcelId);
                        int.TryParse(this.parcelOwnershipData.ListOwnerValidID.Rows[0][this.parcelOwnershipData.ListOwnerValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
                    }

                    ////For Form funcatonality this is commented
                    if (this.validKeyId > 0)
                    {
                        this.LockControls(true);
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        this.LockControls(false);
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Forms the close.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_BaseSmartPart_formClose, Thread = ThreadOption.UserInterface)]
        public void FormClose(object sender, DataEventArgs<string> e)
        {
            if (e.Data == "ApplicationExitCall")
            {
                TerraScanCommon.FormName = string.Empty;
            }
            else if (e.Data == "UserClosing")
            {
                if (this.parcelId != -99)
                {
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = 20000;
                    sliceReloadActiveRecord.SelectedKeyId = this.parcelId;
                    this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                }
            }
        }

        #endregion event subscription

        #region Form Loading

        /// <summary>
        /// Handles the Load event of the F27008 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
       
        private void F27008_Load(object sender, EventArgs e)
        {
            try
            {
                this.flagFormEdited = false;
                this.SetMaxLength();
                this.CustimizeAssociatedOwnersGrid();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.LoadParcelOwnership();
                this.RemoveDefaultSelection();
               
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion  Form Loading

        #region protected methods
        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            this.D9030_F9030_LoadSliceDetails(this, eventArgs);
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

        #endregion protected methods

        #region Common Methods

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.FirstNameTextBox.MaxLength = this.listParcelOwnershipDatatable.FirstNameColumn.MaxLength;
            this.LastNameTextBox.MaxLength = this.listParcelOwnershipDatatable.LastNameColumn.MaxLength;
            this.IsTaxPayerComboBox.MaxLength = this.listParcelOwnershipDatatable.IsTaxPayerColumn.MaxLength;
            this.Address1TextBox.MaxLength = this.listParcelOwnershipDatatable.Address1Column.MaxLength;
            this.Address2TextBox.MaxLength = this.listParcelOwnershipDatatable.Address2Column.MaxLength;
        }
         /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.IsTaxPayerComboBox.Enabled = !controlLook;
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.AssociateOwnerPanel.Enabled = lockControl;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }
        /// <summary>
        /// To save ParcelOwnership
        /// </summary>
        private void SaveParcelOwnership()
        {
            this.listParcelOwnershipDatatable.AcceptChanges();
            string parcelOwnership = TerraScanCommon.GetXmlString(this.listParcelOwnershipDatatable);
            this.form27008Control.WorkItem.F27008_SaveParcelOwnership(parcelOwnership, this.parcelId, TerraScanCommon.UserId);
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess )
            {
                this.EditEnabled();
                this.ParcelOwnershipDataGridView.Columns[this.ParcelOwnerName.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Columns[this.OwnerPercent1.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.PrimaryTextColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.TaxPayerTextColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;    
                this.ParcelOwnershipDataGridView.Refresh();
            }
        }

        /// <summary>
        /// Removes the default selection.
        /// </summary>
        private void RemoveDefaultSelection()
        {
            if (this.ParcelOwnershipDataGridView.OriginalRowCount == 0)
            {
                this.ParcelOwnershipDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.ParcelOwnershipDataGridView.RemoveDefaultSelection = false;
            }
        }
        /// <summary>
        /// To get Parcel Ownership datagrid row index
        /// </summary>
        /// <param name="searchOwnerId">Current Unique order Id to search</param>
        /// <returns>Integer value of the row index </returns>
        private int GetRowIndex(string searchOwnerId)
        {
            try
            {
               
                DataRow[] getrow = this.listParcelOwnershipDatatable.Select("OwnerID=" + searchOwnerId);
                if (getrow.Length > 0)
                {
                    int currnetrow = this.listParcelOwnershipDatatable.Rows.IndexOf(getrow[0]); // listParcelOwnershipDatatable.Rows.IndexOf(getrow);       
                    return this.tempParcelOwnerGridRowId = currnetrow;
                }
                else
                {
                    return this.tempParcelOwnerGridRowId = -1;
                }
            }
            catch (Exception ex)
            {
                return this.tempParcelOwnerGridRowId;
            }
        }
            
        #endregion Common Methods

        #region Common Events
        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "selected Index  Changed Events In Combo Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        ///<summary>
        /// Slice validation for check errors before saving the record.
        ///</summary>
        ////// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            this.validationMessage = string.Empty;
            this.AssginDataToParcelGrid();
            this.listParcelOwnershipDatatable.AcceptChanges();
            
            decimal.TryParse(this.OwnerPercentTextBox.Text.Replace("%", "").Trim(), out this.saveOwnerPercent);
           if (ParcelOwnershipDataGridView.OriginalRowCount.Equals(0))
            {
                ////MessageBox.Show("This record cannot be saved because no Owner has been given an Order of 1 (one).", SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = "This record cannot be saved because no Primary Owner assigned for this parcel";
                return sliceValidationFields;
            }
            ////set atleast one primary as true
            DataRow[] TaxPayerrow = this.listParcelOwnershipDatatable.Select("IsTaxPayer=True");
            if (TaxPayerrow.Length.Equals(1))
            {
               return sliceValidationFields;
                
            }
            else
            {
                
                 DialogResult primary = MessageBox.Show("This record cannot be saved because only one Tax Payer must assigned for this parcel", SharedFunctions.GetResourceString("TerrascanTaxPayer"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                 if (primary.Equals(DialogResult.OK))
                 {
                     sliceValidationFields.RequiredFieldMissing = false;
                     sliceValidationFields.DisableNewMethod = true;
                     return sliceValidationFields;
                 }
                
               //// sliceValidationFields.ErrorMessage = ("This record cannot be saved because only one Tax Payer must assigned for this parcel","TerraScan - Tax Payer");
                return sliceValidationFields;
            }
        }
        #endregion Common Events

        #region parcelOwnershipGridView methods

        /// <summary>
        /// Used to Set the correct value to the ComboBox
        /// </summary>
        /// <param name="setComboBox">The set combo box.</param>
        /// <param name="comboxString">The combox string.</param>
        private static void SetComboboxValue(TerraScan.UI.Controls.TerraScanComboBox setComboBox, string comboxString)
        {
            int correctIndex = 0;
            comboxString = comboxString.ToUpperInvariant();
            if (String.Compare(comboxString, SharedFunctions.GetResourceString("FALSEValue"))==0 || String.Compare(comboxString, SharedFunctions.GetResourceString("TRUEValue")) == 0)
            {
                if (String.Compare(comboxString, SharedFunctions.GetResourceString("FALSEValue")) == 0)
                {
                    correctIndex = 0;
                }
                else
                {
                    correctIndex = 1;
                }
            }
            else
            {
                correctIndex = setComboBox.FindString(comboxString);
            }

            setComboBox.SelectedIndex = correctIndex;
        }

        /// <summary>
        /// Used Fill the Parcel Ownership Header part
        /// </summary>
        /// <param name="currentRowNo">currentRowNo</param>
        private void GetAssociatedOwnersPart(int currentRowNo)
        {
            if ((this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value != null) && (!string.IsNullOrEmpty(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value.ToString())))
            {
                this.isgridRowChange = true;
                this.FirstNameTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.FirstNameColumn.ColumnName].Value.ToString();
                this.LastNameTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.LastNameColumn.ColumnName].Value.ToString();
                this.Address1TextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.Address1Column.ColumnName].Value.ToString();
                this.Address2TextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.Address2Column.ColumnName].Value.ToString();
                this.OwnerPercentTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.OwnerPercent1.Name].Value.ToString();
                this.ownerId = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value.ToString();
                this.CityTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.CityColumn.ColumnName].Value.ToString();
                this.StateTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.StateColumn.ColumnName].Value.ToString();
                this.OwnerCodeTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.OwnerCodeColumn.ColumnName].Value.ToString();
                this.ZipCodeTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.ZipColumn.ColumnName].Value.ToString();
                this.OwnerTypeTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].Value.ToString();                    ////sets the value of primary field to be (YES or NO)
                this.IsPrimaryTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value.ToString();
                bool.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value.ToString(), out this.primaryid);
                if (this.primaryid)
                {
                    this.IsPrimaryTextBox.Text = SharedFunctions.GetResourceString("YESValue");
                    //this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value = SharedFunctions.GetResourceString("YESValue");
                }
                else
                {
                    this.IsPrimaryTextBox.Text = SharedFunctions.GetResourceString("NOValue");
                    //this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value = SharedFunctions.GetResourceString("NOValue");
                }
                     
                SetComboboxValue(this.IsTaxPayerComboBox, this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName].Value.ToString());
                decimal.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.OwnerPercent1.Name].Value.ToString(), out this.ownerPercent);           
                //bool.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName].Value.ToString(), out this.taxpayerId);  
                this.IsTaxPayerComboBox.SelectedValue = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName].Value.ToString();
                int.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value.ToString(), out this.selectedOwnerId);
                int length = this.ParcelOwnershipDataGridView.OriginalRowCount;
                int undividedOwnershipId;
                int.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.UndividedOwnershipIDColumn.ColumnName].Value.ToString(),out undividedOwnershipId);
                string backcolor = null;
                string undividedName = null;
                string[] backcolorArr = null;
                int undividedId=0;
                int RColor;
                int GColor;
                int BColor;
              if (ParcelOwnershipDataGridView.OriginalRowCount>0)
                {
                    //make ListSeparateSTmtDataTable separate stmt texbox option and color.
                    DataRow[] background = this.parcelOwnershipData.ListParcelOwnershipDatatable.Select("UndividedOwnershipID=" + undividedOwnershipId); //("IsTRExtra=False");
                    if (background.Length > 0)
                    {
                        
                        if ((background[0]["UndividedOwnershipcolor"].Equals(null)))
                        {
                            backcolor = string.Empty;
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(background[0]["UndividedOwnershipcolor"].ToString()))
                            {
                                backcolor = string.Empty;
                            }
                            else
                            {
                                backcolor = ((TerraScan.BusinessEntities.F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow)(background[0])).UndividedOwnershipColor;
                            }
                        }
                        if ((background[0]["UndividedOwnership"].Equals(null)))
                        {
                            undividedName = string.Empty;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(background[0]["UndividedOwnership"].ToString()))
                            {
                                undividedName = string.Empty;
                            }
                            else
                            {
                                undividedName = ((TerraScan.BusinessEntities.F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow)(background[0])).UndividedOwnership;
                            }
                        }
                                       
                        this.SeparateStmtTextBox.Text = undividedName;
                    }
                    
                    if (!string.IsNullOrEmpty(backcolor))
                    {
                        char[] splitchar = { ',' };
                        backcolorArr = backcolor.Split(splitchar);
                        if (backcolorArr.Length.Equals(3))
                        {
                            ////Getting Red Color
                            if (string.IsNullOrEmpty(backcolorArr[0]))
                            {
                                RColor = 255;
                            }
                            else
                            {
                                int.TryParse(backcolorArr[0], out RColor);
                            }
                            ////Getting Green Color
                            if (string.IsNullOrEmpty(backcolorArr[1]))
                            {
                                GColor = 255;
                            }
                            else
                            {
                                int.TryParse(backcolorArr[1], out GColor);
                            }
                            ////Getting Blue Color
                            if (string.IsNullOrEmpty(backcolorArr[2]))
                            {
                                BColor = 255;
                            }
                            else
                            {
                                int.TryParse(backcolorArr[2], out BColor);
                            }
                            ////Assign RGB value to form backcolor
                            if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                            {
                                RColor = 255;
                                GColor = 255;
                                BColor = 255;
                            }
                            this.SeparateStmtTextBox.BackColor = Color.FromArgb(RColor, GColor, BColor);
                            this.SeparateStmtpanel.BackColor = Color.FromArgb(RColor, GColor, BColor);
                        }
                        else
                        {
                            this.SeparateStmtTextBox.BackColor = Color.White;
                            this.SeparateStmtpanel.BackColor = Color.White;
                            this.SeparateStmtTextBox.Text = string.Empty;
                        }
                    }
              }
                    else
                    {
                        this.SeparateStmtTextBox.BackColor = Color.White;
                        this.SeparateStmtpanel.BackColor = Color.White;
                        this.SeparateStmtTextBox.Text = string.Empty;     
                    }
              
                this.currentRowIndex = currentRowNo;
                this.isgridRowChange = false;
                this.LockControls(true);
            }
        }
        /// <summary>
        /// Clears all associated owner part.
        /// </summary>
        private void ClearAllAssociatedOwnerPart()
        {
            this.listParcelOwnershipDatatable.Clear();
            this.ParcelOwnershipDataGridView.DataSource = this.listParcelOwnershipDatatable.DefaultView;
            this.ParcelOwnershipDataGridView.Rows[0].Selected = false;
            ////to set the rowindex to 0 when the grid is disabled
            this.currentRowIndex = 0;
            this.ParcelOwnershipDataGridView.Enabled = false;
            this.ParcelOwnershipGridVerticalScroll.Visible = true;
            ////To clear the Associated Owner Grid header part
            this.ClearAssociatedOwnersPart();
            ////To fill the data in the footer part of the Associated Parcel grid
            this.OrderCountlabel.Text = string.Empty;
            this.TtlOwnPercentTextBox.Text = string.Empty;
            this.PercentLabel.Text = string.Empty;
            this.SeparateStmtTextBox.BackColor = Color.White;
            this.SeparateStmtpanel.BackColor = Color.White;
            this.AssociateOwnerPanel.Enabled = false;
            
        }
         /// <summary>
        /// To Custimize AssociatedOwnersGrid
        /// </summary>
        private void CustimizeAssociatedOwnersGrid()
        {
            this.ParcelOwnershipDataGridView.AutoGenerateColumns = false;
            this.MOwnerID.DataPropertyName = this.listParcelOwnershipDatatable.MOwnerIDColumn.ColumnName; ////"MOwnerI
            this.ParcelOwnerName.DataPropertyName = this.listParcelOwnershipDatatable.NameColumn.ColumnName;
            this.FirstName.DataPropertyName = this.listParcelOwnershipDatatable.FirstNameColumn.ColumnName;
            this.LastName.DataPropertyName = this.listParcelOwnershipDatatable.LastNameColumn.ColumnName;
            this.Address1.DataPropertyName = this.listParcelOwnershipDatatable.Address1Column.ColumnName;
            this.Address2.DataPropertyName = this.listParcelOwnershipDatatable.Address2Column.ColumnName;
            this.City.DataPropertyName = this.listParcelOwnershipDatatable.CityColumn.ColumnName;
             this.OwnerPercent1.DataPropertyName = this.listParcelOwnershipDatatable.OwnerPercentColumn.ColumnName;
            this.IsPrimary.DataPropertyName = this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName;
            this.Isbilled.DataPropertyName = this.listParcelOwnershipDatatable.IsBilledColumn.ColumnName;
            this.IsProrated.DataPropertyName = this.listParcelOwnershipDatatable.IsProRatedColumn.ColumnName;
            this.OwnerOrder.DataPropertyName = this.listParcelOwnershipDatatable.OwnerOrderColumn.ColumnName;
            this.ParcelOwnerID.DataPropertyName = this.listParcelOwnershipDatatable.OwnerIDColumn.ColumnName;
            this.IsCurrent.DataPropertyName = this.listParcelOwnershipDatatable.IsCurrentColumn.ColumnName;
            this.IsTaxPayer.DataPropertyName = this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName;
            this.IsTRExtra.DataPropertyName = this.listParcelOwnershipDatatable.IsTRExtraColumn.ColumnName;
            this.UndividedOwnershipId.DataPropertyName = this.listParcelOwnershipDatatable.UndividedOwnershipIDColumn.ColumnName;
            this.UndividedOwnershipOption.DataPropertyName= this.listParcelOwnershipDatatable.UndividedOwnershipColumn .ColumnName;
            this.UndividedOwnershipColor.DataPropertyName=this.listParcelOwnershipDatatable.UndividedOwnershipColorColumn.ColumnName;
            this.Zip.DataPropertyName = this.listParcelOwnershipDatatable.ZipColumn.ColumnName;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.OwnerCodeColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.OwnerCodeColumn.ColumnName;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeIDColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.MOwnerTypeIDColumn.ColumnName;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.StateColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.StateColumn.ColumnName;
            this.ParcelOwnershipDataGridView.PrimaryKeyColumnName = this.listParcelOwnershipDatatable.OwnerIDColumn.ColumnName;
       }

        ///<summary>
        ///sets general combobox value.
        ///</summary>
        private void SetGeneralCombobox()
        {
            this.IsTaxPayerComboBox.Items.Clear();
            this.IsTaxPayerComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.IsTaxPayerComboBox.Items.Insert(1,SharedFunctions.GetResourceString("YESValue"));     
        }


        /// <summary>
        /// Clears the associated owners Header part.
        /// </summary>
        private void ClearAssociatedOwnersPart()
        {
            this.FirstNameTextBox.Text = string.Empty;
            this.LastNameTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.OwnerPercentTextBox.Text = string.Empty;
            this.IsPrimaryTextBox.Text = string.Empty;
            this.IsTaxPayerComboBox.Text = string.Empty;
            this.OwnerTypeTextBox.Text = string.Empty;
            this.SeparateStmtTextBox.Text = string.Empty;
            this.OwnerCodeTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipCodeTextBox.Text = string.Empty;
        }

        ///// <summary>
        ///// To laod the Entire the Parcel ownership form
        ///// </summary>
        private void LoadParcelOwnership()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            //this.SeparateStmtTextBox.Refresh();
            //this.SeparateStmtpanel.Refresh();  
            //  this.parcelOwnershipData = this.form27008Control.WorkItem.F27008_ListParcelOwnership(this.parcelId);
            this.ParcelOwnershipDataGridView.Columns[this.ParcelOwnerName.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.OwnerPercent1.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.PrimaryTextColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.TaxPayerTextColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;         

            ////to set the value for the combo boxs
            this.SetGeneralCombobox();
            this.LoadParcelOwnershipDataGrid();
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// To Laod Parcel Ownership Grid
        /// </summary>
        private void LoadParcelOwnershipDataGrid()
        {
            this.listParcelOwnershipDatatable.Clear();
            this.parcelOwnershipData = this.form27008Control.WorkItem.F27008_ListParcelOwnership(this.parcelId);
            this.listParcelOwnershipDatatable = this.parcelOwnershipData.ListParcelOwnershipDatatable;
            int.TryParse(this.parcelOwnershipData.ListOwnerValidID.Rows[0][this.parcelOwnershipData.ListOwnerValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
            this.PopulateParcelOwnershipDataGrid(0);
        }

        /// <summary>
        ///  To Populates the parcel ownership data grid with Data form database.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulateParcelOwnershipDataGrid(int rowIndex)
        {
            this.ParcelOwnershipDataGridView.DataSource = this.listParcelOwnershipDatatable.DefaultView;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.PrimaryTextColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.TaxPayerTextColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;    
            this.parcelOwnersGridCount = this.ParcelOwnershipDataGridView.OriginalRowCount;
            if (this.parcelOwnersGridCount > 0)
            {
                this.ParcelOwnershipDataGridView.Focus();
                this.ParcelOwnershipDataGridView.Enabled = true;
                ////to fill the Associated Owner Grid header part                    
                this.GetAssociatedOwnersPart(rowIndex);
                this.currentRowIndex = rowIndex;
                //////To fill the data in the foorter part of the Associated Parcel grid
                this.OrderCountlabel.Text = this.parcelOwnersGridCount.ToString();
                this.AssociateOwnerPanel.Enabled = true;
                TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, rowIndex);
                this.ActiveControl = IsTaxPayerComboBox;
                this.IsTaxPayerComboBox.Focus();
                DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
                
            }
            else

            {
                this.ClearAllAssociatedOwnerPart();
            }
            if (this.listParcelOwnershipDatatable.Rows.Count > this.ParcelOwnershipDataGridView.NumRowsVisible)
            {
                this.ParcelOwnershipGridVerticalScroll.Visible = false;
            }
            else
            {
                this.ParcelOwnershipGridVerticalScroll.Visible = true;
            }
        }


        /// <summary>
        /// To get row particular index of the listParcelOwnershipDatatable
        /// </summary>
        /// <param name="currentOwnerId">Current Unique order Id to search</param>
        /// <returns>Integer value of the row index </returns>
        private int GetListParcelOwnershipDatatableRowIndex(string currentOwnerId)
        {
            try
            {
                 DataRow[] owner = this.listParcelOwnershipDatatable.Select("OwnerID=" + currentOwnerId);
                    if (owner.Length > 0)
                    {
                        int currnetIndex = this.listParcelOwnershipDatatable.Rows.IndexOf(owner[0]);  
                        return this.saveRowIndex = currnetIndex;
                    }                        
                
                return this.saveRowIndex;
            }
            catch (Exception ex)
            {
                return this.saveRowIndex;
            }
        }

        #endregion parcelOwnershipGridView methods

        #region parcelOwnershipGridView GridClick
        
        /// <summary>
        /// Handles the RowEnter event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelOwnerGridClick = true;
                if (e.RowIndex >= 0)
                 {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                    {
                        if (!this.avoidParcelGridRowEnter)
                        {
                            this.currentRowIndex = e.RowIndex;
                        }
                    }
                    else
                    {
                        this.GetAssociatedOwnersPart(e.RowIndex);
                    }
                }

                this.parcelOwnerGridClick = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>

        //used for Isprimary column to be red color if value is yes

        private void ParcelOwnershipDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty((this.ParcelOwnershipDataGridView.Rows[e.RowIndex].Cells["IsTaxPayer"].Value.ToString())))
                {
                    this.ParcelOwnershipDataGridView.Rows[currentRowIndex].Selected = true;
                    if (this.ParcelOwnershipDataGridView.Rows[e.RowIndex].Cells["IsTaxPayer"].Value.ToString().Equals("True"))
                    {
                       e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                    }
                                       
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

         /// <summary>
        /// To assign Values to ParcelGrid and datatable
        /// </summary>
        private void AssginDataToParcelGrid()
        {
            if (this.selectedOwnerId > 0)
            {
                this.GetListParcelOwnershipDatatableRowIndex(this.selectedOwnerId.ToString());
                if (this.saveRowIndex >= 0)
                {
                    
                    if (this.IsTaxPayerComboBox.SelectedItem.Equals("YES"))
                    {
                        this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsTaxPayerColumn] = 1;
                        //used to identify already a taxpayer exists
                        DataRow[] IsTaxPayerrow = this.listParcelOwnershipDatatable.Select("IsTaxPayer=True AND OwnerID <>" + this.selectedOwnerId.ToString());
                        if (IsTaxPayerrow.Length > 0)
                        {
                            //used to make previous taxpayer false and delete option.

                            bool trextra = ((TerraScan.BusinessEntities.F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow)(IsTaxPayerrow[0])).IsTRExtra;
                            if (trextra)
                            {
                                IsTaxPayerrow[0]["IsTaxPayer"] = "false";
                                IsTaxPayerrow[0]["IsDeleted"] = "true";
                            }
                            else
                            {
                                IsTaxPayerrow[0]["IsTaxPayer"] = "false";
                                // IsTaxPayerrow[0]["IsDeleted"] = "false";
                            }
                            this.listParcelOwnershipDatatable.AcceptChanges();
                        }
                   
                        DataRow[] IsTaxPayerrow1 = this.listParcelOwnershipDatatable.Select("IsTRExtra=True");
                        if (IsTaxPayerrow1.Length > 0)
                        {
                             bool taxpayer = ((TerraScan.BusinessEntities.F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow)(IsTaxPayerrow1[0])).IsTaxPayer;
                            if (!taxpayer)
                            {
                                IsTaxPayerrow1[0]["IsDeleted"] = "true";
                            }
                            this.listParcelOwnershipDatatable.AcceptChanges();
                        }
                                          
                        foreach (DataRow IsTaxPayerrow2 in this.listParcelOwnershipDatatable.Select("IsTRExtra=True AND IsTaxPayer=True"))
                        {
                            IsTaxPayerrow2["IsDeleted"] = "false";
                            this.listParcelOwnershipDatatable.AcceptChanges();
                        }
                            
                    }
                    else
                        this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsTaxPayerColumn] = 0;
                    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.OwnerPercentColumn.ColumnName] = this.OwnerPercentTextBox.Text.Replace("%", "").Trim();
                    this.listParcelOwnershipDatatable.AcceptChanges();
                    //this.iseditOn = false;
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.parcelOwnerGridClick = true;
                DataGridView currentGrid = (DataGridView)sender;
                ////int partieCoulmnIndex = currentGrid.CurrentCell.ColumnIndex;
                int partieRowIndex = currentGrid.CurrentCell.RowIndex;

                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                                {
                                    this.AssginDataToParcelGrid();
                                    this.GetAssociatedOwnersPart((partieRowIndex + 1));
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, partieRowIndex);
                                    this.currentRowIndex = partieRowIndex;
                                }

                                break;
                            }

                        case Keys.Up:
                            {
                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                                {
                                    this.AssginDataToParcelGrid();
                                    this.GetAssociatedOwnersPart((partieRowIndex - 1));
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, partieRowIndex);
                                    this.currentRowIndex = partieRowIndex;
                                }

                                break;
                            }
                    }
                }
                this.parcelOwnerGridClick = false;

            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the CellClick event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelOwnerGridClick = true;

                if (e.RowIndex >= 0)
                {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.AssginDataToParcelGrid();
                        this.GetAssociatedOwnersPart(e.RowIndex);
                        TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, e.RowIndex);
                        this.currentRowIndex = e.RowIndex;
                    }
                    else
                    {
                        this.GetAssociatedOwnersPart(e.RowIndex);
                        this.currentRowIndex = e.RowIndex;
                    }
                }

                this.parcelOwnerGridClick = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        #endregion parcelOwnershipGridView GridClick 
        
        /// <summary>
        /// selected taxPayer button click NameAddress form will appear.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectTaxPayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    this.AssginDataToParcelGrid();
                }
                Form parcelF9101 = new Form();
                parcelF9101 = this.form27008Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form27008Control.WorkItem);

                if (parcelF9101 != null)
                {
                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                    {
                        int allOwnerGirdRowIndexValue = -1;
                        this.extraownerId = int.Parse(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"));
                        this.ListOwnershipDatatable = this.form27008Control.WorkItem.F27008_GetOwnerDetails(this.extraownerId, TerraScanCommon.UserId).ListOwnersDatatable;
                       if (this.ListOwnershipDatatable.Rows.Count > 0)
                        {
                           if (this.GetRowIndex(this.extraownerId.ToString()) >= 0)
                            {
                                this.GetAssociatedOwnersPart(this.tempParcelOwnerGridRowId);
                            }
                            else
                            {
                                F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow templistParcelOwnersDatatableRow = this.listParcelOwnershipDatatable.NewListParcelOwnershipDatatableRow();
                                F27008TRParcelOwnershipData.ListOwnersDatatableRow templistOwnersDatatableRow = (F27008TRParcelOwnershipData.ListOwnersDatatableRow)this.ListOwnershipDatatable.Rows[0];
                                //assign the values to insert in the list parcel ownership datatable
                                // add new row for empty record
                                

                                    //remove previously selected associated owner
                                    int rowInde = this.ParcelOwnershipDataGridView.OriginalRowCount;
                                    DataRow[] trextra = this.listParcelOwnershipDatatable.Select("IsTRExtra=True");
                                    //int rowInde = this.ParcelOwnershipDataGridView.OriginalRowCount;
                                    if (trextra.Length > 0)
                                    {
                                        rowInde = this.listParcelOwnershipDatatable.Rows.IndexOf(trextra[0]);
                                        //this.ParcelOwnershipDataGridView.Rows.Remove(trextra);    
                                        this.listParcelOwnershipDatatable.Rows.RemoveAt(rowInde);

                                        this.listParcelOwnershipDatatable.AcceptChanges();

                                    }
                                    //// used to change previous taxpayer into false for new associated.
                                    DataRow[] taxPayer = this.listParcelOwnershipDatatable.Select("IsTaxPayer=True");
                                    if (taxPayer.Length > 0)
                                    {
                                              taxPayer[0]["IsTaxPayer"] = "false";
                                    }
                                    int undividedownershipid;
                                    int.TryParse(this.ParcelOwnershipDataGridView.Rows[0].Cells[this.UndividedOwnershipId.Name].Value.ToString(), out undividedownershipid);
                                    //int.TryParse(this.ParcelOwnershipDataGridView.Rows[0].Cells[this.listParcelOwnershipDatatable.UndividedOwnershipIDColumn.ColumnName].Value.ToString(), out undividedOwnershipId);
                                    //if (undividedownershipid.Equals(0))
                                    //{
                                    //    undividedownershipid = 1; 
                                    //}
                                    string backcolor = null;
                                    string[] backcolorArr = null;
                                    int RColor;
                                    int GColor;
                                    int BColor;
                                    short undividedid = 0;
                                    string undivided = null;
                                    // backcolor = this.ParcelOwnershipDataGridView.Rows[0].Cells[this.listParcelOwnershipDatatable.UndividedOwnershipColorColumn.ColumnName].Value.ToString();       
                                        DataRow[] background = this.parcelOwnershipData.ListParcelOwnershipDatatable.Select("UndividedOwnershipID=" + undividedownershipid);
                                        if (background.Length > 0)
                                        {
                                            backcolor = ((TerraScan.BusinessEntities.F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow)(background[0])).UndividedOwnershipColor;
                                            undivided = ((TerraScan.BusinessEntities.F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow)(background[0])).UndividedOwnership;
                                            undividedid = ((TerraScan.BusinessEntities.F27008TRParcelOwnershipData.ListParcelOwnershipDatatableRow)(background[0])).UndividedOwnershipID;
                                            this.SeparateStmtTextBox.Text = undivided;
                                        }
                                        if(ParcelOwnershipDataGridView.OriginalRowCount.Equals(0))  
                                        {
                                            backcolor = string.Empty;
                                            undivided = string.Empty;
                                            undividedid = 0;
                                        }
                                        if (!string.IsNullOrEmpty(backcolor))
                                        {
                                            char[] splitchar = { ',' };
                                            backcolorArr = backcolor.Split(splitchar);
                                            if (backcolorArr.Length.Equals(3))
                                            {
                                                ////Getting Red Color
                                                if (string.IsNullOrEmpty(backcolorArr[0]))
                                                {
                                                    RColor = 255;
                                                }
                                                else
                                                {
                                                    int.TryParse(backcolorArr[0], out RColor);
                                                }

                                                ////Getting Green Color
                                                if (string.IsNullOrEmpty(backcolorArr[1]))
                                                {
                                                    GColor = 255;
                                                }
                                                else
                                                {
                                                    int.TryParse(backcolorArr[1], out GColor);
                                                }

                                                ////Getting Blue Color
                                                if (string.IsNullOrEmpty(backcolorArr[2]))
                                                {
                                                    BColor = 255;
                                                }
                                                else
                                                {
                                                    int.TryParse(backcolorArr[2], out BColor);
                                                }

                                                ////Assign RGB value to form backcolor
                                                if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                                                {
                                                    RColor = 255;
                                                    GColor = 255;
                                                    BColor = 255;
                                                }
                                                this.SeparateStmtTextBox.BackColor = Color.FromArgb(RColor, GColor, BColor);
                                                this.SeparateStmtpanel.BackColor = Color.FromArgb(RColor, GColor, BColor);
                                            }
                                            else
                                            {
                                                this.SeparateStmtTextBox.BackColor = Color.White;
                                                this.SeparateStmtpanel.BackColor = Color.White;
                                            }
                                        }
                                        else
                                        {
                                            this.SeparateStmtTextBox.BackColor = Color.White;
                                            this.SeparateStmtpanel.BackColor = Color.White;
                                        }
                                    if (templistOwnersDatatableRow.IsFirstNameNull())
                                    {
                                        templistOwnersDatatableRow.FirstName = string.Empty;
                                    }
                                    if (templistOwnersDatatableRow.IsLastNameNull())
                                    {
                                        templistOwnersDatatableRow.LastName = string.Empty;
                                    }
                                    templistParcelOwnersDatatableRow.Name = templistOwnersDatatableRow.LastName + templistOwnersDatatableRow.FirstName;

                                    if (templistOwnersDatatableRow.IsAddress1Null())
                                    {
                                        templistOwnersDatatableRow.Address1 = string.Empty;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.Address1 = templistOwnersDatatableRow.Address1;
                                    }
                                    if (templistOwnersDatatableRow.IsAddress2Null())
                                    {
                                        templistOwnersDatatableRow.Address2 = string.Empty;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.Address2 = templistOwnersDatatableRow.Address2;
                                    }
                                    if (templistOwnersDatatableRow.IsFirstNameNull())
                                    {
                                        templistOwnersDatatableRow.LastName = string.Empty;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.FirstName = templistOwnersDatatableRow.FirstName;
                                    }
                                    if (templistOwnersDatatableRow.IsLastNameNull())
                                    {
                                        templistOwnersDatatableRow.LastName = string.Empty;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.LastName = templistOwnersDatatableRow.LastName;
                                    }
                                    templistParcelOwnersDatatableRow.OwnerID = this.extraownerId;
                                    if (templistOwnersDatatableRow.IsCityNull())
                                    {
                                        templistParcelOwnersDatatableRow.City = string.Empty;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.City = templistOwnersDatatableRow.City;
                                    }
                                    if (templistOwnersDatatableRow.IsOwnerPercentNull())
                                    {
                                        templistParcelOwnersDatatableRow.OwnerPercent = 0;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.OwnerPercent = templistOwnersDatatableRow.OwnerPercent;
                                    }
                                    templistParcelOwnersDatatableRow.MOwnerTypeID = 1;
                                    if (templistOwnersDatatableRow.IsStateNull())
                                    {
                                        templistOwnersDatatableRow.State = string.Empty;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.State = templistOwnersDatatableRow.State;
                                    }
                                    if (templistOwnersDatatableRow.IsZipNull())
                                    {
                                        templistParcelOwnersDatatableRow.Zip = string.Empty;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.Zip = templistOwnersDatatableRow.Zip;
                                    }
                                    templistParcelOwnersDatatableRow.MOwnerID = templistOwnersDatatableRow.MOwnerID;
                                    templistParcelOwnersDatatableRow.MOwnerType = templistOwnersDatatableRow.OwnerType;
                                    templistParcelOwnersDatatableRow.UndividedOwnershipID = undividedid;
                                    templistParcelOwnersDatatableRow.UndividedOwnership = undivided;
                                   templistParcelOwnersDatatableRow.UndividedOwnershipColor = backcolor;
                                   if (templistOwnersDatatableRow.IsIsPrimaryOwnerNull())
                                    {
                                        templistParcelOwnersDatatableRow.IsPrimary = false;
                                    }
                                    else
                                    {
                                        templistParcelOwnersDatatableRow.IsPrimary = false;
                                    }
                                    templistParcelOwnersDatatableRow.IsTaxPayer = templistOwnersDatatableRow.IsTaxPayer;
                                    templistParcelOwnersDatatableRow.IsTRExtra = true;
                                    templistParcelOwnersDatatableRow.IsDeleted = false;
                                    //templistParcelOwnersDatatableRow.MOwnerID = 0;   
                                    // this.AssginDataToParcelGrid(); 
                                   // this.listParcelOwnershipDatatable.AcceptChanges();
                                    if (this.ParcelOwnershipDataGridView.OriginalRowCount < this.ParcelOwnershipDataGridView.NumRowsVisible)
                                    {
                                        //this.listParcelOwnershipDatatable.Rows.RemoveAt(this.ParcelOwnershipDataGridView.OriginalRowCount);
                                        //this.avoidParcelGridRowEnter = true;
                                        this.listParcelOwnershipDatatable.Rows.InsertAt(templistParcelOwnersDatatableRow, rowInde);
                                        this.avoidParcelGridRowEnter = false;
                                    }
                                    else
                                    {
                                        //this.listParcelOwnershipDatatable.Rows[this.parcelOwnersGridCount][this.listParcelOwnershipDatatable.OwnerPercentColumn] = this.OwnerPercentTextBox.Text.Replace("%", "").Trim();
                                        this.listParcelOwnershipDatatable.Rows.InsertAt(templistParcelOwnersDatatableRow, rowInde);
                                         
                                    }
                                    this.listParcelOwnershipDatatable.Rows[rowInde][this.listParcelOwnershipDatatable.OwnerPercentColumn.ColumnName] = "0.000";// this.OwnerPercentTextBox.Text.Replace("%", "").Trim();   
                                    this.listParcelOwnershipDatatable.AcceptChanges();
                                    this.PopulateParcelOwnershipDataGrid(this.ParcelOwnershipDataGridView.OriginalRowCount-1);
                                    this.ToEnableEditButtonInMasterForm();
                                  
                                }
                           }
                        }
                    }
              }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the DataBindingComplete event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.ParcelOwnershipDataGridView.OriginalRowCount > 0)
                {
                    this.ParcelOwnershipDataGridView.Rows[this.currentRowIndex].Selected = true;
                    this.ParcelOwnershipDataGridView.CurrentCell = this.ParcelOwnershipDataGridView[ 2,this.currentRowIndex];
                    int length = this.ParcelOwnershipDataGridView.OriginalRowCount;
                    //// for converting the taxpayer true\false into yes\No
                    for (int i = 0; i < length; i++)
                    {
                        bool.TryParse(this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value.ToString(), out this.primaryid);
                        if (this.primaryid)
                        {
                            this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.PrimaryTextColumn.ColumnName].Value = SharedFunctions.GetResourceString("YESValue");
                        }
                        else
                        {
                            this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.PrimaryTextColumn.ColumnName].Value = SharedFunctions.GetResourceString("NOValue");
                        }
                        if (this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName].Value.ToString() != null && !string.IsNullOrEmpty(this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName].Value.ToString()))
                        {
                            bool.TryParse(this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName].Value.ToString(), out this.taxpayerId);
                            if (this.taxpayerId)
                            {
                                this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.TaxPayerTextColumn.ColumnName].Value  = SharedFunctions.GetResourceString("YESValue");
                            }
                            else
                            {
                                this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.TaxPayerTextColumn.ColumnName].Value = SharedFunctions.GetResourceString("NOValue");
                            }

                        }
                    }
                    object totalOwnership;
                    totalOwnership = this.listParcelOwnershipDatatable.Compute("SUM (OwnerPercent)", "OwnerID > 0");
                    this.TtlOwnPercentTextBox.Text = totalOwnership.ToString();
                    this.PercentLabel.Text = this.TtlOwnPercentTextBox.Text;
                }
                else
                {
                    ////To fill the data in the foorter part of the Associated stmt grid   
                    ////this.OrderTextBox.Focus();
                    this.TtlOwnPercentTextBox.Text = string.Empty;
                    this.PercentLabel.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void OwnerRecordPicture_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo sliceForm = new FormInfo();
                int formNo = 91000;
                sliceForm = TerraScanCommon.GetFormInfo(formNo);
                sliceForm.optionalParameters = new object[1];
                sliceForm.optionalParameters[0] = this.ownerId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(sliceForm));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }


       
    }


        

 }
    




