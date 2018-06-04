//----------------------------------------------------------------------------------
// <copyright file="F8904.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8904.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			        Author		       Description
// ----------		    ---------		   -----------------------------------------
// 18 Oct 06            VINOTH             Created
// 08 Dec 06            JAYANTHI           Modified
// 05 Jan 06            VijayaKumar        Modified/Added For  OnD9030_F9030_LoadSliceDetails and LoadGrid()
//*********************************************************************************/

namespace D8900
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.BusinessEntities;

    #endregion

    /// <summary>
    /// F8904 Class
    /// </summary>
    public partial class F8904 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        ///  used to Convert Color From string
        /// </summary>
        private ColorConverter colorConv = new ColorConverter();

        /// <summary>
        /// tabText Local Variable
        /// </summary>
        private string tabText;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int workId;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /////// <summary>
        /////// formMasterPermissionEdit Local variable.
        /////// </summary>
        ////private bool formMasterPermissionEdit;

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F8904Controller form8904Control;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /////// <summary>
        /////// PageMode Local variable.
        /////// </summary>
        ////private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// getEventGrid Local variable.
        /// </summary>
        private F8904EventGridData.GetEventGridDataTable getEventGrid = new F8904EventGridData.GetEventGridDataTable();

        /// <summary>
        /// flag to identify form load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// To store Red Color Code
        /// </summary>
        private int redCode;

        /// <summary>
        /// To store Green Color Code
        /// </summary>
        private int greenCode;

        /// <summary>
        /// To store blue Color Code
        /// </summary>
        private int blueCode;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        private int tableRowCount;


        #endregion

        #region Constructor

        /// <summary>
        /// F8904 Constructor
        /// </summary>
        public F8904()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8052"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8904(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.workId = keyID;
            this.masterFormNo = masterform;
            /////Added by Jayanthi 
            this.ParentFormId = formNo;
            ////this.formMasterPermissionEdit = permissionEdit;
            this.tabText = tabText;
            this.flagFormLoad = false;
            this.redCode = red;
            this.greenCode = green;
            this.blueCode = blue;
            this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(218, 42, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8904 control.
        /// </summary>
        /// <value>The F8904 control.</value>
        [CreateNew]
        public F8904Controller Form8904Control
        {
            get { return this.form8904Control as F8904Controller; }
            set { this.form8904Control = value; }
        }
        #endregion

        #region EventSubscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.masterFormNo)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = "D8900.F8904";
                ////this.Height = this.GridPanel.Height + this.panel2.Height;
                sliceResize.SliceFormHeight = this.GridPanel.Height + this.panel2.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                }
                
                if (this.tableRowCount > 0)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                     //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.GridPanel.Enabled = false;
                    this.panel2.Enabled = false;
                    this.panel4.Enabled = false;
                }
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
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.GridPanel.Enabled = true;
            this.panel2.Enabled = true;
            this.panel4.Enabled = true;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.workId = eventArgs.Data.SelectedKeyId;
                    this.LoadGrid();
                }
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region FormResize

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

        #endregion

        #region Load

        /// <summary>
        /// F8904 Load
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8904_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadGrid();
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the MouseEnter event of the StatementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.DistrictInfoSecIndicatorToolTip.SetToolTip(this.DistrictInfoSecIndicatorPictureBox, "D8900.F8904");
        }

        /// <summary>
        /// Handles the Click event of the StatementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D8900.F8904"));
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the EventGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void EventGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.EventGridView.Columns["EventType"].Index)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (e.RowIndex >= 0)
                    {
                        object[] optionalParameter = new object[] { this.EventGridView.Rows[e.RowIndex].Cells[this.getEventGrid.EventTypeIDColumn.ColumnName].Value, this.EventGridView.Rows[e.RowIndex].Cells[this.getEventGrid.EventIDColumn.ColumnName].Value };
                        Form workOrderEngine = new Form();
                        workOrderEngine = this.form8904Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9002, optionalParameter, this.form8904Control.WorkItem);
                        if (workOrderEngine != null)
                        {
                            workOrderEngine.ShowDialog();
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
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the EventGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EventGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {             
            try
            {
                if (e.ColumnIndex == this.EventGridView.Columns[this.getEventGrid.StatusColorColumn.ColumnName].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    else
                    {
                        if (this.getEventGrid.Rows.Count > 1)
                        {
                            if (!string.IsNullOrEmpty(this.getEventGrid.Rows[e.RowIndex][1].ToString()))
                            {
                                ////string s = getEventGrid.Rows[e.RowIndex][1].ToString();
                                e.CellStyle.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromString(this.getEventGrid.Rows[e.RowIndex][1].ToString());
                                DataGridViewCell cell = this.EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                cell.ToolTipText = this.EventGridView.Rows[e.RowIndex].Cells[4].Value.ToString();                               
                            }
                            else
                            {
                                if (e.RowIndex % 2 == 0)
                                {
                                    e.CellStyle.BackColor = Color.White;
                                }
                                else
                                {
                                    e.CellStyle.BackColor = Color.FromArgb(230, 230, 230);
                                }
                            }
                        }
                    }
                }

                if (e.ColumnIndex == this.EventGridView.Columns["EventType"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (this.EventGridView.Rows[e.RowIndex].Selected || this.EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.White;
                        (EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.Blue;
                        (EventGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }

                if (!string.IsNullOrEmpty(this.EventGridView.Rows[e.RowIndex].Cells[this.getEventGrid.EventDateColumn.ColumnName].Value.ToString()))
                {
                    if (Convert.ToDateTime(this.EventGridView.Rows[e.RowIndex].Cells[this.getEventGrid.EventDateColumn.ColumnName].Value.ToString()) > DateTime.Now)
                    {
                        EventGridView.Rows[e.RowIndex].Cells[this.getEventGrid.EventDateColumn.ColumnName].Style.BackColor = Color.FromArgb(252, 215, 116);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion        

        #region Private Methods

        private void LoadGrid()
        {
            this.FlagSliceForm = true;
            this.CustimizeGrid();
            this.flagFormLoad = true;
            this.PopulateGrid();
            ////this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagFormLoad = false;
        }

        #region Customize DataGrid

        /// <summary>
        /// Custimize Grid
        /// </summary>
        private void CustimizeGrid()
        {
            this.EventGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.EventGridView.Columns;

            columns[this.getEventGrid.EventTypeIDColumn.ColumnName].DataPropertyName = this.getEventGrid.EventTypeIDColumn.ColumnName;
            columns[this.getEventGrid.EventTypeColumn.ColumnName].DataPropertyName = this.getEventGrid.EventTypeColumn.ColumnName;
            columns[this.getEventGrid.EventDateColumn.ColumnName].DataPropertyName = this.getEventGrid.EventDateColumn.ColumnName;
            columns[this.getEventGrid.StatusColorColumn.ColumnName].DataPropertyName = this.getEventGrid.TempColorColumn.ColumnName;
            columns[this.getEventGrid.StatusColumn.ColumnName].DataPropertyName = this.getEventGrid.StatusColumn.ColumnName;
            columns[this.getEventGrid.IsCompleteColumn.ColumnName].DataPropertyName = this.getEventGrid.IsCompleteColumn.ColumnName;
            columns[this.getEventGrid.EventIDColumn.ColumnName].DataPropertyName = this.getEventGrid.EventIDColumn.ColumnName;

            columns[this.getEventGrid.EventTypeIDColumn.ColumnName].DisplayIndex = 0;
            columns[this.getEventGrid.EventTypeColumn.ColumnName].DisplayIndex = 1;
            columns[this.getEventGrid.EventDateColumn.ColumnName].DisplayIndex = 2;
            columns[this.getEventGrid.StatusColorColumn.ColumnName].DisplayIndex = 3;
            columns[this.getEventGrid.StatusColumn.ColumnName].DisplayIndex = 4;
            columns[this.getEventGrid.IsCompleteColumn.ColumnName].DisplayIndex = 5;
            columns[this.getEventGrid.EventIDColumn.ColumnName].DisplayIndex = 6;            
        }

        #endregion

        #region PopulateGrid

        /// <summary>
        /// Populates the grid.
        /// </summary>
        private void PopulateGrid()
        {
            this.getEventGrid = this.form8904Control.WorkItem.GetEventGridDetails(this.workId);
            this.tableRowCount = this.getEventGrid.Rows.Count;
            this.SetSmartPartHeight(this.getEventGrid.Rows.Count);

            if (this.getEventGrid.Rows.Count > 8)  
            {
                this.EventGridVScroll.Visible = false;
            }
            else
            {
                this.EventGridVScroll.Visible = true;
            }

            this.EventGridView.DataSource = this.getEventGrid;
            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = "D8900.F8904";
            sliceResize.SliceFormHeight = this.DistrictInfoSecIndicatorPictureBox.Height;
            if (!this.flagFormLoad)
            {
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, 42, this.tabText, this.redCode, this.greenCode, this.blueCode);
            }

            if (this.EventGridView.OriginalRowCount <= 0)
            {
                this.GridPanel.Enabled = false;
                this.EventGridView.Enabled = false;
            }
            else
            {
                this.GridPanel.Enabled = true;
                this.EventGridView.Enabled = true;
            }
        }

        #endregion

        #region SetSmartPartHeight

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 4)
            {
                if (recordCount > 8)
                {
                    recordCount = 8;
                }

                // Increments empty rows for the Specified recordCount
                int increment = ((recordCount - 4) * 22);

                // Increases the GridHeight, 
                // PanelHeight as per the empty rows,
                // ScrollBar Height and SectionIndicator Height

                this.EventGridView.Height = 109 + increment;
                this.GridPanel.Height = this.EventGridView.Height;
                this.EventGridVScroll.Height = 109 + increment - 1;
                this.DistrictInfoSecIndicatorPictureBox.Height = GridPanel.Height + panel4.Height - 4; ////147 + increment
                this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, 42, this.tabText, this.redCode, this.greenCode, this.blueCode);
                this.panel2.Top = this.GridPanel.Height - 4;
                this.panel4.Top = this.panel2.Top;
                this.EventGridView.NumRowsVisible = recordCount;
            }
            else
             {
                this.EventGridView.Height = 109;
                this.GridPanel.Height = this.EventGridView.Height;
                this.EventGridVScroll.Height = this.EventGridView.Height - 2;
                this.panel2.Top = this.GridPanel.Height - 1;
                this.panel4.Top = this.panel2.Top;
                this.DistrictInfoSecIndicatorPictureBox.Height = GridPanel.Height + panel4.Height - 1;
                this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, 42, this.tabText, this.redCode, this.greenCode, this.blueCode);
                this.EventGridView.NumRowsVisible = 4;
            }
        }

        #endregion

        #endregion
    }
}
