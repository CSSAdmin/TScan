//--------------------------------------------------------------------------------------------
// <copyright file="F9008.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9008.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  23 Jan 07       KUPPUSAMY.B         Created
//*********************************************************************************/

namespace D9000
{
    #region NameSpace
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using TerraScan.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Common.Reports;
    using System.Reflection;
    using System.Drawing.Printing;
    #endregion NameSpace

    /// <summary>
    /// F9008 UserControl class
    /// </summary>
    [SmartPart]
    public partial class F9008 : BaseSmartPart
    {
        #region PrivateMembers

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// userId value
        /// </summary>
        private int userId;

        /// <summary>
        /// Instance for F9008Controller
        /// </summary>
        private F9008Controller form9008Control;

        /// <summary>
        /// ReportDetailsData
        /// </summary>
        private F9008ReportDetailsData form9008ReportDetailsData;

        /// <summary>
        /// report Action Smart part
        /// </summary>
        private ReportActionSmartPart reportActionSmartPart;

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int redColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private string sectionIndicatorTabText;

        /// <summary>
        /// row selected for printing
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// PrintDocument for printing
        /// </summary>
        private PrintDocument printDoc = new PrintDocument();
        
        #endregion PrivateMembers

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the F9008 class.
        /// </summary>
        public F9008()
        {            
            this.InitializeComponent();
            ////this.formMasterPermissionEdit = permissionEdit ;
            ////this.masterFormNo = masterform ;            
            this.ReportDetailspicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReportDetailspicturebox.Height, this.ReportDetailspicturebox.Width, "Report Printer Assignments", 28, 81, 128);
            this.form9008ReportDetailsData = new F9008ReportDetailsData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9008"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F9008(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            ////this.formMasterPermissionEdit = permissionEdit;
            ////this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ReportDetailspicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReportDetailspicturebox.Height, this.ReportDetailspicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            this.form9008ReportDetailsData = new F9008ReportDetailsData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15101"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The featureclass id</param>
        public F9008(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            ////this.formMasterPermissionEdit = permissionEdit;
            ////this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ReportDetailspicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReportDetailspicturebox.Height, this.ReportDetailspicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            this.form9008ReportDetailsData = new F9008ReportDetailsData();
        }        
        #endregion Constructor                
        
        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Properties
        /// <summary>
        /// Gets or sets the form9008 control.
        /// </summary>
        /// <value>The form9008 control.</value>
        [CreateNew]
        public F9008Controller Form9008Control
        {
            get { return this.form9008Control as F9008Controller; }
            set { this.form9008Control = value; }
        }

        #endregion Properties

        #region Event Subscription

