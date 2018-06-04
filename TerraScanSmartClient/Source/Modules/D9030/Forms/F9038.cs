//--------------------------------------------------------------------------------------------
// <copyright file="F9038.cs" company="Congruent">
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
// 06 Jan 07        Suganth Mani       Modified for stylecop changes
//*********************************************************************************/
namespace D9030
{
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

    /// <summary>
    /// class for 9038
    /// </summary>
    public partial class F9038 : BasePage
    {
        #region Private Members

        /// <summary>
        /// used to  store the layout id
        /// </summary>
        private int layoutId;

        /// <summary>
        /// View Name
        /// </summary>
        private string defualtviewName;

        /// <summary>
        /// Record Count
        /// </summary>
        private int layoutRecordCount;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        ///  Used to  store the layout xml.
        /// </summary>
        private string newLayoutXml;

        /// <summary>
        /// Used to store the ViewID
        /// </summary>
        private int newViewId;

        /// <summary>
        /// controller F9038
        /// </summary>
        private F9038Controller form9038Control;

        /// <summary>
        /// layoutDetails
        /// </summary>
        private LoadLayoutDetails loadLayout;

        /// <summary>
        /// typed dataset for layout
        /// </summary>
        private F9038LayoutManagementData layoutManagementData;

        /// <summary>
        /// field to identify the pagemode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// currently active record index
        /// </summary>
        private int currentRecordIndex;

        /// <summary>
        /// member to store current userid
        /// </summary>
        private int currentUserId;

        /// <summary>
        /// Flag to identify form load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// flag to identify modified at queryengine
        /// </summary>
        private bool flagModifiedAtQueryEngine;

        /// <summary>
        /// flag to identify load in active Row
        /// </summary>
        private bool IsActiveRow = false;
        /// <summary>
        /// id of the modified layoout
        /// </summary>
        private int modifiedLayoutId;

        /// <summary>
        /// datamember for masterform no
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// selected xml
        /// </summary>
        private string selectedLayout;

        /// <summary>
        /// name of the queryview
        /// </summary>
        private string queryViewName;

        /// <summary>
        /// permission fields from the form formmaster
        /// </summary>
        private PermissionFields formMasterPermissionFields;

