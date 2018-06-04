//--------------------------------------------------------------------------------------------
// <copyright file="F2200.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Account Selection.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02-Feb-08		Sriparameswari.A        Created
// 03 April 09      Shanmuga Sundaram.A     Modified for implemention the CO:#5928
// 26 May 09        Malliga                 Permission has been implemented for the issue 5191   
// 14 JUL 11        Manoj                   Modified for  District Validation in the field - TSCO  11748 
// 02 Aug 11        Manoj                   Issue fixed for TSBG 13002
// 01 SEP 17        Dhineshkumar.J          Modified for Ex 259 Amount
//**********************************************************************************************/
namespace D20050
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Collections;
    using System.IO;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Globalization;

    /// <summary>
    /// F2200
    /// </summary>
    public partial class F2200 : BasePage
    {
        #region Variables

        /// <summary>
        /// Created Instance for F9101Controller
        /// </summary>
        private F2200Controller form2200Control;

        /// <summary>
        /// editScheduledata
        /// </summary>
        private F2200EditScheduleData editScheduledata = new F2200EditScheduleData();
        F2201CentrallyAssessedSearchData SearchDescriptionDataSet = new F2201CentrallyAssessedSearchData();

        /// <summary>
        /// scheduleId
        /// </summary>
        private int scheduleId;

        /// <summary>
        /// rollyear
        /// </summary>
        private int rollyear;

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData reviewComboData = new CommonData();

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData headOfHouseholdComboBoxdata = new CommonData();

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData exemptComboBoxdata = new CommonData();

        /// <summary>
        /// newDetails
        /// </summary>
        private bool newDetails;

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private int selectedownerId = -1;

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private string selectedownerName;

        /// <summary>
        /// variable holds the parcel Id value.
        /// </summary>
        private int parcelId;

        /// <summary>
        /// stateCode
        /// </summary>
        private int outValue;

        /// <summary>
        /// districtId
        /// </summary>
        private string districtId;

        /// <summary>
        /// isshift
        /// </summary>
        private bool isshift;

        /// <summary>
        /// missingReq
        /// </summary>
        private bool missingReq;

        /// <summary>
        /// isshift
        /// </summary>
        private bool dateSelection;

        /// <summary>
        /// SetSelectedDate
        /// </summary>
        private bool setSeletedDates;

        /// <summary>
        /// saveSchedule
        /// </summary>
        private bool saveSchedule;
        private bool isNegative = false;
        private decimal negValue;

        private string pCode = string.Empty;
        private string pDescription = string.Empty;

        private string stateConfigured = string.Empty;
        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData ownerDetailDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// DistrictSelectionData instance
        /// </summary>
        private F1512DistrictSelectionData districtSlectionDataset = new F1512DistrictSelectionData();

        /// <summary>
        /// stores a string  value when deleted
        /// </summary>
        private int isdeleted;

        /// <summary>
        /// Stores insert or updated scheduleId
        /// </summary>
        private int scheduleIds;

        /// <summary>
        /// Variable for keyDown
        /// </summary>
        private bool keyDown;

        /// <summary>
        /// saveMsg
        /// </summary>
        private bool saveMsg;

        /// <summary>
        /// loadDetails
        /// </summary>
        private bool loadDetails;

        /// <summary>
        /// newMode
        /// </summary>
        private bool newMode;

        /// <summary>
        /// linkCheck
        /// </summary>
        private bool linkCheck;

        /// <summary>
        /// sortcutNew
        /// </summary>
        private bool sortcutNew;

        /// <summary>
        /// linkclickblockbool
        /// </summary>
        private bool linkclickblockbool;

        /// <summary>
        /// newnavigateId
        /// </summary>
        private bool newnavigateId;

        /// <summary>
        /// schedule
        /// </summary>
        private int schedule;

        /// <summary>
        /// editScheduledata
        /// </summary>
        private F2200EditScheduleData editScheduleassessmentdata = new F2200EditScheduleData();

        /// <summary>
        /// CostFlag
        /// </summary>
        private bool newconstflag = false;

        /// <summary>
        /// ScheduleType DataSet
        /// </summary>
        private F2204CopyScheduleData.f25050_ScheduleTypeDataTable scheduleTypeData = new F2204CopyScheduleData.f25050_ScheduleTypeDataTable();

        /// <summary>
        /// assessmentTypeData Dataset
        /// </summary>
        private F2204CopyScheduleData.f25050_AssessmentTypeDataTable assessmentTypeData = new F2204CopyScheduleData.f25050_AssessmentTypeDataTable();

        /// <summary>
        /// stores the Schedule DataTable
        /// </summary>
        private F2200EditScheduleData.f2200ListScheduleDataTableDataTable getDetails = new F2200EditScheduleData.f2200ListScheduleDataTableDataTable();

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData farmComboData = new CommonData();

        /// <summary>
        /// Flag for FillDate change
        /// </summary>
        private bool fillDateChanged = false;

        /// <summary>
        /// MasterFormNo
        /// </summary>
        private int masterformno;

        /// <summary>
        /// deleteflag
        /// </summary>
        private bool deleteflag = false;
        private bool isNotCustomState = false;
        private decimal O1Value;

        #endregion

        /// <summary>
        ///  F2200()
        /// </summary>
        /// <param name="scheduleIds">ScheduleID</param>
        public F2200(int masterformno, int scheduleIds)
        {
            InitializeComponent();
            this.masterformno = masterformno;
            this.scheduleId = scheduleIds;
        }

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #region Properties

        /// <summary>
        /// F2200Controll
        /// </summary>
        [CreateNew]
        public F2200Controller F2200Controll
        {
            get { return this.form2200Control as F2200Controller; }
            set { this.form2200Control = value; }
        }

        /// <summary>
        /// DeletedRecord
        /// </summary>
        public int DeletedRecord
        {
            get { return this.isdeleted; }
            set { this.isdeleted = value; }
        }

        /// <summary>
        /// SendScheduleID
        /// </summary>
        public int SendScheduleID
        {
            get { return this.scheduleIds; }
            set { this.scheduleIds = value; }
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;
        #endregion

        #region Load Event

        /// <summary>
        /// F2200_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F2200_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadEnabled();
                this.CustomizeCombobox();
                this.LoadFieldDeatails();
                this.LockControl(!this.FormPermissionFields.editPermission);
                this.newDetails = false;
                this.dateSelection = false;
                this.missingReq = false;
                this.saveMsg = false;
                this.loadDetails = true;
                this.sortcutNew = false;
                this.saveSchedule = false;
                this.linkclickblockbool = false;
                this.linkCheck = false;
                this.newnavigateId = false;
                this.setSeletedDates = false;
               // this.PersonalPropertyDescriptionPanel.Enabled = false;
                this.PersonalPropertyDescriptionTextBox.Enabled = false;
                this.SaveButtonMenuItem.Click += new EventHandler(this.ParcelStatusSaveButton_Click);
                //
                this.RollYearTextBox.Enabled = false;


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// EditEnabled
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EditEnabled(object sender, EventArgs e)
        {
            // permission has been implemened for the issue 5184
            if (this.FormPermissionFields.editPermission)
            {
                this.EditScheduleCancelButton.Enabled = true;
                this.EditScheduleSaveButton.Enabled = true;
            }
            // Coding ends here
            this.EditScheduleNewButton.Enabled = false;
            this.EditScheduleDeleteButton.Enabled = false;
            this.CopyButton.Enabled = false;

            this.saveMsg = true;
        }

        /// <summary>
        /// LoadEnabled()
        /// </summary>
        private void LoadEnabled()
        {
            this.EditScheduleCancelButton.Enabled = false;
            this.EditScheduleSaveButton.Enabled = false;
            // permission has been implemened for the issue 5184
            if (this.FormPermissionFields.newPermission)
            {
                this.EditScheduleNewButton.Enabled = true;
            }
            else
            {
                this.EditScheduleNewButton.Enabled = false;
            }

            //if (this.FormPermissionFields.deletePermission)
            //{
            //    this.EditScheduleDeleteButton.Enabled = true;
            //}
            //else
            //{
            //    this.EditScheduleDeleteButton.Enabled = false;
            //}

            if (this.FormPermissionFields.newPermission)
            {
                this.CopyButton.Enabled = true;
            }
            else
            {
                this.CopyButton.Enabled = false;
            }
           // this.RollYearTextBox.Enabled = true;
            // Coding Ends Here
        }

        /// <summary>
        /// This Method used to bind datasource and displaymember
        /// CustomizeCombobox
        /// </summary>
        private void CustomizeCombobox()
        {
            ////which loads yes, no value to the DataSet
            this.reviewComboData.LoadYesNoValue();
            this.ReviewCombo.DataSource = this.reviewComboData.ComboBoxDataTable;
            this.ReviewCombo.ValueMember = this.reviewComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.ReviewCombo.DisplayMember = this.reviewComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.ReviewCombo.SelectedValue = 0;
            this.headOfHouseholdComboBoxdata.LoadYesNoValue();
            this.HeadofHouseholdComboBox.DataSource = this.headOfHouseholdComboBoxdata.ComboBoxDataTable;
            this.HeadofHouseholdComboBox.ValueMember = this.headOfHouseholdComboBoxdata.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.HeadofHouseholdComboBox.DisplayMember = this.headOfHouseholdComboBoxdata.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.HeadofHouseholdComboBox.SelectedValue = 0;
            this.exemptComboBoxdata.LoadYesNoValue();
            this.ExcemptComboBox.DataSource = this.exemptComboBoxdata.ComboBoxDataTable;
            this.ExcemptComboBox.ValueMember = this.exemptComboBoxdata.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.ExcemptComboBox.DisplayMember = this.exemptComboBoxdata.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.ExcemptComboBox.SelectedValue = 0;

            ////Farm ComboBox
            this.farmComboData.LoadYesNoValue();
            this.FarmComboBox.DataSource = this.farmComboData.ComboBoxDataTable;
            this.FarmComboBox.ValueMember = this.farmComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.FarmComboBox.DisplayMember = this.farmComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.FarmComboBox.SelectedValue = 0;
        }

        /// <summary>
        /// FillParcelTypeCombo
        /// </summary>
        private void FillScheduleTypeCombo()
        {
            this.assessmentTypeData = this.form2200Control.WorkItem.F25050GetParcelTypeDetails();
            if (this.assessmentTypeData.Rows.Count > 0)
            {
                this.AssessmentTypeComboBox.DisplayMember = this.assessmentTypeData.AssessmentTypeColumn.ColumnName;
                this.AssessmentTypeComboBox.ValueMember = this.assessmentTypeData.AssessmentTypeIDColumn.ColumnName;
                this.AssessmentTypeComboBox.DataSource = this.assessmentTypeData;
            }

            this.scheduleTypeData = this.form2200Control.WorkItem.F25050GetScheduleTypeDetails();
            if (this.scheduleTypeData.Rows.Count > 0)
            {
                this.ScheduleTypeComboBox.DisplayMember = this.scheduleTypeData.ScheduleTypeColumn.ColumnName;
                this.ScheduleTypeComboBox.ValueMember = this.scheduleTypeData.ScheduleTypeIDColumn.ColumnName;
                this.ScheduleTypeComboBox.DataSource = this.scheduleTypeData;
            }

            if (this.editScheduledata.f2200ListScheduleDataTable.Rows.Count > 0)
            {
                this.ScheduleTypeComboBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ScheduleTypeColumn.ColumnName].ToString();
            }
        }

        /// <summary>
        /// LoadFieldDeatails
        /// </summary>
        private void LoadFieldDeatails()
        {
            this.editScheduledata = this.form2200Control.WorkItem.F2200_ListEditScheduleDetails(this.scheduleId);
            this.FillScheduleTypeCombo();

            if (this.editScheduledata.f2200ListScheduleDataTable.Rows.Count > 0)
            {
                if (this.editScheduledata.f25050_pcget_Configuredstate.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.editScheduledata.f25050_pcget_Configuredstate.Rows[0][this.editScheduledata.f25050_pcget_Configuredstate.ConfiguredStateColumn.ColumnName].ToString()))
                    {
                        this.stateConfigured=this.editScheduledata.f25050_pcget_Configuredstate.Rows[0][this.editScheduledata.f25050_pcget_Configuredstate.ConfiguredStateColumn.ColumnName].ToString();
                        //if ((!this.stateConfigured.ToLower().Equals("ne")))
                        //{
                        //    this.CustomViewPanel.SendToBack();
                        //    this.FarmExpertPanel.SendToBack();
                        //    this.ExpertYearpanel.SendToBack();
                        //    this.ExemptAmountPanel.SendToBack();
                        //    this.isNotCustomState = true;
                        //}
                    }
                    this.EnableControls(true);
                    if (stateConfigured.ToLower().ToString().Equals("ne"))
                    {
                        this.PersonalPropertyDescriptionPanel.Visible = true;
                        this.PersonalPropertyCodePanel.Visible = true;
                        this.NEFieldsDisplayPanel.Visible = true;
                        this.BottomPanel.Location = new System.Drawing.Point(this.BottomPanel.Location.X, 136);
                        this.Height = 530;
                    }
                    else
                    {
                        this.CustomViewPanel.SendToBack();
                        this.FarmExpertPanel.SendToBack();
                        this.ExpertYearpanel.SendToBack();
                        this.ExemptAmountPanel.SendToBack();
                        this.isNotCustomState = true;

                        this.PersonalPropertyDescriptionPanel.Visible = false;
                        this.PersonalPropertyCodePanel.Visible = false;
                        this.NEFieldsDisplayPanel.Visible = false;
                        this.BottomPanel.Location = new System.Drawing.Point(this.BottomPanel.Location.X,96);
                        this.Height=485;
                    }

                    if (!string.IsNullOrEmpty(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.OwnerIDColumn.ColumnName].ToString()))
                    {
                        this.selectedownerId = Convert.ToInt32(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.OwnerIDColumn.ColumnName].ToString());
                    }

                    if (!string.IsNullOrEmpty(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ParcelIDColumn.ColumnName].ToString()))
                    {
                        this.parcelId = Convert.ToInt32(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ParcelIDColumn.ColumnName].ToString());
                    }

                    EditSchedulelIDLinkLabel.Text = "tAA_Schedule[ScheduleID] " + this.scheduleId;
                    this.districtId = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.DistrictIDColumn.ColumnName].ToString();
                    this.scheduleId = Convert.ToInt32(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ScheduleIDColumn.ColumnName].ToString());
                    this.ScheduleNumberTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ScheduleNumberColumn.ColumnName].ToString();
                    this.RollYearTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.RollYearColumn.ColumnName].ToString();
                    this.ReviewCombo.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.IsReviewColumn.ColumnName].ToString();

                    ////Assigning O1value to Local varible to display message box....... purushotham on 14/07/2014
                    decimal.TryParse(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.O1ValueColumn.ColumnName].ToString(), out O1Value);


                    if (isNotCustomState)
                    {
                        this.HeadofHouseholdComboBox.SelectedValue = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.IsHeadOfHouseholdColumn.ColumnName].ToString();
                        //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.
                        this.ExcemptComboBox.SelectedValue = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.IsExemptColumn.ColumnName].ToString();
                        this.FarmComboBox.SelectedValue = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.IsFarmColumn.ColumnName].ToString();
                    }
                    else
                    {
                        this.CustomViewPanel.BringToFront();
                        this.CustomViewPanel.BringToFront();
                        this.FarmExpertPanel.BringToFront();
                        this.ExpertYearpanel.BringToFront();
                        this.ExemptAmountPanel.BringToFront();
                        if (this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.IsFarmExemptColumn.ColumnName].ToString().ToLower().Equals("false"))
                        {
                            this.FarmExpertComboBox.SelectedItem = "No";
                        }
                        else
                        {
                            this.FarmExpertComboBox.SelectedItem = "Yes";
                        }

                        string exComboVal = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.Is259ExemptColumn.ColumnName].ToString();
                        if (exComboVal.Equals("0") || exComboVal.Equals(""))
                        {
                            this.Ex259ComboBox.SelectedItem = "No";
                        }
                        else 
                        {
                            this.Ex259ComboBox.SelectedItem = "Yes";
                        }

                        if (!string.IsNullOrEmpty(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.Exempt259AmountColumn.ColumnName].ToString()))
                        {
                            var strTemp = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.Exempt259AmountColumn.ColumnName].ToString();
                            double strNumb = Convert.ToDouble(strTemp);
                            var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                            this.ExAmt259TextBox.Text = strResult;
                        }
                        else
                        {
                            this.ExAmt259TextBox.Text = string.Empty;
                        }


                        if (!string.IsNullOrEmpty(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.FarmExemptYearColumn.ColumnName].ToString()))
                        {
                            this.ExemptYearTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.FarmExemptYearColumn.ColumnName].ToString();
                        }
                        else
                        {
                            this.ExemptYearTextBox.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.FarmExemptAmountColumn.ColumnName].ToString()))
                        {
                            var strTemp = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.FarmExemptAmountColumn.ColumnName].ToString();
                            double strNumb = Convert.ToDouble(strTemp);
                            var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                            this.ExemptAmountTextBox.Text = strResult;
                        }
                        else
                        {
                            this.ExemptAmountTextBox.Text = string.Empty;
                        }
                    }
                    this.propertyTypeTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PropertyTypeColumn.ColumnName].ToString();
                    this.NaicsTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.NAICSColumn.ColumnName].ToString();
                    this.FillingDateTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.FilingDateColumn.ColumnName].ToString();
                    this.DescriptionTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.DescriptionColumn.ColumnName].ToString();
                    // this.ScheduleOwnerLinkLabel.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PrimaryOwnerColumn.ColumnName].ToString();
                    /// Issue fixed for TSBG 13002
                    string ScheduleOwnerField = string.Empty;
                    ScheduleOwnerField = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PrimaryOwnerColumn.ColumnName].ToString();
                    if (ScheduleOwnerField.Contains("&"))
                    {
                        ScheduleOwnerField = ScheduleOwnerField.Replace("&", "&&");
                    }
                    this.ScheduleOwnerLinkLabel.Text = ScheduleOwnerField;
                    this.StreetAddressTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.StreetAddressColumn.ColumnName].ToString();
                    this.BusinessNameTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.BuisnessNameColumn.ColumnName].ToString();
                    this.ParcelReferenceTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ParcelNumberColumn.ColumnName].ToString();
                    this.DistrictTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.DistrictColumn.ColumnName].ToString();
                    this.ScheduleNumberlabel1.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ScheduleNumberColumn.ColumnName].ToString();
                    this.RollYearLabel1.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.RollYearColumn.ColumnName].ToString();

                    this.PenaltyAmountTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PenaltyAmountColumn.ColumnName].ToString();

                    this.PenaltyPercentTextBox.TextCustomFormat = "";
                    this.PenaltyPercentTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PenaltyPercentColumn.ColumnName].ToString();
                    this.Customformat();
                    this.PenaltyPercentTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PenaltyPercentColumn.ColumnName].ToString(); ;
                    ////this.editScheduleassessmentdata = this.f2200Control.WorkItem.F2200_GetAssessmentTypeDetails("Schedule");
                    ////this.editScheduleassessmentdata = this.f2200Control.WorkItem.F25050GetParcelTypeDetails();
                    ////if (this.editScheduleassessmentdata.f25050GetScheduleParcelDetailsTable.Rows.Count > 0)
                    ////{
                    ////    this.AssessmentTypeComboBox.DataSource = this.editScheduleassessmentdata.List_AssessmentTypeDataTable;
                    ////    this.AssessmentTypeComboBox.DisplayMember = this.editScheduleassessmentdata.List_AssessmentTypeDataTable.AssessmentTypeColumn.ColumnName;
                    ////    this.AssessmentTypeComboBox.ValueMember = this.editScheduleassessmentdata.List_AssessmentTypeDataTable.AssessmentTypeIDColumn.ColumnName;
                    ////}

                    this.AssessmentTypeComboBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.AssessmentTypeColumn.ColumnName].ToString();
                    this.NewConstrctionTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.NewConstructionColumn.ColumnName].ToString();
                    //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.
                    if (!string.IsNullOrEmpty(this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ScheduleStateCodeColumn.ColumnName].ToString()))
                    {
                        ///Issue fixed for the TSCO 13002
                        string ScheduleDORfield = string.Empty;
                        ScheduleDORfield = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ScheduleStateCodeColumn.ColumnName].ToString();
                        if (ScheduleDORfield.Contains("&"))
                        {
                            ScheduleDORfield = ScheduleDORfield.Replace("&", "&&");
                        }
                        this.ScheduleDORlinkLabel.Text = ScheduleDORfield;

                        //this.ScheduleDORlinkLabel.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.ScheduleStateCodeColumn.ColumnName].ToString();
                    }
                    else
                    {
                        this.ScheduleDORlinkLabel.Text = "<<>>";
                    }
                    if(this.stateConfigured.ToLower().ToString().Equals("ne"))
                    {
                    this.PersonalPropertyCodeTextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PersonalPropertyCodeColumn.ColumnName].ToString();
                    this.PersonalPropertyDescriptionTextBox.Text=this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.PersonalPropertyCodeDescriptionColumn.ColumnName].ToString();
                    }
                    this.ID1label.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MIDLabel1Column.ColumnName].ToString();
                    this.ID2label.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MIDLabel2Column.ColumnName].ToString();
                    this.ID3label.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MIDLabel3Column.ColumnName].ToString();
                    this.ID4label.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MIDLabel4Column.ColumnName].ToString();
                    this.ID5label.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MIDLabel5Column.ColumnName].ToString();
                    this.ID1TextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MID1Column.ColumnName].ToString();
                    this.ID2TextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MID2Column.ColumnName].ToString();
                    this.ID3TextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MID3Column.ColumnName].ToString();
                    this.ID4TextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MID4Column.ColumnName].ToString();
                    this.ID5TextBox.Text = this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.MID5Column.ColumnName].ToString();
                    this.ScheduleNumberTextBox.Focus();
                }
                else
                {
                    this.ClearDeatails();
                    this.EnableControls(false);
                }
                this.getDetails = editScheduledata.f2200ListScheduleDataTable;
                if (this.FormPermissionFields.deletePermission && this.editScheduledata.f2200ListScheduleDataTable.Rows.Count > 0)
                {
                    int lockSchedule = getDetails[0].LockScheduleBy;
                    if (lockSchedule == null || lockSchedule == 0)
                    {
                        this.EditScheduleDeleteButton.Enabled = true;
                    }
                    else
                    {
                        this.EditScheduleDeleteButton.Enabled = false;
                    }
                }
                else
                {
                    this.EditScheduleDeleteButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void SetSeletedDate(string selectedDate)
        {
            this.FillingDateTextBox.Text = selectedDate;
            this.dateSelection = true;
            this.setSeletedDates = true;
            if (this.FormPermissionFields.editPermission)
            {
                this.EditScheduleCancelButton.Enabled = true;
                this.EditScheduleSaveButton.Enabled = true;
                this.fillDateChanged = true;
            }

            this.EditScheduleNewButton.Enabled = false;
            this.EditScheduleDeleteButton.Enabled = false;
            this.CopyButton.Enabled = false;


            this.saveMsg = true;
            ////this.shiftTab = true;
            FillingDateTextBox.Focus();
            this.TimeMonthCalender.Visible = false;
        }

        /// <summary>
        /// Shows the license expiration date.
        /// </summary>
        private void ShowFillingDate()
        {
            TimeMonthCalender.Enabled = true;
            this.TimeMonthCalender.Visible = true;
            this.TimeMonthCalender.Left = this.FillingDatepanel.Left+160;// +this.ActionDateButton.Left + this.ActionDateButton.Width;
            this.TimeMonthCalender.Top = this.FillingDatepanel.Top + this.FillingDatebutton.Bottom;
            this.TimeMonthCalender.BringToFront();
            this.TimeMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.FillingDateTextBox.Text))
            {
                this.TimeMonthCalender.SetDate(Convert.ToDateTime(this.FillingDateTextBox.Text));
            }
            else
            {
                this.TimeMonthCalender.SetDate(DateTime.Today);
            }
        }

        /// <summary>
        /// ClearDeatails()
        /// </summary>
        private void ClearDeatails()
        {
            this.ScheduleNumberTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.propertyTypeTextBox.Text = string.Empty;
            ////this.ReviewCombo.SelectedIndex = -1;
            this.NaicsTextBox.Text = string.Empty;
            this.FillingDateTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.ScheduleOwnerLinkLabel.Text = string.Empty;
            this.StreetAddressTextBox.Text = string.Empty;
            this.BusinessNameTextBox.Text = string.Empty;
            this.ParcelReferenceTextBox.Text = string.Empty;
            this.DistrictTextBox.Text = string.Empty;
            this.ScheduleNumberlabel1.Text = string.Empty;
            this.RollYearLabel1.Text = string.Empty;
            this.NewConstrctionTextBox.Text = string.Empty;
            this.PenaltyAmountTextBox.Text = string.Empty;
            this.PenaltyPercentTextBox.TextCustomFormat = "";
            this.PenaltyPercentTextBox.Text = string.Empty;
            //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.
            this.ID1TextBox.Text = string.Empty;
            this.ID2TextBox.Text = string.Empty;
            this.ID3TextBox.Text = string.Empty;
            this.ID4TextBox.Text = string.Empty;
            this.ID5TextBox.Text = string.Empty;
            this.ScheduleDORlinkLabel.Text = "<<>>";
            ////this.HeadofHouseholdComboBox.SelectedValue = 1;

            //Added to implement Personal Code CO
            this.PersonalPropertyDescriptionTextBox.Text = string.Empty;
            this.PersonalPropertyCodeTextBox.Text = string.Empty;
            this.label1.Visible = false;
            this.newconstflag = false;
        }

        /// <summary>
        /// ConvertStringToInteger
        /// </summary>
        /// <param name="stringValue">stringValue</param>
        /// <returns>integer</returns>
        private int ConvertStringToInteger(string stringValue)
        {
            this.outValue = 0;
            if (!string.IsNullOrEmpty(stringValue.Trim()))
            {
                int.TryParse(stringValue.Trim(), out this.outValue);
            }

            return this.outValue;
        }

        /// <summary>
        /// Gets the parcel details.
        /// </summary>
        /// <param name="parcelIdentity">The parcel identity.</param>
        private void GetParcelDetails(int parcelIdentity)
        {
            //// SP Changed as per CO:4698 by A.Shanmugasundaram
            F2200EditScheduleData.f25050GetScheduleParcelDetailsTableDataTable parcelDetailsDataTable = new F2200EditScheduleData.f25050GetScheduleParcelDetailsTableDataTable();
            parcelDetailsDataTable = this.form2200Control.WorkItem.F2200_GetDistrictAssessmentParcelId(string.Empty, parcelIdentity).f25050GetScheduleParcelDetailsTable;

            if (parcelDetailsDataTable.Rows.Count > 0)
            {
                //// Fill Property Info
                this.ParcelReferenceTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelNumberColumn.ColumnName].ToString();
                ////  this.TypeTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.PostNameColumn.ColumnName].ToString();
                this.parcelId = Convert.ToInt32(parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelIDColumn.ColumnName].ToString());
            }
        }

        private void EnableControls(bool enable)
        {
            this.ScheduleNumberPanel.Enabled = enable;
            this.RollYearpanel.Enabled = enable;
            this.Reviewpanel.Enabled = enable;
            this.ExemptPanel.Enabled = enable;
            this.HeadOfHouseholdpanel.Enabled = enable;
            this.FarmPanel.Enabled = enable;
            this.Assessmenttypepanel.Enabled = enable;
            this.ScheduleTypePanel.Enabled = enable;
            this.propertyTypepanel.Enabled = enable;
            this.NaicsPanel.Enabled = enable;
            this.FillingDatepanel.Enabled = enable;
            this.Descriptionpanel.Enabled = enable;
            this.ScheduleOwnerpanel.Enabled = enable;
            this.StreetAddresspanel.Enabled = enable;
            this.BusinessNamepanel.Enabled = enable;
            this.ParcelReferencePanel.Enabled = enable;
            this.Districtpanel.Enabled = enable;
            this.NewConstructionPanel.Enabled = enable;
            this.PenaltyAmountPanel.Enabled = enable;
            this.PenaltyPercentPanel.Enabled = enable;
            this.DORpanel10.Enabled = enable;
            this.ID1panel11.Enabled = enable;
            this.ID2panel12.Enabled = enable;
            this.ID3panel13.Enabled = enable;
            this.ID4panel14.Enabled = enable;
            this.ID5panel15.Enabled = enable;

            this.PersonalPropertyCodeTextBox.Enabled = enable;
            this.PersonalPropertyDescriptionTextBox.Enabled = enable;
        }

        private void LockControl(bool lockKey)
        {
            this.ScheduleNumberTextBox.LockKeyPress = lockKey;
            this.RollYearTextBox.LockKeyPress = lockKey;
            this.propertyTypeTextBox.LockKeyPress = lockKey;
            this.NaicsTextBox.LockKeyPress = lockKey;
            this.FillingDateTextBox.LockKeyPress = lockKey;
            this.DescriptionTextBox.LockKeyPress = lockKey;
            //this.ScheduleOwnerLinkLabel.LockKeyPress = lockKey;
            this.StreetAddressTextBox.LockKeyPress = lockKey;
            this.BusinessNameTextBox.LockKeyPress = lockKey;
            this.ParcelReferenceTextBox.LockKeyPress = lockKey;
            this.DistrictTextBox.LockKeyPress = lockKey;
            this.NewConstrctionTextBox.LockKeyPress = lockKey;
            this.PenaltyAmountTextBox.LockKeyPress = lockKey;
            this.PenaltyPercentTextBox.LockKeyPress = lockKey;
            this.ID1TextBox.Enabled = !lockKey;
            this.ID2TextBox.Enabled = !lockKey;
            this.ID3TextBox.Enabled = !lockKey;
            this.ID4TextBox.Enabled = !lockKey;
            this.ID5TextBox.Enabled = !lockKey;

            this.ExcemptComboBox.Enabled = !lockKey;
            this.ReviewCombo.Enabled = !lockKey;
            this.HeadofHouseholdComboBox.Enabled = !lockKey;
            this.FarmComboBox.Enabled = !lockKey;
            this.AssessmentTypeComboBox.Enabled = !lockKey;
            this.ScheduleTypeComboBox.Enabled = !lockKey;

            this.ParcelReferencebutton.Enabled = !lockKey;
            this.ScheduleOwnerPicture.Enabled = !lockKey;
            this.FillingDatebutton.Enabled = !lockKey;
            this.Districbutton.Enabled = !lockKey;

            this.PersonalPropertyDescriptionTextBox.Enabled = !lockKey;
            this.PersonalPropertyCodeTextBox.Enabled = !lockKey;
        }

        /// <summary>
        /// InsertOrUpdateContractDetails()
        /// </summary>
        private void InsertOrUpdateContractDetails()
        {
            ////Added By Ramya
            F2200EditScheduleData editScheduleInt32Data = new F2200EditScheduleData();
            editScheduleInt32Data.f2200ListScheduleDataTable.Clear();
            F2200EditScheduleData.f2200ListScheduleDataTableRow editscheduleRow = editScheduleInt32Data.f2200ListScheduleDataTable.Newf2200ListScheduleDataTableRow();

            ////this.editScheduledata.f2200ListScheduleDataTable.Clear();
            ////F2200EditScheduleData.f2200ListScheduleDataTableRow editscheduleRow = this.editScheduledata.f2200ListScheduleDataTable.Newf2200ListScheduleDataTableRow();         
            if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()))
            {
                editscheduleRow.DistrictID = Convert.ToInt32(this.districtId);
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F2200RequiredFields"), "TerraScan-Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.missingReq = true;
                this.linkCheck = true;
                this.DistrictTextBox.Focus();
                //this.Districbutton.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(this.ScheduleNumberTextBox.Text.Trim()))
            {
                editscheduleRow.ScheduleNumber = ScheduleNumberTextBox.Text.Trim();
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F2200RequiredFields"), "TerraScan-Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.missingReq = true;
                this.linkCheck = true;
                this.ScheduleNumberTextBox.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                int yearval;
                int.TryParse(this.RollYearTextBox.Text.Trim(), out yearval);
                if (yearval <= 1899 || yearval >= 2080)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F2200Rollyear"), "TerraScan-Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.RollYearTextBox.Text = "0";
                    this.RollYearTextBox.Focus();
                    this.EditScheduleCancelButton.Enabled = true;
                    this.EditScheduleSaveButton.Enabled = true;

                    this.EditScheduleNewButton.Enabled = false;

                    this.EditScheduleDeleteButton.Enabled = false;

                    this.CopyButton.Enabled = false;

                    this.missingReq = true;
                    this.linkCheck = true;
                    return;
                }
                else
                {
                    editscheduleRow.RollYear = this.RollYearTextBox.Text.Trim();
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F2200RequiredFields"), "TerraScan-Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.missingReq = true;
                this.linkCheck = true;
                this.RollYearTextBox.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(this.NewConstrctionTextBox.Text.Trim()))
            {
                System.Text.Encoding ascii = System.Text.Encoding.ASCII;
                Byte[] encodedBytes = ascii.GetBytes(this.NewConstrctionTextBox.DecimalTextBoxValue.ToString());
                foreach (Byte b in encodedBytes)
                {
                    if ((b.Equals(44)) || (b >= 48) && (b <= 57))
                    {
                    }
                    else
                    {
                        this.NewConstrctionTextBox.Text = string.Empty;
                    }
                }
            }

            editscheduleRow.IsReview = ReviewCombo.SelectedValue.ToString();

            editscheduleRow.IsHeadOfHousehold = Convert.ToInt16(this.HeadofHouseholdComboBox.SelectedValue.ToString());
            //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.
            editscheduleRow.IsExempt = Convert.ToInt16(this.ExcemptComboBox.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(this.propertyTypeTextBox.Text.Trim()))
            {
                editscheduleRow.PropertyType = this.propertyTypeTextBox.Text.Trim();
            }
            else
            {
                editscheduleRow.PropertyType = string.Empty;
            }
            //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.

            editscheduleRow.IsFarm = Convert.ToInt16(this.FarmComboBox.SelectedValue.ToString());

            if (this.ID1TextBox.Text.Length > 50)
            {
                editscheduleRow.MID1 = this.ID1TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                editscheduleRow.MID1 = this.ID1TextBox.Text.Trim();
            }

            if (this.ID2TextBox.Text.Length > 50)
            {
                editscheduleRow.MID2 = this.ID2TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                editscheduleRow.MID2 = this.ID2TextBox.Text.Trim();
            }

            if (this.ID3TextBox.Text.Length > 50)
            {
                editscheduleRow.MID3 = this.ID3TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                editscheduleRow.MID3 = this.ID3TextBox.Text.Trim();
            }

            if (this.ID4TextBox.Text.Length > 50)
            {
                editscheduleRow.MID4 = this.ID4TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                editscheduleRow.MID4 = this.ID4TextBox.Text.Trim();
            }

            if (this.ID5TextBox.Text.Length > 50)
            {
                editscheduleRow.MID5 = this.ID5TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                editscheduleRow.MID5 = this.ID5TextBox.Text.Trim();
            }

            if (this.ScheduleDORlinkLabel.Text.StartsWith("<"))
            {
                editscheduleRow.ScheduleStateCode = string.Empty;
            }
            else
            {
                editscheduleRow.ScheduleStateCode = this.ScheduleDORlinkLabel.Text.Trim().Replace("&&", "&");
            }

            if (!string.IsNullOrEmpty(this.NaicsTextBox.Text.Trim()))
            {
                editscheduleRow.NAICS = this.NaicsTextBox.Text.Trim();
            }
            else
            {
                editscheduleRow.NAICS = string.Empty;
            }

            if (string.IsNullOrEmpty(this.FillingDateTextBox.Text.Trim()))
            {
                editscheduleRow.FilingDate = string.Empty;
            }
            else
            {
                editscheduleRow.FilingDate = this.FillingDateTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                editscheduleRow.Description = this.DescriptionTextBox.Text.Trim();
            }
            else
            {
                editscheduleRow.Description = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.ScheduleOwnerLinkLabel.Text.Trim()))
            {
                editscheduleRow.OwnerID = this.selectedownerId;
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F2200ScheduleOwnerRequiredFields"), "TerraScan-Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ScheduleOwnerPicture.Focus();
                this.missingReq = true;
                this.linkCheck = true;
                return;
            }

            if (!string.IsNullOrEmpty(this.StreetAddressTextBox.Text.Trim()))
            {
                editscheduleRow.StreetAddress = this.StreetAddressTextBox.Text.Trim();
            }
            else
            {
                editscheduleRow.StreetAddress = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.BusinessNameTextBox.Text.Trim()))
            {
                editscheduleRow.BuisnessName = this.BusinessNameTextBox.Text.Trim();
            }
            else
            {
                editscheduleRow.BuisnessName = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.ParcelReferenceTextBox.Text.Trim()))
            {
                editscheduleRow.ParcelID = this.parcelId;
            }
            else
            {
                editscheduleRow.ParcelID = 0;
            }
            ///Commented for the form District Validation in the field - TSCO  11748 

            //if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()))
            //{
            //    editscheduleRow.DistrictID = Convert.ToInt32(this.districtId);
            //}
            //else
            //{
            //    editscheduleRow.DistrictID = 0;
            //}

            if (!string.IsNullOrEmpty(this.AssessmentTypeComboBox.Text.Trim()))
            {
                editscheduleRow.AssessmentTypeID = Convert.ToInt16(this.AssessmentTypeComboBox.SelectedValue);
            }
            else
            {
                editscheduleRow.AssessmentTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.ScheduleTypeComboBox.Text.Trim()))
            {
                editscheduleRow.ScheduleTypeID = Convert.ToInt16(this.ScheduleTypeComboBox.SelectedValue);
            }
            else
            {
                editscheduleRow.ScheduleTypeID = 0;
            }

            if (!string.IsNullOrEmpty(this.NewConstrctionTextBox.Text.Trim()))
            {
                editscheduleRow.NewConstruction = decimal.Parse(this.NewConstrctionTextBox.DecimalTextBoxValue.ToString());
            }
            else
            {
                editscheduleRow.NewConstruction = 0;
            }

            if (!string.IsNullOrEmpty(this.PenaltyAmountTextBox.Text.Trim()))
            {
                editscheduleRow.PenaltyAmount = this.PenaltyAmountTextBox.DecimalTextBoxValue;
            }
            else
            {
                editscheduleRow.PenaltyAmount = 0;
            }

            ////khaja implemented Money Validation to fix Bug# 5186
            decimal maxPenaltyAmount = 922337203685477.5807M;
            if (this.PenaltyAmountTextBox.DecimalTextBoxValue > maxPenaltyAmount)
            {
                string errorMessage = SharedFunctions.GetResourceString("2200PenaltyAmountValidation");
                MessageBox.Show(errorMessage, "Invalid Penalty Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.PenaltyAmountTextBox.Focus();
                this.EditScheduleCancelButton.Enabled = true;
                this.EditScheduleSaveButton.Enabled = true;
                if (this.FormPermissionFields.newPermission)

                    this.EditScheduleNewButton.Enabled = false;

                this.EditScheduleDeleteButton.Enabled = false;

                this.CopyButton.Enabled = false;

                this.missingReq = true;
                this.linkCheck = true;
                return;
            }

            decimal maxPercentValue = 100;
            if (maxPercentValue < this.PenaltyPercentTextBox.DecimalTextBoxValue)
            {
                string errorMessage = String.Concat(this.PenaltyPercentTextBox.Tag, " ", SharedFunctions.GetResourceString("InvalidRate"), "\n");
                MessageBox.Show(errorMessage, "Invalid Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.PenaltyPercentTextBox.Focus();
                this.EditScheduleCancelButton.Enabled = true;
                this.EditScheduleSaveButton.Enabled = true;

                this.EditScheduleNewButton.Enabled = false;

                this.EditScheduleDeleteButton.Enabled = false;

                this.CopyButton.Enabled = false;

                this.missingReq = true;
                this.linkCheck = true;
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.PenaltyPercentTextBox.Text.Trim()))
                {
                    editscheduleRow.PenaltyPercent = this.PenaltyPercentTextBox.DecimalTextBoxValue;
                }
                else
                {
                    editscheduleRow.PenaltyPercent = 0;
                }
            }

            if ((this.FarmExpertComboBox.SelectedIndex!=-1))
            {
                if (this.FarmExpertComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
                {
                    editscheduleRow.IsFarmExempt = true;
                }
                else
                {
                    editscheduleRow.IsFarmExempt = false;
                }
            }

            if ((this.Ex259ComboBox.SelectedIndex != -1))
            {
                if (this.Ex259ComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
                {
                    editscheduleRow.Is259Exempt = 1;
                }
                else
                {
                    editscheduleRow.Is259Exempt = 0;
                }
            }

            if (!string.IsNullOrEmpty(this.ExAmt259TextBox.Text))
            {
                decimal ex259Amount = 0;
                decimal.TryParse(this.ExAmt259TextBox.Text,out ex259Amount);
                editscheduleRow.Exempt259Amount = ex259Amount;
            }

            if (!string.IsNullOrEmpty(this.ExemptYearTextBox.Text))
            {
                Int16 tempYear = 0;
                Int16.TryParse(this.ExemptYearTextBox.Text, out tempYear);
                editscheduleRow.FarmExemptYear = tempYear;
                //editscheduleRow.FarmExemptYear = 0;
            }
            if (!string.IsNullOrEmpty(this.ExemptYearTextBox.Text))
            {
                decimal temptAmt = 0;
                decimal.TryParse(this.ExemptAmountTextBox.Text, out temptAmt);
                editscheduleRow.FarmExemptAmount = temptAmt;
            }
            if (this.NewConstrctionTextBox.DecimalTextBoxValue > maxPenaltyAmount)
            {
                MessageBox.Show("New Construction value exceeded maximum limit.", "Invalid New Construction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.NewConstrctionTextBox.Focus();
                this.EditScheduleCancelButton.Enabled = true;
                this.EditScheduleSaveButton.Enabled = true;

                this.EditScheduleNewButton.Enabled = false;

                this.EditScheduleDeleteButton.Enabled = false;

                this.CopyButton.Enabled = false;

                this.missingReq = true;
                this.linkCheck = true;
                return;
            }
            if(!string.IsNullOrEmpty(this.PersonalPropertyCodeTextBox.Text.Trim()))
            {
                editscheduleRow.PersonalPropertyCode = this.PersonalPropertyCodeTextBox.Text.Trim();
                editscheduleRow.PersonalPropertyCodeDescription = this.PersonalPropertyDescriptionTextBox.Text;
            }
            editScheduleInt32Data.f2200ListScheduleDataTable.Rows.Add(editscheduleRow);

            if (this.newDetails)
            {
                this.sortcutNew = true;
                F2200EditScheduleData.ListOutputValueDataTable outputValue = new F2200EditScheduleData.ListOutputValueDataTable();
                outputValue = this.form2200Control.WorkItem.F2200_InsertEditSchedule(null, TerraScanCommon.GetXmlString(editScheduleInt32Data.f2200ListScheduleDataTable.Copy()), TerraScan.Common.TerraScanCommon.UserId).ListOutputValue;
                if (outputValue.Rows[0]["IsError"].ToString().Equals("True"))
                {
                    MessageBox.Show(outputValue.Rows[0]["Message"].ToString(), "Record Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.newMode = true;
                    this.missingReq = true;
                    this.saveSchedule = true;
                }
                else
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("NewSaveEditSchedule"), "TerraScan – Navigate to New Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.label1.Visible = true;
                        int.TryParse(outputValue.Rows[0]["PrimaryKeyID"].ToString(), out this.scheduleIds);
                        int.TryParse(outputValue.Rows[0]["PrimaryKeyID"].ToString(), out this.schedule);
                        this.newnavigateId = true;
                        this.linkCheck = false;
                        this.saveSchedule = true;
                        //if (this.schedule != -99)
                        //{
                        //    SliceReloadActiveRecord sliceReloadActiveRecord;
                        //    sliceReloadActiveRecord.MasterFormNo = this.masterformno;
                        //    sliceReloadActiveRecord.SelectedKeyId = this.schedule;
                        //    this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        //}
                    }

                    else
                    {
                        this.label1.Visible = true;
                        //int.TryParse(outputValue.Rows[0]["PrimaryKeyID"].ToString(), out this.scheduleId); 
                        //this.scheduleId = this.scheduleIds;
                        this.newDetails = false;
                        this.LoadFieldDeatails();
                        this.newMode = true;
                        this.missingReq = true;
                        this.saveSchedule = true;
                        this.RollYearTextBox.Enabled = false;
                    }
                }
            }
            else
            {
                this.scheduleIds = this.form2200Control.WorkItem.F2200_UpdateEditSchedule(this.scheduleId, TerraScanCommon.GetXmlString(editScheduleInt32Data.f2200ListScheduleDataTable.Copy()), TerraScan.Common.TerraScanCommon.UserId);
                this.LoadFieldDeatails();
                this.newMode = true;
                this.missingReq = true;
                this.saveSchedule = true;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            D9030_F9030_LoadSliceDetails(this, eventArgs);
        }

        #endregion

        #region Events

        /// <summary>
        /// ParcelStatusNewButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.newPermission)
                {

                    this.ClearDeatails();
                   

                    this.newDetails = true;
                    this.ScheduleNumberTextBox.Focus();
                    ////if (this.FormPermissionFields.editPermission)
                    ////{
                    this.EditScheduleCancelButton.Enabled = true;
                    this.EditScheduleSaveButton.Enabled = true;
                    ////}
                    this.EditScheduleNewButton.Enabled = false;
                    this.EditScheduleDeleteButton.Enabled = false;
                    this.CopyButton.Enabled = false;

                    EditSchedulelIDLinkLabel.Text = "tAA_Schedule[ScheduleID] ";
                    EditSchedulelIDLinkLabel.Enabled = false;
                    this.ReviewCombo.SelectedValue = 0;
                    if (isNotCustomState)
                    {
                        this.CustomViewPanel.SendToBack();
                        this.CustomViewPanel.SendToBack();
                        this.FarmExpertPanel.SendToBack();
                        this.ExpertYearpanel.SendToBack();
                        this.ExemptAmountPanel.SendToBack();
                        this.HeadofHouseholdComboBox.SelectedValue = 0;
                        //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.
                        this.ExcemptComboBox.SelectedValue = 0;
                        this.FarmComboBox.SelectedValue = 0;
                    }
                    else
                    {
                        this.CustomViewPanel.BringToFront();
                        this.CustomViewPanel.BringToFront();
                        this.FarmExpertPanel.BringToFront();
                        this.ExpertYearpanel.BringToFront();
                        this.ExemptAmountPanel.BringToFront();
                        this.FarmExpertComboBox.SelectedItem = "No";
                        this.Ex259ComboBox.SelectedItem = "No";
                        this.ExemptAmountTextBox.Text = string.Empty;
                        this.ExemptYearTextBox.Text = string.Empty;
                    }
                    this.LockControl(false);
                    this.EnableControls(true);
                    this.PersonalPropertyDescriptionTextBox.Enabled = false;

                    //
                    this.RollYearTextBox.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelStatusDeleteButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.deletePermission)
                {
                    if (this.editScheduledata.f2200ListScheduleDataTable.Rows.Count > 0)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteEditSchedule"), "TerraScan – Delete Schedule", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.form2200Control.WorkItem.F2200_DeleteEditSchedule(this.scheduleId, TerraScan.Common.TerraScanCommon.UserId);
                            //// MessageBox.Show("Deleted");
                            this.ClearDeatails();
                            this.LoadEnabled();
                            this.label1.Visible = false;
                            this.DialogResult = DialogResult.OK;
                            this.isdeleted = 1;
                        }
                        else
                        {
                            this.deleteflag = true;
                            this.LoadFieldDeatails();
                            this.deleteflag = false;
                        }
                    }
                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// ParcelStatusSaveButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusSaveButton_Click(object sender, EventArgs e)
            {
            try
            {
                this.ActiveControl = this.EditScheduleSaveButton;
                ////Added by Biju on 24-Sep-2009 to fix the issue after giving invalid date and CTRL+S is pressed,
                ////date is validated, but record is saved.
                if (this.ActiveControl.Name.Equals(FillingDateTextBox.Name))
                {
                    if (!((TerraScan.UI.Controls.TerraScanTextBox)this.ActiveControl).IsValidData)
                    {
                        return;
                    }
                }
                ////till here
                if (this.ActiveControl != null && this.ActiveControl.CausesValidation)
                {
                    this.InsertOrUpdateContractDetails();
                    this.LoadEnabled();
                    this.saveMsg = true;
                    if (!this.missingReq)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.EditScheduleCancelButton.Enabled = true;
                        this.EditScheduleSaveButton.Enabled = true;

                        this.EditScheduleNewButton.Enabled = false;

                        this.EditScheduleDeleteButton.Enabled = false;

                        this.CopyButton.Enabled = false;

                    }

                    if (this.newMode)
                    {
                        this.EditScheduleCancelButton.Enabled = false;
                        this.EditScheduleSaveButton.Enabled = false;
                        // permission has been implemened for the issue 5184
                        if (this.FormPermissionFields.newPermission)
                        {
                            this.EditScheduleNewButton.Enabled = true;
                        }
                        else
                        {
                            this.EditScheduleNewButton.Enabled = false;
                        }
                        if (this.FormPermissionFields.deletePermission)
                        {
                            this.EditScheduleDeleteButton.Enabled = true;
                        }
                        else
                        {
                            this.EditScheduleDeleteButton.Enabled = false;
                        }
                        if (this.FormPermissionFields.newPermission)
                        {
                            this.CopyButton.Enabled = true;
                        }
                        else
                        {
                            this.CopyButton.Enabled = false;
                        }
                        // Coding Ends here
                        this.newMode = false;
                    }

                    this.missingReq = false;
                }
                this.PersonalPropertyDescriptionTextBox.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// FillingDatebutton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FillingDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowFillingDate();
                this.isshift = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// TimeMonthCalender_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimeMonthCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                TimeMonthCalender.Visible = false;
                if (!this.dateSelection)
                {
                    if (this.isshift)
                    {
                        this.FillingDateTextBox.Focus();
                    }
                    else
                    {
                        this.PenaltyAmountTextBox.Focus();
                    }

                    this.dateSelection = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// TimeMonthCalender_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimeMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.editPermission)
                {
                    this.EditScheduleCancelButton.Enabled = true;
                    this.EditScheduleSaveButton.Enabled = true;
                }
                this.EditScheduleNewButton.Enabled = false;
                this.EditScheduleDeleteButton.Enabled = false;
                this.CopyButton.Enabled = false;

                this.saveMsg = true;
                string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.TimeMonthCalender.SelectionStart.ToString(dateFormat));
                }

                this.isshift = e.Shift;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// TimeMonthCalender_DateSelected
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimeMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start.ToShortDateString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ScheduleOwnerPicture_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleOwnerPicture_Click(object sender, EventArgs e)
        {
            try
            {
                Form parcelF9101 = new Form();
                parcelF9101 = TerraScanCommon.GetForm(9101, null, this.form2200Control.WorkItem);

                if (parcelF9101 != null)
                {
                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                    {
                        Int32 resultownerid;
                        Int32.TryParse((TerraScanCommon.GetValue(parcelF9101, SharedFunctions.GetResourceString("MasterNameOwnerId"))), out resultownerid);
                        this.selectedownerId = resultownerid;
                        if (this.FormPermissionFields.editPermission)
                        {
                            this.EditScheduleCancelButton.Enabled = true;
                            this.EditScheduleSaveButton.Enabled = true;
                        }

                        this.EditScheduleNewButton.Enabled = false;
                        this.EditScheduleDeleteButton.Enabled = false;
                        this.CopyButton.Enabled = false;

                        this.saveMsg = true;
                        this.ownerDetailDataSet = this.form2200Control.WorkItem.F15010_GetOwnerDetails(this.selectedownerId);
                        if (this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows.Count > 0)
                        {
                            //Issue fixed for TSBG 13002
                            //this.selectedownerName = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString();
                            string ScheduleOwnerField = string.Empty;
                            ScheduleOwnerField = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameOwnerColumn].ToString();
                            if (ScheduleOwnerField.Contains("&"))
                            {
                                ScheduleOwnerField = ScheduleOwnerField.Replace("&", "&&");
                            }
                            this.ScheduleOwnerLinkLabel.Text = ScheduleOwnerField;
                            //this.ScheduleOwnerLinkLabel.Text = this.selectedownerName;
                        }
                    }
                }

                this.ActiveControl = this.EditScheduleSaveButton;
                this.EditScheduleSaveButton.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.ScheduleOwnerLinkLabel.Focus();
            }
        }

        /// <summary>
        /// ScheduleOwnerLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleOwnerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(91000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.selectedownerId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
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
        /// ParcelReferencebutton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelReferencebutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int rollYear;
                int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);

                object[] optionalParameter = new object[] { rollYear };
                Form parcelSelectionForm = TerraScanCommon.GetForm(1401, optionalParameter, this.form2200Control.WorkItem);
                ////open form in view mode - possible to edit
                if (parcelSelectionForm != null)
                {
                    if (parcelSelectionForm.ShowDialog().Equals(DialogResult.OK))
                    {
                        if (this.FormPermissionFields.editPermission)
                        {
                            this.EditScheduleCancelButton.Enabled = true;
                            this.EditScheduleSaveButton.Enabled = true;
                        }

                        this.EditScheduleNewButton.Enabled = false;
                        this.EditScheduleDeleteButton.Enabled = false;
                        this.CopyButton.Enabled = false;

                        this.parcelId = Convert.ToInt32(TerraScanCommon.GetValue(parcelSelectionForm, "ParcelID"));
                        this.GetParcelDetails(this.parcelId);
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Districbutton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Districbutton_Click(object sender, EventArgs e)
        {
            try
            {
                Form districtF15122 = new Form();
                object[] optionalParameter = new object[] { this.RollYearTextBox.Text, this.ParentFormId };//,this.scheduleId
                districtF15122 = TerraScanCommon.GetForm(1512, optionalParameter, this.form2200Control.WorkItem);

                DialogResult districtDialog;
                if (districtF15122 != null)
                {
                    districtDialog = districtF15122.ShowDialog();

                    if (districtDialog == DialogResult.OK)
                    {
                        try
                        {
                            this.districtId = TerraScanCommon.GetValue(districtF15122, "DistrictId");
                            this.districtSlectionDataset = this.form2200Control.WorkItem.F2200_GetDistrictSelectionData(this.ConvertStringToInteger(this.districtId), "", "", -999);

                            if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count > 0)
                            {
                                if (this.FormPermissionFields.editPermission)
                                {
                                    this.EditScheduleCancelButton.Enabled = true;
                                    this.EditScheduleSaveButton.Enabled = true;
                                }
                                this.EditScheduleNewButton.Enabled = false;
                                this.EditScheduleDeleteButton.Enabled = false;
                                this.CopyButton.Enabled = false;
                                string strMessage = "No message !!";//this.editScheduledata.f2200ListScheduleDataTable.Rows[0][this.editScheduledata.f2200ListScheduleDataTable.UserWarningColumn.ColumnName].ToString();

                                //Modifed to resolve #21448Bug
                                if (!string.IsNullOrEmpty(strMessage) && strMessage == "No message !!")
                                {
                                    this.DistrictTextBox.Text = this.districtSlectionDataset.ListDistrictSelection.Rows[0][this.districtSlectionDataset.ListDistrictSelection.DistrictColumn].ToString() + " - " + this.districtSlectionDataset.ListDistrictSelection.Rows[0][this.districtSlectionDataset.ListDistrictSelection.DescriptionColumn].ToString();

                                }
                                else
                                {
                                    DialogResult ds = TerraScanMessageBox.Show(strMessage, "Terrascan T2 - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (ds.Equals(DialogResult.OK))
                                    {
                                        // this.DialogResult = DialogResult.No;

                                    }
                                }


                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// EditScheduleCancelButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EditScheduleCancelButton_Click(object sender, EventArgs e)
            {
            try
            {
                this.fillDateChanged = false;
                this.PenaltyPercentTextBox.TextCustomFormat = "";
                if (this.newDetails)
                {
                    this.LoadFieldDeatails();
                    ////this.ClearDeatails();
                    this.newDetails = false;
                }
                else
                {
                    this.LoadFieldDeatails();
                }
                this.LockControl(!this.FormPermissionFields.editPermission);
                this.LoadEnabled();
                this.EditSchedulePanel.BackColor = Color.White;
                this.ScheduleNumberlabel1.BackColor = Color.White;
                this.ScheduleNumberTextBox.Focus();
                this.label1.Visible = true;
                this.EditSchedulelIDLinkLabel.Enabled = true;
                this.PersonalPropertyDescriptionTextBox.Enabled = false;
                this.Customformat();
                this.RollYearTextBox.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// EditEnabled
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EditEnabled(object sender, KeyPressEventArgs e)
        {
            if (this.FormPermissionFields.editPermission)
            {
                this.EditScheduleCancelButton.Enabled = true;
                this.EditScheduleSaveButton.Enabled = true;
            }

            this.EditScheduleNewButton.Enabled = false;
            this.EditScheduleDeleteButton.Enabled = false;
            this.CopyButton.Enabled = false;
        }

        /// <summary>
        /// EditSchedulelIDLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EditSchedulelIDLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////if (this.currentParcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = 1;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    //// this.Close();
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
        /// FillingDateTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FillingDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.fillDateChanged)
                {
                    FillingDatepanel.BackColor = Color.White;
                    FillingDateTextBox.BackColor = Color.White;
                    if (this.FormPermissionFields.editPermission)
                    {
                        // DB call for f2200_pcget_PPPenalytPercent(filingDate) to get PenaltyPercent value
                        decimal penaltyPercent = this.F2200Controll.WorkItem.GetPenaltyPercent(this.FillingDateTextBox.Text) * 100;
                        this.PenaltyPercentTextBox.Text = penaltyPercent.ToString();
                        //this.PenaltyAmountTextBox.Focus();
                    }
                }
                this.fillDateChanged = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// DescriptionTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DescriptionTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                Descriptionpanel.BackColor = Color.White;
                DescriptionTextBox.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ScheduleOwnerLinkLabel_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleOwnerLinkLabel_Leave(object sender, EventArgs e)
        {
            try
            {
                ScheduleOwnerpanel.BackColor = Color.White;
                ScheduleOwnerLinkLabel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelReferenceTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelReferenceTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                ParcelReferenceTextBox.BackColor = Color.White;
                ParcelReferencePanel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AllTextBox_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AllTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 46 || e.KeyValue == 8)
                {
                    if (this.FormPermissionFields.editPermission)
                    {
                        this.EditScheduleCancelButton.Enabled = true;
                        this.EditScheduleSaveButton.Enabled = true;
                    }

                    this.EditScheduleNewButton.Enabled = false;
                    this.EditScheduleDeleteButton.Enabled = false;
                    this.CopyButton.Enabled = false;

                    //this.EditScheduleNewButton.Enabled = false;
                    //this.EditScheduleDeleteButton.Enabled = false;
                    //this.CopyButton.Enabled = false;
                }

                this.keyDown = e.Control;
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
        /// AllTextBox_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AllTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.EditScheduleNewButton.Enabled == true)
                {
                    int a = e.KeyChar;
                    if (this.keyDown == true)
                    {
                        if ((e.KeyChar == 'v') || (e.KeyChar == 24))
                        {
                            if (this.FormPermissionFields.editPermission)
                            {
                                this.EditScheduleCancelButton.Enabled = true;
                                this.EditScheduleSaveButton.Enabled = true;
                            }
                            this.EditScheduleNewButton.Enabled = false;
                            this.EditScheduleDeleteButton.Enabled = false;
                            this.CopyButton.Enabled = false;
                        }
                        else
                        {
                            this.EditScheduleCancelButton.Enabled = false;
                            this.EditScheduleSaveButton.Enabled = false;
                            // permission has been implemened for the issue 5184
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.EditScheduleNewButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleNewButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.deletePermission)
                            {
                                this.EditScheduleDeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleDeleteButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.CopyButton.Enabled = true;
                            }
                            else
                            {
                                this.CopyButton.Enabled = false;
                            }
                            // Coding Ends here
                        }
                    }
                    else if (e.KeyChar == 13)
                    {
                        if (this.EditScheduleSaveButton.Enabled == false)
                        {
                            this.EditScheduleCancelButton.Enabled = false;
                            this.EditScheduleSaveButton.Enabled = false;
                            // permission has been implemened for the issue 5184
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.EditScheduleNewButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleNewButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.deletePermission)
                            {
                                this.EditScheduleDeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleDeleteButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.CopyButton.Enabled = true;
                            }
                            else
                            {
                                this.CopyButton.Enabled = false;
                            }
                            // Coding Ends here
                        }
                    }
                    else if (e.KeyChar == 27)
                    {
                        this.Close();
                    }
                    else
                    {
                        ////if (this.FormPermissionFields.editPermission)
                        ////{
                        ////    this.EditScheduleCancelButton.Enabled = true;
                        ////    this.EditScheduleSaveButton.Enabled = true;
                        ////}
                        ////this.EditScheduleNewButton.Enabled = false;
                        ////this.EditScheduleDeleteButton.Enabled = false;
                        ////this.CopyButton.Enabled = false;
                    }
                }

                if (e.KeyChar == 27)
                {
                    if (this.newDetails)
                    {
                        this.LoadFieldDeatails();
                        ////this.ClearDeatails();
                        this.newDetails = false;
                    }
                    else
                    {
                        this.LoadFieldDeatails();
                    }

                    this.LoadEnabled();
                    this.EditSchedulePanel.BackColor = Color.White;
                    this.ScheduleNumberlabel1.BackColor = Color.White;
                    this.ScheduleNumberTextBox.Focus();
                    this.label1.Visible = true;
                    EditSchedulelIDLinkLabel.Enabled = true;
                }

                if (e.KeyChar == 19)
                {
                    if (this.EditScheduleSaveButton.Enabled == true)
                    {
                        this.ScheduleNumberTextBox.Focus();
                        this.InsertOrUpdateContractDetails();

                        if (this.missingReq)
                        {
                            if (this.FormPermissionFields.editPermission)
                            {
                                this.EditScheduleCancelButton.Enabled = true;
                                this.EditScheduleSaveButton.Enabled = true;
                            }
                            this.EditScheduleNewButton.Enabled = false;
                            this.EditScheduleDeleteButton.Enabled = false;
                            this.CopyButton.Enabled = false;
                            this.missingReq = false;
                        }
                        else
                        {
                            this.EditScheduleCancelButton.Enabled = false;
                            this.EditScheduleSaveButton.Enabled = false;
                            // permission has been implemened for the issue 5184
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.EditScheduleNewButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleNewButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.deletePermission)
                            {
                                this.EditScheduleDeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleDeleteButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.CopyButton.Enabled = true;
                            }
                            else
                            {
                                this.CopyButton.Enabled = false;
                            }
                            // Coding Ends here
                            this.missingReq = false;
                        }

                        if (this.newMode)
                        {
                            this.EditScheduleCancelButton.Enabled = false;
                            this.EditScheduleSaveButton.Enabled = false;
                            // permission has been implemened for the issue 5184
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.EditScheduleNewButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleNewButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.EditScheduleDeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleDeleteButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.CopyButton.Enabled = true;
                            }
                            else
                            {
                                this.CopyButton.Enabled = false;
                            }
                            // Coding Ends here
                            this.newMode = false;
                        }

                        if (this.sortcutNew)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.sortcutNew = false;
                        }
                    }
                }

                if (e.KeyChar == 14)
                {
                    this.ClearDeatails();
                    this.newDetails = true;
                    this.ScheduleNumberTextBox.Focus();
                    if (this.FormPermissionFields.editPermission)
                    {
                        this.EditScheduleCancelButton.Enabled = true;
                        this.EditScheduleSaveButton.Enabled = true;
                    }
                    this.EditScheduleNewButton.Enabled = false;
                    this.EditScheduleDeleteButton.Enabled = false;
                    this.CopyButton.Enabled = false;

                    EditSchedulelIDLinkLabel.Text = "tAA_Schedule[ScheduleID] ";
                    EditSchedulelIDLinkLabel.Enabled = false;
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
        /// ScheduleOwnerLinkLabel_LinkClicked_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleOwnerLinkLabel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(91000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.selectedownerId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Close();
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
        /// ScheduleOwnerLinkLabel_LinkClicked_2
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleOwnerLinkLabel_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (ScheduleOwnerLinkLabel.Text != string.Empty)
                {
                    if (this.EditScheduleSaveButton.Enabled == true)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("Do you wish to save this details?"), "Terrascan T2", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.InsertOrUpdateContractDetails();
                        }
                        else
                        {
                            this.missingReq = false;
                            this.linkclickblockbool = true;
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(91000);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = this.selectedownerId;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                            this.Close();
                            this.linkCheck = false;
                        }
                    }

                    if (!this.linkCheck)
                    {
                        this.missingReq = false;
                        this.linkclickblockbool = true;
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(91000);
                        formInfo.optionalParameters = new object[1];
                        formInfo.optionalParameters[0] = this.selectedownerId;
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        this.Close();
                        this.linkCheck = false;
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
        /// ScheduleNumberTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleNumberTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                ScheduleNumberTextBox.BackColor = Color.White;
                ScheduleNumberPanel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ScheduleNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.loadDetails)
                {
                    // permission has been implemened for the issue 5184
                    if (this.FormPermissionFields.editPermission)
                    {
                        this.EditScheduleCancelButton.Enabled = true;
                        this.EditScheduleSaveButton.Enabled = true;
                    }
                    if (this.FormPermissionFields.newPermission)
                    {
                        this.EditScheduleNewButton.Enabled = true;
                    }
                    else
                    {
                        this.EditScheduleNewButton.Enabled = false;
                    }
                    if (this.FormPermissionFields.deletePermission)
                    {
                        this.EditScheduleDeleteButton.Enabled = true;
                    }
                    else
                    {
                        this.EditScheduleDeleteButton.Enabled = false;
                    }
                    if (this.FormPermissionFields.newPermission)
                    {
                        this.CopyButton.Enabled = true;
                    }
                    else
                    {
                        this.CopyButton.Enabled = false;
                    }
                    // Coding Ends here
                }
                else
                {
                    this.EditScheduleCancelButton.Enabled = false;
                    this.EditScheduleSaveButton.Enabled = false;

                    // permission has been implemened for the issue 5184
                    if (this.FormPermissionFields.newPermission)
                    {
                        this.EditScheduleNewButton.Enabled = true;
                    }
                    else
                    {
                        this.EditScheduleNewButton.Enabled = false;
                    }

                    if (this.FormPermissionFields.deletePermission)
                    {
                        this.EditScheduleDeleteButton.Enabled = true;
                    }
                    else
                    {
                        this.EditScheduleDeleteButton.Enabled = false;
                    }

                    if (this.FormPermissionFields.newPermission)
                    {
                        this.CopyButton.Enabled = true;
                    }
                    else
                    {
                        this.CopyButton.Enabled = false;
                    }
                    // Coding Ends here
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            ////loadDetails = false;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableControls(object sender, EventArgs e)
        {
            if (this.loadDetails)
            {
                if (this.FormPermissionFields.editPermission)
                {
                    this.EditScheduleCancelButton.Enabled = true;
                    this.EditScheduleSaveButton.Enabled = true;
                }
                this.EditScheduleNewButton.Enabled = false;
                this.EditScheduleDeleteButton.Enabled = false;
                this.CopyButton.Enabled = false;
            }
            else
            {
                this.EditScheduleCancelButton.Enabled = false;
                this.EditScheduleSaveButton.Enabled = false;
                // permission has been implemened for the issue 5184
                if (this.FormPermissionFields.newPermission)
                {
                    this.EditScheduleNewButton.Enabled = true;
                }
                else
                {
                    this.EditScheduleNewButton.Enabled = false;
                }
                if (this.FormPermissionFields.deletePermission)
                {
                    this.EditScheduleDeleteButton.Enabled = true;
                }
                else
                {
                    this.EditScheduleDeleteButton.Enabled = false;
                }
                if (this.FormPermissionFields.newPermission)
                {
                    this.CopyButton.Enabled = true;
                }
                else
                {
                    this.CopyButton.Enabled = false;
                }
                // Coding Ends here
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the F2200 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F2200_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.EditScheduleSaveButton.Enabled && (!this.linkclickblockbool))
                {
                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForms"), " ", this.AccessibleName), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.ScheduleNumberTextBox.Focus();
                        this.InsertOrUpdateContractDetails();

                        if ((!this.missingReq) || this.saveSchedule)
                        {
                            this.EditScheduleCancelButton.Enabled = false;
                            this.EditScheduleSaveButton.Enabled = false;
                            // permission has been implemened for the issue 5184
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.EditScheduleNewButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleNewButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.deletePermission)
                            {
                                this.EditScheduleDeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleDeleteButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.CopyButton.Enabled = true;
                            }
                            else
                            {
                                this.CopyButton.Enabled = false;
                            }
                            // Coding Ends here
                            this.missingReq = false;
                            this.saveSchedule = false;
                            this.DialogResult = DialogResult.OK;

                            if (!this.newnavigateId)
                            {
                                this.scheduleIds = this.scheduleId;
                            }
                            else
                            {
                                this.scheduleIds = this.schedule;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.ScheduleNumberTextBox.Focus();
                        e.Cancel = false;
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    if (!this.newnavigateId)
                    {
                        this.scheduleIds = this.scheduleId;
                    }
                    else
                    {
                        this.scheduleIds = this.schedule;
                    }

                    this.newnavigateId = false;
                }

                this.linkclickblockbool = false;
                this.saveSchedule = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ScheduleNumberlabel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleNumberlabel1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.HeadertoolTip.SetToolTip(this.ScheduleNumberlabel1, ScheduleNumberlabel1.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ScheduleNumberlabel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleNumberlabel1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Handles the KeyPress event of the F2200 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void F2200_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.EditScheduleSaveButton.Enabled == false)
                {
                    int keyEnter = e.KeyChar;
                    if (keyEnter == 13)
                    {
                        this.EditScheduleSaveButton.Enabled = false;
                        this.EditScheduleCancelButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the NewConstrctionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void NewConstrctionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.editPermission)
                {
                    if (e.KeyChar >= 48 && e.KeyChar <= 57)
                    {
                        if (this.FormPermissionFields.editPermission)
                        {
                            this.EditScheduleCancelButton.Enabled = true;
                            this.EditScheduleSaveButton.Enabled = true;
                        }

                        this.EditScheduleNewButton.Enabled = false;
                        this.EditScheduleDeleteButton.Enabled = false;
                        this.CopyButton.Enabled = false;
                    }
                    //if (this.FormPermissionFields.newPermission)
                    //{
                    //    this.EditScheduleNewButton.Enabled = true;
                    //}
                    //else
                    //{
                    //    this.EditScheduleNewButton.Enabled = false;
                    //}
                    //if (this.FormPermissionFields.deletePermission)
                    //{
                    //    this.EditScheduleDeleteButton.Enabled = true;
                    //}
                    //else
                    //{
                    //    this.EditScheduleDeleteButton.Enabled = false;
                    //}
                    //if (this.FormPermissionFields.newPermission)
                    //{
                    //    this.CopyButton.Enabled = true;
                    //}
                    //else
                    //{
                    //    this.CopyButton.Enabled = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CopyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.newPermission)
                {
                    Form scheduleCopyForm = new Form();
                    object[] optionalParameter = new object[] { this.scheduleId };
                    scheduleCopyForm = TerraScanCommon.GetForm(2204, optionalParameter, this.form2200Control.WorkItem);
                    if (scheduleCopyForm != null)
                    {
                        if (scheduleCopyForm.ShowDialog() == DialogResult.OK)
                        {
                            this.LoadEnabled();
                            this.CustomizeCombobox();
                            this.LoadFieldDeatails();
                            this.newDetails = false;
                            this.dateSelection = false;
                            this.missingReq = false;
                            this.saveMsg = false;
                            this.loadDetails = true;
                            this.sortcutNew = false;
                            this.saveSchedule = false;
                            this.linkclickblockbool = false;
                            this.linkCheck = false;
                            this.newnavigateId = false;
                            this.setSeletedDates = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region coding added for the issue 1128
        /// <summary>
        /// Sets the text custom format.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void setTextCustomFormat(object sender, EventArgs e)
        {
            this.Customformat();
        }

        /// <summary>
        /// Customformats this instance.
        /// </summary>
        private void Customformat()
        {
            string ppercent = this.PenaltyPercentTextBox.DecimalTextBoxValue.ToString();
            int leng = ppercent.Length;
            // to get the decimal position.
            int decPos = ppercent.IndexOf(".");
            //to check if decimal places are avilable.
            if (decPos != -1)
            {
                if (leng - (decPos + 1) > 0)
                {
                    // To get decimal part
                    ppercent = ppercent.ToString().Substring(decPos + 1, leng - (decPos + 1)).Trim();
                }
            }
            int zerocount = 0;
            int nonzerocount = 0;
            // to get how many zero and non-zero are available in decimal part.
            for (int i = ppercent.Length; i >= 1; i--)
            {
                string arrChar = Convert.ToString(ppercent[i - 1]);
                if (arrChar.Equals("0"))
                {
                    if (nonzerocount >= 1)
                    {
                        nonzerocount++;
                    }
                    else
                    {
                        zerocount++;
                    }
                }
                else
                {
                    nonzerocount++;
                }
            }

            // To set the customformat of penalty percent.
            if (decPos != -1)
            {
                if (nonzerocount.Equals(0) || nonzerocount.Equals(1))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.0%";
                }
                else if (nonzerocount.Equals(2))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.00%";
                }
                else if (nonzerocount.Equals(3))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.000%";
                }
                else if (nonzerocount.Equals(4))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.0000%";
                }
                else if (nonzerocount.Equals(5))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.00000%";
                }
                else
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.000000%";
                }
            }
            else
            {
                // To set the customformat of penalty percent.
                this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.0%";
            }
        }
        #endregion 1128

        /// <summary>
        /// Handles the LinkClicked event of the ScheduleDORlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ScheduleDORlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.newDetails)
                {
                    Form statecode = new Form();
                    object[] optionalParameters;
                    if (this.ScheduleDORlinkLabel.Text.StartsWith("<"))
                    {
                        string tempDOR = String.Empty;
                        optionalParameters = new object[] { tempDOR };
                    }
                    else
                    {
                        optionalParameters = new object[] { this.ScheduleDORlinkLabel.Text };
                    }

                    statecode = TerraScanCommon.GetForm(2010, optionalParameters, this.form2200Control.WorkItem);
                    if (statecode != null)
                    {
                        if (statecode.ShowDialog() == DialogResult.OK)
                        {
                            string ScheduleDORfield = string.Empty;
                            ScheduleDORfield = TerraScanCommon.GetValue(statecode, "StateCodeNameReturnValue");
                            if (ScheduleDORfield.Contains("&"))
                            {
                                ScheduleDORfield = ScheduleDORfield.Replace("&", "&&");
                            }
                            this.ScheduleDORlinkLabel.Text = ScheduleDORfield;
                            this.EditEnabled(sender, e);
                        }
                    }
                }
                else
                {
                    if (this.FormPermissionFields.editPermission)
                    {
                        Form statecode = new Form();
                        object[] optionalParameters;
                        if (this.ScheduleDORlinkLabel.Text.StartsWith("<"))
                        {
                            string tempDOR = String.Empty;
                            optionalParameters = new object[] { tempDOR };
                        }
                        else
                        {
                            optionalParameters = new object[] { this.ScheduleDORlinkLabel.Text };
                        }

                        statecode = TerraScanCommon.GetForm(2010, optionalParameters, this.form2200Control.WorkItem);
                        if (statecode != null)
                        {
                            if (statecode.ShowDialog() == DialogResult.OK)
                            {
                                string ScheduleDORfield = string.Empty;
                                ScheduleDORfield = TerraScanCommon.GetValue(statecode, "StateCodeNameReturnValue");
                                if (ScheduleDORfield.Contains("&"))
                                {
                                    ScheduleDORfield = ScheduleDORfield.Replace("&", "&&");
                                }
                                this.ScheduleDORlinkLabel.Text = ScheduleDORfield;
                                this.EditEnabled(sender, e);
                            }
                        }
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
        /// Handles the KeyUp event of the FillingDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FillingDateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue != 9)
                {
                    this.fillDateChanged = true;
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
        #endregion Event

        /*

        #region Coding Added for the issue 1127
        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns>Boolean</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                const int WM_KEYDOWN = 0x100;
                const int WM_SYSKEYDOWN = 0x104;
                if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
                {
                    if (keyData.Equals(Keys.Control | Keys.S))
                    {
                        this.PenaltyPercentTextBox.TextCustomFormat = ""; 
                        this.Customformat();
                        this.InsertOrUpdateContractDetails();
                        this.LoadEnabled();
                        this.saveMsg = true;
                        if (!this.missingReq)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            this.EditScheduleCancelButton.Enabled = true;
                            this.EditScheduleSaveButton.Enabled = true;
                            this.EditScheduleNewButton.Enabled = false;
                            this.EditScheduleDeleteButton.Enabled = false;
                            this.CopyButton.Enabled = false;
                        }

                        if (this.newMode)
                        {
                            this.EditScheduleCancelButton.Enabled = false;
                            this.EditScheduleSaveButton.Enabled = false;
                            // permission has been implemened for the issue 5184
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.EditScheduleNewButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleNewButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.deletePermission)
                            {
                                this.EditScheduleDeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.EditScheduleDeleteButton.Enabled = false;
                            }
                            if (this.FormPermissionFields.newPermission)
                            {
                                this.CopyButton.Enabled = true;
                            }
                            else
                            {
                                this.CopyButton.Enabled = false;
                            }
                            // Coding Ends here
                            this.newMode = false;
                        }

                        this.missingReq = false;
                    }
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return false;
            }
        }
        #endregion 1127

        */

        /// <summary>
        /// Handles the TextChanged event of the PenaltyPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PenaltyPercentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PenaltyPercentTextBox.DecimalTextBoxValue.ToString()) && !this.deleteflag)
            {
                if (this.loadDetails)
                {
                    if (this.FormPermissionFields.editPermission)
                    {
                        this.EditScheduleCancelButton.Enabled = true;
                        this.EditScheduleSaveButton.Enabled = true;
                    }
                    this.EditScheduleNewButton.Enabled = false;
                    this.EditScheduleDeleteButton.Enabled = false;
                    this.CopyButton.Enabled = false;
                }

                //int decimalindex = this.PenaltyPercentTextBox.Text.IndexOf(".");
                //int leng = this.PenaltyPercentTextBox.Text.Length; 
                //string ppercent = this.PenaltyPercentTextBox.Text.ToString().Substring(decimalindex + 1, leng - (decimalindex + 1)).Trim();

                //if (decimalindex.Equals(1))
                //{
                //    if (leng.Equals(9))
                //    {
                //        MessageBox.Show("Entered values exceeds max limit.", "Invalid Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        this.PenaltyPercentTextBox.Text = "";
                //        this.PenaltyPercentTextBox.Focus(); 
                //    }
                //}
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the FarmExpertComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FarmExpertComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.FarmExpertComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
            {
                this.ExemptAmountdetails();
            }
            this.EditEnabled(sender, e);
        }

        /// <summary>
        /// Handles the TextChanged event of the ExemptAmountTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ExemptAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isNegative)
            {
                this.ExemptAmountTextBox.Text = this.negValue.ToString();
                this.isNegative = false;
            }
            //if (!string.IsNullOrEmpty(this.ExemptAmountTextBox.Text))
            //{
            //    decimal amount;
            //    decimal.TryParse(this.ExemptAmountTextBox.Text, out amount);
            //    if (amount < 0)
            //    {
            //        MessageBox.Show("Negative values are not allowed in the Exempt Amount field", "Terrascan – Negative not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //    else
            //    {
            //        if (amount > O1Value)
            //        {
            //            MessageBox.Show("Exemption amount cannot be greater than schedule value", "Terrascan – Invalid exemption amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //}
           // this.ExemptAmountTextBox.Text = amount.ToString();

            this.EditEnabled(sender, e);
        }

        /// <summary>
        /// Handles the TextChanged event of the ExemptYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ExemptYearTextBox_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.ExemptYearTextBox.Text) && Convert.ToInt32(this.ExemptYearTextBox.Text) > 0) //&& this.ExemptYearTextBox.Text.Length >= 4
            {
                this.ExemptAmountdetails();
                //int yearval;
                //int.TryParse(this.ExemptYearTextBox.Text.Trim(), out yearval);
                //if (yearval <= 1899 || yearval >= 2080)
                //{
                //    MessageBox.Show(SharedFunctions.GetResourceString("F2200Rollyear"), "TerraScan-InValid RollYear", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //}
                //else
                //{
                //    this.ExemptAmountdetails();
                //}

            }

            this.EditEnabled(sender, e);
        }


        /// <summary>
        /// Exempts the amountdetails.
        /// </summary>
        private void ExemptAmountdetails()
        {
            try
            {
                if (!isNotCustomState)
                {
                    bool isFarmExempt = false;
                    int exemptYear = 0;
                    if (this.FarmExpertComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
                    {
                        isFarmExempt = true;
                    }
                    if (!string.IsNullOrEmpty(this.ExemptYearTextBox.Text))
                    {
                        exemptYear = Convert.ToInt32(this.ExemptYearTextBox.Text);
                    }

                    bool is259Exempt = false;

                        if (this.Ex259ComboBox.SelectedIndex.ToString().Equals("1"))
                        {
                            is259Exempt = true;
                        }

                        decimal exempt259Amount = 0;
                        if (!string.IsNullOrEmpty(this.ExAmt259TextBox.Text))
                        {
                            exempt259Amount = Convert.ToDecimal(this.ExAmt259TextBox.Text);
                        }


                    if (scheduleId > 0 && (exemptYear > 0) && isFarmExempt)
                    {
                        if (exemptYear >= 1899 && exemptYear <= 2080)
                        {
                            var tempExemptAmount = this.form2200Control.WorkItem.F2200_GetFarmExemptAmount(scheduleId, isFarmExempt, exemptYear, is259Exempt, exempt259Amount);
                            //this.ExemptAmountTextBox.Text = tempExemptAmount.ToString();
                            double strNumb = Convert.ToDouble(tempExemptAmount.ToString());
                            var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                            this.ExemptAmountTextBox.Text = strResult;
                        }
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

        private void ExemptAmountTextBox_Leave(object sender, EventArgs e)
        {
            try            
            {
                if (!string.IsNullOrEmpty(this.ExemptAmountTextBox.Text))
                {
                    decimal amount;
                    //if(this.ExemptAmountTextBox.Text.Contains("("))
                    //{
                    //  this.ExemptAmountTextBox.Text=  this.ExemptAmountTextBox.Text.Replace('(', "");
                    //  this.ExemptAmountTextBox.Text=  this.ExemptAmountTextBox.Text.Replace(")", "");
                    //}
                    decimal.TryParse(this.ExemptAmountTextBox.Text, out amount);
                    if (amount < 0)
                    {
                        MessageBox.Show("Negative values are not allowed in the Exempt Amount field", "Terrascan – Negative not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.ExemptAmountTextBox.Text = "0";
                        this.ExemptAmountTextBox.Focus();
                    }
                    else
                    {
                        if (amount > O1Value)
                        {
                            MessageBox.Show("Exemption amount cannot be greater than schedule value", "Terrascan – Invalid exemption amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.ExemptAmountTextBox.Text = "0";
                            this.ExemptAmountTextBox.Focus();
                            
                        }
                        
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
       
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {                
                Form ExemptionForm = new Form();
                object[] optionalParameter = new object[] { this.PersonalPropertyCodeTextBox.Text };

                ExemptionForm = this.form2200Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2201, optionalParameter, this.form2200Control.WorkItem);

                if (ExemptionForm != null)
                {
                    if (ExemptionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.pCode = TerraScanCommon.GetValue(ExemptionForm, "CommandResult");
                        this.GetDescription();
                        // this.pDescription = TerraScanCommon.GetValue(ExemptionForm, "CommandValue");
                        if (!string.IsNullOrEmpty(pCode))
                        {
                            this.PersonalPropertyCodeTextBox.Text = this.pCode;
                        }
                        if (!string.IsNullOrEmpty(pDescription))
                        {
                            this.PersonalPropertyDescriptionTextBox.Text = this.pDescription;
                        }
                        this.PersonalPropertyDescriptionTextBox.Enabled = false;
                        this.EditEnabled(sender, e);
                    }
                    if (ExemptionForm.DialogResult.ToString().ToLower().Equals("no"))
                    {
                        if (!string.IsNullOrEmpty(this.PersonalPropertyCodeTextBox.Text.ToString()))
                        {
                            this.PersonalPropertyCodeTextBox.Text = string.Empty;
                            this.PersonalPropertyDescriptionTextBox.Text = string.Empty;
                            this.PersonalPropertyDescriptionTextBox.Enabled = false;
                            this.EditEnabled(sender, e);
                        }
                    }
                    
                    this.PersonalPropertyCodeTextBox.Focus();
                   
                    
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

        private void PersonalPropertyCodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PersonalPropertyCodeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.pCode = this.PersonalPropertyCodeTextBox.Text.Trim();
                this.GetDescription();
                this.EditEnabled(sender, e);
               // SendKeys.Send("{TAB}");
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


        private void GetDescription()
        {
                    if (!string.IsNullOrEmpty(this.pCode.Trim()))
                    {
                        this.SearchDescriptionDataSet = this.form2200Control.WorkItem.F2201_GetPersonalPropertyDescription(this.pCode.Trim());
                        if (this.SearchDescriptionDataSet.f2200_PersonalPropertyCodeDescription.Rows.Count > 0)
                        {
                            this.pDescription = this.SearchDescriptionDataSet.f2200_PersonalPropertyCodeDescription.Rows[0][0].ToString();
                            this.PersonalPropertyDescriptionTextBox.Text = pDescription;
                            this.PersonalPropertyDescriptionTextBox.Enabled = false;
                        }
                        else
                        {
                            this.PersonalPropertyDescriptionTextBox.Text = string.Empty;
                            this.PersonalPropertyDescriptionTextBox.Enabled = false;
                        }

                    }
                    else
                    {
                            this.PersonalPropertyDescriptionTextBox.Text = string.Empty;
                        this.PersonalPropertyDescriptionTextBox.Enabled = false;
                    }
        }
        private void ScheduleTypePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Ex 259 ComboBox Selectection Index Changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ex259ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Ex259ComboBox.SelectedIndex.ToString().ToLower().Equals("1"))
            {
                this.Ex259ComboBoxdetails();
            }
            else 
            {
                this.ExAmt259TextBox.Text = string.Empty;
                this.ExAmt259TextBox.Enabled = false;
            }
            this.EditEnabled(sender, e);
        }

        /// <summary>
        /// Ex 259 ComboBox Details.
        /// </summary>
        private void Ex259ComboBoxdetails()
        {
            try
            {
                double strNumb = 0.0;
                double dataVal=0.0;
                if (this.Ex259ComboBox.SelectedIndex.ToString().ToLower().Equals("1"))
                {
                    var tempExemptAmount = this.form2200Control.WorkItem.F2200_Get259ExemptionAmount(scheduleId);
                    if (tempExemptAmount.Get259ExemptionAmount.Rows.Count > 0)
                    {
                        if (!tempExemptAmount.Get259ExemptionAmount[0].IsExempt259AmountNull())
                        {
                            double.TryParse(tempExemptAmount.Get259ExemptionAmount[0].Exempt259Amount.ToString(), out dataVal);
                        }

                        strNumb = dataVal;
                    }
                    else
                    {
                        strNumb = 0;
                    }
                    var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                    this.ExAmt259TextBox.Text = strResult;
                    this.ExAmt259TextBox.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Ex Amt-259 Text Change Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExAmt259TextBox_TextChanged(object sender, EventArgs e)
        {
            this.EditEnabled(sender, e);
        }

        /// <summary>
        /// Ex Amt-259 Text Change Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExAmt259TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!isNotCustomState)
                {
                    bool isFarmExempt = false;
                    int exemptYear = 0;
                    if (this.FarmExpertComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
                    {
                        isFarmExempt = true;
                    }
                    if (!string.IsNullOrEmpty(this.ExemptYearTextBox.Text))
                    {
                        exemptYear = Convert.ToInt32(this.ExemptYearTextBox.Text);
                    }

                    bool is259Exempt = false;

                    if (this.Ex259ComboBox.SelectedItem.ToString().ToLower().Equals("yes"))
                    {
                        is259Exempt = true;
                    }

                    decimal exempt259Amount = 0;
                    if (!string.IsNullOrEmpty(this.ExAmt259TextBox.Text))
                    {
                        exempt259Amount = Convert.ToDecimal(this.ExAmt259TextBox.Text);
                    }

                    var tempExemptAmount = this.form2200Control.WorkItem.F2200_GetFarmExemptAmount(scheduleId, isFarmExempt, exemptYear, is259Exempt, exempt259Amount);
                    double strNumb = Convert.ToDouble(tempExemptAmount.ToString());
                    var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                    this.ExemptAmountTextBox.Text = strResult;
                }

                this.EditEnabled(sender, e);
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
    }
}