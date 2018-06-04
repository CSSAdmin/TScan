//--------------------------------------------------------------------------------------------
// <copyright file="F9070.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9070.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D9070
{
    #region NameSpace
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
    using System.Drawing.Printing;
    using Infragistics.Win;
    using TerraScan.Helper;
    using System.IO;
    using System.Diagnostics;
    #endregion NameSpace

    /// <summary>
    /// F9070 UserControl class
    /// </summary>
    [SmartPart]
    public partial class F9070 : BaseSmartPart
    {
        #region PrivateMembers

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// userId value
        /// </summary>
        private int userId;

        /// <summary>
        /// variable for F9070ReportListingData
        /// </summary>
        private F9070ReportListingData form9070ReportListingData;

        /// <summary>
        /// Instance for F9070Controller
        /// </summary>
        private F9070Controller form9070Control;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int redColor;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// Local variable.
        /// </summary>
        private string sectionIndicatorTabText;

        /// <summary>
        /// row selected for preview
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Created string for current tempFilePath
        /// </summary>
        private string tempFilePath = string.Empty;

        /// <summary>
        ///  Used to Create Reports
        /// </summary>
        private static TerraScan.Common.Reports.Report reportView = new TerraScan.Common.Reports.Report();

        #endregion PrivateMembers

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9070"/> class.
        /// </summary>
        public F9070()
        {
            InitializeComponent();
            this.ReportListingpicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReportListingpicturebox.Height, this.ReportListingpicturebox.Width, "Reports", 28, 81, 128);
            this.form9070ReportListingData = new F9070ReportListingData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9070"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F9070(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ReportListingpicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReportListingpicturebox.Height, this.ReportListingpicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            this.form9070ReportListingData = new F9070ReportListingData();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9070"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F9070(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ReportListingpicturebox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReportListingpicturebox.Height, this.ReportListingpicturebox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            this.form9070ReportListingData = new F9070ReportListingData();
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion Event Publication

        #region Properties
        /// <summary>
        /// Gets or sets the Form9070Control.
        /// </summary>
        /// <value>The Form9070Control.</value>
        [CreateNew]
        public F9070Controller Form9070Control
        {
            get { return this.form9070Control as F9070Controller; }
            set { this.form9070Control = value; }
        }
        #endregion Properties

        #region Event Subscription

        #region Report

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                ////    TerraScanCommon.SetDataGridViewPosition(this.ReportDetailsDataGridView, this.selectedRow);

                ////    this.Cursor = Cursors.WaitCursor;

                ////    //// Calling the Common Function for Report
                ////    Hashtable ht = new Hashtable();
                ////    ht.Add("UserId", this.userId);
                ////    if (this.ReportDetailsDataGridView.SelectedRows.Count > 0)
                ////    {
                ////        ////TerraScanCommon.ShowReport(Convert.ToInt32(this.ReportDetailsDataGridView.SelectedRows[0].Cells[0].Value), TerraScan.Common.Reports.Report.ReportType.Preview);
                ////        TerraScanCommon.ShowReport(900801, TerraScan.Common.Reports.Report.ReportType.Preview, ht);
                ////    }
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

        #endregion Report

        #endregion Event Subscription

        #region Events

        /// <summary>
        /// Handles the Load event of the F9070 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9070_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                this.LoadReportListingDetailsGrid();
                this.ReportListingGrid.Focus();

                // Set focus on first row in child band
                if (this.ReportListingGrid.Rows.Count > 0)
                {
                    if (this.ReportListingGrid.Rows[0].ChildBands.Count > 0)
                    {
                        this.ReportListingGrid.Rows[0].ChildBands[0].Rows[0].Activate();
                        this.ReportListingGrid.Rows[0].ChildBands[0].Rows[0].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (this.ReportListingGrid.ActiveRow != null)
                {
                    int reportNumberId;
                    int.TryParse(this.ReportListingGrid.ActiveRow.Cells[0].Value.ToString(), out reportNumberId);

                    // Calling the Common Function for Report
                    TerraScanCommon.ShowReport(reportNumberId, TerraScan.Common.Reports.Report.ReportType.Preview);
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
        /*
        /// <summary>
        /// Handles the RowEnter event of the ReportListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReportListingGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.selectedRow = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowLeave event of the ReportListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReportListingGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TerraScanCommon.SetDataGridViewPosition(this.ReportListingGridView, this.selectedRow);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
       
        /// <summary>
        /// Handles the CellDoubleClick event of the ReportListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ReportListingGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            int reportNumberID = (Convert.ToInt32(this.ReportListingGridView.SelectedRows[0].Cells[0].Value.ToString()));
            try
            {
                if ((e.RowIndex < this.form9070ReportListingData.F9070GetReportListing.Rows.Count) && (e.RowIndex > -1))
                {
                   // TerraScanCommon.SetDataGridViewPosition(this.ReportListingGridView, this.selectedRow);
                    this.Cursor = Cursors.WaitCursor;

                    //// Calling the Common Function for Report
                    if (this.ReportListingGridView.SelectedRows.Count > 0)
                    {
                        ////TerraScanCommon.ShowReport(Convert.ToInt32(this.ReportDetailsDataGridView.SelectedRows[0].Cells[0].Value), TerraScan.Common.Reports.Report.ReportType.Preview);
                        TerraScanCommon.ShowReport(reportNumberID, TerraScan.Common.Reports.Report.ReportType.Preview);
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
        }*/

        #endregion Events

        #region Private methods
        /*
        /// <summary>
        /// Customizes the report details grid.
        /// </summary>
        private void CustomizeReportListingGridView()
        {
                this.ReportListingGridView.AllowUserToResizeColumns = false;
                this.ReportListingGridView.AllowUserToResizeRows = false;
                this.ReportListingGridView.AutoGenerateColumns = false;
                this.ReportListingGridView.StandardTab = true;
                this.ReportListingGridView.Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].DataPropertyName = this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName;
                this.ReportListingGridView.Columns[this.form9070ReportListingData.F9070GetReportListing.NameColumn.ColumnName].DataPropertyName = this.form9070ReportListingData.F9070GetReportListing.NameColumn.ColumnName;
                this.ReportListingGridView.Columns[this.form9070ReportListingData.F9070GetReportListing.DescriptionColumn.ColumnName].DataPropertyName = this.form9070ReportListingData.F9070GetReportListing.DescriptionColumn.ColumnName;
                this.ReportListingGridView.Columns[this.form9070ReportListingData.F9070GetReportListing.ReportIDColumn.ColumnName].DataPropertyName = this.form9070ReportListingData.F9070GetReportListing.ReportIDColumn.ColumnName;                            
        }
        */
        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            //////Load FormHeaderSmartPart to FormHeaderSmartPartdeckWorkspace
            if (this.form9070Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderSmartPartdeckWorkspace.Show(this.form9070Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderSmartPartdeckWorkspace.Show(this.form9070Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }
            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Report Listing", string.Empty }));
        }

        /// <summary>
        /// To Load ReportListing Details Grid
        /// </summary>
        private void LoadReportListingDetailsGrid()
        {
            DataColumn[] primaryKeys = new DataColumn[1];
            primaryKeys[0] = this.form9070ReportListingData.ReportHeader.GroupIDColumn;
            this.form9070ReportListingData.ReportHeader.PrimaryKey = primaryKeys;
            this.form9070ReportListingData = this.form9070Control.WorkItem.F9070GetReportListingDetails(TerraScanCommon.UserId);

            this.form9070ReportListingData.Relations.Add(this.form9070ReportListingData.ReportHeader.Columns[this.form9070ReportListingData.ReportHeader.GroupIDColumn.ColumnName], this.form9070ReportListingData.F9070GetReportListing.Columns[this.form9070ReportListingData.F9070GetReportListing.GroupIDColumn.ColumnName]);

            this.ReportListingGrid.DataMember = this.form9070ReportListingData.ReportHeader.TableName;
            this.ReportListingGrid.DataSource = this.form9070ReportListingData;

            if (this.form9070ReportListingData.F9070GetReportListing.Rows.Count > 0)
            {
                this.PreviewReportButton.Enabled = true;
            }
            else
            {
                this.PreviewReportButton.Enabled = false;
            }            
        }

        #region Customize Grid
        private void CustomizeGrid()
        {
            this.ReportListingGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.ReportListingGrid.DisplayLayout.InterBandSpacing = 0;

            //// To make Band[1] Column Header Visible False
            this.ReportListingGrid.DisplayLayout.Bands[1].ColHeadersVisible = false;
            this.ReportListingGrid.DisplayLayout.Bands[0].ColHeadersVisible = false;

            // Hide Row selectors for Band[0]
            this.ReportListingGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.False;

            //// Make the Row Indentation to left Corner
            this.ReportListingGrid.DisplayLayout.Bands[0].Indentation = -2;
            this.ReportListingGrid.DisplayLayout.Bands[1].Indentation = -2;

            //// Change the Row selector Apprance for Band[1] and hide the row selector of band[0]
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowSelectorHeaderAppearance.BorderColor = Color.Black;
            this.ReportListingGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.ThemedElementAlpha = Alpha.Default;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.SeparateElement;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowSelectorHeaderAppearance.BorderColor2 = Color.Black;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BackColor = Color.FromArgb(192, 192, 192);

            // To avoid dark line of border 
            this.ReportListingGrid.DisplayLayout.Bands[0].Override.BorderStyleRow = UIElementBorderStyle.None;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.BorderStyleRow = UIElementBorderStyle.None;

            // Font size for band[1]
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.Name = "Arial";
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowAppearance.FontData.SizeInPoints = 8F;

            //// Change the Row RowAppearance
            this.ReportListingGrid.DisplayLayout.Bands[0].Override.RowAppearance.BackColor = Color.FromArgb(31, 65, 103);
            this.ReportListingGrid.DisplayLayout.Bands[0].Override.RowAppearance.ForeColor = Color.White;
            this.ReportListingGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.SizeInPoints = 10;
            this.ReportListingGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.Bold = DefaultableBoolean.True;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowAppearance.BackColor = Color.FromArgb(217, 217, 217);
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowAppearance.ForeColor = Color.Black;

            //// Band[1] Row Alternate apperance
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor2 = Color.White;

            this.ReportListingGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

            //// Row selected appearance changed to white
            this.ReportListingGrid.DisplayLayout.Bands[0].Override.SelectedCellAppearance.ForeColor = Color.White;
            this.ReportListingGrid.DisplayLayout.Bands[1].Override.SelectedCellAppearance.ForeColor = Color.White;

            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.TitleColumn.ColumnName].Header.VisiblePosition = 0;
            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.GroupIDColumn.ColumnName].Hidden = true;
            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.BackColorColumn.ColumnName].Hidden = true;
            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.ForecolorColumn.ColumnName].Hidden = true;
            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.GroupOrderColumn.ColumnName].Hidden = true;
            

            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.NameColumn.ColumnName].Header.VisiblePosition = 0;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.DescriptionColumn.ColumnName].Header.VisiblePosition = 1;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Header.VisiblePosition = 2;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportIDColumn.ColumnName].Hidden = true;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.IsHelpColumn.ColumnName].Hidden = true;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.GroupIDColumn.ColumnName].Hidden = true;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportFileColumn.ColumnName].Hidden = true;

            // Style
            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.TitleColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.NameColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;

            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].CellAppearance.FontData.Underline = DefaultableBoolean.True;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;

            // Make the cell becomes non editable
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            // Align Header Text
            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.TitleColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.ReportListingGrid.DisplayLayout.Bands[0].Columns[this.form9070ReportListingData.ReportHeader.TitleColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;

            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.NameColumn.ColumnName].Width = 218;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.DescriptionColumn.ColumnName].Width = 400;
            this.ReportListingGrid.DisplayLayout.Bands[1].Columns[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Width = 80;

            //Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
            //appearance111.BorderColor = System.Drawing.Color.Black;
            //this.ReportListingGrid.DisplayLayout.Override.CellAppearance = appearance111;

            // Empty Row settings
            this.ReportListingGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
            this.ReportListingGrid.DisplayLayout.EmptyRowSettings.Style = Infragistics.Win.UltraWinGrid.EmptyRowStyle.AlignWithDataRows;
            this.ReportListingGrid.DisplayLayout.EmptyRowSettings.RowAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ReportListingGrid.DisplayLayout.EmptyRowSettings.CellAppearance.BorderColor = Color.Black;
            this.ReportListingGrid.DisplayLayout.EmptyRowSettings.EmptyAreaAppearance.BorderColor = Color.Black;
            this.ReportListingGrid.DisplayLayout.EmptyRowSettings.EmptyAreaAppearance.BackColor = Color.Black;
            this.ReportListingGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;

            // For full row selection
            this.ReportListingGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
        }
        #endregion Customize Grid

        #region ExpandAllParentRows

        /// <summary>
        /// Expands all parent rows.
        /// </summary>
        /// <param name="row">The row.</param>
        private void ExpandAllParentRows(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            this.ReportListingGrid.ActiveRow = row;
            this.ReportListingGrid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExpandRow, false, false);
            this.ReportListingGrid.DisplayLayout.Rows[row.Index].ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.ReportListingGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
        }

        #endregion ExpandAllParentRows

        #region DeactivateHyperLink

        /// <summary>
        /// Deactivates the hyper link.
        /// </summary>
        /// <param name="row">The row.</param>
        private void DeactivateHyperLink(Infragistics.Win.UltraWinGrid.UltraGridRow row, bool isActive)
        {
            // Checks for the isHelp column value, 
            // If FORM value is NULL, then there will be NO hyperlink and it will be consider as
            // an Formatted Text Column
            if (isActive)
            {
                //row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                //row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.True;
                //row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Appearance.Cursor = Cursors.Hand;

                row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].EditorControl = this.ReportFormattedTextEditor;
                row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].EditorControl.Cursor = Cursors.Hand;

            }
            else
            {

                row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
                row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.False;
                row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportNumberColumn.ColumnName].Appearance.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Changing Band color

        private Color GetColor(string backColor, Color defaultColor)
        {
            string[] backColorArr = null;
            int redColor;
            int greenColor;
            int blueColor;
            char[] splitChar = { ',' };
            backColorArr = backColor.Split(splitChar);
            if (backColorArr.Length.Equals(3))
            {
                ////Getting Red Color
                if (string.IsNullOrEmpty(backColorArr[0]))
                {
                    redColor = 255;
                }
                else
                {
                    int.TryParse(backColorArr[0], out redColor);
                }

                ////Getting Green Color
                if (string.IsNullOrEmpty(backColorArr[1]))
                {
                    greenColor = 255;
                }
                else
                {
                    int.TryParse(backColorArr[1], out greenColor);
                }

                ////Getting Blue Color
                if (string.IsNullOrEmpty(backColorArr[2]))
                {
                    blueColor = 255;
                }
                else
                {
                    int.TryParse(backColorArr[2], out blueColor);
                }

                ////Assign RGB value to form backcolor
                if (redColor < 0 || redColor > 255 || greenColor < 0 || greenColor > 255 || blueColor < 0 || blueColor > 255)
                {
                    redColor = 255;
                    greenColor = 255;
                    blueColor = 255;
                }

                return Color.FromArgb(redColor, greenColor, blueColor);
            }

            if (!string.IsNullOrEmpty(defaultColor.ToString()))
            {
                return defaultColor;
            }

            return Color.FromArgb(255, 255, 255);
        }

        #endregion Changing Band color

        #region Get File Location

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        private void GetFileName()
        {
            CommentsData configDetailsDataSet = new CommentsData();
            configDetailsDataSet = WSHelper.GetConfigDetails("TS_HelpFiles");
            this.tempFilePath = configDetailsDataSet.GetCommentsConfigDetails.Rows[0][configDetailsDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        #endregion Get File Location

        #endregion Private methods

        #region Grid Events
        /// <summary>
        /// Handles the InitializeLayout event of the ReportListingGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void ReportListingGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
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
        /// Handles the InitializeRow event of the ReportListingGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void ReportListingGrid_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row.Band.Index.Equals(0))
                {
                    // Expand all Parent Rows
                    this.ExpandAllParentRows(e.Row);

                    e.Row.Height = 36;
                    // Change BackGround color based on 'BackColorColumn' value
                    if (e.Row.Cells[this.form9070ReportListingData.ReportHeader.BackColorColumn.ColumnName].Value != null
                        && !string.IsNullOrEmpty(e.Row.Cells[this.form9070ReportListingData.ReportHeader.BackColorColumn.ColumnName].Value.ToString()))
                    {
                        e.Row.Appearance.BackColor = this.GetColor(e.Row.Cells[this.form9070ReportListingData.ReportHeader.BackColorColumn.ColumnName].Value.ToString(), Color.FromArgb(31, 65, 103));
                        //e.Row.Appearance.BorderColor = Color.Black;
                    }

                    // Change ForeGround color based on 'ForecolorColumn' value
                    if (e.Row.Cells[this.form9070ReportListingData.ReportHeader.ForecolorColumn.ColumnName].Value != null
                        && !string.IsNullOrEmpty(e.Row.Cells[this.form9070ReportListingData.ReportHeader.ForecolorColumn.ColumnName].Value.ToString()))
                    {
                        e.Row.Appearance.ForeColor = this.GetColor(e.Row.Cells[this.form9070ReportListingData.ReportHeader.ForecolorColumn.ColumnName].Value.ToString(), Color.Black);
                    }
                }
                else
                {
                    // Remove hyperlink for 'ReportNumber' column based on 'IsHelp' column value
                    if (e.Row.Cells[this.form9070ReportListingData.F9070GetReportListing.IsHelpColumn.ColumnName].Value != null
                         && !string.IsNullOrEmpty(e.Row.Cells[this.form9070ReportListingData.F9070GetReportListing.IsHelpColumn.ColumnName].Value.ToString())
                        && bool.Parse(e.Row.Cells[this.form9070ReportListingData.F9070GetReportListing.IsHelpColumn.ColumnName].Value.ToString()).Equals(false))
                    {
                        this.DeactivateHyperLink(e.Row, false);
                    }
                    else
                    {
                        this.DeactivateHyperLink(e.Row, true);
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

        private void ReportFormattedTextEditor_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
        {
            try
            {
                // Show report 
                HelpEngineData reportList = new HelpEngineData();
                reportList = WSHelper.ListHelpDocumentForm(e.LinkText.Trim());

                Process process = new Process();

                if (reportList.ListHelpDocumentForm.Rows.Count > 0)
                {
                    this.GetFileName();
                    HelpEngineData.ListHelpDocumentFormRow reportDocumentRow = (HelpEngineData.ListHelpDocumentFormRow)reportList.ListHelpDocumentForm.Rows[0];

                    if (reportDocumentRow.IsFile)
                    {
                        if (!string.IsNullOrEmpty(this.tempFilePath))
                        {
                            if (!this.tempFilePath.EndsWith(@"\"))
                            {
                                this.tempFilePath = this.tempFilePath + @"\";
                            }

                            FileInfo fileInfo = new FileInfo(this.tempFilePath + reportDocumentRow.FileName);
                            if (fileInfo.Exists)
                            {
                                process.StartInfo.FileName = this.tempFilePath + reportDocumentRow.FileName;
                                process.StartInfo.UseShellExecute = true;
                                process.Start();
                            }
                            else
                            {
                                MessageBox.Show("Specified file/path does not exist.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Could not access the shared location.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        process.StartInfo.FileName = "iexplore";
                        process.StartInfo.Arguments = reportDocumentRow.FileName;
                        process.Start();
                    }
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FileAccessDenied"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FileAccessDenied"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Win32Exception)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FileAccessDenied"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ReportListingGrid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (e.Row.Band.Index > 0 && e.Row.Index > -1)
                {
                    if (e.Row.Index < this.form9070ReportListingData.F9070GetReportListing.Rows.Count)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        int reportNumberId;
                        int.TryParse(e.Row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportIDColumn.ColumnName].Value.ToString(), out reportNumberId);

                        TerraScanCommon.ShowReportPreview(reportNumberId, TerraScan.Common.Reports.Report.ReportType.Preview
                            , e.Row.Cells[this.form9070ReportListingData.F9070GetReportListing.ReportFileColumn.ColumnName].Value.ToString()
                            , e.Row.Cells[this.form9070ReportListingData.F9070GetReportListing.DescriptionColumn.ColumnName].Value.ToString());
                        //// Calling the Common Function for Report
                       // TerraScanCommon.ShowReport(reportNumberId, TerraScan.Common.Reports.Report.ReportType.Preview);
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

        #endregion Grid Events
    }
}
