//--------------------------------------------------------------------------------------------
// <copyright file="F15050.cs" company="Congruent">
//       Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//     This file contains Property for the WorkItem .
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author              Description
// ----------        ----------             ---------------------------------------------------------
// 09 Mar 07        Sriparameswari       FeeManagement
// 19 Jun 07         Vinoth              SnapShot Integration
// 29 Jan 09       Sadha Shivudu         #4777 TSCO - Do not clear form fields on "Select Template"
// 04 Sep 09       Sadha Shivudu         #3445 TSCO - Add Fee Type and Remove Template Button
// 03 FEB 11        Manoj Kumar         Removed the SnapShot usage in this form.
// *********************************************************************************/
namespace D11050
{
    using System;
    using System.Collections;
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

    /// <summary>
    /// F15050 class
    /// </summary>
    public partial class F15050 : BaseSmartPart
    {
        #region variables

        /// <summary>
        /// instance variable to hold the accountId;
        /// </summary>
        private int accountId;

        /// <summary>
        /// instance variable to hold the feeId value.
        /// </summary>
        private int feeId;

        /// <summary>
        /// instance variable to hold the 15050Control value.
        /// </summary>
        private F15050Controller form15050Control;

        /// <summary>
        /// instance variable to hold the rollYear value.
        /// </summary>
        private int rollYear;

        /// <summary>
        /// instance variable to hold the feeManagement dataset
        /// </summary>
        private F15050FeeManagementData feeManagementData = new F15050FeeManagementData();

        /// <summary>
        /// instance variable to hold the rollYear configuration value dataset.
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        ///// <summary>
        ///// instance variable to hold the systemSnapShotId value.
        ///// </summary>
        //private int systemSnapShotId;

        /// <summary>
        /// instance variable to hold the includeRowData
        /// </summary>
        private CommonData includeRowData = new CommonData();

        /// <summary>
        /// instance variable to hold the feeTypeId value
        /// </summary>
        private byte feeTypeId;

        /// <summary>
        /// instance variable to hold the key id vlaue
        /// </summary>
        private int keyId;

        #endregion

        #region Form Slice Variables

        /// <summary>
        /// instance variable to hold the masterFormNo value.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// instance variable to hold the permission fields for slice.
        /// </summary>
        private PermissionFields slicePermissionField;

