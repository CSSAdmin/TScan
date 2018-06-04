//--------------------------------------------------------------------------------------------
// <copyright file="F25003.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25003.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 04/05/2006       M.Vijaya Kumar     Created// 
//*********************************************************************************/

namespace D20003
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
    /// F25003 class file
    /// </summary>
    public partial class F25003 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the situsid (always -999 No Use)
        /// </summary>
        private int invalidsiusId = -999;

        /// <summary>
        /// used to store the valid key id
        /// </summary>
        private int validKeyId;

        /// <summary>
        /// Usde to store the parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// Usde to store situsId
        /// </summary>
        private int situsId;

        /// <summary>
        /// Used to store the situsStringValue
        /// </summary>
        private string situsStringValue;

        /// <summary>
        /// Used to store the listSitusManagementDataTableRowCount
        /// </summary>
        private int listSitusManagementDataTableRowCount;

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

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
        /// Used to store the current SitusId which is used to find the current row index in the grid afther saving in the 25004 form
        /// </summary>
        private string currentSitusId;

        /// <summary>
        /// Usde to store the tempSitusMgmtGridRowId
        /// </summary>
        private int tempSitusMgmtGridRowId;

        /// <summary>
        /// Usde to store the situsManagementData insatance of F25003SitusManagementData
        /// </summary>
        private F25003SitusManagementData situsManagementData = new F25003SitusManagementData();

        /// <summary>
        /// Used to store the listSitusManagementDataTable instance of F25003SitusManagementData.ListSitusManagementDataTable
        /// </summary>
        private F25003SitusManagementData.ListSitusManagementDataTable listSitusManagementDataTable = new F25003SitusManagementData.ListSitusManagementDataTable();

        /// <summary>
        /// controller F25003
        /// </summary>
        private F25003Controller form25003Control;

        #region Form Slice Variables

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25003"/> class.
        /// </summary>
        public F25003()
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
        public F25003(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.parcelId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.SitusManagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SitusManagementPictureBox.Height, this.SitusManagementPictureBox.Width, this.sectionIndicatorText, 28, 81, 128);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F25003Control
        /// </summary>
        [CreateNew]
        public F25003Controller Form25003Control
        {
            get { return this.form25003Control as F25003Controller; }
            set { this.form25003Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (eventArgs.Data == this.masterFormNo)
                {
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    this.Height = this.SitusManagementPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
                    sliceResize.SliceFormHeight = this.SitusManagementPictureBox.Height;
                    this.SitusManagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SitusManagementPictureBox.Height, this.SitusManagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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
                this.ClearSitusManagementGrid();
                this.SitusManagementMainPanel.Enabled = false;
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

                    ////to check for invalid key id 
                    if (this.parcelId != eventArgs.Data.KeyId)
                    {
                        this.parcelId = eventArgs.Data.KeyId;
                        this.situsManagementData = this.form25003Control.WorkItem.F25003_ListSitusMangement(this.parcelId, -999);
                        if (this.situsManagementData.ListParcelValidID.Rows.Count > 0)
                        {
                            int.TryParse(this.situsManagementData.ListParcelValidID.Rows[0][this.situsManagementData.ListParcelValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
                        }
                        else
                        {
                            this.validKeyId = 0;
                        }
                    }

                    if (this.validKeyId > 0)
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

                    this.ToSetSlicePermission();
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
                    this.parcelId = eventArgs.Data.SelectedKeyId;

                    this.LoadSitusManagementGrid();
                    this.ToSetSlicePermission();
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
             var formn = (BaseSmartPart)sender;
               // Form varq = sender as Form; 
             if(this.masterFormNo.Equals(formn.ParentFormId) )  
                if (this.parcelId != -99)
                {
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = 20000;
                    sliceReloadActiveRecord.SelectedKeyId = this.parcelId;
                    OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                }
            }
        }


        #endregion Event Subscription

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
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

        #endregion Protected methods

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            D9030_F9030_LoadSliceDetails(this, eventArgs);

        }

        #region Methods

        /// <summary>
        /// To Enable the new modify delete button based on permission
        /// </summary>
        private void ToSetSlicePermission()
        {
            ////to enable the new modify delete buttons based on permissions
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.SitusModifyButton.Enabled = true;
            }
            else
            {
                this.SitusModifyButton.Enabled = false;
            }

            if (this.slicePermissionField.newPermission)
            {
                //// New Button is enabled even if no parcelID on page load. 
                if (!parcelId.Equals(-99))
                {
                    this.SitusNewButton.Enabled = true;
                }
                else
                {
                    this.SitusNewButton.Enabled = false;
                }
            }
            else
            {
                this.SitusNewButton.Enabled = false;
            }

            if (this.slicePermissionField.deletePermission)
            {
                this.SitusDeleteButton.Enabled = true;
            }
            else
            {
                this.SitusDeleteButton.Enabled = false;
            }
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.SitusManagementMainPanel.Enabled = lockControl;
            this.SitusButtonPanel.Enabled = lockControl;
        }

        /// <summary>
        /// Custimizes the situs grid.
        /// </summary>
        private void CustimizeSitusGrid()
        {
            this.SitusManagementGrid.AutoGenerateColumns = false;
            this.HouseNumber.DataPropertyName = this.listSitusManagementDataTable.HouseNumberColumn.ColumnName;
            this.StreetName.DataPropertyName = this.listSitusManagementDataTable.StreetNameColumn.ColumnName;
            this.UnitType.DataPropertyName = this.listSitusManagementDataTable.UnitTypeColumn.ColumnName;
            this.UnitNumber.DataPropertyName = this.listSitusManagementDataTable.UnitNumberColumn.ColumnName;
            this.City.DataPropertyName = this.listSitusManagementDataTable.CityColumn.ColumnName;
            this.Zip.DataPropertyName = this.listSitusManagementDataTable.ZipCodeColumn.ColumnName;
            this.Situs.DataPropertyName = this.listSitusManagementDataTable.SitusColumn.ColumnName;
            this.StreetID.DataPropertyName = this.listSitusManagementDataTable.StreetIDColumn.ColumnName;
            this.AddressID.DataPropertyName = this.listSitusManagementDataTable.AddressIDColumn.ColumnName;
            this.XCoordinate.DataPropertyName = this.listSitusManagementDataTable.X_CoordColumn.ColumnName;
            this.YCoordinate.DataPropertyName = this.listSitusManagementDataTable.Y_CoordColumn.ColumnName;
            this.SitusParcelID.DataPropertyName = this.listSitusManagementDataTable.ParcelIDColumn.ColumnName;
            this.SitusSitusID.DataPropertyName = this.listSitusManagementDataTable.SitusIDColumn.ColumnName;

            this.SetSmartPartHeight(this.listSitusManagementDataTable.Rows.Count);

            this.HouseNumber.DisplayIndex = 0;
            this.StreetName.DisplayIndex = 1;
            this.UnitType.DisplayIndex = 2;
            this.UnitNumber.DisplayIndex = 3;
            this.City.DisplayIndex = 4;
            this.Zip.DisplayIndex = 5;
            this.Situs.DisplayIndex = 6;
            this.StreetID.DisplayIndex = 7;
            this.AddressID.DisplayIndex = 8;
            this.XCoordinate.DisplayIndex = 9;
            this.YCoordinate.DisplayIndex = 10;
            this.SitusParcelID.DisplayIndex = 11;
            this.SitusSitusID.DisplayIndex = 12;
            this.SitusManagementGrid.PrimaryKeyColumnName = this.listSitusManagementDataTable.SitusIDColumn.ColumnName;
        }

        /// <summary>
        /// Loads the situs management grid.
        /// </summary>
        private void LoadSitusManagementGrid()
        {
            this.listSitusManagementDataTable.Clear();
            this.situsManagementData = this.form25003Control.WorkItem.F25003_ListSitusMangement(this.parcelId, this.invalidsiusId);
            this.listSitusManagementDataTable = this.situsManagementData.ListSitusManagement;
            if (this.situsManagementData.ListParcelValidID.Rows.Count > 0)
            {
                int.TryParse(this.situsManagementData.ListParcelValidID.Rows[0][this.situsManagementData.ListParcelValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
            }
            else
            {
                this.validKeyId = 0;
            }

            this.listSitusManagementDataTableRowCount = this.listSitusManagementDataTable.Rows.Count;

            if (this.listSitusManagementDataTableRowCount > 0)
            {
                this.SetSmartPartHeight(this.listSitusManagementDataTableRowCount);

                this.SitusManagementGrid.DataSource = this.listSitusManagementDataTable.DefaultView;
                this.SitusManagementGrid.Rows[0].Selected = true;
                this.SitusManagementGrid.Enabled = true;
                ////TerraScanCommon.SetDataGridViewPosition(this.SitusManagementGrid, 0);
                this.SitusGridpanel.Enabled = true;
                this.TitlePanel.Enabled = true;
                this.ModifyDeletePanel.Enabled = true;
            }
            else
            {
                this.ClearSitusManagementGrid();
                this.SitusGridpanel.Enabled = false;
                this.TitlePanel.Enabled = true;
                this.ModifyDeletePanel.Enabled = false;
            }

            if (this.listSitusManagementDataTableRowCount > this.SitusManagementGrid.NumRowsVisible)
            {
                this.SitusGridVerticalScroll.Visible = false;
            }
            else
            {
                this.SitusGridVerticalScroll.Visible = true;
            }

            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.SitusManagementPictureBox.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));

            if (!this.flagLoadOnProcess)
            {
                this.SitusManagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SitusManagementPictureBox.Height, this.SitusManagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
        }

        /// <summary>
        /// Clears the situs management grid.
        /// </summary>
        private void ClearSitusManagementGrid()
        {
            this.listSitusManagementDataTable.Clear();
            this.SitusManagementGrid.ClearSorting();
            this.SitusManagementGrid.DataSource = this.listSitusManagementDataTable.DefaultView ;
            this.listSitusManagementDataTableRowCount = this.SitusManagementGrid.OriginalRowCount;
            this.SitusManagementGrid.Rows[0].Selected = false;
            this.SitusManagementGrid.Enabled = false;

            this.SetSmartPartHeight(this.listSitusManagementDataTableRowCount);
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 3)
            {
                if (recordCount > 6)
                {
                    recordCount = 6;
                }

                int increment = ((recordCount - 3) * 22);
                this.SitusManagementGrid.Height = 89 + increment;
                this.SitusGridpanel.Height = this.SitusManagementGrid.Height;
                this.SitusGridVerticalScroll.Height = 89 + increment;
                this.SitusManagementMainPanel.Height = 151 + increment;
                this.SitusManagementPictureBox.Height = 151 + increment;
                this.SitusManagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SitusManagementPictureBox.Height, this.SitusManagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.SitusManagementGrid.NumRowsVisible = recordCount;
                this.Height = this.SitusManagementPictureBox.Height;
            }
            else
            {
                this.SitusManagementGrid.Height = 89;
                this.SitusGridpanel.Height = this.SitusManagementGrid.Height;
                this.SitusGridVerticalScroll.Height = this.SitusManagementGrid.Height; //// -1;
                this.SitusManagementMainPanel.Height = 151;
                this.SitusManagementPictureBox.Height = 151;
                this.SitusManagementGrid.NumRowsVisible = 3;
                this.Height = 151;
            }

            this.TitlePanel.Top = this.SitusGridpanel.Bottom - 1;
            this.SitusButtonPanel.Top = this.TitlePanel.Bottom - 1;
        }

        /// <summary>
        /// Selects the situsID form the grid based on the current row.
        /// </summary>
        private void SelectSitusID()
        {
            int rowId = 0;

            ////to get the current row id of the datagrid
            rowId = this.GetCurrentRowIndex();

            if (this.listSitusManagementDataTableRowCount > 0 && rowId >= 0)
            {
                if (!string.IsNullOrEmpty(this.SitusManagementGrid.Rows[rowId].Cells["SitusSitusID"].Value.ToString()))
                {
                    int.TryParse(this.SitusManagementGrid.Rows[rowId].Cells["SitusSitusID"].Value.ToString(), out this.situsId);
                }

                if (!string.IsNullOrEmpty(this.SitusManagementGrid.Rows[rowId].Cells["Situs"].Value.ToString()))
                {
                    this.situsStringValue = this.SitusManagementGrid.Rows[rowId].Cells["Situs"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>integer value of grid row index</returns>
        private int GetCurrentRowIndex()
        {
            try
            {
                if (this.listSitusManagementDataTableRowCount > 0)
                {
                    if (this.SitusManagementGrid.CurrentCell != null)
                    {
                        return this.SitusManagementGrid.CurrentCell.RowIndex;
                    }

                    return -1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// To get Situs Management Grid row index
        /// </summary>
        /// <param name="searchParcelId">The search parcel id.</param>
        /// <returns>Integer value of the row index</returns>
        private int GetRowIndex(string searchParcelId)
        {
            try
            {
                this.tempSitusMgmtGridRowId = -1;

                for (int i = 0; i < this.SitusManagementGrid.Rows.Count; i++)
                {
                    if ((this.SitusManagementGrid.Rows[i].Cells["SitusSitusID"].Value != null) && (!string.IsNullOrEmpty(this.SitusManagementGrid.Rows[i].Cells["SitusSitusID"].Value.ToString())))
                    {
                        if ((this.SitusManagementGrid.Rows[i].Cells["SitusSitusID"].Value.ToString() == searchParcelId))
                        {
                            return this.tempSitusMgmtGridRowId = i;
                        }
                    }
                }

                return this.tempSitusMgmtGridRowId;
            }
            catch (Exception ex)
            {
                return this.tempSitusMgmtGridRowId;
            }
        }

        /// <summary>
        /// To load the Situs edit Form
        /// </summary>
        /// <param name="currentSitusId">The current situs id.</param>
        private void LoadSitusEdit(int currentSitusId)
        {
            Form situsEdit = new Form();
            object[] optionalParameter = new object[] { currentSitusId, this.parcelId };

            situsEdit = this.form25003Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(25004, optionalParameter, this.form25003Control.WorkItem);

            if (situsEdit != null)
            {
                if (situsEdit.ShowDialog() != DialogResult.Cancel)
                {
                    this.currentSitusId = TerraScanCommon.GetValue(situsEdit, "CurrentSitusId");
                    this.LoadSitusManagementGrid();
                    this.GetRowIndex(this.currentSitusId);
                    this.SitusManagementGrid.Focus();
                    TerraScanCommon.SetDataGridViewPosition(this.SitusManagementGrid, this.tempSitusMgmtGridRowId);
                    this.ToSetSlicePermission();
                }
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F25003 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F25003_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustimizeSitusGrid();
                this.LoadSitusManagementGrid();
                this.ToSetSlicePermission();
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
        /// Handles the Click event of the SitusManagementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SitusManagementPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the SitusManagementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SitusManagementPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.SitusMgmtToolTip.SetToolTip(this.SitusManagementPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the SitusManagementGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SitusManagementGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.SitusManagementGrid.Rows[e.RowIndex].Cells["SitusSitusID"].Value.ToString()))
                    {
                        int.TryParse(this.SitusManagementGrid.Rows[e.RowIndex].Cells["SitusSitusID"].Value.ToString(), out this.situsId);
                        this.LoadSitusEdit(this.situsId);
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
        /// Handles the Click event of the SitusNewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SitusNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadSitusEdit(0);
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
        /// Handles the Click event of the SitusModifyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SitusModifyButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SelectSitusID();
                this.LoadSitusEdit(this.situsId);
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
        /// Handles the Click event of the SitusDeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SitusDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult;
                this.SelectSitusID();
                if (this.situsId > 0)
                {
                    dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("Delete ") + this.situsStringValue + SharedFunctions.GetResourceString(" Are you sure"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.OK)
                    {
                        this.form25003Control.WorkItem.F25003_DeleteSitusManagement(this.situsId, TerraScanCommon.UserId);

                        ////to reload this form slice
                        this.LoadSitusManagementGrid();
                        this.ToSetSlicePermission();
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        return;
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

        #endregion Events
    }
}
