//--------------------------------------------------------------------------------------------
// <copyright file="F1501.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1501 Gl Configuration Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 2-11-2006        Krishna Abburi      Created
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
    /// F1501 Class 
    /// </summary>
    [SmartPart]
    public partial class F1501 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// subFundManagementData Variable 
        /// </summary>
        private F9503SubFundManagementData subFundManagementData = new F9503SubFundManagementData();

        /// <summary>
        /// getdescriptionDataset Variable 
        /// </summary>
        private AccountManagementData getdescriptionDataset = new AccountManagementData();

        /// <summary>
        /// gLConfigData Variable 
        /// </summary>
        private F1501GLConfigurationData glconfigData = new F1501GLConfigurationData();

        /// <summary>
        /// listItemsDataset Variable 
        /// </summary>
        private F1503GenericManagementData listItemsDataset = new F1503GenericManagementData();

        /// <summary>
        /// form1501Controll Variable 
        /// </summary>
        private F1501Controller form1501Controll;

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// operationSmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart;

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
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// recordPointerArray variable
        /// </summary>
        private int[] recordPointerArray = new int[2];

        /// <summary>
        /// used to maintain loadstate
        /// </summary>
        private bool formLoad = true;

        /// <summary>
        /// gLConfigID  variable
        /// </summary>
        private int glconfigurationId;

        /// <summary>
        /// rollYear variable
        /// </summary>
        private short rollYear;

        /// <summary>
        /// autosource 
        /// </summary>
        private AutoCompleteStringCollection autosource = new AutoCompleteStringCollection();

        /// <summary>
        /// subFundId
        /// </summary>
        private int subFundIdentifier;

        /// <summary>
        /// current Row Index
        /// </summary>
        private int currentRowIndex = -1;

        /// <summary>
        /// glconfigRecordCount
        /// </summary> 
        private int glconfigRecordCount;

        /// <summary>
        /// current Row Index
        /// </summary>
        private bool gridSelected;

        /// <summary>
        /// sourceSubFund
        /// </summary>
        private AutoCompleteStringCollection sourceSubFund; // = new AutoCompleteStringCollection();
        
        /// <summary>
        /// sourceFunction
        /// </summary>
        private AutoCompleteStringCollection sourceFunction = new AutoCompleteStringCollection();

        /// <summary>
        /// sourceBar
        /// </summary>
        private AutoCompleteStringCollection sourceBar = new AutoCompleteStringCollection();

        /// <summary>
        /// sourceLine
        /// </summary>
        private AutoCompleteStringCollection sourceLine = new AutoCompleteStringCollection();

        /// <summary>
        /// sourceObject
        /// </summary>
        private AutoCompleteStringCollection sourceObject = new AutoCompleteStringCollection();

        /// <summary>
        /// sourceObject
        /// </summary>
        private DataTable tempDataTable = new DataTable();

        /// <summary>
        /// flag formLoad.
        /// </summary>
        private bool formload = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1501"/> class.
        /// </summary>
        public F1501()
        {
            InitializeComponent();
            this.GLConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GLConfigPictureBox.Height, this.GLConfigPictureBox.Width, "GL Item Configuration", 28, 81, 128);
            this.GLConfigListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GLConfigListPictureBox.Height, this.GLConfigListPictureBox.Width, "GL Configuration Listing", 174, 150, 94);
        }

        #endregion

        #region Event Publication

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

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Event Publication for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form1501 controll.
        /// </summary>
        /// <value>The form1501 controll.</value>
        [CreateNew]
        public F1501Controller Form1501Controll
        {
            get { return this.form1501Controll as F1501Controller; }
            set { this.form1501Controll = value; }
        }

        /// <summary>
        /// Gets or sets the GL config id.
        /// </summary>
        /// <value>The GL config id.</value>
        private int GLConfigurationId
        {
            get
            {
                return this.glconfigurationId;
            }

            set
            {
                this.glconfigurationId = value;
                this.additionalOperationSmartPart.KeyId = this.glconfigurationId;
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
                return this.subFundIdentifier;
            }

            set
            {
                this.subFundIdentifier = value;
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

        #endregion

        #region EventSubcription

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
                this.form1501Controll.WorkItem.State["FormStatus"] = this.CheckPageStatus(false);
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
     
        #endregion

        #region FormEvents

        /// <summary>
        /// Handles the Load event of the F1501 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1501_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                this.IntializeCombo();
                this.CustomizeItemListingGridView();
                this.PopulateGLConfigDetailsGridview();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.DistrictTextBox.Select();
                this.SetAutoComplete();
                if (!this.PermissionFiled.editPermission)
                {
                    this.GlConfigInfoPanel.Enabled = false;
                    this.GLConfigItemsGridView.Enabled = false;
                    this.panel5.Enabled = false;
                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
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
                this.PopulateGLConfigDetailsGridview();
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.RollYearCombo.Enabled = true;
                this.GLConfigItemsGridView.AllowSorting = true;
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
                this.PopulateGLConfigDetailsGridview();
                this.SubFundAutoCompleteSource();
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the GLConfigItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GLConfigItemsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.glconfigRecordCount > 0)
                {
                    if (e.RowIndex >= 0 && this.currentRowIndex != e.RowIndex)
                    {
                        this.GLConfigurationId = Convert.ToInt32(this.GLConfigItemsGridView.Rows[e.RowIndex].Cells["GLConfigID"].Value.ToString());
                        this.GLConfigIDAuditlink.Text = SharedFunctions.GetResourceString("1501GLIDLink") + " " + Convert.ToInt32(this.GLConfigItemsGridView.Rows[e.RowIndex].Cells["GLConfigID"].Value.ToString());
                        this.FillGLitemItemBoxes(this.glconfigurationId);
                        this.SetAttachmentCommentsCount();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.RollYearCombo.Enabled = true;
                        this.currentRowIndex = e.RowIndex;
                    }
                }
                else
                {
                    this.currentRowIndex = -1;
                }
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
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
             catch (SoapException exc)
             {
                 ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetDescription(barId, "Bar");
                    if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                    {
                        this.BarsDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.BarsDescTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.BarsDescTextBox.Text = "";
                        this.BarsDescTextBox.Enabled = true;
                        this.BarsDescTextBox.LockKeyPress = false;
                    }
                }
                else
                {
                    this.BarsDescTextBox.Text = "";
                    this.BarsDescTextBox.LockKeyPress = true;
                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetDescription(objectId, "Object");
                if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                {
                    this.ObjectDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                    this.ObjectDescTextBox.LockKeyPress = true;
                }
                else
                {
                    this.ObjectDescTextBox.Text = string.Empty;
                    this.ObjectDescTextBox.Enabled = true;
                    this.ObjectDescTextBox.LockKeyPress = false;
                }
            }
            else
            {
                this.ObjectDescTextBox.Text = string.Empty;
                this.ObjectDescTextBox.LockKeyPress = true;
            }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetDescription(lineId, "Line");
                if (this.getdescriptionDataset.GetDescription.Rows.Count > 0)
                {
                    this.LineDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                    this.LineDescTextBox.LockKeyPress = true;
                }
                else
                {
                    this.LineDescTextBox.Text = string.Empty;
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
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                TerraScanCommon.ShowReport(150110, TerraScan.Common.Reports.Report.ReportType.Preview);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AcctManagementButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcctManagementButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11007);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                ////Form accountSelectionForm = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(11007, null, this.form1501Controll.WorkItem);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the GLConfigIDAuditlink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GLConfigIDAuditlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int auditLinkKeyID = 0;
                int.TryParse(this.GLConfigIDTextBox.Text, out auditLinkKeyID);

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
                ////reportAuditId = Convert.ToInt32(this.GLConfigIDTextBox.Text);
                ////this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("ReportFileID", reportAuditId);
                //////// Shows the report form.
                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(150190, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
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
                functionForm = this.form1501Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1502, null, this.form1501Controll.WorkItem);
                if (functionForm != null)
                {
                    if (functionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.FunctionTextBox.Text = TerraScanCommon.GetValue(functionForm, "FunctionIdValue").ToString();
                        ////this.getdescriptionDataset = this.form1501Controll.WorkItem.F1500_GetDescription(this.FunctionTextBox.Text.Trim(), "Function");
                        ////this.FunctionDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                        this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetFunctionItems(this.FunctionTextBox.Text.Trim());
                        if (this.getdescriptionDataset.GetFunctionItems.Rows.Count > 0)
                        {
                            this.FunctionDescTextBox.Text = this.getdescriptionDataset.GetFunctionItems.Rows[0]["Description"].ToString();
                            this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);

                            if (this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"].ToString() != "")
                            {
                                this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.getdescriptionDataset.GetFunctionItems.Rows[0]["SemiAnnualCode"]);
                            }

                            this.FunctionDescTextBox.ReadOnly = true;
                        }

                        this.FunctionTypeCombo.Enabled = false;
                    }
                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
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
                Form subfundForm = new Form();
                short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                object[] optionalParameter = new object[] { this.rollYear };
                subfundForm = this.form1501Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1515, optionalParameter, this.form1501Controll.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.SubFundTextBox.Text = TerraScanCommon.GetValue(subfundForm, "SubFundItem").ToString();
                        this.SubFundTextBox.ForeColor = Color.Black;
                        if (!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()))
                        {
                            ////this.getdescriptionDataset = this.form1501Controll.WorkItem.F1500_GetDescription(this.SubFundTextBox.Text, "SubFund");
                            ////this.SubFundDescTextBox.Text = this.getdescriptionDataset.GetDescription.Rows[0]["Description"].ToString();
                            ////short.TryParse(this.RollYearTextBox.Text.ToString(), out rollYear);
                            this.subFundManagementData = this.form1501Controll.WorkItem.F9503_GetSubFundItems(this.SubFundTextBox.Text.Trim(), this.rollYear);
                            if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                            {
                                this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                                this.subFundIdentifier = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"].ToString());
                                if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() == "0")
                                {
                                    RollYearTextBox.Text = "0";
                                }

                                ////this.AccounTypeTextBox.Text = 
                            }
                            else
                            {
                                this.subFundIdentifier = 0;
                                this.SubFundTextBox.ForeColor = Color.DarkRed;
                            }
                        }
                    }
                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the GLConfigItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GLConfigItemsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            if (e.KeyValue == 40 || e.KeyValue == 38)
            {
                this.gridSelected = true;
            }
            }
             catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowValidating event of the GLConfigItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void GLConfigItemsGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (this.gridSelected)
                {
                    this.gridSelected = false;
                    if (this.operationSmartPart.SaveButtonEnable == true)
                    {
                        DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (!this.SaveAccountRecord(false))
                            {
                                e.Cancel = true;
                                this.DistrictTextBox.Focus();
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            e.Cancel = false;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            this.GLConfigItemsGridView.TabStop = true;
                            this.GLConfigItemsGridView.AllowSorting = true;
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            this.DistrictTextBox.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the GLConfigItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void GLConfigItemsGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo hit = this.GLConfigItemsGridView.HitTest(e.X, e.Y);

                if (hit.RowIndex > -1 && hit.RowIndex != this.GLConfigItemsGridView.CurrentCell.RowIndex)
                {
                    this.gridSelected = true;
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the GLConfigItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GLConfigItemsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.glconfigRecordCount > 0)
                {
                    ////this.glconfigId = Convert.ToInt32(this.GLConfigItemsGridView.Rows[0].Cells[0].Value.ToString());
                    //////this.GLConfigIDAuditlink.Text = SharedFunctions.GetResourceString("1501GLIDLink") + " " + Convert.ToInt32(this.GLConfigItemsGridView.Rows[0].Cells[0].Value.ToString());
                    //////this.FillGLitemItemBoxes(this.glconfigId);
                    ////this.SetAttachmentCommentsCount();
                    ////this.pageMode = TerraScanCommon.PageModeTypes.View;
                    ////this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    ////this.RollYearCombo.Enabled = true;
                    ////this.currentRowIndex = e.RowIndex;
                }
                else
                {
                    this.currentRowIndex = -1;
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the GLConfigItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GLConfigItemsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /* if (this.CheckPageStatus(false))
            {

                //e.cancel;
                //this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            } */
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the GLConfigItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void GLConfigItemsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.glconfigRecordCount > 0)
                {
                    this.GLConfigItemsGridView.Rows[0].Selected = true;
                    this.GLConfigItemsGridView.CurrentCell = this.GLConfigItemsGridView[0, 0];
                    this.GLConfigItemsGridView.Focus();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Private Methods

        #region Intialization controls

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            // Load ExciseTaxActionButtons SmartPart into ExciseTaxActionButtonsdeckWorkspace
            if (this.form1501Controll.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.TestOperationSmartPartdeckWorkspace.Show(this.form1501Controll.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart));
            }
            else
            {
                this.TestOperationSmartPartdeckWorkspace.Show(this.form1501Controll.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart));
            }

            this.operationSmartPart = (OperationSmartPart)this.form1501Controll.WorkItem.SmartParts[SmartPartNames.OperationSmartPart];
            //// To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
            if (this.form1501Controll.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1501Controll.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1501Controll.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form1501Controll.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form1501Controll.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form1501Controll.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = "General Ledger Configuration"; ////Properties.Resources.FormName;
            formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));

            this.operationSmartPart.DeleteButtonVisible = false;
            this.operationSmartPart.NewButtonVisible = false;
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1501Controll.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1501Controll.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void EnableControls(bool enableValue)
        {
            this.GLConfigIDTextBox.Enabled = true;
            if (RollYearTextBox.Text == "0")
            {
                this.RollYearTextBox.Enabled = false;
            }
            else
            {
                this.RollYearTextBox.Enabled = enableValue;
            }

            this.DistrictTextBox.Enabled = enableValue;
            this.DescriptionTextBox.Enabled = enableValue;
            this.DisableButtonBasedOnConfigValues();
            this.FunctionTypeCombo.Enabled = false;
            this.SubFundTextBox.ForeColor = Color.Black;
            this.SubFundDescTextBox.LockKeyPress = true;
            this.FunctionDescTextBox.LockKeyPress = true;
            this.BarsDescTextBox.LockKeyPress = true;
            this.ObjectDescTextBox.LockKeyPress = true;
            this.LineDescTextBox.LockKeyPress = true;
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
        /// Intializes the combo.
        /// </summary>
        private void IntializeCombo()
        {
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

            this.glconfigData = this.form1501Controll.WorkItem.F1501_ListRollYear();
            if (this.glconfigData.ListGLConfigRollYear.Rows.Count > 0)
            {
                this.RollYearCombo.ValueMember = this.glconfigData.ListGLConfigRollYear.RollYearColumn.ColumnName;
                this.RollYearCombo.DisplayMember = this.glconfigData.ListGLConfigRollYear.RollYearColumn.ColumnName;
                this.RollYearCombo.DataSource = this.glconfigData.ListGLConfigRollYear;
                this.GetYear();
                ////this.RollYearCombo.SelectedValue = this.gLConfigData.ListGLConfigRollYear.Rows.Count;
            }
            else 
            {
                this.SetDefaultState();
            }
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
                CommentsData getYearDataSet = new CommentsData();
                getYearDataSet = this.form1501Controll.WorkItem.GetConfigDetails("TR_RollYear");
                if (getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString() == "0")
                {
                    this.RollYearCombo.SelectedValue = Convert.ToInt16(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString());
                }
                else
                {
                    int temp = 0;
                    int.TryParse(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString(), out temp);
                    if (temp != 0)
                    {
                        this.RollYearCombo.SelectedValue = Convert.ToInt16(getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString());
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        /// <summary>
        /// Gets the config value.
        /// </summary>
        /// <param name="configurationObjectName">Name of the configuration object.</param>
        /// <returns>config value</returns>
        private bool GetConfigValue(string configurationObjectName)
        {
            this.getdescriptionDataset.GetConfiguration.Clear();
            this.getdescriptionDataset.Merge(this.form1501Controll.WorkItem.F1501_GetConfigurationValue(configurationObjectName));
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
        /// Sets the state of the default.
        /// </summary>
        private void SetDefaultState()
        {
            this.ClearGLConfigDetails();
            this.CommentsdeckWorkspace.Enabled = false;
            this.GLConfigIDAuditlink.Enabled = false;
            this.GlConfigInfoPanel.Enabled = false;
            this.GLConfigItemsGridView.Enabled = false;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

                if (this.glconfigurationId != -999)
                {
                    this.additionalOperationSmartPart.KeyId = this.glconfigurationId;
                    additionalOperationCountEntity.AttachmentCount = this.form1501Controll.WorkItem.GetAttachmentCount(this.ParentFormId, this.glconfigurationId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1501Controll.WorkItem.GetCommentsCount(this.ParentFormId, this.glconfigurationId, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
        }

        /// <summary>
        /// Customizes the item listing grid view.
        /// </summary>
        private void CustomizeItemListingGridView()
        {            
            this.GLConfigItemsGridView.AutoGenerateColumns = false;         
            DataGridViewColumnCollection columns = this.GLConfigItemsGridView.Columns;
            columns["GLConfigID"].DataPropertyName = this.glconfigData.ListGLConfigDetail.GLConfigIDColumn.ColumnName;
            columns["SubFundID"].DataPropertyName = this.glconfigData.ListGLConfigDetail.SubFundIDColumn.ColumnName;
            columns["Description"].DataPropertyName = this.glconfigData.ListGLConfigDetail.DescriptionColumn.ColumnName;
            columns["SubFund"].DataPropertyName = this.glconfigData.ListGLConfigDetail.SubFundColumn.ColumnName;
            columns["Function"].DataPropertyName = this.glconfigData.ListGLConfigDetail.FunctionIDColumn.ColumnName;
            columns["Bars"].DataPropertyName = this.glconfigData.ListGLConfigDetail.BarIDColumn.ColumnName;
            columns["Object"].DataPropertyName = this.glconfigData.ListGLConfigDetail.ObjectIDColumn.ColumnName;
            columns["Object"].ReadOnly = true;
            columns["Line"].DataPropertyName = this.glconfigData.ListGLConfigDetail.LineIDColumn.ColumnName;
            columns["Line"].ReadOnly = true;
        }

        /// <summary>
        /// Clears the GL config details.
        /// </summary>
        private void ClearGLConfigDetails()
        {
            this.GLConfigIDTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.DistrictTextBox.Text = string.Empty;
            this.SubFundTextBox.Text = string.Empty;
            this.SubFundDescTextBox.Text = string.Empty;
            this.FunctionTextBox.Text = string.Empty;
            this.FunctionDescTextBox.Text = string.Empty;
            this.FunctionTypeCombo.SelectedValue = 2;
            this.BarsTextBox.Text = string.Empty;
            this.BarsDescTextBox.Text = string.Empty;
            this.ObjectTextBox.Text = string.Empty;
            this.ObjectDescTextBox.Text = string.Empty;
            this.LineTextBox.Text = string.Empty;
            this.LineDescTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Disables the V scroll bar.
        /// </summary>
        /// <param name="recordsCount">The records count.</param>
        private void DisableVScrollBar(int recordsCount)
        {
            if (recordsCount > this.GLConfigItemsGridView.NumRowsVisible)
            {
                this.VScrollBar.Visible = false;
            }
            else
            {
                this.VScrollBar.Visible = true;
            }
        }

        #endregion

        #region Page Settings

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>true or false</returns>
        private bool CheckPageStatus(bool onclose)
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName + "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
        /// Subs the fund text changed.
        /// </summary>
        private void SubFundTextChanged()
        {
                string keyId = string.Empty;
                keyId = this.SubFundTextBox.Text;
                if (!string.IsNullOrEmpty(keyId))
                {
                    short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                    this.subFundManagementData = this.form1501Controll.WorkItem.F9503_GetSubFundItems(keyId, this.rollYear);
                    if (this.subFundManagementData.getSubFundItems.Rows.Count > 0)
                    {
                        this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString();
                        this.subFundIdentifier = Convert.ToInt32(this.subFundManagementData.getSubFundItems.Rows[0]["SubFundID"].ToString());
                        this.SubFundTextBox.ForeColor = Color.Black;
                        this.SubFundDescTextBox.LockKeyPress = true;
                        if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() != this.RollYearTextBox.Text.ToString().Trim())
                        {
                            if (this.subFundManagementData.getSubFundItems.Rows[0]["RollYear"].ToString().Trim() == "0")
                            {
                                this.RollYearTextBox.Text = "0";
                                this.SubFundDescTextBox.Text = this.subFundManagementData.getSubFundItems.Rows[0]["Description"].ToString().Trim();
                            }
                            else
                            {
                                this.SubFundDescTextBox.Text = string.Empty;
                                this.SubFundTextBox.ForeColor = Color.DarkRed;
                                this.subFundIdentifier = 0;
                            }
                        }
                    }
                    else
                    {
                        this.SubFundDescTextBox.Text = string.Empty;
                        this.SubFundTextBox.ForeColor = Color.DarkRed;
                        this.subFundIdentifier = 0;
                    }
                }
                else
                {
                    this.SubFundDescTextBox.Text = string.Empty;
                    this.SubFundDescTextBox.LockKeyPress = true;
                    this.subFundIdentifier = 0;
                }
        }

        /// <summary>
        /// Functions the text box changed.
        /// </summary>
        private void FunctionTextBoxChanged()
        {
                string functionId = string.Empty;
                functionId = this.FunctionTextBox.Text;
                if (!string.IsNullOrEmpty(functionId))
                {
                    this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetFunctionItems(functionId);
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
                    this.FunctionDescTextBox.Text = string.Empty;
                    this.FunctionDescTextBox.LockKeyPress = true;
                    this.FunctionDescTextBox.ReadOnly = true;
                    this.FunctionTypeCombo.Enabled = false;
                    this.FunctionTypeCombo.SelectedValue = 2;
                }
        }

        /// <summary>
        /// Calls to selection.
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

                Form activeWorkOrderSelectForm = new Form();
                object[] optionalParameter = new object[] { keyName };
                activeWorkOrderSelectForm = this.form1501Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1503, optionalParameter, this.form1501Controll.WorkItem);
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
                                        this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetDescription(keyID, "Bar");
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
                                        this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetDescription(keyID, "Line");
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
                                        this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetDescription(keyID, "Object");
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

        /// <summary>
        /// Fields the edit process.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FieldEditProcess(object sender, EventArgs e)
        {
            try
            {
                if (!this.formload)
                {
                    this.SeteditrProcess();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Seteditrs the process.
        /// </summary>
        private void SeteditrProcess()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            this.GLConfigItemsGridView.AllowSorting = false;
            this.RollYearCombo.Enabled = false;
        }

        #endregion

        #region getting values and save value from database

        /// <summary>
        /// Populates the GL config details gridview.
        /// </summary>
        private void PopulateGLConfigDetailsGridview()
        {
            int rolYearValue = 0;
            this.currentRowIndex = -1;
            this.glconfigData.ListGLConfigDetail.Rows.Clear();
            int.TryParse(this.RollYearCombo.Text.ToString(), out rolYearValue);
            this.glconfigData = this.form1501Controll.WorkItem.F1501_ListGLConfigDetails(rolYearValue);
            this.glconfigRecordCount = this.glconfigData.ListGLConfigDetail.Rows.Count;
            this.tempDataTable = this.glconfigData.ListGLConfigDetail.Copy();
            this.GLConfigItemsGridView.DataSource = this.glconfigData.ListGLConfigDetail;            

            if (this.glconfigRecordCount > 0)
            {
                this.EnableControls(true);
                this.GLConfigItemsGridView.Enabled = true;
                this.GLConfigItemsGridView.Focus();
                TerraScanCommon.SetDataGridViewPosition(this.GLConfigItemsGridView, 0);                
                this.GLConfigIDAuditlink.Enabled = true;
                this.GLConfigIDAuditlink.Text = SharedFunctions.GetResourceString("1501GLIDLink") + " " + Convert.ToInt32(this.GLConfigItemsGridView.Rows[0].Cells["GLConfigID"].Value.ToString());
                this.GLConfigurationId = Convert.ToInt32(this.GLConfigItemsGridView.Rows[0].Cells["GLConfigID"].Value.ToString());
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.RollYearCombo.Enabled = true;
                this.SetAttachmentCommentsCount();
            }
            else
            {
                this.ClearGLConfigDetails();
                this.EnableControls(false);
                this.GLConfigItemsGridView.Rows[0].Selected = false;
                this.GLConfigItemsGridView.Enabled = false;
                this.GLConfigIDAuditlink.Enabled = false;
                this.GLConfigIDAuditlink.Text = SharedFunctions.GetResourceString("1501GLIDLink") + " ";
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.SetDefaultState();
                ////this.NullRecords = true;
            }

            this.DisableVScrollBar(this.glconfigRecordCount);
        }

        /// <summary>
        /// Fills the G litem item boxes.
        /// </summary>
        /// <param name="glconfigId">The gl config id.</param>
        private void FillGLitemItemBoxes(int glconfigId)
        {
            this.formload = true;
            this.glconfigData.GetGLConfigDetail.Clear();
            this.glconfigData = this.form1501Controll.WorkItem.F1501_GetGLConfigDetails(this.glconfigurationId);
            ////this.glconfigData = this.form1501Controll.WorkItem.F1501_GetGLConfigDetails(glconfigId);
            if (this.glconfigData.GetGLConfigDetail.Rows.Count > 0)
            {
                this.GLConfigIDTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["ConfigID"].ToString();
                this.RollYearTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["RollYear"].ToString();
                this.DescriptionTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["ConfigDesc"].ToString();
                this.DistrictTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["DistrictID"].ToString();
                this.SubFundTextBox.ForeColor = Color.Black;
                if (!string.IsNullOrEmpty(this.glconfigData.GetGLConfigDetail.Rows[0]["SubFund"].ToString()))
                {
                    this.SubFundTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["SubFund"].ToString();
                }
                else
                {
                    this.SubFundTextBox.Text = "";
                }
              
                if (!string.IsNullOrEmpty(this.glconfigData.GetGLConfigDetail.Rows[0]["SubFundId"].ToString()))
                {
                    this.subFundIdentifier = Convert.ToInt32(this.glconfigData.GetGLConfigDetail.Rows[0]["SubFundId"].ToString());
                }

                this.SubFundDescTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["SubFundDesc"].ToString();
                this.FunctionTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["FunctionID"].ToString();
                this.FunctionDescTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["FunctionDesc"].ToString();
                if (this.glconfigData.GetGLConfigDetail.Rows[0]["SemiAnnualCode"].ToString() != "")
                {
                    this.FunctionTypeCombo.SelectedValue = Convert.ToInt16(this.glconfigData.GetGLConfigDetail.Rows[0]["SemiAnnualCode"]);
                }

                this.BarsTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["BarID"].ToString();
                this.BarsDescTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["BarsDesc"].ToString();
                this.ObjectTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["ObjectID"].ToString();
                this.ObjectDescTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["ObjectDesc"].ToString();
                this.LineTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["LineID"].ToString();
                this.LineDescTextBox.Text = this.glconfigData.GetGLConfigDetail.Rows[0]["LineDesc"].ToString();
                this.formload = false;
                if (this.glconfigData.GetGLConfigDetail.Rows[0]["SubFundID"].ToString() != "")
                {
                    this.subFundIdentifier = Convert.ToInt32(this.glconfigData.GetGLConfigDetail.Rows[0]["SubFundID"].ToString());
                }
            }               
            else
            {
            }
        }

        /// <summary>
        /// Saves the account record.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>true or false</returns>
        private bool SaveAccountRecord(bool onclose)
        {
            ////Code Commented by Malliga for Bugid-2586
           
            ////if ((string.IsNullOrEmpty(this.SubFundTextBox.Text.ToString().Trim())))
            ////{
            ////    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F15004SubFundMissReq") + " \n", "TerraScan - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);               

            ////    if (dialogResult == DialogResult.OK)
            ////    {
            ////        this.SubFundTextBox.Focus();
            ////        return false;
            ////    }
            ////}

            ////if (this.subFundIdentifier == 0)
            ////{
            ////    DialogResult dialogResult = MessageBox.Show("The General Ledger Configuration Item cannot be saved without a valid SubFund.", "TerraScan - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);

            ////    if (dialogResult == DialogResult.OK)
            ////    {
            ////        this.SubFundTextBox.Focus();
            ////        return false;
            ////    }
            ////}

            //if ((string.IsNullOrEmpty(this.FunctionTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.BarsTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.ObjectTextBox.Text.ToString().Trim())) && (string.IsNullOrEmpty(this.LineTextBox.Text.ToString().Trim())))
            //{
            //    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F1501RequiredFieldMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    if (dialogResult == DialogResult.OK)
            //    {
            //        this.FunctionTextBox.Focus();
            //        return false;
            //    }
            //}

                int errorStatus;
                //this.Cursor = Cursors.WaitCursor;
                string validationErrors = string.Empty;
                bool yearValidationErrors = false;
                F1501GLConfigurationData saveGLConfigData = new F1501GLConfigurationData();
                F1501GLConfigurationData.GetGLConfigDetailRow dr = saveGLConfigData.GetGLConfigDetail.NewGetGLConfigDetailRow();
                saveGLConfigData.GetGLConfigDetail.Clear();

                dr.ConfigID = Convert.ToInt32(this.GLConfigIDTextBox.Text.Trim());

                if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    if ((this.RollYearTextBox.Text.Length == 4) || (this.RollYearTextBox.Text == "0"))
                    {
                        dr.RollYear = Convert.ToInt16(this.RollYearTextBox.Text.Trim());
                    }
                    else
                    {
                        yearValidationErrors = true;
                    }
                }
                else
                {
                    validationErrors = validationErrors + SharedFunctions.GetResourceString("1101MissingReqFieldYear") + " \n";
                }

                if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()))
                {
                    dr.DistrictID = Convert.ToInt32(this.DistrictTextBox.Text.Trim());
                }

                dr.ConfigDesc = (this.DescriptionTextBox.Text.Trim());

                if (!string.IsNullOrEmpty(this.SubFundTextBox.Text.Trim()))
                {
                    dr.SubFund = this.SubFundTextBox.Text.Trim();
                    dr.SubFundID = this.SubFundId;
                }
                //else
                //{
                //    validationErrors = validationErrors + SharedFunctions.GetResourceString("1500MissingReqSubFund") + " \n";
                //}

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
                    if (yearValidationErrors.Equals(true))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear") + " \n", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    saveGLConfigData.GetGLConfigDetail.Rows.Add(dr);
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(saveGLConfigData.GetGLConfigDetail.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    string tempxml = string.Empty;
                    tempxml = tempDataSet.GetXml();
                    if (string.IsNullOrEmpty(this.GLConfigIDTextBox.Text.Trim()))
                    {
                        errorStatus = this.form1501Controll.WorkItem.F1501_CreateOrEditGLConfigDetails(0, tempxml, TerraScanCommon.UserId);
                    }
                    else
                    {
                        errorStatus = this.form1501Controll.WorkItem.F1501_CreateOrEditGLConfigDetails(Convert.ToInt32(this.GLConfigIDTextBox.Text), tempDataSet.GetXml(), TerraScanCommon.UserId);
                    }

                    this.GLConfigItemsGridView.AllowSorting = true;

                    switch (errorStatus)
                    {
                        case -1:
                            {
                                DialogResult dialogResult = MessageBox.Show(" The General Ledger Configuration Item cannot be saved without a valid SubFund.", "TerraScan T2 - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (dialogResult == DialogResult.OK)
                                {
                                    this.RollYearTextBox.Focus();
                                    return false;
                                }

                                break;
                            }                        

                        case -2:
                            {
                                DialogResult dialogResult = MessageBox.Show("The Current General Ledger Configuration Item cannot be created because it has been associated with a SubFund for a different Roll Year.", "TerraScan T2 - Invalid SubFund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (dialogResult == DialogResult.OK)
                                {
                                    this.RollYearTextBox.Focus();
                                    return false;
                                }

                                break;
                            }

                        case -3:
                            {
                                DialogResult dialogResult = MessageBox.Show("The current General Ledger Account cannot be saved because another Account record already eists with these values.", "TerraScan T2 - Invalid Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (dialogResult == DialogResult.OK)
                                {
                                    return false;
                                }

                                break;
                            }

                        default:
                            {
                                this.FillGLitemItemBoxes(errorStatus);
                                int itemIndex = -1;
                                this.PopulateGLConfigDetailsGridview();
                                TerraScanCommon.SetDataGridViewPosition(this.GLConfigItemsGridView, this.RetrieveRecordIndex(errorStatus));
                                this.SetAutoComplete();
                                break;
                            }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("GLConfigurationSaveMissingreqFileds"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                if (onclose)
                {
                    return true;
                }

                return true;
                //this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Retrieves the index of the record.
        /// </summary>
        /// <param name="tempGLConfigId">The temp GL config id.</param>
        /// <returns>tempIndex</returns>
        private int RetrieveRecordIndex(int tempGLConfigId)
        {
            int tempIndex = -1;
                this.tempDataTable.DefaultView.RowFilter = string.Concat(this.glconfigData.ListGLConfigDetail.GLConfigIDColumn.ColumnName, " = ", tempGLConfigId);

                if (this.tempDataTable.DefaultView.Count > 0)
                {
                    tempIndex = this.tempDataTable.Rows.IndexOf(this.tempDataTable.DefaultView[0].Row);
                }

                return tempIndex;
        }

        #endregion

        /// <summary>
        /// Sets the auto complere.
        /// </summary>
        private void SetAutoComplete()
        {
            F1503GenericManagementData barsList = new F1503GenericManagementData();
            F1503GenericManagementData objectsList = new F1503GenericManagementData();
            F1503GenericManagementData lineLists = new F1503GenericManagementData();
                this.SubFundAutoCompleteSource();

                lineLists = this.form1501Controll.WorkItem.F1501_GetGenericElementMgmt(null, null, "Objects");
                this.AssignAutoCompletSouce(lineLists.GetGenericElementMgmt.Rows, lineLists.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.ObjectTextBox, this.sourceObject);

                this.getdescriptionDataset = this.form1501Controll.WorkItem.F1501_GetFunctionItems(null);
                this.AssignAutoCompletSouce(this.getdescriptionDataset.GetFunctionItems.Rows, this.getdescriptionDataset.GetFunctionItems.FunctionValueColumn.ColumnName, this.FunctionTextBox, this.sourceFunction);

                barsList = this.form1501Controll.WorkItem.F1501_GetGenericElementMgmt(null, null, "Bars");
                this.AssignAutoCompletSouce(barsList.GetGenericElementMgmt.Rows, barsList.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.BarsTextBox, this.sourceBar);

                objectsList = this.form1501Controll.WorkItem.F1501_GetGenericElementMgmt(null, null, "Lines");
                this.AssignAutoCompletSouce(objectsList.GetGenericElementMgmt.Rows, objectsList.GetGenericElementMgmt.ElementKeyNameColumn.ColumnName, this.LineTextBox, this.sourceLine);
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

        /// <summary>
        /// Subs the fund auto complete source.
        /// </summary>
        private void SubFundAutoCompleteSource()
        {
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text))
            {
                this.rollYear = Convert.ToInt16(this.RollYearTextBox.Text.Trim());
                if (this.rollYear != 0)
                {
                    this.sourceSubFund = new AutoCompleteStringCollection();
                    this.subFundManagementData = this.form1501Controll.WorkItem.F9503_GetSubFundItems(null, this.rollYear);
                    this.AssignAutoCompletSouce(this.subFundManagementData.getSubFundItems.Rows, this.subFundManagementData.getSubFundItems.SubFundColumn.ColumnName, this.SubFundTextBox, this.sourceSubFund);
                }
            }
        }

        #endregion
    }
}
