//--------------------------------------------------------------------------------------------
// <copyright file="F84401.cs" company="Congruent">
//   Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//  This file contains methods for the F84401.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author             Description
// ----------      ---------          ---------------------------------------------------------
// 18/04/2008      D.LathaMaheswari    Created 
// *********************************************************************************/
namespace D84401
{
    using System;
    using System.Data;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls; 

    /// <summary>
    /// F84401 class
    /// </summary>
    public partial class F84401 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// valveId or keyId
        /// </summary>
        private int signId;

        /// <summary>
        /// feature Class Id
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// controller F84721
        /// </summary>
        private F84401Controller form84401Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flag Load On Process
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMaster Edit Permission 
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// instance for GDocCommentData
        /// </summary>
        private GDocCommonData gdocCommonData = new GDocCommonData();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// listGDocStreet datatable instances
        /// </summary>
        private GDocCommonData.ListGDocStreetDataTable listGDocStreet = new GDocCommonData.ListGDocStreetDataTable();

        /// <summary>
        /// listGDocPropertyReference datatable
        /// </summary>
        private GDocCommonData.ListGDocPropertyReferenceDataTable listGDocPropertyReference = new GDocCommonData.ListGDocPropertyReferenceDataTable();

        /// <summary>
        /// An instance for signPropertiesData
        /// </summary>
        private F84401SignsPropertyData signPropertyData = new F84401SignsPropertyData();

        /// <summary>
        /// No of rows present in the ListPropertyReference Datatable returned from DataBase
        /// </summary>
        private int gdocPropertyReferenceRowsCount;

        /// <summary>
        /// Number of rows present in the listGDocStreet datatable returned form database
        /// </summary>
        private int gdocListGDocStreetRowsCount;

        /// <summary>
        /// Sign One ID
        /// </summary>
        private int signOneId;

        /// <summary>
        /// Sign Two ID
        /// </summary>
        private int signTwoId;

        /// <summary>
        /// Sign Three ID
        /// </summary>
        private int signThreeId;

        /// <summary>
        /// Sign Four ID
        /// </summary>
        private int signFourId;

        /// <summary>
        /// Sign Five ID
        /// </summary>
        private int signFiveId;

        /// <summary>
        /// Sign Six ID.
        /// </summary>
        private int signSixId;

        /// <summary>
        /// Sign Seven ID
        /// </summary>
        private int signSevenId;

        /// <summary>
        /// Sign Street ID
        /// </summary>
        private int streetId;

        /// <summary>
        /// Cross Street ID
        /// </summary>
        private int crossStreetId;

