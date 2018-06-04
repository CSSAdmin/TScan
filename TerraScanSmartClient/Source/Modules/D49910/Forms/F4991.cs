//--------------------------------------------------------------------------------------------
// <copyright file="F4991.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F4991.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 31/12/2007       R.Malliga            Created// 
//*********************************************************************************/

namespace D49910
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
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
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;

    /// <summary>
    /// Class file for F4991
    /// </summary>
    public partial class F4991 : Form
    {
        #region Variables

        /// <summary>
        /// instCopyLoadDatatable
        /// </summary>
        private DataTable instCopyDatatable = new DataTable();

        /// <summary>
        /// instCopyLoaddatarow
        /// </summary>
        private DataRow instCopydatarow;

        /// <summary>
        /// instCopyDatatable
        /// </summary>
        private DataTable instCopyLoadDatatable = new DataTable();

        /// <summary>
        /// instCopydatarow
        /// </summary>
        private DataRow instCopyLoaddatarow;

        /// <summary>
        /// instCopy
        /// </summary>
        private string instCopy;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F4991"/> class.
        /// </summary>
        public F4991()
        {
            InitializeComponent();
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the inst copy.
        /// </summary>
        /// <value>The inst copy.</value>
        public string InstCopy
        {
            get { return this.instCopy; }
            set { this.instCopy = value; }
        }
        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F4991 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F4991_Load(object sender, EventArgs e)
        {
            try
            {
                this.CustomiseDataGrid();

                this.instCopyLoadDatatable.Columns.Add(SharedFunctions.GetResourceString("F4991Copyid"));
                this.instCopyLoadDatatable.Columns.Add(SharedFunctions.GetResourceString("F4991Copy"));

                this.instCopyLoaddatarow = this.instCopyLoadDatatable.NewRow();
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copyid")] = true;
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copy")] = SharedFunctions.GetResourceString("F4991Instrument");
                this.instCopyLoadDatatable.Rows.Add(this.instCopyLoaddatarow);

                this.instCopyLoaddatarow = this.instCopyLoadDatatable.NewRow();
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copyid")] = true;
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copy")] = SharedFunctions.GetResourceString("F4991Grantor");
                this.instCopyLoadDatatable.Rows.Add(this.instCopyLoaddatarow);

                this.instCopyLoaddatarow = this.instCopyLoadDatatable.NewRow();
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copyid")] = true;
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copy")] = SharedFunctions.GetResourceString("F4991Grantee");
                this.instCopyLoadDatatable.Rows.Add(this.instCopyLoaddatarow);

                this.instCopyLoaddatarow = this.instCopyLoadDatatable.NewRow();
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copyid")] = true;
                this.instCopyLoaddatarow[SharedFunctions.GetResourceString("F4991Copy")] = SharedFunctions.GetResourceString("F4991Legal");
                this.instCopyLoadDatatable.Rows.Add(this.instCopyLoaddatarow);

                this.InstCopyGrid.DataSource = this.instCopyLoadDatatable;

                this.InstCopyOkButton.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Customises the data grid.
        /// </summary>
        private void CustomiseDataGrid()
        {
            DataGridViewColumnCollection columns = this.InstCopyGrid.Columns;
            columns[SharedFunctions.GetResourceString("F4991Copyid")].DataPropertyName = SharedFunctions.GetResourceString("F4991Copyid");
            columns[SharedFunctions.GetResourceString("F4991Copy")].DataPropertyName = SharedFunctions.GetResourceString("F4991Copy");
            columns[SharedFunctions.GetResourceString("F4991Copyid")].DisplayIndex = 0;
            columns[SharedFunctions.GetResourceString("F4991Copy")].DisplayIndex = 1;
        }
        #endregion

        #region Button Events

        /// <summary>
        /// Handles the Click event of the InstCopyCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstCopyCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the InstCopyOkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstCopyOkButton_Click(object sender, EventArgs e)
        {
            this.instCopy = string.Empty;

            if (this.instCopyDatatable.Columns.Count == 0)
            {
                for (int i = 0; i <= this.InstCopyGrid.Rows.Count - 1; i++)
                {
                    this.instCopyDatatable.Columns.Add(this.InstCopyGrid.Rows[i].Cells[this.Copy.Name].Value.ToString());
                }
            }

            for (int i = 0; i <= this.InstCopyGrid.Rows.Count - 1; i++)
            {
                if (this.InstCopyGrid.Rows[i].Cells[this.Copyid.Name].Value.ToString().Trim() == "True")
                {
                    this.instCopydatarow = this.instCopyDatatable.NewRow();
                    this.instCopydatarow[this.InstCopyGrid.Rows[i].Cells[this.Copy.Name].Value.ToString()] = 1;
                    this.instCopyDatatable.Rows.Add(this.instCopydatarow);
                }
                else
                {
                    this.instCopydatarow = this.instCopyDatatable.NewRow();
                    this.instCopydatarow[this.InstCopyGrid.Rows[i].Cells[this.Copy.Name].Value.ToString()] = 0;
                    this.instCopyDatatable.Rows.Add(this.instCopydatarow);
                }
            }

            this.instCopy = TerraScanCommon.GetXmlString(this.instCopyDatatable);
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the CellContentClick event of the InstCopyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InstCopyGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selected = 0;
                this.InstCopyGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i <= this.InstCopyGrid.Rows.Count - 1; i++)
                {
                    if (!string.IsNullOrEmpty(this.InstCopyGrid.Rows[i].Cells[this.Copyid.Name].Value.ToString().Trim()))
                    {
                        if (this.InstCopyGrid.Rows[i].Cells[this.Copyid.Name].Value.ToString().Trim() == "True")
                        {
                            selected++;
                        }
                    }
                }

                if (selected > 0)
                {
                    this.InstCopyOkButton.Enabled = true;
                }
                else
                {
                    this.InstCopyOkButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentDoubleClick event of the InstCopyGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InstCopyGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selected = 0;
                this.InstCopyGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i <= this.InstCopyGrid.Rows.Count - 1; i++)
                {
                    if (!string.IsNullOrEmpty(this.InstCopyGrid.Rows[i].Cells[this.Copyid.Name].Value.ToString().Trim()))
                    {
                        if (this.InstCopyGrid.Rows[i].Cells[this.Copyid.Name].Value.ToString().Trim() == "True")
                        {
                            selected++;
                        }
                    }
                }

                if (selected > 0)
                {
                    this.InstCopyOkButton.Enabled = true;
                }
                else
                {
                    this.InstCopyOkButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}