//--------------------------------------------------------------------------------------------
// <copyright file="F1016.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 May 06        JYOTHI              Created
// 30 June 06       JYOTHI              Changed MessageBoxButtons to YesNoCancel
// 27 Jul 06        VINOTHBABU          Modified for CAB Conversion
//*********************************************************************************/

namespace D1016
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// UserControl for F1016
    /// </summary>
    [SmartPart]
    public partial class F1016 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// nextNumDetailsDateSet variable is used to store list of Next Number Records details. 
        /// </summary>       
        private DataSet nextNumDetailsDateSet = new DataSet("Root");

        /// <summary>
        /// tempDataSet variable is used to store the test result value for Next Number Configuration. 
        /// </summary>
        private DataSet tempDataSet = new DataSet("Root");

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to Hold the current row value of NextNumberRecordsGridView
        /// </summary>
        private int currentRow;

        /// <summary>
        /// Used to Hold the current row value of NextNumberRecordsGridView
        /// </summary>
        private bool gridSelected;

        /// <summary>
        /// Creates Instance for F1016Controller
        /// </summary>
        private F1016Controller form1016Control;

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NextNumberForm"/> class.
        /// </summary>
        public F1016()
        {
            this.InitializeComponent();
            this.NextNumRecordsGridView.MouseDown += new MouseEventHandler(this.NextNumRecordsGridView_MouseDown);
            this.NextNumRecordsGridView.RowValidating += new DataGridViewCellCancelEventHandler(this.NextNumRecordsGridView_RowValidating);
            this.NextNumRecordsGridView.KeyDown += new KeyEventHandler(this.NextNumRecordsGridView_KeyDown);
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Next Number", 28, 81, 128);
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, "Next Number Records", 28, 81, 128);
        }

        #endregion

        #region EventPublication

        ///// <summary>
        ///// Event publication for setting the SetButton
        ///// </summary>
        ////[EventPublication("topic://Terrascan.UI.CAB/Modules/SetButtons", PublicationScope.WorkItem)]
        ////public event EventHandler<DataEventArgs<Enum>> SetButtons;

        /// <summary>
        /// Event Publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        #endregion

        #region Property

        /// <summary>
        /// Creates Property for F1016Controller
        /// </summary>
        [CreateNew]
        public F1016Controller F1016Control
        {
            get { return this.form1016Control as F1016Controller; }
            set { this.form1016Control = value; }
        }

        #endregion

        #region EventSubscription

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
                this.F1016Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        /// <summary>
        /// Handles the Button Click Event For the WorkSpace
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;                
            }
        }

        /// <summary>
        /// Sets the form cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetFormCancelButton, Thread = ThreadOption.UserInterface)]
        public void SetFormCancelButton(object sender, DataEventArgs<string> e)
        {
            this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
        }

        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
        }

        [EventSubscription(EventTopicNames.AuditLinkClick, ThreadOption.UserInterface)]
        public void OnAuditLinkClick(object sender, DataEventArgs<LinkLabel> eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowAuditReport("NextNumID", this.NextNumberIDTextBox.Text);
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

        #endregion

        #region Events

        /// <summary>
        /// Handles the KeyDown event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 38)
            {
                this.gridSelected = true;
            }
        }

        /// <summary>
        /// Handles the RowValidating event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            ////this.currentRow = e.RowIndex;

            if (this.gridSelected)
            {
                this.gridSelected = false;
                if (this.operationSmartPart.SaveButtonEnable == true)
                {
                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (!this.SaveRecord())
                        {
                            e.Cancel = true;
                            this.NextNumberTextBox.Focus();
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = false;
                        ////this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.SaveMode);
                        this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.NextNumRecordsGridView.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
                        this.NextNumRecordsGridView.TabStop = true;
                        ////e.Cancel = true;
                        ////this.NextNumberTextBox.Focus();
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        this.NextNumberTextBox.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = this.NextNumRecordsGridView.HitTest(e.X, e.Y);

            if (hit.RowIndex > -1 && hit.RowIndex != this.NextNumRecordsGridView.CurrentCell.RowIndex)
            {
                this.gridSelected = true;
            }
        }

        ////private void SaveButton_Click(object sender, EventArgs e)
        ////{
        ////    //ToDo: Have to Call Some Dialog form
        ////    object[] optionalParameter = new object[]{202,1};
        ////    Form testForm = new Form();
        ////    testForm = TerraScanCommon.GetForm("1111", optionalParameter, true);
        ////    if (testForm != null)
        ////    {
        ////        testForm.ShowDialog();
        ////    }
        ////}

        #endregion
        
        #region Next Number Records

        /// <summary>
        /// Loads the next number config.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void LoadNextNumberConfig(int currentRowIndex)
        {
            ////this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
            if (currentRowIndex >= 0)
            {
                this.nextNumDetailsDateSet = F1016WorkItem.ListNextNumberConfiguration();
                this.NextNumRecordsGridView.AutoGenerateColumns = false;
                DataTable tempDataTable = this.nextNumDetailsDateSet.Tables[0].Clone();
                tempDataTable.Columns["NextNumID"].AllowDBNull = true;
                tempDataTable.Columns["NextNumID"].DataType = typeof(int);
                tempDataTable.Load(this.nextNumDetailsDateSet.Tables[0].CreateDataReader());
                this.NextNumRecordsGridView.DataSource = tempDataTable;
                this.DisplayNextNumberHeaderDetails(currentRowIndex);
                this.NextNumRecordsGridView.CurrentRow.Selected = true;
                ////this.LoadTestResult(Convert.ToInt32(this.RollYearTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text), this.FormulaTextBox.Text);
                this.NextNumRecordsGridView.Rows[this.NextNumRecordsGridView.CurrentCell.RowIndex].Selected = false;
                ////TerraScanCommon.SetDataGridViewPosition(this.NextNumRecordsGridView, currentRowIndex);
                this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, Convert.ToInt32(currentRowIndex)];
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////if (this.EditPermissionButton.ActualPermission == true)
            ////{
            ////    this.NextNumberTextBox.LockKeyPress = false;
            ////    this.FormulaTextBox.LockKeyPress = false;
            ////}
            ////else
            ////{
            ////    this.NextNumberTextBox.LockKeyPress = true;
            ////    this.FormulaTextBox.LockKeyPress = true;
            ////}
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            ////if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.EditPermissionButton.ActualPermission)
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission)
            {
                ////this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.EditMode));
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.NextNumRecordsGridView.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.NextNumRecordsGridView.TabStop = false;
            }
        }

        /// <summary>
        /// Displays the next number header details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void DisplayNextNumberHeaderDetails(int currentRowIndex)
        {
            if (currentRowIndex >= 0)
            {
                if (!string.IsNullOrEmpty(this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["ID"].Value.ToString()))
                {
                    this.NextNumberIDTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["ID"].Value.ToString();
                    this.PostTypeTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["PostType"].Value.ToString();
                    this.RollYearTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["Year"].Value.ToString();
                    this.NextNumberTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["NextNumberColumn"].Value.ToString();
                    this.FormulaTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["Formula"].Value.ToString();
                    this.footerSmartPart.KeyId = Convert.ToInt32(this.NextNumberIDTextBox.Text);
                    ////this.NextNumAuditlinkLabel.Text = "tTR_NextNum [NextNumID] " + this.NextNumberIDTextBox.Text;
                }
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>        
        private void SaveButton_Click()
        {
            if (this.operationSmartPart.SaveButtonEnable == true)
            {
                this.SaveRecord();
            }
        }

        /// <summary>
        /// Saves the record.
        /// </summary>
        /// <returns>SaveRecord</returns>
        private bool SaveRecord()
        {
            try
            {
                if (this.CheckErrors())
                {
                    if (!this.GetTestResult(Convert.ToInt32(this.RollYearTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), this.FormulaTextBox.Text.Trim()).Equals("INVALID"))
                    {
                        F1016WorkItem.UpdateNextNumberConfigDetails(Convert.ToInt32(this.NextNumberIDTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), this.FormulaTextBox.Text.Trim(), TerraScanCommon.UserId);
                        ////this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.SaveMode));
                        this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                        this.LoadNextNumberConfig(this.NextNumRecordsGridView.CurrentCell.RowIndex);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.NextNumRecordsGridView.Focus();
                        this.NextNumRecordsGridView.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
                        this.NextNumRecordsGridView.CurrentRow.Selected = true;
                        this.NextNumRecordsGridView.TabStop = true;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidFormula"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <returns>validationErrors</returns>
        private bool CheckErrors()
        {
            
            string validationErrors = string.Empty;

            if (string.IsNullOrEmpty(this.NextNumberTextBox.Text.Trim()))
            {
                validationErrors = validationErrors + "Next Number value should not be empty. \n";
            }

            if (string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()))
            {
                validationErrors = validationErrors + "Formula value should not be empty.";
            }

            if (!string.IsNullOrEmpty(validationErrors))
            {
                MessageBox.Show(validationErrors, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (string.IsNullOrEmpty(this.NextNumberTextBox.Text.Trim()))
                {
                    this.NextNumberTextBox.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()))
                    {
                        this.FormulaTextBox.Focus();
                    }
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        private void CancelButton_Click()
        {
            ////if (MessageBox.Show(SharedFunctions.GetResourceString("Cancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            ////{
            ////this.TestResultTextBox.Text = string.Empty;
            this.DisplayNextNumberHeaderDetails(this.NextNumRecordsGridView.CurrentCell.RowIndex);
            this.TestResultTextBox.Text = this.GetTestResult(Convert.ToInt32(this.RollYearTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), this.FormulaTextBox.Text.Trim());
            this.NextNumRecordsGridView.Rows[this.NextNumRecordsGridView.CurrentCell.RowIndex].Selected = false;
            this.NextNumRecordsGridView.Rows[this.NextNumRecordsGridView.CurrentCell.RowIndex].Selected = true;
            this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, Convert.ToInt32(this.NextNumRecordsGridView.CurrentCell.RowIndex)];
            ////this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.TestResultTextBox.Text = string.Empty;
            this.NextNumRecordsGridView.Focus();
            this.NextNumRecordsGridView.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.NextNumRecordsGridView.TabStop = true;
            ////}
            ////else
            ////{
            ////    this.NextNumberTextBox.Focus();
            ////}
        }

        /// <summary>
        /// Handles the RowEnter event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.currentRow != e.RowIndex)
            {
                this.currentRow = e.RowIndex;
                this.TestResultTextBox.Text = string.Empty;
                this.DisplayNextNumberHeaderDetails(e.RowIndex);
            }
        }

        /// <summary>
        /// Handles the Click event of the TestFormulaButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TestFormulaButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CheckErrors())
                {
                    this.TestResultTextBox.Text = this.GetTestResult(Convert.ToInt32(this.RollYearTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text), this.FormulaTextBox.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the test result.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="nextNum">The next number.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>testresult</returns>
        private string GetTestResult(int year, int nextNum, string formula)
        {
            string testresult = string.Empty;
            this.tempDataSet = F1016WorkItem.CheckNextNumber(year, nextNum, formula);

            if (this.tempDataSet != null)
            {
                if (this.tempDataSet.Tables.Count > 0 && this.tempDataSet.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(this.tempDataSet.Tables[0].Rows[0]["ErrorMsg"].ToString()))
                {
                    testresult = this.tempDataSet.Tables[0].Rows[0]["Result"].ToString();
                }
            }

            return testresult;
        }

        /// <summary>
        /// Edits the record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void EditRecord(object sender, KeyEventArgs e)
        {
            this.SetEditRecord();
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>CheckPageStatus</returns>
        private bool CheckPageStatus()
        {
            if (String.Compare(this.pageMode.ToString(), TerraScanCommon.PageModeTypes.View.ToString(), true) != 0)
            {
                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveRecord();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    this.NextNumberTextBox.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the NextNumAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void NextNumAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
        
        /// <summary>
        /// Handles the Load event of the F1016.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">instance containing the event data.</param>
        private void F1016_Load(object sender, EventArgs e)
        {
            ////todo : this.CancelButton = this.CancelNextNumButton;
            ////todo : this.SaveMenu.Click += new EventHandler(this.SaveButton_Click);
            this.LoadWorkSpaces();
            this.LoadNextNumberConfig(0);
            this.NextNumRecordsGridView.CurrentRow.Selected = true;
        }

        /// <summary>
        /// Loads the WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form1016Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.F1016Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.F1016Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
            }

            this.operationSmartPart.NewButtonVisible = false;
            this.operationSmartPart.DeleteButtonEnable = false;

            // To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
            if (this.form1016Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1016Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1016Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = "Next Number Configuration";
            formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));

            // To Load FooterSmartPart into FooterWorkspace
            if (this.form1016Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form1016Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form1016Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form1016Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form1016Control.WorkItem;
            this.footerSmartPart.FormId = "1016";
            this.footerSmartPart.AuditLinkText = "tTR_NextNum [NextNumID] ";
            this.footerSmartPart.VisibleHelpButton = false;
            this.footerSmartPart.VisibleHelpLinkButton = true;
        }

        /// <summary>
        /// Handles the MouseEnter event of the TestResultTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TestResultTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.TestResultTextBox.Text))
            {
                if (this.TestResultTextBox.Text.Length > 16)
                {
                    this.NextNumberToolTip.SetToolTip(this.TestResultTextBox, this.TestResultTextBox.Text);
                }
                else
                {
                    this.NextNumberToolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.NextNumRecordsGridView.OriginalRowCount > 0)
            {
                this.NextNumRecordsGridView.Rows[0].Selected = true;
                this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, 0];
                this.NextNumRecordsGridView.Focus();
            }
        }

        private void ContentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

     

               
    }
}
