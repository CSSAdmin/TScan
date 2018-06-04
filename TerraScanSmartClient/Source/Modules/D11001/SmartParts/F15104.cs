//--------------------------------------------------------------------------------------------
// <copyright file="F15104.cs" company="Congruent">
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
// 09 Oct 06		KARTHIKEYAN V	    Created
// 19 Sep 11        Manoj Kumar P       Modified to implement Payee Detial.

//*********************************************************************************/

namespace D11001
{
    #region Namespace

    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.CompositeUI;
    using System.ComponentModel;
  


    #endregion Namespace

    /// <summary>
    /// F15104
    /// </summary>
    public partial class F15104 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// recordCount Local variable.
        /// </summary>
        private int recordCount;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// valveId or keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Assigning Empty to parentWorkItem
        /// </summary>
        private WorkItem parentWorkItem;

        ///<summary>
        /// Editable in Receipt Grid View
        /// </summary>
        private bool isEdit;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private string sectionIndicatorTabText;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int redColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// PageModeTypes
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// PermissionFields
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// F15104Controller
        /// </summary>
        private F15104Controller form15104Controller;

        /// <summary>
        /// F15104ReceiptPayamentData
        /// </summary>
        private F15104ReceiptPayamentData receiptPayamentDataSet = new F15104ReceiptPayamentData();

        /// <summary>
        /// flag to identify form load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// Instance for F15100
        /// </summary>
        F15110 f15110;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15104"/> class.
        /// </summary>
        public F15104()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15104"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15104(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15104"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F15104(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// FormSlice_ValidationAlert
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// FormSlice_SectionIndicatorClick
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// FormSlice_EditEnabled
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion

        #region Property

        /// <summary>
        /// For F8050Control
        /// </summary>
        [CreateNew]
        public F15104Controller Form15104Control
        {
            get { return this.form15104Controller as F15104Controller; }
            set { this.form15104Controller = value; }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.masterFormNo)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = this.ReceiptPaymentGridView.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
                sliceResize.SliceFormHeight = this.ReceiptPaymentGridView.Height;
                this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                }

                this.ControlLock(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);

