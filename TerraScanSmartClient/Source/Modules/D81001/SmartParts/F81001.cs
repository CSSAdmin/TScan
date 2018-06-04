//--------------------------------------------------------------------------------------------
// <copyright file="F81001.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Nov 07	    D.LathaMaheswari   Created
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
    using TerraScan.Common;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Web.Services.Protocols;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using Microsoft.Practices.CompositeUI.SmartParts;

    /// <summary>
    /// F81001
    /// </summary>
    [SmartPart]
    public partial class F81001 : BaseSmartPart
    {
        #region PrivateMembers

        /// <summary>
        /// fontroller for the current view
        /// </summary>
        private F81001Controller form81001control;

        /// <summary>
        /// master form no
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// the selected record
        /// </summary>
        private int feeCatId;

        /// <summary>
        /// permissions from form master
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// variable to identify page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Dataset
        /// </summary>
        private F81001FeeCatalogData form81001FeeCatalogDataSet;

        /// <summary>
        /// Store the current row values
        /// </summary>
        private F81001FeeCatalogData.GetFeeCatalogRow currentRow;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flag to identify form values are being binded
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// the formula value selected
        /// </summary>
        private string loadedFormulaValue;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// minimum date
        /// </summary>
        private DateTime minimunDate = new DateTime(1900, 1, 1);

        /// <summary>
        /// maximum date
        /// </summary>
        private DateTime maximumDate = new DateTime(2079, 6, 6);

        #endregion PrivateMembers

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36010"/> class.
        /// </summary>
        public F81001()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36010"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F81001(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.form81001FeeCatalogDataSet = new F81001FeeCatalogData();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.feeCatId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.loadedFormulaValue = string.Empty;
            this.CatalogFormPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CatalogFormPictureBox.Height, this.CatalogFormPictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        #endregion Event Publication

        #region Field encapsulation

        /// <summary>
        /// Gets or sets the form1030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F81001Controller Form81001control
        {
            get { return this.form81001control; }
            set { this.form81001control = value; }
        }

        #endregion Field encapsulation

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
                this.FeeDefinitionlPanel.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                this.LockControls(false);
                this.FormNumberTextBox.Focus();
            }
            else
            {
                this.FeeDefinitionlPanel.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                this.LockControls(true);
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
                this.LockControls(false);
                this.FeeDefinitionlPanel.Enabled = true;
                if (this.form81001FeeCatalogDataSet.GetFeeCatalog.Rows.Count > 0)
                {
                    this.ParentForm.ActiveControl = this.FormNumberTextBox;
                    this.ActiveControl.Focus();
                }
            }
            else
            {
                this.FeeDefinitionlPanel.Enabled = false;
                this.LockControls(true);
            }

            this.flagFormLoad = true;
            this.ClearControls();
            this.SetControlValues();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagFormLoad = false;
            this.FormNumberTextBox.Focus();
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                this.SaveMiscImprovementCataLog();
                this.FeeDefinitionlPanel.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            else
            {
                this.FeeDefinitionlPanel.Enabled = false;
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

                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.FeeDefinitionlPanel.Enabled = this.slicePermissionField.editPermission && this.formMasterPermissionEdit;

                    if (this.form81001FeeCatalogDataSet.GetFeeCatalog.Rows.Count > 0)
                    {
                        this.FormNumberTextBox.Focus();
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            this.FeeDefinitionlPanel.Enabled = false;
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
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                this.form81001control.WorkItem.F81001_DeleteEventFeeCatalog(this.feeCatId, TerraScanCommon.UserId);
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
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
                    this.flagFormLoad = true;
                    this.FeeDefinitionlPanel.Enabled = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.feeCatId = eventArgs.Data.SelectedKeyId;
                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.FeeDefinitionlPanel.Enabled = this.slicePermissionField.editPermission && this.formMasterPermissionEdit;
                    this.LoadMiscImprovementCatalog();
                    this.FormNumberTextBox.Focus();
                    this.flagFormLoad = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /*  /// <summary>
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
                      this.feeCatId = eventArgs.Data.SelectedKeyId;
                  }
              }
              catch (Exception ex)
              {
                  ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
              }
          }*/

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

        #region FormEvents

        /// <summary>
        /// Handles the Click event of the RowCButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RowCButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.ValueCalculationCButton))
                {
                    this.BindSource(false);
                }
                else
                {
                    this.BindSource(true);
                }

                if (sender.Equals(this.Row1CButton))
                {
                    this.SetFormulaValue(this.Row1FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula1"), this.Row1FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row2CButton))
                {
                    this.SetFormulaValue(this.Row2FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula2"), this.Row2FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row3CButton))
                {
                    this.SetFormulaValue(this.Row3FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula3"), this.Row3FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row4CButton))
                {
                    this.SetFormulaValue(this.Row4FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula4"), this.Row4FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row5CButton))
                {
                    this.SetFormulaValue(this.Row5FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula5"), this.Row5FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row6CButton))
                {
                    this.SetFormulaValue(this.Row6FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula6"), this.Row6FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row7CButton))
                {
                    this.SetFormulaValue(this.Row7FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula7"), this.Row7FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row8CButton))
                {
                    this.SetFormulaValue(this.Row8FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula8"), this.Row8FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row9CButton))
                {
                    this.SetFormulaValue(this.Row9FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula9"), this.Row9FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row10CButton))
                {
                    this.SetFormulaValue(this.Row10FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula10"), this.Row10FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row11CButton))
                {
                    this.SetFormulaValue(this.Row11FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula11"), this.Row11FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row12CButton))
                {
                    this.SetFormulaValue(this.Row12FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula12"), this.Row12FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row13CButton))
                {
                    this.SetFormulaValue(this.Row13FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula13"), this.Row13FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row14CButton))
                {
                    this.SetFormulaValue(this.Row14FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula14"), this.Row14FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row15CButton))
                {
                    this.SetFormulaValue(this.Row15FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula15"), this.Row15FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row16CButton))
                {
                    this.SetFormulaValue(this.Row16FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula16"), this.Row16FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row17CButton))
                {
                    this.SetFormulaValue(this.Row17FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula17"), this.Row17FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row18CButton))
                {
                    this.SetFormulaValue(this.Row18FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula18"), this.Row18FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row19CButton))
                {
                    this.SetFormulaValue(this.Row19FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula19"), this.Row19FormulaTextBox.Text));
                }
                else if (sender.Equals(this.Row20CButton))
                {
                    this.SetFormulaValue(this.Row20FormulaTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("Formula20"), this.Row20FormulaTextBox.Text));
                }
                else if (sender.Equals(this.ValueCalculationCButton))
                {
                    this.SetFormulaValue(this.FeeCalculationTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("TotalFormula"), this.FeeCalculationTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Texts the value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad && (this.PermissionEdit && (this.pageMode == TerraScanCommon.PageModeTypes.View)))
                {
                    ////Make visible the save and cancel button
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F36010 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F81001_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagFormLoad = true;
                this.FeeDefinitionlPanel.Enabled = true;
                //// To Bind the values on controls
                this.LoadMiscImprovementCatalog();

                if (this.form81001FeeCatalogDataSet.GetFeeCatalog.Rows.Count > 0)
                {
                    this.FormNumberTextBox.Focus();
                }
                else
                {
                    this.Focus();
                }

                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagFormLoad = false;
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
        /// Decimals the text box leave.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DecimalTextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    int currentDecimalValue;
                    TerraScanTextBox currentDecimalTextBox = (TerraScanTextBox)sender;
                    if (int.TryParse(currentDecimalTextBox.Text, out currentDecimalValue))
                    {
                        if (currentDecimalValue < 0 || currentDecimalValue > 4)
                        {
                            //// To show the message if the user enter more than 4 
                            MessageBox.Show(SharedFunctions.GetResourceString("DecimalRange"), SharedFunctions.GetResourceString("F81001_InvalidDecimalHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            currentDecimalTextBox.Focus();
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
        /// Handles the Click event of the CatalogFormPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CatalogFormPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the CatalogFormPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CatalogFormPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.CatalogFormSliceToolTip.SetToolTip(this.CatalogFormPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// EffectiveDateCalenderButton Click event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void EffectiveDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.EffectiveMonthCalendar.Visible = true;
                this.EffectiveMonthCalendar.ScrollChange = 1;

                // Display the Calender control near the Calender Picture box.
                this.EffectiveMonthCalendar.Left = this.EffectiveDatePanel.Left + this.EffectiveDateCalenderButton.Left + this.EffectiveDateCalenderButton.Width;
                this.EffectiveMonthCalendar.Top = this.EffectiveDatePanel.Top + this.EffectiveDateCalenderButton.Top;
                this.EffectiveMonthCalendar.Tag = this.EffectiveDateCalenderButton.Tag;
                this.EffectiveMonthCalendar.BringToFront();
                this.EffectiveMonthCalendar.Focus();
                if (!string.IsNullOrEmpty(this.EffectiveDateTextBox.Text))
                {
                    this.EffectiveMonthCalendar.SetDate(Convert.ToDateTime(this.EffectiveDateTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// EffectiveDateCalender DateSelection changed event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void EffectiveMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                ////Set the selected date on TextBox
                this.SetSeletedDate(e.Start.ToString(this.dateFormat));
                this.EffectiveDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the EffectiveMonth Calendar
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The Sender</param>
        private void EffectiveMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.EffectiveMonthCalendar.SelectionStart.ToString(this.dateFormat));
                    this.EffectiveDateTextBox.Focus();
                }

                if (e.KeyCode == Keys.Escape)
                {
                    this.EffectiveMonthCalendar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the EffectiveDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void EffectiveDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Control sourceControl = (sender as TextBox);
                string sourceValue = sourceControl.Text.Trim();

                ////allow empty if check for error is not after status 
                if (String.IsNullOrEmpty(sourceValue))
                {
                    return;
                }

                ////Check the entered Date is valid or date
                if (!this.CheckInputValueValidity(ref sourceValue, TypeCode.DateTime))
                {
                    ////revert the changes
                    MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.minimunDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.maximumDate.ToShortDateString(), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("DateValidation")), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sourceControl.Text = this.form81001FeeCatalogDataSet.GetFeeCatalog.EffectiveDateColumn.ToString();
                    sourceControl.Focus();
                }
                else
                {
                    sourceControl.Text = sourceValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion FormEvents

        #region Private Methods

        /// <summary>
        /// Gets the formula value.
        /// </summary>
        /// <param name="columnName">The columnName.</param>
        /// <param name="valueFromForm">Value From Form</param>
        /// <returns>selectedformulaValue</returns>
        private FormulaAlteredValue GetFormulaValue(string columnName, string valueFromForm)
        {
            FormulaAlteredValue currentFormulaAlteredValue;
            UltraGridColumn createdColumn;
            createdColumn = this.QueryEngineGrid.DisplayLayout.Bands[SharedFunctions.GetResourceString("SelectedBandValue")].Columns[columnName];

            if (null == createdColumn.Layout.Grid.CalcManager)
            {
                createdColumn.Layout.Grid.CalcManager = new UltraCalcManager();
            }

            FormulaBuilderDialog dlg = new FormulaBuilderDialog(createdColumn, true);
            this.loadedFormulaValue = valueFromForm;
            dlg.Load += new EventHandler(this.Dlg_Load);
            DialogResult result = dlg.ShowDialog(this.ParentForm);
            if (result.Equals(DialogResult.OK))
            {
                currentFormulaAlteredValue.FlagAltered = true;
                currentFormulaAlteredValue.AlteredValue = dlg.Formula;
            }
            else
            {
                currentFormulaAlteredValue.FlagAltered = false;
                currentFormulaAlteredValue.AlteredValue = dlg.Formula;
            }

            this.loadedFormulaValue = string.Empty;
            return currentFormulaAlteredValue;
        }

        /// <summary>
        /// To Load the Formula value
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void Dlg_Load(object sender, EventArgs e)
        {
            ((FormulaBuilderDialog)sender).Controls[0].Controls[0].Controls[0].Controls[0].Text = this.loadedFormulaValue;
            this.loadedFormulaValue = string.Empty;
        }

        /// <summary>
        /// Binds the source.
        /// </summary>
        /// <param name="flagrowLevelCalculation">if set to <c>true</c> [flagrow level calculation].</param>
        private void BindSource(bool flagrowLevelCalculation)
        {
            try
            {
                DataSet currentDataSet = new DataSet();
                DataTable currentTable = new DataTable(SharedFunctions.GetResourceString("SelectedBandValue"));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula1"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula2"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula3"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula4"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula5"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula6"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula7"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula8"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula9"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula10"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula11"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula12"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula13"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula14"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula15"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula16"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula17"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula18"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula19"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("Formula20"), typeof(double)));
                if (flagrowLevelCalculation)
                {
                    ////this.QueryEngineGrid.DisplayLayout.Bands["SelectedBand"].Columns["VFormula"].Hidden = false;
                    ////this.QueryEngineGrid.DisplayLayout.Bands["SelectedBand"].Columns["VFormula"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
                }
                else
                {
                    currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("TotalFormula"), typeof(double)));
                }

                currentDataSet.Tables.Add(currentTable);
                this.QueryEngineGrid.DataSource = currentDataSet;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>0
        private void ClearControls()
        {
            this.FormNumberTextBox.Text = string.Empty;
            this.EffectiveDateTextBox.Text = string.Empty;
            this.ActionNumberTextBox.Text = string.Empty;
            this.FeeCalculationTextBox.Text = string.Empty;
            this.DefaultDescriptionTextBox.Text = string.Empty;
            ////Clear Label Name TextBoxes
            this.Row1LabelTextBox.Text = string.Empty;
            this.Row2LabelTextBox.Text = string.Empty;
            this.Row3LabelTextBox.Text = string.Empty;
            this.Row4LabelTextBox.Text = string.Empty;
            this.Row5LabelTextBox.Text = string.Empty;
            this.Row6LabelTextBox.Text = string.Empty;
            this.Row7LabelTextBox.Text = string.Empty;
            this.Row8LabelTextBox.Text = string.Empty;
            this.Row9LabelTextBox.Text = string.Empty;
            this.Row10LabelTextBox.Text = string.Empty;
            this.Row11LabelTextBox.Text = string.Empty;
            this.Row12LabelTextBox.Text = string.Empty;
            this.Row13LabelTextBox.Text = string.Empty;
            this.Row14LabelTextBox.Text = string.Empty;
            this.Row15LabelTextBox.Text = string.Empty;
            this.Row16LabelTextBox.Text = string.Empty;
            this.Row17LabelTextBox.Text = string.Empty;
            this.Row18LabelTextBox.Text = string.Empty;
            this.Row19LabelTextBox.Text = string.Empty;
            this.Row20LabelTextBox.Text = string.Empty;
            ////Clear Decimal place TextBoxes
            this.Row1DecimalTextBox.Text = string.Empty;
            this.Row2DecimalTextBox.Text = string.Empty;
            this.Row3DecimalTextBox.Text = string.Empty;
            this.Row4DecimalTextBox.Text = string.Empty;
            this.Row5DecimalTextBox.Text = string.Empty;
            this.Row6DecimalTextBox.Text = string.Empty;
            this.Row7DecimalTextBox.Text = string.Empty;
            this.Row8DecimalTextBox.Text = string.Empty;
            this.Row9DecimalTextBox.Text = string.Empty;
            this.Row10DecimalTextBox.Text = string.Empty;
            this.Row11DecimalTextBox.Text = string.Empty;
            this.Row12DecimalTextBox.Text = string.Empty;
            this.Row13DecimalTextBox.Text = string.Empty;
            this.Row14DecimalTextBox.Text = string.Empty;
            this.Row15DecimalTextBox.Text = string.Empty;
            this.Row16DecimalTextBox.Text = string.Empty;
            this.Row17DecimalTextBox.Text = string.Empty;
            this.Row18DecimalTextBox.Text = string.Empty;
            this.Row19DecimalTextBox.Text = string.Empty;
            this.Row20DecimalTextBox.Text = string.Empty;
            ////Clear Formula TextBoxes
            this.Row1FormulaTextBox.Text = string.Empty;
            this.Row2FormulaTextBox.Text = string.Empty;
            this.Row3FormulaTextBox.Text = string.Empty;
            this.Row4FormulaTextBox.Text = string.Empty;
            this.Row5FormulaTextBox.Text = string.Empty;
            this.Row6FormulaTextBox.Text = string.Empty;
            this.Row7FormulaTextBox.Text = string.Empty;
            this.Row8FormulaTextBox.Text = string.Empty;
            this.Row9FormulaTextBox.Text = string.Empty;
            this.Row10FormulaTextBox.Text = string.Empty;
            this.Row11FormulaTextBox.Text = string.Empty;
            this.Row12FormulaTextBox.Text = string.Empty;
            this.Row13FormulaTextBox.Text = string.Empty;
            this.Row14FormulaTextBox.Text = string.Empty;
            this.Row15FormulaTextBox.Text = string.Empty;
            this.Row16FormulaTextBox.Text = string.Empty;
            this.Row17FormulaTextBox.Text = string.Empty;
            this.Row18FormulaTextBox.Text = string.Empty;
            this.Row19FormulaTextBox.Text = string.Empty;
            this.Row20FormulaTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void LockControls(bool enable)
        {
            this.FormNumberTextBox.LockKeyPress = enable;
            this.EffectiveDateTextBox.LockKeyPress = enable;
            this.ActionNumberTextBox.LockKeyPress = enable;
            this.FeeCalculationTextBox.LockKeyPress = enable;
            this.FeeCalculationTextBox.ReadOnly = true;
            this.DefaultDescriptionTextBox.LockKeyPress = enable;
            ////Enable/Disable Label Name TextBoxes
            this.Row1LabelTextBox.LockKeyPress = enable;
            this.Row2LabelTextBox.LockKeyPress = enable;
            this.Row3LabelTextBox.LockKeyPress = enable;
            this.Row4LabelTextBox.LockKeyPress = enable;
            this.Row5LabelTextBox.LockKeyPress = enable;
            this.Row6LabelTextBox.LockKeyPress = enable;
            this.Row7LabelTextBox.LockKeyPress = enable;
            this.Row8LabelTextBox.LockKeyPress = enable;
            this.Row9LabelTextBox.LockKeyPress = enable;
            this.Row10LabelTextBox.LockKeyPress = enable;
            this.Row11LabelTextBox.LockKeyPress = enable;
            this.Row12LabelTextBox.LockKeyPress = enable;
            this.Row13LabelTextBox.LockKeyPress = enable;
            this.Row14LabelTextBox.LockKeyPress = enable;
            this.Row15LabelTextBox.LockKeyPress = enable;
            this.Row16LabelTextBox.LockKeyPress = enable;
            this.Row17LabelTextBox.LockKeyPress = enable;
            this.Row18LabelTextBox.LockKeyPress = enable;
            this.Row19LabelTextBox.LockKeyPress = enable;
            this.Row20LabelTextBox.LockKeyPress = enable;
            ////Enable/Disable Decimal Place TextBoxes
            this.Row1DecimalTextBox.LockKeyPress = enable;
            this.Row2DecimalTextBox.LockKeyPress = enable;
            this.Row3DecimalTextBox.LockKeyPress = enable;
            this.Row4DecimalTextBox.LockKeyPress = enable;
            this.Row5DecimalTextBox.LockKeyPress = enable;
            this.Row6DecimalTextBox.LockKeyPress = enable;
            this.Row7DecimalTextBox.LockKeyPress = enable;
            this.Row8DecimalTextBox.LockKeyPress = enable;
            this.Row9DecimalTextBox.LockKeyPress = enable;
            this.Row10DecimalTextBox.LockKeyPress = enable;
            this.Row11DecimalTextBox.LockKeyPress = enable;
            this.Row12DecimalTextBox.LockKeyPress = enable;
            this.Row13DecimalTextBox.LockKeyPress = enable;
            this.Row14DecimalTextBox.LockKeyPress = enable;
            this.Row15DecimalTextBox.LockKeyPress = enable;
            this.Row16DecimalTextBox.LockKeyPress = enable;
            this.Row17DecimalTextBox.LockKeyPress = enable;
            this.Row18DecimalTextBox.LockKeyPress = enable;
            this.Row19DecimalTextBox.LockKeyPress = enable;
            this.Row20DecimalTextBox.LockKeyPress = enable;
            ////Enable/Disable Formula TextBoxes
            this.Row1CButton.Enabled = !enable;
            this.Row2CButton.Enabled = !enable;
            this.Row3CButton.Enabled = !enable;
            this.Row4CButton.Enabled = !enable;
            this.Row5CButton.Enabled = !enable;
            this.Row6CButton.Enabled = !enable;
            this.Row7CButton.Enabled = !enable;
            this.Row8CButton.Enabled = !enable;
            this.Row9CButton.Enabled = !enable;
            this.Row10CButton.Enabled = !enable;
            this.Row11CButton.Enabled = !enable;
            this.Row12CButton.Enabled = !enable;
            this.Row13CButton.Enabled = !enable;
            this.Row14CButton.Enabled = !enable;
            this.Row15CButton.Enabled = !enable;
            this.Row16CButton.Enabled = !enable;
            this.Row17CButton.Enabled = !enable;
            this.Row18CButton.Enabled = !enable;
            this.Row19CButton.Enabled = !enable;
            this.Row20CButton.Enabled = !enable;
        }

        /// <summary>
        /// Loads the misc improvement catalog.
        /// </summary>
        private void LoadMiscImprovementCatalog()
        {
            this.form81001FeeCatalogDataSet.GetFeeCatalog.Clear();
            ////Retrieve Data based on FeeCatID
            this.form81001FeeCatalogDataSet = this.form81001control.WorkItem.F81001_GetEventFeeCatalog(this.feeCatId);
            ////Set the values on appropriate controls
            this.SetControlValues();
        }

        /// <summary>
        /// Sets the control values.
        /// </summary>
        private void SetControlValues()
        {
            if (this.form81001FeeCatalogDataSet.GetFeeCatalog.Rows.Count > 0)
            {
                this.currentRow = (F81001FeeCatalogData.GetFeeCatalogRow)this.form81001FeeCatalogDataSet.GetFeeCatalog.Rows[0];

                this.FeeCalculationTextBox.Text = this.currentRow.VFormula;
                this.DefaultDescriptionTextBox.Text = this.currentRow.Description;
                this.FormNumberTextBox.Text = Convert.ToString(this.currentRow.Form);
                this.SetLabelFieldValues();
                this.SetDecimalFieldValues();
                this.SetFormulaFieldValues();

                if (this.currentRow.IsActionNumberNull())
                {
                    this.ActionNumberTextBox.Text = string.Empty;
                }
                else
                {
                    this.ActionNumberTextBox.Text = this.currentRow.ActionNumber.ToString();
                }

                if (this.currentRow.IsEffectiveDateNull())
                {
                    this.EffectiveDateTextBox.Text = string.Empty;
                }
                else
                {
                    this.EffectiveDateTextBox.Text = this.currentRow.EffectiveDate.ToString();
                }
            }
            else
            {
                // clearing all controls.
                this.ClearControls();
                //  Ends here
                this.FeeDefinitionlPanel.Enabled = false;
            }
        }

        /// <summary>
        /// Set the values in Formula Field
        /// </summary>
        private void SetFormulaFieldValues()
        {
            if (this.currentRow.IsF01Null())
            {
                this.Row1FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row1FormulaTextBox.Text = this.currentRow.F01;
            }

            if (this.currentRow.IsF02Null())
            {
                this.Row2FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row2FormulaTextBox.Text = this.currentRow.F02;
            }

            if (this.currentRow.IsF03Null())
            {
                this.Row3FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row3FormulaTextBox.Text = this.currentRow.F03;
            }

            if (this.currentRow.IsF04Null())
            {
                this.Row4FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row4FormulaTextBox.Text = this.currentRow.F04;
            }

            if (this.currentRow.IsF05Null())
            {
                this.Row5FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row5FormulaTextBox.Text = this.currentRow.F05;
            }

            if (this.currentRow.IsF06Null())
            {
                this.Row6FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row6FormulaTextBox.Text = this.currentRow.F06;
            }

            if (this.currentRow.IsF07Null())
            {
                this.Row7FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row7FormulaTextBox.Text = this.currentRow.F07;
            }

            if (this.currentRow.IsF08Null())
            {
                this.Row8FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row8FormulaTextBox.Text = this.currentRow.F08;
            }

            if (this.currentRow.IsF09Null())
            {
                this.Row9FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row9FormulaTextBox.Text = this.currentRow.F09;
            }

            if (this.currentRow.IsF10Null())
            {
                this.Row10FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row10FormulaTextBox.Text = this.currentRow.F10;
            }

            if (this.currentRow.IsF11Null())
            {
                this.Row11FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row11FormulaTextBox.Text = this.currentRow.F11;
            }

            if (this.currentRow.IsF12Null())
            {
                this.Row12FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row12FormulaTextBox.Text = this.currentRow.F12;
            }

            if (this.currentRow.IsF13Null())
            {
                this.Row13FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row13FormulaTextBox.Text = this.currentRow.F13;
            }

            if (this.currentRow.IsF14Null())
            {
                this.Row14FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row14FormulaTextBox.Text = this.currentRow.F14;
            }

            if (this.currentRow.IsF15Null())
            {
                this.Row15FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row15FormulaTextBox.Text = this.currentRow.F15;
            }

            if (this.currentRow.IsF16Null())
            {
                this.Row16FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row16FormulaTextBox.Text = this.currentRow.F16;
            }

            if (this.currentRow.IsF17Null())
            {
                this.Row17FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row17FormulaTextBox.Text = this.currentRow.F17;
            }

            if (this.currentRow.IsF18Null())
            {
                this.Row18FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row18FormulaTextBox.Text = this.currentRow.F18;
            }

            if (this.currentRow.IsF19Null())
            {
                this.Row19FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row19FormulaTextBox.Text = this.currentRow.F19;
            }

            if (this.currentRow.IsF20Null())
            {
                this.Row20FormulaTextBox.Text = string.Empty;
            }
            else
            {
                this.Row20FormulaTextBox.Text = this.currentRow.F20;
            }
        }

        /// <summary>
        /// Set the values in Decimal Field
        /// </summary>
        private void SetDecimalFieldValues()
        {
            if (this.currentRow.IsD01Null())
            {
                this.Row1DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row1DecimalTextBox.Text = this.currentRow.D01.ToString();
            }

            if (this.currentRow.IsD02Null())
            {
                this.Row2DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row2DecimalTextBox.Text = this.currentRow.D02.ToString();
            }

            if (this.currentRow.IsD03Null())
            {
                this.Row3DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row3DecimalTextBox.Text = this.currentRow.D03.ToString();
            }

            if (this.currentRow.IsD04Null())
            {
                this.Row4DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row4DecimalTextBox.Text = this.currentRow.D04.ToString();
            }

            if (this.currentRow.IsD05Null())
            {
                this.Row5DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row5DecimalTextBox.Text = this.currentRow.D05.ToString();
            }

            if (this.currentRow.IsD06Null())
            {
                this.Row6DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row6DecimalTextBox.Text = this.currentRow.D06.ToString();
            }

            if (this.currentRow.IsD07Null())
            {
                this.Row7DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row7DecimalTextBox.Text = this.currentRow.D07.ToString();
            }

            if (this.currentRow.IsD08Null())
            {
                this.Row8DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row8DecimalTextBox.Text = this.currentRow.D08.ToString();
            }

            if (this.currentRow.IsD09Null())
            {
                this.Row9DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row9DecimalTextBox.Text = this.currentRow.D09.ToString();
            }

            if (this.currentRow.IsD10Null())
            {
                this.Row10DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row10DecimalTextBox.Text = this.currentRow.D10.ToString();
            }

            if (this.currentRow.IsD11Null())
            {
                this.Row11DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row11DecimalTextBox.Text = this.currentRow.D11.ToString();
            }

            if (this.currentRow.IsD12Null())
            {
                this.Row12DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row12DecimalTextBox.Text = this.currentRow.D12.ToString();
            }

            if (this.currentRow.IsD13Null())
            {
                this.Row13DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row13DecimalTextBox.Text = this.currentRow.D13.ToString();
            }

            if (this.currentRow.IsD14Null())
            {
                this.Row14DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row14DecimalTextBox.Text = this.currentRow.D14.ToString();
            }

            if (this.currentRow.IsD15Null())
            {
                this.Row15DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row15DecimalTextBox.Text = this.currentRow.D15.ToString();
            }

            if (this.currentRow.IsD16Null())
            {
                this.Row16DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row16DecimalTextBox.Text = this.currentRow.D16.ToString();
            }

            if (this.currentRow.IsD17Null())
            {
                this.Row17DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row17DecimalTextBox.Text = this.currentRow.D17.ToString();
            }

            if (this.currentRow.IsD18Null())
            {
                this.Row18DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row18DecimalTextBox.Text = this.currentRow.D18.ToString();
            }

            if (this.currentRow.IsD19Null())
            {
                this.Row19DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row19DecimalTextBox.Text = this.currentRow.D19.ToString();
            }

            if (this.currentRow.IsD20Null())
            {
                this.Row20DecimalTextBox.Text = string.Empty;
            }
            else
            {
                this.Row20DecimalTextBox.Text = this.currentRow.D20.ToString();
            }
        }

        /// <summary>
        /// Set the values in Label Field
        /// </summary>
        private void SetLabelFieldValues()
        {
            if (this.currentRow.IsL01Null())
            {
                this.Row1LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row1LabelTextBox.Text = this.currentRow.L01;
            }

            if (this.currentRow.IsL02Null())
            {
                this.Row2LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row2LabelTextBox.Text = this.currentRow.L02;
            }

            if (this.currentRow.IsL03Null())
            {
                this.Row3LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row3LabelTextBox.Text = this.currentRow.L03;
            }

            if (this.currentRow.IsL04Null())
            {
                this.Row4LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row4LabelTextBox.Text = this.currentRow.L04;
            }

            if (this.currentRow.IsL05Null())
            {
                this.Row5LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row5LabelTextBox.Text = this.currentRow.L05;
            }

            if (this.currentRow.IsL06Null())
            {
                this.Row6LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row6LabelTextBox.Text = this.currentRow.L06;
            }

            if (this.currentRow.IsL07Null())
            {
                this.Row7LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row7LabelTextBox.Text = this.currentRow.L07;
            }

            if (this.currentRow.IsL08Null())
            {
                this.Row8LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row8LabelTextBox.Text = this.currentRow.L08;
            }

            if (this.currentRow.IsL09Null())
            {
                this.Row9LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row9LabelTextBox.Text = this.currentRow.L09;
            }

            if (this.currentRow.IsL10Null())
            {
                this.Row10LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row10LabelTextBox.Text = this.currentRow.L10;
            }

            if (this.currentRow.IsL11Null())
            {
                this.Row11LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row11LabelTextBox.Text = this.currentRow.L11;
            }

            if (this.currentRow.IsL12Null())
            {
                this.Row12LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row12LabelTextBox.Text = this.currentRow.L12;
            }

            if (this.currentRow.IsL13Null())
            {
                this.Row13LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row13LabelTextBox.Text = this.currentRow.L13;
            }

            if (this.currentRow.IsL14Null())
            {
                this.Row14LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row14LabelTextBox.Text = this.currentRow.L14;
            }

            if (this.currentRow.IsL15Null())
            {
                this.Row15LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row15LabelTextBox.Text = this.currentRow.L15;
            }

            if (this.currentRow.IsL16Null())
            {
                this.Row16LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row16LabelTextBox.Text = this.currentRow.L16;
            }

            if (this.currentRow.IsL17Null())
            {
                this.Row17LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row17LabelTextBox.Text = this.currentRow.L17;
            }

            if (this.currentRow.IsL18Null())
            {
                this.Row18LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row18LabelTextBox.Text = this.currentRow.L18;
            }

            if (this.currentRow.IsL19Null())
            {
                this.Row19LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row19LabelTextBox.Text = this.currentRow.L19;
            }

            if (this.currentRow.IsL20Null())
            {
                this.Row20LabelTextBox.Text = string.Empty;
            }
            else
            {
                this.Row20LabelTextBox.Text = this.currentRow.L20;
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>SliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields;
            int currentfeecodeID;

            ////Check the Required Fields
            if (string.IsNullOrEmpty(this.FormNumberTextBox.Text) || string.IsNullOrEmpty(this.EffectiveDateTextBox.Text) || string.IsNullOrEmpty(this.DefaultDescriptionTextBox.Text) || string.IsNullOrEmpty(this.FeeCalculationTextBox.Text))
            {
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = true;
                this.FormNumberTextBox.Focus();
                return sliceValidationFields;
            }

            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                ////Newly insert record
                currentfeecodeID = -99;
            }
            else
            {
                ////Update existed record
                currentfeecodeID = this.feeCatId;
            }

            ////check whether the form number and Effective Date are already existed in DB
            if (this.form81001control.WorkItem.F81001_CheckEventFeeCatalog(currentfeecodeID, this.FormNumberTextBox.Text, Convert.ToDateTime(this.EffectiveDateTextBox.Text)) == -1)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F81001_ExistingRecord"), SharedFunctions.GetResourceString("DuplicateRecordTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.FormNumberTextBox.Focus();
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.FormNo = this.masterFormNo;
                sliceValidationFields.RequiredFieldMissing = false;
                return sliceValidationFields;
            }
            else
            {
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.FormNo = this.masterFormNo;
                sliceValidationFields.RequiredFieldMissing = false;
                return sliceValidationFields;
            }
        }

        /// <summary>
        /// Sets the formula value.
        /// </summary>
        /// <param name="currentTextBox">The current text box.</param>
        /// <param name="returnedValue">The returned value.</param>
        private void SetFormulaValue(TerraScanTextBox currentTextBox, FormulaAlteredValue returnedValue)
        {
            if (returnedValue.FlagAltered)
            {
                ////Allow the user to enter maximum 500 characters
                if (returnedValue.AlteredValue.Length > 500)
                {
                    string getMaximum = returnedValue.AlteredValue.ToString();
                    returnedValue.AlteredValue = getMaximum.Substring(0, 500);
                    MessageBox.Show(SharedFunctions.GetResourceString("F81001_InvalidFormulaValue"), SharedFunctions.GetResourceString("F3001_InvalidHeaderValue"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                currentTextBox.Text = returnedValue.AlteredValue;
            }
        }

        /// <summary>
        /// Saves the misc improvement catalog.
        /// </summary>
        private void SaveMiscImprovementCataLog()
        {
            this.Cursor = Cursors.WaitCursor;
            F81001FeeCatalogData.GetFeeCatalogDataTable currentTable = new F81001FeeCatalogData.GetFeeCatalogDataTable();
            F81001FeeCatalogData.GetFeeCatalogRow currenRow = currentTable.NewGetFeeCatalogRow();
            Int16 tempValue;
            string feeCatalogItems = string.Empty;

            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                ////Newly insert record
                currenRow.FeeCatID = -99;
            }
            else
            {
                ////Update existed record
                currenRow.FeeCatID = this.feeCatId;
            }

            Int16.TryParse(this.FormNumberTextBox.Text, out tempValue);
            currenRow.Form = Convert.ToInt32(this.FormNumberTextBox.Text);
            currenRow.Description = this.DefaultDescriptionTextBox.Text;
            currenRow.VFormula = this.FeeCalculationTextBox.Text;
            currenRow.ActionNumber = this.ActionNumberTextBox.Text;
            currenRow.EffectiveDate = this.EffectiveDateTextBox.Text;

            currenRow.L01 = this.Row1LabelTextBox.Text;
            currenRow.L02 = this.Row2LabelTextBox.Text;
            currenRow.L03 = this.Row3LabelTextBox.Text;
            currenRow.L04 = this.Row4LabelTextBox.Text;
            currenRow.L05 = this.Row5LabelTextBox.Text;
            currenRow.L06 = this.Row6LabelTextBox.Text;
            currenRow.L07 = this.Row7LabelTextBox.Text;
            currenRow.L08 = this.Row8LabelTextBox.Text;
            currenRow.L09 = this.Row9LabelTextBox.Text;
            currenRow.L10 = this.Row10LabelTextBox.Text;
            currenRow.L11 = this.Row11LabelTextBox.Text;
            currenRow.L12 = this.Row12LabelTextBox.Text;
            currenRow.L13 = this.Row13LabelTextBox.Text;
            currenRow.L14 = this.Row14LabelTextBox.Text;
            currenRow.L15 = this.Row15LabelTextBox.Text;
            currenRow.L16 = this.Row16LabelTextBox.Text;
            currenRow.L17 = this.Row17LabelTextBox.Text;
            currenRow.L18 = this.Row18LabelTextBox.Text;
            currenRow.L19 = this.Row19LabelTextBox.Text;
            currenRow.L20 = this.Row20LabelTextBox.Text;

            currenRow.F01 = this.Row1FormulaTextBox.Text;
            currenRow.F02 = this.Row2FormulaTextBox.Text;
            currenRow.F03 = this.Row3FormulaTextBox.Text;
            currenRow.F04 = this.Row4FormulaTextBox.Text;
            currenRow.F05 = this.Row5FormulaTextBox.Text;
            currenRow.F06 = this.Row6FormulaTextBox.Text;
            currenRow.F07 = this.Row7FormulaTextBox.Text;
            currenRow.F08 = this.Row8FormulaTextBox.Text;
            currenRow.F09 = this.Row9FormulaTextBox.Text;
            currenRow.F10 = this.Row10FormulaTextBox.Text;
            currenRow.F11 = this.Row11FormulaTextBox.Text;
            currenRow.F12 = this.Row12FormulaTextBox.Text;
            currenRow.F13 = this.Row13FormulaTextBox.Text;
            currenRow.F14 = this.Row14FormulaTextBox.Text;
            currenRow.F15 = this.Row15FormulaTextBox.Text;
            currenRow.F16 = this.Row16FormulaTextBox.Text;
            currenRow.F17 = this.Row17FormulaTextBox.Text;
            currenRow.F18 = this.Row18FormulaTextBox.Text;
            currenRow.F19 = this.Row19FormulaTextBox.Text;
            currenRow.F20 = this.Row20FormulaTextBox.Text;

            Int16.TryParse(this.Row1DecimalTextBox.Text, out tempValue);
            currenRow.D01 = tempValue;
            Int16.TryParse(this.Row2DecimalTextBox.Text, out tempValue);
            currenRow.D02 = tempValue;
            Int16.TryParse(this.Row3DecimalTextBox.Text, out tempValue);
            currenRow.D03 = tempValue;
            Int16.TryParse(this.Row4DecimalTextBox.Text, out tempValue);
            currenRow.D04 = tempValue;
            Int16.TryParse(this.Row5DecimalTextBox.Text, out tempValue);
            currenRow.D05 = tempValue;
            Int16.TryParse(this.Row6DecimalTextBox.Text, out tempValue);
            currenRow.D06 = tempValue;
            Int16.TryParse(this.Row7DecimalTextBox.Text, out tempValue);
            currenRow.D07 = tempValue;
            Int16.TryParse(this.Row8DecimalTextBox.Text, out tempValue);
            currenRow.D08 = tempValue;
            Int16.TryParse(this.Row9DecimalTextBox.Text, out tempValue);
            currenRow.D09 = tempValue;
            Int16.TryParse(this.Row10DecimalTextBox.Text, out tempValue);
            currenRow.D10 = tempValue;
            Int16.TryParse(this.Row11DecimalTextBox.Text, out tempValue);
            currenRow.D11 = tempValue;
            Int16.TryParse(this.Row12DecimalTextBox.Text, out tempValue);
            currenRow.D12 = tempValue;
            Int16.TryParse(this.Row13DecimalTextBox.Text, out tempValue);
            currenRow.D13 = tempValue;
            Int16.TryParse(this.Row14DecimalTextBox.Text, out tempValue);
            currenRow.D14 = tempValue;
            Int16.TryParse(this.Row15DecimalTextBox.Text, out tempValue);
            currenRow.D15 = tempValue;
            Int16.TryParse(this.Row16DecimalTextBox.Text, out tempValue);
            currenRow.D16 = tempValue;
            Int16.TryParse(this.Row17DecimalTextBox.Text, out tempValue);
            currenRow.D17 = tempValue;
            Int16.TryParse(this.Row18DecimalTextBox.Text, out tempValue);
            currenRow.D18 = tempValue;
            Int16.TryParse(this.Row19DecimalTextBox.Text, out tempValue);
            currenRow.D19 = tempValue;
            Int16.TryParse(this.Row20DecimalTextBox.Text, out tempValue);
            currenRow.D20 = tempValue;

            currentTable.Rows.Add(currenRow);
            feeCatalogItems = TerraScanCommon.GetXmlString(currentTable);
            int returnValue = this.form81001control.WorkItem.F81001_SaveEventFeeCatalog(currenRow.FeeCatID, feeCatalogItems, TerraScanCommon.UserId);
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            this.EffectiveDateTextBox.Text = dateSelected;
            this.EffectiveMonthCalendar.Visible = false;
        }

        /// <summary>
        /// Checks the input value validity.
        /// </summary>
        /// <param name="validValue">The valid value.</param>
        /// <param name="typeCode">The type code.</param>
        /// <returns>bool value</returns>
        private bool CheckInputValueValidity(ref string validValue, TypeCode typeCode)
        {
            bool returnValue = false;
            switch (typeCode)
            {
                case TypeCode.DateTime:
                    DateTime outDatetime;

                    if (DateTime.TryParse(validValue, out outDatetime) && outDatetime <= this.maximumDate && outDatetime >= this.minimunDate)
                    {
                        validValue = outDatetime.ToString(this.dateFormat);
                        returnValue = true;
                    }
                    else
                    {
                        validValue = System.DateTime.Now.ToString(this.dateFormat);
                    }

                    break;
                case TypeCode.Decimal:
                    Decimal outDecimal;

                    if (Decimal.TryParse(validValue, out outDecimal))
                    {
                        returnValue = true;
                    }

                    validValue = outDecimal.ToString("$#,##0.00");

                    break;
                case TypeCode.Int16:
                    Int16 outInteger;

                    if (Int16.TryParse(validValue, out outInteger))
                    {
                        returnValue = true;
                    }

                    validValue = outInteger.ToString();
                    break;
            }

            return returnValue;
        }

        /// <summary>
        /// struct to track formula being altered
        /// </summary>
        private struct FormulaAlteredValue
        {
            /// <summary>
            /// the value altered at calculator
            /// </summary>
            public string AlteredValue;

            /// <summary>
            /// flag to identify wether value altered at calculator
            /// </summary>
            public bool FlagAltered;
        }

        #endregion Private Methods

    }
}
