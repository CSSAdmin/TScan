//--------------------------------------------------------------------------------------------
// <copyright file="F4990.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F4990.cs.
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
    /// Class file for F4990
    /// </summary>
    public partial class F4990 : Form
    {
        #region Variables

        /// <summary>
        /// InstReviewedStatusId
        /// </summary>
        private string instReviewedStatusId;

        /// <summary>
        /// LastInstReviewedStatusId
        /// </summary>
        /// 
        private string lastInstReviewedStatusId;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F4990"/> class.
        /// </summary>
        public F4990()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F4990"/> class.
        /// </summary>
        /// <param name="lastInstReviewedStatusId">The last inst reviewed status id.</param>
        public F4990(string lastInstReviewedStatusId)
        {
            this.InitializeComponent();
            this.lastInstReviewedStatusId = lastInstReviewedStatusId; 
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the inst reviewed status.
        /// </summary>
        /// <value>The inst reviewed status.</value>
        public string InstReviewedStatus
        {
            get { return this.instReviewedStatusId; }
            set { this.instReviewedStatusId = value; }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// Handles the Click event of the InstReviewedYesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstReviewedYesButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.instReviewedStatusId = string.Empty;
                this.InstReviewedLabel.Text = SharedFunctions.GetResourceString("F4990Reviewed");
                this.InstReviewedLabel.BackColor = System.Drawing.Color.FromArgb((int)(byte)71, (int)(byte)133, (int)(byte)85);
                this.instReviewedStatusId = SharedFunctions.GetResourceString("Yes");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            } 
        }

        /// <summary>
        /// Handles the Click event of the InstReviewedNoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstReviewedNoButton_Click(object sender, EventArgs e)
        {
         try
         {
             this.instReviewedStatusId = string.Empty;
             this.InstReviewedLabel.Text = SharedFunctions.GetResourceString("F4990NotReviewed");
            this.InstReviewedLabel.BackColor = System.Drawing.Color.FromArgb((int)(byte)128, (int)(byte)0, (int)(byte)0);
            this.instReviewedStatusId = SharedFunctions.GetResourceString("No");
            this.DialogResult = DialogResult.OK;
         }
        catch (Exception ex)
        {
            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        }
    }

        /// <summary>
    /// Handles the Click event of the InstReviewedCancelButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InstReviewedCancelButton_Click(object sender, EventArgs e)
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

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F4990 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F4990_Load(object sender, EventArgs e)
        {
          try
           {
               this.InstReviewedLabel.Text = this.lastInstReviewedStatusId;

               if (this.lastInstReviewedStatusId == "Yes")
               {
                   this.InstReviewedLabel.BackColor = System.Drawing.Color.FromArgb((int)(byte)71, (int)(byte)133, (int)(byte)85);
                   this.InstReviewedLabel.Text = SharedFunctions.GetResourceString("F4990Reviewed"); 
               }
               else
               {
                   this.InstReviewedLabel.BackColor = System.Drawing.Color.FromArgb((int)(byte)128, (int)(byte)0, (int)(byte)0);
                   this.InstReviewedLabel.Text = SharedFunctions.GetResourceString("F4990NotReviewed"); 
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