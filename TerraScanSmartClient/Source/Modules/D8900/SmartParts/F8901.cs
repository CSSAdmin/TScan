// --------------------------------------------------------------------------------------------
// <copyright file="F8901.cs" company="Congruent">
//    Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the Work Order Engine. 
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author            Description
// ----------       ---------         ---------------------------------------------------------
//                 M.Vijayakumar       Created
// *********************************************************************************/

namespace D8900
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;

    /// <summary>
    /// Class for F8901
    /// </summary>
    public partial class F8901 : BaseSmartPart
    {
        #region variable

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");

        /// <summary>
        /// Used to store the system id value
        /// </summary>
        private int systemId;

        /// <summary>
        /// Used to store the datatable rows count
        /// </summary>
        private int workOrderGridCount;

        /// <summary>
        /// report Action Smart part
        /// </summary>
        private ReportActionSmartPart reportActionSmartPart;

        /// <summary>
        /// controller F8901
        /// </summary>
        private F8901Controller form8901Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// Instance for GDocWorkOrderEngineData
        /// </summary>
        private GDocWorkOrderEngineData gdocWorkOrderEngineData = new GDocWorkOrderEngineData();

        /// <summary>
        ///  used to Convert Color From string
        /// </summary>
        private ColorConverter colorConv = new ColorConverter();

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// date Changed
        /// </summary>
        private bool dateChanged;

        #endregion variable

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8901"/> class.
        /// </summary>
        public F8901()
        {
            this.InitializeComponent();
        }

        #region Event Publication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Event for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// Enum for Closed Status
        /// when 0 = Closed  
        /// when 1 = Open
        /// when 2 = Both open and closed
        /// </summary>
        private enum ClosedFlagValue
        {
            /// <summary>
            /// Value for closed
            /// </summary>
            closed = 0,

            /// <summary>
            /// Value foropen
            /// </summary>
            open = 1,

            /// <summary>
            /// Value for all
            /// </summary>
            all = 2,
        }

        #endregion Enum

        #region Property

        /// <summary>
        /// Gets or sets the form8901 control.
        /// </summary>
        /// <value>The form8901 control.</value>
        [CreateNew]
        public F8901Controller Form8901Control
        {
            get { return this.form8901Control as F8901Controller; }
            set { this.form8901Control = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        public TerraScanCommon.PageModeTypes PageMode
        {
            get { return this.pageMode; }
            set { this.pageMode = value; }
        }

        #endregion Property

        #region Event Subscription

        #region Reports

        /// <summary>
        /// Prints the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Windows.Forms.Button&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick, Thread = ThreadOption.UserInterface)]
        public void PrintButtonClick(object sender, DataEventArgs<Button> e)
        {
            // TODO : Genralized 
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("SystemID", this.systemId);
                //// changed the parameter type from string to int
                TerraScanCommon.ShowReport(890101, Report.ReportType.Print, ht);
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
        /// Previews the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Windows.Forms.Button&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("SystemID", this.systemId);
                //// changed the parameter type from string to int
                TerraScanCommon.ShowReport(890101, Report.ReportType.Preview, ht);
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
        /// Emails the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Windows.Forms.Button&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick, Thread = ThreadOption.UserInterface)]
        public void EmailButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////// calling  Common Function For Report
                this.Cursor = Cursors.WaitCursor;
                //// Calling the Common Function for Report
                Hashtable ht = new Hashtable();
                ht.Add("SystemID", this.systemId);
                //// changed the parameter type from string to int
                TerraScanCommon.ShowReport(890101, Report.ReportType.Email, ht);
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

        #endregion reports

        #region OptionalParameter

        ////  Code commented for CO:#2250
        /*
        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Object[]&gt;"/> instance containing the event data.</param>
        /// [EventSubscription("topic://TerraScan.UI.CAB/MainForm/SendOptionalParameters", Thread = ThreadOption.UserInterface)]
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
            {
                if (optionalParams[0] != null && string.Equals(optionalParams[0].ToString(), "RecordDeleted"))
                {
                    this.CustomizeWorkOrderEngine();
                }
                else
                {
                    if (optionalParams[0] !=null && !string.IsNullOrEmpty(optionalParams[0].ToString()))
                    {
                        if (!string.IsNullOrEmpty(optionalParams[optionalParams.Length - 3].ToString()) && optionalParams[optionalParams.Length - 3] != null)
                        {
                            this.PermissionFiled = ((PermissionFields)optionalParams[optionalParams.Length - 3]);
                        }

                        this.systemId = Convert.ToInt32(optionalParams[0].ToString());
                        this.CustomizeWorkOrderEngine();
                    }
                    else
                    {
                        this.systemId = 0;
                        this.CustomizeWorkOrderEngine();
                    }
                }
            } 
        }*/

        #endregion OptionalParameter

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data.Equals("F" + this.Tag.ToString()))
            {
                this.form8901Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Windows.Forms.Button&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus())
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        #endregion EventSubscription

        #region Events

        /// <summary>
        /// Handles the Load event of the F8901 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8901_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();

                // Code added for CO:#2250 - Get SystemID from DB
                // Form Number (8901)
                int formId;
                int.TryParse(this.Tag.ToString(), out formId);

                // Get SystemId based on User
                this.systemId = this.form8901Control.WorkItem.GetSystemId(TerraScanCommon.UserId, formId);
                
                // Load Work Order Engine Details
                this.CustomizeWorkOrderEngine();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Calender Operation

        /// <summary>
        /// Handles the DateSelected event of the WorkOrderMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void WorkOrderMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the WorkOrderMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderMonthCalander_Leave(object sender, EventArgs e)
        {
            try
            {
                this.WorkOrderMonthCalander.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the WorkOrderMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void WorkOrderMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.WorkOrderMonthCalander.SelectionStart.ToString(this.dateFormat));
                    this.WorkOrderMonthCalander.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the WorkOrderEngineDateImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderEngineDateImage_Click(object sender, EventArgs e)
        {
            try
            {
                this.WorkOrderMonthCalander.Visible = true;
                this.WorkOrderMonthCalander.ScrollChange = 1;

                ////// Display the Calender control near the Calender Picture box.
                this.WorkOrderMonthCalander.Left = this.WorkOrderEngineDatePanel.Left + this.WorkOrderEngineDateImage.Left + this.WorkOrderEngineDateImage.Width;
                this.WorkOrderMonthCalander.Top = this.WorkOrderEngineDatePanel.Top + this.WorkOrderEngineDateImage.Top;
                this.WorkOrderMonthCalander.Tag = this.WorkOrderEngineDateImage.Tag;
                this.WorkOrderMonthCalander.Focus();

                if (!string.IsNullOrEmpty(this.WorkOrderEngineDateTextBox.Text))
                {
                    this.WorkOrderMonthCalander.SetDate(Convert.ToDateTime(this.WorkOrderEngineDateTextBox.Text));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Calender Operation

        #region Work Order Buttons

        /// <summary>
        /// Handles the Click event of the AllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.AllButton.BackColor = System.Drawing.Color.Gray;
                this.AllButton.ForeColor = System.Drawing.Color.White;

                this.ClosedButton.BackColor = System.Drawing.Color.Silver;
                this.ClosedButton.ForeColor = System.Drawing.Color.Black;

                this.OpenButton.BackColor = System.Drawing.Color.Silver;
                this.OpenButton.ForeColor = System.Drawing.Color.Black;

                this.CustomizeWorkOrderGridItems((int)ClosedFlagValue.all);
            }
            catch (SoapException e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClosedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClosedButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.AllButton.BackColor = System.Drawing.Color.Silver;
                this.AllButton.ForeColor = System.Drawing.Color.Black;

                this.OpenButton.BackColor = System.Drawing.Color.Silver;
                this.OpenButton.ForeColor = System.Drawing.Color.Black;

                this.ClosedButton.BackColor = System.Drawing.Color.Gray;
                this.ClosedButton.ForeColor = System.Drawing.Color.White;

                this.CustomizeWorkOrderGridItems((int)ClosedFlagValue.closed);
            }
            catch (SoapException e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the OpenButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClosedButton.BackColor = System.Drawing.Color.Silver;
                this.ClosedButton.ForeColor = System.Drawing.Color.Black;

                this.AllButton.BackColor = System.Drawing.Color.Silver;
                this.AllButton.ForeColor = System.Drawing.Color.Black;

                this.OpenButton.BackColor = System.Drawing.Color.Gray;
                this.OpenButton.ForeColor = System.Drawing.Color.White;

                this.CustomizeWorkOrderGridItems((int)ClosedFlagValue.open);
            }
            catch (SoapException e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Work Order Buttons

        /// <summary>
        /// Handles the CellContentClick event of the WorkOrderGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void WorkOrderGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.WorkOrderGridView.Columns["WOType"].Index)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (e.RowIndex >= 0)
                    {
                        object[] optionalParameter = new object[] { this.WorkOrderGridView.Rows[e.RowIndex].Cells[this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOTypeIDColumn.ColumnName].Value, this.WorkOrderGridView.Rows[e.RowIndex].Cells[this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOIDColumn.ColumnName].Value };
                        Form workOrderEngine = new Form();
                        Int32 workOrderTypeID = 0;
                        Int32.TryParse(this.WorkOrderGridView.Rows[e.RowIndex].Cells[this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOTypeIDColumn.ColumnName].Value.ToString(), out workOrderTypeID);
                        workOrderEngine = this.form8901Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(workOrderTypeID, optionalParameter, this.form8901Control.WorkItem);
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
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the WorkOrderGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void WorkOrderGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.WorkOrderGridView.Columns["WOType"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (this.WorkOrderGridView.Rows[e.RowIndex].Selected || this.WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.White;
                        (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).ActiveLinkColor = Color.Blue;
                        (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }

                //// Code added for change WOID format as font "Courier New" and size "8F"
                if (e.ColumnIndex.Equals(this.WorkOrderGridView.Columns[this.WOID.Name].Index))
                {
                    (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewTextBoxCell).Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewTextBoxCell).Style.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    (WorkOrderGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewTextBoxCell).Style.ForeColor = System.Drawing.Color.Gray;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the WorkOrderTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the WorkOrderEngineDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderEngineDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dateChanged)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the CommentsTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CommentsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the WorkOrderEngineCompleteCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderEngineCompleteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the WorkOrderEngineAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderEngineAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveConfirmation();
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

        #endregion Events

        #region Methods

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form8901Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form8901Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form8901Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form8901Control.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.GdocPrintHeaderWorkSpace.Show(this.form8901Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.GdocPrintHeaderWorkSpace.Show(this.form8901Control.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
                this.reportActionSmartPart = (ReportActionSmartPart)this.form8901Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart);
            }

            this.reportActionSmartPart.DetailsButtonVisible = false;
            this.formLabelInfo[0] = "Work Order Engine";
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Customizes the work order engine.
        /// </summary>        
        private void CustomizeWorkOrderEngine()
        {
            this.LoadWorkOrderEngine();
        }

        /// <summary>
        /// Loads the work order engine.
        /// </summary>        
        private void LoadWorkOrderEngine()
        {
            int workOrderCount = 0;

            this.gdocWorkOrderEngineData = this.form8901Control.WorkItem.F8901_GetWorkOrderEngine(this.systemId, (int)ClosedFlagValue.all);
            workOrderCount = this.gdocWorkOrderEngineData.GetWorkOrderEngine.Rows.Count;

            ////to check whether workOrderCount have values
            if (workOrderCount > 0)
            {
                if (!string.IsNullOrEmpty(this.gdocWorkOrderEngineData.GetWorkOrderEngine.Rows[0][this.gdocWorkOrderEngineData.GetWorkOrderEngine.SystemColorColumn].ToString()))
                {
                    this.WorkOrderEngineHeaderColorLabel.BringToFront();
                    this.WorkOrderEngineHeaderColorLabel.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromInvariantString(this.gdocWorkOrderEngineData.GetWorkOrderEngine.Rows[0][this.gdocWorkOrderEngineData.GetWorkOrderEngine.SystemColorColumn].ToString());
                }

                this.WorkEngineHeaderDescLable.Text = this.gdocWorkOrderEngineData.GetWorkOrderEngine.Rows[0][this.gdocWorkOrderEngineData.GetWorkOrderEngine.SystemNameColumn].ToString();

                this.WorkOrderButtons(true);
                this.DefaultButtonProperties();

                this.WorkorderHeaderLock(true);
                this.WorkOrderEngineAdd.Enabled = true;
                this.InitWorkOrderTypeCombo();

                this.WorkOrderEngineDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                this.dateChanged = true;
                this.WorkOrderMonthCalander.Visible = false;
                this.WorkOrderEngineDateImage.Enabled = true;

                this.CustomizeWorkOrderGridItems((int)ClosedFlagValue.all);

                ////when new permission is not avaliable then disable the WorkorderHeader.
                if (!this.PermissionFiled.newPermission)
                {
                    this.WorkOrderEngineAdd.Enabled = false;
                    this.WorkorderHeaderLock(false);
                }
                else
                {
                    this.WorkOrderEngineAdd.Enabled = true;
                    this.WorkorderHeaderLock(true);
                }
            }
            else
            {
                this.DisableWorkorderHeader();
            }
        }

        /// <summary>
        /// Customizes the work order grid.
        /// </summary>        
        /// <param name="isopenValue">The isopen.</param>
        private void CustomizeWorkOrderGridItems(int isopenValue)
        {
            this.gdocWorkOrderEngineData = this.form8901Control.WorkItem.F8901_GetWorkOrderEngine(this.systemId, isopenValue);
            this.workOrderGridCount = this.gdocWorkOrderEngineData.GetWorkOrderEngine.Rows.Count;
            if (this.workOrderGridCount > 0)
            {
                this.WorkOrderGridView.Columns[this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOTypeIDColumn.ColumnName].DataPropertyName = this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOTypeIDColumn.ColumnName;
                this.WorkOrderGridView.Columns[this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOTypeColumn.ColumnName].DataPropertyName = this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOTypeColumn.ColumnName;
                this.WorkOrderGridView.Columns[this.gdocWorkOrderEngineData.GetWorkOrderEngine.WODateColumn.ColumnName].DataPropertyName = this.gdocWorkOrderEngineData.GetWorkOrderEngine.WODateColumn.ColumnName;
                this.WorkOrderGridView.Columns[this.gdocWorkOrderEngineData.GetWorkOrderEngine.CommentsColumn.ColumnName].DataPropertyName = this.gdocWorkOrderEngineData.GetWorkOrderEngine.CommentsColumn.ColumnName;
                this.WorkOrderGridView.Columns[this.gdocWorkOrderEngineData.GetWorkOrderEngine.IsOpenColumn.ColumnName].DataPropertyName = this.gdocWorkOrderEngineData.GetWorkOrderEngine.IsOpenColumn.ColumnName;
                this.WorkOrderGridView.Columns[this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOIDColumn.ColumnName].DataPropertyName = this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOIDColumn.ColumnName;
               
                //// Primary key column has been set for resolve sorting issue
                this.WorkOrderGridView.PrimaryKeyColumnName = this.gdocWorkOrderEngineData.GetWorkOrderEngine.WOIDColumn.ColumnName;

                this.WorkOrderGridView.Enabled = true;
                this.WorkOrderGridView.AutoGenerateColumns = false;

                this.WorkOrderGridView.DataSource = this.gdocWorkOrderEngineData.GetWorkOrderEngine.DefaultView;
                this.WorkOrderGridView.Rows[0].Selected = true;

                this.DisableVScrollBar();
            }
            else
            {
                this.ClearWorkOrderGridItems();
            }
        }

        /// <summary>
        /// Clears the work order grid.
        /// </summary>
        private void ClearWorkOrderGridItems()
        {
            this.WorkOrderGridView.Enabled = false;
            this.WorkOrderGridView.DataSource = null;
            this.WorkOrderGridView.Rows[0].Selected = false;
            this.DisableVScrollBar();
        }

        /// <summary>
        /// Disables or enables the Vertical Scroll bar.
        /// </summary>
        private void DisableVScrollBar()
        {
            if (this.workOrderGridCount > this.WorkOrderGridView.NumRowsVisible)
            {
                //// this.WorkOrderVerticalScroll.Enabled = true;
                this.WorkOrderVerticalScroll.Visible = false;
                ////this.WorkOrderVerticalScroll.SendToBack();
            }
            else
            {
                ////this.WorkOrderVerticalScroll.Enabled = false;
                this.WorkOrderVerticalScroll.Visible = true;
                ////this.WorkOrderVerticalScroll.BringToFront();
            }
        }

        /// <summary>
        /// Defaults the button properties.
        /// </summary>
        private void DefaultButtonProperties()
        {
            ////Properties for Buttons on load
            this.AllButton.BackColor = System.Drawing.Color.Gray;
            this.AllButton.ForeColor = System.Drawing.Color.White;

            this.ClosedButton.BackColor = System.Drawing.Color.Silver;
            this.ClosedButton.ForeColor = System.Drawing.Color.Black;

            this.OpenButton.BackColor = System.Drawing.Color.Silver;
            this.OpenButton.ForeColor = System.Drawing.Color.Black;
        }

        /// <summary>
        /// Load the workordertype combo.
        /// </summary>
        private void InitWorkOrderTypeCombo()
        {
            GDocWorkOrderEngineData.GetWorkOrderTypeDataTable workOrderType = new GDocWorkOrderEngineData.GetWorkOrderTypeDataTable();

            ////temporary row is created and added to a temporary datatable(workOrderType) 
            DataRow customRow = workOrderType.NewRow();

            workOrderType.Clear();

            ////<Select> is addded
            customRow[this.gdocWorkOrderEngineData.GetWorkOrderType.WOTypeIDColumn.ColumnName] = "0";
            customRow[this.gdocWorkOrderEngineData.GetWorkOrderType.WOTypeColumn.ColumnName] = "<Select>";
            workOrderType.Rows.Add(customRow);

            this.gdocWorkOrderEngineData = this.form8901Control.WorkItem.F8901_GetWorkOrderType(this.systemId);

            ////The original datatable and temp datatable is merged
            workOrderType.Merge(this.gdocWorkOrderEngineData.GetWorkOrderType);

            this.WorkOrderTypeComboBox.ValueMember = this.gdocWorkOrderEngineData.GetWorkOrderType.WOTypeIDColumn.ColumnName;
            this.WorkOrderTypeComboBox.DisplayMember = this.gdocWorkOrderEngineData.GetWorkOrderType.WOTypeColumn.ColumnName;
            this.WorkOrderTypeComboBox.DataSource = workOrderType;
            this.WorkOrderTypeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// To saves the work order engine the information are converted to datatable.
        /// </summary>
        private void SaveWorkOrderEngine()
        {
            this.gdocWorkOrderEngineData.SaveWorkOrderEngine.Rows.Clear();
            GDocWorkOrderEngineData.SaveWorkOrderEngineRow dataRow = this.gdocWorkOrderEngineData.SaveWorkOrderEngine.NewSaveWorkOrderEngineRow();

            dataRow.SystemID = Convert.ToInt16(this.systemId);

            dataRow.WOTypeID = Convert.ToInt16(this.WorkOrderTypeComboBox.SelectedValue);

            dataRow.WODate = this.WorkOrderEngineDateTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(this.CommentsTextBox.Text.Trim()))
            {
                dataRow.Comments = this.CommentsTextBox.Text.Trim();
            }

            if (this.WorkOrderEngineCompleteCheckBox.Checked)
            {
                ////checkbox is true then false is assigned
                dataRow.IsOpen = false;
            }
            else
            {
                ////checkbox is true then true is assigned
                dataRow.IsOpen = true;
            }

            this.gdocWorkOrderEngineData.SaveWorkOrderEngine.Rows.Add(dataRow);
            this.gdocWorkOrderEngineData.Merge(this.form8901Control.WorkItem.F8901_SaveWorkOrderEngine(Utility.GetXmlString(this.gdocWorkOrderEngineData.SaveWorkOrderEngine.Copy()), TerraScanCommon.UserId));
        }

        /// <summary>
        /// Checks the mandatory field.
        /// </summary>
        /// <returns> True if all mandatoryFields Are Filed  else false
        /// </returns>
        private bool CheckMandatoryField()
        {
            if (this.WorkOrderTypeComboBox.SelectedIndex > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            this.WorkOrderMonthCalander.Tag = string.Empty;
            this.WorkOrderEngineDateImage.Focus();
            this.WorkOrderEngineDateTextBox.Text = dateSelected;
            this.WorkOrderMonthCalander.Visible = false;
        }

        /// <summary>
        /// Clears the work order engine.
        /// </summary>
        private void ClearWorkOrderEngine()
        {
            this.WorkOrderTypeComboBox.SelectedIndex = 0;
            this.WorkOrderEngineDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
            this.CommentsTextBox.Text = string.Empty;
            this.WorkOrderEngineCompleteCheckBox.Checked = false;
        }

        #region To Disable the Form

        /// <summary>
        /// Disables the work order header.
        /// </summary>
        private void DisableWorkorderHeader()
        {
            this.WorkOrderEngineHeaderColorLabel.SendToBack();
            this.WorkEngineHeaderDescLable.Text = string.Empty;
            this.WorkOrderButtons(false);
            this.DefaultButtonProperties();

            this.WorkOrderEngineAdd.Enabled = false;

            this.ClearWorkOrderTypeCombo();

            this.WorkOrderEngineDateTextBox.Text = string.Empty;
            this.WorkOrderMonthCalander.Visible = false;
            this.WorkOrderEngineDateImage.Enabled = false;

            this.CommentsTextBox.Text = string.Empty;
            this.WorkOrderEngineCompleteCheckBox.Checked = false;

            this.WorkorderHeaderLock(false);
            this.ClearWorkOrderGridItems();
        }

        /// <summary>
        /// Clears the work order type combo.
        /// </summary>
        private void ClearWorkOrderTypeCombo()
        {
            this.WorkOrderTypeComboBox.DataSource = null;
            this.WorkOrderTypeComboBox.Items.Clear();
            this.WorkOrderTypeComboBox.Refresh();
        }

        /// <summary>
        /// Method to Lock the Work Order Header Panel.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void WorkorderHeaderLock(bool lockControl)
        {
            this.WorkOrderTypePanel.Enabled = lockControl;
            this.WorkOrderEngineDatePanel.Enabled = lockControl;
            this.CommentsPanel.Enabled = lockControl;
            this.ClosedPanel.Enabled = lockControl;
        }

        #endregion To Disable the Form

        /// <summary>
        /// To enable or disable Works order buttons.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void WorkOrderButtons(bool lockControl)
        {
            this.AllButton.Enabled = lockControl;
            this.ClosedButton.Enabled = lockControl;
            this.OpenButton.Enabled = lockControl;
        }

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.PageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", "Work Order Engine", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.CheckSave();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the save is made or not.
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool CheckSave()
        {
            ////this.flagSaveConfirmed = false;
            try
            {
                if (this.SaveConfirmation())
                {
                    return true;
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    return false;
                }
            }
            catch (SoapException)
            {
                return false;
                ////ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception)
            {
                return false;
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Saves after check the all the required fields.
        /// </summary>
        /// <returns>boolean value</returns>
        private bool SaveConfirmation()
        {
            if (this.PermissionFiled.newPermission)
            {
                if (!string.IsNullOrEmpty(this.WorkOrderEngineDateTextBox.Text.Trim()))
                {
                    ////when the Selected Work Order Date is Greather than the Current Date 
                    if (Convert.ToDateTime(this.WorkOrderEngineDateTextBox.Text) > DateTime.Now)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("WorkOrderEngineDateValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.WorkOrderTypeComboBox.Focus();
                        return false;
                    }
                    else
                    {
                        if (this.CheckMandatoryField())
                        {
                            this.SaveWorkOrderEngine();
                            this.CustomizeWorkOrderGridItems((int)ClosedFlagValue.all);
                            this.ClearWorkOrderEngine();
                            this.DefaultButtonProperties();
                            this.WorkOrderTypeComboBox.Focus();
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.WorkOrderTypeComboBox.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.WorkOrderTypeComboBox.Focus();
                    return false;
                }
            }
            else
            {
                this.WorkOrderEngineAdd.Enabled = false;
                this.WorkOrderTypeComboBox.Focus();
                return false;
            }
        }

        #endregion Methods
    }
}
