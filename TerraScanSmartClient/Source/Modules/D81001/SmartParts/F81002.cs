//--------------------------------------------------------------------------------------------
// <copyright file="F81002.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F81002.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		 ---------------------------------------------------------
// 07/11/2007      Jaya Prakash.K     Created// 
//*********************************************************************************/

namespace D81001
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
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infrastructure.Interface;

    /// <summary>
    /// class inheriting basesmartpart
    /// </summary>
    [SmartPart]
    public partial class F81002 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the Fee ID
        /// </summary>
        private int feeID;

        /// <summary>
        /// Usede to store the rowcount of event fee
        /// </summary>
        private int eventFeeDatatableRowCount;

        /// <summary>
        /// Used to store the eventFeeData
        /// </summary>
        private F81002EventFeeData eventFeeData = new F81002EventFeeData();

        /// <summary>
        /// Used to store the GetFeeEventDataTable
        /// </summary>
        private F81002EventFeeData.GetFeeEventDataTable getFeeEventDataTable = new F81002EventFeeData.GetFeeEventDataTable();

        /// <summary>
        /// controller F81002
        /// </summary>
        private F81002Controller form81002Control;

        /// <summary>
        /// Used to store the feeCataLogId
        /// </summary>
        private int feeCataLogId;

        #region EF Labels Fields Formula

        /// <summary>
        /// used to store the eventFeeFieldsFormula1
        /// </summary>
        private string eventFeeFieldsFormula1 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula2
        /// </summary>
        private string eventFeeFieldsFormula2 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula3
        /// </summary>
        private string eventFeeFieldsFormula3 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula4
        /// </summary>
        private string eventFeeFieldsFormula4 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula5
        /// </summary>
        private string eventFeeFieldsFormula5 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula6
        /// </summary>
        private string eventFeeFieldsFormula6 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula7
        /// </summary>
        private string eventFeeFieldsFormula7 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula8
        /// </summary>
        private string eventFeeFieldsFormula8 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula9
        /// </summary>
        private string eventFeeFieldsFormula9 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula10
        /// </summary>
        private string eventFeeFieldsFormula10 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula11
        /// </summary>
        private string eventFeeFieldsFormula11 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula12
        /// </summary>
        private string eventFeeFieldsFormula12 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula13
        /// </summary>
        private string eventFeeFieldsFormula13 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula14
        /// </summary>
        private string eventFeeFieldsFormula14 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula15
        /// </summary>
        private string eventFeeFieldsFormula15 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula16
        /// </summary>
        private string eventFeeFieldsFormula16 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula17
        /// </summary>
        private string eventFeeFieldsFormula17 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula18
        /// </summary>
        private string eventFeeFieldsFormula18 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula19
        /// </summary>
        private string eventFeeFieldsFormula19 = string.Empty;

        /// <summary>
        /// used to store the eventFeeFieldsFormula20
        /// </summary>
        private string eventFeeFieldsFormula20 = string.Empty;

        /// <summary>
        /// Used to store the vformula
        /// </summary>
        private string vformula = string.Empty;

        #endregion EF Labels Fields Formula

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Used to store the eventId
        /// </summary>
        private int eventId;

        /// <summary>
        /// saveComplete
        /// </summary>
        private bool saveUncomplete;

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool unsavedChangeExists;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Instance for GDocEventHeaderData-To check the Event-Header
        /// </summary>
        private GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();

        #endregion Variables

        #region Form Slice Variables

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
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="F81002"/> class.
        /// </summary>
        public F81002()
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
        public F81002(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;

            this.eventId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;

            this.formMasterPermissionEdit = permissionEdit;

            this.GDocEventFeePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GDocEventFeePictureBox.Height, this.GDocEventFeePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Constructors

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F36011Control
        /// </summary>
        [CreateNew]
        public F81002Controller Form81002Control
        {
            get { return this.form81002Control as F81002Controller; }
            set { this.form81002Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                        this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                        this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                        this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;


                        if (this.eventId != eventArgs.Data.KeyId)
                        {
                            ////To check the invalid key id in set slice event subscription db call is set to F82001_ListLandDetails Method to check invalid key id
                            this.gdocEventHeaderData = this.form81002Control.WorkItem.GetGDocEventHeader(this.eventId);
                        }

                        if (this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0)
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
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        if (this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0)
                        {
                            this.ControlLock(false);
                        }
                        else
                        {
                            this.ControlLock(true);
                        }
                    }
                    else
                    {
                        this.ControlLock(true);
                    }
                }
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
                    {
                        this.ControlLock(false);
                        this.CodeCatalogButton.Enabled = true;
                        this.DescriptionTextBox.Enabled = true;
                        this.CodeCatalogButton.Focus();
                        this.LoadEventFee();
                    }
                    else
                    {
                        this.ControlLock(true);
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.PermissionFiled.deletePermission)
                    {
                        this.Form81002Control.WorkItem.F81002_DeleteEventFee(this.feeID, TerraScanCommon.UserId);
                        SliceFormCloseAlert sliceFormCloseAlert;
                        sliceFormCloseAlert.FormNo = this.masterFormNo;
                        sliceFormCloseAlert.FlagFormClose = true;
                        this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                    }
                }
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    ////this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    {
                        this.SaveEventFee();
                        this.CodeCatalogButton.Enabled = true;
                        this.DescriptionTextBox.Focus();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                    else
                    {
                        this.ControlLock(true);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.DescriptionTextBox.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// Load Slice Detials
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.eventId = eventArgs.Data.SelectedKeyId;
                        this.LoadEventFee();
                        this.CodeCatalogButton.Focus();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    }
                }
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

        #endregion  Protected methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F81002 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F81002_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadEventFee();
                this.CodeCatalogButton.Focus();
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
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
        /// Handles the TextChanged event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
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
        /// set tool tip for the label.
        /// </summary>
        /// <param name="sender">sender of the control</param>
        /// <param name="e">event args</param>
        private void Label_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label tempLabel = (Label)sender;
                if (tempLabel.Text.Trim().Length > 10)
                {
                    EventFeeFormSliceToolTip.RemoveAll();
                    EventFeeFormSliceToolTip.SetToolTip(tempLabel, tempLabel.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the GDocEventFeePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GDocEventFeePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.EventFeeFormSliceToolTip.SetToolTip(this.GDocEventFeePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Fee's label text box leave.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EFLabelTextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                this.flagLoadOnProcess = true;
                if (this.unsavedChangeExists)
                {
                    this.AssignCalculatedValue();
                }

                this.flagLoadOnProcess = false;
                this.CheckLabelValues();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Texts the box change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxChange(object sender, EventArgs e)
        {
            try
            {
                TerraScanTextBox tempTextBox = (TerraScanTextBox)sender;
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(tempTextBox.Text.Trim()))
                    {
                        this.EventTotalFeeCalucationGrid.Rows[0].Cells[tempTextBox.Tag.ToString()].Value = tempTextBox.Text.Trim();
                    }
                    else
                    {
                        this.EventTotalFeeCalucationGrid.Rows[0].Cells[tempTextBox.Tag.ToString()].Value = DBNull.Value;
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CodeCatalogButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CodeCatalogButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(86001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.feeCataLogId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the GDocEventFeePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GDocEventFeePictureBox_Click(object sender, EventArgs e)
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

        #region grid errors

        /// <summary>
        /// Handles the FormulaSyntaxError event of the UltraMIfieldsCalcManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaSyntaxErrorEventArgs"/> instance containing the event data.</param>
        private void UltraMIfieldsCalcManager_FormulaSyntaxError(object sender, FormulaSyntaxErrorEventArgs e)
        {
            try
            {
                e.DisplayErrorMessage = false;
                MessageBox.Show(e.ErrorDisplayText, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormulaCircularityError event of the UltraMIfieldsCalcManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaCircularityErrorEventArgs"/> instance containing the event data.</param>
        private void UltraMIfieldsCalcManager_FormulaCircularityError(object sender, FormulaCircularityErrorEventArgs e)
        {
            try
            {
                e.DisplayErrorMessage = false;
                MessageBox.Show(e.ErrorDisplayText, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion grid errors

        #endregion Events

        #region private methods

        /// <summary>
        /// Loads the entire Event Fee.
        /// </summary>
        private void LoadEventFee()
        {
            this.flagLoadOnProcess = true;
            this.ControlLock(false);

            ////this.LoadEventFeecalculation(this.eventId, this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
            this.LoadEventFeecalculation(this.PermissionFiled.editPermission, this.formMasterPermissionEdit);
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// To Lock the Controls 
        /// </summary>
        /// <param name="controlLook">
        /// true - to Set the control as not editable
        /// false - to Set the control as editable
        /// </param>
        private void ControlLock(bool controlLook)
        {
            this.CodeCatalogButton.Enabled = !controlLook;
            this.DescriptionTextBox.LockKeyPress = controlLook;
        }

        /// <summary>
        /// Loads the event feecalculation.
        /// </summary>
        /// <param name="currentPermissionvalue">if set to <c>true</c> [current permissionvalue].</param>
        /// <param name="currentMasterPermissionFlagvalue">if set to <c>true</c> [current master permission flagvalue].</param>
        ////private void LoadEventFeecalculation(int currentnewmiscCodeidvalue, bool currentPermissionvalue, bool currentMasterPermissionFlagvalue)
        private void LoadEventFeecalculation(bool currentPermissionvalue, bool currentMasterPermissionFlagvalue)
        {
            this.flagLoadOnProcess = true;
            this.eventFeeData = this.form81002Control.WorkItem.F81002_GetEventFee(this.eventId, this.masterFormNo);
            this.getFeeEventDataTable = this.eventFeeData.GetFeeEvent;
            this.eventFeeDatatableRowCount = this.eventFeeData.GetFeeEvent.Rows.Count;

            this.gdocEventHeaderData = this.form81002Control.WorkItem.GetGDocEventHeader(this.eventId);

            #region EF Fileds Decimal Places

            int effields1DecimalPlaces;
            int effields2DecimalPlaces;
            int effields3DecimalPlaces;
            int effields4DecimalPlaces;
            int effields5DecimalPlaces;
            int effields6DecimalPlaces;
            int effields7DecimalPlaces;
            int effields8DecimalPlaces;
            int effields9DecimalPlaces;
            int effields10DecimalPlaces;
            int effields11DecimalPlaces;
            int effields12DecimalPlaces;
            int effields13DecimalPlaces;
            int effields14DecimalPlaces;
            int effields15DecimalPlaces;
            int effields16DecimalPlaces;
            int effields17DecimalPlaces;
            int effields18DecimalPlaces;
            int effields19DecimalPlaces;
            int effields20DecimalPlaces;

            #endregion EF Fileds Decimal Places

            if (this.eventFeeDatatableRowCount > 0)
            {
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.FeeIDColumn].ToString(), out this.feeID);
                this.DescriptionTextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.DescriptionColumn].ToString();
                this.eventFeeFieldsFormula1 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F01Column].ToString();
                this.eventFeeFieldsFormula2 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F02Column].ToString();
                this.eventFeeFieldsFormula3 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F03Column].ToString();
                this.eventFeeFieldsFormula4 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F04Column].ToString();
                this.eventFeeFieldsFormula5 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F05Column].ToString();
                this.eventFeeFieldsFormula6 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F06Column].ToString();
                this.eventFeeFieldsFormula7 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F07Column].ToString();
                this.eventFeeFieldsFormula8 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F08Column].ToString();
                this.eventFeeFieldsFormula9 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F09Column].ToString();
                this.eventFeeFieldsFormula10 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F10Column].ToString();
                this.eventFeeFieldsFormula11 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F11Column].ToString();
                this.eventFeeFieldsFormula12 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F12Column].ToString();
                this.eventFeeFieldsFormula13 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F13Column].ToString();
                this.eventFeeFieldsFormula14 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F14Column].ToString();
                this.eventFeeFieldsFormula15 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F15Column].ToString();
                this.eventFeeFieldsFormula16 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F16Column].ToString();
                this.eventFeeFieldsFormula17 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F17Column].ToString();
                this.eventFeeFieldsFormula18 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F18Column].ToString();
                this.eventFeeFieldsFormula19 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F19Column].ToString();
                this.eventFeeFieldsFormula20 = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.F20Column].ToString();
                this.vformula = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.VFormulaColumn].ToString();

                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D01Column].ToString(), out effields1DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D02Column].ToString(), out effields2DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D03Column].ToString(), out effields3DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D04Column].ToString(), out effields4DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D05Column].ToString(), out effields5DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D06Column].ToString(), out effields6DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D07Column].ToString(), out effields7DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D08Column].ToString(), out effields8DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D09Column].ToString(), out effields9DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D10Column].ToString(), out effields10DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D11Column].ToString(), out effields11DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D12Column].ToString(), out effields12DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D13Column].ToString(), out effields13DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D14Column].ToString(), out effields14DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D15Column].ToString(), out effields15DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D16Column].ToString(), out effields16DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D17Column].ToString(), out effields17DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D18Column].ToString(), out effields18DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D19Column].ToString(), out effields19DecimalPlaces);
                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.D20Column].ToString(), out effields20DecimalPlaces);

                int.TryParse(this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.FeeCatIDColumn].ToString(), out this.feeCataLogId);

                this.EFLabel1Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L01Column].ToString();
                this.EFLabel2Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L02Column].ToString();
                this.EFLabel3Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L03Column].ToString();
                this.EFLabel4Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L04Column].ToString();
                this.EFLabel5Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L05Column].ToString();
                this.EFLabel6Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L06Column].ToString();
                this.EFLabel7Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L07Column].ToString();
                this.EFLabel8Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L08Column].ToString();
                this.EFLabel9Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L09Column].ToString();
                this.EFLabel10Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L10Column].ToString();
                this.EFLabel11Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L11Column].ToString();
                this.EFLabel12Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L12Column].ToString();
                this.EFLabel13Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L13Column].ToString();
                this.EFLabel14Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L14Column].ToString();
                this.EFLabel15Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L15Column].ToString();
                this.EFLabel16Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L16Column].ToString();
                this.EFLabel17Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L17Column].ToString();
                this.EFLabel18Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L18Column].ToString();
                this.EFLabel19Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L19Column].ToString();
                this.EFLabel20Label.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.L20Column].ToString();

                this.ToSetDecimalPlaces(this.EFLabel1TextBox, effields1DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel2TextBox, effields2DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel3TextBox, effields3DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel4TextBox, effields4DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel5TextBox, effields5DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel6TextBox, effields6DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel7TextBox, effields7DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel8TextBox, effields8DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel9TextBox, effields9DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel10TextBox, effields10DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel11TextBox, effields11DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel12TextBox, effields12DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel13TextBox, effields13DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel14TextBox, effields14DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel15TextBox, effields15DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel16TextBox, effields16DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel17TextBox, effields17DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel18TextBox, effields18DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel19TextBox, effields19DecimalPlaces);
                this.ToSetDecimalPlaces(this.EFLabel20TextBox, effields20DecimalPlaces);

                this.EFLabel1TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V01Column].ToString();
                this.EFLabel2TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V02Column].ToString();
                this.EFLabel3TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V03Column].ToString();
                this.EFLabel4TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V04Column].ToString();
                this.EFLabel5TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V05Column].ToString();
                this.EFLabel6TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V06Column].ToString();
                this.EFLabel7TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V07Column].ToString();
                this.EFLabel8TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V08Column].ToString();
                this.EFLabel9TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V09Column].ToString();
                this.EFLabel10TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V10Column].ToString();
                this.EFLabel11TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V11Column].ToString();
                this.EFLabel12TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V12Column].ToString();
                this.EFLabel13TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V13Column].ToString();
                this.EFLabel14TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V14Column].ToString();
                this.EFLabel15TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V15Column].ToString();
                this.EFLabel16TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V16Column].ToString();
                this.EFLabel17TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V17Column].ToString();
                this.EFLabel18TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V18Column].ToString();
                this.EFLabel19TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V19Column].ToString();
                this.EFLabel20TextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V20Column].ToString();
                this.TotalFeeTextBox.Text = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.FeeCalcColumn].ToString();

                this.CheckLabelValues();

                this.ValidateEFfieldsTextBoxs(this.EFLabel1TextBox, this.eventFeeFieldsFormula1, this.EFLabel1Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel2TextBox, this.eventFeeFieldsFormula2, this.EFLabel2Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel3TextBox, this.eventFeeFieldsFormula3, this.EFLabel3Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel4TextBox, this.eventFeeFieldsFormula4, this.EFLabel4Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel5TextBox, this.eventFeeFieldsFormula5, this.EFLabel5Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel6TextBox, this.eventFeeFieldsFormula6, this.EFLabel6Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel7TextBox, this.eventFeeFieldsFormula7, this.EFLabel7Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel8TextBox, this.eventFeeFieldsFormula8, this.EFLabel8Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel9TextBox, this.eventFeeFieldsFormula9, this.EFLabel9Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel10TextBox, this.eventFeeFieldsFormula10, this.EFLabel10Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel11TextBox, this.eventFeeFieldsFormula11, this.EFLabel11Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel12TextBox, this.eventFeeFieldsFormula12, this.EFLabel12Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel13TextBox, this.eventFeeFieldsFormula13, this.EFLabel13Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel14TextBox, this.eventFeeFieldsFormula14, this.EFLabel14Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel15TextBox, this.eventFeeFieldsFormula15, this.EFLabel15Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel16TextBox, this.eventFeeFieldsFormula16, this.EFLabel16Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel17TextBox, this.eventFeeFieldsFormula17, this.EFLabel17Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel18TextBox, this.eventFeeFieldsFormula18, this.EFLabel18Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel19TextBox, this.eventFeeFieldsFormula19, this.EFLabel19Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
                this.ValidateEFfieldsTextBoxs(this.EFLabel20TextBox, this.eventFeeFieldsFormula20, this.EFLabel20Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);

                this.LoadEFCalucaltionGrid();
            }
            else
            {
                if (this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0)
                {
                    this.CodeCatalogPanel.Enabled = true;
                    this.CodeCatalogButton.Enabled = true;
                }
                else
                {
                    this.CodeCatalogPanel.Enabled = false;
                }
                this.TotalFeePanel.Enabled = false;
                this.DescriptionPanel.Enabled = false;
                this.EFLabel1Panel.Enabled = false;
                this.EFLabel2Panel.Enabled = false;
                this.EFLabel3Panel.Enabled = false;
                this.EFLabel4Panel.Enabled = false;
                this.EFLabel5Panel.Enabled = false;
                this.EFLabel6Panel.Enabled = false;
                this.EFLabel7Panel.Enabled = false;
                this.EFLabel8Panel.Enabled = false;
                this.EFLabel9Panel.Enabled = false;
                this.EFLabel10Panel.Enabled = false;
                this.EFLabel11Panel.Enabled = false;
                this.EFLabel12Panel.Enabled = false;
                this.EFLabel13Panel.Enabled = false;
                this.EFLabel14Panel.Enabled = false;
                this.EFLabel15Panel.Enabled = false;
                this.EFLabel16Panel.Enabled = false;
                this.EFLabel17Panel.Enabled = false;
                this.EFLabel18Panel.Enabled = false;
                this.EFLabel19Panel.Enabled = false;
                this.EFLabel20Panel.Enabled = false;
                this.ClearControl();
            }
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Toes the set decimal places.
        /// </summary>
        /// <param name="textBoxControls">The text box controls.</param>
        /// <param name="decimalPlacesValue">The decimal places value.</param>
        private void ToSetDecimalPlaces(Control textBoxControls, int decimalPlacesValue)
        {
            if (textBoxControls.GetType() == typeof(TerraScanTextBox))
            {
                TerraScanTextBox currentTextBox = (TerraScanTextBox)textBoxControls;

                switch (decimalPlacesValue)
                {
                    case 1:
                        currentTextBox.TextCustomFormat = "#,##0.0";
                        currentTextBox.Precision = 1;
                        break;
                    case 2:
                        currentTextBox.TextCustomFormat = "#,##0.00";
                        currentTextBox.Precision = 2;
                        break;
                    case 3:
                        currentTextBox.TextCustomFormat = "#,##0.000";
                        currentTextBox.Precision = 3;
                        break;
                    case 4:
                        currentTextBox.TextCustomFormat = "#,##0.0000";
                        currentTextBox.Precision = 4;
                        break;
                    default:
                        currentTextBox.TextCustomFormat = "#,##";
                        currentTextBox.Precision = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// Validate Eevent Fee TextBoxs.
        /// </summary>
        /// <param name="eventFieldstextBoxControls">eventFieldstextBoxControls</param>
        /// <param name="eventFeeFieldsFormula">eventFeeFieldsFormula</param>
        /// <param name="eventFeeLabelText">eventFeeLabelText</param>
        /// <param name="userPermission">userPermission</param>
        /// <param name="masterFormPermission">masterFormPermission</param>
        private void ValidateEFfieldsTextBoxs(Control eventFieldstextBoxControls, string eventFeeFieldsFormula, string eventFeeLabelText, bool userPermission, bool masterFormPermission)
        {
            if (eventFieldstextBoxControls.GetType() == typeof(TerraScanTextBox))
            {
                TerraScanTextBox currentTextBox = (TerraScanTextBox)eventFieldstextBoxControls;

                ////here based on the permission and formula value we have to edit the textbox controls
                if (userPermission && masterFormPermission)
                {
                    if (!string.IsNullOrEmpty(eventFeeFieldsFormula))
                    {
                        currentTextBox.LockKeyPress = true;
                        currentTextBox.ForeColor = System.Drawing.Color.DarkGray;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(eventFeeLabelText))
                        {
                            currentTextBox.LockKeyPress = true;
                        }
                        else
                        {
                            currentTextBox.LockKeyPress = false;
                        }

                        currentTextBox.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(eventFeeFieldsFormula))
                    {
                        currentTextBox.ForeColor = System.Drawing.Color.DarkGray;
                    }
                    else
                    {
                        currentTextBox.ForeColor = System.Drawing.Color.Black;
                    }

                    currentTextBox.LockKeyPress = true;
                }
            }
        }

        /// <summary>
        /// Used to Load the Event calucaltion Grid
        /// </summary>
        private void LoadEFCalucaltionGrid()
        {
            DataSet tempdataset = new DataSet();
            DataTable eventFeeDataTable = new DataTable("ListMiscImprovements");
            eventFeeDataTable.Columns.Add(new DataColumn("V01", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V02", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V03", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V04", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V05", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V06", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V07", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V08", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V09", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V10", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V11", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V12", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V13", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V14", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V15", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V16", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V17", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V18", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V19", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("V20", typeof(decimal)));
            eventFeeDataTable.Columns.Add(new DataColumn("VFormula", typeof(decimal)));

            DataRow dr = eventFeeDataTable.NewRow();

            eventFeeDataTable.Rows.Add(dr);

            tempdataset.Tables.Add(eventFeeDataTable);

            this.EventTotalFeeCalucationGrid.DataSource = tempdataset;

            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V01"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V01Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V02"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V02Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V03"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V03Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V04"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V04Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V05"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V05Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V06"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V06Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V07"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V07Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V08"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V08Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V09"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V09Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V10"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V10Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V11"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V11Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V12"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V12Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V13"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V13Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V14"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V14Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V15"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V15Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V16"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V16Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V17"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V17Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V18"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V18Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V19"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V19Column];
            this.EventTotalFeeCalucationGrid.Rows[0].Cells["V20"].Value = this.getFeeEventDataTable.Rows[0][this.getFeeEventDataTable.V20Column];
        }

        /// <summary>
        /// Assigns the calculated value.
        /// </summary>
        private void AssignCalculatedValue()
        {
            //// Assigning the First Ten (1 - 10) TextBox Calculated Values.
            this.AssignFirstTenCalculatedValues();

            //// Assigning the Remaining Ten(11 - 20) TextBox Calculated Values.
            this.AssignRemainingCalculatedValue();

            if (!string.IsNullOrEmpty(this.vformula))
            {
                if (this.ToCheckReqFieldeinFormula(this.vformula))
                {
                    this.TotalFeeTextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["VFormula"].Value.ToString();
                }
                else
                {
                    this.TotalFeeTextBox.Text = string.Empty;
                }
            }

            this.unsavedChangeExists = false;
        }

        /// <summary>
        /// Assighning the Remaining 11 to 20 TextBox Values.
        /// </summary>
        private void AssignRemainingCalculatedValue()
        {
            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula11))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula11))
                {
                    this.EFLabel11TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V11"].Value.ToString();
                }
                else
                {
                    this.EFLabel11TextBox.Text = string.Empty;
                }

                this.EFLabel11TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel11TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V11"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula12))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula12))
                {
                    this.EFLabel12TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V12"].Value.ToString();
                }
                else
                {
                    this.EFLabel12TextBox.Text = string.Empty;
                }

                this.EFLabel12TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel12TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V12"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula13))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula13))
                {
                    this.EFLabel13TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V13"].Value.ToString();
                }
                else
                {
                    this.EFLabel13TextBox.Text = string.Empty;
                }

                this.EFLabel13TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel13TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V13"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula14))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula14))
                {
                    this.EFLabel14TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V14"].Value.ToString();
                }
                else
                {
                    this.EFLabel14TextBox.Text = string.Empty;
                }

                this.EFLabel14TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel14TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V14"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula15))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula15))
                {
                    this.EFLabel15TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V15"].Value.ToString();
                }
                else
                {
                    this.EFLabel15TextBox.Text = string.Empty;
                }

                this.EFLabel15TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel15TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V15"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula16))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula16))
                {
                    this.EFLabel16TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V16"].Value.ToString();
                }
                else
                {
                    this.EFLabel16TextBox.Text = string.Empty;
                }

                this.EFLabel16TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel16TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V16"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula17))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula17))
                {
                    this.EFLabel17TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V17"].Value.ToString();
                }
                else
                {
                    this.EFLabel17TextBox.Text = string.Empty;
                }

                this.EFLabel17TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel17TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V17"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula18))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula18))
                {
                    this.EFLabel18TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V18"].Value.ToString();
                }
                else
                {
                    this.EFLabel18TextBox.Text = string.Empty;
                }

                this.EFLabel18TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel18TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V18"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula19))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula19))
                {
                    this.EFLabel19TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V19"].Value.ToString();
                }
                else
                {
                    this.EFLabel19TextBox.Text = string.Empty;
                }

                this.EFLabel19TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel19TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V19"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula20))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula20))
                {
                    this.EFLabel20TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V20"].Value.ToString();
                }
                else
                {
                    this.EFLabel20TextBox.Text = string.Empty;
                }

                this.EFLabel20TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel20TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V20"].Value.ToString();
            }
        }

        /// <summary>
        /// Assigning the First 1 to 10 TextBox Values.
        /// </summary>
        private void AssignFirstTenCalculatedValues()
        {
            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula1))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula1))
                {
                    this.EFLabel1TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V01"].Value.ToString();
                }
                else
                {
                    this.EFLabel1TextBox.Text = string.Empty;
                }

                this.EFLabel1TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel1TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V01"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula2))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula2))
                {
                    this.EFLabel2TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V02"].Value.ToString();
                }
                else
                {
                    this.EFLabel2TextBox.Text = string.Empty;
                }

                this.EFLabel2TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel2TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V02"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula3))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula3))
                {
                    this.EFLabel3TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V03"].Value.ToString();
                }
                else
                {
                    this.EFLabel3TextBox.Text = string.Empty;
                }

                this.EFLabel3TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel3TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V03"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula4))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula4))
                {
                    this.EFLabel4TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V04"].Value.ToString();
                }
                else
                {
                    this.EFLabel4TextBox.Text = string.Empty;
                }

                this.EFLabel4TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel4TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V04"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula5))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula5))
                {
                    this.EFLabel5TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V05"].Value.ToString();
                }
                else
                {
                    this.EFLabel5TextBox.Text = string.Empty;
                }

                this.EFLabel5TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel5TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V05"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula6))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula6))
                {
                    this.EFLabel6TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V06"].Value.ToString();
                }
                else
                {
                    this.EFLabel6TextBox.Text = string.Empty;
                }

                this.EFLabel6TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel6TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V06"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula7))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula7))
                {
                    this.EFLabel7TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V07"].Value.ToString();
                }
                else
                {
                    this.EFLabel7TextBox.Text = string.Empty;
                }

                this.EFLabel7TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel7TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V07"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula8))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula8))
                {
                    this.EFLabel8TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V08"].Value.ToString();
                }
                else
                {
                    this.EFLabel8TextBox.Text = string.Empty;
                }

                this.EFLabel8TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel8TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V08"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula9))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula9))
                {
                    this.EFLabel9TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V09"].Value.ToString();
                }
                else
                {
                    this.EFLabel9TextBox.Text = string.Empty;
                }

                this.EFLabel9TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel9TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V09"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.eventFeeFieldsFormula10))
            {
                if (this.ToCheckReqFieldeinFormula(this.eventFeeFieldsFormula10))
                {
                    this.EFLabel10TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V10"].Value.ToString();
                }
                else
                {
                    this.EFLabel10TextBox.Text = string.Empty;
                }

                this.EFLabel10TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.EFLabel10TextBox.Text = this.EventTotalFeeCalucationGrid.Rows[0].Cells["V10"].Value.ToString();
            }
        }

        /// <summary>
        /// To check req fields are as in formula.
        /// </summary>
        /// <param name="currentFormula">The current formula.</param>
        /// <returns>returns a boolean value</returns>
        private bool ToCheckReqFieldeinFormula(string currentFormula)
        {
            bool validValue = false;
            int validText = 1;

            if (((currentFormula.IndexOf("[V01]") >= 0) || (currentFormula.IndexOf("[V02]") >= 0) || (currentFormula.IndexOf("[V03]") >= 0) || (currentFormula.IndexOf("[V04]") >= 0) || (currentFormula.IndexOf("[V05]") >= 0) || (currentFormula.IndexOf("[V06]") >= 0) || (currentFormula.IndexOf("[V07]") >= 0) || (currentFormula.IndexOf("[V08]") >= 0) || (currentFormula.IndexOf("[V09]") >= 0) || (currentFormula.IndexOf("[V10]") >= 0)))
            {
                if (currentFormula.IndexOf("[V01]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel1TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V02]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V03]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel3TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V04]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel4TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V05]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel5TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V06]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel6TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                        else
                        {
                            validText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V07]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel7TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                        else
                        {
                            validText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V08]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel8TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                        else
                        {
                            validText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V09]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel9TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                        else
                        {
                            validText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V10]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel10TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                        else
                        {
                            validText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V11]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V12]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V13]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V14]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V15]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V16]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V17]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V18]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V19]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                if (currentFormula.IndexOf("[V20]") >= 0)
                {
                    if (!string.IsNullOrEmpty(this.EFLabel2TextBox.Text.Trim()))
                    {
                        if (validText > 0)
                        {
                            validValue = true;
                        }
                    }
                    else
                    {
                        validText = -1;
                        return false;
                    }
                }

                return validValue;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// To set the from to grid
        /// </summary>
        private void SetFormula()
        {
            this.unsavedChangeExists = true;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V01"].Formula = this.eventFeeFieldsFormula1;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V02"].Formula = this.eventFeeFieldsFormula2;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V03"].Formula = this.eventFeeFieldsFormula3;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V04"].Formula = this.eventFeeFieldsFormula4;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V05"].Formula = this.eventFeeFieldsFormula5;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V06"].Formula = this.eventFeeFieldsFormula6;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V07"].Formula = this.eventFeeFieldsFormula7;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V08"].Formula = this.eventFeeFieldsFormula8;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V09"].Formula = this.eventFeeFieldsFormula9;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V10"].Formula = this.eventFeeFieldsFormula10;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V11"].Formula = this.eventFeeFieldsFormula11;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V12"].Formula = this.eventFeeFieldsFormula12;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V13"].Formula = this.eventFeeFieldsFormula13;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V14"].Formula = this.eventFeeFieldsFormula14;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V15"].Formula = this.eventFeeFieldsFormula15;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V16"].Formula = this.eventFeeFieldsFormula16;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V17"].Formula = this.eventFeeFieldsFormula17;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V18"].Formula = this.eventFeeFieldsFormula18;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V19"].Formula = this.eventFeeFieldsFormula19;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["V20"].Formula = this.eventFeeFieldsFormula20;
            this.EventTotalFeeCalucationGrid.DisplayLayout.Bands[0].Columns["VFormula"].Formula = this.vformula;
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
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

            if (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                this.saveUncomplete = true;
            }
            ////else
            ////{
            ////    //sliceValidationFields.RequiredFieldMissing = true;
            ////    //sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
            ////}

            return sliceValidationFields;
        }

        /// <summary>
        /// Save the Event Fees
        /// </summary>
        private void SaveEventFee()
        {
            F81002EventFeeData.GetFeeEventDataTable currentTable = new F81002EventFeeData.GetFeeEventDataTable();
            F81002EventFeeData.GetFeeEventRow currentRow = currentTable.NewGetFeeEventRow();

            decimal store;
            string eventfee = string.Empty;

            currentRow.Description = DescriptionTextBox.Text.Trim();
            currentRow.FeeCalc = this.TotalFeeTextBox.DecimalTextBoxValue;
            currentRow.EventID = this.eventId;
            currentRow.FeeCatID = this.feeCataLogId;

            if (this.feeID > 0)
            {
                currentRow.FeeID = this.feeID;
            }

            currentRow.PPaymentID = 0;

            if (!string.IsNullOrEmpty(this.EFLabel1Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel1TextBox.Text.Trim(), out store);
                currentRow.V01 = store;
            }
            else
            {
                currentRow.V01 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel2Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel2TextBox.Text.Trim(), out store);
                currentRow.V02 = store;
            }
            else
            {
                currentRow.V02 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel3Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel3TextBox.Text.Trim(), out store);
                currentRow.V03 = store;
            }
            else
            {
                currentRow.V03 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel4Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel4TextBox.Text.Trim(), out store);
                currentRow.V04 = store;
            }
            else
            {
                currentRow.V04 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel5Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel5TextBox.Text.Trim(), out store);
                currentRow.V05 = store;
            }
            else
            {
                currentRow.V05 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel6Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel6TextBox.Text.Trim(), out store);
                currentRow.V06 = store;
            }
            else
            {
                currentRow.V06 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel7Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel7TextBox.Text.Trim(), out store);
                currentRow.V07 = store;
            }
            else
            {
                currentRow.V07 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel8Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel8TextBox.Text.Trim(), out store);
                currentRow.V08 = store;
            }
            else
            {
                currentRow.V08 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel9Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel9TextBox.Text.Trim(), out store);
                currentRow.V09 = store;
            }
            else
            {
                currentRow.V09 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel10Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel10TextBox.Text.Trim(), out store);
                currentRow.V10 = store;
            }
            else
            {
                currentRow.V10 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel11Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel11TextBox.Text.Trim(), out store);
                currentRow.V11 = store;
            }
            else
            {
                currentRow.V11 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel12Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel12TextBox.Text.Trim(), out store);
                currentRow.V12 = store;
            }
            else
            {
                currentRow.V12 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel13Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel13TextBox.Text.Trim(), out store);
                currentRow.V13 = store;
            }
            else
            {
                currentRow.V13 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel14Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel14TextBox.Text.Trim(), out store);
                currentRow.V14 = store;
            }
            else
            {
                currentRow.V14 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel15Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel15TextBox.Text.Trim(), out store);
                currentRow.V15 = store;
            }
            else
            {
                currentRow.V15 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel16Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel16TextBox.Text.Trim(), out store);
                currentRow.V16 = store;
            }
            else
            {
                currentRow.V16 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel17Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel17TextBox.Text.Trim(), out store);
                currentRow.V17 = store;
            }
            else
            {
                currentRow.V17 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel18Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel18TextBox.Text.Trim(), out store);
                currentRow.V18 = store;
            }
            else
            {
                currentRow.V18 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel19Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel19TextBox.Text.Trim(), out store);
                currentRow.V19 = store;
            }
            else
            {
                currentRow.V19 = 0;
            }

            if (!string.IsNullOrEmpty(this.EFLabel20Label.Text.Trim()))
            {
                decimal.TryParse(EFLabel20TextBox.Text.Trim(), out store);
                currentRow.V20 = store;
            }
            else
            {
                currentRow.V20 = 0;
            }

            currentTable.Rows.Add(currentRow);

            eventfee = TerraScanCommon.GetXmlString(currentTable);
            int returnValue = this.form81002Control.WorkItem.F81002_SaveEventFee(currentRow.EventID, eventfee, TerraScanCommon.UserId);
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.eventId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
        }

        /// <summary>
        /// checking empty labels and making the respective textbox empty
        /// </summary>
        private void CheckLabelValues()
        {
            if (string.IsNullOrEmpty(this.EFLabel1Label.Text.Trim()))
            {
                EFLabel1TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel2Label.Text.Trim()))
            {
                EFLabel2TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel3Label.Text.Trim()))
            {
                EFLabel3TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel4Label.Text.Trim()))
            {
                EFLabel4TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel5Label.Text.Trim()))
            {
                EFLabel5TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel6Label.Text.Trim()))
            {
                EFLabel6TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel7Label.Text.Trim()))
            {
                EFLabel7TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel8Label.Text.Trim()))
            {
                EFLabel8TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel9Label.Text.Trim()))
            {
                EFLabel9TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel10Label.Text.Trim()))
            {
                EFLabel10TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel11Label.Text.Trim()))
            {
                EFLabel11TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel12Label.Text.Trim()))
            {
                EFLabel12TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel13Label.Text.Trim()))
            {
                EFLabel13TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel14Label.Text.Trim()))
            {
                EFLabel14TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel15Label.Text.Trim()))
            {
                EFLabel15TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel16Label.Text.Trim()))
            {
                EFLabel16TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel17Label.Text.Trim()))
            {
                EFLabel17TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel18Label.Text.Trim()))
            {
                EFLabel18TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel19Label.Text.Trim()))
            {
                EFLabel19TextBox.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.EFLabel20Label.Text.Trim()))
            {
                EFLabel20TextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Clears the control.
        /// </summary>
        private void ClearControl()
        {
            this.EFLabel1TextBox.Text = string.Empty;
            this.EFLabel2TextBox.Text = string.Empty;
            this.EFLabel3TextBox.Text = string.Empty;
            this.EFLabel4TextBox.Text = string.Empty;
            this.EFLabel5TextBox.Text = string.Empty;
            this.EFLabel6TextBox.Text = string.Empty;
            this.EFLabel7TextBox.Text = string.Empty;
            this.EFLabel8TextBox.Text = string.Empty;
            this.EFLabel9TextBox.Text = string.Empty;
            this.EFLabel10TextBox.Text = string.Empty;
            this.EFLabel11TextBox.Text = string.Empty;
            this.EFLabel12TextBox.Text = string.Empty;
            this.EFLabel13TextBox.Text = string.Empty;
            this.EFLabel14TextBox.Text = string.Empty;
            this.EFLabel15TextBox.Text = string.Empty;
            this.EFLabel16TextBox.Text = string.Empty;
            this.EFLabel17TextBox.Text = string.Empty;
            this.EFLabel18TextBox.Text = string.Empty;
            this.EFLabel19TextBox.Text = string.Empty;
            this.EFLabel20TextBox.Text = string.Empty;
            this.TotalFeeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;

            this.EFLabel1Label.Text = string.Empty;
            this.EFLabel2Label.Text = string.Empty;
            this.EFLabel3Label.Text = string.Empty;
            this.EFLabel4Label.Text = string.Empty;
            this.EFLabel5Label.Text = string.Empty;
            this.EFLabel6Label.Text = string.Empty;
            this.EFLabel7Label.Text = string.Empty;
            this.EFLabel8Label.Text = string.Empty;
            this.EFLabel9Label.Text = string.Empty;
            this.EFLabel10Label.Text = string.Empty;
            this.EFLabel11Label.Text = string.Empty;
            this.EFLabel12Label.Text = string.Empty;
            this.EFLabel13Label.Text = string.Empty;
            this.EFLabel14Label.Text = string.Empty;
            this.EFLabel15Label.Text = string.Empty;
            this.EFLabel16Label.Text = string.Empty;
            this.EFLabel17Label.Text = string.Empty;
            this.EFLabel18Label.Text = string.Empty;
            this.EFLabel19Label.Text = string.Empty;
            this.EFLabel20Label.Text = string.Empty;
        }

        #endregion private methods
    }
}
