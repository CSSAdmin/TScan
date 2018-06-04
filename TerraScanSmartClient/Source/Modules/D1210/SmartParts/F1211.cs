//--------------------------------------------------------------------------------------------
// <copyright file="F1211.cs" company="Congruent">
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
// 09 Oct 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F1211 class file
    /// </summary>
    public partial class F1211 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Created Instance for f1105Controller
        /// </summary>
        private F1211Controller form1211Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// DisbursementCheckStagingData object created
        /// </summary>
        private DisbursementCheckStagingData disbursementCheckDataset = new DisbursementCheckStagingData();
        
        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private int rowCountAgency;

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private int rowCountCheckDetail;

        /// <summary>
        /// Created Integer for selected.
        /// </summary>
        private int selected;

        /// <summary>
        /// editMode
        /// </summary>
        private bool editMode;

        /// <summary>
        /// Created Integer for tclId.
        /// </summary>
        private int tclId = -1;

        /// <summary>
        /// flag to identify completion of save operation
        /// </summary>
        private bool flagSaveConfirmed;

        /// <summary>
        /// SmartPart instance for accessing the properties
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// DisbursementCheckInsertData
        /// </summary>
        private DisbursementCheckInsertData disbursementUpdateValueDataset = new DisbursementCheckInsertData();

        /// <summary>
        /// AgencyDataTable
        /// </summary>
        private DataTable agencyDataTable = new DataTable();

        /// <summary>
        /// AllTCLIdXml
        /// </summary>
        private string allTclId = string.Empty;

        /// <summary>
        /// checkDetailDataset
        /// </summary>
        private DataSet checkDetailDataset;

        /// <summary>
        /// pageLoadStatus Local variable.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// currentRow
        /// </summary>
        private int currentRow;

        /// <summary>
        /// this variable used to set postion userdatagrid view 
        /// </summary>
        private int tempRowId = -1;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// registerId
        /// </summary>
        private int registerId;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1211"/> class.
        /// </summary>
        public F1211()
        {
            this.InitializeComponent();
            this.AgencyPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AgencyPictureBox.Height, this.AgencyPictureBox.Width, "Agency", 28, 81, 128);
            this.CheckDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CheckDetailPictureBox.Height, this.CheckDetailPictureBox.Width, "Check Detail", 174, 150, 94);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// event publication for GetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the Disbursement Check Staging controll.
        /// </summary>
        /// <value>The Disbursement Check Staging controll.</value>
        [CreateNew]
        public F1211Controller Form1211Controll
        {
            get { return this.form1211Control as F1211Controller; }
            set { this.form1211Control = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
            }
        }

        #endregion 

        #region EventsSubscription

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
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.CreateChecks_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click();
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
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == "F" + this.Tag.ToString())
            {
                this.form1211Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Selects the un select all.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SelectUnSelectAll(string status)
        {
            if (this.rowCountAgency > 0)
            {
                for (int i = 0; i < this.rowCountAgency; i++)
                {
                    this.DisbursementAgencyGrid.Rows[i].Cells[this.disbursementCheckDataset.ListAgencyTable.IsValidColumn.ColumnName].Value = status;
                }
            }

            this.DisbursementAgencyGrid.RefreshEdit();
        }

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.PageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), SharedFunctions.GetResourceString("FormClose1211"), "", "?"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveMasterConfirm();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the checks_ click.
        /// </summary>
        private void CreateChecks_Click()
        {
            if (this.rowCountAgency > 0)
            {
                int returnValue;
                string selectedTclid = string.Empty;
                selectedTclid = this.SelectedTclId();

                if (!string.IsNullOrEmpty(selectedTclid.Trim()))
                {
                    returnValue = this.form1211Control.WorkItem.F1211_CreateChecks(TerraScanCommon.UserId, selectedTclid);

                    if (returnValue > 0)
                    {
                        ////ErrorList Form
                        Form errorListingForm = this.form1211Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1206, null, this.form1211Control.WorkItem);
                        if (errorListingForm != null)
                        {
                            errorListingForm.ShowDialog();
                        }
                    }
                    else
                    {
                        this.PopulateAgencyGrid(0);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the master confirm.
        /// </summary>
        /// <returns>Returns Boolean Value</returns>
        private bool SaveMasterConfirm()
        {
            this.flagSaveConfirmed = false;
            this.SaveButton_Click();
            return this.flagSaveConfirmed;
        }

        /// <summary>
        /// Selecteds the TCL id.
        /// </summary>
        /// <returns>tclid xml</returns>
        private string SelectedTclId()
        {
            DataSet deleteDataSet = new DataSet();
            DataRow[] deleteRow;
            deleteRow = this.disbursementCheckDataset.ListAgencyTable.Select(this.disbursementCheckDataset.ListAgencyTable.IsValidColumn.ColumnName + "= True");
            deleteDataSet.Merge(deleteRow);

            for (int i = 1; i < deleteDataSet.Tables[0].Columns.Count; i++)
            {
                deleteDataSet.Tables[0].Columns.Remove(deleteDataSet.Tables[0].Columns[i].ColumnName);
            }

            return TerraScanCommon.GetXmlString(deleteDataSet.Tables[0]);
        }

        /// <summary>
        /// Texts the length of the box max.
        /// </summary>
        private void TextBoxMaxLength()
        {
            this.PayableTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.PayableToColumn.MaxLength;
            this.CheckDateTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.CheckDateColumn.MaxLength;
            this.NameTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.NameColumn.MaxLength;
            this.Address1TextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.Address1Column.MaxLength;
            this.Address2TextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.Address2Column.MaxLength;
            this.CityTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.CityColumn.MaxLength;
            this.StateTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.StateColumn.MaxLength;
            this.ZipTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.ZipColumn.MaxLength;
            this.CreatedByTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.Name_DisplayColumn.MaxLength;
            this.TotalAmountTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.TotalAmountColumn.MaxLength;
            this.MemoTextBox.MaxLength = this.disbursementCheckDataset.ListAgencyTable.MemoColumn.MaxLength;
        }

        /// <summary>
        /// Sets the data binding value.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetDataBindingValue(int rowId)
        {
            if (this.rowCountAgency > 0)
            {
                if (rowId >= 0)
                {
                    this.PayableTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.PayableToColumn.ColumnName].Value.ToString();
                    this.CheckDateTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.CheckDateColumn.ColumnName].Value.ToString();
                    this.NameTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells["DisbursementName"].Value.ToString();
                    this.Address1TextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.Address1Column.ColumnName].Value.ToString();
                    this.Address2TextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.Address2Column.ColumnName].Value.ToString();
                    this.CityTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.CityColumn.ColumnName].Value.ToString();
                    this.StateTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.StateColumn.ColumnName].Value.ToString();
                    this.ZipTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.ZipColumn.ColumnName].Value.ToString();
                    this.CreatedByTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.Name_DisplayColumn.ColumnName].Value.ToString();
                    this.MemoTextBox.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.MemoColumn.ColumnName].Value.ToString();
                    this.FromAccountCombo.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.AccountNameColumn.ColumnName].Value.ToString();
                    this.FromAccountLinkLable.Text = this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.AccountNameColumn.ColumnName].Value.ToString();
                    this.registerId = Convert.ToInt32(this.DisbursementAgencyGrid.Rows[rowId].Cells[this.disbursementCheckDataset.ListAgencyTable.RegisterIDColumn.ColumnName].Value.ToString());
                }
            }
        }

        /// <summary>
        /// Sets the data grid view position to firstrow.
        /// </summary>
        /// <param name="firstRow">The first row.</param>
        private void SetDataGridViewPosition(int firstRow)
        {
            if (this.rowCountAgency > 0)
            {
                this.DisbursementAgencyGrid.Rows[firstRow].Selected = true;
                this.DisbursementAgencyGrid.CurrentCell = this.DisbursementAgencyGrid[0, firstRow];
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private int GetRowIndex()
        {
            if (this.disbursementCheckDataset.ListAgencyTable.Rows.Count > 0)
            {
                if (this.DisbursementAgencyGrid.SelectedRows.Count > 0)
                {
                    this.selected = this.DisbursementAgencyGrid.SelectedRows[0].Index;
                }
                else if (this.DisbursementAgencyGrid.SelectedCells.Count > 0)
                {
                    this.selected = this.DisbursementAgencyGrid.CurrentCell.RowIndex;
                }
            }

            return this.selected;
        }

        /// <summary>
        /// Checks the Requireds the field during Save Operation.
        /// </summary>
        /// <returns> Flag to specify wether required.</returns>
        private bool RequiredField()
        {
            // Checks all the Required Controls has value assigned.
            if (this.CheckDateTextBox.Text.Trim().Length > 0 && this.NameTextBox.Text.Trim().Length > 0 && this.Address1TextBox.Text.Trim().Length > 0 && this.CityTextBox.Text.Trim().Length > 0 && this.ZipTextBox.Text.Trim().Length > 0 && this.StateTextBox.Text.Trim().Length > 0 && this.FromAccountCombo.SelectedIndex >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checkeds the column.
        /// </summary>
        private void CheckedColumn()
        {
            this.DisbursementAgencyGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.disbursementCheckDataset.ListAgencyTable.AcceptChanges();
            DataRow[] checkedRow;
            checkedRow = this.disbursementCheckDataset.ListAgencyTable.Select(SharedFunctions.GetResourceString("ExpressionCheckStaging"));

            if (checkedRow.Length > 0)
            {
                this.operationSmartPart.DeleteButtonEnable = true && this.PermissionFiled.deletePermission;
                this.operationSmartPart.NewButtonEnable = true && this.PermissionFiled.newPermission;
            }
            else
            {
                this.DisableAllControls = true;
                this.operationSmartPart.DeleteButtonEnable = false;
                this.operationSmartPart.NewButtonEnable = false;
            }
        }

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form1211Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form1211Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form1211Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form1211Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form1211Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form1211Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
            }

            this.operationSmartPart.NewButtonText = "Create Checks";
            this.operationSmartPart.NewButtonAutoSize = true && this.PermissionFiled.newPermission;

            this.formLabelInfo[0] = SharedFunctions.GetResourceString("1211DisbursementCheckStaging");
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Populates the agency grid.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulateAgencyGrid(int rowIndex)
        {
            int rowId = -1;
            this.disbursementCheckDataset = this.form1211Control.WorkItem.F1211_GetDisbursementCheckList;
            this.agencyDataTable = this.disbursementCheckDataset.ListAgencyTable.Copy();
            this.rowCountAgency = this.disbursementCheckDataset.ListAgencyTable.Rows.Count;
            this.DisbursementAgencyGrid.DataSource = this.disbursementCheckDataset.ListAgencyTable;

            if (this.rowCountAgency > 0)
            {
                this.DisbursementAgencyGrid.Enabled = true;
                this.CheckDetailGrid.Enabled = true;
                this.HeaderPanel.Enabled = true;
                this.CityPanel.Enabled = true;
                this.operationSmartPart.NewButtonEnable = true && this.PermissionFiled.newPermission;

                int.TryParse(this.DisbursementAgencyGrid.Rows[rowIndex].Cells[this.disbursementCheckDataset.ListAgencyTable.TCLIDColumn.ColumnName].Value.ToString(), out rowId);

                this.PopulateFromAccountCombo();
                this.PopulateCheckDetailGrid(rowId);

                TerraScanCommon.SetDataGridViewPosition(this.DisbursementAgencyGrid, rowIndex);
                this.SetDataBindingValue(rowIndex);

                this.PreviewButton.Enabled = true;
            }
            else
            {
                DataTable tempDataTable = new DataTable();
                tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.IsValidColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.SubFundColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.DescriptionColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.TCLIDColumn.ColumnName) });
                this.CheckDetailGrid.DataSource = tempDataTable;

                this.DisbursementAgencyGrid.Rows[0].Cells[0].Selected = false;
                this.DisbursementAgencyGrid.Rows[0].Selected = false;
                this.PreviewButton.Enabled = false;
                this.DisbursementAuditLink.Enabled = false;
                this.DisbursementAuditLink.Text = SharedFunctions.GetResourceString("1211AuditLink");
                this.ClearFields();
                this.FromAccountCombo.DataSource = null;
                this.HeaderPanel.Enabled = false;
                this.CityPanel.Enabled = false;
                this.operationSmartPart.NewButtonEnable = false;
                this.operationSmartPart.DeleteButtonEnable = false;
                this.DisableAllControls = true;
                this.DisbursementAgencyGrid.Enabled = false;
                this.CheckDetailGrid.Enabled = false;
            }

            this.CheckedColumn();
            this.DisbursementAgencyGrid.Focus();

            if (this.rowCountAgency > this.DisbursementAgencyGrid.NumRowsVisible)
            {
                this.DisbursementCheckVScrollBar.Visible = false;
                this.CheckDetailScrollBar.Visible = false;
            }
            else
            {
                this.DisbursementCheckVScrollBar.Visible = true;
                ////this.DisbursementCheckVScrollBar.Enabled = false;
                this.CheckDetailScrollBar.Visible = true;
                ////this.CheckDetailScrollBar.Enabled = false;
            }
        }

        /// <summary>
        /// Removes the columns.
        /// </summary>
        private void RemoveColumns()
        {
            int columnsRemove = this.agencyDataTable.Columns.Count;
            string[] columns = new String[columnsRemove - 1];
            int z = 0;

            for (int i = 1; i < this.agencyDataTable.Columns.Count; i++)
            {
                columns[z++] = this.agencyDataTable.Columns[i].ColumnName;
            }

            foreach (string remove in columns)
            {
                this.agencyDataTable.Columns.Remove(remove);
            }

            this.allTclId = TerraScanCommon.GetXmlString(this.agencyDataTable);
        }

        /// <summary>
        /// Populates the check detail grid.
        /// </summary>
        /// <param name="currentTclId">The TCL id.</param>
        private void PopulateCheckDetailGrid(int currentTclId)
        {
            this.CustomizeCheckDetailGrid();

            DataRow[] matchingRows;
            this.checkDetailDataset = new DataSet();
            matchingRows = this.disbursementCheckDataset.ListCheckDetailTable.Select("TCLID = " + currentTclId);

            if (matchingRows.Length > 0)
            {
                this.checkDetailDataset.Merge(matchingRows);
                this.rowCountCheckDetail = this.checkDetailDataset.Tables[0].Rows.Count;
                this.CheckDetailGrid.DataSource = this.checkDetailDataset.Tables[0];

                this.TotalAmountCalc();
            }
            else
            {
                DataTable tempDataTable = new DataTable();
                tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.IsValidColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.SubFundColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.DescriptionColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName), new DataColumn(this.disbursementCheckDataset.ListCheckDetailTable.TCLIDColumn.ColumnName) });

                this.CheckDetailGrid.DataSource = tempDataTable;
                this.TotalAmountTextBox.Text = string.Empty;
            }

            /*  if (this.rowCountCheckDetail > this.CheckDetailGrid.NumRowsVisible)
            {
                this.CheckDetailScrollBar.Visible = false;
            }
            else
            {
                this.CheckDetailScrollBar.Visible = true;
                this.CheckDetailScrollBar.Enabled = false;
            } */
        }

        /// <summary>
        /// Totals the amount calc.
        /// </summary>
        private void TotalAmountCalc()
        {
            decimal totalAmountCalc = 0;

            if (this.rowCountCheckDetail > 0)
            {
                for (int i = 0; i < this.rowCountCheckDetail; i++)
                {
                    totalAmountCalc = Convert.ToDecimal(this.CheckDetailGrid.Rows[i].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName].Value) + totalAmountCalc;
                }
            }

            this.TotalAmountTextBox.Text = Convert.ToDecimal(totalAmountCalc).ToString("$ #,##0.00");
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeAgencyGrid()
        {            
            this.DisbursementAgencyGrid.AllowUserToResizeColumns = false;
            this.DisbursementAgencyGrid.AutoGenerateColumns = false;
            this.DisbursementAgencyGrid.AllowUserToResizeRows = false;
            this.DisbursementAgencyGrid.StandardTab = true;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.IsValidColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.IsValidColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.AgencyNameColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.AgencyNameColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.AccountNameColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.AccountNameColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.TotalAmountColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.TotalAmountColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.TCLIDColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.TCLIDColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.PayableToColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.PayableToColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.CheckDateColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.CheckDateColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns["DisbursementName"].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.NameColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.Address1Column.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.Address1Column.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.Address2Column.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.Address2Column.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.CityColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.CityColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.StateColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.StateColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.ZipColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.ZipColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.Name_DisplayColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.Name_DisplayColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.MemoColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.MemoColumn.ColumnName;
            this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.RegisterIDColumn.ColumnName].DataPropertyName = this.disbursementCheckDataset.ListAgencyTable.RegisterIDColumn.ColumnName;
            this.DisbursementAgencyGrid.PrimaryKeyColumnName = this.disbursementCheckDataset.ListAgencyTable.TCLIDColumn.ColumnName;
        }

        /// <summary>
        /// Customizes the check detail grid.
        /// </summary>
        private void CustomizeCheckDetailGrid()
        {
            this.CheckDetailGrid.AllowUserToResizeColumns = false;
            this.CheckDetailGrid.AutoGenerateColumns = false;
            this.CheckDetailGrid.AllowUserToResizeRows = false;
            this.CheckDetailGrid.StandardTab = true;

            this.CheckDetailGrid.Columns[0].DataPropertyName = this.disbursementCheckDataset.ListCheckDetailTable.IsValidColumn.ColumnName;
            this.CheckDetailGrid.Columns[1].DataPropertyName = this.disbursementCheckDataset.ListCheckDetailTable.SubFundColumn.ColumnName;
            this.CheckDetailGrid.Columns[2].DataPropertyName = this.disbursementCheckDataset.ListCheckDetailTable.DescriptionColumn.ColumnName;
            this.CheckDetailGrid.Columns[3].DataPropertyName = this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName;
            this.CheckDetailGrid.Columns[4].DataPropertyName = this.disbursementCheckDataset.ListCheckDetailTable.TCLIDColumn.ColumnName;
            this.CheckDetailGrid.Columns[5].DataPropertyName = this.disbursementCheckDataset.ListCheckDetailTable.PreviousAmountColumn.ColumnName;
            this.CheckDetailGrid.Columns[6].DataPropertyName = this.disbursementCheckDataset.ListCheckDetailTable.SubFundIDColumn.ColumnName;
            this.CheckDetailGrid.PrimaryKeyColumnName = this.disbursementCheckDataset.ListCheckDetailTable.SubFundIDColumn.ColumnName;
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.PayableTextBox.Text = string.Empty;
            this.CheckDateTextBox.Text = string.Empty;
            this.NameTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.CreatedByTextBox.Text = string.Empty;
            this.TotalAmountTextBox.Text = string.Empty;
            this.MemoTextBox.Text = string.Empty;
            this.FromAccountLinkLable.Text = string.Empty;
        }

        /// <summary>
        /// Grids the click.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void GridClick(int rowIndex)
        {
            if (this.rowCountAgency > 0)
            {
                if (rowIndex >= 0)
                {
                    int.TryParse(this.DisbursementAgencyGrid.Rows[rowIndex].Cells[this.disbursementCheckDataset.ListAgencyTable.TCLIDColumn.ColumnName].Value.ToString(), out this.tclId);
                    this.PopulateCheckDetailGrid(this.tclId);
                    this.SetDataBindingValue(rowIndex);
                    this.DisbursementAuditLink.Text = SharedFunctions.GetResourceString("1211AuditLink") + " " + this.DisbursementAgencyGrid.Rows[rowIndex].Cells[this.disbursementCheckDataset.ListAgencyTable.TCLIDColumn.ToString()].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Populates from account combo.
        /// </summary>
        private void PopulateFromAccountCombo()
        {
            if (this.disbursementCheckDataset.ListFromAccountTable.Rows.Count > 0)
            {
                this.FromAccountCombo.Enabled = true;
                this.FromAccountLinkLable.Visible = true;
                this.FromAccountCombo.DataSource = this.disbursementCheckDataset.ListFromAccountTable;
                this.FromAccountCombo.DisplayMember = this.disbursementCheckDataset.ListFromAccountTable.AccountNameColumn.ColumnName;
                this.FromAccountCombo.ValueMember = this.disbursementCheckDataset.ListFromAccountTable.RegisterIDColumn.ColumnName;
                this.FromAccountCombo.SelectedIndex = 0;
                this.FromAccountLinkLable.Text = this.disbursementCheckDataset.ListFromAccountTable.Rows[0][this.disbursementCheckDataset.ListFromAccountTable.AccountNameColumn.ColumnName].ToString();  
            }
            else
            {
                this.FromAccountLinkLable.Visible = false;
                this.FromAccountCombo.Text = string.Empty;
                this.FromAccountLinkLable.Text = string.Empty;
                this.FromAccountCombo.Enabled = false;
            }
        }

        /// <summary>
        /// Shows the attachment calender.
        /// </summary>
        private void ShowCheckDateCalender()
        {
            this.CheckDateMonthCalander.Visible = true;
            this.CheckDateMonthCalander.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.CheckDateMonthCalander.Left = this.HeaderPanel.Left + this.CheckDatePanel.Left + this.CheckDatePictureBox.Left + this.CheckDatePictureBox.Width;
            this.CheckDateMonthCalander.Top = this.HeaderPanel.Top + this.CheckDatePanel.Top + this.CheckDatePictureBox.Top;
            this.CheckDateMonthCalander.Tag = this.CheckDatePictureBox.Tag;
            this.CheckDateMonthCalander.Focus();

            if (!string.IsNullOrEmpty(this.CheckDateTextBox.Text))
            {
                this.CheckDateMonthCalander.SetDate(Convert.ToDateTime(this.CheckDateTextBox.Text));
            }
        }

        /// <summary>
        /// Raises the text box key press event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void OnTextBoxKeyPress(KeyPressEventArgs e)
        {
            try
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
                            this.editMode = true;
                            this.pageMode = TerraScanCommon.PageModeTypes.New;
                            ////this.DisbursementAuditLink.Enabled = false;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
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
        /// Raises the text box key up event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void OnTextBoxKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    {
                        break;
                    }

                case Keys.Delete:
                    {
                        this.editMode = true;
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.DisbursementAuditLink.Enabled = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        break;
                    }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        private void SaveButton_Click()
        {
            try
            {
                if (this.RequiredField())
                {
                    string disbursementCheckValue = string.Empty;
                    string checkItems = string.Empty;
                    int registerId = 0;
                    int.TryParse(this.FromAccountCombo.SelectedValue.ToString(), out registerId);
                    DisbursementCheckInsertData checkInsertDataset = new DisbursementCheckInsertData();
                    DataRow disbursementCheckinsertRow;
                    disbursementCheckinsertRow = checkInsertDataset.InsertCheckStaging.NewRow();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.RegisterIDColumn.ColumnName] = registerId;
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.CheckDateColumn.ColumnName] = this.CheckDateTextBox.Text.Trim();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.NameColumn.ColumnName] = this.NameTextBox.Text.Trim();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.Address1Column.ColumnName] = this.Address1TextBox.Text.Trim();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.Address2Column.ColumnName] = this.Address2TextBox.Text.Trim();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.CityColumn.ColumnName] = this.CityTextBox.Text.Trim();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.StateColumn.ColumnName] = this.StateTextBox.Text.Trim();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.ZipColumn.ColumnName] = this.ZipTextBox.Text.Trim();
                    disbursementCheckinsertRow[checkInsertDataset.InsertCheckStaging.MemoColumn.ColumnName] = this.MemoTextBox.Text.Trim();
                    checkInsertDataset.InsertCheckStaging.Rows.Add(disbursementCheckinsertRow);
                    disbursementCheckValue = TerraScanCommon.GetXmlString(checkInsertDataset.Tables[0]);

                    this.CheckDetailGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.checkDetailDataset.Tables[0].AcceptChanges();
                    checkItems = TerraScanCommon.GetXmlString(this.checkDetailDataset.Tables[0]);

                    this.form1211Control.WorkItem.F1211_UpdateCheckStaging(this.tclId, disbursementCheckValue, checkItems, TerraScanCommon.UserId);

                    this.currentRow = this.GetRowIndex();
                    this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                    this.PopulateAgencyGrid(this.currentRow);
                    this.DisbursementAuditLink.Enabled = true;
                    this.DisbursementAgencyGrid.Focus();
                    this.CheckDetailGrid.AllowSorting = true;
                    this.DisbursementAgencyGrid.AllowSorting = true;
                    this.editMode = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagSaveConfirmed = true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CheckDateTextBox.Focus();
                    this.flagSaveConfirmed = false;
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                int rowSelected = 0;
                rowSelected = this.GetRowIndex();
                this.PopulateAgencyGrid(rowSelected);
                this.DisbursementAuditLink.Enabled = true;
                this.CheckDetailGrid.AllowSorting = true;
                this.DisbursementAgencyGrid.AllowSorting = true;
                this.editMode = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        private void DeleteButton_Click()
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deletingRows = string.Empty;
                    deletingRows = this.SelectedTclId();

                    if (!string.IsNullOrEmpty(deletingRows.Trim()))
                    {
                        this.form1211Control.WorkItem.F1211_DeleteCheckStaging(deletingRows, TerraScanCommon.UserId);

                        this.currentRow = this.GetRowIndex();

                        this.disbursementCheckDataset = this.form1211Control.WorkItem.F1211_GetDisbursementCheckList;
                        if (this.currentRow >= this.disbursementCheckDataset.ListAgencyTable.Rows.Count)
                        {
                            this.currentRow = this.disbursementCheckDataset.ListAgencyTable.Rows.Count - 1;
                        }

                        this.PopulateAgencyGrid(this.currentRow);
                    }
                }
                else
                {
                    this.DisbursementAgencyGrid.Focus();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F1211 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1211_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.pageLoadStatus = true;
                this.CustomizeAgencyGrid();
                this.PopulateAgencyGrid(0);
                this.CheckDateMonthCalander.Visible = false;
                
                if (this.rowCountAgency > 0)
                {
                    this.RemoveColumns();
                }

                this.DisbursementAgencyGrid.Focus();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the DisbursementAgencyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisbursementAgencyGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {               
                if (e.RowIndex >= 0 && e.ColumnIndex >= -1 && e.RowIndex < this.rowCountAgency)
                {
                    // Checks the value chaged in Datagrid.
                    if (this.editMode)
                    {
                        if (this.tempRowId != e.RowIndex)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        this.SaveButton_Click();
                                        this.tempRowId = e.RowIndex;
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.tempRowId = e.RowIndex;
                                        this.PopulateAgencyGrid(e.RowIndex);
                                        this.editMode = false;
                                        this.pageMode = TerraScanCommon.PageModeTypes.View; 
                                        this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                                        this.DisbursementAuditLink.Enabled = true;
                                        this.DisbursementAgencyGrid.Focus();
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        this.SetDataGridViewPosition(this.tempRowId);
                                        this.SetDataBindingValue(this.tempRowId);
                                        break;
                                    }
                            }                           
                        }
                    }
                    else
                    {                        
                        this.GridClick(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the DisbursementAgencyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisbursementAgencyGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int clearedBit = -1;
                string tclIdDataset = string.Empty;

                if (e.RowIndex >= 0 && e.ColumnIndex >= -1 && e.RowIndex < this.rowCountAgency)
                {
                    if (e.ColumnIndex == 0)
                    {
                        this.DisbursementAgencyGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        clearedBit = Convert.ToInt32(this.disbursementCheckDataset.ListAgencyTable.Rows[e.RowIndex][this.disbursementCheckDataset.ListAgencyTable.IsValidColumn.ToString()]);

                        this.disbursementUpdateValueDataset.UpdateisValidStatusTable.Clear();
                        DataRow disbursementvalidStatusRow;
                        disbursementvalidStatusRow = this.disbursementUpdateValueDataset.UpdateisValidStatusTable.NewRow();
                        disbursementvalidStatusRow[this.disbursementUpdateValueDataset.UpdateisValidStatusTable.TCLIDColumn.ColumnName] = this.tclId;
                        this.disbursementUpdateValueDataset.UpdateisValidStatusTable.Rows.Add(disbursementvalidStatusRow);
                        tclIdDataset = TerraScanCommon.GetXmlString(this.disbursementUpdateValueDataset.UpdateisValidStatusTable);
                        this.form1211Control.WorkItem.F1211_UpdateAgencyValidStatus(tclIdDataset, clearedBit, TerraScanCommon.UserId);
                        this.CheckedColumn();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DisbursementAgencyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DisbursementAgencyGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.editMode)
                {
                    this.GridClick(e.RowIndex);
                    this.tempRowId = e.RowIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CheckDatePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CheckDatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowCheckDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the CheckDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void CheckDateMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                // Assign the selected date to the DateTextbox.
                this.CheckDateTextBox.Text = e.Start.ToShortDateString();
                this.editMode = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.DisbursementAuditLink.Enabled = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.CheckDateTextBox.Focus();
                this.CheckDateMonthCalander.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the CheckDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CheckDateMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.CheckDateTextBox.Text = this.CheckDateMonthCalander.SelectionStart.ToShortDateString();
                    this.editMode = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.DisbursementAuditLink.Enabled = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                    this.CheckDateMonthCalander.Visible = false;
                    this.CheckDateTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the CheckDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CheckDateMonthCalander_Leave(object sender, EventArgs e)
        {
            this.CheckDateMonthCalander.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rowCountAgency > 0)
                {
                    this.SelectUnSelectAll("True");
                    this.form1211Control.WorkItem.F1211_UpdateAgencyValidStatus(this.allTclId, 1, TerraScanCommon.UserId);
                    this.operationSmartPart.DeleteButtonEnable = true && this.PermissionFiled.deletePermission;
                    this.operationSmartPart.NewButtonEnable = true && this.PermissionFiled.newPermission;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the UnSelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnSelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rowCountAgency > 0)
                {
                    this.SelectUnSelectAll("False");
                    this.form1211Control.WorkItem.F1211_UpdateAgencyValidStatus(this.allTclId, 0, TerraScanCommon.UserId);
                    this.operationSmartPart.DeleteButtonEnable = false;
                    this.operationSmartPart.NewButtonEnable = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the DisbursementAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void DisbursementAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.tclId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.tclId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////Hashtable reportFileIdHashTable = new Hashtable();
                ////string reportAuditId = string.Empty;
                ////this.Cursor = Cursors.WaitCursor;
                ////if (this.tclId != -1)
                ////{
                ////    reportAuditId = this.tclId.ToString();

                ////    reportFileIdHashTable.Clear();                    
                ////    reportFileIdHashTable.Add("KeyValue", reportAuditId);

                ////    // Shows the report form.
                ////    // changed the parameter type from string to int
                ////    TerraScanCommon.ShowReport(121190, TerraScan.Common.Reports.Report.ReportType.Preview, reportFileIdHashTable);
                ////}
                ////else
                ////{
                ////    MessageBox.Show(SharedFunctions.GetResourceString("+"), "Terrascan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}
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
        /// Handles the CellClick event of the CheckDetailGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CheckDetailGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex < this.rowCountCheckDetail)
                {
                    if (this.CheckDetailGrid.Rows[e.RowIndex].Cells["CheckDetailIsValid"].Value.ToString() == "True")
                    {
                        this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.PreviousAmountColumn.ColumnName].Value = this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName].Value;
                        this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName].Value = 0;
                        this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName].ReadOnly = true;
                        this.CheckDetailGrid.Rows[e.RowIndex].Cells["CheckDetailIsValid"].Value = "False";
                    }
                    else if (this.CheckDetailGrid.Rows[e.RowIndex].Cells["CheckDetailIsValid"].Value.ToString() == "False")
                    {
                        this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName].Value = this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.PreviousAmountColumn.ColumnName].Value;
                        this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName].ReadOnly = false;
                        this.CheckDetailGrid.Rows[e.RowIndex].Cells["CheckDetailIsValid"].Value = "True";
                    }

                    this.editMode = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.DisbursementAuditLink.Enabled = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                }

                this.TotalAmountCalc();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEnter event of the CheckDetailGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CheckDetailGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    this.TotalAmountCalc();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Shows the report form.
                // changed the parameter type from string to int
                TerraScanCommon.ShowReport(121101, TerraScan.Common.Reports.Report.ReportType.Preview);
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
        /// Handles the KeyPress event of the NameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the Address1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void Address1TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the Address2TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void Address2TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the CityTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the StateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void StateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the ZipTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ZipTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the FromAccountCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromAccountCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.editMode = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.FromAccountLinkLable.Text = this.FromAccountCombo.Text;
                this.DisbursementAuditLink.Enabled = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the CheckDetailGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void CheckDetailGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                // Only paint if desired column

                if (e.ColumnIndex == this.CheckDetailGrid.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "0");
                        }

                        if (Decimal.TryParse(tempvalue, out outDecimal))
                        {
                            ////change property for combobox change

                            tempvalue = outDecimal.ToString();
                            tempvalue = tempvalue.Replace("-", "");

                            if (!tempvalue.Contains("."))
                            {
                                tempvalue = tempvalue.PadLeft(2, '0').Insert(tempvalue.PadLeft(2, '0').Length - 2, ".");
                            }

                            if (outDecimal.ToString().Contains("-"))
                            {
                                outDecimal = decimal.Parse(tempvalue);
                                outDecimal = decimal.Negate(outDecimal);
                            }
                            else
                            {
                                outDecimal = decimal.Parse(tempvalue);
                            }

                            e.Value = outDecimal;
                            e.ParsingApplied = true;
                        }
                        else
                        {
                            e.Value = decimal.Parse("0");
                            e.ParsingApplied = true;
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
        /// Handles the CellValueChanged event of the CheckDetailGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CheckDetailGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (this.pageLoadStatus)
                    {
                        this.editMode = true;
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.DisbursementAuditLink.Enabled = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        this.CheckDetailGrid.AllowSorting = false;
                        this.DisbursementAgencyGrid.AllowSorting = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the CheckDetailGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CheckDetailGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.PreviousAmountColumn.ColumnName].Value = this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName].Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the DisbursementAgencyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DisbursementAgencyGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.DisbursementAgencyGrid.Columns[this.disbursementCheckDataset.ListAgencyTable.TotalAmountColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.DisbursementAgencyGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListAgencyTable.TotalAmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the CheckDetailGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void CheckDetailGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.CheckDetailGrid.Columns[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                if (e.ColumnIndex == this.CheckDetailGrid.Columns[this.disbursementCheckDataset.ListCheckDetailTable.PreviousAmountColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.CheckDetailGrid.Rows[e.RowIndex].Cells[this.disbursementCheckDataset.ListCheckDetailTable.PreviousAmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the CheckDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CheckDateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the CheckDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CheckDateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the NameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the Address1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void Address1TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the Address2TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void Address2TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the CityTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CityTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the StateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the ZipTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ZipTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the MemoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void MemoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnTextBoxKeyPress(e);
        }

        /// <summary>
        /// Handles the KeyUp event of the MemoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MemoTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnTextBoxKeyUp(e);
        }

        /// <summary>
        /// Handles the LinkClicked event of the FromAccountLinkLable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void FromAccountLinkLable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int cashRegisterId = 0;
                int.TryParse(this.FromAccountCombo.SelectedValue.ToString(), out cashRegisterId);

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(1530);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = cashRegisterId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellLeave event of the CheckDetailGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CheckDetailGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    this.CheckDetailGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.TotalAmountCalc();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DropDownClosed event of the FromAccountCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromAccountCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                this.editMode = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.FromAccountLinkLable.Text = this.FromAccountCombo.Text;
                this.DisbursementAuditLink.Enabled = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}
