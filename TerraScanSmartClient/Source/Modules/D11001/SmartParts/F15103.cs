//--------------------------------------------------------------------------------------------
// <copyright file="F15103.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15103.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  04 Jan 06       KUPPUSAMY.B         Created
//*********************************************************************************/

namespace D11001
{
    #region NameSpace

    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    #endregion NameSpace

    /// <summary>
    /// F15103 Class
    /// </summary>
    public partial class F15103 : BaseSmartPart
    {
        #region PrivateMembers

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance for F15100Controller
        /// </summary>
        private F15103Controller form15103Control;

        /// <summary>
        /// OptionalParameter
        /// </summary>
        private int rowCount;

        /// <summary>
        /// ReceiptItems class
        /// </summary>
        private F15103ReceiptOwnersData form15103ReceiptOwnersData;

        /// <summary>
        /// ReceiptHeaderData class
        /// </summary>
        private F15100ReceiptHeaderData form15100RecieptHeaderData;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// sectionIndicatorTabText
        /// </summary>
        private string sectionIndicatorTabText;

        /// <summary>
        /// Owner Name
        /// </summary>
        public string ownerName = string.Empty;

        /// <summary>
        /// Owner ID.
        /// </summary>
        public string paidownerID = string.Empty;

        /// <summary>
        /// Instance for F15110
        /// </summary>
        F15110 f15110;

        #endregion PrivateMembers

        #region Constructor

        /// <summary>
        /// Intializes component
        /// </summary>
        public F15103()
        {
            this.InitializeComponent();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15101"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The featureclass id</param>
        public F15103(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ReceiptOwnerspicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptOwnerspicturebox.Height, this.ReceiptOwnerspicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            this.form15103ReceiptOwnersData = new F15103ReceiptOwnersData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }
        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F15103Control
        /// </summary>
        [CreateNew]
        public F15103Controller Form15103Control
        {
            get { return this.form15103Control as F15103Controller; }
            set { this.form15103Control = value; }
        }
        #endregion Property

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
                this.Height = this.ReceiptOwnerspicturebox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
                sliceResize.SliceFormHeight = this.ReceiptOwnerspicturebox.Height;
                this.ReceiptOwnerspicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptOwnerspicturebox.Height, this.ReceiptOwnerspicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
        }

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
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

                    //if (this.keyId != eventArgs.Data.KeyId)
                    //{
                    //    ////To check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
                    //    this.form15100RecieptHeaderData = this.form15103Control.WorkItem.GetReceiptHeaderDetails(eventArgs.Data.KeyId);
                    //}

                    byte isValidRecord = 1;
                    byte.TryParse(this.form15103ReceiptOwnersData.ValidRecord.Rows[0][this.form15103ReceiptOwnersData.ValidRecord.IsValidColumn].ToString(), out isValidRecord);
                    ////To check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
                    if (isValidRecord > 0)
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
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription LoadSliceDetails
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.ReceiptOwnersDataGridView.Refresh();
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.CustomizeReceiptOwnersGrid();
                    this.rowCount = this.form15103ReceiptOwnersData.ListReceiptOwners.Rows.Count;
                    if (this.rowCount > 0)
                    {
                        this.ReceiptOwnersDataGridView.DataSource = this.form15103ReceiptOwnersData.ListReceiptOwners.DefaultView;
                    }
                    else
                    {
                        this.form15103ReceiptOwnersData.ListReceiptOwners.Clear();
                        this.ReceiptOwnersDataGridView.DataSource = this.form15103ReceiptOwnersData.ListReceiptOwners.DefaultView;
                    }

                    this.ReceiptOwnersDataGridView.Refresh();
                    this.SetSmartPartHeight(this.rowCount);
                    this.ReceiptOwnerspicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptOwnerspicturebox.Height, this.ReceiptOwnerspicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                    this.Height = this.ReceiptOwnerspicturebox.Height;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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

