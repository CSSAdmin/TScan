//--------------------------------------------------------------------------------------------
// <copyright file="MultipleReports.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the CommentsForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 1 April 06		Guhan S		        Created
//*********************************************************************************/

namespace TerraScan.Common.Reports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using System.Configuration;
    using TerraScan.Common.Reports;
    using System.Drawing.Printing;

    /// <summary>
    /// Multiple Reports
    /// </summary>
    public partial class MultipleReports : Form
    {
        /// <summary>
        ///  Used to store ReportDetails
        /// </summary>
        private DataSet tmpDataSet = new DataSet();

        /// <summary>
        /// Just to get the printer Name if the UI is configured to have that
        /// </summary>
        private string printerName;

        /// <summary>
        ///  Used to store ReportDetails
        /// </summary>
        private Report.ReportDetails tmpReportDetails;

        /// <summary>
        ///  Used to Create Reports
        /// </summary>
        private Report report = new Report();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MultipleReports"/> class.
        /// </summary>
        public MultipleReports()
        {
           this.InitializeComponent();
           this.LoadReport();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MultipleReports"/> class.
        /// </summary>
        /// <param name="reportDetails">The report details.</param>
        /// <param name="reportDataSet">The report data set.</param>
        public MultipleReports(Report.ReportDetails reportDetails, DataSet reportDataSet, string printerName)
        {
            this.InitializeComponent();
            this.tmpDataSet = reportDataSet;
            this.printerName = printerName;            
            this.tmpReportDetails = reportDetails;
            this.FormIDLabel.Text = this.FormIDLabel.Text + " " + reportDetails.ReportName;
            this.LoadReport();
        }

        /// <summary>
        /// Loads the report DataGrie.
        /// </summary>
        private void LoadReport()
        {
            DataGridViewColumnCollection columns = this.ReportDataGridView.Columns;
            this.ReportDataGridView.AutoGenerateColumns = false;
            columns["Report"].DataPropertyName = "Report";
            columns["Report"].Visible = false;
            columns["ReportFile"].DataPropertyName = "ReportFile";
            columns["ReportFile"].Visible = false;
            columns["Description"].DataPropertyName = "Description";
            columns["OrderBy"].DataPropertyName = "OrderBy";
            columns["OrderBy"].Visible = false;
            columns["Printer"].DataPropertyName = "Printer";
            columns["Printer"].Visible = false;

            columns["Report"].DisplayIndex = 0;
            columns["ReportFile"].DisplayIndex = 1;
            columns["Description"].DisplayIndex = 2;
            columns["OrderBy"].DisplayIndex = 3;
            columns["Printer"].DisplayIndex = 4;
            this.ReportDataGridView.DataSource = this.tmpDataSet.Tables[0];
        }

        /// <summary>
        /// Handles the CellContentClick event of the ReportDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReportDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //// create instance for strcuture reports and passing details
                this.tmpReportDetails.ReportFile = this.ReportDataGridView.Rows[e.RowIndex].Cells["ReportFile"].Value.ToString();
                this.tmpReportDetails.ReportDescription = this.ReportDataGridView.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                this.report.OpenReport(this.tmpReportDetails, this.printerName);
                this.Close();
            }
            catch (SoapException ex)
            {
                //// TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ReportCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportCancelButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        /*
        public static void GenerateReport(string reportFile, string description)
        {
            try
            {
                //// create instance for strcuture reports and passing details
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                this.tmpReportDetails.FormId = this.tmpFormID;
                this.tmpReportDetails.KeyId = this.tmpKeyID;
                this.tmpReportDetails.Mode = this.tmpReportType;
                this.tmpReportDetails.ReportFile = this.ReportDataGridView.Rows[e.RowIndex].Cells["ReportFile"].Value.ToString();
                this.tmpReportDetails.ReportDescription = this.ReportDataGridView.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                Reports.ReportViewer tmpReportViwer = new Reports.ReportViewer(this.tmpReportDetails);
                switch (this.tmpReportType)
                {
                    case Reports.ReportType.Email:
                        {
                            tmpReportViwer.ShowReport();
                            break;
                        }

                    case Reports.ReportType.Print:
                        {
                            tmpReportViwer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                            //// Subscribe to render complete event so that the print dialog can be displayed
                            tmpReportViwer.reportViewerControl.RenderingComplete += new Microsoft.Reporting.WinForms.RenderingCompleteEventHandler(this.ReportViewerControl_RenderingComplete);

                            // Hide the current form and show the print dialog
                            tmpReportViwer.Height = 0;
                            tmpReportViwer.Width = 0;
                            tmpReportViwer.Visible = false;
                            tmpReportViwer.Opacity = 0;
                            tmpReportViwer.ShowInTaskbar = false;
                            //// Activate the form
                            tmpReportViwer.Show();

                            //// change the cursor to hour glass
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                            break;
                        }

                    case Reports.ReportType.Preview:
                        {
                            System.Diagnostics.Process proc = new System.Diagnostics.Process();
                            proc.EnableRaisingEvents = false;
                            proc.StartInfo.FileName = "iexplore";
                            string queryString;
                            queryString = "ReportType=Receipt";
                            queryString += "&ServerUrl=" + ConfigurationWrapper.ReportURL;
                            queryString += "&ServerPath=" + ConfigurationWrapper.ReportPath;
                            queryString += "&ReportFile=" + this.tmpReportDetails.ReportFile;
                            queryString += "&Description =" + this.tmpReportDetails.ReportDescription;
                            queryString += "&KeyValue=" + this.tmpReportDetails.KeyId;
                            proc.StartInfo.Arguments = ConfigurationWrapper.ReportViewerURL + "?" + queryString;
                            proc.Start();
                            break;
                        }

                    default:
                        {
                            tmpReportViwer.Show();
                            break;
                        }
                }

                this.Close();
            }
            catch (SoapException ex)
            {
                //// TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }*/
    }
}