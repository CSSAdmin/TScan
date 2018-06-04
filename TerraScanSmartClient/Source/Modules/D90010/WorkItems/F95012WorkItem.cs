// -------------------------------------------------------------------------------------------
// <copyright file="F95010WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F95010</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  Suganth Mani       Created// 
// 
// -------------------------------------------------------------------------------------------
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
    using TerraScan.Helper;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.XPath;
    using System.Xml.Xsl;

    public class F95012WorkItem : WorkItem
    {
        #region F95010WebFormXML

        #region F95010GetWebFormXML

        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="keyId">The key ID.</param>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public F95010GetWebFormXMLData GetWebFormXML(int? keyId, int form, int userId)
        {
            return WSHelper.GetWebFormXML(keyId, form, userId);
        }
        #endregion F95010GetWebFormXML

        /// <summary>
        /// Gets the web browser string value.
        /// </summary>
        /// <param name="websliceXml">The webslice XML.</param>
        /// <returns></returns>
        public string GetWebBrowserStringValue(string websliceXml)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(websliceXml);

            StringWriter StrWriter = new StringWriter();
            XmlTextWriter XTextWriter = new XmlTextWriter(StrWriter);
            xmlDoc.WriteTo(XTextWriter);
            string soapBodyString = StrWriter.ToString();

            //read XML
            TextReader tr1 = new StringReader(soapBodyString);
            XmlTextReader tr11 = new XmlTextReader(tr1);
            XPathDocument xPathDocument = new XPathDocument(tr11);

            //read XSLT
            TextReader tr2 = new StringReader(soapBodyString);
            XmlTextReader tr22 = new XmlTextReader(tr2);
            XslTransform xslt = new XslTransform();
            xslt.Load(tr22);


            //create the output stream
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            xslt.Transform(xPathDocument, null, tw);

            //get result
            string resultXML = sb.ToString();

            return resultXML;
        }

        #endregion F95010WebFormXML
    }
}
