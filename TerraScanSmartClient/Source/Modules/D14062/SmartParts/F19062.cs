
namespace D14062
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Resources;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using System.Globalization;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;
    using System.IO;
     

    /// <summary>
    /// F19062 Form
    /// </summary>
    [SmartPart]
    public partial class F19062 : BaseSmartPart
    {
        #region Variables

        private string ownerIds = string.Empty;
        private string statementIds = string.Empty;
        private string parcelIds = string.Empty;
        private string scheduleIds = string.Empty;
        private string stateIds = string.Empty;
        private bool IsOwnerConfigured = false;
        ArrayList romoveitemIds = new ArrayList();
        ArrayList redRecordList = new ArrayList();
        /// <summary>
        /// form15018Control Controller
        /// </summary>
        private F19062Controller form19062Control;
        F14062StatementPullListData GridResultDataSet = new F14062StatementPullListData();

        DataTable removeTable = new DataTable();
        DataTable redListTable = new DataTable();
        DataTable tempTable = new DataTable();
        DataTable tempRedTable = new DataTable();
        F14062StatementPullListData.f14062_pcget_StatementPullListDataTable PullListDataTable = new F14062StatementPullListData.f14062_pcget_StatementPullListDataTable();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="F19062"/> class.
        /// </summary>
        public F19062()
        {
            InitializeComponent();

            this.ResultGridPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ResultGridPictureBox.Height, this.ResultGridPictureBox.Width, "Grid Results", 175, 150, 96);
            this.PullListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PullListPictureBox.Height, this.PullListPictureBox.Width, "Current Pull List", 175, 150, 96);
            this.CutomizeResultGrid();
            this.ResultGrid.AutoGenerateColumns = false;
            this.ResultGridPictureBox.SendToBack();
            this.CutamizePullListGrid();
            // this.PullListGridDetails(); 
            this.PullListPictureBox.SendToBack();
        }

        /// <summary>
        /// Handles the Load event of the F19062 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F19062_Load(object sender, EventArgs e)
        {
            this.CutomizeResultGrid();
            this.CutamizePullListGrid();
            this.LoadGridResultDetails();
            this.LoadWorkSpaces();
            this.PullListGridDetails();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F15018 control.
        /// </summary>
        /// <value>The F15018 control.</value>
        [CreateNew]
        public F19062Controller Form19062Control
        {
            get { return this.form19062Control as F19062Controller; }
            set { this.form19062Control = value; }
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion

        #region CustomizeGrids
        /// <summary>
        /// Cutamizes the pull list grid.
        /// </summary>
        private void CutamizePullListGrid()
        {
            UltraGridBand currentBand = this.BottomGrid.DisplayLayout.Bands[0];

            currentBand.Override.RowSelectors = DefaultableBoolean.True;

            this.BottomGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;
            this.BottomGrid.DisplayLayout.TabNavigation = TabNavigation.NextCell;

            if (this.BottomGrid.Rows.Count < 10)
            {
                ////to assgin empty row at the end of the gird
                this.BottomGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.BottomGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
            else
            {
                this.BottomGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
            }
            this.BottomGrid.DataSource = this.PullListDataTable.DefaultView;
        }

        /// <summary>
        /// Cutomizes the result grid.
        /// </summary>
        private void CutomizeResultGrid()
        {
            this.ResultGrid.AutoGenerateColumns = false;
            this.ResultGrid.Columns["IsRed"].Visible = false;
            this.ResultGrid.Columns["PostTypeID"].Visible = false;
            this.ResultGrid.Columns["SourceID"].Visible = false;
            this.ResultGrid.Columns["OwnerID"].Visible = false;
            this.ResultGrid.Columns["TypeID"].Visible = false;
            this.ResultGrid.Columns["StatusID"].Visible = false;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.IsCheckedColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.IsCheckedColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.StatementNumberColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.StatementNumberColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.ParcelNumberColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.ParcelNumberColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.PostTypeColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.PostTypeColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.PostTypeIDColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.PostTypeIDColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.TaxpayerNameColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.TaxpayerNameColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.OwnerCodeColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.OwnerCodeColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.OwnerIDColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.OwnerIDColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.UISepColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.UISepColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.TypeColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.TypeColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.TypeIDColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.TypeIDColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.StatusColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.StatusColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.StatusIDColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.StatusIDColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.NoteColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.NoteColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.SourceColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.SourceColumn.ColumnName;
            this.ResultGrid.Columns[this.GridResultDataSet.f14062_GridResultDetailsTable.SourceIDColumn.ColumnName].DataPropertyName = this.GridResultDataSet.f14062_GridResultDetailsTable.SourceIDColumn.ColumnName;
                      

            this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
            this.ResultGrid.ScrollBars = ScrollBars.Both;
        }
        #endregion

        #region Button Events
        /// <summary>
        /// Handles the Click event of the OwnerButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerButton_Click(object sender, EventArgs e)
        {
            try
            {
                string currentOwnerIds = string.Empty;
                Form parcelF9110 = new Form();
                object[] optionalParameter = new object[] { 91000 };
                parcelF9110 = TerraScanCommon.GetForm(9110, null, this.form19062Control.WorkItem);

                if (parcelF9110 != null)
                {
                    if (parcelF9110.ShowDialog() == DialogResult.Yes)
                    {
                        currentOwnerIds = TerraScanCommon.GetValue(parcelF9110, "CommandResult");
                        currentOwnerIds = currentOwnerIds.Replace("OwnerId", "OwnerID");
                        DataSet currentownerDataTable = new DataSet();
                        currentownerDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentOwnerIds));
                        if (!string.IsNullOrEmpty(currentOwnerIds))
                        {
                            this.ownerIds = currentOwnerIds;
                            DataTable dt = new DataTable();
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                dt = this.GridResultDataSet.f14062_GridResultDetailsTable.Copy();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        dt.Rows[i].Delete();
                                    }
                                }
                                dt.AcceptChanges();
                            }
                            if (!string.IsNullOrEmpty(this.ownerIds))
                            {
                                this.GridResultDataSet = this.form19062Control.WorkItem.F14062_GridResultDetails(ownerIds, null, null, null, null, Convert.ToInt32(TerraScanCommon.UserId));//(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, Convert.ToInt32(TerraScanCommon.UserId));
                            }
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"] = true;
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            else
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = false;
                                    }

                                }
                            }
                        }

                    }
                }
                this.EnableFunctionalButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the StatementButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StatementButton_Click(object sender, EventArgs e)
        {
            try
            {
                string currentStatementIds = string.Empty;
                Form parcelF1403 = new Form();
                parcelF1403 = TerraScanCommon.GetForm(1411, null, this.form19062Control.WorkItem);

                if (parcelF1403 != null)
                {
                    if (parcelF1403.ShowDialog() == DialogResult.OK)
                    {

                        currentStatementIds = TerraScanCommon.GetValue(parcelF1403, "CommandResult");
                        DataSet currentParcelsDataTable = new DataSet();
                        currentParcelsDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentStatementIds));
                        if (!string.IsNullOrEmpty(currentStatementIds))
                        {
                            this.statementIds = currentStatementIds;
                            DataTable dt = new DataTable();
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                dt = this.GridResultDataSet.f14062_GridResultDetailsTable.Copy();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        dt.Rows[i].Delete();
                                    }
                                }
                                dt.AcceptChanges();
                            }
                            if (!string.IsNullOrEmpty(this.statementIds))
                            {
                                this.GridResultDataSet = this.form19062Control.WorkItem.F14062_GridResultDetails(null, statementIds, null, null, null, Convert.ToInt32(TerraScanCommon.UserId));//(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, Convert.ToInt32(TerraScanCommon.UserId));
                            }
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"] = true;
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            else
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = false;
                                    }
                                }
                            }
                        }
                    }
                }
                this.EnableFunctionalButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ParcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                string currentParcelIds = string.Empty;
                Form parcelF1403 = new Form();
                parcelF1403 = TerraScanCommon.GetForm(1403, null, this.form19062Control.WorkItem);

                if (parcelF1403 != null)
                {
                    if (parcelF1403.ShowDialog() == DialogResult.OK)
                    {

                        currentParcelIds = TerraScanCommon.GetValue(parcelF1403, "CommandResult");
                        DataSet currentParcelsDataTable = new DataSet();
                        currentParcelsDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentParcelIds));
                        if (!string.IsNullOrEmpty(currentParcelIds))
                        {
                            this.parcelIds = currentParcelIds;
                            DataTable dt = new DataTable();
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                dt = this.GridResultDataSet.f14062_GridResultDetailsTable.Copy();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        dt.Rows[i].Delete();
                                    }
                                }
                                dt.AcceptChanges();
                            }
                            if (!string.IsNullOrEmpty(this.parcelIds))
                            {
                                this.GridResultDataSet = this.form19062Control.WorkItem.F14062_GridResultDetails(null, null, parcelIds, null, null, Convert.ToInt32(TerraScanCommon.UserId));//(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, Convert.ToInt32(TerraScanCommon.UserId));
                            }
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"] = true;
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            else
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = false;
                                    }

                                }
                            }
                        }
                    }
                }
                this.EnableFunctionalButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ScheduleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleButton_Click(object sender, EventArgs e)
        {
            try
            {
                string currentScheduleIds = string.Empty;
                Form parcelF1404 = new Form();
                parcelF1404 = TerraScanCommon.GetForm(1404, null, this.form19062Control.WorkItem);

                if (parcelF1404 != null)
                {
                    if (parcelF1404.ShowDialog() == DialogResult.OK)
                    {

                        currentScheduleIds = TerraScanCommon.GetValue(parcelF1404, "CommandResult");
                        DataSet currentParcelsDataTable = new DataSet();
                        currentParcelsDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentScheduleIds));
                        if (!string.IsNullOrEmpty(currentScheduleIds))
                        {
                            this.scheduleIds = currentScheduleIds;
                            DataTable dt = new DataTable();
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                dt = this.GridResultDataSet.f14062_GridResultDetailsTable.Copy();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        dt.Rows[i].Delete();
                                    }
                                }
                                dt.AcceptChanges();
                            }
                            this.GridResultDataSet = this.form19062Control.WorkItem.F14062_GridResultDetails(null, null, null, scheduleIds, null, Convert.ToInt32(TerraScanCommon.UserId));//(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, Convert.ToInt32(TerraScanCommon.UserId));
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"] = true;
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            else
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = false;
                                    }

                                }
                            }
                        }
                    }
                }
                this.EnableFunctionalButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the StateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StateButton_Click(object sender, EventArgs e)
        {

            try
            {
                string currentStateIds = string.Empty;
                Form parcelF1405 = new Form();
                parcelF1405 = TerraScanCommon.GetForm(1405, null, this.form19062Control.WorkItem);

                if (parcelF1405 != null)
                {
                    if (parcelF1405.ShowDialog() == DialogResult.OK)
                    {

                        currentStateIds = TerraScanCommon.GetValue(parcelF1405, "CommandResult");
                        DataSet currentParcelsDataTable = new DataSet();
                        currentParcelsDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentStateIds));
                        if (!string.IsNullOrEmpty(currentStateIds))
                        {
                            this.stateIds = currentStateIds;
                            DataTable dt = new DataTable();
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                dt = this.GridResultDataSet.f14062_GridResultDetailsTable.Copy();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        dt.Rows[i].Delete();
                                    }
                                }
                                dt.AcceptChanges();
                            }
                            if (!string.IsNullOrEmpty(stateIds))
                            {
                                this.GridResultDataSet = this.form19062Control.WorkItem.F14062_GridResultDetails(null, null, null, null, stateIds, Convert.ToInt32(TerraScanCommon.UserId));//(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, Convert.ToInt32(TerraScanCommon.UserId));
                            }
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"] = true;
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            else
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    this.GridResultDataSet.f14062_GridResultDetailsTable.Merge(dt, true);
                                }
                            }
                            this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                                {
                                    if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.ResultGrid.Rows[i].ReadOnly = false;
                                    }

                                }
                            }
                        }
                    }
                }
                this.EnableFunctionalButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Removes the XML item.
        /// </summary>
        /// <param name="removeListIds">The remove list ids.</param>
        /// <returns></returns>
        private string RemoveXMLItem(ArrayList removeListIds)
        {
            DataTable dt = new DataTable("Table");
            dt.Columns.Add("ParcelNumber", typeof(String));
            foreach (var r in removeListIds)
            {
                dt.Rows.Add(r);
            }
            dt = dt.DefaultView.ToTable("ParcelTable");

            dt.AcceptChanges();
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string removexmlstr;
            removexmlstr = sr.ReadToEnd();
            removexmlstr = removexmlstr.Replace("DocumentElement", "Root");
            removexmlstr = removexmlstr.Replace("ParcelTable", "Table");
            return (removexmlstr);
        }

        private string RemoveXMLTableItem(DataTable removeListIds)
        {
            DataTable dt = new DataTable("Table");
            dt = removeListIds.Copy();
           // removeListIds.Copy(dt);
            //dt.Columns.Add("ParcelNumber", typeof(String));
            //foreach (var r in removeListIds)
            //{
            //    dt.Rows.Add(r);
            //}
            dt = dt.DefaultView.ToTable("ParcelTable");

            dt.AcceptChanges();
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string removexmlstr;
            removexmlstr = sr.ReadToEnd();
            removexmlstr = removexmlstr.Replace("DocumentElement", "Root");
            removexmlstr = removexmlstr.Replace("ParcelTable", "Table");
            return (removexmlstr);
        }


        /// <summary>
        /// Handles the RowPrePaint event of the ResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowPrePaintEventArgs"/> instance containing the event data.</param>
        private void ResultGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                {
                    DataGridView grd = sender as DataGridView;
                    if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[e.RowIndex]["IsRed"].ToString().ToLower().Equals("true"))
                    {
                        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        grd.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }

                }
            }
            catch (Exception ex)
            {
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Handles the Click event of the RemoveToButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveToButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ResultGrid.Rows.Count > 0 && this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                {
                    if (tempTable.Columns.Count == 0)
                    {
                        tempTable.Columns.Add("ParcelNumber", typeof(string));
                        tempTable.Columns.Add("StatementNumber", typeof(string));
                    }
                    if (tempRedTable.Columns.Count == 0)
                    {
                        tempRedTable.Columns.Add("ParcelNumber", typeof(string));
                        tempRedTable.Columns.Add("StatementNumber", typeof(string));
                    }
                    this.redListTable = this.GridResultDataSet.f14062_GridResultDetailsTable.Clone();
                    this.romoveitemIds.Clear();
                    this.redRecordList.Clear();
                    this.redListTable.Clear();
                    this.tempTable.Clear();
                    this.tempRedTable.Clear();
                    for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"].ToString()))
                        {
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"].ToString().ToLower().Equals("true")) //&& this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsRed"].ToString().ToLower().Equals("true")
                            {
                                tempTable.Rows.Add(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["ParcelNumber"].ToString(), this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["StatementNumber"].ToString());
                                romoveitemIds.Add(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["ParcelNumber"].ToString());
                                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsRed"].ToString().ToLower().Equals("true"))
                                {
                                   this.redListTable.ImportRow(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]);
                                   tempRedTable.Rows.Add(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["ParcelNumber"].ToString(), this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["StatementNumber"].ToString());
                                   redRecordList.Add(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["ParcelNumber"].ToString());
                                }
                            }

                        }
                    }
                    // this.GridResultDataSet.f14062_GridResultDetailsTable.AcceptChanges();
                    //if (romoveitemIds.Count > 0)
                    if (tempTable.Rows.Count > 0)
                    {
                        //string returnXML = RemoveXMLItem(romoveitemIds);
                        string returnXML = RemoveXMLTableItem(tempTable);
                        bool isProcess = false;
                        string message = this.form19062Control.WorkItem.F14062_DeleteStatementPullList(returnXML, Convert.ToInt32(TerraScanCommon.UserId), isProcess);
                        if (!string.IsNullOrEmpty(message) && (message.ToLower() != "no message"))
                        {
                            DialogResult check = MessageBox.Show(message, "TerraScan T2", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (check.Equals(DialogResult.Yes))
                            {
                                ////Commented by purushotham
                               // string pullListItems = RemoveXMLItem(redRecordList);

                                string pullListItems = RemoveXMLTableItem(tempRedTable);
                                //this.form19062Control.WorkItem.F14062_SaveGridDetails(returnXML, Convert.ToInt32(TerraScanCommon.UserId));
                                isProcess = true;
                                if (!pullListItems.Equals("<Root />"))
                                {
                                    string tempMessage = this.form19062Control.WorkItem.F14062_DeleteStatementPullList(pullListItems, Convert.ToInt32(TerraScanCommon.UserId), isProcess);

                                    for (int j = 0; j < redListTable.Rows.Count; j++)
                                    {
                                        DataRow[] rows;
                                        if (string.IsNullOrEmpty(this.redListTable.Rows[j]["StatementNumber"].ToString()))
                                        {
                                            rows = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber = '" + this.redListTable.Rows[j]["ParcelNumber"].ToString() + "'"); // UserName is Column Name
                                            foreach (DataRow r in rows)
                                                r.Delete();
                                        }
                                        else
                                        {                                            
                                            rows = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber= '" + this.redListTable.Rows[j]["ParcelNumber"].ToString() + "' AND StatementNumber = '" + this.redListTable.Rows[j]["StatementNumber"].ToString() + "'");//this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber = '" + redRecordList[j].ToString() + "'"); // UserName is Column Name
                                            foreach (DataRow r in rows)
                                                r.Delete();
                                        }

                                    }                                   
                                }
                                this.GridResultDataSet.f14062_GridResultDetailsTable.AcceptChanges();
                                this.romoveitemIds.Clear();
                                this.redRecordList.Clear();
                            }
                        }
                        else
                        {
                            //for (int j = 0; j < redRecordList.Count; j++)
                            for (int j = 0; j < redListTable.Rows.Count; j++)
                            {
                                DataRow[] rows;
                                if (string.IsNullOrEmpty(this.redListTable.Rows[j]["StatementNumber"].ToString()))
                                {
                                    rows = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber = '" + this.redListTable.Rows[j]["ParcelNumber"].ToString() + "'"); // UserName is Column Name
                                    foreach (DataRow r in rows)
                                        r.Delete();
                                }
                                else
                                {
                                    rows = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber= '" + this.redListTable.Rows[j]["ParcelNumber"].ToString() + "' AND StatementNumber = '" + this.redListTable.Rows[j]["StatementNumber"].ToString() + "'");//this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber = '" + redRecordList[j].ToString() + "'"); // UserName is Column Name
                                    foreach (DataRow r in rows)
                                        r.Delete();
                                }

                            }
                            this.GridResultDataSet.f14062_GridResultDetailsTable.AcceptChanges();
                            this.romoveitemIds.Clear();
                            this.redRecordList.Clear();
                            this.redListTable.Clear();

                        }
                    }

                    this.GridResultDataSet.f14062_GridResultDetailsTable.AcceptChanges();
                    this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                    this.PullListGridDetails();
                }

                this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                    {
                        if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                        {
                            this.ResultGrid.Rows[i].ReadOnly = true;
                        }
                        else
                        {
                            this.ResultGrid.Rows[i].ReadOnly = false;
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
        /// Handles the Click event of the AddToButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddToButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveGridDetails();
                this.PullListGridDetails();
                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                    {
                        if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                        {
                            this.ResultGrid.Rows[i].ReadOnly = true;
                        }
                        else
                        {
                            this.ResultGrid.Rows[i].ReadOnly = false;
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
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.GridResultDataSet.f14062_GridResultDetailsTable.Clear();
                this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                this.ownerIds = string.Empty;
                this.stateIds = string.Empty;
                this.statementIds = string.Empty;
                this.parcelIds = string.Empty;
                this.scheduleIds = string.Empty;
                this.PullListGridDetails();
                this.EnableFunctionalButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Enables the functional buttons.
        /// </summary>
        private void EnableFunctionalButtons()
        {
            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
            {
                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[0]["EmptyRecord$"].ToString().ToLower().Equals("false"))
                {
                    this.AddToButton.Enabled = true;
                    this.RemoveToButton.Enabled = true;
                }
                else
                {
                    this.AddToButton.Enabled = false;
                    this.RemoveToButton.Enabled = false;
                }
            }
            else
            {
                this.AddToButton.Enabled = false;
                this.RemoveToButton.Enabled = false;
            }

        }



        /// <summary>
        /// Loads the grid result details.
        /// </summary>
        private void LoadGridResultDetails()
        {
            try
            {
                this.GridResultDataSet = this.form19062Control.WorkItem.F14062_GridResultDetails(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, TerraScanCommon.UserId);
                if (this.GridResultDataSet.f14062_GetConfigDetailTable.Rows.Count > 0)
                {
                    Boolean.TryParse(this.GridResultDataSet.f14062_GetConfigDetailTable.Rows[0][0].ToString(), out IsOwnerConfigured);
                    //this.IsOwnerConfigured = Convert.ToBoolean(this.GridResultDataSet.f14062_GetConfigDetailTable.Rows[0][0].ToString());
                    if (this.IsOwnerConfigured)
                    {
                        this.StatementButton.Enabled = false;
                        this.ParcelButton.Enabled = false;
                        this.StateButton.Enabled = false;
                        this.ScheduleButton.Enabled = false;
                    }
                    else
                    {
                        this.StatementButton.Enabled = true;
                        this.ParcelButton.Enabled = true;
                        this.StateButton.Enabled = true;
                        this.ScheduleButton.Enabled = true;
                    }
                }

                this.EnableFunctionalButtons();
                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                {
                    this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Pulls the list grid details.
        /// </summary>
        private void PullListGridDetails()
        {
            this.PullListDataTable = this.form19062Control.WorkItem.F14062_GetStatementPullListDetails().f14062_pcget_StatementPullList;
            this.BottomGrid.DataSource = this.PullListDataTable.DefaultView;
            this.BottomGrid.DataBind();
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            try
            {
                //////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
                if (this.form19062Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.FormHeaderDeckWorkspace.Show(this.form19062Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.FormHeaderDeckWorkspace.Show(this.form19062Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                ////sets form header
                this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Statement Pull List", string.Empty }));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Parcels the ids list.
        /// </summary>
        private void ParcelIdsList()
        {
            try
            {
                if (this.ResultGrid.Rows.Count > 0 && this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                {
                    this.removeTable = this.GridResultDataSet.f14062_GridResultDetailsTable.Clone();
                    this.romoveitemIds.Clear();
                    this.removeTable.Clear();
                    this.GridResultDataSet.f14062_GridResultDetailsTable.AcceptChanges();
                    for (int i = 0; i < this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"].ToString()))
                        {
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["IsChecked"].ToString().ToLower().Equals("true"))
                            {
                                this.removeTable.ImportRow(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]);
                                romoveitemIds.Add(this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[i]["ParcelNumber"].ToString());
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
        /// Saves the grid details.
        /// </summary>
        private void SaveGridDetails()
        {
            try
            {
                F14062StatementPullListData updateGridDetails = new F14062StatementPullListData();
                this.ParcelIdsList();
                for (int i = 0; i < removeTable.Rows.Count; i++)
                {
                    F14062StatementPullListData.f14062_SaveGridDetailstableRow dr = updateGridDetails.f14062_SaveGridDetailstable.Newf14062_SaveGridDetailstableRow();
                   
                    if (string.IsNullOrEmpty(this.removeTable.Rows[i]["StatementNumber"].ToString()))
                    {
                        DataRow[] statementRow = this.GridResultDataSet.f14062_GridResultDetailsTable.Select(String.Format("ParcelNumber='{0}'", romoveitemIds[i].ToString()));//this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber= '" + this.removeTable.Rows[i]["ParcelNumber"].ToString() + "' AND StatementNumber = '" + this.removeTable.Rows[i]["StatementNumber"].ToString() + "'"); //+ "' AND Model Details = '" + modelDetails)//this.GridResultDataSet.f14062_GridResultDetailsTable.Select((String.Format("ParcelNumber='{0}'", this.removeTable.Rows[i]["ParcelNumber"].ToString()) + String.Format("StatementNumber='{0}'", this.removeTable.Rows[i]["StatementNumber"].ToString()))); //+ "' AND Model Details = '" + modelDetails)
                        dr.ParcelNumber = statementRow[0]["ParcelNumber"].ToString();
                        dr.StatementNumber = statementRow[0]["StatementNumber"].ToString();
                        dr.PostTypeID = statementRow[0]["PostTypeID"].ToString();
                        dr.SourceID = statementRow[0]["SourceID"].ToString();
                        dr.OwnerID = statementRow[0]["OwnerID"].ToString();
                        dr.TypeID = statementRow[0]["TypeID"].ToString();
                        dr.TaxpayerName = statementRow[0]["TaxpayerName"].ToString();
                        dr.StatusID = statementRow[0]["StatusID"].ToString();
                        dr.Note = statementRow[0]["Note"].ToString();
                        //this.removeTable.Rows[i]["StatementNumber"] = "0";
                    }
                    else
                    {
                        DataRow[] statementRow = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber= '" + this.removeTable.Rows[i]["ParcelNumber"].ToString() + "' AND StatementNumber = '" + this.removeTable.Rows[i]["StatementNumber"].ToString() + "'"); //+ "' AND Model Details = '" + modelDetails)//this.GridResultDataSet.f14062_GridResultDetailsTable.Select((String.Format("ParcelNumber='{0}'", this.removeTable.Rows[i]["ParcelNumber"].ToString()) + String.Format("StatementNumber='{0}'", this.removeTable.Rows[i]["StatementNumber"].ToString()))); //+ "' AND Model Details = '" + modelDetails)
                        dr.ParcelNumber = statementRow[0]["ParcelNumber"].ToString();
                        dr.StatementNumber = statementRow[0]["StatementNumber"].ToString();
                        dr.PostTypeID = statementRow[0]["PostTypeID"].ToString();
                        dr.SourceID = statementRow[0]["SourceID"].ToString();
                        dr.OwnerID = statementRow[0]["OwnerID"].ToString();
                        dr.TypeID = statementRow[0]["TypeID"].ToString();
                        dr.TaxpayerName = statementRow[0]["TaxpayerName"].ToString();
                        dr.StatusID = statementRow[0]["StatusID"].ToString();
                        dr.Note = statementRow[0]["Note"].ToString();
                    }
                    this.removeTable.AcceptChanges();                   
                   
                    updateGridDetails.f14062_SaveGridDetailstable.Rows.Add(dr);
                }

                updateGridDetails.f14062_SaveGridDetailstable.AcceptChanges();
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(updateGridDetails.f14062_SaveGridDetailstable.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                string xmlContent = tempDataSet.GetXml();

                Form scheduleSelectionForm = new Form();
                object[] optionalParameter = new object[] { tempDataSet };
                scheduleSelectionForm = this.form19062Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1407, optionalParameter, this.form19062Control.WorkItem);
                if (scheduleSelectionForm != null)
                {
                    if (scheduleSelectionForm.ShowDialog() == DialogResult.OK)
                    {

                        for (int j = 0; j < removeTable.Rows.Count; j++)
                        {
                            DataRow[] rows;
                            if (string.IsNullOrEmpty(this.removeTable.Rows[j]["StatementNumber"].ToString()))
                            {
                                rows = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber = '" + this.removeTable.Rows[j]["ParcelNumber"].ToString() + "'"); // UserName is Column Name
                                foreach (DataRow r in rows)
                                    r.Delete();
                            }
                            else
                            {
                                rows = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber= '" + this.removeTable.Rows[j]["ParcelNumber"].ToString() + "' AND StatementNumber = '" + this.removeTable.Rows[j]["StatementNumber"].ToString() + "'");//this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber = '" + redRecordList[j].ToString() + "'"); // UserName is Column Name
                                foreach (DataRow r in rows)
                                    r.Delete();
                            }

                        }
                        //for (int j = 0; j < romoveitemIds.Count; j++)
                        //{
                        //    DataRow[] rows;
                        //    rows = this.GridResultDataSet.f14062_GridResultDetailsTable.Select("ParcelNumber = '" + romoveitemIds[j].ToString() + "'"); // UserName is Column Name
                        //    foreach (DataRow r in rows)
                        //        r.Delete();                          

                        //}
                        this.GridResultDataSet.f14062_GridResultDetailsTable.AcceptChanges();
                        this.ResultGrid.DataSource = this.GridResultDataSet.f14062_GridResultDetailsTable.DefaultView;
                        this.romoveitemIds.Clear();

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Grid Events
        private void ResultGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.IsOwnerConfigured)
                {
                    if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                    {
                        if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(0))
                        {
                            if (this.ResultGrid.CurrentColumnIndex.Equals(0))
                            {
                                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[e.RowIndex]["EmptyRecord$"].ToString().ToLower().Equals("false"))
                                {
                                    this.ResultGrid.Rows[e.RowIndex].ReadOnly = true;
                                }
                            }
                        }
                    }
                }

                if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
                {
                    if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(0))
                    {
                        if (this.ResultGrid.CurrentColumnIndex.Equals(0))
                        {
                            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows[e.RowIndex]["EmptyRecord$"].ToString().ToLower().Equals("true"))
                            {
                                this.ResultGrid.Rows[e.RowIndex].ReadOnly = true;
                            }
                        }
                    }
                }
                else
                {
                    if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(0))
                    {
                        if (this.ResultGrid.CurrentColumnIndex.Equals(0))
                        {
                            this.ResultGrid.Rows[e.RowIndex].ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ResultGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.EnableButton();
        }

        private void ResultGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.IsOwnerConfigured && this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
            {               
                ResultGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void EnableButton()
        {
            bool isRowChecked = false;

            if (this.GridResultDataSet.f14062_GridResultDetailsTable.Rows.Count > 0)
            {
                var index = this.ResultGrid.Rows.Count;

                for (int i = 0; i < index; i++)
                {
                    if (this.ResultGrid.Rows[i].Cells["IsChecked"].Value.ToString().ToLower().Equals("true"))
                    {
                        isRowChecked = true;
                    }
                }
            }

            if (isRowChecked)
            {
                this.AddToButton.Enabled = true;
                this.RemoveToButton.Enabled = true;
            }
            else
            {
                this.AddToButton.Enabled = false;
                this.RemoveToButton.Enabled = false;
            }

        }

        private void ResultGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.EnableButton();
        }

        #endregion
      

    }   
} 