        #endregion Form Slice Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F15050"/> class.
        /// </summary>
        public F15050()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F15050"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15050(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.feeId = keyID;
            this.keyId = keyID;
            //// this.formMasterPermissionEdit = permissionEdit;            
            this.FeeManagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.FeeManagementPictureBox.Height, this.FeeManagementPictureBox.Width, tabText, red, green, blue);   ////todo remove hard code value                     
        }

        #endregion

        #region Event Publication
        /*
        /// <summary>
        /// Occurs when [D9030_ F9033_ set system snapshot event].
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_SetSystemSnapshotEvent, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails>> D9030_F9033_SetSystemSnapshotEvent;
  */
        ///<summary>
        ///Occurs when [D9030_F9033_ApplyFee Event]
        ///</summary>
        [EventPublication(EventTopicNames.D9030_f9033_ApplyFeeEvent, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> D9030_f9033_ApplyFeeEvent;   
        /// <summary>
        /// Occurs when [form slice_ section indicator click].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form15050 control.
        /// </summary>
        /// <value>The form15050 control.</value>
        [CreateNew]
        public F15050Controller Form15050Control
        {
            get { return this.form15050Control as F15050Controller; }
            set { this.form15050Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SlicePermissionReload&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                ////* used to get whether the keyid is valide or not*//
                this.feeManagementData.GetFeeDetails.Clear();
                this.feeManagementData.GetISValidKey.Clear();
                this.feeManagementData.Merge(this.form15050Control.WorkItem.F15050_getDatas(this.feeId));

                if (this.feeManagementData.GetISValidKey.Rows.Count > 0)
                {
                    if (!this.feeManagementData.GetISValidKey.Rows[0][this.feeManagementData.GetISValidKey.CheckRecordColumn].Equals(0))
                    {
                        ////when 1 the key id is valid
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            ////when 0 the key id is Invalid
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
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

        ///<summary>
        /// Called when [D9030_ F9033_get RecordsetXml].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;TerraScan.Common.SetSystemSnapShotIdnCount&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_GetRecordsetXML, ThreadOption.UserInterface)]
        public void OnD9030_F9033_GetRecordsetXML(object sender, TerraScan.Infrastructure.Interface.EventArgs<string[] > eventArgs)
        {
           ////OBTAIN THE KEYIDS FROM APPLY FEES.
                string recordset=eventArgs.Data[0];
               string  recordCount = eventArgs.Data[1]; 
            ////USED TO SENT THE VALUE FOR APLY FEE TO 
            //THE REGARDING VALUE.
            int recordscount;
            int.TryParse(recordCount, out recordscount);
               if(recordscount>0) 
               {
                   decimal applyFeeAmount = recordscount * this.AmmountTextBox.DecimalTextBoxValue;

                   if (MessageBox.Show(SharedFunctions.GetResourceString("ApplySnapShot") + recordscount + " statements in the current recordset.\nThis will create a total of " + applyFeeAmount.ToString("$#,##0.00") + " in fees.\n\nDo you wish to continue?", ConfigurationWrapper.ApplicationName + " - Verify Fees", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                   {
                       int feesCreated = this.form15050Control.WorkItem.F9033_ApplyFees(recordset, this.AmmountTextBox.DecimalTextBoxValue, this.DescriptionTextBox.Text.Trim(), this.accountId, TerraScanCommon.UserId);
                       if (feesCreated == 0)
                       {
                           MessageBox.Show(SharedFunctions.GetResourceString("AppliedSnapShot") + recordCount + " statements", ConfigurationWrapper.ApplicationName + " - Fees Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       }
                   }
               }
            
        }





       /*
        /// <summary>
        /// Called when [D9030_ F9033_ system snapshot complete event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;TerraScan.Common.SetSystemSnapShotIdnCount&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_SystemSnapshotCompleteEvent, ThreadOption.UserInterface)]
        public void OnD9030_F9033_SystemSnapshotCompleteEvent(object sender, TerraScan.Infrastructure.Interface.EventArgs<SetSystemSnapShotIdnCount> eventArgs)
        {
            int systemSnapShotCount;

            this.systemSnapShotId = eventArgs.Data.SystemSnapShotId;

            if (this.systemSnapShotId > 0)
            {
                systemSnapShotCount = eventArgs.Data.SystemSnapShotCount;

                if (systemSnapShotCount > 0 && this.keyId > 0)
                {
                    decimal applyFeeAmount = systemSnapShotCount * this.AmmountTextBox.DecimalTextBoxValue;

                    if (MessageBox.Show(SharedFunctions.GetResourceString("ApplySnapShot") + systemSnapShotCount + " statements in the current recordset.\nThis will create a total of " + applyFeeAmount.ToString("$#,##0.00") + " in fees.\n\nDo you wish to continue?", ConfigurationWrapper.ApplicationName + " - Verify Fees", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                    {
                        if (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
                        {
                            int feesCreated = this.form15050Control.WorkItem.F9033_ApplyFees(this.systemSnapShotId, this.AmmountTextBox.DecimalTextBoxValue, this.DescriptionTextBox.Text.Trim(), this.accountId, TerraScanCommon.UserId);

                            if (feesCreated == 0)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("AppliedSnapShot") + systemSnapShotCount + " statements", ConfigurationWrapper.ApplicationName + " - Fees Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }
        */
         

        #endregion Event Subscription

        #region Protected Methods
/*
        /// <summary>
        /// Raises the <see cref="E:D9030_F9033_SetSystemSnapshotEvent"/> event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;TerraScan.Common.LoadSystemSnapShotDetails&gt;"/> instance containing the event data.</param>
        protected virtual void OnD9030_F9033_SetSystemSnapshotEvent(TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails> eventArgs)
        {
            if (this.D9030_F9033_SetSystemSnapshotEvent != null)
            {
                this.D9030_F9033_SetSystemSnapshotEvent(this, eventArgs);
            }
        }
        */
        #endregion Protected Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F15050 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F15050_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;

                // initialize the from template combo
                this.InitializeFromTemplateComboBox();

                // initialize the fee type combo
                this.InitializeFeeTypeComboBox();

                // initialize the include rows combo
                this.InitializeIncludeRowsComboBox();

                // get the configuration roll year
                this.GetConfigRollYear();

                // check the item count and select the first item by default and fill the fee details
                this.SelectDefaultFromTemplateItem();

                // enable save and apply fee buttons
                this.EnableSaveAndApplyFeeButton();

                this.FromTemplateComboBox.Focus();
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
        /// Handles the SelectionChangeCommitted event of the FromTemplateComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FromTemplateComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                // populate the template fields upon template selection
                this.PopulateTemplateFields();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the FeeTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FeeTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.FeeTypeComboBox.SelectedValue != null)
                {
                    byte.TryParse(this.FeeTypeComboBox.SelectedValue.ToString(), out this.feeTypeId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the AmmountTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void AmmountTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                decimal moneyMaxValue = 922337203685477.5807M;
                decimal sourceControlValue;
                if (decimal.TryParse(this.AmmountTextBox.DecimalTextBoxValue.ToString(), out sourceControlValue))
                {
                    if (sourceControlValue > moneyMaxValue)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.AmmountTextBox.Text = decimal.Zero.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AccountSelectionButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AccountSelectionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form form1345 = new Form();
                object[] optionalParameter = new object[] { this.rollYear };
                form1345 = this.form15050Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form15050Control.WorkItem);

                if (form1345 != null)
                {
                    if (form1345.ShowDialog().Equals(DialogResult.OK))
                    {
                        this.AccountTextBox.Text = TerraScanCommon.GetValue(form1345, "SelectedAccountName");
                        int.TryParse(TerraScanCommon.GetValue(form1345, "AccountId"), out this.accountId);
                        this.EnableSaveAndApplyFeeButton();
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
        /// Handles the Click event of the SaveTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveTemplate();
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
        /// Handles the Click event of the RemoveTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.feeId > 0)
                {
                    if (MessageBox.Show("You are about to permanently delete the current Fee Template.", "TerraScan T2 - Remove Template", MessageBoxButtons.OKCancel, MessageBoxIcon.Information).Equals(DialogResult.OK))
                    {
                        // remove the selected template
                        this.form15050Control.WorkItem.F15050_RemoveTemplate(this.feeId, TerraScanCommon.UserId);

                        // reload the FromTemplate combo
                        this.InitializeFromTemplateComboBox();

                        // check the item count and select the first item by default and fill the fee details
                        this.SelectDefaultFromTemplateItem();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ApplyFeesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ApplyFeesButton_Click(object sender, EventArgs e)
        {
            try
            {
             
                //LoadSystemSnapShotDetails saveSnapShotDetails = new LoadSystemSnapShotDetails();
                int recordSetType;
                int.TryParse(this.IncludeRowsComboBox.SelectedValue.ToString(), out recordSetType);
                int[] tempArgs = new int[2];
                tempArgs[0] = this.masterFormNo;
                tempArgs[1] = recordSetType;
                this.D9030_f9033_ApplyFeeEvent(this, new DataEventArgs<int[]>(tempArgs)); ;  
                //saveSnapShotDetails.MasterFormNO = this.masterFormNo;
                //saveSnapShotDetails.RecordsetType = recordSetType;
                //saveSnapShotDetails.IsSystemSnapShotLoaded = false;
                //this.D9030_F9033_SetSystemSnapshotEvent(this, new TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails>(saveSnapShotDetails));
                
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
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EnableSaveAndApplyFeeButton();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FeeManagementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FeeManagementPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the FeeManagementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FeeManagementPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.FeeToolTip.SetToolTip(this.FeeManagementPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion  Events

        #region Private Methods

        /// <summary>
        /// Initializes from template combo box.
        /// </summary>
        private void InitializeFromTemplateComboBox()
        {
            try
            {
                this.FromTemplateComboBox.DataSource = null;
                this.feeManagementData.f15050_pclst_Fee.Clear();
                this.feeManagementData.Merge(this.form15050Control.WorkItem.F15050_ComboData());

                if (this.feeManagementData.f15050_pclst_Fee.Rows.Count > 0)
                {
                    this.FromTemplateComboBox.DisplayMember = this.feeManagementData.f15050_pclst_Fee.DescriptionColumn.ColumnName;
                    this.FromTemplateComboBox.ValueMember = this.feeManagementData.f15050_pclst_Fee.FeeIDColumn.ColumnName;
                    this.FromTemplateComboBox.DataSource = this.feeManagementData.f15050_pclst_Fee;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Initializes the include rows combo box.
        /// </summary>
        private void InitializeIncludeRowsComboBox()
        {
            try
            {
                Hashtable includeRows = new Hashtable();
                includeRows.Add("Checked", 2);
                includeRows.Add("Not Checked", 3);
                includeRows.Add("Only Current Row", 4);
                this.includeRowData.LoadGeneralComboData(includeRows);
                DataRow dr;
                dr = this.includeRowData.ComboBoxDataTable.NewRow();
                dr[0] = 1;
                dr[1] = "All";
                this.includeRowData.ComboBoxDataTable.Rows.InsertAt(dr, 0);
                this.IncludeRowsComboBox.DisplayMember = this.includeRowData.ComboBoxDataTable.KeyNameColumn.ColumnName;
                this.IncludeRowsComboBox.ValueMember = this.includeRowData.ComboBoxDataTable.KeyIdColumn.ColumnName;
                this.includeRowData.ComboBoxDataTable.DefaultView.Sort = this.includeRowData.ComboBoxDataTable.KeyIdColumn.ColumnName + " ASC";
                this.IncludeRowsComboBox.DataSource = this.includeRowData.ComboBoxDataTable.DefaultView;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Initializes the fee type combo box.
        /// </summary>
        private void InitializeFeeTypeComboBox()
        {
            this.feeManagementData.ListFeeTypes.Clear();
            this.feeManagementData.Merge(this.form15050Control.WorkItem.F15050_ListFeeTypes(TerraScanCommon.UserId));

            this.FeeTypeComboBox.DisplayMember = this.feeManagementData.ListFeeTypes.FeeTypeColumn.ColumnName;
            this.FeeTypeComboBox.ValueMember = this.feeManagementData.ListFeeTypes.FeeTypeIDColumn.ColumnName;

            if (this.feeManagementData.ListFeeTypes.Rows.Count > 0)
            {
                this.FeeTypeComboBox.DataSource = this.feeManagementData.ListFeeTypes;
                this.FeeTypeComboBox.SelectedIndex = 0;
            }

            if (this.FeeTypeComboBox.SelectedValue != null)
            {
                byte.TryParse(this.FeeTypeComboBox.SelectedValue.ToString(), out this.feeTypeId);
            }
        }

        /// <summary>
        /// Gets the config roll year.
        /// </summary>
        private void GetConfigRollYear()
        {
            CommentsData.GetCommentsConfigDetailsRow rollYearRow;
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            this.getRollYearConfigurationValue = this.form15050Control.WorkItem.GetConfigDetails("TR_RollYear");

            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                rollYearRow = (CommentsData.GetCommentsConfigDetailsRow)this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows[0];

                int.TryParse(rollYearRow.ConfigurationValue, out this.rollYear);
            }
        }

        /// <summary>
        /// Selects the default from template item.
        /// </summary>
        private void SelectDefaultFromTemplateItem()
        {
            if (this.FromTemplateComboBox.Items.Count > 1)
            {
                this.FromTemplateComboBox.SelectedIndex = 1;
                int.TryParse(this.FromTemplateComboBox.SelectedValue.ToString(), out this.feeId);
                this.FillTextBoxes();
                this.RemoveTemplateButton.Enabled = true;
            }
            else
            {
                this.ClearTextBoxes();
                this.RemoveTemplateButton.Enabled = false;
            }
        }

        /// <summary>
        /// Enables the save and apply fee button.
        /// </summary>
        private void EnableSaveAndApplyFeeButton()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim())
                    && (!this.AmmountTextBox.DecimalTextBoxValue.Equals(0))
                    && (this.accountId > 0))
                {
                    this.SaveTemplateButton.Enabled = true;
                    this.ApplyFeesButton.Enabled = true;
                }
                else
                {
                    this.SaveTemplateButton.Enabled = false;
                    this.ApplyFeesButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the template fields.
        /// </summary>
        private void PopulateTemplateFields()
        {
            if (this.FromTemplateComboBox.SelectedValue != null)
            {
                int.TryParse(this.FromTemplateComboBox.SelectedValue.ToString(), out this.feeId);

                if (this.feeId > 0)
                {
                    this.FillTextBoxes();
                    this.RemoveTemplateButton.Enabled = true;
                }
                else
                {
                    this.RemoveTemplateButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Fills the text boxes.
        /// </summary>
        private void FillTextBoxes()
        {
            try
            {
                decimal amountVal;
                this.feeManagementData.GetISValidKey.Clear();
                this.feeManagementData.GetFeeDetails.Clear();
                this.feeManagementData.Merge(this.form15050Control.WorkItem.F15050_getDatas(this.feeId));

                if (this.feeManagementData.GetFeeDetails.Rows.Count > 0)
                {
                    byte.TryParse(this.feeManagementData.GetFeeDetails.Rows[0][this.feeManagementData.GetFeeDetails.FeeTypeIDColumn].ToString(), out this.feeTypeId);
                    this.FeeTypeComboBox.SelectedValue = this.feeTypeId;
                    DescriptionTextBox.Text = this.feeManagementData.GetFeeDetails.Rows[0][this.feeManagementData.GetFeeDetails.DescriptionColumn].ToString();
                    decimal.TryParse(this.feeManagementData.GetFeeDetails.Rows[0][this.feeManagementData.GetFeeDetails.AmountColumn].ToString(), out amountVal);
                    AmmountTextBox.Text = amountVal.ToString();
                    AccountTextBox.Text = this.feeManagementData.GetFeeDetails.Rows[0][this.feeManagementData.GetFeeDetails.AccountNameColumn].ToString();
                    int.TryParse(this.feeManagementData.GetFeeDetails.Rows[0][this.feeManagementData.GetFeeDetails.AccountIDColumn].ToString(), out this.accountId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Clears the text boxes.
        /// </summary>
        private void ClearTextBoxes()
        {
            try
            {
                this.DescriptionTextBox.Text = string.Empty;
                this.AmmountTextBox.Text = string.Empty;
                this.AccountTextBox.Text = string.Empty;

                if (this.FeeTypeComboBox.Items.Count > 0)
                {
                    this.FeeTypeComboBox.SelectedIndex = 0;
                }
                else
                {
                    this.FeeTypeComboBox.SelectedIndex = -1;
                    this.feeTypeId = 0;
                }

                this.accountId = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Saves the template.
        /// </summary>
        private void SaveTemplate()
        {
            try
            {
                int returnFeeTemplateId = this.form15050Control.WorkItem.F15050_SaveFeeManagement(Convert.ToInt32(FromTemplateComboBox.SelectedValue.ToString()), this.DescriptionTextBox.Text.Trim(), this.AmmountTextBox.DecimalTextBoxValue, this.accountId, TerraScanCommon.UserId, this.feeTypeId);

                // Initialize the from template combo with new items
                this.InitializeFromTemplateComboBox();

                // check the item count and set the recently saved template item
                if (this.FromTemplateComboBox.Items.Count > 1)
                {
                    this.FromTemplateComboBox.SelectedValue = returnFeeTemplateId;
                    this.FillTextBoxes();
                    this.RemoveTemplateButton.Enabled = true;
                }
                else
                {
                    this.ClearTextBoxes();
                    this.RemoveTemplateButton.Enabled = false;
                }

                this.FromTemplateComboBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Private Methods
    }
}
