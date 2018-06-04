//--------------------------------------------------------------------------------------------
// <copyright file="F8001.cs" company="Congruent">
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
// 06/09/2001   	GUHAN S	           Created
//*********************************************************************************/
namespace D8000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using System.Collections;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    using System.Configuration;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using TerraScan.SmartParts;
    using TerraScan.Common.Reports;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F8001
    /// </summary>
    public partial class F8001 : BaseSmartPart
    {
        #region variable

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "MM/dd/yyyy";

        /// <summary>
        /// Used to store audit Link Text
        /// </summary>
        private readonly string auditLinkkText = SharedFunctions.GetResourceString("F8001AuditLink");

        /// <summary>
        /// used to save status
        /// </summary>
        private bool saveStatus;

        /// <summary>
        /// Used to store  statusCount
        /// </summary>
        private int statusCount;

        /// <summary>
        /// Used to store  typeCount
        /// </summary>
        private int typeCount;

        /// <summary>
        /// used to store EventID
        /// </summary>
        private int newEventID;

        /// <summary>
        /// used to store EventEngineSource
        /// </summary>
        private BindingSource eventEngineSource = new BindingSource();

        /// <summary>
        /// report Action Smart part
        /// </summary>
        private ReportActionSmartPart reportActionSmartPart;

        /// <summary>
        /// Used to Store FeatureClassid
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// used to store system id;
        /// </summary>
        private int systemId;

        /// <summary>
        ///  Used to Store totalRecords
        /// </summary>
        private int totalDataCount;

        /// <summary>
        ///  used to Convert Color From string
        /// </summary>
        private ColorConverter colorConv = new ColorConverter();

        /// <summary>
        ///  used to store selected parentrow eventid
        /// </summary>
        private string currentParentEventId;

        /// <summary>
        ///  Used to Store CurrentRow
        /// </summary>
        private int currentRowSelected;

        /// <summary>
        ///  Used to Store CurrentRow
        /// </summary>
        private string currentRowLevel;

        /// <summary>
        /// USed to Store Event Type / Status Data
        /// </summary>
        private GDocEventEngineTypeStatusData eventEngineTypeStatusData = new GDocEventEngineTypeStatusData();

        /// <summary>
        /// F8001Controller
        /// </summary>
        private F8001Controller form8001Control;

        /// <summary>
        /// gDocEventEngineData
        /// </summary>
        private GDocEventEngineData eventEngineData = new GDocEventEngineData();

        /// <summary>
        /// eventRowNO
        /// </summary>
        private int eventRowNo;

        /// <summary>
        /// Used to Create Insert XML Data
        /// </summary>
        private GDocF8001InsertXMLData gdocF8001InsertXMLData = new GDocF8001InsertXMLData();

        /// <summary>
        /// FeatureID
        /// </summary>
        private int featureId;

        /////// <summary>
        /////// For Holding Mouse X Postion 
        /////// </summary>
        ////private Point mousepostion;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// used to dataChangeStatus
        /// </summary>
        private bool dataChangeStatus = false;

        /// <summary>
        /// used to dataChangeStatus
        /// </summary>
        private bool invalidKey;

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker eventEngineValidDate = new System.Windows.Forms.DateTimePicker();
        #endregion

        #region Constru

        /// <summary>
        /// F8001
        /// </summary>
        public F8001()
        {
            InitializeComponent();
        }

        #endregion

        #region  EventPublication

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the F8001Control.
        /// </summary>
        /// <value>F8001Control.</value>
        [CreateNew]
        public F8001Controller F8001Control
        {
            get { return this.form8001Control as F8001Controller; }
            set { this.form8001Control = value; }
        }

        #endregion

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
                this.form8001Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }
        #region OptionalParameter
        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        ///[EventSubscription("topic://TerraScan.UI.CAB/MainForm/SendOptionalParameters", Thread = ThreadOption.UserInterface)]
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.Tag))
            {
                if (optionalParams[0] != null && string.Equals(optionalParams[0].ToString(), "RecordDeleted"))
                {
                    ////this.featureClassId = eventEngineFeatureClassId;
                    ////this.featureId = eventEnginefeatureId;
                    this.LoadEventEngine();
                }
                else
                {
                    if (optionalParams[0] != null && optionalParams[1] != null && !string.IsNullOrEmpty(optionalParams[0].ToString()) && !string.IsNullOrEmpty(optionalParams[1].ToString()))
                    {
                        if (!string.IsNullOrEmpty(optionalParams[optionalParams.Length - 3].ToString()) && optionalParams[optionalParams.Length - 3] != null)
                        {
                            this.PermissionFiled = ((PermissionFields)optionalParams[optionalParams.Length - 3]);
                        }

                        this.featureClassId = Convert.ToInt32(optionalParams[0].ToString());
                        this.featureId = Convert.ToInt32(optionalParams[1].ToString());
                        this.LoadEventEngine();
                    }
                    else
                    {
                        this.featureClassId = 0;
                        this.featureId = 0;
                        this.LoadEventEngine();
                    }
                }
            }
        }
        #endregion
        #region Reports

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick, Thread = ThreadOption.UserInterface)]
        public void PrintButtonClick(object sender, DataEventArgs<Button> e)
        {
            // TODO : Genralized 
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("FeatureClassID", this.featureClassId);
                ht.Add("FeatureID", this.featureId);
                /////changed the parameter type from string to int
                TerraScanCommon.ShowReport(800101, Report.ReportType.Print, ht);
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

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("FeatureClassID", this.featureClassId);
                ht.Add("FeatureID", this.featureId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(800101, Report.ReportType.Preview, ht);
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

        /// <summary>
        /// Handles the Click event of the EMailButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick, Thread = ThreadOption.UserInterface)]
        public void EmailButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////// calling  Common Function For Report
                this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("FeatureClassID", this.featureClassId);
                ht.Add("FeatureID", this.featureId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(800101, Report.ReportType.Email, ht);
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

        #endregion reports
        #endregion EventSubScription

        #region Events

        /// <summary>
        /// Loads the event engine.
        /// </summary>
        public void LoadEventEngine()
        {
            //// Load Event Engine DataGrid
            this.LoadEventEngineData();
            this.GDocEventEngineCalenderControl.Visible = false;
            this.SetGdocEventHeader();
            this.GdocEventEngineAuditLink.Text = this.auditLinkkText + " " + this.featureClassId;
            this.SetNewMode();
            ////    this.ParentForm.AcceptButton = (Button)this.GdocEventEngineRowAddLabel;
            if (this.statusCount > 0 && this.typeCount > 0)
            {
                if (!this.PermissionFiled.newPermission)
                {
                    this.PermissionDisable();
                }
            }
            else
            {
                this.Disablecontrol(false);
            }
        }

        /// <summary>
        /// Handles the Load event of the F8001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8001_Load(object sender, EventArgs e)
        {
            this.LoadWorkSpace();
            this.GDocEventEngineCalenderControl.Visible = false;
            this.eventEngineValidDate.CustomFormat = "mm/dd/yyyy";
            this.eventEngineValidDate.MaxDate = new System.DateTime(2075, 12, 31, 0, 0, 0, 0);
            this.eventEngineValidDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// Handles the Click event of the GdocEventEngineDateImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineDateImage_Click(object sender, EventArgs e)
        {
            this.ParentForm.AcceptButton = null;

            this.ShowAttachmentCalender();
        }

        /// <summary>
        /// Handles the DateSelected event of the GDocF8001CalenderControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void GDocF8001CalenderControl_DateSelected(object sender, DateRangeEventArgs e)
        {
            //// Assign the selected date to the DateTextbox.
            this.dataChangeStatus = true;
            this.GDocEventEngineDateTextBox.Text = e.Start.ToShortDateString();
            this.GDocEventEngineDateTextBox.Focus();
            this.GDocEventEngineCalenderControl.Visible = false;
            ////   this.ParentForm.AcceptButton = (Button)this.GdocEventEngineRowAddLabel;
        }

        /// <summary>
        /// Handles the KeyDown event of the GDocF8001CalenderControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GDocF8001CalenderControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.GDocEventEngineDateTextBox.Text = this.GDocEventEngineCalenderControl.SelectionStart.ToShortDateString();
                this.GDocEventEngineCalenderControl.Visible = false;
                this.GDocEventEngineDateTextBox.Focus();
                this.dataChangeStatus = true;
                ////  this.ParentForm.AcceptButton = (Button)this.GdocEventEngineRowAddLabel;
            }
        }

        /// <summary>
        /// Handles the Leave event of the GDocF8001CalenderControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GDocF8001CalenderControl_Leave(object sender, EventArgs e)
        {
            this.GDocEventEngineCalenderControl.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the RowAddLable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RowAddLable_Click(object sender, EventArgs e)
        {
            this.SaveEventEngine();
        }

        /// <summary>
        /// Handles the InitializeLayout event of the ultraGrid1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.GDocEventEngineDataGridView.DisplayLayout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Image;

                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellAppearance.ForeColor = Color.Blue;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellAppearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;

                ////Commented and code added by Jayanthi
                ////this.GDocEventEngineDataGridView.DisplayLayout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellAppearance.ForeColor = Color.Blue;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellAppearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
                ////Till here                

                //// Code Added By Shiva for Change Request Srpint 22
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.TempColorColumn.ColumnName].Header.Caption = "";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Header.Caption = "";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Header.Caption = "Work Order";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventDateColumn.ColumnName].Header.Caption = "Date";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.IsCompleteColumn.ColumnName].Header.Caption = "Complete";
                e.Layout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.EventNumberColumn.ColumnName].Header.Caption = "Event";
                ////Till Here
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
        /// Handles the Click event of the ultraGrid1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_Click(object sender, EventArgs e)
        {
            if (this.GDocEventEngineDataGridView.ActiveCell != null)
            {
                if (this.GDocEventEngineDataGridView.ActiveCell.Row.Index >= 0)
                {
                    if (string.Equals(this.GDocEventEngineDataGridView.ActiveCell.Column.Key, this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName))
                    {
                        if (!string.IsNullOrEmpty(this.GDocEventEngineDataGridView.Rows[this.GDocEventEngineDataGridView.ActiveCell.Row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Text.Trim()))
                        {
                            this.GDocEventEngineDataGridView.Rows[this.GDocEventEngineDataGridView.ActiveCell.Row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Selected = true;
                            ////this.GDocEventEngineDataGridView.DisplayLayout.Override.SelectedCellAppearance.ForeColor = this.GDocEventEngineDataGridView.DisplayLayout.Override.SelectedCellAppearance.ForeColor;
                            this.GDocEventEngineDataGridView.DisplayLayout.Override.SelectedCellAppearance.ForeColor = Color.White;
                            this.WorkOrderShowForm(this.GDocEventEngineDataGridView.Rows[this.GDocEventEngineDataGridView.ActiveCell.Row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Text.Trim());
                            this.GDocEventEngineDataGridView.ActiveCell.Activated = false;
                        }
                        ////this.GDocEventEngineDataGridView.ActiveCell.Activated = false;                        
                    }
                    ////else if (string.Equals(this.GDocEventEngineDataGridView.ActiveCell.Column.Key, this.eventEngineData.GDocEventEngineDataTable.TempColorColumn.ColumnName))
                    ////{
                    ////}
                    else if (this.GDocEventEngineDataGridView.ActiveCell.Column.Index == 0)
                    {
                        ////this.currentRowLevel = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString().Trim();
                        ////if (!string.Equals(this.currentRowLevel, "3") && this.PermissionFiled.newPermission)
                        ////{
                        ////    this.CreateGdocEventEngineNewRow();
                        ////    this.LoadEventEngineData(this.featureClassId, this.featureId);  
                        ////    this.SetNewMode();
                        ////    this.FindShowId(this.currentParentEventId);
                        ////}
                    }
                }
            }
        }

        /// <summary>
        /// Handles the AfterCellActivate event of the ultraGrid1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_AfterCellActivate(object sender, EventArgs e)
        {
            /////  ((UltraGrid)sender).ActiveCell.Column.

            this.currentRowSelected = this.GDocEventEngineDataGridView.ActiveCell.Row.Index;   ////((UltraGrid)sender).ActiveCell.Row.Index;
            int currentColumn = this.GDocEventEngineDataGridView.ActiveCell.Column.Index;                                                      ////((UltraGrid)sender).ActiveCell.Column.Index;
            this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Selected = false;
            ////if (Convert.ToInt32(this.mousepostion.X.ToString()) > this.EventEngineParentImage.Images[0].Width)
            //// {
            try
            {
                if (currentColumn == 1)
                {
                    if (!this.invalidKey)
                    {
                        ////this.GDocEventEngineDataGridView.ActiveCell.Activated = false;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Activated = false;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Selected = true;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Activated = false;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Selected = false;
                        string showFrmId;
                        string showEventID;
                        this.currentRowLevel = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString().Trim();
                        showFrmId = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].Value.ToString().Trim();
                        showEventID = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Value.ToString().Trim();
                        this.ShowEventForm(showFrmId, showEventID);
                    }
                    else
                    {
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Activated = false;
                        this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Selected = true;
                        this.invalidKey = false;
                    }
                }
                else if (currentColumn == 0)
                {
                    this.currentRowLevel = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString().Trim();
                    if (!string.Equals(this.currentRowLevel, "3") && this.PermissionFiled.newPermission)
                    {
                        ////string childShowEventID;
                        this.CreateGdocEventEngineNewRow();
                        if (this.saveStatus)
                        {
                            this.LoadEventEngineData();
                            this.SetNewMode();
                            ////childShowEventID = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Value.ToString().Trim();
                            this.FindShowId(this.newEventID.ToString());
                        }
                        else if (this.newEventID == -1)
                        {
                            this.GDocEventEngineDataGridView.ActiveCell.Activated = false;
                            MessageBox.Show(SharedFunctions.GetResourceString("WorkOrder") + this.GdocEventEngineWorkOrderTextBox.Text + SharedFunctions.GetResourceString("WorkOrderDoesnotExist"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("WorkOrderValidation")), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.GDocEventEngineDataGridView.ActiveCell.Activated = false;
                        }
                    }
                }
                else
                {
                    this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].Selected = true;
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

        /// <summary>
        /// Handles the Click event of the WorkOrderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderPictureBox_Click(object sender, EventArgs e)
        {
            this.ShowWorkOrderForm();
        }

        /// <summary>
        /// Handles the MouseDown event of the ultraGrid1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //// this.mousepostion = new Point(e.X, e.Y);  
        }

        /// <summary>
        /// Handles the MouseHover event of the ultraGrid1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_MouseHover(object sender, EventArgs e)
        {
            //// Infragistics.Win.UIElement mouseHoverUIElement;
            ////string x = ((MouseEventArgs)e).X.ToString();
            if (((UltraGrid)sender).ActiveCell != null)
            {
                int columnIndex = this.GDocEventEngineDataGridView.ActiveCell.Column.Index; /////((UltraGrid)sender).ActiveCell.Column.Index;
                int rowIndex = this.GDocEventEngineDataGridView.ActiveCell.Row.Index; ////((UltraGrid)sender).ActiveCell.Row.Index;
                ////if ( columnIndex == 0 ||  columnIndex == 5)
                ////{
                ////    ((UltraGrid)sender).Cursor = Cursors.Hand;
                ////}
                ////else
                ////{
                ////    ((UltraGrid)sender).Cursor = Cursors.Default;
                ////}
                if (columnIndex == 2)
                {
                    this.GdocEventEngineToolTip.RemoveAll();
                    this.GdocEventEngineToolTip.SetToolTip(this.GDocEventEngineDataGridView, this.GDocEventEngineDataGridView.Rows[rowIndex].Cells[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Value.ToString());
                }
                else
                {
                    this.GdocEventEngineToolTip.RemoveAll();
                }
            }
            ////aUIElement = ultraGrid1.DisplayLayout.UIElement.ElementFromPoint(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
            ////e.Layout.Bands[0].Columns[this.gDocEventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the GdocEventEngineStatusComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineStatusComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.dataChangeStatus = true;
            this.SetStatusColor(this.GdocEventEngineStatusComboBox.SelectedValue.ToString());
        }

        /// <summary>
        /// Handles the MouseMove event of the GdocEventEngineTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineTypeComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.GdocEventEngineTypeComboBox.Text.Length > 10)
            {
                this.GdocEventEngineToolTip.SetToolTip(this.GdocEventEngineTypeComboBox, this.GdocEventEngineTypeComboBox.Text);
            }
            else
            {
                this.GdocEventEngineToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the GdocEventEngineStatusComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineStatusComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.GdocEventEngineStatusComboBox.Text.Trim().Length > 5)
            {
                this.GdocEventEngineToolTip.SetToolTip(this.GdocEventEngineStatusComboBox, this.GdocEventEngineStatusComboBox.Text);
            }
            else
            {
                this.GdocEventEngineToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the GdocEventEngineWorkOrderTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineWorkOrderTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (this.GdocEventEngineTypeComboBox.Text.Length > 20)
            {
                this.GdocEventEngineToolTip.RemoveAll();
                this.GdocEventEngineToolTip.SetToolTip(this.GdocEventEngineTypeComboBox, this.GdocEventEngineTypeComboBox.Text);
            }
            else
            {
                this.GdocEventEngineToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the GdocEventEngineWorkOrderTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineWorkOrderTextBox_MouseHover(object sender, EventArgs e)
        {
            if (this.GdocEventEngineWorkOrderTextBox.Text.Trim().Length > 4)
            {
                this.GdocEventEngineToolTip.SetToolTip(this.GdocEventEngineWorkOrderTextBox, this.GdocEventEngineWorkOrderTextBox.Text);
            }
            else
            {
                this.GdocEventEngineToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the 1 event of the GDocEventEngineDataGridView_MouseMove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouseCursorPoint = new Point(e.X, e.Y);
            UIElement element = ((UltraGrid)sender).DisplayLayout.UIElement.ElementFromPoint(mouseCursorPoint);
            if (element != null)
            {
                UltraGridColumn col = element.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;
                UltraGridRow row = element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
                if (col != null && row != null)
                {
                    if (row.Index == -1)
                    {
                        this.GdocEventEngineToolTip.RemoveAll();
                    }

                    if (col.Index == 2 && row.Index >= 0)
                    {
                        this.GdocEventEngineToolTip.SetToolTip(this.GDocEventEngineDataGridView, this.GDocEventEngineDataGridView.Rows[row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Value.ToString());
                    }
                    else if (col.Index == 14 && row.Index >= 0)
                    {
                        this.GdocEventEngineToolTip.SetToolTip(this.GDocEventEngineDataGridView, this.GDocEventEngineDataGridView.Rows[row.Index].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Value.ToString());
                    }
                    else
                    {
                        this.GdocEventEngineToolTip.RemoveAll();
                    }
                }
                else
                {
                    this.GdocEventEngineToolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the GdocEventEngineAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.featureId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.featureId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////this.systemId = 2001;
                ////this.Cursor = Cursors.WaitCursor;
                ////Hashtable eventEngineDetails = new Hashtable();
                ////eventEngineDetails.Add("FeatureID", this.featureId);
                ////eventEngineDetails.Add("SystemID", this.systemId);
                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(800191, TerraScan.Common.Reports.Report.ReportType.Preview, eventEngineDetails);
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

        #endregion

        #region   Method

        /// <summary>
        /// Checks the mandatory field.
        /// </summary>
        /// <returns> True if all mandatoryFields Are Filed  else false
        /// </returns>
        private bool CheckMandatoryField()
        {
            if (!string.IsNullOrEmpty(this.GDocEventEngineDateTextBox.Text.Trim()) && this.GdocEventEngineTypeComboBox.SelectedIndex > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the new mode.
        /// </summary>
        private void SetNewMode()
        {
            try
            {
                if (this.typeCount > 1)
                {
                    this.GdocEventEngineTypeComboBox.SelectedIndex = 0;
                    this.GDocEventEngineDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                }

                if (this.statusCount > 0)
                {
                    this.GdocEventEngineStatusComboBox.SelectedIndex = 0;
                    this.SetStatusColor(this.GdocEventEngineStatusComboBox.SelectedValue.ToString());
                }

                this.GdocEventEngineWorkOrderTextBox.Text = string.Empty;

                this.GdocEventEngineCompleteCheckBox.Checked = true;
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
        /// Creates the gdoc event engine new row.
        /// </summary>
        private void CreateGdocEventEngineNewRow()
        {
            if (this.PermissionFiled.newPermission)
            {
                try
                {
                    if (this.CheckMandatoryField())
                    {
                        this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.Clear();
                        if (string.Equals(this.currentRowLevel, "-1"))
                        {
                            this.currentParentEventId = "-1";
                        }
                        else if (string.Equals(this.currentRowLevel, "1"))
                        {
                            this.currentParentEventId = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Text;
                        }
                        else if (string.Equals(this.currentRowLevel, "2"))
                        {
                            this.currentParentEventId = this.GDocEventEngineDataGridView.Rows[this.currentRowSelected].Cells[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Text;
                        }

                        DataRow gdocEventEngineNewRow = this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.NewRow();
                        if (string.Equals(this.currentParentEventId, "-1"))
                        {
                            gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.EventIDColumn.ColumnName] = DBNull.Value;
                        }
                        else
                        {
                            gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.EventIDColumn.ColumnName] = this.currentParentEventId;
                        }

                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.EventTypeColumn.ColumnName] = this.GdocEventEngineTypeComboBox.SelectedValue.ToString();
                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.StatusColumn.ColumnName] = this.GdocEventEngineStatusComboBox.SelectedValue.ToString();
                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.CompleteColumn.ColumnName] = this.GdocEventEngineCompleteCheckBox.Checked;
                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.DateColumn.ColumnName] = this.GDocEventEngineDateTextBox.Text.Trim();
                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.WorkOrderColumn.ColumnName] = this.GdocEventEngineWorkOrderTextBox.Text.Trim();
                        gdocEventEngineNewRow[this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.FeatureIDColumn.ColumnName] = this.featureId;
                        this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable.Rows.Add(gdocEventEngineNewRow);
                        string xmlString = TerraScanCommon.GetXmlString(this.gdocF8001InsertXMLData.GDocF8001InsertXMLTable);
                        this.newEventID = this.form8001Control.WorkItem.InsertGDocEventEngineData(xmlString, TerraScan.Common.TerraScanCommon.UserId);
                        if (this.newEventID == -1)
                        {
                            this.saveStatus = false;
                        }
                        else
                        {
                            this.saveStatus = true;
                            this.dataChangeStatus = false;
                        }
                    }
                    else
                    {
                        this.saveStatus = false;
                        //// this.GDocEventEngineDateTextBox.Focus(); 
                        MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception e1)
                {
                    this.saveStatus = false;
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Finds the show id.
        /// </summary>
        /// <param name="findRowId">The find row id.</param>
        private void FindShowId(string findRowId)
        {
            try
            {
                int rowId;
                rowId = this.eventEngineSource.Find("EventID", findRowId);
                if (rowId >= 0)
                {
                    this.ShowEventForm(this.eventEngineData.GDocEventEngineDataTable.Rows[rowId][this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].ToString(), this.eventEngineData.GDocEventEngineDataTable.Rows[rowId][this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].ToString());
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

        /// <summary>
        /// Shows the attachment calender in particular location.
        /// </summary>
        private void ShowAttachmentCalender()
        {
            this.GDocEventEngineCalenderControl.Visible = true;
            this.GDocEventEngineCalenderControl.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.GDocEventEngineCalenderControl.Left = this.GDocEventEngineHeaderPanel.Left + this.GdocEventEngineDate.Left + this.GdocEventEngineDateImage.Left + this.GdocEventEngineDateImage.Width;
            this.GDocEventEngineCalenderControl.Top = this.GDocEventEngineHeaderPanel.Top + this.GdocEventEngineDate.Top + this.GdocEventEngineDateImage.Top;
            this.GDocEventEngineCalenderControl.Tag = this.GdocEventEngineDateImage.Tag;
            this.GDocEventEngineCalenderControl.Focus();

            if (!string.IsNullOrEmpty(this.GDocEventEngineDateTextBox.Text))
            {
                this.GDocEventEngineCalenderControl.SetDate(Convert.ToDateTime(this.GDocEventEngineDateTextBox.Text));
            }
        }

        /// <summary>
        /// Loads the event type combo box.
        /// </summary>
        private void LoadEventTypeAndStatusComboBox()
        {
            try
            {
                this.eventEngineTypeStatusData = this.form8001Control.WorkItem.ListEventTypeStatusDetails(this.featureClassId);

                DataRow dr = this.eventEngineTypeStatusData.ListEventEngineTypeTable.NewRow();
                dr[this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeIDColumn.ColumnName] = 0;
                dr[this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeColumn.ColumnName] = "<Select one>";
                this.eventEngineTypeStatusData.ListEventEngineTypeTable.Rows.Add(dr);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            this.statusCount = this.eventEngineTypeStatusData.ListEventStatusTypeTable.Rows.Count;
            this.typeCount = this.eventEngineTypeStatusData.ListEventEngineTypeTable.Rows.Count;
            DataView orderView = new DataView(this.eventEngineTypeStatusData.ListEventEngineTypeTable);
            orderView.Sort = "EventType ASC";

            this.GdocEventEngineTypeComboBox.DataSource = orderView;
            this.GdocEventEngineTypeComboBox.ValueMember = this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeIDColumn.ColumnName;
            this.GdocEventEngineTypeComboBox.DisplayMember = this.eventEngineTypeStatusData.ListEventEngineTypeTable.EventTypeColumn.ColumnName;
            this.GdocEventEngineStatusComboBox.DataSource = this.eventEngineTypeStatusData.ListEventStatusTypeTable;
            this.GdocEventEngineStatusComboBox.ValueMember = this.eventEngineTypeStatusData.ListEventStatusTypeTable.StatusIDColumn.ColumnName;
            this.GdocEventEngineStatusComboBox.DisplayMember = this.eventEngineTypeStatusData.ListEventStatusTypeTable.StatusColumn.ColumnName;
            if (this.GdocEventEngineStatusComboBox.SelectedValue != null)
            {
                this.SetStatusColor(this.GdocEventEngineStatusComboBox.SelectedValue.ToString());
            }

            this.GdocEventEngineTypeComboBox.Focus();
        }

        /// <summary>
        /// Loads the event engine data.
        /// </summary>
        private void LoadEventEngineData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.eventEngineData = this.form8001Control.WorkItem.LoadEventEngineData(this.featureClassId, this.featureId);
                this.GDocEventEngineDataGridView.DataSource = this.eventEngineData.GDocEventEngineDataTable;
                this.eventEngineSource.DataSource = this.eventEngineData.GDocEventEngineDataTable;
                this.totalDataCount = this.eventEngineData.GDocEventEngineDataTable.Rows.Count;
                if (this.totalDataCount < 20)
                {
                    this.GDocEventEngineDataGridView.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToLastItem;
                }
                else
                {
                    this.GDocEventEngineDataGridView.DisplayLayout.ScrollBounds = ScrollBounds.ScrollToFill;
                }

                this.CustomiseDataGrid();
                this.GdocEventEngineAuditLink.Text = this.auditLinkkText + " " + this.featureId;
                //// Load Type and status Combo Box
                this.LoadEventTypeAndStatusComboBox();
                if (this.statusCount == 0 && this.typeCount == 1)
                {
                    this.Disablecontrol(false);
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

        /// <summary>
        /// Disablecontrols the specified status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void Disablecontrol(bool status)
        {
            this.GDocEventEngineDataGridView.Enabled = status;
            this.GdocEventEngineStatusComboBox.Enabled = status;
            this.GdocEventEngineTypeComboBox.Enabled = status;
            this.GdocEventEngineCompleteCheckBox.Enabled = status;
            this.GdocEventEngineWorkOrderTextBox.Enabled = status;
            this.GdocEventEngineDate.Enabled = status;
            this.GdocEventEngineAuditLink.Text = string.Empty;
            this.GdocEventEngineAuditLink.Enabled = status;
            this.GdocEventEngineWorkOrderPictureBox.Enabled = status;
            this.GdocEventEngineDateImage.Enabled = status;
            this.GdocEventEngineRowAddLabel.Enabled = status;
        }

        /// <summary>
        /// Permissions the disable.
        /// </summary>
        private void PermissionDisable()
        {
            this.GdocEventEngineStatusComboBox.Enabled = false;
            this.GdocEventEngineTypeComboBox.Enabled = false;
            this.GdocEventEngineCompleteCheckBox.Enabled = false;
            this.GdocEventEngineWorkOrderTextBox.Enabled = false;
            this.GdocEventEngineDate.Enabled = false;
            this.GdocEventEngineRowAddLabel.Enabled = false;
            this.GDocEventEngineDataGridView.Focus();
            this.GdocEventEngineWorkOrderPictureBox.Enabled = false;
            this.GdocEventEngineDateImage.Enabled = false;
        }

        /// <summary>
        /// Shows the work order form.
        /// </summary>
        private void ShowWorkOrderForm()
        {
            try
            {
                string selectedWorkOrder;
                Form workOrderF8002 = new Form();
                object[] optionalParameter = new object[] { this.featureClassId };
                workOrderF8002 = this.form8001Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(8002, optionalParameter, this.form8001Control.WorkItem);
                if (workOrderF8002 != null)
                {
                    DialogResult dialogResult = workOrderF8002.ShowDialog();
                    if (dialogResult != DialogResult.Ignore && dialogResult != DialogResult.Cancel)
                    {
                        selectedWorkOrder = TerraScanCommon.GetValue(workOrderF8002, SharedFunctions.GetResourceString("F8001WorkOrder"));
                        this.GdocEventEngineWorkOrderTextBox.Text = selectedWorkOrder;
                        this.dataChangeStatus = true;
                    }
                    else
                    {
                        this.GdocEventEngineWorkOrderTextBox.Text = string.Empty;
                    }

                    this.GdocEventEngineWorkOrderTextBox.Focus();
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

        /// <summary>
        /// Customises the data grid.
        /// </summary>
        private void CustomiseDataGrid()
        {
            this.eventRowNo = 0;
            try
            {
                for (int eventrow = 0; eventrow < this.GDocEventEngineDataGridView.Rows.Count; eventrow++)
                {
                    ////this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Appearance.Image = this.EventEngineRowHeader.Images[0];
                    if (string.Equals(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString(), "1"))
                    {
                        this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Appearance.Image = this.EventEngineParentImage.Images[0];

                        //// ((Button)(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[0].Appearance.Image)).Click += new EventHandler(F8001_Click);
                        /////    this.gDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[0].Appearance.Image = this.EventEngineSChildImage.Images[0];
                    }
                    else if (string.Equals(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString(), "2"))
                    {
                        ////PictureBox tempPictureBox = new PictureBox();
                        ////tempPictureBox.Image = this.EventEngineFChildImage.Images[0];
                        //////this.gDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[0].Appearance.Image = tempPictureBox;
                        this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Appearance.Image = this.EventEngineFChildImage.Images[0];
                    }
                    else if (string.Equals(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Value.ToString(), "3"))
                    {
                        this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Appearance.Image = this.EventEngineSChildImage.Images[0];
                    }

                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.TempColorColumn.ColumnName].Appearance.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromString(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells[this.eventEngineData.GDocEventEngineDataTable.StatusColorColumn.ColumnName].Value.ToString());
                    if (Convert.ToDateTime(this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["EventDate"].Value.ToString()) > DateTime.Now)
                    {
                        this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["EventDate"].Appearance.BackColor = Color.FromArgb(252, 215, 116);
                    }

                    this.GDocEventEngineDataGridView.Rows[this.eventRowNo].Cells["EventDate"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
                    ////this.GDocEventEngineDataGridView.DisplayLayout.Bands[0].Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                    this.eventRowNo = this.eventRowNo + 1;
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

            Infragistics.Win.UltraWinGrid.BandsCollection bands = this.GDocEventEngineDataGridView.DisplayLayout.Bands;

            for (int j = 0; j < bands.Count; j++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridBand band = bands[j];
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.AddRowColumn.ColumnName].Width = 25;
                ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 277;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.TempColorColumn.ColumnName].Width = 26;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.StatusColumn.ColumnName].Width = 107;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventDateColumn.ColumnName].Width = 105;
                ////band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Width = 96;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.IsCompleteColumn.ColumnName].Width = 67;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventNumberColumn.ColumnName].Width = 73;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.Child2Column.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.Child1Column.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.ParentColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.StatusColorColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.levelsColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeIDColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventIDColumn.ColumnName].Hidden = true;
                band.Columns[this.eventEngineData.GDocEventEngineDataTable.IsWorkOrderColumn.ColumnName].Hidden = true;

                ////Code Added By Shiva for Change Request Sprint 22
                if (this.eventEngineData.GDocEventEngineDataTable.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(this.eventEngineData.GDocEventEngineDataTable.Rows[0][this.eventEngineData.GDocEventEngineDataTable.IsWorkOrderColumn.ColumnName]))
                    {
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 277;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Width = 96;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Hidden = false;
                        this.GdocEventEngineWorkOrderPanel.Visible = true;
                        this.EventTypePanel.Size = new System.Drawing.Size(281, 30);
                        this.GdocEventEngineTypeComboBox.Size = new System.Drawing.Size(271, 24);
                        this.StatusColorLabel.Location = new System.Drawing.Point(302, 0);
                        this.StatusPanel.Location = new System.Drawing.Point(326, 0);
                        this.GdocEventEngineDate.Location = new System.Drawing.Point(435, 0);
                        this.GdocEventEngineCompletePanel.Location = new System.Drawing.Point(540, 0);
                    }
                    else
                    {
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 373;
                        band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Hidden = true;
                        this.GdocEventEngineWorkOrderPanel.Visible = false;
                        this.EventTypePanel.Size = new System.Drawing.Size(377, 30);
                        this.GdocEventEngineTypeComboBox.Size = new System.Drawing.Size(367, 24);
                        this.StatusColorLabel.Location = new System.Drawing.Point(398, 0);
                        this.StatusPanel.Location = new System.Drawing.Point(422, 0);
                        this.GdocEventEngineDate.Location = new System.Drawing.Point(531, 0);
                        this.GdocEventEngineCompletePanel.Location = new System.Drawing.Point(636, 0);
                    }
                }
                else
                {
                    band.Columns[this.eventEngineData.GDocEventEngineDataTable.EventTypeColumn.ColumnName].Width = 277;
                    band.Columns[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Width = 96;
                }
                //// Till Here
            }

            this.GDocEventEngineDataGridView.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ExtendFirstColumn;
            ////this.GDocEventEngineDataGridView.DisplayLayout.Override.DefaultColWidth = 2400;
        }

        /// <summary>
        /// Shows the event form.
        /// </summary>
        /// <param name="eventTypeId">The event type id.</param>
        /// <param name="eventId">The event id.</param>
        private void ShowEventForm(string eventTypeId, string eventId)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                int shfrmId;
                if (!string.IsNullOrEmpty(eventTypeId))
                {
                    shfrmId = Convert.ToInt32(eventTypeId);
                    formInfo = TerraScanCommon.GetFormInfo(shfrmId);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = eventId;
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

        /// <summary>
        /// Works the order show form.
        /// </summary>
        /// <param name="workOrderId">The work order id.</param>
        private void WorkOrderShowForm(string workOrderId)
        {
            try
            {
                Form workOrderF8010 = new Form();
                object[] optionalParameter = new object[] { workOrderId };
                workOrderF8010 = this.form8001Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(8010, optionalParameter, this.form8001Control.WorkItem);
                if (workOrderF8010 != null)
                {
                    workOrderF8010.ShowDialog();
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

        /// <summary>
        /// Sets the gdoc event header.
        /// </summary>
        private void SetGdocEventHeader()
        {
            //// this.GdocEventEngineHeaderColorLabel.b
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.eventEngineData = this.form8001Control.WorkItem.GetEventEngineDataHeader(this.featureClassId, this.featureId);
                if (this.eventEngineData.EventEngineHeaderTable.Rows.Count > 0)
                {
                    this.GdocEventEngineHeaderColorLabel.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromInvariantString(this.eventEngineData.EventEngineHeaderTable.Rows[0][this.eventEngineData.EventEngineHeaderTable.SystemColorColumn].ToString());
                    ////this.GdocEventEngineHeaderColorLabel.BackColor = System.Drawing.Color.FromArgb(this.eventEngineData.EventEngineHeaderTable.Rows[0][this.eventEngineData.EventEngineHeaderTable.SystemColorColumn].ToString());

                    this.EventHeaderIdLable.Text = this.eventEngineData.EventEngineHeaderTable.Rows[0][this.eventEngineData.EventEngineHeaderTable.FeatureClassLabelColumn].ToString();
                    this.GdocEventPrefixIdLabel.Text = this.eventEngineData.EventEngineHeaderTable.Rows[0][this.eventEngineData.EventEngineHeaderTable.FeatureClassIDColumn].ToString();
                    if (string.IsNullOrEmpty(this.eventEngineData.EventEngineHeaderTable.Rows[0][this.eventEngineData.EventEngineHeaderTable.FeatureClassIDColumn].ToString()))
                    {
                        this.GdocLableSeperator.Visible = false;
                        this.GdocEventPrefixIdLabel.TextAlign = ContentAlignment.MiddleRight;
                        this.GdocEventPrefixIdLabel.Text = this.eventEngineData.EventEngineHeaderTable.Rows[0][this.eventEngineData.EventEngineHeaderTable.FeatureClassLabelColumn].ToString();
                        this.GdocEventPrefixIdLabel.ForeColor = Color.White;
                        this.EventHeaderIdLable.Text = string.Empty;
                    }
                    else
                    {
                        this.GdocEventPrefixIdLabel.ForeColor = Color.Tan;
                        this.GdocLableSeperator.Visible = true;
                    }
                    //// this.gDocEventEngineData.EventEngineHeaderTable.Rows[0][this.gDocEventEngineData.EventEngineHeaderTable.FeatureClassIDColumn].ToString();
                    this.EventHeaderDescLable.Text = this.eventEngineData.EventEngineHeaderTable.Rows[0][this.eventEngineData.EventEngineHeaderTable.FeatureClassColumn].ToString() + " Events";
                }
                else
                {
                    ////this.GdocEventEngineHeaderColorLabel.BackColor = 
                    this.EventHeaderIdLable.Text = string.Empty;
                    this.GdocEventPrefixIdLabel.Text = string.Empty;
                    this.GdocLableSeperator.Visible = false;
                    //// this.gDocEventEngineData.EventEngineHeaderTable.Rows[0][this.gDocEventEngineData.EventEngineHeaderTable.FeatureClassIDColumn].ToString();
                    this.EventHeaderDescLable.Text = string.Empty;
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

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form8001Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form8001Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form8001Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form8001Control.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.GdocPrintHeaderWorkSpace.Show(this.form8001Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.GdocPrintHeaderWorkSpace.Show(this.form8001Control.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
                this.reportActionSmartPart = (ReportActionSmartPart)this.form8001Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart);
            }

            this.reportActionSmartPart.DetailsButtonVisible = false;
            this.formLabelInfo[0] = "Event Engine";
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Sets the color of the status.
        /// </summary>
        /// <param name="selectedStatus">The selected status.</param>
        private void SetStatusColor(string selectedStatus)
        {
            string statusId = "StatusID =" + selectedStatus;
            DataRow[] statusRow;
            statusRow = this.eventEngineTypeStatusData.ListEventStatusTypeTable.Select(statusId);
            if (statusRow.Length > 0)
            {
                this.StatusColorLabel.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromInvariantString(statusRow[0][2].ToString());
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>IF No Update Operation in Progress then True else its False</returns>
        private bool CheckPageStatus()
        {
            bool pageSatus = false;

            if (this.dataChangeStatus && this.PermissionFiled.newPermission)
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            this.CreateGdocEventEngineNewRow();
                            pageSatus = this.saveStatus;
                            break;
                        }

                    case DialogResult.No:
                        {
                            pageSatus = true;
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            pageSatus = false;
                            break;
                        }
                } ///// End Case
            }
            else
            {
                pageSatus = true;
            }

            return pageSatus;
        }

        #endregion

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the GdocEventEngineTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.dataChangeStatus = true;
        }

        /// <summary>
        /// Handles the KeyPress event of the GDocEventEngineDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(this.GDocEventEngineDateTextBox.Text.Trim()))
                            {
                                this.eventEngineValidDate.Value = DateTime.Parse(this.GDocEventEngineDateTextBox.Text.Trim());
                                ////this.EventEngineKeyEnter(e);
                                return;
                            }
                            else
                            {
                                ////this.EventEngineKeyEnter(e);
                            }
                        }
                        catch
                        {
                            this.GDocEventEngineDateTextBox.Text = string.Empty;
                        }

                        break;
                    }
            }

            this.dataChangeStatus = true;
        }

        /// <summary>
        /// Handles the KeyPress event of the GdocEventEngineWorkOrderTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineWorkOrderTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.dataChangeStatus = true;
            ////this.EventEngineKeyEnter(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the GdocEventEngineTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineTypeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////this.EventEngineKeyEnter(e);
        }

        /// <summary>
        /// Gs the doc key enter.
        /// </summary>
        /// <param name="e1">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EventEngineKeyEnter(KeyPressEventArgs e1)
        {
            switch (e1.KeyChar)
            {
                case (char)13:
                    {
                        this.SaveEventEngine();
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the GdocEventEngineStatusComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineStatusComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////this.EventEngineKeyEnter(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the GdocEventEngineCompleteCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineCompleteCheckBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////this.EventEngineKeyEnter(e);
        }

        /// <summary>
        /// Handles the KeyPress event of the GDocEventEngineDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        this.invalidKey = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the GDocEventEngineDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GDocEventEngineDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 37:
                    {
                        this.invalidKey = true;
                        break;
                    }

                case 38:
                    {
                        this.invalidKey = true;
                        break;
                    }

                case 39:
                    {
                        this.invalidKey = true;
                        break;
                    }

                case 40:
                    {
                        this.invalidKey = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the Click event of the GdocEventEngineCompleteCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GdocEventEngineCompleteCheckBox_Click(object sender, EventArgs e)
        {
            this.dataChangeStatus = true;
        }

        ////private void SetForeColor(int SelectedrowID)
        ////{
        ////    ////for (int colId = 0; colId <= this.GDocEventEngineDataGridView.Rows.Count; colId++)
        ////    ////{
        ////    ////    if (SelectedrowID == colId)
        ////    ////    {
        ////    ////        this.GDocEventEngineDataGridView.DisplayLayout.Override.SelectedCellAppearance.ForeColor = Color.Green;
        ////    ////        this.GDocEventEngineDataGridView.Rows[SelectedrowID].Cells[2].Appearance.ForeColor    = Color.Red;
        ////    ////        this.GDocEventEngineDataGridView.Rows[SelectedrowID].Cells[2].Appearance.ForeColor = Color.Blue;
        ////    ////        this.GDocEventEngineDataGridView.Rows[SelectedrowID].Cells[2].Appearance.ForeColor = Color.Blue;
        ////    ////                                ///this.GDocEventEngineDataGridView.Rows[SelectedrowID].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Appearance.ForeColor = Color.Green ;
        ////    ////    }
        ////    ////    else
        ////    ////    {
        ////    ////        this.GDocEventEngineDataGridView.Rows[SelectedrowID].Cells[this.eventEngineData.GDocEventEngineDataTable.WOIDColumn.ColumnName].Appearance.ForeColor = Color.Blue;

        ////    ////    }
        ////    ////}
        ////}

        /// <summary>
        /// Saves the event engine.
        /// </summary>
        private void SaveEventEngine()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.currentRowLevel = "-1";
                this.CreateGdocEventEngineNewRow();
                if (this.saveStatus)
                {
                    this.LoadEventEngineData();
                    if (this.newEventID > 0)
                    {
                        this.FindShowId(this.newEventID.ToString());
                        this.SetNewMode();
                    }
                    else
                    {
                        this.FindShowId("0");
                    }

                    this.GdocEventEngineTypeComboBox.Focus();
                }
                else if (this.newEventID == -1)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("WorkOrder") + this.GdocEventEngineWorkOrderTextBox.Text + SharedFunctions.GetResourceString("WorkOrderDoesnotExist"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("WorkOrderValidation")), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
