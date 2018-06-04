//----------------------------------------------------------------------------------
// <copyright file="F2101.cs" company="Congruent">
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

    public partial class F2101 : Form
    {

        #region variables

        /// <summary>
        /// DistrictSelectionData instance
        /// </summary>
        private F2101LocationSelectionData locationSlectionDataset = new F2101LocationSelectionData();

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
        private F2101Controller form2101Control;

        /// <summary>
        /// districtID
        /// </summary>
        private int locationID;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// For Selected value
        /// </summary>
        private string commandValue;

        #endregion variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="F2101"/> class.
        /// </summary>
        public F2101()
        {
            InitializeComponent();
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
        public F2101Controller F2101Controll
        {
            get { return this.form2101Control as F2101Controller; }
            set { this.form2101Control = value; }
        }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        /// <value>The district id.</value>
        public int LocationId
        {
            get { return this.locationID; }
            set { this.locationID = value; }
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
                if (this.locationSlectionDataset.F2101LocationSelection.Rows.Count > 0)
                {
                    if (this.LocationSelectionDataGridView.SelectedRows.Count > 0)
                    {
                        this.selected = this.LocationSelectionDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.LocationSelectionDataGridView.SelectedCells.Count > 0)
                    {
                        this.selected = this.LocationSelectionDataGridView.CurrentCell.RowIndex;
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
            if (this.locationSlectionDataset.F2101LocationSelection.Rows.Count > 5)
            {
                this.LocationSelectionVerticalScroll.Enabled = true;
                this.LocationSelectionVerticalScroll.Visible = false;
            }
            else
            {
                this.LocationSelectionVerticalScroll.Enabled = false;
                this.LocationSelectionVerticalScroll.Visible = true;
                this.LocationSelectionVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.LocationSelectionDataGridView.AllowUserToResizeColumns = false;
            this.LocationSelectionDataGridView.AutoGenerateColumns = false;
            this.LocationSelectionDataGridView.AllowUserToResizeRows = false;
            this.LocationSelectionDataGridView.StandardTab = true;
            this.LocationSelectionDataGridView.EnableBinding = false;

            this.LocationSelectionDataGridView.Columns[0].DataPropertyName = "LocationID";
            this.LocationSelectionDataGridView.Columns[1].DataPropertyName = "LocationCode";
            this.LocationSelectionDataGridView.Columns[2].DataPropertyName = "Description";
            this.LocationSelectionDataGridView.PrimaryKeyColumnName = this.LocationSelectionDataGridView.Columns[0].DataPropertyName;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            //!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim()) ||
            if (!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescTextBox.Text.Trim()))
            {
                this.LocationSearchButton.Enabled = true;
                this.LocationClearButton.Enabled = true;
            }
            else
            {
                this.LocationSearchButton.Enabled = false;

                if (this.locationSlectionDataset.F2101LocationSelection.Rows.Count <= 0)
                {
                    this.LocationAcceptButton.Enabled = false;
                    this.LocationClearButton.Enabled = false;
                }
                else
                {
                    this.LocationAcceptButton.Enabled = true;
                    this.LocationClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Gets the district details.
        /// </summary>
        private void GetLocationDetails()
        {
            int rowId = 0;
            rowId = this.GetRowIndex();

            if (!this.emptyRecord)
            {
                if (!string.IsNullOrEmpty(this.LocationSelectionDataGridView.Rows[rowId].Cells["LocationID"].Value.ToString()))
                {
                    int.TryParse(this.LocationSelectionDataGridView.Rows[rowId].Cells["LocationID"].Value.ToString(), out this.locationID);

                    this.commandResult = this.locationID.ToString();

                    if (!string.IsNullOrEmpty(this.LocationSelectionDataGridView.Rows[rowId].Cells["Code"].Value.ToString()))
                    {
                        this.commandValue = this.LocationSelectionDataGridView.Rows[rowId].Cells["Code"].Value.ToString();
                    }
                    else
                    {
                        this.commandValue = string.Empty;
                    }
                    if(!string.IsNullOrEmpty(this.LocationSelectionDataGridView.Rows[rowId].Cells["Description"].Value.ToString()))
                    {
                        var tempDesc = this.LocationSelectionDataGridView.Rows[rowId].Cells["Description"].Value.ToString();
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
            this.DescTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.LocationSearchButton.Enabled = false;
            this.LocationAcceptButton.Enabled = false;
            this.LocationClearButton.Enabled = false;
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
        private void F2101_Load(object sender, EventArgs e)
        {
            this.emptyRecord = true;
            this.CustomizeDataGrid();
            this.LocationSelectionDataGridView.DataSource = null;
            this.LocationSelectionDataGridView.Enabled = false;
            this.LocationSelectionDataGridView.Rows[0].Selected = false;
            this.LocationSelectionDataGridView.CurrentCell = null;
            this.DisableVScrollBar();
            ////this.ClearFields();
            this.DisableButtons();
        }

        /// <summary>
        /// Handles the Click event of the DistrictAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationAcceptButton_Click(object sender, EventArgs e)
        {
            if (!this.emptyRecord)
            {
                this.GetLocationDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.LocationSearchButton.Enabled)
                {
                    int recordCount = 0;                    
                    this.locationSlectionDataset = this.form2101Control.WorkItem.f2101_GetLocationSelection(this.CodeTextBox.Text.Trim(), this.DescTextBox.Text.Replace("'", "''").Trim());
                    if (this.locationSlectionDataset.F2101LocationSelection.Rows.Count > 0)
                    {
                        recordCount = this.locationSlectionDataset.F2101LocationSelection.Rows.Count;
                        this.LocationSelectionDataGridView.Enabled = true;
                        this.LocationSelectionDataGridView.DataSource = this.locationSlectionDataset.F2101LocationSelection.DefaultView;
                        this.LocationSelectionDataGridView.Focus();
                        this.LocationSelectionDataGridView.Rows[0].Selected = true;
                        this.LocationAcceptButton.Enabled = true;
                        ////this.LocationSelectionDataGridView.Focus();
                        this.emptyRecord = false;
                    }
                    else
                    {
                        this.LocationSelectionDataGridView.DataSource = this.locationSlectionDataset.F2101LocationSelection.DefaultView;
                        this.LocationSelectionDataGridView.Enabled = false;
                        this.LocationSelectionDataGridView.Rows[0].Selected = false;
                        this.LocationSelectionDataGridView.CurrentCell = null;
                        this.emptyRecord = true;
                        this.LocationAcceptButton.Enabled = false;
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
        private void LocationClearButton_Click(object sender, EventArgs e)
        {
            this.locationSlectionDataset.F2101LocationSelection.Clear();
            this.LocationSelectionDataGridView.DataSource = null;
            this.LocationSelectionDataGridView.Enabled = false;
            this.LocationSelectionDataGridView.Rows[0].Selected = false;
            this.LocationSelectionDataGridView.CurrentCell = null;
            this.ClearFields();
            this.DisableButtons();
            this.DisableVScrollBar();
            this.CodeTextBox.Focus();
            //this.DistrictIdTextBox.Focus();
        }

        /// <summary>
        /// Handles the Click event of the DistrictCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationCancelButton_Click(object sender, EventArgs e)
        {
            if (this.LocationCancelButton.Enabled)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the DistrictManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LocationManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(21010);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the LocationSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void LocationSelectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.LocationAcceptButton.Enabled)
                {
                    this.GetLocationDetails();
                    this.DialogResult = DialogResult.OK;
                }
            }
            //if (!this.emptyRecord)
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        if (!string.IsNullOrEmpty(this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["LocationID"].Value.ToString()))
            //        {
            //            int.TryParse(this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["LocationID"].Value.ToString(), out this.locationID);
            //            this.commandResult = this.locationID.ToString();

                //            if (!string.IsNullOrEmpty(this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["Code"].Value.ToString()))
            //            {
            //                this.commandValue = this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            //            }
            //            else
            //            {
            //                this.commandValue = string.Empty;
            //            }
            //            if (!string.IsNullOrEmpty(this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["Description"].Value.ToString()))
            //            {
            //                var tempDesc = this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["Description"].Value.ToString();
            //                this.commandValue = commandValue + "-" + tempDesc;
            //            }
            //            this.CodeTextBox.Focus();
            //            //this.DistrictIdTextBox.Focus();
            //            this.DialogResult = DialogResult.OK;
            //        }
            //    }
            //}

            catch (Exception)
            {
            }
        }


        /// <summary>
        /// Handles the CellDoubleClick event of the GroupingSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void LocationSelectionDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.LocationAcceptButton.Enabled)
                {
                    if (this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["LocationID"].Value != null && !string.IsNullOrEmpty(this.LocationSelectionDataGridView.Rows[e.RowIndex].Cells["LocationID"].Value.ToString()))
                    {
                        this.GetLocationDetails();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            if ((!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())))
            {
                this.EnableSearchButton();
            }
            else
            {
                if (this.locationSlectionDataset.F2101LocationSelection.Rows.Count > 0)
                {
                    this.LocationSearchButton.Enabled = false;
                }
                else
                {
                    this.DisableButtons();
                }
            }
        }


        private void LocationManagementLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
            {
                if (LocationSelectionDataGridView.OriginalRowCount > 0)
                {
                    this.LocationSelectionDataGridView.Focus();
                }
                else
                {
                    this.CodeTextBox.Focus();
                    //this.RollYearTextBox.Focus();
                }
            }
        }

        #endregion Events
    }
}
