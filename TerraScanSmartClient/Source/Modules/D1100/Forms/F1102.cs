 //--------------------------------------------------------------------------------------------
// <copyright file="F1102.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1102.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 July 06       JYOTHI              Created
// 25 July 06       JYOTHI              PopulateSearchResults added
// 26 July 06       JYOTHI              Added PopulateRecord
//*********************************************************************************/
namespace D1100
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
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using TerraScan.Common;

    /// <summary>
    /// F1102 Form
    /// </summary>
    public partial class F1102 : Form
    {
        #region Private Variables

        /// <summary>
        /// F1102Controller
        /// </summary>
        private F1102Controller form1102Control;

        /// <summary>
        /// templateIds variable is used to store list of templateIds for Mortgage Import Template. 
        /// </summary>  
        private ExciseRateDistrictSelectionData exciseRateDistrictSelectionDataSet = new ExciseRateDistrictSelectionData();

        /// <summary>
        /// Variable exciseRateDistrictSelectionId 
        /// </summary>
        private int exciseRateDistrictSelectionId;

        /// <summary>
        /// Variable exciseRateDistrictSelectionId 
        /// </summary>
        private int exciseRateDistrictDataCount;

        /// <summary>
        /// Used to store the roll year
        /// </summary>
        private int rollYear;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1102"/> class.
        /// </summary>
        public F1102()
        {
            this.InitializeComponent();
            this.CancelButton = this.DistrictCancelButton;
          ////  this.AcceptButton = this.SearchButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1102"/> class.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        public F1102(int rollYear)
        {
            this.InitializeComponent();
            this.CancelButton = this.DistrictCancelButton;
            ////  this.AcceptButton = this.SearchButton;
            this.rollYear = rollYear;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1102 control.
        /// </summary>
        /// <value>The F1102 control.</value>
        [CreateNew]
        public F1102Controller Form1102Control
        {
            get { return this.Form1102Control as F1102Controller; }
            set { this.form1102Control = value; }
        }

        /// <summary>
        /// Gets or sets the ExciseRateDistrictSelectionId
        /// </summary>
        /// <value>The ExciseRateDistrictSelectionId.</value>
        public int ExciseRateDistrictSelectionId
        {
            set { this.exciseRateDistrictSelectionId = value; }
            get { return this.exciseRateDistrictSelectionId; }
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

        #endregion Properties

        /// <summary>
        /// Handles the Click event of the DistrictCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearDistrictDetails();
                this.DistrictTextBox.Focus();
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
                this.PopulateSearchResults();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region UserDefinedFunctions

        /// <summary>
        /// Populates the search results.
        /// </summary>
        private void PopulateSearchResults()
        {
            int tempDistrict = 0;
            int tempYear = 0;

            //if (!int.TryParse(this.DistrictTextBox.Text, out tempDistrict))
            //{
            //    tempDistrict = -999;
            //}

            if (!int.TryParse(this.YearTextBox.Text, out tempYear))
            {
                tempYear = -999;
            }

            if (string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) && string.IsNullOrEmpty(this.YearTextBox.Text.Trim()) && string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Clear();
                //// Assing Data Count
                this.exciseRateDistrictDataCount = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Count; 
                this.ExciseRateDistrictSelectionGridView.DataSource = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict;
                //// IF No Record Disable the Grid
                this.ExciseRateDistrictSelectionGridView.Enabled = false;
            }
            else
            {
                //int district = 0;
                //int.TryParse(this.DistrictTextBox.Text.Trim(), out district);
                this.exciseRateDistrictSelectionDataSet = this.form1102Control.WorkItem.ListExciseRateDistrict(this.DistrictTextBox.Text.Trim(), tempYear, this.DescriptionTextBox.Text.Trim());
                //this.exciseRateDistrictSelectionDataSet = this.form1102Control.WorkItem.ListExciseRateDistrict(district, tempYear, this.DescriptionTextBox.Text.Trim());
            }

            if (this.exciseRateDistrictSelectionDataSet != null)
            {
                if (this.exciseRateDistrictSelectionDataSet.Tables.Count > 0)
                {
                    if (this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Count > 0)
                    {
                        this.SearchResultLabel.Text = this.exciseRateDistrictSelectionDataSet.Tables[0].Rows.Count.ToString() + " record(s) match.";
                        //// Assing Data Count
                        this.exciseRateDistrictDataCount = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Count;
                        ////  Record Enable the Grid
                        this.ExciseRateDistrictSelectionGridView.Enabled = true;
                        this.ExciseRateDistrictSelectionGridView.DataSource = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict;
                        this.ExciseRateDistrictSelectionGridView.Focus();
                        this.ExciseRateDistrictSelectionGridView.Rows[0].Selected = true;
                        if (this.exciseRateDistrictSelectionDataSet.Tables[0].Rows.Count > 5)
                        {
                            this.DistrictSelectionVscrollBar.Visible = false;
                        }
                        else
                        {
                            this.DistrictSelectionVscrollBar.Visible = true;
                        }

                        this.DistrictSelectAcceptButton.Enabled = true;
                    }
                    else
                    {
                        //// Assign Data Count
                        this.exciseRateDistrictDataCount = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Count;
                        ////  Record Enable the Grid
                        this.ExciseRateDistrictSelectionGridView.Enabled = false;
                        this.SearchResultLabel.Text = this.exciseRateDistrictSelectionDataSet.Tables[0].Rows.Count.ToString() + SharedFunctions.GetResourceString("9101MasterNameSearch");
                        this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Clear();
                        this.ExciseRateDistrictSelectionGridView.DataSource = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict;
                        this.ExciseRateDistrictSelectionGridView.CurrentCell = null;
                        this.DistrictSelectionVscrollBar.Visible = true;
                        this.DistrictSelectAcceptButton.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Clears the District details.
        /// </summary>
        private void ClearDistrictDetails()
        {
            this.DistrictTextBox.Text = string.Empty;
            this.YearTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.SearchResultLabel.Text = string.Empty;
            this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Clear();
            //// Assign the Row Count
            this.exciseRateDistrictDataCount = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Count; 
            this.ExciseRateDistrictSelectionGridView.DataSource = this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict;
            //// Disable Grid no Record
            this.ExciseRateDistrictSelectionGridView.Enabled = false;
            this.DistrictSelectionVscrollBar.Visible = true;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void LockControls(bool enableValue)
        {
            this.SearchButton.Enabled = enableValue;
            this.ClearButton.Enabled = enableValue;
            this.ExciseRateDistrictSelectionGridView.Enabled = enableValue;
        }

        /// <summary>
        /// Populates the record.
        /// </summary>
        private void PopulateRecord()
        {
            int rowIndex = -1;
            if (this.ExciseRateDistrictSelectionGridView.Rows.Count > 0)
            {
                rowIndex = this.ExciseRateDistrictSelectionGridView.CurrentRow.Index;
            }

            if (rowIndex >= 0)
            {
                if (!string.IsNullOrEmpty(this.ExciseRateDistrictSelectionGridView.Rows[rowIndex].Cells["ExciseRateID"].Value.ToString()))
                {
                    this.ExciseRateDistrictSelectionId = Convert.ToInt32(this.ExciseRateDistrictSelectionGridView.Rows[rowIndex].Cells["ExciseRateID"].Value.ToString());
                    this.commandResult = this.ExciseRateDistrictSelectionId.ToString();
                    this.DistrictTextBox.Focus();
                }
            }
        }
    
        #endregion

        /// <summary>
        /// Handles the Load event of the F1102 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1102_Load(object sender, EventArgs e)
        {
            try
            {
                this.ClearDistrictDetails();
                this.ExciseRateDistrictSelectionGridView.Rows[0].Selected = false;
                this.ExciseRateDistrictSelectionGridView.CurrentCell = null;
                this.DistrictTextBox.Focus();
                

                ////to load the year textbox with value(roll year) form the calling form 
                if (this.rollYear > 0)
                {
                    this.YearTextBox.Text = this.rollYear.ToString();
                    this.EnableSearchButton();
                }
                else
                {
                    this.YearTextBox.Text = string.Empty;
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
                this.ClearDistrictDetails();
                this.LockControls(false);
                this.ExciseRateDistrictSelectionGridView.Enabled = true;
                this.DistrictSelectAcceptButton.Enabled = false;
                this.ExciseRateDistrictSelectionGridView.CurrentCell = null;
                this.DistrictTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the ExciseRateDistrictSelectionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ExciseRateDistrictSelectionGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.ExciseRateDistrictSelectionGridView.Rows[e.RowIndex].Cells["ExciseRateID"].Value.ToString()))
                    {
                        this.ExciseRateDistrictSelectionId = Convert.ToInt32(this.ExciseRateDistrictSelectionGridView.Rows[e.RowIndex].Cells["ExciseRateID"].Value.ToString());
                        this.commandResult = this.ExciseRateDistrictSelectionId.ToString();
                        this.DistrictTextBox.Focus();
                        this.DialogResult = DialogResult.Yes;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                this.PopulateRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the TaxRateDefLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void TaxRateDefLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Ignore;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Activated event of the F1102 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1102_Activated(object sender, EventArgs e)
        {
            try
            {
                this.DistrictTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Districts the selection fields key up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DistrictSelectionFieldsKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.CheckKeyUp(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Checks the key up.
        /// </summary>
        /// <param name="e">The KeyEventArgs<see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CheckKeyUp(KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:
                        {
                            break;
                        }

                    case Keys.Tab:
                        {
                            break;
                        }

                    default:
                        {
                            this.EnableSearchButton();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.YearTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                this.SearchButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;

                if (this.exciseRateDistrictSelectionDataSet.ListExciseRateDistrict.Rows.Count <= 0)
                {
                    this.DistrictSelectAcceptButton.Enabled = false;
                    this.ClearButton.Enabled = false;
                    this.SearchResultLabel.Text = string.Empty;
                }
                else
                {
                    this.DistrictSelectAcceptButton.Enabled = true;
                    this.ClearButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the TaxRateDefLinkLabel control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaxRateDefLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
                {
                    if (ExciseRateDistrictSelectionGridView.OriginalRowCount > 0)
                    {
                        this.ExciseRateDistrictSelectionGridView.Focus();
                    }
                    else
                    {
                        this.DescriptionTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}