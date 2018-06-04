//--------------------------------------------------------------------------------------------
// <copyright file="F8910.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Work Order General. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                 	M.Vijayakumar      Created
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
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using System.Web.Services.Protocols;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// F8910 class file
    /// </summary>
    public partial class F8910 : BaseSmartPart
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
        /// controller F8910
        /// </summary>
        private F8910Controller form8910Control;

        /// <summary>
        /// mock permission field for the mock userid
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance for GDocWorkorderCallInData
        /// </summary>
        private GDocWorkOrderGeneralData gdocWorkOrderGeneralData = new GDocWorkOrderGeneralData();

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;       

        #endregion Variables

        #region Constructor      

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8910"/> class.
        /// </summary>
        public F8910()
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
        public F8910(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.workOrderId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.GeneralPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GeneralPictureBox.Height, this.GeneralPictureBox.Width, tabText, red, green, blue);            
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
        /// For F8910Control
        /// </summary>
        [CreateNew]
        public F8910Controller Form8910Control
        {
            get { return this.form8910Control as F8910Controller; }
            set { this.form8910Control = value; }
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
                this.ClearWorkOrderGeneral();
                this.PanelUnLock(false);
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
                if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                this.PanelUnLock(true);
                this.ClearWorkOrderGeneral();
                this.CustomizeWorkOrderGeneral();
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
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
                        this.SaveWorkOrderGeneral();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.PanelUnLock(true);
                    this.ControlLock(false);
                    this.CustomizeWorkOrderGeneral();
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
                   
                    if (this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.Rows.Count > 0)
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
                    this.workOrderId = eventArgs.Data.SelectedKeyId;
                    this.LoadWorkOrderGeneral();
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

        #endregion Event Subscription

        #region Events

        /// <summary>
        /// Handles the Load event of the F8910 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8910_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkOrderGeneral();
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
        /// Handles the Click event of the GeneralPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the TextChanged event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableSaveCancelButton();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
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
        /// Handles the KeyPress event of the LocationNotesTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void LocationNotesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
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
        /// Handles the TextChanged event of the LocationNotesTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationNotesTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableSaveCancelButton();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the CorrectionsTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CorrectionsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
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
        /// Handles the TextChanged event of the CorrectionsTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CorrectionsTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableSaveCancelButton();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the GeneralPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocWorkOrderGeneralToolTip.SetToolTip(this.GeneralPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// To Loads the work order general form slices .
        /// </summary>
        private void LoadWorkOrderGeneral()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeWorkOrderGeneral();
            this.pageMode = TerraScanCommon.PageModeTypes.View;            
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// To Clear the Work order general
        /// </summary>
        private void ClearWorkOrderGeneral()
        {
            this.DescriptionTextBox.Text = string.Empty;
            this.LocationNotesTextBox.Text = string.Empty;
            this.CorrectionsTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To enable are to disable the panels
        /// </summary>
        /// <param name="unlock">
        /// true to disable the panels
        /// false to enable the panels        
        /// </param>
        private void PanelUnLock(bool unlock)
        {            
            this.DescriptionPanel.Enabled = unlock;
            this.LocationNotesPanel.Enabled = unlock;
            this.CorrectionsPanel.Enabled = unlock;
        }

        /// <summary>
        /// T0 enable are disable the Text Box controls
        /// </summary>
        /// <param name="controlLook">
        /// true to disable the controls
        /// false to enable the controls        
        /// </param>
        private void ControlLock(bool controlLook)
        {
            this.DescriptionTextBox.LockKeyPress = controlLook;
            this.LocationNotesTextBox.LockKeyPress = controlLook;
            this.CorrectionsTextBox.LockKeyPress = controlLook;
        }

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons
        /// </summary>
        private void EnableSaveCancelButton()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// To Customize Work order general controls
        /// </summary>
        private void CustomizeWorkOrderGeneral()
        {
            this.gdocWorkOrderGeneralData = this.form8910Control.WorkItem.F8910_GetWorkOrderGeneral(this.workOrderId);
            if (this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.Rows.Count > 0)
            {
                this.PanelUnLock(true);  
                this.DescriptionTextBox.Text = this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.Rows[0][this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.DescriptionColumn].ToString();                                     
                this.LocationNotesTextBox.Text = this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.Rows[0][this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.LocationNotesColumn].ToString();                 
                this.CorrectionsTextBox.Text = this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.Rows[0][this.gdocWorkOrderGeneralData.F8910_GetWorkOrderGeneral.CorrectionsColumn].ToString();                   
            }
            else
            {
                this.PanelUnLock(false);                    
            }           
        }

        /// <summary>
        /// To save the work order general
        /// </summary>
        private void SaveWorkOrderGeneral()
        {
            this.gdocWorkOrderGeneralData.F8910_SaveWorkOrderGeneral.Rows.Clear();
            GDocWorkOrderGeneralData.F8910_SaveWorkOrderGeneralRow dr = this.gdocWorkOrderGeneralData.F8910_SaveWorkOrderGeneral.NewF8910_SaveWorkOrderGeneralRow();

            dr.WOID = this.workOrderId;

            if (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                dr.Description = this.DescriptionTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.LocationNotesTextBox.Text.Trim()))
            {
                dr.LocationNotes = this.LocationNotesTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.CorrectionsTextBox.Text.Trim()))
            {
                dr.Corrections = this.CorrectionsTextBox.Text.Trim();
            }

            this.gdocWorkOrderGeneralData.F8910_SaveWorkOrderGeneral.Rows.Add(dr);

            this.gdocWorkOrderGeneralData.Merge(this.form8910Control.WorkItem.F8910_SaveWorkOrderGeneral(Utility.GetXmlString(this.gdocWorkOrderGeneralData.F8910_SaveWorkOrderGeneral.Copy()), TerraScanCommon.UserId));
        }

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

        #endregion Methods       
    }
}
