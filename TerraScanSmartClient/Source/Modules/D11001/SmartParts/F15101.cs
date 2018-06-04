//--------------------------------------------------------------------------------------------
// <copyright file="F15101.cs" company="Congruent">
//     Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//   This file contains methods for the F15100.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author             Description
// ----------       ---------          ---------------------------------------------------------
//  22 Dec 06       KUPPUSAMY.B         Created
//  18 Aug 09       Sadha Shivudu M     Implemented the TSCO # 2810 
// *********************************************************************************/

namespace D11001
{
    #region NameSpace

    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    #endregion NameSpace

    /// <summary>
    /// F15101 Class
    /// </summary>
    public partial class F15101 : BaseSmartPart
    {
        #region Instance Variables

        /// <summary>
        /// instance variable to hold the constant value for grid maximum rows visible.
        /// </summary>
        private const int MAXROWSVISIBLE = 6;

        /// <summary>
        /// instance variable to hold the constant value for grid minimu rows visible.
        /// </summary>
        private const int MINROWSVISIBLE = 3;

        /// <summary>
        /// instance variable to hold the money max value for validation.
        /// </summary>
        private const decimal MONEYMAXVALUE = 922337203685477.5807M;

        /// <summary>
        /// instance variable to hold the money min value for validation.
        /// </summary>
        private const decimal MONEYMINVALUE = -922337203685477.5808M;

        /// <summary>
        /// instance variable to hold the page mode.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// instance variable to hold the slice permission field object.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// instance variable to hold the form keyId value
        /// </summary>
        private int keyId;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// instance variable to hold the form 15101Controller.
        /// </summary>
        private F15101Controller form15101Control;

        /// <summary>
        /// instance variable to hold the reciept items typed dataset.
        /// </summary>
        private F15101ReceiptItemsData form15101ReceiptItemsData = new F15101ReceiptItemsData();

        /// <summary>
        /// instance variable to hold the reciept header typed dataset.
        /// </summary>
        private F15100ReceiptHeaderData form15100RecieptHeaderData = new F15100ReceiptHeaderData();

        /// <summary>
        /// instance variable to hold the non zero value.
        /// </summary>
        private string nonZero;

        /// <summary>
        /// instance variable to hold the concatinated string.
        /// </summary>
        private string concadinatedString;

        /// <summary>
        /// instance variable to hold the form total amount value when page loads.
        /// </summary>
        private decimal formTotalAmountValue;

        /// <summary>
        /// instance variable to hold the unique transaction id for each row.
        /// </summary>
        private int uniqueTransactionId;

        /// <summary>
        /// Instance for F15100
        /// </summary>
        F15110 f15110;

        #endregion Instance Variables

        #region Formslice Instance Variables

        /// <summary>
        /// instance variable to hold the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  instance variable to hold the red color value
        /// </summary>
        private int redColor;

        /// <summary>
        ///  instance variable to hold the green color value
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  instance variable to hold the blue color value
        /// </summary>
        private int blueColor;

