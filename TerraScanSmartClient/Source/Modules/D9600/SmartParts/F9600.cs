//--------------------------------------------------------------------------------------------
// <copyright file="F9600.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the SearchEngine.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 NOV 06		VINOTH             CREATED
// 24 NOV 06        GUHAN              INCULDED APPCONFIG VALUES
// 09 DEC 07        JAYANTHI           ROW SELECTION REMOVED AND ARROW KEYS MADE TO 
//                                     WORK FOR CELL NAVIGATION AND CELL SELECTION INSERTED
// 04 OCT 07        VINOTH             APPLIED EXTENDED GRAPHICS
//***********************************************************************************************/

namespace D9600
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;    
    using TerraScan.BusinessEntities;    
    using Infragistics.Win.UltraWinGrid;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using Infragistics.Win;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
        
    /// <summary>
    /// T2SearchEngine Class
    /// </summary>
    [SmartPart]
    public partial class F9600 : BaseSmartPart
    {
        #region MemberVariable
        /// <summary>
        /// Created Controller for F9600Controller
        /// </summary>
        private F9600Controller form9600Controller;

        /// <summary>
        /// Instance For F9600SearchData Dataset
        /// </summary>
        private F9600SearchData.ListSearchTableDataTable searchData = new F9600SearchData.ListSearchTableDataTable();

        /// <summary>
        /// To store Column Index
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// To store the SearchButton Hitcount
        /// </summary>
        private int hitCount;

        /// <summary>
        /// To Store the SortIndicator of Group Column
        /// </summary>
        private SortIndicator groupSearchIndicator;

        /// <summary>
        /// To store the SortIndicator of Search Column
        /// </summary>
        private SortIndicator searchSearchIndicator;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];        

        /// <summary>
        /// Sets The Message for ToolTip
        /// </summary>
        private string toolTipMessage = string.Empty;

        /// <summary>
        /// to Stop the Filling Data
        /// </summary>
        private bool stopFillData;

        /// <summary>
        /// string format
        /// </summary>
        private string textFormat = "#,###";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9600"/> class.
        /// </summary>
        public F9600()
        {
            this.InitializeComponent();
            this.InitSchema();
            this.SearchResultPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SearchResultPictureBox.Height, this.SearchResultPictureBox.Width, "Search Result", 28, 81, 128);
        }

        #endregion

        #region EventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F9600 Controll.
        /// </summary>
        /// <value>The F9015 Controll.</value>
        [CreateNew]
        public F9600Controller F9600Controll
        {
            get { return this.form9600Controller as F9600Controller; }
            set { this.form9600Controller = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F9600 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9600_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.SearchTextBox.Focus();
                this.SearchMenu.Visible = false;
                this.PreviewButton.Enabled = false;
                this.ExportExcelButton.Enabled = false;
                this.RecordsCountLabel.Text = "0 Rows";
                ////this.SearchMenu.Click += new EventHandler(this.SearchButton_Click);
                this.SearchResultGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }  
        }

        #region Form Resize

        #region IssueFix : Bug #780: 9600 Tab Stretches label

        /// <summary>
        /// Handles the Resize event of the F9600 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F9600_Resize(object sender, EventArgs e)
        {
            this.SearchResultPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SearchResultPictureBox.Height, this.SearchResultPictureBox.Width, "Search Result", 28, 81, 128);
        }

        #endregion IssueFix

        #endregion Form Resize

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////if (SearchMenu.Pressed)
                ////{
                ////    this.SearchTextBox.Focus();
                ////    this.SearchTextBox.SelectAll();
                ////}

                this.hitCount = 0;
                this.SearchResultGrid.DataSource = null;
                this.RecordsCountLabel.Text = this.SearchResultGrid.Rows.Count.ToString(this.textFormat) + " Rows";

                if (!string.IsNullOrEmpty(this.SearchTextBox.Text.Trim()))
                {
                    ////this.searchData = this.F9600Controll.WorkItem.F9600_ListSearchResult(this.SearchTextBox.Text.Trim(), 1);
                    this.FillData(false, false);
                    if (this.searchData.Rows.Count > 0)
                    {
                        this.groupSearchIndicator = SortIndicator.Ascending;
                        this.searchSearchIndicator = SortIndicator.Ascending;
                        ////this.SearchResultGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;                        
                        this.SearchResultGrid.Focus();
                        this.SearchResultGrid.DisplayLayout.Override.TipStyleCell = TipStyle.Show;
                        this.SearchResultGrid.Rows[0].Cells[this.searchData.GroupColumn.ColumnName].Selected = true;
                        this.SearchResultGrid.Rows[0].Cells[this.searchData.GroupColumn.ColumnName].Activate();                        
                    }
                    else
                    {
                        this.SearchResultGrid.DataSource = null;
                        this.RecordsCountLabel.Text = "0 Rows";

                        this.SearchDataSource.Rows.Clear();
                        this.PreviewButton.Enabled = false;
                        this.ExportExcelButton.Enabled = false;
                        this.SearchResultGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
                    }
                }
                else if (this.SearchResultGrid.Rows.Count.Equals(0))
                {
                    this.SearchResultGrid.DataSource = null;
                    this.RecordsCountLabel.Text = "0 Rows";

                    this.PreviewButton.Enabled = false;
                    this.ExportExcelButton.Enabled = false;
                    this.SearchResultGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
                    MessageBox.Show(SharedFunctions.GetResourceString("F9600SearchTextValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.SearchTextBox.Focus();
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
        /// Handles the InitializeLayout event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                // Reorder the columns.
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].Header.VisiblePosition = 0;
                e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].Header.VisiblePosition = 1;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].Header.VisiblePosition = 2;

                // Hide the Extra Columns.
                e.Layout.Bands[0].Columns[this.searchData.GroupIDColumn.ColumnName].Hidden = true;
                e.Layout.Bands[0].Columns[this.searchData.FormColumn.ColumnName].Hidden = true;
                e.Layout.Bands[0].Columns[this.searchData.MyKeyIDColumn.ColumnName].Hidden = true;

                // Set the Column Height
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].Width = 150;
                e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].Width = 220;

                // Set the Group Column Font
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 10.25f;
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].CellAppearance.TextTrimming = TextTrimming.EllipsisWord;
                e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].CellAppearance.TextTrimming = TextTrimming.EllipsisCharacterWithLineLimit;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].CellAppearance.TextTrimming = TextTrimming.EllipsisCharacterWithLineLimit;

                // Header Appearance
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].Header.Appearance.BackColor = Color.FromArgb(128, 128, 128);
                e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].Header.Appearance.BackColor = Color.FromArgb(128, 128, 128);
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].Header.Appearance.BackColor = Color.FromArgb(128, 128, 128);

                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                e.Layout.Bands[0].Columns[this.searchData.GroupIDColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
                e.Layout.Bands[0].Columns[this.searchData.GroupIDColumn.ColumnName].Header.Appearance.TextVAlign = VAlign.Middle;
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].Header.Appearance.TextVAlign = VAlign.Middle;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].Header.Appearance.TextVAlign = VAlign.Middle;

                // MERGED CELL FUNCTIONALITY RELATED ULTRAGRID SETTINGS
                // ------------------------------------------------------------------------
                // Set the merged cell style to the Always.
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].MergedCellStyle = MergedCellStyle.Always;
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].MergedCellContentArea = MergedCellContentArea.VirtualRect;

                e.Layout.Bands[0].Columns[this.searchData.GroupIDColumn.ColumnName].MergedCellStyle = MergedCellStyle.Always;
                e.Layout.Bands[0].Columns[this.searchData.GroupIDColumn.ColumnName].MergedCellContentArea = MergedCellContentArea.VirtualRect;

                // Activate Cell Select
                ////e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].CellClickAction = CellClickAction.CellSelect;
                e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].CellClickAction = CellClickAction.CellSelect;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].CellClickAction = CellClickAction.CellSelect;
                e.Layout.Bands[0].Columns[this.searchData.GroupIDColumn.ColumnName].CellClickAction = CellClickAction.CellSelect;
                e.Layout.Bands[0].Columns[this.searchData.FormColumn.ColumnName].CellClickAction = CellClickAction.CellSelect;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].CellClickAction = CellClickAction.CellSelect;

                e.Layout.Bands[0].SortedColumns.Tag = SortIndicator.Ascending;
                this.SearchResultGrid.DisplayLayout.Bands[0].SortedColumns.Tag = SortIndicator.Ascending;

                this.SearchResultGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;

                ////// Set the Sorting style.
                ////e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].SortIndicator = SortIndicator.Ascending;
                ////e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].SortIndicator = SortIndicator.Ascending;
                ////e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].SortIndicator = SortIndicator.Disabled;

                // Set the Text HAlign to Left
                e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;

                // Set the Text VAlign to Right
                e.Layout.Bands[0].Columns[this.searchData.SearchColumn.ColumnName].CellAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
                e.Layout.Bands[0].Columns[this.searchData.SupplementColumn.ColumnName].CellAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
                e.Layout.Bands[0].Columns[this.searchData.GroupColumn.ColumnName].CellAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;

                // this.SearchResultGrid.Rows[0].Cells[this.searchData.ListSearchTable.GroupColumn.ColumnName].Tag = SortIndicator.Descending;
                this.SearchResultGrid.Rows[0].Cells[this.searchData.GroupColumn.ColumnName].Appearance.BackColor = Color.FromArgb(206, 223, 173);

                // Set the Row Selector Style
                this.SearchResultGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.SearchResultGrid.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.SeparateElement;
                this.SearchResultGrid.DisplayLayout.Override.RowSelectorHeaderAppearance.BackColor = Color.FromArgb(132, 130, 132);

                ////e.Layout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            }            
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }  
        }

        /// <summary>
        /// Handles the Click event of the ExportExcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExportExcelButton_Click(object sender, EventArgs e)
        {
            // Modified Export logic

            try
            {
                if (this.SearchResultGrid.Rows.Count > 0)
                {
                    string strMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                    // Sets the Current Folder path to MyDocuments\TerraScan
                    DirectoryInfo dirInfo = new DirectoryInfo(strMyDocuments + @"\TerraScan");
                    int fileIndex = 1;
                    string filename = "TS9600_" + fileIndex + ".XLS";

                    // Checks Whether TerraScan Folder Exists in the MyDocument Folder
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }

                    // Gets the set of files available in the current directory                    

                    FileInfo[] fileArray = dirInfo.GetFiles();
                    filename = filename.ToUpper();
                    if (fileArray.Length != 0)
                    {
                        ArrayList filenames = new ArrayList();
                        for (int i = 0; i < fileArray.Length; i++)
                        {
                            // if a file is an xml file, add it to the arraylist
                            if (fileArray[i].Name.EndsWith(".XLS"))
                            {
                                filenames.Add(fileArray[i].Name);
                            }
                        }

                        // Iterates whether the given filename exists in the current folder
                        for (int j = 0; j < filenames.Count; j++)
                        {
                            if (filenames.Contains(filename))
                            {
                                // If Exists, change the filename
                                fileIndex++;
                                filename = "TS9600_" + fileIndex + ".XLS";
                                filename = filename.ToUpper();
                            }
                        }
                    }

                    filename = dirInfo.ToString() + "\\" + filename;
                    this.stopFillData = true;
                    this.ExcelExporter.Export(this.SearchResultGrid, filename);
                    
                    // Opens the Grid Details in the SpreadSheet
                    Process excel = new Process();
                    excel.StartInfo.Arguments = "\"" + dirInfo.ToString() + "\" /e";
                    excel.StartInfo.FileName = filename;
                    excel.Start();
                    this.stopFillData = false;
                }
            }
            catch (IOException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SearchResultGrid.Rows.Count > 0)
                {
                    this.stopFillData = true;
                    this.SearchPreviewDialog.ShowDialog(this);
                    this.stopFillData = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DoubleClickCell event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
         {
             try
             {
                 if (this.SearchResultGrid.Rows.Count > 0)
                 {
                     // Check for the Cell with the Merge property
                     if (e.Cell.IsMergedWith(this.SearchResultGrid.Rows[e.Cell.Row.Index].Cells[this.searchData.GroupColumn.ColumnName]))
                     {
                         object[] optionalParameter = null;
                         if (this.searchData.Count > 0)
                         {
                             optionalParameter = new object[] { this.SearchResultGrid.Rows[e.Cell.Row.Index].Cells[this.searchData.FormColumn.Ordinal].Value, this.SearchResultGrid.Rows[e.Cell.Row.Index].Cells[this.searchData.GroupColumn.Ordinal].Value };
                         }

                         Form snapShot = new Form();
                         snapShot = this.form9600Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9601, optionalParameter, this.form9600Controller.WorkItem);
                         if (snapShot != null)
                         {
                             snapShot.ShowDialog();
                         }
                     }
                     else if (e.Cell.IsMergedWith(this.SearchResultGrid.Rows[e.Cell.Row.Index].Cells[this.searchData.SearchColumn.ColumnName]) || e.Cell.IsMergedWith(this.SearchResultGrid.Rows[e.Cell.Row.Index].Cells[this.searchData.SupplementColumn.ColumnName]))
                     {
                         if (this.searchData.Count > 0)
                         {
                             FormInfo getForm = TerraScan.Common.TerraScanCommon.GetFormInfo(Convert.ToInt32(this.SearchResultGrid.Rows[e.Cell.Row.Index].Cells[this.searchData.FormColumn.Ordinal].Value));
                             getForm.optionalParameters = new object[1];
                             getForm.optionalParameters[0] = Convert.ToInt64(this.SearchResultGrid.Rows[e.Cell.Row.Index].Cells[this.searchData.MyKeyIDColumn.Ordinal].Value);
                             this.ShowForm(this, new DataEventArgs<FormInfo>(getForm));
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
        }

        /// <summary>
        /// Handles the InitializeRow event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                long temp;

                if (this.SearchResultGrid.Rows.Count > 0)
                {
                    temp = Convert.ToInt64(this.SearchResultGrid.Rows[e.Row.Index].Cells[this.searchData.GroupIDColumn.ColumnName].Value.ToString());

                    // set back color that we might want to show in the Group Column
                    if (temp % 2 == 0)
                    {
                        this.SearchResultGrid.Rows[e.Row.Index].Cells[this.searchData.GroupColumn.ColumnName].Appearance.BackColor = Color.FromArgb(206, 219, 239);
                    }
                    else
                    {
                        this.SearchResultGrid.Rows[e.Row.Index].Cells[this.searchData.GroupColumn.ColumnName].Appearance.BackColor = Color.FromArgb(206, 223, 173);
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
        /// Handles the MouseEnterElement event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UIElementEventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_MouseEnterElement(object sender, UIElementEventArgs e)
        {
            try
            {
                if (e.Element != null)
                {
                    UltraGridColumn col = e.Element.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;
                    UltraGridRow row = e.Element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
                    if (e.Element.GetType().Equals(typeof(Infragistics.Win.UltraWinGrid.MergedCellUIElement)))
                    {
                        this.toolTipMessage = this.SearchResultGrid.Rows[row.Index].Cells[this.searchData.GroupColumn.ColumnName].Value.ToString();
                        this.SearchEngineToolTip.SetToolTip(SearchResultGrid, this.toolTipMessage);
                    }
                    else
                    {
                        if (col != null && row != null)
                        {
                            ////msg = this.ultraGrid1.Rows[row.Index].Cells[col.Index].Value.ToString();
                            ////GdocEventEngineToolTip.SetToolTip(ultraGrid1, msg);
                            this.SearchResultGrid.DisplayLayout.Override.TipStyleCell = TipStyle.Show;
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
        }

        /// <summary>
        /// Handles the MouseLeaveElement event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UIElementEventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_MouseLeaveElement(object sender, UIElementEventArgs e)
        {
            try
            {
                this.SearchEngineToolTip.SetToolTip(SearchResultGrid, string.Empty);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_Click(object sender, EventArgs e)
        {
            try
            {
                MouseEventArgs e1 = (MouseEventArgs)e;

                Infragistics.Win.UIElement elementUi;

                Point mousePoint = new Point(e1.X, e1.Y);

                // Cast the sender into an UltraGrid 
                // Get the Mouse Point
                UIElement element = ((UltraGrid)sender).DisplayLayout.UIElement.ElementFromPoint(mousePoint);

                elementUi = this.SearchResultGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e1.X, e1.Y));

                if (element == null)
                {
                    return;
                }

                // Try to get a Column from the element
                UltraGridColumn col = elementUi.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;

                // Gets the Header Element
                HeaderUIElement headerElement = element.GetAncestor(typeof(HeaderUIElement)) as HeaderUIElement;

                if (headerElement != null)
                {
                    this.hitCount++;

                    this.columnIndex = col.Index;

                    if (this.hitCount > 1)
                    {
                        if (this.columnIndex.Equals(2))
                        {
                            if (this.groupSearchIndicator.Equals(SortIndicator.Ascending))
                            {
                                if (this.searchSearchIndicator.Equals(SortIndicator.Ascending))
                                {
                                    this.groupSearchIndicator = SortIndicator.Descending;
                                    this.searchSearchIndicator = SortIndicator.Ascending;
                                }
                                else
                                {
                                    this.groupSearchIndicator = SortIndicator.Descending;
                                    this.searchSearchIndicator = SortIndicator.Descending;
                                }
                            }
                            else
                            {
                                if (this.searchSearchIndicator.Equals(SortIndicator.Ascending))
                                {
                                    this.groupSearchIndicator = SortIndicator.Ascending;
                                    this.searchSearchIndicator = SortIndicator.Ascending;
                                }
                                else
                                {
                                    this.groupSearchIndicator = SortIndicator.Ascending;
                                    this.searchSearchIndicator = SortIndicator.Descending;
                                }
                            }
                        }
                        else if (this.columnIndex.Equals(4) || this.columnIndex.Equals(5))
                        {
                            if (this.groupSearchIndicator.Equals(SortIndicator.Ascending))
                            {
                                if (this.searchSearchIndicator.Equals(SortIndicator.Ascending))
                                {
                                    this.groupSearchIndicator = SortIndicator.Ascending;
                                    this.searchSearchIndicator = SortIndicator.Descending;
                                }
                                else
                                {
                                    this.groupSearchIndicator = SortIndicator.Ascending;
                                    this.searchSearchIndicator = SortIndicator.Ascending;
                                }
                            }
                            else
                            {
                                if (this.searchSearchIndicator.Equals(SortIndicator.Ascending))
                                {
                                    this.groupSearchIndicator = SortIndicator.Descending;
                                    this.searchSearchIndicator = SortIndicator.Descending;
                                }
                                else
                                {
                                    this.groupSearchIndicator = SortIndicator.Descending;
                                    this.searchSearchIndicator = SortIndicator.Ascending;
                                }
                            }
                        }
                    }

                    if (this.SearchResultGrid.ActiveCell != null)
                    {
                        this.SearchResultGrid.ActiveCell.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        /// <summary>
        /// Handles the AfterSortChange event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BandEventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_AfterSortChange(object sender, BandEventArgs e)
        {
            try
            {
                if (this.stopFillData.Equals(false))
                {
                    if (this.groupSearchIndicator.Equals(SortIndicator.Ascending))
                    {
                        if (this.searchSearchIndicator.Equals(SortIndicator.Ascending))
                        {
                            this.FillData(false, false);
                        }
                        else
                        {
                            this.FillData(false, true);
                        }
                    }
                    else
                    {
                        if (this.searchSearchIndicator.Equals(SortIndicator.Ascending))
                        {
                            this.FillData(true, false);
                        }
                        else
                        {
                            this.FillData(true, true);
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
        }        

        /// <summary>
        /// Handles the Load event of the SearchPreviewDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchPreviewDialog_Load(object sender, EventArgs e)
        {
            try
            {
                // Loads the Document Name
                this.SearchPreviewDialog.Document.DocumentName = "Preview";
                this.SearchPreviewDialog.Text = "Preview";
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the F9600 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void F9600_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    this.SearchButton_Click(sender, null);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the SearchTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter))
                {
                    if (this.SearchTextBox.Text.Length > 2)
                    {
                        this.SearchButton_Click(sender, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Row selectors are blocked. If a row selector is clicked, Makes the first cell of that row to be selected
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SearchResultGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            try
            {
                if (this.SearchResultGrid.ActiveRow != null && this.SearchResultGrid.ActiveRow.Selected)
                {
                    this.SearchResultGrid.Rows[SearchResultGrid.ActiveRow.Index].Cells[this.searchData.GroupColumn.ColumnName].Selected = true;
                    this.SearchResultGrid.Rows[SearchResultGrid.ActiveRow.Index].Cells[this.searchData.GroupColumn.ColumnName].Activate();
                    this.SearchResultGrid.ActiveRow.Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the SearchResultGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void SearchResultGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (this.SearchResultGrid.ActiveCell != null)
                    {
                        // Checks for the Empty Row
                        if (this.SearchResultGrid.Rows.Count > 0 && !this.SearchResultGrid.ActiveRow.Selected && !this.SearchResultGrid.Rows[SearchResultGrid.ActiveCell.Row.Index].Selected && SearchResultGrid.ActiveCell.Column.Index.Equals(2))
                        {
                            object[] optionalParameter = null;
                            if (this.searchData.Count > 0)
                            {
                                optionalParameter = new object[] { this.SearchResultGrid.Rows[SearchResultGrid.ActiveCell.Row.Index].Cells[this.searchData.FormColumn.Ordinal].Value, this.SearchResultGrid.Rows[SearchResultGrid.ActiveCell.Row.Index].Cells[this.searchData.GroupColumn.Ordinal].Value };
                            }

                            Form snapShot = new Form();
                            snapShot = this.form9600Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9601, optionalParameter, this.form9600Controller.WorkItem);
                            if (snapShot != null)
                            {
                                snapShot.ShowDialog();
                            }
                        }
                        else if (SearchResultGrid.ActiveCell.Column.Index.Equals(4) || SearchResultGrid.ActiveCell.Column.Index.Equals(5))
                        {
                            FormInfo getForm = TerraScan.Common.TerraScanCommon.GetFormInfo(Convert.ToInt32(this.SearchResultGrid.Rows[SearchResultGrid.ActiveCell.Row.Index].Cells[this.searchData.FormColumn.Ordinal].Value));
                            getForm.optionalParameters = new object[1];
                            getForm.optionalParameters[0] = Convert.ToInt64(this.SearchResultGrid.Rows[SearchResultGrid.ActiveCell.Row.Index].Cells[this.searchData.MyKeyIDColumn.Ordinal].Value);
                            this.ShowForm(this, new DataEventArgs<FormInfo>(getForm));
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
        }        

        /// <summary>
        /// Handles the Click event of the SearchMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SearchResultGrid.Focused)
                {
                    this.SearchTextBox.Focus();
                    this.SearchTextBox.SelectAll();
                }
                else if (this.SearchTextBox.Focused)
                {
                    this.SearchTextBox.SelectAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region TextBoxChange Event

        /// <summary>
        /// Handles the TextChanged event of the SearchTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.SearchTextBox.Text.Trim().Length > 2)
            {
                this.SearchButton.Enabled = true;
            }
            else
            {
                this.SearchButton.Enabled = false;
            }
        }

        #endregion TextBoxChange Event

        #region Private Methods

        #region IntiSchema

        /// <summary>
        /// Inits the schema.
        /// </summary>
        private void InitSchema()
        {
            try
            {
                // The columns show on the subclassed component
                // and we work with a strongly typed structure

                this.SearchDataSource.Band.Key = this.searchData.GroupIDColumn.ColumnName;
                this.SearchDataSource.Band.Columns.Add(this.searchData.GroupColumn.ColumnName).ReadOnly = Infragistics.Win.DefaultableBoolean.True;
                this.SearchDataSource.Band.Columns.Add(this.searchData.GroupIDColumn.ColumnName).ReadOnly = Infragistics.Win.DefaultableBoolean.True;
                this.SearchDataSource.Band.Columns.Add(this.searchData.SearchColumn.ColumnName).ReadOnly = Infragistics.Win.DefaultableBoolean.True;
                this.SearchDataSource.Band.Columns.Add(this.searchData.SupplementColumn.ColumnName).ReadOnly = Infragistics.Win.DefaultableBoolean.True;
                this.SearchDataSource.Band.Columns.Add(this.searchData.MyKeyIDColumn.ColumnName).ReadOnly = Infragistics.Win.DefaultableBoolean.True;
                this.SearchDataSource.Band.Columns.Add(this.searchData.FormColumn.ColumnName).ReadOnly = Infragistics.Win.DefaultableBoolean.True;
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

        #region Fills the WinDataSource

        /// <summary>
        /// Method that Fills the WinDataSource with Data from DB
        /// </summary>
        /// <param name="sortOrder">if set to <c>true</c> [sort order].</param>
        /// <param name="groupOrder">if set to <c>true</c> [group order].</param>
        private void FillData(bool sortOrder, bool groupOrder)
        {           
                this.searchData.Clear();
                ////this.SearchDataSource.Rows.Clear();                 

                // Fetch the result from Database.
                this.searchData = this.F9600Controll.WorkItem.F9600_ListSortResult(this.SearchTextBox.Text.Trim().Replace("'", "''"), TerraScanCommon.ApplicationId, groupOrder, sortOrder);
                if (this.searchData.Rows.Count > 0)
                {
                    this.SearchResultGrid.DataSource = this.searchData;
                    this.RecordsCountLabel.Text = this.SearchResultGrid.Rows.Count.ToString(this.textFormat) + " Rows";
                    
                    // Set the DataSource Count
                    // this.SearchDataSource.Rows.SetCount(this.searchData.ListSearchTable.Rows.Count);
                    this.PreviewButton.Enabled = true;
                    this.ExportExcelButton.Enabled = true;
                }
                else
                {
                    this.SearchResultGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
                    this.SearchResultGrid.DataSource = null;
                    this.RecordsCountLabel.Text = "0 Rows";

                    this.PreviewButton.Enabled = false;
                    this.ExportExcelButton.Enabled = false;
                    MessageBox.Show(SharedFunctions.GetResourceString("F9600RecordsNotFound"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
        }

        #endregion

        #region LoadWorkSpace

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
                // To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
                if (this.form9600Controller.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form9600Controller.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form9600Controller.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                this.formLabelInfo[0] = "Search Engine";
                this.formLabelInfo[1] = string.Empty;

                this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
                this.Cursor = Cursors.Default;
        }
        
        #endregion                       

        #endregion
    }
}
