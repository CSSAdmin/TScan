//--------------------------------------------------------------------------------------------
// <copyright file="F9051.cs" company="Congruent">
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
// 
//*********************************************************************************/

namespace D9050
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;

    /// <summary>
    /// SnapshotUtilityForm Class
    /// </summary>
    public partial class F9051 : Form
    {
        #region Variables
                
        /// <summary>
        /// Created Readonly String for DateFormat
        /// </summary>
        private readonly string dateFormat = "MM/dd/yyyy";

        /// <summary>
        /// this variable used to set postion userdatagrid view 
        /// </summary>
        private int tempRowId = -1;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int savedSuccessfully;

        /// <summary>
        /// Created Integer for recordCount.
        /// </summary>
        private int recordCount;

        /// <summary>
        /// Created Boolean value.
        /// </summary>
        private bool closingNow;

        /// <summary>
        /// Created Boolean value.
        /// </summary>
        private bool gridEdit;

        /// <summary>
        /// Created Boolean value for ValueChanged.
        /// </summary>
        private bool valueChanged;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// Created Integer to get Selected Row Index.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Created Boolean value.
        /// </summary>
        private bool loadClicked;

        /// <summary>
        /// Created Integer for count
        /// </summary>
        private int count;

        /// <summary>
        /// Created int variable to Find Button Operation
        /// </summary>
        private int buttonOperation;

        /// <summary>
        /// Created Integer for FormID
        /// </summary>
        private int snapshotUtilityFormID;

        /// <summary>
        /// set boolean Value to false
        /// </summary>
        private bool closeButton;       

        /// <summary>
        /// Created string for tempName
        /// </summary>
        private string tempName = string.Empty;

        /// <summary>
        /// Created string for tempDescription
        /// </summary>
        private string tempDescription = string.Empty;

        /// <summary>
        /// Created string for snapshotItemIds
        /// </summary>
        private string snapshotItemIds = string.Empty;

        /// <summary>
        /// Created string for snapshotName
        /// </summary>
        private string snapshotName = string.Empty;

        /// <summary>
        /// Created string for snapshotDescription
        /// </summary>
        private string snapshotDescription = string.Empty;

        /// <summary>
        /// Created string for textBoxFocused
        /// </summary>
        private string textBoxFocused = string.Empty;

        /// <summary>
        /// Created Integer for snapshotId
        /// </summary>
        private int snapshotId;

        /// <summary>
        /// Created for Currency Manager
        /// </summary>
        private CurrencyManager currencyManager;

        /// <summary>
        /// Object for dataset created
        /// </summary>
        private SnapshotUtilityData snapshotUtilityDataSet = new SnapshotUtilityData();

        /// <summary>
        /// Created Boolean value for rowCount
        /// </summary>
        private bool rowCount;

        /// <summary>
        /// controller F9051
        /// </summary>
        private F9051Controller form9051Control;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SnapshotUtilityForm"/> class.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="snapshotIds">The snapshot ids.</param>
        /// <param name="count">The count.</param>
        public F9051(int formId, string snapshotIds, int count)
        {
            this.InitializeComponent();

            // Gets the value form Calling form.
            this.count = count;
            this.snapshotUtilityFormID = formId;
            this.snapshotItemIds = snapshotIds;
            this.Tag = formId;
            this.CancelButton = this.cancelSnapshotUtilityButton;
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, "Edit", 28, 81, 128);
            this.existingPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.existingPictureBox.Height, this.existingPictureBox.Width, "Existing", 174, 150, 94);
        }

        #endregion

        #region Enum

        /// <summary>
        /// Button Functionality
        /// </summary>
        public enum ButtonOperation
        {
            /// <summary>
            /// Empty = 0.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// New = 1.
            /// </summary>
            New = 1,

            /// <summary>
            /// Cancel = 2.
            /// </summary>
            Cancel = 2,

            /// <summary>
            /// Delete = 3.
            /// </summary>
            Delete = 3,

            /// <summary>
            /// snapshotUtilityDataGrid = 4.
            /// </summary>
            SnapshotUtilityDataGrid = 4,

            /// <summary>
            /// EmptyGirdRecord = 5.
            /// </summary>
            EmptyGirdRecord = 5
        }

        #endregion

        #region Property

        /// <summary>
        /// For F9051Control
        /// </summary>
        [CreateNew]
        public F9051Controller Form9051Control
        {
            get { return this.form9051Control as F9051Controller; }
            set { this.form9051Control = value; }
        }       

        /// <summary>
        /// Gets or sets the snap shot id.
        /// </summary>
        /// <value>The snap shot id.</value>
        public int SnapShotId
        {
            get { return this.snapshotId; }
            set { this.snapshotId = value; }
        }

        /// <summary>
        /// Gets or sets the SnapshotName.
        /// </summary>
        /// <value>The Snapshot Name.</value>
        public string SnapshotName
        {
            get { return this.snapshotName; }
            set { this.snapshotName = value; }
        }

        /// <summary>
        /// Gets or sets the Snapshot Description.
        /// </summary>
        /// <value>The Snapshot Description.</value>
        public string SnapshotDescription
        {
            get { return this.snapshotDescription; }
            set { this.snapshotDescription = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// the Form Permissions will be captured and Set the Button Action Modes
        /// </summary>
        /// <param name="e">OnLoad Even Args</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Initialize the controls during form load.
            // this.DisableSnapshotUtilityControls();

            // Used to initialize the values during form load.
            this.GetValues();

            // Loads the record from database.
            this.LoadSnapshotUtilityDataGrid();

            // Used to initialize the Button state.
            this.InitializeButton();
            //// check dataset has row then set the datagrid
            if (this.recordCount > 0)
            {
                this.SetDataGridViewPosition(0);
                this.SetDataBindingValue(0);
            }

            // If snapshotUtilityDataSet is empty then clear all the controls.
            if (this.recordCount == 0)
            {
                this.snapshotUtilityGridView.Rows[0].Selected = false;
                this.ClearTextValues();
            }

            this.rowCount = false;
        }

        /// <summary>
        /// Used to create a empty record in datagrid.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>retruns datatable</returns>
        private static DataTable CreateEmptyRows(DataTable sourceDataTable, int maxRowCount)
        {
            int defaultRowsCount = 0;
            DataRow tempRow;

            if (sourceDataTable.Rows.Count < maxRowCount)
            {
                defaultRowsCount = maxRowCount - sourceDataTable.Rows.Count;

                for (int i = 0; i < defaultRowsCount; i++)
                {
                    tempRow = sourceDataTable.NewRow();

                    for (int j = 0; j < sourceDataTable.Columns.Count; j++)
                    {
                        if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int32")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int16")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Boolean")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else
                        {
                            tempRow[j] = string.Empty;
                        }
                    }

                    sourceDataTable.Rows.Add(tempRow);
                }
            }

            return sourceDataTable;
        }

        /// <summary>
        /// Checks whether Dataset is null or not.
        /// </summary>
        /// <param name="currentDataSet">The current data set.</param>
        /// <returns>Returns bool value</returns>
        private static bool ValidDataSet(DataSet currentDataSet)
        {
            // Checks whether Dataset is null or not.
            if (currentDataSet.Tables[0].Rows.Count > 0 && currentDataSet != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="commentRow">The comment row.</param>
        private void SetDataGridViewPosition(int commentRow)
        {
            // Hilight the selected row in the datagrid.
            if (commentRow >= 0)
            {
                this.snapshotUtilityGridView.Rows[Convert.ToInt32(commentRow)].Selected = true;
                this.snapshotUtilityGridView.CurrentCell = this.snapshotUtilityGridView[0, Convert.ToInt32(commentRow)];
            }
        }

        /// <summary>
        /// Sets the focus.
        /// </summary>
        private void FocusNameTextBox()
        {
            this.nameTextBox.Focus();
            this.nameTextBox.SelectAll();

            // this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 121);
        }

        /// <summary>
        /// Temps the data.
        /// </summary>
        private void TempData()
        {
            if (!string.IsNullOrEmpty(this.tempDescription))
            {
                this.descriptionTextBox.Text = this.tempDescription;
            }

            if (!string.IsNullOrEmpty(this.tempName))
            {
                this.nameTextBox.Text = this.tempName;
            }
        }

        /// <summary>
        /// Sets the focus.
        /// </summary>
        private void SetFocus()
        {
            // Assign the focus to the selected textbox.
            if (this.textBoxFocused == this.descriptionTextBox.Name)
            {
                this.descriptionTextBox.Focus();
                this.descriptionTextBox.SelectAll();
            }
            else if (this.textBoxFocused == this.nameTextBox.Name)
            {
                this.nameTextBox.Focus();
                this.nameTextBox.SelectAll();
            }
        }

        /// <summary>
        /// Sets the data binding value.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetDataBindingValue(int rowId)
        {
            if (this.snapshotUtilityGridView.Rows.Count > 0)
            {
                if (rowId >= 0)
                {
                    this.nameTextBox.Text = this.snapshotUtilityGridView.Rows[rowId].Cells["QueryName"].Value.ToString();
                    this.createdByTextBox.Text = this.snapshotUtilityGridView.Rows[rowId].Cells["CreatedBy"].Value.ToString();
                    this.createdOnTextBox.Text = this.snapshotUtilityGridView.Rows[rowId].Cells["CreatedOn"].Value.ToString();
                    this.descriptionTextBox.Text = this.snapshotUtilityGridView.Rows[rowId].Cells["Description"].Value.ToString();
                    this.countTextBox.Text = this.snapshotUtilityGridView.Rows[rowId].Cells["SnapshotCount"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            if (this.cancelSnapshotUtilityButton.Enabled == false)
            {
                this.CancelButton = this.closeQueryUtilityButton;
            }
            else
            {
                this.CancelButton = this.cancelSnapshotUtilityButton;
            }
        }

        /// <summary>
        /// Gets the row count.
        /// </summary>
        /// <returns>return integer</returns>
        private int GetRowCount()
        {
            int datasetRowCount = 0;
            if (this.snapshotUtilityDataSet.Tables[0].Rows.Count > 0)
            {
                datasetRowCount = this.snapshotUtilityDataSet.Tables[0].Rows.Count;
            }

            return datasetRowCount;
        }

        /// <summary>
        /// Saves the records to database.
        /// </summary>
        private void SaveRecords()
        {
            try
            {
                // Store the values in the control to a local variable.
                string currentSnapshotName = this.nameTextBox.Text.Trim();
                string description = this.descriptionTextBox.Text.Trim();
                ////string createdBy = this.createdByTextBox.Text.Trim();
                ////string createdOn = this.createdOnTextBox.Text.Trim();
                int recordCounts = Convert.ToInt32(this.countTextBox.Text.Trim().ToString());
                bool cancelClicked = false;
                int rowSelected = -1;
                int rowIndex = 0;

                if (this.selectedRow >= 0 && this.CheckRowCount() && this.buttonOperation != (int)ButtonOperation.New)
                {
                    rowIndex = this.GetRowIndex();

                    if (!this.gridEdit)
                    {
                        // Gets the SnapShotId of selected row.
                        this.SnapShotId = Convert.ToInt32(this.snapshotUtilityGridView.Rows[rowIndex].Cells["SnapshotUtilityID"].Value.ToString());
                    }
                    else
                    {
                        this.SnapShotId = Convert.ToInt32(this.snapshotUtilityGridView.Rows[this.tempRowId].Cells["SnapshotUtilityID"].Value.ToString());
                    }
                }

                if (this.RequiredField())
                {
                    if (this.buttonOperation == (int)ButtonOperation.New)
                    {
                        // Inserts the value to database.
                        this.savedSuccessfully = this.Form9051Control.WorkItem.InsertSnapshotUtility(0, currentSnapshotName, this.snapshotUtilityFormID, description, recordCounts, TerraScanCommon.UserId, 0, this.snapshotItemIds);                            
                        this.snapshotUtilityGridView.Enabled = true;
                        this.closingNow = true;
                    }
                    else
                    {
                        // Update the value to database.
                        this.savedSuccessfully = this.Form9051Control.WorkItem.InsertSnapshotUtility(this.SnapShotId, currentSnapshotName, this.snapshotUtilityFormID, description, recordCounts, TerraScanCommon.UserId, 0, this.snapshotItemIds);                            
                        this.closingNow = true;
                    }

                    this.gridEdit = false;
                    this.snapshotUtilityGridView.Focus();

                    // Checks the query name already exists.
                    while (this.savedSuccessfully == 1)
                    {
                        ////AlertForm alert = new AlertForm(SharedFunctions.GetResourceString("SnapshotUtilityTitle"), SharedFunctions.GetResourceString("OverwriteSnapshot"), SharedFunctions.GetResourceString("Overwrite1"));
                        if (MessageBox.Show(SharedFunctions.GetResourceString("OverwriteSnapshot") + "/n" + SharedFunctions.GetResourceString("Overwrite1"), SharedFunctions.GetResourceString("SnapshotUtilityTitle"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            if (this.buttonOperation == (int)ButtonOperation.New)
                            {
                                // Inserts the value to database.
                                this.savedSuccessfully = this.form9051Control.WorkItem.InsertSnapshotUtility(0, currentSnapshotName, this.snapshotUtilityFormID, description, recordCounts, TerraScanCommon.UserId, 1, this.snapshotItemIds);
                                this.closingNow = true;
                            }
                            else
                            {
                                // Update the value to database.
                                this.savedSuccessfully = this.form9051Control.WorkItem.InsertSnapshotUtility(this.SnapShotId, currentSnapshotName, this.snapshotUtilityFormID, description, recordCounts, TerraScanCommon.UserId, 1, this.snapshotItemIds);
                                cancelClicked = false;
                                this.closingNow = true;

                                // Gets the row in which the value have to overwrited.

                                DataGridViewRowCollection rowCollection = this.snapshotUtilityGridView.Rows;
                                foreach (DataGridViewRow row in rowCollection)
                                {
                                    if (this.snapshotUtilityGridView.Rows[row.Index].Cells["QueryName"].Value.ToString() == currentSnapshotName)
                                    {
                                        rowSelected = row.Index;
                                        break;
                                    }
                                }

                                if (rowSelected >= 0)
                                {
                                    this.SetDataGridViewPosition(rowSelected);
                                    this.SetDataBindingValue(rowSelected);
                                }
                            }

                            this.snapshotUtilityGridView.Focus();
                            continue;
                        }
                        else
                        {
                            this.savedSuccessfully = 0;
                            cancelClicked = true;
                            this.closingNow = false;
                            //// this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newSnapshotUtilityButton.Enabled = false;
                            this.saveSnapshotUtilityButton.Enabled = true;
                            this.cancelSnapshotUtilityButton.Enabled = true;

                            this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                            this.nameTextBox.Focus();
                            this.nameTextBox.Select(this.nameTextBox.Text.Length, this.nameTextBox.Text.Length + 1);
                        }
                    }

                    if (!cancelClicked)
                    {
                        this.LoadSnapshotUtilityDataGrid();

                        if (this.buttonOperation == (int)ButtonOperation.New)
                        {
                            this.SetDataGridViewPosition(0);
                            this.SetDataBindingValue(0);
                        }
                        else if (this.GetRowCount() == rowIndex)
                        {
                            this.SetDataGridViewPosition(--rowIndex);
                            this.SetDataBindingValue(--rowIndex);
                        }
                        else
                        {
                            this.SetDataGridViewPosition(rowIndex);
                            this.SetDataBindingValue(rowIndex);
                        }

                        this.valueChanged = false;
                        //// this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));

                        this.saveSnapshotUtilityButton.Enabled = false;
                        this.cancelSnapshotUtilityButton.Enabled = false;

                        this.SetCurrentFormButtons(ButtonOperation.Cancel);
                        this.buttonOperation = (int)ButtonOperation.Empty;
                        //// this.DisableSnapshotUtilityControls();
                        this.selectedRow = 0;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("UtilityMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.nameTextBox.Focus();
                    this.closingNow = false;
                }
            }            
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Enables the query utility control.
        /// </summary>
        private void EnableSnapshotUtilityControl()
        {
            this.nameTextBox.BackColor = System.Drawing.Color.White;
            this.nameTextBox.Enabled = true;
            this.descriptionTextBox.BackColor = System.Drawing.Color.White;
            this.descriptionTextBox.Enabled = true;
            this.createdOnTextBox.Enabled = true;
            this.createdByTextBox.Enabled = true;
            this.countTextBox.Enabled = true;
        }

        /// <summary>
        /// Used to find Required fields have value.
        /// </summary>
        /// <returns>Returns</returns>
        private bool RequiredField()
        {
            // Checks the nameTextBox and countTextBox has values.
            if (this.nameTextBox.Text.Trim().Length > 0 && this.countTextBox.Text.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the buttons according to the status.
        /// </summary>
        /// <param name="buttonName">Name of the button.</param>
        private void SetCurrentFormButtons(ButtonOperation buttonName)
        {
            switch (buttonName)
            {
                case ButtonOperation.New:
                    {
                        this.loadSnapshotUtilityButton.Enabled = false;
                        this.closeQueryUtilityButton.Enabled = false;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        if (string.Compare(this.snapshotItemIds, string.Empty) == 0)
                        {
                            this.newSnapshotUtilityButton.Enabled = false;
                        }
                        else
                        {
                            // this.newSnapshotUtilityButton.Enabled = true && this.newSnapshotUtilityButton.ActualPermission;
                            this.newSnapshotUtilityButton.Enabled = true;
                        }

                        if (this.CheckRowCount())
                        {
                            // this.deleteSnapshotUtilityButton.Enabled = true && this.deleteSnapshotUtilityButton.ActualPermission;
                            this.deleteSnapshotUtilityButton.Enabled = true;
                            this.loadSnapshotUtilityButton.Enabled = true;
                        }
                        else
                        {
                            this.deleteSnapshotUtilityButton.Enabled = false;
                            this.loadSnapshotUtilityButton.Enabled = false;
                        }

                        this.closeQueryUtilityButton.Enabled = true;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.Delete:
                    {
                        if (string.Compare(this.snapshotItemIds, string.Empty) == 0)
                        {
                            this.newSnapshotUtilityButton.Enabled = false;
                        }
                        else
                        {
                            // this.newSnapshotUtilityButton.Enabled = true && this.newSnapshotUtilityButton.ActualPermission;
                            this.newSnapshotUtilityButton.Enabled = true;
                        }

                        if (!this.rowCount)
                        {
                            // this.deleteSnapshotUtilityButton.Enabled = true && this.deleteSnapshotUtilityButton.ActualPermission;
                            this.deleteSnapshotUtilityButton.Enabled = true;
                            this.loadSnapshotUtilityButton.Enabled = true;
                        }
                        else
                        {
                            this.deleteSnapshotUtilityButton.Enabled = false;
                            this.loadSnapshotUtilityButton.Enabled = false;
                        }

                        this.closeQueryUtilityButton.Enabled = true;
                        this.rowCount = false;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.SnapshotUtilityDataGrid:
                    {
                        this.loadSnapshotUtilityButton.Enabled = false;
                        this.deleteSnapshotUtilityButton.Enabled = false;
                        this.closeQueryUtilityButton.Enabled = false;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.EmptyGirdRecord:
                    {
                        if (string.Compare(this.snapshotItemIds, string.Empty) == 0)
                        {
                            this.newSnapshotUtilityButton.Enabled = false;
                        }
                        else
                        {
                            // this.newSnapshotUtilityButton.Enabled = true && this.newSnapshotUtilityButton.ActualPermission;
                            this.newSnapshotUtilityButton.Enabled = true;
                        }

                        this.saveSnapshotUtilityButton.Enabled = false;
                        this.cancelSnapshotUtilityButton.Enabled = false;
                        this.deleteSnapshotUtilityButton.Enabled = false;
                        this.loadSnapshotUtilityButton.Enabled = false;
                        this.closeQueryUtilityButton.Enabled = true;
                        this.SetCancelButton();
                        break;
                    }
            }
        }

        /// <summary>
        /// Used to Initialize the query utility form controls.
        /// </summary>
        private void DisableSnapshotUtilityControls()
        {
            this.nameTextBox.Enabled = false;
            this.descriptionTextBox.Enabled = false;

            if (this.recordCount == 0)
            {
                this.createdOnTextBox.Enabled = false;
                this.createdByTextBox.Enabled = false;
                this.countTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// Discards the chages during Cancel Button Click.
        /// </summary>
        private void DiscardChages()
        {
            // Clear all the changes made in the Dataset.
            this.snapshotUtilityDataSet.Tables[0].RejectChanges();
            this.snapshotUtilityGridView.Enabled = true;
            this.currencyManager.Position = Convert.ToInt32("0");
            this.buttonOperation = (int)ButtonOperation.Empty;
            //// this.DisableSnapshotUtilityControls();
            //// Calls the CancelMode to set buttons.
            // this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));

            this.saveSnapshotUtilityButton.Enabled = false;
            this.cancelSnapshotUtilityButton.Enabled = false;

            this.SetCurrentFormButtons(ButtonOperation.Cancel);
            this.LoadSnapshotUtilityDataGrid();
            if (this.recordCount == 0)
            {
                this.ClearTextValues();
            }
        }

        /// <summary>
        /// Clears the text values.
        /// </summary>
        private void ClearTextValues()
        {
            this.nameTextBox.Text = string.Empty;
            this.createdByTextBox.Text = string.Empty;
            this.createdOnTextBox.Text = string.Empty;
            this.descriptionTextBox.Text = string.Empty;
            this.countTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Checks the row count.
        /// </summary>
        /// <returns>Returns </returns>
        private bool CheckRowCount()
        {
            // checks snapshotUtilityGridView has rows in it.
            if (this.snapshotUtilityGridView.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the values to Initialize during form load.
        /// </summary>
        private void GetValues()
        {
            // Gets the Username.
            this.createdByTextBox.Text = TerraScanCommon.UserName;

            // Used to load the current date.
            this.createdOnTextBox.Text = DateTime.Now.ToString(this.dateFormat);

            // Gets the formID.
            this.formIDLabel.Text = "9051";

            // Gets the Where Condition Value.
            this.countTextBox.Text = this.count.ToString();
        }

        /// <summary>
        /// Initializes the buttons during form load.
        /// </summary>
        private void InitializeButton()
        {
            if (string.Compare(this.snapshotItemIds.ToString(), string.Empty) == 0)
            {
                this.newSnapshotUtilityButton.Enabled = false;
            }
            else
            {
                // this.newSnapshotUtilityButton.Enabled = true && this.newSnapshotUtilityButton.ActualPermission;
                this.newSnapshotUtilityButton.Enabled = true;
            }

            if (this.recordCount > 0)
            {
                // this.deleteSnapshotUtilityButton.Enabled = true && this.deleteSnapshotUtilityButton.ActualPermission;
                this.deleteSnapshotUtilityButton.Enabled = true;
                this.loadSnapshotUtilityButton.Enabled = true;
            }
            else
            {
                this.deleteSnapshotUtilityButton.Enabled = false;
                this.loadSnapshotUtilityButton.Enabled = false;
            }

            this.saveSnapshotUtilityButton.Enabled = false;
            this.cancelSnapshotUtilityButton.Enabled = false;
        }

        /* /// <summary>
        /// Sets the data binding.
        /// </summary>
        private void SetDataBinding()
        {
            this.nameTextBox.DataBindings.Add("Text", this.snapshotUtilityDataSet.Tables[0], "SnapshotName");
            this.descriptionTextBox.DataBindings.Add("Text", this.snapshotUtilityDataSet.Tables[0], "Description");
            this.createdByTextBox.DataBindings.Add("Text", this.snapshotUtilityDataSet.Tables[0], "CreatedBy");
            this.createdOnTextBox.DataBindings.Add("Text", this.snapshotUtilityDataSet.Tables[0], "CreatedOn");
            this.countTextBox.DataBindings.Add("Text", this.snapshotUtilityDataSet.Tables[0], "RecordCount");
        } */

        /// <summary>
        /// Clears the data binding.
        /// </summary>
        private void ClearDataBinding()
        {
            this.nameTextBox.DataBindings.Clear();
            this.descriptionTextBox.DataBindings.Clear();
            this.createdOnTextBox.DataBindings.Clear();
            this.createdByTextBox.DataBindings.Clear();
            this.countTextBox.DataBindings.Clear();
        }

        /// <summary>
        /// Assign the values from dataset to datagridview.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.snapshotUtilityGridView.AllowUserToResizeColumns = false;
            this.snapshotUtilityGridView.AutoGenerateColumns = false;
            this.snapshotUtilityGridView.AllowUserToResizeRows = false;
            this.snapshotUtilityGridView.StandardTab = true;
            this.snapshotUtilityGridView.Columns[0].DataPropertyName = "SnapshotName";
            this.snapshotUtilityGridView.Columns[0].DisplayIndex = 0;
            this.snapshotUtilityGridView.Columns[1].DataPropertyName = "SnapshotNote";
            this.snapshotUtilityGridView.Columns[1].DisplayIndex = 1;
            this.snapshotUtilityGridView.Columns[2].DataPropertyName = "CreatedBy";
            this.snapshotUtilityGridView.Columns[2].DisplayIndex = 2;
            this.snapshotUtilityGridView.Columns[3].DataPropertyName = "CreatedOn";
            this.snapshotUtilityGridView.Columns[3].DisplayIndex = 3;
            this.snapshotUtilityGridView.Columns[4].DataPropertyName = "SnapshotID";
            this.snapshotUtilityGridView.Columns[4].DisplayIndex = 4;
            this.snapshotUtilityGridView.Columns[5].DataPropertyName = "RecordCount";
            this.snapshotUtilityGridView.Columns[5].DisplayIndex = 5;
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private int GetRowIndex()
        {
            if (this.snapshotUtilityDataSet.Tables[0].Rows.Count > 0)
            {
                if (this.snapshotUtilityGridView.SelectedRows.Count > 0)
                {
                    this.selected = this.snapshotUtilityGridView.SelectedRows[0].Index;
                }
                else if (this.snapshotUtilityGridView.SelectedCells.Count > 0)
                {
                    this.selected = this.snapshotUtilityGridView.CurrentCell.RowIndex;
                }
            }

            return this.selected;
        }

        /// <summary>
        /// Loads the query utility data grid from database.
        /// </summary>
        private void LoadSnapshotUtilityDataGrid()
        {
            // Assign the values from dataset to datagridview.
            this.CustomizeDataGrid();

            try
            {
                // Fetch the record from the database.
                this.snapshotUtilityDataSet = this.form9051Control.WorkItem.GetSnapshotUtilityList(this.snapshotUtilityFormID);

                this.recordCount = this.snapshotUtilityDataSet.ListSnapshot.Rows.Count;

                // Get the values in the datagrid control from dataset. 
                this.snapshotUtilityGridView.DataSource = this.snapshotUtilityDataSet.ListSnapshot;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }

            //// if no data then clear the text boex  after delete last record 
            if (this.recordCount == 0)
            {
                //// inoreder to  check dataset has value or not
                this.rowCount = true;
                ////this.validRow = false;
                this.ClearTextValues();
                this.DisableSnapshotUtilityControls();
            }

            // Clear the queryUtilityDataGridView.
            this.ClearDataBinding();

            this.currencyManager = (CurrencyManager)this.BindingContext[this.snapshotUtilityDataSet, this.snapshotUtilityDataSet.ListSnapshot.TableName];

            // Assign the value to the controls.
            // this.SetDataBinding();
            // TerraScanCommon.SetGridHeight(this.snapshotUtilityGridView, 5);

            this.SetCancelButton();

            if (this.snapshotUtilityDataSet.ListSnapshot.Rows.Count > 5)
            {
                this.snapshotVScrollBar.Enabled = true;
                this.snapshotVScrollBar.Visible = false;
            }
            else
            {
                this.snapshotVScrollBar.Enabled = false;
                this.snapshotVScrollBar.Visible = true;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the NewSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            if (this.newSnapshotUtilityButton.Enabled)
            {
                this.EnableSnapshotUtilityControl();
                this.buttonOperation = (int)ButtonOperation.New;
                this.ClearDataBinding();
                this.nameTextBox.Text = string.Empty;
                this.descriptionTextBox.Text = string.Empty;
                this.snapshotUtilityGridView.Enabled = false;

                // Assign the know value.
                this.GetValues();

                // Set the Buttons status for New Operation.
                // this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.NewMode));

                this.newSnapshotUtilityButton.Enabled = false;
                this.saveSnapshotUtilityButton.Enabled = true;
                this.cancelSnapshotUtilityButton.Enabled = true;
                this.deleteSnapshotUtilityButton.Enabled = false;

                this.SetCurrentFormButtons(ButtonOperation.New);
                //// this.SetCancelButton();
                //// Get the focus of the cursor to nameTextBox.
                this.nameTextBox.Focus();

                // Hilight the selected row.
                if (this.CheckRowCount())
                {
                    if (this.selectedRow >= 0)
                    {
                        //// this.snapshotUtilityGridView.Rows[this.selectedRow].Selected = false; 
                        this.snapshotUtilityGridView.Rows[0].Selected = false;
                        this.snapshotUtilityGridView.CurrentCell = null;
                    }
                }

                this.newSnapshotUtilityButton.Focus();
                this.valueChanged = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseQueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseQueryUtilityButton_Click(object sender, EventArgs e)
        {
            // Checks whether there is change in datset.
            if ((this.snapshotUtilityDataSet.HasChanges() && this.valueChanged) || this.buttonOperation == (int)ButtonOperation.New)
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            try
                            {
                                // Saves the records to dadabase.
                                this.SaveRecords();
                            }                            
                            catch (Exception ex)
                            {
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                            }

                            if (this.closingNow)
                            {
                                this.closeButton = true;
                                this.Close();
                            }
                            else
                            {
                                this.DialogResult = DialogResult.None;
                            }

                            this.closingNow = false;
                            break;
                        }

                    case DialogResult.No:
                        {
                            this.closeButton = true;
                            this.Close();
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            this.DialogResult = DialogResult.None;
                            this.SetFocus();
                            break;
                        }
                }

                /* if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.closeButton = true;
                    this.DialogResult = DialogResult.No;
                    this.Close();
                } */
            }
            else
            {
                this.closeButton = true;
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            // if (this.valueChanged)
            // {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.DialogResult = DialogResult.None;
                int rowSelected = 0;
                rowSelected = this.GetRowIndex();

                // Clear all changes.
                this.DiscardChages();
                this.valueChanged = false;
                this.snapshotUtilityGridView.Focus();
                this.SetDataGridViewPosition(rowSelected);
                this.SetDataBindingValue(rowSelected);

                if (this.recordCount == 0)
                {
                    this.snapshotUtilityGridView.Rows[0].Selected = false;
                    this.snapshotUtilityGridView.CurrentCell = null;
                    this.snapshotUtilityGridView.Enabled = false;
                }

                /************************
                 
                this.DialogResult = DialogResult.None;

                if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Clear all changes.
                    this.DiscardChages();
                    this.valueChanged = false;
                    this.snapshotUtilityGridView.Focus();
                    this.SetDataGridViewPosition(0);
                    this.SetDataBindingValue(0);
                    //// this.SetCancelButton();
                }
                else
                {
                    this.SetFocus();
                }
                
                *****************/
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            /*  }
             else
             {
                 this.DialogResult = DialogResult.None;

                 // Clear all changes.
                 this.DiscardChages();
                 this.valueChanged = false;
                 this.newSnapshotUtilityButton.Focus();
                 this.SetDataGridViewPosition(0);
             } */
        }

        /// <summary>
        /// Handles the Click event of the LoadSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LoadSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            if (this.CheckRowCount())
            {
                if (this.currencyManager.Position >= 0)
                {
                    int rowIndex = 0;
                    rowIndex = this.GetRowIndex();

                    // Gets the SnapShotId for the selected row.
                    this.SnapShotId = Convert.ToInt32(this.snapshotUtilityGridView.Rows[rowIndex].Cells["SnapshotUtilityID"].Value.ToString());
                    this.SnapshotName = this.nameTextBox.Text;
                    this.loadClicked = true;
                    this.SnapshotDescription = this.descriptionTextBox.Text;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("SnapshotUtilityDelete"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (this.currencyManager.Position >= 0 && this.currencyManager != null)
                    {
                        if (this.CheckRowCount())
                        {
                            int rowIndex = 0;
                            rowIndex = this.GetRowIndex();

                            /* Type formType = this.Owner.GetType();
                            PropertyInfo propertyInfo = formType.GetProperty("ParentSnapShotId");
                            propertyInfo.SetValue(this.Owner, Convert.ToInt32(this.snapshotUtilityGridView.Rows[rowIndex].Cells["SnapshotUtilityID"].Value.ToString()), null); */

                            // Gets the SnapShotId for the selected row.
                            this.SnapShotId = Convert.ToInt32(this.snapshotUtilityGridView.Rows[rowIndex].Cells["SnapshotUtilityID"].Value.ToString());

                            // Delete the record in the database.
                            this.form9051Control.WorkItem.DeleteSnapshotUtility(this.SnapShotId,TerraScanCommon.UserId);
                            this.LoadSnapshotUtilityDataGrid();

                            if (rowIndex == this.recordCount)
                            {
                                rowIndex--;
                            }

                            // this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.DeleteMode));

                            this.saveSnapshotUtilityButton.Enabled = false;
                            this.cancelSnapshotUtilityButton.Enabled = false;

                            this.SetCurrentFormButtons(ButtonOperation.Delete);
                            this.SetDataGridViewPosition(0);
                            this.SetDataBindingValue(0);
                        }

                        if (this.recordCount == 0)
                        {
                            this.ClearTextValues();
                            this.DisableSnapshotUtilityControls();
                            this.snapshotUtilityGridView.Rows[0].Selected = false;
                            this.snapshotUtilityGridView.CurrentCell = null;
                            this.snapshotUtilityGridView.Enabled = false;
                        }

                        // this.SetCancelButton();
                        this.snapshotUtilityGridView.Focus();
                        this.valueChanged = false;
                    }
                }
                else
                {
                    this.nameTextBox.Focus();
                    this.nameTextBox.SelectAll();
                }
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the SnapshotUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    this.selectedRow = e.RowIndex;

                    if (!string.IsNullOrEmpty(this.snapshotUtilityGridView.Rows[e.RowIndex].Cells["SnapshotUtilityID"].Value.ToString()))
                    {
                        if (e.ColumnIndex >= -1)
                        {
                            // Place the cursor in the name textbox.
                            this.EnableSnapshotUtilityControl();
                        }

                        if (this.currencyManager != null && this.currencyManager.Position >= 0)
                        {
                            this.currencyManager.Position = e.RowIndex;
                            this.currencyManager.EndCurrentEdit();
                        }

                        // this.SetDataBindingValue(e.RowIndex);

                        // Checks whether any row has changes.
                        if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                        {
                            if (this.valueChanged)
                            {
                                if (this.tempRowId != e.RowIndex)
                                {
                                    switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                    {
                                        case DialogResult.Yes:
                                            {
                                                try
                                                {
                                                    this.gridEdit = true;
                                                    this.SaveRecords();

                                                    if (this.closingNow)
                                                    {
                                                        this.LoadSnapshotUtilityDataGrid();
                                                        this.tempRowId = e.RowIndex;
                                                        this.DiscardChages();
                                                        this.SetDataGridViewPosition(e.RowIndex);
                                                        this.SetDataBindingValue(e.RowIndex);
                                                        this.valueChanged = false;
                                                        this.EnableSnapshotUtilityControl();
                                                        this.nameTextBox.Focus();
                                                        this.nameTextBox.SelectAll();
                                                    }
                                                    else
                                                    {
                                                        this.SetDataGridViewPosition(this.tempRowId);                                                        
                                                    }
                                                }                                                
                                                catch (Exception ex)
                                                {
                                                   ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                                                }

                                                break;
                                            }

                                        case DialogResult.No:
                                            {
                                                this.tempRowId = e.RowIndex;
                                                this.DiscardChages();
                                                this.SetDataGridViewPosition(e.RowIndex);
                                                this.SetDataBindingValue(e.RowIndex);
                                                this.valueChanged = false;
                                                this.EnableSnapshotUtilityControl();
                                                this.snapshotUtilityGridView.Focus();
                                                break;
                                            }

                                        case DialogResult.Cancel:
                                            {
                                                this.SetDataGridViewPosition(this.tempRowId);
                                                this.SetDataBindingValue(this.tempRowId);
                                                this.TempData();
                                                this.SetFocus();
                                                //// this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                                                this.newSnapshotUtilityButton.Enabled = false;
                                                this.saveSnapshotUtilityButton.Enabled = true;
                                                this.cancelSnapshotUtilityButton.Enabled = true;

                                                this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                                                break;
                                            }
                                    }

                                    /***************
                                    // this.tempRowId = e.RowIndex;
                                    if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        this.tempRowId = e.RowIndex;
                                        this.DiscardChages();
                                        this.SetDataGridViewPosition(this.selectedRow);
                                        this.SetDataBindingValue(this.selectedRow);
                                        this.valueChanged = false;
                                        this.EnableSnapshotUtilityControl();
                                        this.nameTextBox.Focus();
                                        this.nameTextBox.SelectAll();

                                        // this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                                        // this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                                    }
                                    else
                                    {
                                        this.SetDataGridViewPosition(this.tempRowId);
                                        this.SetDataBindingValue(this.tempRowId);
                                        this.SetFocus();

                                        // this.loadSnapshotUtilityButton.Enabled = false;
                                        // this.deleteSnapshotUtilityButton.Enabled = false;
                                        SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                                        this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                                    }
                                     * *****************/
                                }
                            }
                            else
                            {
                                this.tempRowId = e.RowIndex;
                                this.SetDataBindingValue(e.RowIndex);
                            }
                        }

                        if (!this.valueChanged)
                        {
                            this.InitializeButton();
                        }
                    }
                    else
                    {
                        this.DisableSnapshotUtilityControls();
                        this.SetCurrentFormButtons(ButtonOperation.EmptyGirdRecord);
                    }
                }
            }            
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.saveSnapshotUtilityButton.Enabled)
                {
                    // Save the record to Database.
                    this.SaveRecords();
                }
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Enter event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_Enter(object sender, EventArgs e)
        {
            this.textBoxFocused = this.descriptionTextBox.Name;
        }

        /// <summary>
        /// Handles the Enter event of the NameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NameTextBox_Enter(object sender, EventArgs e)
        {
            this.textBoxFocused = this.nameTextBox.Name;
        }

        /// <summary>
        /// Handles the FormClosing event of the SnapshotUtilityForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
            {
                if (e.CloseReason.Equals(CloseReason.UserClosing))
                {
                    if (!this.loadClicked)
                    {
                        if (this.closeButton != true)
                        {
                            // Checks whether there is change in datset.
                            if (this.valueChanged || this.buttonOperation == (int)ButtonOperation.New)
                            {
                                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    case DialogResult.Yes:
                                        {
                                            try
                                            {
                                                // Saves the records to dadabase.
                                                this.SaveRecords();
                                            }                                            
                                            catch (Exception ex)
                                            {
                                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                                            }

                                            if (this.closingNow)
                                            {
                                                this.closeButton = true;
                                                this.Close();
                                            }
                                            else
                                            {
                                                this.DialogResult = DialogResult.None;
                                            }

                                            this.closingNow = false;
                                            break;
                                        }

                                    case DialogResult.No:
                                        {
                                            this.closeButton = true;
                                            this.Close();
                                            break;
                                        }

                                    case DialogResult.Cancel:
                                        {
                                            this.DialogResult = DialogResult.None;
                                            this.SetFocus();
                                            break;
                                        }
                                }

                                /* if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    this.DialogResult = DialogResult.No;
                                    e.Cancel = false;
                                }
                                else
                                {
                                    e.Cancel = true;
                                } */
                            }
                            else
                            {
                                this.DialogResult = DialogResult.No;
                                e.Cancel = false;
                            }
                        }
                        else
                        {
                            this.DialogResult = DialogResult.No;
                            e.Cancel = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the NameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    {
                        break;
                    }

                case Keys.Delete:
                    {
                        this.valueChanged = true;

                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            ////  this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newSnapshotUtilityButton.Enabled = false;
                            this.saveSnapshotUtilityButton.Enabled = true;
                            this.cancelSnapshotUtilityButton.Enabled = true;

                            this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    {
                        break;
                    }

                case Keys.Delete:
                    {
                        this.valueChanged = true;

                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            // this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newSnapshotUtilityButton.Enabled = false;
                            this.saveSnapshotUtilityButton.Enabled = true;
                            this.cancelSnapshotUtilityButton.Enabled = true;

                            this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the NameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.tempName = this.nameTextBox.Text;
        }

        /// <summary>
        /// Handles the KeyUp event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.tempDescription = this.descriptionTextBox.Text;
        }

        /// <summary>
        /// Handles the KeyDown event of the SnapshotUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.valueChanged)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            this.GridCancel(e);

                            break;
                        }

                    case Keys.Up:
                        {
                            this.GridCancel(e);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Grids the cancel.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GridCancel(KeyEventArgs e)
        {
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        try
                        {
                            this.SaveRecords();
                        }
                        catch (Exception ex)
                        {
                            ////MessageBox.Show(ex.Message);
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        }

                        if (this.closingNow)
                        {
                            this.SetDataBindingValue(this.tempRowId);
                            e.Handled = false;
                            this.valueChanged = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }

                        break;
                    }

                case DialogResult.No:
                    {
                        this.SetDataBindingValue(this.tempRowId);
                        e.Handled = false;
                        this.valueChanged = false;
                        //// this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));

                        this.newSnapshotUtilityButton.Enabled = false;
                        this.saveSnapshotUtilityButton.Enabled = true;
                        this.cancelSnapshotUtilityButton.Enabled = true;

                        this.SetCurrentFormButtons(ButtonOperation.Cancel);
                        this.snapshotUtilityGridView.Focus();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        this.SetFocus();
                        e.Handled = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the SnapshotUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.valueChanged)
            {
                if (e.RowIndex >= -1 && e.ColumnIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.snapshotUtilityGridView.Rows[e.RowIndex].Cells["SnapshotCount"].Value.ToString()))
                    {
                        this.selectedRow = e.RowIndex;
                        this.tempRowId = e.RowIndex;
                        this.SetDataBindingValue(e.RowIndex);

                        if (!this.valueChanged)
                        {
                            this.InitializeButton();
                        }
                    }
                    else
                    {
                        this.DisableSnapshotUtilityControls();
                        this.ClearTextValues();
                        this.snapshotUtilityGridView.CurrentCell = null;
                        ////this.SetCurrentFormButtons(ButtonOperation.EmptyGirdRecord);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the SnapShotUtility control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapShotUtility_Load(object sender, EventArgs e)
        {
            this.NewMenu.Click += new EventHandler(this.NewSnapshotUtilityButton_Click);
            this.SaveMenu.Click += new EventHandler(this.SaveSnapshotUtilityButton_Click);
        }

        /// <summary>
        /// Handles the KeyPress event of the NameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        e.Handled = true;
                        break;
                    }

                default:
                    {
                        this.valueChanged = true;

                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            ////  this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newSnapshotUtilityButton.Enabled = false;
                            this.saveSnapshotUtilityButton.Enabled = true;
                            this.cancelSnapshotUtilityButton.Enabled = true;

                            this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        e.Handled = true;
                        break;
                    }

                default:
                    {
                        this.valueChanged = true;

                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            // this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newSnapshotUtilityButton.Enabled = false;
                            this.saveSnapshotUtilityButton.Enabled = true;
                            this.cancelSnapshotUtilityButton.Enabled = true;

                            this.SetCurrentFormButtons(ButtonOperation.SnapshotUtilityDataGrid);
                        }

                        break;
                    }
            }
        }

       /*  /// <summary>
        /// Publication for SetButton
        /// </summary>
        [EventPublication("topic://Terrascan.UI.CAB/Modules/SetButtons", PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Enum>> SetButtons; */

        #endregion
    }
}