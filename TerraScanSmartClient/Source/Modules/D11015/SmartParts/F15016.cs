//--------------------------------------------------------------------------------------------
// <copyright file="F15016.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for F15016. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 04 April 07		Ramya D             Created
//*********************************************************************************/
namespace D11015
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
    using System.Web.Services.Protocols;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// F15016
    /// </summary>
    public partial class F15016 : BaseSmartPart
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
        private F15016StatementHeaderData form15016StatementHeaderData = new F15016StatementHeaderData();

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance of 15016 Controller to call the WorkItem
        /// </summary>
        private F15016Controller form15016Control;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Used to store the statementHeaderSlimRowCount
        /// </summary>
        private int statementHeaderSlimRowCount;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8042"/> class.
        /// </summary>
        public F15016()
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
        public F15016(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
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
        /// For F15016Control
        /// </summary>
        [CreateNew]
        public F15016Controller Form15016Control
        {
            get { return this.form15016Control as F15016Controller; }
            set { this.form15016Control = value; }
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

                    ////to check for invalid key id 
                    if (this.keyID != eventArgs.Data.KeyId)
                    {
                        this.keyID = eventArgs.Data.KeyId;
                        this.form15016StatementHeaderData = this.form15016Control.WorkItem.F15016_GetstatementHeaderSlimDetails(this.keyID);
                        this.statementHeaderSlimRowCount = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows.Count;
                    }

                    if (this.statementHeaderSlimRowCount > 0)
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
                    this.keyID = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.StatemantNumberLinkLabel.Focus();
                    this.LoadDefaultView();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Event Subscription

        /// <summary>
        /// userdefined Methods      
        /// </summary>
        #region userdefined Methods

        /// <summary>
        /// Methods      
        /// </summary>
        private void LoadDefaultView()
        {
            this.ShowPanel(true);
            this.ShowControls(true);
            this.F15016_GetStatementheaderSlimDetails();
        }

        /// <summary>
        /// GetStatementheaderSlimDetails     
        /// </summary>
        private void F15016_GetStatementheaderSlimDetails()
        {
            this.form15016StatementHeaderData = this.form15016Control.WorkItem.F15016_GetstatementHeaderSlimDetails(this.keyID);
            this.statementHeaderSlimRowCount = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows.Count;
            if (this.statementHeaderSlimRowCount > 0)
            {
                this.StatemantNumberLinkLabel.Text = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.StatementNumberColumn].ToString();
                this.RollYearTextBox2.Text = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.RollYearColumn].ToString();
                this.SitusTextBox2.Text = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.SitusColumn].ToString();
                this.ParcelNumberlinkLabel.Text = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.ParcelNumberColumn].ToString();
                this.LegalTextBox2.Text = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.LegalColumn].ToString();
                this.LinkLabel.Text = this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.DistrictColumn].ToString();
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
        /// ShowPanel     
        /// </summary>
        /// <param name="show">bool variable</param>
        private void ShowPanel(bool show)
        {
            this.StatementNumberPanel.Enabled = show;
            this.RollYearpanel1.Enabled = show;
            this.Situspanel2.Enabled = show;
            this.parcelNumberpanel3.Enabled = show;
            this.Legalpanel4.Enabled = show;
            this.Districtpanel5.Enabled = show;
        }

        /// <summary>
        /// ShowControls     
        /// </summary>
        /// <param name="show">bool variable</param>
        private void ShowControls(bool show)
        {
            this.StatemantNumberLinkLabel.Enabled = show;
            this.RollYearTextBox2.Enabled = show;
            this.RollYearTextBox2.IsEditable = false;
            this.SitusTextBox2.Enabled = show;
            this.SitusTextBox2.IsEditable = false;
            this.ParcelNumberlinkLabel.Enabled = show;
            this.LegalTextBox2.Enabled = show;
            this.LegalTextBox2.IsEditable = false;
            this.LinkLabel.Enabled = show;
        }

        /// <summary>
        /// ClearControls     
        /// </summary>
        private void ClearControls()
        {
            this.StatemantNumberLinkLabel.Text = string.Empty;
            this.RollYearTextBox2.Text = string.Empty;
            this.SitusTextBox2.Text = string.Empty;
            this.ParcelNumberlinkLabel.Text = string.Empty;
            this.LegalTextBox2.Text = string.Empty;
            this.LinkLabel.Text = string.Empty;
        }
        #endregion userdefined Methods

        /// <summary>
        /// F15016_Load     
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void F15016_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.LoadDefaultView();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
            this.ActiveControl = StatemantNumberLinkLabel;
            this.StatemantNumberLinkLabel.Focus();
        }

        /// <summary>
        /// StatemantNumberLinkLabel_LinkClicked     
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void StatemantNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;

                if (!string.IsNullOrEmpty(this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.FormColumn].ToString()))
                {
                    formInfo = TerraScanCommon.GetFormInfo(int.Parse(this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.FormColumn].ToString()));
                }
                else
                {
                    formInfo = TerraScanCommon.GetFormInfo(1020);
                }

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
        /// parcelNumberlinkLabel_LinkClicked     
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void ParcelNumberlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(22005);
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
        /// linkLabel1_LinkClicked     
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11002);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = int.Parse(this.form15016StatementHeaderData.f15016StatementHeaderSlim.Rows[0][this.form15016StatementHeaderData.f15016StatementHeaderSlim.DistrictIDColumn].ToString());
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
        /// pictureBox1_MouseEnter     
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.StatementHeaderToolTip.SetToolTip(this.PictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PictureBox_Click     
        /// </summary>
        ///  /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        private void PictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D11015.F15016"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