        #region Report

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
                TerraScanCommon.SetDataGridViewPosition(this.ReportDetailsDataGridView, this.selectedRow);
                this.Cursor = Cursors.WaitCursor;

                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("UserId", this.userId);
                ////ht.Add("ReportId", this.ReportDetailsDataGridView.SelectedRows[0].Cells[0].Value);               
                if (this.ReportDetailsDataGridView.SelectedRows.Count > 0)
                {
                    ////TerraScanCommon.ShowReport(Convert.ToInt32(this.ReportDetailsDataGridView.SelectedRows[0].Cells[0].Value), TerraScan.Common.Reports.Report.ReportType.Print, ht, this.ReportDetailsDataGridView.SelectedRows[0].Cells[2].Value.ToString());
                    TerraScanCommon.ShowReport(900801, TerraScan.Common.Reports.Report.ReportType.Print, ht, this.ReportDetailsDataGridView.SelectedRows[0].Cells[2].Value.ToString());
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
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                TerraScanCommon.SetDataGridViewPosition(this.ReportDetailsDataGridView, this.selectedRow);

                this.Cursor = Cursors.WaitCursor;

                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("UserId", this.userId);
                if (this.ReportDetailsDataGridView.SelectedRows.Count > 0)
                {
                    ////TerraScanCommon.ShowReport(Convert.ToInt32(this.ReportDetailsDataGridView.SelectedRows[0].Cells[0].Value), TerraScan.Common.Reports.Report.ReportType.Preview);
                    TerraScanCommon.ShowReport(900801, TerraScan.Common.Reports.Report.ReportType.Preview, ht);
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

        #endregion Report

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
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
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
            if (e.Data.Equals("F" + this.Tag.ToString()))
            {
                this.form9008Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }
        #endregion Event Subscription

        #region Protected methods
        #endregion Protected methods

        #region Events

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>        
        private void SaveButton_Click()
        {
            try
            {
                if (this.operationSmartPart.SaveButtonEnable == true)
                {
                    this.SaveReportdetails();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.LoadReportDetailsGrid();

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.ReportColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterSelectColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterClearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;                
                ////this.NextNumRecordsGridView.TabStop = true;                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F9008 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9008_Load(object sender, EventArgs e)
        {
            try
            {
                this.formheaderlabel.Text = "For User: " + TerraScanCommon.UserName;
                this.CustomizeReportDetailsGrid();
                this.LoadWorkSpaces();
                this.LoadReportDetailsGrid();

                if (this.ReportDetailsDataGridView.OriginalRowCount == 0)
                {
                    this.ReportDetailsDataGridView.Rows[0].Selected = false;
                    this.ReportDetailsDataGridView.RemoveDefaultSelection = true;
                }
                else
                {
                    this.ReportDetailsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    this.ReportDetailsDataGridView.RemoveDefaultSelection = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        
        
        /// <summary>
        /// Handles the ItemClicked event of the PrinterContextMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void PrinterContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.ReportDetailsDataGridView["Printer", this.selectedRow].Value = e.ClickedItem.Text;
                this.form9008ReportDetailsData.F9008GetReportDetails.Rows[this.selectedRow]["Printer"] = e.ClickedItem.Text;
                this.PrinterContextMenuStrip.Close();                
                TerraScanCommon.SetDataGridViewPosition(this.ReportDetailsDataGridView, this.selectedRow);
                this.SetEditRecord();
                this.operationSmartPart.RetrieveCancelButton.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ReportDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ReportDetailsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.ReportDetailsDataGridView.Columns["PrinterSelect"].Index)
                {
                    if (this.ReportDetailsDataGridView.Rows[e.RowIndex].Selected || this.ReportDetailsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterSelect".ToString()] as DataGridViewLinkCell).LinkColor = Color.White;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterSelect".ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterSelect".ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterSelect".ToString()] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterSelect".ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterSelect".ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }

                if (e.ColumnIndex == this.ReportDetailsDataGridView.Columns["PrinterClear"].Index)
                {
                    if (this.ReportDetailsDataGridView.Rows[e.RowIndex].Selected || this.ReportDetailsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterClear".ToString()] as DataGridViewLinkCell).LinkColor = Color.White;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterClear".ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterClear".ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterClear".ToString()] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterClear".ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (ReportDetailsDataGridView.Rows[e.RowIndex].Cells["PrinterClear".ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReportPrinterAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportPrinterAuditlinkLabel_Click(object sender, EventArgs e)
        {            
                try
                {  
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.Tag;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                    // Shows the report form.
                    // changed the parameter type from string to int
                    ////TerraScanCommon.ShowReport(900801, TerraScan.Common.Reports.Report.ReportType.Preview);
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
        /// Handles the RowEnter event of the ReportDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReportDetailsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.selectedRow = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowLeave event of the ReportDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReportDetailsDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TerraScanCommon.SetDataGridViewPosition(this.ReportDetailsDataGridView, this.selectedRow);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellMouseClick event of the ReportDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ReportDetailsDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ////try
            ////{

            ////    if (e.ColumnIndex == this.ReportDetailsDataGridView.Columns["PrinterSelect"].Index)
            ////    {
            ////        this.Cursor = Cursors.WaitCursor;
            ////        if ((e.RowIndex >= 0) && (e.ColumnIndex == 3))
            ////        {
            ////            String installedPrinters;
            ////            this.PrinterContextMenuStrip.Items.Clear();
            ////            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            ////            {
            ////                installedPrinters = PrinterSettings.InstalledPrinters[i];
            ////                this.PrinterContextMenuStrip.Items.Add(installedPrinters);
            ////            }

            ////            this.PrinterContextMenuStrip.Show(new Point(e.X ,e.Y));
            ////        }

            ////    }
            ////    else if (e.ColumnIndex == 5)
            ////    {
            ////        this.ReportDetailsDataGridView[3, e.RowIndex].Value = string.Empty;
            ////    }

            ////    this.selectedRow = e.RowIndex;
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            ////}
            ////finally
            ////{
            ////    this.Cursor = Cursors.Default;
            ////}
        }

        /// <summary>
        /// Handles the CellContentClick event of the ReportDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReportDetailsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.ReportDetailsDataGridView.Columns["PrinterSelect"].Index)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (e.RowIndex >= 0)
                    {
                        String installedPrinters;
                        this.PrinterContextMenuStrip.Items.Clear();
                        for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                        {
                            installedPrinters = PrinterSettings.InstalledPrinters[i];
                            this.PrinterContextMenuStrip.Items.Add(installedPrinters);
                        }

                        this.PrinterContextMenuStrip.Show(new Point(this.ReportDetailsGridViewPanel.Left + this.ReportDetailsDataGridView[0, e.RowIndex].Size.Width + this.ReportDetailsDataGridView[1, e.RowIndex].Size.Width + this.ReportDetailsDataGridView[2, e.RowIndex].Size.Width + this.ReportDetailsDataGridView.RowHeadersWidth + this.ReportDetailsDataGridView[e.ColumnIndex, e.RowIndex].ContentBounds.Right + this.ParentForm.Parent.Left, this.ReportDetailsGridViewPanel.Location.Y + (this.ReportDetailsDataGridView.RowTemplate.Height * (e.RowIndex - 1))));
                    }
                    ////{
                    ////    PrintDialog dlg = new PrintDialog();

                    ////    dlg.Document = printDoc;
                    ////    dlg.AllowSomePages = false;
                    ////    dlg.AllowPrintToFile = false;
                    ////    dlg.AllowCurrentPage = false;
                    ////    dlg.AllowSelection = false;
                    ////    dlg.UseEXDialog = false;

                    ////    if (dlg.ShowDialog() == DialogResult.OK)
                    ////    {
                    ////        //// printDoc.Print();
                    ////    }
                    ////}
                }
                else if (e.ColumnIndex == 5)
                {
                    if ((this.ReportDetailsDataGridView.Rows[e.RowIndex].Cells[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterColumn.ColumnName].Value != null) && (!string.IsNullOrEmpty(this.ReportDetailsDataGridView.Rows[e.RowIndex].Cells[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterColumn.ColumnName].Value.ToString())))
                    {
                        this.ReportDetailsDataGridView[3, e.RowIndex].Value = string.Empty;
                        this.form9008ReportDetailsData.F9008GetReportDetails.Rows[this.selectedRow]["Printer"] = string.Empty;
                        ////TerraScanCommon.SetDataGridViewPosition(this.ReportDetailsDataGridView, this.selectedRow);
                        this.ReportDetailsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.form9008ReportDetailsData.F9008GetReportDetails.AcceptChanges();
                        this.SetEditRecord();
                        this.operationSmartPart.RetrieveCancelButton.Focus();
                    }
                }

                this.selectedRow = e.RowIndex;
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
        /// Handles the MouseEnter event of the ReportDetailspicturebox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportDetailspicturebox_MouseEnter(object sender, EventArgs e)
        {
            ////this.ReportDetailsToolTip.SetToolTip(this.ReportDetailspicturebox, Utility.GetFormNameSpace(this.Name));
        }
        #endregion Events

        #region Private methods

        /// <summary>
        /// Saves the reportdetails.
        /// </summary>
        /// <returns>reportdetails</returns>
        private bool SaveReportdetails()
        {
                string printConfigValue = string.Empty;

                this.ReportDetailsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.form9008ReportDetailsData.F9008GetReportDetails.AcceptChanges();
                printConfigValue = TerraScanCommon.GetXmlString(this.form9008ReportDetailsData.F9008GetReportDetails);

                this.form9008Control.WorkItem.F9008_SaveReportDetails(TerraScanCommon.UserId, printConfigValue);
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.LoadReportDetailsGrid();

                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.ReportColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterSelectColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterClearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                ////this.NextNumRecordsGridView.TabStop = true;
                return true;            
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;

                    this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.ReportColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterSelectColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterClearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                    ////this.NextNumRecordsGridView.TabStop = true;
                }           
        }       

        /// <summary>
        /// Customizes the report details grid.
        /// </summary>
        private void CustomizeReportDetailsGrid()
        {
                this.ReportDetailsDataGridView.AllowUserToResizeColumns = false;
                this.ReportDetailsDataGridView.AllowUserToResizeRows = false;
                this.ReportDetailsDataGridView.AutoGenerateColumns = false;
                ////this.ReportDetailsDataGridView.StandardTab = true;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.ReportColumn.ColumnName].DataPropertyName = this.form9008ReportDetailsData.F9008GetReportDetails.ReportColumn.ColumnName;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.DescriptionColumn.ColumnName].DataPropertyName = this.form9008ReportDetailsData.F9008GetReportDetails.DescriptionColumn.ColumnName;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterColumn.ColumnName].DataPropertyName = this.form9008ReportDetailsData.F9008GetReportDetails.PrinterColumn.ColumnName;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterSelectColumn.ColumnName].DataPropertyName = this.form9008ReportDetailsData.F9008GetReportDetails.PrinterSelectColumn.ColumnName;
                this.ReportDetailsDataGridView.Columns[this.form9008ReportDetailsData.F9008GetReportDetails.PrinterClearColumn.ColumnName].DataPropertyName = this.form9008ReportDetailsData.F9008GetReportDetails.PrinterClearColumn.ColumnName;

                if (this.ReportDetailsDataGridView.OriginalRowCount == 0)
                {
                    this.ReportDetailsDataGridView.RemoveDefaultSelection = true;
                }
                else
                {
                    this.ReportDetailsDataGridView.RemoveDefaultSelection = false;
                }
        }        

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", "Report management", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveReportdetails();
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
        /// To Load Report Details Grid
        /// </summary>
        private void LoadReportDetailsGrid()
        {
                this.form9008ReportDetailsData = this.form9008Control.WorkItem.F9008GetReportDetails(TerraScanCommon.UserId);

                if (this.form9008ReportDetailsData.F9008GetReportDetails.Rows.Count > this.ReportDetailsDataGridView.NumRowsVisible)
                {
                    this.ReportPrinterGridVscrollBar.Visible = false;
                }
                else
                {
                    this.ReportPrinterGridVscrollBar.Visible = true;
                }

                this.ReportDetailsDataGridView.DataSource = this.form9008ReportDetailsData.F9008GetReportDetails;

                if (this.form9008ReportDetailsData.F9008GetReportDetails.Rows.Count > 0)
                {
                    this.ReportDetailsDataGridView.Rows[0].Selected = true;
                }
                else
                {
                    this.ReportDetailsDataGridView.Rows[0].Selected = false;
                    this.ReportDetailsDataGridView.RemoveDefaultSelection = true;
                }

                this.pageMode = TerraScanCommon.PageModeTypes.View;           
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
                if (this.form9008Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
                {
                    this.operationSmartPart = (OperationSmartPart)this.form9008Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                    this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
                }
                else
                {
                    this.operationSmartPart = (OperationSmartPart)this.Form9008Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                    this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
                }

                this.operationSmartPart.NewButtonVisible = false;
                this.operationSmartPart.DeleteButtonEnable = false;
                this.ParentForm.CancelButton = this.operationSmartPart.RetrieveCancelButton;

                if (this.form9008Control.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
                {
                    this.GdocPrintHeaderWorkSpace.Show(this.form9008Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
                }
                else
                {
                    this.GdocPrintHeaderWorkSpace.Show(this.form9008Control.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
                    this.reportActionSmartPart = (ReportActionSmartPart)this.form9008Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart);
                }

                this.reportActionSmartPart.DetailsButtonVisible = false;
                this.reportActionSmartPart.EmailButtonVisible = false;

                ////Load FormHeaderSmartPart to FormHeaderSmartPartdeckWorkspace
                if (this.Form9008Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.FormHeaderSmartPartdeckWorkspace.Show(this.Form9008Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.FormHeaderSmartPartdeckWorkspace.Show(this.Form9008Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }
                ////sets form header
                this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Report Printer Configuration", string.Empty }));            
        }
        #endregion Private methods
    }
}
