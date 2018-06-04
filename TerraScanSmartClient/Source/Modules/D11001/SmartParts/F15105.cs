using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace D11001
{
    #region NameSpace

    using System;
    using System.Data;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using System.Drawing;

    #endregion NameSpace

    public partial class F15105 : BaseSmartPart
    {
        /// <summary>
        /// instance variable to hold the constant value for grid maximum rows visible.
        /// </summary>
        private const int MAXROWSVISIBLE = 7;

        /// <summary>
        /// instance variable to hold the constant value for grid minimu rows visible.
        /// </summary>
        private const int MINROWSVISIBLE = 7;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// variable holds the AssociatedReceipts rows count.
        /// </summary>
        private int associateReceiptsRow;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// instance variable to hold the unique receipt id for each row.
        /// </summary>
        private int uniqueReceiptId;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Instance for F15100Controller
        /// </summary>
        private F15105Controller form15105control;

        /// <summary>
        /// instance variable to hold the reciept values dataset.
        /// </summary>
        private F15100ReceiptHeaderData form15100ReceiptHeaderData;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// recordCount Local variable.
        /// </summary>
        private int recordCount;

        ///// <summary>
        ///// Assigning Empty to parentWorkItem
        ///// </summary>
        //private WorkItem parentWorkItem;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;

        ///<summary>
        /// Editable in Receipt Grid View
        /// </summary>
        private bool isEdit;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private string sectionIndicatorTabText;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int redColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// flag to identify form load
        /// </summary>
        private bool flagFormLoad;

        #region Constructor
        /// <summary>
        /// Instance
        /// </summary>
        public F15105()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// Creates the instance for F15105 class.
        /// </summary>
        /// <param name="masterForm"></param>
        /// <param name="formNo"></param>
        /// <param name="keyId"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="tabText"></param>
        /// <param name="permissionEdit"></param>
        /// <param name="featureClassId"></param>
        public F15105(int masterForm, int formNo, int keyId, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterForm;
            this.Tag = formNo;
            this.keyId = keyId;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.AssociatePaymentsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociatePaymentsPictureBox.Height, this.AssociatePaymentsPictureBox.Width, tabText, red, green, blue);

        }

        #endregion Constructor

        #region Property

        ///<summary>
        ///Gets or sets the F15105control
        ///</summary>
        [CreateNew]
        public F15105Controller Form15105Control
        {
            get { return this.form15105control as F15105Controller; }
            set { this.form15105control = value; }
        }

        #endregion Property

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

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


        #region Event Subscription

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

                    this.ControlLock(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);

                    if (this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.Rows.Count > 0)
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
        /// Event Subscription LoadSliceDetails
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.PopulateRecieptDetailsGrid();
                    this.ControlLock(this.slicePermissionField.editPermission && this.formMasterPermissionEdit);
                    this.flagFormLoad = false;
                    this.AssociatePaymentsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociatePaymentsPictureBox.Height, this.AssociatePaymentsPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                    this.Height = this.AssociatePaymentsPictureBox.Height;
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
            this.PopulateRecieptDetailsGrid();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.AssociatedGridView.AllowSorting = true;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            ////khaja removed Administrator verification to fix Bug#6519
            ////if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && TerraScanCommon.Administrator && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.PermissionFiled.editPermission)
            {
                //this.SaveReceiptHeaderReceiptNumber();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            else
            {
                //this.ShowPanel(true);
                this.ControlLock(false);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            //if (this.D9030_F9030_ReloadAfterSave != null)
            //{
            //    this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            //}
        }

        #endregion Protected methods

        /// <summary>
        /// Handles the Resize event of the F15101 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F15101_Resize(object sender, EventArgs e)
        {
            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            //this.Height = this.AssociatePaymentsPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
            sliceResize.SliceFormHeight = this.AssociatePaymentsPictureBox.Height;
            this.AssociatePaymentsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociatePaymentsPictureBox.Height, this.AssociatePaymentsPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
        }

        /// <summary>
        /// Load Initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F15105_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeReceiptDetailsGrid();
                this.PopulateRecieptDetailsGrid();
                //this.Height = this.AssociatePaymentsPictureBox.Height;
                //this.ControlLock(false);
                this.flagFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Customize Receipt Detail Gridview.
        /// </summary>
        private void CustomizeReceiptDetailsGrid()
        {
            form15100ReceiptHeaderData = this.form15105control.WorkItem.GetReceiptListDetails(this.keyId);
            //// assign the datasource to grid
            this.AssociatedGridView.DataSource = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.DefaultView;
            this.AssociatedGridView.AllowUserToResizeColumns = false;
            this.AssociatedGridView.AllowUserToResizeRows = false;
            this.AssociatedGridView.AutoGenerateColumns = false;
            this.ReceiptNaumber.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.ReceiptNumberColumn.ColumnName;
            this.PostName.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.PostNameColumn.ColumnName;
            this.StatementNumber.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.StatementNumberColumn.ColumnName;
            this.Rollyear.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.RollYearColumn.ColumnName;
            this.TotalAmount.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.TotalAmountColumn.ColumnName;
            this.TaxAmount.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.TaxAmountColumn.ColumnName;
            this.Fees.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.FeeAmountColumn.ColumnName;
            this.Interest.DataPropertyName = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.InterestAmountColumn.ColumnName;

            this.AssociatedGridView.Columns["ReceiptNaumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.AssociatedGridView.Columns["PostName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.AssociatedGridView.Columns["StatementNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.AssociatedGridView.Columns["Rollyear"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.AssociatedGridView.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.AssociatedGridView.Columns["TaxAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.AssociatedGridView.Columns["Fees"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.AssociatedGridView.Columns["Interest"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.AssociatedGridView.Columns["Fees"].DefaultCellStyle.Font = new Font("Courier New",8F, FontStyle.Bold);
            this.AssociatedGridView.Columns["Interest"].DefaultCellStyle.Font = new Font("Courier New",8F, FontStyle.Bold);

        }

        /// <summary>
        /// Populates the reciept items grid.
        /// </summary>
        private void PopulateRecieptDetailsGrid()
        {
            this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.Clear();
            this.AssociatedGridView.NumRowsVisible = MINROWSVISIBLE;
            // write code to access from db and assign values
            this.form15100ReceiptHeaderData = this.form15105control.WorkItem.GetReceiptListDetails(this.keyId);
            // assign the datasource to grid
            this.AssociatedGridView.DataSource = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.DefaultView;
            this.SetSmartPartHeight(this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.Rows.Count);
            this.associateReceiptsRow = this.AssociatedGridView.OriginalRowCount;
            if (this.associateReceiptsRow > 7)
            {
                this.AssociateReceiptVscrollBar.Visible = false;
            }
            else
            {
                this.AssociateReceiptVscrollBar.Visible = true;
            }

            if (this.AssociatedGridView.OriginalRowCount > 0)
            {
                this.AssociatedGridView.Rows[0].Selected = true;
            }
            else
            {
                this.AssociatedGridView.Rows[0].Selected = false;
            }

            this.pageLoadStatus = false;

            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.AssociatedGridView.Height;

            if (!this.flagFormLoad)
            {
                ////this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.AssociatePaymentsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociatePaymentsPictureBox.Height, this.AssociatePaymentsPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 7)
            {
                if (recordCount > 7)
                {
                    recordCount = 7;
                }

                int increment = ((recordCount - 3) * 22);
                this.AssociatedGridView.Height = 88 + increment;
                this.AssociatePanel.Height = this.AssociatedGridView.Height;
                this.AssociateReceiptVscrollBar.Height = 88 + increment - 2;
                this.AssociatePaymentsPictureBox.Height = 88 + increment;
                this.AssociatePaymentsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociatePaymentsPictureBox.Height, this.AssociatePaymentsPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                this.AssociatedGridView.NumRowsVisible = recordCount;
                this.Height = this.AssociatePaymentsPictureBox.Height;
            }

            else
            {
                this.AssociatedGridView.Height = 178;
                this.AssociatePanel.Height = this.AssociatedGridView.Height - 1;
                this.AssociateReceiptVscrollBar.Height = this.AssociatedGridView.Height - 1;
                this.AssociatePaymentsPictureBox.Height = this.AssociatedGridView.Height - 1;
                this.AssociatedGridView.NumRowsVisible = 7;
                this.Height = this.AssociatedGridView.Height;
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

        /// <summary>
        /// Associate GridView Cellcontent Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.AssociatedGridView.Columns["ReceiptNaumber"].Index)
                {
                    int i = e.RowIndex;
                    if (i >= 0)
                    {
                        Form associateReceiptsForm = new Form();
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(11001);
                        formInfo.optionalParameters = new object[2];
                        formInfo.optionalParameters[0] = this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.Rows[i][this.form15100ReceiptHeaderData.GetAssociatedReceiptDetails.ReceiptIDColumn];
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
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
        /// GridView Cell Formatting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;

            //// Only paint if desired, formattable column

            if (e.ColumnIndex == this.AssociatedGridView.Columns["TaxAmount"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                //// Only paint if text provided, Only paint if desired text is in cell 
                if (e.Value != null && !String.IsNullOrEmpty(this.AssociatedGridView.Rows[e.RowIndex].Cells["TaxAmount"].Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (Decimal.TryParse(val, out outDecimal))
                    {
                        if (outDecimal.ToString().Contains("-"))
                        {
                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                            e.CellStyle.ForeColor = Color.Green;
                        }
                        else
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
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
                    e.Value = "";
                }
            }

            if (e.ColumnIndex == this.AssociatedGridView.Columns["TotalAmount"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                //// Only paint if text provided, Only paint if desired text is in cell 
                if (e.Value != null && !String.IsNullOrEmpty(this.AssociatedGridView.Rows[e.RowIndex].Cells["TotalAmount"].Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (Decimal.TryParse(val, out outDecimal))
                    {
                        if (outDecimal.ToString().Contains("-"))
                        {
                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                            e.CellStyle.ForeColor = Color.Green;
                        }
                        else
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
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
                    e.Value = "";
                }
            }

            if (e.ColumnIndex == this.AssociatedGridView.Columns["Fees"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                //// Only paint if text provided, Only paint if desired text is in cell 
                if (e.Value != null && !String.IsNullOrEmpty(this.AssociatedGridView.Rows[e.RowIndex].Cells["Fees"].Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (Decimal.TryParse(val, out outDecimal))
                    {
                        if (outDecimal.ToString().Contains("-"))
                        {
                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                            e.CellStyle.ForeColor = Color.Green;
                        }
                        else
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
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
                    e.Value = "";
                }
            }

            if (e.ColumnIndex == this.AssociatedGridView.Columns["Interest"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                //// Only paint if text provided, Only paint if desired text is in cell 
                if (e.Value != null && !String.IsNullOrEmpty(this.AssociatedGridView.Rows[e.RowIndex].Cells["Interest"].Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (Decimal.TryParse(val, out outDecimal))
                    {
                        if (outDecimal.ToString().Contains("-"))
                        {
                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                            e.CellStyle.ForeColor = Color.Green;
                        }
                        else
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
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
                    e.Value = "";
                }
            }
        }

        /// <summary>
        /// Payment Picture Box Click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatePaymentsPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D11001.F15105"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Picture Box Mouse Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatePaymentsPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.AssociateReceiptToolTip.SetToolTip(this.AssociatePaymentsPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Edits the enabled.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.AssociatedGridView.AllowSorting = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLook">if set to <c>true</c> [control look].</param>
        private void ControlLock(bool controlLook)
        {
            this.AssociatedGridView.Enabled = controlLook;
        }

    }

}
