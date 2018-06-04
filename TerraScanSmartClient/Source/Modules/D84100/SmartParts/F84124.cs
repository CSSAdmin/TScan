//--------------------------------------------------------------------------------------------
// <copyright file="F84124.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84124.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created// 
//*********************************************************************************/
namespace D84100
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
    /// F84124 class file
    /// </summary>
    [SmartPart]
    public partial class F84124 : BaseSmartPart 
    {
       #region Member Variables

        /// <summary>
        /// valveId or keyId
        /// </summary>
        private int pipeId;

        /// <summary>
        /// to store the form no
        /// </summary>
        private int formNo;

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// Form84124Control Controller
        /// </summary>
        private F84124Controller form84124Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// No of rows present in the ListPropertyReference Datatable returned from DataBase
        /// </summary>
        private int gdocPropertyReferenceRowsCount;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// instance for GDocCommentData
        /// </summary>
        private GDocCommonData gdocCommonData = new GDocCommonData();

        /// <summary>
        /// An instance for waterValveLocationData
        /// </summary>
        private F84124SanitaryPipeLocationData sanitaryPipeLocationData = new F84124SanitaryPipeLocationData();

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// listGDocPropertyReference datatable
        /// </summary>
        private GDocCommonData.ListGDocPropertyReferenceDataTable listGDocPropertyReference = new GDocCommonData.ListGDocPropertyReferenceDataTable();

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

        #endregion

       #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84124"/> class.
        /// </summary>
        public F84124()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84124"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F84124(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.masterFormNo = masterform;
            this.formNo = formNo;
            this.pipeId = keyID;
            this.LocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LocationPictureBox.Height, this.LocationPictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84124"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// /// <param name="featureClassId">The feature class id.</param>
        public F84124(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.masterFormNo = masterform;
            this.formNo = formNo;
            this.pipeId = keyID;
            this.featureClassId = featureClassId;
            this.LocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LocationPictureBox.Height, this.LocationPictureBox.Width, tabText, red, green, blue);
        }
        #endregion

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
        /// Declare the event D84100_F84124_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84100_F84124_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_OnSave_SetKeyId;

        #endregion

       #region Property

        /// <summary>
        /// For form84124Control
        /// </summary>
        [CreateNew]
        public F84124Controller Form84124Control
        {
            get { return this.form84124Control as F84124Controller; }
            set { this.form84124Control = value; }
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
                if (this.slicePermissionField.newPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.ClearSanitaryPipeLocationControls();
                    this.SetNewComboIndex();
                    this.LockControls(true);
                    this.ControlLock(false);
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearSanitaryPipeForm();
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

                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    if (this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows.Count > 0)
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    if (this.slicePermissionField.editPermission)
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
                    if (this.slicePermissionField.editPermission)
                    {
                        this.SaveSanitaryPipeLocation();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CustomizeSanitaryPipeLocation();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                this.LockControls(true);
                this.CustomizeSanitaryPipeLocation();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                    this.FlagSliceForm = true;
                    this.flagLoadOnProcess = true;
                    this.pipeId = eventArgs.Data.SelectedKeyId;
                    this.CustomizeSanitaryPipeLocation();
                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription D84100_F84123_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84100_F84123_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                this.pipeId = eventArgs.Data;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

       #region Protected Methods

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
        #endregion

       #region Private Methods

        /// <summary>
        /// To Clear Sanitary Pipe Location Properties TextBoxs and Checked Boxs 
        /// </summary>
        private void ClearSanitaryPipeLocationControls()
        {
            this.DistrictProjectTextBox.Text = string.Empty;
            this.LocationNotesTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To Clear the entire Sanitary Pipe Location Form
        /// </summary>
        private void ClearSanitaryPipeForm()
        {
            this.ClearComboBox(this);
            this.ClearSanitaryPipeLocationControls();
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.AdministrativeAreaPanel.Enabled = lockControl;
            this.OperationalAreaPanel.Enabled = lockControl;
            this.GridPanel.Enabled = lockControl;
            this.DistrictProjectPanel.Enabled = lockControl;
            this.LocationNotesPanel.Enabled = lockControl;
        }

        /// <summary>
        /// Clears the All the combo boxs in the form.
        /// </summary>
        /// <param name="currentControl">The current control.</param>
        private void ClearComboBox(Control currentControl)
        {
            try
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
            catch (Exception ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
       
        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.DistrictProjectTextBox.LockKeyPress = controlLook;
            this.LocationNotesTextBox.LockKeyPress = controlLook;

            this.AdministrativeAreaComboBox.Enabled = !controlLook;
            this.OperationalAreaComboBox.Enabled = !controlLook;
            this.GridComboBox.Enabled = !controlLook;
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
            try
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
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the operational area combo box.
        /// </summary>
        private void LoadOperationalAreaComboBox()
        {
            try
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
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the grid combo box.
        /// </summary>
        private void LoadGridComboBox()
        {
            try
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
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            try
            {
                this.DistrictProjectTextBox.MaxLength = this.sanitaryPipeLocationData.GetSanitaryPipeLocation.District_ProjectColumn.MaxLength;
                this.LocationNotesTextBox.MaxLength = this.sanitaryPipeLocationData.GetSanitaryPipeLocation.LocationDescriptionColumn.MaxLength;

                this.AdministrativeAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
                this.OperationalAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
                this.GridComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
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
            try
            {
                this.listGDocPropertyReference.Clear();
                DataRow customRow = this.listGDocPropertyReference.NewRow();
                customRow[this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName] = string.Empty;
                customRow[this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName] = "0";
                this.listGDocPropertyReference.Rows.Add(customRow);

                ////to clear the this.gdocCommonData.ListGDocPropertyReference
                this.gdocCommonData.ListGDocPropertyReference.Clear();
                this.gdocCommonData = this.form84124Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
                this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
                this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F84124 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84124_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            ////Set the Max Length for editable fields
            this.SetMaxLength();
            this.CustomizeSanitaryPipeLocation();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Customizes the Sanitary pipe Location.
        /// </summary>
        private void CustomizeSanitaryPipeLocation()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.sanitaryPipeLocationData = this.form84124Control.WorkItem.F84124_GetSanitaryPipeLocation(this.pipeId, this.formNo);
                if (this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows.Count > 0)
                {
                    this.LocationNotesTextBox.Text = this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows[0][this.sanitaryPipeLocationData.GetSanitaryPipeLocation.LocationDescriptionColumn].ToString();
                    this.DistrictProjectTextBox.Text = this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows[0][this.sanitaryPipeLocationData.GetSanitaryPipeLocation.District_ProjectColumn].ToString();

                    this.administrativeAreaId = Convert.ToInt32(this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows[0][this.sanitaryPipeLocationData.GetSanitaryPipeLocation.AdministrativeAreaIDColumn]);
                    this.operationalAreaId = Convert.ToInt32(this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows[0][this.sanitaryPipeLocationData.GetSanitaryPipeLocation.OperationalAreaIDColumn]);
                    this.gridId = Convert.ToInt32(this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows[0][this.sanitaryPipeLocationData.GetSanitaryPipeLocation.GridIDColumn]);
                }
                else
                {
                    this.ClearSanitaryPipeForm();
                    this.LockControls(false);
                }

                ////To Load All The ComBoBoxs
                this.LoadAllComboBox();
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Saves the Sanitary Pipe location.
        /// </summary>
        private void SaveSanitaryPipeLocation()
        {
            try
            {
               string tempSanitaryPipeLocation;
                this.Cursor = Cursors.WaitCursor;

                F84124SanitaryPipeLocationData sanitaryPipeLocationData1 = new F84124SanitaryPipeLocationData();
                //this.sanitaryPipeLocationData.GetSanitaryPipeLocation.Rows.Clear();
                F84124SanitaryPipeLocationData.GetSanitaryPipeLocationRow dr = sanitaryPipeLocationData1.GetSanitaryPipeLocation.NewGetSanitaryPipeLocationRow();
                
                if (!string.IsNullOrEmpty(this.LocationNotesTextBox.Text.Trim()))
                {
                    dr.LocationDescription = this.LocationNotesTextBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.DistrictProjectTextBox.Text.Trim()))
                {
                    dr.District_Project = this.DistrictProjectTextBox.Text.Trim();
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

                sanitaryPipeLocationData1.GetSanitaryPipeLocation.Rows.Add(dr);
             
                tempSanitaryPipeLocation  = Utility.GetXmlString(sanitaryPipeLocationData1.GetSanitaryPipeLocation.Copy());
              
                this.pipeId = this.form84124Control.WorkItem.F84124_SaveSanitaryPipeLocation(this.pipeId, tempSanitaryPipeLocation, TerraScanCommon.UserId);

                ////to sent the this keyid(this.pipeId) 
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<int>(this.pipeId));
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
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
        /// Handles the KeyPress event of the LocationNotesTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void LocationNotesTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

       #region Events

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "Text Changed Events In Text Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }
    
        /// <summary>
        /// Handles the Click event of the WaterValveLocationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the MouseEnter event of the PropertiesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.SanitaryPipeLocationToolTip.SetToolTip(this.LocationPictureBox, Utility.GetFormNameSpace(this.Name));
         }

       #endregion

        private void AdministrativeAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        private void OperationalAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        private void GridComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }
    }
}
