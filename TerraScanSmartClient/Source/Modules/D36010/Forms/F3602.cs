//---------------------------------------------------------------------------------
// <copyright file="F2005.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for Copy or Move Misc Improvements Form 
// </summary>
//---------------------------------------------------------------------------------
// Change History
//*********************************************************************************
// Date			    Author		       Description
// ----------		---------		   --------------------------------------------
// 24/04/17        Dhineshkumar        Created
//*********************************************************************************

namespace D36010
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
    using System.Xml;
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
    using Infragistics.Shared;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using System.Diagnostics;
    using Infragistics.Win.UltraWinEditors;
    using TerraScan.Infrastructure.Interface.Constants;

    public partial class F3602 : Form
    {
        /// <summary>
        /// formName
        /// </summary>
        private string formName;

        /// <summary>
        /// form id.
        /// </summary>
        private int formid=31011;

        /// <summary>
        /// object Id
        /// </summary>
        private int objectId = 0;

        /// <summary>
        /// parcelNumber
        /// </summary>
        private string parcelNumber;

        /// <summary>
        /// lockNumber
        /// </summary>
        private string lockNumber;

        /// <summary>
        /// userName
        /// </summary>
        private string userName;

        /// <summary>
        /// String for Misc Details
        /// </summary>
        private string miscData;

        /// <summary>
        /// formTitle
        /// </summary>
        private string formTitle;

        /// <summary>
        /// Parcel Id
        /// </summary>
        private int parcelId;

        private int callingform = 3602;

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        private int rollyear;

        /// <summary>
        /// Store selected Copy Move selected type.
        /// </summary>
        private string tempCopyMoveComboValue = string.Empty;

        /// <summary>
        /// Store selected Create New Object selected type. 
        /// </summary>
        private string tempCreateNewObjectComboValue = string.Empty;

        /// <summary>
        /// Store selected Create New Object selected type. 
        /// </summary>
        private string tempCreateNewValueSliceValue = string.Empty;

        /// <summary>
        /// userID
        /// </summary>
        private int userID;

        /// <summary>
        /// validUserId
        /// </summary>
        private int validUserId;

       /// <summary>
       /// value slice Id
       /// </summary>
        private int valuesliceId;

        /// <summary>
        /// value slice Information.
        /// </summary>
        private string valueSliceInformation;

        /// <summary>
        /// new ObjectID;
        /// </summary>
        private int newObjectId;

        private int currentRowIndex;

        private int currentColumnIndex;

        /// <summary>
        /// string misc File
        /// </summary>
        public string xmlFile;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// controller
        /// </summary>
        private F3602Controller form3602Controller = new F3602Controller();

        /// <summary>
        /// variable holds the selectedCropsIds.
        /// </summary>
        private List<int> selectedMiscIds = new List<int>();

        private Dictionary<int, string> dictMiscIds = new Dictionary<int, string>();

        /// <summary>
        /// miscDataTableListRowCount
        /// </summary>
        private int miscDataTableListRowCount;

        /// <summary>
        /// valueSliceDataTableRowCount
        /// </summary>
        private int valueSliceDataTableRowCount;

        /// <summary>
        /// cropgridRowCount
        /// </summary>
        private int objectSliceDataRowCount;

        /// <summary>
        /// Value slice new ID
        /// </summary>
        private int valueSliceNewID;

        /// <summary>
        /// Columns XML
        /// </summary>
        private string miscSelectedData = null;

        /// <summary>
        /// Misc Improve Id.
        /// </summary>
        private int miscImproveID;

        /// <summary>
        /// checkCount
        /// </summary>
        private int checkCount=0;

        /// <summary>
        /// Misc Improve Details
        /// </summary>
        private DataTable miscImprovetable = new DataTable("Table");

        /// <summary>
        /// Variable holds the copy or move Misc DataSet.
        /// </summary>
        private F3602CopyMoveMiscImprovement copyorMoveDataSet = new F3602CopyMoveMiscImprovement();

        /// <summary>
        /// Variable holds the copy or move Misc DataSet.
        /// </summary>
        private F3602CopyMoveMiscImprovement.GetMiscImprovementDetailsDataTable getMiscImprovedetails = new F3602CopyMoveMiscImprovement.GetMiscImprovementDetailsDataTable();

        /// <summary>
        /// Variable holds the copy or move Misc DataSet.
        /// </summary>
        private F3602CopyMoveMiscImprovement.GetObjectDetailsTableDataTable getObjectDetails = new F3602CopyMoveMiscImprovement.GetObjectDetailsTableDataTable();

        /// <summary>
        /// Variable holds the copy or move Misc DataSet.
        /// </summary>
        private F3602CopyMoveMiscImprovement.GetValueSliceDetailsDataTable getValueSliceDetails = new F3602CopyMoveMiscImprovement.GetValueSliceDetailsDataTable();

        /// <summary>
        /// Variable holds copy or move miscDetails.
        /// </summary>
        private F3602CopyMoveMiscImprovement.miscDetailsTableDataTable miscDetailsTable = new F3602CopyMoveMiscImprovement.miscDetailsTableDataTable();

        #region Controller
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3602"/> class.
        /// </summary>
        public F3602()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3602"/> class.
        /// </summary>
        /// <param name="miscId"></param>
        /// <param name="formName"></param>
        public F3602(int miscId, string formName)
        {
            InitializeComponent();
            this.keyId = miscId;
            this.formTitle = formName;
        }

        /// <summary>
        /// For F3602Control
        /// </summary>
        [CreateNew]
        public F3602Controller Form25050Control
        {
            get { return this.form3602Controller as F3602Controller; }
            set { this.form3602Controller = value; }
        }

        #endregion

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        /// <summary>
        /// F3602 Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F3602_Load(object sender, EventArgs e)
        {
            try
            {
                this.CopyMoveComboBox.Select();
                this.CopyMoveComboBox.Focus();
                this.valuesliceId = this.keyId;
                this.userID = TerraScanCommon.UserId;
                var sliceInformation = this.form3602Controller.WorkItem.GetSandwichAndItsSliceInformation(this.formid, this.valuesliceId, this.userID);
                valueSliceInformation = ((TerraScan.BusinessEntities.FormMasterData.FormSubTitle1Row)((sliceInformation.FormSubTitle1).Rows[0])).SubTitle1.ToString();
                this.LoadComboBoxValues();
                this.LoadCopyMoveMiscDetails();
                this.GetObjectTypeComboValue();
                this.LoadMiscImprovementsGridDetails();
                this.PopulateMiscImprovementsDataGrid();                          
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

    

        /// <summary>
        /// Load Copy move Misc Details.
        /// </summary>
        private void LoadCopyMoveMiscDetails()
        {
            Color newcolor = Color.FromArgb(28, 81, 128);
            ObjectGridView.ColumnHeadersDefaultCellStyle.BackColor = newcolor;
            ValuesliceGridView.ColumnHeadersDefaultCellStyle.BackColor = newcolor;
            MiscImprovementGridView.ColumnHeadersDefaultCellStyle.BackColor = newcolor;
            ObjectGridView.RowHeadersDefaultCellStyle.BackColor = newcolor;
            ValuesliceGridView.RowHeadersDefaultCellStyle.BackColor = newcolor;
            MiscImprovementGridView.RowHeadersDefaultCellStyle.BackColor = newcolor;

            ObjectGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ObjectGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ObjectGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ValuesliceGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ValuesliceGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ValuesliceGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            MiscImprovementGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            MiscImprovementGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            MiscImprovementGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.MiscImprovementGridView.Columns["MiscStatus"].ReadOnly = true;
            this.CreateNewObjectComboBox.Enabled = false;
            this.ObjectsGridPanel.Visible = false;
            this.ObjectTypePanel.Visible = false;
            this.ObjectTypeValuePanel.Visible = false;
            this.ValueSliceGridPanel.Visible = false;
            this.CreateNewValueslicePanel.Visible = false;
            this.CreateNewValueComboPanel.Visible = false;
            this.MiscImprovementGridPanel.Location = new Point(23, 172);

            this.formTitle = SharedFunctions.GetResourceString("F3602Title");
            this.Text = SharedFunctions.GetResourceString("F1503FormNameTitle") + formTitle;

            string[] parcelNo=null;
            parcelNo = this.valueSliceInformation.Split('/');
            if (parcelNo.Length > 0)
            {
                this.lblParcelvalue.Text = parcelNo[0].Trim();
                if (parcelNo.Length > 1)
                {
                    this.lblParcelRollYear.Text = parcelNo[1].Trim();
                    rollyear = Convert.ToInt32(this.lblParcelRollYear.Text);
                }
            }
        }



        /// <summary>
        /// Parcel Number PictureBox Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelNumberPictureBox_Click(object sender, EventArgs e)
        {
            Form formInfo = new Form();
            object[] optionalParameter = new object[] { this.rollyear,"3602" };
            formInfo = this.form3602Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, optionalParameter, this.form3602Controller.WorkItem);
            if (formInfo != null)
            {
                if (formInfo.ShowDialog() == DialogResult.OK)
                {
                    parcelId = Convert.ToInt32(TerraScanCommon.GetValue(formInfo, "CommandResult").ToString());
                    LinkToTextBox.Text = TerraScanCommon.GetValue(formInfo, "CommandValue").ToString();
                    this.LoadStatus();
                    this.LoadObjectsGridDetails();
                    this.PopulateObjectsDataGrid();
                    this.CreateNewObjectComboBox.SelectedIndex = -1;
                    this.ObjectsGridPanel.Visible = false;
                    this.ObjectTypePanel.Visible = false;
                    this.ObjectTypeValuePanel.Visible = false;
                    this.ObjectTypeComboBox.SelectedIndex = -1;
                    this.CreateNewValueComboPanel.Visible = false;
                    this.CreateNewValueslicePanel.Visible = false;
                    this.NewValueSliceComboBox.SelectedItem = string.Empty;
                    this.ValueSliceGridPanel.Visible = false;
                    this.MiscImprovementGridPanel.Location = new Point(23, 172);
                    this.EnableProcessButton();
                }
            }   
        }

        /// <summary>
        /// Load Combo Box Values.
        /// </summary>
        private void LoadComboBoxValues()
        {
            InitComboBoxValues(CreateNewObjectComboBox);
            InitComboBoxValues(NewValueSliceComboBox);

            CopyMoveComboBox.Items.Insert(0, string.Empty);
            CopyMoveComboBox.Items.Insert(1, "Copy");
            CopyMoveComboBox.Items.Insert(2, "Move");        
        }

        /// <summary>
        /// Inits the combo box values.
        /// </summary>
        /// <param name="initComboBox">The init combo box.</param>
        private static void InitComboBoxValues(TerraScan.UI.Controls.TerraScanComboBox initComboBox)
        {
            initComboBox.Items.Clear();
            initComboBox.Items.Insert(0, string.Empty);
            initComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));
            initComboBox.Items.Insert(2, SharedFunctions.GetResourceString("NOValue"));
        }

        /// <summary>
        /// Copy Move Combo Box Selection Change committed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyMoveComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.CopyMoveComboBox.SelectedIndex != -1 || this.CopyMoveComboBox.SelectedIndex != 0)
            {
                tempCopyMoveComboValue = this.CopyMoveComboBox.SelectedItem.ToString();
            }
            if (this.CopyMoveComboBox.SelectedIndex == -1 || this.CopyMoveComboBox.SelectedIndex == 0)
            {
                this.CreateNewObjectComboBox.SelectedIndex = -1;
                this.ObjectsGridPanel.Visible = false;
                this.ObjectTypePanel.Visible = false;
                this.ObjectTypeValuePanel.Visible = false;
                this.CreateNewValueComboPanel.Visible = false;
                this.CreateNewValueslicePanel.Visible = false;
                this.NewValueSliceComboBox.SelectedItem = string.Empty;
                this.ValueSliceGridPanel.Visible = false;
                this.MiscImprovementGridPanel.Location = new Point(23, 172);
            }
            this.LoadStatus();
            this.EnableProcessButton();
        }

        /// <summary>
        /// Create new object combo Selection change committed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewObjectComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CreateNewObjectComboBox.SelectedIndex == 1)
            {
                this.ObjectTypePanel.Visible = true;
                this.ObjectTypeValuePanel.Visible = true;
                this.CreateNewValueComboPanel.Visible = false;
                this.CreateNewValueslicePanel.Visible = false;
                this.ValueSliceGridPanel.Visible = false;
                this.ObjectsGridPanel.Visible = false;
                this.MiscImprovementGridPanel.Location = new Point(23, 172);
            }
            else if (CreateNewObjectComboBox.SelectedIndex == 2)
            {
                this.ObjectTypePanel.Visible = false;
                this.ObjectTypeValuePanel.Visible = false;
                this.ObjectsGridPanel.Visible = true;
                this.CreateNewValueComboPanel.Visible = true;
                this.CreateNewValueslicePanel.Visible = true;
                this.MiscImprovementGridPanel.Location = new Point(22, 340);
            }
            else if (CreateNewObjectComboBox.SelectedIndex == 0 || CreateNewObjectComboBox.SelectedIndex == -1)
            {
                this.ObjectTypePanel.Visible = false;
                this.ObjectTypeValuePanel.Visible = false;
                this.ObjectsGridPanel.Visible = false;
                this.CreateNewValueComboPanel.Visible = false;
                this.CreateNewValueslicePanel.Visible = false;
                this.ValueSliceGridPanel.Visible = false;
                this.MiscImprovementGridPanel.Location = new Point(23, 172);
            }

            foreach (DataGridViewRow dr in ObjectGridView.Rows)
            {
                DataGridViewCheckBoxCell cell = dr.Cells["ValidStatus"] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (cell.Value.Equals(true))
                    {
                        dr.Cells["ValidStatus"].Value = false;//It's checked!
                    }
                }
            }

            foreach (DataGridViewRow dr in ValuesliceGridView.Rows)
            {
                DataGridViewCheckBoxCell cell = dr.Cells["ValueStatus"] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (cell.Value.Equals(true))
                    {
                        dr.Cells["ValueStatus"].Value = false;//It's checked!
                    }
                }
            }

            this.ObjectTypeComboBox.SelectedIndex = -1;
            this.NewValueSliceComboBox.SelectedIndex = -1;
            this.valueSliceNewID = 0;
            this.newObjectId = 0;
            this.objectId = 0;
            this.checkCount = 0;
            this.EnableProcessButton();
        }

        /// <summary>
        /// Load copy or move misc Details.
        /// </summary>
        private void LoadStatus()
        {
            if (this.CopyMoveComboBox.SelectedIndex != 0 && this.CopyMoveComboBox.SelectedIndex != -1 && LinkToTextBox.Text != string.Empty)
            {
                this.CreateNewObjectComboBox.Enabled = true;
            }
            else
            {
                this.CreateNewObjectComboBox.Enabled = false;
            }
        }

        /// <summary>
        /// Link to textbox Text Changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkToTextBox_TextChanged(object sender, EventArgs e)
        {
            CreateNewObjectComboBox.SelectedItem = string.Empty;
            this.ObjectsGridPanel.Visible = false;
            this.ObjectTypePanel.Visible = false;
            this.ObjectTypeValuePanel.Visible = false;
        }


        /// <summary>
        /// New Value slice combobox Selection Change Committed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewValueSliceComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (NewValueSliceComboBox.SelectedIndex == 2)
            {
                if (checkCount > 0)
                {
                    this.ValueSliceGridPanel.Visible = true;
                    this.MiscImprovementGridPanel.Location = new Point(23, 455);
                }
            }
            else
            {
                this.ValueSliceGridPanel.Visible = false;
                this.MiscImprovementGridPanel.Location = new Point(22, 340);
            }

            foreach (DataGridViewRow dr in ValuesliceGridView.Rows)
            {
                DataGridViewCheckBoxCell cell = dr.Cells["ValueStatus"] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (cell.Value.Equals(true))
                    {
                        dr.Cells["ValueStatus"].Value = false;//It's checked!
                    }
                }
            }
            this.valueSliceNewID = 0;
            this.EnableProcessButton();
        }

        /// <summary>
        /// Get Object type combo Value.
        /// </summary>
        public void GetObjectTypeComboValue()
        {
            F3602CopyMoveMiscImprovement.ObjectTypeTableDataTable objectTypeDataTable = new F3602CopyMoveMiscImprovement.ObjectTypeTableDataTable();
            DataRow customRow = objectTypeDataTable.NewRow();
            objectTypeDataTable.Clear();

            this.copyorMoveDataSet = this.form3602Controller.WorkItem.GetObjectTypesList();
            this.ObjectTypeComboBox.ValueMember = this.copyorMoveDataSet.ObjectTypeTable.ObjectTypeIDColumn.ColumnName;
            this.ObjectTypeComboBox.DisplayMember = this.copyorMoveDataSet.ObjectTypeTable.ObjectTypeColumn.ColumnName;

            ////Empty row is added
            customRow[this.copyorMoveDataSet.ObjectTypeTable.ObjectTypeIDColumn.ColumnName] = "0";
            customRow[this.copyorMoveDataSet.ObjectTypeTable.ObjectTypeColumn.ColumnName] = string.Empty;
            objectTypeDataTable.Rows.Add(customRow);

            ////The original datatable and temp datatable is merged
            objectTypeDataTable.Merge(this.copyorMoveDataSet.ObjectTypeTable);
            this.ObjectTypeComboBox.DataSource = objectTypeDataTable;
        }        

        /// <summary>
        /// Load Objects Data Grid
        /// </summary>
        public void LoadObjectsGridDetails()
        {
            this.copyorMoveDataSet.GetObjectDetailsTable.Clear();
            //this.copyorMoveDataSet = this.form3602Controller.WorkItem.GetObjectDetails(this.parcelId);
            //this.ObjectGridView.DataSource = this.copyorMoveDataSet.GetObjectDetailsTable;
            this.ObjectGridView.AllowUserToResizeColumns = false;
            this.ObjectGridView.AllowUserToResizeRows = false;
            this.ObjectGridView.AutoGenerateColumns = false;
            this.ObjectTypeID.DataPropertyName = this.copyorMoveDataSet.GetObjectDetailsTable.ObjectIDColumn.ColumnName;
            this.objectType.DataPropertyName = this.copyorMoveDataSet.GetObjectDetailsTable.ObjectTypeColumn.ColumnName;
            this.objectDescription.DataPropertyName = this.copyorMoveDataSet.GetObjectDetailsTable.DescriptionColumn.ColumnName;        
        }

        /// <summary>
        /// Load Misc Improvement Data Grid
        /// </summary>
        public void LoadMiscImprovementsGridDetails()
        {
            this.copyorMoveDataSet.GetMiscImprovementDetails.Clear();
            this.MiscImprovementGridView.AllowUserToResizeColumns = false;
            this.MiscImprovementGridView.AllowUserToResizeRows = false;
            this.MiscImprovementGridView.AutoGenerateColumns = false;
            this.MscID.DataPropertyName = this.copyorMoveDataSet.GetMiscImprovementDetails.MIDColumn.ColumnName;
            this.MiscCode.DataPropertyName = this.copyorMoveDataSet.GetMiscImprovementDetails.MICodeColumn.ColumnName;
            this.MiscDescription.DataPropertyName = this.copyorMoveDataSet.GetMiscImprovementDetails.DescriptionColumn.ColumnName;
        }

        /// <summary>
        /// Object GridView CellClick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool isChecked = false;
                checkCount = 0;

                if (e.RowIndex >= 0 && e.RowIndex < this.ObjectGridView.OriginalRowCount)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (e.ColumnIndex.Equals(this.ObjectGridView.Columns["ValidStatus"].Index))
                        {
                            if (this.ObjectGridView.OriginalRowCount > 0)
                            {
                                int currentcolumnclicked = e.ColumnIndex;
                                int currentrowclicked = e.RowIndex;
                                DataGridViewRow row = ObjectGridView.Rows[e.RowIndex];
                                foreach (DataGridViewRow dr in ObjectGridView.Rows)
                                {
                                    DataGridViewCheckBoxCell cell = dr.Cells["ValidStatus"] as DataGridViewCheckBoxCell;

                                    if (cell.Value != null)
                                    {
                                        if (cell.Value.Equals(true))
                                        {
                                            if (row.Index == dr.Index)
                                            {
                                                dr.Cells["ValidStatus"].Value = true;
                                            }
                                            else
                                            {
                                                dr.Cells["ValidStatus"].Value = false;//It's checked!
                                            }
                                        }
                                    }
                                }
                                this.objectId = Convert.ToInt32(this.ObjectGridView.Rows[e.RowIndex].Cells["ObjectTypeID"].Value.ToString());
                                DataGridViewRow obd = ObjectGridView.Rows[e.RowIndex];
                                obd.Cells["ValidStatus"].Value = true;
                                obd.Cells["ValidStatus"].ReadOnly = true;

                                if ((bool)obd.Cells["ValidStatus"].Value)
                                {
                                    checkCount++;
                                }
                                else
                                {
                                    checkCount = 0;
                                }

                                if (NewValueSliceComboBox.SelectedIndex == 2)
                                {
                                    if (checkCount > 0)
                                    {
                                        this.ValueSliceGridPanel.Visible = true;
                                        this.MiscImprovementGridPanel.Location = new Point(23, 467);
                                    }
                                }
                                else
                                {
                                    this.ValueSliceGridPanel.Visible = false;
                                    this.MiscImprovementGridPanel.Location = new Point(22, 340);
                                }

                                //foreach (DataGridViewRow r in ObjectGridView.Rows)
                                //{
                                //    DataGridViewCheckBoxCell chkVal = (DataGridViewCheckBoxCell)r.Cells["ValidStatus"];
                                //    if (chkVal.Value!=null)
                                //    {
                                //        isChecked = (bool)r.Cells["ValidStatus"].Value;
                                //    }
                                //}

                                //if (isChecked)
                                //{
                                //    checkCount++;
                                //}
                                //else
                                //{
                                //    checkCount = 0;
                                //}
                                this.valueSliceNewID = 0;
                                this.LoadValueSlicesGridDetails();
                                this.PopulateValueSlicesDataGrid();
                                this.EnableProcessButton();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Populate Value Slice Grid Details
        /// </summary>
        public void PopulateValueSlicesDataGrid()
        {
            //this.getValueSliceDetails.Clear();
            this.ValuesliceGridView.NumRowsVisible = 3;
            this.copyorMoveDataSet = this.form3602Controller.WorkItem.GetValueSlicesList(this.parcelId,this.objectId);
            this.getValueSliceDetails = this.copyorMoveDataSet.GetValueSliceDetails;
            this.valueSliceDataTableRowCount = this.getValueSliceDetails.Rows.Count;
            if (this.valueSliceDataTableRowCount > 3)
            {
                this.ValueSliceGridScrollBar.Visible = false;
            }
            else
            {
                this.ValueSliceGridScrollBar.Visible = true;
            }
            this.ValuesliceGridView.DataSource = getValueSliceDetails;
        }

        /// <summary>
        /// Populate Misc Improvement Grid Details
        /// </summary>
        public void PopulateMiscImprovementsDataGrid()
        {
            this.getMiscImprovedetails.Clear();
            this.MiscImprovementGridView.NumRowsVisible = 3;
            this.copyorMoveDataSet = this.form3602Controller.WorkItem.GetMiscImprovementsList(this.valuesliceId);
            this.getMiscImprovedetails = this.copyorMoveDataSet.GetMiscImprovementDetails;
            this.miscDataTableListRowCount = this.getMiscImprovedetails.Rows.Count;

            if (this.miscDataTableListRowCount > 3)
            {
                this.MiscImproveGridViewScrollBar.Visible = false;
            }
            else
            {
                this.MiscImproveGridViewScrollBar.Visible = true;
            }
            this.MiscImprovementGridView.DataSource = getMiscImprovedetails;
        }

        /// <summary>
        /// Populate Objects Data Grid.
        /// </summary>
        public void PopulateObjectsDataGrid()
        {
            this.getObjectDetails.Clear();
            this.ObjectGridView.NumRowsVisible = 3;
            this.copyorMoveDataSet = this.form3602Controller.WorkItem.GetObjectDetails(this.parcelId);
            this.getObjectDetails = this.copyorMoveDataSet.GetObjectDetailsTable;
            this.objectSliceDataRowCount = this.getObjectDetails.Rows.Count;

            if (this.objectSliceDataRowCount > 3)
            {
                this.ObjectGridVerticalScroll.Visible = false;
            }
            else
            {
                this.ObjectGridVerticalScroll.Visible = true;
            }
            this.ObjectGridView.DataSource = getObjectDetails;
        }

        /// <summary>
        /// Load Value Slice Data Grid
        /// </summary>
        public void LoadValueSlicesGridDetails()
        {
            this.copyorMoveDataSet.GetValueSliceDetails.Clear();
            this.ValuesliceGridView.AllowUserToResizeColumns = false;
            this.ValuesliceGridView.AllowUserToResizeRows = false;
            this.ValuesliceGridView.AutoGenerateColumns = false;
            this.SliceTypeID.DataPropertyName = this.copyorMoveDataSet.GetValueSliceDetails.ValueSliceIdColumn.ColumnName;
            this.SliceType.DataPropertyName = this.copyorMoveDataSet.GetValueSliceDetails.SliceTypeColumn.ColumnName;
            this.SliceDescription.DataPropertyName = this.copyorMoveDataSet.GetValueSliceDetails.DescriptionColumn.ColumnName;
        }

        /// <summary>
        /// Select All Checkbox Check Changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {               
                if (this.SelectAllCheckbox.Checked == true)
                {
                    this.miscDetailsTable.Clear();
                    selectedMiscIds = new List<int>();
                    dictMiscIds = new Dictionary<int, string>();
                    if (this.miscDataTableListRowCount > 0)
                    {
                        this.SelectUnSelectAll("True");
                    }
                    this.CalculateSelectAllCrops(SelectAllCheckbox.Checked);
                    this.miscDetailsTable.Clear();
                    //for (int count = 0; count < this.selectedMiscIds.Count; count++)
                    //{
                    //    DataRow row = miscDetailsTable.NewRow();
                    //    row["MID"] = Convert.ToInt32(this.MiscImprovementGridView[0, count].Value);
                    //    row["IsChecked"] = this.MiscImprovementGridView.Rows[count].Cells["MiscStatus"].Value;
                    //    miscDetailsTable.Rows.Add(row);
                    //}

                    foreach (var d in dictMiscIds)
                    {
                        DataRow row = miscDetailsTable.NewRow();
                        row["MID"] = Convert.ToInt32(d.Key);
                        row["IsChecked"] = d.Value;
                        miscDetailsTable.Rows.Add(row);
                    }
                    this.miscSelectedData = GetXmlString(miscDetailsTable);
                    this.xmlFile = "<Root>" + miscSelectedData + "</Root>";

                    if (this.miscSelectedData.Equals("<Root />"))
                    {
                        this.miscSelectedData = null;
                    }
                }
                else if (SelectAllCheckbox.Checked == false)
                {
                    if (this.miscDataTableListRowCount > 0 && this.miscDataTableListRowCount <= this.selectedMiscIds.Count)
                    {
                        this.SelectUnSelectAll("False");
                        this.CalculateUnSelectCrops(SelectAllCheckbox.Checked);
                    }
                    this.miscDetailsTable.Clear();
                    //for (int count = 0; count < this.selectedMiscIds.Count; count++)
                    //{
                    //    DataRow row = miscDetailsTable.NewRow();
                    //    row["MID"] = Convert.ToInt32(this.MiscImprovementGridView[0, count].Value);
                    //    row["IsChecked"] = this.MiscImprovementGridView.Rows[count].Cells["MiscStatus"].Value;
                    //    miscDetailsTable.Rows.Add(row);
                    //}

                    foreach (var d in dictMiscIds)
                    {
                        DataRow row = miscDetailsTable.NewRow();
                        row["MID"] = Convert.ToInt32(d.Key);
                        row["IsChecked"] = d.Value;
                        miscDetailsTable.Rows.Add(row);
                    }

                    this.miscSelectedData = GetXmlString(miscDetailsTable);
                    this.xmlFile = "<Root>" + miscSelectedData + "</Root>";
                    if (this.miscSelectedData.Equals("<MiscImps/>"))
                    {
                        this.miscSelectedData = null;
                    }
                }
                this.EnableProcessButton();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Selects the un select all and Unselect all for 163 sprint.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SelectUnSelectAll(string status)
        {
            if (this.miscDataTableListRowCount > 0)
            {
                for (int count = 0; count < this.miscDataTableListRowCount; count++)
                {
                    this.MiscImprovementGridView.Rows[count].Cells["MiscStatus"].Value = status;
                }
            }
        }

        /// <summary>
        /// Calculates the selected Crops for TSCO - D36040.F36041 Crop Form - New "Remove' button .
        /// </summary>
        private void CalculateSelectAllCrops(bool isChecked)
        {
            try
            {
                this.MiscImprovementGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.miscDataTableListRowCount; count++)
                {
                    if (isChecked == true)
                    {
                        this.selectedMiscIds.Add(Convert.ToInt32(this.MiscImprovementGridView[0, count].Value));
                        this.dictMiscIds.Add(Convert.ToInt32(this.MiscImprovementGridView[0,count].Value), this.MiscImprovementGridView.Rows[count].Cells["MiscStatus"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Calculates the Unselected receipts Crops for TSCO - D36040.F36041 Crop Form - New "Remove' button.
        /// </summary>
        private void CalculateUnSelectCrops(bool isChecked)
        {
            try
            {
                this.MiscImprovementGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.miscDataTableListRowCount; count++)
                {
                    if (isChecked == false)
                    {                        
                        this.selectedMiscIds.Remove(Convert.ToInt32(this.MiscImprovementGridView[0, count].Value));
                        this.dictMiscIds.Remove(Convert.ToInt32(this.MiscImprovementGridView[0, count].Value));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Process Button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcess_Click(object sender, EventArgs e)
        {       
            bool isNewObject=false;
            bool isNewValueSlice=false;
            if(CreateNewObjectComboBox.SelectedIndex == 2)
            {
                isNewObject=false;
            }
            else if(CreateNewObjectComboBox.SelectedIndex == 1)
            {
                isNewObject= true;
            }

            if(this.NewValueSliceComboBox.SelectedIndex == 2)
            {
                isNewValueSlice=false;
            }
            else if(NewValueSliceComboBox.SelectedIndex == 1)
            {
                isNewValueSlice= true;
            }

            string copyMove=tempCopyMoveComboValue;
            int parcelid=parcelId;
            int existingObjectId=objectId;

            int newObjectTypeId =newObjectId;
            int existingValueSlice = valueSliceNewID;

            string miscImprovements = xmlFile;
            int userid =this.userID;
            DialogResult dialogResult = MessageBox.Show("Are you sure that you want to " + tempCopyMoveComboValue + " these Misc Improvements?", "TerraScan – Push Legal to Future Years", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.copyorMoveDataSet = this.form3602Controller.WorkItem.F3602_ProcessMiscImprovements(copyMove, parcelid, isNewObject, existingObjectId, newObjectTypeId, isNewValueSlice, existingValueSlice, miscImprovements, userid);
                DialogResult resultMessage = MessageBox.Show(tempCopyMoveComboValue + " process completed successfully. ", formTitle, MessageBoxButtons.OK,MessageBoxIcon.Information);
                if (resultMessage == DialogResult.OK)
                {
                        SliceReloadActiveRecord sliceRecord;
                        sliceRecord.MasterFormNo=31011;
                        sliceRecord.SelectedKeyId=this.valuesliceId;
                        this.D9030_F9030_LoadSliceDetails(this, new DataEventArgs<SliceReloadActiveRecord>(sliceRecord));
                }   
                this.Close();
            }
            else
            {

            }            
        }

        /// <summary>
        /// Cancel Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// MiscImprovement GridView CellClick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiscImprovementGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        if (!string.IsNullOrEmpty(this.MiscImprovementGridView[0, e.RowIndex].Value.ToString().Trim()))
            //        {
            //            int.TryParse(this.MiscImprovementGridView[0, e.RowIndex].Value.ToString().Trim(), out this.miscImproveID);
            //        }

            //        if (e.RowIndex == 0)
            //        {
            //            this.MiscImprovementGridView[this.MiscCode.Name, e.RowIndex].ReadOnly = false;
            //            this.MiscImprovementGridView[this.MiscDescription.Name, e.RowIndex].ReadOnly = false;
            //            this.MiscImprovementGridView.Rows[e.RowIndex].Selected = false;
            //        }

            //        bool hasValues = false;
            //        if (e.RowIndex >= 1)
            //        {
            //            if ((string.IsNullOrEmpty(this.MiscImprovementGridView[this.MiscCode.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MiscImprovementGridView[this.MiscDescription.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
            //            {
            //                if (e.RowIndex + 1 < MiscImprovementGridView.RowCount)
            //                {
            //                    for (int i = e.RowIndex; i < MiscImprovementGridView.RowCount; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty(this.MiscImprovementGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MiscImprovementGridView.Rows[i].Cells[2].Value.ToString().Trim()))
            //                        {
            //                            hasValues = true;
            //                            break;
            //                        }
            //                    }

            //                    if (hasValues)
            //                    {
            //                        this.MiscImprovementGridView[this.MiscCode.Name, e.RowIndex].ReadOnly = false;
            //                        this.MiscImprovementGridView[this.MiscDescription.Name, e.RowIndex].ReadOnly = false;
            //                        this.MiscImprovementGridView.Rows[e.RowIndex].Selected = false;
            //                    }
            //                    else
            //                    {
            //                        if ((string.IsNullOrEmpty(this.MiscImprovementGridView[this.MiscCode.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MiscImprovementGridView[this.MiscDescription.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
            //                        {
            //                            this.MiscImprovementGridView[this.MiscCode.Name, e.RowIndex].ReadOnly = true;
            //                            this.MiscImprovementGridView[this.MiscDescription.Name, e.RowIndex].ReadOnly = true;
            //                        }
            //                        else
            //                        {
            //                            this.MiscImprovementGridView[this.MiscCode.Name, e.RowIndex].ReadOnly = false;
            //                            this.MiscImprovementGridView[this.MiscDescription.Name, e.RowIndex].ReadOnly = false;
            //                            this.MiscImprovementGridView.Rows[e.RowIndex].Selected = false;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    this.MiscImprovementGridView[this.MiscCode.Name, e.RowIndex].ReadOnly = true;
            //                    this.MiscImprovementGridView[this.MiscDescription.Name, e.RowIndex].ReadOnly = true;
            //                }
            //            }
            //            else
            //            {
            //                this.MiscImprovementGridView[this.MiscCode.Name, e.RowIndex].ReadOnly = false;
            //                this.MiscImprovementGridView[this.MiscDescription.Name, e.RowIndex].ReadOnly = false;
            //                this.MiscImprovementGridView.Rows[e.RowIndex].Selected = false;
            //            }
            //        }

            //        this.currentRowIndex = e.RowIndex;
            //        this.currentColumnIndex = e.ColumnIndex;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            //}
            ////if (this.MiscImprovementGridView.OriginalRowCount > 0)
            ////{
            ////    this.miscDetailsTable.Clear();
            ////    int currentcolumnclicked = e.ColumnIndex;
            ////    int currentrowclicked = e.RowIndex;
            ////    miscImproveID = Convert.ToInt32(this.MiscImprovementGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
      
            ////    DataGridViewRow obd = MiscImprovementGridView.Rows[currentrowclicked];
            ////    obd.Cells["MiscStatus"].Value = true;

            ////    foreach (DataGridViewRow r in MiscImprovementGridView.Rows)
            ////    {
            ////        bool isChecked;
            ////        DataGridViewCheckBoxCell chkVal = (DataGridViewCheckBoxCell)r.Cells["MiscStatus"];
            ////        if (chkVal.Value != null)
            ////        {
            ////            isChecked = (bool)r.Cells["MiscStatus"].Value;
            ////            if (isChecked)
            ////            {
            ////                DataRow row = miscDetailsTable.NewRow();
            ////                row["MID"] = Convert.ToInt32(this.MiscImprovementGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            ////                row["IsChecked"] = this.MiscImprovementGridView.Rows[currentrowclicked].Cells["MiscStatus"].Value;
            ////                miscDetailsTable.Rows.Add(row);
            ////            }
            ////        }
            ////    }
                    
               
            ////    this.miscSelectedData = GetXmlString(miscDetailsTable);

            ////    this.xmlFile = "<Root>" + miscSelectedData + "</Root>";
                
            ////    //XmlDocument docXml = new XmlDocument();
            ////    //docXml.Load(miscSelectedData);
            ////    //XmlElement ele = docXml.CreateElement("NewlyAdded");
            ////    //XmlAttribute attr = docXml.CreateAttribute("attr");
            ////    //attr.InnerText = "NewlyAttr";
            ////    //ele.Attributes.Append(attr);
            ////    //docXml.DocumentElement.AppendChild(ele);
            ////    //string xmlChild = docXml.ToString();

            ////    if (this.miscSelectedData.Equals("<Root />"))
            ////    {
            ////        this.miscSelectedData = null;
            ////    }
            ////    this.EnableProcessButton();
            ////}
        }

        /// <summary>
        /// Valuslice GridView ContentClick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValuesliceGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < this.ValuesliceGridView.OriginalRowCount)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (e.ColumnIndex.Equals(this.ValuesliceGridView.Columns["ValueStatus"].Index))
                        {
                            if (this.ValuesliceGridView.OriginalRowCount > 0)
                            {
                                int currentcolumnclicked = e.ColumnIndex;
                                int currentrowclicked = e.RowIndex;
                                DataGridViewRow rowVal = ValuesliceGridView.Rows[e.RowIndex];

                                foreach (DataGridViewRow drs in ValuesliceGridView.Rows)
                                {
                                    DataGridViewCheckBoxCell cell = drs.Cells["ValueStatus"] as DataGridViewCheckBoxCell;
                                    if (cell.Value != null)
                                    {
                                        if (cell.Value.Equals(true))
                                        {
                                            if (rowVal.Index == drs.Index)
                                            {
                                                drs.Cells["ValueStatus"].Value = true;//It's checked!
                                            }
                                            else
                                            {
                                                drs.Cells["ValueStatus"].Value = false;
                                            }
                                        }
                                    }
                                }
                                this.valueSliceNewID = Convert.ToInt32(this.ValuesliceGridView.Rows[e.RowIndex].Cells["SliceTypeID"].Value.ToString());
                                DataGridViewRow rowChange = ValuesliceGridView.Rows[e.RowIndex];
                                rowChange.Cells["ValueStatus"].Value = true;
                                rowChange.Cells["ValueStatus"].ReadOnly = true;

                                if ((bool)rowChange.Cells["ValueStatus"].Value)
                                {
                                    checkCount++;
                                }
                                else
                                {
                                    checkCount = 0;
                                }

                                if (NewValueSliceComboBox.SelectedIndex == 2)
                                {
                                    if (checkCount == 0)
                                    {
                                        this.valueSliceNewID = 0;
                                    }
                                }
                                this.EnableProcessButton();
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Enable Process Button.
        /// </summary>
        private void EnableProcessButton()
        {
            if (CopyMoveComboBox.SelectedIndex != 0 && CopyMoveComboBox.SelectedIndex != -1 && this.LinkToTextBox.Text == string.Empty && this.CreateNewObjectComboBox.SelectedIndex != 0 && this.CreateNewObjectComboBox.SelectedIndex != -1 && objectId != 0 && ObjectTypeComboBox.SelectedIndex != 0 && ObjectTypeComboBox.SelectedIndex != -1 && valueSliceNewID != 0 && NewValueSliceComboBox.SelectedIndex != -1 && NewValueSliceComboBox.SelectedIndex != 0 && selectedMiscIds.Count > 0)
            {
                this.btnProcess.Enabled = true;
            }
            else if (this.CreateNewObjectComboBox.SelectedIndex == 1)
            {
                if (ObjectTypeComboBox.SelectedIndex != 0 && ObjectTypeComboBox.SelectedIndex != -1 && selectedMiscIds.Count > 0)
                {
                    this.btnProcess.Enabled = true;
                }
                else
                {
                    this.btnProcess.Enabled = false;
                }
            }
            else if (this.CreateNewObjectComboBox.SelectedIndex == 2)
            {
                if (objectId != 0 && NewValueSliceComboBox.SelectedIndex == 2 && valueSliceNewID != 0 && selectedMiscIds.Count > 0)
                {
                    this.btnProcess.Enabled = true;
                }
                else if (objectId != 0 && NewValueSliceComboBox.SelectedIndex == 1 && selectedMiscIds.Count > 0)
                {
                    this.btnProcess.Enabled = true;
                }
                else
                {
                    this.btnProcess.Enabled = false;
                }
            }
            else
            {
                this.btnProcess.Enabled = false;
            }
        }

        /// <summary>
        /// Object Type combobox selection committed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int id;
            bool selected = int.TryParse(ObjectTypeComboBox.SelectedValue.ToString(), out id);
            newObjectId = id;
            this.EnableProcessButton();
        }

        #region Help Link
        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Help Link

        /// <summary>
        /// Gets the XML string.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>the XML String</returns>
        private static string GetXmlString(DataTable dt)
        {
            DataSet ds = new DataSet("MiscImps");
            DataTable tempDt = new DataTable();
            if (dt != null)
            {
                tempDt = dt.Copy();
            }

            tempDt.TableName = "MiscImp";
            ds.Tables.Add(tempDt);
            return ds.GetXml();
        }

        private void HelpLink_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Tag = 3602;
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        private void MiscImprovementGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Add and remove the selected items for remove to select the gridview checkbox.
                if (e.RowIndex >= 0 && e.RowIndex < this.MiscImprovementGridView.OriginalRowCount)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (e.ColumnIndex.Equals(this.MiscImprovementGridView.Columns["MiscStatus"].Index))
                        {
                            int cropId;
                            int.TryParse(this.MiscImprovementGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out cropId);
                            if (cropId > 0)
                            {   
                                if (Convert.ToBoolean(this.MiscImprovementGridView.Rows[e.RowIndex].Cells[3].EditedFormattedValue) == true)
                                {
                                    this.MiscImprovementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                                    if (this.selectedMiscIds.Contains(cropId))
                                    {
                                        this.selectedMiscIds.Remove(Convert.ToInt32(this.MiscImprovementGridView[0, e.RowIndex].Value));
                                        this.dictMiscIds.Remove(Convert.ToInt32(this.MiscImprovementGridView[0, e.RowIndex].Value));
                                    }
                                    if (this.miscDataTableListRowCount > this.selectedMiscIds.Count)
                                    {
                                        this.SelectAllCheckbox.Checked = false;
                                    }
                                }
                                else
                                {
                                    this.MiscImprovementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                                    if (!this.selectedMiscIds.Contains(cropId))
                                    {
                                        this.selectedMiscIds.Add(Convert.ToInt32(this.MiscImprovementGridView[0, e.RowIndex].Value));
                                        this.dictMiscIds.Add(Convert.ToInt32(this.MiscImprovementGridView[0, e.RowIndex].Value), this.MiscImprovementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                                    }
                                    if (this.miscDataTableListRowCount == this.selectedMiscIds.Count)
                                    {
                                        this.SelectAllCheckbox.Checked = true;
                                    }
                                }
                                miscImproveID = cropId;

                                    this.miscDetailsTable.Clear();
                                    //for (int count = 0; count < this.selectedMiscIds.Count; count++)
                                    //{
                                        foreach (var d in dictMiscIds)
                                        {
                                            DataRow row = miscDetailsTable.NewRow();
                                            row["MID"] = Convert.ToInt32(d.Key);
                                            row["IsChecked"] = d.Value;
                                            miscDetailsTable.Rows.Add(row);
                                        }
                                    //}
                                    this.miscSelectedData = GetXmlString(miscDetailsTable);
                                    this.xmlFile = "<Root>" + miscSelectedData + "</Root>";
                                
                                this.EnableProcessButton();
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

        private void MiscImprovementGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.MiscImprovementGridView.Columns[0].Index || e.ColumnIndex == this.MiscImprovementGridView.Columns["MiscCode"].Index || e.ColumnIndex == this.MiscImprovementGridView.Columns["MiscDescription"].Index || e.ColumnIndex == this.MiscImprovementGridView.Columns["MiscStatus"].Index)
                {
                    this.MiscImprovementGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void MiscImprovementGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.MiscImprovementGridView.CurrentCell != null)
                {
                    this.MiscImprovementGridView.CurrentCell.ReadOnly = true;
                    this.MiscImprovementGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ValuesliceGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }       
    }
}


