//--------------------------------------------------------------------------------------------
// <copyright file="F1202.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Reverse GL Post
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Sept 06		KRISHNA ABBURI	    Created
// 1 Feb 07		    ranjani             1202 3.1 and 3.2 issue fixed
//*********************************************************************************/

namespace D1200
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Collections;
    using System.IO;
    using System.Configuration;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    
    /// <summary>
    /// f1202 class
    /// </summary>
    public partial class F1202 : Form
    {
        #region Variables

        /// <summary>
        /// form1202Control varaible 
        /// </summary>
        private F1202Controller form1202Control;

        /// <summary>
        /// postingErrorsDataset Contains PostId Details Details 
        /// </summary>
        private PostIdDetailsData postIdDetailsDataset = new PostIdDetailsData();

        /// <summary>
        /// PostId
        /// </summary>
        private int postId;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1202"/> class.
        /// </summary>
        public F1202()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1202"/> class.
        /// </summary>
        /// <param name="postId">The post id.</param>
        public F1202(int postId)
        {
            InitializeComponent();
            this.postId = postId;
        }

        #endregion

        #region properites

        /// <summary>
        /// Gets or sets the F1202 controll.
        /// </summary>
        /// <value>The F1202 controll.</value>
        [CreateNew]
        public F1202Controller F1202Controll
        {
            get { return this.form1202Control as F1202Controller; }
            set { this.form1202Control = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fills the text boxes.
        /// </summary>
        private void FillTheTextBoxes()
        {
            try
            {
            this.postIdDetailsDataset = this.F1202Controll.WorkItem.GetPostIdDetails(this.postId);

                if (this.postIdDetailsDataset.PostingIdDetails.Count > 0)
                {
                    this.PostIdTextBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.PostIDColumn].ToString();
                    this.PostDateTextBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.PostDateColumn].ToString();
                    this.PosttypeTextBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.PostNameColumn].ToString();
                    this.RanOnTextBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.RanOnColumn].ToString();
                    this.AmountTotalTxtBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.AmountTotalColumn].ToString();
                    this.RollYearTxtBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.RollYearColumn].ToString();
                    this.userTxtBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.Name_DisplayColumn].ToString();
                    this.DescTextBox.Text = this.postIdDetailsDataset.PostingIdDetails[0][this.postIdDetailsDataset.PostingIdDetails.DescriptionColumn].ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            }
              
        #endregion
        
        #region Events

            /// <summary>
            /// Handles the Load event of the F1202 control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
            private void F1202_Load(object sender, EventArgs e)
            {
                try
                {
                    this.FillTheTextBoxes();
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }

            /// <summary>
            /// Handles the Click event of the ReversePostButton control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
            private void ReversePostButton_Click(object sender, EventArgs e)
            {
                try
                {
                    if (MessageBox.Show("You are about to permanently reverse all entries in the General Ledger related to the current Posting. Are you sure you want to do this?", "TerraScan – Reverse GL Post", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.form1202Control.WorkItem.InsertReverseGLPost(this.postId, TerraScanCommon.UserId);
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
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
            /// Handles the Click event of the GlPostCancelButton control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
            private void GlPostCancelButton_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }

        #endregion
    }
}