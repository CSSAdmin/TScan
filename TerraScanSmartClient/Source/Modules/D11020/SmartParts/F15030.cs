//--------------------------------------------------------------------------------------------
// <copyright file="F15030.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15030
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15Jun 2007       Sriparameswari.A         Created
//*********************************************************************************/


namespace D11020
{
    #region NameSpace

    using System;
    using System.Collections;
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
    using D11020;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Text.RegularExpressions;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    #endregion

    /// <summary>
    /// F15030
    /// </summary>
    public partial class F15030 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// form15030Control
        /// </summary>
        private F15030Controller form15030Control;

        /// <summary>
        /// parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// districtId
        /// </summary>
        private int districtId;

        /// <summary>
        /// f15030RealPropertyDatas
        /// </summary>
        private F11020RealPropertyData form15030RealPropertyDatas = new F11020RealPropertyData();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        //// <summary>
        //// Created Instance for SupportFormData.GetFormDetailsDataTable
        //// </summary>
        ////private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        //// <summary>
        //// formMasterPermissionEdit
        //// </summary>
        ////private bool formMasterPermissionEdit;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15022"/> class.
        /// </summary>        
        public F15030()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15022"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15030(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            ////this.formMasterPermissionEdit = permissionEdit;
            this.FlagSliceForm = true;
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, tabText, red, green, blue);
        }
        #endregion

        #region EventPublicaton

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

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F15030Controller Form15030Control
        {
            get { return this.form15030Control as F15030Controller; }
            set { this.form15030Control = value; }
        }

        #endregion

        #region EventSubscription
        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
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

            ////check for invalid keyid
            if (this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows.Count > 0)
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

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (eventArgs.Data.MasterFormNo == this.masterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                ////populate statement details with the new key id           
                this.GetStatementDetails();
            }
        }
        #endregion

        #region PrivateMethods

        /// <summary>
        /// ClearAllDetails
        /// </summary>
        private void ClearAllDetails()
        {
            StatementNumberTextBox.Text = string.Empty;
            ParcelNumberLinkLabel.Text = string.Empty;
            RollYearTextBox.Text = string.Empty;
            MinDistrictFeeTextBox.Text = string.Empty;
            TotalAmountTextBox.Text = string.Empty;
            SpecialDistrictLinkLabel.Text = string.Empty;
            IrrigableAcresTextBox.Text = string.Empty;
            RPAcreCountTextBox.Text = string.Empty;
            TurnoutTextBox.Text = string.Empty;
            MortgageLinkLabel.Text = string.Empty;
            TypeTextBox.Text = string.Empty;
            OwnerStatusLinkLabel.Text = string.Empty;
            OwnerlinkLabel.Text = string.Empty;
        }

        /// <summary>
        /// GetStatementDetails
        /// </summary>
        private void GetStatementDetails()
        {
            int lowStatusVal;
            int highStatusVal;
            int ownerCount;

            this.form15030RealPropertyDatas.Clear();
            this.form15030RealPropertyDatas = this.Form15030Control.WorkItem.F15030_GetRealPropertyStatements(this.keyId);
            if (this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows.Count > 0)
            {
                StatementNumberTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.StatementNumberColumn].ToString();
                ParcelNumberLinkLabel.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.ParcelNumberColumn].ToString();
                RollYearTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.RollYearColumn].ToString();
                MinDistrictFeeTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.MinimumDistrictFeeColumn].ToString();
                TotalAmountTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.TotalAmountColumn].ToString();
                SpecialDistrictLinkLabel.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.SpecialDistrictColumn].ToString();
                IrrigableAcresTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.IrrgAcresColumn].ToString();
                RPAcreCountTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.AcresColumn].ToString();
                TurnoutTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.TurnoutsColumn].ToString();
                MortgageLinkLabel.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.MortgageNameColumn].ToString();
                TypeTextBox.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.PostNameColumn].ToString();
                OwnerlinkLabel.Text = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.OwnerNameColumn].ToString();
                int.TryParse(this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.lowColumn].ToString(), out lowStatusVal);
                int.TryParse(this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.HighColumn].ToString(), out highStatusVal);
                int.TryParse(this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.OwnerCountColumn].ToString(), out ownerCount);

                if ((ownerCount == 0) || (ownerCount == 1))
                {
                    OwnerLabel.Text = "Owner";
                }
                else
                {
                    OwnerLabel.Text = "Owner (" + ownerCount + " Total):";
                }

                if (highStatusVal > 0)
                {
                    OwnerStatusLinkLabel.Visible = true;
                    OwnerStatusLinkLabel.Text = "Status";
                    OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                    OwnerPanel.BackColor = Color.FromArgb(237, 205, 203);
                }

                if (lowStatusVal > 0)
                {
                    OwnerStatusLinkLabel.Visible = true;
                    OwnerStatusLinkLabel.Text = "Status";
                    OwnerStatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                    OwnerPanel.BackColor = Color.FromArgb(200, 214, 230);
                }

                if (((lowStatusVal == 0) && (highStatusVal == 0)) || ((lowStatusVal > 0) && (highStatusVal > 0)))
                {
                    OwnerStatusLinkLabel.Visible = false;
                    OwnerStatusLinkLabel.Text = "Status";
                    OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                    OwnerPanel.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            else
            {
                this.ClearAllDetails();
            }
        }

        #endregion

        #region Form Load
        /// <summary>
        /// F15030_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F15030_Load(object sender, EventArgs e)
        {
            try
            {
                this.GetStatementDetails();
                this.ActiveControl = SpecialDistrictLinkLabel;
                ParcelNumberLinkLabel.Focus();
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

        #region Events
        /// <summary>
        /// OwnerStatusLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerStatusLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form ownerstatus = new Form();

                object[] optionalParameters = new object[] { this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.StatementIDColumn], 1, this.masterFormNo };
                ownerstatus = this.form15030Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9102, optionalParameters, this.form15030Control.WorkItem);

                if (ownerstatus != null)
                {
                    ownerstatus.ShowDialog();
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
        /// EditlinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows.Count > 0)
                {
                    this.parcelId = Convert.ToInt32(this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.StatementIDColumn]);
                    FormInfo formInfo;
                    if (this.parcelId > 0)
                    {
                        formInfo = TerraScanCommon.GetFormInfo(10031);
                        formInfo.optionalParameters = new object[1];
                        formInfo.optionalParameters[0] = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.StatementIDColumn];
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelNumberLinkLabel
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows.Count > 0)
                {
                    this.parcelId = Convert.ToInt32(this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.ParcelIDColumn]);
                }
                FormInfo formInfo;
                if (this.parcelId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11006);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.ParcelIDColumn];
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OwnerlinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                if (this.keyId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11015);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.keyId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// StatementPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void StatementPictureBox_Click(object sender, EventArgs e)
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
        /// StatementPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void StatementPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.StatementToolTip.SetToolTip(this.StatementPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SpecialDistrictLinkLabel_LinkClicked_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SpecialDistrictLinkLabel_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.districtId = Convert.ToInt32(this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictIDColumn]);
                FormInfo formInfo;
                if (this.districtId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(10030);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15030RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictIDColumn];
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}
