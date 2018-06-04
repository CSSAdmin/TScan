//--------------------------------------------------------------------------------------------
// <copyright file="F2552.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Statement Selection Form.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22-Sep-2011		Manoj Kumar .P          Created
//**********************************************************************************************/

namespace D2550
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;  
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;
    

    /// <summary>
    /// F2552
    /// </summary>
    public partial class F2552 : Form
    {
        #region Member Variables


        /// <summary>
        /// used to hold TypeID
        /// </summary>
        private int TypeId;


        /// <summary>
        /// used to hold ParcelNumber
        /// </summary>
        private string ParcelNumber;

        /// <summary>
        /// used to hold ParcelId
        /// </summary>
        private int ParcelId;

        /// <summary>
        /// used to hold editStatementList
        /// </summary>
        private DataTable  editStatement;
        
        /// <summary>
        /// used to hold StatementId
        /// </summary>
        private int OwnerId;
        
        /// <summary>
        /// used to hold StatementId
        /// </summary>
        private int StatementId;

        /// <summary>
        /// used to hold StatementList
        /// </summary>
        private string StatementList;

        /// <summary>
        /// Tax Roll Correction form2550Control Controller
        /// </summary>
        private F2552Controller form2552Control;

        /// <summary>
        /// used to hold the values for Edit Statement Data
        /// </summary>
        private F2552StatementSelectionData Statementrecordset = new F2552StatementSelectionData();

        /// <summary>
        /// used to hold the values for Edit DataSet
        /// </summary>
        private DataSet editDataSet = new DataSet();

        #endregion


        #region Constructor

        public F2552()
        {
          InitializeComponent();
        }
    
         /// <summary>
        /// Initializes a new instance of the <see cref="T:f9101"/> class.
        /// </summary>
         public F2552(string  parcelNumber,string RollYear,int ParcelID, int TypeID,string editStatementList)
        {
          this.InitializeComponent();
          this.ParcelLabel.Text ="Parcel: " + parcelNumber +" "+ RollYear;
         // this.ParcelNumber = ParcelNumber.Replace("/", "").Trim();    
          this.TypeId = TypeID;
          this.ParcelId = ParcelID;
          this.StatementList = editStatementList; 
          //DataSet editDataSet = new DataSet();
          if (!string.IsNullOrEmpty(editStatementList))
          {
              this.editDataSet.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(editStatementList));
          }
          else
          {
              this.editDataSet.Clear();  
          }

          this.AcceptButton = this.AcceptMasterNameButton;
        }
        #endregion

         #region Properties

         /// <summary>
         /// Gets or sets the F1031 control.
         /// </summary>
         /// <value>The F1031 control.</value>
         [CreateNew]
         public F2552Controller Form2552Control
         {
             get { return this.form2552Control as F2552Controller; }
             set { this.form2552Control = value; }
         }


         /// <summary>
         /// Gets or sets the parcel id.
         /// </summary>
         /// <value>The parcel id.</value>
         public int Statement
         {
             get { return this.StatementId; }
             set { this.StatementId = value; }
         }

         /// <summary>
         /// Gets or sets the command result.
         /// </summary>
         /// <value>The command result.</value>
         public int OwnerIDs
         {
             get { return this.OwnerId; }
             set { this.OwnerId = value; }
         }

    #endregion

         /// <summary>
        /// Form Load for the form F2552 Statement Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F2552_Load(object sender, EventArgs e)
        {
            this.customizeEditStatementSelectionGrid();
            this.LoadEditStatementDataGrid();
            this.CancelButton = this.MasterNameCancelButton;
            this.AcceptButton = this.AcceptMasterNameButton;
            
        }

        /// <summary>
        /// Customizes the parcel details grid view.
        /// </summary>
        private void customizeEditStatementSelectionGrid()
        {
            this.MasterNameDataGridView.AutoGenerateColumns = false;
            this.OwnerID.DataPropertyName = this.Statementrecordset.StatementDataTable.OwnerIDColumn.ColumnName;
            this.OwnerName.DataPropertyName = this.Statementrecordset.StatementDataTable.OwnerNameColumn.ColumnName;
            this.OwnerPercent.DataPropertyName = this.Statementrecordset.StatementDataTable.OwnerPercentColumn.ColumnName;
            this.StatementNumber.DataPropertyName = this.Statementrecordset.StatementDataTable.StatementNumberColumn.ColumnName;
            this.StatementID.DataPropertyName = this.Statementrecordset.StatementDataTable.StatementIDColumn.ColumnName;
            this.Parcel.DataPropertyName = this.Statementrecordset.StatementDataTable.ParcelIDColumn.ColumnName;
            this.Type.DataPropertyName = this.Statementrecordset.StatementDataTable.TypeIDColumn.ColumnName;
            this.IsEdit.DataPropertyName = this.Statementrecordset.StatementDataTable.IsEditColumn.ColumnName;     
            this.OwnerID.DisplayIndex = 0;
            this.OwnerName.DisplayIndex = 1;
            this.OwnerPercent.DisplayIndex = 2;
            this.StatementNumber.DisplayIndex = 3;
            this.StatementID.DisplayIndex = 4;
            this.Parcel.DisplayIndex = 5;
            this.Type.DisplayIndex = 6;
            this.IsEdit.DisplayIndex = 7; 

        }


        /// <summary>
        /// To Laod Parcel Ownership Grid
        /// </summary>
        private void LoadEditStatementDataGrid()
        {
            this.Statementrecordset.Clear();
            this.Statementrecordset = this.form2552Control.WorkItem.F2552_ListStatementSelectionDetails(this.ParcelId, this.TypeId, TerraScanCommon.UserId);
            if (!string.IsNullOrEmpty(this.StatementList))
            {
                if (this.editDataSet.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < this.Statementrecordset.StatementDataTable.Rows.Count; i++)
                    //{
                    for (int j = 0; j < this.editDataSet.Tables[0].Rows.Count; j++)
                    {
                        DataRow[] dr = this.Statementrecordset.StatementDataTable.Select("StatementID=" + this.editDataSet.Tables[0].Rows[j]["StatementID"].ToString()+" AND OwnerID=" +this.editDataSet.Tables[0].Rows[j]["OwnerID"].ToString());
                        if (dr.Length > 0)
                        {
                            int rowindex = this.Statementrecordset.StatementDataTable.Rows.IndexOf(dr[0]);
                            this.Statementrecordset.StatementDataTable.Rows[rowindex]["IsEdit"] = true;
                        }

                    }
                    //}
                }
            }
            this.MasterNameDataGridView.DataSource = this.Statementrecordset.StatementDataTable.DefaultView;
            if (this.MasterNameDataGridView.OriginalRowCount <= 0)
            {
                this.AcceptMasterNameButton.Enabled = false;
            }
            else
            {
                this.AcceptMasterNameButton.Enabled = true;
            }
            this.ScrollBarVisibility();
            

        }

         /// <summary>
        /// Scrolls the bar visibility.
        /// </summary>
        private void ScrollBarVisibility()
        {
            if (this.MasterNameDataGridView.OriginalRowCount > this.MasterNameDataGridView.NumRowsVisible)
            {
                this.MasterNameVerticalScroll.Visible = false;
                //this.vScrollBar1.Visible = false;
                //this.panel7.BorderStyle = BorderStyle.None;
                //DataGridViewColumnCollection columns = this.MasterNameDataGridView.Columns;
                //columns[this.Statementrecordset.ListParcelDetailsTable.DistrictColumn.ColumnName].Width = 146;
            }
            else
            {
                this.MasterNameVerticalScroll.Visible = true;
                 //this.vScrollBar1.Visible = false;
                //this.panel7.BorderStyle = BorderStyle.None;
                ////DataGridViewColumnCollection columns = this.MasterNameDataGridView.Columns;
                //columns[this.Statementrecordset.StatementDataTable.DistrictColumn.ColumnName].Width = 150;

            }
        }

        private void MasterNameCancelButton_Click(object sender, EventArgs e)
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

        private void MasterNameDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AcceptMasterNameButton_Click(object sender, EventArgs e)
         {
            int currentRow=this.MasterNameDataGridView.CurrentRowIndex;
            this.OwnerId = Convert.ToInt32(this.MasterNameDataGridView.Rows[currentRow].Cells["OwnerID"].Value.ToString());
            this.StatementId = Convert.ToInt32(this.MasterNameDataGridView.Rows[currentRow].Cells["StatementID"].Value.ToString());
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void MasterNameDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((this.MasterNameDataGridView.Rows[e.RowIndex].Cells["IsEdit"].Value != null) && this.MasterNameDataGridView.Rows[e.RowIndex].Cells["IsEdit"].Value.Equals(true))
                {
                         this.MasterNameDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(162, 198, 113);
                   
                                              
                 }
                 else
                    {
                        //this.ParcelDetailsGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                        if (((e.RowIndex) % 2).Equals(0))
                        {
                            this.MasterNameDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            this.MasterNameDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
                        }

                    }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void F2552_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void MasterNameDataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyData.Equals(Keys.Enter))
                {
                    this.AcceptMasterNameButton_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void MasterNameDataGridView_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}

