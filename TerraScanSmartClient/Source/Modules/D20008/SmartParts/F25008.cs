//--------------------------------------------------------------------------------------------
// <copyright file="F25008.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F20008.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 April 06      VINAYAGAMURTHY H    Created
//*********************************************************************************/

namespace D20008
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
    /// F25008 Class
    /// </summary>
    public partial class F25008 : BaseSmartPart
    {
        #region F25008 Variables

        /// <summary>
        /// Used to store the parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// controller F25008
        /// </summary>
        private F25008Controller form25008Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// parcelMiscellaneousConfigData Data Set
        /// </summary>
        private F25008ParcelMiscellaneousData parcelMiscellaneousConfigData = new F25008ParcelMiscellaneousData();

        /// <summary>
        /// ParcelMiscellaneous Config Table
        /// </summary>
        private F25008ParcelMiscellaneousData.GetParcelMiscellaneousConfigurationDataTable parcelMiscellaneousConfigTable = new F25008ParcelMiscellaneousData.GetParcelMiscellaneousConfigurationDataTable();

        /// <summary>
        /// parcelMiscellaneous Data set
        /// </summary>
        private F25008ParcelMiscellaneousData parcelMiscellaneousData = new F25008ParcelMiscellaneousData();

        /// <summary>
        /// parcelMiscellaneous Data Table
        /// </summary>
        private F25008ParcelMiscellaneousData.GetParcelMiscellaneousDataTable parcelMiscellaneousTable = new F25008ParcelMiscellaneousData.GetParcelMiscellaneousDataTable();

        #endregion F25008 Variables

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

        #region F25008 Constructor

        /// <summary>
        /// F25008 Cons
        /// </summary>
        public F25008()
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
        public F25008(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.parcelId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.MiscellaneousPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(196, 42, tabText, red, green, blue);
        }

        #endregion F25008 Constructor

        #region Property

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        /// <summary>
        /// For F25011Control
        /// </summary>
        [CreateNew]
        public F25008Controller Form25008Control
        {
            get { return this.form25008Control as F25008Controller; }
            set { this.form25008Control = value; }
        }

        #endregion Property

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

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
                ////this.pageMode = TerraScanCommon.PageModeTypes.New;
                ////this.SearchStreetNameTextBox.Text = string.Empty;
                ////this.SearchCityTextBox.Text = string.Empty;
                ////this.ClearStreetListingGrid(); ;

                ////////to Enable the header part
                ////this.HeaderMaintenancePanel.Enabled = true;

                ////this.PopulateCityDirSufCombox(this.CityComboBox, string.Empty);
                ////this.PopulateCityDirSufCombox(this.SuffixComboBox, string.Empty);
                ////this.PopulateCityDirSufCombox(this.DirectionalComboBox, string.Empty);
                ////this.LockControls(true);
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
                this.LoadParcelMiscellaneousText();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                        this.SaveParcelMiscellaneous();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    ////this.CustomizeWaterValveProperties();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
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

                    if (this.parcelMiscellaneousTable.Rows.Count > 0)
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
                    this.LoadParcelMiscellaneousText();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
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
                SliceReloadActiveRecord sliceReloadActiveRecord;
                sliceReloadActiveRecord.MasterFormNo = 20000;
                sliceReloadActiveRecord.SelectedKeyId = this.parcelId;
                OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            }
        }

        #endregion Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            D9030_F9030_LoadSliceDetails(this, eventArgs);

        }


        #region Events

        /// <summary>
        /// Miscellaneous PictureBox Click Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MiscellaneousPictureBox_Click(object sender, EventArgs e)
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
        /// Miscellaneous PictureBox MouseEnter Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MiscellaneousPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MiscellaneousToolTip.SetToolTip(this.MiscellaneousPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Form 25008 Load Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void F25008_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.parcelMiscellaneousConfigData = this.form25008Control.WorkItem.F25008_ParcelMiscellaneousConfigData();
                this.parcelMiscellaneousConfigTable.Merge(this.parcelMiscellaneousConfigData.GetParcelMiscellaneousConfiguration);
                this.MaskParcelMiscellaneous();
                this.LoadParcelMiscellaneousLabel();
                this.LoadParcelMiscellaneousText();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// MID1Label MouseEnter Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MID1Label_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MiscellaneousToolTip.SetToolTip(this.MID1Label, this.MID1Label.Text.Trim() + ":" + this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID1DescriptionColumn.ColumnName].ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// MID2Label MouseEnter Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MID2Label_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MiscellaneousToolTip.SetToolTip(this.MID2Label, this.MID2Label.Text.Trim() + ":" + this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID2DescriptionColumn.ColumnName].ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// MID3Label MouseEnter Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MID3Label_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MiscellaneousToolTip.SetToolTip(this.MID3Label, this.MID3Label.Text.Trim() + ":" + this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID3DescriptionColumn.ColumnName].ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// MID4Label MouseEnter Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MID4Label_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MiscellaneousToolTip.SetToolTip(this.MID4Label, this.MID4Label.Text.Trim() + ":" + this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID4DescriptionColumn.ColumnName].ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// MID5Label MouseEnter Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void MID5Label_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MiscellaneousToolTip.SetToolTip(this.MID5Label, this.MID5Label.Text.Trim() + ":" + this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID5DescriptionColumn.ColumnName].ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.MID1Panel.Enabled = lockControl;
            this.MID2Panel.Enabled = lockControl;
            this.MID3Panel.Enabled = lockControl;
            this.MID4Panel.Enabled = lockControl;
            this.MID5Panel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.MID1Panel.Enabled = !controlLook;
            this.MID2Panel.Enabled = !controlLook;
            this.MID3Panel.Enabled = !controlLook;
            this.MID4Panel.Enabled = !controlLook;
            this.MID5Panel.Enabled = !controlLook;
        }

        /// <summary>
        /// Load ParcelMiscellaneous Label
        /// </summary>
        private void LoadParcelMiscellaneousLabel()
        {
            this.MID1Label.Text = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID1LabelColumn.ColumnName].ToString() + ":";
            this.MID2Label.Text = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID2LabelColumn.ColumnName].ToString() + ":";
            this.MID3Label.Text = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID3LabelColumn.ColumnName].ToString() + ":";
            this.MID4Label.Text = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID4LabelColumn.ColumnName].ToString() + ":";
            this.MID5Label.Text = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID5LabelColumn.ColumnName].ToString() + ":";
        }

        /// <summary>
        /// To Load Parcel MiscellaneousText
        /// </summary>
        private void LoadParcelMiscellaneousText()
        {
            this.parcelMiscellaneousTable.Clear();
            this.parcelMiscellaneousData = this.form25008Control.WorkItem.F25008_ParcelMiscellaneousData(this.parcelId);
            this.parcelMiscellaneousTable.Merge(this.parcelMiscellaneousData.GetParcelMiscellaneous);

            if (this.parcelMiscellaneousTable.Rows.Count > 0)
            {
                this.MID1MaskedTextbox.Text = this.parcelMiscellaneousTable.Rows[0][this.parcelMiscellaneousTable.MID1Column.ColumnName].ToString();
                this.MID2MaskedTextbox.Text = this.parcelMiscellaneousTable.Rows[0][this.parcelMiscellaneousTable.MID2Column.ColumnName].ToString();
                this.MID3MaskedTextbox.Text = this.parcelMiscellaneousTable.Rows[0][this.parcelMiscellaneousTable.MID3Column.ColumnName].ToString();
                this.MID4MaskedTextbox.Text = this.parcelMiscellaneousTable.Rows[0][this.parcelMiscellaneousTable.MID4Column.ColumnName].ToString();
                this.MID5MaskedTextbox.Text = this.parcelMiscellaneousTable.Rows[0][this.parcelMiscellaneousTable.MID5Column.ColumnName].ToString();
            }
            else
            {
                this.MID1MaskedTextbox.Text = string.Empty;
                this.MID2MaskedTextbox.Text = string.Empty;
                this.MID3MaskedTextbox.Text = string.Empty;
                this.MID4MaskedTextbox.Text = string.Empty;
                this.MID5MaskedTextbox.Text = string.Empty;
            }
        }

        /// <summary>
        /// To Mask Parcel Miscellaneous
        /// </summary>
        private void MaskParcelMiscellaneous()
        {
            this.MID1MaskedTextbox.Mask = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID1MaskColumn.ColumnName].ToString();
            this.MID2MaskedTextbox.Mask = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID2MaskColumn.ColumnName].ToString();
            this.MID3MaskedTextbox.Mask = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID3MaskColumn.ColumnName].ToString();
            this.MID4MaskedTextbox.Mask = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID4MaskColumn.ColumnName].ToString();
            this.MID5MaskedTextbox.Mask = this.parcelMiscellaneousConfigTable.Rows[0][this.parcelMiscellaneousConfigTable.AA_ID5MaskColumn.ColumnName].ToString();
        }

        /// <summary>
        /// To Save the Parcel Miscellaneous
        /// </summary>
        private void SaveParcelMiscellaneous()
        {
            this.parcelMiscellaneousData.GetParcelMiscellaneous.Clear();
            F25008ParcelMiscellaneousData.GetParcelMiscellaneousRow dr = this.parcelMiscellaneousData.GetParcelMiscellaneous.NewGetParcelMiscellaneousRow();
            dr.ParcelID = this.parcelId;
            dr.MID1 = this.MID1MaskedTextbox.Text;
            dr.MID2 = this.MID2MaskedTextbox.Text;
            dr.MID3 = this.MID3MaskedTextbox.Text;
            dr.MID4 = this.MID4MaskedTextbox.Text;
            dr.MID5 = this.MID5MaskedTextbox.Text;
            this.parcelMiscellaneousData.GetParcelMiscellaneous.Rows.Add(dr);
            this.form25008Control.WorkItem.F25008_SaveParcelMiscellaneous(this.parcelId, (Utility.GetXmlString(this.parcelMiscellaneousData.GetParcelMiscellaneous.Copy())), TerraScanCommon.UserId);
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
        /// All TextBox change Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void AllTextBoxTextChangedEvent(object sender, EventArgs e)
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

        #endregion Methods
    }
}