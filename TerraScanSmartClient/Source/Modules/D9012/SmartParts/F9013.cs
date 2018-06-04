//--------------------------------------------------------------------------------------------
// <copyright file="F9013.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9013 - Next Number Config.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  M.Vijayakumar      Created// 
//21/4/2009         Malliga            Coding Modified for the Issue 4006
//*********************************************************************************/

namespace D9012
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Text.RegularExpressions;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// Class File
    /// </summary>    
    public partial class F9013 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Used to store the "9013" string constant
        /// </summary>
        private readonly string formNo = "9013";

        /// <summary>
        /// Used to store the "IsDefault = 1" string constant
        /// </summary>
        private readonly string rollYearIsDefault = "IsDefault = 1";

        /// <summary>
        /// This contiains string constant keyid
        /// </summary>
        private readonly string keyId = "keyid";

        /// <summary>
        /// Used to Hold the current row value of NextNumberRecordsGridView
        /// </summary>
        private bool gridSelected;

        /// <summary>
        /// Used To store the Roll year value
        /// </summary>
        private int rollYear;

        /// <summary>
        /// Used to store the userId value
        /// </summary>
        private int userId;

        /// <summary>
        /// Used to store nextNumberId
        /// </summary>
        private int nextNumberId;

        /// <summary>
        /// Used to store applicationId
        /// </summary>
        private int applicationId;

        /// <summary>
        /// Used to store nextNumberConfigDetails
        /// </summary>
        private F9013NextNumberData nextNumberConfigDetails = new F9013NextNumberData();     

        /// <summary>
        /// tempDataSet variable is used to store the test result value for Next Number Configuration. 
        /// </summary>
        private DataSet tempDataSet = new DataSet("Root");

        /// <summary>
        /// Used to store the nextNumberRecordCount
        /// </summary>
        private int nextNumberRecordCount;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Creates Instance for F9013Controller
        /// </summary>
        private F9013Controller form9013Control;

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// F9013 Constructors
        /// </summary>
        public F9013()
        {
            this.InitializeComponent();
            this.NextNumRecordsGridView.MouseDown += new MouseEventHandler(this.NextNumRecordsGridView_MouseDown);
            this.NextNumRecordsGridView.RowValidating += new DataGridViewCellCancelEventHandler(this.NextNumRecordsGridView_RowValidating);
            this.NextNumRecordsGridView.KeyDown += new KeyEventHandler(this.NextNumRecordsGridView_KeyDown);
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Next Number", 28, 81, 128);
            this.GirdPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GirdPictureBox.Height, this.GirdPictureBox.Width, "Next Number Records", 28, 81, 128);
            ////this.CustimizeNextNumberSelectionGrid();
        }

        #endregion Constructor

        #region EventPublication

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

        #endregion EventPublication

        #region Property

        /// <summary>
        /// Creates Property for F1016Controller
        /// </summary>
        [CreateNew]
        public F9013Controller F9013Control
        {
            get { return this.form9013Control as F9013Controller; }
            set { this.form9013Control = value; }
        }

        #endregion Property

        #region EventSubscription

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            try
            {
                if (e.Data == this.GetType().Name)
                {
                    this.F9013Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [audit link click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.AuditLinkClick, ThreadOption.UserInterface)]
        public void OnAuditLinkClick(object sender, DataEventArgs<LinkLabel> eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowAuditReport("NextNumID", this.nextNumberId.ToString());
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

        #endregion EventSubscription

        #region Events

        /// <summary>
        /// Handles the Load event of the F9013 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9013_Load(object sender, EventArgs e)
        {
            try
            {
                this.userId = TerraScanCommon.UserId;
                this.applicationId = TerraScanCommon.ApplicationId;

                this.LoadWorkSpaces();
                this.CustimizeNextNumberSelectionGrid();
                this.LoadRollYearCombo();
                this.LoadNextNumberDataGrid(0);
                this.NextNumRecordsGridView.CurrentRow.Selected = true;
                this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, 0];
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ActiveControl = NextNumberTextBox;
                this.NextNumberTextBox.Focus();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                ////if (MessageBox.Show(SharedFunctions.GetResourceString("Cancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ////{
                ////this.TestResultTextBox.Text = string.Empty;
                this.DisplayNextNumberHeaderDetails(this.NextNumRecordsGridView.CurrentCell.RowIndex);

                if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    this.TestResultTextBox.Text = this.GetTestResult(Convert.ToInt32(this.RollYearTextBox.Text.Trim()), Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), this.FormulaTextBox.Text.Trim());
                }
                else
                {
                    this.NextNumberTextBox.Text = string.Empty;
                    this.FormulaTextBox.Text = string.Empty;
                }

                ////this.NextNumRecordsGridView.Rows[this.NextNumRecordsGridView.CurrentCell.RowIndex].Selected = false;
                this.NextNumRecordsGridView.Rows[this.NextNumRecordsGridView.CurrentCell.RowIndex].Selected = true;
                this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, Convert.ToInt32(this.NextNumRecordsGridView.CurrentCell.RowIndex)];
                ////this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.TestResultTextBox.Text = string.Empty;
                this.NextNumRecordsGridView.Focus();
                this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.NextNumRecordsGridView.TabStop = true;
                ////}
                ////else
                ////{
                ////    this.NextNumberTextBox.Focus();
                ////}
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>        
        private void SaveButton_Click()
        {
            try
            {
                if (this.operationSmartPart.SaveButtonEnable == true)
                {
                    this.SaveRecord();
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Alls the next number textbox text_changed Events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AllNextNumberTextBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.operationSmartPart.SaveButtonEnable)
                {
                    this.TestResultTextBox.Text = string.Empty;
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.DisplayNextNumberHeaderDetails(e.RowIndex);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.NextNumRecordsGridView.OriginalRowCount > 0)
                {
                    this.NextNumRecordsGridView.Rows[0].Selected = true;
                    this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, 0];
                    this.NextNumRecordsGridView.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the RollYearCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {   
                if (this.operationSmartPart.SaveButtonEnable)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (!this.SaveRecord())
                        {
                            this.NextNumberTextBox.Focus();
                            this.RollYearCombo.Text = this.rollYear.ToString();
                        }
                        else
                        {
                            int.TryParse(this.RollYearCombo.Text, out this.rollYear);
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            this.LoadNextNumberDataGrid(0);
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        int.TryParse(this.RollYearCombo.Text, out this.rollYear);
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.LoadNextNumberDataGrid(0);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        this.RollYearCombo.Text = this.rollYear.ToString();
                        return;
                        ////this.NextNumberTextBox.Focus();
                    }
                }
                else
                {
                    int.TryParse(this.RollYearCombo.Text, out this.rollYear);
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.LoadNextNumberDataGrid(0);
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 40 || e.KeyValue == 38)
                {
                    this.gridSelected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowValidating event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (this.gridSelected)
                {
                    this.gridSelected = false;
                    if (this.operationSmartPart.SaveButtonEnable)
                    {
                        DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                            this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                            this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                            this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
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
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the NextNumRecordsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void NextNumRecordsGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo hit = this.NextNumRecordsGridView.HitTest(e.X, e.Y);

                if (hit.RowIndex > -1 && hit.RowIndex != this.NextNumRecordsGridView.CurrentCell.RowIndex)
                {
                    this.gridSelected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                string testKeyId  = string.Empty;
                string tempFormulaText = string.Empty;
                if (this.FormulaTextBox.Text.Trim().ToLowerInvariant().Contains(this.keyId))
                {
                    Form form9014 = new Form();
                    object[] optionalParameter = new object[] { };
                    form9014 = this.form9013Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9014, optionalParameter, this.form9013Control.WorkItem);
                    if (form9014 != null)
                    {
                        if (form9014.ShowDialog() != DialogResult.None)
                        {
                            testKeyId = TerraScanCommon.GetValue(form9014, "TestKeyId");
                        }
                    }

                    if (this.CheckErrors())
                    {
                        tempFormulaText = this.FormulaTextBox.Text.Trim().ToLowerInvariant().Replace(this.keyId, testKeyId);
                        this.TestResultTextBox.Text = this.GetTestResult(Convert.ToInt32(this.RollYearTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), tempFormulaText);
                    }
                }
                else
                {
                    if (this.CheckErrors())
                    {
                        this.TestResultTextBox.Text = this.GetTestResult(Convert.ToInt32(this.RollYearTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), this.FormulaTextBox.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the FormulaTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void FormulaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>CheckPageStatus</returns>
        private bool CheckPageStatus()
        {
            if (String.Compare(this.pageMode.ToString(), TerraScanCommon.PageModeTypes.View.ToString(), true) != 0)
            {
                DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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
                    this.DescriptionTextBox.Focus();
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
        /// Saves the record.
        /// </summary>
        /// <returns>SaveRecord</returns>
        private bool SaveRecord()
        {
            try
            {
                if (this.CheckErrors())
                {
                    if (!this.GetTestResult(Convert.ToInt32(this.RollYearTextBox.Text), Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), this.FormulaTextBox.Text.Trim().ToLowerInvariant().Replace(keyId, formNo)).Equals("INVALID"))
                    {
                        this.form9013Control.WorkItem.F9013_UpdateNextNumberConfigDetails(this.nextNumberId, Convert.ToInt32(this.NextNumberTextBox.Text.Trim()), this.FormulaTextBox.Text.Trim(),TerraScanCommon.UserId);
                        ////this.LoadRollYearCombo();
                        this.LoadNextNumberDataGrid(this.NextNumRecordsGridView.CurrentCell.RowIndex);
                        this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        ////this.DisplayNextNumberHeaderDetails(this.NextNumRecordsGridView.CurrentCell.RowIndex);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.NextNumRecordsGridView.Focus();
                        this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                        this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                        this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                        //////this.NextNumRecordsGridView.CurrentRow.Selected = true;
                        //////this.NextNumRecordsGridView.Rows[this.NextNumRecordsGridView.CurrentCell.RowIndex].Selected = true;
                        //////this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, Convert.ToInt32(this.NextNumRecordsGridView.CurrentCell.RowIndex)];
                        this.NextNumRecordsGridView.TabStop = true;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidFormula"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                validationErrors = validationErrors + SharedFunctions.GetResourceString("F9013NextNumberTextBoxValidate");
            }

            if (string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()))
            {
                validationErrors = validationErrors + SharedFunctions.GetResourceString("F9013ForumulaTextBoxValidate");
            }

            if (!string.IsNullOrEmpty(validationErrors))
            {
               //// MessageBox.Show(validationErrors, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(SharedFunctions.GetResourceString("SaveParcelSaleDetails"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        /// Gets the test result.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="nextNum">The next number.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>testresult</returns>
        private string GetTestResult(int year, int nextNum, string formula)
        {
            string testresult = string.Empty;
            this.tempDataSet = this.form9013Control.WorkItem.F9013_CheckNextNumber(year, nextNum, formula);

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
        /// Loads the WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form9013Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form9013Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form9013Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
            }

            this.operationSmartPart.NewButtonVisible = false;
            this.operationSmartPart.DeleteButtonEnable = false;

            // To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
            if (this.form9013Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form9013Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form9013Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = this.AccessibleName;
            ////formLabelInfo[0] = "Next Number Configuration";
            ////formLabelInfo[0] = SharedFunctions.GetResourceString("F9013FormName");
            formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));

            // To Load FooterSmartPart into FooterWorkspace
            if (this.form9013Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form9013Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form9013Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form9013Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form9013Control.WorkItem;
            this.footerSmartPart.FormId = this.formNo;
            this.footerSmartPart.AuditLinkText = SharedFunctions.GetResourceString("F9013AuditLinkText");
            this.footerSmartPart.VisibleHelpButton = false;
            this.footerSmartPart.VisibleHelpLinkButton = true;
            this.footerSmartPart.TabStop = true;
        }

        /// <summary>
        /// Loads the roll year combo.
        /// </summary>
        private void LoadRollYearCombo()
        {
            this.nextNumberConfigDetails.ListNextNumberRollYear.Clear();
            this.nextNumberConfigDetails = this.form9013Control.WorkItem.F9013_ListNextNumberRollYear(this.userId);

            if (this.nextNumberConfigDetails.ListNextNumberRollYear.Rows.Count > 0)
            {
                this.RollYearCombo.DataSource = this.nextNumberConfigDetails.ListNextNumberRollYear;
                this.RollYearCombo.DisplayMember = this.nextNumberConfigDetails.ListNextNumberRollYear.RollYearColumn.ColumnName;
                this.RollYearCombo.ValueMember = this.nextNumberConfigDetails.ListNextNumberRollYear.IsDefaultColumn.ColumnName;
                DataRow[] tempRollYearValue = this.nextNumberConfigDetails.ListNextNumberRollYear.Select(this.rollYearIsDefault);

                if (tempRollYearValue.Length > 0)
                {
                    this.RollYearCombo.SelectedValue = 1;
                }

                int.TryParse(this.RollYearCombo.Text, out this.rollYear);
            }
        }

        /// <summary>
        /// Loads the next number data grid.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void LoadNextNumberDataGrid(int currentRowIndex)
        {      
            ////this.nextNumberConfigDetails.LisNextNumberConfiguration.Clear();
            this.nextNumberConfigDetails = this.form9013Control.WorkItem.F9013_ListNextNumberConfiguration(this.rollYear, this.userId);
            this.nextNumberRecordCount = this.nextNumberConfigDetails.LisNextNumberConfiguration.Rows.Count;

            this.NextNumRecordsGridView.DataSource = this.nextNumberConfigDetails.LisNextNumberConfiguration;

            if (this.nextNumberRecordCount > 0)
            {
                this.NextNumRecordsGridView.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.DisplayNextNumberHeaderDetails(currentRowIndex);
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                this.NextNumRecordsGridView.CurrentRow.Selected = true;
                this.NextNumRecordsGridView.Rows[currentRowIndex].Selected = true;
                this.NextNumRecordsGridView.CurrentCell = this.NextNumRecordsGridView[0, Convert.ToInt32(currentRowIndex)];
            }
            else
            {
                this.NextNumRecordsGridView.Enabled = false;
                this.NextNumRecordsGridView.Rows[0].Selected = false;
                this.NextNumberVerticalScroll.Visible = true;
                ////to disable the audit link when there is no record
                this.footerSmartPart.KeyId = null;
            }

            ////to enable or disable the vertical scroll bar
            if (this.nextNumberRecordCount > this.NextNumRecordsGridView.NumRowsVisible)
            {
                this.NextNumberVerticalScroll.Visible = false;
            }
            else
            {
                this.NextNumberVerticalScroll.Visible = true;
            }           
        }

        /// <summary>
        /// Custimizes the next number selection grid.
        /// </summary>
        private void CustimizeNextNumberSelectionGrid()
        {           
            this.NextNumRecordsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.NextNumRecordsGridView.Columns;

            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].DataPropertyName = this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName].DataPropertyName = this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName].DataPropertyName = this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.FormulaColumn.ColumnName].DataPropertyName = this.nextNumberConfigDetails.LisNextNumberConfiguration.FormulaColumn.ColumnName;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberTypeIDColumn.ColumnName].DataPropertyName = this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberTypeIDColumn.ColumnName;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.ApplicationIDColumn.ColumnName].DataPropertyName = this.nextNumberConfigDetails.LisNextNumberConfiguration.ApplicationIDColumn.ColumnName;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberIDColumn.ColumnName].DataPropertyName = this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberIDColumn.ColumnName;

            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].DisplayIndex = 0;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName].DisplayIndex = 1;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName].DisplayIndex = 2;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.FormulaColumn.ColumnName].DisplayIndex = 3;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberTypeIDColumn.ColumnName].DisplayIndex = 4;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.ApplicationIDColumn.ColumnName].DisplayIndex = 5;
            columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberIDColumn.ColumnName].DisplayIndex = 6;
            this.NextNumRecordsGridView.PrimaryKeyColumnName = this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberIDColumn.ColumnName;
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
                this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.NextNumRecordsGridView.Columns[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.NextNumRecordsGridView.TabStop = false;
            }
        }

        /// <summary>
        /// Displays the next number header details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void DisplayNextNumberHeaderDetails(int currentRowIndex)
        {  
            if (!string.IsNullOrEmpty(this.NextNumRecordsGridView.Rows[currentRowIndex].Cells[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].Value.ToString()) && this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["NextNumberID"].Value != null)
            {
                ////this.NextNumberIDTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["ID"].Value.ToString();
                this.DescriptionTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells[this.nextNumberConfigDetails.LisNextNumberConfiguration.DescriptionColumn.ColumnName].Value.ToString();
                this.RollYearTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells[this.nextNumberConfigDetails.LisNextNumberConfiguration.YearColumn.ColumnName].Value.ToString();
                this.NextNumberTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberColumn.ColumnName].Value.ToString();
                this.FormulaTextBox.Text = this.NextNumRecordsGridView.Rows[currentRowIndex].Cells[this.nextNumberConfigDetails.LisNextNumberConfiguration.FormulaColumn.ColumnName].Value.ToString();
                ////this.nextNumberId = Convert.ToInt32(this.NextNumRecordsGridView.Rows[currentRowIndex].Cells["NextNumberID"].Value.ToString());
                int.TryParse(this.NextNumRecordsGridView.Rows[currentRowIndex].Cells[this.nextNumberConfigDetails.LisNextNumberConfiguration.NextNumberIDColumn.ColumnName].Value.ToString(), out this.nextNumberId);
                this.footerSmartPart.KeyId = this.nextNumberId;
            }
        }

        #endregion Methods

        /// <summary>
        /// Handles the Leave event of the RollYearCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        
        private void RollYearCombo_Leave(object sender, EventArgs e)
        {
            ////Coding Added for the Issue 4006 by Malliga on 21/4/4009
            try
            {
                if (this.operationSmartPart.SaveButtonEnable)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (!this.SaveRecord())
                        {
                            this.NextNumberTextBox.Focus();
                            this.RollYearCombo.Text = this.rollYear.ToString();
                        }
                        else
                        {
                            int.TryParse(this.RollYearCombo.Text, out this.rollYear);
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            this.LoadNextNumberDataGrid(0);
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        int.TryParse(this.RollYearCombo.Text, out this.rollYear);
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.LoadNextNumberDataGrid(0);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        this.RollYearCombo.Text = this.rollYear.ToString();
                        return;
                        ////this.NextNumberTextBox.Focus();
                    }
                }
                else
                {
                    int.TryParse(this.RollYearCombo.Text, out this.rollYear);
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.LoadNextNumberDataGrid(0);
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}