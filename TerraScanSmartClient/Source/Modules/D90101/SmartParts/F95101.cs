//--------------------------------------------------------------------------------------------
// <copyright file="F95101.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Reference Data.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13 Aug 07       H.Vinayagamurthy       Created
// 
//*********************************************************************************/

namespace D90101
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Infragistics.Win;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;
    using TerraScan.BusinessEntities;
    using Infragistics.Documents.Excel;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using Infragistics.Win.UltraWinGrid;

    /// <summary>
    /// Inheriting BaseSmartPart
    /// </summary>
    public partial class F95101 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// controller F95005
        /// </summary>
        private F95101Controller form95101Control;

        /// <summary>
        /// Master form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Calling form number
        /// </summary>
        private int callingFormNo;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// Audit trail keyID
        /// </summary>
        private int auditKeyid;

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Form 95101 Constructor
        /// </summary>
        public F95101()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F95101(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            ////KeyID is the Master form Number
            this.callingFormNo = keyID;
            ////When their is No KeyID and Set to Zero
            this.auditKeyid = 0;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.AuditTrailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AuditTrailPictureBox.Height, this.AuditTrailPictureBox.Width, this.sectionIndicatorText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F95101"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassId">The feature class id.</param>
        public F95101(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            ////KeyID is the Master Form Number
            this.callingFormNo = keyID;
            ////Feature ClassID is Set to KeyID
            this.auditKeyid = featureClassId;
            this.Tag = formNo;           
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.AuditTrailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AuditTrailPictureBox.Height, this.AuditTrailPictureBox.Width, this.sectionIndicatorText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F95101Control
        /// </summary>
        [CreateNew]
        public F95101Controller Form95101Control
        {
            get { return this.form95101Control as F95101Controller; }
            set { this.form95101Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.masterFormNo)
                {
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    this.Height = this.AuditTrailPictureBox.Height;
                    sliceResize.SliceFormHeight = this.AuditTrailPictureBox.Height;
                    this.AuditTrailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AuditTrailPictureBox.Height, this.AuditTrailPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                }
         }

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
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
                    this.LoadAuditTrailDataGrid();
                }
         }

        #region Protected Methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
            }
        }

        #endregion Protected methods

        #endregion Event Subscription

        #region Methods

        /// <summary>
        /// To Set the smart Part Height
        /// </summary>
        /// <param name="recordCount">Record Count</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 5)
            {
                if (recordCount > 10)
                {
                    recordCount = 10;
                }

                int increment = ((recordCount - 5) * 21);
                this.AuditTrailDataPanel.Height = 164 + increment;
                this.AuditTrailDataGrid.Height = 165 + increment;  
                this.AuditTrailPictureBox.Height = this.AuditTrailDataPanel.Height;
                this.AuditTrailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AuditTrailPictureBox.Height, this.AuditTrailPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.Height = this.AuditTrailPictureBox.Height;
                ////To Assgin Empty Row at End of the Row
                this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
                this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
            else
            {
                this.AuditTrailDataPanel.Height = 164;
                this.AuditTrailDataGrid.Height = 164 + 1;
                this.AuditTrailPictureBox.Height = this.AuditTrailDataPanel.Height;
                this.AuditTrailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AuditTrailPictureBox.Height, this.AuditTrailPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.Height = this.AuditTrailPictureBox.Height;

                ////To Assgin Empty Row at End of the Row
                this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.AuditTrailDataGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
        }

        /// <summary>
        /// Load Audit Trail Data to DataSet
        /// </summary>
        private void LoadAuditTrailDataGrid()
        {
            DataSet auditTrailData = new DataSet();
            auditTrailData.Tables.Clear();
            auditTrailData = this.form95101Control.WorkItem.F95101_ListAuditTrail(this.callingFormNo, this.auditKeyid);
            this.SetSmartPartHeight(auditTrailData.Tables[0].Rows.Count);
            DataSet auditData = auditTrailData;
            if (auditTrailData.Tables.Count > 0)
            {
              if (auditTrailData.Tables[0] != null)
                {
                    this.AuditTrailDataGrid.DataSource = auditTrailData;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[0].Header.Caption = "KeyID";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[1].Header.Caption = "ItemName";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[2].Header.Caption = "TableName";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[3].Header.Caption = "FieldName";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[4].Header.Caption = "Old";
                    
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[5].Header.Caption = "New";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[6].Header.Caption = "UpdateTime";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[7].Header.Caption = "UserID";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[8].Header.Caption = "Comment";
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[9].Header.Caption = "AuditType";
                    
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[1].Width = 120;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[2].Width = 160;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[3].Width = 115;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[4].Width = 118;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[5].Width = 118;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[6].Width = 160;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[6].MaxLength = 50;
                    
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[8].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[9].CellActivation = Activation.NoEdit;
                    this.AuditTrailDataGrid.DisplayLayout.Bands[0].Columns[0].Band.Override.HeaderClickAction = HeaderClickAction.SortSingle;
                    this.AuditTrailDataGrid.DisplayLayout.Scrollbars = Scrollbars.Both;
                }
            }

            if (auditTrailData.Tables[0].Rows.Count == 0)
            {
                this.AuditTrailDataPanel.Enabled = false;
            }
            else
            {
                this.AuditTrailDataGrid.Rows[0].Activated = true;
                this.AuditTrailDataGrid.DisplayLayout.Rows[0].Selected = true;
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Form Load Event for 95101
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void F95101_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadAuditTrailDataGrid();
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
        /// AuditTrail PictureBox Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void AuditTrailPictureBox_Click(object sender, EventArgs e)
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
        /// AuditTrailPictureBox MouseHover Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void AuditTrailPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.AuditTrailFormSliceToolTip.SetToolTip(this.AuditTrailPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events
    }
}
