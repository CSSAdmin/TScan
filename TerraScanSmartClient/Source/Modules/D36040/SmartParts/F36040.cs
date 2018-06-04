//--------------------------------------------------------------------------------------------
// <copyright file="F36040.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36040.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/10/2007       Shiva              Created
// 05/05/2009       A.ShanmugaSundaram Modified to implement the CO:#7106
// 19/05/2009       Malliga            Modified for the issue 4224     
//***********************************************************************************************/
namespace D36040
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
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infrastructure.Interface;

    /// <summary>
    /// F36040 Class file
    /// </summary>
    public partial class F36040 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the cropUniqueId (here it is unique id and key id)
        /// </summary>
        private int cropUniqueId;

        /// <summary>
        /// Used to save crop uniques id
        /// </summary>
        private int savedcropUniqueId;

        /// <summary>
        /// used to store the savedCropRollYear
        /// </summary>
        private string savedCropRollYear;

        /// <summary>
        /// used to store permanentCropData.
        /// </summary>
        private F36040PermanentCropData permanentCropData = new F36040PermanentCropData();

        /// <summary>
        /// used to store cropCatalogGrid DataTable. 
        /// </summary>
        private F36040PermanentCropData.ListCropCatalogDetialsDataTable cropCatalogDetailsGridDataTable = new F36040PermanentCropData.ListCropCatalogDetialsDataTable();

        /// <summary>
        /// Used to store the listNeighborhoodTypeDataTable
        /// </summary>
        private F36040PermanentCropData.ListNeighborhoodTypeDataTable listNeighborhoodTypeDataTable = new F36040PermanentCropData.ListNeighborhoodTypeDataTable();

        /// <summary>
        /// used to store permanentCropGridSource
        /// </summary>
        private BindingSource permanentCropGridSource = new BindingSource();

        /// <summary>
        /// Used to store the valueSliceId (Dummy variable will not be used, get from form master)
        /// </summary>
        //////private int valueSliceId;

        /// <summary>
        /// controller F36040
        /// </summary>
        private F36040Controller form36040Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// Used to store the currentRollYesr
        /// </summary>
        private string currentRollYear;

        /// <summary>
        /// Used to store the avoidEditMode
        /// </summary>
        private bool avoidEditMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Used store the setButtonModeOnformLoad
        /// </summary>
        private bool setButtonModeOnformLoad;

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
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        /// <summary>
        /// Used store the rollYearChangeFlag
        /// </summary>
        private bool rollYearChangeFlag = false;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36040"/> class.
        /// </summary>
        public F36040()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36040"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36040(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            ////this.valueSliceId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.PermCropPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PermCropPictureBox.Height, this.PermCropPictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructors

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form36040 control.
        /// </summary>
        /// <value>The form36040 control.</value>
        [CreateNew]
        public F36040Controller Form36040Control
        {
            get { return this.form36040Control as F36040Controller; }
            set { this.form36040Control = value; }
        }

        #endregion Property

        #region Event Subscription

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
                eventArgs.Data.FlagInvalidSliceKey = false;

                if (this.setButtonModeOnformLoad)
                {
                    this.setButtonModeOnformLoad = false;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }
            }
        }

        /// <summary>
        /// Handles the Button Click Event For the WorkSpace
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewPermanentCrop();
                    break;
                case "SAVE":
                    this.SavePermanentCrop();
                    break;
                case "CANCEL":
                    this.CancelPermanentCrop();
                    break;
                case "DELETE":
                    this.DeletePermanentCrop();
                    break;
            }
        }

      
        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                ////eventArgs.Data.FlagFormClose = this.CheckPageStatus(!eventArgs.Data.FlagForQueryEnine);
                eventArgs.Data.FlagFormClose = this.CheckPageStatus();
            }
        }

        /// <summary>
        /// Event Subscription D84700_F84721_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84721_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            ////here the current form slice F36040 is reloaded when F36041 formlice save and delete funcationality
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.PermanentCropDataGrid.Enabled = true;
                this.LoadCropDetailsGrid();
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        #endregion Event Subscription

        #region Private Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form36040Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form36040Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form36040Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>pageStatus Bool Value</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), SharedFunctions.GetResourceString("F36040CropCodeValues"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.SavePermanentCropValues())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.CancelPermanentCrop();

                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the length of the break max.
        /// </summary>
        /// <returns>status of the maxLength</returns>
        private bool CheckBreakMaxLength()
        {
            double breakMaxValue = 9999999.99;
            double valuePerMaxValue = 922337203685477.5807;

            double baseValue;
            double break1;
            double break1ValuePer;
            double break2;
            double break2ValuePer;
            double break3;
            double break3ValuePer;
            double break4;
            double break4ValuePer;
            double break5;
            double break5ValuePer;

            double.TryParse(this.BaseValuePerTextBox.Text.Trim(), out baseValue);
            double.TryParse(this.Break1TextBox.Text.Trim(), out break1);
            double.TryParse(this.Break1ValuePerTextBox.Text.Trim(), out break1ValuePer);
            double.TryParse(this.Break2TextBox.Text.Trim(), out break2);
            double.TryParse(this.Break2ValuePerTextBox.Text.Trim(), out break2ValuePer);
            double.TryParse(this.Break3TextBox.Text.Trim(), out break3);
            double.TryParse(this.Break3ValuePerTextBox.Text.Trim(), out break3ValuePer);
            double.TryParse(this.Break4TextBox.Text.Trim(), out break4);
            double.TryParse(this.Break4ValuePerTextBox.Text.Trim(), out break4ValuePer);
            double.TryParse(this.Break5TextBox.Text.Trim(), out break5);
            double.TryParse(this.Break5ValuePerTextBox.Text.Trim(), out break5ValuePer);

            if (baseValue < valuePerMaxValue && break1 <= breakMaxValue && break2 <= breakMaxValue && break3 <= breakMaxValue && break4 <= breakMaxValue && break5 <= breakMaxValue && break1ValuePer < valuePerMaxValue && break2ValuePer < valuePerMaxValue && break3ValuePer < valuePerMaxValue && break4ValuePer < valuePerMaxValue && break5ValuePer < valuePerMaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Saves the permanent crop values.
        /// </summary>
        /// <returns>error status</returns>
        private bool SavePermanentCropValues()
        {
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.NeighborhoodComboBox.Text.Trim()) && this.NeighborhoodComboBox.SelectedValue != null && !string.IsNullOrEmpty(this.CropCodeTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.FruitTreeComboBox.Text.Trim()) && !string.IsNullOrEmpty(this.BaseValuePerTextBox.Text.Trim()) && this.BaseValuePerTextBox.DecimalTextBoxValue >= 0)
            {
                if (!this.ValidateRollYear())
                {
                    ////This method is used to check the max length for decimal value text box
                    if (this.CheckBreakMaxLength())
                    {
                        this.CheckSaveCropCatalogItems();

                        if (this.ValidateBreakValue())
                        {
                            this.savedCropRollYear = this.RollYearTextBox.Text.Trim();
                            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                            {
                                this.savedcropUniqueId = this.form36040Control.WorkItem.F36040_SaveCropCatalog(null, TerraScanCommon.GetXmlString(this.permanentCropData.SaveCropCatalogDetials.Copy()), TerraScanCommon.UserId);
                            }
                            else
                            {
                                this.savedcropUniqueId = this.form36040Control.WorkItem.F36040_SaveCropCatalog(this.cropUniqueId, TerraScanCommon.GetXmlString(this.permanentCropData.SaveCropCatalogDetials.Copy()), TerraScanCommon.UserId);
                            }

                            ////when the savedCropUniqueid value are not Unique then -1 will be returned                    
                            if (this.savedcropUniqueId > 0)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("Roll year, Neighborhood, and Crop Code combination should be unique."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("Break values should be in increasing order and it should not be equal to zero."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Checks the save crop catalog items.
        /// </summary>
        private void CheckSaveCropCatalogItems()
        {
            Int16 saveRollyear;

            this.permanentCropData.SaveCropCatalogDetials.Rows.Clear();
            F36040PermanentCropData.SaveCropCatalogDetialsRow dr = this.permanentCropData.SaveCropCatalogDetials.NewSaveCropCatalogDetialsRow();

            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                Int16.TryParse(this.RollYearTextBox.Text.Trim(), out saveRollyear);
                dr.RollYear = saveRollyear;
            }

            if (!string.IsNullOrEmpty(this.NeighborhoodComboBox.Text.Trim()))
            {
                dr.NBHDID = Convert.ToInt32(this.NeighborhoodComboBox.SelectedValue);
            }

            dr.CropCode = this.CropCodeTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(this.FruitTreeComboBox.Text.Trim()))
            {
                dr.IsFruitTree = Convert.ToBoolean(this.FruitTreeComboBox.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.BaseValuePerTextBox.Text.Trim()))
            {
                dr.BaseValue = this.BaseValuePerTextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                dr.Description = this.DescriptionTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.Break1TextBox.Text.Trim()))
            {
                dr.Break1 = this.Break1TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break1ValuePerTextBox.Text.Trim()))
            {
                dr.Value1 = this.Break1ValuePerTextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break2TextBox.Text.Trim()))
            {
                dr.Break2 = this.Break2TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break2ValuePerTextBox.Text.Trim()))
            {
                dr.Value2 = this.Break2ValuePerTextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break3TextBox.Text.Trim()))
            {
                dr.Break3 = this.Break3TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break3ValuePerTextBox.Text.Trim()))
            {
                dr.Value3 = this.Break3ValuePerTextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break4TextBox.Text.Trim()))
            {
                dr.Break4 = this.Break4TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break4ValuePerTextBox.Text.Trim()))
            {
                dr.Value4 = this.Break4ValuePerTextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break5TextBox.Text.Trim()))
            {
                dr.Break5 = this.Break5TextBox.DecimalTextBoxValue;
            }

            if (!string.IsNullOrEmpty(this.Break5ValuePerTextBox.Text.Trim()))
            {
                dr.Value5 = this.Break5ValuePerTextBox.DecimalTextBoxValue;
            }

            this.permanentCropData.SaveCropCatalogDetials.Rows.Add(dr);
        }

        /// <summary>
        /// To Validate the Break values
        /// </summary>
        /// <returns>boolean Value</returns>
        private bool ValidateBreakValue()
        {
            decimal currentBreakValue;
            decimal referBreakvalue;
            int currentColumnIndex;

            F36040PermanentCropData.CheckBreakValuesDataTable checkBreakValuesDataTable = new F36040PermanentCropData.CheckBreakValuesDataTable();

            ////Here break Value are validated such that the Break value are in ascending order
            string filtercondtion = "RollYear IS NOT NULL";

            DataRow[] validatateRow = this.permanentCropData.SaveCropCatalogDetials.Select(filtercondtion);

            foreach (DataRow currentValidateRow in validatateRow)
            {
                checkBreakValuesDataTable.ImportRow(currentValidateRow);
            }

            if (checkBreakValuesDataTable.Rows.Count > 0)
            {
                ////here for loop is used to check whether the break values are increasing order and other condition like break can be given when all its desecending break are given
                for (int itemCount = checkBreakValuesDataTable.Columns.Count; itemCount > 0; itemCount--)
                {
                    currentColumnIndex = itemCount - 1;

                    if (!string.IsNullOrEmpty(checkBreakValuesDataTable.Rows[0][currentColumnIndex].ToString()))
                    {
                        decimal.TryParse(checkBreakValuesDataTable.Rows[0][currentColumnIndex].ToString(), out currentBreakValue);

                        if (currentBreakValue > 0)
                        {
                            for (int columnIndex = currentColumnIndex - 1; columnIndex >= 0; columnIndex--)
                            {
                                if (!string.IsNullOrEmpty(checkBreakValuesDataTable.Rows[0][columnIndex].ToString()))
                                {
                                    decimal.TryParse(checkBreakValuesDataTable.Rows[0][columnIndex].ToString(), out referBreakvalue);

                                    if (currentBreakValue <= referBreakvalue)
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit && !this.flagLoadOnProcess && !this.avoidEditMode)
            {
                this.PermanentCropDataGrid.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.BaseValuePerTextBox.LockKeyPress = controlLook;
            this.Break1TextBox.LockKeyPress = controlLook;
            this.Break1ValuePerTextBox.LockKeyPress = controlLook;
            this.Break2TextBox.LockKeyPress = controlLook;
            this.Break2ValuePerTextBox.LockKeyPress = controlLook;
            this.Break3TextBox.LockKeyPress = controlLook;
            this.Break3ValuePerTextBox.LockKeyPress = controlLook;
            this.Break4TextBox.LockKeyPress = controlLook;
            this.Break4ValuePerTextBox.LockKeyPress = controlLook;
            this.Break5TextBox.LockKeyPress = controlLook;
            this.Break5ValuePerTextBox.LockKeyPress = controlLook;
            this.CropCodeTextBox.LockKeyPress = controlLook;

            this.NeighborhoodComboBox.Enabled = !controlLook;
            this.FruitTreeComboBox.Enabled = !controlLook;
        }

        /// <summary>
        /// Populates the permanent crop header part controls.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulatePermanentCropHeaderPartControls(int rowIndex)
        {
            if (this.cropCatalogDetailsGridDataTable.Rows.Count > 0 && rowIndex >= 0 && this.PermanentCropDataGrid.Rows.VisibleRowCount > 1 && rowIndex <= this.cropCatalogDetailsGridDataTable.Rows.Count - 1)
            {
                this.avoidEditMode = true;
                this.RollYearTextBox.Text = string.Empty;
                this.CropCodeTextBox.Text = string.Empty;
                this.DescriptionTextBox.Text = string.Empty;
                this.PopulateAllPermanentCropComboBoxValues(string.Empty, string.Empty);
                this.FruitTreeComboBox.SelectedIndex = 0;
                this.BaseValuePerTextBox.Text = string.Empty;
                this.Break1TextBox.Text = string.Empty;
                this.Break1ValuePerTextBox.Text = string.Empty;
                this.Break2TextBox.Text = string.Empty;
                this.Break2ValuePerTextBox.Text = string.Empty;
                this.Break3TextBox.Text = string.Empty;
                this.Break3ValuePerTextBox.Text = string.Empty;
                this.Break4TextBox.Text = string.Empty;
                this.Break4ValuePerTextBox.Text = string.Empty;
                this.Break5TextBox.Text = string.Empty;
                this.Break5ValuePerTextBox.Text = string.Empty;
                int.TryParse(this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.CropVIDColumn.ColumnName].Value.ToString(), out this.cropUniqueId);
                this.RollYearTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.RollYearColumn.ColumnName].Value.ToString();
                this.LoadNeighborhoodComBoBasedOnRollYear();
                this.PopulateAllPermanentCropComboBoxValues(this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.IsFruitTreeColumn.ColumnName].Value.ToString(), this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.permanentCropData.GetCropNBHD.NBHDIDColumn.ColumnName].Value.ToString());
                this.CropCodeTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.CropCodeColumn.ColumnName].Value.ToString();
                this.DescriptionTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.DescriptionColumn.ColumnName].Value.ToString();
                // Coding Added for the issue 4224 by Malliga on 19/5/2009.
                // if we give any decimal value then it will display decimal otherwise it will display whole value
                this.BaseValuePerTextBox.TextCustomFormat = "###0.00";
                this.BaseValuePerTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.BaseValueColumn.ColumnName].Value.ToString();
                this.AccountTextLeave(this.BaseValuePerTextBox.Text, this.BaseValuePerTextBox);
                this.Break1TextBox.TextCustomFormat = "###0.00";
                this.Break1TextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Break1Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break1TextBox.Text, this.Break1TextBox);
                this.Break1ValuePerTextBox.TextCustomFormat = "###0.00";
                this.Break1ValuePerTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Value1Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break1ValuePerTextBox.Text, this.Break1ValuePerTextBox);
                this.Break2TextBox.TextCustomFormat = "###0.00";
                this.Break2TextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Break2Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break2TextBox.Text, this.Break2TextBox);
                this.Break2ValuePerTextBox.TextCustomFormat = "###0.00";
                this.Break2ValuePerTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Value2Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break2ValuePerTextBox.Text, this.Break2ValuePerTextBox);
                this.Break3TextBox.TextCustomFormat = "###0.00";
                this.Break3TextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Break3Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break3TextBox.Text, this.Break3TextBox);
                this.Break3ValuePerTextBox.TextCustomFormat = "###0.00";
                this.Break3ValuePerTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Value3Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break3ValuePerTextBox.Text, this.Break3ValuePerTextBox);
                this.Break4TextBox.TextCustomFormat = "###0.00";
                this.Break4TextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Break4Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break4TextBox.Text, this.Break4TextBox);
                this.Break4ValuePerTextBox.TextCustomFormat = "###0.00";
                this.Break4ValuePerTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Value4Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break4ValuePerTextBox.Text, this.Break4ValuePerTextBox);
                this.Break5TextBox.TextCustomFormat = "###0.00";
                this.Break5TextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Break5Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break5TextBox.Text, this.Break5TextBox);
                this.Break5ValuePerTextBox.TextCustomFormat = "###0.00";
                this.Break5ValuePerTextBox.Text = this.PermanentCropDataGrid.Rows[rowIndex].Cells[this.cropCatalogDetailsGridDataTable.Value5Column.ColumnName].Value.ToString();
                this.AccountTextLeave(this.Break5ValuePerTextBox.Text, this.Break5ValuePerTextBox);
                // changes ends here for the issue 4224
                this.CropDetialsHeaderPanel.Enabled = true;

                ////Set the current row index
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                this.avoidEditMode = false;
            }
            else
            {
                this.ClearPermanentCropHeaderControls();
                this.cropUniqueId = -999;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.CropDetialsHeaderPanel.Enabled = false;
            }
        }

        /// <summary>
        /// Clears the permanent crop header controls.
        /// </summary>
        private void ClearPermanentCropHeaderControls()
        {
            this.cropUniqueId = -999;

            this.avoidEditMode = true;

            this.RollYearTextBox.Text = string.Empty;
            this.CropCodeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.PopulateAllPermanentCropComboBoxValues(string.Empty, string.Empty);
            this.FruitTreeComboBox.SelectedIndex = 0;
            this.BaseValuePerTextBox.Text = string.Empty;
            this.Break1TextBox.Text = string.Empty;
            this.Break1ValuePerTextBox.Text = string.Empty;
            this.Break2TextBox.Text = string.Empty;
            this.Break2ValuePerTextBox.Text = string.Empty;
            this.Break3TextBox.Text = string.Empty;
            this.Break3ValuePerTextBox.Text = string.Empty;
            this.Break4TextBox.Text = string.Empty;
            this.Break4ValuePerTextBox.Text = string.Empty;
            this.Break5TextBox.Text = string.Empty;
            this.Break5ValuePerTextBox.Text = string.Empty;

            this.avoidEditMode = false;
        }

        /// <summary>
        /// Populates all permanent crop combo box values.
        /// </summary>
        /// <param name="fruitTreeComboBoxSelectedValue">The fruit tree combo box selected value.</param>
        /// <param name="neighborhoodTypeComboBoxSelectedValue">The neighborhood type combo box selected value.</param>
        private void PopulateAllPermanentCropComboBoxValues(string fruitTreeComboBoxSelectedValue, string neighborhoodTypeComboBoxSelectedValue)
        {
            if (!string.IsNullOrEmpty(neighborhoodTypeComboBoxSelectedValue))
            {
                this.NeighborhoodComboBox.SelectedValue = neighborhoodTypeComboBoxSelectedValue;
            }
            else
            {
                this.NeighborhoodComboBox.SelectedValue = -1;
            }

            if (fruitTreeComboBoxSelectedValue.Trim().Equals("Yes"))
            {
                this.FruitTreeComboBox.SelectedValue = 1;
            }
            else
            {
                this.FruitTreeComboBox.SelectedValue = 0;
            }
        }

        /// <summary>
        /// Loads the crop details grid.
        /// </summary>
        private void LoadCropDetailsGrid()
        {
            this.flagLoadOnProcess = true;

            this.InitFruitTreeCombo();
            this.permanentCropData.Clear();
            this.listNeighborhoodTypeDataTable.Clear();
            this.PermanentCropDataGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
            this.PermanentCropDataGrid.DisplayLayout.Bands[0].Columns[this.cropCatalogDetailsGridDataTable.BaseValueColumn.ColumnName].MaskInput = "nnnnnnnnnnnnnnn.nn";

            this.listNeighborhoodTypeDataTable = this.form36040Control.WorkItem.F36040_ListNeighborhoodType().ListNeighborhoodType;
           
            this.DescriptionTextBox.MaxLength = this.cropCatalogDetailsGridDataTable.DescriptionColumn.MaxLength;
            this.CropCodeTextBox.MaxLength = this.cropCatalogDetailsGridDataTable.CropCodeColumn.MaxLength;
            //////bind the combo box with datatable
            ////this.NeighborhoodComboBox.MaxLength = this.cropCatalogDetailsGridDataTable.NBHDListColumn.MaxLength;
            ////this.NeighborhoodComboBox.DisplayMember = this.listNeighborhoodTypeDataTable.NBHDListColumn.ColumnName;
            ////this.NeighborhoodComboBox.ValueMember = this.listNeighborhoodTypeDataTable.NBHDIDColumn.ColumnName;
            ////this.NeighborhoodComboBox.DataSource = this.listNeighborhoodTypeDataTable;

            this.permanentCropData = this.form36040Control.WorkItem.F36040_ListCropCatalog();
            ////to get theapplication roll year
            if (this.permanentCropData.GetAppRollYear.Rows.Count > 0)
            {
                this.currentRollYear = this.permanentCropData.GetAppRollYear.Rows[0][this.permanentCropData.GetAppRollYear.AssessmentRollYearColumn].ToString();
            }

            ////Coding added for the CO : 4796 by malliga 
            this.NeighborhoodComboBox.DataSource = this.permanentCropData.GetCropNBHD;
            this.NeighborhoodComboBox.DisplayMember = this.permanentCropData.GetCropNBHD.NBHDListColumn.ColumnName;
            this.NeighborhoodComboBox.ValueMember = this.permanentCropData.GetCropNBHD.NBHDIDColumn.ColumnName;
            //// Set the Width of the Neiborhood Combo when lenght increases than its width defined.
            this.SetDropDownWidth();

            ////to load the land code grid
            this.cropCatalogDetailsGridDataTable = this.permanentCropData.ListCropCatalogDetials;
            this.PermanentCropDataGrid.DataSource = this.cropCatalogDetailsGridDataTable;
            if (this.cropCatalogDetailsGridDataTable.Rows.Count > 0)
            {
                this.PermanentCropGridPanel.Enabled = true;

                ////to filter the datatset containing the roll year
                if (!string.IsNullOrEmpty(this.currentRollYear))
                {
                    this.FilterCropCatalogGridRows(this.currentRollYear);
                }
                else
                {
                    this.PermanentCropDataGrid.Rows[0].Selected = true;
                }
            }
            else
            {
                this.PermanentCropDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                this.ClearCropCatalogGridDetails();
                this.PermanentCropGridPanel.Enabled = false;
            }

            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Filters the crop catalog grid rows.
        /// </summary>
        /// <param name="filterByRollYear">The filter by roll year.</param>
        private void FilterCropCatalogGridRows(string filterByRollYear)
        {
            this.PermanentCropDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
            this.PermanentCropDataGrid.DisplayLayout.Bands[0].ColumnFilters[this.cropCatalogDetailsGridDataTable.RollYearColumn.ColumnName].FilterConditions.Add(FilterComparisionOperator.StartsWith, filterByRollYear);
            UltraGridRow[] filteredRow = this.PermanentCropDataGrid.Rows.GetFilteredInNonGroupByRows();

            if (filteredRow.Length > 0)
            {
                filteredRow[0].Activated = true;
                filteredRow[0].Selected = true;

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            }
            else
            {
                this.ClearCropCatalogGridDetails();
            }
        }

        /// <summary>
        /// Clears the crop catalog grid details.
        /// </summary>
        private void ClearCropCatalogGridDetails()
        {
            this.cropUniqueId = -999;
            this.setButtonModeOnformLoad = true;
            this.ClearPermanentCropHeaderControls();
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            this.CropDetialsHeaderPanel.Enabled = false;
        }

        /// <summary>
        /// Inits the fruit tree combo.
        /// </summary>
        private void InitFruitTreeCombo()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.FruitTreeComboBox.DataSource = commonData.ComboBoxDataTable;
            this.FruitTreeComboBox.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.FruitTreeComboBox.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Validates the roll year.
        /// </summary>
        /// <returns>rollYear status.</returns>
        private bool ValidateRollYear()
        {
            try
            {
                short tempRollYear;
                Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
                if (tempRollYear < 1900 || tempRollYear > 2079)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the width of the drop down.
        /// </summary>
        private void SetDropDownWidth()
        {
            if (this.NeighborhoodComboBox.Items.Count > 0)
            {
                if (this.permanentCropData.GetCropNBHD.Rows.Count > 0)
                {
                    int oneTimeValueSet = 0;
                    string tempNBHDListLenght = string.Empty;
                    string tempNBHDListmaxLenght = string.Empty;
                    for (int i = 0; i < this.permanentCropData.GetCropNBHD.Rows.Count; i++)
                    {
                        tempNBHDListLenght = this.permanentCropData.GetCropNBHD.Rows[i][this.permanentCropData.GetCropNBHD.NBHDListColumn.ColumnName].ToString();

                        //// First time value has to be set to tempNBHDListmaxLenght for comparing with tempNBHDListLenght values in the dataset
                        if (oneTimeValueSet.Equals(0))
                        {
                            tempNBHDListmaxLenght = tempNBHDListLenght;
                            oneTimeValueSet++;
                        }

                        //// comparing tempNBHDListmaxLenght with tempNBHDListLenght values in the dataset and setting the tempNBHDListmaxLenght value depending on the condition
                        if (tempNBHDListmaxLenght.Length < tempNBHDListLenght.Length)
                        {
                            //// If tempNBHDListmaxLenght smaller than the tempNBHDListLenght then tempNBHDListLenght value is assigned to tempNBHDListmaxLenght
                            tempNBHDListmaxLenght = tempNBHDListLenght;
                        }
                    }

                    ////int oneTimeValueSet = 0;
                    ////string tempNBHDListLenght = string.Empty;
                    ////string tempNBHDListmaxLenght = string.Empty;
                    ////for (int i = 0; i < this.listNeighborhoodTypeDataTable.Count; i++)
                    ////{
                    ////    tempNBHDListLenght = this.listNeighborhoodTypeDataTable.Rows[i][this.listNeighborhoodTypeDataTable.NBHDListColumn.ColumnName].ToString();
                    ////    //// First time value has to be set to tempNBHDListmaxLenght for comparing with tempNBHDListLenght values in the dataset
                    ////    if (oneTimeValueSet.Equals(0))
                    ////    {
                    ////        tempNBHDListmaxLenght = tempNBHDListLenght;
                    ////        oneTimeValueSet++;
                    ////    }

                    ////    //// comparing tempNBHDListmaxLenght with tempNBHDListLenght values in the dataset and setting the tempNBHDListmaxLenght value depending on the condition
                    ////    if (tempNBHDListmaxLenght.Length < tempNBHDListLenght.Length)
                    ////    {
                    ////        //// If tempNBHDListmaxLenght smaller than the tempNBHDListLenght then tempNBHDListLenght value is assigned to tempNBHDListmaxLenght
                    ////        tempNBHDListmaxLenght = tempNBHDListLenght;
                    ////    }
                    ////}

                    //// Calculating the Width of the NeighborhoodComboBox
                    Graphics graphics = this.CreateGraphics();
                    SizeF sizeF = graphics.MeasureString(tempNBHDListmaxLenght, this.NeighborhoodComboBox.Font);
                    float preferredWidth = sizeF.Width;
                    //// Setting the NeighborhoodComboBox's DropDownWidth
                    if (preferredWidth > this.NeighborhoodComboBox.Width)
                    {
                        //// Checking the Number of rows in the ListNeighborhoodType table with the Combo MaxDropDownItems
                        if (this.listNeighborhoodTypeDataTable.Count > this.NeighborhoodComboBox.MaxDropDownItems)
                        {
                            //// If ListNeighborhoodType table has greater than the MaxDropDownItems in NeighborhoodComboBox then "15" increased to show the scroll bar
                            this.NeighborhoodComboBox.DropDownWidth = Convert.ToInt32(preferredWidth) + 15;
                        }
                        else
                        {
                            //// If ListNeighborhoodType table has smaller than the MaxDropDownItems in NeighborhoodComboBox then the preferredWith is assigned
                            this.NeighborhoodComboBox.DropDownWidth = Convert.ToInt32(preferredWidth);
                        }
                    }
                    else
                    {
                        //// If preferredWidth is smaller than NeighborhoodComboBox Width then default width is assigned
                        this.NeighborhoodComboBox.DropDownWidth = this.NeighborhoodComboBox.Width;
                    }
                }
                else
                {
                    //// If ListNeighborhoodType count is empty then default width is assigned
                    this.NeighborhoodComboBox.DropDownWidth = this.NeighborhoodComboBox.Width;
                }
            }
            else
            {
                //// If ListNeighborhoodType count is empty then default width is assigned
                this.NeighborhoodComboBox.DropDownWidth = this.NeighborhoodComboBox.Width;
            }
        }

        #region Coding added for the CO : 4796 by malliga
        /// <summary>
        /// Loads the neighborhood COM bo based on roll year.
        /// </summary>
        private void LoadNeighborhoodComBoBasedOnRollYear()
        {
            int temprollyear;

            ////this.permanentCropData.GetCropNBHD.Rows.Clear(); 
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                int.TryParse(this.RollYearTextBox.Text.Trim(), out temprollyear);
                this.permanentCropData = this.form36040Control.WorkItem.F36040_ListCropNeighborhoodType(temprollyear);
                if (this.permanentCropData.GetCropNBHD.Rows.Count > 0)
                {
                    this.NeighborhoodComboBox.DataSource = this.permanentCropData.GetCropNBHD;
                    this.NeighborhoodComboBox.DisplayMember = this.permanentCropData.GetCropNBHD.NBHDListColumn.ColumnName;
                    this.NeighborhoodComboBox.ValueMember = this.permanentCropData.GetCropNBHD.NBHDIDColumn.ColumnName;
                    this.NeighborhoodComboBox.SelectedIndex = -1;
                    this.NeighborhoodComboBox.Text = string.Empty;
                }
                else
                {
                    this.NeighborhoodComboBox.DataSource = null;
                }
            }
            else
            {
                this.NeighborhoodComboBox.DataSource = null;
            }
        }
        #endregion

        #endregion Private Methods

        #region Events

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36040 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36040_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadWorkSpaces();
                this.LoadCropDetailsGrid();
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Load

        /// <summary>
        /// Handles the MouseHover event of the PermCropPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PermCropPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.PermCorpFormSliceToolTip.SetToolTip(this.PermCropPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PermCropPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PermCropPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the BeforeRowActivate event of the PermanentCropDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.RowEventArgs"/> instance containing the event data.</param>
        private void PermanentCropDataGrid_BeforeRowActivate(object sender, RowEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.PopulatePermanentCropHeaderPartControls(e.Row.Index);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Deletes the permanent crop.
        /// </summary>
        private void DeletePermanentCrop()
        {
            if (this.cropUniqueId > 0)
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int deletedReturnedValue = this.form36040Control.WorkItem.F36040_DeleteCropCatalog(this.cropUniqueId, TerraScanCommon.UserId);

                    ////when the crop is deleted then reload the form
                    if (deletedReturnedValue > 0)
                    {
                        this.LoadCropDetailsGrid();
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F36040DeleteCropCode"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    ////this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Saves the permanent crop.
        /// </summary>
        private void SavePermanentCrop()
        {
            int currentRowIndexValue;
            if (this.SavePermanentCropValues())
            {
                this.PermanentCropDataGrid.Enabled = true;
                this.LoadCropDetailsGrid();
                this.permanentCropGridSource.DataSource = this.cropCatalogDetailsGridDataTable.DefaultView;

                if (this.cropCatalogDetailsGridDataTable.Rows.Count > 0)
                {
                    this.FilterCropCatalogGridRows(this.savedCropRollYear);

                    currentRowIndexValue = this.permanentCropGridSource.Find(this.cropCatalogDetailsGridDataTable.CropVIDColumn.ColumnName, this.savedcropUniqueId);
                    if (currentRowIndexValue >= 0)
                    {
                        this.PopulatePermanentCropHeaderPartControls(currentRowIndexValue);
                        this.PermanentCropDataGrid.Rows[currentRowIndexValue].Selected = true;
                        this.PermanentCropDataGrid.Rows[currentRowIndexValue].Activated = true;
                        this.PermanentCropDataGrid.Focus();
                    }
                }

                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            }
        }

        /// <summary>
        /// Cancels the permanent crop.
        /// </summary>
        private void CancelPermanentCrop()
        {
            this.PermanentCropDataGrid.Enabled = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            this.LoadCropDetailsGrid();
        }

        /// <summary>
        /// News the permanent crop.
        /// </summary>
        private void NewPermanentCrop()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.RollYearTextBox.Text = string.Empty;
            this.CropDetialsHeaderPanel.Enabled = true;
            ////combo box value are loaded based on the Roll year
            this.LoadNeighborhoodComBoBasedOnRollYear();
            this.ControlLock(!this.PermissionFiled.newPermission);
            this.ClearPermanentCropHeaderControls();
            this.RollYearTextBox.Focus();
            this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
        }

        /// <summary>
        /// Handles the TextChangedEvent event of the Controls control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Controls_TextChangedEvent(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Accounts the text leave.
        /// </summary>
        /// <param name="txtValue">The TXT value.</param>
        /// <param name="txtBoxControl">The TXT box control.</param>
        private void AccountTextLeave(string txtValue, TerraScanTextBox txtBoxControl)
        {
            decimal breakvalues = 0;
            //// string txtValue;
            txtValue = txtBoxControl.Text.Trim();
            decimal.TryParse(txtValue, out breakvalues);
            if (breakvalues != 0)
            {
                txtBoxControl.Text = txtValue;
                if (txtValue.IndexOf('.') != -1)
                {
                    // Coding changes for the issue 4224 on 19/5/2009 by Maliga
                    if (txtValue.Substring((txtValue.IndexOf('.') + 1)).Length == 2)
                    {
                        if (txtValue.Substring((txtValue.IndexOf('.') + 1)).ToString().Equals("00"))
                        {
                            txtBoxControl.TextCustomFormat = "###0";
                            txtBoxControl.Text = txtValue.Substring(0, txtValue.IndexOf('.'));
                        }
                        else
                        {
                            txtBoxControl.TextCustomFormat = "###0.00";
                            txtBoxControl.Text = txtValue;
                        }
                    }
                    else
                    {
                        txtBoxControl.TextCustomFormat = "###0.00";
                        txtBoxControl.Text = txtValue;
                    }
                }
                else
                {
                    txtBoxControl.TextCustomFormat = "###0";
                    txtBoxControl.Text = txtValue;
                }
            }
            else
            {
                txtBoxControl.TextCustomFormat = "###0.00";
                txtBoxControl.Text = txtValue;
            }
            // changes ends here
        }

        /// <summary>
        /// Handles the Leave event of the Break2TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break2TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break2TextBox.Text, this.Break2TextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BaseValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseValuePerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.BaseValuePerTextBox.Text, this.BaseValuePerTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break1TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break1TextBox.Text, this.Break1TextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break3TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break3TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break3TextBox.Text, this.Break3TextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break4TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break4TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break4TextBox.Text, this.Break4TextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break5TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break5TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break5TextBox.Text, this.Break5TextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break1ValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break1ValuePerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break1ValuePerTextBox.Text, this.Break1ValuePerTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break2ValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break2ValuePerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break2ValuePerTextBox.Text, this.Break2ValuePerTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break3ValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break3ValuePerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break3ValuePerTextBox.Text, this.Break3ValuePerTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break4ValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break4ValuePerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break4ValuePerTextBox.Text, this.Break4ValuePerTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the Break5ValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Break5ValuePerTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AccountTextLeave(this.Break5ValuePerTextBox.Text, this.Break5ValuePerTextBox);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Coding Added for the issue 1053
        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns>Boolean</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                const int WM_KEYDOWN = 0x100;
                const int WM_SYSKEYDOWN = 0x104;
                if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
                {
                    if (keyData.Equals(Keys.Escape))
                    {
                        this.CancelPermanentCrop();
                    }
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return false;
            }
        }
        #endregion 1053

        #region Coding added for the CO : 4796 by malliga

        /// <summary>
        /// Handles the Leave event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            if (this.rollYearChangeFlag)
            {
                this.LoadNeighborhoodComBoBasedOnRollYear();
                this.SetDropDownWidth();
                this.rollYearChangeFlag = false;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.rollYearChangeFlag = true;
            this.EditEnabled();
        }
     
        /// <summary>
        /// Handles the KeyDown event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_KeyDown(object sender, KeyEventArgs e)
       {
            if (e.KeyValue.Equals(46))
            {
                this.rollYearChangeFlag = true;
                this.EditEnabled();
            }
        }

        /// <summary>
        /// Handles the TextUpdate event of the NeighborhoodComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodComboBox_TextUpdate(object sender, EventArgs e)
        {
            this.EditEnabled();
        }
        #endregion

        /// <summary>
        /// Handles the Validating event of the NeighborhoodComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (this.NeighborhoodComboBox.SelectedValue == null)
            {
                this.NeighborhoodComboBox.Text = string.Empty;  
            }
        }

        /// <summary>
        /// Handles the Validating event of the FruitTreeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void FruitTreeComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (this.FruitTreeComboBox.SelectedValue == null)
            {
                this.FruitTreeComboBox.SelectedValue = 1;
            }
        }
    }
}