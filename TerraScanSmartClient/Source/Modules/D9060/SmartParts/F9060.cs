//--------------------------------------------------------------------------------------------
// <copyright file="F9060.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 April 06      VINAYAGAMURTHY H    Created
//*********************************************************************************/

namespace D9060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
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
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// 9060 Class File
    /// </summary>
    public partial class F9060 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// currentAuditTableID variable.
        /// </summary>
        private static int currentAuditTableID;

        /// <summary>
        /// configuredTableName variable.
        /// </summary>
        private string configuredTableName;

        /// <summary>
        /// New TableName variable.
        /// </summary>
        private string newTableName;

        /// <summary>
        /// oldTableName variable.
        /// </summary>
        private string oldTableName;

        /// <summary>
        /// Capture AuditTableName
        /// </summary>
        private string newAuditTableName;

        /// <summary>
        /// oldAuditTableName variable.
        /// </summary>
        private string oldAuditTableName;

        /// <summary>
        /// AuditColumn Flag variable.
        /// </summary>
        private bool auditColumnFlag;

        /// <summary>
        /// Assign flag for Combo Changes.
        /// </summary>
        private bool flagAuditComboChanged;

        /// <summary>
        /// Assign Cancel Mode
        /// </summary>
        private bool cancelFlag;

        /// <summary>
        /// SelectedIndex boolean variable.
        /// </summary>
        private bool protectSelectedIndex;

        /// <summary>
        /// auditColumnData DataSet For Audit Column
        /// </summary>
        private F9060AuditingConfigurationData auditColumnData = new F9060AuditingConfigurationData();

        /// <summary>
        /// listAuditColumns DataTable for Audit Column
        /// </summary>
        private F9060AuditingConfigurationData.ListAuditingColumnsDataTableDataTable listAuditColumns = new F9060AuditingConfigurationData.ListAuditingColumnsDataTableDataTable();

        /// <summary>
        /// auditTableData DataSet for Audit Table
        /// </summary>
        private F9060AuditingConfigurationData auditTableData = new F9060AuditingConfigurationData();

        /// <summary>
        /// listAuditTable DataTable for Audit Table
        /// </summary>
        private F9060AuditingConfigurationData.ListAuditingBaseTablesDataTable listAuditTable = new F9060AuditingConfigurationData.ListAuditingBaseTablesDataTable();

        /// <summary>
        /// auditConfiguredTableData DataSet for Configure Audit Table
        /// </summary>
        private F9060AuditingConfigurationData auditConfiguredTableData = new F9060AuditingConfigurationData();

        /// <summary>
        /// listAuditConfigureTables DataTable for Configure Audit Table
        /// </summary>
        private F9060AuditingConfigurationData.ListAuditingBaseTablesDataTable listAuditConfigureTables = new F9060AuditingConfigurationData.ListAuditingBaseTablesDataTable();

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Creates Instance for F9060Controller.
        /// </summary>
        private F9060Controller form9060Control;

        /// <summary>
        /// OperationSmartPart Variable.
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// formHeaderSmartPart Variables
        /// </summary>
        private FormHeaderSmartPart formHeaderSmartPart;

        /// <summary>
        /// f9060Controller Controller.
        /// </summary>
        private F9060Controller form9060Controller;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initialize new Instances of the Class
        /// </summary>
        public F9060()
        {
            this.InitializeComponent();
        }

        #endregion Constructor

        #region EventPublication

        /// <summary>
        /// Event Publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        #endregion EventPublication

        #region Property

        /// <summary>
        /// Creates Property for F9060Controller
        /// </summary>
        [CreateNew]
        public F9060Controller F9060Control
        {
            get { return this.form9060Control as F9060Controller; }
            set { this.form9060Control = value; }
        }

        #endregion Property

        #region EventSubscription

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form9060Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        /// <summary>
        /// Handles the Button Click Event For the WorkSpace
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewButton_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click("", -2);
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
                case "DELETE":
                    this.DeleteButton_Click();
                    break;
            }
        }

        /// <summary>
        /// Sets the form cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetFormCancelButton, Thread = ThreadOption.UserInterface)]
        public void SetFormCancelButton(object sender, DataEventArgs<string> e)
        {
            this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
        }

        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
        }

        #endregion EventSubscription

        #region Methods

        /// <summary>
        /// Loads the WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form9060Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form9060Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form9060Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
            }

            if (this.form9060Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPart = (FormHeaderSmartPart)this.form9060Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart);
                this.FormHeaderWorkSpace.Show(this.formHeaderSmartPart);
            }
            else
            {
                this.formHeaderSmartPart = (FormHeaderSmartPart)this.form9060Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart);
                this.FormHeaderWorkSpace.Show(this.formHeaderSmartPart);
            }

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = this.AccessibleName;
            formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));
        }

        /// <summary>
        /// Load the Base tables other than Configure tables
        /// </summary>
        private void LoadAuditTables()
        {
            try
            {
                DataRow customRow = this.listAuditTable.NewRow();
                this.listAuditTable.Clear();

                customRow[this.auditTableData.ListAuditingBaseTables.TableNameColumn.ColumnName] = "<Select table to Add for Audit>";
                customRow[this.auditTableData.ListAuditingBaseTables.TableIDColumn.ColumnName] = "0";
                this.listAuditTable.Rows.Add(customRow);
                this.auditTableData = this.form9060Control.WorkItem.F9060_ListAuditingTables("BASE");
                this.listAuditTable.Merge(this.auditTableData.ListAuditingBaseTables);

                this.TableNameComboBox.DataSource = this.listAuditTable;
                this.TableNameComboBox.DisplayMember = this.auditTableData.ListAuditingBaseTables.TableNameColumn.ColumnName;
                this.TableNameComboBox.ValueMember = this.auditTableData.ListAuditingBaseTables.TableIDColumn.ColumnName;
                ////this.TableNameComboBox.SelectedIndex = 0;
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Load the Audit configured tables
        /// </summary>
        private void LoadAuditConfigureTables()
        {
            try
            {
                this.listAuditConfigureTables.Clear();
                this.auditConfiguredTableData = this.form9060Control.WorkItem.F9060_ListAuditingTables("AUDIT");
                this.listAuditConfigureTables.Merge(this.auditConfiguredTableData.ListAuditingBaseTables);
                this.AuditTableDataGridView.DataSource = this.listAuditConfigureTables;
                this.AuditTableDataGridView.AutoGenerateColumns = false;
                this.AuditTableDataGridView.Columns["AuditTableName"].DataPropertyName = this.listAuditConfigureTables.TableNameColumn.ColumnName;
                if (this.AuditTableDataGridView.OriginalRowCount > this.AuditTableDataGridView.NumRowsVisible)
                {
                    this.AuditTableGridVerticalScroll.Visible = false;
                }
                else
                {
                    this.AuditTableGridVerticalScroll.Visible = true;
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Load the Audit Configure Columns
        /// </summary>
        /// <param name="selectedTableName">Selected Table NAme</param>
        private void LoadAuditingColumns(string selectedTableName)
        {
            try
            {
                this.auditColumnData.Clear();
                this.auditColumnData = this.form9060Control.WorkItem.F9060_ListAuditingColumns(selectedTableName);
                this.listAuditColumns.Clear();
                this.listAuditColumns.Merge(this.auditColumnData.ListAuditingColumnsDataTable);
                this.AuditColumnGridView.DataSource = this.listAuditColumns;
                if (this.AuditColumnGridView.OriginalRowCount > this.AuditColumnGridView.NumRowsVisible)
                {
                    this.AuditColumnGridverticalScrollBar.Visible = false;
                }
                else
                {
                    this.AuditColumnGridverticalScrollBar.Visible = true;
                }
                ////added by Biju on 22/Jun/2009 to fix #1086
                if (this.AuditColumnGridView.Rows.Count > 0)
                {
                    this.AuditColumnGridView.Rows[0].Selected = true;
                }
                else
                {
                    this.AuditColumnGridView.Rows[0].Selected = false;
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Customize the Audit table and Columns DataGrid 
        /// </summary>
        private void CustomizeDataGrid()
        {
            try
            {
                this.AuditTableDataGridView.AutoGenerateColumns = false;
                this.AuditTableDataGridView.Columns["AuditTableName"].DataPropertyName = this.listAuditTable.TableNameColumn.ColumnName;
                this.AuditTableDataGridView.Columns["AuditTableID"].DataPropertyName = this.listAuditTable.TableIDColumn.ColumnName;
                this.AuditTableDataGridView.Columns["AuditTableName"].DisplayIndex = 0;
                this.AuditTableDataGridView.Columns["AuditTableID"].DisplayIndex = 1;
                this.AuditTableDataGridView.PrimaryKeyColumnName = this.listAuditTable.TableIDColumn.ColumnName;

                this.AuditColumnGridView.AutoGenerateColumns = false;
                this.AuditColumnGridView.RemoveDefaultSelection = false;
                this.AuditColumnGridView.Columns["AuditColumnName"].DataPropertyName = this.listAuditColumns.FieldNameColumn.ColumnName;
                this.AuditColumnGridView.Columns["IsAudit"].DataPropertyName = this.listAuditColumns.AuditColumn.ColumnName;
                this.AuditColumnGridView.Columns["FieldID"].DataPropertyName = this.listAuditColumns.FieldIDColumn.ColumnName;
                this.AuditColumnGridView.Columns["AuditColumnName"].DisplayIndex = 0;
                this.AuditColumnGridView.Columns["IsAudit"].DisplayIndex = 1;
                this.AuditColumnGridView.Columns["FieldID"].DisplayIndex = 2;
                this.AuditColumnGridView.PrimaryKeyColumnName = "FieldID";
                this.AuditColumnGridView.Columns["IsAudit"].ReadOnly = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Audit Configuration Form Load Methods
        /// </summary>
        private void AuditFormLoad()
        {
            try
            {
                this.LoadAuditTables();
                this.TableNameComboBox.Enabled = false;
                this.AuditTableDataGridView.Enabled = true;
                this.LoadAuditConfigureTables();
                this.LoadAuditingColumns(AuditTableDataGridView.Rows[0].Cells[0].Value.ToString());
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// New Button Click method
        /// </summary>
        private void NewButton_Click()
        {
            try
            {
                this.LoadAuditTables();
                this.TableNameComboBox.Enabled = true;
                this.AuditTableDataGridView.DataSource = null;
                this.AuditColumnGridView.DataSource = null;

                this.LoadAuditConfigureTables();
                this.AuditTableDataGridView.Rows[0].Cells[0].Selected = false;
                this.AuditColumnGridView.Rows[0].Cells[0].Selected = false;
                this.TableNameComboBox.Focus();
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.AuditTableDataGridView.Enabled = false;
                this.flagAuditComboChanged = true;

                if (this.PermissionFiled.newPermission)
                {
                    this.AuditColumnGridView.Enabled = true;
                    this.AuditTableDataGridView.Enabled = true;
                }
                ////Added by Biju on 22/Jun/2009 to fix #1084
                this.AuditTableDataGridView.Enabled = false;

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Save Button click method
        /// </summary>
        /// <param name="auditConTableName">Table Name</param>
        /// <param name="girdFocusID">Focus id for particular Cell</param>
        private void SaveButton_Click(string auditConTableName, int girdFocusID)
        {
            try
            {
                string findExp = "Audit = 1";
                DataRow[] checkDuplicateOrder = this.listAuditColumns.Select(findExp);
                if ((this.AuditColumnGridView.OriginalRowCount == 0) || (checkDuplicateOrder.Length == 0) || (this.TableNameComboBox.SelectedIndex == 0 && this.TableNameComboBox.Enabled))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(auditConTableName))
                {
                    if (this.TableNameComboBox.Enabled)
                    {
                        this.configuredTableName = this.TableNameComboBox.Text.ToString();
                    }
                    else
                    {
                        this.configuredTableName = this.AuditTableDataGridView.CurrentCell.Value.ToString();
                    }
                }
                else
                {
                    this.configuredTableName = auditConTableName;
                }

                this.listAuditColumns.GetChanges();
                this.form9060Control.WorkItem.F9060_SaveAuditConfiguration(this.configuredTableName, (Utility.GetXmlString(this.listAuditColumns.Copy())), TerraScanCommon.UserId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                if (this.TableNameComboBox.Enabled)
                {
                    this.LoadAuditTables();
                }

                this.TableNameComboBox.Enabled = false;
                this.AuditTableDataGridView.Enabled = true;
                this.LoadAuditConfigureTables();

                if (girdFocusID == -2)
                {
                    this.LoadAuditingColumns(this.configuredTableName);
                    this.GridFocusCell(this.configuredTableName);
                }
                else
                {
                    this.LoadAuditingColumns(this.AuditTableDataGridView.Rows[girdFocusID].Cells[0].Value.ToString());
                    this.GridFocusCell(this.AuditTableDataGridView.Rows[girdFocusID].Cells[0].Value.ToString());
                }

                this.AuditTableDataGridView.Focus();
                this.auditColumnFlag = false;
                this.flagAuditComboChanged = true;

                if (!this.PermissionFiled.editPermission)
                {
                    this.AuditColumnGridView.Enabled = false;
                    this.AuditTableDataGridView.Enabled = true;
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Delete Configures table method
        /// </summary>
        private void DeleteButton_Click()
        {
            try
            {
                DialogResult dialogResult;
                int tableAuditID;
                string tableAuditName;

                if (this.AuditTableDataGridView.OriginalRowCount > 0)
                {
                    dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("Do you want to Delete this Audit?")), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("Delete Audit"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        currentAuditTableID = this.AuditTableDataGridView.CurrentRowIndex;
                        tableAuditID = Convert.ToInt32(this.listAuditConfigureTables.Rows[this.AuditTableDataGridView.CurrentRowIndex]["TableID"].ToString());
                        this.form9060Control.WorkItem.F9060_DeleteAuditConfiguration(tableAuditID, TerraScanCommon.UserId);
                        if (this.AuditTableDataGridView.OriginalRowCount - 1 <= currentAuditTableID)
                        {
                            currentAuditTableID = 0;
                        }

                        this.LoadAuditConfigureTables();
                        this.AuditTableDataGridView.Rows[currentAuditTableID].Cells[0].Selected = true;
                        tableAuditName = this.listAuditConfigureTables.Rows[currentAuditTableID]["TableName"].ToString();
                        this.LoadAuditingColumns(tableAuditName);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }

                if (this.AuditTableDataGridView.OriginalRowCount == 0)
                {
                    this.AuditTableDataGridView.Rows[0].Cells[0].Selected = false;
                    this.AuditColumnGridView.Rows[0].Cells[0].Selected = false;
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Cancel method
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.flagAuditComboChanged = false;
                if (this.AuditTableDataGridView.Enabled)
                {
                    this.LoadAuditingColumns(this.listAuditConfigureTables.Rows[this.AuditTableDataGridView.CurrentRowIndex]["TableName"].ToString());
                }
                else if (this.TableNameComboBox.Enabled)
                {
                    this.AuditTableDataGridView.Enabled = true;
                    this.TableNameComboBox.SelectedIndex = 0;
                    this.LoadAuditConfigureTables();
                    this.LoadAuditingColumns(this.AuditTableDataGridView.Rows[0].Cells[0].Value.ToString());
                    this.TableNameComboBox.Enabled = false;
                }

                if (this.AuditTableDataGridView.OriginalRowCount == 0)
                {
                    this.AuditTableDataGridView.Rows[0].Cells[0].Selected = false;
                    this.AuditColumnGridView.Rows[0].Cells[0].Selected = false;
                }
                else
                {
                    this.AuditTableDataGridView.Focus();
                    //this.AuditTableDataGridView.Rows[0].Cells[0].Selected = true;
                }

                this.auditColumnFlag = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                ////Added by Biju on 22/Jun/2009 to fix #1084
                this.AuditTableDataGridView.Enabled = true;

                if (!this.PermissionFiled.editPermission)
                {
                    this.AuditColumnGridView.Enabled = false;
                }
                ////Added by Biju on 22/Jun/2009 to fix #1083
                this.TableNameComboBox.SelectedIndex = 0;
                this.TableNameComboBox.Enabled = false;
                ////till here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Grid to be Focus based on the FocusTable
        /// </summary>
        /// <param name="girdFocusTable">Grid FocusTable</param>
        private void GridFocusCell(string girdFocusTable)
        {
            for (int tableFocus = 0; tableFocus < this.AuditTableDataGridView.OriginalRowCount; tableFocus++)
            {
                if (this.AuditTableDataGridView.Rows[tableFocus].Cells[0].Value.ToString() == girdFocusTable)
                {
                    this.AuditTableDataGridView.Rows[tableFocus].Cells[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// Perform Cancel operation
        /// </summary>
        /// <param name="cancelledTableName">TableName</param>
        private void DialogCancelMode(string cancelledTableName)
        {
            this.GridFocusCell(cancelledTableName);
            this.LoadAuditingColumns(cancelledTableName);
        }

        /// <summary>
        /// Table to be Saved before Cancelled the Operation
        /// </summary>
        /// <param name="commitTableName">Table Name</param>
        /// <param name="gridFocusID">Focus ID</param>
        private void SelectionGridChangeCommited(string commitTableName, int gridFocusID)
        {
            try
            {
                if (this.operationSmartPart.SaveButtonEnable)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName + "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.SaveButton_Click(commitTableName, gridFocusID);
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.LoadAuditingColumns(this.AuditTableDataGridView.CurrentCell.Value.ToString());
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        if (!string.IsNullOrEmpty(this.oldTableName))
                        {
                            this.DialogCancelMode(this.oldTableName);
                            this.newTableName = this.oldTableName;
                        }
                        else
                        {
                            this.oldTableName = this.AuditTableDataGridView.Rows[0].Cells[0].Value.ToString();
                            this.DialogCancelMode(this.oldTableName);
                            this.newTableName = this.oldTableName;
                        }

                        this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                        return;
                    }
                }
                else
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Selected column to be Saved before cancel the Operation.
        /// </summary>
        private void SelectionComboChangeCommited()
        {
            try
            {
                if (this.operationSmartPart.SaveButtonEnable)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName + "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.SaveButton_Click(this.oldAuditTableName, -2);
                        this.auditColumnFlag = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.TableNameComboBox.Enabled = true;
                        this.TableNameComboBox.Focus();
                        this.TableNameComboBox.Text = this.newAuditTableName;
                        this.AuditTableDataGridView.Enabled = false;
                        ////Added by Biju on 22/Jun/09 to remove the focus 
                        this.AuditTableDataGridView.ClearSelection();

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.LoadAuditingColumns(this.TableNameComboBox.Text.ToString());
                        this.auditColumnFlag = false;
                        this.TableNameComboBox.Focus();
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        this.newAuditTableName = this.oldAuditTableName;
                        this.cancelFlag = false;
                        this.TableNameComboBox.Text = this.oldAuditTableName;
                        this.cancelFlag = true;
                        this.TableNameComboBox.Focus();
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                        return;
                    }
                }
                else
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Table Selection method.
        /// </summary>
        /// <param name="rowIndexId">Row IndexID</param>
        private void TableSelection(int rowIndexId)
        {
            try
            {

                if ((this.AuditTableDataGridView.Rows[rowIndexId].Cells["AuditTableID"].Value != null) && (!string.IsNullOrEmpty(this.AuditTableDataGridView.Rows[rowIndexId].Cells["AuditTableID"].Value.ToString())))
                {
                    this.newTableName = this.AuditTableDataGridView.Rows[rowIndexId].Cells[0].Value.ToString();
                    if (this.operationSmartPart.SaveButtonEnable)
                    {
                        this.SelectionGridChangeCommited(this.oldTableName, rowIndexId);
                    }
                    else
                    {
                        this.LoadAuditingColumns(AuditTableDataGridView.Rows[rowIndexId].Cells[0].Value.ToString());
                    }

                    this.oldTableName = this.newTableName;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PageStatus should be checked when closing the form
        /// </summary>
        /// <returns>boolean Value</returns>
        private bool CheckPageStatus()
        {
            if (String.Compare(this.pageMode.ToString(), TerraScanCommon.PageModeTypes.View.ToString(), true) != 0)
            {
                DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName + "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DataRow[] checkOrderWhenClosing = this.listAuditColumns.Select("Audit = 1");
                    this.listAuditColumns.GetChanges();
                    if ((this.AuditColumnGridView.OriginalRowCount == 0) || (checkOrderWhenClosing.Length == 0) || (this.TableNameComboBox.SelectedIndex != 0))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (this.TableNameComboBox.Enabled)
                    {
                        this.form9060Control.WorkItem.F9060_SaveAuditConfiguration(this.TableNameComboBox.Text, (Utility.GetXmlString(this.listAuditColumns.Copy())), TerraScanCommon.UserId);
                    }
                    else
                    {
                        this.form9060Control.WorkItem.F9060_SaveAuditConfiguration(this.AuditTableDataGridView.CurrentCell.Value.ToString(), (Utility.GetXmlString(this.listAuditColumns.Copy())), TerraScanCommon.UserId);
                    }

                    return true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    this.TableNameComboBox.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// To Invoke the Help Link Click Event 
        /// </summary>
        private void InvokeHelpLinkClickEvent()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                    //HelpEngine.getFormName(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SetEditSaveRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.PermissionFiled.editPermission)
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Form 9060 Load Event
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Args</param>
        private void F9060_Load(object sender, EventArgs e)
        {
            try
            {
                this.flagAuditComboChanged = false;
                this.protectSelectedIndex = true;
                this.auditColumnFlag = false;
                this.cancelFlag = true;
                this.LoadWorkSpaces();
                this.CustomizeDataGrid();
                this.AuditFormLoad();
                if (this.AuditTableDataGridView.OriginalRowCount > 0)
                {
                    this.AuditTableDataGridView.Focus();
                }
                else
                {
                    this.HelpLink.Focus();
                }
                if (!this.PermissionFiled.editPermission)
                {
                    this.AuditColumnGridView.Enabled = false;
                    this.AuditTableDataGridView.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AuditTableDataGridView CellClick Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void AuditTableDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    this.TableSelection(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AuditTableDataGridView KeyDown Event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Key Event Args</param>
        private void AuditTableDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.TableSelection(this.AuditTableDataGridView.CurrentRowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AuditTableDataGridView KeyUp Event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Key Event Args</param>
        private void AuditTableDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.TableSelection(this.AuditTableDataGridView.CurrentRowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Audit ColumnGridView CellClick Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void AuditColumnGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    if (this.AuditColumnGridView.CurrentCell != null && this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.AuditColumnGridView.Columns["AuditColumnName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                        this.AuditColumnGridView.Columns["IsAudit"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    else
                    {
                        this.AuditColumnGridView.Columns["AuditColumnName"].SortMode = DataGridViewColumnSortMode.Programmatic;
                        this.AuditColumnGridView.Columns["IsAudit"].SortMode = DataGridViewColumnSortMode.Programmatic;
                    }
                }
                this.SetEditSaveRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AuditColumnGridView CellContentClick Events
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void AuditColumnGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == 1) && (this.AuditColumnGridView.OriginalRowCount > 0) && (e.RowIndex != -1))
                {
                    this.auditColumnFlag = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    //this.SetEditSaveRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// TableNameComboBox SelectionChangeCommitted Events
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void TableNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ////Added if condition to Fix the TFS issue BugID: 1306 (Exception occurs when we press enter after entering some text in table name drop down.) --- Latha
                if (this.TableNameComboBox.SelectedIndex != -1)
                {
                    this.newAuditTableName = this.listAuditTable.Rows[this.TableNameComboBox.SelectedIndex]["TableName"].ToString();
                    //else
                    //{
                    //    this.newAuditTableName = this.listAuditTable.Rows[0]["TableName"].ToString();
                    //    this.TableNameComboBox.SelectedIndex = 0;
                    //}
                    if (this.operationSmartPart.SaveButtonEnable && this.AuditColumnGridView.OriginalRowCount > 0 && this.auditColumnFlag && this.TableNameComboBox.SelectedIndex != 0)
                    {
                        this.protectSelectedIndex = false;
                        this.SelectionComboChangeCommited();
                    }
                    else
                    {
                        this.LoadAuditingColumns(this.listAuditTable.Rows[this.TableNameComboBox.SelectedIndex]["TableName"].ToString());
                    }

                    this.oldAuditTableName = this.newAuditTableName;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Combo Selection Value Changed Events
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Eargs</param>
        private void TableNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.flagAuditComboChanged && this.TableNameComboBox.SelectedIndex != -1)
                {
                    if (this.cancelFlag)
                    {
                        this.newAuditTableName = this.listAuditTable.Rows[this.TableNameComboBox.SelectedIndex]["TableName"].ToString();
                        if (this.operationSmartPart.SaveButtonEnable && this.AuditColumnGridView.OriginalRowCount > 0 && this.auditColumnFlag && this.TableNameComboBox.SelectedIndex != 0)
                        {
                            if (this.protectSelectedIndex)
                            {
                                this.SelectionComboChangeCommited();
                            }
                            else
                            {
                                this.protectSelectedIndex = true;
                            }
                        }
                        else
                        {
                            this.LoadAuditingColumns(this.listAuditTable.Rows[this.TableNameComboBox.SelectedIndex]["TableName"].ToString());
                        }

                        this.oldAuditTableName = this.newAuditTableName;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        ////Added by Biju on 22/Jun/2009 to fix #1082
                        this.TableNameComboBox.Text = this.newAuditTableName;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Audit Table DataBinding Complete Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void AuditTableDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.AuditTableDataGridView.OriginalRowCount == 0)
            {
                this.AuditTableDataGridView.Rows[0].Selected = false;
                this.HelpLink.Focus();
            }
            if (this.AuditTableDataGridView.OriginalRowCount > 0)
            {
                this.AuditTableDataGridView.Focus();
            }
            else
            {
                this.HelpLink.Focus();
            }
        }

        /// <summary>
        /// Audit Column DataBinding Complete Event
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void AuditColumnGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.AuditColumnGridView.OriginalRowCount == 0)
            {
                this.AuditColumnGridView.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// HelpLink LinkClick Events 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.InvokeHelpLinkClickEvent();
        }

        /// <summary>
        /// HelpMenuItem_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            this.InvokeHelpLinkClickEvent();
        }

        #endregion Events

        private void AuditTableDataGridView_Leave(object sender, EventArgs e)
        {
            if (this.AuditColumnGridView.OriginalRowCount > 0)
            {
                this.AuditColumnGridView.Rows[0].Cells[0].Selected = true;
            }
            else
            {
                this.HelpLink.Focus();
            }
        }

        private void AuditTableDataGridView_Enter(object sender, EventArgs e)
        {

        }

        private void AuditGridPanel_Enter(object sender, EventArgs e)
        {
            if (this.AuditTableDataGridView.OriginalRowCount > 0)
            {
                this.AuditTableDataGridView.Focus();
            }
            else
            {
                this.HelpLink.Focus();
            }
        }

        private void GeneralAudtingPanel_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void TableNamePanel_Leave(object sender, EventArgs e)
        {
            if (this.AuditTableDataGridView.OriginalRowCount > 0)
            {
                this.AuditTableDataGridView.Focus();
            }
            else
            {
                this.HelpLink.Focus();
            }
        }
    }
}

