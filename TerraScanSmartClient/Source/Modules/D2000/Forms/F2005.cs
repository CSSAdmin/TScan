//---------------------------------------------------------------------------------
// <copyright file="F2005.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F2005 Form Slice - Schedule Lock/Unlock Form 
// </summary>
//---------------------------------------------------------------------------------
// Change History
//*********************************************************************************
// Date			    Author		       Description
// ----------		---------		   --------------------------------------------
// 08/03/17        Dhineshkumar.J       Created
//*********************************************************************************

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
    /// F2005 is used for Schedule Lock/UnLock 
    /// </summary>
    public partial class F2005 : Form
    {
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
        /// stores scheduleID
        /// </summary>
        private int scheduleID;

        /// <summary>
        /// flag
        /// </summary>
        private int userid;

        /// <summary>
        /// controller F1503Controller
        /// </summary>
        private F2005Controller formF2005Control;

        /// <summary>
        /// Instance for F2001ParcelLockingData
        /// </summary>
        private F2200EditScheduleData sceduleLockingData = new F2200EditScheduleData();

        /// <summary>
        /// Instance for ParcellocingData
        /// </summary>
        private F2001ParcelLockingData parcellockingData = new F2001ParcelLockingData();

        #region Controller
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2005"/> class.
        /// </summary>
        public F2005()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2005"/> class.
        /// </summary>
        /// <param name="parcelId">ParcelId</param>
        /// <param name="formName"> Name of the form</param>
        public F2005(int parcelId, string formName)
        {
            InitializeComponent();
            this.formName = formName;
            this.keyId = parcelId;
        }
        
        /// <summary>
        /// For F2005Control
        /// </summary>
        [CreateNew]
        public F2005Controller Form2005Controller
        {
            get
            {
                return this.formF2005Control as F2005Controller; 
            }
            set
            {
                this.formF2005Control = value;
            }
        }
        #endregion

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F2005_Load(object sender, EventArgs e)
        {
            try
            {
                this.userid = TerraScanCommon.UserId;
                this.validUserId = this.formF2005Control.WorkItem.F2005_GetValidUser(this.keyId, this.userid);
                this.terrascanBtnLock.Enabled = this.validUserId == 1 ? true : false;
                this.GetScheduleLockDetails();
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
            if (textColor.Equals("0"))
            {
                this.VisualStatusLabel.BackColor = Color.FromArgb(0, 128, 0);
                this.VisualStatusLabel.Text = SharedFunctions.GetResourceString("ScheduleUnlockedBy");
                this.terrascanBtnUnlock.Enabled = false;
                this.terrascanBtnLock.Enabled = true;
            }
            else
            {
                this.VisualStatusLabel.BackColor = Color.FromArgb(128, 0, 0);
                this.VisualStatusLabel.Text = SharedFunctions.GetResourceString("ScheduleLockedBy") + " " + name;
                this.terrascanBtnLock.Enabled = false;
                this.terrascanBtnUnlock.Enabled = this.validUserId == 1 ? true : false;               
            }
        }

        /// <summary>
        /// Edit lock Status
        /// </summary>
        /// <param name="keyid">ParcelId</param>
        /// <param name="lockvalue"> Value of Lock</param>
        /// <param name="lockAdmin"> value of Admin lock</param>
        /// <param name="appraisallock">Value of Apparaisal lock</param>
        private void EditLockStatus(int keyid, int lockvalue, int appraisallock)
        {
            int upadateConfirm;
            if (this.terrascanBtnLock.Enabled || this.terrascanBtnUnlock.Enabled)
            {
                upadateConfirm = this.formF2005Control.WorkItem.F2005_UpdateParcelLockDetails(keyid, lockvalue, TerraScanCommon.UserId);
                if (upadateConfirm == -99)
                {
                    this.DialogResult = DialogResult.None;
                }
                else if (upadateConfirm == 0)
                {
                    this.GetScheduleLockDetails();
                }
            }
        }

        /// <summary>
        /// Get Schedule Lock Details
        /// </summary>
        private void GetScheduleLockDetails()
        {
            this.FormNoLabel.Text = this.formName;
            this.sceduleLockingData = this.formF2005Control.WorkItem.F25050_GetScheduleDetails(this.keyId);
            if (this.sceduleLockingData.f2200ListScheduleDataTable.Rows.Count > 0)
            {
                string[] parcelNo = null;
                if (this.formName == SharedFunctions.GetResourceString("SchedulLockFormName"))
                {
                    this.lockNumber = this.sceduleLockingData.f2200ListScheduleDataTable.Rows[0][this.sceduleLockingData.f2200ListScheduleDataTable.LockScheduleByColumn].ToString();
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("ScheduleLockMessage");
                    this.parcelNumber = this.sceduleLockingData.f2200ListScheduleDataTable.Rows[0][this.sceduleLockingData.f2200ListScheduleDataTable.ScheduleNumberColumn].ToString();
                    parcelNo = this.parcelNumber.Split('/');
                    if (parcelNo.Length > 0)
                    {
                        this.lblScheduleNumber.Text = parcelNo[0].Trim();
                        if (parcelNo.Length > 1)
                        {
                            this.ParcelnumberLabel2.Text = parcelNo[1].Trim();
                        }
                    }

                    this.userID = Convert.ToInt32(this.lockNumber);
                    this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("ScheduleLock");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
                else if (this.formName == SharedFunctions.GetResourceString("SchedulLockFormName"))
                {
                    this.lockNumber = this.sceduleLockingData.f2200ListScheduleDataTable.Rows[0][this.sceduleLockingData.f2200ListScheduleDataTable.LockScheduleByColumn].ToString();
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("ScheduleLockMessage");
                    this.parcelNumber = this.sceduleLockingData.f2200ListScheduleDataTable.Rows[0][this.sceduleLockingData.f2200ListScheduleDataTable.ParcelNumberColumn].ToString();
                    parcelNo = this.parcelNumber.Split('/');
                    this.lblScheduleNumber.Text = parcelNo[0].Trim();
                    this.ParcelnumberLabel2.Text = parcelNo[1].Trim();
                    this.userID = Convert.ToInt32(this.lockNumber);
                    this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("ScheduleLock");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
                else
                {
                    this.lockNumber = this.sceduleLockingData.f2200ListScheduleDataTable.Rows[0][this.sceduleLockingData.f2200ListScheduleDataTable.LockScheduleByColumn].ToString();
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("ScheduleLockMessage");
                    this.parcelNumber = this.sceduleLockingData.f2200ListScheduleDataTable.Rows[0][this.sceduleLockingData.f2200ListScheduleDataTable.ParcelNumberColumn].ToString();
                    parcelNo = this.parcelNumber.Split('/');
                    if (parcelNo.Length > 0)
                    {
                        this.lblScheduleNumber.Text = parcelNo[0].Trim();
                        if (parcelNo.Length > 1)
                        {
                            this.ParcelnumberLabel2.Text = parcelNo[1].Trim();
                        }
                    }

                    this.userID = Convert.ToInt32(this.lockNumber);
                    this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("ScheduleLock");
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
            this.parcellockingData = this.formF2005Control.WorkItem.F2001_getParcelLockingUsername(userlockNumber);
            {
                if (this.parcellockingData.f2001getUserName.Rows.Count > 0)
                {
                    this.userName = this.parcellockingData.f2001getUserName.Rows[0][this.parcellockingData.f2001getUserName.Name_DisplayColumn].ToString();
                }
            }
        }

        #endregion UserDefined Function

        /// <summary>
        /// 
        /// Handles ClockButton event of the F2005 control.
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
        /// Lock Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void terrascanBtnLock_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.formName == SharedFunctions.GetResourceString("SchedulLockFormName"))
                {
                    this.EditLockStatus(this.keyId, this.userid, this.userid);
                }
                else if (this.formName == SharedFunctions.GetResourceString("SchedulLockFormName"))
                {
                    this.EditLockStatus(this.keyId, this.userid, -1);
                }
                else
                {
                    this.EditLockStatus(this.keyId, -1, this.userid);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Unlock Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void terrascanBtnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.validUserId == 1)
                {
                    if (this.formName == SharedFunctions.GetResourceString("SchedulLockFormName"))
                    {
                        this.GetLockUser(this.userid);
                        this.EditLockStatus(this.keyId, 0, this.userid);
                    }
                    else if (this.formName == SharedFunctions.GetResourceString("SchedulLockFormName"))
                    {
                        this.GetLockUser(this.userid);
                        this.EditLockStatus(this.keyId, 0, -1);
                    }
                    else
                    {
                        this.GetLockUser(this.userid);
                        this.EditLockStatus(this.keyId, -1, 0);
                    }
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
