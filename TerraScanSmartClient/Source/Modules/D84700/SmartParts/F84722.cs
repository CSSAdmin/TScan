//--------------------------------------------------------------------------------------------
// <copyright file="F84722.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84722.
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
    /// F84722 Class file
    /// </summary>
    public partial class F84722 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// valveId or keyId
        /// </summary>
        private int valveId;

        /// <summary>
        /// to store the form no
        /// </summary>
        private int formNo;

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// controller F84722
        /// </summary>
        private F84722Controller form84722Control;

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
        /// An instance for waterValveLocationData
        /// </summary>
        private F84722WaterValveLocationData waterValveLocationData = new F84722WaterValveLocationData();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// listGDocStreet datatable instances
        /// </summary>
        private GDocCommonData.ListGDocStreetDataTable listGDocStreet = new GDocCommonData.ListGDocStreetDataTable();

        /// <summary>
        /// listGDocPropertyReference datatable
        /// </summary>
        private GDocCommonData.ListGDocPropertyReferenceDataTable listGDocPropertyReference = new GDocCommonData.ListGDocPropertyReferenceDataTable();

        /// <summary>
        /// No of rows present in the listGDocStreet datatable returned form database
        /// </summary>
        private int gdocListGDocStreetRowsCount;

        /// <summary>
        /// No of rows present in the ListPropertyReference Datatable returned from DataBase
        /// </summary>
        private int gdocPropertyReferenceRowsCount;

        #region Variable For CoboBox Value Members

        /// <summary>
        /// gpsById
        /// </summary>
        private int gpsById;

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

        /// <summary>
        /// nsStreetId
        /// </summary>
        private int northsouthStreetId;

        /// <summary>
        /// ewStreetId
        /// </summary>
        private int eastwestStreetId;

        #endregion Variable For CoboBox Value Members

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84722"/> class.
        /// </summary>
        public F84722()
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
        public F84722(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.Tag = formNo;
            this.masterFormNo = masterform;
            this.valveId = keyID;
            this.formNo = formNo;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterValveLocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(157, 42, tabText, red, green, blue);
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
        public F84722(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.formNo = formNo;
            this.valveId = keyID;
            this.featureClassId = featureClassId;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterValveLocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(157, 42, tabText, red, green, blue);
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
        /// For form84722Control
        /// </summary>
        [CreateNew]
        public F84722Controller Form84722Control
        {
            get { return this.form84722Control as F84722Controller; }
            set { this.form84722Control = value; }
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
                    this.ClearWaterValveLocationControls();
                    this.SetNewComboIndex();
                    this.LockControls(true);
                    this.ControlLock(false);
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearWaterValve();
                    this.SetNewComboIndex();
                    this.LockControls(false);
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
                this.CustomizeWaterValveLocation();
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
                        this.SaveWaterValveLocation();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CustomizeWaterValveLocation();
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
                    if (!this.IsDisposed)
                    {
                        if (this.waterValveLocationData.ListWaterValveLocation.Rows.Count > 0)
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
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.valveId = eventArgs.Data.SelectedKeyId;
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
                    this.valveId = eventArgs.Data.SelectedKeyId;
                    this.LoadWaterValveLocation();
                    if (this.waterValveLocationData.ListWaterValveLocation.Rows.Count > 0)
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
            this.GPSDateTextBox.MaxLength = this.waterValveLocationData.ListWaterValveLocation.GPSDateColumn.MaxLength;
            this.DistrictProjectTextBox.MaxLength = this.waterValveLocationData.ListWaterValveLocation.District_ProjectColumn.MaxLength;
            this.LocationNotesTextBox.MaxLength = this.waterValveLocationData.ListWaterValveLocation.LocationDescriptionColumn.MaxLength;
            this.GPSByComboBox.MaxLength = this.gdocCommonData.ListGDocUser.NameDisplayColumn.MaxLength;
            this.AdministrativeAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.OperationalAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.GridComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.NorthSouthStreetComboBox.MaxLength = this.gdocCommonData.ListGDocStreet.StreetNameColumn.MaxLength;
            this.EastWestStreetComboBox.MaxLength = this.gdocCommonData.ListGDocStreet.StreetNameColumn.MaxLength;
        }

        /// <summary>
        /// To Clear Water Valve Location Properties TextBoxs and Checked Boxs 
        /// </summary>
        private void ClearWaterValveLocationControls()
        {
            this.GPSDateTextBox.Text = string.Empty;
            this.XCoordinateTextBox.Text = string.Empty;
            this.YCoordinateTextBox.Text = string.Empty;
            this.DistrictProjectTextBox.Text = string.Empty;
            this.ElevationTextBox.Text = string.Empty;
            this.DepthTextBox.Text = string.Empty;
            this.LocationNotesTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To Clear the entire Water Valve Form
        /// </summary>
        private void ClearWaterValve()
        {
            this.ClearComboBox(this);
            this.ClearWaterValveLocationControls();
        }

        /// <summary>
        /// Clears the All the combo boxs in the form.
        /// </summary>
        /// <param name="currentControl">The current control.</param>
        private void ClearComboBox(Control currentControl)
        {
            if (currentControl.HasChildren)
            {
                foreach (Control childControl in currentControl.Controls)
                {
                    this.ClearComboBox(childControl);
                }
            }
            else
            {
                if (currentControl.GetType() == typeof(TerraScanComboBox))
                {
                    TerraScanComboBox currentComboBox = (TerraScanComboBox)currentControl;
                    currentComboBox.DataSource = null;
                    currentComboBox.Items.Clear();
                    currentComboBox.Refresh();
                }
            }
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean vlaue</param>
        private void LockControls(bool lockControl)
        {
            this.GPSDatePanel.Enabled = lockControl;
            ////to disable the GPSTimePicker conrols
            this.GPSTimePicker.Enabled = lockControl;
            this.GPSByPanel.Enabled = lockControl;
            this.XCoordinatePanel.Enabled = lockControl;
            this.YCoordinatePanel.Enabled = lockControl;
            this.AdministrativeAreaPanel.Enabled = lockControl;
            this.OperationalAreaPanel.Enabled = lockControl;
            this.GridPanel.Enabled = lockControl;
            this.DistrictProjectPanel.Enabled = lockControl;
            this.NorthSouthStreetPanel.Enabled = lockControl;
            this.EastWestStreetPanel.Enabled = lockControl;
            this.ElevationPanel.Enabled = lockControl;
            this.DepthPanel.Enabled = lockControl;
            this.LocationNotesPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.GPSDateTextBox.LockKeyPress = controlLook;
            this.XCoordinateTextBox.LockKeyPress = controlLook;
            this.YCoordinateTextBox.LockKeyPress = controlLook;
            this.DistrictProjectTextBox.LockKeyPress = controlLook;
            this.ElevationTextBox.LockKeyPress = controlLook;
            this.DepthTextBox.LockKeyPress = controlLook;
            this.LocationNotesTextBox.LockKeyPress = controlLook;

            ////to enable or disable the GPSTimePicker control
            this.GPSDatePanel.Enabled = !controlLook; 
            this.GPSTimePicker.Enabled = !controlLook;
            this.GPSDatePict.Enabled = !controlLook;
            this.GPSByPanel.Enabled = !controlLook; 
            this.GPSByComboBox.Enabled = !controlLook;
            this.AdministrativeAreaPanel.Enabled = !controlLook; 
            this.AdministrativeAreaComboBox.Enabled = !controlLook;
            this.OperationalAreaPanel.Enabled = !controlLook; 
            this.OperationalAreaComboBox.Enabled = !controlLook;
            this.GridPanel.Enabled = !controlLook; 
            this.GridComboBox.Enabled = !controlLook;
            this.NorthSouthStreetPanel.Enabled = !controlLook; 
            this.NorthSouthStreetComboBox.Enabled = !controlLook;
            this.EastWestStreetPanel.Enabled = !controlLook; 
            this.EastWestStreetComboBox.Enabled = !controlLook;

            this.ElevationPanel.Enabled = !controlLook;
            this.ElevationTextBox.Enabled = !controlLook;
            this.DepthPanel.Enabled = !controlLook;
            this.DepthTextBox.Enabled = !controlLook;
            this.XCoordinatePanel.Enabled = !controlLook;
            this.XCoordinateTextBox.Enabled = !controlLook;
            this.YCoordinatePanel.Enabled = !controlLook;
            this.YCoordinateTextBox.Enabled = !controlLook;
            this.DistrictProjectPanel.Enabled = !controlLook;
            this.DistrictProjectTextBox.Enabled = !controlLook;
            this.LocationNotesPanel.Enabled = !controlLook; 
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
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// Creates the Temp ListGdocpropertyreference For combo box which refereces the F8000_GetGDocPropertyReference method.
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
            this.gdocCommonData = this.form84722Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
            this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
            this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
        }

        /// <summary>
        /// To Load StreetCombo Box
        /// </summary>
        private void ToLoadAllStreetComboBox()
        {
            this.listGDocStreet.Clear();
            DataRow customRow = this.listGDocStreet.NewRow();
            this.listGDocStreet.Clear();
            customRow[this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName] = string.Empty;
            customRow[this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName] = "0";
            this.listGDocStreet.Rows.Add(customRow);

            this.gdocCommonData = this.form84722Control.WorkItem.wListStreets();
            this.gdocListGDocStreetRowsCount = this.gdocCommonData.ListGDocStreet.Rows.Count;
            this.listGDocStreet.Merge(this.gdocCommonData.ListGDocStreet);
        }

        /// <summary>
        /// Loads all combo box.
        /// </summary>
        private void LoadAllComboBox()
        {
            this.LoadGPSByComboBox();
            ////To Load the Street comboBoxs(NorthSouthStreetComboBox and EastWestStreetComboBox)
            this.ToLoadAllStreetComboBox();
            this.LoadNorthSouthStreetComboBox();
            this.LoadEastWestStreetComboBox();
            this.LoadAdministrativeAreaComboBox();
            this.LoadOperationalAreaComboBox();
            this.LoadGridComboBox();
        }

        /// <summary>
        /// Loads the GPSBy combo box.
        /// </summary>
        private void LoadGPSByComboBox()
        {
            GDocCommonData.ListGDocUserDataTable listGDocUser = new GDocCommonData.ListGDocUserDataTable();

            DataRow customRow = listGDocUser.NewRow();
            listGDocUser.Clear();
            customRow[this.gdocCommonData.ListGDocUser.NameDisplayColumn.ColumnName] = string.Empty;
            customRow[this.gdocCommonData.ListGDocUser.UserIDColumn.ColumnName] = "0";
            listGDocUser.Rows.Add(customRow);

            this.gdocCommonData = this.form84722Control.WorkItem.F8000_GetGDocUser();
            listGDocUser.Merge(this.gdocCommonData.ListGDocUser);

            if (this.gdocCommonData.ListGDocUser.Rows.Count > 0)
            {
                this.GPSByComboBox.DataSource = listGDocUser;
                this.GPSByComboBox.DisplayMember = this.gdocCommonData.ListGDocUser.NameDisplayColumn.ColumnName;
                this.GPSByComboBox.ValueMember = this.gdocCommonData.ListGDocUser.UserIDColumn.ColumnName;
                if (this.gpsById > 0)
                {
                    this.GPSByComboBox.SelectedValue = this.gpsById;
                }
                else
                {
                    this.GPSByComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the north south street combo box.
        /// </summary>
        private void LoadNorthSouthStreetComboBox()
        {
            DataTable northSouthStreetDataTable = new DataTable();
            northSouthStreetDataTable.Merge(this.listGDocStreet);

            if (this.gdocListGDocStreetRowsCount > 0)
            {
                this.NorthSouthStreetComboBox.DataSource = northSouthStreetDataTable;
                this.NorthSouthStreetComboBox.DisplayMember = this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName;
                this.NorthSouthStreetComboBox.ValueMember = this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName;
                if (this.northsouthStreetId > 0)
                {
                    this.NorthSouthStreetComboBox.SelectedValue = this.northsouthStreetId;
                }
                else
                {
                    this.NorthSouthStreetComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the east west street combo box.
        /// </summary>
        private void LoadEastWestStreetComboBox()
        {
            DataTable eastWestStreetDataTable = new DataTable();
            eastWestStreetDataTable.Merge(this.listGDocStreet);

            if (this.gdocListGDocStreetRowsCount > 0)
            {
                this.EastWestStreetComboBox.DataSource = eastWestStreetDataTable;
                this.EastWestStreetComboBox.DisplayMember = this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName;
                this.EastWestStreetComboBox.ValueMember = this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName;
                if (this.eastwestStreetId > 0)
                {
                    this.EastWestStreetComboBox.SelectedValue = this.eastwestStreetId;
                }
                else
                {
                    this.EastWestStreetComboBox.SelectedIndex = 0;
                }
            }
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
        /// To Load the water Valve Location form Slice
        /// </summary>
        private void LoadWaterValveLocation()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeWaterValveLocation();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Saves the water valve location.
        /// </summary>
        private void SaveWaterValveLocation()
        {
            decimal tempXCoordinate;
            decimal tempYCoordinate;
            decimal tempElevation;
            decimal tempDepth;
            string tempWaterValveLocation;


            F84722WaterValveLocationData waterValveLocationData1 = new F84722WaterValveLocationData();
            //this.waterValveLocationData.ListWaterValveLocation.Rows.Clear();
            F84722WaterValveLocationData.ListWaterValveLocationRow dr = waterValveLocationData1.ListWaterValveLocation.NewListWaterValveLocationRow();

            if (!string.IsNullOrEmpty(this.GPSDateTextBox.Text.Trim()))
            {
                dr.GPSDate = this.GPSDateTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.XCoordinateTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.XCoordinateTextBox.Text.Trim(), out tempXCoordinate);
                dr.X_Coord = tempXCoordinate;
            }

            if (!string.IsNullOrEmpty(this.YCoordinateTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.YCoordinateTextBox.Text.Trim(), out tempYCoordinate);
                dr.Y_Coord = tempYCoordinate;
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

            if (!string.IsNullOrEmpty(this.DistrictProjectTextBox.Text.Trim()))
            {
                dr.District_Project = this.DistrictProjectTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.GPSByComboBox.Text.Trim()))
            {
                dr.GPSByID = Convert.ToInt32(this.GPSByComboBox.SelectedValue);
            }
            else
            {
                dr.GPSByID = 0;
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

            if (!string.IsNullOrEmpty(this.NorthSouthStreetComboBox.Text.Trim()))
            {
                dr.N_S_StreetID = Convert.ToInt32(this.NorthSouthStreetComboBox.SelectedValue);
            }
            else
            {
                dr.N_S_StreetID = 0;
            }

            if (!string.IsNullOrEmpty(this.EastWestStreetComboBox.Text.Trim()))
            {
                dr.E_W_StreetID = Convert.ToInt32(this.EastWestStreetComboBox.SelectedValue);
            }
            else
            {
                dr.E_W_StreetID = 0;
            }

            waterValveLocationData1.ListWaterValveLocation.Rows.Add(dr);
            tempWaterValveLocation = Utility.GetXmlString(waterValveLocationData1.ListWaterValveLocation.Copy());
            this.valveId = this.form84722Control.WorkItem.F84722_SaveWaterValveLocation(this.valveId, tempWaterValveLocation, this.formNo, TerraScanCommon.UserId);

            ////to sent the this keyid(this.valveId) 
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = this.valveId;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));

        }

        /// <summary>
        /// Customizes the water valve location.
        /// </summary>
        private void CustomizeWaterValveLocation()
        {
            string dateFormat = SharedFunctions.GetResourceString("GDocWaterDateFormat");
            this.waterValveLocationData = this.form84722Control.WorkItem.F84722_GetWaterValveLocation(this.valveId, this.formNo);
            if (this.waterValveLocationData.ListWaterValveLocation.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.GPSDateColumn].ToString()))
                {
                    DateTime gpsDate;
                    gpsDate = Convert.ToDateTime(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.GPSDateColumn].ToString());
                    this.GPSDateTextBox.Text = gpsDate.ToString(dateFormat);
                }
                else
                {
                    this.GPSDateTextBox.Text = string.Empty;
                }

                this.XCoordinateTextBox.Text = this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.X_CoordColumn].ToString();
                this.YCoordinateTextBox.Text = this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.Y_CoordColumn].ToString();
                this.ElevationTextBox.Text = this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.ElevationColumn].ToString();
                this.DepthTextBox.Text = this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.DepthColumn].ToString();
                this.LocationNotesTextBox.Text = this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.LocationDescriptionColumn].ToString();
                this.DistrictProjectTextBox.Text = this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.District_ProjectColumn].ToString();

                this.gpsById = Convert.ToInt32(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.GPSByIDColumn]);
                this.administrativeAreaId = Convert.ToInt32(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.AdministrativeAreaIDColumn]);
                this.operationalAreaId = Convert.ToInt32(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.OperationalAreaIDColumn]);
                this.gridId = Convert.ToInt32(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.GridIDColumn]);
                this.northsouthStreetId = Convert.ToInt32(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.N_S_StreetIDColumn]);
                this.eastwestStreetId = Convert.ToInt32(this.waterValveLocationData.ListWaterValveLocation.Rows[0][this.waterValveLocationData.ListWaterValveLocation.E_W_StreetIDColumn]);
            }
            else
            {
                this.ClearWaterValve();
                this.LockControls(false);
            }

            ////To Load All The ComBoBoxs
            this.LoadAllComboBox();
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.GPSByComboBox.SelectedIndex = -1;
            this.NorthSouthStreetComboBox.SelectedIndex = -1;
            this.EastWestStreetComboBox.SelectedIndex = -1;
            this.AdministrativeAreaComboBox.SelectedIndex = -1;
            this.OperationalAreaComboBox.SelectedIndex = -1;
            this.GridComboBox.SelectedIndex = -1;
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
        /// Handles the Click event of the WaterValveLocationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterValveLocationPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the WaterValveLocationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterValveLocationPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocWaterValveLocationToolTip.SetToolTip(this.WaterValveLocationPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the GPSTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GPSTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                if (this.GPSDatePict.Enabled)
                {                 
                    this.GPSDateTextBox.Text = GPSTimePicker.Text;
                    this.ParentForm.ActiveControl = GPSDateTextBox;
                    this.ParentForm.ActiveControl.Focus();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the GPSDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GPSDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GPSDateTextBox.Text.Trim()))
                {
                    GPSTimePicker.Value = Convert.ToDateTime(GPSDateTextBox.Text);
                }
                else
                {
                    GPSTimePicker.Value = DateTime.Today;
                }

                GPSTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F84722 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84722_Load(object sender, EventArgs e)
        {
            try
            {
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.LoadWaterValveLocation();

                if (this.waterValveLocationData.ListWaterValveLocation.Rows.Count > 0)
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
        /// Handles the KeyPress event of the GPSTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GPSTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }

                GPSByComboBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        private void GPSByComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void NorthSouthStreetComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void EastWestStreetComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
