//-------------------------------------------------------------------------------------------------------------------
// <copyright file="F25009.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25009.
// </summary>
//-------------------------------------------------------------------------------------------------------------------
// Change History
//*******************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ------------------------------------------------------------------------------
// 07 May 07        KARTHIKEYAN.B      Created
// 25 June 07       M.Vijayakumar      AutoComplete for combox,invalid key id and permission are set properly
//******************************************************************************************************************/

namespace D20009
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
    /// This contains methods for the F25009
    /// </summary>
    public partial class F25009 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// legalId
        /// </summary>
        private int legalId;

        /// <summary>
        /// Legal Combo flag
        /// </summary>
        private bool comboFocusFlag;

        /// <summary>
        /// Form25009 Controller
        /// </summary>
        private F25009Controller form25009Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// An instance for legalDescriptionData
        /// </summary>        
        private F25009LegalManagementData legalManagementData = new F25009LegalManagementData();

        /// <summary>
        /// Instance for F2001ParcelLockingData
        /// </summary>
        private F2001ParcelLockingData parcelLockingData = new F2001ParcelLockingData();

        ///<summary>
        /// Instance for Hold the value for the Form
        /// </summary>

        private short nwnw;
        private short nwne;
        private short nwsw;
        private short nwse;
        private short swnw;
        private short swne;
        private short swsw;
        private short swse;
        private short nenw;
        private short nene;
        private short nesw;
        private short nese;
        private short senw;
        private short sene;
        private short sesw;
        private short sese;

        #endregion

        #region Variable For ComboBox Value Members

        /// <summary>
        /// Subdivision / Section ID
        /// </summary>
        private int subDivisionId;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25009"/> class.
        /// </summary>
        public F25009()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25009"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F25009(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.LegalDescriptionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalDescriptionPictureBox.Height, this.LegalDescriptionPictureBox.Width, tabText, red, green, blue);
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

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F25009 control.
        /// </summary>
        /// <value>The F25009 control.</value>
        [CreateNew]
        public F25009Controller Form25009Control
        {
            get { return this.form25009Control as F25009Controller; }
            set { this.form25009Control = value; }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ClearLegalManagementControls();
                this.SetNewComboIndex();
                this.LockControls(true);
                this.ControlLock(false);
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearLegalManagement();
                this.SetNewComboIndex();
                this.LockControls(false);
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
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LockControls(true);
            this.CustomizeLegalManagement();
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
                    this.SaveLegalManagement();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SubdivisionComboBox.Focus();
                }
            }
            else
            {
                this.LockControls(true);
                this.ControlLock(false);
                this.CustomizeLegalManagement();
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

                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                    ////to check for invalid key id
                    this.keyId = eventArgs.Data.KeyId;
                    this.parcelLockingData = this.form25009Control.WorkItem.F2001_getParcelLockingDetails(this.keyId);

                    if (this.parcelLockingData.f2001ParcelLock.Rows.Count > 0)
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
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.CustomizeLegalManagement();
                this.subDivisionId = 0;
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
            }
        }

        /// <summary>
        /// Forms the close.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_BaseSmartPart_formClose, Thread = ThreadOption.UserInterface)]
        public void FormClose(object sender, DataEventArgs<string> e)
        {
            if (e.Data == "ApplicationExitCall")
            {
                TerraScanCommon.FormName = string.Empty;
            }
            else if (e.Data == "UserClosing")
            {
                var formn = (BaseSmartPart)sender;
                //Form varq = sender as Form;
                if (this.masterFormNo.Equals(formn.ParentFormId))
                {
                    if (this.keyId != -99)
                    {
                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = 20000;
                        sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                        this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    }
                }
            }
        }

        #endregion

        #region Protected Methods

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
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            this.D9030_F9030_LoadSliceDetails(this, eventArgs);
        }

        #endregion

        #region Form Control Events

        /// <summary>
        /// F25009 Form Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F25009_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                ////Set the Max Length for editable fields
                this.SetMaxLength();
                this.CustomizeLegalManagement();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.flagLoadOnProcess = false;
                if (this.comboFocusFlag)
                {
                    this.ActiveControl = SubdivisionComboBox;
                    SubdivisionComboBox.Focus();
                    this.comboFocusFlag = false;
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the LegalDescriptionPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LegalDescriptionPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the LegalDescriptionPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LegalDescriptionPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.LegalManagementToolTip.SetToolTip(this.LegalDescriptionPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the Direction Label controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DirectionLabels_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                string firstTwoChars;
                string nextTwoChars;
                Label senderLabel = (Label)sender;
                firstTwoChars = senderLabel.Name.Substring(0, 2);
                nextTwoChars = senderLabel.Name.Substring(2, 2);
                if ((string)senderLabel.Tag == "0" || (string)senderLabel.Tag == "2")
                {
                    this.LegalManagementToolTip.SetToolTip(senderLabel, nextTwoChars + " 1/4 of " + firstTwoChars + " 1/4");
                }
                else if ((string)senderLabel.Tag == "1")
                {
                    this.LegalManagementToolTip.SetToolTip(senderLabel, "Partial " + nextTwoChars + " 1/4 of " + firstTwoChars + " 1/4");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To change the color of the Direction Labels based on the current value
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DirectionLabels_Click(object sender, EventArgs e)
        {
            try
            {
               // int tempKeyId;
                ////Save the DIrection Label's value immediately
                this.Cursor = Cursors.WaitCursor;

                //Used for edit Mode Enabled While Change
                this.ToEnableEditButtonInMasterForm();
                Label senderLabel = (Label)sender;

                this.nwnw = this.GetDirectionLabelValue(this.NWNWLabel, sender);
                this.nwne = this.GetDirectionLabelValue(this.NWNELabel, sender);
                this.nwsw = this.GetDirectionLabelValue(this.NWSWLabel, sender);
                this.nwse = this.GetDirectionLabelValue(this.NWSELabel, sender);

                this.nenw = this.GetDirectionLabelValue(this.NENWLabel, sender);
                this.nene = this.GetDirectionLabelValue(this.NENELabel, sender);
                this.nesw = this.GetDirectionLabelValue(this.NESWLabel, sender);
                this.nese = this.GetDirectionLabelValue(this.NESELabel, sender);

                this.swnw = this.GetDirectionLabelValue(this.SWNWLabel, sender);
                this.swne = this.GetDirectionLabelValue(this.SWNELabel, sender);
                this.swsw = this.GetDirectionLabelValue(this.SWSWLabel, sender);
                this.swse = this.GetDirectionLabelValue(this.SWSELabel, sender);

                this.senw = this.GetDirectionLabelValue(this.SENWLabel, sender);
                this.sene = this.GetDirectionLabelValue(this.SENELabel, sender);
                this.sesw = this.GetDirectionLabelValue(this.SESWLabel, sender);
                this.sese = this.GetDirectionLabelValue(this.SESELabel, sender);
                /*
                                this.legalManagementData.GetLegalManagement.Rows.Clear();
                                F25009LegalManagementData.GetLegalManagementRow dr = this.legalManagementData.GetLegalManagement.NewGetLegalManagementRow();

                                dr.ParcelID = this.keyId;
                                dr.LegalID = this.legalId;
                                dr.IsPartialLot = false;
                                dr.IsPartialBlock = false;
                                dr.AssembledLegal = string.Empty;
                
               
 
                                Label senderLabel = (Label)sender;
                
                                //Changed to Hold in a value
                                #region Direction Labels

                                dr.NWNW = this.GetDirectionLabelValue(this.NWNWLabel, sender);
                                dr.NWNE = this.GetDirectionLabelValue(this.NWNELabel, sender);
                                dr.NWSW = this.GetDirectionLabelValue(this.NWSWLabel, sender);
                                dr.NWSE = this.GetDirectionLabelValue(this.NWSELabel, sender);

                                dr.NENW = this.GetDirectionLabelValue(this.NENWLabel, sender);
                                dr.NENE = this.GetDirectionLabelValue(this.NENELabel, sender);
                                dr.NESW = this.GetDirectionLabelValue(this.NESWLabel, sender);
                                dr.NESE = this.GetDirectionLabelValue(this.NESELabel, sender);

                                dr.SWNW = this.GetDirectionLabelValue(this.SWNWLabel, sender);
                                dr.SWNE = this.GetDirectionLabelValue(this.SWNELabel, sender);
                                dr.SWSW = this.GetDirectionLabelValue(this.SWSWLabel, sender);
                                dr.SWSE = this.GetDirectionLabelValue(this.SWSELabel, sender);

                                dr.SENW = this.GetDirectionLabelValue(this.SENWLabel, sender);
                                dr.SENE = this.GetDirectionLabelValue(this.SENELabel, sender);
                                dr.SESW = this.GetDirectionLabelValue(this.SESWLabel, sender);
                                dr.SESE = this.GetDirectionLabelValue(this.SESELabel, sender);

                                #endregion

                                this.legalManagementData.GetLegalManagement.Rows.Add(dr);
                                tempKeyId = this.form25009Control.WorkItem.F25009_SaveLegalManagement(this.legalId, (Utility.GetXmlString(this.legalManagementData.GetLegalManagement.Copy())), TerraScanCommon.UserId);*/
                ////If updated, then update the value in the Grid too
                //if (this.keyId == tempKeyId)
                //{
                if ((string)senderLabel.Tag == "0")
                {
                    senderLabel.BackColor = Color.FromArgb(133, 164, 70);
                    senderLabel.ForeColor = Color.FromArgb(0, 0, 0);
                    senderLabel.Tag = "1";
                }
                else if ((string)senderLabel.Tag == "1")
                {
                    senderLabel.BackColor = Color.FromArgb(80, 120, 169);
                    senderLabel.ForeColor = Color.FromArgb(0, 0, 0);
                    senderLabel.Tag = "2";
                }
                else if ((string)senderLabel.Tag == "2")
                {
                    senderLabel.BackColor = Color.FromArgb(217, 217, 217);
                    senderLabel.ForeColor = Color.FromArgb(128, 128, 128);
                    senderLabel.Tag = "0";
                }
                //}
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "Change Events In Text Box/CheckBox"        
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To handle keypress event of the Textboxes, for not allowing the "Enter" Key
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ////khaja commented this code to work return key.
                ////switch (e.KeyChar)
                ////{
                ////    case (char)13:
                ////        {
                ////            e.Handled = true;
                ////            break;
                ////        }

                ////    case (char)10:
                ////        {
                ////            e.Handled = true;
                ////            break;
                ////        }
                ////}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of all ComboBox in the Legal Management Form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubdivisionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.LoadSectionTownshipRange();
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the SubdivisionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubdivisionComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
                ////khaja added code to fix Bug#4095
                this.AssembleLegalText();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the SubdivisionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubdivisionComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.New || this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    this.LoadSectionTownshipRange();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the SubdivisionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SubdivisionComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13 && (this.pageMode == TerraScanCommon.PageModeTypes.New || this.pageMode == TerraScanCommon.PageModeTypes.Edit))
                {
                    this.LoadSectionTownshipRange();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LotTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LotTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AssembleLegalText();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (((string.IsNullOrEmpty(this.SubdivisionComboBox.Text.Trim())) && (string.IsNullOrEmpty(this.LotTextBox.Text.Trim())) && (string.IsNullOrEmpty(this.BlockTextBox.Text.Trim()))) && (string.IsNullOrEmpty(this.LegalTextBox.Text.Trim())))
            {
                this.SubdivisionComboBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredFields");
                this.SubdivisionComboBox.Focus();
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Sets the max length for the editable textboxes and comboboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.LotTextBox.MaxLength = this.legalManagementData.GetLegalManagement.LotColumn.MaxLength;
            this.BlockTextBox.MaxLength = this.legalManagementData.GetLegalManagement.BlockColumn.MaxLength;
            this.AssembledLegalTextBox.MaxLength = this.legalManagementData.GetLegalManagement.AssembledLegalColumn.MaxLength;
            this.LegalTextBox.MaxLength = this.legalManagementData.GetLegalManagement.UserLegalColumn.MaxLength;
            this.SectionTextBox.MaxLength = this.legalManagementData.GetLegalManagement.SectionColumn.MaxLength;
            this.TownshipTextBox.MaxLength = this.legalManagementData.GetLegalManagement.TownShipColumn.MaxLength;
            this.RangeTextBox.MaxLength = this.legalManagementData.GetLegalManagement.RangeColumn.MaxLength;
            this.SubdivisionComboBox.MaxLength = this.legalManagementData.ListSubdivision.SubNameColumn.MaxLength;
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.SubdivisionComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Control Lock</param>
        private void ControlLock(bool controlLook)
        {
            this.LotTextBox.LockKeyPress = controlLook;
            this.BlockTextBox.LockKeyPress = controlLook;
            this.LegalTextBox.LockKeyPress = controlLook;

            this.LotPartialCheckBox.Enabled = !controlLook;
            this.BlockPartialCheckBox.Enabled = !controlLook;

            this.SubdivisionComboBox.Enabled = !controlLook;

            this.NWNWLabel.Enabled = !controlLook;
            this.NWNELabel.Enabled = !controlLook;
            this.NWSWLabel.Enabled = !controlLook;
            this.NWSELabel.Enabled = !controlLook;

            this.NENWLabel.Enabled = !controlLook;
            this.NENELabel.Enabled = !controlLook;
            this.NESWLabel.Enabled = !controlLook;
            this.NESELabel.Enabled = !controlLook;

            this.SWNWLabel.Enabled = !controlLook;
            this.SWNELabel.Enabled = !controlLook;
            this.SWSWLabel.Enabled = !controlLook;
            this.SWSELabel.Enabled = !controlLook;

            this.SENWLabel.Enabled = !controlLook;
            this.SENELabel.Enabled = !controlLook;
            this.SESWLabel.Enabled = !controlLook;
            this.SESELabel.Enabled = !controlLook;
        }

        /// <summary>
        /// To Enable/Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Lock Control</param>
        private void LockControls(bool lockControl)
        {
            this.SubdivisionPanel.Enabled = lockControl;
            this.LotPanel.Enabled = lockControl;
            this.LotPartialPanel.Enabled = lockControl;
            this.BlockPanel.Enabled = lockControl;
            this.BlockPartialPanel.Enabled = lockControl;
            this.DirectionPanel.Enabled = lockControl;
            this.AssembledLegalPanel.Enabled = lockControl;
            this.LegalPanel.Enabled = lockControl;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
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
        /// To enable the Edit button in nthe Master Form
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// To load all the values in Legal Management Form
        /// </summary>
        private void CustomizeLegalManagement()
        {
            this.legalManagementData = this.form25009Control.WorkItem.F25009_GetLegalManagement(this.keyId, TerraScanCommon.UserId);
            if (this.legalManagementData.GetLegalManagement.Rows.Count > 0)
            {
                string AssembledText;

                this.legalId = Convert.ToInt32(this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.LegalIDColumn]);

                this.LotTextBox.Text = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.LotColumn].ToString();
                this.BlockTextBox.Text = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.BlockColumn].ToString();
                AssembledText = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.AssembledLegalColumn].ToString();
                AssembledText = AssembledText.Trim().Replace("\r", " ");
                AssembledText = AssembledText.Trim().Replace("\n", " ");
                AssembledText = AssembledText.Trim().Replace("\t", " ");
                this.AssembledLegalTextBox.Text = AssembledText.Trim();

                //this.AssembledLegalTextBox.Text = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.AssembledLegalColumn].ToString();
                this.LegalTextBox.Text = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.UserLegalColumn].ToString();

                this.SectionTextBox.Text = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SectionColumn].ToString();
                this.TownshipTextBox.Text = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.TownShipColumn].ToString();
                this.RangeTextBox.Text = this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.RangeColumn].ToString();

                this.LotPartialCheckBox.Checked = Convert.ToBoolean(this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.IsPartialLotColumn].ToString());
                this.BlockPartialCheckBox.Checked = Convert.ToBoolean(this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.IsPartialBlockColumn].ToString());

                this.subDivisionId = Convert.ToInt32(this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SubdivisionIDColumn]);

                ////For Direction Labels
                this.SetDirectionLabels(this.NWNWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NWNWColumn].ToString());
                this.SetDirectionLabels(this.NWNELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NWNEColumn].ToString());
                this.SetDirectionLabels(this.NWSWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NWSWColumn].ToString());
                this.SetDirectionLabels(this.NWSELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NWSEColumn].ToString());

                this.SetDirectionLabels(this.NENWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NENWColumn].ToString());
                this.SetDirectionLabels(this.NENELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NENEColumn].ToString());
                this.SetDirectionLabels(this.NESWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NESWColumn].ToString());
                this.SetDirectionLabels(this.NESELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.NESEColumn].ToString());

                this.SetDirectionLabels(this.SWNWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SWNWColumn].ToString());
                this.SetDirectionLabels(this.SWNELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SWNEColumn].ToString());
                this.SetDirectionLabels(this.SWSWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SWSWColumn].ToString());
                this.SetDirectionLabels(this.SWSELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SWSEColumn].ToString());

                this.SetDirectionLabels(this.SENWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SENWColumn].ToString());
                this.SetDirectionLabels(this.SENELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SENEColumn].ToString());
                this.SetDirectionLabels(this.SESWLabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SESWColumn].ToString());
                this.SetDirectionLabels(this.SESELabel, this.legalManagementData.GetLegalManagement.Rows[0][this.legalManagementData.GetLegalManagement.SESEColumn].ToString());

                this.LockControls(true);
                this.comboFocusFlag = true;
            }
            else
            {
                this.ClearLegalManagement();
                this.LockControls(false);
            }

            ////To Load The ComboBox
            this.LoadComboBox();
        }

        /// <summary>
        /// Load the Subdivision Combobox
        /// </summary>
        private void LoadComboBox()
        {
            F25009LegalManagementData.ListSubdivisionDataTable listSubDivision = new F25009LegalManagementData.ListSubdivisionDataTable();

            DataRow customRow = listSubDivision.NewRow();
            listSubDivision.Clear();
            customRow[this.legalManagementData.ListSubdivision.SubNameColumn.ColumnName] = string.Empty;
            customRow[this.legalManagementData.ListSubdivision.SubdivisionIDColumn.ColumnName] = "0";
            customRow[this.legalManagementData.ListSubdivision.SectionColumn.ColumnName] = string.Empty;
            customRow[this.legalManagementData.ListSubdivision.TownshipColumn.ColumnName] = string.Empty;
            customRow[this.legalManagementData.ListSubdivision.RangeColumn.ColumnName] = string.Empty;
            listSubDivision.Rows.Add(customRow);

            this.legalManagementData = this.form25009Control.WorkItem.F25009_ListSubdivision();
            listSubDivision.Merge(this.legalManagementData.ListSubdivision);

            if (this.legalManagementData.ListSubdivision.Rows.Count > 0)
            {
                this.SubdivisionComboBox.DataSource = listSubDivision;
                this.SubdivisionComboBox.DisplayMember = this.legalManagementData.ListSubdivision.SubNameColumn.ColumnName;
                this.SubdivisionComboBox.ValueMember = this.legalManagementData.ListSubdivision.SubdivisionIDColumn.ColumnName;
                if (this.subDivisionId > 0)
                {
                    this.SubdivisionComboBox.SelectedValue = this.subDivisionId;
                }
                else
                {
                    this.SubdivisionComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// To load the Section, Township and Range Textboxes when the Subdivision Combobox selection changes
        /// </summary>
        private void LoadSectionTownshipRange()
        {
            int subdivisionComboID;

            F25009LegalManagementData.ListSubdivisionDataTable listSubDivision = new F25009LegalManagementData.ListSubdivisionDataTable();

            DataRow customRow = listSubDivision.NewRow();
            listSubDivision.Clear();
            customRow[this.legalManagementData.ListSubdivision.SubNameColumn.ColumnName] = string.Empty;
            customRow[this.legalManagementData.ListSubdivision.SubdivisionIDColumn.ColumnName] = "0";
            customRow[this.legalManagementData.ListSubdivision.SectionColumn.ColumnName] = string.Empty;
            customRow[this.legalManagementData.ListSubdivision.TownshipColumn.ColumnName] = string.Empty;
            customRow[this.legalManagementData.ListSubdivision.RangeColumn.ColumnName] = string.Empty;
            listSubDivision.Rows.Add(customRow);

            this.legalManagementData = this.form25009Control.WorkItem.F25009_ListSubdivision();
            listSubDivision.Merge(this.legalManagementData.ListSubdivision);

            if (this.legalManagementData.ListSubdivision.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.SubdivisionComboBox.Text.Trim()))
                {
                    if (this.SubdivisionComboBox.SelectedValue != null)
                    {
                        int.TryParse(this.SubdivisionComboBox.SelectedValue.ToString(), out subdivisionComboID);
                        this.SectionTextBox.Text = listSubDivision.FindBySubdivisionID(subdivisionComboID).Section.ToString();
                        this.TownshipTextBox.Text = listSubDivision.FindBySubdivisionID(subdivisionComboID).Township.ToString();
                        this.RangeTextBox.Text = listSubDivision.FindBySubdivisionID(subdivisionComboID).Range.ToString();
                    }
                    else
                    {
                        string findCondtion = "SubName = '" + this.SubdivisionComboBox.Text.Trim() + "' AND SubdivisionID > 0"; // + SubdivisionID > 0";

                        DataRow[] dr = listSubDivision.Select(findCondtion);

                        if (dr.Length > 0)
                        {
                            int.TryParse(dr[0]["SubdivisionID"].ToString(), out subdivisionComboID);
                            this.SectionTextBox.Text = listSubDivision.FindBySubdivisionID(subdivisionComboID).Section.ToString();
                            this.TownshipTextBox.Text = listSubDivision.FindBySubdivisionID(subdivisionComboID).Township.ToString();
                            this.RangeTextBox.Text = listSubDivision.FindBySubdivisionID(subdivisionComboID).Range.ToString();
                        }
                        else
                        {
                            this.SectionTextBox.Text = string.Empty;
                            this.TownshipTextBox.Text = string.Empty;
                            this.RangeTextBox.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    this.SectionTextBox.Text = string.Empty;
                    this.TownshipTextBox.Text = string.Empty;
                    this.RangeTextBox.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// To Set the Direction Label's State
        /// </summary>
        /// <param name="sender">The Direction Label Control</param>
        /// <param name="value">The value to set</param>
        private void SetDirectionLabels(object sender, string value)
        {
            Label senderLabel = (Label)sender;
            senderLabel.Tag = value;
            if (value == "0")
            {
                senderLabel.BackColor = Color.FromArgb(217, 217, 217);
                senderLabel.ForeColor = Color.FromArgb(128, 128, 128);
            }
            else if (value == "1")
            {
                senderLabel.BackColor = Color.FromArgb(133, 164, 70);
                senderLabel.ForeColor = Color.FromArgb(0, 0, 0);
            }
            else if (value == "2")
            {
                senderLabel.BackColor = Color.FromArgb(80, 120, 169);
                senderLabel.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        /// <summary>
        /// To Clear the entire Legal Management Form
        /// </summary>
        private void ClearLegalManagement()
        {
            this.ClearComboBox(this);
            this.ClearLegalManagementControls();
            this.ClearDirectionLabels();
        }

        /// <summary>
        /// Clears the All the combo boxs in the form.
        /// </summary>
        /// <param name="currentControl">The current control.</param>
        private void ClearComboBox(Control currentControl)
        {
            if (currentControl.HasChildren)
            {
                foreach (Control childControl in currentControl.Controls)
                {
                    this.ClearComboBox(childControl);
                }
            }
            else
            {
                if (currentControl.GetType() == typeof(TerraScanComboBox))
                {
                    TerraScanComboBox currentComboBox = (TerraScanComboBox)currentControl;
                    currentComboBox.DataSource = null;
                    currentComboBox.Items.Clear();
                    currentComboBox.Refresh();
                }
            }
        }

        /// <summary>
        /// To Clear Legal Management Textboxes and Checkboxes
        /// </summary>
        private void ClearLegalManagementControls()
        {
            this.LotTextBox.Text = string.Empty;
            this.BlockTextBox.Text = string.Empty;
            this.AssembledLegalTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.SectionTextBox.Text = string.Empty;
            this.TownshipTextBox.Text = string.Empty;
            this.RangeTextBox.Text = string.Empty;
            this.LotPartialCheckBox.Checked = false;
            this.BlockPartialCheckBox.Checked = false;
        }

        /// <summary>
        /// To Clear Direction Labels
        /// </summary>
        private void ClearDirectionLabels()
        {
            this.ResetDirectionLabels(this.NWNWLabel);
            this.ResetDirectionLabels(this.NWNELabel);
            this.ResetDirectionLabels(this.NWSWLabel);
            this.ResetDirectionLabels(this.NWSELabel);

            this.ResetDirectionLabels(this.NENWLabel);
            this.ResetDirectionLabels(this.NENELabel);
            this.ResetDirectionLabels(this.NESWLabel);
            this.ResetDirectionLabels(this.NESELabel);

            this.ResetDirectionLabels(this.SWNWLabel);
            this.ResetDirectionLabels(this.SWNELabel);
            this.ResetDirectionLabels(this.SWSWLabel);
            this.ResetDirectionLabels(this.SWSELabel);

            this.ResetDirectionLabels(this.SENWLabel);
            this.ResetDirectionLabels(this.SENELabel);
            this.ResetDirectionLabels(this.SESWLabel);
            this.ResetDirectionLabels(this.SESELabel);
        }

        /// <summary>
        /// To reset the Direction Label to its original state
        /// </summary>
        /// <param name="sender">The Direction Label Control</param>
        private void ResetDirectionLabels(object sender)
        {
            Label senderLabel = (Label)sender;
            senderLabel.Tag = "0";
            senderLabel.BackColor = Color.FromArgb(217, 217, 217);
            senderLabel.ForeColor = Color.FromArgb(128, 128, 128);
        }

        ///<summary>
        ///To save all the values in Legal Management Form
        ///</summary>
        private void SaveLegalManagement()
        {
            bool isFuturePush = false;
            DialogResult dialogResult;
            dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("LegalPushBeforeSave")), "TerraScan T2 – Push Legal to Future Years", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                isFuturePush = true;
            }
            else
            {
                isFuturePush = false;
            }
            this.legalManagementData.GetLegalManagement.Rows.Clear();
            F25009LegalManagementData.GetLegalManagementRow dr = this.legalManagementData.GetLegalManagement.NewGetLegalManagementRow();

            dr.ParcelID = this.keyId;
            dr.LegalID = this.legalId;
            dr.SubName = this.SubdivisionComboBox.Text.Trim();

            #region String Textboxes

            if (!string.IsNullOrEmpty(this.LotTextBox.Text.Trim()))
            {
                dr.Lot = this.LotTextBox.Text.Trim();
            }
            else
            {
                dr.Lot = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.BlockTextBox.Text.Trim()))
            {
                dr.Block = this.BlockTextBox.Text.Trim();
            }
            else
            {
                dr.Block = string.Empty;
            }

            ////khaja added code to fix bug#5385
            string AssembledText = this.AssembledLegalTextBox.Text.Trim().Replace("\r", " ");
            AssembledText = this.AssembledLegalTextBox.Text.Trim().Replace("\n", " ");
            AssembledText = this.AssembledLegalTextBox.Text.Trim().Replace("\t", " ");
            if (AssembledText.Trim().Length > 250)
            {
                //dr.AssembledLegal = this.AssembledLegalTextBox.Text.Trim().Substring(0, 250);
                dr.AssembledLegal = AssembledText.Trim().Substring(0, 250);
            }
            else if (!string.IsNullOrEmpty(this.AssembledLegalTextBox.Text.Trim()))
            {
                dr.AssembledLegal = AssembledText.Trim();    //this.AssembledLegalTextBox.Text.Trim();
            }
            else
            {
                dr.AssembledLegal = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.LegalTextBox.Text.Trim()))
            {
                dr.UserLegal = this.LegalTextBox.Text.Trim();
            }
            else
            {
                dr.UserLegal = string.Empty;
            }

            #endregion

            #region Checkboxes

            dr.IsPartialLot = this.LotPartialCheckBox.Checked;
            dr.IsPartialBlock = this.BlockPartialCheckBox.Checked;

            #endregion

            #region ComboBoxes

            if (!string.IsNullOrEmpty(this.SubdivisionComboBox.Text.Trim()))
            {
                dr.SubdivisionID = Convert.ToInt32(this.SubdivisionComboBox.SelectedValue);
            }
            else
            {
                dr.SubdivisionID = 0;
            }

            #endregion

            #region setDirectionLabels
            dr.NWNW = this.nwnw;
            dr.NWNE = this.nwne;
            dr.NWSW = this.nwsw;
            dr.NWSE = this.nwse;

            dr.NENW = this.nenw;
            dr.NENE = this.nene;
            dr.NESW = this.nesw;
            dr.NESE = this.nese;

            dr.SWNW = this.swnw;
            dr.SWNE = this.swne;
            dr.SWSW = this.swsw;
            dr.SWSE = this.swse;

            dr.SENW = this.senw;
            dr.SENE = this.sene;
            dr.SESW = this.sesw;
            dr.SESE = this.sese;

            #endregion setDirectionLabels

            this.legalManagementData.GetLegalManagement.Rows.Add(dr);
            this.keyId = this.form25009Control.WorkItem.F25009_SaveLegalManagement(this.legalId, (Utility.GetXmlString(this.legalManagementData.GetLegalManagement.Copy())), isFuturePush, TerraScanCommon.UserId);


        }

        /// <summary>
        /// Returns the Direction Label Value to be stored
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="source">The Direction Label Clicked</param>
        /// <returns>Returns the current value of the Direction Label (0,1,2)</returns>
        private Int16 GetDirectionLabelValue(object sender, object source)
        {
            Int16 labelTag;
            Label senderLabel = (Label)sender;
            Label sourceLabel = (Label)source;

            if (senderLabel.Name != sourceLabel.Name)
            {
                Int16.TryParse(senderLabel.Tag.ToString(), out labelTag);
                return labelTag;
            }
            else
            {
                ////Current Value is 2
                if ((string)senderLabel.Tag == "2")
                {
                    return 0;
                }
                ////Current Value is 0
                else if ((string)senderLabel.Tag == "0")
                {
                    return 1;
                }
                ////Current Value is 1
                else
                {
                    return 2;
                }
            }
        }

        ////khaja added code to fix Bug#4095

        /// <summary>
        /// Assembles the legal text.
        /// </summary>
        private void AssembleLegalText()
        {
            string subDivisionText;
            string lotText;
            string blockText;

            if (!string.IsNullOrEmpty(this.SubdivisionComboBox.Text.Trim()))
            {
                subDivisionText = this.SubdivisionComboBox.Text.Trim();
            }
            else
            {
                subDivisionText = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.LotTextBox.Text.Trim()))
            {
                lotText = " Lot " + this.LotTextBox.Text.Trim();
            }
            else
            {
                lotText = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.BlockTextBox.Text.Trim()))
            {
                blockText = " Block " + this.BlockTextBox.Text.Trim();
            }
            else
            {
                blockText = string.Empty;
            }

            if ((!string.IsNullOrEmpty(subDivisionText)) && ((!string.IsNullOrEmpty(lotText)) || (!string.IsNullOrEmpty(blockText))))
            {
                subDivisionText = subDivisionText + ",";
            }
            else if ((!string.IsNullOrEmpty(subDivisionText)) && (!string.IsNullOrEmpty(lotText)) && (!string.IsNullOrEmpty(blockText)))
            {
                subDivisionText = subDivisionText;
            }

            if ((!string.IsNullOrEmpty(lotText)) && (!string.IsNullOrEmpty(blockText)))
            {
                lotText = lotText + ",";
            }

            this.AssembledLegalTextBox.Text = subDivisionText + lotText + blockText;

            if (!string.IsNullOrEmpty(this.LegalTextBox.Text.Trim()) && string.IsNullOrEmpty(this.AssembledLegalTextBox.Text.Trim()))
            {
                ////if (string.IsNullOrEmpty(this.SubdivisionComboBox.Text.Trim()) && string.IsNullOrEmpty(this.LotTextBox.Text.Trim()) && string.IsNullOrEmpty(this.BlockTextBox.Text.Trim()))
                ////{
                string LegalText = this.LegalTextBox.Text.Trim().Replace("\r", " ");
                LegalText = LegalText.Trim().Replace("\n", " ");
                LegalText = LegalText.Trim().Replace("\t", " ");
                int stringLength = LegalText.Trim().Length; //this.LegalTextBox.Text.Trim().Length;

                if (stringLength > 250)
                {
                    this.AssembledLegalTextBox.Text = LegalText.Trim().Substring(0, 250);  //this.LegalTextBox.Text.Trim().Substring(0, 250);

                }
                else
                {
                    this.AssembledLegalTextBox.Text = LegalText.Trim(); //this.LegalTextBox.Text.Trim();
                }
                ////}
            }
        }

        #endregion
    }
}