        /// <summary>
        /// used to set New mode
        /// </summary>
        private bool newMode = false;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84401"/> class.
        /// </summary>
        public F84401()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84401"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F84401(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.signId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.SignPropertyPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SignPropertyPictureBox.Height, this.SignPropertyPictureBox.Width, tabText, red, green, blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84401"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassId">The feature class id.</param>
        public F84401(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.signId = keyID;
            this.featureClassId = featureClassId;
            this.formMasterPermissionEdit = permissionEdit;
            this.SignPropertyPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SignPropertyPictureBox.Height, this.SignPropertyPictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructor

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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;


        /*
        /// <summary>
        /// Declare the event D84700_F84721_OnSave_GetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84721_OnSave_GetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_GetKeyId;
        */

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form84401 control.
        /// </summary>
        /// <value>The form84401 control.</value>
        [CreateNew]
        public F84401Controller Form84401Control
        {
            get { return this.form84401Control as F84401Controller; }
            set { this.form84401Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.PermissionFiled.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ClearSignsProperty();
                this.SetNewComboIndex();
                this.LockControls(true);
                this.ControlLock(false);
                this.newMode = true;
                this.TextTextBox.Focus();
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearSignsProperty();
                this.SetNewComboIndex();
                this.LockControls(false);
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
            //// Lock controls based on Edit permission
            if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LockControls(true);

            // Set values on appropriate controls
            this.SetControlValues();
            this.pageMode = TerraScanCommon.PageModeTypes.View;

            // Set focus on first editable controls
            if (this.signPropertyData.GetSignsProperty.Rows.Count > 0)
            {
                this.ActiveControl = this.TextTextBox;
                this.ActiveControl.Focus();
            }

            this.newMode = false;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.PermissionFiled.editPermission)
                {
                    this.SaveSignProperty();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.LockControls(true);
                this.ControlLock(false);
                this.SetControlValues();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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

                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                if (this.signPropertyData.GetSignsProperty.Rows.Count > 0)
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

        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.PermissionFiled.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                this.form84401Control.WorkItem.F84401_DeleteSignsProperties(this.signId, TerraScanCommon.UserId);
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                this.Cursor = Cursors.Default;
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
                this.FlagSliceForm = true;
                //// On load set pagemode as view
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.signId = eventArgs.Data.SelectedKeyId;
                //// Load sign values
                this.LoadSignsProperties();
                //// Lock controls based on Permission
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                //// Set focus on first editable field
                this.TextTextBox.Focus();
            }
        }

        #endregion Event Subscription

        #region Protected methods

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

        #endregion Protected methods

        #region Methods

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns></returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();

            ////Offset TextBox
            if (!string.IsNullOrEmpty(this.OffSetTextBox.Text.Trim()))
            {
                int leng = this.OffSetTextBox.DecimalTextBoxValue.ToString().Length;
                // to get the decimal position.
                int decPos = this.OffSetTextBox.DecimalTextBoxValue.ToString().IndexOf(".");
                if (leng > 6)
                {
                    if (decPos != -1)
                    {
                        if (decPos > 4)
                        {
                            this.OffSetTextBox.Focus(); 
                           //// sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("Entered values exceeds max limit."));
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit.") ,ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.FormNo = formNo;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.DisableNewMethod = true;
                            return sliceValidationFields;
                        }
                    }
                }
            }

            ////DistFromCrossSt TextBox
            if (!string.IsNullOrEmpty(this.DistanceTextBox.Text.Trim()))
            {
                int leng = this.DistanceTextBox.DecimalTextBoxValue.ToString().Length;
                // to get the decimal position.
                int decPos = this.DistanceTextBox.DecimalTextBoxValue.ToString().IndexOf(".");
                if (leng > 6)
                {
                    if (decPos != -1)
                    {
                        if (decPos > 4)
                        {
                            this.DistanceTextBox.Focus();
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.FormNo = formNo;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.DisableNewMethod = true;
                            return sliceValidationFields;
                        }
                    }
                }
            }

            ////SignHeight TextBox
            if (!string.IsNullOrEmpty(this.SignHeightTextBox.Text.Trim()))
            {
                int leng = this.SignHeightTextBox.DecimalTextBoxValue.ToString().Length;
                // to get the decimal position.
                int decPos = this.SignHeightTextBox.DecimalTextBoxValue.ToString().IndexOf(".");
                if (leng > 6)
                {
                    if (decPos != -1)
                    {
                        if (decPos > 4)
                        {
                            this.SignHeightTextBox.Focus();
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.FormNo = formNo;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.DisableNewMethod = true;
                            return sliceValidationFields;
                        }
                    }
                }
            }

            ////FaceHeight TextBox
            if (!string.IsNullOrEmpty(this.HeightTextBox.Text.Trim()))
            {
                int leng = this.HeightTextBox.DecimalTextBoxValue.ToString().Length;
                // to get the decimal position.
                int decPos = this.HeightTextBox.DecimalTextBoxValue.ToString().IndexOf(".");
                if (leng > 6)
                {
                    if (decPos != -1)
                    {
                        if (decPos > 4)
                        {
                            this.HeightTextBox.Focus();
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.FormNo = formNo;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.DisableNewMethod = true;
                            return sliceValidationFields;
                        }
                    }
                }
            }

            ////FaceWidth TextBox
            if (!string.IsNullOrEmpty(this.WidthTextBox.Text.Trim()))
            {
                int leng = this.WidthTextBox.DecimalTextBoxValue.ToString().Length;
                // to get the decimal position.
                int decPos = this.WidthTextBox.DecimalTextBoxValue.ToString().IndexOf(".");
                if (leng > 5)
                {
                    if (decPos != -1)
                    {
                        if (decPos > 3)
                        {
                            this.WidthTextBox.Focus();
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.FormNo = formNo;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.DisableNewMethod = true;
                            return sliceValidationFields;
                        }
                    }
                }
            }

            ////GPS X TextBox
            if (!string.IsNullOrEmpty(this.XValueTextBox.Text.Trim()))
            {
                int leng = this.XValueTextBox.DecimalTextBoxValue.ToString().Trim().Length;
                // to get the decimal position.
                int decPos = this.XValueTextBox.DecimalTextBoxValue.ToString().Trim().IndexOf(".");
                if (leng > 18)
                {
                    if (decPos != -1)
                    {
                        if (decPos > 14)
                        {
                            this.XValueTextBox.Focus();
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.FormNo = formNo;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.DisableNewMethod = true;
                            return sliceValidationFields;
                        }
                    }
                }
            }

            ////GPS Y TextBox
            if (!string.IsNullOrEmpty(this.YValueTextBox.Text.Trim()))
            {
                int leng = this.YValueTextBox.DecimalTextBoxValue.ToString().Length;
                // to get the decimal position.
                int decPos = this.YValueTextBox.DecimalTextBoxValue.ToString().IndexOf(".");
                if (leng > 18)
                {
                    if (decPos != -1)
                    {
                        if (decPos > 14)
                        {
                            this.YValueTextBox.Focus();
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.FormNo = formNo;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.DisableNewMethod = true;
                            return sliceValidationFields;
                        }
                    }
                }
            }

            return sliceValidationFields;
        }


