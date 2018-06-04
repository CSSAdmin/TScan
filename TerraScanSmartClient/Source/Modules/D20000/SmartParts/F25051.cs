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
// 01 Nov 2011		Manoj P                 Created
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

    public partial class F25051 : BaseSmartPart
    {
    
        #region Member Variables

        /// <summary>
        /// outValue
        /// </summary>
        private int outValue;

        /// <summary>
        /// Unique nbhdID 
        /// </summary>
        private int nbhdID;

        /// <summary>
        /// Roll Year
        /// </summary>
        private string rollYear;

        /// <summary>
        /// districtId
        /// </summary>
        private int districtId;

        /// <summary>
        /// Unique eventID 
        /// </summary>
        private int eventID;

        /// <summary>
        /// Used to store the parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// DataSet which is used to get the details of Stoppage event
        /// </summary>
        private F25051ParcelHeaderData parcelHeaderData = new F25051ParcelHeaderData();

        /// <summary>
        /// getParcelClassData
        /// </summary>
        private F25051ParcelHeaderData getParcelClassData = new F25051ParcelHeaderData();

        /// <summary>
        /// getOwnerOccupiedData
        /// </summary>
        private F25051ParcelHeaderData getOwnerOccupiedData = new F25051ParcelHeaderData();

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
      //  private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// To get the Form 2000 Permission
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getForm2000Details = new SupportFormData.GetFormDetailsDataTable();


        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance of 8106 Controller to call the WorkItem
        /// </summary>
        private F25051Controller form25051Control;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Set Default navigationFlag 
        /// </summary>
        private bool navigationFlag;

        /// <summary>
        /// To get the Form 2000 Permission
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getForm2000DetailsData = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Set Default OpenPermission for Form 2000
        /// </summary>
        private bool form2000OpenPermission;

        /// <summary>
        /// tempprimaryowner
        /// </summary>
        private int tempprimaryowner;

        /// <summary>
        /// tempprimaylandtype
        /// </summary>
        private int tempprimaylandtype;

        /// <summary>
        /// tempAgland
        /// </summary>
        private int tempAgland;

        /// <summary>
        /// tempNonAgland
        /// </summary>
        private int tempNonAgland;

        /// <summary>
        /// tempNonAgImprovement
        /// </summary>
        private int tempNonAgImprovement;

        // <summary>
        /// tempAgImprovement
        /// </summary>
        private int tempAgImprovement;


        /// <summary>
        /// tempNonAgResidential
        /// </summary>
        private int tempNonAgResidential;


        /// <summary>
        /// tempOwnerOccupied
        /// </summary>
        private int tempOwnerOccupied;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25000"/> class.
        /// </summary>
        public F25051()
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
        public F25051(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
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
        /// For F25051Control
        /// </summary>
        [CreateNew]
        public F25051Controller Form25051Control
        {
            get { return this.form25051Control as F25051Controller; }
            set { this.form25051Control = value; }
        }
        #endregion Property


        #region Event Subscription

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

                    if (this.parcelHeaderData.f25051ParcelHeader.Rows.Count > 0)
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

        #region User Deined Methods

        /// <summary>
        /// Loads the default view of the page with controls enabled/Disabled accordingly
        /// </summary>
        private void LoadDefaultView()
        {
            this.ShowPanel(true);

            this.ShowControls(true);

            this.F25051GetParcelDetails();
        }
            
        /// <summary>
        /// SaveButtonClick
        /// </summary>
        private void SaveButtonClick()
        {
            F25051ParcelHeaderData updateparcelDetails = new F25051ParcelHeaderData();
            F25051ParcelHeaderData.f25051ParcelDetailsDataTableRow dr = updateparcelDetails.f25051ParcelDetailsDataTable.Newf25051ParcelDetailsDataTableRow();
            dr.TaxDistrictID = this.districtId;
            dr.NBHDID = this.nbhdID;
            this.ID1TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID2TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID3TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID4TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID5TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID6TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            this.ID7TextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
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

            if (this.ID3TextBox.Text.Length > 50)
            {
                dr.MID3 = this.ID3TextBox.Text.Substring(0, 50).Trim();
            }
            else
            {
                dr.MID3 = this.ID3TextBox.Text.Trim();
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
            int improvementID;
            int.TryParse(this.PrimaryImprovementCombo.SelectedValue.ToString(), out improvementID);
            this.PrimaryImprovementCombo.SelectionLength = 0;
            dr.ImprovementID = improvementID;
            int landTypeID;
             int.TryParse(this.PrimaryLandTypeComboBox.SelectedValue.ToString(), out landTypeID);
             dr.LandTypeID = landTypeID;
             this.PrimaryLandTypeComboBox.SelectionLength = 0;
             if (this.AglandComboBox.SelectedValue != null)
             {
                 int AglandID;
                 int.TryParse(this.AglandComboBox.SelectedValue.ToString(), out AglandID);
                 dr.ClassAglandID = AglandID;
                 this.AglandComboBox.SelectionLength=0;   
             }
             if (this.NonAgLandComboBox.SelectedValue != null)
             {
                 int NonAglandID;
                 int.TryParse(this.NonAgLandComboBox.SelectedValue.ToString(), out NonAglandID);
                 dr.ClassNonAglandID = NonAglandID;
                 this.NonAgLandComboBox.SelectionLength = 0; 

             }
             if (this.AgImprovementComboBox.SelectedValue != null)
             {
                 int AgImprovementID;
                 int.TryParse(this.AgImprovementComboBox.SelectedValue.ToString(), out AgImprovementID);
                 dr.ClassAgImprovementID = AgImprovementID;
                 this.AgImprovementComboBox.SelectionLength = 0; 

             }
             if (this.NonAgImprovementComboBox.SelectedValue != null)
             {
                 int NonAgImprovementID;
                 int.TryParse(this.NonAgImprovementComboBox.SelectedValue.ToString(), out NonAgImprovementID);
                 dr.ClassNonAgImprovementID = NonAgImprovementID;
                 this.NonAgImprovementComboBox.SelectionLength = 0; 
             }
             if (this.NonResidentialsComboBox.SelectedValue != null)
             {
                 int nonResidentialID;
                 int.TryParse(this.NonResidentialsComboBox.SelectedValue.ToString(), out nonResidentialID);
                 dr.ClassNonResidentialID = nonResidentialID;
                 this.NonResidentialsComboBox.SelectionLength = 0;
             }
             if (this.OwnerOccupiedComboBox.SelectedValue != null)
             {
                 int ownerOccupied;
                 int.TryParse(this.OwnerOccupiedComboBox.SelectedValue.ToString(), out ownerOccupied);
                 dr.OwnerOccupiedID = ownerOccupied;
                 this.OwnerOccupiedComboBox.SelectionLength = 0;
                
             }
            updateparcelDetails.f25051ParcelDetailsDataTable.Rows.Add(dr);
            updateparcelDetails.f25051ParcelDetailsDataTable.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(updateparcelDetails.f25051ParcelDetailsDataTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            int returnValue = this.Form25051Control.WorkItem.F25051ParcelHeaderDetails(this.keyID, tempDataSet.GetXml(), TerraScanCommon.UserId);
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
        /// Clears all the controls
        /// </summary>
        private void ClearControls()
        {
            this.ParcelNolinkLabel.Text = string.Empty;
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
            this.DistrictlinkLabel.Text = string.Empty;
            this.NeighborhoodGrouplinkLabel.Text = string.Empty;
            this.EventslinkLabel.Text = string.Empty;
            this.PrimaryImprovementCombo.SelectedIndex = -1;
            this.PrimaryLandTypeComboBox.SelectedIndex = -1;
            this.AglandComboBox.SelectedIndex = -1;
            this.NonAgLandComboBox.SelectedIndex = -1;
            this.AgImprovementComboBox.SelectedIndex = -1;
            this.NonAgImprovementComboBox.SelectedIndex = -1;
            this.NonResidentialsComboBox.SelectedIndex = -1;
            this.OwnerOccupiedComboBox.SelectedIndex = -1; 
            this.ReviewLinkLabel.Text = string.Empty;
        }

        /// <summary>
        /// Reload the Lock while clicking the Lock Buttons
        /// </summary>
        private void LoadLock()
        {
            this.parcelHeaderData = this.form25051Control.WorkItem.F25051_GetParcelDetails(this.keyID);

            if (this.parcelHeaderData.f25051ParcelHeader.Rows.Count > 0)
            {
                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAppraisalByColumn].ToString())) && (this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAppraisalByColumn].ToString() != "0")))
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

                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAdminByColumn].ToString())) && (this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAdminByColumn].ToString() != "0")))
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
            this.OwnerOccupiedPanel.Enabled = show;
            this.ID1panel1.Enabled = show;
            this.ID2panel1.Enabled = show;
            this.ID3panel.Enabled = show;
            this.ID4panel.Enabled = show;
            this.ID5panel.Enabled = show;
            this.ID6panel.Enabled = show;
            this.ID7panel.Enabled = show;
            this.AgLandpanel10.Enabled = show;
            this.NonAglandPanel.Enabled = show;
            this.AgImprovementsPanel.Enabled = show;
            this.NonAgImprovementsPanel.Enabled = show;
            this.NonAgResidentialsPanel.Enabled = show; 
            this.Districtpanel16.Enabled = show;
            this.Neighborhoodpanel17.Enabled = show;
            this.Eventpanel18.Enabled = show;
            this.AessmentTypePanel.Enabled = show;
            this.ReviewPanel.Enabled = show;
        }

        // <summary>
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
            this.DistrictlinkLabel.Enabled = show;
            this.NeighborhoodGrouplinkLabel.Enabled = show;
            this.EventslinkLabel.Enabled = show;
            this.ReviewLinkLabel.Enabled = show;
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
            this.getPrimaryImprovementData = this.form25051Control.WorkItem.ListPrimaryImprovement();
            if (this.getPrimaryImprovementData.f25000ListParcelImprovement.Rows.Count > 0)
            {
                this.PrimaryImprovementCombo.DataSource = this.getPrimaryImprovementData.f25000ListParcelImprovement;
                this.PrimaryImprovementCombo.DisplayMember = this.getPrimaryImprovementData.f25000ListParcelImprovement.ImprovementColumn.ColumnName;
                this.PrimaryImprovementCombo.ValueMember = this.getPrimaryImprovementData.f25000ListParcelImprovement.ImprovementIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Owner Occupied.
        /// </summary>
        private void ListOwnerOccupied()
        {
            this.getOwnerOccupiedData = this.form25051Control.WorkItem.F25051OwnerOccupied();
            if (this.getOwnerOccupiedData.F25051OwnerOccupied.Rows.Count > 0)
            {
                this.OwnerOccupiedComboBox.DataSource = this.getOwnerOccupiedData.F25051OwnerOccupied;
                this.OwnerOccupiedComboBox.DisplayMember = this.getOwnerOccupiedData.F25051OwnerOccupied.OwnerOccupiedColumn.ColumnName;
                this.OwnerOccupiedComboBox.ValueMember = this.getOwnerOccupiedData.F25051OwnerOccupied.OwnerOccupiedIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Parcel Class Type.
        /// </summary>
        private void ListParcelClass()
        {
            this.getParcelClassData = this.form25051Control.WorkItem.F25051ParcelClassTypes();
            if (this.getParcelClassData.F25051ParcelClassTypes.Rows.Count > 0)
            {
                DataTable agland = this.getParcelClassData.Tables[3];
                this.AglandComboBox.DataSource = agland.DefaultView.ToTable();
                this.AglandComboBox.DisplayMember = this.getParcelClassData.F25051ParcelClassTypes.ClassColumn.ColumnName;
                this.AglandComboBox.ValueMember = this.getParcelClassData.F25051ParcelClassTypes.ClassIDColumn.ColumnName;

                DataTable Nonagland = this.getParcelClassData.Tables[3];
                this.NonAgLandComboBox.DataSource = Nonagland.DefaultView.ToTable();
                this.NonAgLandComboBox.DisplayMember = this.getParcelClassData.F25051ParcelClassTypes.ClassColumn.ColumnName;
                this.NonAgLandComboBox.ValueMember = this.getParcelClassData.F25051ParcelClassTypes.ClassIDColumn.ColumnName;
                
                DataTable agImprovement = this.getParcelClassData.Tables[3];
                this.AgImprovementComboBox.DataSource = agImprovement.DefaultView.ToTable();
                this.AgImprovementComboBox.DisplayMember = this.getParcelClassData.F25051ParcelClassTypes.ClassColumn.ColumnName;
                this.AgImprovementComboBox.ValueMember = this.getParcelClassData.F25051ParcelClassTypes.ClassIDColumn.ColumnName;

                DataTable NonagImprovement = this.getParcelClassData.Tables[3];
                this.NonAgImprovementComboBox.DataSource = NonagImprovement.DefaultView.ToTable();
                this.NonAgImprovementComboBox.DisplayMember = this.getParcelClassData.F25051ParcelClassTypes.ClassColumn.ColumnName;
                this.NonAgImprovementComboBox.ValueMember = this.getParcelClassData.F25051ParcelClassTypes.ClassIDColumn.ColumnName;

                DataTable Nonresidential = this.getParcelClassData.Tables[3];
                this.NonResidentialsComboBox.DataSource = Nonresidential.DefaultView.ToTable();
                this.NonResidentialsComboBox.DisplayMember = this.getParcelClassData.F25051ParcelClassTypes.ClassColumn.ColumnName;
                this.NonResidentialsComboBox.ValueMember = this.getParcelClassData.F25051ParcelClassTypes.ClassIDColumn.ColumnName;
            }
        }


        /// <summary>
        /// Primaries the type of the land.
        /// </summary>
        private void PrimaryLandType()
        {
            this.getPrimaryLandTypeData = this.form25051Control.WorkItem.ListPrimaryLandType();
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
        /// Loads the ParcelHeaderDetails
        /// </summary>
        private void F25051GetParcelDetails()
        {
            this.parcelHeaderData.EnforceConstraints = false;
            this.parcelHeaderData = this.form25051Control.WorkItem.F25051_GetParcelDetails(this.keyID);

            if (this.parcelHeaderData.f25051ParcelHeader.Rows.Count > 0)
            {
                int.TryParse(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ParcelIDColumn].ToString(), out this.parcelId);
                string parcelNofield = string.Empty;
                parcelNofield = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ParcelNumberColumn].ToString();
                if (parcelNofield.Contains("&"))
                {
                    parcelNofield = parcelNofield.Replace("&", "&&");
                }
                this.ParcelNolinkLabel.Text = parcelNofield;
                this.rollYear = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.RollYearColumn].ToString();
                this.TypeTextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.TypeColumn].ToString();
                this.AssessmentTypeTextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.AssessmentTypeColumn].ToString();
                this.RetiredTextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.RetiredColumn].ToString();
                this.ExemptTextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ExemptColumn].ToString();
                int.TryParse (this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.TaxDistrictIDColumn].ToString(), out this.districtId);
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ImprovementColumn].ToString()))
                {
                    this.PrimaryImprovementCombo.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ImprovementColumn].ToString();
                    this.tempprimaryowner = (int)this.PrimaryImprovementCombo.SelectedValue;
                }
                else
                {
                    this.PrimaryImprovementCombo.SelectedValue = 0;
                    this.tempprimaryowner = 0;
                }

                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LandTypeColumn].ToString()))
                {
                    this.PrimaryLandTypeComboBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LandTypeColumn].ToString();
                    this.tempprimaylandtype = (int)this.PrimaryLandTypeComboBox.SelectedValue;
                }
                else
                {
                    this.PrimaryLandTypeComboBox.SelectedValue = 0;
                    this.tempprimaylandtype = 0;
                }
                this.DescriptionTextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.DescriptionColumn].ToString();
                string primaryowner = string.Empty;
                primaryowner = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.PrimaryOwnerColumn].ToString();
                if (primaryowner.Contains("&"))
                {
                    primaryowner = primaryowner.Replace("&", "&&");
                }
                this.PrimaryOwnerlinkLabel.Text = primaryowner;
                string primarysitus = string.Empty;
                primarysitus = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.SitusColumn].ToString();
                if (primarysitus.Contains("&"))
                {
                    primarysitus = primarysitus.Replace("&", "&&");
                }
                this.PrimarySituslinkLabel.Text = primarysitus;
                string legalField = string.Empty;
                legalField = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LegalColumn].ToString();
                if (legalField.Contains("&"))
                {
                    legalField = legalField.Replace("&", "&&");
                }
                this.LegallinkLabel.Text = legalField;
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassAgLandIDColumn].ToString()))
                {
                    this.AglandComboBox.SelectedValue = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassAgLandIDColumn].ToString();
                    this.tempAgland = (int)this.AglandComboBox.SelectedValue;
                }
                else
                {
                    this.AglandComboBox.SelectedValue = 0;
                    this.tempAgland = -1;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassNonAgLandIDColumn].ToString()))
                {
                    this.NonAgLandComboBox.SelectedValue = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassNonAgLandIDColumn].ToString();
                    this.tempNonAgland = (int)this.NonAgLandComboBox.SelectedValue;
                }
                else
                {
                    this.NonAgLandComboBox.SelectedValue = 0;
                    this.tempNonAgland = 0;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassAgImprovementIDColumn].ToString()))
                {
                    this.AgImprovementComboBox.SelectedValue = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassAgImprovementIDColumn].ToString();
                    this.tempAgImprovement = (int)this.AgImprovementComboBox.SelectedValue;
                }
                else
                {
                    this.AgImprovementComboBox.SelectedValue = 0;
                    this.tempAgImprovement = 0;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassNonAgImprovementIDColumn].ToString()))
                {
                    this.NonAgImprovementComboBox.SelectedValue = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassNonAgImprovementIDColumn].ToString();
                    this.tempNonAgImprovement = (int)this.NonAgImprovementComboBox.SelectedValue;
                }
                else
                {
                    this.NonAgImprovementComboBox.SelectedValue = 0;
                    this.tempNonAgImprovement = 0;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassNonAgResidentialIDColumn].ToString()))
                {
                    this.NonResidentialsComboBox.SelectedValue = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.ClassNonAgResidentialIDColumn].ToString();
                    this.tempNonAgResidential = (int)this.NonResidentialsComboBox.SelectedValue;
                }
                else
                {
                    this.NonResidentialsComboBox.SelectedValue = 0;
                    this.tempNonAgResidential = 0;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.OwnerOccupiedIDColumn].ToString()))
                {
                    this.OwnerOccupiedComboBox.SelectedValue = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.OwnerOccupiedIDColumn].ToString();
                    this.tempOwnerOccupied = (int)this.OwnerOccupiedComboBox.SelectedValue;
                }
                else
                {
                    this.OwnerOccupiedComboBox.SelectedValue = 0;
                    this.tempOwnerOccupied = 0;
                }

                int.TryParse(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.NBHDIDColumn].ToString(), out this.nbhdID);

              
                this.ID1TextBox.Mask = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Mask1Column].ToString();
                this.ID2TextBox.Mask = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Mask2Column].ToString();
                this.ID3TextBox.Mask = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Mask3Column].ToString();
                this.ID4TextBox.Mask = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Mask4Column].ToString();
                this.ID5TextBox.Mask = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Mask5Column].ToString();
                this.ID6TextBox.Mask = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Mask6Column].ToString();
                this.ID7TextBox.Mask = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Mask7Column].ToString();
 
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID1Column].ToString()))
                {
                    this.ID1TextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID1Column].ToString();
                }
                else
                {
                    this.ID1TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID2Column].ToString()))
                {
                    this.ID2TextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID2Column].ToString();
                }
                else
                {
                    this.ID2TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID3Column].ToString()))
                {
                    this.ID3TextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID3Column].ToString();
                }
                else
                {
                    this.ID3TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID4Column].ToString()))
                {
                    this.ID4TextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID4Column].ToString();
                }
                else
                {
                    this.ID4TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID5Column].ToString()))
                {
                    this.ID5TextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID5Column].ToString();
                }
                else
                {
                    this.ID5TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID6Column].ToString()))
                {
                    this.ID6TextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID6Column].ToString();
                }
                else
                {
                    this.ID6TextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID7Column].ToString()))
                {
                    this.ID7TextBox.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.MID7Column].ToString();
                }
                else
                {
                    this.ID7TextBox.Text = string.Empty;
                }
                 string districtfield = string.Empty;
                districtfield = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.DistrictColumn].ToString();
                if (districtfield.Contains("&"))
                {
                    districtfield = districtfield.Replace("&", "&&");
                }
                this.DistrictlinkLabel.Text = districtfield;
                int.TryParse (this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.TaxDistrictIDColumn].ToString(), out this.districtId);
                string Neighborhoodfield = string.Empty;
                Neighborhoodfield = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.NeighborhoodGroupColumn].ToString();
                if (Neighborhoodfield.Contains("&"))
                {
                    Neighborhoodfield = Neighborhoodfield.Replace("&", "&&");
                }
                this.NeighborhoodGrouplinkLabel.Text = Neighborhoodfield;
                this.EventslinkLabel.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.EventsColumn].ToString();
                int.TryParse(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.EventIDColumn].ToString(), out this.eventID);
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LabelColumn].ToString()))
                {
                    this.ID1label.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LabelColumn].ToString() + ":";
                }
                else
                {
                    this.ID1label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe2Column].ToString()))
                {
                    this.ID2label.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe2Column].ToString() + ":";
                }
                else
                {
                    this.ID2label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe3Column].ToString()))
                {
                    this.ID3label.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe3Column].ToString() + ":";
                }
                else
                {
                    this.ID3label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe4Column].ToString()))
                {
                    this.ID4label.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe4Column].ToString() + ":";
                }
                else
                {
                    this.ID4label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe5Column].ToString()))
                {
                    this.ID5label.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe5Column].ToString() + ":";
                }
                else
                {
                    this.ID5label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe6Column].ToString()))
                {
                    this.ID6label.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe6Column].ToString() + ":";
                }
                else
                {
                    this.ID6label.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe7Column].ToString()))
                {
                    this.ID7label.Text = this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.Labe7Column].ToString() + ":";
                }
                else
                {
                    this.ID7label.Text = string.Empty;
                }
                
                
               

                this.MasktextboxAlignment();
                ////Added By Malliga for C0(April 2008)


                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAppraisalByColumn].ToString())) && (this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAppraisalByColumn].ToString() != "0")))
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

                if (((!string.IsNullOrEmpty(this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAdminByColumn].ToString())) && (this.parcelHeaderData.f25051ParcelHeader.Rows[0][this.parcelHeaderData.f25051ParcelHeader.LockAdminByColumn].ToString() != "0")))
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


        #endregion User Deined Methods

        #region Events

        /// <summary>
        /// Loaads the data
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void F25051_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                //// Added for the CO:4988 by Shanmuga Sundaram.A on Feb25,2009
                this.PrimaryImplementation();
                this.PrimaryLandType();
                this.ListOwnerOccupied();
                this.ListParcelClass();  
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

        #endregion

        /// <summary>
        /// ParcelNolinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ParcelNolinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.getForm2000Details = this.form25051Control.WorkItem.GetFormDetails(Convert.ToInt32(2000), TerraScanCommon.UserId);
                if (this.getForm2000Details.Rows.Count > 0)
                {
                    Boolean.TryParse(this.getForm2000Details.Rows[0][getForm2000Details.IsPermissionOpenColumn.ColumnName].ToString(), out this.form2000OpenPermission);
                }

                Form parcelStatusForm = new Form();
                object[] optionalParameter = new object[] { this.keyID };
                parcelStatusForm = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2000, optionalParameter, this.form25051Control.WorkItem);
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
                    MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + "Parcel Status Form", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                object[] optionalParameter = new object[] { this.rollYear,this.ParentFormId };
                districtF15122 = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1512, optionalParameter, this.form25051Control.WorkItem);

                DialogResult districtDialog;
                if (districtF15122 != null)
                {
                    districtDialog = districtF15122.ShowDialog();

                    if (districtDialog == DialogResult.OK)
                    {
                        try
                        {
                             int.TryParse (TerraScanCommon.GetValue(districtF15122, "DistrictId"), out this.districtId);
                            this.districtSlectionDataset = this.form25051Control.WorkItem.F1512_GetDistrictSelectionData(this.districtId, "", "", -999);//this.ConvertStringToInteger(this.districtId), "", "", -999);

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
                subfundForm = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3510, optionalParameter, this.form25051Control.WorkItem);
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
        /// ParcelHeaderPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelHeaderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D20000.F25051"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                subfundForm = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25051Control.WorkItem);
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
                subfundForm = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25051Control.WorkItem);
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
                subfundForm = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25051Control.WorkItem);
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
                subfundForm = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2001, optionalParameter, this.form25051Control.WorkItem);
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

        private void ID2TextBox_TextChanged(object sender, EventArgs e)
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

        private void ID3TextBox_TextChanged(object sender, EventArgs e)
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

        private void ID4TextBox_TextChanged(object sender, EventArgs e)
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

        private void ID5TextBox_TextChanged(object sender, EventArgs e)
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

        private void ID6TextBox_TextChanged(object sender, EventArgs e)
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

        private void ID7TextBox_TextChanged(object sender, EventArgs e)
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

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AglandCombobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AglandComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempAgland = (int)this.AglandComboBox.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AglandCombobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AglandComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.AglandComboBox.SelectedValue != null)
                {
                    if (!this.tempAgland.Equals(this.AglandComboBox.SelectedValue))
                    {
                        this.SetEditRecord();
                        if (this.AglandComboBox.SelectedValue == null)
                        {
                            this.AglandComboBox.SelectedValue = -1;
                            this.AglandComboBox.Text = string.Empty;
                            this.tempAgland = 0;
                        }
                        else
                        {
                            this.tempAgland = (int)this.AglandComboBox.SelectedValue;
                        }

                    }
                }
                else
                {
                    this.AglandComboBox.Text = string.Empty;
                    this.tempAgland = 0;
                }

            }
            catch (Exception e1)
            {

                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the NonAglandCombobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NonAgLandComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempNonAgland = (int)this.NonAgLandComboBox.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AgImprovementCombobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AgImprovementComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempAgImprovement = (int)this.AgImprovementComboBox.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
              
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the NonAgImprovement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NonAgImprovementComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempNonAgImprovement = (int)this.NonAgImprovementComboBox.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the NonResidentialCombobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NonResidentialsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempNonAgResidential = (int)this.NonResidentialsComboBox.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the OwnerOccupied control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerOccupiedComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.tempOwnerOccupied = (int)this.OwnerOccupiedComboBox.SelectedValue;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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
                reviewForm = this.form25051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2409, optionalParameter, this.form25051Control.WorkItem);
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

        /// Handles the Validating event of the AgImprovementComboBo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void AgImprovementComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if(this.AgImprovementComboBox.SelectedValue !=null)
                {
                    if (!this.tempAgImprovement.Equals(this.AgImprovementComboBox.SelectedValue))
                    {
                        this.SetEditRecord();
                        if (this.AgImprovementComboBox.SelectedValue == null)
                        {
                            this.AgImprovementComboBox.SelectedValue = -1;
                            this.AgImprovementComboBox.Text = string.Empty;
                            this.tempAgImprovement = 0;
                        }
                        else
                        {
                            this.tempAgImprovement = (int)this.AgImprovementComboBox.SelectedValue;
                        }
                    }  
                }
                else
                {
                    this.AgImprovementComboBox.Text = string.Empty;
                    this.tempAgImprovement = 0;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// Handles the Validating event of the NonAgImprovementComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void NonAgImprovementComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.NonAgImprovementComboBox.SelectedValue != null)
                {
                    if (!this.tempNonAgImprovement.Equals(this.NonAgImprovementComboBox.SelectedValue))
                    {
                        this.SetEditRecord();
                        if (this.NonAgImprovementComboBox.SelectedValue == null)
                        {
                            this.NonAgImprovementComboBox.SelectedValue = -1;
                            this.NonAgImprovementComboBox.Text = string.Empty;
                            this.tempNonAgImprovement = 0;
                        }
                        else
                        {
                            this.tempNonAgImprovement = (int)this.NonAgImprovementComboBox.SelectedValue;
                        }

                    }
                }
                else
                {
                    this.NonAgImprovementComboBox.Text = string.Empty;
                    this.tempNonAgImprovement = 0;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// Handles the Validating event of the NonAgLandComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void NonAgLandComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.NonAgLandComboBox.SelectedValue != null)
                {
                    if (!this.tempNonAgland.Equals(this.NonAgLandComboBox.SelectedValue))
                    {
                        this.SetEditRecord();
                        if (this.NonAgLandComboBox.SelectedValue == null)
                        {
                            this.NonAgLandComboBox.SelectedValue = -1;
                            this.NonAgLandComboBox.Text = string.Empty;
                            this.tempNonAgland = 0;
                        }
                        else
                        {
                            this.tempNonAgland = (int)this.NonAgLandComboBox.SelectedValue;
                        }
                    }

                }
                else
                {
                    this.NonAgLandComboBox.Text = string.Empty;
                    this.tempNonAgland = 0;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// Handles the Validating event of the NonResidentialsComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void NonResidentialsComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.NonResidentialsComboBox.SelectedValue != null)
                {
                    if (!this.tempNonAgResidential.Equals(this.NonResidentialsComboBox.SelectedValue))
                    {
                        this.SetEditRecord();
                        if (this.NonResidentialsComboBox.SelectedValue == null)
                        {
                            this.NonResidentialsComboBox.SelectedValue = -1;
                            this.NonResidentialsComboBox.Text = string.Empty;
                            this.tempNonAgResidential = 0;
                        }
                        else
                        {
                            this.tempNonAgResidential = (int)this.NonResidentialsComboBox.SelectedValue;
                        }

                    }
                }
                else
                {
                    this.NonResidentialsComboBox.Text = string.Empty;
                    this.tempNonAgResidential = 0;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// Handles the Validating event of the OwnerOccupiedComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
       private void OwnerOccupiedComboBox_Validating(object sender, CancelEventArgs e)
       {
        try
        {
           if (this.OwnerOccupiedComboBox.SelectedValue != null)
            {
                if (!this.tempOwnerOccupied.Equals(this.OwnerOccupiedComboBox.SelectedValue))
                {
                    this.SetEditRecord();
                    if (this.OwnerOccupiedComboBox.SelectedValue == null)
                    {
                        this.OwnerOccupiedComboBox.SelectedValue = -1;
                        this.OwnerOccupiedComboBox.Text = string.Empty;
                        this.tempOwnerOccupied = 0;
                    }
                    else
                    {
                        this.tempOwnerOccupied = (int)this.OwnerOccupiedComboBox.SelectedValue;
                    }

                }
            }
            else
            {
                this.OwnerOccupiedComboBox.Text = string.Empty;
                this.tempOwnerOccupied = 0;
            }
        }
        catch (Exception e1)
        {
            ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        }
        }

       private void AglandComboBox_Leave(object sender, EventArgs e)
       {
           //try
           //{
           //    if (this.AglandComboBox.SelectedValue != null)
           //    {
           //        if (!this.tempAgland.Equals(this.AglandComboBox.SelectedValue))
           //        {
           //            this.SetEditRecord();
           //            if (this.AglandComboBox.SelectedValue == null)
           //            {
           //                this.AglandComboBox.SelectedValue = -1;
           //                this.AglandComboBox.Text = string.Empty;
           //                this.tempAgland = 0;
           //            }
           //            else
           //            {
           //                this.tempAgland = (int)this.AglandComboBox.SelectedValue;
           //            }

           //        }
           //    }
           //}
           //catch (Exception e1)
           //{

           //    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
           //}

       }
        
        



    }
}
