//--------------------------------------------------------------------------------------------
// <copyright file="F3501.cs" company="Congruent">
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
// 21/06/07        Ramya.D              Created
//*********************************************************************************/

namespace D35100
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
    /// F3501
    /// </summary>
    public partial class F3501 : Form
    {
        #region Variable
        /// <summary>
        /// controller F3501Controller
        /// </summary>
        private F3501Controller formF3501Control;

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// lockNumber
        /// </summary>
        private int lockNumber;

        /// <summary>
        /// formName
        /// </summary>
        private string formName;

        /// <summary>
        /// formTitle
        /// </summary>
        private string formTitle;

        /// <summary>
        /// userName
        /// </summary>
        private string userName;

        /// <summary>
        /// userid
        /// </summary>
        private int userid;

        /// <summary>
        /// qntyParcels
        /// </summary>
        private int qntyParcels;

        /// <summary>
        /// qntyParcelsLocked
        /// </summary>
        private int qntyParcelsLocked;

        /// <summary>
        /// parcelNumber
        /// </summary>
        private string parcelNumber;

        /// <summary>
        /// validUserId
        /// </summary>
        private int validUserId;

        /// <summary>
        /// buttonFlag
        /// </summary>
        private bool buttonFlag;

        /// <summary>
        /// GetNeighborhoodHeaderData
        /// </summary>
        private F3501NeighborhoodParcelLocksData.f35100NeighborhoodParcelLocksRow getNeighborhoodHeaderLockData;

        /// <summary>
        /// neighborhoodParcelLocksData
        /// </summary>
        private F3501NeighborhoodParcelLocksData neighborhoodParcelLocksData = new F3501NeighborhoodParcelLocksData();

        #endregion Variable
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3501"/> class.
        /// </summary>
        public F3501()
        {
            InitializeComponent();
        }

        /// <summary>
        /// F3501
        /// </summary>
        /// <param name="neighborhoodId">neighborhoodId</param>
        /// <param name="formName">formName</param>
        public F3501(int neighborhoodId, string formName)
        {
            InitializeComponent();
            this.formName = formName;
            this.keyId = neighborhoodId;
        }

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Properties
        /// <summary>
        /// For F3501Control
        /// </summary>
        [CreateNew]
        public F3501Controller Form3501Control
        {
            get { return this.formF3501Control as F3501Controller; }
            set { this.formF3501Control = value; }
        }
        #endregion Properties

        /// <summary>
        /// F3501_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F3501_Load(object sender, EventArgs e)
        {
            try
            {
                this.userid = TerraScanCommon.UserId;
                this.validUserId = this.formF3501Control.WorkItem.F2001_GetValidUserName(this.keyId, this.userid, this.formName);
                if (this.validUserId == 1)
                {
                    this.UnlockButton.Enabled = true;
                }
                else
                {
                    this.UnlockButton.Enabled = false;
                }

                this.LoadNeighborhoodParcelLocks();
                this.CancelButton = this.CloseButton;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region User Defined Method

        /// <summary>
        /// LoadNeighborhoodParcelLocks
        /// </summary>
        private void LoadNeighborhoodParcelLocks()
        {
            string neighborhood = null;
            string rollYear = null;
            this.FormNoLabel.Text = this.formName;
            this.neighborhoodParcelLocksData = this.formF3501Control.WorkItem.ListNeighborhoodParcelLocks(this.keyId);
            if (this.neighborhoodParcelLocksData.f35100NeighborhoodParcelLocks.Rows.Count > 0)
            {
                this.getNeighborhoodHeaderLockData = (F3501NeighborhoodParcelLocksData.f35100NeighborhoodParcelLocksRow)this.neighborhoodParcelLocksData.f35100NeighborhoodParcelLocks.Rows[0];
                //// string[] parcelNo = null;
                if (this.formName == SharedFunctions.GetResourceString("NeighborhoodAppraisalformNo"))
                {
                    ////this.getNeighborhoodHeaderLockData = (F3501NeighborhoodParcelLocksData.f35100NeighborhoodParcelLocksRow)this.neighborhoodParcelLocksData.f35100NeighborhoodParcelLocks.Rows[0];
                    this.lockNumber = this.getNeighborhoodHeaderLockData.LockAppraisal;
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("NeighborhoodAppraisalLock");
                    this.parcelNumber = this.getNeighborhoodHeaderLockData.NeighborhoodLabel;
                    if (this.parcelNumber.Length > 0)
                    {
                        rollYear = this.parcelNumber.Substring(this.parcelNumber.Length - 4);
                        neighborhood = this.parcelNumber.Substring(0, this.parcelNumber.Length - 5);
                    }

                    this.ParcelnumberLabel.Text = neighborhood.Trim();
                    this.ParcelnumberLabel2.Text = rollYear.Trim();
                    ////}
                    this.userName = this.getNeighborhoodHeaderLockData.LockAppraisalName;

                    ////this.userID = Convert.ToInt32(this.lockNumber);
                    ////this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("NeighborhoodFormTitle") + " " + SharedFunctions.GetResourceString("Appraisal");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
                else if (this.formName == SharedFunctions.GetResourceString("NeighborhoodAssessmentFormNo"))
                {
                    this.lockNumber = this.getNeighborhoodHeaderLockData.LockAssessment;
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("NeighborhoodAssessmentLock");
                    this.parcelNumber = this.getNeighborhoodHeaderLockData.NeighborhoodLabel;
                    if (this.parcelNumber.Length > 0)
                    {
                        rollYear = this.parcelNumber.Substring(this.parcelNumber.Length - 4);
                        neighborhood = this.parcelNumber.Substring(0, this.parcelNumber.Length - 5);
                    }

                    this.ParcelnumberLabel.Text = neighborhood;
                    this.ParcelnumberLabel2.Text = rollYear.Trim();
                    this.userName = this.getNeighborhoodHeaderLockData.LockAssessmentName;
                    ////this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("NeighborhoodFormTitle") + " " + SharedFunctions.GetResourceString("Assessment");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
                else
                {
                    this.lockNumber = this.getNeighborhoodHeaderLockData.LockAdmin;
                    this.DescriptionLabel.Text = SharedFunctions.GetResourceString("NeighborhoodAdminLock");
                    this.parcelNumber = this.getNeighborhoodHeaderLockData.NeighborhoodLabel;
                    if (this.parcelNumber.Length > 0)
                    {
                        rollYear = this.parcelNumber.Substring(this.parcelNumber.Length - 4);
                        neighborhood = this.parcelNumber.Substring(0, this.parcelNumber.Length - 5);
                    }

                    this.ParcelnumberLabel.Text = neighborhood;
                    this.ParcelnumberLabel2.Text = rollYear.Trim();
                    this.userName = this.getNeighborhoodHeaderLockData.LockAdminName;
                    ////this.GetLockUser(this.userID);
                    this.formTitle = SharedFunctions.GetResourceString("NeighborhoodFormTitle") + " " + SharedFunctions.GetResourceString("Administrative");
                    this.SetFormName(this.formTitle, this.lockNumber, this.userName);
                }
            }
        }

        /// <summary>
        /// EditLockStatus
        /// </summary>
        /// <param name="keyid">keyid</param>
        /// <param name="lockvalue">lockvalue</param>
        /// <param name="lockAdmin">lockAdmin</param>
        /// <param name="appraisallock">appraisallock</param>
        /// <param name="primaryID">primaryID</param>
        private void EditLockStatus(int keyid, int lockvalue, int lockAdmin, int appraisallock, int primaryID)
        {
            if (this.LockButton.Enabled || this.UnlockButton.Enabled)
            {
                this.neighborhoodParcelLocksData = this.formF3501Control.WorkItem.UpdateParcelLockingDetails(keyid, lockvalue, lockAdmin, appraisallock, primaryID, TerraScan.Common.TerraScanCommon.UserId);
                if (this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.Rows.Count > 0)
                {
                    int.TryParse((this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.Rows[0][this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.QntyParcelColumn].ToString()), out this.qntyParcels);
                    int.TryParse((this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.Rows[0][this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.QntyParcelLockedColumn].ToString()), out this.qntyParcelsLocked);
                    if (this.buttonFlag)
                    {
                        switch (MessageBox.Show(SharedFunctions.GetResourceString("NeighborhoodWarning") + " " + this.qntyParcels + " of which " + this.qntyParcelsLocked + " are already locked. ", SharedFunctions.GetResourceString("LockWarning") + " ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                        {
                            case DialogResult.OK:
                                {
                                    this.neighborhoodParcelLocksData = this.formF3501Control.WorkItem.UpdateParcelLockingDetails(this.keyId, lockvalue, lockAdmin, appraisallock, -100, TerraScan.Common.TerraScanCommon.UserId);
                                    if (this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.Rows[0][this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.ReturnValueColumn].ToString().Equals("Zero"))
                                    {
                                        ////updateStatus = true;
                                        this.LoadNeighborhoodParcelLocks();
                                    }

                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (MessageBox.Show(SharedFunctions.GetResourceString("NeighborhoodUnlock") + " " + this.qntyParcels + " of which " + this.qntyParcelsLocked + " are currently locked. ", SharedFunctions.GetResourceString("LockWarning") + " ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                        {
                            case DialogResult.OK:
                                {
                                    this.neighborhoodParcelLocksData = this.formF3501Control.WorkItem.UpdateParcelLockingDetails(this.keyId, lockvalue, lockAdmin, appraisallock, -100, TerraScan.Common.TerraScanCommon.UserId);
                                    if (this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.Rows[0][this.neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks.ReturnValueColumn].ToString().Equals("Zero"))
                                    {
                                        this.LoadNeighborhoodParcelLocks();
                                    }

                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    break;
                                }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set the status bar color
        /// </summary>
        /// <param name="formText">formText</param>
        /// <param name="lockNo">lockNo</param>
        /// <param name="userName">userName</param>
        private void SetFormName(string formText, int lockNo, string name)
        {
            this.Focus();
            this.Text = SharedFunctions.GetResourceString("F1503FormNameTitle") + formText;
            formText = formText.Replace(SharedFunctions.GetResourceString("NBHDLock"), "");
            if (lockNo == 0)
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

        #endregion User Defined Method

        /// <summary>
        /// To close the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To unlock 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void UnlockButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.validUserId == 1)
                {
                    if (this.formName == SharedFunctions.GetResourceString("NeighborhoodAppraisalformNo"))
                    {
                        this.EditLockStatus(this.keyId, -1, -1, 0, -99);
                    }
                    else if (this.formName == SharedFunctions.GetResourceString("NeighborhoodAssessmentFormNo"))
                    {
                        this.EditLockStatus(this.keyId, 0, -1, -1, -99);
                    }
                    else
                    {
                        this.EditLockStatus(this.keyId, -1, 0, -1, -99);
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
        /// To Lock
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LockButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonFlag = true;
                if (this.validUserId == 1)
                {
                    if (this.formName == SharedFunctions.GetResourceString("NeighborhoodAppraisalformNo"))
                    {
                        this.EditLockStatus(this.keyId, -1, -1, this.userid, -99);
                    }
                    else if (this.formName == SharedFunctions.GetResourceString("NeighborhoodAssessmentFormNo"))
                    {
                        this.EditLockStatus(this.keyId, this.userid, -1, -1, -99);
                    }
                    else
                    {
                        this.EditLockStatus(this.keyId, -1, this.userid, -1, -99);
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
        /// Tooltip for neighborhood lable
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParcelnumberLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ParcelLockToolTip.SetToolTip(this.ParcelnumberLabel, this.ParcelnumberLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}