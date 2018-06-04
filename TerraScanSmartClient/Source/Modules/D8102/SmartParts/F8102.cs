//--------------------------------------------------------------------------------------------
// <copyright file="F8102.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8102.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8102
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
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

    /// <summary>
    /// F8102 class file
    /// </summary>
    [SmartPart]
    public partial class F8102 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// eventPropertiesDataSet variable is used to get the details of event properties.
        /// </summary>
        private SanitaryPipeInspectionData eventPropertiesDataSet = new SanitaryPipeInspectionData();

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F8102Controller form8102Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// recordEdited Local variable.
        /// </summary>
        private bool recordEdited = false;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// PermissionFields struct 
        /// </summary>
        private PermissionFields permissions;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8102"/> class.
        /// </summary>
        public F8102()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8102"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8102(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
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

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8102 control.
        /// </summary>
        /// <value>The F8102 control.</value>
        [CreateNew]
        public F8102Controller Form8102Control
        {
            get { return this.form8102Control as F8102Controller; }
            set { this.form8102Control = value; }
        }

        /// <summary>
        /// Gets or sets the FormId
        /// </summary>
        /// <value>The FormId.</value>
        public int KeyId
        {
            set { this.keyId = value; }
            get { return this.keyId; }
        }
        #endregion

        #region Event Subscription

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
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
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
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.ClearEventProperties();
            this.DisableControls(false);
            ////this.EventPropertiesPanel.Enabled = false;
            ////this.LockControls(true);
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.PermissionFiled.deletePermission)
            {
                ////call the delete method
                ////SliceFormCloseAlert sliceFormCloseAlert;
                ////sliceFormCloseAlert.FormNo = this.masterFormNo;
                ////sliceFormCloseAlert.FlagFormClose = false;
                ////this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
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

            this.DisableControls(true);
            ////this.EventPropertiesPanel.Enabled = true;
            ////this.LockControls(false);
            this.GetEventProperties(this.keyId);
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
                    this.SaveEventProperties();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    ////khaja added code to fix Bug#4211
                    this.FormTextBox.Focus();
                }
            }
            else
            {
                this.DisableControls(true);
                ////this.EventPropertiesPanel.Enabled = true;
                this.LockControls(false);
                this.GetEventProperties(this.keyId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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

                    this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                    if (this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows.Count > 0)
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
                this.GetEventProperties(this.keyId);
            }
        }

        #endregion

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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F8102 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8102_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GetEventProperties(this.keyId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Gets the event properties.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        private void GetEventProperties(int eventId)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.eventPropertiesDataSet = this.form8102Control.WorkItem.GetEventEngineEventProperties(eventId);
                if (this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows.Count > 0)
                {
                    this.FormTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.FormColumn].ToString();
                    this.NumberTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.NumberColumn].ToString();
                    this.StartTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.StartColumn].ToString();
                    this.StopTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.StopColumn].ToString();
                    this.StructuralTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.StructuralColumn].ToString();
                    this.RootsTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.RootsColumn].ToString();
                    this.SummaryTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.SummaryColumn].ToString();
                    this.RecommendationTextBox.Text = this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.RecommendationColumn].ToString();
                    this.ReverseCheckBox.Checked = Convert.ToBoolean(this.eventPropertiesDataSet.GetEventEngineEventProperties.Rows[0][this.eventPropertiesDataSet.GetEventEngineEventProperties.ReverseColumn].ToString());
                }
                else
                {
                    this.DisableControls(false);
                    ////this.EventPropertiesPanel.Enabled = false;
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
        /// Clears the event properties.
        /// </summary>
        private void ClearEventProperties()
        {
            this.FormTextBox.Text = string.Empty;
            this.NumberTextBox.Text = string.Empty;
            this.StartTextBox.Text = string.Empty;
            this.StopTextBox.Text = string.Empty;
            this.StructuralTextBox.Text = string.Empty;
            this.RootsTextBox.Text = string.Empty;
            this.SummaryTextBox.Text = string.Empty;
            this.RecommendationTextBox.Text = string.Empty;
            this.ReverseCheckBox.Checked = false;
        }

        /// <summary>
        /// Lock the event properties.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockControls(bool lockControl)
        {
            this.FormTextBox.LockKeyPress = lockControl;
            this.NumberTextBox.LockKeyPress = lockControl;
            this.StartTextBox.LockKeyPress = lockControl;
            this.StopTextBox.LockKeyPress = lockControl;
            this.StructuralTextBox.LockKeyPress = lockControl;
            this.RootsTextBox.LockKeyPress = lockControl;
            this.SummaryTextBox.LockKeyPress = lockControl;
            this.RecommendationTextBox.LockKeyPress = lockControl;
            this.ReverseCheckBox.Enabled = !lockControl;
        }

        /// <summary>
        /// Disables the header controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void DisableControls(bool enable)
        {
            this.FormPanel.Enabled = enable;
            this.NumberPanel.Enabled = enable;
            this.StartPanel.Enabled = enable;
            this.StopPanel.Enabled = enable;
            this.ReversePanel.Enabled = enable;
            this.StructuralPanel.Enabled = enable;
            this.RootsPanel.Enabled = enable;
            this.SummaryPanel.Enabled = enable;
            this.RecommendationPanel.Enabled = enable;
        }

        /// <summary>
        /// Saves the event properties.
        /// </summary>
        private void SaveEventProperties()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SanitaryPipeInspectionData sanitaryPipeInspectionData = new SanitaryPipeInspectionData();
                SanitaryPipeInspectionData.SaveEventEngineEventPropertiesRow dr = sanitaryPipeInspectionData.SaveEventEngineEventProperties.NewSaveEventEngineEventPropertiesRow();

                dr.EventID = this.keyId;
                dr.Form = this.FormTextBox.Text.Trim();
                dr.Number = this.NumberTextBox.Text.Trim();
                dr.Start = this.StartTextBox.Text.Trim();
                dr.Stop = this.StopTextBox.Text.Trim();
                if (this.ReverseCheckBox.Checked == true)
                {
                    dr.Reverse = true;
                }
                else
                {
                    dr.Reverse = false;
                }

                int tempStructural;
                int.TryParse(this.StructuralTextBox.Text.Trim(), out tempStructural);
                dr.Structural = Convert.ToByte(tempStructural);

                int tempRoot;
                int.TryParse(this.RootsTextBox.Text.Trim(), out tempRoot);
                dr.Roots = Convert.ToByte(tempRoot);

                dr.Summary = this.SummaryTextBox.Text.Trim();
                dr.Recommendation = this.RecommendationTextBox.Text.Trim();

                sanitaryPipeInspectionData.SaveEventEngineEventProperties.Rows.Add(dr);
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(sanitaryPipeInspectionData.SaveEventEngineEventProperties.Copy());
                tempDataSet.Tables[0].TableName = "Table";

                this.form8102Control.WorkItem.SaveEventEngineEventProperties(tempDataSet.GetXml(), TerraScanCommon.UserId);
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
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D8102.F8102"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
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
        /// Handles the KeyPress event of the FormTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void FormTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.recordEdited = true;
                ////khaja commented code to fix Bug#4213
                ////this.EditEnabled();
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

        ////khaja added textchanged event to fix Bug#4213

        /// <summary>
        /// Handles the TextChanged event of the FormTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FormTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.recordEdited)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ReverseCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReverseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.recordEdited)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReverseCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReverseCheckBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.recordEdited = true;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the SummaryTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SummaryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    return;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    this.recordEdited = true;
                    this.EditEnabled();
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
                this.SanitaryPipeInspDetailsToolTip.SetToolTip(this.StatementPictureBox, "D8102.F8102");
            }
            catch (Exception ex)
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

            byte maxStructValue = byte.MaxValue;
            int tempStructural, tempRoot;
            int.TryParse(this.StructuralTextBox.Text, out tempStructural);
            int.TryParse(this.RootsTextBox.Text, out tempRoot);

            if ((Convert.ToInt32(tempStructural) > maxStructValue))
            {
                sliceValidationFields.ErrorMessage = "Structural value should not exceed 255";
                this.StructuralTextBox.Text = "255";
            }
            else if (Convert.ToInt32(tempRoot) > maxStructValue)
            {
                sliceValidationFields.ErrorMessage = sliceValidationFields.ErrorMessage + "Roots value should not exceed 255";
                this.RootsTextBox.Text = "255";
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the max value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CheckMaxValue(object sender, EventArgs e)
        {
        }
    }
}
