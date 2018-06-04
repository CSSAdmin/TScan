
namespace D3200
{
    using System;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using System.Runtime.InteropServices;
    using System.Xml;
    using System.Diagnostics;
    using Terrascan.Catalog;
    using System.Windows.Forms;
    using System.IO;
    using System.Windows.Forms.Integration;
    using Terrascan.Common.Data;
    using System.Data;
    using ApexCAMASketch;

    /// <summary>
    /// 
    /// </summary>
    [SmartPart]
    public partial class F32012 : BaseSmartPart
    {
        #region Methods/Consts for Embedding a Window

        private const int WM_SYSCOMMAND = 274;
        private const int SC_MAXIMIZE = 61488;
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        #endregion

        #region Variables

       //  ApexCAMASketch sketchForm = null;
        // CatalogControl catalogForm = null;

        ///<summary>
        /// used to hold ParcelID
        /// </summary>
        private int ParcelId;
        public static int tempID=0;

        /// <summary>
        ///  used to store objectID
        /// </summary>
        private int objectID;

        /// <summary>
        /// Catalog data
        /// </summary>
        F32012CatalogData camaSketchData = new F32012CatalogData();

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F32012Controller form32012Control;

        /// <summary>
        ///// Variable to start the exe file
        ///// </summary>
        private Process catalogProcess = null;

        ///// <summary>
        ///// Variable used to store the process information
        ///// </summary>
        private ProcessStartInfo catalogProcessInfo = new ProcessStartInfo();

        /// <summary>
        /// Hold form master number
        /// </summary>
        private int masterFormNumber;

        /// <summary>
        /// Catalog Window
        /// </summary>
        private CatalogWindow cataLogWindowForm = null;

        /// <summary>
        /// Catalog Control
        /// </summary>
        private CatalogControl catalogForm = null;

        /// <summary>
        /// Parcel xml 
        /// </summary>
        private string parcelData;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        private bool isFormLoad = false;
        //F3205 apexHandlerForm = null;
        Form apexForm = null;

        /// <summary>
        /// Used for identify Apex Opened
        /// </summary>
        public static bool  IsApexOpened = false;

        private bool errorOnSave = false;

        private bool isInEditMode = false; 

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SetViewMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_SetViewMode;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declared the event SubFormSave of D35000_F35002
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;
      
        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;


