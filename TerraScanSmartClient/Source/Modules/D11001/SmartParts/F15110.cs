//--------------------------------------------------------------------------------------------
// <copyright file="F15110.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15110.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/05/07         S. Pradeep         Created
// 06/04/2009       Sadha Shivudu      TSCO 5748 - 15110 Receipt Action - New Field added
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using System.Drawing;

    /// <summary>
    /// F15110 Class file
    /// </summary>
    public partial class F15110 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the reverseDatatableCount
        /// </summary>
        private int reverseDatatableCount;

        /// <summary>
        /// form1555Control
        /// </summary>
        private F15110Controller form15110Control;

        /// <summary>
        ///  Used To Store receiptId
        /// </summary>
        private int receiptId;

        /// <summary>
        /// Store default text for receipt Action
        /// </summary>
        private string receiptAction = "Reverse Receipt";

        /// <summary>
        /// Used to store the featureClassId
        /// </summary>
        private int featureClassId;


        private int formNo;

        /// <summary>
        /// Created instance for the Typed Dataset
        /// </summary>
        private F15110ReceiptActionsData receiptActionsData = new F15110ReceiptActionsData();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// getForm1555DetailsData
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getForm1555DetailsData = new SupportFormData.GetFormDetailsDataTable();


        /// <summary>
        /// getForm1557DetailsData
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getForm1557DetailsData = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Set Default OpenPermission for Form 1555
        /// </summary>
        private bool form1555OpenPermission = false;

        /// <summary>
        /// Set Default OpenPermission for Form 1557
        /// </summary>
        private bool form1557OpenPermission = false;

        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Paid by User Name.
        /// </summary>
        public string paidByUsername;

        /// <summary>
        /// Paid by User Id.
        /// </summary>
        public string paidbyUserId;

        #endregion Form Slice Variables

        #endregion Variables

        F15110ReceiptActionsData.GetReceiptActionRow receiptActions;

        public F15110 f15110
        {
            get { return f15110; }
            set { f15110 = value; }
        }
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15110"/> class.
        /// </summary>
        public F15110()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class F15110.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15110(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.receiptId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.ReverseReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReverseReceiptPictureBox.Height, this.ReverseReceiptPictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15100"/> class.
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
        public F15110(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.receiptId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.ReverseReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReverseReceiptPictureBox.Height, this.ReverseReceiptPictureBox.Width, tabText, red, green, blue);
            this.featureClassId = featureClassID;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;
        
        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// FormSlice_EditEnabled
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// Used to store the receipt action types
        /// </summary>
        private enum ReceiptActionTypes
        {
            /// <summary>
            /// Type Reverse Receipt
            /// </summary>
            ReverseReceipt = 1,

            /// <summary>
            /// Type Reversed Receipt
            /// </summary>
            ReversedReceipt,

            /// <summary>
            /// Type Reversal Receipt
            /// </summary>
            ReversalReceipt
        }

        #endregion enum

        #region property

        /// <summary>
        /// Property instance for form controller
        /// </summary>
        [CreateNew]
        public F15110Controller Form15110Control
        {
            get { return this.form15110Control as F15110Controller; }
            set { this.form15110Control = value; }
        }

        #endregion property

        #region Event Subscription

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

                    if (this.reverseDatatableCount > 0)
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
                    this.receiptId = eventArgs.Data.SelectedKeyId;
                    this.GetReceiptActions();
                    ////commented to enable reverse receipt button enable.
                    ////this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #endregion Protected methods

        #region Private Methods

        /// <summary>
        /// Gets the receipt button actions and assign the color according to the revesal type
        /// </summary>
        private void GetReceiptActions()
        {
            this.receiptActionsData = this.form15110Control.WorkItem.F15110_GetReceiptActions(this.receiptId);
            this.reverseDatatableCount = this.receiptActionsData.GetReceiptAction.Rows.Count;
            if (this.reverseDatatableCount > 0)
            {
                receiptActions = (F15110ReceiptActionsData.GetReceiptActionRow)this.receiptActionsData.GetReceiptAction.Rows[0];
                this.DisplayReverseLinkLable(receiptActions);

                if (!string.IsNullOrEmpty(receiptActions.AddInterestAction.ToString()))
                {
                    if (receiptActions.AddInterestAction.Equals(1))
                    {
                        this.AddIntrestPaidButton.BackColor = Color.FromArgb(28,81,128);

                    }
                    else if (receiptActions.AddInterestAction.Equals(2))
                    {
                        this.AddIntrestPaidButton.BackColor = Color.FromArgb(128,0,0);

                    }
                    else if (receiptActions.AddInterestAction.Equals(3))
                    {
                        this.AddIntrestPaidButton.BackColor = Color.FromArgb(152,152,152);

                    }
                }
                if (receiptActions.ReceiptAction == (int)ReceiptActionTypes.ReverseReceipt)
                {
                    this.EnableControls(receiptActions, true);
                }
                else if (receiptActions.ReceiptAction == (int)ReceiptActionTypes.ReversedReceipt)
                {
                    this.EnableControls(receiptActions, false);
                }
                else if (receiptActions.ReceiptAction == (int)ReceiptActionTypes.ReversalReceipt)
                {
                    this.EnableControls(receiptActions, false);
                }
                if (receiptActions.IsAddInterestButtonEnabled.Equals(1))
                {
                    this.AddIntrestPaidButton.Enabled = true;
                }
                else
                {
                    this.AddIntrestPaidButton.Enabled = false;
                    this.AddIntrestPaidButton.ForeColor = Color.LightGray;
                }
                if (receiptActions.IsManagePaymentButtonEnabled.Equals(1))
                {
                    this.btnManagePayment.Enabled = true;
                    this.btnManagePayment.ForeColor = Color.White;
                    this.btnManagePayment.BackColor = Color.FromArgb(28, 81, 128);
                }
                else
                {
                    this.btnManagePayment.Enabled = false;
                    this.btnManagePayment.ForeColor = Color.LightGray;
                    this.btnManagePayment.BackColor = Color.FromArgb(152, 152, 152);
                }
                if (!string.IsNullOrEmpty(receiptActions.AddInterestText.ToString()))
                {
                    this.AddIntrestPaidButton.Text = receiptActions.AddInterestText.ToString();
                }
                
            }
            else
            {
                this.ReverseReceiptButton.StatusOnText = this.receiptAction;
                this.ReverseReceiptButton.StatusIndicator = true;
                this.ReverseReceiptLabel.Text = string.Empty;
                this.ReverseReceiptLinkLabel.Text = string.Empty;
                this.ReverseReceiptLinkLabel.FormId = 0;
                this.ReverseButtonPanel.Enabled = false;
                this.ReverseReceiptButton.Enabled = false;
                this.AddIntrestPaidButton.Enabled = false;
                this.btnManagePayment.Enabled = false;
                
            }
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="receiptActions">The receipt actions.</param>
        /// <param name="statusIndicator">if set to <c>true</c> [status indicator].</param>
        private void EnableControls(F15110ReceiptActionsData.GetReceiptActionRow receiptActions, bool statusIndicator)
        {
            this.receiptAction = receiptActions.ReceiptActionText;
            this.ReverseButtonPanel.Enabled = Convert.ToBoolean(receiptActions.IsButtonEnabled);
            if (statusIndicator == true)
            {
                this.ReverseReceiptButton.StatusOnText = this.receiptAction;
            }
            else
            {
                this.ReverseReceiptButton.StatusOffText = this.receiptAction;
            }

            this.ReverseReceiptButton.StatusIndicator = statusIndicator;
            this.ReverseReceiptButton.Enabled = Convert.ToBoolean(receiptActions.IsButtonEnabled);
        }

        /// <summary>
        /// Displays the reverse link lable.
        /// </summary>
        /// <param name="receiptActions">The receipt actions.</param>
        private void DisplayReverseLinkLable(F15110ReceiptActionsData.GetReceiptActionRow receiptActions)
        {
            switch (receiptActions.ReceiptAction)
            {
                case 1:
                default:
                    this.ReverseReceiptLabel.Text = string.Empty;
                    this.ReverseReceiptLinkLabel.Text = string.Empty;
                    this.ReverseReceiptLinkLabel.FormId = 0;
                    break;
                case 2:
                    this.ReverseReceiptLabel.Text = SharedFunctions.GetResourceString("ReversedBy");
                    this.ReverseReceiptLinkLabel.Text = receiptActions.ReverseReceiptLink;
                    this.ReverseReceiptLinkLabel.FormId = receiptActions.ReverseReceiptLinkID;
                    break;
                case 3:
                    this.ReverseReceiptLabel.Text = SharedFunctions.GetResourceString("ReversalOf");
                    this.ReverseReceiptLinkLabel.Text = receiptActions.ReverseReceiptLink;
                    this.ReverseReceiptLinkLabel.FormId = receiptActions.ReverseReceiptLinkID;
                    break;
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.ReverseButtonPanel.Enabled = !controlLook;
        }

        #endregion Private Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F15110 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F15110_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GetReceiptActions();
                this.ReverseReceiptButton.Focus();                    
                ////commented to enable reverse receipt button enable.
                ////this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the MouseEnter event of the ReverseReceiptPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReverseReceiptPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ReverseReceiptToolTip.SetToolTip(this.ReverseReceiptPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReverseReceiptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReverseReceiptButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.getForm1555DetailsData = this.form15110Control.WorkItem.GetFormDetails(Convert.ToInt32(1555), TerraScanCommon.UserId);
                if (this.getForm1555DetailsData.Rows.Count > 0)
                {
                    this.form1555OpenPermission = Convert.ToBoolean(this.getForm1555DetailsData.Rows[0][getForm1555DetailsData.IsPermissionOpenColumn.ColumnName].ToString());
                }

                this.Cursor = Cursors.WaitCursor;
                Form form1555 = new Form();
                this.receiptActionsData = this.form15110Control.WorkItem.F15110_GetReceiptActions(this.receiptId);
                object[] optionalParameter = new object[] { this.receiptId };
                form1555 = this.form15110Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1555, optionalParameter, this.form15110Control.WorkItem);

                if (form1555 != null && this.form1555OpenPermission == true)
                {
                    if (form1555.ShowDialog() == DialogResult.OK)
                    {
                        this.GetReceiptActions();
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + "ReverseReceipt Form", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ReverseReceiptLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ReverseReceiptLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.ReverseReceiptLinkLabel.FormId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        private void AddIntrestPaidButton_Click(object sender, EventArgs e)
        {
            if (this.receiptId > 0)
            {
                this.form15110Control.WorkItem.F1557_InsertRefundInterest(this.receiptId, TerraScanCommon.UserId);
               // SliceReloadActiveRecord sliceReloadActiveRecord;
               // sliceReloadActiveRecord.MasterFormNo = this.formNo;
               // sliceReloadActiveRecord.SelectedKeyId = this.receiptId;                
               // sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;               
               // this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

               // SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
               // sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
               // sliceUpdateActiveRecord.SelectedKeyId = this.receiptId;
               //// this.ImportID = returnValue;
               // this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));

               // this.D9030_F9030_LoadSliceDetails(this, sliceReloadActiveRecord);
               // SliceReloadActiveRecord sliceReloadActiveRecord;
                //sliceReloadActiveRecord.MasterFormNo = this.formNo;
                //sliceReloadActiveRecord.SelectedKeyId = this.keyId;
               // this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                //this.D9030_F9030_LoadSliceDetails(this, eventArgs);
                //this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sets));
               // this.D9030_F9030_ReloadAfterSave(this, eventArgs);
               // this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(this.receiptId));
                this.Refresh();
                this.GetReceiptActions();
                SendKeys.Send("{F5}");

               // this.KeyPress += new KeyPressEventHandler(AddIntrestPaidButton_Click);
                //System.Windows.Forms.SendKeys.Send("{F5}");
                
               // this.F15110_Load(sender, e);
            }
        }

        private void btnManagePayment_Click(object sender, EventArgs e)
        {

            this.getForm1557DetailsData = this.form15110Control.WorkItem.GetFormDetails(Convert.ToInt32(1557), TerraScanCommon.UserId);
            if (this.getForm1557DetailsData.Rows.Count > 0)
            {
                this.form1557OpenPermission = Convert.ToBoolean(this.getForm1557DetailsData.Rows[0][getForm1557DetailsData.IsPermissionOpenColumn.ColumnName].ToString());
            }
            Form form1557 = new Form();
            this.receiptActionsData = this.form15110Control.WorkItem.F15110_GetReceiptActions(this.receiptId);
            object[] optionalParameter = new object[3];
            optionalParameter[0] = this.receiptId;
            optionalParameter[1] = this.paidByUsername;
            optionalParameter[2] = this.paidbyUserId;
            form1557 = this.form15110Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1557, optionalParameter, this.form15110Control.WorkItem);

            if (form1557 != null && this.form1557OpenPermission == true)
            {
                if (form1557.ShowDialog() == DialogResult.OK)
                {
                    this.GetReceiptActions();
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + "ReverseReceipt Form", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (this.receiptId > 0)
            {
                this.form15110Control.WorkItem.F1557_InsertRefundInterest(this.receiptId, TerraScanCommon.UserId);
             
            }
        }

        public void DisableManagePaymentButton() 
        {
            this.btnManagePayment.Enabled = false;
            this.btnManagePayment.ForeColor = Color.LightGray;
            this.btnManagePayment.BackColor = Color.FromArgb(152, 152, 152);
        }

        public void EnableManagePaymentButton()      
        {
            if (receiptActions.IsManagePaymentButtonEnabled.Equals(1))
            {
                this.btnManagePayment.Enabled = true;
                this.btnManagePayment.ForeColor = Color.White;
                this.btnManagePayment.BackColor = Color.FromArgb(28, 81, 128);
            }
        }

        /// <summary>
        /// Fill Paid by UserName.
        /// </summary>
        /// <param name="paidusername"></param>
        public void FillUserName(string paidbyUserID, string paiduserName)
        {
            paidbyUserId = paidbyUserID;
            paidByUsername = paiduserName;
        }
    }
}
