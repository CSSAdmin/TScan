//-------------------------------------------------------------------------------------------
// <copyright file="F9052.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This file contains methods for the QueryStringForm.</summary>
// 
// VERSION	DESCRIPTION
//     
// 
//-------------------------------------------------------------------------------------------
//***********************************************************************************/
namespace D9050
{
    #region NameSpace
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using System.Collections;

    #endregion

    /// <summary>
    /// Public Class QueryStringForm
    /// </summary>
   public partial class F9052 : Form
    {
        #region variables

        /// <summary>
        /// controller F9052
        /// </summary>
        private F9052Controller form9052Control;

        /// <summary>
        /// where condition of current recordset
        /// </summary>
        private string currentQueryWhereCondition;

        /// <summary>
        /// user defined where condition
        /// </summary>
        private string userDefinedWhereCondition;

        /// <summary>
        /// current QueryingFieldsDataTable
        /// </summary>
        private DataTable currentQueryingFieldsDataTable = new DataTable();

        #endregion

        #region constructor

        /// <summary>
        /// query by from constructor with parameters
        /// </summary>
        /// <param name="queryWhereCondition">queryWhereCondition</param>
        /// <param name="snapshatName">snapshatName</param>
        /// <param name="snapshotDescription">snapshotDescription</param>
        /// <param name="queryingFieldsDataTable">queryingFieldsDataTable</param>
        public F9052(string queryWhereCondition, string snapshatName, string snapshotDescription, DataTable queryingFieldsDataTable)
        {
            this.InitializeComponent();
            this.currentQueryingFieldsDataTable = queryingFieldsDataTable;
            this.CurrentQueryWhereCondition = queryWhereCondition;
            this.SnapshotNameValueLabel.Text = snapshatName;
            this.SnapshotDescriptionValueLabel.Text = snapshotDescription;
            this.CurrentQueryTextBox.Text = queryWhereCondition;
            this.CurrentQueryTextBox.Focus();
            this.CancelButton = this.CloseRequeryButton;
            this.AcceptButton = this.RequeryButton;
            this.CurrentQueryTextBox.Focus();
            if (String.IsNullOrEmpty(queryWhereCondition.Trim()))
            {
               this.RequeryButton.Enabled = false;
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// where condition of query to be executed
        /// </summary>       
        public string CurrentQueryWhereCondition
        {
            get { return this.userDefinedWhereCondition; }
            set { this.userDefinedWhereCondition = value; }
        }

        /// <summary>
        /// user defined where condition 
        /// </summary>       
        public string UserDefinedWhereCondition
        {
            get { return this.currentQueryWhereCondition; }
            set { this.currentQueryWhereCondition = value; }
        }

        ///// <summary>
        ///// user entered where condition 
        ///// </summary>       
        // public string UserEnteredWhereCondition
        // {
        //    get { return this.userEnteredWhereCondition; }
        //    set { this.userEnteredWhereCondition = value; }
        // }   

        /// <summary>
        /// For F9052Control
        /// </summary>
        [CreateNew]
        public F9052Controller Form9052Control
        {
            get { return this.Form9052Control as F9052Controller; }
            set { this.form9052Control = value; }
        }       

        #endregion

        #region Close Requery

        /// <summary>
        /// Handles the click Event for closeRequreybutton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void CloseRequeryButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        #endregion

        #region Requery

        /// <summary>
        /// Handles the click Event for Requreybutton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void RequeryButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.CurrentQueryTextBox.Text.Trim()))
                {
                    ArrayList tempArr = SharedFunctions.GenerateUserWhereCondition(this.CurrentQueryTextBox.Text.Trim(), this.currentQueryingFieldsDataTable);
                    if (Object.Equals(tempArr[0], false))
                    {
                        this.Visible = false;
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidQuery") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = true;
                        this.CurrentQueryTextBox.Focus();
                    }
                    else
                    {
                        this.CurrentQueryWhereCondition = tempArr[1].ToString();
                        this.UserDefinedWhereCondition = tempArr[tempArr.Count - 1].ToString();
                        this.DialogResult = DialogResult.Yes;
                        this.Visible = false;
                    }
                }
            }
            catch
            {
                ////error on invalid query
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidQuery") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
        /// <summary>
        /// Handeled Query TextBoxChanged Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CurrentQueryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty((sender as TextBox).Text.Trim()))
            {
                this.RequeryButton.Enabled = false;
            }
            else
            {
                this.RequeryButton.Enabled = true;
            }
        }

        /// <summary>
        /// Handle the QueryString load event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">instance containing the event data</param>
        private void QueryString_Load(object sender, EventArgs e)
        {
            this.CurrentQueryTextBox.Focus();
        }

       /// <summary>
       /// handle the querystring_shown event
       /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">instance containing the event data</param>
        private void QueryString_Shown(object sender, EventArgs e)
        {
            this.CurrentQueryTextBox.Focus();
        }

        /// <summary>
        /// Handles the MouseEnter event of the snapshotDescriptionValueLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapshotDescriptionValueLabel_MouseEnter(object sender, EventArgs e)
        {
            Control sourceControl = sender as Control;
            int textLength = 40;

            if (this.SnapshotDescriptionValueLabel.Text.Length > textLength)
            {
                this.DescriptionToolTip.SetToolTip(this.SnapshotDescriptionValueLabel, this.SnapshotDescriptionValueLabel.Text);
            }
            else
            {
                this.DescriptionToolTip.RemoveAll();
            }
        }         
    }
}