//--------------------------------------------------------------------------------------------
// <copyright file="F1500.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1500 Account Management and Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-11-2006        Krishna Abburi      Created
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F1500 Class 
    /// </summary>
    [SmartPart]
    public partial class F1500 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// subFundManagementData
        /// </summary>
        private F9503SubFundManagementData subFundManagementData = new F9503SubFundManagementData();

        /// <summary>
        /// accountMangamentData Variable 
        /// </summary>
        private AccountManagementData getdescriptionDataset = new AccountManagementData();

        /// <summary>
        /// listAccountDetailsDataset Variable 
        /// </summary>
        private AccountManagementData listAccountDetailsDataset = new AccountManagementData();

        /// <summary>
        /// listItemsDataset Variable 
        /// </summary>
        private F1503GenericManagementData listItemsDataset = new F1503GenericManagementData();

        /// <summary>
        /// form1500Controll Variable 
        /// </summary>
        private F1500Controller form1500Controll;

        /// <summary>
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// statusBarSmartPart
        /// </summary>
        private StatusBarSmartPart statusBarSmartPart = new StatusBarSmartPart();

        /// <summary>
        /// operationSmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// institutionId variable is used to store institution id. - default value - null
        /// </summary>       
        private int accountId;

        /// <summary>
        /// currentRecord Local variable
        /// </summary>
        private int currentRecord;

        /// <summary>
        /// totalAccountIdsCount variable is used to find total number of AccountIds. 
        /// </summary>
        private int totalAccountIdsCount;

        /// <summary>
        /// recordPointerArray variable
        /// </summary>
        private int[] recordPointerArray = new int[2];

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// formload
        /// </summary>
        private bool formload = true;

        ///// <summary>
        ///// configurationValue
        ///// </summary>
        ////private bool configurationValue;

        /// <summary>
        /// rollYear
        /// </summary>
        private short rollYear;

        /// <summary>
        /// subFundId
        /// </summary>
        private int subFundId;

        /// <summary>
        /// configurationValue
        /// </summary>
        private bool textBoxEdited;

        /// <summary>
        /// accountsRecordCount
        /// </summary>
        private int accountsRecordCount;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1500"/> class.
        /// </summary>
        public F1500()
        {
            InitializeComponent();
            this.DepositHistoryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DepositHistoryPictureBox.Height, this.DepositHistoryPictureBox.Width, "Account Detail", 28, 81, 128);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Event Publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Event Publication for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event Publication for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event Publication for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Event Publication for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

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

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1500 controll.
        /// </summary>
        /// <value>The form1500 controll.</value>
        [CreateNew]
        public F1500Controller Form1500Controll
        {
            get { return this.form1500Controll as F1500Controller; }
            set { this.form1500Controll = value; }
        }

        ///// <summary>
        ///// Gets or sets the AccountId
        ///// </summary>
        ///// <value>The AccountId.</value>
        ////public int AccountId
        ////{
        ////    set { this.accountId = value; }
        ////    get { return this.accountId; }
        ////}

        ///// <summary>
        ///// Gets or sets the AccountValue
        ///// </summary>
        ///// <value>The accountValue.</value>
        ////public int AccountValue
        ////{
        ////    set { this.accountValue = value; }
        ////    get { return this.accountValue; }
        ////}

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the page status.
        /// </summary>
        /// <value>The page status.</value>
        private TerraScanCommon.PageStatus PageStatus
        {
            get { return this.pageStatus; }
            set { this.pageStatus = value; }
        }

        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        /// <value>The account id.</value>
        private int AccountId
        {
            get
            {
                return this.accountId;
            }

            set
            {
                this.accountId = value;
                this.additionalOperationSmartPart.KeyId = this.accountId;
            }
        }

        /// <summary>
        /// Gets or sets the sub fund id.
        /// </summary>
        /// <value>The sub fund id.</value>
        private int SubFundId
        {
            get
            {
                return this.subFundId;
            }

            set
            {
                this.subFundId = value;
            }
        }

        /// <summary>
        /// Gets or sets the roll year.
        /// </summary>
        /// <value>The roll year.</value>
        private short RollYear
        {
            get
            {
                return this.rollYear;
            }

            set
            {
                this.rollYear = value;
            }
        }

        #endregion

        #region EventSubcription

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.FillAccountDetails(recordNavigationEntity.RecordIndex);
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus(false))
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewButton_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
            }
        }

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
                this.form1500Controll.WorkItem.State["FormStatus"] = this.CheckPageStatus(false);
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

        #endregion

        #region FormEvents

        /// <summary>
        /// Handles the Load event of the F1500 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1500_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                this.IntializeCombo();
                this.FillAccountDetails(-1);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.RollYearTextBox.Select();
                this.SetAutoCompleteForSubFund();
                this.AutoCompleteFcBarLineOBj();
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
        /// Handles the LinkClicked event of the HelplinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.EnableControls(true);
            this.AccountInfoPanel.Enabled = true;
            this.ClearAccountDetails();
            this.GetYear();
            this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
            this.RollYearTextBox.Focus();
            this.AccountIDAuditlinkLabel.Enabled = false;
            this.AccountIDAuditlinkLabel.Text = "tTR_GLAccount [AccountID] " + "";
            this.PendingCombo.SelectedValue = 0;
            this.ActiveCombo.SelectedValue = 1;
            this.FunctionTypeCombo.SelectedValue = 2;
            this.CommentsdeckWorkspace.Enabled = false;
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            this.recordPointerArray[0] = 0;
            this.recordPointerArray[1] = 0;
            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            this.SaveAccountRecord(false);
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.FillAccountDetails(this.currentRecord);
            if (this.listAccountDetailsDataset.ListAccountIDs.Rows.Count > 0)
            {
                this.AccountIDAuditlinkLabel.Enabled = true;
            }
            else
            {
                this.AccountIDAuditlinkLabel.Enabled = false;
            }

            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            this.RollYearTextBox.Select();
        }

        /// <summary>
        /// Handles the Click event of the SubFundButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundButton_Click(object sender, EventArgs e)
        {
            try
            {
                int keyName = 0;
                Form subfundForm = new Form();
                short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                object[] optionalParameter = new object[] { this.rollYear };
                subfundForm = this.form1500Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1515, optionalParameter, this.form1500Controll.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.SubFundTextBox.Text = TerraScanCommon.GetValue(subfundForm, "SubFundItem").ToString();
                        this.SubFundTextBox.ForeColor = Color.Black;
                        if (!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()))
                        {
                            this.subFundManagementData = this.form1500Controll.WorkItem.F9503_GetSubFundItems(this.SubFundTextBox.Text.Trim(), this.rollYear);
                            if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                            {
                                this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                                this.subFundId = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                                if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "False")
                                {
                                    this.AccounTypeTextBox.Text = "Fund";
                                    this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                                }
                                else
                                {
                                    this.AccounTypeTextBox.Text = "Cash";
                                    this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                                }

                                if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "")
                                {
                                    this.AccounTypeTextBox.Text = string.Empty;
                                }
                            }
                            else
                            {
                                this.subFundId = 0;
                            }
                        }
                    }
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
        /// Handles the Click event of the FunctionButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FunctionButton_Click(object sender, EventArgs e)
        {
            try
            {
                string keyName = string.Empty;
                Form functionForm = new Form();
                object[] optionalParameter = new object[] { keyName };
                functionForm = this.form1500Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1502, null, this.form1500Controll.WorkItem);
                if (functionForm != null)
                {
                    if (functionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.FunctionTextBox.Text = TerraScanCommon.GetValue(functionForm, "FunctionIdValue").ToString();
                        this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetFunctionItems(this.FunctionTextBox.Text.Trim());
                        if (this.getdescriptionDataset.GetFunctionItems.Rows.Count > 0)
                        {
                            this.FunctionDescTextBox.Text = this.getdescriptionDataset.GetFunctionItems.Rows[0]["Description"].ToString();
                            this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);

                            if (this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"].ToString() != "")
                            {
                                this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);
                                this.FunctionTypeCombo.Enabled = false;
                            }

                            this.FunctionDescTextBox.ReadOnly = true;
                        }
                    }
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
        /// Handles the Leave event of the LineTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LineTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string lineId = string.Empty;
                lineId = this.LineTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(lineId))
                {
                    this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetDescription(lineId, "Line");
                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                    {
                        this.LineDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.LineDescTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.LineDescTextBox.Text = "";
                        this.LineDescTextBox.Enabled = true;
                        this.LineDescTextBox.LockKeyPress = false;
                    }
                }
                else
                {
                    this.LineDescTextBox.Text = string.Empty;
                    this.LineDescTextBox.LockKeyPress = true;
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
        /// Handles the Leave event of the ObjectTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ObjectTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string objectId = string.Empty;
                objectId = this.ObjectTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(objectId))
                {
                    this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetDescription(objectId, "Object");
                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                    {
                        this.ObjectDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.ObjectDescTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.ObjectDescTextBox.Text = "";
                        this.ObjectDescTextBox.Enabled = true;
                        this.ObjectDescTextBox.LockKeyPress = false;
                    }
                }
                else
                {
                    this.ObjectDescTextBox.Text = "";
                    this.ObjectDescTextBox.LockKeyPress = true;
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
        /// Handles the Leave event of the BarsTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BarsTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string barId = string.Empty;
                barId = this.BarsTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(barId))
                {
                    this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetDescription(barId, "Bar");
                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                    {
                        this.BarsDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.BarsDescTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.BarsDescTextBox.Text = string.Empty;
                        this.BarsDescTextBox.Enabled = true;
                        this.BarsDescTextBox.LockKeyPress = false;
                    }
                }
                else
                {
                    this.BarsDescTextBox.Text = "";
                    this.BarsDescTextBox.ReadOnly = true;
                    this.BarsDescTextBox.LockKeyPress = true;
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
        /// Handles the Leave event of the SubFundTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubFundTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.SubFundTextChanged();
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
        /// Handles the Leave event of the FunctionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FunctionTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.FunctionTextBoxChanged();
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
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                TerraScanCommon.ShowReport(150010, TerraScan.Common.Reports.Report.ReportType.Preview);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the GLConfigButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GLConfigButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(1501);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Calls to selection for bars lines and object buttons
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CallToSelection(object sender, EventArgs e)
        {
            string keyName = string.Empty;
            string keyID = string.Empty;
            Control tempControl = (Control)sender;
            switch (tempControl.Name.ToString())
            {
                case "BarsButton":
                    {
                        keyName = "Bars";
                        break;
                    }

                case "LineButton":
                    {
                        keyName = "Lines";
                        break;
                    }

                case "ObjectButton":
                    {
                        keyName = "Objects";
                        break;
                    }
            }

            try
            {
                Form activeWorkOrderSelectForm = new Form();
                object[] optionalParameter = new object[] { keyName };
                activeWorkOrderSelectForm = this.form1500Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1503, optionalParameter, this.form1500Controll.WorkItem);
                if (activeWorkOrderSelectForm != null)
                {
                    if (activeWorkOrderSelectForm.ShowDialog() == DialogResult.OK)
                    {
                        keyID = TerraScanCommon.GetValue(activeWorkOrderSelectForm, "ElementKeyId");

                        switch (tempControl.Name.ToString())
                        {
                            case "BarsButton":
                                {
                                    this.BarsTextBox.Text = keyID;
                                    if (!string.IsNullOrEmpty(this.BarsTextBox.Text.Trim()))
                                    {
                                        this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetDescription(keyID, "Bar");
                                        if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                                        {
                                            this.BarsDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                                            this.BarsDescTextBox.ReadOnly = true;
                                        }
                                    }

                                    break;
                                }

                            case "LineButton":
                                {
                                    this.LineTextBox.Text = keyID;
                                    if (!string.IsNullOrEmpty(this.LineTextBox.Text.Trim()))
                                    {
                                        this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetDescription(keyID, "Line");
                                        if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                                        {
                                            this.LineDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                                            this.LineDescTextBox.ReadOnly = true;
                                        }
                                    }

                                    break;
                                }

                            case "ObjectButton":
                                {
                                    this.ObjectTextBox.Text = keyID;
                                    if (!string.IsNullOrEmpty(this.ObjectTextBox.Text.Trim()))
                                    {
                                        this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetDescription(keyID, "Object");
                                        if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                                        {
                                            this.ObjectDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                                            this.ObjectDescTextBox.ReadOnly = true;
                                        }
                                    }

                                    break;
                                }
                        }
                    }
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
        /// Handles the LinkClicked event of the AccountIDAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AccountIDAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int auditLinkKeyID = 0;
                int.TryParse(this.AccountIDTextBox.Text, out auditLinkKeyID);

                if (auditLinkKeyID > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = auditLinkKeyID;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////int reportAuditId = 0;
                ////this.Cursor = Cursors.WaitCursor;
                ////reportAuditId = Convert.ToInt32(this.AccountIDTextBox.Text);
                ////this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("ReportFileID", reportAuditId);

                //////// Shows the report form.
                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(150090, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
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
        /// Sets the color of combo box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SetColorOfComboBox(object sender, EventArgs e)
        {
            try
            {
                this.SetColorOfComboBox();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(RollYearTextBox.NumericTextBoxValue < 1900 || RollYearTextBox.NumericTextBoxValue > 2079))
                {
                    if (!this.formload)
                    {
                        if (this.pageMode != TerraScanCommon.PageModeTypes.New)
                        {
                            this.SeteditrProcess();
                        }

                        this.SubFundTextChanged();
                    }

                    this.SetColorOfComboBox();
                    this.SetAutoCompleteForSubFund();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if ((RollYearTextBox.NumericTextBoxValue < 1900 || RollYearTextBox.NumericTextBoxValue > 2079))
                {
                    this.GetYear();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            // LoadAccountActionButtons SmartPart into AccountsActionButtonsdeckWorkspace
            if (this.form1500Controll.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.TestOperationSmartPartdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart));
            }
            else
            {
                this.TestOperationSmartPartdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart));
            }

            this.operationSmartPart = (OperationSmartPart)this.form1500Controll.WorkItem.SmartParts[SmartPartNames.OperationSmartPart];

            // Load RecordNavigatorSmartPart into RecordNavigatorSmartPartdeckWorkspace
            if (this.form1500Controll.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            // To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
            if (this.form1500Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form1500Controll.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form1500Controll.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }
            ////Load StatusBarSmartPart to StatusBarSmartPartdeckWorkspace
            if (this.form1500Controll.WorkItem.SmartParts.Contains(SmartPartNames.StatusBarSmartPart))
            {
                this.statusBarSmartPart = (StatusBarSmartPart)this.form1500Controll.WorkItem.SmartParts.Get(SmartPartNames.StatusBarSmartPart);
            }
            else
            {
                this.statusBarSmartPart = this.form1500Controll.WorkItem.SmartParts.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart);
            }

            this.StatusBarDeckWorkspace.Show(this.statusBarSmartPart);

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = "Account Management"; ////Properties.Resources.FormName;
            formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));

            this.operationSmartPart.DeleteButtonVisible = false;
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1500Controll.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1500Controll.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
            this.statusBarSmartPart.VisibleDelinquentButton = false;
            this.statusBarSmartPart.VisibleAutoPrintButton = false;
            this.statusBarSmartPart.EnableFilteredButton = false;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void EnableControls(bool enableValue)
        {
            //// Account Info EnableControls
            this.AccountIDTextBox.Enabled = true;
            this.AccounTypeTextBox.Enabled = enableValue;
            this.AccounTypeTextBox.ReadOnly = true;

            this.RollYearTextBox.Enabled = enableValue;
            this.PendingCombo.Enabled = enableValue;
            this.ActiveCombo.Enabled = enableValue;
            this.AccountNoTextBox.Enabled = true;
            this.DescriptionTextBox.Enabled = enableValue;

            this.DisableButtonBasedOnConfigValues();
            ////if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            ////{
            ////    this.FunctionTypeCombo.Enabled = true;
            ////}
            ////else
            ////{
            ////    this.FunctionTypeCombo.Enabled = false;
            ////}

            this.FunctionTypeCombo.Enabled = false;
            this.SubFundTextBox.ForeColor = Color.Black;
            this.SubFundDescTextBox.LockKeyPress = true;
            this.FunctionDescTextBox.LockKeyPress = true;
            this.BarsDescTextBox.LockKeyPress = true;
            this.ObjectDescTextBox.LockKeyPress = true;
            this.LineDescTextBox.LockKeyPress = true;
            if (string.Equals(this.AccounTypeTextBox.Text, "Fund"))
            {
                this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            }

            if (string.Equals(this.AccounTypeTextBox.Text, "Cash"))
            {
                this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
        }

        /// <summary>
        /// Clears the account details.
        /// </summary>
        private void ClearAccountDetails()
        {
            this.AccountIDTextBox.Text = string.Empty;
            this.AccounTypeTextBox.Text = string.Empty;

            this.RollYearTextBox.Text = string.Empty;
            this.PendingCombo.Text = string.Empty;
            this.ActiveCombo.Text = string.Empty;
            this.AccountNoTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;

            this.SubFundTextBox.Text = string.Empty;
            this.FunctionTextBox.Text = string.Empty;
            this.BarsTextBox.Text = string.Empty;
            this.ObjectTextBox.Text = string.Empty;
            this.LineTextBox.Text = string.Empty;

            this.SubFundDescTextBox.Text = string.Empty;
            this.FunctionDescTextBox.Text = string.Empty;
            this.BarsDescTextBox.Text = string.Empty;
            this.ObjectDescTextBox.Text = string.Empty;
            this.LineDescTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>true or false</returns>
        private bool CheckPageStatus(bool onclose)
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    bool status = this.SaveAccountRecord(onclose);

                    if (status)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }

                    return status;
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (onclose)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else
                    {
                        this.CancelButton_Click();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Saves the account record.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>true or false </returns>
        private bool SaveAccountRecord(bool onclose)
        {
            ////this.DisableButtonBasedOnConfigValues();
            #region Checking for Required Fields

            AccountManagementData saveAccountData = new AccountManagementData();
            AccountManagementData.ListAccountDetailsRow dr = saveAccountData.ListAccountDetails.NewListAccountDetailsRow();
            saveAccountData.ListAccountDetails.Clear();
            short tempRollYear;
            Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
            if (tempRollYear < 1900 || tempRollYear > 2079)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear") + " \n", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.RollYearTextBox.Focus();
                return false;
            }
            else
            {
                dr.RollYear = tempRollYear;
            }

            if ((string.IsNullOrEmpty(this.SubFundTextBox.Text.ToString().Trim())))
            {
                DialogResult dialogResult = MessageBox.Show("The current General Ledger Account cannot be saved because it is missing required values.", "TerraScan T2 - Missing Required Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.OK)
                {
                    this.SubFundTextBox.Focus();
                    return false;
                }
            }

            if (this.SubFundId == 0)
            {
                DialogResult dialogResult = MessageBox.Show("A General Ledger Account cannot be created without a valid SubFund.", "TerraScan T2 - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.OK)
                {
                    this.SubFundTextBox.Focus();
                    return false;
                }
            }

            if ((string.IsNullOrEmpty(this.FunctionTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.BarsTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.ObjectTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.LineTextBox.Text.ToString().Trim())))
            {
                DialogResult dialogResult = MessageBox.Show("The current General Ledger Account cannot be saved because it is missing required values.", "TerraScan T2 - Missing Required Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dialogResult == DialogResult.OK)
                {
                    this.FunctionTextBox.Focus();
                    return false;
                }
            }

            #endregion

            int errorStatus;
            this.Cursor = Cursors.WaitCursor;
            string validationErrors = string.Empty;
            bool yearValidationErrors = false;
            int tempAccountID = 0;
            ////AccountManagementData saveAccountData = new AccountManagementData();
            ////AccountManagementData.ListAccountDetailsRow dr = saveAccountData.ListAccountDetails.NewListAccountDetailsRow();
            ////saveAccountData.ListAccountDetails.Clear();

            //// tempAccountID = Convert.ToInt32(this.AccountIDTextBox.Text.ToString().Trim());
            if (!tempAccountID.Equals(0))
            {
                dr.AccountID = Convert.ToInt32(this.AccountIDTextBox.Text.Trim());
            }

            ////short tempRollYear;
            ////Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
            ////if (tempRollYear < 1900 || tempRollYear > 2079)
            ////{
            ////    MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear") + " \n", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////    this.RollYearTextBox.Focus();
            ////    return false;
            ////}
            ////else
            ////{
            ////    dr.RollYear = tempRollYear;
            ////}

            if (!string.IsNullOrEmpty(this.PendingCombo.Text.Trim()))
            {
                dr.IsPending = Convert.ToBoolean(Convert.ToInt16(this.PendingCombo.SelectedValue.ToString()));
            }
            else
            {
                validationErrors = validationErrors + SharedFunctions.GetResourceString("1500MissingReqIsPending") + " \n";
            }

            if (!string.IsNullOrEmpty(this.ActiveCombo.Text.Trim()))
            {
                dr.IsActive = Convert.ToBoolean(Convert.ToInt16(this.ActiveCombo.SelectedValue.ToString()));
            }
            else
            {
                validationErrors = validationErrors + SharedFunctions.GetResourceString("1500MissingReqIsActive") + " \n";
            }

            dr.AccountName = (this.AccountNoTextBox.Text.Trim());
            dr.AcctDesc = (this.DescriptionTextBox.Text.Trim());

            if (!string.IsNullOrEmpty(this.AccounTypeTextBox.Text.Trim()))
            {
                if (this.AccounTypeTextBox.Text.Trim() == "Fund")
                {
                    dr.IsCash = false;
                }
                else
                {
                    dr.IsCash = true;
                }
            }

            if (!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()))
            {
                dr.SubFund = this.SubFundTextBox.Text.Trim();
                dr.SubFundID = this.SubFundId;
            }
            else
            {
                validationErrors = validationErrors + SharedFunctions.GetResourceString("1500MissingReqSubFund") + " \n";
            }

            dr.SubFundDesc = this.SubFundDescTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(this.FunctionTypeCombo.Text))
            {
                dr.SemiAnnualCode = Convert.ToByte(this.FunctionTypeCombo.SelectedValue);
            }

            dr.FunctionID = this.FunctionTextBox.Text.Trim();
            dr.FunctionDesc = this.FunctionDescTextBox.Text.Trim();
            dr.BarID = this.BarsTextBox.Text.Trim();
            dr.BarsDesc = this.BarsDescTextBox.Text;
            dr.ObjectID = this.ObjectTextBox.Text.Trim();
            dr.ObjectDesc = this.ObjectDescTextBox.Text.Trim();
            dr.LineID = this.LineTextBox.Text.Trim();
            dr.LineDesc = this.LineDescTextBox.Text.Trim();
            if (string.IsNullOrEmpty(validationErrors.Trim()))
            {
                saveAccountData.ListAccountDetails.Rows.Add(dr);
                DataSet tempDataSet = new DataSet("Root");

                tempDataSet.Tables.Add(saveAccountData.ListAccountDetails.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                string tempxml = string.Empty;
                tempxml = tempDataSet.GetXml();
                if (string.IsNullOrEmpty(this.AccountIDTextBox.Text.Trim()))
                {
                    errorStatus = this.form1500Controll.WorkItem.F1500_CreateOrEditAccount(0, tempxml, TerraScanCommon.UserId);
                }
                else
                {
                    errorStatus = this.form1500Controll.WorkItem.F1500_CreateOrEditAccount(Convert.ToInt32(this.AccountIDTextBox.Text), tempDataSet.GetXml(), TerraScanCommon.UserId);
                }

                switch (errorStatus)
                {
                    case -1:
                        {
                            DialogResult dialogResult = MessageBox.Show("The Current General Ledger Account cannot be created because it has been associated with a SubFund for a different Roll Year.", "TerraScan T2 - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (dialogResult == DialogResult.OK)
                            {
                                return false;
                            }

                            break;
                        }

                    case -2:
                        {
                            DialogResult dialogResult = MessageBox.Show("The General Ledger Account cannot be saved because another Account Record already Exists with these values.", "TerraScan T2 - Invalid Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (dialogResult == DialogResult.OK)
                            {
                                this.RollYearTextBox.Focus();
                                return false;
                            }

                            break;
                        }

                    default:
                        {
                            this.GetAccountDetails(errorStatus);
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("AccountManagementSaveMissingField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (onclose)
            {
                return true;
            }

            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                this.FillAccountDetails(this.currentRecord);
            }
            else
            {
                this.FillAccountDetails(-1);
            }

            this.SetAutoCompleteForSubFund();
            this.AutoCompleteFcBarLineOBj();
            this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.RollYearTextBox.Focus();
            return true;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Loads the account id.
        /// </summary>
        private void LoadAccountId()
        {
            ////clears record set
            this.listAccountDetailsDataset.ListAccountIDs.Clear();
            ////load form record set     ---- check it once 
            int accountid = 0;
            if (accountid == 0)
            {
                accountid = -999;
            }

            this.listAccountDetailsDataset = this.form1500Controll.WorkItem.F1500_ListAccountDetails(accountid);
            this.accountsRecordCount = this.listAccountDetailsDataset.ListAccountIDs.Rows.Count;
            if
                (this.accountsRecordCount == 0)
            {
                this.SetDefaultState();
            }
        }

        /// <summary>
        /// Retrieves the account id.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>account id</returns>
        private int RetrieveAccountId(int index)
        {
            int tempAccountId = 0;
            if (this.listAccountDetailsDataset.Tables.Count > 0)
            {
                if (index > 0)
                {
                    if (index <= this.listAccountDetailsDataset.ListAccountIDs.Rows.Count)
                    {
                        if (this.listAccountDetailsDataset.ListAccountIDs.Rows[index - 1][0].ToString() != null)
                        {
                            tempAccountId = int.Parse(this.listAccountDetailsDataset.ListAccountIDs.Rows[index - 1][0].ToString());
                        }
                    }
                }
                else
                {
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.listAccountDetailsDataset.ListAccountIDs.Rows.Count));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.listAccountDetailsDataset.ListAccountIDs.Rows.Count));
                    this.recordPointerArray[0] = this.listAccountDetailsDataset.ListAccountIDs.Rows.Count;
                    this.recordPointerArray[1] = this.listAccountDetailsDataset.ListAccountIDs.Rows.Count;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    tempAccountId = int.Parse(this.listAccountDetailsDataset.ListAccountIDs.Rows[this.listAccountDetailsDataset.ListAccountIDs.Rows.Count - 1][0].ToString()); ////toverify
                }

                return tempAccountId;
            }

            return tempAccountId;
        }

        /// <summary>
        /// Fills the account details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void FillAccountDetails(int currentRowIndex)
        {
            ////refresh form record set
            this.LoadAccountId();

            if (this.accountsRecordCount > 0)
            {
                if (currentRowIndex > 0)
                {
                    this.currentRecord = currentRowIndex;
                }
                else
                {
                    this.currentRecord = this.listAccountDetailsDataset.ListAccountIDs.Rows.Count;
                }

                this.accountId = Convert.ToInt32(this.listAccountDetailsDataset.ListAccountIDs[this.currentRecord - 1][0].ToString());

                this.totalAccountIdsCount = this.listAccountDetailsDataset.ListAccountIDs.Rows.Count;
                if (currentRowIndex > this.totalAccountIdsCount)
                {
                    currentRowIndex = this.totalAccountIdsCount;
                }

                if (this.totalAccountIdsCount > 0)
                {
                    this.EnableControls(true);
                    /////Reports control enabled 
                    this.RecordNavigatorSmartPartdeckWorkspace.Enabled = true;
                    this.SetActiveRecord(this, new DataEventArgs<int>(currentRowIndex));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalAccountIdsCount));
                    this.recordPointerArray[0] = currentRowIndex;
                    this.recordPointerArray[1] = this.totalAccountIdsCount;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    this.GetAccountDetails(this.RetrieveAccountId(currentRowIndex));
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.SetAttachmentCommentsCount();
                }
                else
                {
                    this.ClearAccountDetails();
                    this.EnableControls(false);
                    this.AccountIDAuditlinkLabel.Enabled = false;
                    this.AccountInfoPanel.Enabled = false;
                    this.NullRecords = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }
            }
        }

        /// <summary>
        /// Gets the Account details.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        private void GetAccountDetails(int accountId)
        {
            this.formload = true;
            this.listAccountDetailsDataset = this.form1500Controll.WorkItem.F1500_ListAccountDetails(accountId);
            if (this.listAccountDetailsDataset != null)
            {
                if (this.listAccountDetailsDataset.Tables.Count > 0)
                {
                    if (this.listAccountDetailsDataset.ListAccountDetails.Rows.Count > 0)
                    {
                        //// Fill District Info
                        this.AccountIDTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["AccountID"].ToString();

                        this.RollYearTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["RollYear"].ToString();

                        if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsPending"].ToString() != null)
                        {
                            this.PendingCombo.SelectedValue = Convert.ToInt16(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsPending"]);
                        }

                        if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsActive"].ToString() != null)
                        {
                            this.ActiveCombo.SelectedValue = Convert.ToInt16(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsActive"]);
                        }

                        this.AccountNoTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["AccountName"].ToString();
                        this.DescriptionTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["AcctDesc"].ToString();
                        if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["IsCash"].ToString().Trim() == "False")
                        {
                            this.AccounTypeTextBox.Text = "Fund";
                            this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                        }
                        else
                        {
                            this.AccounTypeTextBox.Text = "Cash";
                            this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                        }

                        if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SemiAnnualCode"].ToString() != "")
                        {
                            this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SemiAnnualCode"]);
                        }

                        this.SubFundTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFund"].ToString();
                        if (this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFundId"].ToString() != null)
                        {
                            this.SubFundId = Convert.ToInt32(this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFundId"].ToString());
                        }

                        this.SubFundDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["SubFundDesc"].ToString();
                        this.FunctionTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["FunctionID"].ToString();
                        this.FunctionDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["FunctionDesc"].ToString();
                        this.BarsTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["BarID"].ToString();
                        this.BarsDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["BarsDesc"].ToString();
                        this.LineTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["LineID"].ToString();
                        this.LineDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["LineDesc"].ToString();
                        this.ObjectTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["ObjectID"].ToString();
                        this.ObjectDescTextBox.Text = this.listAccountDetailsDataset.ListAccountDetails.Rows[0]["ObjectDesc"].ToString();
                        ////audit link
                        this.AccountIDAuditlinkLabel.Text = "tTR_GLAccount [AccountID] " + this.AccountIDTextBox.Text;
                        this.AccountIDAuditlinkLabel.Enabled = true;
                        ////attachment and comments
                        this.CommentsdeckWorkspace.Enabled = true;
                        this.AccountInfoPanel.Enabled = true;
                        bool pedning = Convert.ToBoolean(this.listAccountDetailsDataset.ListAccountDetails.Rows[0][listAccountDetailsDataset.ListAccountDetails.IsPendingColumn].ToString());
                        this.formload = false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
            CommentsData getYearDataSet = new CommentsData();
            getYearDataSet = this.form1500Controll.WorkItem.GetConfigDetails("TR_RollYear");
            this.RollYearTextBox.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

            if (this.AccountId != -999)
            {
                this.additionalOperationSmartPart.KeyId = this.AccountId;
                additionalOperationCountEntity.AttachmentCount = this.form1500Controll.WorkItem.GetAttachmentCount(this.ParentFormId, this.AccountId, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1500Controll.WorkItem.GetCommentsCount(this.ParentFormId, this.AccountId, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }

            this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
        }

        /// <summary>
        /// Intializes the combo.
        /// </summary>
        private void IntializeCombo()
        {
            ////customize active combobox
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValueUpperCase();
            this.PendingCombo.DataSource = commonData.ComboBoxDataTable;
            this.PendingCombo.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.PendingCombo.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
            CommonData commonData2 = new CommonData();
            commonData2.LoadYesNoValueUpperCase();
            this.ActiveCombo.DataSource = commonData2.ComboBoxDataTable;
            this.ActiveCombo.ValueMember = commonData2.ComboBoxDataTable.KeyIdColumn.ToString();
            this.ActiveCombo.DisplayMember = commonData2.ComboBoxDataTable.KeyNameColumn.ToString();
            ////which loads Balancing,Collection,Disbursement  value to the ComboBoxDataTable
            DataTable workTable = new DataTable("FunctionType");
            DataColumn workCol = workTable.Columns.Add("No", typeof(Int32));
            DataColumn workCol2 = workTable.Columns.Add("Name", typeof(String));
            workCol.AllowDBNull = false;
            workCol.Unique = true;
            workTable.Rows.Add(new Object[] { 1, "Balancing" });
            workTable.Rows.Add(new Object[] { 2, "Collection" });
            workTable.Rows.Add(new Object[] { 3, "Disbursement" });
            this.FunctionTypeCombo.DataSource = workTable;
            this.FunctionTypeCombo.ValueMember = workTable.Columns[0].ToString();
            this.FunctionTypeCombo.DisplayMember = workTable.Columns[1].ToString();
            FunctionTypeCombo.SelectedValue = 2;
        }

        /// <summary>
        /// Gets the config value.
        /// </summary>
        /// <param name="configurationObjectName">Name of the configuration object.</param>
        /// <returns>config value</returns>
        private bool GetConfigValue(string configurationObjectName)
        {
            this.getdescriptionDataset.GetConfiguration.Clear();
            this.getdescriptionDataset.Merge(this.form1500Controll.WorkItem.F1500_GetConfigurationValue(configurationObjectName));
            if (this.getdescriptionDataset.GetConfiguration.Rows.Count > 0)
            {
                return Convert.ToBoolean(this.getdescriptionDataset.GetConfiguration[0][this.getdescriptionDataset.GetConfiguration.ConfigurationValueColumn.ColumnName].ToString());
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Disables the button based on config values.
        /// </summary>
        private void DisableButtonBasedOnConfigValues()
        {
            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountFunctionsEnabled")))
            {
                this.FunctionButton.Enabled = true;
                this.FunctionTextBox.Enabled = true;
                this.FunctionDescTextBox.Enabled = true;
            }
            else
            {
                this.FunctionButton.Enabled = false;
                this.FunctionTextBox.Enabled = false;
                this.FunctionDescTextBox.Enabled = false;
            }

            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountBarsEnabled")))
            {
                this.BarsButton.Enabled = true;
                this.BarsTextBox.Enabled = true;
                this.BarsDescTextBox.Enabled = true;
            }
            else
            {
                this.BarsButton.Enabled = false;
                this.BarsTextBox.Enabled = false;
                this.BarsDescTextBox.Enabled = false;
            }

            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountObjectsEnabled")))
            {
                this.ObjectButton.Enabled = true;
                this.ObjectTextBox.Enabled = true;
                this.ObjectDescTextBox.Enabled = true;
            }
            else
            {
                this.ObjectButton.Enabled = false;
                this.ObjectTextBox.Enabled = false;
                this.ObjectDescTextBox.Enabled = false;
            }

            if (Convert.ToBoolean(this.GetConfigValue("TR_AccountLinesEnabled")))
            {
                this.LineButton.Enabled = true;
                this.LineTextBox.Enabled = true;
                this.LineDescTextBox.Enabled = true;
            }
            else
            {
                this.LineButton.Enabled = false;
                this.LineTextBox.Enabled = false;
                this.LineDescTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// Sets the state of the default.
        /// </summary>
        private void SetDefaultState()
        {
            this.ClearAccountDetails();
            this.CommentsdeckWorkspace.Enabled = false;
            this.AccountIDAuditlinkLabel.Enabled = false;
            this.AccountInfoPanel.Enabled = false;
            this.RecordNavigatorSmartPartdeckWorkspace.Enabled = false;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
        }

        /// <summary>
        /// Sets the color of combo box.
        /// </summary>
        private void SetColorOfComboBox()
        {
            if (this.PendingCombo.Text == "NO")
            {
                this.PendingCombo.ForeColor = Color.Black;
            }
            else
            {
                this.PendingCombo.ForeColor = Color.Red;
            }

            if (this.ActiveCombo.Text == "NO")
            {
                this.ActiveCombo.ForeColor = Color.Red;
            }
            else
            {
                this.ActiveCombo.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Subs the fund text changed.
        /// </summary>
        private void SubFundTextChanged()
        {
            string keyId = string.Empty;
            keyId = this.SubFundTextBox.Text;
            if (!string.IsNullOrEmpty(keyId))
            {
                short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);

                this.subFundManagementData = this.form1500Controll.WorkItem.F9503_GetSubFundItems(keyId, this.rollYear);
                if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                {
                    this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                    this.subFundId = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"]);
                    this.SubFundTextBox.ForeColor = Color.Black;
                    if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "False")
                    {
                        this.AccounTypeTextBox.Text = "Fund";
                        this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
                    }
                    else
                    {
                        this.AccounTypeTextBox.Text = "Cash";
                        this.AccounTypeTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                    }

                    if (this.subFundManagementData.getSubFundItems.Rows[0]["IsCash"].ToString().Trim() == "")
                    {
                        this.AccounTypeTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    this.SubFundTextBox.ForeColor = Color.DarkRed;
                    this.SubFundDescTextBox.Text = string.Empty;
                    this.AccounTypeTextBox.Text = string.Empty;
                    this.subFundId = 0;
                }
            }
            else
            {
                this.SubFundTextBox.ForeColor = Color.DarkRed;
                this.SubFundDescTextBox.Text = string.Empty;
                this.AccounTypeTextBox.Text = string.Empty;
                this.SubFundDescTextBox.LockKeyPress = true;
                this.subFundId = 0;
            }
        }

        /// <summary>
        /// Functions the text box changed.
        /// </summary>
        private void FunctionTextBoxChanged()
        {
            string functionId = string.Empty;
            functionId = this.FunctionTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(functionId))
            {
                this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetFunctionItems(functionId);
                if (this.getdescriptionDataset.GetFunctionItems.Rows.Count > 0)
                {
                    this.FunctionDescTextBox.Text = this.getdescriptionDataset.GetFunctionItems.Rows[0]["Description"].ToString();

                    if (this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"].ToString() != "")
                    {
                        this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);
                        this.FunctionTypeCombo.Enabled = false;
                    }

                    this.FunctionDescTextBox.LockKeyPress = true;
                }
                else
                {
                    this.FunctionDescTextBox.Text = string.Empty;
                    this.FunctionDescTextBox.Enabled = true;
                    this.FunctionDescTextBox.LockKeyPress = false;
                    this.FunctionTypeCombo.Enabled = true;
                    this.FunctionTypeCombo.SelectedValue = 2;
                }
            }
            else
            {
                this.FunctionDescTextBox.Text = "";
                this.FunctionDescTextBox.LockKeyPress = true;
                this.FunctionTypeCombo.SelectedValue = 2;

                if (this.pageMode != TerraScanCommon.PageModeTypes.New)
                {
                    this.FunctionTypeCombo.Enabled = false;
                }
                else
                {
                    this.FunctionTypeCombo.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Fields the edit process.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FieldEditProcess(object sender, EventArgs e)
        {
            if (!this.formload)
            {
                if (this.pageMode != TerraScanCommon.PageModeTypes.New)
                {
                    this.SeteditrProcess();
                }
            }

            this.SetColorOfComboBox();
        }

        /// <summary>
        /// Seteditrs the process.
        /// </summary>
        private void SeteditrProcess()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
        }

        /// <summary>
        /// Sets the auto complere.
        /// </summary>
        private void SetAutoCompleteForSubFund()
        {
            AutoCompleteStringCollection sourceSubFund = new AutoCompleteStringCollection();

            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text))
            {
                this.rollYear = Convert.ToInt16(this.RollYearTextBox.Text.Trim());
                if (this.rollYear != 0)
                {
                    this.subFundManagementData = this.form1500Controll.WorkItem.F9503_GetSubFundItems(null, this.rollYear);
                    if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                    {
                        this.AssignAutoCompletSouce(this.subFundManagementData.getSubFundItems.Rows, this.subFundManagementData.getSubFundItems.SubFundColumn.ColumnName, this.SubFundTextBox, sourceSubFund);
                    }
                    else
                    {
                        this.SubFundTextBox.AutoCompleteCustomSource = null;
                    }
                }
            }
        }

        /// <summary>
        /// Autoes the complete function , bar, lineand Obj.
        /// </summary>
        private void AutoCompleteFcBarLineOBj()
        {
            AutoCompleteStringCollection sourceFunction = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceBar = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceLine = new AutoCompleteStringCollection();
            AutoCompleteStringCollection sourceObject = new AutoCompleteStringCollection();

            F1503GenericManagementData barsList = new F1503GenericManagementData();
            F1503GenericManagementData objectsList = new F1503GenericManagementData();
            F1503GenericManagementData lineLists = new F1503GenericManagementData();

            lineLists = this.form1500Controll.WorkItem.F1500_GetGenericElementMgmt(null, null, "Objects");
            if (lineLists.GetGenericElementMgmt.Rows.Count > 0)
            {
                this.AssignAutoCompletSouce(lineLists.GetGenericElementMgmt.Rows, lineLists.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.ObjectTextBox, sourceObject);
            }
            else
            {
                this.ObjectTextBox.AutoCompleteCustomSource = null;
            }

            this.getdescriptionDataset = this.form1500Controll.WorkItem.F1500_GetFunctionItems(null);
            if (this.getdescriptionDataset.GetFunctionItems.Rows.Count > 0)
            {
                this.AssignAutoCompletSouce(this.getdescriptionDataset.GetFunctionItems.Rows, this.getdescriptionDataset.GetFunctionItems.FunctionValueColumn.ColumnName, this.FunctionTextBox, sourceFunction);
            }
            else
            {
                this.FunctionTextBox.AutoCompleteCustomSource = null;
            }

            barsList = this.form1500Controll.WorkItem.F1500_GetGenericElementMgmt(null, null, "Bars");
            if (barsList.GetGenericElementMgmt.Rows.Count > 0)
            {
                this.AssignAutoCompletSouce(barsList.GetGenericElementMgmt.Rows, barsList.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.BarsTextBox, sourceBar);
            }
            else
            {
                this.BarsTextBox.AutoCompleteCustomSource = null;
            }

            objectsList = this.form1500Controll.WorkItem.F1500_GetGenericElementMgmt(null, null, "Lines");
            if (objectsList.GetGenericElementMgmt.Count > 0)
            {
                this.AssignAutoCompletSouce(objectsList.GetGenericElementMgmt.Rows, objectsList.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.LineTextBox, sourceLine);
            }
            else
            {
                this.LineTextBox.AutoCompleteCustomSource = null;
            }
        }

        /// <summary>
        /// Assigns the auto complet souce.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="textBox">The text box.</param>
        /// <param name="sourceCollection">The source collection.</param>
        private void AssignAutoCompletSouce(DataRowCollection dataRow, string columnName, TerraScanTextBox textBox, AutoCompleteStringCollection sourceCollection)
        {
            for (int count = 0; count < dataRow.Count; count++)
            {
                sourceCollection.Add(dataRow[count][columnName].ToString());
            }

            textBox.AutoCompleteCustomSource = sourceCollection;
        }

        #endregion
    }
}
