//--------------------------------------------------------------------------------------------
// <copyright file="F1555.cs" company="Congruent">
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
// 04/05/2007       S. Pradeep         Reverse receipt created 
// 03/04/2009       M Sadha Shivudu    TSCO 5847 - Make Reverse Date Editable
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Collections;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
   

    /// <summary>
    /// Partial class for F1555
    /// </summary>
    public partial class F1555 : Form
    {
        #region Variables

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private int selectedownerId = -1;

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private string selectedownerName;

        /// <summary>
        ///  Used To Store PostTypeId
        /// </summary>
        private byte postTypeId = 50;

        /// <summary>
        /// Instance for the row of the typed dataeset
        /// </summary>
        private F1555_ReceiptDetailsData.GetReceiptDetailsRow receiptDetail, loadReceiptDetail;

        /// <summary>
        ///  Used To check searchResult
        /// </summary>
        private bool searchResult;

        /// <summary>
        /// To store the statement Id
        /// </summary>
        private int statementId;

        /// <summary>
        /// To store the receipt source Id
        /// </summary>
        private int receiptSourceId = 9;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private PartiesOwnerDetailsData ownerDetailDataSet = new PartiesOwnerDetailsData();

        /// <summary>
        /// Created instance for the Typed Dataset
        /// </summary>
        private F1555_ReceiptDetailsData receiptDetailsData = new F1555_ReceiptDetailsData();

        /// <summary>
        /// Created instance for the Typed Dataset
        /// </summary>
        private F1555_ReceiptDetailsData loadReceiptDetailsData = new F1555_ReceiptDetailsData();

        /// <summary>
        /// form1555Control
        /// </summary>
        private F1555Controller form1555Control;

        /// <summary>
        ///  Used To Store receiptId
        /// </summary>
        private int receiptId;

        /// <summary>
        ///  Used To Store receiptNumber
        /// </summary>
        private string receiptNumber;

        #region Form Slice Variables

        #endregion Form Slice Variables

        /// <summary>
        /// Creating instance for CommonData typed dataset
        /// </summary>
        private CommonData dispositionDatas = new CommonData();

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;
        
        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new class.
        /// </summary>
        public F1555()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1555"/> class.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        public F1555(int receiptId)
        {
            InitializeComponent();
            this.receiptId = receiptId;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// Used to store the disposition categories
        /// </summary>
        private enum DispositionValues
        {
            ///// <summary>
            ///// Type select of the Disposition Values
            ///// </summary>
            //Select = 0,

            /// <summary>
            /// Type Refund of the Disposition Values
            /// </summary>
            Refund = 1,

            /// <summary>
            /// Type Suspend of the Disposition Values
            /// </summary>
            Suspend = 2,

            /// <summary>
            /// Type Remove of the Disposition Values
            /// </summary>
            Remove = 3
        }

        #endregion Enum

        #region Properties

        /// <summary>
        /// Gets or sets the reverse receipt controll.
        /// </summary>
        /// <value>The reverse receipt template controll.</value>
        [CreateNew]
        public F1555Controller F1555Controll
        {
            get { return this.form1555Control as F1555Controller; }
            set { this.form1555Control = value; }
        }

        #endregion Properties

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

        #endregion Protected Methods

        #region Events

        /// <summary>
        /// Handles the Click event of the ReverseCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReverseCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PictureReceipt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PictureReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                Form parcelF9101 = new Form();
                parcelF9101 = this.form1555Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form1555Control.WorkItem);

                if (parcelF9101 != null)
                {
                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                    {
                        Int32 resultownerid;
                        Int32.TryParse((TerraScanCommon.GetValue(parcelF9101, SharedFunctions.GetResourceString("MasterNameOwnerId"))), out resultownerid);
                        this.selectedownerId = resultownerid;
                        this.ownerDetailDataSet = this.form1555Control.WorkItem.GetOwnerDetails(this.selectedownerId);
                        this.selectedownerName = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                        this.ToOwnerTextBox.Text = this.selectedownerName;
                        this.searchResult = true;
                    }
                    else
                    {
                        this.searchResult = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F1555 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1555_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageLoadStatus = true;
                this.getFormDetailsDataDetails = this.form1555Control.WorkItem.GetFormDetails(Convert.ToInt32(this.Tag), TerraScanCommon.UserId);
                this.slicePermissionField.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
                this.slicePermissionField.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
                this.slicePermissionField.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                this.slicePermissionField.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
                this.LoadValues();
                this.EnableRevereseButton();

                int dispositionComboValue = 0;
                if (this.DispositionComboBox.SelectedValue != null)
                {
                    int.TryParse(this.DispositionComboBox.SelectedValue.ToString(), out dispositionComboValue);
                }
                //code reversed for C0 #10148
                // Code commented for CO #8508
                //if (dispositionComboValue == (int)DispositionValues.Remove)
                //{
                    this.ApplyFeePanel.Enabled = this.slicePermissionField.editPermission;
                    this.ApplyFeeCheckBox.Checked = false;
                //}
                //else
                //{
                //    this.ApplyFeePanel.Enabled = false;
                //    this.ApplyFeeCheckBox.Checked = false;
                //}

                /*if (dispositionComboValue == (int)DispositionValues.Select || dispositionComboValue == (int)DispositionValues.Remove)
                {
                    this.PictureReceiptButton.Enabled = false;
                }*/

                //code reversed for co 10148
                // Disable ApplyFee and Owner textbox for CO #8508
                //this.DisableControls();

                this.pageLoadStatus = false;
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
        /// Disable ApplyFee and Owner textbox for CO #8508
        /// </summary>
        private void DisableControls()
        {
            this.ApplyFeePanel.Enabled = false;
            this.ToOwnerPanel.Enabled = false;
            this.ToOwnerTextBox.Text = SharedFunctions.GetResourceString("NotApplicable");
            this.ApplyFeeCheckBox.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the ReverseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReverseButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool continueReverse = true;
                if ((int)this.SharedReceiptCombo.SelectedValue > 0)
                {
                    Form sharedReceiptForm = new Form();
                    sharedReceiptForm = this.form1555Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1556, new object[] { this.receiptId, this.SharedReceiptCombo.SelectedValue }, this.form1555Control.WorkItem);

                    if (sharedReceiptForm != null)
                    {
                        if (sharedReceiptForm.ShowDialog() == DialogResult.Yes)
                        {
                            continueReverse = true;
                        }
                        else
                        {
                            continueReverse = false;
                        }
                    }
                }

                if (continueReverse)
                {
                    //if (!string.IsNullOrEmpty(this.ToOwnerTextBox.Text.Trim()))
                    //{
                        this.Cursor = Cursors.WaitCursor;
                        F1555_ReceiptDetailsData.ReverseDetailsRow reverseDetails = this.receiptDetailsData.ReverseDetails.NewReverseDetailsRow();

                        reverseDetails.ReceiptID = this.receiptId;
                        reverseDetails.UserID = TerraScanCommon.UserId;

                        int dispositionComboValue = 0;
                        if (this.DispositionComboBox.SelectedValue != null)
                        {
                            int.TryParse(this.DispositionComboBox.SelectedValue.ToString(), out dispositionComboValue);
                        }

                        if (dispositionComboValue.Equals((int)DispositionValues.Remove))
                        {
                            this.selectedownerId = 0;
                        }

                        reverseDetails.OwnerID = this.selectedownerId;

                        reverseDetails.Disposition = dispositionComboValue;
                        reverseDetails.Reason = this.CommentTextBox.Text.Trim();
                        reverseDetails.PostTypeID = Convert.ToByte(this.loadReceiptDetail.PostTypeID);

                        if (!string.IsNullOrEmpty(this.ReverseDateTextBox.Text.Trim()))
                        {
                            reverseDetails.ReverseDate = this.ReverseDateTextBox.Text;
                        }

                        this.receiptDetailsData.ReverseDetails.Rows.Add(reverseDetails);

                        int? sharePaymentId = null;
                        if (this.SharedReceiptCombo.SelectedValue != null)
                        {
                            sharePaymentId = (int?)this.SharedReceiptCombo.SelectedValue;
                        }

                        this.form1555Control.WorkItem.F1555_SaveMasterReceipt(0, this.receiptSourceId, (Utility.GetXmlString(this.receiptDetailsData.ReverseDetails.Copy())), sharePaymentId);
                    //code reversed for the co #10148
                    // Code commented for CO #8508
                        if (ApplyFeeCheckBox.Checked)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(11050);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = this.statementId;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                            ////To close the current form
                            this.Close();
                        }

                        SliceReloadActiveRecord currentSliceInfo;
                        currentSliceInfo.MasterFormNo = 11001;
                        currentSliceInfo.SelectedKeyId = this.receiptId;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                   /* }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("OwnerRequiredField"), ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }*/
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
        /// Toes the enable reverse button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToEnableReverseButton(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.EnableRevereseButton();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DispositionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DispositionComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.EnableRevereseButton();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the CommentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CommentTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.slicePermissionField.editPermission)
            {
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }

                    case (char)10:
                        {
                            e.Handled = true;
                            break;
                        }

                    case (char)9:
                        {
                            e.Handled = true;
                            break;
                        }   
                }
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DispositionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DispositionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.EnableRevereseButton();
                }
                //code reversed for the co #10148
                // Code commented for CO #8508
                
                Int32 dispositionComboValue = 0;
                if (this.DispositionComboBox.SelectedValue != null)
                {
                    Int32.TryParse(this.DispositionComboBox.SelectedValue.ToString(), out dispositionComboValue);
                }

                //if (dispositionComboValue == (int)DispositionValues.Remove)
                //{
                    this.ApplyFeePanel.Enabled = this.slicePermissionField.editPermission;
                //}
                //else
                //{
                //    this.ApplyFeePanel.Enabled = false;
                //    this.ApplyFeeCheckBox.Checked = false;
                //}

                ///
                ///since for every disposition value also after checkbbox check moved to Fee Managemente form
                ///
                
                // Code has been replaced to fix the issue #5765 Reverse Receipt - "Apply Fee" - wrong StatementID
                //this.receiptDetailsData = this.form1555Control.WorkItem.GetReceiptDetails(this.receiptId);
                //if (this.receiptDetailsData.GetReceiptDetails.Rows.Count > 0 && !this.searchResult)
                //{
                //    this.receiptDetail = (F1555_ReceiptDetailsData.GetReceiptDetailsRow)this.receiptDetailsData.GetReceiptDetails.Rows[0];
                //    this.selectedownerName = this.receiptDetail.OwnerName;
                //    this.selectedownerId = this.receiptDetail.OwnerID;
                //    this.statementId = this.receiptDetail.StatementID;
                //}

               /*  //code reversed for the co #10148
                if (dispositionComboValue == (int)DispositionValues.Refund || dispositionComboValue == (int)DispositionValues.Suspend)
                {
                    //this.ToOwnerLabel.Enabled = true;
                    //this.ToOwnerTextBox.Enabled = true;
                    //this.PictureReceiptButton.Enabled = true;
                    ////this.ToOwnerTextBox.Text = this.selectedownerName;commented since it is the primitive code
                    //this.selectedownerName = string.IsNullOrEmpty(this.selectedownerName) == true ? string.Empty : this.selectedownerName;
                    //this.ToOwnerTextBox.Text = this.selectedownerName;
                }
                else
                {
                    this.ToOwnerLabel.Enabled = false;
                    this.ToOwnerTextBox.Enabled = false;
                    this.PictureReceiptButton.Enabled = false;
                    this.searchResult = false;
                    this.ToOwnerTextBox.Text = SharedFunctions.GetResourceString("NotApplicable");
                    this.selectedownerId = 0;
                    this.selectedownerName = string.Empty; ////added inorder to check the primitive code
                }*/
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ReverseDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReverseDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ReverseDateTextBox.Text.Trim()))
                {
                    this.ReverseDateTextBox.Text = DateTime.Now.ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReverseDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReverseDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ReverseDateTextBox.Text.Trim()))
                {
                    this.ReverseDateTimePicker.Value = Convert.ToDateTime(this.ReverseDateTextBox.Text);
                }
                else
                {
                    this.ReverseDateTimePicker.Value = DateTime.Today;
                }

                this.ReverseDateTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the ReverseDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReverseDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.ReverseDateTextBox.Text = this.ReverseDateTimePicker.Text;
                this.ReverseDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the ReverseDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ReverseDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode.Equals(9))
                {
                    SendKeys.Send("{Esc}");
                }

                this.CommentTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events

        #region Private Methods

        /// <summary>
        /// To load the combo box
        /// </summary>
        private void LoadDispositionValue()
        {
            Hashtable datasDisposition = new Hashtable();
            
            // added to check the posttypeid whether to load all 3 values into combo or just refund alone
            this.loadReceiptDetailsData = this.form1555Control.WorkItem.GetReceiptDetails(this.receiptId);

            this.loadReceiptDetail = (F1555_ReceiptDetailsData.GetReceiptDetailsRow)this.loadReceiptDetailsData.GetReceiptDetails.Rows[0];
            this.ReceiptNoTextBox.Text = this.loadReceiptDetail.ReceiptNumber;
            this.DispositionComboBox.DisplayMember = this.loadReceiptDetailsData.DispositionList.DispositionColumn.ColumnName;
            this.DispositionComboBox.ValueMember = this.loadReceiptDetailsData.DispositionList.DispositionIDColumn.ColumnName;
            this.DispositionComboBox.DataSource = this.loadReceiptDetailsData.DispositionList;
            if (this.loadReceiptDetailsData.DispositionList.Rows.Count > 0)
            {
              this.DispositionComboBox.SelectedIndex = 0;
            }

            if (this.loadReceiptDetailsData.GetReceiptDetails.Rows.Count > 0 && !this.searchResult)
            {
                this.receiptDetail = (F1555_ReceiptDetailsData.GetReceiptDetailsRow)this.loadReceiptDetailsData.GetReceiptDetails.Rows[0];
                this.selectedownerName = this.receiptDetail.OwnerName;
                this.selectedownerId = this.receiptDetail.OwnerID;
                this.statementId = this.receiptDetail.StatementID;
            }

            // Load shared payment combo
            this.LoadSharedPaymentCombo();

        // Code has been commented for CO #7208
        /*
            if (this.loadReceiptDetail.PostTypeID == this.postTypeId)
            {
                datasDisposition.Add(SharedFunctions.GetResourceString("Refund"), (int)DispositionValues.Refund);
            }
            else if (this.loadReceiptDetail.PPaymentID == 0)
            {
                datasDisposition.Add(SharedFunctions.GetResourceString("Remove"), (int)DispositionValues.Remove);
            }
            else
            {
                datasDisposition.Add(SharedFunctions.GetResourceString("Remove"), (int)DispositionValues.Remove);
                datasDisposition.Add(SharedFunctions.GetResourceString("Suspend"), (int)DispositionValues.Suspend);
                datasDisposition.Add(SharedFunctions.GetResourceString("Refund"), (int)DispositionValues.Refund);
            }

            this.dispositionDatas.LoadGeneralComboData(datasDisposition);
            DataRow dr;
            dr = this.dispositionDatas.ComboBoxDataTable.NewRow();
            dr[0] = 0;
            dr[1] = SharedFunctions.GetResourceString("Select");
            this.dispositionDatas.ComboBoxDataTable.Rows.InsertAt(dr, 0);
            this.DispositionComboBox.DisplayMember = this.dispositionDatas.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.DispositionComboBox.ValueMember = this.dispositionDatas.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.dispositionDatas.ComboBoxDataTable.DefaultView.Sort = this.dispositionDatas.ComboBoxDataTable.KeyIdColumn.ColumnName + " ASC";
            this.DispositionComboBox.DataSource = this.dispositionDatas.ComboBoxDataTable.DefaultView;*/
        }

        /// <summary>
        /// Load Shared Payment combo list
        /// </summary>
        private void LoadSharedPaymentCombo()
        {
            this.SharedReceiptCombo.DisplayMember = this.loadReceiptDetailsData.SharedPaymentList.SharedPaymentColumn.ColumnName;
            this.SharedReceiptCombo.ValueMember = this.loadReceiptDetailsData.SharedPaymentList.SharedPaymentIDColumn.ColumnName;
            this.SharedReceiptCombo.DataSource = this.loadReceiptDetailsData.SharedPaymentList;
            if (this.loadReceiptDetailsData.SharedPaymentList.Rows.Count > 0)
            {
                this.SharedReceiptCombo.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// To load the controls on load 
        /// </summary>
        private void LoadValues()
        {
            this.ReverseDateTextBox.Text = DateTime.Now.ToShortDateString();
            this.ReversedByTextBox.Text = TerraScanCommon.UserName;
            this.ReverseDateTextBox.Focus();
            this.ToOwnerTextBox.Text = SharedFunctions.GetResourceString("NotApplicable");
            this.LoadDispositionValue();
        }

        /// <summary>
        /// Method to enable the reverse button
        /// </summary>
        private void EnableRevereseButton()
        {
            this.CheckEditPermission();

            if ((!string.IsNullOrEmpty(this.ReverseDateTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.CommentTextBox.Text.Trim())) && (this.DispositionComboBox.SelectedIndex >= 0))
            {
                this.ReverseButton.Enabled = true;
            }
            else
            {
                this.ReverseButton.Enabled = false;
            }

            int dispositionComboValue = 0;
            if (this.DispositionComboBox.SelectedValue != null)
            {
                int.TryParse(this.DispositionComboBox.SelectedValue.ToString(), out dispositionComboValue);
            }

            int postType;
            if (this.receiptDetail == null)
            {
                postType = -1;
            }
            else
            {
                postType = this.receiptDetail.PostTypeID;
            }
            //// if (dispositionComboValue == (int)DispositionValues.Suspend && this.receiptDetail.PostTypeID == this.postTypeId)
            if (dispositionComboValue == (int)DispositionValues.Suspend && postType == this.postTypeId)
            {
                this.ReverseButton.Enabled = false;
            }
            //code reversed for the co #10148
            // Code commented for CO #8508
             //if (dispositionComboValue == (int)DispositionValues.Remove)
             //{
                 this.ApplyFeePanel.Enabled = this.slicePermissionField.editPermission;
             //}
             //else
             //{
                 //this.ApplyFeePanel.Enabled = false;
                 //this.ApplyFeeCheckBox.Checked = false;
             //}

            /* if (dispositionComboValue == (int)DispositionValues.Select || dispositionComboValue == (int)DispositionValues.Remove)
             {
                 this.PictureReceiptButton.Enabled = false;
             }*/
        }

        /// <summary>
        /// Checks the edit permission.
        /// </summary>
        private void CheckEditPermission()
        {
            if (this.slicePermissionField.editPermission)
            {
                this.ReversalReasonPanel.Enabled = true;
                this.DispositionPanel.Enabled = true;
                this.ToOwnerPanel.Enabled = true;
                this.CommentTextBox.LockKeyPress = false;
                this.DispositionComboBox.Enabled = true;
                //code reversed for the co #10148
                this.ApplyFeeCheckBox.Enabled = true;
                this.ReverseDateTextBox.LockKeyPress = false;
                //this.PictureReceiptButton.Enabled = true;
                this.ReverseButton.Enabled = true;
                this.ApplyFeeLabel.Enabled = true;
            }
            else
            {
                this.ReversalReasonPanel.Enabled = false;
                this.DispositionPanel.Enabled = false;
                this.ToOwnerPanel.Enabled = false;
                this.CommentTextBox.LockKeyPress = true;
                this.DispositionComboBox.Enabled = false;
                //code reversed for the co #10148
                //this.ApplyFeeCheckBox.Enabled = false;
                this.ReverseDateTextBox.LockKeyPress = true;
                //this.PictureReceiptButton.Enabled = false;
                this.ReverseButton.Enabled = false;
                //this.ApplyFeeLabel.Enabled = false;
            }
        }

        #endregion Private Methods       
    }
}