         ///<summary>
         ///Used to identify the ApexOpenedEvent
         ///</summary>
        [EventPublication(EventTopicNames.F9030_ApexOpenEvent, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<bool>>  F9030_ApexOpenEvent;

       

        #endregion Event Publication

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F32012"/> class.
        /// </summary>
        public F32012()
        {
            InitializeComponent();
            this.Load += new EventHandler(F32012_Load);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35060"/>
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F32012(int masterForm, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.Load += new EventHandler(F32012_Load);
            this.objectID = keyID;
            this.FlagSliceForm = true;
            this.Tag = formNo;
            this.masterFormNumber = masterForm;
            this.CatalogPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CatalogPictureBox.Height, this.CatalogPictureBox.Width, tabText, red, green, blue);
            //this.FormClose += new EventHandler<DataEventArgs<string>>(F32012_FormClose);
            
            
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the F8052 control.
        /// </summary>
        /// <value>The F8052 control.</value>
        [CreateNew]
        public F32012Controller F32012Control
        {
            get { return this.form32012Control as F32012Controller; }

            set { this.form32012Control = value; }
        }

       

        #endregion

        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                //((F3205)apexForm).Close(); 
                this.FormSlice_FormCloseAlert(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SlicePermissionReload&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNumber == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                if (this.objectID > -99)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.Cursor = Cursors.WaitCursor;
            if (this.catalogForm != null)
            {
                this.catalogForm.WriteXml();
                if (this.parcelData != null)
                {
                    DataSet returnValue = this.form32012Control.WorkItem.F32012_SaveCatalog(this.objectID, this.parcelData, TerraScanCommon.UserId);
                    string contentText = string.Empty;
                    if (returnValue.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(returnValue.Tables[0].Rows[0][1].ToString()))
                    {
                        contentText = returnValue.Tables[0].Rows[0][1].ToString();
                        Form exportForm = new Form();
                        object[] optionalParameter = new object[] { contentText };
                        exportForm = this.form32012Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3299, optionalParameter, this.form32012Control.WorkItem);
                        if (exportForm != null)
                        {
                            exportForm.ShowDialog();
                        }

                        this.errorOnSave = true;
                        //SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        //sliceValidationFields.FormNo = eventArgs.Data;
                        //sliceValidationFields.ErrorMessage = string.Empty;
                        //sliceValidationFields.DisableNewMethod = true;
                        //sliceValidationFields.RequiredFieldMissing = false;
                        //this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    }
                    else
                    {
                       
                            this.catalogForm.RefreshWebPage();
                       
                        this.errorOnSave = false;
                        //catalogForm = new CatalogControl();
                        //mainPanel.Controls.Clear();
                        //this.LoadCreateCatalogForm();
                        //this.Cursor = Cursors.Default;
                    }
                    this.catalogForm.DisableSketch(false);
                    this.catalogForm.IsSketchEnable = 1;
                   // this.catalogForm.DisposeGDI();
                    
                        //this.catalogForm.Dispose();
                        //this.catalogForm = null;
                        //catalogForm = new CatalogControl();
                        //mainPanel.Controls.Clear();
                        this.isInEditMode = false;
                        //this.LoadCreateCatalogForm();
                        this.Cursor = Cursors.Default;

                    #region SaveValueSlice Event

                    // Update Appraisal Summary Table
                    // Value has been hard coded, have to implement in future
                    decimal calculatedAmount = 0;

                    F35002SubFormSaveEventArgs subFormSaveEventArgs;
                    subFormSaveEventArgs.type = 5;
                    subFormSaveEventArgs.value = calculatedAmount;
                    subFormSaveEventArgs.valueSliceId = this.objectID;

                    subFormSaveEventArgs.amount = calculatedAmount;
                    this.Cursor = Cursors.Default;
                    this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));

                    #endregion SaveValueSlice Event
                }

            }

            //SliceValidationFields sliceValidationFields = new SliceValidationFields();
            //sliceValidationFields.FormNo = eventArgs.Data;
            //sliceValidationFields.ErrorMessage = string.Empty;
            //sliceValidationFields.DisableNewMethod = true;
            //sliceValidationFields.RequiredFieldMissing = false;
            //this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            //this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNumber));
            this.Cursor = Cursors.Default;


        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            //this.objectID = this.form32012Control.WorkItem.F32012_SaveCatalog(this.objectID, this.parcelData, TerraScanCommon.UserId);
            if (this.errorOnSave)
            {
                this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNumber));
            }

