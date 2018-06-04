//--------------------------------------------------------------------------------------------
// <copyright file="F2204.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved. 
// </copyright>
// <summary>
//	This file contains UI for F2204 Form  - ScheduleCopy
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11/09/07         D.LathaMaheswari   Created
//*********************************************************************************/

namespace D20050
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.IO;
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
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F2204
    /// </summary>
    public partial class F2204 : Form
    {
        #region variable

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// controller F1503Controller
        /// </summary>
        private F2204Controller formF2204Control;

        /// <summary>
        /// Store rollingObject combo box selected Index
        /// </summary>
        private int rollingObjectIndex;

        /// <summary>
        /// to check file Exist
        /// </summary>        
        private bool requiredField;

        /// <summary>
        /// copyFormLoad
        /// </summary>        
        private bool copyFormLoad;

        /// <summary>
        /// Store returnValue 
        /// </summary>
        private int returnValue;

        /// <summary>
        /// createButton
        /// </summary>        
        private bool createButton;

        /// <summary>
        /// Created string for filePath
        /// </summary>
        private string filePath = string.Empty;

        /// <summary>
        /// Created string for fileID
        /// </summary>
        private string fileID = string.Empty;

        /// <summary>
        /// keyDown
        /// </summary>
        private bool keyDown;

        /// <summary>
        /// Store ParcelType combo box selected Index
        /// </summary>
        private int parcelTypeIndex;

        /// <summary>
        /// AssessmentTypeDataset
        /// </summary>
        private F2204CopyScheduleData.f25050_AssessmentTypeDataTable assessmentTypeData = new F2204CopyScheduleData.f25050_AssessmentTypeDataTable();
        
        /// <summary>
        /// ScheduleTypeDataset
        /// </summary>
        private F2204CopyScheduleData.f25050_ScheduleTypeDataTable scheduleTypeData = new F2204CopyScheduleData.f25050_ScheduleTypeDataTable();

        /// <summary>
        /// editScheduledata
        /// </summary>
        private F2200EditScheduleData editScheduleData = new F2200EditScheduleData();

        /// <summary>
        /// scheduleNumber
        /// </summary>
        private string scheduleNumber;

        /// <summary>
        /// scheduleYear
        /// </summary>
        private string scheduleYear;

        #endregion variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2204"/> class.
        /// </summary>
        public F2204()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2204"/> class.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        public F2204(int scheduleId)
        {
            InitializeComponent();
            this.keyId = scheduleId;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Properties
        /// <summary>
        /// For F2004Controller
        /// </summary>
        [CreateNew]
        public F2204Controller Form2204Control
        {
            get { return this.formF2204Control as F2204Controller; }
            set { this.formF2204Control = value; }
        }
        #endregion Properties

        #region Event

        /// <summary>
        /// F2004_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F2204_Load(object sender, EventArgs e)
        {
            try
            {
                this.copyFormLoad = true;
                this.FillParcelTypeCombo();
                this.FillComboBoxes();
                this.GetscheduleNumber();
                this.CloseButton.Visible = true;
                this.CancelButton = this.CloseButton;
                this.copyFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelTypeComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// NonRollingObjectComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CopyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CommentsComboBox4_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CommentsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CancelButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CancelCommandButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ScheduleTypeComboBox.SelectedValue = this.parcelTypeIndex;
                this.CopyComboBox.SelectedIndex = this.rollingObjectIndex;
                this.GetscheduleNumber();
                this.CancelCommandButton.Enabled = false;
                this.ScheduleNumberTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CreateButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.createButton = true;
                this.CreateStatement();
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
        /// HelplinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(0);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// scheduleNumberLabel1_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ScheduleHeaderLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (ScheduleHeaderLabel.Text.Length > 16)
                {
                    this.ScheduleCopyToolTip.SetToolTip(this.ScheduleHeaderLabel, this.ScheduleHeaderLabel.Text + " " + SharedFunctions.GetResourceString("Separator") + " " + this.ScheduleHeaderYearLabel.Text);
                }
                else
                {
                    this.ScheduleCopyToolTip.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// scheduleNumberTextBox_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 46 || e.KeyValue == 86)
                {
                    this.CancelCommandButton.Enabled = true;
                }

                this.keyDown = e.Control;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// F2004_FormClosing
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F2204_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.createButton)
                {
                    if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                    {
                        if (e.CloseReason.Equals(CloseReason.UserClosing))
                        {
                            if (this.CancelCommandButton.Enabled)
                            {
                                switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", "schedule copy", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    case DialogResult.Yes:
                                        {
                                            this.CreateStatement();
                                            if (this.requiredField)
                                            {
                                               //// this.DialogResult = DialogResult.Cancel;
                                                e.Cancel = false;
                                            }
                                            else
                                            {
                                                e.Cancel = true;
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
                                            this.DialogResult = DialogResult.None;
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
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ScheduleNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.copyFormLoad)
                {
                    this.CancelCommandButton.Enabled = true;
                }
                else
                {
                    this.CancelCommandButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.copyFormLoad)
                {
                    this.CancelCommandButton.Enabled = true;
                }
                else
                {
                    this.CancelCommandButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AssessmentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssessmentTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Event

        #region Methods

        /// <summary>
        /// FillTypeCombo
        /// </summary>
        private void FillComboBoxes()
        {
            this.CopyComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.CopyComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.CopyComboBox.SelectedIndex = 1;
            this.rollingObjectIndex = this.CopyComboBox.SelectedIndex;
        }

        /// <summary>
        /// FillParcelTypeCombo
        /// </summary>
        private void FillParcelTypeCombo()
        {
            this.assessmentTypeData = this.formF2204Control.WorkItem.F25050GetParcelTypeDetails();
            if (this.assessmentTypeData.Rows.Count > 0)
            {
                this.AssessmentTypeComboBox.DisplayMember = this.assessmentTypeData.AssessmentTypeColumn.ColumnName;
                this.AssessmentTypeComboBox.ValueMember = this.assessmentTypeData.AssessmentTypeIDColumn.ColumnName;
                this.AssessmentTypeComboBox.DataSource = this.assessmentTypeData;
            }
            
            this.scheduleTypeData = this.formF2204Control.WorkItem.F25050GetScheduleTypeDetails();
            if (this.scheduleTypeData.Rows.Count > 0)
            {
                this.ScheduleTypeComboBox.DataSource = this.scheduleTypeData;
                this.ScheduleTypeComboBox.DisplayMember = this.scheduleTypeData.ScheduleTypeColumn.ColumnName;
                this.ScheduleTypeComboBox.ValueMember = this.scheduleTypeData.ScheduleTypeIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// GetscheduleNumber
        /// </summary>
        private void GetscheduleNumber()
        {
            this.editScheduleData = this.formF2204Control.WorkItem.F2200_ListEditScheduleDetails(this.keyId);
            if (this.editScheduleData.f2200ListScheduleDataTable.Rows.Count > 0)
            {
                this.ScheduleHeaderLabel.Text = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.ScheduleNumberColumn.ColumnName].ToString();
                this.ScheduleHeaderYearLabel.Text = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.RollYearColumn.ColumnName].ToString();
                this.ScheduleNumberTextBox.Text = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.ScheduleNumberColumn.ColumnName].ToString();
                this.scheduleNumber = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.ScheduleNumberColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.RollYearColumn.ColumnName].ToString();
                this.scheduleYear = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.RollYearColumn.ColumnName].ToString();
                this.AssessmentTypeComboBox.Text = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.AssessmentTypeColumn.ColumnName].ToString();
                this.ScheduleTypeComboBox.Text = this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.ScheduleTypeColumn.ColumnName].ToString();
            }
        }

        /// <summary>
        /// Createparcel
        /// </summary>
        private void CreateStatement()
        {
            if (!string.IsNullOrEmpty(this.ScheduleNumberTextBox.Text.Trim()))
            {
                this.requiredField = true;
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.requiredField = false;
                this.createButton = false;
                this.ScheduleNumberTextBox.Focus();
                return;
            }

            int rollYear = 0;
            int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
            if (rollYear != 0)
            {
                if (!this.createButton)
                {
                    if (rollYear <= 1899 || rollYear >= 2080)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.requiredField = false;
                        this.RollYearTextBox.Text = "0";
                        this.RollYearPanel.BackColor = Color.Yellow;
                        this.RollYearTextBox.BackColor = Color.Yellow;
                        this.RollYearTextBox.Focus();
                    }
                    else
                    {
                        this.requiredField = true;
                    }
                }
                else if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    this.requiredField = true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.requiredField = false;
                    this.createButton = false;
                    this.RollYearPanel.BackColor = Color.Yellow;
                    this.RollYearTextBox.BackColor = Color.Yellow;
                    this.RollYearTextBox.Focus();
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.requiredField = false;
                this.createButton = false;
                this.RollYearPanel.BackColor = Color.Yellow;
                this.RollYearTextBox.BackColor = Color.Yellow;
                this.RollYearTextBox.Focus();
            }

            if (this.requiredField)
            {
                this.CreateNewStatement();
                ////this.CancelCommandButton.Enabled = false;
            }
        }

        /// <summary>
        /// To create a parcel Copy
        /// </summary>
        private void CreateNewStatement()
        {
            ////if ((this.scheduleNumber == this.ScheduleNumberTextBox.Text.Trim()) && (this.scheduleYear == this.RollYearTextBox.Text.Trim()) && (this.parcelTypeIndex == Convert.ToInt32(this.ScheduleTypeComboBox.SelectedValue.ToString())))
            ////{
            ////    MessageBox.Show(SharedFunctions.GetResourceString("ScheduleRecordStatus"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
            ////else
            ////{
                F2204CopyScheduleData.f25050_CreateCopyScheduleDataTable createNewSchedule = new F2204CopyScheduleData.f25050_CreateCopyScheduleDataTable();
                F2204CopyScheduleData.f25050_CreateCopyScheduleRow dr = createNewSchedule.Newf25050_CreateCopyScheduleRow();
                dr.ScheduleNumber = this.ScheduleNumberTextBox.Text.Trim();
                dr.RollYear = this.RollYearTextBox.Text.Trim();
                dr.AssessmentTypeID = Convert.ToInt32(this.AssessmentTypeComboBox.SelectedValue.ToString());
                dr.ScheduleTypeID = Convert.ToInt32(this.ScheduleTypeComboBox.SelectedValue.ToString());
                dr.CopyLineItem = this.CopyComboBox.SelectedItem.ToString();
                createNewSchedule.Rows.Add(dr);
                string scheduleIems = TerraScanCommon.GetXmlString(createNewSchedule);

                this.returnValue = this.formF2204Control.WorkItem.F2204CreateNewScheduleCopy(this.keyId, scheduleIems, TerraScanCommon.UserId);

                ////Coding added for the CO 4781 by malliga on 13/11/2009
                if (this.returnValue.Equals(-1))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ScheduleRecordStatus"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   //// this.CancelCommandButton.Enabled = true;
                    this.createButton = false;
                    this.DialogResult = DialogResult.None;
                    return;
                }
                ////this.CancelCommandButton.Enabled = false;
                this.DialogResult = DialogResult.Cancel;
                if (MessageBox.Show(SharedFunctions.GetResourceString("ScheduleCopy"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (this.createButton)
                    {
                        this.Close();
                    }
                }
           //// }
        }

        /// <summary>
        /// SetEditRecord
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetEditRecord(object sender, EventArgs e)
        {
            if (!this.copyFormLoad)
            {
                this.CancelCommandButton.Enabled = true;
              }
            else
            {
                this.CancelCommandButton.Enabled = false;
            }
        }

        /// <summary>
        /// SetKeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.keyDown == true)
                {
                    if ((e.KeyChar == 'v') || (e.KeyChar == 24) || (e.KeyChar == 26))
                    {
                        this.CancelCommandButton.Enabled = true;
                    }
                    else if ((e.KeyChar == 3) && !this.CancelCommandButton.Enabled)
                    {
                        this.CancelCommandButton.Enabled = false;
                    }
                }
                else
                {
                    this.CancelCommandButton.Enabled = true;
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