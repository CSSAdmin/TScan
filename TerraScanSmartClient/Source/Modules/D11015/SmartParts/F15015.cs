//--------------------------------------------------------------------------------------------
// <copyright file="F15015.cs" company="Congruent">
//       Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the F15015 Statement Ownership.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date             Author                Description
// ----------       ---------            ---------------------------------------------------------
// 09/04/07         M.Vijayakumar           Created
// *********************************************************************************/

namespace D11015
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    /// <summary>
    /// F15015 Class file
    /// </summary>
    public partial class F15015 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the statementId(keyid)
        /// </summary>
        private int statementId;

        /// <summary>
        /// Used to store validKeyId
        /// </summary>
        private int validKeyId;

        /// <summary>
        /// Used to save saveRowIndex
        /// </summary>
        private int saveRowIndex;

        /// <summary>
        /// Used to store the rows count of All owners listing grid
        /// </summary>
        private int allOwnersGridCount;

        /// <summary>
        /// Used to store the rows count of stmt owners grid
        /// </summary>
        private int statementOwnersGridCount;

        /// <summary>
        /// Used to store the selectedstmtOwnerGridRowId
        /// </summary>
        private int selectedStatementOwnerGridRowId;

        /// <summary>
        /// Used to store selectedOwnerId
        /// </summary>
        private int selectedOwnerId;

        /// <summary>
        /// Used to store the statementOwnerGridClick
        /// </summary>
        private bool statementOwnerGridClick;

        /// <summary>
        /// Used to store tempstmtOwnerGridRowId
        /// </summary>
        private int tempStatementOwnerGridRowId;

        /// <summary>
        /// controller F15015
        /// </summary>
        private F15015Controller form15015Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flag LoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMaster Edit Permission
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store the parcelOwnershipData
        /// </summary>
        private F27006ParcelOwnershipData parcelOwnershipData = new F27006ParcelOwnershipData();

        /// <summary>
        /// Used to store statementOwnershipData which is instance of F15015StatementOwnershipData
        /// </summary>
        private F15015StatementOwnershipData statementOwnershipData = new F15015StatementOwnershipData();

        /// <summary>
        /// Used to store the listAllOwnersDetailDataTableDataTable insatnce
        /// </summary>
        private F15015StatementOwnershipData.ListAllOwnersDetailDataTableDataTable listAllOwnersDetailDataTable = new F15015StatementOwnershipData.ListAllOwnersDetailDataTableDataTable ();

        /// <summary>
        /// Used to store the liststatementOwnershipDataTable
        /// </summary>
        private F15015StatementOwnershipData.ListStatementOwnershipDataTableDataTable liststatementOwnershipDataTable = new F15015StatementOwnershipData.ListStatementOwnershipDataTableDataTable();

        /// <summary>
        /// owner Percent
        /// </summary>
        private decimal ownerPercent;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Used to store the currentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// Used to store isgridRowChange
        /// </summary>
        private bool isgridRowChange;

        /// <summary>
        /// Used to store iseditOn
        /// </summary>
        private bool iseditOn;

        /// <summary>
        /// Used to store avoidParcelGridRowEnter
        /// </summary>
        private bool avoidParcelGridRowEnter;

        /// <summary>
        /// get MOwnerType Data
        /// </summary>
        private F15015StatementOwnershipData getMOwnerTypeData = new F15015StatementOwnershipData();

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15015"/> class.
        /// </summary>
        public F15015()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F15015"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15015(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.statementId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.AssociatedOwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociatedOwnersPictureBox.Height, this.AssociatedOwnersPictureBox.Width, "Associated Owners", 28, 81, 128); ////todo remove hard code value
            this.AllOwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AllOwnersPictureBox.Height, this.AllOwnersPictureBox.Width, "All Owners", 174, 150, 94);   ////todo remove hard code value                     
        }

        #endregion Constructor

        #region Event Publication

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

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form150015 control.
        /// </summary>
        /// <value>The form150015 control.</value>
        [CreateNew]
        public F15015Controller Form150015Control
        {
            get { return this.form15015Control as F15015Controller; }
            set { this.form15015Control = value; }
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
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.OrderPanel.Enabled = false;
                this.ClearAllAssociatedOwnerPart();
                this.ClearAllOwnersPart();
                this.ClearAllOwnersGrid();
                this.LockControls(false);
                this.OrderPanel.Enabled = true;
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
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                ////For Form funcatonality this is commented
                if (this.validKeyId > 0)
                {
                    this.LockControls(true);
                }
                else
                {
                    this.LockControls(false);
                }

                this.LoadStatementOwnership();
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
                    if (this.PermissionFiled.editPermission)
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                    }
                }
                else
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }
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
                    if (this.PermissionFiled.editPermission)
                    {
                        this.SaveStatementOwnership();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    ////For Form funcatonality this is commented
                    if (this.validKeyId > 0)
                    {
                        this.LockControls(true);
                    }
                    else
                    {
                        this.LockControls(false);
                    }

                    this.ControlLock(false);
                    this.LoadStatementOwnership();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
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

                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                    ////to check for invalid key id 
                    if (this.statementId != eventArgs.Data.KeyId)
                    {
                        this.statementId = eventArgs.Data.KeyId;
                        this.statementOwnershipData = this.form15015Control.WorkItem.F15015_ListStatementOwnership(this.statementId);
                        int.TryParse(this.statementOwnershipData.ListOwnerValidID.Rows[0][this.statementOwnershipData.ListOwnerValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
                    }

                    ////For Form funcatonality this is commented
                    if (this.validKeyId > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.LockControls(true);
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
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.statementId = eventArgs.Data.SelectedKeyId;
                    this.LoadStatementOwnership();
                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
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
        /// Gets the associated owners part.
        /// </summary>
        /// <param name="currentRowNo">The current row no.</param>
        private void GetAssociatedOwnersPart(int currentRowNo)
        {
            if ((this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].Value != null) && (!string.IsNullOrEmpty(this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].Value.ToString())))
            {
                this.isgridRowChange = true;
                this.FirstNameTextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.FirstNameColumn.ColumnName].Value.ToString();
                this.LastNameTextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.LastNameColumn.ColumnName].Value.ToString();
                this.OrderTextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerOrderColumn.ColumnName].Value.ToString();
                SetComboboxValue(this.ReceiveStmtComboBox, this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.IsBilledColumn.ColumnName].Value.ToString());
                this.Address1TextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.Address1Column.ColumnName].Value.ToString();
                this.Address2TextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.Address2Column.ColumnName].Value.ToString();
                SetComboboxValue(this.ProratedStmtComboBox, this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.IsProRatedColumn.ColumnName].Value.ToString());
                this.OwnerPercentTextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerPercentColumn.ColumnName].Value.ToString();

                this.OwnerCodeTextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerCodeColumn.ColumnName].Value.ToString();
                this.CityTextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.CityColumn.ColumnName].Value.ToString();
                this.StateTextBox.Text = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.StateColumn.ColumnName].Value.ToString();
                this.OwnerTypeComboBox.SelectedValue = this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.MOwnerTypeIDColumn.ColumnName].Value.ToString();

                int.TryParse(this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].Value.ToString(), out this.selectedOwnerId);

                decimal.TryParse(this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerPercentColumn.ColumnName].Value.ToString(), out this.ownerPercent);

                int.TryParse(this.StatementOwnershipDataGridView.Rows[currentRowNo].Cells[this.liststatementOwnershipDataTable.OwnerOrderColumn.ColumnName].Value.ToString(), out this.selectedStatementOwnerGridRowId);

                this.currentRowIndex = currentRowNo;

                this.isgridRowChange = false;
            }
        }

        /// <summary>
        /// Clears the associated owners Header part.
        /// </summary>
        private void ClearAssociatedOwnersPart()
        {
            this.FirstNameTextBox.Text = string.Empty;
            this.LastNameTextBox.Text = string.Empty;
            this.OrderTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.OwnerPercentTextBox.Text = string.Empty;
            this.OwnerCodeTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;

            ////Todo combo box control
            this.ReceiveStmtComboBox.Text = string.Empty;
            this.ProratedStmtComboBox.Text = string.Empty;
            this.OwnerTypeComboBox.Text = string.Empty;
        }

        /// <summary>
        /// Clears all associated owner part.
        /// </summary>
        private void ClearAllAssociatedOwnerPart()
        {
            this.liststatementOwnershipDataTable.Clear();
            this.StatementOwnershipDataGridView.DataSource = this.liststatementOwnershipDataTable.DefaultView;
            this.StatementOwnershipDataGridView.Rows[0].Selected = false;
            ////to set the rowindex to 0 when the grid is disabled
            this.currentRowIndex = 0;
            this.StatementOwnershipDataGridView.Enabled = false;
            this.StatementOwnershipDataGridView.Visible = true;
            ////To clear the Associated Owner Grid header part
            this.ClearAssociatedOwnersPart();
            ////To fill the data in the foorter part of the Associated stmt grid
            this.OrderCountlabel.Text = string.Empty;
            this.TtlOwnPercentTextBox.Text = string.Empty;
            this.PercentLabel.Text = string.Empty;
            this.ReceiveStmtComboBox.SelectedIndex = -1;
            this.ProratedStmtComboBox.SelectedIndex = -1;
            this.OwnerTypeComboBox.SelectedIndex = -1;
            this.AssctOwnerHeaderPanel.Enabled = false;
            this.MoveDownButton.Enabled = false;
        }

        /// <summary>
        /// To Laod Stmt Ownership Grid
        /// </summary>
        private void LoadStatementOwnershipDataGrid()
        {
            this.liststatementOwnershipDataTable.Clear();
            this.statementOwnershipData = this.form15015Control.WorkItem.F15015_ListStatementOwnership(this.statementId);
            this.liststatementOwnershipDataTable = this.statementOwnershipData.ListStatementOwnershipDataTable;
            int.TryParse(this.statementOwnershipData.ListOwnerValidID.Rows[0][this.statementOwnershipData.ListOwnerValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
            this.PopulateStatementOwnershipDataGrid(0);

            if (this.liststatementOwnershipDataTable.Rows.Count >= 0)
            {
                this.OrderTextBox.Focus();
            }
        }

        /// <summary>
        ///  To Populates the Stmt ownership data grid with Data form database.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulateStatementOwnershipDataGrid(int rowIndex)
        {
            this.StatementOwnershipDataGridView.DataSource = this.liststatementOwnershipDataTable.DefaultView;
            this.statementOwnersGridCount = this.StatementOwnershipDataGridView.OriginalRowCount;
            if (this.statementOwnersGridCount > 0)
            {
                this.StatementOwnershipDataGridView.Focus();
                this.StatementOwnershipDataGridView.Enabled = true;
                ////to fill the Associated Owner Grid header part                    
                this.GetAssociatedOwnersPart(rowIndex);
                this.currentRowIndex = rowIndex;

                ////To fill the data in the foorter part of the Associated stmt grid
                this.OrderCountlabel.Text = this.statementOwnersGridCount.ToString();
                this.AssctOwnerHeaderPanel.Enabled = true;
                this.MoveDownButton.Enabled = true;
                TerraScanCommon.SetDataGridViewPosition(this.StatementOwnershipDataGridView, rowIndex);
            }
            else
            {
                this.ClearAllAssociatedOwnerPart();
            }

            if (this.StatementOwnershipDataGridView.OriginalRowCount > this.StatementOwnershipDataGridView.NumRowsVisible)
            {
                this.StatementOwnershipGridVerticalScroll.Visible = false;
            }
            else
            {
                this.StatementOwnershipGridVerticalScroll.Visible = true;
            }
        }

        /// <summary>
        /// To laod the Entire the Stmt ownership form
        /// </summary>
        private void LoadStatementOwnership()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.MoveDownButton.Enabled = true;

            this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.NameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.OwnerPercentColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.IsBilledColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.IsProRatedColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.OwnerOrderColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;

            ////to set the value for the combo boxs
            this.SetGeneralComboBox();

            this.ClearAllOwnersPart();
            this.ClearAllOwnersGrid();
            this.LoadStatementOwnershipDataGrid();
            this.DisableButtons();

            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// To Custimize AssociatedOwnersGrid
        /// </summary>
        private void CustimizeAssociatedOwnersGrid()
        {
            this.StatementOwnershipDataGridView.AutoGenerateColumns = false;
            this.MOwnerID.DataPropertyName = this.liststatementOwnershipDataTable.MOwnerIDColumn.ColumnName; ////"MOwnerID";
            this.Name.DataPropertyName = this.liststatementOwnershipDataTable.NameColumn.ColumnName;
            this.FirstName.DataPropertyName = this.liststatementOwnershipDataTable.FirstNameColumn.ColumnName;
            this.LastName.DataPropertyName = this.liststatementOwnershipDataTable.LastNameColumn.ColumnName;
            this.Address1.DataPropertyName = this.liststatementOwnershipDataTable.Address1Column.ColumnName;
            this.Address2.DataPropertyName = this.liststatementOwnershipDataTable.Address2Column.ColumnName;
            this.City.DataPropertyName = this.liststatementOwnershipDataTable.CityColumn.ColumnName;
            this.OwnerPercent.DataPropertyName = this.liststatementOwnershipDataTable.OwnerPercentColumn.ColumnName;
            this.IsBilled.DataPropertyName = this.liststatementOwnershipDataTable.IsBilledColumn.ColumnName;
            this.IsProRated.DataPropertyName = this.liststatementOwnershipDataTable.IsProRatedColumn.ColumnName;
            this.OwnerOrder.DataPropertyName = this.liststatementOwnershipDataTable.OwnerOrderColumn.ColumnName;
            this.OwnerID.DataPropertyName = this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName;
            this.OwnerCode.DataPropertyName = this.liststatementOwnershipDataTable.OwnerCodeColumn.ColumnName;
            this.State.DataPropertyName = this.liststatementOwnershipDataTable.StateColumn.ColumnName;
            this.MOwnerTypeID.DataPropertyName = this.liststatementOwnershipDataTable.MOwnerTypeIDColumn.ColumnName;
            this.MOwnerType.DataPropertyName = this.liststatementOwnershipDataTable.MOwnerTypeColumn.ColumnName;

            this.MOwnerID.DisplayIndex = 0;
            this.Name.DisplayIndex = 1;
            this.FirstName.DisplayIndex = 2;
            this.LastName.DisplayIndex = 3;
            this.Address1.DisplayIndex = 4;
            this.Address2.DisplayIndex = 5;
            this.City.DisplayIndex = 6;
            this.OwnerPercent.DisplayIndex = 7;
            this.IsBilled.DisplayIndex = 8;
            this.IsProRated.DisplayIndex = 9;
            this.OwnerOrder.DisplayIndex = 10;
            this.OwnerID.DisplayIndex = 11;
            this.State.DisplayIndex = 12;
            this.OwnerCode.DisplayIndex = 13;
            this.MOwnerTypeID.DisplayIndex = 14;
            this.MOwnerType.DisplayIndex = 15;

            this.StatementOwnershipDataGridView.PrimaryKeyColumnName = this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName;
        }

        /// <summary>
        /// Sets the general combo box.
        /// </summary>
        private void SetGeneralComboBox()
        {
            this.ReceiveStmtComboBox.Items.Clear();
            this.ProratedStmtComboBox.Items.Clear();
            this.ReceiveStmtComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.ReceiveStmtComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));
            this.ProratedStmtComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.ProratedStmtComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));
        }

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            decimal saveTtlOwnPercent;
            int errorStatusId = -99;
            string tocheckXmlString;

            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            this.AssginDataToStatementGrid();
            decimal.TryParse(this.liststatementOwnershipDataTable.Compute("SUM (OwnerPercent)", "OwnerID > 0").ToString(), out saveTtlOwnPercent);

            if (saveTtlOwnPercent == 100)
            {
                tocheckXmlString = TerraScanCommon.GetXmlString(this.liststatementOwnershipDataTable);
                errorStatusId = this.form15015Control.WorkItem.F27006_CheckOwnershipDetails(tocheckXmlString);

                switch (errorStatusId)
                {
                    case -99:
                        sliceValidationFields.DisableNewMethod = true;
                        MessageBox.Show(SharedFunctions.GetResourceString("OwnerShipValidation"), SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case -100:
                        sliceValidationFields.RequiredFieldMissing = false;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("OwnerShipRequiredFieldValidation");
                        break;
                    case -101:
                        sliceValidationFields.DisableNewMethod = true;
                        MessageBox.Show(SharedFunctions.GetResourceString("OwnerShipOrderValidation"), SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                sliceValidationFields.DisableNewMethod = true;
                MessageBox.Show(SharedFunctions.GetResourceString("F15015TotalOwnerShipValidation"), SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// To save StatementOwnership
        /// </summary>
        private void SaveStatementOwnership()
        {
            this.liststatementOwnershipDataTable.AcceptChanges();
            string statementOwnership = TerraScanCommon.GetXmlString(this.liststatementOwnershipDataTable);
            this.form15015Control.WorkItem.F15015_SaveStatementOwnership(this.statementId, statementOwnership, TerraScanCommon.UserId);
        }

        /// <summary>
        /// To get stmt Ownership datagrid row index
        /// </summary>
        /// <param name="searchOwnerId">Current Unique order Id to search</param>
        /// <returns>Integer value of the row index </returns>
        private int GetRowIndex(string searchOwnerId)
        {
            try
            {
                this.tempStatementOwnerGridRowId = -1;

                for (int i = 0; i < this.StatementOwnershipDataGridView.Rows.Count; i++)
                {
                    if ((this.StatementOwnershipDataGridView.Rows[i].Cells[this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].Value != null) && (!string.IsNullOrEmpty(this.StatementOwnershipDataGridView.Rows[i].Cells[this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].Value.ToString())))
                    {
                        if (this.StatementOwnershipDataGridView.Rows[i].Cells[this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].Value.ToString() == searchOwnerId)
                        {
                            return this.tempStatementOwnerGridRowId = i;
                        }
                    }
                }

                return this.tempStatementOwnerGridRowId;
            }
            catch (Exception)
            {
                return this.tempStatementOwnerGridRowId;
            }
        }

        /// <summary>
        /// To get row particular index of the liststatementOwnershipDataTable
        /// </summary>
        /// <param name="currentOwnerId">Current Unique order Id to search</param>
        /// <returns>Integer value of the row index </returns>
        private int GetListStatementOwnershipDatatableRowIndex(string currentOwnerId)
        {
            try
            {
                this.saveRowIndex = -1;
                this.liststatementOwnershipDataTable.AcceptChanges();
                for (int i = 0; i < this.liststatementOwnershipDataTable.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.liststatementOwnershipDataTable.Rows[i][this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].ToString()))
                    {
                        if (this.liststatementOwnershipDataTable.Rows[i][this.liststatementOwnershipDataTable.OwnerIDColumn.ColumnName].ToString() == currentOwnerId)
                        {
                            return this.saveRowIndex = i;
                        }
                    }
                }

                return this.saveRowIndex;
            }
            catch (Exception)
            {
                return this.saveRowIndex;
            }
        }

        /// <summary>
        /// Used to assign the modified value to the Grid Datatable
        /// </summary>
        private void AssginDataToStatementGrid()
        {
            if (this.selectedOwnerId > 0 && this.iseditOn)
            {
                this.GetListStatementOwnershipDatatableRowIndex(this.selectedOwnerId.ToString());

                if (this.saveRowIndex >= 0)
                {
                    this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.OwnerPercentColumn] = this.OwnerPercentTextBox.Text.Replace("%", "").Trim();

                    if (!string.IsNullOrEmpty(this.OrderTextBox.Text.Trim()))
                    {
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.OwnerOrderColumn] = this.OrderTextBox.Text.Trim();
                    }
                    else
                    {
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.OwnerOrderColumn] = DBNull.Value;
                    }

                    if (String.Equals(this.ReceiveStmtComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                    {
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.IsBilledColumn] = 1;
                    }
                    else
                    {
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.IsBilledColumn] = 0;
                    }

                    if (String.Equals(this.ProratedStmtComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                    {
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.IsProRatedColumn] = 1;
                    }
                    else
                    {
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.IsProRatedColumn] = 0;
                    }

                    if (this.OwnerTypeComboBox.SelectedValue != null)
                    {
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.MOwnerTypeIDColumn] = this.OwnerTypeComboBox.SelectedValue;
                        this.liststatementOwnershipDataTable.Rows[this.saveRowIndex][this.liststatementOwnershipDataTable.MOwnerTypeColumn] = this.OwnerTypeComboBox.Text;
                    }

                    this.liststatementOwnershipDataTable.AcceptChanges();

                    this.iseditOn = false;
                }
            }
        }

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.FirstNameTextBox.MaxLength = this.liststatementOwnershipDataTable.FirstNameColumn.MaxLength;
            this.LastNameTextBox.MaxLength = this.liststatementOwnershipDataTable.LastNameColumn.MaxLength;
            this.ReceiveStmtComboBox.MaxLength = this.liststatementOwnershipDataTable.IsBilledColumn.MaxLength;
            this.Address1TextBox.MaxLength = this.liststatementOwnershipDataTable.Address1Column.MaxLength;
            this.Address2TextBox.MaxLength = this.liststatementOwnershipDataTable.Address2Column.MaxLength;
            this.ProratedStmtComboBox.MaxLength = this.liststatementOwnershipDataTable.IsProRatedColumn.MaxLength;
            this.OwnerCodeTextBox.MaxLength = this.liststatementOwnershipDataTable.OwnerCodeColumn.MaxLength;
            this.CityTextBox.MaxLength = this.liststatementOwnershipDataTable.CityColumn.MaxLength;
            this.StateTextBox.MaxLength = this.liststatementOwnershipDataTable.StateColumn.MaxLength;

            this.SearchFirstNameTextBox.MaxLength = this.listAllOwnersDetailDataTable.FirstNameColumn.MaxLength;
            this.SearchLastNameTextBox.MaxLength = this.listAllOwnersDetailDataTable.LastNameColumn.MaxLength;
            this.SearchAddress1TextBox.MaxLength = this.listAllOwnersDetailDataTable.Address1Column.MaxLength;
            this.SearchAddress2TextBox.MaxLength = this.listAllOwnersDetailDataTable.Address2Column.MaxLength;
            this.SearchCityTextBox.MaxLength = this.listAllOwnersDetailDataTable.CityColumn.MaxLength;
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.AssociatedOwnerPanel.Enabled = lockControl;
            this.AllOwnersPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.OrderTextBox.LockKeyPress = controlLook;
            this.ReceiveStmtComboBox.Enabled = !controlLook;
            this.ProratedStmtComboBox.Enabled = !controlLook;
            this.OwnerPercentTextBox.LockKeyPress = controlLook;
            this.OwnerTypeComboBox.Enabled = !controlLook;

            this.SearchFirstNameTextBox.LockKeyPress = controlLook;
            this.SearchLastNameTextBox.LockKeyPress = controlLook;
            this.SearchAddress1TextBox.LockKeyPress = controlLook;
            this.SearchAddress2TextBox.LockKeyPress = controlLook;
            this.SearchCityTextBox.LockKeyPress = controlLook;

            ////to enable are disable the move up and move down button based on permission
            this.MoveUpDownPanel.Enabled = !controlLook;
        }

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

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess && !this.statementOwnerGridClick) 
            {
                this.EditEnabled();
                this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.NameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.OwnerPercentColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.IsBilledColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.IsProRatedColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.StatementOwnershipDataGridView.Columns[this.liststatementOwnershipDataTable.OwnerOrderColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.StatementOwnershipDataGridView.Refresh();
            }
        }

        /// <summary>
        /// To clear AllOwners Header Part
        /// </summary>
        private void ClearAllOwnersPart()
        {
            this.SearchFirstNameTextBox.Text = string.Empty;
            this.SearchLastNameTextBox.Text = string.Empty;
            this.SearchAddress1TextBox.Text = string.Empty;
            this.SearchAddress2TextBox.Text = string.Empty;
            this.SearchCityTextBox.Text = string.Empty;
        }

        /// <summary>
        /// To clear AllOwnersGrid
        /// </summary>
        private void ClearAllOwnersGrid()
        {
            this.listAllOwnersDetailDataTable.Clear();
            this.AllOwnersdeatilsDataGridView.ClearSorting();
            this.AllOwnersdeatilsDataGridView.DataSource = this.listAllOwnersDetailDataTable.DefaultView;
            this.allOwnersGridCount = this.AllOwnersdeatilsDataGridView.OriginalRowCount;
            this.AllOwnersdeatilsDataGridView.Rows[0].Selected = false;
            this.AllOwnersdeatilsDataGridView.Enabled = false;
            this.AllOwnersDetailsGridVerticalScroll.Visible = true;
        }

        /// <summary>
        /// To Load All Owners Grid
        /// </summary>
        private void LoadAllOwnersGrid()
        {
            this.listAllOwnersDetailDataTable.Clear();
            this.statementOwnershipData = this.form15015Control.WorkItem.F15015_ListALLOwnerDetails(this.SearchFirstNameTextBox.Text.Trim(), this.SearchLastNameTextBox.Text.Trim(), this.SearchAddress1TextBox.Text.Trim(), this.SearchAddress2TextBox.Text.Trim(), this.SearchCityTextBox.Text.Trim());               
            //this.statementOwnershipData  = this.form15015Control.WorkItem.F15015_ListALLOwnerDetails(this.SearchFirstNameTextBox.Text.Trim(), this.SearchLastNameTextBox.Text.Trim(), this.SearchAddress1TextBox.Text.Trim(), this.SearchAddress2TextBox.Text.Trim(), this.SearchCityTextBox.Text.Trim());
            this.listAllOwnersDetailDataTable = this.statementOwnershipData.ListAllOwnersDetailDataTable;
            this.allOwnersGridCount = this.listAllOwnersDetailDataTable.Rows.Count;

            if (this.allOwnersGridCount > 0)
            {
                this.AllOwnersdeatilsDataGridView.DataSource = this.listAllOwnersDetailDataTable.DefaultView;
                this.AllOwnersdeatilsDataGridView.Rows[0].Selected = true;
                this.AllOwnersdeatilsDataGridView.Enabled = true;
                this.MoveUpButton.Enabled = true;
            }
            else
            {
                this.ClearAllOwnersGrid();
                this.MoveUpButton.Enabled = false;
            }

            if (this.listAllOwnersDetailDataTable.Rows.Count > this.AllOwnersdeatilsDataGridView.NumRowsVisible)
            {
                this.AllOwnersDetailsGridVerticalScroll.Visible = false;
            }
            else
            {
                this.AllOwnersDetailsGridVerticalScroll.Visible = true;
            }
        }

        /// <summary>
        /// To Custimize AllOwnersdeatils Grid
        /// </summary>
        private void CustimizeAllOwnersdeatilsGrid()
        {
            try
            {
                this.AllOwnersdeatilsDataGridView.AutoGenerateColumns = false;

                this.ALLOwnerMOwnerID.DataPropertyName = this.listAllOwnersDetailDataTable.MOwnerIDColumn.ColumnName;
                this.AllOwnerName.DataPropertyName = this.listAllOwnersDetailDataTable.NameColumn.ColumnName;
                this.AllOwnerFirstName.DataPropertyName = this.listAllOwnersDetailDataTable.FirstNameColumn.ColumnName;
                this.AllOwnerLastName.DataPropertyName = this.listAllOwnersDetailDataTable.LastNameColumn.ColumnName;
                this.AllOwnerAddress1.DataPropertyName = this.listAllOwnersDetailDataTable.Address1Column.ColumnName;
                this.AllOwnerAddress2.DataPropertyName = this.listAllOwnersDetailDataTable.Address2Column.ColumnName;
                this.AllOwnerCity.DataPropertyName = this.listAllOwnersDetailDataTable.CityColumn.ColumnName;
                this.AllownerPercent.DataPropertyName = this.listAllOwnersDetailDataTable.OwnerPercentColumn.ColumnName;
                this.AllOwnerIsBilled.DataPropertyName = this.listAllOwnersDetailDataTable.IsBilledColumn.ColumnName;
                this.AllOwnerIsProRated.DataPropertyName = this.listAllOwnersDetailDataTable.IsProratedColumn.ColumnName;
                this.AllOwnerOrder.DataPropertyName = this.listAllOwnersDetailDataTable.OwnerOrderColumn.ColumnName;
                this.AllOwnerID.DataPropertyName = this.listAllOwnersDetailDataTable.OwnerIDColumn.ColumnName;
                this.AllOwnerState.DataPropertyName = this.listAllOwnersDetailDataTable.StateColumn.ColumnName;
                this.AllOwnerCode.DataPropertyName = this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName;
                this.AllOwnerMOwnerTypeId.DataPropertyName = this.listAllOwnersDetailDataTable.MOwnerTypeIDColumn.ColumnName;
                this.AllOwnerMOwnerType.DataPropertyName = this.listAllOwnersDetailDataTable.MOwnerTypeColumn.ColumnName;

                this.ALLOwnerMOwnerID.DisplayIndex = 0;
                this.AllOwnerName.DisplayIndex = 1;
                this.AllOwnerFirstName.DisplayIndex = 2;
                this.AllOwnerLastName.DisplayIndex = 3;
                this.AllOwnerAddress1.DisplayIndex = 4;
                this.AllOwnerAddress2.DisplayIndex = 5;
                this.AllOwnerCity.DisplayIndex = 6;
                this.AllownerPercent.DisplayIndex = 7;
                this.AllOwnerIsBilled.DisplayIndex = 8;
                this.AllOwnerIsProRated.DisplayIndex = 9;
                this.AllOwnerOrder.DisplayIndex = 10;
                this.AllOwnerID.DisplayIndex = 11;
                this.AllOwnerState.DisplayIndex = 12;
                this.AllOwnerCode.DisplayIndex = 13;
                this.AllOwnerMOwnerTypeId.DisplayIndex = 14;
                this.AllOwnerMOwnerType.DisplayIndex = 15;

                this.AllOwnersdeatilsDataGridView.PrimaryKeyColumnName = this.listAllOwnersDetailDataTable.OwnerIDColumn.ColumnName;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the search button.
        /// </summary>
        private void EnableSearchButton()
        {
            if ((!string.IsNullOrEmpty(this.SearchFirstNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchLastNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchAddress1TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchAddress2TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchCityTextBox.Text.Trim())))
            {
                this.SearchButton.Enabled = true;
                this.ClearButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;

                if (this.allOwnersGridCount <= 0)
                {
                    this.ClearButton.Enabled = false;
                    this.MoveUpButton.Enabled = false;
                }
                else
                {
                    this.ClearButton.Enabled = true;
                    this.MoveUpButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// To disable the accept,search, and clear buttons
        /// </summary>        
        private void DisableButtons()
        {
            this.SearchButton.Enabled = false;
            this.ClearButton.Enabled = false;
            this.MoveUpButton.Enabled = false;
        }

        /// <summary>
        /// Associates the Stmt ownership.
        /// </summary>
        private void AssociateStatementOwnership()
        {
            int allOwnerGirdRowIndexValue = -1;

            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                this.AssginDataToStatementGrid();
            }

            this.AllOwnersdeatilsDataGridView.Focus();
            allOwnerGirdRowIndexValue = AllOwnersdeatilsDataGridView.CurrentRow.Index;
            if (allOwnerGirdRowIndexValue >= 0)
            {
                F15015StatementOwnershipData.ListStatementOwnershipDataTableRow tempListAllOwnersDetailDataTableRow = this.liststatementOwnershipDataTable.NewListStatementOwnershipDataTableRow();

                int tempOwnerIdValue = -1;
                decimal tempPercent;

                if ((this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerID"].Value != null) && (!string.IsNullOrEmpty(this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerID"].Value.ToString())))
                {
                    int.TryParse(this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerID"].Value.ToString(), out tempOwnerIdValue);
                    decimal.TryParse(this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllownerPercent"].Value.ToString(), out tempPercent);

                    if (tempOwnerIdValue > 0)
                    {
                        if (this.GetRowIndex(this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerID"].Value.ToString()) >= 0)
                        {
                            ////to fill the Associated Owner Grid header part                    
                            this.GetAssociatedOwnersPart(this.tempStatementOwnerGridRowId);
                            TerraScanCommon.SetDataGridViewPosition(this.StatementOwnershipDataGridView, this.tempStatementOwnerGridRowId);
                            int.TryParse(this.StatementOwnershipDataGridView.Rows[this.tempStatementOwnerGridRowId].Cells[this.liststatementOwnershipDataTable.OwnerOrderColumn.ColumnName].Value.ToString(), out this.selectedStatementOwnerGridRowId);
                            this.ToEnableEditButtonInMasterForm();
                        }
                        else
                        {
                            tempListAllOwnersDetailDataTableRow.MOwnerID = 0;
                            tempListAllOwnersDetailDataTableRow.OwnerID = tempOwnerIdValue;
                            tempListAllOwnersDetailDataTableRow.Name = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerName"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.FirstName = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerFirstName"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.LastName = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerLastName"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.Address1 = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerAddress1"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.Address2 = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerAddress2"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.City = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerCity"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.State = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells[this.AllOwnerState.Name].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.OwnerCode = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells[this.AllOwnerCode.Name].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.MOwnerType = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells[this.AllOwnerMOwnerType.Name].Value.ToString();
                            byte ownerTypeId;
                            byte.TryParse(this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells[this.AllOwnerMOwnerTypeId.Name].Value.ToString(), out ownerTypeId);
                            tempListAllOwnersDetailDataTableRow.MOwnerTypeID = ownerTypeId;
                            if (this.StatementOwnershipDataGridView.OriginalRowCount == 0)
                            {
                                tempListAllOwnersDetailDataTableRow.OwnerPercent = 100;
                            }
                            else
                            {
                                tempListAllOwnersDetailDataTableRow.OwnerPercent = 0;
                            }

                            ////default value is set for isbilled
                            tempListAllOwnersDetailDataTableRow.IsBilled = true;
                            tempListAllOwnersDetailDataTableRow.IsProRated = false;

                            int currentTempOwnerOrderValue;
                            int.TryParse(this.liststatementOwnershipDataTable.Compute("MAX (OwnerOrder)", "OwnerID > 0").ToString(), out currentTempOwnerOrderValue);

                            if (currentTempOwnerOrderValue > 0)
                            {
                                if (currentTempOwnerOrderValue >= 255)
                                {
                                    tempListAllOwnersDetailDataTableRow.OwnerOrder = 1;
                                }
                                else
                                {
                                    tempListAllOwnersDetailDataTableRow.OwnerOrder = currentTempOwnerOrderValue + 1;
                                }
                            }
                            else
                            {
                                tempListAllOwnersDetailDataTableRow.OwnerOrder = this.StatementOwnershipDataGridView.OriginalRowCount + 1;
                            }

                            if (this.StatementOwnershipDataGridView.OriginalRowCount < this.StatementOwnershipDataGridView.NumRowsVisible)
                            {
                                this.liststatementOwnershipDataTable.Rows.RemoveAt(this.StatementOwnershipDataGridView.OriginalRowCount);
                                this.avoidParcelGridRowEnter = true;
                                this.liststatementOwnershipDataTable.Rows.InsertAt(tempListAllOwnersDetailDataTableRow, this.StatementOwnershipDataGridView.OriginalRowCount);
                                this.avoidParcelGridRowEnter = false;
                            }
                            else
                            {
                                this.liststatementOwnershipDataTable.Rows.InsertAt(tempListAllOwnersDetailDataTableRow, this.StatementOwnershipDataGridView.Rows.Count);
                            }

                            this.liststatementOwnershipDataTable.AcceptChanges();

                            ////this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                            this.PopulateStatementOwnershipDataGrid(this.statementOwnersGridCount);
                            ////To disable the Associated owner header Part 
                            ////this.AssctOwnerHeaderPanel.Enabled = false;
                            this.ToEnableEditButtonInMasterForm();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the default selection.
        /// </summary>
        private void RemoveDefaultSelection()
        {
            if (this.AllOwnersdeatilsDataGridView.OriginalRowCount == 0)
            {
                this.AllOwnersdeatilsDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.AllOwnersdeatilsDataGridView.RemoveDefaultSelection = false;
            }

            if (this.StatementOwnershipDataGridView.OriginalRowCount == 0)
            {
                this.StatementOwnershipDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.StatementOwnershipDataGridView.RemoveDefaultSelection = false;
            }
        }

        /// <summary>
        /// Shows the type of the M owner.
        /// </summary>
        private void ShowMOwnerType()
        {
            // DB call for get Owner type
            this.getMOwnerTypeData = this.form15015Control.WorkItem.F15015_ListMOwnerType();
            if (this.getMOwnerTypeData.listMOwnerTypeDataTable.Rows.Count > 0)
            {
                // Bind combo
                this.OwnerTypeComboBox.DataSource = this.getMOwnerTypeData.listMOwnerTypeDataTable;
                this.OwnerTypeComboBox.DisplayMember = this.getMOwnerTypeData.listMOwnerTypeDataTable.MOwnerTypeColumn.ColumnName;
                this.OwnerTypeComboBox.ValueMember = this.getMOwnerTypeData.listMOwnerTypeDataTable.MOwnerTypeIDColumn.ColumnName;
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "Text Changed Events In Text Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();

                if (!this.isgridRowChange && this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    this.iseditOn = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the StatementOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatementOwnershipDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.statementOwnerGridClick = true;

                if (e.RowIndex >= 0)
                {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                    {
                        if (!this.avoidParcelGridRowEnter)
                        {
                            this.currentRowIndex = e.RowIndex;
                        }
                    }
                    else
                    {
                        this.GetAssociatedOwnersPart(e.RowIndex);
                    }
                }

                this.statementOwnerGridClick = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the StatementOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatementOwnershipDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.statementOwnerGridClick = true;

                if (e.RowIndex >= 0)
                {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.AssginDataToStatementGrid();
                        this.GetAssociatedOwnersPart(e.RowIndex);
                        TerraScanCommon.SetDataGridViewPosition(this.StatementOwnershipDataGridView, e.RowIndex);
                        this.currentRowIndex = e.RowIndex;
                    }
                    else
                    {
                        this.GetAssociatedOwnersPart(e.RowIndex);
                        this.currentRowIndex = e.RowIndex;
                    }
                }

                this.statementOwnerGridClick = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the StatementOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void StatementOwnershipDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.statementOwnerGridClick = true;
                DataGridView currentGrid = (DataGridView)sender;
                int stmtRowIndex = currentGrid.CurrentCell.RowIndex;

                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                                {
                                    this.AssginDataToStatementGrid();
                                    this.GetAssociatedOwnersPart((stmtRowIndex + 1));
                                    TerraScanCommon.SetDataGridViewPosition(this.StatementOwnershipDataGridView, stmtRowIndex);
                                    this.currentRowIndex = stmtRowIndex;
                                }

                                break;
                            }

                        case Keys.Up:
                            {
                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                                {
                                    this.AssginDataToStatementGrid();
                                    this.GetAssociatedOwnersPart((stmtRowIndex - 1));
                                    TerraScanCommon.SetDataGridViewPosition(this.StatementOwnershipDataGridView, stmtRowIndex);
                                    this.currentRowIndex = stmtRowIndex;
                                }

                                break;
                            }
                    }
                }

                this.statementOwnerGridClick = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the StatementOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatementOwnershipDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.formMasterPermissionEdit && this.PermissionFiled.editPermission && !string.IsNullOrEmpty(this.StatementOwnershipDataGridView.Rows[e.RowIndex].Cells[this.OwnerID.Name].Value.ToString()))
                {
                    this.StatementOwnershipDataGridView.Focus();
                    if (StatementOwnershipDataGridView.CurrentRow.Index >= 0)
                    {
                        this.liststatementOwnershipDataTable.Rows.RemoveAt(StatementOwnershipDataGridView.CurrentRow.Index);
                        this.liststatementOwnershipDataTable.AcceptChanges();
                        this.PopulateStatementOwnershipDataGrid(0);
                        ////this.AssctOwnerHeaderPanel.Enabled = false;
                        ////to enable the save and cancel button in the master form
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the StatementOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void StatementOwnershipDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.StatementOwnershipDataGridView.OriginalRowCount > 0)
                {
                    this.StatementOwnershipDataGridView.Rows[this.currentRowIndex].Selected = true;
                    this.StatementOwnershipDataGridView.CurrentCell = this.StatementOwnershipDataGridView[1, this.currentRowIndex];
                    ////this.OrderTextBox.Focus(); 

                    object totalOwnership;
                    totalOwnership = this.liststatementOwnershipDataTable.Compute("SUM (OwnerPercent)", "OwnerID > 0");
                    this.TtlOwnPercentTextBox.Text = totalOwnership.ToString();
                    this.PercentLabel.Text = this.TtlOwnPercentTextBox.Text;
                }
                else
                {
                    ////To fill the data in the foorter part of the Associated stmt grid   
                    ////this.OrderTextBox.Focus();
                    this.TtlOwnPercentTextBox.Text = string.Empty;
                    this.PercentLabel.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.LoadAllOwnersGrid();
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

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearAllOwnersPart();
                this.ClearAllOwnersGrid();
                this.DisableButtons();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MoveDownButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.OrderTextBox.Text.Trim()))
                {
                    this.StatementOwnershipDataGridView.Focus();
                    if (StatementOwnershipDataGridView.CurrentRow.Index >= 0)
                    {
                        this.liststatementOwnershipDataTable.Rows.RemoveAt(StatementOwnershipDataGridView.CurrentRow.Index);
                        this.liststatementOwnershipDataTable.AcceptChanges();
                        this.PopulateStatementOwnershipDataGrid(0);
                        ////this.AssctOwnerHeaderPanel.Enabled = false;
                        ////to enable the save and cancel button in the master form
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Editexts the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AllOwnersTextBoxs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.SearchFirstNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchLastNameTextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchAddress1TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchAddress2TextBox.Text.Trim())) || (!string.IsNullOrEmpty(this.SearchCityTextBox.Text.Trim())))
                {
                    this.EnableSearchButton();
                }
                else
                {
                    if (this.allOwnersGridCount > 0)
                    {
                        this.SearchButton.Enabled = false;
                    }
                    else
                    {
                        this.DisableButtons();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MoveUpButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.AssociateStatementOwnership();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the F15015 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15015_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetMaxLength();
                this.CustimizeAssociatedOwnersGrid();
                this.CustimizeAllOwnersdeatilsGrid();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.LoadStatementOwnership();
                this.ParentForm.AcceptButton = this.SearchButton;
                this.RemoveDefaultSelection();
                this.ShowMOwnerType();
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

        /// <summary>
        /// Handles the CellDoubleClick event of the AllOwnersdeatilsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AllOwnersdeatilsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.formMasterPermissionEdit && this.PermissionFiled.editPermission && !string.IsNullOrEmpty(this.AllOwnersdeatilsDataGridView.Rows[e.RowIndex].Cells[this.AllOwnerID.Name].Value.ToString()))
                {
                    this.AssociateStatementOwnership();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the OwnerRecordPicture control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerRecordPicture_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo sliceForm = new FormInfo();

                // Owner Management Form Number
                int formNo = 91000;
                sliceForm = TerraScanCommon.GetFormInfo(formNo);
                sliceForm.optionalParameters = new object[1];

                // Set Parameter as OwnerID
                sliceForm.optionalParameters[0] = this.selectedOwnerId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(sliceForm));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events
    }
}
