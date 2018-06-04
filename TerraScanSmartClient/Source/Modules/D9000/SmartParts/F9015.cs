// --------------------------------------------------------------------------------------------
// <copyright file="F9015.cs" company="Congruent">
//    Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//   This file contains methods for the AttachmentForm.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date             Author             Description
// ----------      ---------           ---------------------------------------------------------
// 17 May 06       KARTHIKEYAN V        Created
// 24 Jul 06        VINOTH              Modified For CAB Conversion
// 06 Sep 06        VINOTH              Modified
// 19 Dec 06        VINOTH              Grid Updation
// 24 Nov 06        Malliga             Implemented for the co:4776(Two COmbox has been changed to autocomplete) 
// *********************************************************************************/

namespace D9000
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Infragistics.Documents.Excel;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;

    /// <summary>
    /// SQLSupportForm UserControl class
    /// </summary>
    [SmartPart]
    public partial class F9015 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Created Readonly String for selectValue
        /// </summary>
        private readonly string selectValue = "select";

        /// <summary>
        /// Created Readonly String for insertValue
        /// </summary>
        private readonly string insertValue = "insert";

        /// <summary>
        /// Created Readonly String for updateValue
        /// </summary>
        private readonly string updateValue = "update";

        /// <summary>
        /// Created Readonly String for deleteValue
        /// </summary>
        private readonly string deleteValue = "delete";

        /// <summary>
        /// Static int to store current position of the Index
        /// </summary>
        private static int currentPos;

        /// <summary>
        /// dupFlag
        /// </summary>
        private bool dupFlag;

        /// <summary>
        /// Integer to store the array count
        /// </summary>
        private int count;

        /// <summary>
        /// Object for DataTable created
        /// </summary>
        private DataTable sqlQuery = new DataTable();

        /// <summary>
        /// Created string for sqlQueryText
        /// </summary>
        private string sqlQueryText = string.Empty;

        /// <summary>
        /// Created Controller for F9015Controller
        /// </summary>
        private F9015Controller form9015Control;

        /// <summary>
        /// String array to store sqlStatement
        /// </summary>
        private List<string> sqlString = new List<string>();

        /// <summary>
        /// Index for sqlString
        /// </summary>
        private int index;

        /// <summary>
        /// To store previous category
        /// </summary>
        private string prevSqlCategory = string.Empty;

        /// <summary>
        /// To store previous Description
        /// </summary>
        private string prevSqlDescription = string.Empty;

        /// <summary>
        /// To store previous query
        /// </summary>
        private string prevQuery = string.Empty;

        /// <summary>
        /// Object for GetSqlStringDataTable
        /// </summary>
        private SQLSupportData.GetSqlStringDataTable sqlStringDataTable = new SQLSupportData.GetSqlStringDataTable();

        /// <summary>
        /// Object for GetSqlDescriptionDataTable
        /// </summary>
        private SQLSupportData.ListSqlDescriptionDataTable sqlDescriptionDataTable = new SQLSupportData.ListSqlDescriptionDataTable();

        /// <summary>
        /// Object for GetSqlCategoryDataTable
        /// </summary>
        private SQLSupportData.ListSqlCategoryDataTable sqlCategoryDataTable = new SQLSupportData.ListSqlCategoryDataTable();

        /// <summary>
        /// flag
        /// </summary>
        private int flag;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SQLSupportForm"/> class.
        /// </summary>
        public F9015()
        {
            this.InitializeComponent();
            ////this.AcceptButton = this.sqlRunButton;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F9015 controll.
        /// </summary>
        /// <value>The F9015 controll.</value>
        [CreateNew]
        public F9015Controller F9015Controll
        {
            get { return this.form9015Control as F9015Controller; }
            set { this.form9015Control = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the SQLSupportForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9015_Load(object sender, EventArgs e)
        {
            try
            {
                this.k.Stop();

                // Make the Cursor to focus on sqlQueryTextBox during form load.
                this.SqlLibraryCombo.DisplayMember = "Description";
                this.SqlLibraryCombo.ValueMember = "SQLID";
                this.LoadSqlCombo();
                this.SaveMenuStrip.Visible = false;
                this.SaveMenu.Visible = false;
                this.PreviousButton.Enabled = false;
                this.NextButton.Enabled = false;
                this.sqlRunButton.Enabled = false;
                this.ExcelButton.Enabled = false;
                this.PreviewButton.Enabled = false;
                this.SaveMenu.Click += new EventHandler(this.SqlSaveButton_Click);
                this.SetPermission();
                this.ParentForm.AcceptButton = this.sqlRunButton;
                this.ResultGridView.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.None;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Set ButtonFont Size
        /// </summary>
        private void SetButtonFontSize()
        {
            this.sqlSaveButton.Font = new Font("Arial", 16F, System.Drawing.FontStyle.Bold);            
            this.sqlRunButton.Font = new Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// To Set Permission
        /// </summary>
        private void SetPermission()
        {
            if (this.PermissionFiled.editPermission)
            {
                this.sqlSaveButton.Enabled = true;
            }
            else
            {
                this.sqlSaveButton.Enabled = false;
            }
        }        

        /// <summary>
        /// Handles the Click event of the SqlRunButton control And run the sql query.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SqlRunButton_Click(object sender, EventArgs e)
        {
            // Checks whether sqlQueryTextBox has value or not.
            if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()) && this.sqlQueryTextBox.Text.Trim().Length > 7)
            {
                // Gets the first 6 character of the Query to Check it is valid or not.
                this.sqlQueryText = this.sqlQueryTextBox.Text.Trim().Substring(0, 6).ToLower();

                if (string.Compare(this.sqlQueryText, this.selectValue) == 0 || string.Compare(this.sqlQueryText, this.insertValue) == 0 || string.Compare(this.sqlQueryText, this.deleteValue) == 0 || string.Compare(this.sqlQueryText, this.updateValue) == 0)
                {
                    Application.DoEvents();
                    try
                    {
                        // Default to null
                        this.sqlQuery = null;
                        this.ResultGridView.DataSource = null;

                        // Fetch the result from Database.
                        this.sqlQuery = this.F9015Controll.WorkItem.GetSqlQueryResult(this.sqlQueryTextBox.Text.Trim()).Tables[0];
                        this.ResultGridView.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
                        this.PreviewButton.Enabled = true;
                        this.ExcelButton.Enabled = true;
                        if (this.index == 0)
                        {
                            this.sqlString.Add(this.sqlQueryTextBox.Text.Trim());
                            index++;
                            currentPos = this.index - 1;
                            if (this.sqlString[currentPos].ToString() != null)
                            {
                                count++;
                            }
                        }
                        else
                        {
                            if (!string.Equals(this.sqlString[count - 1].ToString().Trim(), this.sqlQueryTextBox.Text.Trim()))
                            {
                                this.sqlString.Add(this.sqlQueryTextBox.Text.Trim());
                                index++;
                                currentPos = this.index - 1;                                
                                this.PreviousButton.Enabled = true;
                                this.NextButton.Enabled = false;
                                if (this.sqlString[currentPos].ToString() != null)
                                {
                                    count++;
                                }
                            }
                            else
                            {
                                if (currentPos == (this.count - 1))
                                {
                                    this.dupFlag = false;
                                    currentPos = this.index - 1;
                                    if (currentPos == 0)
                                    {
                                        this.PreviousButton.Enabled = false;
                                        this.NextButton.Enabled = false;
                                    }
                                    else
                                    {
                                        this.PreviousButton.Enabled = true;
                                        this.NextButton.Enabled = false;
                                    }
                                }
                                else
                                {                                    
                                    this.dupFlag = true;
                                    this.PreviousButton.Enabled = true;
                                    this.NextButton.Enabled = true;
                                }
                            }
                        }
                        /////Following code has been added to display bool column value as true or false instead of textbox.
                        DataTable tempDataTable = this.sqlQuery.Clone();
                        for (int colCount = 0; colCount < this.sqlQuery.Columns.Count; colCount++)
                        {
                            if (this.sqlQuery.Columns[colCount].DataType.Equals(typeof(bool)))
                            {
                                tempDataTable.Columns[colCount].DataType = typeof(string);
                            }
                        }

                        tempDataTable.Load(this.sqlQuery.CreateDataReader());

                        // Display the result in ResultGridView.
                        this.ResultGridView.DataSource = tempDataTable;

                        if (this.sqlQuery != null)
                        {
                            if (this.sqlQuery.Rows.Count > 0)
                            {
                                // Make the first row to be selected.
                                this.ResultGridView.Rows[0].Selected = true;

                                this.PreviewButton.Enabled = this.PermissionFiled.editPermission;
                                this.ExcelButton.Enabled = this.PermissionFiled.editPermission;
                            }
                            else
                            {
                                this.PreviewButton.Enabled = false;
                                this.ExcelButton.Enabled = false;
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
                        this.k.Stop();                        
                    }
                }
                else
                {
                    this.ResultGridView.DataSource = null;
                    //// ConfigurationWrapper.ApplicationName
                    this.ResultGridView.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                    MessageBox.Show(SharedFunctions.GetResourceString("SQLText") + "\n" + SharedFunctions.GetResourceString("SQLText1") + "\n" + SharedFunctions.GetResourceString("SQLText2"), SharedFunctions.GetResourceString("SqlError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.sqlQueryTextBox.Focus();
                }
            }
            else
            {
                this.ResultGridView.DataSource = null;
                this.ResultGridView.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                MessageBox.Show(SharedFunctions.GetResourceString("SQLText") + "\n" + SharedFunctions.GetResourceString("SQLText1") + "\n" + SharedFunctions.GetResourceString("SQLText2"), SharedFunctions.GetResourceString("SqlError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.sqlQueryTextBox.Focus();
            }
        }

        /// <summary>
        /// Method to Load SQLCombo
        /// </summary>
        private void LoadSqlCombo()
        {
            this.SqlCategoryCombo.DisplayMember = "category";
            this.SqlCategoryCombo.ValueMember = "categoryID";
            DataRow dr = this.sqlCategoryDataTable.NewRow();
            dr[this.sqlCategoryDataTable.CategoryIDColumn.ColumnName] = 0;
            dr[this.sqlCategoryDataTable.CategoryColumn.ColumnName] = "---Select---";
            this.sqlCategoryDataTable.Rows.Add(dr);
            this.sqlCategoryDataTable.Merge(this.F9015Controll.WorkItem.GetSqlCatagory);
            this.SqlCategoryCombo.DataSource = this.sqlCategoryDataTable;
            this.sqlQueryTextBox.Focus();
        }

        /// <summary>
        /// Selection Change Committed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void SqlCategoryCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.SqlCategoryCombo.SelectedIndex != 0)
                {
                    ////Coding added for the co : 4776 by malliga
                    ////SQLLibraryCombo Autocomplete is not set
                    this.SqlLibraryCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
                    this.SqlLibraryCombo.DataSource = null;
                    this.LoadDescriptionCombo();
                    ////After loading the combo box autocomplete is set
                    this.SqlLibraryCombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                    this.SqlLibraryCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                else
                {
                    this.SqlLibraryCombo.DataSource = null;
                    this.SqlLibraryCombo.DisplayMember = "Description";
                    this.SqlLibraryCombo.ValueMember = "SQLID";
                    this.sqlDescriptionDataTable.Rows.Clear();
                    if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                    {
                        this.sqlRunButton.Enabled = true;
                    }
                    else
                    {
                        this.sqlRunButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Method to Save SQL Statement
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void SqlSaveButton_Click(object sender, EventArgs e)
        {
            if (this.sqlSaveButton.Enabled && this.PermissionFiled.editPermission)
            {
                try
                {
                    object[] optionalParameter = null;
                    if (this.sqlDescriptionDataTable.Rows.Count != 0)
                    {
                        this.prevSqlCategory = this.SqlCategoryCombo.SelectedValue.ToString();
                        this.prevSqlDescription = this.SqlLibraryCombo.SelectedValue.ToString();
                        this.prevQuery = this.sqlQueryTextBox.Text.Trim();
                    }
                    else
                    {
                        this.prevSqlCategory = "0";
                        this.prevSqlDescription = string.Empty;
                        this.prevQuery = string.Empty;
                    }

                    if (this.SqlCategoryCombo.SelectedIndex > 0)
                    {
                        if (!string.IsNullOrEmpty(this.SqlLibraryCombo.Text.ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                            {
                                optionalParameter = new object[] { this.SqlCategoryCombo.SelectedValue.ToString(), this.SqlLibraryCombo.Text.ToString(), this.sqlQueryTextBox.Text.Trim() };
                            }
                            else
                            {
                                optionalParameter = new object[] { this.SqlCategoryCombo.SelectedValue.ToString(), this.SqlLibraryCombo.Text.ToString(), string.Empty };
                            }
                        }
                        else if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                        {
                            optionalParameter = new object[] { this.SqlCategoryCombo.SelectedValue.ToString(), string.Empty, this.sqlQueryTextBox.Text.Trim() };
                        }
                        else
                        {
                            optionalParameter = new object[] { this.SqlCategoryCombo.SelectedValue.ToString(), string.Empty, string.Empty };
                        }
                    }
                    else if (!string.IsNullOrEmpty(this.SqlLibraryCombo.Text.ToString()))
                    {
                        if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                        {
                            optionalParameter = new object[] { "0", this.SqlLibraryCombo.Text.ToString(), this.sqlQueryTextBox.Text.Trim() };
                        }
                        else
                        {
                            optionalParameter = new object[] { "0", this.SqlLibraryCombo.Text.ToString(), string.Empty };
                        }
                    }
                    else if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                    {
                        optionalParameter = new object[] { "0", string.Empty, this.sqlQueryTextBox.Text.Trim() };
                    }
                    else
                    {
                        optionalParameter = new object[] { "0", string.Empty, string.Empty };
                    }

                    Form querySave = new Form();
                    querySave = this.form9015Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9016, optionalParameter, this.form9015Control.WorkItem);
                    if (querySave != null)
                    {
                        querySave.ShowDialog();
                        if (this.SqlCategoryCombo.SelectedIndex > 0)
                        {
                            this.LoadDescriptionCombo();
                            if (this.sqlDescriptionDataTable.Rows.Count > 1)
                            {
                                DataView descriptionView = new DataView(this.sqlDescriptionDataTable);
                                descriptionView.Sort = this.sqlDescriptionDataTable.SQLIDColumn.ColumnName;
                                int find = descriptionView.Find(this.prevSqlDescription);
                                if (find > 0)
                                {
                                    this.SqlCategoryCombo.SelectedValue = Convert.ToInt32(this.prevSqlCategory);
                                    this.SqlLibraryCombo.SelectedValue = this.prevSqlDescription;
                                    this.sqlQueryTextBox.Text = this.prevQuery.Trim();
                                }
                                else
                                {
                                    this.SqlCategoryCombo.SelectedValue = Convert.ToInt32(this.prevSqlCategory);
                                    this.SqlLibraryCombo.SelectedValue = 0;
                                    this.sqlQueryTextBox.Text = this.prevQuery.Trim();
                                }
                            }
                            else
                            {
                                this.SqlCategoryCombo.SelectedValue = Convert.ToInt32(this.prevSqlCategory);
                                this.SqlLibraryCombo.SelectedValue = 0;
                                this.sqlQueryTextBox.Text = this.prevQuery.Trim(); ////.Replace("\n", " ");                                
                            }
                        }                        
                    }
                }
                catch (SoapException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Selection Change Committed Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void SqlLibraryCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.SqlLibraryCombo.SelectedIndex >= 1 && !string.IsNullOrEmpty(this.SqlLibraryCombo.SelectedValue.ToString()))
                {
                    this.sqlStringDataTable = this.F9015Controll.WorkItem.GetSqlString(Convert.ToInt32(this.SqlCategoryCombo.SelectedValue.ToString()), Convert.ToInt32(this.SqlLibraryCombo.SelectedValue.ToString()));
                    if (this.sqlStringDataTable.Rows.Count != 0)
                    {
                        this.sqlQueryTextBox.Text = this.sqlStringDataTable.Rows[0][sqlStringDataTable.SQLStringColumn.ColumnName].ToString(); ////.Replace("\n"," ");
                        this.sqlRunButton.Enabled = true;
                    }                                       
                }
                ////else
                ////{
                ////    this.sqlQueryTextBox.Text = string.Empty;
                ////    this.sqlRunButton.Enabled = false;
                ////}
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
        /// sqlQueryTextBox KeyDown Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        private void SqlQueryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                {
                    this.sqlRunButton.Enabled = true;
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.SqlRunButton_Click(sender, null);
                        this.sqlQueryTextBox.Text = this.sqlQueryTextBox.Text.Trim();
                        this.sqlRunButton.Focus();
                    }
                }
                else
                {
                    this.sqlRunButton.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sql PreviousButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void PreviousButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPos <= (this.count - 1))
                {
                    this.flag++;
                    this.NextButton.Enabled = true;
                    --currentPos;
                    if (this.dupFlag == true)
                    {
                        this.sqlQueryTextBox.Text = this.sqlString[currentPos + 1].Trim();
                        currentPos++;
                    }
                    else
                    {
                        this.sqlQueryTextBox.Text = this.sqlString[currentPos].Trim();
                    }

                    if (this.dupFlag == false)
                    {
                        if (currentPos == 0)
                        {
                            this.PreviousButton.Enabled = false;
                            this.sqlRunButton.Focus();
                        }
                        else
                        {
                            this.PreviousButton.Enabled = true;
                            this.sqlRunButton.Focus();
                        }
                    }
                }
                else
                {
                    this.PreviousButton.Enabled = false;
                    this.sqlRunButton.Focus();
                }

                this.dupFlag = false;
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sql NextButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void NextButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPos <= (this.count - 1))
                {
                    this.flag--;
                    this.PreviousButton.Enabled = true;
                    currentPos++;
                    this.sqlQueryTextBox.Text = this.sqlString[currentPos].Trim();
                    if (currentPos == (this.count - 1))
                    {
                        this.NextButton.Enabled = false;
                        this.sqlRunButton.Focus();
                    }
                    else
                    {
                        this.NextButton.Enabled = true;
                        this.sqlRunButton.Focus();
                    }
                }
                else
                {
                    this.NextButton.Enabled = false;
                    this.sqlRunButton.Focus();
                }

                this.dupFlag = false;
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// QueryTextBox TextChanged Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void SqlQueryTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                {
                    this.sqlRunButton.Enabled = true;
                }
                else
                {
                    this.sqlRunButton.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region ResultGridView Event

        /// <summary>
        /// Handles the InitializeLayout event of the ResultGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void ResultGridView_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
                //// Turn on Copy functionality. 
                e.Layout.Override.AllowMultiCellOperations = AllowMultiCellOperation.Copy;
                e.Layout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButtonFixedSize;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeColumnChooserDisplayed event of the ResultGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeColumnChooserDisplayedEventArgs"/> instance containing the event data.</param>
        private void ResultGridView_BeforeColumnChooserDisplayed(object sender, BeforeColumnChooserDisplayedEventArgs e)
        {
            e.Cancel = true;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Form fieldManagementForm = new Form();
                fieldManagementForm = this.form9015Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9034, null, this.form9015Control.WorkItem);
                TerraScanCommon.SetValue(fieldManagementForm, "Grid", this.ResultGridView);
                if (fieldManagementForm != null)
                {
                    fieldManagementForm.ShowDialog();
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

        #endregion

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ResultGridView.Rows.Count > 0)
                {
                    this.ResultPreviewDialog.ShowDialog(this);
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the ResultPreviewDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ResultPreviewDialog_Load(object sender, EventArgs e)
        {
            try
            {
                // Loads the Document Name
                this.ResultPreviewDialog.Document.DocumentName = "Preview";
                this.ResultPreviewDialog.Text = "Preview";
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ExcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ResultGridView.Rows.Count > 0)
                {
                    Workbook wb = new Workbook();
                    Worksheet ws = wb.Worksheets.Add("Sheet 1");
                    string format = "yyyy/MM/dd_hh:mm:ss";
                    string strMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string filename = "SQL" + DateTime.Now.ToString(format) + ".XLS";
                    filename = filename.Replace(":", "");
                    filename = filename.Replace("/", "");
                    DirectoryInfo dirInfo = new DirectoryInfo(strMyDocuments + @"\TerraScan");
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }

                    filename = dirInfo.ToString() + "\\" + filename;
                    this.ExcelExporter.Export(this.ResultGridView, filename);
                    Process excel = new Process();
                    excel.StartInfo.Arguments = "\"" + strMyDocuments + "\" /e";
                    excel.StartInfo.FileName = filename;
                    excel.Start();
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

        #endregion        

        #region LoadDescriptionCombo

        /// <summary>
        /// Loads the description combo.
        /// </summary>
        private void LoadDescriptionCombo()
        {
            this.sqlDescriptionDataTable.Clear();
            DataRow dr = this.sqlDescriptionDataTable.NewRow();
            dr[this.sqlDescriptionDataTable.SQLIDColumn.ColumnName] = 0;
            dr[this.sqlDescriptionDataTable.DescriptionColumn.ColumnName] = "---Select---";
            this.sqlDescriptionDataTable.Rows.Add(dr);
            this.sqlDescriptionDataTable.AcceptChanges(); 
            DataTable tempDataTable = new DataTable();
            tempDataTable = this.F9015Controll.WorkItem.GetSqlDescription(Convert.ToInt32(this.SqlCategoryCombo.SelectedValue.ToString()));
            if (tempDataTable.Rows.Count > 0)
            {
                this.sqlDescriptionDataTable.Merge(tempDataTable, true);
            }

            this.SqlLibraryCombo.DataSource = this.sqlDescriptionDataTable;
            this.SqlLibraryCombo.DisplayMember = "Description";
            this.SqlLibraryCombo.ValueMember = "SQLID";
        }

        #endregion

      #region Coding added for the co : 4776 by malliga

        /// <summary>
        /// Handles the Validating event of the SqlCategoryCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SqlCategoryCombo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.SqlCategoryCombo.SelectedIndex > 0)
                {
                    this.LoadDescriptionCombo();
                }
                else
                {
                    this.SqlLibraryCombo.DataSource = null;
                    this.SqlLibraryCombo.DisplayMember = "Description";
                    this.SqlLibraryCombo.ValueMember = "SQLID";
                    ////this.sqlQueryTextBox.Text = string.Empty;
                    this.sqlDescriptionDataTable.Rows.Clear();
                    if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                    {
                        this.sqlRunButton.Enabled = true;
                    }
                    else
                    {
                        this.sqlRunButton.Enabled = false;
                    }
                    this.SqlCategoryCombo.SelectedValue = 0;
                   //// this.SqlCategoryCombo.Focus(); 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the SqlLibraryCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SqlLibraryCombo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.SqlLibraryCombo.SelectedIndex >= 1 && !string.IsNullOrEmpty(this.SqlLibraryCombo.SelectedValue.ToString()))
                {
                    this.sqlStringDataTable = this.F9015Controll.WorkItem.GetSqlString(Convert.ToInt32(this.SqlCategoryCombo.SelectedValue.ToString()), Convert.ToInt32(this.SqlLibraryCombo.SelectedValue.ToString()));
                    if (this.sqlStringDataTable.Rows.Count != 0)
                    {
                        this.sqlQueryTextBox.Text = this.sqlStringDataTable.Rows[0][sqlStringDataTable.SQLStringColumn.ColumnName].ToString(); ////.Replace("\n"," ");
                        this.sqlRunButton.Enabled = true;
                    }
                }
                else
                {
                    if (this.SqlLibraryCombo.DataSource == null)
                    {
                        this.SqlLibraryCombo.Text = string.Empty;
                    }
                    else
                    {
                        this.SqlLibraryCombo.SelectedValue = 0;
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
        }
     #endregion

    }
}