//--------------------------------------------------------------------------------------------
// <copyright file="F29510.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17 Sep 07		D.LathaMaheswari    Created
//*********************************************************************************/

namespace D24500
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using Infragistics.Win.UltraWinGrid;
    using System.Xml;

    /// <summary>
    /// F29500
    /// </summary>
    public partial class F29510 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// form295108Control Controller
        /// </summary>
        private F29510Controller form29510Control;

        /// <summary>
        /// Unique keyId from the Form Master - parcel id
        /// </summary>
        private int keyId;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Set Default OpenPermission for Form 2000
        /// </summary>
        ////private bool form1410penPermission = false;
        private bool form1410penPermission;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        ///  Used To store Parcel Details
        /// </summary>
        ////private DataSet parcelData = new DataSet();
        private F29510ParcelCombineData parcelData;

        /// <summary>
        ///  Used To strore Valid  DataSet Or Not
        /// </summary>
        private bool validDataSet;

        /// <summary>
        /// Used to Hold the  Record Count
        /// </summary>
        private int parcelRowCount;

        /// <summary>
        /// parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// To Store XML String 
        /// </summary>
        private string combineParcel = string.Empty;

        /// <summary>
        /// Used to store CombinedID
        /// </summary>
        private int combinedId;

        /// <summary>
        /// Used to store ParcelNumber
        /// </summary>
        private string parcelNo;

        /// <summary>
        /// Used to store IsProcessed
        /// </summary>
        private Boolean hasProcessed;

        /// <summary>
        /// redColorCode
        /// </summary>
        private int redColorCode;

        /// <summary>
        /// greenColorCode
        /// </summary>
        private int greenColorCode;

        /// <summary>
        /// blueColorCode
        /// </summary>
        private int blueColorCode;

        /// <summary>
        /// To hold tabText
        /// </summary>
        private string tabText;

        /// <summary>
        /// To Store RollYear
        /// </summary>
        private int rolYear;

        /// <summary>
        /// To find duplicate record
        /// </summary>
        private Boolean hasAdded;

        /// <summary>
        /// Store ParcelNumber
        /// </summary>
        private string tempParcelID;

        bool IsAttachment = false;
        bool IsComment = false;
        bool IsPermit = false;
        bool IsAssociation = false;
        bool IsNewConstruction = false;

        #endregion

        #region Construtor

        /// <summary>
        /// F29510
        /// </summary>
        public F29510()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15018"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29510(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.tabText = tabText;
            this.redColorCode = red;
            this.greenColorCode = green;
            this.blueColorCode = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.CombinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CombinePictureBox.Height, this.CombinePictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F29510 control.
        /// </summary>
        /// <value>The F29510 control.</value>
        [CreateNew]
        public F29510Controller Form29510Control
        {
            get { return this.form29510Control as F29510Controller; }
            set { this.form29510Control = value; }
        }

        #endregion

        #region Event Subscriptions

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
                }

                if (this.parcelData.Tables.Count > 0 && this.parcelData.f29510ListParcel.Rows.Count > 0)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                    this.SetButtonVisibility();
                    this.ParcelNumberTextBox.Focus();
                    TerraScanCommon.SetDataGridViewPosition(this.CombineParcelGridView, 0);
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
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////Reload Details
            this.GetParcelDetails();
            ////Set Button visibility
            this.SetButtonVisibility();
            ////Set the smartpart height based on the number of rows
            this.SetFormHeight(this.parcelRowCount);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission == true)
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors()));
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors()));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {
                this.SaveCombineParcel();
                this.GetParcelDetails();
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
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.GetParcelDetails();
                    this.SetButtonVisibility();
                    this.SetFormHeight(this.parcelRowCount);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
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

        #endregion EventSubscription

        #region Events

        /// <summary>
        /// Select Parcel to combine 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void AddParcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CombineParcelGridView.OriginalRowCount > 0)
                {
                    SupportFormData.GetFormDetailsDataTable getForm1410ParcelData = new SupportFormData.GetFormDetailsDataTable();
                    ////Get the permission related details for the form 1410 from DB
                    getForm1410ParcelData = this.Form29510Control.WorkItem.GetFormDetails(Convert.ToInt32(1401), TerraScanCommon.UserId);
                    if (getForm1410ParcelData.Rows.Count > 0)
                    {
                        this.form1410penPermission = Convert.ToBoolean(getForm1410ParcelData.Rows[0][getForm1410ParcelData.IsPermissionOpenColumn.ColumnName].ToString());
                    }

                    this.Cursor = Cursors.WaitCursor;
                    Form parcelSelectionForm = new Form();

                    //masterformno optional parameter added to implement Readonly for Rollyear panel by priyadharshini
                    object[] optionalParameter = new object[] { this.rolYear,Convert.ToString(this.masterFormNo) };
                    parcelSelectionForm = this.form29510Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, optionalParameter, this.Form29510Control.WorkItem);
                    if (parcelSelectionForm != null && this.form1410penPermission == true)
                    {
                        ////To open Form 1401 Parcel Search
                        parcelSelectionForm.ShowDialog();
                        ////int.TryParse(TerraScanCommon.GetValue(parcelSelectionForm, "ParcelID").ToString(), out this.parcelId);
                        int.TryParse(TerraScanCommon.GetValue(parcelSelectionForm, SharedFunctions.GetResourceString("ParcelIDColumnName")).ToString(), out this.parcelId);

                        ////To Find the duplicate(Repeated) ParcelID
                        if (this.parcelId > 0)
                        {
                            for (int count = 0; count < this.CombineParcelGridView.OriginalRowCount; count++)
                            {
                                if (Convert.ToInt32(this.CombineParcelGridView.Rows[count].Cells[this.parcelData.f29510ListParcel.ParcelsIDColumn.ColumnName].Value.ToString()) == this.parcelId)
                                {
                                    this.hasAdded = true;
                                    break;
                                }
                                else
                                {
                                    this.hasAdded = false;
                                }
                            }

                            if (!this.hasAdded)
                            {
                                DataTable combineDataSet = F29510WorkItem.F29510_GetCombineParcelDetails(this.parcelId).Tables[0];
                                ////Add the newly selected parcel details in Existing dataset
                                this.parcelData.f29510ListParcel.Merge(combineDataSet);
                                this.parcelRowCount = this.parcelData.f29510ListParcel.Rows.Count;
                                this.SetFormHeight(this.parcelRowCount);
                                ////Reload the Grid DataSource
                                this.CombineParcelGridView.DataSource = this.parcelData.f29510ListParcel.DefaultView;
                                if (this.parcelRowCount != this.parcelData.f29510ListParcel.Rows.Count)
                                {
                                    for (int i = this.parcelData.f29510ListParcel.Rows.Count - 1; i >= this.parcelRowCount; i--)
                                    {
                                        this.parcelData.f29510ListParcel.Rows[i].Delete();
                                    }
                                }

                                this.ClearButton.Enabled = true;
                                this.RemoveButton.Enabled = true;
                                this.combineParcel = TerraScanCommon.GetXmlString(this.parcelData.f29510ListParcel);
                                this.SetEditRecord();
                                this.ProcessButton.Enabled = false;
                                TerraScanCommon.SetDataGridViewPosition(this.CombineParcelGridView, this.CombineParcelGridView.OriginalRowCount - 1);
                            }
                            else
                            {
                                //// To show the repeated/same parcel selection
                                MessageBox.Show(SharedFunctions.GetResourceString("ExistParcel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        this.CombineParcelGridView.Focus();
                        SliceReloadActiveRecord currentKeyIdInfo;
                        currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                        currentKeyIdInfo.SelectedKeyId = this.keyId;
                        ////this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                        SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + SharedFunctions.GetResourceString("F29510ParcelSelectionForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.SetButtonVisibility();
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
        /// To clear all the Data in GridView
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                int parcelCount = this.CombineParcelGridView.Rows.Count - 1;
                if (this.CombineParcelGridView.OriginalRowCount > 1)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("F29510ClearButton"), SharedFunctions.GetResourceString("F1410ClearHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //// Clear the Data in DataSet
                        for (int i = 0; i <= parcelCount; i++)
                        {
                            if (this.CombineParcelGridView.Rows[i].Cells[12].Value.ToString() != "1" || string.IsNullOrEmpty(this.CombineParcelGridView.Rows[i].Cells[12].Value.ToString()))
                            {
                                this.parcelData.f29510ListParcel.Rows[i].Delete();
                                i = 0;
                                parcelCount = parcelCount - 1;
                            }

                            this.parcelData.AcceptChanges();
                        }
                    }
                    ////else
                    ////{
                    ////    return;
                    ////}
                }

                this.parcelRowCount = this.parcelData.f29510ListParcel.Rows.Count;
                this.SetFormHeight(this.parcelRowCount);
                this.CombineParcelGridView.DataSource = this.parcelData.f29510ListParcel.DefaultView;
                this.ClearDataSetRows();
                this.SetFormHeight(this.parcelRowCount);
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
                this.RemoveButton.Enabled = false;
                this.ClearButton.Enabled = false;
                this.combineParcel = TerraScanCommon.GetXmlString(this.parcelData.f29510ListParcel);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To Remove the selected row in GridView
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CombineParcelGridView.CurrentRow.Cells[12].Value.ToString() != "1" || string.IsNullOrEmpty(this.CombineParcelGridView.CurrentRow.Cells[12].Value.ToString()))
                {
                    for (int i = 0; i <= this.parcelData.f29510ListParcel.Rows.Count - 1; i++)
                    {
                        if (this.parcelData.f29510ListParcel.Rows[i].ItemArray[0].ToString() == this.CombineParcelGridView.CurrentRow.Cells[10].Value.ToString())
                        {
                            ////Remove the Record from the DataSet
                            this.parcelData.f29510ListParcel.Rows[i].Delete();
                            break;
                        }
                    }
                }
                else
                {
                    //// Show MessageBox when user tried to remove Base Parcel
                    MessageBox.Show(SharedFunctions.GetResourceString("RemoveBaseParcel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.parcelData.f29510ListParcel.AcceptChanges();
                this.parcelRowCount = this.parcelData.f29510ListParcel.Rows.Count;
                this.SetFormHeight(this.parcelRowCount);
                this.CombineParcelGridView.DataSource = this.parcelData.f29510ListParcel.DefaultView;
                this.ClearDataSetRows();
                this.SetFormHeight(this.parcelRowCount);
                this.SetEditRecord();
                this.SetButtonVisibility();
                this.ProcessButton.Enabled = false;
                this.combineParcel = TerraScanCommon.GetXmlString(this.parcelData.f29510ListParcel);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To create Combined parcel value
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
              
                this.tempParcelID = this.ParcelNumberTextBox.Text.Trim().Replace("'", "''");
                if (this.ParcelNumberTextBox.Text == "<Auto>" || string.IsNullOrEmpty(this.ParcelNumberTextBox.Text))
                {
                    this.parcelNo = string.Empty;
                }
                else
                {
                    this.parcelNo = this.tempParcelID;
                }
                IsAttachment = AttachCheckBox.Checked;
                IsComment = CommentsCheckBox.Checked;
                IsPermit = PermitcheckBox.Checked;
                IsAssociation = AssociationcheckBox.Checked;
                IsNewConstruction = ConstructioncheckBox.Checked;
                this.combineParcel = TerraScanCommon.GetXmlString(this.parcelData.f29510ListParcel);
                //int returnVal = F29510WorkItem.F29510_CreateCombinedParcel(this.combinedId, Convert.ToString(this.keyId), this.parcelNo, TerraScanCommon.UserId);

                F29510ParcelCombineData.OutputValuesDataTable returnValues = F29510WorkItem.F29510_CreateCombinedParcel(this.combinedId, Convert.ToString(this.keyId), this.parcelNo, TerraScanCommon.UserId, IsAttachment, IsComment, IsPermit, IsAssociation, IsNewConstruction);

                if (returnValues.Rows.Count > 0 && !string.IsNullOrEmpty(returnValues.Rows[0][returnValues.ResultsColumn.ColumnName].ToString()))
                {
                    MessageBox.Show(returnValues.Rows[0][returnValues.ResultsColumn.ColumnName].ToString(), SharedFunctions.GetResourceString("F29510CombineProcessHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("NewParcelCombine"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.GetParcelDetails();
                this.SetButtonVisibility();
                this.SetTextBoxValue();
                this.SetCheckBoxValue();
                //MessageBox.Show(SharedFunctions.GetResourceString("NewParcelCombine"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                SliceReloadActiveRecord currentKeyIdInfo;
                currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                currentKeyIdInfo.SelectedKeyId = this.keyId;
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Cell Formating
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void CombineParcelGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.CombineParcelGridView.Rows[e.RowIndex].Cells[this.parcelData.f29510ListParcel.ParcelNumberColumn.ColumnName].Value.ToString()))
                {
                    //// Undeline the Base Parcel Record
                    if (this.CombineParcelGridView.Rows[e.RowIndex].Cells[this.parcelData.f29510ListParcel.RankColumn.ColumnName].Value.ToString() == "1")
                    {
                        e.CellStyle.Font = new Font("Microsoft Sans Serif", 8, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline)));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To Display ToolTip when mouse enters the Combine PictureBox
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void CombinePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ParcelCombineToolTip.SetToolTip(this.CombinePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Enter event for TextBox
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelNumberTextBox.Text == "<Auto>")
                {
                    this.ParcelNumberTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Leave event for TextBox
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()))
                {
                    this.ParcelNumberTextBox.Text = "<Auto>";
                    this.ParcelNumberTextBox.ForeColor = Color.Gray;
                }
                else
                {
                    this.ParcelNumberTextBox.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Keypress Event for TextBox
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handle Delete key event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.SetEditRecord();
                    this.ProcessButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Picture Box Click event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void CombinePictureBox_Click(object sender, EventArgs e)
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

        #endregion

        #region Methods

        /// <summary>
        /// Form Load 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void F29510_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.parcelData = F29510WorkItem.F29510GetParcelDetails(this.keyId);
                this.CustomiseDataGridView();
                this.ParcelNumberTextBox.Focus();
                this.GetParcelDetails();
                this.SetButtonVisibility();
                this.SetFormHeight(this.parcelRowCount);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.CombinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CombinePictureBox.Height, this.CombinePictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
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
        /// Enable and disable controls based on Form permissions
        /// </summary>
        /// <param name="controlLook">ControlLock</param>
        private void ControlLock(bool controlLook)
        {
            this.ParcelNumberTextBox.LockKeyPress = controlLook;
            this.AddParcelButton.Enabled = !controlLook;
            this.RemoveButton.Enabled = !controlLook;
            this.ClearButton.Enabled = !controlLook;
            this.ProcessButton.Enabled = !controlLook;
        }

        /// <summary>
        /// CheckErrors
        /// </summary>
        /// <returns>bool</returns>
        private SliceValidationFields CheckErrors()
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            return sliceValidationFields;
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

        /// <summary>
        /// Customises the data grid view.
        /// </summary>
        private void CustomiseDataGridView()
        {
            this.CombineParcelGridView.AllowUserToResizeColumns = false;
            this.CombineParcelGridView.AutoGenerateColumns = false;
            this.CombineParcelGridView.AllowUserToResizeRows = false;
            this.CombineParcelGridView.StandardTab = true;
            this.CombineParcelGridView.PrimaryKeyColumnName = this.parcelData.f29510ListParcel.ParcelsIDColumn.ColumnName;

            DataGridViewColumnCollection columns = this.CombineParcelGridView.Columns;

            columns[this.parcelData.f29510ListParcel.ParcelNumberColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.ParcelNumberColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.ParcelTypeColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.ParcelTypeColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.CodeColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.CodeColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.PrimaryOwnerColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.PrimaryOwnerColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.SitusColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.SitusColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.TaxableValueColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.TaxableValueColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.BaseParcelIDColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.BaseParcelIDColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.CombineIDColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.CombineIDColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.IsProcessedColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.IsProcessedColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.RollYearColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.RollYearColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.ParcelsIDColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.ParcelsIDColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.FeatureIDColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.FeatureIDColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.RankColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.RankColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.CombineParcelNumberColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.CombineParcelNumberColumn.ColumnName;
            columns[this.parcelData.f29510ListParcel.CombineParcelIDColumn.ColumnName].DataPropertyName = this.parcelData.f29510ListParcel.CombineParcelIDColumn.ColumnName;

            columns[this.parcelData.f29510ListParcel.ParcelNumberColumn.ColumnName].DisplayIndex = 0;
            columns[this.parcelData.f29510ListParcel.ParcelTypeColumn.ColumnName].DisplayIndex = 1;
            columns[this.parcelData.f29510ListParcel.CodeColumn.ColumnName].DisplayIndex = 2;
            columns[this.parcelData.f29510ListParcel.PrimaryOwnerColumn.ColumnName].DisplayIndex = 3;
            columns[this.parcelData.f29510ListParcel.SitusColumn.ColumnName].DisplayIndex = 4;
            columns[this.parcelData.f29510ListParcel.TaxableValueColumn.ColumnName].DisplayIndex = 5;
            columns[this.parcelData.f29510ListParcel.BaseParcelIDColumn.ColumnName].DisplayIndex = 6;
            columns[this.parcelData.f29510ListParcel.CombineIDColumn.ColumnName].DisplayIndex = 7;
            columns[this.parcelData.f29510ListParcel.IsProcessedColumn.ColumnName].DisplayIndex = 8;
            columns[this.parcelData.f29510ListParcel.RollYearColumn.ColumnName].DisplayIndex = 9;
            columns[this.parcelData.f29510ListParcel.ParcelsIDColumn.ColumnName].DisplayIndex = 10;
            columns[this.parcelData.f29510ListParcel.FeatureIDColumn.ColumnName].DisplayIndex = 11;
            columns[this.parcelData.f29510ListParcel.RankColumn.ColumnName].DisplayIndex = 12;
            columns[this.parcelData.f29510ListParcel.CombineParcelNumberColumn.ColumnName].DisplayIndex = 13;
            columns[this.parcelData.f29510ListParcel.CombineParcelIDColumn.ColumnName].DisplayIndex = 14;
        }

        /// <summary>
        /// Get Parcel Details
        /// </summary>
        private void GetParcelDetails()
        {
            this.parcelData = F29510WorkItem.F29510GetParcelDetails(this.keyId);
            this.parcelRowCount = this.parcelData.f29510ListParcel.Rows.Count;
            this.CheckValidDataSet(this.parcelData);

            if (this.validDataSet)
            {
                this.CombineParcelGridView.DataSource = this.parcelData.f29510ListParcel.DefaultView;

                if (this.parcelRowCount != this.parcelData.f29510ListParcel.Rows.Count)
                {
                    for (int i = this.parcelData.f29510ListParcel.Rows.Count - 1; i >= this.parcelRowCount; i--)
                    {
                        this.parcelData.f29510ListParcel.Rows[i].Delete();
                    }
                }

                if (this.CombineParcelGridView.OriginalRowCount > 1)
                {
                    for (int i = 0; i < this.CombineParcelGridView.OriginalRowCount; i++)
                    {
                        if (this.CombineParcelGridView.Rows[i].Cells[12].Value.ToString() == "1")
                        {
                            this.combinedId = Convert.ToInt32(this.CombineParcelGridView.Rows[i].Cells[this.parcelData.f29510ListParcel.CombineIDColumn.ColumnName].Value.ToString());
                            this.hasProcessed = Convert.ToBoolean(this.CombineParcelGridView.Rows[i].Cells[this.parcelData.f29510ListParcel.IsProcessedColumn.ColumnName].Value.ToString());
                            ////this.baseParcel = Convert.ToInt32(this.CombineParcelGridView.Rows[i].Cells[this.parcelData.f29510ListParcel.BaseParcelIDColumn.ColumnName].Value.ToString());
                            if (!string.IsNullOrEmpty(this.CombineParcelGridView.Rows[i].Cells[this.parcelData.f29510ListParcel.RollYearColumn.ColumnName].Value.ToString()))
                            {
                                this.rolYear = Convert.ToInt32(this.CombineParcelGridView.Rows[i].Cells[this.parcelData.f29510ListParcel.RollYearColumn.ColumnName].Value.ToString());
                            }
                            break;
                        }
                    }
                }
                else
                {
                    this.combinedId = Convert.ToInt32(this.CombineParcelGridView.Rows[0].Cells[this.parcelData.f29510ListParcel.CombineIDColumn.ColumnName].Value.ToString());
                    this.hasProcessed = Convert.ToBoolean(this.CombineParcelGridView.Rows[0].Cells[this.parcelData.f29510ListParcel.IsProcessedColumn.ColumnName].Value.ToString());
                    ////this.baseParcel = Convert.ToInt32(this.CombineParcelGridView.Rows[0].Cells[this.parcelData.f29510ListParcel.BaseParcelIDColumn.ColumnName].Value.ToString());
                    if (!string.IsNullOrEmpty(this.CombineParcelGridView.Rows[0].Cells[this.parcelData.f29510ListParcel.RollYearColumn.ColumnName].Value.ToString()))
                    {
                        this.rolYear = Convert.ToInt32(this.CombineParcelGridView.Rows[0].Cells[this.parcelData.f29510ListParcel.RollYearColumn.ColumnName].Value.ToString());
                    }
                }

                this.SetTextBoxValue();
                this.SetCheckBoxValue();
                TerraScanCommon.SetDataGridViewPosition(this.CombineParcelGridView, 0);
                this.CombineParcelGridView.Focus();
                this.CombineParcelGridView.CurrentRow.Selected = true;
            }
            else
            {
                this.CombineParcelGridView.DataSource = null;
                this.SetEnable(false);
                this.SetTextBoxValue();
                this.SetCheckBoxValue();
                this.ParcelNumberTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// Set TextBox Value
        /// </summary>
        private void SetTextBoxValue()
        {
            if (!string.IsNullOrEmpty(this.CombineParcelGridView.Rows[0].Cells[this.parcelData.f29510ListParcel.CombineParcelNumberColumn.ColumnName].Value.ToString()))
            {
                this.ParcelNumberTextBox.Text = this.CombineParcelGridView.Rows[0].Cells[this.parcelData.f29510ListParcel.CombineParcelNumberColumn.ColumnName].Value.ToString();
                this.ParcelNumberTextBox.ForeColor = Color.Gray;
            }
            else
            {
                this.ParcelNumberTextBox.Text = "<Auto>";
                this.ParcelNumberTextBox.ForeColor = Color.Gray;
            }
        }

        private void SetCheckBoxValue()
        {

            if (this.parcelData.f29510ListParcel.Rows[0][this.parcelData.f29510ListParcel.IsCombineAttachmentColumn.ColumnName].ToString().ToLower().Equals("true"))
            {
                this.AttachCheckBox.Checked = true;
            }
            else
            {
                this.AttachCheckBox.Checked = false;
            }

            if (this.parcelData.f29510ListParcel.Rows[0][this.parcelData.f29510ListParcel.IsCombineCommentColumn.ColumnName].ToString().ToLower().Equals("true"))
            {
                this.CommentsCheckBox.Checked = true;
            }
            else
            {
                this.CommentsCheckBox.Checked = false;
            }

            if (this.parcelData.f29510ListParcel.Rows[0][this.parcelData.f29510ListParcel.IsCombinePermitColumn.ColumnName].ToString().ToLower().Equals("true"))
            {
                this.PermitcheckBox.Checked = true;
            }
            else
            {
                this.PermitcheckBox.Checked = false;
            }

            if (this.parcelData.f29510ListParcel.Rows[0][this.parcelData.f29510ListParcel.IsCombineAssociationColumn.ColumnName].ToString().ToLower().Equals("true"))
            {
                this.AssociationcheckBox.Checked = true;
            }
            else
            {
                this.AssociationcheckBox.Checked = false;
            }

            if (this.parcelData.f29510ListParcel.Rows[0][this.parcelData.f29510ListParcel.IsNewConstructionColumn.ColumnName].ToString().ToLower().Equals("true"))
            {
                this.ConstructioncheckBox.Checked = true;
            }
            else
            {
                this.ConstructioncheckBox.Checked = false;
            }
           
        }

        /// <summary>
        /// Save Combined Parcel Details
        /// </summary>
        private void SaveCombineParcel()
        {
            ////defaule null
            int? combineIdValue = null;
           
            ////Replace Double quotes instead of single quotes
            this.tempParcelID = this.ParcelNumberTextBox.Text.Trim().Replace("'", "''");
            if (this.ParcelNumberTextBox.Text == "<Auto>" || string.IsNullOrEmpty(this.ParcelNumberTextBox.Text))
            {
                this.parcelNo = string.Empty;
            }
            else
            {
                this.parcelNo = this.tempParcelID;
            }

            if (this.combinedId != 0)
            {
                combineIdValue = this.combinedId;
            }
            IsAttachment = AttachCheckBox.Checked;
            IsComment = CommentsCheckBox.Checked;
            IsPermit = PermitcheckBox.Checked;
            IsAssociation = AssociationcheckBox.Checked;
            IsNewConstruction = ConstructioncheckBox.Checked;
            DataTable dt=this.parcelData.f29510ListParcel;
            dt.Columns.Remove("IsCombineAttachment");
            dt.Columns.Remove("IsCombineComment");
            dt.Columns.Remove("IsCombineAssociation");
            dt.Columns.Remove("IsCombinePermit");
            dt.Columns.Remove("IsNewConstruction");

          

            ////Get XMLString
            this.combineParcel = TerraScanCommon.GetXmlString(dt);
            int returnValue = F29510WorkItem.F29510_SaveCombineParcelDetails(combineIdValue, this.parcelNo, this.combineParcel, TerraScanCommon.UserId,IsAttachment,IsComment,IsPermit,IsAssociation,IsNewConstruction);
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = this.keyId;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            this.ProcessButton.Enabled = true;
            this.ParcelNumberTextBox.Enabled = true;
        }

        /// <summary>
        /// Checks the valid data set.
        /// </summary>
        /// <param name="checkDataSet">The data set.</param>
        private void CheckValidDataSet(DataSet checkDataSet)
        {
            if (checkDataSet.Tables.Count > 0 && checkDataSet != null)
            {
                if (checkDataSet.Tables[0].Rows.Count != 0)
                {
                    this.validDataSet = true;
                }
                else
                {
                    this.validDataSet = false;
                }
            }
            else
            {
                this.validDataSet = false;
            }
        }

        /// <summary>
        /// Enable / Disable Buttons
        /// </summary>
        private void SetButtonVisibility()
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.hasProcessed)
                {
                    if (this.CombineParcelGridView.OriginalRowCount > 1)
                    {
                        this.SetEnable(true);
                        this.ParcelNumberTextBox.Enabled = true;
                        this.AttachCheckBox.Enabled = true;
                        this.AssociationcheckBox.Enabled = true;
                        this.CommentsCheckBox.Enabled = true;
                        this.ConstructioncheckBox.Enabled = true;
                        this.PermitcheckBox.Enabled = true;
                    }
                    else if (this.CombineParcelGridView.OriginalRowCount == 1)
                    {
                        this.AddParcelButton.Enabled = true;
                        this.RemoveButton.Enabled = false;
                        this.ClearButton.Enabled = false;
                        this.ProcessButton.Enabled = false;
                        this.ParcelNumberTextBox.Enabled = true;
                        this.AttachCheckBox.Enabled = true;
                        this.AssociationcheckBox.Enabled = true;
                        this.CommentsCheckBox.Enabled = true;
                        this.ConstructioncheckBox.Enabled = true;
                        this.PermitcheckBox.Enabled = true;
                    }
                    else
                    {
                        this.SetEnable(false);
                        this.ParcelNumberTextBox.Enabled = false;
                        this.AttachCheckBox.Enabled = false;
                        this.AssociationcheckBox.Enabled = false;
                        this.CommentsCheckBox.Enabled = false;
                        this.ConstructioncheckBox.Enabled = false;
                        this.PermitcheckBox.Enabled = false;
                    }
                }
                else
                {
                    this.SetEnable(false);
                    this.ParcelNumberTextBox.Enabled = false;
                    this.AttachCheckBox.Enabled = false;
                    this.AssociationcheckBox.Enabled = false;
                    this.CommentsCheckBox.Enabled = false;
                    this.ConstructioncheckBox.Enabled = false;
                    this.PermitcheckBox.Enabled = false;
                }
            }
            else
            {
                this.SetEnable(false);
                this.ParcelNumberTextBox.Enabled = false;
                this.AttachCheckBox.Enabled = false;
                this.AssociationcheckBox.Enabled = false;
                this.CommentsCheckBox.Enabled = false;
                this.ConstructioncheckBox.Enabled = false;
                this.PermitcheckBox.Enabled = false;
            }
        }

        /// <summary>
        /// Enable or Disable Buttons
        /// </summary>
        /// <param name="enableProperty">EnableProperty</param>
        private void SetEnable(bool enableProperty)
        {
            this.AddParcelButton.Enabled = enableProperty;
            this.RemoveButton.Enabled = enableProperty;
            this.ClearButton.Enabled = enableProperty;
            this.ProcessButton.Enabled = enableProperty;
        }

        /// <summary>
        /// Sets the height of the Form. (Vertical grow of Form)
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetFormHeight(int recordCount)
        {
            if (recordCount > 1)
            {
                int increment = ((recordCount - 1) * 22);
                this.SetHeight(increment);
            }
            else
            {
                this.SetHeight(0);
            }
        }

        /// <summary>
        /// Sets the Controls Height and Position
        /// </summary>
        /// <param name="height">Increased Height</param>
        private void SetHeight(int height)
        {
            this.CombineParcelGridView.Height = 44 + height;
            this.MainParcelCombinepanel.Height = (199 + height + 44) - 93;
            this.CombinePictureBox.Height = (199 + height + 44) - 93;
            this.CombineParcelspanel.Height = 158 + height+44;
            this.Height = this.CombinePictureBox.Height;

            SliceResize sliceResize;
            sliceResize.MasterFormNo = 24510;
            ////sliceResize.SliceFormName = "D24500.F29510";
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.CombinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CombinePictureBox.Height, this.CombinePictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
        }

        /// <summary>
        /// Clear Rows when it doesnt have Data
        /// </summary>
        private void ClearDataSetRows()
        {
            if (this.parcelRowCount != this.parcelData.f29510ListParcel.Rows.Count)
            {
                for (int i = this.parcelData.f29510ListParcel.Rows.Count - 1; i >= this.parcelRowCount; i--)
                {
                    this.parcelData.f29510ListParcel.Rows[i].Delete();
                }
            }
        }

        #endregion

        private void AttachCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void CommentsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void PermitcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void AssociationcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ConstructioncheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}