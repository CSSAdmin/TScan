//--------------------------------------------------------------------------------------------
// <copyright file="F27007.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27007.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 may 2007      Ramya.D             Created
//*********************************************************************************/

namespace D22006
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
    /// F27007
    /// </summary>
    public partial class F27007 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Usde to store parcelHeaderSlimRowCount
        /// </summary>
        private int parcelHeaderSlimRowCount;

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
        private F27007ParcelHeaderSlimData form27007ParcelHeaderSlimData = new F27007ParcelHeaderSlimData();

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance of 8106 Controller to call the WorkItem
        /// </summary>
        private F27007Controller form27007Control;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8042"/> class.
        /// </summary>
        public F27007()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8042"/> class.
        /// </summary>
        ///<param name="masterform">masterform</param>
        ///<param name="formNo">formNo</param>
        ///<param name="keyID">keyID</param>
        ///<param name="red">red</param>
        ///<param name="green">green</param>
        ///<param name="blue">blue</param>
        ///<param name="tabText">tabText</param>
        ///<param name="permissionEdit">permissionEdit</param>
        public F27007(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyID = keyID;
            this.PictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PictureBox.Height, this.PictureBox.Width, tabText, red, green, blue);
        }
        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        #endregion Event Publication

        #region Property
        /// <summary>
        /// For F27007Control
        /// </summary>
        [CreateNew]
        public F27007Controller Form27007Control
        {
            get { return this.form27007Control as F27007Controller; }
            set { this.form27007Control = value; }
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

                    ////to check for invalid key id 
                    if (this.keyID != eventArgs.Data.KeyId)
                    {
                        this.keyID = eventArgs.Data.KeyId;
                        this.form27007ParcelHeaderSlimData = this.form27007Control.WorkItem.F27007_GetParcelHeaderSlimDetails(this.keyID);
                        this.parcelHeaderSlimRowCount = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows.Count;
                    }

                    if (this.parcelHeaderSlimRowCount > 0)
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
        /// D9030_F9030_LoadSliceDetails
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyID = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            this.F27007_GetParcelheaderSlimDetails();
        }

        /// <summary>
        /// F27007_GetParcelheaderSlimDetails
        /// </summary>
        private void F27007_GetParcelheaderSlimDetails()
        {
            this.form27007ParcelHeaderSlimData = this.form27007Control.WorkItem.F27007_GetParcelHeaderSlimDetails(this.keyID);
            this.parcelHeaderSlimRowCount = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows.Count;
            if (this.parcelHeaderSlimRowCount > 0)
            {
                this.ParcelnumberLinkLabel1.Text = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.ParcelNumberColumn].ToString();
                this.RollYearTextBox.Text = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.RollYearColumn].ToString();
                this.SitusTextBox1.Text = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.SitusColumn].ToString();
                this.DistrictlinkLabel2.Text = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.DistrictColumn].ToString();
                this.LegalTextBox2.Text = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.LegalColumn].ToString();
                this.MapTextBox3.Text = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Mask1Column].ToString();
                this.Maplabel9.Text = this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.LabelColumn].ToString() + ":";
                this.ShowPanel(true);
                this.ShowControls(true);
            }
            else
            {
                this.ClearControls();
                this.ShowPanel(false);
                this.ShowControls(false);
            }
        }

        /// <summary>
        /// ClearControls
        /// </summary>
        private void ClearControls()
        {
            this.ParcelnumberLinkLabel1.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.SitusTextBox1.Text = string.Empty;
            this.DistrictlinkLabel2.Text = string.Empty;
            this.LegalTextBox2.Text = string.Empty;
            this.MapTextBox3.Text = string.Empty;
        }

        /// <summary>
        /// ShowControls
        /// </summary>
        /// <param name="show">show</param>
        private void ShowControls(bool show)
        {
            this.ParcelnumberLinkLabel1.Enabled = show;
            this.RollYearTextBox.Enabled = show;
            this.SitusTextBox1.Enabled = show;
            this.DistrictlinkLabel2.Enabled = show;
            this.LegalTextBox2.Enabled = show;
            this.MapTextBox3.Enabled = show;
        }

        /// <summary>
        /// ShowPanel
        /// </summary>
        /// <param name="show">show</param>
        private void ShowPanel(bool show)
        {
            this.ParcelNumberPanel.Enabled = show;
            this.RollYearpanel1.Enabled = show;
            this.Situspanel2.Enabled = show;
            this.Districtpanel3.Enabled = show;
            this.Legalpanel4.Enabled = show;
            this.Mappanel5.Enabled = show;
        }

        #endregion User Defined Methods

        /// <summary>
        /// F27007_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F27007_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.LoadDefaultView();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelnumberLinkLabel1_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelnumberLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                ////Commented by Biju on 22/Jun/2009 to fix #1377
                ////formInfo = TerraScanCommon.GetFormInfo(22005);
                ////Added by Biju on 22/Jun/2009 to fix #1377
                formInfo = TerraScanCommon.GetFormInfo(30000);
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
        /// DistrictlinkLabel2_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DistrictlinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                ////Added by Biju on 22/Jun/2009 to fix #1377
                Int64 districtID = 0;
                formInfo = TerraScanCommon.GetFormInfo(11002);
                formInfo.optionalParameters = new object[1];
                ////Added by Biju on 22/Jun/2009 to fix #1377
                if (this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows.Count > 0)
                {
                    Int64.TryParse(this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.Rows[0][this.form27007ParcelHeaderSlimData.f27007ParcelHeaderSlim.DistrictIDColumn].ToString(), out districtID);
                }
                ////till here
                formInfo.optionalParameters[0] = districtID;//Commented/Added by Biju on 22/Jun/2009 to fix #1377//this.keyID ;
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
        /// PictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D22006.F27007"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ParcelHeaderSlimToolTip.SetToolTip(this.PictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