            this.errorOnSave = false;
        }


        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.catalogForm != null)
            {
               
                   // this.catalogForm.DisposeGDI();  
                    this.catalogForm.Dispose();
                    this.catalogForm = null;
                    catalogForm = new CatalogControl();
                    mainPanel.Controls.Clear();
                
                
                //catalogForm.DisposeGDI();
                
                //this.catalogForm._datacontext._wrapper._dataset.UpdateChanges(false);
                // Refresh object collection
                // this.catalogForm._datacontext.RefreshObjectCollection();
                this.isFormLoad = true;
                // this.catalogForm.ReloadCatalog();
                // Call the method to load the EXE.
                this.isInEditMode = false; 
                this.LoadCreateCatalogForm();
                this.Cursor = Cursors.Default;
                this.isFormLoad = false;
            }
        }

        /// <summary>
        /// Handles the FormReload event of the F3201_F32012 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.F3201_F32012_FormReload, ThreadOption.UserInterface)]
        public void F3201_F32012_FormReload(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data.Equals(this.ParcelId))
            {
                //DialogResult dialogResult = MessageBox.Show("Do you want to discard the changes?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.Yes)
                //{
                //this.FormActivateEvent(this, new DataEventArgs<string>(formInfo.formFile));
                this.isInEditMode = false; 
                this.isFormLoad = true;
                if (this.catalogForm != null)
                {
                    catalogForm.Dispose();
                    catalogForm = null;
                    catalogForm = new CatalogControl();
                    mainPanel.Controls.Clear();
                    IsApexOpened = false;
                    this.LoadCreateCatalogForm();
                }
                else
                {
                    catalogForm = new CatalogControl();
                    mainPanel.Controls.Clear();
                    IsApexOpened = false;
                    this.LoadCreateCatalogForm();

                }
                this.Cursor = Cursors.Default;
                this.isFormLoad = false;

                //}
            }
            else
            {
                if (!this.isInEditMode)
                {
                    this.F9030_ApexOpenEvent(this, new DataEventArgs<bool>(false));
                   
                }
            }
            
        }

         /// <summary>
        /// Handles the FormClose of APex event Loading Different ID
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.F9030_ApexCloseEvent, ThreadOption.UserInterface)]
        public void F9030_ApexCloseEvent(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data.Equals(this.masterFormNumber))   
            {
                if (apexForm != null)
                {
                    ((F3205)apexForm).CloseSketch();   
                }
            }

        
        }

        [EventSubscription(EventTopicNames.ApexOpenedEvent, ThreadOption.UserInterface)]
        public void ApexOpenEvent(object sender, DataEventArgs<bool> eventArgs)
        {
            if (eventArgs.Data.Equals(true))
            {
                this.isFormLoad = true; 
                IsApexOpened = true;
                if (catalogForm != null)
                {
                    catalogForm.DisableSketch(true);
                }
                this.Cursor = Cursors.Default;
                this.isFormLoad = false;
            }
            else
            {
                this.isFormLoad = true;
                 IsApexOpened = false;
                 if (catalogForm != null)
                 {
                     catalogForm.DisableSketch(false);
                 }
                this.Cursor = Cursors.Default;
                this.isFormLoad = false;
            }
        }
        /// <summary>
        /// Handles the Load event of the F32012 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void F32012_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.isFormLoad = true;
                // Call the method to load the EXE.
                this.LoadCreateCatalogForm();
                this.ParentForm.FormClosing += new FormClosingEventHandler(this.ParentForm_FormClosing);
               //this.FormClose += new EventHandler<DataEventArgs<string>>(F32012_FormClose);
                //this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNumber));
                //this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNumber));
            }
            catch (Exception formshowEx)
            {
                ExceptionManager.ManageException(formshowEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                 
                this.Cursor = Cursors.Default;
                this.isFormLoad = false;
            }
        }
        public void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (apexForm != null)
            {
              //  MessageBox.Show("Please Close the previous apex before opening new sketch", "Terrascan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              // ((F3205)apexForm).closeApexForm();
                ((F3205)apexForm).CloseSketch();
               
            }
            if (string.IsNullOrEmpty((sender as System.Windows.Forms.Form).Text))
            {
                //if (this.camaSketchData != null)
                //{
                //    this.camaSketchData.Clear();
                //    this.camaSketchData.Dispose();
                //    this.camaSketchData = null;
                //}
                if (this.mainPanel != null)
                {
                    this.mainPanel.Controls.Clear();
                    //foreach (Control ctrl in this.mainPanel.Controls)
                    //{
                    //    this.mainPanel.Controls.Remove(ctrl);
                    //}
                    //this.mainPanel.Controls.Remove(catalogForm);
                    this.mainPanel.Dispose();
                    this.mainPanel = null;
                }
                if (this.OverallPanel != null)
                {
                    this.OverallPanel.Controls.Clear();
                    this.OverallPanel.Dispose();
                    this.OverallPanel = null;
                }
                if (this.catalogForm != null)
                {
                    // this.catalogForm.Objects = null;
                    this.CatalogPictureBox.Dispose();
                    this.CatalogPictureBox = null;
                    this.catalogProcessInfo = null;
                    catalogForm.ApexFormOpened -= new CatalogControl.OpenEventHandler(catalogForm_ApexFormOpened);
                    catalogForm.LinkFormOpened -= new CatalogControl.OpenLinkEventHandler(catalogForm_LinkFormOpened);
                    catalogForm.ParcelSaved -= new EventHandler<ParcelSavedEventArgs>(catalogForm_ParcelSaved);
                    catalogForm.FormMasterEditEnabled -= new EventHandler(this.CatalogForm_EditEnabled);
                    //if (this.form32012Control != null)
                    //{
                    //    if (this.form32012Control.WorkItem != null)
                    //    {
                    //        //this.catalogForm.DisposeGDI();
                    //        this.form32012Control.WorkItem.Deactivate();
                    //        this.form32012Control.WorkItem.Terminate();
                    //        this.form32012Control.WorkItem.Dispose();
                    //    }
                    //}
                    // this.form32012Control.WorkItem = null;
                    this.form32012Control = null;
                    this.catalogForm.DisposeGDI();  
                    this.catalogForm.Dispose();
                    this.catalogForm = null;
                }
                this.Dispose();
                this.DestroyHandle();
                GC.Collect(); 
            }
           
           
        }
        //[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool DestroyWindow(IntPtr hwnd);

        //[DllImport("User32")]
        //extern public static int GetGuiResources(IntPtr hProcess, int uiFlags);

        //public static int GetGuiResourcesGDICount()
        //{
        //    return GetGuiResources(Process.GetCurrentProcess().Handle, 0);
        //}

        //public static int GetGuiResourcesUserCount()
        //{
        //    return GetGuiResources(Process.GetCurrentProcess().Handle, 1);
        //}
      
        #region Method

        /// <summary>
        /// Loads the create catalog form.
        /// </summary>
        private  void LoadCreateCatalogForm()
        {

            // Used to store the parcel value
            string cataLogval;

            // Used to store the value
            string parcelVal;

            // Used to Store the Component (HTC Value)
            string htcVal;

            // Used to Store config value for the cama sketch.
            string configVal;

            // Stores the mainwindow handle of the process.
            IntPtr exPtr = IntPtr.Zero;

            // Checks for the object Id
            
            if (this.objectID > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                // Call the workitem to get the parameters.
                camaSketchData = this.form32012Control.WorkItem.F32012_GetCatalogData(this.objectID);
                // Check
                if (camaSketchData.ConfigurationValue.Count > 0 && camaSketchData.CatalogXML.Count > 0
                      && camaSketchData.ConfigXML.Count > 0 && camaSketchData.HtcXML.Count > 0
                     && camaSketchData.ParcelXML.Count > 0)
                {
                    //// Gets the needed parameter.
                    cataLogval = camaSketchData.CatalogXML[0]["CatalogXml"].ToString();
                    configVal = camaSketchData.ConfigXML[0]["ConfigXml"].ToString();
                    parcelVal = camaSketchData.ParcelXML[0]["ParcelXml"].ToString();
                    htcVal = camaSketchData.HtcXML[0]["HtcXml"].ToString();

                    if (!string.IsNullOrEmpty(parcelVal.Trim()))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(parcelVal);

                        XmlNode node = doc.DocumentElement.SelectSingleNode("//ParcelID");
                        int.TryParse(node.InnerText, out this.ParcelId);
                        tempID=this.ParcelId; 
                        // Pass the parameter to the catalog dll 
                        // and initialize the form then show it
                        // cataLogWindowForm = new CatalogWindow();
                        catalogForm = null;
                        catalogForm = CatalogControl.Create(cataLogval, htcVal, configVal, parcelVal);


                        // Response.Write("node = " + node);  
                        //catalogForm.InitializeComponent();
                        // used for Opening ApexForm
                        try
                        {
                            catalogForm.ApexFormOpened += new CatalogControl.OpenEventHandler(catalogForm_ApexFormOpened);
                            catalogForm.LinkFormOpened += new CatalogControl.OpenLinkEventHandler(catalogForm_LinkFormOpened);

                            if (camaSketchData.ConfigurationValue.Rows.Count > 0)
                            {
                                F32012CatalogData.ConfigurationValueRow configRow = (F32012CatalogData.ConfigurationValueRow)camaSketchData.ConfigurationValue.Rows[0];
                                if (!configRow.IsIsSketchVisibleNull())
                                {
                                    int sketchVisible = 0;
                                    int.TryParse(configRow.IsSketchVisible.ToString(), out sketchVisible);
                                    catalogForm.IsSketchVisible = sketchVisible;
                                }
                                else
                                {
                                    catalogForm.IsSketchVisible = 0;
                                }
                            }
                            if (!IsApexOpened)
                            {
                                //catalogForm.SketchButton = true;   
                                catalogForm.DisableSketch(false);
                            }
                            else
                            {
                                //catalogForm.SketchButton = false;   
                                catalogForm.DisableSketch(true);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("There was an error launching the Apex Sketch module. It may not have been installed properly. Please contact T2 Support for further assistance.", "TerraScan – Error opening Apex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catalogForm.ParcelSaved += new EventHandler<ParcelSavedEventArgs>(catalogForm_ParcelSaved);
                        catalogForm.FormMasterEditEnabled += new EventHandler(this.CatalogForm_EditEnabled);

                        // Code added to host for WPF control 
                        //ElementHost elementHost = new ElementHost();
                        //elementHost.Dock = DockStyle.None;
                        //elementHost.Width = (int)catalogForm.Width;
                        //elementHost.Height = (int)catalogForm.Height;
                        //elementHost.Child = catalogForm;
                        //mainPanel.Controls.Add(elementHost);
                        mainPanel.Controls.Add(catalogForm);

                        // Size has been changed based on the control size
                        //this.mainPanel.Size = new System.Drawing.Size(catalogForm.Width, catalogForm.Height + 5);
                        //this.Size = new System.Drawing.Size(catalogForm.Width, catalogForm.Height + 5);
                        //this.mainPanel.Size = new System.Drawing.Size(elementHost.Width, elementHost.Height);
                        //this.Size = new System.Drawing.Size(elementHost.Width, elementHost.Height);

                        //this.catalogForm._datacontext._wrapper._dataset.UpdateChanges(true);
                    }
                    else
                    {
                        MessageBox.Show("Invalid ValueSlice Id", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid ValueSlice Id", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void catalogForm_LinkFormOpened(object sender)
        {
            object[] optionalParameter = { this.ParcelId };
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(3201);
            formInfo.optionalParameters = new object[1];
            formInfo.optionalParameters[0] = this.ParcelId;
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }



        #endregion Method

        #region Event


        private void catalogForm_ApexFormOpened(object sender, string filePath)
        {
            if (!IsApexOpened)
            {

                //    IsApexOpened = true;
                //catalogForm.DisableSketch(true);
                if (apexForm != null)
                {
                    //  MessageBox.Show("Please Close the previous apex before opening new sketch", "Terrascan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ((F3205)apexForm).CloseSketch();
                }

                //catalogForm.DisableSketch(true);

                object[] optionalParameter = { this.ParcelId };
                apexForm = TerraScanCommon.GetForm(3205, optionalParameter, this.form32012Control.WorkItem);//(3205, optionalParameter, this.form32012Control.WorkItem);
                ////open form in view mode - possible to edit
                if (apexForm != null)
                {
                    apexForm.Show();

                }


                //catalogForm.DisableSketch(true);

                IsApexOpened = true;
                  
                this.F9030_ApexOpenEvent(this, new DataEventArgs<bool>(true));
                   
            }
        }

        //public void CloseSketch()
        //{
        //    if (apexHandlerForm != null)
        //    {
        //        AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexHandlerForm.Controls["apexExchange"];
        //        existingApex.Focus();
        //        existingApex.Select();
        //        existingApex.CloseApex();
        //    }
        //}

        /// <summary>
        /// Handles the ParcelSaved event of the catalogForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Terrascan.Catalog.ParcelSavedEventArgs"/> instance containing the event data.</param>
        private void catalogForm_ParcelSaved(object sender, ParcelSavedEventArgs e)
        {
            this.parcelData = e.ParcelXML;
        }

        private void CatalogForm_EditEnabled(object sender, EventArgs e)
        {
            if (!this.isFormLoad)
            {
                if (catalogForm != null)
                {
                    catalogForm.IsSketchEnable = 0;
                }
                this.isInEditMode = true;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNumber));
            }
        }

        #endregion Event
    }
}