                if (this.recordCount > 0)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.PopulateReceiptPaymentGrid();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.ReceiptPaymentGridView.AllowSorting = true;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {

                  
                    this.form15104Controller.WorkItem.UpdateReceiptPayment(TerraScanCommon.GetXmlString(this.receiptPayamentDataSet.ReceiptPaymentTable), TerraScanCommon.UserId);

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.ReceiptPaymentGridView.AllowSorting = true;
                }
            }
            else
            {
                // Code commented for issue #6565
                // this.PopulateReceiptPaymentGrid();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.flagFormLoad = true;
                    this.PopulateReceiptPaymentGrid();
                    this.recordCount = this.receiptPayamentDataSet.ReceiptPaymentTable.Rows.Count;
                    if (this.recordCount > 0)
                    {
                        this.ReceiptPaymentGridView.DataSource = this.receiptPayamentDataSet;
                    }
                    else
                    {
                        this.receiptPayamentDataSet.Clear();
                        this.ReceiptPaymentGridView.DataSource = this.receiptPayamentDataSet;
                    }

                    this.ControlLock(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);
                    this.flagFormLoad = false;
                    this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                    this.Height = this.PaymentPictureBox.Height;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates the receipt payment grid.
        /// </summary>
        private void PopulateReceiptPaymentGrid()
        {
            this.receiptPayamentDataSet = this.form15104Controller.WorkItem.GetReceiptPayment(this.keyId);

            this.recordCount = this.receiptPayamentDataSet.ReceiptPaymentTable.Rows.Count;
           // this.SetSmartPartHeight(this.recordCount);
            this.ReceiptPaymentGridView.DataSource = this.receiptPayamentDataSet;

            for (int counter = 0; counter < this.receiptPayamentDataSet.ReceiptPaymentTable.Rows.Count; counter++)
            {
                this.receiptPayamentDataSet.ReceiptPaymentTable.Rows[counter][this.receiptPayamentDataSet.ReceiptPaymentTable.TenderTypeColumn] = this.receiptPayamentDataSet.ReceiptPaymentTable.Rows[counter][this.receiptPayamentDataSet.ReceiptPaymentTable.TenderTypeIDColumn];
            }

            if (this.recordCount > 4)
            {
                this.PaymentEngineVscrollBar.Visible = false;
            }
            else
            {
                this.PaymentEngineVscrollBar.Visible = true;
            }

            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.ReceiptPaymentGridView.Height;

            if (!this.flagFormLoad)
            {
                ////this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            }
        }

        /// <summary>
        /// Customizes the payment grid.
        /// </summary>
        private void CustomizePaymentGrid()
        {
            this.ReceiptPaymentGridView.AllowUserToResizeColumns = false;
            this.ReceiptPaymentGridView.AutoGenerateColumns = false;
            this.ReceiptPaymentGridView.AllowUserToResizeRows = false;
            this.ReceiptPaymentGridView.StandardTab = false;
            this.ReceiptPaymentGridView.TabStop = true;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.TenderTypeColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.TenderTypeColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.PaidByColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.PaidByColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.CheckNumberColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.CheckNumberColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.AmountColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.AmountColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.PaymentIDColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.PaymentIDColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.PPaymentIDColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.PPaymentIDColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.IsEditableColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.IsEditableColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.Address1Column.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.Address1Column.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.Address2Column.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.Address2Column.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.CityColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.CityColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.StateColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.StateColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.ZipColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.ZipColumn.ColumnName;
            this.ReceiptPaymentGridView.Columns[this.receiptPayamentDataSet.ReceiptPaymentTable.CommentColumn.ColumnName].DataPropertyName = this.receiptPayamentDataSet.ReceiptPaymentTable.CommentColumn.ColumnName;
            this.ReceiptPaymentGridView.PrimaryKeyColumnName = this.receiptPayamentDataSet.ReceiptPaymentTable.PaymentIDColumn.ColumnName;

            DataTable tenderTypeDataTable = this.form15104Controller.WorkItem.F1018_ListTenderType(true);
            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
            tempDataTable.Rows.Add(new object[] { "", "" });
            for (int i = 0; i < tenderTypeDataTable.Rows.Count; i++)
            {
                tempDataTable.Rows.Add(new object[] { tenderTypeDataTable.Rows[i]["TenderType"].ToString(), tenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
            }

            (this.ReceiptPaymentGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable;
            (this.ReceiptPaymentGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
            (this.ReceiptPaymentGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";
        }

        /// <summary>
        /// Change the tender type related fields.
        /// </summary>
        /// <param name="combo">The source control.</param>
        private void ChangeTenderTypeRelatedFields(ComboBox combo)
        {
            int rowIndex = 0;

            if (this.ReceiptPaymentGridView.CurrentCell != null)
            {
                rowIndex = this.ReceiptPaymentGridView.CurrentCell.RowIndex;
            }

            ////change property for combobox change
             if (combo.SelectedIndex > 0 && combo.SelectedValue != null)
            {
                if (String.IsNullOrEmpty(this.receiptPayamentDataSet.ReceiptPaymentTable.Rows[rowIndex]["TenderType"].ToString()))
                {
                    this.ReceiptPaymentGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    this.receiptPayamentDataSet.ReceiptPaymentTable.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                }
                else
                {
                    this.ReceiptPaymentGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                    this.receiptPayamentDataSet.ReceiptPaymentTable.Rows[rowIndex]["TenderTypeID"] = combo.SelectedValue;
                }
                this.ReceiptPaymentGridView["PaidBy", rowIndex].ReadOnly = false;
                this.ReceiptPaymentGridView["Address1", rowIndex].ReadOnly = false;
                this.ReceiptPaymentGridView["Address2", rowIndex].ReadOnly = false;
                this.ReceiptPaymentGridView["State", rowIndex].ReadOnly = false;
                this.ReceiptPaymentGridView["City", rowIndex].ReadOnly = false;
                this.ReceiptPaymentGridView["Zip", rowIndex].ReadOnly = false;
                this.ReceiptPaymentGridView["Comment", rowIndex].ReadOnly = false;
                if (string.IsNullOrEmpty(this.ReceiptPaymentGridView["Amount", rowIndex].Value.ToString()) && rowIndex > 0)
                {
                    this.ReceiptPaymentGridView["PaidBy", rowIndex].Value = this.ReceiptPaymentGridView["PaidBy", rowIndex - 1].ToString();
                    this.ReceiptPaymentGridView["Address1", rowIndex].Value = this.ReceiptPaymentGridView["Address1", rowIndex - 1].ToString();
                    this.ReceiptPaymentGridView["Address2", rowIndex].Value = this.ReceiptPaymentGridView["Address2", rowIndex - 1].ToString();
                    this.ReceiptPaymentGridView["City", rowIndex].Value = this.ReceiptPaymentGridView["City", rowIndex - 1].ToString();
                    this.ReceiptPaymentGridView["State", rowIndex].Value = this.ReceiptPaymentGridView["State", rowIndex - 1].ToString();
                    this.ReceiptPaymentGridView["Zip", rowIndex].Value = this.ReceiptPaymentGridView["Zip", rowIndex - 1].ToString();
                    this.ReceiptPaymentGridView["Comment", rowIndex].Value = this.ReceiptPaymentGridView["Comment", rowIndex - 1].ToString();
                }
                else
                {
                    this.ReceiptPaymentGridView["PaidBy", rowIndex].Value = this.ReceiptPaymentGridView["PaidBy", rowIndex].Value.ToString();
                    this.ReceiptPaymentGridView["Address1", rowIndex].Value = this.ReceiptPaymentGridView["Address1", rowIndex].Value.ToString();
                    this.ReceiptPaymentGridView["Address2", rowIndex].Value = this.ReceiptPaymentGridView["Address2", rowIndex].Value.ToString();
                    this.ReceiptPaymentGridView["City", rowIndex].Value = this.ReceiptPaymentGridView["City", rowIndex].Value.ToString();
                    this.ReceiptPaymentGridView["State", rowIndex].Value = this.ReceiptPaymentGridView["State", rowIndex].Value.ToString();
                    this.ReceiptPaymentGridView["Zip", rowIndex].Value = this.ReceiptPaymentGridView["Zip", rowIndex].Value.ToString();
                    this.ReceiptPaymentGridView["Comment", rowIndex].Value = this.ReceiptPaymentGridView["Comment", rowIndex].Value.ToString();
                }

            }
            else
            {
                this.ReceiptPaymentGridView["TenderType", rowIndex].Value = "";
            }

             this.ReceiptPaymentGridView.Refresh();
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ReceiptPaymentGridView.AllowSorting = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="currentRecordCount">currentRecordCount.</param>
        private void SetSmartPartHeight(int currentRecordCount)
        {
            if (currentRecordCount > 3)
            {
                if (currentRecordCount > 6)
                {
                    currentRecordCount = 6;
                }

                int increment = ((currentRecordCount - 3) * 26);
                this.ReceiptPaymentGridView.Height = 88 + increment;
                this.ReceitPaymentPanel.Height = this.ReceiptPaymentGridView.Height;
                this.PaymentEngineVscrollBar.Height = this.ReceiptPaymentGridView.Height - 1;
                this.PaymentPictureBox.Height = this.ReceiptPaymentGridView.Height;
                this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                this.ReceiptPaymentGridView.NumRowsVisible = currentRecordCount;
                this.Height = this.ReceiptPaymentGridView.Height;
            }
            else
            {
                this.ReceiptPaymentGridView.Height = 88;
                this.ReceitPaymentPanel.Height = this.ReceiptPaymentGridView.Height - 1;
                this.PaymentEngineVscrollBar.Height = this.ReceiptPaymentGridView.Height - 1;
                this.PaymentPictureBox.Height = this.ReceiptPaymentGridView.Height - 1;
                this.ReceiptPaymentGridView.NumRowsVisible = 3;
                this.Height = this.ReceiptPaymentGridView.Height;
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.ReceiptPaymentGridView.Enabled = controlLook;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the PaymentPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F15104 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15104_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizePaymentGrid();
                this.flagFormLoad = true;
                this.parentWorkItem = this.form15104Controller.WorkItem;  
                this.PopulateReceiptPaymentGrid();
                ////this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.flagFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ReceiptPaymentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ReceiptPaymentGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.ReceiptPaymentGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null && !String.IsNullOrEmpty(this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the ReceiptPaymentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void ReceiptPaymentGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    if (this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells[this.receiptPayamentDataSet.ReceiptPaymentTable.IsEditableColumn.ColumnName].Value.ToString() == "0")
                    {
                        e.Cancel = true;
                        if (e.ColumnIndex == 0)
                        {
                            this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells[this.receiptPayamentDataSet.ReceiptPaymentTable.TenderTypeColumn.ColumnName].ReadOnly = true;
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
        /// Handles the MouseEnter event of the PaymentPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptHeaderToolTip.SetToolTip(this.PaymentPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ReceiptPaymentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ReceiptPaymentGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.ReceiptPaymentGridView.OriginalRowCount > 0)
                {
                    this.ReceiptPaymentGridView.Rows[0].Selected = true;
                }
                else
                {
                    this.ReceiptPaymentGridView.Rows[0].Selected = false;
                }
                for (int i = 0; i < this.ReceiptPaymentGridView.Rows.Count; i++)
                {
                    TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.ReceiptPaymentGridView["PaidByImage", i];
                    imgCell.ImagexLocation = 1;
                    imgCell.ImageyLocation = 1;
                    //imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.PaymentItemsGridView[0, i].InheritedStyle.BackColor.R), Convert.ToInt32(this.PaymentItemsGridView[0, i].InheritedStyle.BackColor.G), Convert.ToInt32(this.PaymentItemsGridView[0, i].InheritedStyle.BackColor.B));
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the AccountMgmtDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void ReceiptPaymentGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                ////change property for combobox change
                this.ChangeTenderTypeRelatedFields(combo);
                this.EditEnabled();
                this.EditModeDisabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                this.ReceiptPaymentGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
                ////Here the variable/method or whatever which causes the unsaved change to fire can be written
                this.EditEnabled();
                this.EditModeDisabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// deActivates the payment button in Master form according to the conditions specified.
        /// </summary>
        private void EditModeDisabled()
        {
            try
            {
                var f15110s = this.ParentForm.Controls.Find("F15110", true);
                if (f15110s.Length > 0)
                {
                    f15110 = (F15110)f15110s[0];
                }
                if (f15110 != null)
                {
                    f15110.DisableManagePaymentButton();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Payee Details Information Button Click Operation
        /// </summary>
        /// <param name="sender">The Source of the event</param>
        /// <param name="e"></param>
        private void ReceiptPaymentGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex.Equals(this.ReceiptPaymentGridView.Columns["PaidByImage"].Index))
            {
                if (!string.IsNullOrEmpty(this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))// if (!this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].ReadOnly && !this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].ReadOnly)
                {
                    if ((e.X >= 2) && (e.X <= (34 - 10)) && (e.Y >= 1) && (e.Y <= (22 - 1)))//((e.X >= 300) && (e.X <= (331 - 10)) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                    {
                        DataTable tempTable = new DataTable();
                        string PayeeItemXml;

                        if (this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells[this.receiptPayamentDataSet.ReceiptPaymentTable.IsEditableColumn.ColumnName].Value.ToString() == "0")
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
                            DataRow tempRow = tempTable.NewRow();
                            tempRow["PaidBy"] = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["PaidBy"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["PaidBy"].ToString();
                            tempRow["Address1"] = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Address1"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["Address1"].ToString();
                            tempRow["Address2"] = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Address2"].Value.ToString(); //this.ownerDetailDataSet.GetPayment.Rows[0]["Address2"].ToString();
                            tempRow["City"] = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["City"].Value.ToString();   //this.ownerDetailDataSet.GetPayment.Rows[0]["City"].ToString();
                            tempRow["State"] = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["State"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["State"].ToString();
                            tempRow["Zip"] = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Zip"].Value.ToString(); //this.ownerDetailDataSet.GetPayment.Rows[0]["Zip"].ToString();
                            tempRow["Comment"] = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Comment"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["Comment"].ToString();
                            //tempRow["PaymentID"] = 0; 
                            tempTable.Rows.Add(tempRow);
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
                                string PayeeInfo = TerraScanCommon.GetValue(PayeeDetailsForm, SharedFunctions.GetResourceString("PayeeID").ToString());
                                //"<Root><Table><Test1>Test1</Test1><Test2>Test2</Test2></Table></Root>";
                                System.IO.StringReader stringReader = new System.IO.StringReader(PayeeInfo);
                                System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                                //System.Xml.XmlReader reader = new System.Xml.XmlReader();
                                DataSet ds = new DataSet();
                                ds.ReadXml(textReader);

                                if (ds.Tables.Count > 0)
                                {
                                    string A = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["PaidBy"].Value.ToString();
                                    string B = ds.Tables[0].Rows[0]["PaidBy"].ToString();
                                    if (A != B)//this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["PaidBy"].Value != ds.Tables[0].Rows[0]["PaidBy"])
                                    {
                                        this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["PaidBy"].Value = ds.Tables[0].Rows[0]["PaidBy"].ToString().Trim();
                                    }
                                    if (!this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Address1"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Address1"].ToString().Trim()))
                                    {
                                        this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Address1"].Value = ds.Tables[0].Rows[0]["Address1"].ToString().Trim();
                                    }
                                    if (!this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Address2"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Address2"].ToString().Trim()))
                                    {
                                        this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Address2"].Value = ds.Tables[0].Rows[0]["Address2"].ToString().Trim();
                                    }
                                    if (!this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["City"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["City"].ToString().Trim()))
                                    {
                                        this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["City"].Value = ds.Tables[0].Rows[0]["City"].ToString().Trim();
                                    }
                                    if (!this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["State"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["State"].ToString().Trim()))
                                    {
                                        this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["State"].Value = ds.Tables[0].Rows[0]["State"].ToString().Trim();
                                    }
                                    if (!this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Zip"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Zip"].ToString().Trim()))
                                    {
                                        this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Zip"].Value = ds.Tables[0].Rows[0]["Zip"].ToString().Trim();
                                    }
                                    if (!this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Comment"].Value.ToString().Trim().Equals(ds.Tables[0].Rows[0]["Comment"].ToString().Trim()))
                                    {
                                        this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["Comment"].Value = ds.Tables[0].Rows[0]["Comment"].ToString().Trim();
                                    }
                                }

                                //this.paymentsDataTable.AcceptChanges();
                                //this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["PaidBy"].Selected = true;
                                this.ReceiptPaymentGridView.CurrentCell = this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["PaidBy"];
                                //this.PaymentItemsGridView.Rows[e.RowIndex].Cells[0].ToString()=      
                            }
                        }
                    }
                }



            }


        }
        /// <summary>
        /// Changes in the Tender Type Fields for the Receipt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiptPaymentGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (!this.ReceiptPaymentGridView.Rows[e.RowIndex].Cells["TenderType"].ReadOnly)
                    {
                        ////the below method is used to enable the save and cancel methods
                        this.EditEnabled();
                        this.EditModeDisabled();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Paint the Payee Button in the form Receipt Payment Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiptPaymentGridView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                int firstColumn = this.ReceiptPaymentGridView.Columns["PaidBy"].Index;
                int secondColumn = this.ReceiptPaymentGridView.Columns["PaidByImage"].Index;
                Rectangle r1 = this.ReceiptPaymentGridView.GetCellDisplayRectangle(firstColumn, -1, true);
                Rectangle r2 = this.ReceiptPaymentGridView.GetCellDisplayRectangle(secondColumn, -1, true);
                r1.X += 1;
                r1.Y += 1;
                r1.Width += r2.Width - 2;
                r1.Height -= 2;

                // Draw color
                using (SolidBrush br = new SolidBrush(this.ReceiptPaymentGridView.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(br, r1);
                }

                // Draw header text   
                using (SolidBrush br = new SolidBrush(this.ReceiptPaymentGridView.ColumnHeadersDefaultCellStyle.ForeColor))
                {
                    StringFormat sf = new StringFormat();
                    // Set X, Y position to display header text
                    r1.X += 160;
                    r1.Y += 4;
                    e.Graphics.DrawString("Paid By", this.ReceiptPaymentGridView.ColumnHeadersDefaultCellStyle.Font, br, r1, sf);
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