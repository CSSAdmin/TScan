//namespace D9030
//{
//    #region NameSpaces

//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel;
//    using System.Data;
//    using System.Drawing;
//    using System.Text;
//    using System.Windows.Forms;
//    using TerraScan.Common;
//    using Microsoft.Practices.CompositeUI.UIElements;
//    using Microsoft.Practices.CompositeUI.SmartParts;
//    using Microsoft.Practices.CompositeUI.EventBroker;
//    using Microsoft.Practices.CompositeUI.Utility;
//    using Microsoft.Practices.ObjectBuilder;
//    using TerraScan.BusinessEntities;
//    using TerraScan.Infrastructure.Interface.Constants;
//    using TerraScan.Utilities;
//    using System.Configuration;
//    using Infragistics.Win;
//    using Infragistics.Excel;
//    using System.IO;
//    using System.Collections;
//    using System.Diagnostics;
//    using Infragistics.Win.UltraWinGrid;
//    using Infragistics.Win.UltraWinCalcManager;
//    using TerraScan.Helper;
//    using System.Windows;
    
//    #endregion NameSpaces


//    /// <summary>
//    /// 
//    /// </summary>
//    public partial class F9043 : BasePage
//    {
//        private F9043Controller form9043Control;
//        private int snapId;
//        private int formNum;
//        private int userNum;
//        private PermissionFields activeFormMasterPermissionFields;
//        private F9043SnapshotOperations snapshotDetailsDataSet = new F9043SnapshotOperations();
//        private F9043SnapshotOperations snapshotCountDataSet = new F9043SnapshotOperations();
//        private int OperationId;
//        private int ROSnapshotId;
//        private int LOSnapshotId;
//        private string newSnapshotName;
//        private int RecordCount;
      

//        public F9043()
//        {
//            InitializeComponent();
           
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="F9043"/> class.
//        /// </summary>
//        /// <param name="snapshotId">The snapshot id.</param>
//        /// <param name="formmasterNum">The formmaster num.</param>
//        /// <param name="userId">The user id.</param>
//        /// <param name="formMasterPermissionFields">The form master permission fields.</param>
//        public F9043(int snapshotId, int formmasterNum, int userId, PermissionFields formMasterPermissionFields)
//        {
//            this.InitializeComponent();
//            this.snapId = snapshotId;
//            this.formNum = formmasterNum;
//            this.userNum = userId;
//            this.activeFormMasterPermissionFields = formMasterPermissionFields;

//        }
//        #region Property

//        /// <summary>
//        /// For F9043Control
//        /// </summary>
//        [CreateNew]
//        public F9043Controller Form9043Control
//        {
//            get { return this.form9043Control as F9043Controller; }
//            set { this.form9043Control = value; }
//        }

//        #endregion

//        /// <summary>
//        /// Handles the Load event of the F9043 control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
//        private void F9043_Load(object sender, EventArgs e)
//        {
//            try
//            {
//                LoadSnapshotDetails();
//               // this.NewSnapShotNameTextBox.Enabled = true;
//                this.RecorsInNewSnapshotTextBox.Enabled = false;
//                this.RecorsInCommonTextBox.Enabled = false;
//                this.CreateSnapshotOperationButton.Enabled = false;
//                this.NewSnapShotNameTextBox.Text = string.Empty;
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//            finally
//            {
//                this.Cursor = Cursors.Default;
//            }
//        }

//        /// <summary>
//        /// Handles the Click event of the CreateSnapshotOperationButton control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
//        private void CreateSnapshotOperationButton_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (!string.IsNullOrEmpty(this.NewSnapShotNameTextBox.Text))
//                {
//                    this.newSnapshotName = this.NewSnapShotNameTextBox.Text;
//                }
//                if (!string.IsNullOrEmpty(this.newSnapshotName) && (this.OperationId != null) && (this.LOSnapshotId > 0) && (this.ROSnapshotId > 0) && (this.RecordCount > 0))
//                {
                  
//                   form9043Control.WorkItem.insertSnapshotDetails(this.OperationId, this.LOSnapshotId, this.ROSnapshotId, this.RecordCount, this.newSnapshotName, TerraScanCommon.UserId);
//                   this.CloseSnapshotOperationButton.Enabled = false;
//                   this.NewSnapShotNameTextBox.Text = string.Empty;
//                   this.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//            finally
//            {
//                this.Cursor = Cursors.Default;
//            }
//        }

//        /// <summary>
//        /// Handles the Click event of the CloseSnapshotOperationButton control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
//        private void CloseSnapshotOperationButton_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                this.DialogResult = DialogResult.Cancel;
//                this.Close();
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//           // this.Close();
//        }

