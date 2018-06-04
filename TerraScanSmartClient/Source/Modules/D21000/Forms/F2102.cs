//----------------------------------------------------------------------------------
// <copyright file="F2102.cs" company="Congruent">
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
// 25 OCT 2013		PurusHotham A           Created
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

    /// <summary>
    /// F2102 class
    /// </summary>
    public partial class F2102 : Form
    {

        #region variables

        /// <summary>
        /// DistrictSelectionData instance
        /// </summary>
        private F2102GroupingSelectionData groupSelectionDataset = new F2102GroupingSelectionData();

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
        private F2102Controller form2102Control;

        /// <summary>
        /// groupID
        /// </summary>
        private int groupID;

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
        public F2102()
        {
            InitializeComponent();
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
        public F2102Controller F2102Controll
        {
            get { return this.form2102Control as F2102Controller; }
            set { this.form2102Control = value; }
        }

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        /// <value>The district id.</value>
        public int GroupId
        {
            get { return this.groupID; }
            set { this.groupID = value; }
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

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.GroupingSelectionDataGridView.AllowUserToResizeColumns = false;
            this.GroupingSelectionDataGridView.AutoGenerateColumns = false;
            this.GroupingSelectionDataGridView.AllowUserToResizeRows = false;
            this.GroupingSelectionDataGridView.StandardTab = true;
            this.GroupingSelectionDataGridView.EnableBinding = false;

            this.GroupingSelectionDataGridView.Columns[0].DataPropertyName = "GroupingID";
            this.GroupingSelectionDataGridView.Columns[1].DataPropertyName = "GroupCode";
            this.GroupingSelectionDataGridView.Columns[2].DataPropertyName = "Description";
            this.GroupingSelectionDataGridView.PrimaryKeyColumnName = this.GroupingSelectionDataGridView.Columns[0].DataPropertyName;
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            //!string.IsNullOrEmpty(this.DistrictIdTextBox.Text.Trim()) ||
            if (!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescTextBox.Text.Trim()))
            {
                this.GroupingSearchButton.Enabled = true;
                this.GroupingClearButton.Enabled = true;
            }
            else
            {
                this.GroupingSearchButton.Enabled = false;

                if (this.groupSelectionDataset.F2102GroupingSelection.Rows.Count <= 0)
                {
                    this.GroupingAcceptButton.Enabled = false;
                    this.GroupingClearButton.Enabled = false;
                }
                else
                {
                    this.GroupingAcceptButton.Enabled = true;
                    this.GroupingClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.groupSelectionDataset.F2102GroupingSelection.Rows.Count > 5)
            {
                this.GroupingSelectionVerticalScroll.Enabled = true;
                this.GroupingSelectionVerticalScroll.Visible = false;
            }
            else
            {
                this.GroupingSelectionVerticalScroll.Enabled = false;
                this.GroupingSelectionVerticalScroll.Visible = true;
                this.GroupingSelectionVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            //this.DistrictIdTextBox.Text = string.Empty;
            this.CodeTextBox.Text = string.Empty;
            this.DescTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.GroupingAcceptButton.Enabled = false;
            this.GroupingSearchButton.Enabled = false;
            this.GroupingClearButton.Enabled = false;
        }


        /// <summary>
        /// Gets the district details.
        /// </summary>
        private void GetGroupDetails()
        {
            int rowId = 0;
            rowId = this.GetRowIndex();
            if (!this.emptyRecord)
            {
                if (!string.IsNullOrEmpty(this.GroupingSelectionDataGridView.Rows[rowId].Cells["GroupID"].Value.ToString()))
                {
                    int.TryParse(this.GroupingSelectionDataGridView.Rows[rowId].Cells["GroupID"].Value.ToString(), out this.groupID);

                    this.commandResult = this.groupID.ToString();

                    if (!string.IsNullOrEmpty(this.GroupingSelectionDataGridView.Rows[rowId].Cells["GroupCode"].Value.ToString()))
                    {
                        this.commandValue = this.GroupingSelectionDataGridView.Rows[rowId].Cells["GroupCode"].Value.ToString();
                    }
                    else
                    {
                        this.commandValue = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(this.GroupingSelectionDataGridView.Rows[rowId].Cells["Description"].Value.ToString()))
                    {
                        var tempDesc = this.GroupingSelectionDataGridView.Rows[rowId].Cells["Description"].Value.ToString();
                        this.commandValue = commandValue + " - " + tempDesc;
                    }
                    this.CodeTextBox.Focus();
                    //   this.CodeTextBox.Focus();
                }
            }
        }


        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>returns GetRowIndex of selected item</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.groupSelectionDataset.F2102GroupingSelection.Rows.Count > 0)
                {
                    if (this.GroupingSelectionDataGridView.SelectedRows.Count > 0)
                    {
                        this.selected = this.GroupingSelectionDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.GroupingSelectionDataGridView.SelectedCells.Count > 0)
                    {
                        this.selected = this.GroupingSelectionDataGridView.CurrentCell.RowIndex;
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

        /// <summary>
        /// Handles the Load event of the F2102 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F2102_Load(object sender, EventArgs e)
        {

            this.CustomizeDataGrid();
            this.GroupingSelectionDataGridView.DataSource = null;
            this.GroupingSelectionDataGridView.Enabled = false;
            this.GroupingSelectionDataGridView.Rows[0].Selected = false;
            this.GroupingSelectionDataGridView.CurrentCell = null;
            this.DisableVScrollBar();
            this.DisableButtons();
            this.CodeTextBox.Focus();
            this.GroupingSearchButton.Enabled = false;
            this.GroupingClearButton.Enabled = false;

        }

        /// <summary>
        /// Handles the Click event of the GroupingAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupingAcceptButton_Click(object sender, EventArgs e)
        {
            if (!this.emptyRecord)
            {
                this.GetGroupDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the GroupingCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupingCancelButton_Click(object sender, EventArgs e)
        {
            if (this.GroupingCancelButton.Enabled)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the GroupingClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupingClearButton_Click(object sender, EventArgs e)
        {
            this.groupSelectionDataset.F2102GroupingSelection.Clear();
            this.GroupingSelectionDataGridView.DataSource = null;
            this.GroupingSelectionDataGridView.Enabled = false;
            this.GroupingSelectionDataGridView.Rows[0].Selected = false;
            this.GroupingSelectionDataGridView.CurrentCell = null;
            this.ClearFields();
            this.DisableButtons();
            this.DisableVScrollBar();
            this.CodeTextBox.Focus();
        }

        /// <summary>
        /// Handles the Click event of the GroupingSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupingSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.GroupingSearchButton.Enabled)
                {
                    int recordCount = 0;
                    this.groupSelectionDataset = this.form2102Control.WorkItem.f2102_GetGroupingSelection(this.CodeTextBox.Text.Trim(), this.DescTextBox.Text.Replace("'", "''").Trim());
                    if (this.groupSelectionDataset.F2102GroupingSelection.Rows.Count > 0)
                    {
                        recordCount = this.groupSelectionDataset.F2102GroupingSelection.Rows.Count;
                        this.GroupingSelectionDataGridView.Enabled = true;
                        this.GroupingSelectionDataGridView.DataSource = this.groupSelectionDataset.F2102GroupingSelection.DefaultView;
                        this.GroupingSelectionDataGridView.Focus();
                        this.GroupingSelectionDataGridView.Rows[0].Selected = true;
                        this.GroupingAcceptButton.Enabled = true;
                        this.emptyRecord = false;
                    }
                    else
                    {
                        this.GroupingSelectionDataGridView.DataSource = this.groupSelectionDataset.F2102GroupingSelection.DefaultView;
                        this.GroupingSelectionDataGridView.Enabled = false;
                        this.GroupingSelectionDataGridView.Rows[0].Selected = false;
                        this.GroupingSelectionDataGridView.CurrentCell = null;
                        this.emptyRecord = true;
                        this.GroupingAcceptButton.Enabled = false;
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
        /// Editexts the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.CodeTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.GroupingSelectionDataGridView.Rows.Count > 0)
                    {
                        this.GroupingSelectionDataGridView.DataSource = null;
                        this.GroupingSelectionDataGridView.Enabled = false;
                        this.GroupingSelectionDataGridView.Rows[0].Selected = false;
                    }
                    this.DisableButtons();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the FundManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GroupingManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(21020);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the GroupingSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupingSelectionDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.GroupingAcceptButton.Enabled)
                {
                    if (this.GroupingSelectionDataGridView.Rows[e.RowIndex].Cells["GroupID"].Value != null && !string.IsNullOrEmpty(this.GroupingSelectionDataGridView.Rows[e.RowIndex].Cells["GroupID"].Value.ToString()))
                    {
                        this.GetGroupDetails();
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
        /// Handles the RowHeaderMouseDoubleClick event of the GroupingSelectionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GroupingSelectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.GroupingAcceptButton.Enabled)
                {
                    this.GetGroupDetails();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

    }
}