//--------------------------------------------------------------------------------------------
// <copyright file="F9041.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9610.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25/11/2008       D.LathaMaheswari            Created// 
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
    using System.IO;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Class file for F9041
    /// </summary>
    public partial class F9041 : BasePage
    {
        #region Variables
        /// <summary>
        /// F9041Controller
        /// </summary>
        private F9041Controller form9041Control;

        /// <summary>
        /// DataSet for QueryView Description 
        /// </summary>
        private F9041QueryViewDescriptionData queryViewData = new F9041QueryViewDescriptionData();

        /// <summary>
        /// KeyID
        /// </summary>
        private int keyid;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9041"/> class.
        /// </summary>
        public F9041()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9610"/> class.
        /// </summary>
        /// <param name="queryViewId">QueryViewID</param>
        public F9041(int queryViewId)
        {
            this.InitializeComponent();
            this.keyid = queryViewId;
        }

        #endregion
        
        #region Property

        /// <summary>
        /// For F9041Control
        /// </summary>
        [CreateNew]
        public F9041Controller Form9041Control
        {
            get { return this.form9041Control as F9041Controller; }
            set { this.form9041Control = value; }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F9041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9041_Load(object sender, EventArgs e)
        {
            try
            {
                this.CancelButton = this.FormCancelButton;
                this.FormCancelButton.Visible = true;
                this.FormCancelButton.Enabled = true;
                this.GetDescriptionData();
                this.QueryViewNameTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Handles the FormClosing event of the F9040 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9041_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
            {
                if (e.CloseReason.Equals(CloseReason.UserClosing))
                {
                    e.Cancel = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the FormCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel; 
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the description data.
        /// </summary>
        private void GetDescriptionData()
        {
            this.queryViewData = this.form9041Control.WorkItem.F9041GetQueryDescription(this.keyid);

            if (this.queryViewData.GetQueryViewDescription.Rows.Count > 0)
            {
                this.QueryViewNameTextBox.Text = this.queryViewData.GetQueryViewDescription.Rows[0][this.queryViewData.GetQueryViewDescription.QueryViewNameColumn.ColumnName].ToString();
                this.DescriptionTextBox.Text = this.queryViewData.GetQueryViewDescription.Rows[0][this.queryViewData.GetQueryViewDescription.QueryViewDescriptionColumn.ColumnName].ToString();
            }
        }

        #endregion Methods
    }
}