//--------------------------------------------------------------------------------------------
// <copyright file="F8054.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8054.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06        VINOTH              Created
//*********************************************************************************/
namespace D8054
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.BusinessEntities;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Utilities;
    using System.Configuration;

    /// <summary>
    /// F8052 class file
    /// </summary>
    [SmartPart]
    public partial class F8054 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// Stores Section Indicator's text
        /// </summary>
        private string tabText;

        /// <summary>
        /// Stores the KeyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// Stores masterFormNo.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Stores formMasterPermissionEdit Permission.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F8054Controller form8054Control;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Stores the PageMode.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// eventPropertiesDataSet variable is used to get the details of event properties.
        /// </summary>
        private PointEventData pointEventTypeDataSet = new PointEventData();

        /// <summary>
        /// To store the status of TextChanged
        /// </summary>
        private bool textChanged;

        /// <summary>
        /// Stores End Label
        /// </summary>
        private string endLabel;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8052"/> class.
        /// </summary>
        public F8054()
        {
            this.InitializeComponent();
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
        public F8054(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.keyId = keyID;
            this.masterFormNo = masterform;
            this.formMasterPermissionEdit = permissionEdit;
            this.tabText = tabText;
            ////this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(188, 44, tabText, red, green, blue);
            this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, this.DistrictInfoSecIndicatorPictureBox.Width, this.tabText, red, green, blue);
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
        /// Gets or sets the F8054 control.
        /// </summary>
        /// <value>The F8054 control.</value>
        [CreateNew]
        public F8054Controller Form8054Control
        {
            get { return this.form8054Control as F8054Controller; }
            set { this.form8054Control = value; }
        }
        #endregion

        #region EventSubscription

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
                }

                if (this.pointEventTypeDataSet.GetFSLinearEventType.Rows.Count > 0)
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearPointEventProperties();
                    this.PointVisibility(false);
                    this.PointEventPanel.Enabled = false;
                    ////this.LockControls(true);
                }
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

            this.PointEventPanel.Enabled = true;
            this.GetPointEventProperties(this.keyId);
            this.PointLocation();

            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                    this.SavePointEventProperties();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.LockControls(false);
                this.PointEventPanel.Enabled = true;
                this.GetPointEventProperties(this.keyId);
                this.PointVisibility(true);
                this.PointLocation();
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.LoadPointEventProperties();
            }
        }

        #endregion

        #region FormLoad

        /// <summary>
        /// Handles the Load event of the F8052 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8054_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadPointEventProperties();
                ////this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region SectionIndicator

        /// <summary>
        /// DistrictInfoSecIndicatorPictureBox Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void DistrictInfoSecIndicatorPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D8054.F8054"));
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the StatementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox sectionIndicator = (PictureBox)sender;
            this.PointEventToolTip.SetToolTip(sectionIndicator, "D8054.F8054");
            //// MessageBox.Show("D8054.F8054");
        }

        #endregion

        #region Events

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
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the OffsetTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void OffsetTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

                    case (char)45:
                        {
                            if (this.OffsetTextBox.SelectionStart == 0)
                            {
                                e.Handled = false;
                                break;
                            }
                            else
                            {
                                e.Handled = true;
                                break;
                            }
                        }

                    case (char)43:
                        {
                            if (this.OffsetTextBox.SelectionStart == 0)
                            {
                                e.Handled = false;
                                break;
                            }
                            else
                            {
                                e.Handled = true;
                                break;
                            }
                        }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
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
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the OffsetTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void OffsetTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the OffsetTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void OffsetTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.OffsetTextBox.Text.Trim()) || this.OffsetTextBox.Text == "+" || this.OffsetTextBox.Text == "-")
                {
                    this.OffsetTextBox.Text = "0";
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the OffsetTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StartLabelTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textChanged == false)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the undoMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UndoMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.StartTextBox.Focused)
                {
                    this.StartTextBox.Undo();
                }
                else if (this.OffsetTextBox.Focused)
                {
                    this.OffsetTextBox.Undo();
                }
                else if (this.StartLabelTextBox.Focused)
                {
                    this.StartLabelTextBox.Undo();
                }
                else if (this.CommentTextBox.Focused)
                {
                    this.CommentTextBox.Undo();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the RightEndLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RightEndLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.PointEventToolTip.SetToolTip(this.RightEndLabel, this.RightEndLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the StartDistLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StartDistLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.PointEventToolTip.SetToolTip(this.StartDistLabel, this.StartDistLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the StartComLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StartComLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.PointEventToolTip.SetToolTip(this.StartComLabel, this.StartComLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// To Load the point event properties.
        /// </summary>
        private void LoadPointEventProperties()
        {
            this.textChanged = true;
            this.FlagSliceForm = true;
            this.GetPointEventProperties(this.keyId);
            this.PointLocation();
            this.textChanged = false;
        }

        /// <summary>
        /// Clears the Linear event properties.
        /// </summary>
        private void ClearPointEventProperties()
        {
            this.StartTextBox.Text = string.Empty;
            this.OffsetTextBox.Text = string.Empty;
            this.StartLabelTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;
            this.LeftEndLabel.Text = string.Empty;
            this.RightEndLabel.Text = string.Empty;
        }

        /// <summary>
        /// Points the visibility.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private void PointVisibility(bool visible)
        {
            this.PointPictureBox.Visible = visible;
            this.StartComLabel.Visible = visible;
            this.StartDistLabel.Visible = visible;
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
            float temp;
            if (string.IsNullOrEmpty(this.StartTextBox.Text.Trim()))
            {
                this.StartTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }

            if (!string.IsNullOrEmpty(this.StartTextBox.Text.Trim()))
            {
                temp = Convert.ToSingle(this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.EndColumn].ToString());
                if (Convert.ToSingle(this.StartTextBox.Text.Trim()) > temp)
                {
                    ////MessageBox.Show("From check Error");
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("8054DistanceValidation");
                    this.StartTextBox.Focus();
                }
            }

            if (Convert.ToDouble(this.OffsetTextBox.Text.Trim()) > 999999.99)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("8054OffsetValidation");
            }

            if (sliceValidationFields.RequiredFieldMissing)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
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

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockControls(bool lockControl)
        {
            this.StartTextBox.LockKeyPress = lockControl;
            this.StartLabelTextBox.LockKeyPress = lockControl;
            this.OffsetTextBox.Enabled = !lockControl;
            this.CommentTextBox.LockKeyPress = lockControl;
        }

        /// <summary>
        /// Points the location.
        /// </summary>
        private void PointLocation()
        {
            decimal line1length;
            if (this.pointEventTypeDataSet.GetFSLinearEventType.Rows.Count > 0)
            {
                line1length = Convert.ToDecimal(this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.LengthColumn].ToString());
                if (line1length > 0)
                {
                    int panelWidth = Convert.ToInt32(this.Line1Panel.Width);
                    Decimal line1RightEnd = 0;

                    if (!Decimal.TryParse(this.RightEndLabel.Text, out line1RightEnd) || line1RightEnd == 0)
                    {
                        return;
                    }

                    Decimal pointLocation = panelWidth / line1RightEnd;
                    Decimal location = Convert.ToDecimal(this.StartTextBox.Text.Trim()) * pointLocation;
                    Double offset;
                    if (!string.IsNullOrEmpty(this.OffsetTextBox.Text.Trim()))
                    {
                        offset = Convert.ToDouble(this.OffsetTextBox.Text.Trim());
                    }
                    else
                    {
                        offset = 0;
                    }

                    if (offset > 0)
                    {
                        this.PointPictureBox.Left = this.Line1Panel.Left + Convert.ToInt32(location);
                        this.PointPictureBox.Top = this.Line1Panel.Top + 14;
                        ////this.tempLocation = this.StartDistLabel.Location.X  + this.PointPictureBox.Right;
                        ////if (this.tempLocation < this.PointEventPanel.Width)
                        ////{
                        this.StartDistLabel.Text = this.StartTextBox.Text.Trim() + " / " + this.OffsetTextBox.Text.Trim();
                        this.StartDistLabel.Left = this.PointPictureBox.Right - (this.StartDistLabel.Width / 2);
                        this.StartDistLabel.Top = this.Line1Panel.Top + 30;

                        this.StartComLabel.Text = this.StartLabelTextBox.Text.Trim();
                        this.StartComLabel.Left = this.PointPictureBox.Right - (this.StartComLabel.Width / 2);
                        this.StartComLabel.Top = this.Line1Panel.Top + 45;
                        if (this.StartDistLabel.Location.X < panel2.Left || this.StartComLabel.Location.X < this.panel2.Left)
                        {
                            this.StartDistLabel.Left = (this.StartDistLabel.Width / 2) - this.panel2.Left;
                            ////this.StartDistLabel.Top = this.Line1Panel.Top + 21;

                            this.StartComLabel.Left = (this.StartDistLabel.Width / 2) - this.panel2.Left;
                            ////this.StartComLabel.Top = this.Line1Panel.Top + 36;
                        }
                        ////else if (this.StartComLabel.Right > (this.PointPictureBox.Right + (this.PointEventPanel.Width - this.PointPictureBox.Right)))
                        else if (this.StartComLabel.Right >= 773 || this.StartDistLabel.Right >= 792)
                        {
                            this.StartDistLabel.Left = this.PointPictureBox.Right - this.StartDistLabel.Width;
                            this.StartComLabel.Left = this.PointPictureBox.Right - this.StartComLabel.Width;
                        }
                        ////}
                        ////else
                        ////{
                        ////    this.StartDistLabel.Text = this.StartTextBox.Text.Trim() + " / " + this.OffsetTextBox.Text.Trim();
                        ////    this.StartDistLabel.Left = this.PointPictureBox.Right - this.StartDistLabel.Width;
                        ////    this.StartDistLabel.Top = this.Line1Panel.Top + 30;

                        ////    this.StartComLabel.Text = this.StartLabelTextBox.Text.Trim();
                        ////    this.StartComLabel.Left = this.PointPictureBox.Right - this.StartComLabel.Width;
                        ////    this.StartComLabel.Top = this.Line1Panel.Top + 45;
                        ////    if (this.StartDistLabel.Location.X < 0 || this.StartComLabel.Location.X < 0)
                        ////    {
                        ////        this.StartDistLabel.Left = (this.StartDistLabel.Width / 2) - this.PointPictureBox.Left;
                        ////        ////this.StartDistLabel.Top = this.Line1Panel.Top + 21;

                        ////        this.StartComLabel.Left = (this.StartDistLabel.Width / 2) - this.PointPictureBox.Left;
                        ////        ////this.StartComLabel.Top = this.Line1Panel.Top + 36;
                        ////    }
                        ////}

                        this.LeftEndLabel.Top = 47;
                        this.RightEndLabel.Top = 47;
                        this.RightEndLabel.Left = 664;
                    }
                    else if (offset < 0)
                    {
                        this.PointPictureBox.Left = this.Line1Panel.Left + Convert.ToInt32(location);
                        this.PointPictureBox.Top = this.Line1Panel.Top - 22;
                        ////this.tempLocation = this.StartDistLabel.Location.X + this.PointPictureBox.Right;
                        ////if (this.tempLocation < this.PointEventPanel.Width)
                        ////{
                        this.StartDistLabel.Text = this.StartTextBox.Text.Trim() + " / " + this.OffsetTextBox.Text.Trim();
                        this.StartDistLabel.Left = this.PointPictureBox.Right - (this.StartDistLabel.Width / 2);
                        this.StartDistLabel.Top = this.Line1Panel.Top - 38;

                        this.StartComLabel.Text = this.StartLabelTextBox.Text.Trim();
                        this.StartComLabel.Left = this.PointPictureBox.Right - (this.StartComLabel.Width / 2);
                        this.StartComLabel.Top = this.Line1Panel.Top - 53;
                        if (this.StartDistLabel.Location.X < this.panel2.Left || this.StartComLabel.Location.X < this.panel2.Left)
                        {
                            this.StartDistLabel.Left = (this.StartDistLabel.Width / 2) - this.panel2.Left;
                            ////this.StartDistLabel.Top = this.Line1Panel.Top + 21;

                            this.StartComLabel.Left = (this.StartDistLabel.Width / 2) - this.panel2.Left;
                            ////this.StartComLabel.Top = this.Line1Panel.Top + 36;
                        }
                        ////else if (this.StartComLabel.Right > (this.PointPictureBox.Right + (this.PointEventPanel.Width - this.PointPictureBox.Right)) || this.StartDistLabel.Right > (this.PointPictureBox.Right + (this.PointEventPanel.Width - this.PointPictureBox.Right)))
                        else if (this.StartComLabel.Right >= 773 || this.StartDistLabel.Right >= 792)
                        {
                            this.StartDistLabel.Left = this.PointPictureBox.Right - this.StartDistLabel.Width;
                            this.StartComLabel.Left = this.PointPictureBox.Right - this.StartComLabel.Width;
                        }
                        ////}
                        ////else
                        ////{
                        ////    this.StartDistLabel.Text = this.StartTextBox.Text.Trim() + " / " + this.OffsetTextBox.Text.Trim();
                        ////    this.StartDistLabel.Left = this.PointPictureBox.Right - this.StartDistLabel.Width;
                        ////    this.StartDistLabel.Top = this.Line1Panel.Top - 38;

                        ////    this.StartComLabel.Text = this.StartLabelTextBox.Text.Trim();
                        ////    this.StartComLabel.Left = this.PointPictureBox.Right - this.StartComLabel.Width;
                        ////    this.StartComLabel.Top = this.Line1Panel.Top - 53;
                        ////    if (this.StartDistLabel.Location.X < 0 || this.StartComLabel.Location.X < 0)
                        ////    {
                        ////        this.StartDistLabel.Left = (this.StartDistLabel.Width / 2) - this.PointPictureBox.Left;
                        ////        ////this.StartDistLabel.Top = this.Line1Panel.Top + 21;

                        ////        this.StartComLabel.Left = (this.StartDistLabel.Width / 2) - this.PointPictureBox.Left;
                        ////        ////this.StartComLabel.Top = this.Line1Panel.Top + 36;
                        ////    }
                        ////}

                        this.LeftEndLabel.Top = 78;
                        this.RightEndLabel.Top = 78;
                        this.RightEndLabel.Left = 664;
                    }
                    else
                    {
                        this.PointPictureBox.Left = this.Line1Panel.Left + Convert.ToInt32(location);
                        this.PointPictureBox.Top = this.Line1Panel.Top - 5;
                        ////this.tempLocation = this.StartDistLabel.Location.X + this.PointPictureBox.Right;
                        ////if (this.tempLocation < this.PointEventPanel.Width)
                        ////{
                        this.StartDistLabel.Text = this.StartTextBox.Text.Trim() + " / " + this.OffsetTextBox.Text.Trim();
                        this.StartDistLabel.Left = this.PointPictureBox.Right - (this.StartDistLabel.Width / 2);
                        this.StartDistLabel.Top = this.Line1Panel.Top - 21;

                        this.StartComLabel.Text = this.StartLabelTextBox.Text.Trim();
                        this.StartComLabel.Left = this.PointPictureBox.Right - (this.StartComLabel.Width / 2);
                        this.StartComLabel.Top = this.Line1Panel.Top - 36;
                        if (this.StartDistLabel.Location.X < this.panel2.Left || this.StartComLabel.Location.X < this.panel2.Left)
                        {
                            this.StartDistLabel.Left = (this.StartDistLabel.Width / 2) - this.panel2.Left;
                            ////this.StartDistLabel.Top = this.Line1Panel.Top + 21;

                            this.StartComLabel.Left = (this.StartDistLabel.Width / 2) - this.panel2.Left;
                            ////this.StartComLabel.Top = this.Line1Panel.Top + 36;
                        }
                        ////else if (this.StartComLabel.Right > (this.PointPictureBox.Right + (this.PointEventPanel.Width - this.PointPictureBox.Right)) || this.StartDistLabel.Right > (this.PointPictureBox.Right + (this.PointEventPanel.Width - this.PointPictureBox.Right)))
                        else if (this.StartComLabel.Right >= 773 || this.StartDistLabel.Right >= 792)
                        {
                            this.StartDistLabel.Left = this.PointPictureBox.Right - this.StartDistLabel.Width;
                            this.StartComLabel.Left = this.PointPictureBox.Right - this.StartComLabel.Width;
                        }
                        ////}
                        ////else
                        ////{
                        ////    this.StartDistLabel.Text = this.StartTextBox.Text.Trim() + " / " + this.OffsetTextBox.Text.Trim();
                        ////    this.StartDistLabel.Left = this.PointPictureBox.Right - this.StartDistLabel.Width;
                        ////    this.StartDistLabel.Top = this.Line1Panel.Top - 21;

                        ////    this.StartComLabel.Text = this.StartLabelTextBox.Text.Trim();
                        ////    this.StartComLabel.Left = this.PointPictureBox.Right - this.StartComLabel.Width;
                        ////    this.StartComLabel.Top = this.Line1Panel.Top - 36;
                        ////    if (this.StartDistLabel.Location.X < 0 || this.StartComLabel.Location.X < 0)
                        ////    {
                        ////        this.StartDistLabel.Left = (this.StartDistLabel.Width / 2) - this.PointPictureBox.Left;
                        ////        ////this.StartDistLabel.Top = this.Line1Panel.Top + 21;

                        ////        this.StartComLabel.Left = (this.StartDistLabel.Width / 2) - this.PointPictureBox.Left;
                        ////        ////this.StartComLabel.Top = this.Line1Panel.Top + 36;
                        ////    }
                        ////}

                        this.LeftEndLabel.Top = 78;
                        this.RightEndLabel.Top = 78;
                        this.RightEndLabel.Left = 664;
                    }

                    this.PointVisibility(true);
                }
                else
                {
                    this.PointVisibility(false);
                }
            }
        }

        /// <summary>
        /// Gets the Point event properties.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        private void GetPointEventProperties(int eventId)
        {
            Double offset;
            string offsettext;
            this.pointEventTypeDataSet = this.form8054Control.WorkItem.GetPointEventType(eventId);
            if (this.pointEventTypeDataSet.GetFSLinearEventType.Rows.Count > 0)
            {
                this.StartTextBox.Text = this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.StartColumn].ToString();
                offsettext = this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.OffsetColumn].ToString();
                offset = Convert.ToDouble(offsettext);
                if (offset > 0)
                {
                    this.OffsetTextBox.Text = "+" + this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.OffsetColumn].ToString();
                }
                else
                {
                    this.OffsetTextBox.Text = this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.OffsetColumn].ToString();
                }

                this.StartLabelTextBox.Text = this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.StartLabelColumn].ToString();
                this.CommentTextBox.Text = this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.CommentColumn].ToString();

                if (string.IsNullOrEmpty(this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.LengthColumn].ToString()))
                {
                    this.RightEndLabel.Text = "0";
                }
                else
                {
                    this.RightEndLabel.Text = this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.LengthColumn].ToString();
                }

                this.LeftEndLabel.Text = "0";
            }
            else
            {
                this.PointEventPanel.Enabled = false;
                this.PointVisibility(false);
            }
        }

        /// <summary>
        /// Saves the Point event properties.
        /// </summary>
        /// <returns>save the PointEvent Properties</returns>
        private bool SavePointEventProperties()
        {
            PointEventData pointEventData = new PointEventData();
            PointEventData.SaveFSLinearEventTypeRow dr = pointEventData.SaveFSLinearEventType.NewSaveFSLinearEventTypeRow();

            dr.EventID = this.keyId;
            dr.Start = Convert.ToDecimal(this.StartTextBox.Text.Trim());
            if (!string.IsNullOrEmpty(this.OffsetTextBox.Text.Trim()))
            {
                dr.Offset = Convert.ToDouble(this.OffsetTextBox.Text.Trim());
            }
            else
            {
                dr.Offset = 0.00;
            }

            dr.StartLabel = this.StartLabelTextBox.Text.Trim();
            dr.Comment = this.CommentTextBox.Text.Trim();
            if (this.pointEventTypeDataSet.GetFSLinearEventType.Rows.Count > 0)
            {
                dr.EndLabel = this.pointEventTypeDataSet.GetFSLinearEventType.Rows[0][this.pointEventTypeDataSet.GetFSLinearEventType.EndLabelColumn].ToString();
            }

            pointEventData.SaveFSLinearEventType.Rows.Add(dr);
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(pointEventData.SaveFSLinearEventType.Copy());
            tempDataSet.Tables[0].TableName = "Table";

            this.form8054Control.WorkItem.SavePointEventType(tempDataSet.GetXml(), TerraScanCommon.UserId);
            return true;
        }

        #endregion

    }
}
