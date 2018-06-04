//--------------------------------------------------------------------------------------------
// <copyright file="F9050.cs" company="Congruent">
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
    /// QueryUtility Form class
    /// </summary>
    public partial class F9050 : Form
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
        /// controller F1104
        /// </summary>
        private F9050Controller form9050Control;        

        /// <summary>
        /// Created Boolean value for ValueChanged.
        /// </summary>
        private bool valueChanged;

        /// <summary>
        /// Created Boolean value.
        /// </summary>
        private bool loadClicked;

        /// <summary>
        /// Created Boolean value.
        /// </summary>
        private bool closingNow;

        /// <summary>
        /// Created Boolean value.
        /// </summary>
        private bool gridEdit;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int savedSuccessfully;

        /// <summary>
        /// Created Integer for recordCount.
        /// </summary>
        private int recordCount;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// Created string for textBoxFocused
        /// </summary>
        private string textBoxFocused = string.Empty;

        /// <summary>
        /// Created string for tempName
        /// </summary>
        private string tempName = string.Empty;

        /// <summary>
        /// Created string for tempDescription
        /// </summary>
        private string tempDescription = string.Empty;

        /// <summary>
        /// Created Integer for FormID
        /// </summary>
        private int queryUtilityFormID;

        /// <summary>
        /// Created Integer for QueryID
        /// </summary>
        private int queryId;

        /// <summary>
        /// Created Integer to get Selected Row Index.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// set boolean Value to false
        /// </summary>
        private bool closeButton;       

        /// <summary>
        /// Created for Currency Manager
        /// </summary>
        private CurrencyManager currencyManager;

        /// <summary>
        /// Created string for whereCondition
        /// </summary>
        private string whereCondition = string.Empty;

        /// <summary>
        /// Created string for FormID
        /// </summary>
        private string userWhereCondition = string.Empty;

        /// <summary>
        /// Created int variable to Find Button Operation
        /// </summary>
        private int buttonOperation;

        /// <summary>
        /// Object for dataset created
        /// </summary>
        private QueryUtilityData queryUtilityDataSet = new QueryUtilityData();

        /// <summary>
        /// Created Boolean value for rowCount
        /// </summary>
        private bool rowCount;

        #endregion

        #region Construtor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:QueryUtilityForm"/> class.
        /// </summary>
        /// <param name="formID">The form ID.</param>
        /// <param name="whereValue">The where value.</param>
        /// <param name="userDefinedWhere">The user defined where.</param>
        public F9050(int formID, string whereValue, string userDefinedWhere)
        {
            this.InitializeComponent();

            // Assign the value fom Construtor to local variable
            this.WhereCondition = whereValue;
            this.userWhereCondition = userDefinedWhere;
            this.queryUtilityFormID = formID;
            this.Tag = formID;
            this.CancelButton = this.cancelQueryUtilityButton;
            this.newEditPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.newEditPictureBox.Height, this.newEditPictureBox.Width, "New/Edit", 28, 81, 128);
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
            /// QueryUtilityDataGrid = 4.
            /// </summary>
            QueryUtilityDataGrid = 4,

            /// <summary>
            /// EmptyGirdRecord = 5.
            /// </summary>
            EmptyGirdRecord = 5
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the query ID.
        /// </summary>
        /// <value>The query ID.</value>
        public int QueryId
        {
            get { return this.queryId; }
            set { this.queryId = value; }
        }

        /// <summary>
        /// Gets or sets the User where condition.
        /// </summary>
        /// <value>The User where condition.</value>
        public string UserWhereCondition
        {
            get { return this.userWhereCondition; }
            set { this.userWhereCondition = value; }
        }

        /// <summary>
        /// Gets or sets the where condition.
        /// </summary>
        /// <value>The where condition.</value>
        public string WhereCondition
        {
            get { return this.whereCondition; }
            set { this.whereCondition = value; }
        }

        /// <summary>
        /// For F1104Control
        /// </summary>
        [CreateNew]
        public F9050Controller Form9050Control
        {
            get { return this.form9050Control as F9050Controller; }
            set { this.form9050Control = value; }
        }       

        #endregion

        #region Methods

        /// <summary>
        /// the Form Permissions will be captured and Set the Button Action Modes
        /// </summary>
        /// <param name="e">OnLoad Even Args</param>
        protected override void OnLoad(EventArgs e)
        {
            // Calls the base page.
            base.OnLoad(e); 

            // Initialize the controls during form load.
            // this.DisableQueryUtilityControls();

            // Used to initialize the values during form load.
            this.GetValues();

            // Loads the record from database.
            this.LoadQueryUtilityDataGrid();

            if (this.recordCount > 0)
            {
                this.SetDataGridViewPosition(0);
                this.SetDataBindingValue(0);
            }

            // Used to initialize the Button state.
            this.InitializeButton();

            // Checks the row count in datagrid to clear the fields.
            if (this.recordCount == 0)
            {
                // this.queryUtilityGridView.Rows[0].Selected = false;
                this.ClearTextValues();

                // this.SetButtons(
            }

            this.rowCount = false;
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
            // Make the row in the grid as selected.
            if (this.recordCount > 0)
            {
                this.queryUtilityGridView.Rows[Convert.ToInt32(commentRow)].Selected = true;
                this.queryUtilityGridView.CurrentCell = this.queryUtilityGridView[0, Convert.ToInt32(commentRow)];
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private int GetRowIndex()
        {
            if (this.queryUtilityDataSet.Tables[0].Rows.Count > 0)
            {
                if (this.queryUtilityGridView.SelectedRows.Count > 0)
                {
                    this.selected = this.queryUtilityGridView.SelectedRows[0].Index;
                }
                else if (this.queryUtilityGridView.SelectedCells.Count > 0)
                {
                    this.selected = this.queryUtilityGridView.CurrentCell.RowIndex;
                }
            }

            return this.selected;
        }

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            if (this.cancelQueryUtilityButton.Enabled == false)
            {
                this.CancelButton = this.closeQueryUtilityButton;
            }
            else
            {
                this.CancelButton = this.cancelQueryUtilityButton;
            }
        }

        /// <summary>
        /// Checks the row count.
        /// </summary>
        /// <returns>Returns </returns>
        private bool CheckRowCount()
        {
            // Checks whether queryUtilityGridView is empty or not.
            if (this.queryUtilityGridView.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Used to find Required fields have value.
        /// </summary>
        /// <returns>Returns</returns>
        private bool RequiredField()
        {
            // Checks whether nameTextBox and whereConditionTextBox has values.
            if (this.nameTextBox.Text.Trim().Length > 0 && this.whereConditionTextBox.Text.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        /// Used to Initialize the query utility form controls.
        /// </summary>
        private void DisableQueryUtilityControls()
        {
            this.nameTextBox.Enabled = false;
            this.descriptionTextBox.Enabled = false;

            if (this.recordCount == 0)
            {
                this.createdOnTextBox.Enabled = false;
                this.createdByTextBox.Enabled = false;
                this.whereConditionTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// Gets the row count.
        /// </summary>
        /// <returns>return integer</returns>
        private int GetRowCount()
        {
            int count = 0;
            if (this.queryUtilityDataSet.Tables[0].Rows.Count > 0)
            {
                count = this.queryUtilityDataSet.Tables[0].Rows.Count;
            }

            return count;
        }

        /// <summary>
        /// Sets the data binding value.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetDataBindingValue(int rowId)
        {
            if (this.queryUtilityGridView.Rows.Count > 0)
            {
                if (rowId >= 0)
                {
                    this.nameTextBox.Text = this.queryUtilityGridView.Rows[rowId].Cells["QueryName"].Value.ToString();
                    this.createdByTextBox.Text = this.queryUtilityGridView.Rows[rowId].Cells["CreatedBy"].Value.ToString();
                    this.createdOnTextBox.Text = this.queryUtilityGridView.Rows[rowId].Cells["CreatedOn"].Value.ToString();
                    this.descriptionTextBox.Text = this.queryUtilityGridView.Rows[rowId].Cells["Description"].Value.ToString();
                    this.whereConditionTextBox.Text = this.queryUtilityGridView.Rows[rowId].Cells["WhereCondnSql"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Saves the records to dadabase.
        /// </summary>
        private void SaveRecords()
        {
            try
            {
                // Store the values in the control to a local variable.
                string queryName = this.nameTextBox.Text.Trim();
                string description = this.descriptionTextBox.Text.Trim();
                ////string createdBy = this.createdByTextBox.Text.Trim();
                ////string createdOn = this.createdOnTextBox.Text.Trim();
                int queryFormID = this.queryUtilityFormID;
                bool cancelClicked = false;
                int rowSelected = -1;
                int rowIndex = 0;

                // this.selected = this.queryUtilityGridView.SelectedRows[0].Cells[0].RowIndex;

                if (this.selectedRow >= 0 && this.CheckRowCount() && this.buttonOperation != (int)ButtonOperation.New)
                {
                    rowIndex = this.GetRowIndex();

                    if (!this.gridEdit)
                    {
                        // Gets the QueryID of selected row.
                        this.QueryId = Convert.ToInt32(this.queryUtilityGridView.Rows[rowIndex].Cells["SavedQueryId"].Value.ToString());
                    }
                    else
                    {
                        this.QueryId = Convert.ToInt32(this.queryUtilityGridView.Rows[this.tempRowId].Cells["SavedQueryId"].Value.ToString());
                    }
                }

                if (this.RequiredField())
                {
                    if (this.buttonOperation == (int)ButtonOperation.New)
                    {
                        // Inserts the value to database.
                        this.savedSuccessfully = this.form9050Control.WorkItem.InsertQueryUtility(0, queryName, queryFormID, description, TerraScanCommon.UserId, this.whereCondition, this.userWhereCondition, 0);
                        this.queryUtilityGridView.Enabled = true;
                        this.closingNow = true;
                    }
                    else
                    {
                        // Update the value to database.
                        this.savedSuccessfully = this.form9050Control.WorkItem.InsertQueryUtility(this.QueryId, queryName, queryFormID, description, TerraScanCommon.UserId, string.Empty, string.Empty, 0);
                        this.closingNow = true;
                    }

                    this.queryUtilityGridView.Focus();

                    // Checks the query name already exists.
                    while (this.savedSuccessfully == 1)
                    {
                        ////AlertForm alert = new AlertForm(SharedFunctions.GetResourceString("QueryUtilityTitle"), SharedFunctions.GetResourceString("OverwriteQuery"), SharedFunctions.GetResourceString("Overwrite1"));
                        if (MessageBox.Show(SharedFunctions.GetResourceString("OverwriteQuery") + SharedFunctions.GetResourceString("Overwrite1"), SharedFunctions.GetResourceString("QueryUtilityTitle"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            if (this.buttonOperation == (int)ButtonOperation.New)
                            {
                                // Inserts the value to database.
                                this.savedSuccessfully = this.form9050Control.WorkItem.InsertQueryUtility(0, queryName, queryFormID, description, TerraScanCommon.UserId, this.whereCondition, this.userWhereCondition, 1);
                                this.closingNow = true;
                            }
                            else
                            {
                                // Update the value to database.
                                this.savedSuccessfully = this.form9050Control.WorkItem.InsertQueryUtility(this.QueryId, queryName, queryFormID, description, TerraScanCommon.UserId, string.Empty, string.Empty, 1);
                                cancelClicked = false;
                                this.closingNow = true;

                                // Gets the row in which the value have to overwrited.                               
                                DataGridViewRowCollection rowCollection = this.queryUtilityGridView.Rows;
                                foreach (DataGridViewRow row in rowCollection)
                                {
                                    if (this.queryUtilityGridView.Rows[row.Index].Cells["QueryName"].Value.ToString() == queryName)
                                    {
                                        rowSelected = row.Index;
                                        break;
                                    }
                                }

                                if (rowSelected >= 0)
                                {
                                    // Hilight the selected row.
                                    this.SetDataGridViewPosition(rowSelected);
                                    this.SetDataBindingValue(rowSelected);
                                }
                            }

                            this.queryUtilityGridView.Focus();
                            continue;
                        }
                        else
                        {
                            this.savedSuccessfully = 0;
                            cancelClicked = true;
                            this.closingNow = false;
                            //// SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newQueryUtilityButton.Enabled = false;
                            this.saveQueryUtilityButton.Enabled = true;
                            this.cancelQueryUtilityButton.Enabled = true;
                            
                            this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
                            this.nameTextBox.Focus();
                            this.nameTextBox.Select(this.nameTextBox.Text.Length, this.nameTextBox.Text.Length + 1);
                        }
                    }

                    if (!cancelClicked)
                    {
                        this.LoadQueryUtilityDataGrid();

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
                        //// SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));
                                               
                        this.saveQueryUtilityButton.Enabled = false;
                        this.cancelQueryUtilityButton.Enabled = false;
                        
                        this.SetCurrentFormButtons(ButtonOperation.Cancel);
                        this.buttonOperation = (int)ButtonOperation.Empty;
                        //// this.DisableQueryUtilityControls();
                        //// this.selectedRow = 0;
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
        /// Clears the text values.
        /// </summary>
        private void ClearTextValues()
        {
            this.nameTextBox.Text = string.Empty;
            this.createdByTextBox.Text = string.Empty;
            this.createdOnTextBox.Text = string.Empty;
            this.descriptionTextBox.Text = string.Empty;
            this.whereConditionTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Discards the chages during Cancel Button Click.
        /// </summary>
        private void DiscardChages()
        {
            // Rejects all changes made in the dataset.
            this.queryUtilityDataSet.Tables[0].RejectChanges();

            //// this.DisableQueryUtilityControls();

          ////this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));
                        
            this.saveQueryUtilityButton.Enabled = false;
            this.cancelQueryUtilityButton.Enabled = false;
           
            this.SetCurrentFormButtons(ButtonOperation.Cancel);
            this.queryUtilityGridView.Enabled = true;
            this.currencyManager.Position = Convert.ToInt32("0");
            this.buttonOperation = (int)ButtonOperation.Empty;
            this.LoadQueryUtilityDataGrid();

            if (this.recordCount == 0)
            {
                this.ClearTextValues();
            }
        }

        /// <summary>
        /// Initializes the buttons during form load.
        /// </summary>
        private void InitializeButton()
        {
            if (string.Compare(this.whereCondition, string.Empty) == 0)
            {
                this.newQueryUtilityButton.Enabled = false;
            }
            else
            {
                // this.newQueryUtilityButton.Enabled = true && Convert.ToBoolean(this.newQueryUtilityButton.ActualPermission);
                this.newQueryUtilityButton.Enabled = true;
            }

            if (this.recordCount > 0)
            {
                /* this.deleteQueryUtilityButton.Enabled = true && Convert.ToBoolean(this.deleteQueryUtilityButton.ActualPermission);
                this.saveQueryUtilityButton.Enabled = true && Convert.ToBoolean(this.deleteQueryUtilityButton.ActualPermission);
                this.cancelQueryUtilityButton.Enabled = true && Convert.ToBoolean(this.deleteQueryUtilityButton.ActualPermission);
                this.loadQueryUtilityButton.Enabled = true; */

                this.deleteQueryUtilityButton.Enabled = true;
                ////this.saveQueryUtilityButton.Enabled = true;
                ////this.cancelQueryUtilityButton.Enabled = true;
                this.loadQueryUtilityButton.Enabled = true;
            }
            else
            {
                this.deleteQueryUtilityButton.Enabled = false;
                this.loadQueryUtilityButton.Enabled = false;
            }

            this.saveQueryUtilityButton.Enabled = false;
            this.cancelQueryUtilityButton.Enabled = false;
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
        /// Gets the values to Initialize during form load.
        /// </summary>
        private void GetValues()
        {
            // Gets the Username.
            this.createdByTextBox.Text = TerraScanCommon.UserName;

            // Used to load the current date.
            this.createdOnTextBox.Text = DateTime.Now.ToString(this.dateFormat);

            // Gets the formID.
            this.formIDLabel.Text = "9050";

            // Gets the Where Condition Value.
            this.whereConditionTextBox.Text = this.WhereCondition;
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
                        this.loadQueryUtilityButton.Enabled = false;
                        this.closeQueryUtilityButton.Enabled = false;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        if (string.Compare(this.whereCondition, string.Empty) == 0)
                        {
                            this.newQueryUtilityButton.Enabled = false;
                        }
                        else
                        {
                            // this.newQueryUtilityButton.Enabled = true && this.newQueryUtilityButton.ActualPermission;
                            this.newQueryUtilityButton.Enabled = true;
                        }

                        if (this.CheckRowCount())
                        {
                            // this.deleteQueryUtilityButton.Enabled = true && this.deleteQueryUtilityButton.ActualPermission;
                            this.deleteQueryUtilityButton.Enabled = true;
                            this.loadQueryUtilityButton.Enabled = true;
                        }
                        else
                        {
                            this.deleteQueryUtilityButton.Enabled = false;
                            this.loadQueryUtilityButton.Enabled = false;
                        }

                        this.closeQueryUtilityButton.Enabled = true;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.Delete:
                    {
                        if (string.Compare(this.whereCondition, string.Empty) == 0)
                        {
                            this.newQueryUtilityButton.Enabled = false;
                        }
                        else
                        {
                            // this.newQueryUtilityButton.Enabled = true && this.newQueryUtilityButton.ActualPermission;
                            this.newQueryUtilityButton.Enabled = true;
                        }

                        if (!this.rowCount)
                        {
                            // this.deleteQueryUtilityButton.Enabled = true && this.deleteQueryUtilityButton.ActualPermission;
                            this.deleteQueryUtilityButton.Enabled = true;
                            this.loadQueryUtilityButton.Enabled = true;
                        }
                        else
                        {
                            this.deleteQueryUtilityButton.Enabled = false;
                            this.loadQueryUtilityButton.Enabled = false;
                        }

                        this.closeQueryUtilityButton.Enabled = true;
                        this.rowCount = false;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.QueryUtilityDataGrid:
                    {
                        this.loadQueryUtilityButton.Enabled = false;
                        this.deleteQueryUtilityButton.Enabled = false;
                        this.closeQueryUtilityButton.Enabled = false;
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.EmptyGirdRecord:
                    {
                        if (string.Compare(this.whereCondition, string.Empty) == 0)
                        {
                            this.newQueryUtilityButton.Enabled = false;
                        }
                        else
                        {
                            this.newQueryUtilityButton.Enabled = true && this.newQueryUtilityButton.ActualPermission;
                        }

                        this.saveQueryUtilityButton.Enabled = false;
                        this.cancelQueryUtilityButton.Enabled = false;
                        this.deleteQueryUtilityButton.Enabled = false;
                        this.loadQueryUtilityButton.Enabled = false;
                        this.closeQueryUtilityButton.Enabled = true;
                        this.SetCancelButton();
                        break;
                    }
            }
        }

        /// <summary>
        /// Loads the query utility data grid from database.
        /// </summary>
        private void LoadQueryUtilityDataGrid()
        {
            // Assign the values from dataset to datagridview.
            this.CustomizeDataGrid();

            try
            {
                // Gets the value from database.
                this.queryUtilityDataSet = this.form9050Control.WorkItem.GetQueryUtilityList(this.queryUtilityFormID);

                this.recordCount = this.queryUtilityDataSet.ListQuery.Rows.Count;

                // Assign the values to queryUtilityGridView. 
                this.queryUtilityGridView.DataSource = this.queryUtilityDataSet.ListQuery;
            }
            catch (Exception ex)
            {
                //////TODO : Need to find specific exception and handle it.
                 ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }

            // Checks whether queryUtilityDataSet is null.
            if (this.recordCount <= 0)
            {
                this.rowCount = true;
                this.DisableQueryUtilityControls();
            }

            // Clear the queryUtilityDataGridView.
            this.ClearDataBinding();
                        
            // Set the currency manager position.
            this.currencyManager = (CurrencyManager)this.BindingContext[this.queryUtilityDataSet, this.queryUtilityDataSet.ListQuery.TableName];

            // Assign the value to the controls.
            // this.SetDataBinding();
            // TerraScanCommon.SetGridHeight(this.queryUtilityGridView, 5);
            this.SetCancelButton();

            if (this.queryUtilityDataSet.ListQuery.Rows.Count > 5)
            {
                this.queryVScrollBar.Enabled = true;
                this.queryVScrollBar.Visible = false;
            }
            else
            {
                this.queryVScrollBar.Enabled = false;
                this.queryVScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Clears the data binding.
        /// </summary>
        private void ClearDataBinding()
        {
            this.nameTextBox.DataBindings.Clear();
            this.descriptionTextBox.DataBindings.Clear();
            this.createdOnTextBox.DataBindings.Clear();
            this.createdByTextBox.DataBindings.Clear();
            this.whereConditionTextBox.DataBindings.Clear();
        }

        /// <summary>
        /// Assigning values to the datagrid column.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.queryUtilityGridView.AllowUserToResizeColumns = false;
            this.queryUtilityGridView.AutoGenerateColumns = false;
            this.queryUtilityGridView.AllowUserToResizeRows = false;
            this.queryUtilityGridView.StandardTab = true;
            this.queryUtilityGridView.Columns[0].DataPropertyName = "QueryName";
            this.queryUtilityGridView.Columns[0].DisplayIndex = 0;
            this.queryUtilityGridView.Columns[1].DataPropertyName = "Description";
            this.queryUtilityGridView.Columns[1].DisplayIndex = 1;
            this.queryUtilityGridView.Columns[2].DataPropertyName = "CreatedBy";
            this.queryUtilityGridView.Columns[2].DisplayIndex = 2;
            this.queryUtilityGridView.Columns[3].DataPropertyName = "CreatedOn";
            this.queryUtilityGridView.Columns[3].DisplayIndex = 3;
            this.queryUtilityGridView.Columns[4].DataPropertyName = "QueryID";
            this.queryUtilityGridView.Columns[4].DisplayIndex = 4;
            this.queryUtilityGridView.Columns[5].DataPropertyName = "WhereCondnSql";
            this.queryUtilityGridView.Columns[5].DisplayIndex = 5;
            this.queryUtilityGridView.Columns[6].DataPropertyName = "WhereCondn";
            this.queryUtilityGridView.Columns[6].DisplayIndex = 6;
        }

        /* /// <summary>
        /// Sets the data binding to corresponding textbox.
        /// </summary>
        private void SetDataBinding()
        {
            this.nameTextBox.DataBindings.Add("Text", this.queryUtilityDataSet.Tables[0], "QueryName",true);
            this.descriptionTextBox.DataBindings.Add("Text", this.queryUtilityDataSet.Tables[0], "Description",true);
            this.createdByTextBox.DataBindings.Add("Text", this.queryUtilityDataSet.Tables[0], "CreatedBy",true);
            this.createdOnTextBox.DataBindings.Add("Text", this.queryUtilityDataSet.Tables[0], "CreatedOn",true);
            this.whereConditionTextBox.DataBindings.Add("Text", this.queryUtilityDataSet.Tables[0], "WhereCondnSql",true);
        } */

        /// <summary>
        /// Enables the query utility control.
        /// </summary>
        private void EnableQueryUtilityControl()
        {
            this.nameTextBox.BackColor = System.Drawing.Color.White;
            this.nameTextBox.Enabled = true;
            this.descriptionTextBox.BackColor = System.Drawing.Color.White;
            this.descriptionTextBox.Enabled = true;
            this.createdOnTextBox.Enabled = true;
            this.createdByTextBox.Enabled = true;
            this.whereConditionTextBox.Enabled = true;
        }

        /// <summary>
        /// FocusNameTextBox
        /// </summary>
        /// <param name="columnIndex">columnIndex</param>
        private void FocusNameTextBox(int columnIndex)
        {
            if (columnIndex >= -1)
            {
                this.EnableQueryUtilityControl();
                this.nameTextBox.Focus();

                // this.nameTextBox.Select(0, 0);
                this.nameTextBox.SelectAll();
                this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 121);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the CloseQueryUtilityButton control and closes query utility window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseQueryUtilityButton_Click(object sender, EventArgs e)
        {
            // Checks the dataset or controls in the form has any changes.
            if ((this.queryUtilityDataSet.HasChanges() && this.valueChanged) || this.buttonOperation == (int)ButtonOperation.New)
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
                            this.SetFocus();
                            break;
                        }
                }

                /* if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
        /// Handles the Click event of the NewQueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewQueryUtilityButton_Click(object sender, EventArgs e)
        {
            if (this.newQueryUtilityButton.Enabled)
            {
                this.EnableQueryUtilityControl();
                this.buttonOperation = (int)ButtonOperation.New;
                this.ClearDataBinding();
                this.nameTextBox.Text = string.Empty;
                this.descriptionTextBox.Text = string.Empty;
                this.queryUtilityGridView.Enabled = false;

                // Assign the know value.
                this.GetValues();

                // Set the Buttons status for New Operation.
                // SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.NewMode));

                this.newQueryUtilityButton.Enabled = false;
                this.saveQueryUtilityButton.Enabled = true;
                this.cancelQueryUtilityButton.Enabled = true;
                this.deleteQueryUtilityButton.Enabled = false;

                // Set the CurrentFormButtons according to the condition.
                this.SetCurrentFormButtons(ButtonOperation.New);

                // Get the focus of the cursor to nameTextBox.
                this.nameTextBox.Focus();

                if (this.CheckRowCount())
                {
                    if (this.selectedRow >= 0)
                    {
                        // Make the first row as selected.
                        this.queryUtilityGridView.Rows[this.selectedRow].Selected = false;
                        this.queryUtilityGridView.CurrentCell = null;
                    }
                }

                this.newQueryUtilityButton.Focus();
                this.valueChanged = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveQueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveQueryUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.saveQueryUtilityButton.Enabled)
                {
                    // Saves the records to dadabase.
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
        /// Handles the Click event of the CancelQueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelQueryUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.DialogResult = DialogResult.None;
                int rowSelected = 0;
                rowSelected = this.GetRowIndex();
                this.DiscardChages();
                this.valueChanged = false;

                if (this.recordCount == 0)
                {
                    this.queryUtilityGridView.Rows[0].Selected = false;
                    this.queryUtilityGridView.CurrentCell = null;
                    this.queryUtilityGridView.Enabled = false;
                }
                else
                {
                    this.SetDataGridViewPosition(rowSelected);
                    this.SetDataBindingValue(rowSelected);
                }

                this.queryUtilityGridView.Focus();

                /*************
                this.DialogResult = DialogResult.None;

                if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.DiscardChages();
                    this.valueChanged = false;
                    this.queryUtilityGridView.Focus();
                    this.SetDataGridViewPosition(0);
                    this.SetDataBindingValue(0);
                }
                else
                {
                    this.SetFocus();
                }
                 * *****************/
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            /* }
            else
            {
                this.DialogResult = DialogResult.None;
                this.DiscardChages();
                this.valueChanged = false;
                this.newQueryUtilityButton.Focus();
                this.SetDataGridViewPosition(0);
            } */
        }

        /// <summary>
        /// Handles the Click event of the LoadQueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LoadQueryUtilityButton_Click(object sender, EventArgs e)
        {
            if (this.CheckRowCount())
            {
                if (this.currencyManager.Position >= 0)
                {
                    int rowIndex = 0;
                    rowIndex = this.GetRowIndex();

                    // Gets the QueryID of selected Row.                
                    this.QueryId = Convert.ToInt32(this.queryUtilityGridView.Rows[rowIndex].Cells["SavedQueryId"].Value.ToString());
                    this.WhereCondition = this.whereConditionTextBox.Text;
                    this.loadClicked = true;
                    this.UserWhereCondition = this.queryUtilityGridView.Rows[rowIndex].Cells["UserWhereCondn"].Value.ToString();
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteQueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteQueryUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("QueryUtilityDelete"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (this.currencyManager.Position >= 0 && this.currencyManager != null)
                    {
                        if (this.CheckRowCount())
                        {
                            int rowIndex = 0;
                            rowIndex = this.GetRowIndex();

                            // Gets the QueryID of selected Row.
                            this.QueryId = Convert.ToInt32(this.queryUtilityGridView.Rows[rowIndex].Cells["SavedQueryId"].Value.ToString());

                            // Deleted the selected row.
                            this.form9050Control.WorkItem.DeleteQueryUtility(this.QueryId,TerraScanCommon.UserId);
                            this.LoadQueryUtilityDataGrid();

                            if (rowIndex == this.recordCount)
                            {
                                rowIndex--;
                            }

                            // SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.DeleteMode));
                                                        
                            this.saveQueryUtilityButton.Enabled = false;
                            this.cancelQueryUtilityButton.Enabled = false;
                           
                            this.SetCurrentFormButtons(ButtonOperation.Delete);
                            this.SetDataGridViewPosition(0);
                            this.SetDataBindingValue(0);

                            ////Below Line is added .  Reassign to selected row Inorder  avoid after delete new button Click
                            this.selectedRow = rowIndex;
                        }
                    }

                    if (this.recordCount == 0)
                    {
                        this.ClearTextValues();
                        this.DisableQueryUtilityControls();
                        this.queryUtilityGridView.Rows[0].Selected = false;
                        this.queryUtilityGridView.CurrentCell = null;
                        this.queryUtilityGridView.Enabled = false;
                    }

                    this.queryUtilityGridView.Focus();
                    this.valueChanged = false;
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
        /// Handles the CellClick event of the QueryUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void QueryUtilityGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    this.selectedRow = e.RowIndex;

                    // Checks whether Header is selected or not.
                    if (!string.IsNullOrEmpty(this.queryUtilityGridView.Rows[e.RowIndex].Cells["SavedQueryId"].Value.ToString()))
                    {
                        if (e.ColumnIndex >= -1)
                        {
                            // Place the cursor in the name textbox.                            
                            this.EnableQueryUtilityControl();

                            // this.FocusNameTextBox(e.ColumnIndex);

                            /* this.nameTextBox.Focus();
                            this.nameTextBox.SelectAll();
                            this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 121); */
                        }

                        // Assign the currencyManager Position for selected row.
                        if (this.currencyManager != null && this.currencyManager.Position >= 0)
                        {
                            this.currencyManager.Position = e.RowIndex;
                            this.currencyManager.EndCurrentEdit();
                        }

                        if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                        {
                            // Checks the value chaged in Datagrid.
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
                                                        this.LoadQueryUtilityDataGrid();
                                                        this.tempRowId = e.RowIndex;
                                                        this.DiscardChages();
                                                        this.SetDataGridViewPosition(e.RowIndex);
                                                        this.SetDataBindingValue(e.RowIndex);
                                                        this.valueChanged = false;
                                                        this.EnableQueryUtilityControl();
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
                                                this.EnableQueryUtilityControl();
                                                this.queryUtilityGridView.Focus();
                                                break;
                                            }

                                        case DialogResult.Cancel:
                                            {
                                                this.SetDataGridViewPosition(this.tempRowId);
                                                this.SetDataBindingValue(this.tempRowId);
                                                this.TempData();
                                                //// SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                                                this.newQueryUtilityButton.Enabled = false;
                                                this.saveQueryUtilityButton.Enabled = true;
                                                this.cancelQueryUtilityButton.Enabled = true;
                                               
                                                this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
                                                this.nameTextBox.Focus();
                                                this.nameTextBox.SelectAll();
                                                break;
                                            }
                                    }

                                    /***************
                                    if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        this.tempRowId = e.RowIndex;
                                        this.DiscardChages();
                                        this.SetDataGridViewPosition(this.selectedRow);
                                        this.SetDataBindingValue(this.selectedRow);
                                        this.valueChanged = false;
                                        this.EnableQueryUtilityControl();
                                        this.nameTextBox.Focus();
                                        this.nameTextBox.SelectAll();

                                        // SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                                        // this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
                                    }
                                    else
                                    {
                                        this.SetDataGridViewPosition(this.tempRowId);
                                        this.SetDataBindingValue(this.tempRowId);
                                        this.SetFocus();

                                        // this.loadQueryUtilityButton.Enabled = false;
                                        // this.deleteQueryUtilityButton.Enabled = false;
                                        SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                                        this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
                                    }
                                     * ***************/
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
                        this.DisableQueryUtilityControls();
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
        /// Handles the Enter event of the NameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NameTextBox_Enter(object sender, EventArgs e)
        {
            this.textBoxFocused = this.nameTextBox.Name;
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
        /// Handles the FormClosing event of the QueryUtilityForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void QueryUtilityForm_FormClosing(object sender, FormClosingEventArgs e)
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

                                /* if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    this.DialogResult = DialogResult.No;
                                    e.Cancel = false;
                                }
                                else
                                {
                                    this.DialogResult = DialogResult.No;
                                    e.Cancel = true;
                                } */
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
                            //// SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newQueryUtilityButton.Enabled = false;
                            this.saveQueryUtilityButton.Enabled = true;
                            this.cancelQueryUtilityButton.Enabled = true;
                           
                            this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
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
                            // SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newQueryUtilityButton.Enabled = false;
                            this.saveQueryUtilityButton.Enabled = true;
                            this.cancelQueryUtilityButton.Enabled = true;
                           
                            this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
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
        /// Handles the KeyDown event of the QueryUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void QueryUtilityGridView_KeyDown(object sender, KeyEventArgs e)
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
                        this.SaveRecords();
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
                        
                        this.saveQueryUtilityButton.Enabled = false;
                        this.cancelQueryUtilityButton.Enabled = false;
                       
                        this.SetCurrentFormButtons(ButtonOperation.Cancel);
                        this.queryUtilityGridView.Focus();
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
        /// Handles the RowEnter event of the QueryUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void QueryUtilityGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.valueChanged)
            {
                if (e.RowIndex >= -1 && e.ColumnIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.queryUtilityGridView.Rows[e.RowIndex].Cells["SavedQueryId"].Value.ToString()))
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
                        this.DisableQueryUtilityControls();
                        this.ClearTextValues();
                        this.queryUtilityGridView.CurrentCell = null;
                        //// this.SetCurrentFormButtons(ButtonOperation.EmptyGirdRecord);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the QueryUtility control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryUtility_Load(object sender, EventArgs e)
        {
            this.NewMenu.Click += new EventHandler(this.NewQueryUtilityButton_Click);
            this.SaveMenu.Click += new EventHandler(this.SaveQueryUtilityButton_Click);
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
                            //// SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newQueryUtilityButton.Enabled = false;
                            this.saveQueryUtilityButton.Enabled = true;
                            this.cancelQueryUtilityButton.Enabled = true;

                            this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
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
                            // SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));

                            this.newQueryUtilityButton.Enabled = false;
                            this.saveQueryUtilityButton.Enabled = true;
                            this.cancelQueryUtilityButton.Enabled = true;

                            this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
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