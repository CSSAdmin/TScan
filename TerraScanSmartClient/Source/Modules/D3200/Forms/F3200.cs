//--------------------------------------------------------------------------------------------
// <copyright file="F3200.cs" company="Congruent">
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
// 13 Nov 07        Guhan              Created
//*********************************************************************************/


namespace D3200
{
    #region NameSpace
    using System;
    using System.Data;
    using System.IO;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using System.Xml;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using VisiCAMASketch;


    #endregion
    public partial class F3200 : Form
    {
        #region Variables

        /// <summary>
        ///  used to check form closed or not
        /// </summary>
        public static bool formClosed;

        /// <summary>
        ///  used to check form  already opened
        /// </summary>
        public static bool formOpened;

        /// <summary>
        ///  used to check form  is MDI Child or not
        /// </summary>
        private Boolean mdiChild;

        /// <summary>
        ///  used to store the parent Form object
        /// </summary>
        object parentFrm;

        /// <summary>
        ///  used to store parcelID
        /// </summary>
        private int parcelID;

        /// <summary>
        ///  used to store objectID
        /// </summary>
        private int objectID;

        /// <summary>
        /// used to store sketchForm
        /// </summary>
        //public static VisiCAMASketchForm sketchForm = null;

        /// <summary>
        /// used to store camaskethcData
        /// </summary>
        F3200CamaSketchData camaSketchData = new F3200CamaSketchData();

        /// <summary>
        /// formExistsflag
        /// </summary>
        private bool formExistsflag;

        private F3200Controller form3200Control;

        #endregion

        #region Constructor

        public F3200()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form35000 controll.
        /// </summary>
        /// <value>The form35000 controll.</value>
        /// 
        public Boolean FormClosed
        {
            set { formClosed = value; }
            get { return formClosed; }
        }

        /// <summary>
        /// Gets or sets the get opened form.
        /// </summary>
        /// <value>The get opened form.</value>
        //public VisiCAMASketchForm GetOpenedForm
        //{
        //    set { }
        //    get { return sketchForm; }
        //}

        /// <summary>
        /// Gets or sets the set MDI.
        /// </summary>
        /// <value>The set MDI.</value>
        public object SetMDI
        {
            set
            {
                parentFrm = value;
               // loadSkethcForm(parentFrm);
            }
            get
            {
                return parentFrm;
            }
        }

        /// <summary>
        /// Gets or sets the form35000 controll.
        /// </summary>
        /// <value>The form35000 controll.</value>
        public int SetObjectID
        {
            set
            {
                objectID = value;
            }
        }

