//--------------------------------------------------------------------------------------------
// <copyright file="F3001.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22 Aug 07		karthikeyan V	   Created
// 29 Nov 07        D.LathaMaheswari   Change Request (Added 10 fields)
// 04 Feb 09        khaja              Bug#4252 fixed.
//*********************************************************************************/

namespace D3000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F3001
    /// </summary>
    public partial class F3001 : BasePage
    {
        #region Variable

        /// <summary>
        /// Created Instance for F3001Controller
        /// </summary>
        private F3001Controller form3001Control;

        /// <summary>
        /// objectId
        /// </summary>
        private int objectId;

        /// <summary>
        /// F3001ObjectManagementData
        /// </summary>
        private F3001ObjectManagementData objectManagementDataSet = new F3001ObjectManagementData();

        /// <summary>
        /// objectLinkId
        /// </summary>
        private int objectLinkId;

        /// <summary>
        /// rollYear
        /// </summary>
        private int rollYear;

        /// <summary>
        /// parcelId
        /// </summary>
        private int parcelId;

        /// <summary>
        /// appraisalParcelId
        /// </summary>
        private int appraisalParcelId;

        /// <summary>
        /// formNo
        /// </summary>
        private int currentFormId;

        /// <summary>
        /// formEditMode
        /// </summary>
        private bool formEditMode;

        /// <summary>
        /// cancelOperation
        /// </summary>
        private bool cancelOperation;

        /// <summary>
        /// listrecordlock
        /// </summary>
        private F2000ParcelStatusData listrecordlock = new F2000ParcelStatusData();

        /// <summary>
        /// lockStatus
        /// </summary>
        private string lockStatus;

        /// <summary>
        /// lockedBy
        /// </summary>
        private string lockedBy;

        /// <summary>
        /// lockBool
        /// </summary>
        private bool lockBool;

        /// <summary>
        /// lockedDate
        /// </summary>
        private string lockedDate;

        /// <summary>
        /// valueXml
        /// </summary>
        private string valueXml;

        /// <summary>
        /// componentAssumptionsDataSet
        /// </summary>
        private DataSet componentAssumptionsDataSet = new DataSet();

        /// <summary>
        /// Flag to set focus on IsAgriculture field on load
        /// </summary>
        private bool isAgland = false;

        private bool isMobileHome = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3001"/> class.
        /// </summary>
        /// <param name="objectIdParameter">ObjectID</param>
        /// <param name="parcelIdParameter">ParcelID</param>
        /// <param name="formNo">FormNo</param>
        public F3001(int objectIdParameter, int parcelIdParameter, int formNo, bool isAglandValue)
        {
            this.InitializeComponent();
            this.objectId = objectIdParameter;
            this.appraisalParcelId = parcelIdParameter;
            this.CancelButton = this.ObjectCloseButton;
            this.currentFormId = formNo;
            this.isAgland = isAglandValue;
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event D35000_F35000_ParcelChangedValue
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35000_ParcelChangedValue, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> D35000_F35000_ParcelChangedValue;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the F1401 controll.
        /// </summary>
        /// <value>The F1401 controll.</value>
        [CreateNew]
        public F3001Controller F3001Controll
        {
            get { return this.form3001Control as F3001Controller; }
            set { this.form3001Control = value; }
        }

        #endregion

        #region Event Scbscription

        #region HelpEngine

        /// <summary>
        /// Autoes the print on button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_HelpRealEstateButtonClick, Thread = ThreadOption.UserInterface)]
        public void HelpRealEstateButtonClick(object sender, DataEventArgs<int> e)
        {
            TerraScan.Common.HelpEngine.Show(ParentForm.Text, "3001");
        }

        #endregion HelpEngine

        #endregion

        #region Methods

        /// <summary>
        /// Buttons the status.
        /// </summary>
        /// <param name="editMode">EditMode</param>
        private void ButtonStatus(bool editMode)
        {
            if (editMode)
            {
                this.SaveButton.Enabled = true;
                this.SaveButton.ForeColor = Color.White;
                this.ObjectCancelButton.Enabled = true;
                this.DeleteButton.Enabled = false;
                this.CopyButton.Enabled = false;
                this.ObjectCloseButton.Enabled = false;
                ////this.ObjectLinkLabel.Enabled = false;
                this.CancelButton = this.ObjectCancelButton;
                this.formEditMode = true;
                if (this.lockBool == true)
                {
                    this.ObjectCancelButton.Enabled = false;
                    this.DeleteButton.Enabled = false;
                }
            }
            else
            {
                this.SaveButton.Enabled = false;
                this.SaveButton.ForeColor = Color.Gray;
                this.ObjectCancelButton.Enabled = false;
                if (!TerraScanCommon.IsFieldUser)
                {
                    this.DeleteButton.Enabled = true;
                }
                else
                {
                    this.DeleteButton.Enabled = false;
                }
                this.CopyButton.Enabled = true;
                this.ObjectCloseButton.Enabled = true;
                ////this.ObjectLinkLabel.Enabled = true;
                this.CancelButton = this.ObjectCloseButton;
                this.formEditMode = false;
                if (this.lockBool == true)
                {
                    this.ObjectCancelButton.Enabled = false;
                    this.DeleteButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
            //this.objectManagementDataSet.Clear();
            this.objectManagementDataSet = this.form3001Control.WorkItem.F3001_GetObjectDetail(this.objectId);

            if (this.objectManagementDataSet != null)
            {
                // 20110912 Latha - Combo binding added to implement CO #13385
                if (this.objectManagementDataSet.ClassDetail.Rows.Count > 0)
                {
                    this.ClassComboBox.DisplayMember = this.objectManagementDataSet.ClassDetail.ClassColumn.ColumnName;
                    this.ClassComboBox.ValueMember = this.objectManagementDataSet.ClassDetail.ClassIDColumn.ColumnName;
                    this.ClassComboBox.DataSource = this.objectManagementDataSet.ClassDetail.DefaultView;
                }

                if (this.objectManagementDataSet.ObjectDetailDataTable.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.IsMobileHomeColumn.ColumnName]))
                    {
                        this.isMobileHome = true;
                    }
                    if (this.isMobileHome)
                    {
                        this.CustomPanel.Visible = true;
                        this.CustomPanel.Enabled = true;
                    }
                    else
                    {
                        this.CustomPanel.Visible = false;
                        this.CustomPanel.Enabled = false;
                        this.Height = 325;
                        this.ObjectMainPanel.Height = 127;
                        this.CopyParcelPanel.Location = new System.Drawing.Point(this.CopyParcelPanel.Location.X, 84);
                        this.CopyPanel.Location = new System.Drawing.Point(this.CopyPanel.Location.X, 84);

                        this.SaveButton.Location = new System.Drawing.Point(this.SaveButton.Location.X, 231);
                        this.ObjectCloseButton.Location = new System.Drawing.Point(this.ObjectCloseButton.Location.X, 231);
                        this.ObjectCancelButton.Location = new System.Drawing.Point(this.ObjectCancelButton.Location.X, 231);
                        this.DeleteButton.Location = new System.Drawing.Point(this.DeleteButton.Location.X, 231);

                        this.FormLinePanel.Location = new System.Drawing.Point(this.FormLinePanel.Location.X, 270);
                        this.FormNoLabel.Location = new System.Drawing.Point(this.FormNoLabel.Location.X, 275 );
                        this.ObjectHelpSmartPart.Location = new System.Drawing.Point(this.ObjectHelpSmartPart.Location.X, 273);

                        
                    }
                    string[] parcelNo = null;
                    string objParcelNumber;
                    int.TryParse(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ParcelIDColumn.ColumnName].ToString(), out this.parcelId);
                    int.TryParse(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ObjectIDColumn.ColumnName].ToString(), out this.objectLinkId);
                  //  this.ObjectLinkLabel.Text = SharedFunctions.GetResourceString("F3001_ObjectLink") + this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ObjectIDColumn.ColumnName].ToString();
                    ////this.ParcelnumberLabel.Text
                    objParcelNumber = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ParcelNoColumn.ColumnName].ToString();
                    parcelNo = objParcelNumber.Split('/');
                    this.ParcelnumberLabel.Text = parcelNo[0];
                    int.TryParse(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.RollYearColumn.ColumnName].ToString(), out this.rollYear);
                    this.RollYearLabel.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.RollYearColumn.ColumnName].ToString();
                    this.DescriptionTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.DescriptionColumn.ColumnName].ToString();
                    this.CopyParcelTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.CopyToParcelColumn.ColumnName].ToString();
                    //this.DORCodelinkLabel.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.StateCodeColumn.ColumnName].ToString();

                    //if (string.IsNullOrEmpty(this.DORCodelinkLabel.Text.Trim()))
                    //{
                    //    this.DORCodelinkLabel.Text = "N/A";
                    //}

                    if (Convert.ToBoolean(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.IsValueColumn.ColumnName]))
                    {
                        this.ValuedComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ValuedComboBox.SelectedIndex = 1;
                    }

                    ////Modified to implement TFS#21690
                    //if (Convert.ToBoolean(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.IsSeniorObjectColumn.ColumnName]))
                    //{
                    //    this.SeniorObjectCheckBox.Checked = true;
                    //}
                    //else
                    //{
                    //    this.SeniorObjectCheckBox.Checked = false;
                    //}

                    if (Convert.ToBoolean(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.IsRollColumn.ColumnName]))
                    {
                        this.RolledComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.RolledComboBox.SelectedIndex = 1;
                    }

                    if (!string.IsNullOrEmpty(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.IsAgricultureColumn.ColumnName].ToString().Trim())
                        && Convert.ToBoolean(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.IsAgricultureColumn.ColumnName]))
                    {
                        this.AgricultureComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.AgricultureComboBox.SelectedIndex = 1;
                    }

                    if (!string.IsNullOrEmpty(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ClassIDColumn.ColumnName].ToString().Trim())
                        && !string.IsNullOrEmpty(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ClassIDColumn.ColumnName].ToString()))
                    {
                        this.ClassComboBox.SelectedValue = int.Parse(this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ClassIDColumn.ColumnName].ToString());
                    }
                    else
                    {
                        this.ClassComboBox.SelectedIndex = -1;
                    }
                    //Modifed to Load Mobile home fields based on returned variable
                    if (this.isMobileHome)
                    {
                        this.MobileHomeFileTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable._MobileHomeFile_Column.ColumnName].ToString();
                        this.ManufacturerTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable._Manufacturer_Make_Column.ColumnName].ToString();
                        this.ModelTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ModelColumn.ColumnName].ToString();
                        this.DecalTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable._Decal_TPO__Column.ColumnName].ToString();
                        this.SerialTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable._Serial_VIN__Column.ColumnName].ToString();
                        this.HugTagTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable._HUDTag_Column.ColumnName].ToString();
                        this.TitleTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable._Title_Column.ColumnName].ToString();
                        this.TitleEliminationTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable._TitleElimination_Column.ColumnName].ToString();
                        this.FillingDateTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.FilingDateColumn.ColumnName].ToString();
                        this.ParkNameTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.ParkNameColumn.ColumnName].ToString();
                        this.SiteNumberTextBox.Text = this.objectManagementDataSet.ObjectDetailDataTable.Rows[0][this.objectManagementDataSet.ObjectDetailDataTable.SiteNumberColumn.ColumnName].ToString();
                    }

                    this.SetAdditionalOperationCount(this.objectId);
                    this.ButtonStatus(false);
                }
            }
        }

        /// <summary>
        /// Saves the operation.
        /// </summary>
        private void SaveOperation()
        {            
                bool valueDetail = false;
                bool rollDetail = false;
                bool rentalDetail = false;
                bool isAgriculture = false;
                bool isSeniorObject = false;

                if (this.ValuedComboBox.SelectedIndex == 0)
                {
                    valueDetail = true;
                }

                if (this.RolledComboBox.SelectedIndex == 0)
                {
                    rollDetail = true;
                }

                if (this.AgricultureComboBox.SelectedIndex.Equals(0))
                {
                    isAgriculture = true;
                }
                ////Modified to implement TFS#21690
                //if (this.SeniorObjectCheckBox.Checked)
                //{
                //    isSeniorObject = true;
                //}
                //else
                //{
                //    isSeniorObject = false;
                //}

                DataTable objectXmlTable = new DataTable();
                DataColumn[] xmlColumn = new DataColumn[] {
                new DataColumn("Description"),
                new DataColumn("IsValue"),
                new DataColumn("IsRoll"),
                new DataColumn("StateCode"), 
                new DataColumn("IsAgriculture"),
                new DataColumn("ClassID"),
                ////Modified to implement TFS#21690
                //new DataColumn("IsSeniorObject")

             new DataColumn("ManufacturerMake") 
            , new DataColumn("Model")
            , new DataColumn("MobileHomeFileNumber")
            , new DataColumn("DecalTPONumber")
            , new DataColumn("SerialVinNumber")
            , new DataColumn("HudTagNumber")
            , new DataColumn("TitleNumber")
            , new DataColumn("TitleEliminationNumber")
            , new DataColumn("FilingDate")
            , new DataColumn("ParkName")
            , new DataColumn("SiteNumber")             
            };
                objectXmlTable.Columns.AddRange(xmlColumn);

                DataRow objectRow = objectXmlTable.NewRow();
                objectRow["Description"] = this.DescriptionTextBox.Text;
                objectRow["IsValue"] = valueDetail;
                objectRow["IsRoll"] = rollDetail;
                //  objectRow["StateCode"] = this.DORCodelinkLabel.Text;

                objectRow["IsAgriculture"] = isAgriculture;

               //Modified to implement TFS#21690
               // objectRow["IsSeniorObject"] = isSeniorObject;

                //Modifed to save Mobile home fields
                objectRow["ManufacturerMake"] = this.ManufacturerTextBox.Text.Trim();
                objectRow["Model"] = this.ModelTextBox.Text.Trim();
                objectRow["MobileHomeFileNumber"] = this.MobileHomeFileTextBox.Text.Trim();
                objectRow["DecalTPONumber"] = this.DecalTextBox.Text.Trim();
                objectRow["SerialVinNumber"] = this.SerialTextBox.Text.Trim();
                objectRow["HudTagNumber"] = this.HugTagTextBox.Text.Trim();
                objectRow["TitleNumber"] = this.TitleTextBox.Text.Trim();
                objectRow["TitleEliminationNumber"] = this.TitleEliminationTextBox.Text.Trim();
                objectRow["FilingDate"] = this.FillingDateTextBox.Text.Trim();
                objectRow["ParkName"] = this.ParkNameTextBox.Text.Trim();
                objectRow["SiteNumber"] = this.SiteNumberTextBox.Text.Trim();

                if (this.ClassComboBox.SelectedIndex >= 0)
                {
                    objectRow["ClassID"] = this.ClassComboBox.SelectedValue;
                }

                objectXmlTable.Rows.Add(objectRow);

                ////this.objectId = this.form3001Control.WorkItem.F3001_SaveObjectManagement(this.objectId, this.DescriptionTextBox.Text.Trim(), valueDetail, rollDetail,this.DORCodelinkLabel.Text.Trim(),TerraScan.Common.TerraScanCommon.UserId);
                this.objectId = this.form3001Control.WorkItem.F3001_SaveObjectManagement(this.objectId, TerraScanCommon.GetXmlString(objectXmlTable), TerraScanCommon.UserId);                       
        }

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        /// <param name="keyId">KeyID</param>
        private void SetAdditionalOperationCount(int keyId)
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

            ////check for valid registerid
            if (keyId > 0)
            {
                additionalOperationCountEntity.AttachmentCount = this.F3001Controll.WorkItem.GetAttachmentCount(this.currentFormId, keyId, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.F3001Controll.WorkItem.GetCommentsCount(this.currentFormId, keyId, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }
            else
            {
                additionalOperationCountEntity.AttachmentCount = 0;
                additionalOperationCountEntity.CommentCount = 0;
                additionalOperationCountEntity.HighPriority = false;
            }

            this.SetText(additionalOperationCountEntity);
        }

        /// <summary>
        /// Sets the attachment and comments count text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
            ////if not -999 reset text
            if (additionalOperationCountEntity.AttachmentCount != -999)
            {
                if (additionalOperationCountEntity.AttachmentCount <= 0)
                {
                    this.AttachmentButton.Text = SharedFunctions.GetResourceString("Attachment");
                }
                else
                {
                    this.AttachmentButton.Text = string.Concat(SharedFunctions.GetResourceString("Attachment"), "(", additionalOperationCountEntity.AttachmentCount, ")");
                }
            }

            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentButton.Text = SharedFunctions.GetResourceString("Comment");
                }
                else
                {
                    this.CommentButton.Text = this.CommentButton.Text = string.Concat(SharedFunctions.GetResourceString("Comment"), "(", additionalOperationCountEntity.CommentCount, ")");
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    ////red color for high priority 
                    this.CommentButton.BackColor = Color.FromArgb(255, 0, 0);
                    this.CommentButton.CommentPriority = true;
                }
                else
                {
                    ////default brown color
                    this.CommentButton.BackColor = Color.FromArgb(174, 150, 94);
                    this.CommentButton.CommentPriority = false;
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F3001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F3001_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormLoad();
                this.formEditMode = false;
                this.SaveMenu.Click += new EventHandler(this.SaveButton_Click);

                if (this.isAgland)
                {
                    this.ActiveControl = this.AgricultureComboBox;
                    this.AgricultureComboBox.Focus();
                }
                else
                {
                    this.DescriptionTextBox.Focus();
                }

                this.lockBool = false;

                this.valueXml = this.form3001Control.WorkItem.ListRecordLockStatus(Convert.ToInt32(this.Tag), this.parcelId).ToString();
                StringReader stringXmlReader = new StringReader(this.valueXml);
                System.Xml.XmlTextReader textReaderHouseType1 = new System.Xml.XmlTextReader(stringXmlReader);
                this.componentAssumptionsDataSet.ReadXml(textReaderHouseType1);
                if (this.componentAssumptionsDataSet.Tables[0].Rows.Count > 0)
                {
                    this.lockStatus = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockStatus"].ToString();
                    this.lockedBy = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedBy"].ToString();
                    this.lockedDate = this.componentAssumptionsDataSet.Tables[0].Rows[0]["LockedDate"].ToString();
                }

                //////this.listrecordlock = this.form3001Control.WorkItem.ListRecordLockStatus(Convert.ToInt32(this.Tag), this.parcelId);
                //////if (this.listrecordlock.ListRecordLockStatus.Rows.Count > 0)
                //////{
                //////    this.lockStatus = this.listrecordlock.ListRecordLockStatus.Rows[0][this.listrecordlock.ListRecordLockStatus.LockStatusColumn.ColumnName].ToString();
                //////    this.lockedBy = this.listrecordlock.ListRecordLockStatus.Rows[0][this.listrecordlock.ListRecordLockStatus.LockedByColumn.ColumnName].ToString();
                //////    this.lockedDate = this.listrecordlock.ListRecordLockStatus.Rows[0][this.listrecordlock.ListRecordLockStatus.LockedDateColumn.ColumnName].ToString();
                //////}
                if (this.lockStatus == "True")
                {
                    this.lockBool = true;
                }
                else
                {
                    this.lockBool = false;
                }

                if (this.lockBool == true)
                {
                    SaveButton.BackColor = Color.Red;
                    SaveButton.ForeColor = Color.White;

                    SaveButton.Text = "Locked";
                    this.ObjectCancelButton.Enabled = false;
                    this.DeleteButton.Enabled = false;
                    this.SaveButton.Enabled = true;
                    ////  this.SepticYearTextBox.ValidateType = Text;
                    //// this.CopyButton.Enabled = false;
                }
                else
                {
                    SaveButton.Enabled = false;
                    SaveButton.ForeColor = Color.Gray;
                    this.ObjectCancelButton.Enabled = false;
                }
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

        /// <summary>
        /// Handles the Click event of the ParcelPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.rollYear };
                Form parcelSelectionForm = this.form3001Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, optionalParameter, this.form3001Control.WorkItem);
                ////open form in view mode - possible to edit
                if (parcelSelectionForm != null)
                {
                    if (parcelSelectionForm.ShowDialog().Equals(DialogResult.OK))
                    {
                        int.TryParse(TerraScanCommon.GetValue(parcelSelectionForm, "ParcelID").ToString(), out this.parcelId);

                        if (this.parcelId > 0)
                        {
                            this.CopyParcelTextBox.Text = this.form3001Control.WorkItem.F3001_GetParcelDescription(this.parcelId);
                            this.CopyParcelTextBox.ForeColor = Color.Black;
                            ////this.CopyButton.Enabled = false;
                            ////this.ObjectCloseButton.Enabled = false;
                        }
                        else
                        {
                            this.CopyParcelTextBox.Text = "< Current Parcel >";
                            this.CopyParcelTextBox.ForeColor = Color.FromArgb(115, 115, 115);
                        }
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Edits the text.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditText(object sender, EventArgs e)
        {
            try
            {
                this.ButtonStatus(true);
                this.formEditMode = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ValuedComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValuedComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.ButtonStatus(true);
                this.formEditMode = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ObjectLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ObjectLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.objectLinkId > 0)
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.objectLinkId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    this.Close();
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

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ObjectCloseButton.Enabled)
                {
                    this.DialogResult = DialogResult.No;
                    this.cancelOperation = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ObjectCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ObjectCancelButton_Click(object sender, EventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    if (this.ObjectCancelButton.Enabled)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.FormLoad();
                        #region Issue Fix
                        /*To close the Form window after click the close button
                      Modified by Latha*/
                        //// this.cancelOperation = true;
                        this.cancelOperation = false;
                        #endregion Issue Fix
                    }

                    SaveButton.ForeColor = Color.Gray;
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
        }

        /// <summary>
        /// Handles the FormClosing event of the F3001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F3001_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    /*Code Modified by Latha*/
                    ////if (!this.cancelOperation)
                    ////{
                    if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                    {
                        if (e.CloseReason.Equals(CloseReason.UserClosing))
                        {
                            if (!this.cancelOperation)
                            {
                                if (this.formEditMode)
                                {
                                    switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                    {
                                        case DialogResult.Yes:
                                            {
                                                this.SaveOperation();
                                                this.DialogResult = DialogResult.No;
                                                e.Cancel = false;
                                                break;
                                            }

                                        case DialogResult.No:
                                            {
                                                this.DialogResult = DialogResult.No;
                                                e.Cancel = false;
                                                break;
                                            }

                                        case DialogResult.Cancel:
                                            {
                                                e.Cancel = true;
                                                this.DescriptionTextBox.Focus();
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    //// }
                    ////  else
                    ////  {
                    ////       e.Cancel = true;
                    ////   }
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CopyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lockBool == false)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable objectXmlTable = new DataTable();
                    DataColumn[] xmlColumn = new DataColumn[] { new DataColumn("ObjectID"), new DataColumn("ParcelID") };
                    objectXmlTable.Columns.AddRange(xmlColumn);

                    DataRow objectRow = objectXmlTable.NewRow();
                    objectRow[SharedFunctions.GetResourceString("ObjectIDColumnName")] = this.objectId.ToString();
                    objectRow[SharedFunctions.GetResourceString("ParcelIDColumnName")] = this.parcelId.ToString();
                    objectXmlTable.Rows.Add(objectRow);
                    this.parcelId = this.form3001Control.WorkItem.F3001_CopyObject(this.objectId, TerraScanCommon.GetXmlString(objectXmlTable), TerraScan.Common.TerraScanCommon.UserId);

                    if (MessageBox.Show(SharedFunctions.GetResourceString("F3001_ErrorText"), SharedFunctions.GetResourceString("F3001_ErrorHeaderText"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.No;
                        this.cancelOperation = false;
                        this.Close();
                        FormInfo sliceForm = new FormInfo();
                        sliceForm = TerraScanCommon.GetFormInfo(30000);
                        sliceForm.optionalParameters = new object[1];
                        sliceForm.optionalParameters[0] = this.parcelId;
                        this.ShowForm(this, new DataEventArgs<FormInfo>(sliceForm));
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.form3001Control.WorkItem.F3001_DeleteObjectManagement(this.objectId, TerraScan.Common.TerraScanCommon.UserId);
                        this.DialogResult = DialogResult.No;
                        this.cancelOperation = false;
                        this.Close();
                    }                    

                    if (this.appraisalParcelId != -1)
                    {
                        int[] tempArgs = new int[2];
                        tempArgs[0] = this.appraisalParcelId;
                        tempArgs[1] = 1;
                        this.D35000_F35000_ParcelChangedValue(this, new DataEventArgs<int[]>(tempArgs));
                    }
                }
                catch (SoapException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.lockBool == false)
            {
                try
                {
                    if (this.SaveButton.Enabled)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.DescriptionTextBox.Focus();
                        bool smallMoneyValidationFlag = true;
                       
                        this.SaveOperation();
                        this.ButtonStatus(false);
                        this.formEditMode = false;

                        if (this.appraisalParcelId != -1)
                        {
                            int[] tempArgs = new int[2];
                            tempArgs[0] = this.appraisalParcelId;
                            tempArgs[1] = 0;
                            this.D35000_F35000_ParcelChangedValue(this, new DataEventArgs<int[]>(tempArgs));
                        }
                       
                        SaveButton.ForeColor = Color.Gray;
                    }
                }
                catch (SoapException ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        }

        /// <summary>
        /// Handles the TextChanged event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ButtonStatus(true);
                this.formEditMode = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ParcelnumberLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelnumberLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ParcelNumberToolTip.SetToolTip(this.ParcelnumberLabel, this.ParcelnumberLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.currentFormId, this.objectId, this.currentFormId };

                Form attachmentForm = new Form();

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.currentFormId).openPermission))
                {
                    attachmentForm = this.form3001Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.form3001Control.WorkItem);
                    //// this.Tag = this.currentFormId;
                    attachmentForm.Tag = this.currentFormId;
                    if (attachmentForm != null)
                    {
                        attachmentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        AdditionalOperationCountEntity additionalOperationCountEnt;
                        additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        additionalOperationCountEnt.AttachmentCount = Convert.ToInt32(TerraScanCommon.GetValue(attachmentForm, "AttachmentCount"));
                        this.SetText(additionalOperationCountEnt);
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
        /// Handles the Click event of the CommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                object[] optionalParameter;

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.currentFormId).openPermission))
                {
                    optionalParameter = new object[] { this.currentFormId, this.objectId, 3000 };

                    Form commentForm = new Form();
                    commentForm = this.form3001Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.form3001Control.WorkItem);
                    commentForm.Tag = this.currentFormId;

                    if (commentForm != null)
                    {
                        commentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        AdditionalOperationCountEntity additionalOperationCountEnt;
                        additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        additionalOperationCountEnt.CommentCount = Convert.ToInt32(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                        additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                        this.SetText(additionalOperationCountEnt);
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
        /// Handles the LinkClicked event of the DORCodelinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void DORCodelinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //FormInfo formInfo;
                //formInfo = TerraScanCommon.GetFormInfo(2010);
                //formInfo.optionalParameters = new object[1];
                //formInfo.optionalParameters[0] = this.parcelId;
                //this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                Form statecode = new Form();
                object[] optionalParameters=null;
                //if (this.DORCodelinkLabel.Text.Contains("N/A"))
                //{
                //    string tempDOR = String.Empty;
                //    optionalParameters = new object[] { tempDOR };
                //}
                //else
                //{
                //    optionalParameters = new object[] { this.DORCodelinkLabel.Text };
                //}
                 statecode = TerraScanCommon.GetForm(2010, optionalParameters, this.form3001Control.WorkItem);
                 if (statecode != null)
                 {
                     if (statecode.ShowDialog() == DialogResult.OK)
                     {
                         string ScheduleDORfield = string.Empty;
                         ScheduleDORfield = TerraScanCommon.GetValue(statecode, "StateCodeNameReturnValue");
                         if (ScheduleDORfield.Contains("&"))
                         {
                             ScheduleDORfield = ScheduleDORfield.Replace("&", "&&");
                         }
                         if (!string.IsNullOrEmpty(ScheduleDORfield))
                         {
                         //    this.DORCodelinkLabel.Text = ScheduleDORfield;
                             this.ButtonStatus(true);
                             this.formEditMode = true;
                         }
                     }

                 }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// TinyInt Validation
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void TinyIntLeave(object sender, EventArgs e)
        {
            try
            {
                if (this.lockBool == false)
                {
                    TerraScanTextBox currentDecimalTextBox = (TerraScanTextBox)sender;
                    if (!string.IsNullOrEmpty(currentDecimalTextBox.ToString()))
                    {
                        if (((TerraScanTextBox)sender).Name.Equals("BathRoomTextBox"))
                        {
                            this.smallMoneyValidation(currentDecimalTextBox);
                        }
                        else if (((TerraScanTextBox)sender).Name.Equals("BedRoomTextBox"))
                        {
                            this.smallMoneyValidation(currentDecimalTextBox);
                        }

                        else if (((TerraScanTextBox)sender).Name.Equals("EffectiveAgeTextBox"))
                        {
                            string currentDecimalTextBox1 = currentDecimalTextBox.Text;
                            if (!string.IsNullOrEmpty(currentDecimalTextBox1))
                            {
                                int currentBox = Convert.ToInt32(currentDecimalTextBox1.ToString());
                                this.tinyIntValidation(currentDecimalTextBox);
                            }
                        }
                        else
                        {
                            string currentDecimalTextBox1 = currentDecimalTextBox.Text;
                            if (!string.IsNullOrEmpty(currentDecimalTextBox1))
                            {
                                int currentBox = Convert.ToInt32(currentDecimalTextBox1.ToString());
                                this.tinyIntValidation(currentDecimalTextBox);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
               
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Smalls the money validation.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <returns></returns>
        private bool smallMoneyValidation(TerraScanTextBox textBox)
        {
            decimal currentDecimalValue;
            if (decimal.TryParse(textBox.Text, out currentDecimalValue))
            {
                ////Check the tinyint value range
                if (currentDecimalValue < 0 || currentDecimalValue > 214748.3647M)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), SharedFunctions.GetResourceString("F3001_InvalidHeaderValue"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Focus();
                    //textBox.Text = "0";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Smalls the money validation.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <returns></returns>
        private bool tinyIntValidation(TerraScanTextBox textBox)
        {
            int currentDecimalValue;
            if (int.TryParse(textBox.Text, out currentDecimalValue))
            {
                ////Check the tinyint value range
                if (currentDecimalValue < 0 || currentDecimalValue > 255)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("TinyIntValue"), SharedFunctions.GetResourceString("F3001_InvalidHeaderValue"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Focus();
                    //textBox.Text = "0";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Handles the MouseEnter event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_MouseEnter(object sender, EventArgs e)
        {
            if (this.lockBool == true)
            {
                SaveButton.BackColor = Color.Red;
                StringBuilder ownerAddress = new StringBuilder();

                ownerAddress.Append("Locked By:" + " " + this.lockedBy);

                ownerAddress.Append(SharedFunctions.GetResourceString("NexLine"));

                ownerAddress.Append("Locked On:" + " " + this.lockedDate);

                this.LockedtoolTip.SetToolTip(this.SaveButton, ownerAddress.ToString());
            }
        }

        #endregion

        private void ClassComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.ClassComboBox.SelectedValue == null)
                {
                    //if (this.ClassComboBox.Items.Count >= 1)
                    //{
                    //    this.ClassComboBox.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    this.ClassComboBox.SelectedIndex = -1;
                    this.ClassComboBox.Text = string.Empty;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ClassComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!this.flagLoadOnProcess)
                //{
                this.ButtonStatus(true);
                this.formEditMode = true;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SeniorObjectCheckBox_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void SeniorObjectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ButtonStatus(true);
                this.formEditMode = true;               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

            

        

    }
}