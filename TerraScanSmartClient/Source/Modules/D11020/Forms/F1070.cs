//--------------------------------------------------------------------------------------------
// <copyright file="F1070.cs" company="Congruent">
//  Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the Posting Errors.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author             Description
// ----------       ---------           ---------------------------------------------------------
// 20 Aug 2009     LathaMaheswari        Created
// *********************************************************************************/

namespace D11020
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;

    /// <summary>
    /// F1070 class
    /// Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.
    /// </summary>
    [ComVisible(true)] 
    public partial class F1070 : Form
    {
        #region Variables

        /// <summary>
        /// form1070Control varaible 
        /// </summary>
        private F1070Controller form1070Control;

        /// <summary>
        /// Store XML content
        /// </summary>
        private F95010GetWebFormXMLData datasetGetWebFormData = new F95010GetWebFormXMLData();

        /// <summary>
        /// Key ID of calling form
        /// </summary>
        private int keyId;

        /// <summary>
        /// Form Number
        /// </summary>
        private int formNumber;

        /// <summary>
        /// UserID of current user
        /// </summary>
        private int userId;
        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1070"/> class.
        /// </summary>
        public F1070()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1070"/> class.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formNumber">The form number.</param>
        /// <param name="userId">The user id.</param>
        public F1070(int keyId, int formNumber, int userId)
        {
            InitializeComponent();
            this.keyId = keyId;
            this.formNumber = formNumber;
            this.userId = userId;
        }

        #endregion Constructor

        #region properites

        /// <summary>
        /// Gets or sets the F1070 controll.
        /// </summary>
        /// <value>The F1070 controll.</value>
        [CreateNew]
        public F1070Controller F1070Controll
        {
            get { return this.form1070Control as F1070Controller; }
            set { this.form1070Control = value; }
        }

        #endregion properites

        #region Public Methods

        /// <summary>
        /// Closes the form. - Call from Script
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        #endregion Public Methods

        #region Events

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F1070 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1070_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // sets an object that can be accessed by scripting code that is contained within a Web page displayed in the WebBrowser control. 
                this.WebSliceWebBrowser.ObjectForScripting = this;

                this.datasetGetWebFormData = new F95010GetWebFormXMLData();

                // DataBase call for: Procedure f95010_pcget_WebFormXML(KeyId, FormId, UserId)
                this.datasetGetWebFormData = this.form1070Control.WorkItem.GetWebFormDetails(this.keyId, this.formNumber, this.userId);

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
        #endregion Form Load

        #region Browser Events

        /// <summary>
        /// Handles the DocumentCompleted event of the WebSliceWebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.WebBrowserDocumentCompletedEventArgs"/> instance containing the event data.</param>
        private void WebSliceWebBrowser_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (this.WebSliceWebBrowser.ReadyState.Equals(WebBrowserReadyState.Complete))
                {
                    ////WebSliceWebBrowser.Refresh(WebBrowserRefreshOption.IfExpired);
                    if (e.Url.AbsolutePath.ToString() != "blank")
                    {
                        this.WebSliceWebBrowser.ScrollBarsEnabled = false;
                        this.AutoSize = true;
                        this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                        this.WebSliceWebBrowser.Dock = DockStyle.Fill;

                        // Set size for form and webcontrol based on webpage loaded
                        this.SetSize();
                        this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                        this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds  ;
                        this.CenterToScreen();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Browser Events

        #region Resize Event
        /// <summary>
        /// Handles the Resize event of the F1070 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1070_Resize(object sender, System.EventArgs e)
        {
            try
            {
                this.SetSize();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Resize Event

        #endregion Events

        #region Private Method

        /// <summary>
        /// Sets the size based on the webpage loaded.
        /// </summary>
        private void SetSize()
        {
            if (this.WebSliceWebBrowser.Document != null && this.WebSliceWebBrowser.Document.Body != null
               && !this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height.Equals(0))
            {
                this.WebSlicePanel.Size = new System.Drawing.Size(this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Width, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height);
                this.WebSliceWebBrowser.Size = new System.Drawing.Size(this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Width, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 4);
                this.Size = new System.Drawing.Size(this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Width, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 8);
            }
            else
            {
                this.WebSlicePanel.Size = new System.Drawing.Size(this.WebSliceWebBrowser.ClientRectangle.Width, this.WebSliceWebBrowser.ClientRectangle.Height + 10);
                this.WebSliceWebBrowser.Size = new System.Drawing.Size(this.WebSliceWebBrowser.ClientRectangle.Width, this.WebSliceWebBrowser.ClientRectangle.Height);
                this.Size = new System.Drawing.Size(this.WebSliceWebBrowser.ClientRectangle.Width, this.WebSliceWebBrowser.ClientRectangle.Height + 12);
            }
        }

        #endregion Private Methods
    }
}