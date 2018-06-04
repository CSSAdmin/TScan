//--------------------------------------------------------------------------------------------
// <copyright file="F9108.cs" company="Congruent">
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


namespace D9500
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
    /// F9108 class
    /// </summary> 
    public partial class F9108 : Form
    {
        #region variables

        /// <summary>
        /// DistrictSelectionData instance
        /// </summary>
        private DistrictSelectionData districtSlectionDataset = new DistrictSelectionData();

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>  
        private bool emptyRecord;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// Created Instance for F9108Controller
        /// </summary>
        private F9108Controller form9108Control;

        /// <summary>
        /// districtID
        /// </summary>
        private int districtID;

        #endregion variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9108"/> class.
        /// </summary>
        public F9108()
        {
            this.InitializeComponent();
            this.CancelButton = this.DistrictCancelButton;
            this.AcceptButton = this.DistrictSearchButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9108"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F9108(string rollYear)
        {
            this.InitializeComponent();
            this.CancelButton = this.DistrictCancelButton;
            this.AcceptButton = this.DistrictSearchButton;
            this.RollYearTextBox.Text = rollYear.ToString();
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
        /// Gets or sets the F9108 controll.
        /// </summary>
        /// <value>The F9108 controll.</value>
        [CreateNew]
        public F9108Controller F9108Controll
        {
            get { return this.form9108Control as F9108Controller; }
            set { this.form9108Control = value; }
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
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DistrictNoTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
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
                    this.DistrictIdTextBox.Focus();
                }
            }
        }
        
        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.DistrictIdTextBox.Text = string.Empty;
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
        /// Handles the Load event of the F9108 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9108_Load(object sender, EventArgs e)
        {
            try
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
                    this.RollYearTextBox.Select();
                    this.DistrictSearchButton.Enabled = true;
                    this.DistrictClearButton.Enabled = true;
                }
                else
                {
                    this.DistrictIdTextBox.Focus();
                    this.DistrictSearchButton.Enabled = false;
                    this.DistrictClearButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    this.GetDistrictDetails();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    int districtno = 0;

                    if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                    {
                        int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
                    }
                    else
                    {
                        rollYear = -999;
                    }

                    if (!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim()))
                    {
                        int.TryParse(this.DistrictIdTextBox.Text.Trim(), out districtid);
                    }
                    else
                    {
                        districtid = -999;
                    }

                    if (!string.IsNullOrEmpty(this.DistrictNoTextBox.Text.Trim()))
                    {
                        int.TryParse(this.DistrictNoTextBox.Text.Trim(), out districtno);
                    }
                    else
                    {
                        districtno = -999;
                    }

                    ////this.districtSlectionDataset = F9108WorkItem.GetDistrictSelectionData(districtid, districtno, this.DescTextBox.Text.Replace("'", "''").Trim(), rollYear);
                    if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count > 0)
                    {
                        recordCount = this.districtSlectionDataset.ListDistrictSelection.Rows.Count;
                        this.DistrictSelectionDataGridView.Enabled = true;
                        this.DistrictSelectionDataGridView.Rows[0].Selected = true;
                        this.DistrictSelectionDataGridView.DataSource = this.districtSlectionDataset.ListDistrictSelection;
                        this.DistrictAcceptButton.Enabled = true;
                        this.DistrictSelectionDataGridView.Focus();
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
            try
            {
                this.districtSlectionDataset.ListDistrictSelection.Clear();
                this.DistrictSelectionDataGridView.DataSource = null;
                this.DistrictSelectionDataGridView.Enabled = false;
                this.DistrictSelectionDataGridView.Rows[0].Selected = false;
                this.DistrictSelectionDataGridView.CurrentCell = null;
                this.ClearFields();
                this.DisableButtons();
                this.DisableVScrollBar();
                this.DistrictIdTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DistrictCancelButton.Enabled)
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                formInfo = TerraScanCommon.GetFormInfo(11002);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                            this.DistrictIdTextBox.Focus();
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
            if ((!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DistrictNoTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())))
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
    }
}