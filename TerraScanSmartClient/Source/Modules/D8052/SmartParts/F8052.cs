//--------------------------------------------------------------------------------------------
// <copyright file="F8052.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8052.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8052
{
    using System;
    using System.Collections.Generic;
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
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F8052 class file
    /// </summary>
    [SmartPart]
    public partial class F8052 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// eventPropertiesDataSet variable is used to get the details of event properties.
        /// </summary>
        private LinearEventData linearEventTypeDataSet = new LinearEventData();

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F8052Controller form8052Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// PermissionFields struct 
        /// </summary>
        private PermissionFields permissions;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8052"/> class.
        /// </summary>
        public F8052()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8052"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8052(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.keyId = keyID;
            this.masterFormNo = masterform;
            this.formMasterPermissionEdit = permissionEdit;
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8052 control.
        /// </summary>
        /// <value>The F8052 control.</value>
        [CreateNew]
        public F8052Controller Form8052Control
        {
            get { return this.form8052Control as F8052Controller; }
            set { this.form8052Control = value; }
        }
        #endregion

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            this.ClearLinearEventProperties();
            this.Line2Visibility(false);
            this.LinearEventPanel.Enabled = false;
            ////this.LockControls(true);
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
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
                    ////SliceValidationFields sliceValidationFields;
                    ////sliceValidationFields = CheckErrors(eventArgs.Data);
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.LinearEventPanel.Enabled = true;
            this.GetLinearEventProperties(this.keyId);
            ////this.Line2Visibility(true);
            this.Line2Location();

            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.SaveLinearEventProperties();
                }
            }
            else
            {
                this.LockControls(false);
                this.LinearEventPanel.Enabled = true;
                this.GetLinearEventProperties(this.keyId);
                this.Line2Visibility(true);
                this.Line2Location();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            ////else if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            ////{

            ////}

            this.pageMode = TerraScanCommon.PageModeTypes.View;
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

                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);


                    if (this.linearEventTypeDataSet.GetFSLinearEventType.Rows.Count > 0)
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
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.GetLinearEventProperties(this.keyId);
                this.Line2Location();
            }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F8052 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8052_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GetLinearEventProperties(this.keyId);
                this.Line2Location();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the linear event properties.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        private void GetLinearEventProperties(int eventId)
        {
            this.Cursor = Cursors.WaitCursor;
            decimal startValue;
            decimal endValue;
            decimal result;
            try
            {
                this.linearEventTypeDataSet = this.form8052Control.WorkItem.GetLinearEventType(eventId);
                if (this.linearEventTypeDataSet.GetFSLinearEventType.Rows.Count > 0)
                {
                    this.StartTextBox.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.StartColumn].ToString();
                    this.StartLabelTextBox.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.StartLabelColumn].ToString();
                    this.EndTextBox.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.EndColumn].ToString();
                    this.EndLabelTextBox.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.EndLabelColumn].ToString();
                    this.DistTextBox.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.DistColumn].ToString();
                    this.CommentTextBox.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.CommentColumn].ToString();
                    if (string.IsNullOrEmpty(this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.LengthColumn].ToString()))
                    {
                        this.Line1RightEndLabel.Text = "0";
                    }
                    else
                    {
                        this.Line1RightEndLabel.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.LengthColumn].ToString();

                        decimal.TryParse(this.StartTextBox.Text, out startValue);
                        decimal.TryParse(this.EndTextBox.Text, out endValue);
                        //startValue = this.StartTextBox.Text;
                        //endValue = this.EndTextBox.Text;
                        result = endValue - startValue;

                        if (result == 1)
                        {
                            this.Line1RightEndLabel.Location = new System.Drawing.Point(740, 74);
                        }
                        else
                        {
                            this.Line1RightEndLabel.Location = new System.Drawing.Point(736, 74);
                        }
                        //this.InputFileDetailPictureBox.Location = new System.Drawing.Point(370, 0);
                    }

                    this.Line1LeftEndLabel.Text = "0";
                    this.Line2LeftEndLabel.Text = this.StartTextBox.Text;
                    this.Line2RightEndLabel.Text = this.EndTextBox.Text;
                    this.SealCoatLabel.Text = this.linearEventTypeDataSet.GetFSLinearEventType.Rows[0][this.linearEventTypeDataSet.GetFSLinearEventType.EventTypeColumn].ToString();
                }
                else
                {
                    this.LinearEventPanel.Enabled = false;
                    this.Line2Visibility(false);
                }

                // Issue Fix - Line2 Visible when Start = End = 0

                // -- Starts Here Minor Change
                decimal startPoint;
                decimal endPoint;

                Decimal.TryParse(this.StartTextBox.Text, out startPoint);
                startPoint = decimal.Floor(startPoint);

                Decimal.TryParse(this.EndTextBox.Text, out endPoint);
                endPoint = decimal.Floor(endPoint);

                if (string.IsNullOrEmpty(this.StartTextBox.Text) || string.IsNullOrEmpty(this.EndTextBox.Text) || string.IsNullOrEmpty(this.Line1RightEndLabel.Text))
                {
                    this.Line2Visibility(false);
                }
                else if ((startPoint = endPoint) == 0)
                {
                    this.Line2Visibility(false);
                }
                else if (Convert.ToDecimal(this.StartTextBox.Text) == Convert.ToDecimal(this.EndTextBox.Text))
                {
                    ////this.Line2LeftEndLabel.Visible = true;
                    ////this.Line2RightEndLabel.Visible = true;
                    ////this.SealCoatLabel.Visible = true;
                    ////this.Line2Panel.Visible = true;
                    ////this.Line2EndLabel.Visible = true;
                    ////this.Line2StartLabel.Visible = true;
                    ////this.pictureBox3.Visible = true;
                    ////this.pictureBox1.Visible = false;
                    this.Line2Visibility(true);
                    this.pictureBox1.Visible = false;
                }
                else
                {
                    this.Line2Visibility(true);
                }

                //-- Ends Here
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
        /// Clears the Linear event properties.
        /// </summary>
        private void Line2Location()
        {
            int a = Convert.ToInt32(this.Line1Panel.Width);
            Decimal line1RightEnd = 0;

            // Sets the width of the Line2
            if (!Decimal.TryParse(this.Line1RightEndLabel.Text, out line1RightEnd) || line1RightEnd == 0)
            {
                return;
            }

            Decimal b = a / line1RightEnd;

            this.Line2Panel.Width = Convert.ToInt32(((Convert.ToDecimal(this.EndTextBox.Text) - Convert.ToDecimal(this.StartTextBox.Text)) * b));
            this.Line2Panel.Height = 6;

            // Sets the location of the Line2
            Decimal c = (a * Convert.ToDecimal(this.StartTextBox.Text)) / line1RightEnd;
            this.Line2Panel.Left = this.Line1Panel.Left + Convert.ToInt32(c);
            this.Line2Panel.Top = this.Line1Panel.Top - 1;

            this.Line2LeftEndLabel.Top = this.Line2Panel.Top - this.Line2Panel.Height - 13;
            this.Line2RightEndLabel.Top = this.Line2Panel.Top + this.Line2Panel.Height + 3;

            if (Convert.ToDecimal(this.StartTextBox.Text) < 25)
            {
                this.Line2LeftEndLabel.TextAlign = ContentAlignment.MiddleLeft;
                this.Line2LeftEndLabel.Left = this.Line2Panel.Left - 2;

                this.Line2StartLabel.TextAlign = ContentAlignment.MiddleLeft;
                this.Line2StartLabel.Left = this.Line2LeftEndLabel.Left;
            }
            else
            {
                this.Line2LeftEndLabel.TextAlign = ContentAlignment.MiddleCenter;
                this.Line2LeftEndLabel.Left = this.Line2Panel.Left - 25;

                this.Line2StartLabel.TextAlign = ContentAlignment.MiddleCenter;
                this.Line2StartLabel.Left = this.Line2LeftEndLabel.Left - 37;
            }

            if (Convert.ToDecimal(this.EndTextBox.Text) >= (Convert.ToDecimal(this.Line1RightEndLabel.Text) - 15))
            {
                this.Line2RightEndLabel.TextAlign = ContentAlignment.MiddleRight;
                this.Line2RightEndLabel.Left = this.Line2Panel.Left + this.Line2Panel.Width - this.Line2RightEndLabel.Width;

                this.Line2EndLabel.TextAlign = ContentAlignment.MiddleRight;
                this.Line2EndLabel.Left = this.Line2RightEndLabel.Left - this.Line2RightEndLabel.Width - 12;
            }
            else
            {
                this.Line2RightEndLabel.TextAlign = ContentAlignment.MiddleCenter;
                this.Line2RightEndLabel.Left = this.Line2Panel.Left + this.Line2Panel.Width - 37;

                this.Line2EndLabel.TextAlign = ContentAlignment.MiddleCenter;
                this.Line2EndLabel.Left = this.Line2RightEndLabel.Left - 37;
            }

            if (Convert.ToDecimal(this.StartTextBox.Text) >= 460)
            {
                this.Line2LeftEndLabel.TextAlign = ContentAlignment.MiddleRight;
                this.Line2LeftEndLabel.Left = this.Line2Panel.Left - this.Line2LeftEndLabel.Width + this.pictureBox1.Width;

                this.Line2StartLabel.TextAlign = ContentAlignment.MiddleRight;
                this.Line2StartLabel.Left = this.Line2LeftEndLabel.Left - this.Line2LeftEndLabel.Width - 12;
            }

            if (Convert.ToDecimal(this.EndTextBox.Text) < 30)
            {
                this.Line2RightEndLabel.TextAlign = ContentAlignment.MiddleLeft;
                this.Line2RightEndLabel.Left = this.Line2Panel.Left + this.Line2Panel.Width - 13;

                this.Line2EndLabel.TextAlign = ContentAlignment.MiddleLeft;
                this.Line2EndLabel.Left = this.Line2RightEndLabel.Left;
            }

            if (Convert.ToDecimal(this.StartTextBox.Text) < 30 && Convert.ToDecimal(this.DistTextBox.Text) < 70)
            {
                this.SealCoatLabel.Left = this.Line2Panel.Left;
            }
            else if (Convert.ToDecimal(this.EndTextBox.Text) >= (Convert.ToDecimal(this.Line1RightEndLabel.Text) - 70) && Convert.ToDecimal(this.StartTextBox.Text) >= (Convert.ToDecimal(this.Line1RightEndLabel.Text) - 100))
            {
                this.SealCoatLabel.Left = this.Line2Panel.Left + this.Line2Panel.Width - this.SealCoatLabel.Width;
            }
            else
            {
                //this.SealCoatLabel.Left = this.Line2Panel.Left + (Convert.ToInt32(this.Line2Panel.Width) / 2) - this.SealCoatLabel.Width / 2;
                this.SealCoatLabel.Left = this.Line2Panel.Left + ((this.Line2Panel.Width - this.SealCoatLabel.Width) / 2);
            }

            this.Line2EndLabel.Text = this.EndLabelTextBox.Text;
            this.Line2StartLabel.Text = this.StartLabelTextBox.Text;
            this.pictureBox1.Left = this.Line2Panel.Left;
            this.pictureBox1.Top = this.Line2Panel.Top - 4;
            this.pictureBox3.Left = this.Line2Panel.Left + this.Line2Panel.Width - this.pictureBox3.Width;
            this.pictureBox3.Top = this.Line2Panel.Top - 4;

            // Overlap Issue Fix 
            //-- starts here
            if (this.pictureBox3.Left <= this.panel2.Left)
            {
                this.Line2Visibility(true);
                this.pictureBox3.Visible = false;
            }

            if (this.Line2EndLabel.Location.X < 0 || this.Line2RightEndLabel.Location.X < 0 || this.Line2LeftEndLabel.Location.X < 0 || this.Line2StartLabel.Location.X < 0)
            {
                this.SealCoatLabel.Left = this.StartLabel.Left;
                //this.SealCoatLabel.TextAlign = ContentAlignment.MiddleCenter;
                this.SealCoatLabel.TextAlign = ContentAlignment.MiddleLeft;

                this.Line2RightEndLabel.Left = SealCoatLabel.Left;
                this.Line2RightEndLabel.TextAlign = ContentAlignment.MiddleLeft;

                this.Line2LeftEndLabel.Left = Line2RightEndLabel.Left;
                this.Line2LeftEndLabel.TextAlign = ContentAlignment.MiddleLeft;

                this.Line2EndLabel.Left = this.Line2RightEndLabel.Left;
                this.Line2EndLabel.TextAlign = ContentAlignment.MiddleLeft;

                this.Line2StartLabel.Left = this.Line2RightEndLabel.Left;
                this.Line2StartLabel.TextAlign = ContentAlignment.MiddleLeft;
            }
            else if (!this.pictureBox3.Visible)
            {
                this.SealCoatLabel.Left = this.StartLabel.Left;
                //this.SealCoatLabel.TextAlign = ContentAlignment.MiddleCenter;
                this.SealCoatLabel.TextAlign = ContentAlignment.MiddleLeft;

                this.Line2RightEndLabel.Left = this.pictureBox1.Left;
                this.Line2RightEndLabel.TextAlign = ContentAlignment.MiddleLeft;

                this.Line2EndLabel.Left = this.Line2RightEndLabel.Left;
                this.Line2EndLabel.TextAlign = ContentAlignment.MiddleLeft;
            }
            ////else if (this.Line2LeftEndLabel.Right > this.panel3.Right || this.Line2StartLabel.Right > this.panel3.Right)
            ////{
            ////    this.Line2LeftEndLabel.Left = this.Line2RightEndLabel.Left;
            ////    this.Line2LeftEndLabel.TextAlign = ContentAlignment.MiddleRight;

            ////    //this.Line2StartLabel.Left = this.StatementPictureBox.Left - this.Line2StartLabel.Width - 10;
            ////    this.Line2StartLabel.Left = this.Line2EndLabel.Left;
            ////    this.Line2StartLabel.TextAlign = ContentAlignment.MiddleLeft;
            ////}

            else if (this.Line2RightEndLabel.Right > this.panel3.Right || this.Line2EndLabel.Right > this.panel3.Right)
            {
                this.Line2RightEndLabel.Left = this.pictureBox3.Right - this.Line2RightEndLabel.Width;
                this.Line2EndLabel.Left = this.pictureBox3.Right - this.Line2EndLabel.Width;
            }

            else
            {
                //decimal temp = Convert.ToDecimal(this.DistTextBox.Text) / Convert.ToDecimal(2);
                //this.SealCoatLabel.Left = Line2Panel.Width / 2;
                this.SealCoatLabel.Left = this.Line2Panel.Left + ((this.Line2Panel.Width - this.SealCoatLabel.Width) / 2);
                //this.SealCoatLabel.TextAlign = ContentAlignment.MiddleCenter;
                this.SealCoatLabel.TextAlign = ContentAlignment.MiddleLeft;
                if (this.SealCoatLabel.Location.X < 0)
                {
                    this.SealCoatLabel.Left = this.pictureBox3.Left;
                }
            }

            if (this.Line2LeftEndLabel.Right > this.panel3.Right || this.Line2StartLabel.Right > this.panel3.Right)
            {
                if (!this.pictureBox3.Visible)
                {
                    this.Line2LeftEndLabel.Left = this.pictureBox1.Right - this.Line2LeftEndLabel.Width;
                    this.Line2StartLabel.Left = this.pictureBox1.Right - this.Line2StartLabel.Width;
                }
                else
                {
                    this.Line2LeftEndLabel.Left = this.pictureBox3.Right - this.Line2LeftEndLabel.Width;
                    this.Line2StartLabel.Left = this.pictureBox3.Right - this.Line2StartLabel.Width;
                }
            }

            //decimal temp = Convert.ToDecimal(this.DistTextBox.Text) / Convert.ToDecimal(2);
            //this.SealCoatLabel.Left = Line2Panel.Width / 2;
            this.SealCoatLabel.Left = this.Line2Panel.Left + ((this.Line2Panel.Width - this.SealCoatLabel.Width) / 2);
            //this.SealCoatLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.SealCoatLabel.TextAlign = ContentAlignment.MiddleLeft;
            if (this.SealCoatLabel.Location.X < 0)
            {
                this.SealCoatLabel.Left = this.pictureBox3.Left;
            }

            //-- Ends Here
        }

        /// <summary>
        /// Clears the Linear event properties.
        /// </summary>
        private void ClearLinearEventProperties()
        {
            this.StartTextBox.Text = string.Empty;
            this.StartLabelTextBox.Text = string.Empty;
            this.EndTextBox.Text = string.Empty;
            this.EndLabelTextBox.Text = string.Empty;
            this.DistTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;

            this.Line1RightEndLabel.Text = string.Empty;
            this.Line1LeftEndLabel.Text = string.Empty;
            this.Line2LeftEndLabel.Text = string.Empty;
            this.Line2RightEndLabel.Text = string.Empty;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private void Line2Visibility(bool visible)
        {
            this.Line2LeftEndLabel.Visible = visible;
            this.Line2RightEndLabel.Visible = visible;
            this.SealCoatLabel.Visible = visible;
            this.Line2Panel.Visible = visible;
            this.Line2EndLabel.Visible = visible;
            this.Line2StartLabel.Visible = visible;
            this.pictureBox1.Visible = visible;
            this.pictureBox3.Visible = visible;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockControls(bool lockControl)
        {
            this.StartTextBox.LockKeyPress = lockControl;
            this.StartLabelTextBox.LockKeyPress = lockControl;
            this.EndTextBox.LockKeyPress = lockControl;
            this.EndLabelTextBox.LockKeyPress = lockControl;
            this.CommentTextBox.LockKeyPress = lockControl;
        }

        /// <summary>
        /// Saves the Linear event properties.
        /// </summary>
        /// <returns>save the LinearEvent Properties</returns>
        private bool SaveLinearEventProperties()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                LinearEventData linearEventData = new LinearEventData();
                LinearEventData.SaveFSLinearEventTypeRow dr = linearEventData.SaveFSLinearEventType.NewSaveFSLinearEventTypeRow();

                dr.EventID = this.keyId;
                dr.Start = Convert.ToDecimal(this.StartTextBox.Text);
                dr.StartLabel = this.StartLabelTextBox.Text;
                dr.End = Convert.ToDecimal(this.EndTextBox.Text);
                dr.EndLabel = this.EndLabelTextBox.Text;
                dr.Comment = this.CommentTextBox.Text;

                linearEventData.SaveFSLinearEventType.Rows.Add(dr);
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(linearEventData.SaveFSLinearEventType.Copy());
                tempDataSet.Tables[0].TableName = "Table";

                this.form8052Control.WorkItem.SaveLinearEventType(tempDataSet.GetXml(), TerraScanCommon.UserId);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

            if (string.IsNullOrEmpty(StartTextBox.Text.Trim()))
            {
                this.StartTextBox.Focus();
                ////sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }

            if (string.IsNullOrEmpty(EndTextBox.Text.Trim()))
            {
                this.EndTextBox.Focus();
                ////sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }

            if (!string.IsNullOrEmpty(StartTextBox.Text.Trim()) && !string.IsNullOrEmpty(EndTextBox.Text.Trim()))
            {
                if (Convert.ToSingle(StartTextBox.Text.Trim()) > Convert.ToSingle(EndTextBox.Text.Trim()))
                {
                    sliceValidationFields.ErrorMessage = "Start distance should not be greater than end distance";
                }
                else if (Convert.ToSingle(StartTextBox.Text.Trim()) > Convert.ToSingle(this.Line1RightEndLabel.Text) || Convert.ToSingle(this.EndTextBox.Text.Trim()) > Convert.ToSingle(this.Line1RightEndLabel.Text))
                {
                    sliceValidationFields.ErrorMessage = "Start or End distance should not be greater than length";
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Edits the enabled.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the StatementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatementPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D8052.F8052"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the StartTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void StartTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.EditEnabled();
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the StatementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatementPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.LinearEventToolTip.SetToolTip(this.StatementPictureBox, "D8052.F8052");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the StartTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StartTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void Line1RightEndLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.LinearEventToolTip.SetToolTip(this.Line1RightEndLabel, this.Line1RightEndLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
