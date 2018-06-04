//--------------------------------------------------------------------------------------------
// <copyright file="F4992.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Legal Comments.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11/02/2008       KUPPUSAMY.B	    Created
//*********************************************************************************/


namespace D49910
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;

    /// <summary>
    /// F4992
    /// </summary>
    public partial class F4992 : Form
    {
        #region Variable

        /// <summary>
        /// returnTextValue
        /// </summary>
        private string returnTextValue;

        /// <summary>
        /// F49912LegalData
        /// </summary>
        private F49912LegalData form49912LegalData = new F49912LegalData();        

        /// <summary>
        /// PageModeTypes
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        #endregion Variable

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="F4992"/> class.
        /// </summary>
        public F4992()
        {
            this.InitializeComponent();            
        }        

        #endregion Constructor 

        #region Property
        
        /// <summary>
        /// Gets or sets the return text value.
        /// </summary>
        /// <value>The return text value.</value>
        public string ReturnTextValue
        {
            get 
            { 
                return this.returnTextValue; 
            }

            set
            {
                this.returnTextValue = value;
                this.LegalCommentsTextBox.Text = value;
            }                
        }        
          
        #endregion Property        

        #region Events 

        /// <summary>
        /// Handles the Load event of the F4992 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F4992_Load(object sender, EventArgs e)
        {
            try
            {                
                this.AcceptButton = this.CommentsOkButton;
                this.CancelButton = this.CommentsCancelButton;

                if (this.LegalCommentsTextBox.Text.Trim() != string.Empty)
                {
                    this.CommentsOkButton.Enabled = false;
                    this.CommentsCancelButton.Enabled = true;
                }
                else
                {
                    this.CommentsOkButton.Enabled = false;
                    ////this.CommentsCancelButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }            
        }

        /// <summary>
        /// Handles the Click event of the CommentsOkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommentsOkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.LegalCommentsTextBox.Text.Trim() != string.Empty)
                {
                    this.ReturnCommentsToGrid();
                }
                else
                {
                    this.returnTextValue = this.LegalCommentsTextBox.Text.Trim();
                    this.CommentsOkButton.Enabled = false;
                    ////this.CommentsCancelButton.Enabled = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }            
        }

        /// <summary>
        /// Handles the Click event of the CommentsCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommentsCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.returnTextValue = this.LegalCommentsTextBox.Text.Trim();
                this.CommentsOkButton.Enabled = false;
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }       
        }

        /// <summary>
        /// Handles the TextChanged event of the EquipmentNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EquipmentNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                if (this.LegalCommentsTextBox.Text.Trim() != string.Empty)
                {
                    this.CommentsOkButton.Enabled = true;
                    ////this.CommentsCancelButton.Enabled = true;
                }
                else
                {
                    this.CommentsOkButton.Enabled = true;
                    this.CommentsCancelButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }       
        }                

        #endregion Events        

        #region Methods 

        /// <summary>
        /// Returns the comments to grid.
        /// </summary>
        private void ReturnCommentsToGrid()
        {
            this.returnTextValue = this.LegalCommentsTextBox.Text.Trim();
            this.CommentsOkButton.Enabled = false;
            ////this.CommentsCancelButton.Enabled = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion Methods         

        /// <summary>
        /// Handles the FormClosed event of the F4992 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void F4992_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}