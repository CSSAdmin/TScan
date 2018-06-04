namespace D3230
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.SqlServer.Management.Common;
    using System.IO;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infragistics.Win.UltraWinGrid;
    using TerraScan.Helper;
    using System.Threading;
    using System.Xml;

    [SmartPart]
    public partial class F3230 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Background thread
        /// </summary>
        private static BackgroundWorker backGroundWork;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        /// <summary>
        /// Background thread
        /// </summary>
        private static BackgroundWorker backGroundWorkCheckIn;

        /// <summary>
        /// backGroundWorkRemoveData
        /// </summary>
        private static BackgroundWorker backGroundWorkRemoveData;

        /// <summary>
        /// f9060Controller Controller.
        /// </summary>
        private F3230Controller form3230Control;

        /// <summary>
        /// F9065FieldUseData
        /// </summary>
        private F9065FieldUseData fieldUseDataSetData = new F9065FieldUseData();

        /// <summary>
        /// apprasialLock
        /// </summary>
        private bool apprasialLock;

        /// <summary>
        /// valueLock
        /// </summary>
        private bool valueLock;

        /// <summary>
        /// newCheckoutDB
        /// </summary>
        private bool newCheckoutDB;

        /// <summary>
        /// event publication for logout event
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_LogoutEvent, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> LogoutEvent;

        /// <summary>
        /// insertValue
        /// </summary>
        private string insertValue;

        /// <summary>
        /// updateValue
        /// </summary>
        private string updateValue;

        /// <summary>
        /// adminLock
        /// </summary>
        private bool adminLock;

        /// <summary>
        /// snapShotId
        /// </summary>
        ///// private int snapShotId;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// scriptFilePath
        /// </summary>
        private string scriptFilePath = Environment.CurrentDirectory + @"\DBO.T2FieldScript.sql";

        /// <summary>
        /// F3230FieldUseData
        /// </summary>
        private F3230FieldUseData fieldCheckOutDataSet = new F3230FieldUseData();

        /// <summary>
        /// fieldUseDataSet
        /// </summary>
        private F3230FieldUseData fieldUseDataSet = new F3230FieldUseData();

        /// <summary>
        /// fieldCheckInDataSet
        /// </summary>
        private F3230CheckInData fieldCheckInDataSet = new F3230CheckInData();

        /// <summary>
        /// beforeCheckinDataSet
        /// </summary>
        private F3230CheckInData beforeCheckinDataSet = new F3230CheckInData();

        /// <summary>
        /// F3230FieldUseData
        /// </summary>
        private F3230FieldUseData apexfileDataSet = new F3230FieldUseData();

        /// <summary>
        /// F3230CheckIn
        /// </summary>
        private F3230FieldUseData checkInData = new F3230FieldUseData();

        /// <summary>
        /// checkDataSet
        /// </summary>
        private DataSet checkDataSet = new DataSet();

        /// <summary>
        /// snapShotId
        /// </summary>
        private int selectedSnapShotId;

        /// <summary>
        /// Progressform
        /// </summary>
        private Progressform formProgress;


        ///<summary>
        /// ListBoxFOrm
        /// </summary>
       private ListBoxForm listBoxForm;

        ///<summary>
        /// listBoxForm Visibility
        /// </summary>
       private bool fileMissing = false;

        /// <summary>
        /// chkOutXML
        /// </summary>
        private string chkOutXML = string.Empty;

        /// <summary>
        /// Check Out DataTable
        /// </summary>
        private DataTable checkOutDataTable = new DataTable();

        /// <summary>
        /// Apex File Path DataTable
        /// </summary>
        private DataTable apexFileDataTable = new DataTable();

        /// <summary>
        /// Check In DataTable
        /// </summary>
        private DataTable checkInDataTable = new DataTable();

        

        #endregion

        /// <summary>
        /// Constructor for F3230
        /// </summary>
        public F3230()
        {
            this.InitializeComponent();
            this.InitializeBackgoundWorker();
            this.SnapshotPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SnapshotPictureBox.Height, this.SnapshotPictureBox.Width, "Snapshots", 28, 81, 128);
        }


        #region BackGroundWorker

        /// <summary>
        /// Handles the ProgressChanged event of the BackGroundWork control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the DoWork event of the bw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int processTime = 0;
                int startTime = 0;
                int endTime = 0;

                if (TerraScanCommon.IsDataBaseAvailable)
                {
                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("DropDB") });
                    startTime = Environment.TickCount;
                    ScriptEngine.DropDataBase();
                    endTime = Environment.TickCount;
                    processTime = (endTime - startTime) / 1000;
                    this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("RemovingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });
                }

                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("DownloadFile") });
                startTime = Environment.TickCount;
                ScriptEngine.DownloadDatabaseFile();
                endTime = Environment.TickCount;
                processTime = (endTime - startTime) / 1000;
                this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("DownloadingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });

                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("RestoreDB") });
                startTime = Environment.TickCount;
                ScriptEngine.CreateNewFieldDataBase();
                endTime = Environment.TickCount;
                processTime = (endTime - startTime) / 1000;
                this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("RestoreCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });

                this.GetServerData();

                this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("CheckoutCompleted") });
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateProgressBarStatus), new object[1] { false });
                WSHelper.IsOnLineMode = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this.ParentForm);
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("CheckoutFailed") });
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
                ScriptEngine.DropDataBase();
                string localFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\TerrascanFieldDB_Data.bak";
                if (File.Exists(localFilePath))
                {
                    File.Delete(localFilePath);
                }
            }
            finally
            {
                WSHelper.IsOnLineMode = false;
                this.Invoke(new LoadDataGrid(this.LoadGrid), new object[1] { false });
            }
        }

        /// <summary>
        /// UpdateButtonStatus
        /// </summary>
        /// <param name="status"></param>
        private void UpdateButtonStatus(bool status)
        {
            this.formProgress.EnableButton = status;
        }

        /// <summary>
        /// UpdateProgressBarStatus
        /// </summary>
        /// <param name="status"></param>
        private void UpdateProgressBarStatus(bool status)
        {
            this.formProgress.DisableProgressBar = status;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ////this.formProgress.Close();
                backGroundWork.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form3230Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form3230Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form3230Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.formLabelInfo[0] = "Field Checkout";
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form3230Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form3230Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form3230Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form3230Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form3230Control.WorkItem;
            this.footerSmartPart.FormId = "3230";
            this.footerSmartPart.AuditLinkText = string.Empty;
            this.footerSmartPart.VisibleHelpButton = false;

            this.footerSmartPart.VisibleHelpLinkButton = true;

            this.footerSmartPart.TabStop = true;
            foreach (UserControl ctrl in this.FooterWorkspace.SmartParts)
            {
                if (ctrl != null)
                {
                    ctrl.TabStop = true;
                }
            }
        }

        /// <summary>
        /// backGroundWorkCheckIn_RunWorkerCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backGroundWorkCheckIn_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                ////this.formProgress.Close();
                backGroundWorkCheckIn.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// backGroundWorkCheckIn_ProgressChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backGroundWorkCheckIn_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ////
        }

        /// <summary>
        /// backGroundWorkCheckIn_DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backGroundWorkCheckIn_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int processTime = 0;
                int startTime = 0;
                int endTime = 0;
                startTime = Environment.TickCount;
                this.CheckIn();
                WSHelper.IsOnLineMode = true;
                WSHelper.GetAuthenticationState();
                string localPath = string.Empty;
                localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string localAPexPath = localPath + @"\TerraScan Attachment";
                if (Directory.Exists(localAPexPath))
                {
                    this.EmptyFolder(new DirectoryInfo(localAPexPath));
                    foreach (DirectoryInfo dir in new DirectoryInfo(localAPexPath).GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    if (Directory.Exists(localAPexPath))
                        Directory.Delete(localAPexPath);
                }
                ScriptEngine.DropDataBase();
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("Check in completed") });
                endTime = Environment.TickCount;
                processTime = (endTime - startTime) / 1000;
                this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { "Check in completed in " + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateProgressBarStatus), new object[1] { false });
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this.ParentForm);
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { "Check in failed" });
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
            }
            finally
            {
                WSHelper.IsOnLineMode = false;
                this.Invoke(new LoadDataGrid(this.LoadGrid), new object[1] { false });
            }
        }

        #endregion

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Initializes the backgound worker.
        /// </summary>
        private void InitializeBackgoundWorker()
        {
            backGroundWork = new BackgroundWorker();
            backGroundWork.DoWork += new DoWorkEventHandler(this.BackGroundWorkDoWork);
            backGroundWork.ProgressChanged += new ProgressChangedEventHandler(this.BackGroundWorkProgressChanged);
            backGroundWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackGroundWorkRunWorkerCompleted);

            backGroundWorkCheckIn = new BackgroundWorker();
            backGroundWorkCheckIn.DoWork += new DoWorkEventHandler(backGroundWorkCheckIn_DoWork);
            backGroundWorkCheckIn.ProgressChanged += new ProgressChangedEventHandler(backGroundWorkCheckIn_ProgressChanged);
            backGroundWorkCheckIn.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backGroundWorkCheckIn_RunWorkerCompleted);

            backGroundWorkRemoveData = new BackgroundWorker();
            backGroundWorkRemoveData.DoWork += new DoWorkEventHandler(backGroundWorkRemoveData_DoWork);
            backGroundWorkRemoveData.ProgressChanged += new ProgressChangedEventHandler(backGroundWorkRemoveData_ProgressChanged);
            backGroundWorkRemoveData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backGroundWorkRemoveData_RunWorkerCompleted);

        }

        /// <summary>
        /// backGroundWorkRemoveData_RunWorkerCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backGroundWorkRemoveData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ///
        }

        /// <summary>
        /// backGroundWorkRemoveData_ProgressChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backGroundWorkRemoveData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ///
        }

        /// <summary>
        /// EmptyFolder
        /// </summary>
        /// <param name="directoryInfo"></param>
        private void EmptyFolder(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subfolder in directoryInfo.GetDirectories())
            {
                this.EmptyFolder(subfolder);
            }
        }

        /// <summary>
        /// backGroundWorkRemoveData_DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backGroundWorkRemoveData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (TerraScanCommon.IsDataBaseAvailable)
                {
                    if (!TerraScanCommon.IsApexAvail)
                    {

                        WSHelper.IsOnLineMode = false;
                        DataSet ds = this.form3230Control.WorkItem.ParcelIDs();
                        this.chkOutXML = string.Empty;
                        DataSet tempDataset = new DataSet("Root");
                        for (int index = 0; index < ds.Tables.Count; index++)
                        {
                            if (ds.Tables[index].Rows.Count > 0)
                            {
                                tempDataset.Tables.Add(ds.Tables[index].Copy());
                                this.chkOutXML += tempDataset.GetXml();
                                tempDataset.Clear();
                            }
                        }
                        WSHelper.IsOnLineMode = true;
                        this.form3230Control.WorkItem.LockParcelID(null, TerraScanCommon.UserId, TerraScanCommon.UserId, this.chkOutXML);
                        WSHelper.IsOnLineMode = false;
                        chkOutXML = string.Empty;
                        string centralApexPath = string.Empty;
                        string localPath = string.Empty;
                        CommentsData serverFilePathData = WSHelper.GetConfigDetails("AA_Sketch_ApexPath");
                        centralApexPath = serverFilePathData.GetCommentsConfigDetails[0][0].ToString();
                        localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        string localAPexPath = localPath + @"\TerraScan Attachment";
                        if (Directory.Exists(localAPexPath))
                        {
                            this.EmptyFolder(new DirectoryInfo(localAPexPath));
                            foreach (DirectoryInfo dir in new DirectoryInfo(localAPexPath).GetDirectories())
                            {
                                dir.Delete(true);
                            }
                            if (Directory.Exists(localAPexPath))
                                Directory.Delete(localAPexPath);
                        }
                        ScriptEngine.DropDataBase();
                    }
                    else
                    {
                        MessageBox.Show("Please close the Apex Application to remove data", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("Data removed successfully.") });
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateProgressBarStatus), new object[1] { false });
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.JustLog, this.ParentForm);
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { "Remove data failed." });
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
            }
            finally
            {
                WSHelper.IsOnLineMode = false;
                this.Invoke(new LoadDataGrid(this.LoadGrid), new object[1] { false });
            }
        }

        #region Delegate

        /// <summary>
        /// UpdateStatusHandler
        /// </summary>
        /// <param name="mymessage">string</param>
        public delegate void UpdateStatusHandler(string mymessage);

        /// <summary>
        /// UpdateButtonStatusHandler
        /// </summary>
        /// <param name="status"></param>
        public delegate void UpdateButtonStatusHandler(bool status);

        /// <summary>
        /// CheckinButton
        /// </summary>
        /// <param name="enable"></param>
        public delegate void CheckinButton(bool enable);

        /// <summary>
        /// CheckOutButton
        /// </summary>
        /// <param name="enable"></param>
        public delegate void CheckoutButton(bool enable);

        /// <summary>
        /// RemoveButton
        /// </summary>
        /// <param name="enable"></param>
        public delegate void RemoveButton(bool enable);

        /// <summary>
        /// LoadDataGrid
        /// </summary>
        public delegate void LoadDataGrid(bool load);

        #endregion

        #region Property
        /// <summary>
        /// Creates Property for F3230Controller
        /// </summary>

        [CreateNew]
        public F3230Controller F3230Control
        {
            get { return this.form3230Control as F3230Controller; }
            set { this.form3230Control = value; }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Audits the process.
        /// </summary>
        private void AuditProcess()
        {
            // DLL
            Server smoServer = new Server(TerraScanCommon.FieldServerName);
            smoServer.ConnectionContext.Connect();
            Database smoDataBase = new Database(smoServer, TerraScanCommon.FieldDataBaseName);
            smoDataBase = smoServer.Databases[TerraScanCommon.FieldDataBaseName];

            ////smoDataBase.ExecuteNonQuery("UPDATE tTS_FormSlice SET tTS_FormSandwich.SubTitle1= REPLACE(tTS_FormSandwich.SubTitle1,'''',''),tTS_FormSandwich.SubTitle2= REPLACE(tTS_FormSandwich.SubTitle2,'''','')", ExecutionTypes.ContinueOnError);
            smoDataBase.ExecuteNonQuery("CREATE ASSEMBLY T2FieldAuditTrigger FROM  '" + System.Environment.CurrentDirectory + "\\T2FieldAuditTrigger.dll'" + "  WITH PERMISSION_SET = SAFE ", ExecutionTypes.ContinueOnError);
            smoDataBase.ExecuteNonQuery("EXEC f9060_pcins_FieldAuditingColumns", ExecutionTypes.ContinueOnError);
            smoDataBase.ExecuteNonQuery("sp_configure 'clr enabled', 1", ExecutionTypes.ContinueOnError);
            smoDataBase.ExecuteNonQuery("reconfigure", ExecutionTypes.ContinueOnError);
            ////MessageBox.Show("CheckOut process completed.", "TerraScan - CheckOut", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// Inserts the server data.
        /// </summary>
        private void InsertServerData()
        {
            this.chkOutXML = string.Empty;
            TerraScanCommon.CheckOutStatus = true;
            WSHelper.F9065_UpdateApplicationStatus(TerraScanCommon.CheckOutStatus, true, TerraScanCommon.UserId);
            TerraScanCommon.CheckOutStatus = true;
            WSHelper.IsOnLineMode = true;
            this.form3230Control.WorkItem.InsertFieldElement(this.GetXmlString(), TerraScanCommon.UserId);
        }

        /// <summary>
        /// Checks the out process.
        /// </summary>
        private void GetServerData()
        {
            int processTime = 0;
            int startTime = 0;
            int endTime = 0;

            this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingDB") });
            startTime = Environment.TickCount;

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutConfigXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutFormXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutMiscXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ParcelHeaderChkOutXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutSitusXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutCommonXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutSeniorExemptionXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutAssessmentTypeXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutParcelValueXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutType2XML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutDeprMiscXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutDeprXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutVSTGCitemXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutMSCEstimateXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutEstimateResultXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutMSCEstimateOccupancyXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.ChkOutEstimateBuildingXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.GetTerraGonXML()
                , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.GetSaleXML()
               , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.GetStatementXML()
               , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.GetReceiptXML()
               , string.Empty, TerraScanCommon.UserId, true);

            this.form3230Control.WorkItem.InsertCheckOutXml(this.GetCorrectionXML()
               , string.Empty, TerraScanCommon.UserId, true);

            this.GetLandValueXML();

            this.GetEventXML();

            this.GetParcelXML();

            this.GetOwnerXML();

            this.GetNBHDXML();

            this.GetDistrictXML();

            this.GetEstimateComponentXML();

            this.GetCommentXML();

            this.GetChkOutFileXML();

            this.GetLegalXML();

            this.GetMisc_CatalogXML();

            this.GetMiscTableXML();

            this.GetMOwnerXML();

            this.GetObjectXML();

            this.GetValueSliceXML();

            this.GetLandXML();

            this.GetchkOutApexPath();

            this.form3230Control.WorkItem.SaveCheckOutDetails(ProcessCheckOutDetails());

            this.form3230Control.WorkItem.SaveChkOutParcelIDs(ProcessChkOutParcelIDs());

            endTime = Environment.TickCount;
            processTime = (endTime - startTime) / 1000;
            this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });
            //Move the apex File to Local Path

            string ApexPath = string.Empty;
            string ApexPathPermission = string.Empty;
            if (this.apexfileDataSet.ApexFilePath != null)
            {
                if (this.apexfileDataSet.ApexFilePath.Rows.Count > 0)
                {
                    for (int i = 0; i < this.apexfileDataSet.ApexFilePath.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.apexfileDataSet.ApexFilePath.Rows[i][0].ToString()))
                        {
                            string Apexpath = string.Empty;
                            string Apexpath1 = string.Empty;
                            string Apexpath2 = string.Empty;
                            string tempath = string.Empty;
                            //string path = string.Empty;
                            Apexpath1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            Apexpath = this.apexfileDataSet.ApexFilePath.Rows[i][0].ToString();
                            Apexpath2 = Path.GetPathRoot(this.apexfileDataSet.ApexFilePath.Rows[i][0].ToString());
                            tempath = Apexpath.Remove(0, Apexpath2.Length);
                            string Apexpath3 = Apexpath1 + @"\TerraScan Attachment\Apex Path" + tempath;
                            if (System.IO.File.Exists(this.apexfileDataSet.ApexFilePath.Rows[i][0].ToString()))
                            {
                                try
                                {
                                    FileStream fs = new FileStream(this.apexfileDataSet.ApexFilePath.Rows[i][0].ToString(), FileMode.Open);
                                    BinaryReader bR = new BinaryReader(fs);
                                    UpLoadImage(bR.ReadBytes((int)fs.Length), Apexpath3);
                                    bR.Close();
                                    fs.Close();
                                }
                                catch (IOException)
                                {
                                    ErrorEngine.ShowForm(3);
                                }
                            }


                        }


                    }
                }
            }
            //Move the Path to Local Folder Location Of Attachment File.
            string FileIDPath = string.Empty;
            string FilePathPermission = string.Empty;
            if (checkOutDataTable != null)
            {
                if (checkOutDataTable.Rows.Count > 0)
                {
                    DataTable missAttachTable = new DataTable();
                    DataTable missSketchTable = new DataTable();
                    missAttachTable = checkOutDataTable.Clone();
                    missSketchTable = checkOutDataTable.Clone();
                    for (int i = 0; i < checkOutDataTable.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(checkOutDataTable.Rows[i]["AURL"].ToString()))
                        {
                            string path = string.Empty;
                            string path1 = string.Empty;
                            string path2 = string.Empty;
                            string tempath = string.Empty;
                            //string path = string.Empty;
                            path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            path = checkOutDataTable.Rows[i]["AURL"].ToString();
                            path2 = Path.GetPathRoot(checkOutDataTable.Rows[i]["AURL"].ToString());
                            tempath = path.Remove(0, path2.Length);
                            string path3 = path1 + @"\TerraScan Attachment" + tempath;
                            if (System.IO.File.Exists(path))
                            {
                                try
                                {
                                    FileStream fs = new FileStream(path, FileMode.Open);
                                    BinaryReader bR = new BinaryReader(fs);
                                    UpLoadImage(bR.ReadBytes((int)fs.Length), path3);
                                    bR.Close();
                                    fs.Close();
                                }
                                catch (IOException)
                                {
                                    ErrorEngine.ShowForm(3);
                                }
                            }
                              else
                            {
                                this.fileMissing = true;
                                missAttachTable.ImportRow(checkOutDataTable.Rows[i]);    
                                missAttachTable.AcceptChanges(); 
                                //this.ListBoxForm.AttachedFiles = checkOutDataTable.Rows[i]["FileID"].ToString();
                                //int.TryParse(checkOutDataTable.Rows[i]["FileID"].ToString(), out FileMissing);
                                //this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] {FileMissing});
                                //if (string.IsNullOrEmpty(FilePathPermission))
                                //{
                                //    FilePathPermission = checkOutDataTable.Rows[i]["FileID"].ToString();

                                //}
                                //else
                                //{
                                //    FilePathPermission = FilePathPermission + ", " + checkOutDataTable.Rows[i]["FileID"].ToString();
                                //}
                            }
                            
                        }
                        else
                        {
                           
                            this.fileMissing = true;
                            missSketchTable.ImportRow(checkOutDataTable.Rows[i]);
                            missSketchTable.AcceptChanges(); 
                            //this.ListBoxForm.SketchFiles = checkOutDataTable.Rows[i]["FileID"].ToString();
                            //int.TryParse(checkOutDataTable.Rows[i]["FileID"].ToString(), out sketchMissing);
                            //this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] {sketchMissing});
                            // MessageBox.Show("File does not exist or path was not Found");
                            //if (string.IsNullOrEmpty(FileIDPath))
                            //{
                            //    FileIDPath = checkOutDataTable.Rows[i]["FileID"].ToString();
                            //}
                            //else
                            //{
                            //    FileIDPath = FileIDPath + ", " + checkOutDataTable.Rows[i]["FileID"].ToString();
                            //}
                        }


                       
                    }
                  
                    if (this.fileMissing)
                    {
                        this.fileMissing = false;
                        this.listBoxForm = new ListBoxForm(true);
                        if (missAttachTable != null)
                        {
                            if (missAttachTable.Rows.Count > 0)
                            {
                                this.listBoxForm.MissedAttachTable = missAttachTable;
                            }
                        }
                        if (missSketchTable != null)
                        {
                            if (missSketchTable.Rows.Count > 0)
                            {
                                this.listBoxForm.MissedSketchTable = missSketchTable;
                            }
                        }
                        this.listBoxForm.ShowDialog();

                    }

                    
                    //if (!string.IsNullOrEmpty(FilePathPermission))
                    ////{
                    //    MessageBox.Show(" List Of File Id does not have permissions : " + FilePathPermission);
                    //}
                    //if (!string.IsNullOrEmpty(FileIDPath))
                    //{
                    //    MessageBox.Show(" List Of File Id belongs to Sketch Page does not contain AURL Path : " + FileIDPath);
                    //}

                }
            }
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.form3230Control.WorkItem.LockParcelID(this.selectedSnapShotId, TerraScanCommon.UserId, TerraScanCommon.UserId, null);
        }

        #region ChkOut

        /// <summary>
        /// Used To upload the image to central location
        /// </summary>
        /// <param name="data"> The data to be uploaded.</param>
        /// <param name="strFileName"> The path of the file name.</param>
        private static void UpLoadImage(byte[] data, string strFileName)
        {
            string uploadFilePath = strFileName;
            if (!System.IO.Directory.Exists(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\"))))
            {
                // Create the directory as per the file path.
                System.IO.Directory.CreateDirectory(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\")));
            }

            // Used to paste the file in the specified directory.
            FileStream fileStream = new FileStream(uploadFilePath, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(data);
            binaryWriter.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Used To upload the image to central location
        /// </summary>
        /// <param name="data"> The data to be uploaded.</param>
        /// <param name="strFileName"> The path of the file name.</param>
        private static void UpLoadCheckInImage(byte[] data, string strFileName)
        {
            string uploadFilePath = strFileName;
            if (!System.IO.Directory.Exists(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\"))))
            {
                // Create the directory as per the file path.
                System.IO.Directory.CreateDirectory(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\")));
            }
            else
            {
                if (File.Exists(uploadFilePath))
                    System.IO.Directory.Delete(uploadFilePath);
                System.IO.Directory.CreateDirectory(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\")));
            }

            // Used to paste the file in the specified directory.
            FileStream fileStream = new FileStream(uploadFilePath, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(data);
            binaryWriter.Close();
            fileStream.Close();
        }

        private void GetLandValueXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutLandValuesXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// Get EventXML
        /// </summary>
        private void GetEventXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutEventXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        private void GetParcelXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutParcelXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetOwnerXML
        /// </summary>
        private void GetOwnerXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutOwnerXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetNBHDXML
        /// </summary>
        private void GetNBHDXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutNBHDXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetDistrictXML
        /// </summary>
        private void GetDistrictXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutDistrictXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetTerraGonXML
        /// </summary>
        private string GetTerraGonXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutTerraGonXML(this.selectedSnapShotId);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// GetEstimateComponentXML
        /// </summary>
        private void GetEstimateComponentXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutEstimateComponentXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetCommentXML
        /// </summary>
        private void GetCommentXML()
        {

            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutCommentXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }


        /// <summary>
        /// GetVSTGComponentXML
        /// </summary>
        private void GetVSTGComponentXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutVSTGComponentXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetChkOutFileXML
        /// </summary>
        private void GetChkOutFileXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutFileXML(nRowCount, out nRowCount);
                //Move the Content to Create DataTable
                DataSet ds = new DataSet();
                checkOutDataTable.Rows.Clear();
                ds.ReadXml(new StringReader(chkOutXML));
                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                        checkOutDataTable = ds.Tables[1];
                }
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// Check Out Apex Path
        /// </summary>
        private void GetchkOutApexPath()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            WSHelper.IsOnLineMode = true;
            this.apexfileDataSet = this.form3230Control.WorkItem.F3230GetApexFilePath(this.selectedSnapShotId);


        }

        /// <summary>
        /// GetVSTGGonBldgXML
        /// </summary>
        private void GetVSTGGonBldgXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutVSTGGonBldgXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetLegalXML
        /// </summary>
        private void GetLegalXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutLegalXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetMiscTableXML
        /// </summary>
        private void GetMiscTableXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutMiscTableXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// GetMisc_CatalogXML
        /// 
        /// </summary>
        private void GetMisc_CatalogXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutMisc_CatalogXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }


        private void GetMOwnerXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutMOwnerXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }


        private void GetObjectXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutObjectXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }


        private void GetValueSliceXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutValueSliceXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }


        private void GetLandXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutLandXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }


        private void GetVersionXML()
        {
            chkOutXML = string.Empty;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            int nRowCount = -1;
            bool isFirstTime = true;
            while (nRowCount != 0)
            {
                WSHelper.IsOnLineMode = true;
                chkOutXML = this.ProcessChkOutVersionXML(nRowCount, out nRowCount);
                WSHelper.IsOnLineMode = false;
                if (isFirstTime)
                {
                    isFirstTime = false;
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                }
                else
                    this.form3230Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                chkOutXML = String.Empty;
            }
        }

        /// <summary>
        /// ChkOutConfigXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutConfigXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutConfigXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }


        /// <summary>
        /// ParcelHeaderChkOutXML
        /// </summary>
        /// <returns></returns>
        private string ParcelHeaderChkOutXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldUseData = this.form3230Control.WorkItem.ParcelHeaderChkOutXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldUseData.Tables.Count; index++)
            {
                if (fieldUseData.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldUseData.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutFormXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutFormXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutFormXML(this.selectedSnapShotId, this.GetXmlString(), 1);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutStatementXML
        /// </summary>
        /// <returns></returns>
        private string GetStatementXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutStatementXML(this.selectedSnapShotId);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutReceiptXML
        /// </summary>
        /// <returns></returns>
        private string GetReceiptXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutReceiptXML(this.selectedSnapShotId);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutSaleXML
        /// </summary>
        /// <returns></returns>
        private string GetSaleXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutSaleXML(this.selectedSnapShotId);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutCorrectionXML
        /// </summary>
        /// <returns></returns>
        private string GetCorrectionXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutCorrectionXML(this.selectedSnapShotId);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// Gets the XML string.
        /// </summary>
        /// <returns>string</returns>
        private string GetXmlString()
        {
            DataTable xmlDataTable = new DataTable();
            DataColumn[] tempColumn = new DataColumn[] { new DataColumn("SnapshotID"), new DataColumn("Attachemnts"), new DataColumn("Comments"), new DataColumn("PrimaryOnly"), new DataColumn("Sketches"), new DataColumn("ResetField1"), new DataColumn("ResetField2"), new DataColumn("ResetField3"), new DataColumn("ResetField4"), new DataColumn("ResetField5"), new DataColumn("LockAppraisalBy"), new DataColumn("LockValueBy"), new DataColumn("LockAdminBy") };
            xmlDataTable.Columns.AddRange(tempColumn);

            DataRow tempRow = xmlDataTable.NewRow();
            tempRow["SnapshotID"] = this.selectedSnapShotId.ToString();
            tempRow["ResetField5"] = "fasle";

            if (this.apprasialLock)
            {
                tempRow["LockAppraisalBy"] = TerraScanCommon.UserId.ToString();
            }
            else
            {
                tempRow["LockAppraisalBy"] = TerraScanCommon.UserId.ToString();
            }

            if (this.valueLock)
            {
                tempRow["LockValueBy"] = TerraScanCommon.UserId.ToString();
            }
            else
            {
                tempRow["LockValueBy"] = TerraScanCommon.UserId.ToString();
            }

            if (this.adminLock)
            {
                tempRow["LockAdminBy"] = TerraScanCommon.UserId.ToString();
            }
            else
            {
                tempRow["LockAdminBy"] = TerraScanCommon.UserId.ToString();
            }

            xmlDataTable.Rows.Add(tempRow);
            return TerraScanCommon.GetXmlString(xmlDataTable);
        }

        /// <summary>
        /// ChkOutMiscXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutMiscXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutMiscXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutEventXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutEventXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutEventXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tGD_Event"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutParcelXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutParcelXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutParcelXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_Parcel"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutOwnerXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutOwnerXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutOwnerXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tTS_Owner"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutDistrictXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutDistrictXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutDistrictXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tTS_District"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutEstimateComponentXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutEstimateComponentXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutEstimateComponentXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "EstimateComponent"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutCommentXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutCommentXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutCommentXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tTS_Comment"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutVSTGComponentXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutVSTGComponentXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutVSTGComponentXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_VSTG_Component"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutFileXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutFileXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutFileXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tTS_File"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutVSTGGonBldgXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutVSTGGonBldgXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutVSTGGonBldgXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_VSTG_GonBldg"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutNBHDXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutNBHDXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutNBHDXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_NBHD"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutLegalXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutLegalXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutLegalXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tTS_Legal"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutMiscTableXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutMiscTableXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutMiscTableXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_Misc"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutMisc_CatalogXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutMisc_CatalogXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutMisc_CatalogXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_Misc_Catalog"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutMOwnerXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutMOwnerXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutMOwnerXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_MOwner"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutObjectXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutObjectXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutObjectXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_Object"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutValueSliceXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutValueSliceXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutValueSliceXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_ValueSlice"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutLandXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutLandXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutLandXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_Land"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ProcessCheckOutDetails
        /// </summary>
        /// <returns></returns>
        private string ProcessCheckOutDetails()
        {
            WSHelper.IsOnLineMode = false;
            chkOutXML = string.Empty;
            F3230CheckInData fieldDataSet = new F3230CheckInData();
            fieldDataSet = this.form3230Control.WorkItem.GetCheckOutDetails(this.selectedSnapShotId
                , TerraScanCommon.UserId);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("&lt;", "<").Replace("&gt;", ">");
            chkOutXML = chkOutXML.Replace("<Column1>", "").Replace("</Column1>", "").Replace("<Table>", "").Replace("</Table>", ""); ;
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";

            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutParcelIDs
        /// </summary>
        /// <returns></returns>
        private string ProcessChkOutParcelIDs()
        {
            WSHelper.IsOnLineMode = true;
            chkOutXML = string.Empty;
            F3230CheckInData fieldDataSet = new F3230CheckInData();
            fieldDataSet = this.form3230Control.WorkItem.GetChkOutParcelIDs(this.selectedSnapShotId);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("&lt;", "<").Replace("&gt;", ">");
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ProcessChkOutVersionXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkOutVersionXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutVersionXML(this.selectedSnapShotId
                , this.GetXmlString()
                , "tAA_MSC_Version"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutSitusXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutSitusXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutSitusXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutSeniorExemptionXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutSeniorExemptionXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutSeniorExemptionXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutAssessmentTypeXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutAssessmentTypeXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutAssessmentTypeXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutParcelValueXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutParcelValueXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutParcelValueXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutType2XML
        /// </summary>
        /// <returns></returns>
        private string ChkOutType2XML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutType2XML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutDeprMisc
        /// </summary>
        /// <returns></returns>
        private string ChkOutDeprMiscXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutDeprMiscXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutDeprXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutDeprXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutDeprXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }


        /// <summary>
        /// ChkOutEstimateCompXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutEstimateCompXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutEstimateCompXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutVSTGCitemXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutVSTGCitemXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutVSTGCitemXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutMSCEstimateXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutMSCEstimateXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutMSCEstimateXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutEstimateResultXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutEstimateResultXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutEstimateResultXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutMSCEstimateOccupancyXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutMSCEstimateOccupancyXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutMSCEstimateOccupancyXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutEstimateBuildingXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutEstimateBuildingXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutEstimateBuildingXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }

        /// <summary>
        /// ChkOutCommonXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutCommonXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            fieldDataSet = this.form3230Control.WorkItem.ChkOutCommonXML(this.selectedSnapShotId, this.GetXmlString());
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < fieldDataSet.Tables.Count; index++)
            {
                if (fieldDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(fieldDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = false;
            return chkOutXML;
        }


        #endregion ChkOut

        #region Check-In

        /// <summary>
        /// CheckIn
        /// </summary>
        private void CheckIn()
        {
            if (ScriptEngine.IsServerAvailable())
            {
                if (TerraScanCommon.IsDataBaseAvailable)
                {
                    WSHelper.IsOnLineMode = false;

                    this.form3230Control.WorkItem.InsertCheckInXml(this.ChkInTypesXML(), string.Empty, TerraScanCommon.UserId);
                    this.form3230Control.WorkItem.InsertCheckInXml(this.SaveChkInDeprXML(), string.Empty, TerraScanCommon.UserId);
                    this.form3230Control.WorkItem.InsertCheckInXml(this.ChkInEstimateComponentGroupXML(), string.Empty, TerraScanCommon.UserId);
                    this.form3230Control.WorkItem.InsertCheckInXml(this.ChkInNBHDXML(), string.Empty, TerraScanCommon.UserId);
                    this.form3230Control.WorkItem.InsertCheckInXml(this.ChkInLandCodeXML(), string.Empty, TerraScanCommon.UserId);
                    this.SaveCommentXML();
                    this.SaveEstimateXML();
                    this.SaveFileXML();
                    this.SaveLandValuesXML();
                    this.SaveLandXML();
                    this.SaveMiscXML();
                    this.SaveMSC_EstimateXML();
                    this.SaveObjectXML();
                    this.SaveParcelValueXML();
                    this.SaveParcelXML();
                    this.SaveTerraGonXML();
                    this.SaveType2XML();
                    this.SaveType6XML();
                    this.SaveValueSliceXML();
                    string ChkInInsertXML = string.Empty;
                    WSHelper.IsOnLineMode = false;
                    this.form3230Control.WorkItem.ChkInInsertXML(out ChkInInsertXML);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertAddedRecordXML(ChkInInsertXML
                        , string.Empty, TerraScanCommon.UserId);
                    WSHelper.IsOnLineMode = false;
                    this.form3230Control.WorkItem.ChkInTerraGonInsertXML(out ChkInInsertXML);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertAddedRecordXML(ChkInInsertXML
                                            , string.Empty, TerraScanCommon.UserId);

                    this.ChkInInsertedFile();

                    WSHelper.IsOnLineMode = false;
                    DataSet ds = this.form3230Control.WorkItem.ParcelIDs();
                    this.chkOutXML = string.Empty;
                    DataSet tempDataset = new DataSet("Root");
                    for (int index = 0; index < ds.Tables.Count; index++)
                    {
                        if (ds.Tables[index].Rows.Count > 0)
                        {
                            tempDataset.Tables.Add(ds.Tables[index].Copy());
                            this.chkOutXML += tempDataset.GetXml();
                            tempDataset.Clear();
                        }
                    }
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.LockParcelID(null, 0, TerraScanCommon.UserId, this.chkOutXML);
                    string centralApexPath = string.Empty;
                    string localPath = string.Empty;
                    CommentsData serverFilePathData = WSHelper.GetConfigDetails("AA_Sketch_ApexPath");
                    centralApexPath = serverFilePathData.GetCommentsConfigDetails[0][0].ToString();
                    localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string localAPexPath = localPath + @"\TerraScan Attachment\Apex Path";
                    // Get Files & Copy 
                    if (Directory.Exists(localAPexPath))
                    {
                        string[] files = Directory.GetFiles(localAPexPath);
                        foreach (string file in files)
                        {
                            string name = Path.GetFileName(file);
                            string dest = Path.Combine(centralApexPath, name);
                            if (File.Exists(dest))
                                File.Delete(dest);
                            File.Copy(file, dest);
                        }
                    }
                    //// Move newly inserted attachment files from field mode to online.
                    string FileIDPathCheckIn = string.Empty;
                    string FilePathPermissionCheckIn = string.Empty;
                    if (this.fieldCheckInDataSet != null)
                    {
                        if (this.fieldCheckInDataSet.tTS_File.Rows.Count > 0)
                        {
                            for (int i = 0; i < this.fieldCheckInDataSet.tTS_File.Rows.Count; i++)
                            {

                                if (!string.IsNullOrEmpty(this.fieldCheckInDataSet.tTS_File.Rows[i]["AURL"].ToString()))
                                {
                                    string loacalEnvPath = string.Empty;
                                    string fileNonRootPath = string.Empty;
                                    string tempath = string.Empty;
                                    string sourceFilePath = string.Empty;
                                    string destinationPath = string.Empty;
                                    string fileExtention = string.Empty;
                                    string formNumber = string.Empty;
                                    string fileID = string.Empty;
                                    string split1 = string.Empty;
                                    string split2 = string.Empty;
                                    string fileName = string.Empty;

                                    string path1 = string.Empty;
                                    string path2 = string.Empty;
                                    string tempath1 = string.Empty;
                                    string path = string.Empty;
                                    path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                                    path = this.beforeCheckinDataSet.tTS_File.Rows[i]["AURL"].ToString();
                                    path2 = Path.GetPathRoot(this.fieldCheckInDataSet.tTS_File.Rows[i]["AURL"].ToString());
                                    tempath1 = path.Remove(0, path2.Length);
                                    string path3 = path1 + @"\TerraScan Attachment" + tempath1;

                                    formNumber = this.fieldCheckInDataSet.tTS_File.Rows[i]["Form"].ToString();
                                    fileID = this.fieldCheckInDataSet.tTS_File.Rows[i]["FileID"].ToString();
                                    split2 = fileID.Remove(0, fileID.Length - 3);
                                    split1 = fileID.Remove(fileID.Length - 3);
                                    string[] fileExtentionList = this.beforeCheckinDataSet.tTS_File.Rows[i]["AURL"].ToString().Split('.');
                                    fileExtention = "." + fileExtentionList[fileExtentionList.Length - 1];
                                    loacalEnvPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                                    fileNonRootPath = Path.GetPathRoot(this.fieldCheckInDataSet.tTS_File.Rows[i]["AURL"].ToString());
                                    destinationPath = fileNonRootPath + "\\" + formNumber + "\\" + split1 + "\\" + split2 + "\\" + fileID + fileExtention;
                                    tempath = destinationPath.Remove(0, fileNonRootPath.Length);

                                    sourceFilePath = path3;
                                    this.fieldCheckInDataSet.tTS_File.Rows[i]["AURL"] = destinationPath;
                                    if (System.IO.File.Exists(sourceFilePath))
                                    {
                                        try
                                        {
                                            FileStream fs = new FileStream(sourceFilePath, FileMode.Open);
                                            BinaryReader bR = new BinaryReader(fs);
                                            //// Upload the Image to the Central Location.
                                            UpLoadCheckInImage(bR.ReadBytes((int)fs.Length), destinationPath);
                                            bR.Close();
                                            fs.Close();
                                        }
                                        catch (IOException)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }

                    DataSet tempFileDataSet = new DataSet("Root");
                    string root = "<Root>";
                    this.chkOutXML = string.Empty;
                    for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
                    {
                        if (this.fieldCheckInDataSet.Tables["tTS_File"].Rows.Count > 0)
                        {
                            tempFileDataSet.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                            this.chkOutXML += tempFileDataSet.GetXml();
                            tempFileDataSet.Clear();
                        }
                    }
                    this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
                    this.chkOutXML = root + this.chkOutXML + "</Root>";
                    this.form3230Control.WorkItem.UpdateFileXML(this.chkOutXML);
                    this.chkOutXML = string.Empty;

                    //// Moving field modified attachment files
                    FileIDPathCheckIn = string.Empty;
                    FilePathPermissionCheckIn = string.Empty;
                    if (checkInDataTable != null)
                    {
                        if (checkInDataTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < checkInDataTable.Rows.Count; i++)
                            {

                                if (!string.IsNullOrEmpty(checkInDataTable.Rows[i]["AURL"].ToString()))
                                {
                                    string path1 = string.Empty;
                                    string path2 = string.Empty;
                                    string tempath = string.Empty;
                                    string path = string.Empty;
                                    path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                                    path = checkInDataTable.Rows[i]["AURL"].ToString();
                                    path2 = Path.GetPathRoot(checkInDataTable.Rows[i]["AURL"].ToString());
                                    tempath = path.Remove(0, path2.Length);
                                    string path3 = path1 + @"\TerraScan Attachment" + tempath;
                                    if (System.IO.File.Exists(path3))
                                    {
                                        try
                                        {
                                            FileStream fs = new FileStream(path3, FileMode.Open);
                                            BinaryReader bR = new BinaryReader(fs);
                                            //// Upload the Image to the Central Location.
                                            UpLoadCheckInImage(bR.ReadBytes((int)fs.Length), path);
                                            bR.Close();
                                            fs.Close();
                                        }
                                        catch (IOException)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                    WSHelper.IsOnLineMode = true;
                }
                else
                {
                    MessageBox.Show("Field Database not available.\nPlease do the checkout process", "TerraScan T2 - CheckIn", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("SqlNotAvailable"), "TerraScan T2 - CheckIn", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// SaveType2XML
        /// </summary>
        private void SaveType2XML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInType2XML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveType6XML
        /// </summary>
        private void SaveType6XML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInType6XML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveValueSliceXML
        /// </summary>
        private void SaveValueSliceXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInValueSliceXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveParcelValueXML
        /// </summary>
        private void SaveParcelValueXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInParcelValueXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveParcelXML
        /// </summary>
        private void SaveParcelXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInParcelXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveTerraGonXML
        /// </summary>
        private void SaveTerraGonXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInTerraGonXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveObjectXML
        /// </summary>
        private void SaveObjectXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInObjectXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveMSC_EstimateXML
        /// </summary>
        private void SaveMSC_EstimateXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInMSC_EstimateXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveMiscXML
        /// </summary>
        private void SaveMiscXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInMiscXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveLandXML
        /// </summary>
        private void SaveLandXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInLandXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveLandValuesXML
        /// </summary>
        private void SaveLandValuesXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInLandValuesXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveFileXML
        /// </summary>
        private void SaveFileXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInFileXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;

                    //Move the Content to Create DataTable
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(chkOutXML));
                    checkInDataTable = ds.Tables["tTS_File"];

                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveEstimateXML
        /// </summary>
        private void SaveEstimateXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInEstimateXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// SaveCommentXML
        /// </summary>
        private void SaveCommentXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = false;
                    chkOutXML = this.ProcessChkInCommentXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = true;
                    this.form3230Control.WorkItem.InsertCheckInXml(chkOutXML, String.Empty, TerraScanCommon.UserId);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region Processing XML

        /// <summary>
        /// ProcessChkInCommentXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkInCommentXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInCommentXML("tTS_Comment"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }

        /// <summary>
        /// Processes the CHK out land values XML.
        /// </summary>
        /// <param name="nRowcount">The n rowcount.</param>
        /// <param name="RowCount">The row count.</param>
        /// <returns></returns>
        private string ProcessChkOutLandValuesXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form3230Control.WorkItem.ChkOutLandValuesXML(this.selectedSnapShotId
                 , this.GetXmlString()
                 , "tAA_LandValues"
                 , nRowcount
                 , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
                    chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            chkOutXML = root + chkOutXML + "</Root>";
            return chkOutXML;
        }



        /// <summary>
        /// ProcessChkInEstimateXML
        /// </summary>
        /// <param name="nRowcount"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private string ProcessChkInEstimateXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInEstimateXML("Estimate"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInFileXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInFileXML("tTS_File"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInLandValuesXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInLandValuesXML("tAA_LandValues"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInLandXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInLandXML("tAA_Land"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInMiscXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInMiscXML("tAA_Misc"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }

        private void ChkInInsertedFile()
        {
            this.chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = false;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInInsertedFileXML();
            this.beforeCheckinDataSet = this.fieldCheckInDataSet;
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = true;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.InsertFile(this.chkOutXML);
            WSHelper.IsOnLineMode = false;
        }

        private string ProcessChkInMSC_EstimateXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInMSC_EstimateXML("tAA_MSC_Estimate"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInObjectXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInObjectXML("tAA_Object"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInParcelValueXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInParcelValueXML("tAA_ParcelValue"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInParcelXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInParcelXML("tAA_Parcel"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInTerraGonXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInTerraGonXML("tAA_VSTerraGon"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInType2XML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInType2XML("tAA_MA_Type2"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInType6XML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInType6XML("tAA_MA_Type6"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }


        private string ProcessChkInValueSliceXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInValueSliceXML("tAA_ValueSlice"
                , nRowcount
                , out RowCount);
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            return this.chkOutXML;
        }




        private string ChkInTypesXML()
        {

            this.chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = false;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInTypesXML();
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = true;
            return this.chkOutXML;
        }

        private string SaveChkInDeprXML()
        {

            this.chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = false;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInDeprXML();
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = true;
            return this.chkOutXML;
        }



        private string ChkInEstimateComponentGroupXML()
        {
            this.chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = false;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInEstimateComponentGroupXML();
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = true;
            return this.chkOutXML;
        }


        private string ChkInNBHDXML()
        {
            this.chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = false;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInNBHDXML();
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = true;
            return this.chkOutXML;
        }

        #endregion


        private string ChkInLandCodeXML()
        {
            this.chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = false;
            this.fieldCheckInDataSet = this.form3230Control.WorkItem.ChkInLandCodeXML();
            DataSet tempDataset = new DataSet("Root");
            string root = "<Root>";
            for (int index = 0; index < this.fieldCheckInDataSet.Tables.Count; index++)
            {
                if (this.fieldCheckInDataSet.Tables[index].Rows.Count > 0)
                {
                    tempDataset.Tables.Add(this.fieldCheckInDataSet.Tables[index].Copy());
                    this.chkOutXML += tempDataset.GetXml();
                    tempDataset.Clear();
                }
            }
            this.chkOutXML = this.chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
            this.chkOutXML = root + this.chkOutXML + "</Root>";
            WSHelper.IsOnLineMode = true;
            return this.chkOutXML;
        }

        #endregion Check-In

        private void CustomizeGrid()
        {

            // Assigning the column width
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNameColumn.ColumnName].Width = 215;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Width = 220;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.CreatedByColumn.ColumnName].Width = 100;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.InsertedDateColumn.ColumnName].Width = 100;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.RecordCountColumn.ColumnName].Width = 101;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotIDColumn.ColumnName].Width = 0;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.FormColumn.ColumnName].Width = 0;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.InsertedByColumn.ColumnName].Width = 0;

            // Assigning the header caption
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNameColumn.ColumnName].Header.Caption = "Snapshot Name";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Header.Caption = "Description";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.CreatedByColumn.ColumnName].Header.Caption = "Created By";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.InsertedDateColumn.ColumnName].Header.Caption = "Date";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.RecordCountColumn.ColumnName].Header.Caption = "Count";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotIDColumn.ColumnName].Header.Caption = "SnapshotID";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.FormColumn.ColumnName].Header.Caption = "Form";

            // Assigning the VisiblePosition
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNameColumn.ColumnName].Header.VisiblePosition = 0;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Header.VisiblePosition = 1;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.CreatedByColumn.ColumnName].Header.VisiblePosition = 2;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.InsertedDateColumn.ColumnName].Header.VisiblePosition = 3;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.RecordCountColumn.ColumnName].Header.VisiblePosition = 4;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotIDColumn.ColumnName].Header.VisiblePosition = 5;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.FormColumn.ColumnName].Header.VisiblePosition = 6;

            // Makeing the column readonly
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNameColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNoteColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.CreatedByColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.InsertedDateColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.RecordCountColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotIDColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.FormColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNameColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.CreatedByColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.InsertedDateColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.RecordCountColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotIDColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.FormColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNameColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotNoteColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.CreatedByColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.InsertedDateColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.RecordCountColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.SnapshotIDColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSet.ListSnapshotTable.FormColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        }

        private bool CheckScriptFileVersion()
        {
            string localVersion = string.Empty;
            string serverVersion = string.Empty;

            WSHelper.IsOnLineMode = false;
            this.checkDataSet = WSHelper.GetConfigInformation();

            if (!string.IsNullOrEmpty(this.checkDataSet.Tables[0].Rows[0]["Version"].ToString()))
            {
                localVersion = this.checkDataSet.Tables[0].Rows[0]["Version"].ToString();
            }

            WSHelper.IsOnLineMode = true;
            this.checkDataSet = WSHelper.GetConfigInformation();

            if (!string.IsNullOrEmpty(this.checkDataSet.Tables[0].Rows[0]["Version"].ToString()))
            {
                serverVersion = this.checkDataSet.Tables[0].Rows[0]["Version"].ToString();
            }

            if (serverVersion == localVersion)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the server data progress.
        /// </summary>
        private void GetServerDataProgress()
        {
            backGroundWork.RunWorkerAsync();
            bool configValue = false;
            this.fieldUseDataSet = this.form3230Control.WorkItem.GetcfgConfiguration("FieldUseDetails");

            if (this.fieldUseDataSet.Tables.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.fieldUseDataSet.ListCfgConfigTable.Rows[0][this.fieldUseDataSet.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString()))
                {
                    Boolean.TryParse(this.fieldUseDataSet.ListCfgConfigTable.Rows[0][this.fieldUseDataSet.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString(), out configValue);
                }
            }

            this.formProgress = new Progressform(configValue);
            ////this.formProgress.ProgressStatus = SharedFunctions.GetResourceString("GettingData");
            this.formProgress.ShowDialog();
        }

        /// <summary>
        /// Refreshes the local data base.
        /// </summary>
        private void RefreshLocalData()
        {
            WSHelper.IsOnLineMode = false;
            int returnValue = this.form3230Control.WorkItem.DeleteCheckOutTable;
            WSHelper.IsOnLineMode = true;
            this.GetServerDataProgress();
            ////MessageBox.Show(SharedFunctions.GetResourceString("CheckoutCompleted"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// CheckOutProcess
        /// </summary>
        private void CheckOutProcess()
        {
            if (this.FieldCheckOutDataGrid.ActiveRow.Index > -1)
            {
                if (WSHelper.IsOnLineMode)
                {
                    int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString()
                                                                                                , out this.selectedSnapShotId);
                    int RowendValue = 0;
                    F3230FieldUseData lockedParcelDataset = this.form3230Control.WorkItem.ListLockedParcelID(this.selectedSnapShotId
                                                                              , out RowendValue);
                    int lockedParcelCount = 0;
                    if (lockedParcelDataset != null && lockedParcelDataset.LockedParcel.Rows.Count > 0)
                        lockedParcelCount = lockedParcelDataset.LockedParcel.Rows.Count;
                    bool isParcelLocked = false;
                    if (lockedParcelDataset != null && lockedParcelCount > 0)
                    {
                        if (lockedParcelCount == RowendValue)
                        {
                            MessageBox.Show(lockedParcelCount.ToString() + " out of " + RowendValue.ToString() + " "
                                + SharedFunctions.GetResourceString("NotReadyForCheckOut")
                                , SharedFunctions.GetResourceString("NotReadyForCheckOutHeader")
                                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isParcelLocked = false;
                        }
                        else if (MessageBox.Show(lockedParcelCount.ToString() + " out of " + RowendValue.ToString() + " "
                            + SharedFunctions.GetResourceString("LockedParcels")
                            , SharedFunctions.GetResourceString("LockedParcelsHeader")
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            isParcelLocked = true;
                        }
                    }
                    if (lockedParcelCount <= 0)
                        isParcelLocked = true;

                    if (isParcelLocked)
                    {
                        if (ScriptEngine.IsServerAvailable())
                        {
                            bool isDatabaseAvail = TerraScanCommon.IsDataBaseAvailable;  
                            //bool isDatabaseAvail = ScriptEngine.IsDatabaseAvailable();
                            DialogResult ds = DialogResult.Yes;
                            if (isDatabaseAvail)
                                ds = MessageBox.Show(SharedFunctions.GetResourceString("AlreadyCheckout")
                                    , SharedFunctions.GetResourceString("CheckoutHeader")
                                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (!isDatabaseAvail || (isDatabaseAvail && ds == DialogResult.Yes))
                                this.GetServerDataProgress();
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("SqlNotAvailable"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RowSelect"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// Creates the new process.
        /// </summary>
        private void CreateNewProcess()
        {
            this.newCheckoutDB = true;
            this.ExecuteCheckoutProcess();
            ////MessageBox.Show(SharedFunctions.GetResourceString("CheckoutCompleted"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// Executes the checkout process.
        /// </summary>
        private void ExecuteCheckoutProcess()
        {
            backGroundWork.RunWorkerAsync();

            bool configValue = false;
            //this.fieldUseDataSet = this.form3230Control.WorkItem.GetcfgConfiguration("FieldUseDetails");

            if (this.fieldUseDataSet.Tables.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.fieldUseDataSet.ListCfgConfigTable.Rows[0][this.fieldUseDataSet.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString()))
                {
                    Boolean.TryParse(this.fieldUseDataSet.ListCfgConfigTable.Rows[0][this.fieldUseDataSet.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString(), out configValue);
                }
            }

            this.formProgress = new Progressform(configValue);
            this.formProgress.ShowDialog();
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="checkoutStatus">The checkout status.</param>
        private void UpdateStatus(string checkoutStatus)
        {
            this.formProgress.ProgressStatus = checkoutStatus;
        }

        /// <summary>
        /// Checkinbtn
        /// </summary>
        /// <param name="enable"></param>
        private void Checkinbtn(bool enable)
        {
            this.CheckInButton.Enabled = enable;
        }

        /// <summary>
        /// CheckOutBtn
        /// </summary>
        /// <param name="enable"></param>
        private void CheckOutBtn(bool enable)
        {
            this.CheckOutButton.Enabled = enable;
        }

        /// <summary>
        /// RemoveBtn
        /// </summary>
        /// <param name="enable"></param>
        private void RemoveBtn(bool enable)
        {
            this.RemoveDataButton.Enabled = enable;
        }

        /// <summary>
        /// LoadGrid
        /// </summary>
        private void LoadGrid(bool loadnum)
        {
            bool load = loadnum;
            this.LoadSnapShotDataGrid();
        }

        /// <summary>
        /// Updates the duration status.
        /// </summary>
        /// <param name="duration">The duration.</param>
        private void UpdateDurationStatus(string duration)
        {
            this.formProgress.ProcessDuration = duration;
        }

        /// <summary>
        /// Updates the button status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void UpdateFormClose(bool status)
        {
            this.formProgress.FormClose = status;
        }

        /// <summary>
        /// Checks the grid row count.
        /// </summary>
        private void CheckGridRowCount()
        {
            UltraGridRow[] filteredRow = this.FieldCheckOutDataGrid.Rows.GetFilteredInNonGroupByRows();

            if (filteredRow.Length > 0)
            {
                this.CheckOutButton.Enabled = true;
            }
            else
            {
                this.CheckOutButton.Enabled = false;
            }
        }

        /// <summary>
        /// Assigns the text box value.
        /// </summary>
        private void ClearValue()
        {
            this.SnapshotNameTextBox.Text = string.Empty;
            this.CreatedByTextBox.Text = string.Empty;
            this.DateTextBox.Text = string.Empty;
            this.CountTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Inserts the application config.
        /// </summary>
        private void InsertApplicationConfig()
        {
            WSHelper.IsOnLineMode = true;
            this.checkDataSet = WSHelper.GetConfigInformation();
            WSHelper.IsOnLineMode = false;

            if (this.checkDataSet.Tables.Count > 0)
            {
                this.form3230Control.WorkItem.InsertApplicationConfiguration(TerraScanCommon.GetXmlString(this.checkDataSet.Tables[0]), TerraScanCommon.UserId);
            }
        }

        #endregion Methods

        /// <summary>
        /// PreviewButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FieldCheckOutDataGrid.ActiveRow.Index > -1)
                {
                    DataTable previewDataTable = new DataTable();
                    DataColumn[] previewColumn = new DataColumn[] { new DataColumn("UserID") };
                    previewDataTable.Columns.AddRange(previewColumn);
                    DataRow previewRow = previewDataTable.NewRow();
                    previewRow["UserID"] = TerraScanCommon.UserId;
                    previewDataTable.Rows.Add(previewRow);

                    int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                    this.fieldUseDataSetData.ListPreviewDetailTable.Clear();
                    this.fieldUseDataSetData.ListPreviewDetailTable.Merge(this.form3230Control.WorkItem.GetPreviewDetail(this.selectedSnapShotId, TerraScanCommon.GetXmlString(previewDataTable)));

                    if (this.fieldUseDataSetData.ListPreviewDetailTable.Rows.Count > 0)
                    {
                        int totalParcel = 0;
                        int lockedParcel = 0;
                        int deletedParcel = 0;
                        int.TryParse(this.fieldUseDataSetData.ListPreviewDetailTable.Rows[0][this.fieldUseDataSetData.ListPreviewDetailTable.SnapCountColumn.ColumnName].ToString(), out totalParcel);
                        int.TryParse(this.fieldUseDataSetData.ListPreviewDetailTable.Rows[0][this.fieldUseDataSetData.ListPreviewDetailTable.ParcelCountColumn.ColumnName].ToString(), out deletedParcel);
                        int.TryParse(this.fieldUseDataSetData.ListPreviewDetailTable.Rows[0][this.fieldUseDataSetData.ListPreviewDetailTable.LockedParcelColumn.ColumnName].ToString(), out lockedParcel);

                        int deletParcel = totalParcel - deletedParcel;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("PreviewRowSelect"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// FieldCheckOutDataGrid_InitializeLayout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldCheckOutDataGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ApprasialGreenPictureBox_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApprasialGreenPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.apprasialLock)
                {
                    this.apprasialLock = false;
                }
                else
                {
                    this.apprasialLock = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ValueGreenpictureBox_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.valueLock)
                {
                    this.valueLock = false;
                }
                else
                {
                    this.valueLock = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AdminGreenpictureBox_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.adminLock)
                {
                    this.adminLock = false;
                }
                else
                {
                    this.adminLock = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CheckInButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckInButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                backGroundWorkCheckIn.RunWorkerAsync();
                this.formProgress = new Progressform(true);
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("Checkin in progress.") });
                this.formProgress.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                WSHelper.IsOnLineMode = true;
            }
        }

        /// <summary>
        /// RemoveDataButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(SharedFunctions.GetResourceString("RemoveData"), SharedFunctions.GetResourceString("RemoveDataHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        backGroundWorkRemoveData.RunWorkerAsync();
                        this.formProgress = new Progressform(false);
                        this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("Remove data in progress.") });
                        this.formProgress.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Network is not available to remove data.", "Network is not available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                WSHelper.IsOnLineMode = true;
            }
        }

        /// <summary>
        /// CheckOutButton_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOutButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.CheckOutProcess();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                WSHelper.IsOnLineMode = true;
            }
        }

        /// <summary>
        /// F3230_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F3230_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.fieldUseDataSet = this.form3230Control.WorkItem.GetSnapshotDetail();
                this.FieldCheckOutDataGrid.DataSource = this.fieldUseDataSet.ListSnapshotTable.DefaultView;

                if (this.FieldCheckOutDataGrid.Rows.Count > 0)
                {
                    this.FieldCheckOutDataGrid.DisplayLayout.Rows[0].Activated = true;
                    this.FieldCheckOutDataGrid.DisplayLayout.Rows[0].Selected = true;
                    this.CheckOutButton.Enabled = true;
                }
                else
                {
                    this.CheckOutButton.Enabled = false;
                }
                if (WSHelper.IsOnLineMode)
                {
                    this.CheckInButton.Enabled = false;
                    this.RemoveDataButton.Enabled = false;
                    this.CheckOutButton.Enabled = true;
                }
                else
                {
                    this.CheckInButton.Enabled = true;
                    this.RemoveDataButton.Enabled = true;
                    this.CheckOutButton.Enabled = false;
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// FieldCheckOutDataGrid_AfterRowActivate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldCheckOutDataGrid_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow activeRow = this.FieldCheckOutDataGrid.ActiveRow;

                if (activeRow != null)
                {
                    if (activeRow.Index == -1)
                    {
                        this.ClearValue();
                        return;
                    }

                    this.SnapshotNameTextBox.Text = this.FieldCheckOutDataGrid.DisplayLayout.Rows[activeRow.Index].Cells[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNameColumn.ColumnName].Value.ToString();
                    this.CreatedByTextBox.Text = this.FieldCheckOutDataGrid.DisplayLayout.Rows[activeRow.Index].Cells[this.fieldUseDataSetData.ListSnapshotTable.CreatedByColumn.ColumnName].Value.ToString();
                    this.DateTextBox.Text = this.FieldCheckOutDataGrid.DisplayLayout.Rows[activeRow.Index].Cells[this.fieldUseDataSetData.ListSnapshotTable.InsertedDateColumn.ColumnName].Value.ToString();
                    this.CountTextBox.Text = this.FieldCheckOutDataGrid.DisplayLayout.Rows[activeRow.Index].Cells[this.fieldUseDataSetData.ListSnapshotTable.RecordCountColumn.ColumnName].Value.ToString();
                    this.DescriptionTextBox.Text = this.FieldCheckOutDataGrid.DisplayLayout.Rows[activeRow.Index].Cells[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// FieldCheckOutDataGrid_AfterPerformAction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldCheckOutDataGrid_AfterPerformAction(object sender, AfterUltraGridPerformActionEventArgs e)
        {
            try
            {
                this.CheckGridRowCount();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// FieldCheckOutDataGrid_AfterRowFilterChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldCheckOutDataGrid_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
        {
            try
            {
                this.CheckGridRowCount();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// LoadSnapShotDataGrid
        /// </summary>
        private void LoadSnapShotDataGrid()
        {
            //if (ScriptEngine.IsDatabaseAvailable())
           if (TerraScanCommon.IsDataBaseAvailable)
            {
                WSHelper.IsOnLineMode = false;
                TerraScanCommon.IsFieldUser = true;
            }
            else
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    WSHelper.IsOnLineMode = true;
                }
                else
                {
                    MessageBox.Show("Network is not available.", "Network is not available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            this.LoadWorkSpace();
            this.fieldUseDataSet = this.form3230Control.WorkItem.GetSnapshotDetail();
            this.FieldCheckOutDataGrid.DataSource = this.fieldUseDataSet.ListSnapshotTable.DefaultView;
            if (TerraScanCommon.IsDataBaseAvailable)
            {
                this.SnapshotNameTextBox.Text = string.Empty;
                this.CreatedByTextBox.Text = string.Empty;
                this.DateTextBox.Text = string.Empty;
                this.CountTextBox.Text = string.Empty;
                this.DescriptionTextBox.Text = string.Empty;
            }

            if (this.FieldCheckOutDataGrid.Rows.Count > 0)
            {
                this.FieldCheckOutDataGrid.DisplayLayout.Rows[0].Activated = true;
                this.FieldCheckOutDataGrid.DisplayLayout.Rows[0].Selected = true;
                this.CheckOutButton.Enabled = true;
            }
            else
            {
                this.CheckOutButton.Enabled = false;
            }
            if (WSHelper.IsOnLineMode)
            {
                this.Invoke(new UpdateButtonStatusHandler(this.Checkinbtn), new object[1] { false });
                this.Invoke(new UpdateButtonStatusHandler(this.CheckOutBtn), new object[1] { true });
                this.Invoke(new UpdateButtonStatusHandler(this.RemoveBtn), new object[1] { false });
            }
            else
            {
                this.Invoke(new UpdateButtonStatusHandler(this.Checkinbtn), new object[1] { true });
                this.Invoke(new UpdateButtonStatusHandler(this.CheckOutBtn), new object[1] { false });
                this.Invoke(new UpdateButtonStatusHandler(this.RemoveBtn), new object[1] { true });
            }
        }

        /// <summary>
        /// RefeshMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefeshMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadSnapShotDataGrid();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }
    }
}