//--------------------------------------------------------------------------------------------
// <copyright file="F1201.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for posting History and Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Krishna Abburi        Created
//*********************************************************************************/

namespace D1200
{
    using System;
    using System.Collections;
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
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Form F1201
    /// </summary>
    [SmartPart]
    public partial class F1201 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// form1201Control Variable
        /// </summary>
        private F1201Controller form1201Control;

        /// <summary>
        /// postingHistoryDataset Variable contains Posting History detauils
        /// </summary>
        private PostingHistoryData postingHistoryDataset = new PostingHistoryData();

        /// <summary>
        /// additionalOperationSmartPart Variable
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// postID Variable
        /// </summary>
        private int postID;

        /// <summary>
        /// listPostingHistory Variable contains Posting History detauils
        /// </summary>
        private PostingHistoryData.ListPostingHistoryDataTable listPostingHistory = new PostingHistoryData.ListPostingHistoryDataTable();

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// selectedClosedDate Variable
        /// </summary>
        private string selectedRanOnDate;

        ////private bool emptyRecord;

        /// <summary>
        /// selected Variable
        /// </summary>
        private int selected;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1201"/> class.
        /// </summary>
        public F1201()
        {
            InitializeComponent();
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, "Previous Posts to the General Ledger", 28, 81, 128);
        }

        #endregion

        #region Published Events
        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Event Publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion      

        #region Properties

        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        /// <value>The post id.</value>
        public int PostId
        {
            get
            {
                return this.postID;
            }

            set
            {
                this.postID = value;
                this.additionalOperationSmartPart.KeyId = this.postID;
            }
        }

        /// <summary>
        /// Gets or sets the form1201 control.
        /// </summary>
        /// <value>The form1201 control.</value>
        [CreateNew]
        public F1201Controller Form1201Control
        {
            get { return this.form1201Control as F1201Controller; }
            set { this.form1201Control = value; }
        }

        #endregion

        #region FormEvents

        /// <summary>
        /// Handles the Load event of the F1201 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1201_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();

                this.InitGetClosedDate();
                this.InitShowLastcombo();
                this.InitShowOnlycombo();
                this.CustomizeDataGrid();
                this.FillPosthistorygrid();

                if (this.PostingHistoryDataGrid.OriginalRowCount == 0)
                {
                    this.PostingHistoryDataGrid.RemoveDefaultSelection = true;
                }
                else
                {
                    this.PostingHistoryDataGrid.RemoveDefaultSelection = false;
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
        /// Handles the SelectionChangeCommitted event of the ShowOnlyCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ShowOnlyCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.FillPosthistorygrid();
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
        /// Handles the LinkClicked event of the PostingsLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void PostingsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.PostId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.PostId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////int reportAuditId = 0;
                ////this.Cursor = Cursors.WaitCursor;
                ////reportAuditId = this.PostId;
                ////this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("ReportFileID", reportAuditId);
                ////TerraScanCommon.ShowReport(90101, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
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
        /// Handles the LinkClicked event of the CountyConfiglink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void CountyConfiglink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(9020);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReverseGLPostButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReverseGLPostButton_Click(object sender, EventArgs e)
        {
            try
            {
                string closeddate;
                DialogResult result = new DialogResult();
                this.SelectedPostIdDetails(-1);
                closeddate = this.ClosedDateLabel.Text;
                if (Convert.ToDateTime(closeddate) <= Convert.ToDateTime(this.selectedRanOnDate))
                {
                    object[] optionalParameter = new object[] { this.postID };
                    Form reverseGLForm = new Form();
                    reverseGLForm = this.form1201Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1202, optionalParameter, this.Form1201Control.WorkItem);

                    if (reverseGLForm != null)
                    {
                        result = reverseGLForm.ShowDialog();
                        if (result == DialogResult.Yes)
                        {
                            this.FillPosthistorygrid();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("GL Post has been locked"), "TerraScan – GL Post Locked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
        /// Handles the Click event of the ViewGLButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ViewGLButton_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable userTabReport = new Hashtable();
                ////userTabReport.Add("ReportNumber", "120110");-commented WRT change request.
                ////userTabReport.Add("UserID", TerraScanCommon.UserId); ////changed for may change order request
                ////userTabReport.Add("PostID", this.PostId); ////changed for may change order request
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(120110, TerraScan.Common.Reports.Report.ReportType.Preview, userTabReport);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the PostingHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PostingHistoryDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //// Disabling the ReverseGLPostButton when the record has the negative value.

                if (this.listPostingHistory.Rows.Count > 0)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (PostingHistoryDataGrid[7, e.RowIndex].Value.ToString() == "True")
                        {
                            this.ReverseGLPostButton.Enabled = false;
                        }
                        else
                        {
                            this.ReverseGLPostButton.Enabled = true;
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
        /// Handles the RowEnter event of the PostingHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PostingHistoryDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.listPostingHistory.Rows.Count > 0)
                {
                    this.SelectedPostIdDetails(e.RowIndex);
                    this.SetAttachmentCommentsCount();
                    this.PostingsAuditLink.Enabled = true;
                    this.PostingsAuditLink.Text = SharedFunctions.GetResourceString("1201PostIDAuditLink") + " " + this.PostingHistoryDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                    if (PostingHistoryDataGrid[7, e.RowIndex].Value.ToString() == "True")
                    ////if (PostingHistoryDataGrid.Rows[e.RowIndex].Cells["ReversePost"].Value != null && string.Equals(PostingHistoryDataGrid.Rows[e.RowIndex].Cells["ReversePost"].Value.ToString(), bool.TrueString, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.ReverseGLPostButton.Enabled = false;
                    }
                    else
                    {
                        this.ReverseGLPostButton.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the PostingHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void PostingHistoryDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.PostingHistoryDataGrid.Columns["PostIdentifier"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Painting the link color 
                          if the record is negative value then it displys in red color
                          otherwise the link shows blue color.*/

                    if (this.PostingHistoryDataGrid.Rows[e.RowIndex].Selected || this.PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.White;
                        (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        if (PostingHistoryDataGrid.Rows[e.RowIndex].Cells["ReversePost"].Value != null && string.Equals(PostingHistoryDataGrid.Rows[e.RowIndex].Cells["ReversePost"].Value.ToString(), bool.TrueString, StringComparison.CurrentCultureIgnoreCase))
                        {
                            (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.Red;
                            (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.Red;
                        }
                        else
                        {
                            (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.Blue;
                            (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                        }

                        (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                    }

                    if (PostingHistoryDataGrid.Rows[e.RowIndex].Cells["ReversePost"].Value != null && string.Equals(PostingHistoryDataGrid.Rows[e.RowIndex].Cells["ReversePost"].Value.ToString(), bool.TrueString, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (Convert.ToInt32(PostingHistoryDataGrid.Rows[e.RowIndex].Cells["UnPostID"].Value.ToString()) > 0)
                        {
                            (PostingHistoryDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ToolTipText = "UnPostID [" + PostingHistoryDataGrid.Rows[e.RowIndex].Cells["UnPostID"].Value.ToString() + "]";
                        }

                        PostingHistoryDataGrid.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                ////removing the - (Minus) from the Amount and placing the value into brackets
                Decimal outDecimal;

                if (e.ColumnIndex == this.PostingHistoryDataGrid.Columns["Amountposted"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !String.IsNullOrEmpty(this.PostingHistoryDataGrid.Rows[e.RowIndex].Cells["Amountposted"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00";
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ShowLastCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ShowLastCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.FillPosthistorygrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the PostingHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PostingHistoryDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    Hashtable userTabReport = new Hashtable();
                    ////userTabReport.Add("ReportNumber", "120110");
                    userTabReport.Add("PostID", this.PostId);
                    ////changed the parameter type from string to int
                    TerraScanCommon.ShowReport(120110, TerraScan.Common.Reports.Report.ReportType.Preview, userTabReport);
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

        #endregion FormEvents

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1201Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1201Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1201Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form1201Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form1201Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form1201Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = SharedFunctions.GetResourceString("1201GeneralLedgerPostingHistory"); ////Properties.Resources.FormName;
            formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1201Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1201Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.PostingHistoryDataGrid.AllowUserToResizeColumns = false;
            this.PostingHistoryDataGrid.AutoGenerateColumns = false;
            this.PostingHistoryDataGrid.AllowUserToResizeRows = false;
            this.PostingHistoryDataGrid.StandardTab = true;
            this.PostingHistoryDataGrid.EnableBinding = false;

            this.PostingHistoryDataGrid.Columns[0].DataPropertyName = "PostID";
            this.PostingHistoryDataGrid.Columns[1].DataPropertyName = "PostName";
            this.PostingHistoryDataGrid.Columns[2].DataPropertyName = "Name_Display";
            this.PostingHistoryDataGrid.Columns[3].DataPropertyName = "RollYear";
            this.PostingHistoryDataGrid.Columns[4].DataPropertyName = "PostDate";
            this.PostingHistoryDataGrid.Columns[5].DataPropertyName = "RanOn";
            this.PostingHistoryDataGrid.Columns[6].DataPropertyName = "AmountTotal";
            this.PostingHistoryDataGrid.Columns[7].DataPropertyName = "ReversePost";
            this.PostingHistoryDataGrid.Columns[8].DataPropertyName = "UnPostID";
            this.PostingHistoryDataGrid.PrimaryKeyColumnName = "PostID";
        }

        /// <summary>
        /// Enables the buttons.
        /// </summary>
        private void EnableControls()
        {
            this.PostingHistoryDataGrid.Enabled = true;
            this.additionalOperationSmartPart.Enabled = true;
            this.PostingsAuditLink.Enabled = true;
            this.ViewGLButton.Enabled = true;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableControls()
        {
            this.PostingHistoryDataGrid.Enabled = false;
            this.ReverseGLPostButton.Enabled = false;
            this.additionalOperationSmartPart.Enabled = false;
            this.PostingsAuditLink.Enabled = false;
            this.ViewGLButton.Enabled = false;
        }

        /// <summary>
        /// Disables the Vertical scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.listPostingHistory.Rows.Count > 20)
            {
                this.PostingHistoryVScrollBar.Enabled = true;
                this.PostingHistoryVScrollBar.Visible = false;
            }
            else
            {
                this.PostingHistoryVScrollBar.Enabled = false;
                this.PostingHistoryVScrollBar.Visible = true;
                this.PostingHistoryVScrollBar.BringToFront();
            }
        }

        /// <summary>
        /// Inits the show lastcombo.
        /// </summary>
        private void InitShowLastcombo()
        {
            this.ShowLastCombo.Items.Clear();
            this.ShowLastCombo.Items.Insert(0, SharedFunctions.GetResourceString("20 Posts"));
            this.ShowLastCombo.Items.Insert(1, SharedFunctions.GetResourceString("50 Posts"));
            this.ShowLastCombo.Items.Insert(2, SharedFunctions.GetResourceString("100 Posts"));
            this.ShowLastCombo.Items.Insert(3, SharedFunctions.GetResourceString("250 Posts"));
            this.ShowLastCombo.Items.Insert(4, SharedFunctions.GetResourceString("ALL Posts"));
            this.ShowLastCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Inits the show onlycombo.
        /// </summary>
        private void InitShowOnlycombo()
        {
            int form = 0;

            //// Adding custom element to the ShowOnly Combo Box
            
            PostingHistoryData.ListPostTypeDataTable listPostType = new PostingHistoryData.ListPostTypeDataTable();
            DataRow customRow = listPostType.NewRow();
            customRow[this.postingHistoryDataset.ListPostType.PostTypeIDColumn.ColumnName] = "0";
            customRow[this.postingHistoryDataset.ListPostType.PostNameColumn.ColumnName] = SharedFunctions.GetResourceString("All Post Types");
            listPostType.Rows.Add(customRow);
            listPostType.Merge(this.Form1201Control.WorkItem.ListPostTypesData(form));
            this.ShowOnlyCombo.ValueMember = this.postingHistoryDataset.ListPostType.PostTypeIDColumn.ColumnName;
            this.ShowOnlyCombo.DisplayMember = this.postingHistoryDataset.ListPostType.PostNameColumn.ColumnName;
            this.ShowOnlyCombo.DataSource = listPostType;
            this.ShowLastCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Fills the Posthistorygrid.
        /// </summary>
        private void FillPosthistorygrid()
        {
            int count = Convert.ToInt32(ShowLastCombo.SelectedIndex);
            switch (count)
            {
                case 0: count = 20;
                    break;
                case 1: count = 50;
                    break;
                case 2: count = 100;
                    break;
                case 3: count = 250;
                    break;
                default: count = 0;
                    break;
            }

            int postTypeId = Convert.ToInt32(ShowOnlyCombo.SelectedValue);
            this.listPostingHistory = this.Form1201Control.WorkItem.ListPostinghistoryData(count, postTypeId);

            if (this.listPostingHistory.Rows.Count > 0)
            {
                this.EnableControls();
                this.PostingHistoryDataGrid.DataSource = null;
                this.PostingHistoryDataGrid.DataSource = this.listPostingHistory;
                this.PostingHistoryDataGrid.Rows[0].Selected = true;
                this.SelectedPostIdDetails(0);
                this.PostingsAuditLink.Text = SharedFunctions.GetResourceString("1201PostIDAuditLink") + " " + this.PostingHistoryDataGrid.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                //// disabling the controls when no records

                this.DisableControls();
                this.PostingHistoryDataGrid.DataSource = this.listPostingHistory;
                this.PostingHistoryDataGrid.Rows[0].Selected = false;
                this.PostingsAuditLink.Text = "tTR_Postings [PostID]";
            }

            this.DisableVScrollBar();
            this.SetAttachmentCommentsCount();
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

                if (this.postID != -999)
                {
                    this.additionalOperationSmartPart.KeyId = this.PostId;
                    additionalOperationCountEntity.AttachmentCount = this.Form1201Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.postID, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.Form1201Control.WorkItem.GetCommentsCount(this.ParentFormId, this.postID, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Row index</returns>
        ////private int GetRowIndex()
        ////{
        ////    try
        ////    {
        ////        this.Cursor = Cursors.WaitCursor;

        ////        if (this.postingHistoryDataset.ListPostingHistory.Rows.Count > 0)
        ////        {
        ////            if (this.postingHistoryDataset.ListPostingHistory.Count > 0)
        ////            {
        ////                this.selected = this.PostingHistoryDataGrid.SelectedRows[0].Index;
        ////            }
        ////            else if (this.PostingHistoryDataGrid.SelectedCells.Count > 0)
        ////            {
        ////                this.selected = this.PostingHistoryDataGrid.CurrentCell.RowIndex;
        ////            }

        ////            return this.selected;
        ////        }
        ////        else
        ////        {
        ////            return 0;
        ////        }
        ////    }
        ////    catch (Exception)
        ////    {
        ////        return 0;
        ////    }
        ////}

        /// <summary>
        /// Selecteds the post id details.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SelectedPostIdDetails(int rowId)
        {
            if (rowId < 0)
            {
                if (this.PostingHistoryDataGrid.CurrentCell != null)
                {
                    rowId = this.PostingHistoryDataGrid.CurrentCell.RowIndex;
                }
            }

            if (rowId >= 0 && rowId < this.PostingHistoryDataGrid.OriginalRowCount)
            {
                if (!string.IsNullOrEmpty(this.PostingHistoryDataGrid.Rows[rowId].Cells["PostIdentifier"].Value.ToString().Trim()))
                {
                    this.PostId = Convert.ToInt32(this.PostingHistoryDataGrid.Rows[rowId].Cells["PostIdentifier"].Value.ToString());
                    this.selectedRanOnDate = this.PostingHistoryDataGrid.Rows[rowId].Cells["RanOn"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Inits the get closed date.
        /// </summary>
        private void InitGetClosedDate()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.Form1201Control.WorkItem.GetConfigDetails("TR_ClosingDate");
            this.ClosedDateLabel.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the PostingHistoryDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void PostingHistoryDataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.PostingHistoryDataGrid.OriginalRowCount > 0)
            {
                this.PostingHistoryDataGrid.Rows[0].Selected = true;
                this.PostingHistoryDataGrid.CurrentCell = this.PostingHistoryDataGrid[0, 0];
                this.PostingHistoryDataGrid.Focus();
            }
        }

        #endregion
    }
}

