//--------------------------------------------------------------------------------------------
// <copyright file="F15022.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15022.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 Jun 07           Sri Parameswari       	    Created// 
//*********************************************************************************
namespace D11020
{
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

    /// <summary>
    /// F15022 Calss file
    /// </summary>
    public partial class F15022 : BaseSmartPart
    {
        #region Variables
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
        //// private bool formMasterPermissionEdit;

        /// <summary>
        /// Instance of F35001 Controller to call the WorkItem
        /// </summary>
        private F15022Controller form15022Controller;

        /// <summary>
        /// form15022RealPropertyDatas
        /// </summary>
        private F11020RealPropertyData form15022RealPropertyDatas = new F11020RealPropertyData();

        /// <summary>
        /// districtId containg form.districtId - default value -999
        /// </summary>
        private int districtId = -999;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15022"/> class.
        /// </summary>
        public F15022()
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
        public F15022(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
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
        public F15022Controller F15022Control
        {
            get { return this.form15022Controller as F15022Controller; }
            set { this.form15022Controller = value; }
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
                ////check for invalid keyid
                if (this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows.Count > 0)
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
                this.keyId = eventArgs.Data.SelectedKeyId;
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
            RollYearTextBox.Text = string.Empty;
            LevyYearTextBox.Text = string.Empty;
            DistrictLinkLabel.Text = string.Empty;
            TotalValueTextBox.Text = string.Empty;
            OriginalTaxTextBox.Text = string.Empty;
            SitusTextBox.Text = string.Empty;
            ExemptionsTextBox.Text = string.Empty;
            DeductionsTextBox.Text = string.Empty;
            LegalTextBox.Text = string.Empty;
            FillingPenaltyTextBox.Text = string.Empty;
            TaxableValueTextBox.Text = string.Empty;
            TaxBillableTextBox.Text = string.Empty;
            ScheduleNumberLinkLabel.Text = string.Empty;
            MapTextBox.Text = string.Empty;
            OwnerlinkLabel.Text = string.Empty;
            OwnerStatusLinkLabel.Text = string.Empty;
        }

        /// <summary>
        /// GetStatementDetails
        /// </summary>
        private void GetStatementDetails()
        {
            int lowStatusVal;
            int highStatusVal;
            int ownerCount;

            this.form15022RealPropertyDatas.Clear();
            this.form15022RealPropertyDatas = this.F15022Control.WorkItem.F15022_GetRealPropertyStatements(this.keyId);
            if (this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows.Count > 0)
            {
                StatementNumberTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.StatementNumberColumn].ToString();
                RollYearTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.RollYearColumn].ToString();
                LevyYearTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.LevyYearColumn].ToString();
                DistrictLinkLabel.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictColumn].ToString();
                TotalValueTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.TotalValueColumn].ToString();
                OriginalTaxTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.OriginalTaxColumn].ToString();
                SitusTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.SitusColumn].ToString();
                ExemptionsTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.TotalExemptionsColumn].ToString();
                DeductionsTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.TotalDeductionsColumn].ToString();
                LegalTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.LegalColumn].ToString();
                FillingPenaltyTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.LateFilingPercentColumn].ToString();
                TaxableValueTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.TaxableValueColumn].ToString();
                TaxBillableTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.TaxBilledColumn].ToString();
                ScheduleNumberLinkLabel.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.ParcelNumberColumn].ToString();
                MapTextBox.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.MapNumberColumn].ToString();
                OwnerlinkLabel.Text = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.OwnerNameColumn].ToString();

                int.TryParse(this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.OwnerCountColumn].ToString(), out ownerCount);
                if ((ownerCount == 0) || (ownerCount == 1))
                {
                    OwnerLabel.Text = "Owner :";
                }
                else
                {
                    OwnerLabel.Text = "Owner (" + ownerCount + " Total) :";
                }

                int.TryParse(this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.lowColumn].ToString(), out lowStatusVal);
                int.TryParse(this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.HighColumn].ToString(), out highStatusVal);
                this.districtId = Convert.ToInt32(this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictIDColumn]);
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
        /// F15022_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F15022_Load(object sender, EventArgs e)
        {
            try
            {
                this.GetStatementDetails();
                this.DistrictLinkLabel.Focus();
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
        /// DistrictLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.districtId = Convert.ToInt32(this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictIDColumn]);
                FormInfo formInfo;
                if (this.districtId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11002);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = Convert.ToInt32(this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictIDColumn]);
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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

                object[] optionalParameter = new object[] { this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.StatementIDColumn], 1, this.masterFormNo };

                ownerstatus = this.form15022Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9102, optionalParameter, this.F15022Control.WorkItem);

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
                if (this.keyId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    Form editStatementForm = this.form15022Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1423, new object[] { this.keyId, this.ParentFormId }, this.F15022Control.WorkItem);
                    if (editStatementForm != null && editStatementForm.ShowDialog() == DialogResult.Yes)
                    {
                        this.GetStatementDetails();
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
        /// ScheduleNumberLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.districtId = Convert.ToInt32(this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictIDColumn]);
                FormInfo formInfo;
                if (this.districtId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11008);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.Rows[0][this.form15022RealPropertyDatas.GetRealPropertyStatementSummarys.DistrictIDColumn];
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
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
        #endregion
    }
}
