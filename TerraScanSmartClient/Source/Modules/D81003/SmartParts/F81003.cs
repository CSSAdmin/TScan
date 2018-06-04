//--------------------------------------------------------------------------------------------
// <copyright file="F81003.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F81003.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Dec 08        Sadha Shivudu M    Created
//*********************************************************************************/

namespace D81003
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infrastructure.Interface;
    using TerraScan.BusinessEntities;
    using System.Text.RegularExpressions;
    using System.Web.Services.Protocols;
    using TerraScan.UI.Controls;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;

    #endregion namespace

    /// <summary>
    /// F81003 Smartpart
    /// </summary>
    [SmartPart]
    public partial class F81003 : BaseSmartPart
    {
        #region instance variables

        /// <summary>
        /// instance variable to hold the F82006Controller
        /// </summary>
        private F81003Controller form81003Control;

        /// <summary>
        /// instance variable to hold the page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// instance variable to hold the selection catalog dataset
        /// </summary>
        private F81003SelectionCatalogData selectionCatalogData = new F81003SelectionCatalogData();

        /// <summary>
        /// instance variable to hold the form keyId value
        /// </summary>
        private int keyId;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// instacnce variable to hold the catalog identity
        /// </summary>
        private int? catalogIdentity = null;

        /// <summary>
        /// instance variable to hold the category identity
        /// </summary>
        private int categoryIdentity = -1;

        /// <summary>
        /// the formula value selected
        /// </summary>
        private string loadedFormulaValue;

        #endregion instance variables

        #region formslice instance variables

        /// <summary>
        /// instance variable to hold the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  instance variable to hold the red color value
        /// </summary>
        private int redColor;

        /// <summary>
        ///  instance variable to hold the green color value
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  instance variable to hold the blue color value
        /// </summary>
        private int blueColor;

        /// <summary>
        /// instance variable to hold the master form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// instance variable to hold the form master edit permission value
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion formslice instance variables

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F81003"/> class.
        /// </summary>
        public F81003()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F82006"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F81003(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;

            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;

            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.SelectionCatalogPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SelectionCatalogPictureBox.Height, this.SelectionCatalogPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion constructor

        #region event publication

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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        #endregion event publication

        #region property

        /// <summary>
        /// Gets or sets the form81003 control.
        /// </summary>
        /// <value>The form81003 control.</value>
        [CreateNew]
        public F81003Controller Form81003Control
        {
            get
            {
                return this.form81003Control;
            }

            set
            {
                this.form81003Control = value;
            }
        }

        #endregion property

        #region event subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data.Equals(this.masterFormNo))
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = this.SelectionCatalogPictureBox.Height;
                sliceResize.SliceFormHeight = this.SelectionCatalogPictureBox.Height;
                this.SelectionCatalogPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SelectionCatalogPictureBox.Height, this.SelectionCatalogPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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
                if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
                {
                    if (this.selectionCatalogData.GetSelectionCatalog.Rows.Count > 0)
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

                this.CategoryComboBox.Focus();
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
            if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
            {
                this.pageLoadStatus = true;
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.catalogIdentity = this.keyId;
                this.ClearSelectionCatalogDetails();
                this.LoadCategoryComboValues();
                this.PopulateSelectionCatalogDetails();
                this.CategoryComboBox.Focus();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.pageLoadStatus = false;
            }
        }

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) && this.permissionFields.newPermission)
            {
                this.Cursor = Cursors.WaitCursor;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ClearSelectionCatalogDetails();
                this.categoryIdentity = -1;
                this.LoadCategoryComboValues();
                this.catalogIdentity = null;
                this.EnableControls(false);
                this.ControlLock(false);
                this.EffectiveDateTextBox.Text = DateTime.Today.ToShortDateString();
                this.CategoryComboBox.Focus();
                this.Cursor = Cursors.Default;
            }
            else
            {
                this.EnableControls(true);
                this.ControlLock(true);
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
            if (this.PermissionFiled.deletePermission && this.keyId > 0)
            {
                this.form81003Control.WorkItem.F81003_DeleteSelectionCatalog(this.keyId);
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
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                if (this.permissionFields.newPermission || this.permissionFields.editPermission)
                {
                    this.ValidateSliceForm(eventArgs);
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
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                if (this.permissionFields.editPermission || this.permissionFields.newPermission)
                {
                    this.SaveSelectionCatalogDitails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.ControlLock(true);
                this.EnableControls(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            this.Cursor = Cursors.WaitCursor;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.catalogIdentity = this.keyId;
            this.ClearSelectionCatalogDetails();
            this.LoadCategoryComboValues();
            this.PopulateSelectionCatalogDetails();
            this.CategoryComboBox.Focus();
            this.Cursor = Cursors.Default;
        }

        #endregion event subscription

        #region protected methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
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

        #endregion protected methods

        #region event handler methods

        /// <summary>
        /// Handles the Load event of the F81003 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F81003_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageLoadStatus = true;
                this.FlagSliceForm = true;
                this.catalogIdentity = this.keyId;
                this.ClearSelectionCatalogDetails();
                this.LoadCategoryComboValues();
                this.PopulateSelectionCatalogDetails();
                this.CategoryComboBox.Focus();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.pageLoadStatus = false;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the SelectionCatalogPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionCatalogPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.SelectionCatalogFormSliceToolTip.SetToolTip(this.SelectionCatalogPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SelectionCatalogPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionCatalogPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the Click event of the EffectiveDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EffectiveDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EffectiveDateTextBox.Text.Trim()))
                {
                    EffectiveDateTimePicker.Value = Convert.ToDateTime(EffectiveDateTextBox.Text);
                }
                else
                {
                    EffectiveDateTimePicker.Value = DateTime.Today;
                }

                EffectiveDateTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the EffectiveDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EffectiveDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.EffectiveDateTextBox.Text = EffectiveDateTimePicker.Text;
                this.ParentForm.ActiveControl = EffectiveDateTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the EffectiveDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EffectiveDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode.Equals(9))
                {
                    SendKeys.Send("{Esc}");
                }

                this.MultiplyCheckBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the CategoryComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CategoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.CategoryComboBox.SelectedIndex >= 0)
                {
                    int.TryParse(this.CategoryComboBox.SelectedValue.ToString(), out this.categoryIdentity);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ValueCalculationCButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValueCalculationCButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindFormulaBuilderGridSource();
                this.SetFormulaValue(this.FeeCalculationTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("TotalFormula"), this.FeeCalculationTextBox.Text));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To Load the Formula value
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void FormulaBuilderDialog_Load(object sender, EventArgs e)
        {
            try
            {
                ((FormulaBuilderDialog)sender).Controls[0].Controls[0].Controls[0].Controls[0].Text = this.loadedFormulaValue;
                this.loadedFormulaValue = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion event handler methods

        #region private methods

        /// <summary>
        /// Clears the selection catalog details.
        /// </summary>
        private void ClearSelectionCatalogDetails()
        {
            this.CatalogIDTextBox.Text = string.Empty;
            this.SelectionTextBox.Text = string.Empty;
            this.UnitsTextBox.Text = string.Empty;
            this.EffectiveDateTextBox.Text = string.Empty;
            this.MultiplyCheckBox.Checked = false;
            this.UnitFeeTextBox.Text = string.Empty;
            this.FeeCalculationTextBox.Text = string.Empty;
            this.CategoryComboBox.Text = string.Empty;
            this.CategoryComboBox.DataSource = null;
            this.categoryIdentity = -1;
        }

        /// <summary>
        /// Loads the category combo values.
        /// </summary>
        private void LoadCategoryComboValues()
        {
            F81003SelectionCatalogData categoryData = new F81003SelectionCatalogData();
            categoryData = this.form81003Control.WorkItem.F81003_ListSelectionCategory(TerraScanCommon.UserId);

            if (categoryData.ListSelectionCategory.Rows.Count > 0)
            {
                //// load the data into combo box.
                this.CategoryComboBox.DataSource = categoryData.ListSelectionCategory;
                this.CategoryComboBox.DisplayMember = categoryData.ListSelectionCategory.VCategoryColumn.ColumnName;
                this.CategoryComboBox.ValueMember = categoryData.ListSelectionCategory.CategoryIDColumn.ColumnName;
                this.CategoryComboBox.SelectedValue = this.categoryIdentity;
            }
        }

        /// <summary>
        /// Populates the selection catalog details.
        /// </summary>
        private void PopulateSelectionCatalogDetails()
        {
            this.pageLoadStatus = true;
            this.selectionCatalogData = this.form81003Control.WorkItem.F81003_GetSelectionCatalogDetails(this.keyId);

            if (this.selectionCatalogData.GetSelectionCatalog.Rows.Count > 0)
            {
                this.ControlLock(false || !this.permissionFields.editPermission || !this.formMasterPermissionEdit);
                this.EnableControls(false || !this.permissionFields.editPermission || !this.formMasterPermissionEdit);

                int.TryParse(this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.CategoryIDColumn.ColumnName].ToString(), out this.categoryIdentity);
                this.CategoryComboBox.SelectedValue = this.categoryIdentity;

                this.CatalogIDTextBox.Text = this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.CatalogIDColumn.ColumnName].ToString();
                this.catalogIdentity = Convert.ToInt32(this.CatalogIDTextBox.Text.Trim());

                this.SelectionTextBox.Text = this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.SelectionColumn.ColumnName].ToString();
                this.UnitsTextBox.Text = this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.UnitsColumn.ColumnName].ToString();
                this.EffectiveDateTextBox.Text = this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.EffectiveDateColumn.ColumnName].ToString();

                bool flagMultiplyChecked;
                bool.TryParse(this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.MultiplyColumn.ColumnName].ToString(), out flagMultiplyChecked);
                this.MultiplyCheckBox.Checked = flagMultiplyChecked;

                this.UnitFeeTextBox.Text = this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.UnitFeeColumn.ColumnName].ToString();
                this.FeeCalculationTextBox.Text = this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.FeeCalcColumn.ColumnName].ToString();
            }
            else
            {
                this.EnableControls(true);
                this.ControlLock(true);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.EntireSelectionCatalogPanel.Enabled = !enable;
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void ControlLock(bool controlLock)
        {
            this.SelectionTextBox.LockKeyPress = controlLock;
            this.UnitsTextBox.LockKeyPress = controlLock;
            this.EffectiveDateTextBox.LockKeyPress = controlLock;
            this.UnitFeeTextBox.LockKeyPress = controlLock;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.CategoryComboBox.Text.Trim()))
            {
                this.CategoryComboBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F81003RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.EffectiveDateTextBox.Text.Trim()))
            {
                this.EffectiveDateTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F81003RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.SelectionTextBox.Text.Trim()))
            {
                this.SelectionTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F81003RequiredFieldMissing");
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Saves the contractor employee ditails.
        /// </summary>
        private void SaveSelectionCatalogDitails()
        {
            int returnValue = -1;
            this.ConstructSelectionCatalogDetailsRow();
            string selectionItemsXml = this.GetSelectionCatalogDetailsXmlString();
            returnValue = this.form81003Control.WorkItem.F81003_SaveSelectionCatalog(this.catalogIdentity, selectionItemsXml);
            if (returnValue != -1)
            {
                this.catalogIdentity = returnValue;
            }

            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = returnValue;
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        /// <summary>
        /// Constructs the contractor row.
        /// </summary>
        private void ConstructSelectionCatalogDetailsRow()
        {
            if (this.selectionCatalogData.GetSelectionCatalog.Rows.Count > 0)
            {
                this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.CategoryIDColumn.ColumnName] = this.categoryIdentity;
                this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.SelectionColumn.ColumnName] = this.SelectionTextBox.Text.Trim();
                this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.UnitsColumn.ColumnName] = this.UnitsTextBox.Text.Trim();
                this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.EffectiveDateColumn.ColumnName] = this.EffectiveDateTextBox.DateTextBoxValue;
                this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.MultiplyColumn.ColumnName] = this.MultiplyCheckBox.Checked;
                this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.UnitFeeColumn.ColumnName] = this.UnitFeeTextBox.DecimalTextBoxValue;
                this.selectionCatalogData.GetSelectionCatalog.Rows[0][this.selectionCatalogData.GetSelectionCatalog.FeeCalcColumn.ColumnName] = this.FeeCalculationTextBox.Text.Trim();
            }
            else
            {
                DataRow newSelectionCatalogRow = this.selectionCatalogData.GetSelectionCatalog.NewRow();

                newSelectionCatalogRow[this.selectionCatalogData.GetSelectionCatalog.CategoryIDColumn.ColumnName] = this.categoryIdentity;
                newSelectionCatalogRow[this.selectionCatalogData.GetSelectionCatalog.SelectionColumn.ColumnName] = this.SelectionTextBox.Text.Trim();
                newSelectionCatalogRow[this.selectionCatalogData.GetSelectionCatalog.UnitsColumn.ColumnName] = this.UnitsTextBox.Text.Trim();
                newSelectionCatalogRow[this.selectionCatalogData.GetSelectionCatalog.EffectiveDateColumn.ColumnName] = this.EffectiveDateTextBox.DateTextBoxValue;
                newSelectionCatalogRow[this.selectionCatalogData.GetSelectionCatalog.MultiplyColumn.ColumnName] = this.MultiplyCheckBox.Checked;
                newSelectionCatalogRow[this.selectionCatalogData.GetSelectionCatalog.UnitFeeColumn.ColumnName] = this.UnitFeeTextBox.DecimalTextBoxValue;
                newSelectionCatalogRow[this.selectionCatalogData.GetSelectionCatalog.FeeCalcColumn.ColumnName] = this.FeeCalculationTextBox.Text.Trim();

                this.selectionCatalogData.GetSelectionCatalog.Rows.Add(newSelectionCatalogRow);
                this.selectionCatalogData.GetSelectionCatalog.AcceptChanges();
            }
        }

        /// <summary>
        /// Gets the contractor XML string.
        /// </summary>
        /// <returns>returns contractor xml string</returns>
        private string GetSelectionCatalogDetailsXmlString()
        {
            string selectionCatalogDetailsXml = string.Empty;
            DataSet selectionCatalogDataSet = new DataSet(SharedFunctions.GetResourceString("Root"));
            selectionCatalogDataSet.Merge(this.selectionCatalogData.GetSelectionCatalog);
            selectionCatalogDataSet.Tables[this.selectionCatalogData.GetSelectionCatalog.TableName].TableName = SharedFunctions.GetResourceString("Table");
            selectionCatalogDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionCatalogData.GetSelectionCatalog.CatalogIDColumn.ColumnName);
            selectionCatalogDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionCatalogData.GetSelectionCatalog.CategoryNameColumn.ColumnName);
            selectionCatalogDetailsXml = selectionCatalogDataSet.GetXml();
            return selectionCatalogDetailsXml;
        }

        /// <summary>
        /// Binds the formula builder grid source.
        /// </summary>
        private void BindFormulaBuilderGridSource()
        {
            try
            {
                DataSet currentDataSet = new DataSet();
                DataTable currentTable = new DataTable(SharedFunctions.GetResourceString("SelectedBandValue"));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("F81003Formula"), typeof(double)));
                currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("TotalFormula"), typeof(double)));
                currentDataSet.Tables.Add(currentTable);
                this.FormulaBuilderInfraGrid.DataSource = currentDataSet;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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
            createdColumn = this.FormulaBuilderInfraGrid.DisplayLayout.Bands[SharedFunctions.GetResourceString("SelectedBandValue")].Columns[columnName];

            if (null == createdColumn.Layout.Grid.CalcManager)
            {
                createdColumn.Layout.Grid.CalcManager = new UltraCalcManager();
            }

            FormulaBuilderDialog formulaBuilderDialog = new FormulaBuilderDialog(createdColumn, true);
            this.loadedFormulaValue = valueFromForm;
            formulaBuilderDialog.Load += new EventHandler(this.FormulaBuilderDialog_Load);
            DialogResult result = formulaBuilderDialog.ShowDialog(this.ParentForm);
            if (result.Equals(DialogResult.OK))
            {
                currentFormulaAlteredValue.FlagAltered = true;
                currentFormulaAlteredValue.AlteredValue = formulaBuilderDialog.Formula;
            }
            else
            {
                currentFormulaAlteredValue.FlagAltered = false;
                currentFormulaAlteredValue.AlteredValue = formulaBuilderDialog.Formula;
            }

            this.loadedFormulaValue = string.Empty;
            return currentFormulaAlteredValue;
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

        #endregion private methods
    }
}
