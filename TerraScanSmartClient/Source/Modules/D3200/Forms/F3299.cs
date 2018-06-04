//--------------------------------------------------------------------------------------------
// <copyright file="F3299.cs" company="Congruent">
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
// 06 Apr 2010     LathaMaheswari        Created
// *********************************************************************************/

namespace D3200
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;

    /// <summary>
    /// F3299 class
    /// Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.
    /// </summary>
    [ComVisible(true)]
    public partial class F3299 : Form
    {
        #region Variables

        /// <summary>
        /// form1070Control varaible 
        /// </summary>
        private F3299Controller form3299Control;

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

        private string htmlContent = string.Empty;
        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1070"/> class.
        /// </summary>
        public F3299()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1070"/> class.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formNumber">The form number.</param>
        /// <param name="userId">The user id.</param>
        public F3299(string contentText)
        {
            InitializeComponent();
            this.htmlContent = contentText;
        }

        #endregion Constructor

        #region properites

        /// <summary>
        /// Gets or sets the F3299 controll.
        /// </summary>
        /// <value>The F3299 controll.</value>
        [CreateNew]
        public F3299Controller F3299Controll
        {
            get { return this.form3299Control as F3299Controller; }
            set { this.form3299Control = value; }
        }

        public string HtmlContent
        {
            get { return this.htmlContent; }
            set { this.htmlContent = value; }
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
        private void F3299_Load(object sender, EventArgs e)
        {
            try
            {
                this.CancelButton = this.OKButton;
                this.Cursor = Cursors.WaitCursor;

                // sets an object that can be accessed by scripting code that is contained within a Web page displayed in the WebBrowser control. 
                this.WebSliceWebBrowser.ObjectForScripting = this;
                this.WebSliceWebBrowser.DocumentText = this.htmlContent;
                //this.WebSliceWebBrowser.DocumentText = "<html><body><b>DONE</b></body></html>";
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

                        this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                        this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
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
        private void F3299_Resize(object sender, System.EventArgs e)
        {
            try
            {
                //this.SetSize();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Resize Event

        #endregion Events


        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}