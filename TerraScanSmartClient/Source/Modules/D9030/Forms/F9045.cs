//----------------------------------------------------------------------------------
// <copyright file="F9045.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9045.cs.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		           Description
// ----------		---------		       -----------------------------------------
// 14/10/2011       P. Manoj Kumar     Created 
//*********************************************************************************/


namespace D9030
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
    using System.Xml;
    using System.Windows.Forms;
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
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using System.Data.OleDb;
    using System.IO;
    using Excel = Microsoft.Office.Interop.Excel;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Win32;
    using System.Threading;

    /// <summary>
    /// Class file for F9045
    /// </summary>
    
    public partial class F9045 : Form
    {
        #region MemberVariables
        /// <summary>
        /// Used to GenricSearchID
        /// </summary>
        private int GenricSearchID;
        
        ///<summary>
        /// used to hold KeyId
        /// </summary>
        private int KeyId;

        private int rowindex;

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        ///<summary>
        /// Used to hold Config DataSet
        /// </summary>
        private F9045GenericSearchData.ConfigurationDataDataTable  genericData = new F9045GenericSearchData.ConfigurationDataDataTable(); 

        ///<summary>
        /// Used to hold the SearchResult Dataset
        /// </summary>
        private F9045GenericSearchData.SearchResultsDataTable searchData = new F9045GenericSearchData.SearchResultsDataTable();  

        /// <summary>
        /// form9033Control Control Name
        /// </summary>
        private F9045Controller form9045Control;

        #endregion MemberVariables

        #region Constructor

        public F9045()
        {
            InitializeComponent();
        }

        public F9045(int GenericSearch)
        {
            this.InitializeComponent();
            this.GenricSearchID = GenericSearch;  
        }

        #endregion Constructor

        #region F9045Control Property

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F9045Controller F9045Control
        {
            get { return this.form9045Control as F9045Controller; }

            set { this.form9045Control = value; }
        }

      #endregion F9045Control Property

        #region Properities

        /// <summary>
        /// Gets or sets the current statement id.
        /// </summary>
        /// <value>The current statement id.</value>
        public int KeyIDs
        {
            get
            {
                return KeyId;
            }

            set
            {
                KeyId = value;
                
            }
        }


        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return commandResult; }
            set { commandResult = value; }
        }

        #endregion

        #region Customize StateGridView

        private void CustomizeStateGridView()
        {
            this.StateCodeDataGridView.AutoGenerateColumns = false;
            this.StateCode.DataPropertyName = this.searchData.DataColumn.ColumnName;
            this.KeyID.DataPropertyName = this.searchData.KeyIDColumn.ColumnName;
            this.StateCodeDataGridView.PrimaryKeyColumnName = this.searchData.KeyIDColumn.ColumnName;    
            this.StateCode.DisplayIndex = 1;
            this.KeyID.DisplayIndex =0; 
        }


        #endregion Customize StateGridView

        /// <summary>
        /// Form Load for the form Generic Search Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F9045_Load(object sender, EventArgs e)
        {
            this.CustomizeStateGridView();
            this.genericData.Clear();
             
            this.genericData =this.form9045Control.WorkItem.F9045GetConfiguration(this.GenricSearchID).ConfigurationData;
            if (this.genericData.Rows.Count > 0)
            {
                this.Text ="TerraScan T2 - " +this.genericData.Rows[0]["FormTitle"].ToString();
                this.StateCode.HeaderText = this.genericData.Rows[0]["GridTitle"].ToString();
                this.StateCodeLabel.Text = this.genericData.Rows[0]["SearchTitle"].ToString();
                if (this.genericData.Rows[0]["IsSearchOnLoad"].Equals(true))
                {
                    this.SearchOperation(); 
                }
                else
                { 
                    this.StateCodeDataGridView.DataSource = this.searchData.DefaultView;
                    this.StateCodeDataGridView.ClearSelection();
                    this.ActiveControl = this.StateCodeTextBox;
                   // this.ActiveControl.Select();
                    this.ActiveControl.Focus();  
                }
                if (this.genericData.Rows[0]["IsReturnKey"].Equals(true))
                {
                    this.commandResult = string.Empty;
                }
                else
                {
                      this.KeyId =  0;  
                }
               
              
            }
        }
        
        /// <summary>
        /// Search Operation instantly
        /// </summary>
        private void SearchOperation()
        {
            string stateCode;
            if(!string.IsNullOrEmpty(this.StateCodeTextBox.Text))
            {
                 stateCode = this.StateCodeTextBox.Text; 
            }
            else
            {
                 stateCode = string.Empty;   
            }
            this.searchData.Clear();  
            this.searchData = this.form9045Control.WorkItem.F9045GetSearchResults(this.GenricSearchID, stateCode, TerraScanCommon.UserId).SearchResults;
            this.StateCodeDataGridView.DataSource = this.searchData.DefaultView;
            if (this.StateCodeDataGridView.OriginalRowCount > 0)
            {
                this.AcceptMasterNameButton.Enabled = true;
            }
            else
            {
                this.AcceptMasterNameButton.Enabled = false;
            }
            this.scrollBarVisibility();
   

        }
        private void scrollBarVisibility()
        {
            if (this.StateCodeDataGridView.OriginalRowCount > this.StateCodeDataGridView.NumRowsVisible)
            {
                this.stateVSscrollBar.Visible = false; 
            }
            else
            {
                this.stateVSscrollBar.Visible = true;
            }
        }


        /// <summary>
        /// Used to Close the form and nothing return to the calling form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.No;
                this.Close();

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
            this.SearchOperation();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void AcceptMasterNameButton_Click(object sender, EventArgs e)
        {
                try
                {
                    this.rowindex = this.StateCodeDataGridView.CurrentRowIndex;  
                    if (this.genericData.Rows[0]["IsReturnKey"].Equals(true))
                    {
                        this.KeyId = Convert.ToInt32(this.StateCodeDataGridView.Rows[this.rowindex].Cells["KeyID"].Value.ToString());
                        this.DialogResult = DialogResult.OK;
                        this.Close(); 
                    }
                    else
                    {
                        this.commandResult = this.StateCodeDataGridView.Rows[this.rowindex].Cells["StateCode"].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close(); 
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
        }

        private void StateCodeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.rowindex = this.StateCodeDataGridView.CurrentRowIndex;  
                     
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void StateCodeDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.rowindex = e.RowIndex;
                if (this.StateCodeDataGridView.OriginalRowCount > e.RowIndex)
                {
                    if (this.genericData.Rows[0]["IsReturnKey"].Equals(true))
                    {
                        this.KeyId = Convert.ToInt32(this.StateCodeDataGridView.Rows[this.rowindex].Cells["KeyID"].Value.ToString());
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        this.commandResult = this.StateCodeDataGridView.Rows[this.rowindex].Cells["StateCode"].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
