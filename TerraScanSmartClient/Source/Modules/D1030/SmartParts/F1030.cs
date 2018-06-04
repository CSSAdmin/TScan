//--------------------------------------------------------------------------------------------
// <copyright file="f1030.cs" company="Congruent">
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
// 10 NOV 06	    Suganth Mani       Created
//*********************************************************************************/

namespace D1030
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.UI.Controls;
    using System.Globalization;

    #endregion NameSpace

    /// <summary>
    /// Class for special district definition
    /// </summary>
    [SmartPart]
    public partial class F1030 : BaseSmartPart
    {
        #region Private Member Fields

        /// <summary>
        /// The instance of the operation smartpart
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// The instance of the reportactionsmartpart
        /// </summary>
        private ReportActionSmartPart reportActionSmartPart;

        /// <summary>
        /// Instance of additional operation smartpart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// statusBarSmartPart
        /// </summary>
        private StatusBarSmartPart statusBarSmartPart;

        /// <summary>
        /// F1030 controller
        /// </summary>
        private F1030Controller form1030control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// f1030DistrictDefinitionData
        /// </summary>
        private F1030SpecialDistrictDefinitionData form1030DistrictDefinitionData;

        /// <summary>
        /// bindedDistrictRateDetailsDataTable
        /// </summary>
        private F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsDataTable bindedDistrictRateDetailsDataTable;

        /// <summary>
        /// bindedDistrictDistributionDetailsDataTable
        /// </summary>
        private F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsDataTable bindedDistrictDistributionDetailsDataTable;

        /// <summary>
        /// currentRecordIndex
        /// </summary>
        private int currentRecordIndex;

        /// <summary>
        /// activeRecord
        /// </summary>
        private int activeRecord;

        /// <summary>
        /// the rate itemno selected for delete
        /// </summary>
        private int selectedRateItem;

        /// <summary>
        /// the distribution item selcted for delete
        /// </summary>
        private int selectedDistributionItem;

        /// <summary>
        /// flagCopyDistrict
        /// </summary>
        private bool flagCopyDistrict;

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData commonData;

        /// <summary>
        /// tha actual items count of rategrid
        /// </summary>
        private int rateGridActualRowCount;

        /// <summary>
        /// the actual item count of distributiongrid
        /// </summary>
        private int distributionGridActualRowCount;

        /// <summary>
        /// flag to identify the form is being loading
        /// </summary>
        private bool flagFormLoad = true;

        /// <summary>
        /// flag to identify sort direction
        /// </summary>
        private bool flagRateSortDirection;

        /// <summary>
        /// flag to identify distribution item sort direction
        /// </summary>
        private bool flagDistributionSortDirection;

        /// <summary>
        /// the column no selected at rategrid view
        /// </summary>
        private int rateGridViewSelectColumnNo;

        /// <summary>
        /// the column no selected at distribution grid view
        /// </summary>
        private int distributionGridViewSelectColumnNo;

        /// <summary>
        /// default roll year variable 
        /// </summary>
        private int defaultRollYear;

        /// <summary>
        /// flag to identify row header delete in rate grid
        /// </summary>
        private bool flagRateItemDeleteEnabled;

        /// <summary>
        /// flag to identify row header delete in distribution grid
        /// </summary>
        private bool flagDistributionItemDeleteEnabled;

        /// <summary>
        /// flag to show account form.
        /// </summary>
        private bool flagAccountShow;

        /// <summary>
        /// total record count
        /// </summary>
        private int totalRecordCount;

        /// <summary>
        /// cellValue When Editing
        /// </summary>
        private string cellValueWhenEditing;

       #endregion Private Member Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1030"/> class.
        /// </summary>
        public F1030()
        {
            InitializeComponent();
            this.flagCopyDistrict = false;
            this.form1030DistrictDefinitionData = new F1030SpecialDistrictDefinitionData();
            this.bindedDistrictRateDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsDataTable();
            this.bindedDistrictDistributionDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsDataTable();
            this.commonData = new CommonData();
            this.rateGridActualRowCount = 0;
            this.flagFormLoad = false;
            this.flagRateSortDirection = false;
            this.flagDistributionItemDeleteEnabled = false;
            this.flagRateItemDeleteEnabled = false;
            this.flagAccountShow = false;
            this.cellValueWhenEditing = string.Empty;
            this.rateGridViewSelectColumnNo = 0;
            this.distributionGridViewSelectColumnNo = 0;
            this.totalRecordCount = 0;
            this.defaultRollYear = DateTime.Now.Year;
            this.SetMaxLength();
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Event for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Event for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Event for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Declare the event PageStatusActivated        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        #endregion Event Publication

        #region Field encapsulation

        /// <summary>
        /// Gets or sets the form1030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F1030Controller Form9030control
        {
            get { return this.form1030control; }
            set { this.form1030control = value; }
        }

        #endregion Field encapsulation
        
        #region Event Subscription

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            // TODO : Genralized 
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("KeyID", this.activeRecord);

                // todo
                TerraScanCommon.ShowReport(103010, Report.ReportType.Preview, ht);
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
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus())
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            this.flagFormLoad = true;
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.currentRecordIndex = recordNavigationEntity.RecordIndex;
            this.LoadListInformation();
            this.FetchNavigatedRecord(this.currentRecordIndex);
            this.additionalOperationSmartPart.KeyId = this.activeRecord;
            this.DistrictCopyButton.Enabled = true;
            this.flagFormLoad = false;
        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form1030control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        /// <summary>
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewButton_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
                case "DELETE":
                    this.DeleteButton_Click();
                    break;
            }
        }

        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            try
            {
                object[] optionalParams = e.Data;
                if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.Tag))
                {
                    this.flagFormLoad = true;
                    this.LoadWorkSpaces();
                    this.LoadComboxValue();
                    this.ParentForm.CancelButton = this.operationSmartPart.RetrieveCancelButton;
                    if (!this.PermissionEdit)
                    {
                        this.LockControls();
                    }

                    this.DistrictHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictHeaderPictureBox.Height, this.DistrictHeaderPictureBox.Width, "District", 174, 150, 94);
                    this.RateItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.RateItemPictureBox.Height, this.RateItemPictureBox.Width, "Rates", 28, 81, 128);
                    this.DistributionItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistributionItemPictureBox.Height, this.RateItemPictureBox.Width, "Distribution", 0, 64, 0);
                    this.activeRecord = Convert.ToInt32(optionalParams[0]);
                    this.RefreshDetails(this.activeRecord);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagFormLoad = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events Subscription

        #region Events

        #region general form events

        /// <summary>
        /// Handles the Load event of the F1030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1030_Load(object sender, EventArgs e)
        {
            try
            {
                this.flagFormLoad = true;
                this.LoadWorkSpaces();
                this.LoadComboxValue();
                this.ParentForm.CancelButton = this.operationSmartPart.RetrieveCancelButton;
                if (!this.PermissionEdit)
                {
                    this.LockControls();
                }

                this.DistrictHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictHeaderPictureBox.Height, this.DistrictHeaderPictureBox.Width, "District", 174, 150, 94);
                this.RateItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.RateItemPictureBox.Height, this.RateItemPictureBox.Width, "Rates", 28, 81, 128);
                this.DistributionItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistributionItemPictureBox.Height, this.DistributionItemPictureBox.Width, "Distribution", 0, 64, 0);
                this.RefreshDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the combox value.
        /// </summary>
        private void LoadComboxValue()
        {
            try
            {
                this.commonData.ComboKeyStringDataTable.Clear();
                CommonData.ComboKeyStringDataTableRow comboBoxDataTableRow = this.commonData.ComboKeyStringDataTable.NewComboKeyStringDataTableRow();
                comboBoxDataTableRow.KeyId = "Yes";
                comboBoxDataTableRow.KeyName = "Yes";
                this.commonData.ComboKeyStringDataTable.AddComboKeyStringDataTableRow(comboBoxDataTableRow);
                comboBoxDataTableRow = this.commonData.ComboKeyStringDataTable.NewComboKeyStringDataTableRow();
                comboBoxDataTableRow.KeyId = "No";
                comboBoxDataTableRow.KeyName = "No";
                this.commonData.ComboKeyStringDataTable.AddComboKeyStringDataTableRow(comboBoxDataTableRow);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictCopyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictCopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.flagCopyDistrict = true;
                this.DistrictCopyButton.Enabled = false;
                this.DistrictNumberTextBox.Focus();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.additionalOperationSmartPart.AdditionalOperationCountEnt = new AdditionalOperationCountEntity(0, 0, false);
                this.DistrictNumberTextBox.LockKeyPress = false;
                this.RollYearTextBox.LockKeyPress = false;
                this.MinimumDistrictFeeTextBox.LockKeyPress = false;
                this.DistrictNameTextBox.LockKeyPress = false;
                this.TypeComboBox.Enabled = true;
                this.RateDetailsDataGridView.ReadOnly = false;
                this.DistributionGridView.ReadOnly = false;
                this.flagAccountShow = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SpecialDistrictDefinitionAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictDefinitionAuditlinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowAuditReport("103090", this.activeRecord.ToString());
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Edits the enabled event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditEnabledEvent(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                        this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion general form events

        #region RateDetailsDataGridView Events

        /// <summary>
        /// Handles the CellBeginEdit event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.flagRateItemDeleteEnabled = false;
                if (e.ColumnIndex == 2)
                {
                    if (this.RateDetailsDataGridView[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == "2")
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                // this.EnableEditMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.flagRateItemDeleteEnabled)
                {
                    if (e.KeyValue == 46)
                    {
                        this.flagRateItemDeleteEnabled = false;
                        //// this.RefreshDetails(this.activeRecord);
                        this.bindedDistrictRateDetailsDataTable.Rows[this.rateGridActualRowCount].Delete();
                        this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                        this.form1030control.WorkItem.F1030_DeleteDistrictDefinitionRate(this.selectedRateItem, TerraScanCommon.UserId);
                    }
                }
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// Handles the RowHeaderMouseClick event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.RateDetailsDataGridView.CurrentCell != null)
                {
                    this.flagRateItemDeleteEnabled = true;
                    this.rateGridActualRowCount = e.RowIndex;
                    this.selectedRateItem = Convert.ToInt32(this.bindedDistrictRateDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictRateDetailsDataTable.SARateItemIDColumn]);
                    this.RateDetailsDataGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch
            {
            }
        }
       
        /// <summary>
        /// Handles the CellEndEdit event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.flagRateItemDeleteEnabled = false;
                if (e.RowIndex >= this.RateDetailsDataGridView.NumRowsVisible - 1)
                {
                    if ((e.RowIndex + 1) == this.bindedDistrictRateDetailsDataTable.Rows.Count)
                    {
                        this.rateGridActualRowCount++;
                        this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                        this.RateDetailsScrollBar.Visible = false;
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    if (this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString()))
                    {
                        if (Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].Value) == 2)
                        {                           
                            if (this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].Value.ToString().Trim()))
                            {
                                this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].Value = "";
                            }

                            this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
                        }
                        else
                        {
                            this.RateDetailsDataGridView[e.ColumnIndex + 1, e.RowIndex].ReadOnly = false;
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
        /// Handles the CellValueChanged event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.flagRateItemDeleteEnabled = false;
                    this.EnableEditMode();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.flagRateItemDeleteEnabled = false;
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.flagRateSortDirection = !this.flagRateSortDirection;
                    this.rateGridViewSelectColumnNo = e.ColumnIndex;
                    this.PopulateRateDetailsGridView();
                    if (this.flagRateSortDirection)
                    {
                        this.RateDetailsDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        this.RateDetailsDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.RateDetailsDataGridView.Rows.Count > 0)
            {
                this.RateDetailsDataGridView.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.RateDetailsDataGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        Decimal outDecimal = 0.00M;

                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "0");
                        }

                        if (Decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();

                            if (!tempvalue.Contains("."))
                            {
                                outDecimal = outDecimal / 100;
                            }

                            e.Value = outDecimal;
                        }
                        else
                        {
                            e.Value = "0.0";
                        }

                        e.ParsingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the CellFormatting event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                // Only paint if desired, formattable column

                if (e.ColumnIndex == this.RateDetailsDataGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell 

                    if (e.Value != null && !String.IsNullOrEmpty(this.RateDetailsDataGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString()))
                    {
                        string val = e.Value.ToString();

                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }

                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex.Equals(0))
                {
                    this.RateDetailsDataGridView.Rows[0].ReadOnly = false;
                }

                if (e.RowIndex > 1)
                {
                    if (string.IsNullOrEmpty(this.RateDetailsDataGridView[0, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.RateDetailsDataGridView[1, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.RateDetailsDataGridView[2, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.RateDetailsDataGridView[3, e.RowIndex - 1].Value.ToString().Trim()) && (e.RowIndex > 0))
                    {
                        this.RateDetailsDataGridView.Rows[e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        this.RateDetailsDataGridView.Rows[e.RowIndex].ReadOnly = false;
                    }
                }
            }

        }

        /// <summary>
        /// Handles the Enter event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_Enter(object sender, EventArgs e)
        {
            if (this.RateDetailsDataGridView.OriginalRowCount > 0)
            {
                this.RateDetailsDataGridView.Rows[0].Cells[0].Selected = true;
            }
        }

        /// <summary>
        /// Handles the Leave event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_Leave(object sender, EventArgs e)
        {
            if (this.RateDetailsDataGridView.CurrentCell != null)
            {
                this.RateDetailsDataGridView.CurrentCell.Selected = false;
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the RateDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(this.RateDetailDataGridControl_TextChanged);
            e.Control.Validated += new EventHandler(RateDetailsDataGridViewControl_Validated); 
        }

        /// <summary>
        /// Handles the Validated event of the RateDetailsDataGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailsDataGridViewControl_Validated(object sender, EventArgs e)
        {
            this.RateDetailsDataGridView.EditingControl.TextChanged -= new EventHandler(this.RateDetailDataGridControl_TextChanged);
        }

        /// <summary>
        /// Handles the TextChanged event of the RateDetailDataGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RateDetailDataGridControl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableEditMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion RateDetailsDataGridView Events

        #region DistributionGridView Events

        /// <summary>
        /// Handles the CellBeginEdit event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.cellValueWhenEditing = this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
                // this.EnableEditMode();
                this.flagDistributionItemDeleteEnabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EnableEditMode();
                    this.DistributionGridView.Columns["Account"].ReadOnly = true;
                    this.flagDistributionItemDeleteEnabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.flagDistributionItemDeleteEnabled)
                {
                    if (e.KeyValue == 46)
                    {
                        this.flagDistributionItemDeleteEnabled = false;
                        //// this.RefreshDetails(this.activeRecord);
                        this.bindedDistrictDistributionDetailsDataTable.Rows[this.distributionGridActualRowCount].Delete();
                        this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
                        this.form1030control.WorkItem.F1030_DeleteDistrictDefinitionRate(this.selectedDistributionItem, TerraScanCommon.UserId);
                    }
                }
            }
            catch (SoapException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// Handles the RowHeaderMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.DistributionGridView.CurrentCell != null)
                {
                    this.flagDistributionItemDeleteEnabled = true;
                    this.distributionGridActualRowCount = e.RowIndex;
                    this.selectedDistributionItem = Convert.ToInt32(this.bindedDistrictDistributionDetailsDataTable.Rows[e.RowIndex][this.bindedDistrictDistributionDetailsDataTable.SARateItemIDColumn]);
                    this.DistributionGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch 
            {
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.flagDistributionItemDeleteEnabled = false;
                if (e.RowIndex >= this.DistributionGridView.NumRowsVisible - 1)
                {
                    if ((e.RowIndex + 1) == this.bindedDistrictDistributionDetailsDataTable.Rows.Count)
                    {
                        this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
                        this.DistributionDetailsVscrollBar.Visible = false;
                    }
                }

                if (e.ColumnIndex == 0)
                {
                    if (this.DistributionGridView[1, e.RowIndex].Value != null && string.IsNullOrEmpty(this.DistributionGridView[1, e.RowIndex].Value.ToString()))
                    {
                        Decimal defaultValue = 0;
                        try
                        {
                            defaultValue = (Decimal)this.bindedDistrictDistributionDetailsDataTable.Compute("SUM(" + this.DistributionGridView.Columns[1].DataPropertyName + ")", (this.DistributionGridView.Columns[0] as DataGridViewComboBoxColumn).ValueMember + "=" + this.DistributionGridView[0, e.RowIndex].Value.ToString());
                            defaultValue = 1 - defaultValue;
                        }
                        catch (InvalidCastException invCastExp)
                        {
                            defaultValue = 1;
                        }
                        catch
                        {
                            defaultValue = 0;
                        }

                        if ((defaultValue > 0) && (defaultValue <= 100))
                        {
                            this.DistributionGridView[1, e.RowIndex].Value = defaultValue;
                        }
                        else
                        {
                            this.DistributionGridView[1, e.RowIndex].Value = 0;
                        }
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(this.cellValueWhenEditing))
                        {
                            this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = Convert.ToDecimal(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value) / 100;
                        }
                        else
                        {
                            if (this.cellValueWhenEditing != this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value.ToString())
                            {
                                this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = Convert.ToDecimal(this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value) / 100;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.flagDistributionItemDeleteEnabled = false;
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.flagDistributionSortDirection = !this.flagDistributionSortDirection;
                    this.distributionGridViewSelectColumnNo = e.ColumnIndex;
                    this.PopulateDistributionDetailsGridView();
                    if (this.flagDistributionSortDirection)
                    {
                        this.DistributionGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        this.DistributionGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.flagDistributionItemDeleteEnabled = false;
                for (int i = 0; i < this.DistributionGridView.Rows.Count; i++)
                {
                    TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.DistributionGridView[2, i];
                    imgCell.ImagexLocation = 295;
                    imgCell.ImageyLocation = 3;
                    if (e.RowIndex == i)
                    {
                        imgCell.Image = Properties.Resources.Abutton;
                    }
                    else
                    {
                        if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                        {
                            try
                            {
                                imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.RateDetailsDataGridView[e.ColumnIndex, e.RowIndex].InheritedStyle.BackColor.B));
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                this.DistributionGridView.Refresh();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.DistributionGridView.Rows.Count > 0)
            {
                this.DistributionGridView.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// Handles the CellMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.flagAccountShow)
                {
                    this.DistributionGridView.Columns["Account"].ReadOnly = true;
                    if ((e.ColumnIndex == 2) && (e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                    {
                        if (!this.DistributionGridView.Rows[e.RowIndex].ReadOnly)
                        {
                            if ((e.X >= 295) && (e.X <= (326 - 16)) && (e.Y >= 3) && (e.Y <= (22 - 3)))
                            {

                                bool tempAccountStatus;
                                int accountId = 0;
                                int rollYear;
                                bool flagrollYear;
                                string selectedAccountName = string.Empty;
                                object[] optionalParameter = new object[1];
                                flagrollYear = int.TryParse(this.RollYearTextBox.Text, out rollYear);
                                if (flagrollYear)
                                {
                                    optionalParameter[0] = rollYear;
                                }
                                else
                                {
                                    optionalParameter[0] = this.defaultRollYear;
                                }

                                Form accountSelectionForm = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form1030control.WorkItem);
                                if (accountSelectionForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                                {
                                    if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                                    {
                                        int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out accountId);
                                        ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                                        accountNameDataSet = this.form1030control.WorkItem.GetAccountName(accountId);

                                        if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                        {
                                            tempAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                            selectedAccountName = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                                            this.DistributionGridView[3, e.RowIndex].Value = 0;
                                            if (tempAccountStatus)
                                            {
                                                this.DistributionGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(204, 255, 204);
                                                this.DistributionGridView[3, e.RowIndex].Value = 1;
                                            }
                                            else
                                            {
                                                this.DistributionGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = this.DistributionGridView[0, e.RowIndex].Style.BackColor;
                                                this.DistributionGridView[3, e.RowIndex].Value = 0;
                                            }

                                            this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = selectedAccountName;
                                            this.DistributionGridView[4, e.RowIndex].Value = accountId;

                                            if (e.RowIndex >= this.DistributionGridView.NumRowsVisible - 1)
                                            {
                                                if ((e.RowIndex + 1) == this.bindedDistrictDistributionDetailsDataTable.Rows.Count)
                                                {
                                                    this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
                                                    this.DistributionDetailsVscrollBar.Visible = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }   
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                // Only paint if desired column
                if (e.ColumnIndex == this.DistributionGridView.Columns["Percentage"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell
                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();

                        if (tempvalue.Contains("%"))
                        {
                            tempvalue = tempvalue.Replace("%", "");
                        }

                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "0");
                        }

                        if (Decimal.TryParse(tempvalue, NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();
                        }

                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_Enter(object sender, EventArgs e)
        {
            if (this.DistributionGridView.OriginalRowCount > 0)
            {
                this.DistributionGridView.Rows[0].Cells[0].Selected = true;
            }
        }

        /// <summary>
        /// Handles the Leave event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_Leave(object sender, EventArgs e)
        {
            if (this.DistributionGridView.CurrentCell != null)
            {
                this.DistributionGridView.CurrentCell.Selected = false;
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex.Equals(0))
                {
                    this.DistributionGridView.Rows[0].ReadOnly = false;
                }
                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    if (e.RowIndex > 1)
                    {
                        if (string.IsNullOrEmpty(this.DistributionGridView[0, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[1, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[2, e.RowIndex - 1].Value.ToString().Trim()) && (e.RowIndex > 0))
                        {
                            this.DistributionGridView.Rows[e.RowIndex].ReadOnly = true;
                        }
                        else
                        {
                            this.DistributionGridView.Rows[e.RowIndex].ReadOnly = false;
                        }
                    }
                }
                else
                {
                    if (e.RowIndex >= 1)
                    {
                        if (string.IsNullOrEmpty(this.DistributionGridView[0, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[1, e.RowIndex - 1].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DistributionGridView[2, e.RowIndex - 1].Value.ToString().Trim()) && (e.RowIndex > 0))
                        {
                            this.DistributionGridView.Rows[e.RowIndex].ReadOnly = true;
                        }
                        else
                        {
                            this.DistributionGridView.Rows[e.RowIndex].ReadOnly = false;
                        }
                    }
                    else
                    {
                        if (this.DistributionGridView.Rows.Count >= 2)
                        {
                            this.DistributionGridView.Rows[1].ReadOnly = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged += new EventHandler(this.DistributionGridViewControl_TextChanged);
            e.Control.Validated += new EventHandler(this.DistributionGridViewControl_Validated);
        }

        /// <summary>
        /// Handles the Validated event of the DistributionGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridViewControl_Validated(object sender, EventArgs e)
        {
            this.DistributionGridView.EditingControl.TextChanged -= new EventHandler(this.DistributionGridViewControl_TextChanged);
        }

        /// <summary>
        /// Handles the TextChanged event of the DistributionGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridViewControl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableEditMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion DistributionGridView Events

        #endregion Events

        #region Private Member Methods

        /// <summary>
        /// Refreshes the details.
        /// </summary>
        private void RefreshDetails()
        {
            this.LoadListInformation();
            if (this.form1030DistrictDefinitionData.DistrictDetails.Rows.Count > 0)
            {
                if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count > 0)
                {
                    this.currentRecordIndex = 1;
                    this.FetchNavigatedRecord(this.currentRecordIndex);
                    this.additionalOperationSmartPartWorkspace.Enabled = true;
                }
                else
                {
                    this.ClearSpecialDistrictDetails();
                    this.EnableNewMode();
                    this.DistrictCopyButton.Enabled = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    this.NullRecords = true;
                    this.additionalOperationSmartPartWorkspace.Enabled = false;
                }
            }
            else
            {
                this.currentRecordIndex = 0;
                this.FetchNavigatedRecord(this.currentRecordIndex);
            }
        }

        /// <summary>
        /// Refreshes the details.
        /// </summary>
        /// <param name="activeRecordItem">The active record item.</param>
        private void RefreshDetails(int activeRecordItem)
        {
            this.LoadListInformation();
            for (int rowindex = 0; rowindex < this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count; rowindex++)
            {
                if (Convert.ToInt32(this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows[rowindex][this.form1030DistrictDefinitionData.ListDistrictDefinitionID.KeyIDColumn]) == activeRecordItem)
                {
                    this.currentRecordIndex = rowindex + 1;
                    this.FetchNavigatedRecord(rowindex + 1);
                }
            }
        }

        /// <summary>
        /// Loads the list information.
        /// </summary>
        private void LoadListInformation()
        {
            try
            {
                foreach (DataTable dataTable in this.form1030DistrictDefinitionData.Tables)
                {
                    dataTable.Rows.Clear();
                }

                this.form1030DistrictDefinitionData = this.form1030control.WorkItem.F1030_ListDistrictDefinitionType();
                this.TypeComboBox.DisplayMember = this.form1030DistrictDefinitionData.DistrictPostingList.PostNameColumn.ColumnName;
                this.TypeComboBox.ValueMember = this.form1030DistrictDefinitionData.DistrictPostingList.PostTypeIDColumn.ColumnName;

                if (this.form1030DistrictDefinitionData.DistrictPostingList.Rows.Count > 0)
                {
                    this.TypeComboBox.DataSource = this.form1030DistrictDefinitionData.DistrictPostingList.Copy();
                    this.TypeComboBox.SelectedValue = 12;
                }

                if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count <= 0)
                {
                    this.DisableOnEmptyRecord(false);
                }
                else
                {
                    this.totalRecordCount = this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count;
                    this.DisableOnEmptyRecord(true);
                }

                string[] formHeaderInformation = new string[2];
                formHeaderInformation[0] = "Special District Definition";
                this.SetFormHeader(this, new DataEventArgs<string[]>(formHeaderInformation));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1030control.WorkItem.Items.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPartWorkSpace.Show((OperationSmartPart)this.form1030control.WorkItem.Items.Get(SmartPartNames.OperationSmartPart));
            }
            else
            {
                this.operationSmartPart = this.form1030control.WorkItem.Items.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
            }

            ////this.operationSmartPartWorkSpace.TabStop = false;

            if (this.form1030control.WorkItem.Items.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.reportActionSmartPartWorkSpace.Show((ReportActionSmartPart)this.form1030control.WorkItem.Items.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.reportActionSmartPart = this.form1030control.WorkItem.Items.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart);
                this.reportActionSmartPartWorkSpace.Show(this.reportActionSmartPart);
            }

            ////this.reportActionSmartPartWorkSpace.TabStop = false;

            if (this.form1030control.WorkItem.Items.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.additionalOperationSmartPartWorkspace.Show((AdditionalOperationSmartPart)this.form1030control.WorkItem.Items.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.additionalOperationSmartPart = this.form1030control.WorkItem.Items.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
                this.additionalOperationSmartPartWorkspace.Show(this.additionalOperationSmartPart);
            }

            ////this.additionalOperationSmartPartWorkspace.TabStop = false;

            if (this.form1030control.WorkItem.Items.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show((FormHeaderSmartPart)this.form1030control.WorkItem.Items.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1030control.WorkItem.Items.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            ////this.formHeaderSmartPartdeckWorkspace.TabStop = false;

            if (this.form1030control.WorkItem.Items.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorDeckWorkspace.Show((RecordNavigatorSmartPart)this.form1030control.WorkItem.Items.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorDeckWorkspace.Show(this.form1030control.WorkItem.Items.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            ////this.RecordNavigatorDeckWorkspace.TabStop = false;

            if (this.form1030control.WorkItem.Items.Contains(SmartPartNames.StatusBarSmartPart))
            {
                this.StatusBarDeckWorkspace.Show((StatusBarSmartPart)this.form1030control.WorkItem.Items.Get(SmartPartNames.StatusBarSmartPart));
            }
            else
            {
                this.statusBarSmartPart = this.form1030control.WorkItem.Items.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart);
                this.StatusBarDeckWorkspace.Show(this.statusBarSmartPart);
            }

            ////this.StatusBarDeckWorkspace.TabStop = false;

            ////foreach (UserControl ctrl in this.operationSmartPartWorkSpace.SmartParts )
            ////{
            ////    if (ctrl != null)
            ////    {
            ////        ctrl.TabStop = false;
            ////    }
            ////}
            ////foreach (UserControl ctrl in this.reportActionSmartPartWorkSpace.SmartParts)
            ////{
            ////    if (ctrl != null)
            ////    {
            ////        ctrl.TabStop = false;
            ////    }
            ////}
            ////foreach (UserControl ctrl in this.additionalOperationSmartPartWorkspace.SmartParts)
            ////{
            ////    if (ctrl != null)
            ////    {
            ////        ctrl.TabStop = false;
            ////    }
            ////}
            ////foreach (UserControl ctrl in this.formHeaderSmartPartdeckWorkspace.SmartParts)
            ////{
            ////    if (ctrl != null)
            ////    {
            ////        ctrl.TabStop = false;
            ////    }
            ////}
            ////foreach (UserControl ctrl in this.RecordNavigatorDeckWorkspace.SmartParts)
            ////{
            ////    if (ctrl != null)
            ////    {
            ////        ctrl.TabStop = false;
            ////    }
            ////}
            ////foreach (UserControl ctrl in this.StatusBarDeckWorkspace.SmartParts)
            ////{
            ////    if (ctrl != null)
            ////    {
            ////        ctrl.TabStop = false;
            ////    }
            ////}
            
            this.additionalOperationSmartPart.ParentWorkItem = this.form1030control.WorkItem;
            this.statusBarSmartPart.VisibleAutoPrintButton = false;
            this.statusBarSmartPart.VisibleDelinquentButton = false;
            this.statusBarSmartPart.EnableFilteredButton = false;
            this.reportActionSmartPart.DetailsButtonVisible = false;
            this.reportActionSmartPart.PrintButtonVisible = false;
            this.reportActionSmartPart.EmailButtonVisible = false;
            this.reportActionSmartPart.PreviewButtonVisible = true;
        }

        /// <summary>
        /// Fetches the navigated record.
        /// </summary>
        /// <param name="recordIndex">Index of the record.</param>
        private void FetchNavigatedRecord(int recordIndex)
        {
            if ((this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count > 0) && (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count >= recordIndex))
            {
                this.SetActiveRecord(this, new DataEventArgs<int>(recordIndex));
                this.SetRecordCount(this, new DataEventArgs<int>(this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count)); // todo: check for unavailable data
                int[] activeRecordButton = new int[2];
                activeRecordButton[0] = recordIndex;
                activeRecordButton[1] = this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count;
                this.totalRecordCount = activeRecordButton[1];
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(activeRecordButton));
                this.activeRecord = Convert.ToInt32(this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows[recordIndex - 1][this.form1030DistrictDefinitionData.ListDistrictDefinitionID.KeyIDColumn]);
                this.FillDetails(this.activeRecord);
            }
            else
            {
                this.EnableNewMode();
            }

            this.additionalOperationSmartPart.ParentFormId = 1030;
            this.additionalOperationSmartPart.CurrntFormId = 1030;
            this.SetAttachmentCommentsCount();
        }

        /// <summary>
        /// Fills the details.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        private void FillDetails(int keyId)
        {
            try
            {
                this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Clear();
                this.form1030DistrictDefinitionData.GetDistrictRateDetails.Clear();
                this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Clear();
                this.bindedDistrictDistributionDetailsDataTable.Clear();
                this.bindedDistrictRateDetailsDataTable.Clear();
                this.form1030DistrictDefinitionData.Merge(this.form1030control.WorkItem.F1030_GetDistrictDefinitionDetails(keyId), true);
                
                if (this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictColumn].ToString()))
                    {
                        this.DistrictNumberTextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictColumn].ToString();
                    }

                    if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.RollYearColumn].ToString()))
                    {
                        this.defaultRollYear = Convert.ToInt32(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.RollYearColumn]);
                        this.RollYearTextBox.Text = this.defaultRollYear.ToString();
                    }

                    if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.MinimumDistrictFeeColumn].ToString()))
                    {
                        this.MinimumDistrictFeeTextBox.Text = Convert.ToDecimal(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.MinimumDistrictFeeColumn]).ToString("$ #,##0.00");
                    }

                    if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.PostTypeIDColumn].ToString()))
                    {
                        this.TypeComboBox.SelectedValue = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.PostTypeIDColumn];
                    }

                    if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SANameColumn].ToString()))
                    {
                        this.DistrictNameTextBox.Text = this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SANameColumn].ToString();
                    }

                    if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SADistrictIDColumn].ToString()))
                    {
                        this.SpecialDistrictDefinitionAuditlinkLabel.Text = SharedFunctions.GetResourceString("1030AuditLinkText") + this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SADistrictIDColumn].ToString();
                        this.SpecialDistrictDefinitionAuditlinkLabel.Enabled = true;
                    }
                    else
                    {
                        this.SpecialDistrictDefinitionAuditlinkLabel.Text = SharedFunctions.GetResourceString("1030AuditLinkText");
                        this.SpecialDistrictDefinitionAuditlinkLabel.Enabled = false;
                    }

                    if (this.PermissionEdit)
                    {
                        if (!string.IsNullOrEmpty(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictStatementColumn].ToString()))
                        {
                            if (this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.DistrictStatementColumn].ToString() == "1")
                            {
                                this.DistrictNumberTextBox.LockKeyPress = false;
                                this.RollYearTextBox.LockKeyPress = true;
                                this.MinimumDistrictFeeTextBox.LockKeyPress = true;
                                this.DistrictNameTextBox.LockKeyPress = false;
                                this.TypeComboBox.Enabled = false;
                                this.RateDetailsDataGridView.ReadOnly = true;
                                this.DistributionGridView.ReadOnly = true;
                                this.flagAccountShow = false;
                            }
                            else
                            {
                                this.DistrictNumberTextBox.LockKeyPress = false;
                                this.RollYearTextBox.LockKeyPress = false;
                                this.MinimumDistrictFeeTextBox.LockKeyPress = false;
                                this.DistrictNameTextBox.LockKeyPress = false;
                                this.TypeComboBox.Enabled = true;
                                this.RateDetailsDataGridView.ReadOnly = false;
                                this.DistributionGridView.ReadOnly = false;
                                this.flagAccountShow = true;
                            }
                        }
                    }
                }

                this.CustomizeGridView();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>the status of the page</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveSpecialDistrictDefinition();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves the special district definition.
        /// </summary>
        /// <returns>result of save operation</returns>
        private bool SaveSpecialDistrictDefinition()
        {
            bool result = true;
            try
            {
                if (this.ValidateFields())
                {
                    if (string.IsNullOrEmpty(this.MinimumDistrictFeeTextBox.Text.Replace("$", "")))
                    {
                        this.MinimumDistrictFeeTextBox.Text = "0";
                    }

                    if ((Convert.ToDouble(this.MinimumDistrictFeeTextBox.Text.Replace("$", "")) >= Convert.ToDouble("214748.36")) || (Convert.ToDouble(this.DistrictNumberTextBox.Text) >= Convert.ToDouble("2147483647")) || (Convert.ToInt16(this.RollYearTextBox.Text) == 0))
                    {
                        MessageBox.Show("The given input values violates the maximum / minimum data range", "TerraScan – Invalid Field value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.MinimumDistrictFeeTextBox.Focus();
                        return false;
                    }
                    else
                    {
                        F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsDataTable districtDefinitionDetailsDataTable = new F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsDataTable();
                        F1030SpecialDistrictDefinitionData.GetDistrictDefinitionDetailsRow saveDistrictDefinitionDetailsRow;
                        saveDistrictDefinitionDetailsRow = districtDefinitionDetailsDataTable.NewGetDistrictDefinitionDetailsRow();
                        string districtItem = string.Empty, rateItem = string.Empty, distributionItem = string.Empty, primaryKeyID = string.Empty;
                        int districtNo = 0;

                        saveDistrictDefinitionDetailsRow.District = Convert.ToInt32(this.DistrictNumberTextBox.Text);

                        if (string.IsNullOrEmpty(this.MinimumDistrictFeeTextBox.Text))
                        {
                            saveDistrictDefinitionDetailsRow.MinimumDistrictFee = 0;
                        }
                        else
                        {
                            saveDistrictDefinitionDetailsRow.MinimumDistrictFee = Convert.ToDecimal(this.MinimumDistrictFeeTextBox.Text.Replace('$', ' ').Trim());
                        }

                        saveDistrictDefinitionDetailsRow.PostTypeID = Convert.ToInt16(this.TypeComboBox.SelectedValue);
                        saveDistrictDefinitionDetailsRow.RollYear = Convert.ToInt16(this.RollYearTextBox.Text);
                        if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                        {
                            saveDistrictDefinitionDetailsRow.SADistrictID = Convert.ToInt32(this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.Rows[0][this.form1030DistrictDefinitionData.GetDistrictDefinitionDetails.SADistrictIDColumn]);
                        }
                        else
                        {
                            saveDistrictDefinitionDetailsRow.SADistrictID = 0;
                        }

                        saveDistrictDefinitionDetailsRow.SAName = this.DistrictNameTextBox.Text;

                        districtDefinitionDetailsDataTable.Rows.Add(saveDistrictDefinitionDetailsRow);

                        if (this.flagCopyDistrict)
                        {
                            districtNo = 0;
                        }
                        else
                        {
                            districtNo = Convert.ToInt32(this.DistrictNumberTextBox.Text);
                        }

                        // show validation alert if zero item present for 
                        if ((this.bindedDistrictDistributionDetailsDataTable.Rows.Count <= 0) || (this.bindedDistrictRateDetailsDataTable.Rows.Count <= 0))
                        {
                            MessageBox.Show(primaryKeyID, "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DistrictNumberTextBox.Focus();
                            return false;
                        }

                        districtItem = Utility.GetXmlString(districtDefinitionDetailsDataTable);
                        rateItem = Utility.GetXmlString(this.bindedDistrictRateDetailsDataTable);
                        distributionItem = Utility.GetXmlString(this.bindedDistrictDistributionDetailsDataTable);
                        distributionItem = distributionItem.Replace("%", "");
                        primaryKeyID = this.form1030control.WorkItem.F1030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, false, TerraScanCommon.UserId);
                        result = int.TryParse(primaryKeyID, out this.activeRecord);
                        if (result)
                        {
                            this.RefreshDetails(this.activeRecord);
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            this.DistrictCopyButton.Enabled = false;
                        }
                        else
                        {
                            if (primaryKeyID.Contains("There is already an existing record for District"))
                            {
                                if (DialogResult.Yes == MessageBox.Show(primaryKeyID, "TerraScan – Duplicate Special District", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    primaryKeyID = this.form1030control.WorkItem.F1030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, true, TerraScanCommon.UserId);

                                    result = int.TryParse(primaryKeyID, out this.activeRecord);
                                    if (!result)
                                    {
                                        MessageBox.Show(primaryKeyID, "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        this.DistrictNumberTextBox.Focus();
                                    }

                                    this.RefreshDetails(this.activeRecord);
                                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                                    this.SetButtons(TerraScanCommon.ButtonActionMode.OpenMode);
                                    this.DistrictCopyButton.Enabled = false;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show(primaryKeyID, "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DistrictNumberTextBox.Focus();
                            }
                        }

                        return result;
                    }
                 }
                else
                {
                    MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DistrictNumberTextBox.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            return result;
        }

        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButton_Click()
        {
            if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    ////deelte current record
                    if (this.form1030control.WorkItem.F1030_DeleteDistrictDefinition(this.activeRecord, TerraScanCommon.UserId) != 1)
                    {
                        this.activeRecord = 0;
                        this.LoadListInformation();
                        if (this.totalRecordCount > 0)
                        {
                            if (this.totalRecordCount <= this.currentRecordIndex)
                            {
                                if (this.currentRecordIndex == 1)
                                {
                                    this.RefreshDetails();
                                }
                                else
                                {
                                    this.FetchNavigatedRecord(this.currentRecordIndex - 1);
                                    this.currentRecordIndex--;
                                }
                            }
                            else
                            {
                                this.FetchNavigatedRecord(this.currentRecordIndex);
                                this.currentRecordIndex--;
                            }
                        }
                        else
                        {
                            this.RefreshDetails();
                        }

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count > 0)
                        {
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Special District cannot be deleted because Statements \n\n have already been created which reference this record", "TerraScan – Cannot Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.bindedDistrictDistributionDetailsDataTable.Clear();
                this.bindedDistrictRateDetailsDataTable.Clear();
                foreach (DataTable dataTable in this.form1030DistrictDefinitionData.Tables)
                {
                    dataTable.Clear();
                }

                this.RateDetailsScrollBar.Visible = true;
                this.DistributionDetailsVscrollBar.Visible = true;
                             
                if (this.activeRecord != 0)
                {
                    this.RefreshDetails(this.activeRecord);
                }
                else
                {
                    this.RefreshDetails();
                }

                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.activeRecord != 0)
                {
                    this.DistrictCopyButton.Enabled = true;
                    this.flagCopyDistrict = false;
                    if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count > 0)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
                else
                {
                    this.DistrictCopyButton.Enabled = false;
                    this.flagCopyDistrict = true;
                    if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count > 0)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }

                this.SpecialDistrictDefinitionAuditlinkLabel.Enabled = true;
                this.reportActionSmartPart.Enabled = true;

                #region Copydistrict

                #endregion Copydistrict
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            if (this.SaveSpecialDistrictDefinition())
            {
                this.DistrictCopyButton.Enabled = true;
                this.flagCopyDistrict = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
            try
            {
                this.ClearSpecialDistrictDetails();
                this.additionalOperationSmartPart.AdditionalOperationCountEnt = new AdditionalOperationCountEntity(0, 0, false);
                this.DisableOnEmptyRecord(true);
                this.EnableNewMode();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.MinimumDistrictFeeTextBox.Text = 0.ToString("$ #,##0.00");
                this.DistrictNumberTextBox.Focus();
                this.RateDetailsDataGridView[1, 0].Value = 3;
                this.RateDetailsDataGridView[2, 0].Value = "No";
                this.DistrictNumberTextBox.LockKeyPress = false;
                this.RollYearTextBox.LockKeyPress = false;
                this.MinimumDistrictFeeTextBox.LockKeyPress = false;
                this.DistrictNameTextBox.LockKeyPress = false;
                this.TypeComboBox.Enabled = true;
                this.RateDetailsDataGridView.ReadOnly = false;
                this.DistributionGridView.ReadOnly = false;
                this.flagAccountShow = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Clears the special district details.
        /// </summary>
        private void ClearSpecialDistrictDetails()
        {
            this.TypeComboBox.SelectedValue = 12;
            this.DistrictNumberTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = this.defaultRollYear.ToString();
            this.MinimumDistrictFeeTextBox.Text = string.Empty;
            this.DistrictNameTextBox.Text = string.Empty;
            this.bindedDistrictDistributionDetailsDataTable.Clear();
            this.bindedDistrictRateDetailsDataTable.Clear();
        }

        /// <summary>
        /// Enables the new mode.
        /// </summary>
        private void EnableNewMode()
        {
            this.SetGridColumnSettings();
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            int[] activeRecordButton = new int[2];
            activeRecordButton[0] = 0;
            activeRecordButton[1] = 0;
            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(activeRecordButton));
            this.bindedDistrictDistributionDetailsDataTable.Clear();
            this.bindedDistrictRateDetailsDataTable.Clear();
            for (int row = 0; row < this.RateDetailsDataGridView.NumRowsVisible; row++)
            {
                this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
            }

            if (this.form1030DistrictDefinitionData.ListDistrictDefinitionID.Rows.Count <= 0)
            {
                this.RateDetailsDataGridView.AutoGenerateColumns = false;
                this.DistributionGridView.AutoGenerateColumns = false;
                this.RateDetailsDataGridView.DataSource = this.bindedDistrictRateDetailsDataTable;
                this.DistributionGridView.DataSource = this.bindedDistrictDistributionDetailsDataTable;
            }

            this.DistrictCopyButton.Enabled = false;
            this.reportActionSmartPart.Enabled = false;
            this.SpecialDistrictDefinitionAuditlinkLabel.Text = SharedFunctions.GetResourceString("1030AuditLinkText");
            this.SpecialDistrictDefinitionAuditlinkLabel.Enabled = false;
        }

        /// <summary>
        /// Enables the edit mode.
        /// </summary>
        private void EnableEditMode()
        {
            if (this.PermissionEdit && (this.pageMode == TerraScanCommon.PageModeTypes.View))
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.DistrictCopyButton.Enabled = true;
            }
        }

        /// <summary>
        /// This Method used to  Allign The Header width , Column width , And Font Size for 
        /// CommentsDataGridView
        /// </summary>
        private void CustomizeGridView()
        {
            this.SetGridColumnSettings();
            this.PopulateRateDetailsGridView(); 
            this.PopulateDistributionDetailsGridView();
        }

        /// <summary>
        /// Sets the grid column settings.
        /// </summary>
        private void SetGridColumnSettings()
        {
            // RateDetailsDataGrid Properties
            this.RateDetailsDataGridView.AutoGenerateColumns = false;
            this.RateDetailsDataGridView.ApplyStandardBehaviour = true;

            // RateDetailsDataGrid Column Properties
            DataGridViewColumnCollection rateDetailsColumns = this.RateDetailsDataGridView.Columns;
            rateDetailsColumns["RateDescription"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.DescriptionColumn.ColumnName;
            rateDetailsColumns["RateType"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.ItemTypeIDColumn.ColumnName;
            rateDetailsColumns["OneAcreMin"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.HasMinimumColumn.ColumnName;
            rateDetailsColumns["Amount"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.AmountColumn.ColumnName;
            rateDetailsColumns["SARateItemIDRate"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.SARateItemIDColumn.ColumnName;
            rateDetailsColumns["SADistrictIDRate"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictRateDetails.SADistrictIDColumn.ColumnName;
            rateDetailsColumns["RateDescription"].DisplayIndex = 0;
            rateDetailsColumns["RateType"].DisplayIndex = 1;
            rateDetailsColumns["OneAcreMin"].DisplayIndex = 2;
            rateDetailsColumns["Amount"].DisplayIndex = 3;

            // RateDetailsDataGrid RateType Column Properties for data binding
            (this.RateDetailsDataGridView.Columns["RateType"] as DataGridViewComboBoxColumn).DataSource = this.form1030DistrictDefinitionData.DistrictRateList;
            (this.RateDetailsDataGridView.Columns["RateType"] as DataGridViewComboBoxColumn).DisplayMember = this.form1030DistrictDefinitionData.DistrictRateList.ItemTypeColumn.ColumnName;
            (this.RateDetailsDataGridView.Columns["RateType"] as DataGridViewComboBoxColumn).ValueMember = this.form1030DistrictDefinitionData.DistrictRateList.ItemTypeIDColumn.ColumnName;

            // RateDetailsDataGrid OneAcreMin Column Properties for data binding
            (this.RateDetailsDataGridView.Columns["OneAcreMin"] as DataGridViewComboBoxColumn).DataSource = this.commonData.ComboKeyStringDataTable;
            (this.RateDetailsDataGridView.Columns["OneAcreMin"] as DataGridViewComboBoxColumn).DisplayMember = this.commonData.ComboKeyStringDataTable.KeyNameColumn.ColumnName;
            (this.RateDetailsDataGridView.Columns["OneAcreMin"] as DataGridViewComboBoxColumn).ValueMember = this.commonData.ComboKeyStringDataTable.KeyIdColumn.ColumnName;

            DataGridViewColumnCollection distributionDetailsColumns = this.DistributionGridView.Columns;
            this.DistributionGridView.AutoGenerateColumns = false;
            this.DistributionGridView.ApplyStandardBehaviour = true;
            distributionDetailsColumns["DistributionType"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.ItemTypeIDColumn.ColumnName;
            distributionDetailsColumns["Percentage"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.PercentageColumn.ColumnName;
            distributionDetailsColumns["Account"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.AccountColumn.ColumnName;
            distributionDetailsColumns["AccountStatus"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.AccountStatusColumn.ColumnName;
            distributionDetailsColumns["AccountID"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.AccountIDColumn.ColumnName;
            distributionDetailsColumns["SARateItemID"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.SARateItemIDColumn.ColumnName;
            distributionDetailsColumns["SADistrictID"].DataPropertyName = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.SADistrictIDColumn.ColumnName;
            distributionDetailsColumns["DistributionType"].DisplayIndex = 0;
            distributionDetailsColumns["Percentage"].DisplayIndex = 1;
            distributionDetailsColumns["Account"].DisplayIndex = 2;
            (this.DistributionGridView.Columns["DistributionType"] as DataGridViewComboBoxColumn).DataSource = this.form1030DistrictDefinitionData.DistrictDistributionTypeList;
            (this.DistributionGridView.Columns["DistributionType"] as DataGridViewComboBoxColumn).DisplayMember = this.form1030DistrictDefinitionData.DistrictDistributionTypeList.DistributionTypeColumn.ColumnName;
            (this.DistributionGridView.Columns["DistributionType"] as DataGridViewComboBoxColumn).ValueMember = this.form1030DistrictDefinitionData.DistrictDistributionTypeList.ItemTypeIDColumn.ColumnName;
        }

        /// <summary>
        /// Populates the rate details grid view.
        /// </summary>
        private void PopulateRateDetailsGridView()
        {
            try
            {
                // clear the table for repopulating the data
                this.bindedDistrictRateDetailsDataTable.Clear();

                // create form level datatable for ratelist information
                F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow[] tempTable;
                if (this.flagRateSortDirection)
                {
                    tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictRateDetails.Copy().Select("1=1", this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].DataPropertyName + " DESC");
                    this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                }
                else
                {
                    tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictRateDetails.Copy().Select("1=1", this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].DataPropertyName + " ASC");
                    this.RateDetailsDataGridView.Columns[this.rateGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                }

                // add the rows after sorting
                foreach (F1030SpecialDistrictDefinitionData.GetDistrictRateDetailsRow myrow in tempTable)
                {
                    this.bindedDistrictRateDetailsDataTable.ImportRow(myrow);
                }

                // bind the table values to the grid
                this.RateDetailsDataGridView.DataSource = this.bindedDistrictRateDetailsDataTable;

                // add a default empty row for RateDetailsDataGrid
                this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());

                // add necessary empty rows
                int row;
                for (row = this.bindedDistrictRateDetailsDataTable.Rows.Count; row < this.RateDetailsDataGridView.NumRowsVisible; row++)
                {
                    this.bindedDistrictRateDetailsDataTable.AddGetDistrictRateDetailsRow(this.bindedDistrictRateDetailsDataTable.NewGetDistrictRateDetailsRow());
                }

                // set the actual row count for rate details grid
                if (this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows.Count < this.RateDetailsDataGridView.NumRowsVisible)
                {
                    this.rateGridActualRowCount = this.RateDetailsDataGridView.NumRowsVisible - this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows.Count + 1;
                    this.RateDetailsScrollBar.Visible = true;
                }
                else
                {
                    this.rateGridActualRowCount = this.form1030DistrictDefinitionData.GetDistrictRateDetails.Rows.Count + 1;
                    this.RateDetailsScrollBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the distribution details grid view.
        /// </summary>
        private void PopulateDistributionDetailsGridView()
        {
            try
            {
            // clear the table for repopulating the data
            this.bindedDistrictDistributionDetailsDataTable.Clear();

            // create form level datatable for ratelist information
            // this.bindedDistrictDistributionDetailsDataTable = (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsDataTable)this.f1030DistrictDefinitionData.GetDistrictDistributionDetails.Copy();
            F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow[] tempTable;
            if (this.flagDistributionSortDirection)
            {
                tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Copy().Select("1=1", this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].DataPropertyName + " DESC");
                this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            }
            else
            {
                tempTable = (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow[])this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Copy().Select("1=1", this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].DataPropertyName + " ASC");
                this.DistributionGridView.Columns[this.distributionGridViewSelectColumnNo].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }

            // add the rows after sorting
            foreach (F1030SpecialDistrictDefinitionData.GetDistrictDistributionDetailsRow myrow in tempTable)
            {
                this.bindedDistrictDistributionDetailsDataTable.ImportRow(myrow);
            }

            // bind the table values to the grid
            this.DistributionGridView.DataSource = this.bindedDistrictDistributionDetailsDataTable;

            // add a default empty row for DistributionDetailsDataGrid
            this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());

            // add necessary empty rows
            for (int row = this.bindedDistrictDistributionDetailsDataTable.Rows.Count; row < this.DistributionGridView.NumRowsVisible; row++)
            {
                this.bindedDistrictDistributionDetailsDataTable.AddGetDistrictDistributionDetailsRow(this.bindedDistrictDistributionDetailsDataTable.NewGetDistrictDistributionDetailsRow());
            }

            // set the actual row count for distribution details grid
            if (this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows.Count < this.RateDetailsDataGridView.NumRowsVisible)
            {
                this.distributionGridActualRowCount = this.DistributionGridView.NumRowsVisible - this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows.Count + 1;
                this.DistributionDetailsVscrollBar.Visible = true;
            }
            else
            {
                this.distributionGridActualRowCount = this.form1030DistrictDefinitionData.GetDistrictDistributionDetails.Rows.Count + 1;
                this.DistributionDetailsVscrollBar.Visible = false;
            }

            // Set the cell color to lightgreen when account status equals 1
            foreach (DataGridViewRow currentRow in this.DistributionGridView.Rows)
            {
                if (currentRow.Cells["AccountStatus"].Value.ToString() == "1")
                {
                    currentRow.Cells["Account"].Style.BackColor = Color.FromArgb(204, 255, 204);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        }
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                this.additionalOperationSmartPart.KeyId = this.activeRecord;
                additionalOperationCountEntity.AttachmentCount = this.form1030control.WorkItem.GetAttachmentCount(1030, this.activeRecord, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1030control.WorkItem.GetCommentsCount(1030, this.activeRecord, TerraScanCommon.UserId);

                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        private void LockControls()
        {
            this.DistrictNumberTextBox.LockKeyPress = true;
            this.RollYearTextBox.LockKeyPress = true;
            this.MinimumDistrictFeeTextBox.LockKeyPress = true;
            this.DistrictNameTextBox.LockKeyPress = true;
            this.TypeComboBox.Enabled = false;
            this.RateDetailsDataGridView.ReadOnly = true;
            this.DistributionGridView.ReadOnly = true;
        }

        /// <summary>
        /// Disables the on empty record.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        private void DisableOnEmptyRecord(bool enabled)
        {
            this.DistrictDefinitionlPanel.Enabled = enabled;
            this.RateItemPanel.Enabled = enabled;
            this.DistributionPanel.Enabled = enabled;
        }

        /// <summary>
        /// Sets the length of the max.
        /// </summary>
        private void SetMaxLength()
        {
            this.DistrictNumberTextBox.MaxLength = 8;
            this.RollYearTextBox.MaxLength = 4;
            this.DistrictNameTextBox.MaxLength = 50;
        }

        /// <summary>
        /// Validates the fields(returns true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateFields()
        {
            bool validationResult = true;
            if (((string.IsNullOrEmpty(this.DistrictNumberTextBox.Text)) || (string.IsNullOrEmpty(this.RollYearTextBox.Text)) || (string.IsNullOrEmpty(this.DistrictNameTextBox.Text))) || (!this.ValidateRateItems()) || (!this.ValidateDistributionItems()))
            {
                validationResult = false;
                return validationResult;
            }

            return validationResult;
        }

        /// <summary>
        /// Validates the rate items passes true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateRateItems()
        {
            bool validationResult = true;

            bool validTypeId1 = true;

            bool validTypeId2 = true;
                
            for (int currentRow = 0; currentRow < this.RateDetailsDataGridView.Rows.Count; currentRow++)
            {
                if (validationResult)
                {
                    if (!string.IsNullOrEmpty(this.RateDetailsDataGridView[3, currentRow].Value.ToString()) || !string.IsNullOrEmpty(this.RateDetailsDataGridView[1, currentRow].Value.ToString()) || !string.IsNullOrEmpty(this.RateDetailsDataGridView[0, currentRow].Value.ToString()) || !string.IsNullOrEmpty(this.RateDetailsDataGridView[2, currentRow].Value.ToString()))
                    {
                        if (string.IsNullOrEmpty(this.RateDetailsDataGridView[3, currentRow].Value.ToString()) || string.IsNullOrEmpty(this.RateDetailsDataGridView[1, currentRow].Value.ToString()) || string.IsNullOrEmpty(this.RateDetailsDataGridView[0, currentRow].Value.ToString()))
                        {
                            validationResult = false;
                        }

                        if (validTypeId1)
                        {
                            if (this.RateDetailsDataGridView[1, currentRow].Value.ToString() == "2")
                            {
                                validTypeId1 = false;
                            }
                        }

                        if (validTypeId2)
                        {
                            if (this.RateDetailsDataGridView[1, currentRow].Value.ToString() == "3")
                            {
                                validTypeId2 = false;
                            }
                        }
                    }
                }
            }

            validTypeId1 = !validTypeId1;
            validTypeId2 = !validTypeId2;

            return (validationResult && validTypeId1 && validTypeId2);
        }

        /// <summary>
        /// Validates the distribution items passes true on validation success.
        /// </summary>
        /// <returns>validation result</returns>
        private bool ValidateDistributionItems()
        {
            bool validationResult = true;

            bool validTypeId1 = true;

            bool validTypeId2 = true;
            
            for (int currentRow = 0; currentRow < this.DistributionGridView.Rows.Count; currentRow++)
            {
                if (validationResult)
                {
                    if (!string.IsNullOrEmpty(this.DistributionGridView[2, currentRow].Value.ToString()) || !string.IsNullOrEmpty(this.DistributionGridView[1, currentRow].Value.ToString()) || !string.IsNullOrEmpty(this.DistributionGridView[0, currentRow].Value.ToString()))
                    {
                        if (string.IsNullOrEmpty(this.DistributionGridView[2, currentRow].Value.ToString()) || string.IsNullOrEmpty(this.DistributionGridView[2, currentRow].Value.ToString()) || string.IsNullOrEmpty(this.DistributionGridView[0, currentRow].Value.ToString()))
                        {
                            validationResult = false;
                        }

                        if (validTypeId1)
                        {
                            if (this.DistributionGridView[0, currentRow].Value.ToString() == "1")
                            {
                                validTypeId1 = false;
                            }
                        }

                        if (validTypeId2)
                        {
                            if (this.DistributionGridView[0, currentRow].Value.ToString() == "4")
                            {
                                validTypeId2 = false;
                            }
                        }
                    }
                }
            }

            validTypeId1 = !validTypeId1;
            validTypeId2 = !validTypeId2;
            return (validationResult && validTypeId1 && validTypeId2);
        }

        #endregion Private Member Methods
    }
}
