////--------------------------------------------------------------------------------------------
//// <copyright file="F2409.cs" company="Congruent">
//// Copyright (c) Congruent Info-Tech.  All rights reserved.
//// </copyright>
//// <summary>
//// This file contains methods for the Parcel Review Status.
//// </summary>
////----------------------------------------------------------------------------------------------
//// Change History
////***********************************************************************************************
//// Date               Author         Description
//// ----------         ---------      ---------------------------------------------------------
//// 13/07/2009        R.Malliga       Created
////**********************************************************************************************

namespace D20000
{
    #region namespace

    using System;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;

    #endregion namespace

    /// <summary>  
    /// Review Status  
    /// </summary>  
    public partial class F2409 : Form
    {
        #region Variables
        /// <summary>
        /// controller F3510Controller
        /// </summary>
        private F2409Controller form2409Control;

        /// <summary>
        /// Used to store TypedDataset(F2409ReviewStatusData) Value
        /// </summary>
        private F2409ReviewStatusData reviewStatusData = new F2409ReviewStatusData();

        /// <summary>
        /// Used to store TypedDataset(F2409ReviewStatusData) Value
        /// </summary>
        private F2409ReviewStatusData enteredStatusData = new F2409ReviewStatusData();

        /// <summary>
        /// Used to Store ParcelId
        /// </summary>
        private int parcelId;
     
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="F2409"/> class.
        /// </summary>
        public F2409()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F2409"/> class.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        public F2409(int parcelId)
        {
            this.InitializeComponent();
            this.parcelId = parcelId; 
        }
        #endregion

        #region Property
        
        /// <summary>
        /// Gets or sets the form2409 control.
        /// </summary>
        /// <value>The form2409 control.</value>
        [CreateNew]
        public F2409Controller Form2409Control
        {
            get { return this.form2409Control as F2409Controller; }
            set { this.form2409Control = value; }
        }
        #endregion
        
