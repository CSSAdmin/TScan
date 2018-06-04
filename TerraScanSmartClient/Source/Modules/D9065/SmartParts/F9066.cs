//--------------------------------------------------------------------------------------------
// <copyright file="F9066.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Nov 07       D.LathaMaheswari      Created
//*********************************************************************************/

namespace D9065
{
    using System;
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
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infragistics.Win.UltraWinGrid;
    using System.Threading;
    using TerraScan.Helper;

    /// <summary>
    /// F9066
    /// </summary>
    public partial class F9066 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Stop
        /// </summary>
        private static bool stop;

        /// <summary>










        /// ProgressBar
        /// </summary>
        private static ProgressBar prgfrm;

        /// <summary>
        /// f9066Controller Controller.
        /// </summary>
        private F9066Controller form9066Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// DataSet contains Check In Data
        /// </summary>
        private F9066CheckInData checkInData = new F9066CheckInData();

        /// <summary>
        /// Store InsertXML
        /// </summary>
        private string insertValue;

        /// <summary>
        /// Store UpdateXML
        /// </summary>
        private string updateValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance 
        /// </summary>
        public F9066()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion

        #region Property

        /// <summary>
        /// Creates Property for F9060Controller
        /// </summary>
        [CreateNew]
        public F9066Controller F9066Control
        {
            get { return this.form9066Control as F9066Controller; }
            set { this.form9066Control = value; }
        }

        #endregion Property

        #region Methods

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form9066Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form9066Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form9066Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.formLabelInfo[0] = "Field CheckIn";
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Get the value
        /// </summary>
        /// <param name="rowIndex">Selected Row Index</param>
        /// <returns>Selected Row Value</returns>
        private F9066CheckInData.GetCheckInDetailsRow GetSelectedRow(int rowIndex)
        {
            return (F9066CheckInData.GetCheckInDetailsRow)this.checkInData.GetCheckInDetails.Rows[rowIndex];
        }

        /// <summary>
        /// Set the Values from DataSet
        /// </summary>
        /// <param name="selectedRow">Selected Row Value</param>
        private void SetValues(F9066CheckInData.GetCheckInDetailsRow selectedRow)
        {
            if (!selectedRow.IsInsertXMLNull())
            {
                this.insertValue = selectedRow.InsertXML.ToString();
            }
            else
            {
                this.insertValue = null;
            }

            if (!selectedRow.IsUpdateXMLNull())
            {
                this.updateValue = selectedRow.UpdateXML.ToString();
            }
            else
            {
                this.updateValue = null;
            }
        }



        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F9066 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9066_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();

                if (ScriptEngine.IsServerAvailable())
                {
                    if (ScriptEngine.IsDatabaseAvailable())
                    {
                        WSHelper.IsOnLineMode = false;
                        //int auditCount;
                        //auditCount = this.form9066Control.WorkItem.F9066_GetAuditCount();
                        //if (auditCount > 0)
                        //{
                        //    this.CheckInButton.Enabled = true;
                        //}
                        //else
                        //{
                        //    this.CheckInButton.Enabled = false;
                        //}

                        WSHelper.IsOnLineMode = true;
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("DoCheckoutProcess"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.CheckInButton.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("DoCheckoutProcess"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.CheckInButton.Enabled = false;
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
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Save the values
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void CheckInButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.CheckIn();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                WSHelper.IsOnLineMode = true;
            }
        }

        /// <summary>
        /// Checks the in.
        /// </summary>
        private void CheckIn()
        {
            if (ScriptEngine.IsServerAvailable())
            {
                if (ScriptEngine.IsDatabaseAvailable())
                {
                    int auditCount;
                    int deletedRecord;
                    WSHelper.IsOnLineMode = false;
                    //auditCount = this.form9066Control.WorkItem.F9066_GetAuditCount();
                    //if (auditCount > 0)
                    //{
                    //    this.checkInData = this.form9066Control.WorkItem.F9066_GetCheckInData();
                    //    if (this.checkInData.GetCheckInDetails.Rows.Count > 0)
                    //    {
                    //        this.SetValues(this.GetSelectedRow(0));
                    //        WSHelper.IsOnLineMode = true;
                    //        this.form9066Control.WorkItem.F9066_SaveData(this.insertValue, this.updateValue);

















                    //        WSHelper.IsOnLineMode = false;
                    //        deletedRecord = this.form9066Control.WorkItem.F9066_DeleteData();
                    //        int returnValue = WSHelper.F9065_UpdateApplicationStatus(false, true, TerraScanCommon.UserId);
                    //        WSHelper.IsOnLineMode = true;
                    //        this.ParcelCountLabel.Text = "CheckIn Completed.";
                    //        this.CheckInButton.Enabled = false;
                    //    }
                    //}
                    //else
                    //{
                    //    WSHelper.IsOnLineMode = true;
                    //}
                }
                else
                {
                    MessageBox.Show("Field Database not available.\nPlease do the checkout process", "TerraScan T2 - CheckIn", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("SqlNotAvailable"), "TerraScan T2 - CheckIn", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            if (ScriptEngine.IsServerAvailable())
            {
                if (ScriptEngine.IsDatabaseAvailable())
                {
                    WSHelper.IsOnLineMode = false;
                    //int auditCount;
                    //auditCount = this.form9066Control.WorkItem.F9066_GetAuditCount();
                    //if (auditCount > 0)
                    //{
                    //    this.ParcelCountLabel.Text = "Field changes are ready for checkin.";
                    //}
                    //else
                    //{
                    //    this.ParcelCountLabel.Text = "No pending CheckIns.";
                    //}

                    WSHelper.IsOnLineMode = true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("DoCheckoutProcess"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("DoCheckoutProcess"), SharedFunctions.GetResourceString("FieldUseHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        #endregion
    }
}