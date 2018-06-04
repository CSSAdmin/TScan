//--------------------------------------------------------------------------------------------
// <copyright file="F9039.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the QueryEngine.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Aug 07		VINOTH      	    CREATED
// 16 Aug 07		VINOTH      	    UI Functionality
// 20111107         Manoj Kumar         issue fixed for the TSBG 14033
//**********************************************************************************************/

namespace D9030
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinCalcManager;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using Infragistics.Win;
    using System.Web.Services.Protocols;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.EventBroker;

    #endregion NameSpaces

    /// <summary>
    /// F9039 Class
    /// </summary>
    public partial class F9039 : Form
    {
        #region MemberVariable

        /// <summary>
        /// Define grid
        /// </summary>
        private UltraGridBase grid;

        /// <summary>
        ///  CreatedColumn 
        /// </summary>
        private UltraGridColumn createdColumn;

        /// <summary>
        /// The Selected COlumn
        /// </summary>
        private string selectedColumn;

        /// <summary>
        /// form9033Control Control Name
        /// </summary>
        private F9039Controller form9039Control;

        /// <summary>
        /// ListColumnDetails DataTable
        /// </summary>
        private F9039QueryUpdate.ListColumnDetailsDataTable columnDetailsTable = new F9039QueryUpdate.ListColumnDetailsDataTable();

        /// <summary>
        /// QueryViewId
        /// </summary>
        private int queryViewId;

        /// <summary>
        /// keyIds XMLString
        /// </summary>
        private string keyIds;

        /// <summary>
        /// KeyId COlumnName
        /// </summary>
        private string keyIdColumnName;

        /// <summary>
        /// copyColumnDetailsTable
        /// </summary>
        private DataTable copyColumnDetailsTable = new DataTable();

        /// <summary>
        /// The MasterFormNo
        /// </summary>
        private int masterFormNumber;

        /// <summary>
        /// selected Item Values
        /// </summary>
        private List<string> selectedQueueItemValues = new List<string>();

       System.Windows.Forms.ToolTip tiptol = new System.Windows.Forms.ToolTip();

       private bool _tooltipVisible;
       private bool _dropDownOpen;

        #endregion MemberVariable

        #region Constructor

        #region Default Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F9039"/> class.
        /// </summary>
        public F9039()
        {
            this.InitializeComponent();
        }

        #endregion Default Constructor

        #region Overload Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9039"/> class.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="keyIdColumnName">Name of the key id column.</param>
        /// <param name="masterFormNo">The master form no.</param>
        public F9039(int queryViewId, string keyIds, string keyIdColumnName, int masterFormNo)
        {
            this.InitializeComponent();
            this.queryViewId = queryViewId;
            this.keyIds = keyIds;
            this.keyIdColumnName = keyIdColumnName;
            this.masterFormNumber = masterFormNo;
        }

        #endregion Override Constructor

        #endregion Constructor
        
        #region EventPublication

        /// <summary>
        /// event to intiate LoadQueryEngine after Process Button
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_ReloadQueryAfterColumnUpdate, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9033_ReloadQueryAfterColumnUpdate;

        #endregion EventPublication

        #region UltraGridBase Property

        /// <summary>
        /// Gets or sets the UltraGrid instances whose columns are displayed in the column chooser.
        /// </summary>
        public UltraGridBase Grid
        {
            get
            {
                return this.grid;
            }

            set
            {
                if (value != this.grid)
                {
                    this.grid = value;
                    this.QueryEngineGrid.SourceGrid = this.grid;
                }
            }
        }

        #endregion UltraGridBase Property

        #region F9039Control Property

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F9039Controller F9039Control
        {
            get { return this.form9039Control as F9039Controller; }

            set { this.form9039Control = value; }
        }

        #endregion F9039Control Property

        #region SelectedColumn

        /// <summary>
        /// Gets or sets the selected column.
        /// </summary>
        /// <value>The selected column.</value>
        private string SelectedColumn
        {
            get
            {
                return this.selectedColumn;
            }

            set
            {                
                this.selectedColumn = value;
           }
        }
          #endregion SelectedColumn
             
        #region Virtual OnD9030_F9033_ReloadQueryAfterColumnUpdate

        /// <summary>
        /// Called when [D9030_ F9033_ reload query after column update].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_ReloadQueryAfterColumnUpdate(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9033_ReloadQueryAfterColumnUpdate != null)
            {
                this.D9030_F9033_ReloadQueryAfterColumnUpdate(this, eventArgs);
            }
        }

        #endregion Virtual OnD9030_F9033_ReloadQueryAfterColumnUpdate

        #region Form Events

        #region ClearButton

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.columnDetailsTable.Rows.Clear();

                (this.QueryEngineColumn as DataGridViewComboBoxColumn).DataSource = this.columnDetailsTable.Copy();
                (this.QueryEngineColumn as DataGridViewComboBoxColumn).DisplayMember = this.columnDetailsTable.ColumnNameColumn.ColumnName;
                (this.QueryEngineColumn as DataGridViewComboBoxColumn).ValueMember = this.columnDetailsTable.ReplaceIDColumn.ColumnName;

                this.QueryUpdateGrid.DataSource = this.columnDetailsTable.DefaultView;

                this.ProcessButton.Enabled = false;
                this.ClearButton.Enabled = false;
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion ClearButton

        #region FormLoad

        /// <summary>
        /// Handles the Load event of the F9039 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F9039_Load(object sender, EventArgs e)
        {
            try
            {
                this.CustomizeQueryUpdateGrid();                
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

        #endregion FormLoad        

        #region GridEvent

        #region RowEnter

        /// <summary>
        /// Handles the RowEnter event of the QueryUpdateGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void QueryUpdateGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if (this.QueryUpdateGrid.Rows[(e.RowIndex - 1)].Cells[this.QueryEngineColumn.Name].Value != null)
                    {
                        if ((string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[(e.RowIndex - 1)].Cells[this.QueryEngineColumn.Name].Value.ToString().Trim())))
                        {
                            if (e.RowIndex + 1 < QueryUpdateGrid.RowCount)
                            {
                                for (int i = e.RowIndex; i < QueryUpdateGrid.RowCount; i++)
                                {
                                    if (!string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[i].Cells[this.QueryEngineColumn.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[i].Cells[this.ImageColumn.Name].Value.ToString().Trim()))
                                    {
                                        hasValues = true;
                                        break;
                                    }
                                }

                                if (hasValues)
                                {
                                    this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].ReadOnly = false;
                                    this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = false;
                                    this.QueryUpdateGrid["ImageColumn", e.RowIndex].ReadOnly = false;
                                    this.QueryUpdateGrid.Rows[e.RowIndex].Selected = false;
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.QueryUpdateGrid["FormulaColumn", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.QueryUpdateGrid["ImageColumn", e.RowIndex].Value.ToString().Trim())))
                                    {
                                        this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].ReadOnly = true;
                                        this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                                        this.QueryUpdateGrid["ImageColumn", e.RowIndex].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].ReadOnly = false;
                                        this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = false;
                                        this.QueryUpdateGrid["ImageColumn", e.RowIndex].ReadOnly = false;
                                        this.QueryUpdateGrid.Rows[e.RowIndex].Selected = false;
                                    }
                                }
                            }
                            else
                            {
                                this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].ReadOnly = true;
                                this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                                this.QueryUpdateGrid["ImageColumn", e.RowIndex].ReadOnly = true;
                            }
                        }
                        else
                        {
                            this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].ReadOnly = false;
                            this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                            this.QueryUpdateGrid["ImageColumn", e.RowIndex].ReadOnly = true;
                            this.QueryUpdateGrid.Rows[e.RowIndex].Selected = false;
                        }
                    }
                    else
                    {
                        this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].ReadOnly = true;
                        this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                        this.QueryUpdateGrid["ImageColumn", e.RowIndex].ReadOnly = true;
                        this.QueryUpdateGrid.Rows[e.RowIndex].Selected = true;
                    }
                }

                if (e.RowIndex == 0)
                {
                    this.QueryUpdateGrid["QueryEngineColumn", e.RowIndex].ReadOnly = false;
                    this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                    this.QueryUpdateGrid["ImageColumn", e.RowIndex].ReadOnly = true;
                    this.QueryUpdateGrid.Rows[e.RowIndex].Selected = false;
                }
            }
            catch (SoapException ex)
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

        #endregion RowEnter

        #region CellEndEdit

        /// <summary>
        /// Handles the CellEndEdit event of the QueryUpdateGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void QueryUpdateGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value != null
                     && !string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString()))
                {
                    if (this.copyColumnDetailsTable.Rows.Count > 0)
                    {
                        this.DrawCommandButton(e);                        

                        this.ProcessButton.Enabled = true;
                        this.ClearButton.Enabled = true;
                        this.QueryUpdateGrid.Rows[e.RowIndex].Selected = true;
                        this.QueryUpdateGrid.Rows[e.RowIndex].Selected = false;

                        if (((e.RowIndex + 1) == this.QueryUpdateGrid.Rows.Count) && (e.ColumnIndex == 0))
                        {
                            if (!string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                            {                           
                                this.QueryUpdateGrid.AllowUserToAddRows = true;                               
                            }
                        } 

                        this.EnableGridScrollBar(); 
                    }
                }
                else
                {
                    if (this.QueryUpdateGrid.OriginalRowCount <= 0)
                    {
                        this.ProcessButton.Enabled = false;
                        this.ClearButton.Enabled = false;
                    }
                }                
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
                
        #endregion CellEndEdit

        #region CellClick

        /// <summary>
        /// Handles the CellClick event of the QueryUpdateGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void QueryUpdateGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString()))
                {
                    if (e.ColumnIndex == 2)
                    {
                        int formNumber;
                        string commandResult = string.Empty;

                        string filterString = "ReplaceID = " + this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        DataRow[] dr = this.copyColumnDetailsTable.Select(filterString);
                        if (dr.Length > 0)
                        {
                            string formNo = dr[0].ItemArray[2].ToString();
                            int.TryParse(formNo, out formNumber);
                            if (formNumber > 0)
                            {
                                if (formNumber.Equals(9045))
                                {
                                    
                                    object[] optionalParameter = new object[] {dr[0].ItemArray[7] };
                                    Form showConfiguredForm = new Form();
                                    showConfiguredForm = this.form9039Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(formNumber, optionalParameter, this.form9039Control.WorkItem);
                                    if (showConfiguredForm != null)
                                    {
                                        DialogResult returnedResult = showConfiguredForm.ShowDialog();
                                        if (returnedResult.Equals(DialogResult.OK)
                                            || returnedResult.Equals(DialogResult.Yes))
                                        {
                                            commandResult = TerraScanCommon.GetValue(showConfiguredForm, "CommandResult").ToString();
                                            if (!string.IsNullOrEmpty(commandResult))
                                            {
                                                this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = false;
                                                this.QueryUpdateGrid.Rows[e.RowIndex].Cells["FormulaColumn"].Value =commandResult.ToString();
                                                this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                                                ///Changed in Pass the Value in DataTable for the issue TSBG 14033
                                                this.QueryUpdateGrid.Rows[e.RowIndex].Cells["FormulaValue"].Value = commandResult.ToString();
                                            }
                                            else
                                            {
                                                // set the Replace Id from the Grid
                                                int replaceId;
                                                //  int.TryParse (TerraScanCommon.GetValue(showConfiguredForm, "KeyIDs"), out replaceId );
                                                int.TryParse(this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString(), out replaceId);
                                                int keyID;
                                                int.TryParse(TerraScanCommon.GetValue(showConfiguredForm, "KeyIDs"), out keyID);
                                                string key = keyID.ToString();
                                                DataSet commandResultData = new DataSet();
                                                commandResultData = this.SetMaskValue(replaceId, key);

                                                if (commandResultData.Tables.Count > 0)
                                                {
                                                    if (commandResultData.Tables[0].Rows.Count > 0)
                                                    {
                                                        this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = false;
                                                        this.QueryUpdateGrid.Rows[e.RowIndex].Cells["FormulaColumn"].Value = commandResultData.Tables[0].Rows[0][0].ToString();
                                                        this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                                                        ///Changed in Pass the Value in DataTable for the issue TSBG 14033
                                                        this.QueryUpdateGrid.Rows[e.RowIndex].Cells["FormulaValue"].Value = keyID.ToString();
                                                    }
                                                }

                                                
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                // Gets the Configured Form from DB
                                Form showConfiguredForm = new Form();
                                showConfiguredForm = this.form9039Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(formNumber, null, this.form9039Control.WorkItem);
                                
                                if (showConfiguredForm != null)
                                {
                                    DialogResult returnedResult = showConfiguredForm.ShowDialog();
                                    if (returnedResult.Equals(DialogResult.OK)
                                        || returnedResult.Equals(DialogResult.Yes))
                                    {
                                        commandResult = TerraScanCommon.GetValue(showConfiguredForm, "CommandResult").ToString();

                                        // set the Replace Id from the Grid
                                        int replaceId;
                                        int.TryParse(this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString(), out replaceId);

                                        DataSet commandResultData = new DataSet();
                                        commandResultData = this.SetMaskValue(replaceId, commandResult);

                                        if (commandResultData.Tables.Count > 0)
                                        {
                                            if (commandResultData.Tables[0].Rows.Count > 0)
                                            {
                                                this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = false;
                                                this.QueryUpdateGrid.Rows[e.RowIndex].Cells[1].Value = commandResultData.Tables[0].Rows[0][0].ToString();
                                                this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                                            }
                                        }
                                      
                                        this.QueryUpdateGrid.Rows[e.RowIndex].Cells["FormulaValue"].Value = commandResult.ToString();
                                    }
                                }
                                }
                            }
                            else
                            {
                                string formulaString = this.ShowFormulaBuilder();

                                this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = false;
                                this.QueryUpdateGrid.Rows[e.RowIndex].Cells[1].Value = formulaString;
                                this.QueryUpdateGrid["FormulaColumn", e.RowIndex].ReadOnly = true;
                                this.QueryUpdateGrid.Rows[e.RowIndex].Cells["FormulaValue"].Value = this.ChangeFormulaformat(formulaString);
                            }
                        }
                    }                    
                }

                if (e.ColumnIndex == 0)
                {
                    try
                    {
                        this.QueryEngineColumn.ToolTipText = this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                        //var cell = this.QueryUpdateGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        //cell.ToolTipText = this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                    }
                    catch(Exception exp)
                    {
                    }

                    this.AvoidComboItemRepeatationLogic(e);
                }
            }
            catch (SoapException ex)
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

        #endregion CellClick        

        #region KeyDownEvent

        /// <summary>
        /// Handles the KeyDown event of the QueryUpdateGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void QueryUpdateGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }           
        }

        #endregion KeyDownEvent

        #region FormulaBuilder_FunctionInitializing
        //// This event fires for each function added to the list
        //// of functions in the FormulaBuilder and provides the 
        //// opportunity to cancel them. 

        /// <summary>
        /// Handles the FunctionInitializing event of the formulaBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaBuilder.FunctionInitializingEventArgs"/> instance containing the event data.</param>
        private void FormulaBuilder_FunctionInitializing(object sender, FunctionInitializingEventArgs e)
        {
            switch (e.Function.Category)
            {
                case "Financial":
                    e.Cancel = true;
                    break;
                case "Engineering":
                    e.Cancel = true;
                    break;
                case "LookupAndReference":
                    e.Cancel = true;
                    break;
                case "DateAndTime":
                    if (!e.Function.Name.Equals("datediff") && !e.Function.Name.Equals("month")
                        && !e.Function.Name.Equals("year") && !e.Function.Name.Equals("now"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Information":
                    if (!e.Function.Name.Equals("isnumber") && !e.Function.Name.Equals("null"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Logical":
                    if (e.Function.Name.Equals("TRUE"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Math":
                    if (!e.Function.Name.Equals("abs") && !e.Function.Name.Equals("mod")
                        && !e.Function.Name.Equals("pi") && !e.Function.Name.Equals("round")
                        && !e.Function.Name.Equals("sqrt"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Statistical":
                    e.Cancel = true;
                    break;
                case "TextAndData":
                    if (e.Function.Name.Equals("char") || e.Function.Name.Equals("code"))
                    {
                        e.Cancel = true;
                    }
                    break;
            }
        }

        #endregion FormulaBuilder_FunctionInitializing

        #endregion GridEvents

        #region Process

        /// <summary>
        /// Handles the Click event of the ProcessButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[0].Cells[0].Value.ToString()))
                {
                    List<string> outputParamater = new List<string>();
                    DataSet updateXmlData = new DataSet("Root");
                    ////this.QueryUpdateGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    ////this.columnDetailsTable.AcceptChanges();
                    DataTable dt = this.columnDetailsTable.Copy();
                    dt.TableName = "Table";
                    updateXmlData.Tables.Add(dt);
                    ////string updateXmlString = this.GetRecordInGrid();
                    string updateXmlString = updateXmlData.GetXml();
                    outputParamater.Add(this.form9039Control.WorkItem.F9039_UpdateQueryData(this.queryViewId, this.keyIdColumnName, this.keyIds, updateXmlString, 0, TerraScanCommon.UserId));
                    DialogResult continueProcess = MessageBox.Show("You are about to update " + outputParamater[0].ToString() + " records", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (continueProcess.Equals(DialogResult.OK))
                    {
                        outputParamater.Add(this.form9039Control.WorkItem.F9039_UpdateQueryData(this.queryViewId, this.keyIdColumnName, this.keyIds, updateXmlString, 1, TerraScanCommon.UserId));
                        int checkStatus;
                        int.TryParse(outputParamater[1].ToString(), out checkStatus);
                        if (checkStatus == 1)
                        {
                            MessageBox.Show("Process Completed SuccessFully.\n" + outputParamater[0].ToString() + " Records Updated", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.D9030_F9033_ReloadQueryAfterColumnUpdate(this, new TerraScan.Infrastructure.Interface.EventArgs<int>(this.masterFormNumber));
                        }
                        else
                        {
                            MessageBox.Show(outputParamater[1].ToString(), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Process

        #region Close

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {            
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Close

        #region HelpClick

        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Helpclick

        #endregion FormEvents

        #region Private Methods

        #region CustomizeQueryUpdateGrid

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeQueryUpdateGrid()
        {
            this.QueryUpdateGrid.AutoGenerateColumns = false;
            this.columnDetailsTable = this.form9039Control.WorkItem.F9039_ListQueryViewColumn(this.queryViewId).ListColumnDetails;

            this.copyColumnDetailsTable = this.columnDetailsTable.Copy();               

            this.QueryEngineColumn.DataPropertyName = this.columnDetailsTable.ReplaceIDColumn.ColumnName;
            this.FormulaColumn.DataPropertyName = this.columnDetailsTable.FormulaColumnColumn.ColumnName;
            this.ImageColumn.DataPropertyName = this.columnDetailsTable.ImageColumnColumn.ColumnName;
            this.FormulaValue.DataPropertyName = this.columnDetailsTable.FormulaValueColumn.ColumnName;

            (this.QueryEngineColumn as DataGridViewComboBoxColumn).DataSource = this.copyColumnDetailsTable.Copy();
            (this.QueryEngineColumn as DataGridViewComboBoxColumn).DisplayMember = this.columnDetailsTable.ColumnNameColumn.ColumnName;
            (this.QueryEngineColumn as DataGridViewComboBoxColumn).ValueMember = this.columnDetailsTable.ReplaceIDColumn.ColumnName;
            //(this.QueryEngineColumn as DataGridViewComboBoxColumn).ToolTipText= this.columnDetailsTable.ColumnNameColumn.ColumnName;

             this.columnDetailsTable.Clear();
             this.QueryUpdateGrid.DataSource = this.columnDetailsTable.DefaultView;
             this.QueryUpdateGridVScroll.Enabled = false;
         }

        #endregion CustomizeQueryUpdateGrid

        #region DrawCommandButton
        
         /// <summary>
         /// Draws the command button.
         /// </summary>
         /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DrawCommandButton(DataGridViewCellEventArgs e)
        {
            int formNumber;

            if (this.copyColumnDetailsTable.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString()))
                {
                    string filterString = "ReplaceID = " + this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                    DataRow[] dr = this.copyColumnDetailsTable.Select(filterString);
                    if (dr.Length > 0)
                    {
                        string formNo = dr[0].ItemArray[2].ToString();
                        int.TryParse(formNo, out formNumber);
                        if (formNumber > 0)
                        {
                            ////DataGridViewCell cell = (DataGridViewCell)this.QueryUpdateGrid[2, e.RowIndex];
                            TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.QueryUpdateGrid[2, e.RowIndex];
                            imgCell.ImagexLocation = 2;
                            imgCell.ImageyLocation = 1;
                            imgCell.Image = Properties.Resources.S;
                        }
                        else
                        {
                           //// DataGridViewCell cell = (DataGridViewCell)this.QueryUpdateGrid[2, e.RowIndex];
                            TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.QueryUpdateGrid[2, e.RowIndex];
                            imgCell.ImagexLocation = 2;
                            imgCell.ImageyLocation = 1;
                            imgCell.Image = Properties.Resources.C;
                        }
                    }
                    else
                    {
                        TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.QueryUpdateGrid[2, e.RowIndex];
                        imgCell.ImagexLocation = 2;
                        imgCell.ImageyLocation = 1;
                    }
                }
                else
                {
                    TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.QueryUpdateGrid[2, e.RowIndex];
                    imgCell.ImagexLocation = 2;
                    imgCell.ImageyLocation = 1;
                }
            }
        }

        #endregion DrawCommandButton

        #region SetMaskValue
        
        /// <summary>
        /// Sets the mask value.
        /// </summary>
        /// <param name="replaceId">The replace id.</param>
        /// <param name="commandResult">The command result.</param>
        /// <returns>setMaskValueData</returns>
        private DataSet SetMaskValue(int replaceId, string commandResult)
        {
            DataSet setMaskValueData = new DataSet();
            setMaskValueData = this.form9039Control.WorkItem.F9039_GetCommandResult(replaceId, commandResult);
            return setMaskValueData;
        }

        #endregion SetMaskValue

        #region ShowFormulaBuilder

        /// <summary>
        /// Shows the formula builder.
        /// </summary>
        /// <returns>returnFormulaString</returns>
        private string ShowFormulaBuilder()
        {
            string returnFormulaString = string.Empty;

            this.createdColumn = this.QueryEngineGrid.SourceGrid.DisplayLayout.Bands[0].Columns.Add("CITColumn"); 
            ////this.QueryEngineGrid.SourceGrid.DisplayLayout.Bands[0].Columns[columnName];

            if (null == this.createdColumn.Layout.Grid.CalcManager)
            {
                this.createdColumn.Layout.Grid.CalcManager = new UltraCalcManager();
            }

            FormulaBuilderDialog formulaBuilderDialog = new FormulaBuilderDialog(this.createdColumn, false);
            formulaBuilderDialog.FunctionInitializing += new FunctionInitializingEventHandler(this.FormulaBuilder_FunctionInitializing);
            formulaBuilderDialog.OperandInitializing += new OperandInitializingEventHandler(formulaBuilderDialog_OperandInitializing);
            //FormulaBuilderDialog dlg = new FormulaBuilderDialog(this.createdColumn, false);

            DialogResult result = formulaBuilderDialog.ShowDialog(this);

            if (DialogResult.OK == result)  
            {
                returnFormulaString = formulaBuilderDialog.Formula.ToString();
            }

            this.QueryEngineGrid.SourceGrid.DisplayLayout.Bands[0].Columns.Remove("CITColumn");

            return returnFormulaString;
        }

        private void formulaBuilderDialog_OperandInitializing(object sender, OperandInitializingEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.OperandName.ToString().Equals("\\ "))
            {
                e.Cancel = true;
            }
        }

        #endregion ShowFormulaBuilder

        #region AvoidComboItemRepeatationLogic

        /// <summary>
        /// Mokkas the logic.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AvoidComboItemRepeatationLogic(DataGridViewCellEventArgs e)
        {
            string currentCellValue = this.QueryUpdateGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

            if (!string.IsNullOrEmpty(currentCellValue))
            {
                //System.Windows.Forms.ToolTip tiptol = new System.Windows.Forms.ToolTip();
                //tiptol.Show("hello", QueryUpdateGrid, Cursor.Position.X, Cursor.Position.Y);
                DataTable balanceDataTable = new DataTable();
                balanceDataTable = this.RemoveSelectedDataRow();

                string filterString = "ReplaceID = " + currentCellValue;
                DataRow[] filteredRow = this.copyColumnDetailsTable.Select(filterString);
                if (filteredRow.Length > 0)
                {
                    foreach (DataRow dataRow in filteredRow)
                    {
                        DataRow[] selectedRow = balanceDataTable.Select(filterString);
                        if (selectedRow.Length == 0)
                        {
                            balanceDataTable.ImportRow(dataRow);
                        }
                    }
                }
                
                this.AssignDataSourceToCurrentCell(e.RowIndex, balanceDataTable);
            }
            else
            {
                this.AssignDataSourceToCurrentCell(e.RowIndex, this.RemoveSelectedDataRow());
            }
        }

        #endregion AvoidComboItemRepeatationLogic

        #region AssignDataSourceToCurrentCell

        /// <summary>
        /// Assigns the data source to current cell.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="gridDataSource">gridDataSource.</param>
        private void AssignDataSourceToCurrentCell(int rowIndex, DataTable gridDataSource)
        {
            DataGridViewComboBoxCell currentcell = (this.QueryUpdateGrid[0, rowIndex] as DataGridViewComboBoxCell);
            currentcell.DataSource = null;

            DataView currenCellDataView = new DataView(gridDataSource.Copy());
            currenCellDataView.Sort = "ReplaceID";

            DataTable gridDataSourceTable = currenCellDataView.ToTable();

            currentcell.DataSource = gridDataSourceTable.Copy();

            currentcell.DisplayMember = this.columnDetailsTable.ColumnNameColumn.ColumnName;
            currentcell.ValueMember = this.columnDetailsTable.ReplaceIDColumn.ColumnName;
        }

        #endregion AssignDataSourceToCurrentCell

        #region RemoveSelectedDataRow

        /// <summary>
        /// Removes the selected data row.
        /// </summary>
        /// <returns>balanceDataTable</returns>
        private DataTable RemoveSelectedDataRow()
        {
            DataTable balanceDataTable = this.copyColumnDetailsTable.Copy();
            DataView tempSelectedRowTable = new DataView();
            tempSelectedRowTable = (DataView)this.QueryUpdateGrid.DataSource;

            for (int rowIndex = 0; rowIndex < tempSelectedRowTable.Table.Rows.Count; rowIndex++)
            {
                if (!string.IsNullOrEmpty(tempSelectedRowTable.Table.Rows[rowIndex][0].ToString()))
                {
                    string filterString1 = "ReplaceID = " + tempSelectedRowTable.Table.Rows[rowIndex][0].ToString();
                    DataRow[] tempRow = balanceDataTable.Select(filterString1);

                    if (tempRow.Length > 0)
                    {
                        foreach (DataRow row in tempRow)
                        {
                            balanceDataTable.Rows.Remove(row);
                        }
                    }
                }
            }

            return balanceDataTable;
        }

        #endregion RemoveSelectedDataRow                

        #region EnableGridScrollBar

        /// <summary>
        /// Enables the grid scroll bar.
        /// </summary>
        private void EnableGridScrollBar()
        {
            if (this.QueryUpdateGrid.OriginalRowCount >= this.QueryUpdateGrid.NumRowsVisible)
            {
                this.QueryUpdateGridVScroll.Visible = false;
            }
            else
            {                
                this.QueryUpdateGridVScroll.Visible = true;
            }
        }

        #endregion EnableGridScrollBar

        #region Build Formula
        /// <summary>
        /// Formats the summary query.
        /// </summary>
        /// <param name="formulaValue">The formula value.</param>
        /// <returns>Formula String</returns>
        private string ChangeFormulaformat(string formulaValue)
        {
            string formula = formulaValue.Trim();
            //// formula = formula.Replace('"', ' ');
            ////Greater than
            if (formula.Contains(">") || formula.Contains("<") || formula.Contains("<>"))
            {
                formula = "CASE WHEN " + formula + "THEN 'True' ELSE 'FALSE' END ";
            }

            #region And()
            ////And Function
            if (formula.Contains("and("))
            {
                int startIndex = formula.IndexOf("and(") + 3;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("and("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            ////string test;
                            ////formulaArray[i].ToString().Replace('"', ' ');
                            if (j != formulaArray.Length - 1)
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString() + " and ";
                            }
                            else
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString();
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex - 3, endIndex + 4);
                formula = formula.Replace(oldValue, tempFormula);
            }
            #endregion And()

            #region 0r()
            if (formula.Contains("or("))
            {
                int startIndex = formula.IndexOf("or(") + 2;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("or("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            ////string test;
                            ////formulaArray[i].ToString().Replace('"', ' ');
                            if (j != formulaArray.Length - 1)
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString() + " or ";
                            }
                            else
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString();
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex - 2, endIndex + 3);
                formula = formula.Replace(oldValue, tempFormula);
            }
            #endregion 0r()

            #region & Operator
            ////& Operator
            if (formula.Contains("&"))
            {
                formula = formula.Replace("&", "+");
            }
            #endregion & Operator

            #region % Operator
            ////Percentage Operator
            if (formula.Contains("%"))
            {
                formula = formula.Replace("%", "/100.00");
            }
            #endregion % Operator

            #region Find()
            ////Find Function
            if (formula.Contains("find("))
            {
                formula = formula.Replace("find(", "charindex(");
                int startIndex = formula.IndexOf("charindex(") + 9;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("charindex("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            if (j <= 1)
                            {
                                if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim();// +" , ";
                                }
                                else
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim();// +" , ";
                                }
                                if (!j.Equals(formulaArray.Length - 1))
                                {
                                    tempFormula = tempFormula + " ,";
                                }
                            }
                            else
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString().Trim();
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula);
            }

            #endregion Find()

            #region DateDiff()
            ////Date Difference Function
            if (formula.Contains("datediff("))
            {
                int startIndex = formula.IndexOf("datediff(") + 8;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("datediff("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            ////string test;
                            ////formulaArray[i].ToString().Replace('"', ' ');
                            if (j.Equals(0))
                            {
                                string datePartValue = string.Empty;
                                string stringValue = formulaArray[j].ToString().Trim();
                                if (stringValue.Equals("\"d\"") || stringValue.Equals("d") || stringValue.Equals("'d'")
                                    || stringValue.Equals("\"y\"") || stringValue.Equals("y") || stringValue.Equals("'y'"))
                                {
                                    datePartValue = "day";
                                }
                                else if (stringValue.Equals("\"n\"") || stringValue.Equals("n") || stringValue.Equals("'n'"))
                                {
                                    datePartValue = "minute";
                                }
                                else if (stringValue.Equals("\"h\"") || stringValue.Equals("h") || stringValue.Equals("'h'"))
                                {
                                    datePartValue = "hour";
                                }
                                else if (stringValue.Equals("\"q\"") || stringValue.Equals("q") || stringValue.Equals("'q'"))
                                {
                                    datePartValue = "quarter";
                                }
                                else if (stringValue.Equals("\"s\"") || stringValue.Equals("s") || stringValue.Equals("'s'"))
                                {
                                    datePartValue = "second";
                                }
                                else if (stringValue.Equals("\"w\"") || stringValue.Equals("w") || stringValue.Equals("'w'")
                                         || stringValue.Equals("\"ww\"") || stringValue.Equals("ww") || stringValue.Equals("'ww'"))
                                {
                                    datePartValue = "week";
                                }
                                else if (stringValue.Equals("\"m\"") || stringValue.Equals("m") || stringValue.Equals("'m'"))
                                {
                                    datePartValue = "month";
                                }
                                else if (stringValue.Equals("\"yyyy\"") || stringValue.Equals("yyyy") || stringValue.Equals("'yyyy'"))
                                {
                                    datePartValue = "year";
                                }

                                tempFormula = tempFormula + datePartValue + " , ";
                            }
                            else if (j.Equals(1))
                            {
                                if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim() + " , ";
                                }
                                else
                                {
                                    tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'" + " , ";
                                }
                            }
                            else
                            {
                                if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim();// +" , ";
                                }
                                else
                                {
                                    tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'";
                                }
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula);
            }
            #endregion DateDiff()

            #region Congatenate

            if (formula.Contains("concatenate("))
            {
                int startIndex = formula.IndexOf("concatenate(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = tempFormula.Substring(tempFormula.IndexOf("concatenate(") + 12, endIndex - 12);
                tempFormula = tempFormula.Replace(',', '+');
                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }

            #endregion Congatenate

            #region IsBlank()
            ////IsBlank Function
            if (formula.Contains("isblank("))
            {
                int startIndex = formula.IndexOf("isblank(") + 7;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("isblank("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);
                    if (formula.Length > (tempFormula.Length + 9))
                    {
                        tempFormula = tempFormula + " , 1";
                    }
                    else
                    {
                        tempFormula = tempFormula + " , 0";
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
                formula = formula.Replace("isblank(", "dbo.isblankField(");
            }
            #endregion IsBlank()

            #region IsNumber()
            ////IsNumber Function
            if (formula.Contains("isnumber("))
            {
                int startIndex = formula.IndexOf("isnumber(") + 8;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("isnumber("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);
                    if (formula.Length > (tempFormula.Length + 10))
                    {
                        tempFormula = tempFormula + " , 1";
                    }
                    else
                    {
                        tempFormula = tempFormula + " , 0";
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
                formula = formula.Replace("isnumber(", "dbo.isnumberField(");
            }
            #endregion IsNumber()

            #region Value()
            // For val() function
            if (formula.Contains("value("))
            {
                int startIndex = formula.IndexOf("value(") + 6;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (!tempFormula.Contains("value("))
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);
                }
                string oldValue = formula.Substring(startIndex - 6, endIndex + 7);
                //formula = formula.Replace(oldValue, tempFormula.Trim());
                formula = formula.Replace("value(", "dbo.f9033_udf_RemoveCharValues(");
            }

            #endregion Value()

            #region Now()
            ////Current Date Function
            if (formula.ToUpper().Contains("NOW()"))
            {
                formula = formula.Replace("now()", "getdate()");
            }
            #endregion Now()

            #region Trim()
            ////Trim Function
            if (formula.Contains("trim("))
            {
                int startIndex = formula.IndexOf("trim(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = "ltrim( rtrim( " + tempFormula.Substring(tempFormula.IndexOf("trim(") + 5, endIndex - 5) + " ))";
                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }
            #endregion Trim()

            #region True()
            ////True Function
            if (formula.Contains("true()"))
            {
                if (formula.Trim().Length > 6)
                {
                    formula = formula.Replace("true()", "dbo.setBoolean('True', 1)");
                }
                else
                {
                    formula = formula.Replace("true()", "dbo.setBoolean('True', 0)");
                }
            }
            #endregion True()

            #region False()
            ////False Function
            if (formula.Contains("false()"))
            {
                if (formula.Trim().Length > 6)
                {
                    formula = formula.Replace("false()", "dbo.setBoolean('False', 1)");
                }
                else
                {
                    formula = formula.Replace("false()", "dbo.setBoolean('False', 0)");
                }
            }
            #endregion False()

            #region null()
            ////Null() Function
            if (formula.Contains("null("))
            {
                formula = formula.Replace("null()", "dbo.setnull()");
            }
            #endregion null()

            #region Mid()
            ////Mid Function
            if (formula.Contains("mid("))
            {
                int startIndex = formula.IndexOf("mid(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = tempFormula.Substring(tempFormula.IndexOf("mid(") + 4, endIndex - 4);
                string[] formulaArray;
                char[] deliMiter = { ',' };
                formulaArray = tempFormula.Split(deliMiter);
                if (formulaArray.Length > 0)
                {
                    tempFormula = string.Empty;
                    for (int j = 0; j < formulaArray.Length; j++)
                    {
                        if (j == 0)
                        {
                            if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                            {
                                tempFormula = tempFormula + "cast(" + formulaArray[j].ToString().Trim() + " AS varchar(max)), ";
                            }
                            else
                            {
                                tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'" + " , ";
                            }
                        }
                        else if (j == 1)
                        {
                            tempFormula = tempFormula + formulaArray[j].ToString().Trim() + ",";
                        }
                        else
                        {
                            tempFormula = tempFormula + formulaArray[j].ToString().Trim();
                        }
                    }
                }

                tempFormula = "substring(" + tempFormula + ")";
                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }
            #endregion Mid()

            #region Mod()
            if (formula.Contains("mod("))
            {
                int startIndex = formula.IndexOf("mod(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = tempFormula.Substring(tempFormula.IndexOf("mod(") + 4, endIndex - 4);
                string[] formulaArray;
                char[] deliMiter = { ',' };
                formulaArray = tempFormula.Split(deliMiter);
                if (formulaArray.Length > 0)
                {
                    tempFormula = string.Empty;
                    for (int j = 0; j < formulaArray.Length; j++)
                    {
                        if (j == 0)
                        {
                            if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString().Trim() + " % ";
                            }
                            else
                            {
                                tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'" + " % ";
                            }
                        }
                        else
                        {
                            tempFormula = tempFormula + formulaArray[j].ToString().Trim();
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }

            #endregion Mod()

            #region Replace()
            if (formula.Contains("replace("))
            {
                formula = formula.Replace("replace(", "STUFF(");
            }
            #endregion Replace()

            #region Fixed()
            if (formula.Contains("fixed("))
            {
                formula = formula.Replace("fixed(", "round(");
                formula = formula.Replace(" \"true\"", "1").Replace(" \"TRUE\"", "1").Replace(" \"True\"", "1");
                formula = formula.Replace(" \"false\"", "0").Replace(" \"FALSE\"", "0").Replace(" \"False\"", "0");
            }
            #endregion Fixed()

            if (formula.Contains("and(") || formula.Contains("or(") || formula.Contains("find(") || formula.Contains("isblank(") || formula.Contains("isnumber(") || formula.Contains("mid(") || formula.Contains("mod("))
            {
                this.ChangeFormulaformat(formula);
            }

            return formula;
        }
        #endregion Build Formula
        /// <summary>
        /// Handles the EditingControlShowing event of the QueryUpdateGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void QueryUpdateGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ComboBox cmb = e.Control as ComboBox;
                    cmb.DrawMode = DrawMode.OwnerDrawFixed;
                    cmb.DrawItem += cmb_DrawItem;
                    cmb.DropDown += OnDropDown;
                    cmb.DropDownClosed += OnDropDownClosed;
                    cmb.MouseLeave += OnMouseLeave;
                    //((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(QueryUpdateGrid_SelectionChangeCommitted);
                    //((ComboBox)e.Control).SelectionChangeCommitted +=new EventHandler(QueryUpdateGrid_SelectionChangeCommitted);
                } 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void OnDropDown(object sender, EventArgs e)
        {
            _dropDownOpen = true;
        }

        private void OnDropDownClosed(object sender, EventArgs e)
        {
            _dropDownOpen = false;
            ResetToolTip();
        }

        void cmb_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //ComboBox cmb = (ComboBox)sender;
                //System.Windows.Forms.ToolTip tiptol = new System.Windows.Forms.ToolTip();

                ////Label sourceLabel = (Label)sender;
                //string tempValue = string.Empty;
                //if (e.Index != -1)
                //{
                //    tempValue = cmb.GetItemText(cmb.Items[e.Index]);
                //}
                //Graphics graphics = this.CreateGraphics();
                //SizeF sizeF = graphics.MeasureString(tempValue, this.Font);
                //float preferredwidth = sizeF.Width;

                //if (preferredwidth > cmb.Width)
                //{
                //    tiptol.RemoveAll();
                //    tiptol.SetToolTip(cmb, tempValue);
                //}
                //else
                //{
                //    tiptol.RemoveAll();
                //}

                //graphics.Dispose();


                //ComboBox cmb = (ComboBox)sender;
                //string text = string.Empty;
                //System.Windows.Forms.ToolTip tiptol = new System.Windows.Forms.ToolTip();
                //if (e.Index >0)
                //{
                //    text = cmb.GetItemText(cmb.Items[e.Index]);
                //}
                //e.DrawBackground();

                //using (SolidBrush br = new SolidBrush(e.ForeColor))
                //{
                //    e.Graphics.DrawString(text, e.Font, br, e.Bounds);
                //}

                //if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                //{
                //    if (!text.Equals("<Select>"))
                //    {
                //        tiptol = new System.Windows.Forms.ToolTip();
                //        tiptol.ShowAlways = false;
                //        tiptol.Show(text, cmb, e.Bounds.X, e.Bounds.Y);
                //    }
                //}
                //else
                //{
                //    tiptol.Hide(cmb);
                //}
                //e.DrawFocusRectangle();


                ComboBox cmb = (ComboBox)sender;
                string text = string.Empty;
                if (tiptol == null)
                {
                    tiptol = new System.Windows.Forms.ToolTip();
                }
                if (e.Index == -1)
                {
                    return;
                }
                else
                {
                    text = cmb.GetItemText(cmb.Items[e.Index]);
                    e.DrawBackground();
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds.Location, SystemColors.Window);
                        if (_dropDownOpen)
                        {
                            Size szText = TextRenderer.MeasureText(text, cmb.Font);
                            if (szText.Width > cmb.Width && !_tooltipVisible)
                            {
                                if (text != "System.Data.DataRowView")
                                {
                                    ShowToolTip(text, this.PointToClient(MousePosition).X + Cursor.Size.Height, this.PointToClient(MousePosition).Y + Cursor.Size.Height);
                                }
                            }
                        }
                    }
                    else
                    {
                        ResetToolTip();
                        TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds.Location, cmb.ForeColor);
                    }
                }
                e.DrawFocusRectangle();
            }

            catch (Exception Excep)
            {
            }
        }

        private void ShowToolTip(string text, int x, int y)
        {
            tiptol.Show(text, this, x, y);
            _tooltipVisible = true;
        }

        private void ResetToolTip()
        {
            tiptol.SetToolTip(this, null);
            _tooltipVisible = false;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            ResetToolTip();
        }

        private void QueryUpdateGrid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Private Methods

        private void QueryUpdateGrid_MouseHover(object sender, EventArgs e)
        {
            (this.QueryEngineColumn as DataGridViewComboBoxColumn).ToolTipText = this.columnDetailsTable.ColumnNameColumn.ColumnName;
        }

        private void QueryUpdateGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //to be ignored.
        }
    }
}