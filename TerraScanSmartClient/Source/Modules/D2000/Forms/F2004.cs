//--------------------------------------------------------------------------------------------
// <copyright file="F2004.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved. 
// </copyright>
// <summary>
//	This file contains UI for F2004 Form  - ParcelCopy
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20/08/07         Ramya.D             Created
//*********************************************************************************/

namespace D2000
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.IO;
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
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F2004
    /// </summary>
    public partial class F2004 : Form
    {
        #region variable

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// controller F1503Controller
        /// </summary>
        private F2004Controller formF2004Control;

        /// <summary>
        /// Object for attachment typed dataset
        /// </summary>
        private AttachmentsData attachmentDataSet = new AttachmentsData();

        /// <summary>
        /// Store ParcelType combo box selected Index
        /// </summary>
        private int parcelTypeIndex;

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
        /// Store rollingObject combo box selected Index
        /// </summary>
        private int rollingObjectIndex;

        /// <summary>
        /// Store rollingValue combo box selected Index
        /// </summary>
        private int rollingValueIndex;

        /// <summary>
        /// Store returnValue 
        /// </summary>
        private int returnValue;

        /// <summary>
        /// Store attachments combo box selected Index
        /// </summary>
        private int attachmentsIndex;

        /// <summary>
        /// to check file Exist
        /// </summary>        
        private bool requiredField;

        /// <summary>
        /// copyFormLoad
        /// </summary>        
        private bool copyFormLoad;

        /// <summary>
        /// createButton
        /// </summary>        
        private bool createButton;

        /// <summary>
        /// Created string for filePath
        /// </summary>
        private string filePath = string.Empty;

        /// <summary>
        /// Created string for fileID
        /// </summary>
        private string fileID = string.Empty;

        /// <summary>
        /// Store comments combo box selected Index
        /// </summary>
        private int commentsIndex;

        /// <summary>
        /// keyDown
        /// </summary>
        private bool keyDown;

        /// <summary>
        /// parcelTypeDataset
        /// </summary>
        private F2004ParcelCopyData parcelTypeDataset = new F2004ParcelCopyData();

        /// <summary>
        /// parcelStatusData
        /// </summary>
        private F2000ParcelStatusData.ListParcelStatusDataTableDataTable parcelStatusData = new F2000ParcelStatusData.ListParcelStatusDataTableDataTable();

        /// <summary>
        /// editScheduledata
        /// </summary>
        private F2200EditScheduleData editScheduleassessmentdata = new F2200EditScheduleData();

        /// <summary>
        /// parcelNumber
        /// </summary>
        private string parcelNumber;

        /// <summary>
        /// parcelYear
        /// </summary>
        private string parcelYear;

        /// <summary>
        /// parcelType
        /// </summary>
        private int parcelType;

        #endregion variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2001"/> class.
        /// </summary>
        public F2004()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2001"/> class.
        /// </summary>
        /// <param name="parcelId">ParcelId</param>
        public F2004(int parcelId)
        {
            InitializeComponent();
            this.keyId = parcelId;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Properties
        /// <summary>
        /// For F2004Controller
        /// </summary>
        [CreateNew]
        public F2004Controller Form2004Control
        {
            get { return this.formF2004Control as F2004Controller; }
            set { this.formF2004Control = value; }
        }
        #endregion Properties

        #region Event

        /// <summary>
        /// F2004_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F2004_Load(object sender, EventArgs e)
        {
            try
            {
                this.copyFormLoad = true;
                this.FillParcelTypeCombo();
                this.FillComboBoxes();
                this.GetParcelNumber();
                this.CloseButton.Visible = true;
                this.CancelButton = this.CloseButton;
                this.copyFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelTypeComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            this.parcelType = Convert.ToInt32(ParcelTypeComboBox.SelectedValue);
        }

        /// <summary>
        /// NonRollingObjectComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void NonRollingObjectComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// NonRollingValueSlicesComboBox2_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void NonRollingValueSlicesComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AttachmentsComboBox3_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AttachmentsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CommentsComboBox4_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CommentsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CancelCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CancelButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CancelCommandButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ParcelTypeComboBox.SelectedValue = this.parcelTypeIndex;
                this.NonRollingObjectComboBox.SelectedIndex = this.rollingObjectIndex;
                this.NonRollingValueSlicesComboBox.SelectedIndex = this.rollingValueIndex;
                this.AttachmentsComboBox.SelectedIndex = this.attachmentsIndex;
                this.CommentsComboBox.SelectedIndex = this.commentsIndex;
                this.GetParcelNumber();
                this.CancelCommandButton.Enabled = false;
                this.ParcelNumberTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CreateButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.createButton = true;
                this.Createparcel();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To close the Form When pressing Esc Key
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// HelplinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(0);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ParcelnumberLabel1_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelnumberLabel1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (ParcelnumberLabel1.Text.Length > 16)
                {
                    this.ParcelCopyToolTip.SetToolTip(this.ParcelnumberLabel1, this.ParcelnumberLabel1.Text + " " + SharedFunctions.GetResourceString("Separator") + " " + this.ParcelnumberLabel2.Text);
                }
                else
                {
                    this.ParcelCopyToolTip.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Event

        #region Methods

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

        /// <summary>
        /// FillTypeCombo
        /// </summary>
        private void FillComboBoxes()
        {
            this.NonRollingObjectComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.NonRollingObjectComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.NonRollingObjectComboBox.SelectedIndex = 1;
            this.rollingObjectIndex = this.NonRollingObjectComboBox.SelectedIndex;
            this.NonRollingValueSlicesComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.NonRollingValueSlicesComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.NonRollingValueSlicesComboBox.SelectedIndex = 1;
            this.rollingValueIndex = this.NonRollingValueSlicesComboBox.SelectedIndex;
            this.AttachmentsComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.AttachmentsComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.AttachmentsComboBox.SelectedIndex = 1;
            this.attachmentsIndex = this.AttachmentsComboBox.SelectedIndex;
            this.CommentsComboBox.Items.Insert(0, SharedFunctions.GetResourceString("No"));
            this.CommentsComboBox.Items.Insert(1, SharedFunctions.GetResourceString("Yes"));
            this.CommentsComboBox.SelectedIndex = 1;
            this.commentsIndex = this.CommentsComboBox.SelectedIndex;
        }

        /// <summary>
        /// FillParcelTypeCombo
        /// </summary>
        private void FillParcelTypeCombo()
        {
            this.parcelTypeDataset = this.formF2004Control.WorkItem.GetParcelTypeDetails(-1);
            if (this.parcelTypeDataset.f2004ListParcelType.Rows.Count > 0)
            {
                this.ParcelTypeComboBox.DisplayMember = this.parcelTypeDataset.f2004ListParcelType.ParcelTypeColumn.ColumnName;
                this.ParcelTypeComboBox.ValueMember = this.parcelTypeDataset.f2004ListParcelType.ParcelTypeIDColumn.ColumnName;
                this.ParcelTypeComboBox.DataSource = this.parcelTypeDataset.f2004ListParcelType;
                this.parcelTypeDataset.f2004ListParcelType.DefaultView.Sort = SharedFunctions.GetResourceString("ParcelType");
                this.parcelTypeDataset = this.formF2004Control.WorkItem.GetParcelTypeDetails(this.keyId);
                if (this.parcelTypeDataset.f2004ListParcelType.Rows.Count > 0)
                {
                    this.parcelTypeIndex = this.ConvertStringToInteger(this.parcelTypeDataset.f2004ListParcelType.Rows[0][this.parcelTypeDataset.f2004ListParcelType.ParcelTypeIDColumn].ToString());
                    this.ParcelTypeComboBox.SelectedValue = this.parcelTypeIndex;
                }
            }


            this.editScheduleassessmentdata = this.formF2004Control.WorkItem.F2200_GetAssessmentTypeDetails("Parcel");
            if (this.editScheduleassessmentdata.List_AssessmentTypeDataTable.Rows.Count > 0)
            {
                this.AssessmentTypeComboBox.DataSource = this.editScheduleassessmentdata.List_AssessmentTypeDataTable;
                this.AssessmentTypeComboBox.DisplayMember = this.editScheduleassessmentdata.List_AssessmentTypeDataTable.AssessmentTypeColumn.ColumnName;
                this.AssessmentTypeComboBox.ValueMember = this.editScheduleassessmentdata.List_AssessmentTypeDataTable.AssessmentTypeIDColumn.ColumnName;
            }

        }

        /// <summary>
        /// GetParcelNumber
        /// </summary>
        private void GetParcelNumber()
        {
            this.parcelStatusData = this.formF2004Control.WorkItem.F2000_ListParcelStatus(this.keyId);
            if (this.parcelStatusData.Rows.Count > 0)
            {
                this.ParcelnumberLabel1.Text = this.parcelStatusData.Rows[0][this.parcelStatusData.ParcelNumberColumn].ToString();
                this.ParcelnumberLabel2.Text = this.parcelStatusData.Rows[0][this.parcelStatusData.RollYearColumn].ToString();
                this.ParcelNumberTextBox.Text = this.parcelStatusData.Rows[0][this.parcelStatusData.ParcelNumberColumn].ToString();
                this.parcelNumber = this.parcelStatusData.Rows[0][this.parcelStatusData.ParcelNumberColumn].ToString();
                this.RollYearTextBox.Text = this.parcelStatusData.Rows[0][this.parcelStatusData.RollYearColumn].ToString();
                this.parcelYear = this.parcelStatusData.Rows[0][this.parcelStatusData.RollYearColumn].ToString();
            }
        }

        /// <summary>
        /// Createparcel
        /// </summary>
        private void Createparcel()
        {
            if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()))
            {
                this.requiredField = true;
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.requiredField = false;
                this.createButton = false;
                this.ParcelNumberTextBox.Focus();
                return;
            }

            int rollYear = 0;
            int.TryParse(this.RollYearTextBox.Text.Trim(), out rollYear);
            if (rollYear != 0)
            {
                if (!this.createButton)
                {
                    if (rollYear <= 1899 || rollYear >= 2080)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.requiredField = false;
                        this.RollYearTextBox.Text = "0";
                        this.panel5.BackColor = Color.Yellow;
                        this.RollYearTextBox.BackColor = Color.Yellow;
                        this.RollYearTextBox.Focus();
                    }
                    else
                    {
                        this.requiredField = true;
                    }
                }

                else if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    this.requiredField = true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.requiredField = false;
                    this.createButton = false;
                    this.panel5.BackColor = Color.Yellow;
                    this.RollYearTextBox.BackColor = Color.Yellow;
                    this.RollYearTextBox.Focus();
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.requiredField = false;
                this.createButton = false;
                this.panel5.BackColor = Color.Yellow;
                this.RollYearTextBox.BackColor = Color.Yellow;
                this.RollYearTextBox.Focus();
            }

            if (this.requiredField)
            {
                this.CreateNewParcel();
              ////  this.CancelCommandButton.Enabled = false;
            }
        }

        /// <summary>
        ///// To create a parcel Copy
        /// </summary>
        private void CreateNewParcel()
        {
            ////if ((this.parcelNumber == this.ParcelNumberTextBox.Text.Trim()) && (this.parcelYear == this.RollYearTextBox.Text.Trim()) && (this.parcelTypeIndex == Convert.ToInt32(this.ParcelTypeComboBox.SelectedValue.ToString())))
            ////{
            //////// MessageBox.Show("sorry already created");
            ////    MessageBox.Show(SharedFunctions.GetResourceString("RecordStatus"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
            ////else
            ////{
            F2004ParcelCopyData createNewParcel = new F2004ParcelCopyData();
            F2004ParcelCopyData.createNewParcelCopyDataTableRow dr = createNewParcel.createNewParcelCopyDataTable.NewcreateNewParcelCopyDataTableRow();
            dr.ModuleID = SharedFunctions.GetResourceString("ParcelCopyNo");
            dr.InsertedBy = TerraScanCommon.UserId;
            dr.InsertedDate = DateTime.Now.ToString();
            dr.IsDeleted = SharedFunctions.GetResourceString("False");
            dr.ParcelNumber = this.ParcelNumberTextBox.Text.Trim();
            dr.RollYear = this.RollYearTextBox.Text.Trim();
            dr.AssessmentTypeID = Convert.ToInt16(this.AssessmentTypeComboBox.SelectedValue);  
            createNewParcel.createNewParcelCopyDataTable.Rows.Add(dr);
            createNewParcel.createNewParcelCopyDataTable.AcceptChanges();
            DataSet tempDataSet = new DataSet(SharedFunctions.GetResourceString("Root"));
            tempDataSet.Tables.Add(createNewParcel.createNewParcelCopyDataTable.Copy());
            tempDataSet.Tables[0].TableName = SharedFunctions.GetResourceString("SaleAdvisoryTable");
            this.returnValue = this.formF2004Control.WorkItem.CreateNewParcelCopy(this.keyId, this.ConvertStringToInteger(this.ParcelTypeComboBox.SelectedValue.ToString()), this.NonRollingObjectComboBox.SelectedIndex, this.NonRollingValueSlicesComboBox.SelectedIndex, this.AttachmentsComboBox.SelectedIndex, this.CommentsComboBox.SelectedIndex, tempDataSet.GetXml(), TerraScanCommon.UserId);

            ////Coding added for the issue 4496 by malliga on 12/11/2009
            if (this.returnValue.Equals(-1))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RecordStatus"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
               //// this.CancelCommandButton.Enabled = true;
                this.createButton = false;
                this.DialogResult = DialogResult.None;
                return;
            }

            if (this.AttachmentsComboBox.SelectedIndex == 1)
            {
                if (this.returnValue > 0)
                {
                    this.parcelTypeDataset = this.formF2004Control.WorkItem.GetParcelAttachmentDetails(this.keyId, this.returnValue, TerraScanCommon.UserId, Convert.ToInt32(SharedFunctions.GetResourceString("ParcelCopyNo")));
                    if (this.parcelTypeDataset.getParcelAttachmentTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.parcelTypeDataset.getParcelAttachmentTable.Rows.Count; i++)
                        {
                            string fileTypeId = string.Empty;
                            int primaryValue;
                            int publicValue;
                            int rollValue;
                            string description = string.Empty;
                            string eventDate = string.Empty;
                            //string filetype = string.Empty;
                            //string aurl = string.Empty;
                            //string pfileid = string.Empty;

                            this.attachmentKeyID = Convert.ToInt32(this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.Column1Column].ToString());
                            this.attachmentFormID = Convert.ToInt32(this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.FormColumn].ToString());
                            this.browsePathExt = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.ExtensionColumn].ToString();

                            eventDate = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.EventDateColumn].ToString();
                            description = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.DescriptionColumn].ToString();
                            fileTypeId = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.FileTypeIDColumn].ToString();

                           // filetype = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.LinkTypeColumn].ToString();
                         
                           //aurl = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.AURLColumn].ToString();
                           
                           // pfileid = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.PFileIDColumn].ToString();
                           
                            if (this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.IsPrimaryColumn].ToString().Equals(SharedFunctions.GetResourceString("True")))
                            {
                                primaryValue = 1;
                            }
                            else
                            {
                                primaryValue = 0;
                            }

                            if (this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.IsPublicColumn].ToString().Equals("True"))
                            {
                                publicValue = 1;
                            }
                            else
                            {
                                publicValue = 0;
                            }

                            if (this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.IsRollColumn].ToString().Equals("True"))
                            {
                                rollValue = 1;
                            }
                            else
                            {
                                rollValue = 0;
                            }

                            this.attachmentDataSet.GetFilePath.Clear();
                            this.attachmentDataSet.GetFilePath.Merge(this.formF2004Control.WorkItem.GetFilePath(SharedFunctions.GetResourceString("TSFile"), this.attachmentFormID, this.attachmentKeyID, this.browsePathExt, TerraScanCommon.UserId));
                            this.filePath = this.attachmentDataSet.GetFilePath.Rows[0][SharedFunctions.GetResourceString("ParcelCopyFilePath")].ToString();
                            this.fileID = this.attachmentDataSet.GetFilePath.Rows[0][SharedFunctions.GetResourceString("FileID")].ToString();

                            string tempFilePath = string.Empty;
                            tempFilePath = this.parcelTypeDataset.getParcelAttachmentTable.Rows[i][this.parcelTypeDataset.getParcelAttachmentTable.SourceColumn].ToString();
                            try
                            {
                                if (System.IO.File.Exists(tempFilePath))
                                {
                                    FileStream fs = new FileStream(tempFilePath, FileMode.Open);
                                    BinaryReader bR = new BinaryReader(fs);

                                    //// Upload the Image to the Central Location.
                                    UpLoadImage(bR.ReadBytes((int)fs.Length), this.filePath);
                                    ////this.fileExist = true;
                                    bR.Close();
                                    fs.Close();
                                }
                            }
                            catch (UnauthorizedAccessException)
                            {
                                MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), SharedFunctions.GetResourceString("NexLine"), tempFilePath, SharedFunctions.GetResourceString("NexLine"), SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            this.formF2004Control.WorkItem.SaveAttachments(Convert.ToInt32(this.fileID), this.browsePathExt, this.attachmentFormID, this.attachmentKeyID, Convert.ToInt32(fileTypeId), tempFilePath, primaryValue, description, eventDate, TerraScan.Common.TerraScanCommon.UserId, publicValue, rollValue,0,"",0, "TSFile");
                        }
                    }
                }
            }
            else
            {
            }

            this.DialogResult = DialogResult.Cancel;
           //// this.CancelCommandButton.Enabled = false;
            if (MessageBox.Show(SharedFunctions.GetResourceString("ParcelCopy"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (this.createButton)
                {
                    this.Close();
                }
            }
       //// }
        }
        /// <summary>
        /// SetEditRecord
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetEditRecord(object sender, EventArgs e)
        {
            if (!this.copyFormLoad)
            {
                this.CancelCommandButton.Enabled = true;
              }
            else
            {
                this.CancelCommandButton.Enabled = false;
            }
        }

        /// <summary>
        /// SetKeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SetKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.keyDown == true)
                {
                    if ((e.KeyChar == 'v') || (e.KeyChar == 24) || (e.KeyChar == 26))
                    {
                        ////this.copyFormLoad = false;
                        this.CancelCommandButton.Enabled = true;
                    }
                    else if ((e.KeyChar == 3) && !this.CancelCommandButton.Enabled)
                    {
                        this.CancelCommandButton.Enabled = false;
                    }
                }
                else
                {
                    this.CancelCommandButton.Enabled = true;
                }

                ////if (!this.copyFormLoad)
                ////{
                ////    this.CancelCommandButton.Enabled = true;
                ////}
                ////else
                ////{
                ////    this.CancelCommandButton.Enabled = false;
                ////}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To convert string to integer
        /// </summary>
        /// <param name="selectedValue">stringvalue</param>
        /// <returns>integervalue</returns>
        private int ConvertStringToInteger(string selectedValue)
        {
            int outputValue;
            int.TryParse(selectedValue, out outputValue);
            return outputValue;
        }

        #endregion Methods

        /// <summary>
        /// ParcelNumberTextBox_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 46 || e.KeyValue == 86)
                {
                    //this.copyFormLoad = false;
                    //this.keyDown = e.Control;
                    this.CancelCommandButton.Enabled = true;
                }
                this.keyDown = e.Control;
            }
            catch (SoapException soapException)
            {

                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);

            }
        }

        /// <summary>
        /// F2004_FormClosing
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F2004_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.createButton)
                {
                    if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                    {
                        if (e.CloseReason.Equals(CloseReason.UserClosing))
                        {
                            if (this.CancelCommandButton.Enabled)
                            {
                                switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", "parcel copy", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    case DialogResult.Yes:
                                        {
                                            this.Createparcel();
                                            if (this.requiredField)
                                            {
                                               ///// this.DialogResult = DialogResult.Cancel;
                                                e.Cancel = false;
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }

                                            break;
                                        }

                                    case DialogResult.No:
                                        {
                                            this.DialogResult = DialogResult.Cancel;
                                            e.Cancel = false;
                                            break;
                                        }

                                    case DialogResult.Cancel:
                                        {
                                            this.DialogResult = DialogResult.None;
                                            e.Cancel = true;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                e.Cancel = false;
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        ////this.formClosing = true;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void ParcelNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.copyFormLoad)
            {
                this.CancelCommandButton.Enabled = true;
            }
            else
            {
                this.CancelCommandButton.Enabled = false;
            }
            ////this.parcelNumber = ParcelNumberTextBox.Text.Trim();

            
        }

        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.copyFormLoad)
            {
                this.CancelCommandButton.Enabled = true;
            }
            else
            {
                this.CancelCommandButton.Enabled = false;
            }
          ////  this.parcelYear = RollYearTextBox.Text.Trim();
        }

    }
}