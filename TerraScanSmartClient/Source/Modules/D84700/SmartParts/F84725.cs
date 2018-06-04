//--------------------------------------------------------------------------------------------
// <copyright file="F84725.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84725.
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
    /// F84725 class file
    /// </summary>
    public partial class F84725 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// pipeId (key Id)
        /// </summary>
        private int pipeId;

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// controller F84723
        /// </summary>
        private F84725Controller form84725Control;

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
        private F84725WaterPipePropertiesData waterPipePropertiesData = new F84725WaterPipePropertiesData();

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
        /// pipeTypeId
        /// </summary>
        private int pipeTypeId;

        /// <summary>
        /// pipeDiameter
        /// </summary>
        private string pipeDiameter;

        /// <summary>
        /// materialId
        /// </summary>
        private int materialId;

        /// <summary>
        /// waterTypeId
        /// </summary>
        private int waterTypeId;

        /// <summary>
        /// jointTypeMaleId
        /// </summary>
        private int jointTypeMaleId;

        /// <summary>
        /// jointTypeFemaleId
        /// </summary>
        private int jointTypeFemaleId;

        /// <summary>
        /// gasketTypeId
        /// </summary>
        private int gasketTypeId;

        /// <summary>
        /// lifecycleStatusId
        /// </summary>
        private int lifecycleStatusId;

        #endregion Variable For CoboBox Value Members

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84725"/> class.
        /// </summary>
        public F84725()
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
        public F84725(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.pipeId = keyID;
            this.Tag = formNo;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterPipePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(229, 42, tabText, red, green, blue);
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
        public F84725(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.pipeId = keyID;
            this.featureClassId = featureClassId;
            this.formMasterPermissionEdit = permissionEdit;
            this.WaterPipePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(229, 42, tabText, red, green, blue);
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
        /// For form84725Control
        /// </summary>
        [CreateNew]
        public F84725Controller Form84725Control
        {
            get { return this.form84725Control as F84725Controller; }
            set { this.form84725Control = value; }
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
                    this.ClearWaterPipeControls();
                    this.SetNewComboIndex();
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CIDTextBox.Focus();
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearWaterPipeControls();
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
                this.CustomizeWaterPipeProperties();
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
                        this.SaveWaterPipeProperties();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CustomizeWaterPipeProperties();
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
                   
                    if (this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows.Count > 0)
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

                if (this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows.Count > 0)
                {
                    this.ParentForm.ActiveControl = this.CIDTextBox;
                    this.ParentForm.ActiveControl.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.DeleteWaterPipeProperties();

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
                    this.pipeId = eventArgs.Data.SelectedKeyId;
                    this.LoadWaterPipeProperties();
                    if (this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows.Count > 0)
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
            this.CIDTextBox.MaxLength = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.CIDColumn.MaxLength;
            this.PipeClassTextBox.MaxLength = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.PipeClassColumn.MaxLength;
            this.ExteriorCoatingTextBox.MaxLength = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.ExteriorCoatingColumn.MaxLength;
            this.LiningTypeTextBox.MaxLength = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.LiningTypeColumn.MaxLength;
            this.CommentTextBox.MaxLength = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.CommentColumn.MaxLength;

            this.BusinessComboBox.MaxLength = this.gdocCommonData.ListGDocBusiness.BusinessNameColumn.MaxLength;
            this.ManufacturerComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.PipeTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.PipeDiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.MaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.WaterTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.MaleJointTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.FemaleJointTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.GasketTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.LifecycleStatusComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
        }

        /// <summary>
        /// Clears the water pipe controls.
        /// </summary>
        private void ClearWaterPipeControls()
        {
            this.PipeIDTextBox.Text = string.Empty;
            this.CIDTextBox.Text = string.Empty;
            this.PressureRatingTextBox.Text = string.Empty;
            this.PipeClassTextBox.Text = string.Empty;
            this.ExteriorCoatingTextBox.Text = string.Empty;
            this.LiningTypeTextBox.Text = string.Empty;
            this.RoughnessCoefficientTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;

            this.EnableCheckBox.Checked = false;
            this.PrivateCheckBox.Checked = false;
            this.PolywrapCheckBox.Checked = false;
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
        /// To Clear the entire Water Pipe Properties Form
        /// </summary>
        private void ClearWaterPipePropertiesValve()
        {
            this.ClearComboBox(this);
            this.ClearWaterPipeControls();
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.PipeIDPanel.Enabled = lockControl;
            this.CIDPanel.Enabled = lockControl;
            this.EnablePanel.Enabled = lockControl;
            this.PrivatePanel.Enabled = lockControl;
            this.BusinessPanel.Enabled = lockControl;
            this.ManufacturerPanel.Enabled = lockControl;
            this.PipeTypePanel.Enabled = lockControl;
            this.PipeDiameterPanel.Enabled = lockControl;
            this.PressureRatingPanel.Enabled = lockControl;
            this.PipeClassPanel.Enabled = lockControl;
            this.MaterialPanel.Enabled = lockControl;
            this.ExteriorCoatingPanel.Enabled = lockControl;
            this.LiningTypePanel.Enabled = lockControl;
            this.PolywrapPanel.Enabled = lockControl;
            this.RoughnessCoefficientPanel.Enabled = lockControl;
            this.WaterTypePanel.Enabled = lockControl;
            this.MaleJointTypePanel.Enabled = lockControl;
            this.FemaleJointTypePanel.Enabled = lockControl;
            this.GasketTypePanel.Enabled = lockControl;
            this.LifecycleStatusPanel.Enabled = lockControl;
            this.CommentPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">boolean value</param>
        private void ControlLock(bool controlLook)
        {
            ////this.PipeIDTextBox.LockKeyPress = controlLook;
            this.CIDPanel.Enabled = !controlLook; 
            this.CIDTextBox.LockKeyPress = controlLook;
            this.PressureRatingPanel.Enabled = !controlLook; 
            this.PressureRatingTextBox.LockKeyPress = controlLook;
            this.PipeIDPanel.Enabled = !controlLook;  
            this.PipeClassPanel.Enabled = !controlLook;  
            this.PipeClassTextBox.LockKeyPress = controlLook;
            this.ExteriorCoatingPanel.Enabled = !controlLook; 
            this.ExteriorCoatingTextBox.LockKeyPress = controlLook;
            this.LiningTypePanel.Enabled = !controlLook; 
            this.LiningTypeTextBox.LockKeyPress = controlLook;
            this.RoughnessCoefficientPanel.Enabled = !controlLook; 
            this.RoughnessCoefficientTextBox.LockKeyPress = controlLook;
            this.CommentPanel.Enabled = !controlLook; 
            this.CommentTextBox.LockKeyPress = controlLook;

            this.EnablePanel.Enabled = !controlLook; 
             this.EnableCheckBox.Enabled = !controlLook;
             this.PrivatePanel.Enabled = !controlLook; 
            this.PrivateCheckBox.Enabled = !controlLook;
            this.PolywrapPanel.Enabled = !controlLook; 
            this.PolywrapCheckBox.Enabled = !controlLook;

            this.BusinessPanel.Enabled = !controlLook; 
            this.BusinessComboBox.Enabled = !controlLook;
            this.ManufacturerPanel.Enabled = !controlLook; 
            this.ManufacturerComboBox.Enabled = !controlLook;
            this.PipeTypePanel.Enabled = !controlLook; 
            this.PipeTypeComboBox.Enabled = !controlLook;
            this.PipeDiameterPanel.Enabled = !controlLook; 
            this.PipeDiameterComboBox.Enabled = !controlLook;
            this.MaterialPanel.Enabled = !controlLook; 
            this.MaterialComboBox.Enabled = !controlLook;
            this.WaterTypePanel.Enabled = !controlLook; 
            this.WaterTypeComboBox.Enabled = !controlLook;
            this.MaleJointTypePanel.Enabled = !controlLook; 
            this.MaleJointTypeComboBox.Enabled = !controlLook;
            this.FemaleJointTypePanel.Enabled = !controlLook; 
            this.FemaleJointTypeComboBox.Enabled = !controlLook;
            this.GasketTypePanel.Enabled = !controlLook; 
            this.GasketTypeComboBox.Enabled = !controlLook;
            this.LifecycleStatusPanel.Enabled = !controlLook; 
            this.LifecycleStatusComboBox.Enabled = !controlLook;
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
        /// To Load the water pipe properties.
        /// </summary>
        private void LoadWaterPipeProperties()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeWaterPipeProperties();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
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
            this.gdocCommonData = this.form84725Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
            this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
            this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
        }

        /// <summary>
        /// Loads all combo box.
        /// </summary>
        private void LoadAllComboBox()
        {
            this.LoadBusinessComboBox();
            this.LoadManufacturerComboBox();
            this.LoadPipeTypeComboBox();
            this.LoadPipeDiameterComboBox();
            this.LoadMaterialComboBox();
            this.LoadWaterTypeComboBox();
            this.LoadMaleJointTypeComboBox();
            this.LoadFemaleJointTypeComboBox();
            this.LoadGasketTypeComboBox();
            this.LoadLifeCycleStatusComboBox();
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

            this.gdocCommonData = this.form84725Control.WorkItem.F8000_GetGDocBusiness();
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
        /// Loads the Pipe type combo box.
        /// </summary>
        private void LoadPipeTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("PipeTypeID");

            DataTable pipeTypeComboDatatable = new DataTable();
            pipeTypeComboDatatable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.PipeTypeComboBox.DataSource = pipeTypeComboDatatable;
                this.PipeTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.PipeTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.pipeTypeId > 0)
                {
                    this.PipeTypeComboBox.SelectedValue = this.pipeTypeId;
                }
                else
                {
                    this.PipeTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the Pipe diameter combo box.
        /// </summary>
        private void LoadPipeDiameterComboBox()
        {
            GDocCommonData.ListGDocDiameterDataTable listGDocDiameter = new GDocCommonData.ListGDocDiameterDataTable();

            DataRow customRow = listGDocDiameter.NewRow();
            listGDocDiameter.Clear();
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName] = DBNull.Value;
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName] = "0";
            listGDocDiameter.Rows.Add(customRow);

            this.gdocCommonData = this.form84725Control.WorkItem.F8000_GetGDocDiameter(this.featureClassId);
            listGDocDiameter.Merge(this.gdocCommonData.ListGDocDiameter);

            if (this.gdocCommonData.ListGDocDiameter.Rows.Count > 0)
            {
                this.PipeDiameterComboBox.DataSource = listGDocDiameter;
                this.PipeDiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.PipeDiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.pipeDiameter))
                {
                    this.PipeDiameterComboBox.Text = this.pipeDiameter;
                }
                else
                {
                    this.PipeDiameterComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the Material combo box.
        /// </summary>
        private void LoadMaterialComboBox()
        {
            this.CreateTempListGDocPropertyReference("MaterialID");

            DataTable materialComboDataTable = new DataTable();
            materialComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.MaterialComboBox.DataSource = materialComboDataTable;
                this.MaterialComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.MaterialComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.materialId > 0)
                {
                    this.MaterialComboBox.SelectedValue = this.materialId;
                }
                else
                {
                    this.MaterialComboBox.SelectedIndex = 0;
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
        /// Loads the Male Joint type combo box.
        /// </summary>
        private void LoadMaleJointTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("JointTypeMaleID");

            DataTable maleJointComboDataTable = new DataTable();
            maleJointComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.MaleJointTypeComboBox.DataSource = maleJointComboDataTable;
                this.MaleJointTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.MaleJointTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.jointTypeMaleId > 0)
                {
                    this.MaleJointTypeComboBox.SelectedValue = this.jointTypeMaleId;
                }
                else
                {
                    this.MaleJointTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the Female Joint type combo box.
        /// </summary>
        private void LoadFemaleJointTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("JointTypeFemaleID");

            DataTable femaleJointComboDataTable = new DataTable();
            femaleJointComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.FemaleJointTypeComboBox.DataSource = femaleJointComboDataTable;
                this.FemaleJointTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.FemaleJointTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.jointTypeFemaleId > 0)
                {
                    this.FemaleJointTypeComboBox.SelectedValue = this.jointTypeFemaleId;
                }
                else
                {
                    this.FemaleJointTypeComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the Gasket type combo box.
        /// </summary>
        private void LoadGasketTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("GasketTypeID");

            DataTable gasketTypeComboDataTable = new DataTable();
            gasketTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.GasketTypeComboBox.DataSource = gasketTypeComboDataTable;
                this.GasketTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.GasketTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.gasketTypeId > 0)
                {
                    this.GasketTypeComboBox.SelectedValue = this.gasketTypeId;
                }
                else
                {
                    this.GasketTypeComboBox.SelectedIndex = 0;
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
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.BusinessComboBox.SelectedIndex = -1;
            this.ManufacturerComboBox.SelectedIndex = -1;
            this.PipeTypeComboBox.SelectedIndex = -1;
            this.PipeDiameterComboBox.SelectedIndex = -1;
            this.MaterialComboBox.SelectedIndex = -1;
            this.WaterTypeComboBox.SelectedIndex = -1;
            this.MaleJointTypeComboBox.SelectedIndex = -1;
            this.FemaleJointTypeComboBox.SelectedIndex = -1;
            this.GasketTypeComboBox.SelectedIndex = -1;
            this.LifecycleStatusComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Customizes the water Pipe properties.
        /// </summary>
        private void CustomizeWaterPipeProperties()
        {
            this.waterPipePropertiesData = this.form84725Control.WorkItem.F84725_GetWaterPipeProperties(this.pipeId);
            if (this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows.Count > 0)
            {
                this.PipeIDTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.FeatureIDColumn].ToString();
                this.CIDTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.CIDColumn].ToString();
                this.PressureRatingTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.PressureRatingColumn].ToString();
                this.PipeClassTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.PipeClassColumn].ToString();
                this.ExteriorCoatingTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.ExteriorCoatingColumn].ToString();
                this.LiningTypeTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.LiningTypeColumn].ToString();
                this.RoughnessCoefficientTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.RoghnessColumn].ToString();
                this.CommentTextBox.Text = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.CommentColumn].ToString();

                this.EnableCheckBox.Checked = Convert.ToBoolean(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.IsEnabledColumn].ToString());
                this.PrivateCheckBox.Checked = Convert.ToBoolean(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.IsPrivateColumn].ToString());
                this.PolywrapCheckBox.Checked = Convert.ToBoolean(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.IsPolywrapColumn].ToString());

                this.businessId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.BusinessIDColumn]);
                this.manufacturerId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.ManufacturerIDColumn]);
                this.pipeTypeId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.PipeTypeIDColumn]);
                this.pipeDiameter = this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.DiameterColumn].ToString();
                this.materialId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.MaterialIDColumn]);
                this.waterTypeId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.WaterTypeIDColumn]);
                this.jointTypeMaleId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.JoinTypeMaleIDColumn]);
                this.jointTypeFemaleId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.JointTypeFemaleIDColumn]);
                this.gasketTypeId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.GasketTypeIDColumn]);
                this.lifecycleStatusId = Convert.ToInt32(this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows[0][this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.LifecycleStatusIDColumn]);

                this.CIDTextBox.Focus();
            }
            else
            {
                this.ClearWaterPipeControls();
                this.LockControls(false);
            }

            ////To Load All The ComBoBoxs
            this.LoadAllComboBox();
        }

        /// <summary>
        /// Deletes the water pipe properties.
        /// </summary>
        private void DeleteWaterPipeProperties()
        {
            this.form84725Control.WorkItem.F84725_DeleteWaterPipeProperties(this.pipeId, TerraScanCommon.UserId);
        }

        /// <summary>
        /// Saves the water pipe properties.
        /// </summary>
        private void SaveWaterPipeProperties()
        {
            int tempFeatureId;
            int keyId;
            decimal tempPressureRating;
            decimal tempRoughnessCoefficient;

            F84725WaterPipePropertiesData waterPipePropertiesData1 = new F84725WaterPipePropertiesData();
            //this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows.Clear();
            F84725WaterPipePropertiesData.GetWaterPipePropertiesDataTableRow dr = waterPipePropertiesData1.GetWaterPipePropertiesDataTable.NewGetWaterPipePropertiesDataTableRow();

            if (!string.IsNullOrEmpty(this.PipeIDTextBox.Text.Trim()))
            {
                int.TryParse(this.PipeIDTextBox.Text.Trim(), out tempFeatureId);
                keyId = tempFeatureId;
            }
            else
            {
                ////When the pipeID text box is empty then it is 0 to be sent to db                   
                keyId = 0;
            }

            dr.FeatureClassID = this.featureClassId;

            if (!string.IsNullOrEmpty(this.CIDTextBox.Text.Trim()))
            {
                dr.CID = this.CIDTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.PipeClassTextBox.Text.Trim()))
            {
                dr.PipeClass = this.PipeClassTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ExteriorCoatingTextBox.Text.Trim()))
            {
                dr.ExteriorCoating = this.ExteriorCoatingTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.LiningTypeTextBox.Text.Trim()))
            {
                dr.LiningType = this.LiningTypeTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.CommentTextBox.Text.Trim()))
            {
                dr.Comment = this.CommentTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.PressureRatingTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.PressureRatingTextBox.Text.Trim(), out tempPressureRating);
                dr.PressureRating = tempPressureRating;
            }

            if (!string.IsNullOrEmpty(this.RoughnessCoefficientTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.RoughnessCoefficientTextBox.Text.Trim(), out tempRoughnessCoefficient);
                dr.Roghness = tempRoughnessCoefficient;
            }

            dr.IsEnabled = this.EnableCheckBox.Checked;
            dr.IsPrivate = this.PrivateCheckBox.Checked;
            dr.IsPolywrap = this.PolywrapCheckBox.Checked;

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

            if (!string.IsNullOrEmpty(this.PipeTypeComboBox.Text.Trim()))
            {
                dr.PipeTypeID = Convert.ToInt32(this.PipeTypeComboBox.SelectedValue);
            }
            else
            {
                dr.PipeTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.PipeDiameterComboBox.Text.Trim()))
            {
                dr.Diameter = Convert.ToDecimal(this.PipeDiameterComboBox.Text.Trim());
            }

            if (!string.IsNullOrEmpty(this.MaterialComboBox.Text.Trim()))
            {
                dr.MaterialID = Convert.ToInt32(this.MaterialComboBox.SelectedValue);
            }
            else
            {
                dr.MaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.WaterTypeComboBox.Text.Trim()))
            {
                dr.WaterTypeID = Convert.ToInt32(this.WaterTypeComboBox.SelectedValue);
            }
            else
            {
                dr.WaterTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.MaleJointTypeComboBox.Text.Trim()))
            {
                dr.JoinTypeMaleID = Convert.ToInt32(this.MaleJointTypeComboBox.SelectedValue);
            }
            else
            {
                dr.JoinTypeMaleID = 0;
            }

            if (!string.IsNullOrEmpty(this.FemaleJointTypeComboBox.Text.Trim()))
            {
                dr.JointTypeFemaleID = Convert.ToInt32(this.FemaleJointTypeComboBox.SelectedValue);
            }
            else
            {
                dr.JointTypeFemaleID = 0;
            }

            if (!string.IsNullOrEmpty(this.GasketTypeComboBox.Text.Trim()))
            {
                dr.GasketTypeID = Convert.ToInt32(this.GasketTypeComboBox.SelectedValue);
            }
            else
            {
                dr.GasketTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.LifecycleStatusComboBox.Text.Trim()))
            {
                dr.LifecycleStatusID = Convert.ToInt32(this.LifecycleStatusComboBox.SelectedValue);
            }
            else
            {
                dr.LifecycleStatusID = 0;
            }

            waterPipePropertiesData1.GetWaterPipePropertiesDataTable.Rows.Add(dr);
            this.pipeId = this.form84725Control.WorkItem.F84725_SaveWaterPipeProperties(keyId, (Utility.GetXmlString(waterPipePropertiesData1.GetWaterPipePropertiesDataTable.Copy())), TerraScanCommon.UserId);
            ////to sent the this returned keyid(this.pipeid) to 84726
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.pipeId;
            this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            ////to reload the form with the current keyid(this.pipeid)               
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));

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
        /// Handles the MouseEnter event of the WaterPipePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WaterPipePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocWaterPipeToolTip.SetToolTip(this.WaterPipePictureBox, Utility.GetFormNameSpace(this.Name));
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
        /// Handles the Load event of the F84725 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84725_Load(object sender, EventArgs e)
        {
            try
            {
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.LoadWaterPipeProperties();
                if (this.waterPipePropertiesData.GetWaterPipePropertiesDataTable.Rows.Count > 0)
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

        private void PipeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void PipeDiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void MaterialComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void MaleJointTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void FemaleJointTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void GasketTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
