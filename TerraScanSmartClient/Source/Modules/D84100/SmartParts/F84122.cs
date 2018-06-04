//--------------------------------------------------------------------------------------------
// <copyright file="F84122.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84122.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 Dec 06        JYOTHI              Created
// 29 Dec 06        KARTHIKEYAN.B       Added Functionality
//*********************************************************************************/
namespace D84100
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
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;

    /// <summary>
    /// F84122 class file
    /// </summary>
    public partial class F84122 : BaseSmartPart
    {
        #region ReadOnly

        /// <summary>
        /// used to store DateFormat
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateformat");

        #endregion

        #region Member Variables

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F84122Controller form84122Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// instance for GDocCommentData
        /// </summary>
        private GDocCommonData gdocCommonData = new GDocCommonData();

        /// <summary>
        /// An instance for sanitaryManholeLocationData
        /// </summary>
        private F84122SanitaryManholeLocationData sanitaryManholeLocationData = new F84122SanitaryManholeLocationData();

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// listGDocPropertyReference datatable
        /// </summary>
        private GDocCommonData.ListGDocPropertyReferenceDataTable listGDocPropertyReference = new GDocCommonData.ListGDocPropertyReferenceDataTable();

        /// <summary>
        /// No of rows present in the ListPropertyReference Datatable returned from DataBase
        /// </summary>
        private int gdocPropertyReferenceRowsCount;

        /// <summary>
        /// isShift
        /// </summary>
        private bool isShift;

        #endregion

        #region Variable For ComboBox Value Members

        /// <summary>
        /// gpsById
        /// </summary>
        private int gpsById;

        /// <summary>
        /// administrativeAreaId
        /// </summary>
        private int administrativeAreaId;

        /// <summary>
        /// operationalAreaId
        /// </summary>
        private int operationalAreaId;

        /// <summary>
        /// gridId
        /// </summary>
        private int gridId;

        /// <summary>
        /// nsStreetId
        /// </summary>
        private int northsouthStreetId;

        /// <summary>
        /// ewStreetId
        /// </summary>
        private int eastwestStreetId;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84122"/> class.
        /// </summary>
        public F84122()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84122"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F84122(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.LocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LocationPictureBox.Height, this.LocationPictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84122"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// /// <param name="featureClassId">The feature class id.</param>
        public F84122(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.featureClassId = featureClassId;
            this.LocationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LocationPictureBox.Height, this.LocationPictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D84100_F84122_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84100_F84122_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_OnSave_SetKeyId;
       
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F84122 control.
        /// </summary>
        /// <value>The F84122 control.</value>
        [CreateNew]
        public F84122Controller Form84122Control
        {
            get { return this.form84122Control as F84122Controller; }
            set { this.form84122Control = value; }
        }        

        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.ClearSanitaryManholeLocationControls();
                    this.SetNewComboIndex();
                    this.LockControls(true);
                    this.ControlLock(false);
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearSanitaryManhole();
                    this.SetNewComboIndex();
                    this.LockControls(false);
                }
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
            try
            {
                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                this.LockControls(true);
                this.CustomizeSanitaryManholeLocation();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.slicePermissionField.editPermission)
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    }
                }
                else
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.slicePermissionField.editPermission)
                    {
                        this.SaveSanitaryManholeLocation();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.CustomizeSanitaryManholeLocation();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                    if (this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows.Count > 0)
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription D84100_F84121_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84100_F84121_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                this.keyId = eventArgs.Data;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.FlagSliceForm = true;
                    this.flagLoadOnProcess = true;
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.CustomizeSanitaryManholeLocation();
                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }    

        #endregion

        #region Protected Methods

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
        #endregion
        
        #region Common Static Method

        /// <summary>
        /// Shows the GPS date calender.
        /// </summary>
        /// <param name="setCalender">The set calender.</param>
        /// <param name="datePanel">The date panel.</param>
        /// <param name="datePic">The date pic.</param>
        /// <param name="mainPanel">The main panel.</param>
        private static void ShowCalender(MonthCalendar setCalender, Panel datePanel, Button datePic, Panel mainPanel)
        {
            setCalender.Visible = true;
            setCalender.ScrollChange = 1;
            setCalender.BringToFront();
            //// Display the Calender control near the Calender Picture box.
            setCalender.Left = (mainPanel.Left + datePanel.Left + datePic.Left + datePic.Width) + 3;
            setCalender.Top = (mainPanel.Top + datePanel.Top + datePic.Top);
            setCalender.Focus();
        }

        #endregion
        
        #region Calendar Events

        /// <summary>
        /// Handles the KeyDown event of the GPSDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GPSDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.GPSDateTextBox.Text = this.GPSDateCalender.SelectionStart.ToShortDateString();
                    this.GPSDateTextBox.Focus();                    
                    this.GPSDateCalender.Visible = false;
                }
                isShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the GPSDatePic control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GPSDatePic_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GPSDateTextBox.Text.Trim()))
            {
                try
                {
                    this.GPSDateCalender.SetDate(Convert.ToDateTime(this.GPSDateTextBox.Text.Trim()));
                    ShowCalender(this.GPSDateCalender, this.GPSDatePanel, this.GPSDatePic, this.RegisterPanel);
                }
                catch
                {
                    this.GPSDateCalender.Visible = false;
                    this.GPSDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                }
            }
            else
            {
                ShowCalender(this.GPSDateCalender, this.GPSDatePanel, this.GPSDatePic, this.RegisterPanel);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the GPSDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void GPSDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.SetGPSDate(e.Start.ToShortDateString());
        }

        /// <summary>
        /// Handles the Leave event of the GPSDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GPSDateCalender_Leave(object sender, EventArgs e)
        {
            this.GPSDateCalender.Visible = false;
            if (isShift)
            {
                this.GPSDateTextBox.Focus();
            }
            else
            {
                this.GPSDateTextBox.Focus();
            }
        }

        /// <summary>
        /// Sets the GPS date.
        /// </summary>
        /// <param name="dateValue">The date value.</param>        
        private void SetGPSDate(string dateValue)
        {
            ////Assign the selected date to the DateTextbox.
            this.GPSDateTextBox.Text = dateValue;
            this.GPSDateCalender.Visible = false;
            this.GPSDateTextBox.Focus();
        }

        #endregion

        #region Form Control Events

        /// <summary>
        /// Handles the Load event of the F84122 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84122_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            ////Set the Max Length for editable fields
            this.SetMaxLength();
            this.CustomizeSanitaryManholeLocation();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Handles the Click event of the LocationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the MouseEnter event of the LocationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocationPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.InspectionDetailsToolTip.SetToolTip(this.LocationPictureBox, Utility.GetFormNameSpace(this.Name));
        }
        
        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "Text Changed Events In Text Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }
        
        /// <summary>
        /// Handles the SelectionChangeCommitted event of all the ComboBoxes in the Sanitary Manhole Location Form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();                
        }

        /// <summary>
        /// To handle keypress event of the Textboxes, for not allowing the "Enter" Key
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        e.Handled = true;
                        break;
                    }
            }
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            try
            {
                this.GPSDateTextBox.MaxLength = this.sanitaryManholeLocationData.GetSanitaryManholeLocation.GPSDateColumn.MaxLength;
                this.DistrictProjectTextBox.MaxLength = this.sanitaryManholeLocationData.GetSanitaryManholeLocation.District_ProjectColumn.MaxLength;
                this.LocationNotesTextBox.MaxLength = this.sanitaryManholeLocationData.GetSanitaryManholeLocation.LocationDescriptionColumn.MaxLength;

                this.GPSByComboBox.MaxLength = this.gdocCommonData.ListGDocUser.NameDisplayColumn.MaxLength;
                this.AdministrativeAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
                this.OperationalAreaComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
                this.GridComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
                this.NorthSouthStreetComboBox.MaxLength = this.gdocCommonData.ListGDocStreet.StreetNameColumn.MaxLength;
                this.EastWestStreetComboBox.MaxLength = this.gdocCommonData.ListGDocStreet.StreetNameColumn.MaxLength;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To Clear Sanitary Manhole Location Properties TextBoxs and Checked Boxs 
        /// </summary>
        private void ClearSanitaryManholeLocationControls()
        {
            this.GPSDateTextBox.Text = string.Empty;
            this.XCoordinateTextBox.Text = string.Empty;
            this.YCoordinateTextBox.Text = string.Empty;
            this.DistrictProjectTextBox.Text = string.Empty;            
            this.LocationNotesTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To Clear the entire Sanitary Manhole Form
        /// </summary>
        private void ClearSanitaryManhole()
        {
            this.ClearComboBox(this);
            this.ClearSanitaryManholeLocationControls();
        }

        /// <summary>
        /// Clears the All the combo boxs in the form.
        /// </summary>
        /// <param name="currentControl">The current control.</param>
        private void ClearComboBox(Control currentControl)
        {
            try
            {
                if (currentControl.HasChildren)
                {
                    foreach (Control childControl in currentControl.Controls)
                    {
                        this.ClearComboBox(childControl);
                    }
                }
                else
                {
                    if (currentControl.GetType() == typeof(TerraScanComboBox))
                    {
                        TerraScanComboBox currentComboBox = (TerraScanComboBox)currentControl;
                        currentComboBox.DataSource = null;
                        currentComboBox.Items.Clear();
                        currentComboBox.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean vlaue</param>
        private void LockControls(bool lockControl)
        {
            this.GPSDatePanel.Enabled = lockControl;            
            this.GPSDateCalender.Enabled = lockControl;
            this.GPSByPanel.Enabled = lockControl;
            this.XCoordinatePanel.Enabled = lockControl;
            this.YCoordinatePanel.Enabled = lockControl;
            this.AdministrativeAreaPanel.Enabled = lockControl;
            this.OperationalAreaPanel.Enabled = lockControl;
            this.GridPanel.Enabled = lockControl;
            this.DistrictProjectPanel.Enabled = lockControl;
            this.NorthSouthStreetPanel.Enabled = lockControl;
            this.EastWestStreetPanel.Enabled = lockControl;            
            this.LocationNotesPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.GPSDateTextBox.LockKeyPress = controlLook;
            this.XCoordinateTextBox.LockKeyPress = controlLook;
            this.YCoordinateTextBox.LockKeyPress = controlLook;
            this.DistrictProjectTextBox.LockKeyPress = controlLook;
            this.LocationNotesTextBox.LockKeyPress = controlLook;

            this.GPSDatePic.Enabled = !controlLook;
            this.GPSDateCalender.Enabled = !controlLook;
            this.GPSByComboBox.Enabled = !controlLook;
            this.AdministrativeAreaComboBox.Enabled = !controlLook;
            this.OperationalAreaComboBox.Enabled = !controlLook;
            this.GridComboBox.Enabled = !controlLook;
            this.NorthSouthStreetComboBox.Enabled = !controlLook;
            this.EastWestStreetComboBox.Enabled = !controlLook;
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.GPSByComboBox.SelectedIndex = -1;
            this.NorthSouthStreetComboBox.SelectedIndex = -1;
            this.EastWestStreetComboBox.SelectedIndex = -1;
            this.AdministrativeAreaComboBox.SelectedIndex = -1;
            this.OperationalAreaComboBox.SelectedIndex = -1;
            this.GridComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// Creates the Temp ListGdocpropertyreference For combo box which refereces the F8000_GetGDocPropertyReference method.
        /// </summary>
        /// <param name="refFieldValue">The ref field value.</param>
        private void CreateTempListGDocPropertyReference(string refFieldValue)
        {
            try
            {
                this.listGDocPropertyReference.Clear();
                DataRow customRow = this.listGDocPropertyReference.NewRow();
                customRow[this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName] = string.Empty;
                customRow[this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName] = "0";
                this.listGDocPropertyReference.Rows.Add(customRow);

                ////to clear the this.gdocCommonData.ListGDocPropertyReference
                this.gdocCommonData.ListGDocPropertyReference.Clear();
                this.gdocCommonData = this.form84122Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
                this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
                this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        /// <summary>
        /// Customizes the Sanitary Manhole location.
        /// </summary>
        private void CustomizeSanitaryManholeLocation()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.sanitaryManholeLocationData = this.form84122Control.WorkItem.F84122_GetSanitaryManholeLocation(this.keyId);
                if (this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.GPSDateColumn].ToString()))
                    {
                        DateTime gpsDate;
                        gpsDate = Convert.ToDateTime(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.GPSDateColumn].ToString());
                        this.GPSDateTextBox.Text = gpsDate.ToString("M/d/yyyy");
                    }
                    else
                    {
                        this.GPSDateTextBox.Text = string.Empty;
                    }                    

                    this.XCoordinateTextBox.Text = this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.X_CoordColumn].ToString();
                    this.YCoordinateTextBox.Text = this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Y_CoordColumn].ToString();
                    this.DistrictProjectTextBox.Text = this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.District_ProjectColumn].ToString();
                    this.LocationNotesTextBox.Text = this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.LocationDescriptionColumn].ToString();

                    this.gpsById = Convert.ToInt32(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.GPSByIDColumn]);
                    this.administrativeAreaId = Convert.ToInt32(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.AdministrativeAreaIDColumn]);
                    this.operationalAreaId = Convert.ToInt32(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.OperationalAreaIDColumn]);
                    this.gridId = Convert.ToInt32(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.GridIDColumn]);
                    this.northsouthStreetId = Convert.ToInt32(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.N_S_StreetIDColumn]);
                    this.eastwestStreetId = Convert.ToInt32(this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows[0][this.sanitaryManholeLocationData.GetSanitaryManholeLocation.E_W_StreetIDColumn]);
                }
                else
                {
                    this.ClearSanitaryManhole();
                    this.LockControls(false);
                }

                ////To Load All The ComBoBoxs
                this.LoadAllComboBox();
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #region Load Methods for all the Comboboxes

        /// <summary>
        /// Loads all combo box.
        /// </summary>
        private void LoadAllComboBox()
        {
            this.LoadGPSByComboBox();
            this.LoadStringComboBoxes("AdministrativeAreaID", this.AdministrativeAreaComboBox, this.administrativeAreaId);
            this.LoadStringComboBoxes("OperationalAreaID", this.OperationalAreaComboBox, this.operationalAreaId);
            this.LoadStringComboBoxes("GridID", this.GridComboBox, this.gridId);
            this.LoadStreetComboBox(this.NorthSouthStreetComboBox, this.northsouthStreetId);
            this.LoadStreetComboBox(this.EastWestStreetComboBox, this.eastwestStreetId);
        }

        /// <summary>
        /// Generic Load methods for Loading Data in the Comboboxes
        /// </summary>
        /// <param name="referenceType">Reference Type for CreateTempListGDocPropertyReference</param>
        /// <param name="comboControl">The Combobox Control to load</param>
        /// <param name="typeId">The Type ID for Combobox Index</param>
        private void LoadStringComboBoxes(string referenceType, Control comboControl, int typeId)
        {
            try
            {
                this.CreateTempListGDocPropertyReference(referenceType);

                DataTable comboDataTable = new DataTable();
                comboDataTable.Merge(this.listGDocPropertyReference);

                if (this.gdocPropertyReferenceRowsCount > 0)
                {
                    if (comboControl.GetType() == typeof(TerraScanComboBox))
                    {
                        TerraScanComboBox currentComboBox = (TerraScanComboBox)comboControl;
                        currentComboBox.DataSource = comboDataTable;
                        currentComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                        currentComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;

                        if (typeId > 0)
                        {
                            currentComboBox.SelectedValue = typeId;
                        }
                        else
                        {
                            currentComboBox.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the GPSBy combo box.
        /// </summary>
        private void LoadGPSByComboBox()
        {
            try
            {
                GDocCommonData.ListGDocUserDataTable listGDocUser = new GDocCommonData.ListGDocUserDataTable();

                DataRow customRow = listGDocUser.NewRow();
                listGDocUser.Clear();
                customRow[this.gdocCommonData.ListGDocUser.NameDisplayColumn.ColumnName] = string.Empty;
                customRow[this.gdocCommonData.ListGDocUser.UserIDColumn.ColumnName] = "0";
                listGDocUser.Rows.Add(customRow);

                this.gdocCommonData = this.form84122Control.WorkItem.F8000_GetGDocUser();
                listGDocUser.Merge(this.gdocCommonData.ListGDocUser);

                if (this.gdocCommonData.ListGDocUser.Rows.Count > 0)
                {
                    this.GPSByComboBox.DataSource = listGDocUser;
                    this.GPSByComboBox.DisplayMember = this.gdocCommonData.ListGDocUser.NameDisplayColumn.ColumnName;
                    this.GPSByComboBox.ValueMember = this.gdocCommonData.ListGDocUser.UserIDColumn.ColumnName;
                    if (this.gpsById > 0)
                    {
                        this.GPSByComboBox.SelectedValue = this.gpsById;
                    }
                    else
                    {
                        this.GPSByComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>        
        /// Loads the north south street combo box.        
        /// </summary>
        /// <param name="comboControl">The Combobox Control to load</param>
        /// <param name="typeId">The Type ID for Combobox Index</param>
        private void LoadStreetComboBox(Control comboControl, int typeId)
        {
            try
            {
                GDocCommonData.ListGDocStreetDataTable listGDocStreet = new GDocCommonData.ListGDocStreetDataTable();

                DataRow customRow = listGDocStreet.NewRow();
                listGDocStreet.Clear();
                customRow[this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName] = string.Empty;
                customRow[this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName] = "0";
                listGDocStreet.Rows.Add(customRow);

                this.gdocCommonData = this.form84122Control.WorkItem.F8000_GetGDocStreet();
                listGDocStreet.Merge(this.gdocCommonData.ListGDocStreet);

                if (this.gdocCommonData.ListGDocStreet.Rows.Count > 0)
                {
                    if (comboControl.GetType() == typeof(TerraScanComboBox))
                    {
                        TerraScanComboBox currentComboBox = (TerraScanComboBox)comboControl;
                        currentComboBox.DataSource = listGDocStreet;
                        currentComboBox.DisplayMember = this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName;
                        currentComboBox.ValueMember = this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName;
                        if (typeId > 0)
                        {
                            currentComboBox.SelectedValue = typeId;
                        }
                        else
                        {
                            currentComboBox.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Save Method

        /// <summary>
        /// Saves the Sanitary Manhole location.
        /// </summary>
        private void SaveSanitaryManholeLocation()
        {
            try
            {
                decimal tempXCoordinate;
                decimal tempYCoordinate;                
                string tempSanitaryManholeLocation;
                this.Cursor = Cursors.WaitCursor;

                F84122SanitaryManholeLocationData sanitaryManholeLocationData1 = new F84122SanitaryManholeLocationData();
                //this.sanitaryManholeLocationData.GetSanitaryManholeLocation.Rows.Clear();
                F84122SanitaryManholeLocationData.GetSanitaryManholeLocationRow dr = sanitaryManholeLocationData1.GetSanitaryManholeLocation.NewGetSanitaryManholeLocationRow();

                #region String Textboxes

                if (!string.IsNullOrEmpty(this.GPSDateTextBox.Text.Trim()))
                {
                    dr.GPSDate = this.GPSDateTextBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.DistrictProjectTextBox.Text.Trim()))
                {
                    dr.District_Project = this.DistrictProjectTextBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.LocationNotesTextBox.Text.Trim()))
                {
                    dr.LocationDescription = this.LocationNotesTextBox.Text.Trim();
                }

                #endregion

                #region Decimal Textboxes

                if (!string.IsNullOrEmpty(this.XCoordinateTextBox.Text.Trim()))
                {
                    Decimal.TryParse(this.XCoordinateTextBox.Text.Trim(), out tempXCoordinate);
                    dr.X_Coord = tempXCoordinate;
                }

                if (!string.IsNullOrEmpty(this.YCoordinateTextBox.Text.Trim()))
                {
                    Decimal.TryParse(this.YCoordinateTextBox.Text.Trim(), out tempYCoordinate);
                    dr.Y_Coord = tempYCoordinate;
                }

                #endregion

                #region ComboBoxes
                
                if (!string.IsNullOrEmpty(this.GPSByComboBox.Text.Trim()))
                {
                    dr.GPSByID = Convert.ToInt32(this.GPSByComboBox.SelectedValue);
                }
                else
                {
                    dr.GPSByID = 0;
                }

                if (!string.IsNullOrEmpty(this.AdministrativeAreaComboBox.Text.Trim()))
                {
                    dr.AdministrativeAreaID = Convert.ToInt32(this.AdministrativeAreaComboBox.SelectedValue);
                }
                else
                {
                    dr.AdministrativeAreaID = 0;
                }

                if (!string.IsNullOrEmpty(this.OperationalAreaComboBox.Text.Trim()))
                {
                    dr.OperationalAreaID = Convert.ToInt32(this.OperationalAreaComboBox.SelectedValue);
                }
                else
                {
                    dr.OperationalAreaID = 0;
                }

                if (!string.IsNullOrEmpty(this.GridComboBox.Text.Trim()))
                {
                    dr.GridID = Convert.ToInt32(this.GridComboBox.SelectedValue);
                }
                else
                {
                    dr.GridID = 0;
                }

                if (!string.IsNullOrEmpty(this.NorthSouthStreetComboBox.Text.Trim()))
                {
                    dr.N_S_StreetID = Convert.ToInt32(this.NorthSouthStreetComboBox.SelectedValue);
                }
                else
                {
                    dr.N_S_StreetID = 0;
                }

                if (!string.IsNullOrEmpty(this.EastWestStreetComboBox.Text.Trim()))
                {
                    dr.E_W_StreetID = Convert.ToInt32(this.EastWestStreetComboBox.SelectedValue);
                }
                else
                {
                    dr.E_W_StreetID = 0;
                }

                #endregion

                sanitaryManholeLocationData1.GetSanitaryManholeLocation.Rows.Add(dr);
                tempSanitaryManholeLocation = Utility.GetXmlString(sanitaryManholeLocationData1.GetSanitaryManholeLocation.Copy());
                this.keyId = this.form84122Control.WorkItem.F84122_SaveSanitaryManholeLocation(this.keyId, tempSanitaryManholeLocation, TerraScanCommon.UserId);

                ////to sent the this keyid(this.keyId) 
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<int>(this.keyId));
            }
            catch (SoapException e)
            {
                ExceptionManager.ManageException(e, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            catch (Exception ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        private void GPSByComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        #endregion



        private void AdministrativeAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        private void OperationalAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        private void GridComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        private void NorthSouthStreetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        private void EastWestStreetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }
    }
}
