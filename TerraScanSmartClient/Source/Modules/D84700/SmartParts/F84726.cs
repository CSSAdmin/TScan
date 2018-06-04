//--------------------------------------------------------------------------------------------
// <copyright file="F84726.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84726.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created// 
//*********************************************************************************/

namespace D84700
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
    /// F84726 class file
    /// </summary>
    public partial class F84726 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// pipeId(keyid)
        /// </summary>
        private int pipeId;

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// controller F84723
        /// </summary>
        private F84726Controller form84726Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// instance for GDocCommentData
        /// </summary>
        private GDocCommonData gdocCommonData = new GDocCommonData();

        /// <summary>
        /// An instance for waterValvePropertiesData
        /// </summary>
        private F84726WaterPipeLocationData waterPipeLocationData = new F84726WaterPipeLocationData();

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
        /// listGDocPropertyReference datatable
        /// </summary>
        private GDocCommonData.ListGDocPropertyReferenceDataTable listGDocPropertyReference = new GDocCommonData.ListGDocPropertyReferenceDataTable();

        /// <summary>
        /// No of rows present in the ListPropertyReference Datatable returned from DataBase
        /// </summary>
        private int gdocPropertyReferenceRowsCount;

        #region Variable For CoboBox Value Members

        /// <summary>
        /// administrativeAreaId
        /// </summary>
        private int administrativeAreaId;

        /// <summary>
        /// operationalAreaId
        /// </summary>
        private int operationalAreaId;

        /// <summary>
        /// gridId
        /// </summary>
        private int gridId;

        #endregion Variable For CoboBox Value Members

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84726"/> class.
        /// </summary>
        public F84726()
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
        public F84726(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.pipeId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterPipeLocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(118, 42, tabText, red, green, blue);
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
        /// <param name="featureClassId">The feature class id.</param>
        public F84726(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.pipeId = keyID;
            this.featureClassId = featureClassId;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterPipeLocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(118, 42, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For form84726Control
        /// </summary>
        [CreateNew]
        public F84726Controller Form84726Control
        {
            get { return this.form84726Control as F84726Controller; }
            set { this.form84726Control = value; }
        }

        #endregion Property

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
                if (this.PermissionFiled.newPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.ClearWaterPipeLocationControls();
                    this.SetNewComboIndex();
                    this.LockControls(true);
                    this.ControlLock(false);
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearWaterPipeLocationValve();
                    this.SetNewComboIndex();
                    this.LockControls(false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                this.LockControls(true);
                this.CustomizeWaterPipeLocation();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
            finally
            {
                this.Cursor = Cursors.Default;
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
                        this.SaveWaterPipeLocation();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CustomizeWaterPipeLocation();
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
            finally
            {
                this.Cursor = Cursors.Default;
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
                   
                    if (this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
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
                    this.pipeId = eventArgs.Data.SelectedKeyId;
                    this.LoadWaterPipeLocation();
                    if (this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows.Count > 0)
                    {
                        this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
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
        /// Event Subscription D84700_F84721_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84721_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.pipeId = eventArgs.Data.SelectedKeyId;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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

        #endregion Protected methods

        #region Methods

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.DistrictProjectTextBox.MaxLength = this.waterPipeLocationData.GetWaterPipeLocationDataTable.District_ProjectColumn.MaxLength;
            this.LocationNotesTextBox.MaxLength = this.waterPipeLocationData.GetWaterPipeLocationDataTable.LocationDescriptionColumn.MaxLength;

            this.AdministrativeAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.OperationalAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.GridComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
        }

        /// <summary>
        /// Clears the water pipe location controls.
        /// </summary>
        private void ClearWaterPipeLocationControls()
        {
            this.ElevationTextBox.Text = string.Empty;
            this.DepthTextBox.Text = string.Empty;
            this.LocationNotesTextBox.Text = string.Empty;
            this.DistrictProjectTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Clears all combo box.
        /// </summary>
        private void ClearAllComboBox()
        {
            this.ClearOperationalAreaComboBox();
            this.ClearAdministrativeAreaComboBox();
            this.ClearGridComboBox();
        }

        /// <summary>
        /// Clears the operational area combo box.
        /// </summary>
        private void ClearOperationalAreaComboBox()
        {
            this.AdministrativeAreaComboBox.DataSource = null;
            this.AdministrativeAreaComboBox.Items.Clear();
            this.AdministrativeAreaComboBox.Refresh();
        }

        /// <summary>
        /// Clears the administrative area combo box.
        /// </summary>
        private void ClearAdministrativeAreaComboBox()
        {
            this.OperationalAreaComboBox.DataSource = null;
            this.OperationalAreaComboBox.Items.Clear();
            this.OperationalAreaComboBox.Refresh();
        }

        /// <summary>
        /// Clears the grid combo box.
        /// </summary>
        private void ClearGridComboBox()
        {
            this.GridComboBox.DataSource = null;
            this.GridComboBox.Items.Clear();
            this.GridComboBox.Refresh();
        }

        /// <summary>
        /// Clears the Entire water pipe location Form.
        /// </summary>
        private void ClearWaterPipeLocationValve()
        {
            this.ClearWaterPipeLocationControls();
            this.ClearAllComboBox();
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.AdministrativeAreaPanel.Enabled = lockControl;
            this.OperationalAreaPanel.Enabled = lockControl;
            this.ElevationPanel.Enabled = lockControl;
            this.DepthPanel.Enabled = lockControl;
            this.GridPanel.Enabled = lockControl;
            this.DistrictProjectPanel.Enabled = lockControl;
            this.LocationNotesPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.ElevationPanel.Enabled = !controlLook; 
            this.ElevationTextBox.LockKeyPress = controlLook;
            this.DepthPanel.Enabled = !controlLook; 
            this.DepthTextBox.LockKeyPress = controlLook;
            this.LocationNotesPanel.Enabled = !controlLook; 
            this.LocationNotesTextBox.LockKeyPress = controlLook;
            this.DistrictProjectPanel.Enabled = !controlLook; 
            this.DistrictProjectTextBox.LockKeyPress = controlLook;

            this.AdministrativeAreaPanel.Enabled = !controlLook; 
            this.AdministrativeAreaComboBox.Enabled = !controlLook;
            this.OperationalAreaPanel.Enabled = !controlLook; 
            this.OperationalAreaComboBox.Enabled = !controlLook;
            this.GridPanel.Enabled = !controlLook; 
            this.GridComboBox.Enabled = !controlLook;
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
        /// Toes the enable edit button in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// Creates the temp listGdocpropertyreference Datatable to load combo boxs.
        /// </summary>
        /// <param name="refFieldValue">The ref field value.</param>
        private void CreateTempListGDocPropertyReference(string refFieldValue)
        {
            this.listGDocPropertyReference.Clear();
            DataRow customRow = this.listGDocPropertyReference.NewRow();
            customRow[this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName] = string.Empty;
            customRow[this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName] = "0";
            this.listGDocPropertyReference.Rows.Add(customRow);

            ////to clear the this.gdocCommonData.ListGDocPropertyReference
            this.gdocCommonData.ListGDocPropertyReference.Clear();
            this.gdocCommonData = this.form84726Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
            this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
            this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
        }

        /// <summary>
        /// Loads all combo box.
        /// </summary>
        private void LoadAllComboBox()
        {
            this.LoadAdministrativeAreaComboBox();
            this.LoadOperationalAreaComboBox();
            this.LoadGridComboBox();
        }

        /// <summary>
        /// Loads the administrative area combo box.
        /// </summary>
        private void LoadAdministrativeAreaComboBox()
        {
            this.CreateTempListGDocPropertyReference("AdministrativeAreaID");

            DataTable administrativeAreaComboDataTable = new DataTable();
            administrativeAreaComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.AdministrativeAreaComboBox.DataSource = administrativeAreaComboDataTable;
                this.AdministrativeAreaComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.AdministrativeAreaComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.administrativeAreaId > 0)
                {
                    this.AdministrativeAreaComboBox.SelectedValue = this.administrativeAreaId;
                }
                else
                {
                    this.AdministrativeAreaComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the operational area combo box.
        /// </summary>
        private void LoadOperationalAreaComboBox()
        {
            this.CreateTempListGDocPropertyReference("OperationalAreaID");

            DataTable operationalAreaComboDataTable = new DataTable();
            operationalAreaComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.OperationalAreaComboBox.DataSource = operationalAreaComboDataTable;
                this.OperationalAreaComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.OperationalAreaComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.operationalAreaId > 0)
                {
                    this.OperationalAreaComboBox.SelectedValue = this.operationalAreaId;
                }
                else
                {
                    this.OperationalAreaComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the grid combo box.
        /// </summary>
        private void LoadGridComboBox()
        {
            this.CreateTempListGDocPropertyReference("GridID");

            DataTable gridComboDataTable = new DataTable();
            gridComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.GridComboBox.DataSource = gridComboDataTable;
                this.GridComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.GridComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.gridId > 0)
                {
                    this.GridComboBox.SelectedValue = this.gridId;
                }
                else
                {
                    this.GridComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// To Load the water pipe location.
        /// </summary>
        private void LoadWaterPipeLocation()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeWaterPipeLocation();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Saves the water pipe location.
        /// </summary>
        private void SaveWaterPipeLocation()
        {
            decimal tempElevation;
            decimal tempDepth;

            F84726WaterPipeLocationData waterPipeLocationData1 = new F84726WaterPipeLocationData();
            //this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows.Clear();
            F84726WaterPipeLocationData.GetWaterPipeLocationDataTableRow dr = waterPipeLocationData1.GetWaterPipeLocationDataTable.NewGetWaterPipeLocationDataTableRow();

            if (!string.IsNullOrEmpty(this.DistrictProjectTextBox.Text.Trim()))
            {
                dr.District_Project = this.DistrictProjectTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ElevationTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.ElevationTextBox.Text.Trim(), out tempElevation);
                dr.Elevation = tempElevation;
            }

            if (!string.IsNullOrEmpty(this.DepthTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.DepthTextBox.Text.Trim(), out tempDepth);
                dr.Depth = tempDepth;
            }

            if (!string.IsNullOrEmpty(this.LocationNotesTextBox.Text.Trim()))
            {
                dr.LocationDescription = this.LocationNotesTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.AdministrativeAreaComboBox.Text.Trim()))
            {
                dr.AdministrativeAreaID = Convert.ToInt32(this.AdministrativeAreaComboBox.SelectedValue);
            }
            else
            {
                dr.AdministrativeAreaID = 0;
            }

            if (!string.IsNullOrEmpty(this.OperationalAreaComboBox.Text.Trim()))
            {
                dr.OperationalAreaID = Convert.ToInt32(this.OperationalAreaComboBox.SelectedValue);
            }
            else
            {
                dr.OperationalAreaID = 0;
            }

            if (!string.IsNullOrEmpty(this.GridComboBox.Text.Trim()))
            {
                dr.GridID = Convert.ToInt32(this.GridComboBox.SelectedValue);
            }
            else
            {
                dr.GridID = 0;
            }

           waterPipeLocationData1.GetWaterPipeLocationDataTable.Rows.Add(dr);
            this.pipeId = this.form84726Control.WorkItem.F84726_SaveWaterPipeLocation(this.pipeId, (Utility.GetXmlString(waterPipeLocationData1.GetWaterPipeLocationDataTable.Copy())), TerraScanCommon.UserId);
            ////to sent the this keyid(this.pipeId) 
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = this.pipeId;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.AdministrativeAreaComboBox.SelectedIndex = -1;
            this.OperationalAreaComboBox.SelectedIndex = -1;
            this.GridComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Customizes the water Pipe location.
        /// </summary>
        private void CustomizeWaterPipeLocation()
        {
            this.waterPipeLocationData = this.form84726Control.WorkItem.F84726_GetWaterPipeLocation(this.pipeId);
            if (this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows.Count > 0)
            {
                this.ElevationTextBox.Text = this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows[0][this.waterPipeLocationData.GetWaterPipeLocationDataTable.ElevationColumn].ToString();
                this.DepthTextBox.Text = this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows[0][this.waterPipeLocationData.GetWaterPipeLocationDataTable.DepthColumn].ToString();
                this.DistrictProjectTextBox.Text = this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows[0][this.waterPipeLocationData.GetWaterPipeLocationDataTable.District_ProjectColumn].ToString();
                this.LocationNotesTextBox.Text = this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows[0][this.waterPipeLocationData.GetWaterPipeLocationDataTable.LocationDescriptionColumn].ToString();

                this.administrativeAreaId = Convert.ToInt32(this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows[0][this.waterPipeLocationData.GetWaterPipeLocationDataTable.AdministrativeAreaIDColumn]);
                this.operationalAreaId = Convert.ToInt32(this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows[0][this.waterPipeLocationData.GetWaterPipeLocationDataTable.OperationalAreaIDColumn]);
                this.gridId = Convert.ToInt32(this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows[0][this.waterPipeLocationData.GetWaterPipeLocationDataTable.GridIDColumn]);
            }
            else
            {
                this.ClearWaterPipeLocationControls();
                this.LockControls(false);
            }

            ////To Load All The ComBoBoxs
            this.LoadAllComboBox();
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "Text Changed Events In Text Box"
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

        /// <summary>
        /// Handles the MouseEnter event of the WaterPipePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterPipePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocWaterPipeLocationToolTip.SetToolTip(this.WaterPipeLocationPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the WaterPipePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterPipePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the LocationNotesTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void LocationNotesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Handles the Load event of the F84726 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84726_Load(object sender, EventArgs e)
        {
            try
            {
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.LoadWaterPipeLocation();
                if (this.waterPipeLocationData.GetWaterPipeLocationDataTable.Rows.Count > 0)
                {
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
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        #endregion Events

        private void AdministrativeAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void OperationalAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void GridComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
