//----------------------------------------------------------------------------------
// <copyright file="F25090.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Form 25090 Slice form. 
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		            Description
// ----------		---------		        ----------------------------------------
//  20120307          Manoj P                 Created new form Slice instead of F25099 WebSlice in Field Mode
//*********************************************************************************/

namespace D20000
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
    using TerraScan.FieldSummary; 
    using TerraScan.Utilities;
    using System.IO;

    [SmartPart]
    public partial class F25090 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Unique keyId from the Form Master - statement id
        /// </summary>
        private int keyId;


        /// <summary>
        /// flag for identifying wehter it is a slice form
        /// </summary>
        private bool flagSliceForm;
        
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F25090));

        /// <summary>
        ///  Form Master - Number
        /// </summary>
        private int form;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;


        /// <summary>
        /// Tax Roll Correction form2550Control Controller
        /// </summary>
        private F25090Controller form25090Control;

        /// <summary>
        /// rollYearDataset
        /// </summary>
        private F25090FieldSummaryData FieldData = new F25090FieldSummaryData();


        /// <summary>
        /// Created string for Save file path
        /// </summary>
        private string saveFilePath = string.Empty;

        /// <summary>
        /// Form Number of the masterForm 
        /// </summary>
        private int masterFormNo;

         private  DataTable AttachFileDataTable = new DataTable();
         private  DataTable FileDataTable = new DataTable();
         private   DataColumn FileColumn = new DataColumn();
         private   DataColumn EventDate = new DataColumn();
         private   DataColumn FunctionName = new DataColumn();
         private   DataColumn DescriptName = new DataColumn();
         private DataColumn Extension = new DataColumn();
         private DataColumn AttachFilePath = new DataColumn(); 
         private   DataColumn FileID = new DataColumn();
            
        #endregion Member Variables

         
        #region Constructor


        public F25090()
        {
            InitializeComponent();
            //this.F25090PictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.F25090Panel.Height-4 , this.F25090PictureBox.Width, "Field Summary", 28, 81, 128); 
        }

        public F25090(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.form = formNo; 
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.F25090PictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.F25090PictureBox.Height, this.F25090PictureBox.Width, "Quick Summary", 28, 81, 128); 
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1031 control.
        /// </summary>
        /// <value>The F1031 control.</value>
        [CreateNew]
        public F25090Controller Form25090Control
        {
            get { return this.form25090Control as F25090Controller; }
            set { this.form25090Control = value; }
        }

        #endregion properties

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void F25090_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1;
            this.FieldData = this.form25090Control.WorkItem.F25090_FieldSummary(this.keyId); 
            if(this.FieldData.QuickValueSummaryData.Rows.Count>0)
            {
                this.PopulateSummaryUserControl(); 
            }
        }

          #region Event Subscription
        /// <summary>
        /// OnD9030_F9030_LoadSliceDetails
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            // Checks the masterform no is same  
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.FlagSliceForm = true;
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.SummaryButton.Image = global::D20000.Properties.Resources.summer;
                this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
                this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
                this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
                this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;
                this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
                this.Historybutton.Image = global::D20000.Properties.Resources.history1;
                this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1;
                this.FieldData = this.form25090Control.WorkItem.F25090_FieldSummary(this.keyId);
                if (this.FieldData.QuickValueSummaryData.Rows.Count > 0)
                {
                    this.PopulateSummaryUserControl();
                }
            }
        }

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;
        #endregion

        #region Methods

        private void PopulateSummaryUserControl()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.QuickValueSummaryDataRow SummaryRow = (F25090FieldSummaryData.QuickValueSummaryDataRow)this.FieldData.QuickValueSummaryData.Rows[0];     
            F25090FieldSummaryData.QuickValueSummaryDataDataTable ds = new F25090FieldSummaryData.QuickValueSummaryDataDataTable();
            SummaryUserControl SummaryControl = new SummaryUserControl();
            SummaryControl.SummaryDataSet = this.FieldData.QuickValueSummaryData;
            this.UserControlPanel.Controls.Add(SummaryControl);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();  
            SummaryControl.Location = new System.Drawing.Point(0, 0);

        }

        private void PopulateSaleUserControl()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.ParcelSaleHistoryDataDataTable ds = new F25090FieldSummaryData.ParcelSaleHistoryDataDataTable(); 
            SalesUserControl saleControl = new SalesUserControl();
            saleControl.SaleDataSet = this.FieldData.ParcelSaleHistoryData;
            this.UserControlPanel.Controls.Add(saleControl);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();
            saleControl.Location = new System.Drawing.Point(0, 0);

        }

        private void populatePermitUserControl()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.BuildingPermitsDataDataTable ds = new F25090FieldSummaryData.BuildingPermitsDataDataTable(); 
            PermitUserControl permitControl = new PermitUserControl();
            permitControl.PermitDataSet = this.FieldData.BuildingPermitsData;
            this.UserControlPanel.Controls.Add(permitControl);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();
            permitControl.Location = new System.Drawing.Point(0, 0);

        }
        private void populateCorrectionUserControl()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.CorrectionDataDataTable ds = new F25090FieldSummaryData.CorrectionDataDataTable();
            Correction corection = new Correction();
            corection.CorrectionDataSet = this.FieldData.CorrectionData;
            this.UserControlPanel.Controls.Add(corection);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();
            corection.Location = new System.Drawing.Point(0, 0);
        }

        private void populateAncestryUserControl()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.ParcelAncestryDataDataTable ds = new F25090FieldSummaryData.ParcelAncestryDataDataTable();
            Ancestry ancestryUserControl = new Ancestry();
            ancestryUserControl.AncestryDataSet = this.FieldData.ParcelAncestryData;
            this.UserControlPanel.Controls.Add(ancestryUserControl);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();
            ancestryUserControl.Location = new System.Drawing.Point(0, 0);
        }

        private void PopulateParcelOwnerShipUserControl()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.ParcelOwnershipDataDataTable ds = new F25090FieldSummaryData.ParcelOwnershipDataDataTable();
            OwnerShipUserControl parcelOwnerShipControl = new OwnerShipUserControl();
            parcelOwnerShipControl.OwnerShipDataSet = this.FieldData.ParcelOwnershipData;
            this.UserControlPanel.Controls.Add(parcelOwnerShipControl);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();
            parcelOwnerShipControl.Location = new System.Drawing.Point(0, 0);
        }


        private void PopulateHistoryUserControl()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.HistoryDataDataTable ds = new F25090FieldSummaryData.HistoryDataDataTable();
            History HistoryUserControl = new History();
            HistoryUserControl.HistoryDataSet = this.FieldData.HistoryData;
            this.UserControlPanel.Controls.Add(HistoryUserControl);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();
            HistoryUserControl.Location = new System.Drawing.Point(0, 0);
        }

        private  void PopulatePhotosUserControll()
        {
            this.UserControlPanel.Controls.Clear();
            F25090FieldSummaryData.PhotosDataDataTable ds = new F25090FieldSummaryData.PhotosDataDataTable();
            Photos getPhotos = new Photos();
            getPhotos.PhotosDataSet = this.AttachFileDataTable;
            getPhotos.FilesDataSet =    this.FileDataTable;  
            this.UserControlPanel.Controls.Add(getPhotos);
            this.F25090Panel.Controls.Clear();
            this.F25090Panel.Controls.Add(this.UserControlPanel);
            this.F25090Panel.Controls.Add(this.BottomPanel);
            this.F25090PictureBox.SendToBack();
            getPhotos.Location = new System.Drawing.Point(0, 0);
        }

        #endregion Methods

        private void ParcelOwnershipbutton_Click(object sender, EventArgs e)
        {
             if(this.FieldData.ParcelOwnershipData !=null)
             {
                if(this.FieldData.ParcelOwnershipData.Rows.Count>0)
                {
                    this.FieldData.ParcelOwnershipData.Rows.Clear();   
                }
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer1;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.owneship;
            this.FieldData = this.form25090Control.WorkItem.F25090_GetParcelOwnerShip(this.keyId);
            if (this.FieldData != null)
            {
                this.PopulateParcelOwnerShipUserControl();
            }
        }

        private void Salesbutton_Click(object sender, EventArgs e)
        {
            if(this.FieldData.ParcelSaleHistoryData !=null)
            {
                if(this.FieldData.ParcelSaleHistoryData.Rows.Count>0)
                {
                    this.FieldData.ParcelSaleHistoryData.Rows.Clear();   
                }
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer1;
            this.Salesbutton.Image = global::D20000.Properties.Resources.Sales;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1;
            this.FieldData = this.form25090Control.WorkItem.F25090_ParcelSale(this.keyId);
            if (this.FieldData != null)
            {
                this.PopulateSaleUserControl();
            }
        }

        private void Permitsbutton_Click(object sender, EventArgs e)
        {
            if(this.FieldData.BuildingPermitsData !=null)
            {
                if(this.FieldData.BuildingPermitsData.Rows.Count>0)
                {
                    this.FieldData.BuildingPermitsData.Rows.Clear();   
                }
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer1;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1;
            this.FieldData = this.form25090Control.WorkItem.F25090_BuildingPermits(this.keyId);
            if (this.FieldData != null)
            {
                this.populatePermitUserControl(); 
            }
     
        }

        private void Photosbutton_Click(object sender, EventArgs e)
        {
            
            if(this.FieldData.PhotosData !=null)
            {
                if(this.FieldData.PhotosData.Rows.Count>0)
                {
                    this.FieldData.PhotosData.Rows.Clear();   
                }
            }
            if(AttachFileDataTable != null)
            {
                AttachFileDataTable.Rows.Clear();  
            }
            if(FileDataTable != null)
            {
                FileDataTable.Rows.Clear();  
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer1;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;  
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1;
            this.FieldData = this.form25090Control.WorkItem.F25090_GetPhotos(this.keyId,30000);
            if (this.FieldData != null)
            {
                if (FileDataTable.Columns.Count.Equals(0))
                {
                    FileDataTable.Columns.Add(AttachFilePath);
                }
                if (AttachFileDataTable.Columns.Count.Equals(0))
                {
                    AttachFileDataTable.Columns.Add(FileID);
                    AttachFileDataTable.Columns.Add(EventDate);
                    AttachFileDataTable.Columns.Add(FunctionName);
                    AttachFileDataTable.Columns.Add(DescriptName);
                    AttachFileDataTable.Columns.Add(Extension);
                }
                AttachFileDataTable = this.FieldData.PhotosData;  
                
                if (this.FieldData.PhotosData.Rows.Count > 0)
                {

                    if (AttachFileDataTable.Rows.Count > 0)
                    {
                        //Used for the Attachment Path Creation
                        for (int j = 0; j < AttachFileDataTable.Rows.Count; j++)
                        {
                            DataRow fileAttachRow = FileDataTable.NewRow();
                            int fileId;
                            int.TryParse(AttachFileDataTable.Rows[j][3].ToString(), out  fileId);
                            string FileAttachID = this.form25090Control.WorkItem.GetOriginalFilePath(fileId, TerraScanCommon.UserId);
                            string CentralApexPath = string.Empty;
                            string fieldLocalPath = string.Empty;

                            CommentsData serverFilePathData = this.form25090Control.WorkItem.GetudfConfigurationFile();
                            CentralApexPath = serverFilePathData.GetCommentsConfigDetails[0][0].ToString();
                            if (TerraScanCommon.IsFieldUser)
                            {
                                fieldLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TerraScan Attachment\";
                                FileAttachID = FileAttachID.Replace(CentralApexPath, fieldLocalPath);
                                // Path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                                if (String.IsNullOrEmpty(FileAttachID))
                                {
                                }
                                else
                                {
                                    FileInfo photoFile = new FileInfo(FileAttachID);
                                    {
                                        if (photoFile.Exists)
                                        {
                                        }
                                        else
                                        {
                                            FileInfo fieldUpdatedPAth = new FileInfo(FileAttachID.Replace(fieldLocalPath, fieldLocalPath + @"\IsFieldAdded\"));
                                            if (fieldUpdatedPAth.Exists)
                                            {
                                                FileAttachID = FileAttachID.Replace(fieldLocalPath, fieldLocalPath + @"\IsFieldAdded\");
                                            }

                                        }
                                    }
                                }
                            }
                            fileAttachRow[0] = FileAttachID;
                            FileDataTable.Rows.Add(fileAttachRow);
                            FileDataTable.AcceptChanges();
                        }
                    }
                }
                
               
                ///Used to populate Path
                this.PopulatePhotosUserControll();
               
               
            }
        }

        private void Correctionbutton_Click(object sender, EventArgs e)
        {
             if(this.FieldData.PhotosData !=null)
            {
                if(this.FieldData.CorrectionData.Rows.Count>0)
                {
                    this.FieldData.CorrectionData.Rows.Clear();   
                }
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer1;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections;
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1;
            this.FieldData = this.form25090Control.WorkItem.F25090_GetCorrection(this.keyId);
            if (this.FieldData != null)
            {
                this.populateCorrectionUserControl();
            }
        }

        private void Historybutton_Click(object sender, EventArgs e)
        {
            if(this.FieldData.HistoryData !=null)
            {
                if(this.FieldData.HistoryData.Rows.Count>0)
                {
                    this.FieldData.HistoryData.Rows.Clear();   
                }
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer1;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.History;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1; 
            this.FieldData = this.form25090Control.WorkItem.F25090_GetHistoryData(this.keyId);
            if (this.FieldData != null)
            {
                this.PopulateHistoryUserControl();
            }
        }

        private void Ancestrybutton_Click(object sender, EventArgs e)
        {
            if(this.FieldData.ParcelAncestryData !=null)
            {
                if(this.FieldData.ParcelAncestryData.Rows.Count>0)
                {
                    this.FieldData.ParcelAncestryData.Rows.Clear();   
                }
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer1;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1;
            this.Photosbutton.Image = global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.Ancestry;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1; 
            this.FieldData = this.form25090Control.WorkItem.F25090_GetAncestryData(this.keyId);
            if (this.FieldData != null)
            {
                this.populateAncestryUserControl();
            }
        }

        private void SummaryButton_Click(object sender, EventArgs e)
        {
            if(this.FieldData.QuickValueSummaryData !=null)
            {
                if(this.FieldData.QuickValueSummaryData.Rows.Count>0)
                {
                    this.FieldData.QuickValueSummaryData.Rows.Clear();   
                }
            }
            this.SummaryButton.Image = global::D20000.Properties.Resources.summer;
            this.Salesbutton.Image = global::D20000.Properties.Resources.sales1;
            this.Permitsbutton.Image = global::D20000.Properties.Resources.Permits1; 
            this.Photosbutton.Image =  global::D20000.Properties.Resources.Photos1;
            this.Correctionbutton.Image = global::D20000.Properties.Resources.Corrections1;   
            this.Ancestrybutton.Image = global::D20000.Properties.Resources.ancestry1;
            this.Historybutton.Image = global::D20000.Properties.Resources.history1;
            this.ParcelOwnershipbutton.Image = global::D20000.Properties.Resources.ownership1; 
  
            this.FieldData = this.form25090Control.WorkItem.F25090_FieldSummary(this.keyId);
            if (this.FieldData != null)
            {
                this.PopulateSummaryUserControl();
            }
        }

        private void Historybutton_MouseHover(object sender, EventArgs e)
        {
            this.Historybutton.BackColor = System.Drawing.Color.Transparent;
        }

        private void F25090PictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.F25090PanelToolTip.SetToolTip(this.F25090PictureBox, Utility.GetFormNameSpace(this.Name));
        }

        private void F25090PictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D20000.F25090"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }
    }
}

