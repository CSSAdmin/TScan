//----------------------------------------------------------------------------------
// <copyright file="F2103.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm. 
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		            Description
// ----------		---------		        ----------------------------------------
// 25 OCT 2013		Purushotham A           Created
//-----------------------------------------------------------------------------------


namespace D21000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
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
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using System.Globalization;

    public partial class F2103 : Form
    {
        
        #region variables

        /// <summary>
        /// DistrictSelectionData instance
        /// </summary>
        private F2103ExemptionSelectionData exemptionSlectionDataset = new F2103ExemptionSelectionData();

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>  
        private bool emptyRecord;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// Created Instance for F1512Controller
        /// </summary>
        private F2103Controller form2103Control;

        /// <summary>
        /// districtID
        /// </summary>
        private int exemptionID;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// For Selected value
        /// </summary>
        private string commandValue;

        private bool nonNumberEntered = false;

        private string ExemptionCode = string.Empty;

        #endregion variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="F2103"/> class.
        /// </summary>
        public F2103()
        {
            InitializeComponent();
            //this.CancelButton = this.ExemptionCancelButton;
            //this.AcceptButton = this.ExemptionSearchButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F2103"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="FormNO">The form NO.</param>
         public F2103(string rollYear, int FormNO,string exemptionCode)
         {
            this.InitializeComponent();
            //this.CancelButton = this.ExemptionCancelButton;
            //this.AcceptButton = this.ExemptionSearchButton;
            if (FormNO != 1024)
            {
                if (!string.IsNullOrEmpty(rollYear))
                {
                    this.RollYearTextBox.Text = rollYear.ToString();
                    this.RollYearTextBox.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                    this.RollYearTextBox.Enabled = true;
                }
            }
            else
            {
                this.RollYearTextBox.Text = rollYear.ToString();
                this.RollYearTextBox.Enabled = true;
            }
             ////Added by purushotham to enable Remove button
            if (exemptionCode.Contains("<<>>"))
            {
               exemptionCode= exemptionCode.Replace("<<>>", "");
            }
            this.ExemptionCode = exemptionCode;
        }
        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region properites

        /// <summary>
        /// Gets or sets the F2101 controll.
        /// </summary>
        /// <value>The F2101 controll.</value>
        [CreateNew]
        public F2103Controller F2103Controll
        {
            get { return this.form2103Control as F2103Controller; }
            set { this.form2103Control = value; }
        }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        /// <value>The district id.</value>
        public int ExemptionId
        {
            get { return this.exemptionID; }
            set { this.exemptionID = value; }
        }

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return commandResult; }
            set { commandResult = value; }
        }

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        /// <value>The command value.</value>
        public string CommandValue
        {
            get { return this.commandValue; }
            set { this.CommandValue = value; }
        }

        #endregion properites


        #region Methods

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>returns GetRowIndex of selected item</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.exemptionSlectionDataset.F2103ExemptionSelection.Rows.Count > 0)
                {
                    if (this.ExemptionDataGridView.SelectedRows.Count > 0)
                    {
                        this.selected = this.ExemptionDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.ExemptionDataGridView.SelectedCells.Count > 0)
                    {
                        this.selected = this.ExemptionDataGridView.CurrentCell.RowIndex;
                    }

                    return this.selected;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.exemptionSlectionDataset.F2103ExemptionSelection.Rows.Count > 5)
            {
                this.ExemptionSelectionVerticalScroll.Enabled = true;
                this.ExemptionSelectionVerticalScroll.Visible = false;
            }
            else
            {
                this.ExemptionSelectionVerticalScroll.Enabled = false;
                this.ExemptionSelectionVerticalScroll.Visible = true;
                this.ExemptionSelectionVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.ExemptionDataGridView.AllowUserToResizeColumns = false;
            this.ExemptionDataGridView.AutoGenerateColumns = false;
            this.ExemptionDataGridView.AllowUserToResizeRows = false;
            this.ExemptionDataGridView.StandardTab = true;
            this.ExemptionDataGridView.EnableBinding = false;

            this.ExemptionDataGridView.Columns[0].DataPropertyName = "ExemptionID";
            this.ExemptionDataGridView.Columns[1].DataPropertyName = "ExemptionCode";
            this.ExemptionDataGridView.Columns[2].DataPropertyName = "Description";
            this.ExemptionDataGridView.Columns[3].DataPropertyName = "Percent";
            this.ExemptionDataGridView.Columns[4].DataPropertyName = "Maximum";
            this.ExemptionDataGridView.Columns[5].DataPropertyName = "Rollyear";
            this.ExemptionDataGridView.PrimaryKeyColumnName = this.ExemptionDataGridView.Columns[0].DataPropertyName;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            //!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim()) ||
            //var percent = .Trim();
            //if (this.PercentTextBox.Text.Trim().Equals("."))
            //{
            //    this.PercentTextBox.Text = string.Empty;
            //}
            if (!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.PercentTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.MaximunTextBox.Text.Trim()) )
            {
                this.ExemptionSearchButton.Enabled = true;
                this.ExemptionClearButton.Enabled = true;
            }
            else
            {
                this.ExemptionSearchButton.Enabled = false;

                if (this.exemptionSlectionDataset.F2103ExemptionSelection.Rows.Count <= 0)
                {
                    this.ExemptionAcceptButton.Enabled = false;
                    this.ExemptionClearButton.Enabled = false;
                }
                else
                {
                    this.ExemptionAcceptButton.Enabled = true;
                    this.ExemptionClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Gets the district details.
        /// </summary>
        private void GetExemptionDetails()
        {
            int rowId = 0;
            rowId = this.GetRowIndex();

            if (!this.emptyRecord)
            {
                if (!string.IsNullOrEmpty(this.ExemptionDataGridView.Rows[rowId].Cells["ExemptionID"].Value.ToString()))
                {
                    int.TryParse(this.ExemptionDataGridView.Rows[rowId].Cells["ExemptionID"].Value.ToString(), out this.exemptionID);

                    this.commandResult = this.exemptionID.ToString();

                    if (!string.IsNullOrEmpty(this.ExemptionDataGridView.Rows[rowId].Cells["Code"].Value.ToString()))
                    {
                        this.commandValue = this.ExemptionDataGridView.Rows[rowId].Cells["Code"].Value.ToString();
                    }
                    else
                    {
                        this.commandValue = string.Empty;
                    }
                    if(!string.IsNullOrEmpty(this.ExemptionDataGridView.Rows[rowId].Cells["Description"].Value.ToString()))
                    {
                        var tempDesc = this.ExemptionDataGridView.Rows[rowId].Cells["Description"].Value.ToString();
                        this.commandValue = commandValue + " - " + tempDesc;
                    }
                    this.CodeTextBox.Focus();
                    //   this.CodeTextBox.Focus();
                }
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            //this.CodeTextBox.Text = string.Empty;
            this.CodeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
            this.MaximunTextBox.Text = string.Empty;
            this.PercentTextBox.Text = string.Empty;
            //Commented not to clear Rollyear by purushotham
            //this.RollYearTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.ExemptionSearchButton.Enabled = false;
            this.ExemptionAcceptButton.Enabled = false;
            this.ExemptionClearButton.Enabled = false;
            this.RemoveButton.Enabled = false;

        }

        /// <summary>
        /// Checks the key up.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CheckKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        break;
                    }

                case Keys.Tab:
                    {
                        break;
                    }

                default:
                    {
                        this.EnableSearchButton();
                        break;
                    }
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F2101 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F2103_Load(object sender, EventArgs e)
        {
            this.emptyRecord = true;
            this.CustomizeDataGrid();
            this.ExemptionDataGridView.DataSource = null;
            this.ExemptionDataGridView.Enabled = false;
            this.ExemptionDataGridView.Rows[0].Selected = false;
            this.ExemptionDataGridView.CurrentCell = null;
            this.DisableVScrollBar();
            ////this.ClearFields();
            this.DisableButtons();
            if(!string.IsNullOrEmpty(ExemptionCode))
            {
                this.RemoveButton.Enabled=true;
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptionAcceptButton_Click(object sender, EventArgs e)
        {
            if (!this.emptyRecord)
            {
                this.GetExemptionDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptionSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ExemptionSearchButton.Enabled)
                {
                    int rollYear = 0;
                    decimal? percent = null;
                    decimal? maximum = null;
                    bool isDecimal = false;
                    bool isPercentDecimal = false;
                    if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                    {
                        int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
                    }
                    if (!string.IsNullOrEmpty(this.PercentTextBox.Text.Trim()))
                    {
                        if (this.PercentTextBox.Text.Trim().Equals("."))
                        {
                            this.PercentTextBox.Text = "0.";
                            isPercentDecimal = true;
                        }
                        // Added by purushotham to check valid decimal
                        if (this.PercentTextBox.Text.Trim().Contains("."))
                        {
                            var count = this.PercentTextBox.Text.Trim().Split('.').Length - 1;
                            if (count <= 1)
                            {
                                percent = Convert.ToDecimal(this.PercentTextBox.Text.Trim(), CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                percent = 0;
                                this.PercentTextBox.Text = "0";
                                this.PercentTextBox.SelectionStart = 1;
                            }
                        }
                        else
                        {
                            percent = Convert.ToDecimal(this.PercentTextBox.Text.Trim(), CultureInfo.InvariantCulture);
                        }
                        if (percent > 100.00m)
                        {
                            this.PercentTextBox.Text = "0";
                            percent = 0.00m;
                            this.PercentTextBox.SelectionStart = 1;

                        }
                    }
                    if (!string.IsNullOrEmpty(this.MaximunTextBox.Text.Trim()))
                    {
                        if (this.MaximunTextBox.Text.Trim().Equals("."))
                        {
                            this.MaximunTextBox.Text = "0.";
                            isDecimal = true;
                        }
                        if (this.MaximunTextBox.Text.Trim().Contains("."))
                        {
                            var count = this.MaximunTextBox.Text.Trim().Split('.').Length - 1;
                            if (count <= 1)
                            {
                                maximum = decimal.Parse(this.MaximunTextBox.Text.Trim(), CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                maximum = 0;
                                this.MaximunTextBox.Text = "0";
                                this.MaximunTextBox.SelectionStart = 1;
                            }
                        }
                        else
                        {
                            maximum = decimal.Parse(this.MaximunTextBox.Text.Trim(), CultureInfo.InvariantCulture);
                        }

                        if (maximum > 922337203685477.5807M)
                        {
                            this.MaximunTextBox.Text = "0";
                            maximum = 0.00m;
                            // this.MaximunTextBox.Focus();
                            this.MaximunTextBox.SelectionStart = 1;
                        }
                    }
                    this.exemptionSlectionDataset = this.form2103Control.WorkItem.f2103_GetExemptionSelection(this.CodeTextBox.Text.Trim(), this.DescriptionTextBox.Text.Trim(), percent / 100, maximum, rollYear);
                    for (int i = 0; i < this.exemptionSlectionDataset.F2103ExemptionSelection.Rows.Count; i++)
                    {
                        decimal tempper = 0;
                        decimal.TryParse(this.exemptionSlectionDataset.F2103ExemptionSelection.Rows[i]["Percent"].ToString(), out tempper);
                        this.exemptionSlectionDataset.F2103ExemptionSelection.Rows[i]["Percent"] = tempper * 100;
                        this.exemptionSlectionDataset.F2103ExemptionSelection.AcceptChanges();
                    }
                    int recordCount = 0;
                    if (isDecimal.Equals(true))
                    {
                        this.MaximunTextBox.Text = ".";
                        this.MaximunTextBox.SelectionStart = 1;
                    }
                    if (isPercentDecimal.Equals(true))
                    {
                        this.PercentTextBox.Text = ".";
                        this.PercentTextBox.SelectionStart = 1;
                    }
                    if (this.exemptionSlectionDataset.F2103ExemptionSelection.Rows.Count > 0)
                    {
                        recordCount = this.exemptionSlectionDataset.F2103ExemptionSelection.Rows.Count;
                        this.ExemptionDataGridView.Enabled = true;
                        this.ExemptionDataGridView.DataSource = this.exemptionSlectionDataset.F2103ExemptionSelection.DefaultView;
                        this.ExemptionDataGridView.Focus();
                        this.ExemptionDataGridView.Rows[0].Selected = true;
                        this.ExemptionAcceptButton.Enabled = true;
                        this.RemoveButton.Enabled = false;
                        ////this.ExemptionDataGridView.Focus();
                        this.emptyRecord = false;
                    }
                    else
                    {
                        this.ExemptionDataGridView.DataSource = this.exemptionSlectionDataset.F2103ExemptionSelection.DefaultView;
                        this.ExemptionDataGridView.Enabled = false;
                        this.ExemptionDataGridView.Rows[0].Selected = false;
                        this.ExemptionDataGridView.CurrentCell = null;
                        this.emptyRecord = true;
                        this.ExemptionAcceptButton.Enabled = false;
                        if (string.IsNullOrEmpty(ExemptionCode))
                        {
                            this.RemoveButton.Enabled = false;
                        }
                        else
                        {
                            this.RemoveButton.Enabled = true;
                        }
                    }

                    this.RecordCountLabel.Text = recordCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
                    this.DisableVScrollBar();
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
        /// Handles the Click event of the DistrictClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptionClearButton_Click(object sender, EventArgs e)
        {
            this.exemptionSlectionDataset.F2103ExemptionSelection.Clear();
            this.ExemptionDataGridView.DataSource = null;
            this.ExemptionDataGridView.Enabled = false;
            this.ExemptionDataGridView.Rows[0].Selected = false;
            this.ExemptionDataGridView.CurrentCell = null;
            this.ClearFields();
            this.DisableButtons();
            this.DisableVScrollBar();
            this.CodeTextBox.Focus();
            if (!string.IsNullOrEmpty(ExemptionCode))
            {
                this.RemoveButton.Enabled = true;
            }
            else
            {
                this.RemoveButton.Enabled = false;
            }
            //this.DistrictIdTextBox.Focus();
        }

        /// <summary>
        /// Handles the Click event of the DistrictCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptionCancelButton_Click(object sender, EventArgs e)
        {
            if (this.ExemptionCancelButton.Enabled)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the DistrictManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ExemptionManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ////Form districtManagementForm = new Form();
            ////districtManagementForm = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9500, null, this.F9108Controll.WorkItem);
            ////if (districtManagementForm != null)
            ////{
            ////    districtManagementForm.ShowDialog();
            ////}

            ////this.ExemptionDataGridView.Focus();

            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(22080);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the ExemptionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ExemptionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.ExemptionAcceptButton.Enabled)
                {
                    this.GetExemptionDetails();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Editexts the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Editext(object sender, EventArgs e)
        {
            //(!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim())) ||
            if ((!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.PercentTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.MaximunTextBox.Text.Trim())))
            {
                this.EnableSearchButton();
            }
            else
            {
                if (this.exemptionSlectionDataset.F2103ExemptionSelection.Rows.Count > 0)
                {
                    this.ExemptionSearchButton.Enabled = false;
                }
                else
                {
                    this.DisableButtons();
                }
            }
        }


        private void ExemptionManagementLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
            {
                if (ExemptionDataGridView.OriginalRowCount > 0)
                {
                    this.ExemptionDataGridView.Focus();
                }
                else
                {
                    this.CodeTextBox.Focus();
                    //this.RollYearTextBox.Focus();
                }
            }
        }

        #endregion Events

        private void ExemptionDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.ExemptionAcceptButton.Enabled)
                {
                    if (this.ExemptionDataGridView.Rows[e.RowIndex].Cells["ExemptionID"].Value != null && !string.IsNullOrEmpty(this.ExemptionDataGridView.Rows[e.RowIndex].Cells["ExemptionID"].Value.ToString()))
                    {
                        this.GetExemptionDetails();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void ID3TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {

                e.Handled = true;
            }
        }

        private void ID3TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                        if ((e.KeyCode != Keys.Decimal))
                        {
                            if ((e.KeyValue != 190))
                            {
                                nonNumberEntered = true;
                            }
                        }
                    }
                }
            }
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
            //if (Control.ModifierKeys == Keys.Decimal)
            //{
            //    nonNumberEntered = false;
            //}
            //if (e.KeyCode == Keys.Decimal)
            //{
            //    nonNumberEntered = true;
            //}
        }

        private void PercentTextBox_LocationChanged(object sender, EventArgs e)
        {
            this.PercentTextBox.Text = this.PercentTextBox.Text.Trim() == "." ? string.Empty : this.PercentTextBox.Text.Trim();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (this.RemoveButton.Enabled)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }
    }
}