        /// <summary>
        /// the layoutid of the queryengine
        /// </summary>
        private int loadedLayoutId;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9038"/> class.
        /// </summary>
        /// <param name="layoutXml">The layout XML.</param>
        /// <param name="viewId">The view id.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="queryViewName">Name of the query view.</param>
        public F9038(string layoutXml, string viewId, string viewName, int formNo, string queryViewName)
        {
            try
            {
                //// Store this viewId ,newXml
                this.InitializeComponent();
                this.layoutManagementData = new F9038LayoutManagementData();
                this.newViewId = Convert.ToInt32(viewId);
                this.masterFormNo = formNo;
                this.loadLayout.MasterFormNo = formNo;
                this.queryViewName = queryViewName;
                this.newLayoutXml = layoutXml;
                this.selectedLayout = layoutXml;
                //  this.CustomiseDataGrid();
                if (viewName.Contains("(Modified)"))
                {
                    this.flagModifiedAtQueryEngine = true;
                }
                else
                {
                    this.flagModifiedAtQueryEngine = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            //// Checks Layout Changed  or Not
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9038"/> class.
        /// </summary>
        /// <param name="layoutXml">The layout XML.</param>
        /// <param name="viewId">The view id.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="queryViewName">Name of the query view.</param>
        /// <param name="formMasterPermission">The form master permission.</param>
        public F9038(string layoutXml, string viewId, string viewName, int formNo, string queryViewName, PermissionFields formMasterPermission)
        {
            try
            {
                //// Store this viewId ,newXml
                this.InitializeComponent();
                this.formMasterPermissionFields = formMasterPermission;
                this.layoutManagementData = new F9038LayoutManagementData();
                this.newViewId = Convert.ToInt32(viewId);
                this.masterFormNo = formNo;
                this.loadLayout.MasterFormNo = formNo;
                this.queryViewName = queryViewName;
                this.newLayoutXml = layoutXml;
                this.selectedLayout = layoutXml;
                //this.CustomiseDataGrid();
                if (viewName.Contains("(Modified)"))
                {
                    this.flagModifiedAtQueryEngine = true;
                }
                else
                {
                    this.flagModifiedAtQueryEngine = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            //// Checks Layout Changed  or Not
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9038"/> class.
        /// </summary>
        /// <param name="layoutXml">The layout XML.</param>
        /// <param name="viewId">The view id.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="queryViewName">Name of the query view.</param>
        /// <param name="formMasterPermission">The form master permission.</param>
        /// <param name="queryEngineLayoutId">The query engine layout id.</param>
        /// <param name="layoutModified">Flag for modified layout</param>
        public F9038(string layoutXml, string viewId, string viewName, int formNo, string queryViewName, PermissionFields formMasterPermission, int queryEngineLayoutId, bool layoutModified)
        {
            try
            {
                //// Store this viewId, newXml
                this.InitializeComponent();
                this.formMasterPermissionFields = formMasterPermission;
                this.loadedLayoutId = queryEngineLayoutId;
                this.layoutManagementData = new F9038LayoutManagementData();
                this.newViewId = Convert.ToInt32(viewId);
                this.masterFormNo = formNo;
                this.loadLayout.MasterFormNo = formNo;
                this.queryViewName = queryViewName;
                this.newLayoutXml = layoutXml;
                this.selectedLayout = layoutXml;
                this.flagModifiedAtQueryEngine = layoutModified;
                if (this.loadedLayoutId.Equals(0))
                {
                    this.flagModifiedAtQueryEngine = false;
                }

                this.CustomiseDataGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            //// Checks Layout Changed  or Not
        }

        #endregion

        /// <summary>
        /// Declare the event SetRecordCount        
        /// </summary> 
        [EventPublication(EventTopicNames.D9030_F9038_LoadLayoutDetails, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<LoadLayoutDetails>> D9030_F9038_LoadLayoutDetails;

        #region Properties

        /// <summary>
        /// For F9075Control
        /// </summary>
        [CreateNew]
        public F9038Controller Form9038Control
        {
            get { return this.form9038Control as F9038Controller; }
            set { this.form9038Control = value; }
        }

        /// <summary>
        /// For F9075Control
        /// </summary>
        public LoadLayoutDetails LoadLayoutXmldetails
        {
            get { return this.loadLayout; }
            set { this.loadLayout = value; }
        }
        #endregion

        /// <summary>
        /// Loads the layout grid.
        /// </summary>
        public void LoadLayoutGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.layoutManagementData = this.form9038Control.WorkItem.LoadLayoutInformation(this.newViewId, TerraScanCommon.UserId);
                this.layoutRecordCount = this.layoutManagementData.AvailableLayoutTable.Rows.Count;
                this.LayoutMgmtDataGrid.DataSource = this.layoutManagementData.AvailableLayoutTable;
                //this.LayoutMgmtDataGridView.DataSource = this.layoutManagementData.AvailableLayoutTable;
                //this.LayoutMgmtVerticalScroll.Width = 17;
                //this.LayoutMgmtDataGridView.Columns[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].Width = 295;
                //this.LayoutMgmtDataGridView.Width = 332;
                //if (this.layoutRecordCount > this.LayoutMgmtDataGridView.NumRowsVisible)
                //{
                //    this.LayoutMgmtVerticalScroll.Visible = false;
                //}
                //else
                //{
                //    this.LayoutMgmtVerticalScroll.Visible = true;
                //}
                //this.LayoutMgmtDataGrid.cel[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].Width = 295;

                this.LayoutMgmtDataGrid.Width = 332;
                if (this.layoutRecordCount < 0)
                {
                    this.LoadButton.Enabled = false;
                }
                else
                {
                    this.LoadButton.Enabled = true;
                }
                if (this.LayoutMgmtDataGrid.Rows.Count > 9)
                {
                    this.LayoutMgmtDataGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
                    this.LayoutMgmtDataGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;

                }
                else
                {
                    this.LayoutMgmtDataGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
                    this.LayoutMgmtDataGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;

                }
                //this.ScrollBarVisibility(); 
                this.SetColorForDefaultItem();
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
        /// Called when [D9030_ F9038_ load layout details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9038_LoadLayoutDetails(TerraScan.Infrastructure.Interface.EventArgs<LoadLayoutDetails> eventArgs)
        {
            if (this.D9030_F9038_LoadLayoutDetails != null)
            {
                this.D9030_F9038_LoadLayoutDetails(this, eventArgs);
            }
        }

        /// <summary>
        /// Customises the data grid.
        /// </summary>
        private void CustomiseDataGrid()
        {
            //DataGridViewColumnCollection layoutColumns = this.LayoutMgmtDataGrid;
            //this.LayoutMgmtDataGrid.PrimaryKeyColumnName = this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName;
            //this.LayoutMgmtDataGrid.AutoGenerateColumns = false;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.UserIDColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.UserIDColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.Name_DisplayColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.Name_DisplayColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.IsAllUsersColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.IsAllUsersColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.IsTSOnlyColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.IsTSOnlyColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.IsDefault1Column.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.IsDefault1Column.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.QueryViewColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.QueryViewColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.LayoutXMLColumn.ColumnName].DataPropertyName = this.layoutManagementData.AvailableLayoutTable.LayoutXMLColumn.ColumnName;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].DisplayIndex = 0;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName].DisplayIndex = 1;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.UserIDColumn.ColumnName].DisplayIndex = 2;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.Name_DisplayColumn.ColumnName].DisplayIndex = 3;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName].DisplayIndex = 4;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.IsTSOnlyColumn.ColumnName].DisplayIndex = 5;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.IsDefault1Column.ColumnName].DisplayIndex = 6;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.QueryViewColumn.ColumnName].DisplayIndex = 7;
            //layoutColumns[this.layoutManagementData.AvailableLayoutTable.LayoutXMLColumn.ColumnName].DisplayIndex = 8;
            //this.LayoutMgmtDataGridView.PrimaryKeyColumnName = this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName;
        }

        /// <summary>
        /// Handles the Load event of the F9038 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9038_Load(object sender, EventArgs e)
        {
            try
            {
                //// Load the dataGrid
                this.NewMenu.Click += new EventHandler(this.NewLayoutButton_Click);
                this.SaveMenu.Click += new EventHandler(this.SaveButton_Click);
                this.CloseButton.Enabled = true;
                this.CloseButton.Visible = true;
                this.CloseButton.SendToBack();
                this.CancelButton = this.LayoutCancelButton;
                this.flagFormLoad = true;
                this.LoadLayoutGrid();
                this.formNo = 9038;
                this.keyField = "QueryViewID";
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.loadedLayoutId < 0)
                {
                    if (this.layoutRecordCount > 0)
                    {
                        if (this.currentRecordIndex < 0)
                        {
                            this.currentRecordIndex = 0;
                        }

                        this.SetControlValues(this.currentRecordIndex);
                        //   TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
                    }
                }
                else
                {
                    ///// Assign to Defauls Layout Details
                    this.SetDefaultLayout();
                    if (this.loadedLayoutId <= 0)
                    {
                        this.ControlLock(true);
                        this.LayoutMgmtDataGrid.Layouts.Clear();
                        this.currentRecordIndex = 0;
                    }
                }

                if (this.layoutRecordCount > 0)
                {
                    // todo:
                    if (this.flagModifiedAtQueryEngine)
                    {
                        this.modifiedLayoutId = this.layoutId;
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        this.EnableNullRecordMode(false);
                    }
                }
                else
                {
                    this.LayoutNameTextBox.LockKeyPress = true;
                    this.VisableToAllUserCheckBox.Enabled = false;
                    this.FromTerraScanCheckBox.Enabled = false;
                    this.DefaultLayoutRadioButton.Enabled = false;
                    //this.AuditlinkLabel.Text = "tTs_FormViewLayout[FormLayoutID: ";
                    //this.AuditlinkLabel.Left = (this.FormLinePanel.Left + this.FormLinePanel.Width) - this.AuditlinkLabel.Width;
                    //this.AuditlinkLabel.Enabled = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    this.LayoutMgmtDataGrid.Enabled = false;
                    this.NewLayoutButton.Focus();
                }
                ////this.Text = this.form9038Control.WorkItem.GetFormTitle(Convert.ToInt32(this.Tag));
                this.flagFormLoad = false;
                if (TerraScanCommon.IsFieldUser)
                {
                    this.DeleteButton.Enabled = false; 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the LoadButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.LayoutMgmtDataGrid.ActiveRow.Index >= 0)
                {
                    this.LoadSelectedLayout();
                }
                if (TerraScanCommon.IsFieldUser)
                {
                    this.DeleteButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the selected layout.
        /// </summary>
        private void LoadSelectedLayout()
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Are you sure you want to loose the changes", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.loadLayout.LayoutID = this.layoutId;
                        this.loadLayout.LayoutName = this.LayoutNameTextBox.Text.Replace("(Modified)", "");
                        this.loadLayout.LayoutXml = this.newLayoutXml;
                        this.OnD9030_F9038_LoadLayoutDetails(new TerraScan.Infrastructure.Interface.EventArgs<LoadLayoutDetails>(this.loadLayout));
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
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.loadLayout.LayoutID = this.layoutId;
                    this.loadLayout.LayoutName = this.LayoutNameTextBox.Text.Replace("(Modified)", "");
                    this.loadLayout.LayoutXml = this.newLayoutXml;
                    this.OnD9030_F9038_LoadLayoutDetails(new TerraScan.Infrastructure.Interface.EventArgs<LoadLayoutDetails>(this.loadLayout));
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

        ///// <summary>
        ///// Handles the CellClick event of the LayoutMgmtDataGridView control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        //private void LayoutMgmtDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        ////RowIndex has been checked to avoid pop up on ColumnHeader click - Latha
        //        if (!this.flagFormLoad && e.RowIndex >= 0)
        //        {
        //            this.ControlLock(false);
        //            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
        //            {
        //                DialogResult dialogResult;
        //                dialogResult = MessageBox.Show("Do you want to save the layout information", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //                if (dialogResult == DialogResult.Yes)
        //                {
        //                    if (e.RowIndex >= 0)
        //                    {
        //                        if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
        //                        {
        //                            MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        }
        //                        else
        //                        {
        //                            if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
        //                            {
        //                                MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                                return;
        //                            }

        //                            this.SaveLayout();
        //                            this.currentRecordIndex = e.RowIndex;
        //                            this.SetControlValues(e.RowIndex);
        //                            TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, e.RowIndex);
        //                        }
        //                    }
        //                }
        //                else if (dialogResult == DialogResult.No)
        //                {
        //                    this.flagModifiedAtQueryEngine = false;
        //                    this.currentRecordIndex = e.RowIndex;
        //                    this.SetControlValues(e.RowIndex);
        //                    this.pageMode = TerraScanCommon.PageModeTypes.View;
        //                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
        //                }
        //                else
        //                {
        //                    TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                if (e.RowIndex >= 0)
        //                {
        //                    this.currentRecordIndex = e.RowIndex;
        //                    this.SetControlValues(e.RowIndex);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        //    }
        //}

        /// <summary>
        /// Sets the default layout.
        /// </summary>
        private void SetDefaultLayout()
        {
            int defaultSelectedRow = 0;
            if (this.layoutRecordCount > 0)
            {
                if (this.flagModifiedAtQueryEngine || defaultSelectedRow == 0)
                {
                    try
                    {
                        DataView defaultView = new DataView(this.layoutManagementData.AvailableLayoutTable);
                        defaultView.Sort = this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName;
                        foreach (DataRow userRow in defaultView.Table.Rows)
                        {
                            defaultSelectedRow = defaultView.Find(this.loadedLayoutId);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                    }
                }

                this.SetControlValues(defaultSelectedRow);
                this.currentRecordIndex = defaultSelectedRow;
                //  TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, defaultSelectedRow);
                this.flagFormLoad = true;
            }
            //else
            //{
            ////added by vijayakumar
            if (!string.IsNullOrEmpty(this.queryViewName))
            {
                this.LayoutIndicatorlabel.Text = "Layouts for " + this.queryViewName;
            }
            //}
        }

        /// <summary>
        /// Sets the control values. 
        /// </summary>
        /// <param name="rowSelected">The row selected.</param>
        private void SetControlValues(int rowSelected)
        {
            if (rowSelected >= 0)
            {
                this.flagFormLoad = true;
                Boolean isAllUser, isDefualt, isTsuser;
                // DataGridViewRow selectedRows = this.LayoutMgmtDataGrid.Rows[rowSelected];
                this.LayoutNameTextBox.Text = this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].Value.ToString();
                int.TryParse(this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName].Value.ToString(), out this.layoutId);

                this.CreatedBYTextBox.Text = this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.Name_DisplayColumn.ColumnName].Value.ToString();
                int.TryParse(this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.UserIDColumn.ColumnName].Value.ToString(), out this.currentUserId);
                bool.TryParse(this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.IsAllUsersColumn.ColumnName].Value.ToString(), out isAllUser);
                bool.TryParse(this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName].Value.ToString(), out isDefualt);
                bool.TryParse(this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.IsTSOnlyColumn.ColumnName].Value.ToString(), out isTsuser);
                this.VisableToAllUserCheckBox.Checked = isAllUser;
                this.DefaultLayoutRadioButton.Checked = isDefualt;
                this.FromTerraScanCheckBox.Checked = isTsuser;
                if (this.flagModifiedAtQueryEngine)
                {
                    if (this.loadedLayoutId == this.layoutId)
                    {
                        this.LayoutNameTextBox.Text += "(Modified)";
                    }
                    else if (this.modifiedLayoutId.Equals(0))
                    {
                        this.LayoutNameTextBox.Text += "(Modified)";
                    }
                }

                int.TryParse(this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.QueryLayoutIDColumn.ColumnName].Value.ToString(), out this.layoutId);
                //this.AuditlinkLabel.Text = "tTs_FormViewLayout[FormLayoutID: " + this.layoutId + "]";
                //this.AuditlinkLabel.Left = (this.FormLinePanel.Left + this.FormLinePanel.Width) - this.AuditlinkLabel.Width;
                //this.AuditlinkLabel.Enabled = true;
                if (this.FromTerraScanCheckBox.Checked)
                {
                    this.LayoutNameTextBox.LockKeyPress = true;
                    this.VisableToAllUserCheckBox.Enabled = false;
                    this.DefaultLayoutRadioButton.Enabled = false;
                    this.DeleteButton.Enabled = false;
                }
                else
                {
                    this.LayoutNameTextBox.LockKeyPress = false;
                    this.VisableToAllUserCheckBox.Enabled = true;
                    this.DefaultLayoutRadioButton.Enabled = true;
                    if (this.formMasterPermissionFields.deletePermission && this.FormPermissionFields.deletePermission)
                    {
                        if (!TerraScanCommon.IsFieldUser)
                        {
                            this.DeleteButton.Enabled = true;
                        }
                        else
                        {
                            this.DeleteButton.Enabled = false;
                        }
                    }
                    else
                    {
                        this.DeleteButton.Enabled = false;
                    }
                }

                this.newLayoutXml = this.LayoutMgmtDataGrid.Rows[rowSelected].Cells[this.layoutManagementData.AvailableLayoutTable.LayoutXMLColumn.ColumnName].Value.ToString();
                this.FromTerraScanCheckBox.Enabled = false;
                this.flagFormLoad = false;
                this.LayoutMgmtDataGrid.Rows[rowSelected].Selected = true;
                //       TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, rowSelected);
                this.currentRecordIndex = rowSelected;
            }
        }

        /// <summary>
        /// Saves the layout.
        /// </summary>
        private void SaveLayout()
        {
            StringBuilder layoutManagement = new StringBuilder();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                layoutManagement.Append("<Root>");
                layoutManagement.Append("<Table>");
                layoutManagement.Append("<QueryLayoutID>");
                layoutManagement.Append(this.layoutId);
                layoutManagement.Append("</QueryLayoutID>");
                layoutManagement.Append("<QueryViewID>");
                layoutManagement.Append(this.newViewId);
                layoutManagement.Append("</QueryViewID>");
                layoutManagement.Append("<QueryView>");
                layoutManagement.Append(this.defualtviewName);
                layoutManagement.Append("</QueryView>");
                layoutManagement.Append("<LayoutName>");
                layoutManagement.Append(this.LayoutNameTextBox.Text.Replace("(Modified)", "").Replace("&", ""));
                layoutManagement.Append("</LayoutName>");
                layoutManagement.Append("<UserID>");
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    this.layoutId = 0;
                    layoutManagement.Append(TerraScanCommon.UserId);
                }
                else
                {
                    layoutManagement.Append(this.currentUserId);
                }

                layoutManagement.Append("</UserID>");
                layoutManagement.Append("<IsAllUsers>");
                if (this.VisableToAllUserCheckBox.Checked)
                {
                    layoutManagement.Append(1);
                }
                else
                {
                    layoutManagement.Append(0);
                }

                layoutManagement.Append("</IsAllUsers>");
                layoutManagement.Append("<IsDefault>");
                if (this.DefaultLayoutRadioButton.Checked)
                {
                    layoutManagement.Append(1);
                }
                else
                {
                    layoutManagement.Append(0);
                }

                layoutManagement.Append("</IsDefault>");
                layoutManagement.Append("<IsTSOnly>");
                if (this.FromTerraScanCheckBox.Checked)
                {
                    layoutManagement.Append(1);
                }
                else
                {
                    layoutManagement.Append(0);
                }

                layoutManagement.Append("</IsTSOnly>");
                layoutManagement.Append("<LayoutXML>");
                string currentXml;
                if (this.layoutId == this.modifiedLayoutId)
                {
                    if (this.layoutId == 0)
                    {
                        if (this.flagModifiedAtQueryEngine)
                        {
                            if (this.layoutManagementData.AvailableLayoutTable.Rows.Count > 0)
                            {
                                DialogResult layoutAlert = MessageBox.Show("The layout has been modified at query engine.\n Do you want to save the modified layout", "TerraScan T2", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (layoutAlert == DialogResult.Yes)
                                {
                                    layoutManagement.Append(this.selectedLayout);
                                    currentXml = this.selectedLayout;
                                }
                                else
                                {
                                    layoutManagement.Append(this.newLayoutXml);
                                    currentXml = this.newLayoutXml;
                                }
                            }
                            else
                            {
                                this.flagModifiedAtQueryEngine = false;
                                layoutManagement.Append(this.selectedLayout);
                                currentXml = this.selectedLayout;
                            }
                        }
                        else
                        {
                            layoutManagement.Append(this.selectedLayout);
                            currentXml = this.selectedLayout;
                        }
                    }
                    else
                    {
                        DialogResult layoutAlert = MessageBox.Show("The layout has been modified at query engine.\n Do you want to save the modified layout", "TerraScan T2", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (layoutAlert == DialogResult.Yes)
                        {
                            layoutManagement.Append(this.selectedLayout);
                            currentXml = this.selectedLayout;
                        }
                        else
                        {
                            layoutManagement.Append(this.newLayoutXml);
                            currentXml = this.newLayoutXml;
                        }
                    }
                }
                else
                {
                    if (this.layoutId != 0)
                    {
                        layoutManagement.Append(this.selectedLayout);
                        currentXml = this.selectedLayout;
                    }
                    else
                    {
                        layoutManagement.Append(this.selectedLayout);
                        currentXml = this.selectedLayout;
                    }
                }

                layoutManagement.Append("</LayoutXML>");
                layoutManagement.Append("</Table>");
                layoutManagement.Append("</Root>");

                int currentKey = this.form9038Control.WorkItem.F9038_SaveLayoutInformation(this.layoutId, layoutManagement.ToString(), currentXml, TerraScanCommon.UserId);
                
                this.flagFormLoad = true;
                if (this.modifiedLayoutId == currentKey)
                {
                    this.flagModifiedAtQueryEngine = false;
                    this.modifiedLayoutId = 0;
                }

                this.LoadLayoutGrid();
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                if (this.layoutRecordCount > 0)
                {
                    this.LayoutMgmtDataGrid.Enabled = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
                else
                {
                    this.LayoutMgmtDataGrid.Enabled = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }

                this.NewIndex(currentKey);
                this.SetControlValues(this.currentRecordIndex);
                this.LayoutMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;
                // TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
                this.flagFormLoad = false;
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
        /// News the index.
        /// </summary>
        /// <param name="currentKey">The current key.</param>
        private void NewIndex(int currentKey)
        {
            for (int rowNo = 0; rowNo < this.layoutRecordCount; rowNo++)
            {
                int rowKeyId;
                int.TryParse(this.LayoutMgmtDataGrid.Rows[rowNo].Cells["QueryLayoutID"].Value.ToString(), out rowKeyId);
                if (rowKeyId == currentKey)
                {
                    this.currentRecordIndex = rowNo;
                    this.layoutId = currentKey;
                    return;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SaveButton.Enabled)
                {
                    if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
                    {
                        MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
                        {
                            MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        this.SaveLayout();
                        this.LoadButton.Enabled = true;
                    }
                }
                if (TerraScanCommon.IsFieldUser)
                {
                    this.DeleteButton.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.LayoutCancelButton.DialogResult = DialogResult.None;
                this.CancelOperation();
                this.DialogResult = DialogResult.None;
                if (TerraScanCommon.IsFieldUser)
                {
                    this.DeleteButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.form9038Control.WorkItem.F9038_DeleteLayoutInformation(this.layoutId, TerraScanCommon.UserId);
                this.flagFormLoad = true;
                this.LoadLayoutGrid();
                this.flagFormLoad = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.layoutRecordCount > 0)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
                else
                {
                    this.LayoutNameTextBox.Text = string.Empty;
                    this.VisableToAllUserCheckBox.Checked = false;
                    this.FromTerraScanCheckBox.Checked = false;
                    this.DefaultLayoutRadioButton.Checked = false;
                    this.LayoutNameTextBox.LockKeyPress = true;
                    this.VisableToAllUserCheckBox.Enabled = false;
                    this.FromTerraScanCheckBox.Enabled = false;
                    this.DefaultLayoutRadioButton.Enabled = false;
                    //this.AuditlinkLabel.Text = "tTs_FormViewLayout[FormLayoutID: ";
                    //this.AuditlinkLabel.Left = (this.FormLinePanel.Left + this.FormLinePanel.Width) - this.AuditlinkLabel.Width;
                    //this.AuditlinkLabel.Enabled = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }

                if ((this.currentRecordIndex < this.layoutRecordCount) && (this.layoutRecordCount > 0))
                {
                    if (this.currentRecordIndex < 0)
                    {
                        this.currentRecordIndex = 0;
                    }
                    else
                    {
                        this.SetDefaultLayout();
                    }

                    this.SetControlValues(this.currentRecordIndex);
                    this.LayoutMgmtDataGrid.Rows[this.currentRecordIndex].Selected = true;

                    //    TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
                }
                else
                {
                    this.currentRecordIndex = 0;
                    if (this.LayoutMgmtDataGrid.Rows.Count > 0)
                    {
                        this.SetControlValues(this.currentRecordIndex);
                        //       TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
                    }
                    else
                    {
                        this.LayoutMgmtDataGrid.Enabled = false;
                    }
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
        /// Handles the Click event of the NewLayoutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewLayoutButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.NewLayoutButton.Enabled)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                    this.ClearControlValues();
                    this.CreatedBYTextBox.Text = TerraScanCommon.UserName;
                    this.VisableToAllUserCheckBox.Checked = true;
                    this.FromTerraScanCheckBox.Enabled = false;
                    this.VisableToAllUserCheckBox.Enabled = true;
                    this.DefaultLayoutRadioButton.Enabled = true;
                    this.LayoutNameTextBox.LockKeyPress = false;
                    this.LayoutNameTextBox.Focus();
                    this.ControlLock(false);
                    this.LayoutMgmtDataGrid.Rows.ColumnFilters.ClearAllFilters();
                    this.LayoutMgmtDataGrid.Enabled = false;
                    
                }
                if (TerraScanCommon.IsFieldUser)
                {
                    this.DeleteButton.Enabled = false; 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Clears the control values.
        /// </summary>
        private void ClearControlValues()
        {
            this.flagFormLoad = true;
            this.LayoutNameTextBox.Text = string.Empty;
            this.CreatedBYTextBox.Text = string.Empty;
            this.VisableToAllUserCheckBox.Checked = false;
            this.FromTerraScanCheckBox.Checked = false;
            this.DefaultLayoutRadioButton.Checked = false;
            this.flagFormLoad = false;
        }

        /// <summary>
        /// Sets the buttons.
        /// </summary>
        /// <param name="buttonActionMode">The button action mode.</param>
        private void SetButtons(TerraScanCommon.ButtonActionMode buttonActionMode)
        {
            switch (buttonActionMode)
            {
                case TerraScanCommon.ButtonActionMode.CancelMode:
                    {
                        if (this.formMasterPermissionFields.newPermission && this.FormPermissionFields.newPermission)
                        {
                            this.NewLayoutButton.Enabled = true;
                        }
                        else
                        {
                            this.NewLayoutButton.Enabled = false;
                        }

                        this.LayoutCancelButton.Enabled = false;
                        if (this.formMasterPermissionFields.deletePermission && this.FormPermissionFields.deletePermission)
                        {
                            if (!TerraScanCommon.IsFieldUser)
                            {
                                this.DeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.DeleteButton.Enabled = false;
                            }
                        }
                        else
                        {
                            this.DeleteButton.Enabled = false;
                        }
                        if (TerraScanCommon.IsFieldUser)
                        {
                            this.DeleteButton.Enabled = false;
                        }
                        this.SaveButton.Enabled = false;
                        this.EnableNullRecordMode(false);
                        //if (this.LayoutMgmtDataGrid.ActiveRow != null)
                        //{
                        //    if (this.LayoutMgmtDataGrid.ActiveRow.Selected)
                        //    {
                        //        this.LoadButton.Enabled = true;
                        //    }
                        //    else
                        //    {
                        //        this.LoadButton.Enabled = false;
                        //    }
                        //}
                        //else
                        //{
                        //    this.LoadButton.Enabled = false;
                        //}
                        this.LoadButton.Enabled = true;
                        if (this.LayoutCancelButton.Enabled)
                        {
                            this.CancelButton = this.LayoutCancelButton;
                        }
                        else
                        {
                            this.CancelButton = this.CloseButton;
                        }
                        break;
                    }

                case TerraScanCommon.ButtonActionMode.NewMode:
                    {
                        this.NewLayoutButton.Enabled = false;
                        this.LayoutCancelButton.Enabled = true;
                        this.DeleteButton.Enabled = false;
                        this.SaveButton.Enabled = true;
                        this.EnableNullRecordMode(false);
                        this.LoadButton.Enabled = false;
                        if (this.LayoutCancelButton.Enabled)
                        {
                            this.CancelButton = this.LayoutCancelButton;
                        }
                        else
                        {
                            this.CancelButton = this.CloseButton;
                        }
                        break;
                    }

                case TerraScanCommon.ButtonActionMode.EditMode:
                    {
                        if (this.formMasterPermissionFields.editPermission && this.FormPermissionFields.editPermission)
                        {
                            this.NewLayoutButton.Enabled = false;
                            this.LayoutCancelButton.Enabled = true;
                            this.DeleteButton.Enabled = false;
                            this.SaveButton.Enabled = true;
                            this.EnableNullRecordMode(false);
                            this.LoadButton.Enabled = false;
                        }
                        else
                        {
                            this.CancelOperation();
                        }

                        if (this.LayoutCancelButton.Enabled)
                        {
                            this.CancelButton = this.LayoutCancelButton;
                        }
                        else
                        {
                            this.CancelButton = this.CloseButton;
                        }
                        break;
                    }

                case TerraScanCommon.ButtonActionMode.NullRecordMode:
                    {
                        this.CreatedBYTextBox.Text = string.Empty;
                        if (this.formMasterPermissionFields.newPermission && this.FormPermissionFields.newPermission)
                        {
                            this.NewLayoutButton.Enabled = true;
                        }
                        else
                        {
                            this.NewLayoutButton.Enabled = false;
                        }

                        this.LayoutCancelButton.Enabled = false;
                        this.DeleteButton.Enabled = false;
                        this.SaveButton.Enabled = false;
                        this.EnableNullRecordMode(true);
                        this.LoadButton.Enabled = false;
                        if (this.LayoutCancelButton.Enabled)
                        {
                            this.CancelButton = this.LayoutCancelButton;
                        }
                        else
                        {
                            this.CancelButton = this.CloseButton;
                        }
                        break;
                    }
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
                this.FromTerraScanCheckBox.Enabled = false;
                if (this.flagModifiedAtQueryEngine)
                {
                    this.flagModifiedAtQueryEngine = false;
                }

                if (this.currentRecordIndex >= 0)
                {
                    if (this.LayoutMgmtDataGrid.Rows.Count > 0)
                    {
                        this.LayoutMgmtDataGrid.Enabled = true;
                        this.SetControlValues(this.currentRecordIndex);
                        //this.DeleteButton.Enabled = true; 
                    }
                    else
                    {
                        this.VisableToAllUserCheckBox.Checked = false;
                        this.ClearControlValues();
                        this.FromTerraScanCheckBox.Enabled = false;
                        this.DefaultLayoutRadioButton.Checked = false;
                        this.VisableToAllUserCheckBox.Checked = true;
                        this.VisableToAllUserCheckBox.Enabled = false;
                        this.DefaultLayoutRadioButton.Enabled = false;
                        //this.AuditlinkLabel.Text = "tTs_FormViewLayout[FormLayoutID: ";
                        //this.AuditlinkLabel.Left = (this.FormLinePanel.Left + this.FormLinePanel.Width) - this.AuditlinkLabel.Width;
                        //this.AuditlinkLabel.Enabled = false;
                        this.LayoutMgmtDataGrid.Enabled = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
                else
                {
                    if (this.LayoutMgmtDataGrid.Rows.Count > 0)
                    {
                        this.VisableToAllUserCheckBox.Checked = false;
                        this.ClearControlValues();
                        this.FromTerraScanCheckBox.Enabled = false;
                        this.DefaultLayoutRadioButton.Checked = true;
                        this.VisableToAllUserCheckBox.Checked = true;
                        this.LayoutMgmtDataGrid.Enabled = true;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                    }
                    else
                    {
                        this.VisableToAllUserCheckBox.Checked = false;
                        this.ClearControlValues();
                        this.FromTerraScanCheckBox.Enabled = false;
                        this.DefaultLayoutRadioButton.Checked = true;
                        this.VisableToAllUserCheckBox.Checked = true;
                        this.LayoutMgmtDataGrid.Enabled = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                    //// this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the DefaultLayoutRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DefaultLayoutRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        this.FromTerraScanCheckBox.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the VisableToAllUserCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void VisableToAllUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        this.FromTerraScanCheckBox.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AuditlinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowAuditReport("903890", this.layoutId.ToString());
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
        /// Handles the TextChanged event of the LayoutNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LayoutNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        this.FromTerraScanCheckBox.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the FromTerraScanCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromTerraScanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
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

        ///// <summary>
        ///// Handles the CellDoubleClick event of the LayoutMgmtDataGridView control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        //private void LayoutMgmtDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        ////If condition has been added to avoid form close on ColumnHeader click - Latha
        //        if (e.RowIndex >= 0)
        //        {
        //            this.LoadSelectedLayout();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        //    }
        //}

        ///<summary>
        /// Set the Scroll Bar Visibility
        /// </summary>
        private void ScrollBarVisibility()
        {
            if (this.layoutRecordCount > 9)
            {
                this.vScrollBar1.Visible = false;
            }
            else
            {
                this.vScrollBar1.Visible = true;
            }


        }

        /// <summary>
        /// Sets the color for default item.
        /// </summary>
        private void SetColorForDefaultItem()
        {
            int rowIndex = 0;
            this.currentRecordIndex = -1;
            //foreach (DataGridViewRow currentRow in this.LayoutMgmtDataGridView.Rows)
            //{
            //    rowIndex++;
            //    if (Convert.ToBoolean(currentRow.Cells[this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName].Value))
            //    {
            //        currentRow.Cells[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].Style.BackColor = Color.FromArgb(204, 255, 204);
            //        this.currentRecordIndex = rowIndex - 1;
            //    }
            //}
        }

        /// <summary>
        /// Enables the null record mode.
        /// </summary>
        /// <param name="flagEnable">if set to <c>true</c> [flag enable].</param>
        private void EnableNullRecordMode(bool flagEnable)
        {
            this.LayoutNamePanel.Enabled = !flagEnable;
            this.CreatedBYPanel.Enabled = !flagEnable;
            this.VisableToAllUserPanel.Enabled = !flagEnable;
            this.DefaultLayoutPanel.Enabled = !flagEnable;
            this.FromTerraScanPanel.Enabled = !flagEnable;
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void ControlLock(bool lockControl)
        {
            this.LayoutNameTextBox.LockKeyPress = lockControl;
            if (lockControl)
            {
                this.VisableToAllUserCheckBox.Enabled = false;
                this.DefaultLayoutRadioButton.Enabled = false;
                this.FromTerraScanCheckBox.Enabled = false;

            }
            else
            {
                this.VisableToAllUserCheckBox.Enabled = true;
                this.DefaultLayoutRadioButton.Enabled = true;
                this.FromTerraScanCheckBox.Enabled = true;
            }
            //    this.VisableToAllUserCheckBox.Enabled = !lockControl;
            //    this.DefaultLayoutRadioButton.Enabled = !lockControl;
            //    this.FromTerraScanCheckBox.Enabled = !lockControl;
        }

        #region Default Layout Color
        ////Code has been added for Bug ID 4681: Background color should not get changed in available layout -- Added by Latha

        ///// <summary>
        ///// Handles the CellFormatting event of the LayoutMgmtDataGridView control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        //private void LayoutMgmtDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    try
        //    {
        //        if (Convert.ToBoolean(LayoutMgmtDataGridView.Rows[e.RowIndex].Cells[this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName].Value))
        //        {
        //            LayoutMgmtDataGridView.Rows[e.RowIndex].Cells[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].Style.BackColor = Color.FromArgb(204, 255, 204);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        //    }
        //}
        #endregion Default Layout Color

        ///// <summary>
        ///// Handles the KeyDown event of the LayoutMgmtDataGridView control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        //private void LayoutMgmtDataGridView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        int tempRowIndex = 0;
        //        int tempGirdRowIndex = 0;
        //        this.ControlLock(false);
        //        if (((DataGridView)sender).CurrentCell != null)
        //        {
        //            tempRowIndex = ((DataGridView)sender).CurrentCell.RowIndex;

        //            switch (e.KeyCode)
        //            {
        //                case Keys.Down:
        //                    {
        //                        if ((tempRowIndex + 1) <= this.LayoutMgmtDataGridView.OriginalRowCount - 1)
        //                        {
        //                            tempGirdRowIndex = tempRowIndex + 1;

        //                            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
        //                            {
        //                                this.currentRecordIndex = tempGirdRowIndex;
        //                                this.SetControlValues(this.currentRecordIndex);
        //                                TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
        //                                e.Handled = true;
        //                            }
        //                            else
        //                            {
        //                                DialogResult dialogResult;
        //                                dialogResult = MessageBox.Show("Do you want to save the layout information", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //                                if (dialogResult == DialogResult.Yes)
        //                                {
        //                                    if (tempRowIndex >= 0)
        //                                    {
        //                                        if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
        //                                        {
        //                                            MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                                        }
        //                                        else
        //                                        {
        //                                            if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
        //                                            {
        //                                                MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                                                return;
        //                                            }

        //                                            this.SaveLayout();
        //                                            this.currentRecordIndex = tempGirdRowIndex;
        //                                            this.SetControlValues(this.currentRecordIndex);
        //                                            TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, tempRowIndex);
        //                                        }
        //                                    }
        //                                }
        //                                else if (dialogResult == DialogResult.No)
        //                                {
        //                                    this.flagModifiedAtQueryEngine = false;
        //                                    this.currentRecordIndex = tempGirdRowIndex;
        //                                    this.SetControlValues(this.currentRecordIndex);
        //                                    this.pageMode = TerraScanCommon.PageModeTypes.View;
        //                                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
        //                                }
        //                                else
        //                                {
        //                                    TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
        //                                    return;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            this.FromTerraScanCheckBox.Enabled = false;
        //                        }

        //                        break;
        //                    }

        //                case Keys.Up:
        //                    {
        //                        if ((tempRowIndex - 1) >= 0)
        //                        {
        //                            tempGirdRowIndex = tempRowIndex - 1;

        //                            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
        //                            {
        //                                this.currentRecordIndex = tempGirdRowIndex;
        //                                this.SetControlValues(this.currentRecordIndex);
        //                                TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
        //                                e.Handled = true;
        //                            }
        //                            else
        //                            {
        //                                DialogResult dialogResult;
        //                                dialogResult = MessageBox.Show("Do you want to save the layout information", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //                                if (dialogResult == DialogResult.Yes)
        //                                {
        //                                    if (tempRowIndex >= 0)
        //                                    {
        //                                        if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
        //                                        {
        //                                            MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                                        }
        //                                        else
        //                                        {
        //                                            if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
        //                                            {
        //                                                MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                                                return;
        //                                            }

        //                                            this.SaveLayout();
        //                                            this.currentRecordIndex = tempGirdRowIndex;
        //                                            this.SetControlValues(this.currentRecordIndex);
        //                                            TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, tempRowIndex);
        //                                        }
        //                                    }
        //                                }
        //                                else if (dialogResult == DialogResult.No)
        //                                {
        //                                    this.flagModifiedAtQueryEngine = false;
        //                                    this.currentRecordIndex = tempGirdRowIndex;
        //                                    this.SetControlValues(this.currentRecordIndex);
        //                                    this.pageMode = TerraScanCommon.PageModeTypes.View;
        //                                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
        //                                }
        //                                else
        //                                {
        //                                    TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
        //                                    return;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            this.FromTerraScanCheckBox.Enabled = false;
        //                        }

        //                        break;
        //                    }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        //    }
        //}

        private void LayoutMgmtDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
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
        /// Handles the FormClosing event of the F9038 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9038_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) && this.SaveButton.Enabled)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
                                        {
                                            MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            e.Cancel = true;
                                            break;
                                        }
                                        else
                                        {
                                            if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
                                            {
                                                MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                e.Cancel = true;
                                                break;
                                            }
                                            this.SaveLayout();
                                            e.Cancel = false;

                                            break;
                                        }
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

        private void LayoutMgmtDataGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            this.LayoutMgmtDataGrid.DisplayLayout.Bands[0].Columns["LayoutName"].Header.Caption = "Available Layouts";
            this.LayoutMgmtDataGrid.DisplayLayout.Bands[0].Columns["LayoutName"].Width = 294;
            this.LayoutMgmtDataGrid.DisplayLayout.Bands[0].Columns["LayoutName"].MaxWidth = 294;
            this.LayoutMgmtDataGrid.DisplayLayout.Bands[0].Columns["LayoutXML"].Hidden = true;
            this.LayoutMgmtDataGrid.DisplayLayout.Bands[0].Columns["LayoutName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

        }

        private void LayoutMgmtDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    int tempRowIndex = 0;
            //    int tempGirdRowIndex = 0;
            //    this.ControlLock(false);
            //    if (this.LayoutMgmtDataGrid.ActiveRow.Index != null)
            //    {
            //        tempRowIndex = this.LayoutMgmtDataGrid.ActiveRow.Index;


            //        switch (e.KeyCode)
            //        {
            //            case Keys.Down:
            //                {
            //                    if ((tempRowIndex + 1) <= this.LayoutMgmtDataGrid.Rows.Count - 1)
            //                    {
            //                        tempGirdRowIndex = tempRowIndex + 1;

            //                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //                        {
            //                            this.currentRecordIndex = tempGirdRowIndex;
            //                            this.SetControlValues(this.currentRecordIndex);
            //                            // TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
            //                            e.Handled = true;
            //                        }
            //                        else
            //                        {
            //                            DialogResult dialogResult;
            //                            dialogResult = MessageBox.Show("Do you want to save the layout information", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //                            if (dialogResult == DialogResult.Yes)
            //                            {
            //                                if (tempRowIndex >= 0)
            //                                {
            //                                    if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
            //                                    {
            //                                        MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                                    }
            //                                    else
            //                                    {
            //                                        if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
            //                                        {
            //                                            MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                                            return;
            //                                        }

            //                                        this.SaveLayout();
            //                                        this.currentRecordIndex = tempGirdRowIndex;
            //                                        this.SetControlValues(this.currentRecordIndex);
            //                                        //  TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGrid, tempRowIndex);
            //                                    }
            //                                }
            //                            }
            //                            else if (dialogResult == DialogResult.No)
            //                            {
            //                                this.flagModifiedAtQueryEngine = false;
            //                                this.currentRecordIndex = tempGirdRowIndex;
            //                                this.SetControlValues(this.currentRecordIndex);
            //                                this.pageMode = TerraScanCommon.PageModeTypes.View;
            //                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //                            }
            //                            else
            //                            {
            //                                //TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGrid, this.currentRecordIndex);
            //                                return;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        this.FromTerraScanCheckBox.Enabled = false;
            //                    }

            //                    break;
            //                }

            //            case Keys.Up:
            //                {
            //                    if ((tempRowIndex - 1) >= 0)
            //                    {
            //                        tempGirdRowIndex = tempRowIndex - 1;

            //                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //                        {
            //                            this.currentRecordIndex = tempGirdRowIndex;
            //                            this.SetControlValues(this.currentRecordIndex);
            //                            // TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
            //                            e.Handled = true;
            //                        }
            //                        else
            //                        {
            //                            DialogResult dialogResult;
            //                            dialogResult = MessageBox.Show("Do you want to save the layout information", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //                            if (dialogResult == DialogResult.Yes)
            //                            {
            //                                if (tempRowIndex >= 0)
            //                                {
            //                                    if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
            //                                    {
            //                                        MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                                    }
            //                                    else
            //                                    {
            //                                        if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
            //                                        {
            //                                            MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                                            return;
            //                                        }

            //                                        this.SaveLayout();
            //                                        this.currentRecordIndex = tempGirdRowIndex;
            //                                        this.SetControlValues(this.currentRecordIndex);
            //                                        //  TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, tempRowIndex);
            //                                    }
            //                                }
            //                            }
            //                            else if (dialogResult == DialogResult.No)
            //                            {
            //                                this.flagModifiedAtQueryEngine = false;
            //                                this.currentRecordIndex = tempGirdRowIndex;
            //                                this.SetControlValues(this.currentRecordIndex);
            //                                this.pageMode = TerraScanCommon.PageModeTypes.View;
            //                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //                            }
            //                            else
            //                            {
            //                                //  TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
            //                                return;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        this.FromTerraScanCheckBox.Enabled = false;
            //                    }

            //                    break;
            //                }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            //}
        }

        private void LayoutMgmtDataGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                ////RowIndex has been checked to avoid pop up on ColumnHeader click - Latha
                if (!this.flagFormLoad)
                {
                    this.ControlLock(false);
                    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                    {
                        DialogResult dialogResult;
                        dialogResult = MessageBox.Show("Do you want to save the layout information", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (string.IsNullOrEmpty(this.LayoutNameTextBox.Text) || string.IsNullOrEmpty(this.CreatedBYTextBox.Text))
                            {
                                MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan – Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if ((this.DefaultLayoutRadioButton.Checked == true) && (this.VisableToAllUserCheckBox.Checked == false))
                                {
                                    MessageBox.Show("This record cannot be saved because default layout should be visible to all users..", "TerraScan – Missing Layout information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                this.SaveLayout();
                                this.currentRecordIndex = this.LayoutMgmtDataGrid.ActiveRow.Index;
                                this.SetControlValues(this.LayoutMgmtDataGrid.ActiveRow.Index);
                                //TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, e.RowIndex);
                            }

                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            this.flagModifiedAtQueryEngine = false;
                            this.currentRecordIndex = this.LayoutMgmtDataGrid.ActiveRow.Index;
                            this.SetControlValues(this.LayoutMgmtDataGrid.ActiveRow.Index);
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        }
                        else
                        {
                            // TerraScanCommon.SetDataGridViewPosition(this.LayoutMgmtDataGridView, this.currentRecordIndex);
                            return;
                        }
                    }
                    else
                    {
                        if (this.LayoutMgmtDataGrid.ActiveRow != null)
                        {
                            if (this.LayoutMgmtDataGrid.ActiveRow.Index >= 0)
                            {
                                this.currentRecordIndex = this.LayoutMgmtDataGrid.ActiveRow.Index;
                                if (this.LayoutMgmtDataGrid.ActiveCell != null)
                                {
                                    if (!string.IsNullOrEmpty(this.LayoutMgmtDataGrid.ActiveCell.Text))
                                    {
                                        this.SetControlValues(this.LayoutMgmtDataGrid.ActiveRow.Index);
                                        this.LoadButton.Enabled = true;
                                    }
                                    else
                                    {
                                        this.ControlLock(true);
                                        this.LayoutNameTextBox.Text = string.Empty;
                                        this.LayoutNameTextBox.LockKeyPress = true;
                                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                                    }
                                }
                            }
                            else
                            {
                                this.ClearControlValues();
                                this.ControlLock(true);
                                this.LayoutNameTextBox.Text = string.Empty;
                                this.LayoutNameTextBox.LockKeyPress = true;
                                this.pageMode = TerraScanCommon.PageModeTypes.View;
                                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                if (this.LayoutMgmtDataGrid.Rows.Count > 0)
                                {
                                    this.EnableNullRecordMode(false);
                                    if (this.LayoutMgmtDataGrid.ActiveRow.Index > 0)
                                    {
                                        this.LoadButton.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (TerraScanCommon.IsFieldUser)
                {
                    this.DeleteButton.Enabled = false; 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LayoutMgmtDataGrid_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                UltraGrid grid = (UltraGrid)sender;
                UIElement lastElementEntered = grid.DisplayLayout.UIElement.LastElementEntered;
                UltraGridCell cell = lastElementEntered.GetContext(typeof(UltraGridCell)) as UltraGridCell;
                if (cell.Activated)
                {
                    ////If condition has been added to avoid form close on ColumnHeader click - Latha
                    if (this.LayoutMgmtDataGrid.ActiveCell != null)
                    {
                        if (!string.IsNullOrEmpty(this.LayoutMgmtDataGrid.ActiveCell.Text))
                        {
                            if (this.LayoutMgmtDataGrid.ActiveRow.Index >= 0)
                            {
                                this.LoadSelectedLayout();
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

        private void LayoutMgmtDataGrid_BeforeCellActivate(object sender, CancelableCellEventArgs e)
        {

            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.LayoutMgmtDataGrid.ActiveRow.Index >= 0)
                {
                    this.SetControlValues(this.LayoutMgmtDataGrid.ActiveRow.Index);
                    this.LoadButton.Enabled = true;
                }
            }
        }

        private void LayoutMgmtDataGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row != null)
                {
                    //this.LayoutMgmtDataGrid.ActiveCell.CanEnterEditMode.Equals(false);   
                    if (!string.IsNullOrEmpty(e.Row.Cells[this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName].Value.ToString()))
                    {

                        if (Convert.ToBoolean(e.Row.Cells[this.layoutManagementData.AvailableLayoutTable.IsDefaultColumn.ColumnName].Value))
                        {
                            e.Row.Cells[this.layoutManagementData.AvailableLayoutTable.LayoutNameColumn.ColumnName].Appearance.BackColor = Color.FromArgb(204, 255, 204);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LayoutMgmtDataGrid_FilterCellValueChanged(object sender, FilterCellValueChangedEventArgs e)
        {
            this.ControlLock(true);
            this.LayoutNameTextBox.Text = string.Empty;
            this.LayoutNameTextBox.LockKeyPress = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            if (this.LayoutMgmtDataGrid.Rows.Count > 0)
            {
                this.EnableNullRecordMode(false);
                //this.LoadButton.Enabled = true;  
            }
        }

        private void LayoutMgmtDataGrid_FilterRow(object sender, FilterRowEventArgs e)
        {
            // this.ClearControlValues(); 

        }

        private void LayoutMgmtDataGrid_BeforeRowActivate(object sender, RowEventArgs e)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                this.SetControlValues(e.Row.Index);
                this.currentRecordIndex = e.Row.Index;
            }
        }

        private void LayoutMgmtDataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void LayoutMgmtDataGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void LayoutMgmtDataGrid_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}