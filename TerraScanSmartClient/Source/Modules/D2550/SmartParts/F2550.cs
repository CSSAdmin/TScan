//--------------------------------------------------------------------------------------------
// <copyright file="F2550.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2550.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Aug 06        JYOTHI              Created
// 17 Oct 07        LathaMaheswari.D    Modified (To set count of Attachment and Comment)
// 22 Nov 10        Manoj Kumar         Modified for the Co:8509
// 08 Nov 11        Manoj Kumar         Modified for the TSCO #14038
// 20120917         Manoj Kumar         Modified for the TSCo #17621  
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
    /// 2550
    /// </summary>
    [SmartPart]
    public partial class F2550 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Tax Roll Correction form2550Control Controller
        /// </summary>
        private F2550Controller form2550Control;

        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// ratesDetailsDataTable variable is used to get the details of rate listing Details.
        /// </summary>
        private F2550TaxRollCorrectionData taxRollCorrectionDataSet = new F2550TaxRollCorrectionData();

        private F2550TaxRollCorrectionData.ConfiguredStateDataTable ConfiguredStateTable = new F2550TaxRollCorrectionData.ConfiguredStateDataTable();
        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// currentExciseTaxStatementId variable is used to store ExciseTaxStatement id. 
        /// </summary>       
        private int? currentParcelId = null;

        /// <summary>
        /// Used to Hold the current row value of selected ParcelRecordGridView
        /// </summary>
        private int currentRow;

        /// <summary>
        /// Used to Hold the current row value of ParcelRecordGridView
        /// </summary>
        private bool gridSelected;

        /// <summary>
        /// checkedCount
        /// </summary>
        private int checkedCount;

        /// <summary>
        /// Used to Hold the current row value of ParcelRecordGridView
        /// </summary>
        private bool processStatus;

        /// <summary>
        /// Used to Hold the current row value of ParcelRecordGridView
        /// </summary>
        private bool recordExist;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

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

        ///<summary>
        /// EditStatementDataTable
        /// </summary>
        private F2550TaxRollCorrectionData.EditStatementDataTableDataTable editStatement = new F2550TaxRollCorrectionData.EditStatementDataTableDataTable();  
            

        /// <summary>
        /// Object for attachment typed dataset
        /// </summary>
        private AttachmentsData attachmentDataSet = new AttachmentsData();

        /// <summary>
        /// Created Integer for Attachment FormID
        /// </summary>
        private int attachmentFormID;

        /// <summary>
        /// Created Integer for Attachment keyID 
        /// </summary>
        private int attachmentKeyID;

        /// <summary>
        /// Created string to Find Extension 
        /// </summary>
        private string browsePathExt = string.Empty;

        /// <summary>
        /// to check file Exist
        /// </summary>        
        private bool fileExist;

        /// <summary>
        /// countChecked
        /// </summary>
        private bool countChecked;

        /// <summary>
        /// Created string for filePath
        /// </summary>
        private string filePath = string.Empty;

        /// <summary>
        /// Created string for fileID
        /// </summary>
        private string fileID = string.Empty;

        /// <summary>
        /// Created string for store ParcelIDs
        /// </summary>
        private string parcelIdXml = string.Empty;

        /// <summary>
        /// used to store Scheduleid. 
        /// </summary>   
        private int? currentScheduleId = null;

        /// <summary>
        /// used to store Setpreviewflag. 
        /// </summary>   
        private bool setpreviewflag = false;

        /// <summary>
        /// used to store sumOfOrigValue. 
        /// </summary>  
        private Decimal sumOfOrigValue;

        /// <summary>
        /// used to store sumOfCorrectedValue. 
        /// </summary>  
        private Decimal sumOfCorrectedValue;

        /// <summary>
        /// used to store flagColumnHeaderClicked. 
        /// </summary>  
        private bool columnCount;

        /// <summary>
        /// used to store parcelIds XML
        /// </summary>
        private string parcelIds = string.Empty;

        /// <summary>
        /// used to store scheduleIds XML
        /// </summary>
        private string scheduleIds = string.Empty;

        /// <summary>
        /// used to store stateIds XML
        /// </summary>
        private string stateIds = string.Empty;

        private string centralXmlIds = string.Empty;

        ///<summary>
        /// Used to hold the Current StatementId
        /// </summary>
        private string statementId;


        ///<summary>
        /// Used to hold the Current parcelid
        /// </summary>
        private int parcelid;

        ///<summary>
        /// use to hold the Current TypeID
        /// </summary>
        private short parcelTypeID;

        ///<summary>
        /// use to hold the Current OwnerId
        /// </summary> 
        private int ownerId;

        ///<Summary>
        /// Used to hold the State 
        /// </Summary>
        private bool IsState = false;
        ///<summary>
        /// use to store the Current 
        /// </summary>
        private int rowInde;

      ///<summary>
      /// used to store Edit Statement
      /// </summary>
        private string editStatementList;

        ///<summary>
        /// Used to hold the button Type
       /// </summary>
        private int buttonType;

        private bool isformclose = false;
        private bool isFormOpen = false;

        private int correctionCode = -1;

        private string stateConfigured = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2550"/> class.
        /// </summary>
        public F2550()
        {
            InitializeComponent();
            this.ParcelPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelPictureBox.Height, this.ParcelPictureBox.Width, "Parcels", 28, 81, 128);
            this.DetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DetailPictureBox.Height, this.DetailPictureBox.Width, "Detail", 28, 81, 128);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// To set the attachment and comment count
        /// </summary>
        ////Added by Latha
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_SetButtonText, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<AdditionalOperationCountEntity>> SetAttachmentCount;

        /// <summary>
        /// Optional Parameters Form event Publishing
        /// </summary>
        /// added by manoj
        /// used to Pass the Optional Parameters to Edit Statement
        /// <param name="sender"> The sender UserControl</param>
        /// <param name="e">The argument associated with the event</param>
        [EventPublication(EventTopics.D9001_ShellForm_SendOptionalParameters, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<object[]>> SendOptionalParameters;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1031 control.
        /// </summary>
        /// <value>The F1031 control.</value>
        [CreateNew]
        public F2550Controller Form2550Control
        {
            get { return this.form2550Control as F2550Controller; }
            set { this.form2550Control = value; }
        }

        /// <summary>
        /// Gets or sets the color of the high priority comment back.
        /// </summary>
        /// <value>The color of the high priority comment back.</value>
        public Color HighPriorityCommentColor
        {
            get { return this.highPriorityCommentColor; }
            set { this.highPriorityCommentColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the default comment button back.
        /// </summary>
        /// <value>The color of the default comment button back.</value>
        public Color DefaultCommentButtonBackColor
        {
            get
            {
                return this.defaultCommentButtonBackColor;
            }

            set
            {
                this.defaultCommentButtonBackColor = value;
                this.CommentAllButton.BackColor = this.defaultCommentButtonBackColor;
            }
        }

        /// <summary>
        /// Gets or sets the current statement id.
        /// </summary>
        /// <value>The current statement id.</value>
        private int? CurrentParcelId
        {
            get
            {
                return this.currentParcelId;
            }

            set
            {
                this.currentParcelId = value;
                if (this.additionalOperationSmartPart != null)
                {
                    this.additionalOperationSmartPart.KeyId = this.currentParcelId ?? -1;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current schedule id.
        /// </summary>
        /// <value>The current schedule id.</value>
        private int? CurrentScheduleId
        {
            get
            {
                return this.currentScheduleId;
            }

            set
            {
                this.currentScheduleId = value;
                if (this.additionalOperationSmartPart != null)
                {
                    this.additionalOperationSmartPart.KeyId = this.currentScheduleId ?? -1;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current schedule id.
        /// </summary>
        /// <value>The current schedule id.</value>
        public bool IsFormClose
        {
            get
            {
                return this.isformclose;
            }

            set
            {
                this.isformclose = value;
                
            }
        }
        #endregion

        #region Private Methods

        #region Issue Fixed - BugID:772 (Attachment files are not copied to new location)
        ////Added by Latha

        /// <summary>
        /// Used To upload the image to central location
        /// </summary>
        /// <param name="data"> The data to be uploaded.</param>
        /// <param name="strFileName"> The path of the file name.</param>
        private static void UpLoadImage(byte[] data, string strFileName)
        {
            string uploadFilePath = strFileName;
            if (!System.IO.Directory.Exists(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\"))))
            {
                // Create the directory as per the file path.
                System.IO.Directory.CreateDirectory(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\")));
            }

            // Used to paste the file in the specified directory.
            FileStream fileStream = new FileStream(uploadFilePath, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(data);
            binaryWriter.Close();
            fileStream.Close();
        }
        #endregion Issue Fixed - BugID:772 (Attachment files are not copied to new location)

        /// <summary>
        /// Customizes the parcel details grid view.
        /// </summary>
        private void CustomizeParcelDetailsGridView()
        {
            this.ParcelDetailsGridView.AutoGenerateColumns = false;
            this.ParcelNumber.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName;
            this.Year.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.RollYearColumn.ColumnName;
            this.District.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DistrictColumn.ColumnName;
            this.OrigValue.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.OrigValueColumn.ColumnName;
            this.NewValue.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.NewValueColumn.ColumnName;
            this.DistrictID.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DistrictIDColumn.ColumnName;
            this.ParcelID.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName;
            this.IsValid.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName;
            this.ParcelType.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName;
            this.Statement.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.StatementIDColumn.ColumnName;
            this.TypeID.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.TypeIDColumn.ColumnName;
            this.OwnerID.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.OwnerIDColumn.ColumnName;
            this.IsEdit.DataPropertyName = this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsEditColumn.ColumnName;     
            this.ParcelDetailsGridView.PrimaryKeyColumnName = "ParcelID";


            this.IsValid.DisplayIndex = 0;
            this.ParcelType.DisplayIndex = 1;
            this.ParcelNumber.DisplayIndex = 2;
            this.Year.DisplayIndex = 3;
            this.District.DisplayIndex = 4;
            this.OrigValue.DisplayIndex = 5;
            this.NewValue.DisplayIndex = 6;
            this.DistrictID.DisplayIndex = 7;
            this.ParcelID.DisplayIndex = 8;
            this.Statement.DisplayIndex = 9;
            this.TypeID.DisplayIndex = 10;
            this.OwnerID.DisplayIndex = 11;
            this.IsEdit.DisplayIndex = 12; 
 
            this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
            this.ParcelDetailsGridView.CurrentRow.Selected = false;
            this.ParcelDetailsGridView.Rows[this.ParcelDetailsGridView.CurrentCell.RowIndex].Selected = false;
        }

        /// <summary>
        /// Populates the parcel details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void PopulateParcelDetails(int currentRowIndex)
        {
            this.currentScheduleId = null;
            if (currentRowIndex >= 0)
            {
                this.taxRollCorrectionDataSet.EnforceConstraints = true;
              ///  this.taxRollCorrectionDataSet.Merge(this.form2550Control.WorkItem.F2550_ListParcelDetails(this.parcelIds, this.scheduleIds));
                this.taxRollCorrectionDataSet.Merge(this.form2550Control.WorkItem.F2550_ListParcelDetails(this.parcelIds, this.scheduleIds,null,null));

                if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count > 0)
                {
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                    if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    {
                        if (maxrowcount > 7)
                        {
                            this.ParcelsIncludedTextBox.Text = maxrowcount.ToString();
                        }

                        for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                        {
                            this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                            this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                            {
                                minrowcount = minrowcount + 1;
                            }

                        }
                    }

                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.EnableFormControls(true);
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    this.recordExist = true;
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                    this.SetText(this.additionalOperationCountEnt);
                    this.SetAttachmentText(this.additionalOperationCount);
                }
                else
                {
                    this.ClearParcelDetails();
                    this.EnableFormControls(false);
                }
            }
        }

        /// <summary>
        /// Adds the parcel details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void AddParcelDetails(int currentRowIndex)
        {
            //// this.currentScheduleId = null;
            if (currentRowIndex >= 0)
            {
                F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable dt = new F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable();

                dt.Clear();
               //// dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.CurrentParcelId, null).ListParcelDetailsTable;
                ///dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.parcelIds, null).ListParcelDetailsTable;
                ///used for stateID
                dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.parcelIds, null,null,null).ListParcelDetailsTable;

                if (this.ParcelDetailsGridView.OriginalRowCount < this.ParcelDetailsGridView.NumRowsVisible)
                {
                    for (int i = this.ParcelDetailsGridView.RowCount; i >= this.ParcelDetailsGridView.OriginalRowCount + 1; i--)
                    {
                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.RemoveAt(i - 1);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                   // ///used to clear edit Statement List
                   //if(!string.IsNullOrEmpty(this.editStatementList))
                   //{
                   //    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges(); 
                   //     DataRow [] undoRow =  this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit=TRUE");
                   //     if(undoRow.Length >0)
                   //    {    
                   //         if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                   //            {
                   //                this.editStatement.Clear();  
                   //                this.editStatementList = string.Empty;
                   //               for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                   //                 {
                   //                     if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                   //                     {
                   //                         this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;                                        }
                   //                 }
                              
                                 
                   //            }
                   //            else
                   //            {
                   //                this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                   //                return;
                   //            }

                   //        }
                   //    }
                
                   }
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.Merge(dt);
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                     
                    ////Coding added by biju on 2/11/2009 [for displaying #parcels textbox]
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "IsParcelSelected=1";
                    minrowcount = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.Count;
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "";

                    ////Commented by biju on 2/11/2009 [for displaying #parcels textbox]
                    ////if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    ////{
                    ////    if (maxrowcount > 7)
                    ////    {
                    ////        this.ParcelsIncludedTextBox.Text = maxrowcount.ToString();
                    ////    }

                    ////    for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                    ////    {
                    ////        //// this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                    ////        this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    ////        if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                    ////        {
                    ////            minrowcount = minrowcount + 1;
                    ////        }
                    ////    }
                    ////}

                    //////this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    ////if (minrowcount > this.checkedCount && countChecked == true)
                    ////{
                    ////    checkedCount++;
                    ////    this.ParcelsIncludedTextBox.Text = checkedCount.ToString();
                    ////}
                    ////else
                    ////{
                    ////    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    ////}
                    this.ProcessButton.Enabled = false;
                    this.EditButton.Enabled = false;
                    this.PreviewButton.Enabled = false; 
                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();

                    ////this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.RowCountLabel.Text = " of" + " " + maxrowcount.ToString();
                    this.EnableFormControls(true);
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    ////Added by Biju on 28/Oct/09 to show the attachment for the first record
                    this.SetAttachmentText(this.additionalOperationCount );
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                }
                else
                {
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                    ////this.ClearParcelDetails();
                    ////this.EnableFormControls(false);
                }
            
        }

        /// Modified for the CO: 8509
         /// <summary>
        /// Adds the state details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void AddStateDetails(int currentRowIndex)
        {
            if (currentRowIndex >= 0)
            {
                F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable dt = new F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable();
                dt.Clear();
                //// dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.CurrentParcelId, null).ListParcelDetailsTable;
                //dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.parcelIds, null).ListParcelDetailsTable;
                ///used for stateID
                dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(null, null, this.stateIds,null).ListParcelDetailsTable;

                if (this.ParcelDetailsGridView.OriginalRowCount < this.ParcelDetailsGridView.NumRowsVisible)
                {
                    for (int i = this.ParcelDetailsGridView.RowCount; i >= this.ParcelDetailsGridView.OriginalRowCount + 1; i--)
                    {
                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.RemoveAt(i - 1);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    //this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();  
                    //if (!string.IsNullOrEmpty(this.editStatementList))
                    //{
                    //    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges(); 
                    //    DataRow[] undoRow2 = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit = True");
                    //    if(undoRow2.Length >0)
                    //      {
                    //            if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //            {
                    //                this.editStatement.Clear();  
                    //                this.editStatementList = string.Empty;
                    //                for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                    //                {
                    //                    if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                    //                    {
                    //                        this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                    //                    }
                    //                }
                                        
                                    
                    //            }
                    //            else
                    //            {
                    //                this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                    //                return;
                    //            }

                    //        }
                        
                    //}
                    //DataTable ds = new DataTable();
                    //ds = dt.Clone();
                    //ds=dt.Columns.Remove[10];   
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.Merge(dt);
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                 
                    ////Coding added by biju on 2/11/2009 [for displaying #parcels textbox]
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "IsParcelSelected=1";
                    minrowcount = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.Count;
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "";

                    ////Commented by biju on 2/11/2009 [for displaying #parcels textbox]
                    ////if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    ////{
                    ////    if (maxrowcount > 7)
                    ////    {
                    ////        this.ParcelsIncludedTextBox.Text = maxrowcount.ToString();
                    ////    }

                    ////    for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                    ////    {
                    ////        //// this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                    ////        this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    ////        if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                    ////        {
                    ////            minrowcount = minrowcount + 1;
                    ////        }
                    ////    }
                    ////}

                    //////this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    ////if (minrowcount > this.checkedCount && countChecked == true)
                    ////{
                    ////    checkedCount++;
                    ////    this.ParcelsIncludedTextBox.Text = checkedCount.ToString();
                    ////}
                    ////else
                    ////{
                    ////    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    ////}
                    this.ProcessButton.Enabled = false;
                    this.EditButton.Enabled = false;
                    this.PreviewButton.Enabled = false; 
                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();

                    ////this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.RowCountLabel.Text = " of" + " " + maxrowcount.ToString();
                    this.EnableFormControls(true);
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    ////Added by Biju on 28/Oct/09 to show the attachment for the first record
                    this.SetAttachmentText(this.additionalOperationCount);
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                }
                else
                {
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                    this.ClearParcelDetails();
                    this.EnableFormControls(false);
                }
            }
        }

        /// Modified for the CO: 8509
        /// <summary>
        /// Populates the State details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void PopulateStateDetails(int currentRowIndex)
        {
            if (currentRowIndex >= 0)
            {
              ///this.taxRollCorrectionDataSet = this.form2550Control.WorkItem.F2550_ListParcelDetails(null, this.scheduleIds);
                 this.taxRollCorrectionDataSet = this.form2550Control.WorkItem.F2550_ListParcelDetails(null, null, this.stateIds,null);
                if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count > 0)
                {
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                    if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    {
                        if (maxrowcount > 7)
                        {
                            this.ParcelsIncludedTextBox.Text = maxrowcount.ToString();
                        }

                        for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                        {
                            this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                            this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                            {
                                minrowcount = minrowcount + 1;
                            }
                        }
                    }

                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.EnableFormControls(true);
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    this.recordExist = true;
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                    this.SetText(this.additionalOperationCountEnt);
                    this.SetAttachmentText(this.additionalOperationCount);
                }
                else
                {
                    this.ClearParcelDetails();
                    this.EnableFormControls(false);
                }
            }

        }


     /*  
      commented import all button disabled in visible
        /// <summary>
        /// Imports all parcel details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void ImportAllParcelDetails(int currentRowIndex)
        {
            if (currentRowIndex >= 0)
            {
                ///this.taxRollCorrectionDataSet = this.form2550Control.WorkItem.F2550_ListParcelDetails(null, null);
                this.taxRollCorrectionDataSet = this.form2550Control.WorkItem.F2550_ListParcelDetails(this.CurrentParcelId, this.CurrentScheduleId);
                if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count > 0)
                {
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                    if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    {
                        if (maxrowcount > 7)
                        {
                            this.ParcelsIncludedTextBox.Text = maxrowcount.ToString();
                        }

                        for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                        {
                            this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                            this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                            {
                                minrowcount = minrowcount + 1;
                            }
                        }
                    }

                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.EnableFormControls(true);
                    this.recordExist = true;
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    this.ParcelDetailsGridView.Rows[this.ParcelDetailsGridView.CurrentCell.RowIndex].Selected = true;
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                    this.SetText(this.additionalOperationCountEnt);
                    this.SetAttachmentText(this.additionalOperationCount);
                }
                else
                {
                    this.ClearParcelDetails();
                    this.EnableFormControls(false);
                    this.additionalOperationCount.AttachmentCount = 0;
                    this.additionalOperationCount.CommentCount = 0;
                    this.SetAttachmentCount(this, new DataEventArgs<AdditionalOperationCountEntity>(this.additionalOperationCount));
                }
            }
        }*/

        /// <summary>
        /// Scrolls the bar visibility.
        /// </summary>
        private void ScrollBarVisibility()
        {
            if (this.ParcelDetailsGridView.OriginalRowCount > this.ParcelDetailsGridView.NumRowsVisible)
            {
                this.ParcelsGridVscrollBar.Visible = false;
               // this.vScrollBar1.Visible = false;
                this.panel7.BorderStyle = BorderStyle.None;
                DataGridViewColumnCollection columns = this.ParcelDetailsGridView.Columns;
                columns[this.taxRollCorrectionDataSet.ListParcelDetailsTable.DistrictColumn.ColumnName].Width = 146;
            }
            else
            {
                this.ParcelsGridVscrollBar.Visible = true;
               // this.vScrollBar1.Visible = false;
                this.panel7.BorderStyle = BorderStyle.None;
                DataGridViewColumnCollection columns = this.ParcelDetailsGridView.Columns;
                columns[this.taxRollCorrectionDataSet.ListParcelDetailsTable.DistrictColumn.ColumnName].Width = 150;
            }
        }

        /// <summary>
        /// Displays the parcel header details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void DisplayParcelHeaderDetails(int currentRowIndex)
        {
            this.OriginalValueTextBox.Text = string.Empty;
            this.CorrectedValueTextBox.Text = string.Empty;
            this.TotalValueChangeTextBox.Text = string.Empty;

            if (currentRowIndex >= 0 && currentRowIndex <= this.ParcelDetailsGridView.OriginalRowCount)
            {
                if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells["ParcelID"].Value.ToString()))
                {
                    if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count > 0)
                    {
                        ////Coding added by malliga on 2/11/2009 [for performace issue]
                        ////this.sumOfOrigValue = 0;
                        ////this.sumOfCorrectedValue = 0;
                        DataTable sumCalculateDataTable = new DataTable();
                        IDataReader dataRead = this.taxRollCorrectionDataSet.ListParcelDetailsTable.CreateDataReader();
                        sumCalculateDataTable.Load(dataRead, LoadOption.OverwriteChanges);
                        sumCalculateDataTable.DefaultView.RowFilter = "IsParcelSelected=1";
                        sumCalculateDataTable = sumCalculateDataTable.DefaultView.ToTable("sumCalculateDataTable");

                        this.OriginalValueTextBox.Text = sumCalculateDataTable.Compute("SUM(OrigValue)", "").ToString();
                       
                        this.CorrectedValueTextBox.Text = sumCalculateDataTable.Compute("SUM(NewValue)", "").ToString();

                        this.TotalValueChangeTextBox.Text = (this.OriginalValueTextBox.DecimalTextBoxValue - this.CorrectedValueTextBox.DecimalTextBoxValue).ToString();
                       

                        ////Commented added by malliga on 2/11/2009 [for performace issue]
                        ////this.sumOfOrigValue = 0;
                        ////this.sumOfCorrectedValue = 0;
                        ////for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                        ////{
                        ////    if (Convert.ToBoolean(this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value))
                        ////    {
                        ////        ////string a = this.ParcelDetailsGridView.Rows[count].Cells["ParcelNumber"].Value.ToString();
                        ////        if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[count].Cells["OrigValue"].Value.ToString()))
                        ////        {
                        ////            Decimal origValue = Convert.ToDecimal(this.ParcelDetailsGridView.Rows[count].Cells["OrigValue"].Value.ToString());
                        ////            this.sumOfOrigValue = (origValue + this.sumOfOrigValue);
                        ////        }
                        ////        else
                        ////        {
                        ////            this.sumOfOrigValue = (0 + this.sumOfOrigValue);
                        ////        }

                        ////        this.OriginalValueTextBox.Text = this.sumOfOrigValue.ToString();

                        ////        Decimal totalNewValue = Convert.ToDecimal(this.ParcelDetailsGridView.Rows[count].Cells["NewValue"].Value.ToString());
                        ////        this.sumOfCorrectedValue = (totalNewValue + this.sumOfCorrectedValue);
                        ////        CorrectedValueTextBox.Text = this.sumOfCorrectedValue.ToString();

                        ////        Decimal totalValueChange;
                        ////        totalValueChange = (this.sumOfCorrectedValue - this.sumOfOrigValue);
                        ////        this.TotalValueChangeTextBox.Text = totalValueChange.ToString();

                        ////        //// decimal b = Convert.ToDecimal(this.taxRollCorrectionDataSet.ListParcelDetailsTable.OrigValueColumn.ToString());
                        ////    }
                        ////}

                        this.CurrentParcelId = Convert.ToInt32(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells["ParcelID"].Value.ToString());
                        decimal totalOrinValue = Convert.ToDecimal(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Compute("Sum(OrigValue)", "1=1").ToString());
                        //// this.OriginalValueTextBox.Text = totalOrinValue.ToString();
                        //// decimal totalNewValue = Convert.ToDecimal(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Compute("Sum(NewValue)", "1=1").ToString());
                        ////   this.CorrectedValueTextBox.Text = totalNewValue.ToString();

                        /////* decimal totalValueChange;
                        //// ////totalValueChange = totalOrinValue - totalNewValue;
                        //// /*Modified for bug fixing id:2624 - by kuppu*/
                        //// totalValueChange = totalNewValue - totalOrinValue;
                        //// this.TotalValueChangeTextBox.Text = totalValueChange.ToString();/*

                        if (this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName].Value.ToString() == "Parcel")
                        {
                            this.ParcelNumberLabel.Text = "Parcel Number:";
                        }
                        else
                            if (this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName].Value.ToString() == "State")
                            {
                                this.IsState = true; 
                                this.ParcelNumberLabel.Text = "Parcel Number:";
                            }
                            else
                        {
                            this.ParcelNumberLabel.Text = "Schedule Number:";
                        }

                        this.ParcelNumberLinkLabel.Text = this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString();
                        this.RollYearTextBox.Text = this.ParcelDetailsGridView.Rows[currentRowIndex].Cells["Year"].Value.ToString();
                        DataRow[] rowindex = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("ParcelID=" + this.CurrentParcelId);
                        if (rowindex.Length > 0)
                        {
                            this.RetiredTextBox.Text = rowindex[0]["IsRetired"].ToString();
                            this.ExemptTextBox.Text = rowindex[0]["IsExempt"].ToString();
                            this.LegalTextBox.Text = rowindex[0]["Legal"].ToString();
                            this.PrimaryOwnerTextBox.Text = rowindex[0]["OwnerName"].ToString();
                            //this.PrimaryOwnerLinkLabel.Text = rowindex[0]["OwnerName"].ToString();
                            //this.PrimarySitusLinkLabel.Text = rowindex[0]["Situs"].ToString();
                            this.PrimarySitusTextBox.Text = rowindex[0]["Situs"].ToString();
                            this.DORTextBox.Text = rowindex[0]["DOR"].ToString();
                            //this.DORLinkLabel.Text = rowindex[0]["DOR"].ToString();
                            if (!string.IsNullOrEmpty(rowindex[0]["ID1"].ToString()))
                            {
                                this.ID1Label.Text = rowindex[0]["ID1"].ToString();
                            }
                            else
                            {
                                this.ID1Label.Text = string.Empty; 
                            }
                            if (!string.IsNullOrEmpty(rowindex[0]["ID2"].ToString()))
                            {
                                this.ID2Label.Text = rowindex[0]["ID2"].ToString();
                            }
                            else
                            {
                                this.ID2Label.Text = string.Empty;
                            }
                             //this.ID1LinkLabel.Text = rowindex[0]["MID1"].ToString();
                             //this.ID2LinkLabel.Text = rowindex[0]["MID2"].ToString();
                             this.ID2TextBox.Text = rowindex[0]["MID2"].ToString();
                                this.ID1TextBox.Text  = rowindex[0]["MID1"].ToString();
                             this.AppraisedValueTextBox.Text = rowindex[0]["AppraisedValue"].ToString();
                             this.AssessedValueTextBox.Text = rowindex[0]["AssessedValue"].ToString();
                             this.TaxableValueTextBox.Text = rowindex[0]["TaxableValue"].ToString();
                             this.FillingDateTextBox.Text = rowindex[0]["FilingDate"].ToString();
                             this.NAICSTextBox.Text =rowindex[0]["NAICS"].ToString();
                             this.BusinessNameTextBox.Text =rowindex[0]["BuisnessName"].ToString();

                        }
                        //this.RetiredTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsRetiredColumn.ColumnName].ToString();
                        //this.ExemptTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsExemptColumn.ColumnName].ToString();
                        //this.DistrictTextBox.Text = this.ParcelDetailsGridView.Rows[currentRowIndex].Cells["District"].Value.ToString();

                        //this.LegalTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.LegalColumn.ColumnName].ToString();
                        //this.PrimaryOwnerLinkLabel.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.OwnerNameColumn.ColumnName].ToString();
                        //this.PrimarySitusLinkLabel.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.SitusColumn.ColumnName].ToString();
                        //this.DORLinkLabel.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.DORColumn.ColumnName].ToString();

                        //if (!string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ID1Column.ColumnName].ToString()))
                        //{
                        //    this.ID1Label.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ID1Column.ColumnName].ToString() + ":";
                        //}
                        //else
                        //{
                        //    this.ID1Label.Text = "";
                        //}

                        //if (!string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ID2Column.ColumnName].ToString()))
                        //{
                        //    this.ID2Label.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ID2Column.ColumnName].ToString() + ":";
                        //}
                        //else
                        //{
                        //    this.ID2Label.Text = "";
                        //}

                        //this.ID1LinkLabel.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.MID1Column.ColumnName].ToString();
                        //this.ID2LinkLabel.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.MID2Column.ColumnName].ToString();

                        //this.AppraisedValueTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.AppraisedValueColumn.ColumnName].ToString();
                        //this.AssessedValueTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.AssessedValueColumn.ColumnName].ToString();
                        //this.TaxableValueTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.TaxableValueColumn.ColumnName].ToString();

                        //this.FillingDateTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.FilingDateColumn.ColumnName].ToString();
                        ////Coding added for the issue 4649 by malliga on 30/10/2009
                        if (!string.IsNullOrEmpty(this.FillingDateTextBox.Text))
                        {
                            DateTime filldate;
                            filldate = DateTime.Parse(this.FillingDateTextBox.Text);
                            this.FillingDateTextBox.Text = filldate.ToShortDateString();
                        }

                        //this.NAICSTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.NAICSColumn.ColumnName].ToString();
                        //this.BusinessNameTextBox.Text = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.BuisnessNameColumn.ColumnName].ToString();
                        this.SubHeaderLabel.Text = this.ParcelNumberLinkLabel.Text.Trim() + " / ";
                        this.SubHeader1Label.Text = this.RollYearTextBox.Text.Trim();
                        //this.parcelTypeID = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.p.ColumnName].ToString();
                        //this.ownerId  =Convert.ToInt32(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.OwnerIDColumn.ColumnName].ToString());
                       //this.parcelTypeID = Convert.ToInt16 (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.TypeIDColumn.ColumnName].ToString());  
                       //this.statementId = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.StatementIDColumn.ColumnName].ToString();
                       //this.parcelid  =  Convert.ToInt32(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRowIndex][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName].ToString());                     
                        this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
                       //this.footerSmartPart.KeyId = this.CurrentParcelId;
                    }
                }
            }
        }

        /// <summary>
        /// Enables the form controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void EnableFormControls(bool enableValue)
        {
            ////Added by Biju on 28/Oct/09 to set the attachment count when it is enabled
            ////This is done because when import all is clicked, if no records are available, these buttons will be
            ////disabled. But while enabling, the attachment/comment count for 9999 should be retained.
            if (enableValue & !this.AttachmentAllButton.Enabled)
                SetAttachmentAllCount();
            this.CommentsdeckWorkspace.Enabled = enableValue;
            this.ParcelDetailsGridView.Enabled = enableValue;
            this.ClearButton.Enabled = enableValue;
            this.ParcelDetailsGridView.CurrentRow.Selected = enableValue;
            this.AttachmentAllButton.Enabled = enableValue;
            this.CommentAllButton.Enabled = enableValue;
            this.SelectAllButton.Enabled = enableValue;
            this.UnSelectAllButton.Enabled = enableValue;
           
             ////this.ParcelsGridVscrollBar.Visible = enableValue;
            //// this.ProcessButton.Enabled = enableValue;
        }

        /// <summary>
        /// Clears the parcel details.
        /// </summary>
        private void ClearParcelDetails()
        {
            this.CorrectionCodeComboBox.SelectedIndex = -1;
            this.ParcelsIncludedTextBox.Text = "0";
            ////  this.CorrectedValueTextBox.Text = string.Empty;

            this.RowCountLabel.Text = " of" + " " + "0";
            this.OriginalValueTextBox.Text = string.Empty;
            this.CorrectedValueTextBox.Text = string.Empty;
            this.TotalValueChangeTextBox.Text = string.Empty;
            this.CorrectionNoteTextBox.Text = string.Empty;

            this.ParcelNumberLinkLabel.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.RetiredTextBox.Text = string.Empty;
            this.ExemptTextBox.Text = string.Empty;
            this.DistrictTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.checkedCount = 0;
            this.countChecked = false;
            this.PrimaryOwnerTextBox.Text = string.Empty;
            //this.PrimaryOwnerLinkLabel.Text = string.Empty;
            //this.PrimarySitusLinkLabel.Text = string.Empty;
            this.PrimarySitusTextBox.Text = string.Empty;
            this.DORTextBox.Text = string.Empty;    
            //this.DORLinkLabel.Text = string.Empty;
            //this.ID1LinkLabel.Text = string.Empty;
            //this.ID2LinkLabel.Text = string.Empty;
            this.ID1TextBox.Text = string.Empty;
            this.ID2TextBox.Text = string.Empty;    
            this.FillingDateTextBox.Text = string.Empty;
            this.NAICSTextBox.Text = string.Empty;
            this.AppraisedValueTextBox.Text = string.Empty;
            this.AssessedValueTextBox.Text = string.Empty;
            this.TaxableValueTextBox.Text = string.Empty;
            this.BusinessNameTextBox.Text = string.Empty;

            this.taxRollCorrectionDataSet.ListParcelDetailsTable.Clear();
            this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
            DataGridViewColumnCollection columns = this.ParcelDetailsGridView.Columns;
            columns[this.taxRollCorrectionDataSet.ListParcelDetailsTable.DistrictColumn.ColumnName].Width = 150;
            this.CurrentParcelId = null;
            this.recordExist = false;
            this.footerSmartPart.KeyId = null;

            this.additionalOperationCount.AttachmentCount = 0;
            this.additionalOperationCount.CommentCount = 0;
            this.SetAttachmentCount(this, new DataEventArgs<AdditionalOperationCountEntity>(additionalOperationCount));

            this.AttachmentAllButton.Text = "Attachment All";
            this.CommentAllButton.Text = "Comment All";
            this.ProcessButton.Enabled = false;
            this.EditButton.Enabled = false;
            this.PreviewButton.Enabled = false; 
            this.ParcelNumberLabel.Text = "Parcel Number:";
            this.setpreviewflag = false;
            this.panel7.BorderStyle = BorderStyle.None;

            //// code added by khaja to Fix Bug #3929
            ////this.ParcelsGridVscrollBar.Visible = true;
            ////this.vScrollBar1.Visible = false;
            this.ScrollBarVisibility();

            //// code added by khaja to Fix Bug #3817(2)
            this.SubHeaderLabel.Text = string.Empty;
            this.SubHeader1Label.Text = string.Empty;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
            ////if not -999 reset text
            if (additionalOperationCountEntity.AttachmentCount != -999)
            {
                if (additionalOperationCountEntity.AttachmentCount <= 0)
                {
                    this.AttachmentAllButton.Text = "Attachment All";
                }
                else
                {
                    this.AttachmentAllButton.Text = "Attachment All" + "(" + additionalOperationCountEntity.AttachmentCount + ")";
                }
            }

            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentAllButton.Text = "Comment All";
                }
                else
                {
                    this.CommentAllButton.Text = "Comment All" + "(" + additionalOperationCountEntity.CommentCount + ")";
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    this.CommentAllButton.BackColor = this.highPriorityCommentColor;
                    this.CommentAllButton.CommentPriority = true;
                }
                else
                {
                    this.CommentAllButton.BackColor = this.defaultCommentButtonBackColor;
                    this.CommentAllButton.CommentPriority = false;
                }
            }
        }

        /// <summary>
        /// Sets the attachment text.
        /// </summary>
        /// <param name="additionalOperationCount">The additional operation count.</param>
        private void SetAttachmentText(AdditionalOperationCountEntity additionalOperationCount)
        {
            int currentParcelId = (int)this.CurrentParcelId;
            AttachmentsData attachmentDataSet = new AttachmentsData();
            CommentsData commentDataSet = new CommentsData();
            attachmentDataSet.GetAttachmentItems.Clear();
            attachmentDataSet.GetAttachmentItems.Merge(this.form2550Control.WorkItem.GetAttachmentItems(2550, currentParcelId, TerraScan.Common.TerraScanCommon.UserId));
            commentDataSet = this.form2550Control.WorkItem.GetComments(currentParcelId, 2550, TerraScanCommon.UserId);
            this.additionalOperationCount.AttachmentCount = attachmentDataSet.GetAttachmentItems.Rows.Count;
            this.additionalOperationCount.CommentCount = commentDataSet.GetComments.Rows.Count;
            DataView tempDataView = new DataView(commentDataSet.GetComments);
            //tempDataView.RowFilter = string.Concat(commentDataSet.GetComments.IsHighPriorityColumn.ColumnName, "= 'HIGH'");
            tempDataView.RowFilter = string.Concat(commentDataSet.GetComments.CommentPriorityIDColumn.ColumnName, " > 0");
            if (tempDataView.Count > 0)
            {
                this.additionalOperationCount.HighPriority = true;
            }
            else
            {
                this.additionalOperationCount.HighPriority = false;
            }

            this.SetAttachmentCount(this, new DataEventArgs<AdditionalOperationCountEntity>(additionalOperationCount));
            ////Added by Biju on 27/Oct/09 to make the performance better. For 9999 form related attachment/comment
            ////the DB call need not send for each row click
            ////this.SetAttachmentAllCount();
        }

        /// <summary>
        /// Sets the attachment all count.
        /// </summary>
        private void SetAttachmentAllCount()
        {
            object[] optionalParameter = new object[] { 9999, 0, 9999 };
            AttachmentsData attachmentAllDataSet = new AttachmentsData();
            CommentsData commentAllDataSet = new CommentsData();
            attachmentAllDataSet.GetAttachmentItems.Clear();
            attachmentAllDataSet.GetAttachmentItems.Merge(this.form2550Control.WorkItem.GetAttachmentItems(9999, 0, TerraScan.Common.TerraScanCommon.UserId));
            commentAllDataSet = this.form2550Control.WorkItem.GetComments(0, 9999, TerraScanCommon.UserId);
            this.additionalOperationCountEnt.AttachmentCount = attachmentAllDataSet.GetAttachmentItems.Rows.Count;
            this.additionalOperationCountEnt.CommentCount = commentAllDataSet.GetComments.Rows.Count;
            DataView tempDataView = new DataView(commentAllDataSet.GetComments);
            //tempDataView.RowFilter = string.Concat(commentAllDataSet.GetComments.IsHighPriorityColumn.ColumnName, "= 'HIGH'");
            tempDataView.RowFilter = string.Concat(commentAllDataSet.GetComments.CommentPriorityIDColumn.ColumnName, " > 0");
            if (tempDataView.Count > 0)
            {
                this.additionalOperationCountEnt.HighPriority = true;
            }
            else
            {
                this.additionalOperationCountEnt.HighPriority = false;
            }

            this.SetText(this.additionalOperationCountEnt);
        }

        #region Issue Fixed - BugID:772 (Attachment files are not copied to new location)
        ////Added by Latha

        /// <summary>
        /// Copy all the attached files 
        /// </summary>
        private void CopyAttachment()
        {
            this.parcelTypeDataset = this.form2550Control.WorkItem.F2550_ListAttachmentDetails(Convert.ToInt32("9999"), this.parcelIdXml, TerraScanCommon.UserId, Convert.ToInt32("2550"));
            if (this.parcelTypeDataset.ListAttachmentDetailsTable.Rows.Count > 0)
            {
                for (int i = 0; i < this.parcelTypeDataset.ListAttachmentDetailsTable.Rows.Count; i++)
                {
                    string fileTypeId = string.Empty;

                    this.attachmentKeyID = Convert.ToInt32(this.parcelTypeDataset.ListAttachmentDetailsTable.Rows[i][this.parcelTypeDataset.ListAttachmentDetailsTable.NewKeyIDColumn].ToString());
                    this.attachmentFormID = Convert.ToInt32(this.parcelTypeDataset.ListAttachmentDetailsTable.Rows[i][this.parcelTypeDataset.ListAttachmentDetailsTable.FormColumn].ToString());
                    this.browsePathExt = this.parcelTypeDataset.ListAttachmentDetailsTable.Rows[i][this.parcelTypeDataset.ListAttachmentDetailsTable.ExtensionColumn].ToString();
                    fileTypeId = this.parcelTypeDataset.ListAttachmentDetailsTable.Rows[i][this.parcelTypeDataset.ListAttachmentDetailsTable.FileTypeIDColumn].ToString();

                    this.attachmentDataSet.GetFilePath.Clear();
                    this.attachmentDataSet.GetFilePath.Merge(this.form2550Control.WorkItem.GetFilePath("TSFile", this.attachmentFormID, this.attachmentKeyID, this.browsePathExt));
                    this.filePath = this.attachmentDataSet.GetFilePath.Rows[0]["FilePath"].ToString();
                    this.fileID = this.attachmentDataSet.GetFilePath.Rows[0]["FileID"].ToString();
                    string tempFilePath = string.Empty;
                    tempFilePath = this.parcelTypeDataset.ListAttachmentDetailsTable.Rows[i][this.parcelTypeDataset.ListAttachmentDetailsTable.SourceColumn].ToString();

                    try
                    {
                        if (System.IO.File.Exists(tempFilePath))
                        {
                            FileStream fs = new FileStream(tempFilePath, FileMode.Open);
                            BinaryReader bR = new BinaryReader(fs);

                            // Upload the Image to the Central Location.
                            UpLoadImage(bR.ReadBytes((int)fs.Length), this.filePath);
                            this.fileExist = true;
                            bR.Close();
                            fs.Close();
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", tempFilePath, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.DeleteAttachmentAndComment();
            }
            else if (this.additionalOperationCountEnt.CommentCount > 0)
            {
                this.DeleteAttachmentAndComment();
            }
        }

        /// <summary>
        /// Deletes the attachment and comment for 9999.
        /// </summary>
        private void DeleteAttachmentAndComment()
        {
            this.form2550Control.WorkItem.F2550_DeleteAttachmentDetails(Convert.ToInt32("9999"));
            this.additionalOperationCountEnt.AttachmentCount = 0;
            this.additionalOperationCountEnt.CommentCount = 0;
            this.additionalOperationCountEnt.HighPriority = false;
            this.SetText(this.additionalOperationCountEnt);
        }
        #endregion Issue Fixed - BugID:772 (Attachment files are not copied to new location)


        private void GetConfiguredStateDetails()
        {
            this.ConfiguredStateTable = this.form2550Control.WorkItem.F2550_GetConfiguredState().ConfiguredState;
            if (this.ConfiguredStateTable.Rows.Count > 0)
            {
                this.stateConfigured = this.ConfiguredStateTable.Rows[0][0].ToString();
            }

        }
        private void ChangeButtomNameBasedOnState()
        {
            if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToLower().ToString().Equals("ne"))
            {
                this.AddStateButton.Text = "Add Central(s)";
            }
            else
            {
                this.AddStateButton.Text = "Add State(s)";
            }
        }
        #endregion

        #region Page Load

        /// <summary>
        /// Handles the Load event of the F2550 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F2550_Load(object sender, EventArgs e)
        {
            try
            {
                //Added by purushotham to implement TFS#21279 on 26Feb2015
                this.GetConfiguredStateDetails();
                this.LoadWorkSpaces();
                this.CustomizeParcelDetailsGridView();
                this.EnableFormControls(false);
                this.ParcelDetailsGridView.Enabled = false;
                this.footerSmartPart.KeyId = null;
                ////For Combox Box - Correction Code
                this.parcelTypeDataset = this.form2550Control.WorkItem.F2550_ListCorrectionCode();
                this.CorrectionCodeComboBox.DataSource = this.parcelTypeDataset.ListCorrectionCode;
                this.CorrectionCodeComboBox.DisplayMember = this.parcelTypeDataset.ListCorrectionCode.CorrectionCodeColumn.ColumnName;
                this.CorrectionCodeComboBox.ValueMember = this.parcelTypeDataset.ListCorrectionCode.CorrectionCodeIDColumn.ColumnName;
                this.CorrectionCodeComboBox.SelectedIndex = -1;
                this.ClearPanel.Enabled =false;
                this.ProcessPanel.Enabled =false;
                this.previewPanel.Enabled = false; 
                this.label3.Enabled = false;
                this.label7.Enabled = false;   
                this.RowCountLabel.Text = " of 0";
                //vScrollBar1.Visible = false;
                ////Added by Biju on 28/Oct/09 to delete the 9999 related attachments and comments on form load.
                DeleteAttachmentAndComment();
                //Added by purushotham to implement TFS#21279 on 26Feb2015
                this.ChangeButtomNameBasedOnState();
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

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the AddParcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddParcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                ///used to clear edit Statement List
                if (!string.IsNullOrEmpty(this.editStatementList))
                {
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                    DataRow[] undoRow = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit=TRUE");
                    if (undoRow.Length > 0)
                    {
                        if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.editStatement.Clear();
                            this.editStatementList = string.Empty;
                            for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                            {
                                if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                                {
                                    this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                                }
                            }

                            this.setpreviewOperation(); 
                        }
                        else
                        {
                            this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                            return;
                        }

                    }
                }
                ///Used to Modify the call form
                object[] optionalParameter = new object[] {"2550"};
                Form parcelSelectionForm = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1403, optionalParameter, this.form2550Control.WorkItem);

                if (parcelSelectionForm != null)
                {

                    if (parcelSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.parcelIds = TerraScanCommon.GetValue(parcelSelectionForm, "CommandResult");

                        ////Bining mulitple parcels
                        DataSet currentparcelDataTable = new DataSet();
                        currentparcelDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(this.parcelIds));
   
                        if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                        {
                            if (currentparcelDataTable.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] listparcelDataRow = currentparcelDataTable.Tables[0].Select();
                                                               
                                foreach (DataRow parcel in listparcelDataRow)
                                {
                                    if (!string.IsNullOrEmpty(parcel.ItemArray[0].ToString()))
                                    {
                                        DataRow[] parcelId = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("ParcelID=" + parcel.ItemArray[0].ToString() + " AND ParcelType = 'Parcel'");

                                        if (parcelId.Length > 0)
                                        {
                                            if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistParcel"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                               //// this.PopulateParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                                this.AddParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                            }
                        }
                        else
                        {
                            this.AddParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                        }
                                                                       
                        
                        ////this.CurrentParcelId = Convert.ToInt32(TerraScanCommon.GetValue(parcelSelectionForm, "ParcelID").ToString());

                        ////////Check Duplicate Record

                        ////DataView parcelDataView = new DataView(this.taxRollCorrectionDataSet.ListParcelDetailsTable);
                        ////parcelDataView.RowFilter = "ParcelID=" + this.CurrentParcelId;
                        ////if (parcelDataView.Count <= 0)
                        ////{
                        ////    if (this.CurrentParcelId.HasValue && this.recordExist == false)
                        ////    {
                        ////        this.PopulateParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                        ////    }
                        ////    else
                        ////    {
                        ////        this.AddParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                        ////    }
                        ////}
                        ////else if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistParcel"), "Terrascan - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        ////{
                        ////    return;
                        ////}
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                this.ClearParcelDetails();
                ///used to clear edit Statement List
                this.editStatement.Clear();  
                this.editStatementList = string.Empty;  
                ////Added by Biju on 27/Oct/09 to delete the attachment/comment mapped to 9999.
                DeleteAttachmentAndComment();
                this.EnableFormControls(false);
                this.additionalOperationCount.AttachmentCount = 0;
                this.additionalOperationCount.CommentCount = 0;
                ///used to set the edit button behaviour
                this.EditButton.Enabled = false;  
                this.SetAttachmentCount(this, new DataEventArgs<AdditionalOperationCountEntity>(this.additionalOperationCount));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
                /// <summary>
        /// D2550_F2551_StatementEditSaved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D2550_F2551_StatementEditSaved, ThreadOption.UserInterface)]
        public void D2550_F2551_StatementEditSaved(object sender, DataEventArgs<int[]> eventArgs)
        {
            //if (eventArgs.Data.Length ==0)
            //{
            //    this.MasterbuttonEnable(true);
            //}
            //else
            //{
                if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count > 0)
                {

                    DataRow[] dr = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("ParcelID=" + eventArgs.Data[0]);
                    if (dr.Length > 0)
                    {
                        for (int i = 0; i < this.ParcelDetailsGridView.OriginalRowCount; i++)
                        {
                            if (this.ParcelDetailsGridView.Rows[i].Cells["ParcelID"].Value.Equals(+eventArgs.Data[0]))
                            {
                                this.ParcelDetailsGridView.Rows[i].Cells["IsEdit"].Value = 1;
                            }
                        }
                        //  this.rowInde = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.IndexOf(dr[0]);
                        //this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[this.rowInde]["IsEdit"] = true;
                        //////this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                        // this.ParcelDetailsGridView.Rows[this.rowInde].Cells["IsEdit"].Value  = 1;
                        DataRow[] updateRow = this.editStatement.Select("StatementID=" + eventArgs.Data[2] +" AND OwnerID=" +eventArgs.Data[3]);
                        if (updateRow.Length <= 0)
                        {
                            DataRow tempRow = this.editStatement.NewRow();
                            tempRow["ParcelID"] = eventArgs.Data[0];
                            tempRow["TypeID"] = eventArgs.Data[1];
                            tempRow["StatementID"] = eventArgs.Data[2];
                            tempRow["OwnerID"] = eventArgs.Data[3];
                            tempRow["IsEdit"] = true;
                            this.editStatement.Rows.Add(tempRow);
                        }
                        this.editStatementList = TerraScanCommon.GetXmlString(this.editStatement);
                        //this.MasterbuttonEnable(true);  
                    }
                   // this.MasterbuttonEnable(true);
                }
            //}
            
        }

        [EventSubscription(EventTopicNames.D2550_F2551_FormClose, ThreadOption.UserInterface)]
        public void D2550_F2551_FormClose(object sender, DataEventArgs<int[]> eventArgs)
        {
            this.isFormOpen = false;  
            if (eventArgs.Data.Length == 0)
            {
                this.MasterbuttonEnable(true);

                for (int i = 0; i < this.ParcelDetailsGridView.OriginalRowCount; i++)
                {
                    this.ParcelDetailsGridView.Rows[i].Cells["IsValid"].ReadOnly = false;  
                }
                this.CorrectionCodeComboBox.Enabled = true;  
            }
        }

      /*  /// <summary>
        /// Handles the Click event of the ImportAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ImportAllParcelDetails(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }*/

        /// <summary>
        /// Handles the RowEnter event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if(e.ColumnIndex !=0)
                //{
                    if (this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value.Equals(false))
                    {
                        this.EditButton.Enabled = false;
                    }
                    else
                    {
                        if (this.ProcessButton.Enabled)
                        {
                            this.EditButton.Enabled = true;
                        }
                        else if (!this.ProcessButton.Enabled && !this.ClearButton.Enabled)
                        {
                            this.EditButton.Enabled = true;
                        }
                        else
                        {
                            this.EditButton.Enabled = false;
                        }
                    }

                    if (!this.processStatus)
                    {
                        if (this.currentRow != e.RowIndex)
                        //if (this.currentRow != e.RowIndex && e.ColumnIndex > 0) 
                        {
                            this.currentRow = e.RowIndex;
                            this.DisplayParcelHeaderDetails(this.currentRow);
                            this.SetAttachmentText(this.additionalOperationCount);
                            ////Commented by Biju on 28/Oct/09 to fire the attachment and parcel details method 
                            ////properly for all the row entering event.
                           //// this.processStatus = true;
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //// 
        }

        /// <summary>
        /// Handles the KeyDown event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 40 || e.KeyValue == 38)
                {
                    this.gridSelected = true;
                    ////int rowSelected = this.ParcelDetailsGridView.CurrentRow.Index;
                    this.DisplayParcelHeaderDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex );
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo hit = this.ParcelDetailsGridView.HitTest(e.X, e.Y);

                if (hit.RowIndex > -1 && hit.RowIndex != this.ParcelDetailsGridView.CurrentCell.RowIndex)
                {
                    this.DisplayParcelHeaderDetails(this.currentRow);
                    this.gridSelected = true;
                    this.processStatus = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AttachmentAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { 9999, 0, 9999 };

                Form attachmentForm = new Form();
                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.additionalOperationSmartPart.CurrntFormId).openPermission))
                {
                    attachmentForm = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.additionalOperationSmartPart.ParentWorkItem);
                    attachmentForm.Tag = this.additionalOperationSmartPart.CurrntFormId;
                    if (attachmentForm != null)
                    {
                        attachmentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        ////AdditionalOperationCountEntity additionalOperationCountEnt;
                        ////this.additionalOperationCountEnt = new AdditionalOperationCountEntity(-9999, -9999, false);
                        this.additionalOperationCountEnt.AttachmentCount = Convert.ToInt32(TerraScanCommon.GetValue(attachmentForm, "AttachmentCount"));
                        this.SetText(this.additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Handles the Click event of the CommentAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter;

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(2550).openPermission))
                {
                    optionalParameter = new object[] { 9999, 0, 9999 };

                    Form commentForm = new Form();
                    commentForm = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.additionalOperationSmartPart.ParentWorkItem);
                    commentForm.Tag = this.additionalOperationSmartPart.CurrntFormId; //9999;

                    if (commentForm != null)
                    {
                        commentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        ////AdditionalOperationCountEntity additionalOperationCountEnt;
                        ////this.additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        this.additionalOperationCountEnt.CommentCount = Convert.ToInt32(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                        this.additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                        this.SetText(this.additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Handles the LinkClicked event of the ParcelNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int currentRow = this.ParcelDetailsGridView.CurrentRowIndex;
                if (this.ParcelDetailsGridView.Rows[currentRow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName].Value.ToString() == "Parcel")
                //if (this.ParcelNumberLabel.Text == "Parcel Number:")
                {
                    ////check for valid value
                   if (this.CurrentParcelId > 0)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        //Changed the CurrentParcelId to HyperLinkKeyId 
                        int hyperLinkKeyID;
                        int.TryParse(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRow]["HyperLinkKeyID"].ToString(), out hyperLinkKeyID);
                        ////Statement Management Form - FormID - 20000 With the CurrentParcelId
                        FormInfo formInfo = TerraScanCommon.GetFormInfo(30000);
                        formInfo.optionalParameters = new object[] { hyperLinkKeyID };
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }

                if (this.ParcelDetailsGridView.Rows[currentRow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName].Value.ToString() == "Schedule")
                {
                    if (this.CurrentParcelId > 0) ////Coding modified for the issue : 4648 by malliga on 30/10/2009
                    {
                        this.Cursor = Cursors.WaitCursor;
                        //Changed the CurrentParcelId to HyperLinkKeyId 
                        int hyperLinkKeyID;
                        int.TryParse(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRow]["HyperLinkKeyID"].ToString(), out hyperLinkKeyID);
                        ////Statement Management Form - FormID - 20050 With the CurrentScheduleId
                        FormInfo formInfo = TerraScanCommon.GetFormInfo(30050);
                        formInfo.optionalParameters = new object[] { hyperLinkKeyID };
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }
                if (this.ParcelDetailsGridView.Rows[currentRow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName].Value.ToString() == "State")
                {
                    if (this.CurrentParcelId > 0) 
                    {
                        this.Cursor = Cursors.WaitCursor;
                        //Changed the CurrentParcelId to HyperLinkKeyId 
                        int stateId;
                        int.TryParse(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentRow]["HyperLinkKeyID"].ToString() , out stateId);
                            
                        ////Statement Management Form - FormID - 20050 With the CurrentScheduleId
                        FormInfo formInfo = TerraScanCommon.GetFormInfo(30075);
                        formInfo.optionalParameters = new object[] { stateId };
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
        /// Handles the LinkClicked event of the PrimaryOwnerLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void PrimaryOwnerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.CurrentParcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Statement Management Form - FormID - 22006 With the CurrentParcelId
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(22006);
                    formInfo.optionalParameters = new object[] { this.CurrentParcelId };
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
        /// Handles the LinkClicked event of the PrimarySitusLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void PrimarySitusLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.CurrentParcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Statement Management Form - FormID - 20003 With the CurrentParcelId
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(20003);
                    formInfo.optionalParameters = new object[] { this.CurrentParcelId };
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

     /*   /// <summary>
        /// Handles the LinkClicked event of the DORLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void DORLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.CurrentParcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ////Statement Management Form - FormID - 20005 With the CurrentParcelId
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(20005);
                    formInfo.optionalParameters = new object[] { this.CurrentParcelId };
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
*/
        /// <summary>
        /// Handles the LinkClicked event of the ID1LinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ID1LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.CurrentParcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ////Statement Management Form - FormID - 20008 With the CurrentParcelId
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(20008);
                    formInfo.optionalParameters = new object[] { this.CurrentParcelId };
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
        /// Handles the LinkClicked event of the ID2LinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ID2LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.CurrentParcelId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(20008);
                    formInfo.optionalParameters = new object[] { this.CurrentParcelId };
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
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                {
                    for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                    {
                        this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                        this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    this.ParcelDetailsGridView.RefreshEdit();
                    this.ParcelsIncludedTextBox.Text = this.ParcelDetailsGridView.OriginalRowCount.ToString();
                    this.ProcessButton.Enabled = this.PermissionEdit;
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
        /// Handles the Click event of the UnSelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnSelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                {
                    for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                    {
                        this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "False";
                        this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    this.ParcelDetailsGridView.RefreshEdit();
                    this.ParcelsIncludedTextBox.Text = "0";
                    ////   this.CorrectedValueTextBox.Text = string.Empty;
                    this.ProcessButton.Enabled = false;
                    this.EditButton.Enabled = false;
                    this.PreviewButton.Enabled = false;
 
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
        /// Handles the Click event of the ProcessButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                {
          
                if (this.buttonType.Equals (0))
                {
                    //if (!string.IsNullOrEmpty(this.editStatementList))
                    //{
                    //    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();  
                    //    DataRow [] undoRow =  this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit=TRUE");
                    //    if(undoRow.Length >0)
                    //    {
                    //           if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //            {
                    //                this.editStatementList = string.Empty;
                    //                for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                    //                {
                    //                    if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                    //                    {
                    //                        this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                    //                return;
                    //            }

                    //        }
                    
                    //    }

                   }
                    int selectedRecCount = 0;
                    if (this.setpreviewflag)
                    {
                        //// Added the code to fix the issue #3817(3)
                        this.setpreviewflag = false;
                        this.checkedCount = 0;
                        this.countChecked = false;
                        this.Cursor = Cursors.WaitCursor;
                        this.parcelIdXml = string.Empty;
                        ////bool duplicateFlag = false;

                        DataTable tempTable = new DataTable();

                        foreach (DataColumn column in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Columns)
                        {
                            if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName)
                            {
                                tempTable.Columns.Add(new DataColumn(column.ColumnName));
                            }

                            if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName)
                            {
                                tempTable.Columns.Add(new DataColumn(column.ColumnName));
                            }

                            if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName)
                            {
                                tempTable.Columns.Add(new DataColumn(column.ColumnName));
                            }
                        }

                        foreach (DataRow dr in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows)
                        {
                            DataRow tempRow = tempTable.NewRow();

                            if (dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName] != DBNull.Value && Convert.ToBoolean(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName]).Equals(true))
                            {
                                if (Convert.ToBoolean(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName]) && Convert.ToInt32(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsDuplicateColumn.ColumnName]).Equals(0))
                                {
                                    foreach (DataColumn column in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Columns)
                                    {
                                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName)
                                        {
                                            tempRow[column.ColumnName] = dr[column.ColumnName];
                                        }

                                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName)
                                        {
                                            tempRow[column.ColumnName] = dr[column.ColumnName];
                                        }

                                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName)
                                        {
                                            tempRow[column.ColumnName] = dr[column.ColumnName];
                                        }
                                      
                                    }

                                    tempTable.Rows.Add(tempRow);
                                }
                            }
                           
                        }
                        //Uncheck Isparcel selection
                        if (tempTable.Rows.Count > 0)
                        {
                            this.parcelIdXml = TerraScanCommon.GetXmlString(tempTable);
                            this.form2550Control.WorkItem.F2550_ExecTaxRollCorrections(this.parcelIdXml, TerraScanCommon.UserId);

                            this.CopyAttachment();
                            ///Used for Clear Edited Data
                            this.editStatementList = string.Empty;
                            this.editStatement.Clear();

                            if (MessageBox.Show("The Parcel Correction process completed successfully.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                                for (int i = 0; i < this.ParcelDetailsGridView.OriginalRowCount; i++)
                                {
                                    ////DataTable tmpstmttable;
                                    ////tmpstmttable = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Copy();
                                    //////tmpstmttable = statementDataTable;
                                    ////for (int j = 0; j <= tmpstmttable.Rows.Count - 1; j++)
                                    ////{
                                    ////    if (tmpstmttable.Rows[j]["IsParcelSelected"].ToString() == "True")
                                    ////    {
                                    ////        tmpstmttable.Rows[j].Delete();
                                    ////    }
                                    ////}

                                    if (Convert.ToBoolean(this.ParcelDetailsGridView.Rows[i].Cells["IsValid"].Value.ToString()))
                                    {
                                        this.processStatus = true;
                                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                                        this.ParcelDetailsGridView.Rows.RemoveAt(i);
                                        ////this.taxRollCorrectionDataSet.ListAttachmentDetailsTable.Rows[i].Delete();
                                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                                        this.ParcelDetailsGridView.NumRowsVisible = 7;

                                        if (i == 1)
                                        {
                                            i = i - 1;
                                        }
                                        else if (i == 0)
                                        {
                                            i = -1;
                                        }
                                        else
                                        {
                                            i = i - 2;
                                        }
                                    }
                                }

                                this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                                this.ProcessButton.Enabled = false;
                                this.PreviewButton.Enabled = false;
                                //Changes for the form Uncheck Edit
                                this.EditButton.Enabled = false; 
                                this.ParcelsIncludedTextBox.Text = this.ParcelDetailsGridView.OriginalRowCount.ToString();
                                this.RowCountLabel.Text = " of" + " " + this.ParcelDetailsGridView.OriginalRowCount.ToString();
                            }
                        }
                    }

                    for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                    {
                        this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                        this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.EditButton.Enabled = true; 
                    }

                    this.sumOfOrigValue = 0;
                    this.sumOfCorrectedValue = 0;
                    for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                    {
                        if (Convert.ToBoolean(this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value))
                        {
                            ////string a = this.ParcelDetailsGridView.Rows[count].Cells["ParcelNumber"].Value.ToString();
                            Decimal origValue = Convert.ToDecimal(this.ParcelDetailsGridView.Rows[count].Cells["OrigValue"].Value.ToString());
                            this.sumOfOrigValue = (origValue + this.sumOfOrigValue);
                            this.OriginalValueTextBox.Text = this.sumOfOrigValue.ToString();

                            Decimal totalNewValue = Convert.ToDecimal(this.ParcelDetailsGridView.Rows[count].Cells["NewValue"].Value.ToString());
                            this.sumOfCorrectedValue = (totalNewValue + this.sumOfCorrectedValue);
                            CorrectedValueTextBox.Text = this.sumOfCorrectedValue.ToString();

                            Decimal totalValueChange;
                            totalValueChange = (this.sumOfCorrectedValue - this.sumOfOrigValue);
                            this.TotalValueChangeTextBox.Text = totalValueChange.ToString();
                        }
                    }

                    if (this.ParcelDetailsGridView.OriginalRowCount == 0)
                    {
                        this.SubHeaderLabel.Text = string.Empty;
                        this.SubHeader1Label.Text = string.Empty;
                        this.EnableFormControls(false);
                        ////  this.RowCountLabel.Text = " of" + " " + ParcelDetailsGridView.OriginalRowCount.ToString();
                        this.ParcelsIncludedTextBox.Text = "0";
                        if (ParcelsIncludedTextBox.Text == "0")
                        {
                            this.CorrectionCodeComboBox.SelectedIndex = -1;
                            this.OriginalValueTextBox.Text = string.Empty;
                            this.CorrectedValueTextBox.Text = string.Empty;
                            this.TotalValueChangeTextBox.Text = string.Empty;
                            this.RowCountLabel.Text = " of" + " " + "0";
                            this.CorrectionNoteTextBox.Text = string.Empty;
                        }

                        this.ClearParcelDetails();
                    }
                    else
                    {
                        this.CorrectionNoteTextBox.Text = string.Empty;
                        TerraScanCommon.SetDataGridViewPosition(ParcelDetailsGridView, 0);
                        this.CorrectionCodeComboBox.SelectedIndex = -1;
                        this.DisplayParcelHeaderDetails(0);
                        this.PreviewButton.Enabled = false;
                        this.ProcessButton.Enabled = false;
                        this.EditButton.Enabled = false;  
                        this.SetAttachmentText(this.additionalOperationCount);
                    }
                    //// code added by khaja to fix Bug#3929
                    this.ScrollBarVisibility();
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
        /// Handles the CellFormatting event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column
                if ((e.ColumnIndex == this.ParcelDetailsGridView.Columns[this.taxRollCorrectionDataSet.ListParcelDetailsTable.OrigValueColumn.ColumnName.ToString()].Index) || (e.ColumnIndex == this.ParcelDetailsGridView.Columns[this.taxRollCorrectionDataSet.ListParcelDetailsTable.NewValueColumn.ColumnName.ToString()].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && (!String.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[e.RowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.OrigValueColumn.ColumnName.ToString()].Value.ToString().Trim()) || !String.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[e.RowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.NewValueColumn.ColumnName.ToString()].Value.ToString().Trim())))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = "0";
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("$ #,##0");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                if (e.ColumnIndex == 0)
                {
                    DataGridViewCell cell = this.ParcelDetailsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].ReadOnly)
                    {
                       
                            cell.ToolTipText = "This action cannot be done while the 2551 Edit Statement form is open";
                       
                       

                    }
                    else
                    {
                        cell.ToolTipText = "";
                    }
                    
                }
                if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                {
                    if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[e.RowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName].Value.ToString()))
                    {
                        if (Convert.ToBoolean(this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value.ToString()))
                        {
                            this.ParcelDetailsGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else

                        {
                            this.ParcelDetailsGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                        }
                    }
                   
                }
                if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsEdit"].Value.ToString()) && this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsEdit"].Value.ToString().ToLower().Equals("true") )
                {
                    this.ParcelDetailsGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(162, 198, 113);
                }
                else
                {
                    //this.ParcelDetailsGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                    if (((e.RowIndex) % 2).Equals(0))
                    {
                        this.ParcelDetailsGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        this.ParcelDetailsGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(224,224,224);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

       /* /// <summary>
        /// Handles the MouseEnter event of the PrimarySitusLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrimarySitusLinkLabel_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.PrimarySitusLinkLabel.Text))
            {
                if (this.PrimarySitusLinkLabel.Text.Length > 68)
                {
                    this.ParcelsToolTip.SetToolTip(this.PrimarySitusLinkLabel, this.PrimarySitusLinkLabel.Text);
                }
                else
                {
                    this.ParcelsToolTip.RemoveAll();
                }
            }
        }*/

     /*   /// <summary>
        /// Handles the MouseEnter event of the PrimaryOwnerLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrimaryOwnerLinkLabel_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.PrimaryOwnerLinkLabel.Text))
            {
                if (this.PrimaryOwnerLinkLabel.Text.Length > 32)
                {
                    this.ParcelsToolTip.SetToolTip(this.PrimaryOwnerLinkLabel, this.PrimaryOwnerLinkLabel.Text);
                }
                else
                {
                    this.ParcelsToolTip.RemoveAll();
                }
            }
        }*/
/*
        /// <summary>
        /// Handles the MouseEnter event of the DORLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DORLinkLabel_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.DORLinkLabel.Text))
            {
                if (this.DORLinkLabel.Text.Length > 32)
                {
                    this.ParcelsToolTip.SetToolTip(this.DORLinkLabel, this.DORLinkLabel.Text);
                }
                else
                {
                    this.ParcelsToolTip.RemoveAll();
                }
            }
        }*/

     /*   /// <summary>
        /// Handles the MouseEnter event of the ID1LinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ID1LinkLabel_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.ID2LinkLabel.Text))
            {
                if (this.ID2LinkLabel.Text.Length > 29)
                {
                    this.ParcelsToolTip.SetToolTip(this.ID2LinkLabel, this.ID2LinkLabel.Text);
                }
                else
                {
                    this.ParcelsToolTip.RemoveAll();
                }
            }
        }*/

      /*  /// <summary>
        /// Handles the MouseEnter event of the ID2LinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ID2LinkLabel_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.ID2LinkLabel.Text))
            {
                if (this.ID2LinkLabel.Text.Length > 32)
                {
                    this.ParcelsToolTip.SetToolTip(this.ID2LinkLabel, this.ID2LinkLabel.Text);
                }
                else
                {
                    this.ParcelsToolTip.RemoveAll();
                }
            }
        }*/

        /// <summary>
        /// Handles the MouseEnter event of the ParcelNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNumberLinkLabel_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.ParcelNumberLinkLabel.Text))
            {
                if (this.ParcelNumberLinkLabel.Text.Length > 24)
                {
                    this.ParcelsToolTip.SetToolTip(this.ParcelNumberLinkLabel, this.ParcelNumberLinkLabel.Text);
                }
                else
                {
                    this.ParcelsToolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(0))
                {
                    int selectedRecCount = 0;
                    this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    int a = this.ParcelDetailsGridView.OriginalRowCount;

                    this.sumOfOrigValue = 0;
                    this.sumOfCorrectedValue = 0;
                    if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    {
                       // this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                        if (!string.IsNullOrEmpty(this.editStatementList) && !this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].ReadOnly)
                        {
                            DataRow[] undoRow3 = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit= True");
                            if (undoRow3.Length > 0)
                            {

                                if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    this.editStatement.Clear();
                                    this.editStatementList = string.Empty;
                                    for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                                    {
                                        if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                                        {
                                            this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                                        }
                                    }
                                }
                                else
                                {
                                   
                                    if (this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value.Equals(true))
                                    {
                                        this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value = false;
                                    }
                                    else
                                    {
                                        this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value = true;
                                    }
                                    this.ParcelDetailsGridView.Rows[e.RowIndex].Cells[1].Selected = true;
                                    this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Selected = true;
                                    return;
                                }


                            }
                        }
                        //if (this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value.Equals(false))
                        //{
                        //    //if (!string.IsNullOrEmpty(this.editStatementList))
                        //    //{
                        //    //    if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        //    //    {
                        //    //        this.editStatementList = string.Empty;
                        //    //        for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                        //    //        {
                        //    //            if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[j]["IsEdit"].Equals(true))
                        //    //            {
                        //    //                this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[j]["IsEdit"] = false;
                        //    //            }
                        //    //        }
                        //    //        this.EditButton.Enabled = false;
                        //    //    }
                        //    //    else
                        //    //    {
                        //    //        this.EditButton.Enabled = false;
                        //    //        return;
                        //    //    }

                        //    //}
                        //    this.EditButton.Enabled = false;
                        //}
                        //else
                        //{
                        //    if (this.ProcessButton.Enabled )
                        //    {
                        //        this.EditButton.Enabled = true;
                        //    }
                        //    else if (!this.ProcessButton.Enabled && !this.ClearButton.Enabled)
                        //    {
                        //        this.EditButton.Enabled = true;
                        //    }
                        //    else
                        //    {
                        //        this.EditButton.Enabled = false;
                        //    }
                        //}

                        ////  Decimal b = 0.0;
                        //this.EditButton.Enabled = false;
                        for (int i = 0; i < this.ParcelDetailsGridView.OriginalRowCount; i++)
                        {
                            if (Convert.ToBoolean(this.ParcelDetailsGridView.Rows[i].Cells["IsValid"].Value.ToString()))
                            {
                                selectedRecCount += 1;
                                this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;

                                if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[i].Cells["OrigValue"].Value.ToString()))
                                {
                                    Decimal origValue = Convert.ToDecimal(this.ParcelDetailsGridView.Rows[i].Cells["OrigValue"].Value.ToString());
                                    this.sumOfOrigValue = (origValue + this.sumOfOrigValue);
                                }

                                Decimal totalNewValue = Convert.ToDecimal(this.ParcelDetailsGridView.Rows[i].Cells["NewValue"].Value.ToString());
                                this.sumOfCorrectedValue = (totalNewValue + this.sumOfCorrectedValue);

                                this.CorrectedValueTextBox.Text = this.sumOfCorrectedValue.ToString();
                                this.OriginalValueTextBox.Text = this.sumOfOrigValue.ToString();

                                Decimal totalValueChange;
                                totalValueChange = (this.sumOfCorrectedValue - this.sumOfOrigValue);
                                this.TotalValueChangeTextBox.Text = totalValueChange.ToString();

                                ////  CorrectedValueTextBox.Text = this.b.ToString();
                            }
                            else
                            {
                                this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                            }
                        }
                    }

                    this.RowCountLabel.Text = " of" + " " + ParcelDetailsGridView.OriginalRowCount.ToString();
                    this.ParcelsIncludedTextBox.Text = selectedRecCount.ToString();
                    this.checkedCount = selectedRecCount;
                    this.countChecked = true;
                    //Code Removed for uncheck grid 
                    /*
                    this.ProcessButton.Enabled = false;
                    this.PreviewButton.Enabled = false; 
                    if (!this.SetPreviewButton.Enabled && this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value.Equals(true))
                    {
                        this.EditButton.Enabled = true;
                    }
                    else
                    {
                        this.EditButton.Enabled = false;
                    }*/
                    
                    if (ParcelsIncludedTextBox.Text == "0")
                    {
                        this.OriginalValueTextBox.Text = string.Empty;
                        this.CorrectedValueTextBox.Text = string.Empty;
                        this.TotalValueChangeTextBox.Text = string.Empty;
                        //this.RowCountLabel.Text = " of" + " " + "0";
                        this.CorrectionNoteTextBox.Text = string.Empty;

                    }
                    //Code Removed for uncheck grid  if all the parcel grid gets disabled
                    if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[e.RowIndex]["IsParcelSelected"].Equals(false))
                    {
                        this.EditButton.Enabled = false;
                        //DataRow[] undoRow3 = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsParcelSelected= True");
                        //if (undoRow3.Length==1)
                        //{
                        //    this.PreviewButton.Enabled = false;
                        //    this.ProcessButton.Enabled = false;  
                        //}


                    }
                    else
                    {
                        if (this.setpreviewflag)
                        {
                            this.EditButton.Enabled = true;
                        }
                        //this.PreviewButton.Enabled = true;
                        //this.ProcessButton.Enabled = true;  
                    }
                    //// Removed the code to fix the issue #3817(3)
                    /*if (this.setpreviewflag)
                    {
                        if (selectedRecCount > 0)
                        {
                            this.ProcessButton.Enabled = this.PermissionEdit;
                        }
                        else
                        {
                            this.ProcessButton.Enabled = false;
                        }                      
                    }*/

                }
                //else
                //{
                //    if (this.ParcelDetailsGridView.Rows[e.RowIndex].Cells["IsValid"].Value.Equals(false))
                //    {

                //        this.EditButton.Enabled = false;
                //    }
                //    else
                //    {
                //        if (this.ProcessButton.Enabled)
                //        {
                //            this.EditButton.Enabled = true;
                //        }
                //        else if (!this.ProcessButton.Enabled && !this.ClearButton.Enabled)
                //        {
                //            this.EditButton.Enabled = true;
                //        }
                //        else
                //        {
                //            this.EditButton.Enabled = false;
                //        }
                //    }
                //    ////   MessageBox.Show("hai");
                //}
                                   
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
        /// Handles the Click event of the AddScheduleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddScheduleButton_Click(object sender, EventArgs e)
        {
            try
            {
                ///used to clear edit Statement List
                if (!string.IsNullOrEmpty(this.editStatementList))
                {
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                    DataRow[] undoRow = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit=TRUE");
                    if (undoRow.Length > 0)
                    {
                        if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.editStatement.Clear();
                            this.editStatementList = string.Empty;
                            for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                            {
                                if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                                {
                                    this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                                }
                            }
                            this.setpreviewOperation();

                        }
                        else
                        {
                            this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                            return;
                        }

                    }
                }
                Form scheduleSelectionForm = new Form();
                object[] optionalParameter = new object[] { this.RollYearTextBox.Text.ToString().Trim() };
                scheduleSelectionForm = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1404, optionalParameter, this.form2550Control.WorkItem);
                if (scheduleSelectionForm != null)
                {
                    if (scheduleSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        this.scheduleIds = TerraScanCommon.GetValue(scheduleSelectionForm, "CommandResult");
                        DataSet currentscheduleDataTable = new DataSet();
                        currentscheduleDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(this.scheduleIds));

                        if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                        {
                            if (currentscheduleDataTable.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] listscheduleDataRow = currentscheduleDataTable.Tables[0].Select();

                                foreach (DataRow schedule in listscheduleDataRow)
                                {
                                    if (!string.IsNullOrEmpty(schedule.ItemArray[0].ToString()))
                                    {
                                        DataRow[] scheduleId = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("ParcelID=" + schedule.ItemArray[0].ToString() + " AND ParcelType = 'Schedule'");

                                        if (scheduleId.Length > 0)
                                        {
                                            if (MessageBox.Show(SharedFunctions.GetResourceString("ExistSchedule"), "Terrascan T2 - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                                //// this.PopulateParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                                this.AddScheduleDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                            }
                        }
                        else
                        {
                            this.AddScheduleDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                        }
                        
                        ////this.CurrentScheduleId = Convert.ToInt32(TerraScanCommon.GetValue(scheduleSelectionForm, "ScheduleId").ToString());
                        ////////Check Duplicate Record

                        ////DataView scheduleDataView = new DataView(this.taxRollCorrectionDataSet.ListParcelDetailsTable);
                        ////scheduleDataView.RowFilter = "ParcelID=" + this.CurrentScheduleId;
                        ////if (scheduleDataView.Count <= 0)
                        ////{
                        ////    if (this.CurrentScheduleId.HasValue && this.recordExist == false)
                        ////    {
                        ////        this.PopulateScheduleDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                        ////    }
                        ////    else
                        ////    {
                        ////        this.AddScheduleDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                        ////    }
                        ////}
                        ////else if (MessageBox.Show(SharedFunctions.GetResourceString("ExistSchedule"), "Terrascan - Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        ////{
                        ////    return;
                        ////}
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates the schedule details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void PopulateScheduleDetails(int currentRowIndex)
        {
            if (currentRowIndex >= 0)
            {
                /// this.taxRollCorrectionDataSet = this.form2550Control.WorkItem.F2550_ListParcelDetails(null , this.scheduleIds);
                this.taxRollCorrectionDataSet = this.form2550Control.WorkItem.F2550_ListParcelDetails(null , this.scheduleIds,null,null);
                if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count > 0)
                {
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                    if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    {
                        if (maxrowcount > 7)
                        {
                            this.ParcelsIncludedTextBox.Text = maxrowcount.ToString();
                        }

                        for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                        {
                            this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                            this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                            {
                                minrowcount = minrowcount + 1;
                            }
                        }
                    }

                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.EnableFormControls(true);
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    this.recordExist = true;
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                    this.SetText(this.additionalOperationCountEnt);
                    this.SetAttachmentText(this.additionalOperationCount);
                }
                else
                {
                    this.ClearParcelDetails();
                    this.EnableFormControls(false);
                }
            }
        }

        /// <summary>
        /// Adds the schedule details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void AddScheduleDetails(int currentRowIndex)
        {
            if (currentRowIndex >= 0)
            {
                F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable dt = new F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable();

                dt.Clear();
               /// dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(null, this.scheduleIds).ListParcelDetailsTable;
                /// used for state ID
                dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(null, this.scheduleIds,null,null).ListParcelDetailsTable;
                if (this.ParcelDetailsGridView.OriginalRowCount < this.ParcelDetailsGridView.NumRowsVisible)
                {
                    for (int i = this.ParcelDetailsGridView.RowCount; i >= this.ParcelDetailsGridView.OriginalRowCount + 1; i--)
                    {
                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.RemoveAt(i - 1);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    /////used to clear edit Statement List
                    //if (!string.IsNullOrEmpty(this.editStatementList))
                    //{
                    //    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();  
                    //    DataRow[] undoRow1 = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit=True");
                    //    if(undoRow1.Length >0) 
                    //     {
                    //            if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    //            {
                    //                this.editStatement.Clear();
                    //                this.editStatementList = string.Empty;
                    //                for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                    //                {
                    //                    if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                    //                    {
                    //                        this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                    //                return;
                    //            }

                    //        }
                        
                    //}
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.Merge(dt);
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                    
                    ////Coding added by biju on 2/11/2009 [for displaying #parcels textbox]
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "IsParcelSelected=1";
                    minrowcount = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.Count;
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "";

                    ////commented by biju on 2/11/2009 [for displaying #parcels textbox]
                    ////if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                    ////{
                    ////    if (maxrowcount > 7)
                    ////    {
                    ////        this.ParcelsIncludedTextBox.Text = maxrowcount.ToString();
                    ////    }

                    ////    for (int count = 0; count < this.ParcelDetailsGridView.OriginalRowCount; count++)
                    ////    {
                    ////        ////  this.ParcelDetailsGridView.Rows[count].Cells["IsValid"].Value = "True";
                    ////        this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    ////        if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentRowIndex].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                    ////        {
                    ////            minrowcount = minrowcount + 1;
                    ////        }
                    ////    }
                    ////}

                    //////this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    ////if (minrowcount != this.checkedCount && checkedCount > 0)
                    ////{
                    ////    checkedCount++;
                    ////    this.ParcelsIncludedTextBox.Text = checkedCount.ToString();
                    ////}
                    ////else
                    ////{
                    ////    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();
                    ////}

                    this.ProcessButton.Enabled = false;
                    this.PreviewButton.Enabled = false;  
                    this.EditButton.Enabled = false;  
                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();

                    this.RowCountLabel.Text = " of" + " " + maxrowcount.ToString();
                   //// this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.EnableFormControls(true);
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    ////Added by Biju on 28/Oct/09 to show the attachment for the first record
                    this.SetAttachmentText(this.additionalOperationCount);
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                }
                else
                {
                    this.ClearParcelDetails();
                    this.EnableFormControls(false);
                }
            }
        }



        private void AddCentralDetails(int currentRowIndex)
        {
            if (currentRowIndex >= 0)
            {
                F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable dt = new F2550TaxRollCorrectionData.ListParcelDetailsTableDataTable();

                dt.Clear();
                dt = this.form2550Control.WorkItem.F2550_ListParcelDetails(null, null, null,this.centralXmlIds).ListParcelDetailsTable;
                if (this.ParcelDetailsGridView.OriginalRowCount < this.ParcelDetailsGridView.NumRowsVisible)
                {
                    for (int i = this.ParcelDetailsGridView.RowCount; i >= this.ParcelDetailsGridView.OriginalRowCount + 1; i--)
                    {
                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.RemoveAt(i - 1);
                    }
                }

                if (dt.Rows.Count > 0)
                {                   
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.Merge(dt);
                    int maxrowcount = this.ParcelDetailsGridView.OriginalRowCount;
                    int minrowcount = 0;
                    this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;

                    ////Coding added by biju on 2/11/2009 [for displaying #parcels textbox]
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "IsParcelSelected=1";
                    minrowcount = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.Count;
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView.RowFilter = "";
                   
                    this.ProcessButton.Enabled = false;
                    this.PreviewButton.Enabled = false;
                    this.EditButton.Enabled = false;
                    this.ParcelsIncludedTextBox.Text = minrowcount.ToString();

                    this.RowCountLabel.Text = " of" + " " + maxrowcount.ToString();
                    //// this.RowCountLabel.Text = " of" + " " + minrowcount.ToString();
                    this.EnableFormControls(true);
                    this.DisplayParcelHeaderDetails(currentRowIndex);
                    ////Added by Biju on 28/Oct/09 to show the attachment for the first record
                    this.SetAttachmentText(this.additionalOperationCount);
                    this.ParcelDetailsGridView.CurrentCell = this.ParcelDetailsGridView[0, Convert.ToInt32(currentRowIndex)];
                    this.ScrollBarVisibility();
                }
                else
                {
                    this.ClearParcelDetails();
                    this.EnableFormControls(false);
                }
            }
        }
        /// <summary>
        /// Handles the CellContentDoubleClick event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(0))
                {
                    int selectedRecCount = 0;
                    this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    for (int i = 0; i < this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count; i++)
                    {
                        if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i]["IsParcelSelected"] != DBNull.Value)
                        {
                            if (Convert.ToBoolean(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i]["IsParcelSelected"]))
                            {
                                selectedRecCount += 1;
                                this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                            else
                            {
                                this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                            }
                        }
                    }

                    this.RowCountLabel.Text = " of" + " " + ParcelDetailsGridView.OriginalRowCount.ToString();
                    this.ParcelsIncludedTextBox.Text = selectedRecCount.ToString();

                    /*if (this.setpreviewflag)
                    {
                        if (selectedRecCount > 0)
                        {
                            this.ProcessButton.Enabled = this.PermissionEdit;
                            //this.setpreviewflag = false;
                        }
                        else
                        {
                            this.ProcessButton.Enabled = false;
                        }
                        this.setpreviewflag = false;
                    }*/
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
        /// Handles the Click event of the SetPreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SetPreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                {
                    if (string.IsNullOrEmpty(this.CorrectionCodeComboBox.Text))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("TaxRollRequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges(); 
                    if (!string.IsNullOrEmpty(this.editStatementList))
                    {   
                        DataRow[] undoRow3 = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit= True");
                        if(undoRow3.Length >0) 
                        {
                            
                                if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    this.editStatement.Clear();  
                                    this.editStatementList = string.Empty;
                                    for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount;j++)
                                    {
                                        if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                                        {
                                            this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                                        }
                                    }
                                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();   
                                }
                                else
                                {
                                    return; 
                                }

                            
                        }
                    }

                    int.TryParse(this.CorrectionCodeComboBox.SelectedValue.ToString(), out this.correctionCode);

                    ////Correction Temp Item XML
                    string correctionTempItems = string.Empty;
                    DataTable correctionTempItemstable = new DataTable("CorrectionTempItems");
                    if (correctionTempItemstable.Columns.Count == 0)
                    {
                        correctionTempItemstable.Columns.Add("CorrectionCode");
                        correctionTempItemstable.Columns.Add("OriginalValue");
                        correctionTempItemstable.Columns.Add("CorrectedValue");
                        correctionTempItemstable.Columns.Add("TotalValueChange");
                        correctionTempItemstable.Columns.Add("Parcels");
                        correctionTempItemstable.Columns.Add("CorrectionNote");
                    } 

                    DataRow correctionTempItemsdr;
                    correctionTempItemsdr = correctionTempItemstable.NewRow();
                    correctionTempItemsdr["CorrectionCode"] = this.CorrectionCodeComboBox.SelectedValue;
                    correctionTempItemsdr["OriginalValue"] = this.OriginalValueTextBox.Text;
                    correctionTempItemsdr["CorrectedValue"] = this.CorrectedValueTextBox.Text;
                    correctionTempItemsdr["TotalValueChange"] = this.TotalValueChangeTextBox.Text;
                    string parcels;
                    parcels = this.ParcelsIncludedTextBox.Text + this.RowCountLabel.Text;
                    correctionTempItemsdr["Parcels"] = parcels;
                    correctionTempItemsdr["CorrectionNote"] = this.CorrectionNoteTextBox.Text;
                    correctionTempItemstable.Rows.Add(correctionTempItemsdr);
                    correctionTempItems = TerraScanCommon.GetXmlString(correctionTempItemstable);

                    ////Correction Parcel Id XML
                    this.parcelIdXml = string.Empty;
                    bool duplicateFlag = false;
                    DataTable corrParcelIdtemptable = new DataTable("CorrParcelIDs");

                    foreach (DataColumn column in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Columns)
                    {
                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName)
                        {
                            corrParcelIdtemptable.Columns.Add(new DataColumn(column.ColumnName));
                        }

                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName)
                        {
                            corrParcelIdtemptable.Columns.Add(new DataColumn(column.ColumnName));
                        }

                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.RollYearColumn.ColumnName)
                        {
                            corrParcelIdtemptable.Columns.Add(new DataColumn(column.ColumnName));
                        }
                    }

                    int count = 0;
                    ////foreach (DataRow dr in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows)
                    for (int i = 0; i < this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count; i++)
                    {
                        count = count + 1;
                        DataRow tempRow = corrParcelIdtemptable.NewRow();
                        DataRow dr = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i];
                        if (dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName] != DBNull.Value && Convert.ToBoolean(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName]).Equals(true))
                        {
                            if (Convert.ToBoolean(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName]) && Convert.ToInt32(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsDuplicateColumn.ColumnName]).Equals(0))
                            {
                                foreach (DataColumn column in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Columns)
                                {
                                    if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName)
                                    {
                                        tempRow[column.ColumnName] = dr[column.ColumnName];
                                    }

                                    if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName)
                                    {
                                        tempRow[column.ColumnName] = dr[column.ColumnName];
                                    }

                                    if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.RollYearColumn.ColumnName)
                                    {
                                        tempRow[column.ColumnName] = dr[column.ColumnName];
                                    }
                                }

                                corrParcelIdtemptable.Rows.Add(tempRow);
                            }
                            else
                            {
                                duplicateFlag = true;
                                if (duplicateFlag)
                                {
                                    if (MessageBox.Show("There are duplicate Statements for some of the selected Parcels.\nDo you wish to continue with the valid Parcel records only?", "TerraScan – Duplicate Statements", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.RemoveAt(count - 1);
                                        count = count - 1;
                                        i = i - 1;
                                        this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                                        this.ParcelsIncludedTextBox.Text = Convert.ToString(Convert.ToInt32(this.ParcelsIncludedTextBox.Text) - 1);
                                        string tempCount = this.RowCountLabel.Text.Remove(0, 3);
                                        this.RowCountLabel.Text = " of" + " " + Convert.ToString(Convert.ToInt32(tempCount) - 1);
                                        if (checkedCount > 0)
                                        {
                                            checkedCount = checkedCount - 1;
                                        }

                                        if (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName].ToString()) && (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].ToString())) && (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName].ToString())) && (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.RollYearColumn.ColumnName].ToString())))
                                        {
                                            this.ClearParcelDetails();
                                            this.EnableFormControls(false);
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    int row = this.ParcelDetailsGridView.CurrentRowIndex;
                    this.DisplayParcelHeaderDetails(row);
                    if (corrParcelIdtemptable.Rows.Count > 0)
                    {
                        this.parcelIdXml = TerraScanCommon.GetXmlString(corrParcelIdtemptable);
                        int returnValue = this.form2550Control.WorkItem.F2550_SaveCorrectionParcelsTemp(0, correctionTempItems, this.parcelIdXml, this.parcelIdXml, TerraScanCommon.UserId);
                        if (returnValue > 0)
                        {
                            this.ProcessButton.Enabled = true;
                            this.PreviewButton.Enabled = true;  
                            this.setpreviewflag = true;
                            if (MessageBox.Show("Would you like to preview a report of the Correction(s) you are about to process?", "TerraScan T2 - Preview Correction", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ////FormInfo formInfo = TerraScanCommon.GetFormInfo(255001);
                                ////formInfo.optionalParameters = new object[] { TerraScanCommon.UserId };
                                ////this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                                Hashtable reportOptionalParameter = new Hashtable();
                                reportOptionalParameter.Add("UserID", TerraScanCommon.UserId);
                                TerraScanCommon.ShowReport(255001, Report.ReportType.Preview, reportOptionalParameter);
                            }

                            if (this.ParcelDetailsGridView.Rows.Count > 0 && this.ParcelDetailsGridView.CurrentRowIndex != null && this.ParcelDetailsGridView.Rows[this.ParcelDetailsGridView.CurrentRowIndex].Cells["IsValid"].Value.Equals(false))
                            {
                                this.EditButton.Enabled = false;
                            }
                            else
                            {
                                this.EditButton.Enabled = true;
                            }
                        }
                         
                    }
                    else

                    {
                        MessageBox.Show("Please select any parcel/schedule", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
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
        /// Handles the CellEndEdit event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRecCount = 0;
            this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            for (int i = 0; i < this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count; i++)
            {
                if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i]["IsParcelSelected"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i]["IsParcelSelected"]))
                    {
                        selectedRecCount += 1;
                        this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedRecCount = 0;
            this.ParcelDetailsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            for (int i = 0; i < this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count; i++)
            {
                if (this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i]["IsParcelSelected"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i]["IsParcelSelected"]))
                    {
                        selectedRecCount += 1;
                        this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.ParcelDetailsGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the CellClick event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex.Equals(0))
                {

                }
                //////Commented by Biju on 28/Oct/09 to avoid multiple DB calls
                ////this.DisplayParcelHeaderDetails(e.RowIndex);
                ////this.SetAttachmentText(this.additionalOperationCount);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ParcelDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ParcelDetailsGridView_Leave(object sender, EventArgs e)
        {
        }

        private void CorrectionCodeComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.editStatementList))
            {
                this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                DataRow[] undoRow3 = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit= True");
                if (undoRow3.Length > 0)
                {

                    if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.editStatement.Clear();
                        this.editStatementList = string.Empty;
                        for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                        {
                            if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                            {
                                this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                            }
                        }
                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                        if (this.CorrectionCodeComboBox.SelectedValue != null)
                        {
                            int.TryParse(this.CorrectionCodeComboBox.SelectedValue.ToString(), out this.correctionCode);
                        }
                        this.ProcessButton.Enabled = false;
                        this.PreviewButton.Enabled = false; 
                        this.EditButton.Enabled =false;
                    }
                    else
                    {
                        int currentRow = this.ParcelDetailsGridView.CurrentRowIndex;
                        if (this.ParcelDetailsGridView.Rows[currentRow].Cells["IsValid"].Value.Equals(false))
                        {
                            this.EditButton.Enabled = false;  
                        }
                        this.ProcessButton.Enabled = true;
                        this.PreviewButton.Enabled = true;  
                        
                        this.CorrectionCodeComboBox.SelectedValue = this.correctionCode;
                        return;
                    }


                }
            }
            else
            {
                this.ProcessButton.Enabled = false;
                this.PreviewButton.Enabled = false; 
                this.EditButton.Enabled = false;
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the CorrectionCodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CorrectionCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ProcessButton.Enabled = false;
            //this.EditButton.Enabled = false;
           
        }

        #endregion Events

        #region LoadWorkSpaces

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form2550Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form2550Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form2550Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form2550Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form2550Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form2550Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            ////set required variable - attachment and comment
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form2550Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form2550Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);
            ///used to Modified for the CO 
            this.formLabelInfo[0] = "Tax Roll Corrections"; ////SharedFunctions.GetResourceString("F1031FormHeader");
            this.formLabelInfo[1] = string.Empty;
            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form2550Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form2550Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form2550Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form2550Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form2550Control.WorkItem;
            this.footerSmartPart.FormId = "2550";
           // this.footerSmartPart.AuditLinkText = "tAA_Parcel [ParcelID] ";
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

        #endregion

        /// Modified for the CO: 8509
        /// <summary>
        /// Handles the Click event of the AddStateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddStateButton_Click(object sender, EventArgs e)
        {
            try
            {
                ///used to clear edit Statement List
                if (!string.IsNullOrEmpty(this.editStatementList))
                {
                    this.taxRollCorrectionDataSet.ListParcelDetailsTable.AcceptChanges();
                    DataRow[] undoRow = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("IsEdit=TRUE");
                    if (undoRow.Length > 0)
                    {
                        if (MessageBox.Show("This action will undo any Statement Edits that you have made. Are you sure you wish to continue?", "TerraScan – Undo All Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.editStatement.Clear();
                            this.editStatementList = string.Empty;
                            for (int j = 0; j < this.ParcelDetailsGridView.OriginalRowCount; j++)
                            {
                                if (this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value.Equals(true))
                                {
                                    this.ParcelDetailsGridView.Rows[j].Cells["IsEdit"].Value = false;
                                }
                            }

                            this.setpreviewOperation();
                        }
                        else
                        {
                            this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable.DefaultView;
                            return;
                        }

                    }
                }
                    if (!string.IsNullOrEmpty(this.AddStateButton.Text) && this.AddStateButton.Text.Equals("Add State(s)"))
                {
                    Form stateSelectionForm = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1405, null, this.form2550Control.WorkItem);

                    if (stateSelectionForm != null)
                    {
                        if (stateSelectionForm.ShowDialog() == DialogResult.OK)
                        {
                            this.stateIds = TerraScanCommon.GetValue(stateSelectionForm, "CommandResult");

                            ////Bining mulitple parcels
                            DataSet currentstateDataTable = new DataSet();
                            currentstateDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(this.stateIds));

                            if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                            {
                                if (currentstateDataTable.Tables[0].Rows.Count > 0)
                                {
                                    DataRow[] liststateDataRow = currentstateDataTable.Tables[0].Select();

                                    foreach (DataRow state in liststateDataRow)
                                    {
                                        if (!string.IsNullOrEmpty(state.ItemArray[0].ToString()))
                                        {
                                            DataRow[] stateId = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("ParcelID=" + state.ItemArray[0].ToString() + " AND ParcelType = 'State'");

                                            if (stateId.Length > 0)
                                            {
                                                if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistState"), "Terrascan T2- Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    //// this.PopulateParcelDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                                    this.AddStateDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                                }
                            }
                            else
                            {
                                this.AddStateDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                            }


                        }
                    }
                }
                else
                {
                    Form centralSelectionForm = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1406, null, this.form2550Control.WorkItem);

                    if (centralSelectionForm != null)
                    {
                        if (centralSelectionForm.ShowDialog() == DialogResult.OK)
                        {
                            // this.stateIds = TerraScanCommon.GetValue(centralSelectionForm, "CommandResult");


                            this.centralXmlIds = TerraScanCommon.GetValue(centralSelectionForm, "CommandResult");
                            if (!string.IsNullOrEmpty(this.centralXmlIds) && !this.centralXmlIds.Equals("<Root />"))
                            {
                                ////Bining mulitple parcels
                                DataSet currentstateDataTable = new DataSet();
                                currentstateDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(this.centralXmlIds));

                                if (this.ParcelDetailsGridView.OriginalRowCount > 0)
                                {
                                    if (currentstateDataTable.Tables[0].Rows.Count > 0)
                                    {
                                        DataRow[] liststateDataRow = currentstateDataTable.Tables[0].Select();

                                        foreach (DataRow state in liststateDataRow)
                                        {
                                            if (!string.IsNullOrEmpty(state.ItemArray[0].ToString()))
                                            {
                                                DataRow[] stateId = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Select("ParcelID=" + state.ItemArray[0].ToString() + " AND ParcelType = 'Central'");

                                                if (stateId.Length > 0)
                                                {
                                                    if (MessageBox.Show(SharedFunctions.GetResourceString("ExsistState"), "Terrascan T2- Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                    {
                                                        return;
                                                    }
                                                }
                                            }
                                        }

                                        this.AddCentralDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                                    }
                                }
                                else
                                {
                                    this.AddCentralDetails(this.ParcelDetailsGridView.CurrentCell.RowIndex);
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        private void MasterbuttonEnable(bool locks)
        {
            //this.ProcessButton.Enabled = locks;
            //if (!locks)
            //{
            //    this.ProcessButton.SetButtonType = TerraScanButton.ButtonType.None;
            //    this.buttonType = 5;
            //    this.ProcessButton.ForeColor = Color.FromArgb(155, 155, 155);   
            //    this.ProcessButton.Click += new EventHandler(ProcessButton_Click);
            //}
            //else
            //{
            //    this.ProcessButton.SetButtonType = TerraScanButton.ButtonType.CommandButton;
            //    this.buttonType = 0;
            //    this.ProcessButton.ForeColor = Color.FromArgb(255, 255, 255);
            //}
            this.ProcessButton.Enabled = locks;
            if (!this.ProcessButton.Enabled)
            {
                this.label3.Enabled = true;  
                this.ProcessPanel.Enabled = true;
                this.ProcessPanel.BringToFront();
            }
            else
            {
                this.label3.Enabled = false;
                this.ProcessPanel.Enabled = false;
                this.ProcessPanel.SendToBack();
            }

            this.AddParcelButton.Enabled = locks;
            if (!this.AddParcelButton.Enabled)
            {
                 
                this.AddParcelPanel.BringToFront();  
            }
            else
            {
                this.AddParcelPanel.SendToBack();  
            }
            this.AddScheduleButton.Enabled = locks;
            if (!this.AddScheduleButton.Enabled)
            {
                this.AddSchedulePanel.BringToFront();  
            }
            else
            {
                this.AddSchedulePanel.SendToBack();  
            }
            this.AddStateButton.Enabled = locks;
            if (!this.AddStateButton.Enabled)
            {
                this.AddStatePanel.BringToFront();  
            }
            else
            {
                this.AddStatePanel.SendToBack();  
            }
            this.SetPreviewButton.Enabled = locks;
            if (!this.SetPreviewButton.Enabled)
            {
                this.SetPreviewPanel.BringToFront();  
            }
            else
            {
                this.SetPreviewPanel.SendToBack();   
            }
            this.ClearButton.Enabled = locks;
            if (!this.ClearButton.Enabled)
            {
                this.label7.Enabled = true;
                this.ClearPanel.Enabled = true;  
                this.ClearPanel.BringToFront();  
            }
            else
            {
                this.label7.Enabled = false;
                this.ClearPanel.Enabled = false;
                this.ClearPanel.SendToBack(); 
            }
            this.PreviewButton.Enabled = locks;
            if (!this.PreviewButton.Enabled)
            {
                this.label9.Enabled = true;
                this.previewPanel.Enabled = true;
                this.previewPanel.BringToFront();  
            }
            else
            {
                this.label9.Enabled = false;
                this.previewPanel.Enabled = false;
                this.previewPanel.SendToBack();  
            }
        }
        private void EditButton_Click(object sender, EventArgs e)
        {
        
            int currentrow = this.ParcelDetailsGridView.CurrentRowIndex;
            if (this.ParcelDetailsGridView.Rows[currentrow].Cells["IsValid"].Value.Equals(true))
            {
                if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentrow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.OwnerIDColumn.ColumnName].Value.ToString()))
                {
                    this.ownerId = Convert.ToInt32(this.ParcelDetailsGridView.Rows[currentrow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.OwnerIDColumn.ColumnName].Value.ToString());
                }
                if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentrow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.TypeIDColumn.ColumnName].Value.ToString()))
                {
                    this.parcelTypeID = Convert.ToInt16(this.ParcelDetailsGridView.Rows[currentrow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.TypeIDColumn.ColumnName].Value.ToString());
                }
                if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentrow].Cells["Statement"].Value.ToString()))
                {
                    this.statementId = this.ParcelDetailsGridView.Rows[currentrow].Cells["Statement"].Value.ToString();
                }
                else
                {
                    this.statementId = string.Empty ; 
                }
                if (!string.IsNullOrEmpty(this.ParcelDetailsGridView.Rows[currentrow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName].Value.ToString()))
                {
                    this.parcelid = Convert.ToInt32(this.ParcelDetailsGridView.Rows[currentrow].Cells[this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName].Value.ToString());
                }

                // this.ownerId = Convert.ToInt32(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentrow][this.taxRollCorrectionDataSet.ListParcelDetailsTable.OwnerIDColumn.ColumnName].ToString());
                //this.parcelTypeID = Convert.ToInt16(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentrow][this.taxRollCorrectionDataSet.ListParcelDetailsTable.TypeIDColumn.ColumnName].ToString());
                //this.statementId = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentrow][this.taxRollCorrectionDataSet.ListParcelDetailsTable.StatementIDColumn.ColumnName].ToString();
                //this.parcelid = Convert.ToInt32(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[currentrow][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName].ToString()); 
                if (string.IsNullOrEmpty(this.statementId))
                {
                    //Form statementSelection = new Form();
                    object[] optionalParameter = new object[] { this.SubHeaderLabel.Text.Trim(), this.SubHeader1Label.Text.Trim(), this.parcelid, this.parcelTypeID, this.editStatementList};
                    Form statementSelection = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2552, optionalParameter, this.form2550Control.WorkItem);
                    if (statementSelection != null)
                    {
                        if (statementSelection.ShowDialog() == DialogResult.Yes)
                        {
                            string ownid = TerraScanCommon.GetValue(statementSelection, "OwnerIDs");
                            int.TryParse(ownid, out this.ownerId);
                            this.statementId = TerraScanCommon.GetValue(statementSelection, "Statement");
                            this.EditStatement();
                        }
                        else
                        {
                            if (this.AddParcelButton.Enabled && this.AddStateButton.Enabled)
                            {
                                this.MasterbuttonEnable(true);
                            }
                            else
                            {
                                this.MasterbuttonEnable(false);
                            }
                        }

                    }
                }
                else
                {
                    this.EditStatement();
                    //FormInfo formInfo = TerraScanCommon.GetFormInfo(2551);
                    //formInfo.optionalParameters = new object[] { this.statementId,this.parcelid,this.ownerId,this.parcelTypeID };
                    //object[] tempArray = new object[formInfo.optionalParameters.Length + 1];
                    //formInfo.optionalParameters.CopyTo(tempArray, 0);
                    //this.SendOptionalParameters(this, new DataEventArgs<object[]>(tempArray));
                    //this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                }
            }
             
        }
        private void setpreviewOperation()
        {
            int.TryParse(this.CorrectionCodeComboBox.SelectedValue.ToString(), out this.correctionCode);

                    ////Correction Temp Item XML
                    string correctionTempItems = string.Empty;
                    DataTable correctionTempItemstable = new DataTable("CorrectionTempItems");
                    if (correctionTempItemstable.Columns.Count == 0)
                    {
                        correctionTempItemstable.Columns.Add("CorrectionCode");
                        correctionTempItemstable.Columns.Add("OriginalValue");
                        correctionTempItemstable.Columns.Add("CorrectedValue");
                        correctionTempItemstable.Columns.Add("TotalValueChange");
                        correctionTempItemstable.Columns.Add("Parcels");
                        correctionTempItemstable.Columns.Add("CorrectionNote");
                    } 

                    DataRow correctionTempItemsdr;
                    correctionTempItemsdr = correctionTempItemstable.NewRow();
                    correctionTempItemsdr["CorrectionCode"] = this.CorrectionCodeComboBox.SelectedValue;
                    correctionTempItemsdr["OriginalValue"] = this.OriginalValueTextBox.Text;
                    correctionTempItemsdr["CorrectedValue"] = this.CorrectedValueTextBox.Text;
                    correctionTempItemsdr["TotalValueChange"] = this.TotalValueChangeTextBox.Text;
                    string parcels;
                    parcels = this.ParcelsIncludedTextBox.Text + this.RowCountLabel.Text;
                    correctionTempItemsdr["Parcels"] = parcels;
                    correctionTempItemsdr["CorrectionNote"] = this.CorrectionNoteTextBox.Text;
                    correctionTempItemstable.Rows.Add(correctionTempItemsdr);
                    correctionTempItems = TerraScanCommon.GetXmlString(correctionTempItemstable);

                    ////Correction Parcel Id XML
                    this.parcelIdXml = string.Empty;
                    bool duplicateFlag = false;
                    DataTable corrParcelIdtemptable = new DataTable("CorrParcelIDs");

                    foreach (DataColumn column in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Columns)
                    {
                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName)
                        {
                            corrParcelIdtemptable.Columns.Add(new DataColumn(column.ColumnName));
                        }

                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName)
                        {
                            corrParcelIdtemptable.Columns.Add(new DataColumn(column.ColumnName));
                        }

                        if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.RollYearColumn.ColumnName)
                        {
                            corrParcelIdtemptable.Columns.Add(new DataColumn(column.ColumnName));
                        }
                    }

                    int count = 0;
                    ////foreach (DataRow dr in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows)
                    for (int i = 0; i < this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.Count; i++)
                    {
                        count = count + 1;
                        DataRow tempRow = corrParcelIdtemptable.NewRow();
                        DataRow dr = this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[i];
                        if (dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName] != DBNull.Value && Convert.ToBoolean(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName]).Equals(true))
                        {
                            if (Convert.ToBoolean(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsParcelSelectedColumn.ColumnName]) && Convert.ToInt32(dr[this.taxRollCorrectionDataSet.ListParcelDetailsTable.IsDuplicateColumn.ColumnName]).Equals(0))
                            {
                                foreach (DataColumn column in this.taxRollCorrectionDataSet.ListParcelDetailsTable.Columns)
                                {
                                    if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName)
                                    {
                                        tempRow[column.ColumnName] = dr[column.ColumnName];
                                    }

                                    if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName)
                                    {
                                        tempRow[column.ColumnName] = dr[column.ColumnName];
                                    }

                                    if (column.ColumnName == this.taxRollCorrectionDataSet.ListParcelDetailsTable.RollYearColumn.ColumnName)
                                    {
                                        tempRow[column.ColumnName] = dr[column.ColumnName];
                                    }
                                }

                                corrParcelIdtemptable.Rows.Add(tempRow);
                            }
                            else
                            {
                                duplicateFlag = true;
                                if (duplicateFlag)
                                {
                                    if (MessageBox.Show("There are duplicate Statements for some of the selected Parcels.\nDo you wish to continue with the valid Parcel records only?", "TerraScan – Duplicate Statements", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows.RemoveAt(count - 1);
                                        count = count - 1;
                                        i = i - 1;
                                        this.ParcelDetailsGridView.DataSource = this.taxRollCorrectionDataSet.ListParcelDetailsTable;
                                        this.ParcelsIncludedTextBox.Text = Convert.ToString(Convert.ToInt32(this.ParcelsIncludedTextBox.Text) - 1);
                                        string tempCount = this.RowCountLabel.Text.Remove(0, 3);
                                        this.RowCountLabel.Text = " of" + " " + Convert.ToString(Convert.ToInt32(tempCount) - 1);
                                        if (checkedCount > 0)
                                        {
                                            checkedCount = checkedCount - 1;
                                        }

                                        if (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelIDColumn.ColumnName].ToString()) && (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelNumberColumn.ColumnName].ToString())) && (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.ParcelTypeColumn.ColumnName].ToString())) && (string.IsNullOrEmpty(this.taxRollCorrectionDataSet.ListParcelDetailsTable.Rows[0][this.taxRollCorrectionDataSet.ListParcelDetailsTable.RollYearColumn.ColumnName].ToString())))
                                        {
                                            this.ClearParcelDetails();
                                            this.EnableFormControls(false);
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    int row = this.ParcelDetailsGridView.CurrentRowIndex;
                    this.DisplayParcelHeaderDetails(row);
                    if (corrParcelIdtemptable.Rows.Count > 0)
                    {
                        this.parcelIdXml = TerraScanCommon.GetXmlString(corrParcelIdtemptable);
                        int returnValue = this.form2550Control.WorkItem.F2550_SaveCorrectionParcelsTemp(0, correctionTempItems, this.parcelIdXml, this.parcelIdXml, TerraScanCommon.UserId);
                        if (returnValue > 0)
                        {
                            this.ProcessButton.Enabled = true;
                            this.PreviewButton.Enabled = true;
                            this.setpreviewflag = true;
                            //if (MessageBox.Show("Would you like to preview a report of the Correction(s) you are about to process?", "TerraScan - Preview Correction", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //{
                            //    ////FormInfo formInfo = TerraScanCommon.GetFormInfo(255001);
                            //    ////formInfo.optionalParameters = new object[] { TerraScanCommon.UserId };
                            //    ////this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                            //    Hashtable reportOptionalParameter = new Hashtable();
                            //    reportOptionalParameter.Add("UserID", TerraScanCommon.UserId);
                            //    TerraScanCommon.ShowReport(255001, Report.ReportType.Preview, reportOptionalParameter);
                            //}

                            if (this.ParcelDetailsGridView.Rows.Count > 0 && this.ParcelDetailsGridView.CurrentRowIndex != null && this.ParcelDetailsGridView.Rows[this.ParcelDetailsGridView.CurrentRowIndex].Cells["IsValid"].Value.Equals(false))
                            {
                                this.EditButton.Enabled = false;
                            }
                            else
                            {
                                this.EditButton.Enabled = true;
                            }
                        }
                      }
                         
        }

        private void EditStatement()
        {
           
            this.MasterbuttonEnable(false);
           for(int i=0;i<this.ParcelDetailsGridView.OriginalRowCount;i++)  
           {
           this.ParcelDetailsGridView.Rows[i].Cells["IsValid"].ReadOnly  = true;      
           }
           this.CorrectionCodeComboBox.Enabled = false;  
            FormInfo formInfo = TerraScanCommon.GetFormInfo(2551);
                         
                formInfo.optionalParameters = new object[] { this.statementId, this.parcelid, this.ownerId, this.parcelTypeID, this.SubHeader1Label.Text.Trim(), this.SubHeaderLabel.Text.Replace("/", "").Trim(), this.ParentFormId };
                object[] tempArray = new object[formInfo.optionalParameters.Length];
                formInfo.optionalParameters.CopyTo(tempArray, 0);
                if (!this.isFormOpen)
                {
                    this.SendOptionalParameters(this, new DataEventArgs<object[]>(tempArray));
                }
                //object[] optionalParameter = new object[] { };
                //UserControl  EditSelection = this.form2550Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetSmartPart(2551, tempArray, this.form2550Control.WorkItem);
                //if (EditSelection != null)
                //{
                //    bool IsEdit;
                //   // bool.TryParse(TerraScanCommon.GetSmartPart(EditSelection, "ISEdit"), out IsEdit);
                //}
              this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
              this.isFormOpen = true; 
         

        }

        private void ProcessButton_MouseHover(object sender, EventArgs e)
        {
          //  Control control = GetChildAtPoint(e); 
            //if (!this.ProcessButton.Enabled)
            //{
                //if (this.PrimarySitusLinkLabel.Text.Length > 68)
                //{
           // this.ParcelsToolTip.SetToolTip(this.ProcessButton, "This process cannot be run while the 2551 Edit Statement form is open");
                //}
                //else
                //{
                //    this.ParcelsToolTip.RemoveAll();
                //}
            
        }

        private void ProcessButton_MouseEnter(object sender, EventArgs e)
        {

        }

        private void ProcessButton_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void F2550_MouseMove(object sender, MouseEventArgs e)
        {

            if (!this.ProcessButton.Enabled)
            {
                // Control ctrl = this.GetChildAtPoint(e.Location);


                //(X >= Command1.Left And X <= (Command1.Left + Command1.Width)) And (Y >= Command1.Height And Y <= (Command1.Top + Command1.Height)) 
                ////if ((e.X >= this.ProcessButton.Left && e.X <= (this.ProcessButton.Left + this.ProcessButton.Width)) && (e.Y >= this.ProcessButton.Height && e.Y <= (this.ProcessButton.Top + this.ProcessButton.Height)))
                ////{


                ////    this.ProcessButton.Tag = "This process cannot be run while the 2551 Edit Statement form is open";
                ////    string tipstring = this.ParcelsToolTip.GetToolTip(this.ProcessButton);
                ////    this.ParcelsToolTip.Show("This process cannot be run while the 2551 Edit Statement form is open", this.ProcessButton, this.ProcessButton.Width / 2, this.ProcessButton.Height / 2);
                ////    //for (int i = 0; i < 1; i++)
                ////    //{
                ////    // if(this.ParcelsToolTip. 
                ////    //this.ParcelsToolTip.SetToolTip(this, "This process cannot be run while the 2551 Edit Statement form is open");
                ////    //}
                ////}
                ////else
                ////{
                ////    this.ParcelsToolTip.RemoveAll();
                ////}
            }
        }

        private void ProcessPanel_MouseEnter(object sender, EventArgs e)
        {
            //SetStyle(ControlStyles.Opaque, true);
            //UpdateStyles();
            //this.ProcessPanel.BackColor = System.Drawing.Color.Transparent;
           // this.ProcessPanel.SendToBack();
            //this.ProcessButton.MouseHover +=new EventHandler(ProcessButton_MouseHover);
            this.ParcelsToolTip.SetToolTip(this.ProcessPanel, "This process cannot be run while the 2551 Edit Statement form is open");
        }

        private void ProcessPanel_MouseHover(object sender, EventArgs e)
        {
           // this.ParcelsToolTip.SetToolTip(this.ProcessPanel, "This process cannot be run while the 2551 Edit Statement form is open");
        }

        private void AddParcelPanel_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.AddParcelPanel, "This Add Parcel Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void AddSchedulePanel_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.AddSchedulePanel, "This Add Schedule Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void AddStatePanel_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.AddStatePanel, "This Add State Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void ClearPanel_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.ClearPanel, "This Clear Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void SetPreviewPanel_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.SetPreviewPanel, "This Prepare Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.label7, "This Clear Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.label8, "This Prepare Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.label4, "This Add Parcel Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.label5, "This Add Schedule Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.label6, "This Add State Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.label3, "This process cannot be run while the 2551 Edit Statement form is open");
        }

        private void ParcelDetailsGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        /// <summary>
        /// Used to Show the 255001 Tax Roll Correction Preview Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">UserID</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable reportOptionalParameter = new Hashtable();
                reportOptionalParameter.Add("UserID", TerraScanCommon.UserId);
                TerraScanCommon.ShowReport(255001, Report.ReportType.Preview, reportOptionalParameter);
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

        private void previewPanel_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.previewPanel, "This Preview Operation cannot be run while the 2551 Edit Statement form is open");
        }

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelsToolTip.SetToolTip(this.label9, "This Preview Operation cannot be run while the 2551 Edit Statement form is open");
        }


    }
}