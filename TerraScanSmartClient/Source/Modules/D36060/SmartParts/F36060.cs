//--------------------------------------------------------------------------------------------
// <copyright file="F36060.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36060 FS Deprecation.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15/12/2007       VijayaKumar.M      Created
//***********************************************************************************************/

namespace D36060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
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
    using TerraScan.Utilities;
    using Infrastructure.Interface;

    /// <summary>
    /// F36060 Class file
    /// </summary>
    [SmartPart]
    public partial class F36060 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the key id(DeprTableID) 
        /// </summary>
        private int keyId;

        /// <summary>
        /// Used to store the lifeCount
        /// </summary>
        private int lifeCount;

        /// <summary>
        /// Used to store the form36060DepreciationData
        /// </summary>
        private F36060DepreciationData form36060DepreciationData = new F36060DepreciationData();

        /// <summary>
        /// Used to store the DepreciationGridDatatable (will be binded Depreciation Grid)
        /// </summary>
        private F36060DepreciationData.ListDepreciationGridTablesDataTable listDepreciationGridTablesDataTable = new F36060DepreciationData.ListDepreciationGridTablesDataTable();

        /// <summary>
        /// Used to store the DepreciationIteamsGridDatatable (will be Binded to DepreciationIteams Grid)
        /// </summary>
        private F36060DepreciationData.ListDepreciationIteamsTablesDataTable depreciationIteamsGridDatatable = new F36060DepreciationData.ListDepreciationIteamsTablesDataTable();

        /// <summary>
        /// controller F15015
        /// </summary>
        private F36060Controller form36060Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36060"/> class.
        /// </summary>
        public F36060()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36033"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36060(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.DeprecationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DeprecationPictureBox.Height, this.DeprecationPictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F36060Control
        /// </summary>
        [CreateNew]
        public F36060Controller Form36060Control
        {
            get { return this.form36060Control as F36060Controller; }
            set { this.form36060Control = value; }
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
            this.Cursor = Cursors.WaitCursor;
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.AllowDeprGridSortable(false);
            this.form36060DepreciationData.Clear();
            this.flagLoadOnProcess = true;
            this.ClearDeprecationheaderPartControls();
            this.LoadDepreCationGrid();
            this.CreatedDeprecationItemsGrid(1);
            ////to get the roll year
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            this.getRollYearConfigurationValue = this.form36060Control.WorkItem.GetConfigDetails(SharedFunctions.GetResourceString("DefaultRollYear"));
            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                this.RollYearTextBox.Text = this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString();
            }

            this.LifeTextBox.Text = "1";
            this.lifeCount = 1;
            this.LoadDeprecationItemsGrid();
            this.flagLoadOnProcess = false;
            this.LockControls(true);
            this.ControlLock(!this.PermissionFiled.newPermission);
            if (this.PermissionFiled.newPermission)
            {
                this.TableNameTextBox.Focus();
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.LoadDeprecationControls();

            if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
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
            if ((this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.PermissionFiled.editPermission && this.formMasterPermissionEdit) || (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && this.PermissionFiled.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
            if ((this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.PermissionFiled.editPermission && this.formMasterPermissionEdit) || (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && this.PermissionFiled.newPermission))
            {
                int saveDeprtableId = this.SaveDeprecationtableItems();

                if (saveDeprtableId > 0)
                {
                    SliceReloadActiveRecord currentSliceInfo;
                    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    currentSliceInfo.SelectedKeyId = saveDeprtableId;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                }
            }
            else
            {
                this.ControlLock(false);
                this.LoadDeprecationControls();
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
            if (this.PermissionFiled.deletePermission && this.keyId > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                this.form36060Control.WorkItem.F36060_DeleteDepreciationTables(this.keyId, TerraScanCommon.UserId);
                this.Cursor = Cursors.Default;

                ////to close the master form afther save
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
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
            if (this != null && this.IsDisposed != true && this.Visible && this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                ////For Form funcatonality this is commented
                if (this.form36060DepreciationData.GetDepreciationTables.Rows.Count > 0)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                    this.LockControls(true);

                    ////to set the focus for DepreciationDataGridView
                    this.DepreciationDataGridView.Rows[0].Selected = true;

                    ////to set the focus for DeprecationItemsDataGridView
                    if (this.depreciationIteamsGridDatatable.Rows.Count > 0)
                    {
                        this.DeprecationItemsDataGridView.Rows[0].Selected = true;
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
                    this.LockControls(false);
                }
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
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.LoadDeprecationControls();
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            }
        }

        #endregion Event Subscription

        #region Protected methods

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
        /// Used to Checks whether all required fields exists
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (!this.CheckRequiredFieldsExists())
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Saves the deprecationtable items.
        /// </summary>
        /// <returns>saved or updated key id is returned</returns>
        private int SaveDeprecationtableItems()
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                return this.Form36060Control.WorkItem.F36060_SaveDepreciationTables(0, this.DeprecationSaveXmlstring(), Utility.GetXmlString(this.depreciationIteamsGridDatatable), TerraScanCommon.UserId);
            }
            else
            {
                return this.Form36060Control.WorkItem.F36060_SaveDepreciationTables(this.keyId, this.DeprecationSaveXmlstring(), Utility.GetXmlString(this.depreciationIteamsGridDatatable), TerraScanCommon.UserId);
            }
        }

        /// <summary>
        /// To check whether all required fields exists
        /// </summary>
        /// 
        /// <returns>
        /// true -- when all required exists
        /// false -- any fields missing
        /// </returns>
        private bool CheckRequiredFieldsExists()
        {
            if (!string.IsNullOrEmpty(this.TableNameTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()) && !this.RollYearTextBox.Text.Trim().Equals("0") && !string.IsNullOrEmpty(this.ActiveComboBox.Text.Trim()) && !string.IsNullOrEmpty(this.PersonalPropertyComboBox.Text.Trim()) && !string.IsNullOrEmpty(this.LifeTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[0].Cells[this.Description.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[1].Cells[this.Description.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[2].Cells[this.Description.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[3].Cells[this.Description.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[4].Cells[this.Description.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[5].Cells[this.Description.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[0].Cells[this.Multipler.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[1].Cells[this.Multipler.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[2].Cells[this.Multipler.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[3].Cells[this.Multipler.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[4].Cells[this.Multipler.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[5].Cells[this.Multipler.Name].Value.ToString().Trim()))
            {
                ////Added by Biju on 24/May/2010 to fix #7131
                for (int i=0; i < this.listDepreciationGridTablesDataTable.Rows.Count; i++)
                {
                    this.listDepreciationGridTablesDataTable[i][this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName] = this.listDepreciationGridTablesDataTable[i][this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName].ToString().Replace("%", "");
                }
                ////till here
                this.DepreciationDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.listDepreciationGridTablesDataTable.AcceptChanges();
                this.DeprecationItemsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.depreciationIteamsGridDatatable.AcceptChanges();
                ////Modified by Biju on 24/May/2010 to fix #7131
                string listDepreciationGridTablesFilterConditions = "(" + this.listDepreciationGridTablesDataTable.DescriptionColumn.ColumnName + " IS  NULL OR " + this.listDepreciationGridTablesDataTable.DescriptionColumn.ColumnName + " = '' OR " + this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName + " IS  NULL OR " + this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName + " < 0.0) AND (EmptyRecord$ = False)"; //// OR (" + this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName + " <=0) OR (" + this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName + " > 100)";
                string listDepreItemsGridFilterConditions = "(" + this.depreciationIteamsGridDatatable.Depr1Column.ColumnName + " IS  NULL OR " + this.depreciationIteamsGridDatatable.Depr1Column.ColumnName + " < 0.0 OR " + this.depreciationIteamsGridDatatable.Depr2Column.ColumnName + " IS  NULL OR " + this.depreciationIteamsGridDatatable.Depr2Column.ColumnName + " < 0.0 OR " + this.depreciationIteamsGridDatatable.Depr3Column.ColumnName + " IS  NULL OR " + this.depreciationIteamsGridDatatable.Depr3Column.ColumnName + " < 0 OR " + this.depreciationIteamsGridDatatable.Depr4Column.ColumnName + " IS  NULL OR " + this.depreciationIteamsGridDatatable.Depr4Column.ColumnName + " < 0.0 OR " + this.depreciationIteamsGridDatatable.Depr5Column.ColumnName + " IS  NULL OR " + this.depreciationIteamsGridDatatable.Depr5Column.ColumnName + " < 0.0 OR " + this.depreciationIteamsGridDatatable.Depr6Column.ColumnName + " IS  NULL OR " + this.depreciationIteamsGridDatatable.Depr6Column.ColumnName + " < 0.0) AND (EmptyRecord$ = False)";
                
                DataRow[] listDepreciationGridRowFilter = this.listDepreciationGridTablesDataTable.Select(listDepreciationGridTablesFilterConditions);
                DataRow[] listDepreItemsGridRowFilter = this.depreciationIteamsGridDatatable.Select(listDepreItemsGridFilterConditions);
            
                if (listDepreciationGridRowFilter.Length > 0 || listDepreItemsGridRowFilter.Length > 0)
                {
                    ////missing required fields
                    return false;
                }
                return true;
            }
            else
            {
                ////missing required fields
                return false;
            }
        }

        /// <summary>
        /// All Deprecaition values are made to an xml string
        /// </summary>
        /// <returns>
        /// xml string containing the Deprecaition values
        /// </returns>
        private string DeprecationSaveXmlstring()
        {
            Int16 tempRollyear;
            int saveLifevalue;

            this.form36060DepreciationData.GetDepreciationTables.Rows.Clear();
            F36060DepreciationData.GetDepreciationTablesRow deprecationDataRow = this.form36060DepreciationData.GetDepreciationTables.NewGetDepreciationTablesRow();

            deprecationDataRow.DeprName = this.TableNameTextBox.Text.Trim();

            Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollyear);

            deprecationDataRow.RollYear = tempRollyear;

            if (String.Equals(this.ActiveComboBox.Text.ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                deprecationDataRow.IsActive = true;
            }
            else
            {
                deprecationDataRow.IsActive = false;
            }

            if (String.Equals(this.PersonalPropertyComboBox.Text.ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                deprecationDataRow.IsPersonalProperty = true;
            }
            else
            {
                deprecationDataRow.IsPersonalProperty = false;
            }

            int.TryParse(this.LifeTextBox.Text.Trim(), out saveLifevalue);
            deprecationDataRow.Life = saveLifevalue;

            deprecationDataRow.Description = this.DescriptionTextBox.Text.Trim();

            deprecationDataRow.Desc1 = this.DepreciationDataGridView.Rows[0].Cells[this.Description.Name].Value.ToString().Trim();
            deprecationDataRow.Desc2 = this.DepreciationDataGridView.Rows[1].Cells[this.Description.Name].Value.ToString().Trim();
            deprecationDataRow.Desc3 = this.DepreciationDataGridView.Rows[2].Cells[this.Description.Name].Value.ToString().Trim();
            deprecationDataRow.Desc4 = this.DepreciationDataGridView.Rows[3].Cells[this.Description.Name].Value.ToString().Trim();
            deprecationDataRow.Desc5 = this.DepreciationDataGridView.Rows[4].Cells[this.Description.Name].Value.ToString().Trim();
            deprecationDataRow.Desc6 = this.DepreciationDataGridView.Rows[5].Cells[this.Description.Name].Value.ToString().Trim();

            deprecationDataRow.Cond1 = this.DepreciationDataGridView.Rows[0].Cells[this.Multipler.Name].Value.ToString().Trim();
            deprecationDataRow.Cond2 = this.DepreciationDataGridView.Rows[1].Cells[this.Multipler.Name].Value.ToString().Trim();
            deprecationDataRow.Cond3 = this.DepreciationDataGridView.Rows[2].Cells[this.Multipler.Name].Value.ToString().Trim();
            deprecationDataRow.Cond4 = this.DepreciationDataGridView.Rows[3].Cells[this.Multipler.Name].Value.ToString().Trim();
            deprecationDataRow.Cond5 = this.DepreciationDataGridView.Rows[4].Cells[this.Multipler.Name].Value.ToString().Trim();
            deprecationDataRow.Cond6 = this.DepreciationDataGridView.Rows[5].Cells[this.Multipler.Name].Value.ToString().Trim();

            this.form36060DepreciationData.GetDepreciationTables.Rows.Add(deprecationDataRow);

            return Utility.GetXmlString(this.form36060DepreciationData.GetDepreciationTables.Copy());
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.TableNameTextBox.LockKeyPress = controlLook;
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.LifeTextBox.LockKeyPress = controlLook;
            this.DescriptionTextBox.LockKeyPress = controlLook;
            this.ActiveComboBox.Enabled = !controlLook;
            this.PersonalPropertyComboBox.Enabled = !controlLook;
        }

        /// <summary>
        /// To Clear the Depreciation Header part Controls
        /// </summary>
        private void ClearDeprecationheaderPartControls()
        {
            this.TableNameTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.LifeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.TableIDTextBox.Text = string.Empty;
            this.ActiveComboBox.SelectedIndex = 1;
            this.PersonalPropertyComboBox.SelectedIndex = 1;

            this.DepreciationDataGridView.ClearSorting();
            this.DeprecationItemsDataGridView.ClearSorting();
        }

        /// <summary>
        /// Gets the selected row of the ListDepreciationTables.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        /// <returns>Datarow instance of GetDepreciationTables </returns>
        private F36060DepreciationData.GetDepreciationTablesRow GetSelectedRow(int currentRowIndex)
        {
            return (F36060DepreciationData.GetDepreciationTablesRow)this.form36060DepreciationData.GetDepreciationTables.Rows[currentRowIndex];
        }

        /// <summary>
        /// Sets Correspnding values for the depreciation controls.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetDepreciationControls(F36060DepreciationData.GetDepreciationTablesRow selectedRow)
        {
            if (selectedRow != null)
            {
                this.TableNameTextBox.Text = selectedRow.DeprName;
                this.RollYearTextBox.Text = selectedRow.RollYear.ToString();
                this.SetActiveComboboxValue(selectedRow.IsActive.ToString(), "Active");
                this.SetActiveComboboxValue(selectedRow.IsPersonalProperty.ToString(), "PersonalProperty");
                this.LifeTextBox.Text = selectedRow.Life.ToString();
                this.lifeCount = selectedRow.Life;
                this.TableIDTextBox.Text = selectedRow.DeprTableID.ToString();
                ////this.currentTableID = selectedRow.DeprTableID;
                this.DescriptionTextBox.Text = selectedRow.Description;
            }
        }

        /// <summary>
        /// To Create datatable which will be bind to DepreciationGrid
        /// </summary>
        private void CreatedDeprecationDataTable()
        {
            ////to create datatable which will be bind to DepreciationGrid
            ////here records come in single row which made into a datatable
            int descColumnIndex = 7;
            int multipleColumnIndex = 8;

            this.listDepreciationGridTablesDataTable.Clear();

            if (this.form36060DepreciationData.GetDepreciationTables.Rows.Count > 0)
            {
                ////Here the Data for DepreciationDataGridView Grid come in a single row which has to stored in a  datatable format to bind to Grid
                for (int i = 0; i < 6; i++)
                {
                    this.listDepreciationGridTablesDataTable.Rows.Add(this.listDepreciationGridTablesDataTable.NewListDepreciationGridTablesRow());
                    this.listDepreciationGridTablesDataTable.Rows[i][this.listDepreciationGridTablesDataTable.SerialNoColumn.ColumnName] = i + 1;
                    this.listDepreciationGridTablesDataTable.Rows[i][this.listDepreciationGridTablesDataTable.DescriptionColumn.ColumnName] = this.form36060DepreciationData.GetDepreciationTables.Rows[0][descColumnIndex];
                    this.listDepreciationGridTablesDataTable.Rows[i][this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName] = this.form36060DepreciationData.GetDepreciationTables.Rows[0][multipleColumnIndex];
                    descColumnIndex = descColumnIndex + 2;
                    multipleColumnIndex = multipleColumnIndex + 2;
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    this.listDepreciationGridTablesDataTable.Rows.Add(this.listDepreciationGridTablesDataTable.NewListDepreciationGridTablesRow());
                    this.listDepreciationGridTablesDataTable.Rows[i][this.listDepreciationGridTablesDataTable.SerialNoColumn.ColumnName] = i + 1;
                }
            }

            this.SerialNumber.ReadOnly = true;
        }

        /// <summary>
        /// To Create datatable which will be bind to Depreciation Iteams Grid.
        /// </summary>
        /// <param name="rowCount">Depreciation Iteams Grid row count</param>
        private void CreatedDeprecationItemsGrid(int rowCount)
        {
            this.EffectiveAge.ReadOnly = false;

            for (int i = 0; i < rowCount; i++)
            {
                this.depreciationIteamsGridDatatable.Rows.Add(this.depreciationIteamsGridDatatable.NewListDepreciationIteamsTablesRow());
                this.depreciationIteamsGridDatatable.Rows[i][this.depreciationIteamsGridDatatable.EffectiveAgeColumn.ColumnName] = i + 1;
            }

            this.EffectiveAge.ReadOnly = false;
        }

        /// <summary>
        /// To Loads the deprecation grid.
        /// </summary>
        private void LoadDepreCationGrid()
        {
            ////to create datatable which will be bind to DepreciationGrid
            ////here records come in single row which made into a datatable
            this.CreatedDeprecationDataTable();

            if (this.listDepreciationGridTablesDataTable.Rows.Count > 0)
            {
               this.DepreciationDataGridView.DataSource = this.listDepreciationGridTablesDataTable.DefaultView;
               this.DepreciationDataGridView.Rows[0].Selected = true;
            }
            else
            {
                this.listDepreciationGridTablesDataTable.Clear();
                this.DepreciationDataGridView.DataSource = this.listDepreciationGridTablesDataTable.DefaultView;
                this.DepreciationDataGridView.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// Loads the deprecation items grid.
        /// </summary>
        private void LoadDeprecationItemsGrid()
        {
            this.DeprecationItemsDataGridView.ClearSorting();
            if (this.depreciationIteamsGridDatatable.Rows.Count > 0)
            {
                this.DeprecationItemsDataGridView.DataSource = this.depreciationIteamsGridDatatable.DefaultView;
                this.DeprecationItemsDataGridView.Rows[0].Selected = true;
            }
            else
            {
                this.depreciationIteamsGridDatatable.Clear();
                this.DeprecationItemsDataGridView.DataSource = this.depreciationIteamsGridDatatable.DefaultView;
                this.DeprecationItemsDataGridView.Rows[0].Selected = false;
            }

            if (this.DeprecationItemsDataGridView.Rows.Count > this.DeprecationItemsDataGridView.NumRowsVisible)
            {
                this.DeprecationItemsVerticalScroll.Visible = false;
            }
            else
            {
                this.DeprecationItemsVerticalScroll.Visible = true;
            }
        }

        /// <summary>
        /// To Customize Deprecaition Controls.
        /// </summary>
        private void CustomizeDepricationControls()
        {
            ////to set the Max length for controls
            this.TableNameTextBox.MaxLength = this.form36060DepreciationData.GetDepreciationTables.DeprNameColumn.MaxLength;
            this.DescriptionTextBox.MaxLength = this.form36060DepreciationData.GetDepreciationTables.DescriptionColumn.MaxLength;

            ////to customize the Active Combox box
            this.ActiveComboBox.Items.Clear();
            this.ActiveComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.ActiveComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));

            ////to customize the Personal Property Combox box
            this.PersonalPropertyComboBox.Items.Clear();
            this.PersonalPropertyComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.PersonalPropertyComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));

            ////to customize the Deprecation Grid controls
            this.SerialNumber.DataPropertyName = this.listDepreciationGridTablesDataTable.SerialNoColumn.ColumnName;
            ////Here the Data for column Description come in a single row which has to stored in a Grid format
            this.Description.DataPropertyName = this.listDepreciationGridTablesDataTable.DescriptionColumn.ColumnName;
            ////Here the Data for column multipler come in a single row which has to stored in a Grid format
            this.Multipler.DataPropertyName = this.listDepreciationGridTablesDataTable.MultiplierColumn.ColumnName;
            this.DeprGridSortColumn.DataPropertyName = this.listDepreciationGridTablesDataTable.SerialNoColumn.ColumnName;
            this.SerialNumber.DisplayIndex = 0;
            this.Description.DisplayIndex = 1;
            this.Multipler.DisplayIndex = 2;
            this.DeprGridSortColumn.DisplayIndex = 3;
            this.DepreciationDataGridView.PrimaryKeyColumnName = this.listDepreciationGridTablesDataTable.SerialNoColumn.ColumnName;

            ///// to customize the Deprecation Item grid controls
            this.DeprItemID.DataPropertyName = this.depreciationIteamsGridDatatable.DeprItemIDColumn.ColumnName;
            this.DeprTableID.DataPropertyName = this.depreciationIteamsGridDatatable.DeprTableIDColumn.ColumnName;
            this.EffectiveAge.DataPropertyName = this.depreciationIteamsGridDatatable.EffectiveAgeColumn.ColumnName;
            this.DeprDepr1.DataPropertyName = this.depreciationIteamsGridDatatable.Depr1Column.ColumnName;
            this.DeprDepr2.DataPropertyName = this.depreciationIteamsGridDatatable.Depr2Column.ColumnName;
            this.DeprDepr3.DataPropertyName = this.depreciationIteamsGridDatatable.Depr3Column.ColumnName;
            this.DeprDepr4.DataPropertyName = this.depreciationIteamsGridDatatable.Depr4Column.ColumnName;
            this.DeprDepr5.DataPropertyName = this.depreciationIteamsGridDatatable.Depr5Column.ColumnName;
            this.DeprDepr6.DataPropertyName = this.depreciationIteamsGridDatatable.Depr6Column.ColumnName;

            this.DeprItemID.DisplayIndex = 0;
            this.DeprTableID.DisplayIndex = 1;
            this.EffectiveAge.DisplayIndex = 2;
            this.DeprDepr1.DisplayIndex = 3;
            this.DeprDepr2.DisplayIndex = 4;
            this.DeprDepr3.DisplayIndex = 5;
            this.DeprDepr4.DisplayIndex = 6;
            this.DeprDepr5.DisplayIndex = 7;
            this.DeprDepr6.DisplayIndex = 8;
            this.DeprecationItemsDataGridView.PrimaryKeyColumnName = this.depreciationIteamsGridDatatable.DeprItemIDColumn.ColumnName;
        }

        /// <summary>
        /// Used to Set the correct value to the ComboBox
        /// </summary>
        /// <param name="comboxString">The combox string.</param>
        /// <param name="comboName">The ComboBox Name</param>
        private void SetActiveComboboxValue(string comboxString, string comboName)
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
                if (comboName == "Active")
                {
                    correctIndex = this.ActiveComboBox.FindString(comboxString);
                }
                else
                {
                    correctIndex = this.PersonalPropertyComboBox.FindString(comboxString);
                }
            }

            if (comboName == "Active")
            {
                this.ActiveComboBox.SelectedIndex = correctIndex;
            }
            else
            {
                this.PersonalPropertyComboBox.SelectedIndex = correctIndex;
            }
        }

        /// <summary>
        /// Formates the current cell value.
        /// </summary>
        /// <param name="cellDisplay">The cell display.</param>
        /// <returns>
        /// string conataing the cell value in a formate
        /// </returns>
        private string FormateCurrentCellValue(decimal cellDisplay)
        {
            string displayString = string.Empty;
            if (cellDisplay <= 100 && cellDisplay >= 0)
            {
                decimal dispValue = 0;
                dispValue = cellDisplay;
                ////string tempvalue = cellDisplay.ToString();  
                ////int stringLength = tempvalue.Length;
                ////int decPosition = tempvalue.IndexOf(".");

                ////// Get Precision value
                ////if (decPosition != -1)
                ////{
                ////    if (stringLength - (decPosition + 1) > 0)
                ////    {
                ////        tempvalue = tempvalue.Substring(decPosition + 1, stringLength - (decPosition + 1)).Trim();
                ////        Decimal.TryParse(tempvalue, out dispValue);
                ////    }
                ////}

                ////dispValue = Math.Round(cellDisplay, tempvalue.Length);


                char[] chseperator = new char[1];
                chseperator[0] = '.';
                string[] splitString = dispValue.ToString().Split(chseperator);

                if (splitString.Length >= 1)
                {
                    if (splitString.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(splitString[1]))
                        {
                            if (Convert.ToInt32(splitString[1]) > 0)
                            {
                                displayString = splitString[0] + "." + splitString[1] + "%";
                                //// displayString = splitString[0] + "." + splitString[1];
                            }
                            else
                            {
                                displayString = splitString[0] + "%";
                                //// displayString = splitString[0];
                            }
                        }
                    }
                    else
                    {
                        //// displayString = splitString[0] + "%";
                        displayString = splitString[0];
                    }
                }
            }

            return displayString;
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.AllowDeprGridSortable(false);
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.TableNamePanel.Enabled = lockControl;
            this.RollYearPanel.Enabled = lockControl;
            this.ActivePanel.Enabled = lockControl;
            this.LifePanel.Enabled = lockControl;
            this.TableIDpanel.Enabled = lockControl;
            this.DescriptionPanel.Enabled = lockControl;
            this.PersonalPropertyPanel.Enabled = lockControl;
            this.DepreciationDataGridPanel.Enabled = lockControl;
            this.DeprecationItemsDataGridViewPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Make the column of the deprecation item grid is editable or not
        /// </summary>
        /// <param name="iscolumnReadOnly">if set to <c>true</c> [iscolumn read only].</param>
        private void GridColumnReadOnly(bool iscolumnReadOnly)
        {
            this.EffectiveAge.ReadOnly = iscolumnReadOnly;
            this.DeprDepr1.ReadOnly = iscolumnReadOnly;
            this.DeprDepr2.ReadOnly = iscolumnReadOnly;
            this.DeprDepr3.ReadOnly = iscolumnReadOnly;
            this.DeprDepr4.ReadOnly = iscolumnReadOnly;
            this.DeprDepr5.ReadOnly = iscolumnReadOnly;
            this.DeprDepr6.ReadOnly = iscolumnReadOnly;
        }

        /// <summary>
        /// Selects the deprecation item grid rows where column EmptyRecord$ = False.
        /// </summary>
        private void SelectDeprecationItemGridRows()
        {
            DataRow[] tempRowCollection;
            F36060DepreciationData.ListDepreciationIteamsTablesDataTable tempepreciationIteamsGridDatatable = new F36060DepreciationData.ListDepreciationIteamsTablesDataTable();
            this.depreciationIteamsGridDatatable.AcceptChanges();
            tempRowCollection = this.depreciationIteamsGridDatatable.Select("EmptyRecord$ = False");
            DataRow tempRow;

            foreach (DataRow dataRow in tempRowCollection)
            {
                tempRow = tempepreciationIteamsGridDatatable.NewRow();
                tempRow[0] = dataRow[0];
                tempRow[1] = dataRow[1];
                tempRow[2] = dataRow[2];
                tempRow[3] = dataRow[3];
                tempRow[4] = dataRow[4];
                tempRow[5] = dataRow[5];
                tempRow[6] = dataRow[6];
                tempRow[7] = dataRow[7];
                tempRow[8] = dataRow[8];
                tempepreciationIteamsGridDatatable.Rows.Add(tempRow);
            }

            this.depreciationIteamsGridDatatable.Clear();
            this.depreciationIteamsGridDatatable.Merge(tempepreciationIteamsGridDatatable);
            this.depreciationIteamsGridDatatable.AcceptChanges();
        }

        /// <summary>
        /// Loads the deprecation controls.
        /// </summary>
        private void LoadDeprecationControls()
        {
            this.flagLoadOnProcess = true;
            this.LockControls(true);
            this.ClearDeprecationheaderPartControls();
            this.AllowDeprGridSortable(true);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.form36060DepreciationData = this.form36060Control.WorkItem.F36060_GetDepreciationTables(this.keyId);
            this.depreciationIteamsGridDatatable = this.form36060DepreciationData.ListDepreciationIteamsTables;

            if (this.form36060DepreciationData.GetDepreciationTables.Rows.Count > 0)
            {
                ////to fill the header part controls
                this.SetDepreciationControls(this.GetSelectedRow(0));
            }

            this.LoadDepreCationGrid();
            this.LoadDeprecationItemsGrid();

            if (this.form36060DepreciationData.GetDepreciationTables.Rows.Count > 0)
            {
                this.LockControls(true);
            }
            else
            {
                this.LockControls(false);
            }

            ////to set the focus to first editable controls
            if (this.form36060DepreciationData.GetDepreciationTables.Rows.Count > 0)
            {
                this.TableNameTextBox.Focus();
            }

            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Depreciations data grid cell permission.
        /// </summary>
        /// <param name="currentCell">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridCellPermission(DataGridViewCellEventArgs currentCell)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.DepreciationDataGridViewCellEvents(currentCell, this.PermissionFiled.newPermission);
            }
            else
            {
                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                {
                    this.DepreciationDataGridViewCellEvents(currentCell, true);
                }
                else
                {
                    this.DepreciationDataGridViewCellEvents(currentCell, false);
                }
            }
        }

        /// <summary>
        /// Depreciation Items Grid cell permission
        /// </summary>
        /// <param name="depreciationItemsCurrentCell">Depreciation Items Grid Current Cell</param>
        private void DepreciationItemsGridCellPermission(DataGridViewCellEventArgs depreciationItemsCurrentCell)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.DepreciationItemsGridViewCellEvents(depreciationItemsCurrentCell, this.PermissionFiled.newPermission);
            }
            else
            {
                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                {
                    this.DepreciationItemsGridViewCellEvents(depreciationItemsCurrentCell, true);
                }
                else
                {
                    this.DepreciationItemsGridViewCellEvents(depreciationItemsCurrentCell, false);
                }
            }
        }

        /// <summary>
        /// Allows the Depr grid sortable or not.
        /// </summary>
        /// <param name="issortable">if set to <c>true</c> [issortable].</param>
        private void AllowDeprGridSortable(bool issortable)
        {
            if (issortable)
            {
                this.DepreciationDataGridView.Columns[this.Description.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DepreciationDataGridView.Columns[this.Multipler.Name].SortMode = DataGridViewColumnSortMode.Programmatic;

                this.DeprecationItemsDataGridView.Columns[this.DeprItemID.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.DeprTableID.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.EffectiveAge.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr1.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr2.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr3.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr4.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr5.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr6.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            else
            {
                this.DepreciationDataGridView.Columns[this.Description.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DepreciationDataGridView.Columns[this.Multipler.Name].SortMode = DataGridViewColumnSortMode.NotSortable;

                this.DeprecationItemsDataGridView.Columns[this.DeprItemID.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.DeprTableID.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.EffectiveAge.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr1.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr2.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr3.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr4.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr5.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.DeprecationItemsDataGridView.Columns[this.DeprDepr6.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion Methods

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36060 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36060_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeDepricationControls();
                this.LoadDeprecationControls();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Load

        #region Events

        /// <summary>
        /// Deprecation Header part controls text change events
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprecationControlsEdit(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region DepreciationDataGridView Events

        /// <summary>
        /// Handles the CellFormatting event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        decimal outDecimal;
                        decimal.TryParse(e.Value.ToString().Replace("%", "").Trim(), out outDecimal);
                        if (outDecimal != 0)
                        {
                            e.Value = outDecimal.ToString("#,##0") + "%";
                            e.FormattingApplied = true;
                            ////e.Value = this.FormateCurrentCellValue(outDecimal);
                        }
                        else
                        {
                            e.Value = "0.0 %";
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
        /// Handles the CellParsing event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                // Only paint if desired column
                if (e.ColumnIndex == 2)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell
                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        if (!string.IsNullOrEmpty(tempvalue))
                        {
                            Decimal outDecimal;

                            if (tempvalue.EndsWith("."))
                            {
                                tempvalue = string.Concat(tempvalue, "0");
                            }
                            ////remove soecial character
                            tempvalue = tempvalue.Replace("%", "");
                            if (Decimal.TryParse(tempvalue, out outDecimal))
                            {
                                tempvalue = outDecimal.ToString();

                                if (tempvalue.Contains("-"))
                                {
                                    outDecimal = Decimal.Zero;
                                }
                                else
                                {
                                    outDecimal = Math.Round(outDecimal, 1);
                                }
                            }

                            e.Value = outDecimal;
                            e.ParsingApplied = true;
                        }
                        else
                        {
                            e.Value = DBNull.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.DepreciationDataGridCellPermission(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (this.DepreciationDataGridView[this.Multipler.Name, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.DepreciationDataGridView[this.Multipler.Name, e.RowIndex].Value.ToString().Trim()))
                    {
                        decimal outDecimal;
                        decimal.TryParse(this.DepreciationDataGridView[this.Multipler.Name, e.RowIndex].Value.ToString().Replace("%", "").Trim(), out outDecimal);
                        if (outDecimal != 0)
                        {
                            this.DepreciationDataGridView[this.Multipler.Name, e.RowIndex].Value = this.FormateCurrentCellValue(outDecimal).Replace("%", "").Trim();
                        }
                        else
                        {
                            this.DepreciationDataGridView[this.Multipler.Name, e.RowIndex].Value = DBNull.Value;
                        }

                        if (outDecimal == 0)
                        {
                            this.DepreciationDataGridView[this.Multipler.Name, e.RowIndex].Value = "0%";
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
        /// Handles the ColumnHeaderMouseClick event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 1)
                {
                    this.DepreciationDataGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
            try
            {
                this.DepreciationDataGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                ////to enable the save cancel button in form master
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Depreciations data grid cell events.
        /// </summary>
        /// <param name="cell">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <param name="currentCellPermission">if set to <c>true</c> [current cell permission].</param>
        private void DepreciationDataGridViewCellEvents(DataGridViewCellEventArgs cell, bool currentCellPermission)
        {
            try
            {
                if (cell.RowIndex >= 0)
                {
                    ////When permission does not exists this grid is not editable
                    if (currentCellPermission)
                    {
                        bool hasValues = false;
                        if (cell.RowIndex >= 1)
                        {
                            if ((string.IsNullOrEmpty(this.DepreciationDataGridView[this.Description.Name, (cell.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.DepreciationDataGridView[this.Multipler.Name, (cell.RowIndex - 1)].Value.ToString().Trim())))
                            {
                                if (cell.RowIndex + 1 < this.DepreciationDataGridView.RowCount)
                                {
                                    for (int i = cell.RowIndex; i < this.DepreciationDataGridView.RowCount; i++)
                                    {
                                        if (!string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[i].Cells[this.Description.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.DepreciationDataGridView.Rows[i].Cells[this.Multipler.Name].Value.ToString().Trim()))
                                        {
                                            hasValues = true;
                                            break;
                                        }
                                    }

                                    if (hasValues)
                                    {
                                        this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = false;
                                        this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = false;
                                        this.DepreciationDataGridView.Rows[cell.RowIndex].Selected = false;
                                    }
                                    else
                                    {
                                        if ((string.IsNullOrEmpty(this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].Value.ToString().Trim())))
                                        {
                                            this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = true;
                                            this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = true;
                                        }
                                        else
                                        {
                                            this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = false;
                                            this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = false;
                                            this.DepreciationDataGridView.Rows[cell.RowIndex].Selected = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].Value.ToString().Trim())))
                                    {
                                        this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = true;
                                        this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = false;
                                        this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = false;
                                        this.DepreciationDataGridView.Rows[cell.RowIndex].Selected = false;
                                    }
                                }
                            }
                            else
                            {
                                this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = false;
                                this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = false;
                                this.DepreciationDataGridView.Rows[cell.RowIndex].Selected = false;
                            }
                        }

                        if (cell.RowIndex == 0)
                        {
                            this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = false;
                            this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = false;
                            this.DepreciationDataGridView.Rows[cell.RowIndex].Selected = false;
                        }
                    }
                    else
                    {
                        this.DepreciationDataGridView[this.Description.Name, cell.RowIndex].ReadOnly = true;
                        this.DepreciationDataGridView[this.Multipler.Name, cell.RowIndex].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.DepreciationDataGridCellPermission(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the DepreciationDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DepreciationDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.DepreciationDataGridView.CurrentCell != null)
                {
                    this.DepreciationDataGridView.CurrentCell.ReadOnly = true;
                    this.DepreciationDataGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion DepreciationDataGridView Events

        #region DeprecationItemsDataGridView Events

        /// <summary>
        /// Handles the CellFormatting event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 2)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        decimal outDecimal;
                        decimal.TryParse(e.Value.ToString().Replace("%", "").Trim(), out outDecimal);
                        string s = TerraScanCommon.CustomDecimalFormat(e.Value.ToString());
                        e.Value = outDecimal.ToString(s) + "%";

                        //if (outDecimal != 0)
                        //{
                        //    // Coding modifed for 4334 
                        //    // Sorting is not proper because of string datatype.now it is changed to decimal datatype.
                        //    e.Value = outDecimal.ToString() + "%";
                        //    e.FormattingApplied = true;
                        //    ////e.Value = this.FormateCurrentCellValue(outDecimal);
                        //}
                        //else
                        //{
                        //    e.Value = "0.0 %";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Coding Added for the issue 4334.if invalid character is entered in the celll value it will retain old value
        /// <summary>
        /// Handles the CellParsing event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                // Only paint if desired column
                if (e.ColumnIndex > 2)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell
                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        if (!string.IsNullOrEmpty(tempvalue))
                        {
                            Decimal outDecimal;

                            if (tempvalue.EndsWith("."))
                            {
                                tempvalue = string.Concat(tempvalue, "0");
                            }
                            ////remove soecial character
                            tempvalue = tempvalue.Replace("%", "");
                            if (Decimal.TryParse(tempvalue, out outDecimal))
                            {
                                tempvalue = outDecimal.ToString();

                                if (tempvalue.Contains("-"))
                                {
                                    outDecimal = Decimal.Zero;
                                }
                                else
                                {
                                    int stringLength = tempvalue.Length;
                                    int decPosition = tempvalue.IndexOf(".");
                                  
                                    // Get Precision value
                                    if (decPosition != -1)
                                    {
                                        if (stringLength - (decPosition + 1) > 0)
                                        {
                                            tempvalue = tempvalue.Substring(decPosition + 1, stringLength - (decPosition + 1)).Trim();

                                            Decimal.TryParse(e.Value.ToString().Trim(), out outDecimal);

                                        }
                                    }

                                    outDecimal = Math.Round(outDecimal, tempvalue.Length);
                                }
                            }

                            e.Value = outDecimal;
                            e.ParsingApplied = true;
                        }
                        else
                        {
                            e.Value =decimal.Zero ; //(decimal.Parse(null) );
                            e.ParsingApplied = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        /// <summary>
        /// Handles the CellClick event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.DepreciationItemsGridCellPermission(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To make Depreciations items grid cell readonly based on condtions.
        /// </summary>
        /// <param name="depreciationItemsGridCurrentCell">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <param name="readonlyFlag">if set to <c>true</c> [readonly flag].</param>
        private void DepreciationItemsGridViewCellReadonly(DataGridViewCellEventArgs depreciationItemsGridCurrentCell, bool readonlyFlag)
        {
            try
            {
                if (!readonlyFlag)
                {
                    this.DeprecationItemsDataGridView[this.DeprDepr1.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = false;
                    this.DeprecationItemsDataGridView[this.DeprDepr2.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = false;
                    this.DeprecationItemsDataGridView[this.DeprDepr3.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = false;
                    this.DeprecationItemsDataGridView[this.DeprDepr4.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = false;
                    this.DeprecationItemsDataGridView[this.DeprDepr5.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = false;
                    this.DeprecationItemsDataGridView[this.DeprDepr6.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = false;
                    this.DeprecationItemsDataGridView.Rows[depreciationItemsGridCurrentCell.RowIndex].Selected = false;
                }
                else
                {
                    this.DeprecationItemsDataGridView[this.DeprDepr1.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = true;
                    this.DeprecationItemsDataGridView[this.DeprDepr2.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = true;
                    this.DeprecationItemsDataGridView[this.DeprDepr3.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = true;
                    this.DeprecationItemsDataGridView[this.DeprDepr4.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = true;
                    this.DeprecationItemsDataGridView[this.DeprDepr5.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = true;
                    this.DeprecationItemsDataGridView[this.DeprDepr6.Name, depreciationItemsGridCurrentCell.RowIndex].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To make Depreciations items grid cell readonly.
        /// </summary>
        /// <param name="depreciationItemsGridCell">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <param name="depreciationItemsGridCellPermission">if set to <c>true</c> [depreciation items grid cell permission].</param>
        private void DepreciationItemsGridViewCellEvents(DataGridViewCellEventArgs depreciationItemsGridCell, bool depreciationItemsGridCellPermission)
        {
            try
            {
                if (depreciationItemsGridCell.RowIndex >= 0)
                {
                    ////When permission does not exists this grid is not editable
                    if (depreciationItemsGridCellPermission && depreciationItemsGridCell.RowIndex < this.lifeCount)
                    {
                        bool depreciationItemsGridCellHasValues = false;
                        if (depreciationItemsGridCell.RowIndex >= 1)
                        {
                            if (string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr1.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr2.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr3.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr4.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr5.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr6.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()))
                            {
                                if (depreciationItemsGridCell.RowIndex + 1 < this.DeprecationItemsDataGridView.RowCount)
                                {
                                    for (int i = depreciationItemsGridCell.RowIndex; i < this.DeprecationItemsDataGridView.RowCount; i++)
                                    {
                                        if ((!string.IsNullOrEmpty(this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr1.Name].Value.ToString().Trim()) || this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr1.Name].Value != null) && (!string.IsNullOrEmpty(this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr2.Name].Value.ToString().Trim()) || this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr2.Name].Value != null) && (!string.IsNullOrEmpty(this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr3.Name].Value.ToString().Trim()) || this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr3.Name].Value != null) && (!string.IsNullOrEmpty(this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr4.Name].Value.ToString().Trim()) || this.DeprecationItemsDataGridView.Rows[i].Cells[this.DeprDepr4.Name].Value != null))
                                        {
                                            depreciationItemsGridCellHasValues = true;
                                            break;
                                        }
                                    }

                                    if (depreciationItemsGridCellHasValues)
                                    {
                                        this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, false);
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr1.Name, depreciationItemsGridCell.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr2.Name, depreciationItemsGridCell.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr3.Name, depreciationItemsGridCell.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr4.Name, depreciationItemsGridCell.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr5.Name, depreciationItemsGridCell.RowIndex].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr6.Name, depreciationItemsGridCell.RowIndex].Value.ToString().Trim()))
                                        {
                                            this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, true);
                                        }
                                        else
                                        {
                                            this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, false);
                                        }
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr1.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr2.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr3.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr4.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr5.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()) && string.IsNullOrEmpty(this.DeprecationItemsDataGridView[this.DeprDepr6.Name, (depreciationItemsGridCell.RowIndex - 1)].Value.ToString().Trim()))
                                    {
                                        this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, false);
                                    }
                                    else
                                    {
                                        this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, true);
                                    }
                                }
                            }
                            else
                            {
                                this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, false);
                            }
                        }

                        if (depreciationItemsGridCell.RowIndex == 0)
                        {
                            this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, false);
                        }
                    }
                    else
                    {
                        this.DepreciationItemsGridViewCellReadonly(depreciationItemsGridCell, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 2)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (this.DeprecationItemsDataGridView[e.ColumnIndex, e.RowIndex].Value != null && !string.IsNullOrEmpty(this.DeprecationItemsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Trim()))
                    {
                        decimal outDeprecationItemsDecimal;
                        decimal.TryParse(this.DeprecationItemsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString().Replace("%", "").Trim(), out outDeprecationItemsDecimal);
                        if (outDeprecationItemsDecimal != 0)
                        {
                            this.DeprecationItemsDataGridView[e.ColumnIndex, e.RowIndex].Value = this.FormateCurrentCellValue(outDeprecationItemsDecimal).Replace("%", "").Trim();
                        }
                        else if (outDeprecationItemsDecimal == 0)
                        {
                            this.DeprecationItemsDataGridView[e.ColumnIndex, e.RowIndex].Value = this.FormateCurrentCellValue(outDeprecationItemsDecimal).Replace("%", "").Trim();
                        }
                        //else
                        //{
                        //    this.DeprecationItemsDataGridView[e.ColumnIndex, e.RowIndex].Value = DBNull.Value;
                        //}
                    }
                    //else
                    //{
                    //    this.DeprecationItemsDataGridView[e.ColumnIndex, e.RowIndex].Value = DBNull.Value;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 1)
                {
                    this.DeprecationItemsDataGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.DeprecationItemsDataGrid_TextChanged);
                e.Control.Validated += new EventHandler(this.DeprecationItemsDataGrid_Validated);
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
        private void DeprecationItemsDataGrid_Validated(object sender, EventArgs e)
        {
            try
            {
                this.DeprecationItemsDataGridView.EditingControl.TextChanged -= new EventHandler(this.DeprecationItemsDataGrid_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGrid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////to enable the save cancel button in form master
                this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.DepreciationItemsGridCellPermission(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the DeprecationItemsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void DeprecationItemsDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.DeprecationItemsDataGridView.CurrentCell != null)
                {
                    this.DeprecationItemsDataGridView.CurrentCell.ReadOnly = true;
                    this.DeprecationItemsDataGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion DeprecationItemsDataGridView Events

        /// <summary>
        /// Handles the MouseHover event of the DeprecationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprecationPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.F36060DeprecationFormToolTip.SetToolTip(this.DeprecationPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeprecationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprecationPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LifeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LifeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.Cursor = Cursors.WaitCursor;

                    Int16 tempLifeCount;
                    Int16.TryParse(this.LifeTextBox.Text.Trim(), out tempLifeCount);

                    if (!string.IsNullOrEmpty(this.LifeTextBox.Text.Trim()) && tempLifeCount > 0 && tempLifeCount <= 255)
                    {
                        this.depreciationIteamsGridDatatable.AcceptChanges();
                        if (tempLifeCount < this.DeprecationItemsDataGridView.OriginalRowCount && this.DeprecationItemsDataGridView.OriginalRowCount > 0)
                        {
                            for (int i = this.DeprecationItemsDataGridView.OriginalRowCount - 1; i >= tempLifeCount; i--)
                            {
                                this.depreciationIteamsGridDatatable.Rows.RemoveAt(i);
                            }

                            ////here the grid rows with EmptyRecord$ = False are removed on selection
                            this.SelectDeprecationItemGridRows();
                        }
                        else
                        {
                            ////here the grid rows with EmptyRecord$ = False are removed on selection
                            this.SelectDeprecationItemGridRows();

                            for (int j = this.DeprecationItemsDataGridView.OriginalRowCount; j < tempLifeCount; j++)
                            {
                                this.GridColumnReadOnly(false);

                                this.depreciationIteamsGridDatatable.Rows.Add(this.depreciationIteamsGridDatatable.NewListDepreciationIteamsTablesRow());

                                this.depreciationIteamsGridDatatable.Rows[j][this.depreciationIteamsGridDatatable.EffectiveAgeColumn.ColumnName] = j + 1;
                                this.depreciationIteamsGridDatatable.Rows[j][this.depreciationIteamsGridDatatable.Depr1Column.ColumnName] = DBNull.Value;
                                this.depreciationIteamsGridDatatable.Rows[j][this.depreciationIteamsGridDatatable.Depr2Column.ColumnName] = DBNull.Value;
                                this.depreciationIteamsGridDatatable.Rows[j][this.depreciationIteamsGridDatatable.Depr3Column.ColumnName] = DBNull.Value;
                                this.depreciationIteamsGridDatatable.Rows[j][this.depreciationIteamsGridDatatable.Depr4Column.ColumnName] = DBNull.Value;
                                this.depreciationIteamsGridDatatable.Rows[j][this.depreciationIteamsGridDatatable.Depr5Column.ColumnName] = DBNull.Value;
                                this.depreciationIteamsGridDatatable.Rows[j][this.depreciationIteamsGridDatatable.Depr6Column.ColumnName] = DBNull.Value;

                                this.GridColumnReadOnly(true);
                            }
                        }

                        this.depreciationIteamsGridDatatable.AcceptChanges();

                        this.lifeCount = tempLifeCount;
                        this.LoadDeprecationItemsGrid();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.LifeTextBox.Text.Trim()))
                        {
                            this.LifeTextBox.Text = "0";
                        }

                        this.lifeCount = 0;
                    }
                }
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

        #endregion Events
    }
}
