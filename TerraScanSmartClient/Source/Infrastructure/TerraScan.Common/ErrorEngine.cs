//--------------------------------------------------------------------------------------------
// <copyright file="ErrorEngine.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13 June 06		KARTHIKEYAN V	    Created
// 18 July 07       GUHAN.S             BugID  2592  Fixed
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
    using TerraScan.Helper;
    using System.Configuration;
    using System.IO;
    using System.Diagnostics;
    using TerraScan.UI.Controls;
    using System.Runtime.InteropServices;
    using System.Web.Services.Protocols;
    using System.Security.Permissions;
    using System.Security;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Threading;
    using TerraScan.Utilities;
    using System.Net;
    using System.Collections;
    using TerraScan.Common;
    using TerraScan.BusinessEntities; 

    /// <summary>
    /// ErrorEngineForm
    /// </summary>
    public partial class ErrorEngine : Form
    {
        #region Variable

        /// <summary>
        /// Created Integer for errorTypeId
        /// </summary>
        private int errorTypeId;

        /// <summary>
        /// Created string for SelectFilePath
        /// </summary>
        private string userIP = string.Empty;
        
        /// <summary>
        /// Object for dataset created
        /// </summary>
        private ErrorEngineData errorEngineDataset = new ErrorEngineData();

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable errorIdHashTable = new Hashtable();

        /// <summary>
        /// Created string for errorString
        /// </summary>       
        private int errorStringMessage;

        /// <summary>
        /// Created string for errorMessage
        /// </summary>
        private string errorMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Used to Initialize the Components.
        /// </summary>
        /// <param name="errorID">errorID</param>
        public ErrorEngine(int errorID)
        {
            this.errorTypeId = errorID;
            this.InitializeComponent();
           // this.AcceptButton = this.okErrorEngineButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ErrorEngineForm"/> class.
        /// </summary>
        /// <param name="errorID">The error ID.</param>
        /// <param name="errorString">The error string.</param>
        public ErrorEngine(int errorID, int errorString)
        {
            this.errorTypeId = errorID;
            this.errorStringMessage = errorString;
            this.InitializeComponent();
          ///  this.AcceptButton = this.okErrorEngineButton;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEngine"/> class.
        /// </summary>
        /// <param name="errorID">The error ID.</param>
        /// <param name="errorString">The error string.</param>
        public ErrorEngine(int errorID, string errorString)
        {
            this.errorTypeId = errorID;
            this.errorMessage = errorString;
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="errorId">The error id.</param>
        public static void ShowForm(int errorId)
        {
            // Create a instance for ErrorEngine Form and Show.
            Application.UseWaitCursor = false;
            ErrorEngine errorEngine = new ErrorEngine(errorId);
            errorEngine.ShowDialog();
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="errorId">The error id.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void ShowForm(int errorId, int errorMessage)
        {
            Application.UseWaitCursor = false;
            // Create a instance for ErrorEngine Form and Show.
            ErrorEngine errorEngine = new ErrorEngine(errorId, errorMessage);
            errorEngine.ShowDialog();
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="errorId">The error id.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void ShowForm(int errorId, string errorMessage)
        {
            Application.UseWaitCursor = false;
           //Create a instance for ErrorEngine Form and Show.
            ErrorEngine errorEngine = new ErrorEngine(errorId, errorMessage);
            errorEngine.ShowDialog();
        }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        private void GetIPAddress()
        {
            // Get the HostName
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress address in hostEntry.AddressList)
            {
                // Get the IP Address
                this.userIP = address.ToString();
            }
        }

        /// <summary>
        /// Processes the cause list.
        /// </summary>
        private void ProcessCauseList()
        {
            // this.Cursor = Cursors.WaitCursor;
            this.errorIdHashTable.Clear();
            this.errorIdHashTable.Add("KeyName", "ErrorTypeId");
            this.errorIdHashTable.Add("KeyValue", this.errorTypeId);

           
        }

        /// <summary>
        /// Saves the records.
        /// </summary>
        private void SaveRecords()
        {
            try
            {
                // Gets the IP Address
                this.GetIPAddress();

                // Saves the Value of Error Engine.
                WSHelper.InsertErrorEngine(this.dateTimeLabel.Text, TerraScanCommon.UserId, this.userIP, this.errorTypeId, "", this.commentTextBox.Text);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Close();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the ErrorEngineForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ErrorEngineForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Gets the data from Table tts_Error
                this.errorEngineDataset = WSHelper.GetErrorEngine(this.errorTypeId);

                // Display the Current date and time.
                this.dateTimeLabel.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                // Display the errorTypeID in Header.
                this.Text = this.Text + "Error " + this.errorTypeId;

                if (this.errorEngineDataset.ErrorEngineTable.Rows.Count > 0 )
                {
                    if (this.errorTypeId == 6)
                    {
                        if (this.errorStringMessage == 0)
                        {
                            this.errorStringLabel.Text = this.errorEngineDataset.ErrorEngineTable.Rows[0]["BeforeText"].ToString() + "\n" + this.errorEngineDataset.ErrorEngineTable.Rows[0]["AfterText"].ToString();
                        }
                        else
                        {
                            this.errorStringLabel.Text = this.errorEngineDataset.ErrorEngineTable.Rows[0]["BeforeText"].ToString() + " (Form: " + this.errorStringMessage + ")" + "\n" + this.errorEngineDataset.ErrorEngineTable.Rows[0]["AfterText"].ToString();
                        }
                    }
                    else if (this.errorTypeId == 13)
                    {
                        this.errorStringLabel.Text = this.errorEngineDataset.ErrorEngineTable.Rows[0]["BeforeText"].ToString() + "\n " + this.errorMessage + "\n" + this.errorEngineDataset.ErrorEngineTable.Rows[0]["AfterText"].ToString();
                    }
                    else
                    {
                        if (this.errorStringMessage == 0)
                        {
                            // Display Befor Text and AfterText from Database.
                            this.errorStringLabel.Text = this.errorEngineDataset.ErrorEngineTable.Rows[0]["BeforeText"].ToString() + "\n" + this.errorEngineDataset.ErrorEngineTable.Rows[0]["AfterText"].ToString();
                        }                        
                        else
                        {
                            this.errorStringLabel.Text = this.errorEngineDataset.ErrorEngineTable.Rows[0]["BeforeText"].ToString() + " " + this.errorStringMessage + "\n" + this.errorEngineDataset.ErrorEngineTable.Rows[0]["AfterText"].ToString();
                        }
                    }                   
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the OKErrorEngineButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OKErrorEngineButton_Click(object sender, EventArgs e)
        {
            this.SaveRecords();
        }

        /// <summary>
        /// Handles the LinkClicked event of the CauseListLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void CauseListLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //Save the records
                this.SaveRecords();
                System.Threading.Thread thread = new Thread(new ThreadStart(ProcessCauseList));
                thread.Start();
                //Once thread over make invisible the form and again open the form(BugID = 1052)
                this.Hide(); 
                // Shows the report form.
                TerraScanCommon.ShowReport(90101, TerraScan.Common.Reports.Report.ReportType.Preview, this.errorIdHashTable);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Error Close Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void ErrorCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        
    }
}