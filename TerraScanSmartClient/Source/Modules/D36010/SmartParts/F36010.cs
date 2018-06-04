//--------------------------------------------------------------------------------------------
// <copyright file="F36010.cs" company="Congruent">
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
// 28 JUN 07	    Suganth Mani       Created
// 11 MAR 09        M.Sadha Shivudu    Implemented TSCO# 5176
// 17 APR 09        M.Sadha Shivudu    Implemented TSCO# 6930
//*********************************************************************************/
namespace D36010
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
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F36010
    /// </summary>
    public partial class F36010 : BaseSmartPart
    {
        #region PrivateMembers

        /// <summary>
        ///  CreatedColumn 
        /// </summary>
        private UltraGridColumn createdColumn;

        /// <summary>
        /// fontroller for the current view
        /// </summary>
        private F36010Controller form36010control;

        /// <summary>
        /// master form no
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// the selected record
        /// </summary>
        private int micodeID;

        /// <summary>
        /// permissions from form master
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// variable to identify page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// default roll year
        /// </summary>
        private int defaultRollYear;

        /// <summary>
        /// dataset
        /// </summary>
        private F36010MiscImprovementCatalog formF36010MiscImprovementCatalog;

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

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
        /// Store the current row values
        /// </summary>
        private F36010MiscImprovementCatalog.GetMICatalogRow currentRow;

        /// <summary>
        /// deprTable
        /// </summary>
        private F36010MiscImprovementCatalog.ListDeprTableDataTable listDeprTable = new F36010MiscImprovementCatalog.ListDeprTableDataTable();

        /// <summary>
        /// variable to hold the rollyear textbox change status
        /// </summary>
        private bool rollYearChanged;

        #endregion PrivateMembers

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36010"/> class.
        /// </summary>
        public F36010()
        {
            InitializeComponent();
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
        public F36010(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.formF36010MiscImprovementCatalog = new F36010MiscImprovementCatalog();
            this.getRollYearConfigurationValue = new CommentsData();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.micodeID = keyID;
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

        #region Enum

        /// <summary>
        /// Used to store the SketchIschecked
        /// </summary>
        private enum SketchIschecked
        {
            /// <summary>
            /// check box is checked
            /// </summary>
            ischecked = 1,

            /// <summary>
            /// Check box is unchecked
            /// </summary>
            isunchecked = 0
        }

        #endregion Enum

        #region Property

        /// <summary>
        /// Gets or sets the form1030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F36010Controller Form36010control
        {
            get { return this.form36010control; }
            set { this.form36010control = value; }
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
            if (this.slicePermissionField.newPermission)
            {
                this.DistrictDefinitionlPanel.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                this.LockControls(false);
                this.CodeTextBox.Focus();
            }
            else
            {
                this.DistrictDefinitionlPanel.Enabled = false;
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
                this.DistrictDefinitionlPanel.Enabled = true;
            }
            else
            {
                this.DistrictDefinitionlPanel.Enabled = false;
                this.LockControls(true);
            }

            this.flagFormLoad = true;
            this.ClearControls();
            this.LoadMiscImprovementCatalog();
            this.CodeTextBox.Focus();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagFormLoad = false;
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
                this.DistrictDefinitionlPanel.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            else
            {
                this.DistrictDefinitionlPanel.Enabled = false;
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.DistrictDefinitionlPanel.Enabled = this.slicePermissionField.editPermission && this.formMasterPermissionEdit;

                if (this.formF36010MiscImprovementCatalog.GetMICatalog.Rows.Count > 0)
                {
                    this.CodeTextBox.Focus();
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                    this.DistrictDefinitionlPanel.Enabled = false;
                    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
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
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                this.form36010control.WorkItem.F36010_DeleteMiscImprovementCatalog(this.micodeID, TerraScan.Common.TerraScanCommon.UserId);
                this.Cursor = Cursors.Default;
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.flagFormLoad = true;
                this.DistrictDefinitionlPanel.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.micodeID = eventArgs.Data.SelectedKeyId;
                this.ClearControls();
                this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.DistrictDefinitionlPanel.Enabled = this.slicePermissionField.editPermission && this.formMasterPermissionEdit;
                this.LoadMiscImprovementCatalog();
                this.CodeTextBox.Focus();
                this.flagFormLoad = false;
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.micodeID = eventArgs.Data.SelectedKeyId;
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
                else if (sender.Equals(this.ValueCalculationCButton))
                {
                    this.SetFormulaValue(this.ValueCalculationTextBox, this.GetFormulaValue(SharedFunctions.GetResourceString("TotalFormula"), this.ValueCalculationTextBox.Text));
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
        /// Handles the Click event of the RowDButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RowDButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button currentRowButton = (Button)sender;
                string currentRowCatalogChoiceXml = string.Empty;
                string modifiedCurrentRowCatalogChoiceXml = string.Empty;
                string fieldNum = currentRowButton.Tag.ToString();

                FormInfo miscSelectionFormInfo = TerraScanCommon.GetFormInfo(36012);
                if (miscSelectionFormInfo.openPermission.Equals(1))
                {
                    Form miscImprovementSelectionForm = new Form();
                    object[] optionalParameter = new object[4];
                    optionalParameter[0] = this.micodeID;
                    optionalParameter[1] = fieldNum;
                    optionalParameter[2] = this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice;
                    optionalParameter[3] = this.permissionFields;

                    miscImprovementSelectionForm = this.form36010control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(36012, optionalParameter, this.form36010control.WorkItem);
                    if (miscImprovementSelectionForm != null)
                    {
                        if (miscImprovementSelectionForm.ShowDialog().Equals(DialogResult.OK))
                        {
                            this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice.Clear();
                            bool recordUpdated;
                            bool.TryParse(TerraScanCommon.GetObject(miscImprovementSelectionForm, "RecordUpdated").ToString(), out recordUpdated);
                            this.formF36010MiscImprovementCatalog.Merge((F36010MiscImprovementCatalog)TerraScanCommon.GetObject(miscImprovementSelectionForm, "MiscImprovementCatalogChoiceDataSet"));

                            if (recordUpdated)
                            {
                                this.ToEnableFormMasterEditMode();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + miscSelectionFormInfo.visibleName + SharedFunctions.GetResourceString("AdminMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                this.ToEnableFormMasterEditMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Load event of the F36010 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36010_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagFormLoad = true;
                this.DistrictDefinitionlPanel.Enabled = true;
                this.SetDefaultRollYear();
                this.ClearControls();
                this.LoadMiscImprovementCatalog();
                if (this.formF36010MiscImprovementCatalog.GetMICatalog.Rows.Count > 0)
                {
                    this.CodeTextBox.Focus();
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
            finally
            {
                this.Cursor = Cursors.Default;
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
            finally
            {
                this.Cursor = Cursors.Default;
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Leave event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad && this.rollYearChanged)
                {
                    this.LoadDepreciationTableComboBox();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.rollYearChanged = true;
                }

                this.ToEnableFormMasterEditMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Sketch CheckBox Events

        /// <summary>
        /// Handles the CheckedChanged event of the Row1SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row1SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row1SketchCheckBox, this.Row1ComboCheckBox, this.Row1FormulaTextBox, this.Row1CButton, this.Row1DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row2SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row2SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row2SketchCheckBox, this.Row2ComboCheckBox, this.Row2FormulaTextBox, this.Row2CButton, this.Row2DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row3SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row3SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row3SketchCheckBox, this.Row3ComboCheckBox, this.Row3FormulaTextBox, this.Row3CButton, this.Row3DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row4SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row4SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row4SketchCheckBox, this.Row4ComboCheckBox, this.Row4FormulaTextBox, this.Row4CButton, this.Row1DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row5SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row5SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row5SketchCheckBox, this.Row5ComboCheckBox, this.Row5FormulaTextBox, this.Row5CButton, this.Row5DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row6SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row6SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row6SketchCheckBox, this.Row6ComboCheckBox, this.Row6FormulaTextBox, this.Row6CButton, this.Row6DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row7SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row7SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row7SketchCheckBox, this.Row7ComboCheckBox, this.Row7FormulaTextBox, this.Row7CButton, this.Row7DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row8SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row8SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row8SketchCheckBox, this.Row8ComboCheckBox, this.Row8FormulaTextBox, this.Row8CButton, this.Row8DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row9SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row9SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row9SketchCheckBox, this.Row9ComboCheckBox, this.Row9FormulaTextBox, this.Row9CButton, this.Row9DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row10SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row10SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row10SketchCheckBox, this.Row10ComboCheckBox, this.Row10FormulaTextBox, this.Row10CButton, this.Row10DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row11SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row11SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row11SketchCheckBox, this.Row11ComboCheckBox, this.Row11FormulaTextBox, this.Row11CButton, this.Row11DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row12SketchCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row12SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.DisableFormulaFieldsOnSketchCheked(this.Row12SketchCheckBox, this.Row12ComboCheckBox, this.Row12FormulaTextBox, this.Row12CButton, this.Row12DButton);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Sketch CheckBox Events

        #region Combo CheckBox Events

        /// <summary>
        /// Handles the CheckedChanged event of the Row1ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row1ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row1ComboCheckBox, this.Row1SketchCheckBox, this.Row1CButton, this.Row1DButton, this.Row1FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row2ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row2ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row2ComboCheckBox, this.Row2SketchCheckBox, this.Row2CButton, this.Row2DButton, this.Row2FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row3ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row3ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row3ComboCheckBox, this.Row3SketchCheckBox, this.Row3CButton, this.Row3DButton, this.Row3FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row4ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row4ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row4ComboCheckBox, this.Row4SketchCheckBox, this.Row4CButton, this.Row4DButton, this.Row4FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row5ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row5ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row5ComboCheckBox, this.Row5SketchCheckBox, this.Row5CButton, this.Row5DButton, this.Row5FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row6ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row6ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row6ComboCheckBox, this.Row6SketchCheckBox, this.Row6CButton, this.Row6DButton, this.Row6FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row7ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row7ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row7ComboCheckBox, this.Row7SketchCheckBox, this.Row7CButton, this.Row7DButton, this.Row7FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row8ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row8ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row8ComboCheckBox, this.Row8SketchCheckBox, this.Row8CButton, this.Row8DButton, this.Row8FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row9ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row9ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row9ComboCheckBox, this.Row9SketchCheckBox, this.Row9CButton, this.Row9DButton, this.Row9FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row10ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row10ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row10ComboCheckBox, this.Row10SketchCheckBox, this.Row10CButton, this.Row10DButton, this.Row10FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row11ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row11ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row11ComboCheckBox, this.Row11SketchCheckBox, this.Row11CButton, this.Row11DButton, this.Row11FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Row12ComboCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Row12ComboCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToggleFandDIconsOnComboChecked(this.Row12ComboCheckBox, this.Row12SketchCheckBox, this.Row12CButton, this.Row12DButton, this.Row12FormulaTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Combo CheckBox Events

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
            this.createdColumn = this.QueryEngineGrid.DisplayLayout.Bands[SharedFunctions.GetResourceString("SelectedBandValue")].Columns[columnName];

            if (null == this.createdColumn.Layout.Grid.CalcManager)
            {
                this.createdColumn.Layout.Grid.CalcManager = new UltraCalcManager();
            }

            FormulaBuilderDialog dlg = new FormulaBuilderDialog(this.createdColumn, true);
            this.loadedFormulaValue = valueFromForm;
            dlg.Load += new EventHandler(this.Dlg_Load);
            dlg.FunctionInitializing += new FunctionInitializingEventHandler(FormulaBuilder_FunctionInitializing);
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

            dlg.FunctionInitializing -= new FunctionInitializingEventHandler(FormulaBuilder_FunctionInitializing);
            this.loadedFormulaValue = string.Empty;
            return currentFormulaAlteredValue;
        }

        /// <summary>
        /// Load Formula Builder
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void Dlg_Load(object sender, EventArgs e)
        {
            ((FormulaBuilderDialog)sender).Controls[0].Controls[0].Controls[0].Controls[0].Text = this.loadedFormulaValue;
            this.loadedFormulaValue = string.Empty;
            ////Infragistics.Win.UltraWinTree.UltraTree winTree = (Infragistics.Win.UltraWinTree.UltraTree)((FormulaBuilderDialog)sender).Controls[0].Controls[2].Controls[1].Controls[0];
        }

        #region FormulaBuilder_FunctionInitializing
        //// This event fires for each function added to the list
        //// of functions in the FormulaBuilder and provides the 
        //// opportunity to cancel them. 

        /// <summary>
        /// Handles the FunctionInitializing event of the formulaBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaBuilder.FunctionInitializingEventArgs"/> instance containing the event data.</param>
        private void FormulaBuilder_FunctionInitializing(object sender, FunctionInitializingEventArgs e)
        {
            switch (e.Function.Category)
            {
                case "Financial":
                    e.Cancel = true;
                    break;
                case "Engineering":
                    e.Cancel = true;
                    break;
                case "LookupAndReference":
                    e.Cancel = true;
                    break;
                case "DateAndTime":
                    if (!e.Function.Name.Equals("datediff") && !e.Function.Name.Equals("month")
                        && !e.Function.Name.Equals("year") && !e.Function.Name.Equals("now"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Information":
                    if (!e.Function.Name.Equals("isnumber") && !e.Function.Name.Equals("null"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Logical":
                    if (e.Function.Name.Equals("TRUE"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Math":
                    if (!e.Function.Name.Equals("abs") && !e.Function.Name.Equals("mod")
                        && !e.Function.Name.Equals("pi") && !e.Function.Name.Equals("round")
                        && !e.Function.Name.Equals("sqrt"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Statistical":
                    e.Cancel = true;
                    break;
                case "TextAndData":
                    if (e.Function.Name.Equals("char") || e.Function.Name.Equals("code"))
                    {
                        e.Cancel = true;
                    }
                    break;
            }
        }

        #endregion FormulaBuilder_FunctionInitializing

        /// <summary>
        /// Binds the source.
        /// </summary>
        /// <param name="flagrowLevelCalculation">if set to <c>true</c> [flagrow level calculation].</param>
        private void BindSource(bool flagrowLevelCalculation)
        {
            DataSet currentDataSet = new DataSet();
            DataTable currentTable = new DataTable(SharedFunctions.GetResourceString(SharedFunctions.GetResourceString("SelectedBandValue")));
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
            currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("FormulaQulatity"), typeof(double)));
            currentTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("FormulaCondition"), typeof(double)));

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

        /// <summary>
        /// Sets the default roll year.
        /// </summary>
        private void SetDefaultRollYear()
        {
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            this.getRollYearConfigurationValue = this.form36010control.WorkItem.GetConfigDetails(SharedFunctions.GetResourceString("DefaultRollYear"));
            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                if (int.TryParse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.defaultRollYear))
                {
                    this.RollYearTextBox.Text = this.defaultRollYear.ToString();
                }
            }
        }

        /// <summary>
        /// Inits the with primary combo.
        /// </summary>
        private void InitWithPrimaryComboBox()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.WithPrimaryComboBox.DataSource = commonData.ComboBoxDataTable;
            this.WithPrimaryComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.WithPrimaryComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
            this.WithPrimaryComboBox.SelectedValue = 0;
        }

        /// <summary>
        /// Loads the depreciation table combo box.
        /// </summary>
        private void LoadDepreciationTableComboBox()
        {
            int tempRollYear;
            ////To assign a empty row in the combo box
            DataRow customRow = this.listDeprTable.NewRow();
            this.listDeprTable.Clear();
            customRow[this.listDeprTable.DeprTableIDColumn.ColumnName] = "0";
            customRow[this.listDeprTable.DeprNameColumn.ColumnName] = string.Empty;
            this.listDeprTable.Rows.Add(customRow);

            int.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
            this.listDeprTable.Merge(this.Form36010control.WorkItem.F36010_ListDeprTable(tempRollYear).ListDeprTable);

            if (this.listDeprTable.Rows.Count > 0)
            {
                this.DepreciationTableComboBox.DataSource = this.listDeprTable;
                this.DepreciationTableComboBox.DisplayMember = this.listDeprTable.DeprNameColumn.ColumnName;
                this.DepreciationTableComboBox.ValueMember = this.listDeprTable.DeprTableIDColumn.ColumnName;
            }

            this.rollYearChanged = false;
        }

        /// <summary>
        /// Assigns the with primary combo box.
        /// </summary>
        /// <param name="withPrimayValue">The with primay value.</param>
        private void AssignWithPrimaryComboBox(int withPrimayValue)
        {
            if (withPrimayValue >= 0)
            {
                this.WithPrimaryComboBox.SelectedValue = withPrimayValue;
            }
            else
            {
                this.WithPrimaryComboBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Assigns the depreciation table combo box.
        /// </summary>
        /// <param name="depreciationTableId">The depreciation table id.</param>
        private void AssignDepreciationTableComboBox(int depreciationTableId)
        {
            if (this.listDeprTable.Rows.Count > 0)
            {
                if (depreciationTableId >= 0)
                {
                    this.DepreciationTableComboBox.SelectedValue = depreciationTableId;
                }
                else
                {
                    this.DepreciationTableComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.CodeTextBox.Text = string.Empty;
            this.ValueCalculationTextBox.Text = string.Empty;
            this.DefaultDescriptionTextBox.Text = string.Empty;

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

            this.Row1SketchCheckBox.Checked = false;
            this.Row2SketchCheckBox.Checked = false;
            this.Row3SketchCheckBox.Checked = false;
            this.Row4SketchCheckBox.Checked = false;
            this.Row5SketchCheckBox.Checked = false;
            this.Row6SketchCheckBox.Checked = false;
            this.Row7SketchCheckBox.Checked = false;
            this.Row8SketchCheckBox.Checked = false;
            this.Row9SketchCheckBox.Checked = false;
            this.Row10SketchCheckBox.Checked = false;
            this.Row11SketchCheckBox.Checked = false;
            this.Row12SketchCheckBox.Checked = false;

            this.Row1ComboCheckBox.Checked = false;
            this.Row2ComboCheckBox.Checked = false;
            this.Row3ComboCheckBox.Checked = false;
            this.Row4ComboCheckBox.Checked = false;
            this.Row5ComboCheckBox.Checked = false;
            this.Row6ComboCheckBox.Checked = false;
            this.Row7ComboCheckBox.Checked = false;
            this.Row8ComboCheckBox.Checked = false;
            this.Row9ComboCheckBox.Checked = false;
            this.Row10ComboCheckBox.Checked = false;
            this.Row11ComboCheckBox.Checked = false;
            this.Row12ComboCheckBox.Checked = false;

            this.Row1CButton.Enabled = true;
            this.Row2CButton.Enabled = true;
            this.Row3CButton.Enabled = true;
            this.Row4CButton.Enabled = true;
            this.Row5CButton.Enabled = true;
            this.Row6CButton.Enabled = true;
            this.Row7CButton.Enabled = true;
            this.Row8CButton.Enabled = true;
            this.Row9CButton.Enabled = true;
            this.Row10CButton.Enabled = true;
            this.Row11CButton.Enabled = true;
            this.Row12CButton.Enabled = true;

            this.Row1DButton.Enabled = false;
            this.Row2DButton.Enabled = false;
            this.Row3DButton.Enabled = false;
            this.Row4DButton.Enabled = false;
            this.Row5DButton.Enabled = false;
            this.Row6DButton.Enabled = false;
            this.Row7DButton.Enabled = false;
            this.Row8DButton.Enabled = false;
            this.Row9DButton.Enabled = false;
            this.Row10DButton.Enabled = false;
            this.Row11DButton.Enabled = false;
            this.Row12DButton.Enabled = false;

            this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice.Clear();
            this.SetDefaultRollYear();
            this.LoadDepreciationTableComboBox();
            this.DepreciationTableComboBox.SelectedIndex = 0;
            this.WithPrimaryComboBox.SelectedValue = 0;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void LockControls(bool enable)
        {
            this.CodeTextBox.LockKeyPress = enable;
            this.ValueCalculationTextBox.LockKeyPress = enable;
            this.ValueCalculationTextBox.ReadOnly = true;
            this.DefaultDescriptionTextBox.LockKeyPress = enable;
            this.WithPrimaryComboBox.Enabled = !enable;
            this.DepreciationTableComboBox.Enabled = !enable;

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

            this.Row1SketchCheckBox.Enabled = !enable;
            this.Row2SketchCheckBox.Enabled = !enable;
            this.Row3SketchCheckBox.Enabled = !enable;
            this.Row4SketchCheckBox.Enabled = !enable;
            this.Row5SketchCheckBox.Enabled = !enable;
            this.Row6SketchCheckBox.Enabled = !enable;
            this.Row7SketchCheckBox.Enabled = !enable;
            this.Row8SketchCheckBox.Enabled = !enable;
            this.Row9SketchCheckBox.Enabled = !enable;
            this.Row10SketchCheckBox.Enabled = !enable;
            this.Row11SketchCheckBox.Enabled = !enable;
            this.Row12SketchCheckBox.Enabled = !enable;

            this.Row1ComboCheckBox.Enabled = !enable;
            this.Row2ComboCheckBox.Enabled = !enable;
            this.Row3ComboCheckBox.Enabled = !enable;
            this.Row4ComboCheckBox.Enabled = !enable;
            this.Row5ComboCheckBox.Enabled = !enable;
            this.Row6ComboCheckBox.Enabled = !enable;
            this.Row7ComboCheckBox.Enabled = !enable;
            this.Row8ComboCheckBox.Enabled = !enable;
            this.Row9ComboCheckBox.Enabled = !enable;
            this.Row10ComboCheckBox.Enabled = !enable;
            this.Row11ComboCheckBox.Enabled = !enable;
            this.Row12ComboCheckBox.Enabled = !enable;

            this.Row1CButton.Enabled = !this.Row1ComboCheckBox.Checked && !this.Row1SketchCheckBox.Checked && !enable;
            this.Row2CButton.Enabled = !this.Row2ComboCheckBox.Checked && !this.Row2SketchCheckBox.Checked && !enable;
            this.Row3CButton.Enabled = !this.Row3ComboCheckBox.Checked && !this.Row3SketchCheckBox.Checked && !enable;
            this.Row4CButton.Enabled = !this.Row4ComboCheckBox.Checked && !this.Row4SketchCheckBox.Checked && !enable;
            this.Row5CButton.Enabled = !this.Row5ComboCheckBox.Checked && !this.Row5SketchCheckBox.Checked && !enable;
            this.Row6CButton.Enabled = !this.Row6ComboCheckBox.Checked && !this.Row6SketchCheckBox.Checked && !enable;
            this.Row7CButton.Enabled = !this.Row7ComboCheckBox.Checked && !this.Row7SketchCheckBox.Checked && !enable;
            this.Row8CButton.Enabled = !this.Row8ComboCheckBox.Checked && !this.Row8SketchCheckBox.Checked && !enable;
            this.Row9CButton.Enabled = !this.Row9ComboCheckBox.Checked && !this.Row9SketchCheckBox.Checked && !enable;
            this.Row10CButton.Enabled = !this.Row10ComboCheckBox.Checked && !this.Row10SketchCheckBox.Checked && !enable;
            this.Row11CButton.Enabled = !this.Row11ComboCheckBox.Checked && !this.Row11SketchCheckBox.Checked && !enable;
            this.Row12CButton.Enabled = !this.Row12ComboCheckBox.Checked && !this.Row12SketchCheckBox.Checked && !enable;

            this.Row1DButton.Enabled = this.Row1ComboCheckBox.Checked && !enable;
            this.Row2DButton.Enabled = this.Row2ComboCheckBox.Checked && !enable;
            this.Row3DButton.Enabled = this.Row3ComboCheckBox.Checked && !enable;
            this.Row4DButton.Enabled = this.Row4ComboCheckBox.Checked && !enable;
            this.Row5DButton.Enabled = this.Row5ComboCheckBox.Checked && !enable;
            this.Row6DButton.Enabled = this.Row6ComboCheckBox.Checked && !enable;
            this.Row7DButton.Enabled = this.Row7ComboCheckBox.Checked && !enable;
            this.Row8DButton.Enabled = this.Row8ComboCheckBox.Checked && !enable;
            this.Row9DButton.Enabled = this.Row9ComboCheckBox.Checked && !enable;
            this.Row10DButton.Enabled = this.Row10ComboCheckBox.Checked && !enable;
            this.Row11DButton.Enabled = this.Row11ComboCheckBox.Checked && !enable;
            this.Row12DButton.Enabled = this.Row12ComboCheckBox.Checked && !enable;
        }

        /// <summary>
        /// Loads the misc improvement catalog.
        /// </summary>
        private void LoadMiscImprovementCatalog()
        {
            this.formF36010MiscImprovementCatalog.GetMICatalog.Clear();
            this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice.Clear();
            this.formF36010MiscImprovementCatalog = this.form36010control.WorkItem.F36010_GetMiscImprovementCatalog(this.micodeID);
            this.InitWithPrimaryComboBox();
            this.SetControlValues();
        }

        /// <summary>
        /// Sets the control values.
        /// </summary>
        private void SetControlValues()
        {
            if (this.formF36010MiscImprovementCatalog.GetMICatalog.Rows.Count > 0)
            {
                this.currentRow = (F36010MiscImprovementCatalog.GetMICatalogRow)this.formF36010MiscImprovementCatalog.GetMICatalog.Rows[0];

                if (this.currentRow.IsVFormulaNull())
                {
                    this.ValueCalculationTextBox.Text = string.Empty;
                }
                else
                {
                    this.ValueCalculationTextBox.Text = this.currentRow.VFormula;
                }

                if (this.currentRow.IsDescriptionNull())
                {
                    this.DefaultDescriptionTextBox.Text = string.Empty;
                }
                else
                {
                    this.DefaultDescriptionTextBox.Text = this.currentRow.Description;
                }

                if (this.currentRow.IsMICodeNull())
                {
                    this.CodeTextBox.Text = string.Empty;
                }
                else
                {
                    this.CodeTextBox.Text = this.currentRow.MICode.Trim();
                }

                if (this.currentRow.IsRollYearNull())
                {
                    this.RollYearTextBox.Text = string.Empty;
                }
                else
                {
                    this.RollYearTextBox.Text = this.currentRow.RollYear.ToString();
                }

                this.LoadDepreciationTableComboBox();

                if (this.currentRow.IsWithPrimaryNull())
                {
                    this.WithPrimaryComboBox.SelectedIndex = -1;
                }
                else
                {
                    if (this.currentRow.WithPrimary)
                    {
                        this.AssignWithPrimaryComboBox(1);
                    }
                    else
                    {
                        this.AssignWithPrimaryComboBox(0);
                    }
                }

                if (this.currentRow.IsDeprTableIDNull())
                {
                    this.DepreciationTableComboBox.SelectedIndex = 0;
                }
                else
                {
                    int tempDeprTableId;
                    int.TryParse(this.currentRow.DeprTableID.ToString(), out tempDeprTableId);
                    this.AssignDepreciationTableComboBox(tempDeprTableId);
                }

                this.SetDecimalFieldValues();
                this.SetFormulaFieldValues();
                this.SetLabelFieldValues();
            }
            else
            {
                this.DistrictDefinitionlPanel.Enabled = false;
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

            this.SetSketchCheckBoxs();
            this.SetComboCheckBoxs();
        }

        /// <summary>
        /// Sets the sketch check boxs.
        /// </summary>
        private void SetSketchCheckBoxs()
        {
            if (this.currentRow.IsS01Null())
            {
                this.Row1SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S01.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row1SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row1SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS02Null())
            {
                this.Row2SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S02.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row2SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row2SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS03Null())
            {
                this.Row3SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S03.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row3SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row3SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS04Null())
            {
                this.Row4SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S04.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row4SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row4SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS05Null())
            {
                this.Row5SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S05.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row5SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row5SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS06Null())
            {
                this.Row6SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S06.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row6SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row6SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS07Null())
            {
                this.Row7SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S07.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row7SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row7SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS08Null())
            {
                this.Row8SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S08.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row8SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row8SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS09Null())
            {
                this.Row9SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S09.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row9SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row9SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS10Null())
            {
                this.Row10SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S10.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row10SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row10SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS11Null())
            {
                this.Row11SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S11.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row11SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row11SketchCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsS12Null())
            {
                this.Row12SketchCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.S12.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row12SketchCheckBox.Checked = true;
                }
                else
                {
                    this.Row12SketchCheckBox.Checked = false;
                }
            }
        }

        /// <summary>
        /// Sets the combo check boxs.
        /// </summary>
        private void SetComboCheckBoxs()
        {
            if (this.currentRow.IsC01Null())
            {
                this.Row1ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C01.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row1ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row1ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC02Null())
            {
                this.Row2ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C02.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row2ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row2ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC03Null())
            {
                this.Row3ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C03.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row3ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row3ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC04Null())
            {
                this.Row4ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C04.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row4ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row4ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC05Null())
            {
                this.Row5ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C05.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row5ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row5ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC06Null())
            {
                this.Row6ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C06.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row6ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row6ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC07Null())
            {
                this.Row7ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C07.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row7ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row7ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC08Null())
            {
                this.Row8ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C08.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row8ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row8ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC09Null())
            {
                this.Row9ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C09.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row9ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row9ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC10Null())
            {
                this.Row10ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C10.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row10ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row10ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC11Null())
            {
                this.Row11ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C11.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row11ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row11ComboCheckBox.Checked = false;
                }
            }

            if (this.currentRow.IsC12Null())
            {
                this.Row12ComboCheckBox.Checked = false;
            }
            else
            {
                if (this.currentRow.C12.Equals((int)SketchIschecked.ischecked))
                {
                    this.Row12ComboCheckBox.Checked = true;
                }
                else
                {
                    this.Row12ComboCheckBox.Checked = false;
                }
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
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>SliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields;
            int rollYear, currentmicodeID;

            if (string.IsNullOrEmpty(this.CodeTextBox.Text) || string.IsNullOrEmpty(this.RollYearTextBox.Text) || string.IsNullOrEmpty(this.DefaultDescriptionTextBox.Text) || string.IsNullOrEmpty(this.ValueCalculationTextBox.Text))
            {
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = true;
                this.CodeTextBox.Focus();
                return sliceValidationFields;
            }

            // validation for decimal text box's
            foreach (Control rowEntirePanel in this.DistrictDefinitionlPanel.Controls)
            {
                if (rowEntirePanel.GetType().Equals(typeof(Panel)) && rowEntirePanel.Name.Contains("EntirePanel"))
                {
                    foreach (Control decimalTextBoxPanel in rowEntirePanel.Controls)
                    {
                        if (decimalTextBoxPanel.GetType().Equals(typeof(Panel)) && decimalTextBoxPanel.Name.Contains("DecimalPanel"))
                        {
                            foreach (Control decimalTextBox in decimalTextBoxPanel.Controls)
                            {
                                if (decimalTextBox.GetType().Equals(typeof(TerraScanTextBox)) && decimalTextBox.Name.Contains("DecimalTextBox"))
                                {
                                    int currentDecimalValue;

                                    if (int.TryParse(decimalTextBox.Text, out currentDecimalValue))
                                    {
                                        if (currentDecimalValue < 0 || currentDecimalValue > 4)
                                        {
                                            MessageBox.Show(SharedFunctions.GetResourceString("DecimalRange"), SharedFunctions.GetResourceString("F81001_InvalidDecimalHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            decimalTextBox.Focus();
                                            sliceValidationFields.DisableNewMethod = true;
                                            sliceValidationFields.ErrorMessage = string.Empty;
                                            sliceValidationFields.FormNo = formNo;
                                            sliceValidationFields.RequiredFieldMissing = false;
                                            return sliceValidationFields;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int formulaMaxLength = this.formF36010MiscImprovementCatalog.GetMICatalog.VFormulaColumn.MaxLength;

            if (this.ValueCalculationTextBox.Text.Length > formulaMaxLength ||
                this.Row1FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row2FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row3FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row4FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row5FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row6FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row7FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row8FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row9FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row10FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row11FormulaTextBox.Text.Length > formulaMaxLength ||
                this.Row12FormulaTextBox.Text.Length > formulaMaxLength)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("Formula value must not exceed 1500 characters."), SharedFunctions.GetResourceString("TerraScan T2 - Invalid Data"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
                return sliceValidationFields;
            }

            Int16 tempValue;
            Int16.TryParse(this.RollYearTextBox.Text, out tempValue);
            if (tempValue == 0)
            {
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = true;
                this.RollYearTextBox.Focus();
                return sliceValidationFields;
            }

            if (int.TryParse(this.RollYearTextBox.Text, out rollYear))
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    currentmicodeID = -99;
                }
                else
                {
                    currentmicodeID = this.micodeID;
                }

                if (this.form36010control.WorkItem.F36010_CheckMiscImprovementCatalog(currentmicodeID, this.CodeTextBox.Text, rollYear) == -1)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("DuplicateRollYear"), SharedFunctions.GetResourceString("DuplicateRecordTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.CodeTextBox.Focus();
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
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidRollYear"), SharedFunctions.GetResourceString("DistrictHeaderMessage"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.RollYearTextBox.Focus();
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.FormNo = formNo;
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
                currentTextBox.Text = returnedValue.AlteredValue;
            }
        }

        /// <summary>
        /// Saves the misc improvement cata log.
        /// </summary>
        private void SaveMiscImprovementCataLog()
        {
            F36010MiscImprovementCatalog.GetMICatalogDataTable currentTable = new F36010MiscImprovementCatalog.GetMICatalogDataTable();
            F36010MiscImprovementCatalog.GetMICatalogRow currenMICatalogRow = currentTable.NewGetMICatalogRow();

            Int16 tempValue;

            string miscCatalogItems = string.Empty;
            string miscCatalogChoiceItems = string.Empty;

            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                currenMICatalogRow.MICodeID = -99;
            }
            else
            {
                currenMICatalogRow.MICodeID = this.micodeID;
            }

            currenMICatalogRow.MICode = this.CodeTextBox.Text.Trim();
            currenMICatalogRow.Description = this.DefaultDescriptionTextBox.Text;
            Int16.TryParse(this.RollYearTextBox.Text, out tempValue);

            ////if (tempValue == 0)
            ////{
            ////    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    this.RollYearTextBox.BackColor = Color.Yellow;
            ////    this.RollYearTextBox.Focus();
            ////}

            currenMICatalogRow.RollYear = tempValue;
            currenMICatalogRow.VFormula = this.ValueCalculationTextBox.Text;

            if (!string.IsNullOrEmpty(this.WithPrimaryComboBox.Text.Trim()) && this.WithPrimaryComboBox.SelectedValue != null)
            {
                if (this.WithPrimaryComboBox.SelectedValue.ToString().Equals("1"))
                {
                    currenMICatalogRow.WithPrimary = true;
                }
                else
                {
                    currenMICatalogRow.WithPrimary = false;
                }
            }

            if (!string.IsNullOrEmpty(this.DepreciationTableComboBox.Text.Trim()) && this.DepreciationTableComboBox.SelectedValue != null)
            {
                int tempDeprTableId;
                int.TryParse(this.DepreciationTableComboBox.SelectedValue.ToString(), out tempDeprTableId);
                currenMICatalogRow.DeprTableID = tempDeprTableId;
            }
            else
            {
                currenMICatalogRow.DeprTableID = 0;
            }

            currenMICatalogRow.L01 = this.Row1LabelTextBox.Text;
            currenMICatalogRow.L02 = this.Row2LabelTextBox.Text;
            currenMICatalogRow.L03 = this.Row3LabelTextBox.Text;
            currenMICatalogRow.L04 = this.Row4LabelTextBox.Text;
            currenMICatalogRow.L05 = this.Row5LabelTextBox.Text;
            currenMICatalogRow.L06 = this.Row6LabelTextBox.Text;
            currenMICatalogRow.L07 = this.Row7LabelTextBox.Text;
            currenMICatalogRow.L08 = this.Row8LabelTextBox.Text;
            currenMICatalogRow.L09 = this.Row9LabelTextBox.Text;
            currenMICatalogRow.L10 = this.Row10LabelTextBox.Text;
            currenMICatalogRow.L11 = this.Row11LabelTextBox.Text;
            currenMICatalogRow.L12 = this.Row12LabelTextBox.Text;

            currenMICatalogRow.F01 = this.Row1FormulaTextBox.Text;
            currenMICatalogRow.F02 = this.Row2FormulaTextBox.Text;
            currenMICatalogRow.F03 = this.Row3FormulaTextBox.Text;
            currenMICatalogRow.F04 = this.Row4FormulaTextBox.Text;
            currenMICatalogRow.F05 = this.Row5FormulaTextBox.Text;
            currenMICatalogRow.F06 = this.Row6FormulaTextBox.Text;
            currenMICatalogRow.F07 = this.Row7FormulaTextBox.Text;
            currenMICatalogRow.F08 = this.Row8FormulaTextBox.Text;
            currenMICatalogRow.F09 = this.Row9FormulaTextBox.Text;
            currenMICatalogRow.F10 = this.Row10FormulaTextBox.Text;
            currenMICatalogRow.F11 = this.Row11FormulaTextBox.Text;
            currenMICatalogRow.F12 = this.Row12FormulaTextBox.Text;

            Int16.TryParse(this.Row1DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D01 = tempValue;
            Int16.TryParse(this.Row2DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D02 = tempValue;
            Int16.TryParse(this.Row3DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D03 = tempValue;
            Int16.TryParse(this.Row4DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D04 = tempValue;
            Int16.TryParse(this.Row5DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D05 = tempValue;
            Int16.TryParse(this.Row6DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D06 = tempValue;
            Int16.TryParse(this.Row7DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D07 = tempValue;
            Int16.TryParse(this.Row8DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D08 = tempValue;
            Int16.TryParse(this.Row9DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D09 = tempValue;
            Int16.TryParse(this.Row10DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D10 = tempValue;
            Int16.TryParse(this.Row11DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D11 = tempValue;
            Int16.TryParse(this.Row12DecimalTextBox.Text, out tempValue);
            currenMICatalogRow.D12 = tempValue;

            if (this.Row1SketchCheckBox.Checked)
            {
                currenMICatalogRow.S01 = 1;
            }
            else
            {
                currenMICatalogRow.S01 = 0;
            }

            if (this.Row2SketchCheckBox.Checked)
            {
                currenMICatalogRow.S02 = 1;
            }
            else
            {
                currenMICatalogRow.S02 = 0;
            }

            if (this.Row3SketchCheckBox.Checked)
            {
                currenMICatalogRow.S03 = 1;
            }
            else
            {
                currenMICatalogRow.S03 = 0;
            }

            if (this.Row4SketchCheckBox.Checked)
            {
                currenMICatalogRow.S04 = 1;
            }
            else
            {
                currenMICatalogRow.S04 = 0;
            }

            if (this.Row5SketchCheckBox.Checked)
            {
                currenMICatalogRow.S05 = 1;
            }
            else
            {
                currenMICatalogRow.S05 = 0;
            }

            if (this.Row6SketchCheckBox.Checked)
            {
                currenMICatalogRow.S06 = 1;
            }
            else
            {
                currenMICatalogRow.S06 = 0;
            }

            if (this.Row7SketchCheckBox.Checked)
            {
                currenMICatalogRow.S07 = 1;
            }
            else
            {
                currenMICatalogRow.S07 = 0;
            }

            if (this.Row8SketchCheckBox.Checked)
            {
                currenMICatalogRow.S08 = 1;
            }
            else
            {
                currenMICatalogRow.S08 = 0;
            }

            if (this.Row9SketchCheckBox.Checked)
            {
                currenMICatalogRow.S09 = 1;
            }
            else
            {
                currenMICatalogRow.S09 = 0;
            }

            if (this.Row10SketchCheckBox.Checked)
            {
                currenMICatalogRow.S10 = 1;
            }
            else
            {
                currenMICatalogRow.S10 = 0;
            }

            if (this.Row11SketchCheckBox.Checked)
            {
                currenMICatalogRow.S11 = 1;
            }
            else
            {
                currenMICatalogRow.S11 = 0;
            }

            if (this.Row12SketchCheckBox.Checked)
            {
                currenMICatalogRow.S12 = 1;
            }
            else
            {
                currenMICatalogRow.S12 = 0;
            }

            if (this.Row1ComboCheckBox.Checked)
            {
                currenMICatalogRow.C01 = 1;
            }
            else
            {
                currenMICatalogRow.C01 = 0;
            }

            if (this.Row2ComboCheckBox.Checked)
            {
                currenMICatalogRow.C02 = 1;
            }
            else
            {
                currenMICatalogRow.C02 = 0;
            }

            if (this.Row3ComboCheckBox.Checked)
            {
                currenMICatalogRow.C03 = 1;
            }
            else
            {
                currenMICatalogRow.C03 = 0;
            }

            if (this.Row4ComboCheckBox.Checked)
            {
                currenMICatalogRow.C04 = 1;
            }
            else
            {
                currenMICatalogRow.C04 = 0;
            }

            if (this.Row5ComboCheckBox.Checked)
            {
                currenMICatalogRow.C05 = 1;
            }
            else
            {
                currenMICatalogRow.C05 = 0;
            }

            if (this.Row6ComboCheckBox.Checked)
            {
                currenMICatalogRow.C06 = 1;
            }
            else
            {
                currenMICatalogRow.C06 = 0;
            }

            if (this.Row7ComboCheckBox.Checked)
            {
                currenMICatalogRow.C07 = 1;
            }
            else
            {
                currenMICatalogRow.C07 = 0;
            }

            if (this.Row8ComboCheckBox.Checked)
            {
                currenMICatalogRow.C08 = 1;
            }
            else
            {
                currenMICatalogRow.C08 = 0;
            }

            if (this.Row9ComboCheckBox.Checked)
            {
                currenMICatalogRow.C09 = 1;
            }
            else
            {
                currenMICatalogRow.C09 = 0;
            }

            if (this.Row10ComboCheckBox.Checked)
            {
                currenMICatalogRow.C10 = 1;
            }
            else
            {
                currenMICatalogRow.C10 = 0;
            }

            if (this.Row11ComboCheckBox.Checked)
            {
                currenMICatalogRow.C11 = 1;
            }
            else
            {
                currenMICatalogRow.C11 = 0;
            }

            if (this.Row12ComboCheckBox.Checked)
            {
                currenMICatalogRow.C12 = 1;
            }
            else
            {
                currenMICatalogRow.C12 = 0;
            }

            currentTable.Rows.Add(currenMICatalogRow);
            currentTable.AcceptChanges();
            miscCatalogItems = TerraScanCommon.GetXmlString(currentTable);
            this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice.Columns.Remove(this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice.IsCommitedColumn.ColumnName);
            miscCatalogChoiceItems = TerraScanCommon.GetXmlString(this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice);
            int returnValue = this.form36010control.WorkItem.F36010_SaveMiscImprovementCatalog(currenMICatalogRow.MICodeID, miscCatalogItems, TerraScan.Common.TerraScanCommon.UserId, miscCatalogChoiceItems);
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
        }

        /// <summary>
        /// To enable the form master save and cancel button on edit mode
        /// </summary>
        private void ToEnableFormMasterEditMode()
        {
            if (!this.flagFormLoad && (this.PermissionEdit && (this.pageMode == TerraScanCommon.PageModeTypes.View)))
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Disables the formula fields on sketch cheked.
        /// </summary>
        /// <param name="currentRowSketchCheckbox">The current row sketch checkbox.</param>
        /// <param name="currentRowComboCheckBox">The current row combo check box.</param>
        /// <param name="currentRowFormulaTextBox">The current row formula text box.</param>
        /// <param name="currentRowFormulaButton">The current row formula button.</param>
        /// <param name="currentRowDataButton">The current row data button.</param>
        private void DisableFormulaFieldsOnSketchCheked(TerraScanCheckBox currentRowSketchCheckbox, TerraScanCheckBox currentRowComboCheckBox, TerraScanTextBox currentRowFormulaTextBox, Button currentRowFormulaButton, Button currentRowDataButton)
        {
            if (currentRowSketchCheckbox.Checked)
            {
                currentRowFormulaTextBox.Text = string.Empty;
                currentRowFormulaTextBox.Enabled = false;
                currentRowFormulaButton.Enabled = false;

                if (currentRowComboCheckBox.Checked)
                {
                    currentRowDataButton.BringToFront();
                    currentRowDataButton.Enabled = true;
                }
                else
                {
                    currentRowDataButton.SendToBack();
                    currentRowDataButton.Enabled = false;
                }
            }
            else
            {
                if (currentRowComboCheckBox.Checked)
                {
                    currentRowFormulaTextBox.Text = string.Empty;
                    currentRowFormulaTextBox.Enabled = false;
                    currentRowDataButton.BringToFront();
                    currentRowDataButton.Enabled = true;
                }
                else
                {
                    currentRowFormulaTextBox.Enabled = true;
                    currentRowFormulaButton.Enabled = true;
                    currentRowDataButton.SendToBack();
                    currentRowDataButton.Enabled = false;
                }
            }

            this.ToEnableFormMasterEditMode();
        }

        /// <summary>
        /// Toggles the fand D icons on combo checked.
        /// </summary>
        /// <param name="currentRowComboCheckBox">The current row combo check box.</param>
        /// <param name="currentRowSketchCheckBox">The current row sketch check box.</param>
        /// <param name="currentRowFormulaButton">The current row formula button.</param>
        /// <param name="currentRowDataButton">The current row data button.</param>
        /// <param name="currentRowFormulaTextBox">The current row formula text box.</param>
        private void ToggleFandDIconsOnComboChecked(TerraScanCheckBox currentRowComboCheckBox, TerraScanCheckBox currentRowSketchCheckBox, Button currentRowFormulaButton, Button currentRowDataButton, TerraScanTextBox currentRowFormulaTextBox)
        {
            if (currentRowComboCheckBox.Checked)
            {
                currentRowFormulaTextBox.Text = string.Empty;
                currentRowFormulaTextBox.Enabled = false;
                currentRowFormulaButton.SendToBack();
                currentRowFormulaButton.Enabled = false;
                currentRowDataButton.BringToFront();
                currentRowDataButton.Enabled = true;
            }
            else
            {
                if (currentRowSketchCheckBox.Checked)
                {
                    currentRowFormulaTextBox.Text = string.Empty;
                    currentRowFormulaButton.BringToFront();
                    currentRowFormulaTextBox.Enabled = false;
                    currentRowDataButton.SendToBack();
                    currentRowDataButton.Enabled = false;
                }
                else
                {
                    currentRowFormulaTextBox.Enabled = true;
                    currentRowFormulaButton.BringToFront();
                    currentRowFormulaButton.Enabled = true;
                    currentRowDataButton.SendToBack();
                    currentRowDataButton.Enabled = false;
                }
            }

            this.ToEnableFormMasterEditMode();
        }

        /// <summary>
        /// Gets the current row catalog choice XML string.
        /// </summary>
        /// <param name="fieldNum">The field num.</param>
        /// <returns>CurrentRow Xml String</returns>
        private string GetCurrentRowCatalogChoiceXmlString(int fieldNum)
        {
            DataSet currentRowCatalogChoiceDataSet = new DataSet("Root");

            string filterCond = this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice.FieldNumColumn.ColumnName + " = " + fieldNum.ToString();
            DataRow[] catalogChoiceRows = this.formF36010MiscImprovementCatalog.GetMiscCatalogChoice.Select(filterCond);

            if (catalogChoiceRows.Length > 0)
            {
                currentRowCatalogChoiceDataSet.Merge(catalogChoiceRows);
            }

            return currentRowCatalogChoiceDataSet.GetXml();
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

        private void Row3SketchLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