        /// <summary>
        /// instance variable to hold the master form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// instance variable to hold the form master edit permission value
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Formslice Instance Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F15101"/> class.
        /// </summary>
        public F15101()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F15101"/> class.
        /// </summary>
        /// <param name="masterform">The master form number.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F15101(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ReceiptItemsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptItemsPictureBox.Height, this.ReceiptItemsPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Occurs when [form slice_ edit enabled].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Occurs when [form slice_ validation alert].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Occurs when [form slice_ section indicator click].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Occurs when [form slice_ form close alert].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Occurs when [form slice_ resize].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form15101 control.
        /// </summary>
        /// <value>The form15101 control.</value>
        [CreateNew]
        public F15101Controller Form15101Control
        {
            get { return this.form15101Control as F15101Controller; }
            set { this.form15101Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.masterFormNo)
            {
                //SliceResize sliceResize;
                //sliceResize.MasterFormNo = this.masterFormNo;
                //sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                //this.Height = this.ReceiptItemsPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
                //sliceResize.SliceFormHeight = this.ReceiptItemsPictureBox.Height;
                //this.ReceiptItemsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptItemsPictureBox.Height, this.ReceiptItemsPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                //this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SlicePermissionReload&gt;"/> instance containing the event data.</param>
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

                    this.ControlLock(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);

                    if (this.ReceiptItemsGridView.OriginalRowCount > 0)
                    {
                        this.ReceiptItemsGridView.Rows[0].Selected = true;
                    }
                    else
                    {
                        this.ReceiptItemsGridView.Rows[0].Selected = false;
                    }

                    //if (this.keyId != eventArgs.Data.KeyId)
                    //{
                        ////To check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
                       // this.form15100RecieptHeaderData = this.form15101Control.WorkItem.GetReceiptHeaderDetails(eventArgs.Data.KeyId);
                    //}

                    byte isValidRecord = 1;
                    byte.TryParse(this.form15101ReceiptItemsData.ValidRecord.Rows[0][this.form15101ReceiptItemsData.ValidRecord.IsValidColumn].ToString(), out isValidRecord);
                    ////To check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
                    
                    if (isValidRecord > 0) //if (this.form15100RecieptHeaderData.GetReceiptHeader.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SliceReloadActiveRecord&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.PopulateRecieptItemsGrid();
                    this.Height = this.ReceiptItemsPictureBox.Height;
                    this.ReceiptItemsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptItemsPictureBox.Height, this.ReceiptItemsPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    this.ControlLock(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.slicePermissionField.editPermission)
            {
                this.ValidateSliceForm(eventArgs);
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                if (this.slicePermissionField.editPermission)
                {
                    // get the edited rows receipt items xml string
                    string getEditedReceiptItemsXml = this.GetEditedReceiptItemsXml();

                    if (!string.IsNullOrEmpty(getEditedReceiptItemsXml.Trim()))
                    {
                        this.form15101Control.WorkItem.F15101_UpdateTransactionItems(getEditedReceiptItemsXml, TerraScanCommon.UserId);
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.ReceiptItemsGridView.AllowSorting = true;
                }
            }
            else
            {
                // Code commented for issue #6565
                //this.PopulateRecieptItemsGrid();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.PopulateRecieptItemsGrid();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.ReceiptItemsGridView.AllowSorting = true;
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

        #endregion Protected methods

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F15101 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F15101_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeReceiptItemsGrid();
                this.PopulateRecieptItemsGrid();
                this.Height = this.ReceiptItemsPictureBox.Height;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Resize event of the F15101 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F15101_Resize(object sender, EventArgs e)
        {
            // this.Height = this.ReceiptItemsPictureBox.Height;
            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            //this.Height = this.ReceiptItemsPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
            sliceResize.SliceFormHeight = this.ReceiptItemsPictureBox.Height;
            this.ReceiptItemsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptItemsPictureBox.Height, this.ReceiptItemsPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
        }

        /// <summary>
        /// Handles the Click event of the ReceiptItemsPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D11001.F15101"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ReceiptItemsPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptItemsToolTip.SetToolTip(this.ReceiptItemsPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the DisplayLabelToolTip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DisplayLabelToolTip_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label sourceLabel = (Label)sender;
                string tempValue = string.Empty;
                tempValue = sourceLabel.Text;
                Graphics graphics = this.CreateGraphics();
                SizeF sizeF = graphics.MeasureString(tempValue, sourceLabel.Font);
                float preferredwidth = sizeF.Width;

                if (preferredwidth > sourceLabel.Width)
                {
                    this.TotalsTextBoxToolTip.RemoveAll();
                    this.TotalsTextBoxToolTip.SetToolTip(sourceLabel, tempValue);
                }
                else
                {
                    this.TotalsTextBoxToolTip.RemoveAll();
                }

                graphics.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Form Events

        #region Receipt Items GridView Events

        /// <summary>
        /// Handles the CellFormatting event of the ReceiptItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            decimal outDecimal = 0;
            bool overrideAmountColor = false;

            try
            {
                // paint the row with darkRed if the amount value exceeds the maxValue else row with Black color
                if (e.RowIndex >= 0 && e.ColumnIndex.Equals(this.Amount.Index))
                {
                    decimal tempCurrentRowMaxValue;
                    decimal tempCurrentRowMinValue;
                    decimal tempCurrentRowAmountValue;

                    decimal.TryParse(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.Amount.Index].Value.ToString(), out tempCurrentRowAmountValue);
                    decimal.TryParse(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.MaxValue.Index].Value.ToString(), out tempCurrentRowMaxValue);
                    decimal.TryParse(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.MinValue.Index].Value.ToString(), out tempCurrentRowMinValue);

                    if (tempCurrentRowAmountValue < tempCurrentRowMinValue || tempCurrentRowAmountValue > tempCurrentRowMaxValue)
                    {
                        this.ReceiptItemsGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                        overrideAmountColor = true;
                    }
                    else
                    {
                        this.ReceiptItemsGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        overrideAmountColor = false;
                    }
                }

                if (e.ColumnIndex.Equals(this.Amount.Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if desired text is in cell 
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.form15101ReceiptItemsData.ListReceiptItems.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (decimal.TryParse(val, out outDecimal))
                        {
                            string formatestringValue = this.GridAccountValueLeave(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.form15101ReceiptItemsData.ListReceiptItems.AmountColumn.ColumnName.ToString()].Value.ToString().Trim());
                            //string formatestringValue = TerraScanCommon.CustomDecimalFormat(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.form15101ReceiptItemsData.ListReceiptItems.AmountColumn.ColumnName.ToString()].Value.ToString().Trim());

                            if (formatestringValue.Equals("#,##0.0"))
                            {
                                formatestringValue = "#,##0.00";
                            }

                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString(formatestringValue), ")");

                                if (overrideAmountColor)
                                {
                                    e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                                }
                                else
                                {
                                    e.CellStyle.ForeColor = Color.Green;
                                }
                            }
                            else
                            {
                                e.Value = outDecimal.ToString(formatestringValue);
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
                        e.Value = string.Empty;
                    }
                }

                // assign the toolTip text for the grid view columns as maxValue of each row
                if (e.ColumnIndex.Equals(this.Description.Index) || e.ColumnIndex.Equals(this.AccountName.Index)
                    || e.ColumnIndex.Equals(this.ItemType.Index) || e.ColumnIndex.Equals(this.Amount.Index))
                {
                    bool isEmptyRow;

                    // get the current cell
                    DataGridViewCell cell = this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = string.Empty;

                    DataTable dt = ((DataView)this.ReceiptItemsGridView.DataSource).ToTable();

                    if (bool.TryParse(dt.Rows[e.RowIndex][this.ReceiptItemsGridView.EmptyRecordColumnName].ToString(), out isEmptyRow))
                    {
                        if (!isEmptyRow)
                        {
                            decimal tempMaxValue;
                            decimal tempMinValue;
                            string cellToolTipText = string.Empty;

                            if (this.ReceiptItemsGridView[this.MinValue.Index, e.RowIndex].Value != null &&
                                !string.IsNullOrEmpty(this.ReceiptItemsGridView[this.MinValue.Index, e.RowIndex].Value.ToString()))
                            {
                                if (decimal.TryParse(this.ReceiptItemsGridView[this.MinValue.Index, e.RowIndex].Value.ToString(), out tempMinValue))
                                {
                                    cellToolTipText = "MinValue: " + tempMinValue.ToString("#,##0.00") + Environment.NewLine;
                                }
                                else
                                {
                                    cellToolTipText = "MinValue: " + string.Empty + Environment.NewLine;
                                }
                            }
                            else
                            {
                                cellToolTipText = "MinValue: " + string.Empty + Environment.NewLine;
                            }

                            if (this.ReceiptItemsGridView[this.MaxValue.Index, e.RowIndex].Value != null &&
                                !string.IsNullOrEmpty(this.ReceiptItemsGridView[this.MaxValue.Index, e.RowIndex].Value.ToString()))
                            {
                                if (decimal.TryParse(this.ReceiptItemsGridView[this.MaxValue.Index, e.RowIndex].Value.ToString(), out tempMaxValue))
                                {
                                    cellToolTipText += "MaxValue: " + tempMaxValue.ToString("#,##0.00");
                                }
                                else
                                {
                                    cellToolTipText += "MaxValue: " + string.Empty;
                                }
                            }
                            else
                            {
                                cellToolTipText += "MaxValue: " + string.Empty;
                            }

                            cell.ToolTipText = cellToolTipText;
                        }
                        else
                        {
                            cell.ToolTipText = string.Empty;
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
        /// Handles the RowEnter event of the ReceiptItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int.TryParse(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.TransactionID.Index].Value.ToString(), out this.uniqueTransactionId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the ReceiptItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                this.ReceiptItemsGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
                // enable the master form save cancel buttons
                this.EditEnabled();
                this.EditModeDisabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the ReceiptItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                // check for the amount column maches
                if (e.ColumnIndex.Equals(this.Amount.Index))
                {
                    byte isAmountEditable;

                    // check wheather the amount column can be editable by user if it is not posted already
                    if (byte.TryParse(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.form15101ReceiptItemsData.ListReceiptItems.IsEditableColumn.ColumnName].Value.ToString(), out isAmountEditable))
                    {
                        if (isAmountEditable.Equals(0))
                        {
                            e.Cancel = true;
                            this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.form15101ReceiptItemsData.ListReceiptItems.AmountColumn.ColumnName].ReadOnly = true;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.form15101ReceiptItemsData.ListReceiptItems.AmountColumn.ColumnName].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the ReceiptItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                decimal outDecimal;
                decimal lastEditedAmountValue;

                // check for the amount column index
                if (e.ColumnIndex.Equals(this.Amount.Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null)
                    {
                        decimal.TryParse(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.Amount.Index].Value.ToString(), out lastEditedAmountValue);

                        string tempvalue = e.Value.ToString().Trim();
                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "0");
                        }

                        if (decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();
                            tempvalue = tempvalue.Replace("-", string.Empty);

                            if (!tempvalue.Contains("."))
                            {
                                tempvalue = tempvalue.PadLeft(2, '0').Insert(tempvalue.PadLeft(2, '0').Length - 2, ".");
                            }

                            if (outDecimal.ToString().Contains("-"))
                            {
                                outDecimal = decimal.Parse(tempvalue);
                                outDecimal = decimal.Negate(outDecimal);
                            }
                            else
                            {
                                outDecimal = decimal.Parse(tempvalue);
                            }

                            if (outDecimal >= MONEYMINVALUE && outDecimal <= MONEYMAXVALUE)
                            {
                                e.Value = outDecimal;
                                e.ParsingApplied = true;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Value = decimal.Parse(lastEditedAmountValue.ToString());
                                e.ParsingApplied = true;
                                return;
                            }
                        }
                        else
                        {
                            e.Value = decimal.Parse(lastEditedAmountValue.ToString());
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

        /// <summary>
        /// Handles the CellValueChanged event of the ReceiptItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReceiptItemsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal tempCurrentRowAmountValue;
                int currentRowIndex;

                if ((!this.pageLoadStatus) &&
                    (e.RowIndex >= 0) &&
                    decimal.TryParse(this.ReceiptItemsGridView.Rows[e.RowIndex].Cells[this.Amount.Index].Value.ToString(), out tempCurrentRowAmountValue))
                {
                    currentRowIndex = this.GetSelectedReceiptItemRowIndex(this.uniqueTransactionId);
                    this.form15101ReceiptItemsData.ListReceiptItems.Rows[currentRowIndex][IsEdited.HeaderText] = true;
                    string formateStringValue = this.GridAccountValueLeave(tempCurrentRowAmountValue.ToString());
                    //string formateStringValue = TerraScanCommon.CustomDecimalFormat(tempCurrentRowAmountValue.ToString());
                    if (formateStringValue.Equals("#,##0.0"))
                    {
                        formateStringValue = "#,##0.00";
                    }

                    this.form15101ReceiptItemsData.ListReceiptItems.Rows[currentRowIndex][Amount.HeaderText] = tempCurrentRowAmountValue.ToString(formateStringValue);
                    this.form15101ReceiptItemsData.ListReceiptItems.AcceptChanges();

                    // display current amount total value
                    this.DisplayCurrentAmountTotalValue();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Receipt Items GridView Events

        #region Private methods

        /// <summary>
        /// Grids the account value leave.
        /// </summary>
        /// <param name="txtValue">The TXT value.</param>
        /// <returns>the format for the value.</returns>
        private string GridAccountValueLeave(string txtValue)
        {
            int stringLength = txtValue.Length;

            // Get Deimal position of the value
            int decPosition = txtValue.IndexOf(".");
            string customFormat = string.Empty;

            // Get Precision value
            if (decPosition != -1)
            {
                if (stringLength - (decPosition + 1) > 0)
                {
                    txtValue = txtValue.Substring(decPosition + 1, stringLength - (decPosition + 1)).Trim();
                }
            }

            int nonzerocount = 0;

            // Get number of precision in the value
            for (int i = txtValue.Length; i >= 1; i--)
            {
                string arrChar = Convert.ToString(txtValue[i - 1]);
                if (arrChar.Equals("0"))
                {
                    if (nonzerocount >= 1)
                    {
                        nonzerocount++;
                    }
                }
                else
                {
                    nonzerocount++;
                }
            }

            // Based on the precision presents in the textbox value, set the textbox format
            if (decPosition != -1)
            {
                switch (nonzerocount)
                {
                    case 0:
                        customFormat = "#,##0.00";
                        break;
                    case 1:
                        customFormat = "#,##0.00";
                        break;
                    case 2:
                        customFormat = "#,##0.00";
                        break;
                    case 3:
                        customFormat = "#,##0.000";
                        break;
                    case 4:
                        customFormat = "#,##0.0000";
                        break;
                    default:
                        customFormat = "#,##0.0000";
                        break;
                }
            }
            else
            {
                customFormat = "#,##0.0";
            }

            return customFormat;
        }

        /// <summary>
        /// Customizes the receipt items grid.
        /// </summary>
        private void CustomizeReceiptItemsGrid()
        {
            this.ReceiptItemsGridView.AllowUserToResizeColumns = false;
            this.ReceiptItemsGridView.AllowUserToResizeRows = false;
            this.ReceiptItemsGridView.AutoGenerateColumns = false;

            this.TransactionID.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.TransactionIDColumn.ColumnName;
            this.Description.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.DescriptionColumn.ColumnName;
            this.Amount.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.AmountColumn.ColumnName;
            this.ItemType.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.ItemTypeColumn.ColumnName;
            this.AccountName.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.AccountNameColumn.ColumnName;
            this.MaxValue.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.MaxValueColumn.ColumnName;
            this.IsEditable.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.IsEditableColumn.ColumnName;
            this.IsEdited.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.IsEditedColumn.ColumnName;
            this.MinValue.DataPropertyName = this.form15101ReceiptItemsData.ListReceiptItems.MinValueColumn.ColumnName;
        }

        /// <summary>
        /// Populates the reciept items grid.
        /// </summary>
        private void PopulateRecieptItemsGrid()
        {
            this.pageLoadStatus = true;
            this.formTotalAmountValue = 0;

            this.form15101ReceiptItemsData.ListReceiptItems.Clear();
            this.form15101ReceiptItemsData.GetReceiptTotal.Clear();
            //this.form15100RecieptHeaderData.GetReceiptHeader.Clear();

            this.ReceiptItemsGridView.NumRowsVisible = MINROWSVISIBLE;

            // write code to access from db and assign values
            this.form15101ReceiptItemsData = this.form15101Control.WorkItem.ListReceiptItems(this.keyId);

            // assign the datasource to grid
            this.ReceiptItemsGridView.DataSource = this.form15101ReceiptItemsData.ListReceiptItems.DefaultView;

            // to check the invalid key id in set slice event subscription db call is set to GetReceiptHeaderDetails Method to check invalid key id
            //this.form15100RecieptHeaderData = this.form15101Control.WorkItem.GetReceiptHeaderDetails(this.keyId);

            // set the smart part height depends on the number of rows
            this.SetSmartPartHeight(this.ReceiptItemsGridView.OriginalRowCount);

            // display the amount total vlaue
            this.DisplayTotalAmountValue();

            if (this.form15101ReceiptItemsData.ListReceiptItems.Rows.Count > MAXROWSVISIBLE)
            {
                this.ReceiptItemsGridVscrollBar.Visible = false;
            }
            else
            {
                this.ReceiptItemsGridVscrollBar.Visible = true;
            }

            if (this.ReceiptItemsGridView.OriginalRowCount > 0)
            {
                this.ReceiptItemsGridView.Rows[0].Selected = true;
            }
            else
            {
                this.ReceiptItemsGridView.Rows[0].Selected = false;
            }

            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > MINROWSVISIBLE)
            {
                if (recordCount > MAXROWSVISIBLE)
                {
                    recordCount = MAXROWSVISIBLE;
                }

                int increment = (recordCount - MINROWSVISIBLE) * 22;
                this.ReceiptItemsGridView.Height = 87 + increment;
                this.ReceiptItemsGridViewPanel.Height = this.ReceiptItemsGridView.Height + this.FooterPanel.Height - 1;
                this.ReceiptItemsGridVscrollBar.Height = this.ReceiptItemsGridView.Height - 2;
                this.ReceiptItemsPictureBox.Height = this.ReceiptItemsGridViewPanel.Height;
                this.FooterPanel.Top = this.ReceiptItemsGridView.Height - 2;
                this.ReceiptItemsGridView.NumRowsVisible = recordCount;
                this.Height = this.ReceiptItemsGridViewPanel.Height;
            }
            else
            {
                this.ReceiptItemsGridView.Height = 87;
                this.ReceiptItemsGridViewPanel.Height = this.ReceiptItemsGridView.Height + this.FooterPanel.Height - 1;
                this.ReceiptItemsGridVscrollBar.Height = this.ReceiptItemsGridView.Height - 1;
                this.FooterPanel.Top = this.ReceiptItemsGridView.Height - 2;
                this.ReceiptItemsPictureBox.Height = this.ReceiptItemsGridViewPanel.Height;
                this.ReceiptItemsGridView.NumRowsVisible = MINROWSVISIBLE;
                this.Height = this.ReceiptItemsGridViewPanel.Height;
            }
            //OnD9030_F9030_AlertResizableSlice(this, new DataEventArgs<int>(this.masterFormNo));
            //SliceResize sliceResize;
            //sliceResize.MasterFormNo = this.masterFormNo;
            //sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            //this.Height = this.ReceiptItemsPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
            //sliceResize.SliceFormHeight = this.ReceiptItemsPictureBox.Height;
            //this.ReceiptItemsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptItemsPictureBox.Height, this.ReceiptItemsPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            //this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));


        }

        /// <summary>
        /// Displays the total amount value.
        /// </summary>
        private void DisplayTotalAmountValue()
        {
            this.TotalAmountValueLabel.Text = string.Empty;
            this.CurrentTotalValueLabel.Text = string.Empty;

            this.TotalAmountValueLabel.ForeColor = Color.White;
            this.CurrentTotalLabel.ForeColor = Color.White;
            this.CurrentTotalValueLabel.ForeColor = Color.White;

            if (this.form15101ReceiptItemsData.GetReceiptTotal.Rows.Count > 0)
            {
                if (decimal.TryParse(this.form15101ReceiptItemsData.GetReceiptTotal.Rows[0][this.form15101ReceiptItemsData.GetReceiptTotal.TotalAmountColumn.ColumnName].ToString(), out this.formTotalAmountValue))
                {
                    this.TotalAmountValueLabel.Text = this.formTotalAmountValue.ToString("#,##0.00");
                    this.CurrentTotalValueLabel.Text = this.formTotalAmountValue.ToString("#,##0.0000");
                }
            }
        }

        /// <summary>
        /// Displays the current amount total value.
        /// </summary>
        private void DisplayCurrentAmountTotalValue()
        {
            decimal tempCurrentTotalAmountValue;

            this.CurrentTotalValueLabel.Text = string.Empty;

            if (this.form15101ReceiptItemsData.ListReceiptItems.Rows.Count > 0)
            {
                if (decimal.TryParse(this.form15101ReceiptItemsData.ListReceiptItems.Compute("SUM(Amount)", "1=1").ToString(), out tempCurrentTotalAmountValue))
                {
                    this.CurrentTotalValueLabel.Text = tempCurrentTotalAmountValue.ToString("#,##0.0000");

                    if (tempCurrentTotalAmountValue.Equals(this.formTotalAmountValue))
                    {
                        this.CurrentTotalLabel.ForeColor = Color.White;
                        this.CurrentTotalValueLabel.ForeColor = Color.White;
                    }
                    else
                    {
                        this.CurrentTotalLabel.ForeColor = Color.FromArgb(218, 151, 146);
                        this.CurrentTotalValueLabel.ForeColor = Color.FromArgb(218, 151, 146);
                    }
                }
            }
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>the status of the slice validation fileds.</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            decimal currentAmountValue;
            decimal.TryParse(this.CurrentTotalValueLabel.Text.Replace(",", string.Empty), out currentAmountValue);

            if (!currentAmountValue.Equals(this.formTotalAmountValue))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F15101InvalidTotalAndCurrentMessage"), SharedFunctions.GetResourceString("F15101InvalidTotalAndCurrentMessageTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                return sliceValidationFields;
            }

            // filter condition to check the wheather each rows amount exceeds the max value
            string filterCondtions = "Amount < MinValue OR Amount > MaxValue";
            DataRow[] filteredRows = this.form15101ReceiptItemsData.ListReceiptItems.Select(filterCondtions);

            if (filteredRows.Length > 0)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F15101InvalidItemAmountsMessage"), SharedFunctions.GetResourceString("F15101InvalidItemAmountsMessageTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Edits the enabled.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ReceiptItemsGridView.AllowSorting = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLook">if set to <c>true</c> [control look].</param>
        private void ControlLock(bool controlLook)
        {
            this.ReceiptItemsGridView.Enabled = controlLook;
        }

        /// <summary>
        /// Gets the index of the selected receipt item row.
        /// </summary>
        /// <param name="transactionId">The transaction id.</param>
        /// <returns>the selected row index.</returns>
        private int GetSelectedReceiptItemRowIndex(int transactionId)
        {
            int tempIndex = this.form15101ReceiptItemsData.ListReceiptItems.Rows.Count;
            DataTable tempDataTable = this.form15101ReceiptItemsData.ListReceiptItems.Copy();
            tempDataTable.DefaultView.RowFilter = this.form15101ReceiptItemsData.ListReceiptItems.TransactionIDColumn.ColumnName + " = " + transactionId.ToString();

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
            }

            return tempIndex;
        }
        /// <summary>
        /// deActivates the payment button in Master form according to the conditions specified.
        /// </summary>
        private void EditModeDisabled()
        {
            try
            {
                var f15110s = this.ParentForm.Controls.Find("F15110", true);
                if (f15110s.Length > 0)
                {
                    f15110 = (F15110)f15110s[0];
                }
                if (f15110 != null)
                {
                    f15110.DisableManagePaymentButton();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Gets the edited receipt items XML.
        /// </summary>
        /// <returns>the edited receipt items xml string.</returns>
        private string GetEditedReceiptItemsXml()
        {
            string editedReceiptItemsXml = string.Empty;
            F15101ReceiptItemsData selectedReceiptItemsDataSet = new F15101ReceiptItemsData();

            string filterCondition = this.form15101ReceiptItemsData.ListReceiptItems.IsEditedColumn.ColumnName + "=" + bool.TrueString;
            this.form15101ReceiptItemsData.ListReceiptItems.AcceptChanges();
            DataRow[] selectedReceiptItemRows = this.form15101ReceiptItemsData.ListReceiptItems.Select(filterCondition);

            if (selectedReceiptItemRows.Length > 0)
            {
                selectedReceiptItemsDataSet.Merge(selectedReceiptItemRows);
                selectedReceiptItemsDataSet.ListReceiptItems.Columns.Remove(this.form15101ReceiptItemsData.ListReceiptItems.IsEditedColumn.ColumnName);
                editedReceiptItemsXml = TerraScanCommon.GetXmlString(selectedReceiptItemsDataSet.ListReceiptItems);
            }

            return editedReceiptItemsXml;
        }

        #endregion Private methods
    }
}
