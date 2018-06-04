// --------------------------------------------------------------------------------------------
// <copyright file="F9612.cs" company="Congruent">
//        Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//   This file contains methods for the Activity Queue form.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author           Description
// ----------        ---------         ---------------------------------------------------------
// 22 Sep 2009    D.LathaMaheswari     Created
// *********************************************************************************/
namespace D9030
{
    using System;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// Classs for F9612
    /// </summary>
    public partial class F9612 : Form
    {
        #region Variable

        /// <summary>
        /// Controller for F9612
        /// </summary>
        private F9612Controller form9612Control;

        /// <summary>
        /// DataSet contains visited records details
        /// </summary>
        private F9612ActivityQueueData recordHistoryData = new F9612ActivityQueueData();

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F9612"/> class.
        /// </summary>
        public F9612()
        {
            InitializeComponent();
            this.AcceptButton = this.AcceptRecordButton;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Occurs when [show form].
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Occurs when [on form master_ visible forms].
        /// </summary>
        [EventPublication(EventTopicNames.FormMaster_VisibleForms, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<EnablePanelEventArgs>> OnFormMaster_VisibleForms;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form9612 control.
        /// </summary>
        /// <value>The form9612 control.</value>
        [CreateNew]
        public F9612Controller Form9612Control
        {
            get { return this.form9612Control as F9612Controller; }
            set { this.form9612Control = value; }
        }

        #endregion Property

        #region Events

        /// <summary>
        /// Handles the Load event of the F9612 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F9612_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.CustomizeGrid();
                this.recordHistoryData = (F9612ActivityQueueData)this.form9612Control.WorkItem.RootWorkItem.State["RecordHistoryDataSet"];
                this.RecordHistoryGridView.DataSource = this.recordHistoryData.VisitedRecordHistory.DefaultView;
                
                // Sort the record on based on datetime of visited records
                this.RecordHistoryGridView.Sort(this.DateTime, ListSortDirection.Descending);
               
                // Set form height based on the record count
                this.SetFormHeight();
                
                // Display the form on center of the screen
                this.CenterToScreen();
                this.AcceptRecordButton.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellToolTipTextNeeded event of the RecordHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs"/> instance containing the event data.</param>
        private void RecordHistoryGridView_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    // Display visited DateTime as Tooltip for each record
                    e.ToolTipText = this.RecordHistoryGridView.Rows[e.RowIndex].Cells[this.DateTime.Name].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the RecordHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RecordHistoryGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    this.FormMaster_FormVisibility(false);
                    this.ShowSliceForm(e.RowIndex);
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
            finally
            {
                this.FormMaster_FormVisibility(true);
            }
        }

        /// <summary>
        /// Handles the Click event of the AcceptRecordButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.RecordHistoryGridView.CurrentRowIndex > -1)
                {
                    this.ShowSliceForm(this.RecordHistoryGridView.CurrentRowIndex);
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
        /// Handles the KeyUp event of the RecordHistoryGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void RecordHistoryGridView_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(13))
                {
                    if (this.RecordHistoryGridView.CurrentRowIndex > -1)
                    {
                        this.FormMaster_FormVisibility(false);
                        this.ShowSliceForm(this.RecordHistoryGridView.CurrentRowIndex);
                    }
                }
                else if (e.KeyValue.Equals(35))
                {
                    if (this.RecordHistoryGridView.OriginalRowCount > 0)
                    {
                        this.RecordHistoryGridView.ClearSelection();
                        //this.RecordHistoryGridView.Rows[this.RecordHistoryGridView.OriginalRowCount - 1].Selected = true;
                        TerraScanCommon.SetDataGridViewPosition(this.RecordHistoryGridView, this.RecordHistoryGridView.OriginalRowCount - 1);
                    }
                }
                else if (e.KeyValue.Equals(36))
                {
                    if (this.RecordHistoryGridView.OriginalRowCount > 0)
                    {
                        this.RecordHistoryGridView.ClearSelection();
                        TerraScanCommon.SetDataGridViewPosition(this.RecordHistoryGridView, 0);
                    }
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
            finally
            {
                if (e.KeyValue.Equals(13))
                {
                    if (this.RecordHistoryGridView.CurrentRowIndex > -1)
                    {
                        this.FormMaster_FormVisibility(true);
                    }
                }
            }
        }
        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Close the form on Esc key press
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the F9612 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void F9612_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            try
            {
                TerraScanCommon.activityForm = null;
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
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
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

        #endregion Events

        #region Methods

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            this.RecordHistoryGridView.AutoGenerateColumns = false;
            
            this.FormNumber.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.FormNumberColumn.ColumnName;
            this.Parameter1.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.Parameter1Column.ColumnName;
            this.Parameter2.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.Parameter2Column.ColumnName;
            this.Parameter3.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.Parameter3Column.ColumnName;
            this.Parameter4.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.Parameter4Column.ColumnName;
            this.UserId.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.UserIdColumn.ColumnName;
            this.Task.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.TaskColumn.ColumnName;
            this.DateTime.DataPropertyName = this.recordHistoryData.VisitedRecordHistory.VisitedTimeColumn.ColumnName;
        }

        /// <summary>
        /// Sets the height of the form.
        /// The form will grow until it reach record count as 20
        /// If the grid contains more than 20 records, then grid will show vertical scroll
        /// </summary>
        private void SetFormHeight()
        {
            if (this.RecordHistoryGridView.OriginalRowCount > 20)
            {
                this.LayoutMgmtVerticalScroll.Visible = false;
                this.RecordHistoryGridView.Height = 462;
                this.HistoryPanel.Height = this.RecordHistoryGridView.Height;
                this.Height = 537;
            }
            else
            {
                this.LayoutMgmtVerticalScroll.Visible = true;
                if (this.RecordHistoryGridView.OriginalRowCount > 0)
                {
                    this.RecordHistoryGridView.Height = (this.RecordHistoryGridView.OriginalRowCount * 22) 
                                                         + this.RecordHistoryGridView.ColumnHeadersHeight;
                }
                else
                {
                    // If there is no record presents in grid set grid height as Column header height + row height
                    this.RecordHistoryGridView.Height = this.RecordHistoryGridView.ColumnHeadersHeight + 22;
                } 

                this.HistoryPanel.Height = this.RecordHistoryGridView.Height;
                this.Height = this.HistoryPanel.Height + 74;
            }

            this.LayoutMgmtVerticalScroll.Height = this.RecordHistoryGridView.Height;
        }

        /// <summary>
        /// Shows the slice form.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ShowSliceForm(int rowIndex)
        {
            string formFile = string.Empty;
            string visibleName = string.Empty;
            FormInfo getPermissionForm = new FormInfo();
            bool isSlice;
            PermissionFields permissions;
            SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

            getFormDetailsDataDetails = this.form9612Control.WorkItem.GetFormDetails(Convert.ToInt32(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.FormNumber.Name].Value.ToString().Trim()), Convert.ToInt32(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.UserId.Name].Value.ToString().Trim()));
            if (getFormDetailsDataDetails.Rows.Count > 0)
            {
                permissions.newPermission = Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
                getPermissionForm.addPermission = Convert.ToInt32(Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName]));

                permissions.openPermission = Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
                getPermissionForm.openPermission = Convert.ToInt32(Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName]));

