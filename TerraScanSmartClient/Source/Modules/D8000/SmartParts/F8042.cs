//--------------------------------------------------------------------------------------------
// <copyright file="F8042.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8042.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17 Oct 06        JAYANTHI              Created
// 10 Nov 06        JAYANTHI              Modified(Code review issues - fixed)  
//
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
    using System.Reflection;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F8042 UI designer Class to subscribe all individual events
    /// </summary>
    public partial class F8042 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// DataSet which is used to get the details of Stoppage event
        /// </summary>
        private TimeFooterData timeFooterData = new TimeFooterData();

        /// <summary>
        /// Instance of 8106 Controller to call the WorkItem
        /// </summary>
        private F8042Controller form8042Control;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyID;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// PermissionFields struct 
        /// </summary>
        private PermissionFields permissions;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;


        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8042"/> class.
        /// </summary>
        public F8042()
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
        public F8042(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyID = keyID;
            this.TimeFooterPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(37, 42, string.Empty, red, green, blue);
        }
        #endregion Constructor

        #region Property
        /// <summary>
        /// For F8106Control
        /// </summary>
        [CreateNew]
        public F8042Controller Form8042Control
        {
            get { return this.form8042Control as F8042Controller; }
            set { this.form8042Control = value; }
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

                    if (this.timeFooterData.GetTimeFooter.Rows.Count > 0)
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
        /// Time grid when added/edited/deleted records, here it has to be refreshed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.FormSlice_TimeFooterCountRefresh, ThreadOption.UserInterface)]
        public void OnFormSlice_TimeFooterCountRefresh(object sender, EventArgs eventArgs)
        {
            try
            {
                this.F8042_GetTimeFooterDetails();
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
            this.F8042_GetTimeFooterDetails();
        }

        /// <summary>
        /// Gets the details of time Footer
        /// </summary>
        private void F8042_GetTimeFooterDetails()
        {
            this.Cursor = Cursors.WaitCursor;
            this.timeFooterData = this.form8042Control.WorkItem.F8042_GetTimeFooterDetails(this.keyID, this.masterFormNo);

            if (this.timeFooterData.GetTimeFooter.Rows.Count > 0)
            {
                this.CountTextBox.Text = this.timeFooterData.GetTimeFooter.Rows[0][this.timeFooterData.GetTimeFooter.TotalCountColumn].ToString();
                this.StartDateTextBox.Text = this.timeFooterData.GetTimeFooter.Rows[0][this.timeFooterData.GetTimeFooter.StartDateColumn].ToString();
                this.EndDateTextBox.Text = this.timeFooterData.GetTimeFooter.Rows[0][this.timeFooterData.GetTimeFooter.EndDateColumn].ToString();
                this.TotalHoursTextBox.Text = this.timeFooterData.GetTimeFooter.Rows[0][this.timeFooterData.GetTimeFooter.TotalHoursColumn].ToString();
                this.TotalCostLinkLabel.ValidateType = TerraScanLinkLabel.ControlValidationType.Decimal;
                this.TotalCostLinkLabel.TextCustomFormat = "$ #,##0.00";
                this.TotalCostLinkLabel.Text = this.timeFooterData.GetTimeFooter.Rows[0][this.timeFooterData.GetTimeFooter.TotalCostColumn].ToString();
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
            this.StartDateTextBox.Text = string.Empty;
            this.EndDateTextBox.Text = string.Empty;
            this.TotalHoursTextBox.Text = string.Empty;
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
            this.StartDateTextBox.Enabled = show;
            this.EndDateTextBox.Enabled = show;
            this.TotalHoursTextBox.Enabled = show;
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
            this.StartDatePanel.Enabled = show;
            this.EndDatePanel.Enabled = show;
            this.TotalCostPanel.Enabled = show;
            this.TotalHoursPanel.Enabled = show;
        }

        #endregion User Defined Methods

        #region Event Handlers
        /// <summary>
        /// Loads the page with the default values
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F8042_Load(object sender, EventArgs e)
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
        private void TimeFooterPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.TimeFooterToolTip.SetToolTip(this.TimeFooterPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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
                TerraScanCommon.ShowReport(800100, TerraScan.Common.Reports.Report.ReportType.Preview, reportOpen);
            }
            catch (Exception exception)
            {
                ExceptionManager.ManageException(exception, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Event Handlers
    }
}
