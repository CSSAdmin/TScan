//--------------------------------------------------------------------------------------------
// <copyright file="F3040.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3040.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15/05/2006       M.Vijaya Kumar     Created// 
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
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
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;

    public partial class F3040 : Form
    {
        #region Variables       

        /// <summary>
        /// Used to store the boolean value to enable or diable the Accept and other Buttons
        /// </summary>
        private bool enableButtonStatus;

        /// <summary>
        /// Used to store the neighborhoodZoningId
        /// </summary>
        private int neighborhoodZoningId;

        /// <summary>
        /// Used to store the current key id
        /// </summary>
        private int currentKeyId;

        private int currentFormId;

        /// <summary>
        /// Used to store the Zoning Grid Datatable row count
        /// </summary>
        private int ZoningDataGridDataTableRowCount;

        private F3040ZoningData zoningDetailsData = new F3040ZoningData();

        /// <summary>
        /// controller F3040Controller
        /// </summary>
        private F3040Controller form3040Control;

        /// <summary>
        /// To store Whether Save is performed 
        /// </summary>
        private bool saveStatus;

        /// <summary>
        /// currentRowIndex
        /// </summary>
        private int currentRowIndex;

        #endregion Variables

        #region Constructor

        public F3040()
        {
            this.enableButtonStatus = true;
            InitializeComponent();
        }

        public F3040(int neighborhoodZoningId)
        {
            this.enableButtonStatus = true;
            InitializeComponent();
            this.neighborhoodZoningId = neighborhoodZoningId;
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// For F3040Control
        /// </summary>
        [CreateNew]
        public F3040Controller Form3040Control
        {
            get { return this.form3040Control as F3040Controller; }
            set { this.form3040Control = value; }
        }

        #endregion Property

        #region Methods

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            if (!this.ZoningCancelButton.Enabled)
            {
                this.CancelButton = this.ZoningCloseButton;                
            }
            else
            {
                this.CancelButton = this.ZoningCancelButton;                
            }
        }

        private void LoadZoningForm()
        {
            this.enableButtonStatus = true;
            this.LoadZoningGrid();
            int.TryParse(this.Tag.ToString(), out currentFormId);
            this.SetCancelButton();            
        }

        private void CustimizeZoningGrid()
        {
            this.ZoningDataGridView.AutoGenerateColumns = false;
            this.ZoningCode.DataPropertyName = this.zoningDetailsData.ListZoning.ZoningCodeColumn.ColumnName;
            this.Zoning.DataPropertyName = this.zoningDetailsData.ListZoning.ZoningColumn.ColumnName;
            this.ZoningID.DataPropertyName = this.zoningDetailsData.ListZoning.ZoningIDColumn.ColumnName;

            this.ZoningCode.DisplayIndex = 0;
            this.Zoning.DisplayIndex = 1;
            this.ZoningID.DisplayIndex = 2;
            this.ZoningDataGridView.PrimaryKeyColumnName = this.zoningDetailsData.ListZoning.ZoningIDColumn.ColumnName;            
        }

        private void LoadZoningGrid()
        {
            try
            {
                int emptyRows;
                this.Cursor = Cursors.WaitCursor;
                this.zoningDetailsData.ListZoning.Clear();
                this.zoningDetailsData = this.form3040Control.WorkItem.F3040_GetZoningDetails();
                this.ZoningDataGridDataTableRowCount = this.zoningDetailsData.ListZoning.Rows.Count;
                ////to set the ZoningDataGridView focus
                ////this.ZoningDataGridView.Focus();
                if (this.ZoningDataGridDataTableRowCount > 0)
                {
                    ////if the no of visiable rows(grid) is greater than the actual rows from the Datatable ---
                    //// --- then a temp datatable with empty rows are merged with ListZoning datatable                    
                    if (this.ZoningDataGridView.NumRowsVisible > this.ZoningDataGridDataTableRowCount)
                    {
                        emptyRows = this.ZoningDataGridView.NumRowsVisible - this.ZoningDataGridDataTableRowCount;

                        for (int i = 0; i < emptyRows; i++)
                        {
                            this.zoningDetailsData.ListZoning.AddListZoningRow(this.zoningDetailsData.ListZoning.NewListZoningRow());
                        }
                    }
                    else
                    {
                        this.zoningDetailsData.ListZoning.AddListZoningRow(this.zoningDetailsData.ListZoning.NewListZoningRow());
                    }

                    ////this.GenericMgmtAcceptButton.Enabled = true;
                    this.ZoningDataGridView.DataSource = this.zoningDetailsData.ListZoning;
                    this.ZoningDataGridView.Rows[0].Selected = true;
                    ////TerraScanCommon.SetDataGridViewPosition(this.ZoningDataGridView, 0);
                }
                else
                {
                    for (int i = 0; i < this.ZoningDataGridView.NumRowsVisible; i++)
                    {
                        this.zoningDetailsData.ListZoning.AddListZoningRow(this.zoningDetailsData.ListZoning.NewListZoningRow());
                    }

                    ////this.GenericMgmtAcceptButton.Enabled = false;
                    this.ZoningDataGridView.DataSource = this.zoningDetailsData.ListZoning;
                    this.ZoningDataGridView.Rows[0].Selected = false;
                }

                this.ZoningDataGridView.Enabled = true;

                ////to enable or disable the vertical scroll bar
                if (this.zoningDetailsData.ListZoning.Rows.Count > this.ZoningDataGridView.NumRowsVisible)
                {
                    this.ZoningGridVerticalScroll.Visible = false;
                }
                else
                { 
                    this.ZoningGridVerticalScroll.Visible = true;
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// To enable or disable the save,cancel and close buttons
        /// </summary>
        /// <param name="unlock">Boolean Value
        /// when true = controls are enabled
        /// when false = controls are disabled
        /// </param>
        private void EnableSelectButtons(bool unlock)
        {
            this.ZoningSaveButton.Enabled = !unlock;
            this.ZoningCancelButton.Enabled = !unlock;
            this.ZoningCloseButton.Enabled = unlock;
            ////this.AttachmentButton.Enabled = unlock;
            ////this.CommentButton.Enabled = unlock;
            this.SetCancelButton();
        }

        private void SaveZoningDetails()
        {
            try 
            {
                int saveConfirm;
                if(this.ZoningSaveButton.Enabled)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (this.ValidateKeyItems())
                    {
                        string zoningItems = string.Empty;
                        this.ZoningDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.zoningDetailsData.ListZoning.AcceptChanges();
                        zoningItems = TerraScanCommon.GetXmlString(this.zoningDetailsData.ListZoning);
                        saveConfirm = this.form3040Control.WorkItem.F3040_SaveZoningDetails(zoningItems,TerraScan.Common.TerraScanCommon.UserId );

                        if (saveConfirm == -99)
                        {
                            MessageBox.Show("Zoning Code can not be duplicate", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.saveStatus = false;
                            this.DialogResult = DialogResult.None;
                        }
                        else if (saveConfirm == -100)
                        {
                            MessageBox.Show("Zoning can not be duplicate", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.saveStatus = false;
                            this.DialogResult = DialogResult.None;
                        }
                        else if (saveConfirm == 0)
                        {
                            ////to reload the entire form afther save function.
                            this.LoadZoningForm();
                            this.EnableSelectButtons(true);
                            ////TO DISABLE THE SEARCH BUTTON                            
                            this.saveStatus = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.saveStatus = false;
                        this.DialogResult = DialogResult.None;  
                    }

                }
                
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.Display, this.ParentForm);
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
        /// To check whether required fields exists.
        /// </summary>
        /// <returns></returns>
        private bool ValidateKeyItems()
        {
            try
            {
                bool validationResult = true;
                this.ZoningDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.zoningDetailsData.ListZoning.AcceptChanges();

                for (int i = 0; i < this.ZoningDataGridView.Rows.Count; i++)
                {
                    if (validationResult)
                    {
                        if (((!string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells[0].Value.ToString().Trim()))) && ((string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells[0].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells[1].Value.ToString().Trim()))))
                        {
                            validationResult = false;
                        }
                    }
                }

                return validationResult;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// To return boolean value whether emprty rows are available
        /// </summary>
        /// <returns>
        /// When false - empty rows are present
        /// when True - No empty rows present
        /// </returns>
        private bool ValidateEmptyRows()
        {
            try
            {
                bool emptyRowAvailable = true;
                this.ZoningDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.zoningDetailsData.ListZoning.AcceptChanges();
                if (this.ZoningDataGridDataTableRowCount > 0)
                {
                    for (int i = 0; i < this.ZoningDataGridDataTableRowCount; i++)
                    {
                        if (emptyRowAvailable)
                        {
                            if (((string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells[this.zoningDetailsData.ListZoning.ZoningCodeColumn.ColumnName].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells[this.zoningDetailsData.ListZoning.ZoningColumn.ColumnName].Value.ToString().Trim()))))
                            {
                                emptyRowAvailable = false;
                            }
                        }
                    }
                }

                return emptyRowAvailable;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// To verfiy empty rows available
        /// </summary>
        private void CheckEmptyRowsAvaliable()
        {
            if (this.ValidateEmptyRows())
            {
                this.SaveZoningDetails();
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.saveStatus = false;
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// To show message box to verfiy condition to save
        /// </summary>
        private void CloseButtonSaveMessage()
        {
            if (this.ZoningSaveButton.Enabled)
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + " ?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            try
                            {                                
                                this.CheckEmptyRowsAvaliable();
                                ////when save is made this form will close
                                if (this.saveStatus)
                                {
                                    this.Close();
                                }
                            }
                            catch (SoapException ex)
                            {
                                ////TODO : Need to find specific exception and handle it.
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                            }

                            break;
                        }

                    case DialogResult.No:
                        {
                            this.DialogResult = DialogResult.Cancel;
                            this.ZoningSaveButton.Enabled = false;
                            this.Close();
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            this.DialogResult = DialogResult.None;
                            break;
                        }
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #endregion Methods

        private void F3040_Load(object sender, EventArgs e)
        {
            try
            {
                this.CustimizeZoningGrid();
                this.LoadZoningForm();                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ZoningCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ZoningCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.EnableSelectButtons(true);
                this.ZoningCloseButton.Focus();
                this.LoadZoningGrid();
                this.ZoningCloseButton.Focus();
                this.ZoningDataGridView.Focus();
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ZoningCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.CloseButtonSaveMessage();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ZoningSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ZoningSaveButton_Click(object sender, EventArgs e)
        {
            try
            {                
                this.CheckEmptyRowsAvaliable();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ZoningDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                ////this is to check whether any unsaved changes exisits
                if (!this.enableButtonStatus)
                {
                    this.EnableSelectButtons(false);
                }
                else
                {
                    this.EnableSelectButtons(true);
                    this.enableButtonStatus = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ZoningDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.ZoningDataGridView.Columns[this.zoningDetailsData.ListZoning.ZoningCodeColumn.ColumnName].Index || e.ColumnIndex == this.ZoningDataGridView.Columns[this.zoningDetailsData.ListZoning.ZoningColumn.ColumnName].Index)
                {                    
                    this.enableButtonStatus = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ZoningDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.ZoningDataGridView.CurrentCell != null)
                {
                    this.ZoningDataGridView.CurrentCell.ReadOnly = true;
                    this.ZoningDataGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ZoningDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
                {
                    this.ZoningDataGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ZoningDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == 0)
                {
                    this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                    this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                    this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                }

                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.ZoningDataGridView["ZoningCode", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ZoningDataGridView["Zoning", (e.RowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < ZoningDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < ZoningDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells["ZoningCode"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells["Zoning"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                                this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                                this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.ZoningDataGridView["ZoningCode", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ZoningDataGridView["Zoning", e.RowIndex].Value.ToString().Trim())))
                                {
                                    this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = true;
                                    this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                                    this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                                    this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = true;
                            this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                        this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                        this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                    }
                }

                this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ZoningDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((((e.RowIndex) + 1) == this.ZoningDataGridView.Rows.Count) && ((e.ColumnIndex == 0)))
                {
                    if ((!string.IsNullOrEmpty(this.ZoningDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.ZoningDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim())))
                    {
                        this.zoningDetailsData.ListZoning.AddListZoningRow(this.zoningDetailsData.ListZoning.NewListZoningRow());
                        this.ZoningGridVerticalScroll.Visible = false;
                    }
                }


                if ((((e.RowIndex) + 1) == this.ZoningDataGridView.Rows.Count) && ((e.ColumnIndex == 1)))
                {
                    if ((!string.IsNullOrEmpty(this.ZoningDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.ZoningDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim())))
                    {
                        this.zoningDetailsData.ListZoning.AddListZoningRow(this.zoningDetailsData.ListZoning.NewListZoningRow());
                        this.ZoningGridVerticalScroll.Visible = false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ZoningDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////if (e.RowIndex == 0)
                ////{
                ////    this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                ////    this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                ////    this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                ////}

                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.ZoningDataGridView["ZoningCode", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ZoningDataGridView["Zoning", (e.RowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < ZoningDataGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < ZoningDataGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells["ZoningCode"].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.ZoningDataGridView.Rows[i].Cells["Zoning"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                                this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                                this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.ZoningDataGridView["ZoningCode", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ZoningDataGridView["Zoning", e.RowIndex].Value.ToString().Trim())))
                                {
                                    this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = true;
                                    this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                                    this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                                    this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = true;
                            this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                        this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                        this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                    }
                }

                if (e.RowIndex == 0)
                {
                    this.ZoningDataGridView["ZoningCode", e.RowIndex].ReadOnly = false;
                    this.ZoningDataGridView["Zoning", e.RowIndex].ReadOnly = false;
                    this.ZoningDataGridView.Rows[e.RowIndex].Selected = false;
                }

                if ((this.ZoningDataGridView["Zoning", e.RowIndex].Value != null))
                {
                    this.HeaderZoneLabel.Text = this.ZoningDataGridView.Rows[e.RowIndex].Cells["Zoning"].Value.ToString();

                    ////which will be sent to the attachment and comment button
                    int.TryParse(this.ZoningDataGridView.Rows[e.RowIndex].Cells["ZoningID"].Value.ToString(), out this.currentKeyId);

                    if (!this.ZoningSaveButton.Enabled)
                    {
                        this.SetAdditionalOperationCount(this.currentKeyId);
                    }
                    else
                    {
                        this.SetAdditionalOperationCount(this.currentKeyId);
                    }

                    if (this.currentKeyId > 0)
                    {
                        /////to set the audit link label text
                        this.ZoningIDAuditlinkLabel.Text = SharedFunctions.GetResourceString("tAA_Zoning[ZoningID]: ") + this.currentKeyId;
                        this.ZoningIDAuditlinkLabel.Visible = true;
                    }
                    else
                    {
                        this.ZoningIDAuditlinkLabel.Visible = false;
                    }
                }

                this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void F3040_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (this.ZoningSaveButton.Enabled)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + " ?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        try
                                        {                                            
                                            this.CheckEmptyRowsAvaliable();
                                            ////when save is made this form will close
                                            if (this.saveStatus)
                                            {
                                                this.DialogResult = DialogResult.Cancel;
                                                e.Cancel = false;
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }
                                        }
                                        catch (SoapException ex)
                                        {
                                            ////TODO : Need to find specific exception and handle it.
                                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                        }
                                        catch (Exception ex)
                                        {
                                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                                        }

                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.DialogResult = DialogResult.Cancel;
                                        e.Cancel = false;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        e.Cancel = true;
                                        this.DialogResult = DialogResult.None;
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ZoningDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            this.ZoningDataGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            ////Here the variable/method or whatever which causes the unsaved change to fire can be written
            this.EnableSelectButtons(false);
        }

        /// <summary>
        /// Handles the Click event of the AttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.currentFormId, this.currentKeyId, this.currentFormId };

                Form attachmentForm = new Form();
                //attachmentForm.Icon = @"D:\Workarea\TerraScan-T.ico";
                if (!this.ZoningSaveButton.Enabled)
                {

                    if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.currentFormId).openPermission))
                    {
                        attachmentForm = this.form3040Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.form3040Control.WorkItem);
                        attachmentForm.Tag = this.Tag;
                        if (attachmentForm != null)
                        {
                            attachmentForm.ShowDialog();

                            // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                            AdditionalOperationCountEntity additionalOperationCountEnt;
                            additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                            additionalOperationCountEnt.AttachmentCount = Convert.ToInt32(TerraScanCommon.GetValue(attachmentForm, "AttachmentCount"));
                            this.SetText(additionalOperationCountEnt);
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Unsaved changes exists"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void CommentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                
                object[] optionalParameter;

                if (!this.ZoningSaveButton.Enabled)
                {
                    if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.currentFormId).openPermission))
                    {
                        optionalParameter = new object[] { this.currentFormId, this.currentKeyId, 25003 };

                        Form commentForm = new Form();
                        commentForm = this.form3040Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.form3040Control.WorkItem);
                        commentForm.Tag = this.currentFormId;

                        if (commentForm != null)
                        {
                            commentForm.ShowDialog();

                            // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                            AdditionalOperationCountEntity additionalOperationCountEnt;
                            additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                            additionalOperationCountEnt.CommentCount = Convert.ToInt32(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                            additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                            this.SetText(additionalOperationCountEnt);
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Unsaved changes exists"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Sets the attachment and comments count.
        /// </summary>
        private void SetAdditionalOperationCount(int keyId)
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                ////check for valid registerid
                if (keyId > 0)
                {
                    additionalOperationCountEntity.AttachmentCount = this.form3040Control.WorkItem.GetAttachmentCount(this.currentFormId, keyId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form3040Control.WorkItem.GetCommentsCount(this.currentFormId, keyId, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }
                else
                {
                    additionalOperationCountEntity.AttachmentCount = 0;
                    additionalOperationCountEntity.CommentCount = 0;
                    additionalOperationCountEntity.HighPriority = false;
                }

                this.SetText(additionalOperationCountEntity);
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
        /// Sets the attachment and comments count text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
            ////if not -999 reset text
            if (additionalOperationCountEntity.AttachmentCount != -999)
            {
                if (additionalOperationCountEntity.AttachmentCount <= 0)
                {
                    this.AttachmentButton.Text = SharedFunctions.GetResourceString("Attachment");
                }
                else
                {
                    this.AttachmentButton.Text = string.Concat(SharedFunctions.GetResourceString("Attachment"), "(", additionalOperationCountEntity.AttachmentCount, ")");
                }
            }

            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentButton.Text = SharedFunctions.GetResourceString("Comment");
                }
                else
                {
                    this.CommentButton.Text = this.CommentButton.Text = string.Concat(SharedFunctions.GetResourceString("Comment"), "(", additionalOperationCountEntity.CommentCount, ")");
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    ////red color for high priority 
                    this.CommentButton.BackColor = Color.FromArgb(255, 0, 0);
                    this.CommentButton.CommentPriority = true;
                }
                else
                {
                    ////default brown color
                    this.CommentButton.BackColor = Color.FromArgb(174, 150, 94);
                    this.CommentButton.CommentPriority = false;
                }
            }
        }


        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.ZoningCloseButton.Focus();
                this.CheckEmptyRowsAvaliable();
                this.ZoningCloseButton.Focus();                
                this.ZoningDataGridView.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
      
        /////// <summary>
        /////// Handles the Click event of the SaveToolStripMenuItem control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        ////private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        this.NameTextBox.Focus();
        ////        ////this.SaveGenericElement();
        ////        this.CheckEmptyRowsAvaliable();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        ////    }
        ////}
    }
}