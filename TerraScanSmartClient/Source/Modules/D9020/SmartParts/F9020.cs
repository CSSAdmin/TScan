//--------------------------------------------------------------------------------------------
// <copyright file="F9020.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the CountyConfiguration.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 May 06		GUHAN S	            Created
// 27 Jul 06        VINOTH BABU         Modified For CAB Conversion
//*********************************************************************************/

namespace D9020
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;
    using System.Configuration;
    using TerraScan.Utilities;
    using System.IO;
    using TerraScan.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// UserControl for F9020
    /// </summary>
    [SmartPart]
    public partial class F9020 : BaseSmartPart
    {
        #region  Variable Declarations

        /// <summary>
        ///  Used To store Config Details
        /// </summary>
        private DataSet configDetails = new DataSet();

        /// <summary>
        ///  Used To strore Valid  DataSet Or Not
        /// </summary>
        private bool validDataSet;

        /// <summary>
        /// Used to Hold the Type
        /// </summary>
        private int selectedType;

        /// <summary>
        /// Used to Hold the configType
        /// </summary>
        private int configType;

        /// <summary>
        /// Used to Hold the configValue
        /// </summary>
        private string configValue;

        /// <summary>
        /// Object for OpenfileDialog Created
        /// </summary>
        private System.Windows.Forms.OpenFileDialog configOpenDialog = new OpenFileDialog();

        /// <summary>
        /// Object for OpenfileDialog Created
        /// </summary>
        private System.Windows.Forms.FolderBrowserDialog configFolderDialog = new FolderBrowserDialog();

        /// <summary>
        /// Used to Hold the configValue
        /// </summary>
        private bool configChange;

        /// <summary>
        /// Used to Hold the configValue
        /// </summary>
        private int currentRow;

        /// <summary>
        ///  used to Store tempo Row Positions
        /// </summary>
        private int tempRowId;

        /// <summary>
        /// Used to Hold the cellChange
        /// </summary>
        private int cellRow;

        /// <summary>
        /// Used to Hold the  Record Count
        /// </summary>
        private int countyRowCount;

        /// <summary>
        /// used to hold the validFileType
        /// </summary>
        private bool validFileType;

        /// <summary>
        /// used to hold the fileType
        /// </summary>
        private string fileType;

        /// <summary>
        /// used to hold the formClosing
        /// </summary>
        private bool formClosing;

        /// <summary>
        /// used to hold the clickColumIndex
        /// </summary>
        private int clickColumIndex;

        /// <summary>
        /// Controller F9020Controller
        /// </summary>
        private F9020Controller form9020Control;

        #endregion

        #region Constructor

        /// <summary>
        /// F9020 Constructor
        /// </summary>
        public F9020()
        {
            this.InitializeComponent();
            this.pictureBox3.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox3.Height, this.pictureBox3.Width, "Option", 28, 81, 128);
            this.pictureBox2.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox2.Height, this.pictureBox2.Width, "Configuration Options", 28, 81, 128);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Event Publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region enumeratorButtonOperation

        /// <summary>
        /// Button Functionality
        /// </summary>
        public enum ConfigType
        {
            /// <summary>
            /// Empty = 0.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// DateType  = 1.
            /// </summary>
            DateType = 1,

            /// <summary>
            /// TextType = 2.
            /// </summary>
            TextType = 2,

            /// <summary>
            /// ChoiceType = 3.
            /// </summary>
            ChoiceType = 3,

            /// <summary>
            /// FilePath = 4.
            /// </summary>
            FilePath = 4,

            /// <summary>
            /// FolderPath = 5.
            /// </summary>
            FolderPath = 5
        }

        #endregion

        #region Property

        /// <summary>
        /// Created Property for F9020Controller
        /// </summary>
        [CreateNew]
        public F9020Controller F9020Controll
        {
            get { return this.form9020Control as F9020Controller; }
            set { this.form9020Control = value; }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.F9020Controll.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        #endregion

        #region Static Method
        /// <summary>
        /// Creates the empty rows.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>DataTable</returns>
        private static DataTable CreateEmptyRows(DataTable sourceDataTable, int maxRowCount)
        {
            int defaultRowsCount = 0;
            DataRow tempRow;
            if (sourceDataTable.Rows.Count < maxRowCount)
            {
                defaultRowsCount = maxRowCount - sourceDataTable.Rows.Count;
                for (int i = 0; i < defaultRowsCount; i++)
                {
                    tempRow = sourceDataTable.NewRow();
                    for (int j = 0; j < sourceDataTable.Columns.Count; j++)
                    {
                        if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int32")
                        {
                            tempRow[j] = 1;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int16")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Boolean")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else
                        {
                            tempRow[j] = string.Empty;
                        }
                    }

                    sourceDataTable.Rows.Add(tempRow);
                }
            }

            return sourceDataTable;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="commentRow">The comment row.</param>
        private void SetDataGridViewPosition(Control controlName, int commentRow)
        {
            DataGridView tempDataGridview = (DataGridView)controlName;
            try
            {
                if (tempDataGridview.Rows.Count > 0 && commentRow >= 0)
                {
                    tempDataGridview.Rows[Convert.ToInt32(commentRow)].Selected = true;
                    tempDataGridview.CurrentCell = tempDataGridview[0, Convert.ToInt32(commentRow)];
                    ////  this.cellChange = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the config data grid view.
        /// </summary>
        private void LoadConfigDataGridView()
        {
            //int applicationId = 1;
            this.configDetails = F9020WorkItem.GetCountyConfiguration(TerraScanCommon.ApplicationId, TerraScanCommon.UserId);

            this.countyRowCount = this.configDetails.Tables[0].Rows.Count;
            this.CheckValidDataSet(this.configDetails);
            this.OptionTextBox.BackColor = System.Drawing.Color.White;
            this.DescriptionTextBox.BackColor = System.Drawing.Color.White;
            if (this.validDataSet)
            {
                this.OptionGridView.DataSource = this.configDetails.Tables[0];

                if (this.countyRowCount > 0)
                {
                    // Change Request : Upon Load, Sort first Column of Grid to Asc
                    //this.OptionGridView.Sort(OptionGridView.Columns[this.configDetails.Tables[0].Columns[4].ColumnName], ListSortDirection.Ascending);

                    this.SetChoiceDataCombo(this.OptionGridView.Rows[this.tempRowId].Cells["CfgID"].Value.ToString());
                    this.SetTheComboboxValue(this.OptionGridView.Rows[this.tempRowId].Cells["CfgValue"].Value.ToString());

                    if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.tempRowId].Cells["Type"].Value.ToString()))
                    {
                        this.SetVisiblePanels((ConfigType)Convert.ToInt32(this.OptionGridView.Rows[this.tempRowId].Cells["Type"].Value.ToString()));
                    }
                    //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                    this.ConfigLinkLabel.Enabled = true;
                    this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.OptionGridView.Rows[this.tempRowId].Cells["CfgID"].Value.ToString();
                    this.SetTextBoxesValues(this.tempRowId);

                    if (this.countyRowCount > this.OptionGridView.NumRowsVisible)
                    {
                        this.ConfigVerticalScrollBar.Visible = false;
                    }
                    else
                    {
                        this.ConfigVerticalScrollBar.Visible = true;
                    }
                    //if (this.countyRowCount > 16)
                    //{
                    //    this.ConfigVerticalScrollBar.Enabled = true;
                    //    this.ConfigVerticalScrollBar.Visible = true;
                    //}
                    //else
                    //if (this.countyRowCount <= 16)
                    //{
                    //    this.ConfigVerticalScrollBar.Enabled = false;
                    //    this.ConfigVerticalScrollBar.Visible = true;
                    //}
                }
                else
                {
                   this.ConfigVerticalScrollBar.Visible = true;
                   this.ConfigLinkLabel.Enabled = false;
                }
            }
            else
            {
                ////this.EmptyOptionGridView.DataSource = TerraScanCommon.CreateEmptyRows(this.configDetails.Tables[0].Clone(), 16);
                ////this.EmptyOptionGridView.Enabled = false;
                ////this.EmptyOptionGridView.ScrollBars = ScrollBars.None;
            }
        }

        /// <summary>
        /// Customises the data grid view.
        /// </summary>
        private void CustomiseDataGridView()
        {
            this.OptionGridView.AllowUserToResizeColumns = false;
            this.OptionGridView.AutoGenerateColumns = false;
            this.OptionGridView.AllowUserToResizeRows = false;

            DataGridViewColumnCollection columns = this.OptionGridView.Columns;
            columns["DisplayName"].DataPropertyName = "DisplayName";
            columns["CfgValue"].DataPropertyName = "CfgValue";
            columns["Type"].DataPropertyName = "Type";
            columns["CfgId"].DataPropertyName = "CfgId";
            columns["Description"].DataPropertyName = "Description";
            columns["CfgName"].DataPropertyName = "CfgName";
            columns["DisplayName"].DisplayIndex = 0;
            columns["CfgValue"].DisplayIndex = 1;
            columns["Type"].DisplayIndex = 2;
            columns["CfgId"].DisplayIndex = 3;
            columns["Description"].DisplayIndex = 4;
            columns["CfgName"].DisplayIndex = 5;
        }

        /// <summary>
        /// Checks the valid data set.
        /// </summary>
        /// <param name="cmntDataSet">The CMNT data set.</param>
        private void CheckValidDataSet(DataSet cmntDataSet)
        {
            if (cmntDataSet.Tables.Count > 0 && cmntDataSet != null)
            {
                this.validDataSet = true;
            }
            else
            {
                this.validDataSet = false;
            }
        }

        /// <summary>
        /// Sets the visible panels.
        /// </summary>
        /// <param name="ctype">The ctype.</param>
        private void SetVisiblePanels(ConfigType ctype)
        {
            switch (ctype)
            {
                case ConfigType.DateType:
                    {
                        this.FilePanel.Visible = false;
                        this.TextPanel.Visible = false;
                        this.ChoicePanel.Visible = false;
                        this.DatePanel.Visible = true;
                        this.DatePanel.BringToFront();
                        break;
                    }

                case ConfigType.FilePath:
                    {
                        this.DatePanel.Visible = false;
                        this.TextPanel.Visible = false;
                        this.ChoicePanel.Visible = false;
                        this.FilePanel.Visible = true;
                        this.ConfigMonthCalander.Visible = false;
                        this.FilePanel.BringToFront();
                        break;
                    }

                case ConfigType.FolderPath:
                    {
                        this.DatePanel.Visible = false;
                        this.TextPanel.Visible = false;
                        this.ChoicePanel.Visible = false;
                        this.FilePanel.Visible = true;
                        this.ConfigMonthCalander.Visible = false;
                        this.FilePanel.BringToFront();
                        break;
                    }

                case ConfigType.TextType:
                    {
                        this.DatePanel.Visible = false;
                        this.ConfigMonthCalander.Visible = false;
                        this.FilePanel.Visible = false;
                        this.ChoicePanel.Visible = false;
                        this.TextPanel.Visible = true;
                        this.TextPanel.BringToFront();
                        break;
                    }

                case ConfigType.ChoiceType:
                    {
                        this.DatePanel.Visible = false;
                        this.ConfigMonthCalander.Visible = false;
                        this.FilePanel.Visible = false;
                        this.TextPanel.Visible = false;
                        this.ChoicePanel.Visible = true;
                        this.ChoicePanel.BringToFront();
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets the config desc.
        /// </summary>
        /// <param name="ctype">The ctype.</param>
        /// <returns>True if Values are filled else false</returns>
        private bool GetConfigDesc(ConfigType ctype)
        {
            bool tempValid = false;
            switch (ctype)
            {
                case ConfigType.DateType:
                    {
                        if (!string.IsNullOrEmpty(this.ConfigDateTextBox.Text.Trim()))
                        {
                            this.configValue = this.ConfigDateTextBox.Text.Trim();
                            tempValid = true;
                        }
                        else
                        {
                            tempValid = false;
                        }

                        break;
                    }

                case ConfigType.FilePath:
                    {
                        if (!string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
                        {
                            this.configValue = this.FilePathTextBox.Text.Trim();
                            tempValid = true;
                        }
                        else
                        {
                            tempValid = false;
                        }

                        break;
                    }

                case ConfigType.FolderPath:
                    {
                        if (!string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
                        {
                            this.configValue = this.FilePathTextBox.Text.Trim();
                            tempValid = true;
                        }
                        else
                        {
                            tempValid = false;
                        }

                        break;
                    }

                case ConfigType.TextType:
                    {
                        if (!string.IsNullOrEmpty(this.ValueTextBox.Text.Trim()))
                        {
                            this.configValue = this.ValueTextBox.Text.Trim();
                            tempValid = true;
                        }
                        else
                        {
                            tempValid = false;
                        }

                        break;
                    }

                case ConfigType.ChoiceType:
                    {
                        if (this.ChoiceCombo.SelectedIndex >= 0)
                        {
                            this.configValue = this.ChoiceCombo.SelectedItem.ToString();
                            tempValid = true;
                        }
                        else
                        {
                            tempValid = false;
                        }

                        break;
                    }
            }

            return tempValid;
        }

        /// <summary>
        /// Shows the attachment calender in particular location.
        /// </summary>
        private void ShowAttachmentCalender()
        {
            this.ConfigMonthCalander.Visible = true;
            this.ConfigMonthCalander.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.ConfigMonthCalander.Left = this.HeaderPanel.Left + this.DatePanel.Left + this.ConfigDatePictureBox.Left + this.ConfigDatePictureBox.Width;
            this.ConfigMonthCalander.Top = this.HeaderPanel.Top + this.DatePanel.Top + this.ConfigDatePictureBox.Top;
            this.ConfigMonthCalander.Tag = this.ConfigDatePictureBox.Tag;
            this.ConfigMonthCalander.Focus();
            if (!String.IsNullOrEmpty(this.ConfigDateTextBox.Text.Trim()))
            {
                this.ConfigMonthCalander.SetDate(Convert.ToDateTime(this.ConfigDateTextBox.Text));
            }

            this.ConfigMonthCalander.BringToFront();
        }

        /// <summary>
        /// Sets the text boxes values.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetTextBoxesValues(int rowId)
        {
            if (rowId >= 0 && !this.configChange)
            {
                this.fileType = this.OptionGridView.Rows[rowId].Cells["Type"].Value.ToString();
                this.OptionTextBox.Text = this.OptionGridView.Rows[rowId].Cells["DisplayName"].Value.ToString();
                this.DescriptionTextBox.Text = this.OptionGridView.Rows[rowId].Cells["Description"].Value.ToString();
                //this.NameTextBox.Text = this.configDetails.Tables[0].Rows[rowId]["CfgName"].ToString();
                this.NameTextBox.Text = this.OptionGridView.Rows[rowId].Cells["CfgName"].Value.ToString();

                if (this.FilePanel.Visible)
                {
                    this.FilePathTextBox.Text = this.OptionGridView.Rows[rowId].Cells["CfgValue"].Value.ToString();
                    this.ValueTextBox.Text = string.Empty;
                    this.ConfigDateTextBox.Text = string.Empty;
                    this.ChoiceCombo.Text = string.Empty;
                }
                else if (this.TextPanel.Visible)
                {
                    this.ValueTextBox.Text = this.OptionGridView.Rows[rowId].Cells["CfgValue"].Value.ToString();
                    this.FilePathTextBox.Text = string.Empty;
                    this.ConfigDateTextBox.Text = string.Empty;
                    this.ChoiceCombo.Text = string.Empty;
                }
                else if (this.DatePanel.Visible)
                {
                    this.ValueTextBox.Text = string.Empty;
                    this.FilePathTextBox.Text = string.Empty;
                    this.ChoiceCombo.Text = string.Empty;
                    this.ConfigDateTextBox.Text = this.OptionGridView.Rows[rowId].Cells["CfgValue"].Value.ToString();
                }
                else if (this.ChoicePanel.Visible)
                {
                    this.ValueTextBox.Text = string.Empty;
                    this.FilePathTextBox.Text = string.Empty;
                    this.ConfigDateTextBox.Text = string.Empty;
                    this.SetChoiceDataCombo(this.OptionGridView.Rows[rowId].Cells["cfgId"].Value.ToString());
                    this.SetTheComboboxValue(this.OptionGridView.Rows[rowId].Cells["CfgValue"].Value.ToString());
                }
            }
        }

        /// <summary>
        /// Sets the choice data combo.
        /// </summary>
        /// <param name="keyID">The key ID.</param>
        private void SetChoiceDataCombo(string keyID)
        {
            try
            {
                this.ChoiceCombo.Items.Clear();
                int le = 0;
                DataRow[] emptyRow1;
                string expression;
                expression = "CfgID = " + keyID;
                emptyRow1 = this.configDetails.Tables[1].Select(expression);
                while (le < emptyRow1.Length)
                {
                    this.ChoiceCombo.Items.Add(emptyRow1[le][2].ToString());
                    le++;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the data grid value.
        /// </summary>
        /// <param name="valueToBeSet">The value to be set.</param>
        private void SetDataGridValue(string valueToBeSet)
        {
            if (this.currentRow >= 0)
            {
                this.OptionGridView.Rows[this.currentRow].Cells["CfgValue"].Value = valueToBeSet;
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the CountyConfigurationForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CountyConfigurationForm_Load(object sender, EventArgs e)
        {
            ////this.SaveMenu.Click += new EventHandler(this.CountyConfigurationSave_Click);
            //// Customise DataGrid 
            this.CustomiseDataGridView();

            //// used to load the configdatagrid
            this.LoadConfigDataGridView();

            if (this.countyRowCount > 0)
            {
                TerraScanCommon.SetDataGridViewPosition(this.OptionGridView, 0);
            }

            this.OptionTextBox.BackColor = System.Drawing.Color.White;
            this.DescriptionTextBox.BackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// Handles the DateSelected event of the attachmentMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void AttachmentMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            // Assign the selected date to the DateTextbox.
            this.ConfigDateTextBox.Text = e.Start.ToShortDateString();
            this.configChange = true;
            this.DisableOptionGridSorting();
            this.ConfigMonthCalander.Visible = false;
            this.ConfigDateTextBox.Focus();
        }

        /// <summary>
        /// Handles the CellClick event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void OptionGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //// Set the Current Row
                this.currentRow = e.RowIndex;

                if (this.configChange)
                {
                    if (this.tempRowId != e.RowIndex)
                    {
                        ////if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        ////{
                        ////    this.configDetails.RejectChanges();
                        ////    this.configChange = false;
                        ////    this.configType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString());
                        ////    this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString());
                        ////    this.SetVisiblePanels((ConfigType)this.selectedType);
                        ////    this.SetChoiceDataCombo(this.configType.ToString());
                        ////    this.SetTheComboboxValue(this.OptionGridView.Rows[e.RowIndex].Cells["CfgValue"].Value.ToString());
                        ////    //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                        ////    this.ConfigLinkLabel.Text = "tTS_Cfg[CfgID]" + this.configType;
                        ////    this.SetTextBoxesValues(e.RowIndex);
                        ////    this.SetTextBoxesFocus();
                        ////    //// disable option grid
                        ////    this.EnableOptionGrid();
                        ////    this.tempRowId = e.RowIndex; 
                        ////}
                        ////else
                        ////{
                        ////    this.SetDataGridViewPosition(this.OptionGridView, this.tempRowId);
                        ////    this.SetTextBoxesValues(this.tempRowId);
                        ////    this.SetTextBoxesFocus();
                        ////}

                        ////switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm") , this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                {
                                    this.SaveConfig();
                                    if (this.formClosing)
                                    {
                                        this.tempRowId = e.RowIndex;
                                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString()))
                                        {
                                            this.configType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString());
                                        }
                                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString()))
                                        {
                                            this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString());
                                        }
                                        this.SetVisiblePanels((ConfigType)this.selectedType);
                                        this.SetChoiceDataCombo(this.configType.ToString());
                                        this.SetTheComboboxValue(this.OptionGridView.Rows[e.RowIndex].Cells["CfgValue"].Value.ToString());
                                        //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                                        this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.configType;
                                        this.SetTextBoxesValues(e.RowIndex);
                                        this.SetDataGridViewPosition(this.OptionGridView, e.RowIndex);
                                    }
                                    else
                                    {
                                        this.SetDataGridViewPosition(this.OptionGridView, this.tempRowId);
                                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString()))
                                        {
                                            this.configType = Convert.ToInt32(this.OptionGridView.Rows[this.tempRowId].Cells["CfgID"].Value.ToString());
                                        }

                                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString()))
                                        {
                                            this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[this.tempRowId].Cells["Type"].Value.ToString());
                                        }
                                        this.SetVisiblePanels((ConfigType)this.selectedType);
                                        this.SetChoiceDataCombo(this.configType.ToString());
                                        this.SetTheComboboxValue(this.OptionGridView.Rows[this.tempRowId].Cells["CfgValue"].Value.ToString());
                                        //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                                        this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.configType;
                                        this.SetTextBoxesValues(this.tempRowId);
                                    }

                                    break;
                                }

                            case DialogResult.No:
                                {
                                    this.configDetails.RejectChanges();
                                    this.configChange = false;
                                    this.LoadConfigDataGridView();
                                    if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString()))
                                    {
                                        this.configType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString());
                                    }
                                    if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString()))
                                    {
                                        this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString());
                                    }
                                    this.SetVisiblePanels((ConfigType)this.selectedType);
                                    this.SetChoiceDataCombo(this.configType.ToString());
                                    this.SetTheComboboxValue(this.OptionGridView.Rows[e.RowIndex].Cells["CfgValue"].Value.ToString());
                                    //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                                    this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.configType;
                                    this.SetTextBoxesValues(e.RowIndex);
                                    //// disable option grid
                                    this.EnableOptionGridSorting();
                                    this.tempRowId = e.RowIndex;
                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    TerraScanCommon.SetDataGridViewCellPosition(this.OptionGridView, this.tempRowId, this.clickColumIndex);

                                    this.SetTextBoxesValues(this.tempRowId);
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    if (e.ColumnIndex >= 0)
                    {
                        this.clickColumIndex = e.ColumnIndex;
                    }
                    else
                    {
                        this.clickColumIndex = 0;
                    }

                    this.tempRowId = this.currentRow;
                    if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString()))
                    {
                        this.configType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["CfgID"].Value.ToString());
                    }
                    if (!string.IsNullOrEmpty(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString()))
                    {
                        this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString());
                    }
                    this.SetTextBoxesValues(this.tempRowId);
                    if (this.configType > 0 && this.selectedType > 0)
                    {
                        this.SetVisiblePanels((ConfigType)this.selectedType);
                        this.SetChoiceDataCombo(this.configType.ToString());
                        this.SetTheComboboxValue(this.OptionGridView.Rows[e.RowIndex].Cells["CfgValue"].Value.ToString());

                        //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                        this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.configType;
                    }
                }
            }
        }

        /// <summary>
        /// Used to Set the correct value to the ComboBox
        /// </summary>
        /// <param name="comboxString">The combox string.</param>
        private void SetTheComboboxValue(string comboxString)
        {
            int correctIndex = 0;
            //// get the index of the cfgValue
            correctIndex = this.ChoiceCombo.FindString(comboxString);
            this.ChoiceCombo.SelectedIndex = correctIndex;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ChocieCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ChocieCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.configChange = true;
            //// Disable Option Grid
            this.DisableOptionGridSorting();
        }

        /// <summary>
        /// Handles the Click event of the ConfigDatePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConfigDatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowAttachmentCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OptionGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.OptionGridView.SelectedRows != null && this.OptionGridView.Rows.Count > 0)
            {
                try
                {
                    int index = Convert.ToInt32(this.OptionGridView.SelectedRows[0].Index.ToString());

                    this.currentRow = index;
                    if (!this.configChange)
                    {
                        this.tempRowId = this.currentRow;
                    }

                    if (this.configChange)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.configDetails.RejectChanges();
                            this.configChange = false;
                        }
                        else
                        {
                            this.SetDataGridViewPosition(this.OptionGridView, this.tempRowId);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.currentRow].Cells["CfgID"].Value.ToString()))
                        {
                            this.configType = Convert.ToInt32(this.OptionGridView.Rows[this.currentRow].Cells["CfgID"].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.currentRow].Cells["Type"].Value.ToString()))
                        {
                            this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[this.currentRow].Cells["Type"].Value.ToString());
                        }
                        this.SetVisiblePanels((ConfigType)this.selectedType);
                        this.SetChoiceDataCombo(this.selectedType.ToString());
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the FileOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FileOpen_Click(object sender, EventArgs e)
        {
            if (string.Compare(this.fileType, "4") == 0)
            {
                this.configOpenDialog.Filter = "Windows Bitmaps(*.bmp)|*.bmp|JPEG Files(*.jpg)|*.jpg|GIF Files(*.gif)|*.gif|TIFF Files(*.tif)|*.tif|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
                this.configOpenDialog.FilterIndex = 6;
                this.configOpenDialog.Multiselect = false;

                this.configOpenDialog.FileName = string.Empty;
                this.configOpenDialog.ShowDialog();

                if (!string.IsNullOrEmpty(this.configOpenDialog.FileName))
                {
                    this.FilePathTextBox.Text = this.configOpenDialog.FileName;
                    this.configChange = true;
                    //// disable option grid
                    this.DisableOptionGridSorting();
                }
            }
            else if (string.Compare(this.fileType, "5") == 0)
            {
                this.configFolderDialog.SelectedPath = string.Empty;
                this.configFolderDialog.ShowDialog();

                if (!string.IsNullOrEmpty(this.configFolderDialog.SelectedPath))
                {
                    if (this.configFolderDialog.SelectedPath.Length <= 3)
                    {
                        this.FilePathTextBox.Text = this.configFolderDialog.SelectedPath;
                    }
                    else
                    {
                        this.FilePathTextBox.Text = this.configFolderDialog.SelectedPath + "\\";
                    }
                    this.configChange = true;
                    //// disable option grid
                    this.DisableOptionGridSorting();
                }
                else
                {
                    this.configChange = false;
                }
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param> 
        private void OptionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.configChange && this.countyRowCount > 0)
            {
                this.cellRow = e.RowIndex;
                this.tempRowId = e.RowIndex;
                if (e.ColumnIndex >= 0)
                {
                    this.clickColumIndex = e.ColumnIndex;
                }
                else
                {
                    this.clickColumIndex = 0;
                }
                //// inorder to avoid muliple click and maintain the row position
                ////if (!this.saveButton)
                ////{
                ////    this.tempRowId = e.RowIndex;
                ////}

                if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.cellRow].Cells["CfgID"].Value.ToString()))
                {
                    this.configType = Convert.ToInt32(this.OptionGridView.Rows[this.cellRow].Cells["CfgID"].Value.ToString());
                }

                if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.cellRow].Cells["Type"].Value.ToString()))
                {
                    this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[this.cellRow].Cells["Type"].Value.ToString());
                }
                this.SetVisiblePanels((ConfigType)this.selectedType);

                this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.configType;

                if (this.selectedType == 3)
                {
                    this.SetChoiceDataCombo(this.configType.ToString());
                    this.SetTheComboboxValue(this.OptionGridView.Rows[this.cellRow].Cells["CfgValue"].Value.ToString());
                }

                this.SetTextBoxesValues(this.cellRow);
            }
            else
            {
                /////  this.CountyConfigurationSave.Focus();
                ////   this.tempRowId = e.RowIndex;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.CapsLock:
                    {
                        break;
                    }

                case Keys.ShiftKey:
                    {
                        break;
                    }

                default:
                    {
                        this.configChange = true;
                        //// Disable option Grid 
                        this.DisableOptionGridSorting();
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the FilePathTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FilePathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.CapsLock:
                    {
                        break;
                    }

                case Keys.ShiftKey:
                    {
                        break;
                    }

                default:
                    {
                        this.configChange = true;
                        //// Disable option Grid 
                        this.DisableOptionGridSorting();
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ConfigDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ConfigDateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.CapsLock:
                    {
                        break;
                    }

                case Keys.ShiftKey:
                    {
                        break;
                    }

                default:
                    {
                        this.configChange = true;
                        //// Disable option Grid 
                        this.DisableOptionGridSorting();
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the Click event of the CountyConfigurationSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CountyConfigurationSave_Click(object sender, EventArgs e)
        {
            this.CountyConfigurationSave.Focus();
            this.SaveConfig();
        }

        /// <summary>
        /// Saves the config.
        /// </summary>
        private void SaveConfig()
        {
            try
            {
                if (this.OptionGridView.CurrentCell != null)
                {
                    int currentRowIndex = this.OptionGridView.CurrentCell.RowIndex;
                    //this.nextNumDetailsDateSet = F1016WorkItem.ListNextNumberConfiguration();
                    this.OptionGridView.AutoGenerateColumns = false;
                    //DataTable tempDataTable = this.nextNumDetailsDateSet.Tables[0].Clone();
                    //tempDataTable.Columns["NextNumID"].AllowDBNull = true;
                    //tempDataTable.Columns["NextNumID"].DataType = typeof(int);
                    //tempDataTable.Load(this.nextNumDetailsDateSet.Tables[0].CreateDataReader());
                    //this.NextNumRecordsGridView.DataSource = tempDataTable;
                    //this.DisplayNextNumberHeaderDetails(currentRowIndex);
                    this.OptionGridView.CurrentRow.Selected = true;
                    this.OptionGridView.Rows[this.OptionGridView.CurrentCell.RowIndex].Selected = false;
                    this.OptionGridView.CurrentCell = this.OptionGridView[0, Convert.ToInt32(currentRowIndex)];

                    if (this.GetConfigDesc((ConfigType)this.selectedType))
                    {
                        this.validFileType = true;
                        //// check for file type
                        if ((ConfigType)this.selectedType == ConfigType.FilePath)
                        {
                            if (File.Exists(this.configValue))
                            {
                                this.validFileType = true;
                            }
                            else
                            {
                                this.validFileType = false;
                            }
                        }
                        else if ((ConfigType)this.selectedType == ConfigType.FolderPath)
                        {
                            if (Directory.Exists(this.configValue))
                            {
                                this.validFileType = true;
                            }
                            else
                            {
                                this.validFileType = false;
                            }
                        }

                        if (this.validFileType)
                        {
                            int configRowID;
                            this.Cursor = Cursors.WaitCursor;
                            //// Inorder to avoid value change in row enter Event
                            configRowID = this.tempRowId;
                            F9020WorkItem.UpdateCountyConfigDetails(this.configType, this.configValue.Trim(),TerraScanCommon.UserId);
                            //// this.saveButton = true;
                            if (this.configType.Equals(40))
                            {
                                if (this.configValue.Trim().ToLower().Equals("true"))
                                {
                                    TerraScanCommon.debugConfiguration = true;
                                    ////Commented by purushotham due to string value cannot be convert to boolean directly
                                    //= Convert.ToBoolean(this.configValue.Trim());
                                }
                                else
                                {
                                    TerraScanCommon.debugConfiguration = false;
                                }
                            }

                            if (this.configType.Equals(41))
                            {
                                //Commented the binded value and set 0 for session time out purushotham to resolve 19872
                                TerraScanCommon.barCodeSessionTimeOut = 0;// Convert.ToInt32(this.configValue.Trim());
                            }

                            this.formClosing = true;
                            //// used to load the configdatagrid
                            this.LoadConfigDataGridView();
                            this.configChange = false;
                            this.SetDataGridViewPosition(this.OptionGridView, configRowID);
                            this.SetTextBoxesValues(this.tempRowId);
                            //// EnableOptionGrid option Grid 
                            this.EnableOptionGridSorting();
                            this.OptionGridView.Focus();
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            this.formClosing = false;
                            MessageBox.Show(SharedFunctions.GetResourceString("FolderPath"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        this.formClosing = false;
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    this.formClosing = false;
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the CountyConfigurationSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CountyConfigurationSave_Leave(object sender, EventArgs e)
        {
            if (this.configChange)
            {
                /* if (this.FilePanel.Visible)
                //{
                //    this.FilePathTextBox.Focus(); 
                //}
                //else if (this.TextPanel.Visible)
                //{
                //    this.ValueTextBox.Focus(); 
                //} 
                //else if (this.DatePanel.Visible)
                //{
                //    this.ConfigDateTextBox.Focus(); 
                //}
                //else if (this.ChoicePanel.Visible)
                //{
                //    this.ChoiceCombo.Focus(); 
                //}*/
            }
        }
        #endregion

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        /// 
        private void OptionGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //// TODO
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void DisableOptionGridSorting()
        {
            DataGridViewColumnCollection disableSortColumn = this.OptionGridView.Columns;
            disableSortColumn["CfgValue"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableSortColumn["DisplayName"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void EnableOptionGridSorting()
        {
            DataGridViewColumnCollection enableSortColumn = this.OptionGridView.Columns;
            enableSortColumn["CfgValue"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["DisplayName"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        ///// COMMENTED BECAUSE AS PER NEW REQUEST AVOID FOCUS ON TEXT WHEN USER CLICKS ON GRID
        ///// <summary>
        ///// Sets the text boxes values.
        ///// </summary>

        ////private void SetTextBoxesFocus()
        ////{
        ////    if (this.FilePanel.Visible)
        ////    {
        ////        this.FilePathTextBox.Focus();
        ////    }
        ////    else if (this.TextPanel.Visible)
        ////    {
        ////        this.ValueTextBox.Focus();
        ////    }
        ////    else if (this.DatePanel.Visible)
        ////    {
        ////        this.ConfigDateTextBox.Focus();
        ////    }
        ////    else if (this.ChoicePanel.Visible)
        ////    {
        ////        this.ChoiceCombo.Focus();
        ////    }
        ////}

        /// <summary>
        /// Handles the DataBindingComplete event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void OptionGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ////  this.saveButton = false;
        }

        /// <summary>
        /// Handles the FormClosing event of the CountyConfigurationForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9020_FormClosing(object sender, FormClosingEventArgs e)
        {
            ////if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
            ////{
            ////    if (e.CloseReason.Equals(CloseReason.UserClosing))
            ////    {
            ////        if (this.configChange)
            ////        {
            ////            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            ////            {
            ////                case DialogResult.Yes:
            ////                    {
            ////                        this.SaveConfig();
            ////                        if (this.formClosing)
            ////                        {
            ////                            this.DialogResult = DialogResult.No;
            ////                            e.Cancel = false;
            ////                        }
            ////                        else
            ////                        {
            ////                            e.Cancel = true;
            ////                        }
            ////                        break;
            ////                    }
            ////                case DialogResult.No:
            ////                    {
            ////                        this.DialogResult = DialogResult.No;
            ////                        e.Cancel = false;
            ////                        break;
            ////                    }
            ////                case DialogResult.Cancel:
            ////                    {
            ////                        e.Cancel = true;
            ////                        break;
            ////                    }
            ////            }
            ////        }
            ////    }
            ////}
        }

        /// <summary>
        /// Handles the KeyPress event of the ValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //// inorder to avoid enter

            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the OptionPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OptionPanel_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Handles the Click event of the DescriptionPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionPanel_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Handles the Click event of the OptionLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OptionLabel_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Sets the calander in visisble.
        /// </summary>
        private void SetCalanderInVisisble()
        {
            this.ConfigMonthCalander.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the OptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OptionTextBox_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Handles the Click event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Handles the Click event of the DescriptionLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionLabel_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Handles the MouseEnter event of the ValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValueTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.ValueTextBox.Text))
            {
                if (this.ValueTextBox.Text.Length > 36)
                {
                    this.valueToolTip.SetToolTip(this.ValueTextBox, this.ValueTextBox.Text);
                }
                else
                {
                    this.valueToolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the FilePathTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FilePathTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.FilePathTextBox.Text))
            {
                if (this.FilePathTextBox.Text.Length > 36)
                {
                    this.valueToolTip.SetToolTip(this.FilePathTextBox, this.FilePathTextBox.Text);
                }
                else
                {
                    this.valueToolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ConfigDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConfigDateTextBox_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Handles the Click event of the DateLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DateLabel_Click(object sender, EventArgs e)
        {
            this.SetCalanderInVisisble();
        }

        /// <summary>
        /// Handles the KeyPress event of the ConfigDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ConfigDateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    // Calls the method to show the calender control.
                    this.ShowAttachmentCalender();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ConfigMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConfigMonthCalander_Leave(object sender, EventArgs e)
        {
            this.ConfigMonthCalander.Visible = false;
        }

        /// <summary>
        /// Handles the KeyDown event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void OptionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.configChange)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            this.SetCountyconfig(e);

                            break;
                        }

                    case Keys.Up:
                        {
                            this.SetCountyconfig(e);

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Handles setCountyconfig
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void SetCountyconfig(KeyEventArgs e)
        {
            //switch (MessageBox.Show(string.Concat(string.Concat(SharedFunctions.GetResourceString("CancelForm"),"",  this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)))
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        this.SaveConfig();
                        //// this.saveButton = false;
                        e.Handled = false;
                        this.configChange = false;
                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.tempRowId].Cells["CfgID"].Value.ToString()))
                        {
                            this.configType = Convert.ToInt32(this.OptionGridView.Rows[this.tempRowId].Cells["CfgID"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.tempRowId].Cells["Type"].Value.ToString()))
                        {
                            this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[this.tempRowId].Cells["Type"].Value.ToString());
                        }
                        this.SetVisiblePanels((ConfigType)this.selectedType);
                        this.SetChoiceDataCombo(this.configType.ToString());
                        this.SetTheComboboxValue(this.OptionGridView.Rows[this.tempRowId].Cells["CfgValue"].Value.ToString());
                        //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                        this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.configType;
                        this.SetTextBoxesValues(this.tempRowId);

                        break;
                    }

                case DialogResult.No:
                    {
                        this.configDetails.RejectChanges();
                        this.configChange = false;
                        e.Handled = false;
                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.tempRowId].Cells["CfgID"].Value.ToString()))
                        {
                            this.configType = Convert.ToInt32(this.OptionGridView.Rows[this.tempRowId].Cells["CfgID"].Value.ToString());
                        }
                        if (!string.IsNullOrEmpty(this.OptionGridView.Rows[this.tempRowId].Cells["Type"].Value.ToString()))
                        {
                            this.selectedType = Convert.ToInt32(this.OptionGridView.Rows[this.tempRowId].Cells["Type"].Value.ToString());
                        }
                        this.SetVisiblePanels((ConfigType)this.selectedType);
                        this.SetChoiceDataCombo(this.configType.ToString());
                        this.SetTheComboboxValue(this.OptionGridView.Rows[this.tempRowId].Cells["CfgValue"].Value.ToString());
                        //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                        this.ConfigLinkLabel.Text = "tTS_Cfg [CfgID] " + this.configType;
                        this.SetTextBoxesValues(this.tempRowId);
                        //// disable option grid
                        this.EnableOptionGridSorting();

                        break;
                    }

                case DialogResult.Cancel:
                    {
                        e.Handled = true;

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the DataGridViewBindingComplete event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void OptionGridView_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.SetDataGridViewPosition(this.OptionGridView, 0);
        }

        /// <summary>
        /// Handles the Link event of the ConfigLinkLabel control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConfigLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.configType > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(90101);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.configType;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Cursor = Cursors.Default;
            }
            
            ////this.Cursor = Cursors.WaitCursor;
            ////TerraScanCommon.ShowAuditReport("CfgID", this.configType.ToString());
            ////this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the KeyDown event of the DateLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConfigMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.ConfigDateTextBox.Text = this.ConfigMonthCalander.SelectionStart.ToShortDateString();
                this.configChange = true;
                this.DisableOptionGridSorting();
                this.ConfigMonthCalander.Visible = false;
                this.ConfigDateTextBox.Focus();
            }
        }

        /// <summary>
        /// Loads F9020 Form
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void F9020_Load(object sender, EventArgs e)
        {
            this.SaveCounty.Click += new EventHandler(this.CountyConfigurationSave_Click);
            //// Customise DataGrid 
            this.CustomiseDataGridView();
            this.LoadWorkSpaces();
            //// used to load the configdatagrid
            this.LoadConfigDataGridView();

            if (this.countyRowCount > 0)
            {
                TerraScanCommon.SetDataGridViewPosition(this.OptionGridView, 0);
            }

            this.OptionTextBox.BackColor = System.Drawing.Color.White;

            this.DescriptionTextBox.BackColor = System.Drawing.Color.White;
            // Coding modified for the issue 1107[Focus should be in first editable field]
            this.FilePathTextBox.Select();
            this.FilePathTextBox.Focus();
        }

        /// <summary>
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            // To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
            if (this.form9020Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form9020Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form9020Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = "Configuration";
            formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>bool pageStatus</returns>
        private bool CheckPageStatus()
        {
            bool pageStatus = true;
            if (this.configChange)
            {
                //switch (MessageBox.Show(string.Concat(string.Concat(SharedFunctions.GetResourceString("CancelForm"),"",  this.AccessibleName  , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)))
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            this.SaveConfig();
                            if (this.formClosing)
                            {
                                pageStatus = true;
                            }
                            else
                            {
                                pageStatus = false;
                            }

                            break;
                        }

                    case DialogResult.No:
                        {
                            pageStatus = true;
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            pageStatus = false;
                            break;
                        }
                }
            }

            return pageStatus;
        }

        /// <summary>
        /// Handles the Resize event of the OptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OptionGridView_Resize(object sender, EventArgs e)
        {
            this.pictureBox2.Image = ExtendedGraphics.GenerateVerticalImage(this.OptionGridView.Height, this.pictureBox2.Width, "Configuration Options", 28, 81, 128);
        }

        private void FilePathTextBox_Leave(object sender, EventArgs e)
       {
        //    this.panel1.Focus();
        //    this.panel1.Select();
 
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void OptionGridView_MouseDown(object sender, MouseEventArgs e)
        //{
        //    DataGridView.HitTestInfo hit = this.OptionGridView.HitTest(e.X, e.Y);

        //    if (hit.RowIndex > -1 && hit.RowIndex != this.OptionGridView.CurrentCell.RowIndex)
        //    {
        //        this.gridSelected = true;
        //    }

        //}




    }
}
