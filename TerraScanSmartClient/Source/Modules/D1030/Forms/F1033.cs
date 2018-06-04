//----------------------------------------------------------------------------------
// <copyright file="F1033.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1103.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		          Description
// ----------		---------		      ------------------------------------------
// 17 Nov 2006      Sam K                 Created 
// 03 Arp 2009      Shanmuga Sundaram.A   Modified to Implement CO#6032 
//*********************************************************************************/

namespace D1030
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Common.Reports;
    using System.Collections;
    using System.Configuration;

    /// <summary>
    /// Class file for F1033
    /// </summary>
    public partial class F1033 : Form
    {
        #region Private Variable
        /// <summary>
        /// form1033control
        /// </summary>
        private F1033Controller form1033control;

        /// <summary>
        /// To store specialDistrictID
        /// </summary>
        private int specialDistrictId;

        /// <summary>
        /// To Store District and SpecialDistrictName
        /// </summary>
        private string specialDistrictName;

        /// <summary>
        /// tempRollYear
        /// </summary>
        private string tempRollYear;

        /// <summary>
        /// To Get the List of PostTypes
        /// </summary>
        //private F1033SpecialDistrictSelectionData f1033SpecialDistrictSelectionData = new F1033SpecialDistrictSelectionData();
        /// <summary>
        /// To Store the Special District Data
        /// </summary>
        private F1033SpecialDistrictSelectionData.ListSpecialDistrictDataTable listSpecialDistrict = new F1033SpecialDistrictSelectionData.ListSpecialDistrictDataTable();

        #endregion Private Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1033"/> class.
        /// </summary>
        public F1033()
        {
            InitializeComponent();
        }

        ////Modified to Implement CO#6032 on 03 Arp 2009 by Shanmuga Sundaram.A 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="F1033"/> class.
        /// </summary>
        /// <param name="rollyear">The rollyear.</param>
        public F1033(string rollyear)
        {
            InitializeComponent();
            this.tempRollYear = rollyear;
        }
        #endregion Constructor

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #region properties

        /// <summary>
        /// Gets or sets the form1033control.
        /// </summary>
        /// <value>The form1033control.</value>
        [CreateNew]
        public F1033Controller Form1033control
        {
            get { return this.form1033control; }
            set { this.form1033control = value; }
        }

        /// <summary>
        /// Get and Set the SpecialDistrictID
        /// </summary>
        /// <value>specialDistrictID</value>
        public int SpecialDistrictId
        {
            get { return this.specialDistrictId; }
            set { this.specialDistrictId = value; }
        }

        /// <summary>
        /// Get and Set the District + SpecialDistrictName
        /// </summary>
        /// <value>SpecialDistrictName</value>
        public string SpecialDistrictName
        {
            get { return this.specialDistrictName; }
            set { this.specialDistrictName = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the PostType ComboBox
        /// </summary>
        private void InitPostTypeCombo()
        {
            F1033SpecialDistrictSelectionData.ListPostTypeDataTable listPostType = new F1033SpecialDistrictSelectionData.ListPostTypeDataTable();
            DataRow customRow = listPostType.NewRow();
            ////customRow[this.f1033SpecialDistrictSelectionData.ListPostType.PostTypeIDColumn.ColumnName] = "0";
            ////customRow[this.f1033SpecialDistrictSelectionData.ListPostType.PostNameColumn.ColumnName] = SharedFunctions.GetResourceString("All Post Types");
            customRow[listPostType.PostTypeIDColumn.ColumnName] = "0";
            customRow[listPostType.PostNameColumn.ColumnName] = SharedFunctions.GetResourceString("All Post Types");
            listPostType.Rows.Add(customRow);
            listPostType.Merge(this.form1033control.WorkItem.ListPostType(Convert.ToInt32(this.Tag)));
            this.PostTypeCombo.ValueMember = listPostType.PostTypeIDColumn.ColumnName;
            this.PostTypeCombo.DisplayMember = listPostType.PostNameColumn.ColumnName;
            this.PostTypeCombo.DataSource = listPostType;
        }

        /// <summary>
        /// Customize the SpecialDistrictSelection Grid
        /// </summary>
        private void CustimizeGrid()
        {
            this.SpecialDistrictSelectionGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.SpecialDistrictSelectionGridView.Columns;
            columns[this.listSpecialDistrict.DistrictColumn.ColumnName].DataPropertyName = this.listSpecialDistrict.DistrictColumn.ColumnName;
            columns[this.listSpecialDistrict.RollYearColumn.ColumnName].DataPropertyName = this.listSpecialDistrict.RollYearColumn.ColumnName;
            columns[this.listSpecialDistrict.DescriptionColumn.ColumnName].DataPropertyName = this.listSpecialDistrict.DescriptionColumn.ColumnName;
            columns[this.listSpecialDistrict.PostNameColumn.ColumnName].DataPropertyName = this.listSpecialDistrict.PostNameColumn.ColumnName;
            columns[this.listSpecialDistrict.SADistrictIDColumn.ColumnName].DataPropertyName = this.listSpecialDistrict.SADistrictIDColumn.ColumnName;

            columns[this.listSpecialDistrict.DistrictColumn.ColumnName].DisplayIndex = 0;
            columns[this.listSpecialDistrict.RollYearColumn.ColumnName].DisplayIndex = 1;
            columns[this.listSpecialDistrict.DescriptionColumn.ColumnName].DisplayIndex = 2;
            columns[this.listSpecialDistrict.PostNameColumn.ColumnName].DisplayIndex = 3;
            columns[this.listSpecialDistrict.SADistrictIDColumn.ColumnName].DisplayIndex = 4;
        }

        /// <summary>
        /// Populate Special District Data into Griid
        /// </summary>
        private void PopulateGrid()
        {
            try
            {
                int? district = null;
                int? rollYear = null;
                int? postTYpeID = null;
                if (!string.IsNullOrEmpty(this.DistrictTextBox.Text))
                {
                    district = Convert.ToInt32(this.DistrictTextBox.Text);
                }

                if (!string.IsNullOrEmpty(this.YearTextBox.Text))
                {
                    rollYear = Convert.ToInt32(this.YearTextBox.Text);
                }

                if (this.PostTypeCombo.SelectedIndex == 0)
                {
                    postTYpeID = null;
                }
                else
                {
                    postTYpeID = Convert.ToInt32(this.PostTypeCombo.SelectedValue.ToString());
                }

                this.listSpecialDistrict = this.form1033control.WorkItem.ListSpecialDistrict(district, rollYear, this.DescriptionTextBox.Text.Trim(), postTYpeID);
                if (this.listSpecialDistrict.Rows.Count > 0)
                {
                    this.SpecialDistrictSelectionGridView.Enabled = true;
                    this.SpecialDistrictSelectionGridView.DataSource = this.listSpecialDistrict;
                    this.SpecialDistrictSelectionGridView.Focus();
                    this.SpecialDistrictSelectionGridView.Rows[0].Selected = true;
                    if (this.listSpecialDistrict.Count > this.SpecialDistrictSelectionGridView.NumRowsVisible)
                    {
                        this.SpecialDistrictSelectionVscrollBar.Visible = false;
                    }
                    else
                    {
                        this.SpecialDistrictSelectionVscrollBar.Visible = true;
                    }

                    this.SpecialDistrictSelectAcceptButton.Enabled = true;
                    ////this.ClearButton.Enabled = true;                    
                }
                else
                {
                    this.listSpecialDistrict.Rows.Clear();
                    this.SpecialDistrictSelectionGridView.DataSource = this.listSpecialDistrict;
                    this.SpecialDistrictSelectionGridView.Enabled = false;
                    this.SpecialDistrictSelectionVscrollBar.Visible = true;
                    this.SpecialDistrictSelectAcceptButton.Enabled = false;
                    ////this.ClearButton.Enabled = false;                    
                }

                this.ClearButton.Enabled = true;
                this.SearchResultLabel.Text = this.listSpecialDistrict.Rows.Count.ToString() + " record(s) match.";
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Clear Special District Data
        /// </summary>
        private void ClearSpecialDistrictDetails()
        {
            this.DistrictTextBox.Text = string.Empty;
            this.YearTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.PostTypeCombo.SelectedIndex = 0;
            this.SearchResultLabel.Text = string.Empty;
            this.listSpecialDistrict.Rows.Clear();
            this.SpecialDistrictSelectionGridView.DataSource = this.listSpecialDistrict;
            this.SpecialDistrictSelectionGridView.Enabled = false;
            this.SpecialDistrictSelectionVscrollBar.Visible = true;
            this.SpecialDistrictSelectAcceptButton.Enabled = false;
        }

        /// <summary>
        /// Populates the record.
        /// </summary>
        private void PopulateRecord()
        {
            int rowIndex = -1;
            if (this.SpecialDistrictSelectionGridView.Rows.Count > 0)
            {
                rowIndex = this.SpecialDistrictSelectionGridView.CurrentRow.Index;
            }

            if (rowIndex >= 0)
            {
                if (!string.IsNullOrEmpty(this.SpecialDistrictSelectionGridView.Rows[rowIndex].Cells["SADistrictID"].Value.ToString()))
                {
                    this.specialDistrictId = Convert.ToInt32(this.SpecialDistrictSelectionGridView.Rows[rowIndex].Cells["SADistrictID"].Value.ToString());
                    this.specialDistrictName = this.SpecialDistrictSelectionGridView.Rows[rowIndex].Cells["District"].Value.ToString() + " - " + this.SpecialDistrictSelectionGridView.Rows[rowIndex].Cells["Description"].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Enables the clear button.
        /// </summary>
        private void EnableClearButton()
        {
            if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.YearTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.ClearButton.Enabled = false;
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handle the load Event of F1033 Form
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1033_Load(object sender, EventArgs e)
        {
            this.InitPostTypeCombo();
            this.CustimizeGrid();
            this.ClearSpecialDistrictDetails();
            this.DistrictTextBox.Enabled = true;
            this.SearchButton.Enabled = true;
            this.YearTextBox.Text = this.tempRollYear;
            ////this.DistrictTextBox.Focus();
        }

        /// <summary>
        /// Search Button Click
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.PopulateGrid();
        }

        /// <summary>
        /// Form Actvate
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1033_Activated(object sender, EventArgs e)
        {
            this.DistrictTextBox.Focus();
        }

        /// <summary>
        /// Special District Accept Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictSelectAcceptButton_Click(object sender, EventArgs e)
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
        /// Clear Button Click
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.ClearSpecialDistrictDetails();
            this.SpecialDistrictSelectionGridView.Enabled = false;
            this.DistrictTextBox.Focus();
            this.ClearButton.Enabled = false;
        }

        /// <summary>
        /// Handle the Double Click event in Special District Selection Grid
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictSelectionGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    ////if (this.SpecialDistrictSelectionGridView.Rows[e.RowIndex].Cells["SADistrictID"].Value != null)
                    if (!string.IsNullOrEmpty(this.SpecialDistrictSelectionGridView.Rows[e.RowIndex].Cells["SADistrictID"].Value.ToString()))
                    {
                        this.specialDistrictId = Convert.ToInt32(this.SpecialDistrictSelectionGridView.Rows[e.RowIndex].Cells["SADistrictID"].Value.ToString());
                        this.specialDistrictName = this.SpecialDistrictSelectionGridView.Rows[e.RowIndex].Cells["District"].Value.ToString() + " - " + this.SpecialDistrictSelectionGridView.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handle the Click event in Link Label
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo = TerraScanCommon.GetFormInfo(10030);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CheckKeyUp
        /// </summary>
        /// <param name="e">Event argu</param>
        private void CheckKeyUp(KeyEventArgs e)
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

                case Keys.Enter:
                    {
                        break;
                    }

                default:
                    {
                        this.EnableClearButton();
                        break;
                    }
            }
        }

        /// <summary>
        /// Specials the district selection key up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictSelectionKeyUp(object sender, KeyEventArgs e)
        {
            this.CheckKeyUp(e);
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the SpecialDistrictLinkLabel control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictLinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
            {
                if (SpecialDistrictSelectionGridView.OriginalRowCount > 0)
                {
                    this.SpecialDistrictSelectionGridView.Focus();
                }
                else
                {
                    this.PostTypeCombo.Focus();
                }
            }
        }
        #endregion Events
    }
}