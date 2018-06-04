//----------------------------------------------------------------------------------
// <copyright file="F9042.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F942.cs.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		           Description
// ----------		---------		       -----------------------------------------
// 27/11/2008       A.ShanmugaSundaram     Created 
//*********************************************************************************/

namespace D9030
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
    using System.Xml;
    using System.Windows.Forms;
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
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using System.Data.OleDb;
    using System.IO;
    using Excel = Microsoft.Office.Interop.Excel;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Win32;
    using System.Threading;

    /// <summary>
    /// Class file for F9041
    /// </summary>
    public partial class F9042 : BasePage
    {
        #region Variables

        /// <summary>
        /// Background thread
        /// </summary>
        private static BackgroundWorker backGroundWork;

        /// <summary>
        /// F9041Controller
        /// </summary>
        private F9042Controller form9042Control;

        /// <summary>
        /// Excel Application
        /// </summary>
        private Excel.Application myapplication;

        /// <summary>
        /// Excel Workbook
        /// </summary>
        private Excel.Workbook wb;

        /// <summary>
        /// ThreadStart object
        /// </summary>
        private ThreadStart job;

        /// <summary>
        /// Thread Object
        /// </summary>
        private Thread thread;
       
        /// <summary>
        /// KeyID
        /// </summary>
        private int keyid;

        /// <summary>
        /// excelversion
        /// </summary>
        private string excelversion;

        /// <summary>
        /// vTemplateFile
        /// </summary>
        private string templateFile;

        /// <summary>
        /// vTemplatePath
        /// </summary>
        private string templatePath;

        /// <summary>
        /// vConnStr
        /// </summary>
        private string connectionString;

        /// <summary>
        /// vTemplateName
        /// </summary>
        private string templateName;

        /// <summary>
        /// vTemplateDef
        /// </summary>
        private string templateDefinition;

        /// <summary>
        /// vTemplatePurpose
        /// </summary>
        private string templatePurpose;

        /// <summary>
        /// vUser
        /// </summary>
        private string username;

        /// <summary>
        /// vTitle
        /// </summary>
        private string title;

        /// <summary>
        /// vDesc
        /// </summary>
        private string description;

        /// <summary>
        /// vQueryView
        /// </summary>
        private string queryView;

        /// <summary>
        /// vSnapshot
        /// </summary>
        private string snapshot;

        /// <summary>
        /// vSQL
        /// </summary>
        private string sqlQuery;

        /// <summary>
        /// ListTemplateData
        /// </summary>
        private F9042ExcelAnalyticsData listTemplateData = new F9042ExcelAnalyticsData();

        /// <summary>
        /// GetTemplateData
        /// </summary>
        private F9042ExcelAnalyticsData getTemplateData = new F9042ExcelAnalyticsData();        

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9041"/> class.
        /// </summary>
        public F9042()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9610"/> class.
        /// </summary>
        /// <param name="queryViewId">QueryViewID</param>
        /// <param name="queryViewName">Name of the query view.</param>
        /// <param name="snapShotName">Name of the snap shot.</param>
        /// <param name="queryString">The query string.</param>
        public F9042(int queryViewId, string queryViewName, string snapShotName, string queryString)
        {
            this.InitializeComponent();
            this.keyid = queryViewId;
            this.snapshot = snapShotName;
            this.queryView = queryViewName;
            this.sqlQuery = queryString;
        }

        #endregion
        
        #region Property

        /// <summary>
        /// For F9041Control
        /// </summary>
        [CreateNew]
        public F9042Controller Form9042Control
        {
            get { return this.form9042Control as F9042Controller; }
            set { this.form9042Control = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether [is excel installed].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is excel installed]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExcelInstalled()
        {
            bool functionReturnValue = false;
            //// Define the RegistryKey objects for the registry hives. 
            RegistryKey regClasses = Registry.ClassesRoot;

            //// Check whether Microsoft Excel is installed on this computer, 
            //// by searching the HKEY_CLASSES_ROOT\Excel.Application key. 
            RegistryKey regExcel = regClasses.OpenSubKey("Excel.Application");
            if (regExcel == null)
            {
                functionReturnValue = false;
            }
            else
            {
                functionReturnValue = true;
            }

            //// Always close Registry keys after using them. 
            regExcel.Close();

            if (this.IsExcelInstalled())
            {
                Console.WriteLine("Microsoft Excel is installed");
            }
            else
            {
                Console.WriteLine("Microsoft Excel is not installed");
            }

            return functionReturnValue;
        }

        /// <summary>
        /// Backs the ground work run worker completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //// Close the Loading data thread
                this.thread.Abort();

                this.wb.Connections["TSLink"].OLEDBConnection.MaintainConnection = true;
                Excel.Sheets sheets = this.wb.Worksheets;
                Excel.Worksheet mysheet = (Excel.Worksheet)sheets.get_Item(SharedFunctions.GetResourceString("ExcelSheetName"));

                //// Assigning the value to a particular cell
                Excel.Range b2 = (Excel.Range)mysheet.get_Range("B2", "B2");
                b2.Value2 = this.templateName;
                Excel.Range b3 = (Excel.Range)mysheet.get_Range("B3", "B3");
                b3.Value2 = this.templateFile;
                Excel.Range b4 = (Excel.Range)mysheet.get_Range("B4", "B4");
                b4.Value2 = this.templateDefinition;
                Excel.Range b5 = (Excel.Range)mysheet.get_Range("B5", "B5");
                b5.Value2 = this.templatePurpose;

                Excel.Range b8 = (Excel.Range)mysheet.get_Range("B8", "B8");
                b8.Value2 = this.username;
                Excel.Range b9 = (Excel.Range)mysheet.get_Range("B9", "B9");
                b9.Value2 = this.title;
                Excel.Range b10 = (Excel.Range)mysheet.get_Range("B10", "B10");
                b10.Value2 = this.description;
                Excel.Range b11 = (Excel.Range)mysheet.get_Range("B11", "B11");
                b11.Value2 = this.queryView;
                Excel.Range b12 = (Excel.Range)mysheet.get_Range("B12", "B12");
                b12.Value2 = this.snapshot;
                Excel.Range b13 = (Excel.Range)mysheet.get_Range("B13", "B13");
                b13.Value2 = this.sqlQuery;

                this.wb.Connections["TSLink"].OLEDBConnection.Refresh();
                this.myapplication.Visible = true;
                this.wb.Activate();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Threads the job.
        /// </summary>
        public void ThreadJob()
        {
            ////calling "Loading data.." thread UI
            Progressform prgfrm = new Progressform();
            prgfrm.ShowDialog();
        }

        /// <summary>
        /// Handles the DoWork event of the bw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //// Updating the connection and command type to Excel WorkSheet

                //// Temporary solution as per client advice 
                wb.Connections["TSLink"].OLEDBConnection.Connection = connectionString;

                this.wb.Connections["TSLink"].OLEDBConnection.CommandType = Microsoft.Office.Interop.Excel.XlCmdType.xlCmdSql;
                this.wb.Connections["TSLink"].OLEDBConnection.CommandText = this.sqlQuery;
                //// Fixed Issue 4779 on 11th feb'09 by A.Shanmugasundaram
                this.wb.Connections["TSLink"].OLEDBConnection.BackgroundQuery = false;
                this.wb.Connections["TSLink"].OLEDBConnection.Refresh();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Backs the ground work progress changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ////Status of the BackGround thread
            MessageBox.Show("Reached " + e.ProgressPercentage + "%");
        }

        /// <summary>
        /// Gets the description data.
        /// </summary>
        private void GetTemplateListData()
        {
            this.listTemplateData = this.form9042Control.WorkItem.F9042_ListTemplate(this.keyid.ToString());

            if (this.listTemplateData.ListTemplate.Rows.Count > 0)
            {
                this.TemplateNameCombo.DataSource = this.listTemplateData.ListTemplate;
                this.TemplateNameCombo.DisplayMember = this.listTemplateData.ListTemplate.TemplateNameColumn.ColumnName;
                this.TemplateNameCombo.ValueMember = this.listTemplateData.ListTemplate.TemplateIDColumn.ColumnName;
            }
        }

        #endregion Methods

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F9041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9042_Load(object sender, EventArgs e)
         {
             try
             {
                 this.GetTemplateListData();
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
                 this.Cursor = Cursors.Default;
             }
        }

        #endregion        

        #region Combo Events

        /// <summary>
        /// Handles the SelectedIndexChanged event of the TemplateNameCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TemplateNameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.TemplateNameCombo.SelectedIndex > 0)
                {
                    this.getTemplateData = this.form9042Control.WorkItem.F9042_GetTemplate(Convert.ToInt32(this.TemplateNameCombo.SelectedValue));
                    if (this.getTemplateData.GetTemplate.Rows.Count > 0)
                    {
                        this.DefinitionTextBox.Text = this.getTemplateData.GetTemplate.Rows[0][this.getTemplateData.GetTemplate.DefinitionColumn.ColumnName].ToString();
                        this.PurposeTextBox.Text = this.getTemplateData.GetTemplate.Rows[0][this.getTemplateData.GetTemplate.PurposeColumn.ColumnName].ToString();
                        this.templateFile = this.getTemplateData.GetTemplate.Rows[0][this.getTemplateData.GetTemplate.TemplateFileColumn.ColumnName].ToString();
                    }
                }
                else
                {
                    this.DefinitionTextBox.Text = string.Empty;
                    this.PurposeTextBox.Text = string.Empty;
                    this.TitleTextBox.Text = string.Empty;
                    this.DescriptionTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
        
        #region Button Events
        /// <summary>
        /// Handles the Click event of the NewDatasetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewDatasetButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TemplateNameCombo.SelectedIndex > 0)
                {
                    RegistryKey regClasses = Registry.ClassesRoot;
                    //// Check whether Microsoft Excel is installed on this computer, 
                    //// by searching the HKEY_CLASSES_ROOT\Excel.Application key. 
                    RegistryKey regExcel = regClasses.OpenSubKey("Excel.Application");
                    if (regExcel != null)
                    {
                        this.myapplication = new Excel.Application();

                        //// Checking for the Excel version
                        this.excelversion = this.myapplication.Version;
                        decimal installedExcelVersion = 0;

                        if (!string.IsNullOrEmpty(this.excelversion))
                        {
                            decimal.TryParse(this.excelversion, out installedExcelVersion);
                        }

                        //if (this.excelversion.Trim().Equals("12.0"))
                        //{
                        if (installedExcelVersion >= 12)
                        {
                            //// Getting the folder path and the template file
                            CommentsData excelDataSet = new CommentsData();
                            excelDataSet = this.form9042Control.WorkItem.GetConfigDetails("TS_ATemplateRoot");
                            if (excelDataSet.Tables.Count > 0 && excelDataSet.Tables[excelDataSet.GetCommentsConfigDetails.TableName].Rows.Count > 0)
                            {
                                this.templatePath = excelDataSet.GetCommentsConfigDetails.Rows[0][excelDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString() + "\\" + this.templateFile + ".xltm";
                                if (!string.IsNullOrEmpty(this.templatePath) && System.IO.File.Exists(this.templatePath))
                                {
                                    this.myapplication.Visible = true;

                                    //// Opening a workbook with template assigned from a specific location
                                    this.wb = (Excel.Workbook)(this.myapplication.Workbooks.Add(this.templatePath));

                                    //// Getting the value from the DB for Connection String
                                    excelDataSet = this.form9042Control.WorkItem.GetConfigDetails("TS_AConnStr");
                                    if (excelDataSet.Tables.Count > 0 && excelDataSet.Tables[excelDataSet.GetCommentsConfigDetails.TableName].Rows.Count > 0)
                                    {
                                        this.connectionString = excelDataSet.GetCommentsConfigDetails.Rows[0][excelDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
                                    }

                                    this.templateName = this.TemplateNameCombo.Text.ToString();
                                    this.templateDefinition = this.DefinitionTextBox.Text;
                                    this.templatePurpose = this.PurposeTextBox.Text;
                                    this.username = TerraScanCommon.UserName;
                                    this.title = this.TitleTextBox.Text;
                                    this.description = this.DescriptionTextBox.Text;

                                    //// Back Ground thread invoke process
                                    backGroundWork = new BackgroundWorker();
                                    backGroundWork.DoWork += new DoWorkEventHandler(this.BackGroundWorkDoWork);
                                    backGroundWork.ProgressChanged += new ProgressChangedEventHandler(this.BackGroundWorkProgressChanged);
                                    backGroundWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackGroundWorkRunWorkerCompleted);
                                    backGroundWork.RunWorkerAsync();

                                    this.job = new ThreadStart(this.ThreadJob);
                                    this.thread = new Thread(this.job);
                                    this.thread.IsBackground = false;
                                    this.thread.Start();
                                    this.Close();
                                }
                                else
                                {
                                    ////Template file not found 
                                    ErrorEngine.ShowForm(13, this.templatePath);
                                }
                            }
                            else
                            {
                                ////Path not found
                                ErrorEngine.ShowForm(14);  //// if TS_ATemplateRoot doesnot exit
                            }
                        }
                        else
                        {
                            ////Excel version not found
                            ErrorEngine.ShowForm(12);
                        }
                   
                         //else
                         //{
                         //    ////Excel version not found
                         //    ErrorEngine.ShowForm(12);
                         //}
                     }
                     else
                     {
                         MessageBox.Show(SharedFunctions.GetResourceString("ExcelNotFound"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("SelectTemplateName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }
    }

        #endregion Button Events
}