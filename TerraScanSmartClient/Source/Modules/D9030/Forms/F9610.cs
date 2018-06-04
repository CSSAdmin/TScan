//--------------------------------------------------------------------------------------------
// <copyright file="F9610.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9610.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 03/06/2007       R.Malliga            Created
//20120510         Manoj P             Changes in the form Quick Find Operation 
//20120918         Manoj P             Changes in position,Current record Load Operation and Jerk free
//                                                              form Master Operation. 
//20120921         Manoj P              Need for Sp call to Load Navigation button Enabled.   
//*********************************************************************************/

namespace D9030
{
    using System;
    using System.Data;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using D90010;

    /// <summary>
    /// Class file for F9610
    /// </summary>
    public partial class F9610 : Form
    {
        #region Variables
        /// <summary>
        /// F9610Controller
        /// </summary>
        private F9610Controller form9610Control;

        /// <summary>
        ///quickfinditemDataSet
        /// </summary>
        private F9610QuickFind quickfinditem = new F9610QuickFind();

        /// <summary>
        /// Formno
        /// </summary>
        private int formno;

        /// <summary>
        /// Previous Count
        /// </summary>
        private int prevcount;

        /// <summary>
        /// Next Count
        /// </summary>
        private int nextcount = 1;

        /// <summary>
        /// No of Rows
        /// </summary>
        private string noofrows = string.Empty;

        /// <summary>
        /// Previously stored datatable
        /// </summary>
        private DataTable lastcontentdt = new DataTable();

        /// <summary>
        /// No of Rows
        /// </summary>
        private string lastcontentString = string.Empty;

        private F9030 formmaster;

        private DataSet xmlCovertDataSet = new DataSet();

        private int previouspress = -1;

        private int keyid;

        private DataTable tempDataTable= new DataTable();

        private DataTable tempdataset;

        ///<summary>
        /// Position of the record
        /// </summary>
        private int recordPositions = -1;

        private bool flagLoadProcess = false;

        private int findRowIndex;

        private  int selectRowIndex=0;

        private int tempKeyId;

        private bool colorChange = true;
        private bool tempValue = false;
        private int currentId;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9610"/> class.
        /// </summary>
        public F9610()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9610"/> class.
        /// </summary>
        /// <param name="formno">The formno.</param>
        public F9610(int formno, string recordIdentity, string findvalue, DataTable dataTable, bool value,int currentKeyId)
        {
            this.InitializeComponent();
            this.formno = formno;
            if (dataTable != null)
            {
                if (value)
                {
                    this.colorChange = true;
                    this.tempdataset = dataTable;
                }
                else
                {
                    this.colorChange = false;
                    this.tempdataset = dataTable;
                }
                  
            }
            else
            {
                this.tempdataset = null;
            }
            if (!string.IsNullOrEmpty(findvalue))
            {
                this.FindTextBox.Text = findvalue;
            }
            else
            {
                this.FindTextBox.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(recordIdentity))
            {
                int.TryParse(recordIdentity, out this.keyid);
            }

            this.tempValue = value;
            this.currentId = currentKeyId;
        }

        #endregion

        #region Property

        /// <summary>
        /// For F9610Control
        /// </summary>
        [CreateNew]
        public F9610Controller Form9610Control
        {
            get { return this.form9610Control as F9610Controller; }
            set { this.form9610Control = value; }
        }

         ////<summary>
         ////Gets or sets the content of the last.
         ////</summary>
         ////<value>The content of the last.</value>
        public string CommandString
        {
            get { return this.lastcontentString; }
            set { this.lastcontentString = value; }
        }

        ///// <summary>
        ///// Gets or sets the Position of the Record.
        ///// </summary>
        ///// <value>The position of the record.</value>
        //public int CommandRecord
        //{
        //    get { return this.nextcount; }
        //    set { this.nextcount = value; }
        //}

        /// <summary>
        /// Gets or sets the Identity of the Record.
        /// </summary>
        /// <value>The Identity of the record.</value>
        public int CommandIdentity
        {
            get { return this.keyid; }
            set { this.keyid = value; }
        }

