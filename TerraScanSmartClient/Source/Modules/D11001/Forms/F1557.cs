//--------------------------------------------------------------------------------------------
// <copyright file="F1557.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1555.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/09/2016    Priyadharshini.R       Created
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;
    using System.Data;
    using System.Drawing;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.CompositeUI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Partial class for F1557
    /// </summary>
    public partial class F1557 : Form
    {
        #region Variables

        /// <summary>
        /// recordCount Local variable.
        /// </summary>
        private int recordCount;

        /// <summary>
        /// SelectedCount Local variable.
        /// </summary>
        private int SelectedCount;

        /// <summary>
        /// SelectedCount Local variable.
        /// </summary>
        private DataTable InitialPaymentData;

        #region Enum

        /// <summary>
        /// Enumerator PaymentOption
        /// </summary>
        public enum PaymentOptionTypes
        {
            /// <summary>
            /// MinDue  = 0.
            /// </summary>
            MinDue = 0,

            /// <summary>
            /// Balance = 1.
            /// </summary>
            Balance,

            /// <summary>
            /// Partial = 2.
            /// </summary>
            Partial
        }

        #endregion

        /// <summary>
        /// Created instance for the Typed Dataset
        /// </summary>
        private F1555_ReceiptDetailsData.ReverseSharedPaymentDataTable loadReceiptDetailsData = new F1555_ReceiptDetailsData.ReverseSharedPaymentDataTable();

        /// <summary>
        /// form1555Control
        /// </summary>
        private F1557Controller form1557Control;

        /// <summary>
        /// variable holds the selectedPaymentIds.
        /// </summary>
        private List<int> selectedPaymentItemIds = new List<int>();

        /// <summary>
        /// variable holds the selected Payment ids xml string.
        /// </summary>
        private string selectedPaymentItemIdsXml = string.Empty;

        /// <summary>
        /// variable holds the selected Payment ids xml string.
        /// </summary>
        private string selectedPaymentValueIdXml = string.Empty;

        /// <summary>
        /// Instance variable to hold the page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance variable to hold the form PaymentgridRowCount 
        /// </summary>
        private int paymentgridRowCount;

        /// <summary>
        /// create variable for paymentoptiontypes
        /// </summary>
        private PaymentOptionTypes paymentOption;

        /// <summary>
        /// flag to identify form load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// Variable Holds the Parent Form Id
        /// </summary>
        private int parentFormId;

        /// <summary>
        /// PaymentManageGridDataTable
        /// </summary>
        private F1557PayamentManagementData payamentManageDataSet = new F1557PayamentManagementData();

        /// <summary>
        /// Payeee details Instance.
        /// </summary>
        private PaymentEngineData payeeDetails = new PaymentEngineData();

        /// <summary>
        ///  Used To Store receiptId
        /// </summary>
        private int receiptId;

        /// <summary>
        /// Instance for F15100
        /// </summary>
        F15104 f15104;


        ///<summary>
        /// Editable in Receipt Grid View
        /// </summary>
        private bool isEdit;

        /// <summary>
        /// Shared Payment ID
        /// </summary>
        private int sharedPaymentId;

        /// <summary>
        /// Assigning Empty to parentWorkItem
        /// </summary>
        private WorkItem parentWorkItem;

        /// <summary>
        /// Temp Balance Variable
        /// </summary>
        private decimal tempbalance = 0;

        private bool CancelItem=false;

        /// <summary>
        /// Paid by Owner Name
        /// </summary>
        private string ownerNamePaidBy=string.Empty;

        /// <summary>
        /// Paid by Owner ID
        /// </summary>
        private string ownerIdPaidBy = string.Empty;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new class.
        /// </summary>
        public F1557()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1557"/> class.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        public F1557(int receiptId,string ownerName,string ownerID)
        {
            InitializeComponent();
            this.receiptId = receiptId;
            this.ownerNamePaidBy = ownerName;
            this.ownerIdPaidBy = ownerID;
        }

        #endregion Constructor


        #region Properties

        /// <summary>
        /// Gets or sets the reverse receipt controll.
        /// </summary>
        /// <value>The reverse receipt template controll.</value>
        [CreateNew]
        public F1557Controller F1557Controll
        {
            get { return this.form1557Control as F1557Controller; }
            set { this.form1557Control = value; }
        }

        /// <summary>
        /// Property Which is Having Get and Set for ParentFormID
        /// </summary>
        public int ParentFormId
        {
            get { return this.parentFormId; }
            set { this.parentFormId = value; }
        }

        #endregion Properties

        #region Event Publication


        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;


        [EventPublication(EventTopicNames.F3201_F32012_FormReload, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> F3201_F32012_FormReload;

        #endregion Event Publication

        #region Protected Methods

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            }
        }
        /// <summary>
        /// event publication to intimate form master about the selected keyid doesnot exists
        /// due to change in the filter option
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_OnChange_Neighborhood, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9033_OnChange_Neighborhood;

        /// <summary>
        /// Declared the event SubFormSave of D35000_F35002
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

        /// <summary>
        /// Called when [D9030_ F9033_ on filter_ key id reset].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_OnChange_Neighborhood(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9033_OnChange_Neighborhood != null)
            {
                this.D9030_F9033_OnChange_Neighborhood(this, eventArgs);
            }
        }

        #endregion Protected Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F1556 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1557_Load(object sender, EventArgs e)
        {
            try
            {
                this.parentWorkItem = this.form1557Control.WorkItem;
                this.CustomizePaymentManageGrid();
                this.PopulateManagePaymentGrid();
                this.PopulateManageTableValues();
                this.flagFormLoad = false;
                EnableSaveCancelButton(false);
                EnableRemoveSelectedButton(false);
                this.Cancel.Enabled = false;
                
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
       
        private void NoButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                    decimal balance = 0;
                    if (Decimal.TryParse(this.BalanceTextBox.Text, NumberStyles.Currency, null, out balance))
                    {
                    }
                    if (balance != 0)
                    {
                        EnableSaveCancelButton(false);
                        return;
                    }
                    DataTable dtNew = payamentManageDataSet.PaymentManageGrid.Copy();
                    dtNew.Columns.Remove("EmptyRecord$");

                    DataSet dsEditedApproach = new DataSet("Root");
                    dsEditedApproach.Tables.Add(dtNew.Copy());
                    dsEditedApproach.Tables[0].TableName = "Table";

                    DataSet dsNewApproach = new DataSet("Root");
                    dsNewApproach.Tables.Add(dtNew.Copy());
                    dsNewApproach.Tables[0].TableName = "Table";

                    for (int i = dsEditedApproach.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        if ((dsEditedApproach.Tables[0].Rows[i]["PPaymentID"]) != DBNull.Value)
                        {
                            // To Add PPaymentID in PaymentManageGrid instead of adding PaymentID.
                            //foreach (DataRow dr in dsEditedApproach.Tables[0].Rows)
                            //{
                            //    dr[this.payamentManageDataSet.PaymentManageGrid.PPaymentIDColumn.ColumnName] = PPaymentIDTextBox.Text;
                            //}
                            dsEditedApproach.Tables[0].Rows[i]["PPaymentID"] = PPaymentIDTextBox.Text;
                        }

                        if ((dsEditedApproach.Tables[0].Rows[i]["Amount"]) != DBNull.Value)
                        {
                            dsEditedApproach.Tables[0].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(dsEditedApproach.Tables[0].Rows[i]["Amount"]), 2);
                        }
                    }
                    for (int i = dtNew.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtNew.Rows[i]["PaymentID"] == DBNull.Value)// && dsEditedApproach.Tables[0].Rows[i]["TenderTypeID"] == DBNull.Value)
                        {
                            dsEditedApproach.Tables[0].Rows[i].Delete();
                        }

                        if (dtNew.Rows[i]["PaymentID"] != DBNull.Value || dtNew.Rows[i]["TenderTypeID"] == DBNull.Value)
                        {
                            dsNewApproach.Tables[0].Rows[i].Delete();
                        }
                    }

                    dsEditedApproach.Tables[0].AcceptChanges();
                    string EditedApproachItemxml = dsEditedApproach.GetXml();

                    this.form1557Control.WorkItem.UpdatePayment(EditedApproachItemxml, TerraScanCommon.UserId);

                    dsNewApproach.Tables[0].AcceptChanges();

                    foreach (DataRow dr in dsNewApproach.Tables[0].Rows)
                    {
                        dr[this.payamentManageDataSet.PaymentManageGrid.PPaymentIDColumn.ColumnName] = PPaymentIDTextBox.Text;
                    }

                    for (int i = dsNewApproach.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        if ((dsNewApproach.Tables[0].Rows[i]["Amount"]) != DBNull.Value)
                        {
                            dsNewApproach.Tables[0].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(dsNewApproach.Tables[0].Rows[i]["Amount"]), 2);
                        }
                    }

                    string IncomeApproachItemxml = dsNewApproach.GetXml();

                    this.form1557Control.WorkItem.InsertPayment(IncomeApproachItemxml, TerraScanCommon.UserId);
                    this.PaymentManagementGridView.AllowSorting = true;
                    SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                    sliceReloadActiveRecord.MasterFormNo = 11001;
                    sliceReloadActiveRecord.SelectedKeyId = this.receiptId;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.D9030_F9033_OnChange_Neighborhood(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.F3201_F32012_FormReload(this, new DataEventArgs<int>(this.receiptId));
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Calculates the selected Payment
        /// </summary>
        private void CalculateSelectAllPayment(bool isChecked)
        {
            try
            {
                this.PaymentManagementGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.paymentgridRowCount; count++)
                {
                    if (isChecked == true)
                    {
                        if (!String.IsNullOrEmpty(this.PaymentManagementGridView[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName, count].Value.ToString()))
                        {
                            this.selectedPaymentItemIds.Add(Convert.ToInt32(this.PaymentManagementGridView[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName, count].Value));
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
        /// Calculates the Unselected Payment.
        /// </summary>
        private void CalculateUnSelectPayment(bool isChecked)
        {
            try
            {
                this.PaymentManagementGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.paymentgridRowCount; count++)
                {
                    if (isChecked == false)
                    {
                        if (!String.IsNullOrEmpty(this.PaymentManagementGridView[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName, count].Value.ToString()))
                        {
                            this.selectedPaymentItemIds.Remove(Convert.ToInt32(this.PaymentManagementGridView[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName, count].Value));
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
        /// Gets the selected Income Source ids to XML .
        /// </summary>
        private void GetSelectedPaymentIdsXml()
        {
            this.selectedPaymentItemIdsXml = string.Empty;
            DataTable tempXMLdataTable = new DataTable();
            foreach (DataColumn column in this.payamentManageDataSet.PaymentManageGrid.Columns)
            {
                if (column.ColumnName == this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName)
                {
                    tempXMLdataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            for (int item = 0; item < this.selectedPaymentItemIds.Count; item++)
            {
                DataRow tempXMLDataRow = tempXMLdataTable.NewRow();
                tempXMLDataRow[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName] = this.selectedPaymentItemIds[item].ToString();
                tempXMLdataTable.Rows.Add(tempXMLDataRow);
            }
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(tempXMLdataTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            this.selectedPaymentItemIdsXml = tempDataSet.GetXml();
        }

        private void YesButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View) // && this.slicePermissionField.deletePermission)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("RemoveRecordPayment"), SharedFunctions.GetResourceString("RemoveRecordPaymentHead"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        GetSelectedPaymentIdsXml();
                        this.form1557Control.WorkItem.F1557_DeletePayment(this.selectedPaymentItemIdsXml, TerraScanCommon.UserId);
                        this.CustomizePaymentManageGrid();
                        this.PopulateManagePaymentGrid();
                        this.PopulateManageTableValues();
                        this.PaymentManagementGridView.RefreshEdit();
                        this.selectedPaymentItemIds = new List<int>();
                        EnableRemoveSelectedButton(false);
                        this.SelectAllCheckBox.Checked = false;
                       // this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        EnableSaveCancelButton(true);
                      
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                //  this.removeEnablesavecancel = false;
            }
        }

        /// <summary>
        /// Selects the un select all and Unselect all for 163 sprint.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SelectUnSelectAll(string status)
        {
            if (this.paymentgridRowCount > 0)
            {
                for (int count = 0; count < this.paymentgridRowCount; count++)
                {
                   this.PaymentManagementGridView.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = status;
                }
            }
        }

        #endregion Events

        /// <summary>
        /// Change the tender type related fields.
        /// </summary>
        /// <param name="combo">The source control.</param>
        private void ChangeTenderTypeRelatedFields(ComboBox combo)
        {
            int rowIndex = 0;

            if (this.PaymentManagementGridView.CurrentCell != null)
            {
                rowIndex = this.PaymentManagementGridView.CurrentCell.RowIndex;
            }

            if (combo.SelectedIndex > 0 && combo.SelectedValue != null)
            {
                if (String.IsNullOrEmpty(this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["TenderType"].ToString()))
                {
                    this.PaymentManagementGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                    
                    //// Paid by owner name if it is first record
                    //if (this.PaymentManagementGridView.CurrentCell != null && this.PaymentManagementGridView.CurrentCell.RowIndex > 0)
                    //{
                    //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["PaidBy"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["PaidBy"];
                    //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address1"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Address1"];
                    //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address2"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Address2"];
                    //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["City"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["City"];
                    //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["State"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["State"];
                    //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Zip"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Zip"];
                    //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Comment"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Comment"];
                    //}
                    //else
                    //{
                        if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                        {
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];
                        }
                    //}
                    //if (!string.IsNullOrEmpty(this.payamentManageDataSet.PaymentManageGrid[0].PaidBy))
                    //{
                        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["PaidBy"] = ownerNamePaidBy.ToString();
                    //}
                    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Amount"] = this.BalanceTextBox.DecimalTextBoxValue;
                }
                else
                {
                    this.PaymentManagementGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                    
                    // Paid by value should change only if it is null 
                    if (string.IsNullOrEmpty(this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["PaidBy"].ToString()))
                    {
                        //if (this.PaymentManagementGridView.CurrentCell != null && this.PaymentManagementGridView.CurrentCell.RowIndex > 0)
                        //{
                        if (!string.IsNullOrEmpty(this.ownerNamePaidBy))
                        {
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["PaidBy"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["PaidBy"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address1"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Address1"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address2"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Address2"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["City"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["City"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["State"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["State"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Zip"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Zip"];
                            this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Comment"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex - 1]["Comment"];
                        }
                        //}
                        //else
                        //{
                        //    this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["PaidBy"] = this.ownerNamePaidBy;
                        //    if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                        //    {
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];
                        //    }
                        //    else
                        //    {
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address1"] = "";
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Address2"] = "";
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["City"] = "";
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["State"] = "";
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Zip"] = "";
                        //        this.payamentManageDataSet.PaymentManageGrid.Rows[rowIndex]["Comment"] = "";
                        //    }
                        //}
                    }
                }

                this.PaymentManagementGridView["PaidBy", rowIndex].ReadOnly = false;
                this.PaymentManagementGridView["CheckNumber", rowIndex].ReadOnly = false;
                this.PaymentManagementGridView["Amount", rowIndex].ReadOnly = false;
                this.Cancel.Enabled = true;
                EnableSaveCancelButton(true);
            }
            else
            {
                this.PaymentManagementGridView["TenderType", rowIndex].Value = DBNull.Value;
                this.PaymentManagementGridView["PaidBy", rowIndex].Value = "";
                this.PaymentManagementGridView["Amount", rowIndex].Value = DBNull.Value;
                this.PaymentManagementGridView["CheckNumber", rowIndex].Value = "";
                this.PaymentManagementGridView["Address1", rowIndex].Value = "";
                this.PaymentManagementGridView["Address2", rowIndex].Value = "";
                this.PaymentManagementGridView["City", rowIndex].Value = "";
                this.PaymentManagementGridView["State", rowIndex].Value = "";
                this.PaymentManagementGridView["Zip", rowIndex].Value = "";
                this.PaymentManagementGridView["Comment", rowIndex].Value = "";

                this.PaymentManagementGridView["PaidBy", rowIndex].ReadOnly = true;
                this.PaymentManagementGridView["CheckNumber", rowIndex].ReadOnly = true;
                this.PaymentManagementGridView["Amount", rowIndex].ReadOnly = true;
            }
            this.PaymentManagementGridView.Refresh();
        }

        /// <summary>
        /// Changes in the Tender Type Fields for the Receipt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentManagementGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (!this.PaymentManagementGridView.Rows[e.RowIndex].Cells["TenderType"].ReadOnly)
                    {
                        ////the below method is used to enable the save and cancel methods
                        //this.EditEnabled();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Customizes the payment Management grid.
        /// </summary>
        private void CustomizePaymentManageGrid()
        {
            try
            {
                this.PaymentManagementGridView.AllowUserToResizeColumns = false;
                this.PaymentManagementGridView.AutoGenerateColumns = false;
                this.PaymentManagementGridView.AllowUserToResizeRows = false;
                this.PaymentManagementGridView.StandardTab = false;
                this.PaymentManagementGridView.TabStop = true;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.TenderTypeColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.TenderTypeColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.PaidByColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.PaidByColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.CheckNumberColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.CheckNumberColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.AmountColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.AmountColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.IsEditableColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.IsEditableColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.Address1Column.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.Address1Column.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.Address2Column.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.Address2Column.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.CityColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.CityColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.StateColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.StateColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.ZipColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.ZipColumn.ColumnName;
                this.PaymentManagementGridView.Columns[this.payamentManageDataSet.PaymentManageGrid.CommentColumn.ColumnName].DataPropertyName = this.payamentManageDataSet.PaymentManageGrid.CommentColumn.ColumnName;
                this.PaymentManagementGridView.PrimaryKeyColumnName = this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName;

                DataTable tenderTypeDataTable = this.F1557Controll.WorkItem.F1018_ListTenderType(true);
                DataTable tempDataTable = new DataTable();
                tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
                tempDataTable.Rows.Add(new object[] { "", "" });
                for (int i = 0; i < tenderTypeDataTable.Rows.Count; i++)
                {
                    tempDataTable.Rows.Add(new object[] { tenderTypeDataTable.Rows[i]["TenderType"].ToString(), tenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
                }
                (this.PaymentManagementGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable;
                (this.PaymentManagementGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
                (this.PaymentManagementGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";

                this.PaymentManagementGridView.Columns[SharedFunctions.GetResourceString("ValidStatus")].ReadOnly = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the manage payment grid.
        /// </summary>
        private void PopulateManagePaymentGrid()
        {
            this.payamentManageDataSet = this.form1557Control.WorkItem.GetManagePayment(this.receiptId);
            this.recordCount = this.payamentManageDataSet.PaymentManageGrid.Rows.Count;
            InitialPaymentData = this.payamentManageDataSet.PaymentManageGrid.Copy();
            this.paymentgridRowCount = this.payamentManageDataSet.PaymentManageGrid.Rows.Count;
            for (int counter = 0; counter < this.payamentManageDataSet.PaymentManageGrid.Rows.Count; counter++)
            {
                this.payamentManageDataSet.PaymentManageGrid.Rows[counter][this.payamentManageDataSet.PaymentManageGrid.TenderTypeColumn] = this.payamentManageDataSet.PaymentManageGrid.Rows[counter][this.payamentManageDataSet.PaymentManageGrid.TenderTypeIDColumn];
                this.payamentManageDataSet.PaymentManageGrid.Rows[counter][this.payamentManageDataSet.PaymentManageGrid.PPaymentIDColumn] = this.payamentManageDataSet.PaymentManageGrid.Rows[counter][this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn];
              
            }
            int emptyRows;
            if (this.paymentgridRowCount > 0)
            {
                if (this.PaymentManagementGridView.NumRowsVisible > this.paymentgridRowCount)
                {
                    emptyRows = this.PaymentManagementGridView.NumRowsVisible - this.paymentgridRowCount;
                    for (int i = 0; i < emptyRows; i++)
                    {
                        this.payamentManageDataSet.PaymentManageGrid.AddPaymentManageGridRow(this.payamentManageDataSet.PaymentManageGrid.NewPaymentManageGridRow());
                    }
                }
                else
                {
                    this.payamentManageDataSet.PaymentManageGrid.AddPaymentManageGridRow(this.payamentManageDataSet.PaymentManageGrid.NewPaymentManageGridRow());
                }
                this.SelectAllCheckBox.Enabled = true;
            }
            else
            {
                for (int i = 0; i < this.PaymentManagementGridView.NumRowsVisible; i++)
                {
                    this.payamentManageDataSet.PaymentManageGrid.AddPaymentManageGridRow(this.payamentManageDataSet.PaymentManageGrid.NewPaymentManageGridRow());
                }
            }
            this.PaymentManagementGridView.DataSource = this.payamentManageDataSet.PaymentManageGrid.DefaultView;
            this.PaymentManagementGridView.Enabled = true;
            this.PaymentManagementGridView.AutoGenerateColumns = false;

            int ownerID;
            int.TryParse(this.ownerIdPaidBy, out ownerID);
            if (ownerID > 0)
            {
                this.payeeDetails = this.form1557Control.WorkItem.F1019_GetPayeeDetails(ownerID);
            }
            else
            {
                this.payeeDetails.PayeeDetail.Rows.Clear();
            }

            if (this.payamentManageDataSet.PaymentManageGrid.Rows.Count > 3)
            {
                this.PaymentManageVscrollBar.Visible = false;
            }
            else
            {
                this.PaymentManageVscrollBar.Visible = true;
            }
        }

        private void PopulateManageTableValues()
        {
            decimal tempbalance = 0;
            if (this.payamentManageDataSet.PaymentManagementTable.Count > 0)
            {
                this.PPaymentIDTextBox.Text = this.payamentManageDataSet.PaymentManagementTable.Rows[0]["PPaymentID"].ToString();
                this.SourceTextBox.Text = this.payamentManageDataSet.PaymentManagementTable.Rows[0]["ReceiptSource"].ToString();
                this.ReceiptTotalTextBox.Text = this.payamentManageDataSet.PaymentManagementTable.Rows[0]["ReceiptTotal"].ToString();
                this.PaymentTotaltextBox.Text = this.payamentManageDataSet.PaymentManagementTable.Rows[0]["PaymentTotal"].ToString();
                this.BalanceTextBox.Text = Convert.ToString(Convert.ToDecimal(this.payamentManageDataSet.PaymentManagementTable.Rows[0]["ReceiptTotal"]) - Convert.ToDecimal(this.payamentManageDataSet.PaymentManagementTable.Rows[0]["PaymentTotal"]));
                tempbalance = Convert.ToDecimal(this.payamentManageDataSet.PaymentManagementTable.Rows[0]["ReceiptTotal"]) - Convert.ToDecimal(this.payamentManageDataSet.PaymentManagementTable.Rows[0]["PaymentTotal"]);

                if(Convert.ToDecimal(tempbalance)==0)
                {
                    this.panel4.BackColor = Color.FromArgb(130, 189, 140);
                    this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
                    //this.SaveCloseButton.Enabled = true;
                }
                else
                {
                    this.panel4.BackColor = Color.FromArgb(128, 0, 0);
                    this.BalanceTextBox.ForeColor = Color.White;
                    this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                    //this.SaveCloseButton.Enabled = false;
                }
            }
        }

        private void PaymentManagementGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);

                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                    ((ComboBox)e.Control).KeyPress -= new KeyPressEventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                    ((ComboBox)e.Control).KeyPress += new KeyPressEventHandler(this.ReceiptEngine_SelectionChangeCommitted); 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PaymentItemsGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void ReceiptEngine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = (ComboBox)sender;
                this.ChangeTenderTypeRelatedFields(combo);
                this.CalculatePaymentTotal();
                this.CalculateBalance();
                this.PaymentEndEdit(); 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Calculatings the balance.
        /// </summary>       
        private void CalculateBalance()
        {
            decimal receiptBalance = this.ReceiptTotalTextBox.DecimalTextBoxValue - this.PaymentTotaltextBox.DecimalTextBoxValue;
            this.BalanceTextBox.Text = receiptBalance.ToString();

            if (receiptBalance == 0)
            {
                this.EnableSaveCancelButton(true);
                this.panel4.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.panel4.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceTextBox.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Calculatings the payment total.
        /// </summary>        
        private void CalculatePaymentTotal()
        {
            decimal outDecimalValue;
            decimal paymentsTotal = 0;
            int paymentCount = 0;

            for (int counter = 0; counter < this.payamentManageDataSet.PaymentManageGrid.Rows.Count; counter++)
            {
                if (!String.IsNullOrEmpty(this.payamentManageDataSet.PaymentManageGrid.Rows[counter]["TenderType"].ToString()) && Decimal.TryParse(this.payamentManageDataSet.PaymentManageGrid.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    paymentsTotal += outDecimalValue;
                    if (outDecimalValue != 0)
                    {
                        paymentCount++;
                    }
                }
            }

            this.PaymentTotaltextBox.Text = paymentsTotal.ToString();

            if (paymentCount == this.payamentManageDataSet.PaymentManageGrid.Rows.Count)
            {
                F1557PayamentManagementData.PaymentManageGridRow dr = this.payamentManageDataSet.PaymentManageGrid.NewPaymentManageGridRow();
                dr.ModuleID = this.ParentFormId;
                this.payamentManageDataSet.PaymentManageGrid.Rows.Add(dr);
                this.PaymentManageVscrollBar.Visible = false;
                this.PaymentManagementGridView.Refresh();
            }
        }

        ///<summary>
        /// Used to hold the Calculation in PaymentGrid View
        /// </summary>
        private void PaymentEndEdit()
        {
            decimal outDecimalValue;
            decimal paymentsTotal = 0;
            int paymentCount = 0;

            for (int counter = 0; counter < this.payamentManageDataSet.PaymentManageGrid.Rows.Count; counter++)
            {
                if (!String.IsNullOrEmpty(this.payamentManageDataSet.PaymentManageGrid.Rows[counter]["TenderType"].ToString()) && Decimal.TryParse(this.payamentManageDataSet.PaymentManageGrid.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    paymentsTotal += outDecimalValue;
                    if (outDecimalValue != 0)
                    {
                        paymentCount++;
                    }
                }
            }

            this.PaymentTotaltextBox.Text = paymentsTotal.ToString();

            if (paymentCount == this.payamentManageDataSet.PaymentManageGrid.Rows.Count)
            {
                F1557PayamentManagementData.PaymentManageGridRow dr = this.payamentManageDataSet.PaymentManageGrid.NewPaymentManageGridRow();
                dr.ModuleID = this.ParentFormId;
                this.payamentManageDataSet.PaymentManageGrid.Rows.Add(dr);
                this.PaymentManagementGridView.Refresh();
            }
            else
            {
                if (paymentCount.Equals(this.PaymentManagementGridView.CurrentCell))
                {
                    if (this.PaymentManagementGridView.CurrentCell != null && this.PaymentManagementGridView.CurrentCell.RowIndex >= 0)
                    {
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["PaidBy"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex]["PaidBy"];
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["Address1"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex]["Address1"];
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["Address2"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex]["Address2"];
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["City"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex]["City"];
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["State"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex]["State"];
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["Zip"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex]["Zip"];
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["Comment"] = this.payamentManageDataSet.PaymentManageGrid.Rows[this.PaymentManagementGridView.CurrentCell.RowIndex]["Comment"];
                    }
                    else
                    {
                        this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["PaidBy"] = this.ownerNamePaidBy;
                    }
                }
                //this.payamentManageDataSet.PaymentManageGrid.Rows[paymentCount]["Amount"] = this.BalanceTextBox.DecimalTextBoxValue;

                this.PaymentManagementGridView["PaidBy", paymentCount].ReadOnly = false;
                this.PaymentManagementGridView["Amount", paymentCount].ReadOnly = false;
                this.PaymentManagementGridView["CheckNumber", paymentCount].ReadOnly = false;
                this.PaymentManagementGridView.Refresh();

                this.CalculatePaymentTotal();
                //this.CalculateBalance();
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.PaymentManagementGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cancel.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SelectAllCheckBox.Checked = false;
                this.SelectAllCheckBox.Enabled = false;
                SelectUnSelectAll("false");
                this.selectedPaymentItemIds = new List<int>();
                EnableRemoveSelectedButton(false);
                EnableSaveCancelButton(true);
                ////Here the variable/method or whatever which causes the unsaved change to fire can be written
                //this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void PaymentManagementGridView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                int firstColumn = this.PaymentManagementGridView.Columns["PaidBy"].Index;
                int secondColumn = this.PaymentManagementGridView.Columns["PaidByImage"].Index;
                Rectangle r1 = this.PaymentManagementGridView.GetCellDisplayRectangle(firstColumn, -1, true);
                Rectangle r2 = this.PaymentManagementGridView.GetCellDisplayRectangle(secondColumn, -1, true);
                r1.X += 1;
                r1.Y += 1;
                r1.Width += r2.Width - 2;
                r1.Height -= 2;

                // Draw color
                using (SolidBrush br = new SolidBrush(this.PaymentManagementGridView.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(br, r1);
                }

                // Draw header text   
                using (SolidBrush br = new SolidBrush(this.PaymentManagementGridView.ColumnHeadersDefaultCellStyle.ForeColor))
                {
                    StringFormat sf = new StringFormat();
                    // Set X, Y position to display header text
                    r1.X += 160;
                    r1.Y += 4;
                    e.Graphics.DrawString("Paid By", this.PaymentManagementGridView.ColumnHeadersDefaultCellStyle.Font, br, r1, sf);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void PaymentManagementGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                for (int i = 0; i < this.PaymentManagementGridView.Rows.Count; i++)
                {
                    TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.PaymentManagementGridView["PaidByImage", i];
                    imgCell.ImagexLocation = 1;
                    imgCell.ImageyLocation = 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

     

        /// <summary>
        /// Event to handle checkbox click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentManagementGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Add and remove the selected items for remove to  selectedPaymentItemIds in the sprint 163

                if (e.RowIndex >= 0 && e.RowIndex < this.PaymentManagementGridView.OriginalRowCount)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (e.ColumnIndex.Equals(this.PaymentManagementGridView.Columns["ValidStatus"].Index))
                        {
                           
                            // If save cancel button is enabled, there are unsaved changes. disable remove button
                            //if (SaveCloseButton.Enabled)
                            //{
                            //    this.Cancel.Enabled = true;
                            //    this.SelectAllCheckBox.Checked = false;
                            //    this.SelectUnSelectAll("False");
                            //    EnableRemoveSelectedButton(false);
                            //    return;
                            //}

                            int PaymentId;
                            int.TryParse(this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName].Value.ToString(), out PaymentId);
                            if (PaymentId > 0)
                            {
                                if (Convert.ToBoolean(this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.ValidStatus.Name].EditedFormattedValue) == true)
                                {
                                    this.PaymentManagementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                                    if (this.selectedPaymentItemIds.Contains(PaymentId))
                                    {
                                        this.selectedPaymentItemIds.Remove(Convert.ToInt32(this.PaymentManagementGridView[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName, e.RowIndex].Value));
                                        if (this.selectedPaymentItemIds.Count == 0)
                                        {
                                            EnableRemoveSelectedButton(false);
                                        }
                                    }
                                    if (this.paymentgridRowCount == 0 && this.selectedPaymentItemIds.Count == 0)
                                    {
                                        EnableRemoveSelectedButton(false);
                                    }
                                    if (this.paymentgridRowCount > this.selectedPaymentItemIds.Count)
                                    {
                                        this.SelectAllCheckBox.Checked = false;
                                    }
                                }
                                else
                                {
                                    this.PaymentManagementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                                    if (!this.selectedPaymentItemIds.Contains(PaymentId))
                                    {
                                        this.selectedPaymentItemIds.Add(Convert.ToInt32(this.PaymentManagementGridView[this.payamentManageDataSet.PaymentManageGrid.PaymentIDColumn.ColumnName, e.RowIndex].Value));
                                    }
                                    EnableRemoveSelectedButton(true);
                                    if (this.paymentgridRowCount == this.selectedPaymentItemIds.Count)
                                    {
                                        this.SelectAllCheckBox.Checked = true;
                                    }
                                }
                            }
                        }

                        if (e.ColumnIndex.Equals(this.PaymentManagementGridView.Columns["Amount"].Index))
                        {

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
        /// Event to handle grid changes and to calculate balance amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentManagementGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(this.PaymentManagementGridView.Columns["Amount"].Index))
                {
                    if ((e.RowIndex + 1) == this.PaymentManagementGridView.Rows.Count)
                    {
                        if ((!string.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.TenderType.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.PaidBy.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.CheckNumber.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.Amount.Name].Value.ToString().Trim())))
                        {
                            this.payamentManageDataSet.PaymentManageGrid.AddPaymentManageGridRow(this.payamentManageDataSet.PaymentManageGrid.NewPaymentManageGridRow());
                            if (this.PaymentManagementGridView.CurrentCell == null)
                            {
                                this.PaymentManagementGridView.CurrentCell = this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.CheckNumber.Name];
                                this.PaymentManagementGridView.Focus();
                            }
                        }
                    }
                    this.PaymentManagementGridView.DataSource = this.payamentManageDataSet.PaymentManageGrid.DefaultView;
                    if (this.payamentManageDataSet.PaymentManageGrid.Rows.Count > 3)
                    {
                        this.PaymentManageVscrollBar.Visible = false;
                    }
                    else
                    {
                        this.PaymentManageVscrollBar.Visible = true;
                    }
                    //To Calculate Balance TextBox
                    if (this.PaymentManagementGridView.OriginalRowCount > 0)
                    {
                        decimal tmpPaymentAmount = 0;
                        decimal PaymentAmount = 0;
                        for (int i = 0; i < PaymentManagementGridView.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(this.payamentManageDataSet.PaymentManageGrid.Rows[i]["TenderType"].ToString()) && Decimal.TryParse(this.payamentManageDataSet.PaymentManageGrid.Rows[i]["Amount"].ToString().Trim(), out PaymentAmount))
                            {
                                //decimal.TryParse(this.PaymentManagementGridView.Rows[i].Cells["Amount"].Value.ToString().Trim(), out PaymentAmount);
                                tmpPaymentAmount = tmpPaymentAmount + PaymentAmount;
                            }
                        }

                        this.PaymentTotaltextBox.Text = tmpPaymentAmount.ToString();

                        if (this.payamentManageDataSet.PaymentManagementTable.Count > 0)
                        {
                            this.BalanceTextBox.Text = (Convert.ToDecimal(Convert.ToDecimal(this.payamentManageDataSet.PaymentManagementTable.Rows[0]["ReceiptTotal"])) - tmpPaymentAmount).ToString();
                            decimal balance = 0;
                            if (Decimal.TryParse(this.BalanceTextBox.Text, NumberStyles.Currency, null, out balance))
                            {
                            }
                            if (balance == 0)
                            {
                                this.panel4.BackColor = Color.FromArgb(130, 189, 140);
                                this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
                                EnableSaveCancelButton(true);
                            }
                            else
                            {
                                this.panel4.BackColor = Color.FromArgb(128, 0, 0);
                                this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                                this.BalanceTextBox.ForeColor = Color.White;
                                EnableSaveCancelButton(false);
                            }
                        }
                    }
                    else
                    {
                        this.BalanceTextBox.Text = string.Empty;
                        this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                        this.BalanceTextBox.ForeColor = Color.White;
                        EnableSaveCancelButton(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.PopulateManageTableValues();
            this.CustomizePaymentManageGrid();
            this.PopulateManagePaymentGrid();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.Cancel.Enabled = false;
            EnableSaveCancelButton(false);
            this.CancelItem = true;
            
        }

        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // If save cancel button is enabled, there are unsaved changes. disable remove button
                //if (SaveCloseButton.Enabled)
                //{
                //    this.SelectAllCheckBox.Checked = false;
                //    this.SelectUnSelectAll("False");
                //    //EnableRemoveSelectedButton(false);
                //    return;
                //}

                if (this.PaymentManagementGridView.OriginalRowCount > 0)
                {
                    if (SelectAllCheckBox.Checked == true)
                    {
                        selectedPaymentItemIds = new List<int>();
                        if (this.paymentgridRowCount > 0)
                        {
                            this.SelectUnSelectAll("True");
                            EnableRemoveSelectedButton(true);
                        }
                        this.CalculateSelectAllPayment(SelectAllCheckBox.Checked);

                    }
                    else if (SelectAllCheckBox.Checked == false)
                    {
                        if (this.paymentgridRowCount > 0 && this.paymentgridRowCount <= this.selectedPaymentItemIds.Count)
                        {
                            this.SelectUnSelectAll("False");
                            EnableRemoveSelectedButton(false);
                            this.CalculateUnSelectPayment(SelectAllCheckBox.Checked);
                        }
                    }
                }
                else
                {
                    SelectAllCheckBox.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Method to Enable / Disable SaveCancel Button
        /// </summary>
        /// <param name="Enabled"></param>
        private void EnableSaveCancelButton(bool Enabled)
        {
            try
            {
                decimal balance = 0;
                if (Decimal.TryParse(this.BalanceTextBox.Text, NumberStyles.Currency, null, out balance))
                {
                }
                if (Enabled && balance==0)
                {
                    this.SaveCloseButton.Enabled = true;
                    this.SaveCloseButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.SaveCloseButton.ForeColor = Color.White;
                   
                    //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                }
                else
                {
                    this.SaveCloseButton.Enabled = false;
                    this.SaveCloseButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.SaveCloseButton.ForeColor = Color.Gray;
                   
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Method to Enable / Disable RemoveSelected Button
        /// </summary>
        /// <param name="Enabled"></param>
        private void EnableRemoveSelectedButton(bool Enabled)
        {
            try
            {
                if (Enabled)
                {
                    this.RemoveButton.Enabled = true;
                    this.RemoveButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.RemoveButton.ForeColor = Color.White;
                }
                else
                {
                    this.RemoveButton.Enabled = false;
                    this.RemoveButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.RemoveButton.ForeColor = Color.Gray; 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private bool IsPaymentEntryModified(DataTable dtPaymentManageGrid)
        {
            bool IsPaymentEntryModified = false;
            try
            {
                foreach (DataRow dr in InitialPaymentData.Rows)
                {
                    DataRow[] draPayment = dtPaymentManageGrid.Select("PaymentId = '" + dr["PaymentId"].ToString() + "'");
                    if (draPayment.Length > 0)
                    {
                        if (dr["TenderTypeId"].ToString().Trim() != draPayment[0]["TenderTypeId"].ToString().Trim() ||
                            dr["PaidBy"].ToString().Trim() != draPayment[0]["PaidBy"].ToString().Trim() ||
                            dr["CheckNumber"].ToString().Trim() != draPayment[0]["CheckNumber"].ToString().Trim() ||
                            dr["Amount"].ToString().Trim() != draPayment[0]["Amount"].ToString().Trim())
                        {
                            IsPaymentEntryModified = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            return IsPaymentEntryModified;
        }

        private bool IsPaymentEntryAdded(DataTable dtPaymentManageGrid)
        {
            bool NewPaymentEntryAdded = false;
            try
            {
                var draNewApproach = from myRow in dtPaymentManageGrid.AsEnumerable()
                                     where myRow["PaymentId"] == DBNull.Value
                                     select myRow;
                if (draNewApproach == null || draNewApproach.Count() == 0)
                {
                    NewPaymentEntryAdded = false;
                }
                else
                {
                    foreach (var dr in draNewApproach)
                    {
                        if (dr["TenderTypeId"].ToString().Trim() != "" || dr["PaidBy"].ToString().Trim() != "" ||
                            dr["CheckNumber"].ToString().Trim() != "" || dr["Amount"].ToString().Trim() != "")
                        {
                            NewPaymentEntryAdded = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            return NewPaymentEntryAdded;
        }

        private void F1557_FormClosing(object sender, FormClosingEventArgs e)
        {
            decimal balance = 0;
            if (Decimal.TryParse(this.BalanceTextBox.Text, NumberStyles.Currency, null, out balance))
            {
            }
            try
            {
                if (balance == 0 && this.CancelItem==false)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                this.CancelItem = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void PaymentManagementGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {


                Decimal outDecimal;

                if (e.ColumnIndex == this.PaymentManagementGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null && !String.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            //else if (this.PaymentManagementGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString() == "2")
                            //{
                            //    ////Added By Ramya.D For Sprint 41
                            //    //if (!this.instrumentPayment && this.Tag != null && !this.Tag.Equals("1013") && !this.Tag.Equals("1410"))
                            //    //////Added last 2 conditions by Biju on 13/Apr/2010 to implement #6556.
                            //    //{
                            //    //    e.Value = String.Concat("(", outDecimal.ToString("#,##0.00"), ")");
                            //    //    e.CellStyle.ForeColor = Color.Green;
                            //    //}
                            //    //else
                            //    //{
                            //    //    e.Value = outDecimal.ToString("#,##0.00");
                            //    //}
                            //}
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            ////if (!this.ApplyInstrumentPayment)
                            ////{
                            e.Value = "0.00";
                            ////}
                            ////else
                            ////{
                            ////e.Value = this.ApplyInstrumentBalanceAmount;
                            ////this.instrumentPaymentsDataTable.Rows[e.RowIndex][e.ColumnIndex] = this.ApplyInstrumentBalanceAmount;
                            //////this.instrumentPaymentsDataTable.AcceptChanges();
                            //////this.instrumentPaymentsDataTable.AcceptChanges();
                            ////}
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }


                //decimal outDecimal;

                //if (e.ColumnIndex == this.PaymentManagementGridView.Columns[this.Amount.Name].Index)
                //{
                //    if (e.RowIndex < 0)
                //    {
                //        return;
                //    }

                //    if (e.Value != null && !String.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.Amount.Name].Value.ToString()))
                //    {
                //        string val = e.Value.ToString();
                //        if (Decimal.TryParse(val, out outDecimal))
                //        {
                //            e.Value = outDecimal.ToString("#,##0.00");
                //            e.FormattingApplied = true;
                //        }
                //        else
                //        {
                //            e.Value = "";
                //        }
                //    }
                //    else
                //    {
                //        e.Value = "";
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void PaymentManagementGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // Paid ny Image click handler
                if (e.RowIndex >= 0 && e.ColumnIndex.Equals(this.PaymentManagementGridView.Columns["PaidByImage"].Index))
                {
                    if (!string.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))// if (!this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].ReadOnly && !this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].ReadOnly)
                    {
                        if ((e.X >= 2) && (e.X <= (34 - 10)) && (e.Y >= 1) && (e.Y <= (22 - 1)))//((e.X >= 300) && (e.X <= (331 - 10)) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                        {
                            DataTable tempTable = new DataTable();
                            string PayeeItemXml;

                            if (this.PaymentManagementGridView.Rows[e.RowIndex].Cells[this.payamentManageDataSet.PaymentManageGrid.IsEditableColumn.ColumnName].Value.ToString() == "0")
                            {
                                this.isEdit = false;
                            }
                            else
                            {
                                this.isEdit = true;
                                //this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["CheckNumber"].ReadOnly = true;
                            }
                            if (tempTable.Columns.Count <= 0)
                            {
                                tempTable.Columns.AddRange(new DataColumn[] { new DataColumn("PaidBy"), new DataColumn("Address1"), new DataColumn("Address2"), new DataColumn("City"), new DataColumn("State"), new DataColumn("Zip"), new DataColumn("Comment") });
                            }
                            ///Column information

                            if (tempTable.Rows.Count == 0)
                            {
                                try
                                {
                                    DataRow tempRow = tempTable.NewRow();
                                    if (tempTable.Rows.Count.Equals(0))
                                    {
                                        //if (!string.IsNullOrEmpty(this.ownerIdPaidBy) && !string.IsNullOrEmpty(this.ownerNamePaidBy))
                                        //{
                                        //    tempRow["PaidBy"] = this.payeeDetails.PayeeDetail.Rows[0]["PaidBy"];
                                        //    tempRow["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                                        //    tempRow["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                                        //    tempRow["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                                        //    tempRow["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                                        //    tempRow["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                                        //    tempRow["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];
                                        //    tempTable.Rows.Add(tempRow);
                                        //}
                                        //else
                                        //{
                                            tempRow["PaidBy"] = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["PaidBy"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["PaidBy"].ToString();
                                            tempRow["Address1"] = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Address1"].Value.ToString();   //this.ownerDetailDataSet.GetPayment.Rows[0]["Address1"].ToString();
                                            tempRow["Address2"] = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Address2"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["Address2"].ToString();
                                            tempRow["City"] = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["City"].Value.ToString();    //this.ownerDetailDataSet.GetPayment.Rows[0]["City"].ToString();
                                            tempRow["State"] = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["State"].Value.ToString();   //this.ownerDetailDataSet.GetPayment.Rows[0]["State"].ToString();
                                            tempRow["Zip"] = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Zip"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["Zip"].ToString();
                                            tempRow["Comment"] = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Comment"].Value.ToString();   //this.ownerDetailDataSet.GetPayment.Rows[0]["Comment"].ToString();
                                            tempTable.Rows.Add(tempRow);
                                        //}
                                    }
                                    else
                                    {
                                        tempRow["PaidBy"] = "";
                                        tempRow["Address1"] = "";
                                        tempRow["Address2"] = "";
                                        tempRow["City"] = "";
                                        tempRow["State"] = "";
                                        tempRow["Zip"] = "";
                                        tempRow["Comment"] = "";
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                            DataSet tempDataSet = new DataSet("Root");
                            tempDataSet.Tables.Add(tempTable);
                            tempDataSet.Tables[0].TableName = "Table";
                            string payeeIDs = tempDataSet.GetXml();
                            object[] optionalParameter = new object[2];
                            optionalParameter[0] = payeeIDs;
                            optionalParameter[1] = this.isEdit;
                            //this.parentWorkItem = D1018.F1019WorkItem;   
                            //ownerDetailDataSet = this.form1019Control.WorkItem.F1019_GetPayeeDetails(this.OwnerpayeeID); 
                            Form PayeeDetailsForm = TerraScanCommon.GetForm(1019, optionalParameter, this.parentWorkItem);
                            if (PayeeDetailsForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                            {
                                if (PayeeDetailsForm.ShowDialog() == DialogResult.OK)
                                {
                                    this.Cancel.Enabled = true;
                                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                                    this.SelectAllCheckBox.Checked = false;
                                    this.SelectAllCheckBox.Enabled = false;
                                    SelectUnSelectAll("false");
                                    this.selectedPaymentItemIds = new List<int>();
                                    EnableRemoveSelectedButton(false);
                                    EnableSaveCancelButton(true);
                                    string PayeeInfo = TerraScanCommon.GetValue(PayeeDetailsForm, SharedFunctions.GetResourceString("PayeeID").ToString());
                                    //"<Root><Table><Test1>Test1</Test1><Test2>Test2</Test2></Table></Root>";
                                    System.IO.StringReader stringReader = new System.IO.StringReader(PayeeInfo);
                                    System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                                    //System.Xml.XmlReader reader = new System.Xml.XmlReader();
                                    DataSet ds = new DataSet();
                                    ds.ReadXml(textReader);

                                    if (ds.Tables.Count > 0)
                                    {
                                        string A = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["PaidBy"].Value.ToString();
                                        string B = ds.Tables[0].Rows[0]["PaidBy"].ToString();
                                        if (A != B)//this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["PaidBy"].Value != ds.Tables[0].Rows[0]["PaidBy"])
                                        {
                                            this.PaymentManagementGridView.Rows[e.RowIndex].Cells["PaidBy"].Value = ds.Tables[0].Rows[0]["PaidBy"].ToString().Trim();
                                        }
                                        if (!this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Address1"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Address1"].ToString().Trim()))
                                        {
                                            this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Address1"].Value = ds.Tables[0].Rows[0]["Address1"].ToString().Trim();
                                        }
                                        if (!this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Address2"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Address2"].ToString().Trim()))
                                        {
                                            this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Address2"].Value = ds.Tables[0].Rows[0]["Address2"].ToString().Trim();
                                        }
                                        if (!this.PaymentManagementGridView.Rows[e.RowIndex].Cells["City"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["City"].ToString().Trim()))
                                        {
                                            this.PaymentManagementGridView.Rows[e.RowIndex].Cells["City"].Value = ds.Tables[0].Rows[0]["City"].ToString().Trim();
                                        }
                                        if (!this.PaymentManagementGridView.Rows[e.RowIndex].Cells["State"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["State"].ToString().Trim()))
                                        {
                                            this.PaymentManagementGridView.Rows[e.RowIndex].Cells["State"].Value = ds.Tables[0].Rows[0]["State"].ToString().Trim();
                                        }
                                        if (!this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Zip"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Zip"].ToString().Trim()))
                                        {
                                            this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Zip"].Value = ds.Tables[0].Rows[0]["Zip"].ToString().Trim();
                                        }
                                        if (!this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Comment"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Comment"].ToString().Trim()))
                                        {
                                            this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Comment"].Value = ds.Tables[0].Rows[0]["Comment"].ToString().Trim();
                                        }
                                    }
                                    this.PaymentManagementGridView.CurrentCell = this.PaymentManagementGridView.Rows[e.RowIndex].Cells["PaidBy"];
                                }
                            }
                        }
                    }
                }

                if (e.RowIndex >= 0 && (e.ColumnIndex.Equals(this.PaymentManagementGridView.Columns["Amount"].Index) || e.ColumnIndex.Equals(this.PaymentManagementGridView.Columns["PaidBy"].Index) || e.ColumnIndex.Equals(this.PaymentManagementGridView.Columns["CheckNumber"].Index)))
                {
                    if (string.IsNullOrEmpty(this.PaymentManagementGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))// if (!this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].ReadOnly && !this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].ReadOnly)
                    {
                        this.PaymentManagementGridView.Rows[e.RowIndex].Cells["Amount"].ReadOnly = true;
                        this.PaymentManagementGridView.Rows[e.RowIndex].Cells["PaidBy"].ReadOnly = true;
                        this.PaymentManagementGridView.Rows[e.RowIndex].Cells["CheckNumber"].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void PaymentManagementGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            Decimal outDecimal;
            Int64 outInt;

            // Only paint if desired column

            if (e.ColumnIndex == this.PaymentManagementGridView.Columns["Amount"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                //// Only paint if text provided, Only paint if desired text is in cell
                if (e.Value != null)
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (tempvalue.EndsWith("."))
                    {
                        tempvalue = string.Concat(tempvalue, "0");
                    }

                    if (Decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal))
                    {
                        //// check for Over / under based on tender type
                        //// ComboBox combo = (ComboBox)sender;

                        ////change property for combobox change

                        tempvalue = outDecimal.ToString();
                        tempvalue = tempvalue.Replace("-", "");

                        ////Added By ramya
                        ////if (!this.instrumentPayment)
                        ////{
                        if (!tempvalue.Contains("."))
                        {
                            tempvalue = tempvalue.PadLeft(2, '0').Insert(tempvalue.PadLeft(2, '0').Length - 2, ".");
                        }
                        ////}

                        if (outDecimal.ToString().Contains("-"))
                        {
                            outDecimal = decimal.Parse(tempvalue);
                            outDecimal = decimal.Negate(outDecimal);
                        }
                        else
                        {
                            outDecimal = decimal.Parse(tempvalue);
                        }

                        e.Value = outDecimal;

                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = decimal.Parse("0");
                        e.ParsingApplied = true;
                    }
                }
            }
        }
    }
}