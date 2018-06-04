//--------------------------------------------------------------------------------------------
// <copyright file="F27000.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27000.
// </summary>
//----------------------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------------------
// 02 Apr 07        Ranjani JG        	    Created// 
// 11 Feb 11        Manoj                   Changes in weed Control Configurations.
// 20120424         Manoj                   Changed the roll Year for the Form Shows Current Record Roll Year
// 20121015         Manoj P                  Created two new fields for MAD Type 4 Parcel rate and Acre Rate. 
//***********************************************************************************************************
namespace D22000
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Resources;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using System.Globalization;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;

    /// <summary>
    /// Form F27000
    /// </summary>   
    public partial class F27000 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15018Control Controller
        /// </summary>
        private F27000Controller form27000Control;

        /// <summary>
        /// Unique keyId from the Form Master - statement id
        /// </summary>
        private int keyId;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        // Added static Variable TSBG - 22000 Misc Assessments Form - Account percentage not retaining 2 decimal places
        public static string commonValue = "";

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// miscAssessment dataset
        /// </summary>
        private F22000MiscAssessmentData miscAssessment = new F22000MiscAssessmentData();

        /// <summary>
        /// Unique districtTypeId - contains district typeid - default value(0)
        /// </summary>
        private int districtTypeId;

        /// <summary>
        /// checks history grid row validation cancelled or not
        /// </summary>
        private bool processRowEnter;

        /// <summary>
        /// pageLoadStatus variable is used to find the pageLoadStatus - default true. 
        /// </summary>   
        private bool pageLoadStatus = true;

        /// <summary>
        /// distributedItemDeleted variable is used to find the distributedItemDeleted - default false. 
        /// </summary>   
        private bool distributedItemDeleted;

        /// <summary>
        /// Instance variable to hold the max money field value
        /// </summary>
        private double maxMoneyFieldValue = 922337203685477.58;

        private bool isAccountRequired = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F27000"/> class.
        /// </summary>
        public F27000()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15018"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F27000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.HeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.HeaderPictureBox.Height, this.HeaderPictureBox.Width, string.Empty, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F27000 control.
        /// </summary>
        /// <value>The F27000 control.</value>
        [CreateNew]
        public F27000Controller Form27000Control
        {
            get { return this.form27000Control as F27000Controller; }
            set { this.form27000Control = value; }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////setting the pagemode
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                ////clear dataset
                this.miscAssessment.Clear();
                this.HeaderPanel.Enabled = true;
                this.DistrictTypeComboBox.Enabled = true;
                ////display default Assesser Rollyear - Assigning Default Value. 
                CommentsData.GetCommentsConfigDetailsDataTable commentsConfigDetailsDataTable = this.form27000Control.WorkItem.GetConfigDetails("AA_RollYear").GetCommentsConfigDetails;
                if (commentsConfigDetailsDataTable.Rows.Count > 0)
                {
                    this.RollYearTextBox.Text = commentsConfigDetailsDataTable.Rows[0][commentsConfigDetailsDataTable.ConfigurationValueColumn.ColumnName].ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }
                ////assign default value to district type
                this.DistrictTypeComboBox.SelectedValue = 0;
                this.MiscAssessmentPanel.Visible = false;
                this.MiscAssessmentPictureBox.Visible = false;
                ////enable or disable required controls
                this.SetFieldsPermission();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.DistrictTypeComboBox.Focus();
                this.Cursor = Cursors.Default;
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
                this.Cursor = Cursors.WaitCursor;
                this.GetMiscAssessmentDetails();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
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
                this.Cursor = Cursors.WaitCursor;
                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.SaveMiscAssessment();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

                    ////check wether the form is populated with records - based on thekeyid                    
                    if (this.miscAssessment.GetMADetails.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.SetFieldsPermission();
                        ////setdefault selection
                        this.RollYearTextBox.Focus();
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.GetMiscAssessmentDetails();
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

        #endregion

        #region Protected Methods

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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F27000 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F27000_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                ////load combo boxes
                this.LoadComboBox();
                ////Binding Database Columns to distribution Grid.
                this.CustomizeDistributionGridView();
                ////populate form details
                this.GetMiscAssessmentDetails();
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

        #region Misc Assessment Details

       
        /// <summary>
        /// Gets the Misc Assessment details and fill sub headers.
        /// </summary>
        private void GetMiscAssessmentDetails()
        {
            try
            {
                this.miscAssessment.Clear();
                bool outValue;
                this.miscAssessment = this.form27000Control.WorkItem.F27000_GetMiscAssessment(this.keyId);

                if (this.miscAssessment.AccountRequiredTable.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.miscAssessment.AccountRequiredTable.Rows[0][this.miscAssessment.AccountRequiredTable.IsAccountRequiredColumn].ToString()))
                    {
                        if (this.miscAssessment.AccountRequiredTable.Rows[0][this.miscAssessment.AccountRequiredTable.IsAccountRequiredColumn].ToString().Trim().ToUpper().Equals("TRUE"))
                        {
                            this.isAccountRequired = true;

                        }
                        else if (this.miscAssessment.AccountRequiredTable.Rows[0][this.miscAssessment.AccountRequiredTable.IsAccountRequiredColumn].ToString().Trim().ToUpper().Equals("FALSE"))
                        {
                            this.isAccountRequired = false;
                        }
                    }
                    else
                    {

                        this.isAccountRequired = true;
                    }
                }
                //this.isAccountRequired = outValue;
                ////change pagemode
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                ////set pageLoadStatus - suppress textchanged event
                this.pageLoadStatus = true;

                if (this.miscAssessment.GetMADetails.Rows.Count > 0)
                {
                    ////enable Receipt Panel
                    this.HeaderPanel.Enabled = true;
                    ////get receipt header details
                    F22000MiscAssessmentData.GetMADetailsRow miscAssessmentRow = (F22000MiscAssessmentData.GetMADetailsRow)this.miscAssessment.GetMADetails.Rows[0];
                    F22000MiscAssessmentData.GetMADetailsDataTable miscAssessmentDataTable = (F22000MiscAssessmentData.GetMADetailsDataTable)this.miscAssessment.GetMADetails;
                    ////check for district type
                    if (miscAssessmentRow.IsMADTypeIDNull() || miscAssessmentRow.MADTypeID <= 0)
                    {
                        this.ClearMiscAssessmentDetails();
                        return;
                    }
                    else
                    {
                        this.DistrictTypeComboBox.SelectedValue = miscAssessmentRow.MADTypeID;
                        this.districtTypeId = miscAssessmentRow.MADTypeID;
                        this.DistrictTypeComboBox.Enabled = false;
                    }

                    if (miscAssessmentRow.IsRollYearNull())
                    {
                        this.RollYearTextBox.Text = string.Empty;
                    }
                    else
                    {
                        this.RollYearTextBox.Text = miscAssessmentRow.RollYear.ToString();
                    }
                    ////load subheader details
                    this.LoadSubFormHeader();
                    ////check for valid district type
                    if (this.districtTypeId > 0)
                    {
                        ////load common sub header 1
                        this.DistrictNumberTextBox.Text = miscAssessmentRow.DistrictNumber;
                        this.DescriptionTextBox.Text = miscAssessmentRow.Description;

                        switch (this.districtTypeId)
                        {
                            case 1:
                                ////load for district type 1 - subheader 2                            
                                this.BaseFeeTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseFeeColumn].ToString();
                                this.MinimumChargeTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinimumChargeColumn].ToString();
                                this.LevyRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.LevyRateColumn].ToString();
                                this.MaximumAcresTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MaximumAcresColumn].ToString();
                                this.SiteBaseTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.SiteBaseColumn].ToString();
                                this.SiteRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.SiteRateColumn].ToString();
                                ////subheader 3
                                this.DryBaseTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DryBaseColumn].ToString();
                                this.DryRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DryRateColumn].ToString();
                                this.IrrigatedBaseTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.IrrigatedBaseColumn].ToString();
                                this.IrrigatedRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.IrrigatedRateColumn].ToString();
                                this.TimberBaseTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TimberBaseColumn].ToString();
                                this.TimberRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TimberRateColumn].ToString();
                                this.OtherBaseTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.OtherBaseColumn].ToString();
                                this.OtherRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.OtherRateColumn].ToString();
                                break;
                            case 2:
                                ////load for district type 2 - subheader 2                    
                                this.BaseFeeType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseFeeColumn].ToString();
                                this.MinChargeType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinimumChargeColumn].ToString();
                                if (!miscAssessmentRow.IsIsOwnerAssessedNull())
                                {
                                    this.AssessByType2ComboBox.SelectedValue = miscAssessmentRow.IsOwnerAssessed;
                                }
                                this.BaseRateType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseRateColumn].ToString();
                                this.BaseAcreType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseAcresColumn].ToString();
                                this.MinSiteTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinSiteAcresColumn].ToString();
                                this.SiteBaseType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.SiteBaseColumn].ToString();
                                this.SiteRateType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.SiteRateColumn].ToString();

                                // Subheader 3
                                this.MinDryTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinDryAcresColumn].ToString();
                                this.DryBaseType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DryBaseColumn].ToString();
                                this.DryRateType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DryRateColumn].ToString();
                                this.MinIrrigatedTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinIrrigatedAcresColumn].ToString();
                                this.IrrigatedBaseType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.IrrigatedBaseColumn].ToString();
                                this.IrrigatedRateType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.IrrigatedRateColumn].ToString();

                                // Subheader 4
                                this.MinTimberTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinTimberAcresColumn].ToString();
                                this.TimberBaseType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TimberBaseColumn].ToString();
                                this.TimberRateType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TimberRateColumn].ToString();
                                this.MinOtherTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinOtherAcresColumn].ToString();
                                this.OtherBaseType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.OtherBaseColumn].ToString();
                                this.OtherRateType2TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.OtherRateColumn].ToString();
                                break;
                            case 3:
                                ////load for district type 3
                                this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinimumChargeColumn].ToString();
                                ////subheader 2  
                                this.BaseFeeType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseFeeColumn].ToString();
                                this.BaseRateType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseRateColumn].ToString();
                                this.SiteBaseType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.SiteBaseColumn].ToString();
                                this.SiteRateType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.SiteRateColumn].ToString();
                                this.DryBaseType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DryBaseColumn].ToString();
                                this.DryRateType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DryRateColumn].ToString();
                                ////subheader 3 
                                this.IrrigatedBaseType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.IrrigatedBaseColumn].ToString();
                                this.IrrigatedRateType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.IrrigatedRateColumn].ToString();
                                this.TimberBaseType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TimberBaseColumn].ToString();
                                this.TimberRateType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TimberRateColumn].ToString();
                                this.OtherBaseType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.OtherBaseColumn].ToString();
                                this.OtherRateType3TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.OtherRateColumn].ToString();
                                break;
                            case 4:
                                //    ////load for district type 4   
                                //    this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.RateColumn].ToString();
                                this.LevyRateType4TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.RateColumn].ToString();
                                this.ParcelRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.ParcelRateColumn].ToString();
                                this.AcreRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.AcreRateColumn].ToString();
                                break;
                            case 5:
                                ////load for district type 5   
                                this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.RateColumn].ToString();
                                break;
                            case 6:
                                ////load for district type 6
                                this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.CountyFeeColumn].ToString();
                                ////subheader 2                 
                                this.MaxLotSizeTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MaxLotSizeColumn].ToString();
                                this.FPAFlatFeeTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.FPAFeeColumn].ToString();
                                this.LCFFlatFeeTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.LCFFeeColumn].ToString();
                                this.TotalFlatFeeTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TotalFlatFeeColumn].ToString();
                                this.FPAAcreRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.FPARateColumn].ToString();
                                this.LCFAcreRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.LCFRateColumn].ToString();
                                this.TotalAcreRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.TotalAcreRateColumn].ToString();
                                break;
                            case 7:
                                this.ResidentialRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.ResidentialRateColumn].ToString();
                                this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.CommercialRateColumn].ToString();
                                break;
                            case 8:
                                this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.ConservationBaseFeeColumn].ToString();
                                break;
                            case 9:
                                this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.LakeRateColumn].ToString();
                                break;
                            case 10:
                                // Load for district type 10 - Milfoil District   
                                this.RateType5TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.RateColumn].ToString();
                                this.BaseFeeType5TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseFeeColumn].ToString();
                                this.MinimumType5TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MinimumColumn].ToString();
                                this.MaximumType5TextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MaximumColumn].ToString();
                                break;
                            case 11:
                                // Load for district type 10 - Irrigation District   
                                this.SubHeader1DistrictTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DistrictNumberColumn].ToString();
                                this.SubHeader1DescriptionTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.DescriptionColumn].ToString();
                                this.SubHeader1PerAcreTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.PerAcreColumn].ToString();
                                this.SubHeader1PerParcelTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.PerParcelColumn].ToString();
                                this.SubHeader1MarketRateTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.MarketRateColumn].ToString();
                                break;
                            case 12:
                                // Load for district type 10 - Lighting District   
                                this.SubHeader1DynamicTextBox.Text = miscAssessmentRow[miscAssessmentDataTable.BaseFeeColumn].ToString();
                                break;
                        }
                    }
                    ////load distribution
                    this.PopulateDistributionItemGrid();
                    ////sets permission to fields
                    this.SetFieldsPermission();
                    this.RollYearTextBox.Focus();
                }
                else
                {
                    this.ClearMiscAssessmentDetails();
                }
                ////reset pageLoadStatus - trigger textchanged event
                this.pageLoadStatus = false;
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Clears the Misc Receipt Details
        /// </summary>
        private void ClearMiscAssessmentDetails()
        {
            ////clear header section
            this.DistrictTypeComboBox.SelectedIndex = -1;
            this.RollYearTextBox.Text = string.Empty;
            this.HeaderPanel.Enabled = false;
            this.MiscAssessmentPanel.Visible = false;
            this.MiscAssessmentPictureBox.Visible = false;
            ////////clears subheader section
            ////this.ClearSubHeaderSection();
            ////////clear distribution section
            ////this.miscAssessment.ListMADistributionItem.Clear();
            ////this.DistributionGridView.DataSource = this.miscAssessment.ListMADistributionItem;
            ////this.MiscAssessmentPanel.Enabled = false;
        }

        /// <summary>
        /// Clears the Sub Header Section.
        /// </summary>
        private void ClearSubHeaderSection()
        {
            ////check for valid district type
            if (this.districtTypeId > 0)
            {
                ////clear common sub header 1
                this.DistrictNumberTextBox.Text = string.Empty;
                this.DescriptionTextBox.Text = string.Empty;
                this.SubHeader1DynamicTextBox.Text = string.Empty;

                switch (this.districtTypeId)
                {
                    case 1:
                        ////clear for district type 1 - subheader 2
                        this.BaseFeeTextBox.Text = string.Empty;
                        this.MinimumChargeTextBox.Text = string.Empty;
                        this.LevyRateTextBox.Text = string.Empty;
                        this.MaximumAcresTextBox.Text = string.Empty;
                        this.SiteBaseTextBox.Text = string.Empty;
                        this.SiteRateTextBox.Text = string.Empty;
                        ////subheader 3
                        this.DryBaseTextBox.Text = string.Empty;
                        this.DryRateTextBox.Text = string.Empty;
                        this.IrrigatedBaseTextBox.Text = string.Empty;
                        this.IrrigatedRateTextBox.Text = string.Empty;
                        this.TimberBaseTextBox.Text = string.Empty;
                        this.TimberRateTextBox.Text = string.Empty;
                        this.OtherBaseTextBox.Text = string.Empty;
                        this.OtherRateTextBox.Text = string.Empty;
                        break;
                    case 2:
                        // clear for district type 2 - subheader 2                    
                        this.BaseFeeType2TextBox.Text = string.Empty;
                        this.MinChargeType2TextBox.Text = string.Empty;
                        // default value - no
                        this.AssessByType2ComboBox.SelectedValue = 0;
                        this.BaseAcresTextBox.Text = string.Empty;
                        //new changes
                        this.BaseAcreType2TextBox.Text = string.Empty;
                        this.SiteBaseType2TextBox.Text = string.Empty;
                        this.BaseRateType2TextBox.Text = string.Empty;  
                        this.MinSiteTextBox.Text = string.Empty;
                        this.SiteBaseTextBox.Text = string.Empty;
                        this.SiteRateType2TextBox.Text = string.Empty;
                        // subheader 3
                        this.MinDryTextBox.Text = string.Empty;
                        this.DryBaseType2TextBox.Text = string.Empty;
                        this.DryRateType2TextBox.Text = string.Empty;
                        this.MinIrrigatedTextBox.Text = string.Empty;
                        this.IrrigatedBaseType2TextBox.Text = string.Empty;
                        this.IrrigatedRateType2TextBox.Text = string.Empty;
                        // SubHeader 4
                        this.MinTimberTextBox.Text = string.Empty;
                        this.TimberBaseType2TextBox.Text = string.Empty;
                        this.TimberRateType2TextBox.Text = string.Empty;
                        this.MinOtherTextBox.Text = string.Empty;
                        this.OtherBaseType2TextBox.Text = string.Empty;
                        this.OtherRateType2TextBox.Text = string.Empty;
                        break;
                    case 3:
                        ////clear for district type 3 - subheader 2  
                        this.BaseFeeType3TextBox.Text = string.Empty;
                        this.BaseRateType3TextBox.Text = string.Empty;
                        this.SiteBaseType3TextBox.Text = string.Empty;
                        this.SiteRateType3TextBox.Text = string.Empty;
                        this.DryBaseType3TextBox.Text = string.Empty;
                        this.DryRateType3TextBox.Text = string.Empty;
                        ////subheader 3 
                        this.IrrigatedBaseType3TextBox.Text = string.Empty;
                        this.IrrigatedRateType3TextBox.Text = string.Empty;
                        this.TimberBaseType3TextBox.Text = string.Empty;
                        this.TimberRateType3TextBox.Text = string.Empty;
                        this.OtherBaseType3TextBox.Text = string.Empty;
                        this.OtherRateType3TextBox.Text = string.Empty;
                        break;
                    case 4:
                        ////clear for district type 4    - subheader1 cleared  
                        //new field Datas being Closed
                        this.LevyRateType4TextBox.Text = string.Empty;
                        this.ParcelRateTextBox.Text = string.Empty;
                        this.AcreRateTextBox.Text = string.Empty;  
                        break;
                    case 5:
                        ////clear for district type 5   - subheader1 cleared                        
                        break;
                    case 6:
                        ////clear for district type 6 - subheader 2                 
                        this.MaxLotSizeTextBox.Text = string.Empty;
                        this.FPAFlatFeeTextBox.Text = string.Empty;
                        this.LCFFlatFeeTextBox.Text = string.Empty;
                        this.TotalFlatFeeTextBox.Text = string.Empty;
                        this.FPAAcreRateTextBox.Text = string.Empty;
                        this.LCFAcreRateTextBox.Text = string.Empty;
                        this.TotalAcreRateTextBox.Text = string.Empty;
                        break;
                    case 7:
                        this.ResidentialRateTextBox.Text = string.Empty;
                        break;
                    case 10:
                        this.RateType5TextBox.Text = string.Empty;
                        this.BaseFeeType5TextBox.Text = string.Empty;
                        this.MinimumType5TextBox.Text = string.Empty;
                        this.MaximumType5TextBox.Text = string.Empty;
                        break;
                    case 11:
                        this.SubHeader1DistrictTextBox.Text = string.Empty;
                        this.SubHeader1DescriptionTextBox.Text = string.Empty;
                        this.SubHeader1PerAcreTextBox.Text = string.Empty;
                        this.SubHeader1PerParcelTextBox.Text = string.Empty;
                        this.SubHeader1MarketRateTextBox.Text = string.Empty;
                        break;
                }
            }
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves the Misc Assessment.
        /// </summary>
        private void SaveMiscAssessment()
        {
            ////clears save Misc Assessment
            this.miscAssessment.SaveMiscAssessment.Rows.Clear();
            F22000MiscAssessmentData.SaveMiscAssessmentRow miscRow = this.miscAssessment.SaveMiscAssessment.NewSaveMiscAssessmentRow();
            ////for edit record
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                miscRow.MADistrictID = this.keyId;
            }

            miscRow.MADTypeID = this.districtTypeId;
            // check for rollyear
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                miscRow.RollYear = this.RollYearTextBox.NumericTextBoxValue;
            }
            // for common subheader value
            if (!string.IsNullOrEmpty(this.DistrictNumberTextBox.Text.Trim()))
            {
                miscRow.DistrictNumber = this.DistrictNumberTextBox.Text;
            }

            if (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                miscRow.Description = this.DescriptionTextBox.Text;
            }

            if (this.districtTypeId.Equals(1))
            {
                // load for district type 1 - subheader 2                            
                if (!string.IsNullOrEmpty(this.BaseFeeTextBox.Text.Trim()))
                {
                    miscRow.BaseFee = this.BaseFeeTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.MinimumChargeTextBox.Text.Trim()))
                {
                    miscRow.MinimumCharge = this.MinimumChargeTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.SiteBaseTextBox.Text.Trim()))
                {
                    miscRow.SiteBase = this.SiteBaseTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.SiteRateTextBox.Text.Trim()))
                {
                    miscRow.SiteRate = this.SiteRateTextBox.DecimalTextBoxValue;
                }
                // subheader 3
                if (!string.IsNullOrEmpty(this.DryBaseTextBox.Text.Trim()))
                {
                    miscRow.DryBase = this.DryBaseTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.DryRateTextBox.Text.Trim()))
                {
                    miscRow.DryRate = this.DryRateTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.IrrigatedBaseTextBox.Text.Trim()))
                {
                    miscRow.IrrigatedBase = this.IrrigatedBaseTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.IrrigatedRateTextBox.Text.Trim()))
                {
                    miscRow.IrrigatedRate = this.IrrigatedRateTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.TimberBaseTextBox.Text.Trim()))
                {
                    miscRow.TimberBase = this.TimberBaseTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.TimberRateTextBox.Text.Trim()))
                {
                    miscRow.TimberRate = this.TimberRateTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.OtherBaseTextBox.Text.Trim()))
                {
                    miscRow.OtherBase = this.OtherBaseTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.OtherRateTextBox.Text.Trim()))
                {
                    miscRow.OtherRate = this.OtherRateTextBox.DecimalTextBoxValue;
                }

                // load for district type 1 - subheader 2   
                if (!string.IsNullOrEmpty(this.LevyRateTextBox.Text.Trim()))
                {
                    miscRow.LevyRate = this.LevyRateTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.MaximumAcresTextBox.Text.Trim()))
                {
                    miscRow.MaximumAcres = this.MaximumAcresTextBox.NumericTextBoxValue;
                }
            }
            else if (this.districtTypeId.Equals(2))
            {
                // For DistrictType 2 (Weed)
                this.RetrieveWeedDetails(miscRow);
            }
            else
            {
                // Get SubHeader Getails
                this.RetrieveSubHeader(miscRow);
            }

            this.miscAssessment.SaveMiscAssessment.Rows.Add(miscRow);
            // Get distribution items
            this.miscAssessment.ListMADistributionItem.AcceptChanges();
            DataView tempDataView = new DataView(this.miscAssessment.ListMADistributionItem, string.Concat(this.miscAssessment.ListMADistributionItem.AccountIDColumn.ColumnName, " IS NOT NULL AND ", this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, " IS NOT NULL AND ", this.miscAssessment.ListMADistributionItem.RateColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
            DataTable tempDataTable = tempDataView.ToTable();
            int savedDistrictId = 0;
            savedDistrictId = this.form27000Control.WorkItem.F27000_SaveMADetails(Utility.GetXmlString(tempDataTable), Utility.GetXmlString(this.miscAssessment.SaveMiscAssessment), TerraScanCommon.UserId);

            // district saved
            if (savedDistrictId > 0)
            {
                this.keyId = savedDistrictId;
            }

            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

            // populate misc assessment with the newly saved record           
            this.GetMiscAssessmentDetails();
        }

        /// <summary>
        /// Retrieves the sub header.
        /// </summary>
        /// <param name="miscRow">The misc row.</param>
        private void RetrieveSubHeader(F22000MiscAssessmentData.SaveMiscAssessmentRow miscRow)
        {
            ////check for district typeid
            switch (this.districtTypeId)
            {
                case 3:
                    ////load for district type 3
                    if (!string.IsNullOrEmpty(this.SubHeader1DynamicTextBox.Text.Trim()))
                    {
                        miscRow.MinimumCharge = this.SubHeader1DynamicTextBox.DecimalTextBoxValue;
                    }
                    ////subheader 2  
                    if (!string.IsNullOrEmpty(this.BaseFeeType3TextBox.Text.Trim()))
                    {
                        miscRow.BaseFee = this.BaseFeeType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.BaseRateType3TextBox.Text.Trim()))
                    {
                      ///Changes in the Base Rate from Numeric Text Box Value into Decimal Text Box Value.
                        miscRow.BaseRate = this.BaseRateType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.SiteBaseType3TextBox.Text.Trim()))
                    {
                        miscRow.SiteBase = this.SiteBaseType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.SiteRateType3TextBox.Text.Trim()))
                    {
                        miscRow.SiteRate = this.SiteRateType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.DryBaseType3TextBox.Text.Trim()))
                    {
                        miscRow.DryBase = this.DryBaseType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.DryRateType3TextBox.Text.Trim()))
                    {
                        miscRow.DryRate = this.DryRateType3TextBox.DecimalTextBoxValue;
                    }
                    ////subheader 3 
                    if (!string.IsNullOrEmpty(this.IrrigatedBaseType3TextBox.Text.Trim()))
                    {
                        miscRow.IrrigatedBase = this.IrrigatedBaseType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.IrrigatedRateType3TextBox.Text.Trim()))
                    {
                        miscRow.IrrigatedRate = this.IrrigatedRateType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.TimberBaseType3TextBox.Text.Trim()))
                    {
                        miscRow.TimberBase = this.TimberBaseType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.TimberRateType3TextBox.Text.Trim()))
                    {
                        miscRow.TimberRate = this.TimberRateType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.OtherBaseType3TextBox.Text.Trim()))
                    {
                        miscRow.OtherBase = this.OtherBaseType3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.OtherRateType3TextBox.Text.Trim()))
                    {
                        miscRow.OtherRate = this.OtherRateType3TextBox.DecimalTextBoxValue;
                    }

                    break;
                case 4:
                ////load for district type 4 - same as of type 5  
                    if (!string.IsNullOrEmpty(this.LevyRateType4TextBox.Text.Trim()))
                    {
                        miscRow.Rate = this.LevyRateType4TextBox.DecimalTextBoxValue;  
                    }
                    if (!string.IsNullOrEmpty(this.ParcelRateTextBox.Text.Trim()))
                    {
                        miscRow.ParcelRate = this.ParcelRateTextBox.DecimalTextBoxValue;
                    }
                    if (!string.IsNullOrEmpty(this.AcreRateTextBox.Text.Trim()))
                    {
                        miscRow.AcreRate = this.AcreRateTextBox.DecimalTextBoxValue;
                    }
                    break;
                    
                case 5:
                    // load for district type 5  
                    if (!string.IsNullOrEmpty(this.SubHeader1DynamicTextBox.Text.Trim()))
                    {
                        miscRow.Rate = this.SubHeader1DynamicTextBox.DecimalTextBoxValue;
                    }

                    break;
                case 6:
                    // load for district type 6
                    if (!string.IsNullOrEmpty(this.SubHeader1DynamicTextBox.Text.Trim()))
                    {
                        miscRow.CountyFee = this.SubHeader1DynamicTextBox.DecimalTextBoxValue;
                    }
                    // subheader 2      
                    if (!string.IsNullOrEmpty(this.MaxLotSizeTextBox.Text.Trim()))
                    {
                        miscRow.MaxLotSize = this.MaxLotSizeTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.FPAFlatFeeTextBox.Text.Trim()))
                    {
                        miscRow.FPAFee = this.FPAFlatFeeTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.LCFFlatFeeTextBox.Text.Trim()))
                    {
                        miscRow.LCFFee = this.LCFFlatFeeTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.FPAAcreRateTextBox.Text.Trim()))
                    {
                        miscRow.FPARate = this.FPAAcreRateTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.LCFAcreRateTextBox.Text.Trim()))
                    {
                        miscRow.LCFRate = this.LCFAcreRateTextBox.DecimalTextBoxValue;
                    }

                    break;
                case 7:
                    // load for district type 7
                    if (!string.IsNullOrEmpty(this.ResidentialRateTextBox.Text.Trim()))
                    {
                        miscRow.ResidentialRate = this.ResidentialRateTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.SubHeader1DynamicTextBox.Text.Trim()))
                    {
                        miscRow.CommercialRate = this.SubHeader1DynamicTextBox.DecimalTextBoxValue;
                    }

                    break;
                case 8:
                    // load for district type 8  
                    if (!string.IsNullOrEmpty(this.SubHeader1DynamicTextBox.Text.Trim()))
                    {
                        miscRow.ConservationBaseFee = this.SubHeader1DynamicTextBox.DecimalTextBoxValue;
                    }

                    break;
                case 9:
                    // load for district type 9  
                    if (!string.IsNullOrEmpty(this.SubHeader1DynamicTextBox.Text.Trim()))
                    {
                        miscRow.LakeRate = this.SubHeader1DynamicTextBox.DecimalTextBoxValue;
                    }

                    break;
                case 10:
                    if (!string.IsNullOrEmpty(this.RateType5TextBox.Text.Trim()))
                    {
                        miscRow.Rate = this.RateType5TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.BaseFeeType5TextBox.Text.Trim()))
                    {
                        miscRow.BaseFee = this.BaseFeeType5TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.MinimumType5TextBox.Text.Trim()))
                    {
                        miscRow.Minimum = this.MinimumType5TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.MaximumType5TextBox.Text.Trim()))
                    {
                        miscRow.Maximum = this.MaximumType5TextBox.DecimalTextBoxValue;
                    }
                    break;
                case 11:
                    // load for district type 8  
                    if (!string.IsNullOrEmpty(this.SubHeader1DistrictTextBox.Text.Trim()))
                    {
                        miscRow.DistrictNumber = this.SubHeader1DistrictTextBox.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(this.SubHeader1DescriptionTextBox.Text.Trim()))
                    {
                        miscRow.Description = this.SubHeader1DescriptionTextBox.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(this.SubHeader1PerAcreTextBox.Text.Trim()))
                    {
                        miscRow.PerAcre = this.SubHeader1PerAcreTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.SubHeader1PerParcelTextBox.Text.Trim()))
                    {
                        miscRow.PerParcel = this.SubHeader1PerParcelTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.SubHeader1MarketRateTextBox.Text.Trim()))
                    {
                        miscRow.MarketRate = this.SubHeader1MarketRateTextBox.DecimalTextBoxValue;
                    }

                    break;
                case 12:
                    // load for district type 8  
                    if (!string.IsNullOrEmpty(this.SubHeader1DynamicTextBox.Text.Trim()))
                    {
                        miscRow.BaseFee = this.SubHeader1DynamicTextBox.DecimalTextBoxValue;
                    }

                    break;
            }
        }

        /// <summary>
        /// Retrieves the weed details.
        /// </summary>
        /// <param name="miscRow">The misc row.</param>
        private void RetrieveWeedDetails(F22000MiscAssessmentData.SaveMiscAssessmentRow miscRow)
        {
            // Retrieve SubheaderType2 details
            // subheader 2   
            if (this.AssessByType2ComboBox.SelectedIndex > -1)
            {
                miscRow.IsOwnerAssessed = Convert.ToBoolean(this.AssessByType2ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.BaseAcreType2TextBox.Text.Trim()))
            {
                miscRow.BaseAcres = this.BaseAcreType2TextBox.NumericTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.BaseFeeType2TextBox.Text.Trim()))
            {
                miscRow.BaseFee = this.BaseFeeType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.MinChargeType2TextBox.Text.Trim()))
            {
                miscRow.MinimumCharge = this.MinChargeType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.MinSiteTextBox.Text.Trim()))
            {
                miscRow.MinSiteAcres = this.MinSiteTextBox.NumericTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.SiteBaseType2TextBox.Text.Trim()))
            {
                miscRow.SiteBase = this.SiteBaseType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.SiteRateType2TextBox.Text.Trim()))
            {
                miscRow.SiteRate = this.SiteRateType2TextBox.DecimalTextBoxValue;
            }
            if (!string.IsNullOrEmpty(this.BaseRateType2TextBox.Text.Trim()))
            {
                miscRow.BaseRate = this.BaseRateType2TextBox.DecimalTextBoxValue;
            }

            // subheader 3
            if (!string.IsNullOrEmpty(this.MinDryTextBox.Text.Trim()))
            {
                miscRow.MinDryAcres = this.MinDryTextBox.NumericTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.DryBaseType2TextBox.Text.Trim()))
            {
                miscRow.DryBase = this.DryBaseType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.DryRateType2TextBox.Text.Trim()))
            {
                miscRow.DryRate = this.DryRateType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.MinIrrigatedTextBox.Text.Trim()))
            {
                miscRow.MinIrrigatedAcres = this.MinIrrigatedTextBox.NumericTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.IrrigatedBaseType2TextBox.Text.Trim()))
            {
                miscRow.IrrigatedBase = this.IrrigatedBaseType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.IrrigatedRateType2TextBox.Text.Trim()))
            {
                miscRow.IrrigatedRate = this.IrrigatedRateType2TextBox.DecimalTextBoxValue;
            }

            // subheader 4
            if (!string.IsNullOrEmpty(this.MinTimberTextBox.Text.Trim()))
            {
                miscRow.MinTimberAcres = this.MinTimberTextBox.NumericTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.TimberBaseType2TextBox.Text.Trim()))
            {
                miscRow.TimberBase = this.TimberBaseType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.TimberRateType2TextBox.Text.Trim()))
            {
                miscRow.TimberRate = this.TimberRateType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.MinOtherTextBox.Text.Trim()))
            {
                miscRow.MinOtherAcres = this.MinOtherTextBox.NumericTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.OtherBaseType2TextBox.Text.Trim()))
            {
                miscRow.OtherBase = this.OtherBaseType2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.OtherRateType2TextBox.Text.Trim()))
            {
                miscRow.OtherRate = this.OtherRateType2TextBox.DecimalTextBoxValue;
            }
        }

        #endregion

        #region User Defined Methods

        /// <summary>
        /// Displays the account icon.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void DisplayAccountIcon(int rowIndex)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) || this.slicePermissionField.editPermission || this.formMasterPermissionEdit)
            {
                if (this.DistributionGridView.Rows.Count > 1)
                {
                    for (int i = 0; i < this.DistributionGridView.Rows.Count; i++)
                    {
                        TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.DistributionGridView[this.Account.Name, i];
                        if (rowIndex == i)
                        {
                            imgCell.Image = Properties.Resources.Abutton;
                        }
                        else
                        {
                            if (rowIndex >= 0)
                            {
                                try
                                {
                                    imgCell.Image = new Bitmap(1, 1);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    this.DistributionGridView.Refresh();
                }
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns SliceValidationFields - validated in master form</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            
            ////required field validation
            if (this.DistrictTypeComboBox.SelectedIndex <= 0)
            {                
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("1031MissingRequiredFields");
                this.DistrictTypeComboBox.Focus();
                return sliceValidationFields;
                
            }

            if (String.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("1031MissingRequiredFields");
                this.RollYearTextBox.Focus();
                return sliceValidationFields;
            }

            string districtNumber = string.Empty;

            if (this.districtTypeId != 11)
            {
                districtNumber = this.DistrictNumberTextBox.Text.Trim();
            }
            else
            {
                districtNumber = this.SubHeader1DistrictTextBox.Text.Trim();
            }

            if (String.IsNullOrEmpty(districtNumber))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("1031MissingRequiredFields");
                if (this.districtTypeId != 11)
                {
                    this.DistrictNumberTextBox.Focus();
                }
                else
                {
                    this.SubHeader1DistrictTextBox.Focus();
                }

                return sliceValidationFields;
            }
        
            //// added by shiva.
            this.miscAssessment.ListMADistributionItem.AcceptChanges();
            this.DistributionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            DataView tempDataView = new DataView(this.miscAssessment.ListMADistributionItem, string.Concat(this.miscAssessment.ListMADistributionItem.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, " IS NOT NULL OR ", this.miscAssessment.ListMADistributionItem.RateColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
            int recordCount = tempDataView.Count;
            ////tempDataView = new DataView(this.miscAssessment.ListMADistributionItem, string.Concat(this.miscAssessment.ListMADistributionItem.AccountIDColumn.ColumnName, " IS NOT NULL AND ", this.miscAssessment.ListMADistributionItem.MADistItemIDColumn.ColumnName, " IS NOT NULL AND ", this.miscAssessment.ListMADistributionItem.RateColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
            tempDataView = new DataView(this.miscAssessment.ListMADistributionItem, string.Concat(this.miscAssessment.ListMADistributionItem.AccountIDColumn.ColumnName, " IS NOT NULL AND ", this.miscAssessment.ListMADistributionItem.RateColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
            if (recordCount == 0 || recordCount != tempDataView.Count)
            {
                if (isAccountRequired)
                {
                    sliceValidationFields.RequiredFieldMissing = true;
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("1031MissingRequiredFields");
                    this.DistributionGridView.Focus();
                    return sliceValidationFields;
                }
            }
            else
            {
                ////true IF Sum fo the rate fields total is 100%              
                DataTable tempDataTable = tempDataView.ToTable();
                //
                int distributionType = 0;
                DataGridViewComboBoxColumn col = (this.DistributionGridView.Columns[this.DistributionType.Name] as DataGridViewComboBoxColumn);
                for (int i = 0; i < col.Items.Count; i++)
                {
                    DataRow dr = (col.Items[i] as DataRowView).Row;
                    int districtTypeCount = 0;
                    if (!string.IsNullOrEmpty(dr[col.ValueMember].ToString()))
                    {
                        ////districtTypeCount = Convert.ToInt32(tempDataTable.Compute(string.Concat("COUNT(", this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, ")"), string.Concat(this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, "=", dr[col.ValueMember])));
                        districtTypeCount = Convert.ToInt32(tempDataTable.Compute(string.Concat("COUNT(", this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, ")"), string.Concat(this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, "=", "'" + dr[col.ValueMember] + "'")));
                        //Added for TSBG - 22000 Misc Assessments Form - Account percentage not retaining 2 decimal places
                         distributionType = Convert.ToInt32(dr.ItemArray[0]);
                    }

                    
                    if (districtTypeCount > 0)
                    {
                        //Added for TSBG - 22000 Misc Assessments Form - Account percentage not retaining 2 decimal places
                        ////decimal totalPercentage = Convert.ToDecimal(tempDataTable.Compute(string.Concat("SUM(", this.miscAssessment.ListMADistributionItem.RateColumn.ColumnName, ")"), string.Concat(this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, "=", dr[col.ValueMember])));
                        decimal totalPercentage = 0;//Convert.ToDecimal(tempDataTable.Compute(string.Concat("SUM(", this.miscAssessment.ListMADistributionItem.RateColumn.ColumnName, ")"), string.Concat(this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName, "=", "'" + dr[col.ValueMember] + "'")));
                        for (int j = 0; j < this.DistributionGridView.OriginalRowCount; j++)
                        {
                            if (Convert.ToInt32(this.DistributionGridView.Rows[j].Cells[0].Value) == distributionType)
                            {
                                decimal tempValue = 0;
                                tempValue = Convert.ToDecimal(this.DistributionGridView.Rows[j].Cells[1].Value);
                                tempValue = tempValue * 100;
                                tempValue = Convert.ToDecimal(tempValue.ToString("###.##"));
                                totalPercentage += Convert.ToDecimal(tempValue);
                            }
                           
                        }
                        totalPercentage = totalPercentage / 100;
                        int count = BitConverter.GetBytes(decimal.GetBits(totalPercentage)[3])[2];
                       
                        if (totalPercentage != 1)
                        {
                            sliceValidationFields.RequiredFieldMissing = true;
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("WrongPercentageTotal");
                            this.DistributionGridView.Focus();
                            return sliceValidationFields;
                        }
                    }
                }
            }

            ////validation stands for text box controls          
            decimal maxMoneyValue = (decimal)int.MaxValue;

            // checks for - smallmoney datatype range
            maxMoneyValue = maxMoneyValue / 10000;
            decimal maxPercentValue = 100;

            int maxIntegerValue = int.MaxValue;

            Int16 maxSmallIntValue = Int16.MaxValue;

            this.EditControlRangeValidation(ref sliceValidationFields, maxMoneyValue, maxPercentValue, maxIntegerValue, maxSmallIntValue);
            ///maxPercentage value for weed Control is 999.000%
            this.WeedControlRangeValidation(ref sliceValidationFields, maxMoneyValue, 999, maxIntegerValue, maxSmallIntValue);
            this.OtherTypesRangeValidation(ref sliceValidationFields, maxMoneyValue, maxPercentValue, maxIntegerValue, maxSmallIntValue);
            return sliceValidationFields;
        }

        /// <summary>
        /// textbox controls range validation.
        /// </summary>
        /// <param name="sliceValidationFields">The slice validation fields.</param>
        private void EditControlRangeValidation(ref SliceValidationFields sliceValidationFields, decimal maxMoneyValue, decimal maxPercentValue, int maxIntegerValue, Int16 maxSmallIntValue)
        {
            if (this.RollYearTextBox.NumericTextBoxValue.Equals(0))
            {
                sliceValidationFields.ErrorMessage = String.Concat(this.RollYearTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidFieldYear"), "\n");
                this.BaseFeeTextBox.Focus();
                return;
            }

            if (this.districtTypeId.Equals(1))
            {
                // check for district type 1, 2 - subheader 2                            
                if (!this.MoneyValidation(ref sliceValidationFields, this.BaseFeeTextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.MinimumChargeTextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.SiteBaseTextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.SiteRateTextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.DryBaseTextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.DryRateTextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.IrrigatedBaseTextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.IrrigatedRateTextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.TimberBaseTextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.TimberRateTextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.OtherBaseTextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.OtherRateTextBox, maxPercentValue))
                {
                    return;
                }

                // check for district type 1 - subheader 2  
                if (!this.RateValidation(ref sliceValidationFields, this.LevyRateTextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.IntegerValidation(ref sliceValidationFields, this.MaximumAcresTextBox, maxIntegerValue))
                {
                    return;
                }
            }
        }

        private void WeedControlRangeValidation(ref SliceValidationFields sliceValidationFields, decimal maxMoneyValue, decimal maxPercentValue, int maxIntegerValue, Int16 maxSmallIntValue)
        {
            if (this.districtTypeId.Equals(2))
            {
                // check for district type 2                        
                if (!this.MoneyValidation(ref sliceValidationFields, this.BaseFeeType2TextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.MinChargeType2TextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.IntegerValidation(ref sliceValidationFields, this.MinSiteTextBox, maxIntegerValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.SiteBaseType2TextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.SiteRateType2TextBox, maxPercentValue))
                {
                    return;
                }
                if (!this.RateValidation(ref sliceValidationFields, this.BaseRateType2TextBox , maxPercentValue))
                {
                    return;
                }
                if (!this.IntegerValidation(ref sliceValidationFields, this.MinDryTextBox, maxIntegerValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.DryBaseType2TextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.DryRateType2TextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.IntegerValidation(ref sliceValidationFields, this.MinIrrigatedTextBox, maxIntegerValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.IrrigatedBaseType2TextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.IrrigatedRateType2TextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.IntegerValidation(ref sliceValidationFields, this.MinTimberTextBox, maxIntegerValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.TimberBaseType2TextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.TimberRateType2TextBox, maxPercentValue))
                {
                    return;
                }

                if (!this.IntegerValidation(ref sliceValidationFields, this.MinOtherTextBox, maxIntegerValue))
                {
                    return;
                }

                if (!this.MoneyValidation(ref sliceValidationFields, this.OtherBaseType2TextBox, maxMoneyValue))
                {
                    return;
                }

                if (!this.RateValidation(ref sliceValidationFields, this.OtherRateType2TextBox, maxPercentValue))
                {
                    return;
                }

                // check for district type 2 
                if (maxSmallIntValue < this.BaseAcreType2TextBox.NumericTextBoxValue)
                {
                    sliceValidationFields.ErrorMessage = String.Concat(this.BaseAcreType2TextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidSize"), "\n");
                    this.BaseAcreType2TextBox.Focus();
                    return;
                }
            }
        }

        private void OtherTypesRangeValidation(ref SliceValidationFields sliceValidationFields, decimal maxMoneyValue, decimal maxPercentValue, int maxIntegerValue, Int16 maxSmallIntValue)
        {
            switch (this.districtTypeId)
            {
                case 3:
                    // check for district type 3

                    if (!this.MoneyValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.BaseFeeType3TextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.BaseRateType3TextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.SiteBaseType3TextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.SiteRateType3TextBox, maxPercentValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.DryBaseType3TextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.DryRateType3TextBox, maxPercentValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.IrrigatedBaseType3TextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.IrrigatedRateType3TextBox, maxPercentValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.TimberBaseType3TextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.TimberRateType3TextBox, maxPercentValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.OtherBaseType3TextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.OtherRateType3TextBox, maxPercentValue))
                    {
                        return;
                    }

                    break;
                case 4:
                // check for district type 4 - same as of type 5 
                    if (!this.MoneyValidation(ref sliceValidationFields, this.ParcelRateTextBox, maxMoneyValue))
                    {
                        return;
                    }
                    if (!this.MoneyValidation(ref sliceValidationFields, this.AcreRateTextBox, maxMoneyValue))
                    {
                        return;
                    }
                    if (!this.RateValidation(ref sliceValidationFields, this.LevyRateType4TextBox, maxPercentValue))
                    {
                        return;
                    }
                    break;
                case 5:
                    // check for district type 5  

                    if (!this.MoneyValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxMoneyValue))
                    {
                        return;
                    }

                    break;

                case 6:
                    // check for district type 6
                    if (!this.MoneyValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxMoneyValue))
                    {
                        return;
                    }

                    // subheader 2      

                    if (maxIntegerValue < this.MaxLotSizeTextBox.NumericTextBoxValue)
                    {
                        sliceValidationFields.ErrorMessage = String.Concat(this.MaxLotSizeTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidSize"), "\n");
                        this.MaxLotSizeTextBox.Focus();
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.FPAFlatFeeTextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.LCFFlatFeeTextBox, maxMoneyValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.FPAAcreRateTextBox, maxPercentValue))
                    {
                        return;
                    }

                    if (!this.MoneyValidation(ref sliceValidationFields, this.LCFAcreRateTextBox, maxMoneyValue))
                    {
                        return;
                    }

                    break;

                case 7:
                    // check for district type 7
                    if (!this.RateValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxPercentValue))
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.ResidentialRateTextBox, maxPercentValue))
                    {
                        return;
                    }

                    break;

                case 8:
                    // check for district type 8
                    if (!this.MoneyValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxMoneyValue))
                    {
                        return;
                    }

                    break;

                case 9:
                    // check for district type 9
                    maxPercentValue = 999999.999999M;

                    if (!this.RateValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxPercentValue))
                    {
                        return;
                    }

                    break;
                case 10:
                    // Check for district type 10 - Milfoil District

                    //if (!this.RateValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxPercentValue))
                    //{
                    //    return;
                    //}

                    decimal maxValue = 922337203685477.58M;
                    decimal minValue = -922337203685477.5808M;
                    if (maxValue < this.BaseFeeType5TextBox.DecimalTextBoxValue || minValue > this.BaseFeeType5TextBox.DecimalTextBoxValue)
                    {
                        sliceValidationFields.ErrorMessage = String.Concat(this.BaseFeeType5TextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidAmount"), "\n");
                        this.BaseFeeType5TextBox.Focus();
                        return;
                    }

                    if (maxValue < this.RateType5TextBox.DecimalTextBoxValue || minValue > this.RateType5TextBox.DecimalTextBoxValue)
                    {
                        sliceValidationFields.ErrorMessage = String.Concat("Rate", " ", SharedFunctions.GetResourceString("InvalidAmount"), "\n");
                        this.RateType5TextBox.Focus();
                        return;
                    }

                    if (maxValue < this.MinimumType5TextBox.DecimalTextBoxValue || minValue > this.MinimumType5TextBox.DecimalTextBoxValue)
                    {
                        sliceValidationFields.ErrorMessage = String.Concat("Minimum", " ", SharedFunctions.GetResourceString("InvalidAmount"), "\n");
                        this.MinimumType5TextBox.Focus();
                        return;
                    }
                    if (maxValue < this.MaximumType5TextBox.DecimalTextBoxValue || minValue > this.MaximumType5TextBox.DecimalTextBoxValue)
                    {
                        sliceValidationFields.ErrorMessage = String.Concat("Maximum", " ", SharedFunctions.GetResourceString("InvalidAmount"), "\n");
                        this.MaximumType5TextBox.Focus();
                        return;
                    }

                    break;
                case 11:
                    // check for district type 11
                    //if (!this.MoneyValidation(ref sliceValidationFields, this.SubHeader1PerAcreTextBox, maxMoneyValue))
                    //{
                    //    return;
                    //}

                    //if (!this.MoneyValidation(ref sliceValidationFields, this.SubHeader1PerParcelTextBox, maxMoneyValue))
                    //{
                    //    return;
                    //}
                    if (maxMoneyFieldValue < (double)this.SubHeader1PerAcreTextBox.DecimalTextBoxValue)
                    {
                        return;
                    }
                    if (maxMoneyFieldValue < (double)this.SubHeader1PerParcelTextBox.DecimalTextBoxValue)
                    {
                        return;
                    }

                    if (!this.RateValidation(ref sliceValidationFields, this.SubHeader1MarketRateTextBox, maxPercentValue))
                    {
                        return;
                    }

                    break;
                case 12:
                    // check for district type 12
                    //if (!this.MoneyValidation(ref sliceValidationFields, this.SubHeader1DynamicTextBox, maxMoneyValue))
                    //{
                    //    return;
                    //}
                    if (maxMoneyFieldValue < (double)this.SubHeader1DynamicTextBox.DecimalTextBoxValue)
                    {
                        return;
                    }
                    break;
            }
        }

        private bool MoneyValidation(ref SliceValidationFields sliceValidationFields, Control textContol, decimal maxMoneyValue)
        {
            TerraScanTextBox currentControl = (TerraScanTextBox)textContol;
            if (maxMoneyValue < currentControl.DecimalTextBoxValue)
            {
                sliceValidationFields.ErrorMessage = String.Concat(textContol.Tag, " ", SharedFunctions.GetResourceString("InvalidAmount"), "\n");
                textContol.Focus();
                return false;
            }

            return true;
        }

        private bool RateValidation(ref SliceValidationFields sliceValidationFields, Control textContol, decimal maxPercentValue)
        {
            TerraScanTextBox currentControl = (TerraScanTextBox)textContol;
            if (maxPercentValue < currentControl.DecimalTextBoxValue)
            {
                ////sliceValidationFields.ErrorMessage = String.Concat(textContol.Tag, " ", SharedFunctions.GetResourceString("InvalidRate"), "\n");
                sliceValidationFields.ErrorMessage = String.Concat(SharedFunctions.GetResourceString("InvalidRate"), "\n");
                textContol.Focus();
                return false;
            }

            return true;
        }

        private bool IntegerValidation(ref SliceValidationFields sliceValidationFields, Control textContol, decimal maxIntegerValue)
        {
            TerraScanTextBox currentControl = (TerraScanTextBox)textContol;
            if (!string.IsNullOrEmpty(currentControl.Text.Trim()) && maxIntegerValue < Int64.Parse(currentControl.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = String.Concat(textContol.Tag, " ", SharedFunctions.GetResourceString("InvalidRate"), "\n");
                textContol.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizeDistributionGridView
        /// </summary>
        private void CustomizeDistributionGridView()
        {
            this.DistributionGridView.AutoGenerateColumns = false;
            F22000MiscAssessmentData.ListMADistributionItemDataTable distributionItemDataTable = this.miscAssessment.ListMADistributionItem;
            this.DistributionType.DataPropertyName = distributionItemDataTable.DistributionTypeColumn.ColumnName;
            this.Percentage.DataPropertyName = distributionItemDataTable.RateColumn.ColumnName;
            this.Account.DataPropertyName = distributionItemDataTable.AccountNameColumn.ColumnName;
            this.DistributionId.DataPropertyName = distributionItemDataTable.MADistItemIDColumn.ColumnName;
            this.AccountId.DataPropertyName = distributionItemDataTable.AccountIDColumn.ColumnName;
            this.AccountStatus.DataPropertyName = distributionItemDataTable.IsActiveColumn.ColumnName;

            this.DistributionType.DisplayIndex = 0;
            this.Percentage.DisplayIndex = 1;
            this.Account.DisplayIndex = 2;

            this.DistributionGridView.DataSource = distributionItemDataTable.DefaultView;
            //TSBG - 22000 Misc Assessments Form - Account percentage not retaining 2 decimal places
            this.DistributionGridView.Columns[this.Percentage.Name].DefaultCellStyle.Format = "0.00 %";

            //this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName;
            this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.ItemTypeIDColumn.ColumnName;
            //this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.MADistItemIDColumn.ColumnName;
            //this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.AccountIDColumn.ColumnName;            
        }

        /// <summary>
        /// Calculatings the Distribution Item and add new row if needed.
        /// </summary>        
        private void CalculateDistributionItemCount()
        {
            int recordCount = 0;
            Decimal outDecimalValue;

            for (int counter = 0; counter < this.miscAssessment.ListMADistributionItem.Rows.Count; counter++)
            {
                if (!this.miscAssessment.ListMADistributionItem.Rows[counter].RowState.Equals(DataRowState.Detached) && !this.miscAssessment.ListMADistributionItem.Rows[counter].RowState.Equals(DataRowState.Deleted))
                {
                    if (Decimal.TryParse(this.miscAssessment.ListMADistributionItem.Rows[counter][this.miscAssessment.ListMADistributionItem.RateColumn].ToString(), out outDecimalValue) && !String.IsNullOrEmpty(this.miscAssessment.ListMADistributionItem.Rows[counter][this.miscAssessment.ListMADistributionItem.DistributionTypeColumn].ToString()))
                    {
                        if (outDecimalValue > 0 && !String.IsNullOrEmpty(this.miscAssessment.ListMADistributionItem.Rows[counter][this.miscAssessment.ListMADistributionItem.AccountIDColumn].ToString()))
                        {
                            recordCount++;
                        }
                    }
                }
            }

            if (recordCount == this.miscAssessment.ListMADistributionItem.Rows.Count || this.miscAssessment.ListMADistributionItem.Rows.Count < this.DistributionGridView.NumRowsVisible)
            {
                F22000MiscAssessmentData.ListMADistributionItemRow dr = this.miscAssessment.ListMADistributionItem.NewListMADistributionItemRow();
                if (this.miscAssessment.ListMADistributionItem.Columns.Contains(this.DistributionGridView.EmptyRecordColumnName))
                {
                    dr[this.DistributionGridView.EmptyRecordColumnName] = true;
                }

                this.miscAssessment.ListMADistributionItem.Rows.Add(dr);
                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.DistributionGridView.FirstDisplayedScrollingRowIndex = this.DistributionGridView.Rows.Count - this.DistributionGridView.NumRowsVisible;
                }

                this.DistributionGridView.Refresh();
            }

            // check for scrollbar visibility
            if (this.miscAssessment.ListMADistributionItem.Rows.Count > this.DistributionGridView.NumRowsVisible)
            {
                this.DistributionGridVscrollBar.Visible = false;
            }
            else
            {
                this.DistributionGridVscrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Sets the fields permission - set edit or new permission.
        /// </summary>
        private void SetFieldsPermission()
        {
            bool permissionFields = false;
            // set permission depending on page mode
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                permissionFields = false;
            }
            else
            {
                permissionFields = !this.slicePermissionField.editPermission || !this.formMasterPermissionEdit;
            }

            // lock header section
            this.RollYearTextBox.LockKeyPress = permissionFields;
            // lock sub header Section  - sub header 1
            this.DistrictNumberTextBox.LockKeyPress = permissionFields;
            this.DescriptionTextBox.LockKeyPress = permissionFields;
            this.ResidentialRateTextBox.LockKeyPress = permissionFields;
            this.SubHeader1DynamicTextBox.LockKeyPress = permissionFields;
            // SubHeader 1 Type 12
            this.SubHeader1DistrictTextBox.LockKeyPress = permissionFields;
            this.SubHeader1DescriptionTextBox.LockKeyPress = permissionFields;
            this.SubHeader1PerAcreTextBox.LockKeyPress = permissionFields;
            this.SubHeader1PerParcelTextBox.LockKeyPress = permissionFields;
            this.SubHeader1MarketRateTextBox.LockKeyPress = permissionFields;
            // subheader 2
            this.BaseFeeTextBox.LockKeyPress = permissionFields;
            this.MinimumChargeTextBox.LockKeyPress = permissionFields;
            this.LevyRateTextBox.LockKeyPress = permissionFields;
            this.MaximumAcresTextBox.LockKeyPress = permissionFields;
            this.BaseAcresTextBox.LockKeyPress = permissionFields;
            this.SiteBaseTextBox.LockKeyPress = permissionFields;
            this.SiteRateTextBox.LockKeyPress = permissionFields;
            ////this.AssessOwnerComboBox.Enabled = !permissionFields;
            // subheader 3
            this.DryBaseTextBox.LockKeyPress = permissionFields;
            this.DryRateTextBox.LockKeyPress = permissionFields;
            this.IrrigatedBaseTextBox.LockKeyPress = permissionFields;
            this.IrrigatedRateTextBox.LockKeyPress = permissionFields;
            this.TimberBaseTextBox.LockKeyPress = permissionFields;
            this.TimberRateTextBox.LockKeyPress = permissionFields;
            this.OtherBaseTextBox.LockKeyPress = permissionFields;
            this.OtherRateTextBox.LockKeyPress = permissionFields;
            // subheader 2 type 3
            this.BaseFeeType3TextBox.LockKeyPress = permissionFields;
            this.BaseRateType3TextBox.LockKeyPress = permissionFields;
            this.SiteBaseType3TextBox.LockKeyPress = permissionFields;
            this.SiteRateType3TextBox.LockKeyPress = permissionFields;
            this.DryBaseType3TextBox.LockKeyPress = permissionFields;
            this.DryRateType3TextBox.LockKeyPress = permissionFields;
            //New Fields for the form Misc assessement Tyep4
            this.LevyRateType4TextBox.LockKeyPress = permissionFields;
            this.ParcelRateTextBox.LockKeyPress = permissionFields;  
            this.AcreRateTextBox.LockKeyPress = permissionFields;  
            // subheader 3 type 3
            this.IrrigatedBaseType3TextBox.LockKeyPress = permissionFields;
            this.IrrigatedRateType3TextBox.LockKeyPress = permissionFields;
            this.TimberBaseType3TextBox.LockKeyPress = permissionFields;
            this.TimberRateType3TextBox.LockKeyPress = permissionFields;
            this.OtherBaseType3TextBox.LockKeyPress = permissionFields;
            this.OtherRateType3TextBox.LockKeyPress = permissionFields;
            // subheader 2 type 6
            this.MaxLotSizeTextBox.LockKeyPress = permissionFields;
            this.FPAFlatFeeTextBox.LockKeyPress = permissionFields;
            this.LCFFlatFeeTextBox.LockKeyPress = permissionFields;
            this.FPAAcreRateTextBox.LockKeyPress = permissionFields;
            this.LCFAcreRateTextBox.LockKeyPress = permissionFields;
            // distribution part
            this.DistributionType.ReadOnly = permissionFields;
            this.Percentage.ReadOnly = permissionFields;

            //      Subheader for Weed
            // Subheader 2 Type 2
            this.BaseFeeType2TextBox.LockKeyPress = permissionFields;
            this.MinChargeType2TextBox.LockKeyPress = permissionFields;
            this.MinSiteTextBox.LockKeyPress = permissionFields;
            this.BaseAcreType2TextBox.LockKeyPress = permissionFields;
            this.SiteBaseType2TextBox.LockKeyPress = permissionFields;
            this.SiteRateType2TextBox.LockKeyPress = permissionFields;
            this.BaseRateType2TextBox.LockKeyPress = permissionFields;  
            this.AssessByType2ComboBox.Enabled = !permissionFields;
            // Subheader 3 Type 2
            this.MinDryTextBox.LockKeyPress = permissionFields;
            this.DryBaseType2TextBox.LockKeyPress = permissionFields;
            this.DryRateType2TextBox.LockKeyPress = permissionFields;
            this.MinIrrigatedTextBox.LockKeyPress = permissionFields;
            this.IrrigatedBaseType2TextBox.LockKeyPress = permissionFields;
            this.IrrigatedRateType2TextBox.LockKeyPress = permissionFields;
            // Subheader 4 Type 2
            this.MinTimberTextBox.LockKeyPress = permissionFields;
            this.TimberBaseType2TextBox.LockKeyPress = permissionFields;
            this.TimberRateType2TextBox.LockKeyPress = permissionFields;
            this.MinOtherTextBox.LockKeyPress = permissionFields;
            this.OtherBaseType2TextBox.LockKeyPress = permissionFields;
            this.OtherRateType2TextBox.LockKeyPress = permissionFields;

            // Milfoil Type
            this.BaseFeeType5TextBox.LockKeyPress = permissionFields;
            this.RateType5TextBox.LockKeyPress = permissionFields;
            this.MinimumType5TextBox.LockKeyPress = permissionFields;
            this.MaximumType5TextBox.LockKeyPress = permissionFields;
        }

        /// <summary>
        /// LoadSubFormHeader according to the selected district type
        /// </summary>        
        private void LoadSubFormHeader()
        {
            this.MiscAssessmentPanel.Visible = true;
            this.MiscAssessmentPictureBox.Visible = true;
            string sectionText = this.DistrictTypeComboBox.Text;
            this.DescriptionPanel.Width = 642;
            this.DescriptionTextBox.Width = 628;
            this.ResidentialRatePanel.BringToFront();
            ////used to display the required panel according to the district type selected
            switch (this.districtTypeId)
            {
                case 1:
                    ////display for district type 1
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = true;
                    this.LevyRatePanel.Visible = true;
                    this.MaximumAcresPanel.Visible = true;
                    this.AssessOwnerPanel.Visible = false;
                    this.BaseAcresPanel.Visible = false;
                    this.SubHeader3Panel.Visible = true;
                    this.SubHeader1DynamicPanel.Visible = false;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;   
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader3Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.SubHeader2Panel.Height + this.SubHeader3Panel.Height + this.DistributionPanel.Height - 3;
                    break;
                case 2:
                    // display for district type 2   
                    this.SubHeaderType2Panel.Visible = true;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.LevyRatePanel.Visible = false;
                    this.MaximumAcresPanel.Visible = false;
                    this.AssessOwnerPanel.Visible = false;
                    this.BaseAcresPanel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = false;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeaderType2Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.SubHeaderType2Panel.Height + this.DistributionPanel.Height - 2;
                    break;
                case 3:
                    ////display for district type 3
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = true;
                    this.SubHeader2Type3Panel.Visible = true;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = true;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader3Type3Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.SubHeader2Type3Panel.Height + this.SubHeader3Type3Panel.Height + this.DistributionPanel.Height - 3;
                    break;
                case 4:
                    ////display for district type 4   
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = false;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = true;  
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader2Type4Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height+ this.SubHeader2Type4Panel.Height  + this.DistributionPanel.Height - 1;
                    break;
                case 5:
                    ////display for district type 5      
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = true;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader1Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.DistributionPanel.Height - 1;
                    break;
                case 6:
                    ////display for district type 6    
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = true;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = true;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader2Type6Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.SubHeader2Type6Panel.Height + this.DistributionPanel.Height - 2;
                    break;
                case 7:
                    ////display for district type 7   
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = true;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = true;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader1Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.DistributionPanel.Height - 1;
                    break;
                case 8:
                    ////display for district type 8    
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = true;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader1Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.DistributionPanel.Height - 1;
                    break;
                case 9:
                    ////display for district type 9    
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = true;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader1Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.DistributionPanel.Height - 1;
                    break;
                case 10:
                    // Display for district type 10 - MilFoil District      
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader5Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = false;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader5Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.SubHeader5Panel.Height + this.DistributionPanel.Height - 2;
                    break;
                case 11:
                    ////display for district type 8    
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = false;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = false;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = true;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader1Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.DistributionPanel.Height - 1;
                    break;
                case 12:
                    ////display for district type 8    
                    this.SubHeaderType2Panel.Visible = false;
                    this.SubHeader1Panel.Visible = true;
                    this.SubHeader2Panel.Visible = false;
                    this.SubHeader3Panel.Visible = false;
                    this.SubHeader1DynamicPanel.Visible = true;
                    this.SubHeader2Type3Panel.Visible = false;
                    this.SubHeader2Type4Panel.Visible = false;
                    this.SubHeader2Type6Panel.Visible = false;
                    this.SubHeader3Type3Panel.Visible = false;
                    this.ResidentialRatePanel.Visible = false;
                    this.SubHeader1Type12Panel.Visible = false;
                    this.SubHeader5Panel.Visible = false;
                    this.DistributionPanel.Top = this.SubHeader1Panel.Bottom;
                    this.MiscAssessmentPictureBox.Height = this.SubHeader1Panel.Height + this.DistributionPanel.Height - 1;
                    break;
                default:
                    ////if selected district type is not valid
                    this.MiscAssessmentPanel.Visible = false;
                    this.MiscAssessmentPictureBox.Visible = false;
                    sectionText = string.Empty;
                    break;
            }
            ////used to dynamically change the size of the panel
            this.DistributionPanel.Top -= 1;
            this.MiscAssessmentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscAssessmentPictureBox.Height, this.MiscAssessmentPictureBox.Width, sectionText, 82, 101, 140);
            //if (this.districtTypeId == 3 || this.districtTypeId == 4 || this.districtTypeId == 5 || this.districtTypeId == 6 || this.districtTypeId == 7 || this.districtTypeId == 8 || this.districtTypeId == 9)
            if (this.districtTypeId >= 3 && this.districtTypeId !=4 && this.districtTypeId <= 12)
            {
                if (this.SubHeader1Panel.Width.Equals(this.DistrictNumberPanel.Width + this.DescriptionPanel.Width - 1))
                {
                    this.DescriptionPanel.Width -= this.SubHeader1DynamicPanel.Width - 1;
                    this.DescriptionTextBox.Width -= this.SubHeader1DynamicPanel.Width;
                }

                this.SubHeader1DynamicTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////specific for subheader1 panel
                switch (this.districtTypeId)
                {
                    case 3:
                        ////display for district type 3
                        this.SubHeader1DynamicLabel.Text = "Minimum Charge:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "$ #,##0.00";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = true;
                        this.SubHeader1DynamicTextBox.Tag = "Minimum Charge";
                        break;
                    case 4:
                        ////display for district type 4                   
                        this.SubHeader1DynamicLabel.Text = "Levy Rate:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "0.0000000 %";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = false;
                        this.SubHeader1DynamicTextBox.Tag = "Levy";
                        break;
                    case 5:
                        ////display for district type 5                    
                        this.SubHeader1DynamicLabel.Text = "Rate:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "0.000 %";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = false;
                        break;
                    case 6:
                        ////display for district type 6                   
                        this.SubHeader1DynamicLabel.Text = "County Fee:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "$ #,##0.00";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = true;
                        this.SubHeader1DynamicTextBox.Tag = "County Fee";
                        break;
                    case 7:
                        ////display for district type 7                   
                        this.SubHeader1DynamicLabel.Text = "Commercial Rate:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "0.000 %";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = false;
                        this.SubHeader1DynamicTextBox.Tag = "Commercial Rate";
                        this.DescriptionPanel.Width = 384;
                        this.DescriptionTextBox.Width = 371;
                        break;
                    case 8:
                        ////display for district type 8                   
                        this.SubHeader1DynamicLabel.Text = "Base Fee:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "$ #,##0.00";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = true;
                        this.SubHeader1DynamicTextBox.Tag = "Base Fee";
                        break;
                    case 9:
                        ////display for district type 9                   
                        this.SubHeader1DynamicLabel.Text = "Rate:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "#,##0.000000 %";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = false;
                        this.SubHeader1DynamicTextBox.Tag = "Rate";
                        break;
                    case 10:
                        // Display for district type 10 - Milfoil District                   
                        //this.SubHeader1DynamicLabel.Text = "Rate:";
                        //this.SubHeader1DynamicTextBox.TextCustomFormat = "0.0000 %";
                        //this.SubHeader1DynamicTextBox.ApplyCFGFormat = false;
                        this.DescriptionPanel.Width = 642;
                        this.DescriptionTextBox.Width = 628;
                        break;
                    case 11:
                        ////display for district type 8                   
                        //this.SubHeader1DynamicLabel.Text = "Base Fee:";
                        //this.SubHeader1DynamicTextBox.TextCustomFormat = "$ #,##0.00";
                        //this.SubHeader1DynamicTextBox.ApplyCFGFormat = true;
                        //this.SubHeader1DynamicTextBox.Tag = "Base Fee";
                        break;
                    case 12:
                        ////display for district type 8                   
                        this.SubHeader1DynamicLabel.Text = "Base Fee:";
                        this.SubHeader1DynamicTextBox.TextCustomFormat = "$ #,##0.00";
                        this.SubHeader1DynamicTextBox.ApplyCFGFormat = true;
                        this.SubHeader1DynamicTextBox.Tag = "Base Fee";
                        break;
                }
            }
            else
            {
                if (!this.SubHeader1Panel.Width.Equals(this.DistrictNumberPanel.Width + this.DescriptionPanel.Width - 1))
                {
                    this.DescriptionPanel.Width += this.SubHeader1DynamicPanel.Width - 1;
                    this.DescriptionTextBox.Width += this.SubHeader1DynamicPanel.Width;
                }
            }
        }

        /// <summary>
        /// Populates the Distribution item grid.
        /// </summary>
        private void PopulateDistributionItemGrid()
        {
            this.processRowEnter = false;
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.DistributionGridView.AllowSorting = true;
            }
            else
            {
                this.DistributionGridView.AllowSorting = false;
            }

            this.DistributionGridView.IsSorted = false;
            this.DistributionGridView.DataSource = this.miscAssessment.ListMADistributionItem.DefaultView;

            ////load distribution item type
            CommonData.ComboKeyStringDataTableDataTable districtTypeDataTable = this.form27000Control.WorkItem.F27000_ListMADistrictItemType(this.districtTypeId).ComboKeyStringDataTable;
            CommonData.ComboKeyStringDataTableRow dr = districtTypeDataTable.NewComboKeyStringDataTableRow();
            districtTypeDataTable.Rows.InsertAt(dr, 0);
            (this.DistributionGridView.Columns[this.DistributionType.Name] as DataGridViewComboBoxColumn).DataSource = districtTypeDataTable.Copy();
            (this.DistributionGridView.Columns[this.DistributionType.Name] as DataGridViewComboBoxColumn).DisplayMember = districtTypeDataTable.KeyNameColumn.ToString();
            (this.DistributionGridView.Columns[this.DistributionType.Name] as DataGridViewComboBoxColumn).ValueMember = districtTypeDataTable.KeyIdColumn.ToString();
            //(this.DistributionGridView.Columns[this.DistributionType.Name] as DataGridViewComboBoxColumn).SortMode = DataGridViewColumnSortMode.NotSortable;


            if (this.DistributionGridView.Rows.Count > this.DistributionGridView.NumRowsVisible)
            {
                this.DistributionGridVscrollBar.Visible = false;
            }
            else
            {
                this.DistributionGridVscrollBar.Visible = true;
            }

            this.CalculateDistributionItemCount();
            this.DisplayAccountIcon(this.DistributionGridView.Rows.Count);
            this.processRowEnter = true;

            //this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.DistributionTypeColumn.ColumnName;
            this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.ItemTypeIDColumn.ColumnName;
            //this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.MADistItemIDColumn.ColumnName;
            //this.DistributionGridView.PrimaryKeyColumnName = this.miscAssessment.ListMADistributionItem.AccountIDColumn.ColumnName;            
        }

        /// <summary>
        /// Loads the with District Type combo value.
        /// </summary>
        private void LoadWithDistrictTypeComboValue()
        {
            this.pageLoadStatus = true;
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.districtTypeId = Convert.ToInt32(this.DistrictTypeComboBox.SelectedValue);
                ////reload with new district type id
                this.LoadSubFormHeader();
                this.ClearSubHeaderSection();
                ////populate distributin grid
                this.miscAssessment.ListMADistributionItem.Clear();
                this.PopulateDistributionItemGrid();
            }
        }

        /// <summary>
        /// This Method used to load combobox datasource
        /// LoadComboBox
        /// </summary>
        private void LoadComboBox()
        {
            CommonData commonData = new CommonData();
            ////customize District Type combobox - loads DistrictType to DistrictTypeComboBox                
            commonData = this.form27000Control.WorkItem.F27000_ListMADistrictType();
            this.DistrictTypeComboBox.DataSource = commonData.ComboBoxDataTable.Copy();
            this.DistrictTypeComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.DistrictTypeComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();

            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            ////load AssessOwner combobox
            this.AssessByType2ComboBox.DataSource = commonData.ComboBoxDataTable.Copy();
            this.AssessByType2ComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.AssessByType2ComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
            commonData.Clear();
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Removes the grid sorting.
        /// </summary>
        private void RemoveGridSorting()
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && (this.DistributionGridView.AllowSorting || this.DistributionGridView.IsSorted))
            {
                this.DistributionGridView.AllowSorting = false;
                this.DistributionGridView.ClearSorting();
                this.DistributionGridView.IsSorted = false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DistrictTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ////load the form with new district type id
                this.LoadWithDistrictTypeComboValue();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the DistrictTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void DistrictTypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ////check whether same id exist
                if (!this.districtTypeId.Equals(this.DistrictTypeComboBox.SelectedValue))
                {
                    ////load the form with new district type id
                    this.LoadWithDistrictTypeComboValue();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //Added for TSBG - 22000 Misc Assessments Form - Account percentage not retaining 2 decimal places
            string tempvalue=Convert.ToString(commonValue);
            Decimal tempPercentage=0;
            if (Decimal.TryParse(tempvalue, out tempPercentage))
            {
                 tempPercentage = Convert.ToDecimal(tempvalue) / 100;
            }
            
            try
            {
                // Only paint if desired, formattable column
                if (e.ColumnIndex == this.DistributionGridView.Columns[this.Percentage.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                }

                if (e.ColumnIndex == this.DistributionGridView.Columns[this.Account.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (this.DistributionGridView[this.AccountStatus.Name, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.DistributionGridView[this.AccountStatus.Name, e.RowIndex].Value.ToString()))
                    {
                        if (Convert.ToBoolean(this.DistributionGridView[this.AccountStatus.Name, e.RowIndex].Value))
                        {
                            e.CellStyle.BackColor = Color.FromArgb(187, 222, 173);
                        }

                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_Leave(object sender, EventArgs e)
        {
            try
            {
                this.DisplayAccountIcon(this.DistributionGridView.Rows.Count);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.processRowEnter)
                {
                    this.DisplayAccountIcon(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellMouseClick event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                ////used for distribution and percent column
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.DistributionGridView.AllowSorting)
                {
                    this.DistributionGridView.AllowSorting = false;
                }

                if ((this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) || this.slicePermissionField.editPermission && this.formMasterPermissionEdit) && e.RowIndex >= 0 && e.ColumnIndex == this.DistributionGridView.Columns[this.Account.Name].Index)
                {
                    if (((TerraScanTextAndImageCell)DistributionGridView[e.ColumnIndex, e.RowIndex]).Image == null)
                    {
                        this.DisplayAccountIcon(e.RowIndex);
                    }

                    if ((e.X >= 295) && (e.X <= (326 - 16)) && (e.Y >= 3) && (e.Y <= (22 - 3)))
                    {
                        int accountId = 0;
                        CommentsData.GetCommentsConfigDetailsDataTable commentsConfigDetailsDataTable = this.form27000Control.WorkItem.GetConfigDetails("TR_RollYear").GetCommentsConfigDetails;

                        int rollYear = DateTime.Now.Year;
                        // Retrieve the roll Year based on the current Record 

                        //if (commentsConfigDetailsDataTable.Rows.Count > 0)
                        //{
                        //    rollYear = int.Parse(commentsConfigDetailsDataTable.Rows[0][commentsConfigDetailsDataTable.ConfigurationValueColumn.ColumnName].ToString());
                        //}
                        if(!string.IsNullOrEmpty(this.RollYearTextBox.Text))
                        {
                            int.TryParse(this.RollYearTextBox.Text, out rollYear);
                        }
  
                        Form accountSelectionForm = this.form27000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, new object[] { rollYear }, this.form27000Control.WorkItem);
                        if (accountSelectionForm != null && accountSelectionForm.ShowDialog() == DialogResult.OK)
                        {
                            int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out accountId);
                            F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();
                            accountNameDataSet = this.form27000Control.WorkItem.F15013_GetAccountName(accountId);

                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                F15013ExciseTaxRateData.GetAccountNameRow dr = (F15013ExciseTaxRateData.GetAccountNameRow)accountNameDataSet.GetAccountName.Rows[0];

                                this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = dr.AccountName;
                                this.DistributionGridView[this.AccountStatus.Name, e.RowIndex].Value = dr.AccountStatus;
                                this.DistributionGridView[this.AccountId.Name, e.RowIndex].Value = accountId;
                                this.CalculateDistributionItemCount();
                                this.SetEditRecord();
                            }
                            else
                            {
                                this.DistributionGridView[e.ColumnIndex, e.RowIndex].Value = string.Empty;
                                this.DistributionGridView[this.AccountId.Name, e.RowIndex].Value = DBNull.Value;
                                this.DistributionGridView[this.AccountStatus.Name, e.RowIndex].Value = 0;
                            }
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
        /// Handles the EditingControlShowing event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.DistributionGrid_SelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.DistributionGrid_SelectionChangeCommitted);
                    ((ComboBox)e.Control).KeyDown += new KeyEventHandler(this.DistributionGrid_KeyDown);
                }

                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged += new EventHandler(this.DistributionGridControl_TextChanged);
                    e.Control.Validating -= new CancelEventHandler(this.DistributionGridControl_Validating);
                    e.Control.Validating += new CancelEventHandler(this.DistributionGridControl_Validating);
                    e.Control.KeyDown += new KeyEventHandler(this.DistributionGridControl_KeyDown);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DistributionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGrid_KeyDown(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the DistributionGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void DistributionGridControl_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.DistributionGridView.EditingControl.TextChanged -= new EventHandler(this.DistributionGridControl_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DistributionGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridControl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text == " ")
                {
                    (sender as TextBox).Text = string.Empty;
                }
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DistributionGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DistributionGridControl_KeyDown(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DistributionGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void DistributionGrid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) || this.slicePermissionField.editPermission || this.formMasterPermissionEdit)
                {
                    int tempIndex = -1;
                    if (this.DistributionGridView.CurrentCell != null)
                    {
                        tempIndex = this.DistributionGridView.CurrentCell.RowIndex;
                    }
                    ////delete key
                    if (e.KeyCode == Keys.Delete && tempIndex >= 0 && this.DistributionGridView.Rows[tempIndex].Selected)
                    {
                        if ((this.DistributionGridView[this.DistributionType.Name, tempIndex].Value != null && !string.IsNullOrEmpty(this.DistributionGridView[this.DistributionType.Name, tempIndex].Value.ToString())) || !string.IsNullOrEmpty(this.DistributionGridView[this.AccountId.Name, tempIndex].Value.ToString()) || (this.DistributionGridView[this.Percentage.Name, tempIndex].Value != null && !string.IsNullOrEmpty(this.DistributionGridView[this.Percentage.Name, tempIndex].Value.ToString())))
                        {
                            this.distributedItemDeleted = true;
                            if (this.DistributionGridView[this.DistributionId.Name, tempIndex].Value != null && int.TryParse(this.DistributionGridView[this.DistributionId.Name, tempIndex].Value.ToString(), out tempIndex))
                            {
                                DataView tempDataView = new DataView(this.miscAssessment.ListMADistributionItem);
                                tempDataView.RowFilter = string.Concat(this.miscAssessment.ListMADistributionItem.MADistItemIDColumn.ColumnName, " = ", tempIndex);
                                if (tempDataView.Count > 0)
                                {
                                    tempIndex = this.miscAssessment.ListMADistributionItem.Rows.IndexOf(tempDataView[0].Row);
                                    if (tempIndex >= 0)
                                    {
                                        this.miscAssessment.ListMADistributionItem.Rows.RemoveAt(tempIndex);
                                        this.miscAssessment.ListMADistributionItem.AcceptChanges();
                                        this.SetEditRecord();
                                        this.distributedItemDeleted = false;
                                        this.CalculateDistributionItemCount();
                                        if (this.DistributionGridView.CurrentCell != null)
                                        {
                                            tempIndex = this.DistributionGridView.CurrentCell.RowIndex;
                                        }
                                        else
                                        {
                                            tempIndex = this.DistributionGridView.Rows.Count;
                                        }

                                        this.DisplayAccountIcon(tempIndex);
                                    }
                                }
                            }
                        }
                    }
                }
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
        /// Handles the CellParsing event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
            

                // Only paint if desired column
                if (e.ColumnIndex == this.DistributionGridView.Columns[this.Percentage.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Modified format for TSBG - 22000 Misc Assessments Form - Account percentage not retaining 2 decimal places
                    if (e.Value != null)
                    {
                        commonValue = (string)(e.Value);
                        string tempvalue = e.Value.ToString().Trim();
                        if (!string.IsNullOrEmpty(tempvalue))
                        {
                            Decimal outDecimal;

                            if (tempvalue.EndsWith("."))
                            {
                                tempvalue = string.Concat(tempvalue, "0");
                            }
                            ////remove soecial character
                            tempvalue = tempvalue.Replace("%", "");
                            if (Decimal.TryParse(tempvalue, out outDecimal))
                            {
                                tempvalue = outDecimal.ToString();

                                if (tempvalue.Contains("-"))
                                {
                                    outDecimal = Decimal.Zero;
                                }
                                else
                                {
                                    outDecimal =  outDecimal / 100;
                                }
                            }

                            string val = e.Value.ToString();
                            int count = 0;
                            Decimal outDecimal1;
                            if (Decimal.TryParse(tempvalue, out outDecimal1))
                            {
                               // var text = val.ToString(System.Globalization.CultureInfo.InvariantCulture).TrimEnd('0');
                                decimal argument = Convert.ToDecimal(outDecimal1);
                                count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];
                            }
                            if (count == 0)
                            {
                                e.Value = "0.0 %";
                            }
                            else if (count == 2)
                            {
                                DistributionGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Format = "0.00 %";
                               
                               
                            }
                            else if(count==1)
                            {
                                DistributionGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Format = "0.00 %";
                            }

                            e.Value = outDecimal;
                            e.ParsingApplied = true;
                        }
                        else
                        {
                            e.Value = DBNull.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.RemoveGridSorting();
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellLeave event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.DistributionGridView.Columns[this.Account.Name].Index)
                {
                    this.RemoveGridSorting();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the DistributionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DistributionGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.distributedItemDeleted && !this.DistributionGridView.IsSorted)
                {
                    ////calculating related values for new values
                    this.CalculateDistributionItemCount();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the EditTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditControl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.SetEditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the FlatFeeControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FlatFeeControl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.SetEditRecord();
                }

                decimal totalValue = Decimal.Zero;
                ////claculate Total Flat Fee
                totalValue = this.FPAFlatFeeTextBox.DecimalTextBoxValue + this.LCFFlatFeeTextBox.DecimalTextBoxValue;
                this.TotalFlatFeeTextBox.Text = totalValue.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the AcreRateControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AcreRateControl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.SetEditRecord();
                }

                decimal totalValue = Decimal.Zero;
                ////claculate Total Acre Rate            
                totalValue = this.FPAAcreRateTextBox.DecimalTextBoxValue + this.LCFAcreRateTextBox.DecimalTextBoxValue;
                this.TotalAcreRateTextBox.Text = totalValue.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #endregion
    }
}