        #endregion Event Subscription

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
            }
        }

        #endregion Protected methods

        #region Event Handlers
        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e"> EventArgs</param>
        private void F15103_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeReceiptOwnersGrid();
                this.rowCount = this.form15103ReceiptOwnersData.ListReceiptOwners.Rows.Count;
                this.ReceiptOwnersDataGridView.DataSource = this.form15103ReceiptOwnersData.ListReceiptOwners.DefaultView;
                this.ReceiptOwnersDataGridView.Rows[0].Selected = false;
                this.SetSmartPartHeight(this.rowCount);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReceiptOwnerspicturebox control
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">EventArgs</param>
        private void ReceiptOwnerspicturebox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D11001.F15103"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ReceiptOwnersDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ReceiptOwnersDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.ReceiptOwnersDataGridView.Columns["OwnerName"].Index)
                {
                    if (this.ReceiptOwnersDataGridView.Rows[e.RowIndex].Selected || this.ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells["OwnerName".ToString()] as DataGridViewLinkCell).LinkColor = Color.White;
                        (ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells["OwnerName".ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells["OwnerName".ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells["OwnerName".ToString()] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells["OwnerName".ToString()] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells["OwnerName".ToString()] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the ReceiptOwnersDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReceiptOwnersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.ReceiptOwnersDataGridView.Columns["OwnerName"].Index)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    if (e.RowIndex >= 0)
                    {
                        formInfo = TerraScanCommon.GetFormInfo(91000);
                        formInfo.optionalParameters = new object[] { this.ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells[this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerIDColumn.ColumnName].Value };
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        ////object[] optionalParameter = new object[] { this.ReceiptOwnersDataGridView.Rows[e.RowIndex].Cells[this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerIDColumn.ColumnName].Value };
                        ////Form receiptOwners = new Form();
                        ////receiptOwners = TerraScanCommon.GetForm(91000, optionalParameter, this.form15103Control.WorkItem);
                        ////if (receiptOwners != null)
                        ////{
                        ////    receiptOwners.ShowDialog();
                        ////}
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
        /// Handles the MouseEnter event of the ReceiptOwnerspicturebox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptOwnerspicturebox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptOwnersToolTip.SetToolTip(this.ReceiptOwnerspicturebox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ReceiptOwnersDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ReceiptOwnersDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.ReceiptOwnersDataGridView.OriginalRowCount > 0)
                {
                    this.ReceiptOwnersDataGridView.Rows[0].Selected = true;
                }
                else
                {
                    this.ReceiptOwnersDataGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Event Handlers

        #region Private Methods
        /// <summary>
        /// Customise ReceiptOwners grid.
        /// </summary>
        private void CustomizeReceiptOwnersGrid()
        {
            this.ReceiptOwnersDataGridView.AllowUserToResizeColumns = false;
            this.ReceiptOwnersDataGridView.AllowUserToResizeRows = false;
            this.ReceiptOwnersDataGridView.AutoGenerateColumns = false;
            this.ReceiptOwnersDataGridView.StandardTab = true;
            this.ReceiptOwnersDataGridView.Columns[this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerIDColumn.ColumnName].DataPropertyName = this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerIDColumn.ColumnName;
            this.ReceiptOwnersDataGridView.Columns[this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerNameColumn.ColumnName].DataPropertyName = this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerNameColumn.ColumnName;
            this.ReceiptOwnersDataGridView.Columns[this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerPercentColumn.ColumnName].DataPropertyName = this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerPercentColumn.ColumnName;
            this.ReceiptOwnersDataGridView.Columns[this.form15103ReceiptOwnersData.ListReceiptOwners.IsBilledColumn.ColumnName].DataPropertyName = this.form15103ReceiptOwnersData.ListReceiptOwners.IsBilledColumn.ColumnName;
            this.ReceiptOwnersDataGridView.Columns[this.form15103ReceiptOwnersData.ListReceiptOwners.IsProratedColumn.ColumnName].DataPropertyName = this.form15103ReceiptOwnersData.ListReceiptOwners.IsProratedColumn.ColumnName;
            this.ReceiptOwnersDataGridView.Columns[this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerOrderColumn.ColumnName].DataPropertyName = this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerOrderColumn.ColumnName;
            this.ReceiptOwnersDataGridView.PrimaryKeyColumnName = this.form15103ReceiptOwnersData.ListReceiptOwners.OwnerIDColumn.ColumnName;
            this.form15103ReceiptOwnersData = this.form15103Control.WorkItem.ListReceiptOwners(this.keyId);

            ////To check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
           // this.form15100RecieptHeaderData = this.form15103Control.WorkItem.GetReceiptHeaderDetails(this.keyId);

            this.SetSmartPartHeight(this.form15103ReceiptOwnersData.ListReceiptOwners.Rows.Count);

            if (this.form15103ReceiptOwnersData.ListReceiptOwners.Rows.Count > 0)
            {
                paidownerID = this.form15103ReceiptOwnersData.ListReceiptOwners[0].OwnerID.ToString();
                ownerName = this.form15103ReceiptOwnersData.ListReceiptOwners[0].OwnerName.ToString();
            }
            else 
            {
                paidownerID = string.Empty;
                ownerName = string.Empty;
            }

            if (this.form15103ReceiptOwnersData.ListReceiptOwners.Rows.Count > 6)
            {
                this.ReceiptOwnersVscrollBar.Visible = false;
            }
            else
            {
                this.ReceiptOwnersVscrollBar.Visible = true;
            }
            this.GetPaidByOwnerName();
        }

        // <summary>
        /// Get the paid by owner name for the payment form.
        /// </summary>
        private void GetPaidByOwnerName()
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
                    f15110.FillUserName(paidownerID , ownerName);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 3)
            {
                if (recordCount > 6)
                {
                    recordCount = 6;
                }

                int increment = ((recordCount - 3) * 22);
                this.ReceiptOwnersDataGridView.Height = 88 + increment;
                this.ReceiptOwnerspanel.Height = this.ReceiptOwnersDataGridView.Height;
                this.ReceiptOwnersVscrollBar.Height = 88 + increment - 2;
                this.ReceiptOwnerspicturebox.Height = 88 + increment;
                this.ReceiptOwnerspicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptOwnerspicturebox.Height, this.ReceiptOwnerspicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                this.ReceiptOwnersDataGridView.NumRowsVisible = recordCount;
                this.Height = this.ReceiptOwnerspicturebox.Height;
            }
            else
            {
                this.ReceiptOwnersDataGridView.Height = 88;
                this.ReceiptOwnerspanel.Height = this.ReceiptOwnersDataGridView.Height;
                this.ReceiptOwnersVscrollBar.Height = this.ReceiptOwnersDataGridView.Height - 1;
                this.ReceiptOwnerspicturebox.Height = 88;
                this.ReceiptOwnersDataGridView.NumRowsVisible = 3;
                this.Height = 88;
            }
            ////this.ReceiptOwnersDataGridView.DataSource = this.form15103ReceiptOwnersData.ListReceiptOwners;
        }

        #endregion Private Methods
    }
}
