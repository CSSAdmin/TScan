//--------------------------------------------------------------------------------------------
// <copyright file="F9040.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 May 07        Suganth Mani       Created
//*********************************************************************************/
namespace D9030
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using System.Configuration;
    using Infragistics.Win;
    using Infragistics.Documents.Excel;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using TerraScan.Helper;


    #endregion NameSpaces

    /// <summary>
    /// class for 9040
    /// </summary>
    public partial class F9040 : BasePage
    {
        #region Private Members

        /// <summary>
        /// controller F9038
        /// </summary>
        private F9040Controller form9040Control;

        /// <summary>
        /// pinned rows from queryutility
        /// </summary>
        private DataTable addedPinnedDataTable;

        /// <summary>
        /// unpinned rows from queryutility
        /// </summary>
        private DataTable addedUnPinnedDataTable;

        /// <summary>
        /// active form master no
        /// </summary>
        private int activeFormMaster;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        //// <summary>
        //// active queryview name from queryutility
        //// </summary>
        ////private string activeQueryViewName;

        /// <summary>
        /// permissions available at the form master
        /// </summary>
        private PermissionFields activeFormMasterPermissionFields;

        /// <summary>
        /// snapShot Details 
        /// </summary>
        private LoadSnapShotDetails loadSnapShotDetails;

        /// <summary>
        /// selected snapshot values
        /// </summary>
        private DataSet selectedSnapshotResult;

        /// <summary>
        /// the record index of the currently selected row
        /// </summary>
        private int currentRecordIndex;

        /// <summary>
        /// the entity of the sanpshot utility
        /// </summary>
        private F9040SnapShotUtilityData snapShotUtilityData;

        /// <summary>
        /// the queryview selected by the user
        /// </summary>
        private int selectedQueryView;

        /// <summary>
        /// The QueryView ID selected and passed from QE form.
        /// </summary>
        private int baseQueryViewId;

        /// <summary>
        /// the current page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flag to identify form load event
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// snapShotId selected
        /// </summary>
        private int selectedSnapShotId;

        /// <summary>
        /// Used to store the Current SnapShot Id
        /// </summary>
        private int currentSnapShotId;

        /// <summary>
        /// Used to store the currentSnapShotName
        /// </summary>
        private string currentSnapShotName = string.Empty;

        /// <summary>
        /// the selected userid
        /// </summary>
        private int selectedUsedId;

        /// <summary>
        /// currentKey
        /// </summary>
        private int currentKey;

        /// <summary>
        /// flag to select the default radio button
        /// </summary>
        private string defaultRadioButton;

        /// <summary>
        /// Used to check whether the form f9040 is loaded from Batch Button controls
        /// </summary>
        private bool batchButtonControlFormCall;

        /// <summary>
        /// Total Record count
        /// </summary>
        private int totalRecord;

        /// <summary>
        /// Filter XML 
        /// </summary>
        private string filterXML;

        /// <summary>
        /// Selected snapshot value
        /// </summary>
        private int snapShotValue;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9040"/> class.
        /// </summary>
        public F9040()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9040"/> class.
        /// </summary>
        /// <param name="pinnedDataTable">The pinned data table.</param>
        /// <param name="notPinnedDataTable">The not pinned data table.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="formMasterNo">The form master no.</param>
        /// <param name="queryViewName">Name of the query view.</param>
        /// <param name="formMasterPermissionFields">The form master permission fields.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="selectedSnapshotValue">The selected snapshot value.</param>
        public F9040(DataTable pinnedDataTable, DataTable notPinnedDataTable, int queryViewId, int formMasterNo, string queryViewName, PermissionFields formMasterPermissionFields, int recordCount, string filterValue, int selectedSnapshotValue)
        {
            this.InitializeComponent();
            this.addedPinnedDataTable = new DataTable();
            this.addedUnPinnedDataTable = new DataTable();
            this.addedPinnedDataTable = pinnedDataTable;
            this.addedUnPinnedDataTable = notPinnedDataTable;
            // this.selectedQueryView = queryViewId;
            this.baseQueryViewId = queryViewId;
            this.activeFormMaster = formMasterNo;
            ////this.activeQueryViewName = queryViewName;            
            this.activeFormMasterPermissionFields = formMasterPermissionFields;
            this.totalRecord = recordCount;
            this.filterXML = filterValue;
            this.snapShotValue = selectedSnapshotValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9040"/> class.
        /// </summary>
        /// <param name="formSliceNo">The form slice no.</param>
        /// <param name="parentFormSlicePermision">The parent form slice permision.</param>
        public F9040(int formSliceNo, PermissionFields parentFormSlicePermision)
        {
            this.InitializeComponent();
            /* ParentForm's FormSlice no*/
            this.activeFormMaster = formSliceNo;
            /* ParentForm's Formslice permision*/
            this.activeFormMasterPermissionFields = parentFormSlicePermision;
            /*to indicate the form is loaded from batch button controls*/
            this.batchButtonControlFormCall = true;
        }

        #endregion Constructor

        #region Event Publication
        /// <summary>
        /// Declare the Event for LoadSnapShot
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_LoadSnapShotDetails, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<LoadSnapShotDetails>> D9030_F9033_LoadSnapShotDetails;
        #endregion

        #region Properties

        /// <summary>
        /// For F9040Control
        /// </summary>
        [CreateNew]
        public F9040Controller Form9040Control
        {
            get { return this.form9040Control as F9040Controller; }
            set { this.form9040Control = value; }
        }

        /// <summary>
        /// Gets or sets the selected snapshot result.
        /// </summary>
        /// <value>The selected snapshot result.</value>
        public DataSet SelectedSnapshotResult
        {
            get { return this.selectedSnapshotResult; }
            set { this.selectedSnapshotResult = value; }
        }

        /// <summary>
        /// Gets or sets the selected query view.
        /// </summary>
        /// <value>The selected query view.</value>
        public int SelectedQueryView
        {
            get { return this.selectedQueryView; }
            set { this.selectedQueryView = value; }
        }

        /// <summary>
        /// Gets or sets the SnapShotId
        /// </summary>
        /// <value>The current snap shot id.</value>
        public int CurrentSnapShotId
        {
            get { return this.currentSnapShotId; }
            set { this.currentSnapShotId = value; }
        }

        /// <summary>
        /// Gets or sets the name of the current snap shot.
        /// </summary>
        /// <value>The name of the current snap shot.</value>
        public string CurrentSnapShotName
        {
            get { return this.currentSnapShotName; }
            set { this.currentSnapShotName = value; }
        }

        #endregion Properties

        #region Protected Methods
        /// <summary>
        /// Called when [D9030_ F9033_ load snap shot details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_LoadSnapShotDetails(TerraScan.Infrastructure.Interface.EventArgs<LoadSnapShotDetails> eventArgs)
        {
            if (this.D9030_F9033_LoadSnapShotDetails != null)
            {
                this.D9030_F9033_LoadSnapShotDetails(this, eventArgs);
            }
        }
        #endregion

        #region Private Events

        #region RadioButtonClickEvent

        /// <summary>
        /// Handles the Click event of the AllRowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AllRowRadioButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AllRowRadioButton.Checked)
                {
                    this.MergePinnedUnpinnedRows();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PinnedRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PinnedRadioButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.PinnedRadioButton.Checked)
                {
                    DataTable tempPinnedDataTable = new DataTable("tempPinnedDataTable");
                    tempPinnedDataTable = this.addedPinnedDataTable.Copy();
                    if (tempPinnedDataTable.Rows.Count > 0)
                    {
                        this.countTextBox.Text = tempPinnedDataTable.Rows.Count.ToString("#,###");
                    }
                    else
                    {
                        this.countTextBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the UnPinnedRowRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UnPinnedRowRadioButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.UnPinnedRowRadioButton.Checked)
                {
                    DataTable tempUnPinnedDataTable = new DataTable("tempPinnedDataTable");
                    tempUnPinnedDataTable = this.addedUnPinnedDataTable.Copy();
                    if (tempUnPinnedDataTable.Rows.Count > 0)
                    {
                        //this.countTextBox.Text = tempUnPinnedDataTable.Rows.Count.ToString("#,###");
                        int pinnedRows = this.addedPinnedDataTable.Rows.Count;
                        int unPinnedRows = this.totalRecord - pinnedRows;
                        this.countTextBox.Text = unPinnedRows.ToString("#,###");
                    }
                    else
                    {
                        this.countTextBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion RadioButtonClickEvent

        /// <summary>
        /// Handles the CellDoubleClick event of the snapshotUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    this.currentRecordIndex = e.RowIndex;
                    if (this.snapshotUtilityGridView.Rows[e.RowIndex].Cells[this.SnapshotID.Name].Value != null && !string.IsNullOrEmpty(this.snapshotUtilityGridView.Rows[e.RowIndex].Cells[this.SnapshotID.Name].Value.ToString()))
                    {
                        this.LoadSnapShot();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the newSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.newSnapshotUtilityButton.Enabled)
                {
                    this.selectedSnapShotId = 0;
                    this.selectedUsedId = TerraScanCommon.UserId;
                    ////this.selectedQueryView = this.activeQueryViewId;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                    this.ClearControlValues();
                    this.createdByTextBox.Text = TerraScanCommon.UserName;
                    this.createdOnTextBox.Text = DateTime.Now.Date.ToShortDateString();

                    if (!this.batchButtonControlFormCall)
                    {
                        this.MergePinnedUnpinnedRows();
                    }
                    this.SnapshotMgmtDataGrid.Rows.ColumnFilters.ClearAllFilters();
                    this.SnapshotMgmtDataGrid.Enabled = false;
                    //this.snapshotUtilityGridView.Enabled = false;
                    this.nameTextBox.Focus();
                    this.ControlLock(false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the saveSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.saveSnapshotUtilityButton.Enabled)
                {
                    if (string.IsNullOrEmpty(this.nameTextBox.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.SaveSnapshot();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the cancelSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.cancelSnapshotUtilityButton.DialogResult = DialogResult.None;
                this.CancelOperation();
                ////this.newSnapshotUtilityButton.Focus();
                //if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                //{
                ////focus is set to Name textbox
                this.ActiveControl = this.nameTextBox;
                this.nameTextBox.Focus();
                //}

                this.SetDefault();
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the deleteSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (this.currentRecordIndex < this.snapShotUtilityData.ListSnapShot.Rows.Count)
                {
                    //if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this Snapshot containing " + this.snapshotUtilityGridView[this.snapShotUtilityData.ListSnapShot.RecordCountColumn.ColumnName, this.currentRecordIndex].Value.ToString() + " records?", "TerraScan – Delete Snapshot", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this Snapshot containing " + this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Cells[this.snapShotUtilityData.ListSnapShot.RecordCountColumn.ColumnName].Value.ToString() + " records?", "TerraScan – Delete Snapshot", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    {
                        if (this.selectedSnapShotId != 0)
                        {
                            this.form9040Control.WorkItem.F9040_DeleteSnapShot(this.selectedSnapShotId, TerraScanCommon.UserId); // WorkItem.F9038_DeleteLayoutInformation(this.layoutId);
                            this.flagFormLoad = true;
                            this.LoadSnapShotGrid();
                            this.flagFormLoad = false;
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                            {
                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            }
                            else
                            {
                                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                            }

                            if (this.currentRecordIndex < this.snapShotUtilityData.ListSnapShot.Rows.Count)
                            {
                                this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                                // TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                            }
                            else
                            {
                                this.currentRecordIndex = 0;
                                if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                                {
                                    this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                }
                                else
                                {
                                    this.flagFormLoad = true;
                                    this.nameTextBox.Text = string.Empty;
                                    this.descriptionTextBox.Text = string.Empty;
                                    this.createdOnTextBox.Text = string.Empty;
                                    this.createdByTextBox.Text = string.Empty;
                                    this.countTextBox.Text = string.Empty;
                                    this.flagFormLoad = false;
                                }
                            }

                            if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                            {
                                ////focus is set to Name textbox
                                this.ActiveControl = this.nameTextBox;
                                this.nameTextBox.Focus();
                            }
                        }
                    }
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
        /// Handles the Click event of the closeQueryUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseQueryUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F9040 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9040_Load(object sender, EventArgs e)
        {
            try
            {
                this.snapShotUtilityData = new F9040SnapShotUtilityData();
                this.selectedSnapshotResult = new DataSet();
                //this.CustomizeDataGrid();
                this.CancelButton = this.cancelSnapshotUtilityButton;
                this.LoadSnapShotGrid();
                this.SetDefault();
                this.formNo = 9040;
                this.keyField = "SnapshotID";
                if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                {
                    if (this.snapShotValue > 0)
                    {
                        DataTable tempListSnapShotDataTable = this.snapShotUtilityData.ListSnapShot.Copy();
                        ////int.TryParse(this.snapshotUtilityGridView.Rows[rowIndex].Cells[this.SnapshotID.Name].Value.ToString(), out tempSnapshotId);
                        tempListSnapShotDataTable.DefaultView.Sort = this.SnapshotID.Name;
                        int tempCurrentRowIndex = tempListSnapShotDataTable.DefaultView.Find(this.snapShotValue);

                        if (tempCurrentRowIndex != -1)
                        {
                            this.SetControlValues((F9040SnapShotUtilityData.ListSnapShotRow)this.snapShotUtilityData.ListSnapShot.Rows[tempCurrentRowIndex]);
                            this.SnapshotMgmtDataGrid.Rows[tempCurrentRowIndex].Selected = true;
                            //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, tempCurrentRowIndex);
                        }
                        else
                        {
                            this.SetControlValues((F9040SnapShotUtilityData.ListSnapShotRow)this.snapShotUtilityData.ListSnapShot.Rows[0]);
                        }
                        ////this.SetControlValues(this.GetSelectedRow(this.snapShotValue));
                    }
                    else
                    {
                        this.nameTextBox.Text = string.Empty;
                        this.descriptionTextBox.Text = string.Empty;
                        this.createdByTextBox.Text = string.Empty;
                        this.createdOnTextBox.Text = string.Empty;
                        this.countTextBox.Text = string.Empty;
                        this.ControlLock(true);
                        this.SnapshotMgmtDataGrid.Selected.Rows.Clear();
                        this.SnapshotMgmtDataGrid.Selected.Cells.Clear();
                        //this.snapshotUtilityGridView.ClearSelection();
                    }

                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.EnableNullRecordMode(false);
                    if (this.currentRecordIndex >= 0)
                    {
                        if (this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected)
                        {
                            this.deleteSnapshotUtilityButton.Enabled = true;
                            this.loadSnapshotUtilityButton.Enabled = true;
                        }
                        else
                        {
                            this.deleteSnapshotUtilityButton.Enabled = false;
                            this.loadSnapshotUtilityButton.Enabled = false;
                        }
                    }
                    ////focus is set to Name textbox
                    this.ActiveControl = this.nameTextBox;
                    this.nameTextBox.Focus();

                }
                else
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    this.EnableNullRecordMode(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the loadSnapshotUtilityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LoadSnapshotUtilityButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                {
                    this.LoadSnapShot();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the snapshotUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.currentRecordIndex = e.RowIndex;

                    // this.LoadSnapShot();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Event event of the Edit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Edit_Event(object sender, EventArgs e)
        {
            if (!this.flagFormLoad)
            {
                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                }
            }
        }

        /// <summary>
        /// Handles the CellClick event of the snapshotUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad && e.RowIndex >= 0)
                {
                    this.ControlLock(false);
                    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                    {
                        DialogResult dialogResult;
                        dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + " " + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (e.RowIndex >= 0)
                            {
                                if (string.IsNullOrEmpty(this.nameTextBox.Text) || string.IsNullOrEmpty(this.descriptionTextBox.Text))
                                {
                                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    this.SaveSnapshot();
                                    this.currentRecordIndex = e.RowIndex;
                                    this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                    this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                                    TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                                }
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            this.currentRecordIndex = e.RowIndex;
                            this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        }
                        else
                        {
                            this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                            TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                            return;
                        }
                    }
                    else
                    {
                        if (e.RowIndex >= 0)
                        {
                            this.currentRecordIndex = e.RowIndex;
                            this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
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
        /// Handles the KeyDown event of the snapshotUtilityGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SnapshotUtilityGridView_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    int tempRowIndex = 0;
            //    int tempmiscGirdRowIndex = 0;
            //    this.ControlLock(false);
            //    tempRowIndex = ((DataGridView)sender).CurrentCell.RowIndex;

            //    switch (e.KeyCode)
            //    {
            //        case Keys.Down:
            //            {
            //                if ((tempRowIndex + 1) <= this.snapshotUtilityGridView.OriginalRowCount - 1)
            //                {
            //                    tempmiscGirdRowIndex = tempRowIndex + 1;

            //                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //                    {
            //                        this.currentRecordIndex = tempmiscGirdRowIndex;
            //                        this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                        this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                        // TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                        e.Handled = true;
            //                    }
            //                    else
            //                    {
            //                        DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            //                        if (dialogResult == DialogResult.Yes)
            //                        {
            //                            e.Handled = true;

            //                            if (string.IsNullOrEmpty(this.nameTextBox.Text) || string.IsNullOrEmpty(this.descriptionTextBox.Text))
            //                            {
            //                                MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                            }
            //                            else
            //                            {
            //                                this.SaveSnapshot();
            //                                this.currentRecordIndex = tempmiscGirdRowIndex;
            //                                this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                                this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                                // TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                            }
            //                        }
            //                        else if (dialogResult == DialogResult.No)
            //                        {
            //                            e.Handled = true;

            //                            this.currentRecordIndex = tempmiscGirdRowIndex;
            //                            this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                            this.pageMode = TerraScanCommon.PageModeTypes.View;
            //                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //                        }
            //                        else if (dialogResult == DialogResult.Cancel)
            //                        {
            //                            e.Handled = true;
            //                            this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                            //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                            return;
            //                        }
            //                    }
            //                }

            //                break;
            //            }

            //        case Keys.Up:
            //            {
            //                if ((tempRowIndex - 1) >= 0)
            //                {
            //                    tempmiscGirdRowIndex = tempRowIndex - 1;

            //                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //                    {
            //                        this.currentRecordIndex = tempmiscGirdRowIndex;
            //                        this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                        e.Handled = true;
            //                    }
            //                    else
            //                    {
            //                        DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            //                        if (dialogResult == DialogResult.Yes)
            //                        {
            //                            e.Handled = true;

            //                            if (string.IsNullOrEmpty(this.nameTextBox.Text) || string.IsNullOrEmpty(this.descriptionTextBox.Text))
            //                            {
            //                                MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                            }
            //                            else
            //                            {
            //                                this.SaveSnapshot();
            //                                this.currentRecordIndex = tempmiscGirdRowIndex;
            //                                this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                                this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                                //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                            }
            //                        }
            //                        else if (dialogResult == DialogResult.No)
            //                        {
            //                            e.Handled = true;

            //                            this.currentRecordIndex = tempmiscGirdRowIndex;
            //                            this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                            this.pageMode = TerraScanCommon.PageModeTypes.View;
            //                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //                        }
            //                        else if (dialogResult == DialogResult.Cancel)
            //                        {
            //                            e.Handled = true;
            //                            this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                            TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                            return;
            //                        }
            //                    }
            //                }

            //                break;
            //            }
            //    }
            //}
            //catch (Exception e1)
            //{
            //    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        /// <summary>
        /// Handles the FormClosing event of the F9040 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9040_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) && this.saveSnapshotUtilityButton.Enabled)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        if (this.SaveSnapShotDetails())
                                        {
                                            e.Cancel = false;
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                        }

                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        e.Cancel = false;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        e.Cancel = true;
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion Private Events

        #region Private Methods

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            try
            {
                if (this.cancelSnapshotUtilityButton.Enabled)
                {
                    this.CancelButton = this.cancelSnapshotUtilityButton;
                }
                else
                {
                    this.CancelButton = this.closeQueryUtilityButton;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            try
            {
                DataGridViewColumnCollection snapShotColumns = this.snapshotUtilityGridView.Columns;
                this.snapshotUtilityGridView.PrimaryKeyColumnName = this.snapShotUtilityData.ListSnapShot.SnapshotIDColumn.ColumnName;
                this.snapshotUtilityGridView.AutoGenerateColumns = false;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.SnapshotNameColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.SnapshotNameColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.SnapshotNoteColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.SnapshotNoteColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.Name_DisplayColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.Name_DisplayColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.InsertedDateColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.InsertedDateColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.SnapshotIDColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.SnapshotIDColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.RecordCountColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.RecordCountColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.UserIDColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.UserIDColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.QueryViewColumn.ColumnName].DataPropertyName = this.snapShotUtilityData.ListSnapShot.QueryViewColumn.ColumnName;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.SnapshotNameColumn.ColumnName].DisplayIndex = 0;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.SnapshotNoteColumn.ColumnName].DisplayIndex = 1;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.Name_DisplayColumn.ColumnName].DisplayIndex = 2;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.InsertedDateColumn.ColumnName].DisplayIndex = 3;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.SnapshotIDColumn.ColumnName].DisplayIndex = 4;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.RecordCountColumn.ColumnName].DisplayIndex = 5;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.UserIDColumn.ColumnName].DisplayIndex = 6;
                snapShotColumns[this.snapShotUtilityData.ListSnapShot.QueryViewColumn.ColumnName].DisplayIndex = 7;

                this.snapshotUtilityGridView.PrimaryKeyColumnName = this.snapShotUtilityData.ListSnapShot.SnapshotIDColumn.ColumnName;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the snap shot grid.
        /// </summary>
        private void LoadSnapShotGrid()
        {
            try
            {
                this.snapShotUtilityData.ListSnapShot.Rows.Clear();
                this.snapShotUtilityData.DefaultIncludeRows.Rows.Clear();
                if (this.batchButtonControlFormCall)
                {
                    this.snapShotUtilityData = this.form9040Control.WorkItem.F9040_ListBatchButtonSnapShots(this.activeFormMaster);
                }
                else
                {
                    this.snapShotUtilityData = this.form9040Control.WorkItem.F9040_ListSnapShots(this.activeFormMaster);
                }
                this.SnapshotMgmtDataGrid.DataSource = this.snapShotUtilityData.ListSnapShot.Copy();
                //this.snapshotUtilityGridView.DataSource = this.snapShotUtilityData.ListSnapShot.Copy();
                //this.SnapshotMgmtDataGrid
                //if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 8)
                //{
                //    this.snapshotVScrollBar.Enabled = false;
                //    this.snapshotVScrollBar.Visible = false;
                //}
                //else
                //{
                //    this.snapshotVScrollBar.Enabled = false;
                //    this.snapshotVScrollBar.Visible = true;
                //}
                if (this.SnapshotMgmtDataGrid.Rows.Count > 8)
                {
                    this.SnapshotMgmtDataGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
                    this.SnapshotMgmtDataGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
                }
                else
                {
                    this.SnapshotMgmtDataGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                    this.SnapshotMgmtDataGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the selected row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>F9040SnapShotUtilityData.ListSnapShotRow</returns>
        private F9040SnapShotUtilityData.ListSnapShotRow GetSelectedRow(int rowIndex)
        {
            int tempSnapshotId;
            int tempCurrentRowIndex = -1;

            /*here when grid is sorted the row index of the grid and datatable will change, 
             * so apporiate row index of the datatable for current row is find using snapshot id 
             * of the grid for current selected row in the grid
             * */

            DataTable tempListSnapShotDataTable = this.snapShotUtilityData.ListSnapShot.Copy();
            int.TryParse(this.SnapshotMgmtDataGrid.Rows[rowIndex].Cells[this.SnapshotID.Name].Value.ToString(), out tempSnapshotId);
            // tempListSnapShotDataTable.DefaultView.Sort = this.SnapshotID.Name;
            //tempCurrentRowIndex = tempListSnapShotDataTable.DefaultView.Find(tempSnapshotId);
            DataRow[] row = tempListSnapShotDataTable.Select("SnapshotID=" + tempSnapshotId);
            if (row.Length > 0)
            {
                tempCurrentRowIndex = tempListSnapShotDataTable.Rows.IndexOf(row[0]);
            }

            if (tempCurrentRowIndex != -1)
            {
                //this.snapshotUtilityGridView.Rows[tempCurrentRowIndex].Selected = true;
                //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, tempCurrentRowIndex);
                //return (F9040SnapShotUtilityData.ListSnapShotRow)this.snapShotUtilityData.ListSnapShot.Rows[tempCurrentRowIndex];
                this.SnapshotMgmtDataGrid.Rows[rowIndex].Selected = true;
                //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, rowIndex);
                return (F9040SnapShotUtilityData.ListSnapShotRow)this.snapShotUtilityData.ListSnapShot.Rows[tempCurrentRowIndex];
            }
            else
            {
                return (F9040SnapShotUtilityData.ListSnapShotRow)this.snapShotUtilityData.ListSnapShot.Rows[rowIndex];
            }
        }

        /// <summary>
        /// Sets the control values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetControlValues(F9040SnapShotUtilityData.ListSnapShotRow selectedRow)
        {
            this.flagFormLoad = true;

            this.nameTextBox.Text = selectedRow.SnapshotName;
            ////used to get the name of the snapshot
            this.currentSnapShotName = selectedRow.SnapshotName;
            this.descriptionTextBox.Text = selectedRow.SnapshotNote;
            this.createdByTextBox.Text = selectedRow.Name_Display;
            this.createdOnTextBox.Text = selectedRow.InsertedDate.ToShortDateString();
            this.countTextBox.Text = selectedRow.RecordCount.ToString();
            this.selectedQueryView = selectedRow.QueryView;
            this.selectedSnapShotId = selectedRow.SnapshotID;
            this.selectedUsedId = selectedRow.UserID;

            this.flagFormLoad = false;
        }

        /// <summary>
        /// Loads the snap shot.
        /// </summary>
        private void LoadSnapShot()
        {
            try
            {
                if (this.selectedSnapShotId != 0)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        if (this.currentRecordIndex < this.snapShotUtilityData.ListSnapShot.Rows.Count && !this.batchButtonControlFormCall)
                        {
                            this.loadSnapShotDetails.MasterFormNO = this.activeFormMaster;
                            this.loadSnapShotDetails.SnapShotId = this.selectedSnapShotId;
                            this.loadSnapShotDetails.QueryViewId = this.selectedQueryView;
                            this.loadSnapShotDetails.SnapShotCount = this.countTextBox.NumericTextBoxValue;
                            this.OnD9030_F9033_LoadSnapShotDetails(new TerraScan.Infrastructure.Interface.EventArgs<LoadSnapShotDetails>(this.loadSnapShotDetails));
                        }

                        ////when the F9040 is loaded from Batch Button controls
                        if (this.batchButtonControlFormCall && this.currentRecordIndex < this.snapShotUtilityData.ListSnapShot.Rows.Count)
                        {
                            ////this snapshot id is passed to the Batch Button controls
                            this.currentSnapShotId = this.selectedSnapShotId;
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the buttons.
        /// </summary>
        /// <param name="buttonActionMode">The button action mode.</param>
        private void SetButtons(TerraScanCommon.ButtonActionMode buttonActionMode)
        {
            try
            {
                switch (buttonActionMode)
                {
                    case TerraScanCommon.ButtonActionMode.CancelMode:
                        {
                            if (this.activeFormMasterPermissionFields.newPermission && this.FormPermissionFields.newPermission)
                            {
                                this.newSnapshotUtilityButton.Enabled = true;
                            }
                            else
                            {
                                this.newSnapshotUtilityButton.Enabled = false;
                            }

                            if (this.activeFormMasterPermissionFields.deletePermission && this.FormPermissionFields.deletePermission)
                            {
                                this.deleteSnapshotUtilityButton.Enabled = true;
                            }
                            else
                            {
                                this.deleteSnapshotUtilityButton.Enabled = false;
                            }

                            this.saveSnapshotUtilityButton.Enabled = false;
                            this.EnableNullRecordMode(false);
                            this.loadSnapshotUtilityButton.Enabled = true;
                            this.label1.Visible = false;
                            this.AllRowRadioButton.Visible = false;
                            this.PinnedRadioButton.Visible = false;
                            this.UnPinnedRowRadioButton.Visible = false;
                            this.cancelSnapshotUtilityButton.Enabled = false;
                            ////to set the cancel button
                            this.SetCancelButton();

                            break;
                        }

                    case TerraScanCommon.ButtonActionMode.NewMode:
                        {
                            this.newSnapshotUtilityButton.Enabled = false;
                            this.cancelSnapshotUtilityButton.Enabled = true;
                            this.deleteSnapshotUtilityButton.Enabled = false;
                            this.saveSnapshotUtilityButton.Enabled = true;
                            this.EnableNullRecordMode(false);
                            this.loadSnapshotUtilityButton.Enabled = false;

                            if (!this.batchButtonControlFormCall)
                            {
                                this.label1.Visible = true;
                                this.AllRowRadioButton.Visible = true;
                                this.PinnedRadioButton.Visible = true;
                                this.UnPinnedRowRadioButton.Visible = true;
                            }

                            ////to set the cancel button
                            this.SetCancelButton();

                            break;
                        }

                    case TerraScanCommon.ButtonActionMode.EditMode:
                        {
                            if (this.activeFormMasterPermissionFields.editPermission && this.FormPermissionFields.editPermission)
                            {
                                this.newSnapshotUtilityButton.Enabled = false;
                                this.cancelSnapshotUtilityButton.Enabled = true;
                                this.deleteSnapshotUtilityButton.Enabled = false;
                                this.saveSnapshotUtilityButton.Enabled = true;
                                this.EnableNullRecordMode(false);
                                this.loadSnapshotUtilityButton.Enabled = false;
                            }
                            else
                            {
                                this.CancelOperation();
                            }

                            ////to set the cancel button
                            this.SetCancelButton();

                            break;
                        }

                    case TerraScanCommon.ButtonActionMode.NullRecordMode:
                        {
                            this.createdByTextBox.Text = string.Empty;
                            this.createdOnTextBox.Text = string.Empty;
                            if (this.activeFormMasterPermissionFields.newPermission && this.FormPermissionFields.newPermission)
                            {
                                this.newSnapshotUtilityButton.Enabled = true;
                            }
                            else
                            {
                                this.newSnapshotUtilityButton.Enabled = false;
                            }

                            this.cancelSnapshotUtilityButton.Enabled = false;
                            this.deleteSnapshotUtilityButton.Enabled = false;
                            this.saveSnapshotUtilityButton.Enabled = false;
                            this.EnableNullRecordMode(true);
                            this.loadSnapshotUtilityButton.Enabled = false;
                            this.label1.Visible = false;
                            this.AllRowRadioButton.Visible = false;
                            this.PinnedRadioButton.Visible = false;
                            this.UnPinnedRowRadioButton.Visible = false;

                            ////to set the cancel button
                            this.SetCancelButton();

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Cancels the operation.
        /// </summary>
        private void CancelOperation()
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                if (this.currentRecordIndex >= 0)
                {
                    if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                    {
                        this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                        // this.snapshotUtilityGridView.Enabled = true;                       
                        this.SnapshotMgmtDataGrid.Enabled = true;
                    }
                    else
                    {
                        this.ClearControlValues();
                        this.SnapshotMgmtDataGrid.Enabled = false;
                        //this.snapshotUtilityGridView.Enabled = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
                else
                {
                    if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                    {
                        this.ClearControlValues();
                        this.SnapshotMgmtDataGrid.Enabled = true;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else
                    {
                        this.ClearControlValues();
                        this.SnapshotMgmtDataGrid.Enabled = false;
                        //this.snapshotUtilityGridView.Enabled = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the null record mode.
        /// </summary>
        /// <param name="flagEnable">if set to <c>true</c> [flag enable].</param>
        private void EnableNullRecordMode(bool flagEnable)
        {
            this.namePanel.Enabled = !flagEnable;
            this.descriptionPanel.Enabled = !flagEnable;
            this.panel1.Enabled = !flagEnable;
            this.createdByPanel.Enabled = !flagEnable;
            this.createdOnPanel.Enabled = !flagEnable;
            this.countPanel.Enabled = !flagEnable;
            if (!this.batchButtonControlFormCall)
            {
                this.includeRowsPanel.Enabled = !flagEnable;
            }
            else
            {
                this.includeRowsPanel.Enabled = false;
            }
        }

        /// <summary>
        /// Clears the control values.
        /// </summary>
        private void ClearControlValues()
        {
            this.flagFormLoad = true;
            this.nameTextBox.Text = string.Empty;
            this.descriptionTextBox.Text = string.Empty;
            this.createdOnTextBox.Text = string.Empty;
            this.createdByTextBox.Text = string.Empty;
            this.countTextBox.Text = string.Empty;
            this.flagFormLoad = false;
        }

        /// <summary>
        /// Saves the snapshot.
        /// </summary>
        private void SaveSnapshot()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.SaveSnapShotDetails())
                {
                    this.flagFormLoad = true;
                    this.LoadSnapShotGrid();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;

                    if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 0)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.NewIndex(this.currentKey);
                        this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                        this.SnapshotMgmtDataGrid.Enabled = true;
                        //this.snapshotUtilityGridView.Enabled = true;
                        this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;

                        //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                    }
                    else
                    {
                        this.SnapshotMgmtDataGrid.Enabled = false;
                        //this.snapshotUtilityGridView.Enabled = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }

                    this.SetDefault();
                    this.flagFormLoad = false;
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
        /// Saves the snap shot details.
        /// </summary>
        /// <returns>Boolean value
        /// true -- when saved
        /// false -- when unsaved
        /// </returns>
        private bool SaveSnapShotDetails()
        {
            F9040SnapShotUtilityData selectedSnapShotDetails = new F9040SnapShotUtilityData();
            F9040SnapShotUtilityData.SnapShotDetailsRow snapShotDetails = selectedSnapShotDetails.SnapShotDetails.NewSnapShotDetailsRow();
            DataTable tempPinnedDataTable, tempUnPinnedDataTable;
            DataSet selectedSnapShotIdCollection = new DataSet();
            string pinnedType = string.Empty;

            if (this.batchButtonControlFormCall)
            {
                /*on batch button call QueryView is always zero*/
                snapShotDetails.QueryView = 0;
            }
            else
            {
                // If condition added to fix invalid queryviewid passing issue
                if (this.selectedSnapShotId > 0)
                {
                    // For existing record, pass queryviewid which is present in current gridrow
                    snapShotDetails.QueryView = this.selectedQueryView;
                }
                else
                {
                    // For new record, pass queryviewid which is getting from QE form
                    snapShotDetails.QueryView = this.baseQueryViewId;
                }

                ////used to get the snapshot id collection
                tempPinnedDataTable = this.addedPinnedDataTable.Copy();
                tempUnPinnedDataTable = this.addedUnPinnedDataTable.Copy();

                if (AllRowRadioButton.Checked)
                {
                    ////tempPinnedDataTable.Merge(tempUnPinnedDataTable, true);
                    pinnedType = "All";
                }
                else if (PinnedRadioButton.Checked)
                {
                    ////tempUnPinnedDataTable.Rows.Clear();
                    ////tempPinnedDataTable.Merge(tempUnPinnedDataTable, true);
                    pinnedType = "Pinned";
                }
                else if (UnPinnedRowRadioButton.Checked)
                {
                    ////tempPinnedDataTable.Rows.Clear();
                    ////tempPinnedDataTable.Merge(tempUnPinnedDataTable, true);
                    pinnedType = "UnPinned";
                }

                selectedSnapShotIdCollection.Tables.Add(tempPinnedDataTable.Copy());
                selectedSnapShotIdCollection.Tables[0].TableName = "PinnedTable";
                selectedSnapShotIdCollection.Tables["PinnedTable"].Columns[0].ColumnName = "KeyID";
            }

            snapShotDetails.Form = this.activeFormMaster;
            snapShotDetails.SnapshotName = this.nameTextBox.Text.Trim();
            snapShotDetails.SnapshotNote = this.descriptionTextBox.Text.Trim();
            snapShotDetails.UserID = this.selectedUsedId;
            selectedSnapShotDetails.SnapShotDetails.Rows.Add(snapShotDetails);

            if (!this.batchButtonControlFormCall)
            {
                if ((this.addedPinnedDataTable.Rows.Count <= 0 && this.addedUnPinnedDataTable.Rows.Count <= 0) || Convert.ToInt32(this.countTextBox.Text.Replace(",", "")) <= 0)
                {
                    MessageBox.Show("This snapshot's details cannot be saved because there are no records available to save.", SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    this.currentKey = this.form9040Control.WorkItem.F9040_SaveSnapShot(this.selectedSnapShotId, TerraScanCommon.GetXmlString(selectedSnapShotDetails.SnapShotDetails), TerraScanCommon.GetXmlString(selectedSnapShotIdCollection.Tables[0]), this.filterXML, pinnedType, TerraScanCommon.UserId, this.snapShotValue);
                    return true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.nameTextBox.Text) && this.activeFormMaster > 0)
                {
                    /* on batch button form call*/
                    this.currentKey = this.form9040Control.WorkItem.F9040_SaveBatchButtonSnapShots(this.selectedSnapShotId, TerraScanCommon.GetXmlString(selectedSnapShotDetails.SnapShotDetails), TerraScanCommon.UserId);
                    return true;
                }
                else
                {
                    MessageBox.Show("This snapshot's details cannot be saved because there are  no records available to save.", SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// News the index.
        /// </summary>
        /// <param name="currentKeyValue">The current key value.</param>
        private void NewIndex(int currentKeyValue)
        {
            for (int rowNo = 0; rowNo < this.snapShotUtilityData.ListSnapShot.Rows.Count; rowNo++)
            {
                int rowKeyId;
                //int.TryParse(this.snapshotUtilityGridView[this.snapShotUtilityData.ListSnapShot.SnapshotIDColumn.ColumnName, rowNo].Value.ToString(), out rowKeyId);
                int.TryParse(this.SnapshotMgmtDataGrid.Rows[rowNo].Cells[this.snapShotUtilityData.ListSnapShot.SnapshotIDColumn.ColumnName].Value.ToString(), out rowKeyId);
                if (rowKeyId == currentKeyValue)
                {
                    this.currentRecordIndex = rowNo;
                    this.selectedSnapShotId = currentKeyValue;
                    return;
                }
            }
        }

        /// <summary>
        /// Sets the default.
        /// </summary>
        private void SetDefault()
        {
            try
            {
                if (this.snapShotUtilityData.DefaultIncludeRows.Rows.Count > 0)
                {
                    this.defaultRadioButton = this.snapShotUtilityData.DefaultIncludeRows.Rows[0][this.snapShotUtilityData.DefaultIncludeRows.IncludeRowsColumn].ToString();

                    if (!string.IsNullOrEmpty(this.defaultRadioButton))
                    {
                        switch (this.defaultRadioButton)
                        {
                            case "All":
                                {
                                    this.AllRowRadioButton.Checked = true;
                                    this.PinnedRadioButton.Checked = false;
                                    this.UnPinnedRowRadioButton.Checked = false;
                                    break;
                                }

                            case "Pinned":
                                {
                                    this.AllRowRadioButton.Checked = false;
                                    this.PinnedRadioButton.Checked = true;
                                    this.UnPinnedRowRadioButton.Checked = false;
                                    break;
                                }

                            case "NotPinned":
                                {
                                    this.AllRowRadioButton.Checked = false;
                                    this.PinnedRadioButton.Checked = false;
                                    this.UnPinnedRowRadioButton.Checked = true;
                                    break;
                                }
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
        /// Merges the pinned unpinned rows.
        /// </summary>
        private void MergePinnedUnpinnedRows()
        {
            try
            {
                DataTable tempPinnedDataTable = new DataTable("tempPinnedDataTable");
                DataTable tempUnPinnedDataTable = new DataTable("tempUnPinnedDataTable");
                tempPinnedDataTable = this.addedPinnedDataTable.Copy();
                tempUnPinnedDataTable = this.addedUnPinnedDataTable.Copy();
                tempPinnedDataTable.Merge(tempUnPinnedDataTable, true);
                if (tempPinnedDataTable.Rows.Count > 0)
                {
                    //this.countTextBox.Text = tempPinnedDataTable.Rows.Count.ToString("#,###");
                    this.countTextBox.Text = this.totalRecord.ToString("#,###");
                }
                else
                {
                    this.countTextBox.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void ControlLock(bool lockControl)
        {
            this.nameTextBox.LockKeyPress = lockControl;
            this.descriptionTextBox.LockKeyPress = lockControl;
        }

        #endregion Private Methods

        private void SnapshotMgmtDataGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SnapshotName");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SnapshotNote");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Form");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name_Display");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InsertedDate");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SnapshotID");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecordCount");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            //Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("QueryView");
            //ultraGridColumn1.MaxWidth = 180;
            //ultraGridColumn2.MaxWidth = 266;
            //ultraGridColumn4.MaxWidth = 105;
            //ultraGridColumn5.MaxWidth = 89;



        }

        private void SnapshotMgmtDataGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (this.SnapshotMgmtDataGrid.ActiveRow != null)
                {
                    if (!this.flagFormLoad && this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                    {
                        this.ControlLock(false);
                        this.deleteSnapshotUtilityButton.Enabled = true;
                        this.loadSnapshotUtilityButton.Enabled = true;
                        if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                        {
                            DialogResult dialogResult;
                            dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + " " + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dialogResult == DialogResult.Yes)
                            {
                                if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                                {
                                    if (string.IsNullOrEmpty(this.nameTextBox.Text) || string.IsNullOrEmpty(this.descriptionTextBox.Text))
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        this.SaveSnapshot();
                                        this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;
                                        this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                        this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                                        //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                                    }
                                }
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;
                                this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                this.pageMode = TerraScanCommon.PageModeTypes.View;
                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            }
                            else
                            {
                                this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                                // TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                                return;
                            }
                        }
                        else
                        {
                            if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                            {

                                this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;
                                if (this.currentRecordIndex != this.snapShotUtilityData.ListSnapShot.Rows.Count)
                                {
                                    this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ClearControlValues();
                        this.ControlLock(true);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.deleteSnapshotUtilityButton.Enabled = false;
                        this.loadSnapshotUtilityButton.Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SnapshotMgmtDataGrid_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                UltraGrid grid = (UltraGrid)sender;
                UIElement lastElementEntered = grid.DisplayLayout.UIElement.LastElementEntered;
                UltraGridCell cell = lastElementEntered.GetContext(typeof(UltraGridCell)) as UltraGridCell;
                if (cell.Activated)
                {
                    if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                    {
                        this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;
                        if (this.SnapshotMgmtDataGrid.Rows[this.SnapshotMgmtDataGrid.ActiveRow.Index].Cells[this.SnapshotID.Name].Value != null && !string.IsNullOrEmpty(this.SnapshotMgmtDataGrid.Rows[this.SnapshotMgmtDataGrid.ActiveRow.Index].Cells[this.SnapshotID.Name].Value.ToString()))
                        {
                            this.LoadSnapShot();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SnapshotMgmtDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    int tempRowIndex = 0;
            //    int tempmiscGirdRowIndex = 0;
            //    this.ControlLock(false);
            //    if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
            //    {
            //        tempRowIndex = ((TerraScan.UI.Controls.TerraScanInfragisticsUltraGrid)sender).ActiveRow.Index;

            //        switch (e.KeyCode)
            //        {
            //            case Keys.Down:
            //                {
            //                    if ((tempRowIndex + 1) <= this.SnapshotMgmtDataGrid.Rows.Count - 1)
            //                    //if ((tempRowIndex + 1) <= this.snapshotUtilityGridView.OriginalRowCount - 1)
            //                    {
            //                        tempmiscGirdRowIndex = tempRowIndex + 1;

            //                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //                        {
            //                            this.currentRecordIndex = tempmiscGirdRowIndex;
            //                            this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                            this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                            //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                            e.Handled = true;
            //                        }
            //                        else
            //                        {
            //                            DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            //                            if (dialogResult == DialogResult.Yes)
            //                            {
            //                                e.Handled = true;

            //                                if (string.IsNullOrEmpty(this.nameTextBox.Text) || string.IsNullOrEmpty(this.descriptionTextBox.Text))
            //                                {
            //                                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                                }
            //                                else
            //                                {
            //                                    this.SaveSnapshot();
            //                                    this.currentRecordIndex = tempmiscGirdRowIndex;
            //                                    this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                                    this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                                    // TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                                }
            //                            }
            //                            else if (dialogResult == DialogResult.No)
            //                            {
            //                                e.Handled = true;

            //                                this.currentRecordIndex = tempmiscGirdRowIndex;
            //                                this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                                this.pageMode = TerraScanCommon.PageModeTypes.View;
            //                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //                            }
            //                            else if (dialogResult == DialogResult.Cancel)
            //                            {
            //                                e.Handled = true;
            //                                this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                                //  TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                                return;
            //                            }
            //                        }
            //                    }

            //                    break;
            //                }

            //            case Keys.Up:
            //                {
            //                    if ((tempRowIndex - 1) >= 0)
            //                    {
            //                        tempmiscGirdRowIndex = tempRowIndex - 1;

            //                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //                        {
            //                            this.currentRecordIndex = tempmiscGirdRowIndex;
            //                            this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                            e.Handled = true;
            //                        }
            //                        else
            //                        {
            //                            DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            //                            if (dialogResult == DialogResult.Yes)
            //                            {
            //                                e.Handled = true;

            //                                if (string.IsNullOrEmpty(this.nameTextBox.Text) || string.IsNullOrEmpty(this.descriptionTextBox.Text))
            //                                {
            //                                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                                }
            //                                else
            //                                {
            //                                    this.SaveSnapshot();
            //                                    this.currentRecordIndex = tempmiscGirdRowIndex;
            //                                    this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                                    this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                                    // TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                                }
            //                            }
            //                            else if (dialogResult == DialogResult.No)
            //                            {
            //                                e.Handled = true;

            //                                this.currentRecordIndex = tempmiscGirdRowIndex;
            //                                this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
            //                                this.pageMode = TerraScanCommon.PageModeTypes.View;
            //                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //                            }
            //                            else if (dialogResult == DialogResult.Cancel)
            //                            {
            //                                e.Handled = true;
            //                                this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
            //                                //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
            //                                return;
            //                            }
            //                        }
            //                    }

            //                    break;
            //                }
            //        }
            //    }
            //}
            //catch (Exception e1)
            //{
            //    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        private void SnapshotMgmtDataGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.SnapshotMgmtDataGrid.ActiveRow != null)
                {
                    if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0 && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                    {
                        this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;

                        // this.LoadSnapShot();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SnapshotMgmtDataGrid_FilterRow(object sender, FilterRowEventArgs e)
        {
            if (this.snapShotUtilityData.ListSnapShot.Rows.Count > 8)
            {
                this.snapshotVScrollBar.Enabled = false;
                this.snapshotVScrollBar.Visible = false;
            }
            else
            {
                this.snapshotVScrollBar.Enabled = false;
                this.snapshotVScrollBar.Visible = true;
            }
        }

        private void SnapshotMgmtDataGrid_BeforeCellActivate(object sender, CancelableCellEventArgs e)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                {
                    if (this.SnapshotMgmtDataGrid.ActiveRow.Index < this.snapShotUtilityData.ListSnapShot.Rows.Count)
                        this.SetControlValues(this.GetSelectedRow(this.SnapshotMgmtDataGrid.ActiveRow.Index));
                }
            }
        }

        private void SnapshotMgmtDataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.SnapshotMgmtDataGrid.ActiveRow != null)
                {
                    if (!this.flagFormLoad && this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                    {
                        this.ControlLock(false);
                        this.deleteSnapshotUtilityButton.Enabled = true;
                        this.loadSnapshotUtilityButton.Enabled = true;
                        if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                        {
                            DialogResult dialogResult;
                            dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + " " + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (dialogResult == DialogResult.Yes)
                            {
                                if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                                {
                                    if (string.IsNullOrEmpty(this.nameTextBox.Text) || string.IsNullOrEmpty(this.descriptionTextBox.Text))
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        this.SaveSnapshot();
                                        this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;
                                        this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                        this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                                        //TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                                    }
                                }
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;
                                this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                this.pageMode = TerraScanCommon.PageModeTypes.View;
                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            }
                            else
                            {
                                this.SnapshotMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                                // TerraScanCommon.SetDataGridViewPosition(this.snapshotUtilityGridView, this.currentRecordIndex);
                                return;
                            }
                        }
                        else
                        {
                            if (this.SnapshotMgmtDataGrid.ActiveRow.Index >= 0)
                            {

                                this.currentRecordIndex = this.SnapshotMgmtDataGrid.ActiveRow.Index;
                                if (this.currentRecordIndex != this.snapShotUtilityData.ListSnapShot.Rows.Count)
                                {
                                    this.SetControlValues(this.GetSelectedRow(this.currentRecordIndex));
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ClearControlValues();
                        this.ControlLock(true);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.deleteSnapshotUtilityButton.Enabled = false;
                        this.loadSnapshotUtilityButton.Enabled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
               
        private void SnapshotOperationsButton_Click_1(object sender, EventArgs e)
        {
            object[] optionalParameter = null;
            //this.loadSnapShotDetails.MasterFormNO = this.activeFormMaster;
            //this.loadSnapShotDetails.SnapShotId = this.selectedSnapShotId;
            //int selectedValue = 0;
            //bool selectedValueFound;
            //int selectedSnapshotValue = 0;
            Form snapShotForm = new Form();
            optionalParameter = new object[] { this.selectedSnapShotId, this.activeFormMaster, TerraScanCommon.UserId, this.activeFormMasterPermissionFields };
            snapShotForm = TerraScanCommon.GetForm(9044, optionalParameter, this.form9040Control.WorkItem);
            if (snapShotForm != null)
            {
                snapShotForm.ShowDialog();
                LoadSnapShotGrid();

            }
        }
    }
}