//--------------------------------------------------------------------------------------------
// <copyright file="AboutBox.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Information about the Application.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 May 06		Thilak Raj		        Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Deployment;
using System.Deployment.Application;
using System.IO;


namespace TerraScan.UI
{

    /// <summary>
    /// This Class contains Information about the Application
    /// </summary>
    public partial class AboutBox : Form
    {
        #region Variables

        /// <summary>
        /// sketchVersion
        /// </summary>
        string sketchVersion;

        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AboutBoxForm"/> class.
        /// </summary>
        public AboutBox()
        {
            this.InitializeComponent();

            ////  Initialize the AboutBox to display the product information from the assembly information.
            ////  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            // this.Text = String.Format("About {0}", this.AssemblyTitle);
            ////this.labelProductName.Text = this.AssemblyProduct;
            //this.labelVersion.Text = String.Format("Version {0}", this.AssemblyVersion);
            //int copyrightLength = this.AssemblyCopyright.Length;
            //int cutyear = copyrightLength-4;
            this.labelCopyright.Text = this.AssemblyCopyright.Remove(28, 4) + DateTime.Now.Year.ToString();
            //System.Version objVersion = System.Reflection.Assembly.LoadFile(Environment.CurrentDirectory.Replace("Source\\Infrastructure\\Shell\\bin\\Debug", "Lib\\visi3200.dll")).GetName().Version;
            //this.sketchVersion = objVersion.ToString();
            Assembly asswmblyVersion = Assembly.GetExecutingAssembly();
            //asswmblyVersion = Assembly.LoadFrom("visi3200.dll");

            /*
            // Execute Assembly Location
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // Get directory(Full directory path) of the execute assembly
            string appDir = Path.GetDirectoryName(appPath);
            // Sketch dll path
            string sketchDll = appDir + "\\visi3200.dll";
            // Version of Sketch dll
            System.Version objVersion = Assembly.LoadFrom(sketchDll).GetName().Version;
           
            this.sketchVersion = objVersion.ToString();*/
            //this.descriptionTexbox.Text = sketchDll + "  :  " + objVersion.ToString(); 

            this.GetDeployInfo();
        }

        #region Assembly Attribute Accessors

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        /// <value>The assembly version.</value>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the assembly description.
        /// </summary>
        /// <value>The assembly description.</value>
        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                //// If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                {
                    return "";
                }
                //// If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        /// <value>The assembly product.</value>
        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                //// If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                {
                    return "";
                }
                //// If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                //// If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                {
                    return "";
                }
                //// If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the assembly company.
        /// </summary>
        /// <value>The assembly company.</value>
        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                //// If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                {
                    return "";
                }
                //// If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        /// <value>The assembly title.</value>
        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                //// If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    //// If it is not an empty string, return it
                    if (!string.IsNullOrEmpty(titleAttribute.Title))
                    {
                        return titleAttribute.Title;
                    }
                }
                //// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        #endregion

        #region ClickOnce Info

        /// <summary>
        /// Gets the deploy info.
        /// </summary>
        internal void GetDeployInfo()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                if (ad != null)
                {
                    try
                    {

                        //this.descriptionTexbox.Text += "Local Data Directory: " + ad.DataDirectory + "\n" + "Version: " + ad.CurrentVersion.ToString() + "\n" + "Sketch DLL Version: " + this.sketchVersion + "\n" + "First time running this version: " + ad.IsFirstRun.ToString() + "\n" + "Last Checked for Updates: " + ad.TimeOfLastUpdateCheck.ToString() + "\n" + "Location of Updates: " + ad.UpdateLocation.AbsoluteUri + "\n" + "Updates Available: " + ad.CheckForUpdate().ToString();
                        this.descriptionTexbox.Text += "Local Data Directory: " + ad.DataDirectory + "\n" + "Version: " + ad.CurrentVersion.ToString() + "\n" + "First time running this version: " + ad.IsFirstRun.ToString() + "\n" + "Last Checked for Updates: " + ad.TimeOfLastUpdateCheck.ToString() + "\n" + "Location of Updates: " + ad.UpdateLocation.AbsoluteUri + "\n" + "Updates Available: " + ad.CheckForUpdate().ToString();
                        //descriptionTextBox.Text += "\nVersion: " + ad.CurrentVersion.ToString();
                        //descriptionTextBox.Text += "\nSketch DLL Version: " + this.sketchVersion;
                        //descriptionTextBox.Text += "\nFirst time running this version: " + ad.IsFirstRun.ToString();
                        //descriptionTextBox.Text += "\nLast Checked for Updates: " + ad.TimeOfLastUpdateCheck.ToString();
                        //descriptionTextBox.Text += "\nLocation of Updates: " + ad.UpdateLocation.AbsoluteUri;
                        //descriptionTextBox.Text += "\nUpdates Available: " + ad.CheckForUpdate().ToString();
                        //descriptionTextBox.Text += "\n";
                    }
                    catch (Exception ex)
                    {
                        this.descriptionTexbox.Text += ex.Message;

                    }
                }
            }
            else
            {
                this.descriptionTexbox.Text += "\n" + "Cannot contact update server...";
            }

            this.descriptionTexbox.Text = this.descriptionTexbox.Text;
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the OkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
 

    }
}
