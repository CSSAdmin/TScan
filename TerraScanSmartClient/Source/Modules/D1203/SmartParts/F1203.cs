
namespace D1203
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Resources;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using System.Globalization;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;
    using System.IO;


    /// <summary>
    /// 
    /// </summary>
    [SmartPart]
    public partial class F1203 : BaseSmartPart
    {
        #region Variable
        /// <summary>
        /// postTypeData
        /// </summary>
        private string postTypeData = string.Empty;

        /// <summary>
        /// form1203Control
        /// </summary>
        private F1203Controller form1203Control;
        F1203DueDateManagementData.f1203_pcget_PostTypeDueDateDataTable DueDatePostDetailsTable = new F1203DueDateManagementData.f1203_pcget_PostTypeDueDateDataTable();

        private string rollYear = string.Empty;

        private string postType = string.Empty;
        private string fieldIndex = string.Empty;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = "M/d/yyyy";

        private int gridHeight = -1;
        /// <summary>
        /// variable holds the CurrentRow Index
        /// </summary>
        private int gridCurrentRowIndex = -1;

        /// <summary>
        /// variable holds the CurrentColumn Index.
        /// </summary>
        private int gridCurrentColumnIndex = -1;

        private int rowIndex;
        private int columnIndex;


        private bool isChanged = false;
        /// <summary>
        /// DatePicker object
        /// </summary>
        private System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F15018 control.
        /// </summary>
        /// <value>The F15018 control.</value>
        [CreateNew]
        public F1203Controller Form1203Control
        {
            get { return this.form1203Control as F1203Controller; }
            set { this.form1203Control = value; }
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion


        public F1203()
        {
            InitializeComponent();           
            this.DueDatePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DueDatePictureBox.Height, this.DueDatePictureBox.Width, "Due Date Management", 28, 81, 128);                        
        }

        #region Private Methods

        private void F1203_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                this.CustomizeGrid();
                this.LoadFormGrid();
                this.ButtonEnable(false);
                this.validDate.CustomFormat = this.dateFormat; //// "m/d/yyyy";
                this.validDate.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
                this.validDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void LoadFormGrid()
        {
            try
            {
                this.DueDateGridView.AutoGenerateColumns = false;
                this.DueDatePostDetailsTable.Clear();
                this.DueDatePostDetailsTable = this.form1203Control.WorkItem.F1203LoadDueDateManagement().f1203_pcget_PostTypeDueDate;
                this.DueDateGridView.DataSource = this.DueDatePostDetailsTable.DefaultView;
                this.DueDateGridView.CurrentRow.Selected = false;
                this.DueDateGridView.CurrentCell.Selected = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void EnableandDisableGridRow()
        {
            if (this.DueDateGridView.Rows.Count > 0)
            {
                for (int i = 0; i < this.DueDateGridView.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.DueDatePostDetailsTable.Rows[i]["IsFirstDueEditable"].ToString()) && this.DueDatePostDetailsTable.Rows[i]["IsFirstDueEditable"].Equals(1))
                    {
                        this.DueDateGridView.Rows[i].Cells["FirstDue"].ReadOnly = false;
                    }
                    else
                    {
                        this.DueDateGridView.Rows[i].Cells["FirstDue"].ReadOnly = true;
                    }
                    if (!string.IsNullOrEmpty(this.DueDatePostDetailsTable.Rows[i]["IsSecondDueEditable"].ToString()) && this.DueDatePostDetailsTable.Rows[i]["IsSecondDueEditable"].Equals(1))
                    {
                        this.DueDateGridView.Rows[i].Cells["SecondDue"].ReadOnly = false;
                    }
                    else
                    {
                        this.DueDateGridView.Rows[i].Cells["SecondDue"].ReadOnly = true;
                    }

                }
            }
        }

        private void ButtonEnable(bool enable)
        {
            this.SaveButton.Enabled = enable;
            this.CancelButton.Enabled = enable;
        }

        private void CustomizeGrid()
        {
            try
            {
                this.DueDateGridView.AutoGenerateColumns = false;
                this.DueDateGridView.StandardTab = false;

                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.PostTypeIDColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.PostTypeIDColumn.ColumnName;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.PostTypeColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.PostTypeColumn.ColumnName;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.RollYearColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.RollYearColumn.ColumnName;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.FirstDueColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.FirstDueColumn.ColumnName;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.IsFirstDueEditableColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.IsFirstDueEditableColumn.ColumnName;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.SecondDueColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.SecondDueColumn.ColumnName;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.IsSecondDueEditableColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.IsSecondDueEditableColumn.ColumnName;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.DefaultGracePeriodColumn.ColumnName].DataPropertyName = this.DueDatePostDetailsTable.DefaultGracePeriodColumn.ColumnName;

                this.DueDateGridView.Columns[0].Visible = false;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.PostTypeColumn.ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.RollYearColumn.ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.FirstDueColumn.ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.SecondDueColumn.ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.DefaultGracePeriodColumn.ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //this.DueDateGridView.AlternatingRowsDefaultCellStyle.
                //this.DueDateGridView.Columns[this.DueDatePostDetailsTable.RollYearColumn.ColumnName].AlternatingRowsDefaultCellStyle
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.RollYearColumn.ColumnName].DefaultCellStyle.ForeColor = Color.Gray;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.PostTypeColumn.ColumnName].DefaultCellStyle.ForeColor = Color.Gray;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.FirstDueColumn.ColumnName].DefaultCellStyle.ForeColor = Color.Black;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.SecondDueColumn.ColumnName].DefaultCellStyle.ForeColor = Color.Black;
                this.DueDateGridView.Columns[this.DueDatePostDetailsTable.DefaultGracePeriodColumn.ColumnName].DefaultCellStyle.ForeColor = Color.Black;

                this.DueDateGridView.DataSource = this.DueDatePostDetailsTable.DefaultView;
                this.DueDateGridView.PrimaryKeyColumnName = this.DueDatePostDetailsTable.PostTypeIDColumn.ColumnName;

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LoadWorkSpaces()
        {
            try
            {
                //////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
                if (this.form1203Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.FormHeaderDeckWorkspace.Show(this.form1203Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.FormHeaderDeckWorkspace.Show(this.form1203Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                ////sets form header
                this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Due Date Management", string.Empty }));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void SaveOeration()
        {
            bool returnValue = this.CheckErrors();
            if (returnValue)
            {
                this.DueDateGridView.CurrentRow.Selected = false;
                int indexValue;
                int.TryParse(this.fieldIndex.ToString(), out indexValue);
                MessageBox.Show(postType + " in " + rollYear + " missing required data in Default Grace Period field", "Terrascan T2 – Missing required data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DueDateGridView.CurrentCell = this.DueDateGridView.Rows[indexValue].Cells["DefaultGracePeriod"];
                this.DueDateGridView.Rows[indexValue].Cells["DefaultGracePeriod"].Selected = true;
            }
            else
            {
                this.DueDatePostDetailsTable.AcceptChanges();
                this.postTypeData = string.Empty;
                this.postTypeData = this.ConvertToXML(this.DueDatePostDetailsTable);
                this.form1203Control.WorkItem.F1203_SaveDueDateManagement(TerraScanCommon.UserId, this.postTypeData);
                this.ButtonEnable(false);
                this.gridCurrentColumnIndex = -1;
                this.LoadFormGrid();
                this.DueDateGridView.CurrentRow.Selected = false;               
            }
        }

        private string ConvertToXML(DataTable table)
        {
            DataTable dt = table.Copy();
            dt.Namespace = "";
            dt.TableName = "f1203_pcget_PostTypeDueDate";
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            xmlstr = xmlstr.Replace("DocumentElement", "root");
            xmlstr = xmlstr.Replace("f1203_pcget_PostTypeDueDate", "Table");
            return (xmlstr);
        }

        private bool CheckErrors()
        {
            this.DueDatePostDetailsTable.AcceptChanges();
            for (int i = 0; i < this.DueDateGridView.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.DueDateGridView.Rows[i].Cells["DefaultGracePeriod"].Value.ToString()))
                {
                    this.rollYear = this.DueDateGridView.Rows[i].Cells["RollYear"].Value.ToString();
                    this.postType = this.DueDateGridView.Rows[i].Cells["PostType"].Value.ToString();
                    this.fieldIndex = i.ToString();
                    this.DueDateGridView.CurrentRow.Selected = false;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Shows the owner date time picker.
        /// </summary>
        private void ShowOwnerDateTimePicker()
        {
            //// Set the date according to the Current Column Index 
            if (!this.gridCurrentRowIndex.Equals(-1) && !this.gridCurrentColumnIndex.Equals(-1))
            {
                if (!string.IsNullOrEmpty(this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value.ToString()))
                {
                    try
                    {
                        this.OwnerDateTimePicker.Value = Convert.ToDateTime(this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value).Date;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    this.OwnerDateTimePicker.Value = DateTime.Now.Date;
                }
            }

            this.OwnerDateTimePicker.Visible = true;
            this.OwnerDateTimePicker.Focus();
            SendKeys.Send("{F4}");
        } 
        #endregion

        #region Button Events

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveOeration();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DueDatePostDetailsTable.Clear();
                this.gridCurrentColumnIndex = -1;
                this.LoadFormGrid();
                this.ButtonEnable(false);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        #endregion

        #region OwnerDate Calendar Events

        /// <summary>
        /// Handles the KeyPress event of the OwnerDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void OwnerDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the OwnerDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                //// Assign the Date to the Current Cell
                if (!this.gridCurrentRowIndex.Equals(-1) && !this.gridCurrentColumnIndex.Equals(-1))
                {
                    if (this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value != null)
                    {
                        if (this.gridCurrentColumnIndex == this.DueDateGridView.Columns["FirstDue"].Index)
                        {
                            if (!string.IsNullOrEmpty(this.OwnerDateTimePicker.Text.ToString()))
                            {
                                this.isChanged = true;
                                this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = Convert.ToDateTime(this.OwnerDateTimePicker.Text);
                                this.ButtonEnable(true);
                            }
                            else
                            {
                                this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = string.Empty;
                            }
                            
                        }
                        
                        else if (this.gridCurrentColumnIndex == this.DueDateGridView.Columns["SecondDue"].Index)
                        {
                            //// Enddate
                            if (!string.IsNullOrEmpty(this.OwnerDateTimePicker.Text.ToString()))
                            {
                                this.isChanged = true;
                                DateTime date = DateTime.Parse(this.OwnerDateTimePicker.Text).Date;
                                this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = date;   //Convert.ToDateTime(this.OwnerDateTimePicker.Text).ToShortDateString();
                               
                                this.ButtonEnable(true);
                            }
                            else
                            {
                                this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = string.Empty;
                            }
                        }
                        //this.DueDateGridView.Focus();

                       // this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Selected = true;
                        this.DueDateGridView.CurrentCell = this.DueDateGridView.Rows[this.gridCurrentRowIndex].Cells[this.gridCurrentColumnIndex];
                    }
                }

                this.OwnerDateTimePicker.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

     
        #endregion

        #region Grid Events

        private void DueDateGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
              
                for (int i = 0; i < this.DueDateGridView.Rows.Count; i++)
                {
                    TerraScanTextAndImageCell FirstDueCell = (TerraScanTextAndImageCell)this.DueDateGridView[this.DueDatePostDetailsTable.FirstDueColumn.ColumnName, i];
                    FirstDueCell.ImagexLocation = 79;
                    FirstDueCell.ImageyLocation = 2;

                    TerraScanTextAndImageCell secondDueCell = (TerraScanTextAndImageCell)this.DueDateGridView[this.DueDatePostDetailsTable.SecondDueColumn.ColumnName, i];

                    secondDueCell.ImagexLocation = 79;
                    secondDueCell.ImageyLocation = 2;

                    if (e.RowIndex == i)
                    {
                        FirstDueCell.Image = Properties.Resources.calendarImage;
                        secondDueCell.Image = Properties.Resources.calendarImage;
                    }
                    else
                    {
                        if (e.RowIndex >= 0)
                        {
                            FirstDueCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DueDateGridView[this.DueDateGridView.Columns["FirstDue"].Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                            secondDueCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DueDateGridView[this.DueDateGridView.Columns["SecondDue"].Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                        }
                    }
                    if (!string.IsNullOrEmpty(this.DueDateGridView.Rows[i].Cells["IsFirstDueEditable"].Value.ToString()) && this.DueDateGridView.Rows[i].Cells["IsFirstDueEditable"].Value.Equals(0))
                   // if (!string.IsNullOrEmpty(this.DueDatePostDetailsTable.Rows[i]["IsFirstDueEditable"].ToString()) && this.DueDatePostDetailsTable.Rows[i]["IsFirstDueEditable"].Equals(0))
                    {
                        if (e.RowIndex == i)
                        {
                            FirstDueCell.Image = Properties.Resources.calendarImage_disable;
                            this.DueDateGridView.Rows[i].Cells["FirstDue"].ReadOnly = true;
                        }
                        else
                        {
                            if (e.RowIndex >= 0)
                            {
                                FirstDueCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DueDateGridView[this.DueDateGridView.Columns["FirstDue"].Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                            }

                        }
                    }
                    if (!string.IsNullOrEmpty(this.DueDateGridView.Rows[i].Cells["IsSecondDueEditable"].Value.ToString()) && this.DueDateGridView.Rows[i].Cells["IsSecondDueEditable"].Value.Equals(0))
                   // if (!string.IsNullOrEmpty(this.DueDatePostDetailsTable.Rows[i]["IsSecondDueEditable"].ToString()) && this.DueDatePostDetailsTable.Rows[i]["IsSecondDueEditable"].Equals(0))
                    {
                        if (e.RowIndex == i)
                        {
                            this.DueDateGridView.Rows[i].Cells["SecondDue"].ReadOnly = true;
                            secondDueCell.Image = Properties.Resources.calendarImage_disable;

                        }
                        else
                        {
                            if (e.RowIndex >= 0)
                            {
                                secondDueCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DueDateGridView[this.DueDateGridView.Columns["FirstDue"].Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
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
           
        private void DueDateGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.gridCurrentRowIndex = e.RowIndex;
                this.gridCurrentColumnIndex = e.ColumnIndex;

                if ((e.ColumnIndex.Equals(this.DueDateGridView.Columns["FirstDue"].Index) || e.ColumnIndex.Equals(this.DueDateGridView.Columns["SecondDue"].Index)) && (e.RowIndex >= 0) && (e.ColumnIndex >= 0))
                {
                    if (!string.IsNullOrEmpty(this.DueDateGridView.Rows[e.RowIndex].Cells["IsFirstDueEditable"].Value.ToString()) && this.DueDateGridView.Rows[e.RowIndex].Cells["IsFirstDueEditable"].Value.Equals(1))
                    {
                        if (!this.DueDateGridView.Rows[e.RowIndex].ReadOnly)
                        {
                            if ((e.X >= 71) && (e.X <= (100 - 4)) && (e.Y >= 3) && (e.Y <= (22 - 3)))
                            {
                                if (e.ColumnIndex.Equals(this.DueDateGridView.Columns["FirstDue"].Index))
                                {
                                    this.OwnerDateTimePicker.Left = this.DueDateGridPanel.Left + this.DueDateGridView.RowHeadersWidth + this.DueDateGridView.Columns[0].Width + 305;
                                    this.OwnerDateTimePicker.Top = this.DueDateGridPanel.Top + this.gridHeight - e.Y; ////+ (22 - e.Y); 

                                    this.gridCurrentColumnIndex = e.ColumnIndex;
                                    this.gridCurrentRowIndex = e.RowIndex;
                                    this.ShowOwnerDateTimePicker();
                                   // this.DueDateGridView.UpdateCellValue(this.DueDateGridView.Columns["FirstDue"].Index, e.RowIndex);
                                }
                                else if (e.ColumnIndex.Equals(this.DueDateGridView.Columns["SecondDue"].Index))
                                {
                                    this.OwnerDateTimePicker.Left = this.DueDateGridPanel.Left + this.DueDateGridView.RowHeadersWidth + this.DueDateGridView.Columns[0].Width + this.DueDateGridView.Columns[1].Width + 200;
                                    this.OwnerDateTimePicker.Top = this.DueDateGridPanel.Top + this.gridHeight - e.Y; // +(22 - e.Y);

                                    this.gridCurrentColumnIndex = e.ColumnIndex;
                                    this.gridCurrentRowIndex = e.RowIndex;
                                    this.ShowOwnerDateTimePicker();
                                    //this.DueDateGridView.UpdateCellValue(this.DueDateGridView.Columns["SecondDue"].Index, e.RowIndex);
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
       

        private void DueDateGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.DueDateGridView.Rows.Count > 0)
                {
                    this.DueDateGridView.Rows[0].Selected = false;
                    //this.DueDateGridView.CurrentCell.Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void DueDateGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < this.DueDateGridView.Rows.Count; i++)
                {
                    TerraScanTextAndImageCell FirstDueCell = (TerraScanTextAndImageCell)this.DueDateGridView[this.DueDatePostDetailsTable.FirstDueColumn.ColumnName, i];
                    FirstDueCell.ImagexLocation = 99;
                    FirstDueCell.ImageyLocation = 2;

                    TerraScanTextAndImageCell secondDueCell = (TerraScanTextAndImageCell)this.DueDateGridView[this.DueDatePostDetailsTable.SecondDueColumn.ColumnName, i];

                    secondDueCell.ImagexLocation = 99;
                    secondDueCell.ImageyLocation = 2;
                    if (e.RowIndex == i)
                    {
                        FirstDueCell.Image = Properties.Resources.calendarImage;
                        secondDueCell.Image = Properties.Resources.calendarImage;
                    }
                    else
                    {
                        if (e.RowIndex >= 0)
                        {
                            FirstDueCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DueDateGridView[this.DueDateGridView.Columns["FirstDue"].Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                            secondDueCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.DueDateGridView[this.DueDateGridView.Columns["SecondDue"].Index, e.RowIndex].InheritedStyle.BackColor.R), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.G), Convert.ToInt32(this.DueDateGridView[0, e.RowIndex].InheritedStyle.BackColor.B));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void DueDateGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(this.DueDateGridView.Columns["FirstDue"].Index) || e.ColumnIndex.Equals(this.DueDateGridView.Columns["SecondDue"].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();

                        try
                        {
                            if (!string.IsNullOrEmpty(tempvalue))
                            {
                                this.DueDateGridView.RefreshEdit();
                                DateTime outDate;
                                DateTime.TryParse(tempvalue, out outDate);
                                this.validDate.Value = outDate;
                                this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = this.validDate.Value.ToString(this.dateFormat);
                            }
                        }
                        catch
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //e.Value = DateTime.Now.ToString(this.dateFormat);
                            if (string.IsNullOrEmpty(this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value.ToString()))
                            {
                                this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = string.Empty;
                            }                          
                           
                        }

                        e.ParsingApplied = true;
                        this.DueDateGridView.RefreshEdit();
                        
                    }
                }
                else if (e.ColumnIndex.Equals(this.DueDateGridView.Columns["DefaultGraceperiod"].Index))
                {
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void DueDateGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void DueDateGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(this.DueDateGridView.Columns["FirstDue"].Index) || e.ColumnIndex.Equals(this.DueDateGridView.Columns["SecondDue"].Index))
                {
                    if (!string.IsNullOrEmpty(this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value.ToString().Trim()))
                    {
                        DateTime tempValue = new DateTime();
                        if (DateTime.TryParse(this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value.ToString().Trim(), null, System.Globalization.DateTimeStyles.RoundtripKind, out tempValue))
                        {
                            try
                            {
                                this.validDate.Value = DateTime.Parse(this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value.ToString());
                                this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = this.validDate.Value.ToString(this.dateFormat);
                            }
                            catch
                            {
                                this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = string.Empty;
                               // MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                               // this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = DateTime.Now.ToString(this.dateFormat);
                            }
                        }
                        else
                        {
                            this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = string.Empty;
                           // MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.validDate.MinDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.validDate.MaxDate.ToShortDateString(), SharedFunctions.GetResourceString("DateValidation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                           // this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = DateTime.Now.ToString(this.dateFormat);
                        }
                    }
                }
                
                if ((e.ColumnIndex.Equals(this.DueDateGridView.Columns["SecondDue"].Index)))
                {
                    DateTime secondDue;
                    
                    if (!String.IsNullOrEmpty(DueDateGridView.Rows[e.RowIndex].Cells[6].Value.ToString()))
                    {
                        DateTime.TryParse(DueDateGridView.Rows[e.RowIndex].Cells[6].Value.ToString(), out secondDue);
                        //this.DueDateGridView[e.ColumnIndex, e.RowIndex].Value = this.DueDateGridView[e.ColumnIndex, e.RowIndex].Value;
                        this.DueDateGridView[e.ColumnIndex, e.RowIndex].Value = secondDue;
                        //this.DueDatePostDetailsTable.Rows[e.RowIndex +1]["SecondDue"] = secondDue;
                       
                    }
                }
                else if (e.ColumnIndex.Equals(4))
                {
                    if (!String.IsNullOrEmpty(DueDateGridView.Rows[e.RowIndex].Cells[4].Value.ToString()))
                    {                       
                        DateTime firstDue;
                        DateTime.TryParse(DueDateGridView.Rows[e.RowIndex].Cells[4].Value.ToString(), out firstDue);
                        this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = DueDateGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                    }
                }
                else if (e.ColumnIndex.Equals(this.DueDateGridView.Columns["DefaultGraceperiod"].Index))
                {
                    if (!String.IsNullOrEmpty(DueDateGridView.Rows[e.RowIndex].Cells["DefaultGraceperiod"].Value.ToString()))
                    {
                        int gracePeriod;
                        int.TryParse(DueDateGridView.Rows[e.RowIndex].Cells["DefaultGraceperiod"].Value.ToString(), out gracePeriod);
                        this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = DueDateGridView.Rows[e.RowIndex].Cells["DefaultGraceperiod"].Value.ToString();
                    }
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void DueDateGridView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.gridHeight = e.Y;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }



        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string dd = sender.ToString();             
               
                ////to enable the save cancel button in form master
                this.ButtonEnable(true);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.DueDateGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void DueDateGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            e.Control.TextChanged+= new EventHandler(this.Control_TextChanged);
            e.Control.Validated += new EventHandler(this.Control_Validated);
            if (DueDateGridView.CurrentCell.ColumnIndex == 7) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);                    
                }
               
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }            
            
        }

        private void DueDateGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                if((e.ColumnIndex.Equals(this.DueDateGridView.Columns["DefaultGraceperiod"].Index)))
                {
                    if ((e.ColumnIndex.Equals(this.DueDateGridView.Columns["DefaultGraceperiod"].Index)))
                    {
                        if (!String.IsNullOrEmpty(DueDateGridView.Rows[e.RowIndex].Cells["DefaultGraceperiod"].Value.ToString()))
                        {
                            int number;
                            if (!int.TryParse(DueDateGridView.Rows[e.RowIndex].Cells["DefaultGraceperiod"].Value.ToString(), out number) || number < -32768 || number > 32767)
                            {
                            //int gracePeriod;
                            this.DueDateGridView[this.gridCurrentColumnIndex, this.gridCurrentRowIndex].Value = "0";
                            }
                        }                                      
                    }
                }
                
            }
        }


        /// <summary>
        /// Handles the CellEndEdit event of the DueDateGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DueDateGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {               
                if ((e.ColumnIndex.Equals(this.DueDateGridView.Columns["FirstDue"].Index)))
                {
                    if (!String.IsNullOrEmpty(DueDateGridView.Rows[e.RowIndex].Cells["FirstDue"].Value.ToString()))
                    {
                        DateTime firstDue;
                        DateTime.TryParse(DueDateGridView.Rows[e.RowIndex].Cells["FirstDue"].Value.ToString(), out firstDue);
                        this.DueDateGridView[e.ColumnIndex, this.DueDateGridView.CurrentRowIndex].Value = firstDue;
                    }
                }
                else if ((e.ColumnIndex.Equals(this.DueDateGridView.Columns["SecondDue"].Index)))
                {
                    if (!String.IsNullOrEmpty(DueDateGridView.Rows[e.RowIndex].Cells["SecondDue"].Value.ToString()))
                    {
                        DateTime secondDue;
                        DateTime.TryParse(DueDateGridView.Rows[e.RowIndex].Cells["SecondDue"].Value.ToString(), out secondDue);
                        this.DueDateGridView[e.ColumnIndex, e.RowIndex].Value = secondDue;
                    }
                }
               
            }
        }
        
        /// <summary>
        /// Handles the CellEnter event of the DueDateGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DueDateGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex > 0)
                {
                    
                    if (DueDateGridView[e.ColumnIndex, e.RowIndex].ReadOnly)
                    {
                        if (!e.ColumnIndex.Equals(1))
                        {
                            SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            if (this.isChanged)
                            {
                                this.isChanged = false;                                
                            }
                            else
                            {
                                if (this.gridCurrentColumnIndex > 0)
                                {
                                    this.DueDateGridView.CurrentCell = this.DueDateGridView.Rows[e.RowIndex].Cells[4];

                                }
                            }
                          
                        }                       

                    }
                    else
                    {
                        this.gridCurrentRowIndex = e.RowIndex;
                        this.gridCurrentColumnIndex = e.ColumnIndex;
                    }                       
                    

                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Handles the KeyPress event of the DueDateGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void DueDateGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                      
            {
               // this.DueDateGridView.CurrentRow = this.DueDateGridView.Rows[rowIndex + 1];
                this.DueDateGridView.CurrentCell = this.DueDateGridView.Rows[rowIndex + 1].Cells[this.gridCurrentColumnIndex];
            }
        }


      
        

        #endregion
       
    }
}
