//--------------------------------------------------------------------------------------------
// <copyright file="F2551.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2551.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Sep 11        Manoj Kumar.P              Created
// 08 Nov 11        Manoj Kumar.P         Modified for the TSCO # 14038
//*********************************************************************************/


namespace D2550
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using System.IO;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;
    using System.Collections;
    using TerraScan.Common.Reports;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// 2551
    /// </summary>
    [SmartPart]
    public partial class F2551 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Tax Roll Correction form2550Control Controller
        /// </summary>
        private F2551Controller form2551Control;

        ///<summary>
        ///used to hold the temp string of isEdit Temp
        /// </summary>
        //private string isEdittemp=string.Empty;
        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// ParcelID
        /// </summary>
        public int ParcelID;

        /// <summary>
        /// StatementID
        /// </summary>
        public int StatementID;

        /// <summary>
        ///  typeID
        /// </summary>
        public short TypeID;


        /// <summary>
        ///  ParcelNumber
        /// </summary>
        public int ParcelNumber;

        /// <summary>
        /// OwnerId
        /// </summary>
        private int OwnerID;

        /// <summary>
        /// ratesDetailsDataTable variable is used to get the details of rate listing Details.
        /// </summary>
        private F2551EditStmtData editStatementRecordset = new F2551EditStmtData();

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;


        /// <summary>
        /// Created string for store gridItems
        /// </summary>
        private string itemXml = string.Empty;

        /// <summary>
        ///  for store ChangeXML
        /// </summary>
        private string changeXml = string.Empty;

        /// <summary>
        /// Created string for store Header Items
        /// </summary>
        private string headerXml = string.Empty;

        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color highPriorityCommentColor = Color.FromArgb(255, 0, 0);

        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color defaultCommentButtonBackColor = Color.FromArgb(174, 150, 94);

        /// <summary>
        /// Used to Hold the arguments to update Attachments and comments for 9999
        /// </summary>
        private AdditionalOperationCountEntity additionalOperationCountEnt = new AdditionalOperationCountEntity(-9999, -9999, false);

        /// <summary>
        /// Used to Hold the arguments to update Attachments and comments for 2550
        /// </summary>
        private AdditionalOperationCountEntity additionalOperationCount = new AdditionalOperationCountEntity(-9999, -9999, false);

        /// <summary>
        /// parcelTypeDataset
        /// </summary>
        private F2550TaxRollCorrectionData parcelTypeDataset = new F2550TaxRollCorrectionData();

        /// <summary>
        /// Object for attachment typed dataset
        /// </summary>
        private AttachmentsData attachmentDataSet = new AttachmentsData();

        /// <summary>
        /// used to hold PageLoad
        /// </summary>
        private bool PageLoadStatus = true;

        ///<summary>
        /// Used to Edit Button
        /// </summary>
        private bool isEdit = false;

        /// <summary>
        /// hold variable to Close Form
        /// </summary>
        private bool isClose = false; 
       
        /// <summary>
        /// used to store edit textbox;
        /// </summary>
        private bool IsLandEdit=false;
        private bool IsImprovEdit = false;
        private bool IsPermEdit = false;
        private bool IsOriginalEdit = false;
        private bool IsAdjustEdit = false;
        private bool IsExcessEdit = false;
        private bool IsBaseEdit = false;

        public bool canClose = false;

        /// <summary>
        /// Instance variable to hold the max money field value
        /// </summary>
        private double maxMoneyFieldValue = 922337203685477.58;

        /// <summary>
        /// Hold to maintain edited explanation value after cancel process
        /// </summary>
        private string editExplanation = string.Empty;

        /// <summary>
        /// Parcel number
        /// </summary>
        private string parcelNumber = string.Empty;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        #endregion

        #region Constructor

        public F2551()
        {
            InitializeComponent();
            this.EditPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EditPictureBox.Height, this.EditPictureBox.Width, "Edit Statement", 28, 81, 128);
           this.FormClose += new EventHandler<DataEventArgs<string>>(F2551_FormClose);
            
           // this.ParentForm.FormClosing += new FormClosingEventHandler(this.ParentForm_FormClosing);
           
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1031 control.
        /// </summary>
        /// <value>The F1031 control.</value>
        [CreateNew]
        public F2551Controller Form2551Control
        {
            get { return this.form2551Control as F2551Controller; }
            set { this.form2551Control = value; }
        }

        
        /// <summary>
        /// Gets or sets the current bool for IsEdit.
        /// </summary>
        /// <value>The Current bool for IsEdit.</value>
        public bool ISEdit
        {
            get { return this.isEdit; }
            set { this.isEdit = value; }
        }

        /// <summary>
        /// Gets or sets the current bool for IsClose.
        /// </summary>
        /// <value>The Current bool for IsClose.</value>
        public bool ISClose
        {
            get { return this.isClose; }
            set { this.isClose = value; }
        }

        #endregion Properities

        #region Event Publication
        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        ///<summary>
        ///Declare the event D11011_F15011_ReceiptId
        /// </summary>
        [EventPublication(EventTopicNames.D2550_F2551_StatementEditSaved, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> D2550_F2551_StatementEditSaved;

        /// <summary>
        /// To set the attachment and comment count
        /// </summary>
        ////Added by Latha
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_SetButtonText, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<AdditionalOperationCountEntity>> SetAttachmentCount;

        ///<summary>
        ///Declare the event D11011_F15011_ReceiptId
        /// </summary>
        [EventPublication(EventTopicNames.D2550_F2551_FormClose, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> D2550_F2551_FormClose;

        #endregion

        #region Event Scbscription

        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
             object[] optionalParams = e.Data;
             if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
             {
                 if (this.SaveButton.Enabled)
                 {
                     if (MessageBox.Show("Do you want to discard the changes?", "TerraScan T2", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                     {
                         // object[] optionalParams = e.Data;
                         if (optionalParams.Length > 0)
                         {
                             this.StatementID = Convert.ToInt32(optionalParams[0]);
                             this.ParcelID = Convert.ToInt32(optionalParams[1]);
                             this.OwnerID = Convert.ToInt32(optionalParams[2]);
                             this.TypeID = Convert.ToInt16(optionalParams[3]);
                             //this.ParcelNumber = Convert.ToInt32(optionalParams[4]);
                             this.parcelNumber = optionalParams[5].ToString();
                             this.PageLoadStatus = true;
                             this.LoadEditStatementDataGrid();

                             this.EditExplTextBox.Text = "";
                             this.ChangeButton.Text = "";
                             this.ChangeButton.Enabled = false;
                             this.ChangeComboBox.SelectedIndex = 0;
                             this.ChangeButton.Enabled = false;
                             this.ChangeExcessPanel.Visible = false;
                             this.ChangeBasePanel.Visible = false;
                             this.AdjustTaxPanel.Visible = false;
                          }
                     }

                 }
                 else
                 {
                     // object[] optionalParams = e.Data;
                     if (optionalParams.Length > 0)
                     {
                         this.StatementID = Convert.ToInt32(optionalParams[0]);
                         this.ParcelID = Convert.ToInt32(optionalParams[1]);
                         this.OwnerID = Convert.ToInt32(optionalParams[2]);
                         this.TypeID = Convert.ToInt16(optionalParams[3]);
                         //this.ParcelNumber = Convert.ToInt32(optionalParams[4]);
                         this.parcelNumber = optionalParams[5].ToString();
                         this.PageLoadStatus = true;
                         this.LoadEditStatementDataGrid();

                        // this.EditExplTextBox.Text = "";
                         this.ChangeButton.Text = "";
                         this.ChangeButton.Enabled = false;
                         this.ChangeComboBox.SelectedIndex = 0;
                         this.ChangeButton.Enabled = false;
                         this.ChangeExcessPanel.Visible = false;
                         this.ChangeBasePanel.Visible = false;
                         this.AdjustTaxPanel.Visible = false;
                     }
                 }
                // this.additionalOperationSmartPart.KeyId = this.ParcelID;
                
             }
        }

        private void F2551_FormClose(object sender, DataEventArgs<string> e)
        {
            if (this.SaveButton.Enabled)
            {
                //e.Cancel = true;
                DialogResult result = MessageBox.Show("Do you want to save the changes to Edit Statement?", "TerraScan T2", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result.Equals(DialogResult.Cancel))
                {
                    // Cancel the form close
                    this.canClose = false;
                    return;
                }
                else
                {
                    this.canClose = true;
                    if (result.Equals(DialogResult.Yes))
                    {
                        // If the user select 'Yes', Save the statement before close
                        this.SaveStatement();
                    }

                    if (this.canClose)
                    {
                        this.isClose = true; 
                        int[] tempArgs = new int[0];
                        this.D2550_F2551_FormClose(this, new DataEventArgs<int[]>(tempArgs));
                    }
                }
            }
            else
            {
                this.isClose = true;
                this.canClose = true;
                int[] tempArgs = new int[0];
                this.D2550_F2551_FormClose(this, new DataEventArgs<int[]>(tempArgs));
            }
        }
        /// <summary>
        /// Handles the FormClosing event of the ParentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        public void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.SaveButton.Enabled)
            {
                //e.Cancel = true;
                MessageBox.Show("Do you want to Save the changes to Edit Statement?", "TerraScan T2", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                e.Cancel = true; 
            }   
        }

        #endregion

        #region LoadWorkSpaces

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            //if (this.form2551Control.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            //{
            //    this.ReportsdeckWorkspace.Show(this.form2551Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            //}
            //else
            //{
            //    this.ReportsdeckWorkspace.Show(this.form2551Control.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
            //}
            if (this.form2551Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form2551Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form2551Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }
            ///Removed due to TSCO # 14038
            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            //if (this.form2551Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            //{
            //    this.CommentsdeckWorkspace.Show(this.form2551Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            //}
            //else
            //{
            //    this.CommentsdeckWorkspace.Show(this.form2551Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            //}

            ////set required variable - attachment and comment
            //this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form2551Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            //this.additionalOperationSmartPart.ParentWorkItem = this.form2551Control.WorkItem;
            //this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            //this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);
            this.formLabelInfo[0] = "Edit Statement"; ////SharedFunctions.GetResourceString("F1031FormHeader");
            this.formLabelInfo[1] = string.Empty;
            //sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form2551Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form2551Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form2551Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form2551Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form2551Control.WorkItem;
            this.footerSmartPart.FormId = "2551";
            this.footerSmartPart.AuditLinkText = "";//"tAA_Parcel [ParcelID] ";
            this.footerSmartPart.VisibleHelpButton = false;

            this.footerSmartPart.VisibleHelpLinkButton = true;

            this.footerSmartPart.TabStop = true;
            foreach (UserControl ctrl in this.FooterWorkspace.SmartParts)
            {
                if (ctrl != null)
                {
                    ctrl.TabStop = true;
                }
            }
        }
        #endregion LoadWorkSpaces

        /// <summary>
        /// Customizes the parcel details grid view.
        /// </summary>
        private void CustomizeEditStatementGridView()
        {
            this.EditStatementGridView.AutoGenerateColumns = false;
            this.TransactionID.DataPropertyName = this.editStatementRecordset.EditItemDataTable.TransactionIDColumn.ColumnName;
            this.Account.DataPropertyName = this.editStatementRecordset.EditItemDataTable.AccountColumn.ColumnName;
            this.AccountID.DataPropertyName = this.editStatementRecordset.EditItemDataTable.AccountIDColumn.ColumnName;
            this.Type.DataPropertyName = this.editStatementRecordset.EditItemDataTable.TypeColumn.ColumnName;
            this.ItemTypeID.DataPropertyName = this.editStatementRecordset.EditItemDataTable.ItemTypeIDColumn.ColumnName;
            this.Description.DataPropertyName = this.editStatementRecordset.EditItemDataTable.DescriptionColumn.ColumnName;
            this.Amount.DataPropertyName = this.editStatementRecordset.EditItemDataTable.AmountColumn.ColumnName;
            this.EditStatementGridView.PrimaryKeyColumnName = this.editStatementRecordset.EditItemDataTable.TransactionIDColumn.ColumnName;

            this.TransactionID.DisplayIndex = 0;
            this.Account.DisplayIndex = 1;
            this.AccountID.DisplayIndex = 2;
            this.Type.DisplayIndex = 3;
            this.ItemTypeID.DisplayIndex = 4;
            this.Description.DisplayIndex = 5;
            this.Amount.DisplayIndex = 6;
            this.EditStatementGridView.TabStop = true;
        }

        /// <summary>
        /// To Laod Parcel Ownership Grid
        /// </summary>
        private void LoadEditStatementDataGrid()
        {
            this.editStatementRecordset.Clear();
            this.editStatementRecordset = this.form2551Control.WorkItem.F2551_ListEditStatementDetails(this.ParcelID, this.TypeID, this.StatementID, this.OwnerID, TerraScanCommon.UserId);
            this.EditStatementGridView.DataSource = this.editStatementRecordset.EditItemDataTable.DefaultView;
            this.editStatementRecordset.EditItemDataTable.AcceptChanges();
            this.ScrollBarVisibility();
            this.PopulateHeaderEditStatementDetails();
            this.SaveButton.Enabled = false;
            this.CancelButton.Enabled = false;
            this.PageLoadStatus = false;
            this.EditStatementGridView.AllowSorting = true;
            this.CalculateTotalAmount();
            //this.EditExplTextBox.Text = "";
            this.ChangeButton.Text = "";
            this.ChangeButton.Enabled = false;
            this.ChangeComboBox.SelectedIndex = 0;
            this.ChangeButton.Enabled = false;
            this.ChangeExcessPanel.Visible = false;
            this.ChangeBasePanel.Visible = false;
            this.AdjustTaxPanel.Visible = false;
            if (Convert.ToBoolean(this.editStatementRecordset.StateLoad.Rows[0]["IsPermCropEnabled"]) == true)
            {
                this.CropValueLabel.Text = "Perm Crop Value:";
            }
            else
            {
                this.CropValueLabel.Text = "Outbuildings:";
            }
           // this.SetAttachmentText(this.additionalOperationCount);
        }

        private bool CheckErrors()
        {
            if (string.IsNullOrEmpty(this.EditExplTextBox.Text))
            {
                MessageBox.Show("This record cannot be saved because it is missing required fields.", "TerraScan T2 - Missing Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if(this.ChangeButton.Enabled)
            {
               DialogResult Change= MessageBox.Show("Do you want to discard Change Values?","TerraScan T2", MessageBoxButtons.YesNo,MessageBoxIcon.Warning );
               if (Change.Equals(DialogResult.No))
                {
                    return false;
                }
            }
            //if (this.isEdittemp.Equals(this.EditExplTextBox.Text.Trim()))
            //{
            //    MessageBox.Show("The Edit Explanation need to be modified for the Record.", "TerraScan - Modify Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            maxMoneyFieldValue = 922337203685477.58;

            if (922337203685477.58m < this.OriginalTaxTextBox.DecimalTextBoxValue)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.OriginalTaxTextBox.Focus();
                return false;
            }

            if (!this.IntegerValidation(this.LandValueTextBox))
            {
                return false; 
            }

            if (!this.IntegerValidation(this.ImprovementTextBox))
            {
                return false;
            }

            if (!this.IntegerValidation(this.CropTextBox))
            {
                return false;
            }

            if (!this.IntegerValidation(this.OriginalTextBox))
            {
                return false;
            }

            decimal maxDecimalValue = 99999999.9999m;

            if (!this.DeciamlValidation(this.AcresTextBox, maxDecimalValue))
            {
                return false;
            }

            if (!this.DeciamlValidation(this.IrrAcresTextBox, maxDecimalValue))
            {
                return false;
            }

            return true;
        }

        private bool IntegerValidation(TerraScanTextBox textContol)
        {
            TerraScanTextBox currentControl = (TerraScanTextBox)textContol;
            if (!string.IsNullOrEmpty(currentControl.Text.Trim()) && int.MaxValue < currentControl.DecimalTextBoxValue)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                textContol.Focus();
                return false;
            }

            return true;
        }

        private bool DeciamlValidation(TerraScanTextBox textContol, decimal maxValue)
        {
            TerraScanTextBox currentControl = (TerraScanTextBox)textContol;

            if (!string.IsNullOrEmpty(currentControl.Text.Trim()) && maxValue < currentControl.DecimalTextBoxValue)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                textContol.Focus();
                return false;
            }

            return true;
        }


        /// <summary>
        /// Sets the attachment text.
        /// </summary>
        /// <param name="additionalOperationCount">The additional operation count.</param>
        private void SetAttachmentText(AdditionalOperationCountEntity additionalOperationCount)
        {
            
            int currentParcelId = (int)this.ParcelID;
            this.additionalOperationSmartPart.KeyId = currentParcelId;
            AttachmentsData attachmentDataSet = new AttachmentsData();
            CommentsData commentDataSet = new CommentsData();
            attachmentDataSet.GetAttachmentItems.Clear();
            attachmentDataSet.GetAttachmentItems.Merge(this.form2551Control.WorkItem.GetAttachmentItems(2551, currentParcelId, TerraScan.Common.TerraScanCommon.UserId));
            commentDataSet = this.form2551Control.WorkItem.GetComments(currentParcelId, 2551, TerraScanCommon.UserId);
            this.additionalOperationCount.AttachmentCount = attachmentDataSet.GetAttachmentItems.Rows.Count;
            this.additionalOperationCount.CommentCount = commentDataSet.GetComments.Rows.Count;
            DataView tempDataView = new DataView(commentDataSet.GetComments);
            tempDataView.RowFilter = string.Concat(commentDataSet.GetComments.IsHighPriorityColumn.ColumnName, "= 'HIGH'");
            if (tempDataView.Count > 0)
            {
                this.additionalOperationCount.HighPriority = true;
            }
            else
            {
                this.additionalOperationCount.HighPriority = false;
            }

            this.SetAttachmentCount(this, new DataEventArgs<AdditionalOperationCountEntity>(additionalOperationCount));

        }


        /// <summary>
        /// Scrolls the bar visibility.
        /// </summary>
        private void ScrollBarVisibility()
        {
            if (this.EditStatementGridView.OriginalRowCount > this.EditStatementGridView.NumRowsVisible)
            {
                this.EditStatementVerticalScroll.Visible = false;
            }
            else
            {
                this.EditStatementVerticalScroll.Visible = true;
                this.EditStatementVerticalScroll.BringToFront();
            }
        }


        /// <summary>
        /// Edit Enabled event
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void EditEnabled(object sender, EventArgs e)
        {

            this.SaveButton.Enabled = true;
            this.CancelButton.Enabled = true;
            this.EditStatementGridView.AllowSorting = false;
        }


        /// <summary>
        ///  To Populates the parcel ownership data grid with Data form database.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulateHeaderEditStatementDetails()
        {
            if (this.editStatementRecordset.EditHeaderDatatable.Rows.Count > 0)
            {
                this.LandValueTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["LandValue"].ToString();
                this.ImprovementTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["ImprovementValue"].ToString();
                this.CropTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["PermCropValue"].ToString();
                this.AcresTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["Acres"].ToString();
                this.IrrAcresTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["IrrgAcres"].ToString();
                this.OriginalTaxTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["OriginalTax"].ToString();
                this.OriginalTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["OriginalValue"].ToString();
                // Manoj Kumar.P         Modified for the TSCO # 14038
                if (!string.IsNullOrEmpty(this.editStatementRecordset.EditHeaderDatatable.Rows[0]["EditExplanation"].ToString()))
                {
                    this.EditExplTextBox.Text = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["EditExplanation"].ToString();
                   // this.isEdittemp = this.editStatementRecordset.EditHeaderDatatable.Rows[0]["EditExplanation"].ToString();
                }
                else
                {
                    this.EditExplTextBox.Text = string.Empty;
                }
            }
            else
            {
                this.LandValueTextBox.Text = string.Empty;
                this.ImprovementTextBox.Text = string.Empty;
                this.CropTextBox.Text = string.Empty;
                this.AcresTextBox.Text = string.Empty;
                this.IrrAcresTextBox.Text = string.Empty;
                this.OriginalTaxTextBox.Text = string.Empty;
                this.OriginalTextBox.Text = string.Empty;
                this.EditExplTextBox.Text = string.Empty;
            }
                
                int rowind = this.EditStatementGridView.CurrentRowIndex;
                if (this.TypeID.Equals(0))
                {
                    this.SubHeaderLabel.Text = "Parcel: " + this.parcelNumber.Trim();
                }
                if (this.TypeID.Equals(1))
                {
                    this.SubHeaderLabel.Text = "Schedule: " + this.parcelNumber.Trim();
                }
                if (this.TypeID.Equals(2))
                {
                    this.SubHeaderLabel.Text = "State: " + this.parcelNumber.Trim();
                }

            

        }

        private void F2551_Load(object sender, EventArgs e)
        {
            try
            {
                this.SaveButton.Enabled = false;
                this.PageLoadStatus = true;
                this.LoadWorkSpaces();
                this.CustomizeEditStatementGridView();
                
                //this.EditExplTextBox.Text = "";
                //this.ChangeButton.Text = "";
                //this.ChangeButton.Enabled = false;
                //this.ChangeComboBox.SelectedIndex = 0;
                //this.ChangeButton.Enabled = false;
                //this.ChangeExcessPanel.Visible = false;
                //this.ChangeBasePanel.Visible = false;
                //this.AdjustTaxPanel.Visible = false;
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

        private void ChangeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.ChangeComboBox.SelectedIndex.Equals(0))
                {
                    this.ChangeButton.Text = "";
                    this.ChangeBaseTextBox.Text = "";
                    this.ChangeExcessTextBox.Text = "";
                    this.AdjustTaxTextBox.Text = "";
                    this.ChangeButton.Enabled = false;
                    this.ChangeExcessPanel.Visible = false;
                    this.ChangeBasePanel.Visible = false;
                    this.AdjustTaxPanel.Visible = false;
                    this.ChangeComboLabel.Text = "Change Type:";
                }
                else if (this.ChangeComboBox.SelectedIndex.Equals(1))
                {
                    this.ChangeBaseTextBox.Text = "";
                    this.ChangeExcessTextBox.Text = "";
                    this.AdjustTaxTextBox.Text = "";
                    this.ChangeButton.Text = "Change Taxes By Value";
                    this.ChangeExcessPanel.Visible = true;
                    this.ChangeBasePanel.Visible = true;
                    this.ChangeButton.Enabled = false;
                    this.AdjustTaxPanel.Visible = false;
                    this.ChangeComboLabel.Text = "Change Type:";
                }
                else
                    if (this.ChangeComboBox.SelectedIndex.Equals(2))
                    {
                        this.ChangeBaseTextBox.Text = "";
                        this.ChangeExcessTextBox.Text = "";
                        this.AdjustTaxTextBox.Text = "";
                        this.ChangeButton.Text = "Adjust Tax By Amount";
                        this.ChangeExcessPanel.Visible = false;
                        this.ChangeBasePanel.Visible = false;
                        this.ChangeButton.Enabled = false;
                        this.AdjustTaxPanel.Visible = true;
                        this.ChangeComboLabel.Text = "Change Type:";
                    }
                this.editMode(); 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.itemXml = string.Empty;
                this.changeXml = string.Empty;
                DataTable tempTable = new DataTable();
                foreach (DataColumn column in this.editStatementRecordset.EditItemDataTable.Columns)
                {
                    if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.TransactionIDColumn.ColumnName)
                    {
                        tempTable.Columns.Add(new DataColumn(column.ColumnName));
                    }

                    if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.AccountIDColumn.ColumnName)
                    {
                        tempTable.Columns.Add(new DataColumn(column.ColumnName));
                    }

                    if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.ItemTypeIDColumn.ColumnName)
                    {
                        tempTable.Columns.Add(new DataColumn(column.ColumnName));
                    }

                    if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.DescriptionColumn.ColumnName)
                    {
                        tempTable.Columns.Add(new DataColumn(column.ColumnName));
                    }

                    if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.AmountColumn.ColumnName)
                    {
                        tempTable.Columns.Add(new DataColumn(column.ColumnName));
                    }
                }

                foreach (DataRow dr in this.editStatementRecordset.EditItemDataTable.Rows)
                {
                    DataRow tempRow = tempTable.NewRow();
                    foreach (DataColumn column in this.editStatementRecordset.EditItemDataTable.Columns)
                    {
                        if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.TransactionIDColumn.ColumnName)
                        {
                            tempRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.AccountIDColumn.ColumnName)
                        {
                            tempRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.ItemTypeIDColumn.ColumnName)
                        {
                            tempRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.DescriptionColumn.ColumnName)
                        {
                            tempRow[column.ColumnName] = dr[column.ColumnName];
                        }

                        if (column.ColumnName == this.editStatementRecordset.EditItemDataTable.AmountColumn.ColumnName)
                        {
                            tempRow[column.ColumnName] = dr[column.ColumnName];
                        }
                    }
                    tempTable.Rows.Add(tempRow);
                }
                this.itemXml = TerraScanCommon.GetXmlString(tempTable);
                DataTable changeTable = new DataTable();
                System.Type typeInt32 = System.Type.GetType("System.Int32");
                System.Type moneyDecimal = System.Type.GetType("System.Decimal");
                changeTable.Columns.Add(new DataColumn("ChangeType", typeInt32));
                changeTable.Columns.Add(new DataColumn("BaseValue", typeInt32));
                changeTable.Columns.Add(new DataColumn("ExcessValue", typeInt32));
                changeTable.Columns.Add(new DataColumn("AddTax", moneyDecimal));
                DataRow changeRow = changeTable.NewRow();
                if (this.ChangeComboBox.SelectedIndex.Equals(1))
                {
                    changeRow["ChangeType"] = Convert.ToInt16(this.ChangeComboBox.SelectedIndex);
                    changeRow["BaseValue"] = Convert.ToInt32(this.ChangeBaseTextBox.Text.Replace(",", "").Trim());
                    changeRow["ExcessValue"] = Convert.ToInt32(this.ChangeExcessTextBox.Text.Replace(",", "").Trim());
                }
                else if (this.ChangeComboBox.SelectedIndex.Equals(2))
                {
                    changeRow["ChangeType"] = this.ChangeComboBox.SelectedIndex;
                    changeRow["AddTax"] = this.AdjustTaxTextBox.Text;
                }
                changeTable.Rows.Add(changeRow);
                this.changeXml = TerraScanCommon.GetXmlString(changeTable);
                this.editStatementRecordset.Clear();

                this.editStatementRecordset = this.form2551Control.WorkItem.F2551_LoadStatementGridDetails(this.ParcelID, this.TypeID, this.StatementID, this.OwnerID, this.itemXml, this.changeXml, TerraScanCommon.UserId);
                this.EditStatementGridView.DataSource = this.editStatementRecordset.EditItemDataTable.DefaultView;
                this.ChangeComboBox.SelectedIndex = 0;
                this.ChangeButton.Text = "";
                this.ChangeBaseTextBox.Text = "";
                this.ChangeExcessTextBox.Text = "";
                this.AdjustTaxTextBox.Text = "";
                this.ChangeButton.Enabled = false;
                this.ChangeExcessPanel.Visible = false;
                this.ChangeBasePanel.Visible = false;
                this.AdjustTaxPanel.Visible = false;
                this.CalculateTotalAmount();
                this.ScrollBarVisibility();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void EditStatementGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //try
            //{
            //    double amount = 0;
            //    if (this.EditStatementGridView.OriginalRowCount > 0)
            //    {
            //        for (int i = 0; i < this.EditStatementGridView.OriginalRowCount; i++)
            //        {
            //            decimal value;
            //            value = Convert.ToDecimal(this.EditStatementGridView.Rows[i].Cells["Amount"].Value.ToString());
            //            amount = amount + double.Parse(value.ToString());
            //        }
            //        //decimal.TryParse(this.editStatementRecordset.EditItemDataTable.Compute("SUM (Amount)", "1=1").ToString(), out amount);
            //        this.AmountLabel.Text = amount.ToString("#,##0.00");
            //        this.AmountLabel.ForeColor = Color.White;
            //    }
            //    else
            //    {
            //        this.AmountLabel.Text = string.Empty;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }


        private void EditStatementGridView_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                this.CalculateTotalAmount();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CalculateTotalAmount()
        {
            double amount = 0;
            if (this.EditStatementGridView.OriginalRowCount > 0)
            {
                for (int i = 0; i < this.EditStatementGridView.OriginalRowCount; i++)
                {
                    decimal value;
                    value = Convert.ToDecimal(this.EditStatementGridView.Rows[i].Cells["Amount"].Value.ToString());
                    amount = amount + double.Parse(value.ToString());
                }
                //decimal.TryParse(this.editStatementRecordset.EditItemDataTable.Compute("SUM (Amount)", "1=1").ToString(), out amount);

                if (amount <= 0)
                {
                    this.AmountLabel.Text = "(" + amount.ToString("#,##0.00").Replace("-", "") + ")";
                }
                else
                {
                    this.AmountLabel.Text = amount.ToString("#,##0.00");
                }
                this.AmountLabel.ForeColor = Color.White;
            }
            else
            {
                this.AmountLabel.Text = string.Empty;
            }
        }

        private void editMode()
        {
            this.isEdit = true; 
            this.SaveButton.Enabled = true;
            this.CancelButton.Enabled = true;
            this.EditStatementGridView.AllowSorting = false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveStatement();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SaveStatement()
        {
            try
            {
                if (CheckErrors())
                {
                    this.SaveButton.Enabled = false;
                    this.CancelButton.Enabled = false;
                    this.itemXml = string.Empty;
                    this.headerXml = string.Empty;
                    DataTable tempTable = new DataTable();
                    tempTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditItemDataTable.TransactionIDColumn.ColumnName));
                    tempTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditItemDataTable.AccountIDColumn.ColumnName));
                    tempTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditItemDataTable.ItemTypeIDColumn.ColumnName));
                    tempTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditItemDataTable.DescriptionColumn.ColumnName));
                    tempTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditItemDataTable.AmountColumn.ColumnName));

                    foreach (DataRow dr in this.editStatementRecordset.EditItemDataTable.Rows)
                    {
                        DataRow tempRow = tempTable.NewRow();
                        tempRow[this.editStatementRecordset.EditItemDataTable.TransactionIDColumn.ColumnName] = dr[this.editStatementRecordset.EditItemDataTable.TransactionIDColumn.ColumnName];
                        tempRow[this.editStatementRecordset.EditItemDataTable.AccountIDColumn.ColumnName] = dr[this.editStatementRecordset.EditItemDataTable.AccountIDColumn.ColumnName];
                        tempRow[this.editStatementRecordset.EditItemDataTable.ItemTypeIDColumn.ColumnName] = dr[this.editStatementRecordset.EditItemDataTable.ItemTypeIDColumn.ColumnName];
                        tempRow[this.editStatementRecordset.EditItemDataTable.DescriptionColumn.ColumnName] = dr[this.editStatementRecordset.EditItemDataTable.DescriptionColumn.ColumnName];
                        tempRow[this.editStatementRecordset.EditItemDataTable.AmountColumn.ColumnName] = dr[this.editStatementRecordset.EditItemDataTable.AmountColumn.ColumnName];

                        tempTable.Rows.Add(tempRow);
                    }
                    this.itemXml = TerraScanCommon.GetXmlString(tempTable);

                    DataTable headerTable = new DataTable();
                    //foreach (DataColumn column in this.editStatementRecordset.EditHeaderDatatable.Columns)
                    //{
                        headerTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditHeaderDatatable.LandValueColumn.ColumnName));
                        headerTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditHeaderDatatable.ImprovementValueColumn.ColumnName));
                        headerTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditHeaderDatatable.PermCropValueColumn.ColumnName));
                        headerTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditHeaderDatatable.AcresColumn.ColumnName));
                        headerTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditHeaderDatatable.IrrgAcresColumn.ColumnName));
                        headerTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditHeaderDatatable.OriginalTaxColumn.ColumnName));
                        headerTable.Columns.Add(new DataColumn(this.editStatementRecordset.EditHeaderDatatable.OriginalValueColumn.ColumnName));
                    //}
                    System.Type textstring = System.Type.GetType("System.String");
                    headerTable.Columns.Add(new DataColumn("EditExplanation", textstring));
                    DataRow headerRow = headerTable.NewRow();
                    string landvalue, improvementValue;
                    landvalue = this.LandValueTextBox.Text.Replace(",", "");
                    headerRow["LandValue"] = landvalue;
                    improvementValue = this.ImprovementTextBox.Text.Replace(",", "");
                    headerRow["ImprovementValue"] = improvementValue;
                    headerRow["PermCropValue"] = this.CropTextBox.Text.Replace(",","");//this.CropTextBox.DecimalTextBoxValue;
                    headerRow["Acres"] = this.AcresTextBox.Text.Replace(",", "");//this.AcresTextBox.DecimalTextBoxValue;//this.AcresTextBox.Text;
                    headerRow["IrrgAcres"] = this.IrrAcresTextBox.Text.Replace(",", "");//this.IrrAcresTextBox.DecimalTextBoxValue;//this.IrrAcresTextBox.Text;
                    headerRow["OriginalTax"] = this.OriginalTaxTextBox.Text.Replace(",", "");//this.OriginalTaxTextBox.DecimalTextBoxValue;
                    headerRow["OriginalValue"] = this.OriginalTextBox.Text.Replace(",", ""); //this.OriginalTextBox.DecimalTextBoxValue;
                    headerRow["EditExplanation"] = this.EditExplTextBox.Text;
                    headerTable.Rows.Add(headerRow);

                    this.headerXml = TerraScanCommon.GetXmlString(headerTable);
                    int returnval = this.form2551Control.WorkItem.SaveEditStatementtDetails(this.ParcelID, this.TypeID, this.StatementID, this.OwnerID, this.itemXml, this.headerXml, TerraScanCommon.UserId);
                    if (returnval >= 0)
                    {
                        this.LoadEditStatementDataGrid(); 
                        int[] tempArgs = new int[4];
                        tempArgs[0] = this.ParcelID;
                        tempArgs[1] = this.TypeID;
                        tempArgs[2] = this.StatementID;
                        tempArgs[3] = this.OwnerID;
                        this.D2550_F2551_StatementEditSaved(this, new DataEventArgs<int[]>(tempArgs));

                       editExplanation = this.EditExplTextBox.Text.Trim();
                    }
                    
                    // this.ParentForm.Close();
                }
                else
                {
                    this.canClose = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != '-')
            {

                Keys key = (Keys)e.KeyChar;
                if (!(key == Keys.Back || key == Keys.Delete))
                {
                    e.Handled = true;
                }

            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                //this.editStatementRecordset.EditItemDataTable.RejectChanges();
                //this.EditStatementGridView.DataSource = this.editStatementRecordset.EditItemDataTable.DefaultView;
                //this.PopulateHeaderEditStatementDetails();
                this.LoadEditStatementDataGrid();

                this.EditExplTextBox.Text = editExplanation;

                this.SaveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                this.ChangeButton.Text = "";
                this.ChangeButton.Enabled = false;
                this.ChangeComboBox.SelectedIndex = 0;
                this.ChangeButton.Enabled = false;
                this.ChangeExcessPanel.Visible = false;
                this.ChangeBasePanel.Visible = false;
                this.AdjustTaxPanel.Visible = false;
                this.EditStatementGridView.AllowSorting = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void EditStatementGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.PageLoadStatus)
                {
                    if (e.ColumnIndex == 6)
                    {
                        this.SaveButton.Enabled = true;
                        this.CancelButton.Enabled = true;
                        this.EditStatementGridView.AllowSorting = false;

                        if (this.EditStatementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                        {
                            decimal amountValue = 0;
                            decimal.TryParse(this.EditStatementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out amountValue);
                            if (922337203685477.58m < amountValue)
                            {
                                this.EditStatementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                            }
                        }

                        DataRow[] editstate = this.editStatementRecordset.EditItemDataTable.Select("TransactionID=" + this.EditStatementGridView.Rows[e.RowIndex].Cells["TransactionID"].Value.ToString());
                        if (editstate.Length > 0)
                        {
                            int rowindex = this.editStatementRecordset.EditItemDataTable.Rows.IndexOf(editstate[0]);
                            this.editStatementRecordset.EditItemDataTable.Rows[rowindex]["Amount"] = this.EditStatementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void EditStatementGridView_CellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.Value.Equals(null))
                {
                    return;
                }

                if (e.ColumnIndex.Equals(this.Amount.Index))
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(tempvalue))
                    {
                        decimal outDecimal;

                        // If the entered value ends with '.' append 00
                        if (tempvalue.EndsWith("."))
                        {
                            tempvalue = string.Concat(tempvalue, "00");
                        }

                        if (decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal))
                        {
                            tempvalue = outDecimal.ToString();

                            //if (tempvalue.Contains("-"))
                            //{
                            //    // Restrict negative values
                            //    outDecimal = decimal.Zero;
                            //}

                            if (outDecimal > 922337203685477.58m)
                            {
                                outDecimal = decimal.Zero;
                            }
                        }

                        e.Value = outDecimal;//outDecimal.ToString("#,##0.0000");
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = decimal.Zero;
                        e.ParsingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        
      private   void EditStatementGridView_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
          try
            {
            //// Only paint if text provided, Only paint if desired text is in cell 
                if (e.ColumnIndex.Equals(this.EditStatementGridView.Rows[e.RowIndex].Cells["Amount"].ColumnIndex) 
                    && e.Value != null && !String.IsNullOrEmpty(this.EditStatementGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString()))
                {
                    decimal outDecimal;
                    string val = e.Value.ToString();
                    if (Decimal.TryParse(val, out outDecimal))
                    {
                        if (outDecimal.ToString().Contains("-"))
                        {
                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00##"), ")");
                            e.CellStyle.ForeColor = Color.Green;
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = outDecimal.ToString("#,##0.00##");
                            e.FormattingApplied = true;
                        }
                    }
                    else
                    {
                        ////if (!this.ApplyInstrumentPayment)
                        ////{
                        e.Value = "0.00";
                        ////}
                        ////else
                        ////{
                        ////e.Value = this.ApplyInstrumentBalanceAmount;
                        ////this.instrumentPaymentsDataTable.Rows[e.RowIndex][e.ColumnIndex] = this.ApplyInstrumentBalanceAmount;
                        //////this.instrumentPaymentsDataTable.AcceptChanges();
                        //////this.instrumentPaymentsDataTable.AcceptChanges();
                        ////}
                    }
                }
                //else
                //{
                //    e.Value = "";
                //}
            }
           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void ChangeBaseTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChangeComboBox.SelectedIndex.Equals(1))
                {
                    if (!string.IsNullOrEmpty(this.ChangeBaseTextBox.Text) && !string.IsNullOrEmpty(this.ChangeExcessTextBox.Text))
                    {
                        this.ChangeButton.Enabled = true;
                    }
                    else
                    {
                        this.ChangeButton.Enabled = false;
                    }
                }
                this.IsBaseEdit = true;
                this.editMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ChangeExcessTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChangeComboBox.SelectedIndex.Equals(1))
                {
                    if (!string.IsNullOrEmpty(this.ChangeBaseTextBox.Text) && !string.IsNullOrEmpty(this.ChangeExcessTextBox.Text))
                    {
                        this.ChangeButton.Enabled = true;
                    }
                    else
                    {
                        this.ChangeButton.Enabled = false;
                    }
                }
                this.IsExcessEdit = true;
                this.editMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void AdjustTaxTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ChangeComboBox.SelectedIndex.Equals(2))
                {
                    if (!string.IsNullOrEmpty(this.AdjustTaxTextBox.Text))
                    {
                        this.ChangeButton.Enabled = true;
                    }
                    else
                    {
                        this.ChangeButton.Enabled = false;
                    }
                }
                this.IsAdjustEdit = true;
                this.editMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void AdjustTaxTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.IsAdjustEdit && !string.IsNullOrEmpty(this.AdjustTaxTextBox.Text))
                {
                    decimal Adjust = 0;
                    decimal.TryParse(this.AdjustTaxTextBox.Text.Replace(",", "").Trim(), out Adjust);
                    decimal minValue = -922337203685477.5808M;
                    decimal maxValue = 922337203685477.5807M;
                    if (Adjust > maxValue || Adjust < minValue)
                    {
                        this.AdjustTaxTextBox.Text = "0.00";//string.Empty;
                        this.AdjustTaxTextBox.Focus();
                    }
                    else
                    {
                        this.AdjustTaxTextBox.Text = Convert.ToString(Adjust);
                    }
                    this.IsAdjustEdit = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ChangeExcessTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.IsExcessEdit && !string.IsNullOrEmpty(this.ChangeExcessTextBox.Text))
                {
                    int excessValue = 0;
                    int.TryParse(this.ChangeExcessTextBox.Text.Replace(",", "").Trim(), out excessValue);
                    int maxint = 2147483647;
                    if (excessValue > maxint)
                    {
                        this.ChangeExcessTextBox.Text = string.Empty;
                        this.ChangeExcessTextBox.Focus();
                    }
                    else
                    {
                        if (this.ChangeExcessTextBox.DecimalTextBoxValue < 0)
                        {
                            this.ChangeExcessTextBox.Text = "0";
                        }
                        else
                        {
                            this.ChangeExcessTextBox.Text = excessValue.ToString();
                        }
                    }
                    this.IsExcessEdit = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ChangeBaseTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.IsBaseEdit && !string.IsNullOrEmpty(this.ChangeBaseTextBox.Text))
                {
                    int baseValue = 0;
                    int.TryParse(this.ChangeBaseTextBox.Text.Replace(",", "").Trim(), out baseValue);
                    int maxint = 2147483647;
                    if (baseValue > maxint)
                    {
                        this.ChangeBaseTextBox.Text = string.Empty;
                        this.ChangeBaseTextBox.Focus();
                    }
                    else
                    {
                        if (this.ChangeBaseTextBox.DecimalTextBoxValue < 0)
                        {
                            this.ChangeBaseTextBox.Text = "0";
                        }
                        else
                        {
                            this.ChangeBaseTextBox.Text = baseValue.ToString();
                        }
                    }
                    this.IsBaseEdit = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LandValueTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.IsLandEdit && !string.IsNullOrEmpty(this.LandValueTextBox.Text))
                {
                    long baseValue = 0;
                    long.TryParse(this.LandValueTextBox.Text.Replace(",", "").Trim(), out baseValue);
                    //baseValue = (int)this.LandValueTextBox.DecimalTextBoxValue; 
                    int maxint = 2147483647;
                    if (baseValue > maxint)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered value exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.LandValueTextBox.Text = string.Empty;
                        this.LandValueTextBox.Focus();
                    }
                    else
                    {
                        if (this.LandValueTextBox.DecimalTextBoxValue < 0)
                        {
                            this.LandValueTextBox.Text = "0";
                        }
                        else
                        {
                            this.LandValueTextBox.Text = baseValue.ToString();
                        }
                    }
                    this.IsLandEdit = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LandValueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.IsLandEdit = true;
                this.editMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ImprovementTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.IsImprovEdit = true;
                this.editMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ImprovementTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.IsImprovEdit && !string.IsNullOrEmpty(this.ImprovementTextBox.Text))
                {
                    long baseValue = 0;
                    long.TryParse(this.ImprovementTextBox.Text.Replace(",", "").Trim(), out baseValue);
                    int maxint = 2147483647;
                    if (baseValue > maxint)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered value exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ImprovementTextBox.Text = string.Empty;
                        this.ImprovementTextBox.Focus();
                    }
                    else
                    {
                        if (this.ImprovementTextBox.DecimalTextBoxValue < 0)
                        {
                            this.ImprovementTextBox.Text = "0";
                        }
                        else
                        {
                            this.ImprovementTextBox.Text = baseValue.ToString();
                        }
                    }
                    this.IsImprovEdit = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CropTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.IsPermEdit = true;
                this.editMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CropTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.IsPermEdit && !string.IsNullOrEmpty(this.CropTextBox.Text))
                {
                    long baseValue = 0;
                    long.TryParse(this.CropTextBox.Text.Replace(",", "").Trim(), out baseValue);
                    int maxint = 2147483647;
                    if (baseValue > maxint)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered value exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.CropTextBox.Text = string.Empty;
                        this.CropTextBox.Focus();
                    }
                    else
                    {
                        if (this.CropTextBox.DecimalTextBoxValue < 0)
                        {
                            this.CropTextBox.Text = "0";
                        }
                        else
                        {
                            this.CropTextBox.Text = baseValue.ToString();
                        }
                    }
                    this.IsPermEdit = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void OriginalTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.IsOriginalEdit = true;
                this.editMode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void OriginalTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.IsOriginalEdit && !string.IsNullOrEmpty(this.OriginalTextBox.Text))
                {
                    long baseValue = 0;
                    long.TryParse(this.OriginalTextBox.Text.Replace(",", "").Trim(), out baseValue);
                    int maxint = 2147483647;
                    if (baseValue > maxint)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered value exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.OriginalTextBox.Text = string.Empty;
                        this.OriginalTextBox.Focus();
                    }
                    else
                    {
                        if (baseValue < 0)
                        {
                            this.OriginalTextBox.Text = "0";
                        }
                        else
                        {
                            this.OriginalTextBox.Text = baseValue.ToString();
                        }
                    }
                    this.IsOriginalEdit = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void OriginalTaxTextBox_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.OriginalTextBox.Text))
                {
                    if (922337203685477.58m < this.OriginalTaxTextBox.DecimalTextBoxValue)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered value exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.OriginalTaxTextBox.Text = "0";
                        this.OriginalTaxTextBox.Focus();
                        // return false;
                    }
                    else if (this.OriginalTaxTextBox.DecimalTextBoxValue < 0)
                    {
                        this.OriginalTaxTextBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void AmountLabel_MouseEnter(object sender, System.EventArgs e)
        {
            try
            {
                this.TotalToolTip.SetToolTip(this.AmountLabel, this.AmountLabel.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void AcresTextBox_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    if (99999999.99m < this.AcresTextBox.DecimalTextBoxValue)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered value exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.AcresTextBox.Text = "0";
                        this.AcresTextBox.Focus();
                    }
                    else if (this.AcresTextBox.DecimalTextBoxValue < 0)
                    {
                        this.AcresTextBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void IrrAcresTextBox_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.IrrAcresTextBox.Text))
                {
                    if (99999999.99m < this.IrrAcresTextBox.DecimalTextBoxValue)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("Entered value exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.IrrAcresTextBox.Text = "0";
                        this.IrrAcresTextBox.Focus();
                    }
                    else if (this.IrrAcresTextBox.DecimalTextBoxValue < 0)
                    {
                        this.IrrAcresTextBox.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable reportOptionalParameter = new Hashtable();
                reportOptionalParameter.Add("ParcelID", this.ParcelID);
                reportOptionalParameter.Add("TypelID", this.TypeID);
                TerraScanCommon.ShowReport(255100, Report.ReportType.Print, reportOptionalParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable reportOptionalParameter = new Hashtable();
                reportOptionalParameter.Add("ParcelID", this.ParcelID);
                reportOptionalParameter.Add("TypelID", this.TypeID);
                TerraScanCommon.ShowReport(255100, Report.ReportType.Preview, reportOptionalParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

    }
}