// ----------------------------------------------------------------------------------
// <copyright file="F25055.cs" company="Congruent">
//      Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the F25055.
// </summary>
// ----------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author             Description
// ----------       ---------          ---------------------------------------------
// 03/08/2009     D.LathaMaheswari     Created
// *********************************************************************************/
namespace D25055
{
    using System;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// F25055 class
    /// </summary>
    [SmartPart]
    public partial class F25055 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Stores Schedule ID
        /// </summary>
        private int scheduleId;

        /// <summary>
        /// Stores District ID
        /// </summary>
        private int districtId;

        /// <summary>
        /// Stores Owner ID
        /// </summary>
        private int ownerId;

        /// <summary>
        /// Stores Parcel ID
        /// </summary>
        private int parcelId;

        /// <summary>
        /// controller for the current view
        /// </summary>
        private F25055Controller form25055Control;

        /// <summary>
        /// stores the personal property DataTable
        /// </summary>
        private F25055PropertyHeaderData personalPropertyData = new F25055PropertyHeaderData();

        /// <summary>
        /// Master Form Number
        /// </summary>
        private int masterFormNo;
        
        #endregion 

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="F25055"/> class.
        /// </summary>
        public F25055()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F25055"/> class.
        /// </summary>
        /// <param name="masterForm">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyId">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F25055(int masterForm, int formNo, int keyId, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.masterFormNo = masterForm;
            this.Tag = formNo;
            this.scheduleId = keyId;
            this.PropertyHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PropertyHeaderPictureBox.Height, this.PropertyHeaderPictureBox.Width, tabText, red, green, blue);
        }
        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_KeyIdAlertSlice, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9030_KeyIdAlertSlice;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Occurs when [D9030_ F9030_ reload after delete].
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterDelete, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterDelete;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form25055 control.
        /// </summary>
        /// <value>The form25055 control.</value>
        [CreateNew]
        public F25055Controller Form25055Control
        {
            get { return this.form25055Control as F25055Controller; }
            set { this.form25055Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Loads the slice details.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SliceReloadActiveRecord&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> e)
        {
            if (e.Data.MasterFormNo == this.masterFormNo)
            {
                // Get Keyid 
                this.scheduleId = e.Data.SelectedKeyId;

                // Load personal property header for specific keyid
                this.GetPropertyHeaderDetails(this.scheduleId);
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    if (this.personalPropertyData.GetPersonalPropertyDetail.Rows.Count > 0)
                    {
                        // Set the flag value as valid slice key
                        eventArgs.Data.FlagInvalidSliceKey = false;
                       
                        // Enable all controls 
                        this.EnableScheduleHeader(true);
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            // Set the flag as invalid key
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }

                        // Disable controls for invalid keyid
                        this.EnableScheduleHeader(false);
                    }
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

        #region ProtectedMethods

        /// <summary>
        /// Called when [D9030_ F9033_ query engine close].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_KeyIdAlertSlice(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9030_KeyIdAlertSlice != null)
            {
                this.D9030_F9030_KeyIdAlertSlice(this, eventArgs);
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

        #endregion

        #endregion Event Subscription

        #region Events

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F25050 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void F25055_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;

                if (this.scheduleId > 0)
                {
                    // Load Personal property header for specific keyid
                    this.GetPropertyHeaderDetails(this.scheduleId);

                    // Set focus on first editable field
                    this.ScheduleNumberLinkLabel.Focus();
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

        #endregion Form Load

        #region Link Events
    
        /// <summary>
        /// Handles the LinkClicked event of the ScheduleNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ScheduleNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.scheduleId > 0)
                {
                    object[] optionalParameter;
                    optionalParameter = new object[2];
                    optionalParameter[0] = this.masterFormNo;
                    optionalParameter[1] = this.scheduleId;
                    Form scheduleForm = new Form();
                    scheduleForm = this.form25055Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2200, optionalParameter, this.form25055Control.WorkItem);
                    if (scheduleForm != null)
                    {
                        if (scheduleForm.ShowDialog() == DialogResult.OK)
                        {
                            int formScheduleID;
                            int.TryParse(TerraScanCommon.GetValue(scheduleForm, SharedFunctions.GetResourceString("F25050ScheduleProperty")).ToString(), out formScheduleID);
                            SliceReloadActiveRecord currentSliceInfo;
                            currentSliceInfo.MasterFormNo = this.masterFormNo;
                            currentSliceInfo.SelectedKeyId = formScheduleID;

                             int isRecordDeleted;
                            int.TryParse(TerraScanCommon.GetValue(scheduleForm, SharedFunctions.GetResourceString("F25050DeleteProperty")).ToString(), out isRecordDeleted);

                            if (isRecordDeleted > 0)
                            {
                                this.D9030_F9030_ReloadAfterDelete(this, new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                            }
                            else
                            {
                                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));

                                // Refresh master form with the returned keyid
                                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                                this.GetPropertyHeaderDetails(formScheduleID);
                                this.scheduleId = formScheduleID;
                            }

                            //if (isDeleted > 0)
                            //{
                            //    this.OnD9030_F9030_KeyIdAlertSlice(new TerraScan.Infrastructure.Interface.EventArgs<int>(this.masterFormNo));
                            //}
                            //else
                            //{
                            //    this.GetPropertyHeaderDetails(formScheduleID);
                            //    this.scheduleId = formScheduleID;
                            //}
                        }
                    }
                }

                this.ActiveControl = this.ScheduleNumberLinkLabel;
                this.ActiveControl.Focus();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the EventslinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void EventslinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.ShowSubForm(24001, this.scheduleId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
         }

        /// <summary>
        /// Handles the LinkClicked event of the PrimaryOwnerlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void PrimaryOwnerlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.ShowSubForm(91000, this.ownerId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ParcelReferencelinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ParcelReferencelinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.ShowSubForm(30000, this.parcelId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the DistrictlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void DistrictlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Open F11002 District form
                this.ShowSubForm(11002, this.districtId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Link Events

        #region PicturBox Events

        /// <summary>
        /// Handles the MouseHover event of the PropertyHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PropertyHeaderPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                // Set tooltip for section indicator
                this.PropertySliceToolTip.SetToolTip(this.PropertyHeaderPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PropertyHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PropertyHeaderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Expand/Collapse formslice
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion PicturBox Events

        #endregion Events

        #region PrivateMethods

        #region Enable/Disable controls

        /// <summary>
        /// Enable/Disable controls based on the isEnable value
        /// </summary>
        /// <param name="isEnable">if set to <c>true</c> [is enable].</param>
        private void EnableScheduleHeader(bool isEnable)
        {
            this.ScheduleNumberPanel.Enabled = isEnable;
            this.RollYearPanel.Enabled = isEnable;
            this.SourcePanel.Enabled = isEnable;
            this.RealPropTaxablePanel.Enabled = isEnable;
            this.ReviewPanel.Enabled = isEnable;
            this.AssessmentTypePanel.Enabled = isEnable;
            this.Exemptpanel.Enabled = isEnable;
            this.ScheduleDORpanel.Enabled = isEnable;
            this.FairCashPanel.Enabled = isEnable;
            this.CappedValuePanel.Enabled = isEnable;
            this.ScheduleTaxablePanel.Enabled = isEnable;
            this.MID1panel.Enabled = isEnable;
            this.MID2panel.Enabled = isEnable;
            this.MID3panel.Enabled = isEnable;
            this.MID4panel.Enabled = isEnable;
            this.MID5panel.Enabled = isEnable;
            this.PropertyTypePanel.Enabled = isEnable;
            this.PropertyTypePanel.Enabled = isEnable;
            this.ScheduleTypePanel.Enabled = isEnable;
            this.EventsPanel.Enabled = isEnable;
            this.EventslinkLabel.Visible = isEnable;
            this.DescriptionPanel.Enabled = isEnable;
            this.PrimaryOwnerPanel.Enabled = isEnable;
            this.StreetAddressPanel.Enabled = isEnable;
            this.ParcelReferencePanel.Enabled = isEnable;
            this.RealPropertyDORPanel.Enabled = isEnable;
            this.NAICSPanel.Enabled = isEnable;
            this.LegalPanel.Enabled = isEnable;
            this.MapNumberPanel.Enabled = isEnable;
            this.PenaltyAmountPanel.Enabled = isEnable;
            this.PenaltyPercentPanel.Enabled = isEnable;
            this.AssessedValuePanel.Enabled = isEnable;
            this.ResultingTaxablepanel.Enabled = isEnable;
            this.DistrictPanel.Enabled = isEnable;
            this.BusinessNamePanel.Enabled = isEnable;
            //this.PropertyHeaderPictureBox.Enabled = isEnable;
            this.FilingDatePanel.Enabled = isEnable;
            this.NewConstructionPanel.Enabled = isEnable;
        }

        #endregion Enable/Disable controls

        #region Clear Controls

        /// <summary>
        /// Clear all the controls.
        /// </summary>
        private void ClearControls()
        {
            this.ScheduleNumberLinkLabel.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.SourceTextBox.Text = string.Empty;
            this.ReviewTextBox.Text = string.Empty;
            this.PropertyTypeTextBox.Text = string.Empty;
            this.ScheduleTypeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.PrimaryOwnerlinkLabel.Text = string.Empty;
            this.StreetAddressTextBox.Text = string.Empty;
            this.ParcelReferencelinkLabel.Text = string.Empty;
            this.RealPropertyDORTextBox.Text = string.Empty;
            this.NAICSTextBox.Text = string.Empty;
            this.FilingDateTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.RealPropertyMapNumberTextBox.Text = string.Empty;
            this.PenaltyAmountTextBox.Text = string.Empty;
            this.PenaltyPercentTextBox.Text = string.Empty;
            this.AssessedValueTextBox.Text = string.Empty;
            this.ResultingTaxableTextBox.Text = string.Empty;
            this.DistrictlinkLabel.Text = string.Empty;
            this.BusinessNameTextBox.Text = string.Empty;
            this.AssessmentTypeTextBox.Text = string.Empty;
            this.NewConstrctionTextBox.Text = string.Empty;
            this.MID1TextBox.Text = string.Empty;
            this.MID2TextBox.Text = string.Empty;
            this.MID3TextBox.Text = string.Empty;
            this.MID4TextBox.Text = string.Empty;
            this.MID5TextBox.Text = string.Empty;
            this.ScheduleDORTextBox.Text = string.Empty;
            this.ExemptTextBox.Text = SharedFunctions.GetResourceString("No");
        }

        #endregion Clear Controls

        #region Load Personalal Property Details

        /// <summary>
        /// Gets the schedule details.
        /// </summary>
        /// <param name="scheduleValue">The schedule value.</param>
        private void GetPropertyHeaderDetails(int scheduleValue)
        {
            // DB call for retrieving data for specific kyid
            this.personalPropertyData = this.form25055Control.WorkItem.GetPropertyHeaderDetails(scheduleValue);

            if (this.personalPropertyData.GetPersonalPropertyDetail.Rows.Count > 0)
            {
                // Set the retrieved values on appropriate controls and make it enable
                this.SetPropertyHeaderDetails((F25055PropertyHeaderData.GetPersonalPropertyDetailRow)this.personalPropertyData.GetPersonalPropertyDetail.Rows[0]);
                this.EnableScheduleHeader(true);
            }
            else
            {
                // If there is no relevent record presents for that keyid, clear all controls and make it disable
                this.ClearControls();
                this.EnableScheduleHeader(false);
            }
        }

        #endregion Load Personalal Property Details

        #region Set Control Value

        /// <summary>
        /// Sets the schedule details.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetPropertyHeaderDetails(F25055PropertyHeaderData.GetPersonalPropertyDetailRow selectedRow)
        {
            this.SetLinkDetails(selectedRow);
            this.SetMIDIDetails(selectedRow);

            if (!string.IsNullOrEmpty(selectedRow.RollYear.ToString()))
            {
                this.RollYearTextBox.Text = selectedRow.RollYear.ToString();
            }
            else
            {
                this.RollYearTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsFilingTypeNull())
            {
                this.SourceTextBox.Text = selectedRow.FilingType;
            }
            else
            {
                this.SourceTextBox.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(selectedRow.IsReview.ToString()))
            {
                this.ReviewTextBox.Text = selectedRow.IsReview;
            }
            else
            {
                this.ReviewTextBox.Text = string.Empty;
            }

            if (selectedRow.IsExempt)
            {
                this.ExemptTextBox.Text = SharedFunctions.GetResourceString("Yes");
            }
            else
            {
                this.ExemptTextBox.Text = SharedFunctions.GetResourceString("No");
            }

            if (!selectedRow.IsPropertyTypeNull())
            {
                this.PropertyTypeTextBox.Text = selectedRow.PropertyType;
            }
            else
            {
                this.PropertyTypeTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsAssessmentTypeNull())
            {
                this.AssessmentTypeTextBox.Text = selectedRow.AssessmentType;
            }
            else
            {
                this.AssessmentTypeTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsScheduleTypeNull())
            {
                this.ScheduleTypeTextBox.Text = selectedRow.ScheduleType;
                if (!selectedRow.ScheduleType.ToUpper().Equals("ACTIVE"))
                {
                    // Set fore color as red if schedultype is other than 'Active'
                    this.ScheduleTypeTextBox.ForeColor = Color.Red;
                }
                else
                {
                    // Set fore color as gray if schedule type is 'Active'
                    this.ScheduleTypeTextBox.ForeColor = Color.Gray;
                }
            }
            else
            {
                this.ScheduleTypeTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsDescriptionNull())
            {
                this.DescriptionTextBox.Text = selectedRow.Description;
            }
            else
            {
                this.DescriptionTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsStreetAddressNull())
            {
                this.StreetAddressTextBox.Text = selectedRow.StreetAddress;
            }
            else
            {
                this.StreetAddressTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsScheduleStateCodeNull())
            {
                this.ScheduleDORTextBox.Text = selectedRow.ScheduleStateCode;
            }
            else
            {
                this.ScheduleDORTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsParcelStateCodeNull())
            {
                this.RealPropertyDORTextBox.Text = selectedRow.ParcelStateCode;
            }
            else
            {
                this.RealPropertyDORTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsNAICSNull())
            {
                this.NAICSTextBox.Text = selectedRow.NAICS;
            }
            else
            {
                this.NAICSTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsLegalNull())
            {
                this.LegalTextBox.Text = selectedRow.Legal;
            }
            else
            {
                this.LegalTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsMapNumberNull())
            {
                this.RealPropertyMapNumberTextBox.Text = selectedRow.MapNumber;
            }
            else
            {
                this.RealPropertyMapNumberTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsRealPropertyResultingTaxableNull())
            {
                this.RealPropTaxableTextBox.Text = selectedRow.RealPropertyResultingTaxable.ToString();
            }
            else
            {
                this.RealPropTaxableTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsFilingDateNull())
            {
                this.FilingDateTextBox.Text = selectedRow.FilingDate.ToString();
            }
            else
            {
                this.FilingDateTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsPenaltyAmountNull())
            {
                this.PenaltyAmountTextBox.Text = selectedRow.PenaltyAmount.ToString();
            }
            else
            {
                this.PenaltyAmountTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsPenaltyPercentNull())
            {
                this.PenaltyPercentTextBox.TextCustomFormat = TerraScanCommon.CustomDecimalFormat(selectedRow.PenaltyPercent.ToString())+ "%";
                this.PenaltyPercentTextBox.Text = selectedRow.PenaltyPercent.ToString();
            }
            else
            {
                this.PenaltyPercentTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsNewConstructionNull())
            {
                this.NewConstrctionTextBox.Text = selectedRow.NewConstruction.ToString();
            }
            else
            {
                this.NewConstrctionTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsPPTaxableValueNull())
            {
                this.ScheduleTaxableTextBox.Text = selectedRow.PPTaxableValue.ToString();
            }
            else
            {
                this.ScheduleTaxableTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsResultingTaxableNull())
            {
                this.ResultingTaxableTextBox.Text = selectedRow.ResultingTaxable.ToString();
            }
            else
            {
                this.ResultingTaxableTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsBuisnessNameNull())
            {
                this.BusinessNameTextBox.Text = selectedRow.BuisnessName;
            }
            else
            {
                this.BusinessNameTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the MIDI details.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetMIDIDetails(F25055PropertyHeaderData.GetPersonalPropertyDetailRow selectedRow)
        {
            if (!selectedRow.IsMIDLabel1Null())
            {
                if (selectedRow.MIDLabel1.Trim().EndsWith(":") || string.IsNullOrEmpty(selectedRow.MIDLabel1.Trim()))
                {
                    this.MID1label.Text = selectedRow.MIDLabel1.Trim();
                }
                else
                {
                    this.MID1label.Text = selectedRow.MIDLabel1.Trim() + ":";
                }
            }
            else
            {
                this.MID1label.Text = string.Empty;
            }

            if (!selectedRow.IsMID1Null())
            {
                this.MID1TextBox.Text = selectedRow.MID1;
            }
            else
            {
                this.MID1TextBox.Text = string.Empty;
            }

            if (!selectedRow.IsMIDLabel2Null())
            {
                if (selectedRow.MIDLabel2.Trim().EndsWith(":") || string.IsNullOrEmpty(selectedRow.MIDLabel2.Trim()))
                {
                    this.MID2label.Text = selectedRow.MIDLabel2.Trim();
                }
                else
                {
                    this.MID2label.Text = selectedRow.MIDLabel2.Trim() + ":";
                }
            }
            else
            {
                this.MID2label.Text = string.Empty;
            }

            if (!selectedRow.IsMID2Null())
            {
                this.MID2TextBox.Text = selectedRow.MID2;
            }
            else
            {
                this.MID2TextBox.Text = string.Empty;
            }

            if (!selectedRow.IsMIDLabel3Null())
            {
                if (selectedRow.MIDLabel3.Trim().EndsWith(":") || string.IsNullOrEmpty(selectedRow.MIDLabel3.Trim()))
                {
                    this.MID3label.Text = selectedRow.MIDLabel3.Trim();
                }
                else
                {
                    this.MID3label.Text = selectedRow.MIDLabel3.Trim() + ":";
                }
            }
            else
            {
                this.MID3label.Text = string.Empty;
            }

            if (!selectedRow.IsMID3Null())
            {
                this.MID3TextBox.Text = selectedRow.MID3;
            }
            else
            {
                this.MID3TextBox.Text = string.Empty;
            }

            if (!selectedRow.IsMIDLabel4Null())
            {
                if (selectedRow.MIDLabel4.Trim().EndsWith(":") || string.IsNullOrEmpty(selectedRow.MIDLabel4.Trim()))
                {
                    this.MID4label.Text = selectedRow.MIDLabel4.Trim();
                }
                else
                {
                    this.MID4label.Text = selectedRow.MIDLabel4.Trim() + ":";
                }
            }
            else
            {
                this.MID4label.Text = string.Empty;
            }

            if (!selectedRow.IsMID4Null())
            {
                this.MID4TextBox.Text = selectedRow.MID4;
            }
            else
            {
                this.MID4TextBox.Text = string.Empty;
            }

            if (!selectedRow.IsMIDLabel5Null())
            {
                if (selectedRow.MIDLabel5.Trim().EndsWith(":") || string.IsNullOrEmpty(selectedRow.MIDLabel5.Trim()))
                {
                    this.MID5label.Text = selectedRow.MIDLabel5.Trim();
                }
                else
                {
                    this.MID5label.Text = selectedRow.MIDLabel5.Trim() + ":";
                }
            }
            else
            {
                this.MID5label.Text = string.Empty;
            }

            if (!selectedRow.IsMID5Null())
            {
                this.MID5TextBox.Text = selectedRow.MID5;
            }
            else
            {
                this.MID5TextBox.Text = string.Empty;
            }

            if (!selectedRow.IsO1ValueNull())
            {
                this.FairCashTextBox.Text = selectedRow.O1Value.ToString();
            }
            else
            {
                this.FairCashTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsO2ValueNull())
            {
                this.CappedValueTextBox.Text = selectedRow.O2Value.ToString();
            }
            else
            {
                this.CappedValueTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsO3ValueNull())
            {
                this.AssessedValueTextBox.Text = selectedRow.O3Value.ToString();
            }
            else
            {
                this.AssessedValueTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the value in appropriate link field
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetLinkDetails(F25055PropertyHeaderData.GetPersonalPropertyDetailRow selectedRow)
        {
            if (!selectedRow.IsParcelIDNull())
            {
                this.parcelId = selectedRow.ParcelID;
            }
            else
            {
                this.parcelId = 0;
            }

            if (!selectedRow.IsDistrictIDNull())
            {
                this.districtId = selectedRow.DistrictID;
            }
            else
            {
                this.districtId = 0;
            }

            if (!selectedRow.IsOwnerIDNull())
            {
                this.ownerId = selectedRow.OwnerID;
            }
            else
            {
                this.ownerId = 0;
            }

            if (!string.IsNullOrEmpty(selectedRow.ScheduleNumber.ToString()))
            {
                this.ScheduleNumberLinkLabel.Text = selectedRow.ScheduleNumber;
            }
            else
            {
                this.ScheduleNumberLinkLabel.Text = string.Empty;
            }

            if (!selectedRow.IsParcelNumberNull())
            {
                this.ParcelReferencelinkLabel.Text = selectedRow.ParcelNumber;
            }
            else
            {
                this.ParcelReferencelinkLabel.Text = string.Empty;
            }

            if (!selectedRow.IsPrimaryOwnerNull())
            {
                this.PrimaryOwnerlinkLabel.Text = selectedRow.PrimaryOwner;
            }
            else
            {
                this.PrimaryOwnerlinkLabel.Text = string.Empty;
            }

            if (!selectedRow.IsDistrictNull())
            {
                this.DistrictlinkLabel.Text = selectedRow.District;
            }
            else
            {
                this.DistrictlinkLabel.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(selectedRow.ScheduleNumber.ToString()))
            {
                this.ScheduleNumberLinkLabel.Text = selectedRow.ScheduleNumber;
            }
            else
            {
                this.ScheduleNumberLinkLabel.Text = string.Empty;
            }
        }

        #endregion Set Control value

        #region Show Sub Form

        /// <summary>
        /// Shows the sub form.
        /// </summary>
        /// <param name="formNumber">The form number.</param>
        /// <param name="keyId">The key id.</param>
        private void ShowSubForm(int formNumber, int keyId)
        {
            // Open the form using formNumber and specific keyid
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(formNumber);
            formInfo.optionalParameters = new object[1];
            formInfo.optionalParameters[0] = keyId;
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }

        #endregion Show Sub Form

        #endregion PrivateMethords
     }
}
    
