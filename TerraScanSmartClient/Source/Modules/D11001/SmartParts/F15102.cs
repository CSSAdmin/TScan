//--------------------------------------------------------------------------------------------
// <copyright file="F15102.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15102.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  02 Jan 07       KUPPUSAMY.B         Created
//*********************************************************************************/

namespace D11001
{
    #region NameSpace

    using System;
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
    /// F15100 class
    /// </summary>
    public partial class F15102 : BaseSmartPart
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
        /// ReceiptHeaderData class
        /// </summary>
        private F15100ReceiptHeaderData form15100RecieptHeaderData;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance for F15102Controller
        /// </summary>
        private F15102Controller form15102Control;

        /// <summary>
        /// ReceiptStatementHeaderData class
        /// </summary>
        private F15102ReceiptStatementHeaderData form15102ReceiptStatementHeaderData;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails;

        #endregion PrivateMembers

        #region Constructor

        /// <summary>
        /// Initializes component
        /// </summary>
        public F15102()
        {
            this.InitializeComponent();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15102"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15102(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.StmtPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StmtPictureBox.Height, this.StmtPictureBox.Width, tabText, red, green, blue);
            this.form15102ReceiptStatementHeaderData = new F15102ReceiptStatementHeaderData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15102"/> class.
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
        public F15102(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.StmtPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StmtPictureBox.Height, this.StmtPictureBox.Width, tabText, red, green, blue);
            this.form15102ReceiptStatementHeaderData = new F15102ReceiptStatementHeaderData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        #endregion Constructor

        #region Event Publication

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
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property
        ///<summary>
        ///Gets or sets the F15102control
        ///</summary>
        [CreateNew]
        public F15102Controller Form15102Control
        {
            get { return this.form15102Control as F15102Controller; }
            set { this.form15102Control = value; }
        }
        #endregion Property

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

                    //if (this.keyId != eventArgs.Data.KeyId)
                    //{
                    //    ////To check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
                    //    this.form15100RecieptHeaderData = this.form15102Control.WorkItem.GetReceiptHeaderDetails(eventArgs.Data.KeyId);
                    //}

                    byte isValidRecord = 1;
                    byte.TryParse(this.form15102ReceiptStatementHeaderData.ValidRecord.Rows[0][this.form15102ReceiptStatementHeaderData.ValidRecord.IsValidColumn].ToString(), out isValidRecord);
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
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.ShowPanel(true);
                    this.GetReceiptStatementHeaderDetails();
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

        #endregion Protected methods

        #region Event Handlers
        /// <summary>
        /// F15102 form load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F15102_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.ShowPanel(true);
                this.GetReceiptStatementHeaderDetails();
                ////this.form15102ReceiptStatementHeaderData = this.form15102Control.WorkItem.GetReceiptStatementHeaderDetails(this.keyId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the stmtPictureBox control
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">EventArgs</param>
        private void StmtPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D11001.F15102"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the StatementLinkLabel
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The instance containing the event data.</param>
        private void StatementLinkLabel_Click(object sender, EventArgs e)
        {
        }

        
        /// <summary>
        /// Handles the MouseEnter event of the StmtPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StmtPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.StmtToolTip.SetToolTip(this.StmtPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the StatementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void StatementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.StatementLinkLabel.Text.Trim()))
                {
                    Form receiptStatement = new Form();
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(Convert.ToInt32(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.ReceiptFormColumn]));
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.StatementIDColumn];
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Event Handlers

        #region Private methods

        /// <summary>
        /// method to get the Receipt Statement Header details
        /// </summary>
        private void GetReceiptStatementHeaderDetails()
        {
            ////To check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
            // this.form15100RecieptHeaderData = this.form15102Control.WorkItem.GetReceiptHeaderDetails(this.keyId);

            this.form15102ReceiptStatementHeaderData = this.form15102Control.WorkItem.GetReceiptStatementHeaderDetails(this.keyId);

            if (this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.StatementNumberColumn].ToString()))
                {
                    this.StatementLinkLabel.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.StatementNumberColumn].ToString();
                }
                else
                {
                    this.StatementLinkLabel.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.RollYearColumn].ToString()))
                {
                    this.RollYearTextBox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.RollYearColumn].ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.ParcelNumberColumn].ToString()))
                {
                    ////Modified by Biju on 18/May/2010 to implement #6886
                    this.ParcelNumberTextbox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.ParcelNumberColumn].ToString();
                }
                else
                {
                    ////Modified by Biju on 18/May/2010 to implement #6886
                    this.ParcelNumberTextbox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.DateCreatedColumn].ToString()))
                {
                    this.DateCreatedTextBox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.DateCreatedColumn].ToString();
                }
                else
                {
                    this.DateCreatedTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.PostNameColumn].ToString()))
                {
                    this.PostTypeTextBox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.PostNameColumn].ToString();
                }
                else
                {
                    this.PostTypeTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.TaxAmountColumn].ToString()))
                {
                    this.TaxAmountTextBox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.TaxAmountColumn].ToString();
                }
                else
                {
                    this.TaxAmountTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.OutstandingFeesColumn].ToString()))
                {
                    this.OutstandingFeesTextBox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.OutstandingFeesColumn].ToString();
                }
                else
                {
                    this.OutstandingFeesTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.PaidFeesColumn].ToString()))
                {
                    this.PaidFeesTextBox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.PaidFeesColumn].ToString();
                }
                else
                {
                    this.PaidFeesTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.TotalAmountColumn].ToString()))
                {
                    this.TotalAmountTextBox.Text = this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.Rows[0][this.form15102ReceiptStatementHeaderData.GetReceiptStatementHeader.TotalAmountColumn].ToString();
                }
                else
                {
                    this.TotalAmountTextBox.Text = string.Empty;
                }
            }
            else
            {
                this.ShowPanel(false);
                this.ClearDetails();
            }
        }

        /// <summary>
        /// method to show the Receipt Statement Header details Panel
        /// </summary>
        /// <param name="show">show</param>
        private void ShowPanel(bool show)
        {
            this.StatementNumberPanel.Enabled = show;
            this.RollYearpanel.Enabled = show;
            this.ParcelNumberpanel.Enabled = show;
            this.DateCreatedpanel.Enabled = show;
            this.PostTypepanel.Enabled = show;
            this.TaxAmountpanel.Enabled = show;
            this.OutstandingFeespanel.Enabled = show;
            this.PaidFeespanel.Enabled = show;
            this.TotalAmountpanel.Enabled = show;
        }

        /// <summary>
        /// Clears the details.
        /// </summary>
        private void ClearDetails()
        {
            this.StatementLinkLabel.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            ////Modified by Biju on 18/May/2010 to implement #6886
            this.ParcelNumberTextbox.Text = string.Empty;
            this.DateCreatedTextBox.Text = string.Empty;
            this.PostTypeTextBox.Text = string.Empty;
            this.TaxAmountTextBox.Text = string.Empty;
            this.OutstandingFeesTextBox.Text = string.Empty;
            this.PaidFeesTextBox.Text = string.Empty;
            this.TotalAmountTextBox.Text = string.Empty;
        }
        #endregion Private methods
    }
}