                permissions.editPermission = Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
                getPermissionForm.editPermission = Convert.ToInt32(Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName]));

                permissions.deletePermission = Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                getPermissionForm.deletePermission = Convert.ToInt32(Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName]));

                formFile = getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.FormFileColumn.ColumnName].ToString();
                getPermissionForm.formFile = formFile;

                visibleName = getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.MenuNameColumn.ColumnName].ToString();
                getPermissionForm.visibleName = visibleName;
                getPermissionForm.form = Convert.ToInt32(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.FormNumber.Name].Value.ToString().Trim());

                isSlice = Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsSliceColumn.ColumnName].ToString());

                if (!isSlice)
                {
                    if (permissions.openPermission && Convert.ToBoolean(getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionMenuColumn.ColumnName].ToString()))
                    {
                        getPermissionForm.optionalParameters = new object[6];

                        if (!string.IsNullOrEmpty(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter1.Name].Value.ToString().Trim()))
                        {
                            getPermissionForm.optionalParameters[0] = this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter1.Name].Value.ToString().Trim();

                            if (!string.IsNullOrEmpty(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter2.Name].Value.ToString().Trim()))
                            {
                                getPermissionForm.optionalParameters[1] = this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter2.Name].Value.ToString().Trim();
                            }

                            if (!string.IsNullOrEmpty(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter3.Name].Value.ToString().Trim()))
                            {
                                getPermissionForm.optionalParameters[2] = this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter3.Name].Value.ToString().Trim();
                            }

                            if (!string.IsNullOrEmpty(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter4.Name].Value.ToString().Trim()))
                            {
                                getPermissionForm.optionalParameters[3] = this.RecordHistoryGridView.Rows[rowIndex].Cells[this.Parameter4.Name].Value.ToString().Trim();
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        getPermissionForm.optionalParameters[4] = permissions;
                        getPermissionForm.optionalParameters[5] = this.RecordHistoryGridView.Rows[rowIndex].Cells[this.UserId.Name].Value.ToString().Trim();

                        TerraScanCommon.SupportFormUserId = Convert.ToInt32(this.RecordHistoryGridView.Rows[rowIndex].Cells[this.UserId.Name].Value.ToString().Trim());
                        this.Close();
                        this.ShowForm(this, new DataEventArgs<FormInfo>(getPermissionForm));
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("PermissionCheck"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Slice Form cannot be opened", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Forms the master_ reduce flicker.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        private void FormMaster_FormVisibility(bool isVisible)
        {
            EnablePanelEventArgs visibleInfo;
            visibleInfo.IsSlice = false;
            visibleInfo.IsVisible = isVisible;
            this.OnFormMaster_VisibleForms(this, new DataEventArgs<EnablePanelEventArgs>(visibleInfo));
        }

        #endregion Methods
    }
}
