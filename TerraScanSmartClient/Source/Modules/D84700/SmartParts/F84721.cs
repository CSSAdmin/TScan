//--------------------------------------------------------------------------------------------
// <copyright file="F84721.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84721.
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
    /// F84721 Class file
    /// </summary>
    public partial class F84721 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// valveId or keyId
        /// </summary>
        private int valveId;

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// controller F84721
        /// </summary>
        private F84721Controller form84721Control;

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
        private F84721WaterValvePropertiesData waterValvePropertiesData = new F84721WaterValvePropertiesData();

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
        /// businessId
        /// </summary>
        private int businessId;

        /// <summary>
        /// manufacturerId
        /// </summary>
        private int manufacturerId;

        /// <summary>
        /// valveTypeId
        /// </summary>
        private int valveTypeId;

        /// <summary>
        /// regulationTypeId
        /// </summary>
        private int regulationTypeId;

        /// <summary>
        /// lifecycleStatusId
        /// </summary>
        private int lifecycleStatusId;

        /// <summary>
        /// connectionTypeId
        /// </summary>
        private int connectionTypeId;

        /// <summary>
        /// valveSeatTypeId
        /// </summary>
        private int valveSeatTypeId;

        /// <summary>
        /// accessTypeId
        /// </summary>
        private int accessTypeId;

        /// <summary>
        /// waterTypeId
        /// </summary>
        private int waterTypeId;

        /// <summary>
        /// userId
        /// </summary>
        private int userId;

        /// <summary>
        /// mainDiameter
        /// </summary>
        private string mainDiameter;

        #endregion Variable For CoboBox Value Members

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        public F84721()
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
        public F84721(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.valveId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterValvePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(268, 42, tabText, red, green, blue);
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
        public F84721(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.valveId = keyID;
            this.featureClassId = featureClassId;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterValvePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(268, 42, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event D84700_F84721_OnSave_GetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84721_OnSave_GetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_GetKeyId;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F8050Control
        /// </summary>
        [CreateNew]
        public F84721Controller Form84721Control
        {
            get { return this.form84721Control as F84721Controller; }
            set { this.form84721Control = value; }
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
                    this.ClearWaterValveControls();
                    this.SetNewComboIndex();
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CIDTextBox.Focus();
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
                this.CustomizeWaterValveProperties();
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
                        this.SaveWaterValveProperties();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CustomizeWaterValveProperties();
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
                        if (this.waterValvePropertiesData.GetWaterValveProperties.Rows.Count > 0)
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

                if (this.waterValvePropertiesData.GetWaterValveProperties.Rows.Count > 0)
                {
                    this.ParentForm.ActiveControl = this.CIDTextBox;
                    this.ParentForm.ActiveControl.Focus();
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
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this.PermissionFiled.deletePermission)
                {
                    this.DeleteWaterValveProperties();
                    //this.NullRecords = true;
                    SliceFormCloseAlert sliceFormCloseAlert;
                    sliceFormCloseAlert.FormNo = this.masterFormNo;
                    sliceFormCloseAlert.FlagFormClose = true;
                    this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
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
                    this.LoadWaterValveProperties();
                    if (this.waterValvePropertiesData.GetWaterValveProperties.Rows.Count > 0)
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
        /// Event Subscription D84700_F84722_OnSave_SetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84722_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_SetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
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

        #endregion Protected methods

        #region Methods

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.CIDTextBox.MaxLength = this.waterValvePropertiesData.GetWaterValveProperties.CIDColumn.MaxLength;
            this.ModelNumberTextBox.MaxLength = this.waterValvePropertiesData.GetWaterValveProperties.ModelNumberColumn.MaxLength;
            this.SerialNumberTextBox.MaxLength = this.waterValvePropertiesData.GetWaterValveProperties.SerialNumberColumn.MaxLength;
            this.CommentTextBox.MaxLength = this.waterValvePropertiesData.GetWaterValveProperties.CommentColumn.MaxLength;

            this.BusinessComboBox.MaxLength = this.gdocCommonData.ListGDocBusiness.BusinessNameColumn.MaxLength;
            this.ManufacturerComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.ValveTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.RegulationTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.LifeCycleStatusComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.ConnectionTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.ValveSeatTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.WaterTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.AccessTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.ValveUseComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.MainDiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
        }

        /// <summary>
        /// To Clear Water Valve Properties TextBoxs and Checked Boxs 
        /// </summary>
        private void ClearWaterValveControls()
        {
            this.ValveIDTextBox.Text = string.Empty;
            this.CIDTextBox.Text = string.Empty;
            this.ModelNumberTextBox.Text = string.Empty;
            this.SerialNumberTextBox.Text = string.Empty;
            this.PercentOpenTextBox.Text = string.Empty;
            this.TurnstoCloseTextBox.Text = string.Empty;
            this.PressureSettingTextBox.Text = string.Empty;
            this.HydrostaticPressureTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;

            this.EnableCheckBox.Checked = false;
            this.PrivateCheckBox.Checked = false;
            this.CurrentlyOpenCheckBox.Checked = false;
            this.NormallyOpenCheckBox.Checked = false;
            this.MachineRunCheckBox.Checked = false;
            this.LongKeyCheckBox.Checked = false;
            this.ClockwisetoCloseCheckBox.Checked = false;
            this.BypassValveCheckBox.Checked = false;
            this.MotorizedCheckBox.Checked = false;
        }

        /// <summary>
        /// To Clear the entire Water Valve Form
        /// </summary>
        private void ClearWaterValve()
        {
            this.ClearComboBox(this);
            this.ClearWaterValveControls();
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
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.ValveIDPanel.Enabled = lockControl;
            this.CIDPanel.Enabled = lockControl;
            this.EnablePanel.Enabled = lockControl;
            this.PrivatePanel.Enabled = lockControl;
            this.BusinessPanel.Enabled = lockControl;
            this.ManufacturerPanel.Enabled = lockControl;
            this.ModelNumberPanel.Enabled = lockControl;
            this.SerialNumberPanel.Enabled = lockControl;
            this.ValveTypePanel.Enabled = lockControl;
            this.MainDiameterPanel.Enabled = lockControl;
            this.CurrentlyOpenPanel.Enabled = lockControl;
            this.NormallyOpenPanel.Enabled = lockControl;
            this.PercentOpenPanel.Enabled = lockControl;
            this.MachineRunPanel.Enabled = lockControl;
            this.LongKeyPanel.Enabled = lockControl;
            this.ClockwisetoClosePanel.Enabled = lockControl;
            this.TurnstoCloseLablePanel.Enabled = lockControl;
            this.PressureSettingPanel.Enabled = lockControl;
            this.HydrostaticPressurePanel.Enabled = lockControl;
            this.BypassValvePanel.Enabled = lockControl;
            this.RegulationTypePanel.Enabled = lockControl;
            this.MotorizedPanel.Enabled = lockControl;
            this.LifeCycleStatusPanel.Enabled = lockControl;
            this.ConnectionTypePanel.Enabled = lockControl;
            this.ValveSeatTypePanel.Enabled = lockControl;
            this.WaterTypePanel.Enabled = lockControl;
            this.AccessTypePanel.Enabled = lockControl;
            this.ValveUsePanel.Enabled = lockControl;
            this.CommentPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            ////this.ValveIDTextBox.LockKeyPress = controlLook;
            this.CIDPanel.Enabled = !controlLook;
            this.ValveIDPanel.Enabled = !controlLook; 
            this.CIDTextBox.LockKeyPress = controlLook;
            this.ModelNumberTextBox.LockKeyPress = controlLook;
            this.SerialNumberTextBox.LockKeyPress = controlLook;
            this.PercentOpenTextBox.LockKeyPress = controlLook;
            this.TurnstoCloseTextBox.LockKeyPress = controlLook;
            this.PressureSettingTextBox.LockKeyPress = controlLook;
            this.HydrostaticPressureTextBox.LockKeyPress = controlLook;
            this.CommentTextBox.LockKeyPress = controlLook;

            this.EnablePanel.Enabled = !controlLook;
            this.EnableCheckBox.Enabled = !controlLook;
            this.PrivatePanel.Enabled = !controlLook; 
            this.PrivateCheckBox.Enabled = !controlLook;
            this.CurrentlyOpenPanel.Enabled = !controlLook; 
            this.CurrentlyOpenCheckBox.Enabled = !controlLook;
            this.NormallyOpenPanel.Enabled = !controlLook; 
            this.NormallyOpenCheckBox.Enabled = !controlLook;
            this.MachineRunPanel.Enabled = !controlLook; 
            this.MachineRunCheckBox.Enabled = !controlLook;
            this.LongKeyPanel.Enabled = !controlLook; 
            this.LongKeyCheckBox.Enabled = !controlLook;
            this.ClockwisetoClosePanel.Enabled = !controlLook; 
            this.ClockwisetoCloseCheckBox.Enabled = !controlLook;
            this.BypassValvePanel.Enabled = !controlLook; 
            this.BypassValveCheckBox.Enabled = !controlLook;
            this.MotorizedPanel.Enabled = !controlLook; 
            this.MotorizedCheckBox.Enabled = !controlLook;

            this.BusinessPanel.Enabled = !controlLook; 
            this.BusinessComboBox.Enabled = !controlLook;
            this.ManufacturerPanel.Enabled = !controlLook; 
            this.ManufacturerComboBox.Enabled = !controlLook;
            this.ValveTypePanel.Enabled = !controlLook; 
            this.ValveTypeComboBox.Enabled = !controlLook;
            this.MainDiameterPanel.Enabled = !controlLook; 
            this.MainDiameterComboBox.Enabled = !controlLook;
            this.RegulationTypePanel.Enabled = !controlLook; 
            this.RegulationTypeComboBox.Enabled = !controlLook;
            this.LifeCycleStatusPanel.Enabled = !controlLook; 
            this.LifeCycleStatusComboBox.Enabled = !controlLook;
            this.ConnectionTypePanel.Enabled = !controlLook; 
            this.ConnectionTypeComboBox.Enabled = !controlLook;
            this.ValveSeatTypePanel.Enabled = !controlLook; 
            this.ValveSeatTypeComboBox.Enabled = !controlLook;
            this.WaterTypePanel.Enabled = !controlLook; 
            this.WaterTypeComboBox.Enabled = !controlLook;
            this.AccessTypePanel.Enabled = !controlLook; 
            this.AccessTypeComboBox.Enabled = !controlLook;
            this.ValveUsePanel.Enabled = !controlLook; 
            this.ValveUseComboBox.Enabled = !controlLook;

            this.ModelNumberPanel.Enabled = !controlLook;
            this.ModelNumberTextBox.Enabled = !controlLook;
            this.SerialNumberPanel.Enabled = !controlLook;
            this.SerialNumberTextBox.Enabled = !controlLook;
            this.PercentOpenPanel.Enabled = !controlLook;
            this.PercentOpenTextBox.Enabled = !controlLook;
            this.TurnstoCloseLablePanel.Enabled = !controlLook;
            this.TurnstoCloseTextBox.Enabled = !controlLook;
            this.PressureSettingPanel.Enabled = !controlLook;
            this.PressureSettingTextBox.Enabled = !controlLook;
            this.HydrostaticPressurePanel.Enabled = !controlLook;
            this.HydrostaticPressureTextBox.Enabled = !controlLook;
            this.CommentPanel.Enabled = !controlLook;
            this.CommentTextBox.Enabled = !controlLook;  
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
        /// Customizes the water valve properties.
        /// </summary>
        private void CustomizeWaterValveProperties()
        {
            this.waterValvePropertiesData = this.form84721Control.WorkItem.F84721_GetWaterValveProperties(this.valveId);
            if (this.waterValvePropertiesData.GetWaterValveProperties.Rows.Count > 0)
            {
                ////Used to set the focus
                this.CIDTextBox.Focus();

                this.ValveIDTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.FeatureIDColumn].ToString();
                this.CIDTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.CIDColumn].ToString();
                this.ModelNumberTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.ModelNumberColumn].ToString();
                this.SerialNumberTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.SerialNumberColumn].ToString();
                this.PercentOpenTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.PercentOpenColumn].ToString();
                this.TurnstoCloseTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.TurnsToCloseColumn].ToString();
                this.PressureSettingTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.PressureSettingColumn].ToString();
                this.HydrostaticPressureTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.HydrostaticPressueColumn].ToString();
                this.CommentTextBox.Text = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.CommentColumn].ToString();

                this.EnableCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsEnabledColumn].ToString());
                this.PrivateCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsPrivateColumn].ToString());
                this.CurrentlyOpenCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsCurrentlyOpenColumn].ToString());
                this.NormallyOpenCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsNormallyOpenColumn].ToString());
                this.MachineRunCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsMachineRunColumn].ToString());
                this.LongKeyCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsLongKeyColumn].ToString());
                this.ClockwisetoCloseCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsClockwiseToCloseColumn].ToString());
                this.BypassValveCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsBypassValveColumn].ToString());
                this.MotorizedCheckBox.Checked = Convert.ToBoolean(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.IsMotorizedColumn].ToString());

                this.businessId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.BusinessIDColumn]);
                this.manufacturerId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.ManufacturerIDColumn]);
                this.valveTypeId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.ValveTypeIDColumn]);
                this.regulationTypeId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.RegulationTypeIDColumn]);
                this.lifecycleStatusId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.LifecycleStatusIDColumn]);
                this.connectionTypeId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.ConnectionTypeIDColumn]);
                this.valveSeatTypeId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.ValveSeatTypeIDColumn]);
                this.waterTypeId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.WaterTypeIDColumn]);
                this.accessTypeId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.AccessTypeIDColumn]);
                this.userId = Convert.ToInt32(this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.UserIDColumn]);
                this.mainDiameter = this.waterValvePropertiesData.GetWaterValveProperties.Rows[0][this.waterValvePropertiesData.GetWaterValveProperties.DiameterColumn].ToString();

                ////Used to set the focus
                this.CIDTextBox.Focus();
            }
            else
            {
                this.ClearWaterValve();
                this.LockControls(false);
            }

            ////To Load All The ComBoBoxs
            this.LoadAllCombOBox();
        }

        /// <summary>
        /// Saves the water valve properties.
        /// </summary>
        private void SaveWaterValveProperties()
        {
            int tempFeatureId;
            int keyId;
            decimal tempTurnstoClose;
            decimal tempPercentOpen;
            decimal tempPressureSetting;
            decimal tempHydrostaticPressure;

            F84721WaterValvePropertiesData waterValvePropertiesData1 = new F84721WaterValvePropertiesData();
            //this.waterValvePropertiesData.GetWaterValveProperties.Rows.Clear();
            F84721WaterValvePropertiesData.GetWaterValvePropertiesRow dr = waterValvePropertiesData1.GetWaterValveProperties.NewGetWaterValvePropertiesRow();

            if (!string.IsNullOrEmpty(this.ValveIDTextBox.Text.Trim()))
            {
                int.TryParse(this.ValveIDTextBox.Text.Trim(), out tempFeatureId);
                keyId = tempFeatureId;
            }
            else
            {
                ////When the ValveId text box is empty then it is 0 to be sent to db                   
                keyId = 0;
            }

            dr.FeatureClassID = this.featureClassId;

            if (!string.IsNullOrEmpty(this.CIDTextBox.Text.Trim()))
            {
                dr.CID = this.CIDTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ModelNumberTextBox.Text.Trim()))
            {
                dr.ModelNumber = this.ModelNumberTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.SerialNumberTextBox.Text.Trim()))
            {
                dr.SerialNumber = this.SerialNumberTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.PercentOpenTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.PercentOpenTextBox.Text.Trim(), out tempPercentOpen);
                dr.PercentOpen = tempPercentOpen;
            }

            if (!string.IsNullOrEmpty(this.TurnstoCloseTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.TurnstoCloseTextBox.Text.Trim(), out tempTurnstoClose);
                dr.TurnsToClose = tempTurnstoClose;
            }

            if (!string.IsNullOrEmpty(this.PressureSettingTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.PressureSettingTextBox.Text.Trim(), out tempPressureSetting);
                dr.PressureSetting = tempPressureSetting;
            }

            if (!string.IsNullOrEmpty(this.HydrostaticPressureTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.HydrostaticPressureTextBox.Text.Trim(), out tempHydrostaticPressure);
                dr.HydrostaticPressue = tempHydrostaticPressure;
            }

            if (!string.IsNullOrEmpty(this.CommentTextBox.Text.Trim()))
            {
                dr.Comment = this.CommentTextBox.Text.Trim();
            }

            dr.IsEnabled = this.EnableCheckBox.Checked;
            dr.IsPrivate = this.PrivateCheckBox.Checked;
            dr.IsCurrentlyOpen = this.CurrentlyOpenCheckBox.Checked;
            dr.IsNormallyOpen = this.NormallyOpenCheckBox.Checked;
            dr.IsMachineRun = this.MachineRunCheckBox.Checked;
            dr.IsLongKey = this.LongKeyCheckBox.Checked;
            dr.IsClockwiseToClose = this.ClockwisetoCloseCheckBox.Checked;
            dr.IsBypassValve = this.BypassValveCheckBox.Checked;
            dr.IsMotorized = this.MotorizedCheckBox.Checked;

            if (!string.IsNullOrEmpty(this.BusinessComboBox.Text.Trim()))
            {
                dr.BusinessID = Convert.ToInt32(this.BusinessComboBox.SelectedValue);
            }
            else
            {
                dr.BusinessID = 0;
            }

            if (!string.IsNullOrEmpty(this.ManufacturerComboBox.Text.Trim()))
            {
                dr.ManufacturerID = Convert.ToInt32(this.ManufacturerComboBox.SelectedValue);
            }
            else
            {
                dr.ManufacturerID = 0;
            }

            if (!string.IsNullOrEmpty(this.ValveTypeComboBox.Text.Trim()))
            {
                dr.ValveTypeID = Convert.ToInt32(this.ValveTypeComboBox.SelectedValue);
            }
            else
            {
                dr.ValveTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.MainDiameterComboBox.Text.Trim()))
            {
                dr.Diameter = Convert.ToDecimal(this.MainDiameterComboBox.Text.Trim());
            }

            if (!string.IsNullOrEmpty(this.RegulationTypeComboBox.Text.Trim()))
            {
                dr.RegulationTypeID = Convert.ToInt32(this.RegulationTypeComboBox.SelectedValue);
            }
            else
            {
                dr.RegulationTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.LifeCycleStatusComboBox.Text.Trim()))
            {
                dr.LifecycleStatusID = Convert.ToInt32(this.LifeCycleStatusComboBox.SelectedValue);
            }
            else
            {
                dr.LifecycleStatusID = 0;
            }

            if (!string.IsNullOrEmpty(this.ConnectionTypeComboBox.Text.Trim()))
            {
                dr.ConnectionTypeID = Convert.ToInt32(this.ConnectionTypeComboBox.SelectedValue);
            }
            else
            {
                dr.ConnectionTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.ValveSeatTypeComboBox.Text.Trim()))
            {
                dr.ValveSeatTypeID = Convert.ToInt32(this.ValveSeatTypeComboBox.SelectedValue);
            }
            else
            {
                dr.ValveSeatTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.WaterTypeComboBox.Text.Trim()))
            {
                dr.WaterTypeID = Convert.ToInt32(this.WaterTypeComboBox.SelectedValue);
            }
            else
            {
                dr.WaterTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.AccessTypeComboBox.Text.Trim()))
            {
                dr.AccessTypeID = Convert.ToInt32(this.AccessTypeComboBox.SelectedValue);
            }
            else
            {
                dr.AccessTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.ValveUseComboBox.Text.Trim()))
            {
                dr.UserID = Convert.ToInt32(this.ValveUseComboBox.SelectedValue);
            }
            else
            {
                dr.UserID = 0;
            }

            waterValvePropertiesData1.GetWaterValveProperties.Rows.Add(dr);
            this.valveId = this.form84721Control.WorkItem.F84721_SaveWaterValveProperties(keyId, (Utility.GetXmlString(waterValvePropertiesData1.GetWaterValveProperties.Copy())), TerraScanCommon.UserId);
            ////to sent the this returned keyid(this.valveId) to 84722
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.valveId;
            this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            ////to reload the form with the current keyid(this.valveId)afther save
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        /// <summary>
        /// Deletes the water valve properties.
        /// </summary>
        private void DeleteWaterValveProperties()
        {
            this.form84721Control.WorkItem.F84721_DeleteWaterValveProperties(this.valveId, TerraScanCommon.UserId);
        }

        /// <summary>
        /// Loads all combO boxs.
        /// </summary>
        private void LoadAllCombOBox()
        {
            this.LoadBusinessComboBox();
            this.LoadMainDiameterComboBox();
            this.LoadManufacturerComboBox();
            this.LoadValveTypeComboBox();
            this.LoadRegulationTypeComboBox();
            this.LoadLifeCycleStatusComboBox();
            this.LoadConnectionTypeComboBox();
            this.LoadValveSeatTypeComboBox();
            this.LoadWaterTypeComboBox();
            this.LoadAccessTypeComboBox();
            this.LoadValveUseComboBox();
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
            this.gdocCommonData = this.form84721Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
            this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
            this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
        }

        /// <summary>
        /// Loads the business combo box.
        /// </summary>
        private void LoadBusinessComboBox()
        {
            GDocCommonData.ListGDocBusinessDataTable listGDocBusiness = new GDocCommonData.ListGDocBusinessDataTable();

            DataRow customRow = listGDocBusiness.NewRow();
            listGDocBusiness.Clear();
            customRow[this.gdocCommonData.ListGDocBusiness.BusinessNameColumn.ColumnName] = string.Empty;
            customRow[this.gdocCommonData.ListGDocBusiness.BusinessIDColumn.ColumnName] = "0";
            listGDocBusiness.Rows.Add(customRow);

            this.gdocCommonData = this.form84721Control.WorkItem.F8000_GetGDocBusiness();
            listGDocBusiness.Merge(this.gdocCommonData.ListGDocBusiness);

            if (this.gdocCommonData.ListGDocBusiness.Rows.Count > 0)
            {
                this.BusinessComboBox.DataSource = listGDocBusiness;
                this.BusinessComboBox.DisplayMember = this.gdocCommonData.ListGDocBusiness.BusinessNameColumn.ColumnName;
                this.BusinessComboBox.ValueMember = this.gdocCommonData.ListGDocBusiness.BusinessIDColumn.ColumnName;
                if (this.businessId > 0)
                {
                    this.BusinessComboBox.SelectedValue = this.businessId;
                }
                else
                {
                    this.BusinessComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the main diameter combo box.
        /// </summary>
        private void LoadMainDiameterComboBox()
        {
            GDocCommonData.ListGDocDiameterDataTable listGDocDiameter = new GDocCommonData.ListGDocDiameterDataTable();

            DataRow customRow = listGDocDiameter.NewRow();
            listGDocDiameter.Clear();
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName] = DBNull.Value;
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName] = "0";
            listGDocDiameter.Rows.Add(customRow);

            this.gdocCommonData = this.form84721Control.WorkItem.F8000_GetGDocDiameter(this.featureClassId);
            listGDocDiameter.Merge(this.gdocCommonData.ListGDocDiameter);

            if (this.gdocCommonData.ListGDocDiameter.Rows.Count > 0)
            {
                this.MainDiameterComboBox.DataSource = listGDocDiameter;
                this.MainDiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.MainDiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.mainDiameter))
                {
                    this.MainDiameterComboBox.Text = this.mainDiameter;
                }
                else
                {
                    this.MainDiameterComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the manufacturer combo box.
        /// </summary>
        private void LoadManufacturerComboBox()
        {
            this.CreateTempListGDocPropertyReference("ManufacturerID");

            DataTable manufacturerComboDataTable = new DataTable();
            manufacturerComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.ManufacturerComboBox.DataSource = manufacturerComboDataTable;
                this.ManufacturerComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.ManufacturerComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.manufacturerId > 0)
                {
                    this.ManufacturerComboBox.SelectedValue = this.manufacturerId;
                }
                else
                {
                    this.ManufacturerComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the valve type combo box.
        /// </summary>
        private void LoadValveTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("ValveTypeID");

            DataTable valveTypeComboDatatable = new DataTable();
            valveTypeComboDatatable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.ValveTypeComboBox.DataSource = valveTypeComboDatatable;
                this.ValveTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.ValveTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.valveTypeId > 0)
                {
                    this.ValveTypeComboBox.SelectedValue = this.valveTypeId;
                }
                else
                {
                    this.ValveTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the regulation type combo box.
        /// </summary>
        private void LoadRegulationTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("RegulationTypeID");

            DataTable regulationTypeComboDataTable = new DataTable();
            regulationTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.RegulationTypeComboBox.DataSource = regulationTypeComboDataTable;
                this.RegulationTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.RegulationTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.regulationTypeId > 0)
                {
                    this.RegulationTypeComboBox.SelectedValue = this.regulationTypeId;
                }
                else
                {
                    this.RegulationTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the life cycle status combo box.
        /// </summary>
        private void LoadLifeCycleStatusComboBox()
        {
            this.CreateTempListGDocPropertyReference("LifecycleStatusID");

            DataTable lifeCycleStatusCombDataTable = new DataTable();
            lifeCycleStatusCombDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.LifeCycleStatusComboBox.DataSource = lifeCycleStatusCombDataTable;
                this.LifeCycleStatusComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.LifeCycleStatusComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.lifecycleStatusId > 0)
                {
                    this.LifeCycleStatusComboBox.SelectedValue = this.lifecycleStatusId;
                }
                else
                {
                    this.LifeCycleStatusComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the connection type combo box.
        /// </summary>
        private void LoadConnectionTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("ConnectionTypeID");

            DataTable connectionTypeComboDataTable = new DataTable();
            connectionTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.ConnectionTypeComboBox.DataSource = connectionTypeComboDataTable;
                this.ConnectionTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.ConnectionTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.connectionTypeId > 0)
                {
                    this.ConnectionTypeComboBox.SelectedValue = this.connectionTypeId;
                }
                else
                {
                    this.ConnectionTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the valve seat type combo box.
        /// </summary>
        private void LoadValveSeatTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("ValveSeatTypeID");

            DataTable valveSeatTypeComboDataTable = new DataTable();
            valveSeatTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.ValveSeatTypeComboBox.DataSource = valveSeatTypeComboDataTable;
                this.ValveSeatTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.ValveSeatTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.valveSeatTypeId > 0)
                {
                    this.ValveSeatTypeComboBox.SelectedValue = this.valveSeatTypeId;
                }
                else
                {
                    this.ValveSeatTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the water type combo box.
        /// </summary>
        private void LoadWaterTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("WaterTypeID");

            DataTable waterTypeComboDataTable = new DataTable();
            waterTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.WaterTypeComboBox.DataSource = waterTypeComboDataTable;
                this.WaterTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.WaterTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.waterTypeId > 0)
                {
                    this.WaterTypeComboBox.SelectedValue = this.waterTypeId;
                }
                else
                {
                    this.WaterTypeComboBox.SelectedIndex = 0;
                }
            }

        }

        /// <summary>
        /// Loads the access type combo box.
        /// </summary>
        private void LoadAccessTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("AccessTypeID");

            DataTable accessTypeComboDataTable = new DataTable();
            accessTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.AccessTypeComboBox.DataSource = accessTypeComboDataTable;
                this.AccessTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.AccessTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.accessTypeId > 0)
                {
                    this.AccessTypeComboBox.SelectedValue = this.accessTypeId;
                }
                else
                {
                    this.AccessTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the valve use combo box.
        /// </summary>
        private void LoadValveUseComboBox()
        {
            this.CreateTempListGDocPropertyReference("UserID");

            DataTable valveUseComboDataTable = new DataTable();
            valveUseComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.ValveUseComboBox.DataSource = valveUseComboDataTable;
                this.ValveUseComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.ValveUseComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.userId > 0)
                {
                    this.ValveUseComboBox.SelectedValue = this.userId;
                }
                else
                {
                    this.ValveUseComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.BusinessComboBox.SelectedIndex = -1;
            this.MainDiameterComboBox.SelectedIndex = -1;
            this.ManufacturerComboBox.SelectedIndex = -1;
            this.ValveTypeComboBox.SelectedIndex = -1;
            this.RegulationTypeComboBox.SelectedIndex = -1;
            this.LifeCycleStatusComboBox.SelectedIndex = -1;
            this.ConnectionTypeComboBox.SelectedIndex = -1;
            this.ValveSeatTypeComboBox.SelectedIndex = -1;
            this.WaterTypeComboBox.SelectedIndex = -1;
            this.AccessTypeComboBox.SelectedIndex = -1;
            this.ValveUseComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// To Load the water valve properties.
        /// </summary>
        private void LoadWaterValveProperties()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeWaterValveProperties();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
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
        /// Handles the Click event of the WaterValvePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterValvePictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the WaterValvePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterValvePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocWaterValveToolTip.SetToolTip(this.WaterValvePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F84721 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84721_Load(object sender, EventArgs e)
        {
            try
            {
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.LoadWaterValveProperties();
                if (this.waterValvePropertiesData.GetWaterValveProperties.Rows.Count > 0)
                {
                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
                this.CIDPanel.Focus();
                this.CIDTextBox.Focus();                

                if (this.waterValvePropertiesData.GetWaterValveProperties.Rows.Count > 0)
                {
                    this.ParentForm.ActiveControl = this.CIDTextBox;
                    this.ParentForm.ActiveControl.Focus();
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
        /// Handles the KeyPress event of the CommentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CommentTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion Events

        private void BusinessComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ManufacturerComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ValveTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void MainDiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void RegulationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void LifeCycleStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ConnectionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ValveSeatTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void WaterTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void ValveUseComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void AccessTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