        #region Clear Controls
        /// <summary>
        /// To Clear Signs Properties TextBoxs 
        /// </summary>
        private void ClearSignsPropertyControls()
        {
            this.TextTextBox.Text = string.Empty;
            this.CIDTextBox.Text = string.Empty;
            this.OffSetTextBox.Text = string.Empty;
            this.DistanceTextBox.Text = string.Empty;
            this.AspectTextBox.Text = string.Empty;
            this.WidthTextBox.Text = string.Empty;
            this.HeightTextBox.Text = string.Empty;
            this.XValueTextBox.Text = string.Empty;
            this.YValueTextBox.Text = string.Empty;
            this.SignHeightTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To Clear the entire Signs Property Form
        /// </summary>
        private void ClearSignsProperty()
        {
            this.ClearSignsPropertyControls();
        }

        /// <summary>
        /// To set index for combo on new mode  
        /// </summary>
        private void SetNewComboIndex()
        {
            this.Sign1ComboBox.SelectedIndex = -1;
            this.Sign2ComboBox.SelectedIndex = -1;
            this.Sign3ComboBox.SelectedIndex = -1;
            this.Sign4ComboBox.SelectedIndex = -1;
            this.Sign5ComboBox.SelectedIndex = -1;
            this.Sign6ComboBox.SelectedIndex = -1;
            this.Sign7ComboBox.SelectedIndex = -1;
            this.StreetComboBox.SelectedIndex = -1;
            this.CrossStreetComboBox.SelectedIndex = -1;
        }

        #endregion Clear Controls

        #region Lock Controls

        /// <summary>
        /// To Enable/Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.TextPanel.Enabled = lockControl;
            this.CIDPanel.Enabled = lockControl;
            this.Sign1Panel.Enabled = lockControl;
            this.Sign2Panel.Enabled = lockControl;
            this.Sign3Panel.Enabled = lockControl;
            this.Sign4Panel.Enabled = lockControl;
            this.Sign5Panel.Enabled = lockControl;
            this.Sign6Panel.Enabled = lockControl;
            this.Sign7Panel.Enabled = lockControl;
            this.StreetPanel.Enabled = lockControl;
            this.CrossStreetPanel.Enabled = lockControl;
            this.OffSetPanel.Enabled = lockControl;
            this.DistancePanel.Enabled = lockControl;
            this.AspectPanel.Enabled = lockControl;
            this.WidthPanel.Enabled = lockControl;
            this.HeightPanel.Enabled = lockControl;
            this.XValuePanel.Enabled = lockControl;
            this.YValuePanel.Enabled = lockControl;
            this.SignHeightPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLock">Boolean value</param>
        private void ControlLock(bool controlLock)
        {
            this.TextTextBox.LockKeyPress = controlLock;
            this.CIDTextBox.LockKeyPress = controlLock;
            this.OffSetTextBox.LockKeyPress = controlLock;
            this.DistanceTextBox.LockKeyPress = controlLock;
            this.AspectTextBox.LockKeyPress = controlLock;
            this.WidthTextBox.LockKeyPress = controlLock;
            this.HeightTextBox.LockKeyPress = controlLock;
            this.XValueTextBox.LockKeyPress = controlLock;
            this.YValueTextBox.LockKeyPress = controlLock;
            this.SignHeightTextBox.LockKeyPress = controlLock;

            this.Sign1ComboBox.Enabled = !controlLock;
            this.Sign2ComboBox.Enabled = !controlLock;
            this.Sign3ComboBox.Enabled = !controlLock;
            this.Sign4ComboBox.Enabled = !controlLock;
            this.Sign5ComboBox.Enabled = !controlLock;
            this.Sign6ComboBox.Enabled = !controlLock;
            this.Sign7ComboBox.Enabled = !controlLock;
            this.StreetComboBox.Enabled = !controlLock;
            this.CrossStreetComboBox.Enabled = !controlLock;
        }

        #endregion Lock Controls

        #region Set Length

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            ////Set Maximum Length for Text Boxes
            this.TextTextBox.MaxLength = this.signPropertyData.GetSignsProperty.SignTextColumn.MaxLength;
            this.CIDTextBox.MaxLength = this.signPropertyData.GetSignsProperty.CIDColumn.MaxLength;
            this.AspectTextBox.MaxLength = this.signPropertyData.GetSignsProperty.AspectColumn.MaxLength;
            ////Set Maximum Length for Sign Combo Boxes
            this.Sign1ComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.Sign2ComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.Sign3ComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.Sign4ComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.Sign5ComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.Sign6ComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            this.Sign7ComboBox.MaxLength = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.MaxLength;
            ////Set Maximum Length for Street Combo Boxes
            this.StreetComboBox.MaxLength = this.gdocCommonData.ListGDocStreet.StreetNameColumn.MaxLength;
            this.CrossStreetComboBox.MaxLength = this.gdocCommonData.ListGDocStreet.StreetNameColumn.MaxLength;
        }

