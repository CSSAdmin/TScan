// --------------------------------------------------------------------------------------------
// <copyright file="F2205.cs" company="Congruent">
//   Copyright (c) Congruent Info-Tech.  All rights reserved. 
// </copyright>
// <summary>
//  This file contains UI for F2205 Form  - ScheduleCopy
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author            Description
// ----------       ---------         ---------------------------------------------------------
// 16/07/09         D.LathaMaheswari   Created
// *********************************************************************************/

namespace D20050
{
    using System;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;

    /// <summary>
    /// F2205 form
    /// </summary>
    public partial class F2205 : Form
    {
        #region Variable

        /// <summary>
        /// The key Id
        /// </summary>
        private int keyId;

        /// <summary>
        /// controller F2205Controller
        /// </summary>
        private F2205Controller formF2205Control;

        /// <summary>
        /// Owner Id from Master name search
        /// </summary>
        private string ownerId;

        /// <summary>
        /// Owner Name from Master name search
        /// </summary>
        private string ownerName;

        /// <summary>
        /// Schedule Id from Schedule search form
        /// </summary>
        private string existScheduleId;

        /// <summary>
        /// Schedule Number from Schedule search form
        /// </summary>
        private string existScheduleNumber;

        /// <summary>
        /// District Id from District selection form
        /// </summary>
        private string districtId;

        /// <summary>
        /// District Number from District selection form
        /// </summary>
        private string districtNumber;

        /// <summary>
        /// Assessment Type Dataset
        /// </summary>
        private F2204CopyScheduleData.f25050_AssessmentTypeDataTable assessmentTypeData = new F2204CopyScheduleData.f25050_AssessmentTypeDataTable();

        /// <summary>
        /// Schedule Type Dataset
        /// </summary>
        private F2204CopyScheduleData.f25050_ScheduleTypeDataTable scheduleTypeData = new F2204CopyScheduleData.f25050_ScheduleTypeDataTable();

        /// <summary>
        /// Schedule Header data
        /// </summary>
        private F2200EditScheduleData scheduleHeaderData = new F2200EditScheduleData();

        /// <summary>
        /// XMLstring for schedult header table
        /// </summary>
        private string scheduleHeaderItems;

