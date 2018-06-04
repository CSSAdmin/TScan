//--------------------------------------------------------------------------------------------
// <copyright file="F81004.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F81004 Selection.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26 Dec 08        Sadha Shivudu M    Created
// 15 May 09        Malliga            Coding added for the issue 4697
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
    using Infragistics.Win;

    #endregion namespace

    /// <summary>
    /// F81004 Smartpart
    /// </summary>
    [SmartPart]
    public partial class F81004 : BaseSmartPart
    {
        #region instance variables

        /// <summary>
        /// instance variable to hold the F82006Controller
        /// </summary>
        private F81004Controller form81004Control;

        /// <summary>
        /// instance variable to hold the page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// instance variable to hold the selection catalog dataset
        /// </summary>
        private F81004SelectionData selectionData = new F81004SelectionData();

        /// <summary>
        /// instance variable to hold the form keyId value
        /// </summary>
        private int keyId;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// Child RowHeight
        /// </summary>
        private int childRowHeight;

        /// <summary>
        /// Parent RowHeight
        /// </summary>
        private int parentRowHeight;

        /// <summary>
        /// Parent RowCount
        /// </summary>
        private int parentRowCount;

        /// <summary>
        /// Child RowCount
        /// </summary>
        private int childRowCount;

        /// <summary>
        /// row Count status
        /// </summary>
        private bool rowCount;

        /// <summary>
        /// slice ValueListName
        /// </summary>
        private string selectionValueListName;

        /// <summary>
        /// Holds Active Row details
        /// </summary>
        private Infragistics.Win.UltraWinGrid.UltraGridRow activeRow;

        /// <summary>
        /// Holds Active Cell details
        /// </summary>
        private Infragistics.Win.UltraWinGrid.UltraGridCell activeCell;

        /// <summary>
        /// basePanelScrolled variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool basePanelScrolled;

        /// <summary>
        /// instance variable to hold the cell value change
        /// </summary>
        private bool cellValueChanged;

        /// <summary>
        /// instance variable to hold cancel button clicked
        /// </summary>
        private bool cancelClicked = false;

        /// <summary>
        /// instance variable to hold the y-axis point.
        /// </summary>
        private int yaxisPoint;

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
        /// Initializes a new instance of the <see cref="T:F81004"/> class.
        /// </summary>
        public F81004()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F81004"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F81004(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
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
            this.SelectionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SelectionPictureBox.Height, this.SelectionPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
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
        /// Gets or sets the form81004 control.
        /// </summary>
        /// <value>The form81004 control.</value>
        [CreateNew]
        public F81004Controller Form81004Control
        {
            get
            {
                return this.form81004Control;
            }

            set
            {
                this.form81004Control = value;
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
                this.Height = this.SelectionPictureBox.Height;
                sliceResize.SliceFormHeight = this.SelectionPictureBox.Height;
                this.SelectionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SelectionPictureBox.Height, this.SelectionPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
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
            if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
            {
                if (this.selectionData.ListCategoryHeaderDetails.Rows.Count > 0)
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
                this.LoadSelectionCategoryDetails();
                this.SetSmartpartHeight();
                this.cancelClicked = true;
                this.rowCount = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.pageLoadStatus = false;
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
                if (this.permissionFields.editPermission)
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
                if (this.permissionFields.editPermission)
                {
                    this.SaveSelectionItems();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
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
            this.LoadSelectionCategoryDetails();
            this.SetSmartpartHeight();
            this.cancelClicked = true;
            this.rowCount = false;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Handles the Load event of the F81004 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F81004_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageLoadStatus = true;
                this.FlagSliceForm = true;
                this.LoadSelectionCategoryDetails();
                this.SetSmartpartHeight();
                this.rowCount = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.pageLoadStatus = false;

                ////Events for Master Form scroll
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(this.Scroll_Click);
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(this.Smartpart_Scroll);
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
        /// Handles the Click event of the SelectionPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the SelectionPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.SelectionFormSliceToolTip.SetToolTip(this.SelectionPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion event handler methods

        #region selection grid events

        /// <summary>
        /// Handles the InitializeLayout event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row.ChildBands != null)
                {
                    // Expand all Parent Rows
                    this.ExpandAllParentRows(e.Row);
                }

                if (!e.Row.IsAddRow)
                {
                    //// Change the appearance of unitfee cell
                    this.UnitFeeCellAppearance(e.Row);

                    //// Populate the selection combo values
                    this.PopulateSelectionComboItems(e.Row);
                }

                if (e.Row.Band.Index.Equals(1))
                {
                    if (e.Row.HasParent(this.SelectionGrid.DisplayLayout.Bands[0]))
                    {
                        this.childRowHeight = e.Row.Height;

                        if (this.rowCount)
                        {
                            this.childRowCount++;
                        }
                    }
                }

                if (e.Row.Band.Index.Equals(0))
                {
                    //// make the first row of band 0 specifice cells back color.
                    if (!string.IsNullOrEmpty(e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].Value.ToString()))
                    {
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].Appearance.BackColor = Color.FromArgb(61, 61, 61);
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].Appearance.BorderColor = Color.Black;
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].Appearance.BackColor = Color.FromArgb(61, 61, 61);
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].Appearance.BorderColor = Color.Black;
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].Appearance.BackColor = Color.FromArgb(61, 61, 61);
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].Appearance.BorderColor = Color.Black;
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].Appearance.BackColor = Color.FromArgb(61, 61, 61);
                        e.Row.Cells[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].Appearance.BorderColor = Color.Black;
                    }

                    this.parentRowHeight = e.Row.Height;

                    if (this.rowCount)
                    {
                        this.parentRowCount++;
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeTemplateAddRow event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
        {
            try
            {
                this.activeCell = this.SelectionGrid.ActiveCell;

                //// Checks for the TemplateRow Column and Changes the style to Combo
                //// loads datasource to the dropdownlist
                if (e.TemplateAddRow.Band.Index.Equals(1))
                {
                    int categoryId;
                    if (e.TemplateAddRow.ParentRow.Cells[this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.ColumnName].Value != null)
                    {
                        int.TryParse(e.TemplateAddRow.ParentRow.Cells[this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.ColumnName].Value.ToString(), out categoryId);

                        ////Initialize the selection valuelists based on category id
                        this.InitializeSelectionValueLists(categoryId);

                        e.TemplateAddRow.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].ValueList = this.SelectionGrid.DisplayLayout.ValueLists[this.selectionValueListName];
                        e.TemplateAddRow.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Value = "Selection";
                        e.TemplateAddRow.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                        e.TemplateAddRow.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                        e.TemplateAddRow.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Activation = Activation.NoEdit;
                    }

                    if (this.activeCell != null && this.activeCell.Column.Header.Caption.Equals(this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName))
                    {
                        this.SetSmartpartHeight();

                        ////Set scroll position
                        if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                        {
                            this.yaxisPoint = this.yaxisPoint + 25;
                            ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeEnterEditMode event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_BeforeEnterEditMode(object sender, CancelEventArgs e)
        {
            try
            {
                this.activeCell = this.SelectionGrid.ActiveCell;

                if (this.activeCell != null)
                {
                    if (this.activeCell.Band.Index.Equals(1) && this.formMasterPermissionEdit && this.permissionFields.editPermission)
                    {
                        if (this.activeCell.Column.Header.Caption.Equals(this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName))
                        {
                            this.activeCell.Appearance.TextHAlign = HAlign.Left;
                            this.activeCell.Appearance.TextVAlign = VAlign.Middle;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                this.activeRow = this.SelectionGrid.ActiveRow;
                this.activeCell = this.SelectionGrid.ActiveCell;
                int categoryId = -1;
                int catalogId = -1;
                if (this.activeCell != null)
                {
                    if (this.activeCell.Column.Header.Caption.Equals(this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName))
                    {
                        this.SetEditRecord();
                        this.SelectionGrid.UpdateData();
                        int.TryParse(this.activeRow.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Value.ToString(), out catalogId);
                        int.TryParse(this.activeRow.Cells[this.selectionData.GetSelectionDetails.CategoryIDColumn.ColumnName].Value.ToString(), out categoryId);

                        ////populate the active row with the selected catalog details
                        this.PopulateSelectionRowDetails(categoryId, catalogId);

                        ////calculate the total fee
                        this.CalculateTotalFee();
                    }
                    else if (this.activeCell.Column.Header.Caption.Equals(this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName))
                    {
                        this.SetEditRecord();
                        this.cellValueChanged = true;
                        this.activeRow.Cells[this.selectionData.GetSelectionDetails.IsEditColumn.ColumnName].Value = true;
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                this.activeCell = this.SelectionGrid.ActiveCell;
                if (this.activeCell != null)
                {
                    if (this.activeCell.Column.Header.Caption.Equals(this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName))
                    {
                        decimal qtyValue;
                        decimal minValue = 0;
                        decimal maxValue = 9999999999999999.99M;

                        //// checks for the appropriate qty cell value and resets with the default value if not
                        decimal.TryParse(this.activeCell.Text, out qtyValue);
                        if (qtyValue > minValue && qtyValue <= maxValue)
                        {
                            this.activeCell.Value = qtyValue;
                        }
                        else
                        {
                            this.activeCell.Value = 0;
                        }

                        this.SelectionGrid.UpdateData();
                    }

                    if (this.activeCell.Band.Index.Equals(1) && this.formMasterPermissionEdit && this.permissionFields.editPermission)
                    {
                        if (this.activeCell.Column.Header.Caption.Equals(this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName))
                        {
                            this.activeCell.Appearance.TextHAlign = HAlign.Right;
                            this.activeCell.Appearance.TextVAlign = VAlign.Middle;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterExitEditMode event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                if (this.cellValueChanged)
                {
                    //// calculate the fee
                    this.CalculateFee();

                    ////calculate the total fee
                    this.CalculateTotalFee();

                    //// reset the cell value changed flag
                    this.cellValueChanged = false;
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDataError event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellDataErrorEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_CellDataError(object sender, CellDataErrorEventArgs e)
        {
            try
            {
                ////aviod to raise the grid error event
                e.RaiseErrorEvent = false;
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowFormulaForm();
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                this.basePanelScrolled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.basePanelScrolled)
                {
                    if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        //((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(e.X, e.Y);
                        this.basePanelScrolled = false;
                    }
                }
                else
                {
                    this.basePanelScrolled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    this.ShowFormulaForm();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormulaSyntaxError event of the UltraFeeCalcManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaSyntaxErrorEventArgs"/> instance containing the event data.</param>
        private void UltraFeeCalcManager_FormulaSyntaxError(object sender, FormulaSyntaxErrorEventArgs e)
        {
            try
            {
                e.DisplayErrorMessage = false;
                if (this.cellValueChanged)
                {
                    MessageBox.Show(e.ErrorDisplayText, ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("InvalidFormulaTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormulaCircularityError event of the UltraFeeCalcManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaCircularityErrorEventArgs"/> instance containing the event data.</param>
        private void UltraFeeCalcManager_FormulaCircularityError(object sender, FormulaCircularityErrorEventArgs e)
        {
            try
            {
                e.DisplayErrorMessage = false;
                if (this.cellValueChanged)
                {
                    MessageBox.Show(e.ErrorDisplayText, ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("InvalidFormulaTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterCellActivate event of the SelectionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectionGrid_AfterCellActivate(object sender, EventArgs e)
        {
            //if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            //{
            //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            //}
        }

        #endregion selection grid events

        #region scroll events

        /// <summary>
        /// Handles the Click event of the Scroll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void Scroll_Click(object sender, ScrollEventArgs e)
        {
            try
            {
                this.yaxisPoint = e.NewValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Scroll event of the Smartpart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Smartpart_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion scroll events

        #region private methods

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            if (enable)
            {
                this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellActivation = Activation.NoEdit;
            }
            else
            {
                this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellActivation = Activation.AllowEdit;
            }
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
            return sliceValidationFields;
        }

        /// <summary>
        /// Loads the selection category details.
        /// </summary>
        private void LoadSelectionCategoryDetails()
        {
            ////get the selection details
            this.selectionData = this.form81004Control.WorkItem.F81004_GetSelectionDetails(this.keyId, this.masterFormNo);

            if (this.selectionData.ListCategoryHeaderDetails.Rows.Count > 0)
            {
                //// create the relation between two tables
                this.selectionData.Relations.Add(
                    this.selectionData.ListCategoryHeaderDetails.Columns[this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.ColumnName],
                    this.selectionData.GetSelectionDetails.Columns[this.selectionData.GetSelectionDetails.CategoryIDColumn.ColumnName]);

                this.rowCount = true;
                ////assign data source
                this.SelectionGrid.DataSource = this.selectionData;

                ////check for first band rows and made selection
                if (this.SelectionGrid.Rows[0].ChildBands.Count > 0 && this.SelectionGrid.Rows[0].ChildBands[0].Rows.Count > 0)
                {
                    this.SelectionGrid.Rows[0].ChildBands[0].Rows[0].Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Activate();
                    this.SelectionGrid.PerformAction(UltraGridAction.EnterEditModeAndDropdown);
                }

                this.EnableControls(false || !this.permissionFields.editPermission || !this.formMasterPermissionEdit);
            }
            else
            {
                this.SelectionGrid.DataSource = null;
                this.CreateCategoryAndSelectionDataTable();
            }

            this.ColumnPositioning();
            this.CalculateTotalFee();
        }

        /// <summary>
        /// Creates the category and selection data table.
        /// </summary>
        private void CreateCategoryAndSelectionDataTable()
        {
            DataSet emptyCategorySelectionDataSet = new DataSet(SharedFunctions.GetResourceString("F81004EmptyCategorySelectionDataSet"));
            DataTable selectionCategoryDataTable = new DataTable(SharedFunctions.GetResourceString("F81004SelectionCategoryDataTable"));
            DataColumn[] categoryDataColumn = new DataColumn[] 
            { 
                new DataColumn(this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.Caption), 
                new DataColumn(this.selectionData.ListCategoryHeaderDetails.CategoryColumn.Caption), 
                new DataColumn(this.selectionData.ListCategoryHeaderDetails.UnitColumn.Caption),                
                new DataColumn(this.selectionData.ListCategoryHeaderDetails.QntyColumn.Caption), 
                new DataColumn(this.selectionData.ListCategoryHeaderDetails.FormulaColumn.Caption),                
                new DataColumn(this.selectionData.ListCategoryHeaderDetails.FeeColumn.Caption)
            };

            selectionCategoryDataTable.Columns.AddRange(categoryDataColumn);

            DataRow newCategoryHeaderRow = selectionCategoryDataTable.NewRow();
            newCategoryHeaderRow[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName] = SharedFunctions.GetResourceString("F81004UnitColumn");
            newCategoryHeaderRow[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName] = SharedFunctions.GetResourceString("F81004QntyColumn");
            newCategoryHeaderRow[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName] = SharedFunctions.GetResourceString("F81004UnitFeeColumn");
            newCategoryHeaderRow[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName] = SharedFunctions.GetResourceString("F81004FeeColumn");
            selectionCategoryDataTable.Rows.Add(newCategoryHeaderRow);

            DataTable selectionDetailsDataTable = new DataTable(SharedFunctions.GetResourceString("F81004SelectionDetailsDataTable"));
            DataColumn[] selectionDataColumn = new DataColumn[] 
            { 
                new DataColumn(this.selectionData.GetSelectionDetails.SelectionIDColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.EventIDColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.CategoryIDColumn.Caption),                
                new DataColumn(this.selectionData.GetSelectionDetails.CategoryColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.CatalogIDColumn.Caption),                
                new DataColumn(this.selectionData.GetSelectionDetails.UnitsColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.VQNTYColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.FormulaColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.FeeColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.UnitFeeColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.MultiplyColumn.Caption), 
                new DataColumn(this.selectionData.GetSelectionDetails.SelectionColumn.Caption),
                new DataColumn(this.selectionData.GetSelectionDetails.EffectiveDateColumn.Caption),
                new DataColumn(this.selectionData.GetSelectionDetails.IsEditColumn.Caption),
                new DataColumn(this.selectionData.GetSelectionDetails.CalculatedFeeColumn.Caption)
            };

            selectionDetailsDataTable.Columns.AddRange(selectionDataColumn);

            emptyCategorySelectionDataSet.Tables.Add(selectionCategoryDataTable);
            emptyCategorySelectionDataSet.Tables.Add(selectionDetailsDataTable);

            emptyCategorySelectionDataSet.Relations.Add(emptyCategorySelectionDataSet.Tables[SharedFunctions.GetResourceString("F81004SelectionCategoryDataTable")].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.ColumnName], emptyCategorySelectionDataSet.Tables[SharedFunctions.GetResourceString("F81004SelectionDetailsDataTable")].Columns[this.selectionData.GetSelectionDetails.CategoryIDColumn.ColumnName]);

            this.SelectionGrid.DataSource = emptyCategorySelectionDataSet;

            //// disable the band[1] columns to avoid selection from user
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].CellActivation = Activation.Disabled;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].CellActivation = Activation.Disabled;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].CellActivation = Activation.Disabled;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellActivation = Activation.Disabled;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].CellActivation = Activation.Disabled;
        }

        /// <summary>
        /// Columns the positioning.
        /// </summary>
        private void ColumnPositioning()
        {
            //// visble Position of Band[0]
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryColumn.ColumnName].Header.VisiblePosition = 0;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].Header.VisiblePosition = 1;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].Header.VisiblePosition = 2;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].Header.VisiblePosition = 3;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].Header.VisiblePosition = 4;

            //// visble Position of Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Header.VisiblePosition = 0;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].Header.VisiblePosition = 1;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Header.VisiblePosition = 2;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Header.VisiblePosition = 3;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].Header.VisiblePosition = 4;

            //// hide Band[0] Columns
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.ColumnName].Hidden = true;

            //// hide Band[1] Columns
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionIDColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.EventIDColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CategoryIDColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CategoryColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CatalogIDColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FormulaColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.MultiplyColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.EffectiveDateColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.IsEditColumn.ColumnName].Hidden = true;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Hidden = true;
        }

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            this.SelectionGrid.UpdateMode = UpdateMode.OnCellChange;
            this.SelectionGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;

            //// make Band[1] column header visible false
            this.SelectionGrid.DisplayLayout.Bands[1].ColHeadersVisible = false;
            this.SelectionGrid.DisplayLayout.Bands[0].ColHeadersVisible = false;

            this.SelectionGrid.DisplayLayout.Bands[0].Indentation = 0;
            this.SelectionGrid.DisplayLayout.Bands[1].Indentation = 0;

            this.SelectionGrid.DisplayLayout.Appearance.BorderColor = Color.FromArgb(31, 65, 103);

            //// hide row selector apprance for Band[0]
            this.SelectionGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.False;

            //// show row selector apprance for Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowSelectors = DefaultableBoolean.True;

            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BorderColor = Color.Black;
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BackColor = Color.FromArgb(191, 191, 191);
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.ThemedElementAlpha = Alpha.Default;

            //// rowAppearance for Band[0]
            this.SelectionGrid.DisplayLayout.Bands[0].Override.RowAppearance.BackColor = Color.FromArgb(31, 65, 103);
            this.SelectionGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderColor = Color.FromArgb(31, 65, 103);
            this.SelectionGrid.DisplayLayout.Bands[0].Override.RowAppearance.ForeColor = Color.White;
            this.SelectionGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.SizeInPoints = 10;
            this.SelectionGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.Bold = DefaultableBoolean.True;

            //// rowAppearance for Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowAppearance.BackColor = Color.FromArgb(217, 217, 217);
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowAppearance.ForeColor = Color.Black;
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowAppearance.BorderColor = Color.Black;

            //// alternate row appearance in Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor2 = Color.White;

            ////allows row add with in the Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.FixedAddRowOnBottom;

            //// column sizing as per specification
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryColumn.ColumnName].Width = this.SelectionGrid.Left + 390;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].Width = this.SelectionGrid.Left + 110;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].MaxWidth = this.SelectionGrid.Left + 70;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].MaxWidth = this.SelectionGrid.Left + 99;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].MaxWidth = this.SelectionGrid.Left + 94;

            //// text alignment of Band[0] cells
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            //// text alignment of Band[1] cells
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellAppearance.FontData.Name = SharedFunctions.GetResourceString("CourierNew");
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].CellAppearance.FontData.Name = SharedFunctions.GetResourceString("CourierNew");
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].CellAppearance.ForeColor = Color.FromArgb(37, 37, 255);

            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].CellAppearance.FontData.Name = SharedFunctions.GetResourceString("CourierNew");
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            //// apply the forecolor for the readonly cells as per specification
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].CellAppearance.ForeColor = Color.FromArgb(64, 64, 64);
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].CellAppearance.ForeColor = Color.FromArgb(64, 64, 64);

            //// default values using the InitializeTemplateAddRow event of the UltraGrid.
            //// applies default string "Selection" to the SelectionColumn in the Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].DefaultCellValue = "Selection";
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.ForeColor = Color.FromArgb(166, 166, 166);
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.BackColor = Color.White;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextVAlign = VAlign.Middle;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextHAlign = HAlign.Left;

            this.SelectionGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

            //// row selected appearance changed to white
            this.SelectionGrid.DisplayLayout.Bands[0].Override.SelectedCellAppearance.ForeColor = Color.White;
            this.SelectionGrid.DisplayLayout.Bands[1].Override.SelectedCellAppearance.ForeColor = Color.White;

            this.SelectionGrid.DisplayLayout.Bands[1].Override.RowPreviewAppearance.BackColor = Color.Black;

            //// display style for combo in Band[1] to always
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;

            //// readonly columns for Band[0]
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].CellActivation = Activation.NoEdit;

            //// readonly columns for Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].CellActivation = Activation.AllowEdit;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].CellActivation = Activation.AllowEdit;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].CellActivation = Activation.NoEdit;

            //// apply the format for qty and fee column in Band[1]
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Format = SharedFunctions.GetResourceString("F81004CellFormat");
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].Format = SharedFunctions.GetResourceString("F81004CellFormat");

            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.CategoryColumn.ColumnName].TabStop = false;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.UnitColumn.ColumnName].TabStop = false;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FormulaColumn.ColumnName].TabStop = false;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.QntyColumn.ColumnName].TabStop = false;
            this.SelectionGrid.DisplayLayout.Bands[0].Columns[this.selectionData.ListCategoryHeaderDetails.FeeColumn.ColumnName].TabStop = false;

            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].TabStop = false;
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].TabStop = false;

            //// Change the editable cell appearance
            this.SelectionGrid.DisplayLayout.Override.EditCellAppearance.BackColor = Color.FromArgb(255, 255, 121);
            this.SelectionGrid.DisplayLayout.Override.EditCellAppearance.ForeColor = Color.Black;

            //// Change the selected row appearance
            this.SelectionGrid.DisplayLayout.Bands[1].Override.ActiveRowAppearance.BackColor = Color.FromArgb(0, 0, 133);
            this.SelectionGrid.DisplayLayout.Bands[1].Override.ActiveRowAppearance.ForeColor = Color.White;

            //// Restrict the user to enter max chars
            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].MaxLength = 19;
        }

        /// <summary>
        /// Expands all parent rows.
        /// </summary>
        /// <param name="row">The row.</param>
        private void ExpandAllParentRows(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            this.SelectionGrid.ActiveRow = row;
            this.SelectionGrid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExpandRow, false, false);
            this.SelectionGrid.DisplayLayout.Rows[row.Index].ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.SelectionGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
        }

        /// <summary>
        /// Populates the selection combo items.
        /// </summary>
        /// <param name="row">The row.</param>
        private void PopulateSelectionComboItems(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row.Band.Index.Equals(1))
            {
                int categoryId;
                if (row.ParentRow.Cells[this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.ColumnName].Value != null)
                {
                    int.TryParse(row.ParentRow.Cells[this.selectionData.ListCategoryHeaderDetails.CategoryIDColumn.ColumnName].Value.ToString(), out categoryId);
                    //// initialize the slection combo values
                    this.InitializeSelectionValueLists(categoryId);
                    row.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].ValueList = this.SelectionGrid.DisplayLayout.ValueLists[this.selectionValueListName];
                    row.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                    row.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                }
            }
        }

        /// <summary>
        /// Units the fee cell appearance.
        /// </summary>
        /// <param name="row">The row.</param>
        private void UnitFeeCellAppearance(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row.Band.Index.Equals(1))
            {
                bool multiply;
                decimal unitFee;
                string units = row.Cells[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].Value.ToString();
                decimal.TryParse(row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Value.ToString(), out unitFee);
                bool.TryParse(row.Cells[this.selectionData.GetSelectionDetails.MultiplyColumn.ColumnName].Value.ToString(), out multiply);

                if (multiply)
                {
                    ////row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.False;
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(64, 64, 64);
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextHAlign = HAlign.Right;
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextVAlign = VAlign.Middle;
                }
                else
                {
                    ////row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.True;
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.Cursor = Cursors.Hand;
                    ////row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(37, 37, 255);
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextHAlign = HAlign.Center;
                    row.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextVAlign = VAlign.Middle;
                }

                if (string.IsNullOrEmpty(units.Trim()))
                {
                    row.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Activation = Activation.NoEdit;
                }
                else
                {
                    row.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Activation = Activation.AllowEdit;
                }
            }
        }

        /// <summary>
        /// Initializes the selection value lists.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        private void InitializeSelectionValueLists(int categoryId)
        {
            this.selectionValueListName = System.Guid.NewGuid().ToString();

            if (this.SelectionGrid.DisplayLayout.ValueLists.Exists(this.selectionValueListName))
            {
                return;
            }

            ValueList selectionValueList = this.SelectionGrid.DisplayLayout.ValueLists.Add(this.selectionValueListName);

            //// loads corresponding Table in the Combo
            //// ValueList is Just like a datasource in the combo, 
            //// which holds DisplayMemeber and DisplayValue
            F81004SelectionData.GetSelectionCatalogDetailsDataTable selectionCatalogDetailsTable = new F81004SelectionData.GetSelectionCatalogDetailsDataTable();
            selectionCatalogDetailsTable = this.form81004Control.WorkItem.F81004_GetSelectionCatalogDetails(categoryId);

            if (selectionCatalogDetailsTable.Rows.Count > 0)
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                for (int i = 0; i < selectionCatalogDetailsTable.Rows.Count; i++)
                {
                    selectionValueList.ValueListItems.Add(Convert.ToInt32(selectionCatalogDetailsTable.Rows[i].ItemArray[0].ToString()), selectionCatalogDetailsTable.Rows[i].ItemArray[1].ToString());
                }
            }
        }

        /// <summary>
        /// Sets the height of the smartpart.
        /// </summary>
        private void SetSmartpartHeight()
        {
            int tempChildHeight;
            int tempParentHeight;

            if (this.selectionData.ListCategoryHeaderDetails.Rows.Count > 0)
            {
                this.parentRowCount = this.selectionData.ListCategoryHeaderDetails.Rows.Count;
                this.childRowCount = this.selectionData.GetSelectionDetails.Rows.Count + this.selectionData.ListCategoryHeaderDetails.Rows.Count;

                if (this.childRowHeight != 0)
                {
                    tempChildHeight = this.childRowCount * this.childRowHeight;
                    tempParentHeight = this.parentRowCount * (this.parentRowHeight + 10);
                }
                else
                {
                    tempChildHeight = this.childRowCount * 22;
                    tempParentHeight = this.parentRowCount * (this.parentRowHeight + 10);
                }

                //// To Reduce Extra space in the Bottom
                int tempCalc = this.childRowCount - this.parentRowCount;
                int gridHeight = tempParentHeight + tempChildHeight;

                if (this.cancelClicked)
                {
                    gridHeight = (gridHeight - tempCalc) + 2;
                }
                else
                {
                    switch (this.parentRowCount)
                    {
                        case 1:
                        case 2:
                            gridHeight = (gridHeight - tempCalc) + this.parentRowCount + 2;
                            break;
                        case 3:
                        case 4:
                            gridHeight = (gridHeight - tempCalc) + this.parentRowCount + 5;
                            break;
                        case 5:
                            gridHeight = (gridHeight - tempCalc) + this.parentRowCount + 8;
                            break;
                        case 6:
                            gridHeight = (gridHeight - tempCalc) + this.parentRowCount + 10;
                            break;
                        case 7:
                            gridHeight = (gridHeight - tempCalc) + this.parentRowCount + 12;
                            break;
                        default:
                            gridHeight = (gridHeight - tempCalc) + this.parentRowCount + (this.parentRowCount * 2); // for 7
                            break;
                    }
                }

                this.SelectionGridPanel.Height = gridHeight + this.SummaryPanel.Height - 1;
                this.SelectionGrid.Height = gridHeight;
                this.SummaryPanel.Location = new Point(0, this.SelectionGrid.Height - 3);
                this.SelectionPictureBox.Height = this.SelectionGridPanel.Height;
                this.Height = this.SelectionGridPanel.Height;
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.SelectionGridPanel.Height + 2;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.SelectionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SelectionPictureBox.Height, this.SelectionPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
            else
            {
                this.SelectionGridPanel.Height = 50 + this.SummaryPanel.Height;
                this.SelectionGrid.Height = 50;
                this.SummaryPanel.Location = new Point(0, this.SelectionGrid.Height - 2);
                this.SelectionPictureBox.Height = this.SelectionGridPanel.Height;
                this.Height = this.SelectionGridPanel.Height;
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.SelectionGridPanel.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.SelectionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SelectionPictureBox.Height, this.SelectionPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
        }

        /// <summary>
        /// Populates the selection row details.
        /// </summary>
        /// <param name="cetegoryId">The cetegory id.</param>
        /// <param name="catalogId">The catalog id.</param>
        private void PopulateSelectionRowDetails(int cetegoryId, int catalogId)
        {
            //// clear the existing catalog details
            this.selectionData.GetSelectionCatalogDetails.Clear();

            //// populate the selecction catalog table with details
            this.selectionData.Merge(this.form81004Control.WorkItem.F81004_GetSelectionCatalogDetails(cetegoryId));

            //// find the specific catalogId row
            DataRow[] catalogDetailsRow = this.selectionData.GetSelectionCatalogDetails.Select("CatalogID = " + catalogId);

            if (catalogDetailsRow.Length > 0)
            {
                string units = string.Empty;
                string feeCalc = string.Empty;
                decimal unitFee;
                bool multilply;
                double calculatedFee;

                //// validation for calculated fee
                double maxMoneyValue = (double)Int64.MaxValue;
                //// checks for - money datatype range
                maxMoneyValue = Math.Floor(maxMoneyValue / 10000);

                units = catalogDetailsRow[0][this.selectionData.GetSelectionCatalogDetails.UnitsColumn.ColumnName].ToString();
                decimal.TryParse(catalogDetailsRow[0][this.selectionData.GetSelectionCatalogDetails.UnitFeeColumn.ColumnName].ToString(), out unitFee);
                feeCalc = catalogDetailsRow[0][this.selectionData.GetSelectionCatalogDetails.FeeCalcColumn.ColumnName].ToString();
                bool.TryParse(catalogDetailsRow[0][this.selectionData.GetSelectionCatalogDetails.MultiplyColumn.ColumnName].ToString(), out multilply);

                ////assign the values to active row cells
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.CatalogIDColumn.ColumnName].Value = catalogId;
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName].Value = catalogDetailsRow[0][this.selectionData.GetSelectionCatalogDetails.SelectionColumn.ColumnName];
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitsColumn.ColumnName].Value = units;
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.MultiplyColumn.ColumnName].Value = multilply;

                if (multilply)
                {
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextHAlign = HAlign.Right;
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextVAlign = VAlign.Middle;
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Value = unitFee;
                }
                else
                {
                    //// this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.Cursor = Cursors.Hand;
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextHAlign = HAlign.Center;
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.TextVAlign = VAlign.Middle;
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Value = "formula";
                }

                //// check for existing selection id
                if (string.IsNullOrEmpty(this.activeRow.Cells[this.selectionData.GetSelectionDetails.SelectionIDColumn.ColumnName].OriginalValue.ToString()))
                {
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.SelectionIDColumn.ColumnName].Value = 0;
                }

                this.activeRow.Cells[this.selectionData.GetSelectionDetails.FormulaColumn.ColumnName].Value = feeCalc;
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Value = 0;
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].Value = 0;
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Value = 0;
                this.activeRow.Cells[this.selectionData.GetSelectionDetails.IsEditColumn.ColumnName].Value = true;

                if (!multilply && !string.IsNullOrEmpty(feeCalc))
                {
                    this.cellValueChanged = true;
                    this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Formula = string.Empty;
                    this.SelectionGrid.CalcManager = null;
                    this.SelectionGrid.CalcManager = this.UltraFeeCalcManager;
                    this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Formula = feeCalc;
                    this.UltraFeeCalcManager.ReCalc(-1);
                    double.TryParse(this.activeRow.Cells[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Value.ToString(), out calculatedFee);

                    //// check for the maximum limit for calculated fee with max money value
                    if (calculatedFee > maxMoneyValue)
                    {
                        calculatedFee = 0;
                        this.activeRow.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Value = 0;
                        MessageBox.Show(SharedFunctions.GetResourceString("F81004InvalidFee"), SharedFunctions.GetResourceString("Log"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].Value = calculatedFee;
                    this.cellValueChanged = false;
                }

                //// update the grid data into table
                this.selectionData.GetSelectionDetails.AcceptChanges();

                //// Condition 1: Selection Only - If configured units for the selection is blank(NULL), 
                ////do not allow Qnty input and do not use $/per, Fees are not calculated
                if (!string.IsNullOrEmpty(units.Trim()))
                {
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Activation = Activation.AllowEdit;
                }
                else
                {
                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Activation = Activation.NoEdit;
                }
            }
        }

        /// <summary>
        /// Shows the formula form.
        /// </summary>
        private void ShowFormulaForm()
        {
            this.activeCell = this.SelectionGrid.ActiveCell;
            this.activeRow = this.SelectionGrid.ActiveRow;
            if (this.activeRow != null)
            {
                if (this.activeRow.Band.Index.Equals(1))
                {
                    if (this.activeCell != null)
                    {
                        ////if (this.activeCell.StyleResolved.Equals(Infragistics.Win.UltraWinGrid.ColumnStyle.URL))
                        if (this.activeCell.Appearance.FontData.Underline == DefaultableBoolean.True)
                        {
                            string formula = this.activeRow.Cells[this.selectionData.GetSelectionDetails.FormulaColumn.ColumnName].Value.ToString();
                            F810031 form810031 = new F810031();
                            if (form810031 != null)
                            {
                                form810031.Formula = formula;
                                form810031.ShowDialog();
                            }
                            this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(154, 50, 92);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the fee.
        /// </summary>
        private void CalculateFee()
        {
            this.activeCell = this.SelectionGrid.ActiveCell;
            this.activeRow = this.SelectionGrid.ActiveRow;

            //// validation for calculated fee
            double maxMoneyValue = (double)Int64.MaxValue;
            //// checks for - money datatype range
            maxMoneyValue = Math.Floor(maxMoneyValue / 10000);

            if (this.activeCell != null)
            {
                if (this.activeCell.Column.Header.Caption.Equals(this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName))
                {
                    double vqnty;
                    string feeCalc = string.Empty;
                    double unitFee;
                    bool multilply;
                    double calculatedFee = 0;

                    double.TryParse(this.activeCell.Text, out vqnty);
                    double.TryParse(this.activeRow.Cells[this.selectionData.GetSelectionDetails.UnitFeeColumn.ColumnName].Value.ToString(), out unitFee);
                    feeCalc = this.activeRow.Cells[this.selectionData.GetSelectionDetails.FormulaColumn.ColumnName].Value.ToString();
                    bool.TryParse(this.activeRow.Cells[this.selectionData.GetSelectionDetails.MultiplyColumn.ColumnName].Value.ToString(), out multilply);
                    if (unitFee <= 0 && string.IsNullOrEmpty(feeCalc))
                    {
                        return;
                    }
                    else if (vqnty >= 0)
                    {
                        if (multilply)
                        {
                            this.SelectionGrid.CalcManager = null;
                            this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Formula = string.Empty;
                            calculatedFee = vqnty * unitFee;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(feeCalc))
                            {
                                this.SelectionGrid.CalcManager = null;
                                this.SelectionGrid.CalcManager = this.UltraFeeCalcManager;
                                this.SelectionGrid.DisplayLayout.Bands[1].Columns[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Formula = this.activeRow.Cells[this.selectionData.GetSelectionDetails.FormulaColumn.ColumnName].Value.ToString();
                                this.UltraFeeCalcManager.ReCalc(-1);
                                double.TryParse(this.activeRow.Cells[this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName].Value.ToString(), out calculatedFee);
                            }
                        }
                    }

                    //// check for the maximum limit for calculated fee with max money value
                    if (calculatedFee > maxMoneyValue)
                    {
                        calculatedFee = 0;
                        this.activeRow.Cells[this.selectionData.GetSelectionDetails.VQNTYColumn.ColumnName].Value = 0;
                        MessageBox.Show(SharedFunctions.GetResourceString("F81004InvalidFee"), SharedFunctions.GetResourceString("Log"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.activeRow.Cells[this.selectionData.GetSelectionDetails.FeeColumn.ColumnName].Value = calculatedFee;
                    this.selectionData.GetSelectionDetails.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// Totals the fee calculation.
        /// </summary>
        private void CalculateTotalFee()
        {
            double totalFee;
            double.TryParse(this.selectionData.GetSelectionDetails.Compute("SUM([Fee])", "").ToString(), out totalFee);
            this.TotalFeeTextBox.Text = totalFee.ToString(SharedFunctions.GetResourceString("F81004CellFormat"));
        }

        /// <summary>
        /// Saves the selection items.
        /// </summary>
        private void SaveSelectionItems()
        {
            int returnValue = -1;
            string selectionItemsXml = this.GetSelectionItemsXmlString();
            returnValue = this.form81004Control.WorkItem.F81004_SaveSelectionItems(this.keyId, selectionItemsXml, TerraScanCommon.UserId);
            if (returnValue != -1)
            {
                this.keyId = returnValue;
            }

            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.keyId;
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        /// <summary>
        /// Gets the selection items XML string.
        /// </summary>
        /// <returns>selection items xml string</returns>
        private string GetSelectionItemsXmlString()
        {
            string selectionItemsXml = string.Empty;
            string filterExpr = this.selectionData.GetSelectionDetails.IsEditColumn.ColumnName + "= " + SharedFunctions.GetResourceString("True");
            DataSet selectionDataSet = new DataSet(SharedFunctions.GetResourceString("Root"));
            DataRow[] modifiedRows = this.selectionData.GetSelectionDetails.Select(filterExpr);
            if (modifiedRows.Length > 0)
            {
                foreach (DataRow tempRow in modifiedRows)
                {
                    string unitFee = tempRow[this.selectionData.GetSelectionDetails.UnitFeeColumn].ToString();
                    if (!string.IsNullOrEmpty(unitFee))
                    {
                        if (unitFee.Contains(","))
                        {
                            tempRow[this.selectionData.GetSelectionDetails.UnitFeeColumn] = unitFee.Replace(",", "");
                        }
                    }
                }

                selectionDataSet.Merge(modifiedRows);
                selectionDataSet.Tables[this.selectionData.GetSelectionDetails.TableName].TableName = SharedFunctions.GetResourceString("Table");
                selectionDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionData.GetSelectionDetails.EventIDColumn.ColumnName);
                selectionDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionData.GetSelectionDetails.CategoryIDColumn.ColumnName);
                selectionDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionData.GetSelectionDetails.CategoryColumn.ColumnName);
                selectionDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionData.GetSelectionDetails.SelectionColumn.ColumnName);
                selectionDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionData.GetSelectionDetails.EffectiveDateColumn.ColumnName);
                selectionDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionData.GetSelectionDetails.CalculatedFeeColumn.ColumnName);
                selectionDataSet.Tables[SharedFunctions.GetResourceString("Table")].Columns.Remove(this.selectionData.GetSelectionDetails.IsEditColumn.ColumnName);

                selectionItemsXml = selectionDataSet.GetXml();
            }

            return selectionItemsXml;
        }

        #endregion private methods

        #region Coding Added for the issue 4697 on 15/5/2009 by Malliga
        /// <summary>
        /// Handles the Resize event of the F81004 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F81004_Resize(object sender, EventArgs e)
        {
            this.SetSmartpartHeight();
        }
        #endregion 4697
    }
}
