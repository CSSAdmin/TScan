//--------------------------------------------------------------------------------------------
// <copyright file="F1011.cs" company="Congruent">
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
// 
//*********************************************************************************/

namespace D1010
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.IO;
    using System.Collections.Specialized;
    //// using TerraScan.UI.BusinessEntities;
    using TerraScan.Common.Reports;
    using System.Globalization;
    using System.Configuration;
    using System.Reflection;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Form 1011 class
    /// </summary>
    [SmartPart]
    public partial class F1011 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// controller F1011
        /// </summary>
        private F1011Controller form1011Control;

        /// <summary>
        /// templateIds variable is used to store list of templateIds for Mortgage Import Template. 
        /// </summary>       
        private MortgageImpotTemplateData templateIds;

        /// <summary>
        /// templateIds variable is used to store list of templateIds for Mortgage Import Template. 
        /// </summary>       
        private MortgageImpotTemplateData mortageTemplateDataSet;

        /// <summary>
        /// InputFileDataTable variable is used to store list of InputFile details for Mortgage Import Template. 
        /// </summary> 
        private DataTable inputFileDataTable;

        /// <summary>
        /// totalTemplateCount variable is used to find total number of templates for mortgage import template. 
        /// </summary>
        private int totalTemplateCount;

        /// <summary>
        /// current Record
        /// </summary>
        private int currentRecord;

        /// <summary>
        /// record PointerArray
        /// </summary>
        private int[] recordPointerArray;

        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] formLabelInfo;

        /// <summary>
        /// makeButtonEnable
        /// </summary>
        private string[] makeButtonEnable;

        /// <summary>
        /// permissionForButton
        /// </summary>
        private int[] permissionForButton;

        /// <summary>
        /// pageLoadStatus Local variable.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// saveComplete Local variable.
        /// </summary>
        private bool saveComplete;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScan.Common.TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// SmartPart instance for accessing the properties
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MortgageImportTemplateForm"/> class.
        /// </summary>
        public F1011()
        {
            this.InitializeComponent();
            this.templateIds = new MortgageImpotTemplateData();
            this.mortageTemplateDataSet = new MortgageImpotTemplateData();
            this.inputFileDataTable = new DataTable();
            this.recordPointerArray = new int[2];
            this.formLabelInfo = new string[2];
            this.makeButtonEnable = new string[2];
            this.permissionForButton = new int[4];
            this.pageLoadStatus = false;
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Template Header", 28, 81, 128);
            this.InputFileDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(265, 36, "Input File Detail", 174, 150, 94);
        }
        #endregion

        #region EventsPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// event publication for GetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// event publication for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// event publication for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event Publication for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion EventsPublication

        #region Properties

        /// <summary>
        /// For F1011Control
        /// </summary>
        [CreateNew]
        public F1011Controller Form1011Control
        {
            get { return this.form1011Control as F1011Controller; }
            set { this.form1011Control = value; }
        }

        #endregion Properties

        #region EventsSubscription

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus())
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.FillMortgageTemplateDetails(recordNavigationEntity.RecordIndex);
        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form1011Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
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
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
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
                    this.NewTemplateButton_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
                case "DELETE":
                    this.DeleteButton_Click();
                    break;
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
                TerraScanCommon.ShowAuditReport("TemplateID", this.TemplateIDTextBox.Text);
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

        #endregion EventsSubscription

        #region TemplateDetails

        /// <summary>
        /// retrieves the current statementId
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>integer</returns>
        private int RetrieveTemplateId(int index)
        {
            int tempMortageTemplateID = 0;
            if (this.templateIds.ListMortgageImportTemplate != null)
            {
                if (this.currentRecord <= this.templateIds.ListMortgageImportTemplate.Rows.Count)
                {
                    if (this.templateIds.ListMortgageImportTemplate.Rows[index - 1][0].ToString() != null)
                    {
                        tempMortageTemplateID = int.Parse(this.templateIds.ListMortgageImportTemplate.Rows[index - 1][0].ToString());
                    }
                }
                else
                {
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.templateIds.ListMortgageImportTemplate.Rows.Count));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.templateIds.ListMortgageImportTemplate.Rows.Count));
                }
            }

            return tempMortageTemplateID;
        }

        /// <summary>
        /// Gets the mortgage template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        private void GetMortgageTemplateDetails(int templateId)
        {
            try
            {
                this.mortageTemplateDataSet = F1011WorkItem.GetMortgageTemplate(templateId);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            try
            {
                if (this.mortageTemplateDataSet.GetMortgageImportTemplate != null)
                {
                    if (this.mortageTemplateDataSet.GetMortgageImportTemplate.Rows.Count > 0)
                    {
                        this.TemplateIDTextBox.Text = this.mortageTemplateDataSet.GetMortgageImportTemplate.Rows[0][this.mortageTemplateDataSet.GetMortgageImportTemplate.TemplateIDColumn].ToString();
                        this.DescriptionTextBox.Text = this.mortageTemplateDataSet.GetMortgageImportTemplate.Rows[0][this.mortageTemplateDataSet.GetMortgageImportTemplate.DescriptionColumn].ToString();
                        this.FilePathTextBox.Text = this.mortageTemplateDataSet.GetMortgageImportTemplate.Rows[0][this.mortageTemplateDataSet.GetMortgageImportTemplate.FilePathColumn].ToString();
                        this.TemplateNameTextBox.Text = this.mortageTemplateDataSet.GetMortgageImportTemplate.Rows[0][this.mortageTemplateDataSet.GetMortgageImportTemplate.TemplateNameColumn].ToString();
                        this.ImportTypeComboBox.SelectedValue = this.mortageTemplateDataSet.GetMortgageImportTemplate.Rows[0][this.mortageTemplateDataSet.GetMortgageImportTemplate.TypeIDColumn].ToString();
                        ////this.TemplateAuditlinkLabel.Text = SharedFunctions.GetResourceString("ImportTemplateAuditLink") + this.TemplateIDTextBox.Text;
                        this.footerSmartPart.KeyId = Convert.ToInt32(this.TemplateIDTextBox.Text);
                        //  this.CreateInputFile(this.mortageTemplateDataSet.GetMortgageImportTemplate);
                        this.DisplayInputFileDetails(this.mortageTemplateDataSet.GetMortgageImportTemplate);
                    }
                    else
                    {
                        MessageBox.Show("Record has been deleted by some other user", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ////MessageBox.Show("Record has been deleted by some other user");
                        ////this.ClearTemplateDetails();
                        this.FillMortgageTemplateDetails(this.currentRecord);
                    }
                }
                else
                {
                    this.ClearTemplateDetails();
                }
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Fills the mortgage template details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void FillMortgageTemplateDetails(int currentRowIndex)
        {
            try
            {
                this.templateIds = this.Form1011Control.WorkItem.ListMortgageTemplate;
                this.ParentForm.Activate();
                if (this.templateIds.ListMortgageImportTemplate != null)
                {
                    this.totalTemplateCount = this.templateIds.ListMortgageImportTemplate.Rows.Count;

                    if (this.totalTemplateCount <= 0)
                    {
                        this.NullRecords = true;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        this.EmailButton.Enabled = false;
                        this.PrintButton.Enabled = false;
                        this.PreviewButton.Enabled = false;
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            ////this.ImportTypeComboBox.Focus();
                            this.ParentForm.ActiveControl = this.ImportTypeComboBox;
                            this.ParentForm.ActiveControl.Focus();
                            this.ImportTypeComboBox.Select();
                        }
                        else
                        {
                            this.footerSmartPart.Controls[0].TabStop = true;
                            this.footerSmartPart.Controls[0].Focus();
                        }
                    }
                    else
                    {
                        this.EmailButton.Enabled = true;
                        this.PrintButton.Enabled = true;
                        this.PreviewButton.Enabled = true;
                        ////this.ImportTypeComboBox.Focus();
                        this.ParentForm.ActiveControl = this.ImportTypeComboBox;
                        this.ParentForm.ActiveControl.Focus();
                        this.ImportTypeComboBox.Select();
                    }

                    if (currentRowIndex > this.totalTemplateCount)
                    {
                        currentRowIndex = this.totalTemplateCount;
                    }

                    if (this.totalTemplateCount > 0)
                    {
                        this.LockControls(true && this.EditPermissionButton.ActualPermission);
                        this.LockTextBoxControls(false || !this.EditPermissionButton.ActualPermission);
                        if (currentRowIndex == 0)
                        {
                            this.currentRecord = this.totalTemplateCount;
                        }
                        else
                        {
                            this.currentRecord = currentRowIndex;
                        }

                        this.SetActiveRecord(this, new DataEventArgs<int>(this.currentRecord));
                        this.SetRecordCount(this, new DataEventArgs<int>(this.totalTemplateCount));
                        this.recordPointerArray[0] = this.currentRecord;
                        this.recordPointerArray[1] = this.totalTemplateCount;
                        this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                        this.GetMortgageTemplateDetails(this.RetrieveTemplateId(this.currentRecord));
                        this.TextboxEnabled(true);
                    }
                    else
                    {
                        this.ClearTemplateDetails();
                        this.LockControls(false);
                        this.LockTextBoxControls(true);
                        this.NullRecords = true;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        this.EmailButton.Enabled = false;
                        this.PrintButton.Enabled = false;
                        this.PreviewButton.Enabled = false;
                        this.TextboxEnabled(false);
                    }
                }
                else
                {
                    this.ClearTemplateDetails();
                    this.LockControls(false);
                    this.LockTextBoxControls(true);
                    this.NullRecords = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    this.EmailButton.Enabled = false;
                    this.PrintButton.Enabled = false;
                    this.PreviewButton.Enabled = false;
                    this.TextboxEnabled(false);
                }

                this.ParentForm.Activate();
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Method Will Enable/Disable the Text Boxes
        /// </summary>
        /// <param name="enable">Enable/Disabled</param>
        private void TextboxEnabled(bool enable)
        {
            this.DescriptionTextBox.Enabled = enable;
            this.FilePathTextBox.Enabled = enable;
            this.TemplateNameTextBox.Enabled = enable;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void LockControls(bool enableValue)
        {
            this.ImportTypeComboBox.Enabled = enableValue;
            this.FilePathButton.Enabled = enableValue;
            this.TopPanel.Enabled = enableValue;
            this.MortgageTemplateControlPanel.Enabled = enableValue;
            this.TemplateIDTextBox.Enabled = enableValue;
        }

        /// <summary>
        /// Locks the text box controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockTextBoxControls(bool lockControl)
        {
            this.DescriptionTextBox.LockKeyPress = lockControl;
            this.FilePathTextBox.LockKeyPress = lockControl;
            this.TemplateNameTextBox.LockKeyPress = lockControl;
        }

        /// <summary>
        /// Gets the type of the mortgage import file.
        /// </summary>
        private void GetMortgageImportFileType()
        {
            try
            {
                MortageImportData mortgageImportFileTypeDataSet = this.Form1011Control.WorkItem.ListMortgageImportFileType();
                this.ImportTypeComboBox.ValueMember = "TypeID";
                this.ImportTypeComboBox.DisplayMember = "TypeName";
                this.ImportTypeComboBox.DataSource = mortgageImportFileTypeDataSet.ListMortgageImportFileType;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Input File Details

        /// <summary>
        /// Creates the input file.
        /// </summary>
        /// <param name="mortgageDatatable">The mortgage datatable.</param>
        private void CreateInputFile(DataTable mortgageDatatable)
        {
            try
            {
                for (int colCount = this.inputFileDataTable.Columns.Count - 1; colCount >= 0; colCount--)
                {
                    this.inputFileDataTable.Columns.RemoveAt(colCount);
                }

                this.inputFileDataTable.Clear();

                this.inputFileDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("FieldName"), new DataColumn("Position"), new DataColumn("Width") });
                if (mortgageDatatable != null)
                {
                    if (mortgageDatatable.Rows.Count > 0)
                    {
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileStatementID"), mortgageDatatable.Rows[0]["StatementID_Pos"].ToString(), mortgageDatatable.Rows[0]["StatementID_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileStatementNum"), mortgageDatatable.Rows[0]["StatementNum_Pos"].ToString(), mortgageDatatable.Rows[0]["StatementNum_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileParcelNum"), mortgageDatatable.Rows[0]["ParcelNum_Pos"].ToString(), mortgageDatatable.Rows[0]["ParcelNum_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFilePostType"), mortgageDatatable.Rows[0]["PostType_Pos"].ToString(), mortgageDatatable.Rows[0]["PostType_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFilePaymentAmount"), mortgageDatatable.Rows[0]["Amount_Pos"].ToString(), mortgageDatatable.Rows[0]["Amount_wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileComment"), mortgageDatatable.Rows[0]["Comment_Pos"].ToString(), mortgageDatatable.Rows[0]["Comment_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileBankCode"), mortgageDatatable.Rows[0]["BankCode_pos"].ToString(), mortgageDatatable.Rows[0]["BankCode_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileLoanNumber"), mortgageDatatable.Rows[0]["LoanNum_Pos"].ToString(), mortgageDatatable.Rows[0]["LoanNum_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileTaxpayerName"), mortgageDatatable.Rows[0]["TaxPayName_Pos"].ToString(), mortgageDatatable.Rows[0]["TaxPayName_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileOwnerID"), mortgageDatatable.Rows[0]["OwnerID_Pos"].ToString(), mortgageDatatable.Rows[0]["OwnerID_Wid"].ToString() });
                        //For implementing CartId
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileCartID"), mortgageDatatable.Rows[0]["CartID_Pos"].ToString(), mortgageDatatable.Rows[0]["CartID_Wid"].ToString() });
                    }
                    else
                    {
                        this.CreateEmptyStructure();
                    }
                }
                else
                {
                    this.CreateEmptyStructure();
                }

                this.InputFileGridView.DataSource = this.inputFileDataTable;
                this.SetDefaultProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Creates the empty structure.
        /// </summary>
        private void CreateEmptyStructure()
        {
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileStatementID"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileStatementNum"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileParcelNum"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFilePostType"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFilePaymentAmount"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileComment"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileBankCode"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileLoanNumber"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileTaxpayerName"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileOwnerID"), string.Empty, string.Empty });
            //For implementing CartId
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("MIInputFileCartID"), string.Empty, string.Empty });

        }

        /// <summary>
        /// Displays the input file details.
        /// </summary>
        /// <param name="mortgageDatatable">The mortgage datatable.</param>
        private void DisplayInputFileDetails(MortgageImpotTemplateData.GetMortgageImportTemplateDataTable mortgageDatatable)
        {
            try
            {
                if (mortgageDatatable != null)
                {
                    if (mortgageDatatable.Rows.Count > 0)
                    {
                        this.inputFileDataTable.Rows[0][1] = mortgageDatatable.Rows[0][mortgageDatatable.StatementID_PosColumn].ToString();
                        this.inputFileDataTable.Rows[0][2] = mortgageDatatable.Rows[0][mortgageDatatable.StatementID_WidColumn].ToString();
                        this.inputFileDataTable.Rows[1][1] = mortgageDatatable.Rows[0][mortgageDatatable.StatementNum_PosColumn].ToString();
                        this.inputFileDataTable.Rows[1][2] = mortgageDatatable.Rows[0][mortgageDatatable.StatementNum_WidColumn].ToString();

                        ////Parcel Number,PostType
                        this.inputFileDataTable.Rows[2][1] = mortgageDatatable.Rows[0][mortgageDatatable.ParcelNumber_PosColumn].ToString();
                        this.inputFileDataTable.Rows[2][2] = mortgageDatatable.Rows[0][mortgageDatatable.ParcelNumber_WidColumn].ToString();
                        this.inputFileDataTable.Rows[3][1] = mortgageDatatable.Rows[0][mortgageDatatable.PostType_PosColumn].ToString();
                        this.inputFileDataTable.Rows[3][2] = mortgageDatatable.Rows[0][mortgageDatatable.PostType_WidColumn].ToString();

                        this.inputFileDataTable.Rows[4][1] = mortgageDatatable.Rows[0][mortgageDatatable.Amount_PosColumn].ToString();
                        this.inputFileDataTable.Rows[4][2] = mortgageDatatable.Rows[0][mortgageDatatable.Amount_widColumn].ToString();
                        this.inputFileDataTable.Rows[5][1] = mortgageDatatable.Rows[0][mortgageDatatable.Comment_PosColumn].ToString();
                        this.inputFileDataTable.Rows[5][2] = mortgageDatatable.Rows[0][mortgageDatatable.Comment_WidColumn].ToString();
                        this.inputFileDataTable.Rows[6][1] = mortgageDatatable.Rows[0][mortgageDatatable.BankCode_posColumn].ToString();
                        this.inputFileDataTable.Rows[6][2] = mortgageDatatable.Rows[0][mortgageDatatable.BankCode_WidColumn].ToString();
                        this.inputFileDataTable.Rows[7][1] = mortgageDatatable.Rows[0][mortgageDatatable.LoanNum_PosColumn].ToString();
                        this.inputFileDataTable.Rows[7][2] = mortgageDatatable.Rows[0][mortgageDatatable.LoanNum_WidColumn].ToString();
                        this.inputFileDataTable.Rows[8][1] = mortgageDatatable.Rows[0][mortgageDatatable.TaxPayName_PosColumn].ToString();
                        this.inputFileDataTable.Rows[8][2] = mortgageDatatable.Rows[0][mortgageDatatable.TaxPayName_WidColumn].ToString();

                        ////Coding Added for the CO : 6498 by malliga
                        this.inputFileDataTable.Rows[9][1] = mortgageDatatable.Rows[0][mortgageDatatable.OwnerID_PosColumn].ToString();
                        this.inputFileDataTable.Rows[9][2] = mortgageDatatable.Rows[0][mortgageDatatable.OwnerID_WidColumn].ToString();

                        ////Coding Added for the CO : 21053 by purushotham
                        this.inputFileDataTable.Rows[10][1] = mortgageDatatable.Rows[0][mortgageDatatable.CartID_PosColumn].ToString();
                        this.inputFileDataTable.Rows[10][2] = mortgageDatatable.Rows[0][mortgageDatatable.CartID_WidColumn].ToString();


                    }
                    else
                    {
                        this.PopulateEmptyFile();
                    }
                }
                else
                {
                    this.PopulateEmptyFile();
                }

                this.InputFileGridView.DataSource = this.inputFileDataTable;
                this.SetDefaultProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the default property.
        /// </summary>
        private void SetDefaultProperty()
        {
            this.InputFileGridView.Columns[0].DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 153);
            this.InputFileGridView.Columns[0].ReadOnly = true;
            this.InputFileGridView.Columns[0].HeaderText = "Field Name";
            this.InputFileGridView.Columns[0].Width = 176;
            this.InputFileGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.InputFileGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.InputFileGridView.Columns[0].DefaultCellStyle.SelectionForeColor = Color.FromArgb(51, 51, 153);
            this.InputFileGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.InputFileGridView.Columns[1].Frozen = true;
            this.InputFileGridView.Columns[2].Frozen = true;
            this.InputFileGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.InputFileGridView.Columns[1].Resizable = DataGridViewTriState.False;
            this.InputFileGridView.Columns[2].Resizable = DataGridViewTriState.False;
            ////this.InputFileGridView.Columns[1].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 121);
            ////this.InputFileGridView.Columns[2].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 121);
        }

        /// <summary>
        /// Populates the empty file.
        /// </summary>
        private void PopulateEmptyFile()
        {
            this.inputFileDataTable.Rows[0][1] = string.Empty;
            this.inputFileDataTable.Rows[0][2] = string.Empty;
            this.inputFileDataTable.Rows[1][1] = string.Empty;
            this.inputFileDataTable.Rows[1][2] = string.Empty;
            this.inputFileDataTable.Rows[2][1] = string.Empty;
            this.inputFileDataTable.Rows[2][2] = string.Empty;
            this.inputFileDataTable.Rows[3][1] = string.Empty;
            this.inputFileDataTable.Rows[3][2] = string.Empty;
            this.inputFileDataTable.Rows[4][1] = string.Empty;
            this.inputFileDataTable.Rows[4][2] = string.Empty;
            this.inputFileDataTable.Rows[5][1] = string.Empty;
            this.inputFileDataTable.Rows[5][2] = string.Empty;
            this.inputFileDataTable.Rows[6][1] = string.Empty;
            this.inputFileDataTable.Rows[6][2] = string.Empty;
            this.inputFileDataTable.Rows[7][1] = string.Empty;
            this.inputFileDataTable.Rows[7][2] = string.Empty;
            this.inputFileDataTable.Rows[8][1] = string.Empty;
            this.inputFileDataTable.Rows[8][2] = string.Empty;
            this.inputFileDataTable.Rows[9][1] = string.Empty;
            this.inputFileDataTable.Rows[9][2] = string.Empty;

            this.inputFileDataTable.Rows[10][1] = string.Empty;
            this.inputFileDataTable.Rows[10][2] = string.Empty;

        }
        #endregion

        #region UserDefinedFunction

        /// <summary>
        /// Clears the mortgage import template details.
        /// </summary>
        private void ClearTemplateDetails()
        {
            this.TemplateIDTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.FilePathTextBox.Text = string.Empty;
            this.TemplateNameTextBox.Text = string.Empty;
            ////this.TemplateAuditlinkLabel.Text = SharedFunctions.GetResourceString("ImportTemplateAuditLink");
            ////this.TemplateAuditlinkLabel.Enabled = false;
            this.currentRecord = 0;
            this.totalTemplateCount = 0;
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            this.recordPointerArray[0] = 0;
            this.recordPointerArray[1] = 0;
            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
            this.footerSmartPart.KeyId = null;
            this.CreateInputFile(null);
        }
        #endregion

        #region Record Navigation

        /// <summary>
        /// Displays the last record in the gridview.
        /// </summary>
        private void LastRecordDisplay()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!this.CheckPageStatus())
                {
                    return;
                }

                this.totalTemplateCount = this.templateIds.ListMortgageImportTemplate.Rows.Count;
                if (this.totalTemplateCount >= 1)
                {
                    this.currentRecord = this.totalTemplateCount;
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.totalTemplateCount));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalTemplateCount));
                    this.GetMortgageTemplateDetails(this.RetrieveTemplateId(this.currentRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }

                this.recordPointerArray[0] = this.currentRecord;
                this.recordPointerArray[1] = this.totalTemplateCount;
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
            }
            catch (FormatException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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

        /// <summary>
        /// Handles the Load event of the MortgageImportTemplateForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1011_Load(object sender, EventArgs e)
        {
            ////todo: this.CancelButton = this.CancelTemplateButton;
            this.ParentForm.Activate();
            this.EditPermissionButton.ActualPermission = this.PermissionFiled.editPermission;
            this.LoadWorkSpaces();
            this.pageLoadStatus = false;
            this.CreateInputFile(null);
                this.GetMortgageImportFileType();
            this.FillMortgageTemplateDetails(0);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            //// this.ParentForm.ActiveControl = ImportTypeComboBox;
            //// this.ParentForm.ActiveControl.Focus();
            //// this.ParentForm.Activate();
            ////this.ImportTypeComboBox.Select();
            ////ImportTypeComboBox.Focus();

            ////todo: this.NewTemplateButton.TabStop =false;
            this.InputFileDetailPictureBox.SendToBack();
            if (this.ImportTypeComboBox.SelectedValue != null)
            {
                if (this.InputFileGridView.Columns.Count >= 2)
                {
                    if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                    {
                        this.InputFileGridView.Columns[2].Visible = true;
                        this.InputFileGridView.Width = 378;
                        this.MortgageTemplateControlPanel.Width = 480;
                        this.InputFileDetailPictureBox.Location = new System.Drawing.Point(370, 0);
                    }
                    else if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                    {
                        this.InputFileGridView.Columns[2].Visible = false;
                        this.MortgageTemplateControlPanel.Width = 315;
                        this.InputFileGridView.Width = 276;
                        this.InputFileDetailPictureBox.Location = new System.Drawing.Point(268, 0);
                        this.InputFileDetailPictureBox.Size = new System.Drawing.Size(36, 265);
                    }
                }
            }
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            try
            {
                if (this.form1011Control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
                {
                    this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1011Control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
                }
                else
                {
                    this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1011Control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
                }

                if (this.form1011Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form1011Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form1011Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                if (this.form1011Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
                {
                    this.operationSmartPart = (OperationSmartPart)this.form1011Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                    this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
                }
                else
                {
                    this.operationSmartPart = (OperationSmartPart)this.form1011Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(this.Name + SmartPartNames.OperationSmartPart);
                    this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
                }

                //// To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
                if (this.form1011Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
                {
                    this.FooterWorkspace.Show(this.form1011Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
                }
                else
                {
                    this.FooterWorkspace.Show(this.form1011Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
                }

                this.footerSmartPart = (FooterSmartPart)this.form1011Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
                this.footerSmartPart.ParentWorkItem = this.form1011Control.WorkItem;
                this.footerSmartPart.FormId = "1011";
                this.footerSmartPart.AuditLinkText = SharedFunctions.GetResourceString("ImportTemplateAuditLink");
                this.footerSmartPart.VisibleHelpButton = false;
                this.footerSmartPart.VisibleHelpLinkButton = true;

                foreach (UserControl ctrl in this.FooterWorkspace.SmartParts)
                {
                    if (ctrl != null)
                    {
                        ctrl.TabStop = true;
                    }
                }

                this.RecordNavigatorSmartPartdeckWorkspace.TabStop = false;

                foreach (UserControl ctrl in this.RecordNavigatorSmartPartdeckWorkspace.SmartParts)
                {
                    if (ctrl != null)
                    {
                        ctrl.TabStop = false;
                    }
                }

                this.formHeaderSmartPartdeckWorkspace.TabStop = false;

                foreach (UserControl ctrl in this.formHeaderSmartPartdeckWorkspace.SmartParts)
                {
                    if (ctrl != null)
                    {
                        ctrl.TabStop = false;
                    }
                }

                this.operationSmartPartWorkSpace.TabStop = false;

                foreach (UserControl ctrl in this.operationSmartPartWorkSpace.SmartParts)
                {
                    if (ctrl != null)
                    {
                        ctrl.TabStop = false;
                    }
                }

                this.operationSmartPart.NewButtonText = "New Template";
                this.formLabelInfo[0] = Properties.Resources.TemplateName;
                this.formLabelInfo[1] = string.Empty;

                this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NewTemplateButton control.
        /// </summary>
        private void NewTemplateButton_Click()
        {
            try
                {
                this.ParentForm.Activate();
                this.Cursor = Cursors.WaitCursor;
                this.pageLoadStatus = false;
                this.TopPanel.Focus();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.LockControls(true && Convert.ToBoolean(this.permissionForButton[2]));
                this.LockControls(true);
                this.TextboxEnabled(true);
                this.LockTextBoxControls(false);
                this.ClearTemplateDetails();
                    this.GetMortgageImportFileType();
                this.DescriptionTextBox.Enabled = true;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);

                //// this.DescriptionTextBox.Focus();
                this.ParentForm.ActiveControl = ImportTypeComboBox;
                this.ParentForm.ActiveControl.Focus();
                this.pageLoadStatus = true;
                this.ParentForm.Activate();
                ////this.ActiveControl.Focus();
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        private void SaveButton_Click()
        {
            this.SaveMortageImport();
        }

        /// <summary>
        /// Saves the mortage import.
        /// </summary>
        /// <returns>SaveMortageImport</returns>
        private bool SaveMortageImport()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.pageLoadStatus = false;
                string validationErrors = string.Empty;
                int tempTemlateID = 0;
                int tempImportType = 0;
                int tempStatementIdPos = 0;
                int tempStatementIdWid = 0;
                int tempStatementNumPos = 0;
                int tempStatementNumWid = 0;
                int tempAmountPos = 0;
                int tempAmountWid = 0;
                int tempCommentPos = 0;
                int tempCommentWid = 0;
                int tempBankCodePos = 0;
                int tempBankCodeWid = 0;
                int tempLoanNumPos = 0;
                int tempLoanNumWid = 0;
                int tempTaxPayNamePos = 0;
                int tempTaxPayNameWid = 0;
                int tempParcelNumberPos = 0;
                int tempParcelNumberWid = 0;
                int tempPostTypePos = 0;
                int tempPostTypeWid = 0;
                int tempOwnerIdPos = 0;
                int tempOwnerIdWid = 0;
                int tempCartPos = 0;
                int tempCartWid = 0;

                string fileError = string.Empty;

                fileError = this.CheckFileExist();

                if (!string.IsNullOrEmpty(fileError))
                {
                    validationErrors = validationErrors + "You can not save this form because it is missing the following required values \n" + fileError;
                }

                if (string.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
                    {
                        this.TemplateNameTextBox.Focus();
                    }

                    validationErrors = validationErrors + "Template Name should be entered. \n";
                }

                if (!string.IsNullOrEmpty(this.TemplateIDTextBox.Text))
                {
                    tempTemlateID = Convert.ToInt32(this.TemplateIDTextBox.Text);
                }

                if (!string.IsNullOrEmpty(this.ImportTypeComboBox.SelectedValue.ToString()))
                {
                    tempImportType = Convert.ToInt32(this.ImportTypeComboBox.SelectedValue.ToString());
                }
                else
                {
                    validationErrors = validationErrors + "Import Type should be entered. \n";
                }
                ////Commented by Biju on 11/May/2010 to implement #6564
                ////if ((string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim())
                ////|| string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0"))
                ////&& (string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim())
                ////|| string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0")))
                ////{
                ////    validationErrors = validationErrors + "Enter position for StatementID or Statement Number. \n";
                ////}
                ////till here
                //// Position Is Mandatory

                //For Checking Nulls for StatmentID,Statement Number,CartId,Amount
         
                if (!string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[10]["position"].ToString().Trim(), "0")

                    || !string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0")

                    || !string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0")

                    || !string.IsNullOrEmpty(this.inputFileDataTable.Rows[4]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[4]["position"].ToString().Trim(), "0"))
                {

                    
                    if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0")
                    && string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0"))
                    {
                        if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["position"].ToString().Trim()))
                        {
                            validationErrors = validationErrors + "Cart ID position is mandatory. \n";
                        }
                    } 
                }
                else
                {
                    validationErrors = "Following fields are required to save this record,\n Statement ID \n OR \n Statement Number \n OR \n Cart ID and Payment Amount \n";

                }

                //Amount Field is mandatory
                if (!string.IsNullOrEmpty(this.inputFileDataTable.Rows[4]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[4]["position"].ToString().Trim(), "0"))
                {
                    if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                    {
                        if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[4]["width"].ToString().Trim()) || string.Equals(this.inputFileDataTable.Rows[4]["width"].ToString().Trim(), "0"))
                        {
                            validationErrors = validationErrors + "Payment Amount Width should be entered. \n";
                        }
                    }
                }
                else
                {
                    validationErrors = validationErrors + "Payment Amount position is mandatory. \n";
                }

                //
                if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0")
                    && string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0") )
                {
                    if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["position"].ToString().Trim()))
                    {
                        validationErrors = validationErrors + "Cart ID position is mandatory. \n";
                    }
                }

                //Width Validation
                if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                {
                    if (!string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[10]["width"].ToString().Trim(), "0")

                   || !string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["width"].ToString().Trim(), "0")

                   || !string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["width"].ToString().Trim(), "0")

                   || !string.IsNullOrEmpty(this.inputFileDataTable.Rows[4]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[4]["width"].ToString().Trim(), "0"))
                    {

                        if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["width"].ToString().Trim(), "0")
                        && string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["width"].ToString().Trim(), "0"))
                        {
                            if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["width"].ToString().Trim()))
                            {
                                validationErrors = validationErrors + "Cart ID width is mandatory. \n";
                            }
                        }
                    }
                    else
                    {
                        validationErrors = "Following fields are required to save this record,\n Statement ID \n OR \n Statement Number \n OR \n Cart ID and Payment Amount \n";

                    }                   

                    //
                    //if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0")
                    //    && string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0"))
                    //{
                    //    if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["position"].ToString().Trim()))
                    //    {
                    //        validationErrors = validationErrors + "Cart ID position is mandatory. \n";
                    //    }
                    //}
                }

                

                // Commented to Change the Required field Validation
                //if (!string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[10]["position"].ToString().Trim(), "0"))
                //{
                //    if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                //    {
                //        if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["width"].ToString().Trim()) || string.Equals(this.inputFileDataTable.Rows[10]["width"].ToString().Trim(), "0"))
                //        {
                //            validationErrors = validationErrors + "Cart ID Width should be entered. \n";
                //        }
                //    }
                //}
                //else
                //{
                //    if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim())
                //   || string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0")
                //   || string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim())
                //   || string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0"))
                //    {
                //        //Validation message need to be changed
                //        validationErrors = validationErrors + "Cart ID position is mandatory. \n";
                //    }
                //}

                //if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[10]["width"].ToString().Trim()) || string.Equals(this.inputFileDataTable.Rows[10]["width"].ToString().Trim(), "0"))
                //{

                //}


                //Commented by purushotham on 23-07-2015
                //////Added by Biju on 11/May/2010 to implement #6564. See the text assigned to validationErrors for the functionality.
                //if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim())
                //    || string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0"))
                //{
                //    if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim())
                //    || string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0"))
                //    {
                //        if ((string.IsNullOrEmpty(this.inputFileDataTable.Rows[2]["position"].ToString().Trim())
                //        || string.Equals(this.inputFileDataTable.Rows[2]["position"].ToString().Trim(), "0"))
                //            )
                //        {
                //            if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[9]["position"].ToString().Trim())
                //            || string.Equals(this.inputFileDataTable.Rows[9]["position"].ToString().Trim(), "0"))
                //            {
                //                validationErrors = "Following fields are required to save this record,\n Statement ID \n OR \n Statement Number \n OR \n Parcel Number and Post Type \n OR \n Owner ID \n";
                //            }
                //        }
                //        else if ((string.IsNullOrEmpty(this.inputFileDataTable.Rows[3]["position"].ToString().Trim())
                //        || string.Equals(this.inputFileDataTable.Rows[3]["position"].ToString().Trim(), "0")))
                //        {
                //            validationErrors = "Following fields are required to save this record,\n Statement ID \n OR \n Statement Number \n OR \n Parcel Number and Post Type \n OR \n Owner ID \n";
                //        }

                //    }
                //}
                ////till here
                validationErrors = validationErrors + this.CheckErrors();

                if (string.IsNullOrEmpty(validationErrors.Trim()))
                {
                    int.TryParse(this.inputFileDataTable.Rows[0]["position"].ToString(), out tempStatementIdPos);
                    int.TryParse(this.inputFileDataTable.Rows[0]["width"].ToString(), out tempStatementIdWid);
                    int.TryParse(this.inputFileDataTable.Rows[1]["position"].ToString(), out tempStatementNumPos);
                    int.TryParse(this.inputFileDataTable.Rows[1]["width"].ToString(), out tempStatementNumWid);

                    int.TryParse(this.inputFileDataTable.Rows[2]["position"].ToString(), out tempParcelNumberPos);
                    int.TryParse(this.inputFileDataTable.Rows[2]["width"].ToString(), out tempParcelNumberWid);
                    int.TryParse(this.inputFileDataTable.Rows[3]["position"].ToString(), out tempPostTypePos);
                    int.TryParse(this.inputFileDataTable.Rows[3]["width"].ToString(), out tempPostTypeWid);

                    int.TryParse(this.inputFileDataTable.Rows[4]["position"].ToString(), out tempAmountPos);
                    int.TryParse(this.inputFileDataTable.Rows[4]["width"].ToString(), out tempAmountWid);
                    int.TryParse(this.inputFileDataTable.Rows[5]["position"].ToString(), out tempCommentPos);
                    int.TryParse(this.inputFileDataTable.Rows[5]["width"].ToString(), out tempCommentWid);
                    int.TryParse(this.inputFileDataTable.Rows[6]["position"].ToString(), out tempBankCodePos);
                    int.TryParse(this.inputFileDataTable.Rows[6]["width"].ToString(), out tempBankCodeWid);
                    int.TryParse(this.inputFileDataTable.Rows[7]["position"].ToString(), out tempLoanNumPos);
                    int.TryParse(this.inputFileDataTable.Rows[7]["width"].ToString(), out tempLoanNumWid);
                    int.TryParse(this.inputFileDataTable.Rows[8]["position"].ToString(), out tempTaxPayNamePos);
                    int.TryParse(this.inputFileDataTable.Rows[8]["width"].ToString(), out tempTaxPayNameWid);
                    ////Coding Added for the co 6498 by malliga
                    int.TryParse(this.inputFileDataTable.Rows[9]["position"].ToString(), out tempOwnerIdPos);
                    int.TryParse(this.inputFileDataTable.Rows[9]["width"].ToString(), out tempOwnerIdWid);

                    int.TryParse(this.inputFileDataTable.Rows[10]["position"].ToString(), out tempCartPos);
                    int.TryParse(this.inputFileDataTable.Rows[10]["width"].ToString(), out tempCartWid);

                    if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                    {
                        ////WSHelper.SaveMortgageImportTemplate(tempTemlateID, this.TemplateNameTextBox.Text, tempImportType, TerraScanCommon.UserId, this.DescriptionTextBox.Text, this.FilePathTextBox.Text, tempStatementIdPos, 0, tempStatementNumPos, 0, tempAmountPos, 0, tempCommentPos, 0, tempBankCodePos, 0, tempLoanNumPos, 0, tempTaxPayNamePos, 0);
                        F1011WorkItem.SaveMortgageImportTemplate(tempTemlateID, this.TemplateNameTextBox.Text, tempImportType, TerraScanCommon.UserId, this.DescriptionTextBox.Text, this.FilePathTextBox.Text, tempStatementIdPos, 0, tempStatementNumPos, 0, tempAmountPos, 0, tempCommentPos, 0, tempBankCodePos, 0, tempLoanNumPos, 0, tempTaxPayNamePos, 0, tempParcelNumberPos, 0, tempPostTypePos, 0, tempOwnerIdPos, 0, tempCartPos, 0);

                        this.LockControls(this.EditPermissionButton.ActualPermission);
                        this.LockTextBoxControls(!this.EditPermissionButton.ActualPermission);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(validationErrors))
                        {
                            ////WSHelper.SaveMortgageImportTemplate(tempTemlateID, this.TemplateNameTextBox.Text, tempImportType, TerraScanCommon.UserId, this.DescriptionTextBox.Text, this.FilePathTextBox.Text, tempStatementIdPos, tempStatementIdWid, tempStatementNumPos, tempStatementNumWid, tempAmountPos, tempAmountWid, tempCommentPos, tempCommentWid, tempBankCodePos, tempBankCodeWid, tempLoanNumPos, tempLoanNumWid, tempTaxPayNamePos, tempTaxPayNameWid);
                            F1011WorkItem.SaveMortgageImportTemplate(tempTemlateID, this.TemplateNameTextBox.Text, tempImportType, TerraScanCommon.UserId, this.DescriptionTextBox.Text, this.FilePathTextBox.Text, tempStatementIdPos, tempStatementIdWid, tempStatementNumPos, tempStatementNumWid, tempAmountPos, tempAmountWid, tempCommentPos, tempCommentWid, tempBankCodePos, tempBankCodeWid, tempLoanNumPos, tempLoanNumWid, tempTaxPayNamePos, tempTaxPayNameWid, tempParcelNumberPos, tempParcelNumberWid, tempPostTypePos, tempPostTypeWid, tempOwnerIdPos, tempOwnerIdWid, tempCartPos, tempCartWid);
                            this.LockControls(this.EditPermissionButton.ActualPermission);
                            this.LockTextBoxControls(!this.EditPermissionButton.ActualPermission);
                        }
                        else
                        {
                            MessageBox.Show(validationErrors, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.pageLoadStatus = true;
                            return false;
                        }
                    }

                    this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                    ////this.templateIds = WSHelper.ListMortgageTemplate();
                    this.templateIds = this.Form1011Control.WorkItem.ListMortgageTemplate;

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    ////this.TemplateAuditlinkLabel.Enabled = true;

                    if (this.currentRecord > 0)
                    {
                        this.FillMortgageTemplateDetails(Convert.ToInt32(this.currentRecord));
                    }
                    else
                    {
                        this.EmailButton.Enabled = true;
                        this.PrintButton.Enabled = true;
                        this.PreviewButton.Enabled = true;
                        this.LastRecordDisplay();
                    }
                }
                else
                {
                    if (validationErrors.Trim() == "Position value should be unique.")
                    {
                        MessageBox.Show("Position value should be unique.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ////MessageBox.Show(validationErrors, ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return false;
                }

                this.saveComplete = true;
                this.pageLoadStatus = true;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.pageLoadStatus = true;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <returns>validationErrors</returns>
        private string CheckErrors()
                {
            StringBuilder validationErrors = new StringBuilder();

            int temp1, temp2;
            for (int rowIndex = 0; rowIndex <= 6; rowIndex++)
            {
                int.TryParse(this.inputFileDataTable.Rows[rowIndex][1].ToString().Trim(), out temp1);
                if (temp1 > 0)
                {
                    for (int nextRowIndex = 6; nextRowIndex >= rowIndex; nextRowIndex--)
                    {
                        if (nextRowIndex != rowIndex)
                        {
                            int.TryParse(this.inputFileDataTable.Rows[nextRowIndex][1].ToString().Trim(), out temp2);
                            ////MessageBox.Show(temp1.ToString() + "---" + temp2.ToString());
                            if (temp2 > 0)
                            {
                                if (temp1 == temp2)
                                {
                                    validationErrors = validationErrors.Append(SharedFunctions.GetResourceString("DuplicatePosition"));
                                    ////MessageBox.Show(validationErrors);
                                    return validationErrors.ToString();
                                }
                            }
                        }
                    }
                }
            }

            if (this.ImportTypeComboBox.SelectedValue.ToString().Trim() == "2" && !string.IsNullOrEmpty(validationErrors.ToString()))
            {
                MessageBox.Show(validationErrors.ToString());
                return validationErrors.ToString();
            }

            //// Check for remaining Value in Grid validity
            for (int rowIndex = 0; rowIndex <= 6; rowIndex++)
            {
                if (rowIndex != 2)
                {
                    if (!(string.IsNullOrEmpty(this.inputFileDataTable.Rows[rowIndex]["position"].ToString().Trim()) ||
                        string.Equals(this.inputFileDataTable.Rows[rowIndex]["position"].ToString().Trim(), "0")))
                    {
                        if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                        {
                            if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[rowIndex]["width"].ToString().Trim()) || string.Equals(this.inputFileDataTable.Rows[rowIndex]["width"].ToString().Trim(), "0"))
                            {
                                validationErrors = validationErrors.Append(this.inputFileDataTable.Rows[rowIndex]["FieldName"].ToString() + SharedFunctions.GetResourceString("WidthInputError") + "\n");
                            }
                        }
                    }

                    if (!(string.IsNullOrEmpty(this.inputFileDataTable.Rows[rowIndex]["width"].ToString().Trim()) ||
                        string.Equals(this.inputFileDataTable.Rows[rowIndex]["width"].ToString().Trim(), "0")))
                    {
                        if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                        {
                            if (string.IsNullOrEmpty(this.inputFileDataTable.Rows[rowIndex]["position"].ToString().Trim()) || string.Equals(this.inputFileDataTable.Rows[rowIndex]["position"].ToString().Trim(), "0"))
                            {
                                validationErrors = validationErrors.Append(this.inputFileDataTable.Rows[rowIndex]["FieldName"].ToString() + SharedFunctions.GetResourceString("PositionInputError") + "\n");
                            }
                        }
                    }
                }
            }

            return validationErrors.ToString();
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.ParentForm.Activate();
                this.Cursor = Cursors.WaitCursor;
                ////if (MessageBox.Show(SharedFunctions.GetResourceString("Cancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ////{
                ////this.ClearTemplateDetails();
                this.pageLoadStatus = false;
                this.FillMortgageTemplateDetails(this.currentRecord);
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                ////this.TemplateAuditlinkLabel.Enabled = true;                    
                ////}                
                if (this.totalTemplateCount <= 0)
                {
                    ////this.TemplateAuditlinkLabel.Enabled = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    this.EmailButton.Enabled = false;
                    this.PrintButton.Enabled = false;
                    this.PreviewButton.Enabled = false;
                }

                this.pageLoadStatus = true;
                this.ParentForm.Activate();
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
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        private void DeleteButton_Click()
        {
            this.pageLoadStatus = false;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(this.TemplateIDTextBox.Text))
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ////if (WSHelper.DeleteMortgageTemplate(Convert.ToInt32(this.TemplateIDTextBox.Text), false) == 1)
                        if (this.Form1011Control.WorkItem.DeleteMortgageTemplate(Convert.ToInt32(this.TemplateIDTextBox.Text), false, TerraScanCommon.UserId) == 1)
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteMortgageConfirmation"), SharedFunctions.GetResourceString("MortgageDeleteTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                ////WSHelper.DeleteMortgageTemplate(Convert.ToInt32(this.TemplateIDTextBox.Text), true);
                                this.Form1011Control.WorkItem.DeleteMortgageTemplate(Convert.ToInt32(this.TemplateIDTextBox.Text), true, TerraScanCommon.UserId);
                            }
                            else
                            {
                                this.pageLoadStatus = true;
                                return;
                            }
                        }

                        ////this.templateIds = WSHelper.ListMortgageTemplate();
                        this.templateIds = this.Form1011Control.WorkItem.ListMortgageTemplate;
                        this.FillMortgageTemplateDetails(this.currentRecord);
                        this.SetButtons(TerraScanCommon.ButtonActionMode.DeleteMode);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;

                        if (this.currentRecord == 0)
                        {
                            this.operationSmartPart.DeleteButtonEnable = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.pageLoadStatus = true;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellEnter event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) 
            {
                SendKeys.Send("{TAB}");
            }
        }

        /// <summary>
        /// Imports the type combo box_ selected index changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ////khaja commmented code to fix Bug#4546
                if (this.ImportTypeComboBox.SelectedValue != null)
                {
                    if (this.InputFileGridView.Columns.Count >= 2)
                    {
                        if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                        {
                            this.InputFileGridView.Columns[2].Visible = true;
                            this.InputFileGridView.Width = 378;
                            this.MortgageTemplateControlPanel.Width = 480;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(370, 0);
                            this.pageLoadStatus = true;
                            //this.SetEditRecord();
                        }
                        else if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                        {
                            this.InputFileGridView.Columns[2].Visible = false;
                            this.MortgageTemplateControlPanel.Width = 315;
                            this.InputFileGridView.Width = 276;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(268, 0);
                            this.InputFileDetailPictureBox.Size = new System.Drawing.Size(36, 265);
                            this.pageLoadStatus = true;
                            //this.SetEditRecord();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int outInt;

            // Only paint if desired, formattable column

            if (e.ColumnIndex == this.InputFileGridView.Columns["Position"].Index || e.ColumnIndex == this.InputFileGridView.Columns["Width"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                //// Only paint if text provided, Only paint if desired text is in cell 
                ////if (e.Value != null && !String.IsNullOrEmpty(this.InputFileGridView.Rows[e.RowIndex].Cells["Position"].Value.ToString()))
                if (!string.IsNullOrEmpty(e.Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (e.Value.ToString().Trim() == "M")
                    {
                    }

                    if (!int.TryParse(val, out outInt))
                    {
                        e.Value = string.Empty;
                        e.FormattingApplied = false;
                    }
                    else if (e.Value.ToString() == "0")
                    {
                        e.Value = string.Empty;
                    }
                }
                else
                {
                    e.Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// Checks the file exist.
        /// </summary>
        /// <returns>returns bool</returns>
        private string CheckFileExist()
        {
            string filePath = string.Empty;
            filePath = this.FilePathTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(filePath))
            {
                if (!File.Exists(filePath))
                {
                    this.FilePathTextBox.Text = string.Empty;
                    this.FilePathTextBox.Focus();
                    return "Specified File doesn't Exist. \n";
                }

                return string.Empty;
            }
            else
            {
                this.FilePathTextBox.Focus();
                return "File path should be entered. \n";
            }
        }

        /// <summary>
        /// Check the page Status when the New Reciept is Cancelled
        /// </summary>
        /// <returns>boolean Value</returns>
        private bool CheckPageStatus()
        {
            if (String.Compare(this.pageMode.ToString(), TerraScanCommon.PageModeTypes.View.ToString(), true) != 0)
            {
                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + " " + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveMortageImport();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.CancelButton_Click();
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
        /// Handles the FormClosing event of the MortgageImportTemplateForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void MortgageImportTemplateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
            {
                if (e.CloseReason.Equals(CloseReason.UserClosing))
                {
                    if (!this.CheckPageStatus())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageLoadStatus && this.pageMode == TerraScanCommon.PageModeTypes.View && this.EditPermissionButton.ActualPermission && !string.IsNullOrEmpty(this.TemplateIDTextBox.Text.Trim()))
            {
                this.pageLoadStatus = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.pageLoadStatus = true;
                this.saveComplete = false;
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.SetEditRecord();
        }

        /// <summary>
        /// Handles the Click event of the FilePathButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FilePathButton_Click(object sender, EventArgs e)
        {
            this.FilePathOpenFileDialog.Filter = "Text Documents(*.txt)|*.txt|CSV(*.csv)|*.csv|All Files(*.*)|*.*";
            if (this.FilePathOpenFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                this.SetEditRecord();
                this.FilePathTextBox.Text = this.FilePathOpenFileDialog.FileName;
            }
        }

        /// <summary>
        /// Handles the Leave event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_Leave(object sender, EventArgs e)
        {
            try
            {
                this.InputFileGridView.CurrentCell = null;
            }
            catch (System.InvalidOperationException ex)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

            ////if (!this.pageLoadStatus)
            ////{
            ////    try
            ////    {
            ////        this.InputFileGridView.CurrentCell = null;
            ////    }
            ////    catch (System.InvalidOperationException ex)
            ////    {
            ////        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
            ////    }
            ////}
        }

        /// <summary>
        /// Handles the Enter event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_Enter(object sender, EventArgs e)
        {
            this.pageLoadStatus = true;
            this.InputFileGridView.CurrentCell = this.InputFileGridView[1, 0];
        }

        /// <summary>
        /// Handles the Click event of the ImportViewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportViewButton_Click(object sender, EventArgs e)
        {
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(1010);
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }

        /// <summary>
        /// Handles the Enter event of the MortgageImportTemplateForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MortgageImportTemplateForm_Enter(object sender, EventArgs e)
        {
            this.pageLoadStatus = true;
        }

        /// <summary>
        /// Handles the CellParsing event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            int temp;
            if (e.Value.ToString().IndexOf('-') > -1)
            {
                e.Value = string.Empty;
                e.ParsingApplied = true;
            }
            else if (!int.TryParse(e.Value.ToString(), out temp))
            {
                e.Value = string.Empty;
                e.ParsingApplied = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrintButton_Click(object sender, EventArgs e)
        {
            // TODO : Genralized 
            Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                reportOptionalParameter.Clear();
                reportOptionalParameter.Add("KeyName", "TemplateID");
                reportOptionalParameter.Add("KeyValue", this.TemplateIDTextBox.Text);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(10111, TerraScan.Common.Reports.Report.ReportType.Print, reportOptionalParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            // TODO : Genralized 
            Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                reportOptionalParameter.Clear();
                reportOptionalParameter.Add("KeyName", "TemplateID");
                reportOptionalParameter.Add("KeyValue", this.TemplateIDTextBox.Text);
                TerraScanCommon.ShowReport(10111, TerraScan.Common.Reports.Report.ReportType.Preview, reportOptionalParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the EmailButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EmailButton_Click(object sender, EventArgs e)
        {
            // TODO : Genralized 
            Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                reportOptionalParameter.Clear();
                reportOptionalParameter.Add("KeyName", "TemplateID");
                reportOptionalParameter.Add("KeyValue", this.TemplateIDTextBox.Text);

                TerraScanCommon.ShowReport(10111, TerraScan.Common.Reports.Report.ReportType.Email, reportOptionalParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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
                if (this.FilePathTextBox.Text.Length > 60)
                {
                    this.TemplateToolTip.RemoveAll();
                    this.TemplateToolTip.SetToolTip(this.FilePathTextBox, this.FilePathTextBox.Text);
                }
                else
                {
                    this.TemplateToolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// Handles the DataError event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the Leave event of the NewTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewTemplateButton_Leave(object sender, EventArgs e)
        {
            ////todo: this.NewTemplateButton.TabStop = false;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ImportTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.InputFileDetailPictureBox.SendToBack();
                if (this.ImportTypeComboBox.SelectedValue != null)
                {
                    if (this.InputFileGridView.Columns.Count >= 2)
                    {
                        if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                        {
                            this.InputFileGridView.Columns[2].Visible = true;
                            this.InputFileGridView.Width = 378;
                            this.MortgageTemplateControlPanel.Width = 480;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(370, 0);
                            this.pageLoadStatus = true;
                            this.SetEditRecord();
                        }
                        else if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                        {
                            this.InputFileGridView.Columns[2].Visible = false;
                            this.MortgageTemplateControlPanel.Width = 315;
                            this.InputFileGridView.Width = 276;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(268, 0);
                            this.InputFileDetailPictureBox.Size = new System.Drawing.Size(36, 265);
                            this.pageLoadStatus = true;
                            this.SetEditRecord();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the F1011 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1011_Enter(object sender, EventArgs e)
        {
            this.ParentForm.Activate();
            this.pageLoadStatus = true;
        }

        /// <summary>
        /// Enables the edit record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void EnableEditRecord(object sender, KeyEventArgs e)
            {
            if (e.KeyCode == Keys.Delete)
            {
                this.pageLoadStatus = true;
                this.SetEditRecord();
            }
        }

        /// <summary>
        /// Edits the record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EditRecord(object sender, KeyPressEventArgs e)
        {
            this.pageLoadStatus = true;
            this.SetEditRecord();
        }

        #region Events
        /// <summary>
        /// Handles the Validated event of the InputFileGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_Validated(object sender, EventArgs e)
        {
            this.InputFileGridView.EditingControl.TextChanged -= new EventHandler(this.InputFileGridView_TextChanged);
        }

        /// <summary>
        /// Handles the TextChanged event of theInputFileGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_TextChanged(object sender, EventArgs e)
            {
            try
            {
                if (!this.saveComplete)
                {
                    this.pageLoadStatus = true;
                    this.SetEditRecord();
                    this.pageLoadStatus = false;
                }
                else
                {
                    this.saveComplete = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            e.Control.TextChanged += new EventHandler(this.InputFileGridView_TextChanged);
            e.Control.Validated += new EventHandler(this.InputFileGridView_Validated);
        }

        /// <summary>
        /// Handles the CellEndEdit event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    this.InputFileGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    int currentValue = 0;
                    int.TryParse(this.InputFileGridView.CurrentCell.Value.ToString(), out currentValue);
                    if (currentValue > 0 && currentValue <= 32000)
                    {
                        // this.InputFileGridView.CurrentCell = null;
                    }
                    else
                    {
                        this.InputFileGridView.CurrentCell.Value = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

       
    }
}
