//--------------------------------------------------------------------------------------------
// <copyright file="F9510.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9510.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Dec 08        LathaMaheswari.D    Created
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
    /// 9510
    /// </summary>
    [ComVisible(true)]
    public partial class F9510 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// previous url
        /// </summary>
        private string previousUrl;

        /// <summary>
        /// controller F95005
        /// </summary>
        private F9510Controller form9510Control;

        /// <summary>
        /// Flag for Form Load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// Store XML content
        /// </summary>
        private F95010GetWebFormXMLData datasetf95010GetWebFormXMLData = new F95010GetWebFormXMLData();

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F2550"/> class.
        /// </summary>
        public F9510()
        {
            InitializeComponent();
        }

        #endregion

        #region EventPublication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion EventPublication

        #region Properties

        /// <summary>
        /// Gets or sets the F9510 control.
        /// </summary>
        /// <value>The F9510 control.</value>
        [CreateNew]
        public F9510Controller Form9510Control
        {
            get { return this.form9510Control as F9510Controller; }
            set { this.form9510Control = value; }
        }

        #endregion

        #region Page Load

        /// <summary>
        /// Handles the Load event of the F9510 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9510_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.WebSliceWebBrowser.ObjectForScripting = this;
                this.datasetf95010GetWebFormXMLData = new F95010GetWebFormXMLData();
                ////Get Form Number
                int formId = (int)this.Tag;
                this.datasetf95010GetWebFormXMLData = this.form9510Control.WorkItem.GetWebFormXML(formId, TerraScanCommon.UserId);
                if (this.datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows.Count > 0)
                {
                    string xmlTestDocument = this.datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows[0][this.datasetf95010GetWebFormXMLData.F95010GetWebFormXML.XmlDocumentColumn.ColumnName].ToString();
                    ////Set navigate url for WebBrowser
                    this.WebSliceWebBrowser.Navigate(xmlTestDocument);
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

        #endregion

        #region WebBrowser Event

        /// <summary>
        /// Handles the DocumentCompleted event of the WebSliceWebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.WebBrowserDocumentCompletedEventArgs"/> instance containing the event data.</param>
        private void WebSliceWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if (this.previousUrl != e.Url.ToString() && (e.Url.AbsolutePath != SharedFunctions.GetResourceString("NavigateUrl")))
                    {
                        this.previousUrl = e.Url.ToString();
                    }
                }
                else
                {
                    this.previousUrl = e.Url.ToString();
                }

                this.flagFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Navigating event of the WebSliceWebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.WebBrowserNavigatingEventArgs"/> instance containing the event data.</param>
        private void WebSliceWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                string navigatedurl = e.Url.AbsoluteUri;
                ////For 9002 form call
                if (navigatedurl.Contains(SharedFunctions.GetResourceString("NavigateUrlStarts")))
                {
                    byte result;
                    int formNo;
                    string stringFormNo = string.Empty;
                    string[] empty = new string[1];
                    empty[0] = SharedFunctions.GetResourceString("NavigateUrlStarts");
                    string[] parameters = navigatedurl.Split(empty, StringSplitOptions.RemoveEmptyEntries);
                    empty[0] = SharedFunctions.GetResourceString("NavigateForm");
                    string[] parametersForm = navigatedurl.Split(empty, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < parametersForm.Length; i++)
                    {
                        if (parametersForm[i].EndsWith("&"))
                        {
                            parametersForm[i] = parametersForm[i].Remove(parametersForm[i].Length - 1, 1);
                        }

                        byte.TryParse(parametersForm[i].Substring(1, 1).ToString(), out result);

                        ////Get Form number from url
                        if (result.Equals(0))
                        {
                            if (parametersForm[i].Substring(0, 2).ToString().Equals(SharedFunctions.GetResourceString("NavigateUrlContains")))
                            {
                                stringFormNo = parametersForm[i].Remove(0, 3);
                            }
                        }
                    }

                    int.TryParse(stringFormNo, out formNo);

                    ////Get Form information
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(formNo);

                    if (parameters.Length - 2 > 0)
                    {
                        formInfo.optionalParameters = new object[parameters.Length - 2];
                    }

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        if (parameters[i].EndsWith("&"))
                        {
                            parameters[i] = parameters[i].Remove(parameters[i].Length - 1, 1);
                        }

                        byte.TryParse(parameters[i].Substring(0, 1).ToString(), out result);

                        ////Assign parameter values to formInfo
                        if (result != 0)
                        {
                            formInfo.optionalParameters[result - 1] = parameters[i].Remove(0, 2);
                        }
                        else
                        {
                            if (parameters[i].Substring(0, 1).ToString().Equals(SharedFunctions.GetResourceString("Zero")))
                            {
                                formInfo.optionalParameters[result] = parameters[i].Remove(0, 2);
                            }
                        }
                    }

                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    //string xmlTestDocument = this.datasetf95010GetWebFormXMLData.F95010GetWebFormXML.Rows[0][this.datasetf95010GetWebFormXMLData.F95010GetWebFormXML.XmlDocumentColumn.ColumnName].ToString();
                    //this.WebSliceWebBrowser.DocumentText = xmlTestDocument.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// T2s the form call.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        [ComVisible(true)]
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

      #endregion WebBrowser Event
    }
}
