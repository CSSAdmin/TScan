//--------------------------------------------------------------------------------------------
// <copyright file="F9110.cs" company="Congruent">
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
// 20120410         Manoj Kumar P       Removed the .Replace("'","''") for Database Execution retrieves nothing
//*********************************************************************************/

namespace D9100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Helper;

    /// <summary>
    /// F9110 class
    /// </summary>
    public partial class F9110 : Form
    {
        #region Variables

        /// <summary>
        /// masterNameSearch instance
        /// </summary>
        private F9110MasterNameSearchData masterNameSearch = new F9110MasterNameSearchData();

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// Created Instance for F9110Controller
        /// </summary>
        private F9110Controller form9110Control;

        /// <summary>
        /// masterNameOwnerId
        /// </summary>
        private string masterNameOwnerId;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// masternamesearchDatatable
        /// </summary>
        private DataTable masternamesearchDatatable = new DataTable();

        /// <summary>
        /// masternamesearchdatarow
        /// </summary>
        private DataRow masternamesearchdatarow;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f9101"/> class.
        /// </summary>
        public F9110()
        {
            this.InitializeComponent();
            this.CancelButton = this.MasterNameCancelButton;
            this.AcceptButton = this.SearchButton;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F9110Controller F9110Controll
        {
            get { return this.form9110Control as F9110Controller; }
            set { this.form9110Control = value; }
        }

        /// <summary>
        /// Gets or sets the master name owner id.
        /// </summary>
        /// <value>The master name owner id.</value>
        public string MasterNameOwnerId
        {
            get { return this.masterNameOwnerId; }
            set { this.masterNameOwnerId = value; }
        }

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return this.commandResult; }
            set { this.commandResult = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.masterNameSearch.ListMasterNameDataTable.Rows.Count > 0)
                {
                    if (this.MasterNameDataGridView.SelectedRows.Count > 0)
                    {
                        this.selected = this.MasterNameDataGridView.SelectedRows[0].Index;
                    }
                    else if (this.MasterNameDataGridView.SelectedCells.Count > 0)
                    {
                        this.selected = this.MasterNameDataGridView.CurrentCell.RowIndex;
                    }

                    return this.selected;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.masterNameSearch.Tables[0].Rows.Count > 5)
            {
                this.MasterNameVerticalScroll.Enabled = true;
                this.MasterNameVerticalScroll.Visible = false;
                this.ScrollPanel.SendToBack();
            }
            else
            {
                this.MasterNameVerticalScroll.Enabled = false;
                this.MasterNameVerticalScroll.Visible = true;
                this.ScrollPanel.BringToFront();
                this.MasterNameVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.MasterNameDataGridView.IsMultiSelect = true;
            this.MasterNameDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MasterNameDataGridView.AllowUserToResizeColumns = false;
            this.MasterNameDataGridView.AutoGenerateColumns = false;
            this.MasterNameDataGridView.AllowUserToResizeRows = false;
            this.MasterNameDataGridView.StandardTab = true;
            this.MasterNameDataGridView.PrimaryKeyColumnName = "OwnerID";
            
            this.MasterNameDataGridView.Columns[0].DataPropertyName = "LastName";
            this.MasterNameDataGridView.Columns[1].DataPropertyName = "FirstName";
            this.MasterNameDataGridView.Columns[2].DataPropertyName = "Address";
            this.MasterNameDataGridView.Columns[3].DataPropertyName = "OwnerID";
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            // if (this.LastNameTextBox.Text.Trim().Length > 0 || this.FirstNameTextBox.Text.Trim().Length > 0 || this.AddressTextBox.Text.Trim().Length > 0)
            if (!string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.FirstNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.AddressTextBox.Text.Trim()))
            {
                this.SearchButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;

                if (this.masterNameSearch.Tables[0].Rows.Count <= 0)
                {
                    this.AcceptMasterNameButton.Enabled = false;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.AcceptMasterNameButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Gets the owner id.
        /// </summary>
        private void GetOwnerId()
        {
            int rowId = 0;
            rowId = this.GetRowIndex();
         
            if (!this.emptyRecord)
            {
                if (this.masternamesearchDatatable.Columns.Count == 0)
                {
                  this.masternamesearchDatatable.Columns.Add("OwnerId");
                }

                for (int i = 0; i <= this.MasterNameDataGridView.OriginalRowCount - 1; i++)
                {
                    if (this.MasterNameDataGridView.Rows[i].Selected)
                    {
                        if (!string.IsNullOrEmpty(this.MasterNameDataGridView.Rows[i].Cells["OwnerId"].Value.ToString().Trim()))
                        {
                            this.masternamesearchdatarow = this.masternamesearchDatatable.NewRow();
                            this.masternamesearchdatarow["OwnerId"] = this.MasterNameDataGridView.Rows[i].Cells["OwnerId"].Value.ToString().Trim();
                            this.masternamesearchDatatable.Rows.Add(this.masternamesearchdatarow);
                            this.commandResult = TerraScanCommon.GetXmlString(this.masternamesearchDatatable);
                            this.DialogResult = DialogResult.Yes;
                            this.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            this.LastNameTextBox.Text = string.Empty;
            this.FirstNameTextBox.Text = string.Empty;
            this.AddressTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
            this.SearchButton.Enabled = false;
            this.AcceptMasterNameButton.Enabled = false;
            this.ClearButton.Enabled = false;
        }

        #endregion

        #region EventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion EventPublication

        #region Events

        /// <summary>
        /// Handles the Load event of the f9110 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9110_Load(object sender, EventArgs e)
        {
            try
            {
                // this.LoadWorkSpaces();
                this.emptyRecord = true;
                this.CustomizeDataGrid();
                this.MasterNameDataGridView.DataSource = null;
                this.MasterNameDataGridView.Enabled = false;
                this.MasterNameDataGridView.Rows[0].Selected = false;
                this.ClearFields();
                this.DisableButtons();
                this.DisableVScrollBar();
                this.LastNameTextBox.Focus();
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
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.SearchButton.Enabled)
                {
                    int recordCount = 0;
                    //Removed the .Replace("'","''") for Database Execution retrieves nothing
                    string lastName = this.LastNameTextBox.Text.Trim();
                    string firstName = this.FirstNameTextBox.Text.Trim();
                    string address = this.AddressTextBox.Text.Trim();

                    
                    if (!(string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(address)))
                    {
                        this.masterNameSearch = this.form9110Control.WorkItem.F9110MasterNameSearch(lastName, firstName, address);
                        ////this.masterNameSearch = F9101WorkItem.GetMasterNameSearch(this.LastNameTextBox.Text.Trim(), this.FirstNameTextBox.Text.Trim(), this.AddressTextBox.Text.Trim());

                        if (this.masterNameSearch.ListMasterNameDataTable.Rows.Count > 0)
                        {
                            recordCount = this.masterNameSearch.ListMasterNameDataTable.Rows.Count;
                            this.MasterNameDataGridView.Enabled = true;
                            this.MasterNameDataGridView.DataSource = this.masterNameSearch.ListMasterNameDataTable;
                            this.MasterNameDataGridView.Focus();
                            this.MasterNameDataGridView.Rows[0].Selected = true;
                            this.AcceptMasterNameButton.Enabled = true;
                            ////this.MasterNameDataGridView.Focus();
                            this.emptyRecord = false;
                        }
                        else
                        {
                            this.MasterNameDataGridView.DataSource = this.masterNameSearch.ListMasterNameDataTable;
                            this.MasterNameDataGridView.Rows[0].Selected = false;
                            this.MasterNameDataGridView.Enabled = false;
                            this.emptyRecord = true;
                            this.AcceptMasterNameButton.Enabled = false;
                        }

                        this.RecordCountLabel.Text = recordCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
                        this.DisableVScrollBar();
                    }
                    else
                    {
                        ////MessageBox.Show(SharedFunctions.GetResourceString("9101InvalidInput"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.RecordCountLabel.Text = 0 + SharedFunctions.GetResourceString("9101MasterNameSearch");
                    }
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
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MasterNameCancelButton.Enabled)
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
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
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.masterNameSearch.ListMasterNameDataTable.Clear();
                this.MasterNameDataGridView.DataSource = null;
                this.MasterNameDataGridView.Enabled = false;
                this.MasterNameDataGridView.Rows[0].Selected = false;
                this.ClearFields();
                this.DisableButtons();
                this.DisableVScrollBar();
                this.LastNameTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        /// <summary>
        /// Handles the CellDoubleClick event of the MasterNameDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MasterNameDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               if (!this.emptyRecord)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (!string.IsNullOrEmpty(this.MasterNameDataGridView.Rows[e.RowIndex].Cells["OwnerId"].Value.ToString()))
                        {
                            if (this.masternamesearchDatatable.Columns.Count == 0)
                            {
                              this.masternamesearchDatatable.Columns.Add("OwnerId");
                            }

                            for (int i = 0; i <= this.MasterNameDataGridView.OriginalRowCount - 1; i++)
                            {
                                if (this.MasterNameDataGridView.Rows[i].Selected)
                                {
                                    if (!string.IsNullOrEmpty(this.MasterNameDataGridView.Rows[i].Cells["OwnerId"].Value.ToString().Trim()))
                                    {
                                        this.masternamesearchdatarow = this.masternamesearchDatatable.NewRow();
                                        this.masternamesearchdatarow["OwnerId"] = this.MasterNameDataGridView.Rows[i].Cells["OwnerId"].Value.ToString().Trim();
                                        this.masternamesearchDatatable.Rows.Add(this.masternamesearchdatarow);
                                    }
                                }
                            }

                            this.commandResult = TerraScanCommon.GetXmlString(this.masternamesearchDatatable);
                            this.DialogResult = DialogResult.Yes;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the Click event of the AcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    this.GetOwnerId();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the MasterNameLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void MasterNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ///Changes to open the form Name\Address Management Form for the CO:10531
                ////Master Name Address Form - FormID - 91000
                FormInfo formInfo = TerraScanCommon.GetFormInfo(91000);
                formInfo.optionalParameters = new object[] { null };
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close(); 
         

                //Form form9100 = new Form();
            //    form9100 = this.form9110Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(91000, null, this.F9110Controll.WorkItem);

            //    if (form9100 != null)
            //    {
            //        form9100.ShowDialog();
            //    }
            }
            catch (Exception)
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
            }
            finally
            {
                this.MasterNameDataGridView.Focus();
            }
        }

        /// <summary>
        /// Edits the text.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditText(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.FirstNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.AddressTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.masterNameSearch.ListMasterNameDataTable.Rows.Count > 0)
                    {
                        this.SearchButton.Enabled = false;
                    }
                    else
                    {
                        this.DisableButtons();
                    }
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

        #endregion
    }
}