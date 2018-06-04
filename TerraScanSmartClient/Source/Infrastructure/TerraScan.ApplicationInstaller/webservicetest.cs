// -------------------------------------------------------------------------------------------
// <copyright file="WebServiceTest.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update NextNumber Configuration</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ---------------	---------		   ---------------------------------------------------------
// 14 September 06		DINESH	       Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.ApplicationInstaller
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Net;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using System.IO;
    using System.Configuration.Install;

    /// <summary>
    /// WebService Test
    /// </summary>
    public partial class Webservicetest : Form
    {
        /// <summary>
        /// WebService URL Test
        /// </summary>
        /// <param name="wsurl">WebService URL</param>
        public Webservicetest(string wsurl)
        {
            this.InitializeComponent();
            this.wsurltextbox.Text = wsurl;
            this.statuslabel.Text = "Web Service URL is InValid !";
            this.statuslabel.BackColor = Color.Red;
            this.wsurltextbox.Focus();
        }
        public string Updatedurl = "";

        /// <summary>
        /// Uses codedom to create the webservice test DLL and execute a method provided
        /// against the web service.
        /// </summary>
        /// <param name="webServiceAsmxUrl">Web Service URL</param>
        /// <param name="serviceName">Web Service Name</param>
        /// <param name="methodName">Method Name</param>
        /// <param name="args">Method Arguments</param>
        /// <returns>Web Service Response Object</returns>
        public object Testwsurl(string webServiceAsmxUrl, string serviceName, string methodName, object[] args)
        {
            args = new object[] { "Success" };
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(webServiceAsmxUrl);
            webRequest.Timeout = 10000;
            webRequest.UserAgent = "TerraScan Treasurer";

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader responseStreamlo = new StreamReader(webResponse.GetResponseStream(), enc);

            string response = responseStreamlo.ReadToEnd();

            responseStreamlo.Close();
            webResponse.Close();
            
            // MessageBox.Show(Response);
            // return webResponse;

            System.Net.WebClient client = new System.Net.WebClient();

            // -Connect To the web service
            System.IO.Stream stream = client.OpenRead(webServiceAsmxUrl + "?wsdl");
            
            // --Now read the WSDL file describing a // service.
            System.Web.Services.Description.ServiceDescription description = ServiceDescription.Read(stream);
            
            ///// LOAD THE DOM /////////
            
            // --Initialize a service description importer.
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap12"; // Use SOAP 1.2.
            importer.AddServiceDescription(description, null, null);
            
            // --Generate a proxy client. 
            importer.Style = ServiceDescriptionImportStyle.Client;
            
            // --Generate properties to represent pri // mitive values.
            importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
            
            // --Initialize a Code-DOM tree into which we will import the service.
            CodeNamespace nmspace = new CodeNamespace();
            CodeCompileUnit unit1 = new CodeCompileUnit();
            unit1.Namespaces.Add(nmspace);
            
            // --Import the service into the Code-DOM tree. 
            
            // --This creates proxy code that uses the service.
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);
            if (warning == 0) // --If zero then we are good to go
            {
                // --Generate the proxy code 
                CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");
                
                // --Compile the assembly proxy with the appropriate references
                string[] assemblyReferences = new string[5] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };
                CompilerParameters parms = new CompilerParameters(assemblyReferences);
                CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);
                
                // -Check For Errors
                if (results.Errors.Count > 0)
                {
                    foreach (CompilerError oops in results.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine("========Compiler error============");
                        System.Diagnostics.Debug.WriteLine(oops.ErrorText);
                    }

                    throw new System.Exception("Compile Error Occured calling webservice. Check Debug ouput window.");
                }
                
                // --Finally, Invoke the web service method 
                object wsvcClass = results.CompiledAssembly.CreateInstance(serviceName);
                MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                return mi.Invoke(wsvcClass, args);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check the Web Service URL Entered.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eevent args</param>
        private void Checkwsurlbutton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Uri uri = new Uri(this.wsurltextbox.Text);

                string webServiceAsmxUrl = this.wsurltextbox.Text;
                string serviceName = "TerraScanService";
                string methodName = "CheckInstallation";
                object[] args = null;
                object test = this.Testwsurl(webServiceAsmxUrl, serviceName, methodName, args);

                if (test.ToString() == "Success")
                {
                    this.statuslabel.Text = "Web Service URL is Valid !";
                    this.statuslabel.BackColor = Color.Green;
                    MessageBox.Show("Valid URL", "WebService Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Updatedurl = this.wsurltextbox.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid URL", "WebService Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.statuslabel.Text = "Web Service URL is InValid !";
                    this.statuslabel.BackColor = Color.Red;
                    this.wsurltextbox.Focus();
                }
            }
            catch (UriFormatException urexp)
            {
                MessageBox.Show("Invalid URL Format !!" + urexp.Message, "WebService Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (WebException webexp)
            {
                if (webexp.Message.Contains("404"))
                {
                    MessageBox.Show("Web Service URL is not valid", "WebService Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (webexp.Message.Contains("403"))
                {
                    MessageBox.Show("Web Service URL is not valid", "WebService Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (this.wsurltextbox.Text == "")
                {
                    MessageBox.Show("Please Enter a URI to Validate","WebService Validation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("404 error " + ex.Message + ex.InnerException);
                }
            }
        }
    }
}