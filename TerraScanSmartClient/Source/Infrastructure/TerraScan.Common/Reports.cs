//--------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Congruent">
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
// 29 April 06		Guhan S		        Created
//*********************************************************************************/
namespace TerraScan.Common.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Drawing;
    using System.IO;
    using TerraScan.Helper;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Collections;
    using PdfSharp.Pdf.Printing;
    using Microsoft.Win32;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using TerraScan.BusinessEntities;
    using Outlook = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// Report Class Used as Wrapper to Generate Reports Preview/ Email/ Print / AutoPrint
    /// </summary>
    public class Report
    {
        #region Private Variables

        /// <summary>
        /// used To renderedReport
        /// </summary>
        private byte[][] renderedReport;

        /// <summary>
        /// Delegate For Graphis
        /// </summary>
        private Graphics.EnumerateMetafileProc metaFileProcDelegate;

        /// <summary>
        /// current page Content
        /// </summary>
        private MemoryStream currentPageStream;

        /// <summary>
        /// metaFile
        /// </summary>
        private Metafile metaFile;

        /// <summary>
        ///  total number of pages
        /// </summary>
        private int numberOfPages;

        /// <summary>
        ///  current page number printing
        /// </summary>
        private int currentPrintingPage;

        /// <summary>
        ///  current page number for last page printing
        /// </summary>
        private int lastPrintingPage;

        /// <summary>
        ///  Config object
        /// </summary>
        private CommentsData getWaittime = new CommentsData();

        /// <summary>
        ///waittime
        /// </summary>
        private int waittime = 0;

        #endregion

        #region enum

        /// <summary>
        /// Enumerator sorttype
        /// </summary>
        public enum ReportType
        {
            /// <summary>
            /// Print 
            /// </summary>
            Print = 0,

            /// <summary>
            /// Preview
            /// </summary>
            Preview = 1,

            /// <summary>
            /// Email
            /// </summary>
            Email = 2,

            /// <summary>
            /// PrintDefault
            /// </summary>
            PrintDefault = 3
        }
        #endregion enum

        #region  for Enum ProgramType

        /// <summary>
        /// ProgramType
        /// </summary>
        public enum ReportFor
        {
            /// <summary>
            /// Empty inorder to avoid warnings.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// For StateMent = 1000.
            /// </summary>
            Statement = 1000,

            /// <summary>
            /// Receipt = 1020.
            /// </summary>
            Receipt = 1020
        }
        #endregion

        #region  Property for Print
        /// <summary>
        /// Gets or sets the rendered report.
        /// </summary>
        /// <value>The rendered report.</value>
        public byte[][] RenderedReport
        {
            get
            {
                return this.renderedReport;
            }

            set
            {
                this.renderedReport = value;
            }
        }
        #endregion

        #region Open Outlook
        /// <summary>
        /// Opens the outlook with default default email client.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="reportPath">The report path.</param>
        /// <param name="reportDetails">The report details.</param>
        public static void OpenOutlook(string fileName, string reportPath, ReportDetails reportDetails)
        {
            string reportFilePath = Createpdf(fileName, reportPath, reportDetails);
            ////Commente by Biju on 27/Oct/2009 to implement outlook integration when Email button is clicked.

            ////StringBuilder mailstr = new StringBuilder();
            ////mailstr.Append("mailto:");
            ////mailstr.Append("&Attach=\"\"" + reportFilePath + "\"\"");
            ////Process emailProcess = new Process();
            ////emailProcess.StartInfo.FileName = mailstr.ToString();
            ////emailProcess.StartInfo.UseShellExecute = true;
            ////emailProcess.StartInfo.RedirectStandardOutput = false;
            ////emailProcess.Start();
            ////emailProcess.Dispose();

            ////Added by Biju on 27/Oct/2009 to implement outlook integration when Email button is clicked
            try
            {
                Outlook.ApplicationClass outlookApp = new Outlook.ApplicationClass();

                Outlook.MailItem outlookMail = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                if (outlookMail == null)
                {
                    MessageBox.Show("Error occured while accessing Outlook. Please try again!", "TerraScan - Outlook Emailing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                outlookMail.Attachments.Add(reportFilePath, Outlook.OlAttachmentType.olEmbeddeditem, 1, "Terrascan");
                outlookApp.Inspectors.Add(outlookMail);
                if (outlookApp.Inspectors.Count > 0)
                {
                    outlookApp.Inspectors[outlookApp.Inspectors.Count].Activate();
                    outlookApp.Inspectors[outlookApp.Inspectors.Count].WindowState = Outlook.OlWindowState.olMaximized;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TerraScan - Outlook Emailing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ////till here
            
            
        }
        #endregion

        #region ViewReport
        /// <summary>
        /// Views the report.
        /// </summary>
        /// <param name="tmpReportDetails">The TMP report details.</param>
        public static void ViewReport(ReportDetails tmpReportDetails)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "iexplore";
            string queryString = "";
            queryString = ConfigurationWrapper.ReportPath;
            queryString += tmpReportDetails.ReportFile;
            if (tmpReportDetails.OptionalReportParameter != null)
            {
                IDictionaryEnumerator en = tmpReportDetails.OptionalReportParameter.GetEnumerator();
                while (en.MoveNext())
                {
                    queryString += "&" + en.Key.ToString() + "=" + en.Value.ToString();
                }
            }

            proc.StartInfo.Arguments = ConfigurationWrapper.ReportViewerURL + "?" + queryString + "&rs:Command=Render";
            proc.Start();
        }
        #endregion

        #region Createpdf

        /// <summary>
        /// Used to create PDF file to send as attachment.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="reportPath">The report path.</param>
        /// <param name="reportDetails">The report details.</param>
        /// <returns>returns filepath</returns>
        public static string Createpdf(string fileName, string reportPath, ReportDetails reportDetails)
        {
            byte[] result;
            string encoding;
            string mimetype;
            string[] streamids;
            IDictionaryEnumerator en = reportDetails.OptionalReportParameter.GetEnumerator();
            en.MoveNext();
            if (reportDetails.OptionalReportParameter != null)
            {
                result = WSHelper.GetRenderReport(reportPath, "PDF", "", out encoding, out mimetype, out streamids, en.Key.ToString(), en.Value.ToString());
            }
            else
            {
                result = WSHelper.GetRenderReport(reportPath, "PDF", "", out encoding, out mimetype, out streamids, "", "");
            }

            string presentDate = DateTime.Now.ToShortDateString();
            string presentTime = DateTime.Now.ToShortTimeString();
            string reportFilePath = reportDetails.ApplicationPathforPdf.ToString() + "\\" + fileName + "_" + presentDate.Replace("/", "") + "_" + presentTime.Replace(":", "").Replace("AM", "").Replace("PM", "") + ".pdf";
            if (System.IO.File.Exists(reportFilePath))
            {
                System.IO.File.Delete(reportFilePath);
            }

            System.IO.FileStream outputFile = new System.IO.FileStream(reportFilePath, System.IO.FileMode.Create);
            outputFile.Write(result, 0, result.Length - 1);
            outputFile.Close();
            return reportFilePath;
        }
        #endregion

        #region Open Report
        /// <summary>
        /// Method to Open the Report
        /// </summary>
        /// <param name="tmpReportDetails">tmpReportDetails</param>
        /// <param name="printerName">printerName</param>
        public void OpenReport(ReportDetails tmpReportDetails, string printerName)
        {
            switch (tmpReportDetails.Mode)
            {
                case Reports.Report.ReportType.Email:
                    {
                        RegistryKey regClasses = Registry.ClassesRoot;
                        RegistryKey regOutlook = regClasses.OpenSubKey("Outlook.Application");
                        if (regOutlook != null)
                        {
                            Reports.Report.OpenOutlook(
                                tmpReportDetails.ReportFile,
                                ConfigurationWrapper.ReportPath + tmpReportDetails.ReportFile,
                                tmpReportDetails);
                        }
                        else
                        {
                            MessageBox.Show("This feature is not available because outlook is not installed in the machine.","TerraScan",MessageBoxButtons.OK ,MessageBoxIcon.Stop );
                        }
                        break;
                    }

                case Reports.Report.ReportType.Print:
                    {
                        PrintDocument prtdoc = new PrintDocument();
                        Reports.Report printReport = new Report();
                        if (printerName != null)
                        {
                            printReport.PrintReport(printerName, ConfigurationWrapper.ReportPath + tmpReportDetails.ReportFile, tmpReportDetails);
                        }
                        else
                        {
                            printReport.PrintReport(prtdoc.PrinterSettings.PrinterName, ConfigurationWrapper.ReportPath + tmpReportDetails.ReportFile, tmpReportDetails);
                        }

                        break;
                    }

                case Reports.Report.ReportType.Preview:
                    {
                        Reports.Report.ViewReport(tmpReportDetails);
                        break;
                    }

                case Reports.Report.ReportType.PrintDefault:
                    {
                        PrintDocument prtdoc = new PrintDocument();
                        Reports.Report printReport = new Report();
                        if (printerName != null)
                        {
                            printReport.PrintReport(printerName, ConfigurationWrapper.ReportPath + tmpReportDetails.ReportFile, tmpReportDetails);
                        }
                        else
                        {
                            printReport.PrintReport(prtdoc.PrinterSettings.PrinterName, ConfigurationWrapper.ReportPath + tmpReportDetails.ReportFile, tmpReportDetails);
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region Code for Direct Print

        /// <summary>
        /// Prints the report.
        /// </summary>
        /// <param name="printerName">Name of the printer.</param>
        /// <param name="reportPath">The report path.</param>
        /// <param name="tmpReportDetails">The TMP report details.</param>
        /// <returns>true if morethan 1 pages else false</returns>
        public bool PrintReport(string printerName, string reportPath, ReportDetails tmpReportDetails)
        {
            ////Added by Biju on 26-Nov-07 to fix the report printing format issue - START
            ////try
            ////{
            ////    string reportFilePath = Createpdf(tmpReportDetails.ReportFile, reportPath, tmpReportDetails);

            ////    RegistryPermission fread = new RegistryPermission(RegistryPermissionAccess.Read, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Curr entVersion\App Paths\AcroRd32.exe");
            ////    fread.AddPathList(RegistryPermissionAccess.Read | RegistryPermissionAccess.Write, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Curr entVersion\App Paths\AcroRd32.exe");
            ////    RegistryKey ourKey = Registry.LocalMachine;
            ////    ourKey = ourKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\AcroRd32.exe", true);
            ////    string acrobatreaderlocation = ourKey.GetValue("").ToString();
            ////    ourKey.Close();
            ////    ourKey = null;

            ////    PdfFilePrinter.AdobeReaderPath = acrobatreaderlocation;

            ////    PrintDocument printpdfdoc = new PrintDocument();

            ////    //Set the PDF Document and Printer Name (default printer name will be used).
            ////    PdfFilePrinter pdfprinter = new PdfFilePrinter(reportFilePath, printpdfdoc.PrinterSettings.PrinterName);

            ////    // Send the document to the printer and try to close acrobat after the time period.
            ////    //Added be Malliga on 23/6/2008 for issue 2651
            ////    this.getWaittime.GetCommentsConfigDetails.Clear();
            ////    this.getWaittime = GetConfigDetails("TS_WaitTime");
            ////    if (this.getWaittime.GetCommentsConfigDetails.Rows.Count > 0)
            ////    {
            ////        this.waittime = int.Parse(this.getWaittime.GetCommentsConfigDetails[0][this.getWaittime.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
            ////    }  //END

            ////    pdfprinter.Print(this.waittime);
            ////}
            ////catch (System.Exception sysexp)
            ////{
            ////    MessageBox.Show(sysexp.Message, "Some Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
            ////return true;
            
            ////Added by Biju on 26-Nov-07 to fix the report printing format issue - END
            ////Commented by Biju on 26-Nov-07 to fix the report printing format issue - START
            if (tmpReportDetails.OptionalReportParameter != null)
            {
                IDictionaryEnumerator en = tmpReportDetails.OptionalReportParameter.GetEnumerator();
                en.MoveNext();
                this.RenderedReport = this.RenderReport(reportPath, en.Key.ToString(), en.Value.ToString());
            }
            else
            {
                this.RenderedReport = this.RenderReport(reportPath, "", "");
            }

            // Wait for the report to completely render.
            if (this.numberOfPages < 1)
            {
                return false;
            }
            else
            {
                PrinterSettings printerSettings = new PrinterSettings();
                
                printerSettings.PrinterName = printerName;
                
                printerSettings.MaximumPage = this.numberOfPages;
                printerSettings.MinimumPage = 1;
                printerSettings.PrintRange = PrintRange.SomePages;
                printerSettings.FromPage = 1;
                printerSettings.ToPage = this.numberOfPages;

                PageSettings pageSetting = new PageSettings(printerSettings);
                if (WSHelper.ExecInfo != null)
                {
                    int width, height;
                    int.TryParse(Math.Floor(WSHelper.ExecInfo.ReportPageSettings.PaperSize.Width * 0.0393700787402 * 100).ToString(), out width);
                    int.TryParse(Math.Floor(WSHelper.ExecInfo.ReportPageSettings.PaperSize.Height * 0.0393700787402 * 100).ToString(), out height);
                    //printerSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom Size", width, height);
                    pageSetting.PaperSize = new PaperSize("Custom Size", width, height);
                    int left, right, top, bottom, tempVar;
                    int.TryParse(Math.Floor(WSHelper.ExecInfo.ReportPageSettings.Margins.Left * 0.0393700787402 * 100).ToString(), out left);
                    int.TryParse(Math.Floor(WSHelper.ExecInfo.ReportPageSettings.Margins.Right * 0.0393700787402 * 100).ToString(), out right);
                    int.TryParse(Math.Floor(WSHelper.ExecInfo.ReportPageSettings.Margins.Top * 0.0393700787402 * 100).ToString(), out top);
                    int.TryParse(Math.Floor(WSHelper.ExecInfo.ReportPageSettings.Margins.Bottom * 0.0393700787402 * 100).ToString(), out bottom);

                    PrintDocument pd = new PrintDocument();

                    if (width > height)
                    {
                        printerSettings.DefaultPageSettings.Landscape = true;
                        //pd.DefaultPageSettings.Landscape = true;
                        pageSetting.Landscape = true;
                        tempVar = left;
                        left = right;
                        right = tempVar;

                        tempVar = top;
                        top = bottom;
                        bottom = tempVar;
                        
                    }

                    printerSettings.DefaultPageSettings.Margins = new Margins(left, right, top, bottom);
                    pageSetting.Margins = new Margins(left, right, top, bottom);
                    pd.DefaultPageSettings.Margins =new Margins(left, right, top, bottom); 
                    printerSettings.CreateMeasurementGraphics(pageSetting, true );


                    //pd.DefaultPageSettings.Margins = new Margins(left, right, top, bottom);
                    this.currentPrintingPage = 1;
                    this.lastPrintingPage = this.numberOfPages;
                    pd.PrinterSettings = printerSettings;
                    pd.PrintPage += new PrintPageEventHandler(this.PrintPage);
                    pd.Print();
                }
            }

            return true;
            ////Commented by Biju on 26-Nov-07 to fix the report printing format issue - END
        }

        /// <summary>
        /// Gets the config details.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns></returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// Handles the PrintPage event of the pd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="ev">The <see cref="T:System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        public void PrintPage(object sender, PrintPageEventArgs ev)
        {
            ev.HasMorePages = false;
            if (this.currentPrintingPage <= this.lastPrintingPage && this.MoveToPage())
            {
                // Draw the page
                this.ReportDrawPage(ev.Graphics);

                // If the next page is less than or equal to the last page, 
                // print another page.
                if (++this.currentPrintingPage <= this.lastPrintingPage)
                {
                    ev.HasMorePages = true;
                }
            }
        }

        /// <summary>
        /// Renders the report.
        /// </summary>
        /// <param name="reportPath">The report path.</param>
        /// <param name="key">The key.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>REnderd Report </returns>
        private byte[][] RenderReport(string reportPath, string key, string keyId)
        {
            // Private variables for rendering
            string deviceInfo = null;
            string format = "IMAGE";
            Byte[] firstPage = new byte[1] { 0x00 };
            string encoding = string.Empty;
            string mimetype = string.Empty;
            string[] streamids = null;
            Byte[][] pages = new Byte[0][];

            this.numberOfPages = 1;

            while (firstPage.Length > 0)
            {
                deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage><PrintDpiX>78</PrintDpiX><PrintDpiY>78</PrintDpiY></DeviceInfo>", "emf", this.numberOfPages);

                firstPage = WSHelper.GetRenderReport(reportPath, format, deviceInfo, out encoding, out mimetype, out streamids, key, keyId);

                if (firstPage.Length == 0 && numberOfPages == 1)
                {
                    break;
                }

                if (firstPage.Length > 0)
                {
                    Array.Resize(ref pages, pages.Length + 1);
                    pages[pages.Length - 1] = firstPage;
                    this.numberOfPages++;
                }
            }

            this.numberOfPages = this.numberOfPages - 1;

            return pages;
        }

        /// <summary>
        ///Method to draw the current emf memory stream .
        /// </summary>
        /// <param name="g">The g.</param>
        private void ReportDrawPage(Graphics g)
        {
            if (null == this.currentPageStream || 0 == this.currentPageStream.Length || null == this.metaFile)
            {
                return;
            }

            lock (this)
            {
                // Set the metafile delegate.
                this.metaFileProcDelegate = new Graphics.EnumerateMetafileProc(this.MetafileCallback);

                // Draw in the rectangle
                Point destPoint = new Point(0, 0);
                g.EnumerateMetafile(this.metaFile, destPoint, this.metaFileProcDelegate);

                // Clean up
                this.metaFileProcDelegate = null;
            }
        }

        /// <summary>
        /// Moves to page.
        /// </summary>
        /// <returns>check current page exists</returns>
        private bool MoveToPage()
        {
            // Check to make sure that the current page exists in
            // the array list
            if (null == this.RenderedReport[this.currentPrintingPage - 1])
            {
                return false;
            }

            // Set current page stream equal to the rendered page
            this.currentPageStream = new MemoryStream(this.RenderedReport[this.currentPrintingPage - 1]);

            // Set its postion to start.
            this.currentPageStream.Position = 0;

            // Initialize the metafile
            if (null != this.metaFile)
            {
                this.metaFile.Dispose();
                this.metaFile = null;
            }

            // Load the this.metaFile image for this page
            this.metaFile = new Metafile((Stream)this.currentPageStream);
            return true;
        }

        /// <summary>
        /// Metafiles the callback.
        /// </summary>
        /// <param name="recordType">Type of the record.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="dataSize">Size of the data.</param>
        /// <param name="data">The data.</param>
        /// <param name="callbackData">The callback data.</param>
        /// <returns>retrun the Metafile </returns>
        private bool MetafileCallback(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;

            // Dance around unmanaged code.
            if (data != IntPtr.Zero)
            {
                // Copy the unmanaged record to a managed byte buffer 
                // that can be used by PlayRecord.
                dataArray = new byte[dataSize];
                Marshal.Copy(data, dataArray, 0, dataSize);
            }

            // play the record.      
            this.metaFile.PlayRecord(recordType, flags, dataSize, dataArray);

            return true;
        }
        #endregion

        #region Strut
        /// <summary>
        /// ReportDetails
        /// </summary>
        public struct ReportDetails
        {          
            /// <summary>
            /// used to store Report mode (Print\Preview\Email)
            /// </summary>
            internal ReportType Mode;

            /// <summary>
            /// used to store Report file
            /// </summary>
            internal string ReportFile;

            /// <summary>
            /// used to store Report Description.
            /// </summary>
            internal string ReportDescription;

            /// <summary>
            /// used to Create PDF file.
            /// </summary>
            internal int ReportName;

            /// <summary>
            /// used to Create PDF file.
            /// </summary>
            internal string ApplicationPathforPdf;

            /// <summary>
            /// used to pass optionalReportParameter
            /// </summary>
            internal Hashtable OptionalReportParameter;
        }
        #endregion
    }
}