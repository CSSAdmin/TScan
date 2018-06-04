//--------------------------------------------------------------------------------------------
// <copyright file="F1210.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1210 Disbursement Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-10-2006       Shiva              Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Common.Reports;
    using System.Collections;
    using System.Configuration;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F1210 Disbursement User InterFace
    /// </summary>
    [SmartPart]
    public partial class F1210 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Variable Instance for F1210Controller
        /// </summary>
        private F1210Controller form1210Controll;

        /// <summary>
        /// Variable Holds the disbursementDataset
        /// </summary>
        private DisbursementData disbursementDataset = new DisbursementData();

        /// <summary>
        /// Binding Source
        /// </summary>
        private BindingSource bindingSource;

        /// <summary>
        /// Register Id
        /// </summary>
        private int registerId;

        /// <summary>
        /// AgencyList Row Count
        /// </summary>
        private int agencyListRowCount;

        /// <summary>
        /// Agency ID 
        /// </summary>
        private int agencyId;

        /// <summary>
        /// postDate variable is used to store the postdate. 
        /// </summary>   
        private DateTime postDate;

        /// <summary>
        /// postDateChanged variable is used to find the postdate is changed. 
        /// </summary>   
        private bool postDateChanged;

        /// <summary>
        /// pageLoadStatus variable is used to find the postdate is changed. 
        /// </summary>   
        private bool pageLoadStatus;

        /// <summary>
        /// variable Holds the CurrentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// isShift
        /// </summary>
        private bool isShift;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1210"/> class.
        /// </summary>
        public F1210()
        {
            InitializeComponent();
            this.registerId = -1;
            this.agencyId = -1;
            this.currentRowIndex = -1;
            this.AgencyListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AgencyListPictureBox.Height, this.AgencyListPictureBox.Width, "Agency", 28, 81, 128);
            this.SubFundsListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SubFundsListPictureBox.Height, this.SubFundsListPictureBox.Width, "SubFunds", 174, 150, 94);
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1210 controll.
        /// </summary>
        /// <value>The form1210 controll.</value>
        [CreateNew]
        public F1210Controller Form1210Controll
        {
            get { return this.form1210Controll as F1210Controller; }
            set { this.form1210Controll = value; }
        }

        /// <summary>
        /// Gets or sets the register id.
        /// </summary>
        /// <value>The register id.</value>
        public int RegisterId
        {
            get { return this.registerId; }
            set { this.registerId = value; }
        }

        /// <summary>
        /// Gets or sets the agency id.
        /// </summary>
        /// <value>The agency id.</value>
        public int AgencyId
        {
            get { return this.agencyId; }
            set { this.agencyId = value; }
        }

        /// <summary>
        /// Gets or sets the agency list row count.
        /// </summary>
        /// <value>The agency list row count.</value>
        public int AgencyListRowCount
        {
            get { return this.agencyListRowCount; }
            set { this.agencyListRowCount = value; }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F1210 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1210_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageLoadStatus = true;
                this.LoadWorkSpaces();
                this.CustomizeAgencyListDataGrid();
                this.CustomizeSubFundsDataGrid();
                this.InitAccountNameCombo();
                this.postDate = DateTime.Today.Date;
                this.DisburseThroughTextBox.Text = this.postDate.ToString("MM/dd/yyyy");
                this.PopulateAgencyListDataGrid();
                this.SetTextBoxMaxLength();
                this.AccountNameComboBox.Select();
                this.AccountNameComboBox.Focus();
                this.pageLoadStatus = false;
            }
            catch (SoapException SoapEx)
            {
                ExceptionManager.ManageException(SoapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PrepareChecksButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrepareChecksButton_Click(object sender, EventArgs e)
        {
            string agencyIdXML = string.Empty;
            int returnValue;
            int overrideStatus = 0;
            agencyIdXML = this.GetAgencyIds();
            if (!string.IsNullOrEmpty(agencyIdXML.Trim()) && this.registerId != -1)
            {
                try
                {
                    returnValue = this.Form1210Controll.WorkItem.F1210_SaveDisbursement(this.RegisterId, TerraScanCommon.UserId, this.DisburseThroughTextBox.DateTextBoxValue, agencyIdXML, overrideStatus);
                    if (returnValue > 0)
                    {
                        DialogResult result = MessageBox.Show(SharedFunctions.GetResourceString("F1210PrepareCheckText"), SharedFunctions.GetResourceString("F1210PrepareCheckTitle"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            returnValue = this.Form1210Controll.WorkItem.F1210_SaveDisbursement(this.RegisterId, TerraScanCommon.UserId, this.DisburseThroughTextBox.DateTextBoxValue, agencyIdXML, 1);
                            this.PopulateAgencyListDataGrid();
                        }
                        else if (result == DialogResult.No)
                        {
                            returnValue = this.Form1210Controll.WorkItem.F1210_SaveDisbursement(this.RegisterId, TerraScanCommon.UserId, this.DisburseThroughTextBox.DateTextBoxValue, agencyIdXML, 2);
                            this.PopulateAgencyListDataGrid();
                        }
                        else
                        {
                            this.PopulateAgencyListDataGrid();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Process Completed Successfully", ConfigurationWrapper.ApplicationName + " - Prepare Checks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SoapException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                catch (Exception ex1)
                {
                    ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AgencyListRowCount > 0)
                {
                    for (int i = 0; i < this.AgencyListRowCount; i++)
                    {
                        this.AgencyListingGridView.Rows[i].Cells["AgencyCheck"].Value = "True";
                    }

                    this.AgencyListingGridView.RefreshEdit();
                    this.PrepareChecksButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AgencyIdLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AgencyIdLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11004);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.AgencyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AccountNameComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(this.AccountNameComboBox.SelectedValue) != 0)
                {
                    this.RegisterId = Convert.ToInt32(this.AccountNameComboBox.SelectedValue);
                }
                else
                {
                    this.RegisterId = -1;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the Click event of the UnSelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnSelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AgencyListRowCount > 0)
                {
                    for (int i = 0; i < this.AgencyListRowCount; i++)
                    {
                        this.AgencyListingGridView.Rows[i].Cells["AgencyCheck"].Value = "False";
                    }

                    this.AgencyListingGridView.RefreshEdit();
                    this.PrepareChecksButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Report

        /// <summary>
        /// Handles the Click event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable reportOptionalParameter = new Hashtable();
                ////// calling  Common Function For Report                    
                reportOptionalParameter.Add("UserID", TerraScanCommon.UserId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(121001, Report.ReportType.Preview, reportOptionalParameter);
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

        #endregion

        #region Agency Grid Events

        /// <summary>
        /// Handles the RowEnter event of the AgencyListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AgencyListingGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.agencyListRowCount > 0)
                {
                    if (e.RowIndex >= 0 && this.currentRowIndex != e.RowIndex)
                    {
                        this.agencyId = Convert.ToInt32(this.AgencyListingGridView.Rows[e.RowIndex].Cells["AgencyId"].Value);
                        this.PopulateSubFundsGrid(this.AgencyId);
                        this.PopulateAgencyHeader(this.AgencyId);
                        this.currentRowIndex = e.RowIndex;
                    }
                }
                else
                {
                    this.currentRowIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the AgencyListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AgencyListingGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    this.AgencyListingGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.disbursementDataset.AgencyListing.AcceptChanges();
                    this.DisablePrepareChecksButton();
                }
            }
            catch (DataException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the AgencyListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AgencyListingGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.RowIndex < this.agencyListRowCount)
                {
                    int rowid = -1;
                    rowid = Convert.ToInt32(this.bindingSource.Find(this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName.ToString(), this.AgencyListingGridView.Rows[e.RowIndex].Cells[this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName.ToString()].Value));
                    this.AgencyId = Convert.ToInt32(this.disbursementDataset.AgencyListing.Rows[rowid][this.disbursementDataset.AgencyListing.AgencyIDColumn.ToString()]);
                    this.PopulateSubFundsGrid(this.AgencyId);
                    this.PopulateAgencyHeader(this.AgencyId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the AgencyListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void AgencyListingGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            DateTime outDateTime;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.UndisbursedTotalColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.AgencyListingGridView.Rows[e.RowIndex].Cells[this.disbursementDataset.AgencyListing.UndisbursedTotalColumn.ColumnName.ToString()].Value.ToString().Trim()))
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
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region SubFunds Grid Events

        /// <summary>
        /// Handles the CellFormatting event of the SubFundsListGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void SubFundsListGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.AmountColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.SubFundsListGridView.Rows[e.RowIndex].Cells[this.disbursementDataset.SubFundsList.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
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
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Date Related Calender Controls Events

        /// <summary>
        /// Handles the DateSelected event of the DisbursementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void DisbursementMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start.ToString("MM/dd/yyyy"));
                this.ParentForm.ActiveControl = DisburseThroughTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DisburseThroughButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DisburseThroughButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowDisbursementDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DisbursementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DisbursementMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.DisbursementMonthCalendar.SelectionStart.ToString("MM/dd/yyyy"));
                }               
                isShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            this.DisbursementMonthCalendar.FocusRemovedFrom = false;
            this.DisburseThroughButton.Focus();
            this.DisburseThroughTextBox.Text = dateSelected;
            this.DisburseThroughTextBox_Leave(this.DisburseThroughTextBox, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the Leave event of the DisburseThroughTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DisburseThroughTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.postDateChanged)
                {
                    this.pageLoadStatus = true;
                    if (!DateTime.TryParse(this.DisburseThroughTextBox.Text.Trim(), out this.postDate))
                    {
                        this.postDate = DateTime.Now;
                        this.DisburseThroughTextBox.Text = this.postDate.ToString("MM/dd/yyyy");
                    }

                    if (this.postDate > DateTime.Now)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("PostDateValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.postDate = DateTime.Now;
                        this.DisburseThroughTextBox.Text = this.postDate.ToString("MM/dd/yyyy");
                    }

                    this.pageLoadStatus = false;
                    this.postDateChanged = false;
                    this.PopulateAgencyListDataGrid();
                    
                }
                DisburseThroughTextBox.BackColor = Color.White;
                DisbursementDatePanel.BackColor = Color.White;   
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DisburseThroughTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DisburseThroughTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus)
            {
                this.postDateChanged = true;
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1210Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1210Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1210Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { SharedFunctions.GetResourceString("1210Disbursement"), string.Empty }));
        }

        /// <summary>
        /// Customizes the Agency Id data grid.
        /// </summary>
        private void CustomizeAgencyListDataGrid()
        {
            this.AgencyListingGridView.AllowUserToResizeColumns = false;
            this.AgencyListingGridView.AutoGenerateColumns = false;
            this.AgencyListingGridView.AllowUserToResizeRows = false;
            this.AgencyListingGridView.StandardTab = true;
            this.AgencyListingGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.AgencyListingGridView.Columns[1].Resizable = DataGridViewTriState.False;  
            this.AgencyListingGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.AgencyListingGridView.PrimaryKeyColumnName = this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName;
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.AgencyNameColumn.ColumnName.ToString()].DefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.EntryDateColumn.ColumnName.ToString()].DefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);
            
            this.AgencyListingGridView.Columns["AgencyCheck"].DataPropertyName = "AgencyCheck";
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.AgencyNameColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.AgencyNameColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.EntryDateColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.EntryDateColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.UndisbursedTotalColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.UndisbursedTotalColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.PrimaryContactColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.PrimaryContactColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.AddressColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.AddressColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.ContactEmailColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.ContactEmailColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.CityColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.CityColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.StateColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.StateColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.ZipColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.ZipColumn.ColumnName.ToString();
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.ContactPhoneColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.AgencyListing.ContactPhoneColumn.ColumnName.ToString();

            this.AgencyListingGridView.Columns["AgencyCheck"].Width = 60;
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.AgencyNameColumn.ColumnName.ToString()].Width = 376;
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.EntryDateColumn.ColumnName.ToString()].Width = 145;
            this.AgencyListingGridView.Columns[this.disbursementDataset.AgencyListing.UndisbursedTotalColumn.ColumnName.ToString()].Width = 164;
        }

        /// <summary>
        /// Customizes the SubFunds data grid.
        /// </summary>
        private void CustomizeSubFundsDataGrid()
        {
            this.SubFundsListGridView.AllowUserToResizeColumns = false;
            this.SubFundsListGridView.AutoGenerateColumns = false;
            this.SubFundsListGridView.AllowUserToResizeRows = false;
            this.SubFundsListGridView.StandardTab = true;
            this.SubFundsListGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.SubFundsListGridView.PrimaryKeyColumnName = this.disbursementDataset.SubFundsList.SubFundIDColumn.ColumnName;
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.SubFundColumn.ColumnName.ToString()].DefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.DescriptionColumn.ColumnName.ToString()].DefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.SubFundsListGridView.Columns[0].DataPropertyName = this.disbursementDataset.SubFundsList.AgencyIDColumn.ColumnName.ToString();
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.SubFundIDColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.SubFundsList.SubFundIDColumn.ColumnName.ToString();
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.SubFundColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.SubFundsList.SubFundColumn.ColumnName.ToString();
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.DescriptionColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.SubFundsList.DescriptionColumn.ColumnName.ToString();
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.AmountColumn.ColumnName.ToString()].DataPropertyName = this.disbursementDataset.SubFundsList.AmountColumn.ColumnName.ToString();

            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.SubFundColumn.ColumnName.ToString()].Width = 150;
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.DescriptionColumn.ColumnName.ToString()].Width = 300;
            this.SubFundsListGridView.Columns[this.disbursementDataset.SubFundsList.AmountColumn.ColumnName.ToString()].Width = 200;
        }

        /// <summary>
        /// Populates the agency list data grid.
        /// </summary>
        private void PopulateAgencyListDataGrid()
        {
            this.bindingSource = new BindingSource();
            this.disbursementDataset.AgencyListing.Rows.Clear();
            this.disbursementDataset.SubFundsList.Rows.Clear();
            this.disbursementDataset = this.form1210Controll.WorkItem.F1210_GetDisbursementDetails(this.DisburseThroughTextBox.DateTextBoxValue);
            this.agencyListRowCount = this.disbursementDataset.AgencyListing.Rows.Count;
            this.AddCheckBoxColumn();
            this.AgencyListingGridView.DataSource = this.disbursementDataset.AgencyListing;
            this.bindingSource.DataSource = this.disbursementDataset.AgencyListing.Copy();

            if (this.agencyListRowCount > 0 && this.PermissionFiled.newPermission)
            {
                this.AgencyListingGridView.Enabled = true;
                this.AgencyListingGridView.Rows[0].Selected = true;
                this.agencyId = Convert.ToInt32(this.disbursementDataset.AgencyListing.Rows[0][this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName.ToString()]);
                this.PopulateSubFundsGrid(this.AgencyId);
                this.PopulateAgencyHeader(this.AgencyId);
                this.PrepareChecksButton.Enabled = true;
            }
            else
            {
                this.AgencyListingGridView.CurrentCell = null;
                this.AgencyListingGridView.Rows[0].Selected = false;
                this.AgencyListingGridView.Enabled = false;
                this.PopulateSubFundsGrid(-1);
                this.ClearFields();
                this.PrepareChecksButton.Enabled = false;
            }

            if (this.agencyListRowCount > this.AgencyListingGridView.NumRowsVisible)
            {
                this.AgencyListingGridViewVScrollBar.Visible = false;
            }
            else
            {
                this.AgencyListingGridViewVScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Fills the account name combo.
        /// </summary>
        private void InitAccountNameCombo()
        {
            DisbursementData.ListAccountNameDataTable listAccountNames = new DisbursementData.ListAccountNameDataTable();
            listAccountNames = this.form1210Controll.WorkItem.F1210_DisbursementAccountNames();
            this.AccountNameComboBox.ValueMember = this.disbursementDataset.ListAccountName.RegisterIDColumn.ColumnName;
            this.AccountNameComboBox.DisplayMember = this.disbursementDataset.ListAccountName.AccountNameColumn.ColumnName;
            this.AccountNameComboBox.DataSource = listAccountNames;

            DataRow[] dataRow;
            bool defaultValue = true;
            string findExp = "IsDefault =" + defaultValue.ToString();
            dataRow = listAccountNames.Select(findExp);

            if (dataRow.Length > 0)
            {
                int rowIndex = listAccountNames.Rows.IndexOf(dataRow[0]);
                this.registerId = Convert.ToInt32(dataRow[0][this.disbursementDataset.ListAccountName.RegisterIDColumn.ColumnName.ToString()]);
                this.AccountNameComboBox.SelectedIndex = rowIndex;
            }
            else
            {
                this.RegisterId = -1;
                this.AccountNameComboBox.SelectedIndex = this.RegisterId;
            }
        }

        /// <summary>
        /// Sets the text box max lenght.
        /// </summary>
        private void SetTextBoxMaxLength()
        {
            this.AgencyNameTextBox.MaxLength = this.disbursementDataset.AgencyListing.AgencyNameColumn.MaxLength;
            ////this.TotalUndisbursedFundsTextBox.MaxLength = this.disbursementDataset.AgencyListing.UndisbursedTotalColumn.MaxLength;
            this.PrimaryContactTextBox.MaxLength = this.disbursementDataset.AgencyListing.PrimaryContactColumn.MaxLength;
            this.AddressTextBox.MaxLength = this.disbursementDataset.AgencyListing.AddressColumn.MaxLength;
            this.ContactEmailTextBox.MaxLength = this.disbursementDataset.AgencyListing.ContactEmailColumn.MaxLength;
            this.CityTextBox.MaxLength = this.disbursementDataset.AgencyListing.CityColumn.MaxLength;
            this.StateTextBox.MaxLength = this.disbursementDataset.AgencyListing.StateColumn.MaxLength;
            this.ZipTextBox.MaxLength = this.disbursementDataset.AgencyListing.ZipColumn.MaxLength;
            this.ContactPhoneTextBox.MaxLength = this.disbursementDataset.AgencyListing.ContactPhoneColumn.MaxLength;
        }

        /// <summary>
        /// Populates the sub funds grid.
        /// </summary>
        /// <param name="agencyIdentifier">The agency identifier.</param>
        private void PopulateSubFundsGrid(int agencyIdentifier)
        {
            int subFundsListRowCount;
            DataRow[] matchingSubFunds;
            DataSet subFundsDataSet = new DataSet();
            string findExp = "AgencyID =" + agencyIdentifier.ToString();
            matchingSubFunds = this.disbursementDataset.SubFundsList.Select(findExp);
            if (matchingSubFunds.Length > 0)
            {
                subFundsDataSet.Merge(matchingSubFunds);
                subFundsListRowCount = subFundsDataSet.Tables[0].Rows.Count;
                this.SubFundsListGridView.DataSource = subFundsDataSet.Tables[0];

                if (subFundsListRowCount > 0)
                {
                    this.SubFundsListGridView.Enabled = true;
                    this.SubFundsListGridView.Rows[0].Selected = true;
                }
                else
                {
                    this.SubFundsListGridView.CurrentCell = null;
                    this.SubFundsListGridView.Rows[0].Selected = false;
                    this.SubFundsListGridView.Enabled = false;
                }

                if (subFundsListRowCount > this.SubFundsListGridView.NumRowsVisible)
                {
                    this.SubFundsListVScorrlBar.Visible = false;
                }
                else
                {
                    this.SubFundsListVScorrlBar.Visible = true;
                }
            }
            else
            {
                DataTable tempSubFundsDataTable = new DataTable();
                tempSubFundsDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.disbursementDataset.SubFundsList.AgencyIDColumn.ColumnName), new DataColumn(this.disbursementDataset.SubFundsList.SubFundIDColumn.ColumnName), new DataColumn(this.disbursementDataset.SubFundsList.SubFundColumn.ColumnName), new DataColumn(this.disbursementDataset.SubFundsList.DescriptionColumn.ColumnName), new DataColumn(this.disbursementDataset.SubFundsList.AmountColumn.ColumnName) });
                this.SubFundsListGridView.DataSource = tempSubFundsDataTable;
                this.SubFundsListVScorrlBar.Visible = true;
                this.SubFundsListGridView.CurrentCell = null;
                this.SubFundsListGridView.Enabled = false;
                this.SubFundsListGridView.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.AgencyIdLinkLabel.Text = string.Empty;
            this.AgencyNameTextBox.Text = string.Empty;
            this.TotalUndisbursedFundsTextBox.Text = string.Empty;
            this.PrimaryContactTextBox.Text = string.Empty;
            this.AddressTextBox.Text = string.Empty;
            this.ContactEmailTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.ContactPhoneTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Shows the disbursement date calender.
        /// </summary>
        private void ShowDisbursementDateCalender()
        {
            this.DisbursementMonthCalendar.Visible = true;
            this.DisbursementMonthCalendar.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.DisbursementMonthCalendar.Left = this.AffidavitListPanel.Left + this.DisbursementDatePanel.Left + this.DisburseThroughButton.Left + this.DisburseThroughButton.Width;
            this.DisbursementMonthCalendar.Top = this.AffidavitListPanel.Top + this.DisbursementDatePanel.Top + this.DisburseThroughButton.Top;
            this.DisbursementMonthCalendar.Focus();
            this.DisbursementMonthCalendar.FocusRemovedFrom = true;
            if (!string.IsNullOrEmpty(this.DisburseThroughTextBox.Text))
            {
                this.DisbursementMonthCalendar.SetDate(Convert.ToDateTime(this.DisburseThroughTextBox.Text));
            }
        }

        /// <summary>
        /// Populates the agency header.
        /// </summary>
        /// <param name="agencyIdentifier">The agency identifier.</param>
        private void PopulateAgencyHeader(int agencyIdentifier)
        {
            DataRow[] agencyHeaderFields;
            string findExp = "AgencyID =" + agencyIdentifier.ToString();
            agencyHeaderFields = this.disbursementDataset.AgencyListing.Select(findExp);
            if (agencyHeaderFields.Length > 0)
            {
                this.AgencyIdLinkLabel.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName.ToString()].ToString();
                this.AgencyNameTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.AgencyNameColumn.ColumnName.ToString()].ToString();
                this.TotalUndisbursedFundsTextBox.Text = Convert.ToDecimal(agencyHeaderFields[0][this.disbursementDataset.AgencyListing.UndisbursedTotalColumn.ColumnName.ToString()]).ToString("$ #,##0.00");
                this.PrimaryContactTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.PrimaryContactColumn.ColumnName.ToString()].ToString();
                this.AddressTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.AddressColumn.ColumnName.ToString()].ToString();
                this.ContactEmailTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.ContactEmailColumn.ColumnName.ToString()].ToString();
                this.CityTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.CityColumn.ColumnName.ToString()].ToString();
                this.StateTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.StateColumn.ColumnName.ToString()].ToString();
                this.ZipTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.ZipColumn.ColumnName.ToString()].ToString();
                this.ContactPhoneTextBox.Text = agencyHeaderFields[0][this.disbursementDataset.AgencyListing.ContactPhoneColumn.ColumnName.ToString()].ToString();
            }
        }

        /// <summary>
        /// Adds the check box column.
        /// </summary>
        private void AddCheckBoxColumn()
        {
            this.disbursementDataset.AgencyListing.Columns.Add("AgencyCheck", typeof(bool));
            foreach (DataRow row in this.disbursementDataset.AgencyListing.Rows)
            {
                row["AgencyCheck"] = true;
            }
        }

        /// <summary>
        /// Gets the Agency ids.
        /// </summary>
        /// <returns>XML String as AgencyIds</returns>
        private string GetAgencyIds()
        {
            DataTable tempAgencyIdsDataTable = new DataTable();
            
            string agencyIds = string.Empty;

            foreach (DataColumn column in this.disbursementDataset.AgencyListing.Columns)
            {
                if (column.ColumnName == this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName)
                {
                    tempAgencyIdsDataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            foreach (DataRow dr in this.disbursementDataset.AgencyListing.Rows)
            {
                DataRow tempAgencyIdsDataRow = tempAgencyIdsDataTable.NewRow();

                if (dr["AgencyCheck"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(dr["AgencyCheck"]))
                    {
                        foreach (DataColumn column in this.disbursementDataset.AgencyListing.Columns)
                        {
                            if (column.ColumnName == this.disbursementDataset.AgencyListing.AgencyIDColumn.ColumnName)
                            {
                                tempAgencyIdsDataRow[column.ColumnName] = dr[column.ColumnName];
                            }
                        }

                        tempAgencyIdsDataTable.Rows.Add(tempAgencyIdsDataRow);
                    }
                }
            }

            if (tempAgencyIdsDataTable.Rows.Count > 0)
            {
                agencyIds = TerraScanCommon.GetXmlString(tempAgencyIdsDataTable);
            }

            return agencyIds;
        }

        /// <summary>
        /// Handles the MouseHover event of the ToolTip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToolTip_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Control tempControl = (Control)sender;
                if (!String.IsNullOrEmpty(tempControl.Text))
                {
                    if (tempControl.Text.Length > 20)
                    {
                        this.AccountNameToolTip.RemoveAll();
                        this.AccountNameToolTip.SetToolTip(tempControl, tempControl.Text);
                    }
                    else
                    {
                        this.AccountNameToolTip.RemoveAll();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Disables the prepare checks button.
        /// </summary>
        private void DisablePrepareChecksButton()
        {
            DataRow[] dataRow;
            bool checkedValue = true;
            string findExp = "AgencyCheck =" + checkedValue.ToString();
            dataRow = this.disbursementDataset.AgencyListing.Select(findExp);

            if (dataRow.Length > 0)
            {
                this.PrepareChecksButton.Enabled = true;
            }
            else
            {
                this.PrepareChecksButton.Enabled = false;
            }
        }

        #endregion

        private void DisbursementMonthCalendar_Leave(object sender, EventArgs e)
        {
            if (isShift)
            {
                this.DisburseThroughTextBox.Focus();
            }
            else
            {
                this.DisbursementHelpSmartPart.Focus();
            }
        }
    }
}
