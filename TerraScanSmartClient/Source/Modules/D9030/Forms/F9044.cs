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
    using System.Windows;

    #endregion NameSpaces
    public partial class F9044 : BasePage
    {
        private F9044Controller form9044Control;
        private int snapId;
        private int formNum;
        private int userNum;
        private PermissionFields activeFormMasterPermissionFields;
        private F9044SnapshotOperations snapshotDetailsDataSet = new F9044SnapshotOperations();
        private F9044SnapshotOperations snapshotCountDataSet = new F9044SnapshotOperations();
        private int OperationId;
        private int ROSnapshotId;
        private int LOSnapshotId;
        private string newSnapshotName;
        private int RecordCount;
        public F9044()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="F9044"/> class.
        /// </summary>
        /// <param name="snapshotId">The snapshot id.</param>
        /// <param name="formmasterNum">The formmaster num.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="formMasterPermissionFields">The form master permission fields.</param>
        public F9044(int snapshotId, int formmasterNum, int userId, PermissionFields formMasterPermissionFields)
        {
            this.InitializeComponent();
            this.snapId = snapshotId;
            this.formNum = formmasterNum;
            this.userNum = userId;
            this.activeFormMasterPermissionFields = formMasterPermissionFields;

        }
        #region Property

        /// <summary>
        /// For F9043Control
        /// </summary>
        [CreateNew]
        public F9044Controller Form9044Control
        {
            get { return this.form9044Control as F9044Controller; }
            set { this.form9044Control = value; }
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the F9044 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F9044_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSnapshotDetails();
                // this.NewSnapShotNameTextBox.Enabled = true;
                this.RecorsInNewSnapshotTextBox.Enabled = false;
                this.RecorsInCommonTextBox.Enabled = false;
                this.CreateSnapshotOperationButton.Enabled = false;
                this.NewSnapShotNameTextBox.Text = string.Empty;
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
        /// Handles the Click event of the CreateSnapshotOperationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CreateSnapshotOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                int newRecordCount = 0;
                if (!string.IsNullOrEmpty(this.NewSnapShotNameTextBox.Text))
                {
                    this.newSnapshotName = this.NewSnapShotNameTextBox.Text;
                }
                if (!string.IsNullOrEmpty(this.newSnapshotName) && (this.OperationId != null) && (this.LOSnapshotId > 0) && (this.ROSnapshotId > 0) && (this.RecordCount > 0))
                {
                    if (!string.IsNullOrEmpty(this.RecorsInNewSnapshotTextBox.Text))
                    {
                        newRecordCount = Convert.ToInt32(this.RecorsInNewSnapshotTextBox.Text);
                    }
                    form9044Control.WorkItem.insertSnapshotDetails(this.OperationId, this.LOSnapshotId, this.ROSnapshotId, newRecordCount, this.newSnapshotName, TerraScanCommon.UserId);
                    this.CloseSnapshotOperationButton.Enabled = false;
                    this.NewSnapShotNameTextBox.Text = string.Empty;
                    this.Close();
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
        /// Handles the Click event of the CloseSnapshotOperationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CloseSnapshotOperationButton_Click(object sender, EventArgs e)
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
            // this.Close();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the OperationComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OperationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.OperationComboBox.SelectedIndex == 0)
                {
                    this.OperationId = this.OperationComboBox.SelectedIndex;
                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9044_pcget_Operations.Rows[0][this.snapshotDetailsDataSet.f9044_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
                }

                if (this.OperationComboBox.SelectedIndex == 1)
                {
                    this.OperationId = this.OperationComboBox.SelectedIndex;
                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9044_pcget_Operations.Rows[1][this.snapshotDetailsDataSet.f9044_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
                }
                if (this.OperationComboBox.SelectedIndex == 2)
                {
                    this.OperationId = this.OperationComboBox.SelectedIndex;
                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9044_pcget_Operations.Rows[2][this.snapshotDetailsDataSet.f9044_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
                }
                LoadRecordDetails();
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
        /// Loads the snapshot details.
        /// </summary>
        public void LoadSnapshotDetails()
        {
            try
            {
                this.snapshotDetailsDataSet = this.form9044Control.WorkItem.GetSnapshotDetails(this.formNum, TerraScanCommon.UserId);
                if (snapshotDetailsDataSet.f9044_pcget_SnapshotOperations.Rows.Count > 0)
                {
                    DataTable dtBottomGrid = new DataTable();
                    dtBottomGrid = snapshotDetailsDataSet.f9044_pcget_SnapshotOperations.Copy();
                    this.SnapshotoperationTopGrid.DataSource = snapshotDetailsDataSet.f9044_pcget_SnapshotOperations;
                    this.SnapshotoperationTopGrid.DataBind();
                    DataRow[] dr = snapshotDetailsDataSet.f9044_pcget_SnapshotOperations.Select("SnapshotID= '" + snapId + "'");
                    foreach (DataRow drow in snapshotDetailsDataSet.f9044_pcget_SnapshotOperations)
                    {
                        this.BottomSnapshotOperationGrid.DataSource = dtBottomGrid;
                        this.BottomSnapshotOperationGrid.DataBind();
                        int strIndex;
                        if (drow.ItemArray.Length > 0)
                        {
                            int intID = Convert.ToInt32(drow["SnapshotID"]);
                            if (snapId == intID)
                            {
                                strIndex = snapshotDetailsDataSet.f9044_pcget_SnapshotOperations.Rows.IndexOf(drow);
                                var ultraRow = this.SnapshotoperationTopGrid.Rows.GetRowWithListIndex(strIndex);
                                int index = ultraRow.Index;
                                this.SnapshotoperationTopGrid.Rows[index].Cells[0].Activate();
                                this.SnapshotoperationTopGrid.Rows[index].Selected = true;
                                this.SnapshotoperationTopGrid.ActiveRowScrollRegion.FirstRow = this.SnapshotoperationTopGrid.ActiveRow;
                            }
                        }

                    }
                    if (snapId > 0)
                    {
                        LOSnapshotId = snapId;
                    }

                }
                if (this.snapshotDetailsDataSet.f9044_pcget_Operations.Rows.Count > 0)
                {
                    this.OperationComboBox.DataSource = this.snapshotDetailsDataSet.f9044_pcget_Operations;
                    this.OperationComboBox.DisplayMember = this.snapshotDetailsDataSet.f9044_pcget_Operations.OperationColumn.ColumnName;
                    this.OperationComboBox.ValueMember = this.snapshotDetailsDataSet.f9044_pcget_Operations.OperationIDColumn.ColumnName;
                    this.bottomGridLabel.Text = this.snapshotDetailsDataSet.f9044_pcget_Operations.Rows[0][this.snapshotDetailsDataSet.f9044_pcget_Operations.OperationDescriptionColumn.ColumnName].ToString();
                    this.OperationId = this.OperationComboBox.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the record details.
        /// </summary>
        public void LoadRecordDetails()
        {
            if (this.LOSnapshotId > 0 && this.OperationId != null && this.ROSnapshotId > 0)
            {
                this.snapshotCountDataSet = this.form9044Control.WorkItem.GetSnapshotOperationCount(this.OperationId, this.LOSnapshotId, this.ROSnapshotId, TerraScanCommon.UserId);
                // this.snapshotDetailsDataSet.f9044_pcget_SnapshotOperationCount.Merge( this.form9044Control.WorkItem.GetSnapshotOperationCount(this.OperationId,this.LOSnapshotId,this.ROSnapshotId, TerraScanCommon.UserId).f9044_pcget_SnapshotOperationCount);
                if (this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.Rows.Count > 0 && this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.Rows.Count != null)
                {
                    if (!string.IsNullOrEmpty(this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.RecordsInCommonColumn.ColumnName].ToString()))
                    {
                        this.RecorsInCommonTextBox.Text = this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.RecordsInCommonColumn.ColumnName].ToString();
                    }
                    if (!string.IsNullOrEmpty(this.RecorsInNewSnapshotTextBox.Text = this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.RecordsInNewSnapshotColumn.ColumnName].ToString()))
                    {
                        this.RecorsInNewSnapshotTextBox.Text = this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.Rows[0][this.snapshotCountDataSet.f9044_pcget_SnapshotOperationCount.RecordsInNewSnapshotColumn.ColumnName].ToString();
                    }
                }
                //if(!string.IsNullOrEmpty(this.RecorsInCommonTextBox.Text) && (!string.IsNullOrEmpty(this.RecorsInNewSnapshotTextBox.Text)))
                //{
                //    this.RecordCount = Convert.ToInt32(this.RecorsInCommonTextBox.Text) + Convert.ToInt32(this.RecorsInNewSnapshotTextBox.Text);//this.RecorsInCommonTextBox.Text +
                //}
                if (!string.IsNullOrEmpty(this.RecorsInCommonTextBox.Text))
                {
                    if (!string.IsNullOrEmpty(this.RecorsInNewSnapshotTextBox.Text))
                    {
                        this.RecordCount = Convert.ToInt32(this.RecorsInCommonTextBox.Text) + Convert.ToInt32(this.RecorsInNewSnapshotTextBox.Text);//this.RecorsInCommonTextBox.Text +
                    }
                    else
                    {
                        this.RecordCount = Convert.ToInt32(this.RecorsInCommonTextBox.Text);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.RecorsInNewSnapshotTextBox.Text))
                    {
                        this.RecordCount = Convert.ToInt32(this.RecorsInNewSnapshotTextBox.Text);
                    }
                }
                if (!string.IsNullOrEmpty(this.NewSnapShotNameTextBox.Text) && (this.LOSnapshotId > 0) && (this.OperationId != null) && (this.ROSnapshotId > 0) && (this.RecordCount > 0))
                {
                    this.CreateSnapshotOperationButton.Enabled = true;
                }
                else
                {
                    this.CreateSnapshotOperationButton.Enabled = false;
                }

            }
        }

        /// <summary>
        /// Handles the AfterSelectChange event of the SnapshotoperationTopGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs"/> instance containing the event data.</param>
        private void SnapshotoperationTopGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            try
            {
                if (this.SnapshotoperationTopGrid.Selected.Cells.Count > 0)
                {
                    this.SnapshotoperationTopGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;

                }


                if (this.SnapshotoperationTopGrid.Selected.Rows.Count > 0)
                {

                    UltraGridRow dr = this.SnapshotoperationTopGrid.Selected.Rows[0];
                    if (dr != null)
                    {
                        int index = dr.Index;
                        this.LOSnapshotId = Convert.ToInt32(dr.Cells["SnapshotID"].Value);

                    }

                }
                LoadRecordDetails();
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
        /// Handles the ClickCell event of the SnapshotoperationTopGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.ClickCellEventArgs"/> instance containing the event data.</param>
        private void SnapshotoperationTopGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (this.SnapshotoperationTopGrid.Rows.Count > 0)
            {
                if (this.SnapshotoperationTopGrid.ActiveRow != null)
                {
                    if (this.SnapshotoperationTopGrid.ActiveRow.Index >= 0)
                    {
                        this.SnapshotoperationTopGrid.Rows[this.SnapshotoperationTopGrid.ActiveRow.Index].Selected = true;
                        SnapshotoperationTopGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
                    }
                }
                if (SnapshotoperationTopGrid.Selected.Rows.Count > 0)
                {

                    UltraGridRow dr = this.SnapshotoperationTopGrid.Selected.Rows[0];
                    if (dr != null)
                    {
                        int index = dr.Index;
                        LOSnapshotId = Convert.ToInt32(dr.Cells["SnapshotID"].Value);

                    }

                }
            }
        }

        /// <summary>
        /// Handles the AfterSelectChange event of the BottomSnapshotOperationGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs"/> instance containing the event data.</param>
        private void BottomSnapshotOperationGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            try
            {
                if (this.BottomSnapshotOperationGrid.Selected.Cells.Count > 0)
                {
                    this.BottomSnapshotOperationGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;

                }


                if (this.BottomSnapshotOperationGrid.Selected.Rows.Count > 0)
                {

                    UltraGridRow drow = this.BottomSnapshotOperationGrid.Selected.Rows[0];
                    if (drow != null)
                    {
                        int index = drow.Index;
                        this.ROSnapshotId = Convert.ToInt32(drow.Cells["SnapshotID"].Value);

                    }

                }
                LoadRecordDetails();
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
        /// Handles the TextChanged event of the NewSnapShotNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewSnapShotNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((this.OperationId != null) && (this.LOSnapshotId > 0) && (this.ROSnapshotId > 0) && (this.RecordCount > 0) && (!string.IsNullOrEmpty(this.NewSnapShotNameTextBox.Text)))
            {
                this.CreateSnapshotOperationButton.Enabled = true;
            }
            else
            {
                this.CreateSnapshotOperationButton.Enabled = false;

            }
        }

        /// <summary>
        /// Handles the KeyDown event of the F9044 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void F9044_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ClickCell event of the BottomSnapshotOperationGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.ClickCellEventArgs"/> instance containing the event data.</param>
        private void BottomSnapshotOperationGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (this.BottomSnapshotOperationGrid.Rows.Count > 0)
            {
                if (BottomSnapshotOperationGrid.ActiveRow != null)
                {
                    if (this.BottomSnapshotOperationGrid.ActiveRow.Index >= 0)
                    {
                        this.BottomSnapshotOperationGrid.Rows[this.BottomSnapshotOperationGrid.ActiveRow.Index].Selected = true;
                        this.BottomSnapshotOperationGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
                    }
                }
            }
        }
        /// <summary>
        /// Handles the MouseClick event of the SnapshotoperationTopGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void SnapshotoperationTopGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.SnapshotoperationTopGrid.Rows.Count > 0)
            {
                if (this.SnapshotoperationTopGrid.ActiveRow != null)
                {
                    if (this.SnapshotoperationTopGrid.ActiveRow.Index >= 0)
                    {
                        this.SnapshotoperationTopGrid.Rows[this.SnapshotoperationTopGrid.ActiveRow.Index].Selected = true;
                        SnapshotoperationTopGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
                    }
                }
                
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the BottomSnapshotOperationGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void BottomSnapshotOperationGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.BottomSnapshotOperationGrid.Rows.Count > 0)
            {
                if (this.BottomSnapshotOperationGrid.ActiveRow != null)
                {
                    if (this.BottomSnapshotOperationGrid.ActiveRow.Index >= 0)
                    {
                        this.BottomSnapshotOperationGrid.Rows[this.BottomSnapshotOperationGrid.ActiveRow.Index].Selected = true;
                        this.BottomSnapshotOperationGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
                    }

                }
            }
        }

    }
}
