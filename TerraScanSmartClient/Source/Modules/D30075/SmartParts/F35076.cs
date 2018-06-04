//--------------------------------------------------------------------------------------------
// <copyright file="F35076.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35076
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 2/9/2008        Malliga             Created
//3010/2009        Malliga             Modified for the issue : 4387 
//***********************************************************************************************/

namespace D30075
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
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;
    using System.Globalization;
    using System.Diagnostics;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;

    /// <summary>
    /// 35076
    /// </summary>
    [SmartPart]
    public partial class F35076 : BaseSmartPart, IUIElementDrawFilter
    {
        #region Variables

        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        /// <summary>
        /// Usede to store the form slice key id
        /// </summary>
        private int? stateId;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// controller F35076
        /// </summary>
        private F35076Controller form35076Control;

        ///<summary>
        ///StateAssessesOwnerDetalDataSetData
        ///</summary>
        private F35075StateAssessedData StateAssessesOwnerDataSet = new F35075StateAssessedData();

        /// <summary>
        /// stateItemId
        /// </summary>
        private string stateItemId;

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
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        /// formloadflag
        /// </summary>
        private bool formloadflag = false;

        /// <summary>
        /// stategridheight
        /// </summary>
        private int stategridheight = 20;

        /// <summary>
        /// staterowcount
        /// </summary>
        private int staterowcount = 0;

        /// <summary>
        /// basePanelScrolled variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool basePanelScrolled;

        /// <summary>
        /// yaxisPoint
        /// </summary>
        private int yaxisPoint;

        private string rollYear = string.Empty;

        /// <summary>
        /// ColumnName
        /// </summary>
        private string columnName = string.Empty;

        /// <summary>
        /// columnIndex
        /// </summary>
        private int columnindex;

        ///<Summary>
        /// totalSumValue;
        /// </Summary>
        private decimal totalSumValue = 0;

        ///<summary>
        /// RealTotalSumValue
        /// </summary>
        private decimal realTotalSumValue = 0;

        /// <summary>
        /// PersonalTotalSumValue
        /// </summary>
        private decimal perosonalTotalSumValue = 0;

        private bool flagLoadForm = false;
        private bool QueryLoadForm = false;
        private static int col1pos = 6;
        private static int col2pos = 0;
        private static int col3Pos=3;
        private static int col4pos=2;
        private static int col5pos=4;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F35076"/> [ERROR: invalid expression DeclaringTypeKind].
        /// </summary>
        public F35076()
        {
            InitializeComponent();
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[1].Header.VisiblePosition = 6;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[2].Header.VisiblePosition = 0;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[3].Header.VisiblePosition = 3;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[4].Header.VisiblePosition = 2;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[5].Header.VisiblePosition = 4;
                    

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35076"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35076(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.stateId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.QueryLoadForm = true;
            this.StateAssessedPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StateAssessedPictureBox.Height, this.StateAssessedPictureBox.Width, tabText, red, green, blue);
            //this.StateAssessedGrid.Height = 43;
            //this.MainPanel.Height = this.StateAssessedGrid.Height + this.BottomPanel.Height;
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;


        /// <summary> 
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SetViewMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_SetViewMode;


        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    ////this.StateAssessesOwnerDataSet = new F35075StateAssessedData();
                    if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                int isdeleted = 0;
                isdeleted = this.form35076Control.WorkItem.F35075_DeleteStateAssessed(Convert.ToInt32(this.stateId), TerraScanCommon.UserId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                ////SliceFormCloseAlert sliceFormCloseAlert;
                ////sliceFormCloseAlert.FormNo = this.masterFormNo;
                ////sliceFormCloseAlert.FlagFormClose = false;
                ////this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.performCancel();
            if (col3Pos != 0)
            {
                this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[1].Header.VisiblePosition = col1pos;
                this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[2].Header.VisiblePosition = col2pos;
                this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[3].Header.VisiblePosition = col3Pos;
                this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[4].Header.VisiblePosition = col4pos;
                this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[5].Header.VisiblePosition = col5pos;
            }
            if (this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows.Count > 0)
            {
                int recordcount = Convert.ToInt32(this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows[0]["StateAssessedCount"].ToString());
                if (recordcount.Equals(0))
                {
                    this.MainPanel.Enabled = false;
                }
                else
                {
                    this.MainPanel.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                //if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                //if ((this.slicePermissionField.editPermission) || (this.slicePermissionField.newPermission))
                //{
                if (this.PermissionFiled.editPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                    ////this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Clear();
                this.StateAssessedGrid.DataSource = this.StateAssessesOwnerDataSet.ListStateAssessedDetails;

                this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
                this.EnableControls(true);
                this.RealPropertyTextBox.Text = string.Empty;
                this.PersonalPropertyTextBox.Text = string.Empty;   
                this.TotalValueTextBox.Text = string.Empty;
                this.SetSmartPartHeight();
                this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }

            else
            {
                this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Clear();
                this.StateAssessedGrid.DataSource = this.StateAssessesOwnerDataSet.ListStateAssessedDetails;
                this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
                this.SetSmartPartHeight();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                /// this.EnableControls(false);
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
           // this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
           // this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Width = 150;
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.stateId = eventArgs.Data.SelectedKeyId;
                this.StateAssessesOwnerDataSet = this.form35076Control.WorkItem.F35075_GetStateAssessedOwnerDetails(Convert.ToInt32(this.stateId));

                //Added for TSBG#12769 by purushotham on 20122012
                foreach (DataRow dr in this.StateAssessesOwnerDataSet.ListStateAssessedDetails)
                {
                    if (!string.IsNullOrEmpty(dr["RealProperty"].ToString()))
                    {
                        string rpValue = (dr["RealProperty"].ToString());
                        if (rpValue.Contains("."))
                        {
                            string[] strArray = rpValue.Split('.');
                            if (strArray[0] != null)
                            {
                                rpValue = strArray[0];
                                //rpValue=rpValue.Insert(0, "$");
                            }
                            dr["RealProperty"] = rpValue;
                        }
                        this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                    }
                    if (!string.IsNullOrEmpty(dr["PersonalProperty"].ToString()))
                    {
                        string ppValue = (dr["PersonalProperty"].ToString());
                        if (ppValue.Contains("."))
                        {
                            string[] strArray = ppValue.Split('.');
                            if (strArray[0] != null)
                            {
                                ppValue = strArray[0];
                                //ppValue.Insert(0, "$");
                            }
                            dr["PersonalProperty"] = ppValue;
                        }
                        this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                    }

                }
                this.StateAssessedGrid.DataSource = this.StateAssessesOwnerDataSet.ListStateAssessedDetails;
                if (col3Pos != 0)
                {
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[1].Header.VisiblePosition = col1pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[2].Header.VisiblePosition = col2pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[3].Header.VisiblePosition = col3Pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[4].Header.VisiblePosition = col4pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[5].Header.VisiblePosition = col5pos;
                }
                if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count > 0)
                {
                    this.EnableControls(true);
                    this.CalculateTotal();
                    this.SetSmartPartHeight();
                    this.StateAssessedGrid.Focus();
                    //// this.StateAssessedPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StateAssessedPictureBox.Height, this.StateAssessedPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                }
                else
                {
                    ////this.EnableControls(false);
                    ////if (this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows.Count > 0)
                    ////{
                    ////    int recordcount = Convert.ToInt32(this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows[0]["StateAssessedCount"].ToString());
                    ////    if (recordcount.Equals(0))
                    ////    {
                    ////        this.MainPanel.Enabled = false;
                    ////    }
                    ////    else
                    ////    {
                    ////        this.MainPanel.Enabled = true;
                    ////    }
                    ////    //{
                    ////    //    this.MainPanel.Enabled = true;
                    ////    //}
                    ////}
                    ////else
                    ////{
                    ////    this.MainPanel.Enabled = false;
                    ////}
                    this.TotalValueTextBox.Text = string.Empty;

                    this.SetSmartPartHeight();
                }

                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                {
                    this.PermissionControlLock(false);
                }
                else
                {
                    this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }

        }

        [EventSubscription(EventTopicNames.D84700_F84722_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {

                if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    //if (eventArgs.Data.ParameterList != string.Empty)
                    //{


                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    {
                        if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                        {
                            this.stateId = eventArgs.Data.SelectedKeyId;
                            if (this.stateId == -99)
                            {
                                this.stateId = null;
                            }

                            this.CalculateTotal();

                            /////return;
                            this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                            string StateAssessedGrid = string.Empty;
                            col1pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[1].Header.VisiblePosition;
                            col2pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[2].Header.VisiblePosition;
                            col3Pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[3].Header.VisiblePosition;
                            col4pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[4].Header.VisiblePosition;
                            col5pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[5].Header.VisiblePosition;
                            StateAssessedGrid = TerraScanCommon.GetXmlString(this.StateAssessesOwnerDataSet.ListStateAssessedDetails);
                            int returnValue = this.form35076Control.WorkItem.F35076_SaveStateAssessedGrid(this.stateId, StateAssessedGrid, TerraScanCommon.UserId);

                            ///this.PermissionControlLock(!this.PermissionFiled.editPermission);
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }
                    else
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F95005_AlertFomrMasterCancel(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FomrMasterCancel != null)
            {
                this.D9030_F95005_FomrMasterCancel(this, eventArgs);
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form29610 control.
        /// </summary>
        /// <value>The form29610 control.</value>
        [CreateNew]
        public F35076Controller Form35076Control
        {
            get { return this.form35076Control as F35076Controller; }
            set { this.form35076Control = value; }
        }

        #endregion Property

        #region State Assessed Picture Box
        /// <summary>
        /// Handles the Click event of the StateAssessedPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StateAssessedPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the StateAssessedPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StateAssessedPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.StateAssessedOwnerToolTip.SetToolTip(this.StateAssessedPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F35076 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35076_Load(object sender, EventArgs e)
        {
            try
            {
                
                this.flagLoadForm = false;
                this.StateAssessedGrid.Enabled = true;
                ////Events for Master Form scroll
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(Scroll_Click);
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(this.Smartpart_Scroll);
                this.FlagSliceForm = true;
                this.StateAssessesOwnerDataSet = this.form35076Control.WorkItem.F35075_GetStateAssessedOwnerDetails(Convert.ToInt32(this.stateId));
                this.StateAssessedGrid.DrawFilter = this;
                //Added for TSBG#12769 by purushotham on 20122012
                foreach (DataRow dr in this.StateAssessesOwnerDataSet.ListStateAssessedDetails)
                {
                    if (!string.IsNullOrEmpty(dr["RealProperty"].ToString()))
                    {
                        string rpValue = (dr["RealProperty"].ToString());
                        if (rpValue.Contains("."))
                        {
                            string[] strArray = rpValue.Split('.');
                            if (strArray[0] != null)
                            {
                                rpValue = strArray[0];
                               // rpValue = "$" + rpValue;
                               //rpValue= rpValue.Insert(0, "$");
                            }
                            dr["RealProperty"] = rpValue;
                        }
                        this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                    }
                    if (!string.IsNullOrEmpty(dr["PersonalProperty"].ToString()))
                    {
                        string ppValue = (dr["PersonalProperty"].ToString());
                        if (ppValue.Contains("."))
                        {
                            string[] strArray = ppValue.Split('.');
                            if (strArray[0] != null)
                            {
                                ppValue = strArray[0];
                               // ppValue.Insert(0, "$");
                            }
                            dr["PersonalProperty"] = ppValue;
                        }
                        this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                    }
                    
                }
                this.StateAssessedGrid.DataSource = this.StateAssessesOwnerDataSet.ListStateAssessedDetails;

                //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Format = "$#,###0";//"$#,###0.00";
                //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Format = "$#,###0";
                if (col3Pos != 0)
                {
                    //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Width = 150;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[1].Header.VisiblePosition = col1pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[2].Header.VisiblePosition = col2pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[3].Header.VisiblePosition = col3Pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[4].Header.VisiblePosition = col4pos;
                    this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[5].Header.VisiblePosition = col5pos;
                    
                }
                if (this.StateAssessedGrid.Rows.Count > 0)
                {
                    this.MainPanel.Enabled = true;

                    this.SetSmartPartHeight();
                    this.CalculateTotal();
                    if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                    {
                        this.PermissionControlLock(false);
                    }
                    else
                    {
                        this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    }
                    this.StateAssessedGrid.Focus();
                }
                else
                {
                    this.TotalValueTextBox.Text = string.Empty;

                    if (this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows.Count > 0)
                    {
                        int recordcount = Convert.ToInt32(this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows[0]["StateAssessedCount"].ToString());
                        if (recordcount.Equals(0))
                        {
                            this.EnableControls(false);
                            //this.MainPanel.Enabled = false;
                            //this.StateAssessedGrid.Enabled = false;
                        }
                        else
                        {
                            this.MainPanel.Enabled = true;
                            this.EnableControls(true);
                        }
                        ////{
                        ////    this.MainPanel.Enabled = true;
                        ////}
                    }
                    else
                    {
                        this.MainPanel.Enabled = false;
                    }

                    this.SetSmartPartHeight();

                    this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();

                }
                this.flagLoadForm = true;
                this.QueryLoadForm = false; 

            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.MainPanel.Enabled = enable;
            this.StateAssessedGrid.Enabled = enable;
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            ////this.StateAssessedGrid.Focus();  
            ////this.TotalValueTextBox.Focus();  
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            for (int i = 0; i <= this.StateAssessedGrid.Rows.Count - 1; i++)
            {
                if (string.IsNullOrEmpty(this.StateAssessedGrid.Rows[i].Cells[2].Value.ToString().Trim()))
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                    sliceValidationFields.FormNo = formNo;
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.StateAssessedGrid.ActiveCell = this.StateAssessedGrid.ActiveRow.Cells["ParcelNumber"];
                    this.StateAssessedGrid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    this.StateAssessedGrid.Focus();
                }

                ////Coding modified by malliga on 2/11/2009 [For Checking District column index]
                if (!string.IsNullOrEmpty(this.StateAssessedGrid.Rows[i].Cells[8].Value.ToString().Trim()))
                {
                    if (this.StateAssessedGrid.Rows[i].Cells[8].Value.ToString() == "<<< >>>")
                    {
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = true;
                        this.StateAssessedGrid.ActiveCell = this.StateAssessedGrid.ActiveRow.Cells["District"];
                        this.StateAssessedGrid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                        this.StateAssessedGrid.Focus();
                    }
                }

                if (!string.IsNullOrEmpty(this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][3].ToString().Trim()))
                {
                    int rppos;
                    string rpvalue;
                    decimal test;
                    int rplen;
                    decimal.TryParse(this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][3].ToString(), out test);
                    rpvalue = test.ToString();
                    ////rpvalue = this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName.ToString()].Value.ToString();
                    ////rpvalue = this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][3].ToString(); 
                    rplen = rpvalue.Length;
                    rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            //// this.StateAssessedGrid.Rows[i].Cells[4].Value = 0;
                            this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][3] = 0;
                        }

                        if (rppos == -1)
                        {
                            //// this.StateAssessedGrid.Rows[i].Cells[4].Value = 0;
                            this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][3] = 0;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][4].ToString().Trim()))
                {
                    int rppos;
                    string rpvalue;
                    int rplen;

                    rpvalue = this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][4].ToString();
                    rplen = rpvalue.Length;
                    rppos = rpvalue.IndexOf(".");
                    if (rplen > 15)
                    {
                        if (rppos > 15)
                        {
                            //// this.StateAssessedGrid.Rows[i].Cells[5].Value = 0;
                            this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][4] = 0;
                        }

                        if (rppos == -1)
                        {
                            //// this.StateAssessedGrid.Rows[i].Cells[5].Value = 0;
                            this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][4] = 0;
                        }
                    }
                }
            }

            if (this.StateAssessedGrid.Rows.Count.Equals(0))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = true;
                this.StateAssessedGrid.Focus();
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            this.StateAssessedGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.True;
            ////this.StateAssessedGrid.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.CellSelect;  

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.StateIDColumn.ColumnName].Hidden = true;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.StateItemIDColumn.ColumnName].Hidden = true;
            ////to fix bug #4361 -->khaja
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RollYearColumn.ColumnName].Hidden = true;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Hidden = true;

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Header.VisiblePosition = 0;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Header.VisiblePosition = 1;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Header.VisiblePosition = 2;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Header.VisiblePosition = 3;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Header.VisiblePosition = 4;

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Width = 150;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Width = 100;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Width = 150;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Width = 150;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Width = 150;

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Header.Caption = "Parcel Number";
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Header.Caption = "Tax District";
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Header.Caption = "Real Property";
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Header.Caption = "Personal Property";

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            ////this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].CellAppearance.ForeColor = Color.FromArgb(31, 65, 103);

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Format =  "$#,###0";//"$#,###0.00";
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Format = "$#,###0"; // "$#,###0.00";
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Format = "$#,###0"; // "$#,###0.00";

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].CellActivation = Activation.AllowEdit;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].CellActivation = Activation.AllowEdit;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].CellActivation = Activation.AllowEdit;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].CellActivation = Activation.NoEdit;

            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].MaxLength = 50;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].MaxLength = 20;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].MaxLength = 20;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
            this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;

            if (this.PermissionFiled.newPermission)
            {
               this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
                this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Width = 114;
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

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary
        /// Calculates the total.
        /// </summary>
        private void CalculateTotal()
        {
            decimal totalvalue = 0;
            decimal totalGridvalue = 0;
            decimal realtotalValue = 0;
            decimal realtotalGridValue = 0;
            decimal personaltotalValue = 0;
            decimal personaltotalGridValue = 0;
            for (int i = 0; i <= this.StateAssessedGrid.Rows.Count - 1; i++)
            {
                if (this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value != null && this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value != null)
                {
                    if ((!string.IsNullOrEmpty(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()) && ((!string.IsNullOrEmpty(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString())))))
                    {
                        /// totalvalue = totalvalue + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value) + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        totalGridvalue = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value) + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        realtotalGridValue = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        personaltotalGridValue = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value);
                        if (realtotalGridValue > 922337203685477)
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = 0;
                            MessageBox.Show("Total Value exceeds the maximum limit", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = realtotalGridValue;
                        }
                        if (personaltotalGridValue > 922337203685477)
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = 0;
                            MessageBox.Show("Total Value exceeds the maximum limit", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = personaltotalGridValue;
                        }
                        if (totalGridvalue > 922337203685477)
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = 0;
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = 0;
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = 0;
                            MessageBox.Show("Total Value exceeds the maximum limit", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ////return;
                        }
                        else
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = totalGridvalue;
                        }
                        realtotalValue = realtotalValue + +Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        personaltotalValue = personaltotalValue + +Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value);
                        totalvalue = totalvalue + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value);
                        ///this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value) + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                    }
                }
            }

            this.TotalValueTextBox.Text = totalvalue.ToString();
            this.totalSumValue = totalvalue; 
            this.RealPropertyTextBox.Text = realtotalValue.ToString();
            this.realTotalSumValue = realtotalValue;
            this.PersonalPropertyTextBox.Text = personaltotalValue.ToString();
            //this.perosonalTotalSumValue = Convert.ToDecimal(personaltotalValue.ToString());
            this.perosonalTotalSumValue = personaltotalValue;
            this.TotalValueLabel.Visible = true;
            this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
            this.changePosition(); 
        }

        /// <summary>
        /// Saves the assessed grid.
        /// </summary>
        private void SaveAssessedGrid()
        {
            try
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    string StateAssessedGrid = string.Empty;
                    StateAssessedGrid = TerraScanCommon.GetXmlString(this.StateAssessesOwnerDataSet.ListStateAssessedDetails);
                    int returnValue = this.form35076Control.WorkItem.F35076_SaveStateAssessedGrid(this.stateId, StateAssessedGrid, TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        private void SetSmartPartHeight()
        {
            ////Method added for set height for smarpart - Latha
            ////Bug #4748:Grid footer shouldnot get added when more records are added in state grid
            int childRowCount;
            int tempChildHeight;
            int gridHeight;

            if (this.StateAssessedGrid.Rows.Count > 0)
            {
                childRowCount = this.StateAssessedGrid.Rows.Count + 1;
                tempChildHeight = childRowCount * 22;
                gridHeight = tempChildHeight + (this.StateAssessedGrid.DisplayLayout.Bands[0].Header.Height + 18);
                this.MainPanel.Height = gridHeight + 5 - ((childRowCount * 2) + (childRowCount - 2));
                this.StateAssessedGrid.Height = gridHeight - (18 + (childRowCount * 2) + (childRowCount - 2));
                this.BottomPanel.Location = new Point(0, this.StateAssessedGrid.Height - 6);
                this.StateAssessedPictureBox.Height = gridHeight + 5 - ((childRowCount * 2) + (childRowCount - 2));
                this.Height = this.MainPanel.Height;
            }
            else
            {
                gridHeight = 68;
                this.MainPanel.Height = gridHeight;
                this.StateAssessedGrid.Height = gridHeight - 25;
                this.BottomPanel.Location = new Point(0, this.StateAssessedGrid.Height - 5);
                this.StateAssessedPictureBox.Height = gridHeight;
                this.Height = this.MainPanel.Height;
            }

            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = "D30075.F35076";
            sliceResize.SliceFormHeight = this.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.Height = sliceResize.SliceFormHeight;
            this.StateAssessedPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StateAssessedPictureBox.Height, this.StateAssessedPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        private void performCancel()
        {
            this.stategridheight = 20;
            this.staterowcount = 0;
            ////this.PermissionControlLock(this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            this.StateAssessesOwnerDataSet = this.form35076Control.WorkItem.F35075_GetStateAssessedOwnerDetails(Convert.ToInt32(this.stateId));
            this.StateAssessedGrid.DataSource = this.StateAssessesOwnerDataSet.ListStateAssessedDetails;
            if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count > 0)
            {
                this.CalculateTotal();
                this.MainPanel.Enabled = true;
            }
            else
            {
                this.TotalValueTextBox.Text = string.Empty;
                this.MainPanel.Enabled = false;
                //// this.MainPanel.Enabled = false;
                //// this.TotalValueLabel.ForeColor = Color.White;   
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            if (this.StateAssessedGrid.Rows.Count > 0)
            {
                this.SetSmartPartHeight();
            }
            else
            {
                if (this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows.Count > 0)
                {
                    int recordcount = Convert.ToInt32(this.StateAssessesOwnerDataSet.GetStateAssessedRecordCount.Rows[0]["StateAssessedCount"].ToString());
                    if (recordcount.Equals(0))
                    {
                        ////this.MainPanel.Enabled = false;
                    }
                    else
                    {
                        //// this.MainPanel.Enabled = true;
                    }
                    ////{
                    ////    this.MainPanel.Enabled = true;
                    ////}
                }
                else
                {
                    /// this.MainPanel.Enabled = false;
                }
                this.SetSmartPartHeight();
            }
            if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
            {
                this.PermissionControlLock(false);
            }
            else
            {
                this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            }

        }

        #region Permission


        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">
        /// true - to Set the control as not editable
        /// false - to Set the control as editable
        /// </param>
        private void PermissionControlLock(bool controlLook)
        {
            ////this.EnableControls(controlLook);
            ////Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
            ///this.MainPanel.Enabled = !controlLook;
            /// this.StateAssessedGrid.Enabled = !controlLook;
            ////this.StateAssessedGrid.Rows[0].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Activation = Activation.NoEdit;
            //if (this.permissionFields.editPermission)
            //{
            this.MainPanel.Enabled = true;
            this.StateAssessedGrid.Enabled = true;
            if (!controlLook)
            {
                this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
            }
            ///}
            else
            {
                // this.MainPanel.Enabled = true;
                //this.StateAssessedGrid.Enabled = true;
                this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
            }

            /// this.CellEditStatus(activeRow, controlLook);
            //}
            //else
            //{
            //    this.CellEditStatus(activeRow, controlLook);
            //    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            //    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            //}
        }


        /// <summary>
        /// Cells the edit status.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void CellEditStatus(Infragistics.Win.UltraWinGrid.UltraGridRow row, bool value)
        {
            try
            {
                if (value)
                {
                    //// Making column readonly false
                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;

                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //// row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                }
                else
                {
                    //// Making column readonly false
                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    //row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Permission

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            try
            {
                if (this.FormSlice_Resize != null)
                {
                    this.FormSlice_Resize(this, eventArgs);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the InitializeLayout event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
               //// this.StateAssessedGrid.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.CellSelect;  
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;

                ////activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Value = "<<< >>>";

                if (activeCell != null && !string.IsNullOrEmpty(activeCell.Text.ToString()))
                {
                    this.SetEditRecord();
                }
                ////}
                // //this.StateAssessedGrid.UpdateData(); 
                //// this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();

                //if (this.StateAssessedGrid.Rows.Count == 0)
                //{
                //    this.SetSmartPartHeight();
                //}

                //if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                //{
                //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                //}

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDataError event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellDataErrorEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_CellDataError(object sender, CellDataErrorEventArgs e)
        {
            e.RestoreOriginalValue = true;
            e.StayInEditMode = true;
        }

        /// <summary>
        /// Handles the Error event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.ErrorEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_Error(object sender, ErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the BeforeCellDeactivate event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
            //activeRow.Cells[
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Format = "$#,###0";//"$#,###0.00"; 
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Format = "$#,###0"; //"$#,###0.00";  
           // activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].SelText.  = "$#,###0";
            if (activeRow != null && (activeRow.Index != -1))
            {
                if (activeCell != null && activeCell.Column != null && activeCell.Column.ToString() != "DistrictID" && activeCell.Column.ToString() != "ParcelNumber")
                {
                    if (activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value != null && activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value != null)
                    {
                        if ((!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString()) && (string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()))))
                        {
                            activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value;
                        }

                        if ((string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString()) && (!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()))))
                        {
                            activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value;
                        }

                        if (!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString()))
                        {
                            int rppos;
                            string rpvalue;
                            int rplen;
                            
                            rpvalue = activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString();
                            //if (rpvalue.Contains("."))
                            //{
                            //    string[] strArray = rpvalue.Split('.');
                            //    rpvalue = strArray[0];
                            //}
                            //activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = rpvalue;
                            //rpvalue = activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString();
                            rplen = rpvalue.Replace(",", "").Replace(".", "").Length;
                            rppos = rpvalue.IndexOf(".");
                            if (rplen > 15)
                            {
                                if (rppos > 15)
                                {
                                    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = 0;
                                }

                                if (rppos == -1)
                                {
                                    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = 0;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()))
                        {
                            string ppvalue;
                            int pppos;
                            int pplen;


                            ppvalue = activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString();
                            //if(ppvalue.Contains("."))
                            //{
                            //    string[] strArray = ppvalue.Split('.');
                            //    ppvalue = strArray[0]; 
                            //}
                            pplen = ppvalue.Length;
                            pppos = ppvalue.IndexOf(".");
                            if (pplen > 15)
                            {
                                if (pppos > 15)
                                {
                                    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = 0;
                                }

                                if (pppos == -1)
                                {
                                    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = 0;
                                }
                            }
                        }

                        ////if ((!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString()) || (!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()))))
                        ////{
                        ////    decimal rp = 0;
                        ////    decimal pp = 0;
                        ////    rp = Convert.ToDecimal(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        ////    pp = Convert.ToDecimal(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value);
                        ////    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = rp + pp;
                        ////}

                        this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                    }

                    //if (activeCell.Column.Index != 4 && activeCell.Column.Index != 6)
                    //if (activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value != null && activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value != null)
                    //{
                    if (activeCell.Column.ToString() == this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName.ToString() || activeCell.Column.ToString() == this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName.ToString())
                    {
                        //if ((!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString()) && (!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()))))
                        //{
                        decimal rp = 0;
                        decimal pp = 0;

                        if (activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value != null)
                        {
                            if (!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString()))
                            {
                                rp = Convert.ToDecimal(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                            }
                        }
                        if (activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value != null)
                        {
                            if (!string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()) && activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value != null)
                            {
                                pp = Convert.ToDecimal(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value);
                            }
                        }
                        activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = rp + pp;
                        //}
                    }


                    this.CalculateTotal();
                    this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                    ////}
                }
            }

            ////this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Format = "$#,###0.00";
        }

        /// <summary>
        /// Handles the Click event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_Click(object sender, EventArgs e)
        {
            //Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
            //Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
            
            

            //this.StateAssessedGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
            //this.StateAssessedGrid.DisplayLayout.Override.AllowColMoving = AllowColMoving.WithinBand;
            //this.StateAssessedGrid.DisplayLayout.Override.AllowColSwapping = AllowColSwapping.NotAllowed; 
            
            //this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.ParcelNumberColumn.ColumnName].Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.WithinBand;
            //string districtid;

            //if (activeRow != null)
            //{
            //    if (activeCell != null)
            //    {
            //        if (activeCell.Column.Index.Equals(6))
            //        {
            //            if (activeCell.Value.ToString() != null)
            //            {
            //                if (!string.IsNullOrEmpty(activeCell.Value.ToString()))
            //                {
            //                    DialogResult districtDialog;
            //                    Form districtForm = new Form();
            //                    ////to fix bug #4361 -->khaja
            //                    object[] optionalParameter = new object[] { activeRow.Cells["RollYear"].Value.ToString() }; // activeCell.Text.ToString() };
            //                    districtForm = TerraScanCommon.GetForm(1512, optionalParameter, this.form35076Control.WorkItem);
            //                    if (districtForm != null)
            //                    {
            //                        districtDialog = districtForm.ShowDialog();
            //                        if (districtDialog == DialogResult.OK)
            //                        {
            //                            districtid = TerraScanCommon.GetValue(districtForm, "DistrictId");
            //                            if (!string.IsNullOrEmpty(districtid))
            //                            {
            //                                activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Value = districtid;
            //                                this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
            //                            }

            //                            this.SetEditRecord();
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        else if (activeCell.Column.Index.Equals(5))
            //        {
            //            this.stateItemId = activeCell.Text.ToString();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Handles the KeyDown event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_KeyDown(object sender, KeyEventArgs e)             
        {
            try
            {
                    Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
                    Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;


                    //activeRow.Activate();
                    //activeRow.Selected = true;
                    //if (e.KeyValue.Equals(38))
                    //{
                    //    this.id1 = this.StateAssessedGrid.ActiveRow.Index - 1;
                    //}
                    //if (e.KeyValue.Equals(40))
                    //{
                    //    this.id1 = this.StateAssessedGrid.ActiveRow.Index + 1;
                    //}

                    if (e.KeyValue.Equals(27))
                    {
                        this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                    }
                    ////Added by Biju on 07/Jan/2010 to fix #540
                    else if (e.KeyValue.Equals(9) && !e.Shift && activeCell != null && this.StateAssessesOwnerDataSet.Tables.Count >= 2 && activeCell.Column.Index.Equals(this.StateAssessedGrid.DisplayLayout.Bands[0].Columns["Total"].Index) && activeRow.Index.Equals(this.StateAssessesOwnerDataSet.Tables[1].Rows.Count))
                    {
                        this.ParentForm.Controls[0].Controls["HelplinkLabel"].Focus();
                    }
                    ////till here
                    string districtid, districtName;

                    if (activeCell != null)
                    {
                        if (activeCell.Text != null)
                        {
                            if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                            {
                                if (e.KeyValue == 46 && (activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName.ToString() && activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName.ToString()))
                                {
                                    this.SetEditRecord();
                                }
                                else if (e.KeyValue == 13)
                                {
                                    if (activeCell.Column.Index.Equals(8))
                                    {
                                        if (activeCell.Value.ToString() != null && !string.IsNullOrEmpty(activeCell.Value.ToString()) && this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False)
                                        {
                                            DialogResult districtDialog;
                                            Form districtForm = new Form();
                                            ////to fix bug #4361 -->Ramya.D
                                            object[] optionalParameter;
                                            if (this.form35076Control.WorkItem.RootWorkItem.State["RollYear"] != null)
                                            {
                                                optionalParameter = new object[] { this.form35076Control.WorkItem.RootWorkItem.State["RollYear"].ToString(), this.ParentFormId }; // activeCell.Text.ToString() };
                                            }
                                            else
                                            {
                                                optionalParameter = new object[] { };
                                            }
                                            ////object[] optionalParameter = new object[] { TerraScanCommon.GetRollYear }; // activeCell.Text.ToString() };
                                            districtForm = TerraScanCommon.GetForm(1512, optionalParameter, this.form35076Control.WorkItem);
                                            if (districtForm != null)
                                            {
                                                districtDialog = districtForm.ShowDialog();
                                                if (districtDialog == DialogResult.OK)
                                                {
                                                    districtid = TerraScanCommon.GetValue(districtForm, "CommandResult");
                                                    districtName = TerraScanCommon.GetValue(districtForm, "CommandValue");
                                                    if (!string.IsNullOrEmpty(districtid))
                                                    {
                                                        activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Value = districtid;
                                                        activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Value = districtName;
                                                        this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                                                    }

                                                    this.SetEditRecord();
                                                }
                                                else
                                                {

                                                }

                                            }
                                            //this.StateAssessedGrid.KeyUp += new KeyEventHandler(this.StateAssessedGrid_KeyUp);
                                        }
                                    }
                                }
                                ////else if (e.KeyValue == 9 && (activeCell.Column.ToString() == "RealProperty"|| activeCell.Column.ToString() == "PersonalProperty"))
                                ////{
                                ////    decimal personalproperty=0;
                                ////    decimal realProperty=0;
                                ////     decimal.TryParse(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString(),out personalproperty);
                                ////     decimal.TryParse(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString(), out realProperty);
                                ////     activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = personalproperty;
                                ////     activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = realProperty;

                                ////}
                            }
                        }
                    }

                    ////this.StateAssessedGrid.AutoScrollOffset = new Point(0, 0);
                    ////Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
                    ////Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
                    ////if (this.StateAssessedGrid.ActiveCell != null)
                    ////{
                    ////    if (activeCell.Text != null)
                    ////    {
                    ////        if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.ToString()))
                    ////        {
                    ////            //// this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    ////            if (e.KeyValue == 88)
                    ////            {
                    ////                this.SetEditRecord();
                    ////            }
                    ////        }
                    ////    }
                    ////}
                    ////int i = 0;
                    ////i = this.StateAssessedGrid.Selected.Rows.Count;
                
            }

            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// Handles the BeforeRowDeactivate event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
        {
            ////if (this.StateAssessedGrid.Rows.Count > 0)
            ////{
            ////    int gridHeight = 0;
            ////    int formHeight = 0;

            ////    gridHeight = (20 * this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count) + this.StateAssessedGrid.DisplayLayout.Bands[0].Header.Height + this.StateAssessedGrid.Rows.TemplateAddRow.Height + 0;
            ////    this.StateAssessedGrid.Height = gridHeight - 20;

            ////    formHeight = this.StateAssessedGrid.Height + this.BottomPanel.Height + 2;
            ////    this.MainPanel.Height = formHeight;
            ////    this.BottomPanel.Location = new Point(0, this.StateAssessedGrid.Height);
            ////    this.StateAssessedPictureBox.Height = formHeight - 2;
            ////    this.Height = formHeight;

            ////    SliceResize sliceResize;
            ////    sliceResize.MasterFormNo = this.masterFormNo;
            ////    sliceResize.SliceFormName = "D30075.F35076";
            ////    sliceResize.SliceFormHeight = this.Height;
            ////    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            ////    this.StateAssessedPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StateAssessedPictureBox.Height, this.StateAssessedPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            ////}
            ////else
            ////{
            ////    int formHeight = 0;
            ////    this.StateAssessedGrid.Height = 45;

            ////    formHeight = 50 + this.BottomPanel.Height + 2;
            ////    this.MainPanel.Height = formHeight;
            ////    this.BottomPanel.Location = new Point(0, this.StateAssessedGrid.Height);
            ////    this.StateAssessedPictureBox.Height = formHeight - 7;
            ////    this.Height = formHeight;

            ////    SliceResize sliceResize;
            ////    sliceResize.MasterFormNo = this.masterFormNo;
            ////    sliceResize.SliceFormName = "D20050.F35050";
            ////    sliceResize.SliceFormHeight = this.Height;
            ////    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            ////    this.StateAssessedPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StateAssessedPictureBox.Height, this.StateAssessedPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            ////}
        }

        /// <summary>
        /// Handles the InitializeTemplateAddRow event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
        {
            if (this.StateAssessedGrid.Rows.Count > 0)
            {
                // this.MainPanel.Enabled = true;
                /*int gridHeight = 0;
                int formHeight = 0;

                gridHeight = (20 * this.StateAssessedGrid.Rows.Count) + this.StateAssessedGrid.DisplayLayout.Bands[0].Header.Height + this.StateAssessedGrid.Rows.TemplateAddRow.Height;
                if (this.StateAssessedGrid.Rows.Count == 1 && !this.formloadflag)
                {
                    this.StateAssessedGrid.Height = gridHeight + 20;
                    this.formloadflag = true;
                }
                else
                {
                    this.StateAssessedGrid.Height = gridHeight - this.stategridheight;
                    this.staterowcount = this.staterowcount + 1;
                }

                if (this.staterowcount >= 5)
                {
                    this.stategridheight = this.stategridheight + 5;
                    this.staterowcount = 0;
                }

              formHeight = this.StateAssessedGrid.Height + this.BottomPanel.Height + this.StateAssessedGrid.Rows.TemplateAddRow.Height;
                this.MainPanel.Height = formHeight;
                this.BottomPanel.Location = new Point(0, this.StateAssessedGrid.Height);
                this.StateAssessedPictureBox.Height = this.MainPanel.Height;////formHeight - 2;
                this.Height = formHeight;*/


                ////this.BottomPanel.BringToFront();

                /*if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count > 0)
                {
                    for (int i = 0; i <= this.StateAssessedGrid.Rows.Count - 1; i++)
                    {
                        if (this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Value != null)
                        {
                            if (string.IsNullOrEmpty(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Value.ToString()))
                            {
                                this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[i][this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName] = "<<< >>>";
                            }
                        }
                    }
                }

                this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();*/

                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
                if (activeRow != null && activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Value != null && string.IsNullOrEmpty(activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Value.ToString()))
                {
                    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Value = "<<< >>>";
                }

                ////Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;

                if (activeRow != null)
                {
                    this.SetSmartPartHeight();

                    ////Set the scroll position while adding new row
                    if (this.ParentForm != null && this.ParentForm.Controls[0] != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        this.yaxisPoint = this.yaxisPoint + 25;
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                    }
                    ////Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;

                    if (activeCell != null)
                    {
                        activeCell.Activate();
                        this.StateAssessedGrid.PerformAction(UltraGridAction.EnterEditMode);
                        try
                        {
                            if (activeCell.IsInEditMode == true && activeCell.SelText.Length > 0)
                            {
                                activeCell.SelStart = activeCell.SelText.Length;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

            }
            else
            {
                //// this.MainPanel.Enabled = true;

                this.SetSmartPartHeight();

                this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();

                ////Set scroll position
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    this.yaxisPoint = this.yaxisPoint + 25;
                    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }
                //// this.StateAssessedGrid.Rows[0].Cells[6].Value = "<<< >>>";
            }
        }

        /// <summary>
        /// Handles the Click event of the TotalValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TotalValueTextBox_Click(object sender, EventArgs e)
        {
            this.BottomPanel.BackColor = Color.FromArgb(31, 65, 103);
            this.TotalValueTextBox.BackColor = Color.FromArgb(31, 65, 103);
        }

        /// <summary>
        /// Handles the Enter event of the TotalValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TotalValueTextBox_Enter(object sender, EventArgs e)
        {
            this.BottomPanel.BackColor = Color.FromArgb(31, 65, 103);
            this.TotalValueTextBox.BackColor = Color.FromArgb(31, 65, 103);
        }

        /// <summary>
        /// Handles the KeyUp event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StateAssessedGrid_KeyUp(object sender, KeyEventArgs e)
        {

            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
            //this.id1 = this.StateAssessedGrid.ActiveRow.Index;
            //activeRow.Activate();
            //activeRow.Selected = true;
            if (this.StateAssessedGrid.ActiveCell != null)
            {
                if (activeCell.Text != null)
                {
                    if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                    {
                        if (activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName.ToString() && activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName.ToString())
                        {

                            if ((e.KeyValue == 88) || (e.KeyValue == 32))//// && (activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName.ToString() && activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName.ToString()))
                            {
                                this.SetEditRecord();
                            }

                            if (e.KeyValue == 8)////&& (activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName.ToString() && activeCell.Column.Key.ToString() != this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName.ToString()))
                            {
                                this.SetEditRecord();
                            }


                        }
                    }
                }
            }


            if (activeRow != null && e.KeyCode == Keys.Delete && this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.deletePermission && this.StateAssessedGrid.ActiveCell == null)
            {
                int stateItemvalue = 0;

                if (this.StateAssessedGrid.ActiveRow != null)
                {
                    if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count > this.StateAssessedGrid.ActiveRow.Index)
                    {
                        if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[this.StateAssessedGrid.ActiveRow.Index][this.StateAssessesOwnerDataSet.ListStateAssessedDetails.StateItemIDColumn.ColumnName].ToString() == "-99")
                        {
                            this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.RemoveAt(this.StateAssessedGrid.ActiveRow.Index);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[this.StateAssessedGrid.ActiveRow.Index][this.StateAssessesOwnerDataSet.ListStateAssessedDetails.StateItemIDColumn.ColumnName].ToString()))
                            {
                                int.TryParse(this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows[this.StateAssessedGrid.ActiveRow.Index][this.StateAssessesOwnerDataSet.ListStateAssessedDetails.StateItemIDColumn.ColumnName].ToString(), out stateItemvalue);
                                int isdeleted = 0;
                                isdeleted = this.form35076Control.WorkItem.F35076_DeleteStateAssessedDetails(stateItemvalue, TerraScanCommon.UserId);
                                this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.RemoveAt(this.StateAssessedGrid.ActiveRow.Index);
                            }
                        }
                    }
                }

                this.StateAssessesOwnerDataSet = this.form35076Control.WorkItem.F35075_GetStateAssessedOwnerDetails(Convert.ToInt32(this.stateId));
                this.StateAssessedGrid.DataSource = this.StateAssessesOwnerDataSet.ListStateAssessedDetails;
                if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count > 0)
                {
                    this.CalculateTotal();
                }
                else
                {
                    this.TotalValueTextBox.Text = string.Empty;
                }

                this.SetSmartPartHeight();

                this.CustomizeGrid();

            }
            //if (e.KeyCode == Keys.Escape)
            //{
            //    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //    this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
            //    this.performCancel();
            //}

            //if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            //{
            //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            //}
        }

        #endregion

        ////To get the scroll position
        #region scroll events

        /// <summary>
        /// Handles the Click event of the Scroll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void Scroll_Click(object sender, ScrollEventArgs e)
        {
            try
            {
                if (e.NewValue > 5)
                {
                    this.yaxisPoint = e.NewValue;
                }
                else
                {
                    this.yaxisPoint = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Scroll event of the Smartpart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Smartpart_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion scroll events
        private void F35076_Resize(object sender, EventArgs e)
        {
            //if (this.StateAssessesOwnerDataSet.ListStateAssessedDetails.Rows.Count > 0)
            //{
            // this.Height = this.MainPanel.Height;
            //}

        }

        private void StateAssessedGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void StateAssessedGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                this.basePanelScrolled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void StateAssessedGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                UltraGrid grid = sender as UltraGrid;
                UIElement controlElement = grid.DisplayLayout.UIElement;
                UIElement elementAtPoint = controlElement != null ? controlElement.ElementFromPoint(e.Location) : null;
                UltraGridColumn column = null;
                while (elementAtPoint != null)
                {
                    HeaderUIElement headerElement = elementAtPoint as HeaderUIElement;
                    if (headerElement != null &&
                         headerElement.Header is Infragistics.Win.UltraWinGrid.ColumnHeader)
                    {
                        column = headerElement.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;


                        break;
                    }

                    elementAtPoint = elementAtPoint.Parent;
                }
               Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
                
                string districtid, districtName;
               
                if (activeRow != null)
                {
                    if (activeCell != null)
                    {
                        //if (activeCell.Column.Index.Equals(8) && e.X >= 170 && e.X <= 268 && (this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False))
                        if (activeCell.Column.Index.Equals(8) && (this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False))
                        {
                            if (activeCell.Value.ToString() != null)
                            {
                                if (!string.IsNullOrEmpty(activeCell.Value.ToString()))
                                {
                                  
                                    DialogResult districtDialog;
                                    Form districtForm = new Form();
                                    ////to fix bug #4361 -->Ramya.D
                                    object[] optionalParameter;
                                    if (this.form35076Control.WorkItem.RootWorkItem.State["RollYear"] != null)
                                    {
                                        optionalParameter = new object[] { this.form35076Control.WorkItem.RootWorkItem.State["RollYear"].ToString(), this.ParentFormId }; // activeCell.Text.ToString() };
                                    }
                                    else
                                    {
                                        optionalParameter = new object[] { };
                                    }
                                    // o///bject[] optionalParameter = new object[] {  }; // activeCell.Text.ToString() };

                                    districtForm = TerraScanCommon.GetForm(1512, optionalParameter, this.form35076Control.WorkItem);
                                    if (districtForm != null)
                                    {
                                        districtDialog = districtForm.ShowDialog();
                                        if (districtDialog == DialogResult.OK)
                                        {
                                            ////Coding added for the issue : 4387 on 29/10/2009 by mallliga
                                            districtid = TerraScanCommon.GetValue(districtForm, "CommandResult");
                                            districtName = TerraScanCommon.GetValue(districtForm, "CommandValue");
                                            string strMessage = string.Empty;
                                            if (this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows.Count > 0 && !string.IsNullOrEmpty(districtid))
                                            {
                                                 strMessage = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.UserWarningColumn.ColumnName].ToString();
                                            }
                                                if (string.IsNullOrEmpty(strMessage))
                                                {
                                                    if (!string.IsNullOrEmpty(districtid))
                                                    {
                                                        ////Coding added for the issue : 4387 on 29/10/2009 by mallliga
                                                        activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Value = districtid;
                                                        activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Value = districtName;
                                                        this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                                                    }
                                                }
                                                else
                                                {
                                                    DialogResult ds = TerraScanMessageBox.Show(strMessage, "Terrascan T2 - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    if (ds.Equals(DialogResult.OK))
                                                    {
                                                        // this.DialogResult = DialogResult.No;

                                                    }
                                                }
                                                this.SetEditRecord();
                                            
                                            //if (!string.IsNullOrEmpty(districtid))
                                            //{
                                            //    ////Coding added for the issue : 4387 on 29/10/2009 by mallliga
                                            //    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictIDColumn.ColumnName].Value = districtid;
                                            //    activeRow.Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.DistrictColumn.ColumnName].Value = districtName;
                                            //    this.StateAssessesOwnerDataSet.ListStateAssessedDetails.AcceptChanges();
                                            //}
                                            //this.SetEditRecord();

                                        }
                                    }
                                }
                            }

                        }
                        else if (activeCell.Column.Index.Equals(5))
                        {
                            this.stateItemId = activeCell.Text.ToString();
                        }
                    }

                    if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void F35076_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > 5)
            {
                this.yaxisPoint = e.NewValue;
            }
            else
            {
                this.yaxisPoint = 0;
            }
        }

        private void StateAssessedGrid_BeforeCellActivate(object sender, CancelableCellEventArgs e)
        {
            //if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            //{
            //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            //}
        }

        private void StateAssessedGrid_TextChanged(object sender, EventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;
            if (activeCell.Text != null)
            {
                if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                {
                    this.SetEditRecord();
                }
            }
            //if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            //{
            //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            //}
        }

        private void StateAssessedGrid_AfterCellActivate(object sender, EventArgs e)
        {
            //// this.CustomizeGrid();
            //if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            //{
            //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            //}
        }
        /// <summary>
        /// Handles the KeyUp event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void RealPropertyTextBox_Enter(object sender, EventArgs e)
        {
            this.BottomPanel.BackColor = Color.FromArgb(31, 65, 103);
            this.RealPropertyTextBox.BackColor = Color.FromArgb(31, 65, 103);
        }
        /// <summary>
        /// Handles the Click event of the TotalValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RealPropertyTextBox_Click(object sender, EventArgs e)
        {
            this.BottomPanel.BackColor = Color.FromArgb(31, 65, 103);
            this.RealPropertyTextBox.BackColor = Color.FromArgb(31, 65, 103);
        }
        /// <summary>
        /// Handles the Click event of the TotalValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PersonalPropertyTextBox_Click(object sender, EventArgs e)
        {
            this.BottomPanel.BackColor = Color.FromArgb(31, 65, 103);
            this.PersonalPropertyTextBox.BackColor = Color.FromArgb(31, 65, 103);
        }
        /// <summary>
        /// Handles the KeyUp event of the StateAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PersonalPropertyTextBox_Enter(object sender, EventArgs e)
        {
            this.BottomPanel.BackColor = Color.FromArgb(31, 65, 103);
            this.PersonalPropertyTextBox.BackColor = Color.FromArgb(31, 65, 103);
        }

        private void StateAssessedGrid_BeforeColPosChanged(object sender, BeforeColPosChangedEventArgs e)
        {
            if (e.ColumnHeaders[0].VisiblePosition.Equals(0))
            {
                if (this.columnName != ("ParcelNumber") && this.columnName != ("District"))
                {
                    e.Cancel = true;
                }

            }
            if (e.ColumnHeaders[0].VisiblePosition.Equals(1))
            {
                if (this.columnName != ("District") && this.columnName != ("ParcelNumber"))
                {
                    e.Cancel = true;
                }

            }
            if (e.ColumnHeaders[0].VisiblePosition >= 2)
            {
                if (this.columnName.Equals("District") )
                {
                    e.Cancel = true;
                }
                if (this.columnName.Equals("ParcelNumber"))
                {
                    e.Cancel = true; 
                }
            }
            //if (this.columnName.Equals("ParcelNumber"))
            //{
            //    e.Cancel = true;
            //}
            //if (this.columnName.Equals("District"))
            //{
            //    e.Cancel = true;
            //}
           

        }

        private void StateAssessedGrid_MouseDown(object sender, MouseEventArgs e)
        {
            Infragistics.Win.UIElement aUIElement = this.StateAssessedGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
            UltraGridColumn bCol;
            bCol = (UltraGridColumn)aUIElement.GetContext(typeof(UltraGridColumn));
            if (bCol != null)
            {
                this.columnindex = bCol.Index;
                this.columnName = bCol.Key;
            }

        }

        private void changePosition()
        {
            int first;
            int second;
            int third;
            first = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[3].Header.VisiblePosition;
            second = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[4].Header.VisiblePosition;
            third = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[5].Header.VisiblePosition;
            if(first.Equals(3))
            {
                this.RealPropertyTextBox.Text = this.realTotalSumValue.ToString();
            }
            else if (first.Equals(2))
            {
                this.PersonalPropertyTextBox.Text = this.realTotalSumValue.ToString();
            }
            //if (first.Equals(4))
            else
            {
                this.TotalValueTextBox.Text = this.realTotalSumValue.ToString();
            }
            if (second.Equals(2))
            {
                this.PersonalPropertyTextBox.Text = this.perosonalTotalSumValue.ToString();
            }
            else if (second.Equals(3))
            {
                this.RealPropertyTextBox.Text = this.perosonalTotalSumValue.ToString();
            }
            else
            {
                this.TotalValueTextBox.Text = this.perosonalTotalSumValue.ToString();
            }
            if (third.Equals(2))
            {
                this.PersonalPropertyTextBox.Text = this.totalSumValue.ToString();
            }
            else if (third.Equals(3))
            {
                this.RealPropertyTextBox.Text = this.totalSumValue.ToString();
            }
            else
            {
                this.TotalValueTextBox.Text = this.totalSumValue.ToString();
            }


            //if ((first.Equals(3)) && (second.Equals(2)) && (third.Equals(4)))
            //{
            //    this.PersonalPropertyTextBox.Text = this.perosonalTotalSumValue.ToString();
            //    this.RealPropertyTextBox.Text = this.realTotalSumValue.ToString();
            //    this.TotalValueTextBox.Text = this.totalSumValue.ToString();
            //}
            //else
            //    if ((first.Equals(4)) && (second.Equals(2)) && (third.Equals(3)))
            //    {
            //        this.PersonalPropertyTextBox.Text = this.perosonalTotalSumValue.ToString();
            //        this.RealPropertyTextBox.Text = this.totalSumValue.ToString();
            //        this.TotalValueTextBox.Text = this.realTotalSumValue.ToString();
            //    }
            //    else
            //        if ((first.Equals(2)) && (second.Equals(3)) && (third.Equals(4)))
            //        {
            //            this.PersonalPropertyTextBox.Text = this.realTotalSumValue.ToString();
            //            this.RealPropertyTextBox.Text = this.perosonalTotalSumValue.ToString();
            //            this.TotalValueTextBox.Text = this.totalSumValue.ToString();
            //        }
            //        else
            //            if ((first.Equals(2)) && (second.Equals(4)) && (third.Equals(3)))
            //            {
            //                this.PersonalPropertyTextBox.Text = this.realTotalSumValue.ToString();
            //                this.RealPropertyTextBox.Text = this.totalSumValue.ToString();
            //                this.TotalValueTextBox.Text = this.perosonalTotalSumValue.ToString();
            //            }
            //            else
            //                if ((first.Equals(3)) && (second.Equals(4)) && (third.Equals(2)))
            //                {
            //                    this.PersonalPropertyTextBox.Text = this.totalSumValue.ToString();
            //                    this.RealPropertyTextBox.Text = this.realTotalSumValue.ToString();
            //                    this.TotalValueTextBox.Text = this.perosonalTotalSumValue.ToString();
            //                }
            //                else
            //                    if ((first.Equals(3)) && (second.Equals(4)) && (third.Equals(2)))
            //                    {
            //                        this.PersonalPropertyTextBox.Text = this.totalSumValue.ToString();
            //                        this.RealPropertyTextBox.Text = this.realTotalSumValue.ToString();
            //                        this.TotalValueTextBox.Text = this.perosonalTotalSumValue.ToString();
            //                    }
            //                    else
            //                        if ((first.Equals(4)) && (second.Equals(3)) && (third.Equals(2)))
            //                        {
            //                            this.PersonalPropertyTextBox.Text = this.totalSumValue.ToString();
            //                            this.RealPropertyTextBox.Text = this.perosonalTotalSumValue.ToString();
            //                            this.TotalValueTextBox.Text = this.realTotalSumValue.ToString();
            //                        }
        }

        private void StateAssessedGrid_AfterColPosChanged(object sender, AfterColPosChangedEventArgs e)
        {

            decimal totalvalue = 0;
            decimal totalGridvalue = 0;
            decimal realtotalValue = 0;
            decimal realtotalGridValue = 0;
            decimal personaltotalValue = 0;
            decimal personaltotalGridValue = 0;
            for (int i = 0; i <= this.StateAssessedGrid.Rows.Count - 1; i++)
            {
                if (this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value != null && this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value != null)
                {
                    if ((!string.IsNullOrEmpty(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value.ToString()) && ((!string.IsNullOrEmpty(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value.ToString())))))
                    {
                        /// totalvalue = totalvalue + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value) + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        totalGridvalue = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value) + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        realtotalGridValue = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        personaltotalGridValue = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value);
                        if (realtotalGridValue > 922337203685477)
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = 0;
                            MessageBox.Show("Total Value exceeds the maximum limit", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = realtotalGridValue;
                        }
                        if (personaltotalGridValue > 922337203685477)
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = 0;
                            MessageBox.Show("Total Value exceeds the maximum limit", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = personaltotalGridValue;
                        }
                        if (totalGridvalue > 922337203685477)
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = 0;
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value = 0;
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value = 0;
                            MessageBox.Show("Total Value exceeds the maximum limit", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ////return;
                        }
                        else
                        {
                            this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = totalGridvalue;
                        }
                        realtotalValue = realtotalValue + +Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                        personaltotalValue = personaltotalValue + +Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value);
                        totalvalue = totalvalue + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value);
                        ///this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.TotalColumn.ColumnName].Value = Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.PersonalPropertyColumn.ColumnName].Value) + Convert.ToDecimal(this.StateAssessedGrid.Rows[i].Cells[this.StateAssessesOwnerDataSet.ListStateAssessedDetails.RealPropertyColumn.ColumnName].Value);
                    }
                }
            }
            this.changePosition();
            if (this.flagLoadForm)
            {
                col1pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[1].Header.VisiblePosition;
                col2pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[2].Header.VisiblePosition;
                col3Pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[3].Header.VisiblePosition;
                col4pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[4].Header.VisiblePosition;
                col5pos = this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[5].Header.VisiblePosition;
            }
            //else
            //{
            //    col3Pos = 0;
            //    col4pos = 0;
            ////    col5pos = 0;
            //}
            

        }


         public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            UltraGridRow row = drawParams.Element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
            UltraGridColumn col = drawParams.Element.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;

            if (row != null && row.Selected)
            {
                drawParams = CustomRowAppearance(drawParams);
            }
            if (row != null && row.Activated)
            {
                drawParams = CustomRowAppearance(drawParams);
            }
            return false;
        }


            private static UIElementDrawParams CustomRowAppearance(UIElementDrawParams drawParams)
            {
                drawParams.AppearanceData.ForeColor = Color.White;

                drawParams.AppearanceData.BackColor = Color.Black;
                return drawParams;
            }

            public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
            {
                if (drawParams.Element is Infragistics.Win.FormattedLinkLabel.TextSectionUIElement)
                {
                    return DrawPhase.BeforeDrawForeground;
                }

                return DrawPhase.None;

            }
                  

       
             
        
    }
}