        #region Button Events
        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////To Call Save Method
                this.SaveReviewStatus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Date Events
        /// <summary>
        /// Handles the Click event of the InspectedByDateCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InspectedByDateCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                ////Calling Timer Image Method
                this.TimerImage_Click(this.InspectedByDateTextBox, this.InspectedByDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the InspectedByDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InspectedByDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.InspectedByDateTextBox.Text = this.InspectedByDateTimePicker.Text;
                this.InspectedByDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the CloseUp event of the EnteredBydateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnteredDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.EnteredDateTextBox.Text = this.EnteredDateTimePicker.Text;
                this.EnteredDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the ReviewedBydateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReviewedBydateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.ReviewedByDateTextBox.Text = this.ReviewedBydateTimePicker.Text;
                this.ReviewedByDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReviewedByCaldendarButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReviewedByCaldendarButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////Calling Timer Image Method
                this.TimerImage_Click(this.ReviewedByDateTextBox, this.ReviewedBydateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the Click event of the EnteredByCaldendarButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnteredDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                ////Calling Timer Image Method
                this.TimerImage_Click(this.EnteredDateTextBox, this.EnteredDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region TimePicker_KeyPress
        
        /// <summary>
        /// Handles the KeyPress event of the TimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void TimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ////For ESC Key
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send(SharedFunctions.GetResourceString("ESC"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion TimePicker_KeyPress

        #region TimerImage_Click
        
        /// <summary>
        /// Timers the image_ click.
        /// </summary>
        /// <param name="textControl">The text control.</param>
        /// <param name="timePickerControl">The time picker control.</param>
        private void TimerImage_Click(TextBox textControl, DateTimePicker timePickerControl)
        {
           try
            {
                timePickerControl.BringToFront();
               ////to set Datetimepicker control 
                if (!string.IsNullOrEmpty(textControl.Text.Trim()))
                {
                    timePickerControl.Value = Convert.ToDateTime(textControl.Text);
                }
                else
                {
                    timePickerControl.Value = DateTime.Today;
                }
                timePickerControl.Visible = true; 
                this.SaveButton.Enabled = true; 
                SendKeys.Send(SharedFunctions.GetResourceString("F4"));
                timePickerControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion TimerImage_Click

        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F2409 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F2409_Load(object sender, EventArgs e)
        {
          try
          {
              ////Populate Combo Values
              this.PopulateComboValue(); 
             ////To Populate Form Control Values.
              this.PopulateControlValues();
             // this.SaveButton.Enabled = false;
              
           }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Populates the combo value.
        /// </summary>
        private void PopulateComboValue()
        {
            ////Populate InspectionByUser Combobox.
            this.reviewStatusData = this.form2409Control.WorkItem.F2409_ReviewstatusInspectionByUser(TerraScanCommon.ApplicationId);
            this.InspectedByComboBox.DataSource = this.reviewStatusData.F2409_ListInspectedByUser;
            this.InspectedByComboBox.DisplayMember = this.reviewStatusData.F2409_ListInspectedByUser.Name_DIsplayColumn.ColumnName;
            this.InspectedByComboBox.ValueMember = this.reviewStatusData.F2409_ListInspectedByUser.UserIDColumn.ColumnName;
            this.InspectedByComboBox.SelectedIndex = 0;

            ////Populate ReviewByUser Combobox.
            this.reviewStatusData = this.form2409Control.WorkItem.F2409_ReviewstatusInspectionByUser(TerraScanCommon.ApplicationId);
            this.ReviewedByComboBox.DataSource = this.reviewStatusData.F2409_ListInspectedByUser;
            this.ReviewedByComboBox.DisplayMember = this.reviewStatusData.F2409_ListInspectedByUser.Name_DIsplayColumn.ColumnName;
            this.ReviewedByComboBox.ValueMember = this.reviewStatusData.F2409_ListInspectedByUser.UserIDColumn.ColumnName;
            this.ReviewedByComboBox.SelectedIndex = 0;

            ////To Populate ReviewInspectionType Comobobox.
            this.reviewStatusData = this.form2409Control.WorkItem.F2409_ReviewstatusInspectionType();
            this.InspectionTypeComboBox.DataSource = this.reviewStatusData.F2409_InspectedType;
            this.InspectionTypeComboBox.DisplayMember = this.reviewStatusData.F2409_InspectedType.InspectionTypeColumn.ColumnName;
            this.InspectionTypeComboBox.ValueMember = this.reviewStatusData.F2409_InspectedType.InspectionTypeIDColumn.ColumnName;
            this.InspectionTypeComboBox.SelectedIndex = 0;

            /////To populate EnteredBy Combobox
            this.enteredStatusData = this.form2409Control.WorkItem.F2409_ReviewstatusInspectionByUser(TerraScanCommon.ApplicationId);
            this.EnteredByComboBox.DataSource = this.enteredStatusData.F2409_ListInspectedByUser;
            this.EnteredByComboBox.DisplayMember = this.enteredStatusData.F2409_ListInspectedByUser.Name_DIsplayColumn.ColumnName;
            this.EnteredByComboBox.ValueMember = this.enteredStatusData.F2409_ListInspectedByUser.UserIDColumn.ColumnName;
            this.EnteredByComboBox.SelectedIndex = 0;
            ///To Populate ReviewInspectionType Comobobox.
            this.reviewStatusData = this.form2409Control.WorkItem.F2409_ReviewStatus();
            this.ReviewStatusComboBox.DataSource = this.reviewStatusData.F2409_ReviewStatus;
            this.ReviewStatusComboBox.DisplayMember = this.reviewStatusData.F2409_ReviewStatus.ReviewStatusColumn.ColumnName;
            this.ReviewStatusComboBox.ValueMember = this.reviewStatusData.F2409_ReviewStatus.ReviewStatusIDColumn.ColumnName;
            this.ReviewStatusComboBox.SelectedIndex = 0;
           
        }
        
        /// <summary>
        /// Saves the review status.
        /// </summary>
        private void SaveReviewStatus()
        {
            try
            {
                bool saveFailure = false;
                ////assign all control values to datatable
                this.Cursor = Cursors.WaitCursor;   
                F2409ReviewStatusData listReviewStatusData = new F2409ReviewStatusData();
                listReviewStatusData.F2409_ListReviewStatus.Columns.Add("ParcelID");
                F2409ReviewStatusData.F2409_ListReviewStatusRow reviewStatusRow = listReviewStatusData.F2409_ListReviewStatus.NewF2409_ListReviewStatusRow();

                if (this.InspectedByComboBox.SelectedIndex > 0)
                {
                    reviewStatusRow.InspectedByUserID = this.InspectedByComboBox.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(this.InspectedByDateTextBox.Text.Trim()))
                {
                    var dateString = this.InspectedByDateTextBox.Text;
                    DateTime result;
                    bool success = DateTime.TryParse(dateString, out result);
                    if (success)
                    {
                        reviewStatusRow.DateInspected = this.InspectedByDateTextBox.Text.Trim();
                    }
                    else
                    {
                        saveFailure = true; 
                    }


                }

                if (this.InspectionTypeComboBox.SelectedIndex > 0)
                {
                    reviewStatusRow.InspectionTypeID = this.InspectionTypeComboBox.SelectedValue.ToString();
                }

                if (this.ReviewedByComboBox.SelectedIndex > 0)
                {
                    reviewStatusRow.ReviewedByUserID = this.ReviewedByComboBox.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(this.ReviewedByDateTextBox.Text.Trim()))
                {
                    var dateString = this.ReviewedByDateTextBox.Text;
                    DateTime result;
                    bool success = DateTime.TryParse(dateString, out result);
                    if (success)
                    {
                        reviewStatusRow.ReviewDate = this.ReviewedByDateTextBox.Text.Trim();
                    }
                    else
                    {
                        saveFailure = true;
                    }
                }
                if (this.ReviewStatusComboBox.SelectedIndex > 0)
                {
                    reviewStatusRow.ReviewStatusID = this.ReviewStatusComboBox.SelectedValue.ToString();
                }
                if (this.EnteredByComboBox.SelectedIndex > 0)
                {
                  reviewStatusRow.EnteredByUserID = this.EnteredByComboBox.SelectedValue.ToString();    
                }
                if(!string.IsNullOrEmpty(this.EnteredDateTextBox.Text.Trim()))
                {
                    var dateString = this.EnteredDateTextBox.Text;
                    DateTime result;
                    bool success = DateTime.TryParse(dateString, out result);
                    if (success)
                    {
                        reviewStatusRow.EnteredByDate = this.EnteredDateTextBox.Text.Trim();
                    }
                    else
                    {
                        saveFailure = true;
                    }
                }

                reviewStatusRow["ParcelID"] = this.parcelId;
                listReviewStatusData.F2409_ListReviewStatus.Rows.Add(reviewStatusRow);
                ////DB Call for save
                if (!saveFailure)
                {
                    this.form2409Control.WorkItem.F2409UpdateParcelReviewDetails(TerraScanCommon.GetXmlString(listReviewStatusData.F2409_ListReviewStatus.Copy()), TerraScanCommon.UserId);
                    ////After saving Populate values to controls.
                    this.PopulateControlValues();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Date Format - Save Operation Aborted", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the control values.
        /// </summary>
        private void PopulateControlValues()
        {
            try
            {
                ////Calling DB for the parcelId review Status Details
                this.reviewStatusData = this.form2409Control.WorkItem.F2409_ListReviewstatus(Convert.ToInt32(this.parcelId));
                ////if review status detail datatable rowcount is greater than Zero then do the following lines. 
                if (this.reviewStatusData.F2409_ListReviewStatus.Rows.Count > 0)
                {
                    this.InspectedByComboBox.SelectedValue = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.InspectedByUserIDColumn.ColumnName].ToString();
                    this.InspectedByDateTextBox.Text = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.DateInspectedColumn.ColumnName].ToString();
                    this.EnteredByComboBox.SelectedValue = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.EnteredByUserIDColumn.ColumnName].ToString();
                    this.InspectionTypeComboBox.SelectedValue = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.InspectionTypeIDColumn.ColumnName].ToString();
                    this.ReviewedByComboBox.SelectedValue = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.ReviewedByUserIDColumn.ColumnName].ToString();
                    this.ReviewedByDateTextBox.Text = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.ReviewDateColumn.ColumnName].ToString();
                    this.ReviewStatusComboBox.SelectedValue = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.ReviewStatusIDColumn.ColumnName].ToString();
                    this.EnteredDateTextBox.Text = this.reviewStatusData.F2409_ListReviewStatus.Rows[0][this.reviewStatusData.F2409_ListReviewStatus.EnteredByDateColumn.ColumnName].ToString();
                }
                else
                {
                    this.ClearControlValues(); 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Clears the control values.
        /// </summary>
        private void ClearControlValues()
        {
            try
            {
            //// To Clear All Control Values
            this.InspectedByDateTextBox.Text = string.Empty;
            this.ReviewedByDateTextBox.Text = string.Empty;
            this.EnteredDateTextBox.Text = string.Empty;
            this.EnteredByComboBox.SelectedIndex = 0;
            this.ReviewStatusComboBox.SelectedIndex = 0;  
            this.InspectedByComboBox.SelectedIndex = 0;
            this.ReviewedByComboBox.SelectedIndex = 0;
            this.InspectionTypeComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Save Method
        /// <summary>
        /// Handles the Click event of the SaveToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveReviewStatus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Combo Events
        /// <summary>
        /// Handles the Validating event of the InspectedByComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void InspectedByComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.InspectedByComboBox.SelectedValue == null)
                {
                    this.InspectedByComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the InspectionTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void InspectionTypeComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.InspectionTypeComboBox.SelectedValue == null)
                {
                    this.InspectionTypeComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the ReviewedByComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ReviewedByComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.ReviewedByComboBox.SelectedValue == null)
                {
                    this.ReviewedByComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        

        /// <summary>
        /// Handles the Validating event of the EnteredByComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void EnteredByComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.EnteredByComboBox.SelectedValue == null)
                {
                    this.EnteredByComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the ReviewStatusComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ReviewStatusComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (this.ReviewStatusComboBox.SelectedValue == null)
                {
                    this.ReviewStatusComboBox.SelectedIndex = 0;
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
