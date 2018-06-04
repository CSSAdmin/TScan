//--------------------------------------------------------------------------------------------
// <copyright file="F15021.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15021.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 Dec 06        Ranjani JG        	Created
// 07 Dec 07        LathaMaheswari.D    Modified - Change Order has implemented - Added two Text Boxex Frozen Value and Misc Assessment
//*********************************************************************************
namespace D11020
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
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
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Form F15021
    /// </summary>
    [SmartPart]
    public partial class F15021 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15021Control Controller
        /// </summary>
        private F15021Controller form15021Control;

        /// <summary>
        /// DataSet Contains Institution Detail 
        /// </summary>
        private F11020RealPropertyData realProperty = new F11020RealPropertyData();

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// parcelId containg form.ParcelID - default value -999
        /// </summary>
        private int parcelId = -999;

        /// <summary>
        /// districtId containg form.districtId - default value -999
        /// </summary>
        private int districtId = -999;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15021"/> class.
        /// </summary>
        public F15021()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15021"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15021(int masterform, int formNo, int keyId, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyId;
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.Height, this.StatementPictureBox.Width, tabText, red, green, blue);
        }

        #endregion

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F15021 control.
        /// </summary>
        /// <value>The F15021 control.</value>
        [CreateNew]
        public F15021Controller Form15021Control
        {
            get { return this.form15021Control as F15021Controller; }
            set { this.form15021Control = value; }
        }

        #endregion

        #region Event Subscription

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

                    // check wether the form is populated with records
                    // based on thekeyid
                    if (this.realProperty.GetRealPropertyStatement.Rows.Count > 0)
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

                    //this.ActiveControl = this.DistrictLinkLabel;
                    //DistrictLinkLabel.Focus();
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
        /// Loads the slice details.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> e)
        {
            try
            {
                if (e.Data.MasterFormNo == this.masterFormNo)
                {
                    this.keyId = e.Data.SelectedKeyId;
                    ////populate statement details with the new key id           
                    this.GetStatementDetails();
                    //this.ActiveControl = this.DistrictLinkLabel;
                    //DistrictLinkLabel.Focus();
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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F15021 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15021_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                ////getStatement function is used to load the Statement in Realproperty slice
                this.GetStatementDetails();
                this.ActiveControl = DistrictLinkLabel;
                this.DistrictLinkLabel.Focus();
                this.StatementPictureBox.Enabled = true;
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

        /// <summary>
        /// Gets the statement details and fill realestatemasterform accordingly.
        /// </summary>
        private void GetStatementDetails()
        {
            int ownerCount;
            int lowStatusVal;
            int highStatusVal;

            this.realProperty.Clear();
            this.realProperty = this.form15021Control.WorkItem.F11020_GetRealPropertyStatement(this.keyId);

            if (this.realProperty.GetRealPropertyStatement.Rows.Count > 0)
            {
                this.StatementPanel.Enabled = true;
                this.StatementNumberTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.StatementNumberColumn].ToString();
                this.RollYearTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.RollYearColumn].ToString();
                this.LevyYearTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.LevyYearColumn].ToString();
                ////chek for district id existence
                if (!String.IsNullOrEmpty(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.DistrictIDColumn].ToString()))
                {
                    this.districtId = Convert.ToInt32(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.DistrictIDColumn]);
                    this.DistrictLinkLabel.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.DistrictColumn].ToString();
                }
                else
                {
                    ////assign default value
                    this.districtId = -999;
                    this.DistrictLinkLabel.Text = String.Empty;
                }

                this.TotalValueTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.TotalValueColumn].ToString();
                this.OriginalTaxTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.OriginalTaxColumn].ToString();
                this.SitusTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.SitusColumn].ToString();
                this.ExemptionsTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.TotalExemptionsColumn].ToString();
                this.DeductionsTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.TotalDeductionsColumn].ToString();
                this.MiscTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.MiscAssessmentsColumn].ToString();
                this.FrozenLabel.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.FrozenLabelColumn].ToString();
                this.FrozenTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.FrozenValueColumn].ToString();
                this.LegalTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.LegalColumn].ToString();
                this.TaxableValueTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.TaxableValueColumn].ToString();
                this.TaxBillableTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.TaxBilledColumn].ToString();
                this.MapTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.MapNumberColumn].ToString();
                this.OwnerlinkLabel.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.OwnerNameColumn].ToString();
                int.TryParse(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.lowColumn].ToString(), out lowStatusVal);
                int.TryParse(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.HighColumn].ToString(), out highStatusVal);
                int.TryParse(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.OwnerCountColumn].ToString(), out ownerCount);
                if ((ownerCount == 0) || (ownerCount == 1))
                {
                    OwnerLabel.Text = "Owner";
                }
                else
                {
                    OwnerLabel.Text = "Owner (" + ownerCount + " Total):";
                }

                if (lowStatusVal > 0)
                {
                    OwnerStatusLinkLabel.Visible = true;
                    OwnerStatusLinkLabel.Text = "Status";
                    OwnerStatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                    OwnerPanel.BackColor = Color.FromArgb(200, 214, 230);
                }

                if (highStatusVal > 0)
                {
                    OwnerStatusLinkLabel.Visible = true;
                    OwnerStatusLinkLabel.Text = "Status";
                    OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                    OwnerPanel.BackColor = Color.FromArgb(237, 205, 203);
                }

                if ((lowStatusVal == 0 && highStatusVal == 0) || (lowStatusVal > 0 && highStatusVal > 0))
                {
                    OwnerStatusLinkLabel.Visible = false;
                    OwnerStatusLinkLabel.Text = "Status";
                    OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                    OwnerPanel.BackColor = Color.FromArgb(255, 255, 255);
                }
                ////chek for parcel id existence
                if (!String.IsNullOrEmpty(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.ParcelIDColumn].ToString()))
                {
                    this.parcelId = Convert.ToInt32(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.ParcelIDColumn]);
                    this.ParcelNumberLinkLabel.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.ParcelNumberColumn].ToString();
                }
                else
                {
                    ////assign default value
                    this.parcelId = -999;
                    this.ParcelNumberLinkLabel.Text = String.Empty;
                }
            }
            else
            {
                this.ClearRealEstateDetails();
            }
        }

        /// <summary>
        /// Clears the real estate details.
        /// </summary>
        private void ClearRealEstateDetails()
        {
            this.StatementNumberTextBox.Text = String.Empty;
            this.RollYearTextBox.Text = String.Empty;
            this.LevyYearTextBox.Text = String.Empty;
            this.TotalValueTextBox.Text = String.Empty;
            this.OriginalTaxTextBox.Text = String.Empty;
            this.SitusTextBox.Text = String.Empty;
            this.ExemptionsTextBox.Text = String.Empty;
            this.DeductionsTextBox.Text = String.Empty;
            this.LegalTextBox.Text = String.Empty;
            this.TaxableValueTextBox.Text = String.Empty;
            this.TaxBillableTextBox.Text = String.Empty;
            this.MapTextBox.Text = String.Empty;
            this.DistrictLinkLabel.Text = String.Empty;
            this.parcelId = -999;
            this.ParcelNumberLinkLabel.Text = String.Empty;
            this.OwnerlinkLabel.Text = String.Empty;
            this.StatementPanel.Enabled = false;
            this.OwnerStatusLinkLabel.Text = string.Empty;
            this.FrozenTextBox.Text = string.Empty;
            this.MiscTextBox.Text = string.Empty;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the StatementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatementPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the StatementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatementPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.StatementToolTip.SetToolTip(this.StatementPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the EditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void EditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.keyId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////real property statement Detail Form - FormID - 1423
                    Form editStatementForm = this.form15021Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1423, new object[] { this.keyId, this.ParentFormId }, this.form15021Control.WorkItem);
                    ////open form in edit mode
                    if (editStatementForm != null && editStatementForm.ShowDialog() == DialogResult.Yes)
                    {
                        ////reload form slice
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
        /// Handles the LinkClicked event of the DistrictLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void DistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.districtId = Convert.ToInt32(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.DistrictIDColumn]);
                FormInfo formInfo;
                if (this.districtId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11002);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.DistrictIDColumn];
                    //// formInfo.optionalParameters[1] = this.featureClassId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    this.Districtpanel.Focus();
                    this.DistrictLinkLabel.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelNumberLinkLabel_LinkClicked_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.districtId = Convert.ToInt32(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.ParcelIDColumn]);
                FormInfo formInfo;
                if (this.districtId > 0)
                {
                    formInfo = TerraScanCommon.GetFormInfo(11006);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.ParcelIDColumn];
                    //// formInfo.optionalParameters[1] = this.featureClassId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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

                object[] optionalParameters = new object[] { this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.StatementIDColumn], 1, this.masterFormNo };
                ownerstatus = this.form15021Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9102, optionalParameters, this.form15021Control.WorkItem);

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
        /// Handles the LinkClicked event of the OwnerlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion
    }
}
