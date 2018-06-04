//--------------------------------------------------------------------------------------------
// <copyright file="F8046.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8046.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Oct 06        JAYANTHI              Created
// 10 Nov 06        JAYANTHI              Modified(Code review issues - fixed)  
//*********************************************************************************/

namespace D8000
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
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using System.Collections;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F8046 UI designer Class to subscribe all individual events
    /// </summary>
    public partial class F8046 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// DataSet which is used to get the details of Stoppage event
        /// </summary>
        private MaterialsFooterData materialsFooterData = new MaterialsFooterData();

        /// <summary>
        /// Instance of 8106 Controller to call the WorkItem
        /// </summary>
        private F8046Controller form8046Control;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;
        
        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8042"/> class.
        /// </summary>
        public F8046()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8042"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8046(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();                        
            this.masterFormNo = masterform;
            this.keyID = keyID;
            this.MaterialsFooterPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(37, 42, string.Empty, red, green, blue);
        }
        #endregion Constructor   

        #region Property
        /// <summary>
        /// For F8046Control
        /// </summary>
        [CreateNew]
        public F8046Controller Form8046Control
        {
            get { return this.form8046Control as F8046Controller; }
            set { this.form8046Control = value; }
        }
        #endregion Property 

        #region Event Subscription
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
                this.LoadDefaultView();
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
        /// Calls the New Method in Master Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            this.ClearControls();
            this.ShowControls(false);
            this.ShowPanel(false);
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, EventArgs eventArgs)
        {
            this.LoadDefaultView();
        }

        /// <summary>
        /// To call the Cancel button click in Master Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.LoadDefaultView();
        }

        /// <summary>
        /// When a record is added in F8044, the count here and the other data has to get refreshed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">e</param>
        [EventSubscription(EventTopicNames.FormSlice_MaterialFooterCountRefresh, ThreadOption.UserInterface)]
        public void OnFormSlice_MaterialFooterCountRefresh(object sender, EventArgs eventArgs)
        {
            try
            {
                this.F8046_GetMaterialsFooterDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            this.F8046_GetMaterialsFooterDetails();
        }

        /// <summary>
        /// Gets the details of Materials Footer
        /// </summary>
        private void F8046_GetMaterialsFooterDetails()
        {
            this.Cursor = Cursors.WaitCursor;
                this.materialsFooterData = this.form8046Control.WorkItem.F8046_GetMaterialsFooterDetails(this.keyID, this.masterFormNo);

                if (this.materialsFooterData.GetMaterialFooter.Rows.Count > 0)
                {
                    this.CountTextBox.Text = this.materialsFooterData.GetMaterialFooter.Rows[0][this.materialsFooterData.GetMaterialFooter.TotalCountColumn].ToString();
                    this.TotalPartsTextBox.Text = this.materialsFooterData.GetMaterialFooter.Rows[0][this.materialsFooterData.GetMaterialFooter.TotalPartColumn].ToString();
                    this.TotalCostLinkLabel.ValidateType = TerraScanLinkLabel.ControlValidationType.Decimal;
                    this.TotalCostLinkLabel.TextCustomFormat = "$ #,##0.00";
                    this.TotalCostLinkLabel.Text = this.materialsFooterData.GetMaterialFooter.Rows[0][this.materialsFooterData.GetMaterialFooter.TotalCostColumn].ToString();                        
                    this.ShowPanel(true);
                    this.ShowControls(true);                
                }
                else
                {
                    this.ClearControls();
                    this.ShowPanel(false);
                    this.ShowControls(false);
                }

                this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Clears all the controls
        /// </summary>
        private void ClearControls()
        {
            this.CountTextBox.Text = string.Empty;
            this.TotalPartsTextBox.Text = string.Empty;
            this.TotalCostLinkLabel.ValidateType = TerraScanLinkLabel.ControlValidationType.Text;
            this.TotalCostLinkLabel.TextCustomFormat = string.Empty;
            this.TotalCostLinkLabel.Text = string.Empty;            
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [lock control].</param>
        private void ShowControls(bool show)
        {
            this.CountTextBox.Enabled = show;
            this.EmptyPanel3.Enabled = show;
            this.TotalPartsTextBox.Enabled = show;
            this.EmptyPanel1.Enabled = show;
            this.EmptyPanel2.Enabled = show;           
        }

        /// <summary>
        /// Enables or disables the Panels accordingly
        /// </summary>
        /// <param name="show">bool value to enable/Disable</param>
        private void ShowPanel(bool show)
        {
            this.CountPanel.Enabled = show;
            this.EmptyPanel3.Enabled = show;            
            this.TotalCostPanel.Enabled = show;
            this.TotalPartsPanel.Enabled = show;
        }

        #endregion User Defined Methods

        #region Event Handlers
        /// <summary>
        /// Link button click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TotalCostLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hashtable reportOpen = new Hashtable();
            try
            {
                reportOpen.Add("KeyId", this.keyID);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(800101, TerraScan.Common.Reports.Report.ReportType.Preview, reportOpen);
            }
            catch (Exception exception)
            {
                ExceptionManager.ManageException(exception, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }            
        }

        /// <summary>
        /// Loads the page with the default values
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F8046_Load(object sender, EventArgs e)
        {
            try
            {
            this.FlagSliceForm = true;
            this.LoadDefaultView();
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
        /// Displays the tooltip, On Mouse Enter of the picture box 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MaterialsFooterPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.MaterialsFooterToolTip.SetToolTip(this.MaterialsFooterPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Event Handlers        
    }
}
