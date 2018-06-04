// --------------------------------------------------------------------------------------------
// <copyright file="F36091.cs" company="Congruent">
//       Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//  This file contains methods F36091 Income Source Approach..
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author            Description
// ----------       ---------          ---------------------------------------------------------
// 20160826         Priyadharshini      Created
// 20170111         Priyadharshini     TSBG - D31091.F36091 Income Approach form - Use Contract Rents bug
// *********************************************************************************/

namespace D31091
{
    #region Namespace

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
    using Infragistics.Win.UltraWinGrid;
    using Infrastructure.Interface;
    using System.Drawing.Drawing2D;

    #endregion Namespace

    /// <summary>            
    /// F36091 class
    /// </summary>
    [SmartPart]
    public partial class F36091 : BaseSmartPart
    {
        #region Instance Variables

        #region Common Instance Variables

        /// <summary>
        /// Instance variable to hold the flag for load on process
        /// </summary>
        private bool flagLoadOnProcess;


        /// <summary>
        /// F36091IncomeApproachData
        /// </summary>
        private F36091IncomeApproachData incomesourceData = new F36091IncomeApproachData();

        /// <summary>
        /// Instance variable to hold the Income Source Items dataset.
        /// </summary>
        private F36091IncomeApproachData incomesourceTypeDataSet=new F36091IncomeApproachData();

        /// <summary>
        /// Instance variable to hold the Income Approach Items dataset.
        /// </summary>
        private F36091IncomeApproachData incomeapproachItemDataSet=new F36091IncomeApproachData();

        /// <summary>
        /// Used to Store Income Approach Data Table
        /// </summary>
        private F36091IncomeApproachData.IncomeSourcesDataTable getincomesourceDataTable = new F36091IncomeApproachData.IncomeSourcesDataTable();
        /// <summary>
        /// Used to Store Income Approach Data Table
        /// </summary>
        private F36091IncomeApproachData.IncomeApproachDetailsDataTable getIncomeApproachDataTable = new F36091IncomeApproachData.IncomeApproachDetailsDataTable();


        /// <summary>
        /// Used to Store Income Source Types Data Table
        /// </summary>
        private F36091IncomeApproachData.IncomeSourceTypesDataTable getincomesourcetypesDataTable = new F36091IncomeApproachData.IncomeSourceTypesDataTable();


        /// <summary>
        /// Usede to store the form slice key id
        /// </summary>
        private int valueSliceId;

        /// <summary>
        /// Instance variable to hold the current form id
        /// </summary>
        private int currentFormId;

        /// <summary>
        /// Used to store Income Source Id
        /// </summary>
        private string tempIncomeSourceId;

        /// <summary>
        /// allowDelete
        /// </summary>
        private bool allowDelete;

        /// <summary>
        /// removeEnablesavecancel
        /// </summary>
        private bool removeEnablesavecancel = false;


        /// <summary>
        /// cancelEnablesavecancel
        /// </summary>
        private bool cancelEnablesavecancel = false;

        /// <summary>
        /// cancelEnablesavecancel
        /// </summary>
        private bool saveCompleted = false;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus=false;

        /// <summary>
        /// pageLoad variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoad = false;

        /// <summary>
        /// variable holds the selectedIncomeSourceIds.
        /// </summary>
        private List<int> selectedIncomeSourceItemIds = new List<int>();

        /// <summary>
        /// variable holds the selected Income Source ids xml string.
        /// </summary>
        private string selectedIncomeSourceItemIdsXml = string.Empty;

        /// <summary>
        /// variable holds the selected Income Source ids xml string.
        /// </summary>
        private string selectedIncomeSourceValueIdXml = string.Empty;

        /// <summary>
        /// Instance variable to hold the form sourcegridRowCount 
        /// </summary>
        private int sourcegridRowCount;

        /// <summary>
        /// To Store CropID
        /// </summary>
        private int incomeApproachItemIdIdValue;


        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// currentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// Used to store the currentColumnIndex
        /// </summary>
        private int currentColumnIndex;

        /// <summary>
        /// Instance variable to hold the page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;


        /// <summary>
        /// Instance variable to hold the form36035Controller
        /// </summary>
        private F36091Controller form36091Control;

        private F36091WorkItem F36091WorkItem;

     

        /// <summary>
        /// selectionchangeflag
        /// </summary>
        private bool selectionchangeflag = false;

        /// <summary>
        /// Instance variable to hold the current selectedRow index.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Instance variable to hold the label foreColor value
        /// </summary>
        private Color standardLabelForeColor = System.Drawing.Color.FromArgb(51, 51, 153);

        /// <summary>
        /// Instance variable to hold the standard textbox fore color value
        /// </summary>
        private Color standardTextBoxForeColor = Color.Black;

        /// <summary>
        /// Instance variable to hold the standard textbox back color value
        /// </summary>
        private Color standardTextBoxBackColor = Color.White;

        /// <summary>
        /// Instance variable to hold the standard pane back color value
        /// </summary>
        private Color standardPanelBackColor = Color.White;

        /// <summary>
        /// Instance variable to hold the disabled textbox fore and back color value
        /// </summary>
        private Color disabledTextBoxForeAndBackColor = Color.LightGray;

        /// <summary>
        /// Instance variable to hold the disabled label fore color value
        /// </summary>
        private Color disabledLabelForeColor = Color.DarkGray;

        /// <summary>
        /// Instance variable to hold the disabled panel back color value
        /// </summary>
        private Color disabledPanelBackColor = Color.LightGray;

        /// <summary>
        /// Instance variable to hold the max money field value
        /// </summary>
        private double maxMoneyFieldValue = 922337203685477.58;

        /// <summary>
        /// Instance variable to hold the min money field value
        /// </summary>
        private double minMoneyFieldValue = -922337203685477.58;

        /// <summary>
        /// Instance varaible to hold the base market field max value
        /// </summary>
        private double maxBaseMarketFieldValue = 999999999.99;

        /// <summary>
        /// Instance varaible to hold the base market field min value
        /// </summary>
        private double minBaseMarketFieldValue = -999999999.99;

        /// <summary>
        /// Instance variable to hold influenceGridRowIndex;
        /// </summary>
        private int influencerowindex;

        /// <summary>
        /// Instance variable to hold influenceGridColumnIndex;
        /// </summary>
        private int influencecolumnindex;

        /// <summary>
        /// deleteValidation
        /// </summary>
        private bool deleteValidation;

        /// <summary>
        /// Source
        /// </summary>
        private string SourceCode;

        /// <summary>
        /// Source
        /// </summary>
        private int IncomeSourceIDValueMember;

        /// <summary>
        /// 
        /// </summary>
        System.Windows.Forms.ToolTip tiptol = new System.Windows.Forms.ToolTip();

        /// <summary>
        /// 
        /// </summary>
        private bool _tooltipVisible;
        /// <summary>
        /// 
        /// </summary>
        private bool _dropDownOpen;

        #endregion Common Instance Variables

        #region Form Slice Variables

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// instance variable to hold the form master edit permission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Instance Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F36035"/> class.
        /// </summary>
        public F36091()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F36035"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36091(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.currentFormId = formNo;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;