        #endregion Set Length

        #region SetValues

        /// <summary>
        /// Sets the control values.
        /// </summary>
        private void SetControlValues()
        {
            this.signPropertyData = this.form84401Control.WorkItem.F84401_GetSignsProperties(this.signId);
            if (this.signPropertyData.GetSignsProperty.Rows.Count > 0)
            {
                //// Coding added for the issue 4497(quick find)
                //// controls should enable when records are available.
                this.LockControls(true);
                //// Coding ends here

                ////Set Label Text
                this.Sign1Label.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign01LabelValueColumn].ToString() + ":";
                this.Sign2Label.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign02LabelValueColumn].ToString() + ":";
                this.Sign3Label.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign03LabelValueColumn].ToString() + ":";
                this.Sign4Label.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign04LabelValueColumn].ToString() + ":";
                this.Sign5Label.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign05LabelValueColumn].ToString() + ":";
                this.Sign6Label.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign06LabelValueColumn].ToString() + ":";
                this.Sign7Label.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign07LabelValueColumn].ToString() + ":";
                ////Set TextBox value
                this.TextTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.SignTextColumn].ToString();
                this.CIDTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.CIDColumn].ToString();
                this.OffSetTextBox.TextCustomFormat = "";
                this.OffSetTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.OffsetColumn].ToString();
                this.RemoveLastZero(this.OffSetTextBox);
                this.DistanceTextBox.TextCustomFormat = "";
                this.DistanceTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.DistFromCrossStColumn].ToString();
                this.RemoveLastZero(this.DistanceTextBox);
                this.AspectTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.AspectColumn].ToString();
                this.WidthTextBox.TextCustomFormat = "";
                this.WidthTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.WidthColumn].ToString();
                this.RemoveLastZero(this.WidthTextBox);
                this.HeightTextBox.TextCustomFormat = "";
                this.HeightTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.HeightColumn].ToString();
                this.RemoveLastZero(this.HeightTextBox);
                this.XValueTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.GPX_XColumn].ToString();
                this.YValueTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.GPX_YColumn].ToString();
                this.SignHeightTextBox.TextCustomFormat = "";
                this.SignHeightTextBox.Text = this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.SignHeightColumn].ToString();
                this.RemoveLastZero(this.SignHeightTextBox);
                ////Set ComboBox selected ID
                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign01_IDColumn].ToString()))
                {
                    this.signOneId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign01_IDColumn]);
                }
                else
                {
                    this.signOneId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign02_IDColumn].ToString()))
                {
                    this.signTwoId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign02_IDColumn]);
                }
                else
                {
                    this.signTwoId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign03_IDColumn].ToString()))
                {
                    this.signThreeId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign03_IDColumn]);
                }
                else
                {
                    this.signThreeId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign04_IDColumn].ToString()))
                {
                    this.signFourId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign04_IDColumn]);
                }
                else
                {
                    this.signFourId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign05_IDColumn].ToString()))
                {
                    this.signFiveId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign05_IDColumn]);
                }
                else
                {
                    this.signFiveId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign06_IDColumn].ToString()))
                {
                    this.signSixId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign06_IDColumn]);
                }
                else
                {
                    this.signSixId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign07_IDColumn].ToString()))
                {
                    this.signSevenId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Sign07_IDColumn]);
                }
                else
                {
                    this.signSevenId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Primary_StreetIDColumn].ToString()))
                {
                    this.streetId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Primary_StreetIDColumn]);
                }
                else
                {
                    this.streetId = 0;
                }

                if (!string.IsNullOrEmpty(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Cross_StreetIDColumn].ToString()))
                {
                    this.crossStreetId = Convert.ToInt32(this.signPropertyData.GetSignsProperty.Rows[0][this.signPropertyData.GetSignsProperty.Cross_StreetIDColumn]);
                }
                else
                {
                    this.crossStreetId = 0;
                }
            }
            else
            {
                this.ClearSignsProperty();
                this.LockControls(false);
            }

            ////To Load All The ComBoBoxs
            this.LoadAllCombOBox();
        }

        #endregion SetValues

        #region Enable Edit

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        #endregion Enable Edit

        #region Load Signs Property
        /// <summary>
        /// To Load the Signs properties.
        /// </summary>
        private void LoadSignsProperties()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.SetControlValues();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
        }
        #endregion Load Signs Property

        #region ComboBox

        #region All Combo Box

        /// <summary>
        /// Loads all combO boxs.
        /// </summary>
        private void LoadAllCombOBox()
        {
            this.LoadAllStreetComboBox();
            this.LoadStreetComboBox();
            this.LoadCrossStreetComboBox();
            this.LoadSignOneComboBox();
            this.LoadSignTwoComboBox();
            this.LoadSignThreeComboBox();
            this.LoadSignFourComboBox();
            this.LoadSignFiveComboBox();
            this.LoadSignSixComboBox();
            this.LoadSignSevenComboBox();
        }

        #endregion  All Combo Box

        #region Street ComboBox

        /// <summary>
        /// To Load StreetCombo Box
        /// </summary>
        private void LoadAllStreetComboBox()
        {
            this.listGDocStreet.Clear();
            DataRow customRow = this.listGDocStreet.NewRow();
            this.listGDocStreet.Clear();
            customRow[this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName] = string.Empty;
            customRow[this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName] = "0";
            this.listGDocStreet.Rows.Add(customRow);

            this.gdocCommonData = this.form84401Control.WorkItem.ListStreets();
            this.gdocListGDocStreetRowsCount = this.gdocCommonData.ListGDocStreet.Rows.Count;
            this.listGDocStreet.Merge(this.gdocCommonData.ListGDocStreet);
        }

        /// <summary>
        /// Loads the north south street combo box.
        /// </summary>
        private void LoadStreetComboBox()
        {
            DataTable streetDataTable = new DataTable();
            streetDataTable.Merge(this.listGDocStreet);
            if (this.gdocListGDocStreetRowsCount > 0)
            {
                this.StreetComboBox.DataSource = streetDataTable;
                this.StreetComboBox.DisplayMember = this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName;
                this.StreetComboBox.ValueMember = this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName;
                bool found = this.CheckAvailableStreet(streetDataTable, this.streetId);
                if (this.streetId > 0 && found)
                {
                    this.StreetComboBox.SelectedValue = this.streetId;
                }
                else
                {
                    this.StreetComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the east west street combo box.
        /// </summary>
        private void LoadCrossStreetComboBox()
        {
            DataTable crossStreetDataTable = new DataTable();
            crossStreetDataTable.Merge(this.listGDocStreet);
            if (this.gdocListGDocStreetRowsCount > 0)
            {
                this.CrossStreetComboBox.DataSource = crossStreetDataTable;
                this.CrossStreetComboBox.DisplayMember = this.gdocCommonData.ListGDocStreet.StreetNameColumn.ColumnName;
                this.CrossStreetComboBox.ValueMember = this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName;
                bool found = this.CheckAvailableStreet(crossStreetDataTable, this.crossStreetId);
                if (this.crossStreetId > 0 && found)
                {
                    this.CrossStreetComboBox.SelectedValue = this.crossStreetId;
                }
                else
                {
                    this.CrossStreetComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Checks the available street.
        /// </summary>
        /// <param name="tempTable">The temp table.</param>
        /// <param name="findId">The find id.</param>
        /// <returns>Flag for available street</returns>
        private bool CheckAvailableStreet(DataTable tempTable, int findId)
        {
            if (tempTable.Rows.Count > 0)
            {
                for (int count = 0; count < tempTable.Rows.Count; count++)
                {
                    if (tempTable.Rows[count][this.gdocCommonData.ListGDocStreet.StreetIDColumn.ColumnName].ToString() == findId.ToString())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion Street ComboBox

        #region Sign ComboBoxes

        /// <summary>
        /// Creates the Temp ListGdocpropertyreference For combo box which refereces the F8000_GetGDocPropertyReference method.
        /// </summary>
        /// <param name="refFieldValue">The ref field value.</param>
        private void CreateTempListGDocPropertyReference(string refFieldValue)
        {
            this.listGDocPropertyReference.Clear();
            DataRow customRow = this.listGDocPropertyReference.NewRow();
            customRow[this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName] = string.Empty;
            customRow[this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName] = "0";
            this.listGDocPropertyReference.Rows.Add(customRow);

            ////To clear the this.gdocCommonData.ListGDocPropertyReference
            this.gdocCommonData.ListGDocPropertyReference.Clear();
            this.gdocCommonData = this.form84401Control.WorkItem.F8000_GetGDocPropertyReference(this.featureClassId, refFieldValue);
            this.gdocPropertyReferenceRowsCount = this.gdocCommonData.ListGDocPropertyReference.Rows.Count;
            this.listGDocPropertyReference.Merge(this.gdocCommonData.ListGDocPropertyReference);
        }

        /// <summary>
        /// Loads the sign one combo box.
        /// </summary>
        private void LoadSignOneComboBox()
        {
            this.CreateTempListGDocPropertyReference("GD_Sign01Label");
            DataTable signOneComboDataTable = new DataTable();
            signOneComboDataTable.Merge(this.listGDocPropertyReference);
            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.Sign1ComboBox.DataSource = signOneComboDataTable;
                this.Sign1ComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.Sign1ComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;
                bool found = this.CheckAvailability(signOneComboDataTable, this.signOneId);
                if (this.signOneId > 0 && found)
                {
                    this.Sign1ComboBox.SelectedValue = this.signOneId;
                }
                else
                {
                    this.Sign1ComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the sign two combo box.
        /// </summary>
        private void LoadSignTwoComboBox()
        {
            this.CreateTempListGDocPropertyReference("GD_Sign02Label");
            DataTable signTwoComboDataTable = new DataTable();
            signTwoComboDataTable.Merge(this.listGDocPropertyReference);
            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.Sign2ComboBox.DataSource = signTwoComboDataTable;
                this.Sign2ComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.Sign2ComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;
                bool found = this.CheckAvailability(signTwoComboDataTable, this.signTwoId);
                if (this.signTwoId > 0 && found)
                {
                    this.Sign2ComboBox.SelectedValue = this.signTwoId;
                }
                else
                {
                    this.Sign2ComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the sign three combo box.
        /// </summary>
        private void LoadSignThreeComboBox()
        {
            this.CreateTempListGDocPropertyReference("GD_Sign03Label");
            DataTable signThreeComboDataTable = new DataTable();
            signThreeComboDataTable.Merge(this.listGDocPropertyReference);
            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.Sign3ComboBox.DataSource = signThreeComboDataTable;
                this.Sign3ComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.Sign3ComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;
                bool found = this.CheckAvailability(signThreeComboDataTable, this.signThreeId);
                if (this.signThreeId > 0 && found)
                {
                    this.Sign3ComboBox.SelectedValue = this.signThreeId;
                }
                else
                {
                    this.Sign3ComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the sign four combo box.
        /// </summary>
        private void LoadSignFourComboBox()
        {
            this.CreateTempListGDocPropertyReference("GD_Sign04Label");
            DataTable signFourComboDataTable = new DataTable();
            signFourComboDataTable.Merge(this.listGDocPropertyReference);
            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.Sign4ComboBox.DataSource = signFourComboDataTable;
                this.Sign4ComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.Sign4ComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;
                bool found = this.CheckAvailability(signFourComboDataTable, this.signFourId);
                if (this.signFourId > 0 && found)
                {
                    this.Sign4ComboBox.SelectedValue = this.signFourId;
                }
                else
                {
                    this.Sign4ComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the sign five combo box.
        /// </summary>
        private void LoadSignFiveComboBox()
        {
            this.CreateTempListGDocPropertyReference("GD_Sign05Label");
            DataTable signFiveComboDataTable = new DataTable();
            signFiveComboDataTable.Merge(this.listGDocPropertyReference);
            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.Sign5ComboBox.DataSource = signFiveComboDataTable;
                this.Sign5ComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.Sign5ComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;
                bool found = this.CheckAvailability(signFiveComboDataTable, this.signFiveId);
                if (this.signFiveId > 0 && found)
                {
                    this.Sign5ComboBox.SelectedValue = this.signFiveId;
                }
                else
                {
                    this.Sign5ComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the sign six combo box.
        /// </summary>
        private void LoadSignSixComboBox()
        {
            this.CreateTempListGDocPropertyReference("GD_Sign06Label");
            DataTable signSixComboDataTable = new DataTable();
            signSixComboDataTable.Merge(this.listGDocPropertyReference);
            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.Sign6ComboBox.DataSource = signSixComboDataTable;
                this.Sign6ComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.Sign6ComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;
                bool found = this.CheckAvailability(signSixComboDataTable, this.signSixId);
                if (this.signSixId > 0 && found)
                {
                    this.Sign6ComboBox.SelectedValue = this.signSixId;
                }
                else
                {
                    this.Sign6ComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Loads the sign seven combo box.
        /// </summary>
        private void LoadSignSevenComboBox()
        {
            this.CreateTempListGDocPropertyReference("GD_Sign07Label");
            DataTable signSevenComboDataTable = new DataTable();
            signSevenComboDataTable.Merge(this.listGDocPropertyReference);
            if (this.gdocPropertyReferenceRowsCount > 0)
            {
                this.Sign7ComboBox.DataSource = signSevenComboDataTable;
                this.Sign7ComboBox.DisplayMember = this.gdocCommonData.ListGDocPropertyReference.ValueColumn.ColumnName;
                this.Sign7ComboBox.ValueMember = this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName;
                bool found = this.CheckAvailability(signSevenComboDataTable, this.signSevenId);
                if (this.signSevenId > 0 && found)
                {
                    this.Sign7ComboBox.SelectedValue = this.signSevenId;
                }
                else
                {
                    this.Sign7ComboBox.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Checks the availability.
        /// </summary>
        /// <param name="tempTable">The temp table.</param>
        /// <param name="findId">The find id.</param>
        /// <returns>flag for availability</returns>
        private bool CheckAvailability(DataTable tempTable, int findId)
        {
            if (tempTable.Rows.Count > 0)
            {
                for (int count = 0; count < tempTable.Rows.Count; count++)
                {
                    if (tempTable.Rows[count][this.gdocCommonData.ListGDocPropertyReference.ItemIDColumn.ColumnName].ToString() == findId.ToString())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion Sign ComboBoxes

        #endregion ComboBox

        #region Save Signs Property

        /// <summary>
        /// Saves the sign property.
        /// </summary>
        private void SaveSignProperty()
        {
            decimal tempOffset;
            decimal tempDistance;
            decimal tempWidth;
            decimal tempHeight;
            decimal tempXCoordinate;
            decimal tempYCoordinate;
            decimal tempSignHeight;
            string signPropertyItems;

            this.signPropertyData.SignsProperty.Rows.Clear();
            F84401SignsPropertyData.SignsPropertyRow dr = this.signPropertyData.SignsProperty.NewSignsPropertyRow();

            if (!string.IsNullOrEmpty(this.CIDTextBox.Text.Trim()))
            {
                dr.CID = this.CIDTextBox.Text.Trim();
            }

            dr.FeatureClassID = Convert.ToInt32(this.featureClassId);

            if (!string.IsNullOrEmpty(this.TextTextBox.Text.Trim()))
            {
                dr.SignText = this.TextTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.OffSetTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.OffSetTextBox.Text.Trim(), out tempOffset);
                dr.Offset = tempOffset;
            }

            if (!string.IsNullOrEmpty(this.DistanceTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.DistanceTextBox.Text.Trim(), out tempDistance);
                dr.DistFromCrossSt = tempDistance;
            }

            if (!string.IsNullOrEmpty(this.AspectTextBox.Text.Trim()))
            {
                dr.Aspect = this.AspectTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.WidthTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.WidthTextBox.Text.Trim(), out tempWidth);
                dr.Width = tempWidth;
            }

            if (!string.IsNullOrEmpty(this.HeightTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.HeightTextBox.Text.Trim(), out tempHeight);
                dr.Height = tempHeight;
            }

            if (!string.IsNullOrEmpty(this.XValueTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.XValueTextBox.Text.Trim(), out tempXCoordinate);
                dr.GPX_X = tempXCoordinate;
            }

            if (!string.IsNullOrEmpty(this.YValueTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.YValueTextBox.Text.Trim(), out tempYCoordinate);
                dr.GPX_Y = tempYCoordinate;
            }

            if (!string.IsNullOrEmpty(this.Sign1ComboBox.Text.Trim()))
            {
                dr.Sign01_ID = Convert.ToInt32(this.Sign1ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.Sign2ComboBox.Text.Trim()))
            {
                dr.Sign02_ID = Convert.ToInt32(this.Sign2ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.Sign3ComboBox.Text.Trim()))
            {
                dr.Sign03_ID = Convert.ToInt32(this.Sign3ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.Sign4ComboBox.Text.Trim()))
            {
                dr.Sign04_ID = Convert.ToInt32(this.Sign4ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.Sign5ComboBox.Text.Trim()))
            {
                dr.Sign05_ID = Convert.ToInt32(this.Sign5ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.Sign6ComboBox.Text.Trim()))
            {
                dr.Sign06_ID = Convert.ToInt32(this.Sign6ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.Sign7ComboBox.Text.Trim()))
            {
                dr.Sign07_ID = Convert.ToInt32(this.Sign7ComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.StreetComboBox.Text.Trim()))
            {
                dr.Primary_StreetID = Convert.ToInt32(this.StreetComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.CrossStreetComboBox.Text.Trim()))
            {
                dr.Cross_StreetID = Convert.ToInt32(this.CrossStreetComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.SignHeightTextBox.Text.Trim()))
            {
                Decimal.TryParse(this.SignHeightTextBox.Text.Trim(), out tempSignHeight);
                dr.SignHeight = tempSignHeight;
            }

            this.signPropertyData.SignsProperty.Rows.Add(dr);
            signPropertyItems = Utility.GetXmlString(this.signPropertyData.SignsProperty.Copy());

            if (this.newMode)
            {
                this.signId = 0;
            }

            // DB call for Save 
            int returnValue = this.form84401Control.WorkItem.F84401_SaveSignsProperties(this.signId, signPropertyItems, TerraScanCommon.UserId);

            // Reload form after save
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            this.newMode = false;
            this.Cursor = Cursors.Default;
        }

        #endregion Save Signs Property

        #endregion Methods

        #region Page_Load
        /// <summary>
        /// Handles the Load event of the F84401 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F84401_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.FlagSliceForm = true;
                //// Set Max length for all controls
                this.SetMaxLength();
                //// Load sign details
                this.LoadSignsProperties();
                //// Lock controls based on permission
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                //// Set focus on first editable field
                this.TextTextBox.Focus();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        #endregion Page_Load

        #region Events

        /// <summary>
        /// Handles the Click event of the SignPropertyPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SignPropertyPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                //// Expand / Collapse form slice
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the SignPropertyPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SignPropertyPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                //// Show Tool Tip 
                this.GDocSignsPropertyToolTip.SetToolTip(this.SignPropertyPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the edit button in master form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableEditButtonInMasterForm(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    //// Enable form master Save/Cancel buttons
                    this.EditEnabled();
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
        /// Handles the SelectedIndexChanged event of the Sign1ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Sign1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                this.Cursor = Cursors.WaitCursor;
                CommentsData imageDataSet = new CommentsData();
                //// Get image location
                imageDataSet = this.form84401Control.WorkItem.GetConfigDetails("GD_IconPath");
                if (imageDataSet.Tables.Count > 0 && imageDataSet.Tables[imageDataSet.GetCommentsConfigDetails.TableName].Rows.Count > 0 && this.Sign1ComboBox.SelectedIndex > 0)
                {
                    //// Set image on picture box
                    this.SignImage.ImageLocation = imageDataSet.GetCommentsConfigDetails.Rows[0][imageDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString() + "\\" + this.Sign1ComboBox.SelectedValue.ToString() + ".jpg";
                    //// If file exists show image
                    if (System.IO.File.Exists(this.SignImage.ImageLocation.ToString()))
                    {
                        this.SignImage.Visible = true;
                    }
                    else
                    {
                        this.SignImage.Visible = false;
                    }
                }
                else
                {
                    this.SignImage.Visible = false;
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

        #endregion Events

        /// <summary>
        /// Handles the Leave event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TerraScanTextBox textbox = (TerraScanTextBox)sender;
            if (!string.IsNullOrEmpty(textbox.Text.Trim()))
            {
                string decimalvalue = textbox.DecimalTextBoxValue.ToString();
                int leng = decimalvalue.Length;
                // to get the decimal position.
                int decPos = decimalvalue.IndexOf(".");
                //to check if decimal places are avilable.
                if (decPos != -1)
                {
                    if (leng - (decPos + 1) > 0)
                    {
                        // To get decimal part
                        decimalvalue = decimalvalue.ToString().Substring(decPos + 1, leng - (decPos + 1)).Trim();
                        int declength = decimalvalue.Length;
                        if (declength.Equals(1))
                        {
                            textbox.TextCustomFormat = "#,##0.0";
                            textbox.Text = textbox.Text.Trim();
                        }
                        else
                        {
                            textbox.TextCustomFormat = "#,##0.00";
                            textbox.Text = textbox.Text.Trim();
                        }
                    }
                }
                else
                {
                    textbox.TextCustomFormat = "#,##0.0";
                    textbox.Text = textbox.Text.Trim();
                }
            }
            else
            {
                textbox.TextCustomFormat = "#,##0.0";
                textbox.Text = textbox.Text.Trim();  
            }
        }

        /// <summary>
        /// Removes the last zero.
        /// </summary>
        /// <param name="textbox">The textbox.</param>
        private void RemoveLastZero(TerraScanTextBox textbox)
        {
            if (!string.IsNullOrEmpty(textbox.Text.Trim()))
            {
                string decimalvalue = textbox.DecimalTextBoxValue.ToString();
                int leng = decimalvalue.Length;
                // to get the decimal position.
                int decPos = decimalvalue.IndexOf(".");
                //to check if decimal places are avilable.
                if (decPos != -1)
                {
                    if (leng - (decPos + 1) > 0)
                    {
                        // To get decimal part
                        decimalvalue = decimalvalue.ToString().Substring(decPos + 1, leng - (decPos + 1)).Trim();
                        int declength = decimalvalue.Length;
                        if (declength.Equals(2))
                        {
                            if (decimalvalue.EndsWith("0"))
                            {
                                textbox.Text = textbox.Text.Trim().Substring(0, leng - 1);
                                textbox.TextCustomFormat = "#,##0.0";
                                textbox.Text = textbox.Text.Trim().ToString();
                            }
                            else
                            {
                                textbox.TextCustomFormat = "#,##0.00";
                                textbox.Text = textbox.Text.Trim().ToString();
                            }
                        }
                        else
                        {
                            textbox.TextCustomFormat = "#,##0.00";
                            textbox.Text = textbox.Text.Trim().ToString();
                        }
                    }
                }
            }
            else
            {
                textbox.TextCustomFormat = "#,##0.0";
                textbox.Text = textbox.Text.Trim().ToString();
            }
        }
    }
}
