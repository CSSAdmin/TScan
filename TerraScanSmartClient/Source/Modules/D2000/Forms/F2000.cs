//--------------------------------------------------------------------------------------------
// <copyright file="F2000.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07 May 07		Sam K	            Created
//*********************************************************************************/

namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
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
    using Microsoft.Practices.CompositeUI.UIElements;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Class For F2000
    /// </summary>
    public partial class F2000 : BasePage
    {
        #region Variables

        /// <summary>
        /// Hash Table for reportFileIdHashTable
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// To Store Old Row Index
        /// </summary>
        private int oldRowIndex;

        /// <summary>
        /// To Store Current Row Index
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// Flag to Restrict allowTextChange in Form Load
        /// </summary>
        private bool allowTextChange;

        /// <summary>
        /// To store OwnerMainButton Value
        /// </summary>
        private int tempActiveButton;

        /// <summary>
        /// To store OwnerMainButton Value
        /// </summary>
        private int tempOwnerMainButton;

        /// <summary>
        /// To store ExemptButton Value
        /// </summary>
        private int tempExemptButton;

        /// <summary>
        /// Temp value of RowIndex
        /// </summary>
        private int tempPartieCoulmnIndex;

        /// <summary>
        /// Used to load the Parcel Status Data Table.
        /// </summary>
        private F2000ParcelStatusData.ListParcelStatusDataTableDataTable listParcelStatusDataTable = new F2000ParcelStatusData.ListParcelStatusDataTableDataTable();

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// To find EditMode
        /// </summary>
        private bool dataChanged = false;

        /// <summary>
        /// To find Form Closing
        /// </summary>
        private bool formClosing = true;

        /// <summary>
        /// Used to store Parcel KeyID
        /// </summary>
        private int parcelKeyId;

        /// <summary>
        /// To load Current ParcelID
        /// </summary>
        private int currentParcelId;

        /// <summary>
        /// Get the No of Parcel Records
        /// </summary>
        private int parcelCount;

        /// <summary>
        /// form8002Control
        /// </summary>
        private F2000Controller form2000Control;

        /// <summary>
        /// parcelTypeDataset
        /// </summary>
        private F2000ParcelStatusData.f2000ListParcelTypeDataTable parcelTypeDataset = new F2000ParcelStatusData.f2000ListParcelTypeDataTable();

        /// <summary>
        /// listrecordlock
        /// </summary>
        private F2000ParcelStatusData listrecordlock = new F2000ParcelStatusData();

        /// <summary>
        /// lockStatus
        /// </summary>
        private string lockStatus;

        /// <summary>
        /// lockedBy
        /// </summary>
        private string lockedBy;

        /// <summary>
        /// lockBool
        /// </summary>
        private bool lockBool;

        /// <summary>
        /// lockedDate
        /// </summary>
        private string lockedDate;
        
        /// <summary>
        /// valueXml
        /// </summary>
        private string valueXml;
        
        /// <summary>
        /// componentAssumptionsDataSet
        /// </summary>
        private DataSet componentAssumptionsDataSet = new DataSet();

          /// <summary>
        /// cancelbool
        /// </summary>
        private bool cancelbool;
        
        #endregion

        #region constructor

        /// <summary>
        /// constructor F2000
        /// </summary>
        public F2000()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// constructor F2000
        /// </summary>
        /// <param name="keyId">keyId</param>
        public F2000(int keyId)
        {
            this.InitializeComponent();
            this.parcelKeyId = keyId;
        }

        #endregion constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F2000Controller F2000Controll
        {
            get { return this.form2000Control as F2000Controller; }
            set { this.form2000Control = value; }
        }

        #endregion property

        #region Events

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F2000_Load(object sender, EventArgs e)
        {
            ////ParcelStatusSaveButton.Enabled = false;
            ////ParcelStatusDeleteButton.Enabled = true;
            this.FillParcelTypeCombo();
            this.lockBool = false;
            this.CloseButton.TabStop = false;

            this.valueXml = this.form2000Control.WorkItem.ListRecordLockStatus(Convert.ToInt32(this.Tag), this.parcelKeyId).ToString();
            StringReader stringXmlReader = new StringReader(this.valueXml);
            System.Xml.XmlTextReader textReaderHouseType1 = new System.Xml.XmlTextReader(stringXmlReader);
            this.componentAssumptionsDataSet.ReadXml(textReaderHouseType1);
            if (this.componentAssumptionsDataSet.Tables[0].Rows.Count > 0)
            {
                this.lockStatus = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockStatus"].ToString();
                this.lockedBy = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedBy"].ToString();
                this.lockedDate = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedDate"].ToString();
            }

            if (this.lockStatus == "True")
            {
                this.lockBool = true;
            }
            else
            {
                this.lockBool = false;
            }

            this.SaveMenu.Click += new EventHandler(this.ParcelStatusSaveButton_Click);
            this.getFormDetailsDataDetails = this.form2000Control.WorkItem.GetFormDetails(Convert.ToInt32(this.Tag), TerraScanCommon.UserId);
            this.slicePermissionField.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
            this.slicePermissionField.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
            this.slicePermissionField.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
            this.slicePermissionField.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
            this.CustomizeParcelStatusGridView();
            this.LoadParcelStatusGridView();
            this.SetDataBindingValue(0);
            ////this.ParcelStatusDescription.Select();
            ////this.ParcelStatusDescription.Focus();
            this.ParcelTypeComboBox.Select();
            this.ParcelTypeComboBox.Focus();
            this.CloseButton.Visible = true;
            ////this.CancelButton = this.CloseButton;
            this.SetCancelButton();
            if (this.lockBool == true)
            {
                ////  ParcelStatusSaveButton.SetButtonType =
                ParcelStatusSaveButton.BackColor = Color.Red;
                ParcelStatusSaveButton.Text = "Locked";
                this.ParcelStatusCancelButton.Enabled = false;
                this.ParcelStatusDeleteButton.Enabled = false;
                this.ParcelStatusSaveButton.Enabled = true;
            }
            else
            {
                ParcelStatusSaveButton.Enabled = false;
                ParcelStatusSaveButton.ForeColor = Color.Gray;
                this.ParcelStatusCancelButton.Enabled = false;
                ////ParcelStatusCancelButton.Enabled = true;
                ////ParcelStatusDeleteButton.Enabled = true;
            }
        }

        /*
        /// <summary>
        /// To Change the Status IsActive Button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">name</param>
        private void ParcelStatusActiveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelStatusActiveButton.StatusIndicator)
                {
                    this.ParcelStatusActiveButton.StatusIndicator = false;
                }
                else
                {
                    this.ParcelStatusActiveButton.StatusIndicator = true;
                }

                this.tempActiveButton = Convert.ToInt32(this.ParcelStatusActiveButton.StatusIndicator);
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        */

        /// <summary>
        /// To Change the Status IsExempt Button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusExemptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelStatusExemptButton.StatusIndicator)
                {
                    this.ParcelStatusExemptButton.StatusIndicator = false;
                }
                else
                {
                    this.ParcelStatusExemptButton.StatusIndicator = true;
                }

                this.tempExemptButton = Convert.ToInt32(this.ParcelStatusExemptButton.StatusIndicator);
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To Change the Status IsOwnerMain Button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusOwnerMainButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelStatusOwnerMainButton.StatusIndicator)
                {
                    this.ParcelStatusOwnerMainButton.StatusIndicator = false;
                }
                else
                {
                    this.ParcelStatusOwnerMainButton.StatusIndicator = true;
                }

                this.tempOwnerMainButton = Convert.ToInt32(this.ParcelStatusOwnerMainButton.StatusIndicator);
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event For ParcelStatusDataGridView Cell Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (this.dataChanged)
                    {
                        if (this.oldRowIndex != e.RowIndex)
                        {
                            this.currentRowIndex = e.RowIndex;
                            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        this.form2000Control.WorkItem.F2000_UpdateParcelStatus(this.currentParcelId, this.ParcelStatusDescription.Text.Trim(), this.ParcelTypeComboBox.SelectedValue.ToString(), this.tempExemptButton, this.tempOwnerMainButton, TerraScanCommon.UserId);
                                        //// this.dataChanged = false;
                                        this.LoadParcelStatusGridView();
                                        this.SetDataBindingValue(this.currentRowIndex);
                                        TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.currentRowIndex);
                                        this.EnableParcelStatusGridSorting();
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.dataChanged = false;
                                        this.SetDataBindingValue(this.currentRowIndex);
                                        TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.currentRowIndex);
                                        this.ToEnableSaveCancelButton(false);
                                        this.oldRowIndex = e.RowIndex;
                                        this.EnableParcelStatusGridSorting();
                                        ////this.ParcelStatusCancelButton.Enabled = true; 
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.oldRowIndex);
                                        break;
                                    }
                            } ///// End Case
                        }
                    }
                    else
                    {
                        this.ClearParcelStatesValues();
                        this.oldRowIndex = e.RowIndex;
                        this.SetDataBindingValue(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event For ParcelStatusDataGridView RowEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.ParcelStatusDataGridView.OriginalRowCount > e.RowIndex)
                {
                    if (!this.dataChanged)
                    {
                        this.ClearParcelStatesValues();
                        this.oldRowIndex = e.RowIndex;
                        this.SetDataBindingValue(e.RowIndex);
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Save Button Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusSaveButton_Click(object sender, EventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    if (this.ParcelStatusSaveButton.Enabled)
                    {
                        if (this.ParcelTypeComboBox.SelectedValue.ToString() != null)
                        {
                            this.form2000Control.WorkItem.F2000_UpdateParcelStatus(this.currentParcelId, this.ParcelStatusDescription.Text.Trim(), this.ParcelTypeComboBox.SelectedValue.ToString(), this.tempExemptButton, this.tempOwnerMainButton, TerraScanCommon.UserId);
                            this.LoadParcelStatusGridView();
                            this.SetDataBindingValue(this.oldRowIndex);
                            TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.oldRowIndex);
                            this.EnableParcelStatusGridSorting();
                            this.ParcelStatusSaveButton.Enabled = false;
                            this.ParcelStatusSaveButton.ForeColor = Color.Gray;
                        }
                    }
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Event for KeyUP and Down
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.dataChanged)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                if (((DataGridView)sender).CurrentCell.RowIndex <= this.parcelCount)
                                {
                                    this.tempPartieCoulmnIndex = ((DataGridView)sender).CurrentCell.RowIndex + 1;
                                }

                                this.ParcelStatusGridViewKeyupKeyDown(sender, e);
                                break;
                            }

                        case Keys.Up:
                            {
                                if (((DataGridView)sender).CurrentCell.RowIndex > 0)
                                {
                                    this.tempPartieCoulmnIndex = ((DataGridView)sender).CurrentCell.RowIndex - 1;
                                }

                                this.ParcelStatusGridViewKeyupKeyDown(sender, e);
                                break;
                            }
                    }
                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                if (((DataGridView)sender).CurrentCell.RowIndex <= this.parcelCount)
                                {
                                    this.oldRowIndex = ((DataGridView)sender).CurrentCell.RowIndex + 1;
                                }

                                this.ClearParcelStatesValues();
                                this.SetDataBindingValue(this.oldRowIndex);
                                break;
                            }

                        case Keys.Up:
                            {
                                if (((DataGridView)sender).CurrentCell.RowIndex > 0)
                                {
                                    this.oldRowIndex = ((DataGridView)sender).CurrentCell.RowIndex - 1;
                                }

                                this.ClearParcelStatesValues();
                                this.SetDataBindingValue(this.oldRowIndex);
                                break;
                            }
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event for Key up and dowd
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusGridViewKeyupKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.dataChanged)
                {
                    if (this.oldRowIndex != this.tempPartieCoulmnIndex)
                    {
                        this.currentRowIndex = this.tempPartieCoulmnIndex;
                        switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                {
                                    this.form2000Control.WorkItem.F2000_UpdateParcelStatus(this.currentParcelId, this.ParcelStatusDescription.Text.Trim(), this.ParcelTypeComboBox.SelectedValue.ToString(), this.tempExemptButton, this.tempOwnerMainButton, TerraScanCommon.UserId);
                                    this.LoadParcelStatusGridView();
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.currentRowIndex);
                                    this.SetDataBindingValue(this.currentRowIndex);
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.currentRowIndex);
                                    this.EnableParcelStatusGridSorting();
                                    this.oldRowIndex = this.tempPartieCoulmnIndex;
                                    e.Handled = true;
                                    break;
                                }

                            case DialogResult.No:
                                {
                                    this.dataChanged = false;
                                    ////TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.currentRowIndex);
                                    this.SetDataBindingValue(this.currentRowIndex);
                                    this.ToEnableSaveCancelButton(false);
                                    this.EnableParcelStatusGridSorting();
                                    this.oldRowIndex = this.tempPartieCoulmnIndex;
                                    e.Handled = false;
                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.oldRowIndex);
                                    e.Handled = true;
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    this.oldRowIndex = this.tempPartieCoulmnIndex;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event For ParcelStatusCancelButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusCancelButton_Click(object sender, EventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    if (this.ParcelStatusSaveButton.Enabled)
                    {
                    ////    cancelbool = false;
                    ////    //return;
                    ////}
                    ////if (cancelbool)
                    ////{ 
                        this.dataChanged = false;
                        this.EnableParcelStatusGridSorting();
                        TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.oldRowIndex);
                        this.SetDataBindingValue(this.oldRowIndex);
                        this.ToEnableSaveCancelButton(false);
                        this.SetCancelButton();
                      ParcelStatusSaveButton.ForeColor = Color.Gray;
                    }
                ////cancelbool = true;
               }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Event For ParcelStatusDescription_TextChang
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusDescription_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event For ParcelStatusDeleteButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.F2000Controll.WorkItem.F2000_DeleteParcelStatus(this.currentParcelId, TerraScanCommon.UserId);
                        this.dataChanged = true;
                        this.LoadParcelStatusGridView();
                        this.SetDataBindingValue(0);
                    }
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Event For ParcelStatusNewButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form subfundForm = new Form();
                object[] optionalParameter = new object[] { this.parcelKeyId };
                subfundForm = this.form2000Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2004, optionalParameter, this.form2000Control.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.FillParcelTypeCombo();

                        this.SaveMenu.Click += new EventHandler(this.ParcelStatusSaveButton_Click);
                        this.getFormDetailsDataDetails = this.form2000Control.WorkItem.GetFormDetails(Convert.ToInt32(this.Tag), TerraScanCommon.UserId);
                        this.slicePermissionField.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                        this.slicePermissionField.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
                        this.slicePermissionField.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
                        this.slicePermissionField.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
                        this.CustomizeParcelStatusGridView();
                        this.LoadParcelStatusGridView();
                        this.SetDataBindingValue(0);
                        this.ParcelStatusDescription.Select();
                        this.ParcelStatusDescription.Focus();
                        ////this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event For ParcelStatusDataGridView_CellDoubleClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.ParcelStatusDataGridView.OriginalRowCount > e.RowIndex)
                {
                    if (this.dataChanged)
                    {
                        switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                {
                                    this.form2000Control.WorkItem.F2000_UpdateParcelStatus(this.currentParcelId, this.ParcelStatusDescription.Text.Trim(), this.ParcelTypeComboBox.SelectedValue.ToString(), this.tempExemptButton, this.tempOwnerMainButton, TerraScanCommon.UserId);
                                    this.SetCancelButton();
                                    this.DialogResult = DialogResult.OK;
                                    if ((this.ParcelStatusDataGridView.Rows[e.RowIndex].Cells["ParcelFormID"].Value != null) && (!string.IsNullOrEmpty(this.ParcelStatusDataGridView.Rows[e.RowIndex].Cells["ParcelFormID"].Value.ToString())))
                                    {
                                        if (this.currentParcelId > 0)
                                        {
                                            this.Cursor = Cursors.WaitCursor;
                                            FormInfo formInfo;
                                            formInfo = TerraScanCommon.GetFormInfo(20000);
                                            formInfo.optionalParameters = new object[1];
                                            formInfo.optionalParameters[0] = this.currentParcelId;
                                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                                        }
                                    }

                                    break;
                                }

                            case DialogResult.No:
                                {
                                    this.DialogResult = DialogResult.OK;
                                    if ((this.ParcelStatusDataGridView.Rows[e.RowIndex].Cells["ParcelFormID"].Value != null) && (!string.IsNullOrEmpty(this.ParcelStatusDataGridView.Rows[e.RowIndex].Cells["ParcelFormID"].Value.ToString())))
                                    {
                                        if (this.currentParcelId > 0)
                                        {
                                            this.Cursor = Cursors.WaitCursor;
                                            FormInfo formInfo;
                                            formInfo = TerraScanCommon.GetFormInfo(20000);
                                            formInfo.optionalParameters = new object[1];
                                            formInfo.optionalParameters[0] = this.currentParcelId;
                                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                                        }
                                    }

                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if ((this.ParcelStatusDataGridView.Rows[e.RowIndex].Cells["ParcelFormID"].Value != null) && (!string.IsNullOrEmpty(this.ParcelStatusDataGridView.Rows[e.RowIndex].Cells["ParcelFormID"].Value.ToString())))
                        {
                            if (this.currentParcelId > 0)
                            {
                                this.Cursor = Cursors.WaitCursor;
                                FormInfo formInfo;
                                formInfo = TerraScanCommon.GetFormInfo(20000);
                                formInfo.optionalParameters = new object[1];
                                formInfo.optionalParameters[0] = this.currentParcelId;
                                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                                //// Added by Ramya
                                ////this.DialogResult = DialogResult.None;
                            }
                        }
                    }
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
        /// Form Closing Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    if (this.formClosing == true)
                    {
                        if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                        {
                            if (e.CloseReason.Equals(CloseReason.UserClosing))
                            {
                                if (this.dataChanged)
                                {
                                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                    {
                                        case DialogResult.Yes:
                                            {
                                                this.form2000Control.WorkItem.F2000_UpdateParcelStatus(this.currentParcelId, this.ParcelStatusDescription.Text.Trim(), this.ParcelTypeComboBox.SelectedValue.ToString(), this.tempExemptButton, this.tempOwnerMainButton, TerraScanCommon.UserId);
                                                //// this.dataChanged = false;
                                                this.LoadParcelStatusGridView();
                                                this.SetDataBindingValue(this.oldRowIndex);
                                                TerraScanCommon.SetDataGridViewPosition(this.ParcelStatusDataGridView, this.currentRowIndex);
                                                this.DialogResult = DialogResult.OK;
                                                e.Cancel = false;
                                                break;
                                            }

                                        case DialogResult.No:
                                            {
                                                this.DialogResult = DialogResult.No;
                                                e.Cancel = false;
                                                break;
                                            }

                                        case DialogResult.Cancel:
                                            {
                                                e.Cancel = true;
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    e.Cancel = false;
                                }
                            }
                            else
                            {
                               //// this.DialogResult = DialogResult.No;
                                e.Cancel = true;
                                this.formClosing = true;
                                this.lockBool = false;
                                this.cancelbool = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        this.formClosing = true;
                    }
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Current ParcelID Link Label Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusParcelIDLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.currentParcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.currentParcelId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    this.Close();
                }

                ////int reportAuditId = 0;
                ////this.Cursor = Cursors.WaitCursor;
                ////reportAuditId = this.currentParcelId;
                ////this.reportFileIdHashTable.Clear();
                ////this.reportFileIdHashTable.Add("ReportFileID", reportAuditId);

                //////// Shows the report form.
                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(200090, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Set Tooltip for ParcelNumber Label
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelStatusParcelNumberLabel_MouseEnter(object sender, EventArgs e)
        {
            if (this.ParcelStatusParcelNumberLabel.Text.Trim().Length > 13)
            {
                this.ParcelStatusLabelToolTip.SetToolTip(this.ParcelStatusParcelNumberLabel, this.ParcelStatusParcelNumberLabel.Text);
            }
            else
            {
                this.ParcelStatusLabelToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Combo Box Selection Changed Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ParcelTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To close the Form When pressing Esc Key
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the MouseEnter event of the ParcelStatusSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ParcelStatusSaveButton_MouseEnter(object sender, EventArgs e)
        {
            if (this.lockBool == true)
            {
                ParcelStatusSaveButton.BackColor = Color.Red;
                StringBuilder ownerAddress = new StringBuilder();

                ownerAddress.Append("Locked By:" + " " + this.lockedBy);

                ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));

                ownerAddress.Append("Locked On:" + " " + this.lockedDate);

                this.LockedtoolTip.SetToolTip(this.ParcelStatusSaveButton, ownerAddress.ToString());
            }
        }

        #endregion Event

        #region Methods

        /// <summary>
        /// Used to Set the correct value to the ComboBox
        /// </summary>
        /// <param name="setComboBox">The set combo box.</param>
        /// <param name="comboxString">The combox string.</param>
        private static void SetComboboxValue(TerraScan.UI.Controls.TerraScanComboBox setComboBox, string comboxString)
        {
            int correctIndex = 0;
            comboxString = comboxString.ToUpperInvariant();
            if (String.Compare(comboxString, SharedFunctions.GetResourceString("FALSEValue")) == 0 || String.Compare(comboxString, SharedFunctions.GetResourceString("TRUEValue")) == 0)
            {
                if (String.Compare(comboxString, SharedFunctions.GetResourceString("FALSEValue")) == 0)
                {
                    correctIndex = 0;
                }
                else
                {
                    correctIndex = 1;
                }
            }
            else
            {
                correctIndex = setComboBox.FindString(comboxString);
            }

            setComboBox.SelectedIndex = correctIndex;
        }

        /// <summary>
        /// CustomizeParcelStatus Grid
        /// </summary>
        private void CustomizeParcelStatusGridView()
        {
            try
            {
                this.ParcelStatusDataGridView.PrimaryKeyColumnName = this.listParcelStatusDataTable.ParcelFormIDColumn.ColumnName;
                this.ParcelStatusDataGridView.AutoGenerateColumns = false;
                this.ParcelFormID.DataPropertyName = this.listParcelStatusDataTable.ParcelFormIDColumn.ColumnName;
                this.ParcelID.DataPropertyName = this.listParcelStatusDataTable.ParcelIDColumn.ColumnName;
                this.ParcelNumber.DataPropertyName = this.listParcelStatusDataTable.ParcelNumberColumn.ColumnName;
                this.RollYear.DataPropertyName = this.listParcelStatusDataTable.RollYearColumn.ColumnName;
                this.Type.DataPropertyName = this.listParcelStatusDataTable.TypeColumn.ColumnName;
                this.Description.DataPropertyName = this.listParcelStatusDataTable.DescriptionColumn.ColumnName;
                this.IsExempt.DataPropertyName = this.listParcelStatusDataTable.IsExemptColumn.ColumnName;
                this.IsOwnerPrimary.DataPropertyName = this.listParcelStatusDataTable.IsOwnerPrimaryColumn.ColumnName;
                this.IsRetired.DataPropertyName = this.listParcelStatusDataTable.IsRetiredColumn.ColumnName;
                this.Type.DisplayIndex = 0;
                this.Description.DisplayIndex = 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Load Parcel Status Grid
        /// </summary>
        private void LoadParcelStatusGridView()
        {
            try
            {
                this.listParcelStatusDataTable.Clear();
                this.ParcelStatusDataGridView.DataSource = null;
                this.listParcelStatusDataTable = this.form2000Control.WorkItem.F2000_ListParcelStatus(this.parcelKeyId);
                this.parcelCount = this.listParcelStatusDataTable.Rows.Count;
                if (this.parcelCount > 0)
                {
                    this.ParcelStatusDataGridView.Enabled = true;
                    this.ParcelStatusDataGridView.Focus();
                    this.ParcelStatusDataGridView.DataSource = this.listParcelStatusDataTable;
                    this.ParcelStatusDataGridView.Rows[0].Selected = true;
                    if (this.ParcelStatusDataGridView.OriginalRowCount > this.ParcelStatusDataGridView.NumRowsVisible)
                    {
                        this.ParcelStatusVscrollBar.Visible = false;
                    }
                    else
                    {
                        this.ParcelStatusVscrollBar.Visible = true;
                    }
                }
                else
                {
                    this.DisableControls();
                    this.listParcelStatusDataTable.Clear();
                    this.ClearParcelStatesValues();
                    this.ParcelStatusDataGridView.DataSource = this.listParcelStatusDataTable;
                    this.ParcelStatusDataGridView.Enabled = false;
                    this.ParcelStatusDataGridView.Focus();
                    this.ParcelStatusVscrollBar.Visible = true;
                }

                this.ToEnableSaveCancelButton(false);

                this.SetCancelButton();

                this.dataChanged = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Bind the Selected ParcelFormID Details to Controls
        /// </summary>
        /// <param name="rowId">rowId</param>
        private void SetDataBindingValue(int rowId)
        {
            try
            {
                if (this.ParcelStatusDataGridView.OriginalRowCount > rowId)
                {
                    if (this.parcelCount > 0)
                    {
                        if (rowId >= 0 && this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.ParcelFormIDColumn.ColumnName].Value != null)
                        {
                            ////if (this.ParcelStatusDataGridView.Rows[rowId].Cells[listParcelStatusDataTable.IsRetiredColumn.ColumnName].Value.ToString() == "True")
                            ////{
                            ////    this.ParcelStatusStatusLabel.Text = "Retired";
                            ////}
                            ////else
                            ////{
                            ////    this.ParcelStatusStatusLabel.Text = "FormLabelSecondLine";
                            ////}

                            this.allowTextChange = true;

                            ////if (this.ParcelStatusDataGridView.Rows[rowId].Cells[listParcelStatusDataTable.IsActiveColumn.ColumnName].Value.ToString() == "True")
                            ////{
                            ////    this.tempActiveButton = 1;
                            ////}
                            ////else
                            ////{
                            ////    this.tempActiveButton = 0;
                            ////}

                            if (this.ParcelStatusDataGridView.Rows[rowId].Cells[listParcelStatusDataTable.IsExemptColumn.ColumnName].Value.ToString() == "True")
                            {
                                this.tempExemptButton = 1;
                            }
                            else
                            {
                                this.tempExemptButton = 0;
                            }

                            if (this.ParcelStatusDataGridView.Rows[rowId].Cells[listParcelStatusDataTable.IsOwnerPrimaryColumn.ColumnName].Value.ToString() == "True")
                            {
                                this.tempOwnerMainButton = 1;
                            }
                            else
                            {
                                this.tempOwnerMainButton = 0;
                            }

                            int.TryParse(this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.ParcelFormIDColumn.ColumnName].Value.ToString(), out this.currentParcelId);
                            //// this.SetStatusButtonsProperty(this.ParcelStatusActiveButton, Convert.ToBoolean(this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.IsActiveColumn.ColumnName].Value));
                            this.SetStatusButtonsProperty(this.ParcelStatusExemptButton, Convert.ToBoolean(this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.IsExemptColumn.ColumnName].Value));
                            this.SetStatusButtonsProperty(this.ParcelStatusOwnerMainButton, Convert.ToBoolean(this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.IsOwnerPrimaryColumn.ColumnName].Value));
                            this.ParcelStatusDescription.Text = this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.DescriptionColumn.ColumnName].Value.ToString();
                            SetComboboxValue(this.ParcelTypeComboBox, this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.TypeColumn.ColumnName].Value.ToString());
                            this.ParcelStatusParcelNumberLabel.Text = this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.ParcelNumberColumn.ColumnName].Value.ToString();
                            this.ParcelStatusRollYearLabel.Text = this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.RollYearColumn.ColumnName].Value.ToString();
                            this.ParcelStatusStatusLabel.Text = this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.TypeColumn.ColumnName].Value.ToString();
                            if (this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.ParcelNumberColumn.ColumnName].Value.ToString() != string.Empty && this.ParcelStatusDataGridView.Rows[rowId].Cells[this.listParcelStatusDataTable.RollYearColumn.ColumnName].Value.ToString() != string.Empty)
                            {
                                this.ParcelStatusSeprator.Text = "/";
                            }

                            this.ParcelStatusParcelIDLinkLabel.Text = "tAA_Parcel[ParcelID]" + this.ParcelStatusDataGridView.Rows[rowId].Cells[listParcelStatusDataTable.ParcelFormIDColumn.ColumnName].Value.ToString();
                            ////this.ParcelStatusParcelIDLinkLabel.Text = "tAA_Parcel[ParcelID]" + this.ParcelStatusDataGridView.Rows[rowId].Cells[listParcelStatusDataTable.ParcelIDColumn.ColumnName].Value.ToString();
                            this.allowTextChange = false;
                            
                            if (!this.slicePermissionField.newPermission)
                            {
                                this.ParcelStatusNewButton.Enabled = false;
                            }
                            else
                            {
                                this.ParcelStatusNewButton.Enabled = true;
                            }


                            //this.ParcelStatusNewButton.Enabled = this.slicePermissionField.newPermission;
                           // this.ParcelStatusNewButton.Enabled = true;
                        }
                    }
                }
                else
                {
                    this.ParcelStatusNewButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ClearParcelStatesValues
        /// </summary>
        private void ClearParcelStatesValues()
        {
            this.allowTextChange = true;
            ////this.SetStatusButtonsProperty(this.ParcelStatusActiveButton, true);
            this.SetStatusButtonsProperty(this.ParcelStatusExemptButton, true);
            this.SetStatusButtonsProperty(this.ParcelStatusOwnerMainButton, true);
            this.ParcelStatusDescription.Text = string.Empty;
            this.ParcelStatusSeprator.Text = string.Empty;
            this.ParcelStatusParcelNumberLabel.Text = string.Empty;
            this.ParcelStatusRollYearLabel.Text = string.Empty;
            this.ParcelStatusStatusLabel.Text = string.Empty;
            this.ParcelStatusParcelIDLinkLabel.Text = string.Empty;
            this.allowTextChange = false;
        }

        /// <summary>
        /// To Disable Controls If Record Count 0
        /// </summary>
        private void DisableControls()
        {
            ////this.ParcelStatusActiveButton.Enabled = false;
            this.ParcelStatusExemptButton.Enabled = false;
            this.ParcelStatusOwnerMainButton.Enabled = false;
            ////this.ParcelStatusActiveButton.StatusIndicator = false;
            this.ParcelStatusExemptButton.StatusIndicator = false;
            this.ParcelStatusOwnerMainButton.StatusIndicator = false;
            this.DescriptionPanel.Enabled = false;
            this.ParcelTypePanel.Enabled = false;
            this.ParcelStatusSaveButton.Enabled = false;
            this.ParcelStatusDeleteButton.Enabled = false;
            this.ParcelStatusCancelButton.Enabled = false;
        }

        /// <summary>
        /// Set the Status Buttons
        /// </summary>
        /// <param name="controlName">controlName</param>
        /// <param name="statusValue">statusValue</param>
        private void SetStatusButtonsProperty(TerraScanButton controlName, bool statusValue)
        {
            // To Change the color of the Button 
            if (statusValue != true)
            {
                controlName.StatusIndicator = false;
            }
            else
            {
                controlName.StatusIndicator = true;
            }
        }

        /// <summary>
        /// Set Edit Mode When Changing DescriptionText,IsActive,IsExempt,IsOwnerMain
        /// </summary>
        private void EditEnabled()
        {
            if (!this.allowTextChange)
            {
                this.DisableParcelStatusGridSorting();
                this.ToEnableSaveCancelButton(true);
                this.SetCancelButton();
                this.dataChanged = true;
                ParcelStatusSaveButton.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// To Enable and Disable Controls
        /// </summary>
        /// <param name="isenabled">isenabled</param>
        private void ToEnableSaveCancelButton(bool isenabled)
        {
            this.CheckEditPermission();

            if (!this.slicePermissionField.editPermission)
            {
                this.ParcelStatusCancelButton.Enabled = false;
                this.ParcelStatusSaveButton.Enabled = false;
            }
            else
            {
                if (this.lockBool == true)
                {
                    this.ParcelStatusCancelButton.Enabled = false;
                    this.ParcelStatusSaveButton.Enabled = false;
                }
                else
                {
                    this.ParcelStatusCancelButton.Enabled = isenabled;
                    this.ParcelStatusSaveButton.Enabled = isenabled;
                }
            }

            //if (!this.slicePermissionField.newPermission)
            //{
            //    this.ParcelStatusNewButton.Enabled = false;
            //}
            //else
            //{
            //    this.ParcelStatusNewButton.Enabled = !isenabled;
            //}

            if (!this.slicePermissionField.deletePermission)
            {
                this.ParcelStatusDeleteButton.Enabled = false;
            }
            else
            {
                if (this.lockBool == true)
                {
                    this.ParcelStatusDeleteButton.Enabled = false;
                }
                else
                {
                    this.ParcelStatusDeleteButton.Enabled = !isenabled;
                }
            }

            if (this.parcelCount == 0)
            {
                ////this.ParcelStatusActiveButton.Enabled = false;
                this.ParcelStatusExemptButton.Enabled = false;
                this.ParcelStatusOwnerMainButton.Enabled = false;
                ////this.ParcelStatusActiveButton.StatusIndicator = false;
                this.ParcelStatusExemptButton.StatusIndicator = false;
                this.ParcelStatusOwnerMainButton.StatusIndicator = false;
                this.ParcelStatusDeleteButton.Enabled = false;
            }
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void DisableParcelStatusGridSorting()
        {
            DataGridViewColumnCollection disableSortColumn = this.ParcelStatusDataGridView.Columns;
            disableSortColumn["Type"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableSortColumn["Description"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void EnableParcelStatusGridSorting()
        {
            DataGridViewColumnCollection enableSortColumn = this.ParcelStatusDataGridView.Columns;
            enableSortColumn["Type"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["Description"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            if (this.ParcelStatusCancelButton.Enabled)
            {
                this.CancelButton = this.ParcelStatusCancelButton;
            }
            else
            {
                this.CancelButton = this.CloseButton;
            }
        }

        /// <summary>
        /// Checks the edit permission.
        /// </summary>
        private void CheckEditPermission()
        {
            if (this.slicePermissionField.editPermission)
            {
                this.ParcelTypeComboBox.Enabled = true;
                ////this.EnableCommentControl();
                this.ParcelStatusDescription.LockKeyPress = false;

                // this.ParcelStatusActiveButton.Enabled = true;
                this.ParcelStatusExemptButton.Enabled = true;
                this.ParcelStatusOwnerMainButton.Enabled = true;
            }
            else
            {
                ////this.DisableCommentControl();
                this.ParcelTypeComboBox.Enabled = false;
                this.ParcelStatusDescription.LockKeyPress = true;
                this.ParcelStatusDescription.BackColor = Color.White;
                ////this.ParcelStatusActiveButton.Enabled = false;
                this.ParcelStatusExemptButton.Enabled = false;
                this.ParcelStatusOwnerMainButton.Enabled = false;
            }
        }

        /// <summary>
        /// Fill ParcelType combobox
        /// </summary>
        private void FillParcelTypeCombo()
        {
            try
            {
                this.parcelTypeDataset = this.form2000Control.WorkItem.GetParcelType();
                if (this.parcelTypeDataset.Rows.Count > 0)
                {
                    this.ParcelTypeComboBox.DisplayMember = this.parcelTypeDataset.ParcelTypeColumn.ColumnName;
                    this.ParcelTypeComboBox.ValueMember = this.parcelTypeDataset.ParcelTypeColumn.ColumnName;
                    this.ParcelTypeComboBox.DataSource = this.parcelTypeDataset;
                    this.parcelTypeDataset.DefaultView.Sort = "ParcelType ASC";
                    this.ParcelTypeComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        #endregion Methods   
    }
}