        /// <summary>
        /// Gets or sets the form35000 controll.
        /// </summary>
        /// <value>The form35000 controll.</value>
        public int SetParcelID
        {
            set
            {
                parcelID = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [form opened].
        /// </summary>
        /// <value><c>true</c> if [form opened]; otherwise, <c>false</c>.</value>
        public static bool FormOpened
        {
            set
            {
                formOpened = value;
            }
            get
            {
                return formOpened;
            }
        }

        [CreateNew]
        public F3200Controller F3200Control
        {
            get { return this.form3200Control as F3200Controller; }

            set { this.form3200Control = value; }
        }
        #endregion

        #region Method

       /* /// <summary>
        /// Loads the skethc form.
        /// </summary>
        /// <param name="parentForm">The parent form.</param>
        public void loadSkethcForm(object parentForm)
        {
            // Used to store the config value
            string configValtTemp = string.Empty;

            // Used to store the parcel value
            string cataLogval = string.Empty;

            // Used to store the value
            string parcelVal = string.Empty;

            // Used to Store the Component (HTC Value)
            string htcVal = string.Empty;

            // Used to Store config value for the cama sketch.
            string configVal = string.Empty;

            try
            {
                if (this.objectID > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //// Calls DB To get XML
                    camaSketchData = WSHelper.F3200_GetSketchData(this.objectID);
                   
                    // Check
                    if(camaSketchData.ConfigurationValue.Count > 0 && camaSketchData.CatalogXML.Count > 0
                          && camaSketchData.ConfigXML.Count > 0 && camaSketchData.HtcXML.Count > 0
                         && camaSketchData.ParcelXML.Count > 0)
                    {
                        //try
                        //{
                            //// Call the visi dll with required parameter
                            cataLogval = camaSketchData.CatalogXML[0]["CatalogXml"].ToString();
                            configVal = camaSketchData.ConfigXML[0]["ConfigXml"].ToString();
                            parcelVal = camaSketchData.ParcelXML[0]["ParcelXml"].ToString();
                            htcVal = camaSketchData.HtcXML[0]["HtcXml"].ToString();

                          
                           configValtTemp = camaSketchData.ConfigurationValue[0]["ConfigurationValue"].ToString();

                            // Check all required fields are available 
                            if (!string.IsNullOrEmpty(cataLogval) && !string.IsNullOrEmpty(configVal)
                                  && !string.IsNullOrEmpty(parcelVal) && !string.IsNullOrEmpty(htcVal))
                            {
                                // Create instance for the CAMA Sketch Form
                                sketchForm = new VisiCAMASketchForm(cataLogval, configVal, htcVal);

                                // Assing parcel
                                sketchForm.Parcel = parcelVal;

                                // Set form opened 
                                FormOpened = true;
                            }
                            else
                            {
                                ////this.formExistsflag = false;
                                MessageBox.Show(SharedFunctions.GetResourceString("Empty Input"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        //}
                        //catch (System.Runtime.InteropServices.COMException ee)
                        //{
                        //    if (ee.ErrorCode == -2032465765)
                        //    {
                        //        MessageBox.Show("VSD file for the selected parcel is not available", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //}
                        //catch (System.Exception ex)
                        //{
                        //    MessageBox.Show(ex.ToString());
                        //}
                        //finally
                        //{
                        //    this.Cursor = Cursors.Default;
                        //}

                        // Check for Is open as MDI Form
                        if (configValtTemp == "1")
                        {
                            this.mdiChild = true;
                            sketchForm.MdiParent = (Form)parentForm;
                            Form mdiForm = ((Form)this.SetMDI);
                            foreach (Form currentForm in sketchForm.MdiParent.MdiChildren)
                            {
                                if (currentForm is VisiCAMASketchForm)
                                {
                                    sketchForm.BringToFront();
                                    FormOpened = true;

                                }
                            }
                            Form mdiForm1 = ((Form)this.SetMDI);
                            TerraScanCommon.SetValue(mdiForm, "SetActiveWindow", 3200);
                        }
                        else
                        {
                            sketchForm.Text = SharedFunctions.GetResourceString("F3200_CamaSkethTerraScan"); ;
                            sketchForm.WindowState = FormWindowState.Maximized;
                            FormOpened = true;
                        }

                        // Set the Form name
                        sketchForm.Name = SharedFunctions.GetResourceString("F3200_CamaSketh");

                        // Bind the Events
                        sketchForm.SketchChanged += new VisiCAMASketchForm.EVisiCAMASketchForm_SketchChangedEventHandler(sketchForm_SketchChanged);
                        sketchForm.FormClosed += new FormClosedEventHandler(sketchForm_FormClosed);
                        sketchForm.Disposed += new EventHandler(sketchForm_Disposed);
                         
                        // Set Form as Maximized
                        sketchForm.WindowState = FormWindowState.Maximized;

                        // Show the Form
                        sketchForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("CamaSketchObjectID"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (System.Runtime.InteropServices.COMException ee)
            {
                if (ee.ErrorCode == -2032465765)
                {
                    //MessageBox.Show("VSD file for the selected parcel is not available", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExceptionManager.ManageException("VSD file for the selected parcel is not available.", ee, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                else
                {
                    //MessageBox.Show("Exception Occured. Please contact your administrator.", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExceptionManager.ManageException(ee, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }

                sketchForm = null;
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("A VSType that doesn't exist in the catalog was assigned to a terragon."))
                {
                   // MessageBox.Show("A VSType that doesn't exist in the catalog was assigned to a terragon.", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExceptionManager.ManageException("A VSType that doesn't exist in the catalog was assigned to a terragon.", ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                else if (ex.Message.Equals("A problem occurred while initializing the Visio control. Ensure that Microsofot Office Visio has been installed and activated."))
                {
                    //MessageBox.Show("A problem occurred while initializing the Visio control. Ensure that Microsofot Office Visio has been installed and activated.", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExceptionManager.ManageException("A problem occurred while initializing the Visio control. Ensure that Microsofot Office Visio has been installed and activated.", ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                else
                {
                   // MessageBox.Show("Exception Occured. Please contact your administrator.", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }

                sketchForm = null;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }*/


        #endregion

        #region Events

        /// <summary>
        /// Handles the FormClosed event of the sketchForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        void sketchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                FormOpened = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        ///  Used For Save The CamaSkethc SML
        /// </summary>
        /// <param name="xmlDataOut"></param>
        void sketchForm_SketchChanged(string xmlDataOut)
        {
            try
            {
                DataSet saveResult = WSHelper.F3200_SaveSketchData(this.objectID, xmlDataOut, TerraScanCommon.UserId);
                string contentText = string.Empty;
                if (saveResult.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(saveResult.Tables[0].Rows[0][1].ToString()))
                {
                    contentText = saveResult.Tables[0].Rows[0][1].ToString();
                    Form exportForm = new Form();
                    object[] optionalParameter = new object[] { contentText };
                    //exportForm = this.form3200Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3299, optionalParameter, this.form3200Control.WorkItem);
                   // exportForm = TerraScanCommon.GetForm("3299", optionalParameter, true);
                    F3299 checkForm = new F3299();
                    checkForm.HtmlContent = contentText;
                    checkForm.ShowDialog();
                    //if (exportForm != null)
                    //{
                    //    exportForm.ShowDialog();
                    //}
                }
            }
            catch (Exception ex)
            {
                //sketchForm.Dispose();
                //sketchForm.Close();
                //this.Dispose();
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                //sketchForm.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// Used Remove the Winodscount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void sketchForm_Disposed(object sender, EventArgs e)
        {
            try
            {
                if (this.mdiChild)
                {
                    Form mdiForm = ((Form)this.SetMDI);
                    FormOpened = false;
                    TerraScanCommon.SetValue(mdiForm, "DeActiveWindow", 3200);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion  Events
    }

}