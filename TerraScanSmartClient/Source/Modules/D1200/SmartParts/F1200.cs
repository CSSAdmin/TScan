//--------------------------------------------------------------------------------------------
// <copyright file="F1200.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F1200 and Functionality
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Ranjani              Created    
// 02-01-2007       Ranjani              1200 posting 1.1 issue fixed
// 30/04/2009       ShanmugaSundaram.A   Modified to Implement the CO:#7104
//*********************************************************************************/
namespace D1200
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Configuration;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;    
    using TerraScan.UI.Controls;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Diagnostics;

    /// <summary>
    /// Form F1200
    /// </summary>
    [SmartPart]
    public partial class F1200 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// f1100Control Variable
        /// </summary>
        private F1200Controller form1200Control;

        /// <summary>
        /// DataSet Contains posting Details - PostTypes, previewposting and postingprocess details
        /// </summary>
        private PostingData postingDetails = new PostingData();

        /// <summary>
        /// formPostingType variable is used to find the type of posting in the form. 
        /// </summary>   
        private PostingType formPostingType;

        /// <summary>
        /// postDate variable is used to store the postdate. 
        /// </summary>   
        private DateTime postDate;

        /// <summary>
        /// postDateChanged variable is used to find the postdate is changed. 
        /// </summary>   
        private bool postDateChanged;

        /// <summary>
        /// pageLoadStatus variable is used to find the postdate is changed. 
        /// </summary>   
        private bool pageLoadStatus;

        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();
       
        #endregion   

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1200"/> class.
        /// </summary>
        public F1200()
        {
            this.InitializeComponent();
            
            ////Customize PostTypesGridView
            this.CustomizePostTypesGridView();
            this.PostTypesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PostTypesPictureBox.Height, this.PostTypesPictureBox.Width, "Post Types", 28, 81, 128);
        }

        #endregion     
     
        #region Event Publication

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// Enumerator PostingType
        /// </summary>
        public enum PostingType
        {
            /// <summary>
            /// Load Mode
            /// </summary>
            Load = 0,

            /// <summary>
            /// PreveiewPosting Mode
            /// </summary>
            PreveiewPosting,

            /// <summary>
            /// PreveiewPostingCompleted Mode
            /// </summary>
            PreveiewPostingCompleted, 

            /// <summary>
            /// Posting Process Mode
            /// </summary>
            PostingProcess,

            /// <summary>
            /// Posting Completed Mode
            /// </summary>
            PostingCompleted
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1100 control.
        /// </summary>
        /// <value>The F1100 control.</value>
        [CreateNew]
        public F1200Controller Form1200Control
        {
            get { return this.form1200Control as F1200Controller; }
            set { this.form1200Control = value; }
        }

        /// <summary>
        /// Gets or sets the type of the form posting.
        /// </summary>
        /// <value>The type of the form posting.</value>
        private PostingType FormPostingType
        {
            get { return this.formPostingType; }
            set { this.formPostingType = value; }
        }

        #endregion

        #region EventSubcription      

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1200 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1200_Load(object sender, EventArgs e)
        {
            try
            {
                ////Set DefaultDate
                this.postDate = DateTime.Now;
                this.LoadWorkSpaces();
                this.formPostingType = PostingType.Load;
                this.LoadPostingRelatedFields();
                this.ParentForm.FormClosing += new FormClosingEventHandler(this.CurrentForm_FormClosing);
                ////Apply Permission
                this.PostDateTextBox.LockKeyPress = !this.PermissionFiled.newPermission;
                this.PostDateButton.Enabled = this.PermissionFiled.newPermission;
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
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            ////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
            if (this.Form1200Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1200Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1200Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }
            
            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Posting", string.Empty }));           
        }

        /// <summary>
        /// Loads the posting related fields.
        /// </summary>
        private void LoadPostingRelatedFields()
        {
            this.pageLoadStatus = true;
            this.formPostingType = PostingType.Load;
            ////Load default postdate            
            this.PostDateTextBox.Text = this.postDate.ToString("MM/dd/yyyy");
            this.ReportButton.Enabled = false;
            this.ProcessPostingButton.Enabled = false;
            this.PostingStatuslabel.Text = string.Empty;
            this.postingDetails.Clear();

            ////Fill Dataview 
            this.postingDetails.ListPostTypes.Merge(this.Form1200Control.WorkItem.ListPostTypes());
            this.postingDetails.ListPostTypes.DefaultView.Sort = string.Concat(this.postingDetails.ListPostTypes.PostTypeIDColumn.ColumnName, " DESC");
            ////Check for empty record
            if (this.postingDetails.ListPostTypes.Rows.Count == 0)
            {
                this.PreviewPostingButton.Enabled = false;
                this.PostTypesGridView.Enabled = false;
            }
            else
            {
                this.PreviewPostingButton.Enabled = true && this.PermissionFiled.newPermission;
                this.PostTypesGridView.Enabled = true;
            }

            this.PostTypesGridView.DataSource = this.postingDetails.ListPostTypes;
            this.PostTypesGridView.CurrentCell = null;           
            if (this.postingDetails.ListPostTypes.Rows.Count > this.PostTypesGridView.NumRowsVisible)
            {
                this.PostTypesVscrollBar.Visible = false;
            }
            else
            {
                this.PostTypesVscrollBar.Visible = true;
            }

            ////qnty and amount totals
            this.PostingQuantityLabel.Text = "0";
            this.PostingAmountLinkLabel.Text = "0";
            this.ReceiptQuantityLabel.Text = "0";
            this.ReceiptTotalLinkLabel.Text = "0";

            this.pageLoadStatus = false;
        }

        #endregion  

        #region Form Close

        /// <summary>
        /// Handles the FormClosing event of the ParentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void CurrentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.formPostingType.Equals(PostingType.Load) && !this.formPostingType.Equals(PostingType.PostingCompleted))
                {
                    ////Clear Temporary Records - clears glinterface and posting error
                    this.form1200Control.WorkItem.ClearTemporaryRecords(TerraScanCommon.UserId);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region PostTypesGridView

        /// <summary>
        /// Updates the post T ypes grid.
        /// </summary>
        private void UpdatePostTypesGrid()
        {         
            this.postingDetails.ListPostingPreview.DefaultView.Sort = string.Concat(this.postingDetails.ListPostingPreview.PostTypeIDColumn.ColumnName, " DESC");
            int postingPreviewCount = 0;
            int postingQuantityCount = 0;
            int receiptQuantityCount = 0;
            decimal postingAmountTotal = 0;
            decimal receiptAmountTotal = 0;
            decimal tempValue = 0;
            int tempIntValue = 0;
            int postTypeCount = 0;
            
            ////Posttypes grid view sorted in descending order
            ////int postTypeCountFromTable = this.postingDetails.ListPostingPreview.Rows.Count;

            for (int i = 0; i < this.postingDetails.ListPostingPreview.Rows.Count; i++)
            {
                for (postTypeCount = 0; postTypeCount < this.PostTypesGridView.OriginalRowCount && postingPreviewCount < this.postingDetails.ListPostingPreview.Rows.Count; postTypeCount++)
                {
                    if (this.PostTypesGridView["PostTypeID", postTypeCount].Value != null && !string.IsNullOrEmpty(this.postingDetails.ListPostingPreview.Rows[postingPreviewCount][this.postingDetails.ListPostingPreview.PostTypeIDColumn].ToString()))
                    {
                        if (string.Equals(this.PostTypesGridView["PostTypeID", postTypeCount].Value.ToString(), this.postingDetails.ListPostingPreview.Rows[postingPreviewCount][this.postingDetails.ListPostingPreview.PostTypeIDColumn].ToString()))
                        {
                            int.TryParse(this.postingDetails.ListPostingPreview.Rows[postingPreviewCount][this.postingDetails.ListPostingPreview.QntyColumn].ToString(), out tempIntValue);
                            decimal.TryParse(this.postingDetails.ListPostingPreview.Rows[postingPreviewCount][this.postingDetails.ListPostingPreview.ReceiptAmountColumn].ToString(), out tempValue);

                            this.PostTypesGridView.Rows[postTypeCount].Cells["Quantity"].Value = tempIntValue;
                            this.PostTypesGridView.Rows[postTypeCount].Cells["ReceiptAmount"].Value = tempValue;
                            receiptQuantityCount += tempIntValue;
                            receiptAmountTotal += tempValue;
                            if (this.PostTypesGridView["SelectionColumn", postTypeCount].Value != null && string.Equals(this.PostTypesGridView["SelectionColumn", postTypeCount].Value.ToString(), bool.TrueString, StringComparison.CurrentCultureIgnoreCase))
                            {
                                postingQuantityCount += tempIntValue;
                                postingAmountTotal += tempValue;
                            }

                            postingPreviewCount++;
                        }
                    }
                }
            }

            this.ReceiptQuantityLabel.Text = receiptQuantityCount.ToString();
            this.ReceiptTotalLinkLabel.Text = receiptAmountTotal.ToString();
            this.PostingQuantityLabel.Text = postingQuantityCount.ToString();
            this.PostingAmountLinkLabel.Text = postingAmountTotal.ToString();
            if (postTypeCount > 0)
            {
                this.PostTypesGridView.CurrentCell = this.PostTypesGridView["SelectionColumn", 0];
                this.PostTypesGridView.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the PostTypesGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void PostTypesGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.PostTypesGridView.Columns["ReceiptAmount"].Index || e.ColumnIndex == this.PostTypesGridView.Columns["AmountPosted"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
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

                if (e.ColumnIndex == this.PostTypesGridView.Columns["ReceiptAmount"].Index)
                {
                    DataGridViewLinkCell tempLinkCell = this.PostTypesGridView.Rows[e.RowIndex].Cells["ReceiptAmount"] as DataGridViewLinkCell;
                    if (this.PostTypesGridView.Rows[e.RowIndex].Selected || this.PostTypesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        tempLinkCell.LinkColor = Color.White;
                        tempLinkCell.ActiveLinkColor = Color.White;
                        tempLinkCell.VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        tempLinkCell.LinkColor = Color.Blue;
                        tempLinkCell.ActiveLinkColor = Color.Red;
                        tempLinkCell.VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the PostTypesGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void PostTypesGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }    

        /// <summary>
        /// Handles the CellContentClick event of the PostTypesGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PostTypesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < this.PostTypesGridView.OriginalRowCount && e.ColumnIndex == this.PostTypesGridView.Columns["SelectionColumn"].Index)
                {
                    this.PostTypesGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (this.PostTypesGridView[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        ////used to store selected receipt amount
                        decimal outDecimal;
                        ////used to store posting quantity value
                        int outInteger;
                        if (this.PostTypesGridView[this.ReceiptAmount.Name, e.RowIndex].Value != null && this.PostTypesGridView[this.Quantity.Name, e.RowIndex].Value != null && !String.IsNullOrEmpty(this.PostTypesGridView[this.ReceiptAmount.Name, e.RowIndex].Value.ToString()) && !String.IsNullOrEmpty(this.PostTypesGridView[this.Quantity.Name, e.RowIndex].Value.ToString()))
                        {
                            decimal.TryParse(this.PostTypesGridView[this.PostTypesGridView.Columns["ReceiptAmount"].Index, e.RowIndex].Value.ToString(), System.Globalization.NumberStyles.Currency, null, out outDecimal);
                            int.TryParse(this.PostingQuantityLabel.Text, System.Globalization.NumberStyles.Currency, null, out outInteger);

                            if (string.Equals(this.PostTypesGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), bool.TrueString, StringComparison.CurrentCultureIgnoreCase))
                            {
                                outInteger += Convert.ToInt32(this.PostTypesGridView[this.PostTypesGridView.Columns["Quantity"].Index, e.RowIndex].Value);
                                outDecimal += this.PostingAmountLinkLabel.DecimalLinkLabelValue;
                            }
                            else
                            {
                                outInteger -= Convert.ToInt32(this.PostTypesGridView[this.PostTypesGridView.Columns["Quantity"].Index, e.RowIndex].Value);
                                outDecimal = this.PostingAmountLinkLabel.DecimalLinkLabelValue - outDecimal;
                            }

                            this.PostingQuantityLabel.Text = outInteger.ToString();
                            this.PostingAmountLinkLabel.Text = outDecimal.ToString();
                        }
                    }
                }

                if (e.ColumnIndex == this.PostTypesGridView.Columns["ReceiptAmount"].Index)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (e.RowIndex >= 0)
                    {
                        ////object[] optionalParameter = new object[] { string.Concat(new object[] { "WHERE POSTTYPEID = ", this.PostTypesGridView[e.ColumnIndex, e.RowIndex].Value, "RECEIPTDATE <= '", this.PostDateTextBox.Text, "',POSTID = 0, PPAYMENTID <> 0" }) };
                        ////Form receipt = new Form();
                        ////receipt = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1001, optionalParameter, this.form1200Control.WorkItem);
                        ////if (receipt != null)
                        ////{
                        ////    receipt.ShowDialog();
                        ////}

                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(11001);
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }
            }
            catch (InvalidCastException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #endregion

        #region Preview Posting

        /// <summary>
        /// Handles the Click event of the PreviewPostingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewPostingButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.PostingStatuslabel.Text = string.Empty;
                this.Cursor = Cursors.WaitCursor;                
                try
                {
                    this.validDate.Value = DateTime.Parse(PostDateTextBox.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("please enter valid date range." + "\n" + "Allowed formats: " + "d/m/yyyy." + "\n" + "Minimum value: " + "1/1/1900" + "\n" + "Minimum value: " + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PostDateTextBox.Text = System.DateTime.Now.ToShortDateString();    
                   PostDateTextBox.Focus();
                   return;
                }

                int pos = PostDateTextBox.Text.LastIndexOf("/");
                if (pos == -1)
                {
                    MessageBox.Show("please enter valid date range." + "\n" + "Allowed formats: " + "d/m/yyyy." + "\n" + "Minimum value: " + "1/1/1900" + "\n" + "Minimum value: " + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PostDateTextBox.Text = System.DateTime.Now.ToShortDateString();
                    PostDateTextBox.Focus();
                    return;
                }

                string str1 = PostDateTextBox.Text.Substring(pos + 1, 4);
                if (Convert.ToInt32(str1) < 1900 || Convert.ToInt32(str1) > 2079)
                {
                    MessageBox.Show("please enter valid date range." + "\n" + "Allowed formats: " + "d/m/yyyy." + "\n" + "Minimum value: " + "1/1/1900" + "\n" + "Minimum value: " + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PostDateTextBox.Text = System.DateTime.Now.ToShortDateString();
                    PostDateTextBox.Focus();
                    return;
                }

                ////which clears temporary records - clears glinterface, posting error and reload the form
                this.ClearForm(false);

                this.formPostingType = PostingType.PreveiewPosting;
                ////Update Post Types Grid With Qnty And Amount
                this.postingDetails.ListPostingPreview.Clear();
                ////~~~~~Commented by Malliga on 26/3/2008
                ////this.postingDetails.ListPostingPreview.Merge(this.form1200Control.WorkItem.ListPreviewPosting(this.PostDateTextBox.DateTextBoxValue));
                ////~~~~~~~Commented by Malliga on 26/3/2008
                ////Added by Malliga  on 26/3/2008

                this.postingDetails.ListPostingPreview.Merge(this.form1200Control.WorkItem.ListPreviewPosting(DateTime.Parse(this.PostDateTextBox.Text.Trim())));
                this.UpdatePostTypesGrid();

                ////Assemble posting Record set and gets error count
                this.postingDetails.PostingErrorCount.Clear();
                ////~~~~~Commented by Malliga on 26/3/2008
                ////this.postingDetails.PostingErrorCount.Merge(this.form1200Control.WorkItem.CompilePostingRecordSet(this.PostDateTextBox.DateTextBoxValue, this.RetrieveSelectedPostTypes(true), TerraScanCommon.UserId).PostingErrorCount);
                ////~~~~~Commented by Malliga on 26/3/2008
                ////Added by Malliga  on 26/3/2008
                this.postingDetails.PostingErrorCount.Merge(this.form1200Control.WorkItem.CompilePostingRecordSet(DateTime.Parse(this.PostDateTextBox.Text.Trim()), this.RetrieveSelectedPostTypes(true), TerraScanCommon.UserId).PostingErrorCount);
                if (this.postingDetails.PostingErrorCount.Rows.Count > 0 && this.postingDetails.PostingErrorCount.Rows[0][this.postingDetails.PostingErrorCount.ErrorColumn] != null && !String.IsNullOrEmpty(this.postingDetails.PostingErrorCount.Rows[0][this.postingDetails.PostingErrorCount.ErrorColumn].ToString()))
                {
                    ////Compare with the error count
                    if (Convert.ToInt32(this.postingDetails.PostingErrorCount.Rows[0][this.postingDetails.PostingErrorCount.ErrorColumn]) > 0)
                    {                        
                        ////ErrorList Form
                        Form errorListingForm = this.form1200Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1206, null, this.form1200Control.WorkItem);
                        if (errorListingForm != null)
                        {
                            errorListingForm.ShowDialog();
                        }
                    }
                    else
                    {
                        //if (this.ReceiptTotalLinkLabel.DecimalLinkLabelValue > 0)
                        //{
                        if (!string.IsNullOrEmpty(ReceiptQuantityLabel.Text.Trim()) && Convert.ToInt32(ReceiptQuantityLabel.Text.Trim()) > 0)
                        {
                            this.ReportButton.Enabled = true;
                            this.ProcessPostingButton.Enabled = true;
                            this.formPostingType = PostingType.PreveiewPostingCompleted;
                            MessageBox.Show(SharedFunctions.GetResourceString("PreviewPostStatus"), SharedFunctions.GetResourceString("TerrascanPreviewStatus"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.PostingStatuslabel.Text = SharedFunctions.GetResourceString("PreviewPostComplete") + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                        }
                        //}
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

        #endregion Preview Posting

        #region Report

        /// <summary>
        /// Handles the Click event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable reportOptionalParameter = new Hashtable();
                ////// calling  Common Function For Report                    
                reportOptionalParameter.Add("UserID", TerraScanCommon.UserId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(120010, Report.ReportType.Preview, reportOptionalParameter);
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

        #endregion Report

        #region Posting Process

        /// <summary>
        /// Handles the Click event of the PostingProcessButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PostingProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.PostingStatuslabel.Text = string.Empty;
                this.Cursor = Cursors.WaitCursor;
                if (this.formPostingType.Equals(PostingType.PreveiewPostingCompleted))
                {
                    string selectedPostTypes = this.RetrieveSelectedPostTypes(false);
                    if (string.IsNullOrEmpty(selectedPostTypes))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequirePostType"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        // If no validation errors, Show F1207 GL Export form
                        Form exportForm = new Form();
                        object[] optionalParameter = null;
                        exportForm = this.form1200Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1207, optionalParameter, this.form1200Control.WorkItem);
                        if (exportForm != null)
                        {
                            exportForm.ShowDialog();
                        }
                    }

                    this.formPostingType = PostingType.PostingProcess;

                    this.PostingProcessStatusButton.Visible = true;
                    this.PostingProcessStatusButton.Refresh();

                    ////check for Post Locks
                    this.postingDetails.PostLockCount.Clear();
                    ////Perform Posting
                    ////~~~~~Commented by Malliga on 1/4/2008
                    ////this.postingDetails.PostLockCount.Merge(this.form1200Control.WorkItem.PerformPosting(this.PostDateTextBox.DateTextBoxValue, selectedPostTypes, TerraScanCommon.UserId).PostLockCount);
                    ////~~~~~Added by Malliga on 1/4/2008
                    this.postingDetails.PostLockCount.Merge(this.form1200Control.WorkItem.PerformPosting(DateTime.Parse(this.PostDateTextBox.Text.Trim()), selectedPostTypes, TerraScanCommon.UserId).PostLockCount);
                    if (this.postingDetails.PostLockCount.Rows.Count > 0 && this.postingDetails.PostLockCount.Rows[0][this.postingDetails.PostLockCount.ErrorIDColumn] != null && !String.IsNullOrEmpty(this.postingDetails.PostLockCount.Rows[0][this.postingDetails.PostLockCount.ErrorIDColumn].ToString()))
                    {
                        int errorId = Convert.ToInt32(this.postingDetails.PostLockCount.Rows[0][this.postingDetails.PostLockCount.ErrorIDColumn]);
                        ////error occured
                        if (errorId > 0)
                        {
                            ////check the Locks
                            if (errorId == 1)
                            {
                                MessageBox.Show(this.postingDetails.PostLockCount.Rows[0][this.postingDetails.PostLockCount.ErrMsgColumn].ToString(), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("PostingConflict")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.ProcessPostingButton.Enabled = false;
                            }
                            else if (errorId == 2)
                            {
                                ////excise export error
                                MessageBox.Show(this.postingDetails.PostLockCount.Rows[0][this.postingDetails.PostLockCount.ErrMsgColumn].ToString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            this.formPostingType = PostingType.PreveiewPostingCompleted;
                            return;
                        }
                    }

                    this.formPostingType = PostingType.PostingCompleted;
                    MessageBox.Show(SharedFunctions.GetResourceString("ProcessPostStatus"), SharedFunctions.GetResourceString("TerrascanProcessStatus"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////which clears temporary records and reload the form
                    this.ClearForm(true);
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
                this.PostingProcessStatusButton.Visible = false;
                this.Cursor = Cursors.Default;
            }     
        }

        #endregion Posting Process

        #region Private Methods

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizeExciseTaxReceiptGridView
        /// </summary>
        private void CustomizePostTypesGridView()
        {
            DataGridViewColumnCollection columns = this.PostTypesGridView.Columns;

            columns["PostType"].DataPropertyName = this.postingDetails.ListPostTypes.PostNameColumn.ColumnName;
            columns["Quantity"].DataPropertyName = this.postingDetails.ListPostTypes.QntyColumn.ColumnName;
            columns["ReceiptAmount"].DataPropertyName = this.postingDetails.ListPostTypes.ReceiptAmountColumn.ColumnName;
            columns["ReceivedThrough"].DataPropertyName = this.postingDetails.ListPostTypes.PostDateColumn.ColumnName;
            columns["RanOn"].DataPropertyName = this.postingDetails.ListPostTypes.RanOnColumn.ColumnName;
            columns["AmountPosted"].DataPropertyName = this.postingDetails.ListPostTypes.AmountTotalColumn.ColumnName;
            columns["PostTypeID"].DataPropertyName = this.postingDetails.ListPostTypes.PostTypeIDColumn.ColumnName;

            columns["SelectionColumn"].DisplayIndex = 0;
            columns["PostType"].DisplayIndex = 1;
            columns["Quantity"].DisplayIndex = 2;
            columns["ReceiptAmount"].DisplayIndex = 3;
            columns["ReceivedThrough"].DisplayIndex = 4;
            columns["RanOn"].DisplayIndex = 5;
            columns["AmountPosted"].DisplayIndex = 6;
            columns["PostTypeID"].DisplayIndex = 7;            
        }

        /// <summary>
        /// Retrieves the selected post types.
        /// </summary>
        /// <param name="includeAll">if set to <c>true</c> [include all].</param>
        /// <returns>the posttypeIds</returns>
        private string RetrieveSelectedPostTypes(bool includeAll)
        {           
            DataTable tempDataTable = new DataTable();            
            tempDataTable.Columns.Add(this.postingDetails.ListPostTypes.PostTypeIDColumn.ColumnName, typeof(int));

            ////Posttypes grid view sorted in descending order
            for (int postTypeCount = 0; postTypeCount < this.PostTypesGridView.OriginalRowCount; postTypeCount++)
            {
                if ((includeAll || (this.PostTypesGridView["SelectionColumn", postTypeCount].Value != null && string.Equals(this.PostTypesGridView["SelectionColumn", postTypeCount].Value.ToString(), bool.TrueString, StringComparison.CurrentCultureIgnoreCase))) && this.PostTypesGridView["PostTypeID", postTypeCount].Value != null && !string.IsNullOrEmpty(this.PostTypesGridView["PostTypeID", postTypeCount].Value.ToString()))
                {
                    tempDataTable.Rows.Add(new object[] { this.PostTypesGridView["PostTypeID", postTypeCount].Value });
                }
            }

            if (tempDataTable.Rows.Count == 0)
            {
                return string.Empty;
            }

            return Utility.GetXmlString(tempDataTable);
        }

        #endregion

        #region Date Related Calender Controls Events

        /// <summary>
        /// Handles the DateSelected event of the PostDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void PostDateMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            this.PostDateMonthCalendar.FocusRemovedFrom = false;
            this.PostDateButton.Focus();            
            this.PostDateTextBox.Text = dateSelected;
            this.PostDateTextBox_Leave(this.PostDateTextBox, EventArgs.Empty);
        }        

        /// <summary>
        /// Handles the Click event of the PostDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PostDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.

                //Added to fix TFS#Bug 21335 by purushotham
                this.PostingStatuslabel.SendToBack();
                this.ShowPostDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }   

        /// <summary>
        /// Shows the RecieptDate calender in particular location.
        /// </summary>
        private void ShowPostDateCalender()
        {
            this.PostDateMonthCalendar.Visible = true;
            this.PostDateMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.
            this.PostDateMonthCalendar.Left = this.PostDatePanel.Left + this.PostDateButton.Left + this.PostDateButton.Width;
            this.PostDateMonthCalendar.Top = this.PostDatePanel.Top + this.PostDateButton.Top;            
            this.PostDateMonthCalendar.Focus();
            this.PostDateMonthCalendar.FocusRemovedFrom = true;
            this.PostDateMonthCalendar.Tag = this.PostDateButton.Tag;
            ////if (!string.IsNullOrEmpty(this.PostDateTextBox.Text))
            ////{
            ////    this.PostDateMonthCalendar.SetDate(Convert.ToDateTime(this.PostDateTextBox.Text));
            ////}
        } 

        /// <summary>
        /// Handles the KeyDown event of the PostDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PostDateMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.PostDateMonthCalendar.SelectionStart.ToString("MM/dd/yyyy"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        

        /// <summary>
        /// Handles the Validating event of the PostDateMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void PostDateMonthCalendar_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                if (this.PostDateMonthCalendar.FocusRemovedFrom)
                {
                    if (this.PostTypesGridView.OriginalRowCount > 0)
                    {
                        this.PostTypesGridView.Focus();
                    }
                    else
                    {
                        this.PostingAmountLinkLabel.Focus();
                    }
                }

                e.Cancel = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion    

        #region Link Click

        /// <summary>
        /// Handles the LinkClicked event of the PostingAmountLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void PostingAmountLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ////object[] optionalParameter = new object[] { string.Concat(new object[] { "WHERE RECEIPTDATE <= '", this.PostDateTextBox.Text, "',POSTID = 0, PPAYMENTID <> 0" }) };
                ////Form receipt = new Form();
                ////receipt = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1001, optionalParameter, this.form1200Control.WorkItem);
                ////if (receipt != null)
                ////{
                ////    receipt.ShowDialog();
                ////}

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the LinkClicked event of the ReceiptTotalLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ReceiptTotalLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ////object[] optionalParameter = new object[] { string.Concat(new object[] { "WHERE RECEIPTDATE <= '", this.PostDateTextBox.Text, "',POSTID = 0, PPAYMENTID <> 0" }) };
                ////Form receipt = new Form();
                ////receipt = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1001, optionalParameter, this.form1200Control.WorkItem);
                ////if (receipt != null)
                ////{
                ////    receipt.ShowDialog();
                ////}

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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

        #endregion

        #region Clear Form

        /// <summary>
        /// Handles the Click event of the ClearFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearFormButton_Click(object sender, EventArgs e)
        {            
            this.ClearForm(true);
        }

        /// <summary>
        /// Clears the form.
        /// </summary>
        /// <param name="setDefaultPostDate">if set to <c>true</c> [set default post date].</param>
        private void ClearForm(bool setDefaultPostDate)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ////Clear Temporary Records - clears glinterface and posting error
                this.form1200Control.WorkItem.ClearTemporaryRecords(TerraScanCommon.UserId);
                if (setDefaultPostDate)
                {
                    this.postDate = DateTime.Now;
                }

                this.LoadPostingRelatedFields();                
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

        #endregion

        #region PostDate

        /// <summary>
        /// Handles the TextChanged event of the PostDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PostDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.postDateChanged = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the PostDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PostDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.postDateChanged)
                {
                    this.pageLoadStatus = true;
                    this.PostingStatuslabel.Text = string.Empty;
                    if (!DateTime.TryParse(this.PostDateTextBox.Text.Trim(), out this.postDate))
                    {
                        this.postDate = DateTime.Now;
                       //// this.PostDateTextBox.Text = this.postDate.ToString("MM/dd/yyyy");
                    }

                    if (this.postDate > DateTime.Now)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("PostDateValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.postDate = DateTime.Now;
                        this.PostDateTextBox.Text = this.postDate.ToString("MM/dd/yyyy");
                    }

                    this.pageLoadStatus = false;
                    this.postDateChanged = false;
                    if (!this.formPostingType.Equals(PostingType.Load))
                    {
                        this.ClearForm(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}