        /// <summary>
        /// XMLString for Schedule Line Items
        /// </summary>
        private string scheduleLineItems; 

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F2205"/> class.
        /// </summary>
        public F2205()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F2205"/> class.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        public F2205(int scheduleId)
        {
            InitializeComponent();
            this.keyId = scheduleId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F2205"/> class.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        public F2205(int scheduleId, string scheduleItems)
        {
            InitializeComponent();
            this.keyId = scheduleId;
            this.scheduleLineItems = scheduleItems;
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
        /// Gets or sets the form2205 control.
        /// </summary>
        /// <value>The form2205 control.</value>
        [CreateNew]
        public F2205Controller Form2205Control
        {
            get { return this.formF2205Control as F2205Controller; }
            set { this.formF2205Control = value; }
        }

        #endregion Properties

        #region Events

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F2205 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F2205_Load(object sender, EventArgs e)
        {
            try
            {
                // Bind Yes,No Combo
                this.FillComboBoxes(this.ExistScheduleComboBox, 0);
                this.FillComboBoxes(this.HouseholdComboBox, 1);
                this.FillComboBoxes(this.ExemptComboBox, 0);
               
                // Bind Assessment and ScheduleType Combo
                this.FillScheduleCombo();
                
                // Get Schedule Details
                this.GetscheduleDetails();
               
                // Enable/Disable controls
                this.EnableControls(false);
                
                // Set cancel button
                this.CancelButton = this.CancelCommandButton;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #endregion Form Load

        #region ToolTip

        /// <summary>
        /// Handles the MouseHover event of the ScheduleHeaderLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleHeaderLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //// Show ToolTip 
                if (ScheduleHeaderLabel.Text.Length > 16)
                {
                    this.MoveScheduleToolTip.SetToolTip(this.ScheduleHeaderLabel, this.ScheduleHeaderLabel.Text + " " + SharedFunctions.GetResourceString("Separator") + " " + this.ScheduleHeaderYearLabel.Text);
                }
                else
                {
                    this.MoveScheduleToolTip.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ToolTip

        #region Help Link

        /// <summary>
        /// Handles the Click event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HelpLink_Click(object sender, EventArgs e)
        {
            try
            {
                ////FormInfo formInfo;
                ////formInfo = TerraScanCommon.GetFormInfo(0);
                ////formInfo.optionalParameters = new object[1];
                ////formInfo.optionalParameters[0] = this.keyId;
                //////// Open Help form
                ////this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                ////this.Cursor = Cursors.Arrow;
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Help Link

        #region Combo Events

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ExistScheduleComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ExistScheduleComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.ExistScheduleComboBox.Text.Equals(SharedFunctions.GetResourceString("Yes")))
                {
                    // Diable all controls except Exist schedule number
                    this.EnableControls(true);
                }
                else if (this.ExistScheduleComboBox.Text.Equals(SharedFunctions.GetResourceString("No")))
                {
                    // Enable all controls except Exist schedule number
                    this.EnableControls(false);
                    
                    // Set values on appropriate controls
                    this.BindValues();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Combo Events

        #region ScheduleOwner Button Event

        /// <summary>
        /// Handles the Click event of the OwnerPicture control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerPicture_Click(object sender, EventArgs e)
        {
            try
            {
                Form masterNameSearchForm = new Form();
                object[] optionalParameter = new object[0];
                masterNameSearchForm = this.formF2205Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, optionalParameter, this.formF2205Control.WorkItem);
                if (masterNameSearchForm != null)
                {
                    if (masterNameSearchForm.ShowDialog() == DialogResult.Yes)
                    {
                        // Get selected owner details
                        this.ownerId = TerraScanCommon.GetValue(masterNameSearchForm, "MasterNameOwnerId");
                        this.ownerName = TerraScanCommon.GetValue(masterNameSearchForm, "CommandValue");
                        this.OwnerTextBox.Text = this.ownerName;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ScheduleOwner Button Event

        #region District Button Event

        /// <summary>
        /// Handles the Click event of the DistrictButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DistrictButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form districtSelectionForm = new Form();
                object[] optionalParameter = new object[] { this.RollYearTextBox.Text.Trim(), 2205 };
                districtSelectionForm = TerraScanCommon.GetForm(1512, optionalParameter, this.formF2205Control.WorkItem);

                if (districtSelectionForm != null)
                {
                    if (districtSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        // Get selected district details
                        this.districtId = TerraScanCommon.GetValue(districtSelectionForm, "DistrictId");
                        this.districtNumber = TerraScanCommon.GetValue(districtSelectionForm, "CommandValue");
                        this.DistrictTextBox.Text = this.districtNumber;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion  District Button Event

        #region Schedule Button Event

        /// <summary>
        /// Handles the Click event of the ScheduleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form scheduleSelectionForm = new Form();
                object[] optionalParameter = new object[] { this.RollYearTextBox.Text.Trim() };
                scheduleSelectionForm = this.formF2205Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1402, optionalParameter, this.formF2205Control.WorkItem);
                if (scheduleSelectionForm != null)
                {
                    if (scheduleSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        // Get selected schedule details
                        this.existScheduleId = TerraScanCommon.GetValue(scheduleSelectionForm, "ScheduleId");
                        this.existScheduleNumber = TerraScanCommon.GetValue(scheduleSelectionForm, "CommandValue");

                        if (this.existScheduleId.Equals(this.keyId.ToString()))
                        {
                            // Show messagebox when select same scheduleid
                            MessageBox.Show(SharedFunctions.GetResourceString("SameScheduleMessage"),
                                            SharedFunctions.GetResourceString("MoveScheduleLineHeader"), 
                                            MessageBoxButtons.OK, 
                                            MessageBoxIcon.Information);
                           
                            // Remove the existing value presents in the textbox
                            this.ExistScheduleTextBox.Text = string.Empty;
                           
                            // Set focus on schedule button to select another schedule
                            this.ScheduleButton.Focus();
                        }
                        else
                        {
                            this.ExistScheduleTextBox.Text = this.existScheduleNumber;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Schedule Button Event

        #region Cancel Button

        /// <summary>
        /// Handles the Click event of the CancelCommandButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelCommandButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Close the form
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Cancel Button

        #region Create Button

        /// <summary>
        /// Handles the Click event of the CreateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ExistScheduleComboBox.Text.Equals(SharedFunctions.GetResourceString("Yes")))
                {
                    // If the Schedule already exists (Does the schedule exist? - Yes)
                    // Required Field validation for Existing Schedule Number 
                    if (!this.IsRequiredField(this.ExistScheduleTextBox))
                    {
                        this.ScheduleButton.Focus();
                        return;
                    }

                    DialogResult dialogResult;
                    dialogResult = MessageBox.Show("Do you want to move these items?", "TerraScan T2 - Move Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (dialogResult.Equals(DialogResult.Yes))
                    {
                        int scheduleId;
                        int.TryParse(this.existScheduleId, out scheduleId);
                        
                        // Move schedule line items to exist schedule
                        int returnValue = this.formF2205Control.WorkItem.F2205CreateSchedule(scheduleId, false, null, this.scheduleLineItems, TerraScanCommon.UserId);
                        
                        // show confirmation messgae based on return value from DataBase
                        this.ShowMessage(returnValue);
                    }
                }
                else
                {
                    // If the Schedule is New (Does the schedule exist? - No)
                    // Required Field validation for NewScheduleNumber, RollYear, ScheduleType and AssessmntType
                    if (!this.IsRequiredField(this.NewScheduleTextBox))
                    {
                        return;
                    }
                    else if (!this.IsRequiredField(this.RollYearTextBox))
                    {
                        return;
                    }
                    else if (!this.IsRequiredField(this.ScheduleTypeComboBox))
                    {
                        return;
                    }
                    else if (!this.IsRequiredField(this.AssessmentTypeComboBox))
                    {
                        return;
                    }

                    DialogResult dialogResult;
                    dialogResult = MessageBox.Show("Do you want to move these items?", "TerraScan T2 - Move Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult.Equals(DialogResult.Yes))
                    {
                        // Get shedule header details
                        this.GetScheduleHeaderDetails();

                        // Move schedule line items to new schedule id
                        int returnValue = this.formF2205Control.WorkItem.F2205CreateSchedule(null, true, this.scheduleHeaderItems, this.scheduleLineItems, TerraScanCommon.UserId);

                        // show confirmation messgae based on return value from DataBase
                        this.ShowMessage(returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Create Button
                
        #endregion Events

        #region Methods

        #region Fill Yes/No Combo

        /// <summary>
        /// Fills the combo boxes.
        /// </summary>
        /// <param name="fillCombo">The fill combo.</param>
        /// <param name="selectValue">The select value.</param>
        private void FillComboBoxes(ComboBox fillCombo, byte selectValue)
        {
            // Fill comboBox value as Yes, No
            (fillCombo as ComboBox).Items.Insert(0, SharedFunctions.GetResourceString("No"));
            (fillCombo as ComboBox).Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            (fillCombo as ComboBox).SelectedIndex = selectValue;
        }

        #endregion Fill Yes/No Combo

        #region Fill Assessment & ScheduleType Combo

        /// <summary>
        /// Fills the schedule combo.
        /// </summary>
        private void FillScheduleCombo()
        {
            // Fill Assessment Type combo
            this.assessmentTypeData = this.formF2205Control.WorkItem.F25050GetParcelTypeDetails();
            if (this.assessmentTypeData.Rows.Count > 0)
            {
                this.AssessmentTypeComboBox.DisplayMember = this.assessmentTypeData.AssessmentTypeColumn.ColumnName;
                this.AssessmentTypeComboBox.ValueMember = this.assessmentTypeData.AssessmentTypeIDColumn.ColumnName;
                this.AssessmentTypeComboBox.DataSource = this.assessmentTypeData;
            }

            // Fill Schedule Type combo
            this.scheduleTypeData = this.formF2205Control.WorkItem.F25050GetScheduleTypeDetails();
            if (this.scheduleTypeData.Rows.Count > 0)
            {
                this.ScheduleTypeComboBox.DataSource = this.scheduleTypeData;
                this.ScheduleTypeComboBox.DisplayMember = this.scheduleTypeData.ScheduleTypeColumn.ColumnName;
                this.ScheduleTypeComboBox.ValueMember = this.scheduleTypeData.ScheduleTypeIDColumn.ColumnName;
            }
        }

        #endregion Fill Assessment & ScheduleType Combo

        #region Enable Controls

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enableControl">if set to <c>true</c> [enable control].</param>
        private void EnableControls(bool enableControl)
        {
            // Lock/Unlock TextBox
            this.NewScheduleTextBox.LockKeyPress = enableControl;
            this.RollYearTextBox.LockKeyPress = enableControl;
           
            // Enable/Disable Buttons
            this.ScheduleButton.Enabled = enableControl;
            this.OwnerPicture.Enabled = !enableControl;
            this.DistrictButton.Enabled = !enableControl;
           
            // Enable/Disable Panel (For Combobox)
            this.ScheduleTypePanel.Enabled = !enableControl;
            this.AssessmentTypePanel.Enabled = !enableControl;
            this.HouseholdPanel.Enabled = !enableControl;
            this.ExemptPanel.Enabled = !enableControl;

            // If ScheduleID exists (Does the schedule exist? - Yes)
            if (enableControl)
            {
                this.ExistScheduleTextBox.Text = string.Empty;
                this.NewScheduleTextBox.Text = string.Empty;
                this.RollYearTextBox.Text = string.Empty;
                this.OwnerTextBox.Text = string.Empty;
                this.DistrictTextBox.Text = string.Empty;
                this.ScheduleTypeComboBox.SelectedIndex = -1;
                this.AssessmentTypeComboBox.SelectedIndex = -1;
                this.HouseholdComboBox.SelectedIndex = -1;
                this.ExemptComboBox.SelectedIndex = -1;
            }
        }

        #endregion Enable Controls

        #region Schedule Details

        /// <summary>
        /// Getschedules the details.
        /// </summary>
        private void GetscheduleDetails()
        {
            // Get Schedule Details for ScheduleID
            this.scheduleHeaderData = this.formF2205Control.WorkItem.F2200_ListEditScheduleDetails(this.keyId);
            
            // Set values on appropriate controls
            this.BindValues();
        }
        
        #endregion Schedule Details

        #region Bind values

        /// <summary>
        /// Binds the values.
        /// </summary>
        private void BindValues()
        {
            if (this.scheduleHeaderData.f2200ListScheduleDataTable.Rows.Count > 0)
            {
                F2200EditScheduleData.f2200ListScheduleDataTableRow currentRow;
                currentRow = (F2200EditScheduleData.f2200ListScheduleDataTableRow)this.scheduleHeaderData.f2200ListScheduleDataTable.Rows[0];
                this.ScheduleHeaderLabel.Text = currentRow.ScheduleNumber;
                this.ScheduleHeaderYearLabel.Text = currentRow.RollYear;
                this.NewScheduleTextBox.Text = string.Empty;
                this.RollYearTextBox.Text = currentRow.RollYear;

                // Owner Details
                if (!currentRow.IsOwnerIDNull())
                {
                    this.ownerId = currentRow.OwnerID.ToString();
                }
                else
                {
                    this.ownerId = string.Empty;
                }

                if (!currentRow.IsPrimaryOwnerNull())
                {
                    this.OwnerTextBox.Text = currentRow.PrimaryOwner;
                }
                else
                {
                    this.OwnerTextBox.Text = string.Empty;
                }

                // District Details
                if (!currentRow.IsDistrictIDNull())
                {
                    this.districtId = currentRow.DistrictID.ToString();
                }
                else
                {
                    this.districtId = string.Empty;
                }

                if (!currentRow.IsDistrictNull())
                {
                    this.DistrictTextBox.Text = currentRow.District;
                }
                else
                {
                    this.DistrictTextBox.Text = string.Empty;
                }

                // Assessment Type
                if (!currentRow.IsAssessmentTypeNull())
                {
                    this.AssessmentTypeComboBox.Text = currentRow.AssessmentType;
                }
                else
                {
                    this.AssessmentTypeComboBox.SelectedIndex = -1;
                }

                // Schedule Type
                if (!currentRow.IsScheduleTypeNull())
                {
                    this.ScheduleTypeComboBox.Text = currentRow.ScheduleType;
                }
                else
                {
                    this.ScheduleTypeComboBox.SelectedIndex = -1;
                }

                // Head of household
                if (!currentRow.IsIsHeadOfHouseholdNull())
                {
                    this.HouseholdComboBox.SelectedIndex = currentRow.IsHeadOfHousehold;
                }
                else
                {
                    this.HouseholdComboBox.Text = string.Empty;
                }

                // Exempt value
                if (!currentRow.IsIsExemptNull())
                {
                    this.ExemptComboBox.SelectedIndex = currentRow.IsExempt;
                }
                else
                {
                    this.ExemptComboBox.Text = string.Empty;
                }
            }
        }

        #endregion Bind values

        #region ScheduleHeader Details

        /// <summary>
        /// Gets the schedule header details.
        /// </summary>
        private void GetScheduleHeaderDetails()
        {
            F2205MoveScheduleData.ScheduleHeaderItemsTableDataTable moveScheduleTable = new F2205MoveScheduleData.ScheduleHeaderItemsTableDataTable();
            F2205MoveScheduleData.ScheduleHeaderItemsTableRow moveScheduleRow = moveScheduleTable.NewScheduleHeaderItemsTableRow();
            
            // Conversion
            int rollYear;
            int districtId;
            int ownerId;
            int scheduleTypeId = 0;
            int assessmentTypeId = 0;
            int.TryParse(this.RollYearTextBox.Text, out rollYear);
            int.TryParse(this.ownerId, out ownerId);
            int.TryParse(this.districtId, out districtId);
            if (this.ScheduleTypeComboBox.SelectedValue != null)
            {
                int.TryParse(this.ScheduleTypeComboBox.SelectedValue.ToString(), out scheduleTypeId);
            }

            if (this.AssessmentTypeComboBox.SelectedValue != null)
            {
                int.TryParse(this.AssessmentTypeComboBox.SelectedValue.ToString(), out assessmentTypeId);
            }

            // Assign value on appropriate controls
            moveScheduleRow.ScheduleNumber = this.NewScheduleTextBox.Text;
            moveScheduleRow.RollYear = rollYear;
            moveScheduleRow.OwnerID = ownerId;
            moveScheduleRow.DistrictID = districtId;
            moveScheduleRow.ScheduleTypeID = scheduleTypeId;
            moveScheduleRow.AssessmentTypeID = assessmentTypeId;
            moveScheduleRow.IsHeadofHouseHold = (byte)this.HouseholdComboBox.SelectedIndex;
            moveScheduleRow.IsExempt = (byte)this.ExemptComboBox.SelectedIndex;
            moveScheduleTable.Rows.Add(moveScheduleRow);
            
            // Get XML string for moveScheduleTable 
            this.scheduleHeaderItems = TerraScanCommon.GetXmlString(moveScheduleTable);
        }

        #endregion ScheduleHeader Details

        #region Validation

        /// <summary>
        /// Determines whether [is required field] [the specified reg field].
        /// </summary>
        /// <param name="regField">The reg field.</param>
        /// <returns>
        ///    <c>true</c> if [is required field] [the specified reg field]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsRequiredField(Control regField)
        {
            if (regField is TextBox)
            {
                // Required field validation for TextBox
                TerraScan.UI.Controls.TerraScanTextBox requiredTextBox = regField as TerraScan.UI.Controls.TerraScanTextBox;
               
                if (requiredTextBox.ValidateType.Equals(TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Year))
                {
                    // Validate Roll year 
                    short tempRollYear;
                    short.TryParse(requiredTextBox.Text, out tempRollYear);

                    if (tempRollYear > 0)
                    {
                        return true;
                    }
                }
                else if (!string.IsNullOrEmpty(requiredTextBox.Text.Trim()))
                {
                    return true;
                }
            }
            else
            {
                // Required field validation for combobox
                ComboBox comboValue = regField as ComboBox;
                if (comboValue.SelectedValue != null)
                {
                    return true;
                }
                else
                {
                    // if entered value is not presents in combo list, remove that text and set the index as -1
                    comboValue.Text = string.Empty;
                    comboValue.SelectedIndex = -1;
                }
            }

            // Show message as missing required field
            MessageBox.Show(
                            SharedFunctions.GetResourceString("MoveScheduleMissingFieldMessage"),
                            SharedFunctions.GetResourceString("TerraScanRequiredFieldMissingTitle"), 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);

            // Set focus on missing value field
            regField.Focus();

            return false;
        }
    
        #endregion Validation

        #region Confirmation Message

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        private void ShowMessage(int returnValue)
        {
            if (returnValue.Equals(-1))
            {
                // If ScheduleID and RollYear combination already exists
                MessageBox.Show(
                                SharedFunctions.GetResourceString("MoveScheduleExistMessage"),
                                SharedFunctions.GetResourceString("MoveScheduleExistHeader"), 
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                this.ExistScheduleComboBox.Focus();
            }
            else if (returnValue.Equals(0))
            {
                // Any error occur while moving schedule line items
                MessageBox.Show(
                                SharedFunctions.GetResourceString("MoveScheduleErrorMessage"),
                                SharedFunctions.GetResourceString("MoveScheduleLineHeader"), 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
                this.ExistScheduleComboBox.Focus();
            }
            else
            {
                // After Successful move show message
                MessageBox.Show(
                                SharedFunctions.GetResourceString("MoveScheduleSuccessMessage"),
                                SharedFunctions.GetResourceString("MoveScheduleLineHeader"), 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Information);
                
                // For successful schedule move return the dialog result as OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #endregion Confirmation Message

        #endregion Methods
    }
}
