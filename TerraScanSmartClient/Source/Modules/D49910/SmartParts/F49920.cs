//--------------------------------------------------------------------------------------------
// <copyright file="F49920.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49920 Search Form .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 01/02/2008       R.Malliga             Created
//***********************************************************************************************/

namespace D49910
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
    using Infragistics.Win.UltraWinGrid;
    using Infrastructure.Interface;

    /// <summary>
    /// F49920 Class File
    /// </summary>
    public partial class F49920 : BaseSmartPart
    {
        #region Variables


        private bool loadSearchGrid = false;

        /// <summary>
        /// form49920Controller
        /// </summary>
        private F49920Controller form49920Control;

         ////<summary>
         ////flagLoadOnProcess
         ////</summary>
        private bool flagLoadOnProcess;
        /// <summary>
        ///LoadInstSearchEngineDataSet
        /// </summary>
        private F49920InstrumentSearchEngineData loadInstSearchEngineDataSet = new F49920InstrumentSearchEngineData();

        /// <summary>
        ///instLoadItemTable
        /// </summary>
        private F49920InstrumentSearchEngineData.InstrumentLoadItemsDataTable instLoadItemTable = new F49920InstrumentSearchEngineData.InstrumentLoadItemsDataTable();

        /// <summary>
        ///InstrumentSearchEngineResultsDataTable
        /// </summary>
        private F49920InstrumentSearchEngineData.ListInstrumentDataTable instrumentSearchEngineResultsDataTable = new F49920InstrumentSearchEngineData.ListInstrumentDataTable();

        /////<summary>
        /////page mode edit
        /////</summary>
        //private TerraScanCommon.PageModeTypes pageMode;
        
        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] formLabelInfo = new string[2];

        private bool clearflag;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F49920"/> class.
        /// </summary>
        public F49920()
        {
            InitializeComponent();
            this.InsrumentSearchpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.InsrumentSearchpictureBox.Height, this.InsrumentSearchpictureBox.Width, "Instrument Search", 0, 51, 0);
            this.ResultsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ResultsPictureBox.Height, this.ResultsPictureBox.Width, "Results", 0, 51, 0);
            //this.fsLegalGridUserControl.SectionGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridEditEndEventHandler(this.FsLegalGridUserControl_SectionGridEndEdit);
            //this.fsLegalGridUserControl.NEGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NEGridEditEndEventHandler(this.FsLegalGridUserControl_NEGridEndEdit);
            //this.fsLegalGridUserControl.NWGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NWGridEditEndEventHandler(this.FsLegalGridUserControl_NWGridEndEdit);
            //this.fsLegalGridUserControl.SWGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SWGridEditEndEventHandler(this.FsLegalGridUserControl_SWGridEndEdit);
            //this.fsLegalGridUserControl.SEGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SEGridEditEndEventHandler(this.FsLegalGridUserControl_SEGridEndEdit);
            this.fsLegalGridUserControl.NEGridKeyUp += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NEGridKeyUpEventHandler(this.FsLegalGridUserControl_NEGridKeyUp);
            this.fsLegalGridUserControl.NWGridKeyUp += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NWGridKeyUpEventHandler(this.FsLegalGridUserControl_NWGridKeyUp);
            this.fsLegalGridUserControl.SEGridKeyUp += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SEGridKeyUpEventHandler(this.FsLegalGridUserControl_SEGridKeyUp);
            this.fsLegalGridUserControl.SWGridKeyUp += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SWGridKeyUpEventHandler(this.FsLegalGridUserControl_SWGridKeyUp);
            this.fsLegalGridUserControl.SecGridKeyUp += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridKeyUpEventHandler(this.FsLegalGridUserControl_SecGridKeyUp);

            this.fsLegalGridUserControl.SectionGridSelectionChangeEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridSelectionChangeEventHandler(fsLegalGridUserControl_SectionGridSelectionChangeEvent);
            this.fsLegalGridUserControl.SectionPreviewKeyDownEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridPreviewKeyDownEventHandler(fsLegalGridUserControl_SectionPreviewKeyDownEvent);
            this.fsLegalGridUserControl.SectionTextChanged += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridTextChanged(fsLegalGridUserControl_SectionTextChanged);
            this.fsLegalGridUserControl.NETextChanged += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NEGridTextChanged(fsLegalGridUserControl_NETextChanged);
            this.fsLegalGridUserControl.NWTextChanged += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NWGridTextChanged(fsLegalGridUserControl_NWTextChanged);
            this.fsLegalGridUserControl.SETextChanged += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SEGridTextChanged(fsLegalGridUserControl_SETextChanged);
            this.fsLegalGridUserControl.SWTextChanged += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SWGridTextChanged(fsLegalGridUserControl_SWTextChanged);

            this.fsLegalGridUserControl.SectionPKeyDownEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionPreviewKeyDownEventHandler(fsLegalGridUserControl_SectionPKeyDownEvent);
            this.fsLegalGridUserControl.NEPreviewKeyDownEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NEGridPreviewKeyDownEventHandler(fsLegalGridUserControl_NEPreviewKeyDownEvent);
            this.fsLegalGridUserControl.NWPreviewKeyDownEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NWGridPreviewKeyDownEventHandler(fsLegalGridUserControl_NWPreviewKeyDownEvent);
            this.fsLegalGridUserControl.SEPreviewKeyDownEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SEGridPreviewKeyDownEventHandler(fsLegalGridUserControl_SEPreviewKeyDownEvent);
            this.fsLegalGridUserControl.SWPreviewKeyDownEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SWGridPreviewKeyDownEventHandler(fsLegalGridUserControl_SWPreviewKeyDownEvent);


            this.fsLegalGridUserControl.NEKeyPressEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NEGridKeyPressEventHandler(fsLegalGridUserControl_NEKeyPressEvent);
            this.fsLegalGridUserControl.NWKeyPressEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NWGridKeyPressEventHandler(fsLegalGridUserControl_NWKeyPressEvent);
            this.fsLegalGridUserControl.SWKeyPressEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SWGridKeyPressEventHandler(fsLegalGridUserControl_SWKeyPressEvent);
            this.fsLegalGridUserControl.SEKeyPressEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SEGridKeyPressEventHandler(fsLegalGridUserControl_SEKeyPressEvent);
            this.fsLegalGridUserControl.SectionKeyPressEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridKeyPressEventHandler(fsLegalGridUserControl_SectionKeyPressEvent);

            this.fsLegalGridUserControl.SectionGridCellContentClick += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridCellContentClickEventHandler(fsLegalGridUserControl_SectionGridCellContentClick);
            this.fsLegalGridUserControl.SectionGridCellContentDoubleClick += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridCellContentDoubleClickEventHandler(fsLegalGridUserControl_SectionGridCellContentDoubleClick);

        }

        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the F49910Control.
        /// </summary>
        /// <value>The F49910Control.</value>
        [CreateNew]
        public F49920Controller F49920Control
        {
            get { return this.form49920Control as F49920Controller; }
            set { this.form49920Control = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F49920 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F49920_Load(object sender, EventArgs e)
        {
            try
            {
                this.flagLoadOnProcess = true;
                ////For Combox Box - Instrument Type
                this.loadInstSearchEngineDataSet = this.form49920Control.WorkItem.F49920_ListInstrumentLoad();
                this.InstrumentTypeComboBox.DataSource = this.loadInstSearchEngineDataSet.ListInstrumentLoadItems;

                this.InstrumentTypeComboBox.DisplayMember = this.loadInstSearchEngineDataSet.ListInstrument.InstrumentTypeColumn.ColumnName;
                this.InstrumentTypeComboBox.ValueMember = this.loadInstSearchEngineDataSet.ListInstrument.ITypeIDColumn.ColumnName;
                this.InstrumentTypeComboBox.SelectedIndex = -1;
                ////this.InstrumentTypeComboBox.DropDownStyle = ComboBoxStyle.DropDown;

                if (this.form49920Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form49920Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form49920Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                this.formLabelInfo[0] = SharedFunctions.GetResourceString("F49920Header");
                this.formLabelInfo[1] = string.Empty;

                ////sets form header
                this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));

                this.LoadFSLegalGrid();
                this.InstrumentSearchEngineSearchButton.Enabled = false;

                this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstNumColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
                this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.FileDateColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
                this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstrumentTypeColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
                this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GrantorColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
                this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GranteeColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
                this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubNameColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
                this.flagLoadOnProcess = false;
                //this.pageMode = TerraScanCommon.PageModeTypes.View;    
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Calendar Events

        /// <summary>
        /// Timers the image_ click.
        /// </summary>
        /// <param name="textControl">The text control.</param>
        /// <param name="timePickerControl">The time picker control.</param>
        private void TimerImage_Click(TextBox textControl, DateTimePicker timePickerControl)
        {
            this.ParentForm.AcceptButton = null;
            try
            {
                timePickerControl.BringToFront();
                if (!string.IsNullOrEmpty(textControl.Text.Trim()))
                {
                    timePickerControl.Value = Convert.ToDateTime(textControl.Text);
                }
                else
                {
                    timePickerControl.Value = DateTime.Today;
                }

                SendKeys.Send("{F4}");
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FromDateCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromDateCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.FromDateTextBox, this.FromDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ToDateCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToDateCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.ToDateTextBox, this.ToDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the ToDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.ToDateTextBox.Text = this.ToDateTimePicker.Text;
                this.ToDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the FromDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.FromDateTextBox.Text = this.FromDateTimePicker.Text;
                this.FromDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region PictureBox Events

        /// <summary>
        /// Handles the Click event of the InsrumentSearchpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InsrumentSearchpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ResultsPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ResultsPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region User Control
        /// <summary>
        /// Loads the FS legal grid.
        /// </summary>
        private void LoadFSLegalGrid()
        {
            this.instLoadItemTable = new F49920InstrumentSearchEngineData.InstrumentLoadItemsDataTable();

            this.loadInstSearchEngineDataSet = this.form49920Control.WorkItem.F49920_ListInstrumentLoad();
            this.instLoadItemTable = this.loadInstSearchEngineDataSet.InstrumentLoadItems;

            DataTable masterTable = new DataTable();
            masterTable.Clear();
            masterTable.Columns.Clear();
            masterTable.Columns.AddRange(new DataColumn[] { new DataColumn("SubID", System.Type.GetType("System.Int32")), new DataColumn("SubName", System.Type.GetType("System.String")) });
            DataRow emptyGrantorRow = masterTable.NewRow();
            emptyGrantorRow[this.instLoadItemTable.Columns["Subname"].ColumnName] = "";
            emptyGrantorRow[this.instLoadItemTable.Columns["SubID"].ColumnName] = -1;
            masterTable.Rows.Add(emptyGrantorRow);
            this.instLoadItemTable.Merge(masterTable);

            this.fsLegalGridUserControl.fillData(this.instLoadItemTable);
        }

        #endregion

        #region Button Events
        /// <summary>
        /// Handles the Click event of the InstrumentSearchEngineSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstrumentSearchEngineSearchButton_Click(object sender, EventArgs e)
        {
            try
            {

                InstrumentSearchEngineSearchButton.Focus();
                this.instrumentSearchEngineResultsDataTable.Rows.Clear();
                this.InstrumentSearchEngineResultsDataGrid.DataSource = this.instrumentSearchEngineResultsDataTable;
                this.ResultsGridFormatting();

                DataSet ds1 = new DataSet();
                ds1 = fsLegalGridUserControl.GetDatas();
                int tempSelectedSubDivision = -1;
                if (((fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView)[0, 0] as DataGridViewComboBoxCell).Value != null)
                {
                    int.TryParse(((fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView)[0, 0] as DataGridViewComboBoxCell).Value.ToString(), out tempSelectedSubDivision);
                }

                ds1.Tables.RemoveAt(5);
                ds1.AcceptChanges();
                string instrumentcondition = string.Empty;

                DataTable dt = new DataTable();
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add(SharedFunctions.GetResourceString("F49920Grantor"));
                    dt.Columns.Add(SharedFunctions.GetResourceString("F49920Grantee"));
                    dt.Columns.Add(SharedFunctions.GetResourceString("F49920InstNum"));
                    dt.Columns.Add(SharedFunctions.GetResourceString("F49920BookPage"));
                    dt.Columns.Add(SharedFunctions.GetResourceString("F49920InstrumentType"));
                    dt.Columns.Add(SharedFunctions.GetResourceString("F49920FromDate"));
                    dt.Columns.Add(SharedFunctions.GetResourceString("F49920ToDate"));
                }

                DataRow dr;
                dr = dt.NewRow();
                dr[SharedFunctions.GetResourceString("F49920Grantor")] = this.GrantorTextBox.Text.Trim();
                dr[SharedFunctions.GetResourceString("F49920Grantee")] = this.GranteeTextBox.Text.Trim();
                dr[SharedFunctions.GetResourceString("F49920InstNum")] = this.InstrumentNumberTextBox.Text.Trim();
                dr[SharedFunctions.GetResourceString("F49920BookPage")] = this.BookPageTextBox.Text.Trim();
                dr[SharedFunctions.GetResourceString("F49920InstrumentType")] = this.InstrumentTypeComboBox.SelectedValue;
                dr[SharedFunctions.GetResourceString("F49920FromDate")] = this.FromDateTextBox.Text.Trim();
                dr[SharedFunctions.GetResourceString("F49920ToDate")] = this.ToDateTextBox.Text.Trim();
                dt.Rows.Add(dr);
                ds1.Tables.Add(dt);

                instrumentcondition = ds1.GetXml();

                this.LoadInstrumentSearchEngineResults(instrumentcondition);

                ////if (fsLegalGridUserControl.Controls[0] != null)
                ////{
                ////    if (fsLegalGridUserControl.Controls[0].Controls[10] != null)
                ////    {
                ////        if (fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] != null)
                ////        {
                ////            if (tempSelectedSubDivision != -1)
                ////            {
                ////                ((fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView)[0, 0] as DataGridViewComboBoxCell).Value = tempSelectedSubDivision;
                ////            }
                ////        }
                ////    }
                ////}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the InstrumentClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstrumentClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.clearflag = true;
                this.ClearInstrumentSearchEngineDetails();
                this.LoadFSLegalGrid();
                this.clearflag = false;
                this.GrantorTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Methods
        #region editrecord
        ///<summary>
        ///editable field for instrument type combo
        ///</summary>
        private void editrecord()
        {
            //if (!this.flagLoadOnProcess)
            //{
            //    this.pageMode = TerraScanCommon.PageModeTypes.View;
            //}
        }
        #endregion
        #region InstrumentSearchEngine
        /// <summary>
        /// Loads the instrument search engine results.
        /// </summary>
        /// <param name="instrumentcondition">The instrumentcondition.</param>
        private void LoadInstrumentSearchEngineResults(string instrumentcondition)
        {
            ////For Grid
            this.loadInstSearchEngineDataSet = this.form49920Control.WorkItem.F49920_ListInstrumentSearch(instrumentcondition);
            this.instrumentSearchEngineResultsDataTable = this.loadInstSearchEngineDataSet.ListInstrument;

            ////For UltraDataSouce--Load On Demand
            this.InstrumentDataSource.Band.Columns.Clear();
            this.InstrumentDataSource.Rows.SetCount(this.instrumentSearchEngineResultsDataTable.Rows.Count);

            for (int i = 0; i < this.instrumentSearchEngineResultsDataTable.Columns.Count; i++)
            {
                this.InstrumentDataSource.Band.Columns.Add(this.instrumentSearchEngineResultsDataTable.Columns[i].ColumnName);
            }

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.LoadStyle = LoadStyle.LoadOnDemand;
            this.InstrumentSearchEngineResultsDataGrid.DataSource = this.InstrumentDataSource;
            if (InstrumentSearchEngineResultsDataGrid.Rows.Count > 0)
            {
                InstrumentSearchEngineResultsDataGrid.Rows[0].Selected = true;
            }

            // this.ResultsGridFormatting();
        }
        #endregion

        #region ClearInstrumentSearchEngineDetails

        /// <summary>
        /// Clears the instrument search engine details.
        /// </summary>
        private void ClearInstrumentSearchEngineDetails()
        {
            this.instrumentSearchEngineResultsDataTable.Rows.Clear();
            this.InstrumentSearchEngineResultsDataGrid.DataSource = this.instrumentSearchEngineResultsDataTable;
            this.ResultsGridFormatting();
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstNumColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.FileDateColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstrumentTypeColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GrantorColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GranteeColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubNameColumn.ColumnName].SortIndicator = SortIndicator.Disabled;
            this.InstrumentSearchEngineSearchButton.Enabled = false;

            this.InstrumentTypeComboBox.Text = string.Empty;
            this.GrantorTextBox.Text = string.Empty;
            this.GranteeTextBox.Text = string.Empty;
            this.InstrumentNumberTextBox.Text = string.Empty;
            this.BookPageTextBox.Text = string.Empty;
            this.ToDateTextBox.Text = string.Empty;
            this.FromDateTextBox.Text = string.Empty;
            ////Added By Ramya


            this.InstrumentTypeComboBox.SelectedIndex = -1;
            this.InstrumentTypeComboBox.Text = string.Empty;
            this.InstrumentTypeComboBox.SelectedIndex = -1;

            this.GrantorPanel.Focus();
            this.GrantorTextBox.Focus();
        }

        /// <summary>
        /// Resultses the grid formatting.
        /// </summary>
        private void ResultsGridFormatting()
        {
            this.GrantorTextBox.MaxLength = this.instrumentSearchEngineResultsDataTable.GrantorColumn.MaxLength;
            this.GranteeTextBox.MaxLength = this.instrumentSearchEngineResultsDataTable.GranteeColumn.MaxLength;
            this.InstrumentNumberTextBox.MaxLength = this.instrumentSearchEngineResultsDataTable.InstNumColumn.MaxLength;

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstIDColumn.ColumnName].Hidden = true;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.ITypeIDColumn.ColumnName].Hidden = true;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubIDColumn.ColumnName].Hidden = true;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubName1Column.ColumnName].Hidden = true;

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstNumColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstNumColumn.ColumnName].Header.VisiblePosition = 0;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstNumColumn.ColumnName].Header.Caption = "Instrument #";
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstNumColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.FileDateColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.FileDateColumn.ColumnName].Header.VisiblePosition = 1;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.FileDateColumn.ColumnName].Header.Caption = "File Date";
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.FileDateColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstrumentTypeColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstrumentTypeColumn.ColumnName].Header.VisiblePosition = 2;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstrumentTypeColumn.ColumnName].Header.Caption = "Instrument Type";
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstrumentTypeColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GrantorColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GrantorColumn.ColumnName].Header.VisiblePosition = 3;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GrantorColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GranteeColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GranteeColumn.ColumnName].Header.VisiblePosition = 4;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GranteeColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubNameColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubNameColumn.ColumnName].Header.VisiblePosition = 5;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubNameColumn.ColumnName].Header.Caption = "Section/Subdivision";
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubNameColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            ////For Sorting
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstNumColumn.ColumnName].SortIndicator = SortIndicator.None;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.FileDateColumn.ColumnName].SortIndicator = SortIndicator.None;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.InstrumentTypeColumn.ColumnName].SortIndicator = SortIndicator.None;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GrantorColumn.ColumnName].SortIndicator = SortIndicator.None;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.GranteeColumn.ColumnName].SortIndicator = SortIndicator.None;
            this.InstrumentSearchEngineResultsDataGrid.DisplayLayout.Bands[0].Columns[this.instrumentSearchEngineResultsDataTable.SubNameColumn.ColumnName].SortIndicator = SortIndicator.None;
        }
        #endregion

        #endregion

        #region Results Grid Events
        /// <summary>
        /// Handles the DoubleClickRow event of the InstrumentSearchEngineResultsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs"/> instance containing the event data.</param>
        private void InstrumentSearchEngineResultsDataGrid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                ////below value used for new and cancel operation fo the master form(In new operation keyid is set to zero, then during cancel operation(reload the entire form) this value is used 
                int keyId;
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(49901);
                formInfo.optionalParameters = new object[1];
                int.TryParse(this.InstrumentSearchEngineResultsDataGrid.ActiveRow.Cells[this.instrumentSearchEngineResultsDataTable.InstIDColumn.ColumnName].Value.ToString(), out keyId);
                formInfo.optionalParameters[0] = keyId;
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
        /// Handles the KeyDown event of the InstrumentSearchEngineResultsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InstrumentSearchEngineResultsDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ////below value used for new and cancel operation fo the master form(In new operation keyid is set to zero, then during cancel operation(reload the entire form) this value is used 
                    int keyId;
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(49901);
                    formInfo.optionalParameters = new object[1];
                    int.TryParse(this.InstrumentSearchEngineResultsDataGrid.ActiveRow.Cells[this.instrumentSearchEngineResultsDataTable.InstIDColumn.ColumnName].Value.ToString(), out keyId);
                    formInfo.optionalParameters[0] = keyId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
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

        #endregion

        #region Help Link
        /// <summary>
        /// Handles the LinkClicked event of the HelpLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Instument DataSource - Load On Demand

        /// <summary>
        /// Handles the CellDataRequested event of the InstrumentDataSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs"/> instance containing the event data.</param>
        private void InstrumentDataSource_CellDataRequested(object sender, Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs e)
        {
            try
            {
                e.Data = this.instrumentSearchEngineResultsDataTable.Rows[e.Row.Index].ItemArray[e.Column.Index].ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Text Changed Events
        /// <summary>
        /// Handles the TextChanged event of the GrantorTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GrantorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.flagLoadOnProcess)
            {
                this.SetUpdateMode();
            }
        }

        /// <summary>
        /// Sets the update mode.
        /// </summary>
        private void SetUpdateMode()
        {
           
            //DataGridView editedGridView = (this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView);
            if (this.PermissionEdit)
            {
                if (!string.IsNullOrEmpty(this.GrantorTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.GranteeTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.InstrumentNumberTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.BookPageTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.FromDateTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.ToDateTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.InstrumentTypeComboBox.Text.Trim()))
                {
                    this.InstrumentSearchEngineSearchButton.Enabled = true;
                    return;
                }
                else
                {
                    this.InstrumentSearchEngineSearchButton.Enabled = false;
                }
                ////SectionGrid Checking

                if (((DataGridView)this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0]).DataSource != null)
                {
                    int subdivisionsection;
                    bool lotp;
                    bool blockp;

                    int.TryParse(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0])).Rows[0].Cells[0].Value.ToString().Trim(), out subdivisionsection);
                    bool.TryParse(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0])).Rows[0].Cells[2].Value.ToString().Trim(), out lotp);
                    bool.TryParse(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0])).Rows[0].Cells[4].Value.ToString().Trim(), out blockp);

                    if (subdivisionsection != 0 || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0])).Rows[0].Cells[1].Value.ToString().Trim()) || lotp != false || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0])).Rows[0].Cells[3].Value.ToString().Trim()) || blockp != false)
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = true;
                        return;
                    }
                    else
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = false;
                    }
                }

                //if (!string.IsNullOrEmpty(editedGridView[1, 0].Value.ToString()))
                //{
                //    this.InstrumentSearchEngineSearchButton.Enabled = true;
                //}

                ////NEGrid Checking

                if (((DataGridView)this.fsLegalGridUserControl.Controls[0].Controls[6].Controls[0]).DataSource != null)
                {
                    if (!string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[6].Controls[0])).Rows[0].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[6].Controls[0])).Rows[0].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[6].Controls[0])).Rows[0].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[6].Controls[0])).Rows[0].Cells[3].Value.ToString().Trim()))
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = true;
                        return;
                    }
                    else
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = false;
                    }
                }

                ////NWGrid Checking

                if (((DataGridView)this.fsLegalGridUserControl.Controls[0].Controls[5].Controls[0]).DataSource != null)
                {
                    if (!string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[5].Controls[0])).Rows[0].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[5].Controls[0])).Rows[0].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[5].Controls[0])).Rows[0].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[5].Controls[0])).Rows[0].Cells[3].Value.ToString().Trim()))
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = true;
                        return;
                    }
                    else
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = false;
                    }
                }

                ////SWGrid Checking
                if (((DataGridView)this.fsLegalGridUserControl.Controls[0].Controls[4].Controls[0]).DataSource != null)
                {
                    if (!string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[4].Controls[0])).Rows[0].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[4].Controls[0])).Rows[0].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[4].Controls[0])).Rows[0].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[4].Controls[0])).Rows[0].Cells[3].Value.ToString().Trim()))
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = true;
                        return;
                    }
                    else
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = false;
                    }
                }

                ////SEGrid Checking
                if (((DataGridView)this.fsLegalGridUserControl.Controls[0].Controls[8].Controls[0]).DataSource != null)
                {
                    if (!string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[8].Controls[0])).Rows[0].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[8].Controls[0])).Rows[0].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[8].Controls[0])).Rows[0].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(((DataGridView)(this.fsLegalGridUserControl.Controls[0].Controls[8].Controls[0])).Rows[0].Cells[3].Value.ToString().Trim()))
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = true;
                        return;
                    }
                    else
                    {
                        this.InstrumentSearchEngineSearchButton.Enabled = false;
                    }
                }
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
                this.GrantorTextBox.LockKeyPress = true;
                this.GranteeTextBox.LockKeyPress = true;
                this.InstrumentNumberTextBox.LockKeyPress = true;
                this.BookPageTextBox.LockKeyPress = true;
                this.FromDateCalendar.Enabled = false;
                this.ToDateCalendar.Enabled = false;
                this.InstrumentTypeComboBox.Enabled = false;
                this.fsLegalGridUserControl.Enabled = false;
            }
        }
        #endregion

        #region UserControlEvents

        /// <summary>
        /// Handles the NETextChanged event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_NETextChanged(object sender, EventArgs e)
        {

            DataGridView NEChangeGridView = (this.fsLegalGridUserControl.Controls[0].Controls[6].Controls[0] as DataGridView);

            NEChangeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (!string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[3].Value.ToString().Trim()))
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }

            // this.InstrumentSearchEngineSearchButton.Enabled = true;
            //throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Handles the SectionGridSelectionChangeEvent event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_SectionGridSelectionChangeEvent(object sender, EventArgs e)
        {
            this.SetUpdateMode();
            this.InstrumentSearchEngineSearchButton.Enabled = true;
        }

        private void fsLegalGridUserControl_SectionGridCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView NEChangeGridView = (this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView);

            NEChangeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            bool lotp;
            bool blockp;

            bool.TryParse(NEChangeGridView.Rows[0].Cells[2].Value.ToString().Trim(), out lotp);
            bool.TryParse(NEChangeGridView.Rows[0].Cells[4].Value.ToString().Trim(), out blockp);

            if (lotp != false || blockp != false)
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
            }
        }

        private void fsLegalGridUserControl_SectionGridCellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView NEChangeGridView = (this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView);

            NEChangeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            bool lotp;
            bool blockp;

            bool.TryParse(NEChangeGridView.Rows[0].Cells[2].Value.ToString().Trim(), out lotp);
            bool.TryParse(NEChangeGridView.Rows[0].Cells[4].Value.ToString().Trim(), out blockp);

            if (lotp != false || blockp != false)
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
            }
        }
        private void fsLegalGridUserControl_SectionPreviewKeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13 && this.InstrumentSearchEngineSearchButton.Enabled)
            {

                this.InstrumentSearchEngineSearchButton_Click(sender, e);
            }
        }

        private void fsLegalGridUserControl_SectionKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || e.KeyChar == 22)
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the SectionTextChanged event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_SectionTextChanged(object sender, EventArgs e)
        {
            if (!this.flagLoadOnProcess)
            {
                DataGridView NEChangeGridView = (this.fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView);

                NEChangeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                int subdivisionsection;
                bool lotp;
                bool blockp;

                int.TryParse(NEChangeGridView.Rows[0].Cells[0].Value.ToString().Trim(), out subdivisionsection);
                bool.TryParse(NEChangeGridView.Rows[0].Cells[2].Value.ToString().Trim(), out lotp);
                bool.TryParse(NEChangeGridView.Rows[0].Cells[4].Value.ToString().Trim(), out blockp);

                if (subdivisionsection != 0 || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[1].Value.ToString().Trim()) || lotp != false || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[3].Value.ToString().Trim()) || blockp != false)
                {
                    this.InstrumentSearchEngineSearchButton.Enabled = true;
                }
            }

            // this.InstrumentSearchEngineSearchButton.Enabled = true;
        }

        /// <summary>
        /// Handles the SWTextChanged event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_SWTextChanged(object sender, EventArgs e)
        {
            DataGridView NEChangeGridView = (this.fsLegalGridUserControl.Controls[0].Controls[4].Controls[0] as DataGridView);

            NEChangeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (!string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[0].Value.ToString().Trim().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[1].Value.ToString().Trim().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[2].Value.ToString().Trim().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[3].Value.ToString().Trim().Trim()))
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            // this.InstrumentSearchEngineSearchButton.Enabled = true;
        }

        /// <summary>
        /// Handles the SETextChanged event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_SETextChanged(object sender, EventArgs e)
        {
            DataGridView NEChangeGridView = (this.fsLegalGridUserControl.Controls[0].Controls[8].Controls[0] as DataGridView);

            NEChangeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (!string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[3].Value.ToString().Trim()))
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            //this.InstrumentSearchEngineSearchButton.Enabled = true;
        }

        /// <summary>
        /// Handles the NWTextChanged event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_NWTextChanged(object sender, EventArgs e)
        {
            DataGridView NEChangeGridView = (this.fsLegalGridUserControl.Controls[0].Controls[5].Controls[0] as DataGridView);

            NEChangeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (!string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(NEChangeGridView.Rows[0].Cells[3].Value.ToString().Trim()))
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            //this.InstrumentSearchEngineSearchButton.Enabled = true;
        }

        /// <summary>
        /// Handles the SWPreviewKeyDownEvent event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_SWPreviewKeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13 && this.InstrumentSearchEngineSearchButton.Enabled)
            {
                this.InstrumentSearchEngineSearchButton_Click(sender, e);
            }
        }

        private void fsLegalGridUserControl_NWKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || e.KeyChar == 22)
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
            }
        }

        private void fsLegalGridUserControl_SEKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || e.KeyChar == 22)
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
            }

        }

        private void fsLegalGridUserControl_SWKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || e.KeyChar == 22)
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the NWPreviewKeyDownEvent event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_NWPreviewKeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13 && this.InstrumentSearchEngineSearchButton.Enabled)
            {
                this.InstrumentSearchEngineSearchButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles the NEPreviewKeyDownEvent event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_NEPreviewKeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13 && this.InstrumentSearchEngineSearchButton.Enabled)
            {
                this.InstrumentSearchEngineSearchButton_Click(sender, e);
            }
        }

        private void fsLegalGridUserControl_NEKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || e.KeyChar == 22)
            {
                this.InstrumentSearchEngineSearchButton.Enabled = true;
            }
            else
            {
                this.InstrumentSearchEngineSearchButton.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the SectionPKeyDownEvent event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_SectionPKeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13 && this.InstrumentSearchEngineSearchButton.Enabled)
            {
                this.InstrumentSearchEngineSearchButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles the SEPreviewKeyDownEvent event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_SEPreviewKeyDownEvent(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 13 && this.InstrumentSearchEngineSearchButton.Enabled)
            {
                this.InstrumentSearchEngineSearchButton_Click(sender, e);
            }
        }

        #endregion

        #region All TextBox KeyDown

        /// <summary>
        /// Validates the date.
        /// </summary>
        /// <returns>boolean</returns>
        private bool ValidateDate()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.FromDateTextBox.Text.Trim()))
                {
                    DateTime.Parse(this.FromDateTextBox.Text.Trim());
                }

                if (!string.IsNullOrEmpty(this.ToDateTextBox.Text.Trim()))
                {
                    DateTime.Parse(this.ToDateTextBox.Text.Trim());
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// FromDateTextBox_KeyUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FromDateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.InstrumentSearchEngineSearchButton.Enabled)
                {
                    if (this.ValidateDate())
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.Enter:
                                {
                                    this.UserControlKeyDown();
                                    break;
                                }

                            default:
                                {
                                    break;
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
        /// Handles the NEGridKeyUp event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_NEGridKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.InstrumentSearchEngineSearchButton.Enabled)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                this.UserControlKeyDown();
                                break;
                            }

                        default:
                            {
                                break;
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
        /// Handles the NWGridKeyUp event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_NWGridKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.InstrumentSearchEngineSearchButton.Enabled)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                this.UserControlKeyDown();
                                break;
                            }

                        default:
                            {
                                break;
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
        /// Handles the SEGridKeyUp event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_SEGridKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.InstrumentSearchEngineSearchButton.Enabled)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                this.UserControlKeyDown();

                                break;
                            }

                        default:
                            {
                                break;
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
        /// Handles the SWGridKeyUp event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_SWGridKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.InstrumentSearchEngineSearchButton.Enabled)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                this.UserControlKeyDown();

                                break;
                            }

                        default:
                            {
                                break;
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
        /// Handles the SecGridKeyUp event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_SecGridKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.InstrumentSearchEngineSearchButton.Enabled)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Enter:
                            {
                                this.UserControlKeyDown();
                                break;
                            }

                        default:
                            {
                                break;
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
        /// Users the control key down.
        /// </summary>
        private void UserControlKeyDown()
        {
            this.instrumentSearchEngineResultsDataTable.Rows.Clear();
            this.InstrumentSearchEngineResultsDataGrid.DataSource = this.instrumentSearchEngineResultsDataTable;
            this.ResultsGridFormatting();

            DataSet ds1 = new DataSet();
            ds1 = fsLegalGridUserControl.GetDatas();
            int tempSelectedSubDivision = -1;
            if (((fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView)[0, 0] as DataGridViewComboBoxCell).Value != null)
            {
                int.TryParse(((fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView)[0, 0] as DataGridViewComboBoxCell).Value.ToString(), out tempSelectedSubDivision);
            }

            ds1.Tables.RemoveAt(5);
            ds1.AcceptChanges();
            string instrumentcondition = string.Empty;

            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add(SharedFunctions.GetResourceString("F49920Grantor"));
                dt.Columns.Add(SharedFunctions.GetResourceString("F49920Grantee"));
                dt.Columns.Add(SharedFunctions.GetResourceString("F49920InstNum"));
                dt.Columns.Add(SharedFunctions.GetResourceString("F49920BookPage"));
                dt.Columns.Add(SharedFunctions.GetResourceString("F49920InstrumentType"));
                dt.Columns.Add(SharedFunctions.GetResourceString("F49920FromDate"));
                dt.Columns.Add(SharedFunctions.GetResourceString("F49920ToDate"));
            }

            DataRow dr;
            dr = dt.NewRow();
            dr[SharedFunctions.GetResourceString("F49920Grantor")] = this.GrantorTextBox.Text.Trim();
            dr[SharedFunctions.GetResourceString("F49920Grantee")] = this.GranteeTextBox.Text.Trim();
            dr[SharedFunctions.GetResourceString("F49920InstNum")] = this.InstrumentNumberTextBox.Text.Trim();
            dr[SharedFunctions.GetResourceString("F49920BookPage")] = this.BookPageTextBox.Text.Trim();
            dr[SharedFunctions.GetResourceString("F49920InstrumentType")] = this.InstrumentTypeComboBox.SelectedValue;
            dr[SharedFunctions.GetResourceString("F49920FromDate")] = this.FromDateTextBox.Text.Trim();
            dr[SharedFunctions.GetResourceString("F49920ToDate")] = this.ToDateTextBox.Text.Trim();
            dt.Rows.Add(dr);
            ds1.Tables.Add(dt);

            instrumentcondition = ds1.GetXml();

            this.LoadInstrumentSearchEngineResults(instrumentcondition);

            if (fsLegalGridUserControl.Controls[0] != null)
            {
                if (fsLegalGridUserControl.Controls[0].Controls[10] != null)
                {
                    if (fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] != null)
                    {
                        if (tempSelectedSubDivision != -1)
                        {
                            ((fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView)[0, 0] as DataGridViewComboBoxCell).Value = tempSelectedSubDivision;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Enter event of the fsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void fsLegalGridUserControl_Enter(object sender, EventArgs e)
        {
            // fsLegalGridUserControl.Focus();
            if (!this.clearflag)
            {
                (fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView).CurrentCell = ((fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView)[0, 0]);
                (fsLegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView).Focus();
            }
        }

        private void fsLegalGridUserControl_Load(object sender, EventArgs e)
        {
            fsLegalGridUserControl.commentshide = false;
        }

        private void BookPagePanel_Paint(object sender, PaintEventArgs e)
        {


        }

        private void InstrumentTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //this.editrecord(); 
            //try
            //{
            //    this.EditRecord();
            //    if (this.InstrumentTypeComboBox.SelectedValue != null)
            //    {
            //        int.TryParse(this.InstrumentTypeComboBox.SelectedValue.ToString(), out this.insTypeId);
            //        this.insTypeId = (int)this.InstrumentTypeComboBox.SelectedValue;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
            
        }
    }
}
