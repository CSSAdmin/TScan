//--------------------------------------------------------------------------------------------
// <copyright file="ExceptionViewer.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the CommentsForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 29 April 06		Thilakraj R		        Created
//*********************************************************************************/
namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Utilities; 
 
    /// <summary>
    /// ExceptionViewer used to display custom error
    /// </summary>
    public partial class ExceptionViewer : Form
    {
        /// <summary>
        /// Local ActionType variable
        /// </summary>
        private ExceptionManager.ActionType tempActionType;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExceptionViewer"/> class.
        /// </summary>
        public ExceptionViewer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExceptionViewer"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="actionType">Type of the action.</param>
        public ExceptionViewer(string errorMessage, ExceptionManager.ActionType actionType)
        {
            this.InitializeComponent();
            this.ErrorMessageLabel.Text = errorMessage.Replace("\\n", "\n");

            // this.ErrorMessageLabel.Text = errorMessage;
            this.tempActionType = actionType;
        }

        /// <summary>
        /// Handles the Click event of the MoreCommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MoreCommentButton_Click(object sender, EventArgs e)
        {
            if (this.MoreCommentButton.Text.Equals(SharedFunctions.GetResourceString("OpenComments")))
            {
                this.Height = this.Height + this.CommentTextBox.Height + 10;
                this.MoreCommentButton.Text = SharedFunctions.GetResourceString("CloseComments");
            }
            else
            {
                this.Height = this.Height - this.CommentTextBox.Height - 10;
                this.MoreCommentButton.Text = SharedFunctions.GetResourceString("OpenComments");
            }
        }

        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            switch (this.tempActionType)
            {
                case ExceptionManager.ActionType.CloseCurrentForm:
                    {
                        this.Owner.Close();
                        break;
                    }

                case ExceptionManager.ActionType.CloseApplication:
                    {
                        Application.Exit(new CancelEventArgs(true));
                        break;
                    }
            }
        }
    }   
}