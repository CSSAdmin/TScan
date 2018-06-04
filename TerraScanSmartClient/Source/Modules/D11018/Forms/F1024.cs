//--------------------------------------------------------------------------------------------
// <copyright file="F1024.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1021.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/
namespace D11018
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
    using TerraScan.Utilities;
    using TerraScan.Common;
    using System.Configuration;
    using System.Web.Services.Protocols;
    using System.Collections;

    /// <summary>
    /// F1024 Form
    /// </summary>
    public partial class F1024 : BasePage
    {
        #region Private Variables

        /// <summary>
        /// form1021Control Variable
        /// </summary>
        private F1024Controller form1024Control;

        /// <summary>
        /// roll year;
        /// </summary>
        private string rollYear;

        /// <summary>
        /// District ID
        /// </summary>
        private string districtId;

        /// <summary>
        /// Datatable to hold values return after save
        /// </summary>
        private F11018MiscReceiptData.ListReceiptItemDataTable districtTable = new F11018MiscReceiptData.ListReceiptItemDataTable();

        /// <summary>
        /// data table to hold new sub fund list
        /// </summary>
        DistrictSelectionData.ListSubFundItemsDataTable NewSubFundList;

        /// <summary>
        /// hold button action results;
        /// </summary>
        private string Button;

        ///<summary>
        ///load value boolean
        ///</summary>
        private bool flagloadProcess = false;

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1024"/> class.
        /// </summary>
        public F1024()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1024"/> class.
        /// </summary>
        /// <param name="parentMiscReceiptFields">The parent misc receipt fields.</param>
        public F1024(string rollYear)
        {
            this.InitializeComponent();
            ////set misc receipt fields which contain neceaary information
            this.rollYear = rollYear;

            this.CancelButton = this.CancelMiscTemplateButton;
            this.SaveMenuToolStripMenuItem.Click += new EventHandler(this.SaveMiscTemplateButton_Click);
            ////Set form name
            //this.Text = string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SaveMiscellaneousName"));
        }

        #endregion

        #region Enumerator

        /// <summary>
        ///  enumerator for Receipt Type
        /// </summary>
        public enum Levies
        {
            /// <summary>
            /// Value for All
            /// </summary>
            All = 1,

            /// <summary>
            /// Value for Base Only
            /// </summary>
            BaseOnly = 2,

            /// <summary>
            /// Value for Excess Only
            /// </summary>
            ExcessOnly = 3
        }

        #endregion Enumerator

        #region Properties

        /// <summary>
        /// Gets or sets the form1021 control.
        /// </summary>
        /// <value>The form1024 control.</value>
        [CreateNew]
        public F1024Controller F1024Control
        {
            get { return this.form1024Control as F1024Controller; }
            set { this.form1024Control = value; }
        }


        /// <summary>
        /// Gets or sets the misc tempalte id.
        /// </summary>
        /// <value>The misc tempalte id.</value>
        public F11018MiscReceiptData.ListReceiptItemDataTable SelectedDistrict
        {
            get { return this.districtTable; }
            set { this.districtTable = value; }
        }

        public string ButtonAction
        {
            get { return this.Button; }
            set { this.Button = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1021 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1024_Load(object sender, EventArgs e)
        {
            ////set default value on form load
            // this.DistrictTextBox.Text = this.miscReceiptFields.DefaultComment;
            this.flagloadProcess = false;
            this.LoadLeviesCombo();
            this.AmountTextBox.Text = string.Empty;
            this.AmountTextBox.Focus();
            this.NewSubFundList = new DistrictSelectionData.ListSubFundItemsDataTable();
            this.DistrictListDataGridView.AutoGenerateColumns = false;
            this.DistrictListDataGridView.DataSource = null;
            this.DistrictListDataGridView.Enabled = false;
            this.DistrictListDataGridView.Rows[0].Selected = false;
            this.CustomizeDataGrid();
            this.flagloadProcess = true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.DistrictListDataGridView.IsMultiSelect = false;
            this.DistrictListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DistrictListDataGridView.AllowUserToResizeColumns = false;
            this.DistrictListDataGridView.AutoGenerateColumns = false;
            this.DistrictListDataGridView.AllowUserToResizeRows = false;
            this.DistrictListDataGridView.StandardTab = true;
            this.DistrictListDataGridView.PrimaryKeyColumnName = "SubFundID";
            //this.DistrictListDataGridView.Columns[1].CellTemplate = new DataGridViewCheckBoxCell();    
            this.DistrictListDataGridView.Columns[0].DataPropertyName = "SubFundID";
            this.DistrictListDataGridView.Columns[1].DataPropertyName = "Checked";

            this.DistrictListDataGridView.Columns[2].DataPropertyName = "SubFund";
            this.DistrictListDataGridView.Columns[3].DataPropertyName = "Description";
            this.DistrictListDataGridView.Columns[4].DataPropertyName = "Rate";
            this.DistrictListDataGridView.Columns[5].DataPropertyName = "IsVoterApproved";
            //this.DistrictListDataGridView.Columns[0].DataPropertyName = "SubFundID";
            this.DistrictListDataGridView.Columns[1].DisplayIndex = 2;
            this.DistrictListDataGridView.Columns[2].DisplayIndex = 3;
            this.DistrictListDataGridView.Columns[3].DisplayIndex = 4;
            this.DistrictListDataGridView.Columns[4].DisplayIndex = 5;
            this.DistrictListDataGridView.Columns[5].DisplayIndex = 1;

            this.DistrictListDataGridView.Columns[1].Width = 30;
            this.DistrictListDataGridView.Columns[4].Width = 130;

            //this.SubFundPanel.Height = 174;
            //this.MiscTemplateVscrollBar.Height = 173;
            //this.DistrictListDataGridView.Height = 173;
        }
        /// <summary>
        /// Load combo details
        /// </summary>
        private void LoadLeviesCombo()
        {
            Hashtable leviesTable = new Hashtable();
            leviesTable.Add("All", (int)Levies.All);
            leviesTable.Add("Base Only", (int)Levies.BaseOnly);
            leviesTable.Add("Excess Only", (int)Levies.ExcessOnly);
            CommonData leviesTableData = new CommonData();
            leviesTableData.LoadGeneralComboData(leviesTable);

            // Assign default values to combo box
            this.LevisComboBox.DisplayMember = leviesTableData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.LevisComboBox.ValueMember = leviesTableData.ComboBoxDataTable.KeyIdColumn.ColumnName;

            // List the values based on receipttype id
            leviesTableData.ComboBoxDataTable.DefaultView.Sort = leviesTableData.ComboBoxDataTable.KeyIdColumn.ColumnName + " ASC";
            this.LevisComboBox.DataSource = leviesTableData.ComboBoxDataTable.DefaultView;
        }

        #region Save

        /// <summary>
        /// Handles the Click event of the SaveMiscTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveMiscTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ReplaceDistrictButton.Enabled)
                {
                    if (this.DistrictListDataGridView.OriginalRowCount > 0)
                    {
                        this.ReplaceDistrictButton.Focus();
                        ////Check For Required Fields
                        if (String.IsNullOrEmpty(this.AmountTextBox.Text.Trim()))// || string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()))
                        {
                            MessageBox.Show("Amount cannot be Blank.", ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.AmountTextBox.Focus();
                            return;
                        }

                        this.Cursor = Cursors.WaitCursor;
                        int selectedDistrictId = 0;
                        if (this.districtId != null && !string.IsNullOrEmpty(this.districtId))
                        {
                            int.TryParse(this.districtId, out selectedDistrictId);
                        }

                        bool IsReplace = true; // For replace 
                        string SubFundXML = string.Empty;
                        this.Button = "Replace";
                        SubFundXML = TerraScanCommon.GetXmlString(this.GetSelectedSubFundID());

                        this.SelectedDistrict = this.form1024Control.WorkItem.F1024_SaveDistrictDetails((int)this.LevisComboBox.SelectedValue, selectedDistrictId, this.AmountTextBox.DecimalTextBoxValue, TerraScanCommon.UserId, IsReplace, SubFundXML).ListReceiptItem;

                        this.Cursor = Cursors.Default;
                        ////modified flag 
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select District.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        private void Districbutton_Click(object sender, EventArgs e)
        {
            try
            {
                Form districtF15122 = new Form();
                object[] optionalParameter = new object[] { this.rollYear,this.ParentFormId };
                districtF15122 = TerraScanCommon.GetForm(1512, optionalParameter, this.form1024Control.WorkItem);

                DialogResult districtDialog;
                if (districtF15122 != null)
                {
                    districtDialog = districtF15122.ShowDialog();

                    if (districtDialog == DialogResult.OK)
                    {
                        try
                        {
                            this.districtId = TerraScanCommon.GetValue(districtF15122, "DistrictId");
                            int selectedDistrictId = 0;
                            if (this.districtId != null && !string.IsNullOrEmpty(this.districtId))
                            {
                                int.TryParse(this.districtId, out selectedDistrictId);
                            }

                            F1512DistrictSelectionData districtSlectionDataset = this.form1024Control.WorkItem.F1204_GetDistrictSelectionData(selectedDistrictId, "", "", -999);

                            if (districtSlectionDataset.ListDistrictSelection.Rows.Count > 0)
                            {
                                this.DistrictTextBox.Text = districtSlectionDataset.ListDistrictSelection.Rows[0][districtSlectionDataset.ListDistrictSelection.DistrictColumn].ToString() + " - " + districtSlectionDataset.ListDistrictSelection.Rows[0][districtSlectionDataset.ListDistrictSelection.DescriptionColumn].ToString();
                            }
                            this.LoadSubGrid(this.districtId);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }
        /// <summary>
        /// Loading sub grid
        /// </summary>
        /// <param name="DistrictID">pass distinct id</param>
        private void LoadSubGrid(string DistrictID)
        {
            try
            {
                this.NewSubFundList = this.form1024Control.WorkItem.f1024_pclst_SubFunds(Convert.ToInt32(districtId)).ListSubFundItems;

                if (this.NewSubFundList.Rows.Count > 0)
                {
                    foreach (DataRow dr in this.NewSubFundList.Rows)
                    {
                        if (this.LevisComboBox.SelectedValue.ToString() == "1")
                        {
                            dr[this.NewSubFundList.CheckedColumn.ColumnName] = true;
                        }
                        else if (this.LevisComboBox.SelectedValue.ToString() == "2" && Convert.ToBoolean(dr["IsVoterApproved"]) == false)
                        {
                            dr[this.NewSubFundList.CheckedColumn.ColumnName] = true;
                        }
                        else if (this.LevisComboBox.SelectedValue.ToString() == "3" && Convert.ToBoolean(dr["IsVoterApproved"]) == true)
                        {
                            dr[this.NewSubFundList.CheckedColumn.ColumnName] = true;
                        }
                    }
                }

                this.DistrictListDataGridView.DataSource = this.NewSubFundList.DefaultView;

                if (this.DistrictListDataGridView.Rows.Count > 0)
                {
                    this.DistrictListDataGridView.Enabled = true;
                    this.MiscTemplateVscrollBar.Visible = false;
                    this.DistrictListDataGridView.Width = 456;                    
                }

            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Get Selected sub fund List
        /// </summary>
        /// <returns>Sub Fund ID's </returns>
        private DataTable GetSelectedSubFundID()
        {
            DataTable SelectedList = new DataTable("List");

            DataColumn SubFundID = new DataColumn("SubFundID");
            SelectedList.Columns.Add(SubFundID);
            for (int Cnt = 0; Cnt < this.DistrictListDataGridView.Rows.Count; Cnt++)
            {
                if (Convert.ToBoolean(this.DistrictListDataGridView.Rows[Cnt].Cells["Checked"].Value) == true)
                {
                    SelectedList.Rows.Add(this.DistrictListDataGridView.Rows[Cnt].Cells["SubFundID"].Value);
                }
            }
            return SelectedList;
        }
        #endregion

        private void CancelMiscTemplateButton_Click(object sender, EventArgs e)
        {
            this.Button = "";
        }

        private void AppendDistrictButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ReplaceDistrictButton.Enabled)
                {
                    if (this.DistrictListDataGridView.OriginalRowCount > 0)
                    {
                        this.ReplaceDistrictButton.Focus();
                        ////Check For Required Fields
                        if (String.IsNullOrEmpty(this.AmountTextBox.Text.Trim()))// || string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()))
                        {
                            MessageBox.Show("Amount cannot be Blank.", ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.AmountTextBox.Focus();
                            return;
                        }

                        this.Cursor = Cursors.WaitCursor;
                        int selectedDistrictId = 0;
                        if (this.districtId != null && !string.IsNullOrEmpty(this.districtId))
                        {
                            int.TryParse(this.districtId, out selectedDistrictId);
                        }

                        bool IsReplace = false; // For Append
                        string SubFundXML = string.Empty;
                        this.Button = "Append";
                        SubFundXML = TerraScanCommon.GetXmlString(this.GetSelectedSubFundID());

                        this.SelectedDistrict = this.form1024Control.WorkItem.F1024_SaveDistrictDetails((int)this.LevisComboBox.SelectedValue, selectedDistrictId, this.AmountTextBox.DecimalTextBoxValue, TerraScanCommon.UserId, IsReplace, SubFundXML).ListReceiptItem;

                        this.Cursor = Cursors.Default;
                        ////modified flag 
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select District.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);   
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LevisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.flagloadProcess)
            {
                if (this.DistrictListDataGridView.OriginalRowCount > 0)
                {
                    for (int Cnt = 0; Cnt < this.DistrictListDataGridView.Rows.Count; Cnt++)
                    {
                        if (this.DistrictListDataGridView.Rows[Cnt].Cells["SubFundID"].Value != null)
                        {
                            if (this.LevisComboBox.SelectedValue.ToString() == "1")
                            {
                                this.DistrictListDataGridView.Rows[Cnt].Cells["Checked"].Value = true;
                            }
                            else if ((this.LevisComboBox.SelectedValue.ToString() == "2" && Convert.ToBoolean(this.DistrictListDataGridView.Rows[Cnt].Cells["IsVoterApproved"].Value) == false))
                            {
                                this.DistrictListDataGridView.Rows[Cnt].Cells["Checked"].Value = true;
                            }

                            else if ((this.LevisComboBox.SelectedValue.ToString() == "3" && Convert.ToBoolean(this.DistrictListDataGridView.Rows[Cnt].Cells["IsVoterApproved"].Value) == true))
                            {
                                this.DistrictListDataGridView.Rows[Cnt].Cells["Checked"].Value = true;
                            }
                            else
                            {
                                this.DistrictListDataGridView.Rows[Cnt].Cells["Checked"].Value = false;
                            }
                        }
                    }
                }
                //else
                //{
                //    MessageBox.Show("Select valid District", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
        }
    }
}