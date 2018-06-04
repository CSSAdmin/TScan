//--------------------------------------------------------------------------------------------
// <copyright file="F27010.cs" company="Congruent">
//      Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author             Description
// ----------        ---------       ---------------------------------------------------------
// 26 Mar 08      D.LathaMaheswari     Created
// 16/07/2009     D.LathaMaheswari     CO:#1459 -  MAD type 7 (Fire District) Calculation has been changed
// *********************************************************************************/
namespace D22000
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// form27010Control Controller
    /// </summary>
    public partial class F27010 : BaseSmartPart
    {
        #region Member Variable
        /// <summary>
        /// form27010Control Controller
        /// </summary>
        private F27010Controller form27010Control;

        /// <summary>
        /// Unique keyId from the Form Master - statement id
        /// </summary>
        private int keyId;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Misc Assessment DataSet
        /// </summary>
        private F27010MiscAssessmentData assessmentData = new F27010MiscAssessmentData();

        /// <summary>
        /// Roll Year for the Form
        /// </summary>
        private int rollYear;

        /// <summary>
        /// combo Font
        /// </summary>
        private System.Drawing.Font comboFont;

        /// <summary>
        ///  used to fill the Color 
        /// </summary>
        private SolidBrush comboFontColor = new SolidBrush(System.Drawing.Color.Blue);

        /// <summary>
        ///  used to store AssessmentComboBox XML String
        /// </summary>
        private string assessmentValue;

        /// <summary>
        ///  used to store DistrictComboBox XML String
        /// </summary>
        private string districtValue;

        /// <summary>
        ///  used to store assessmentComboBox Data
        /// </summary>
        private DataSet assessmentDataSet = new DataSet();

        /// <summary>
        ///  used to store DistrictComboBox Data
        /// </summary>
        private DataSet districtDataSet = new DataSet();

        /// <summary>
        ///  used to store Grid Data
        /// </summary>
        private DataSet miscDataSet = new DataSet();

        /// <summary>
        ///  used to store Message Data
        /// </summary>
        private DataSet messageDataSet = new DataSet();

        /// <summary>
        /// Store misc data
        /// </summary>
        private DataSet tempMiscData = new DataSet();

        /// <summary>
        /// Flag for save
        /// </summary>
        private bool afterSave;

        /// <summary>
        /// District Type ID
        /// </summary>
        private int districtTypeId;

        /// <summary>
        /// Flag for Form Load
        /// </summary>
        private bool formLoad;

        /// <summary>
        /// TR_LevyDisplayOffset value from tTS_cfg
        /// </summary>
        private int leavyOffSet;

        /// <summary>
        /// Maximum allowed levy offset value is 31
        /// </summary>
        private int maxLevy = 31;

        #endregion Member Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F27010"/> class.
        /// </summary>
        public F27010()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F27010"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green colr.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F27010(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.HeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.HeaderPictureBox.Height, this.HeaderPictureBox.Width, string.Empty, red, green, blue);
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
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion

        #region Enum

        /// <summary>
        /// Enumerator for Assessment Type
        /// </summary>
        private enum AssessmentTypes
        {
            /// <summary>
            /// Value for GWMA
            /// </summary>
            GWMA = 1,

            /// <summary>
            /// Value for Weed
            /// </summary>
            Weed = 2,

            /// <summary>
            /// Value for Pest Control
            /// </summary>
            PestControl = 3,

            /// <summary>
            /// Value for Mosquito
            /// </summary>
            Mosquito = 4,

            /// <summary>
            /// Value for Drainage
            /// </summary>
            Drainage = 5,

            /// <summary>
            /// Value for DNR
            /// </summary>
            DNR = 6,

            /// <summary>
            /// Value for Fire District
            /// </summary>
            FireDistrict = 7,

            /// <summary>
            /// Value for Conservation
            /// </summary>
            Conservation = 8,

            /// <summary>
            /// Value for Lake District
            /// </summary>
            LakeDistrict = 9,

            /// <summary>
            /// Value for Milfoil District
            /// </summary>
            MilfoilDistrict = 10,

            /// <summary>
            /// Value for Irrigation
            /// </summary>
            Irrigation = 11,

            /// <summary>
            /// Value for Lighting
            /// </summary>
            Lighting = 12

        }

        #endregion Enum

        #region Properties

        /// <summary>
        /// Gets or sets the F27010 control.
        /// </summary>
        /// <value>The F27010 control.</value>
        [CreateNew]
        public F27010Controller Form27010Control
        {
            get { return this.form27010Control as F27010Controller; }
            set { this.form27010Control = value; }
        }

        #endregion

        #region Event Subscription

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
                this.Cursor = Cursors.WaitCursor;
                this.GetRollYear();
                this.LoadAssessmentTypeCombo();
                this.SetGridVisibility();
                ////this.DistrictComboBox.Enabled = false;
                this.ShowControls(false);
                this.SetDistrictLabelText();
                this.SetDistrictComboColor();
                this.AssessmentTypeComboBox.Focus();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.SaveMiscAssessment();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

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
                if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    ////check wether the form is populated with records - based on the keyid                    
                    if (this.rollYear > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.formLoad = true;
                        this.LoadControls();
                        ////Set Default selection
                        this.AssessmentTypeComboBox.Focus();
                        this.formLoad = false;
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
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
                {
                    this.formLoad = true;

                    if (!this.afterSave)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        //this.AssessmentTypeComboBox.Focus();
                        this.FlagSliceForm = true;
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        this.CustomizeGridView();
                        this.GetRollYear();
                        this.LoadAssessmentTypeCombo();
                        this.LoadDistrictCombo();
                        this.SetGridVisibility();
                        //this.ActiveControl = this.AssessmentTypeComboBox;
                        //this.AssessmentTypeComboBox.Focus();
                       // this.ActiveControl.Focus();
                        ////this.DistrictComboBox.Enabled = false;
                        this.ShowControls(false);
                        this.AssessmentTypeComboBox.SelectedIndex = 0;
                        this.DistrictComboBox.SelectedIndex = -1;
                        this.ExemptComboBox.SelectedIndex = 0;
                        this.OverrideCheckBox.Checked = false;
                        this.SetDistrictLabelText();
                        this.SetDistrictComboColor();
                        this.MiscAssessmentGridView.DataSource = null;
                        this.SetGridHeight();
                        this.AssessmentTypeComboBox.Focus();
                    }
                    else
                    {
                        this.GetMiscAssessmentDetails();
                        this.afterSave = false;
                        this.AssessmentTypeComboBox.Focus();
                    }

                    this.formLoad = false;
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
                if (this.MiscAssessmentGridView.OriginalRowCount > 1)
                {
                    this.Height = 45;
                }
                else if (this.MiscAssessmentGridView.OriginalRowCount.Equals(1))
                {
                    this.Height = 110;
                }
                else
                {
                    this.Height = 45;
                }

                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
                this.GetMiscAssessmentDetails();
            }
        }

        #endregion

        #region Regular Expression

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is integer; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsInteger(string value)
        {
            return IsMatch(value, @"^([0-9]*|[0-9]*(\.[0-9])[0-9]*)$");
        }

        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///  <c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsMatch(string value, string pattern)
        {
            System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex(@pattern);
            if (objRegex.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Regular Expression

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F27010 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F27010_Load(object sender, EventArgs e)
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.FlagSliceForm = true;
                this.formLoad = true;
                this.Cursor = Cursors.WaitCursor;
                this.CustomizeGridView();
                this.GetRollYear();
                this.LoadAssessmentTypeCombo();
                this.LoadDistrictCombo();
                this.SetGridVisibility();
                ////this.DistrictComboBox.Enabled = false;
                this.ShowControls(false);
                this.FillComboBoxes();
                this.SetGridHeight();
                ////Set Label Text
                this.SetDistrictLabelText();
                this.SetDistrictComboColor();
                this.AssessmentTypeComboBox.Focus();
                this.formLoad = false;
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

        #region Events

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AssessmentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssessmentTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.formLoad = true;
                }
                else
                {
                    this.formLoad = false;
                }

                if (this.AssessmentTypeComboBox.SelectedIndex > 0 && this.AssessmentTypeComboBox.SelectedValue.ToString() != "5"
                    && this.AssessmentTypeComboBox.SelectedValue.ToString() != "11" && this.AssessmentTypeComboBox.SelectedValue.ToString() != "12"
                    && this.AssessmentTypeComboBox.SelectedValue.ToString() != "10")
                {
                    this.ComboLinkLabel.BringToFront();
                    this.MiscAssessmentGridView.BringToFront();
                    this.MiscAssessmentGridVscrollBar.BringToFront();
                    ////this.DistrictComboBox.Enabled = true;
                    this.ShowControls(true);
                    this.Cursor = Cursors.WaitCursor;
                    this.districtTypeId = Convert.ToInt32(this.AssessmentTypeComboBox.SelectedValue);

                    // Get TR_LevyDisplayOffset from tTS_cfg for Assessment Type 'FireDistrict' - CO:#1459
                    if (this.districtTypeId.Equals((byte)AssessmentTypes.FireDistrict))
                    {
                        CommentsData leavyRateDataSet = new CommentsData();
                        leavyRateDataSet = this.form27010Control.WorkItem.GetConfigDetails("TR_LevyDisplayOffset");
                        
                        if (leavyRateDataSet.GetCommentsConfigDetails.Rows.Count > 0)
                        {
                            int.TryParse(leavyRateDataSet.GetCommentsConfigDetails.Rows[0][leavyRateDataSet.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.leavyOffSet);
                        }

                        // To avoid Arithmatic Overflow Error
                        if (this.leavyOffSet > this.maxLevy)
                        {
                            this.leavyOffSet = 0;
                        }
                    }

                    this.CustomizeGridView();
                    this.GetMiscAssessmentDetails();
                }
                else
                {
                    ////this.DistrictComboBox.Enabled = false;
                    this.ShowControls(false);
                    this.DistrictComboBox.SelectedIndex = -1;
                    this.DistrictLabel.Text = "District:";
                    this.DistrictPanel.BackColor = Color.FromArgb(255, 255, 255);
                    this.DistrictComboBox.BackColor = Color.FromArgb(255, 255, 255);
                    this.DistrictComboBox.BringToFront();
                    this.MiscAssessmentPanel.Visible = false;
                    this.MiscAssessmentPictureBox.Visible = false;
                    this.districtTypeId = 0;
                    this.MiscAssessmentPanel.Height = 0;
                    this.MiscAssessmentGridView.Height = 0;
                    this.MiscAssessmentGridVscrollBar.Height = 0;
                    this.MiscAssessmentPictureBox.Height = 0;
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    this.Height = 45;
                    sliceResize.SliceFormHeight = this.Height;
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                }

                this.AssessmentTypeComboBox.Focus();
                this.formLoad = false;
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

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DistrictComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.formLoad = false;
                this.SetEditRecord();
                this.SetLinkText();
                if (this.DistrictComboBox.SelectedIndex >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    int madTypeId = 0;

                    ////Get Default District
                    if (this.AssessmentTypeComboBox.SelectedIndex > 0)
                    {
                        madTypeId = Convert.ToInt32(this.AssessmentTypeComboBox.SelectedValue.ToString());
                    }

                    int madId = 0;
                    if (this.DistrictComboBox.SelectedIndex >= 0)
                    {
                        madId = Convert.ToInt32(this.DistrictComboBox.SelectedValue.ToString());
                    }

                    this.messageDataSet.Tables.Clear();
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetMessage(this.keyId, madTypeId, madId);
                    this.messageDataSet.Tables.Add(this.assessmentData.GetMessageTable.Copy());
                    this.LoadAssessmentGrid();
                    this.SetGridHeight();
                    this.SetGridVisibility();
                }
                else
                {
                    this.MiscAssessmentPanel.Visible = false;
                    this.MiscAssessmentPictureBox.Visible = false;
                    this.DistrictComboBox.Focus();
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the DistrictComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictComboBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.SetToolTipValue(this.DistrictComboBox);
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
        /// Sets the tool tip value.
        /// </summary>
        /// <param name="controlName">Control Name</param>
        private void SetToolTipValue(Control controlName)
        {
            if (this.DistrictComboBox.SelectedIndex >= 0 && this.AssessmentTypeComboBox.SelectedValue.ToString() != "5")
            {
                ////Set ToolTip Text
                string message = string.Empty;
                string[] tooltipArray;
                char[] delimiter = { '.' };

                if (this.messageDataSet.Tables[0].Rows.Count > 0)
                {
                    string test = this.messageDataSet.Tables[0].Rows[0].ItemArray[1].ToString();
                    test = test.Replace("\\n", "");
                    if (this.messageDataSet.Tables.Count > 0)
                    {
                        tooltipArray = test.Split(delimiter);
                        if (tooltipArray.Length > 0)
                        {
                            if (string.IsNullOrEmpty(tooltipArray[1].ToString()))
                            {
                                message = this.DistrictComboBox.Text + "\n \n" + tooltipArray[0].ToString().Trim() + ".";
                            }
                            else
                            {
                                message = this.DistrictComboBox.Text + "\n \n" + tooltipArray[0].ToString() + "." + " \n" + tooltipArray[1].ToString() + ".";
                            }
                        }
                    }
                }
                this.MiscToolTip.SetToolTip(controlName, message);
            }
            else
            {
                this.MiscToolTip.SetToolTip(controlName, string.Empty);
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
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
        /// Handles the CellEndEdit event of the MiscAssessmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MiscAssessmentGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //////Calculate assessment column value and bind in the Grid
                this.miscDataSet.Tables[0].AcceptChanges();
                this.MiscAssessmentGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (this.districtTypeId < 4)
                {
                    decimal tempTaxFeeDue = 0;
                    decimal.TryParse(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString().Replace(",", "").Replace("(", "").Replace(")", ""), out tempTaxFeeDue);
                    if (string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString()) && this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString() == "")
                    {
                        this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value = this.ResetAcreValue(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString());
                    }
                    else
                    {
                        this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value = tempTaxFeeDue;
                        this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value = this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value;
                    }
                }

                this.CalculateAssessment();
                this.SumOfAcres();
                this.SumOfAssessment();
                this.SetReadOnly();
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
        /// Handles the DrawItem event of the DistrictComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void DistrictComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            this.comboFont = new Font(SharedFunctions.GetResourceString("Arial"), 10, FontStyle.Underline);
            e.Graphics.DrawString(this.DistrictComboBox.SelectedText, this.comboFont, this.comboFontColor, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
        }

        /// <summary>
        /// Handles the CellFormatting event of the MiscAssessmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void MiscAssessmentGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                ////For  Currency column 
                decimal outAmount;
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName].Index) 
                    || e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName].Index) 
                    || e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName].Index) 
                    || e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.BaseFee.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.BaseFee.Name].Index))
                        {
                            ////For Two Decimals
                            if (Decimal.TryParse(val, out outAmount))
                            {
                                e.Value = "$ " + outAmount.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "$ 0.00";
                            }
                        }
                        else
                        {
                            ////For Four Decimals
                            if (Decimal.TryParse(val, out outAmount))
                            {
                                e.Value = "$ " + outAmount.ToString("#,##0.0000");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "$ 0.0000";
                            }
                        }
                    }
                    else
                    {
                        if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.BaseFee.Name].Index))
                        {
                            e.Value = "$ 0.00";
                        }
                        else
                        {
                            e.Value = "$ 0.0000";
                        }
                    }
                }

                ////For Four Decimal Places
                decimal outFourDecimal;
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.ResidentialRate.Name].Index) || e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.CommercialRate.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outFourDecimal))
                        {
                            e.Value = outFourDecimal.ToString("#,##0.0000");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.0000";
                        }
                    }
                    else
                    {
                        e.Value = "0.0000";
                    }
                }

                ////For Two Decimal Places
                decimal outTwoDecimal;
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.FirePatrolAcres.Name].Index) 
                    || e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.FPARate.Name].Index) 
                    || e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.LCFRate.Name].Index) 
                    || e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.BenefitCharge.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outTwoDecimal))
                        {
                            e.Value = outTwoDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "0.00";
                    }
                }

                decimal outAcre;

                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.Acres.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outAcre))
                        {
                            if (outAcre.ToString().Contains("-"))
                            {
                                e.Value = "0.00";
                            }
                            else
                            {
                                e.Value = outAcre.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        if (this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Minimum") 
                            || this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") 
                            || this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                        {
                            e.Value = "";
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                }

                decimal feeValue;
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out feeValue))
                        {
                            e.Value = "$ " + feeValue.ToString("#,##0.0000");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "$ 0.0000";
                        }
                    }
                    else
                    {
                        e.Value = "$ 0.0000";
                    }
                }

                decimal rateValue;
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.Rate.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.Rate.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out rateValue))
                        {
                            if (this.districtTypeId.Equals((byte)AssessmentTypes.LakeDistrict))
                            {
                                e.Value = rateValue.ToString("#,##0.000000");
                            }
                            else
                            {
                                e.Value = rateValue.ToString("#,##0.0000");
                            }
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            if (this.districtTypeId.Equals((byte)AssessmentTypes.LakeDistrict))
                            {
                                e.Value = "0.000000";
                            }
                            else
                            {
                                e.Value = "0.0000";
                            }
                        }
                    }
                    else
                    {
                        if (this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Minimum") 
                            || this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") 
                            || this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                        {
                            e.Value = "";
                        }
                        else
                        {
                            if (this.districtTypeId.Equals((byte)AssessmentTypes.LakeDistrict))
                            {
                                e.Value = "0.000000";
                            }
                            else
                            {
                                e.Value = "0.0000";
                            }
                        }
                    }
                }

                decimal assessmentValue;
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.Assessment.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.Assessment.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out assessmentValue))
                        {
                            e.Value = "$ " + assessmentValue.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "$ 0.00";
                    }
                }

                ////For Fire District - 6
                if (this.districtTypeId.Equals(6))
                {
                    Decimal firePatrolAcre;
                    if (e.ColumnIndex == this.MiscAssessmentGridView.Columns[this.FirePatrolAcres.Name].Index)
                    {
                        if (e.RowIndex < 0)
                        {
                            return;
                        }

                        if (e.Value != null && !String.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.FirePatrolAcres.Name].Value.ToString()))
                        {
                            string val = e.Value.ToString();

                            if (Decimal.TryParse(val, out firePatrolAcre))
                            {
                                e.Value = firePatrolAcre.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0.00";
                            }
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                }

                decimal beneftCharges;
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.BenefitCharge.Name].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[this.BenefitCharge.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out beneftCharges))
                        {
                            e.Value = beneftCharges.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the MiscAssessmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void MiscAssessmentGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.MiscAssessmentGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellLeave event of the MiscAssessmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T    :System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MiscAssessmentGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(this.MiscAssessmentGridView.Columns[this.Acres.Name].Index))
                {
                    this.MiscAssessmentGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    ////this.tempAcre = this.MiscAssessmentGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }

                this.MiscAssessmentGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DropDownClosed event of the DistrictComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                this.SetLinkText();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ComboLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComboLinkLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.SetToolTipValue(this.ComboLinkLabel);
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
        /// Handles the LinkClicked event of the ComboLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ComboLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.DistrictComboBox.SelectedIndex >= 0)
                {
                    int districtId = Convert.ToInt32(this.DistrictComboBox.SelectedValue.ToString());
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(22000);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = districtId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the Click event of the MiscAssessmentPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscAssessmentPictureBox_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Handles the MouseHover event of the MiscAssessmentPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscAssessmentPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.MiscToolTip.RemoveAll();
                this.MiscToolTip.SetToolTip(this.MiscAssessmentPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the HeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HeaderPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.MiscToolTip.RemoveAll();
                this.MiscToolTip.SetToolTip(this.HeaderPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the DistrictComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictComboBox_Leave(object sender, EventArgs e)
        {
            this.SetDistrictComboColor();
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the MiscAssessmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void MiscAssessmentGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                this.SetReadOnly();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ExemptComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExemptComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.formLoad)
                {
                    this.SetEditRecord();
                }

                this.SumOfAssessment();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the OverrideValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OverrideValueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.formLoad)
                {
                    this.SetEditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the OverrideCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OverrideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.formLoad)
                {
                    this.SetEditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events

        #region Private Methods

        #region ControlsVisibility
        /// <summary>
        /// Loads the controls.
        /// </summary>
        private void LoadControls()
        {
            bool permissionFields = this.slicePermissionField.editPermission;
            this.AssessmentTypeComboBox.Enabled = permissionFields;
            this.DistrictComboBox.Enabled = permissionFields;
            this.MiscAssessmentGridView.Enabled = permissionFields;
            this.ExemptComboBox.Enabled = permissionFields;
            this.OverrideCheckBox.Enabled = permissionFields;
            this.OverrideValueTextBox.Enabled = permissionFields;
            ////Check Form's Edit permission
            if (permissionFields)
            {
                this.SetDistrictLabelText();
            }

            this.SetReadOnly();
        }
        #endregion ControlsVisibility

        #region RollYear
        /// <summary>
        /// Gets the roll year.
        /// </summary>
        private void GetRollYear()
        {
            ////Get the Roll Year based on the ParcelID
            this.rollYear = this.form27010Control.WorkItem.F27010GetRollYear(this.keyId);
        }
        #endregion RollYear

        #region AssessmentTypeComboBox
        /// <summary>
        /// Loads the assessment type combo.
        /// </summary>
        private void LoadAssessmentTypeCombo()
        {
            this.assessmentDataSet.Tables.Clear();
            F27010MiscAssessmentData.AssessmentTypeTableDataTable assessmentTable = new F27010MiscAssessmentData.AssessmentTypeTableDataTable();
            assessmentTable = this.form27010Control.WorkItem.F27010GetAssessmentType(this.rollYear).AssessmentTypeTable;
            this.AssessmentTypeComboBox.DataSource = assessmentTable.Copy();
            this.AssessmentTypeComboBox.ValueMember = assessmentTable.KeyIDColumn.ToString();
            this.AssessmentTypeComboBox.DisplayMember = assessmentTable.KeyNameColumn.ToString();
            this.assessmentDataSet.Tables.Add(assessmentTable.Copy());
        }
        #endregion AssessmentTypeComboBox

        #region DistrictComboBox
        /// <summary>
        /// Loads the district combo.
        /// </summary>
        private void LoadDistrictCombo()
        {
            this.districtDataSet.Tables.Clear();
            int madTypeId = 0;
            if (this.AssessmentTypeComboBox.SelectedValue!= null && this.AssessmentTypeComboBox.SelectedIndex >= 0)
            {
                madTypeId = Convert.ToInt32(this.AssessmentTypeComboBox.SelectedValue.ToString());
            }

            F27010MiscAssessmentData.DistrictTableDataTable districtTable = new F27010MiscAssessmentData.DistrictTableDataTable();
            districtTable = this.form27010Control.WorkItem.F27010GetDistrict(this.keyId, madTypeId, this.rollYear).DistrictTable;
            this.DistrictComboBox.DataSource = districtTable.Copy();
            this.DistrictComboBox.ValueMember = districtTable.KeyIDColumn.ToString();
            this.DistrictComboBox.DisplayMember = districtTable.KeyNameColumn.ToString();
            this.districtDataSet.Tables.Add(districtTable.Copy());
        }

        /// <summary>
        /// Sets the color of the district combo.
        /// </summary>
        private void SetDistrictComboColor()
        {
            if (this.messageDataSet.Tables.Count > 0 && this.messageDataSet.Tables[0].Rows.Count > 0 && this.DistrictComboBox.SelectedIndex >= 0)
            {
                if (this.messageDataSet.Tables[0].Rows[0].ItemArray[0].ToString().Equals("True"))
                {
                    this.DistrictPanel.BackColor = Color.FromArgb(179, 200, 255);
                    this.DistrictComboBox.BackColor = Color.FromArgb(179, 200, 255);
                }
                else
                {
                    this.DistrictPanel.BackColor = Color.FromArgb(255, 255, 255);
                    this.DistrictComboBox.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            else
            {
                this.DistrictPanel.BackColor = Color.FromArgb(255, 255, 255);
                this.DistrictComboBox.BackColor = Color.FromArgb(255, 255, 255);
            }
        }
        #endregion DistrictComboBox

        #region AssessmentGrid
        /// <summary>
        /// Loads the assessment grid.
        /// </summary>
        private void LoadAssessmentGrid()
        {
            if (this.AssessmentTypeComboBox.SelectedIndex > 0)
            {
                int madistrictId = 0;
                if (this.DistrictComboBox.SelectedIndex >= 0)
                {
                    madistrictId = Convert.ToInt32(this.DistrictComboBox.SelectedValue.ToString());
                }

                if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.GWMA).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetMiscData(madistrictId, this.keyId);
                }
                else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.Weed).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetOthersMiscData(madistrictId, this.keyId, "f27010_pclst_MAType2");
                }
                else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.PestControl).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetOthersMiscData(madistrictId, this.keyId, "f27010_pclst_MAType3");
                }
                else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.Mosquito).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetOthersMiscData(madistrictId, this.keyId, "f27010_pclst_MAType4");
                }
                else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.DNR).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetOthersMiscData(madistrictId, this.keyId, "f27010_pclst_MAType6");
                }
                else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.FireDistrict).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetOthersMiscData(madistrictId, this.keyId, "f27010_pclst_MAType7");
                }
                else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.Conservation).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetOthersMiscData(madistrictId, this.keyId, "f27010_pclst_MAType8");
                }
                else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.LakeDistrict).ToString()))
                {
                    this.assessmentData = this.form27010Control.WorkItem.F27010GetOthersMiscData(madistrictId, this.keyId, "f27010_pclst_MAType9");
                }

                this.FormatGrid();
                this.SetGridVisibility();
                this.BindControls();
                this.miscDataSet.Tables.Clear();
                this.miscDataSet.Tables.Add(this.assessmentData.ListMiscAssessmentTable.Copy());

                this.tempMiscData.Tables.Clear();
                this.tempMiscData.Tables.Add(this.assessmentData.ListMiscAssessmentTable.Copy());

                this.MiscAssessmentGridView.DataSource = this.assessmentData.ListMiscAssessmentTable.DefaultView;

                this.CalculateAssessment();
                this.SetReadOnly();
                this.SumOfAcres();
                this.SumOfAssessment();
            }
        }

        /// <summary>
        /// Calculates the assessment.1
        /// </summary>
        private void CalculateAssessment()
        {
            ////Get the row count which contains values (>0.00) in acres field 
            int validAcreCount = 0;
            for (int count = 0; count < this.MiscAssessmentGridView.OriginalRowCount; count++)
            {
                if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[count].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString()) && Convert.ToDouble(this.MiscAssessmentGridView.Rows[count].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString()) > 0)
                {
                    if ((this.MiscAssessmentGridView.Rows[count].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString() != "Base") && (this.MiscAssessmentGridView.Rows[count].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString() != "Base Land"))
                    {
                        validAcreCount = validAcreCount + 1;
                    }
                }
            }

            double assessmentSum = 0;
            if (this.MiscAssessmentGridView.OriginalRowCount > 0)
            {
                // Assessment calculation for TypeID 1, 2, 3 and 4
                if (this.MiscAssessmentGridView.OriginalRowCount > 1)
                {
                    double siteBaseRate = 0.00;
                    double baseFee = 0.00;
                    if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.GWMA).ToString()))
                    {
                        string filterString = this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName + " = 'Site Land'";
                        F27010MiscAssessmentData.ListMiscAssessmentTableRow[] filteredRow = (F27010MiscAssessmentData.ListMiscAssessmentTableRow[])this.assessmentData.ListMiscAssessmentTable.Select(filterString);

                        if (filteredRow.Length > 0 && !filteredRow[0].IsFeesNull())
                        {
                            double.TryParse(filteredRow[0].Fees.ToString().Trim(), out siteBaseRate);
                        }

                        filterString = this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName + " = 'Base' OR " + this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName + " = 'Base Land'";
                        filteredRow = (F27010MiscAssessmentData.ListMiscAssessmentTableRow[])this.assessmentData.ListMiscAssessmentTable.Select(filterString);
                        if (filteredRow.Length > 0 && !filteredRow[0].IsFeesNull())
                        {
                            double.TryParse(filteredRow[0].Fees.ToString().Trim(), out baseFee);
                        }
                    }
                    
                    ////bool hasBaseValue = false;
                    for (int i = 0; i < this.MiscAssessmentGridView.OriginalRowCount; i++)
                    {
                        if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Minimum"))
                        {
                            assessmentSum = 0;
                            for (int j = 0; j < this.MiscAssessmentGridView.OriginalRowCount; j++)
                            {
                                if (!this.MiscAssessmentGridView.Rows[j].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Minimum"))
                                {
                                    assessmentSum = assessmentSum + Convert.ToDouble(this.MiscAssessmentGridView.Rows[j].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value.ToString());
                                }
                            }

                            Double feeValue = 0;
                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Value.ToString()))
                            {
                                feeValue = Convert.ToDouble(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Value.ToString());
                            }

                            if (assessmentSum < feeValue)
                            {
                                this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value = feeValue - assessmentSum;
                            }
                            else
                            {
                                this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value = 0.00;
                            }
                        }
                        else
                        {
                            Double feeAmount = 0;
                            Double rateAmt = 0;
                            Double acreVal = 0;
                            int minAcre = 0;
                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Value.ToString()))
                            {
                                feeAmount = Convert.ToDouble(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Value.ToString());
                            }

                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Value.ToString()))
                            {
                                rateAmt = Convert.ToDouble(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Value.ToString());
                            }

                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString()) && this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString() != "")
                            {
                                acreVal = Convert.ToDouble(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString());
                                if (acreVal > 0)
                                {
                                    acreVal = Math.Round(acreVal, 2, MidpointRounding.AwayFromZero);
                                }
                            }

                            #region Assessment Calculation

                            if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.GWMA).ToString()))
                            {
                                // AssessmentType 1 (GWMA)
                                if (validAcreCount.Equals(0))
                                {
                                    // i)   No values in "acres"
                                    if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") || this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                                    {
                                        if (siteBaseRate > 0)
                                        {
                                            assessmentSum = 0.00;
                                        }
                                        else
                                        {
                                            assessmentSum = feeAmount;
                                        }
                                    }
                                    else
                                    {
                                        if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Site Land"))
                                        {
                                            assessmentSum = siteBaseRate;
                                        }
                                        else
                                        {
                                            assessmentSum = rateAmt * acreVal;
                                        }
                                    }
                                }
                                else if (validAcreCount.Equals(1))
                                {
                                    // Only one Land contains "Acre" value
                                    if (acreVal > 0)
                                    {
                                        assessmentSum = feeAmount + (rateAmt * acreVal);
                                    }
                                    else
                                    {
                                        assessmentSum = acreVal * rateAmt;
                                    }

                                    if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") || this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                                    {
                                        if (feeAmount > 0.00)
                                        {
                                            assessmentSum = feeAmount;
                                        }
                                    }
                                }
                                else // if (validAcreCount > 1)
                                {
                                    // i)   More than one values in "acres"
                                    //  Get Maximum Acre value
                                    string maxValue = string.Empty;
                                    DataView deprView = new DataView(this.assessmentData.ListMiscAssessmentTable);

                                    // set the output column array of the destination table 
                                    string[] strColumns = { "Acres" };

                                    // true = yes, to get distinct values. 

                                    DataTable distinctTable = new DataTable();
                                    distinctTable.Columns.AddRange(new DataColumn[] { new DataColumn("Acres")});
                                    distinctTable = deprView.ToTable(false, strColumns);
                                    DataColumn decimalAcres = new DataColumn("DecimalAcre");
                                    decimalAcres.DataType = typeof(decimal); ;
                                    decimalAcres.Expression = " IIF(Acres IS NULL OR Acres = '', 0, Acres)";
                                    //try
                                    //{
                                        distinctTable.Columns.Add(decimalAcres);
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //}
                      
                                    maxValue = distinctTable.Compute("MAX(DecimalAcre)", "DecimalAcre > '0.00'").ToString();

                                    ////maxValue = this.assessmentData.ListMiscAssessmentTable.Compute("MAX(Acres)", "Acres > '0.00' and Acres <> ' '").ToString();
                                    double maxAcre = 0.00;
                                    int maxAcreCount = 0;
                                    if (!string.IsNullOrEmpty(maxValue))
                                    {
                                        maxAcre = Convert.ToDouble(maxValue);
                                    }

                                    // Get Record count of maximum Acre 
                                    if (maxAcre > 0.00)
                                    {
                                        for (int count = 0; count < this.MiscAssessmentGridView.OriginalRowCount; count++)
                                        {
                                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[count].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString()) && maxAcre.Equals(Convert.ToDouble(this.MiscAssessmentGridView.Rows[count].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString())))
                                            {
                                                maxAcreCount = maxAcreCount + 1;
                                            }
                                        }
                                    }

                                    if (maxAcreCount > 0)
                                    {
                                        if (maxAcreCount > 1)
                                        {
                                            if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Site Land"))
                                            {
                                                assessmentSum = feeAmount + (rateAmt * acreVal);
                                            }
                                            else
                                            {
                                                assessmentSum = rateAmt * acreVal;
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString()) && this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString() != "")
                                            {
                                                if (acreVal.Equals(maxAcre))
                                                {
                                                    assessmentSum = feeAmount + (rateAmt * acreVal);
                                                }
                                                else
                                                {
                                                    assessmentSum = rateAmt * acreVal;
                                                }
                                            }
                                            else
                                            {
                                                assessmentSum = 0.00;
                                            }
                                        }
                                    }

                                    if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") || this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                                    {
                                        if (feeAmount > 0.00)
                                        {
                                            assessmentSum = feeAmount + (rateAmt * acreVal);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") || this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                                {
                                    // For Base / Base Land
                                    if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.PestControl).ToString()))
                                    {
                                        assessmentSum = feeAmount + (rateAmt * acreVal);
                                    }
                                    else
                                    {
                                        assessmentSum = feeAmount;
                                    }
                                }
                                else
                                {
                                    if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.Weed).ToString()))
                                    {
                                        // AssessmentType 2 (Weed) - Code added for CO #1214
                                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.MinAcresColumn.ColumnName].Value.ToString().Trim()))
                                        {
                                            Int32.TryParse(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.MinAcresColumn.ColumnName].Value.ToString().Trim(), out minAcre);
                                        }

                                        if (acreVal > minAcre) // If Acre value greater than minimum acre
                                        {
                                            // Assessment = Acre * Rate
                                            assessmentSum = rateAmt * acreVal;
                                        }
                                        else
                                        {
                                            assessmentSum = 0;
                                        }
                                    }
                                    else if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.PestControl).ToString()))
                                    {
                                        // AssessmentType 3 (Pest)
                                        assessmentSum = feeAmount + (rateAmt * acreVal);
                                    }
                                    else
                                    {
                                        assessmentSum = rateAmt * acreVal;
                                    }
                                }
                            }
                            //// return assessmentSum;
                            #endregion Assessment Calculation

                            this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value = assessmentSum;
                        }
                    }
                }
                else
                {
                    // Assessment calculation for TypeID 6
                    assessmentSum = 0;
                    if (this.districtTypeId.Equals((byte)AssessmentTypes.DNR))
                    {
                        Double countyFees = 0;
                        Double lcfFees = 0;
                        Double fpaFees = 0;
                        Double maxAcres = 0;
                        Double lcfRates = 0;
                        Double fpaRates = 0;

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName].Value.ToString()))
                        {
                            countyFees = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName].Value.ToString()))
                        {
                            fpaFees = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName].Value.ToString()))
                        {
                            lcfFees = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.OtherAcresColumn.ColumnName].Value.ToString()))
                        {
                            maxAcres = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.OtherAcresColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.LCFRateColumn.ColumnName].Value.ToString()))
                        {
                            lcfRates = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.LCFRateColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FPARateColumn.ColumnName].Value.ToString()))
                        {
                            fpaRates = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FPARateColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].Value.ToString().Trim()))
                        {
                            Double firePatrolAcre = 0;
                            Double.TryParse(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].Value.ToString(), out firePatrolAcre);
                            this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].Value = firePatrolAcre;

                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.OtherAcresColumn.ColumnName].Value.ToString()))
                            {
                                if (firePatrolAcre <= Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.OtherAcresColumn.ColumnName].Value.ToString()))
                                {
                                    assessmentSum = countyFees + fpaFees + lcfFees;
                                }
                                else
                                {
                                    assessmentSum = countyFees + fpaFees + lcfFees + ((firePatrolAcre - maxAcres) * fpaRates) + ((firePatrolAcre - maxAcres) * lcfRates);
                                }
                            }
                            else
                            {
                                assessmentSum = countyFees + fpaFees + lcfFees;
                            }
                        }
                        else
                        {
                            assessmentSum = countyFees + fpaFees + lcfFees;
                            this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].Value = DBNull.Value;
                        }

                        this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value = assessmentSum;
                    }

                    // Assessment calculation for TypeID 7
                    if (this.districtTypeId.Equals((byte)AssessmentTypes.FireDistrict))
                    {
                        Double value = 0;
                        Double residentRate = 0;
                        Double commercalRate = 0;

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.TaxAmountColumn.ColumnName].Value.ToString()))
                        {
                            value = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.TaxAmountColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.ResidentialRateColumn.ColumnName].Value.ToString()))
                        {
                            residentRate = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.ResidentialRateColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.CommercialRateColumn.ColumnName].Value.ToString()))
                        {
                            commercalRate = Convert.ToDouble(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.CommercialRateColumn.ColumnName].Value.ToString());
                        }

                        if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName].Value.ToString().Trim()))
                        {
                            Double benefitCharge = 0;
                            Double.TryParse(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName].Value.ToString(), out benefitCharge);
                            this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName].Value = benefitCharge;
                            this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value = benefitCharge;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Value.ToString().Trim()))
                            {
                                int stateCode;
                                int.TryParse(this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Value.ToString().Trim().Substring(0, 2), out stateCode);
                                
                                // Calculation has been modified for CO:#1459
                                if ((stateCode >= 11 && stateCode <= 19) || (stateCode >= 41 && stateCode <= 49)
                                    || (stateCode >= 81 && stateCode <= 88) || (stateCode >= 91 && stateCode <= 95)
                                    || stateCode.Equals(99))
                                {
                                    // Assessment = O2Value * ResidentialRate / POWER (10 , dbo.f9020_udf_ConfigurationValue('TR_LevyDisplayOffset'))
                                    this.MiscAssessmentGridView.Rows[0].Cells[this.Assessment.Name].Value = Math.Round((value * residentRate) / Math.Pow(10, this.leavyOffSet), MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    // Assessment = O2Value * CommercialRate / POWER (10 , dbo.f9020_udf_ConfigurationValue('TR_LevyDisplayOffset'))
                                    this.MiscAssessmentGridView.Rows[0].Cells[this.Assessment.Name].Value = Math.Round((value * commercalRate) / Math.Pow(10, this.leavyOffSet), 2, MidpointRounding.AwayFromZero);
                                }
                            }
                            else
                            {
                                this.MiscAssessmentGridView.Rows[0].Cells[this.Assessment.Name].Value = DBNull.Value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the misc assessment details.
        /// </summary>
        private void GetMiscAssessmentDetails()
        {
            if (this.AssessmentTypeComboBox.SelectedIndex > 0)
            {
                this.miscDataSet.Tables.Clear();
                this.messageDataSet.Tables.Clear();
                int madTypeId = Convert.ToInt32(this.AssessmentTypeComboBox.SelectedValue.ToString());
                this.assessmentData = this.form27010Control.WorkItem.F27010GetDefaultMiscData(this.keyId, madTypeId);
                this.miscDataSet.Tables.Add(this.assessmentData.ListMiscAssessmentTable.Copy());

                this.tempMiscData.Tables.Clear();
                this.tempMiscData.Tables.Add(this.assessmentData.ListMiscAssessmentTable.Copy());

                this.messageDataSet.Tables.Add(this.assessmentData.GetMessageTable.Copy());

                this.MiscAssessmentGridView.DataSource = this.assessmentData.ListMiscAssessmentTable.DefaultView;

                this.SetReadOnly();

                this.MiscAssessmentGridView.DataSource = this.assessmentData.ListMiscAssessmentTable.DefaultView;

                string distKey = "0";
                if (this.assessmentData.DistrictTable.Rows.Count > 0)
                {
                    distKey = this.assessmentData.DistrictTable.Rows[0].ItemArray[0].ToString();
                }

                ////string miscDetails = Utility.GetXmlString(this.assessmentData.ListMiscAssessmentTable);
                this.LoadDistrictCombo();
                bool found = false;
                for (int count = 0; count < this.districtDataSet.Tables[0].Rows.Count; count++)
                {
                    if (this.districtDataSet.Tables[0].Rows[count].ItemArray[0].ToString().Equals(distKey))
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    this.DistrictComboBox.SelectedValue = distKey;
                }
                else
                {
                    this.DistrictComboBox.SelectedValue = 0;
                }

                this.SetGridVisibility();
                this.FormatGrid();
                this.BindControls();
                ////Set color
                this.SetDistrictComboColor();

                this.CalculateAssessment();
                ////Set Label Text
                this.SetDistrictLabelText();
                this.SetGridHeight();
                this.SumOfAcres();
                this.SumOfAssessment();
                this.SetLinkText();
            }
        }

        #endregion AssessmentGrid

        #region CustomizeGrid

        /// <summary>
        /// Customizes the grid view.
        /// </summary>
        private void CustomizeGridView()
        {
            this.MiscAssessmentGridView.AutoGenerateColumns = false;
            this.MiscAssessmentGridView.PrimaryKeyColumnName = this.assessmentData.ListMiscAssessmentTable.RankColumn.ColumnName;
            this.MiscAssessmentGridView.AllowSorting = false;

            DataGridViewColumnCollection columns = this.MiscAssessmentGridView.Columns;
            if (this.MiscAssessmentGridView.Columns.Count > 0)
            {
                columns[this.assessmentData.ListMiscAssessmentTable.RankColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.RankColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.ParcelIDColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.ParcelIDColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.MADistrictIDColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.MADistrictIDColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.SiteAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.SiteAcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.AssessedValueColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.AssessedValueColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.DryAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.DryAcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.IrrigatedAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.IrrigatedAcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.ResidentialRateColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.ResidentialRateColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.TimberAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.TimberAcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.CommercialRateColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.CommercialRateColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.FPARateColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.FPARateColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.OtherAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.OtherAcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.BaseAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.BaseAcresColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.LCFRateColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.LCFRateColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.TaxAmountColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.TaxAmountColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName;
                columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName;

                columns[this.assessmentData.ListMiscAssessmentTable.MinAcresColumn.ColumnName].DataPropertyName = this.assessmentData.ListMiscAssessmentTable.MinAcresColumn.ColumnName;

                columns[this.assessmentData.ListMiscAssessmentTable.RankColumn.ColumnName].DisplayIndex = 0;
                columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].DisplayIndex = 1;
                columns[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].DisplayIndex = 2;
                columns[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].DisplayIndex = 3;
                columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].DisplayIndex = 4;
                columns[this.assessmentData.ListMiscAssessmentTable.ParcelIDColumn.ColumnName].DisplayIndex = 5;
                columns[this.assessmentData.ListMiscAssessmentTable.MADistrictIDColumn.ColumnName].DisplayIndex = 6;
                columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].DisplayIndex = 7;
                columns[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].DisplayIndex = 8;
                columns[this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName].DisplayIndex = 9;
                columns[this.assessmentData.ListMiscAssessmentTable.SiteAcresColumn.ColumnName].DisplayIndex = 10;
                columns[this.assessmentData.ListMiscAssessmentTable.AssessedValueColumn.ColumnName].DisplayIndex = 11;
                columns[this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName].DisplayIndex = 12;
                columns[this.assessmentData.ListMiscAssessmentTable.DryAcresColumn.ColumnName].DisplayIndex = 13;
                columns[this.assessmentData.ListMiscAssessmentTable.IrrigatedAcresColumn.ColumnName].DisplayIndex = 14;
                columns[this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName].DisplayIndex = 15;
                columns[this.assessmentData.ListMiscAssessmentTable.ResidentialRateColumn.ColumnName].DisplayIndex = 16;
                columns[this.assessmentData.ListMiscAssessmentTable.TimberAcresColumn.ColumnName].DisplayIndex = 17;
                columns[this.assessmentData.ListMiscAssessmentTable.CommercialRateColumn.ColumnName].DisplayIndex = 18;
                columns[this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName].DisplayIndex = 19;
                columns[this.assessmentData.ListMiscAssessmentTable.FPARateColumn.ColumnName].DisplayIndex = 20;
                columns[this.assessmentData.ListMiscAssessmentTable.OtherAcresColumn.ColumnName].DisplayIndex = 21;
                columns[this.assessmentData.ListMiscAssessmentTable.BaseAcresColumn.ColumnName].DisplayIndex = 22;
                columns[this.assessmentData.ListMiscAssessmentTable.LCFRateColumn.ColumnName].DisplayIndex = 23;
                columns[this.assessmentData.ListMiscAssessmentTable.TaxAmountColumn.ColumnName].DisplayIndex = 24;
                columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].DisplayIndex = 25;
                columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].DisplayIndex = 26;

                columns[this.assessmentData.ListMiscAssessmentTable.MinAcresColumn.ColumnName].DisplayIndex = 27;
            }
        }

        #endregion CustomizeGrid

        #region Validation
        /// <summary>
        /// Checking Errors
        /// </summary>
        /// <param name="formNo">Form Number.</param>
        /// <returns>slice Validation Fields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            if (this.AssessmentTypeComboBox.SelectedIndex > 0)
            {
                this.GetAssessmentXML();
            }
            else
            {
                this.assessmentValue = null;
            }

            if (this.DistrictComboBox.SelectedIndex >= 0)
            {
                this.GetDistrictXML();
            }
            else
            {
                this.districtValue = null;
            }

            if (string.IsNullOrEmpty(this.assessmentValue) || string.IsNullOrEmpty(this.districtValue))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }
            else
            {
                sliceValidationFields.ErrorMessage = string.Empty;
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = false;
            }

            return sliceValidationFields;
        }
        #endregion Validation

        #region Save
        /// <summary>
        /// Saves the misc assessment.
        /// </summary>
        private void SaveMiscAssessment()
        {
            this.GetAssessmentXML();
            this.GetDistrictXML();
            string miscDetails = null;

            this.assessmentData.ListMiscAssessmentTable.AcceptChanges();

            if (this.ExemptComboBox.SelectedItem.ToString() == "Yes")
            {
                this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsExemptColumn] = true;
            }
            else
            {
                this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsExemptColumn] = false;
            }

            if (this.OverrideCheckBox.Checked)
            {
                this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsOverrideColumn] = true;
            }
            else
            {
                this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsOverrideColumn] = false;
            }

            this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.OverrideAmountColumn] = this.OverrideValueTextBox.DecimalTextBoxValue;
            miscDetails = TerraScanCommon.GetXmlString(this.assessmentData.ListMiscAssessmentTable);
            int saved;

            saved = this.form27010Control.WorkItem.F27010_SaveMiscAssessment(this.keyId, this.assessmentValue, this.districtValue, miscDetails, TerraScanCommon.UserId);

            this.afterSave = true;
        }
        #endregion Save

        #region GetAssessmentXML
        /// <summary>
        /// Gets the assessment XML.
        /// </summary>
        private void GetAssessmentXML()
        {
            DataTable assessmentDetails = new DataTable("Table");
            assessmentDetails.Columns.AddRange(new DataColumn[] { new DataColumn("KeyID"), new DataColumn("KeyName"), new DataColumn("RollYear") });
            if (this.AssessmentTypeComboBox.SelectedValue.ToString() != "0")
            {
                DataRow row = assessmentDetails.NewRow();
                row["KeyID"] = this.AssessmentTypeComboBox.SelectedValue.ToString();
                row["KeyName"] = this.AssessmentTypeComboBox.Text;
                row["RollYear"] = this.rollYear;
                assessmentDetails.Rows.Add(row);
            }

            this.assessmentValue = TerraScanCommon.GetXmlString(assessmentDetails);

            if (this.assessmentValue.Equals("<Root />"))
            {
                this.assessmentValue = null;
            }
        }
        #endregion GetAssessmentXML

        #region GetDistrictXML
        /// <summary>
        /// Gets the district XML.
        /// </summary>
        private void GetDistrictXML()
        {
            DataTable districtDetails = new DataTable("Table");
            districtDetails.Columns.AddRange(new DataColumn[] { new DataColumn("KeyID"), new DataColumn("KeyName"), new DataColumn("DistrictID") });
            string districtId = string.Empty;
            for (int i = 0; i < this.districtDataSet.Tables[0].Rows.Count; i++)
            {
                if (this.districtDataSet.Tables[0].Rows[i].ItemArray[0].ToString().Equals(this.DistrictComboBox.SelectedValue.ToString()))
                {
                    districtId = this.districtDataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                    break;
                }
            }

            if (this.DistrictComboBox.SelectedValue.ToString() != "-1")
            {
                DataRow row = districtDetails.NewRow();
                row["KeyID"] = this.DistrictComboBox.SelectedValue.ToString();
                row["KeyName"] = this.DistrictComboBox.Text;
                row["DistrictID"] = districtId;
                districtDetails.Rows.Add(row);
            }

            this.districtValue = TerraScanCommon.GetXmlString(districtDetails);

            if (this.districtValue.Equals("<Root />"))
            {
                this.districtValue = null;
            }
        }
        #endregion GetDistrictXML

        #region SetFormHeight
        /// <summary>
        /// Sets the height of the grid.
        /// </summary>
        private void SetGridHeight()
        {
            if (this.MiscAssessmentGridView.OriginalRowCount > 1)
            {
                this.MiscAssessmentPanel.Height = 197;
                this.MiscAssessmentGridView.Height = 178;
                this.MiscAssessmentPictureBox.Height = 198;
                this.MiscAssessmentGridVscrollBar.Height = 197;
                this.Footerpanel.Location = new System.Drawing.Point(-1, 176);
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = this.MiscAssessmentPictureBox.Height + this.HeaderPictureBox.Height;
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.MiscAssessmentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscAssessmentPictureBox.Height, this.MiscAssessmentPictureBox.Width, this.AssessmentTypeComboBox.Text, 82, 101, 140);
                this.TotalAcresLabel.Visible = true;
                this.MosquitoTotal.Visible = false;
                this.totalLabel.Visible = true;
                this.totalLabel.Text = "Totals";
                this.MiscAssessmentPanel.Top = this.HeaderPanel.Bottom;
            }
            else if (this.MiscAssessmentGridView.OriginalRowCount.Equals(1))
            {
                this.MiscAssessmentGridView.NumRowsVisible = 1;
                this.TotalAcresLabel.Visible = false;
                this.MosquitoTotal.Visible = true;
                this.totalLabel.Visible = false;
                this.MosquitoTotal.Text = "Total";
                this.MiscAssessmentPictureBox.Height = 68;
                this.MiscAssessmentPanel.Height = 67;
                this.MiscAssessmentGridView.Height = 46;
                this.MiscAssessmentGridVscrollBar.Height = 67;
                this.Footerpanel.Location = new System.Drawing.Point(-1, 44);
                this.MiscAssessmentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscAssessmentPictureBox.Height, this.MiscAssessmentPictureBox.Width, this.AssessmentTypeComboBox.Text, 82, 101, 140);
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = 110;
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
            else
            {
                this.MiscAssessmentPanel.Height = 0;
                this.MiscAssessmentGridView.Height = 0;
                this.MiscAssessmentGridVscrollBar.Height = 0;
                this.MiscAssessmentPictureBox.Height = 0;
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = 45;
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
        }
        #endregion SetFormHeight

        #region TotalAcre
        /// <summary>
        /// Sums the of acres.
        /// </summary>
        private void SumOfAcres()
        {
            Double totalSum = 0;
            for (int i = 0; i < this.MiscAssessmentGridView.OriginalRowCount; i++)
            {
                if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString()) && (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString() != "0.00"))
                {
                    totalSum = totalSum + Convert.ToDouble(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Value.ToString());
                }
            }

            this.TotalAcresLabel.Text = totalSum.ToString("#,##0.00");
        }
        #endregion TotalAcre

        #region TotalAssessment
        /// <summary>
        /// Sums the of assessment.
        /// </summary>
        private void SumOfAssessment()
        {
            Double totalAssessment = 0;
            for (int i = 0; i < this.MiscAssessmentGridView.OriginalRowCount; i++)
            {
                if (!string.IsNullOrEmpty(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value.ToString()))
                {
                    totalAssessment = totalAssessment + Convert.ToDouble(this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Value.ToString());
                }
            }

            if (this.ExemptComboBox.SelectedItem.ToString().Equals("Yes"))
            {
                this.TotalValueLabel.Text = "$ 0.00";
            }
            else
            {
                this.TotalValueLabel.Text = "$ " + totalAssessment.ToString("#,##0.00");
            }
        }
        #endregion TotalAssessment

        #region GridFormat
        /// <summary>
        /// Formats the grid.
        /// </summary>
        private void FormatGrid()
        {
            ////To set Base & Minimum column's acre field as not Read only
            if (this.slicePermissionField.editPermission)
            {
                if (this.MiscAssessmentGridView.OriginalRowCount > 1)
                {
                    for (int i = 0; i < this.MiscAssessmentGridView.OriginalRowCount; i++)
                    {
                        if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Minimum"))
                        {
                            this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = true;
                        }
                        else if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") || this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                        {
                            ////Base column
                            if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.PestControl).ToString()))
                            {
                                this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = false;
                            }
                            else
                            {
                                this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = true;
                            }
                        }
                        else
                        {
                            this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = false;
                        }
                    }

                    this.DisableGridColumns();
                }
                else if (this.MiscAssessmentGridView.OriginalRowCount.Equals(1))
                {
                    switch (this.districtTypeId)
                    {
                        case 4:
                            this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = true;
                            break;
                    }

                    this.DisableGridColumns();
                }
            }
        }

        /// <summary>
        /// Disables the grid columns.
        /// </summary>
        private void DisableGridColumns()
        {
            switch (this.districtTypeId)
            {
                case 1:
                    ////For District Type 1
                    this.EnableForFullForm(true);
                    this.EnableForDNRDistrict(false);
                    this.EnableForFireDistrict(false);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = false;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Width = 220;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 130;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Width = 100;

                    break;

                case 2:
                    ////For District Type 2
                    this.EnableForFullForm(true);
                    this.EnableForDNRDistrict(false);
                    this.EnableForFireDistrict(false);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = false;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Width = 220;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 130;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Width = 100;
                    break;

                case 3:
                    ////For District Type 3
                    this.EnableForFullForm(true);
                    this.EnableForDNRDistrict(false);
                    this.EnableForFireDistrict(false);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = false;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Width = 220;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 130;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Width = 100;
                    break;

                case 4:
                    ////For District Type 4
                    this.EnableForFullForm(false);
                    this.EnableForDNRDistrict(false);
                    this.EnableForFireDistrict(false);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Visible = true;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Visible = true;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = true;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Width = 339;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Width = 128;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 137;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Width = 130;
                    break;

                case 6:
                    ////For District Type 6
                    this.EnableForFullForm(false);
                    this.EnableForDNRDistrict(true);
                    this.EnableForFireDistrict(false);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = false;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].Width = 114;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName].Width = 100;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName].Width = 100;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName].Width = 100;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FPARateColumn.ColumnName].Width = 100;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.LCFRateColumn.ColumnName].Width = 100;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 120;
                    break;
                case 7:
                    ////For District Type 7
                    this.EnableForFullForm(false);
                    this.EnableForDNRDistrict(false);
                    this.EnableForFireDistrict(true);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Visible = true;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = false;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Width = 240;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName].Width = 127;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ResidentialRateColumn.ColumnName].Width = 120;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.CommercialRateColumn.ColumnName].Width = 120;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 127;
                    break;
                case 8:
                    ////For District Type 8
                    this.EnableForFullForm(false);
                    this.EnableForDNRDistrict(false);
                    this.EnableForFireDistrict(false);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = true;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Width = 367;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 367;
                    break;
                case 9:
                    ////For District Type 9
                    this.EnableForFullForm(false);
                    this.EnableForDNRDistrict(false);
                    this.EnableForFireDistrict(false);
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Visible = true;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Visible = true;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.OpenSpaceColumn.ColumnName].Visible = false;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BaseFeeColumn.ColumnName].Visible = false;

                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Width = 320;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Width = 207;
                    this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AssessmentColumn.ColumnName].Width = 207;
                    break;
            }
        }

        /// <summary>
        /// Enables for DNR district.
        /// </summary>
        /// <param name="visible">Flag for visible</param>
        private void EnableForDNRDistrict(bool visible)
        {
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FirePatrolAcresColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.CountyFeeColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FPAFeeColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.LCFFeeColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FPARateColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.LCFRateColumn.ColumnName].Visible = visible;
        }

        /// <summary>
        /// Enables for fire district.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private void EnableForFireDistrict(bool visible)
        {
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.BenefitChargeColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ResidentialRateColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.CommercialRateColumn.ColumnName].Visible = visible;
        }

        /// <summary>
        /// Enables for full form.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private void EnableForFullForm(bool visible)
        {
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.FeesColumn.ColumnName].Visible = visible;
            this.MiscAssessmentGridView.Columns[this.assessmentData.ListMiscAssessmentTable.RateColumn.ColumnName].Visible = visible;
        }

        /// <summary>
        /// Removes the grid sorting.
        /// </summary>
        private void RemoveGridSorting()
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                this.MiscAssessmentGridView.AllowSorting = false;
                this.MiscAssessmentGridView.ClearSorting();
            }
        }

        /// <summary>
        /// Sets the grid visibility.
        /// </summary>
        private void SetGridVisibility()
        {
            if (this.AssessmentTypeComboBox.SelectedIndex > 0 && this.DistrictComboBox.SelectedIndex >= 0)
            {
                this.MiscAssessmentPanel.Visible = true;
                this.MiscAssessmentPictureBox.Visible = true;
            }
            else
            {
                this.MiscAssessmentPanel.Visible = false;
                this.MiscAssessmentPictureBox.Visible = false;
            }
        }

        /// <summary>
        /// Sets the read only.
        /// </summary>
        private void SetReadOnly()
        {
            if (this.slicePermissionField.editPermission)
            {
                if (this.MiscAssessmentGridView.OriginalRowCount > 1)
                {
                    ////For Full slice
                    for (int i = 0; i < this.MiscAssessmentGridView.OriginalRowCount; i++)
                    {
                        if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Minimum"))
                        {
                            ////Minimum Column
                            this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = true;
                        }
                        else if (this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base") || this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.ChargeColumn.ColumnName].Value.ToString().Equals("Base Land"))
                        {
                            ////Base column
                            if (this.AssessmentTypeComboBox.SelectedValue.ToString().Equals(((byte)AssessmentTypes.PestControl).ToString()))
                            {
                                this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = false;
                            }
                            else
                            {
                                this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = true;
                            }
                        }
                        else
                        {
                            ////Other Lands
                            this.MiscAssessmentGridView.Rows[i].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = false;
                        }
                    }
                }
                else if (this.MiscAssessmentGridView.OriginalRowCount.Equals(1)) 
                {
                    ////For thin Slice
                    switch (this.districtTypeId)
                    {
                        case 4:
                            this.MiscAssessmentGridView.Rows[0].Cells[this.assessmentData.ListMiscAssessmentTable.AcresColumn.ColumnName].ReadOnly = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Resets the acre value.
        /// </summary>
        /// <param name="chargeValue">The charge value.</param>
        /// <returns>Acre value.</returns>
        private string ResetAcreValue(string chargeValue)
        {
            string defaultAcre = string.Empty;
            for (int i = 0; i <= this.tempMiscData.Tables[0].Rows.Count; i++)
            {
                if (this.tempMiscData.Tables[0].Rows[i].ItemArray[1].ToString().Equals(chargeValue))
                {
                    defaultAcre = this.tempMiscData.Tables[0].Rows[i].ItemArray[2].ToString();
                    break;
                }
            }

            return defaultAcre;
        }
        #endregion GridFormat

        #region CellFormat
        /// <summary>
        /// Formats the cell.
        /// </summary>
        /// <param name="format">The format.</param>
        private void FormatCell(string format)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
            dataGridViewCellStyle.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle.Format = format;
            dataGridViewCellStyle.NullValue = null;
            dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.Fees.DefaultCellStyle = dataGridViewCellStyle;
        }
        #endregion CellFormat

        #region DistrictLabel
        /// <summary>
        /// Sets the link text.
        /// </summary>
        private void SetLinkText()
        {
            if (Convert.ToInt32(this.DistrictComboBox.SelectedValue) != 0)
            {
                this.ComboLinkLabel.Visible = true;
                this.ComboLinkLabel.Text = this.DistrictComboBox.Text;
            }
            else
            {
                this.ComboLinkLabel.Visible = false;
                this.ComboLinkLabel.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the district label text.
        /// </summary>
        private void SetDistrictLabelText()
        {
            if (this.AssessmentTypeComboBox.SelectedIndex > 0)
            {
                this.DistrictLabel.Text = this.AssessmentTypeComboBox.Text + " District:";
                this.ComboLinkLabel.BringToFront();
                ////this.DistrictComboBox.Enabled = true;
                this.ShowControls(true);
            }
            else
            {
                this.DistrictLabel.Text = "District:";
                this.DistrictComboBox.SelectedIndex = -1;
                this.DistrictComboBox.BringToFront();
                ////this.DistrictComboBox.Enabled = false;
                this.ShowControls(false);
            }
        }
        #endregion DistrictLabel

        #region Exempt ComboBox

        /// <summary>
        /// Fills the combo boxes.
        /// </summary>
        private void FillComboBoxes()
        {
            this.ExemptComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.ExemptComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.ExemptComboBox.SelectedIndex = 0;
        }

        #endregion Exempt ComboBox

        #region Enable Controls

        /// <summary>
        /// Shows the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void ShowControls(bool enable)
        {
            this.DistrictComboBox.Enabled = enable;
            this.ExemptComboBox.Enabled = enable;
            this.OverrideCheckBox.Enabled = enable;
            this.OverrideValueTextBox.Enabled = enable;

            if (!enable)
            {
                this.OverrideValueTextBox.Text = string.Empty;
                this.OverrideCheckBox.Checked = false;
                if (this.ExemptComboBox.Items.Count > 0)
                {
                    this.ExemptComboBox.SelectedIndex = -1;
                }
            }
        }

        #endregion Enable Controls

        #region BindControls

        /// <summary>
        /// Binds the controls.
        /// </summary>
        private void BindControls()
        {
            if (!string.IsNullOrEmpty(this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.OverrideAmountColumn].ToString()))
            {
                this.OverrideValueTextBox.Text = this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.OverrideAmountColumn].ToString();
            }
            else
            {
                this.OverrideValueTextBox.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsExemptColumn].ToString()))
            {
                if (this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsExemptColumn].ToString().Equals("True"))
                {
                    this.ExemptComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.ExemptComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                this.ExemptComboBox.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsOverrideColumn].ToString()))
            {
                if (this.assessmentData.ListMiscAssessmentTable.Rows[0][this.assessmentData.ListMiscAssessmentTable.IsOverrideColumn].ToString().Equals("True"))
                {
                    this.OverrideCheckBox.Checked = true;
                }
                else
                {
                    this.OverrideCheckBox.Checked = false;
                }
            }
            else
            {
                this.OverrideCheckBox.Checked = false;
            }
        }

        #endregion BindControls

        #endregion Private Methods
    }
}
