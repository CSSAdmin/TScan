//----------------------------------------------------------------------------------
// <copyright file="F25000.cs" company="Congruent">
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
// o4 April 07		Ramya D                 Created
// 04 March 09      ShanmugaSundaram A      Modified for Implementing CO:4988
// 22 JUNE 11       Manoj Kumar P            #Bug TSBG:12124  
//*********************************************************************************/

namespace D20000
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
    using System.Globalization;
    using D24530;


    /// <summary>
    /// f25000
    /// </summary>
    public partial class F25000 : BaseSmartPart
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

        /// <summary>
        /// DataSet which is used to get the details of Stoppage event
        /// </summary>
        private F25000ParcelHeaderData parcelHeaderData = new F25000ParcelHeaderData();

        /// <summary>
        /// getPrimaryImprovementData
        /// </summary>
        private F25000ParcelHeaderData getPrimaryImprovementData = new F25000ParcelHeaderData();

        /// <summary>
        /// getPrimaryLandTypeData
        /// </summary>
        private F25000ParcelHeaderData getPrimaryLandTypeData = new F25000ParcelHeaderData();

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
        /// Instance of 8106 Controller to call the WorkItem
        /// </summary>
        private F25000Controller form25000Control;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Unique eventID 
        /// </summary>
        private int eventID;

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

        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        /// <summary>
        ///// Instance for 29531.
        ///// </summary>
        F29531 f29531;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25000"/> class.
        /// </summary>
        public F25000()
        {
            this.InitializeComponent();
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
        public F25000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
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

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

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
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

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
        public F25000Controller Form25000Control
        {
            get { return this.form25000Control as F25000Controller; }
            set { this.form25000Control = value; }
        }
        #endregion Property

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
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

                    if (this.parcelHeaderData.f25000ParcelHeader.Rows.Count > 0)
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

        #region Event Subscription
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

        #region User Defined Methods

        /// <summary>
        /// Loads the default view of the page with controls enabled/Disabled accordingly
        /// </summary>
        private void LoadDefaultView()
        {
            this.ShowPanel(true);

            this.ShowControls(true);

            this.F25000GetParcelDetails();
        }

        /// <summary>
        /// Loads the ParcelHeaderDetails
        /// </summary>
        private void F25000GetParcelDetails()
        {
            this.parcelHeaderData.EnforceConstraints = false;
            this.parcelHeaderData = this.form25000Control.WorkItem.F25000_GetParcelDetails(this.keyID);

            if (this.parcelHeaderData.f25000ParcelHeader.Rows.Count > 0)
            {
                int.TryParse(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.ParcelIDColumn].ToString(), out this.parcelId);
                string parcelNofield = string.Empty;
                parcelNofield = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.ParcelNumberColumn].ToString();
                if (parcelNofield.Contains("&"))
                {
                    parcelNofield = parcelNofield.Replace("&", "&&");
                }
                this.ParcelNolinkLabel.Text = parcelNofield;

                //// Added local variable instead of the link label as per CO :4988
                this.rollYear = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.RollYearColumn].ToString();
                F29531.rollYearval = rollYear;
                F29531.RollYear = Convert.ToInt32(rollYear);
                this.TypeTextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.TypeColumn].ToString();
                this.RetiredTextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.RetiredColumn].ToString();
                this.ExemptTextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.ExemptColumn].ToString();
                this.DescriptionTextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DescriptionColumn].ToString();
                string primaryowner = string.Empty;
                primaryowner = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.PrimaryOwnerColumn].ToString();
                if (primaryowner.Contains("&"))
                {
                    primaryowner = primaryowner.Replace("&", "&&");
                }
                ////this.PrimaryOwnerlinkLabel.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.PrimaryOwnerColumn].ToString();
                this.PrimaryOwnerlinkLabel.Text = primaryowner;

                string primarysitus = string.Empty;
                primarysitus = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.SitusColumn].ToString();
                if (primarysitus.Contains("&"))
                {
                    primarysitus = primarysitus.Replace("&", "&&");
                }
                this.PrimarySituslinkLabel.Text = primarysitus;
                /// #Bug TSBG:12124  issue fixed in Legal Field Description.
                string legalField = string.Empty;
                legalField = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LegalColumn].ToString();
                if (legalField.Contains("&"))
                {
                    legalField = legalField.Replace("&", "&&");
                }
                this.LegallinkLabel.Text = legalField;
                ////#TSBG:12124.

                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DORColumn].ToString()))
                {
                    string DORField = string.Empty;
                    DORField = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DORColumn].ToString();
                    if (DORField.Contains("&"))
                    {
                        DORField = DORField.Replace("&", "&&");
                    }
                    this.DORlinkLabel.Text = DORField;

                }
                else
                {
                    this.DORlinkLabel.Text = "N/A";
                }

                int.TryParse(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.NBHDIDColumn].ToString(), out this.nbhdID);

                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb27,2009 as per the client to show the MIDs value in Mask Value Format

                this.ID1TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask1Column].ToString();
                this.ID2TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask2Column].ToString();
                this.ID3TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask3Column].ToString();
                this.ID4TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask4Column].ToString();
                this.ID5TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask5Column].ToString();
                this.ID6TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask6Column].ToString();
                this.ID7TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask7Column].ToString();
                this.ID8TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask8Column].ToString();

                //// Coding modified for the issue 1517
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID1Column].ToString()))
                {
                    this.ID1TextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID1Column].ToString();
                }
                else
                {
                    this.ID1TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID2Column].ToString()))
                {
                    this.ID2TextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID2Column].ToString();
                }
                else
                {
                    this.ID2TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID3Column].ToString()))
                {
                    string strTemp = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID3Column].ToString();
                    ////Added for 25000 CO on 12/7/2012 by purushotham
                    double strNumb = Convert.ToDouble(strTemp);
                    var strResult = strNumb.ToString("#,##0.0####", CultureInfo.InvariantCulture);
                    this.ID3TextBox.Text = strResult;
                }
                else
                {
                    this.ID3TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID4Column].ToString()))
                {
                    this.ID4TextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID4Column].ToString();
                }
                else
                {
                    this.ID4TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID5Column].ToString()))
                {
                    this.ID5TextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID5Column].ToString();
                }
                else
                {
                    this.ID5TextBox.Text = string.Empty;
                }

                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID6Column].ToString()))
                {
                    this.ID6TextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID6Column].ToString();
                }
                else
                {
                    this.ID6TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID7Column].ToString()))
                {
                    this.ID7TextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID7Column].ToString();
                }
                else
                {
                    this.ID7TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID8Column].ToString()))
                {
                    this.ID8TextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID8Column].ToString();
                }
                else
                {
                    this.ID8TextBox.Text = string.Empty;
                }
                ////1517 ends here
                string districtfield = string.Empty;
                districtfield = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DistrictColumn].ToString();
                if (districtfield.Contains("&"))
                {
                    districtfield = districtfield.Replace("&", "&&");
                }
                this.DistrictlinkLabel.Text = districtfield;

                //this.DistrictlinkLabel.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DistrictColumn].ToString();
                this.districtId = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DistrictIDColumn].ToString();
                string Neighborhoodfield = string.Empty;
                Neighborhoodfield = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.NeighborhoodGroupColumn].ToString();
                if (Neighborhoodfield.Contains("&"))
                {
                    Neighborhoodfield = Neighborhoodfield.Replace("&", "&&");
                }
                this.NeighborhoodGrouplinkLabel.Text = Neighborhoodfield;
                // this.NeighborhoodGrouplinkLabel.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.NeighborhoodGroupColumn].ToString();
                this.EventslinkLabel.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.EventsColumn].ToString();
                int.TryParse(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.EventIDColumn].ToString(), out this.eventID);
                ////Coding modified for the issue 1517
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LabelColumn].ToString()))
                {
                    this.ID1label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LabelColumn].ToString() + ":";
                }
                else
                {
                    this.ID1label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe2Column].ToString()))
                {
                    this.ID2label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe2Column].ToString() + ":";
                }
                else
                {
                    this.ID2label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe3Column].ToString()))
                {
                    this.ID3label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe3Column].ToString() + ":";
                }
                else
                {
                    this.ID3label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe4Column].ToString()))
                {
                    this.ID4label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe4Column].ToString() + ":";
                }
                else
                {
                    this.ID4label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe5Column].ToString()))
                {
                    this.ID5label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe5Column].ToString() + ":";
                }
                else
                {
                    this.ID5label.Text = string.Empty;
                }

                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe6Column].ToString()))
                {
                    this.ID6label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe6Column].ToString() + ":";
                }
                else
                {
                    this.ID6label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe7Column].ToString()))
                {
                    this.ID7label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe7Column].ToString() + ":";
                }
                else
                {
                    this.ID7label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe8Column].ToString()))
                {
                    this.ID8label.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Labe8Column].ToString() + ":";
                }
                else
                {
                    this.ID8label.Text = string.Empty;
                }
                ////1517 ends here
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.ImprovementColumn].ToString()))
                {
                    this.PrimaryImprovementCombo.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.ImprovementColumn].ToString();
                    this.tempprimaryowner = (int)this.PrimaryImprovementCombo.SelectedValue;
                }
                else
                {
                    this.PrimaryImprovementCombo.SelectedValue = 0;
                }

                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LandTypeColumn].ToString()))
                {
                    this.PrimaryLandTypeComboBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LandTypeColumn].ToString();
                    this.tempprimaylandtype = (int)this.PrimaryLandTypeComboBox.SelectedValue;
                }
                else
                {
                    this.PrimaryLandTypeComboBox.SelectedValue = 0;
                }

                this.MasktextboxAlignment();
                ////Added By Malliga for C0(April 2008)
                this.AssessmentTypeTextBox.Text = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.AssessmentTypeColumn].ToString();

                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAppraisalByColumn].ToString())) && (this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAppraisalByColumn].ToString() != "0")))
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

                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAdminByColumn].ToString())) && (this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAdminByColumn].ToString() != "0")))
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
            this.DORlinkLabel.Text = string.Empty;
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
            this.DORlinkLabel.Enabled = show;
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
            this.DORpanel10.Enabled = show;
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

        //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009

        /// <summary>
        /// Primaries the implementation.
        /// </summary>
        private void PrimaryImplementation()
        {
            this.getPrimaryImprovementData = this.form25000Control.WorkItem.ListPrimaryImprovement();
            if (this.getPrimaryImprovementData.f25000ListParcelImprovement.Rows.Count > 0)
            {
                this.PrimaryImprovementCombo.DataSource = this.getPrimaryImprovementData.f25000ListParcelImprovement;
                this.PrimaryImprovementCombo.DisplayMember = this.getPrimaryImprovementData.f25000ListParcelImprovement.ImprovementColumn.ColumnName;
                this.PrimaryImprovementCombo.ValueMember = this.getPrimaryImprovementData.f25000ListParcelImprovement.ImprovementIDColumn.ColumnName;
            }
        }

        //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009

        /// <summary>
        /// Primaries the type of the land.
        /// </summary>
        private void PrimaryLandType()
        {
            this.getPrimaryLandTypeData = this.form25000Control.WorkItem.ListPrimaryLandType();
            if (this.getPrimaryLandTypeData.f25000ListParcelLandTypes.Rows.Count > 0)
            {
                this.PrimaryLandTypeComboBox.DataSource = this.getPrimaryLandTypeData.f25000ListParcelLandTypes;
                this.PrimaryLandTypeComboBox.DisplayMember = this.getPrimaryLandTypeData.f25000ListParcelLandTypes.LandTypeColumn.ColumnName;
                this.PrimaryLandTypeComboBox.ValueMember = this.getPrimaryLandTypeData.f25000ListParcelLandTypes.LandTypeIDColumn.ColumnName;
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
        #endregion User Defined Methods

        #region Events

        /// <summary>
        /// Loaads the data
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void F25000_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009
                this.PrimaryImplementation();
                this.PrimaryLandType();

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
                this.getForm2000DetailsData = this.form25000Control.WorkItem.GetFormDetails(Convert.ToInt32(2000), TerraScanCommon.UserId);
                if (this.getForm2000DetailsData.Rows.Count > 0)
                {
                    Boolean.TryParse(this.getForm2000DetailsData.Rows[0][getForm2000DetailsData.IsPermissionOpenColumn.ColumnName].ToString(), out this.form2000OpenPermission);
                }

                Form parcelStatusForm = new Form();
                object[] optionalParameter = new object[] { this.keyID };
                parcelStatusForm = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2000, optionalParameter, this.form25000Control.WorkItem);
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
            try
            {
                Form statecode = new Form();
                object[] optionalParameters = new object[] { this.DORlinkLabel.Text };
                statecode = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2010, optionalParameters, this.form25000Control.WorkItem);
                if (statecode != null)
                {
                    if (statecode.ShowDialog() == DialogResult.OK)
                    {
                        string DORLabel = string.Empty;
                        DORLabel = TerraScanCommon.GetValue(statecode, "StateCodeNameReturnValue");
                        if (DORLabel.Contains("&"))
                        {
                            ////Comment to resolve TFS#19535 bug by Purushotham on 12/April/2013
                            DORLabel = DORLabel.Replace("&", "&&");
                        }
                        this.DORlinkLabel.Text = DORLabel;
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
                districtF15122 = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1512, optionalParameter, this.form25000Control.WorkItem);

                DialogResult districtDialog;
                if (districtF15122 != null)
                {
                    districtDialog = districtF15122.ShowDialog();

                    if (districtDialog == DialogResult.OK)
                    {
                        try
                        {
                            this.districtId = TerraScanCommon.GetValue(districtF15122, "DistrictId");
                            this.districtSlectionDataset = this.form25000Control.WorkItem.F1512_GetDistrictSelectionData(this.ConvertStringToInteger(this.districtId), "", "", -999);

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
                subfundForm = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3510, optionalParameter, this.form25000Control.WorkItem);
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

                        string strMessage = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.UserWarningColumn.ColumnName].ToString();
                        if (string.IsNullOrEmpty(strMessage))
                        {
                            this.NeighborhoodGrouplinkLabel.Text = neighborhoodLabel;


                        }
                        else
                        {
                            DialogResult ds = TerraScanMessageBox.Show(strMessage, "Terrascan T2 - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (ds.Equals(DialogResult.OK))
                            {
                                // this.DialogResult = DialogResult.No;

                            }
                        }
                        // this.NeighborhoodGrouplinkLabel.Text = neighborhoodLabel;
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
                subfundForm = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25000Control.WorkItem);
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
                subfundForm = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25000Control.WorkItem);
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
                subfundForm = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25000Control.WorkItem);
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
                subfundForm = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25000Control.WorkItem);
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
            this.parcelHeaderData = this.form25000Control.WorkItem.F25000_GetParcelDetails(this.keyID);

            if (this.parcelHeaderData.f25000ParcelHeader.Rows.Count > 0)
            {
                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAppraisalByColumn].ToString())) && (this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAppraisalByColumn].ToString() != "0")))
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

                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAdminByColumn].ToString())) && (this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.LockAdminByColumn].ToString() != "0")))
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
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D20000.F25000"));
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
            bool isCopyHeader = false;
            var DORField = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DORColumn].ToString();
            if (DORField.Contains("&"))
            {
                DORField = DORField.Replace("&", "&&");
            }
            var districtfield = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.DistrictColumn].ToString();
            if (districtfield.Contains("&"))
            {
                districtfield = districtfield.Replace("&", "&&");
            }
            var MID2 = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.MID2Column].ToString();
            if (!DORField.Equals(this.DORlinkLabel.Text) || (!districtfield.Equals(this.DistrictlinkLabel.Text)) || (!MID2.Equals(this.ID2TextBox.Text)))
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Do you want to push changes to the DOR, Map Number, and District fields to future roll years?", "TerraScan T2 – Push changes to Future Years", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    isCopyHeader = true;
                }
                else
                {
                    isCopyHeader = false;
                }
            }
            F25000ParcelHeaderData updateparcelDetails = new F25000ParcelHeaderData();
            F25000ParcelHeaderData.updateParcelDetailsDataTableRow dr = updateparcelDetails.updateParcelDetailsDataTable.NewupdateParcelDetailsDataTableRow();
            dr.TaxDistrictID = this.districtId;
            //// Modified by purushotham to resolve TFS#19535
            string DORLabel = this.DORlinkLabel.Text.Trim();
            if (DORLabel.Contains("&&"))
            {
                DORLabel = DORLabel.Replace("&&", "&");
                dr.StateCode = DORLabel;
            }
            else
            {
                dr.StateCode = this.DORlinkLabel.Text.Trim();
            }

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
            this.ID3TextBox.Mask = this.parcelHeaderData.f25000ParcelHeader.Rows[0][this.parcelHeaderData.f25000ParcelHeader.Mask3Column].ToString();
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
                    //var MID3= this.ID3TextBox.Text.Substring(0, 50).Trim();

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
                        ////  var MID3 = this.ID3TextBox.Text.Trim();
                        //  MID3 = this.ID3TextBox.Text.Trim();
                        //  MID3 = MID3.Replace(",", "");
                        //  string[] strArray = MID3.Split('.');
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
                                //dr.MID3="";

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

            updateparcelDetails.updateParcelDetailsDataTable.Rows.Add(dr);
            updateparcelDetails.updateParcelDetailsDataTable.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(updateparcelDetails.updateParcelDetailsDataTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";

            int returnValue = this.Form25000Control.WorkItem.UpdateParcelHeaderDetails(this.keyID, tempDataSet.GetXml(), isCopyHeader, TerraScanCommon.UserId);
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
                reviewForm = this.form25000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2409, optionalParameter, this.form25000Control.WorkItem);
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
    }
}
