//--------------------------------------------------------------------------------------------
// <copyright file="F9016.cs" company="Congruent">
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
// 06 Sep 06        VINOTH             Created
// 19 Dec 06        VINOTH             Added Delete Functionality
// 19 Dec 06        VINOTH             Added Description Grid 
//*********************************************************************************/

namespace D9000
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.ObjectBuilder;
    using System.Web.Services.Protocols;

    #endregion

    /// <summary>
    /// F90151 Class
    /// </summary>
    public partial class F9016 : BasePage
    {
        #region Variable

        /////// <summary>
        /////// Created Boolean value for ValueChanged.
        /////// </summary>
        ////private bool valueChanged;

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
        /// Created string for sqlQueryText
        /// </summary>
        private string sqlQueryText = string.Empty;

        /// <summary>
        /// Created Controller for F9016Controller
        /// </summary>
        private F9016Controller form9016Control;

        /////// <summary>
        /////// Object for DataTable created
        /////// </summary>
        ////private DataTable sqlQuery = new DataTable();

        /// <summary>
        /// Created instance for SQLSupportData GetSqlCategoryDataTable
        /// </summary>
        private SQLSupportData.ListSqlCategoryDataTable sqlCategory = new SQLSupportData.ListSqlCategoryDataTable();

        /// <summary>
        /// Created instance for SQLSupportData ListSqlDescriptionDataTable
        /// </summary>
        private SQLSupportData.ListSqlDescriptionDataTable listDescription = new SQLSupportData.ListSqlDescriptionDataTable();

        /// <summary>
        /// Created instance for SQLSupportData GetSqlStringDataTable
        /// </summary>
        private SQLSupportData.GetSqlStringDataTable sqlString = new SQLSupportData.GetSqlStringDataTable();

        /// <summary>
        /// Stores CurrentRow Index
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// To store category
        /// </summary>
        private int category;

        /// <summary>
        /// To store Description
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// To store Query
        /// </summary>
        private string query = string.Empty;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        
        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields currentFormPermissionField;

        /// <summary>
        /// Used to store the current form No
        /// </summary>
        private int currentFormNo = 9016;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public F9016()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// F90151 Constructor
        /// </summary>
        /// <param name="categoryId">category</param>
        /// <param name="description">description</param>
        /// <param name="queryValue">queryString</param>
        public F9016(string categoryId, string description, string queryValue)
        {
            this.InitializeComponent();

            // Assign the value fom Construtor to local variable
            this.category = Convert.ToInt32(categoryId);
            if (description.ToLower().Equals("---Select---"))
            {
                this.description = string.Empty;
            }
            else
            {
                this.description = description;
            }
            this.query = queryValue;
        }        

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F9015 controll.
        /// </summary>
        /// <value>The F9015 controll.</value>
        [CreateNew]
        public F9016Controller F9016Controll
        {
            get { return this.form9016Control as F9016Controller; }
            set { this.form9016Control = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// F90151 Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void QuerySave_Load(object sender, EventArgs e)
        {
            try
            {
                this.getFormDetailsDataDetails = this.form9016Control.WorkItem.GetFormDetails(currentFormNo, TerraScanCommon.UserId);

                if (this.getFormDetailsDataDetails.Rows.Count > 0)
                {
                    this.currentFormPermissionField.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                    this.currentFormPermissionField.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
                    this.currentFormPermissionField.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
                    this.currentFormPermissionField.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
                }

                this.DeleteSQLButton.Enabled = false;
                this.SqlCategoryCombo.DisplayMember = "category";
                this.SqlCategoryCombo.ValueMember = "CategoryID";
                this.LoadCombo();
                this.SaveMenu.Visible = false;
                this.SaveMenu.Visible = false;
                if (this.SaveSQLButton.ActualPermission == true)
                {
                    this.SaveSQLButton.Enabled = this.currentFormPermissionField.editPermission;
                    this.CancelSQLButton.Enabled = this.currentFormPermissionField.editPermission;
                }

                this.SaveMenu.Click += new EventHandler(this.SaveSQLButton_Click);
                this.PopulateGrid();
                this.CancelButton = this.CancelSQLButton;
                ////this.sqlDescriptionTextBox.Focus();
            }
            catch (SoapException e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Load Category Combo
        /// </summary>
        private void LoadCombo()
        {
            DataRow dr = this.sqlCategory.NewRow();
            dr[this.sqlCategory.CategoryIDColumn.ColumnName] = 0;
            dr[this.sqlCategory.CategoryColumn.ColumnName] = "---Select---";
            this.sqlCategory.Rows.Add(dr);
            this.sqlCategory.Merge(this.F9016Controll.WorkItem.GetSqlCatagory);
            this.SqlCategoryCombo.DataSource = this.sqlCategory;
            ////this.sqlDescriptionTextBox.Focus();
            ////SqlCategoryCombo.DataSource = F90151WorkItem.GetSqlCatagory();
            this.SqlCategoryCombo.SelectedValue = this.category;
            this.sqlDescriptionTextBox.Text = string.Empty;
            this.sqlQueryTextBox.Text = this.query;            
        }

        /// <summary>
        /// Save SQLButton Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void SaveSQLButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.sqlDescriptionTextBox.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                    {
                        if (this.SqlCategoryCombo.SelectedIndex > 0)
                        {
                            // Checks whether sqlQueryTextBox has value or not.
                            if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()) && this.sqlQueryTextBox.Text.Trim().Length > 7)
                            {
                                // Gets the first 6 character of the Query to Check it is valid or not.
                                this.sqlQueryText = this.sqlQueryTextBox.Text.Trim().Substring(0, 6).ToLower();

                                if (string.Compare(this.sqlQueryText, this.selectValue) == 0 || string.Compare(this.sqlQueryText, this.insertValue) == 0 || string.Compare(this.sqlQueryText, this.deleteValue) == 0 || string.Compare(this.sqlQueryText, this.updateValue) == 0)
                                {
                                    this.SaveRecords();
                                }
                                else
                                {
                                    ////MessageBox.Show("Invalid Query", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                                    MessageBox.Show(SharedFunctions.GetResourceString("SQLText") + "\n" + SharedFunctions.GetResourceString("SQLText1") + "\n" + SharedFunctions.GetResourceString("SQLText2"), SharedFunctions.GetResourceString("SqlError"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            else
                            {
                                ////MessageBox.Show("Invalid Query", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                                MessageBox.Show(SharedFunctions.GetResourceString("SQLText") + "\n" + SharedFunctions.GetResourceString("SQLText1") + "\n" + SharedFunctions.GetResourceString("SQLText2"), SharedFunctions.GetResourceString("SqlError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.sqlQueryTextBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.SqlCategoryCombo.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.sqlQueryTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.sqlDescriptionTextBox.Focus();
                }
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Cancel SQLButton Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void CancelSQLButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (SoapException e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SaveRecords Method
        /// </summary>
        private void SaveRecords()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.sqlDescriptionTextBox.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(this.sqlQueryTextBox.Text.Trim()))
                    {
                        if (this.SqlCategoryCombo.SelectedIndex > 0)
                        {
                            if (this.DescriptionGrid.SelectedCells.Count > 0)
                            {
                                this.F9016Controll.WorkItem.SaveSqlQuery(Convert.ToInt32(this.SqlCategoryCombo.SelectedValue.ToString()), this.sqlDescriptionTextBox.Text.Trim(), this.sqlQueryTextBox.Text.Trim(), Convert.ToInt32(Tag.ToString()), TerraScan.Common.TerraScanCommon.UserId, this.currentRowIndex);
                                this.Close();
                            }
                            else
                            {
                                this.F9016Controll.WorkItem.SaveSqlQuery(Convert.ToInt32(this.SqlCategoryCombo.SelectedValue.ToString()), this.sqlDescriptionTextBox.Text.Trim(), this.sqlQueryTextBox.Text.Trim(), Convert.ToInt32(Tag.ToString()), TerraScan.Common.TerraScanCommon.UserId, 0);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }        

        /// <summary>
        /// sqlQueryTextBox KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyPressEventArgs</param>
        private void SqlQueryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    e.Handled = true;
                }
            }
            catch (SoapException e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the grid.
        /// </summary>
        private void PopulateGrid()
        {
            DataGridViewColumnCollection columns = this.DescriptionGrid.Columns;
            this.listDescription = this.form9016Control.WorkItem.GetSqlDescription(this.category);
            columns["SQLID"].DataPropertyName = this.listDescription.SQLIDColumn.ColumnName;
            columns["Descript"].DataPropertyName = this.listDescription.DescriptionColumn.ColumnName;

            columns["Descript"].DisplayIndex = 0;

            this.DescriptionGrid.DataSource = this.listDescription;
            if (this.listDescription.Rows.Count > 0)
            {
                this.DeleteSQLButton.Enabled = this.currentFormPermissionField.deletePermission;
                this.DescriptionGrid.Rows[0].Selected = false;
            }

            if (this.listDescription.Rows.Count > this.DescriptionGrid.NumRowsVisible)
            {
               this.DescriptionVScroll.Visible = false;
            }
            else
            {
                this.DescriptionVScroll.Visible = true;
            }            
        }              

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the SqlCategoryCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SqlCategoryCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Int32 selectedValue = 0;
                if (this.SqlCategoryCombo.SelectedIndex > 0)
                {
                    if (Int32.TryParse(this.SqlCategoryCombo.SelectedValue.ToString(), out selectedValue))
                    {
                        this.listDescription = this.form9016Control.WorkItem.GetSqlDescription(selectedValue);
                        this.DescriptionGrid.DataSource = this.listDescription;
                    }

                    if (this.listDescription.Rows.Count > this.DescriptionGrid.NumRowsVisible)
                    {
                        this.DescriptionVScroll.Visible = false;
                    }
                    else
                    {
                        this.DescriptionVScroll.Visible = true;
                    }

                    if (this.listDescription.Rows.Count > 0)
                    {
                        this.DeleteSQLButton.Enabled = this.currentFormPermissionField.deletePermission;
                        this.DescriptionGrid.Rows[0].Selected = false;
                    }
                    else
                    {
                        this.DeleteSQLButton.Enabled = false;
                    }
                }
                else
                {
                    this.listDescription.Clear();
                    this.DescriptionGrid.DataSource = this.listDescription;
                    this.DeleteSQLButton.Enabled = false;
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
        /// Handles the CellClick event of the DescriptionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DescriptionGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int categoryId;
                this.currentRowIndex = 0;
                if (e.RowIndex != -1)
                {
                    if (Int32.TryParse(this.DescriptionGrid.Rows[e.RowIndex].Cells[this.listDescription.SQLIDColumn.ColumnName].Value.ToString(), out this.currentRowIndex) && Int32.TryParse(this.SqlCategoryCombo.SelectedValue.ToString(), out categoryId))
                    {
                        this.sqlString = this.form9016Control.WorkItem.GetSqlString(categoryId, this.currentRowIndex);
                        if (this.sqlString.Rows.Count > 0)
                        {
                            this.sqlDescriptionTextBox.Text = this.sqlString.Rows[0][sqlString.DescriptionColumn.ColumnName].ToString();
                            this.sqlQueryTextBox.Text = this.sqlString.Rows[0][sqlString.SQLStringColumn.ColumnName].ToString();
                        }
                    }
                }
            }
            catch (SoapException e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }   

        /// <summary>
        /// Handles the Click event of the DeleteSQLButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteSQLButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DescriptionGrid.Rows.Count > 0 && this.sqlString.Rows.Count > 0)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.form9016Control.WorkItem.F9015_DeleteQuery(this.currentRowIndex, TerraScanCommon.UserId);
                        this.Close();
                    }
                    else
                    {
                        return;
                    }                    
                }
                else
                {
                    MessageBox.Show("Select any Description to Delete", ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);                    
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