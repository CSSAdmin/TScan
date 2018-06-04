//--------------------------------------------------------------------------------------------
// <copyright file="F9075.cs" company="Congruent">
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
// 20120906             Manoj P         Change the Comment Button Color based on SP call.
// 20130405             Purushotham.A   TO Enable scroll bar for an disabled Comment field  
//*********************************************************************************/
[assembly: System.CLSCompliant(false)]
namespace D9075
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
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
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using Infrastructure.Interface;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Helper;
    using TerraScan.Infrastructure.Interface.Constants;
   
    /// <summary>
    /// Comment form class
    /// </summary>
    public partial class F9075 : BasePage
    {
        #region  Variable Decl

        #region constantFields

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "MM/dd/yyyy";

        /// <summary>
        /// used for LinkLableValue
        /// </summary>
        private readonly string LinkLableValueText = "tTS_Comment[CommentID]";

        private int commentId;

        #endregion

        /// <summary>
        /// Local variable for commentSelectedRow
        /// </summary>
        private int commentSelectedRow;

        /// <summary>
        /// Local variable for commenGridRow
        /// </summary>
        ////private int commenGridRow;

        /// <summary>
        /// controller F9075
        /// </summary>
        private F9075Controller form9075Control;

        /// <summary>
        /// commentsDataDataSet variable is used to get the Comments of Comments Data.
        /// </summary>
        private CommentsData commentDataSet = new CommentsData();

        /// <summary>
        /// set empty string to buttonOPeration
        /// </summary>
        private int buttonOperation;
        public bool fromContent=false;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;
        # region Added for TSCO - D9075.F9075 Comments Form - New "Remove' button
        /// <summary>
        /// variable holds the selectedCommentIds.
        /// </summary>
        private List<int> selectedCommentIds = new List<int>();

      

        /// <summary>
        /// variable holds the selected Comments ids xml string.
        /// </summary>
        private string selectedCommentsIdsXml = string.Empty;

        /// <summary>
        /// variable holds the selected Comments ids xml string.
        /// </summary>
        private string selectedCommentsValueIdXml = string.Empty;

        /// <summary>
        /// commentsgridRowCount
        /// </summary>
        private int commentsgridRowCount;
        #endregion

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        /// Set Bool value to Valid Data
        /// </summary>
        private bool validData = true;

        /// <summary>
        /// textChangedInTextBox
        /// </summary>
        private bool textChangedInTextBox = false;

        /// <summary>
        /// create commentFrmId as Integer DataType
        /// </summary>
        private int commentFrmId;

        /// <summary>
        /// create commentKeyID as Integer DataType
        /// </summary>
        private int commentKeyID;

        /// <summary>
        /// Intialize CurrentRow to Zero
        /// </summary>
        private int currentRow;

          private PermissionFields slicePermissionField;
        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;
        private bool formMasterPermissionEdit;
        private int masterFormNo;

        /// <summary>
        /// commentsPublic
        /// </summary>
        ////private bool commentsPublic;

        /// <summary>
        /// Set Style to DataGrid Header
        /// </summary>
        ////private System.Windows.Forms.DataGridViewCellStyle commentHeader = new System.Windows.Forms.DataGridViewCellStyle();

        /// <summary>
        /// Set Style to DataGrid Cell
        /// </summary>
        ////private System.Windows.Forms.DataGridViewCellStyle commentDefaultCell = new System.Windows.Forms.DataGridViewCellStyle();

        /// <summary>
        ///  Valid DataSet
        /// </summary>
        private bool validDataSet;

        /// <summary>
        ///  Valid DataSet
        /// </summary>
        private bool dataChanged;

        /// <summary>
        /// IsOnLoad
        /// </summary>
        private bool isonLoad;

        /// <summary>
        /// Flag
        /// </summary>
        private bool flag;

        /// <summary>
        /// USed to store comment  count
        /// </summary>
        private int commentCount;

        /// <summary>
        ///  Used To Hold the Current Row;
        /// </summary>
        private int commentGridRowIndex;

        /// <summary>
        ///  Used To Hold the Current Row;
        /// </summary>
        private int commentGridAdminCount;

        /// <summary>
        ///  Temp Store Row ID
        /// </summary>
        private int tempRowId;

        /// <summary>
        /// used to store coomentID
        /// </summary>
        private int commentID;

        /// <summary>
        /// used to store coomentID
        /// </summary>
        ////private bool sortedStatus;

        /// <summary>
        /// used to store coomentID
        /// </summary>
        ////private string sortedOrder;

        /// <summary>
        /// used to store coomentID
        /// </summary>
        ////private string sortedColumn;

        /// <summary>
        /// used to store coomentID
        /// </summary>
        private int tempColumnID;

        /// <summary>
        /// used to store coomentID
        /// </summary>
        private bool closingNow;

        /// <summary>
        /// used to store prioroty
        /// </summary>
        private bool highPriorityFlag;

        /// <summary>
        /// listTemplateDate
        /// </summary>
        private F9075CommentTemplate listTemplateDate = new F9075CommentTemplate();

        /// <summary>
        /// getCommentTemplateData
        /// </summary>
        private F9076NewCommentTemplateData getCommentTemplateData = new F9076NewCommentTemplateData();

       #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CommentsForm"/> class.
        /// </summary>
        /// <param name="currentformId">The currentform id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="parentformId">The parentform id.</param>
        public F9075(int currentformId, int keyId, int parentformId)
        {
            this.InitializeComponent();
            this.CancelButton = this.CancelCommentButton;
            this.keyField = "CommentID";
            this.formNo = 9075;
            //this.SetPriorityCombo();
            if (!String.IsNullOrEmpty(keyId.ToString()))
            {
                this.commentKeyID = keyId;
                this.commentFrmId = currentformId;
                //this.CommentTextBox.Enabled = false;
                //this.CommentTextBox.LockKeyPress = true;
                this.CommentTextBox.BackColor = Color.White;
                ////this.Tag = parentformId;

                /* // Used to display the Comments recipt ID or statement ID.
                if (this.commentFrmId == 1000)
                {
                    this.ReceiptNoLabel.Text = "Comments for Receipt : " + this.commentKeyID;
                }
                else if (this.commentFrmId == 1020)
                {
                    this.ReceiptNoLabel.Text = "Comments for Statement : " + this.commentKeyID;
                }
                else if (this.commentFrmId == 1010)
                {
                    this.ReceiptNoLabel.Text = "Comments for Mortgage Import : " + this.commentKeyID;
                } */
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("EmptyKeyId"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ////TerraScanCommon.SetSameProperty(this.CommentsDataGridView, this.commentsGridViewEmpty, 4);

            //// this.FillFormID();
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;


        #endregion Event Publication

        #region enumeratorButtonOperation

        /// <summary>
        /// Button Functionality
        /// </summary>
        public enum ButtonOperation
        {
            /// <summary>
            /// Empty = 0.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// New = 1.
            /// </summary>
            New = 1,

            /// <summary>
            /// Save = 2.
            /// </summary>
            Save = 2,

            /// <summary>
            /// Delete = 3.
            /// </summary>
            Delete = 3,

            /// <summary>
            /// Cancel = 4.
            /// </summary>
            Cancel = 4,

            /// <summary>
            /// Cancel = 5.
            /// </summary>
            GridOperation = 5,

            /// <summary>
            /// Update = 6.
            /// </summary>
            Update = 6
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the comment key ID.
        /// </summary>
        /// <value>The comment key ID.</value>
        public string CommentKeyId
        {
            get { return this.commentKeyID.ToString(); }
            set { this.commentKeyID = Convert.ToInt32(value); }
        }

        /// <summary>
        /// Gets or sets the comment FRM id.
        /// </summary>
        /// <value>The comment FRM id.</value>
        public string CommentFrmId
        {
            get { return this.commentFrmId.ToString(); }
            set { this.commentFrmId = Convert.ToInt32(value); }
        }

        /// <summary>
        /// Gets or sets the parentform id.
        /// </summary>
        /// <value>The parentform id.</value>
        public string ParentformId
        {
            get { return this.Tag.ToString(); }
            set { this.Tag = value; }
        }

        /// <summary>
        /// For F9075Control
        /// </summary>
        [CreateNew]
        public F9075Controller Form9075Control
        {
            get { return this.form9075Control as F9075Controller; }
            set { this.form9075Control = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [high priority flag].
        /// </summary>
        /// <value><c>true</c> if [high priority flag]; otherwise, <c>false</c>.</value>
        public bool HighPriorityFlag
        {
            get { return this.highPriorityFlag; }
            set { this.highPriorityFlag = value; }
        }

        /// <summary>
        /// Gets or sets the comment count.
        /// </summary>
        /// <value>The comment count.</value>
        public int CommentCount
        {
            get { return this.commentCount; }
            set { this.commentCount = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// the Form Permissions will be captured and Set the Button Action Modes
        /// </summary>
        /// <param name="e">OnLoad Even Args</param>
        protected override void OnLoad(EventArgs e)
        {
            this.isonLoad = true;
            base.OnLoad(e);

            if (!TerraScanCommon.Administrator.Equals(true))
            {
                this.TemplateCommentButton.Visible = false;
            }

            this.FromTemplateList();

            this.SetPriorityCombo();


            this.LoadCommentDataGridView();
            //this.FromTemplateList();

            //this.SetPriorityCombo();

            if (this.commentCount > 0)
            {
                this.CommentsDataGridView.Focus();
                this.SetCommentTextBoxValues(0);
                this.CheckEditPermission();
                
                if (this.commentDataSet.GetComments.Rows.Count != 0
                       && this.CommentsDataGridView.Rows[0].Cells["IsAdmin"].Value != null
                       && !string.IsNullOrEmpty(this.CommentsDataGridView.Rows[0].Cells["IsAdmin"].Value.ToString().Trim()))
                {
                    if (this.CommentsDataGridView.Rows[0].Cells["IsAdmin"].Value.ToString().Trim().ToUpper().Equals("FALSE"))
                    {
                       // this.CommentTextBox.Enabled = false;
                       
                        this.CommentsDataGridView.Rows[0].Selected = true;
                        this.CommentTextBox.SetFocusColor = System.Drawing.Color.White;
                        this.CommentTextBox.ForeColor = System.Drawing.Color.Gray;
                        this.CommentTextBox.IsEditable = false;
                        this.CommentTextBox.ReadOnly = true;
                        this.CommentTextBox.LockKeyPress = true;

                        this.CommentPublicCheckBox.Enabled = false;
                        this.CommentPrintCheckBox.Enabled = false;
                        this.willRollCheckBox.Enabled = false;
                        this.CommentsPriorityCombo.Enabled = false;
                        this.CommentTextBox.BackColor = System.Drawing.Color.White;
                        this.CommentsPriorityCombo.BackColor = System.Drawing.Color.White;

                        this.CommentDateTextBox.Enabled = false;
                        this.CommentDateTextBox.BackColor = System.Drawing.Color.White;
                        this.CommentFormIDTextBox.Enabled = false;
                        this.CommentFormIDTextBox.BackColor = System.Drawing.Color.White;
                        this.CommentUserTextBox.Enabled = false;
                        this.CommentUserTextBox.BackColor = System.Drawing.Color.White;
                        this.SetCommenButton(ButtonOperation.Empty);
                        this.DeleteCommentButton.Enabled = false;
                    }
                }
            }
            else
            {
                this.CommentTextBox.Enabled = true;
                this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
                this.CommentTextBox.ForeColor = System.Drawing.Color.Black;

                this.CommentsDataGridView.Rows[0].Selected = false;
                this.CommentsDataGridView.CurrentCell = null;
                this.CommentsDataGridView.Rows[0].Cells[0].Selected = false;
                this.DeleteCommentButton.Enabled = false;
                //// if Count is 0  then assing focus to NewButton
                this.NewCommentButton.Focus();
            }

            /* if (this.commentKeyID > 0)
             {
                 this.ReceiptNoLabel.Text = "Comments for: " + this.commentFrmId + "-" + this.commentKeyID;
             }
             else
             {
                 this.ReceiptNoLabel.Text = "Comments for: " + this.commentFrmId;
             }*/

            this.CancelButton = this.CancelCommentButton;
            this.SetCancelButton();
            this.isonLoad = false;

            //this.CommentTextBox.LockKeyPress = true;
            //this.CommentTextBox.BackColor = Color.White;
            //this.CommentPublicCheckBox.Enabled = false;

            //if (TerraScanCommon.IsFieldUser)
            //{
            //    this.DeleteCommentButton.Enabled = false; 
            //}
        }

        /// <summary>
        /// Checks the edit permission.
        /// </summary>
        private void CheckEditPermission()
        {
            if (this.FormPermissionFields.editPermission)
            {
                ////this.EnableCommentControl();
                this.CommentTextBox.LockKeyPress = false;
                this.CommentPublicCheckBox.Enabled = true;
                this.CommentPrintCheckBox.Enabled = true;
                this.willRollCheckBox.Enabled = true;
                this.CommentsPriorityCombo.Enabled = true;
            }
            else
            {
                ////this.DisableCommentControl();
                this.CommentTextBox.LockKeyPress = true;
                this.CommentTextBox.BackColor = Color.White;
                this.CommentPublicCheckBox.Enabled = false;
                this.CommentPrintCheckBox.Enabled = false;
                this.willRollCheckBox.Enabled = false;
                this.CommentsPriorityCombo.Enabled = false;
            }
        }

        /// <summary>
        /// Checks the record count.
        /// </summary>
        private void CheckRecordCount()
        {
            if (this.commentCount <= 0)
            {
                if (this.DeleteCommentButton.ActualPermission == true)
                {
                    this.DeleteCommentButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the Comments control.
        /// This Event Load the CommentsDataGridView and DisableThe controls 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OnLoad(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the CommentNewButton control.
        /// Initialize The Controls and Create A Empty DataRow
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentNewButton_Click(object sender, EventArgs e)
        {
            if (this.NewCommentButton.Enabled)
            {
                this.Cursor = Cursors.WaitCursor;
                this.EnableCommentControl();
                this.CommentTextBox.LockKeyPress = false;
                this.buttonOperation = (int)ButtonOperation.New;
                this.SetCommenButton(ButtonOperation.New);
                this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.NewMode);
                this.currentRow = 0;
                this.tempRowId = 0;
                //if (this.commentCount > 0)
                //{
                //    this.SetDataGridViewPosition(this.currentRow);
                //}

                this.FillFormID();
                this.SetFocus();
                this.CommentDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                this.CommentUserTextBox.Text = TerraScanCommon.UserName;
                this.CommentsDataGridView.Enabled = false;
                this.CommentTextBox.Text = string.Empty;
                this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
                this.CommentPublicCheckBox.Checked = false;
                this.CommentPrintCheckBox.Checked = false;
                this.willRollCheckBox.Checked = false;
                this.CommentLinkLabel.Text = this.LinkLableValueText;
                this.CommentLinkLabel.Enabled = false;
                if (this.CommentsPriorityCombo.Items.Count > 0)
                {
                    this.CommentsPriorityCombo.SelectedIndex = 0;
                }
                else
                {
                    this.CommentsPriorityCombo.SelectedIndex = -1;
                }
                ////Commented by Biju to Fix #3218
                //this.CommentFromTemplateComboBox.SelectedIndex = 0;
                this.Cursor = Cursors.Default;

                this.SetCancelButton();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }
            //// if (this.CommentsDataGridView.Rows.Count > 0)
            //// {
            ////     this.CommentsDataGridView.Rows[0].Selected = false;
            //// }
        }

        /// <summary>
        /// This Event  is used to Save  and Update the Datas to the DataBase
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveCommentButton_Click(object sender, EventArgs e)
        {
            if (this.SaveCommentButton.Enabled)
            {
                ////Calls The function To Savethe Comment Details
                this.SaveCommentDetails();
            }
        }

        /// <summary>
        /// Save Comment details
        /// </summary>
        private void SaveCommentDetails()
        {
            int canPrint;
            int canPublic;
            int foundIndex;
            int dateFileId = 0;
            int editCommentId = 0;
            int canRoll;
            int commentPriority = 0;
            ////bool validPublic = true;
            try
            {
                /* if (this.commentsPublic)
                 {
                     if (this.CommentPublicCheckBox.Checked)
                     {
                         validPublic = true;
                     }
                     else
                     {
                         validPublic = false;
                     }
                 }*/
                this.Cursor = Cursors.WaitCursor;

                if (this.RequiredField())
                {
                    if (this.CommentPublicCheckBox.Checked)
                    {
                        canPublic = 1;
                    }
                    else
                    {
                        canPublic = 0;
                    }

                    if (this.CommentPrintCheckBox.Checked)
                    {
                        canPrint = 1;
                    }
                    else
                    {
                        canPrint = 0;
                    }

                    if (this.willRollCheckBox.Checked)
                    {
                        canRoll = 1;
                    }
                    else
                    {
                        canRoll = 0;
                    }

                    if (this.CommentsPriorityCombo.SelectedIndex >= 0)
                    {
                        int.TryParse(this.CommentsPriorityCombo.SelectedValue.ToString(), out commentPriority);
                    }



                    if (this.buttonOperation == (int)ButtonOperation.New)
                    {
                        ////WSHelper.SaveComments(0, Convert.ToInt32(this.CommentFormIDTextBox.Text), Convert.ToInt32(this.CommentKeyIDTextBox.Text), Convert.ToDateTime(this.CommentDateTextBox.Text), TerraScanCommon.UserId, this.CommentTextBox.Text.Trim(), canPrint, canPublic, this.CommentsPriorityCombo.SelectedIndex);
                        this.Form9075Control.WorkItem.SaveComments(0, Convert.ToInt32(this.CommentFormIDTextBox.Text), this.commentKeyID, Convert.ToDateTime(this.CommentDateTextBox.Text), TerraScanCommon.UserId, this.CommentTextBox.Text.Trim(), canPrint, canPublic, this.CommentsPriorityCombo.SelectedIndex, canRoll, commentPriority);
                        if (!WSHelper.IsOnLineMode && this.commentID > 0)
                            TerraScanCommon.AddFieldUseValues(this.commentID, this.keyField, this.formNo, null, TerraScanCommon.UserId);
                    }
                    else
                    {
                        ////WSHelper.SaveComments(this.commentID, Convert.ToInt32(this.CommentFormIDTextBox.Text), Convert.ToInt32(this.CommentKeyIDTextBox.Text), Convert.ToDateTime(this.CommentDateTextBox.Text), TerraScanCommon.UserId, this.CommentTextBox.Text.Trim(), canPrint, canPublic, this.CommentsPriorityCombo.SelectedIndex);
                        this.Form9075Control.WorkItem.SaveComments(this.commentID, Convert.ToInt32(this.CommentFormIDTextBox.Text), this.commentKeyID, Convert.ToDateTime(this.CommentDateTextBox.Text), TerraScanCommon.UserId, this.CommentTextBox.Text.Trim(), canPrint, canPublic, this.CommentsPriorityCombo.SelectedIndex, canRoll, commentPriority);
                        if (!WSHelper.IsOnLineMode && this.commentID>0)
                            TerraScanCommon.InsertFieldUseDetails(this.commentID, this.keyField, TerraScanCommon.UserId);
                        this.flag = true;
                        this.CommentFromTemplateComboBox.SelectedIndex = 0;
                        this.flag = false;

                        this.currentRow = this.tempRowId;
                        editCommentId = Convert.ToInt32(this.CommentsDataGridView.Rows[this.currentRow].Cells["CommentID"].Value.ToString());
                    }
                    ////Added by Biju to Fix #3218
                    CommentFromTemplateComboBox.SelectedIndex = 0;

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.closingNow = true;
                    this.SetCommenButton(ButtonOperation.Empty);
                    this.Cursor = Cursors.Default;
                    this.SetCancelButton();
                    this.LoadCommentDataGridView();
                    this.Cursor = Cursors.WaitCursor;

                    if (this.buttonOperation == (int)ButtonOperation.New)
                    {
                        this.currentRow = 0;
                        DataSet commentIdDataset = new DataSet();
                        DataRow[] newRowCommentID;
                        newRowCommentID = this.commentDataSet.GetComments.Select("CommentID = MAX(CommentID)");
                        commentIdDataset.Merge(newRowCommentID);
                        dateFileId = Convert.ToInt32(commentIdDataset.Tables[0].Rows[0]["CommentID"].ToString());
                        BindingSource source = new BindingSource();
                        source.DataSource = this.commentDataSet.GetComments;
                        foundIndex = source.Find("CommentID", dateFileId);
                        this.SetDataGridViewPosition(foundIndex);
                        this.SetCommentTextBoxValues(foundIndex);
                    }
                    else if (this.buttonOperation == (int)ButtonOperation.Update)
                    {
                        this.currentRow = this.tempRowId;
                        BindingSource source = new BindingSource();
                        source.DataSource = this.commentDataSet.GetComments;
                        foundIndex = source.Find("CommentID", editCommentId);
                        this.SetDataGridViewPosition(foundIndex);
                        this.SetCommentTextBoxValues(foundIndex);
                    }

                    this.EnableCommentsGridSorting();
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.SaveMode);
                    this.buttonOperation = (int)ButtonOperation.Empty;

                    this.dataChanged = false;
                    if (this.validDataSet)
                    {
                        ////this.SetCommentCount(this.commentCount);
                    }

                    this.CommentsDataGridView.Focus();
                    this.CommentLinkLabel.Enabled = true;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    this.closingNow = false;
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CommentTextBox.Focus();
                }

                this.CheckEditPermission();
                this.SetCancelButton();
                //if(TerraScanCommon.IsFieldUser)
                //{
                //    this.DeleteCommentButton.Enabled = false;  
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteCommentButton control.
        /// this Event used to delete  a  selected record
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteCommentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.commentCount > 0)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        ////this.selected = this.CommentsDataGridView.SelectedRows[0].Index;
                        ////WSHelper.DeleteComments(Convert.ToInt32(this.CommentsDataGridView.Rows[this.tempRowId].Cells["KeyID"].Value.ToString()), this.commentFrmId, Convert.ToInt32(this.CommentsDataGridView.Rows[this.tempRowId].Cells["CommentID"].Value.ToString()));

                        this.Form9075Control.WorkItem.DeleteComments(Convert.ToInt32(this.CommentsDataGridView.Rows[this.tempRowId].Cells["KeyID"].Value.ToString()), this.commentFrmId, Convert.ToInt32(this.CommentsDataGridView.Rows[this.tempRowId].Cells["CommentID"].Value.ToString()), TerraScanCommon.UserId);
                        this.commentSelectedRow = this.CommentsDataGridView.CurrentRow.Index;
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.LoadCommentDataGridView();
                        if (this.commentSelectedRow == this.commentCount)
                        {
                            this.commentSelectedRow = 0;
                        }
                        else if (this.commentCount == 1)
                        {
                            this.commentSelectedRow = 0;
                            this.tempRowId = 0;
                        }
                        ////else if (this.tempRowId > this.commentCount)
                        ////{
                        ////    commenGridRow = this.tempRowId - 1;
                        ////}
                        ////else if (this.tempRowId == this.commentCount)
                        ////{
                        ////    this.tempRowId = this.tempRowId - 1;
                        ////    commenGridRow = this.tempRowId;
                        ////}

                        this.SetDataGridViewPosition(this.commentSelectedRow);
                        this.SetCancelButton();
                        if (this.CheckValidDataSet(this.commentDataSet))
                        {
                            ////this.SetCommentCount(this.commentCount);
                            if (this.commentCount == 0)
                            {
                                this.ClearTextBoxes();
                                this.DisableCommentControl();
                                this.NewCommentButton.Focus();

                                if (this.CommentsDataGridView.Rows.Count > 0)
                                {
                                    this.CommentsDataGridView.Rows[0].Selected = false;
                                    this.CommentsDataGridView.CurrentCell = null;
                                }
                            }
                            else
                            {
                                this.CheckEditPermission();
                                this.CommentsDataGridView.Focus();
                            }
                        }

                        //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                        this.buttonOperation = (int)ButtonOperation.Empty;
                        this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.DeleteMode);
                        if (this.commentCount <= 0)
                        {
                            this.DisableCommentControl();
                            this.DeleteCommentButton.Enabled = false;
                            this.CommentLinkLabel.Text = this.LinkLableValueText + " " + " " + ""; ////this.CommentIDTextBox.Text;
                        }
                        else
                        {
                            if (commentSelectedRow >= 0 && this.commentDataSet.GetComments.Rows.Count != 0
                                   && this.CommentsDataGridView.Rows[commentSelectedRow].Cells["IsAdmin"].Value != null
                                   && !string.IsNullOrEmpty(this.CommentsDataGridView.Rows[commentSelectedRow].Cells["IsAdmin"].Value.ToString().Trim()))
                            {

                                if (this.CommentsDataGridView.Rows[commentSelectedRow].Cells["IsAdmin"].Value.ToString().Trim().ToUpper().Equals("FALSE"))
                                {
                                    //this.CommentTextBox.Enabled = false;
                                    this.CommentTextBox.SetFocusColor = System.Drawing.Color.White;
                                    this.CommentTextBox.ForeColor = System.Drawing.Color.Gray;
                                    //this.CommentTextBox.IsEditable = false;
                                    this.CommentTextBox.ReadOnly = true;
                                   // this.CommentTextBox.LockKeyPress = true;

                                    this.CommentPublicCheckBox.Enabled = false;
                                    this.CommentPrintCheckBox.Enabled = false;
                                    this.willRollCheckBox.Enabled = false;
                                    this.CommentsPriorityCombo.Enabled = false;
                                    this.CommentTextBox.BackColor = System.Drawing.Color.White;
                                    this.CommentsPriorityCombo.BackColor = System.Drawing.Color.White;

                                    this.CommentDateTextBox.Enabled = false;
                                    this.CommentDateTextBox.BackColor = System.Drawing.Color.White;
                                    this.CommentFormIDTextBox.Enabled = false;
                                    this.CommentFormIDTextBox.BackColor = System.Drawing.Color.White;
                                    this.CommentUserTextBox.Enabled = false;
                                    this.CommentUserTextBox.BackColor = System.Drawing.Color.White;

                                    //this.SetCommenButton(ButtonOperation.Empty);
                                    this.NewCommentButton.Enabled = true && this.NewCommentButton.ActualPermission;
                                    this.SaveCommentButton.Enabled = false;
                                    this.CancelCommentButton.Enabled = false;
                                    this.DeleteCommentButton.Enabled = false;
                                    this.CommentsDataGridView.Enabled = true;
                                    this.SetCancelButton();
                                }
                                else
                                {
                                    if (this.FormPermissionFields.editPermission)
                                    {
                                       // this.CommentTextBox.BackColor = System.Drawing.Color.White;
                                        this.CommentTextBox.Enabled = true;
                                        this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
                                        this.CommentTextBox.ForeColor = System.Drawing.Color.Black;
                                        this.CommentPublicCheckBox.Enabled = true;
                                        this.CommentPrintCheckBox.Enabled = true;
                                        this.willRollCheckBox.Enabled = true;
                                        this.CommentsPriorityCombo.Enabled = true;
                                        this.CommentDateTextBox.Enabled = true;
                                        this.CommentFormIDTextBox.Enabled = true;
                                        this.CommentUserTextBox.Enabled = true;
                                        this.SetCommenButton(ButtonOperation.Empty);
                                    }

                                    //this.DeleteCommentButton.Enabled = true;
                                }
                            }
                        }

                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        this.SetFocus();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelCommentButton control.
        /// This Event used to RoolBack all the changes.
        /// </summary>
        /// <param name="sender">CancelButton</param>
        /// <param name="e">Click</param>    
        private void CancelCommentButton_Click(object sender, EventArgs e)
        {
            /*if (this.dataChanged || this.buttonOperation == (int)ButtonOperation.New)
            {
                //if (TerraScanMessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //{
                if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.CommentsDataGridView.Enabled = true;
                    this.dataChanged = false;
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.CancelMode);
                    this.CheckRecordCount();

                    if (this.buttonOperation == (int)ButtonOperation.New && this.commentCount == 0)
                    {
                        this.CommentsDataGridView.Enabled = false;
                        this.NewCommentButton.Focus(); 
                    }

                  
                  
                    this.buttonOperation = (int)ButtonOperation.Empty;
                    if ( this.commentCount == 0)
                    {
                        this.ClearTextBoxes();
                        this.DisableCommentControl(); 
                        this.CommentsDataGridView.Rows[0].Selected = false;
                        this.CommentLinkLabel.Text = this.LinkLableValueText + " "; ///this.CommentIDTextBox.Text;
                    }
                    else
                    {
                        this.SetCommentTextBoxValues(0);
                        this.SetDataGridViewPosition(0);
                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ///this.CommentIDTextBox.Text;
                        
                    }
                    this.CommentLinkLabel.Enabled = true;
                    
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    if (this.dataChanged || this.buttonOperation == (int)ButtonOperation.New)
                    {
                        this.CommentsDataGridView.Enabled = true;
                    }
                    this.SetFocus();
                }
            }*/

            try
            {
                this.DialogResult = DialogResult.None;

                //// Prevents Asking alter
                this.flag = true;
                this.CommentFromTemplateComboBox.SelectedIndex = 0;
                this.flag = false;
                this.Cursor = Cursors.WaitCursor;
                int rowSelected = 0;
                rowSelected = this.GetRowIndex();

                this.CommentsDataGridView.Enabled = true;
                this.dataChanged = false;
                this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.CancelMode);
                this.CheckRecordCount();

                if (this.buttonOperation == (int)ButtonOperation.New && this.commentCount == 0)
                {
                    this.CommentsDataGridView.Enabled = false;
                    this.NewCommentButton.Focus();
                }

                this.buttonOperation = (int)ButtonOperation.Empty;
                if (this.commentCount == 0)
                {
                    this.ClearTextBoxes();
                    this.DisableCommentControl();
                    this.CommentsDataGridView.Rows[0].Selected = false;
                    this.CommentsDataGridView.CurrentCell = null;
                    this.CommentLinkLabel.Enabled = false;
                }
                else
                {
                    this.SetCommentTextBoxValues(rowSelected);
                    if (this.CommentsDataGridView.SelectedCells.Count > 0)
                    {
                        this.SetDataGridViewPosition(rowSelected, this.CommentsDataGridView.CurrentCell.ColumnIndex);
                    }
                    else
                    {
                        this.SetDataGridViewPosition(rowSelected);
                    }

                    this.CommentLinkLabel.Enabled = true;
                    this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                    this.EnableCommentsGridSorting();
                    this.CheckEditPermission();
                    this.CommentsDataGridView.Focus();
                }

                this.SetCancelButton();
                this.Cursor = Cursors.Default;
                this.SelectAllCheckBox.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the CommentPrintRadioButton control.
        ///  This Event  is used to select or unSelect the CommentPrintRadioButton control and  allow 
        ///  only one CommentPrintRadioButton to select at time.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentPrintRadioButton_Click(object sender, EventArgs e)
        {
            ////DataGridViewRowCollection rowCollection = this.CommentsDataGridView.Rows;
            ////foreach (DataGridViewRow rows in rowCollection)
            ////{
            ////    if (rows.Index == this.commentsCm.Position && this.buttonOperation != (int)ButtonOperation.New)
            ////    {
            ////        this.CommentsDataGridView.Rows[commentsCm.Position].Cells["IsPrintOnReceipt"].Value = true;
            ////    }
            ////    else
            ////    {
            ////        this.CommentsDataGridView.Rows[rows.Index].Cells["IsPrintOnReceipt"].Value = false;
            ////    }
            ////}

            ////if (this.commentsCm != null && this.commentsCm.Position >= 0)
            ////{
            ////    this.commentsCm.EndCurrentEdit();
            ////}
        }

        private void SetEditRecord()
        {

            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.FormPermissionFields.editPermission)
            {

                this.RemoveButton.Enabled = false;
                //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                this.SelectAllCheckBox.Checked = false;
                this.SelectAllCheckBox.Enabled = false;
                this.selectedCommentIds = new List<int>();
                //this.ReadOnlyAll("false");
                //end
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }
        /// <summary>
        /// Handles the KeyPress event of the CommentTextBox control.
        /// This Event Used To Update The CurrencyManager
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CommentTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((TerraScanCommon.isadministrator == false) && (this.CommentTextBox.ReadOnly == true) && (this.FormPermissionFields.editPermission))
            {
               
            }
            else
            //if (this.FormPermissionFields.editPermission)
            {
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }

                    case (char)10:
                        {
                            e.Handled = true;
                            break;
                        }

                    /*modified for bug fixing for validating "Ctrl + I" keys-displaying junk values.*/
                    case (char)9:
                        {
                            e.Handled = true;
                            break;
                        }

                    default:
                        {
                            if (!this.dataChanged)
                            {
                                this.dataChanged = true;
                                if (this.buttonOperation != (int)ButtonOperation.New)
                                {
                                    if (!string.IsNullOrEmpty(this.CommentTextBox.Text))
                                    {
                                        this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                                        this.SetCommenButton(ButtonOperation.Update);
                                        this.buttonOperation = (int)ButtonOperation.Update;
                                        this.DisableCommentsGridSorting();
                                        this.RemoveButton.Enabled = false;
                                        //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                                        this.SelectAllCheckBox.Checked = false;
                                        this.SelectAllCheckBox.Enabled = false;
                                        this.selectedCommentIds = new List<int>();
                                        this.ReadOnlyAll("false");
                                    }
                                    else
                                    {                                        
                                        this.CommentTextBox.LockKeyPress = true;
                                        this.CommentTextBox.BackColor = Color.White;
                                        this.MainPanel.BackColor = Color.White;
                                        this.CommentPublicCheckBox.Enabled = false;
                                        this.Cursor = Cursors.Default;
                                        ActiveControl = null;
                                        this.dataChanged = false;
                                       

                                    }
                                }
                            }

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Sets the comment text box values.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetCommentTextBoxValues(int rowId)
        {
            if (this.commentDataSet.GetComments.Rows.Count != 0)
            {
                if (rowId >= 0)
                {
                    this.CommentDateTextBox.Text = this.CommentsDataGridView.Rows[rowId].Cells["CommentDate"].Value.ToString();
                    this.commentID = Convert.ToInt32(this.CommentsDataGridView.Rows[rowId].Cells["CommentId"].Value.ToString());
                    this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;

                    if (!string.IsNullOrEmpty(this.CommentsDataGridView.Rows[rowId].Cells["IsRoll"].Value.ToString()))
                    {
                        this.willRollCheckBox.Checked = Convert.ToBoolean(this.CommentsDataGridView.Rows[rowId].Cells["IsRoll"].Value.ToString());
                    }
                    else
                    {
                        this.willRollCheckBox.Checked = false;
                    }

                    if (!string.IsNullOrEmpty(this.CommentsDataGridView.Rows[rowId].Cells["IspublicComment"].Value.ToString()))
                    {
                        this.CommentPublicCheckBox.Checked = Convert.ToBoolean(this.CommentsDataGridView.Rows[rowId].Cells["IspublicComment"].Value.ToString());
                    }
                    else
                    {
                        this.CommentPublicCheckBox.Checked = false;
                    }

                    if (!string.IsNullOrEmpty(this.CommentsDataGridView.Rows[rowId].Cells["IsPrintOnReceipt"].Value.ToString()))
                    {
                        this.CommentPrintCheckBox.Checked = Convert.ToBoolean(this.CommentsDataGridView.Rows[rowId].Cells["IsPrintOnReceipt"].Value.ToString());
                    }
                    else
                    {
                        this.CommentPrintCheckBox.Checked = false;
                    }

                    //int correctIndex = 0;
                    ////// get the index of the cfgValue
                    //correctIndex = this.CommentsPriorityCombo.FindString(this.CommentsDataGridView.Rows[rowId].Cells["IsPriority"].Value.ToString());
                    //this.CommentsPriorityCombo.SelectedIndex = correctIndex;

                    if (this.CommentsDataGridView.Rows[rowId].Cells["CommentPriorityID"].Value != null
                        && !string.IsNullOrEmpty(this.CommentsDataGridView.Rows[rowId].Cells["CommentPriorityID"].Value.ToString().Trim()))
                    {
                        int commentPriority = 0;
                        int.TryParse(this.CommentsDataGridView.Rows[rowId].Cells["CommentPriorityID"].Value.ToString(), out commentPriority);
                        //if (commentPriority > 0)
                        //{
                        this.CommentsPriorityCombo.SelectedValue = commentPriority;
                        //}
                        //else
                        //{
                        //    this.CommentsPriorityCombo.SelectedIndex = -1;
                        //}
                    }
                    else
                    {
                        this.CommentsPriorityCombo.SelectedIndex = -1;
                    }

                    this.CommentUserTextBox.Text = this.CommentsDataGridView.Rows[rowId].Cells["UserName"].Value.ToString();
                    this.CommentFormIDTextBox.Text = this.CommentsDataGridView.Rows[rowId].Cells["FormId"].Value.ToString();
                    ////this.CommentIDTextBox.Text = this.CommentsDataGridView.Rows[rowId].Cells["CommentId"].Value.ToString();
                    this.CommentTextBox.Text = this.CommentsDataGridView.Rows[rowId].Cells["Comment"].Value.ToString().Trim();

                    int tempKeyId = 0;
                    int.TryParse(this.CommentsDataGridView.Rows[rowId].Cells["KeyID"].Value.ToString(), out tempKeyId);

                    ////if (tempKeyId > 0)
                    ////{
                    ////    this.CommentKeyIDTextBox.Text = tempKeyId.ToString();
                    ////}
                    ////else
                    ////{
                    ////    this.CommentKeyIDTextBox.Text = string.Empty;
                    ////}
                }
            }
        }

        /// <summary>
        /// Sets the choice data combo.
        /// </summary>
        private void SetPriorityCombo()
        {
            //this.CommentsPriorityCombo.Items.Clear();
            //this.CommentsPriorityCombo.Items.Insert(0, "LOW");
            //this.CommentsPriorityCombo.Items.Insert(1, "HIGH");

            if (this.listTemplateDate.CommentPriorities.Rows.Count > 0)
            {
                this.CommentsPriorityCombo.DataSource = this.listTemplateDate.CommentPriorities;
                this.CommentsPriorityCombo.DisplayMember = this.listTemplateDate.CommentPriorities.CommentPriorityColumn.ColumnName;
                this.CommentsPriorityCombo.ValueMember = this.listTemplateDate.CommentPriorities.CommentPriorityIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the CommentsDataGridView control.
        /// This Event Used to check only one isPrintColumn  is selected
        /// and also enable the controls and set focus in CommentTextBox
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CommentsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= -1)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.commentGridRowIndex = e.RowIndex;
                    if (!this.dataChanged)
                    {
                        this.tempRowId = this.commentGridRowIndex;
                        this.tempColumnID = e.ColumnIndex;
                    }

                    if (this.dataChanged)
                    {
                        if (this.CommentTextBox.Enabled)
                        {
                            this.CommentsDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsValid")].Value = true;
                        }
                        if (this.tempRowId != this.commentGridRowIndex)
                        {
                            ////if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            ////{
                            ////    this.tempRowId = e.RowIndex;

                            ////    this.dataChanged = false;

                            ////    this.SetCommenButton(ButtonOperation.Empty);
                            ////    this.SetCommentTextBoxValues(this.tempRowId);
                            ////    this.SetDataGridViewPosition(this.tempRowId);
                            ////  //  this.CommentTextBox.Focus();
                            ////    this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID + " )"; ///this.CommentIDTextBox.Text;
                            ////}
                            ////else
                            ////{
                            ////    ///  this.SetEnableControls(e.ColumnIndex);
                            ////    //this.SetCommentTextBoxValues(this.tempRowId);
                            ////    this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID + " )"; ///this.CommentIDTextBox.Text;
                            ////    this.SetDataGridViewPosition(this.tempRowId,this.tempColumnID);
                            ////    ///this.CommentTextBox.Focus();
                            ////}

                            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        this.SaveCommentDetails();
                                        if (this.closingNow)
                                        {
                                            this.LoadCommentDataGridView();
                                            this.tempRowId = e.RowIndex;
                                            this.SetCommentTextBoxValues(this.tempRowId);
                                            this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                                            this.SetDataGridViewPosition(this.tempRowId);
                                            this.dataChanged = false;
                                            this.SetCommenButton(ButtonOperation.Empty);
                                            this.EnableCommentsGridSorting();
                                            this.SetCancelButton();

                                        }
                                        else
                                        {
                                            //// this.SetCommentTextBoxValues(this.tempRowId);
                                            this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                                            this.SetDataGridViewPosition(this.tempRowId, e.ColumnIndex);
                                            this.SetCancelButton();
                                        }

                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.tempRowId = e.RowIndex;
                                        this.SetCommentTextBoxValues(this.tempRowId);
                                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; /////this.CommentIDTextBox.Text;
                                        this.SetDataGridViewPosition(this.tempRowId, 0);
                                        this.dataChanged = false;
                                        this.SetCommenButton(ButtonOperation.Empty);
                                        this.EnableCommentsGridSorting();
                                        this.SetCancelButton();
                                        this.SelectAllCheckBox.Enabled = true;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; /////this.CommentIDTextBox.Text;
                                        this.SetDataGridViewPosition(this.tempRowId, this.tempColumnID);
                                        this.CommentTextBox.Focus();
                                        this.SetCancelButton();
                                        this.SelectAllCheckBox.Enabled = true;
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; /////this.CommentIDTextBox.Text;
                        this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                        this.SetCommenButton(ButtonOperation.Empty);
                        //// this.SetEnableControls(e.ColumnIndex);
                        this.SetCommentTextBoxValues(e.RowIndex);
                        if (e.ColumnIndex >= 0)
                        {
                            this.SetDataGridViewPosition(e.RowIndex, e.ColumnIndex);
                        }
                        else
                        {
                            this.SetDataGridViewPosition(e.RowIndex);
                        }

                        this.SetCancelButton();
                    }

                    this.CheckEditPermission();

                    if (e.RowIndex >= 0 && this.commentDataSet.GetComments.Rows.Count != 0
                           && this.CommentsDataGridView.Rows[e.RowIndex].Cells["IsAdmin"].Value != null
                           && !string.IsNullOrEmpty(this.CommentsDataGridView.Rows[e.RowIndex].Cells["IsAdmin"].Value.ToString().Trim()))
                    {

                        if (this.CommentsDataGridView.Rows[e.RowIndex].Cells["IsAdmin"].Value.ToString().Trim().ToUpper().Equals("FALSE"))
                        {
                            // this.CommentTextBox.Enabled = false;
                            this.CommentTextBox.SetFocusColor = System.Drawing.Color.White;
                            this.CommentTextBox.ForeColor = System.Drawing.Color.Gray;
                            //this.CommentTextBox.IsEditable = false;
                            this.CommentTextBox.ReadOnly = true;
                            this.CommentTextBox.LockKeyPress = true;

                            this.CommentPublicCheckBox.Enabled = false;
                            this.CommentPrintCheckBox.Enabled = false;
                            this.willRollCheckBox.Enabled = false;
                            this.CommentsPriorityCombo.Enabled = false;
                            this.CommentTextBox.BackColor = System.Drawing.Color.White;
                            this.CommentsPriorityCombo.BackColor = System.Drawing.Color.White;

                            this.CommentDateTextBox.Enabled = false;
                            this.CommentDateTextBox.BackColor = System.Drawing.Color.White;
                            this.CommentFormIDTextBox.Enabled = false;
                            this.CommentFormIDTextBox.BackColor = System.Drawing.Color.White;
                            this.CommentUserTextBox.Enabled = false;
                            this.CommentUserTextBox.BackColor = System.Drawing.Color.White;

                            //this.SetCommenButton(ButtonOperation.Empty);
                            this.NewCommentButton.Enabled = true && this.NewCommentButton.ActualPermission;
                            this.SaveCommentButton.Enabled = false;
                            this.CancelCommentButton.Enabled = false;
                            this.DeleteCommentButton.Enabled = false;
                            this.CommentsDataGridView.Enabled = true;
                            this.SetCancelButton();
                        }
                        else
                        {
                            if (this.tempRowId == this.commentGridRowIndex)
                            {
                                this.CommentsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            }
                            else
                            {
                                if (this.FormPermissionFields.editPermission)
                                {
                                    // this.CommentTextBox.BackColor = System.Drawing.Color.White;
                                    this.CommentTextBox.Enabled = true;
                                    this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
                                    this.CommentTextBox.ForeColor = System.Drawing.Color.Black;
                                    this.CommentPublicCheckBox.Enabled = true;
                                    this.CommentPrintCheckBox.Enabled = true;
                                    this.willRollCheckBox.Enabled = true;
                                    this.CommentsPriorityCombo.Enabled = true;
                                    this.CommentDateTextBox.Enabled = true;
                                    this.CommentFormIDTextBox.Enabled = true;
                                    this.CommentUserTextBox.Enabled = true;
                                    this.SetCommenButton(ButtonOperation.Empty);
                                }
                            }
                            //this.DeleteCommentButton.Enabled = true;
                        }
                    }

                    ////}
                    ////else
                    ////{
                    ////    this.SetFocus();
                    ////}
                }
                else
                {
                    this.commentGridRowIndex = e.RowIndex;
                    if (!this.dataChanged)
                    {
                        this.tempRowId = this.commentGridRowIndex;
                        this.tempColumnID = e.ColumnIndex;
                    }

                    if (this.dataChanged)
                    {
                        if (this.CommentTextBox.Enabled)
                        {
                            this.CommentsDataGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsValid")].Value = true;
                        }
                        if (this.tempRowId != this.commentGridRowIndex)
                        {
                            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        this.SaveCommentDetails();
                                        if (this.closingNow)
                                        {
                                            this.LoadCommentDataGridView();
                                            this.tempRowId = e.RowIndex;
                                            this.SetCommentTextBoxValues(this.tempRowId);
                                            this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                                            this.SetDataGridViewPosition(this.tempRowId);
                                            this.dataChanged = false;
                                            this.SetCommenButton(ButtonOperation.Empty);
                                            this.EnableCommentsGridSorting();
                                            this.SetCancelButton();

                                        }
                                        else
                                        {
                                            //// this.SetCommentTextBoxValues(this.tempRowId);
                                            this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                                            this.SetDataGridViewPosition(this.tempRowId, e.ColumnIndex);
                                            this.SetCancelButton();
                                        }

                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.tempRowId = e.RowIndex;
                                        this.dataChanged = false;
                                        this.SetCommentTextBoxValues(this.tempRowId);
                                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; /////this.CommentIDTextBox.Text;
                                        this.SetDataGridViewPosition(this.tempRowId, 0);
                                        this.dataChanged = false;
                                        this.SetCommenButton(ButtonOperation.Empty);
                                        this.EnableCommentsGridSorting();
                                        this.SetCancelButton();
                                        this.SelectAllCheckBox.Enabled = true;
                                        this.pageMode=TerraScanCommon.PageModeTypes.View;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; /////this.CommentIDTextBox.Text;
                                        this.SetDataGridViewPosition(this.tempRowId, this.tempColumnID);
                                        this.CommentTextBox.Focus();
                                        this.SetCancelButton();
                                        this.SelectAllCheckBox.Enabled = false;
                                        break;
                                    }
                            }
                        }
                    }
                    DataGridViewRow row = CommentsDataGridView.Rows[e.RowIndex];
                    foreach (DataGridViewRow dr in CommentsDataGridView.Rows)
                    {
                        DataGridViewCheckBoxCell cell = dr.Cells["IsValid"] as DataGridViewCheckBoxCell;

                        if (cell.Value != null)
                        {
                            if (cell.Value.Equals(true))
                            {
                                if (row.Index == dr.Index)
                                {
                                    dr.Cells["IsValid"].Value = false;
                                }
                                else
                                {
                                    dr.Cells["IsValid"].Value = false;//It's checked!
                                }
                            }
                        }
                    }
                    DataGridViewRow obd = this.CommentsDataGridView.Rows[e.RowIndex];
                    obd.Cells["IsValid"].ReadOnly = true;
                }
            }
            else
            {
                //// this.DisableCommentControl();
            }
        }

        private void CommentsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                // Add and remove the selected items for remove to  selectedCommentIds 

                if (e.RowIndex >= 0 && e.RowIndex < this.CommentsDataGridView.OriginalRowCount)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (e.ColumnIndex.Equals(this.CommentsDataGridView.Columns["IsValid"].Index))
                        
                            {
                            int commentId;
                            int.TryParse(this.CommentsDataGridView.Rows[e.RowIndex].Cells[this.commentDataSet.GetComments.CommentIDColumn.ColumnName].Value.ToString(), out commentId);
                            if (commentId > 0)
                            {
                                if (Convert.ToBoolean(this.CommentsDataGridView.Rows[e.RowIndex].Cells[this.IsValid.Name].EditedFormattedValue) == true)
                                {
                                    this.CommentsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                                    
                                        if (this.selectedCommentIds.Contains(commentId))
                                        {
                                            this.selectedCommentIds.Remove(Convert.ToInt32(this.CommentsDataGridView[this.commentDataSet.GetComments.CommentIDColumn.ColumnName, e.RowIndex].Value));
                                            if (this.selectedCommentIds.Count == 0)
                                            {
                                                this.RemoveButton.Enabled = false;
                                            }
                                        }
                                        if (this.commentsgridRowCount == 0 && this.selectedCommentIds.Count == 0)
                                        {
                                            this.RemoveButton.Enabled = false;
                                        }
                                        if (this.commentsgridRowCount > this.selectedCommentIds.Count)
                                        {
                                            this.SelectAllCheckBox.Checked = false;

                                        }
                                    

                                }
                                else
                                {
                                    if (this.CommentsDataGridView.Rows[e.RowIndex].Cells["IsAdmin"].Value.ToString().Trim().ToUpper().Equals("TRUE"))
                                    {
                                        this.CommentsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                                        //if (!this.selectedCommentIds.Contains(commentId))
                                        //{
                                        //    this.selectedCommentIds.Add(Convert.ToInt32(this.CommentsDataGridView[this.commentDataSet.GetComments.CommentIDColumn.ColumnName, e.RowIndex].Value));
                                        //}

                                        if (!this.selectedCommentIds.Contains(commentId))
                                        {
                                            this.selectedCommentIds.Add(Convert.ToInt32(this.CommentsDataGridView[this.commentDataSet.GetComments.CommentIDColumn.ColumnName, e.RowIndex].Value));
                                        }
                                        this.RemoveButton.Enabled = true;
                                        if (this.commentGridAdminCount == this.commentsgridRowCount)
                                        {
                                            if (this.commentsgridRowCount == this.selectedCommentIds.Count)
                                            {
                                                this.SelectAllCheckBox.Checked = true;
                                            }
                                        }
                                        else
                                        {
                                            if (this.commentGridAdminCount == this.selectedCommentIds.Count)
                                            {
                                                this.SelectAllCheckBox.Checked = true;
                                            }

                                        }
                                    }
                                }
                            }

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
        /// Handles the Click event of the CloseCommentButton control.
        /// This Event Used to Close the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseCommentButton_Click(object sender, EventArgs e)
        {
            ////if (this.dataChanged || this.buttonOperation == (int)ButtonOperation.New)
            ////{
            ////    if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            ////    {
            ////       this.SetCommentCount(this.commentCount);
            ////        this.closeButton = true;
            ////        this.Close();
            ////   }
            ////    else
            ////    {
            ////        SetDataGridViewPosition(this.tempRowId);
            ////        this.SetFocus();
            ////    }
            ////}
            ////else
            ////{
            ////    this.SetCommentCount(this.commentCount);
            ////    this.closeButton = true;
            ////    this.Close();
            ////}

            //Changes in Comment Button color based on the below Sp Call

            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
            if (!string.IsNullOrEmpty(this.commentKeyID.ToString()))
            {
               CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.Form9075Control.WorkItem.GetCommentsCount(this.commentFrmId, this.commentKeyID, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    this.highPriorityFlag = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }
                
            
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the CommentPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentPanel_Click(object sender, EventArgs e)
        {
            if (this.CommentTextBox.Enabled)
            {
                this.CommentTextBox.Focus();
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the CommentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CommentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.FormPermissionFields.editPermission)
            {
                switch (e.KeyCode)
                {
                    case Keys.Tab:
                        {
                            break;
                        }

                    case Keys.Delete:
                        {
                            if (!this.dataChanged)
                            {
                                this.dataChanged = true;
                                if (this.buttonOperation != (int)ButtonOperation.New)
                                {
                                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                                    this.SetCommenButton(ButtonOperation.Update);
                                    this.buttonOperation = (int)ButtonOperation.Update;
                                    this.DisableCommentsGridSorting();
                                }
                            }

                            break;
                        }
                }
            }
        }

        /// <summary> 
        /// Handles the FormClosing event of the CommentsForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void CommentsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
            {
                if (e.CloseReason.Equals(CloseReason.UserClosing))
                {
                    if (this.dataChanged || this.buttonOperation == (int)ButtonOperation.New)
                    {
                        switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                {
                                    this.SaveCommentDetails();
                                    if (this.closingNow)
                                    {
                                        this.DialogResult = DialogResult.No;
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
                                    this.DialogResult = DialogResult.No;
                                    e.Cancel = false;
                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    e.Cancel = true;
                                    this.SetFocus();
                                    break;
                                }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the CommentsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CommentGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.commentCount > 0)
            {
                if (this.dataChanged)
                {
                    ////  this.SaveCommentButton.Focus();
                }
                else
                {
                    this.SetCommentTextBoxValues(e.RowIndex);
                    this.tempRowId = e.RowIndex;
                    this.tempColumnID = e.ColumnIndex;
                    this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;


                    if (this.commentCount <= e.RowIndex)
                    {
                        this.DisableCommentControl();
                        this.SetCommenButton(ButtonOperation.Empty);
                        this.DeleteCommentButton.Enabled = false;
                    }
                    else if (this.buttonOperation != (int)ButtonOperation.New)
                    {
                        ////this.EnableCommentControl();
                        this.SetCommenButton(ButtonOperation.Empty);
                    }

                    if (e.RowIndex >= 0 && this.commentDataSet.GetComments.Rows.Count != 0
                        && this.CommentsDataGridView.Rows[e.RowIndex].Cells["IsAdmin"].Value != null
                        && !string.IsNullOrEmpty(this.CommentsDataGridView.Rows[e.RowIndex].Cells["IsAdmin"].Value.ToString().Trim()))
                    {
                        //this.DeleteCommentButton.Enabled = false;
                        if (this.CommentsDataGridView.Rows[e.RowIndex].Cells["IsAdmin"].Value.ToString().Trim().ToUpper().Equals("FALSE"))
                        {
                            //this.CommentTextBox.Enabled = false;
                            this.CommentTextBox.SetFocusColor = System.Drawing.Color.White;
                            this.CommentTextBox.ForeColor = System.Drawing.Color.Gray;
                           // this.CommentTextBox.IsEditable = false;
                            this.CommentTextBox.ReadOnly = true;
                           // this.CommentTextBox.LockKeyPress = true;

                            this.CommentPublicCheckBox.Enabled = false;
                            this.CommentPrintCheckBox.Enabled = false;
                            this.willRollCheckBox.Enabled = false;
                            this.CommentsPriorityCombo.Enabled = false;
                            this.CommentTextBox.BackColor = System.Drawing.Color.White;
                            this.CommentsPriorityCombo.BackColor = System.Drawing.Color.White;

                            this.CommentDateTextBox.Enabled = false;
                            this.CommentDateTextBox.BackColor = System.Drawing.Color.White;
                            this.CommentFormIDTextBox.Enabled = false;
                            this.CommentFormIDTextBox.BackColor = System.Drawing.Color.White;
                            this.CommentUserTextBox.Enabled = false;
                            this.CommentUserTextBox.BackColor = System.Drawing.Color.White;

                            //this.SetCommenButton(ButtonOperation.Empty);
                            this.NewCommentButton.Enabled = true && this.NewCommentButton.ActualPermission;
                            this.SaveCommentButton.Enabled = false;
                            this.CancelCommentButton.Enabled = false;
                            this.DeleteCommentButton.Enabled = false;
                            this.CommentsDataGridView.Enabled = true;
                            this.SetCancelButton();
                        }
                        else
                        {
                            if (this.FormPermissionFields.editPermission)
                            {
                               // this.CommentTextBox.BackColor = System.Drawing.Color.White;
                                this.CommentTextBox.Enabled = true;
                                this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
                                this.CommentTextBox.ForeColor = System.Drawing.Color.Black;
                                this.CommentTextBox.LockKeyPress = false;
                                this.CommentPublicCheckBox.Enabled = true;
                                this.CommentPrintCheckBox.Enabled = true;
                                this.willRollCheckBox.Enabled = true;
                                this.CommentsPriorityCombo.Enabled = true;
                                this.CommentDateTextBox.Enabled = true;
                                this.CommentFormIDTextBox.Enabled = true;
                                this.CommentUserTextBox.Enabled = true;
                                this.SetCommenButton(ButtonOperation.Empty);
                            }
                            //this.DeleteCommentButton.Enabled = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the CommentsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CommentGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.dataChanged)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            this.GridCancel(e);

                            break;
                        }

                    case Keys.Up:
                        {
                            this.GridCancel(e);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// gridCancel
        /// </summary>
        /// <param name="e">The Instance of event.</param>
        private void GridCancel(KeyEventArgs e)
        {
            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        this.SaveCommentDetails();
                        if (this.closingNow)
                        {
                            this.SetCommentTextBoxValues(this.tempRowId);
                            ////this.SetDataGridViewPosition(this.tempRowId, this.tempColumnID); 
                            e.Handled = false;
                            this.dataChanged = false;
                            this.EnableCommentsGridSorting();
                        }
                        else
                        {
                            e.Handled = true;
                        }

                        break;
                    }

                case DialogResult.No:
                    {
                        ////this.SetDataGridViewPosition(this.tempRowId, this.tempColumnID); 
                        this.SetCommentTextBoxValues(this.tempRowId);
                        e.Handled = false;
                        this.dataChanged = false;
                        this.EnableCommentsGridSorting();
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        e.Handled = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the CommentsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void CommentGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.CommentsDataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the CommentPublicCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentPublicCheckBox_Click(object sender, EventArgs e)
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableCommentsGridSorting();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CommentPrintCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentPrintCheckBox_Click(object sender, EventArgs e)
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableCommentsGridSorting();
                }
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
        }

        /// <summary>
        /// CommentsPriorityCombo SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CommentsPriorityCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.SetCommentUpdateMode();
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        /// <summary>
        /// Handles the LinkClicked event of the CommentLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void CommentLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.commentID > 0)
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.commentID;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////Hashtable commentDetails = new Hashtable();
                ////commentDetails.Add("TableName", "tTs_Comment");
                ////commentDetails.Add("KeyFieldName", "CommentID");
                ////commentDetails.Add("CommentID", this.commentID);

                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(90131, TerraScan.Common.Reports.Report.ReportType.Preview, commentDetails);
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
        /// Comments DataGridView ColumnSortModeChanged
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Instance of the event</param>
        private void CommentsDataGridView_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        /// <summary>
        /// CommentsData GridView Sorted
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The Instance of the event</param> 
        private void CommentsDataGridView_Sorted(object sender, EventArgs e)
        {
             //DataGridViewColumnHeaderCell headerCell=CommentsDataGridView.Columns[2].HeaderCell;
             //if (headerCell.SortGlyphDirection != SortOrder.Ascending)
             //{
             //    //string val=CommentsDataGridView.SortOrder.ToString();
             //    //CommentsDataGridView.Columns[2].HeaderText.ToString();
             //    CommentsDataGridView.Columns["CommentDate"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
             //}
             //else
             //{
             //    //CommentsDataGridView.Columns["Date"].DefaultCellStyle.Format = "d";
             //    CommentsDataGridView.Columns[2].HeaderCell.SortGlyphDirection = SortOrder.Descending;
             //}

            //if (CommentsDataGridView.SortOrder = SortOrder.Ascending)
            //{
            //    isSorted = true;
            //    sortedColumn = CommentsDataGridView.SortedColumn.ToString();
            //    sortedOrder = CommentsDataGridView.SortOrder.ToString();
            //}
            //else
            //{
            //    isSorted = false;
            //    sortedColumn = CommentsDataGridView.SortedColumn.ToString();
            //    sortedOrder = CommentsDataGridView.SortOrder.ToString();
            //}
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CommentPrintCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentPrintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CommentPublicCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentPublicCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
            }
        }

        /// <summary>
        /// Handles the 1 event of the CommentPublicCheckBox_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentPublicCheckBox_Click_1(object sender, EventArgs e)
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableCommentsGridSorting();
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the willRollCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void willRollCheckBox_Click(object sender, EventArgs e)
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableCommentsGridSorting();
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the willRollCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void willRollCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CommentsPriorityCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentsPriorityCombo_Click(object sender, EventArgs e)
        {
            // this.SetCommentUpdateMode();
        }

        /// <summary>
        /// Handles the Load event of the Comments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Comments_Load(object sender, EventArgs e)
        {
            this.NewMenu.Click += new EventHandler(this.CommentNewButton_Click);
            this.SaveMenu.Click += new EventHandler(this.SaveCommentButton_Click);

            this.panel4.Focus();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            CommonData connectionString = new CommonData();

            connectionString = this.Form9075Control.WorkItem.GetConnectionString(1);
        }

        /// <summary>
        /// Handles the Click event of the TemplateCommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TemplateCommentButton_Click(object sender, EventArgs e)
        {
            Form newCommentTemplateForm = new Form();
            object[] optionalParameter = null;
            int templateId;
            //// Constructing Optional Parameter
            int.TryParse(this.CommentFromTemplateComboBox.SelectedValue.ToString(), out templateId);
            string templateName = this.CommentFromTemplateComboBox.Text;
            int formid = this.commentFrmId;
            string comments = this.CommentTextBox.Text;
            string priority = this.CommentsPriorityCombo.Text;
            int priorityId = 0;
            if (this.CommentsPriorityCombo.SelectedIndex >= 0)
            {
                int.TryParse(this.CommentsPriorityCombo.SelectedValue.ToString(), out priorityId);
            }

            bool publiccheckbox;
            bool print;
            bool willRoll;
            if (this.CommentPublicCheckBox.Checked.Equals(true))
            {
                publiccheckbox = true;
            }
            else
            {
                publiccheckbox = false;
            }

            if (this.CommentPrintCheckBox.Checked.Equals(true))
            {
                print = true;
            }
            else
            {
                print = false;
            }

            if (this.willRollCheckBox.Checked.Equals(true))
            {
                willRoll = true;
            }
            else
            {
                willRoll = false;
            }

            optionalParameter = new object[] { templateId, templateName, print, priority, comments, willRoll, publiccheckbox, formid, priorityId };

            newCommentTemplateForm = this.form9075Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9076, optionalParameter, this.form9075Control.WorkItem);
            if (newCommentTemplateForm != null)
            {
                DialogResult dr = newCommentTemplateForm.ShowDialog();
                if (!dr.Equals(DialogResult.Cancel))
                {
                    this.FromTemplateList();
                }
            }
        }

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message"></see>, passed by reference, that represents the Win32 message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"></see> values that represents the key to process.</param>
        /// <returns>
        /// true if the keystroke was processed and consumed by the control; otherwise, false to allow further processing.
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                if ((keyData.Equals(Keys.Control | Keys.T)))
                {
                    ////Code
                    if (this.CommentFromTemplateComboBox.Items.Count > 1)
                    {
                        if (this.dataChanged == false)
                        {
                            this.ControlFuncation();
                            if (this.CommentFromTemplateComboBox.Items.Count > 1)
                            {
                                this.textChangedInTextBox = true;
                                if (this.CommentFromTemplateComboBox.SelectedIndex == (this.CommentFromTemplateComboBox.Items.Count - 1))
                                {
                                    int i = 1;
                                    this.CommentFromTemplateComboBox.SelectedIndex = i;
                                }
                                else
                                {
                                    this.CommentFromTemplateComboBox.SelectedIndex += 1;
                                }

                                int operation = 0;
                                int.TryParse(this.CommentFromTemplateComboBox.SelectedValue.ToString(), out operation);
                                this.Clearvalues();
                                this.getCommentTemplateData = this.Form9075Control.WorkItem.F9076_getTemplate(operation);

                                if (this.getCommentTemplateData.GetCommentTemplate.Rows.Count > 0)
                                {
                                    this.CommentTextBox.Text = this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentColumn.ColumnName].ToString();
                                    this.CommentPublicCheckBox.Checked = Convert.ToBoolean(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.IsPublicColumn.ColumnName].ToString());
                                    this.willRollCheckBox.Checked = Convert.ToBoolean(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.WillRollColumn.ColumnName].ToString());
                                    this.CommentPrintCheckBox.Checked = Convert.ToBoolean(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.WillPrintColumn.ColumnName].ToString());
                                    string priorityCombo = this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.IsHighPriorityColumn.ColumnName].ToString();
                                    //if (priorityCombo.ToLower().Equals("true"))
                                    //{
                                    //    this.CommentsPriorityCombo.SelectedIndex = 1;
                                    //}
                                    //else
                                    //{
                                    //    this.CommentsPriorityCombo.SelectedIndex = 0;
                                    //}

                                    if (this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName] != null
                                        && !string.IsNullOrEmpty(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName].ToString()))
                                    {
                                        int commentPriority = 0;
                                        int.TryParse(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName].ToString(), out commentPriority);
                                        this.CommentsPriorityCombo.SelectedValue = commentPriority;
                                    }
                                    else
                                    {
                                        this.CommentsPriorityCombo.SelectedIndex = -1;
                                    }
                                }
                            }
                        }

                        this.textChangedInTextBox = false;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the CommentFromTemplateComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommentFromTemplateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CommentFromTemplateComboBox.SelectedIndex > 0)
            {
                if (this.textChangedInTextBox.Equals(false))
                {
                    if (this.dataChanged == false)
                    {
                        if ((!this.isonLoad) && (!this.flag))
                        {
                            this.ControlFuncation();
                        }

                        int tempId = 0;
                        int.TryParse(this.CommentFromTemplateComboBox.SelectedValue.ToString(), out tempId);
                        this.Clearvalues();
                        this.getCommentTemplateData = this.Form9075Control.WorkItem.F9076_getTemplate(tempId);

                        if (this.getCommentTemplateData.GetCommentTemplate.Rows.Count > 0)
                        {
                            this.CommentTextBox.Text = this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentColumn.ColumnName].ToString();
                            this.CommentPublicCheckBox.Checked = Convert.ToBoolean(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.IsPublicColumn.ColumnName].ToString());
                            this.willRollCheckBox.Checked = Convert.ToBoolean(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.WillRollColumn.ColumnName].ToString());
                            this.CommentPrintCheckBox.Checked = Convert.ToBoolean(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.WillPrintColumn.ColumnName].ToString());
                            string priorityCombo = this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.IsHighPriorityColumn.ColumnName].ToString();
                            //if (priorityCombo.ToLower().Equals("true"))
                            //{
                            //    this.CommentsPriorityCombo.SelectedIndex = 1;
                            //}
                            //else
                            //{
                            //    this.CommentsPriorityCombo.SelectedIndex = 0;
                            //}

                            if (this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName] != null
                                && !string.IsNullOrEmpty(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName].ToString()))
                            {
                                int commentPriority = 0;
                                int.TryParse(this.getCommentTemplateData.GetCommentTemplate.Rows[0][this.getCommentTemplateData.GetCommentTemplate.CommentPriorityIDColumn.ColumnName].ToString(), out commentPriority);
                                this.CommentsPriorityCombo.SelectedValue = commentPriority;
                            }
                            else
                            {
                                this.CommentsPriorityCombo.SelectedIndex = -1;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Controls the funcation.
        /// </summary>
        private void ControlFuncation()
        {
            this.CommentNewButton_Click(null, null);
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    ////Commented by Biju on 02/Dec/2009 to fix #5022
                    ////HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                    ////Added by Biju on 02/Dec/2009 to fix #5022
                    HelpEngine.Show(this.AccessibleName, "9075");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private int GetRowIndex()
        {
            if (this.commentDataSet.GetComments.Rows.Count > 0)
            {
                if (this.CommentsDataGridView.SelectedRows.Count > 0)
                {
                    this.selected = this.CommentsDataGridView.SelectedRows[0].Index;
                }
                else if (this.CommentsDataGridView.SelectedCells.Count > 0)
                {
                    this.selected = this.CommentsDataGridView.CurrentCell.RowIndex;
                }
            }

            return this.selected;
        }

        /*  /// <summary>
        /// Saves the records. when the user close the close button
        /// </summary>
        private void SaveRecords()
        {
            if (this.RequiredField() && this.validRecID)
            {
                try
                {
                    //// WSHelper.SaveCommentsTerraScanCommon.UserId, TerraScan.Utilities.Utility.GetXmlString(this.commentDataSet.Tables[0]));
                    ////this.SetCommentCount(this.commentDataSet.GetComments.Rows.Count);
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("UnableSave"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } */

        /// <summary>
        /// This Methods Validate all Required Fields are Selected or Not
        /// </summary>
        /// <returns>
        /// True if all Are Required Fields filled else False
        /// </returns>
        private bool RequiredField()
        {
            if (this.CommentDateTextBox.Text.Trim().Length > 0 && this.CommentTextBox.Text.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the data row empty.
        /// </summary>
        /// <returns> retrun false if has empty row or it  return true </returns>
        private bool CheckDataRowEmpty()
        {
            if (this.validDataSet)
            {
                DataRowCollection dataRowCollection = this.commentDataSet.GetComments.Rows;
                foreach (DataRow tempDataRow in dataRowCollection)
                {
                    if (String.IsNullOrEmpty(tempDataRow["Comment"].ToString()))
                    {
                        this.validData = false;
                        break;
                    }
                    else
                    {
                        this.validData = true;
                    }
                }
            }

            return this.validData;
        }

        /// <summary>
        /// Disables the comment control.
        /// </summary>
        private void DisableCommentControl()
        {
            //this.CommentsPriorityCombo.SelectedIndex = 0;
            this.CommentsPriorityCombo.SelectedIndex = -1;
            //this.CommentTextBox.Enabled = false;
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.White;
            this.CommentTextBox.ForeColor = System.Drawing.Color.Gray;
           // this.CommentTextBox.IsEditable = false;
            this.CommentTextBox.ReadOnly = true;
           // this.CommentTextBox.LockKeyPress = true;

            this.CommentPublicCheckBox.Enabled = false;
            this.CommentPrintCheckBox.Enabled = false;
            this.willRollCheckBox.Enabled = false;
            this.CommentsPriorityCombo.Enabled = false;
            this.CommentTextBox.BackColor = System.Drawing.Color.White;
            this.CommentsPriorityCombo.BackColor = System.Drawing.Color.White;

            this.CommentDateTextBox.Enabled = false;
            this.CommentDateTextBox.BackColor = System.Drawing.Color.White;
            this.CommentFormIDTextBox.Enabled = false;
            this.CommentFormIDTextBox.BackColor = System.Drawing.Color.White;
            this.CommentUserTextBox.Enabled = false;
            this.CommentUserTextBox.BackColor = System.Drawing.Color.White;
            ////this.CommentKeyIDTextBox.Enabled = false;
            ////this.CommentKeyIDTextBox.BackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// Enables the comment control.
        /// </summary>
        private void EnableCommentControl()
        {
           // this.CommentTextBox.BackColor = System.Drawing.Color.White;
            this.CommentTextBox.Enabled = true;
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CommentTextBox.ForeColor = System.Drawing.Color.Black;
            this.CommentPublicCheckBox.Enabled = true;
            this.CommentPrintCheckBox.Enabled = true;
            this.willRollCheckBox.Enabled = true;
            this.CommentsPriorityCombo.Enabled = true;
            this.CommentDateTextBox.Enabled = true;
            this.CommentFormIDTextBox.Enabled = true;
            this.CommentUserTextBox.Enabled = true;
            this.panel4.Focus();
            ////this.CommentKeyIDTextBox.Enabled = true;
        }

        /// <summary>
        /// This Methods Initialize Loads The  CommentsDataGridView For Particular User 
        /// </summary>
        private void LoadCommentDataGridView()
        {
            try
            {
                this.RemoveButton.Enabled = false;
                this.SelectAllCheckBox.Checked = false;
                selectedCommentIds = new List<int>();
                this.Cursor = Cursors.WaitCursor;

                this.commentDataSet = this.Form9075Control.WorkItem.GetComments(this.commentKeyID, this.commentFrmId, TerraScanCommon.UserId);
                commentsgridRowCount = this.commentDataSet.GetComments.Rows.Count;

                commentGridAdminCount = commentDataSet.GetComments.Select("IsAdmin = 'True'").Length;
                
              
                //Removed the Comment Color status based on the GetCommentSCount Stored Procedure during close
                //instead of CommentPriority Id.

                //DataView tempDataView = new DataView(this.commentDataSet.GetComments);
                //tempDataView.RowFilter = string.Concat(this.commentDataSet.GetComments.CommentPriorityIDColumn.ColumnName, " > 1");
                //string firstFilter = string.Concat(this.commentDataSet.GetComments.IsHighPriorityColumn.ColumnName, "= 'HIGH'");
                //string secondfilter = string.Concat(this.commentDataSet.GetComments.IsHighPriorityColumn.ColumnName, "= 'CRITICAL'");
                //tempDataView.RowFilter = firstFilter + " OR " + secondfilter; 
                //if (tempDataView.Count > 0)
                //{
                //    this.highPriorityFlag = true;
                //}
                //else
                //{
                //    this.highPriorityFlag = false;
                //}

                //// Commented Because Mandatory Filed Public
                /*  this.configDataSet = WSHelper.GetConfigDetails("CommentsPublic");
                  if (this.configDataSet != null)
                  {
                      if (this.configDataSet.Tables.Count > 0 && this.configDataSet.Tables[0].Rows.Count > 0)
                      {
                          commentsPublic = Convert.ToBoolean(configDataSet.Tables[0].Rows[0]["ConfigurationValue"].ToString());
                      }
                      else
                      {
                          commentsPublic = Convert.ToBoolean(configDataSet.Tables[0].Rows[0]["ConfigurationValue"].ToString());
                      }
                  }*/
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }

            if (this.CheckValidDataSet(this.commentDataSet))
            {
                this.ClearDataBinding();
                this.CustomizeDataGrid();
                this.commentCount = this.commentDataSet.GetComments.Rows.Count;
                this.commentDataSet.GetComments.CommentColumn.ToString().Replace("\n", "\r\n");
                this.CommentsDataGridView.DataSource = this.commentDataSet.GetComments.DefaultView;
                if (this.CommentsDataGridView.Rows.Count > 5)
                {
                    this.CommentsVerticalScrollBar.Visible = false;
                }
                else
                {
                    this.CommentsVerticalScrollBar.Visible = true;
                }

                if (this.commentCount == 0)
                {
                    //// if Count is 0  then disable
                    this.DisableCommentControl();
                    this.CommentLinkLabel.Enabled = false;
                }
                else
                {
                    this.SetDataGridViewPosition(this.currentRow);
                    this.CommentLinkLabel.Enabled = true;
                    this.EnableCommentControl();
                }
                int setReadVar = 0;
                if (this.commentsgridRowCount > 0)
                {
                    for (int count = 0; count < this.commentsgridRowCount; count++)
                    {
                        if (string.Concat(this.CommentsDataGridView.Rows[count].Cells[SharedFunctions.GetResourceString("IsAdmin")].Value) == "False")
                        {
                            this.CommentsDataGridView.Rows[count].Cells[SharedFunctions.GetResourceString("IsValid")].ReadOnly = true;
                            setReadVar = setReadVar + 1;
                        }
                    }
                }


                if (setReadVar == this.commentsgridRowCount)
                {
                    this.SelectAllCheckBox.Enabled=false;
                }
                else
                {
                    this.SelectAllCheckBox.Enabled = true;
                }
            }
            else
            {
                this.CustomizeDataGrid();
            }

            if (this.commentCount == 0)
            {
                this.CommentLinkLabel.Enabled = false;
            }

            this.SetCommenButton(ButtonOperation.Empty);
            this.Cursor = Cursors.Default;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Determines whether  DataTable is Exist or not in  [the specified CMNT data set].
        /// 
        /// </summary>
        /// <param name="cmntDataSet">The CMNT data set.</param>
        /// <returns>
        /// 	<c>true</c> if [is data table] [the specified CMNT data set]; otherwise, <c>false</c>.
        /// </returns>
        private bool CheckValidDataSet(DataSet cmntDataSet)
        {
            if (cmntDataSet.Tables.Count > 0 && cmntDataSet != null)
            {
                this.validDataSet = true;
                return true;
            }
            else
            {
                this.validDataSet = false;
                return false;
            }
        }

        /// <summary>
        /// This Method used to  Allign The Header width , Column width , And Font Size for 
        /// CommentsDataGridView
        /// </summary>
        private void CustomizeDataGrid()
        {
            ////this.commentHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            ////this.commentDefaultCell.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            ////this.CommentsDataGridView.EnableHeadersVisualStyles = false;
            ////this.CommentsDataGridView.MultiSelect = false;
            DataGridViewColumnCollection columns = this.CommentsDataGridView.Columns;
            //////this.CommentsDataGridView.Columns["Comment"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;            
            ////this.CommentsDataGridView.Columns["Comment"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            columns["IsValid"].DataPropertyName = "IsValid";
            columns["CommentDate"].DataPropertyName = "CommentDate";
            columns["Comment"].DataPropertyName = "Comment";
            columns["UserName"].DataPropertyName = "UserName";
            columns["IsPublicComment"].DataPropertyName = "IsPublic";
            columns["IsPrintOnReceipt"].DataPropertyName = "IsPrint";
            columns["KeyID"].DataPropertyName = "KeyID";
            columns["FormID"].DataPropertyName = "Form";
            columns["IsPriority"].DataPropertyName = "IsHighPriority";
            columns["CommentId"].DataPropertyName = "CommentId";
            columns["IsRoll"].DataPropertyName = "IsRoll";
            columns["CommentPriorityID"].DataPropertyName = "CommentPriorityID";
            columns["IsAdmin"].DataPropertyName = "IsAdmin";
            columns["IsValid"].DisplayIndex = 0;
            columns["CommentDate"].DisplayIndex = 1;
            columns["Comment"].DisplayIndex = 2;
            columns["UserName"].DisplayIndex = 3;
            columns["IsPublicComment"].DisplayIndex = 4;
            columns["IsPrintOnReceipt"].DisplayIndex = 5;
            columns["IsPriority"].DisplayIndex = 6;
             columns["CommentId"].DisplayIndex = 7;
             columns["IsValid"].Width = 29;
            columns["CommentDate"].Width = 65;
            columns["Comment"].Width = 355;
            columns["UserName"].Width = 78;
            columns["IsPublicComment"].Width = 63;
            columns["IsPrintOnReceipt"].Width = 60;
            columns["IsPriority"].Width = 55;
            columns["KeyID"].Width = 0;
            columns["KeyID"].Visible = false;
            this.CommentsDataGridView.PrimaryKeyColumnName = "CommentId";
            this.CommentsDataGridView.Columns[SharedFunctions.GetResourceString("IsValid")].ReadOnly = true;
            
            //bool test= this.CommentsDataGridView.Rows[this.commentDataSet.GetComments.CommentIDColumn.ColumnName.].Cells[e.ColumnIndex].Value
            //// this.CommentsDataGridView.DefaultCellStyle = this.commentDefaultCell;
            //// this.commentHeader.Alignment = DataGridViewContentAlignment.MiddleCenter;  
            //// this.CommentsDataGridView.ColumnHeadersDefaultCellStyle = this.commentHeader;
        }

        ///// <summary>
        ///// Sets the data grid view position.
        ///// </summary>
        ///// <param name="firstRow">The first row.</param>
        //// private void SetDataGridViewPosition(int firstRow)
        //// {
        ////     this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
        ////     if (this.CommentsDataGridView.Rows.Count > 0 && commentRow >= 0)
        ////     {
        ////         this.CommentsDataGridView.Rows[Convert.ToInt32(firstRow)].Selected = true;
        ////         this.CommentsDataGridView.CurrentCell = this.CommentsDataGridView[0, Convert.ToInt32(firstRow)];
        ////     }
        //// }
        

        /// <summary>
        /// Calculates the selected Comments for TSCO - D9075.F9075 Comments Form - New "Remove' button .
        /// </summary>
        private void CalculateSelectAllComments(bool isChecked)
        {
            try
            {
                this.CommentsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.commentsgridRowCount; count++)
                {
                    if (isChecked == true)
                    {
                        if (string.Concat(this.CommentsDataGridView.Rows[count].Cells[SharedFunctions.GetResourceString("IsAdmin")].Value) == "True")
                        {
                            this.selectedCommentIds.Add(Convert.ToInt32(this.CommentsDataGridView[this.commentDataSet.GetComments.CommentIDColumn.ColumnName, count].Value));
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
        /// Calculates the Unselected Comments for TSCO - D9075.F9075 Comments Form - New "Remove' button.
        /// </summary>
        private void CalculateUnSelectComments(bool isChecked)
        {
            try
            {
                this.CommentsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.commentsgridRowCount; count++)
                {
                    if (isChecked == false)
                    {
                        if (string.Concat(this.CommentsDataGridView.Rows[count].Cells[SharedFunctions.GetResourceString("IsAdmin")].Value) == "True")
                        {
                            this.selectedCommentIds.Remove(Convert.ToInt32(this.CommentsDataGridView[this.commentDataSet.GetComments.CommentIDColumn.ColumnName, count].Value));

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
        /// Gets the selected Comments ids to XML.
        /// </summary>
        private void GetSelectedCommentIdsXml()
        {
            this.selectedCommentsIdsXml = string.Empty;
            DataTable tempXMLdataTable = new DataTable();
            foreach (DataColumn column in this.commentDataSet.GetComments.Columns)
            {
                if (column.ColumnName == this.commentDataSet.GetComments.CommentIDColumn.ColumnName)
                {
                    tempXMLdataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            for (int item = 0; item < this.selectedCommentIds.Count; item++)
            {
                DataRow tempXMLDataRow = tempXMLdataTable.NewRow();
                tempXMLDataRow[this.commentDataSet.GetComments.CommentIDColumn.ColumnName] = this.selectedCommentIds[item].ToString();
                tempXMLdataTable.Rows.Add(tempXMLDataRow);
            }

            this.selectedCommentsIdsXml = TerraScanCommon.GetXmlString(tempXMLdataTable);
        }

        /// <summary>
        /// Selects the un select all and Unselect all for 163 sprint.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SelectUnSelectAll(string status)
        {
            if (this.commentsgridRowCount > 0)
            {
                for (int count = 0; count < this.commentsgridRowCount; count++)
                {
                    if (string.Concat(this.CommentsDataGridView.Rows[count].Cells[SharedFunctions.GetResourceString("IsAdmin")].Value) == "True")
                    {
                        this.CommentsDataGridView.Rows[count].Cells[SharedFunctions.GetResourceString("IsValid")].Value = status;
                    }
                }
            }

        }

        /// <summary>
        /// Set the Readonly all check box column.
        /// </summary>
        /// <param name="status">The status.</param>
        private void ReadOnlyAll(string status)
        {
            if (this.commentsgridRowCount > 0)
            {
                for (int count = 0; count < this.commentsgridRowCount; count++)
                {
                    this.CommentsDataGridView.Rows[count].Cells["IsValid"].Value = status;
                }
            }

        }

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="commentRow">The current row.</param>
        /// <param name="columnIndex">The Column Index.</param>
        private void SetDataGridViewPosition(int commentRow, int columnIndex)
        {
            try
            {
                this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                if (this.CommentsDataGridView.Rows.Count > 0 && commentRow >= 0)
                {
                    //// this.CommentsDataGridView.Rows[Convert.ToInt32(commentRow)].Selected = true;
                    this.CommentsDataGridView.CurrentCell = this.CommentsDataGridView[columnIndex, commentRow];
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="commentRow">The current row.</param>
        private void SetDataGridViewPosition(int commentRow)
        {
            try
            {
                this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                if (this.CommentsDataGridView.Rows.Count > 0 && commentRow >= 0)
                {
                    this.CommentsDataGridView.Rows[Convert.ToInt32(commentRow)].Selected = true;
                    this.CommentsDataGridView.CurrentCell = this.CommentsDataGridView[3, commentRow];
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Clears the data binding.
        /// </summary>
        private void ClearDataBinding()
        {
            this.CommentUserTextBox.DataBindings.Clear();
            this.CommentFormIDTextBox.DataBindings.Clear();
            this.CommentDateTextBox.DataBindings.Clear();
            ////this.CommentKeyIDTextBox.DataBindings.Clear();
            //// this.CommentIDTextBox.DataBindings.Clear();
            this.CommentTextBox.DataBindings.Clear();
            this.CommentPublicCheckBox.DataBindings.Clear();
            this.CommentPrintCheckBox.DataBindings.Clear();
            this.willRollCheckBox.DataBindings.Clear();
        }

        /* /// <summary>
        /// Fills the data set.
        /// </summary>
        private void FillDataSet()
        {
            this.commentDataRow["FormID"] = this.commentFrmId;
            this.commentDataRow["KeyID"] = this.commentKeyID;
            this.commentDataRow["CommentDate"] = Convert.ToDateTime(this.CommentDateTextBox.Text.ToString());
            this.commentDataRow["Comment"] = this.CommentTextBox.Text.Trim();
            this.commentDataRow["isPublicComment"] = this.CommentPublicCheckBox.Checked;
            this.commentDataRow["isPrintOnReceipt"] = this.CommentPrintCheckBox.Checked;
            this.commentDataRow["UserName"] = TerraScanCommon.UserName;
            ////            this.commentDataSet.Tables[0].Rows.Add(this.commentDataRow);
            this.currentRow = Convert.ToInt32(this.commentDataSet.GetComments.Rows.Count.ToString()) - 1;
        } */

        /// <summary>
        /// Clears the text boxes.
        /// </summary>
        private void ClearTextBoxes()
        {
            this.CommentDateTextBox.Text = string.Empty;
            /////this.CommentIDTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;
            this.CommentPublicCheckBox.Checked = false;
            this.CommentPrintCheckBox.Checked = false;
            this.willRollCheckBox.Checked = false;
            this.CommentUserTextBox.Text = string.Empty;
            this.CommentFormIDTextBox.Text = string.Empty;
            ////this.CommentKeyIDTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Sets the focusc to the  comment textbox.
        /// </summary>
        private void SetFocus()
        {
            ////this.CommentTextBox.Focus();            
            ////this.CommentTextBox.SelectAll();
            this.panel1.Focus();
            this.CommentsPriorityCombo.Focus();
        }

        /// <summary>
        /// Initializes the button.
        /// </summary>
        private void InitializeButton()
        {
            this.NewCommentButton.Enabled = true && this.NewCommentButton.ActualPermission;
            this.SaveCommentButton.Enabled = false;
            if (this.validDataSet)
            {
                if (this.commentDataSet.GetComments.Rows.Count > 0)
                {
                    if (!TerraScanCommon.IsFieldUser)
                    {
                        this.DeleteCommentButton.Enabled = true;
                    }
                    else
                    {
                        this.DeleteCommentButton.Enabled = false;
                    }

                }
                else
                {
                    this.DeleteCommentButton.Enabled = false;
                }
            }
            else
            {
                this.DeleteCommentButton.Enabled = false;
            }

            this.CancelCommentButton.Enabled = false;
        }

        /// <summary>
        /// Assing the values to the textboxes
        /// </summary>
        private void FillFormID()
        {
            this.CommentUserTextBox.Text = TerraScanCommon.UserName;
            this.CommentFormIDTextBox.Text = this.commentFrmId.ToString();

            ////if (this.commentKeyID > 0)
            ////{
            ////    this.CommentKeyIDTextBox.Text = this.commentKeyID.ToString();
            ////}
            ////else
            ////{
            ////    this.CommentKeyIDTextBox.Text = string.Empty;
            ////}
        }

        /* /// <summary>
        /// Set The number Comments available for the current USer
        /// </summary>
        /// <param name="commentCount">return number of comments</param>
        private void SetCommentCount(int commentCount)
        {
            //// if FormID is 1000 then  its for Receipt
            ////if ((this.commentFrmId == 1000) && (commentCount > 0))
            ////{
            ////    ((ReceiptEngineUserControl)((Panel)((Panel)this.Owner.ActiveMdiChild.Controls["ContentPanel"]).Controls["ReceiptEngineControlPanel"]).Controls["ReceiptEngineUserControl"]).CommentsButtonControl.Text = "Comment(" + commentCount.ToString() + ")";
            ////}
            ////else if (this.commentFrmId == 1000)
            ////{
            ////    ((ReceiptEngineUserControl)((Panel)((Panel)this.Owner.ActiveMdiChild.Controls["ContentPanel"]).Controls["ReceiptEngineControlPanel"]).Controls["ReceiptEngineUserControl"]).CommentsButtonControl.Text = "Comment ";
            ////}

            ////Type formType = TerraScanCommon.mdiparent.GetType();
            ////MethodInfo methodInfo = formType.GetMethod("SetCommentCountFromChild");
            ////methodInfo.Invoke(this, new object[] { 1000, this.commentKeyID }); 
        } */

        /////// <summary>
        /////// Sets the enable controls.
        /////// </summary>
        /////// <param name="columnIndex">Index of the column.</param>
        ////private void SetEnableControls(int columnIndex)
        ////{
        ////    this.EnableCommentControl();
        ////    this.SetFocus();
        ////}

        /// <summary>
        /// Sets the group button.
        /// </summary>
        /// <param name="commentButton">The comment button.</param>
        private void SetCommenButton(ButtonOperation commentButton)
        {
            switch (commentButton)
            {
                case ButtonOperation.New:
                    {
                        //// this.NewCommentButton.Enabled = false;
                        //// this.SaveCommentButton.Enabled = true;
                        //// this.CancelCommentButton.Enabled = true;
                        //// this.DeleteCommentButton.Enabled = false;

                        this.CommentsDataGridView.Enabled = false;
                        this.SetCancelButton();
                        this.RemoveButton.Enabled = false;
                        //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                        this.SelectAllCheckBox.Checked = false;
                        this.SelectAllCheckBox.Enabled = false;
                        this.selectedCommentIds = new List<int>();
                        this.ReadOnlyAll("false");
                        //end
                        break;
                    }

                case ButtonOperation.Save:
                    {
                        //// this.NewCommentButton.Enabled = true && this.NewCommentButton.ActualPermission;
                        //// this.SaveCommentButton.Enabled = false;
                        //// this.CancelCommentButton.Enabled = false;

                        this.CommentsDataGridView.Enabled = true;
                        if (this.commentCount > 0)
                        {
                            if (!TerraScanCommon.IsFieldUser)
                            {
                                this.DeleteCommentButton.Enabled = true && this.DeleteCommentButton.ActualPermission;
                            }
                            else
                            {
                                this.DeleteCommentButton.Enabled = false; 
                            }
                        }
                        else
                        {
                            this.DeleteCommentButton.Enabled = false;
                        }

                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        //// this.NewCommentButton.Enabled = true;
                        //// this.SaveCommentButton.Enabled = false;
                        //// this.CancelCommentButton.Enabled = false;

                        this.CommentsDataGridView.Enabled = true;
                        if (this.commentCount > 0)
                        {
                            if (!TerraScanCommon.IsFieldUser)
                            {
                                this.DeleteCommentButton.Enabled = true && this.DeleteCommentButton.ActualPermission;
                            }
                            else
                            {
                                this.DeleteCommentButton.Enabled = false; 
                            }
                        }
                        else
                        {
                            this.DeleteCommentButton.Enabled = false;
                        }

                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.Update:
                    {
                        this.NewCommentButton.Enabled = false;
                        this.CancelCommentButton.Enabled = true;
                        this.CommentsDataGridView.Enabled = true;
                        this.DeleteCommentButton.Enabled = false;
                        this.SetCancelButton();
                        this.RemoveButton.Enabled = false;
                        //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                        this.SelectAllCheckBox.Checked = false;
                        this.SelectAllCheckBox.Enabled = false;
                        this.selectedCommentIds = new List<int>();
                        this.ReadOnlyAll("false");
                        //end
                        break;
                    }

                case ButtonOperation.Empty:
                    {
                        this.NewCommentButton.Enabled = true && this.NewCommentButton.ActualPermission;
                        this.SaveCommentButton.Enabled = false;
                        this.CancelCommentButton.Enabled = false;
                        if (this.commentCount > 0)
                        {
                            if (!TerraScanCommon.IsFieldUser)
                            {
                                this.DeleteCommentButton.Enabled = true && this.DeleteCommentButton.ActualPermission;
                            }
                            else
                            {
                                this.DeleteCommentButton.Enabled = false; 
                            }
                            this.CommentsDataGridView.Enabled = true;
                        }
                        else
                        {
                            this.DeleteCommentButton.Enabled = false;
                            this.CommentsDataGridView.Enabled = false;
                        }

                        this.SetCancelButton();
                        break;
                    }
            }
        }

        /// <summary>
        /// SetComment UpdateMode
        /// </summary>
        private void SetCommentUpdateMode()
        {
            if (!this.dataChanged)
            {
                this.dataChanged = true;
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCommenButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableCommentsGridSorting();
                    this.RemoveButton.Enabled = false;
                    //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                    this.SelectAllCheckBox.Checked = false;
                    this.SelectAllCheckBox.Enabled = false;
                    this.selectedCommentIds = new List<int>();
                    this.ReadOnlyAll("false");
                }
            }
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void DisableCommentsGridSorting()
        {
            DataGridViewColumnCollection disableSortColumn = this.CommentsDataGridView.Columns;
            disableSortColumn["CommentDate"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableSortColumn["UserName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableSortColumn["Comment"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void EnableCommentsGridSorting()
        {
            DataGridViewColumnCollection enableSortColumn = this.CommentsDataGridView.Columns;
            enableSortColumn["CommentDate"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["UserName"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["Comment"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            if (this.CancelCommentButton.Enabled == false)
            {
                this.CancelButton = this.CloseCommentButton;
            }
            else
            {
                this.CancelButton = this.CancelCommentButton;
            }
        }

        /// <summary>
        /// Froms the template list.
        /// </summary>
        private void FromTemplateList()
        {
            this.listTemplateDate = this.form9075Control.WorkItem.F9075_ListTemplate(this.commentFrmId, TerraScanCommon.UserId);
            if (this.listTemplateDate.TemplateList.Rows.Count > 0)
            {
                this.CommentFromTemplateComboBox.DataSource = this.listTemplateDate.TemplateList;
                this.CommentFromTemplateComboBox.DisplayMember = this.listTemplateDate.TemplateList.TemplateColumn.ColumnName;
                this.CommentFromTemplateComboBox.ValueMember = this.listTemplateDate.TemplateList.TemplateIDColumn.ColumnName;
            }
        }


        /// <summary>
        /// Clearvalueses this instance.
        /// </summary>
        private void Clearvalues()
        {
            this.CommentTextBox.Text = string.Empty;
            this.CommentPublicCheckBox.Checked = false;
            this.CommentPrintCheckBox.Checked = false;
            this.willRollCheckBox.Checked = false;
        }

        #endregion

        /// <summary>
        /// Used for Close Operation call PcgetComment Count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F9075_FormClosed(object sender, FormClosedEventArgs e)
        {
            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
            if (!string.IsNullOrEmpty(this.commentKeyID.ToString()))
            {
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.Form9075Control.WorkItem.GetCommentsCount(this.commentFrmId, this.commentKeyID, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    this.highPriorityFlag = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }
                
        }

        private void CommentsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
           // DataGridViewColumnHeaderCell headerCell = CommentsDataGridView.Columns[2].HeaderCell;
           //////// CommentsDataGridView.Rows[0].Cells[2].ValueType = typeof(DateTime);
           //////// CommentsDataGridView.Columns["CommentDate"].DefaultHeaderCellType = System.Type.GetType("System.Date");
           ////////CommentsDataGridView.Rows[0].Cells["CommentDate"].Style.Format = "HH:mm :: dd/MM/yyyy";
           // if (headerCell.SortGlyphDirection != SortOrder.Ascending)
           // {
           //     CommentsDataGridView.Columns["CommentDate"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
           // }
           // else
           // {
           //     CommentsDataGridView.Columns[2].HeaderCell.SortGlyphDirection = SortOrder.Descending;
           // }
        }

        private void CommentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.dataChanged)
            {
                this.SetEditRecord();
            }
        }

        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (SelectAllCheckBox.Checked == true)
                {
                    selectedCommentIds = new List<int>();
                    if (this.commentsgridRowCount > 0)
                    {
                        this.SelectUnSelectAll("True");
                        this.RemoveButton.Enabled = true;
                    }
                    this.CalculateSelectAllComments(SelectAllCheckBox.Checked);

                }
                else if (SelectAllCheckBox.Checked == false)
                {
                    if (this.commentsgridRowCount > 0 && this.commentsgridRowCount <= this.selectedCommentIds.Count)
                    {
                        this.SelectUnSelectAll("False");
                        this.RemoveButton.Enabled = false;
                        this.CalculateUnSelectComments(SelectAllCheckBox.Checked);
                    }

                    if (this.commentsgridRowCount > 0 && this.commentGridAdminCount <= this.selectedCommentIds.Count)
                    {
                        this.SelectUnSelectAll("False");
                        this.RemoveButton.Enabled = false;
                        this.CalculateUnSelectComments(SelectAllCheckBox.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        
        private void panel11_Paint(object sender, PaintEventArgs e)
        {
            if (panel11.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 1;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(0,
                                                              0,
                                                              panel11.ClientSize.Width - thickness,
                                                              panel11.ClientSize.Height - thickness));
                }
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("RemoveComments"), SharedFunctions.GetResourceString("RemoveTerrComments"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    GetSelectedCommentIdsXml();
                    this.Form9075Control.WorkItem.F9075_DeleteCommentIds(this.selectedCommentsIdsXml, TerraScanCommon.UserId);
                    this.LoadCommentDataGridView();
                    this.CustomizeDataGrid();
                    this.CommentsDataGridView.RefreshEdit();
                    this.RemoveButton.Enabled = false;
                    this.SelectAllCheckBox.Checked = false;
                    this.Cursor = Cursors.Default;
                    if (this.commentSelectedRow == this.commentCount)
                    {
                        this.commentSelectedRow = 0;
                    }
                    else if (this.commentCount == 1)
                    {
                        this.commentSelectedRow = 0;
                        this.tempRowId = 0;
                    }

                    this.SetDataGridViewPosition(this.commentSelectedRow);
                    this.SetCancelButton();
                    if (this.CheckValidDataSet(this.commentDataSet))
                    {
                        ////this.SetCommentCount(this.commentCount);
                        if (this.commentCount == 0)
                        {
                            this.ClearTextBoxes();
                            this.DisableCommentControl();
                            this.NewCommentButton.Focus();

                            if (this.CommentsDataGridView.Rows.Count > 0)
                            {
                                this.CommentsDataGridView.Rows[0].Selected = false;
                                this.CommentsDataGridView.CurrentCell = null;
                            }
                        }
                        else
                        {
                            this.CheckEditPermission();
                            this.CommentsDataGridView.Focus();
                        }
                    }
                    //// According To Spec The Table Name is HardCoded It Will Used For Audit Form in Future.
                    this.CommentLinkLabel.Text = this.LinkLableValueText + " " + this.commentID; ////this.CommentIDTextBox.Text;
                    this.buttonOperation = (int)ButtonOperation.Empty;
                    this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.DeleteMode);
                    if (this.commentCount <= 0)
                    {
                        this.DisableCommentControl();
                        this.DeleteCommentButton.Enabled = false;
                        this.CommentLinkLabel.Text = this.LinkLableValueText + " " + " " + ""; ////this.CommentIDTextBox.Text;
                    }

                }
                else
                {
                    this.SetFocus();
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }  
    }
}