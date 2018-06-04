//--------------------------------------------------------------------------------------------
// <copyright file="F84723.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84723.
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
    /// F84723 class file
    /// </summary>
    public partial class F84723 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// hydrantId
        /// </summary>
        private int hydrantId;

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// controller F84723
        /// </summary>
        private F84723Controller form84723Control;

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
        private F84723WaterHydrantPropertiesData waterHydrantPropertiesData = new F84723WaterHydrantPropertiesData();

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
        /// Instance for listGDocPropertyReference Datatable
        /// </summary>
        private GDocCommonData.ListGDocPropertyReferenceDataTable listGDocPropertyReference = new GDocCommonData.ListGDocPropertyReferenceDataTable();

        /// <summary>
        /// instance for listGDocDiameter datatable
        /// </summary>
        private GDocCommonData.ListGDocDiameterDataTable listGDocDiameterDataTable = new GDocCommonData.ListGDocDiameterDataTable();

        /// <summary>
        /// No of rows present in the ListPropertyReference Datatable returned from DataBase
        /// </summary>
        private int gdocPropertyReferenceRowsCount;

        /// <summary>
        /// No of rows present in the listDiameter Datatable returned from DataBase
        /// </summary>
        private int gdocDiameterRowsCount;

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
        /// mainDiameter
        /// </summary>
        private string mainDiameter;

        /// <summary>
        /// barrelDiameter
        /// </summary>
        private string barrelDiameter;

        /// <summary>
        /// waterTypeId
        /// </summary>
        private int waterTypeId;

        /// <summary>
        /// userId
        /// </summary>
        private int userId;

        /// <summary>
        /// lifecycleStatusId
        /// </summary>
        private int lifecycleStatusId;

        /// <summary>
        /// nozzelDiameter1
        /// </summary>
        private string nozzelDiameter1;

        /// <summary>
        /// nozzelDiameter2
        /// </summary>
        private string nozzelDiameter2;

        /// <summary>
        /// nozzelDiameter3
        /// </summary>
        private string nozzelDiameter3;

        /// <summary>
        /// nozzelDiameter4
        /// </summary>
        private string nozzelDiameter4;

        #endregion Variable For CoboBox Value Members

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84723"/> class.
        /// </summary>
        public F84723()
        {
            this.InitializeComponent();
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
        public F84723(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.hydrantId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterHydrantPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(235, 42, tabText, red, green, blue);
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
        public F84723(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.hydrantId = keyID;
            this.featureClassId = featureClassId;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterHydrantPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(235, 42, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

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
        /// For F84723Control
        /// </summary>
        [CreateNew]
        public F84723Controller Form84723Control
        {
            get { return this.form84723Control as F84723Controller; }
            set { this.form84723Control = value; }
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
                    this.ClearWaterHydrantControls();
                    this.SetNewComboIndex();
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CIDTextBox.Focus();
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearWaterHydrantControls();
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
                this.CustomizeWaterHydrantProperties();
                // Coding for the issue 1113[Cancel time focus should not be in comments field].
                this.HydrantCommentPanel.BackColor = Color.White;   
                this.CommentTextBox.BackColor = Color.White;   
                this.CIDTextBox.Focus();
                // Ends here 1113
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
                        this.SaveWaterHydrantProperties();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CustomizeWaterHydrantProperties();
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

                    if (this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows.Count > 0)
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

                if (this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows.Count > 0)
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
                    this.hydrantId = eventArgs.Data.SelectedKeyId;
                    this.LoadWaterHydrantProperties();
                    if (this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows.Count > 0)
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
                    this.DeleteWaterHydranteProperties();

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
                    this.hydrantId = eventArgs.Data.SelectedKeyId;
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
            this.CIDTextBox.MaxLength = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.CIDColumn.MaxLength;
            this.ModelNumberTextBox.MaxLength = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.ModelNumberColumn.MaxLength;
            this.SerialNumberTextBox.MaxLength = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.SerialNumberColumn.MaxLength;
            this.CommentTextBox.MaxLength = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.CommentColumn.MaxLength;

            this.BusinessComboBox.MaxLength = this.gdocCommonData.ListGDocBusiness.BusinessNameColumn.MaxLength;
            this.ManufacturerComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.MainDiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.BarrelDiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.WaterTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.HydrentUseComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.LifecycleStatusComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.Nozzle1DiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.Nozzle2DiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.Nozzle3DiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.Nozzle4DiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
        }

        /// <summary>
        /// Clears the water hydrant controls.
        /// </summary>
        private void ClearWaterHydrantControls()
        {
            this.HydrantIDTextBox.Text = string.Empty;
            this.CIDTextBox.Text = string.Empty;
            this.ModelNumberTextBox.Text = string.Empty;
            this.SerialNumberTextBox.Text = string.Empty;
            this.MainValveTextBox.Text = string.Empty;
            this.MainValveLinkLabel.Text = string.Empty;
            this.PressureSettingTextBox.Text = string.Empty;
            this.HydrostaticPressureTextBox.Text = string.Empty;
            this.NozzlesTextBox.Text = string.Empty;
            this.ValveOpeningTextBox.Text = string.Empty;
            this.TurnstoCloseTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;

            this.HydrantEnableCheckBox.Checked = false;
            this.PrivateCheckBox.Checked = false;
            this.DeadEndCheckBox.Checked = false;
            this.ClockwisetoCloseCheckBox.Checked = false;
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
        /// Clears the entire water hydrant Form.
        /// </summary>
        private void ClearWaterHydrantProperties()
        {
            this.ClearWaterHydrantControls();
            this.ClearComboBox(this);
        }

        /// <summary>
        /// To Disable All the Panels int the form
        /// </summary>
        /// <param name="lockControl">boolean Value</param>
        private void LockControls(bool lockControl)
        {
            this.HydrantIDPanel.Enabled = lockControl;
            this.CIDPanel.Enabled = lockControl;
            this.HydrantEnablePanel.Enabled = lockControl;
            this.PrivatePanel.Enabled = lockControl;
            this.BusinessPanel.Enabled = lockControl;
            this.ManufacturerPanel.Enabled = lockControl;
            this.ModelNumberPanel.Enabled = lockControl;
            this.SerialNumberPanel.Enabled = lockControl;
            this.MainValvePanel.Enabled = lockControl;
            this.DeadEndPanel.Enabled = lockControl;
            this.PressureSettingPanel.Enabled = lockControl;
            this.HydrostaticPressurePanel.Enabled = lockControl;
            this.NozzlesPanel.Enabled = lockControl;
            this.ValveOpeningPanel.Enabled = lockControl;
            this.ClockwisetoClosePanel.Enabled = lockControl;
            this.TurnstoClosePanel.Enabled = lockControl;
            this.MainDiameterPanel.Enabled = lockControl;
            this.BarrelDiameterPanel.Enabled = lockControl;
            this.WaterTypePanel.Enabled = lockControl;
            this.HydrentUsePanel.Enabled = lockControl;
            this.LifecycleStatusPanel.Enabled = lockControl;
            this.Nozzle1DiameterPanel.Enabled = lockControl;
            this.Nozzle2DiameterPanel.Enabled = lockControl;
            this.Nozzle3DiameterPanel.Enabled = lockControl;
            this.Nozzle4DiameterPanel.Enabled = lockControl;
            this.HydrantCommentPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">boolean Value</param>
        private void ControlLock(bool controlLook)
        {
            this.CIDPanel.Enabled = !controlLook;  
            this.CIDTextBox.LockKeyPress = controlLook;
            this.ModelNumberPanel.Enabled = !controlLook; 
            this.ModelNumberTextBox.LockKeyPress = controlLook;
            this.SerialNumberPanel.Enabled = !controlLook; 
            this.SerialNumberTextBox.LockKeyPress = controlLook;
            this.MainValvePanel.Enabled = !controlLook; 
            this.MainValveTextBox.LockKeyPress = controlLook;
            this.PressureSettingPanel.Enabled = !controlLook; 
            this.PressureSettingTextBox.LockKeyPress = controlLook;
            this.HydrostaticPressurePanel.Enabled = !controlLook; 
            this.HydrostaticPressureTextBox.LockKeyPress = controlLook;
            this.NozzlesPanel.Enabled = !controlLook; 
            this.NozzlesTextBox.LockKeyPress = controlLook;
            this.ValveOpeningPanel.Enabled = !controlLook; 
            this.ValveOpeningTextBox.LockKeyPress = controlLook;
            this.TurnstoClosePanel.Enabled = !controlLook; 
            this.TurnstoCloseTextBox.LockKeyPress = controlLook;
            this.CommentTextBox.LockKeyPress = controlLook;

            this.HydrantEnablePanel.Enabled = !controlLook; 
            this.HydrantEnableCheckBox.Enabled = !controlLook;
            this.PrivatePanel.Enabled = !controlLook; 
            this.PrivateCheckBox.Enabled = !controlLook;
            this.DeadEndPanel.Enabled = !controlLook; 
            this.DeadEndCheckBox.Enabled = !controlLook;
            this.ClockwisetoClosePanel.Enabled = !controlLook; 
            this.ClockwisetoCloseCheckBox.Enabled = !controlLook;

            this.BusinessPanel.Enabled = !controlLook; 
            this.BusinessComboBox.Enabled = !controlLook;
            this.ManufacturerPanel.Enabled = !controlLook; 
            this.ManufacturerComboBox.Enabled = !controlLook;
            this.MainDiameterPanel.Enabled = !controlLook; 
            this.MainDiameterComboBox.Enabled = !controlLook;
            this.BarrelDiameterPanel.Enabled = !controlLook; 
            this.BarrelDiameterComboBox.Enabled = !controlLook;
            this.WaterTypePanel.Enabled = !controlLook; 
            this.WaterTypeComboBox.Enabled = !controlLook;
            this.HydrentUsePanel.Enabled = !controlLook; 
            this.HydrentUseComboBox.Enabled = !controlLook;
            this.LifecycleStatusPanel.Enabled = !controlLook;
            this.LifecycleStatusComboBox.Enabled = !controlLook;
            this.Nozzle1DiameterPanel.Enabled = !controlLook; 
            this.Nozzle1DiameterComboBox.Enabled = !controlLook;
            this.Nozzle2DiameterPanel.Enabled = !controlLook; 
            this.Nozzle2DiameterComboBox.Enabled = !controlLook;
            this.Nozzle3DiameterPanel.Enabled = !controlLook; 
            this.Nozzle3DiameterComboBox.Enabled = !controlLook;
            this.Nozzle4DiameterPanel.Enabled = !controlLook; 
            this.Nozzle4DiameterComboBox.Enabled = !controlLook;
            this.HydrantIDPanel.Enabled = !controlLook;
            this.HydrantCommentPanel.Enabled = !controlLook; 
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
        /// Loads all combo box.
        /// </summary>
        private void LoadAllComboBox()
        {
            this.LoadBusinessComboBox();
            this.ToLoadDiameterComboBox();
            this.LoadMainDiameterComboBox();
            this.LoadBarrelDiameterComboBox();
            this.LoadNozzle1DiameterComboBox();
            this.LoadNozzle2DiameterComboBox();
            this.LoadNozzle3DiameterComboBox();
            this.LoadNozzle4DiameterComboBox();
            this.LoadManufacturerComboBox();
            this.LoadWaterTypeComboBox();
            this.LoadHydrantUseComboBox();
            this.LoadLifeCycleStatusComboBox();
        }

        /// <summary>
        /// Toes the load diameter combo box.
        /// </summary>
        private void ToLoadDiameterComboBox()
        {
            this.listGDocDiameterDataTable.Clear();

            DataRow customRow = this.listGDocDiameterDataTable.NewRow();
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName] = DBNull.Value;
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName] = "0";
            this.listGDocDiameterDataTable.Rows.Add(customRow);

            this.gdocCommonData = this.form84723Control.WorkItem.F8000_GetGDocDiameter(this.featureClassId);
            this.gdocDiameterRowsCount = this.gdocCommonData.ListGDocDiameter.Rows.Count;
            this.listGDocDiameterDataTable.Merge(this.gdocCommonData.ListGDocDiameter);
        }

        /// <summary>
        /// Creates the temp list G doc property reference.
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
            this.gdocCommonData = this.form84723Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
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

            this.gdocCommonData = this.form84723Control.WorkItem.F8000_GetGDocBusiness();
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
            DataTable mainDiameterDataTable = new DataTable();
            mainDiameterDataTable.Merge(this.listGDocDiameterDataTable);

            if (this.gdocDiameterRowsCount > 0)
            {
                this.MainDiameterComboBox.DataSource = mainDiameterDataTable;
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
        /// Loads the barrel diameter combo box.
        /// </summary>
        private void LoadBarrelDiameterComboBox()
        {
            DataTable barrelDiameterDataTable = new DataTable();
            barrelDiameterDataTable.Merge(this.listGDocDiameterDataTable);

            if (this.gdocDiameterRowsCount > 0)
            {
                this.BarrelDiameterComboBox.DataSource = barrelDiameterDataTable;
                this.BarrelDiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.BarrelDiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.barrelDiameter))
                {
                    this.BarrelDiameterComboBox.Text = this.barrelDiameter;
                }
                else
                {
                    this.BarrelDiameterComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the nozzle1 diameter combo box.
        /// </summary>
        private void LoadNozzle1DiameterComboBox()
        {
            DataTable nozzle1DiameterDataTable = new DataTable();
            nozzle1DiameterDataTable.Merge(this.listGDocDiameterDataTable);

            if (this.gdocDiameterRowsCount > 0)
            {
                this.Nozzle1DiameterComboBox.DataSource = nozzle1DiameterDataTable;
                this.Nozzle1DiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.Nozzle1DiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.nozzelDiameter1))
                {
                    this.Nozzle1DiameterComboBox.Text = this.nozzelDiameter1;
                }
                else
                {
                    this.Nozzle1DiameterComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the nozzle2 diameter combo box.
        /// </summary>
        private void LoadNozzle2DiameterComboBox()
        {
            DataTable nozzle2DiameterDataTable = new DataTable();
            nozzle2DiameterDataTable.Merge(this.listGDocDiameterDataTable);
            if (this.gdocDiameterRowsCount > 0)
            {
                this.Nozzle2DiameterComboBox.DataSource = nozzle2DiameterDataTable;
                this.Nozzle2DiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.Nozzle2DiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.nozzelDiameter2))
                {
                    this.Nozzle2DiameterComboBox.Text = this.nozzelDiameter2;
                }
                else
                {
                    this.Nozzle2DiameterComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the nozzle3 diameter combo box.
        /// </summary>
        private void LoadNozzle3DiameterComboBox()
        {
            DataTable nozzle3DiameterDataTable = new DataTable();
            nozzle3DiameterDataTable.Merge(this.listGDocDiameterDataTable);
            if (this.gdocDiameterRowsCount > 0)
            {
                this.Nozzle3DiameterComboBox.DataSource = nozzle3DiameterDataTable;
                this.Nozzle3DiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.Nozzle3DiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.nozzelDiameter3))
                {
                    this.Nozzle3DiameterComboBox.Text = this.nozzelDiameter3;
                }
                else
                {
                    this.Nozzle3DiameterComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the nozzle4 diameter combo box.
        /// </summary>
        private void LoadNozzle4DiameterComboBox()
        {
            DataTable nozzle4DiameterDataTable = new DataTable();
            nozzle4DiameterDataTable.Merge(this.listGDocDiameterDataTable);
            if (this.gdocDiameterRowsCount > 0)
            {
                this.Nozzle4DiameterComboBox.DataSource = nozzle4DiameterDataTable;
                this.Nozzle4DiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.Nozzle4DiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.nozzelDiameter4))
                {
                    this.Nozzle4DiameterComboBox.Text = this.nozzelDiameter4;
                }
                else
                {
                    this.Nozzle4DiameterComboBox.SelectedIndex = 0;
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
        /// Loads the water type combo box.
        /// </summary>
        private void LoadWaterTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("WaterTypeID");

            DataTable waterTypeComboDataTable = new DataTable();
            waterTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocPropertyReferenceRowsCount > 0)
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
        /// Loads the hydrant use combo box.
        /// </summary>
        private void LoadHydrantUseComboBox()
        {
            this.CreateTempListGDocPropertyReference("UserID");

            DataTable hydrantUseComboDataTable = new DataTable();
            hydrantUseComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.HydrentUseComboBox.DataSource = hydrantUseComboDataTable;
                this.HydrentUseComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.HydrentUseComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.userId > 0)
                {
                    this.HydrentUseComboBox.SelectedValue = this.userId;
                }
                else
                {
                    this.HydrentUseComboBox.SelectedIndex = 0;
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

            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.LifecycleStatusComboBox.DataSource = lifeCycleStatusCombDataTable;
                this.LifecycleStatusComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.LifecycleStatusComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.lifecycleStatusId > 0)
                {
                    this.LifecycleStatusComboBox.SelectedValue = this.lifecycleStatusId;
                }
                else
                {
                    this.LifecycleStatusComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Saves the water hydrant properties.
        /// </summary>
        private void SaveWaterHydrantProperties()
        {
            int keyIdValue;
            int tempfeatureId;
            int tempmainValveId;
            decimal temphydrostaticPressure;
            ////decimal tempBarrelDiameter;
            ////decimal tempDiameter;
            decimal tempPressureSetting;
            decimal tempValveOpening;
            decimal tempTurnsToClose;
            int tempNozzles;

              F84723WaterHydrantPropertiesData waterHydrantPropertiesData1 = new F84723WaterHydrantPropertiesData();

            //this.waterHydrantPropertiesData.Clear(); 
            //this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows.Clear();
            this.waterHydrantPropertiesData = null;
            F84723WaterHydrantPropertiesData.GetWaterHydrantPropertiesDataTableRow dr = waterHydrantPropertiesData1.GetWaterHydrantPropertiesDataTable.NewGetWaterHydrantPropertiesDataTableRow();

            dr.FeatureClassID = this.featureClassId;

            if (!string.IsNullOrEmpty(this.HydrantIDTextBox.Text.Trim()))
            {
                int.TryParse(this.HydrantIDTextBox.Text.Trim(), out tempfeatureId);
                keyIdValue = tempfeatureId;
            }
            else
            {
                keyIdValue = 0;
            }

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

            if (!string.IsNullOrEmpty(this.MainValveLinkLabel.Text.Trim()))
            {
                int.TryParse(this.MainValveLinkLabel.Text.Trim(), out tempmainValveId);
                dr.MainValveID = tempmainValveId;
            }

            if (!string.IsNullOrEmpty(this.PressureSettingTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.PressureSettingTextBox.Text.Trim(), out temphydrostaticPressure);
                dr.PressureSetting = temphydrostaticPressure;
            }

            if (!string.IsNullOrEmpty(this.HydrostaticPressureTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.HydrostaticPressureTextBox.Text.Trim(), out tempPressureSetting);
                dr.HydrostaticPressue = tempPressureSetting;
            }

            if (!string.IsNullOrEmpty(this.NozzlesTextBox.Text.Trim()))
            {
                int.TryParse(this.NozzlesTextBox.Text.Trim(), out tempNozzles);
                dr.Nozzles = tempNozzles;
            }

            if (!string.IsNullOrEmpty(this.ValveOpeningTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.ValveOpeningTextBox.Text.Trim(), out tempValveOpening);
                dr.ValveOpening = tempValveOpening;
            }

            if (!string.IsNullOrEmpty(this.TurnstoCloseTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.TurnstoCloseTextBox.Text.Trim(), out tempTurnsToClose);
                dr.TurnsToClose = tempTurnsToClose;
            }

            if (!string.IsNullOrEmpty(this.CommentTextBox.Text.Trim()))
            {
                dr.Comment = this.CommentTextBox.Text.Trim();
            }

            dr.IsEnabled = this.HydrantEnableCheckBox.Checked;
            dr.IsPrivate = this.PrivateCheckBox.Checked;
            dr.IsDeadend = this.DeadEndCheckBox.Checked;
            dr.IsClockwiseToClose = this.ClockwisetoCloseCheckBox.Checked;

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

            if (!string.IsNullOrEmpty(this.MainDiameterComboBox.Text.Trim()))
            {
                dr.Diameter = Convert.ToDecimal(this.MainDiameterComboBox.Text);
            }

            if (!string.IsNullOrEmpty(this.BarrelDiameterComboBox.Text.Trim()))
            {
                dr.BarrelDiameter = Convert.ToDecimal(this.BarrelDiameterComboBox.Text);
            }

            if (!string.IsNullOrEmpty(this.WaterTypeComboBox.Text.Trim()))
            {
                dr.WaterTypeID = Convert.ToInt32(this.WaterTypeComboBox.SelectedValue);
            }
            else
            {
                dr.WaterTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.HydrentUseComboBox.Text.Trim()))
            {
                dr.UserID = Convert.ToInt32(this.HydrentUseComboBox.SelectedValue);
            }
            else
            {
                dr.UserID = 0;
            }

            if (!string.IsNullOrEmpty(this.LifecycleStatusComboBox.Text.Trim()))
            {
                dr.LifecycleStatusID = Convert.ToInt32(this.LifecycleStatusComboBox.SelectedValue);
            }
            else
            {
                dr.LifecycleStatusID = 0;
            }

            if (!string.IsNullOrEmpty(this.Nozzle1DiameterComboBox.Text.Trim()))
            {
                dr.NozzleDiameter1 = Convert.ToDecimal(this.Nozzle1DiameterComboBox.Text);
            }

            if (!string.IsNullOrEmpty(this.Nozzle2DiameterComboBox.Text.Trim()))
            {
                dr.NozzleDiameter2 = Convert.ToDecimal(this.Nozzle2DiameterComboBox.Text);
            }

            if (!string.IsNullOrEmpty(this.Nozzle3DiameterComboBox.Text.Trim()))
            {
                dr.NozzleDiameter3 = Convert.ToDecimal(this.Nozzle3DiameterComboBox.Text);
            }

            if (!string.IsNullOrEmpty(this.Nozzle4DiameterComboBox.Text.Trim()))
            {
                dr.NozzleDiameter4 = Convert.ToDecimal(this.Nozzle4DiameterComboBox.Text);
            }

            waterHydrantPropertiesData1.GetWaterHydrantPropertiesDataTable.Rows.Add(dr);

            this.hydrantId = this.form84723Control.WorkItem.F84723_SaveWaterHydrantProperties(keyIdValue, (Utility.GetXmlString(waterHydrantPropertiesData1.GetWaterHydrantPropertiesDataTable.Copy())), TerraScanCommon.UserId);

            ////to sent the this returned keyid(this.hydrantId)to F84724
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.hydrantId;
            this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            ////to reload the form with the current keyid(this.valveId)
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (!this.CheckMainValveId())
            {
                this.MainValveTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F84702InvalidMainValveID");
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// To check Valid Main valve ID
        /// </summary>
        /// <returns>A Boolean valve</returns>
        private bool CheckMainValveId()
        {
            try
            {
                int errorId = -1;
                int mainvalveIdvalue;
                if (!string.IsNullOrEmpty(this.MainValveLinkLabel.Text.Trim()))
                {
                    int.TryParse(this.MainValveLinkLabel.Text.Trim().Replace(",", ""), out mainvalveIdvalue);
                    errorId = this.form84723Control.WorkItem.F84723_CheckMainValveId(mainvalveIdvalue);
                    if (errorId == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (SoapException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the water hydrante properties.
        /// </summary>
        private void DeleteWaterHydranteProperties()
        {
            this.form84723Control.WorkItem.F84723_DeleteWaterHydrantProperties(this.hydrantId, TerraScanCommon.UserId);
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.BusinessComboBox.SelectedIndex = -1;
            this.ManufacturerComboBox.SelectedIndex = -1;
            this.MainDiameterComboBox.SelectedIndex = -1;
            this.BarrelDiameterComboBox.SelectedIndex = -1;
            this.WaterTypeComboBox.SelectedIndex = -1;
            this.HydrentUseComboBox.SelectedIndex = -1;
            this.LifecycleStatusComboBox.SelectedIndex = -1;
            this.Nozzle1DiameterComboBox.SelectedIndex = -1;
            this.Nozzle2DiameterComboBox.SelectedIndex = -1;
            this.Nozzle3DiameterComboBox.SelectedIndex = -1;
            this.Nozzle4DiameterComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Customizes the water hydrant properties.
        /// </summary>
        private void CustomizeWaterHydrantProperties()
        {
            int mainValveIdLinkvalue;

            this.waterHydrantPropertiesData = this.form84723Control.WorkItem.F84723_GetWaterHydrantProperties(this.hydrantId);
            if (this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows.Count > 0)
            {
                this.HydrantIDTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.FeatureIDColumn].ToString();
                this.CIDTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.CIDColumn].ToString();
                this.ModelNumberTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.ModelNumberColumn].ToString();
                this.SerialNumberTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.SerialNumberColumn].ToString();
                ////this.MainValveTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.MainValveIDColumn].ToString();
                this.MainValveTextBox.Text = string.Empty;
                mainValveIdLinkvalue = Convert.ToInt32(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.MainValveIDColumn]);
                if (mainValveIdLinkvalue != 0)
                {
                    this.MainValveLinkLabel.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.MainValveIDColumn].ToString();
                }
                else
                {
                    this.MainValveLinkLabel.Text = string.Empty;
                }

                this.PressureSettingTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.PressureSettingColumn].ToString();
                this.HydrostaticPressureTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.HydrostaticPressueColumn].ToString();
                this.NozzlesTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.NozzlesColumn].ToString();
                this.ValveOpeningTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.ValveOpeningColumn].ToString();
                this.TurnstoCloseTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.TurnsToCloseColumn].ToString();
                this.CommentTextBox.Text = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.CommentColumn].ToString();

                this.HydrantEnableCheckBox.Checked = Convert.ToBoolean(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.IsEnabledColumn].ToString());
                this.PrivateCheckBox.Checked = Convert.ToBoolean(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.IsPrivateColumn].ToString());
                this.DeadEndCheckBox.Checked = Convert.ToBoolean(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.IsDeadendColumn].ToString());
                this.ClockwisetoCloseCheckBox.Checked = Convert.ToBoolean(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.IsClockwiseToCloseColumn].ToString());

                this.businessId = Convert.ToInt32(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.BusinessIDColumn]);
                this.manufacturerId = Convert.ToInt32(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.ManufacturerIDColumn]);
                this.mainDiameter = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.DiameterColumn].ToString();
                this.barrelDiameter = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.BarrelDiameterColumn].ToString();
                this.waterTypeId = Convert.ToInt32(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.WaterTypeIDColumn]);
                this.userId = Convert.ToInt32(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.UserIDColumn]);
                this.lifecycleStatusId = Convert.ToInt32(this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.LifecycleStatusIDColumn]);
                this.nozzelDiameter1 = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.NozzleDiameter1Column].ToString();
                this.nozzelDiameter2 = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.NozzleDiameter2Column].ToString();
                this.nozzelDiameter3 = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.NozzleDiameter3Column].ToString();
                this.nozzelDiameter4 = this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows[0][this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.NozzleDiameter4Column].ToString();

                this.CIDTextBox.Focus();
            }
            else
            {
                this.ClearWaterHydrantControls();
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
        /// Handles the Click event of the WaterHydrantPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterHydrantPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the WaterHydrantPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterHydrantPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocWaterHydrantToolTip.SetToolTip(this.WaterHydrantPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        /// <summary>
        /// Handles the Load event of the F84723 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84723_Load(object sender, EventArgs e)
        {
            try
            {
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.LoadWaterHydrantProperties();
                if (this.waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable.Rows.Count > 0)
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
        /// To Load the water hydrant properties.
        /// </summary>
        private void LoadWaterHydrantProperties()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeWaterHydrantProperties();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Handles the Click event of the MainValveTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MainValveTextBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnableDisableMainValveTex();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the disable main valve Link Label.
        /// </summary>
        private void EnableDisableMainValveTex()
        {
            if (this.MainValveLinkLabel.Visible)
            {
                this.MainValveLinkLabel.Visible = false;
                this.flagLoadOnProcess = true;
                this.MainValveTextBox.Text = this.MainValveLinkLabel.Text;
                this.flagLoadOnProcess = false;
            }
        }

        /// <summary>
        /// Handles the Leave event of the MainValveTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MainValveTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int tempValue = -1;
                if (!string.IsNullOrEmpty(this.MainValveTextBox.Text.Trim()))
                {
                    if (!(int.TryParse(MainValveTextBox.Text, out tempValue)) || (Convert.ToInt32(MainValveTextBox.Text) > int.MaxValue))
                    {
                        this.MainValveLinkLabel.Text = "0";
                    }
                    else
                    {
                        this.MainValveLinkLabel.Text = this.MainValveTextBox.Text;
                    }
                }
                else
                {
                    this.MainValveLinkLabel.Text = string.Empty;
                }

                this.flagLoadOnProcess = true;
                this.MainValveTextBox.Text = string.Empty;
                this.flagLoadOnProcess = false;
                this.MainValveLinkLabel.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MainValveTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MainValveTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.MainValveLinkLabel.Visible)
                {
                    this.MainValveLinkLabel.Visible = false;
                }

                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the MainValveLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void MainValveLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int valveId;
                int tmepValveIdValue;
                int.TryParse(this.MainValveLinkLabel.Text.Trim().Replace(",", ""), out tmepValveIdValue);
                valveId = tmepValveIdValue;
                FormInfo formInfo;
                if (valveId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(84701);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = valveId;
                    formInfo.optionalParameters[1] = this.featureClassId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// When the link label is just clicked not on the actual value but on the empty space,
        /// then Text box should be enabled
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MainValveLinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnableDisableMainValveTex();
                this.MainValveTextBox.Focus();
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

        private void BarrelDiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void HydrentUseComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void LifecycleStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void Nozzle1DiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void Nozzle2DiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void Nozzle3DiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void Nozzle4DiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
