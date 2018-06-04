//--------------------------------------------------------------------------------------------
// <copyright file="F9102.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Status.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D91000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using TerraScan.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Common.Reports;
    using System.Reflection;

    /// <summary>
    /// public partial class F9102
    /// </summary>
    public partial class F9102 : Form
    {
        #region Variables

        /// <summary>
        /// Used to store the typeId
        /// </summary>
        private int typeId;

        /// <summary>
        /// Used to store the keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// Used to store the rowcount
        /// </summary>
        private int rowcount;

        /// <summary>
        /// Created Instance for F1512Controller
        /// </summary>
        private F9102Controller form9102Control;

        /// <summary>
        /// Created Instance for F9102OwnerStatusData
        /// </summary>
        private F9102OwnerStatusData ownerStatusData = new F9102OwnerStatusData();

        /// <summary>
        /// Used to store the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// variable holds the paymentIds Xml String.
        /// </summary>
        private string ownerIdsXml;

        /// <summary>
        /// strSortOrder
        /// </summary>
        private string strSortOrder = "ASC";

        /// <summary>
        /// systemSnapShotId
        /// </summary>
        private int systemSnapShotId;

        /// <summary>
        /// systemSnapShotCount
        /// </summary>
        private int systemSnapShotCount;

        /// <summary>
        /// systemSnapShotCount
        /// </summary>
        private bool isSystemSnapShot;

        /// <summary>
        /// Used to store the ownerId
        /// </summary>
        private int ownerId = 0;

        /// <summary>
        /// Binding Source
        /// </summary>
        private BindingSource bindingSource;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9102"/> class.
        /// </summary>
        public F9102()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9102"/> class.
        /// </summary>         
        /// <param name="keyId">KeyId</param>
        /// <param name="typeId">The typeId</param>
        public F9102(int typeId, int keyId)
        {
            InitializeComponent();
            this.typeId = typeId;
            this.keyId = keyId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F9102"/> class.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="masterform">The masterform.</param>
        public F9102(int keyId, int typeId, int masterform)
        {
            InitializeComponent();
            this.typeId = typeId;
            this.keyId = keyId;
            this.masterFormNo = masterform;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Event publication to intimate and set system snapshot in query engine
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_SetSystemSnapshotEvent, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails>> D9030_F9033_SetSystemSnapshotEvent;

        #endregion Event Publication

        #region Properties

        /// <summary>
        /// Gets or sets the F9102 control.
        /// </summary>
        /// <value>The F9102 control.</value>
        [CreateNew]
        public F9102Controller F9102Control
        {
            get { return this.form9102Control as F9102Controller; }
            set { this.form9102Control = value; }
        }

        /// <summary>
        /// Gets or sets the ownerIdsXml.
        /// </summary>
        /// <value>The ownerIdsXml.</value>
        public string OwnerIdsXml
        {
            get
            {
                return this.ownerIdsXml;
            }

            set
            {
                this.ownerIdsXml = value;
            }
        }
        #endregion Properties

        #region EventSubscription

        /// <summary>
        /// Called when [D9030_ F9033_ system snapshot complete event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_SystemSnapshotCompleteEvent, ThreadOption.UserInterface)]
        public void OnD9030_F9033_SystemSnapshotCompleteEvent(object sender, TerraScan.Infrastructure.Interface.EventArgs<SetSystemSnapShotIdnCount> eventArgs)
        {
            this.systemSnapShotId = eventArgs.Data.SystemSnapShotId;
            this.systemSnapShotCount = eventArgs.Data.SystemSnapShotCount;
        }
        #endregion EventSubscription

        #region SetSystemSnapshotEvent

        /// <summary>
        /// SetSystemSnapshot Event
        /// </summary>
        /// <param name="eventArgs">eventArgs</param>
        protected virtual void OnD9030_F9033_SetSystemSnapshotEvent(TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails> eventArgs)
        {
            if (this.D9030_F9033_SetSystemSnapshotEvent != null)
            {
                this.D9030_F9033_SetSystemSnapshotEvent(this, eventArgs);
            }
        }

        #endregion SetSystemSnapshotEvent

        #region Methods
        /// <summary>
        /// Method to load the OwnerStatusGrid
        /// </summary>
        private void LoadOwnerStatusGrid()
        {
            ////this.Cursor = Cursors.WaitCursor;
            this.bindingSource = new BindingSource();
            this.ownerStatusData = this.form9102Control.WorkItem.F9102_GetOwnerStatusDetails(this.typeId, this.keyId);
            this.rowcount = this.ownerStatusData.OwnerStatusDetailsTable.Rows.Count;

            this.Owner.DataPropertyName = this.ownerStatusData.OwnerStatusDetailsTable.OwnerNameColumn.ColumnName;
            this.Status.DataPropertyName = this.ownerStatusData.OwnerStatusDetailsTable.OwnerStatusTypeColumn.ColumnName;
            this.BeginDate.DataPropertyName = this.ownerStatusData.OwnerStatusDetailsTable.BeginDateColumn.ColumnName;
            this.EndDate.DataPropertyName = this.ownerStatusData.OwnerStatusDetailsTable.EndDateColumn.ColumnName;
            this.Note.DataPropertyName = this.ownerStatusData.OwnerStatusDetailsTable.NoteColumn.ColumnName;
            this.Priority.DataPropertyName = this.ownerStatusData.OwnerStatusDetailsTable.IsPriorityColumn.ColumnName;
            this.SortID.DataPropertyName = this.ownerStatusData.OwnerStatusDetailsTable.SortIDColumn.ColumnName;

            this.OwnerStatusDataGridView.PrimaryKeyColumnName = this.ownerStatusData.OwnerStatusDetailsTable.SortIDColumn.ColumnName;
            this.OwnerStatusDataGridView.DataSource = this.ownerStatusData.OwnerStatusDetailsTable.DefaultView;
            this.bindingSource.DataSource = this.ownerStatusData.OwnerStatusDetailsTable.DefaultView;
            ////this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.RowFilter = "EmptyRecord$=False"; 
            this.OwnerStatusDataGridView.Columns[this.ownerStatusData.OwnerStatusDetailsTable.OwnerStatusTypeIDColumn.ColumnName].Visible = false;
            this.OwnerStatusDataGridView.Columns[this.ownerStatusData.OwnerStatusDetailsTable.OwnerIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerStatusDataGridView.Columns[this.ownerStatusData.OwnerStatusDetailsTable.OwnerIDColumn.ColumnName].Visible = false; 
            if (this.rowcount > this.OwnerStatusDataGridView.NumRowsVisible)
            {
                this.OwnerStatusVerticalScroll.Enabled = true;
                this.OwnerStatusVerticalScroll.Visible = false;
            }
            else
            {
                this.OwnerStatusVerticalScroll.Visible = true;
                this.OwnerStatusVerticalScroll.Enabled = false;
            }
        }

        /// <summary>
        /// Method to load the TitleText
        /// </summary>
        private void SetTitleText()
        {
            if (!string.IsNullOrEmpty(this.ownerStatusData.TitleTable.Rows[0][this.ownerStatusData.TitleTable.TitleBarTextColumn].ToString()))
            {
                this.Text = this.ownerStatusData.TitleTable.Rows[0][this.ownerStatusData.TitleTable.TitleBarTextColumn].ToString();
            }
        }

        /// <summary>
        /// Gets the snapshot XML.
        /// </summary>
        /// <returns>ownerIdsXml</returns>
        private string GetSnapshotXML()
        {
            //// to get the ownerid's XML String to create system snapshotid.
            this.ownerIdsXml = string.Empty;
            DataTable tempDataTable = new DataTable();
            DataColumn tempDataTableColumn = new DataColumn("KeyID");
            tempDataTable.Columns.Add("KeyID");

            for (int item = 0; item < this.OwnerStatusDataGridView.Rows.Count; item++)
            {
                DataRow tempDataRow = tempDataTable.NewRow();
                tempDataRow[0] = this.OwnerStatusDataGridView.Rows[item].Cells[this.ownerStatusData.OwnerStatusDetailsTable.OwnerIDColumn.ColumnName].Value;
                tempDataTable.Rows.Add(tempDataRow);
            }

            this.ownerIdsXml = TerraScanCommon.GetXmlString(tempDataTable);

            return this.ownerIdsXml;
        }
        #endregion Methods

        #region Events
        /// <summary>
        /// F9102 form load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F9102_Load(object sender, EventArgs e)
        {
            this.CancelButton = this.OwnerCloseButton;
            try
            {
                this.LoadOwnerStatusGrid();
                this.OwnerCloseButton.TabStop = false;
                this.SetTitleText();
                this.OwnerStatusDataGridView.Focus();
                this.OwnerStatusDataGridView.Rows[0].Selected = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event for OwnerCloseButton_Click
        /// </summary>
        /// /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event for OwnerStatusDataGridView_CellFormatting
        /// </summary>
        /// /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerStatusDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.OwnerStatusDataGridView.Rows[e.RowIndex].Cells["OwnerID"].Value.ToString()))
                {
                    if (this.OwnerStatusDataGridView.Rows[e.RowIndex].Cells["Priority"].Value.ToString() == "True")
                    {
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["Owner"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Red;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["Status"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Red;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["BeginDate"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Red;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["EndDate"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Red;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["Note"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["Owner"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Black;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["Status"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Black;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["BeginDate"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Black;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["EndDate"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Black;
                        (OwnerStatusDataGridView.Rows[e.RowIndex].Cells["Note"] as DataGridViewTextBoxCell).Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// LinkClickedEvent for OwnerStatusLinkLabel
        /// </summary>
        /// /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerStatusLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Sets the SystemSnapShot Status in the State <91000SystemSnapShotLoaded>
                this.form9102Control.WorkItem.RootWorkItem.State["91000SystemSnapShotLoaded"] = true;

                LoadSystemSnapShotDetails currentLoadSystemSnapShotDetails;
                currentLoadSystemSnapShotDetails.MasterFormNO = this.masterFormNo;
                currentLoadSystemSnapShotDetails.RecordsetType = 1;
                currentLoadSystemSnapShotDetails.IsSystemSnapShotLoaded = true;
                currentLoadSystemSnapShotDetails.KeyIdColumnName = this.ownerStatusData.OwnerStatusDetailsTable.OwnerIDColumn.ColumnName;
                currentLoadSystemSnapShotDetails.SnapShotXML = this.GetSnapshotXML();
                //this.OnD9030_F9033_SetSystemSnapshotEvent(new TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails>(currentLoadSystemSnapShotDetails));
                this.systemSnapShotId = this.form9102Control.WorkItem.F9033_InsertSnapShotItems(TerraScanCommon.UserId, this.GetSnapshotXML());
                this.form9102Control.WorkItem.RootWorkItem.State["91000SystemSnapShotId"] = this.systemSnapShotId;
                //For bugid--278 Added by Malliga on 18/6/2008
                //if (!string.IsNullOrEmpty(this.OwnerStatusDataGridView.Rows[0].Cells["OwnerID"].Value.ToString()))
                //{
                //    this.ownerId = int.Parse(this.OwnerStatusDataGridView.Rows[0].Cells["OwnerID"].Value.ToString());
                //}

                // To Open SystemSnapShot Records in the corresponding Form-master
                if (this.systemSnapShotId > 0)
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(91000);
                    // this.OnD9030_F9033_SetSystemSnapshotEvent(new TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails>(currentLoadSystemSnapShotDetails));
                    formInfo.optionalParameters = new object[1];
                    //For bugid--278 Added by Malliga on 18/6/2008
                    //if (this.systemSnapShotId != 0)
                    //{
                    formInfo.optionalParameters[0] = this.systemSnapShotId;
                    //}
                    //else
                    //{
                    //    formInfo.optionalParameters[0] = this.ownerId;
                    //}

                    // Holds the SystemSnapshotId in the state variable <91000SystemSnapShotId>
                    this.form9102Control.WorkItem.RootWorkItem.State["91000SystemSnapShotId"] = this.systemSnapShotId;

                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                    this.form9102Control.WorkItem.RootWorkItem.State["91000SystemSnapShotId"] = null;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Events

        /// <summary>
        /// Handles the Leave event of the OwnerStatusLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerStatusLinkLabel_Leave(object sender, EventArgs e)
        {
            this.OwnerStatusDataGridView.Rows[0].Selected = true;
            this.OwnerStatusDataGridView.CurrentCell = this.OwnerStatusDataGridView.Rows[0].Cells[2];
            this.OwnerStatusDataGridView.Focus();
        }

        /// <summary>
        /// Handles the Leave event of the OwnerStatusDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OwnerStatusDataGridView_Leave(object sender, EventArgs e)
        {
            if (this.ownerStatusData.TitleTable.Rows.Count > this.OwnerStatusDataGridView.Rows.Count) 
            {
                this.OwnerStatusDataGridView.Rows[this.ownerStatusData.TitleTable.Rows.Count].Selected = false;
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the OwnerStatusDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void OwnerStatusDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex == 5)
            //{
            //    //this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.Sort = "Convert(DateTime,BeginDate,106) ASC";
            //    //OwnerStatusDataGridView.Columns[4].SortMode = DataGridViewColumnSortMode.Programmatic;
            //    this.ownerStatusData.OwnerStatusDetailsTable.Columns.Add("datenew", Type.GetType("System.DateTime"));
            //    this.ownerStatusData.OwnerStatusDetailsTable.Columns["datenew"].Expression = "BeginDate";
            //    this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.Sort = "datenew " + strSortOrder;
            //    this.ownerStatusData.OwnerStatusDetailsTable.Columns.Remove("datenew");
            //    if (strSortOrder == "ASC")
            //    {
            //        OwnerStatusDataGridView.Columns[5].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            //        strSortOrder = "DESC";
            //    }
            //    else
            //    {
            //        OwnerStatusDataGridView.Columns[5].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            //        strSortOrder = "ASC";
            //    }
            //}
            //////else
            //////{
            //////    this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.ApplyDefaultSort = true;
            //////}
            //else if (e.ColumnIndex == 4)
            //{
            //    this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.Sort = "OwnerStatusType " + strSortOrder;

            //    if (strSortOrder == "ASC")
            //    {
            //        OwnerStatusDataGridView.Columns[4].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            //        strSortOrder = "DESC";
            //    }
            //    else
            //    {
            //        OwnerStatusDataGridView.Columns[4].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            //        strSortOrder = "ASC";
            //    }
            //}
            //else if (e.ColumnIndex == 2)
            //{
            //    this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.Sort = "OwnerName " + strSortOrder;

            //    if (strSortOrder == "ASC")
            //    {
            //        OwnerStatusDataGridView.Columns[2].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            //        strSortOrder = "DESC";
            //    }
            //    else
            //    {
            //        OwnerStatusDataGridView.Columns[2].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            //        strSortOrder = "ASC";
            //    }
            //}
            //else if (e.ColumnIndex == 6)
            //{
            //    this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.Sort = "EndDate " + strSortOrder;

            //    if (strSortOrder == "ASC")
            //    {
            //        OwnerStatusDataGridView.Columns[6].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            //        strSortOrder = "DESC";
            //    }
            //    else
            //    {
            //        OwnerStatusDataGridView.Columns[6].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            //        strSortOrder = "ASC";
            //    }
            //}
            //else if (e.ColumnIndex == 7)
            //{
            //    this.ownerStatusData.OwnerStatusDetailsTable.DefaultView.Sort = "Note " + strSortOrder;

            //    if (strSortOrder == "ASC")
            //    {
            //        OwnerStatusDataGridView.Columns[7].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            //        strSortOrder = "DESC";
            //    }
            //    else
            //    {
            //        OwnerStatusDataGridView.Columns[7].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            //        strSortOrder = "ASC";
            //    }
            //}
        }
    }
}