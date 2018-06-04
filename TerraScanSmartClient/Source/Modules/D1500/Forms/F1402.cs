//--------------------------------------------------------------------------------------------
// <copyright file="F1402.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1402.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/3/2008       R.Malliga            Created
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
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
    
    /// <summary>
    /// F1402
    /// </summary>
    public partial class F1402 : Form
    {
        #region Variables
        /// <summary>
        /// controller F1402Controller
        /// </summary>
        private F1402Controller form1402Control;

        /// <summary>
        /// F1402ScheduleSelectionData
        /// </summary>
        private F1402ScheduleSelectionData scheduleSelectionData = new F1402ScheduleSelectionData();

        /// <summary>
        /// Used to store the no of rows in the grid
        /// </summary>
        private int scheduleSelectionGridRowCount = 0;

        /// <summary>
        /// RollYear
        /// </summary>
        private string rollYear;

        /// <summary>
        /// ScheduleID
        /// </summary>
        private string scheduleID;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// For Selected value
        /// </summary>
        private string commandValue;

        /// <summary>
        /// Form No
        /// </summary>
        private string formNum = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1402"/> class.
        /// </summary>
        public F1402()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3510"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F1402(int rollYear, string masterFormNo)
        {
            this.InitializeComponent();
            this.rollYear = rollYear.ToString();
            this.formNum = masterFormNo.ToString();
        }


       #endregion
        
        #region Property
        /// <summary>
        /// For F1402Control
        /// </summary>
        [CreateNew]
        public F1402Controller Form1402Control
        {
            get { return this.form1402Control as F1402Controller; }
            set { this.form1402Control = value; }
        }

        /// <summary>
        /// Gets or sets the schedule id.
        /// </summary>
        /// <value>The schedule id.</value>
        public string ScheduleId
        {
            get { return this.scheduleID; }
            set { this.scheduleID = value; }
        }

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return commandResult; }
            set { commandResult = value; }
        }

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        /// <value>The command value.</value>
        public string CommandValue
        {
            get { return this.commandValue; }
            set { this.CommandValue = value; }
        }

        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F1402 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1402_Load(object sender, EventArgs e)
        {
            this.ClearScheduleSearch();
            this.CustomizeScheduleSelectionGrid();
            this.SettingReadOnlyForScheduleGrid();
            this.ScheduleSearchDataGridView.DataSource = null;
            this.ScheduleSearchDataGridView.Enabled = false;
            this.ScheduleSearchDataGridView.Rows[0].Selected = false;
            this.DisableButtons();
            if (!string.IsNullOrEmpty(this.rollYear))
            {
                this.RollYearTextBox.Text = this.rollYear;
                this.RollYearTextBox.Enabled = false;
                this.RollYearTextBox.LockKeyPress = true;
            }
            else
            {
                this.RollYearTextBox.Enabled = true;
                this.RollYearTextBox.LockKeyPress = false;
            }
        }
        #endregion

        #region Button Events
        /// <summary>
        /// Handles the Click event of the AcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.scheduleSelectionGridRowCount > 0)
                {
                    this.SelectScheduleId();
                    this.DialogResult = DialogResult.OK;
                }
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
                string fillDate = string.Empty;
                DateTime fillDateTime;

                if (!this.ValidateDate())
                {
                    MessageBox.Show("please enter valid date range." + "\n" + "Allowed formats: " + "m/d/yyyy." + "\n" + "Minimum value: " + "1/1/1900" + "\n" + "Minimum value: " + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.FilingDateTextBox.Text = "";
                    this.FilingDateTextBox.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(this.FilingDateTextBox.Text.Trim()))
                {
                    fillDateTime = Convert.ToDateTime(this.FilingDateTextBox.Text.Trim());
                    fillDate = fillDateTime.ToShortDateString();
                }

                if (fillDate != "")
                    {
                        int pos = fillDate.LastIndexOf("/");

                        string str1 = fillDate.Substring(pos + 1, 4);
                        if (Convert.ToInt32(str1) < 1900 || Convert.ToInt32(str1) > 2079)
                        {
                            MessageBox.Show("please enter valid date range." + "\n" + "Allowed formats: " + "m/d/yyyy." + "\n" + "Minimum value: " + "1/1/1900" + "\n" + "Minimum value: " + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.FilingDateTextBox.Text = "";
                            this.FilingDateTextBox.Focus();
                            return;
                        }
                    }
               
                if (this.RollYearTextBox.Text != "")
                {
                   int rollyear;
                   int.TryParse(this.RollYearTextBox.Text, out rollyear);

                   if (rollyear == 0)
                   {
                       MessageBox.Show("Year should be greater than 1899 and lesser than 2080", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                       this.RollYearTextBox.Text = "";
                       return;
                   }

                       if (Convert.ToInt32(this.RollYearTextBox.Text) < 1900 || Convert.ToInt32(this.RollYearTextBox.Text) > 2079)
                       {
                           MessageBox.Show("Year should be greater than 1899 and lesser than 2080", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                           this.RollYearTextBox.Text = "";
                           return;
                       }
                    }

             string schedulecondition = string.Empty;

             F1402ScheduleSelectionData scheduleSelectionData1 = new F1402ScheduleSelectionData();
              
             DataTable searchdt = new DataTable();
             searchdt = (DataTable)scheduleSelectionData1.GetScheduleSelection.Clone();

                DataRow searchdr;
                 searchdr = searchdt.NewRow();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName] = this.ScheduleNumberTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.ParcelNumberColumn.ColumnName] = this.refParcelNumberTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.StatementNumberColumn.ColumnName] = this.StatementNumberTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.OwnerNameColumn.ColumnName] = this.OwnerTextBox.Text.Trim();
                 if (this.RollYearTextBox.Text != "")
                 {
                     searchdr[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName] = this.RollYearTextBox.Text.Trim();
                 }
                 else
                 {
                     searchdr[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName] = DBNull.Value;
                 }

                 searchdr[this.scheduleSelectionData.GetScheduleSelection.StreetAddressColumn.ColumnName] = this.StreetAddressTextBox.Text.Trim();
                 if (this.FilingDateTextBox.Text != "")
                 {
                     searchdr[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName] = fillDate;
                 }
                 else
                 {
                     searchdr[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName] = DBNull.Value;
                 }

                 searchdr[this.scheduleSelectionData.GetScheduleSelection.NAICSColumn.ColumnName] = this.NAICSTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.LegalColumn.ColumnName] = this.LegalTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.DistrictColumn.ColumnName] = this.DistrictTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.DescriptionColumn.ColumnName] = this.DescriptionTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName] = this.BusinessNameTextBox.Text.Trim();
                 searchdr[this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName] = this.PropertyTypeTextBox.Text.Trim();
                 searchdt.Rows.Add(searchdr);

                 schedulecondition = TerraScanCommon.GetXmlString(searchdt);
            this.LoadScheduleSelectionGrid(schedulecondition);
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
                this.ClearScheduleSearch();
                this.ClearScheduleSearchGrid();
                this.CustomizeScheduleSelectionGrid();
                this.SettingReadOnlyForScheduleGrid();
                this.DisableButtons();
                this.ScheduleNumberTextBox.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ScheduleNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.ScheduleNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.refParcelNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.StatementNumberTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.OwnerTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.StreetAddressTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.FilingDateTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.NAICSTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.LegalTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.BusinessNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.PropertyTypeTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.scheduleSelectionGridRowCount > 0)
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

        #endregion

        #region Methods
        /// <summary>
        /// Clears the schedule search.
        /// </summary>
        private void ClearScheduleSearch()
        {
            try
            {
            this.ScheduleNumberTextBox.Text = string.Empty;
            this.refParcelNumberTextBox.Text = string.Empty;
            this.StatementNumberTextBox.Text = string.Empty;
            this.OwnerTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = Convert.ToString(this.rollYear);
            this.StreetAddressTextBox.Text = string.Empty;
            this.FilingDateTextBox.Text = string.Empty;   
            this.NAICSTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.DistrictTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.BusinessNameTextBox.Text = string.Empty;
            this.PropertyTypeTextBox.Text = string.Empty;
            this.RecordCountLabel.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        }
        }

        /// <summary>
        /// Clears the schedule search grid.
        /// </summary>
        private void ClearScheduleSearchGrid()
        {
            this.ScheduleSearchDataGridView.Enabled = false;
            this.ScheduleSearchDataGridView.DataSource = null;
            this.ScheduleSearchDataGridView.Rows[0].Selected = false;
            this.ScheduleAcceptButton.Enabled = false;
            ////this.ScheduleSelectionVerticalScroll.Visible = true; 
          }

          /// <summary>
          /// To disable the accept,search, and clear buttons
          /// </summary>        
          private void DisableButtons()
          {
              //this.SearchButton.Enabled = false;
              this.ScheduleAcceptButton.Enabled = false;
              this.ClearButton.Enabled = false;
          }

          /// <summary>
          /// Enables the search button.
          /// </summary>
        private void EnableSearchButton()
        {
            try
            {
            if (!string.IsNullOrEmpty(this.ScheduleNumberTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.refParcelNumberTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.StatementNumberTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.OwnerTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.StreetAddressTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.FilingDateTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.NAICSTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.LegalTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.BusinessNameTextBox.Text.Trim().Replace("'", "''")) || !string.IsNullOrEmpty(this.PropertyTypeTextBox.Text.Trim().Replace("'", "''")))
            {
                this.SearchButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;

                if (this.scheduleSelectionGridRowCount  <= 0)
                {
                    this.ScheduleAcceptButton.Enabled = false;
                    this.ClearButton.Enabled = false;
                }
                else
                {
                    this.ScheduleAcceptButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        }
        }

          /// <summary>
          /// Customizes the schedule selection grid.
          /// </summary>
          private void CustomizeScheduleSelectionGrid()
           {
              try
              {
            this.ScheduleSearchDataGridView.AutoGenerateColumns = false;

           this.ScheduleSearchDataGridView.PrimaryKeyColumnName = this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName;

            DataGridViewColumnCollection columns = this.ScheduleSearchDataGridView.Columns;

            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.StreetAddressColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.StreetAddressColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.NAICSColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.NAICSColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.DescriptionColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.DescriptionColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.ParcelNumberColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.ParcelNumberColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.StatementNumberColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.StatementNumberColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerIDColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.OwnerIDColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerNameColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.OwnerNameColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.LegalColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.LegalColumn.ColumnName;
            columns[this.scheduleSelectionData.GetScheduleSelection.DistrictColumn.ColumnName].DataPropertyName = this.scheduleSelectionData.GetScheduleSelection.DistrictColumn.ColumnName;

            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].DisplayIndex = 0;
            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].DisplayIndex = 1;
            columns[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName].DisplayIndex = 6;
            columns[this.scheduleSelectionData.GetScheduleSelection.StreetAddressColumn.ColumnName].DisplayIndex = 7;
            columns[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName].DisplayIndex = 8;
            columns[this.scheduleSelectionData.GetScheduleSelection.NAICSColumn.ColumnName].DisplayIndex = 9;
            columns[this.scheduleSelectionData.GetScheduleSelection.DescriptionColumn.ColumnName].DisplayIndex = 12;
            columns[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName].DisplayIndex = 13;
            columns[this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName].DisplayIndex = 14;
            columns[this.scheduleSelectionData.GetScheduleSelection.ParcelNumberColumn.ColumnName].DisplayIndex = 2;
            columns[this.scheduleSelectionData.GetScheduleSelection.StatementNumberColumn.ColumnName].DisplayIndex = 3;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerIDColumn.ColumnName].DisplayIndex = 4;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerNameColumn.ColumnName].DisplayIndex = 5;
            columns[this.scheduleSelectionData.GetScheduleSelection.LegalColumn.ColumnName].DisplayIndex = 10;
            columns[this.scheduleSelectionData.GetScheduleSelection.DistrictColumn.ColumnName].DisplayIndex = 11;
               }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
       }

       /// <summary>
       /// Settings the read only for schedule grid.
       /// </summary>
        private void SettingReadOnlyForScheduleGrid()
        {
            try
            {
             ScheduleSearchDataGridView.ScrollBars = ScrollBars.Both;  
            DataGridViewColumnCollection columns = this.ScheduleSearchDataGridView.Columns;
            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.StreetAddressColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.NAICSColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.DescriptionColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.ParcelNumberColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.StatementNumberColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerIDColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerNameColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.LegalColumn.ColumnName].ReadOnly = true;
            columns[this.scheduleSelectionData.GetScheduleSelection.DistrictColumn.ColumnName].ReadOnly = true;

            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columns[this.scheduleSelectionData.GetScheduleSelection.StreetAddressColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.NAICSColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.DescriptionColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.ParcelNumberColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.StatementNumberColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerIDColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerNameColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.LegalColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            columns[this.scheduleSelectionData.GetScheduleSelection.DistrictColumn.ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.StreetAddressColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.NAICSColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.ParcelNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.StatementNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.OwnerNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.LegalColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            columns[this.scheduleSelectionData.GetScheduleSelection.DistrictColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;

            columns[this.scheduleSelectionData.GetScheduleSelection.RollYearColumn.ColumnName].HeaderText = "Roll Year";
            columns[this.scheduleSelectionData.GetScheduleSelection.FilingDateColumn.ColumnName].HeaderText = "Filling Date";
            columns[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName].HeaderText = "Business Name";
            columns[this.scheduleSelectionData.GetScheduleSelection.PropertyTypeColumn.ColumnName].HeaderText = "Property Type";

            columns[this.scheduleSelectionData.GetScheduleSelection.BuisnessNameColumn.ColumnName].Width = 150;
          }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
       }

        /// <summary>
        /// Selects the schedule id.
        /// </summary>
        private void SelectScheduleId()
        {
            int rowId = 0;

            ////To get the Row index for Schedule DataGridView
            rowId = this.GetRowIndex();

            if (this.scheduleSelectionGridRowCount > 0 && rowId >= 0)
            {
                if (!string.IsNullOrEmpty(this.ScheduleSearchDataGridView.Rows[rowId].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].Value.ToString()))
                {
                    this.scheduleID = this.ScheduleSearchDataGridView.Rows[rowId].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].Value.ToString();
                    this.commandResult = this.scheduleID;

                    if (!string.IsNullOrEmpty(this.ScheduleSearchDataGridView.Rows[rowId].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].Value.ToString()))
                    {
                        this.commandValue = this.ScheduleSearchDataGridView.Rows[rowId].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].Value.ToString();
                    }
                    else
                    {
                        this.commandValue = string.Empty;
                    }
                }
                
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>integer value of grid row index</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.scheduleSelectionGridRowCount  > 0)
                {
                    if (this.ScheduleSearchDataGridView.CurrentCell != null)
                    {
                        return this.ScheduleSearchDataGridView.CurrentCell.RowIndex;
                    }

                    return -1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        /// <summary>
        /// Loads the schedule selection grid.
        /// </summary>
        /// <param name="scheduleConditionXML">The schedule condition XML.</param>
        private void LoadScheduleSelectionGrid(string scheduleConditionXML)
        {
            try
            {
            ////For Grid
            this.scheduleSelectionData = this.form1402Control.WorkItem.F1402_ListScheduleSearch(scheduleConditionXML);
            this.scheduleSelectionGridRowCount = this.scheduleSelectionData.GetScheduleSelection.Rows.Count;   

            if (this.scheduleSelectionGridRowCount > 0)
            {
                this.ScheduleSearchDataGridView.Enabled = true;

                this.ScheduleSearchDataGridView.DataSource = this.scheduleSelectionData;
                this.ScheduleSearchDataGridView.Focus();
                this.ScheduleSearchDataGridView.Rows[0].Selected = true;

                this.ScheduleAcceptButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.ClearScheduleSearchGrid();
            }

            //////to enable or disable the vertical scroll bar
            ////if (this.scheduleSelectionGridRowCount > this.ScheduleSearchDataGridView.NumRowsVisible)
            ////{
            ////    this.ScheduleSelectionVerticalScroll.Visible = false;
            ////}
            ////else
            ////{
            ////    this.ScheduleSelectionVerticalScroll.Visible = true;
            ////}

            ////To display the no of display rows in the grid
            this.RecordCountLabel.Text = this.scheduleSelectionGridRowCount + SharedFunctions.GetResourceString("9101MasterNameSearch");
            this.ScheduleSearchDataGridView.ScrollBars = ScrollBars.Both;  
        }
        catch (Exception ex)
        {
            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        }
        }

        /// <summary>
        /// Validates the date.
        /// </summary>
        /// <returns>boolean</returns>
        private bool ValidateDate()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.FilingDateTextBox.Text.Trim()))
                {
                    DateTime.Parse(this.FilingDateTextBox.Text.Trim());
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Grid Events
        /// <summary>
        /// Handles the CellDoubleClick event of the ScheduleSearchDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ScheduleSearchDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.ScheduleAcceptButton.Enabled)
                {
                    if (this.scheduleSelectionGridRowCount > 0)
                    {
                        if (!string.IsNullOrEmpty(this.ScheduleSearchDataGridView.Rows[e.RowIndex].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].Value.ToString()))
                        {
                            this.scheduleID = this.ScheduleSearchDataGridView.Rows[e.RowIndex].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleIDColumn.ColumnName].Value.ToString();
                            this.commandResult = this.scheduleID;

                            if (!string.IsNullOrEmpty(this.ScheduleSearchDataGridView.Rows[e.RowIndex].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].Value.ToString()))
                            {
                                this.commandValue = this.ScheduleSearchDataGridView.Rows[e.RowIndex].Cells[this.scheduleSelectionData.GetScheduleSelection.ScheduleNumberColumn.ColumnName].Value.ToString();
                            }
                            else
                            {
                                this.commandValue = string.Empty;
                            }

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            this.scheduleID = "";
                            this.commandResult = this.scheduleID;
                            this.commandValue = string.Empty;
                        }
                 }
                    else
                    {
                        this.DialogResult = DialogResult.None;
                    }
                   //// this.SelectScheduleId();
                   //// this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region TextBox EnterKey Events
        /// <summary>
        /// Handles the KeyUp event of the ScheduleNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ScheduleNumberTextBox_KeyUp(object sender, KeyEventArgs e)
        {
             try
            {
                if (this.SearchButton.Enabled)
                {
                    if (this.ValidateDate())
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.Enter:
                                {
                                    this.SearchButton_Click(sender, e);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            MessageBox.Show("please enter valid date range." + "\n" + "Allowed formats: " + "m/d/yyyy." + "\n" + "Minimum value: " + "1/1/1900" + "\n" + "Minimum value: " + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.FilingDateTextBox.Text = "";
                            this.FilingDateTextBox.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        /// <summary>
        /// Handles the Scroll event of the ScheduleSelectionVerticalScroll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void ScheduleSelectionVerticalScroll_Scroll(object sender, ScrollEventArgs e)
        {
            this.DialogResult = DialogResult.None;
        }
    }
}