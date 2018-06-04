//--------------------------------------------------------------------------------------------
// <copyright file="F1207.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Posting Errors.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 May 2009	    LathaMaheswari	    Created
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
    using System.Runtime.InteropServices;

    /// <summary>
    /// f1207 class
    /// </summary>
    [ComVisible(true)] // Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.
    public partial class F1207 : Form
    {
        #region Variables

        /// <summary>
        /// form1207Control varaible 
        /// </summary>
        private F1207Controller form1207Control;

        /// <summary>
        /// Store XML content
        /// </summary>
        private F95010GetWebFormXMLData datasetGetWebFormData = new F95010GetWebFormXMLData();

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1207"/> class.
        /// </summary>
        public F1207()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region properites

        /// <summary>
        /// Gets or sets the F1207 controll.
        /// </summary>
        /// <value>The F1207 controll.</value>
        [CreateNew]
        public F1207Controller F1207Controll
        {
            get { return this.form1207Control as F1207Controller; }
            set { this.form1207Control = value; }
        }

        #endregion properites

        #region Methods

        /// <summary>
        /// Closes the form. - Call from Script
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F1207 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1207_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // sets an object that can be accessed by scripting code that is contained within a Web page displayed in the WebBrowser control. 
                this.WebSliceWebBrowser.ObjectForScripting = this;

                this.datasetGetWebFormData = new F95010GetWebFormXMLData();

                // DataBase call for: Procedure f95010_pcget_WebFormXML(KeyId, FormId, UserId)
                this.datasetGetWebFormData = this.form1207Control.WorkItem.GetWebFormDetails(null, 1207, TerraScanCommon.UserId);

                if (this.datasetGetWebFormData.F95010GetWebFormXML.Rows.Count > 0)
                {
                    string displayDocument = this.datasetGetWebFormData.F95010GetWebFormXML.Rows[0][this.datasetGetWebFormData.F95010GetWebFormXML.XmlDocumentColumn.ColumnName].ToString();

                    // Loads the document at the specified URL into the WebBrowser control
                    this.WebSliceWebBrowser.Navigate(displayDocument);
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Events
    }
}