//        /// <summary>
//        /// Handles the SelectedIndexChanged event of the OperationComboBox control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
//        private void OperationComboBox_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                if (this.OperationComboBox.SelectedIndex == 0)
//                {
//                    this.OperationId = this.OperationComboBox.SelectedIndex;
//                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9043_pcget_Operations.Rows[0][this.snapshotDetailsDataSet.f9043_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
//                }

//                if (this.OperationComboBox.SelectedIndex == 1)
//                {
//                    this.OperationId = this.OperationComboBox.SelectedIndex;
//                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9043_pcget_Operations.Rows[1][this.snapshotDetailsDataSet.f9043_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
//                }
//                if (this.OperationComboBox.SelectedIndex == 2)
//                {
//                    this.OperationId = this.OperationComboBox.SelectedIndex;
//                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9043_pcget_Operations.Rows[2][this.snapshotDetailsDataSet.f9043_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
//                }
//                LoadRecordDetails();
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//            finally
//            {
//                this.Cursor = Cursors.Default;
//            }
//        }
       
//        /// <summary>
//        /// Loads the snapshot details.
//        /// </summary>
//        public void LoadSnapshotDetails()
//        {
//            try
//            {
                
//                this.snapshotDetailsDataSet = this.form9043Control.WorkItem.GetSnapshotDetails(this.formNum, TerraScanCommon.UserId);
//                if (snapshotDetailsDataSet.f9043_pcget_SnapshotOperations.Rows.Count > 0)
//                {
//                    DataTable dtBottomGrid = new DataTable();
//                    dtBottomGrid = snapshotDetailsDataSet.f9043_pcget_SnapshotOperations.Copy();
//                    this.SnapshotoperationTopGrid.DataSource = snapshotDetailsDataSet.f9043_pcget_SnapshotOperations;
//                    this. SnapshotoperationTopGrid.DataBind();
//                    DataRow[] dr = snapshotDetailsDataSet.f9043_pcget_SnapshotOperations.Select("SnapshotID= '" + snapId + "'");
//                    foreach (DataRow drow in snapshotDetailsDataSet.f9043_pcget_SnapshotOperations)
//                    {
//                        this.BottomSnapshotOperationGrid.DataSource = dtBottomGrid;
//                        this.BottomSnapshotOperationGrid.DataBind();
//                        int strIndex;
//                        if (drow.ItemArray.Length > 0)
//                        {
//                            int intID = Convert.ToInt32(drow["SnapshotID"]);
//                            if (snapId == intID)
//                            {
//                                strIndex = snapshotDetailsDataSet.f9043_pcget_SnapshotOperations.Rows.IndexOf(drow);
//                                //SnapshotoperationTopGrid.DataSource = snapshotDetailsDataSet.f9043_pcget_SnapshotOperations;
//                                //SnapshotoperationTopGrid.DataBind();
//                                this. SnapshotoperationTopGrid.Rows[strIndex].Cells[0].Activate();
//                                this.SnapshotoperationTopGrid.Rows[strIndex].Selected = true;
//                            }
//                        }

//                    }
//                    if (snapId > 0)
//                    {
//                        LOSnapshotId = snapId;
//                    }

//                }
//                if (this.snapshotDetailsDataSet.f9043_pcget_Operations.Rows.Count > 0)
//                {
//                    this.OperationComboBox.DataSource = this.snapshotDetailsDataSet.f9043_pcget_Operations;
//                    this.OperationComboBox.DisplayMember = this.snapshotDetailsDataSet.f9043_pcget_Operations.OperationColumn.ColumnName;
//                    this.OperationComboBox.ValueMember = this.snapshotDetailsDataSet.f9043_pcget_Operations.OperationIDColumn.ColumnName;
//                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9043_pcget_Operations.Rows[0][this.snapshotDetailsDataSet.f9043_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
//                    this.OperationId = this.OperationComboBox.SelectedIndex;
//                }
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//        }

//        /// <summary>
//        /// Loads the record details.
//        /// </summary>
//        public void LoadRecordDetails()
//        {
//            if (this.LOSnapshotId > 0 && this.OperationId != null && this.ROSnapshotId > 0)
//            {
//                this.snapshotCountDataSet = this.form9043Control.WorkItem.GetSnapshotOperationCount(this.OperationId, this.LOSnapshotId, this.ROSnapshotId, TerraScanCommon.UserId);
//                // this.snapshotDetailsDataSet.f9043_pcget_SnapshotOperationCount.Merge( this.form9043Control.WorkItem.GetSnapshotOperationCount(this.OperationId,this.LOSnapshotId,this.ROSnapshotId, TerraScanCommon.UserId).f9043_pcget_SnapshotOperationCount);
//                if (this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.Rows.Count > 0 && this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.Rows.Count != null)
//                {
//                    if (!string.IsNullOrEmpty(this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.RecordsInCommonColumn.ColumnName].ToString()))
//                    {
//                        this.RecorsInCommonTextBox.Text = this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.RecordsInCommonColumn.ColumnName].ToString();
//                    }
//                    if (!string.IsNullOrEmpty(this.RecorsInNewSnapshotTextBox.Text = this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.RecordsInNewSnapshotColumn.ColumnName].ToString()))
//                    {
//                        this.RecorsInNewSnapshotTextBox.Text = this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9043_pcget_SnapshotOperationCount.RecordsInNewSnapshotColumn.ColumnName].ToString();
//                    }
//                }
//                this.RecordCount = Convert.ToInt32(this.RecorsInCommonTextBox.Text + this.RecorsInNewSnapshotTextBox.Text);

