//--------------------------------------------------------------------------------------------
// <copyright file="F8902.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8902.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Oct 06        VINOTH              Created
// 5 Jan 06         VijayKumar          Modified/Added for OnD9030_F9030_LoadSliceDetails
//**********************************************************************************************/

namespace D8900
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
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.SmartParts;

    /// <summary>
    /// F8902 Class
    /// </summary>
    [SmartPart]
    public partial class F8902 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int workId;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F8902Controller form8902Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Instance for GDocEventHeaderData
        /// </summary>
        private F8902HeaderData.GetWorkOrderHeaderDataTable workOrderHeaderData = new F8902HeaderData.GetWorkOrderHeaderDataTable();

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Has the value from delete message dialog box
        /// </summary>
        private bool deleteMessageResult;

        #endregion

        #region Constructor

        /// <summary>
        /// F8902 Constructor
        /// </summary>
        public F8902()
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
        public F8902(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.workId = keyID;
            this.masterFormNo = masterform;
            this.formMasterPermissionEdit = permissionEdit;           
            this.WorkOrderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(39, 42, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

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

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;  

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8902 control.
        /// </summary>
        /// <value>The F8902 control.</value>
        [CreateNew]
        public F8902Controller Form8902Control
        {
            get { return this.form8902Control as F8902Controller; }
            set { this.form8902Control = value; }
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
                }
                
                this.LockControls(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);                    

                if (this.workOrderHeaderData.Rows.Count > 0)
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
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearHeaderFields();
                this.PanelEnable(false);
            }
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
                    this.SaveHeaderProperties();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.LockControls(true);
                ////this.ClosedCheckBox.Enabled = true;
                this.PanelEnable(true);
                this.GetHeaderProperties();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                this.LockControls(true);
                ////this.ClosedCheckBox.Enabled = false;
            }
            else
            {
                this.LockControls(false);
                ////this.ClosedCheckBox.Enabled = true;
            }

            this.PanelEnable(true);
            this.GetHeaderProperties();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }
        
        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.DeleteHeaderProperties();
                if (this.deleteMessageResult == true)
                {
                    ////call the delete method
                    DataSet menuDataSet = (DataSet)this.form8902Control.WorkItem.RootWorkItem.State["FormItemsDataSet"];
                    this.PanelEnable(false);
                    this.LockControls(false);

                    DataRow[] formExistRow;
                    formExistRow = menuDataSet.Tables[0].Select("Form =8901 and Active = 1");
                    if (formExistRow.Length > 0)
                    {
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(8901);
                        formInfo.optionalParameters = new object[1];
                        formInfo.optionalParameters[0] = "RecordDeleted";
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }

                    FormInfo formInform;
                    formInform = TerraScanCommon.GetFormInfo(this.masterFormNo);
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInform));
                    SliceFormCloseAlert sliceFormCloseAlert;
                    sliceFormCloseAlert.FormNo = this.masterFormNo;
                    sliceFormCloseAlert.FlagFormClose = true;
                    this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                }
                else
                {
                    this.LockControls(true);
                    this.GetHeaderProperties();

                    SliceFormCloseAlert sliceFormCloseAlert;
                    sliceFormCloseAlert.FormNo = this.masterFormNo;
                    sliceFormCloseAlert.FlagFormClose = false;
                    this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                }
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
                    this.workId = eventArgs.Data.SelectedKeyId;
                    this.LoadHeaderProperties();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion        

        #region Load

        /// <summary>
        /// F8902 Load
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void F8902_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadHeaderProperties();
                ////this.LockControls(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion       

        #region PrivateMethod

        /// <summary>
        /// Loads the header properties.
        /// </summary>
        private void LoadHeaderProperties()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.GetHeaderProperties();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
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
            this.WorkOrderHeaderDatePict.Enabled = lockControl;
            this.ClosedCheckBox.Enabled = lockControl;
            this.WorkOrderTimePicker.Enabled = lockControl;
            this.WorkOrderDate.LockKeyPress = !lockControl;            
        }

        /// <summary>
        /// To set the Panel Visiblity
        /// </summary>
        /// <param name="enable">bool</param>
        private void PanelEnable(bool enable)
        {
            this.WorkOrderPanel.Enabled = enable;
            this.EventDatePanel.Enabled = enable;
            this.ClosedPanel.Enabled = enable;
            this.WorkOrderIDPanel.Enabled = enable;
            this.WorkOrderPictureBox.Enabled = enable;            
        }

        /// <summary>
        /// Clears the Header event properties.
        /// </summary>
        private void ClearHeaderFields()
        {
            this.WorkIdTextBox.Text = string.Empty;
            this.WorkOrderDate.Text = string.Empty;
            this.ClosedCheckBox.Checked = false;
            this.WorkOrderTextBox.Text = string.Empty;
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

            if (string.IsNullOrEmpty(this.WorkOrderDate.Text.Trim()))
            {
                this.WorkOrderHeaderDatePict.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.WorkIdTextBox.Text.Trim()))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (Convert.ToDateTime(this.WorkOrderDate.Text) > DateTime.Now)
            {
                ////MessageBox.Show("From check error");
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F8902DateValidation");
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Get Header Properties
        /// </summary>               
        private void GetHeaderProperties()
        {
            this.Cursor = Cursors.WaitCursor;
            this.workOrderHeaderData = this.form8902Control.WorkItem.GetHeader(this.workId);
            if (this.workOrderHeaderData.Rows.Count > 0)
            {
                this.WorkIdTextBox.Text = this.workOrderHeaderData.Rows[0][this.workOrderHeaderData.WOTypeColumn].ToString();
                this.WorkOrderDate.Text = this.workOrderHeaderData.Rows[0][this.workOrderHeaderData.WODateColumn].ToString();
                if (!string.IsNullOrEmpty(this.workOrderHeaderData.Rows[0][this.workOrderHeaderData.IsOpenColumn].ToString()))
                {
                    this.ClosedCheckBox.Checked = Convert.ToBoolean(this.workOrderHeaderData.Rows[0][this.workOrderHeaderData.IsOpenColumn].ToString());
                }
                else
                {
                    this.ClosedCheckBox.Checked = false;
                }

                if (!string.IsNullOrEmpty(this.workOrderHeaderData.Rows[0][this.workOrderHeaderData.WOIDColumn].ToString()))
                {
                    this.WorkOrderTextBox.Text = this.workOrderHeaderData.Rows[0][this.workOrderHeaderData.WOIDColumn].ToString();
                }
                else
                {
                    this.WorkOrderTextBox.Text = string.Empty;
                }

                this.PanelEnable(true);
            }
            else
            {
                this.ClearHeaderFields();
                this.LockControls(false);
                this.PanelEnable(false);
            }

           this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Save Header Properties
        /// </summary>
        /// <returns>bool</returns>
        private bool SaveHeaderProperties()
        {
            this.Cursor = Cursors.WaitCursor;
            F8902HeaderData headerData = new F8902HeaderData();
            F8902HeaderData.SaveWorkOrderHeaerRow dr = headerData.SaveWorkOrderHeaer.NewSaveWorkOrderHeaerRow();

            if (!string.IsNullOrEmpty(this.WorkIdTextBox.Text.Trim()))
            {
                dr.WOType = this.workOrderHeaderData.Rows[0][this.workOrderHeaderData.WOTypeColumn].ToString();
            }

            dr.WODate = this.WorkOrderDate.Text.Trim();
            if (ClosedCheckBox.Checked)
            {
                dr.IsOpen = true;
            }
            else
            {
                dr.IsOpen = false;
            }

            dr.WOID = this.workId;
            headerData.SaveWorkOrderHeaer.Rows.Add(dr);
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(headerData.SaveWorkOrderHeaer.Copy());
            tempDataSet.Tables[0].TableName = "Table";

            this.form8902Control.WorkItem.SaveHeader(tempDataSet.GetXml(),TerraScanCommon.UserId);
            this.GetHeaderProperties();                
            return true;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Delete Header Properties
        /// </summary>
        private void DeleteHeaderProperties()
        {
            DialogResult dialogResult;
            this.Cursor = Cursors.WaitCursor;
            this.workOrderHeaderData = this.form8902Control.WorkItem.GetHeader(this.workId);

            if (this.workOrderHeaderData.Rows.Count > 0)
            {
                dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F8902WorkOrderConfirmDelete"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.form8902Control.WorkItem.DeleteHeader(this.workId, TerraScanCommon.UserId);
                    this.deleteMessageResult = true;

                    FormInfo formInform;
                    formInform = TerraScanCommon.GetFormInfo(this.masterFormNo);
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInform));
                    SliceFormCloseAlert sliceFormCloseAlert;
                    sliceFormCloseAlert.FormNo = this.masterFormNo;
                    sliceFormCloseAlert.FlagFormClose = true;
                    this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));                        
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.deleteMessageResult = false;
                    return;
                }                                     
            }                
            this.Cursor = Cursors.Default;
        }        

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the WorkOrderTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderHeaderDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(WorkOrderDate.Text.Trim()))
                {
                    WorkOrderTimePicker.Value = Convert.ToDateTime(WorkOrderDate.Text);
                }
                else
                {
                    WorkOrderTimePicker.Value = DateTime.Today;
                }

                WorkOrderTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the WorkOrderTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.WorkOrderDate.Text = WorkOrderTimePicker.Text;
                this.ParentForm.ActiveControl=WorkOrderDate;
                this.ParentForm.ActiveControl.Focus();

            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the WorkOrderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.WorkOrderHeaderToolTip.SetToolTip(this.WorkOrderPictureBox, "D8900.F8902");
        }

        /// <summary>
        /// Handles the MouseClick event of the ClosedCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClosedCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the TextChanged event of the WorkOrderDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the WorkOrderDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (char.IsDigit(e.KeyChar))
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditEnabled();
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the WorkOrderTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (char.IsDigit(e.KeyChar))
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
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the WorkOrderTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderTextBox_KeyDown(object sender, KeyEventArgs e)
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
        
        #endregion       

        private void WorkOrderTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }

                ClosedCheckBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
