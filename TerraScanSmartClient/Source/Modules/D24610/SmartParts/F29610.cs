//--------------------------------------------------------------------------------------------
// <copyright file="F29610.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36032 FS Land Codes.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18/4/2008        Malliga             Created
//***********************************************************************************************/
namespace D24610
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
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;
   
    /// <summary>
    /// 29610
    /// </summary>
    [SmartPart]
    public partial class F29610 : BaseSmartPart
    {
        #region Variables

        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        /// <summary>
        /// Usede to store the form slice key id
        /// </summary>
        private int eventId;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// controller F36041
        /// </summary>
        private F29610Controller form29610Control;

        /// <summary>
        /// Unique ownerID 
        /// </summary>
        private int ownerId;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private PartiesOwnerDetailsData ownerDetailDataSet = new PartiesOwnerDetailsData();

        /// <summary>
        /// HoHExemptionDetailDataSetData
        /// </summary>
        private F29610HoHExemptionData HoHExemptionDetailDataSet = new F29610HoHExemptionData();

        int Hohownerid = 0;

        int scheduleid = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:26041"/> class.
        /// </summary>
        public F29610()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29610"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29610(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.eventId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.HoHpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.HoHpictureBox.Height, this.HoHpictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form29610 control.
        /// </summary>
        /// <value>The form29610 control.</value>
        [CreateNew]
        public F29610Controller Form29610Control
        {
            get { return this.form29610Control as F29610Controller; }
            set { this.form29610Control = value; }
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
            if (this.slicePermissionField.newPermission)
            {
                this.GetHoHEemptionDetails(this.eventId); 
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControl();
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
            this.ClearControl();
            this.GetHoHEemptionDetails(this.eventId); 
           this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    //this.Cursor = Cursors.WaitCursor;
                    string HoHItems = string.Empty;
                    F29610HoHExemptionData.GetHoHExemptionDetailsDataTable currentTable = new F29610HoHExemptionData.GetHoHExemptionDetailsDataTable();
                    F29610HoHExemptionData.GetHoHExemptionDetailsRow currenRow = currentTable.NewGetHoHExemptionDetailsRow();
                    currenRow.OwnerID = this.Hohownerid;
                    currenRow.EventID = this.eventId;
                    currenRow.IsQualified = Convert.ToBoolean(this.QualifiedComboBox.SelectedValue);
                    currenRow.EffectiveDate = this.EffectiveDateTextBox.Text.Trim();
                    currenRow.ThroughDate = this.ThroughDateTextBox.Text.Trim();
                    string reductionofvalue = this.ReductionofvalueTextBox.Text.Replace("$", "").Replace(",", "").Trim();
                    currenRow.ReductionValue = Convert.ToInt32(reductionofvalue);
                    string resultingtaxablevalue = this.ResultingTaxableValueTextBox.Text.Replace("$", "").Replace(",", "").Trim();
                    currenRow.ResultingTaxable = Convert.ToInt32(resultingtaxablevalue);
                    currentTable.Rows.Add(currenRow);
                    HoHItems = TerraScanCommon.GetXmlString(currentTable);
                    int returnValue = this.form29610Control.WorkItem.F29610_SaveHoHExemptionDetails(this.eventId, HoHItems, TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
            }
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
                //this.ClearControl();
                this.eventId = eventArgs.Data.SelectedKeyId;
                this.GetHoHEemptionDetails(this.eventId); 
               this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        #endregion Event Subscription

        #region Methods

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

        /// <summary>
        /// Clears the control.
        /// </summary>
        private void ClearControl()
        {
            this.OwnerAppplyingTextBox.Text = string.Empty;
            this.OwnerAddressTextBox.Text = string.Empty;
            this.CityStateZipTextBox.Text = string.Empty;
            this.QualifiedComboBox.SelectedIndex = 0;
            this.EffectiveDateTextBox.Text = string.Empty;
            this.ThroughDateTextBox.Text = string.Empty;
            this.OwnerPercentTextBox.Text = string.Empty;
            this.ResultingOwnerValueTextBox.Text = string.Empty;
            this.ReductionofvalueTextBox.Text = string.Empty;
            this.ResultingTaxableValueTextBox.Text = string.Empty;
        }

        private void GetHoHEemptionDetails(int eventId)
        {
            this.HoHExemptionDetailDataSet = this.form29610Control.WorkItem.F29610_GetHoHExemptionDetails(eventId);
            if (this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows.Count > 0)
            {
                string hohowner;
                
                hohowner = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.OwnerIDColumn].ToString();
                if(!string.IsNullOrEmpty(hohowner))
                {
                Hohownerid = Convert.ToInt32(hohowner); 
                }

                this.OwnerAppplyingTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.OwnerNameColumn].ToString();
                this.OwnerAddressTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.AddressColumn].ToString();
                this.CityStateZipTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.StateDetailColumn].ToString();
                string qualifiedcombo = string.Empty;
                qualifiedcombo = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.IsQualifiedColumn].ToString();
                if (qualifiedcombo == "False")
                {
                    this.QualifiedComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.QualifiedComboBox.SelectedIndex  = 0;
                }
                this.EffectiveDateTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.EffectiveDateColumn].ToString();
                this.ThroughDateTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.ThroughDateColumn].ToString();
                this.OwnerPercentTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.OwnerPercentColumn].ToString();
                this.ResultingOwnerValueTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.O1ValueColumn].ToString();
                scheduleid = Convert.ToInt32(this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.ScheduleIDColumn].ToString());
                int exemptionid = Convert.ToInt32(this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.ExemptionIDColumn].ToString());
                this.ResultingTaxableValueTextBox.Text = this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.Rows[0][this.HoHExemptionDetailDataSet.GetHoHExemptionDetails.ResultingTaxableColumn].ToString();
                this.HoHExemptionDetailDataSet = this.form29610Control.WorkItem.F29610_GetCalculationOfHoH(scheduleid, exemptionid);
                if (this.QualifiedComboBox.SelectedIndex == 0)
                {
                    this.ReductionofvalueTextBox.Text = this.HoHExemptionDetailDataSet.GetCalculationHeadOfHousehold.Rows[0][this.HoHExemptionDetailDataSet.GetCalculationHeadOfHousehold.ReductionValueColumn].ToString();
                }
                else
                {
                    this.ReductionofvalueTextBox.Text = string.Empty;
                }
                this.HoHExemptionDetailDataSet = this.form29610Control.WorkItem.F29610_GetOwnerPercent(Hohownerid, scheduleid);
                if (this.HoHExemptionDetailDataSet.GetOwnerPercent.Rows.Count > 0)
                {
                    this.OwnerPercentTextBox.Text = this.HoHExemptionDetailDataSet.GetOwnerPercent.Rows[0][this.HoHExemptionDetailDataSet.GetOwnerPercent.OwnerPercentColumn].ToString();
                }
            }
            else
            {
                this.ClearControl();
            }
        }
        
        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            string validationmessage = string.Empty; 
            ////Check the Required Fields
            if (string.IsNullOrEmpty(this.OwnerAppplyingTextBox.Text) || string.IsNullOrEmpty(this.EffectiveDateTextBox.Text) || string.IsNullOrEmpty(this.ThroughDateTextBox.Text) || string.IsNullOrEmpty(this.ReductionofvalueTextBox.Text) || string.IsNullOrEmpty(this.ResultingTaxableValueTextBox.Text))
            {
                //sliceValidationFields.DisableNewMethod = false;
                //sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                //MessageBox.Show("You cannot save this Head of Household Exemption because it is missing required fields.", "Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                validationmessage = "You cannot save this Head of Household Exemption because it is missing required fields.";
                sliceValidationFields.ErrorMessage = validationmessage;
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = true;
                this.OwnerAppplyingTextBox.Focus();
                return sliceValidationFields;
             }
            return sliceValidationFields;
         }

         /// <summary>
         /// Inits the qualified combo.
         /// </summary>
        private void InitQualifiedCombo()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.QualifiedComboBox.DataSource = commonData.ComboBoxDataTable;
            this.QualifiedComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.QualifiedComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
            this.QualifiedComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Validates the date.
        /// </summary>
        /// <returns>boolean</returns>
        private bool ValidateDate()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.EffectiveDateTextBox.Text.Trim()))
                {
                    DateTime.Parse(this.EffectiveDateTextBox.Text.Trim());
                }

                if (!string.IsNullOrEmpty(this.ThroughDateTextBox.Text.Trim()))
                {
                    DateTime.Parse(this.ThroughDateTextBox.Text.Trim());
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region PictureBox Events
        /// <summary>
        /// Handles the Click event of the HoHpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HoHpictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the HoHpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HoHpictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.HoHExemptionToolTip.SetToolTip(this.HoHpictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F29610 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29610_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.InitQualifiedCombo();
                this.GetHoHEemptionDetails(this.eventId);
                this.OwnerAppplyingTextBox.LockKeyPress  = true;
                this.OwnerAppplyingTextBox.Focus(); 
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
        #endregion

        #region Owner Applying PictureBox Events
        /// <summary>
        /// Handles the Click event of the OwnerApplyingPicture control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerApplyingPicture_Click(object sender, EventArgs e)
        {
            try
            {
                Form ownerIdForm = new Form();
                ownerIdForm = this.form29610Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null , this.form29610Control.WorkItem);
                if (ownerIdForm != null)
                {
                    if (ownerIdForm.ShowDialog() == DialogResult.Yes)
                    {
                        this.Hohownerid = Convert.ToInt32(TerraScanCommon.GetValue(ownerIdForm, "MasterNameOwnerId"));
                        this.SetEditRecord();
                        this.ownerDetailDataSet = this.form29610Control.WorkItem.GetOwnerDetails(this.Hohownerid);
                        this.OwnerAppplyingTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                        this.OwnerAppplyingTextBox.ForeColor = Color.Black; 
                        this.OwnerAddressTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString() + "," + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                        this.CityStateZipTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString() + "," +  this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString() + "," + this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.ZipColumn].ToString();
                        //this.Hohownerid = this.ownerId;
                        this.HoHExemptionDetailDataSet = this.form29610Control.WorkItem.F29610_GetOwnerPercent(Hohownerid, scheduleid);
                        if (this.HoHExemptionDetailDataSet.GetOwnerPercent.Rows.Count > 0)
                        {
                            this.OwnerPercentTextBox.Text = this.HoHExemptionDetailDataSet.GetOwnerPercent.Rows[0][this.HoHExemptionDetailDataSet.GetOwnerPercent.OwnerPercentColumn].ToString();
                        }
                    }
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
        /// Handles the TextChanged event of the OwnerAppplyingTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerAppplyingTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.ValidateDate())
            {
                this.SetEditRecord();
            }
        }

        #endregion

        #region Calendar Events
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

                SendKeys.Send("{F4}");
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the EffectiveDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EffectiveDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.EffectiveDateTextBox.Text = this.EffectiveDateTimePicker.Text;
                this.EffectiveDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the EffectiveDateCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EffectiveDateCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.EffectiveDateTextBox, this.EffectiveDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ThroughDateCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ThroughDateCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                this.TimerImage_Click(this.ThroughDateTextBox, this.ThroughDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the ThroughDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ThroughDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.ThroughDateTextBox.Text = this.ThroughDateTimePicker.Text;
                this.ThroughDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}
