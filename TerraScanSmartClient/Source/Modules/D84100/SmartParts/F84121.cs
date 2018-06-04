//--------------------------------------------------------------------------------------------
// <copyright file="F84121.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84121.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 Dec 06        JYOTHI              Created
// 26 Dec 06        KARTHIKEYAN.B       Added Functionality
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
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;

    /// <summary>
    /// F84121 class file
    /// </summary>
    public partial class F84121 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// Form84121 Controller
        /// </summary>
        private F84121Controller form84121Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// instance for GDocCommentData
        /// </summary>
        private GDocCommonData gdocCommonData = new GDocCommonData(); 

        /// <summary>
        /// An instance for sanitaryManholePropertiesData
        /// </summary>
        private F84121SanitaryManholePropertiesData sanitaryManholePropertiesData = new F84121SanitaryManholePropertiesData();

        /// <summary>
        /// listGDocPropertyReference datatable
        /// </summary>
        private GDocCommonData.ListGDocPropertyReferenceDataTable listGDocPropertyReference = new GDocCommonData.ListGDocPropertyReferenceDataTable();

        /// <summary>
        /// No of rows present in the ListPropertyReference Datatable returned from DataBase
        /// </summary>
        private int gdocPropertyReferenceRowsCount;

        #endregion

        #region Variable For ComboBox Value Members

        /// <summary>
        /// Life Cycle Status Id
        /// </summary>
        private int lifeCycleStatusId;

        /// <summary>
        /// Cover Diameter
        /// </summary>
        private string coverDiameter;

        /// <summary>
        /// Cover Type Id
        /// </summary>
        private int coverTypeId;

        /// <summary>
        /// Manhole Type Id
        /// </summary>
        private int manholeTypeId;

        /// <summary>
        /// Barrel Diameter
        /// </summary>
        private string barrelDiameter;

        /// <summary>
        /// Frame Type
        /// </summary>
        private int frameTypeId;

        /// <summary>
        /// Base Material Id
        /// </summary>
        private int baseMaterialId;

        /// <summary>
        /// Bench Material Id
        /// </summary>
        private int benchMaterialId;

        /// <summary>
        /// Channel Material Id
        /// </summary>
        private int channelMaterialId;

        /// <summary>
        /// Cone Material Id
        /// </summary>
        private int coneMaterialId;

        /// <summary>
        /// Ring Material Id
        /// </summary>
        private int ringMaterialId;

        /// <summary>
        /// Step Material Id
        /// </summary>
        private int stepMaterialId;

        /// <summary>
        /// Wall Material Id
        /// </summary>
        private int wallMaterialId;

        /// <summary>
        /// Access Type Id
        /// </summary>
        private int accessTypeId;

        /// <summary>
        /// Surface Cover Id
        /// </summary>
        private int surfaceCoverId;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84121"/> class.
        /// </summary>
        public F84121()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84121"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F84121(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.PropertiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PropertiesPictureBox.Height, this.PropertiesPictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84121"/> class.
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
        public F84121(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.featureClassId = featureClassId;
            this.PropertiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PropertiesPictureBox.Height, this.PropertiesPictureBox.Width, tabText, red, green, blue);
        }
        
        #endregion
                
        #region Event Publication

        /// <summary>
        /// event publication to Show the child form 
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
        /// Declare the event D84100_F84121_OnSave_GetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84100_F84121_OnSave_GetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_OnSave_GetKeyId;

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets or sets the F84121 control.
        /// </summary>
        /// <value>The F84121 control.</value>
        [CreateNew]
        public F84121Controller Form84121Control
        {
            get { return this.form84121Control as F84121Controller; }
            set { this.form84121Control = value; }
        }
        #endregion
        
        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
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
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ClearSanitaryManholeControls();
                this.SetNewComboIndex();
                this.LockControls(true);
                this.ControlLock(false);
                this.CIDTextBox.Focus();
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearSanitaryManhole();
                this.SetNewComboIndex();
                this.LockControls(false);
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
            this.CommentPanel.Focus();
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LockControls(true);
            this.CustomizeSanitaryManholeProperties();

            this.CIDPanel.Focus();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.SaveSanitaryManholeProperties();
                    this.CIDTextBox.Focus();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.LockControls(true);
                this.ControlLock(false);
                this.CustomizeSanitaryManholeProperties();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                    this.getFormDetailsDataDetails = this.form84121Control.WorkItem.GetFormDetails(Convert.ToInt32(this.Tag), eventArgs.Data.SelectUserId);

                    if (this.getFormDetailsDataDetails.Rows.Count > 0)
                    {
                        this.slicePermissionField.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                        this.slicePermissionField.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
                        this.slicePermissionField.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
                        this.slicePermissionField.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
                        this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    }

                    if (this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows.Count > 0)
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


                if (this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows.Count > 0)
                {
                    this.ParentForm.ActiveControl = this.CIDTextBox;
                    this.ParentForm.ActiveControl.Focus();
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
                this.DeleteSanitaryManholeProperties();

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
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.CustomizeSanitaryManholeProperties();
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
        /// Event Subscription D84100_F84122_OnSave_SetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84100_F84122_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_SetKeyId(object sender, DataEventArgs<int> eventArgs)
        {
            this.keyId = eventArgs.Data;
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

        #region Form Control Events

        /// <summary>
        /// F84121 Form Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F84121_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.CustomizeSanitaryManholeProperties();
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the PropertiesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PropertiesPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.InspectionDetailsToolTip.SetToolTip(this.PropertiesPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of all the ComboBoxes in the Sanitary Manhole Properties Form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
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
        /// Check the page mode to enable or disable the save, cancel Buttons for "Change Events In Text Box/CheckBox"        
        /// </summary>
        /// <param name="sender">The source of the event.</param>
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
        /// To handle keypress event of the Textboxes, for not allowing the "Enter" Key
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion
  
        #region Methods

        /// <summary>
        /// Sets the max length for the editable textboxes and comboboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.CIDTextBox.MaxLength = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.CIDColumn.MaxLength;
            this.CommentTextBox.MaxLength = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.CommentColumn.MaxLength;

            this.LifeCycleStatusComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.CoverDiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.CoverTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.ManholeTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.BarrelDiameterComboBox.MaxLength = this.gdocCommonData.ListGDocDiameter.DiameterColumn.MaxLength;
            this.FrameTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.BaseMaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.BenchMaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.ChannelMaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.ConeMaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.RingMaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.StepMaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.WallMaterialComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.AccessTypeComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.SurfaceCoverComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
        }

        /// <summary>
        /// To Clear Sanitary Manhole Properties Textboxes and Checkboxes
        /// </summary>
        private void ClearSanitaryManholeControls()
        {
            this.ManholeIdTextBox.Text = string.Empty;
            this.CIDTextBox.Text = string.Empty;
            this.RingElevationTextBox.Text = string.Empty;
            this.FlowElevationTextBox.Text = string.Empty;
            this.DepthTextBox.Text = string.Empty;            
            this.CommentTextBox.Text = string.Empty;

            this.EnabledCheckBox.Checked = false;
            this.PrivateCheckBox.Checked = false;
        }

        /// <summary>
        /// To Clear the entire Sanitary Manhole Form
        /// </summary>
        private void ClearSanitaryManhole()
        {
            this.ClearComboBox(this);
            this.ClearSanitaryManholeControls();
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
        /// To Enable/Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Lock Control</param>
        private void LockControls(bool lockControl)
        {
            this.ManholeIdPanel.Enabled = lockControl;
            this.CIDPanel.Enabled = lockControl;
            this.EnabledPanel.Enabled = lockControl;
            this.PrivatePanel.Enabled = lockControl;
            this.LifeCycleStatusPanel.Enabled = lockControl;
            this.CoverDiameterPanel.Enabled = lockControl;
            this.CoverTypePanel.Enabled = lockControl;
            this.ManholeTypePanel.Enabled = lockControl;
            this.RingElevationPanel.Enabled = lockControl;
            this.FlowElevationPanel.Enabled = lockControl;
            this.DepthPanel.Enabled = lockControl;
            this.BarrelDiameterPanel.Enabled = lockControl;
            this.FrameTypePanel.Enabled = lockControl;
            this.BaseMaterialPanel.Enabled = lockControl;
            this.BenchMaterialPanel.Enabled = lockControl;
            this.ChannelMaterialPanel.Enabled = lockControl;
            this.ConeMaterialPanel.Enabled = lockControl;
            this.RingMaterialPanel.Enabled = lockControl;
            this.StepMaterialPanel.Enabled = lockControl;
            this.WallMaterialPanel.Enabled = lockControl;                        
            this.AccessTypePanel.Enabled = lockControl;
            this.SurfaceCoverPanel.Enabled = lockControl;
            this.CommentPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Control Lock</param>
        private void ControlLock(bool controlLook)
        {            
            this.CIDTextBox.LockKeyPress = controlLook;
            this.RingElevationTextBox.LockKeyPress = controlLook;
            this.FlowElevationTextBox.LockKeyPress = controlLook;
            this.DepthTextBox.LockKeyPress = controlLook;
            this.CommentTextBox.LockKeyPress = controlLook;

            this.EnabledCheckBox.Enabled = !controlLook;
            this.PrivateCheckBox.Enabled = !controlLook;

            this.LifeCycleStatusComboBox.Enabled = !controlLook;
            this.CoverDiameterComboBox.Enabled = !controlLook;
            this.CoverTypeComboBox.Enabled = !controlLook;
            this.ManholeTypeComboBox.Enabled = !controlLook;
            this.BarrelDiameterComboBox.Enabled = !controlLook;
            this.FrameTypeComboBox.Enabled = !controlLook;
            this.BaseMaterialComboBox.Enabled = !controlLook;
            this.BenchMaterialComboBox.Enabled = !controlLook;
            this.ChannelMaterialComboBox.Enabled = !controlLook;
            this.ConeMaterialComboBox.Enabled = !controlLook;
            this.RingMaterialComboBox.Enabled = !controlLook;
            this.StepMaterialComboBox.Enabled = !controlLook;
            this.WallMaterialComboBox.Enabled = !controlLook;
            this.AccessTypeComboBox.Enabled = !controlLook;
            this.SurfaceCoverComboBox.Enabled = !controlLook;
        }
        
        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.LifeCycleStatusComboBox.SelectedIndex = -1;
            this.CoverDiameterComboBox.SelectedIndex = -1;
            this.CoverTypeComboBox.SelectedIndex = -1;
            this.ManholeTypeComboBox.SelectedIndex = -1;
            this.BarrelDiameterComboBox.SelectedIndex = -1;
            this.FrameTypeComboBox.SelectedIndex = -1;
            this.BaseMaterialComboBox.SelectedIndex = -1;
            this.BenchMaterialComboBox.SelectedIndex = -1;
            this.ChannelMaterialComboBox.SelectedIndex = -1;
            this.ConeMaterialComboBox.SelectedIndex = -1;
            this.RingMaterialComboBox.SelectedIndex = -1;
            this.StepMaterialComboBox.SelectedIndex = -1;
            this.WallMaterialComboBox.SelectedIndex = -1;
            this.AccessTypeComboBox.SelectedIndex = -1;
            this.SurfaceCoverComboBox.SelectedIndex = -1;
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
        /// To enable the Edit button in nthe Master Form
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// To load all the values in Sanitary Manhole Properties Form
        /// </summary>
        private void CustomizeSanitaryManholeProperties()
        {
            this.Cursor = Cursors.WaitCursor;
            this.sanitaryManholePropertiesData = this.form84121Control.WorkItem.F84121_GetSanitaryManholeProperties(this.keyId);

            if (this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows.Count > 0)
            {
                this.ManholeIdTextBox.Text = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.FeatureIDColumn].ToString();
                this.CIDTextBox.Text = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.CIDColumn].ToString();

                this.RingElevationTextBox.Text = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.RingElevationColumn].ToString();
                this.FlowElevationTextBox.Text = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.FlowElevationColumn].ToString();
                this.DepthTextBox.Text = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.DepthColumn].ToString();
                this.CommentTextBox.Text = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.CommentColumn].ToString();

                this.EnabledCheckBox.Checked = Convert.ToBoolean(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.IsEnabledColumn].ToString());
                this.PrivateCheckBox.Checked = Convert.ToBoolean(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.IsPrivateColumn].ToString());

                this.lifeCycleStatusId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.LifecycleStatusIDColumn]);
                this.coverDiameter = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.CoverDiameterColumn].ToString();
                this.coverTypeId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.CoverTypeIDColumn]);
                this.manholeTypeId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.ManholeTypeIDColumn]);
                this.barrelDiameter = this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.BarrelDiameterColumn].ToString();

                this.frameTypeId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.FrameTypeIDColumn]);
                this.baseMaterialId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.BaseMaterialIDColumn]);
                this.benchMaterialId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.BenchMaterialIDColumn]);
                this.channelMaterialId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.ChannelMaterialIDColumn]);
                this.coneMaterialId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.ConeMaterialIDColumn]);
                this.ringMaterialId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.RingMaterialIDColumn]);
                this.stepMaterialId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.StepMaterialIDColumn]);
                this.wallMaterialId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.WallMaterialIDColumn]);
                this.accessTypeId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.AccessTypeIDColumn]);
                this.surfaceCoverId = Convert.ToInt32(this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows[0][this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.SurfaceCoverIDColumn]);
            }
            else
            {
                this.ClearSanitaryManhole();
                this.LockControls(false);
            }

            ////To Load All The ComboBoxes
            this.LoadAllCombOBox();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Create Temporary Property Reference for GDoc
        /// </summary>
        /// <param name="refFieldValue">refFieldValue</param>
        private void CreateTempListGDocPropertyReference(string refFieldValue)
        {
            this.listGDocPropertyReference.Clear();
            DataRow customRow = this.listGDocPropertyReference.NewRow();
            customRow[this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName] = string.Empty;
            customRow[this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName] = "0";
            this.listGDocPropertyReference.Rows.Add(customRow);

            ////to clear the this.gdocCommonData.ListGDocPropertyReference
            this.gdocCommonData.ListGDocPropertyReference.Clear();
            this.gdocCommonData = this.form84121Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
            this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
            this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
        }

        #region Load Methods for all the Comboboxes

        /// <summary>
        /// Populate all the comboboxes in the Sanitary Manhole Properties Form
        /// </summary>
        private void LoadAllCombOBox()
        {
            this.LoadStringComboBoxes("LifeCycleStatusID", this.LifeCycleStatusComboBox, this.lifeCycleStatusId);
            this.LoadDiameterComboBoxes(this.CoverDiameterComboBox, this.coverDiameter);
            this.LoadStringComboBoxes("CoverTypeID", this.CoverTypeComboBox, this.coverTypeId);
            this.LoadStringComboBoxes("ManholeTypeID", this.ManholeTypeComboBox, this.manholeTypeId);
            this.LoadDiameterComboBoxes(this.BarrelDiameterComboBox, this.barrelDiameter);
            this.LoadStringComboBoxes("FrameTypeID", this.FrameTypeComboBox, this.frameTypeId);
            this.LoadStringComboBoxes("BaseMaterialID", this.BaseMaterialComboBox, this.baseMaterialId);
            this.LoadStringComboBoxes("BenchMaterialID", this.BenchMaterialComboBox, this.benchMaterialId);
            this.LoadStringComboBoxes("ChannelMaterialID", this.ChannelMaterialComboBox, this.channelMaterialId);
            this.LoadStringComboBoxes("ConeMaterialID", this.ConeMaterialComboBox, this.coneMaterialId);
            this.LoadStringComboBoxes("RingMaterialID", this.RingMaterialComboBox, this.ringMaterialId);
            this.LoadStringComboBoxes("StepMaterialID", this.StepMaterialComboBox, this.stepMaterialId);
            this.LoadStringComboBoxes("WallMaterialID", this.WallMaterialComboBox, this.wallMaterialId);
            this.LoadStringComboBoxes("AccessTypeID", this.AccessTypeComboBox, this.accessTypeId);
            this.LoadStringComboBoxes("SurfaceCoverID", this.SurfaceCoverComboBox, this.surfaceCoverId);
        }

        /// <summary>
        /// Generic Load methods for Loading Data in the Diameter Comboboxes
        /// </summary>
        /// <param name="comboControl">The Combobox Control to load</param>
        /// <param name="comboText">The Text to be displayed in the combobox</param>
        private void LoadDiameterComboBoxes(Control comboControl, string comboText)
        {
            GDocCommonData.ListGDocDiameterDataTable listGDocDiameter = new GDocCommonData.ListGDocDiameterDataTable();

            DataRow customRow = listGDocDiameter.NewRow();
            listGDocDiameter.Clear();
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName] = DBNull.Value;
            customRow[this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName] = "0";
            listGDocDiameter.Rows.Add(customRow);

            this.gdocCommonData = this.form84121Control.WorkItem.F8000_GetGDocDiameter(this.featureClassId);
            listGDocDiameter.Merge(this.gdocCommonData.ListGDocDiameter);

            if (this.gdocCommonData.ListGDocDiameter.Rows.Count > 0)
            {
                if (comboControl.GetType() == typeof(TerraScanComboBox))
                {
                    TerraScanComboBox currentComboBox = (TerraScanComboBox)comboControl;
                    currentComboBox.DataSource = listGDocDiameter;
                    currentComboBox.DisplayMember = this.gdocCommonData.ListGDocDiameter.DiameterColumn.ColumnName;
                    currentComboBox.ValueMember = this.gdocCommonData.ListGDocDiameter.DiameterIDColumn.ColumnName;
                    if (!string.IsNullOrEmpty(comboText))
                    {
                        currentComboBox.Text = comboText;
                    }
                    else
                    {
                        currentComboBox.SelectedIndex = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Generic Load methods for Loading Data in the Comboboxes
        /// </summary>
        /// <param name="referenceType">Reference Type for CreateTempListGDocPropertyReference</param>
        /// <param name="comboControl">The Combobox Control to load</param>
        /// <param name="typeId">The Type ID for Combobox Index</param>
        private void LoadStringComboBoxes(string referenceType, Control comboControl, int typeId)
        {
            this.CreateTempListGDocPropertyReference(referenceType);

            DataTable comboDataTable = new DataTable();
            comboDataTable.Merge(this.listGDocPropertyReference);

            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                if (comboControl.GetType() == typeof(TerraScanComboBox))
                {
                    TerraScanComboBox currentComboBox = (TerraScanComboBox)comboControl;
                    currentComboBox.DataSource = comboDataTable;
                    currentComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                    currentComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                    if (typeId > 0)
                    {
                        currentComboBox.SelectedValue = typeId;
                    }
                    else
                    {
                        currentComboBox.SelectedIndex = 0;
                    }
                }
            }
        }

        #endregion

        #region Save & Delete Methods
        
        ///<summary>
        ///To save all the values in Sanitary Manhole Properties Form
        ///</summary>
        private void SaveSanitaryManholeProperties()
        {
            int tempFeatureId;
            int tempKeyId;
            decimal tempRingElevation;
            decimal tempFlowElevation;
            decimal tempDepth;

            this.Cursor = Cursors.WaitCursor;

            F84121SanitaryManholePropertiesData sanitaryManholePropertiesData1 = new F84121SanitaryManholePropertiesData();
            //this.sanitaryManholePropertiesData.GetSanitaryManholeProperties.Rows.Clear();
            F84121SanitaryManholePropertiesData.GetSanitaryManholePropertiesRow dr = sanitaryManholePropertiesData1.GetSanitaryManholeProperties.NewGetSanitaryManholePropertiesRow();

            if (!string.IsNullOrEmpty(this.ManholeIdTextBox.Text.Trim()))
            {
                int.TryParse(this.ManholeIdTextBox.Text.Trim(), out tempFeatureId);
                tempKeyId = tempFeatureId;
            }
            else
            {
                ////When the ManholeId text box is empty then it is 0 to be sent to db                   
                tempKeyId = 0;
            }

            dr.FeatureClassID = this.featureClassId;

            #region String Textboxes

            if (!string.IsNullOrEmpty(this.CIDTextBox.Text.Trim()))
            {
                dr.CID = this.CIDTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.CommentTextBox.Text.Trim()))
            {
                dr.Comment = this.CommentTextBox.Text.Trim();
            }

            #endregion

            #region Decimal Textboxes

            if (!string.IsNullOrEmpty(this.RingElevationTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.RingElevationTextBox.Text.Trim(), out tempRingElevation);
                dr.RingElevation = tempRingElevation;
            }

            if (!string.IsNullOrEmpty(this.FlowElevationTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.FlowElevationTextBox.Text.Trim(), out tempFlowElevation);
                dr.FlowElevation = tempFlowElevation;
            }

            if (!string.IsNullOrEmpty(this.DepthTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.DepthTextBox.Text.Trim(), out tempDepth);
                dr.Depth = tempDepth;
            }

            #endregion

            #region Checkboxes

            dr.IsEnabled = this.EnabledCheckBox.Checked;
            dr.IsPrivate = this.PrivateCheckBox.Checked;

            #endregion

            #region ComboBoxes

            if (!string.IsNullOrEmpty(this.LifeCycleStatusComboBox.Text.Trim()))
            {
                dr.LifecycleStatusID = Convert.ToInt32(this.LifeCycleStatusComboBox.SelectedValue);
            }
            else
            {
                dr.LifecycleStatusID = 0;
            }

            if (!string.IsNullOrEmpty(this.CoverDiameterComboBox.Text.Trim()))
            {
                dr.CoverDiameter = Convert.ToDecimal(this.CoverDiameterComboBox.Text.Trim());
            }

            if (!string.IsNullOrEmpty(this.CoverTypeComboBox.Text.Trim()))
            {
                dr.CoverTypeID = Convert.ToInt32(this.CoverTypeComboBox.SelectedValue);
            }
            else
            {
                dr.CoverTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.ManholeTypeComboBox.Text.Trim()))
            {
                dr.ManholeTypeID = Convert.ToInt32(this.ManholeTypeComboBox.SelectedValue);
            }
            else
            {
                dr.ManholeTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.BarrelDiameterComboBox.Text.Trim()))
            {
                dr.BarrelDiameter = Convert.ToDecimal(this.BarrelDiameterComboBox.Text.Trim());
            }

            if (!string.IsNullOrEmpty(this.FrameTypeComboBox.Text.Trim()))
            {
                dr.FrameTypeID = Convert.ToInt32(this.FrameTypeComboBox.SelectedValue);
            }
            else
            {
                dr.FrameTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.BaseMaterialComboBox.Text.Trim()))
            {
                dr.BaseMaterialID = Convert.ToInt32(this.BaseMaterialComboBox.SelectedValue);
            }
            else
            {
                dr.BaseMaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.BenchMaterialComboBox.Text.Trim()))
            {
                dr.BenchMaterialID = Convert.ToInt32(this.BenchMaterialComboBox.SelectedValue);
            }
            else
            {
                dr.BenchMaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.ChannelMaterialComboBox.Text.Trim()))
            {
                dr.ChannelMaterialID = Convert.ToInt32(this.ChannelMaterialComboBox.SelectedValue);
            }
            else
            {
                dr.ChannelMaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.ConeMaterialComboBox.Text.Trim()))
            {
                dr.ConeMaterialID = Convert.ToInt32(this.ConeMaterialComboBox.SelectedValue);
            }
            else
            {
                dr.ConeMaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.RingMaterialComboBox.Text.Trim()))
            {
                dr.RingMaterialID = Convert.ToInt32(this.RingMaterialComboBox.SelectedValue);
            }
            else
            {
                dr.RingMaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.StepMaterialComboBox.Text.Trim()))
            {
                dr.StepMaterialID = Convert.ToInt32(this.StepMaterialComboBox.SelectedValue);
            }
            else
            {
                dr.StepMaterialID = 0;
            }

            if (!string.IsNullOrEmpty(this.WallMaterialComboBox.Text.Trim()))
            {
                dr.WallMaterialID = Convert.ToInt32(this.WallMaterialComboBox.SelectedValue);
            }
            else
            {
                dr.WallMaterialID = 0;
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
                dr.SurfaceCoverID = 0;
            }

            #endregion

            ////this.ToEnableEditButtonInMasterForm();
            sanitaryManholePropertiesData1.GetSanitaryManholeProperties.Rows.Add(dr);
            this.keyId = this.form84121Control.WorkItem.F84121_SaveSanitaryManholeProperties(tempKeyId, (Utility.GetXmlString(sanitaryManholePropertiesData1.GetSanitaryManholeProperties.Copy())), TerraScanCommon.UserId);
            this.CIDTextBox.Focus();
            ////to send this keyid(this.keyId) to 84122
            this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<int>(this.keyId));
            ////to reload the form with the current keyid(this.keyId)
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.keyId;
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// To delete the values in Sanitary Manhole Properties Form
        /// </summary>
        private void DeleteSanitaryManholeProperties()
        {
            this.Cursor = Cursors.WaitCursor;
            this.form84121Control.WorkItem.F84121_DeleteSanitaryManholeProperties(this.keyId, TerraScanCommon.UserId);
            this.Cursor = Cursors.Default;
        }

        #endregion

        /// <summary>
        /// Handles the TextChanged event of the ManholeIdTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ManholeIdTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        #endregion

        private void ManholeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void BaseMaterialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void LifeCycleStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void CoverDiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void CoverTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void BarrelDiameterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void ConeMaterialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void RingMaterialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void StepMaterialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void WallMaterialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void AccessTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        private void SurfaceCoverComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }
    }
}
