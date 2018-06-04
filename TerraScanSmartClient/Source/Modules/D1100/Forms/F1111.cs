//--------------------------------------------------------------------------------------------
// <copyright file="f1111.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Information about the Application.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 July 06		Krishna A		        Created
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using D1100;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Text.RegularExpressions;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// Form f1111
    /// </summary>
    public partial class F1111 : Form
    {
        #region Variable

        /// <summary>
        /// represents statementID
        /// </summary>
        private int statementID = 0;

        /// <summary>
        /// represents isTreasurer
        /// </summary>
        private int treasurerStatus = 0;

        /// <summary>
        /// represents statusID
        /// </summary>
        private int statusID = 0;

        /// <summary>
        /// represents status
        /// </summary>
        private string status = string.Empty;

        /// <summary>
        /// Used to store the user name 
        /// </summary>
        private string username;

        /// <summary>
        /// Used to store the UserId
        /// </summary>
        private int userId;

        /// <summary>
        /// Used to store the UpdateTime
        /// </summary>
        private string updateTime = string.Empty;

        /// <summary>
        /// represents f1111Controller
        /// </summary>
        private F1111Controller form1111Control;

        /// <summary>
        /// Used to save isstatusAvailable
        /// </summary>
        private bool isstatusAvailable;

        /// <summary>
        /// Used to store availableStatusId
        /// </summary>
        private int availableStatusId;

        /// <summary>
        /// used to store the availableStatus
        /// </summary>
        private string availableStatus;

        /// <summary>
        /// used to store the avilableUsername
        /// </summary>
        private string avilableUsername;

        /// <summary>
        /// Used to store the avilableUpdateDatetime
        /// </summary>
        private string avilableUpdateDatetime = string.Empty;

        #endregion Variable

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1111"/> class.
        /// </summary>
        public F1111()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1111"/> class.
        /// </summary>
        /// <param name="frmid">The frmid.</param>
        /// <param name="statementid">The statementid.</param>
        /// <param name="istreasurer">The istreasurer.</param>
        public F1111(string frmid, int statementid, int istreasurer)
        {
            this.InitializeComponent();
            this.statementID = statementid;
            this.treasurerStatus = istreasurer;
            this.formIDLabel.Text = frmid;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1111"/> class.
        /// </summary>
        /// <param name="frmid">The frmid.</param>
        /// <param name="statementid">The statementid.</param>
        /// <param name="istreasurer">The istreasurer.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="status">The status.</param>
        public F1111(string frmid, int statementid, int istreasurer, int statusId, string status, string lastUpdatedTime, string userName)
        {
            this.InitializeComponent();

            this.isstatusAvailable = true;
            ////this.availableStatus = status;
            this.status = status;
            this.availableStatusId = statusId;

            this.statementID = statementid;
            this.treasurerStatus = istreasurer;
            this.formIDLabel.Text = frmid;

            this.avilableUpdateDatetime = lastUpdatedTime;
            this.avilableUsername = userName;
        }


        #endregion Constructors

        #region Properties


        /// <summary>
        /// Gets or sets the status id.
        /// </summary>
        /// <value>The status id.</value>
        public int StatusId
        {
            get { return this.statusID; }
            set { this.statusID = value; }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        public int StatusValidationUserID
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string StatusValidationUserName
        {
            get { return this.username; }
            set { this.username = value; }
        }

        /// <summary>
        /// Gets or sets the UpdatedTime.
        /// </summary>
        /// <value>The status.</value>
        public string StatusValidationUpdatedTime
        {
            get { return this.updateTime; }
            set { this.updateTime = value; }
        }

        /// <summary>
        /// Gets or sets the F1111 control.
        /// </summary>
        /// <value>The F1111 control.</value>
        [CreateNew]
        public F1111Controller F1111Control
        {
            get { return this.form1111Control as F1111Controller; }
            set { this.form1111Control = value; }
        }

        #endregion Properties

        #region Events

        /// <summary>
        /// Handles the Load event of the f1111 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1111_Load(object sender, EventArgs e)
        {
            try
            {
                ////string s = DateTime.Now.ToLocalTime().ToString();

                if (!this.isstatusAvailable)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ExciseAffidavitValidationData affidavitValidationDataSet = new ExciseAffidavitValidationData();
                    affidavitValidationDataSet = this.form1111Control.WorkItem.GetExciseTaxAffidavitStatus(this.statementID, this.treasurerStatus);

                    if (affidavitValidationDataSet != null)
                    {
                        if (affidavitValidationDataSet.Tables[0].Rows.Count > 0)
                        {
                            this.statusID = Convert.ToInt32(affidavitValidationDataSet.Tables[0].Rows[0][0]);
                            this.status = affidavitValidationDataSet.Tables[0].Rows[0][1].ToString();
                        }
                    }

                    ////this.AffidavitValidationDateTimePicker.Value = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.

                    this.StatusLabel.Text = this.status.ToString();
                    switch (this.statusID)
                    {
                        case 0:
                            this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
                            break;
                        case 1:
                            this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                            break;
                        case 2:
                            this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                            break;
                    }
                }
                else
                {
                    if (this.treasurerStatus == 1)
                    {
                        this.StatusLabel.Text = SharedFunctions.GetResourceString("Treasurer - ") + this.status;
                    }
                    else
                    {
                        this.StatusLabel.Text = SharedFunctions.GetResourceString("Assessor - ") + this.status;
                    }

                    this.UserDetailsLabel.Text = this.avilableUpdateDatetime + ' ' + this.avilableUsername;


                    switch (this.availableStatusId)
                    {
                        case 0:
                            this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
                            this.UserDetailsLabel.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
                            break;
                        case 1:
                            this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                            this.UserDetailsLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                            break;
                        case 2:
                            this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                            this.UserDetailsLabel.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ApprovedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ApprovedButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.treasurerStatus == 1 || this.treasurerStatus == 0)
                {
                    this.statusID = 1;
                    this.status = "Approved";
                    this.updateTime = DateTime.Now.ToLocalTime().ToString();
                    this.userId = TerraScanCommon.UserId;
                    this.username = TerraScanCommon.UserName;
                }

                ////this.F1111Control.WorkItem.UpdateExciseAffidavitStatus(this.statementID, this.treasurerStatus, this.statusID);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the RejectedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RejectedButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.treasurerStatus == 1 || this.treasurerStatus == 0)
                {
                    this.statusID = 2;
                    this.status = "Rejected";
                    this.updateTime = DateTime.Now.ToLocalTime().ToString();
                    this.userId = TerraScanCommon.UserId;
                    this.username = TerraScanCommon.UserName;
                }

                ////this.F1111Control.WorkItem.UpdateExciseAffidavitStatus(this.statementID, this.treasurerStatus, this.statusID);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the UnverifiedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnverifiedButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.treasurerStatus == 1 || this.treasurerStatus == 0)
                {
                    this.statusID = 0;
                    this.status = "Unverified";
                    this.updateTime = DateTime.Now.ToLocalTime().ToString();
                    this.userId = TerraScanCommon.UserId;
                    this.username = TerraScanCommon.UserName;
                }

                ////this.F1111Control.WorkItem.UpdateExciseAffidavitStatus(this.statementID, this.treasurerStatus, this.statusID);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Events
    }
}