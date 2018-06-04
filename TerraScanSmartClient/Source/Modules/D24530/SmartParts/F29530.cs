//--------------------------------------------------------------------------------------------
// <copyright file="F29530.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29530 FS Land Codes.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/09/2007       Sriparameswari              Created
//***********************************************************************************************/

namespace D24530
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
    using TerraScan.Utilities;
    using Infrastructure.Interface;

    /// <summary>
    /// F29530
    /// </summary>
    [SmartPart]
    public partial class F29530 : BaseSmartPart
    {
        #region  Variable
        /// <summary>
        /// controller F36032
        /// </summary>
        private F29530Controller form29530Control;

        /// <summary>
        /// Used to store the gdocEventHeaderDataTableRowCount
        /// </summary>
        private int gdocEventHeaderDataTableRowCount;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Used to store the statementId(keyid)
        /// </summary>
        private int eventId;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// F29530EventAssociationData
        /// </summary>
        private F29530EventAssociationData eventData = new F29530EventAssociationData();

        /// <summary>
        /// associationEventCount
        /// </summary>
        /// 
        private int associationEventCount;

        /// <summary>
        /// Instance For F9600SearchData Dataset
        /// </summary>
        private F29530EventAssociationData.ListEventAssociationTableDataTable eventDataCollection = new F29530EventAssociationData.ListEventAssociationTableDataTable();

        /// <summary>
        /// Instance for GDocEventHeaderData
        /// </summary>
        private GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29530"/> class.
        /// </summary>
        public F29530()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29530(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.eventId = keyID;
            this.AssociationEventGridpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociationEventGridpictureBox.Height, this.AssociationEventGridpictureBox.Width, string.Empty, 28, 81, 128);
        }

        #endregion Constructor

        #region Event Publication
        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form36032 control.
        /// </summary>
        /// <value>The form36032 control.</value>
        [CreateNew]
        public F29530Controller Form29530Control
        {
            get { return this.form29530Control as F29530Controller; }
            set { this.form29530Control = value; }
        }

        #endregion Property

        #region Event Subscription

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

                if (this.associationEventCount > 0)
                {
                    // this.AssociationEventsGridView.Rows[0].Selected = true;
                    TerraScanCommon.SetDataGridViewPosition(this.AssociationEventsGridView, 0);
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
                    this.LoadAssociationEventGrid();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        #endregion

        #region Protected methods
        #endregion

        #region Methods

        #region LoadAssociationEventGrid

        /// <summary>
        /// LoadAssociationEventGrid()
        /// </summary>
        private void LoadAssociationEventGrid()
        {
            this.eventData.ListEventAssociationTable.Clear();
            this.eventData = this.form29530Control.WorkItem.F29530_FillAssociationEventGrid(this.eventId);
            this.associationEventCount = this.eventData.ListEventAssociationTable.Rows.Count;
            if (this.associationEventCount > 0)
            {
                this.LinkText.DataPropertyName = this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName;
                this.Description.DataPropertyName = this.eventData.ListEventAssociationTable.DescriptionColumn.ColumnName;
                this.Form.DataPropertyName = this.eventData.ListEventAssociationTable.FormColumn.ColumnName;
                this.Param1.DataPropertyName = this.eventData.ListEventAssociationTable.Param1Column.ColumnName;
                this.Param2.DataPropertyName = this.eventData.ListEventAssociationTable.Param2Column.ColumnName;
                this.Param3.DataPropertyName = this.eventData.ListEventAssociationTable.Param3Column.ColumnName;
                this.AssociationID.DataPropertyName = this.eventData.ListEventAssociationTable.AssociationIDColumn.ColumnName;
                this.AssociationEventsGridView.AutoGenerateColumns = false;
                if (this.associationEventCount == 1)
                {
                    this.AssociationEventsGridView.NumRowsVisible = 1;

                    //// Coding added for the issue 4497.if we do Crtl+F and do some some searches then height
                    //// should not change.
                    if (this.AssociationEventsGridView.Height != 46)
                    {
                        this.AssociationEventsGridView.Height = this.AssociationEventsGridView.Height - 23;
                    }
                    this.CIDPanel.Height = this.AssociationEventsGridView.Height;
                    this.AssociationEventGridpictureBox.Height = this.AssociationEventsGridView.Height;
                    this.AssociationEventGridVscrollBar.Height = this.AssociationEventsGridView.Height;
                }

                this.AssociationEventsGridView.DataSource = this.eventData.ListEventAssociationTable;
                this.AssociationEventsGridView.Focus();
                this.AssociationEventsGridView.Rows[0].Selected = true;
                TerraScanCommon.SetDataGridViewPosition(this.AssociationEventsGridView, 0);

                if (this.eventData.ListEventAssociationTable.Rows.Count > this.AssociationEventsGridView.NumRowsVisible)
                {
                    this.AssociationEventGridVscrollBar.Visible = false;
                }
                else
                {
                    this.AssociationEventGridVscrollBar.Enabled = false;
                    this.AssociationEventGridVscrollBar.Visible = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// F29530_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F29530_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadAssociationEventGrid();
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

        #endregion

        #region Events

        /// <summary>
        /// AssociationEventsGridView_CellContentClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AssociationEventsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    if (this.AssociationEventsGridView.Rows.Count > 0)
                    {
                        string tempvalue = this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.FormColumn.ColumnName].Value.ToString();
                        FormInfo getForm = TerraScan.Common.TerraScanCommon.GetFormInfo(Convert.ToInt32(this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.FormColumn.ColumnName].Value.ToString()));
                        getForm.optionalParameters = new object[2];
                        int opp1;
                        int opp2;
                        int opp3;

                        if (!string.IsNullOrEmpty(this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventDataCollection.Param1Column.ColumnName].Value.ToString()))
                        {
                            int.TryParse(this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventDataCollection.Param1Column.ColumnName].Value.ToString(), out opp1);
                            getForm.optionalParameters[0] = opp1;
                        }
                        else
                        {
                            getForm.optionalParameters[0] = null;
                        }

                        if (!string.IsNullOrEmpty(this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventDataCollection.Param2Column.ColumnName].Value.ToString()))
                        {
                            int.TryParse(this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventDataCollection.Param2Column.ColumnName].Value.ToString(), out opp2);
                            getForm.optionalParameters[1] = opp2;
                        }
                        else
                        {
                            getForm.optionalParameters[1] = null;
                        }

                        if (!string.IsNullOrEmpty(this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventDataCollection.Param3Column.ColumnName].Value.ToString()))
                        {
                            int.TryParse(this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventDataCollection.Param3Column.ColumnName].Value.ToString(), out opp3);
                            ////getForm.optionalParameters[2] = opp3;
                        }
                        else
                        {
                            //// getForm.optionalParameters[2] = null;
                        }

                        this.ShowForm(this, new DataEventArgs<FormInfo>(getForm));
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
        }

        /// <summary>
        ///  AssociationEventsGridView_CellFormatting
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AssociationEventsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            this.ParcelNumberLinkForeColor(e);
        }

        /// <summary>
        /// Parcels the color of the number link fore.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberLinkForeColor(DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.AssociationEventsGridView.Columns[this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName].Index)
            {
                if (this.AssociationEventsGridView.Rows[e.RowIndex].Selected || this.AssociationEventsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                {
                    (this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.White;
                    (this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                    (this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                }
                else
                {
                    (this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.Blue;
                    (this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                    (this.AssociationEventsGridView.Rows[e.RowIndex].Cells[this.eventData.ListEventAssociationTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// AssociationEventGridpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AssociationEventGridpictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// AssociationEventGridpictureBox_MouseHover
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AssociationEventGridpictureBox_MouseHover(object sender, EventArgs e)
        {
            this.EventAssociationToolTip.SetToolTip(this.AssociationEventGridpictureBox, Utility.GetFormNameSpace(this.Name));
        }

        #endregion Events
    }
}
