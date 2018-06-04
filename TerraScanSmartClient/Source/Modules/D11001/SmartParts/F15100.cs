//--------------------------------------------------------------------------------------------
// <copyright file="F15100.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  20 Dec 06       KUPPUSAMY.B         Created
//  28 Apr 09       SHAIK KHAJA         Modified to fix Bug#4941
//  29 Apr 13       Purushotham.A       Enabling and disabling Reciept num field
//*********************************************************************************/

namespace D11001
{
    #region NameSpace

    using System;
    using System.Data;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using System.Drawing;

    #endregion NameSpace

    /// <summary>
    /// F15100 class
    /// </summary>
    public partial class F15100 : BaseSmartPart
    {
        #region PrivateMembers

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

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
        private F15100Controller form15100control;

        /// <summary>
        /// Instance for F15100
        /// </summary>
        F15110 f15110;

        /// <summary>
        /// Instance for F15100Controller
        /// </summary>
        //public F15110 form15110{
        //    get { return f15110; }
        //    set { f15110 = value; }
        //}

        /// <summary>
        /// ReceiptHeaderData class
        /// </summary>
        private F15100ReceiptHeaderData form15100RecieptHeaderData;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// used to store DateFormat
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");

        #endregion PrivateMembers

        #region Constructor

        /// <summary>
        /// Initializes component
        /// </summary>
        public F15100()
        {
            this.InitializeComponent();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
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
        public F15100(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.ReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptPictureBox.Height, this.ReceiptPictureBox.Width, tabText, red, green, blue);
            this.form15100RecieptHeaderData = new F15100ReceiptHeaderData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
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
        public F15100(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.ReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptPictureBox.Height, this.ReceiptPictureBox.Width, tabText, red, green, blue);
            this.form15100RecieptHeaderData = new F15100ReceiptHeaderData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

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
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;



        /// <summary>
        /// FormSlice_ViewEnabled
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SetViewMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_SetViewMode;

        

        #endregion Event Publication

        #region Property
        ///<summary>
        ///Gets or sets the F15100control
        ///</summary>
        [CreateNew]
        public F15100Controller Form15100Control
        {
            get { return this.form15100control as F15100Controller; }
            set { this.form15100control = value; }
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

                    ////this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    if (this.form15100RecieptHeaderData.GetReceiptHeader.Rows.Count > 0)
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
                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.GetReceiptHeaderDetails();
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
            this.ShowPanel(true);
            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
            this.GetReceiptHeaderDetails();
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            ////khaja removed Administrator verification to fix Bug#6519
            ////if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && TerraScanCommon.Administrator && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.PermissionFiled.editPermission)
            {
                this.SaveReceiptHeaderReceiptNumber();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            else
            {
                this.ShowPanel(true);
                this.ControlLock(false);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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

        #region Event Handlers

        /// <summary>
        /// F15100 form load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F15100_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.ShowPanel(true);
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.GetReceiptHeaderDetails();
                this.ReceiptNumberTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReceiptPictureBox control
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">EventArgs</param>
        private void ReceiptPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D11001.F15100"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ReceiptPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>       
        private void ReceiptPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptHeaderToolTip.SetToolTip(this.ReceiptPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ReceiptNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                    this.EditModeDisabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            try
            {
                ////khaja removed Administrator verification to fix Bug#6519
                ////if (this.pageMode == TerraScanCommon.PageModeTypes.View && TerraScanCommon.Administrator && this.PermissionFiled.editPermission)
                if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                }
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
            catch(Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

    
        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            ////To Verify whether the selected value is valid Value member
            if (string.IsNullOrEmpty(this.ReceiptNumberTextBox.Text.Trim()))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Timers the image_ click.
        /// </summary>
        /// <param name="textControl">The text control.</param>
        /// <param name="timePickerControl">The time picker control.</param>
        private void TimerImage_Click(TextBox textControl, DateTimePicker timePickerControl)
        {
            this.ParentForm.AcceptButton = null;
            try
            {
                timePickerControl.BringToFront();
                if (!string.IsNullOrEmpty(textControl.Text.Trim()))
                {
                    timePickerControl.Value = Convert.ToDateTime(textControl.Text);
                }
                else
                {
                    timePickerControl.Value = DateTime.Today;
                }

                SendKeys.Send(SharedFunctions.GetResourceString("F4"));
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Event Handlers

        #region PrivateMethods

        /// <summary>
        /// To allow the ReceiptNumberTextBox editable when the user is Administrator
        /// </summary>
        private void AllowReceiptNumberEditable()
        {
            //if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString()))
            //{
            //    if (this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString().Contains("Yes"))
            //    {
            //        this.ReceiptNumberTextBox.Enabled = false;
            //    }
            //}
            ////MOdified by purushotham to implement TFS#19403 CO
            if (TerraScanCommon.Administrator && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString()))
                {
                    if (this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString().Contains("Yes"))
                    {
                        this.ReceiptNumberTextBox.Enabled = false;
                    }
                    else
                    {
                        this.ReceiptNumberTextBox.LockKeyPress = false;
                        this.ReceiptNumberTextBox.Enabled = true;
                    }
                }
                else
                {
                    this.ReceiptNumberTextBox.LockKeyPress = false;
                    this.ReceiptNumberTextBox.Enabled = true;
                }
                 // this.ReceiptNumberTextBox.LockKeyPress = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString()))
                {
                    if (this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString().Contains("Yes"))
                    {
                        this.ReceiptNumberTextBox.Enabled = false;
                    }
                    else
                    {
                        this.ReceiptNumberTextBox.LockKeyPress = false;
                        this.ReceiptNumberTextBox.Enabled = true;
                    }
                }
                else
                {
                    this.ReceiptNumberTextBox.LockKeyPress = false;
                    this.ReceiptNumberTextBox.Enabled = true;
                }
               // this.ReceiptNumberTextBox.LockKeyPress = true;
            }
        }

        /// <summary>
        /// method to get the Receipt Header details
        /// </summary>
        private void GetReceiptHeaderDetails()
        {
            this.flagLoadOnProcess = true;

            this.form15100RecieptHeaderData = this.form15100control.WorkItem.GetReceiptHeaderDetails(this.keyId);

            if (this.form15100RecieptHeaderData.GetReceiptHeader.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.ReceiptNumberColumn].ToString()))
                {
                    this.ReceiptNumberTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.ReceiptNumberColumn].ToString();
                }
                else
                {
                    this.ReceiptNumberTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.ReceiptDateColumn].ToString()))
                {
                    this.ReceiptDateTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.ReceiptDateColumn].ToString();
                }
                else
                {
                    this.ReceiptDateTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.InterestDateColumn].ToString()))
                {
                    this.InterestDateTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.InterestDateColumn].ToString();
                }
                else
                {
                    this.InterestAmountTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.UserNameColumn].ToString()))
                {
                    this.UserTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.UserNameColumn].ToString();
                }
                else
                {
                    this.UserTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString()))
                {
                    this.PostedToGLTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostedToGLColumn].ToString();
                }
                else
                {
                    this.PostedToGLTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostNameColumn].ToString()))
                {
                    this.PostTypeTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.PostNameColumn].ToString();
                }
                else
                {
                    this.PostTypeTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.StatementNumberColumn].ToString()))
                {
                    this.StatementNumberTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.StatementNumberColumn].ToString();
                }
                else
                {
                    this.StatementNumberTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.ReceiptSourceColumn].ToString()))
                {
                    this.SourceTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.ReceiptSourceColumn].ToString();
                }
                else
                {
                    this.SourceTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.RollYearColumn].ToString()))
                {
                    this.RollYearTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.RollYearColumn].ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.TotalAmountColumn].ToString()))
                {
                    this.TotalAmountTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.TotalAmountColumn].ToString();
                }
                else
                {
                    this.TotalAmountTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.TaxAmountColumn].ToString()))
                {
                    this.TaxAmountTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.TaxAmountColumn].ToString();
                }
                else
                {
                    this.TaxAmountTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.FeeAmountColumn].ToString()))
                {
                    this.FeeAmountTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.FeeAmountColumn].ToString();
                }
                else
                {
                    this.FeeAmountTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.InterestAmountColumn].ToString()))
                {
                    this.InterestAmountTextBox.Text = this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.InterestAmountColumn].ToString();
                }
                else
                {
                    this.InterestAmountTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.IsEditableDateColumn].ToString()) && Convert.ToInt32(this.form15100RecieptHeaderData.GetReceiptHeader.Rows[0][this.form15100RecieptHeaderData.GetReceiptHeader.IsEditableDateColumn].ToString()) > 0)
                {
                    this.ReceiptDatePanel.Enabled = true;
                    this.InterestDatepanel.Enabled = true;
                    this.GeneralPaymentDatePict.Enabled = true;
                    this.GeneralFormDatePic.Enabled = true;
                }
                else
                {
                    this.ReceiptDatePanel.Enabled = false;
                    this.InterestDatepanel.Enabled = false;
                    this.GeneralPaymentDatePict.Enabled = false;
                    this.GeneralFormDatePic.Enabled = false;
                }
            }
            else
            {
                ////Coding added for the issue 4497  by Malliga on 29/3/3009
                this.ReceiptNumberTextBox.Text = string.Empty;
                this.ReceiptDateTextBox.Text = string.Empty;
                this.InterestDateTextBox.Text = string.Empty;
                this.UserTextBox.Text = string.Empty;
                this.PostedToGLTextBox.Text = string.Empty;
                this.PostTypeTextBox.Text = string.Empty;
                this.StatementNumberTextBox.Text = string.Empty;
                this.SourceTextBox.Text = string.Empty;
                this.RollYearTextBox.Text = string.Empty;
                this.TotalAmountTextBox.Text = string.Empty;
                this.TaxAmountTextBox.Text = string.Empty;
                this.FeeAmountTextBox.Text = string.Empty;
                this.InterestAmountTextBox.Text = string.Empty;
                ////End here for 4497
                this.ShowPanel(false);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;

            this.AllowReceiptNumberEditable();
        }

        /// <summary>
        /// method to enable or disable the Receipt Header details Panel
        /// </summary>
        /// <param name="show">show</param>
        private void ShowPanel(bool show)
        {
            this.ReceiptNumberPanel.Enabled = show;
            this.ReceiptDatePanel.Enabled = show;
            this.InterestDatepanel.Enabled = show;
            this.Userpanel.Enabled = show;
            this.PostedToGLpanel.Enabled = show;
            this.PostTypepanel.Enabled = show;
            this.StatementNumberpanel.Enabled = show;
            this.Sourcepanel.Enabled = show;
            this.RollYearpanel.Enabled = show;
            this.TotalAmountpanel.Enabled = show;
            this.TaxAmountpanel.Enabled = show;
            this.FeeAmountpanel.Enabled = show;
            this.InterestAmountpanel.Enabled = show;
        }

        /// <summary>
        /// To Save the Receipt Header Receipt Number
        /// </summary>
        private void SaveReceiptHeaderReceiptNumber()
        {
            DataTable receiptData = new DataTable();
            receiptData.Columns.AddRange(new DataColumn[] { new DataColumn("ReceiptNumber"), new DataColumn("ReceiptDate"), new DataColumn("InterestDate") });
            DataRow dr = receiptData.NewRow();
            dr["ReceiptNumber"] = this.ReceiptNumberTextBox.Text.Trim();
            dr["ReceiptDate"] = this.ReceiptDateTextBox.Text;
            dr["InterestDate"] = this.InterestDateTextBox.Text;
            receiptData.Rows.Add(dr);

            string receiptValue = TerraScanCommon.GetXmlString(receiptData);

            try
            {
                this.form15100control.WorkItem.F15100_SaveReceiptHeaderreceiptNumber(this.keyId, receiptValue, TerraScanCommon.UserId);
            }
            catch (Exception ex)
            {
            }

            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.keyId;
            ////to reload the form with the current keyid
            ////to refresh the master form with the return keyid
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
        }

        #region Events
        /// <summary>
        /// Handles the Click event of the GeneralPaymentDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GeneralPaymentDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.ReceiptDateTextBox, this.ReceiptDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the GeneralFormDatePic control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GeneralFormDatePic_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.InterestDateTextBox, this.InterestdateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the ReceiptDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptDateTextBox.Text = this.ReceiptDateTimePicker.Text;
                this.ReceiptDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the ReceiptDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send(SharedFunctions.GetResourceString("ESC"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ReceiptDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                    this.EditModeDisabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the InterestDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                    this.EditModeDisabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the InterestdateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InterestdateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.InterestDateTextBox.Text = this.InterestdateTimePicker.Text;
                this.InterestDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the InterestdateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void InterestdateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send(SharedFunctions.GetResourceString("ESC"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the general form date seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetGeneralFormDateSeletedDate(string dateSelected)
        {
            this.FormDateCalender.Tag = string.Empty;
            this.GeneralFormDatePic.Focus();
            this.ReceiptDateTextBox.Text = dateSelected;
            this.FormDateCalender.Visible = false;
        }

        /// <summary>
        /// Handles the KeyDown event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetGeneralFormDateSeletedDate(this.FormDateCalender.SelectionStart.ToString(this.dateFormat));
                    this.FormDateCalender.Visible = false;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Leave event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                this.FormDateCalender.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetGeneralFormDateSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the general payment seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetGeneralPaymentSeletedDate(string dateSelected)
        {
            this.PaymentDateCalender.Tag = string.Empty;
            this.GeneralPaymentDatePict.Focus();
            this.InterestDateTextBox.Text = dateSelected;
            this.PaymentDateCalender.Visible = false;
        }

        /// <summary>
        /// Handles the DateSelected event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetGeneralPaymentSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetGeneralFormDateSeletedDate(this.FormDateCalender.SelectionStart.ToString(this.dateFormat));
                    this.FormDateCalender.Visible = false;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Leave event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                this.FormDateCalender.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLook">if set to <c>true</c> [control look].</param>
        private void ControlLock(bool controlLook)
        {
            ////khaja made changes to fix Bug#4941(2)
            ////this.ReceiptDateTextBox.LockKeyPress = !controlLook;
            this.ReceiptDatePanel.Enabled = !controlLook;
            this.GeneralPaymentDatePict.Enabled = !controlLook;
            this.InterestDatepanel.Enabled = !controlLook;
            ////this.InterestDateTextBox.LockKeyPress = !controlLook;
            this.GeneralFormDatePic.Enabled = !controlLook;
        }
        #endregion Events
        #endregion PrivateMethods
    }
}
