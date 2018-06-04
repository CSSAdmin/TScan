// -------------------------------------------------------------------------------------------------
// <copyright file="TerraScanDataGridView.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// User Control 
// </summary>
// -------------------------------------------------------------------------------------------------

namespace TerraScan.UI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Data;

    /// <summary>
    /// TerraScanDataGridView - (dont use the Column with 'EmptyRecord$' ColumnName)
    /// </summary>
    public class TerraScanDataGridView : System.Windows.Forms.DataGridView
    {
        #region Variables

        /// <summary>
        /// Allow empty rows
        /// </summary>
        private bool allowEmptyRows;

        /// <summary>
        /// Allow Enter Key
        /// </summary>
        private bool allowEnterKey;

        /// <summary>
        /// Has Selectable Fields
        /// </summary>
        private bool hasSelectableFields = true;

        /// <summary>
        /// Not Applicable
        /// </summary>
        private bool applyStandardBehaviour;

        /// <summary>
        /// previous CurrentCell
        /// </summary>
        private DataGridViewCell previousCurrentCell = null;

        /// <summary>
        /// Allow Sorting
        /// </summary>
        private bool allowSorting;

        /// <summary>
        /// Allow Double Click
        /// </summary>
        private bool allowDoubleClick;

        /// <summary>
        /// Allow cell Click
        /// </summary>
        private bool allowCellClick = true;

        /// <summary>
        /// checks emptyrecord column exists
        /// </summary>
        private bool emptyRecordColumnExists;

        /// <summary>
        /// checks emptyrecord column exists
        /// </summary>
        private bool gridContentSelected;

        /// <summary>
        /// indicates whether it is editable or not
        /// </summary>
        private bool iseditableGrid;

        /// <summary>
        /// gets priginal row count
        /// </summary>
        private int originalRowCount;

        /// <summary>
        /// deselects specified row
        /// </summary>
        private int deselectSpecifiedRow = -1;

        /// <summary>
        /// defaultRow Index
        /// </summary>
        private int defaultRowIndex;

        /// <summary>
        /// The Number of visible by default
        /// </summary>
        private int numRowsVisible;

        /// <summary>
        /// The row index of the new row
        /// </summary>
        private int nextRowIndex = -1;

        /// <summary>
        /// The column index of the new column
        /// </summary>
        private int nextColumnIndex = -1;

        /// <summary>
        /// The row index of the previous row if currentcell is null
        /// </summary>
        private int previousRowIndex = -1;

        /// <summary>
        /// The column index of the previous column if currentcell is null
        /// </summary>
        private int previousColumnIndex = -1;

        /// <summary>
        /// The column name of the empty rows
        /// </summary>
        private string emptyRecordColumnName = "EmptyRecord$";

        /// <summary>
        /// The column name of the primary Key
        /// </summary>
        private string primaryKeyColumnName = String.Empty;

        /// <summary>
        /// The row index of the current row
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// The current XPosition
        /// </summary>
        private int currentXPosition;

        /// <summary>
        /// current YPosition
        /// </summary>
        private int currentYPosition;

        /// <summary>
        /// through Mouse Click
        /// </summary>
        private bool throughMouseClick;

        /// <summary>
        /// The column index of the current column
        /// </summary>
        private int currentColumnIndex;

        /// <summary>
        /// deselect Current Cell
        /// </summary>
        private bool deselectCurrentCell;

        /// <summary>
        /// Base DataView
        /// </summary>
        private DataView baseDataView;

        /// <summary>
        /// variable contains grid sort order
        /// </summary>
        private GridSortOrder baseGridSortOrder;

        /// <summary>
        /// variable contains grid sorted column
        /// </summary>
        private DataGridViewColumn baseSortedColumn;

        /// <summary>
        /// clears currentcell value on leave
        /// </summary>
        private bool clearCurrentCellOnLeave;

        /// <summary>
        /// removes the defaule selection of the currentcell
        /// </summary>
        private bool removeDefaultSelection;

        /// <summary>
        /// maintains sort
        /// </summary>
        private bool remainSortFields;

        /// <summary>
        /// hitTextInfo instance
        /// </summary>
        private HitTestInfo hitTextInfo;

        /// <summary>
        /// Enable Binding
        /// </summary>
        private bool enableBinding = true;

        /// <summary>
        /// Enable Binding
        /// </summary>
        private bool sortedGrid = false;

        /// <summary>
        /// set readOnlyCell
        /// </summary>
        private DataGridViewCell readOnlyCell;

        /// <summary>
        /// editMode Changed explicitly
        /// </summary>
        private bool editModeChanged;

        /// <summary>
        /// indicate whether key sroke processed or not - between keydown and datagridviewkey event
        /// </summary>
        private bool keystrokeProcessed;

        #endregion

        #region Constructor

        /// <summary>
        /// TerraScanDataGridView
        /// </summary>
        public TerraScanDataGridView()
        {
            ////this.EnableHeadersVisualStyles = false;
            ////this.ColumnHeadersHeight = 23;
            ////this.AllowUserToAddRows = false;
            ////this.RowHeadersWidth = 20;
            //////// this.BackgroundColor = System.Drawing.Color.Blue;
            ////this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ////this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ////this.ScrollBars = ScrollBars.Vertical;
            ////this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            ////this.MultiSelect = false;
            ////this.AllowUserToResizeColumns = false;
            ////this.AllowUserToResizeRows = false;
            //// Call to Default Settings
            this.SetDefaultDataGridViewSettings();
        }

        #endregion

        #region delegateDeclaration
        /// <summary>
        /// Declare delegate for Before Row Enter.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        /// <returns>the Boolvalue</returns>
        public delegate bool BeforeRowEnterEventHandler(DataGridViewCellEventArgs e);

        #endregion

        #region eventDecalration

        /// <summary>
        /// Declare the event, which is associated with the
        /// delegate BeforeRowEnter(object, DataGridViewCellEventArgs).  
        /// </summary>          
        public event BeforeRowEnterEventHandler BeforeRowEnter;

        #endregion

        #region Enum

        /// <summary>
        /// Enumerator Status Action
        /// </summary>
        public enum GridSortOrder
        {
            /// <summary>
            /// ASC = 0
            /// </summary>
            Asc = 0,

            /// <summary>
            /// DESC = 1
            /// </summary>
            Desc = 1
        }

        #endregion

        #region DataSource

        /// <summary>
        /// Gets or sets the data source that the <see cref="T:System.Windows.Forms.DataGridView"></see> is displaying data for.
        /// </summary>
        /// <value></value>
        /// <returns>The object that contains data for the <see cref="T:System.Windows.Forms.DataGridView"></see> to display.</returns>
        /// <exception cref="T:System.Exception">An error occurred in the data source and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }

            set
            {
                this.emptyRecordColumnExists = false;
                this.baseDataView = null;

                DataView tempDataView = null;

                if (value != null)
                {
                    switch (value.GetType().ToString())
                    {
                        case "System.Data.DataView":
                            tempDataView = value as DataView;
                            break;
                        case "System.Data.DataTable":
                            tempDataView = (value as DataTable).DefaultView;
                            break;
                        case "System.Data.DataSet":
                            DataSet tempDataSet = value as DataSet;
                            if (tempDataSet.Tables.Count > 0)
                            {
                                tempDataView = tempDataSet.Tables[0].DefaultView;
                            }
                            else
                            {
                                tempDataView = null;
                            }

                            break;
                        default:
                            if (value.GetType().BaseType.ToString() == "System.Data.DataTable" || value.GetType().BaseType.BaseType.ToString() == "System.Data.DataTable")
                            {
                                tempDataView = (value as DataTable).DefaultView;
                            }
                            else
                            {
                                DataSet tempTypedDataSet = new DataSet();

                                tempTypedDataSet = value as DataSet;
                                if (tempTypedDataSet.Tables.Count > 0)
                                {
                                    tempDataView = tempTypedDataSet.Tables[0].DefaultView;
                                }
                                else
                                {
                                    tempDataView = null;
                                }
                            }

                            break;
                    }
                }

                if (tempDataView != null)
                {
                    if (this.enableBinding)
                    {
                        this.baseDataView = tempDataView;
                    }
                    else
                    {
                        this.baseDataView = new DataView(tempDataView.ToTable());
                    }
                }

                this.CreateEmptyRows();

                this.CheckSelectableFields();

                base.DataSource = this.baseDataView;

                if (this.RemainSortFields && this.BaseSortedColumn != null)
                {
                    this.baseSortedColumn = this.Columns[this.BaseSortedColumn.Name];
                    this.ApplySortExpression();
                }
                else
                {
                    this.ClearSorting();
                }

                if (this.emptyRecordColumnExists && this.Columns[this.emptyRecordColumnName] != null)
                {
                    this.Columns[this.emptyRecordColumnName].Visible = false;
                }
                ////OnDataSourceChanged(EventArgs.Empty);

                ////OnDataBindingComplete(new DataGridViewBindingCompleteEventArgs(ListChangedType.Reset));
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// Set The the Grid Soriting is Clicked(Header).
        /// </summary>
        /// <value>The empty name of the record column.</value>
        public bool IsSorted
        {
            set { this.sortedGrid = value; }
            get { return this.sortedGrid; }
        }

        /// <summary>
        /// Sets whether multiple row selection is enable
        /// </summary>
        /// <value>The true/false value for multiselection</value>
        public bool IsMultiSelect
        {
            set { this.MultiSelect = value; }
            get { return this.MultiSelect; }
        }

        /// <summary>
        /// Gets the empty name of the record column.
        /// </summary>
        /// <value>The empty name of the record column.</value>
        public string EmptyRecordColumnName
        {
            get { return this.emptyRecordColumnName; }
        }

        /// <summary>
        /// Gets or sets the name of the primary key column.
        /// </summary>
        /// <value>The name of the primary key column.</value>
        public string PrimaryKeyColumnName
        {
            get { return this.primaryKeyColumnName; }
            set { this.primaryKeyColumnName = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [deselect current cell].
        /// </summary>
        /// <value><c>true</c> if [deselect current cell]; otherwise, <c>false</c>.</value>
        public bool DeselectCurrentCell
        {
            get { return this.deselectCurrentCell; }
            set { this.deselectCurrentCell = value; }
        }

        /// <summary>
        /// Gets a value indicating whether [through mouse click].
        /// </summary>
        /// <value><c>true</c> if [through mouse click]; otherwise, <c>false</c>.</value>
        public bool ThroughMouseClick
        {
            get { return this.throughMouseClick; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow empty rows].
        /// </summary>
        /// <value><c>true</c> if [allow empty rows]; otherwise, <c>false</c>.</value>
        public bool AllowEmptyRows
        {
            get { return this.allowEmptyRows; }
            set { this.allowEmptyRows = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow enter key].
        /// </summary>
        /// <value><c>true</c> if [allow enter key]; otherwise, <c>false</c>.</value>
        public bool AllowEnterKey
        {
            get { return this.allowEnterKey; }
            set { this.allowEnterKey = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has selectable fields.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has selectable fields; otherwise, <c>false</c>.
        /// </value>
        public bool HasSelectableFields
        {
            get
            {
                this.CheckSelectableFields();
                return this.hasSelectableFields;
            }
        }

        /// <summary>
        /// Gets or sets the index of the default row.
        /// </summary>
        /// <value>The index of the default row.</value>
        public int DefaultRowIndex
        {
            get
            {
                return this.defaultRowIndex;
            }

            set
            {
                int temp = value;

                if (temp < 0 && temp >= this.GetOriginalRowCount())
                {
                    temp = 0;
                }

                this.defaultRowIndex = temp;
            }
        }

        /// <summary>
        /// Gets the original row count.
        /// </summary>
        /// <value>The original row count.</value>
        public int OriginalRowCount
        {
            get
            {
                this.originalRowCount = this.GetOriginalRowCount();
                return this.originalRowCount;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow double click].
        /// </summary>
        /// <value><c>true</c> if [allow double click]; otherwise, <c>false</c>.</value>
        public bool AllowDoubleClick
        {
            get { return this.allowDoubleClick; }
            set { this.allowDoubleClick = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable binding].
        /// </summary>
        /// <value><c>true</c> if [enable binding]; otherwise, <c>false</c>.</value>
        public bool EnableBinding
        {
            get { return this.enableBinding; }
            set { this.enableBinding = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow cell click].
        /// </summary>
        /// <value><c>true</c> if [allow cell click]; otherwise, <c>false</c>.</value>
        public bool AllowCellClick
        {
            get { return this.allowCellClick; }
            set { this.allowCellClick = value; }
        }

        /// <summary>
        /// Not Applicable
        /// </summary>      
        public bool ApplyStandardBehaviour
        {
            get { return this.applyStandardBehaviour; }
            set { this.applyStandardBehaviour = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow sorting].
        /// </summary>
        /// <value><c>true</c> if [allow sorting]; otherwise, <c>false</c>.</value>
        public bool AllowSorting
        {
            get { return this.allowSorting; }
            set { this.allowSorting = value; }
        }

        /// <summary>
        /// Gets or sets the num rows visible.
        /// </summary>
        /// <value>The num rows visible.</value>
        public int NumRowsVisible
        {
            get { return this.numRowsVisible; }
            set { this.numRowsVisible = value; }
        }

        /// <summary>
        /// Gets the base sorted column.
        /// </summary>
        /// <value>The base sorted column.</value>
        public DataGridViewColumn BaseSortedColumn
        {
            get { return this.baseSortedColumn; }
        }

        /// <summary>
        /// Gets the base sorted order.
        /// </summary>
        /// <value>The base sort order.</value>
        public GridSortOrder BaseSortOrder
        {
            get
            {
                return this.baseGridSortOrder;
            }
        }

        /// <summary>
        /// Gets the index of the next row.
        /// </summary>
        /// <value>The index of the next row.</value>
        public int NextRowIndex
        {
            get { return this.nextRowIndex; }
        }

        /// <summary>
        /// Gets the index of the next column.
        /// </summary>
        /// <value>The index of the next column.</value>
        public int NextColumnIndex
        {
            get { return this.nextColumnIndex; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [clear current cell on leave].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [clear current cell on leave]; otherwise, <c>false</c>.
        /// </value>
        public bool ClearCurrentCellOnLeave
        {
            get { return this.clearCurrentCellOnLeave; }
            set { this.clearCurrentCellOnLeave = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [grid content selected].
        /// </summary>
        /// <value><c>true</c> if [grid content selected]; otherwise, <c>false</c>.</value>
        public bool GridContentSelected
        {
            get { return this.gridContentSelected; }
            set { this.gridContentSelected = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editable grid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is editable grid; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditableGrid
        {
            get { return this.iseditableGrid; }
            set { this.iseditableGrid = value; }
        }



        /// <summary>
        /// Gets the index of the current column.
        /// </summary>
        /// <value>The index of the current column.</value>
        public int CurrentColumnIndex
        {
            get
            {
                if (this.CurrentCell == null)
                {
                    this.currentColumnIndex = -1;
                }
                else
                {
                    this.currentColumnIndex = this.CurrentCell.ColumnIndex;
                }

                return this.currentColumnIndex;
            }
        }

        /// <summary>
        /// Gets the index of the previous row if currentcell is null.
        /// </summary>
        /// <value>The index of the previous row.</value>
        public int PreviousRowIndex
        {
            get
            {
                if (this.previousCurrentCell == null)
                {
                    this.previousRowIndex = -1;
                }
                else
                {
                    this.previousRowIndex = this.previousCurrentCell.RowIndex;
                }

                return this.previousRowIndex;
            }
        }

        /// <summary>
        /// Gets the index of the previous column if currentcell is null.
        /// </summary>
        /// <value>The index of the previous column.</value>
        public int PreviousColumnIndex
        {
            get
            {
                if (this.previousCurrentCell == null)
                {
                    this.previousColumnIndex = -1;
                }
                else
                {
                    this.previousColumnIndex = this.previousCurrentCell.ColumnIndex;
                }

                return this.previousColumnIndex;
            }
        }

        /// <summary>
        /// Gets the index of the current row.
        /// </summary>
        /// <value>The index of the current row.</value>
        public int CurrentRowIndex
        {
            get
            {
                if (this.CurrentCell == null)
                {
                    this.currentRowIndex = -1;
                }
                else
                {
                    this.currentRowIndex = this.CurrentCell.RowIndex;
                }

                return this.currentRowIndex;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default selection].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [remove default selection]; otherwise, <c>false</c>.
        /// </value>
        public bool RemoveDefaultSelection
        {
            get { return this.removeDefaultSelection; }
            set { this.removeDefaultSelection = value; }
        }

        /// <summary>
        /// Gets or sets the deselect specified row.
        /// </summary>
        /// <value>The deselect specified row.</value>
        public int DeselectSpecifiedRow
        {
            get { return this.deselectSpecifiedRow; }
            set { this.deselectSpecifiedRow = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remain sort fields].
        /// </summary>
        /// <value><c>true</c> if [remain sort fields]; otherwise, <c>false</c>.</value>
        public bool RemainSortFields
        {
            get { return this.remainSortFields; }
            set { this.remainSortFields = value; }
        }

        #endregion

        #region Accessible public methods

        /// <summary>
        /// Clears the sorting related fields.
        /// </summary>
        public void ClearSorting()
        {
            if (this.baseDataView != null)
            {
                if (this.emptyRecordColumnExists && this.allowSorting)
                {
                    this.baseDataView.Sort = string.Concat(this.emptyRecordColumnName, " ASC");
                }
                else
                {
                    this.baseDataView.Sort = string.Empty;
                }
            }

            this.baseGridSortOrder = GridSortOrder.Asc;
            this.baseSortedColumn = null;
        }

        /// <summary>
        /// Resets the next indexes.
        /// </summary>
        public void ResetIndexes()
        {
            this.nextColumnIndex = -1;
            this.nextRowIndex = -1;
        }

        /// <summary>
        /// Creates the empty rows.
        /// </summary>
        public void CreateEmptyRows()
        {
            if (this.AllowEmptyRows)
            {
                DataTable tempDataTable = new DataTable();

                if (this.baseDataView == null)
                {
                    foreach (DataGridViewColumn tempDataGridViewColumn in this.Columns)
                    {
                        DataColumn tempDataColumn = new DataColumn(tempDataGridViewColumn.DataPropertyName);
                        tempDataColumn.DefaultValue = System.DBNull.Value;
                        tempDataColumn.AllowDBNull = true;
                        tempDataTable.Columns.Add(tempDataColumn);
                    }

                    this.baseDataView = tempDataTable.DefaultView;
                }
                else
                {
                    tempDataTable = this.baseDataView.Table;

                    foreach (DataColumn tempDataColumn in tempDataTable.Columns)
                    {
                        tempDataColumn.DefaultValue = System.DBNull.Value;
                        tempDataColumn.AllowDBNull = true;
                    }
                }

                ////for emptyRecord 
                ////for generating unique names
                if (!this.emptyRecordColumnExists)
                {
                    ////int i = 1;
                    this.emptyRecordColumnName = "EmptyRecord$";
                    ////if (tempDataTable.Columns.Contains(this.emptyRecordColumnName))
                    ////{
                    ////    this.emptyRecordColumnName += "$";
                    ////}
                    ////do
                    ////{
                    ////    if (tempDataTable.Columns.Contains(this.emptyRecordColumnName))
                    ////    {
                    ////        this.emptyRecordColumnName += "$";
                    ////        break;
                    ////        i++;
                    ////    }
                    ////    else
                    ////    {
                    ////        break;
                    ////    }
                    ////}
                    ////while (true);

                    this.emptyRecordColumnExists = true;
                    if (tempDataTable.Columns.Contains(this.emptyRecordColumnName))
                    {
                        tempDataTable.Columns[this.emptyRecordColumnName].DefaultValue = false;
                    }
                    else
                    {
                        DataColumn dataColumn = new DataColumn(this.emptyRecordColumnName, typeof(bool));
                        dataColumn.DefaultValue = false;
                        tempDataTable.Columns.Add(dataColumn);
                        tempDataTable.AcceptChanges();
                    }
                }

                this.originalRowCount = tempDataTable.Rows.Count;

                for (int i = this.originalRowCount; i < this.NumRowsVisible; i++)
                {
                    DataRow dr = tempDataTable.NewRow();
                    dr[this.emptyRecordColumnName] = true;
                    tempDataTable.Rows.Add(dr);
                }
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event.
        /// </summary>
        /// <param name="displayErrorDialogIfNoHandler">true to display an error dialog box if there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event.</param>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"></see> that contains the event data.</param>
        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
                ////base.OnDataError(displayErrorDialogIfNoHandler, e);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.DataBindingComplete"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"></see> that contains the event data.</param>
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                base.OnDataBindingComplete(e);
                if (this.RemoveDefaultSelection && this.CurrentCell != null)
                {
                    this.ClearSelection();
                    if (this.OriginalRowCount == 0)
                    {
                        this.CurrentCell = null;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Raises the before row enter event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <returns>the bool value</returns>
        protected virtual bool OnBeforeRowEnter(DataGridViewCellEventArgs e)
        {
            // If an event has no subscribers registerd, it will 
            // evaluate to null. The test checks that the value is not
            // null, ensuring that there are subsribers before
            // calling the event itself.
            if (this.BeforeRowEnter != null)
            {
                return this.BeforeRowEnter(e);  // Notify Subscribers
            }

            return true;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Enter"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.InvalidCastException">The control is configured to enter edit mode when it receives focus, but upon entering focus, the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"></see> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"></see> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"></see>.</exception>
        /// <exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        protected override void OnEnter(EventArgs e)
        {
            if (!this.allowCellClick)
            {
                return;
            }

            this.hitTextInfo = HitTest(this.currentXPosition, this.currentYPosition);
            ////supress row enter of the current cell - on mouse click
            if (MouseButtons == MouseButtons.Left)
            {
                ////this.throughMouseClick = true;

                if (this.ApplyStandardBehaviour)
                {
                    this.SetExpectedIndex();
                    this.SetCurrentCell(true);
                }
            }
            else
            {
                this.throughMouseClick = false;
            }

            base.OnEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowEnter"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"></see> that contains the event data.</param>
        protected override void OnRowEnter(DataGridViewCellEventArgs e)
        {
            if (!this.OnBeforeRowEnter(e))
            {
                return;
            }

            base.OnRowEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellEnter"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"></see> property of e is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"></see> property of e is greater than the number of rows in the control minus one.</exception>
        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            ////reset readonly cell
            if (this.EditMode.Equals(DataGridViewEditMode.EditOnEnter))
            {
                if (this.readOnlyCell != null)
                {
                    this.readOnlyCell.ReadOnly = false;
                    this.readOnlyCell = null;
                }
            }

            base.OnCellEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!this.iseditableGrid)
            {
                int rowCount = this.GetOriginalRowCount();
                if (!this.AllowCellClick || rowCount == 0)
                {
                    this.SetCurrentCellAddressCore(-1, -1, false, false, false);
                    return;
                }

                this.hitTextInfo = HitTest(e.X, e.Y);

                this.SetExpectedIndex();

                if (!this.hasSelectableFields)
                {
                    return;
                }

                ////checks for default selection
                if (this.hitTextInfo.Type.Equals(DataGridViewHitTestType.TopLeftHeader) || this.hitTextInfo.Type.Equals(DataGridViewHitTestType.None))
                {
                    return;
                }

                ////block empty records - depends on row selection
                if ((this.hitTextInfo.Type.Equals(DataGridViewHitTestType.RowHeader) || this.hitTextInfo.Type.Equals(DataGridViewHitTestType.Cell)) && this.CheckEmptyRecord(this.hitTextInfo.RowIndex))
                {
                    return;
                }
            }

            this.throughMouseClick = true;

            base.OnMouseDown(e);

            this.throughMouseClick = false;

            if (this.deselectCurrentCell)
            {
                ////deselecte currentcell
                this.SetCurrentCellAddressCore(-1, -1, false, false, true);
                this.ClearSelection();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseDown"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"></see> property of e is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex"></see> property of e is greater than the number of rows in the control minus one.</exception>
        /// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. </exception>
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            if (!this.AllowDoubleClick)
            {
                return;
            }

            base.OnDoubleClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentDoubleClick"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"></see> property of e is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"></see> property of e is greater than the number of rows in the control minus one.</exception>
        protected override void OnCellContentDoubleClick(DataGridViewCellEventArgs e)
        {
            if (!this.AllowDoubleClick)
            {
                return;
            }

            base.OnCellContentDoubleClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellClick"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"></see> property of e is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"></see> property of e is greater than the number of rows in the control minus one.</exception>
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            if (!this.AllowCellClick)
            {
                return;
            }

            ////reset readonly cell
            if (this.readOnlyCell != null)
            {
                this.readOnlyCell.ReadOnly = false;
                this.readOnlyCell = null;
            }
            ////reset editmode to editonenter if it is explicitly changed
            if (this.editModeChanged && e.RowIndex > -1)
            {
                this.EditMode = DataGridViewEditMode.EditOnEnter;
                this.editModeChanged = false;
            }

            //////Added condition to prevent to show the deselected record values in textboxes --- Added by Latha
            //if (e.RowIndex >= 0)
            //{
            //    if (this.Rows[e.RowIndex].Selected)
            //    {
                    base.OnCellClick(e);
            //    }
            //}
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueChanged"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"></see> property of e is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"></see> property of e is greater than the number of rows in the control minus one.</exception>
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            if (this.iseditableGrid && this.CheckEmptyRecord(e.RowIndex))
            {
                if (e.RowIndex != -1)
                {
                    this.baseDataView[e.RowIndex][this.emptyRecordColumnName] = false;
                }
            }

            base.OnCellValueChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!this.iseditableGrid)
            {
                this.currentXPosition = e.X;
                this.currentYPosition = e.Y;
                if (e.Button == MouseButtons.Left)
                {
                    HitTestInfo hitTestInfo = HitTest(e.X, e.Y);

                    if (this.CheckEmptyRecord(hitTestInfo.RowIndex))
                    {
                        return;
                    }

                    this.nextColumnIndex = hitTestInfo.ColumnIndex;
                    this.nextRowIndex = hitTestInfo.RowIndex;
                }
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.Exception">This action would cause the control to enter edit mode but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"></see>.</exception>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                if (!this.iseditableGrid)
                {
                    #region Selecting Multiple rows 
                    ////Selecting Multiple rows using Arrow Keys   ----  Added by Latha
                    if (IsMultiSelect)
                    {
                        ////For Down Arrow Key 
                        if (e.KeyCode == (Keys.Back | Keys.Space) && e.Shift)
                        {
                            if (this.originalRowCount - 1 > this.CurrentRow.Index)
                            {
                                if (this.CurrentRow.Selected)
                                {
                                    this.SetCurrentCellAddressCore(-1, this.CurrentRow.Index, false, false, false);
                                }
                            }
                        }

                        ////For Up Arrow Key 
                        if (e.KeyCode == (Keys.RButton | Keys.MButton | Keys.Space) && e.Shift)
                        {
                            if (this.CurrentRow.Selected)
                            {
                                this.SetCurrentCellAddressCore(-1, this.CurrentRow.Index, false, false, false);
                            }
                        }
                    }
                    #endregion Selecting Multiple rows
                    
                    ////set to avoid explicit cell navigation in datagridviewkey(e)s
                    this.keystrokeProcessed = true;
                    ////navigate cell return true if key process handled manually else return false for default behaviour
                    if (this.NavigateCell(e))
                    {
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                ////reset
                this.keystrokeProcessed = false;
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Processes keys, such as the TAB, ESCAPE, RETURN, and ARROW keys, used to control dialog boxes.
        /// </summary>
        /// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"></see> values that represents the key or keys to process.</param>
        /// <returns>
        /// true if the key was processed; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"></see> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"></see> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"></see>.</exception>
        /// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. </exception>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (!this.iseditableGrid)
                {
                    ////check for editing control to trigger manual navigation
                    if (this.EditingControl != null)
                    {
                        ////navigate cell return true if key process handled manually else return false for default behaviour
                        if (this.NavigateCell(new KeyEventArgs(keyData)))
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Processes keys used for navigating in the <see cref="T:System.Windows.Forms.DataGridView"></see>.
        /// </summary>
        /// <param name="e">Contains information about the key that was pressed.</param>
        /// <returns>
        /// true if the key was processed; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"></see> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"></see> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"></see>.</exception>
        /// <exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true.-or-The DELETE key would delete one or more rows, but an error in the data source prevents the deletion and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"></see> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"></see> property to true. </exception>
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            try
            {
                if (!this.iseditableGrid)
                {
                    ////check for editing control to trigger manual navigation
                    if (this.EditingControl != null && !this.keystrokeProcessed)
                    {
                        ////navigate cell return true if key process handled manually else return false for default behaviour
                        if (this.NavigateCell(e))
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
            }

            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeaderMouseClick"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"></see> that contains information about the mouse and the header cell that was clicked.</param>
        protected override void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.EditMode.Equals(DataGridViewEditMode.EditOnEnter))
                {
                    if (this.CurrentCell != null)
                    {
                        this.readOnlyCell = this.CurrentCell;
                    }
                    else
                    {
                        int defaultColumnIndex = this.Columns.GetFirstColumn(DataGridViewElementStates.Displayed).Index;
                        if (defaultColumnIndex >= 0)
                        {
                            this.readOnlyCell = this[defaultColumnIndex, e.RowIndex];
                        }
                    }

                    if (this.readOnlyCell != null && !this.readOnlyCell.ReadOnly)
                    {
                        this.readOnlyCell.ReadOnly = true;
                    }
                    else
                    {
                        this.readOnlyCell = null;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                base.OnRowHeaderMouseClick(e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeaderMouseClick"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"></see> that contains the event data.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"></see> property of e is less than zero or greater than the number of columns in the control minus one.</exception>
        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.GetOriginalRowCount() > 0 && this.AllowSorting && this.baseDataView != null && e.Clicks % 2 != 0)
                {
                    DataGridViewColumn newSortedColumn = this.Columns[e.ColumnIndex];
                    DataGridViewColumn oldSortedColumn = this.baseSortedColumn;
                    GridSortOrder gridSortOrder;

                    if (!newSortedColumn.SortMode.Equals(DataGridViewColumnSortMode.NotSortable))
                    {
                        //// If oldSortedColumn is null, then the girdview is not sorted.
                        if (oldSortedColumn != null)
                        {
                            ////Sort the same column again, reversing the SortOrder.
                            if (oldSortedColumn == newSortedColumn && this.BaseSortOrder.Equals(GridSortOrder.Asc))
                            {
                                gridSortOrder = GridSortOrder.Desc;
                            }
                            else
                            {
                                // Sort a new column and remove the old SortGlyph.
                                gridSortOrder = GridSortOrder.Asc;
                                oldSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                            }
                        }
                        else
                        {
                            gridSortOrder = GridSortOrder.Asc;
                        }

                        this.baseSortedColumn = newSortedColumn;
                        this.baseGridSortOrder = gridSortOrder;

                        //// Sort the selected column.                    
                        this.ApplySortExpression();
                    }
                }

                //if (IsMultiSelect)
                //{
                //    this.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                //    this.Columns[e.ColumnIndex].Selected = true;
                //}

            }
            catch (Exception)
            {
            }
            finally
            {
                base.OnColumnHeaderMouseClick(e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Validated"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            this.GridContentSelected = false;

            if (this.ClearCurrentCellOnLeave)
            {
                //Coding for Avoing set reentrant cell to setcore function
                try
                {
                    this.CurrentCell = null;
                }
                catch
                {
                }
                //End Here
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.EditingControlShowing"></see> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"></see> that contains information about the editing control.</param>
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            ////set color to editing control
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            ////reset editmode to editonenter if it is explicitly changed
            if (this.editModeChanged)
            {
                this.EditMode = DataGridViewEditMode.EditOnEnter;
                this.editModeChanged = false;
            }

            base.OnEditingControlShowing(e);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Sets the default data grid view settings.
        /// </summary>
        private void SetDefaultDataGridViewSettings()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.BackgroundColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;

            this.DefaultCellStyle = dataGridViewCellStyle19;
            this.EnableHeadersVisualStyles = false;
            this.GridColor = System.Drawing.Color.Black;
            this.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EditMode = DataGridViewEditMode.EditOnEnter;
            this.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.RowHeadersWidth = 20;
            this.ColumnHeadersHeight = 23;
            this.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MultiSelect = false;
        }

        /// <summary>
        /// Sets the expected index.
        /// </summary>
        private void SetExpectedIndex()
        {
            if (!this.hasSelectableFields)
            {
                this.nextColumnIndex = -1;
                this.nextRowIndex = -1;
                return;
            }

            if (this.hitTextInfo.RowIndex < 0 || this.hitTextInfo.RowIndex == this.deselectSpecifiedRow || this.hitTextInfo.RowIndex >= this.OriginalRowCount)
            {
                this.nextColumnIndex = this.CurrentColumnIndex;
                this.nextRowIndex = this.CurrentRowIndex;
            }
            else
            {
                this.nextColumnIndex = this.hitTextInfo.ColumnIndex;
                this.nextRowIndex = this.hitTextInfo.RowIndex;
            }
        }

        /// <summary>
        /// Sets the current cell.
        /// </summary>
        /// <param name="withExpectedIndex">if set to <c>true</c> [with expected index].</param>
        private void SetCurrentCell(bool withExpectedIndex)
        {
            if (withExpectedIndex)
            {
                if (this.ApplyStandardBehaviour && this.nextRowIndex < 0)
                {
                    ////deselecte currentcell
                    this.SetCurrentCellAddressCore(-1, -1, false, false, true);
                    this.ClearSelection();
                }

                return;
            }

            if (this.ApplyStandardBehaviour && this.CurrentCell == null)
            {
                if (this.previousCurrentCell == null)
                {
                    ////firstcolumn
                    this.nextColumnIndex = this.Columns.GetFirstColumn(DataGridViewElementStates.Displayed).Index;
                    this.nextRowIndex = this.defaultRowIndex;
                }
                else
                {
                    this.nextColumnIndex = this.previousCurrentCell.ColumnIndex;
                    this.nextRowIndex = this.previousCurrentCell.RowIndex;
                }

                if (this.nextRowIndex == this.deselectSpecifiedRow || this.nextColumnIndex == -1 || this.nextRowIndex == -1)
                {
                    this.nextColumnIndex = -1;
                    this.nextRowIndex = -1;
                    this.SetCurrentCellAddressCore(this.NextColumnIndex, this.NextRowIndex, false, false, true);
                    this.ClearSelection();
                }
                else
                {
                    this.SetCurrentCellAddressCore(this.NextColumnIndex, this.NextRowIndex, false, false, true);
                    this.SetSelectedRowCore(this.NextRowIndex, true);
                }
            }
        }

        /// <summary>
        /// Sets the current row After Sorting.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        private void SetCurrentRow(object keyValue)
        {
            int rowIndex = -1;
            int defaultRowIndex = this.Rows.GetFirstRow(DataGridViewElementStates.Visible, DataGridViewElementStates.Frozen);
            int defaultColumnIndex = this.Columns.GetFirstColumn(DataGridViewElementStates.Displayed).Index;

            if (keyValue != null && !String.IsNullOrEmpty(keyValue.ToString()))
            {
                DataTable tempDataTable = this.baseDataView.ToTable();
                tempDataTable.DefaultView.RowFilter = string.Concat(this.primaryKeyColumnName, "='", keyValue, "'");

                if (tempDataTable.DefaultView.Count > 0)
                {
                    rowIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
                }

                ////if (this.Rows[rowIndex].Frozen)
                ////{
                ////    rowIndex = -1;
                ////}
            }

            this.ClearSelection();

            if (rowIndex > -1 && rowIndex < this.GetOriginalRowCount() && defaultColumnIndex > -1)
            {
                ////select currentrow - prior to sorting
                this.SetCurrentCellAddressCore(defaultColumnIndex, rowIndex, false, false, false);
                this.SetSelectedRowCore(rowIndex, true);
                this.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            else if (this.GetOriginalRowCount() > 0 && defaultColumnIndex > -1 && defaultRowIndex > -1)
            {
                ////select first row
                this.SetCurrentCellAddressCore(defaultColumnIndex, defaultRowIndex, false, false, false);
                this.SetSelectedRowCore(defaultRowIndex, true);
                this.FirstDisplayedScrollingRowIndex = defaultRowIndex;
            }
            else
            {
                ////deselect current row
                this.SetCurrentCellAddressCore(-1, -1, false, false, false);
            }
        }

        /// <summary>
        /// Navigates the cell manually.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        /// <returns>the boolean - true navigation succeed  no more navigation required else proceed normal </returns>
        private bool NavigateCell(KeyEventArgs e)
        {
            int rowCount = this.GetOriginalRowCount();

            if (this.CurrentColumnIndex < 0)
            {
                return true;
            }

            if (!this.AllowCellClick || rowCount == 0)
            {
                return true;
            }

            ////for enter key
            if (e.KeyValue == 13 && !this.allowEnterKey)
            {
                return true;
            }

            ////home and end
            if (e.KeyValue == 35 || e.KeyValue == 36)
            {
                return true;
            }

            this.GridContentSelected = true;
            bool setGridCell = false;
            int firstColumnIndex = this.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            int lastColumnIndex = this.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            int firstRowIndex = this.Rows.GetFirstRow(DataGridViewElementStates.Visible, DataGridViewElementStates.Frozen);

            ////down key
            if (e.KeyValue == 40 || e.KeyData == Keys.Enter)
            {
                if (e.Control || e.Shift)
                {
                    return true;
                }
                else
                {
                    ////not last row
                    if (this.CurrentRowIndex == rowCount - 1)
                    {
                        this.nextRowIndex = -1;
                    }
                    else
                    {
                        this.nextRowIndex = this.Rows.GetNextRow(this.CurrentRowIndex, DataGridViewElementStates.None);
                    }
                }

                this.nextColumnIndex = this.CurrentColumnIndex;
            }

            ////up key
            if (e.KeyValue == 38)
            {
                if (e.Control || e.Shift)
                {
                    return true;
                }

                ////first row
                if (this.CurrentRowIndex == 0)
                {
                    this.nextRowIndex = -1;
                }
                else
                {
                    this.nextRowIndex = this.Rows.GetPreviousRow(this.CurrentRowIndex, DataGridViewElementStates.None);
                }

                this.nextColumnIndex = this.CurrentColumnIndex;
            }

            ////left key
            if (e.KeyData == Keys.Left)
            {
                if (e.Control || e.Shift)
                {
                    return true;
                }

                ////first cell
                if (this.CurrentColumnIndex == firstColumnIndex)
                {
                    this.nextColumnIndex = -1;
                }
                else
                {
                    this.nextColumnIndex = this.Columns.GetPreviousColumn(this.Columns[this.CurrentColumnIndex], DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
                }

                this.nextRowIndex = this.CurrentRowIndex;
            }

            ////right key
            if (e.KeyData == Keys.Right)
            {
                if (e.Control || e.Shift)
                {
                    return true;
                }

                ////last cell
                if (this.CurrentColumnIndex == lastColumnIndex)
                {
                    this.nextColumnIndex = -1;
                }
                else
                {
                    this.nextColumnIndex = this.Columns.GetNextColumn(this.Columns[this.CurrentColumnIndex], DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
                }

                this.nextRowIndex = this.CurrentRowIndex;
            }

            ////tab key
            if (e.KeyValue == 9)
            {
                if (this.StandardTab)
                {
                    this.nextColumnIndex = -1;
                    this.nextRowIndex = -1;
                    return false;
                }

                if (e.Shift)
                {
                    ////first cell
                    if (this.CurrentColumnIndex == firstColumnIndex)
                    {
                        ////first row
                        if (this.CurrentRowIndex == firstRowIndex)
                        {
                            this.nextColumnIndex = -1;
                            this.nextRowIndex = -1;
                            ////navigate to previous control if cell not exist to navigate
                            base.ProcessDialogKey(Keys.Control | Keys.Tab | Keys.Shift);
                            return true;
                        }
                        else
                        {
                            this.nextColumnIndex = lastColumnIndex;
                            this.nextRowIndex = this.Rows.GetPreviousRow(this.CurrentRowIndex, DataGridViewElementStates.Visible);
                        }
                    }
                    else
                    {
                        this.nextColumnIndex = this.Columns.GetPreviousColumn(this.Columns[this.CurrentColumnIndex], DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
                        this.nextRowIndex = this.CurrentRowIndex;
                    }
                }
                else
                {
                    ////last cell
                    if (this.CurrentColumnIndex == lastColumnIndex)
                    {
                        ////last row
                        if (this.CurrentRowIndex == rowCount - 1)
                        {
                            this.nextColumnIndex = -1;
                            this.nextRowIndex = -1;
                            ////navigate to next control if next cell not exist 
                            base.ProcessDialogKey(Keys.Control | Keys.Tab);
                            return true;
                        }
                        else
                        {
                            this.nextColumnIndex = firstColumnIndex;
                            this.nextRowIndex = this.Rows.GetNextRow(this.CurrentRowIndex, DataGridViewElementStates.None);
                        }
                    }
                    else
                    {
                        this.nextColumnIndex = this.Columns.GetNextColumn(this.Columns[this.CurrentColumnIndex], DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
                        this.nextRowIndex = this.CurrentRowIndex;
                    }
                }
            }

            ////pageup
            if (e.KeyValue == 33)
            {
                this.nextColumnIndex = this.CurrentColumnIndex;
                this.nextRowIndex = firstRowIndex;
                if (this.nextRowIndex == this.DeselectSpecifiedRow)
                {
                    this.nextRowIndex += 1;
                }

                setGridCell = true;
            }

            ////pagedown
            if (e.KeyValue == 34)
            {
                this.nextColumnIndex = this.CurrentColumnIndex;
                setGridCell = true;
                this.nextRowIndex = rowCount - 1;
            }

            if (this.NextColumnIndex < 0)
            {
                this.nextColumnIndex = this.CurrentColumnIndex;
            }

            ////not for enter key
            if (e.KeyValue != 13)
            {
                //// block empty record
                if (this.nextRowIndex < 0 || this.nextRowIndex == this.deselectSpecifiedRow || this.nextRowIndex >= rowCount)
                {
                    this.nextRowIndex = this.CurrentRowIndex;
                    return true;
                }

                if (setGridCell)
                {
                    //// check for validity
                    if (!this.Columns[this.nextColumnIndex].Visible)
                    {
                        this.nextColumnIndex = this.CurrentColumnIndex;
                        return true;
                    }

                    ////check for valid index
                    if (this.nextColumnIndex < 0 || this.nextRowIndex < 0)
                    {
                        this.nextColumnIndex = -1;
                        this.nextRowIndex = -1;
                    }

                    this.ClearSelection();
                    this.SetCurrentCellAddressCore(this.nextColumnIndex, this.nextRowIndex, false, false, false);
                    if (this.nextRowIndex >= 0)
                    {
                        this.SetSelectedRowCore(this.NextRowIndex, true);
                    }

                    return true;
                }

                if (this.ApplyStandardBehaviour && !this.OnBeforeRowEnter(new DataGridViewCellEventArgs(this.NextColumnIndex, this.NextRowIndex)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the original row count.
        /// </summary>
        /// <returns>The Original Row Count</returns>
        private int GetOriginalRowCount()
        {
            if (this.baseDataView == null)
            {
                return 0;
            }

            if (this.emptyRecordColumnExists)
            {
                DataView tempDataView = this.baseDataView.ToTable().DefaultView;

                tempDataView.RowFilter = string.Concat(this.emptyRecordColumnName, "= true");

                return this.baseDataView.Count - tempDataView.Count;
            }

            return this.baseDataView.Count;
        }

        /// <summary>
        /// Checks whether the record is empty.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>boolean value</returns>
        private bool CheckEmptyRecord(int rowIndex)
        {
            if (this.baseDataView == null)
            {
                return false;
            }
            else
            {
                if (rowIndex > -1 && rowIndex < this.RowCount)
                {
                    if ((this.emptyRecordColumnExists && string.Equals(this.baseDataView[rowIndex][this.emptyRecordColumnName].ToString(), "true", StringComparison.CurrentCultureIgnoreCase) || rowIndex == this.DeselectSpecifiedRow))
                    {
                        return true;
                    }
                }
                else if (rowIndex == -1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Applies the sort expression to the baseDataView.
        /// </summary>     
        private void ApplySortExpression()
        {
            this.sortedGrid = true;
            ////reset edit mode for editonenter - to hide editing control
            if (this.EditMode.Equals(DataGridViewEditMode.EditOnEnter))
            {
                this.EditMode = DataGridViewEditMode.EditOnKeystroke;
                this.editModeChanged = true;
            }

            StringBuilder sortString = new StringBuilder(string.Empty);
            ////default empty row sort
            if (this.emptyRecordColumnExists)
            {
                sortString.Append(this.emptyRecordColumnName);
                sortString.Append(" ASC, ");
            }

            ////append selected column property name
            if (this.BaseSortedColumn != null)
            {
                sortString.Append(this.BaseSortedColumn.DataPropertyName);
                sortString.Append(" ");
                sortString.Append(this.BaseSortOrder);

                ////finding current row key value
                object keyValue = null;
                if (this.baseDataView.Table.Columns.Contains(this.primaryKeyColumnName) && this.CurrentRowIndex > -1)
                {
                    keyValue = this.baseDataView[this.currentRowIndex][this.primaryKeyColumnName];
                }

                ////apply sort expression
                this.baseDataView.Sort = sortString.ToString();
                ////set row after sorting
                this.SetCurrentRow(keyValue);

                if (this.BaseSortedColumn != null)
                {
                    if (this.BaseSortOrder.Equals(GridSortOrder.Asc))
                    {
                        this.BaseSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        this.BaseSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the selectable fields.
        /// </summary>
        private void CheckSelectableFields()
        {
            ////check for row count if one check for its selectability
            if (this.OriginalRowCount == 1 && this.deselectSpecifiedRow == 0)
            {
                this.hasSelectableFields = false;
            }
            else
            {
                this.hasSelectableFields = true;
            }
        }

        #endregion private methods
    }
}
