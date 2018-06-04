//--------------------------------------------------------------------------------------------
// <copyright file="F2001.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F2001 Form Slice - Account Management 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/05/07        Krishna Abburi       Created
//*********************************************************************************/


namespace D2000
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
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F2001
    /// </summary>
    public partial class F2001 : Form
    {
        #region variable

        /// <summary>
        /// formName
        /// </summary>
        private string formName;

        /// <summary>
        /// parcelNumber
        /// </summary>
        private string parcelNumber;

        /// <summary>
        /// lockNumber
        /// </summary>
        private string lockNumber;

        /// <summary>
        /// userName
        /// </summary>
        private string userName;

        /// <summary>
        /// formTitle
        /// </summary>
        private string formTitle;

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// userID
        /// </summary>
        private int userID;

        /// <summary>
        /// validUserId
        /// </summary>
        private int validUserId;

        /// <summary>
        /// flag
        /// </summary>
        private int userid;

        /// <summary>
        /// controller F1503Controller
        /// </summary>
        private F2001Controller formF2001Control;

        /// <summary>
        /// Instance for F2001ParcelLockingData
        /// </summary>
        private F2001ParcelLockingData parcelLockingData = new F2001ParcelLockingData();

        #endregion variable
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2001"/> class.
        /// </summary>
        public F2001()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2001"/> class.
        /// </summary>
        /// <param name="parcelId">ParcelId</param>
        /// <param name="formName"> Name of the form</param>
        public F2001(int parcelId, string formName)
        {
            InitializeComponent();
            this.formName = formName;
            this.keyId = parcelId;
        }

        #region Properties
        /// <summary>
        /// For F2001Control
        /// </summary>
        [CreateNew]
        public F2001Controller Form2001Control
        {
            get { return this.formF2001Control as F2001Controller; }
            set { this.formF2001Control = value; }
        }
        #endregion Properties

        /// <summary>
        /// 
        /// Handles the Load event of the F2001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F2001_Load(object sender, EventArgs e)
        {
            try
            {
                this.userid = TerraScanCommon.UserId;
                this.validUserId = this.formF2001Control.WorkItem.F2001_GetValidUserName(this.keyId, this.userid, this.formName);
                if (this.validUserId == 1)
                {
                    this.UnlockButton.Enabled = true;
                }
                else
                {
                    this.UnlockButton.Enabled = false;
                }

                this.GetParcelLockingDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region UserDefined Function

        /// <summary>
        /// Set the form Name and VisualStatus color
        /// </summary>
        /// <param name="formText">Form Name</param>
        /// <param name="textColor">TextColor</param>
        /// <param name="name">name</param>
        private void SetFormName(string formText, string textColor, string name)
        {
            this.Text = SharedFunctions.GetResourceString("F1503FormNameTitle") + formText;
            formText = formText.Replace(SharedFunctions.GetResourceString("Lock"), "");
            if (textColor.Equals("0"))
            {
                this.VisualStatusLabel.BackColor = Color.FromArgb(0, 128, 0);
                this.VisualStatusLabel.Text = formText + SharedFunctions.GetResourceString("Unlocked");
                this.UnlockButton.Enabled = false;
                this.LockButton.Enabled = true;
            }
            else
            {
                this.VisualStatusLabel.BackColor = Color.FromArgb(128, 0, 0);
                this.VisualStatusLabel.Text = formText + SharedFunctions.GetResourceString("Lockedby") + " " + name;

                this.LockButton.Enabled = false;
                if (this.validUserId == 1)
                {
                    this.UnlockButton.Enabled = true;
                }
                else
                {
                    this.UnlockButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Edit lock Status
        /// </summary>
        /// <param name="keyid">ParcelId</param>
        /// <param name="lockvalue"> Value of Lock</param>
        /// <param name="lockAdmin"> value of Admin lock</param>
        /// <param name="appraisallock">Value of Apparaisal lock</param>
        private void EditLockStatus(int keyid, int lockvalue, int lockAdmin, int appraisallock)
        {
            int upadateConfirm;
            if (this.LockButton.Enabled || this.UnlockButton.Enabled)
            {
                upadateConfirm = this.formF2001Control.WorkItem.F2001_UpdateParcelLockingDetails(keyid, lockvalue, lockAdmin, appraisallock, TerraScanCommon.UserId);
                if (upadateConfirm == -99)
                {
                    ////this.updateStatus = false;
                    this.DialogResult = DialogResult.None;
                }
                else if (upadateConfirm == 0)
                {
                    this.GetParcelLockingDetails();
                    ////this.updateStatus = true;
                }
            }
        }

        /// <summary>
        /// get the parcel Lock Details
        /// </summary>
        private void GetParcelLockingDetails()
        {
            this.FormNoLabel.Text = this.formName;
            this.parcelLockingData = this.formF2001Control.WorkItem.F2001_getParcelLockingDetails(this.keyId);
            if (this.parcelLockingData.f2001ParcelLock.Rows.Count > 0)
            {
                string[] parcelNo = null;
                if (this.formName == SharedFunctions.GetResourceString("formName"))
                {
                    this.lockNumber = this.parcelLockingData.f2001ParcelLock.Rows[0][this.parcelLockingData.f2001ParcelLock.LockAppraisalByColumn].ToString();
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("AppraisalMessage");
                    this.parcelNumber = this.parcelLockingData.f2001ParcelLock.Rows[0][this.parcelLockingData.f2001ParcelLock.ParcelNumberColumn].ToString();
                    parcelNo = this.parcelNumber.Split('/');
                    if (parcelNo.Length > 0)
                    {
                        this.ParcelnumberLabel1.Text = parcelNo[0].Trim();
                        if (parcelNo.Length > 1)
                        {
                            this.ParcelnumberLabel2.Text = parcelNo[1].Trim();
                        }
                    }
                  
                    this.userID = Convert.ToInt32(this.lockNumber);
                    this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("Appraisal");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
                else if (this.formName == SharedFunctions.GetResourceString("formName1"))
                {
                    this.lockNumber = this.parcelLockingData.f2001ParcelLock.Rows[0][this.parcelLockingData.f2001ParcelLock.LockValueByColumn].ToString();
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("AssessmentMessage");
                    this.parcelNumber = this.parcelLockingData.f2001ParcelLock.Rows[0][this.parcelLockingData.f2001ParcelLock.ParcelNumberColumn].ToString();
                    parcelNo = this.parcelNumber.Split('/');
                    this.ParcelnumberLabel1.Text = parcelNo[0].Trim();
                    this.ParcelnumberLabel2.Text = parcelNo[1].Trim();
                    this.userID = Convert.ToInt32(this.lockNumber);
                    this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("Assessment");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
                else
                {
                    this.lockNumber = this.parcelLockingData.f2001ParcelLock.Rows[0][this.parcelLockingData.f2001ParcelLock.LockAdminByColumn].ToString();
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("AdministrativeMessage");
                    this.parcelNumber = this.parcelLockingData.f2001ParcelLock.Rows[0][this.parcelLockingData.f2001ParcelLock.ParcelNumberColumn].ToString();
                    parcelNo = this.parcelNumber.Split('/');
                    if (parcelNo.Length > 0)
                    {
                        this.ParcelnumberLabel1.Text = parcelNo[0].Trim();
                        if (parcelNo.Length > 1)
                        {
                            this.ParcelnumberLabel2.Text = parcelNo[1].Trim();
                        }
                    }
                    //this.ParcelnumberLabel1.Text = parcelNo[0].Trim();
                    //this.ParcelnumberLabel2.Text = parcelNo[1].Trim();

                    this.userID = Convert.ToInt32(this.lockNumber);
                    this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("Administrative");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
            }
        }

        /// <summary>
        /// GetLockUser
        /// </summary>
        /// <param name="userlockNumber">userlockNumber</param>
        private void GetLockUser(int userlockNumber)
        {
            this.parcelLockingData = this.formF2001Control.WorkItem.F2001_getParcelLockingUsername(userlockNumber);
            {
                if (this.parcelLockingData.f2001getUserName.Rows.Count > 0)
                {
                    this.userName = this.parcelLockingData.f2001getUserName.Rows[0][this.parcelLockingData.f2001getUserName.Name_DisplayColumn].ToString();
                }
            }
        }

        #endregion UserDefined Function

        /// <summary>
        /// 
        /// Handles ClockButton event of the F2001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
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
        /// 
        /// Handles UnlockkButton event of the F2001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnlockButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.validUserId == 1)
                {
                    if (this.formName == SharedFunctions.GetResourceString("formName"))
                    {
                        this.GetLockUser(this.userid);
                        this.EditLockStatus(this.keyId, -1, -1, 0);
                    }
                    else if (this.formName == SharedFunctions.GetResourceString("formName1"))
                    {
                        this.GetLockUser(this.userid);
                        this.EditLockStatus(this.keyId, 0, -1, -1);
                    }
                    else
                    {
                        this.GetLockUser(this.userid);
                        this.EditLockStatus(this.keyId, -1, 0, -1);
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles LockkButton event of the F2001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LockButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.formName == SharedFunctions.GetResourceString("formName"))
                {
                    this.EditLockStatus(this.keyId, -1, -1, this.userid);
                }
                else if (this.formName == SharedFunctions.GetResourceString("formName1"))
                {
                    this.EditLockStatus(this.keyId, this.userid, -1, -1);
                }
                else
                {
                    this.EditLockStatus(this.keyId, -1, this.userid, -1);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}