        /// <summary>
        /// Gets or sets the CommentDataTable
        /// </summary>
        /// <value>The Identity of the record.</value>
        public DataTable CommentDataTable
        {
            get { return this.tempDataTable; }
            set { this.tempDataTable = value; }
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        /// <summary>
        /// Event for SetActiveKeyid
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_SetMasterGnrlPropertis, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> SetGnrlProperties;

        /// <summary>
        /// Occurs when [set report additional properties].
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_EnableReportAdditonalProperties, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> SetReportAdditionalProperties;

        /// <summary>
        /// Occurs when [on form master_ visible forms].
        /// </summary>
        [EventPublication(EventTopicNames.FormMaster_VisibleForms, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<EnablePanelEventArgs>> OnFormMaster_VisibleForms;


        /// <summary>
        /// Occurs when [on quick find_ key id].
        /// </summary>
        [EventPublication(EventTopicNames.QuickFind_KeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> OnQuickFind_KeyId;

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F9610 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9610_Load(object sender, EventArgs e)
        {
            try
            {
                
                this.flagLoadProcess = false;
               // this.FindTextBox.Text = string.Empty;
                this.ItemTextBox.Text = string.Empty;
                this.FindButton.Enabled = false;
                this.PreviewButton.Enabled = false;
                this.CloseButton.Enabled = false;
                this.CustomizeGridView();
                if (this.tempdataset!=null)
                {
                    if (this.tempdataset.Rows.Count > 0)
                    {
                        this.PreviewButton.Enabled = true;
                        this.CloseButton.Enabled = true;
                        this.quickfinditem.F9610_GetQuickFindItem.Clear();   
                    }
                    
                    this.quickfinditem.F9610_GetQuickFindItem.Merge(this.tempdataset);   
                    this.FindListDataGridView.DataSource = this.tempdataset.DefaultView; 
                    noofrows = this.tempdataset.Rows.Count.ToString();
                    if (int.Parse(noofrows) > 0)
                    {
                        int.TryParse(this.tempdataset.Rows[0]["KeyID"].ToString(), out this.tempKeyId);  
                        this.RecordCountLabel.Text = "1" + " / " + noofrows + " " + "Found";
                    }
                    if (this.FindListDataGridView.OriginalRowCount >= 10)
                    {
                        this.MasterNameVerticalScroll.Visible = false;
                    }
                    else
                    {
                        this.MasterNameVerticalScroll.Visible = true;
                    }
                }
                this.FindTextBox.Focus();
                this.flagLoadProcess = true;
                             
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

        #region Find Textbox TextChanged Event
        /// <summary>
        /// Handles the TextChanged event of the FindTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FindTextBox_TextChanged(object sender, EventArgs e)
           {
            try
            {
                string reAssign = String.Empty;
                
                if (!string.IsNullOrEmpty(this.FindTextBox.Text.Trim()))
                {
                    this.Cursor = Cursors.Arrow;
                    this.FindButton.Enabled = true;
                   // FindTextBox.Text = this.FindTextBox.Text.Trim();
                    

                }
                else
                {
                    this.FindButton.Enabled = false;
            
                }
                
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the FindTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.FindButton_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        private void CustomizeGridView()
        {
            this.FindListDataGridView.AutoGenerateColumns = false;
            this.FindListDataGridView.ApplyStandardBehaviour = true;
            DataGridViewColumnCollection FindListDataColumns = this.FindListDataGridView.Columns;
            if (FindListDataColumns.Count > 0)
            {
                FindListDataColumns["Item"].DataPropertyName = this.quickfinditem.F9610_GetQuickFindItem.ItemColumn.ColumnName;
                FindListDataColumns["Key"].DataPropertyName = this.quickfinditem.F9610_GetQuickFindItem.KeyIDColumn.ColumnName;
                FindListDataColumns["Item"].DisplayIndex = 0;
                FindListDataColumns["Key"].DisplayIndex = 1;
            }
            this.FindListDataGridView.DataSource = this.quickfinditem.F9610_GetQuickFindItem.DefaultView;
            this.FindListDataGridView.PrimaryKeyColumnName = this.FindListDataGridView.Columns[1].DataPropertyName;
        }


        #region Button Events
        /// <summary>
        /// Handles the Click event of the FindButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FindButton_Click(object sender, EventArgs e)
        {
            try
            {
                string findvalue = string.Empty;
                if (!string.IsNullOrEmpty((this.FindTextBox.Text.Trim())))
                {

                    if (this.FindTextBox.Text.Trim().Contains("'"))
                    {
                        findvalue = this.FindTextBox.Text.Trim().Replace("'", "''");
                    }
                    else
                    {
                        findvalue = this.FindTextBox.Text.Trim();
                    }
                    this.quickfinditem.F9610_GetQuickFindItem.Clear();
                    this.quickfinditem = this.form9610Control.WorkItem.F9610QuickFind(this.formno, findvalue);
                    this.CustomizeGridView();
                    if (this.quickfinditem.F9610_GetQuickFindItem.Rows.Count > 0)
                    {
                        this.findRowIndex = 0;
                        this.FindListDataGridView.DataSource = null;
                        this.FindListDataGridView.DataSource = this.quickfinditem.F9610_GetQuickFindItem.DefaultView;
                        int.TryParse(this.quickfinditem.F9610_GetQuickFindItem.Rows[0]["KeyID"].ToString(), out this.tempKeyId);  
                        if (this.FindListDataGridView.OriginalRowCount > 10)
                        {
                            this.MasterNameVerticalScroll.Visible = false;
                        }
                        else
                        {
                            this.MasterNameVerticalScroll.Visible = true;
                        }
                        if (this.quickfinditem.F9610_GetQuickFindRows.Count > 0)
                        {
                            noofrows = this.quickfinditem.F9610_GetQuickFindRows[0]["NoOFRows"].ToString();
                            if (int.Parse(noofrows) > 0)
                            {
                                this.RecordCountLabel.Text = 1 + " / " + noofrows + " " + "Found";
                                this.PreviewButton.Enabled = true;
                                this.CloseButton.Enabled = true;
                            }
                            else
                            {
                                this.PreviewButton.Enabled = false;
                                this.CloseButton.Enabled = false;
                                this.RecordCountLabel.Text = "None Found";
                            }
                        }

                    }
                    else
                    {
                        this.FindListDataGridView.DataSource = null;
                        this.RecordCountLabel.Text = "0 record(s) match.";
                        this.MasterNameVerticalScroll.Visible = true;
                        this.PreviewButton.Enabled = false;
                        this.CloseButton.Enabled = false;
                    }
                
                }
                else
                {
                    this.FindListDataGridView.DataSource = null;
                    this.RecordCountLabel.Text = null;
                    this.MasterNameVerticalScroll.Visible = true;
                    this.PreviewButton.Enabled = false;
                    this.CloseButton.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {

                if (this.quickfinditem.F9610_GetQuickFindRows.Rows.Count > 0)
                {
                    this.FormMaster_FormVisibility(true);
                }
                
            }
        }

    /*    /// <summary>
        /// Handles the Click event of the PreviousButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviousButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.nextcount == 1)
                //{
                //    this.nextcount = int.Parse(noofrows) + 1;
                //}
                //this.nextcount = this.nextcount - 1;
                //this.ItemTextBox.Text = this.quickfinditem.F9610_GetQuickFindItem[nextcount - 1]["Item"].ToString();
                //keyid = int.Parse(this.quickfinditem.F9610_GetQuickFindItem[nextcount - 1]["KeyID"].ToString());

                //this.RecordCountLabel.Text = nextcount + " / " + noofrows + " " + "Found";

                ////this.FormMaster_FormVisibility(false);

                //SliceReloadActiveRecord sliceReloadActiveRecord;
                //sliceReloadActiveRecord.MasterFormNo = this.formno;
                //sliceReloadActiveRecord.SelectedKeyId = keyid;
                ////OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                //OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                //if (F95012.IsQuickFindFlag)
                //{
                //    this.lastcontentdt.Clear();
                //    this.Close();
                //    this.Visible = false;  
                //}
                //else
                //{
                //    int[] currentformno = new int[2];
                //    currentformno[0] = this.formno;
                //    currentformno[1] = keyid;
                //    this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                //    int[] currentformdetails = new int[3];
                //    currentformdetails[0] = this.formno;
                //    currentformdetails[1] = keyid;
                //    currentformdetails[2] = 1;
                //    this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                //}
                //F95012.IsQuickFindFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.FormMaster_FormVisibility(true);
            }
        }

         /// <summary>
        /// Handles the Click event of the NextButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NextButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.nextcount.ToString() == noofrows)
                //{
                //    this.nextcount = 0;
                //}

                //this.ItemTextBox.Text = this.quickfinditem.F9610_GetQuickFindItem[nextcount]["Item"].ToString();
                //keyid = int.Parse(this.quickfinditem.F9610_GetQuickFindItem[nextcount]["KeyID"].ToString());

                //this.nextcount = this.nextcount + 1;
                //this.RecordCountLabel.Text = nextcount + " / " + noofrows + " " + "Found";

                ////this.FormMaster_FormVisibility(false);

                //SliceReloadActiveRecord sliceReloadActiveRecord;
                //sliceReloadActiveRecord.MasterFormNo = this.formno;
                //sliceReloadActiveRecord.SelectedKeyId = keyid;
                ////OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                //    OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                //    if (F95012.IsQuickFindFlag)
                //    {
                //        this.lastcontentdt.Clear();
                //        this.Close();
                //        this.Visible = false;  
                //    }
                //    else
                //    {


                //            int[] currentformno = new int[2];
                //            currentformno[0] = this.formno;
                //            currentformno[1] = keyid;
                //            this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                //            int[] currentformdetails = new int[3];
                //            currentformdetails[0] = this.formno;
                //            currentformdetails[1] = keyid;
                //            currentformdetails[2] = 1;
                //            this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));

                //    }
                //    F95012.IsQuickFindFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.FormMaster_FormVisibility(true);
            }
        }*/

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            D9030_F9030_LoadSliceDetails(this, eventArgs);
        }

        /// <summary>
        /// Handles the FormClosing event of the F9610 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9610_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
            /*if (lastcontentdt.Columns.Count == 0)
            {
                lastcontentdt.Columns.Add("FormNo");
                lastcontentdt.Columns.Add("KeyWord");
            }
            lastcontentdt.PrimaryKey = new DataColumn[1] { lastcontentdt.Columns[0] };
            
                if (lastcontentdt.Rows.Count > 0)
                {
                    for (int i = 0; i <= lastcontentdt.Rows.Count - 1; i++)
                    {
                        if (this.formno.ToString() == lastcontentdt.Rows[i]["FormNo"].ToString())
                        {
                            lastcontentdt.Rows[i].Delete();
                            DataRow dr;
                            dr = lastcontentdt.NewRow();
                            dr["FormNo"] = this.formno;
                            if (!string.IsNullOrEmpty(this.FindTextBox.Text))
                            {
                                dr["KeyWord"] = this.FindTextBox.Text.Trim();
                            }
                            else
                            {
                                dr["KeyWord"] = "XXX";
                            }
                            lastcontentdt.Rows.Add(dr);
                            //CommandResult = TerraScanCommon.GetXmlString(lastcontentdt);
                            //CommandRecord = this.nextcount;
                            CommandIdentity = this.keyid;

                            // Manoj 20120409 Changes for FM Button disabled incorrectly issue fixed TSBG #17405
                            int[] currentformdetails = new int[3];
                            currentformdetails[0] = this.formno;
                            currentformdetails[1] = keyid;
                            currentformdetails[2] = 1;
                            this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                        }
                    }
                }
                else
                {
                    DataRow dr;
                    dr = lastcontentdt.NewRow();
                    dr["FormNo"] = this.formno;
                    if (!string.IsNullOrEmpty(this.FindTextBox.Text))
                    {
                        dr["KeyWord"] = this.FindTextBox.Text.Trim();
                    }
                    else
                    {
                        dr["KeyWord"] = "XXX";
                    }
                    lastcontentdt.Rows.Add(dr);
                    CommandResult = TerraScanCommon.GetXmlString(lastcontentdt);
                    

                }*/
                if (this.keyid > 0)
                {
                    CommandIdentity = this.keyid;
                }
                if (this.quickfinditem.F9610_GetQuickFindItem.Rows.Count > 0)
                {
                    this.tempDataTable = this.quickfinditem.F9610_GetQuickFindItem;
                    CommentDataTable = this.tempDataTable;
                }
                else
                {
                    CommentDataTable = null;
                }
                if (!string.IsNullOrEmpty(this.FindTextBox.Text))
                {
                    this.lastcontentString = this.FindTextBox.Text.Trim();
                    CommandString = this.lastcontentString;
                }
                else
                {
                    CommandString = string.Empty; 
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                const int WM_KEYDOWN = 0x100;
                const int WM_SYSKEYDOWN = 0x104;
                if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
                {
                    if (!string.IsNullOrEmpty(this.FindTextBox.Text) && !string.IsNullOrEmpty(noofrows))
                    {
                        if (keyData.Equals(Keys.Control | Keys.P))
                        {
                            this.previewOperation();
                        }
                        if (keyData.Equals(Keys.F3))
                        {
                            if (this.nextcount == 1)
                            {
                                this.nextcount = int.Parse(noofrows) + 1;
                            }

                            this.nextcount = this.nextcount - 1;
                            if (this.nextcount > 0)
                            {
                                this.ItemTextBox.Text = this.quickfinditem.F9610_GetQuickFindItem[nextcount - 1]["Item"].ToString();
                                keyid = int.Parse(this.quickfinditem.F9610_GetQuickFindItem[nextcount - 1]["KeyID"].ToString());

                                this.FormMaster_FormVisibility(false);

                                this.RecordCountLabel.Text = nextcount + " / " + noofrows + " " + "Found";
                                SliceReloadActiveRecord sliceReloadActiveRecord;
                                sliceReloadActiveRecord.MasterFormNo = this.formno;
                                sliceReloadActiveRecord.SelectedKeyId = keyid;
                                //OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                                OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                                int[] currentformno = new int[2];
                                currentformno[0] = this.formno;
                                currentformno[1] = keyid;
                                this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                                int[] currentformdetails = new int[3];
                                currentformdetails[0] = this.formno;
                                currentformdetails[1] = keyid;
                                currentformdetails[2] = 1;
                                this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                                // CheckPageStatus(this, new DataEventArgs<Button>(this.NextButton));
                            }
                        }
                        else if (keyData.Equals(Keys.F4))
                        {
                            if (this.nextcount.ToString() == noofrows)
                            {
                                this.nextcount = 0;
                            }

                            if (this.quickfinditem.F9610_GetQuickFindItem.Rows.Count > 0)
                            {
                                this.ItemTextBox.Text = this.quickfinditem.F9610_GetQuickFindItem[nextcount]["Item"].ToString();
                                keyid = int.Parse(this.quickfinditem.F9610_GetQuickFindItem[nextcount]["KeyID"].ToString());

                                this.FormMaster_FormVisibility(false);

                                this.nextcount = this.nextcount + 1;
                                this.RecordCountLabel.Text = nextcount + " / " + noofrows + " " + "Found";
                                SliceReloadActiveRecord sliceReloadActiveRecord;
                                sliceReloadActiveRecord.MasterFormNo = this.formno;
                                sliceReloadActiveRecord.SelectedKeyId = keyid;
                                //OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                                
                                OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                                // CheckPageStatus(this, new DataEventArgs<Button>(this.NextButton));
                                int[] currentformno = new int[2];
                                currentformno[0] = this.formno;
                                currentformno[1] = keyid;
                                this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                                int[] currentformdetails = new int[3];
                                currentformdetails[0] = this.formno;
                                currentformdetails[1] = keyid;
                                currentformdetails[2] = 1;
                                this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                            }
                        }


                        if (keyData.Equals(Keys.Escape))
                        {
                            if (this.CloseButton.Enabled)
                            {
                                this.closeOperation();
                            }
                            else
                            {
                                this.Close();
                                this.DialogResult = DialogResult.Cancel;
                            }
                        }
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(noofrows))
                        {
                            if (keyData.Equals(Keys.Control | Keys.P))
                            {
                                this.previewOperation();
                            }
                        }
                        if (keyData.Equals(Keys.Escape))
                        {
                            this.Close();
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                if (!string.IsNullOrEmpty(this.FindTextBox.Text) && !string.IsNullOrEmpty(noofrows))
                {
                    if (keyData.Equals(Keys.F3) || keyData.Equals(Keys.F4))
                    {
                        this.FormMaster_FormVisibility(true);
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
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
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Forms the master_ reduce flicker.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        private void FormMaster_FormVisibility(bool isVisible)
        {
            EnablePanelEventArgs visibleInfo;
            visibleInfo.IsSlice = false;
            visibleInfo.IsVisible = isVisible;
            this.OnFormMaster_VisibleForms(this, new DataEventArgs<EnablePanelEventArgs>(visibleInfo));
        }
        
        #endregion

        private void previewOperation()
        {
            try
            {
                if (!keyid.Equals(this.tempKeyId))
                {
                    keyid = this.tempKeyId;
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.formno;
                    sliceReloadActiveRecord.SelectedKeyId = keyid;
                    //  this.D9030_F9030_LoadSliceDetails(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord)); 
                    //OnD9030_F9030_LoadSliceDetails(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    DataRow[] selectedrow = this.quickfinditem.F9610_GetQuickFindItem.Select("KeyId=" + keyid);
                    this.FindListDataGridView.Rows[this.selectRowIndex].Cells[1].Style.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                    int[] currentformno = new int[2];
                    currentformno[0] = this.formno;
                    currentformno[1] = keyid;
                    this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                    int[] currentformdetails = new int[3];
                    currentformdetails[0] = this.formno;
                    currentformdetails[1] = keyid;
                    currentformdetails[2] = 1;
                    this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                }
                else
                {
                    if (tempValue.Equals(false) && (keyid.Equals(this.tempKeyId)) && (!currentId.Equals(this.tempKeyId)))
                    {
                        keyid = this.tempKeyId;
                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.formno;
                        sliceReloadActiveRecord.SelectedKeyId = keyid;
                        //  this.D9030_F9030_LoadSliceDetails(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord)); 
                        //OnD9030_F9030_LoadSliceDetails(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        DataRow[] selectedrow = this.quickfinditem.F9610_GetQuickFindItem.Select("KeyId=" + keyid);
                        this.FindListDataGridView.Rows[this.selectRowIndex].Cells[1].Style.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                        int[] currentformno = new int[2];
                        currentformno[0] = this.formno;
                        currentformno[1] = keyid;
                        this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                        int[] currentformdetails = new int[3];
                        currentformdetails[0] = this.formno;
                        currentformdetails[1] = keyid;
                        currentformdetails[2] = 1;
                        this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                    }
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void closeOperation()
        {
            try
            {
                if (!keyid.Equals(this.tempKeyId) )
                {
                    keyid = this.tempKeyId;
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.formno;
                    sliceReloadActiveRecord.SelectedKeyId = keyid;
                    OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.Close();
                    this.DialogResult = DialogResult.Cancel;
                    int[] currentformno = new int[2];
                    currentformno[0] = this.formno;
                    currentformno[1] = keyid;
                    this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                    int[] currentformdetails = new int[3];
                    currentformdetails[0] = this.formno;
                    currentformdetails[1] = keyid;
                    currentformdetails[2] = 1;
                    this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                }
                else
                {
                    if (tempValue.Equals(false) && (keyid.Equals(this.tempKeyId)) && (!currentId.Equals(this.tempKeyId)))
                    {
                                keyid = this.tempKeyId;
                                SliceReloadActiveRecord sliceReloadActiveRecord;
                                sliceReloadActiveRecord.MasterFormNo = this.formno;
                                sliceReloadActiveRecord.SelectedKeyId = keyid;
                                OnQuickFind_KeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                                this.Close();
                                this.DialogResult = DialogResult.Cancel;
                                int[] currentformno = new int[2];
                                currentformno[0] = this.formno;
                                currentformno[1] = keyid;
                                this.SetGnrlProperties(this, new DataEventArgs<int[]>(currentformno));
                                int[] currentformdetails = new int[3];
                                currentformdetails[0] = this.formno;
                                currentformdetails[1] = keyid;
                                currentformdetails[2] = 1;
                                this.SetReportAdditionalProperties(this, new DataEventArgs<int[]>(currentformdetails));
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.closeOperation();
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            this.previewOperation();
        }

        private void FindListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (flagLoadProcess)
                {
                    if (e.RowIndex > -1)
                    {
                        
                        if (this.FindListDataGridView.Rows[e.RowIndex].Cells["Item"].Value.ToString() != null) 
                        {
                            this.selectRowIndex = e.RowIndex; 
                            int.TryParse(this.FindListDataGridView.Rows[e.RowIndex].Cells["Key"].Value.ToString()  , out this.tempKeyId); 
                            this.findRowIndex = e.RowIndex;
                            int rowCount = e.RowIndex + 1;
                            this.RecordCountLabel.Text = rowCount + " / " + noofrows + " " + "Found";
                           // this.FindListDataGridView.Rows[e.RowIndex].Cells[0].Style.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
                            //this.FindListDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.SystemColors.HotTrack;
                            //is.FindListDataGridView.RowHeadersDefaultCellStyle.BackColor 
                            if (this.keyid != null)
                            {
                                //if (this.keyid.Equals(this.tempKeyId))
                                //{
                                //    this.PreviewButton.Enabled = false;
                                //    this.CloseButton.Enabled = false;
                                //}
                                //else
                                //{
                                //    this.PreviewButton.Enabled = true;
                                //    this.CloseButton.Enabled = true;
                                //}
                            }

                        }
                        else
                        {

                            this.RecordCountLabel.Text = string.Empty;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void FindListDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (flagLoadProcess)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (this.FindListDataGridView.Rows[e.RowIndex].Cells[1].Value.Equals(keyid))
                        {
                            if (this.colorChange)
                            {
                                this.FindListDataGridView.Rows[e.RowIndex].Cells[0].Style.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                            }
                            else
                            {
                                this.FindListDataGridView.Rows[e.RowIndex].Cells[0].Style.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                            }
                        }
                        else
                        {
                            this.FindListDataGridView.Rows[e.RowIndex].Cells[0].Style.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
                        }
                    }
                 }
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }


        }

        private void FindListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.flagLoadProcess)
            {
                if (e.RowIndex >= 0)
                {
                 
                    this.closeOperation();
                }
            }

        }

        private void FindListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //this.FindListDataGridView.SelectedCells
            if (this.FindListDataGridView.GridContentSelected)
            {
                this.selectRowIndex = this.FindListDataGridView.SelectedRows[0].Index;
                int.TryParse(this.FindListDataGridView.Rows[selectRowIndex].Cells["Key"].Value.ToString(), out this.tempKeyId);
                    this.findRowIndex = selectRowIndex;
                    int rowCount = selectRowIndex + 1;
                    this.RecordCountLabel.Text = rowCount + " / " + noofrows + " " + "Found";
            }
            else
            {
            }

            

            //if (selectRowIndex != null)
            //{
                
            //    int.TryParse(this.FindListDataGridView.Rows[selectRowIndex].Cells["Key"].Value.ToString(), out this.tempKeyId);
            //    this.findRowIndex = selectRowIndex;
            //    int rowCount = selectRowIndex + 1;
            //    this.RecordCountLabel.Text = rowCount + " / " + noofrows + " " + "Found";
            //}
            //else
            //{

            //}

              
        }

        private void FindListDataGridView_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
