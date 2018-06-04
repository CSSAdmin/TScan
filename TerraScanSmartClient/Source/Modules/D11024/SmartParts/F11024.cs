//--------------------------------------------------------------------------------------------
// <copyright file="F11024.cs" company="Congruent">
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
// 21 Dec 16        Priyadharshini      Modified to bind the Description Field
// 11 Jan 17        Priyadharshini      TSCO 11024 Multiple Journal Entry - Discard items with zero amount
//*********************************************************************************/
namespace D11024
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Resources;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using System.Globalization;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;
    using System.IO;

    
    [SmartPart]
    public partial class F11024 : BaseSmartPart
    {
        
        #region Variables
		
        private F11024Controller form11024Control; 

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        private int JetTemplateID =0;

        /// <summary>
        /// isshift
        /// </summary>
        private bool isshift;

        /// <summary>
        /// iscalleave
        /// </summary>
        private bool iscalleave;

        /// <summary>
        /// iscall
        /// </summary>
        private bool iscall;
        private DateTime receiptDate;
        private string accountValueList = string.Empty;
        private string toAccountValueList = string.Empty;
        private int masterFormNo;
        private string toRollYear = string.Empty;
        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        private UltraGridRow activeRow;

        /// <summary>
        /// instance variable to hold the schedule line grid active cell.
        /// </summary>
        private UltraGridCell activeCell;

        /// <summary>
        /// Minimum filter length from tts_cfg table(TR_AccountListLookup) for auto complete functionality
        /// </summary>
        private int minFilterLength = 3;

        private string isFilteredAccount = string.Empty;
        private bool canDelete = false;
        private bool hasFromAccount = false;
        private string fromRowIndex = string.Empty;

        private bool rollChange = false;

        DataTable dtRollYear = new DataTable();

        F11024MultiplejournalEntryData.f1124_JournalEntryTemplateItemDataTable JournalTemplateTable = new F11024MultiplejournalEntryData.f1124_JournalEntryTemplateItemDataTable();
        F11018MiscReceiptData.AccountListingDataTable filterdData = new F11018MiscReceiptData.AccountListingDataTable();
        F11024MultiplejournalEntryData.f11024_TempFromAccountTableDataTable FromAccountDataTable = new F11024MultiplejournalEntryData.f11024_TempFromAccountTableDataTable();
        F11024MultiplejournalEntryData.f11024_TempToAccountTableDataTable ToAccountDataTable = new F11024MultiplejournalEntryData.f11024_TempToAccountTableDataTable();

	   #endregion

        #region Constructor
        public F11024()
        {
            InitializeComponent();
            this.MultilpejournalEntryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MultilpejournalEntryPictureBox.Height, this.MultilpejournalEntryPictureBox.Width, "Transfers", 28, 81, 128);
            //this.CustomizeGrid();
        }

        private void F11024_Load(object sender, EventArgs e)
        {
            this.CustomizeGrid();
            this.LoadWorkSpaces();
            this.EnableNewMethod();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F15018 control.
        /// </summary>
        /// <value>The F15018 control.</value>
        [CreateNew]
        public F11024Controller Form11024Control
        {
            get { return this.form11024Control as F11024Controller; }
            set { this.form11024Control = value; }
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the grid template details.
        /// </summary>
        private void LoadGridTemplateDetails()
        {            
            this.JournalTemplateTable=this.form11024Control.WorkItem.F11024_GetMultipleJournalTemplateDetails(this.JetTemplateID).f1124_JournalEntryTemplateItem;
            if (this.JournalTemplateTable.Rows.Count > 0)
            {
                this.JournalEntryGrid.DataSource = this.JournalTemplateTable.DefaultView;
            }
        }

        /// <summary>
        /// Enables the new method.
        /// </summary>
        private void EnableNewMethod()
        {
            this.NewButton.Enabled = true;
            this.SaveButton.Enabled = false;
            this.CancelButton.Enabled = false;
            this.JournalEntryGrid.Enabled = false;
            this.JournalEntryGridPanel.Enabled = false;
            this.TransferDatePanel.Enabled = false;
            this.TransferDescriptionPanel.Enabled = false;
            this.FromTemplateButton.Enabled = false;
            this.TransferDateTextBox.BackColor = Color.White;
            this.TransferDescriptionPanel.BackColor = Color.White;
            this.ReceiptDateCalenderButton.Enabled = false;
        }

        /// <summary>
        /// Edits the mode.
        /// </summary>
        private void EditMode()
        {
            this.SaveButton.Enabled = true;
            this.CancelButton.Enabled = true;
            this.NewButton.Enabled = false;
            this.JournalEntryGrid.Enabled = true;
            this.JournalEntryGridPanel.Enabled = true;
            this.TransferDatePanel.Enabled = true;
            this.TransferDescriptionPanel.Enabled = true;
            this.FromTemplateButton.Enabled = true;
            this.ReceiptDateCalenderButton.Enabled = true;
        }
        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            try
            {
                //////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
                if (this.form11024Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.FormHeaderDeckWorkspace.Show(this.form11024Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.FormHeaderDeckWorkspace.Show(this.form11024Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                ////sets form header
                this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Multiple Journal Entry", string.Empty }));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Shows the receipt date calender.
        /// </summary>
        private void ShowReceiptDateCalender()
        {
            this.ReceiptMonthCalender.Visible = true;
            // this.isshift = false;

            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ReceiptMonthCalender.ScrollChange = 1;

            // Set the calendar location.
            //this.ReceiptMonthCalender.Left = this.ReceiptDatePanel.Left + this.ReceiptDateCalenderButton.Left + this.ReceiptDateCalenderButton.Width;
          //  this.ReceiptMonthCalender.Left = this.TransferDatePanel.Left - 50; // +this.ReceiptDateCalenderButton.Right + this.ReceiptDateCalenderButton.Width;
           // this.ReceiptMonthCalender.Top = this.ReceiptDateCalenderButton.Bottom;//this.ReceiptDatePanel.Top + this.ReceiptDateCalenderButton.Top;
            this.ReceiptMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.TransferDateTextBox.Text))
            {
                this.ReceiptMonthCalender.SetDate(this.TransferDateTextBox.DateTextBoxValue);
            }
        }


        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns></returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.TransferDescriptionTextBox.Text.Trim()))
            {
                this.TransferDescriptionTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (string.IsNullOrEmpty(this.TransferDateTextBox.Text.Trim()))
            {
                this.TransferDateTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            this.JournalTemplateTable.AcceptChanges();
            if (this.JournalTemplateTable.Rows.Count == 0)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MultipleJournalEntryTemplateMissingField");
                this.JournalEntryGrid.Focus();
                return sliceValidationFields;
            }
            DataView tempDataView = new DataView(this.JournalTemplateTable, string.Concat(this.JournalTemplateTable.ToAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.FromAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.TransferAmountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.ToAccountIDColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.FromAccountIDColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);

            for (int i = 0; i < this.JournalTemplateTable.Rows.Count; i++)
            {

                if (string.IsNullOrEmpty(this.JournalTemplateTable.Rows[i]["FromAccount"].ToString()) || string.IsNullOrEmpty(this.JournalTemplateTable.Rows[i]["ToAccount"].ToString()) || string.IsNullOrEmpty(this.JournalTemplateTable.Rows[i]["TransferAmount"].ToString()))
                {
                    sliceValidationFields.RequiredFieldMissing = true;
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MultipleJournalEntryTemplateMissingField");
                    this.JournalEntryGrid.Focus();
                    return sliceValidationFields;
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.JournalTemplateTable.Clear();
            this.TransferDateTextBox.Text = string.Empty;
            this.TransferDescriptionTextBox.Text = string.Empty;
            this.JournalEntryGrid.DataSource = this.JournalTemplateTable.DefaultView;
            this.JournalEntryGrid.Layouts.Clear();
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void SetSeletedDate(DateTime selectedDate)
        {
            ////assign date to the receiptDate and textbox
            this.TransferDateTextBox.Text = this.GetNextWorkingDay(selectedDate);
            ////change receiptdatetext box color
            this.ChangeDateBackGround();
            TransferDateTextBox.Focus();
            ////this.ReceiptDateCalenderButton.Focus();
            this.ReceiptMonthCalender.Visible = false;
        }

        /// <summary>
        /// Gets the next working day.
        /// </summary>
        /// <param name="receiptDateTime">The receipt date time.</param>
        /// <returns></returns>
        private string GetNextWorkingDay(DateTime receiptDateTime)
        {
            ////get next day if today else update for default date management
            if (receiptDateTime.Equals(DateTime.Today))
            {
                this.receiptDate = this.form11024Control.WorkItem.F9001_GetNextWorkingDay();
                return this.receiptDate.ToShortDateString();
            }
            else if (String.IsNullOrEmpty(this.TransferDateTextBox.Text.Trim()) && receiptDateTime.Equals(DateTime.MinValue))
            {
                ////check for valid date - if not return the empty value assigned in text box else validated value
                this.receiptDate = DateTime.Now;
                return String.Empty;
            }

            this.receiptDate = receiptDateTime;
            return this.receiptDate.ToShortDateString();
        }
        /// <summary>
        /// Handles the Click event of the ReceiptDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowReceiptDateCalender();
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
        /// Handles the DateSelected event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start);
                TransferDateTextBox.Focus();
                this.iscall = false;
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
        /// Handles the KeyDown event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.ReceiptMonthCalender.SelectionStart);
                }

                this.isshift = e.Shift;
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
        /// Changes the date back ground.
        /// </summary>
        private void ChangeDateBackGround()
        {
            ////change background color to red if date is not today
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) || string.IsNullOrEmpty(this.TransferDateTextBox.Text) || this.TransferDateTextBox.DateTextBoxValue.Equals(System.DateTime.Today))
            {
                this.TransferDateTextBox.Parent.BackColor = Color.White;
                this.TransferDateTextBox.BackColor = Color.White;
            }
            else
            {
                this.TransferDateTextBox.Parent.BackColor = Color.FromArgb(238, 210, 211);
                this.TransferDateTextBox.BackColor = Color.FromArgb(238, 210, 211);
            }
        }


        /// <summary>
        /// Loads to account item.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void LoadToAccountItem(int rowIndex)
        {
            //To Account Combobox
            if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index >= 0
           && this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].ValueList != null)
            {
                this.toAccountValueList = this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].ValueList.ToString();
            }
            else
            {
                this.toAccountValueList = System.Guid.NewGuid().ToString();
            }
            if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.toAccountValueList))
            {
                return;
            }

            if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index.Equals(rowIndex)
                 && this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null && this.JournalEntryGrid.ActiveCell.ValueList.ItemCount > 0)
            {
                // TO DO
                this.toAccountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
            }
            else
            {
                ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.toAccountValueList);

                if (rowIndex < this.JournalTemplateTable.Rows.Count)// && this.flagLoadOnProcess)
                {
                    // ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.accountValueList);
                    if (this.JournalTemplateTable.Rows[rowIndex][this.JournalTemplateTable.ToAccountColumn.ColumnName] != null
                        && !string.IsNullOrEmpty(this.JournalTemplateTable.Rows[rowIndex][this.JournalTemplateTable.ToAccountColumn.ColumnName].ToString().Trim()))
                    {
                        objValueList.ValueListItems.Add((this.JournalTemplateTable.Rows[rowIndex][this.JournalTemplateTable.ToAccountColumn.ColumnName].ToString()));

                    }
                }
            }
        }

        /// <summary>
        /// Loads the account item.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void LoadAccountItem(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index >= 0
                    && this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].ValueList != null)
                {
                    this.accountValueList = this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].ValueList.ToString();
                }
                else
                {
                    this.accountValueList = System.Guid.NewGuid().ToString();
                }

                //    // If the cell already populated with list item
                if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                {
                   // this.JournalEntryGrid.DisplayLayout.ValueLists.Clear();
                    return;
                }

                if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index.Equals(rowIndex)
                       && this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null && this.JournalEntryGrid.ActiveCell.ValueList.ItemCount > 0)
                {
                    // TO DO
                    this.accountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                }
                else
                {
                    ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.accountValueList);

                    if (rowIndex < this.JournalTemplateTable.Rows.Count)// && this.flagLoadOnProcess)
                    {
                        // ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.accountValueList);
                        if (this.JournalTemplateTable.Rows[rowIndex][this.JournalTemplateTable.FromAccountColumn.ColumnName] != null
                            && !string.IsNullOrEmpty(this.JournalTemplateTable.Rows[rowIndex][this.JournalTemplateTable.FromAccountColumn.ColumnName].ToString().Trim()))
                        {
                            objValueList.ValueListItems.Add((this.JournalTemplateTable.Rows[rowIndex][this.JournalTemplateTable.FromAccountColumn.ColumnName].ToString()));
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {

            UltraGridBand currentBand = this.JournalEntryGrid.DisplayLayout.Bands[0];
            currentBand.Override.SelectTypeRow = SelectType.Extended;

            currentBand.Override.RowSelectors = DefaultableBoolean.True;
            this.JournalEntryGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;
            this.JournalEntryGrid.DisplayLayout.TabNavigation = TabNavigation.NextCell;

            currentBand.Columns[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.JournalTemplateTable.DescriptionColumn.ColumnName].Hidden = true;


            currentBand.Columns[this.JournalTemplateTable.TransferAmountColumn.ColumnName].Format = "$#,###0.00";

            currentBand.Columns[this.JournalTemplateTable.FromAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            currentBand.Columns[this.JournalTemplateTable.ToAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;

            currentBand.Columns[this.JournalTemplateTable.TransferAmountColumn.ColumnName].MaxLength = 14;

            if (this.JournalEntryGrid.Rows.Count < 14)
            {
                ////to assgin empty row at the end of the gird
                this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
            else
            {
                this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
            }
            this.JournalEntryGrid.DataSource = this.JournalTemplateTable.DefaultView;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            this.SaveButton.Enabled = true;
            this.CancelButton.Enabled = true;
            this.NewButton.Enabled = false;
        }
        #endregion

        #region Grid Events/Methods


        /// <summary>
        /// Handles the InitializeLayout event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {

                this.LoadAccountItem(e.Row.Index);
                e.Row.Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].ValueList = this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList];
                e.Row.Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.Row.Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                this.LoadToAccountItem(e.Row.Index);

                if (rollChange)
                {
                    // e.Row.Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].ValueList = null;

                    this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList].ValueListItems.Clear();
                    this.rollChange = false;
                }
                
                e.Row.Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].ValueList = this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList];
                e.Row.Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.Row.Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    e.Row.Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                    e.Row.Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                }
                else
                {
                    e.Row.Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    e.Row.Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                this.activeCell = this.JournalEntryGrid.ActiveCell;
                this.activeRow = this.JournalEntryGrid.ActiveRow;
                if (this.activeCell != null && this.activeCell.Value != null)
                {
                    this.SetEditRecord();

                    if (e.Cell.Text != null && e.Cell.Text.Trim().Length.Equals(this.minFilterLength)
                        && activeCell.Column.Key.Equals(this.JournalTemplateTable.FromAccountColumn.ColumnName))
                    {
                        try
                        {
                            e.Cell.Tag = e.Cell.Text.Trim();
                            this.masterFormNo = 11024;
                            filterdData = this.form11024Control.WorkItem.F15018_ListAccountDetails(e.Cell.Text.Trim(), null, masterFormNo).AccountListing;
                            DataView fromDataView = filterdData.DefaultView;
                            FromAccountDataTable.Clear();
                            foreach (DataRow sourcerow in filterdData.Rows)
                            {
                                DataRow destRow = FromAccountDataTable.NewRow();
                                destRow["FromAccountID"] = sourcerow["AccountID"];
                                destRow["FromAccount"] = sourcerow["AccountName"];
                                FromAccountDataTable.Rows.Add(destRow);
                            }

                            DataRow[] filteredRow = FromAccountDataTable.Select();

                            if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                            {
                                this.accountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                            }
                            else
                            {
                                this.accountValueList = System.Guid.NewGuid().ToString();
                            }

                            if (filteredRow.Length > 0)
                            {
                                if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                                {
                                    this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.Clear();
                                    
                                }

                                ValueList objValueList = (ValueList)e.Cell.ValueList;


                                for (int count = 0; count < filteredRow.Length; count++)
                                {

                                    if (filteredRow[count][this.JournalTemplateTable.FromAccountColumn.ColumnName] != null
                                          && !string.IsNullOrEmpty(filteredRow[count][this.JournalTemplateTable.FromAccountIDColumn.ColumnName].ToString().Trim()))
                                    {
                                        objValueList.ValueListItems.Add((filteredRow[count][this.JournalTemplateTable.FromAccountIDColumn.ColumnName].ToString()), filteredRow[count][this.JournalTemplateTable.FromAccountColumn.ColumnName].ToString());
                                    }
                                }
                            }
                            else
                            {
                                //e.Cell.CancelUpdate();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }



                    if (e.Cell.Text != null && e.Cell.Text.Trim().Length.Equals(this.minFilterLength)
                       && activeCell.Column.Key.Equals(this.JournalTemplateTable.ToAccountColumn.ColumnName))
                   {

                        try
                        {
                            //Added to check From Account
                            if (!hasFromAccount)
                            {
                                if (!string.IsNullOrEmpty(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value.ToString()))
                                {
                                    this.hasFromAccount = true;
                                }
                            }

                            //Added contion to perform validation based on From account by purushotham
                            //if (hasFromAccount && !string.IsNullOrEmpty(fromRowIndex) && this.fromRowIndex.Equals(activeRow.Index.ToString()))
                            if (hasFromAccount)
                            {                                
                                e.Cell.Tag = e.Cell.Text.Trim();
                                this.masterFormNo = 11024;
                                int roll = 0;
                                //if (!string.IsNullOrEmpty(toRollYear))
                                //{
                                //    roll = Int32.Parse(toRollYear);
                                //}
                                if (dtRollYear.Rows.Count > 0)
                                {
                                    if (activeRow.Index < this.dtRollYear.Rows.Count)
                                    {
                                        if (!string.IsNullOrEmpty(dtRollYear.Rows[activeRow.Index][1].ToString()))
                                        {
                                            roll = int.Parse(this.dtRollYear.Rows[activeRow.Index][1].ToString());
                                        }
                                    }
                                }
                                if(!string.IsNullOrEmpty(roll.ToString()))
                                {
                                    filterdData = this.form11024Control.WorkItem.F15018_ListAccountDetails(e.Cell.Text.Trim(), roll, masterFormNo).AccountListing;
                                }
                                else
                                {
                                   // filterdData = this.form11024Control.WorkItem.F15018_ListAccountDetails(e.Cell.Text.Trim(), null, masterFormNo).AccountListing;
                                }

                                DataView fromDataView = filterdData.DefaultView;
                                ToAccountDataTable.Clear();
                                foreach (DataRow sourcerow in filterdData.Rows)
                                {
                                    DataRow destRow = ToAccountDataTable.NewRow();
                                    destRow["ToAccountID"] = sourcerow["AccountID"];
                                    destRow["ToAccount"] = sourcerow["AccountName"];
                                    ToAccountDataTable.Rows.Add(destRow);
                                }
                                DataRow[] filteredRow = ToAccountDataTable.Select();

                                if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                                {
                                    this.toAccountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                                }
                                else
                                {
                                    this.toAccountValueList = System.Guid.NewGuid().ToString();
                                }

                                if (filteredRow.Length > 0)
                                {
                                    if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.toAccountValueList))
                                    {
                                        this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList].ValueListItems.Clear();
                                    }

                                    ValueList objValueList = (ValueList)e.Cell.ValueList;


                                    for (int count = 0; count < filteredRow.Length; count++)
                                    {

                                        if (filteredRow[count][this.JournalTemplateTable.ToAccountColumn.ColumnName] != null
                                              && !string.IsNullOrEmpty(filteredRow[count][this.JournalTemplateTable.ToAccountIDColumn.ColumnName].ToString().Trim()))
                                        {
                                            objValueList.ValueListItems.Add((filteredRow[count][this.JournalTemplateTable.ToAccountIDColumn.ColumnName].ToString()), filteredRow[count][this.JournalTemplateTable.ToAccountColumn.ColumnName].ToString());
                                        }
                                    }
                                }
                                this.hasFromAccount = false;
                                this.fromRowIndex = string.Empty;
                            }
                            else
                            {
                                //To dispaly Validation Message
                                if (string.IsNullOrEmpty(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value.ToString()))
                                {
                                    MessageBox.Show("From Account must be selected prior to selecting the To Account", "Terrascan T2 - Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                //else
                                //{
                                //    if (!string.IsNullOrEmpty(fromRowIndex) && !this.fromRowIndex.Equals(activeRow.Index.ToString()))
                                //    {
                                //       // MessageBox.Show("From Account must be selected prior to selecting the To Account", "Terrascan T2 - Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //    }
                                //}
                                
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                this.activeCell = this.JournalEntryGrid.ActiveCell;
                this.activeRow = this.JournalEntryGrid.ActiveRow;

                // Added roll year info in the local datatable "dtRollYear" fror CR21639
                if (dtRollYear.Columns.Count == 0)
                {
                    dtRollYear.Columns.Add("Index", typeof(string));
                    dtRollYear.Columns.Add("FromRollyear", typeof(string));
                }

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.JournalTemplateTable.FromAccountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        bool rollYearChanged = false;
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            var toText = this.activeCell.Text.Trim();
                            //var arrayList = toText.Split('(',')');
                            string[] result = toText.Split(new string[] { "(" }, StringSplitOptions.None);
                            if (result.Length > 0)
                            {
                                foreach (var r in result)
                                {
                                    if (r.Contains(")"))
                                    {
                                        //var str = r.Split(')'); 
                                        var temp = r.Substring(0, 4);
                                        if(!string.IsNullOrEmpty(temp))
                                        {
                                            if (!this.toRollYear.ToString().Equals(temp.ToString()))
                                            {
                                                rollYearChanged = true;
                                                this.toRollYear = temp;
                                            }
                                            else
                                            {
                                                rollYearChanged = false;                                               
                                            }
                                        }                                        
                                    }
                                }
                                
                            }
                            if (dtRollYear.Rows.Count > 0)
                            {
                                bool isAdded = false;
                                for (int i = 0; i < dtRollYear.Rows.Count; i++)
                                {
                                    if (!string.IsNullOrEmpty(dtRollYear.Rows[i][0].ToString()))
                                    {
                                        if (dtRollYear.Rows[i][0].ToString().Equals(activeRow.Index.ToString()))
                                        {
                                            this.dtRollYear.Rows[i][1] = toRollYear;                                        
                                            isAdded = true;
                                            break;
                                        }

                                    }
                                }
                                if (!isAdded)
                                {
                                    dtRollYear.Rows.Add(activeRow.Index.ToString(), toRollYear);
                                    isAdded = true;
                                }
                            }
                            else
                            {
                                dtRollYear.Rows.Add(activeRow.Index.ToString(), this.toRollYear);
                            }
                            this.accountValueList = System.Guid.NewGuid().ToString();

                            //Modifed to clear To Account Details
                            if (rollYearChanged)
                            {
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value = string.Empty;
                                this.rollChange = true;
                                this.JournalEntryGrid.UpdateData();
                                
                            }


                            if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.accountValueList)
                                && this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.SelectedItem == null)
                            {

                                this.activeCell.CancelUpdate();
                                // this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.AccountNameColumn.ColumnName].Value = DBNull.Value;
                                //Modiifed AccountName to ID
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value = string.Empty;

                            }
                            else
                            {
                                int availablestring = -1;
                                if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                                {
                                    this.accountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                                    //modified by purushotham to avoid cancel update when description field returns null value
                                    if (this.activeCell.Text.Trim().Length > 3)
                                    {
                                        availablestring = this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.FindString(this.activeCell.Text.Trim());
                                    }

                                    //  availablestring = this.JournalEntryGrid.ActiveCell.ValueList.FindStringExact(this.activeCell.Text.Trim());
                                }

                                if (availablestring < 0)
                                {
                                    this.activeCell.CancelUpdate();
                                }
                            }
                            //this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                            //this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value = string.Empty;
                        }
                        else
                        {
                            //Modiifed AccountName to ID
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value = string.Empty;
                        }
                    }




                    //ToAccount Details

                    if (activeCell.Column.Key.Equals(this.JournalTemplateTable.ToAccountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            this.toAccountValueList = System.Guid.NewGuid().ToString();
                            if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.toAccountValueList)
                                && this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList].ValueListItems.ValueList.SelectedItem == null)
                            {

                                this.activeCell.CancelUpdate();

                                //Modiifed AccountName to ID
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value = string.Empty;

                            }
                            else
                            {
                                int availablestring = -1;
                                if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                                {
                                    this.toAccountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                                    //modified by purushotham to avoid cancel update when description field returns null value
                                    if (this.activeCell.Text.Trim().Length > 3)
                                    {
                                        availablestring = this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList].ValueListItems.ValueList.FindString(this.activeCell.Text.Trim());
                                    }
                                }

                                if (availablestring < 0)
                                {
                                    this.activeCell.CancelUpdate();
                                }
                            }
                        }
                        else
                        {
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value = string.Empty;
                        }
                    }


                    if (activeCell.Column.Key.Equals(this.JournalTemplateTable.TransferAmountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            decimal convertedValue = 0;
                            decimal.TryParse(this.activeCell.Text.Trim(), out convertedValue);
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.TransferAmountColumn.ColumnName].Value = convertedValue;
                        }
                        else
                        {
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalTemplateTable.TransferAmountColumn.ColumnName].Value = DBNull.Value;
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
        /// Handles the AfterExitEditMode event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                this.activeCell = this.JournalEntryGrid.ActiveCell;
                this.activeRow = this.JournalEntryGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.JournalTemplateTable.FromAccountColumn.ColumnName))
                    {
                        this.fromRowIndex = string.Empty;
                        this.fromRowIndex = activeRow.Index.ToString();
                        //Commented by purushotham to check save
                        //if (this.activeCell.DataChanged) //((int)this.activeCell.OriginalValue != (int)this.activeCell.Value)//
                        //{
                            if (this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value != null
                                && !string.IsNullOrEmpty(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value.ToString()))
                            {
                                //Added by purushotham
                               // this.fromRowIndex = activeRow.Index.ToString();
                                F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();
                                try
                                {
                                    int accountId = int.Parse(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value.ToString());                                
                                    accountNameDataSet = this.form11024Control.WorkItem.F15013_GetAccountName(accountId);
                                }
                                catch (Exception ex)
                                {
                                    int accountId = int.Parse(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Value.ToString());
                                    accountNameDataSet = this.form11024Control.WorkItem.F15013_GetAccountName(accountId);
                                }    

                                if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                {
                                    F15013ExciseTaxRateData.GetAccountNameRow dr = (F15013ExciseTaxRateData.GetAccountNameRow)accountNameDataSet.GetAccountName.Rows[0];
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Value = dr.AccountID;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value = dr.AccountName + " " + dr.Description;
                                                                       
                                }
                                else
                                {
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.DescriptionColumn.ColumnName].Value = string.Empty;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value = string.Empty;
                                }
                                this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value;
                                //this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Value;

                            }
                            else
                            {
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value = string.Empty;
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                            }

                            this.JournalEntryGrid.UpdateData();

                            DataView tempDataView = new DataView(this.JournalTemplateTable, string.Concat(this.JournalTemplateTable.FromAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.TransferAmountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.ToAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                            DataTable valdiateTable = tempDataView.ToTable().Copy();
                            
                       // }
                        if (!string.IsNullOrEmpty(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.FromAccountColumn.ColumnName].Value.ToString()))
                        {
                            this.hasFromAccount = true;
                        }
                    }


                    if (activeCell.Column.Key.Equals(this.JournalTemplateTable.ToAccountColumn.ColumnName))
                    {                       
                        //if (this.activeCell.DataChanged)
                        //{
                        
                        if (this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value != null
                            && !string.IsNullOrEmpty(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value.ToString()))
                        {
                            F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();                            
                            try
                            {
                                int accountId = int.Parse(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value.ToString());                               

                                 accountNameDataSet = this.form11024Control.WorkItem.F15013_GetAccountName(accountId);
                            }
                            catch (Exception ex)
                            {
                                int accountId = int.Parse(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value.ToString());

                                accountNameDataSet = this.form11024Control.WorkItem.F15013_GetAccountName(accountId);
                            }                            
                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                F15013ExciseTaxRateData.GetAccountNameRow dr = (F15013ExciseTaxRateData.GetAccountNameRow)accountNameDataSet.GetAccountName.Rows[0];

                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value = dr.AccountID;
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value = dr.AccountName + " " + dr.Description;
                                                              
                            }
                            else
                            {
                                //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.DescriptionColumn.ColumnName].Value = string.Empty;
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value = string.Empty;
                                //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.AccountStatusColumn.ColumnName].Value = 0;
                            }
                            this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value;
                            //this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value;

                        }
                        else
                        {
                            this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountColumn.ColumnName].Value = string.Empty;
                            this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalTemplateTable.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                        }
                        this.JournalEntryGrid.UpdateData();
                        this.JournalTemplateTable.AcceptChanges();                        

                        DataView tempDataView = new DataView(this.JournalTemplateTable, string.Concat(this.JournalTemplateTable.FromAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.TransferAmountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.ToAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                        DataTable valdiateTable = tempDataView.ToTable().Copy();


                       // }
                    }
                    if (this.activeCell.DataChanged)
                    {
                        this.JournalEntryGrid.UpdateData();
                        DataView tempDataView = new DataView(this.JournalTemplateTable, string.Concat(this.JournalTemplateTable.FromAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.TransferAmountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.ToAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalTemplateTable.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
       
        #region Button events

        /// <summary>
        /// Handles the Click event of the FromTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FromTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string currentId = string.Empty;
                string rollyear = string.Empty;
                Form parcelF1124 = new Form();
                parcelF1124 = this.form11024Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1124, null, this.form11024Control.WorkItem);
                if (parcelF1124 != null)
                {
                    if (parcelF1124.ShowDialog() == DialogResult.OK)
                    {
                        // Added roll year info in the local datatable "dtRollYear" fror Co21639
                        if (dtRollYear.Columns.Count == 0)
                        {
                            dtRollYear.Columns.Add("Index", typeof(string));
                            dtRollYear.Columns.Add("FromRollyear", typeof(string));
                        }
                        currentId = TerraScanCommon.GetValue(parcelF1124, "CommandResult");
                        rollyear = TerraScanCommon.GetValue(parcelF1124, "RollYear");
                        Int32.TryParse(currentId, out this.JetTemplateID);
                        DataTable tempTable = new DataTable();
                        DataTable tempHtrollyear = new DataTable();
                        tempTable = this.JournalTemplateTable.Copy();
                        tempHtrollyear = this.dtRollYear.Copy();
                        this.JournalTemplateTable.Clear();
                        this.dtRollYear.Clear();
                        this.JournalTemplateTable = this.form11024Control.WorkItem.F11024_GetMultipleJournalTemplateDetails(this.JetTemplateID).f1124_JournalEntryTemplateItem;
                        //Added to maintain rollyear data internally purushotham 
                        if (this.JournalTemplateTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < this.JournalTemplateTable.Rows.Count; i++)
                            {
                                this.dtRollYear.Rows.Add(i.ToString(), rollyear);
                            }
                        }
                        if (tempTable.Rows.Count > 0)
                        {
                            var oldCount = tempHtrollyear.Rows.Count;
                            var newCount = dtRollYear.Rows.Count;
                            for (int i = 0; i < tempHtrollyear.Rows.Count; i++)
                            {
                                this.dtRollYear.Rows.Add(newCount.ToString(),tempHtrollyear.Rows[i][1].ToString());
                                //oldCount = oldCount + 1;
                                newCount = newCount + 1;
                            }

                            this.JournalTemplateTable.Merge(tempTable, true);
                        }
                        /* Modified to bind the Description Field */
                        if (this.JournalTemplateTable.Rows.Count > 0)
                        {
                            this.TransferDescriptionTextBox.Text = this.JournalTemplateTable.Rows[0]["Description"].ToString();
                        }


                        this.JournalEntryGrid.DataSource = this.JournalTemplateTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }
        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.ClearControls();
            this.EnableNewMethod();
            this.ChangeDateBackGround();
            this.dtRollYear.Clear();

        }
        /// <summary>
        /// Handles the Click event of the NewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewButton_Click(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.ClearControls();
            this.EditMode();
            DateTime now = DateTime.Now;
            this.TransferDateTextBox.Text = now.ToShortDateString();
            this.TransferDescriptionTextBox.Focus();
            this.dtRollYear.Clear();
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            //TSCO 11024 Multiple Journal Entry - Discard items with zero amount
            try
            {
                int formNum = 11024;
                bool TransferAmountStatus = false;
                var returnValue = this.CheckErrors(formNum);
                if (returnValue.RequiredFieldMissing)
                {
                    MessageBox.Show(returnValue.ErrorMessage, "Terrascan T2 - Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int i = JournalTemplateTable.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToDecimal(JournalTemplateTable.Rows[i]["TransferAmount"]) == 0)
                        {
                            TransferAmountStatus = true;
                            
                        }
                    }
                    if (TransferAmountStatus == true)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("ReceiptItems"), SharedFunctions.GetResourceString("DiscardZero"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            for (int i = JournalTemplateTable.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToDecimal(JournalTemplateTable.Rows[i]["TransferAmount"]) == 0)
                                {
                                    JournalTemplateTable.Rows[i].Delete();
                                }
                            }
                            DataView view = new DataView(JournalTemplateTable);
                            DataTable dtSpecificCols = new DataTable("Table");
                            dtSpecificCols = view.ToTable(false, new string[] { "FromAccountID", "ToAccountID", "TransferAmount" });
                            dtSpecificCols.Namespace = "";
                            DataSet tempDataSet = new DataSet("Root");
                            tempDataSet.Tables.Add(dtSpecificCols);
                            tempDataSet.Tables[0].TableName = "Table";
                            string xmlString = tempDataSet.GetXml();
                            DateTime tempDate;
                            DateTime.TryParse(TransferDateTextBox.Text, out tempDate);
                            string tempdate = tempDate.ToShortDateString();
                            form11024Control.WorkItem.F11024_SaveMultipleJournalTemplate(tempdate, TerraScanCommon.UserId, TransferDescriptionTextBox.Text, xmlString);
                            this.ClearControls();
                            this.EnableNewMethod();
                            this.ChangeDateBackGround();
                        }
                    }
                    else
                    {
                        DataView view = new DataView(JournalTemplateTable);
                        DataTable dtSpecificCols = new DataTable("Table");
                        dtSpecificCols = view.ToTable(false, new string[] { "FromAccountID", "ToAccountID", "TransferAmount" });
                        dtSpecificCols.Namespace = "";
                        DataSet tempDataSet = new DataSet("Root");
                        tempDataSet.Tables.Add(dtSpecificCols);
                        tempDataSet.Tables[0].TableName = "Table";
                        string xmlString = tempDataSet.GetXml();
                        DateTime tempDate;
                        DateTime.TryParse(TransferDateTextBox.Text, out tempDate);
                        string tempdate = tempDate.ToShortDateString();
                        form11024Control.WorkItem.F11024_SaveMultipleJournalTemplate(tempdate, TerraScanCommon.UserId, TransferDescriptionTextBox.Text, xmlString);
                        this.ClearControls();
                        this.EnableNewMethod();
                        this.ChangeDateBackGround();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        private void JournalEntryGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            if (this.canDelete)
            {
                this.canDelete = false;
                e.Cancel = true;
            }
        }

        private void JournalEntryGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                if (JournalEntryGrid.Selected.Rows.Count > 0)
                {
                    this.JournalTemplateTable.AcceptChanges();
                    if (this.JournalTemplateTable.Rows.Count > 0)
                    {
                        this.JournalTemplateTable.AcceptChanges();
                        foreach (UltraGridRow rowSelected in this.JournalEntryGrid.Selected.Rows)
                        {                           
                            var index = rowSelected.Index;
                            if (index < this.JournalTemplateTable.Rows.Count)
                            {
                                this.JournalTemplateTable.AcceptChanges();
                            }
                            else
                            {
                               this.canDelete = true;
                            }
                        }
                    }
                    else
                    {
                        this.canDelete = true;
                    }
                }
            }
        }

        private void JournalEntryGrid_AfterRowsDeleted(object sender, EventArgs e)
        {
            this.JournalTemplateTable.AcceptChanges();
            this.JournalEntryGrid.DataSource = this.JournalTemplateTable.DefaultView;
            if (JournalEntryGrid.Rows.Count < 14)
            {
                this.CustomizeGrid();
            }
        }
        /// <summary>
        /// Handles the Click event of the HelplinkLabel control. Added for TSBG - 11024 Multiple Journal Entry - wrong form number passed to Help SP 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> help Link click the event data.</param>
        private void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    
    }
}
