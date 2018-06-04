

namespace D3200
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using ApexCAMASketch;
    using Terrascan.Catalog; 
    using System.Collections;
    using System.IO;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Security.Permissions;
    using System.Security.AccessControl;
    using System.Diagnostics;
    
    public partial class F3205 : BasePage
    {
        #region Variables

        /// <summary>
        /// Apex user control
        /// </summary>
        private  ApexSketch apexSketch;


        private CatalogControl catalog;

        /// <summary>
        /// F3502Controller variable.
        /// </summary>
        private F3205Controller form3205Controll;

        /// <summary>
        /// DataSet for the form F3205
        /// </summary>
        private F3205ApexSketchData ApexSketchData;

        /// <summary>
        /// DataSet for the form F3205
        /// </summary>
        private F3205ApexSketchData.F3205pcgetSketchLinksExistDataTable SketchLinksExistData;


        /// <summary>
        /// DataSet for the save F3205
        /// </summary>
        private F3205ApexSketchData saveSketchData;

        private int keyId;

        /// <summary>
        /// used to hold the XMl Data Count
        /// </summary>
        public string fileIDDataxml = "";

        private static int sketchCount = 0;


        bool flagLoadOnProcess = false;

        /// <summary>
        ///  Used to hold the Sketch Images
        /// </summary>
        private int PageCount = 0;

        public string ApexPathFolder= "";

        F32012 Fpmr; 
        #endregion Varaiables

        #region Constructor
        public F3205()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
           

        }

        public F3205(int parcelId)
        {
            this.InitializeComponent();
            this.keyId = parcelId;
            //this.PageCount = pageCount;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
           

        }


        [EventPublication(EventTopicNames.F3201_F32012_FormReload, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> F3201_F32012_FormReload;

        public bool CheckApexAvailability()
        {
            bool isExisted = false;
            if (apexSketch != null)
            {

                AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"];
                if (existingApex != null)// && existingApex.InterruptCode != 2729)
                {

                    foreach (Process processList in Process.GetProcesses())
                    {
                        if (processList.ProcessName.Contains("apexwin5p"))
                        {
                            //if (existingApex.Tag != null && processList.MainWindowTitle.ToUpper().Contains(existingApex.Tag.ToString().Trim().ToUpper()))
                            if (existingApex.Tag != null && processList.Id.Equals(existingApex.Tag))
                            {
                                isExisted = true;
                                break;
                            }
                        }


                    }

                    if (!isExisted)
                    {
                        this.ApexPanel.Controls.Remove(apexSketch);
                        apexSketch = null;
                        this.Close();
                    }

                    return isExisted;
                }
               
            }
            return isExisted;
        }

      
        public void CloseSketch()
        {
            if (apexSketch != null)
            {
                AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"];
                //apexSketch.SketchState = "12";
                if (existingApex != null)// && existingApex.InterruptCode != 2729)
                {
                    bool isExisted = false;
                    foreach (Process processList in Process.GetProcesses())
                    {
                        //if (processList.ProcessName.Contains("apexwin5p"))
                        //{
                        //    if (existingApex.Tag != null && processList.MainWindowTitle.ToUpper().Contains(existingApex.Tag.ToString().Trim().ToUpper()))
                        //    {
                        //        isExisted = true;
                        //        break;
                        //    }
                        //}
                        if (processList.ProcessName.Contains("apexwin5p"))
                        {
                            //if (existingApex.Tag != null && processList.MainWindowTitle.ToUpper().Contains(existingApex.Tag.ToString().Trim().ToUpper()))
                            if (existingApex.Tag != null && processList.Id.Equals(existingApex.Tag))
                            {
                                isExisted = true;
                                break;
                            }
                        }


                    }

                    if (isExisted)
                    {
                        existingApex.Focus();
                        existingApex.Select();
                       // existingApex.SaveFile();
                        //existingApex.CloseSketch();  
                        existingApex.CloseApex();
                        this.ApexPanel.Controls.Remove(apexSketch);
                        if (apexSketch != null)
                        {
                                          
                            apexSketch = null;
                         
                        }
                    }
                    else
                    {
                        this.ApexPanel.Controls.Remove(apexSketch);
                        apexSketch = null;
                        this.Close();
                    }
                }
                else
                {
                    this.ApexPanel.Controls.Remove(apexSketch);
                    apexSketch = null;
                    this.Close();
                }
            }
            
        }


        #endregion Constructor

        #region properties



        /// <summary>
        /// Gets or sets the form35001 controll.
        /// </summary>
        /// <value>The form35001 controll.</value>
        [CreateNew]
        public F3205Controller Form3205Controll
        {
            get { return this.form3205Controll as F3205Controller; }
            set { this.form3205Controll = value; }
        }



        #endregion properties

        #region Event publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        ///<summary>
        ///Used to identify the ApexOpenedEvent
        ///</summary>
        [EventPublication(EventTopicNames.F9030_ApexOpenEvent, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<bool>> F9030_ApexOpenEvent;

        #endregion Event Publication

        [EventSubscription(EventTopicNames.ApexLogOutEvent , ThreadOption.UserInterface)]
        public void ApexLogOutEvent(object sender, DataEventArgs<bool> eventArgs)
        {
            bool permit= this.CheckApexAvailability();
            TerraScanCommon.IsApexAvail   = permit;  
        }

        private void F3205_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.openPermission)
                {
                    flagLoadOnProcess = true;
                    if (apexSketch != null)
                    {
                        AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"];
                        existingApex.CloseApex();
                    }
                    if (!TerraScanCommon.IsFieldUser)
                    {
                        this.ApexSketchData = this.form3205Controll.WorkItem.F3205pcgetSketchFilePath(this.keyId, TerraScanCommon.UserId);
                        if (this.ApexSketchData.F3205pcgetSketchFilePath.Rows.Count > 0)
                        {
                            string ApexFilePath = this.ApexSketchData.F3205pcgetSketchFilePath.Rows[0][0].ToString();
                            Uri uriAddress2 = new Uri(ApexFilePath);
                            //FileInfo f = new FileInfo(ApexFilePath); 
                            //if (!uriAddress2.IsLoopback)
                            //{
                            //    string[] path = ApexFilePath.Split('\\');
                            //    int length = path.Length;
                            //    int param = path[length - 1].Length;
                            //    int FileLength = ApexFilePath.Length;
                            //    if (length < 5)
                            //    {
                            //        MessageBox.Show("There was Error in Creating Apex File UNC File Path.", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    }
                            //    else
                            //    {
                            //        this.ApexPathFolder = ApexFilePath.Remove(FileLength - (param), param);
                            //        FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Write, this.ApexPathFolder);
                            //        f2.AllFiles = FileIOPermissionAccess.Write;
                            //        string Permission = f2.AllFiles.ToString();
                            //        if (Permission.Equals("NoAccess"))
                            //        {
                            //            MessageBox.Show("There was Error in Creating Apex File.", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        }
                            //        else
                            //        {
                            //            this.ApexPanel.Controls.Add(apexSketch);
                            //            apexSketch = new ApexSketch();
                            //            apexSketch.FilePath = this.ApexSketchData.F3205pcgetSketchFilePath.Rows[0][0].ToString();
                            //            sketchCount = 1;
                            //            apexSketch.ApexSaveImage += new ApexSketch.SaveImageHandler(apexSketch_ApexSaveImage);
                            //            apexSketch.ApexFormClosed += new ApexSketch.CloseEventHandler(apexSketch_ApexFormClosed);
                            //            flagLoadOnProcess = false;
                            //        }
                            //    }

                            //}
                            //else
                            //{
                            if (!uriAddress2.IsLoopback)
                            {
                                string[] path = ApexFilePath.Split('\\');
                                int length = path.Length;
                                int param = path[length - 1].Length;
                                int FileLength = ApexFilePath.Length;
                                if (length < 5)
                                {
                                    MessageBox.Show("There was Error in Creating Apex File UNC File Path.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {

                                    this.ApexPathFolder = ApexFilePath;
                                    this.ApexPathFolder = ApexFilePath.Remove(FileLength - (param + 1), param + 1);
                                    bool permission = HasWritePermissionOnDir(this.ApexPathFolder);
                                    if (permission)
                                    {
                                        this.ApexPanel.Controls.Add(apexSketch);
                                        apexSketch = new ApexSketch();
                                        apexSketch.FilePath = this.ApexSketchData.F3205pcgetSketchFilePath.Rows[0][0].ToString();
                                        sketchCount = 1;
                                        apexSketch.ApexSaveImage += new ApexSketch.SaveImageHandler(apexSketch_ApexSaveImage);
                                        apexSketch.ApexFormClosed += new ApexSketch.CloseEventHandler(apexSketch_ApexFormClosed);
                                        apexSketch.InterLogEvent += new ApexSketch.InterruptEventLogging(apexSketch_InterLogEvent);
                                        flagLoadOnProcess = false;

                                        // Code added to dispose object to avoid exception on immediate close on apex load
                                        // while trying to close apex without its initialization completes
                                        if (((AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"]).ApplicationStatus != null
                                            && ((AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"]).ApplicationStatus.ToUpper().Equals("CLOSED"))
                                        {
                                            AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"];
                                            this.ApexPanel.Controls.Remove(apexSketch);
                                            apexSketch = null;
                                            this.Close();
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        
                            this.ApexSketchData = this.form3205Controll.WorkItem.F3205pcgetSketchFilePath(this.keyId, TerraScanCommon.UserId);
                            if (this.ApexSketchData.F3205pcgetSketchFilePath.Rows.Count > 0)
                            {
                                string ApexFilePath = this.ApexSketchData.F3205pcgetSketchFilePath.Rows[0][0].ToString();
                                string SpecialPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                                string tempPath1 = SpecialPath + @"\Terrascan Attachment\Apex Path\" + ApexFilePath;
                                this.ApexPathFolder =SpecialPath + @"\Terrascan Attachment\Apex Path";
                               this.ApexPanel.Controls.Add(apexSketch);
                                apexSketch = new ApexSketch();
                                apexSketch.FilePath = tempPath1;
                                sketchCount = 1;
                                apexSketch.ApexSaveImage += new ApexSketch.SaveImageHandler(apexSketch_ApexSaveImage);
                                apexSketch.ApexFormClosed += new ApexSketch.CloseEventHandler(apexSketch_ApexFormClosed);
                                apexSketch.InterLogEvent += new ApexSketch.InterruptEventLogging(apexSketch_InterLogEvent);
                                flagLoadOnProcess = false;

                                            // Code added to dispose object to avoid exception on immediate close on apex load
                                            // while trying to close apex without its initialization completes
                                            if (((AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"]).ApplicationStatus != null
                                                && ((AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"]).ApplicationStatus.ToUpper().Equals("CLOSED"))
                                            {
                                                AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"];
                                                this.ApexPanel.Controls.Remove(apexSketch);
                                                apexSketch = null;
                                                this.Close();
                                            }
                                        }
                                    }
                                }
                
                        }

                    
            catch (Exception ex)
            {
                if (ex.Message.Equals("Method failed with unexpected error code 53."))
                {
                    MessageBox.Show("Please provide the specific Network path to create apex file", "Terrascan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ex.Message.Equals("Attempted to perform an unauthorized operation."))
                {
                    MessageBox.Show("Please provide the folder permission to create apex file", "Terrascan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this);
                    MessageBox.Show("There was an error launching the Apex Sketch module. It may not have been installed properly. Please contact T2 Support for further assistance.", "TerraScan – Error opening Apex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
        }

        private void apexSketch_ApexSaveImage(object sender, int pageCount, bool apexSave)
        {
            try
            {
                if (!TerraScanCommon.IsFieldUser)
                {
                    if (apexSave)
                    {
                        this.ApexSketchData = this.form3205Controll.WorkItem.F3205pcinsSketchImage(this.keyId, TerraScanCommon.UserId, pageCount);
                        ////Re called the method to create thumnails by purushotham during Solving process TFS#19913 on 22Dec2013
                       this.createThumbNail();
                    }

                }
            }
            catch (Exception ex)
            {
               ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this);
               MessageBox.Show("There was an error creating or updating the Sketch Image Attachment record. The rest of the Save process will proceed as normal. If this happens again, please contact T2 Support for further assistance.", "TerraScan T2 - Error Saving Sketch Image", MessageBoxButtons.OK ,MessageBoxIcon.Warning);   
            }
        }
        ///<summary>
        /// Apex Sketch Log Event for the Interruption
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="LogMessage">The Log Message.</param>
        private void apexSketch_InterLogEvent(object sender, string MessageLog)
        {
            try
            {
                Exception ex = new Exception(MessageLog);
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this);
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.JustLog, this);
            }

        }

        public void createThumbNail()
        {
            if (this.ApexSketchData.F3205pcinsSketchImage.Rows.Count > 0)
            {
                DataTable F3205SketchImage = this.ApexSketchData.F3205pcinsSketchImage;
                apexSketch.SaveJpegPath = F3205SketchImage;
                ///Create Generate Thumbnails
                DataTable fileID = new DataTable();
                fileID.TableName = "Table";
                DataColumn fileIDCol = new DataColumn();
                DataRow fileIDRow;
                DataSet fileDataSet = new DataSet();
                fileDataSet.DataSetName = "Root";
                fileIDCol = new DataColumn();
                fileIDCol.ColumnName = "FileID";
                //fileIDCol.DataType = System.Type.GetType("System.String");
                fileIDCol.AutoIncrement = false;
                fileIDCol.Caption = "FileID";
                fileIDCol.ReadOnly = false;
                fileIDCol.Unique = false;
                fileID.Columns.Add(fileIDCol);
                for (int index = 0; index < this.ApexSketchData.F3205pcinsSketchImage.Rows.Count; index++)
                {
                    fileIDRow = fileID.NewRow();
                    fileIDRow["FileID"] = this.ApexSketchData.F3205pcinsSketchImage.Rows[index][0];
                    fileID.Rows.Add(fileIDRow);
                }
                fileDataSet.Tables.Add(fileID);
                this.fileIDDataxml = fileDataSet.GetXml();
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                ///Used to Generate Thumbnails for the form
                this.form3205Controll.WorkItem.GenerateThumbnail(null, TerraScanCommon.UserId, this.fileIDDataxml);

            }
        }

        /// <summary>
        /// Apexes the sketch_ apex form closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="apexData">The apex data.</param>
        /// <param name="pageCount">The page count.</param>
        /// 
        private void apexSketch_ApexFormClosed(object sender, string apexData, int pageCount, bool apexSave)
        {

            try
            {
                ///Used to Store the Sketch data
                if (apexData != null && apexSave)
                {
                    this.form3205Controll.WorkItem.SaveApexSketch(apexData, this.keyId, TerraScanCommon.UserId);
                    //Called this method to create medium and short tumbnails Solves TFS#19913 by Purushotham on 22Dec2013
                    this.createThumbNail(); 
                    this.ApexSketchData = this.form3205Controll.WorkItem.F3205pcgetSketchLinksExist(this.keyId, TerraScanCommon.UserId);
                    if (this.ApexSketchData.F3205pcgetSketchLinksExist.Rows.Count > 0)
                    {
                        
                        int sketchLink;
                        bool sketchLinkExist = (this.ApexSketchData.F3205pcgetSketchLinksExist.Rows[0][0].ToString().Equals("1") ? true : false);
                       //sketchLinkExist =  this.ApexSketchData.F3205pcgetSketchLinksExist.Rows[0][0].ToString();
                        //bool.TryParse(this.ApexSketchData.F3205pcgetSketchLinksExist.Rows[0][0].ToString(), out sketchLinkExist);
                        if (!sketchLinkExist)
                        {
                            ///used to open the Sketch Link Form if Sketch Link does not exist
                         //   this.Cursor = Cursors.WaitCursor;
                          //  this.ParentForm.Close();
                            if (this.ShowForm != null)
                            {
                                object[] optionalParameter = { this.keyId };
                                FormInfo formInfo;
                                formInfo = TerraScanCommon.GetFormInfo(3201);
                                formInfo.optionalParameters = new object[1];
                                formInfo.optionalParameters[0] = this.keyId;
                                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                            }
                        }
                        else
                        {
                            ///used to call the execute Recalculate RCN Values for the form
                             string Result = this.form3205Controll.WorkItem.F3205_pcexeReCalcValues(TerraScanCommon.UserId, this.keyId);
                             if (!string.IsNullOrEmpty(Result))
                             {
                                 object[] optionalParameter = { Result };
                                 Form SketchErrorMsg = TerraScanCommon.GetForm(3299, optionalParameter, form3205Controll.WorkItem);//(3205, optionalParameter, this.form32012Control.WorkItem);
                                 ////open form in view mode - possible to edit
                                 if (SketchErrorMsg != null)
                                 {
                                     SketchErrorMsg.ShowDialog() ;

                                 }
                             }
                        }
                    }
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                    if (apexSketch != null)
                    {

                        AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"];
                        this.ApexPanel.Controls.Remove(apexSketch);
                        apexSketch = null;
                        this.Close();
                    }
                }
                else
                    if (apexData != null)
                    {
                         if (apexSketch != null)
                    {

                        AxExchange2XControl1.AxExchange2X existingApex = (AxExchange2XControl1.AxExchange2X)apexSketch.Controls["apexExchange"];
                        this.ApexPanel.Controls.Remove(apexSketch);
                        apexSketch = null;
                        this.Close();
                    }

                         bool apexOpen=false;
                         F32012.IsApexOpened = false;
                         if (this.F9030_ApexOpenEvent != null)
                         {
                             this.F9030_ApexOpenEvent(this, new DataEventArgs<bool>(apexOpen));
                         }
                        
                        
                         
                                         
                    }
             
                   
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this);
                MessageBox.Show("The Apex Sketch Application has encountered an error and must close. If you receive this error message repeatedly, please contact T2 Support", "TerraScan T2 - Error in Apex Sketch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            finally
            {
                sketchCount = 0;
                apexSketch = null;
                
                
            }
        }
        private void LoadEnabled()
        {

        }
     

        public static bool HasWritePermissionOnDir(string path)
        {
            var writeAllow = false;
            //var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
               // else if (rule.AccessControlType == AccessControlType.Deny)
                    //writeDeny = true;
            }

            return writeAllow ;
        }

        private void F3205_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            try
            {
                
                this.F3201_F32012_FormReload(this, new DataEventArgs<int>(this.keyId));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this);
            }
        }
    }
}
