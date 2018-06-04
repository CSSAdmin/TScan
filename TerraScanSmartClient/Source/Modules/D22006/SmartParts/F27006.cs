//---------------------------------------------------------------------------------
// <copyright file="F27006.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27006 Parcel Ownership.
// </summary>
//---------------------------------------------------------------------------------
// Change History
//*********************************************************************************
// Date			    Author		          Description
// ----------		---------		      -----------------------------------------
// 28/03/07         M.Vijayakumar          Created
// 03/02/09         Shanmuga Sundaram.A    Modified for CO:4807 
// 21/09/10         ManojKumar             Modified for CO:8568   
// 25/11/10         ManojKumar             Modified for CO:9514
//*********************************************************************************/

namespace D22006
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

    /// <summary>
    /// F27006 Class file
    /// </summary>
    public partial class F27006 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the parcelId(keyid)
        /// </summary>
        private int parcelId;

        /// <summary>
        /// Used to store validKeyId
        /// </summary>
        private int validKeyId;

        /// <summary>
        /// Used to save saveRowIndex
        /// </summary>
        private int saveRowIndex;

        /// <summary>
        /// Used to store ownerId
        /// </summary>
        private string ownerId;

        /// <summary>
        /// Used to store fururepush
        /// </summary>
        private bool futurepush;
        /// <summary>
        /// Used to store the rows count of All owners listing grid
        /// </summary>
        private int allOwnersGridCount;

        /// <summary>
        /// Used to store the rows count of Parcel owners grid
        /// </summary>
        private int parcelOwnersGridCount;

        /// <summary>
        /// Used to store the selectedParcelOwnerGridRowId
        /// </summary>
        private int selectedParcelOwnerGridRowId;

        /// <summary>
        /// Used to store selectedOwnerId
        /// </summary>
        private int selectedOwnerId;

        /// <summary>
        /// used to set Yes or No for IsPrimary
        /// </summary>
        private bool primaryId;

        /// <summary>
        /// Used to store undividedOwnershipId
        /// </summary>
        private int undividedOwnershipId;

        /// <summary>
        /// Used to store the parcelOwnerGridClick
        /// </summary>
        private bool parcelOwnerGridClick;

        /// <summary>
        /// Used to store tempParcelOwnerGridRowId
        /// </summary>
        private int tempParcelOwnerGridRowId;

        /// <summary>
        /// controller F27006
        /// </summary>
        private F27006Controller form27006Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store the saveOwnerPercent
        /// </summary>
        private decimal saveOwnerPercent;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store parcelOwnershipData which is instance of F27006ParcelOwnershipData
        /// </summary>
        private F27006ParcelOwnershipData parcelOwnershipData = new F27006ParcelOwnershipData();

        /// <summary>
        /// getMOwnerTypeData
        /// </summary>
        private F27006ParcelOwnershipData getMOwnerTypeData = new F27006ParcelOwnershipData();

        /// <summary>
        /// Used to store the listAllOwnersDetailDataTableDataTable insatnce
        /// </summary>
        private F27006ParcelOwnershipData.ListAllOwnersDetailDataTableDataTable listAllOwnersDetailDataTable = new F27006ParcelOwnershipData.ListAllOwnersDetailDataTableDataTable();

        /// <summary>
        /// Used to store the listParcelOwnershipDataTableDataTable
        /// </summary>
        private F27006ParcelOwnershipData.ListParcelOwnershipDataTableDataTable listParcelOwnershipDatatable = new F27006ParcelOwnershipData.ListParcelOwnershipDataTableDataTable();


        /// <summary>
        /// Used to store the listSeparateStmtDataTable
        /// </summary>
        private F27006ParcelOwnershipData.ListSeparateStmtDataTableDataTable ListSeparateStmtDataTable = new F27006ParcelOwnershipData.ListSeparateStmtDataTableDataTable();

        /// <summary>
        /// ownerOrder
        /// </summary>
        private string ownerOrder;

        /// <summary>
        /// isBilled
        /// </summary>
        private string isbilled;

        /// <summary>
        /// isprorated
        /// </summary>
        private string isprorated;

        /// <summary>
        /// ownerPercent
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
        /// Used to store validationMessage;
        /// </summary>
        private string validationMessage;

        /// <summary>
        /// Used to store flagFormEdited
        /// </summary>
        private bool flagFormEdited;

        /// <summary>
        /// Used to store the flagForMessageNo
        /// </summary>
        private bool flagForMessageNo;

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

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F27006"/> class.
        /// </summary>
        public F27006()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F27006(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.parcelId = keyID;
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
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        //// Event added for opening Owner Management as per CO:4807 by A.shanmuga Sundaram

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F27006Control
        /// </summary>
        [CreateNew]
        public F27006Controller Form27006Control
        {
            get { return this.form27006Control as F27006Controller; }
            set { this.form27006Control = value; }
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
                this.ClearAllAssociatedOwnerPart();
                this.ClearAllOwnersPart();
                this.ClearAllOwnersGrid();
                this.LockControls(false);

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
                this.flagFormEdited = false;
                this.flagForMessageNo = true;
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

               this.LoadParcelOwnership();
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
                        ///Modified for the CO:9514
                        
                        DialogResult Future = MessageBox.Show("Do you want to push the changes to this Parcel’s ownership to all future Roll Years?", "TerraScan – Push Ownership to Future Years", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (Future.Equals(DialogResult.Yes))
                        {
                            this.futurepush = true;
                        }
                        else
                        {
                            this.futurepush = false;
                        }
                        this.SaveParcelOwnership();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    if (this.validKeyId > 0)
                    {
                        this.LockControls(true);
                    }
                    else
                    {
                        this.LockControls(false);
                    }

                    this.ControlLock(false);
                    this.LoadParcelOwnership();
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
                    if (this.parcelId != eventArgs.Data.KeyId)
                    {
                        this.parcelId = eventArgs.Data.KeyId;
                        this.parcelOwnershipData = this.form27006Control.WorkItem.F27006_ListParcelOwnership(this.parcelId);
                        int.TryParse(this.parcelOwnershipData.ListOwnerValidID.Rows[0][this.parcelOwnershipData.ListOwnerValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
                    }

                    ////For Form funcatonality this is commented
                    if (this.validKeyId > 0)
                    {
                        this.LockControls(true);
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        this.LockControls(false);
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
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
                    this.SeparateStmtPanel.BackColor = Color.White;
                    this.SeparateStmtComboBox.BackColor = Color.White;
                    this.flagFormEdited = false;
                    this.flagForMessageNo = false;
                    this.parcelId = eventArgs.Data.SelectedKeyId;
                    this.LoadParcelOwnership();
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

        /// <summary>
        /// Forms the close.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_BaseSmartPart_formClose, Thread = ThreadOption.UserInterface)]
        public void FormClose(object sender, DataEventArgs<string> e)
        {
            if (e.Data == "ApplicationExitCall")
            {
                TerraScanCommon.FormName = string.Empty;
            }
            else if (e.Data == "UserClosing")
            {
                var formn = (BaseSmartPart)sender;
                //Form varq = sender as Form;
                if (this.masterFormNo.Equals(formn.ParentFormId))
                {
                    if (this.parcelId != -99)
                    {
                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = 20000;
                        sliceReloadActiveRecord.SelectedKeyId = this.parcelId;
                        this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    }
                }
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

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            this.D9030_F9030_LoadSliceDetails(this, eventArgs);
        }

        #endregion Protected methods


        #region Coding Parcel OwnerShip Associated Grid

        #region Parcel OwnerShip Associated Grid  Methods



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

        //changed  the fields for the CO:8568 
        /// <summary>
        /// Used Fill the Parcel Ownership Header part
        /// </summary>
        /// <param name="currentRowNo">currentRowNo</param>
        private void GetAssociatedOwnersPart(int currentRowNo)
        {
            if ((this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value != null) && (!string.IsNullOrEmpty(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value.ToString())))
            {
                this.isgridRowChange = true;

                this.FirstNameTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.FirstNameColumn.ColumnName].Value.ToString();
                this.LastNameTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.LastNameColumn.ColumnName].Value.ToString();
                this.Address1TextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.Address1Column.ColumnName].Value.ToString();
                this.Address2TextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.Address2Column.ColumnName].Value.ToString();
                this.OwnerPercentTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerPercent.Name].Value.ToString();
                this.ownerId = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value.ToString();
                //// Added this column for CO4807
                this.CityTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.CityColumn.ColumnName].Value.ToString();
                this.StateTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.StateColumn.ColumnName].Value.ToString();
                this.OwnerCodeTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.OwnerCodeColumn.ColumnName].Value.ToString();
                this.ZipCodeTextBox.Text = this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.ZipColumn.ColumnName].Value.ToString();
                SetComboboxValue(this.OwnerTypeComboBox, this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].Value.ToString());
                int.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerID.Name].Value.ToString(), out this.selectedOwnerId);
                SetComboboxValue(this.IsPrimaryComboBox, this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value.ToString());
                //SetComboboxValue(this.SeparateStmtComboBox, this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.UndividedOwnership.Name].Value.ToString());
                int undividedOwnershipId;
                if (this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.UndividedOwnership.Name].Value != null
                    && !string.IsNullOrEmpty(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.UndividedOwnership.Name].Value.ToString()))
                {
                    int.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.UndividedOwnership.Name].Value.ToString(), out undividedOwnershipId);
                    this.SeparateStmtComboBox.SelectedValue = undividedOwnershipId;
                }
                this.setComboboxBackcolor();
                decimal.TryParse(this.ParcelOwnershipDataGridView.Rows[currentRowNo].Cells[this.ParcelOwnerPercent.Name].Value.ToString(), out this.ownerPercent);
                this.currentRowIndex = currentRowNo;
                this.isgridRowChange = false;
                this.LockControls(true);
            }
        }


        /// <summary>
        /// Clears the associated owners Header part.
        /// </summary>
        private void ClearAssociatedOwnersPart()
        {
            //make textbox and combobox empty

            this.FirstNameTextBox.Text = string.Empty;
            this.LastNameTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.OwnerPercentTextBox.Text = string.Empty;
            this.OwnerCodeTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipCodeTextBox.Text = string.Empty;

            ////Todo combo box control
            this.IsPrimaryComboBox.Text = string.Empty;
            this.OwnerTypeComboBox.Text = string.Empty;
            this.SeparateStmtComboBox.Text = string.Empty;

        }

        /// <summary>
        /// Clears all associated owner part.
        /// </summary>
        private void ClearAllAssociatedOwnerPart()
        {
            this.listParcelOwnershipDatatable.Clear();
            this.ParcelOwnershipDataGridView.DataSource = this.listParcelOwnershipDatatable.DefaultView;
            this.ParcelOwnershipDataGridView.Rows[0].Selected = false;
            ////to set the rowindex to 0 when the grid is disabled
            this.currentRowIndex = 0;
            this.ParcelOwnershipDataGridView.Enabled = false;
            this.ParcelOwnershipGridVerticalScroll.Visible = true;
            ////To clear the Associated Owner Grid header part
            this.ClearAssociatedOwnersPart();
            ////To fill the data in the footer part of the Associated Parcel grid
            this.OrderCountlabel.Text = string.Empty;
            this.TtlOwnPercentTextBox.Text = string.Empty;
            this.PercentLabel.Text = string.Empty;
            ////to set the combobox when the grid empty
            this.IsPrimaryComboBox.SelectedIndex = -1;
            this.OwnerTypeComboBox.SelectedIndex = -1;
            this.SeparateStmtComboBox.SelectedIndex =-1;
            ////Color for the SeparateStmt Combobox
            this.SeparateStmtComboBox.BackColor = Color.White;
            this.SeparateStmtPanel.BackColor = Color.White;
            this.AssctOwnerHeaderPanel.Enabled = false;
            this.MoveDownButton.Enabled = false;
             
        }

        /// <summary>
        /// To Laod Parcel Ownership Grid
        /// </summary>
        private void LoadParcelOwnershipDataGrid()
        {
            this.listParcelOwnershipDatatable.Clear();
            this.parcelOwnershipData = this.form27006Control.WorkItem.F27006_ListParcelOwnership(this.parcelId);
            this.listParcelOwnershipDatatable = this.parcelOwnershipData.ListParcelOwnershipDataTable;
            int.TryParse(this.parcelOwnershipData.ListOwnerValidID.Rows[0][this.parcelOwnershipData.ListOwnerValidID.KeyIDColumn.ColumnName].ToString(), out this.validKeyId);
            this.PopulateParcelOwnershipDataGrid(0);
        }

        /// <summary>
        ///  To Populates the parcel ownership data grid with Data form database.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulateParcelOwnershipDataGrid(int rowIndex)
        {
            this.ParcelOwnershipDataGridView.DataSource = this.listParcelOwnershipDatatable.DefaultView;
            this.parcelOwnersGridCount = this.ParcelOwnershipDataGridView.OriginalRowCount;
            if (this.parcelOwnersGridCount > 0)
            {
                this.ParcelOwnershipDataGridView.Focus();
                this.ParcelOwnershipDataGridView.Enabled = true;
                ////to fill the Associated Owner Grid header part                    
                this.GetAssociatedOwnersPart(rowIndex);
                this.currentRowIndex = rowIndex;
                //////To fill the data in the foorter part of the Associated Parcel grid
                this.OrderCountlabel.Text = this.parcelOwnersGridCount.ToString();
                this.AssctOwnerHeaderPanel.Enabled = true;
                this.MoveDownButton.Enabled = true;
                TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, rowIndex);
                this.ActiveControl = SeparateStmtPanel;
                this.SeparateStmtPanel.Focus();
            }
            else
            {
                this.ClearAllAssociatedOwnerPart();
            }
            if (this.listParcelOwnershipDatatable.Rows.Count > this.ParcelOwnershipDataGridView.NumRowsVisible)
            {
                this.ParcelOwnershipGridVerticalScroll.Visible = false;
            }
            else
            {
                this.ParcelOwnershipGridVerticalScroll.Visible = true;
            }
        }

        ///// <summary>
        ///// To laod the Entire the Parcel ownership form
        ///// </summary>
        private void LoadParcelOwnership()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.MoveDownButton.Enabled = true;
            this.ParcelOwnershipDataGridView.Columns[this.ParcelOwnerName.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.ParcelOwnerPercent.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.Primarytype.Name].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            ////to set the value for the combo boxs
            this.SetGeneralComboBox();
            this.ClearAllOwnersPart();
            this.ClearAllOwnersGrid();
            this.ShowSeparateStmt(); 
            this.LoadParcelOwnershipDataGrid();
            this.DisableButtons();
            this.flagLoadOnProcess = false;
        }

        //Changes performed to the Co:8568 for added new field primary and 
        /// <summary>
        /// To Custimize AssociatedOwnersGrid
        /// </summary>
        private void CustimizeAssociatedOwnersGrid()
        {

            this.ParcelOwnershipDataGridView.AutoGenerateColumns = false;
            this.MownerID.DataPropertyName = this.listParcelOwnershipDatatable.MOwnerIDColumn.ColumnName; ////"MOwnerID";
            this.ParcelOwnerName.DataPropertyName = this.listParcelOwnershipDatatable.NameColumn.ColumnName;
            this.FirstName.DataPropertyName = this.listParcelOwnershipDatatable.FirstNameColumn.ColumnName;
            this.LastName.DataPropertyName = this.listParcelOwnershipDatatable.LastNameColumn.ColumnName;
            this.Address1.DataPropertyName = this.listParcelOwnershipDatatable.Address1Column.ColumnName;
            this.Address2.DataPropertyName = this.listParcelOwnershipDatatable.Address2Column.ColumnName;
            this.City.DataPropertyName = this.listParcelOwnershipDatatable.CityColumn.ColumnName;
            this.ParcelOwnerPercent.DataPropertyName = this.listParcelOwnershipDatatable.OwnerPercentColumn.ColumnName;
            this.IsPrimary.DataPropertyName = this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName;
            this.ParcelOwnerGet.DataPropertyName = this.listParcelOwnershipDatatable.IsBilledColumn.ColumnName;
            this.Prorated.DataPropertyName = this.listParcelOwnershipDatatable.IsProRatedColumn.ColumnName;
            this.ParcelOwnerOrder.DataPropertyName = this.listParcelOwnershipDatatable.OwnerOrderColumn.ColumnName;
            this.ParcelOwnerID.DataPropertyName = this.listParcelOwnershipDatatable.OwnerIDColumn.ColumnName;
            this.IsCurrent.DataPropertyName = this.listParcelOwnershipDatatable.IsCurrentColumn.ColumnName;
            this.IsTaxpayer.DataPropertyName = this.listParcelOwnershipDatatable.IsTaxPayerColumn.ColumnName;
            this.IsTRExtra.DataPropertyName = this.listParcelOwnershipDatatable.IsTRExtraColumn.ColumnName;
            this.UndividedOwnership.DataPropertyName = this.listParcelOwnershipDatatable.UndividedOwnershipIDColumn.ColumnName;
            this.Zip.DataPropertyName = this.listParcelOwnershipDatatable.ZipColumn.ColumnName;
            //// Added Columns to the ParcelOwnershipDataGridView as per the CO:4807 by Shanmuga Sundaram.a
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.OwnerCodeColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.OwnerCodeColumn.ColumnName;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeIDColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.MOwnerTypeIDColumn.ColumnName;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName;
            this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.StateColumn.ColumnName].DataPropertyName = this.listParcelOwnershipDatatable.StateColumn.ColumnName;
            this.ParcelOwnershipDataGridView.PrimaryKeyColumnName = this.listParcelOwnershipDatatable.OwnerIDColumn.ColumnName;
        }

        /// <summary>
        /// Sets the general combo box.
        /// </summary>
        private void SetGeneralComboBox()
        {
            this.IsPrimaryComboBox.Items.Clear();
            this.IsPrimaryComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.IsPrimaryComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));
        }

        /// <summary>
        /// To get Parcel Ownership datagrid row index
        /// </summary>
        /// <param name="searchOwnerId">Current Unique order Id to search</param>
        /// <returns>Integer value of the row index </returns>
        private int GetRowIndex(string searchOwnerId)
        {
            try
            {
                this.tempParcelOwnerGridRowId = -1;

                for (int i = 0; i < this.ParcelOwnershipDataGridView.Rows.Count; i++)
                {
                    if ((this.ParcelOwnershipDataGridView.Rows[i].Cells[this.ParcelOwnerID.Name].Value != null) && (!string.IsNullOrEmpty(this.ParcelOwnershipDataGridView.Rows[i].Cells[this.ParcelOwnerID.Name].Value.ToString())))
                    {
                        if ((this.ParcelOwnershipDataGridView.Rows[i].Cells[this.ParcelOwnerID.Name].Value.ToString() == searchOwnerId))
                        {
                            return this.tempParcelOwnerGridRowId = i;
                        }
                    }
                }

                return this.tempParcelOwnerGridRowId;
            }
            catch (Exception ex)
            {
                return this.tempParcelOwnerGridRowId;
            }
        }

        /// <summary>
        /// To get row particular index of the listParcelOwnershipDatatable
        /// </summary>
        /// <param name="currentOwnerId">Current Unique order Id to search</param>
        /// <returns>Integer value of the row index </returns>
        private int GetListParcelOwnershipDatatableRowIndex(string currentOwnerId)
        {
            try
            {
                this.saveRowIndex = -1;
                this.listParcelOwnershipDatatable.AcceptChanges();
                for (int i = 0; i < this.listParcelOwnershipDatatable.Rows.Count; i++)
                {
                    if ((!string.IsNullOrEmpty(this.listParcelOwnershipDatatable.Rows[i][this.listParcelOwnershipDatatable.OwnerIDColumn.ColumnName].ToString())))
                    {
                        if ((this.listParcelOwnershipDatatable.Rows[i][this.listParcelOwnershipDatatable.OwnerIDColumn.ColumnName].ToString() == currentOwnerId))
                        {
                            return this.saveRowIndex = i;
                        }
                    }
                }

                return this.saveRowIndex;
            }
            catch (Exception ex)
            {
                return this.saveRowIndex;
            }
        }

        #endregion Parcel OwnerShip Associated Grid Methods

        #region Parcel OwnerShip Associated Grid click

        /// <summary>
        /// Handles the RowEnter event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelOwnerGridClick = true;

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

                this.parcelOwnerGridClick = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To assign Values to ParcelGrid and datatable
        /// </summary>
        private void AssignDataToParcelGrid()
        {
            if (this.selectedOwnerId > 0 && this.iseditOn)
            {
                this.GetListParcelOwnershipDatatableRowIndex(this.selectedOwnerId.ToString());

                if (this.saveRowIndex >= 0)
                {
                    ////make one primary owner field as true and remaining primary to false 
                    if (this.IsPrimaryComboBox.SelectedIndex.Equals(1))
                    {
                        DataRow[] primaryRow = this.listParcelOwnershipDatatable.Select("IsPrimary=True");
                        if (primaryRow.Length > 0)
                        {
                            
                                primaryRow[0]["IsPrimary"] = "false";
                            
                        }
                    }

                    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.OwnerPercentColumn] = this.OwnerPercentTextBox.Text.Replace("%", "").Trim();
                    if (string.Equals(this.IsPrimaryComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                    {
                        this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsPrimaryColumn] = 1;

                    }
                    else
                    {
                        this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsPrimaryColumn] = 0;
                    }

                    //// Updation done in listParcelOwnershipDatatable when OwnerType Dropdown is changed as per the CO:4807 by Shanmuga Sundaram.a
                    byte ownerTypeId;
                    byte.TryParse(this.OwnerTypeComboBox.SelectedValue.ToString(), out ownerTypeId);
                    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.MOwnerTypeIDColumn] = ownerTypeId;
                    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.MOwnerTypeColumn] = this.OwnerTypeComboBox.Text.ToString();
                    this.listParcelOwnershipDatatable.AcceptChanges();
                    int undividedOwnershipId;
                    int.TryParse(this.SeparateStmtComboBox.SelectedValue.ToString(), out undividedOwnershipId);
                    int saveid = this.ParcelOwnershipDataGridView.OriginalRowCount;
                    for (int i = 0; i < saveid; i++)
                    {
                        this.listParcelOwnershipDatatable.Rows[i][this.listParcelOwnershipDatatable.UndividedOwnershipIDColumn] = undividedOwnershipId;
                    }

                    this.listParcelOwnershipDatatable.AcceptChanges();
                    this.iseditOn = false;
                }
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>

        private void ParcelOwnershipDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //used for Isprimary column to be red color if value is yes
                if (!string.IsNullOrEmpty((this.ParcelOwnershipDataGridView.Rows[e.RowIndex].Cells["IsPrimary"].Value.ToString())))
                {
                    this.ParcelOwnershipDataGridView.Rows[currentRowIndex].Selected = true;
                    if (this.ParcelOwnershipDataGridView.Rows[e.RowIndex].Cells["IsPrimary"].Value.ToString().Equals("True"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelOwnerGridClick = true;

                if (e.RowIndex >= 0)
                {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.AssignDataToParcelGrid();
                        this.GetAssociatedOwnersPart(e.RowIndex);
                        TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, e.RowIndex);
                        this.currentRowIndex = e.RowIndex;
                    }
                    else
                    {
                        this.GetAssociatedOwnersPart(e.RowIndex);
                        this.currentRowIndex = e.RowIndex;
                    }
                }

                this.parcelOwnerGridClick = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.parcelOwnerGridClick = true;
                DataGridView currentGrid = (DataGridView)sender;
                ////int partieCoulmnIndex = currentGrid.CurrentCell.ColumnIndex;
                int partieRowIndex = currentGrid.CurrentCell.RowIndex;

                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                                {
                                    this.AssignDataToParcelGrid();
                                    this.GetAssociatedOwnersPart((partieRowIndex + 1));
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, partieRowIndex);
                                    this.currentRowIndex = partieRowIndex;
                                }

                                break;
                            }

                        case Keys.Up:
                            {
                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                                {
                                    this.AssignDataToParcelGrid();
                                    this.GetAssociatedOwnersPart((partieRowIndex - 1));
                                    TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, partieRowIndex);
                                    this.currentRowIndex = partieRowIndex;
                                }

                                break;
                            }
                    }
                }
                this.parcelOwnerGridClick = false;

            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.ParcelOwnershipDataGridView.OriginalRowCount > 0)
                {
                    this.ParcelOwnershipDataGridView.Rows[this.currentRowIndex].Selected = true;
                    this.ParcelOwnershipDataGridView.CurrentCell = this.ParcelOwnershipDataGridView[2, this.currentRowIndex];
                    int length = this.ParcelOwnershipDataGridView.OriginalRowCount;
               for (int i = 0; i < length; i++)
                 {
                        ////sets the value of primary field to be (YES or NO)
                    if(this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value.ToString()!=null && !string.IsNullOrEmpty(this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value.ToString()) )
                    {
                        bool.TryParse(this.ParcelOwnershipDataGridView.Rows[i].Cells[this.listParcelOwnershipDatatable.IsPrimaryColumn.ColumnName].Value.ToString(), out this.primaryId);
                        if (this.primaryId)
                        {
                            this.ParcelOwnershipDataGridView.Rows[i].Cells[this.Primarytype.Name].Value = SharedFunctions.GetResourceString("YESValue");
                        }
                        else
                        {
                            this.ParcelOwnershipDataGridView.Rows[i].Cells[this.Primarytype.Name].Value = SharedFunctions.GetResourceString("NOValue");
                        }
                     }

                 }
                    object totalOwnership;
                    totalOwnership = this.listParcelOwnershipDatatable.Compute("SUM (OwnerPercent)", "OwnerID > 0");
                    this.TtlOwnPercentTextBox.Text = totalOwnership.ToString();
                    this.PercentLabel.Text = this.TtlOwnPercentTextBox.Text;
                }
                else
                {
                    ////To fill the data in the foorter part of the Associated stmt grid   
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
        /// Handles the CellDoubleClick event of the ParcelOwnershipDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelOwnershipDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.formMasterPermissionEdit && this.PermissionFiled.editPermission && !string.IsNullOrEmpty(this.ParcelOwnershipDataGridView.Rows[e.RowIndex].Cells[this.ParcelOwnerID.Name].Value.ToString()))
                {
                    this.ParcelOwnershipDataGridView.Focus();
                    if (ParcelOwnershipDataGridView.CurrentRow.Index >= 0)
                    {
                        this.listParcelOwnershipDatatable.Rows.RemoveAt(ParcelOwnershipDataGridView.CurrentRow.Index);
                        this.listParcelOwnershipDatatable.AcceptChanges();
                        this.PopulateParcelOwnershipDataGrid(0);
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

        #endregion Parcel OwnerShip Associated Grid click

        #endregion Coding Parcel OwnerShip Associated Grid

        #region Events

        //// Added Click event for Image to open Owner Management for particular OwnerID as per the CO:4807 by Shanmuga Sundaram.a

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
                int formNo = 91000;
                sliceForm = TerraScanCommon.GetFormInfo(formNo);
                sliceForm.optionalParameters = new object[1];
                sliceForm.optionalParameters[0] = this.ownerId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(sliceForm));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Common part

        #region Common Events

        /// <summary>
        /// To validet Parcel onwership record to save 
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool ValidParcelOwnership()
        {
            try
            {
                decimal.TryParse(this.OwnerPercentTextBox.Text.Replace("%", "").Trim(), out this.saveOwnerPercent);


                if ((this.saveOwnerPercent > 100 || this.saveOwnerPercent < 0))
                {
                    if (string.IsNullOrEmpty(this.validationMessage))
                    {
                        this.validationMessage = SharedFunctions.GetResourceString("Owner Percent of this Parcel is Invalid");
                    }
                    else
                    {
                        this.validationMessage = this.validationMessage + "\n" + SharedFunctions.GetResourceString("Owner Percent of this Parcel is Invalid");
                    }

                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
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
            this.validationMessage = string.Empty;
            this.AssignDataToParcelGrid();
            this.listParcelOwnershipDatatable.AcceptChanges();


            ////conveert owner percent to percentage format
            decimal.TryParse(this.OwnerPercentTextBox.Text.Replace("%", "").Trim(), out this.saveOwnerPercent);
            ////percent greater than 100 0r less than 0 invalid
            if ((this.saveOwnerPercent > 100 || this.saveOwnerPercent < 0))
            {
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Owner Percent of this Parcel is Invalid");
                return sliceValidationFields;
            }
            

            //DataRow[] ownerRows;
            //ownerRows = this.listParcelOwnershipDatatable.Select("OwnerOrder =" + "'" + 1 + "'");
            //if (ownerRows.Length <= 0)
            //{
            //    ////MessageBox.Show("This record cannot be saved because no Owner has been given an Order of 1 (one).", SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    sliceValidationFields.RequiredFieldMissing = false;
            //    sliceValidationFields.ErrorMessage = "This record cannot be saved because no Owner has been given an Order of 1 (one).";
            //    return sliceValidationFields;
            //}
            //else if (ownerRows.Length > 1)
            //{
            //    sliceValidationFields.RequiredFieldMissing = false;
            //    sliceValidationFields.ErrorMessage = "This record cannot be saved because more than one Owner has been given an Order of 1 (one).";
            //    return sliceValidationFields;
            //}

            ////set atleast one primary as true (or) without a single parcel record can't save 
            DataRow[] primaryRow = this.listParcelOwnershipDatatable.Select("IsPrimary=True");
            if (primaryRow.Length.Equals(0) || ParcelOwnershipDataGridView.OriginalRowCount.Equals(0))
            {
                DialogResult primary = MessageBox.Show("This record cannot be saved because no Primary Owner has been assigned for this Parcel.", "TerraScan T2 - Primary Owner", MessageBoxButtons.OK, MessageBoxIcon.Error);       
                if(primary.Equals(DialogResult.OK))
                {
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.DisableNewMethod = true;
                return sliceValidationFields;
                }
                return sliceValidationFields;
            }

            decimal.TryParse(this.listParcelOwnershipDatatable.Compute("SUM(OwnerPercent)", "OwnerID > 0").ToString(), out saveTtlOwnPercent);
            if (saveTtlOwnPercent == 100)
            {
                tocheckXmlString = TerraScanCommon.GetXmlString(this.listParcelOwnershipDatatable);
                //errorStatusId = this.form27006Control.WorkItem.F27006_CheckOwnershipDetails(tocheckXmlString);
                //switch (errorStatusId)
                //{
                //    case -99:
                //        sliceValidationFields.DisableNewMethod = true;
                //        MessageBox.Show(SharedFunctions.GetResourceString("OwnerShipValidation"), SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        ////sliceValidationFields.RequiredFieldMissing = false;
                //        ////sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("OwnerShipValidation");
                //        break;
                //    case -100:
                //        sliceValidationFields.RequiredFieldMissing = false;
                //        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("OwnerShipRequiredFieldValidation");
                //        break;
                //    case -101:
                //        sliceValidationFields.DisableNewMethod = true;
                //        MessageBox.Show(SharedFunctions.GetResourceString("OwnerShipOrderValidation"), SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        ////sliceValidationFields.RequiredFieldMissing = false;
                //        ////sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("OwnerShipOrderValidation");
                //        break;
                //}
            }
            else
            {
                //// shows the warning message if owner percent not equal to 100
                DialogResult result = MessageBox.Show("You are about to save this Parcel with a total ownership percentage that is not 100%. \r\nAre you sure you want to continue?", "TerraScan – Ownership Total", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result.Equals(DialogResult.No))
                {
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.DisableNewMethod = true;
                    return sliceValidationFields;

                }
                else
                {
                    return sliceValidationFields;

                }

            }

            return sliceValidationFields;
        }

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
        /// Handles the Leave event of the OwnerPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerPercentTextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.OwnerPercentTextBox.Text))
            {
                decimal outDecimal;
                decimal.TryParse(this.OwnerPercentTextBox.Text.ToString().Trim(), out outDecimal);

                if (outDecimal > 100)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Owner Percent of this Parcel is Invalid"), SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.OwnerPercentTextBox.Focus();
                }
            }
        }

        #endregion Common events

        #region Common Methods

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.FirstNameTextBox.MaxLength = this.listParcelOwnershipDatatable.FirstNameColumn.MaxLength;
            this.LastNameTextBox.MaxLength = this.listParcelOwnershipDatatable.LastNameColumn.MaxLength;
            this.IsPrimaryComboBox.MaxLength = this.listParcelOwnershipDatatable.IsPrimaryColumn.MaxLength;
            this.OwnerTypeComboBox.MaxLength = this.listParcelOwnershipDatatable.MOwnerTypeColumn.MaxLength;
            this.Address1TextBox.MaxLength = this.listParcelOwnershipDatatable.Address1Column.MaxLength;
            this.Address2TextBox.MaxLength = this.listParcelOwnershipDatatable.Address2Column.MaxLength;

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

            this.SeparateStmtComboBox.Enabled = !controlLook;
            this.IsPrimaryComboBox.Enabled = !controlLook;
            this.OwnerTypeComboBox.Enabled = !controlLook;
            this.OwnerPercentTextBox.LockKeyPress = controlLook;
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

                ////this.MoveDownButton.Enabled = false;
                ////this.MoveUpButton.Enabled = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Updates the parcel ownership data table.
        /// </summary>
        private void UpdateParcelOwnershipDataTable()
        {
            if (this.GetListParcelOwnershipDatatableRowIndex(this.ownerId) >= 0)
            {
                this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.FirstNameColumn] = this.FirstNameTextBox.Text.Trim();
                this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.LastNameColumn] = this.LastNameTextBox.Text.Trim();
                //if (!string.IsNullOrEmpty(this.OrderTextBox.Text.Trim()))
                //{
                //    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.OwnerOrderColumn] = this.OrderTextBox.Text.Trim();
                //}

                //if (String.Equals(this.ReceiveStmtComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                //{
                //    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsBilledColumn] = 1;
                //}
                //else
                //{
                //    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsBilledColumn] = 0;
                //}
                if (string.Equals(this.IsPrimaryComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESVALUE")))
                {
                    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsPrimaryColumn] = 1;
                }
                else
                {
                    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsPrimaryColumn] = 0;
                }



                this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.Address1Column] = this.Address1TextBox.Text.Trim();
                this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.Address2Column] = this.Address2TextBox.Text.Trim();

                //if (String.Equals(this.ProratedStmtComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                //{
                //    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsProRatedColumn] = 1;
                //}
                //else
                //{
                //    this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.IsProRatedColumn] = 0;
                //}

                this.listParcelOwnershipDatatable.Rows[this.saveRowIndex][this.listParcelOwnershipDatatable.OwnerPercentColumn] = this.OwnerPercentTextBox.Text.Replace("%", "").Trim();
                this.listParcelOwnershipDatatable.AcceptChanges();
            }
        }

        /// <summary>
        /// To save ParcelOwnership
        /// </summary>
        private void SaveParcelOwnership()
        {
            ////this.UpdateParcelOwnershipDataTable();
            this.listParcelOwnershipDatatable.AcceptChanges();
            string parcelOwnership = TerraScanCommon.GetXmlString(this.listParcelOwnershipDatatable);
            this.form27006Control.WorkItem.F27006_SaveParcelOwnership(parcelOwnership, this.parcelId, TerraScanCommon.UserId, this.futurepush);
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess && !this.parcelOwnerGridClick)
            {
                this.EditEnabled();
                this.ParcelOwnershipDataGridView.Columns[this.ParcelOwnerName.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Columns[this.ParcelOwnerPercent.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Columns[this.Primarytype.Name].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Columns[this.listParcelOwnershipDatatable.MOwnerTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.ParcelOwnershipDataGridView.Refresh();
            }
        }

        /// <summary>
        /// Removes the default selection.
        /// </summary>
        private void RemoveDefaultSelection()
        {
            if (this.ParcelOwnershipDataGridView.OriginalRowCount == 0)
            {
                this.ParcelOwnershipDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.ParcelOwnershipDataGridView.RemoveDefaultSelection = false;
            }
            if (this.AllOwnersdeatilsDataGridView.OriginalRowCount == 0)
            {
                this.AllOwnersdeatilsDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.AllOwnersdeatilsDataGridView.RemoveDefaultSelection = false;
            }
        }

        //// Added OwnerType Dropdown function as per the CO:4807 by Shanmuga Sundaram.a

        /// <summary>
        /// Shows the type of the M owner.
        /// </summary>
        private void ShowMOwnerType()
        {
            this.getMOwnerTypeData = this.form27006Control.WorkItem.ListMOwnerType();
            if (this.getMOwnerTypeData.listMOwnerTypeDataTable.Rows.Count > 0)
            {
                this.OwnerTypeComboBox.DataSource = this.getMOwnerTypeData.listMOwnerTypeDataTable;
                this.OwnerTypeComboBox.DisplayMember = this.getMOwnerTypeData.listMOwnerTypeDataTable.MOwnerTypeColumn.ColumnName;
                this.OwnerTypeComboBox.ValueMember = this.getMOwnerTypeData.listMOwnerTypeDataTable.MOwnerTypeIDColumn.ColumnName;
            }
        }
        ///<summary>
        ///shows the separate Statement for header part.
        ///</summary>
        private void ShowSeparateStmt()
        {
            this.parcelOwnershipData = this.form27006Control.WorkItem.F27006_ListParcelOwnership(this.parcelId);
            if (this.parcelOwnershipData.ListSeparateStmtDataTable.Rows.Count > 0)
            {
                this.SeparateStmtComboBox.DataSource = this.parcelOwnershipData.ListSeparateStmtDataTable;
                this.SeparateStmtComboBox.DisplayMember = this.parcelOwnershipData.ListSeparateStmtDataTable.UndividedOwnershipOptionColumn.ColumnName;
                this.SeparateStmtComboBox.ValueMember = this.parcelOwnershipData.ListSeparateStmtDataTable.UndividedOwnershipIDColumn.ColumnName;

            }

        }
        #endregion Common Methods

        #endregion Common part

        #region Coding for All Owners Grid

        #region AllOwnersGrid Methods

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
            //for search
            this.parcelOwnershipData = this.form27006Control.WorkItem.F27006_ListALLOwnerDetails(this.SearchFirstNameTextBox.Text.Trim(), this.SearchLastNameTextBox.Text.Trim(), this.SearchAddress1TextBox.Text.Trim(), this.SearchAddress2TextBox.Text.Trim(), this.SearchCityTextBox.Text.Trim());
            this.listAllOwnersDetailDataTable = this.parcelOwnershipData.ListAllOwnersDetailDataTable;
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
            this.AllOwnersdeatilsDataGridView.Columns.Add(this.listAllOwnersDetailDataTable.StateColumn.ColumnName, this.listAllOwnersDetailDataTable.StateColumn.ColumnName);
            this.AllOwnersdeatilsDataGridView.Columns.Add(this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName, this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName);

            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.StateColumn.ColumnName].Visible = false;
            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.StateColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName].Visible = false;
            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;

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
            this.AllOwnerIsProRated.DataPropertyName = this.listAllOwnersDetailDataTable.IsProRatedColumn.ColumnName;
            this.AllOwnerOrder.DataPropertyName = this.listAllOwnersDetailDataTable.OwnerOrderColumn.ColumnName;
            this.AllOwnerID.DataPropertyName = this.listAllOwnersDetailDataTable.OwnerIDColumn.ColumnName;
            this.AllOwnerZip.DataPropertyName = this.listAllOwnersDetailDataTable.ZipColumn.ColumnName;
            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.StateColumn.ColumnName].DataPropertyName = this.listAllOwnersDetailDataTable.StateColumn.ColumnName;
            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName].DataPropertyName = this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName;
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
            this.AllOwnerZip.DisplayIndex = 12;

            //// Added Index to Columns in ParcelOwnershipDataGridView as per the CO:4807 by Shanmuga Sundaram.a
            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.StateColumn.ColumnName].DisplayIndex = 12;
            this.AllOwnersdeatilsDataGridView.Columns[this.listAllOwnersDetailDataTable.OwnerCodeColumn.ColumnName].DisplayIndex = 13;
            ////primary key is ownerId
            this.AllOwnersdeatilsDataGridView.PrimaryKeyColumnName = this.listAllOwnersDetailDataTable.OwnerIDColumn.ColumnName;
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
        /// Associates the parcel ownership.
        /// </summary>
        private void AssociateParcelOwnership()
        {
            int allOwnerGirdRowIndexValue = -1;
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                this.AssignDataToParcelGrid();
            }

            this.AllOwnersdeatilsDataGridView.Focus();
            allOwnerGirdRowIndexValue = AllOwnersdeatilsDataGridView.CurrentRow.Index;
            if (allOwnerGirdRowIndexValue >= 0)
            {
                F27006ParcelOwnershipData.ListParcelOwnershipDataTableRow tempListAllOwnersDetailDataTableRow = this.listParcelOwnershipDatatable.NewListParcelOwnershipDataTableRow();
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
                            this.GetAssociatedOwnersPart(this.tempParcelOwnerGridRowId);
                            //TerraScanCommon.SetDataGridViewPosition(this.ParcelOwnershipDataGridView, this.tempParcelOwnerGridRowId);
                            //int.TryParse(this.ParcelOwnershipDataGridView.Rows[this.tempParcelOwnerGridRowId].Cells[this.listParcelOwnershipDatatable.OwnerOrderColumn.ColumnName].Value.ToString(), out this.selectedParcelOwnerGridRowId);
                            this.ToEnableEditButtonInMasterForm();
                        }
                        else
                        {
                            //// assign the values fetch from all owner to associate owner.
                            tempListAllOwnersDetailDataTableRow.MOwnerID = 0;
                            tempListAllOwnersDetailDataTableRow.OwnerID = tempOwnerIdValue;
                            tempListAllOwnersDetailDataTableRow.Name = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerName"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.FirstName = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerFirstName"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.LastName = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerLastName"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.Address1 = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerAddress1"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.Address2 = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerAddress2"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.City = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerCity"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.State = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["State"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.OwnerCode = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["OwnerCode"].Value.ToString();
                            tempListAllOwnersDetailDataTableRow.Zip = this.AllOwnersdeatilsDataGridView.Rows[allOwnerGirdRowIndexValue].Cells["AllOwnerZip"].Value.ToString();
                            if (this.ParcelOwnershipDataGridView.OriginalRowCount == 0)
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
                            tempListAllOwnersDetailDataTableRow.IsPrimary = false;
                            int undividedOwnershipId;
                            if (this.ParcelOwnershipDataGridView.OriginalRowCount.Equals(0))
                            {
                                this.SeparateStmtComboBox.SelectedIndex = 0;  
                            }
                            int.TryParse(this.SeparateStmtComboBox.SelectedValue.ToString(), out undividedOwnershipId);
                            tempListAllOwnersDetailDataTableRow.UndividedOwnershipID = undividedOwnershipId;
                            this.ShowSeparateStmt(); 
                            this.setComboboxBackcolor(); 
                            int currentTempOwnerOrderValue;
                            int.TryParse(this.listParcelOwnershipDatatable.Compute("MAX (OwnerOrder)", "OwnerID > 0").ToString(), out currentTempOwnerOrderValue);
                            if (this.ParcelOwnershipDataGridView.OriginalRowCount < this.ParcelOwnershipDataGridView.NumRowsVisible)
                            {
                                this.listParcelOwnershipDatatable.Rows.RemoveAt(this.ParcelOwnershipDataGridView.OriginalRowCount);
                                this.avoidParcelGridRowEnter = true;
                                this.listParcelOwnershipDatatable.Rows.InsertAt(tempListAllOwnersDetailDataTableRow, this.ParcelOwnershipDataGridView.OriginalRowCount);
                                this.avoidParcelGridRowEnter = false;
                            }
                            else
                            {
                                this.listParcelOwnershipDatatable.Rows.InsertAt(tempListAllOwnersDetailDataTableRow, this.ParcelOwnershipDataGridView.Rows.Count);
                            }

                            this.listParcelOwnershipDatatable.AcceptChanges();
                            ////this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                            this.PopulateParcelOwnershipDataGrid(this.parcelOwnersGridCount);
                            ////To disable the Associated owner header Part 
                            ////this.AssctOwnerHeaderPanel.Enabled = false;
                            this.ToEnableEditButtonInMasterForm();
                            ////to set the current parcel grid row index
                            ////int.TryParse(this.ParcelOwnershipDataGridView.Rows[5].Cells[this.ListParcelOwnershipDatatable.LastNameColumn.ColumnName].Value.ToString(), out tempRowIndex);
                            /////this.selectedParcelOwnerGridRowId = tempRowIndex;
                        }
                    }
                }
            }
        }

        #endregion AllOwnersGrid Methods

        #region AllOwnersGrid Events

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
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
                //if (!string.IsNullOrEmpty(this.OrderTextBox.Text.Trim()))
                //{
                int tempvalue = 0;
                this.ParcelOwnershipDataGridView.Focus();
                tempvalue = ParcelOwnershipDataGridView.CurrentRow.Index;
                if (ParcelOwnershipDataGridView.CurrentRow.Index >= 0)
                {
                    this.listParcelOwnershipDatatable.Rows.RemoveAt(ParcelOwnershipDataGridView.CurrentRow.Index);
                    this.listParcelOwnershipDatatable.AcceptChanges();
                    this.PopulateParcelOwnershipDataGrid(0);
                    ////this.AssctOwnerHeaderPanel.Enabled = false;
                    ////to enable the save and cancel button in the master form
                    this.ToEnableEditButtonInMasterForm();
                }
                //}
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

                this.AssociateParcelOwnership();
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
                //this.ParcelOwnershipDataGridView.Rows[currentRowIndex].Selected = true;
                if (e.RowIndex >= 0 && this.formMasterPermissionEdit && this.PermissionFiled.editPermission && !string.IsNullOrEmpty(this.AllOwnersdeatilsDataGridView.Rows[e.RowIndex].Cells[this.AllOwnerID.Name].Value.ToString()))
                {
                    this.AssociateParcelOwnership();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion AllOwnersGrid Events

        #endregion Coding for All Owners Grid

        #region Form Loading

        /// <summary>
        /// Handles the Load event of the F27006 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F27006_Load(object sender, EventArgs e)
        {
            try
            {

                this.flagFormEdited = false;
                this.SetMaxLength();
                this.CustimizeAssociatedOwnersGrid();
                this.CustimizeAllOwnersdeatilsGrid();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.LoadParcelOwnership();
                ////this.ParcelOwnershipDataGridView.Focus();
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

        #endregion Form Loading


        private void SeparateStmtComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////after selecting separate stmt background color changes according to the option.
            try
            {
                if (this.ParcelOwnershipDataGridView.OriginalRowCount != 0)
                {
                    this.setComboboxBackcolor();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);

            } 
        }
        private void setComboboxBackcolor()
        {
            int undividedOwnershipId;
            int.TryParse(this.SeparateStmtComboBox.SelectedValue.ToString(), out undividedOwnershipId);
            //if (this.ParcelOwnershipDataGridView.OriginalRowCount == 0)
            //{
            //    undividedOwnershipId = 1;
            //}
            string backcolor = null;
            string[] backcolorArr = null;
            int RColor;
            int GColor;
            int BColor;
            DataRow[] background = this.parcelOwnershipData.ListSeparateStmtDataTable.Select("UndividedOwnershipID=" + undividedOwnershipId);
            if (background.Length > 0)
            {
                backcolor = ((TerraScan.BusinessEntities.F27006ParcelOwnershipData.ListSeparateStmtDataTableRow)(background[0])).BackgroundColor;
            }
            if (!string.IsNullOrEmpty(backcolor))
            {
                char[] splitchar = { ',' };
                backcolorArr = backcolor.Split(splitchar);
                if (backcolorArr.Length.Equals(3))
                {
                    ////Getting Red Color
                    if (string.IsNullOrEmpty(backcolorArr[0]))
                    {
                        RColor = 255;
                    }
                    else
                    {
                        int.TryParse(backcolorArr[0], out RColor);
                    }

                    ////Getting Green Color
                    if (string.IsNullOrEmpty(backcolorArr[1]))
                    {
                        GColor = 255;
                    }
                    else
                    {
                        int.TryParse(backcolorArr[1], out GColor);
                    }

                    ////Getting Blue Color
                    if (string.IsNullOrEmpty(backcolorArr[2]))
                    {
                        BColor = 255;
                    }
                    else
                    {
                        int.TryParse(backcolorArr[2], out BColor);
                    }

                    ////Assign RGB value to form backcolor
                    if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                    {
                        RColor = 255;
                        GColor = 255;
                        BColor = 255;
                    }
                    this.SeparateStmtComboBox.BackColor = Color.FromArgb(RColor, GColor, BColor);
                    this.SeparateStmtPanel.BackColor = Color.FromArgb(RColor, GColor, BColor);
                }
                else
                {
                    this.SeparateStmtComboBox.BackColor = Color.White;
                    this.SeparateStmtPanel.BackColor = Color.White;
                }
            }
        }

        private void IsPrimaryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {


        }

        private void SeparateStmtComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.setComboboxBackcolor();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

    }

}