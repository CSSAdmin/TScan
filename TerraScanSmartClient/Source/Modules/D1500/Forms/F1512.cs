//--------------------------------------------------------------------------------------------
// <copyright file="F1512.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the District Selection.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 August 2006	KRISHNA ABBURI	    Created
//*********************************************************************************/


namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Collections;
    using System.IO;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using TerraScan.Utilities;

    /// <summary>
    /// F1512 class
    /// </summary> 
    public partial class F1512 : Form
    {
        #region variables

        /// <summary>
        /// DistrictSelectionData instance
        /// </summary>
        private F1512DistrictSelectionData districtSlectionDataset = new F1512DistrictSelectionData();

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
        private F1512Controller form1512Control;

        /// <summary>
        /// districtID
        /// </summary>
        private int districtID;

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
        /// Initializes a new instance of the <see cref="T:F1512"/> class.
        /// </summary>
        public F1512()
        {
            this.InitializeComponent();
            this.CancelButton = this.DistrictCancelButton;
            this.AcceptButton = this.DistrictSearchButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1512"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F1512(string rollYear, int FormNO)
        {
            this.InitializeComponent();
            this.CancelButton = this.DistrictCancelButton;
            this.AcceptButton = this.DistrictSearchButton;
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
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region properites

        /// <summary>
        /// Gets or sets the F1512 controll.
        /// </summary>
        /// <value>The F1512 controll.</value>
        [CreateNew]
        public F1512Controller F1512Controll
        {
            get { return this.form1512Control as F1512Controller; }
            set { this.form1512Control = value; }
        }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        /// <value>The district id.</value>
        public int DistrictId
        {
            get { return this.districtID; }
            set { this.districtID = value; }
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
                if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count > 0)
                {
                    if (this.DistrictSelectionDataGridView.SelectedRows.Count > 0)
                    {
                        this.selected = this.DistrictSelectionDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.DistrictSelectionDataGridView.SelectedCells.Count > 0)
                    {
                        this.selected = this.DistrictSelectionDataGridView.CurrentCell.RowIndex;
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
            if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count > 5)
            {
                this.DistrictSlectionVerticalScroll.Enabled = true;
                this.DistrictSlectionVerticalScroll.Visible = false;
            }
            else
            {
                this.DistrictSlectionVerticalScroll.Enabled = false;
                this.DistrictSlectionVerticalScroll.Visible = true;
                this.DistrictSlectionVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.DistrictSelectionDataGridView.AllowUserToResizeColumns = false;
            this.DistrictSelectionDataGridView.AutoGenerateColumns = false;
            this.DistrictSelectionDataGridView.AllowUserToResizeRows = false;
            this.DistrictSelectionDataGridView.StandardTab = true;
            this.DistrictSelectionDataGridView.EnableBinding = false;

            this.DistrictSelectionDataGridView.Columns[0].DataPropertyName = "DistrictID";
            this.DistrictSelectionDataGridView.Columns[1].DataPropertyName = "District";
            this.DistrictSelectionDataGridView.Columns[2].DataPropertyName = "Description";
            this.DistrictSelectionDataGridView.Columns[3].DataPropertyName = "RollYear";
            this.DistrictSelectionDataGridView.PrimaryKeyColumnName = this.DistrictSelectionDataGridView.Columns[0].DataPropertyName;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            //!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim()) ||
            if (!string.IsNullOrEmpty(this.DistrictNoTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.DistrictSearchButton.Enabled = true;
                this.DistrictClearButton.Enabled = true;
            }
            else
            {
                this.DistrictSearchButton.Enabled = false;

                if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count <= 0)
                {
                    this.DistrictAcceptButton.Enabled = false;
                    this.DistrictClearButton.Enabled = false;
                }
                else
                {
                    this.DistrictAcceptButton.Enabled = true;
                    this.DistrictClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Gets the district details.
        /// </summary>
        private void GetDistrictDetails()
        {
            int rowId = 0;
            rowId = this.GetRowIndex();

            if (!this.emptyRecord)
            {
                if (!string.IsNullOrEmpty(this.DistrictSelectionDataGridView.Rows[rowId].Cells["ID"].Value.ToString()))
                {
                    int.TryParse(this.DistrictSelectionDataGridView.Rows[rowId].Cells["ID"].Value.ToString(), out this.districtID);

                    this.commandResult = this.districtID.ToString();

                    if (!string.IsNullOrEmpty(this.DistrictSelectionDataGridView.Rows[rowId].Cells["District"].Value.ToString()))
                    {
                        this.commandValue = this.DistrictSelectionDataGridView.Rows[rowId].Cells["District"].Value.ToString();
                    }
                    else
                    {
                        this.commandValue = string.Empty;
                    }
                    this.DistrictNoTextBox.Focus();  
                 //   this.DistrictIdTextBox.Focus();
                }
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            //this.DistrictIdTextBox.Text = string.Empty;
            this.DistrictNoTextBox.Text = string.Empty;
            this.DescTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.DistrictSearchButton.Enabled = false;
            this.DistrictAcceptButton.Enabled = false;
            this.DistrictClearButton.Enabled = false;
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
        /// Handles the Load event of the F1512 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1512_Load(object sender, EventArgs e)
        {
            this.emptyRecord = true;
            this.CustomizeDataGrid();
            this.DistrictSelectionDataGridView.DataSource = null;
            this.DistrictSelectionDataGridView.Enabled = false;
            this.DistrictSelectionDataGridView.Rows[0].Selected = false;
            this.DistrictSelectionDataGridView.CurrentCell = null;
            this.DisableVScrollBar();
            ////this.ClearFields();
            this.DisableButtons();
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text))
            {
                //this.RollYearTextBox.Select();
                this.DistrictNoTextBox.Focus();
                this.DistrictSearchButton.Enabled = true;
                this.DistrictClearButton.Enabled = true;
            }
            else
            {
                //this.DistrictIdTextBox.Focus();
                this.DistrictNoTextBox.Focus();
                this.DistrictSearchButton.Enabled = false;
                this.DistrictClearButton.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictAcceptButton_Click(object sender, EventArgs e)
        {
            if (!this.emptyRecord)
            {
                this.GetDistrictDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DistrictSearchButton.Enabled)
                {
                    int recordCount = 0;
                    int rollYear = 0;
                    int districtid = 0;
                    ////string districtno=null; ////= 0; ////Ramya D

                    if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                    {
                        int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
                    }
                    else
                    {
                        rollYear = -999;
                    }



                    //if (!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim()))
                    //{
                    //    int.TryParse(this.DistrictIdTextBox.Text.Trim(), out districtid);
                    //}
                    //else
                    //{
                      districtid = -999;
                    //}

                    /// Modified By Ramya D

                    //if (!string.IsNullOrEmpty(this.DistrictNoTextBox.Text.Trim()))
                    //{
                    //    int.TryParse(this.DistrictNoTextBox.Text.Trim(), out districtno);
                    //}
                    //else
                    //{
                    //    districtno = -999;
                    //if (!string.IsNullOrEmpty(this.DistrictNoTextBox.Text.Trim()))
                    //{
                    //    districtno = this.DistrictNoTextBox.Text.Trim();
                    //}
                    //}

                    /// ---------------- End ----------------
                    this.districtSlectionDataset = this.form1512Control.WorkItem.F1512_GetDistrictSelectionData(districtid, this.DistrictNoTextBox.Text.Trim(), this.DescTextBox.Text.Replace("'", "''").Trim(), rollYear);
                    if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count > 0)
                    {
                        recordCount = this.districtSlectionDataset.ListDistrictSelection.Rows.Count;
                        this.DistrictSelectionDataGridView.Enabled = true;                        
                        this.DistrictSelectionDataGridView.DataSource = this.districtSlectionDataset.ListDistrictSelection;
                        this.DistrictSelectionDataGridView.Focus();
                        this.DistrictSelectionDataGridView.Rows[0].Selected = true;
                        this.DistrictAcceptButton.Enabled = true;
                        ////this.DistrictSelectionDataGridView.Focus();
                        this.emptyRecord = false;
                    }
                    else
                    {
                        this.DistrictSelectionDataGridView.DataSource = this.districtSlectionDataset.ListDistrictSelection;
                        this.DistrictSelectionDataGridView.Enabled = false;
                        this.DistrictSelectionDataGridView.Rows[0].Selected = false;
                        this.DistrictSelectionDataGridView.CurrentCell = null;
                        this.emptyRecord = true;
                        this.DistrictAcceptButton.Enabled = false;
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
        private void DistrictClearButton_Click(object sender, EventArgs e)
        {
            this.districtSlectionDataset.ListDistrictSelection.Clear();
            this.DistrictSelectionDataGridView.DataSource = null;
            this.DistrictSelectionDataGridView.Enabled = false;
            this.DistrictSelectionDataGridView.Rows[0].Selected = false;
            this.DistrictSelectionDataGridView.CurrentCell = null;
            this.ClearFields();
            this.DisableButtons();
            this.DisableVScrollBar();
            this.DistrictNoTextBox.Focus();
            //this.DistrictIdTextBox.Focus();
        }

        /// <summary>
        /// Handles the Click event of the DistrictCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictCancelButton_Click(object sender, EventArgs e)
        {
            if (this.DistrictCancelButton.Enabled)
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
        private void DistrictManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ////Form districtManagementForm = new Form();
            ////districtManagementForm = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9500, null, this.F9108Controll.WorkItem);
            ////if (districtManagementForm != null)
            ////{
            ////    districtManagementForm.ShowDialog();
            ////}

            ////this.DistrictSelectionDataGridView.Focus();

            try
            {
                FormInfo formInfo;
                // BugId 1846 Fixed (Changed the form no 11002 to 11009)
                formInfo = TerraScanCommon.GetFormInfo(11009);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the DistrictSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistrictSelectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (!string.IsNullOrEmpty(this.DistrictSelectionDataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
                        {
                            int.TryParse(this.DistrictSelectionDataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out this.districtID);
                            this.commandResult = this.districtID.ToString();

                            if (!string.IsNullOrEmpty(this.DistrictSelectionDataGridView.Rows[e.RowIndex].Cells["District"].Value.ToString()))
                            {
                                this.commandValue = this.DistrictSelectionDataGridView.Rows[e.RowIndex].Cells["District"].Value.ToString();
                            }
                            else
                            {
                                this.commandValue = string.Empty;
                            }
                            this.DistrictNoTextBox.Focus();  
                            //this.DistrictIdTextBox.Focus();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
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
            if ( (!string.IsNullOrEmpty(this.DistrictNoTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())))
            {
                this.EnableSearchButton();
            }
            else
            {
                if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count > 0)
                {
                    this.DistrictSearchButton.Enabled = false;
                }
                else
                {
                    this.DisableButtons();
                }
            }
        }

        #endregion Events

        private void DistrictManagementLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
            {
                if ( DistrictSelectionDataGridView.OriginalRowCount > 0)
                {
                    this.DistrictSelectionDataGridView.Focus();
                }
                else
                {
                    this.DistrictNoTextBox.Focus(); 
                    //this.RollYearTextBox.Focus();
                }
            }
        }
    }
}