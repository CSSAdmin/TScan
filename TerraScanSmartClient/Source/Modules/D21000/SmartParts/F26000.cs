//----------------------------------------------------------------------------------
// <copyright file="F26000.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm. 
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		            Description
// ----------		---------		        ----------------------------------------
// 23 Oct 13       Purushotham A             Created
//*********************************************************************************/
namespace D21000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.UI.Controls;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using TerraScan.BusinessEntities;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Collections;
    using System.Runtime;

    /// <summary>
    /// 
    /// </summary>
    public partial class F26000 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;
        private int classCodeConfigValue;
        private string classCode;


        /// <summary>
        /// Set Default OpenPermission for Form 2000
        /// </summary>
        private bool form2000OpenPermission;

        /// <summary>
        /// Set Default navigationFlag 
        /// </summary>
        private bool navigationFlag;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;


        /// <summary>
        /// DataSet which is used to get the details of Stoppage event
        /// </summary>
        private F26000ParcelHeaderFormData ParcelHeaderFormData = new F26000ParcelHeaderFormData();

        /// <summary>
        /// ImprovementListData
        /// </summary>
        private F26000ParcelHeaderFormData ImprovementListData = new F26000ParcelHeaderFormData();

        /// <summary>
        /// LandtypeData
        /// </summary>
        private F26000ParcelHeaderFormData LandtypeData = new F26000ParcelHeaderFormData();

         F26000ParcelHeaderFormData.f26000_pcget_ExemptFieldDetailsDataTable ExemptFieldTable = new F26000ParcelHeaderFormData.f26000_pcget_ExemptFieldDetailsDataTable();

        /// <summary>
        /// DistrictSelectionData instance
        /// </summary>
        private F1512DistrictSelectionData districtSlectionDataset = new F1512DistrictSelectionData();

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// To get the Form 2000 Permission
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getForm2000DetailsData = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>


        private F26000ParcelHeaderFormData.f26000ClassCodeDataTable classCodeDataTable = new F26000ParcelHeaderFormData.f26000ClassCodeDataTable();

        private F26000ParcelHeaderFormData.f26000_pclst_AppraisalTypeDataTable ApprisalTypeTable = new F26000ParcelHeaderFormData.f26000_pclst_AppraisalTypeDataTable();

        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Unique nbhdID 
        /// </summary>
        private int nbhdID;

        /// <summary>
        /// stateCode
        /// </summary>
        private int outValue;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Roll Year
        /// </summary>
        private string rollYear;

        /// <summary>
        /// districtId
        /// </summary>
        private string districtId;

        /// <summary>
        /// Instance of 8106 Controller to call the WorkItem
        /// </summary>
        private F26000Controller form26000Control;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store the parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// tempprimaryowner
        /// </summary>
        private int tempprimaryowner;

        /// <summary>
        /// tempprimaylandtype
        /// </summary>
        private int tempprimaylandtype;

        /// <summary>
        /// 
        /// </summary>
        private int greenBeltComboIndex;
        /// <summary>
        /// tifId
        /// </summary>
        private int tifFundId;
        /// <summary>
        /// tempClassCode
        /// </summary>
        private string tempClassCode;

        /// <summary>
        /// templength
        /// </summary>
        private int templength = 0;
        /// <summary>
        /// locationId
        /// </summary>
        private int locationId;
        /// <summary>
        /// groupingId
        /// </summary>
        private int groupingId;
        /// <summary>
        /// exemptionId
        /// </summary>
        private int exemptionId;
        /// <summary>
        /// exemptionCode
        /// </summary>
        private string exemptionCode;



        ////private List<DataSetCollection> dataSetCollection;

        // private int previousCursorindex;

        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25000"/> class.
        /// </summary>
        public F26000()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8042"/> class.
        /// </summary>
        /// <param name="masterform">The master Form No</param>
        /// <param name="formNo">The Form No</param>
        /// <param name="keyID">KeyID</param>
        /// <param name="red">Picturebox color</param>
        /// <param name="green">pictureboxcolor</param>
        /// <param name="blue">picturebox color</param>
        /// <param name="tabText">tabText</param>
        /// <param name="permissionEdit">permissionEdit</param>
        public F26000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.ParcelHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelHeaderPictureBox.Height, this.ParcelHeaderPictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        ///// <summary>
        ///// event publication for getting the form status
        ///// </summary>
        //[EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        //public event EventHandler<DataEventArgs<string>> GetFormStatus;

        ///// <summary>
        ///// Get Cancel Button
        ///// </summary>
        //[EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        //public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        #endregion Event Publication

        #region Property
        /// <summary>
        /// For F25000Control
        /// </summary>
        [CreateNew]
        public F26000Controller Form26000Control
        {
            get { return this.form26000Control as F26000Controller; }
            set { this.form26000Control = value; }
        }
        #endregion Property

        #region Event Subscription

        /// <summary>
        ///  Called when [D9030_ F9030_ load slice details].
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

                    if (this.ParcelHeaderFormData.F26000ParcelHeader.Rows.Count > 0)
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
        /// OnD9030_F9030_LoadSliceDetails
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            // Checks the masterform no is same  
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                // Coding Added for while navigating the record will not get display because keyid is passing as -99.
                // if we add this code it will get a selected keyid.and it will display a record.
                this.keyID = eventArgs.Data.SelectedKeyId;
                // Added coding ends here

                // Checks its not in View Mode 
                if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                {
                    this.keyID = eventArgs.Data.SelectedKeyId;
                    this.flagLoadOnProcess = true;
                    this.navigationFlag = false;
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }
                else if (this.keyID == eventArgs.Data.SelectedKeyId)  // IF the id is same from parcel owner then needs to refresh the form
                {
                    this.flagLoadOnProcess = true;
                    this.navigationFlag = false;
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
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
            this.flagLoadOnProcess = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.ClearControls();
            this.LoadDefaultView();
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {

        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {
                this.SaveButtonClick();
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

        #endregion Event Subscription

        #region user Defined methods

        /// <summary>
        /// Loads the default view of the page with controls enabled/Disabled accordingly
        /// </summary>
        private void LoadDefaultView()
        {
            this.ShowPanel(true);
            this.ShowControls(true);
            this.F26000GetParcelDetails();
            byte isStateNE;
            if (this.ParcelHeaderFormData.GetConfigState.Rows.Count > 0)
            {
                F26000ParcelHeaderFormData.GetConfigStateRow fffgggg = (F26000ParcelHeaderFormData.GetConfigStateRow)this.ParcelHeaderFormData.GetConfigState.Rows[0];
                if (!fffgggg.IsIsCfgStateNENull())
                {
                    byte.TryParse(fffgggg.IsCfgStateNE.ToString(), out isStateNE);
                    if (isStateNE == 0)
                    {
                        this.AgriLandTaxCreditLabel.Visible = false;                        
                        this.AgriLandTaxCreditTextBox.Visible = false;
                        this.CurrStateRealTaxCreditpanel.Visible = false;
                        this.CurrStateAgriRealTaxCreditpanel.Visible = false;
                        this.CurrStateUnusedTaxCreditpanel.Visible = false;
                        this.Size = new System.Drawing.Size(804, 490);
                        this.ParcelHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(481, 42, "Identification", 28, 81, 128);
                    }
                    else
                    {
                        this.AgriLandTaxCreditLabel.Visible = true;
                        this.AgriLandTaxCreditTextBox.Visible = true;
                        this.CurrStateRealTaxCreditpanel.Visible = true;
                        this.CurrStateAgriRealTaxCreditpanel.Visible = true;
                        this.CurrStateUnusedTaxCreditpanel.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Loads the ParcelHeaderDetails
        /// </summary>
        private void F26000GetParcelDetails()
        {
            this.ParcelHeaderFormData.EnforceConstraints = false;
            this.ParcelHeaderFormData = this.form26000Control.WorkItem.F26000_GetParcelFormDetails(this.keyID);


            if (this.ParcelHeaderFormData.F26000ParcelHeader.Rows.Count > 0)
            {
                int.TryParse(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ParcelIDColumn].ToString(), out this.parcelId);
                string parcelNofield = string.Empty;
                parcelNofield = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ParcelNumberColumn].ToString();
                if (parcelNofield.Contains("&"))
                {
                    parcelNofield = parcelNofield.Replace("&", "&&");
                }
                this.ParcelNolinkLabel.Text = parcelNofield;
                this.rollYear = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.RollYearColumn].ToString();
                this.TypeTextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.TypeColumn].ToString();
                this.RetiredTextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.RetiredColumn].ToString();
                this.ExemptTextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptColumn].ToString();
                this.DescriptionTextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.DescriptionColumn].ToString();
                string primaryowner = string.Empty;
                primaryowner = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.PrimaryOwnerColumn].ToString();
                if (primaryowner.Contains("&"))
                {
                    primaryowner = primaryowner.Replace("&", "&&");
                }
                ////this.PrimaryOwnerlinkLabel.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.PrimaryOwnerColumn].ToString();
                this.PrimaryOwnerlinkLabel.Text = primaryowner;

                string primarysitus = string.Empty;
                primarysitus = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.SitusColumn].ToString();
                if (primarysitus.Contains("&"))
                {
                    primarysitus = primarysitus.Replace("&", "&&");
                }
                this.PrimarySituslinkLabel.Text = primarysitus;
                /// #Bug TSBG:12124  issue fixed in Legal Field Description.
                string legalField = string.Empty;
                legalField = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LegalColumn].ToString();
                if (legalField.Contains("&"))
                {
                    legalField = legalField.Replace("&", "&&");
                }
                this.LegallinkLabel.Text = legalField;
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AutoCompleteValueColumn].ToString()))
                {
                    this.classCodeConfigValue = Convert.ToInt32(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AutoCompleteValueColumn]);
                }
                string ClassCodeField = string.Empty;
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ClassCodeColumn].ToString()))
                {

                    ClassCodeField = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ClassCodeColumn].ToString();

                    StringBuilder sb = new StringBuilder();
                    if (!string.IsNullOrEmpty(ClassCodeField))
                    {
                        List<string> result = new List<string>(Regex.Split(ClassCodeField, @"(?<=\G.{2})", RegexOptions.Singleline));
                        var count = result.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (result[i].Length.Equals(2))
                            {
                                if (sb.ToString().Length < 17)
                                {
                                    string temp1 = result[i].Insert(2, " ").ToString();

                                    sb.Append(temp1);
                                }

                            }
                            else
                            {
                                if (sb.ToString().Length < 17)
                                {
                                    sb.Append(result[i].ToString());
                                }
                            }

                        }
                        ClassCodeField = sb.ToString();

                    }



                    //this.ClassCodeComboBox.SelectedValue = ClassCodeField;
                    this.ClassCodeComboBox.Text = ClassCodeField;
                    this.ClassCodeTextBox.Text = ClassCodeField;
                    // ClassCodeTextBox.TextChanged += new EventHandler(this.ClassCodeTextBox_TextChanged);
                    //int tempForeColor = 0;
                    string temp = (this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ClassCodeColorRGBRColumn].ToString());
                    string[] array = temp.Split(',');
                    if (array.Length > 0)
                    {
                        int R = 0;
                        int G = 0;
                        int B = 0;
                        if (array[0].Length > 0)
                        {
                            R = Convert.ToInt32(array[0]);
                        }
                        if (array[1].Length > 0)
                        {
                            G = Convert.ToInt32(array[1]);
                        }
                        if (array[2].Length > 0)
                        {
                            B = Convert.ToInt32(array[2]);
                        }
                        Color foreColor = Color.FromArgb(R, G, B);
                        if (foreColor != null)
                        {
                            this.ClassCodeComboBox.ForeColor = foreColor;
                        }
                    }
                    //var color = temp;
                    //tempForeColor = Convert.ToInt32(temp);

                    //this.ClassCodeTextBox.ForeColor = System.Drawing.Color.FromArgb(temp);

                }
                else
                {
                    this.ClassCodeComboBox.Text = ClassCodeField;
                    this.ClassCodeTextBox.Text = ClassCodeField;
                }


                int.TryParse(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.NBHDIDColumn].ToString(), out this.nbhdID);

                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb27,2009 as per the client to show the MIDs value in Mask Value Format

                this.ID1TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask1Column].ToString();
                this.ID2TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask2Column].ToString();
                this.ID3TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask3Column].ToString();
                this.ID4TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask4Column].ToString();
                this.ID5TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask5Column].ToString();
                this.ID6TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask6Column].ToString();
                this.ID7TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask7Column].ToString();
                this.ID8TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask8Column].ToString();

                //// Coding modified for the issue 1517
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID1Column].ToString()))
                {
                    this.ID1TextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID1Column].ToString();
                }
                else
                {
                    this.ID1TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID2Column].ToString()))
                {
                    this.ID2TextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID2Column].ToString();
                }
                else
                {
                    this.ID2TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID3Column].ToString()))
                {
                    string strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID3Column].ToString();
                    ////Added for 25000 CO on 12/7/2012 by purushotham
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.0####", CultureInfo.InvariantCulture);
                    this.ID3TextBox.Text = strResult;
                }
                else
                {
                    this.ID3TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID4Column].ToString()))
                {
                    this.ID4TextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID4Column].ToString();
                }
                else
                {
                    this.ID4TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID5Column].ToString()))
                {
                    this.ID5TextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID5Column].ToString();
                }
                else
                {
                    this.ID5TextBox.Text = string.Empty;
                }

                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID6Column].ToString()))
                {
                    this.ID6TextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID6Column].ToString();
                }
                else
                {
                    this.ID6TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID7Column].ToString()))
                {
                    this.ID7TextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID7Column].ToString();
                }
                else
                {
                    this.ID7TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID8Column].ToString()))
                {
                    this.ID8TextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.MID8Column].ToString();
                }
                else
                {
                    this.ID8TextBox.Text = string.Empty;
                }
                ////1517 ends here
                string districtfield = string.Empty;
                districtfield = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.DistrictColumn].ToString();
                if (districtfield.Contains("&"))
                {
                    districtfield = districtfield.Replace("&", "&&");
                }
                this.DistrictlinkLabel.Text = districtfield;

                //this.DistrictlinkLabel.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.DistrictColumn].ToString();
                this.districtId = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.DistrictIDColumn].ToString();
                int.TryParse(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LocationIDColumn].ToString(), out this.locationId);
                int.TryParse(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionIDColumn].ToString(), out this.exemptionId);
               // int.TryParse(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.TIFFundIDColumn].ToString(), out this.tifFundId);
                int.TryParse(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.GroupingIDColumn].ToString(), out this.groupingId);
                //this.locationId = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.].ToString();
                string Neighborhoodfield = string.Empty;
                Neighborhoodfield = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.NeighborhoodGroupColumn].ToString();
                if (Neighborhoodfield.Contains("&"))
                {
                    Neighborhoodfield = Neighborhoodfield.Replace("&", "&&");
                }
                this.NeighborhoodGrouplinkLabel.Text = Neighborhoodfield;

                this.EventslinkLabel.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.EventsColumn].ToString();
                ////Comment by Purushotham not retruning EventId from DB

                // int.TryParse(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.EventIDColumn].ToString(), out this.eventID);
                ////Coding modified for the issue 1517
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LabelColumn].ToString()))
                {
                    this.ID1label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LabelColumn].ToString() + ":";
                }
                else
                {
                    this.ID1label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe2Column].ToString()))
                {
                    this.ID2label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe2Column].ToString() + ":";
                }
                else
                {
                    this.ID2label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe3Column].ToString()))
                {
                    this.ID3label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe3Column].ToString() + ":";
                }
                else
                {
                    this.ID3label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe4Column].ToString()))
                {
                    this.ID4label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe4Column].ToString() + ":";
                }
                else
                {
                    this.ID4label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe5Column].ToString()))
                {
                    this.ID5label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe5Column].ToString() + ":";
                }
                else
                {
                    this.ID5label.Text = string.Empty;
                }

                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe6Column].ToString()))
                {
                    this.ID6label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe6Column].ToString() + ":";
                }
                else
                {
                    this.ID6label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe7Column].ToString()))
                {
                    this.ID7label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe7Column].ToString() + ":";
                }
                else
                {
                    this.ID7label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe8Column].ToString()))
                {
                    this.ID8label.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Labe8Column].ToString() + ":";
                }
                else
                {
                    this.ID8label.Text = string.Empty;
                }
                ////1517 ends here
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ImprovementColumn].ToString()))
                {
                    this.PrimaryImprovementCombo.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ImprovementColumn].ToString();
                    this.tempprimaryowner = (int)this.PrimaryImprovementCombo.SelectedValue;
                }
                else
                {
                    this.PrimaryImprovementCombo.SelectedValue = 0;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LandTypeColumn].ToString()))
                {
                    this.PrimaryLandTypeComboBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LandTypeColumn].ToString();
                    this.tempprimaylandtype = (int)this.PrimaryLandTypeComboBox.SelectedValue;
                }
                else
                {
                    this.PrimaryLandTypeComboBox.SelectedValue = 0;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AppraisalTypeColumn].ToString()))
                {
                    this.ApprisalTypeComboBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AppraisalTypeColumn].ToString();
                   // this.tempprimaylandtype = (int)this.ApprisalTypeComboBox.SelectedValue;
                }
                else
                {
                    this.ApprisalTypeComboBox.SelectedValue = 0;
                }

                this.MasktextboxAlignment();

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AssessorLocationColumn].ToString()))
                {
                    this.AssessorLocationLinkLabel.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AssessorLocationColumn].ToString();
                }
                else
                {
                    this.AssessorLocationLinkLabel.Text = "<<>>";
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ValuationGroupingColumn].ToString()))
                {
                    this.ValutaionGroupingLinkLabel.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ValuationGroupingColumn].ToString();
                }
                else
                {
                    this.ValutaionGroupingLinkLabel.Text = "<<>>";
                }

                //if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.TIFFundColumn].ToString()))
                //{
                //    this.TIFFundLinkLabel.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.TIFFundColumn].ToString();
                //}
                //else
                //{
                //    this.TIFFundLinkLabel.Text = "<<>>";
                //}

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionCodeColumn].ToString()))
                {
                    this.ExemptionCodeLinkLabel.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionCodeColumn].ToString();
                }
                else
                {
                    this.ExemptionCodeLinkLabel.Text = "<<>>";
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptFromAmountColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptFromAmountColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                    this.ExemptFromAmountTextBox.Text = strResult;
                }
                else
                {
                    this.ExemptFromAmountTextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionAmountColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionAmountColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                    this.ExemptionReductionTextBox.Text = strResult;
                }
                else
                {
                    this.ExemptionReductionTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionPercentColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionPercentColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    strNumb = strNumb * 100;
                    var strResult = strNumb.ToString("#,##0.##", CultureInfo.InvariantCulture);
                    this.PercentageTextBox.Text = strResult + "%";
                }
                else
                {
                    this.PercentageTextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.IsGreenbeltColumn].ToString()))
                {
                    if (this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.IsGreenbeltColumn].ToString().Equals("True"))
                    {
                        this.greenBeltComboIndex = 1;
                        this.GreenbeltAreaComboBox.SelectedIndex = 1;
                    }
                    else
                    {
                        this.greenBeltComboIndex = 2;
                        this.GreenbeltAreaComboBox.SelectedIndex = 2;
                    }
                }
                else
                {
                    this.GreenbeltAreaComboBox.SelectedIndex = 0;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.GreenbeltFilingDateColumn].ToString()))
                {
                    this.GreenbeltFillingDateTextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.GreenbeltFilingDateColumn].ToString();
                }
                else
                {
                    this.GreenbeltFillingDateTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.GreenbeltMarketValueColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.GreenbeltMarketValueColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                    this.GreenbeltMarketValueTextBox.Text = strResult;
                }
                else
                {
                    this.GreenbeltMarketValueTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.GreenbeltValueLossColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.GreenbeltValueLossColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                    this.GreenbeltValueLossTextBox.Text = strResult;
                }
                else
                {
                    this.GreenbeltValueLossTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.PropertyPenaltyColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.PropertyPenaltyColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00", CultureInfo.InvariantCulture);
                    this.PropertyPenalityTextBox.Text = strResult;
                }
                else
                {
                    this.PropertyPenalityTextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.TaxCreditColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.TaxCreditColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00", CultureInfo.InvariantCulture);
                    this.TaxCreditTextBox.Text = strResult;
                }
                else
                {
                    this.TaxCreditTextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.UnusedTaxCreditColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.UnusedTaxCreditColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00", CultureInfo.InvariantCulture);
                    this.UnUsedTaxCreditTextBox.Text = strResult;
                }
                else
                {
                    this.UnUsedTaxCreditTextBox.Text = string.Empty;
                }

                //Modified for TSCO - #22049
                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AgLandTaxCreditColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AgLandTaxCreditColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00", CultureInfo.InvariantCulture);
                    this.AgriLandTaxCreditTextBox.Text = strResult;
                }
                else
                {
                    this.AgriLandTaxCreditTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.CurrStatRealTaxCreditColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.CurrStatRealTaxCreditColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00", CultureInfo.InvariantCulture);
                    this.CurrStateRealTaxCreditTextBox.Text = strResult;
                }
                else
                {
                    this.CurrStateRealTaxCreditTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.CurrStatAglandTaxCreditColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.CurrStatAglandTaxCreditColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00", CultureInfo.InvariantCulture);
                    this.CurrStateAgriLandTaxCreditTextBox.Text = strResult;
                }
                else
                {
                    this.CurrStateAgriLandTaxCreditTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.CurrStatUnusedTaxCreditColumn].ToString()))
                {
                    var strTemp = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.CurrStatUnusedTaxCreditColumn].ToString();
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.00", CultureInfo.InvariantCulture);
                    this.CurrStateUnusedTaxCreditTextBox.Text = strResult;
                }
                else
                {
                    this.CurrStateUnusedTaxCreditTextBox.Text = string.Empty;
                }

                ////Added By Malliga for C0(April 2008)
                this.AssessmentTypeTextBox.Text = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.AssessmentTypeColumn].ToString();

                if (((!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAppraisalByColumn].ToString())) && (this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAppraisalByColumn].ToString() != "0")))
                {
                    this.AppRedPictureBox.Visible = true;
                    this.AppRedPictureBox.BringToFront();

                    this.AppGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AppGreenpictureBox.Visible = true;
                    this.AppGreenpictureBox.BringToFront();

                    this.AppRedPictureBox.Visible = false;
                }

                if (((!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAdminByColumn].ToString())) && (this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAdminByColumn].ToString() != "0")))
                {
                    this.AdminRedpictureBox.Visible = true;
                    this.AdminRedpictureBox.BringToFront();

                    this.AdminGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AdminRedpictureBox.Visible = false;
                    this.AdminGreenpictureBox.Visible = true;

                    this.AdminGreenpictureBox.BringToFront();
                }

                this.ReviewLinkLabel.Text = "Status";
                this.ShowPanel(true);
                this.ShowControls(true);
                if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                {
                    this.ShowControls(false);
                    this.ShowLock(false);
                }
                else
                {
                    this.ShowPanel(true);
                    this.ShowControls(true);
                    this.ShowLock(true);
                }
            }
            else
            {
                this.AppGreenpictureBox.Visible = true;
                this.AppGreenpictureBox.BringToFront();
                this.AppRedPictureBox.Visible = false;
                this.AdminRedpictureBox.Visible = false;
                this.AdminGreenpictureBox.Visible = true;
                this.AdminGreenpictureBox.BringToFront();
                this.ClearControls();
                this.ShowPanel(false);
                this.ShowControls(false);
                this.ShowLock(false);
            }

            this.ParcelNolinkLabel.Focus();
        }

        /// <summary>
        /// Clears all the controls
        /// </summary>
        private void ClearControls()
        {
            this.ParcelNolinkLabel.Text = string.Empty;
            //// Added local variable instead of the link label as per CO :4988
            this.rollYear = string.Empty;
            this.TypeTextBox.Text = string.Empty;
            this.RetiredTextBox.Text = string.Empty;
            this.ExemptTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.PrimaryOwnerlinkLabel.Text = string.Empty;
            this.PrimarySituslinkLabel.Text = string.Empty;
            this.LegallinkLabel.Text = string.Empty;
            this.ClassCodeTextBox.Text = string.Empty;
            this.ID1TextBox.Text = string.Empty;
            this.ID2TextBox.Text = string.Empty;
            this.ID3TextBox.Text = string.Empty;
            this.ID4TextBox.Text = string.Empty;
            this.ID5TextBox.Text = string.Empty;
            this.ID6TextBox.Text = string.Empty;
            this.ID7TextBox.Text = string.Empty;
            this.ID8TextBox.Text = string.Empty;
            this.DistrictlinkLabel.Text = string.Empty;
            this.NeighborhoodGrouplinkLabel.Text = string.Empty;
            this.EventslinkLabel.Text = string.Empty;
            this.PrimaryImprovementCombo.SelectedIndex = -1;
            this.PrimaryLandTypeComboBox.SelectedIndex = -1;
            this.ReviewLinkLabel.Text = string.Empty;
            this.AssessorLocationLinkLabel.Text = string.Empty;
            this.ValutaionGroupingLinkLabel.Text = string.Empty;
           // this.TIFFundLinkLabel.Text = string.Empty;
            this.ExemptionCodeLinkLabel.Text = string.Empty;
            this.ExemptFromAmountTextBox.Text = string.Empty;
            this.ExemptionReductionTextBox.Text = string.Empty;
            this.PercentageTextBox.Text = string.Empty;
            this.GreenbeltAreaComboBox.SelectedIndex = -1;
            this.GreenbeltFillingDateTextBox.Text = string.Empty;
            this.GreenbeltMarketValueTextBox.Text = string.Empty;
            this.GreenbeltValueLossTextBox.Text = string.Empty;
            this.PropertyPenalityTextBox.Text=string.Empty;
            this.TaxCreditTextBox.Text=string.Empty;
            this.UnUsedTaxCreditTextBox.Text = string.Empty;
        }


        /// <summary>
        /// Enables or disables the Panels accordingly
        /// </summary>
        /// <param name="show">bool value to enable/Disable</param>
        private void ShowPanel(bool show)
        {
            this.ParcelNumberPanel.Enabled = show;
            this.Retiredpanel3.Enabled = show;
            this.Typepanel2.Enabled = show;
            this.Exemptpanel4.Enabled = show;
            this.lockspanel5.Enabled = show;
            this.PrimaryImprovementpanel.Enabled = show;
            this.PrimaryLandTypepanel.Enabled = show;
            this.Descriptionpanel6.Enabled = show;
            this.primaryownerpanel7.Enabled = show;
            this.primarySituspanel8.Enabled = show;
            this.legalpanel9.Enabled = show;
            this.ClassCodePanel.Enabled = show;
            this.ID1panel11.Enabled = show;
            this.ID2panel12.Enabled = show;
            this.ID3panel13.Enabled = show;
            this.ID4panel14.Enabled = show;
            this.ID5panel15.Enabled = show;
            this.ID6panel16.Enabled = show;
            this.ID7panel17.Enabled = show;
            this.ID8panel18.Enabled = show;
            this.Districtpanel16.Enabled = show;
            this.Neighborhoodpanel17.Enabled = show;
            this.Eventpanel18.Enabled = show;
            this.AessmentTypePanel.Enabled = show;
            this.ReviewPanel.Enabled = show;
            //newly added items
            this.AssessorLocationPanel.Enabled = show;
            this.ValutaionGroupPanel.Enabled = show;
            //this.TIFFundPanel.Enabled = show;
            this.ExemptionCodePanel.Enabled = show;
            this.ExemptfromAmountPanel.Enabled = show;
            this.ExemptionReductionPanel.Enabled = show;
            this.PercentagePanel.Enabled = show;
            this.GreenbeltAreaPanel.Enabled = show;
            this.GreenbeltFillingDatePanel.Enabled = show;
            this.GreenbeltMarketValuePanel.Enabled = show;
            this.GreenbeltValueLossPanel.Enabled = show;
            this.UnusedTaxCreditpanel.Enabled = show;
            this.TaxCreditPanel.Enabled = show;
            this.PropertyPenaltyPanel.Enabled = show;

        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [lock control].</param>
        private void ShowControls(bool show)
        {
            this.ParcelNolinkLabel.Enabled = show;
            this.PrimaryOwnerlinkLabel.Enabled = show;
            this.PrimarySituslinkLabel.Enabled = show;
            this.LegallinkLabel.Enabled = show;
            this.ClassCodeTextBox.Enabled = show;
            this.ID1TextBox.Enabled = show;
            this.ID2TextBox.Enabled = show;
            this.ID3TextBox.Enabled = show;
            this.ID4TextBox.Enabled = show;
            this.ID5TextBox.Enabled = show;
            this.ID6TextBox.Enabled = show;
            this.ID7TextBox.Enabled = show;
            this.ID8TextBox.Enabled = show;
            this.DistrictlinkLabel.Enabled = show;
            this.NeighborhoodGrouplinkLabel.Enabled = show;
            this.EventslinkLabel.Enabled = show;
            this.ReviewLinkLabel.Enabled = show;
            //newly added items
            this.AssessorLocationPanel.Enabled = show;
            this.ValutaionGroupPanel.Enabled = show;
           // this.TIFFundPanel.Enabled = show;
            this.ExemptionCodePanel.Enabled = show;
            this.ExemptfromAmountPanel.Enabled = show;
            this.ExemptionReductionPanel.Enabled = show;
            this.PercentagePanel.Enabled = show;
            this.GreenbeltAreaPanel.Enabled = show;
            this.GreenbeltFillingDatePanel.Enabled = show;
            this.GreenbeltMarketValuePanel.Enabled = show;
            this.GreenbeltValueLossPanel.Enabled = show;
            this.PropertyPenalityTextBox.Enabled = show;
            this.TaxCreditTextBox.Enabled = show;
            this.UnUsedTaxCreditTextBox.Enabled = show;
        }


        /// <summary>
        /// Enables or disables the Panels accordingly
        /// </summary>
        /// <param name="show">bool value to enable/Disable</param>
        private void ShowLock(bool show)
        {
            this.AppGreenpictureBox.Enabled = show;
            this.AppRedPictureBox.Enabled = show;
            this.AdminRedpictureBox.Enabled = show;
            this.AdminGreenpictureBox.Enabled = show;
        }

        /// <summary>
        /// Primaries the implementation.
        /// </summary>
        private void PrimaryImplementation()
        {
            this.ImprovementListData = this.form26000Control.WorkItem.PrimaryImprovementList();
            if (this.ImprovementListData.f26000ListParcelImprovement.Rows.Count > 0)
            {
                this.PrimaryImprovementCombo.DataSource = this.ImprovementListData.f26000ListParcelImprovement;
                this.PrimaryImprovementCombo.DisplayMember = this.ImprovementListData.f26000ListParcelImprovement.ImprovementColumn.ColumnName;
                this.PrimaryImprovementCombo.ValueMember = this.ImprovementListData.f26000ListParcelImprovement.ImprovementIDColumn.ColumnName;
            }
        }


        /// <summary>
        /// Primaries the type of the land.
        /// </summary>
        private void PrimaryLandType()
        {
            this.LandtypeData = this.form26000Control.WorkItem.PrimaryLandTypeList();
            if (this.LandtypeData.f26000ListParcelLandTypes.Rows.Count > 0)
            {
                this.PrimaryLandTypeComboBox.DataSource = this.LandtypeData.f26000ListParcelLandTypes;
                this.PrimaryLandTypeComboBox.DisplayMember = this.LandtypeData.f26000ListParcelLandTypes.LandTypeColumn.ColumnName;
                this.PrimaryLandTypeComboBox.ValueMember = this.LandtypeData.f26000ListParcelLandTypes.LandTypeIDColumn.ColumnName;
            }
        }

        private void LoadApprisalTypeCombo()
        {
            this.ApprisalTypeTable.Clear();
            this.ApprisalTypeTable = this.form26000Control.WorkItem.F26000_GetApprisalType().f26000_pclst_AppraisalType;
            if (this.ApprisalTypeTable.Rows.Count > 0)
            {
                DataRow customRow = this.ApprisalTypeTable.NewRow();
                customRow[this.ApprisalTypeTable.AppraisalTypeColumn.ColumnName] = string.Empty;
                customRow[this.ApprisalTypeTable.AppraisalTypeIDColumn.ColumnName] = "0";
                this.ApprisalTypeTable.Rows.InsertAt(customRow,0);
                //this.ApprisalTypeTable.Rows.(customRow);
               // this.ApprisalTypeTable.Merge(this.ApprisalTypeTable);
                this.ApprisalTypeComboBox.DataSource = this.ApprisalTypeTable;
                this.ApprisalTypeComboBox.DisplayMember = this.ApprisalTypeTable.AppraisalTypeColumn.ColumnName;
                this.ApprisalTypeComboBox.ValueMember = this.ApprisalTypeTable.AppraisalTypeIDColumn.ColumnName;
            }

        }
        /// <summary>
        /// Masktextboxes the alignment.
        /// </summary>
        private void MasktextboxAlignment()
        {
            this.ID1TextBox.SelectionStart = 0;
            this.ID2TextBox.SelectionStart = 0;
            this.ID3TextBox.SelectionStart = 0;
            this.ID4TextBox.SelectionStart = 0;
            this.ID5TextBox.SelectionStart = 0;
            this.ID6TextBox.SelectionStart = 0;
            this.ID7TextBox.SelectionStart = 0;
            this.ID8TextBox.SelectionStart = 0;
        }

        #endregion user Defined methods


        #region Event

        /// <summary>
        /// Loaads the data
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void F26000_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009
                this.PrimaryImplementation();
                this.PrimaryLandType();
                this.LoadApprisalTypeCombo();
                this.LoadDefaultView();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
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
        /// ParcelNolinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ParcelNolinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.getForm2000DetailsData = this.form26000Control.WorkItem.GetFormDetails(Convert.ToInt32(2000), TerraScanCommon.UserId);
                if (this.getForm2000DetailsData.Rows.Count > 0)
                {
                    Boolean.TryParse(this.getForm2000DetailsData.Rows[0][getForm2000DetailsData.IsPermissionOpenColumn.ColumnName].ToString(), out this.form2000OpenPermission);
                }

                Form parcelStatusForm = new Form();
                object[] optionalParameter = new object[] { this.keyID };
                parcelStatusForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2000, optionalParameter, this.form26000Control.WorkItem);
                if (parcelStatusForm != null && this.form2000OpenPermission)
                {
                    parcelStatusForm.ShowDialog();

                    //// Fixed Bug # 5434 by A.Shanmuga Sundaram on March 16th'09
                    //this.LoadDefaultView();
                    SliceReloadActiveRecord currentKeyIdInfo;
                    currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                    currentKeyIdInfo.SelectedKeyId = this.keyID;
                    SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = this.keyID;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                }
                else
                {
                    if (!TerraScanCommon.IsFieldUser)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + "Parcel Status Form", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// EventslinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void EventslinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(24000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.parcelId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// PrimarySituslinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void PrimarySituslinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20003);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// LegallinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void LegallinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20009);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// DORlinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void DORlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //    try
            //    {
            //        Form statecode = new Form();
            //        object[] optionalParameters = new object[] { this.ClassCodeTextBox.Text };
            //        statecode = this. form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2010, optionalParameters, this. form26000Control.WorkItem);
            //        if (statecode != null)
            //        {
            //            if (statecode.ShowDialog() == DialogResult.OK)
            //            {
            //                string DORLabel = string.Empty;
            //                DORLabel = TerraScanCommon.GetValue(statecode, "StateCodeNameReturnValue");
            //                if (DORLabel.Contains("&"))
            //                {
            //                    ////Comment to resolve TFS#19535 bug by Purushotham on 12/April/2013
            //                    DORLabel = DORLabel.Replace("&", "&&");
            //                }
            //                this.ClassCodeTextBox.Text = DORLabel;
            //            }
            //        }
            //    }
            //    catch (Exception e1)
            //    {
            //        ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //    }
            //    finally
            //    {
            //        this.Cursor = Cursors.Default;
            //    }
        }

        /// <summary>
        /// DistrictlinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param> 
        private void DistrictlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form districtF15122 = new Form();
                //// Added local variable instead of the link label as per CO :4988
                object[] optionalParameter = new object[] { this.rollYear, this.ParentFormId };
                districtF15122 = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1512, optionalParameter, this.form26000Control.WorkItem);

                DialogResult districtDialog;
                if (districtF15122 != null)
                {
                    districtDialog = districtF15122.ShowDialog();

                    if (districtDialog == DialogResult.OK)
                    {
                        try
                        {
                            this.districtId = TerraScanCommon.GetValue(districtF15122, "DistrictId");
                            this.districtSlectionDataset = this.form26000Control.WorkItem.F1512_GetDistrictSelectionData(this.ConvertStringToInteger(this.districtId), "", "", -999);

                            if (this.districtSlectionDataset.ListDistrictSelection.Rows.Count > 0)
                            {
                                string districtfield = string.Empty;
                                districtfield = this.districtSlectionDataset.ListDistrictSelection.Rows[0][this.districtSlectionDataset.ListDistrictSelection.DistrictColumn].ToString() + " - " + this.districtSlectionDataset.ListDistrictSelection.Rows[0][this.districtSlectionDataset.ListDistrictSelection.DescriptionColumn].ToString();
                                if (districtfield.Contains("&"))
                                {
                                    districtfield = districtfield.Replace("&", "&&");
                                }
                                this.DistrictlinkLabel.Text = districtfield;

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
        /// NeighborhoodGrouplinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void NeighborhoodGrouplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form subfundForm = new Form();
                //// Added local variable instead of the link label(RolllinkLabel) as per CO :4988
                object[] optionalParameter = new object[] { this.rollYear.Trim() };
                subfundForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3510, optionalParameter, this.form26000Control.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.nbhdID = Convert.ToInt32(TerraScanCommon.GetValue(subfundForm, "NeighborId"));
                        string neighborhoodLabel = string.Empty;
                        neighborhoodLabel = TerraScanCommon.GetValue(subfundForm, "NeighborName") + " / " + TerraScanCommon.GetValue(subfundForm, "RollYear");
                        if (neighborhoodLabel.Contains("&"))
                        {
                            neighborhoodLabel = neighborhoodLabel.Replace("&", "&&");
                        }
                        ////Commented by purushotham No Column USerWarning is returning 
                        //string strMessage = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.UserWarningColumn.ColumnName].ToString();
                        //if (string.IsNullOrEmpty(strMessage))
                        //{
                        //    this.NeighborhoodGrouplinkLabel.Text = neighborhoodLabel;


                        //}
                        //else
                        //{
                        //    DialogResult ds = TerraScanMessageBox.Show(strMessage, "Terrascan T2 - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    if (ds.Equals(DialogResult.OK))
                        //    {
                        //        // this.DialogResult = DialogResult.No;

                        //    }
                        //}

                        ////UnCommented by purushotham
                        this.NeighborhoodGrouplinkLabel.Text = neighborhoodLabel;
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
        /// PrimaryOwnerlinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void PrimaryOwnerlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(22006);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// ID1linkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ID1linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20008);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// ID2linkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ID2linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20008);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// ID3linkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ID3linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20008);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// ID4linkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ID4linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20008);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// ID5linkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ID5linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20008);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// ToolTip
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ParcelHeaderPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelHeaderToolTip.SetToolTip(this.ParcelHeaderPictureBox, Utility.GetFormNameSpace(this.Name));
        }

        /// <summary>
        /// AppraisalGreenButtonClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AppGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form subfundForm = new Form();
                object[] optionalParameter = new object[] { this.keyID, "2001" };
                subfundForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form26000Control.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.LoadLock();
                        this.AppRedPictureBox.Focus();
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
        /// AppraisalGreenButton MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AppGreenpictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ParcelHeaderToolTip.SetToolTip(this.AppGreenpictureBox, SharedFunctions.GetResourceString("ParcelheaderAppraisal"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AdminGreenButton Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdminGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form subfundForm = new Form();
                object[] optionalParameter = new object[] { this.keyID, "2003" };
                subfundForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form26000Control.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.LoadLock();
                        this.AdminRedpictureBox.Focus();
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
        /// AdminGreenButton MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdminGreenpictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ParcelHeaderToolTip.SetToolTip(this.AdminGreenpictureBox, SharedFunctions.GetResourceString("ParcelheaderAdmin"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AdminRedButton Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdminRedpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form subfundForm = new Form();
                object[] optionalParameter = new object[] { this.keyID, "2003" };
                subfundForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form26000Control.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.LoadLock();
                        this.AdminGreenpictureBox.Focus();
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
        /// AdminRed MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdminRedpictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ParcelHeaderToolTip.SetToolTip(this.AdminRedpictureBox, SharedFunctions.GetResourceString("ParcelheaderAdmin"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AppraisalRedButton Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AppRedPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form subfundForm = new Form();
                /////short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                object[] optionalParameter = new object[] { this.keyID, "2001" };
                subfundForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form26000Control.WorkItem);
                ////subfundForm.ShowDialog(); 
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.LoadLock();
                        this.AppGreenpictureBox.Focus();
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
        /// AppraisalRedButton MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AppRedPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ParcelHeaderToolTip.SetToolTip(this.AppRedPictureBox, SharedFunctions.GetResourceString("ParcelheaderAppraisal"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Reload the Lock while clicking the Lock Buttons
        /// </summary>
        private void LoadLock()
        {
            this.ParcelHeaderFormData = this.form26000Control.WorkItem.F26000_GetParcelFormDetails(this.keyID);

            if (this.ParcelHeaderFormData.F26000ParcelHeader.Rows.Count > 0)
            {
                if (((!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAppraisalByColumn].ToString())) && (this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAppraisalByColumn].ToString() != "0")))
                {
                    this.AppRedPictureBox.Visible = true;
                    this.AppRedPictureBox.BringToFront();
                    this.AppGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AppGreenpictureBox.Visible = true;
                    this.AppGreenpictureBox.BringToFront();
                    this.AppRedPictureBox.Visible = false;
                }

                if (((!string.IsNullOrEmpty(this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAdminByColumn].ToString())) && (this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.LockAdminByColumn].ToString() != "0")))
                {
                    this.AdminRedpictureBox.Visible = true;
                    this.AdminRedpictureBox.BringToFront();
                    this.AdminGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AdminRedpictureBox.Visible = false;
                    this.AdminGreenpictureBox.Visible = true;
                    this.AdminGreenpictureBox.BringToFront();
                }
            }
        }

        /// <summary>
        /// ParcelHeaderPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelHeaderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D21000.F26000"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }

        /// <summary>
        /// SaveButtonClick
        /// </summary>
        private void SaveButtonClick()
        {
            //Commented by purushotham No Business Entity for 26000 do Direct Save operation

            F26000ParcelHeaderFormData updateparcelDetails = new F26000ParcelHeaderFormData();
            F26000ParcelHeaderFormData.updateParcelHeaderDetailsDataTableRow dr = updateparcelDetails.updateParcelHeaderDetailsDataTable.NewupdateParcelHeaderDetailsDataTableRow();
            dr.TaxDistrictID = this.districtId;

            //// Modified by purushotham to resolve TFS#19535
            //string DORLabel = this.ClassCodeTextBox.Text.Trim();
            //if (DORLabel.Contains("&&"))
            //{
            //    DORLabel = DORLabel.Replace("&&", "&");
            //    dr.StateCode = DORLabel;
            //}
            //else
            //{
            //    dr.StateCode = this.ClassCodeTextBox.Text.Trim();
            //}

            dr.NBHDID = this.nbhdID.ToString().Trim();
            //// Added Maskformat to save only the data in DB as per CO :4988
            this.ID1TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID2TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID3TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID4TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID5TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID6TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID7TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID8TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //// Added MID values to updateparcelDetails dataset for save operation as per CO :4988

            if (this.ID1TextBox.Text.Length > 50)
            {
                dr.MID1 = this.ID1TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID1 = this.ID1TextBox.Text.Trim();
            }

            if (this.ID2TextBox.Text.Length > 50)
            {
                dr.MID2 = this.ID2TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID2 = this.ID2TextBox.Text.Trim();
            }
            ////To check weather Masked value has been Configured in tts_cfg.based on that MID3 Column has validated on 12/7/2012 by purushotham
            this.ID3TextBox.Mask = this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.Mask3Column].ToString();
            if (!string.IsNullOrEmpty(this.ID3TextBox.Mask))
            {
                var MID3 = this.ID3TextBox.Lines[0].ToString();
                if (MID3.Contains("_"))
                {
                    //MID3=MID3.Replace("_", "");
                    //Convert.ToDecimal(MID3);
                    //dr.MID3 = MID3;
                    MessageBox.Show("Invalid Input Format", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    dr.MID3 = MID3;
                }
                // dr.MID3 = this.ID3TextBox.Lines[0].ToString();

            }
            else
            {
                if (this.ID3TextBox.Text.Length > 0)
                {

                    //if (this.ID3TextBox.Text.Length > 50)
                    //{
                    //    //var MID3= this.ID3TextBox.Text.Substring(0, 50).Trim();

                    ////Added for changed datatype varchar to decimal(12,5) by purushotham
                    var MID3 = this.ID3TextBox.Text.Trim();
                    dr.MID3 = MID3.Replace(",", "");
                    string[] strArray = dr.MID3.Split('.');
                    if ((strArray[0].Length > 0))
                    {

                        if (strArray.Length == 1)
                        {
                            string strNumb = (strArray[0]);

                            if (strNumb.Length > 7)
                            {
                                strNumb = strNumb.Substring(0, 7).Trim();

                            }
                            //double strDecimal = Convert.ToDouble(strNumb);
                            dr.MID3 = strNumb;
                        }
                        else
                        {
                            string strNumb = (strArray[0]);
                            string strDecimal = strArray[1];
                            if (strNumb.Length > 7)
                            {
                                strNumb = strNumb.Substring(0, 7).Trim();
                            }
                            if (strDecimal.Length > 5)
                            {
                                strDecimal = strDecimal.Substring(0, 5).Trim();
                            }
                            dr.MID3 = strNumb + "." + strDecimal;
                        }
                    }
                    //}
                    else
                    {
                        //dr.MID3 = this.ID3TextBox.Text.Trim();

                        ////Added for changed datatype varchar to decimal(12,5) by purushotham
                        //var MID3 = this.ID3TextBox.Text.Trim();
                        //MID3 = MID3.Replace(",", "");
                        //string[] strArray = MID3.Split('.');
                        if ((strArray[0].Length != null))
                        {
                            if (strArray.Length == 1)
                            {
                                string strNumb = (strArray[0]);

                                if (strNumb.Length > 7)
                                {
                                    strNumb = strNumb.Substring(0, 7).Trim();
                                }
                                dr.MID3 = strNumb;
                            }
                            else
                            {
                                string strNumb = (strArray[0]);
                                string strDecimal = strArray[1];
                                if (strNumb.Length.Equals(0))
                                {
                                    strNumb = "0";
                                }
                                if ((strNumb.Length > 0) && (strDecimal.Length > 0))
                                {
                                    if (strNumb.Length > 7)
                                    {
                                        strNumb = strNumb.Substring(0, 7).Trim();
                                    }
                                    if (strDecimal.Length > 5)
                                    {
                                        strDecimal = strDecimal.Substring(0, 5).Trim();
                                    }
                                    dr.MID3 = strNumb + "." + strDecimal;
                                }
                            }
                        }
                    }
                }
            }
            if (this.ID4TextBox.Text.Length > 50)
            {
                dr.MID4 = this.ID4TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID4 = this.ID4TextBox.Text.Trim();
            }

            if (this.ID5TextBox.Text.Length > 50)
            {
                dr.MID5 = this.ID5TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID5 = this.ID5TextBox.Text.Trim();
            }

            if (this.ID6TextBox.Text.Length > 50)
            {
                dr.MID6 = this.ID6TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID6 = this.ID6TextBox.Text.Trim();
            }

            if (this.ID7TextBox.Text.Length > 50)
            {
                dr.MID7 = this.ID7TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID7 = this.ID7TextBox.Text.Trim();
            }

            if (this.ID8TextBox.Text.Length > 50)
            {
                dr.MID8 = this.ID8TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID8 = this.ID8TextBox.Text.Trim();
            }

            dr.ImprovementID = Convert.ToInt32(this.PrimaryImprovementCombo.SelectedValue.ToString());
            dr.LandTypeID = Convert.ToInt32(this.PrimaryLandTypeComboBox.SelectedValue.ToString());
            dr.AppraisalTypeID = Convert.ToInt32(this.ApprisalTypeComboBox.SelectedValue.ToString());
            if (!string.IsNullOrEmpty(this.ClassCodeComboBox.Text.Trim()))
            {
                string tempVar = this.ClassCodeComboBox.Text.Trim();
                tempVar = tempVar.Replace(" ", "");
                dr.ClassCode = tempVar;
            }
            else
            {

            }
            dr.LocationID = this.locationId;
            dr.GroupingID = this.groupingId;
           // dr.TIFFundID = this.tifFundId;
            dr.ExemptionID = this.exemptionId;
            if (!string.IsNullOrEmpty(this.exemptionCode))
            {
                dr.ExemptionCode = this.exemptionCode;
            }
            else
            {
                string[] array = this.ExemptionCodeLinkLabel.Text.Split('-');
                if (array[0].Length > 0)
                {
                    dr.ExemptionCode = array[0].ToString();
                    //this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionIDColumn].ToString();
                    //dr.ExemptionID = 0;
                }
            }
            decimal tempExemptAmount;
            decimal.TryParse(this.ExemptFromAmountTextBox.Text.Trim(), out tempExemptAmount);
            dr.ExemptFromAmount = tempExemptAmount;
            //this.ParcelHeaderFormData.F26000ParcelHeader.Rows[0][this.ParcelHeaderFormData.F26000ParcelHeader.ExemptionAmountColumn].ToString();
            if (this.GreenbeltAreaComboBox.SelectedIndex > 0)
            {
                if (this.GreenbeltAreaComboBox.SelectedIndex.Equals(1))
                {
                    dr.IsGreenbelt = true;
                }
                else
                {
                    dr.IsGreenbelt = false;
                }
            }
            else
            {

            }
            if (!string.IsNullOrEmpty(this.GreenbeltFillingDateTextBox.Text))
            {
                var date = (this.GreenbeltFillingDateTextBox.Text);

                dr.GreenbeltFilingDate = date;
            }
            decimal marketvalue;
            decimal.TryParse(this.GreenbeltMarketValueTextBox.Text, out marketvalue);
            dr.GreenbeltMarketValue = marketvalue;
            decimal valueLoss;
            decimal.TryParse(this.GreenbeltValueLossTextBox.Text, out valueLoss);
            dr.GreenbeltValueLoss = valueLoss;
            decimal penalty;
            decimal.TryParse(this.PropertyPenalityTextBox.Text, out penalty);
            dr.PropertyPenalty = penalty;

            updateparcelDetails.updateParcelHeaderDetailsDataTable.Rows.Add(dr);
            updateparcelDetails.updateParcelHeaderDetailsDataTable.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(updateparcelDetails.updateParcelHeaderDetailsDataTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";

            int returnValue = this.form26000Control.WorkItem.UpdateParcelHeaderFormDetails(this.keyID, tempDataSet.GetXml(), TerraScanCommon.UserId, Convert.ToInt32(this.rollYear));
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = returnValue;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
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
        /// Handles the TextChanged event of the ID1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ID1TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Coding added for the CO 1517
        /// <summary>
        /// Handles the LinkClicked event of the ReviewLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ReviewLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form reviewForm = new Form();
                object[] optionalParameter = new object[] { this.keyID };
                reviewForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2409, optionalParameter, this.form26000Control.WorkItem);
                if (reviewForm != null)
                {
                    if (reviewForm.ShowDialog() == DialogResult.OK)
                    {
                        this.nbhdID = Convert.ToInt32(TerraScanCommon.GetValue(reviewForm, "NeighborId"));
                        this.NeighborhoodGrouplinkLabel.Text = TerraScanCommon.GetValue(reviewForm, "NeighborName");
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
        #endregion

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PrimaryImprovementCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PrimaryImprovementCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempprimaryowner = (int)this.PrimaryImprovementCombo.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the PrimaryImprovementCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void PrimaryImprovementCombo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.tempprimaryowner.Equals(this.PrimaryImprovementCombo.SelectedValue))
                {
                    this.SetEditRecord();
                    if (this.PrimaryImprovementCombo.SelectedValue == null)
                    {
                        this.PrimaryImprovementCombo.SelectedValue = 0;
                    }
                    this.tempprimaryowner = (int)this.PrimaryImprovementCombo.SelectedValue;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the PrimaryLandTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void PrimaryLandTypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.tempprimaylandtype.Equals(this.PrimaryLandTypeComboBox.SelectedValue))
                {
                    this.SetEditRecord();
                    if (this.PrimaryLandTypeComboBox.SelectedValue == null)
                    {
                        this.PrimaryLandTypeComboBox.SelectedValue = 0;
                    }
                    this.tempprimaryowner = (int)this.PrimaryImprovementCombo.SelectedValue;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PrimaryLandTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PrimaryLandTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempprimaylandtype = (int)this.PrimaryLandTypeComboBox.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ParcelNumberPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ID3TextBox_KeyUp(object sender, KeyEventArgs e)
        {

            ID3TextBox.Text.TrimEnd('0');

        }

        private void ID3TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {

                e.Handled = true;
            }
        }

        private void ID3TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode != Keys.Back)
                    {
                        if ((e.KeyCode != Keys.Decimal))
                        {
                            if ((e.KeyValue != 190))
                            {
                                nonNumberEntered = true;
                            }
                        }
                    }
                }
            }
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
            //if (Control.ModifierKeys == Keys.Decimal)
            //{
            //    nonNumberEntered = false;
            //}
            //if (e.KeyCode == Keys.Decimal)
            //{
            //    nonNumberEntered = true;
            //}
        }

        private void AssessorLocationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form LocationForm = new Form();
                object[] optionalParameter = new object[0];
                LocationForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2101, optionalParameter, this.form26000Control.WorkItem);
                if (LocationForm != null)
                {
                    if (LocationForm.ShowDialog() == DialogResult.OK)
                    {
                        var result = TerraScanCommon.GetValue(LocationForm, "LocationId");
                        this.locationId = Convert.ToInt32(result);
                        var resultVarible = TerraScanCommon.GetValue(LocationForm, "CommandValue");
                        this.AssessorLocationLinkLabel.Text = resultVarible;

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
        /// Handles the LinkClicked event of the ValutaionGroupingLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ValutaionGroupingLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form GroupingForm = new Form();
                object[] optionalParameter = new object[0];
                GroupingForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2102, optionalParameter, this.form26000Control.WorkItem);
                if (GroupingForm != null)
                {
                    if (GroupingForm.ShowDialog() == DialogResult.OK)
                    {
                        var result = TerraScanCommon.GetValue(GroupingForm, "GroupId");
                        this.groupingId = Convert.ToInt32(result);
                        var resultVarible = TerraScanCommon.GetValue(GroupingForm, "CommandValue");
                        this.ValutaionGroupingLinkLabel.Text = resultVarible;
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
        /// Handles the LinkClicked event of the TIFFundLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void TIFFundLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form fundSelectionForm = new Form();
                object[] optionalParameter = new object[0];
                fundSelectionForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1513, optionalParameter, this.form26000Control.WorkItem);
                if (fundSelectionForm != null)
                {
                    if (fundSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.tifFundId = Convert.ToInt32(TerraScanCommon.GetValue(fundSelectionForm, "FundId"));
                       // this.TIFFundLinkLabel.Text = TerraScanCommon.GetValue(fundSelectionForm, "FundItem").ToString();
                    }
                    ////Added by Biju to fix #5160
                    else
                    {
                        if (!this.ParentForm.MdiParent.ActiveMdiChild.Text.Equals(this.ParentForm.Text))
                        {
                            SendKeys.Send("^{TAB}");
                            SendKeys.Send("^+{TAB}");
                        }

                    }
                    ////till here
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ExemptionCodeLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ExemptionCodeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form ExemptionForm = new Form();
                object[] optionalParameter = new object[] { this.rollYear, this.ParentFormId, this.ExemptionCodeLinkLabel.Text };
                ExemptionForm = this.form26000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2103, optionalParameter, this.form26000Control.WorkItem);
                if (ExemptionForm != null)
                {
                    if (ExemptionForm.ShowDialog() == DialogResult.OK)
                    {
                        var result = TerraScanCommon.GetValue(ExemptionForm, "ExemptionId");
                        this.exemptionId = Convert.ToInt32(result);
                        var resultVarible = TerraScanCommon.GetValue(ExemptionForm, "CommandValue");
                        var array = resultVarible.Split('-');
                        if (!string.IsNullOrEmpty(array[0].ToString()))
                        {
                            this.exemptionCode = array[0].ToString();
                        }
                        else
                        {
                            this.exemptionCode = string.Empty;
                        }
                        this.ExemptionCodeLinkLabel.Text = resultVarible;

                        if (parcelId > 0 && exemptionId > 0)
                        {
                            this.ExemptFieldTable = this.form26000Control.WorkItem.F26000_ExemptFieldDetails(parcelId, exemptionId, exemptionCode).f26000_pcget_ExemptFieldDetails;

                            if (this.ExemptFieldTable.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(this.ExemptFieldTable.Rows[0][this.ExemptFieldTable.ExemptionFromAmountColumn].ToString()))
                                {
                                    var strTemp = this.ExemptFieldTable.Rows[0][this.ExemptFieldTable.ExemptionFromAmountColumn].ToString();
                                    double strNumb = Convert.ToDouble(strTemp);
                                    var strResult = strNumb.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                                    this.ExemptFromAmountTextBox.Text = strResult;
                                }
                                else
                                {
                                    this.ExemptFromAmountTextBox.Text = string.Empty;
                                }
                                if (!string.IsNullOrEmpty(this.ExemptFieldTable.Rows[0][this.ExemptFieldTable.ExemptionReductionColumn].ToString()))
                                {
                                    var strTemp = this.ExemptFieldTable.Rows[0][this.ExemptFieldTable.ExemptionReductionColumn].ToString();                                    
                                    double strNumb = Convert.ToDouble(strTemp);
                                    var strResult = strNumb.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                                    this.ExemptionReductionTextBox.Text = strResult;
                                }
                                else
                                {
                                    this.ExemptionReductionTextBox.Text = string.Empty;

                                }
                                if (!string.IsNullOrEmpty(this.ExemptFieldTable.Rows[0][this.ExemptFieldTable.ExemptionPercentColumn].ToString()))
                                {
                                    var strTemp = this.ExemptFieldTable.Rows[0][this.ExemptFieldTable.ExemptionPercentColumn].ToString();
                                    double strNumb = Convert.ToDouble(strTemp);
                                    strNumb = strNumb * 100;
                                    var strResult = strNumb.ToString("#,##0.##", CultureInfo.InvariantCulture);
                                    this.PercentageTextBox.Text = strResult + "%";
                                }
                                else
                                {
                                    this.PercentageTextBox.Text = string.Empty;
                                }
                            }
                        }
                    }
                    if (ExemptionForm.DialogResult.ToString().ToLower().Equals("no"))
                    {
                        if (!string.IsNullOrEmpty(this.ExemptionCodeLinkLabel.Text.ToString()))
                        {
                            this.ExemptionCodeLinkLabel.Text = "<<>>";
                            this.exemptionCode = null;
                            this.ExemptFromAmountTextBox.Text = "";
                            this.ExemptionReductionTextBox.Text = "";
                            this.PercentageTextBox.Text = "";
                            this.SetEditRecord();
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
        /// Handles the TextChanged event of the ClassCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ClassCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //dataSetCollection = new List<DataSetCollection>();
            //DataSet ds = new DataSet();
            int cursorPosition = ClassCodeTextBox.SelectionStart;
            this.tempClassCode = ClassCodeTextBox.Text;
            this.templength = tempClassCode.Length;
            this.tempClassCode = tempClassCode.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tempClassCode))
            {
                List<string> result = new List<string>(Regex.Split(tempClassCode, @"(?<=\G.{2})", RegexOptions.Singleline));
                var count = result.Count;
                for (int i = 0; i < count; i++)
                {
                    if (result[i].Length.Equals(2))
                    {
                        if (sb.ToString().Length < 17)
                        {
                            string temp = result[i].Insert(2, " ").ToString();

                            sb.Append(temp);
                        }

                    }
                    else
                    {
                        if (sb.ToString().Length < 17)
                        {
                            sb.Append(result[i].ToString());
                        }
                    }
                }
                //previousCursorindex = cursorPosition;
                var length = sb.ToString().Length;
                ClassCodeTextBox.TextChanged -= new EventHandler(this.ClassCodeTextBox_TextChanged);
                if (length > 17)
                {
                    ClassCodeComboBox.Text = sb.ToString().Remove(17);
                    length = length - 1;
                }
                else
                {
                    ClassCodeComboBox.Text = sb.ToString();
                }
                if (length > templength)
                {
                    ClassCodeTextBox.SelectionStart = cursorPosition + 1;
                }
                else
                {
                    if (cursorPosition != 0)
                    {
                        ClassCodeTextBox.SelectionStart = cursorPosition;
                    }
                }
                if (length.Equals(templength))
                {
                    ClassCodeTextBox.SelectionStart = cursorPosition;
                }
                //ClassCodeRGB
                //AddDataSetValues("DSName", "", "f26000_udf_GetParcelClassCodeRGB");
                //ds = this.form26000Control.WorkItem.ClassCode_RGB(this.dataSetCollection[0].commandText);
                ClassCodeTextBox.TextChanged += new EventHandler(this.ClassCodeTextBox_TextChanged);
                this.SetEditRecord();
            }

        }

        #region TimePicker_KeyPress

        /// <summary>
        /// Handles the KeyPress event of the TimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void TimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        #endregion TimePicker_KeyPress

        #region TimerImage_Click

        /// <summary>
        /// Timers the image_ click.
        /// </summary>
        /// <param name="textControl">The text control.</param>
        /// <param name="timePickerControl">The time picker control.</param>
        private void TimerImage_Click(TextBox textControl, DateTimePicker timePickerControl)
        {
            try
            {
                timePickerControl.BringToFront();
                ////to set Datetimepicker control 
                if (!string.IsNullOrEmpty(textControl.Text.Trim()))
                {
                    timePickerControl.Value = Convert.ToDateTime(textControl.Text);
                }
                else
                {
                    timePickerControl.Value = DateTime.Today;
                }
                timePickerControl.Visible = true;
                ////  this.SaveButton.Enabled = true;
                SendKeys.Send(SharedFunctions.GetResourceString("F4"));
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion TimerImage_Click
        /// <summary>
        /// Handles the CloseUp event of the EnteredBydateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GreenbeltFillingDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.GreenbeltFillingDateTextBox.Text = this.GreenbeltFillingDateTimePicker.Text;
                this.GreenbeltFillingDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void GreenbeltFillingDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                ////Calling Timer Image Method
                this.TimerImage_Click(this.GreenbeltFillingDateTextBox, this.GreenbeltFillingDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void GreenbeltValueLossTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SetEditRecord();

        }

        private void GreenbeltMarketValueTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SetEditRecord();

        }

        //private void AddDataSetValues(string dsName, string commandType, string commandText)
        //{
        //    DataSetCollection tempDataSetCollection;
        //    tempDataSetCollection.dataSetName = dsName;
        //    tempDataSetCollection.commandType = commandType;
        //    tempDataSetCollection.commandText = commandText;
        //    this.dataSetCollection.Add(tempDataSetCollection);
        //}

        private void ClassCodeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == ' ')
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }

        }

        private void ClassCodeComboBox_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = ClassCodeComboBox.SelectionStart;
            this.tempClassCode = ClassCodeComboBox.Text;
            this.templength = tempClassCode.Length;
            this.tempClassCode = tempClassCode.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tempClassCode))
            {
                List<string> result = new List<string>(Regex.Split(tempClassCode, @"(?<=\G.{2})", RegexOptions.Singleline));
                var count = result.Count;
                for (int i = 0; i < count; i++)
                {
                    if (result[i].Length.Equals(2))
                    {
                        if (sb.ToString().Length < 17)
                        {
                            string temp = result[i].Insert(2, " ").ToString();

                            sb.Append(temp);
                        }

                    }
                    else
                    {
                        if (sb.ToString().Length < 17)
                        {
                            sb.Append(result[i].ToString());
                        }
                    }
                }
                //previousCursorindex = cursorPosition;
                var length = sb.ToString().Length;
                ClassCodeComboBox.TextChanged -= new EventHandler(this.ClassCodeComboBox_TextChanged);
                if (length > 17)
                {
                    ClassCodeComboBox.Text = sb.ToString().Remove(17);
                    length = length - 1;
                }
                else
                {
                    ClassCodeComboBox.Text = sb.ToString();
                }
                if (length > templength)
                {
                    ClassCodeComboBox.SelectionStart = cursorPosition + 1;
                }
                else
                {
                    if (cursorPosition != 0)
                    {
                        ClassCodeComboBox.SelectionStart = cursorPosition;
                    }
                }
                if (length.Equals(templength))
                {
                    ClassCodeComboBox.SelectionStart = cursorPosition;
                }
                ClassCodeComboBox.TextChanged += new EventHandler(this.ClassCodeComboBox_TextChanged);
                this.SetEditRecord();
            }
        }
        private void ClassCodeComboBox_TextUpdate(object sender, EventArgs e)
        {
            this.classCode = this.ClassCodeComboBox.Text;
            if (parcelId > 0 && (!string.IsNullOrEmpty(this.ClassCodeComboBox.Text)) && (classCodeConfigValue > 0) && (classCode.ToString().Replace(" ", "").Length == classCodeConfigValue))
            {
                this.classCodeDataTable = this.form26000Control.WorkItem.F26000_ClassCodeDetails(classCode).f26000ClassCode;
                this.ClassCodeComboBox.DisplayMember = classCodeDataTable.ClassCodeColumn.ColumnName;
                // var imagePaths = classCodeDataTable.AsEnumerable().Select(r => r.Field<string>("ClassCode"));
                if (classCodeDataTable.Rows.Count > 0)
                {
                    this.ClassCodeComboBox.DataSource = classCodeDataTable.DefaultView;
                    this.classCode = this.ClassCodeComboBox.Text;
                    this.ClassCodeComboBox.Text = this.classCode;
                    this.ClassCodeComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    this.ClassCodeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                    this.ClassCodeComboBox.Text = this.classCode;
                    this.ClassCodeComboBox.Select(this.ClassCodeComboBox.Text.Length, 0);
                   // this.ClassCodeComboBox.DataSource = classCodeDataTable;
                  // this.ClassCodeComboBox.TextChanged += new EventHandler(this.ClassCodeComboBox_TextChanged);
                    //this.ClassCodeComboBox.Text = this.classCode + " ";
                }
                //if (ClassCodeComboBox.Text.Length == 6)
                //{
                //    this.ClassCodeComboBox.Text ="1";
                //    this.ClassCodeComboBox.Text = this.classCode + " ";
                //}
                //if (Convert.ToInt32(this.ClassCodeComboBox.Text.Length) == 6)
                //{ 
                //    this.ClassCodeComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                //    this.ClassCodeComboBox.Text = this.classCode;
                //    //this.ClassCodeComboBox.Text=
                //    //this.ClassCodeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                //}
            }

        }

    }
}