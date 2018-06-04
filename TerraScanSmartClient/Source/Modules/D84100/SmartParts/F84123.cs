//--------------------------------------------------------------------------------------------
// <copyright file="F84123.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84123.
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
    /// F84123 class file
    /// </summary>
      [SmartPart]
    public partial class F84123 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// pipeId
        /// </summary>
        private int pipeId;
      
        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// controller F84721
        /// </summary>
        private F84123Controller form84123Control;

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
        /// An instance for SanitaryPipePropertiesData
        /// </summary>
        private F84123SanitaryPipePropertiesData sanitaryPipePropertiesData = new F84123SanitaryPipePropertiesData();

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

        /// <summary>
        /// ComboBox Variables
        /// </summary>
        #region Variable For ComboBox Value Members

        /// <summary>
        /// LifeCycleStatusId
        /// </summary>
        private int lifeCycleStatusId;

        /// <summary>
        /// mainTypeId
        /// </summary>
        private int mainTypeId;

        /// <summary>
        /// materialId
        /// </summary>
        private int materialId;

        /// <summary>
        /// accessTypeId
        /// </summary>
        private int accessTypeId;

        /// <summary>
        /// surfaceCoverId
        /// </summary>
        private int surfaceCoverId;

        /// <summary>
        /// mainDiameterId
        /// </summary>
        private string mainDiameterId;
        
        #endregion Variable For ComboBox Value Members

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84123"/> class.
        /// </summary>
        public F84123()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84123"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F84123(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.pipeId = keyID;
            this.Tag = formNo;
            this.PropertiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PropertiesPictureBox.Height, this.PropertiesPictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84123"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// /// <param name="featureClassId">featureClassId.</param>
        public F84123(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.pipeId = keyID;
            this.Tag = formNo;
            this.featureClassId = featureClassId;
            this.PropertiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PropertiesPictureBox.Height, this.PropertiesPictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event D84100_F84123_OnSave_GetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84100_F84123_OnSave_GetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_OnSave_GetKeyId;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F84123 control.
        /// </summary>
        /// <value>The F84123 control.</value>
        [CreateNew]
        public F84123Controller Form84123Control
        {
            get 
            { 
                return this.form84123Control as F84123Controller;
            }

            set 
            { 
                this.form84123Control = value;
            }
        }
        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;                    
                this.ClearSanitaryPipeControls();
                this.LockControls(true);
                this.ControlLock(false);
                this.SetNewComboIndex();
                this.CIDTextBox.Focus();
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearSanitaryPipePropertiesForm();
                this.LockControls(false);
                this.SetNewComboIndex();
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
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LockControls(true);
            this.CustomizeSanitaryPipeProperties();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
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

                    if (this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows.Count > 0)
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
                if (this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows.Count > 0)
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
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
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

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.SaveSanitaryPipeProperties();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.CIDTextBox.Focus();
                }
            }
            else
            {
                this.LockControls(true);
                this.ControlLock(false);
                this.CustomizeSanitaryPipeProperties();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            if (this.slicePermissionField.deletePermission)
            {
                this.DeleteSanitaryPipeProperties();
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = true;
                this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
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
                    this.CustomizeSanitaryPipeProperties();
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
        /// Event Subscription D84100_F84124_OnSave_SetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84100_F84124_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_SetKeyId(object sender, DataEventArgs<int> eventArgs)
        {
                this.pipeId = eventArgs.Data;
        }

        #endregion Event Subscription

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

        #region Private Methods

        /// <summary>
        /// Clears the entire Sanitary Pipe Property
        /// </summary>
        private void ClearSanitaryPipePropertiesForm()
        {
            this.ClearComboBox(this);
            this.ClearSanitaryPipeControls();
        }

        /// <summary>
        /// To Clear Sanitary Pipe Properties TextBoxs and Checked Boxs
        /// </summary>
        private void ClearSanitaryPipeControls()
        {
            this.PipeIdTextBox.Text = string.Empty;
            this.CIDTextBox.Text = string.Empty;
            this.DownStreamDepthTextBox.Text = string.Empty;
            this.DownStreamElevationTextBox.Text = string.Empty;
            this.UpStreamDepthTextBox.Text = string.Empty;
            this.UpStreamElevationTextBox.Text = string.Empty;
            this.SlopeTextBox.Text = string.Empty;
            this.MainLengthTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;
            this.PrivateCheckBox.Checked = false;
            this.EnabledCheckBox.Checked = false;
            this.ForcedCheckBox.Checked = false;
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
            this.PipeIdPanel.Enabled = lockControl;
            this.MaterialPanel.Enabled = lockControl;
            this.MainTypePanel.Enabled = lockControl;
            this.CIDPanel.Enabled = lockControl;
            this.CommentPanel.Enabled = lockControl;
            this.LifeCycleStatusPanel.Enabled = lockControl;
            this.PrivatePanel.Enabled = lockControl;
            this.EnabledPanel.Enabled = lockControl;
            this.MainLengthPanel.Enabled = lockControl;
            this.SlopePanel.Enabled = lockControl;
            this.UpStreamElevationPanel.Enabled = lockControl;
            this.UpStreamDepthPanel.Enabled = lockControl;
            this.DownStreamElevationPanel.Enabled = lockControl;
            this.DownStreamDepthPanel.Enabled = lockControl;
            this.ForcedPanel.Enabled = lockControl;
            this.SurfaceCoverPanel.Enabled = lockControl;
            this.AccessTypePanel.Enabled = lockControl;
            this.MainDiameterPanel.Enabled = lockControl;
         }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
         /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.CIDTextBox.LockKeyPress = controlLook;
            this.DownStreamDepthTextBox.LockKeyPress = controlLook;
            this.DownStreamElevationTextBox.LockKeyPress = controlLook;
            this.UpStreamDepthTextBox.LockKeyPress = controlLook;
            this.UpStreamElevationTextBox.LockKeyPress = controlLook;
            this.SlopeTextBox.LockKeyPress = controlLook;
            this.MainLengthTextBox.LockKeyPress = controlLook;
            this.CommentTextBox.LockKeyPress = controlLook;

            this.EnabledCheckBox.Enabled = !controlLook;
            this.PrivateCheckBox.Enabled = !controlLook;
            this.ForcedCheckBox.Enabled = !controlLook;

            this.LifeCycleStatusComboBox.Enabled = !controlLook;
            this.MainTypeComboBox.Enabled = !controlLook;
            this.MaterialComboBox.Enabled = !controlLook;
            this.AccessTypeComboBox.Enabled = !controlLook;
            this.SurfaceCoverComboBox.Enabled = !controlLook;
            this.MainDiameterComboBox.Enabled = !controlLook;
        }

        /// <summary>
        /// Customizes Sanitary Pipe properties.
        /// </summary>
        private void CustomizeSanitaryPipeProperties()
        {
            this.sanitaryPipePropertiesData = this.form84123Control.WorkItem.F84123_GetSanitaryPipeProperties(this.pipeId);
            if (this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows.Count > 0)
            {
                this.PipeIdTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.FeatureIDColumn].ToString();
                this.CIDTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.CIDColumn].ToString();
                this.DownStreamDepthTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.DownDepthColumn].ToString();
                this.DownStreamElevationTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.DownElevationColumn].ToString();
                this.UpStreamDepthTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.UpDepthColumn].ToString();
                this.UpStreamElevationTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.UpElevationColumn].ToString();
                this.SlopeTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.SlopeColumn].ToString();
                this.MainLengthTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.LengthColumn].ToString();
                this.CommentTextBox.Text = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.CommentColumn].ToString(); 

                this.EnabledCheckBox.Checked = Convert.ToBoolean(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.IsEnabledColumn].ToString());
                this.PrivateCheckBox.Checked = Convert.ToBoolean(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.IsPrivateColumn].ToString());
                this.ForcedCheckBox.Checked = Convert.ToBoolean(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.IsForcedColumn].ToString());

                this.lifeCycleStatusId = Convert.ToInt32(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.LifecycleStatusIDColumn]);
                this.mainTypeId = Convert.ToInt32(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.PipeTypeIDColumn]);
                this.materialId = Convert.ToInt32(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.MaterialIDColumn]);
                this.accessTypeId = Convert.ToInt32(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.AccessTypeIDColumn]);
                this.surfaceCoverId = Convert.ToInt32(this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.SurfaceCoverIDColumn]);
                this.mainDiameterId  = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows[0][this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.DiameterColumn].ToString();
            }
            else
            {
                this.ClearSanitaryPipePropertiesForm();
                this.LockControls(false);
            }

            ////To Load All The ComBoBoxs
            this.LoadAllComboBox();
        }

        /// <summary>
        /// Loads all combO boxes.
        /// </summary>
        private void LoadAllComboBox()
        {
            this.LoadLifeCycleStatusComboBox();
            this.LoadMainTypeComboBox();
            this.LoadMaterialComboBox();
            this.LoadAccessTypeComboBox();
            this.LoadSurfaceCoverComboBox();
            this.LoadMainDiameterComboBox();
         }

        /// <summary>
         /// Creates the temp listGdocpropertyreference Datatable to load Combo boxes.
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
            this.gdocCommonData = this.form84123Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
            this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
            this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
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

            this.gdocCommonData = this.form84123Control.WorkItem.F8000_GetGDocDiameter(this.featureClassId);
            listGDocDiameter.Merge(this.gdocCommonData.ListGDocDiameter);

            if (this.gdocCommonData.ListGDocDiameter.Rows.Count > 0)
            {
                this.MainDiameterComboBox.DataSource  = listGDocDiameter;
                this.MainDiameterComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                this.MainDiameterComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                if (!string.IsNullOrEmpty(this.mainDiameterId))
                {
                    this.MainDiameterComboBox.Text = this.mainDiameterId;
                }
                else
                {
                    this.MainDiameterComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the Life Cycle Status combo box.
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

                if (this.lifeCycleStatusId > 0)
                {
                    this.LifeCycleStatusComboBox.SelectedValue = this.lifeCycleStatusId;
                }
                else
                {
                    this.LifeCycleStatusComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the Main Type combo box.
        /// </summary>
        private void LoadMainTypeComboBox()
        {
            this.CreateTempListGDocPropertyReference("PipeTypeID");

            DataTable pipeTypeComboDataTable = new DataTable();
            pipeTypeComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.MainTypeComboBox.DataSource = pipeTypeComboDataTable;
                this.MainTypeComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.MainTypeComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.mainTypeId > 0)
                {
                    this.MainTypeComboBox.SelectedValue = this.mainTypeId;
                }
                else
                {
                    this.MainTypeComboBox.SelectedIndex = 0;
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
                this.MaterialComboBox.DataSource  = materialComboDataTable;
                this.MaterialComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.MaterialComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.materialId > 0)
                {
                    this.MaterialComboBox.SelectedValue = this.materialId;
                }
                else
                {
                    this.MaterialComboBox.SelectedIndex  = 0;
                }
            }
        }

        /// <summary>
        /// Loads the Access Type combo box.
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
        /// Loads the Surface Cover combo box.
        /// </summary>
        private void LoadSurfaceCoverComboBox()
        {
            this.CreateTempListGDocPropertyReference("SurfaceCoverID");

            DataTable surfaceCoverComboDataTable = new DataTable();
            surfaceCoverComboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocCommonData.ListGDocPropertyReference.Rows.Count > 0)
            {
                this.SurfaceCoverComboBox.DataSource = surfaceCoverComboDataTable;
                this.SurfaceCoverComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.SurfaceCoverComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                if (this.surfaceCoverId > 0)
                {
                    this.SurfaceCoverComboBox.SelectedValue  = this.surfaceCoverId;
                }
                else
                {
                    this.SurfaceCoverComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.LifeCycleStatusComboBox.SelectedIndex = -1;
            this.MainTypeComboBox.SelectedIndex = -1;
            this.MaterialComboBox.SelectedIndex = -1;
            this.AccessTypeComboBox.SelectedIndex = -1;
            this.SurfaceCoverComboBox.SelectedIndex = -1;
            this.MainDiameterComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.CIDTextBox.MaxLength  = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.CIDColumn.MaxLength;
      
            this.CommentTextBox.MaxLength = this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.CommentColumn.MaxLength;

            this.LifeCycleStatusComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.MainTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.MaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.AccessTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.SurfaceCoverComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.MainDiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
        }

        /// <summary>
        /// Saves the Sanitary Pipe Properties.
        /// </summary>
        private void SaveSanitaryPipeProperties()
        {
            int tempFeatureId;
            int keyId;
            decimal tempMainLength;
            decimal tempDownSreamDepth;
            decimal tempDownSreamElevation;
            decimal tempUpSreamDepth;
            decimal tempUpSreamElevation;
            decimal tempSlope;
            
            F84123SanitaryPipePropertiesData sanitaryPipePropertiesData1 = new F84123SanitaryPipePropertiesData();
            //this.sanitaryPipePropertiesData.GetSanitaryPipeProperties.Rows.Clear();
            F84123SanitaryPipePropertiesData.GetSanitaryPipePropertiesRow dr = sanitaryPipePropertiesData1.GetSanitaryPipeProperties.NewGetSanitaryPipePropertiesRow();
            if (!string.IsNullOrEmpty(this.PipeIdTextBox.Text.Trim()))
            {
                int.TryParse(this.PipeIdTextBox.Text.Trim(), out tempFeatureId);
                keyId = tempFeatureId;
            }
            else
            {
                ////When the PipeId text box is empty then it is 0 to be sent to db                   
                keyId = 0;
            }

            dr.FeatureClassID = this.featureClassId;

            if (!string.IsNullOrEmpty(this.CIDTextBox.Text.Trim()))
            {
                dr.CID = this.CIDTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.DownStreamDepthTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.DownStreamDepthTextBox.Text.Trim(), out tempDownSreamDepth);
                dr.DownDepth = tempDownSreamDepth;
            }

            if (!string.IsNullOrEmpty(this.DownStreamElevationTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.DownStreamElevationTextBox.Text.Trim(), out tempDownSreamElevation);
                dr.DownElevation = tempDownSreamElevation;
            }

            if (!string.IsNullOrEmpty(this.UpStreamDepthTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.UpStreamDepthTextBox.Text.Trim(), out tempUpSreamDepth);
                dr.UpDepth = tempUpSreamDepth;
            }

            if (!string.IsNullOrEmpty(this.UpStreamElevationTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.UpStreamElevationTextBox.Text.Trim(), out tempUpSreamElevation);
                dr.UpElevation = tempUpSreamElevation;
            }

            if (!string.IsNullOrEmpty(this.SlopeTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.SlopeTextBox.Text.Trim(), out tempSlope);
                dr.Slope = tempSlope;
            }

            if (!string.IsNullOrEmpty(this.MainLengthTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.MainLengthTextBox.Text.Trim(), out tempMainLength);
                dr.Length = tempMainLength;
            }

            if (!string.IsNullOrEmpty(this.CommentTextBox.Text.Trim()))
            {
                dr.Comment = this.CommentTextBox.Text.Trim();
            }

            dr.IsEnabled = this.EnabledCheckBox.Checked;
            dr.IsPrivate = this.PrivateCheckBox.Checked;
            dr.IsForced = this.ForcedCheckBox.Checked;
            
            if (!string.IsNullOrEmpty(this.LifeCycleStatusComboBox.Text.Trim()))
            {
                dr.LifecycleStatusID = Convert.ToInt32(this.LifeCycleStatusComboBox.SelectedValue);
            }
            else
            {
                dr.LifecycleStatusID = 0;
            }

            if (!string.IsNullOrEmpty(this.MainTypeComboBox.Text.Trim()))
            {
                dr.PipeTypeID = Convert.ToInt32(this.MainTypeComboBox.SelectedValue);
            }
            else
            {
                dr.PipeTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.MaterialComboBox.Text.Trim()))
            {
                dr.MaterialID = Convert.ToInt32(this.MaterialComboBox.SelectedValue);
            }
            else
            {
                dr.MaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.AccessTypeComboBox.Text.Trim()))
            {
                dr.AccessTypeID = Convert.ToInt32(this.AccessTypeComboBox.SelectedValue);
            }
            else
            {
                dr.AccessTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.SurfaceCoverComboBox.Text.Trim()))
            {
                dr.SurfaceCoverID = Convert.ToInt32(this.SurfaceCoverComboBox.SelectedValue);
            }
            else
            {
                dr.SurfaceCoverID  = 0;
            }

            if (!string.IsNullOrEmpty(this.MainDiameterComboBox.Text.Trim()))
            {
                dr.Diameter = Convert.ToDecimal(this.MainDiameterComboBox.Text.Trim());
            }

            sanitaryPipePropertiesData1.GetSanitaryPipeProperties.Rows.Add(dr);
            this.pipeId = this.form84123Control.WorkItem.F84123_SaveSanitaryPipeProperties(keyId, (Utility.GetXmlString(sanitaryPipePropertiesData1.GetSanitaryPipeProperties.Copy())), TerraScanCommon.UserId);
            this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<int>(this.pipeId));
            ////to reload the form with the current keyid(this.pipeId)
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.pipeId;
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo)); 
        }

        /// <summary>
        /// Deletes the water valve properties.
        /// </summary>
        private void DeleteSanitaryPipeProperties()
        {
            this.form84123Control.WorkItem.F84123_DeleteSanitaryPipeProperties(this.pipeId, TerraScanCommon.UserId);
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

      #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F84721 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84123_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.CustomizeSanitaryPipeProperties();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.flagLoadOnProcess = false;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PropertiesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PropertiesPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the PropertiesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PropertiesPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.SanitaryPipeDetailsToolTip.SetToolTip(this.PropertiesPictureBox, Utility.GetFormNameSpace(this.Name));
        }

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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        private void LifeCycleStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void MainTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void SurfaceCoverComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}