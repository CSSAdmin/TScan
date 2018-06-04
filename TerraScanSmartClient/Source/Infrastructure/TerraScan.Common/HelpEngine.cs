//--------------------------------------------------------------------------------------------
// <copyright file="HelpEngine.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the HelpEngine.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07 Aug 06		JYOTHI		        Created
// 25 Dec 06        JAYANTHI            Modified (Link color to be changed when the cell is clicked in the 
//                                      GridView)
//*********************************************************************************/
namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;
    using System.Diagnostics;
    using System.IO;
    using System.Configuration;
    using TerraScan.Utilities;
    
    /// <summary>
    /// Help Engine
    /// </summary>
    public partial class HelpEngine : Form
    {
        /// <summary>
        /// Created string for current tempFilePath
        /// </summary>
        private string tempFilePath = string.Empty;

        /// <summary>
        /// Created string for current tempFileName
        /// </summary>
        private string tempFileName = string.Empty;

        /// <summary>
        /// Created string for current tempFileName
        /// </summary>
        private string currentFormId = string.Empty;

        /// <summary>
        /// Created string for current tempFileName
        /// </summary>
        public string currentFormName = string.Empty;


        /// <summary>
        /// Usde to store the listDatatableRowsCount
        /// </summary>
        private int listDatatableRowsCount;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="T:HelpEngine"/> class.
        ///// </summary>
        ///// <param name="formId">The form id.</param>
        //public HelpEngine(string formId)
        //{
        //    InitializeComponent();
        //    this.currentFormId = formId;
        //    //this.currentFormName = formName;
        //    this.CancelButton = this.ReportCancelButton;
        //    this.LoadHelpDocuments(false);
        //    this.GetFileName();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formId"></param>
        public HelpEngine(string formName, string formId)
        {
            InitializeComponent();
            this.currentFormId = formId;
            this.currentFormName = formName;
            this.CancelButton = this.ReportCancelButton;
            this.LoadHelpDocuments(false);
            this.GetFileName();
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public static void Show(string formName, string formId)
        {
            //// Create a instance for ErrorEngine Form and Show.
            //HelpEngine helpEngine = new HelpEngine(formId);
            HelpEngine helpEngine = new HelpEngine(formName, formId);
            if (helpEngine.LoadHelpDocuments(true))
            {
                helpEngine.ShowDialog();
            }
            else
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Nine);
            }
        }

        //public static void Show(string formId)
        //{
        //    //// Create a instance for ErrorEngine Form and Show.
        //    //HelpEngine helpEngine = new HelpEngine(formId);
        //    HelpEngine helpEngine = new HelpEngine(formId);
        //    if (helpEngine.LoadHelpDocuments(true))
        //    {
        //        helpEngine.ShowDialog();
        //    }
        //    else
        //    {
        //        ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Nine);
        //    }
        //}

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formName"></param>
        public void setFormName(string formName)
        {
            this.currentFormName = formName;
        }

        /// <summary>
        /// Loads the report DataGrie.
        /// </summary>
        /// <param name="checkRecordsFlag">if set to <c>true</c> [check records flag].</param>
        /// <returns>A boolean value</returns>
        private Boolean LoadHelpDocuments(bool checkRecordsFlag)
        {
            HelpEngineData tmpDataSet = new HelpEngineData();
            tmpDataSet = WSHelper.ListHelpDocumentForm(this.currentFormId);
            if (checkRecordsFlag.Equals(true) && tmpDataSet.ListHelpDocumentForm.Rows.Count.Equals(0))
            {
                return false;
            }
            else
            {
                DataGridViewColumnCollection columns = this.HelpEngineDataGridView.Columns;
                this.HelpEngineDataGridView.AutoGenerateColumns = false;
                columns["HelpID"].DataPropertyName = "HelpID";
                columns["HelpID"].Visible = false;
                columns["FileName"].DataPropertyName = "FileName";
                columns["FileName"].Visible = false;
                ////columns["FormFile"].DataPropertyName = "FormFile";
                ////columns["FormFile"].Visible = false;
                columns["Description"].DataPropertyName = "Description";
                columns["IsFile"].DataPropertyName = "IsFile";
                columns["IsFile"].Visible = false;

                columns["HelpID"].DisplayIndex = 0;
                columns["FileName"].DisplayIndex = 1;
                ////columns["FormFile"].DisplayIndex = 2;
                columns["Description"].DisplayIndex = 3;
                columns["IsFile"].DisplayIndex = 4;

                this.HelpEngineDataGridView.DataSource = tmpDataSet.ListHelpDocumentForm;
                this.FormIDLabel.Text = "9018 - Help Documents for " + this.currentFormId +" "+ this.currentFormName;
                SetFormHeight(this.HelpEngineDataGridView.Rows.Count);
                return true;
            }
        }

        /// <summary>
        /// Sets the height of the Form.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetFormHeight(int recordCount)
        {
            if (recordCount > 7)
            {
                int increment = ((recordCount - 7) * 22);
                if (increment <= 290)
                {
                    this.SetHeight(increment);
                }
                else
                {
                    this.SetHeight(290);
                    this.HelpEngineDataGridView.ScrollBars = ScrollBars.Vertical;
                }
            }
            else
            {
               this.SetHeight(0);
            }

        }

        /// <summary>
        /// Sets the Controls Height and Position
        /// </summary>
        /// <param name="height">Increased Height</param>
        private void SetHeight(int height)
        {
            this.HelpEngineDataGridView.Height = 155 + height;
            this.HelpPanel.Height = 228 + height;
            this.Height = 260 + height;
            this.panel1.Top = 208 + height;
            this.FormIDLabel.Top = 212 + height;
            this.ReportCancelButton.Top = 180 + height;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        private void GetFileName()
        {
            try
            {
                CommentsData configDetailsDataSet = new CommentsData();
                configDetailsDataSet = WSHelper.GetConfigDetails("TS_HelpFiles");
                this.tempFilePath = configDetailsDataSet.GetCommentsConfigDetails.Rows[0][configDetailsDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the CellClick event of the HelpEngineDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void HelpEngineDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Process process = new Process();
                if (HelpEngineDataGridView.Rows[e.RowIndex].Cells[HelpEngineDataGridView.Columns["IsFile"].Index].Value.ToString() == "True")
                {
                    if (!string.IsNullOrEmpty(this.tempFilePath))
                    {
                        if (!this.tempFilePath.EndsWith(@"\"))
                        {
                            this.tempFilePath = this.tempFilePath + @"\";
                        }

                        FileInfo fileInfo = new FileInfo(this.tempFilePath + HelpEngineDataGridView.Rows[e.RowIndex].Cells[HelpEngineDataGridView.Columns["FileName"].Index].Value.ToString());
                        if (fileInfo.Exists)
                        {
                            process.StartInfo.FileName = this.tempFilePath + HelpEngineDataGridView.Rows[e.RowIndex].Cells[HelpEngineDataGridView.Columns["FileName"].Index].Value.ToString();
                            process.StartInfo.UseShellExecute = true;
                            process.Start();
                            ////Visited link color changed when link clicked.
                            (this.HelpEngineDataGridView.Columns["Description"] as DataGridViewLinkColumn).LinkColor = System.Drawing.Color.FromArgb(128, 0, 128);
                        }
                        else
                        {
                            MessageBox.Show("Specified file/path does not exist.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not access the shared location.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    process.StartInfo.FileName = "iexplore";
                    process.StartInfo.Arguments = HelpEngineDataGridView.Rows[e.RowIndex].Cells[HelpEngineDataGridView.Columns["FileName"].Index].Value.ToString(); ///// "www.yahoo.com";
                    process.Start();
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FileAccessDenied"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FileAccessDenied"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Win32Exception)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FileAccessDenied"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Leave event of the HelpEngineDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelpEngineDataGridView_Leave(object sender, EventArgs e)
        {
            this.HelpEngineDataGridView.CurrentCell = null;
        }
    }
}