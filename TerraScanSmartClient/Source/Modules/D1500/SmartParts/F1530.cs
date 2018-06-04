//--------------------------------------------------------------------------------------------
// <copyright file="F1530.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1530.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Nov 06        Ranjani            Created
// 06 Feb 07        Ranjani            17.1 - 1530 issue fixed
// 05 Mar 06        RANJANI            Code Review Issue Fixed
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
    using System.Diagnostics;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Text.RegularExpressions;
    using System.Data.SqlClient;

    /// <summary>
    /// Form F1530
    /// </summary>
    [SmartPart]
    public partial class F1530 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// F1530Controller Variable
        /// </summary>
        private F1530Controller form1530Controll;

        /// <summary>
        /// DataSet Contains institution Details - IDs and institution, cash account, institution contact details
        /// </summary>
        private F1530CashAccountManagementData cashAccountManagement = new F1530CashAccountManagementData();

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart = new AdditionalOperationSmartPart();

        /// <summary>
        /// operationSmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart = new OperationSmartPart();

        /// <summary>
        /// institutionId variable is used to store institution id. - default value - '-999'(invalid value)
        /// </summary>       
        private int institutionId = -999;

        /// <summary>
        /// institutionName variable is used pass the institution name to the calling subform
        /// </summary>       
        private string institutionName = String.Empty;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// pageLoadStatus variable is used to find the pageLoadStatus - default true. 
        /// </summary>   
        private bool pageLoadStatus = true;

        private static string tempValidUrl = string.Empty;
        private static readonly string ieExplorer = "IEXPLORE.EXE";

        /// <summary>
        /// registerId variable is used to store registerId. - default value - '-999'(invalid value)
        /// </summary>   
        private int registerId = -999;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1530"/> class.
        /// </summary>
        public F1530()
        {
            this.InitializeComponent();
            ////SetMaxLength for editable fields   
            this.SetMaxLength();
            ////Customize AccountsGridView and ContactsGridView
            this.CustomizeDataGridView();
            ////Customize institution and active combobox
            this.CustomizeCombobox();
            ////give color and shape to the institution combo section, rgb value - 0, 0 , 0 (black)
            this.InstitutionListPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.InstitutionListPictureBox.Height, this.InstitutionListPictureBox.Width, String.Empty, 0, 0, 0);
            ////give color and shape to the institution section, rgb value - 28, 81 , 128 
            this.InstitutionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.InstitutionPictureBox.Height, this.InstitutionPictureBox.Width, SharedFunctions.GetResourceString("InstitutionSectionName"), 28, 81, 128);
            ////give color and shape to the Accounts section, rgb value - 174, 150, 94
            this.AccountsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AccountsPictureBox.Height, this.AccountsPictureBox.Width, SharedFunctions.GetResourceString("AccountsSectionName"), 174, 150, 94);
            ////give color and shape to the Contact section, rgb value - 0, 51, 0
            this.ContactPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContactPictureBox.Height, this.ContactPictureBox.Width, SharedFunctions.GetResourceString("ContactsSectionName"), 0, 51, 0);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Set Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Properties

        /// <summary>
        /// Gets or sets the 1530 control.
        /// </summary>
        /// <value>The 1530 control.</value>
        [CreateNew]
        public F1530Controller Form1530Control
        {
            get { return this.form1530Controll as F1530Controller; }
            set { this.form1530Controll = value; }
        }

        /// <summary>
        /// Gets or sets the institution id.
        /// </summary>
        /// <value>The institution id.</value>
        private int InstitutionId
        {
            get
            {
                return this.institutionId;
            }

            set
            {
                this.institutionId = value;
                ////sets additionalOperationSmartPart keyid - required for attachment and comment
                if (this.institutionId == -999)
                {
                    this.additionalOperationSmartPart.KeyId = -1;
                    this.registerId = -999;
                }
                else
                {
                    this.additionalOperationSmartPart.KeyId = this.institutionId;
                }
            }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get { return this.pageMode; }
            set { this.pageMode = value; }
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
                this.form1530Controll.WorkItem.State["FormStatus"] = this.CheckPageStatus(true);
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
                    this.CancelInstitutionButton_Click();
                    break;
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
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
            {
                if (this.CheckPageStatus(false))
                {
                    ////send register id to get institution id
                    this.cashAccountManagement = this.form1530Controll.WorkItem.F1531_GetCashAccountDetail(Convert.ToInt32(optionalParams[0]));
                    if (this.cashAccountManagement.GetCashAccount.Rows.Count > 0 && !string.IsNullOrEmpty(this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.InstitutionIDColumn].ToString()))
                    {
                        this.institutionId = Convert.ToInt32(this.cashAccountManagement.GetCashAccount.Rows[0][this.cashAccountManagement.GetCashAccount.InstitutionIDColumn]);
                        this.registerId = Convert.ToInt32(optionalParams[0]);
                    }

                    ////reload form
                    this.FillInstitutionDetails(true);
                }
            }
        }

        #endregion Events Subscription

        #region Static Methods

        /// <summary>
        /// retrieves the row index depending on the key value
        /// </summary>
        /// <param name="keyValue">The key value - which is used to find index.</param>
        /// <param name="dataColumn">The data column - used to retrieve the columnname.</param>
        /// <param name="listDataTable">The main data table.</param>
        /// <returns>index of the row to be selected</returns>
        private static int RetrieveGridRowIndex(int keyValue, DataColumn dataColumn, DataTable listDataTable)
        {
            int rowIndex = 0;
            if (keyValue != -999)
            {
                DataTable tempDataTable = listDataTable.Copy();
                tempDataTable.DefaultView.RowFilter = string.Concat(dataColumn.ColumnName, "=", keyValue);

                if (tempDataTable.DefaultView.Count > 0)
                {
                    rowIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
                    if (rowIndex < 0)
                    {
                        rowIndex = 0;
                    }
                }
            }

            return rowIndex;
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1530 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1530_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                ////LOad form with data
                this.FillInstitutionDetails(true);
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
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            ////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
            if (this.Form1530Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1530Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1530Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { this.AccessibleName, string.Empty }));

            // To Load OperationSmartPart into OperationSmartPartWorkSpace
            if (this.Form1530Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.Form1530Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = this.Form1530Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.OperationSmartPartWorkSpace.Show(this.operationSmartPart);

            // To Load AdditionalOperationSmartPart into AddtionalOperationDeckWorkspace
            if (this.Form1530Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.Form1530Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart);
            }
            else
            {
                this.additionalOperationSmartPart = this.Form1530Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
            }

            this.AddtionalOperationDeckWorkspace.Show(this.additionalOperationSmartPart);

            ////set required variable - attachment and comment
            this.additionalOperationSmartPart.ParentWorkItem = this.Form1530Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
            ////set visible property to the controls
            this.operationSmartPart.DeleteButtonVisible = false;
        }

        #endregion

        #region Private Methods

        #region Institution - Get And Retrieve Method

        /// <summary>
        /// Fills the Institution form details.
        /// </summary>
        /// <param name="onload">if set to <c>true</c> [onload].</param>
        private void FillInstitutionDetails(bool onload)
        {
            this.Cursor = Cursors.WaitCursor;
            ////set pageLoadStatus - suppress textchanged event
            this.pageLoadStatus = true;
            ////set page mode
            this.PageMode = TerraScanCommon.PageModeTypes.View;
            ////rset action buttons
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            ////refresh form record set - default load Institution ids and details     
            this.cashAccountManagement.Clear();
            this.cashAccountManagement.Merge(this.form1530Controll.WorkItem.F1530_GetInstitutionDetail(this.institutionId));

            if (this.cashAccountManagement.ListInstitution.Rows.Count > 0)
            {
                ////onload is true then load combobox
                if (onload)
                {
                    ////customize instituttion combobox - binds this.cashAccountManagement.ListInstitution to InstitutionComboBox
                    this.InstitutionComboBox.DataSource = this.cashAccountManagement.ListInstitution.Copy();
                    this.InstitutionComboBox.ValueMember = this.cashAccountManagement.ListInstitution.InstitutionIDColumn.ToString();
                    this.InstitutionComboBox.DisplayMember = this.cashAccountManagement.ListInstitution.InstitutionColumn.ToString();
                }

                ////enable button - count > zero
                this.ReportButton.Enabled = true;
                ////get current institution id - assign institutionid
                if (this.institutionId == -999)
                {
                    ////this.InstitutionComboBox.SelectedValue datatype is integer and assigning the value
                    this.InstitutionId = Convert.ToInt32(this.InstitutionComboBox.SelectedValue);
                }

                ////fill institution related fields
                this.GetInstitutionDetails();
            }
            else
            {
                this.InstitutionId = -999;
                this.ClearInstitutionDetail();
            }
            ////reset pageLoadStatus - trigger textchanged event
            this.pageLoadStatus = false;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Gets the institution details and fill accounts and contacts list.
        /// </summary>        
        private void GetInstitutionDetails()
        {
            ////gets the Institution detail and fill Institution detail and contacts, accounts item
            if (this.cashAccountManagement.GetInstitution.Rows.Count > 0)
            {
                ////check for institutionid and the values retrieved - not equal set institution combo
                if (!this.institutionId.Equals(this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.InstitutionIDColumn]))
                {
                    this.InstitutionComboBox.SelectedValue = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.InstitutionIDColumn];
                    ////if not exists in the institution list clears the fields
                    if (this.InstitutionComboBox.SelectedIndex < 0)
                    {
                        this.ClearInstitutionDetail();
                        this.InstitutionId = -999;
                        return;
                    }

                    ////sets newly selected value
                    this.InstitutionId = Convert.ToInt32(this.InstitutionComboBox.SelectedValue);
                }
                else
                {
                    ////reset value with institution id
                    this.InstitutionComboBox.SelectedValue = this.institutionId;
                }

                ////Enable institution ComboBox
                this.InstitutionComboBox.Enabled = true;
                ////Enable institution panel
                this.InstitutionPanel.Enabled = true;
                ////Fill Institution 
                this.InstitutionIdTextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.InstitutionIDColumn].ToString();
                this.InstitutionNameTextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.InstitutionNameColumn].ToString();
                ////institution name - used to pass the value to the calling form
                this.institutionName = this.InstitutionNameTextBox.Text;
                this.ActiveComboBox.SelectedValue = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.IsActiveColumn];
                this.AddressLine1TextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.Address1Column].ToString();
                this.AddressLine2TextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.Address2Column].ToString();
                this.CityTextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.CityColumn].ToString();
                this.StateTextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.StateColumn].ToString();
                this.ZipTextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.ZipCodeColumn].ToString();
                this.PhoneNumberTextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.PhoneNumberColumn].ToString();
                this.WebsiteTextBox.Text = this.cashAccountManagement.GetInstitution.Rows[0][this.cashAccountManagement.GetInstitution.WebsiteColumn].ToString();
                ////refresh Account items
                this.PopulateAccountsGrid(false);
                ////refresh Contacts items
                this.PopulateContactsGrid(false, -999);
                ////enable audit link label
                this.InstitutionAuditlinkLabel.Text = string.Concat(SharedFunctions.GetResourceString("InstitutionAuditText"), this.institutionId);
                this.InstitutionAuditlinkLabel.Enabled = true;
                ////Used to set record count of attachment and comments.
                this.AddtionalOperationDeckWorkspace.Enabled = true;
                this.SetAdditionalOperationCount(true);
                ////sets permission to fields
                this.SetFieldsPermission();
            }
            else
            {
                this.ClearInstitutionDetail();
            }
        }

        #endregion

        #region Clear Institution Detail

        /// <summary>
        /// Method will Clear the PaymentEngine DataGrid
        /// </summary>
        private void ClearInstitutionDetail()
        {
            ////clear institution related fields
            this.ClearInstitutionSection();
            ////disable form if record not valid
            this.InstitutionPanel.Enabled = false;
            if (this.cashAccountManagement.ListInstitution.Rows.Count == 0)
            {
                ////clear combo box
                this.InstitutionComboBox.DataSource = null;
                ////disable institution combo if record not exists                
                this.InstitutionComboBox.Enabled = false;
                this.ReportButton.Enabled = false;
            }
            else
            {
                ////enable institution combo if record exists                
                this.InstitutionComboBox.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the institution section and other related fields.
        /// </summary>
        private void ClearInstitutionSection()
        {
            ////InsitutionDetail Section
            this.InstitutionIdTextBox.Text = String.Empty;
            this.InstitutionNameTextBox.Text = String.Empty;
            this.ActiveComboBox.SelectedIndex = -1;
            this.AddressLine1TextBox.Text = String.Empty;
            this.AddressLine2TextBox.Text = String.Empty;
            this.CityTextBox.Text = String.Empty;
            this.StateTextBox.Text = String.Empty;
            this.ZipTextBox.Text = String.Empty;
            this.PhoneNumberTextBox.Text = String.Empty;
            this.WebsiteTextBox.Text = String.Empty;
            ////Accounts Section         
            this.cashAccountManagement.ListCashAccount.Clear();
            this.AccountsGridView.DataSource = this.cashAccountManagement.ListCashAccount;
            this.AccountsVscrollBar.Visible = true;
            ////contacts Section      
            this.cashAccountManagement.ListInstitutionContact.Clear();
            this.ContactsGridView.DataSource = this.cashAccountManagement.ListInstitutionContact;
            this.ContactvScrollBar.Visible = true;
            ////reset attachment and comments
            this.AddtionalOperationDeckWorkspace.Enabled = false;
            this.SetAdditionalOperationCount(false);
            ////clear audit link
            this.InstitutionAuditlinkLabel.Text = SharedFunctions.GetResourceString("InstitutionAuditText");
            this.InstitutionAuditlinkLabel.Enabled = false;
            ////disable control specific to new and invalid state
            this.AccountsPanel.Enabled = false;
            this.ContactsPanel.Enabled = false;
        }

        #endregion

        #region User Defined Methods

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizeDataGridView
        /// </summary>
        private void CustomizeDataGridView()
        {
            ////customize accounts grid
            this.AccountsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.AccountsGridView.Columns;

            columns["RegisterId"].DataPropertyName = this.cashAccountManagement.ListCashAccount.RegisterIDColumn.ColumnName;
            columns["AccountName"].DataPropertyName = this.cashAccountManagement.ListCashAccount.AccountNameColumn.ColumnName;
            columns["SubFund"].DataPropertyName = this.cashAccountManagement.ListCashAccount.SubFundColumn.ColumnName;
            columns["Default"].DataPropertyName = this.cashAccountManagement.ListCashAccount.IsDefaultColumn.ColumnName;
            columns["Balance"].DataPropertyName = this.cashAccountManagement.ListCashAccount.BalanceColumn.ColumnName;

            columns["RegisterId"].DisplayIndex = 0;
            columns["AccountName"].DisplayIndex = 1;
            columns["SubFund"].DisplayIndex = 2;
            columns["Default"].DisplayIndex = 3;
            columns["Balance"].DisplayIndex = 4;

            ////customize contacts grid
            this.ContactsGridView.AutoGenerateColumns = false;
            columns = this.ContactsGridView.Columns;

            columns["ContactId"].DataPropertyName = this.cashAccountManagement.ListInstitutionContact.ContactIDColumn.ColumnName;
            columns["ContactName"].DataPropertyName = this.cashAccountManagement.ListInstitutionContact.NameColumn.ColumnName;
            columns["Title"].DataPropertyName = this.cashAccountManagement.ListInstitutionContact.TitleColumn.ColumnName;
            columns["PhoneNumber"].DataPropertyName = this.cashAccountManagement.ListInstitutionContact.PhoneColumn.ColumnName;

            columns["ContactId"].DisplayIndex = 0;
            columns["ContactName"].DisplayIndex = 1;
            columns["Title"].DisplayIndex = 2;
            columns["PhoneNumber"].DisplayIndex = 3;
        }

        /// <summary>
        /// Populates the contacts grid.
        /// </summary>
        /// <param name="loadContacts">if set to <c>true</c> [load contacts] - load contacts retrieve values from the database.</param>
        /// <param name="contactId">The contact id.- used to set default contact.</param>
        private void PopulateContactsGrid(bool loadContacts, int contactId)
        {
            this.ContactsPanel.Enabled = true;
            if (loadContacts)
            {
                this.cashAccountManagement.ListInstitutionContact.Clear();
                this.cashAccountManagement.ListInstitutionContact.Merge(this.form1530Controll.WorkItem.F1530_GetInstitutionDetail(this.institutionId).ListInstitutionContact);
            }

            this.ContactsGridView.DataSource = this.cashAccountManagement.ListInstitutionContact.DefaultView;
            if (this.cashAccountManagement.ListInstitutionContact.Rows.Count > this.ContactsGridView.NumRowsVisible)
            {
                this.ContactvScrollBar.Visible = false;
            }
            else
            {
                this.ContactvScrollBar.Visible = true;
            }

            if (this.ContactsGridView.OriginalRowCount > 0)
            {
                this.ContactsGridView.Enabled = true;
                this.ViewContactButton.Enabled = true;
                ////used to find rowindex using ids for select
                int rowIndex = RetrieveGridRowIndex(contactId, this.cashAccountManagement.ListInstitutionContact.ContactIDColumn, this.cashAccountManagement.ListInstitutionContact);

                ////check for current cell
                if (rowIndex < this.ContactsGridView.OriginalRowCount)
                {
                    this.ContactsGridView.CurrentCell = this.ContactsGridView["ContactId", rowIndex];
                    this.ContactsGridView.FirstDisplayedScrollingRowIndex = rowIndex;
                }
                else
                {
                    this.ContactsGridView.CurrentCell = this.ContactsGridView.FirstDisplayedCell;
                    rowIndex = this.ContactsGridView.CurrentCell.RowIndex;
                }

                ////select current cell
                this.ContactsGridView.Rows[rowIndex].Selected = true;
            }
            else
            {
                this.ContactsGridView.Enabled = false;
                this.ViewContactButton.Enabled = false;
            }
        }

        /// <summary>
        /// Populates the accounts grid.
        /// </summary>
        /// <param name="loadAccounts">if set to <c>true</c> [load accounts]- load accounts retrieve values from the database.</param>
        private void PopulateAccountsGrid(bool loadAccounts)
        {
            this.AccountsPanel.Enabled = true;
            if (loadAccounts)
            {
                this.cashAccountManagement.ListCashAccount.Clear();
                this.cashAccountManagement.ListCashAccount.Merge(this.form1530Controll.WorkItem.F1530_GetInstitutionDetail(this.institutionId).ListCashAccount);
            }

            this.AccountsGridView.DataSource = this.cashAccountManagement.ListCashAccount.DefaultView;
            if (this.cashAccountManagement.ListCashAccount.Rows.Count > this.AccountsGridView.NumRowsVisible)
            {
                this.AccountsVscrollBar.Visible = false;
            }
            else
            {
                this.AccountsVscrollBar.Visible = true;
            }

            int rowIndex = 0;
            if (this.AccountsGridView.OriginalRowCount > 0)
            {
                this.AccountsGridView.Enabled = true;
                this.ViewAccountButton.Enabled = true;

                ////used to find rowindex using ids for select
                rowIndex = RetrieveGridRowIndex(this.registerId, this.cashAccountManagement.ListCashAccount.RegisterIDColumn, this.cashAccountManagement.ListCashAccount);

                ////check for current cell
                if (rowIndex < this.AccountsGridView.OriginalRowCount)
                {
                    this.AccountsGridView.CurrentCell = this.AccountsGridView["RegisterId", rowIndex];
                    this.AccountsGridView.FirstDisplayedScrollingRowIndex = rowIndex;
                }
                else
                {
                    this.AccountsGridView.CurrentCell = this.AccountsGridView.FirstDisplayedCell;
                    rowIndex = this.AccountsGridView.CurrentCell.RowIndex;
                }

                ////select current cell
                this.AccountsGridView.Rows[rowIndex].Selected = true;
            }
            else
            {
                this.ViewAccountButton.Enabled = false;
                this.AccountsGridView.Enabled = false;
            }
        }

        /// <summary>
        /// This Method used to bind datasource and displaymember
        /// CustomizeCombobox
        /// </summary>
        private void CustomizeCombobox()
        {
            ////customize active combobox
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.ActiveComboBox.DataSource = commonData.ComboBoxDataTable;
            this.ActiveComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.ActiveComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [on close].</param>
        /// <returns>true - for continuing/false - leave unsaved changes</returns>
        private bool CheckPageStatus(bool onclose)
        {
            DialogResult dialogResult;
            bool returnValue = false;

            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    returnValue = this.SaveInstitutionRecord(onclose);

                    return returnValue;
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (!onclose)
                    {
                        this.CancelInstitutionButton_Click();
                    }

                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the CellFormatting event of the AccountsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void AccountsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            //// Only paint if desired, formattable column
            if (e.ColumnIndex == this.AccountsGridView.Columns["Balance"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                /* Only paint if text provided
                 Only paint if desired text is in cell */
                if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
                {
                    string val = e.Value.ToString();
                    if (Decimal.TryParse(val, out outDecimal))
                    {
                        if (outDecimal.ToString().Contains("-"))
                        {
                            ////color change for negative amount
                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                            e.CellStyle.ForeColor = Color.FromArgb(0, 128, 0);
                        }
                        else
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.CellStyle.ForeColor = Color.Black;
                        }

                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "0.00";
                    }
                }
                else
                {
                    e.Value = "";
                }
            }
        }

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        /// <param name="onload">if set to <c>true</c> [onload].</param>
        private void SetAdditionalOperationCount(bool onload)
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       

            if (this.additionalOperationSmartPart != null)
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                if (onload && this.institutionId != -999)
                {
                    additionalOperationCountEntity.AttachmentCount = this.form1530Controll.WorkItem.GetAttachmentCount(this.ParentFormId, this.institutionId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1530Controll.WorkItem.GetCommentsCount(this.ParentFormId, this.institutionId, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
        }

        /// <summary>
        /// Sets the fields permission - set edit or new permission.
        /// </summary>
        private void SetFieldsPermission()
        {
            bool permissionFields = false;
            ////set permission depending on page mode
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                permissionFields = !this.PermissionFiled.newPermission;
            }
            else
            {
                permissionFields = !this.PermissionEdit;
            }

            ////lock InsitutionDetail Section            
            this.InstitutionNameTextBox.LockKeyPress = permissionFields;
            this.AddressLine1TextBox.LockKeyPress = permissionFields;
            this.AddressLine2TextBox.LockKeyPress = permissionFields;
            this.CityTextBox.LockKeyPress = permissionFields;
            this.StateTextBox.LockKeyPress = permissionFields;
            this.ZipTextBox.LockKeyPress = permissionFields;
            this.PhoneNumberTextBox.LockKeyPress = permissionFields;
            this.WebsiteTextBox.LockKeyPress = permissionFields;
            this.ActiveComboBox.Enabled = !permissionFields;
            ////enable or disable add
            this.AddAccountButton.Enabled = this.PermissionFiled.newPermission;
            this.AddContactButton.Enabled = this.PermissionFiled.newPermission;
        }

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.InstitutionNameTextBox.MaxLength = this.cashAccountManagement.GetInstitution.InstitutionNameColumn.MaxLength;
            this.AddressLine1TextBox.MaxLength = this.cashAccountManagement.GetInstitution.Address1Column.MaxLength;
            this.AddressLine2TextBox.MaxLength = this.cashAccountManagement.GetInstitution.Address2Column.MaxLength;
            this.CityTextBox.MaxLength = this.cashAccountManagement.GetInstitution.CityColumn.MaxLength;
            this.StateTextBox.MaxLength = this.cashAccountManagement.GetInstitution.StateColumn.MaxLength;
            this.ZipTextBox.MaxLength = this.cashAccountManagement.GetInstitution.ZipCodeColumn.MaxLength;
            this.PhoneNumberTextBox.MaxLength = this.cashAccountManagement.GetInstitution.PhoneNumberColumn.MaxLength;
            this.WebsiteTextBox.MaxLength = this.cashAccountManagement.GetInstitution.WebsiteColumn.MaxLength;
        }

        /// <summary>
        /// Handles the ValueChanged event of the EditControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditControl_ValueChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.PageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.InstitutionComboBox.Enabled = false;
            }
        }

        #endregion

        #region New

        /// <summary>
        /// News the button_ click - prepare form for inserting new record.
        /// </summary>
        private void NewButton_Click()
        {
            ////reset pageLoadStatus - trigger textchanged event
            this.pageLoadStatus = true;
            this.PageMode = TerraScanCommon.PageModeTypes.New;
            ////reset values
            this.InstitutionId = -999;
            ////set default buttons
            this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
            ////clears fields for new mode and disable required control
            this.ClearInstitutionSection();
            ////reset institution combobox 
            this.InstitutionComboBox.SelectedIndex = -1;
            ////enable/disavle necessary control
            this.InstitutionPanel.Enabled = true;
            ////set permission
            this.SetFieldsPermission();
            this.InstitutionComboBox.Enabled = false;
            ////Set default value
            this.ActiveComboBox.SelectedValue = "1";
            ////focus to the first control
            this.InstitutionNameTextBox.Focus();
            ////reset pageLoadStatus - trigger textchanged event
            this.pageLoadStatus = false;
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            this.SaveInstitutionRecord(false);
        }

        /// <summary>
        /// Saves the excise rate record.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>bool value true or false </returns>
        private bool SaveInstitutionRecord(bool onclose)
        {
            ////Check For Required Fields
            if (String.IsNullOrEmpty(this.InstitutionNameTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InstitutionNameTextBox.Focus();
                return false;
            }

            if (this.ActiveComboBox.SelectedIndex < 0)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveComboBox.Focus();
                return false;
            }

            this.Cursor = Cursors.WaitCursor;
            ////get values from ui
            this.cashAccountManagement.SaveInstitution.Rows.Clear();
            F1530CashAccountManagementData.SaveInstitutionRow cashAccountManagementDataRow = this.cashAccountManagement.SaveInstitution.NewSaveInstitutionRow();

            cashAccountManagementDataRow.InstitutionName = this.InstitutionNameTextBox.Text.Trim();
            ////this.ActiveComboBox.SelectedValue datatype is bool
            cashAccountManagementDataRow.IsActive = Convert.ToByte(this.ActiveComboBox.SelectedValue);
            cashAccountManagementDataRow.Address1 = this.AddressLine1TextBox.Text.Trim();
            cashAccountManagementDataRow.Address2 = this.AddressLine2TextBox.Text.Trim();
            cashAccountManagementDataRow.City = this.CityTextBox.Text.Trim();
            cashAccountManagementDataRow.State = this.StateTextBox.Text.Trim();
            cashAccountManagementDataRow.ZipCode = this.ZipTextBox.Text.Trim();
            cashAccountManagementDataRow.PhoneNumber = this.PhoneNumberTextBox.Text.Trim();
            cashAccountManagementDataRow.Website = this.WebsiteTextBox.Text.Trim();

            this.cashAccountManagement.SaveInstitution.Rows.Add(cashAccountManagementDataRow);

            ////Save Institution record - if -999 then insert else update, validated in dal - returns saved/updated institution id
            this.InstitutionId = this.form1530Controll.WorkItem.F1530_SaveInstitution(this.institutionId, Utility.GetXmlString(this.cashAccountManagement.SaveInstitution.Copy()), TerraScanCommon.UserId);
            ////reset ids for calling form                
            this.registerId = -999;

            if (onclose)
            {
                return true;
            }

            ////reload institution
            this.FillInstitutionDetails(true);
            ////sets focus
            this.InstitutionComboBox.Focus();

            return true;
            this.Cursor = Cursors.Default;
        }

        #endregion

        #region Cancel

        /// <summary>
        /// Cancels the institution - button_ click.
        /// </summary>
        private void CancelInstitutionButton_Click()
        {
            ////reset ids for calling form            
            this.registerId = -999;
            ////repopulate institution details
            this.FillInstitutionDetails(true);
            ////sets focus
            this.InstitutionComboBox.Focus();
        }

        #endregion

        #region Links

        /// <summary>
        /// Handles the LinkClicked event of the InstitutionAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void InstitutionAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.institutionId > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(90101);
                formInfo.optionalParameters = new object[2];
                formInfo.optionalParameters[0] = this.Tag;
                formInfo.optionalParameters[1] = this.institutionId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }

            ////this.Cursor = Cursors.WaitCursor;
            ////////check for validity
            ////if (this.institutionId != -999)
            ////{
            ////    ////// calling  Common Function For Report
            ////    Hashtable reportOptionalParameter = new Hashtable();                    
            ////    reportOptionalParameter.Add("InstitutionId", this.institutionId);
            ////    TerraScanCommon.ShowReport(153090, Report.ReportType.Preview, reportOptionalParameter);
            ////}
            this.Cursor = Cursors.Default;
        }

        #endregion

        #region Accounts Action Buttons

        /// <summary>
        /// Handles the Click event of the AddAccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.institutionId != -999)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////CAsh Account Detail Form - FormID - 1531
                    Form cashAccountForm = this.form1530Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1531, new object[] { this.ParentFormId, TerraScanCommon.PageModeTypes.New, this.institutionId, -999, this.institutionName, this.AccountsGridView.OriginalRowCount }, this.form1530Controll.WorkItem);
                    ////open form in new mode
                    if (cashAccountForm != null && cashAccountForm.ShowDialog() == DialogResult.Yes)
                    {
                        this.registerId = Convert.ToInt32(TerraScanCommon.GetValue(cashAccountForm, "RegisterId"));
                        ////refresh the accounts grid
                        this.PopulateAccountsGrid(true);
                    }
                }
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
        /// Handles the CellDoubleClick event of the AccountsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AccountsGridView_CellDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////row clicked
                if (e.RowIndex >= 0)
                {
                    ////call subform in view mode - call view button click
                    this.ViewAccountMethod();
                }
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
        /// Handles the Click event of the ViewAccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ViewAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ViewAccountMethod();
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
        /// Views the account method.
        /// </summary>
        private void ViewAccountMethod()
        {
            ////check for valid value
            if (this.institutionId != -999 && this.AccountsGridView.CurrentCell != null)
            {
                ////set register id
                this.registerId = Convert.ToInt32(this.AccountsGridView["RegisterId", this.AccountsGridView.CurrentCell.RowIndex].Value);

                ////Institution Contact Detail Form - FormID - 1532
                Form cashAccountForm = this.form1530Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1531, new object[] { this.ParentFormId, TerraScanCommon.PageModeTypes.View, this.institutionId, this.registerId, this.institutionName, this.AccountsGridView.OriginalRowCount }, this.form1530Controll.WorkItem);
                ////open form in view mode - possible to edit
                if (cashAccountForm != null && cashAccountForm.ShowDialog() == DialogResult.Yes)
                {
                    ////refresh the accounts grid
                    this.PopulateAccountsGrid(true);
                }
                else
                {
                    ////reset register id
                    this.registerId = -999;
                }
            }
        }

        #endregion

        #region Contacts Action Buttons

        /// <summary>
        /// Handles the Click event of the AddContactButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddContactButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.institutionId != -999)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Institution Contact Detail Form - FormID - 1532
                    Form cantactForm = this.form1530Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1532, new object[] { this.ParentFormId, TerraScanCommon.PageModeTypes.New, this.institutionId, -999, this.institutionName }, this.form1530Controll.WorkItem);
                    ////open form in new mode
                    if (cantactForm != null && cantactForm.ShowDialog() == DialogResult.Yes)
                    {
                        ////refresh the contacts grid
                        this.PopulateContactsGrid(true, Convert.ToInt32(TerraScanCommon.GetValue(cantactForm, "ContactId")));
                    }
                }
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
        /// Handles the CellMouseDoubleClick event of the ContactsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ContactsGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////row clicked
                if (e.RowIndex >= 0)
                {
                    ////call subform in view mode - call view contact method
                    this.ViewContactMethod();
                }
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
        /// Handles the Click event of the ViewContactButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ViewContactButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ViewContactMethod();
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
        /// Views the contact method.
        /// </summary>
        private void ViewContactMethod()
        {
            ////check for valid value
            if (this.institutionId != -999 && this.ContactsGridView.CurrentCell != null)
            {
                ////set contactid
                int contactId = Convert.ToInt32(this.ContactsGridView["ContactId", this.ContactsGridView.CurrentCell.RowIndex].Value);

                ////Institution Contact Detail Form - FormID - 1532
                Form cantactForm = this.form1530Controll.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1532, new object[] { this.ParentFormId, TerraScanCommon.PageModeTypes.View, this.institutionId, contactId, this.institutionName }, this.form1530Controll.WorkItem);
                ////open form in view mode - possible to edit
                if (cantactForm != null && cantactForm.ShowDialog() == DialogResult.Yes)
                {
                    ////refresh the contacts grid
                    this.PopulateContactsGrid(true, contactId);
                }
            }
        }

        #endregion

        #region Institution Combo events and methods

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the InstitutionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstitutionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ////check for load status
                if (!this.pageLoadStatus)
                {
                    ////load the form with new institution id
                    this.LoadWithInstitutionComboValue();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the InstitutionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void InstitutionComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ////check for load status and check whether same id exist
                if (!this.pageLoadStatus && !this.institutionId.Equals(this.InstitutionComboBox.SelectedValue))
                {
                    ////load the form with new institution id
                    this.LoadWithInstitutionComboValue();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the with institution combo value.
        /// </summary>
        private void LoadWithInstitutionComboValue()
        {
            ////true load institution combobox
            bool loadCombo = false;
            this.pageLoadStatus = true;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                loadCombo = true;
                if (!this.CheckPageStatus(true))
                {
                    this.InstitutionComboBox.SelectedValue = this.institutionId;
                    this.pageLoadStatus = false;
                    return;
                }
            }

            this.InstitutionId = Convert.ToInt32(this.InstitutionComboBox.SelectedValue);
            ////reset ids for calling form                
            this.registerId = -999;
            ////reload with new institution id
            this.FillInstitutionDetails(loadCombo);
        }

        #endregion

        #region Web Site

        /// <summary>
        /// Handles the Click event of the WebSiteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WebSiteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.WebsiteTextBox.Text.Trim()))
                {
                    ////start the process with the url
                    this.Cursor = Cursors.WaitCursor;
                    ////processStartInfo creates instance to assign specific property
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    ////launch internet explorer
                    processStartInfo.FileName = "IEXPLORE.EXE";
                    processStartInfo.Arguments = this.WebsiteTextBox.Text.Trim();
                    processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    Process.Start(processStartInfo);
                }
                //if (!string.IsNullOrEmpty(this.WebsiteTextBox.Text.Trim()))
                //{
                //    tempValidUrl = TerraScanCommon.ValidateUrl(this.WebsiteTextBox.Text.Trim());
                //}
                //if (!String.IsNullOrEmpty(tempValidUrl))
                //{
                //    this.Cursor = Cursors.WaitCursor;
                //    SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorerClass();
                //    IWebBrowserApp wb = (IWebBrowserApp)ie;
                //    wb.Visible = true;
                //    wb.FullScreen = true;
                //    object o = null;
                //    wb.Navigate(tempValidUrl, ref o, ref o, ref o, ref o);

                //    //Commented to implement TFS#20236
                //    //ProcessStartInfo processStartInfo = new ProcessStartInfo();
                //    //processStartInfo.FileName = ieExplorer;
                //    //processStartInfo.Arguments = tempValidUrl;
                //    //processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                //    //Process.Start(processStartInfo);
                //}
                //else
                //{
                //    MessageBox.Show("An input parameter is invalid", "Terrascan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    this.WebsiteTextBox.Focus();
                //}

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("WebSiteValidation"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                this.WebsiteTextBox.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Report

        /// <summary>
        /// Handles the Click event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //// calling  Common Function For Report    
                TerraScanCommon.ShowReport(153010, Report.ReportType.Preview);
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

        #endregion

        #endregion

    }
}
