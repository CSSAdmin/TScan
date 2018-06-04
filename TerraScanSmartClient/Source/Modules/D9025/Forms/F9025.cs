//----------------------------------------------------------------------------------
// <copyright file="F9025.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9025.cs.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			 Author		          Description
// ----------	 ---------		      ----------------------------------------------
// 31/12/2008    A.Shanmuga Sundaram  Created// 
//*********************************************************************************/

namespace D9025
{
    #region Namespaces
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
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
    using TerraScan.Helper;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using System.IO;
    using TerraScan.Infrastructure.Interface.Constants;
    #endregion Namespaces

    /// <summary>
    /// F9025
    /// </summary>
    public partial class F9025 : BasePage
    {
        #region Variables

        /// <summary>
        /// userRole
        /// </summary>
       private bool userRole;

        /// <summary>
        /// F9076Controller
        /// </summary>
        private F9025Controller form9025Control;
        
        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F9025"/> class.
        /// </summary>
        public F9025()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Property

        /// <summary>
        /// Gets or sets the form9025 control.
        /// </summary>
        /// <value>The form9025 control.</value>
        [CreateNew]
        public F9025Controller Form9025Control
        {
            get { return this.form9025Control as F9025Controller; }
            set { this.form9025Control = value; }
        }

        #endregion property

        #region Page Load

        /// <summary>
        /// Handles the Load event of the F9025 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F9025_Load(object sender, EventArgs e)
        {
            try
            {
                this.AcceptButton = this.LoginValidationButton;
                this.CancelButton = this.CancelValidationButton;
                this.CancelValidationButton.Enabled = true;
                this.LoginValidationButton.Focus();             
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Page Load

        #region Button Events

        /// <summary>
        /// Handles the Click event of the LoginValidationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LoginValidationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (WSHelper.ValidateUser(this.UserNameTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim()))
                {
                    DataSet userInfoDataset = WSHelper.GetUserInformation(this.UserNameTextBox.Text.Trim(), TerraScanCommon.ApplicationId);
                    if (userInfoDataset.Tables[0].Rows.Count > 0)
                    {
                        this.userRole = Convert.ToBoolean(userInfoDataset.Tables[0].Rows[0]["IsAdministrator"].ToString());
                        TerraScanCommon.ValidationUserId = Convert.ToInt32(userInfoDataset.Tables[0].Rows[0]["UserID"].ToString());
                        if (this.userRole.Equals(true))
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("UserRoleValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (DialogResult.OK == dialogResult)
                            {
                                this.PasswordTextBox.Text = string.Empty;
                                this.UserNameTextBox.Focus();
                            }
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("UserLoginValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (DialogResult.OK == dialogResult)
                        {
                            this.PasswordTextBox.Text = string.Empty;
                            this.UserNameTextBox.Focus();
                        }                        
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("UserLoginValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (DialogResult.OK == dialogResult)
                    {
                        this.PasswordTextBox.Text = string.Empty;
                        this.PasswordTextBox.Focus();
                    }   
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelValidationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelValidationButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the UserNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.UserNameTextBox.Text.Trim()))
                {
                    this.LoginValidationButton.Enabled = true;
                }
                else
                {
                    this.LoginValidationButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Button Events        
    }
}