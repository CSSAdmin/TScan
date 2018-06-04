//--------------------------------------------------------------------------------------------
// <copyright file="F9065.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Nov 07      karthikeyan V      Created
//*********************************************************************************/

namespace D9065
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
    using System.IO;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infragistics.Win.UltraWinGrid;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.SqlServer.Management.Common;
    using TerraScan.Helper;
    using System.Threading;
    using System.Linq;

    /// <summary>
    /// F9065
    /// </summary>
    public partial class F9065 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Background thread
        /// </summary>
        private static BackgroundWorker backGroundWork;

        /// <summary>
        /// f9060Controller Controller.
        /// </summary>
        private F9065Controller form9065Control;

        /// <summary>
        /// chkOutXML
        /// </summary>
        private string chkOutXML = string.Empty;

        /// <summary>
        /// F9065FieldUseData
        /// </summary>
        private F9065FieldUseData fieldUseDataSetData = new F9065FieldUseData();

        private F3230FieldUseData fieldUseDataSet = new F3230FieldUseData();


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
        /// F9065FieldUseData
        /// </summary>
        //private F9065FieldUseData fieldCheckOutDataSet = new F9065FieldUseData();
        private F3230FieldUseData fieldCheckOutDataSet = new F3230FieldUseData();

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9065"/> class.
        /// </summary>
        public F9065()
        {
            this.InitializeComponent();
            this.InitializeBackgoundWorker();
            this.SnapshotPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SnapshotPictureBox.Height, this.SnapshotPictureBox.Width, "Snapshots", 28, 81, 128);
            this.DetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DetailPictureBox.Height, this.DetailPictureBox.Width, "", 28, 81, 128);
        }

        #endregion

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

        #endregion

        #region Event Publication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        #endregion

        #region Property

        /// <summary>
        /// Creates Property for F9060Controller
        /// </summary>
        [CreateNew]
        public F9065Controller F9065Control
        {
            get { return this.form9065Control as F9065Controller; }
            set { this.form9065Control = value; }
        }

        #endregion Property

        #region BackGroundWorker

        /// <summary>
        /// Handles the ProgressChanged event of the BackGroundWork control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ////this.percent = e.ProgressPercentage;
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

                if (this.newCheckoutDB)
                {
                    if (ScriptEngine.IsDatabaseAvailable())
                    {
                        this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("DetachingDB") });
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

                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("AttachDB") });
                    startTime = Environment.TickCount;
                    ScriptEngine.CreateNewFieldDataBase();
                    endTime = Environment.TickCount;
                    processTime = (endTime - startTime) / 1000;
                    this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("AttachingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });

                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("GettingData") });
                    startTime = Environment.TickCount;
                    this.InsertApplicationConfig();
                    this.GetServerData();
                    endTime = Environment.TickCount;
                    processTime = (endTime - startTime) / 1000;
                    this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("GettingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });

                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingDB") });
                    startTime = Environment.TickCount;
                    this.InsertServerData();
                    endTime = Environment.TickCount;
                    processTime = (endTime - startTime) / 1000;
                    this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });

                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingSchema") });
                    startTime = Environment.TickCount;
                    this.AuditProcess();
                    endTime = Environment.TickCount;
                    processTime = (endTime - startTime) / 1000;
                    this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("SchemaCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });
                    this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("CheckoutCompleted") });
                    this.Invoke(new UpdateButtonStatusHandler(this.UpdateProgressBarStatus), new object[1] { false });
                }
                else
                {
                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("GettingData") });
                    startTime = Environment.TickCount;
                    this.GetServerData();
                    endTime = Environment.TickCount;
                    processTime = (endTime - startTime) / 1000;
                    this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("GettingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });

                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingDB") });
                    startTime = Environment.TickCount;
                    this.InsertServerData();
                    endTime = Environment.TickCount;
                    processTime = (endTime - startTime) / 1000;
                    this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });
                    this.Invoke(new UpdateButtonStatusHandler(this.UpdateButtonStatus), new object[1] { true });
                    this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("CheckoutCompleted") });
                    this.Invoke(new UpdateButtonStatusHandler(this.UpdateProgressBarStatus), new object[1] { false });
                }

                this.newCheckoutDB = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
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

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the backgound worker.
        /// </summary>
        private void InitializeBackgoundWorker()
        {
            backGroundWork = new BackgroundWorker();
            backGroundWork.DoWork += new DoWorkEventHandler(this.BackGroundWorkDoWork);
            backGroundWork.ProgressChanged += new ProgressChangedEventHandler(this.BackGroundWorkProgressChanged);
            backGroundWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackGroundWorkRunWorkerCompleted);
        }

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form9065Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form9065Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form9065Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.formLabelInfo[0] = "Field Checkout";
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            // Assigning the column width
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNameColumn.ColumnName].Width = 215;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Width = 220;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.CreatedByColumn.ColumnName].Width = 100;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.InsertedDateColumn.ColumnName].Width = 100;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.RecordCountColumn.ColumnName].Width = 101;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotIDColumn.ColumnName].Width = 0;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.FormColumn.ColumnName].Width = 0;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.InsertedByColumn.ColumnName].Width = 0;

            // Assigning the header caption
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNameColumn.ColumnName].Header.Caption = "Snapshot Name";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Header.Caption = "Description";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.CreatedByColumn.ColumnName].Header.Caption = "Created By";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.InsertedDateColumn.ColumnName].Header.Caption = "Date";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.RecordCountColumn.ColumnName].Header.Caption = "Count";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotIDColumn.ColumnName].Header.Caption = "SnapshotID";
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.FormColumn.ColumnName].Header.Caption = "Form";

            // Assigning the VisiblePosition
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNameColumn.ColumnName].Header.VisiblePosition = 0;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Header.VisiblePosition = 1;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.CreatedByColumn.ColumnName].Header.VisiblePosition = 2;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.InsertedDateColumn.ColumnName].Header.VisiblePosition = 3;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.RecordCountColumn.ColumnName].Header.VisiblePosition = 4;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotIDColumn.ColumnName].Header.VisiblePosition = 5;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.FormColumn.ColumnName].Header.VisiblePosition = 6;

            // Makeing the column readonly
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNameColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNoteColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.CreatedByColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.InsertedDateColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.RecordCountColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotIDColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.FormColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNameColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNoteColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.CreatedByColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.InsertedDateColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.RecordCountColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotIDColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.FormColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;

            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNameColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotNoteColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.CreatedByColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.InsertedDateColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.RecordCountColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.SnapshotIDColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.FieldCheckOutDataGrid.DisplayLayout.Bands[0].Columns[this.fieldUseDataSetData.ListSnapshotTable.FormColumn.ColumnName].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
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
            tempRow["Attachemnts"] = this.AttachmentCheckBox.Checked.ToString();
            tempRow["Comments"] = this.CommentsCheckBox.Checked.ToString();
            tempRow["PrimaryOnly"] = this.PrimaryOnlyCheckBox.Checked.ToString();
            tempRow["Sketches"] = this.SketchesCheckBox.Checked.ToString();
            tempRow["ResetField1"] = this.ResetField1CheckBox.Checked.ToString();
            tempRow["ResetField2"] = this.ResetField2CheckBox.Checked.ToString();
            tempRow["ResetField3"] = this.ResetField3CheckBox.Checked.ToString();
            tempRow["ResetField4"] = this.ResetField4CheckBox.Checked.ToString();
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
        /// Checks the script file version.
        /// </summary>
        /// <returns>bool</returns>
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
        /// Gets the server data progress.
        /// </summary>
        private void GetServerDataProgress()
        {
            backGroundWork.RunWorkerAsync();

            ////this.job = new ThreadStart(delegate() { this.ThreadJob("Inserting data to local DB.."); });
            ////this.thread = new Thread(this.job);
            ////this.thread.IsBackground = false;
            ////this.thread.Start();       

            bool configValue = false;
            this.fieldUseDataSetData = this.form9065Control.WorkItem.GetcfgConfiguration("FieldUseDetails");

            if (this.fieldUseDataSet.Tables.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.fieldUseDataSetData.ListCfgConfigTable.Rows[0][this.fieldUseDataSetData.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString()))
                {
                    Boolean.TryParse(this.fieldUseDataSetData.ListCfgConfigTable.Rows[0][this.fieldUseDataSetData.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString(), out configValue);
                }
            }

            this.formProgress = new Progressform(configValue);
            ////this.formProgress.ProgressStatus = SharedFunctions.GetResourceString("GettingData");
            this.formProgress.ShowDialog();
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
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutConfigXML(this.selectedSnapShotId, this.GetXmlString());
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
            F25000FieldUseData fieldUseData = this.form9065Control.WorkItem.ParcelHeaderChkOutXML(this.selectedSnapShotId, this.GetXmlString());
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
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutFormXML(this.selectedSnapShotId, this.GetXmlString(), 1);
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
        /// ChkOutMiscXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutMiscXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutMiscXML(this.selectedSnapShotId, this.GetXmlString());
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

        
        private string ProcessChkOutEventXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutEventXML(this.selectedSnapShotId
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

        
        private string ProcessChkOutOwnerXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutOwnerXML(this.selectedSnapShotId
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
               
        
        private string ProcessChkOutDistrictXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutDistrictXML(this.selectedSnapShotId
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

        //private string ProcessChkOutVSTerraGonXML(int nRowcount, out int RowCount)
        //{
        //    RowCount = -1;
        //    this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutVSTerraGonXML(this.selectedSnapShotId
        //        , this.GetXmlString()
        //        , "tAA_VSTerraGon"
        //        , nRowcount
        //        , out RowCount);
        //    DataSet tempDataset = new DataSet("Root");
        //    string root = "<Root>";
        //    for (int index = 0; index < this.fieldCheckOutDataSet.Tables.Count; index++)
        //    {
        //        if (this.fieldCheckOutDataSet.Tables[index].Rows.Count > 0)
        //        {
        //            tempDataset.Tables.Add(this.fieldCheckOutDataSet.Tables[index].Copy());
        //            chkOutXML += tempDataset.GetXml();
        //            tempDataset.Clear();
        //        }
        //    }
        //    chkOutXML = chkOutXML.Replace("<Root>", "").Replace("<Root />", "").Replace("</Root>", "").Replace("'", "''");
        //    chkOutXML = root + chkOutXML + "</Root>";
        //    return chkOutXML;
        //}
        
        private string ProcessChkOutEstimateComponentXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutEstimateComponentXML(this.selectedSnapShotId
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

        private string ProcessChkOutCommentXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutCommentXML(this.selectedSnapShotId
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

        private string ProcessChkOutVSTGComponentXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutVSTGComponentXML(this.selectedSnapShotId
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

        private string ProcessChkOutFileXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutFileXML(this.selectedSnapShotId
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

        private string ProcessChkOutVSTGGonBldgXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutVSTGGonBldgXML(this.selectedSnapShotId
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
        private string ProcessChkOutNBHDXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutNBHDXML(this.selectedSnapShotId
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
        
        private string ProcessChkOutLegalXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutLegalXML(this.selectedSnapShotId
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
        
        
        private string ProcessChkOutMiscTableXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutMiscTableXML(this.selectedSnapShotId
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
        
        
        private string ProcessChkOutMisc_CatalogXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutMisc_CatalogXML(this.selectedSnapShotId
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
        
        
        private string ProcessChkOutMOwnerXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutMOwnerXML(this.selectedSnapShotId
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
        
        
        private string ProcessChkOutObjectXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutObjectXML(this.selectedSnapShotId
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
        
        
        private string ProcessChkOutValueSliceXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutValueSliceXML(this.selectedSnapShotId
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
        
        
        private string ProcessChkOutLandXML(int nRowcount, out int RowCount)
        {
            RowCount = -1;
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form9065Control.WorkItem.ChkOutLandXML(this.selectedSnapShotId
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
        
        
        private string ProcessChkOutVersionXML(int nRowcount, out int RowCount)
        {
            RowCount = -1; 
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form9065Control.WorkItem.ChkOutVersionXML(this.selectedSnapShotId
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

        
        private void GetEventXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutEventXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }


        private void GetOwnerXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutOwnerXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }

        private void GetNBHDXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutNBHDXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }

        private void GetDistrictXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutDistrictXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }

        private void GetVSTerraGonXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    //chkOutXML = this.ProcessChkOutVSTerraGonXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }

        private void GetEstimateComponentXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutEstimateComponentXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }

        private void GetCommentXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutCommentXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }

        private void GetVSTGComponentXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutVSTGComponentXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }

        private void GetChkOutFileXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutFileXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        

        private void GetVSTGGonBldgXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutVSTGGonBldgXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        private void GetLegalXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutLegalXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        
        
        private void GetMiscTableXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutMiscTableXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        
        
        private void GetMisc_CatalogXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutMisc_CatalogXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        
        
        private void GetMOwnerXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutMOwnerXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        
        
        private void GetObjectXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutObjectXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        
        
        private void GetValueSliceXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutValueSliceXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        
        
        private void GetLandXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutLandXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }
        
        
        private void GetVersionXML()
        {
            try
            {
                chkOutXML = string.Empty;
                int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
                int nRowCount = -1;
                while (nRowCount != 0)
                {
                    WSHelper.IsOnLineMode = true;
                    chkOutXML = this.ProcessChkOutVersionXML(nRowCount, out nRowCount);
                    WSHelper.IsOnLineMode = false;
                    if (nRowCount == -1)
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, true);
                    else
                        this.form9065Control.WorkItem.InsertCheckOutXml(chkOutXML, String.Empty, TerraScanCommon.UserId, false);
                    chkOutXML = String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
        }


        private string ChkOutSitusXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutSitusXML(this.selectedSnapShotId, this.GetXmlString());
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
        
        
        private string ChkOutSeniorExemptionXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form9065Control.WorkItem.ChkOutSeniorExemptionXML(this.selectedSnapShotId, this.GetXmlString());
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
        
        
        private string ChkOutAssessmentTypeXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form9065Control.WorkItem.ChkOutAssessmentTypeXML(this.selectedSnapShotId, this.GetXmlString());
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
        
        
        private string ChkOutParcelValueXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form9065Control.WorkItem.ChkOutParcelValueXML(this.selectedSnapShotId, this.GetXmlString());
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
        private string ChkOutType2XML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F25000FieldUseData fieldDataSet = new F25000FieldUseData();
            fieldDataSet = this.form9065Control.WorkItem.ChkOutType2XML(this.selectedSnapShotId, this.GetXmlString());
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
            fieldDataSet = this.form9065Control.WorkItem.ChkOutDeprMiscXML(this.selectedSnapShotId, this.GetXmlString());
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
            System.Diagnostics.Debug.Write(chkOutXML);
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
            fieldDataSet = this.form9065Control.WorkItem.ChkOutEstimateCompXML(this.selectedSnapShotId, this.GetXmlString());
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
            fieldDataSet = this.form9065Control.WorkItem.ChkOutVSTGCitemXML(this.selectedSnapShotId, this.GetXmlString());
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
            fieldDataSet = this.form9065Control.WorkItem.ChkOutMSCEstimateXML(this.selectedSnapShotId, this.GetXmlString());
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
            fieldDataSet = this.form9065Control.WorkItem.ChkOutEstimateResultXML(this.selectedSnapShotId, this.GetXmlString());
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
            fieldDataSet = this.form9065Control.WorkItem.ChkOutMSCEstimateOccupancyXML(this.selectedSnapShotId, this.GetXmlString());
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
            fieldDataSet = this.form9065Control.WorkItem.ChkOutEstimateBuildingXML(this.selectedSnapShotId, this.GetXmlString());
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
        /// ChkOutLandValuesXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutLandValuesXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            F3230FieldUseData fieldDataSet = new F3230FieldUseData();
            //fieldDataSet = this.form9065Control.WorkItem.ChkOutLandValuesXML(this.selectedSnapShotId, this.GetXmlString());
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
        /// ChkOutUserXML
        /// </summary>
        /// <returns></returns>
        private string ChkOutUserXML()
        {
            chkOutXML = string.Empty;
            WSHelper.IsOnLineMode = true;
            int.TryParse(this.FieldCheckOutDataGrid.Rows[this.FieldCheckOutDataGrid.ActiveRow.Index].Cells["SnapshotID"].Value.ToString(), out this.selectedSnapShotId);
            this.fieldCheckOutDataSet = this.form9065Control.WorkItem.ChkOutUserXML(this.selectedSnapShotId, this.GetXmlString());
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
        /// Checks the out process.
        /// </summary>
        private void GetServerData()
        {
            try
            {
                int processTime = 0;
                int startTime = 0;
                int endTime = 0;
                endTime = Environment.TickCount;
                processTime = (endTime - startTime) / 1000;
                this.Invoke(new UpdateStatusHandler(this.UpdateDurationStatus), new object[1] { SharedFunctions.GetResourceString("GettingCompleted") + processTime + SharedFunctions.GetResourceString("CheckoutSeconds") });
                this.Invoke(new UpdateStatusHandler(this.UpdateStatus), new object[1] { SharedFunctions.GetResourceString("UpdatingDB") });
                startTime = Environment.TickCount;
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutConfigXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutFormXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutMiscXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ParcelHeaderChkOutXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutSitusXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutSeniorExemptionXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutAssessmentTypeXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutParcelValueXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutType2XML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutDeprMiscXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutEstimateCompXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutVSTGCitemXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutMSCEstimateXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutEstimateResultXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutMSCEstimateOccupancyXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutEstimateBuildingXML(), string.Empty, TerraScanCommon.UserId, true);
                this.form9065Control.WorkItem.InsertCheckOutXml(this.ChkOutLandValuesXML(), string.Empty, TerraScanCommon.UserId, true);
                this.GetEventXML();
                this.GetOwnerXML();
                this.GetNBHDXML();
                this.GetDistrictXML();
                this.GetVSTerraGonXML();
                this.GetEstimateComponentXML();
                this.GetCommentXML();
                this.GetVSTGComponentXML();
                this.GetChkOutFileXML();
                this.GetVSTGGonBldgXML();
                this.GetLegalXML();
                this.GetMiscTableXML();
                this.GetMisc_CatalogXML();
                this.GetMOwnerXML();
                this.GetObjectXML();
                this.GetValueSliceXML();
                this.GetLandXML();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                this.Invoke(new UpdateButtonStatusHandler(this.UpdateFormClose), new object[1] { true });
            }
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
            this.form9065Control.WorkItem.InsertFieldElement(this.GetXmlString(), TerraScanCommon.UserId);
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
                this.form9065Control.WorkItem.InsertApplicationConfiguration(TerraScanCommon.GetXmlString(this.checkDataSet.Tables[0]), TerraScanCommon.UserId);
            }
        }

        /// <summary>
        /// Checks the out process.
        /// </summary>
        private void CheckOutProcess()
        {
            if (this.FieldCheckOutDataGrid.ActiveRow.Index > -1)
            {
                if (WSHelper.IsOnLineMode)
                {
                    bool checkOutProccessed = false;
                    int auditRow = 0;

                    if (ScriptEngine.IsServerAvailable())
                    {
                        if (ScriptEngine.IsDatabaseAvailable())
                        {
                            if (this.CheckScriptFileVersion())
                            {
                                WSHelper.IsOnLineMode = false;
                                this.checkDataSet = WSHelper.GetConfigInformation();
                                WSHelper.IsOnLineMode = true;

                                // Make one server db call to get the Checkout variable
                                if (!string.IsNullOrEmpty(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString()))
                                {
                                    checkOutProccessed = Convert.ToBoolean(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString());
                                }

                                if (checkOutProccessed)
                                {
                                    if (MessageBox.Show(SharedFunctions.GetResourceString("AlreadyCheckout"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        WSHelper.IsOnLineMode = false;
                                        auditRow = this.form9065Control.WorkItem.GetAuditCount;
                                        if (auditRow > 0)
                                        {
                                            if (MessageBox.Show(SharedFunctions.GetResourceString("ChangesInDataBase"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                this.RefreshLocalData();
                                            }
                                        }
                                        else
                                        {
                                            this.RefreshLocalData();
                                        }
                                    }
                                }
                                else
                                {
                                    this.GetServerDataProgress();
                                    //// MessageBox.Show(SharedFunctions.GetResourceString("CheckoutCompleted"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                }
                            }
                            else
                            {
                                WSHelper.IsOnLineMode = false;
                                this.checkDataSet = WSHelper.GetConfigInformation();
                                WSHelper.IsOnLineMode = true;

                                if (!string.IsNullOrEmpty(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString()))
                                {
                                    checkOutProccessed = Convert.ToBoolean(this.checkDataSet.Tables[0].Rows[0]["IsCheckedOut"].ToString());
                                }

                                if (checkOutProccessed)
                                {
                                    if (MessageBox.Show(SharedFunctions.GetResourceString("AlreadyCheckout"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        WSHelper.IsOnLineMode = false;
                                        auditRow = this.form9065Control.WorkItem.GetAuditCount;
                                        if (auditRow > 0)
                                        {
                                            if (MessageBox.Show(SharedFunctions.GetResourceString("ChangesInDataBase"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                if (ScriptEngine.DataFileExists())
                                                {
                                                    this.CreateNewProcess();
                                                }
                                                else
                                                {
                                                    MessageBox.Show(SharedFunctions.GetResourceString("FileNotFound"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (ScriptEngine.DataFileExists())
                                            {
                                                this.CreateNewProcess();
                                            }
                                            else
                                            {
                                                MessageBox.Show(SharedFunctions.GetResourceString("FileNotFound"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ScriptEngine.DataFileExists())
                                    {
                                        this.CreateNewProcess();
                                    }
                                    else
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("FileNotFound"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ScriptEngine.DataFileExists())
                            {
                                this.CreateNewProcess();
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("FileNotFound"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("SqlNotAvailable"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    ////MessageBox.Show("Switch to OnLine mode for checkout process", SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                ////}
                ////else
                ////{
                ////    MessageBox.Show("Checkout operation cannot proceed without" + TerraScanCommon.FieldServerName + "SQL Express Instance.\n\n Please install the above before doing checkout.", "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RowSelect"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// Executes the checkout process.
        /// </summary>
        private void ExecuteCheckoutProcess()
        {
            backGroundWork.RunWorkerAsync();

            bool configValue = false;
            this.fieldUseDataSetData = this.form9065Control.WorkItem.GetcfgConfiguration("FieldUseDetails");

            if (this.fieldUseDataSet.Tables.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.fieldUseDataSetData.ListCfgConfigTable.Rows[0][this.fieldUseDataSetData.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString()))
                {
                    Boolean.TryParse(this.fieldUseDataSetData.ListCfgConfigTable.Rows[0][this.fieldUseDataSetData.ListCfgConfigTable.ConfigurationValueColumn.ColumnName].ToString(), out configValue);
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
        private void UpdateButtonStatus(bool status)
        {
            this.formProgress.EnableButton = status;
        }

        /// <summary>
        /// Updates the button status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void UpdateProgressBarStatus(bool status)
        {
            this.formProgress.DisableProgressBar = status;
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
        /// Refreshes the local data base.
        /// </summary>
        private void RefreshLocalData()
        {
            WSHelper.IsOnLineMode = false;
            int returnValue = this.form9065Control.WorkItem.DeleteCheckOutTable;
            WSHelper.IsOnLineMode = true;
            this.GetServerDataProgress();
            ////MessageBox.Show(SharedFunctions.GetResourceString("CheckoutCompleted"), SharedFunctions.GetResourceString("CheckoutHeader"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        /// Checks the grid row count.
        /// </summary>
        private void CheckGridRowCount()
        {
            UltraGridRow[] filteredRow = this.FieldCheckOutDataGrid.Rows.GetFilteredInNonGroupByRows();

            if (filteredRow.Length > 0)
            {
                this.PreviewButton.Enabled = true;
                this.CheckOutButton.Enabled = true;
                this.SubFormPanel.Enabled = true;
            }
            else
            {
                this.PreviewButton.Enabled = false;
                this.CheckOutButton.Enabled = false;
                this.SubFormPanel.Enabled = false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F9065 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9065_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpace();
                this.fieldUseDataSetData = this.form9065Control.WorkItem.GetSnapshotDetail();
                this.FieldCheckOutDataGrid.DataSource = this.fieldUseDataSetData;

                if (this.FieldCheckOutDataGrid.Rows.Count > 0)
                {
                    this.FieldCheckOutDataGrid.DisplayLayout.Rows[0].Activated = true;
                    this.FieldCheckOutDataGrid.DisplayLayout.Rows[0].Selected = true;
                    this.PreviewButton.Enabled = true;
                    this.CheckOutButton.Enabled = true;
                    this.SubFormPanel.Enabled = true;
                }
                else
                {
                    this.PreviewButton.Enabled = false;
                    this.CheckOutButton.Enabled = false;
                    this.SubFormPanel.Enabled = false;
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
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeLayout event of the FieldCheckOutDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
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
        /// Handles the AfterRowActivate event of the FieldCheckOutDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
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

                    /* this.SnapshotNameTextBox.Text = this.fieldUseDataSet.ListSnapshotTable.Rows[activeRow.Index][this.fieldUseDataSet.ListSnapshotTable.SnapshotNameColumn.ColumnName].ToString();
                    this.CreatedByTextBox.Text = this.fieldUseDataSet.ListSnapshotTable.Rows[activeRow.Index][this.fieldUseDataSet.ListSnapshotTable.CreatedByColumn.ColumnName].ToString();
                    this.DateTextBox.Text = this.fieldUseDataSet.ListSnapshotTable.Rows[activeRow.Index][this.fieldUseDataSet.ListSnapshotTable.InsertedDateColumn.ColumnName].ToString();
                    this.CountTextBox.Text = this.fieldUseDataSet.ListSnapshotTable.Rows[activeRow.Index][this.fieldUseDataSet.ListSnapshotTable.RecordCountColumn.ColumnName].ToString();
                    this.DescriptionTextBox.Text = this.fieldUseDataSet.ListSnapshotTable.Rows[activeRow.Index][this.fieldUseDataSet.ListSnapshotTable.SnapshotNoteColumn.ColumnName].ToString(); */

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
        /// Handles the Click event of the CheckOutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the AppRedPictureBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AppRedPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.apprasialLock)
                {
                    this.ApprasialRedPictureBox.Visible = false;
                    this.ApprasialGreenPictureBox.Visible = true;
                    this.apprasialLock = false;
                }
                else
                {
                    this.ApprasialRedPictureBox.Visible = true;
                    this.ApprasialGreenPictureBox.Visible = false;
                    this.apprasialLock = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ValueGreenpictureBox6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValueGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.valueLock)
                {
                    this.ValueRedPictureBox.Visible = false;
                    this.ValueGreenpictureBox.Visible = true;
                    this.valueLock = false;
                }
                else
                {
                    this.ValueRedPictureBox.Visible = true;
                    this.ValueGreenpictureBox.Visible = false;
                    this.valueLock = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AdminGreenpictureBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AdminGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.adminLock)
                {
                    this.AdminRedpictureBox.Visible = false;
                    this.AdminGreenpictureBox.Visible = true;
                    this.adminLock = false;
                }
                else
                {
                    this.AdminRedpictureBox.Visible = true;
                    this.AdminGreenpictureBox.Visible = false;
                    this.adminLock = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="/T:System.EventArgs"/> instance containing the event data.</param>
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
                    this.fieldUseDataSetData.ListPreviewDetailTable.Merge(this.form9065Control.WorkItem.GetPreviewDetail(this.selectedSnapShotId, TerraScanCommon.GetXmlString(previewDataTable)));

                    if (this.fieldUseDataSetData.ListPreviewDetailTable.Rows.Count > 0)
                    {
                        int totalParcel = 0;
                        int lockedParcel = 0;
                        int deletedParcel = 0;
                        int.TryParse(this.fieldUseDataSetData.ListPreviewDetailTable.Rows[0][this.fieldUseDataSetData.ListPreviewDetailTable.SnapCountColumn.ColumnName].ToString(), out totalParcel);
                        int.TryParse(this.fieldUseDataSetData.ListPreviewDetailTable.Rows[0][this.fieldUseDataSetData.ListPreviewDetailTable.ParcelCountColumn.ColumnName].ToString(), out deletedParcel);
                        int.TryParse(this.fieldUseDataSetData.ListPreviewDetailTable.Rows[0][this.fieldUseDataSetData.ListPreviewDetailTable.LockedParcelColumn.ColumnName].ToString(), out lockedParcel);

                        int deletParcel = totalParcel - deletedParcel;
                        this.PreviewCountLabel.Text = "Parcels " + lockedParcel + " out of " + totalParcel + " has been locked.";
                        this.PreviewDeleteLabel.Text = "Parcels " + deletParcel + " out of " + totalParcel + " has been deleted.";
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
        /// Handles the AfterRowFilterChanged event of the FieldCheckOutDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs"/> instance containing the event data.</param>
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
        /// Handles the AfterPerformAction event of the FieldCheckOutDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.AfterUltraGridPerformActionEventArgs"/> instance containing the event data.</param>
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

        #endregion
    }
}