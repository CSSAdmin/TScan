//--------------------------------------------------------------------------------------------
// <copyright file="F8912.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Work Order Call In. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                 	M.Vijayakumar      Created
//                  Jayanthi           Modified
//*********************************************************************************/

namespace D8900
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
    using TerraScan.Utilities;

    /// <summary>
    /// F8912 class file
    /// </summary>
    public partial class F8912 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// workOrderId
        /// </summary>
        private int workOrderId;

        /// <summary>
        /// Used to store complainant address index value
        /// </summary>
        private int complainantAddId = -1;

        /// <summary>
        /// Used to store incident address index value
        /// </summary>
        private int incidentAddId = -1; 

        /// <summary>
        /// controller F8912
        /// </summary>
        private F8912Controller form8912Control;

        /// <summary>
        /// mock permission field for the mock userid
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance for GDocWorkorderCallInData
        /// </summary>
        private GDocWorkorderCallInData gdocWorkorderCallInData = new GDocWorkorderCallInData();

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        #endregion Variables

        #region Constructor      

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8912"/> class.
        /// </summary>
        public F8912()
        {
            InitializeComponent();            
        }

         /// <summary>
        /// Initializes a new instance of the <see cref="T:F8030"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8912(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.Tag = formNo;
            this.masterFormNo = masterform;
            this.workOrderId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.WorkOrderCallInPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.WorkOrderCallInPictureBox.Height, this.WorkOrderCallInPictureBox.Width, tabText, red, green, blue);            
        }

        #endregion Constructor       

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

        #region Property

        /// <summary>
        /// For F8030Control
        /// </summary>
        [CreateNew]
        public F8912Controller Form8912Control
        {
            get { return this.form8912Control as F8912Controller; }
            set { this.form8912Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearWorkOrderCallIn();
                this.PanelEnable(false);
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
            try
            {
                this.CustomizeWorkOrderCallIn();

                if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                this.PanelEnable(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.PermissionFiled.editPermission)
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.PermissionFiled.editPermission)
                    {
                        this.SaveWorkOrderCallIn();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.PanelEnable(true);
                    this.ControlLock(false);
                    this.CustomizeWorkOrderCallIn();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.workOrderId = eventArgs.Data.SelectedKeyId;
                    this.LoadWorkOrderCallIn();
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    
                    if (this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows.Count > 0)
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

        #endregion Event Subscription       

        #region Events

        /// <summary>
        /// Handles the Load event of the F8912 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8912_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkOrderCallIn();
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the WorkOrderCallInPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderCallInPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the TextChanged event of the ComplainantNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComplainantNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the ComplainantNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ComplainantNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the ComplainantPhoneTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ComplainantPhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the TextChanged event of the ComplainantPhoneTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComplainantPhoneTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the ComplainantEmailTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ComplainantEmailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the TextChanged event of the ComplainantEmailTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComplainantEmailTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the SelectionChangeCommitted event of the ComplainantAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComplainantAddressCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ComplainantAddressCombo.Text.Trim()))
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditEnabled();
                    }
                }
            }          
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ComplainantAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComplainantAddressCombo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ComplainantAddressCombo.Text.Trim()))
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditEnabled();
                    }
                }
            }          
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the ComplainantAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ComplainantAddressCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the ComplainantAddressTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ComplainantAddressTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the TextChanged event of the ComplainantAddressTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComplainantAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the SelectionChangeCommitted event of the IncidentAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void IncidentAddressCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.IncidentAddressCombo.Text.Trim()))
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditEnabled();
                    }
                }
            }          
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the IncidentAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void IncidentAddressCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the TextChanged event of the IncidentAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void IncidentAddressCombo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.IncidentAddressCombo.Text.Trim()))
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditEnabled();
                    }
                }
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the IncidentAddressTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void IncidentAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the IncidentAddressTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void IncidentAddressTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the MouseEnter event of the WorkOrderCallInPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderCallInPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocWorkOrderToolTip.SetToolTip(this.WorkOrderCallInPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the IncidentAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void IncidentAddressCombo_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.IncidentAddressCombo.Text.Trim().Length > 50)
                {
                    this.GDocWorkOrderToolTip.RemoveAll();
                    this.GDocWorkOrderToolTip.SetToolTip(this.IncidentAddressCombo, this.IncidentAddressCombo.Text);
                }
                else
                {
                    this.GDocWorkOrderToolTip.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ComplainantAddressCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComplainantAddressCombo_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.ComplainantAddressCombo.Text.Trim().Length > 50)
                {
                    this.GDocWorkOrderToolTip.RemoveAll();
                    this.GDocWorkOrderToolTip.SetToolTip(this.ComplainantAddressCombo, this.ComplainantAddressCombo.Text);
                }
                else
                {
                    this.GDocWorkOrderToolTip.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// To Loads the Work Order CallIn form slices.
        /// </summary>
        private void LoadWorkOrderCallIn()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeWorkOrderCallIn();
            this.pageMode = TerraScanCommon.PageModeTypes.View;           
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// To clear the Work order call in form 
        /// </summary>
        private void ClearWorkOrderCallIn()
        {
            this.ClearWorkOrder();
            this.ClearComplAddress();
            this.ClearIncidAddress();
        }

        /// <summary>
        /// To clear the work order call in form text boxs
        /// </summary>
        private void ClearWorkOrder()
        {
            this.ComplainantNameTextBox.Text = string.Empty;
            this.ComplainantPhoneTextBox.Text = string.Empty;
            this.ComplainantEmailTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To clear the Complainant Address combo box
        /// </summary>
        private void ClearComplAddress()
        {
            this.ComplainantAddressCombo.DataSource = null;
            this.ComplainantAddressCombo.Items.Clear();
            this.ComplainantAddressCombo.Refresh();
            this.ComplainantAddressCombo.Enabled = false;

            this.ComplainantAddressCombo.Text = string.Empty;
            this.ComplainantAddressTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To clear the Incident address combo box
        /// </summary>
        private void ClearIncidAddress()
        {
            this.IncidentAddressCombo.DataSource = null;
            this.IncidentAddressCombo.Items.Clear();
            this.IncidentAddressCombo.Refresh();
            this.IncidentAddressCombo.Enabled = false;

            this.IncidentAddressCombo.Text = string.Empty;
            this.IncidentAddressTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To Enable or disable the all panels
        /// </summary>
        /// <param name="unlock">
        ///  true - enables the panels
        ///  false - disables the panels
        /// </param>
        private void PanelEnable(bool unlock)
        {
            this.ComplainantNamePanel.Enabled = unlock;
            this.ComplainantPhonePanel.Enabled = unlock;
            this.ComplainantEmailPanel.Enabled = unlock;
            this.ComplainantAddressPanel.Enabled = unlock;
            this.IncidentAddressPanel.Enabled = unlock;
        }

        /// <summary>
        /// To Lock the controls
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.ComplainantNameTextBox.LockKeyPress = controlLook;
            this.ComplainantPhoneTextBox.LockKeyPress = controlLook;
            this.ComplainantEmailTextBox.LockKeyPress = controlLook;
            this.ComplainantAddressCombo.Enabled = !controlLook;
            this.ComplainantAddressTextBox.LockKeyPress = controlLook;
            this.IncidentAddressCombo.Enabled = !controlLook;
            this.IncidentAddressTextBox.LockKeyPress = controlLook;
        }

        /// <summary>
        /// Customizes the work order call in.
        /// </summary>
        private void CustomizeWorkOrderCallIn()
        {
            this.gdocWorkorderCallInData = this.form8912Control.WorkItem.F8912_GetWorkOrderCallIn(this.workOrderId);
            if (this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows.Count > 0)
            {
                this.PanelEnable(true);
                this.ComplainantNameTextBox.Text = this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows[0][this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.ComplNameColumn].ToString();
                this.ComplainantPhoneTextBox.Text = this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows[0][this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.ComplPhoneNoColumn].ToString();
                this.ComplainantEmailTextBox.Text = this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows[0][this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.ComplEmailColumn].ToString();

                if (!string.IsNullOrEmpty(this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows[0][this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.ComplAddIDColumn].ToString()))
                {
                    this.complainantAddId = Convert.ToInt32(this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows[0][this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.ComplAddIDColumn]);
                }
                else
                {
                    this.complainantAddId = -1;
                }

                if (!string.IsNullOrEmpty(this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows[0][this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.AddIDColumn].ToString()))
                {
                    this.incidentAddId = Convert.ToInt32(this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.Rows[0][this.gdocWorkorderCallInData.F8912_GetWorkOrderCallIn.AddIDColumn]);
                }
                else
                {
                    this.incidentAddId = -1;
                }

                this.LoadComplAddress();
                this.LoadIncidentAddress();

                this.ControlLock(false);
                this.PanelEnable(true);
            }
            else
            {
                this.ClearWorkOrder();
                this.ClearComplAddress();
                this.ClearIncidAddress();
                this.PanelEnable(false);
                this.ControlLock(false);
            }
        }

        /// <summary>
        /// Loads the complaliant address Combo box.
        /// </summary>
        private void LoadComplAddress()
        {           
            this.gdocWorkorderCallInData = this.form8912Control.WorkItem.wListAddresses();

            if (this.gdocWorkorderCallInData.ListAddresses.Rows.Count > 0)
            {
                this.ComplainantAddressCombo.DataSource = this.gdocWorkorderCallInData.ListAddresses;
                this.ComplainantAddressCombo.DisplayMember = this.gdocWorkorderCallInData.ListAddresses.ComplAddressColumn.ColumnName;
                this.ComplainantAddressCombo.ValueMember = this.gdocWorkorderCallInData.ListAddresses.ComplIDColumn.ColumnName;

                ////if (!string.IsNullOrEmpty(this.complainantAddId.ToString()))
                if (this.complainantAddId > 0)
                {   
                    this.ComplainantAddressCombo.SelectedValue = this.complainantAddId;
                }
                else
                {
                    this.ComplainantAddressCombo.SelectedIndex = -1;
                }
            }           
        }

        /// <summary>
        /// Loads the incident address combo box.
        /// </summary>
        private void LoadIncidentAddress()
        {           
            this.gdocWorkorderCallInData = this.form8912Control.WorkItem.wListAddresses();
            if (this.gdocWorkorderCallInData.ListAddresses.Rows.Count > 0)
            {
                this.IncidentAddressCombo.DataSource = this.gdocWorkorderCallInData.ListAddresses;
                this.IncidentAddressCombo.DisplayMember = this.gdocWorkorderCallInData.ListAddresses.IncidentAddressColumn.ColumnName;
                this.IncidentAddressCombo.ValueMember = this.gdocWorkorderCallInData.ListAddresses.IncidentIDColumn.ColumnName;

                ////if (!string.IsNullOrEmpty(this.incidentAddId.ToString()))
                if (this.incidentAddId > 0)
                {                       
                    this.IncidentAddressCombo.SelectedValue = this.incidentAddId;
                }
                else
                {
                    this.IncidentAddressCombo.SelectedIndex = -1;
                }
            }           
        }

        /// <summary>
        /// Saves the work order call in.
        /// </summary>
        private void SaveWorkOrderCallIn()
        {
            this.gdocWorkorderCallInData.F8912_SaveWorkOrderCallIn.Rows.Clear();
            GDocWorkorderCallInData.F8912_SaveWorkOrderCallInRow dr = this.gdocWorkorderCallInData.F8912_SaveWorkOrderCallIn.NewF8912_SaveWorkOrderCallInRow();

            dr.WOID = this.workOrderId;
            if (!string.IsNullOrEmpty(this.ComplainantNameTextBox.Text.Trim()))
            {
                dr.ComplName = this.ComplainantNameTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ComplainantPhoneTextBox.Text.Trim()))
            {
                dr.ComplPhoneNo = this.ComplainantPhoneTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ComplainantEmailTextBox.Text.Trim()))
            {
                dr.ComplEmail = this.ComplainantEmailTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.ComplainantAddressCombo.Text.Trim()))
            {
                dr.ComplAddID = Convert.ToInt32(this.ComplainantAddressCombo.SelectedValue);
            }
            else
            {
                dr.ComplAddID = 0;
            }

            if (!string.IsNullOrEmpty(this.IncidentAddressCombo.Text.Trim()))
            {
                dr.AddID = Convert.ToInt32(this.IncidentAddressCombo.SelectedValue);
            }
            else
            {
                dr.AddID = 0;
            }

            this.gdocWorkorderCallInData.F8912_SaveWorkOrderCallIn.Rows.Add(dr);

            this.gdocWorkorderCallInData.Merge(this.form8912Control.WorkItem.F8912_SaveWorkOrderCallIn(Utility.GetXmlString(this.gdocWorkorderCallInData.F8912_SaveWorkOrderCallIn.Copy()), TerraScanCommon.UserId));
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

            if (!string.IsNullOrEmpty(this.ComplainantEmailTextBox.Text.Trim()) && !TerraScanCommon.CheckValidEmailID(this.ComplainantEmailTextBox.Text.Trim()))
            {  
               this.ComplainantEmailTextBox.Focus();                    
               sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("EmailValidation");
               return sliceValidationFields;            
            }

            ////To Verify whether the selected value is valid Value member
            if (!string.IsNullOrEmpty(Convert.ToString(this.ComplainantAddressCombo.Text)) && string.IsNullOrEmpty(Convert.ToString(this.ComplainantAddressCombo.SelectedValue)))
            {       
               sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("CallInInValidComplainant");
               return sliceValidationFields;
            }

            ////To Verify whether the selected value is valid Value member
            if (!string.IsNullOrEmpty(Convert.ToString(this.IncidentAddressCombo.Text)) && string.IsNullOrEmpty(Convert.ToString(this.IncidentAddressCombo.SelectedValue)))
            {                
               sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("CallInInValidIncident");
               return sliceValidationFields;
            }            

            sliceValidationFields.RequiredFieldMissing = false;  
            return sliceValidationFields;
        }

        #region To Enable the Save and Cancel Buttons

        /// <summary>
        /// Edits the enabled.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        #endregion To Enable the Save and Cancel Buttons       

        private void ComplainantAddressCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Methods     

        private void IncidentAddressCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