            this.valueSliceId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.IncomeAppPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.IncomeAppPictureBox.Height, this.IncomeAppPictureBox.Width, "Income", this.redColor, this.greenColor, this.blueColor);
            this.form36091Control = new F36091Controller();
            this.F36091WorkItem = new F36091WorkItem();
            this.incomesourceData = new F36091IncomeApproachData();
            this.tblIncomeApproach1.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);
            this.tblIncomeApproach2.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel2_CellPaint);
            this.tblIncomeApproach3.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel3_CellPaint);
            this.tblIncomeApproach4.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel4_CellPaint);
        }

        

        #endregion Constructor

        void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 || e.Row == 5)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }

            if (e.Column == 0 || e.Column == 7)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }

            if (e.Column == 4)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Black, r);
            }

            //var panel = sender as TableLayoutPanel;
            //e.Graphics.SmoothingMode = SmoothingMode.Default;
            //var rectangle = e.CellBounds;
            //using (var pen = new Pen(Color.Black, 1))
            //{
            //    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
            //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            //    if (e.Row == (panel.RowCount - 1))
            //    {
            //        rectangle.Height -= 1;
            //    }

            //    if (e.Column == (panel.ColumnCount - 1))
            //    {
            //        rectangle.Width -= 1;
            //    }

            //    e.Graphics.DrawRectangle(pen, rectangle);
            //}
        }

        void tableLayoutPanel2_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }

            if (e.Column == 0 || e.Column == 5)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }

            if (e.Column == 3)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Black, r);
            }
        }

        void tableLayoutPanel3_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 || e.Row == 3)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }

            if (e.Column == 0 || e.Column == 4)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }
        }

        void tableLayoutPanel4_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 1)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }

            if (e.Column == 0 || e.Column == 4)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Gray, r);
            }
        }


        #region Event Publication


        /// <summary>
        /// Occurs when [D35000_ F35002_ sub form save].
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        ///// <summary>
        ///// Declare the event FormSlice_EditEnabled        
        ///// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Occurs when [form slice_ resize].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

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
        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordModeAfterDelete, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceNullRecordModeEventArgs>> FormSlice_NullRecordModeAfterDelete;
        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// Enumerator for Adjustment Types
        /// </summary>
        private enum AdjustmentTypes
        {
            /// <summary>
            /// Value for None
            /// </summary>
            None = 0,

            /// <summary>
            /// Value for Alternate Land Code
            /// </summary>
            AlternateLandCode = 1,

            /// <summary>
            /// Value for Factor
            /// </summary>
            Factor = 2,

            /// <summary>
            /// Value for Unit
            /// </summary>
            UnitValue = 3,

            /// <summary>
            /// Value for Production
            /// </summary>
            Production = 4,

            /// <summary>
            /// Value for Additive
            /// </summary>
            Additive = 5,

            ////Added by Biju on 01-Dec-2010 to implement #9328
            /// <summary>
            /// Value for Total Value
            /// </summary>
            TotalValue = 6
        }

        /// <summary>
        /// Enumerator for Shape Types.
        /// </summary>
        private enum ShapeTypes
        {
            /// <summary>
            /// Value for Rectangular
            /// </summary>
            Rectangular = 0,

            /// <summary>
            /// Value for Irregular
            /// </summary>
            Irregular = 1
        }

        #endregion Enum

        #region Sub form Events
        /// <summary>
        /// Usde to alert the value slice header
        /// </summary>
        private void AlertValueSliceHeader()
        {
            // Update Appraisal Summary Table

            decimal resultAmount;
            Decimal.TryParse(this.txtIncomeApproachRate.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);
            F35002SubFormSaveEventArgs subFormSaveEventArgs;
            subFormSaveEventArgs.type = 5;
            subFormSaveEventArgs.value = resultAmount;
            subFormSaveEventArgs.valueSliceId = this.valueSliceId;
            subFormSaveEventArgs.amount = resultAmount;
            this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
        }
        #endregion Sub form Events

        #region Property

        /// <summary>
        /// Gets or sets the form36035 control.
        /// </summary>
        /// <value>The form36035 control.</value>
        [CreateNew]
        public F36091Controller Form36091Control
        {
            get { return this.form36091Control as F36091Controller; }
            set { this.form36091Control = value; }
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
            }
        }

       

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            // here form master save is not used but we are using this Event subscription to update the value slice header form slice
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            this.AlertValueSliceHeader();
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            //if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                decimal vacancycollectionloss = 0;
                decimal marketcollection = 0;
                decimal InsuranceAmount = 0;
                decimal marketPercent = 0;
                decimal managementpayroll = 0;
                decimal managementpayrollmarket = 0;
                decimal contractutilities = 0;
                decimal marketutilities = 0;
                decimal maintanenceAmt = 0;
                decimal maintanenceMarket = 0;
                decimal reservesAmt = 0;
                decimal reservesMarket = 0;
                decimal capitalizationrate = 0;
                decimal incomeapproach = 0;
                decimal SuppliesContractValue = 0;
                decimal SuppliesMarketPercent = 0;
                decimal OtherExpensesContractValue = 0;
                decimal OtherExpensesMarketPercent = 0;
                decimal MiscIncomeContractValue = 0;
                decimal MiscIncomeMarketPercent = 0;
                decimal PersonalPropertyValue = 0;

                F36091IncomeApproachData IncomeApproachDetails = new F36091IncomeApproachData();
                F36091IncomeApproachData.IncomeApproachRow IncomeAppraochSourceRow = IncomeApproachDetails.IncomeApproach.NewIncomeApproachRow();

                if (this.txtContractAmt_VacancyandCollectionLoss.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_VacancyandCollectionLoss.Text, out vacancycollectionloss);
                    IncomeAppraochSourceRow.VacAndCollContractValue = vacancycollectionloss;
                }
                else
                {
                    IncomeAppraochSourceRow.VacAndCollContractValue = vacancycollectionloss;
                }

                if (this.txtMarket_VacancyandCollectionLoss.Text != string.Empty)
                {
                    double dmarketcollection = 0;
                    double.TryParse(this.txtMarket_VacancyandCollectionLoss.Text.Replace("%", string.Empty).ToString(), out dmarketcollection);
                    dmarketcollection = dmarketcollection * 0.01;
                    marketcollection = Convert.ToDecimal(dmarketcollection);
                    IncomeAppraochSourceRow.VacAndCollMarketPercent = marketcollection;
                }
                else
                {
                    IncomeAppraochSourceRow.VacAndCollMarketPercent = marketcollection;
                }

                if (this.txtContractAmt_Insurance.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_Insurance.Text, out InsuranceAmount);
                    IncomeAppraochSourceRow.InsuranceContractValue = InsuranceAmount;
                }
                else
                {
                    IncomeAppraochSourceRow.InsuranceContractValue = InsuranceAmount;
                }

                if (this.txtMarket_Insurance.Text != string.Empty)
                {
                    double dmarketPercent = 0;
                    double.TryParse(this.txtMarket_Insurance.Text.Replace("%", string.Empty).ToString(), out dmarketPercent);
                    dmarketPercent = dmarketPercent * 0.01;
                    marketPercent = Convert.ToDecimal(dmarketPercent);
                    IncomeAppraochSourceRow.InsuranceMarketPercent = marketPercent;
                }
                else
                {
                    IncomeAppraochSourceRow.InsuranceMarketPercent = marketPercent;
                }

                if (this.txtContractAmt_ManagementandPayroll.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_ManagementandPayroll.Text, out managementpayroll);
                    IncomeAppraochSourceRow.ManAndPayContractValue = managementpayroll;
                }
                else
                {
                    IncomeAppraochSourceRow.ManAndPayContractValue = managementpayroll;
                }

                if (this.txtMarket_ManagementandPayroll.Text != string.Empty)
                {
                    double dmanagementpayrollmarket = 0;
                    double.TryParse(this.txtMarket_ManagementandPayroll.Text.Replace("%", string.Empty).ToString(), out dmanagementpayrollmarket);
                    dmanagementpayrollmarket = dmanagementpayrollmarket * 0.01;
                    managementpayrollmarket = Convert.ToDecimal(dmanagementpayrollmarket);
                    IncomeAppraochSourceRow.ManAndPayMarketPercent = managementpayrollmarket;
                }
                else
                {
                    IncomeAppraochSourceRow.ManAndPayMarketPercent = managementpayrollmarket;
                }

                if (this.txtContractAmt_Utilities.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_Utilities.Text, out contractutilities);
                    IncomeAppraochSourceRow.UtilitiesContractValue = contractutilities;
                }
                else
                {
                    IncomeAppraochSourceRow.UtilitiesContractValue = contractutilities;
                }

                if (this.txtMarket_Utilities.Text != string.Empty)
                {
                    double dmarketutilities = 0;
                    double.TryParse(this.txtMarket_Utilities.Text.Replace("%", string.Empty).ToString(), out dmarketutilities);
                    dmarketutilities = dmarketutilities * 0.01;
                    marketutilities = Convert.ToDecimal(dmarketutilities);
                    IncomeAppraochSourceRow.UtilitiesMarketPercent = marketutilities;
                }
                else
                {
                    IncomeAppraochSourceRow.UtilitiesMarketPercent = marketutilities;
                }

                if (this.txtContractAmt_MaintainceandRepairs.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_MaintainceandRepairs.Text, out maintanenceAmt);
                    IncomeAppraochSourceRow.MaintAndRepContractValue = maintanenceAmt;
                }
                else
                {
                    IncomeAppraochSourceRow.MaintAndRepContractValue = maintanenceAmt;
                }

                if (this.txtMarket_MaintainceandRepairs.Text != string.Empty)
                {
                    double dmaintanenceMarket = 0;
                    double.TryParse(this.txtMarket_MaintainceandRepairs.Text.Replace("%", string.Empty).ToString(), out dmaintanenceMarket);
                    dmaintanenceMarket = dmaintanenceMarket * 0.01;
                    maintanenceMarket = Convert.ToDecimal(dmaintanenceMarket);
                    IncomeAppraochSourceRow.MaintAndRepMarketPercent = maintanenceMarket;
                }
                else
                {
                    IncomeAppraochSourceRow.MaintAndRepMarketPercent = maintanenceMarket;
                }

                if (this.txtContractAmt_ReservesforReplacement.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_ReservesforReplacement.Text, out reservesAmt);
                    IncomeAppraochSourceRow.ResReplaceContractValue = reservesAmt;
                }
                else
                {
                    IncomeAppraochSourceRow.ResReplaceContractValue = reservesAmt;
                }

                if (this.txtMarket_ReservesforReplacement.Text != string.Empty)
                {
                    double dreservesMarket = 0;
                    double.TryParse(this.txtMarket_ReservesforReplacement.Text.Replace("%", string.Empty).ToString(), out dreservesMarket);
                    dreservesMarket = dreservesMarket * 0.01;
                    reservesMarket = Convert.ToDecimal(dreservesMarket);
                    IncomeAppraochSourceRow.ResReplaceMarketPercent = reservesMarket;
                }
                else
                {
                    IncomeAppraochSourceRow.ResReplaceMarketPercent = reservesMarket;
                }

                if (this.txtCapitalizationRate.Text != string.Empty)
                {
                    double dcapitalizationrate = 0;
                    double.TryParse(this.txtCapitalizationRate.Text.Replace("%", string.Empty).ToString(), out dcapitalizationrate);
                    dcapitalizationrate = dcapitalizationrate * 0.01;
                    capitalizationrate = Convert.ToDecimal(dcapitalizationrate);
                    IncomeAppraochSourceRow.CapitalizationRate = capitalizationrate;
                }
                else
                {
                    IncomeAppraochSourceRow.CapitalizationRate = capitalizationrate;
                }

                IncomeAppraochSourceRow.IsUseContract = chkUseContractRents.Checked;

                if (this.txtIncomeApproachRate.Text != string.Empty)
                {
                    decimal.TryParse(this.txtIncomeApproachRate.Text, out incomeapproach);
                    IncomeAppraochSourceRow.IncomeApproach = incomeapproach;
                }
                else
                {
                    IncomeAppraochSourceRow.IncomeApproach = incomeapproach;
                }
                //
                if (this.txtSuppliesContractAmt.Text != string.Empty)
                {
                    decimal.TryParse(this.txtSuppliesContractAmt.Text, out SuppliesContractValue);
                    IncomeAppraochSourceRow.SuppliesContractValue = SuppliesContractValue;
                }
                else
                {
                    IncomeAppraochSourceRow.SuppliesContractValue = SuppliesContractValue;
                }

                if (this.txtSuppliesMarketPer.Text != string.Empty)
                {
                    double dContractSuppliesper = 0;
                    double.TryParse(this.txtSuppliesMarketPer.Text.Replace("%", string.Empty).ToString(), out dContractSuppliesper);
                    SuppliesMarketPercent = Convert.ToDecimal(dContractSuppliesper * 0.01);
                    IncomeAppraochSourceRow.SuppliesMarketPercent = SuppliesMarketPercent;
                }
                else
                {
                    IncomeAppraochSourceRow.SuppliesMarketPercent = SuppliesMarketPercent;
                }

                if (this.txtOthersContractAmt.Text != string.Empty)
                {
                    decimal.TryParse(this.txtOthersContractAmt.Text, out OtherExpensesContractValue);
                    IncomeAppraochSourceRow.OtherExpensesContractValue = OtherExpensesContractValue;
                }
                else
                {
                    IncomeAppraochSourceRow.OtherExpensesContractValue = OtherExpensesContractValue;
                }
                if (this.txtOthersMarketPer.Text != string.Empty)
                {
                    double dOthersMarketPer = 0;
                    double.TryParse(this.txtOthersMarketPer.Text.Replace("%", string.Empty).ToString(), out dOthersMarketPer);
                    OtherExpensesMarketPercent = Convert.ToDecimal(dOthersMarketPer * 0.01);
                    IncomeAppraochSourceRow.OtherExpensesMarketPercent = OtherExpensesMarketPercent;
                }
                else
                {
                    IncomeAppraochSourceRow.OtherExpensesMarketPercent = OtherExpensesMarketPercent;
                }
                //TSCO - Income Approach - New Misc Income and Personal Property fields
                if (this.text_MiscIncomeContractValue.Text != string.Empty)
                {
                    decimal.TryParse(this.text_MiscIncomeContractValue.Text, out MiscIncomeContractValue);
                    IncomeAppraochSourceRow.MiscIncomeContractValue = MiscIncomeContractValue;
                }
                else
                {
                    IncomeAppraochSourceRow.MiscIncomeContractValue = MiscIncomeContractValue;
                }

                if (this.text_MiscIncomeMarketPercent.Text != string.Empty)
                {
                    double dMiscImproveMarketPer = 0;
                    double.TryParse(this.text_MiscIncomeMarketPercent.Text.Replace("%", string.Empty).ToString(), out dMiscImproveMarketPer);
                    MiscIncomeMarketPercent = Convert.ToDecimal(dMiscImproveMarketPer * 0.01);
                    IncomeAppraochSourceRow.MiscIncomeMarketPercent = MiscIncomeMarketPercent;
                }
                else
                {
                    IncomeAppraochSourceRow.MiscIncomeMarketPercent = MiscIncomeMarketPercent;
                }

                if (this.textPersonalProperty_MarketAmt.Text != string.Empty)
                {
                    decimal.TryParse(this.textPersonalProperty_MarketAmt.Text, out PersonalPropertyValue);
                    IncomeAppraochSourceRow.PersonalProperty = PersonalPropertyValue;
                }
                else
                {
                    IncomeAppraochSourceRow.PersonalProperty = PersonalPropertyValue;
                }

                IncomeApproachDetails.IncomeApproach.Rows.Add(IncomeAppraochSourceRow);
                IncomeApproachDetails.IncomeApproach.AcceptChanges();
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(IncomeApproachDetails.IncomeApproach.Copy());
                tempDataSet.Tables[0].TableName = "IncomeApproach";
                string IncomeApproachvaluexml = tempDataSet.GetXml();
                //Save Grid Details

                DataSet gridDataSet = new DataSet("IncomeApproachItems");
                gridDataSet.Tables.Add(getincomesourceDataTable.Copy());
                gridDataSet.Tables[0].TableName = "IncomeApproachItem";
                DataTable dtNew = gridDataSet.Tables[0].Copy();
                dtNew.Columns.Remove("EmptyRecord$");
                dtNew.Columns.Remove("Source");
                DataSet newIncomeAppraoch = new DataSet("IncomeApproachItems");
                newIncomeAppraoch.Tables.Add(dtNew);
                newIncomeAppraoch.Tables[0].TableName = "IncomeApproachItem";

                for (int i = newIncomeAppraoch.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    if (newIncomeAppraoch.Tables[0].Rows[i]["Units"] == DBNull.Value)

                        newIncomeAppraoch.Tables[0].Rows[i].Delete();
                }

                newIncomeAppraoch.Tables[0].AcceptChanges();
                string IncomeApproachItemxml = newIncomeAppraoch.GetXml();
                string xml = "<Root>" + IncomeApproachItemxml + "</Root>";
                this.form36091Control.WorkItem.F36091_SaveIncomeSourceDetails(this.valueSliceId, xml, IncomeApproachvaluexml, TerraScanCommon.UserId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SelectAllCheckBox.Enabled = true;
               // this.saveCompleted = true;
            }
            else
            {
                SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(Convert.ToInt32(this.valueSliceId));
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                SliceReloadActiveRecord sliceReloadActiveRecord;
                sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.valueSliceId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SelectAllCheckBox.Enabled = true;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;Infrastructure.Interface.AlertSliceOnClose&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                //SliceReloadActiveRecord sliceRecord;
                //sliceRecord.MasterFormNo = 30000;
                //sliceRecord.SelectedKeyId = this.parcelIdVal;
                //this.D9030_F9030_LoadSliceDetails(this, new DataEventArgs<SliceReloadActiveRecord>(sliceRecord));
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
                this.CustomizeIncomeSourcesGridView();
                this.LoadSourceTypesDetails();
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
            this.ClearControl();
            this.CustomizeIncomeSourcesGridView();
            this.LoadSourceTypesDetails();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }
        /// <summary>
        /// Clearing Controls
        /// </summary>
        private void ClearControl()
        {
            try
            {
                this.cancelEnablesavecancel = true;
                this.IncomeGridView.DataSource = null;
                this.txtContractAmt_PotentialGrossIncome.Text = string.Empty;
                this.txtMarketAmt_PotentialGrossIncome.Text = string.Empty;
                this.txtContract_VacancyandCollectionLoss.Text = string.Empty;
                this.txtContractAmt_VacancyandCollectionLoss.Text = string.Empty;
                this.txtMarket_VacancyandCollectionLoss.Text = string.Empty;
                this.txtMarketAmt_VacancyandCollectionLoss.Text = string.Empty;
                this.txtContractAmt_EffectiveGrossIncome.Text = string.Empty;
                this.txtMarketAmt_EffectiveGrossIncome.Text = string.Empty;
                this.txtContract_Insurance.Text = string.Empty;
                this.txtContractAmt_Insurance.Text = string.Empty;
                this.txtMarket_Insurance.Text = string.Empty;
                this.txtMarketAmt_Insurance.Text = string.Empty;
                this.txtContract_ManagementandPayroll.Text = string.Empty;
                this.txtContractAmt_ManagementandPayroll.Text = string.Empty;
                this.txtMarket_ManagementandPayroll.Text = string.Empty;
                this.txtMarketAmt_ManagementandPayroll.Text = string.Empty;
                this.txtContract_Utilities.Text = string.Empty;
                this.txtContractAmt_Utilities.Text = string.Empty;
                this.txtMarket_Utilities.Text = string.Empty;
                this.txtMarketAmt_Utilities.Text = string.Empty;
                this.txtContract_MaintainceandRepairs.Text = string.Empty;
                this.txtContractAmt_MaintainceandRepairs.Text = string.Empty;
                this.txtMarket_MaintainceandRepairs.Text = string.Empty;
                this.txtMarketAmt_MaintainceandRepairs.Text = string.Empty;
                this.txtContract_ReservesforReplacement.Text = string.Empty;
                this.txtContractAmt_ReservesforReplacement.Text = string.Empty;
                this.txtMarket_ReservesforReplacement.Text = string.Empty;
                this.txtMarketAmt_ReservesforReplacement.Text = string.Empty;
                this.txtContract_TotalExpenses.Text = string.Empty;
                this.txtContractAmt_TotalExpenses.Text = string.Empty;
                this.txtMarket_TotalExpenses.Text = string.Empty;
                this.txtMarketAmt_TotalExpenses.Text = string.Empty;
                this.txtNetOperatingIncome1.Text = string.Empty;
                this.txtNetOperatingIncome2.Text = string.Empty;
                this.txtCapitalizationRate.Text = string.Empty;
                this.txtIncomeApproachRate.Text = string.Empty;

                //TSCO - D31091.F36091 Income Approach form change requests
                this.txtSuppliesContractAmt.Text = string.Empty;
                this.txtSuppliesMarketAmt.Text = string.Empty;
                this.txtSuppliesMarketPer.Text = string.Empty;
                this.txtContractSuppliesPer.Text = string.Empty;
                this.txtOthersContractAmt.Text = string.Empty;
                this.txtOthersMarketPer.Text = string.Empty;
                this.txtOthersContractPer.Text = string.Empty;
                this.txtMarket_OthersAmt.Text = string.Empty;

                //TSCO - Income Approach - New Misc Income and Personal Property fields
                this.text_MiscIncomeContractPercent.Text = string.Empty;
                this.text_MiscIncomeContractValue.Text = string.Empty;
                this.text_MiscIncomeMarketPercent.Text = string.Empty;
                this.text_MiscIncomeMarketValue.Text = string.Empty;
                this.textPersonalProperty_MarketAmt.Text = string.Empty;

                this.chkUseContractRents.Checked = false;
                this.CustomizeIncomeSourcesGridView();
                this.LoadSourceTypesDetails();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.cancelEnablesavecancel = false;
            }
        }


        #endregion Event Subscription

        #region Protected Methods

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

        #endregion Protected Methods

        #region Form Events

        /// <summary>
        /// Handles the Delete buttton click
        /// </summary>
        private void DeleteButtonClick()
        {
          
        }
        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "selected Index  Changed Events In Combo Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.EditEnabled();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.removeEnablesavecancel)
                {
                    this.RemoveButton.Enabled = false;
                    //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.SelectAllCheckBox.Checked = false;
                   this.SelectAllCheckBox.Enabled = false;
                    this.selectedIncomeSourceItemIds = new List<int>();
                    this.ReadOnlyAll("false");
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }
        }

        /// <summary>
        /// Set the Readonly all check box column.
        /// </summary>
        /// <param name="status">The status.</param>
        private void ReadOnlyAll(string status)
        {
            if (this.sourcegridRowCount > 0)
            {
                for (int count = 0; count < this.sourcegridRowCount; count++)
                {
                    this.IncomeGridView.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = status;
                }
            }

        }
        /// <summary>
        /// Handles the Resize event of the F36035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F36035_Resize(object sender, EventArgs e)
        {
            try
            {
                this.Height = this.EntireIncomeFormPanel.Height;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the LandPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void IncomeAppPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the LandPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void IncomeAppPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandFormSliceToolTip.SetToolTip(this.IncomeAppPictureBox, Utility.GetFormNameSpace(this.Name));
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
                SizeF sizeF = graphics.MeasureString(tempValue, this.Font);
                float preferredwidth = sizeF.Width;

                if (preferredwidth > sourceLabel.Width)
                {
                    this.TotalValueToolTip.RemoveAll();
                    this.TotalValueToolTip.SetToolTip(sourceLabel, tempValue);
                }
                else
                {
                    this.TotalValueToolTip.RemoveAll();
                }

                graphics.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Form Events

        #region Common Methods

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            if (this.IncomeGridView.RowCount > 1)
            {
                string filterCondtions = "(((Description IS NULL or Description = '') OR (Units IS NULL or Units < 0) OR (ContractPerUnit IS NULL or ContractPerUnit < 0)))"; //OR (Value IS NULL or Value < 0)))"; // (Value IS NULL or Value < 0)(Acres IS NULL Or Acres = '' Or Acres <= 0.0 
                DataRow[] drfilterCondtions = this.getincomesourceDataTable.Select(filterCondtions);

                if (this.getincomesourceDataTable.Rows.Count > 4)
                {
                    if (drfilterCondtions.Length > 1)
                    {
                        sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                    else
                    {
                        sliceValidationFields.ErrorMessage = string.Empty;
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                }
                else
                {
                    bool saveflag = false;
                    double decValue;
                    for (int i = 0; i < IncomeGridView.RowCount - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.Source.Name].Value.ToString().Trim()))
                        {
                            if (string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.Source.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.Description.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.Units.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.ContractPerUnit.Name].Value.ToString().Trim()))
                            {
                                saveflag = false;
                                break;
                            }
                            else
                            {
                                saveflag = true;
                            }
                        }
                        else
                        {

                            if (string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.Source.Name].Value.ToString().Trim()))
                            {
                                if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.Description.Name].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.Units.Name].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[this.ContractPerUnit.Name].Value.ToString().Trim()))
                                {
                                    saveflag = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (saveflag == false)
                    {
                        sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                    else
                    {
                        sliceValidationFields.ErrorMessage = string.Empty;
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                }
                decimal unitsvalue = 0;
                decimal contractperunit = 0;
                decimal contract = 0;
                decimal marketperunit = 0;
                decimal market = 0;
                for (int i = 0; i < IncomeGridView.RowCount - 1; i++)
                {
                    decimal.TryParse(this.IncomeGridView.Rows[i].Cells[this.Units.Name].Value.ToString().Trim(), out unitsvalue);
                    decimal.TryParse(this.IncomeGridView.Rows[i].Cells[this.ContractPerUnit.Name].Value.ToString().Trim(), out contractperunit);
                    decimal.TryParse(this.IncomeGridView.Rows[i].Cells[this.Contract.Name].Value.ToString().Trim(), out contract);
                    decimal.TryParse(this.IncomeGridView.Rows[i].Cells[this.MarketPerUnit.Name].Value.ToString().Trim(), out marketperunit);
                    decimal.TryParse(this.IncomeGridView.Rows[i].Cells[this.Market.Name].Value.ToString().Trim(), out market);
                   
                }

                double maxmoney = 922337203685477.5807;
                if (Convert.ToDouble(unitsvalue) > maxmoney || Convert.ToDouble(contractperunit) > maxmoney || Convert.ToDouble(contract) > maxmoney || Convert.ToDouble(marketperunit) > maxmoney || Convert.ToDouble(market) > maxmoney)
                {
                    sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("Entered values exceeds max limit."));
                    sliceValidationFields.FormNo = formNo;
                    sliceValidationFields.RequiredFieldMissing = false;
                }
                ////Ends here
            }
            if (string.IsNullOrEmpty(this.txtCapitalizationRate.Text.Trim()))
            {
                this.txtCapitalizationRate.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            //TSBG - D31091.F36091 Income Approach form - Use Contract Rents bug Commented
            //if (this.chkUseContractRents.Checked == false)
            //{
            //    this.chkUseContractRents.Focus();
            //    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
            //    sliceValidationFields.RequiredFieldMissing = true;
            //    return sliceValidationFields;
            //}

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the land fields max limit validation.
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        /// <param name="validatedTextBox">The validated text box.</param>
        /// <param name="flagMoneyValidation">if set to <c>true</c> [flag money validation].</param>
        /// <param name="flagNegativeMoneyValidation">if set to <c>true</c> [flag negative money validation].</param>
        /// <returns>The validation flag value.</returns>
        private bool CheckLandFieldsMaxLimitValidation(Control sourceControl, TerraScanTextBox validatedTextBox, bool flagMoneyValidation, bool flagNegativeMoneyValidation)
        {
            double validatedTextBoxValue;
            double.TryParse(validatedTextBox.DecimalTextBoxValue.ToString(), out validatedTextBoxValue);
            bool valueExceeded = false;

            if (flagMoneyValidation)
            {
                if (!flagNegativeMoneyValidation)
                {
                    if (validatedTextBoxValue > Math.Floor(this.maxMoneyFieldValue))
                    {
                        valueExceeded = true;
                    }
                }
                else
                {
                    if (validatedTextBoxValue < Math.Floor(this.minMoneyFieldValue) || validatedTextBoxValue > Math.Floor(this.maxMoneyFieldValue))
                    {
                        valueExceeded = true;
                    }
                }
            }
            else
            {
                if (validatedTextBoxValue > this.maxBaseMarketFieldValue)
                {
                    valueExceeded = true;
                }
            }

            if (valueExceeded)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                validatedTextBox.Text = decimal.Zero.ToString();

                if (sourceControl.GetType().Equals(typeof(TerraScanTextBox)))
                {
                    sourceControl.Text = decimal.Zero.ToString();
                }

                sourceControl.Focus();
                return true;
            }

            return false;
        }

   
        #endregion Common Methods


        /// <summary>
        /// Customizing IncomeSources Grid 
        /// </summary>
        private void CustomizeIncomeSourcesGridView()
        {
            this.RemoveButton.Enabled = false;
            this.SelectAllCheckBox.Checked = false;
          
            selectedIncomeSourceItemIds = new List<int>();
            this.incomesourceData = this.form36091Control.WorkItem.F36091_ListSourceDetails(this.valueSliceId);
            this.getincomesourcetypesDataTable = this.incomesourceData.IncomeSourceTypes;
            this.IncomeGridView.AutoGenerateColumns = false;
            this.IncomeApproachItemID.DataPropertyName = this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName;
            this.IncomeSourceID.DataPropertyName = this.getincomesourceDataTable.IncomeSourceIDColumn.ColumnName;
            this.Source.DataPropertyName = this.getincomesourceDataTable.SourceColumn.ColumnName; ////Crop Code
            this.Description.DataPropertyName = this.getincomesourceDataTable.DescriptionColumn.ColumnName;
            this.Units.DataPropertyName = this.getincomesourceDataTable.UnitsColumn.ColumnName;
            this.Contract.DataPropertyName = this.getincomesourceDataTable.ContractColumn.ColumnName;
            this.ContractPerUnit.DataPropertyName = this.getincomesourceDataTable.ContractPerUnitColumn.ColumnName;
            this.Market.DataPropertyName = this.getincomesourceDataTable.MarketColumn.ColumnName;
            this.MarketPerUnit.DataPropertyName = this.getincomesourceDataTable.MarketPerUnitColumn.ColumnName;
            this.IncomeGridView.Columns[SharedFunctions.GetResourceString("ValidStatus")].ReadOnly = true;
           
        }

        private void F36091_Load(object sender, EventArgs e)
        {
            try
            {

                this.FlagSliceForm = true;
                this.CustomizeIncomeSourcesGridView();
                this.LoadSourceTypesDetails();
                this.keyField = "IncomeApproachItemID";
                this.formNo = 36091;
                this.pageLoad = true;
               
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
        /// Loading CropGrid 
        /// </summary>
        private void LoadSourceTypesDetails()
        {
            this.incomesourceData = this.form36091Control.WorkItem.F36091_GetIncomeSources(this.valueSliceId);
            this.getincomesourceDataTable = this.incomesourceData.IncomeSources;
            this.getIncomeApproachDataTable = this.incomesourceData.IncomeApproachDetails;
            sourcegridRowCount = this.incomesourceData.IncomeSources.Rows.Count;

            ////to custmize combo box in grid

            this.IncomeGridView.Columns["IncomeSourceID"].DataPropertyName = this.getincomesourcetypesDataTable.IncomeSourceIDColumn.ColumnName;


            ////to custmize combo box in grid
            (this.Source as DataGridViewComboBoxColumn).DataSource = this.getincomesourcetypesDataTable;
            (this.Source as DataGridViewComboBoxColumn).DisplayMember = this.getincomesourcetypesDataTable.SourceCodeColumn.ColumnName;
            (this.Source as DataGridViewComboBoxColumn).ValueMember = this.getincomesourcetypesDataTable.SourceCodeColumn.ColumnName;

            this.SelectAllCheckBox.Enabled = true;

            int emptyRows;
            if (sourcegridRowCount > 0)
            {
                ////if the no of visiable rows(grid) is greater than the actual rows from the Datatable ---
                //// --- then a temp datatable with empty rows are merged with getCropdetailsDataTable datatable                    
                if (this.IncomeGridView.NumRowsVisible > sourcegridRowCount)
                {
                    emptyRows = this.IncomeGridView.NumRowsVisible - sourcegridRowCount;


                    for (int i = 0; i < emptyRows; i++)
                    {
                        this.getincomesourceDataTable.AddIncomeSourcesRow(this.getincomesourceDataTable.NewIncomeSourcesRow());
                    }
                }
                else
                {
                    this.getincomesourceDataTable.AddIncomeSourcesRow(this.getincomesourceDataTable.NewIncomeSourcesRow());
                }

                this.IncomeGridView.DataSource = this.getincomesourceDataTable.DefaultView;

                this.IncomeGridView.Rows[0].Selected = true;

                // For Enabled top most check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                this.SelectAllCheckBox.Enabled = true;

            }
            else
            {
                for (int i = 0; i < this.IncomeGridView.NumRowsVisible; i++)
                {
                    this.getincomesourceDataTable.AddIncomeSourcesRow(this.getincomesourceDataTable.NewIncomeSourcesRow());
                }

                this.IncomeGridView.DataSource = this.getincomesourceDataTable.DefaultView;
                this.IncomeGridView.Rows[0].Selected = false;
                // For Disabled top most check box  modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                this.SelectAllCheckBox.Enabled = false;
            }

            this.IncomeGridView.Enabled = true;
            this.IncomeGridView.AutoGenerateColumns = false;

            /////to enable or disable the vertical scroll bar
            if (this.getincomesourceDataTable.Rows.Count > 4)
            {
                this.SourceVerticalScroll.Visible = false;
            }
            else
            {
                this.SourceVerticalScroll.Visible = true;
            }

            //Binding Income Approach Items
            if (getIncomeApproachDataTable != null)
            {
                if (getIncomeApproachDataTable.Count > 0)
                {
                    this.txtContractAmt_PotentialGrossIncome.Text= ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["PotGrossIncomeContractValue"].ToString());
                    this.txtContractAmt_PotentialGrossIncome.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["PotGrossIncomeContractValue"].ToString());
                    this.txtMarketAmt_PotentialGrossIncome.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["PotGrossIncomeMarketValue"].ToString());
                    this.txtContract_VacancyandCollectionLoss.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["VacAndCollContractPercent"].ToString());
                    this.txtContractAmt_VacancyandCollectionLoss.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["VacAndCollContractValue"].ToString());
                    this.txtMarket_VacancyandCollectionLoss.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["VacAndCollMarketPercent"].ToString());
                    this.txtMarketAmt_VacancyandCollectionLoss.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["VacAndCollMarketValue"].ToString());
                    this.txtContractAmt_EffectiveGrossIncome.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["EffGrossIncomeContractValue"].ToString());
                    this.txtMarketAmt_EffectiveGrossIncome.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["EffGrossIncomeMarketValue"].ToString());
                    this.txtContract_Insurance.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["InsuranceContractPercent"].ToString());
                    this.txtContractAmt_Insurance.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["InsuranceContractValue"].ToString());
                    this.txtMarket_Insurance.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["InsuranceMarketPercent"].ToString());
                    this.txtMarketAmt_Insurance.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["InsuranceMarketValue"].ToString());
                    this.txtContract_ManagementandPayroll.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["ManAndPayContractPercent"].ToString());
                    this.txtContractAmt_ManagementandPayroll.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["ManAndPayContractValue"].ToString());
                    this.txtMarket_ManagementandPayroll.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["ManAndPayMarketPercent"].ToString());
                    this.txtMarketAmt_ManagementandPayroll.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["ManAndPayMarketValue"].ToString());
                    this.txtContract_Utilities.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["UtilitiesContractPercent"].ToString());
                    this.txtContractAmt_Utilities.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["UtilitiesContractValue"].ToString());
                    this.txtMarket_Utilities.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["UtilitiesMarketPercent"].ToString());
                    this.txtMarketAmt_Utilities.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["UtilitiesMarketValue"].ToString());
                    this.txtContract_MaintainceandRepairs.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["MaintAndRepContractPercent"].ToString());
                    this.txtContractAmt_MaintainceandRepairs.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["MaintAndRepContractValue"].ToString());
                    this.txtMarket_MaintainceandRepairs.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["MaintAndRepMarketPercent"].ToString());
                    this.txtMarketAmt_MaintainceandRepairs.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["MaintAndRepMarketValue"].ToString());
                    this.txtContract_ReservesforReplacement.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["ResReplaceContractPercent"].ToString());
                    this.txtContractAmt_ReservesforReplacement.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["ResReplaceContractValue"].ToString());
                    this.txtMarket_ReservesforReplacement.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["ResReplaceMarketPercent"].ToString());
                    this.txtMarketAmt_ReservesforReplacement.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["ResReplaceMarketValue"].ToString());
                    this.txtContract_TotalExpenses.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["TotalExpensesContractPercent"].ToString());
                    this.txtContractAmt_TotalExpenses.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["TotalExpensesContractValue"].ToString());
                    this.txtMarket_TotalExpenses.Text = ContractPercentageFormat(getIncomeApproachDataTable.Rows[0]["TotalExpensesMarketPercent"].ToString());
                    this.txtMarketAmt_TotalExpenses.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["TotalExpensesMarketValue"].ToString());
                    this.txtNetOperatingIncome1.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["NetOpIncContractValue"].ToString());
                    this.txtNetOperatingIncome2.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["NetOpIncMarketValue"].ToString());
                    this.txtCapitalizationRate.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["CapitalizationRate"].ToString());
                    if (getIncomeApproachDataTable.Rows[0]["IsUseContract"] != DBNull.Value && getIncomeApproachDataTable.Rows[0]["IsUseContract"] != null)
                        this.chkUseContractRents.Checked = Convert.ToBoolean(getIncomeApproachDataTable.Rows[0]["IsUseContract"]);
                    this.txtIncomeApproachRate.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["IncomeApproach"].ToString());

                    this.txtSuppliesContractAmt.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["SuppliesContractValue"].ToString());
                    this.txtSuppliesMarketAmt.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["SuppliesMarketValue"].ToString());
                    this.txtSuppliesMarketPer.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["SuppliesMarketPercent"].ToString());
                    this.txtContractSuppliesPer.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["SuppliesContractPercent"].ToString());
                    this.txtOthersContractAmt.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["OtherExpensesContractValue"].ToString());
                    this.txtOthersMarketPer.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["OtherExpensesMarketPercent"].ToString());
                    this.txtOthersContractPer.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["OtherExpensesContractPercent"].ToString());
                    this.txtMarket_OthersAmt.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["OtherExpensesMarketValue"].ToString());

                    this.text_MiscIncomeContractPercent.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["MiscIncomeContractPercent"].ToString());
                    this.text_MiscIncomeContractValue.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["MiscIncomeContractValue"].ToString());
                    this.text_MiscIncomeMarketPercent.Text = ConvertPercentageFormat(getIncomeApproachDataTable.Rows[0]["MiscIncomeMarketPercent"].ToString());
                    this.text_MiscIncomeMarketValue.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["MiscIncomeMarketValue"].ToString());
                    this.textPersonalProperty_MarketAmt.Text = ContractAmountFormat(getIncomeApproachDataTable.Rows[0]["PersonalProperty"].ToString());
                }
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            this.EditEnabled();
        }

        #region Grid Events

        /// <summary>
        /// Handles the CellClick event of the CropDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void IncomeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////Code Added for avoiding Emptyrow
                if (e.ColumnIndex == -1)
                {
                    this.deleteValidation = true;
                }
                else
                {
                    this.deleteValidation = false;
                }

                if (e.RowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.IncomeGridView[this.IncomeApproachItemID.Name, e.RowIndex].Value.ToString().Trim()))
                    {
                        int.TryParse(this.IncomeGridView[this.IncomeApproachItemID.Name, e.RowIndex].Value.ToString().Trim(), out this.incomeApproachItemIdIdValue);
                    }

                    if (e.RowIndex == 0)
                    {
                        this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                        this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                        this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                        this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                      
                    }

                    bool hasValues = false;
                    if (e.RowIndex >= 1)
                    {
                        if ((string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Units.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.ContractPerUnit.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Contract.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Market.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.MarketPerUnit.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                        {
                            if (e.RowIndex + 1 < IncomeGridView.RowCount)
                            {
                                for (int i = e.RowIndex; i < IncomeGridView.RowCount; i++)
                                {
                                    if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[1].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                    {
                                        hasValues = true;
                                        break;
                                    }
                                }

                                if (hasValues)
                                {
                                    this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                                   
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Units.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.ContractPerUnit.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Contract.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.MarketPerUnit.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Market.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                                    {
                                        this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = true;
                                        this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                                        this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = true;
                                        this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                                        this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                        this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                                        this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                                      
                                    }
                                }
                            }
                            else
                            {
                                this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = true;
                                this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                                this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = true;
                                this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = true;
                               
                            }
                        }
                        else
                        {
                            this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                            this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                            this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                            this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                            
                        }
                    }

                    this.currentRowIndex = e.RowIndex;
                    this.currentColumnIndex = e.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the IncomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void IncomeGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                
                //this.selectionchangeflag = false;
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged -= new EventHandler(this.Control_TextChanged);

                ////Coding added for the issue 5670 on 7/4/2009 by malliga
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    if (string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, this.IncomeGridView.CurrentRowIndex].Value.ToString()))
                    {
                        this.SourceCode = string.Empty;
                    }
                    else
                    {
                        this.SourceCode = this.IncomeGridView[this.Source.Name, this.IncomeGridView.CurrentRowIndex].Value.ToString();
                    }

                    

                    if (this.IncomeGridView.CurrentColumnIndex.Equals(2) && !this.IncomeGridView[this.Source.Name, this.IncomeGridView.CurrentRowIndex].ReadOnly
                        && string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, this.IncomeGridView.CurrentRowIndex].Value.ToString()))
                    {

                        ((ComboBox)this.IncomeGridView.EditingControl).SelectedIndex = -1;
                    }
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.F36091_SourceSelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F36091_SourceSelectionChangeCommitted);
                }


                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.MouseClick += new MouseEventHandler(this.Control_MouseClick);
                e.Control.Validated += new EventHandler(this.Control_Validated);

                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    if ((IncomeGridView.CurrentCell.ColumnIndex == 5) || (IncomeGridView.CurrentCell.ColumnIndex == 6))
                    {
                        e.Control.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    try
                    {
                        ComboBox cmb = e.Control as ComboBox;
                        cmb.DrawMode = DrawMode.OwnerDrawFixed;
                        cmb.DrawItem += cmb_DrawItem;
                        cmb.DropDown += OnDropDown;
                        cmb.DropDownClosed += OnDropDownClosed;
                        cmb.MouseLeave += OnMouseLeave;
                    }
                    catch { }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void OnDropDown(object sender, EventArgs e)
        {
            _dropDownOpen = true;
        }

        private void OnDropDownClosed(object sender, EventArgs e)
        {
            _dropDownOpen = false;
            ResetToolTip();
        }

        void cmb_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                string text = string.Empty;
                if (tiptol == null)
                {
                    tiptol = new System.Windows.Forms.ToolTip();
                }
                if (e.Index == -1)
                {
                    return;
                }
                else
                {
                    text = cmb.GetItemText(cmb.Items[e.Index]);
                    e.DrawBackground();
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds.Location, SystemColors.Window);
                        if (_dropDownOpen)
                        {
                            Size szText = TextRenderer.MeasureText(text, cmb.Font);
                            if (szText.Width > cmb.Width && !_tooltipVisible)
                            {
                                if (text != "System.Data.DataRowView")
                                {
                                    ShowToolTip(text, this.PointToClient(MousePosition).X + Cursor.Size.Height, this.PointToClient(MousePosition).Y);
                                }
                            }
                        }
                    }

                    else
                    {
                        ResetToolTip();
                        TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds.Location, cmb.ForeColor);
                    }
                }
                e.DrawFocusRectangle();
            }

            catch (Exception Excep)
            {
            }
        }

        private void ShowToolTip(string text, int x, int y)
        {
            tiptol.Show(text, this, x, y);
            _tooltipVisible = true;
        }

        private void ResetToolTip()
        {
            tiptol.SetToolTip(this, null);
            _tooltipVisible = false;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            ResetToolTip();
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((IncomeGridView.CurrentCell.ColumnIndex == 5) || (IncomeGridView.CurrentCell.ColumnIndex == 6))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = false;
            }
        }

        private void Column1_KeyPressCharAllow(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Handles the CellEndEdit event of the CropDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// 
        private void IncomeGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double maxmoney = 922337203685477.5807;
                if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value.ToString().Trim()))
                {
                    if (Convert.ToDouble(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value) > maxmoney)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value = 0.0;
                    }
                }
                if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.ContractPerUnit.Name].Value.ToString().Trim()))
                {
                    if (Convert.ToDouble(this.IncomeGridView.Rows[e.RowIndex].Cells[this.ContractPerUnit.Name].Value) > maxmoney)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.IncomeGridView.Rows[e.RowIndex].Cells[this.ContractPerUnit.Name].Value = 0.0;
                    }
                }

                if ((e.RowIndex + 1) == this.IncomeGridView.Rows.Count)
                {
                    if ((!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Source.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Description.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.ContractPerUnit.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Contract.Name].Value.ToString().Trim())))
                    {
                        this.getincomesourceDataTable.AddIncomeSourcesRow(this.getincomesourceDataTable.NewIncomeSourcesRow());
                        if (this.IncomeGridView.CurrentCell == null)
                        {
                            this.IncomeGridView.CurrentCell = this.IncomeGridView.Rows[e.RowIndex].Cells[this.Description.Name];
                            this.IncomeGridView.Focus();
                        }
                    }
                }
                this.IncomeGridView.DataSource = this.getincomesourceDataTable.DefaultView;
                if (this.getincomesourceDataTable.Rows.Count > 4)
                {
                    this.SourceVerticalScroll.Visible = false;
                }
                else
                {
                    this.SourceVerticalScroll.Visible = true;
                }
                PopulateDecriptionValue(e);
                if (e.ColumnIndex == this.IncomeGridView.Columns[this.Units.Name].Index
                    || e.ColumnIndex == this.IncomeGridView.Columns[this.Source.Name].Index
                    || e.ColumnIndex == this.IncomeGridView.Columns[this.ContractPerUnit.Name].Index)
                {
                    decimal units = 0;
                    decimal.TryParse(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value.ToString().Trim(), out units);
                    decimal contractperUnit = 0;
                    decimal.TryParse(this.IncomeGridView.Rows[e.RowIndex].Cells[this.ContractPerUnit.Name].Value.ToString().Trim(), out contractperUnit);
                    decimal contract = 0;
                    decimal marketperunit = 0;
                    decimal market = 0;
                    if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.IncomeSourceID.Name].Value.ToString().Trim()))
                    {
                        this.incomeapproachItemDataSet = this.F36091WorkItem.F36091_ListApproachValues(Convert.ToInt32(this.IncomeGridView.Rows[e.RowIndex].Cells[this.IncomeSourceID.Name].Value.ToString().Trim()), units, contractperUnit, out contract, out marketperunit, out market);
                        this.IncomeGridView.Rows[e.RowIndex].Cells[this.MarketPerUnit.Name].Value = marketperunit;
                        this.IncomeGridView.Rows[e.RowIndex].Cells[this.Contract.Name].Value = contract;
                       
                        this.IncomeGridView.Rows[e.RowIndex].Cells[this.Market.Name].Value = market;
                    }
                    if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value.ToString().Trim()))
                    {
                        this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value = units;
                    }
                }

                this.currentRowIndex = e.RowIndex;
                this.currentColumnIndex = e.ColumnIndex;
           

                this.selectionchangeflag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
       
        /// <summary>
        /// Populating the Description Column.
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void PopulateDecriptionValue(int RowIndex)
        {
            try
            {
                if (this.incomeapproachItemDataSet.IncomeApproachItemValues.Count > 0)
                {
                    this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Contract.Name].Value = this.incomeapproachItemDataSet.IncomeApproachItemValues[0]["Contract"].ToString();
                    this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Market.Name].Value = this.incomeapproachItemDataSet.IncomeApproachItemValues[0]["Market"].ToString();
                }
                 
                string fillterCondtion = "SourceCode = '" + this.SourceCode + "'";

                DataRow[] tempCurrentRowValues = this.getincomesourcetypesDataTable.Select(fillterCondtion);

                if (tempCurrentRowValues.Length > 0)
                {
                    if (this.selectionchangeflag)
                    {
                        this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Description.Name].Value = tempCurrentRowValues[0]["Description"].ToString();
                        this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.IncomeSourceID.Name].Value = tempCurrentRowValues[0]["IncomeSourceID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            //}
        }
        /// <summary>
        /// Handles the SourceSelectionChangeCommitted event of the F36041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36091_SourceSelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = (ComboBox)sender;
                this.tempIncomeSourceId = combo.Text;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.tempIncomeSourceId != null && this.tempIncomeSourceId != "")
                {
                    combo.SendToBack();
                    combo.SelectAll();
                    this.selectionchangeflag = true;
                    this.Control_TextChanged(null, null);
                    this.ToEnableEditButtonInMasterForm();
                    ((ComboBox)this.IncomeGridView.EditingControl).SelectionChangeCommitted -= new EventHandler(this.F36091_SourceSelectionChangeCommitted);
                    combo.BringToFront();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Calculates the selected Income sources 
        /// </summary>
        private void CalculateSelectAllIncomeSources(bool isChecked)
        {
            try
            {
                this.IncomeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.sourcegridRowCount; count++)
                {
                    if (isChecked == true)
                    {
                        this.selectedIncomeSourceItemIds.Add(Convert.ToInt32(this.IncomeGridView[this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName, count].Value));
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Calculates the Unselected Income Sources.
        /// </summary>
        private void CalculateUnSelectIncomeSource(bool isChecked)
        {
            try
            {
                this.IncomeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.sourcegridRowCount; count++)
                {
                    if (isChecked == false)
                    {
                        this.selectedIncomeSourceItemIds.Remove(Convert.ToInt32(this.IncomeGridView[this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName, count].Value));
                    }

                }
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
                if (this.IncomeGridView.EditingControl != null)
                {
                    this.IncomeGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);

                }

                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (!this.deleteValidation)// && this.tempCropId != null && this.tempCropId != "")
                    {
                        this.ToEnableEditButtonInMasterForm();
                        if (this.currentColumnIndex == this.IncomeGridView.Columns[this.Source.Name].Index)
                        {
                            string fillterCondtion = "SourceCode = '" + this.IncomeGridView[this.Source.Name, this.IncomeGridView.CurrentRowIndex].Value.ToString() + "'";

                            DataRow[] tempCurrentRowValues = this.getincomesourcetypesDataTable.Select(fillterCondtion);

                            if (tempCurrentRowValues.Length > 0)
                            {
                                //this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Source.Name].Value = this.tempCropId;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Description.Name].Value = DBNull.Value;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Units.Name].Value = DBNull.Value;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.ContractPerUnit.Name].Value = DBNull.Value;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Contract.Name].Value = DBNull.Value;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.MarketPerUnit.Name].Value = DBNull.Value;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Market.Name].Value = DBNull.Value;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.IncomeApproachItemID.Name].Value = this.incomeApproachItemIdIdValue;
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.IncomeSourceID.Name].Value = tempCurrentRowValues[0]["IncomeSourceID"].ToString();
                                this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Source.Name].Value = tempCurrentRowValues[0]["SourceCode"].ToString();
                            }
                        }
                    }
                }

                if (!this.allowDelete)
                {
                    if (this.IncomeGridView.CurrentColumnIndex == this.IncomeGridView.Columns[this.Source.Name].Index && (!string.IsNullOrEmpty(this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Source.Name].Value.ToString().Trim())))
                    {
                        string fillterCondtion = "SourceCode = '" + this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Source.Name].Value.ToString().Trim() + "'";

                        DataRow[] tempCurrentRowValues = this.getincomesourcetypesDataTable.Select(fillterCondtion);

                        if (tempCurrentRowValues.Length > 0)
                        {
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Description.Name].Value = DBNull.Value;
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Units.Name].Value = DBNull.Value;
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.ContractPerUnit.Name].Value = DBNull.Value;
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Contract.Name].Value = DBNull.Value;
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.MarketPerUnit.Name].Value = DBNull.Value;
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Market.Name].Value = DBNull.Value;
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.IncomeApproachItemID.Name].Value = this.incomeApproachItemIdIdValue;
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.IncomeSourceID.Name].Value = tempCurrentRowValues[0]["IncomeSourceID"].ToString();
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Source.Name].Value = tempCurrentRowValues[0]["SourceCode"].ToString();
                        }

                    
                    }
                }

            }
            catch (StackOverflowException est)
            {
                ExceptionManager.ManageException(est, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region TextBox Change Events
        private void CalculateIncomeApproach()
        {
            try
            {
                decimal contractAmt_PotentialGrossIncome = 0;
                decimal market_PotentialGrossIncome = 0;
                decimal contract_VacancyandCollectionLoss = 0;
                decimal contractAmt_VacancyandCollectionLoss = 0;
                decimal market_VacancyandCollectionLoss = 0;
                decimal marketAmt_VacancyandCollectionLoss = 0;
                decimal contractAmt_EffectiveGrossIncome = 0;
                decimal marketAmt_EffectiveGrossIncome = 0;
                decimal contract_Insurance = 0;
                decimal contractAmt_Insurance = 0;
                decimal market_Insurance = 0;
                decimal marketAmt_Insurance = 0;
                decimal contract_ManagementandPayroll = 0;
                decimal contractAmt_ManagementandPayroll = 0;
                decimal market_ManagementandPayroll = 0;
                decimal marketAmt_ManagementandPayroll = 0;
                decimal contract_Utilities = 0;
                decimal contractAmt_Utilities = 0;
                decimal market_Utilities = 0;
                decimal marketAmt_Utilities = 0;
                decimal contract_MaintainceandRepairs = 0;
                decimal contractAmt_MaintainceandRepairs = 0;
                decimal market_MaintainceandRepairs = 0;
                decimal marketAmt_MaintainceandRepairs = 0;
                decimal contract_ReservesforReplacement = 0;
                decimal contractAmt_ReservesforReplacement = 0;
                decimal market_ReservesforReplacement = 0;
                decimal marketAmt_ReservesforReplacement = 0;
                decimal contract_TotalExpenses = 0;
                decimal contractAmt_TotalExpenses = 0;
                decimal market_TotalExpenses = 0;
                decimal marketAmt_TotalExpenses = 0;
                decimal netOperatingIncome1 = 0;
                decimal netOperatingIncome2 = 0;
                decimal capitalizationRate = 0;
                bool useContractRents;
                decimal incomeApproachRate = 0;
                //TSCO - D31091.F36091 Income Approach form change requests
                decimal SuppliesContractAmt = 0;
                decimal SuppliesMarketAmt = 0;
                decimal SuppliesMarketPer = 0;
                decimal ContractSupplies = 0;
                decimal OthersContractAmt = 0;
                decimal OthersMarketPer = 0;
                decimal OthersContractPer = 0;
                decimal Market_OthersAmt = 0;
                //TSCO - Income Approach - New Misc Income and Personal Property fields
                decimal MiscIncomeContractPercent = 0;
                decimal MiscIncomeContractValue = 0;
                decimal MiscIncomeMarketPercent = 0;
                decimal MiscIncomeMarketValue = 0;
                

                F36091IncomeApproachData IncomeApproachDetails = new F36091IncomeApproachData();
                F36091IncomeApproachData.GetIncomeApproachValuesRow getIncomeAppraochSourceRow = IncomeApproachDetails.GetIncomeApproachValues.NewGetIncomeApproachValuesRow();

                if (this.txtContractAmt_PotentialGrossIncome.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_PotentialGrossIncome.Text, out contractAmt_PotentialGrossIncome);
                    getIncomeAppraochSourceRow.PotGrossIncomeContractValue = contractAmt_PotentialGrossIncome;
                }
                else
                {
                    getIncomeAppraochSourceRow.PotGrossIncomeContractValue = contractAmt_PotentialGrossIncome;
                }

                if (this.txtMarketAmt_PotentialGrossIncome.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_PotentialGrossIncome.Text, out market_PotentialGrossIncome);
                    getIncomeAppraochSourceRow.PotGrossIncomeMarketValue = market_PotentialGrossIncome;
                }
                else
                {
                    getIncomeAppraochSourceRow.PotGrossIncomeMarketValue = market_PotentialGrossIncome;
                }

                if (this.txtContract_VacancyandCollectionLoss.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContract_VacancyandCollectionLoss.Text, out contract_VacancyandCollectionLoss);
                    getIncomeAppraochSourceRow.VacAndCollContractPercent = contract_VacancyandCollectionLoss;
                }
                else
                {
                    getIncomeAppraochSourceRow.VacAndCollContractPercent = contract_VacancyandCollectionLoss;
                }

                if (this.txtContractAmt_VacancyandCollectionLoss.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_VacancyandCollectionLoss.Text, out contractAmt_VacancyandCollectionLoss);
                    getIncomeAppraochSourceRow.VacAndCollContractValue = contractAmt_VacancyandCollectionLoss;
                }
                else
                {
                    getIncomeAppraochSourceRow.VacAndCollContractValue = contractAmt_VacancyandCollectionLoss;
                }

                if (this.txtMarket_VacancyandCollectionLoss.Text != string.Empty)
                {
                    double dmarket_VacancyandCollectionLoss = 0;
                    double.TryParse(this.txtMarket_VacancyandCollectionLoss.Text.Replace("%", string.Empty).ToString(), out dmarket_VacancyandCollectionLoss);
                    market_VacancyandCollectionLoss = Convert.ToDecimal(dmarket_VacancyandCollectionLoss * 0.01);
                    getIncomeAppraochSourceRow.VacAndCollMarketPercent = market_VacancyandCollectionLoss;
                }
                else
                {
                    getIncomeAppraochSourceRow.VacAndCollMarketPercent = market_VacancyandCollectionLoss;
                }

                if (this.txtMarketAmt_VacancyandCollectionLoss.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_VacancyandCollectionLoss.Text, out marketAmt_VacancyandCollectionLoss);
                    getIncomeAppraochSourceRow.VacAndCollMarketValue = marketAmt_VacancyandCollectionLoss;
                }
                else
                {
                    getIncomeAppraochSourceRow.VacAndCollMarketValue = marketAmt_VacancyandCollectionLoss;
                }

                if (this.txtContractAmt_EffectiveGrossIncome.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_EffectiveGrossIncome.Text, out contractAmt_EffectiveGrossIncome);
                    getIncomeAppraochSourceRow.EffGrossIncomeContractValue = contractAmt_EffectiveGrossIncome;
                }
                else
                {
                    getIncomeAppraochSourceRow.EffGrossIncomeContractValue = contractAmt_EffectiveGrossIncome;
                }

                if (this.txtMarketAmt_EffectiveGrossIncome.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_EffectiveGrossIncome.Text, out marketAmt_EffectiveGrossIncome);
                    getIncomeAppraochSourceRow.EffGrossIncomeMarketValue = marketAmt_EffectiveGrossIncome;
                }
                else
                {
                    getIncomeAppraochSourceRow.EffGrossIncomeMarketValue = marketAmt_EffectiveGrossIncome;
                }

                if (this.txtContract_Insurance.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContract_Insurance.Text, out contract_Insurance);
                    getIncomeAppraochSourceRow.InsuranceContractPercent = contract_Insurance;
                }
                else
                {
                    getIncomeAppraochSourceRow.InsuranceContractPercent = contract_Insurance;
                }

                if (this.txtContractAmt_Insurance.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_Insurance.Text, out contractAmt_Insurance);
                    getIncomeAppraochSourceRow.InsuranceContractValue = contractAmt_Insurance;
                }
                else
                {
                    getIncomeAppraochSourceRow.InsuranceContractValue = contractAmt_Insurance;
                }

                if (this.txtMarket_Insurance.Text != string.Empty)
                {
                    double dmarket_Insurance = 0;
                    double.TryParse(this.txtMarket_Insurance.Text.Replace("%", string.Empty).ToString(), out dmarket_Insurance);
                    market_Insurance = Convert.ToDecimal(dmarket_Insurance * 0.01);
                    getIncomeAppraochSourceRow.InsuranceMarketPercent = market_Insurance;
                }
                else
                {
                    getIncomeAppraochSourceRow.InsuranceMarketPercent = market_Insurance;
                }

                if (this.txtMarketAmt_Insurance.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_Insurance.Text, out marketAmt_Insurance);
                    getIncomeAppraochSourceRow.InsuranceMarketValue = marketAmt_Insurance;
                }
                else
                {
                    getIncomeAppraochSourceRow.InsuranceMarketValue = marketAmt_Insurance;
                }

                if (this.txtContract_ManagementandPayroll.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContract_ManagementandPayroll.Text, out contract_ManagementandPayroll);
                    getIncomeAppraochSourceRow.ManAndPayContractPercent = contract_ManagementandPayroll;
                }
                else
                {
                    getIncomeAppraochSourceRow.ManAndPayContractPercent = contract_ManagementandPayroll;
                }

                if (this.txtContractAmt_ManagementandPayroll.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_ManagementandPayroll.Text, out contractAmt_ManagementandPayroll);
                    getIncomeAppraochSourceRow.ManAndPayContractValue = contractAmt_ManagementandPayroll;
                }
                else
                {
                    getIncomeAppraochSourceRow.ManAndPayContractValue = contractAmt_ManagementandPayroll;
                }

                if (this.txtMarket_ManagementandPayroll.Text != string.Empty)
                {
                    double dmarket_ManagementandPayroll = 0;
                    double.TryParse(this.txtMarket_ManagementandPayroll.Text.Replace("%",string.Empty).ToString(), out dmarket_ManagementandPayroll);
                    market_ManagementandPayroll = Convert.ToDecimal(dmarket_ManagementandPayroll * 0.01);
                    getIncomeAppraochSourceRow.ManAndPayMarketPercent = market_ManagementandPayroll;
                }
                else
                {
                    getIncomeAppraochSourceRow.ManAndPayMarketPercent = market_ManagementandPayroll;
                }

                if (this.txtMarketAmt_ManagementandPayroll.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_ManagementandPayroll.Text, out marketAmt_ManagementandPayroll);
                    getIncomeAppraochSourceRow.ManAndPayMarketValue = marketAmt_ManagementandPayroll;
                }
                else
                {
                    getIncomeAppraochSourceRow.ManAndPayMarketValue = marketAmt_ManagementandPayroll;
                }


                if (this.txtContract_Utilities.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContract_Utilities.Text, out contract_Utilities);
                    getIncomeAppraochSourceRow.UtilitiesContractPercent = contract_Utilities;
                }
                else
                {
                    getIncomeAppraochSourceRow.UtilitiesContractPercent = contract_Utilities;
                }

                if (this.txtContractAmt_Utilities.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_Utilities.Text, out contractAmt_Utilities);
                    getIncomeAppraochSourceRow.UtilitiesContractValue = contractAmt_Utilities;
                }
                else
                {
                    getIncomeAppraochSourceRow.UtilitiesContractValue = contractAmt_Utilities;
                }

                if (this.txtMarket_Utilities.Text != string.Empty)
                {
                    double dmarket_Utilities = 0;
                    double.TryParse(this.txtMarket_Utilities.Text.Replace("%", string.Empty).ToString(), out dmarket_Utilities);
                    market_Utilities = Convert.ToDecimal(dmarket_Utilities * 0.01);
                    getIncomeAppraochSourceRow.UtilitiesMarketPercent = market_Utilities;
                }
                else
                {
                    getIncomeAppraochSourceRow.UtilitiesMarketPercent = market_Utilities;
                }

                if (this.txtMarketAmt_Utilities.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_Utilities.Text, out marketAmt_Utilities);
                    getIncomeAppraochSourceRow.UtilitiesMarketValue = marketAmt_Utilities;
                }
                else
                {
                    getIncomeAppraochSourceRow.UtilitiesMarketValue = marketAmt_Utilities;
                }


                if (this.txtContract_MaintainceandRepairs.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContract_MaintainceandRepairs.Text, out contract_MaintainceandRepairs);
                    getIncomeAppraochSourceRow.MaintAndRepContractPercent = contract_MaintainceandRepairs;
                }
                else
                {
                    getIncomeAppraochSourceRow.MaintAndRepContractPercent = contract_MaintainceandRepairs;
                }

                if (this.txtContractAmt_MaintainceandRepairs.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_MaintainceandRepairs.Text, out contractAmt_MaintainceandRepairs);
                    getIncomeAppraochSourceRow.MaintAndRepContractValue = contractAmt_MaintainceandRepairs;
                }
                else
                {
                    getIncomeAppraochSourceRow.MaintAndRepContractValue = contractAmt_MaintainceandRepairs;
                }

                if (this.txtMarket_MaintainceandRepairs.Text != string.Empty)
                {
                    double dmarket_MaintainceandRepairs = 0;
                    double.TryParse(this.txtMarket_MaintainceandRepairs.Text.Replace("%", string.Empty).ToString(), out dmarket_MaintainceandRepairs);
                    market_MaintainceandRepairs = Convert.ToDecimal(dmarket_MaintainceandRepairs * 0.01);
                    getIncomeAppraochSourceRow.MaintAndRepMarketPercent = market_MaintainceandRepairs;
                }
                else
                {
                    getIncomeAppraochSourceRow.MaintAndRepMarketPercent = market_MaintainceandRepairs;
                }

                if (this.txtMarketAmt_MaintainceandRepairs.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_MaintainceandRepairs.Text, out marketAmt_MaintainceandRepairs);
                    getIncomeAppraochSourceRow.MaintAndRepMarketValue = marketAmt_MaintainceandRepairs;
                }
                else
                {
                    getIncomeAppraochSourceRow.MaintAndRepMarketValue = marketAmt_MaintainceandRepairs;
                }

                if (this.txtContract_ReservesforReplacement.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContract_ReservesforReplacement.Text, out contract_ReservesforReplacement);
                    getIncomeAppraochSourceRow.ResReplaceContractPercent = contract_ReservesforReplacement;
                }
                else
                {
                    getIncomeAppraochSourceRow.ResReplaceContractPercent = contract_ReservesforReplacement;
                }


                if (this.txtContractAmt_ReservesforReplacement.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_ReservesforReplacement.Text, out contractAmt_ReservesforReplacement);
                    getIncomeAppraochSourceRow.ResReplaceContractValue = contractAmt_ReservesforReplacement;
                }
                else
                {
                    getIncomeAppraochSourceRow.ResReplaceContractValue = contractAmt_ReservesforReplacement;
                }

                if (this.txtMarket_ReservesforReplacement.Text != string.Empty)
                {
                    double dmarket_ReservesforReplacement = 0;
                    double.TryParse(this.txtMarket_ReservesforReplacement.Text.Replace("%", string.Empty).ToString(), out dmarket_ReservesforReplacement);
                    market_ReservesforReplacement = Convert.ToDecimal(dmarket_ReservesforReplacement * 0.01);
                    getIncomeAppraochSourceRow.ResReplaceMarketPercent = market_ReservesforReplacement;
                }
                else
                {
                    getIncomeAppraochSourceRow.ResReplaceMarketPercent = market_ReservesforReplacement;
                }

                if (this.txtMarketAmt_ReservesforReplacement.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_ReservesforReplacement.Text, out marketAmt_ReservesforReplacement);
                    getIncomeAppraochSourceRow.ResReplaceMarketValue = marketAmt_ReservesforReplacement;
                }
                else
                {
                    getIncomeAppraochSourceRow.ResReplaceMarketValue = marketAmt_ReservesforReplacement;
                }

                if (this.txtContract_TotalExpenses.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContract_TotalExpenses.Text, out contract_TotalExpenses);
                    getIncomeAppraochSourceRow.TotalExpensesContractPercent = contract_TotalExpenses;
                }
                else
                {
                    getIncomeAppraochSourceRow.TotalExpensesContractPercent = contract_TotalExpenses;
                }

                if (this.txtContractAmt_TotalExpenses.Text != string.Empty)
                {
                    decimal.TryParse(this.txtContractAmt_TotalExpenses.Text, out contractAmt_TotalExpenses);
                    getIncomeAppraochSourceRow.TotalExpensesContractValue = contractAmt_TotalExpenses;
                }
                else
                {
                    getIncomeAppraochSourceRow.TotalExpensesContractValue = contractAmt_TotalExpenses;
                }

                if (this.txtMarket_TotalExpenses.Text != string.Empty)
                {
                    double dmarket_TotalExpenses = 0;
                    double.TryParse(this.txtMarket_TotalExpenses.Text.Replace("%", string.Empty).ToString(), out dmarket_TotalExpenses);
                    market_TotalExpenses = Convert.ToDecimal(dmarket_TotalExpenses);
                    getIncomeAppraochSourceRow.TotalExpensesMarketPercent = market_TotalExpenses;
                }
                else
                {
                    getIncomeAppraochSourceRow.TotalExpensesMarketPercent = market_TotalExpenses;
                }

                if (this.txtMarketAmt_TotalExpenses.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarketAmt_TotalExpenses.Text, out marketAmt_TotalExpenses);
                    getIncomeAppraochSourceRow.TotalExpensesMarketValue = marketAmt_TotalExpenses;
                }
                else
                {
                    getIncomeAppraochSourceRow.TotalExpensesMarketValue = marketAmt_TotalExpenses;
                }

                if (this.txtNetOperatingIncome1.Text != string.Empty)
                {
                    decimal.TryParse(this.txtNetOperatingIncome1.Text, out netOperatingIncome1);
                    getIncomeAppraochSourceRow.NetOpIncContractValue = netOperatingIncome1;
                }
                else
                {
                    getIncomeAppraochSourceRow.NetOpIncContractValue = netOperatingIncome1;
                }

                if (this.txtNetOperatingIncome2.Text != string.Empty)
                {
                    decimal.TryParse(this.txtNetOperatingIncome2.Text, out netOperatingIncome2);
                    getIncomeAppraochSourceRow.NetOpIncMarketValue = netOperatingIncome2;
                }
                else
                {
                    getIncomeAppraochSourceRow.NetOpIncMarketValue = netOperatingIncome2;
                }

                if (this.txtCapitalizationRate.Text != string.Empty)
                {
                    double dcapitalizationRate = 0;
                    double.TryParse(this.txtCapitalizationRate.Text.Replace("%", string.Empty).ToString(), out dcapitalizationRate);
                    capitalizationRate = Convert.ToDecimal(dcapitalizationRate * 0.01);
                    getIncomeAppraochSourceRow.CapitalizationRate = capitalizationRate;
                }
                else
                {
                    getIncomeAppraochSourceRow.CapitalizationRate = capitalizationRate;
                }

                useContractRents = chkUseContractRents.Checked;
                getIncomeAppraochSourceRow.IsUseContract = useContractRents;

                if (this.txtIncomeApproachRate.Text != string.Empty)
                {
                    decimal.TryParse(this.txtIncomeApproachRate.Text, out incomeApproachRate);
                    getIncomeAppraochSourceRow.IncomeApproach = incomeApproachRate;
                }
                else
                {
                    getIncomeAppraochSourceRow.IncomeApproach = incomeApproachRate;
                }

                //
                if (this.txtSuppliesContractAmt.Text != string.Empty)
                {
                    decimal.TryParse(this.txtSuppliesContractAmt.Text, out SuppliesContractAmt);
                    getIncomeAppraochSourceRow.SuppliesContractValue = SuppliesContractAmt;
                }
                else
                {
                    getIncomeAppraochSourceRow.SuppliesContractValue = SuppliesContractAmt;
                }

                if (this.txtSuppliesMarketAmt.Text != string.Empty)
                {
                    decimal.TryParse(this.txtSuppliesMarketAmt.Text, out SuppliesMarketAmt);
                    getIncomeAppraochSourceRow.SuppliesMarketValue = SuppliesMarketAmt;
                }
                else
                {
                    getIncomeAppraochSourceRow.SuppliesMarketValue = SuppliesMarketAmt;
                }
                if (this.txtSuppliesMarketPer.Text != string.Empty)
                {
                    double dSuppliesMarketPer = 0;
                    double.TryParse(this.txtSuppliesMarketPer.Text.Replace("%", string.Empty).ToString(), out dSuppliesMarketPer);
                    SuppliesMarketPer = Convert.ToDecimal(dSuppliesMarketPer * 0.01);
                    getIncomeAppraochSourceRow.SuppliesMarketPercent = SuppliesMarketPer;

                }
                else
                {
                    getIncomeAppraochSourceRow.SuppliesMarketPercent = SuppliesMarketPer;
                }

                if (this.txtContractSuppliesPer.Text != string.Empty)
                {

                    double dContractSuppliesPer = 0;
                    double.TryParse(this.txtContractSuppliesPer.Text.Replace("%", string.Empty).ToString(), out dContractSuppliesPer);
                    ContractSupplies = Convert.ToDecimal(dContractSuppliesPer * 0.01);
                    getIncomeAppraochSourceRow.SuppliesContractPercent = ContractSupplies;

                }
                else
                {
                    getIncomeAppraochSourceRow.SuppliesContractPercent = ContractSupplies;
                }

                if (this.txtOthersContractAmt.Text != string.Empty)
                {
                    decimal.TryParse(this.txtOthersContractAmt.Text, out OthersContractAmt);
                    getIncomeAppraochSourceRow.OtherExpensesContractValue = OthersContractAmt;
                }
                else
                {
                    getIncomeAppraochSourceRow.OtherExpensesContractValue = OthersContractAmt;
                }

                if (this.txtOthersMarketPer.Text != string.Empty)
                {
                    double dOthersMarketPer = 0;
                    double.TryParse(this.txtOthersMarketPer.Text.Replace("%", string.Empty).ToString(), out dOthersMarketPer);
                    OthersMarketPer = Convert.ToDecimal(dOthersMarketPer * 0.01);
                    getIncomeAppraochSourceRow.OtherExpensesMarketPercent = OthersMarketPer;

                }
                else
                {
                    getIncomeAppraochSourceRow.OtherExpensesMarketPercent = OthersMarketPer;
                }


                if (this.txtOthersContractPer.Text != string.Empty)
                {
                    double dOthersContractPer = 0;
                    double.TryParse(this.txtOthersContractPer.Text.Replace("%", string.Empty).ToString(), out dOthersContractPer);
                    OthersContractPer = Convert.ToDecimal(dOthersContractPer * 0.01);
                    getIncomeAppraochSourceRow.OtherExpensesContractPercent = OthersContractPer;
                }
                else
                {
                    getIncomeAppraochSourceRow.OtherExpensesContractPercent = OthersContractPer;
                }

                if (this.txtMarket_OthersAmt.Text != string.Empty)
                {
                    decimal.TryParse(this.txtMarket_OthersAmt.Text, out Market_OthersAmt);
                    getIncomeAppraochSourceRow.OtherExpensesMarketValue = Market_OthersAmt;
                }
                else
                {
                    getIncomeAppraochSourceRow.OtherExpensesMarketValue = Market_OthersAmt;
                }
                //TSCO - Income Approach - New Misc Income and Personal Property fields.
                if (this.text_MiscIncomeContractPercent.Text != string.Empty)
                {
                    double dMiscIncomeContractPercent = 0;
                    double.TryParse(this.text_MiscIncomeContractPercent.Text.Replace("%", string.Empty).ToString(), out dMiscIncomeContractPercent);
                    MiscIncomeContractPercent = Convert.ToDecimal(dMiscIncomeContractPercent * 0.01);
                    getIncomeAppraochSourceRow.MiscIncomeContractPercent = MiscIncomeContractPercent;
                }
                else
                {
                    getIncomeAppraochSourceRow.MiscIncomeContractPercent = MiscIncomeContractPercent;
                }

                if (this.text_MiscIncomeContractValue.Text != string.Empty)
                {
                    decimal.TryParse(this.text_MiscIncomeContractValue.Text, out MiscIncomeContractValue);
                    getIncomeAppraochSourceRow.MiscIncomeContractValue = MiscIncomeContractValue;
                }
                else
                {
                    getIncomeAppraochSourceRow.MiscIncomeContractValue = MiscIncomeContractValue;
                }

                if (this.text_MiscIncomeMarketPercent.Text != string.Empty)
                {
                    double dMiscImproveMarketPer = 0;
                    double.TryParse(this.text_MiscIncomeMarketPercent.Text.Replace("%", string.Empty).ToString(), out dMiscImproveMarketPer);
                    MiscIncomeMarketPercent = Convert.ToDecimal(dMiscImproveMarketPer * 0.01);
                    getIncomeAppraochSourceRow.MiscIncomeMarketPercent = MiscIncomeMarketPercent;
                }
                else
                {
                    getIncomeAppraochSourceRow.MiscIncomeMarketPercent = MiscIncomeMarketPercent;
                }

                if (this.text_MiscIncomeMarketValue.Text != string.Empty)
                {
                    decimal.TryParse(this.text_MiscIncomeMarketValue.Text, out MiscIncomeMarketValue);
                    getIncomeAppraochSourceRow.MiscIncomeMarketValue = MiscIncomeMarketValue;
                }
                else
                {
                    getIncomeAppraochSourceRow.MiscIncomeMarketValue = MiscIncomeMarketValue;
                }

                IncomeApproachDetails.GetIncomeApproachValues.Rows.Add(getIncomeAppraochSourceRow);
                IncomeApproachDetails.GetIncomeApproachValues.AcceptChanges();
                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(IncomeApproachDetails.GetIncomeApproachValues.Copy());

                tempDataSet.Tables[0].TableName = "IncomeApproach";
                string xml = tempDataSet.GetXml();
                IncomeApproachDetails = this.F36091WorkItem.F36091_GetIncomeApproachItemDetails(tempDataSet.GetXml());
                this.txtContract_VacancyandCollectionLoss.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["VacAndCollContractPercent"].ToString());
                this.txtMarketAmt_VacancyandCollectionLoss.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["VacAndCollMarketValue"].ToString());
                this.txtContractAmt_EffectiveGrossIncome.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["EffGrossIncomeContractValue"].ToString());
                this.txtMarketAmt_EffectiveGrossIncome.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["EffGrossIncomeMarketValue"].ToString());
                this.txtContract_Insurance.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["InsuranceContractPercent"].ToString());
                this.txtMarketAmt_Insurance.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["InsuranceMarketValue"].ToString());
                this.txtContract_ManagementandPayroll.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["ManAndPayContractPercent"].ToString());
                this.txtMarketAmt_ManagementandPayroll.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["ManAndPayMarketValue"].ToString());
                this.txtContract_Utilities.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["UtilitiesContractPercent"].ToString());
                this.txtMarketAmt_Utilities.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["UtilitiesMarketValue"].ToString());
                this.txtContract_MaintainceandRepairs.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["MaintAndRepContractPercent"].ToString());
                this.txtMarketAmt_MaintainceandRepairs.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["MaintAndRepMarketValue"].ToString());
                this.txtContract_ReservesforReplacement.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["ResReplaceContractPercent"].ToString());
                this.txtMarketAmt_ReservesforReplacement.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["ResReplaceMarketValue"].ToString());
                this.txtContract_TotalExpenses.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["TotalExpensesContractPercent"].ToString());
                this.txtContractAmt_TotalExpenses.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["TotalExpensesContractValue"].ToString());
                this.txtMarket_TotalExpenses.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["TotalExpensesMarketPercent"].ToString());
                this.txtMarketAmt_TotalExpenses.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["TotalExpensesMarketValue"].ToString());
                this.txtNetOperatingIncome1.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["NetOpIncContractValue"].ToString());
                this.txtNetOperatingIncome2.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["NetOpIncMarketValue"].ToString());
                if(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["IsUseContract"]!=DBNull.Value && IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["IsUseContract"]!=null)
                this.chkUseContractRents.Checked = Convert.ToBoolean(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["IsUseContract"]);
                this.txtIncomeApproachRate.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["IncomeApproach"].ToString());
                //
                this.txtContractSuppliesPer.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["SuppliesContractPercent"].ToString());
                this.txtSuppliesMarketAmt.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["SuppliesMarketValue"].ToString());
                this.txtOthersContractPer.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["OtherExpensesContractPercent"].ToString());
                this.txtMarket_OthersAmt.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["OtherExpensesMarketValue"].ToString());
                //TSCO - Income Approach - New Misc Income and Personal Property fields.
                this.text_MiscIncomeContractPercent.Text = ContractPercentageFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["MiscIncomeContractPercent"].ToString());
                this.text_MiscIncomeMarketValue.Text = ContractAmountFormat(IncomeApproachDetails.GetIncomeApproachValues.Rows[0]["MiscIncomeMarketValue"].ToString());
                
                this.EditEnabled();
            }
            catch (Exception ex) 
            {

            }
        }

        private void txtContractAmt_PotentialGrossIncome_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_PotentialGrossIncome.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_VacancyandCollectionLoss_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_VacancyandCollectionLoss.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtMarket_VacancyandCollectionLoss_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarket_VacancyandCollectionLoss.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_OperatingExpenses_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_OperatingExpenses.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtMarket_OperatingExpenses_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarket_OperatingExpenses.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_Insurance_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_Insurance.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtMarket_Insurance_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarket_Insurance.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_ManagementandPayroll_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_ManagementandPayroll.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtMarket_ManagementandPayroll_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarket_ManagementandPayroll.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_Utilities_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_Utilities.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtMarket_Utilities_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarket_Utilities.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_MaintainceandRepairs_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_MaintainceandRepairs.Text))
            {

                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtMarket_MaintainceandRepairs_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarket_MaintainceandRepairs.Text))
            {

                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_ReservesforReplacement_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_ReservesforReplacement.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtMarket_ReservesforReplacement_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarket_ReservesforReplacement.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtCapitalizationRate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCapitalizationRate.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtContractAmt_PotentialGrossIncome_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_PotentialGrossIncome.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }
        private void txtMarketAmt_PotentialGrossIncome_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMarketAmt_PotentialGrossIncome.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void chkUseContractRents_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cancelEnablesavecancel)
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        #endregion TextBox Change Events

        /// <summary>
        /// RemoveButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.removeEnablesavecancel = true;
                if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.deletePermission)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("RemoveRecordIncomeSources"), SharedFunctions.GetResourceString("RemoveRecordIncomeSourceHead"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        GetSelectedIncomeSourceIdsXml();
                        this.form36091Control.WorkItem.F36091_DeleteIncomeSource(this.selectedIncomeSourceItemIdsXml, TerraScanCommon.UserId);
                        this.CustomizeIncomeSourcesGridView();
                        this.LoadSourceTypesDetails();
                        this.IncomeGridView.RefreshEdit();
                        this.RemoveButton.Enabled = false;
                        this.SelectAllCheckBox.Checked = false;
                        this.CustomizeIncomeSourcesGridView();
                        this.allowDelete = true;
                        this.LoadSourceTypesDetails();


                        decimal resultAmount;
                        Decimal.TryParse(this.txtIncomeApproachRate.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

                        F35002SubFormSaveEventArgs subFormSaveEventArgs;
                        subFormSaveEventArgs.type = 5;
                        subFormSaveEventArgs.value = resultAmount;
                        subFormSaveEventArgs.valueSliceId = this.valueSliceId;

                        subFormSaveEventArgs.amount = resultAmount;
                        this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));

                        this.pageMode = TerraScanCommon.PageModeTypes.View;

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.removeEnablesavecancel = false;
            }
        }

        /// <summary>
        /// Gets the selected Income Source ids to XML .
        /// </summary>
        private void GetSelectedIncomeSourceIdsXml()
        {
            this.selectedIncomeSourceItemIdsXml = string.Empty;
            DataTable tempXMLdataTable = new DataTable();
            foreach (DataColumn column in this.getincomesourceDataTable.Columns)
            {
                if (column.ColumnName == this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName)
                {
                    tempXMLdataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            for (int item = 0; item < this.selectedIncomeSourceItemIds.Count; item++)
            {
                DataRow tempXMLDataRow = tempXMLdataTable.NewRow();
                tempXMLDataRow[this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName] = this.selectedIncomeSourceItemIds[item].ToString();
                tempXMLdataTable.Rows.Add(tempXMLDataRow);
            }
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(tempXMLdataTable.Copy());
            tempDataSet.Tables[0].TableName = "Tables";
            this.selectedIncomeSourceItemIdsXml = tempDataSet.GetXml();
        }


        /// <summary>
        /// Selects the un select all and Unselect all for 163 sprint.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SelectUnSelectAll(string status)
        {
            if (this.sourcegridRowCount > 0)
            {
                for (int count = 0; count < this.sourcegridRowCount; count++)
                {
                    this.IncomeGridView.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = status;
                }
            }

        }

        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (SelectAllCheckBox.Checked == true)
                {
                    selectedIncomeSourceItemIds = new List<int>();
                    if (this.sourcegridRowCount > 0)
                    {
                        this.SelectUnSelectAll("True");
                        this.RemoveButton.Enabled = true;
                    }
                    this.CalculateSelectAllIncomeSources(SelectAllCheckBox.Checked);

                }
                else if (SelectAllCheckBox.Checked == false)
                {
                    if (this.sourcegridRowCount > 0 && this.sourcegridRowCount <= this.selectedIncomeSourceItemIds.Count)
                    {
                        this.SelectUnSelectAll("False");
                        this.RemoveButton.Enabled = false;
                        this.CalculateUnSelectIncomeSource(SelectAllCheckBox.Checked);
                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void IncomeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < this.IncomeGridView.OriginalRowCount)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (e.ColumnIndex.Equals(this.IncomeGridView.Columns["ValidStatus"].Index))
                    {
                        int IncomeSourceItemId;
                        int.TryParse(this.IncomeGridView.Rows[e.RowIndex].Cells[this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName].Value.ToString(), out IncomeSourceItemId);
                        if (IncomeSourceItemId > 0)
                        {
                            if (Convert.ToBoolean(this.IncomeGridView.Rows[e.RowIndex].Cells[this.ValidStatus.Name].EditedFormattedValue) == true)
                            {
                                this.IncomeGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                                if (this.selectedIncomeSourceItemIds.Contains(IncomeSourceItemId))
                                {
                                    this.selectedIncomeSourceItemIds.Remove(Convert.ToInt32(this.IncomeGridView[this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName, e.RowIndex].Value));
                                    if (this.selectedIncomeSourceItemIds.Count == 0)
                                    {
                                        this.RemoveButton.Enabled = false;
                                    }
                                }
                                if (this.sourcegridRowCount == 0 && this.selectedIncomeSourceItemIds.Count == 0)
                                {
                                    this.RemoveButton.Enabled = false;
                                }
                                if (this.sourcegridRowCount > this.selectedIncomeSourceItemIds.Count)
                                {
                                    this.SelectAllCheckBox.Checked = false;
                                }
                            }
                            else
                            {
                                this.IncomeGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                                if (!this.selectedIncomeSourceItemIds.Contains(IncomeSourceItemId))
                                {
                                    this.selectedIncomeSourceItemIds.Add(Convert.ToInt32(this.IncomeGridView[this.getincomesourceDataTable.IncomeApproachItemIDColumn.ColumnName, e.RowIndex].Value));
                                }
                                this.RemoveButton.Enabled = true;
                                if (this.sourcegridRowCount == this.selectedIncomeSourceItemIds.Count)
                                {
                                    this.SelectAllCheckBox.Checked = true;
                                }
                            }
                        }

                    }
                }
            }
        }

        private void IncomeGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                decimal outDecimal;

                if (e.ColumnIndex == this.IncomeGridView.Columns[this.Units.Name].Index || e.ColumnIndex == this.IncomeGridView.Columns[this.Units.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Units.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                decimal outDecimalContract;

                if (e.ColumnIndex == this.IncomeGridView.Columns[this.Contract.Name].Index || e.ColumnIndex == this.IncomeGridView.Columns[this.Contract.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Contract.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimalContract))
                        {
                            e.Value = outDecimalContract.ToString("#,##0");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
                decimal outDecimalContractperunit;

                if (e.ColumnIndex == this.IncomeGridView.Columns[this.ContractPerUnit.Name].Index || e.ColumnIndex == this.IncomeGridView.Columns[this.ContractPerUnit.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.ContractPerUnit.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimalContractperunit))
                        {
                            e.Value = outDecimalContractperunit.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
                decimal outDecimalmarket;
                if (e.ColumnIndex == this.IncomeGridView.Columns[this.Market.Name].Index || e.ColumnIndex == this.IncomeGridView.Columns[this.Market.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Market.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimalmarket))
                        {
                            e.Value = outDecimalmarket.ToString("#,##0");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                decimal outDecimalmarketperunit;
                if (e.ColumnIndex == this.IncomeGridView.Columns[this.MarketPerUnit.Name].Index || e.ColumnIndex == this.IncomeGridView.Columns[this.MarketPerUnit.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.MarketPerUnit.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimalmarketperunit))
                        {
                            e.Value = outDecimalmarketperunit.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
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
        private void Control_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.currentRowIndex == 0)
                {
                    this.IncomeGridView[this.Source.Name, this.currentRowIndex].ReadOnly = false;
                    this.IncomeGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                    this.IncomeGridView[this.Units.Name, this.currentRowIndex].ReadOnly = false;
                    this.IncomeGridView[this.ContractPerUnit.Name, this.currentRowIndex].ReadOnly = false;
                    this.IncomeGridView.Rows[this.currentRowIndex].Selected = false;
                }

                bool hasValues = false;
                if (this.currentRowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                        (string.IsNullOrEmpty(this.IncomeGridView[this.Description.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                        (string.IsNullOrEmpty(this.IncomeGridView[this.Units.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                        (string.IsNullOrEmpty(this.IncomeGridView[this.ContractPerUnit.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                        (string.IsNullOrEmpty(this.IncomeGridView[this.Contract.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                        (string.IsNullOrEmpty(this.IncomeGridView[this.Market.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                        (string.IsNullOrEmpty(this.IncomeGridView[this.MarketPerUnit.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (this.currentRowIndex + 1 < IncomeGridView.RowCount)
                        {
                            for (int i = this.currentRowIndex; i < IncomeGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.IncomeGridView[this.Source.Name, this.currentRowIndex].ReadOnly = false;
                                this.IncomeGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                                this.IncomeGridView[this.Units.Name, this.currentRowIndex].ReadOnly = false;
                                this.IncomeGridView[this.ContractPerUnit.Name, this.currentRowIndex].ReadOnly = false;
                                this.IncomeGridView.Rows[this.currentRowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                                    (string.IsNullOrEmpty(this.IncomeGridView[this.Description.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                                    (string.IsNullOrEmpty(this.IncomeGridView[this.Units.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                                    (string.IsNullOrEmpty(this.IncomeGridView[this.ContractPerUnit.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                                    (string.IsNullOrEmpty(this.IncomeGridView[this.Contract.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                                    (string.IsNullOrEmpty(this.IncomeGridView[this.Market.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) &&
                                    (string.IsNullOrEmpty(this.IncomeGridView[this.MarketPerUnit.Name, (this.currentRowIndex - 1)].Value.ToString().Trim()))
                                    )
                                {
                                    this.IncomeGridView[this.Source.Name, this.currentRowIndex].ReadOnly = true;
                                    this.IncomeGridView[this.Description.Name, this.currentRowIndex].ReadOnly = true;
                                    this.IncomeGridView[this.Units.Name, this.currentRowIndex].ReadOnly = true;
                                    this.IncomeGridView[this.ContractPerUnit.Name, this.currentRowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.IncomeGridView[this.Source.Name, this.currentRowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.Units.Name, this.currentRowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.ContractPerUnit.Name, this.currentRowIndex].ReadOnly = false;
                                    this.IncomeGridView.Rows[this.currentRowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.IncomeGridView[this.Source.Name, this.currentRowIndex].ReadOnly = true;
                            this.IncomeGridView[this.Description.Name, this.currentRowIndex].ReadOnly = true;
                            this.IncomeGridView[this.Units.Name, this.currentRowIndex].ReadOnly = true;
                            this.IncomeGridView[this.ContractPerUnit.Name, this.currentRowIndex].ReadOnly = true;

                        }
                    }
                    else
                    {
                        this.IncomeGridView[this.Source.Name, this.currentRowIndex].ReadOnly = false;
                        this.IncomeGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                        this.IncomeGridView[this.Units.Name, this.currentRowIndex].ReadOnly = false;
                        this.IncomeGridView[this.ContractPerUnit.Name, this.currentRowIndex].ReadOnly = false;
                        this.IncomeGridView.Rows[this.currentRowIndex].Selected = false;
                    }
                }
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
                if (this.IncomeGridView.EditingControl is DataGridViewComboBoxEditingControl)
                {
                    if (string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, this.IncomeGridView.CurrentRowIndex].Value.ToString()))
                    {
                        this.tempIncomeSourceId = string.Empty;
                    }
                    else
                    {
                        this.tempIncomeSourceId = this.IncomeGridView[this.Source.Name, this.IncomeGridView.CurrentRowIndex].Value.ToString();
                    }
                }
                this.IncomeGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }



        /// <summary>
        /// Populating the Description Column.
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void PopulateDecriptionValue(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.IncomeGridView.Columns[this.Source.Name].Index && (!string.IsNullOrEmpty(this.IncomeGridView.Rows[e.RowIndex].Cells[this.Source.Name].Value.ToString().Trim())))
                {
                    string fillterCondtion = "SourceCode = '" + this.IncomeGridView.Rows[e.RowIndex].Cells[this.Source.Name].Value.ToString().Trim() +"'";

                    DataRow[] tempCurrentRowValues = this.getincomesourcetypesDataTable.Select(fillterCondtion);

                    if (tempCurrentRowValues.Length > 0)
                    {
                        if (this.selectionchangeflag)
                        {
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.Description.Name].Value = tempCurrentRowValues[0]["Description"].ToString();
                            this.IncomeGridView.Rows[this.currentRowIndex].Cells[this.IncomeSourceID.Name].Value = tempCurrentRowValues[0]["IncomeSourceID"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            //}
        }

        private void IncomeGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.IncomeGridView[this.IncomeApproachItemID.Name, e.RowIndex].Value.ToString().Trim()))
                {
                    int.TryParse(this.IncomeGridView[this.IncomeApproachItemID.Name, e.RowIndex].Value.ToString().Trim(), out this.incomeApproachItemIdIdValue);
                }

                if (e.RowIndex == 0)
                {
                    this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                    this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                    this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                    this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                }

                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Units.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Units.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.ContractPerUnit.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < IncomeGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < IncomeGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[1].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.IncomeGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                                this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                                this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.IncomeGridView[this.Source.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Units.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.ContractPerUnit.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.IncomeGridView[this.Contract.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                                {
                                    this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = true;
                                    this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                                    this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = true;
                                    this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                                    this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                                }
                            }
                        }
                        else
                        {
                            this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = true;
                            this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                            this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = true;
                            this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.IncomeGridView[this.Source.Name, e.RowIndex].ReadOnly = false;
                        this.IncomeGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                        this.IncomeGridView[this.Units.Name, e.RowIndex].ReadOnly = false;
                        this.IncomeGridView[this.ContractPerUnit.Name, e.RowIndex].ReadOnly = false;
                    }

                }


                this.currentRowIndex = e.RowIndex;
                this.currentColumnIndex = e.ColumnIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void IncomeGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }

        private void txtContractAmt_VacancyandCollectionLoss_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            this.txtContractAmt_VacancyandCollectionLoss.Text = ContractAmountFormat(this.txtContractAmt_VacancyandCollectionLoss.Text);
        }

        private void txtContractAmt_Insurance_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            this.txtContractAmt_Insurance.Text = ContractAmountFormat(this.txtContractAmt_Insurance.Text);
        }

        private void txtContractAmt_ManagementandPayroll_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            this.txtContractAmt_ManagementandPayroll.Text = ContractAmountFormat(this.txtContractAmt_ManagementandPayroll.Text);
        }

        private void txtContractAmt_Utilities_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            this.txtContractAmt_Utilities.Text = ContractAmountFormat(this.txtContractAmt_Utilities.Text);
        }

        public static string ContractAmountFormat(string strval)
        {
            string retval = "";
            decimal outDecimalContractperunit;
            if (!String.IsNullOrEmpty(strval))
            {
                if (Decimal.TryParse(strval, out outDecimalContractperunit))
                {
                    retval = outDecimalContractperunit.ToString("#,##0");
                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
            return retval;
        }

        public static string ContractPercentageFormat(string strval)
        {
            string retval = "";
            decimal outDecimalContractPercentage;
            if (!String.IsNullOrEmpty(strval))
            {
                if (Decimal.TryParse(strval, out outDecimalContractPercentage))
                {
                    outDecimalContractPercentage = outDecimalContractPercentage * 100;
                    retval = outDecimalContractPercentage.ToString("###0.00") + "%";
                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
            return retval;
        }


        public static string MarketPercentageFormat(string strval)
        {
            string retval = "";
            decimal outDecimalMarketPercentage;
            if (!String.IsNullOrEmpty(strval))
            {
                strval = strval.Replace("%", string.Empty).ToString();
                if (Decimal.TryParse(strval, out outDecimalMarketPercentage))
                {
                    retval = outDecimalMarketPercentage.ToString("###0.00") + "%";
                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
            return retval;
        }

        public static string ConvertPercentageFormat(string strval)
        {
            string retval = "";
            decimal outDecimalContractPercentage;
            if (!String.IsNullOrEmpty(strval))
            {
                if (Decimal.TryParse(strval, out outDecimalContractPercentage))
                {
                    outDecimalContractPercentage = outDecimalContractPercentage * 100;
                    retval = outDecimalContractPercentage.ToString("###0.00") + "%";
                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
            return retval;
        }

        private void txtContractAmt_MaintainceandRepairs_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            this.txtContractAmt_MaintainceandRepairs.Text = ContractAmountFormat(this.txtContractAmt_MaintainceandRepairs.Text);
        }

        private void txtContractAmt_ReservesforReplacement_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            this.txtContractAmt_ReservesforReplacement.Text = ContractAmountFormat(this.txtContractAmt_ReservesforReplacement.Text);
        }

        private void txtMarket_PotentialGrossIncome_Leave(object sender, EventArgs e)
        {
            txtMarket_PotentialGrossIncome.Text = MarketPercentageFormat(txtMarket_PotentialGrossIncome.Text);
        }

        private void txtMarket_VacancyandCollectionLoss_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtMarket_VacancyandCollectionLoss.Text = MarketPercentageFormat(txtMarket_VacancyandCollectionLoss.Text);
        }

        private void txtMarket_EffectiveGrossIncome_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtMarket_EffectiveGrossIncome.Text = MarketPercentageFormat(txtMarket_EffectiveGrossIncome.Text);
        }

        private void txtMarket_Insurance_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtMarket_Insurance.Text = MarketPercentageFormat(txtMarket_Insurance.Text);
        }

        private void txtMarket_ManagementandPayroll_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtMarket_ManagementandPayroll.Text = MarketPercentageFormat(txtMarket_ManagementandPayroll.Text);
        }

        private void txtMarket_Utilities_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtMarket_Utilities.Text = MarketPercentageFormat(txtMarket_Utilities.Text);
        }

        private void txtMarket_MaintainceandRepairs_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtMarket_MaintainceandRepairs.Text = MarketPercentageFormat(txtMarket_MaintainceandRepairs.Text);
        }

        private void txtMarket_ReservesforReplacement_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtMarket_ReservesforReplacement.Text = MarketPercentageFormat(txtMarket_ReservesforReplacement.Text);
        }

        private void txtMarket_TotalExpenses_Leave(object sender, EventArgs e)
        {
            txtMarket_TotalExpenses.Text = MarketPercentageFormat(txtMarket_TotalExpenses.Text);
        }

        private void txtCapitalizationRate_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtCapitalizationRate.Text = MarketPercentageFormat(txtCapitalizationRate.Text);
        }

        private void IncomeGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.pageLoad)
            {
                if (e.ColumnIndex == 7)
                {
                    decimal sum = 0;

                    foreach (DataGridViewRow row in IncomeGridView.Rows)
                    {
                        if (row.Cells["Contract"].Value.ToString() != string.Empty && row.Cells["Contract"].Value != DBNull.Value)
                            sum += Convert.ToDecimal(row.Cells["Contract"].Value);
                    }
                    txtContractAmt_PotentialGrossIncome.Text = ContractAmountFormat(sum.ToString());
                }
                if (e.ColumnIndex == 9)
                {
                    decimal sum = 0;

                    foreach (DataGridViewRow row in IncomeGridView.Rows)
                    {
                        if (row.Cells["Market"].Value.ToString() != string.Empty && row.Cells["Market"].Value != DBNull.Value)
                            sum += Convert.ToDecimal(row.Cells["Market"].Value);
                    }
                    txtMarketAmt_PotentialGrossIncome.Text = ContractAmountFormat(sum.ToString());
                    //if (sum > 0)
                    //{
                    //    txtMarketAmt_PotentialGrossIncome.Text = ContractAmountFormat(sum.ToString());
                    //}
                    //else
                    //{
                    //    txtMarketAmt_PotentialGrossIncome.Text = string.Empty;
                    //    CalculateIncomeApproach();
                    //}
                }
            }
        }


        private void txtContractAmt_VacancyandCollectionLoss_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMarket_VacancyandCollectionLoss_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtContractAmt_Insurance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMarket_Insurance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtContractAmt_ManagementandPayroll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtContractAmt_Utilities_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtContractAmt_MaintainceandRepairs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtContractAmt_ReservesforReplacement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMarket_ManagementandPayroll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMarket_Utilities_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMarket_MaintainceandRepairs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMarket_ReservesforReplacement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCapitalizationRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtContractAmt_PotentialGrossIncome_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtContractAmt_VacancyandCollectionLoss_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtMarket_VacancyandCollectionLoss_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtContractAmt_Insurance_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtMarket_Insurance_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtContractAmt_ManagementandPayroll_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtMarket_ManagementandPayroll_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtContractAmt_Utilities_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtMarket_Utilities_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtContractAmt_MaintainceandRepairs_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtMarket_MaintainceandRepairs_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtContractAmt_ReservesforReplacement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtMarket_ReservesforReplacement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtCapitalizationRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtSuppliesContractAmt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtSuppliesContractAmt.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtSuppliesMarketPer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtSuppliesMarketPer.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtOthersContractAmt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtOthersContractAmt.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtOthersMarketPer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtOthersMarketPer.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void txtSuppliesContractAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtSuppliesMarketPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtOthersContractAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtOthersMarketPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtOthersMarketPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtOthersContractAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtSuppliesContractAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtSuppliesMarketPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtSuppliesContractAmt_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtSuppliesContractAmt.Text = ContractAmountFormat(txtSuppliesContractAmt.Text);
        }

        private void txtSuppliesMarketPer_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtSuppliesMarketPer.Text = MarketPercentageFormat(txtSuppliesMarketPer.Text);
        }

        private void txtOthersContractAmt_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtOthersContractAmt.Text = ContractAmountFormat(txtOthersContractAmt.Text);
        }

        private void txtOthersMarketPer_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            txtOthersMarketPer.Text = MarketPercentageFormat(txtOthersMarketPer.Text);
        }

        private void IncomeGridView_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void IncomeGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void txtContractAmt_MaintainceandRepairs_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtContractAmt_MaintainceandRepairs.Text))
            {

                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }

        }

        private void txtContractAmt_MaintainceandRepairs_Leave_1(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            this.txtContractAmt_MaintainceandRepairs.Text = ContractAmountFormat(this.txtContractAmt_MaintainceandRepairs.Text);
        }

        private void txtContractAmt_MaintainceandRepairs_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void txtContractAmt_MaintainceandRepairs_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }

        private void lblOthers_Click(object sender, EventArgs e)
        {

        }
        //TSCO - 22079 - New Misc Income and Personal Property fields

        private void text_MiscIncomeContractValue_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.text_MiscIncomeContractValue.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void text_MiscIncomeContractValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void text_MiscIncomeContractValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void text_MiscIncomeContractValue_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            text_MiscIncomeContractValue.Text = ContractAmountFormat(text_MiscIncomeContractValue.Text);
        }

        private void text_MiscIncomeMarketPercent_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void text_MiscIncomeMarketPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void text_MiscIncomeMarketPercent_Leave(object sender, EventArgs e)
        {
            CalculateIncomeApproach();
            text_MiscIncomeMarketPercent.Text = MarketPercentageFormat(text_MiscIncomeMarketPercent.Text);
        }

        private void text_MiscIncomeMarketPercent_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.text_MiscIncomeMarketPercent.Text))
            {
                if (this.pageLoad)
                {
                    CalculateIncomeApproach();
                }
            }
        }

        private void textPersonalProperty_MarketAmt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textPersonalProperty_MarketAmt.Text))
            {
                if (this.pageLoad)
                {
                    this.EditEnabled();
                    //CalculateIncomeApproach();
                }
            }
        }

        private void textPersonalProperty_MarketAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void textPersonalProperty_MarketAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textPersonalProperty_MarketAmt_Leave(object sender, EventArgs e)
        {
            //CalculateIncomeApproach();
            textPersonalProperty_MarketAmt.Text = ContractAmountFormat(textPersonalProperty_MarketAmt.Text);
        }   
    }
}
