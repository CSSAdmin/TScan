//--------------------------------------------------------------------------------------------
// <copyright file="F95010.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Reference Data.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 07        Suganth Mani       Created
// 
//*********************************************************************************/

namespace D90010
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;
    using TerraScan.BusinessEntities;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using System.Web.Services.Configuration;
    using System.Runtime.InteropServices;

    /// <summary>
    /// F95010
    /// </summary>
    [ComVisible(true)]
    public partial class F95011 : BaseSmartPart
    {
        #region Variable
        /// <summary>
        /// master formno
        /// </summary>
        private int currentMasterFormNo;

        /// <summary>
        /// tab text fromform master
        /// </summary>
        private string currentTabText;

        /// <summary>
        /// previous url
        /// </summary>
        private string previousUrl;

        /// <summary>
        /// keyid 
        /// </summary>
        private int keyId;

        /// <summary>
        /// currentFormNo
        /// </summary>
        private int currentFormNo;

        /// <summary>
        /// redcolor
        /// </summary>
        private int currentRed;

        /// <summary>
        /// green
        /// </summary>
        private int currentGreen;

        /// <summary>
        /// blue
        /// </summary>
        private int currentBlue;

        /// <summary>
        /// controller F95005
        /// </summary>
        private F95011Controller form95011Control;

        private bool flagFormLoad;

        /// <summary>
        /// Store XML content
        /// </summary>
        F95010GetWebFormXMLData datasetf95010GetWebFormXMLData = new F95010GetWebFormXMLData();

        /// <summary>
        /// variable to identify page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// XML Document
        /// </summary>
        private string xmlTestDocument;

        /// <summary>
        /// Scroll Value
        /// </summary>
        private int yPoint;

        /// <summary>
        /// Flag to control reload after Save
        /// </summary>
        private bool isSaveMode;

        /// <summary>
        /// form slice height from tts_form
        /// </summary>
        private int formHeight;

        /// <summary>
        /// flag for restrict preview keydown multiple call
        /// </summary>
        private bool skipOnce = false;

        /// <summary>
        /// Flag to identify web page loading
        /// </summary>
        private bool isWebPageLoading = false;

        #endregion Variable

        #region Constructor
        /// <summary>
        /// F95010
        /// </summary>
        public F95011()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F95010"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F95011(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            InitializeComponent();
            this.currentMasterFormNo = masterform;
            this.currentFormNo = formNo;
            this.currentBlue = blue;
            this.currentGreen = green;
            this.currentRed = red;
            this.currentTabText = tabText;
            this.previousUrl = string.Empty;
            this.keyId = keyID;
            this.WebSliceFormPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.WebSliceFormPictureBox.Height, this.WebSliceFormPictureBox.Width, this.currentTabText, this.currentRed, this.currentGreen, this.currentBlue);
        }
        #endregion Constructor

        #region EventPublication
        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Enable QuickFind for WebSlice
        /// </summary>
        [EventPublication(EventTopicNames.WebSlice_QuickFind, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> WebSlice_QuickFind;

        [EventPublication(EventTopicNames.WebSlice_FormMasterRefresh, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> WebSlice_FormMasterRefresh;

        #endregion EventPublication

        #region Property

        /// <summary>
        /// For F95010Control
        /// </summary>
        [CreateNew]
        public F95011Controller Form95011Control
        {
            get { return this.form95011Control as F95011Controller; }
            set { this.form95011Control = value; }
        }

        #endregion Property

        #region FormResize

        ///// <summary>
        ///// Called when [form slice_ resize].
        ///// </summary>
        ///// <param name="eventArgs">The event args.</param>
        //protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        //{
        //    if (this.FormSlice_Resize != null)
        //    {
        //        this.FormSlice_Resize(this, eventArgs);
        //    }
        //}

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this.currentMasterFormNo == eventArgs.Data.MasterFormNo)
                {
                    if (datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                         //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

       /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit))
            {
                mshtml.IHTMLWindow2 win = (mshtml.IHTMLWindow2)WebSliceWebBrowser.Document.Window.DomWindow;
                win.execScript("if(typeof tsSave=='function')tsSave();", "javascript");
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.isSaveMode = true;
            }
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            mshtml.IHTMLWindow2 win = (mshtml.IHTMLWindow2)WebSliceWebBrowser.Document.Window.DomWindow;
            win.execScript("if(typeof tsCancel=='function')tsCancel()", "javascript");
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.WebSlicePanel.Focus();
           // this.WebSliceWebBrowser.Focus();
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (WebSliceWebBrowser.Document != null)////Added by Biju on 12/Apr/2010 to fix #6421
            {
                mshtml.IHTMLWindow2 win = (mshtml.IHTMLWindow2)WebSliceWebBrowser.Document.Window.DomWindow;
                win.execScript("if(typeof tsNew=='function')tsNew();", "javascript");
            }
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
        }

        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            mshtml.IHTMLWindow2 win = (mshtml.IHTMLWindow2)WebSliceWebBrowser.Document.Window.DomWindow;
            win.execScript("if(typeof tsDelete=='function')tsDelete();", "javascript");
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.currentMasterFormNo == eventArgs.Data.MasterFormNo)
                {
                    //if (!this.isSaveMode)
                    //{
                        this.flagFormLoad = true;
                        this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                        this.FlagSliceForm = true;
                        this.keyId = eventArgs.Data.SelectedKeyId;

                        // this.currentFormNo = eventArgs.Data.SelectedKeyId;
                        //F95010GetWebFormXMLData 
                        datasetf95010GetWebFormXMLData = new F95010GetWebFormXMLData();
                        datasetf95010GetWebFormXMLData = this.form95011Control.WorkItem.GetWebFormXML(this.keyId, this.currentFormNo, TerraScanCommon.UserId);

                        this.formHeight = this.GetFormHeight();
                        this.SetFormHeight();

                        if (datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows.Count > 0)
                        {
                            xmlTestDocument = datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows[0][datasetf95010GetWebFormXMLData.F95010GetWebFormXML.XmlDocumentColumn.ColumnName].ToString();

                            ////this.WebSliceWebBrowser.DocumentText = xmlTestDocument.ToString();
                            if (!string.IsNullOrEmpty(xmlTestDocument))
                            this.WebSliceWebBrowser.Navigate(xmlTestDocument);
                        }
                        //else
                        //{
                        //    this.WebSliceWebBrowser.DocumentText = string.Empty;
                        //}
                        this.previousUrl = string.Empty;
                        this.flagFormLoad = false;
                    //}
                    //else
                    //{
                    //    this.isSaveMode = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                F95012.IsQuickFindFlag = true;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
            }
        }

        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F95010 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F95011_Load(object sender, EventArgs e)
        {
            try
            {
                this.flagFormLoad = true;
                this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                this.FlagSliceForm = true;
                this.WebSliceWebBrowser.ObjectForScripting = this;

                this.pageMode = TerraScanCommon.PageModeTypes.View;
                // this.WebSliceWebBrowser.Url = new Uri(@"http://google.co.in?sid=102001&9002P1=5&9002P2=1&9002PF=91000");
                // this.WebSliceWebBrowser.Url = new Uri(@"http://google.co.in");
                //F95010GetWebFormXMLData 
                datasetf95010GetWebFormXMLData = new F95010GetWebFormXMLData();
                datasetf95010GetWebFormXMLData = this.form95011Control.WorkItem.GetWebFormXML(this.keyId, this.currentFormNo, TerraScanCommon.UserId);

                this.formHeight = this.GetFormHeight();
                this.SetFormHeight();

                if (datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows.Count > 0)
                {
                    string xmlTestDocument = datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows[0][datasetf95010GetWebFormXMLData.F95010GetWebFormXML.XmlDocumentColumn.ColumnName].ToString();

                    ////string text = this.form95010Control.WorkItem.GetWebBrowserStringValue(xmlTestDocument);
                    // this.WebSliceWebBrowser.DocumentText = xmlTestDocument.ToString();

                    this.WebSliceWebBrowser.Navigate(xmlTestDocument);
                    //this.WebSliceWebBrowser.Navigate("www.google.com");
                    ////this.WebSliceWebBrowser.DocumentText = text.ToString();
                    //// this.WebSliceWebBrowser.Show();                    

                    //if (xmlTestDocument != string.Empty)
                    //{
                    //    this.WebSliceWebBrowser.DocumentText = xmlTestDocument;
                    //    //string filepath = Environment.SpecialFolder.LocalApplicationData.ToString();

                    //    ////StreamWriter sw = new StreamWriter(filepath + "\\" + "Temp.Xml");
                    //    ////sw.Write(xmlDocument);
                    //    ////sw.Close();

                    //    //string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "Temp.Xml";
                    //   // if (!File.Exists(path))
                    //    //{
                    //        // Create a file to write to.
                    //     //   using (StreamWriter sw = File.CreateText(path))
                    //     //   {
                    //     //       sw.Write(xmlDocument); 
                    //     //   }
                    //   // }

                    //    //this.WebSliceWebBrowser.Url = new System.Uri(path, System.UriKind.Absolute);
                    //}
                    //Sample subClass = new Sample();
                    //subClass.LoadSample();
                }
            }
            catch (Exception ex)
            {
                F95012.IsQuickFindFlag = true;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                // this.flagFormLoad = false;
            }
        }
        #endregion Form Load

        #region WebBrowser Event
        /// <summary>
        /// Handles the DocumentCompleted event of the WebSliceWebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.WebBrowserDocumentCompletedEventArgs"/> instance containing the event data.</param>
        private void WebSliceWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (this.WebSliceWebBrowser.ReadyState.Equals(WebBrowserReadyState.Complete) && this.formHeight <= 0)
                {
                    this.isWebPageLoading = true;
                    ////WebSliceWebBrowser.Refresh(WebBrowserRefreshOption.IfExpired);
                    string currentUrl = e.Url.Scheme + "//" + e.Url.Authority + e.Url.AbsolutePath;

                    if (!this.flagFormLoad)
                    {
                        if (this.previousUrl != e.Url.ToString() && (e.Url.AbsolutePath.ToString() != "blank"))
                        {
                            this.previousUrl = e.Url.ToString();
                            //this.WebSliceWebBrowser.ScrollBarsEnabled = false;

                            if (this.WebSliceWebBrowser.Document != null
                                && this.WebSliceWebBrowser.Document.Body != null
                                && !this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height.Equals(0))
                            {
                                this.Height = this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 8;
                                this.WebSlicePanel.Size = new System.Drawing.Size(766, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height);
                                this.WebSliceWebBrowser.Size = new System.Drawing.Size(766, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 4);
                                this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 4;
                            }
                            else
                            {
                                this.Height = this.WebSliceWebBrowser.ClientRectangle.Height + 12;
                                this.WebSlicePanel.Height = this.WebSliceWebBrowser.ClientRectangle.Height + 10;
                                this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.ClientRectangle.Height;
                            }

                            if (this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height > 460)
                            {
                                this.WebSlicePanel.Width = 766;
                                this.WebSliceWebBrowser.Width = 768;
                                this.WebSlicePanel.Height = this.WebSlicePanel.Height + 17;
                                this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.Height + 17;
                                this.Height = this.Height + 14;
                            }
                            else
                            {
                                this.WebSlicePanel.Width = 766;
                                this.WebSliceWebBrowser.Width = 768;
                            }

                            //this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                            SliceResize sliceResize;
                            sliceResize.MasterFormNo = this.currentMasterFormNo;
                            sliceResize.SliceFormName = this.GetType().FullName;
                            sliceResize.SliceFormHeight = this.Height;
                            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                            this.Height = sliceResize.SliceFormHeight;
                            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                            this.WebSliceFormPictureBox.Height = this.WebSlicePanel.Height;
                            this.WebSliceFormPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.WebSliceFormPictureBox.Height, this.WebSliceFormPictureBox.Width, this.currentTabText, this.currentRed, this.currentGreen, this.currentBlue);
                        }
                        else
                        {
                            if (this.previousUrl != e.Url.ToString())
                            {
                                if (this.WebSliceWebBrowser.Document != null
                                    && this.WebSliceWebBrowser.Document.Body != null
                                    && !this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height.Equals(0))
                                {
                                    this.Height = this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 8;
                                    this.WebSlicePanel.Size = new System.Drawing.Size(766, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height);
                                    this.WebSliceWebBrowser.Size = new System.Drawing.Size(766, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 4);
                                    this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 4;
                                }
                                else
                                {
                                    this.Height = this.WebSliceWebBrowser.ClientRectangle.Height + 12;
                                    this.WebSlicePanel.Height = this.WebSliceWebBrowser.ClientRectangle.Height + 10;
                                    this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.ClientRectangle.Height;
                                }

                                if (this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height > 460)
                                {
                                    this.WebSlicePanel.Width = 766;
                                    this.WebSliceWebBrowser.Width = 768;
                                    this.WebSlicePanel.Height = this.WebSlicePanel.Height + 17;
                                    this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.Height + 17;
                                    this.Height = this.Height + 14;
                                }
                                else
                                {
                                    this.WebSlicePanel.Width = 766;
                                    this.WebSliceWebBrowser.Width = 768;
                                }

                                //this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                                SliceResize sliceResize;
                                sliceResize.MasterFormNo = this.currentMasterFormNo;
                                sliceResize.SliceFormName = this.GetType().FullName;
                                sliceResize.SliceFormHeight = this.Height;
                                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                                this.Height = sliceResize.SliceFormHeight;
                                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                                this.WebSliceFormPictureBox.Height = this.WebSlicePanel.Height;
                                this.WebSliceFormPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.WebSliceFormPictureBox.Height, this.WebSliceFormPictureBox.Width, this.currentTabText, this.currentRed, this.currentGreen, this.currentBlue);
                            }
                        }

                        this.flagFormLoad = false;
                    }
                    else
                    {
                        if (this.previousUrl != e.Url.ToString())
                        {
                            this.previousUrl = e.Url.ToString();
                            //this.WebSlicePanel.Location = new System.Drawing.Point(-2, -2);
                           // this.WebSliceWebBrowser.ScrollBarsEnabled = false;

                            if (this.WebSliceWebBrowser.Document != null
                                && this.WebSliceWebBrowser.Document.Body != null
                                && !this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height.Equals(0))
                            {
                                this.Height = this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 8;
                                this.WebSlicePanel.Size = new System.Drawing.Size(766, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height);// + 10);
                                this.WebSliceWebBrowser.Size = new System.Drawing.Size(766, this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 4);// + 10);
                                this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height + 4;// +8;
                            }
                            else
                            {
                                this.Height = this.WebSliceWebBrowser.ClientRectangle.Height + 10;
                                this.WebSlicePanel.Height = this.WebSliceWebBrowser.ClientRectangle.Height + 10;
                                this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.ClientRectangle.Height;
                            }

                            if (this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height > 460)
                            {
                                this.WebSlicePanel.Width = 766;
                                this.WebSliceWebBrowser.Width = 768;
                                this.WebSlicePanel.Height = this.WebSlicePanel.Height + 17;
                                this.WebSliceWebBrowser.Height = this.WebSliceWebBrowser.Height + 17;
                                this.Height = this.Height + 14;
                            }
                            else
                            {
                                this.WebSlicePanel.Width = 766;
                                this.WebSliceWebBrowser.Width = 768;
                            }
                            //this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                            SliceResize sliceResize;
                            sliceResize.MasterFormNo = this.currentMasterFormNo;
                            sliceResize.SliceFormName = this.GetType().FullName;
                            sliceResize.SliceFormHeight = this.Height;
                            // sliceResize.SliceFormHeight = 800;
                            //this.WebSliceFormPictureBox.Height = sliceResize.SliceFormHeight;
                            this.WebSliceFormPictureBox.Height = this.WebSlicePanel.Height;
                            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                            this.Height = sliceResize.SliceFormHeight;
                            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                            if(!string.IsNullOrEmpty(currentTabText))
                            {
                              this.WebSliceFormPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.WebSliceFormPictureBox.Height, this.WebSliceFormPictureBox.Width, this.currentTabText, this.currentRed, this.currentGreen, this.currentBlue);
                            }
                        }

                        this.flagFormLoad = false;
                    }
                    if (!string.IsNullOrEmpty(currentTabText))
                    {
                        WebSliceWebBrowser.Document.MouseDown += new HtmlElementEventHandler(Document_Click);
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(Scroll_Click);
                        WebSliceWebBrowser.Document.ContextMenuShowing += new HtmlElementEventHandler(Document_ContextMenuShowing);
                        // WebSliceWebBrowser.Parent.MouseWheel += new MouseEventHandler(Document_Scroll);
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(Smartpart_Scroll);
                        //WebSliceWebBrowser.GoBack();
                    }
                }
                else
                {
                    this.Height = this.formHeight;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.isWebPageLoading = false;
            }
        }

        /// <summary>
        /// Handles the Navigating event of the WebSliceWebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.WebBrowserNavigatingEventArgs"/> instance containing the event data.</param>
        private void WebSliceWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            ////try
            ////{
            ////    string navigatedurl = e.Url.AbsoluteUri;
            ////    if (navigatedurl.Contains("9002P"))
            ////    {
            ////        int result, formNo;
            ////        string stringFormNo = string.Empty;
            ////        string[] empty = new string[1];
            ////        empty[0] = "9002P";
            ////        string[] parameters = navigatedurl.Split(empty, StringSplitOptions.RemoveEmptyEntries);
            ////        empty[0] = "9002";
            ////        string[] parametersForm = navigatedurl.Split(empty, StringSplitOptions.RemoveEmptyEntries);

            ////        for (int i = 0; i < parametersForm.Length; i++)
            ////        {
            ////            if (parametersForm[i].EndsWith("&"))
            ////            {
            ////                parametersForm[i] = parametersForm[i].Remove(parametersForm[i].Length - 1, 1);
            ////            }

            ////            int.TryParse(parametersForm[i].Substring(1, 1).ToString(), out result);

            ////            if (result == 0)
            ////            {
            ////                if (parametersForm[i].Substring(0, 2).ToString() == "PF")
            ////                {
            ////                    stringFormNo = parametersForm[i].Remove(0, 3);
            ////                }
            ////            }
            ////        }

            ////        int.TryParse(stringFormNo, out formNo);

            ////        FormInfo formInfo;
            ////        formInfo = TerraScanCommon.GetFormInfo(formNo);

            ////        if (parameters.Length - 2 > 0)
            ////        {
            ////            formInfo.optionalParameters = new object[parameters.Length - 2];
            ////        }

            ////        for (int i = 0; i < parameters.Length; i++)
            ////        {
            ////            if (parameters[i].EndsWith("&"))
            ////            {
            ////                parameters[i] = parameters[i].Remove(parameters[i].Length - 1, 1);
            ////            }

            ////            int.TryParse(parameters[i].Substring(0, 1).ToString(), out result);
            ////            if (result != 0)
            ////            {
            ////                formInfo.optionalParameters[result - 1] = parameters[i].Remove(0, 2);
            ////            }
            ////            else
            ////            {
            ////                if (parameters[i].Substring(0, 1).ToString() == "0")
            ////                {
            ////                    formInfo.optionalParameters[result] = parameters[i].Remove(0, 2);
            ////                }
            ////            }
            ////        }

            ////        // formInfo.optionalParameters = new object[1];
            ////        // formInfo.optionalParameters[0] = this.systemSnapShotId;
            ////        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

            ////       // string xmlTestDocument = datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows[0][datasetf95010GetWebFormXMLData.F95010GetWebFormXML.XmlDocumentColumn.ColumnName].ToString();

            ////        //string text = this.form95010Control.WorkItem.GetWebBrowserStringValue(xmlTestDocument);
                 
            ////       //this.WebSliceWebBrowser.DocumentText = xmlTestDocument.ToString();
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            ////}
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the WebSliceWebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void WebSliceWebBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F && Control.ModifierKeys == Keys.Control)
                {
                    this.WebSlice_QuickFind(this, new DataEventArgs<int>(this.currentMasterFormNo));
                    return;
                }

                if (e.KeyCode.Equals(Keys.F5))
                {
                    //essageBox.Show("F5");
                    this.WebSlice_FormMasterRefresh(this, new DataEventArgs<int>(this.currentMasterFormNo));
                    return;
                }

                // Code added for Bug #6053: Activity Queue form is not closing.
                if (this.skipOnce)
                {
                    this.skipOnce = false;
                    return;
                }

                //if (e.KeyCode == Keys.F && Control.ModifierKeys == Keys.Control)
                //{
                //    this.WebSlice_QuickFind(this, new DataEventArgs<int>(this.currentMasterFormNo));
                //}

                this.skipOnce = true;

                //if (e.KeyCode.Equals(Keys.F5))
                //{
                //    MessageBox.Show("F5");
                //}
                //SendKeys.Send("F5");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion WebBrowser Event

        #region Scroll
        /// <summary>
        /// Handles the Click event of the Scroll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void Scroll_Click(object sender, ScrollEventArgs e)
        {
            yPoint = e.NewValue;
        }

        /// <summary>
        /// Handles the Scroll event of the Smartpart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Smartpart_Scroll(object sender, MouseEventArgs e)
        {
            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                yPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
            }
        }

        /// <summary>
        /// Handles the Click event of the Document control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.HtmlElementEventArgs"/> instance containing the event data.</param>
        private void Document_Click(object sender, HtmlElementEventArgs e)
        {
            int xxPoint = e.MousePosition.X;
            int yyPoint = e.MousePosition.Y;
            if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, yPoint);
            }

            e.ReturnValue = false;
        }

        /// <summary>
        /// Handles the ContextMenuShowing event of the Document control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.HtmlElementEventArgs"/> instance containing the event data.</param>
        private void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, yPoint);
            e.ReturnValue = false;
        }
        #endregion Scroll

        #region PictureBox Event
        /// <summary>
        /// Handles the Click event of the AuditTrailPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AuditTrailPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the AuditTrailPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AuditTrailPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.WebsliceFormToolTip.SetToolTip(this.WebSliceFormPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion PictureBox Event

        #region Client Event

        /// <summary>
        /// Refresh form master from web form
        /// </summary>
        [ComVisible(true)]
        public void RefreshFormMaster()
        {
            this.WebSlice_FormMasterRefresh(this, new DataEventArgs<int>(this.currentMasterFormNo));
        }

        /// <summary>
        /// Clients the form changed.
        /// </summary>
        [ComVisible(true)]
        public void ClientFormChanged()
        {
            this.SetEditRecord();
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        public void SetEditRecord()
        {
            //if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            //{
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.currentMasterFormNo));
            //}
        }

        /// <summary>
        /// T2s the form call.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        public void T2FormCall(int formId, int keyId)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(formId);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Client Event
       
        #region Methods

        /// <summary>
        /// Gets the height of the form.
        /// </summary>
        /// <returns></returns>
        private int GetFormHeight()
        {
            int configuredHeight = 0;
            if (this.datasetf95010GetWebFormXMLData.WebFormHeight.Rows.Count > 0
                && this.datasetf95010GetWebFormXMLData.WebFormHeight.Rows[0][this.datasetf95010GetWebFormXMLData.WebFormHeight.WebHeightColumn.ColumnName] != null)
            {
                int.TryParse(this.datasetf95010GetWebFormXMLData.WebFormHeight.Rows[0][this.datasetf95010GetWebFormXMLData.WebFormHeight.WebHeightColumn.ColumnName].ToString(), out configuredHeight);

                if (configuredHeight > 8190)
                {
                    configuredHeight = 8190;
                }
            }

            return configuredHeight;
        }

        /// <summary>
        /// Sets the height of the form.
        /// </summary>
        private void SetFormHeight()
        {
            if (this.formHeight > 0)
            {
                this.Height = this.formHeight;
                this.WebSlicePanel.Size = new System.Drawing.Size(766, this.formHeight + 3);
                this.WebSliceWebBrowser.Size = new System.Drawing.Size(768, this.formHeight + 3);
               // this.WebSliceWebBrowser.Height = this.formHeight;

                this.WebSliceWebBrowser.ScrollBarsEnabled = true;
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.currentMasterFormNo;
                sliceResize.SliceFormName = this.GetType().FullName;
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.Height = sliceResize.SliceFormHeight;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.WebSliceFormPictureBox.Height = this.WebSlicePanel.Height;
                this.WebSliceFormPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.WebSliceFormPictureBox.Height, this.WebSliceFormPictureBox.Width, this.currentTabText, this.currentRed, this.currentGreen, this.currentBlue);
            }
        }

        #endregion Methods

        /// <summary>
        /// Event added to fix "Form partially blank", if the application is idle for some time.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F95011_Resize(object sender, EventArgs e)
        {
            if (!this.isWebPageLoading)
            {
                this.Height = this.WebSliceFormPictureBox.Height;
            }
            //this.Height = this.WebSliceWebBrowser.Height;
            //if (this.WebSliceWebBrowser.Document != null)
            //{
            //    this.Height = this.WebSliceWebBrowser.Document.Body.ScrollRectangle.Height;// this.WebSliceWebBrowser.Height;
            //}
        }
    }
}
