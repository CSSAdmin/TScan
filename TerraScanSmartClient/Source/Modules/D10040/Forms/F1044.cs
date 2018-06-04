//--------------------------------------------------------------------------------------------
// <copyright file="F1044.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the District Type Selection.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//20170616          Dhineshkumar      Created
//*********************************************************************************/
namespace D10040
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
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;

    public partial class F1044 : Form
    {
        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// Template Id
        /// </summary>
        private int disctrictTemplateId;

        /// <summary>
        /// For Selected value
        /// </summary>
        private string commandValue = string.Empty;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        /// <summary>
        /// Controller
        /// </summary>
        private F1044Controller f1404Controller;

        /// <summary>
        /// Improvement District Definition Details.
        /// </summary>
        private F16040ImprovementDistrictDefinition getImprovementDistrictDefinition = new F16040ImprovementDistrictDefinition();

        /// <summary>
        /// Improvement District Type details.
        /// </summary>
        private F16040ImprovementDistrictDefinition.DistrictTypeTableDataTable getDistrictTypeDetails = new F16040ImprovementDistrictDefinition.DistrictTypeTableDataTable();

        /// <summary>
        /// Contstructor
        /// </summary>
        public F1044()
        {
            InitializeComponent();
            this.DescriptionPanel.Focus();
            this.DescriptionTextBox.Focus();
            this.ActiveControl = this.DescriptionTextBox;
        }

        /// <summary>
        /// Property DistrictTemplateTypeID 
        /// </summary>
        public int DistrictTemplateTypeID
        {
            set
            {
                this.disctrictTemplateId = value;
            }

            get
            {
                return this.DistrictTemplateTypeID;
            }
        }
        /// <summary>
        /// Gets or sets the form1030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F1044Controller Form1044Controller
        {
            get { return this.f1404Controller; }
            set { this.f1404Controller = value; }
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

        /// <summary>
        /// Gets or sets the command value.
        /// </summary>
        /// <value>The command value.</value>
        public string CommandValue
        {
            get { return this.commandValue; }
            set { this.CommandValue = value; }
        }

        #region EventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion EventPublication

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F1044_Load(object sender, EventArgs e)
        {
            this.LoadImprovementDistrictType();
            this.SpecialDistrictSelectionGridView.DataSource = null;
            this.SpecialDistrictSelectionGridView.Enabled = false;
            this.SpecialDistrictSelectionGridView.Rows[0].Selected = false;
            this.DistrictTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.SearchResultLabel.Visible = false;
            this.DisableButtons();
            this.DisableVScrollBar();
            this.DescriptionTextBox.Focus();
        }

        /// <summary>
        /// For F3602Control
        /// </summary>
        public F1044Controller Form1404Controller
        {
            get { return this.f1404Controller as F1044Controller; }
            set { this.f1404Controller = value; }
        }

        /// <summary>
        /// Load Improvement District Type.
        /// </summary>
        private void LoadImprovementDistrictType()
        {
            this.getImprovementDistrictDefinition.DistrictTypeTable.Clear();
            this.SpecialDistrictSelectionGridView.AllowUserToResizeColumns = false;
            this.SpecialDistrictSelectionGridView.AllowUserToResizeRows = false;
            this.SpecialDistrictSelectionGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.SpecialDistrictSelectionGridView.Columns;
            columns["DistrictTypeId"].DataPropertyName = this.getImprovementDistrictDefinition.DistrictTypeTable.IDTypeIDColumn.ColumnName;
            columns["DescriptionText"].DataPropertyName = this.getImprovementDistrictDefinition.DistrictTypeTable.IDTypeColumn.ColumnName;
        }

        /// <summary>
        /// Populate Improvement District Type.
        /// </summary>
        private void PopulateImprovementDistrictType()
        {
            string districtType = string.Empty;
            this.getDistrictTypeDetails.Clear();
            this.SpecialDistrictSelectionGridView.NumRowsVisible = 5;
            this.getImprovementDistrictDefinition = this.f1404Controller.WorkItem.ImprovementDistrictTypelist(districtType);
            this.getDistrictTypeDetails = this.getImprovementDistrictDefinition.DistrictTypeTable;
            int rowcount = this.getDistrictTypeDetails.Rows.Count;

            if (rowcount > 3)
            {
                this.SpecialDistrictSelectionVscrollBar.Visible = false;
            }
            else
            {
                this.SpecialDistrictSelectionVscrollBar.Visible = true;
            }

            this.SpecialDistrictSelectionGridView.DataSource = getDistrictTypeDetails;
        }

        private void ImprovementDistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormInfo formInfo = TerraScanCommon.GetFormInfo(1043);
            formInfo.optionalParameters = new object[] { 999 };
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            this.Close();
        }

        /// <summary>
        /// search event.
        /// </summary>
        private void SearchFunction()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.SearchButton.Enabled)
                {
                    int recordCount = 0;
                    //Removed the .Replace("'","''") for Database Execution retrieves nothing
                    string description = this.DescriptionTextBox.Text.Trim();
                    string idType = this.DistrictTextBox.Text.Trim();


                    //if (!string.IsNullOrEmpty(description) && string.IsNullOrEmpty(idType))
                    //{
                        this.getImprovementDistrictDefinition = this.f1404Controller.WorkItem.ImprovementDistrictTypelist(description);

                        if (this.getImprovementDistrictDefinition.DistrictTypeTable.Rows.Count > 0)
                        {
                            recordCount = this.getImprovementDistrictDefinition.DistrictTypeTable.Rows.Count;
                            this.SpecialDistrictSelectionGridView.Enabled = true;
                            this.SpecialDistrictSelectionGridView.DataSource = this.getImprovementDistrictDefinition.DistrictTypeTable;
                            this.SpecialDistrictSelectionGridView.Focus();
                            this.SpecialDistrictSelectionGridView.Rows[0].Selected = true;
                            this.SpecialDistrictSelectAcceptButton.Enabled = true;
                            this.emptyRecord = false;
                        }
                        else
                        {
                            this.SpecialDistrictSelectionGridView.DataSource = this.getImprovementDistrictDefinition.DistrictTypeTable;
                            this.SpecialDistrictSelectionGridView.Rows[0].Selected = false;
                            this.SpecialDistrictSelectionGridView.Enabled = false;
                            this.emptyRecord = true;
                            this.SpecialDistrictSelectAcceptButton.Enabled = false;
                        }

                        this.SearchResultLabel.Visible = true;
                        this.SearchResultLabel.Text = recordCount + " record(s) match.";
                        this.DisableVScrollBar();
                    //}
                    //else
                    //{
                    //    this.SearchResultLabel.Text = recordCount + " record(s) match.";
                    //}
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
        /// Search Button Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.SearchFunction();
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
                if ((!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim())))
                {
                    //this.EnableSearchButton();
                }
                else
                {
                    if (this.getImprovementDistrictDefinition != null)
                    {
                        if (this.getImprovementDistrictDefinition.DistrictTypeTable.Rows.Count > 0)
                        {
                            //this.SearchButton.Enabled = false;
                        }
                        else
                        {
                            this.DisableButtons();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
               // this.SearchButton.Enabled = true;
               // this.ClearButton.Enabled = true;
            }
            else
            {
                //this.SearchButton.Enabled = false;

                if (this.getImprovementDistrictDefinition.Tables[0].Rows.Count <= 0)
                {
                    this.SpecialDistrictSelectAcceptButton.Enabled = false;
                    //this.ClearButton.Enabled = false;
                }
                else
                {
                    this.SpecialDistrictSelectAcceptButton.Enabled = true;
                   // this.ClearButton.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Disables the Vertical scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.getImprovementDistrictDefinition != null)
            {
                if (this.getImprovementDistrictDefinition.DistrictTypeTable.Rows.Count > 5)
                {
                    this.SpecialDistrictSelectionVscrollBar.Enabled = true;
                    this.SpecialDistrictSelectionVscrollBar.Visible = false;
                }
                else
                {
                    this.SpecialDistrictSelectionVscrollBar.Enabled = false;
                    this.SpecialDistrictSelectionVscrollBar.Visible = true;
                    this.SpecialDistrictSelectionVscrollBar.BringToFront();
                }
            }
        }


        private void SpecialDistrictSelectionGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    this.PopulateRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        private void SpecialDistrictSelectionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.PopulateRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        private void SpecialDistrictSelectionGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (int)Keys.Enter)
                {
                    e.Handled = true;
                    this.PopulateRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Method to Populate the Current Record and Close the Form
        /// </summary>
        private void PopulateRecord()
        {
            try
            {
                int rowIndex = -1;
                if (this.SpecialDistrictSelectionGridView.Rows.Count > 0)
                {
                    rowIndex = this.SpecialDistrictSelectionGridView.CurrentRow.Index;
                    this.SpecialDistrictSelectionGridView.CurrentRow.Selected = true;
                }
                if (rowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.SpecialDistrictSelectionGridView.Rows[rowIndex].Cells["DistrictTypeId"].Value.ToString()))
                    {
                        this.disctrictTemplateId = Convert.ToInt32(this.SpecialDistrictSelectionGridView.Rows[rowIndex].Cells["DistrictTypeId"].Value.ToString());
                        this.commandResult = this.disctrictTemplateId.ToString();

                        if (!string.IsNullOrEmpty(this.SpecialDistrictSelectionGridView.Rows[this.SpecialDistrictSelectionGridView.CurrentRowIndex].Cells["DescriptionText"].Value.ToString()))
                        {
                            this.commandValue = this.SpecialDistrictSelectionGridView.Rows[this.SpecialDistrictSelectionGridView.CurrentRowIndex].Cells["DescriptionText"].Value.ToString();
                        }
                        else
                        {
                            this.commandValue = string.Empty;
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SpecialDistrictSelectionGridView.Enabled = false;
                this.SpecialDistrictSelectionGridView.Rows[0].Selected = false;
                this.DistrictTextBox.Text = string.Empty;
                this.DescriptionTextBox.Text = string.Empty;         
                this.SearchResultLabel.Visible =false;
                this.DisableButtons();
                if (this.getImprovementDistrictDefinition != null)
                {
                    this.getImprovementDistrictDefinition.DistrictTypeTable.Clear();
                    this.SpecialDistrictSelectionGridView.DataSource = null;

                }
                this.DisableVScrollBar();
                this.DescriptionTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Disables the buttons.
        /// </summary>
        private void DisableButtons()
        {
           // this.SearchButton.Enabled = false;
            this.SpecialDistrictSelectAcceptButton.Enabled = false;
           // this.ClearButton.Enabled = false;
        }

        private void DistrictCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DistrictCancelButton.Enabled)
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

        private void SpecialDistrictSelectAcceptButton_Click(object sender, EventArgs e)
        {
            this.PopulateRecord();
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
                        this.ClearButton.Enabled = true;
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
        /// Enables the clear button.
        /// </summary>
        private void EnableClearButton()
        {
            if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
               // this.ClearButton.Enabled = true;
            }
            else
            {
               // this.ClearButton.Enabled = false;
            }
        }

        /// <summary>
        /// Description Textbox Keydown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescriptionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SearchFunction();
            }
        }
    }
}