//                if (!string.IsNullOrEmpty(this.NewSnapShotNameTextBox.Text) && (this.LOSnapshotId > 0) && (this.OperationId != null) && (this.ROSnapshotId > 0) && (this.RecordCount>0))
//                {
//                    this.CreateSnapshotOperationButton.Enabled = true;
//                }
//                else
//                {
//                    this.CreateSnapshotOperationButton.Enabled = false;
//                }
               
//            }
//        }

//        /// <summary>
//        /// Handles the AfterSelectChange event of the SnapshotoperationTopGrid control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs"/> instance containing the event data.</param>
//        private void SnapshotoperationTopGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
//        {
//            try
//            {
//                if (this.SnapshotoperationTopGrid.Selected.Cells.Count > 0)
//                {
//                    this.SnapshotoperationTopGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;

//                }


//                if (this.SnapshotoperationTopGrid.Selected.Rows.Count > 0)
//                {

//                    UltraGridRow dr = this.SnapshotoperationTopGrid.Selected.Rows[0];
//                    if (dr != null)
//                    {
//                        int index = dr.Index;
//                        this.LOSnapshotId = Convert.ToInt32(dr.Cells["SnapshotID"].Value);

//                    }

//                }
//                LoadRecordDetails();
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//            finally
//            {
//                this.Cursor = Cursors.Default;
//            }
//        }

//        /// <summary>
//        /// Handles the ClickCell event of the SnapshotoperationTopGrid control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.ClickCellEventArgs"/> instance containing the event data.</param>
//        private void SnapshotoperationTopGrid_ClickCell(object sender, ClickCellEventArgs e)
//        {
//            SnapshotoperationTopGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
//            if (SnapshotoperationTopGrid.Selected.Rows.Count > 0)
//            {

//                UltraGridRow dr = this.SnapshotoperationTopGrid.Selected.Rows[0];
//                if (dr != null)
//                {
//                    int index = dr.Index;
//                    LOSnapshotId = Convert.ToInt32(dr.Cells["SnapshotID"].Value);

//                }

//            }
//        }

//        /// <summary>
//        /// Handles the AfterSelectChange event of the BottomSnapshotOperationGrid control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs"/> instance containing the event data.</param>
//        private void BottomSnapshotOperationGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
//        {
//            try
//            {
//                if (this.BottomSnapshotOperationGrid.Selected.Cells.Count > 0)
//                {
//                    this.BottomSnapshotOperationGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;

//                }


//                if (this.BottomSnapshotOperationGrid.Selected.Rows.Count > 0)
//                {

//                    UltraGridRow drow = this.BottomSnapshotOperationGrid.Selected.Rows[0];
//                    if (drow != null)
//                    {
//                        int index = drow.Index;
//                        this.ROSnapshotId = Convert.ToInt32(drow.Cells["SnapshotID"].Value);

//                    }

//                }
//                LoadRecordDetails();
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//            finally
//            {
//                this.Cursor = Cursors.Default;
//            }
//        }

//        /// <summary>
//        /// Handles the TextChanged event of the NewSnapShotNameTextBox control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
//        private void NewSnapShotNameTextBox_TextChanged(object sender, EventArgs e)
//        {
//            if ((this.OperationId != null) && (this.LOSnapshotId > 0) && (this.ROSnapshotId > 0) && (this.RecordCount > 0) && (!string.IsNullOrEmpty(this.NewSnapShotNameTextBox.Text)))
//            {
//                this.CreateSnapshotOperationButton.Enabled = true;
//            }
//            else
//            {
//                this.CreateSnapshotOperationButton.Enabled = false;

//            }
//        }

//        /// <summary>
//        /// Handles the KeyDown event of the F9043 control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
//        private void F9043_KeyDown(object sender, KeyEventArgs e)
//        {
//            try
//            {

//                if (e.KeyCode == Keys.Escape)
//                {
//                    this.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
//            }
//        }

                                             
//    }
//}
