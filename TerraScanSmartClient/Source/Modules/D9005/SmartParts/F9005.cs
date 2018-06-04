//--------------------------------------------------------------------------------------------
// <copyright file="F9005.cs" company="Congruent">
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
// 19 July 06		Karthikeyan V          Created
// 04 Aug  06       Vinoth                 Modified for CAB
// 24 Nov 06        guhan                  inclueded configurationWrapper
// 20 Apr 08        Malliga                The New file path is not updated properly (Bug Id : 5999)   
// 11 Jan 10        Malliga                Code modified for the issue 5643 
// 27 July 16       priyadharshini         Wrong value in @FileID and @PFileID variables Bug Fixing
//*********************************************************************************/

namespace D9005
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using System.Configuration;
    using System.IO;
    using System.Diagnostics;
    using TerraScan.UI.Controls;
    using System.Runtime.InteropServices;
    using TerraScan.ReceiptEngine;
    using System.Web.Services.Protocols;
    using TerraScan.Common;
    using System.Security.Permissions;
    using System.Security;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Threading;
    using TerraScan.Utilities;
    using System.Collections;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;

    /// <summary>
    /// F9005 class file
    /// </summary>
    public partial class F9005 : BasePage
    {
        #region Variables

        /// <summary>
        /// Created Readonly String for DateFormat
        /// </summary>
        private readonly string dateFormat = "MM/dd/yyyy";

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        /// Created string for cental File Path
        /// </summary>
        private static string centalFilePath = string.Empty;

        /// <summary>
        /// Created Boolean value for scanFile.
        /// </summary>
        private bool scanFile;

        /// <summary>
        /// Created Integer for Attachment FormID
        /// </summary>
        private int attachmentFormID;

        /// <summary>
        /// Created Integer for recordCount
        /// </summary>
        private int recordCount;

        /// <summary>
        /// this variable used to set postion userdatagrid view 
        /// </summary>
        private int tempRowId;

        /// <summary>
        /// Created Integer for Attachment keyID 
        /// </summary>
        private int attachmentKeyID;

        /// <summary>
        /// Created Boolean value for ValueChanged.
        /// </summary>
        private bool valueChanged;

        /// <summary>
        /// Created Boolean value for dateChanged.
        /// </summary>
        private bool dateChanged;

        /// <summary>
        /// Created Boolean value for ClosingNow.
        /// </summary>
        private bool closingNow;

        /// <summary>
        /// Created Integer for SavedSuccessfully.
        /// </summary>
        private int selected;

        /// <summary>
        /// Created string for textBoxFocused
        /// </summary>
        private string textBoxFocused = string.Empty;

        /// <summary>
        /// Object for dataset created
        /// </summary>
        ////private DataSet attachmentDataSet = new DataSet();

        /// <summary>
        /// Object for attachment typed dataset
        /// </summary>
        private AttachmentsData attachmentDataSet = new AttachmentsData();

        /// <summary>
        /// Object for dataset created
        /// </summary>
        ////private DataSet functionNameDataSet = new DataSet();

        /// <summary>
        /// created Integer to Find Current Row
        /// </summary>
        private int currentRow;

        /// <summary>
        /// Object for DataRow created
        /// </summary>
        private DataRow attachmentDataRow;

        /// <summary>
        /// Object for DataTable created
        /// </summary>
        private DataTable tmpDataTable = new DataTable();

        /// <summary>
        /// Object for OpenfileDialog Created
        /// </summary>
        private System.Windows.Forms.OpenFileDialog browseOpenDialog = new OpenFileDialog();

        /// <summary>
        /// Created int variable to Find Button Operation
        /// </summary>
        private int buttonOperation;

        /// <summary>
        /// Created string for SelectFilePath
        /// </summary>
        private string selectedFilePath = string.Empty;

        /// <summary>
        /// Created string for auditFileID
        /// </summary>
        private string auditFileID = string.Empty;

        /// <summary>
        /// Created string to Find Extension 
        /// </summary>
        private string browsePathExt = string.Empty;

        /// <summary>
        /// Created for Currency Manager
        /// </summary>
        private CurrencyManager currencyManager;

        /// <summary>
        /// Created Boolean value to validData
        /// </summary>
        private bool validData = true;

        /// <summary>
        /// Created Boolean value to editOperation
        /// </summary>
        ////private bool editOperation;

        /// <summary>
        /// Created object for DataGridViewCellStyle
        /// </summary>
        private System.Windows.Forms.DataGridViewCellStyle commentHeader = new System.Windows.Forms.DataGridViewCellStyle();

        /// <summary>
        /// Created object for DataGridViewCellStyle
        /// </summary>
        private System.Windows.Forms.DataGridViewCellStyle commentDefaultCell = new System.Windows.Forms.DataGridViewCellStyle();

        /// <summary>
        /// Created string for Browse Temp Path
        /// </summary>
        private string browseTmpPath = string.Empty;

        /// <summary>
        /// Created string for tempDate
        /// </summary>
        private string tempDate = string.Empty;

        /// <summary>
        /// Created string for tempFileType
        /// </summary>
        private string tempFileType = string.Empty;

        /// <summary>
        /// Created string for tempDescription
        /// </summary>
        private string tempDescription = string.Empty;

        /// <summary>
        /// Created bool for tempPublic
        /// </summary>
        private bool tempPublic;

        /// <summary>
        /// Created bool for tempPrimary
        /// </summary>
        private bool tempPrimary;

        /// <summary>
        /// Created bool for tempRoll
        /// </summary>
        private bool tempRoll;

        /// <summary>
        /// Created Integer to get Selected Row Index.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Created string for Scan Temp Path
        /// </summary>
        private string scanTmpPath = string.Empty;

        /// <summary>
        /// Created string for scanFileExt
        /// </summary>
        private string scanFileExt = string.Empty;

        /// <summary>
        /// Created for DataRow
        /// </summary>
        private DataRow tmpDataRow;

        /// <summary>
        /// Created Integer for Temp FileID
        /// </summary>
        private int tmpFileID;

        /// <summary>
        /// Created string for Temp Path
        /// </summary>
        private string tmpPath = string.Empty;

        /// <summary>
        /// Created Boolean for closeButton
        /// </summary>
        private bool closeButton;

        /// <summary>
        /// Created Boolean for browseSelected
        /// </summary>
        private bool browseSelected;

        /// <summary>
        /// Created Boolean for scanSelected
        /// </summary>
        private bool scanSelected;

        /// <summary>
        /// Created string for filePath
        /// </summary>
        private string filePath = string.Empty;

        /// <summary>
        /// Created string for fileID
        /// </summary>
        private string fileID = string.Empty;

        /// <summary>
        /// Created for AttachmentScanning
        /// </summary>      
        ////private F9006 scanForm;

        /// <summary>
        /// Created Boolean to Find emptyRecord
        /// </summary>        
        private bool emptyRecord;

        /// <summary>
        /// to check file Exist
        /// </summary>        
        private bool fileExist;

        /// <summary>
        /// used to store tem image
        /// </summary>
        private DataTable tmpImageTable = new DataTable();

        /// <summary>
        /// to assign  permission
        /// </summary>
        private bool validPermission;

        /// <summary>
        /// used to store tem image
        /// </summary>
        private DataTable tempSourcePath = new DataTable();

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        ////private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// Created Controller for F9015Controller
        /// </summary>
        private F9005Controller form9005Control;

        /// <summary>
        /// F9006 object
        /// </summary>
        ////F9006 form9006;

        /// <summary>
        /// used to strore attachment Count
        /// </summary>
        private int attachmentCount;

        /// <summary>
        /// localFilePath
        /// </summary>
        private string localFilePath;

        /// <summary>
        /// linktypeid
        /// </summary>
        private int linktypeid;

        /// <summary>
        /// Created string to Find Extension 
        /// </summary>
        private string fileurlpath = string.Empty;

        private static string validTempPath = string.Empty;

        /// <summary>
        /// To Get Rowindex
        /// </summary>
        private int rowIndex;

        string filePathDelete = string.Empty;

        private bool textchange = false;

        /// <summary>
        /// Created string for Save file path
        /// </summary>
        private string saveFilePath = string.Empty;

        /// <summary>
        /// flagFile
        /// </summary>
        bool flagFile = true;


        ///<summary>
        /// Identified the row no for replacing the socument in attachment form.
        /// </summary>
        private int RowNo;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AttachmentForm"/> class.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="parentFormId">The parent form id.</param>
        public F9005(int formId, int keyId, int parentFormId)
        {
            this.InitializeComponent();

            // Assign the FormID and KeyID to local variable.
            this.attachmentFormID = formId;
            this.attachmentKeyID = keyId;
            ////this.Tag = parentFormId;

            // Get the default values during form load.
            this.GetValue();

            // Used to store the temporary Browse file path.
            this.CreateTmpDataTable();
            ////this.scanForm = new F9006();
            this.CancelButton = this.cancelAttachmentButton;

            // Uset to store the temporary deleted file path.
            this.CreateSourceDataTable();

            // TerraScan.Common.TerraScanCommon.SetSameProperty(this.attachmentGridView, this.attachmentGridViewEmpty, 5);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region  for Enum ProgramType

        /// <summary>
        /// ProgramType
        /// </summary>
        public enum ProgramType
        {
            /// <summary>
            /// OpenEmpty = 0.
            /// </summary>
            OpenEmpty = 0,

            /// <summary>
            /// OpenWithOS = 1.
            /// </summary>
            OpenWithOS = 1,

            /// <summary>
            /// Aspx = 2.
            /// </summary>
            OpenWithAspx = 2,

            /// <summary>
            /// Exe = 3.
            /// </summary>
            OpenWithExe = 3,

            /// <summary>
            /// Decimal = 4.
            /// </summary>
            OpenWithFormId = 4
        }

        /// <summary>
        /// Button Functionality
        /// </summary>
        public enum ButtonOperation
        {
            /// <summary>
            /// Empty = 0.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// New = 1.
            /// </summary>
            New = 1,

            /// <summary>
            /// Save = 2.
            /// </summary>
            Save = 2,

            /// <summary>
            /// Delete = 3.
            /// </summary>
            Delete = 3,

            /// <summary>
            /// Cancel = 4.
            /// </summary>
            Cancel = 4,

            /// <summary>
            /// Scan = 5.
            /// </summary>
            Scan = 5,

            /// <summary>
            /// Browse = 6.
            /// </summary>
            Browse = 6,

            /// <summary>
            /// AttachmentGrid = 7.
            /// </summary>
            AttachmentGrid = 7,

            /// <summary>
            /// EmptyGrid = 8.
            /// </summary>
            EmptyGrid = 8
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cental file path.
        /// </summary>
        /// <value>The cental file path.</value>
        public static string CentalFilePath
        {
            get { return centalFilePath; }
            set { centalFilePath = value; }
        }

        /// <summary>
        /// Gets or sets F9005Control
        /// </summary>
        [CreateNew]
        public F9005Controller F9005Control
        {
            get { return this.form9005Control as F9005Controller; }
            set { this.form9005Control = value; }
        }

        /// <summary>
        /// Gets or sets the attachment count.
        /// </summary>
        /// <value>The attachment count.</value>
        public int AttachmentCount
        {
            get { return this.attachmentCount; }
            set { this.attachmentCount = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// the Form Permissions will be captured and Set the Button Action Modes
        /// </summary>
        /// <param name="e">OnLoad Even Args</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ////this.DeleteScanFile();

            try
            {
                //// Gets the Funtion Name from Database
                ////this.functionNameDataSet = F9005WorkItem.GetAttachementFunctionName(this.attachmentFormID);
                this.attachmentDataSet = this.form9005Control.WorkItem.GetAttachementFunctionName(this.attachmentFormID);

                if (this.attachmentDataSet.GetAttachementFunctionName.Rows.Count > 0)
                {
                    // Loads the Data to Combo box.
                    this.typeComboBox.DataSource = this.attachmentDataSet.GetAttachementFunctionName;
                    this.typeComboBox.DisplayMember = "FunctionName";
                    this.typeComboBox.ValueMember = "FileTypeID";
                    this.typeComboBox.SelectedIndex = 0;
                    this.fileTypeIDTextBox.Text = this.typeComboBox.SelectedValue.ToString();
                }

                // Loads the Data from Database
                try
                {
                    this.LoadAttachmentGrid();
                }
                catch (Exception ex)
                {
                }

                // this.attachmentReceiptIDLabel.Text = "Attachments for " + this.attachmentFormID + ": " + this.attachmentKeyID;

                /* if (this.attachmentKeyID > 0)
                 {
                     this.attachmentReceiptIDLabel.Text = "Attachments for Permit: " + this.attachmentFormID + "-" + this.attachmentKeyID;
                 }
                 else
                 {
                     this.attachmentReceiptIDLabel.Text = "Attachments for Permit: " + this.attachmentFormID;
                 }*/

                /*  // Used to display the Attachment recipt or statement ID.
                if (this.attachmentFormID == 1000)
                {
                    this.attachmentReceiptIDLabel.Text = "Attachments for Receipt : " + this.attachmentKeyID;
                }
                else if (this.attachmentFormID == 1020)
                {
                    this.attachmentReceiptIDLabel.Text = "Attachments for Statement : " + this.attachmentKeyID;
                }
                else if (this.attachmentFormID == 1010)
                {
                    this.attachmentReceiptIDLabel.Text = "Attachments for Mortgage Import : " + this.attachmentKeyID;
                } */

                this.SelectOpenFilePath();
                this.attachmentMonthCalander.Visible = false;
                this.InitializeButton();
                this.SetDataGridViewPosition(0);

                if (this.recordCount > 0)
                {
                    this.SetDataBindingValue(0);
                    this.CheckEditPermission();
                    this.auditLinkLabel.Text = "tTS_File[FileID] " + this.AttachmentGridView.Rows[0].Cells["AttachmentFileID"].Value;
                }
                else
                {
                    this.AttachmentGridView.Rows[0].Selected = false;
                }

                // Checks datagrid have record or not to disable the delete button.
                if (this.AttachmentGridView.Rows.Count <= 0)
                {
                    this.SetCurrentFormButtons(ButtonOperation.Cancel);
                    this.ClearHeader();
                    this.DisableAttachmentControl();

                    if (this.deleteAttachmentButton.ActualPermission == true)
                    {

                        this.deleteAttachmentButton.Enabled = false;

                    }
                }

                this.AttachmentGridView.Focus();
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
        /// Used to check the permission for a particular Directory.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns>
        /// 	<c>true</c> if [has permissions on network] [the specified filepath]; otherwise, <c>false</c>.
        /// </returns>
        private static bool HasPermissionsOnNetwork(string filepath)
        {
            // This Commented Code will be used later.

            /* CodeAccessPermission userPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, filepath);
            WindowsIdentity identity = (WindowsIdentity)principal.Identity;
            bool flagIsAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            WindowsPrincipal principal = (WindowsPrincipal)Thread.CurrentPrincipal;
            AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
            FileIOPermission filePermission = new FileIOPermission(FileIOPermissionAccess.PathDiscovery, filepath);
            filePermission.Demand(); */

            // Checks wheather directory exists or not.
            DirectoryInfo dinfo = new DirectoryInfo(filepath);
            try
            {
                if (dinfo.Exists)
                {
                    DirectorySecurity directorySecurity = dinfo.GetAccessControl();
                    directorySecurity.GetType();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to create a empty row in a Datagrid.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>returns datatable</returns>
        private static DataTable CreateEmptyRows(DataTable sourceDataTable, int maxRowCount)
        {
            int defaultRowsCount = 0;
            DataRow tempRow;

            if (sourceDataTable.Rows.Count < maxRowCount)
            {
                defaultRowsCount = maxRowCount - sourceDataTable.Rows.Count;

                for (int i = 0; i < defaultRowsCount; i++)
                {
                    tempRow = sourceDataTable.NewRow();
                    for (int j = 0; j < sourceDataTable.Columns.Count; j++)
                    {
                        if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int32")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int16")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Boolean")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Byte")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else
                        {
                            tempRow[j] = string.Empty;
                        }
                    }

                    sourceDataTable.Rows.Add(tempRow);
                }
            }

            return sourceDataTable;
        }

        /// <summary>
        /// Opens the file according to the Program type.
        /// </summary>
        /// <param name="programID"> The id for accesing the program.</param>
        /// <param name="path"> The path of the file.</param>
        /// <param name="program"> The name of the program to access the file</param>
        private static void OpenFile(int programID, string path, string program)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            // Used To Check User has Permission to view the file
            int startIndex = path.LastIndexOf("\\");
            if (WSHelper.IsOnLineMode.Equals(true))
            {

                // Used to Check  File Exist or Not
                if (HasPermissionsOnNetwork(path.Substring(0, startIndex + 1)))
                {
                    if (System.IO.File.Exists(path))
                    {
                        FileOpenFunction(programID, path, program);
                    }
                    else
                    {
                        //// ShowMessage.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", GetDefaultPath(path), "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ////To get File Extension
                        ////if (!string.IsNullOrEmpty(path))
                        ////{
                        ////    string ext = path.Substring(path.LastIndexOf(".")).ToString();
                        ////    if (!string.IsNullOrEmpty(ext))
                        ////    {
                        ////        int len = ext.Length;
                        ////        if (len > 4)
                        ////        {
                        ////            DateTime filecreationtime = System.IO.File.GetCreationTime(path);
                        ////            string dateconvert = filecreationtime.ToString();
                        ////            if (!string.IsNullOrEmpty(dateconvert))
                        ////            {
                        ////                //string newpath = path.Remove(path.Trim().LastIndexOf("."));
                        ////                //ext = ext.Remove(4); 
                        ////                //path = newpath + ext;

                        ////                FileOpenFunction(programID, path, program);
                        ////            }
                        ////            else
                        ////            {
                        ////                ErrorEngine.ShowForm(4);
                        ////            }
                        ////        }
                        ////    }
                        ////}
                        ErrorEngine.ShowForm(4);
                    }
                }
                else
                {
                    // ShowMessage.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("File"), "\n", path, "\n", SharedFunctions.GetResourceString("DoesnotExists"), "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("FileMissing"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ErrorEngine.ShowForm(3);
                }
            }
            else
                if (WSHelper.IsOnLineMode.Equals(false) && TerraScanCommon.IsFieldUser)
                {
                    if (System.IO.File.Exists(path))
                    {
                        FileOpenFunction(programID, path, program);
                    }
                    else
                    {
                        ErrorEngine.ShowForm(4);
                    }
                }
        }

        private static void FileOpenFunction(int programID, string path, string program)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            if (programID == (int)ProgramType.OpenWithOS)
            {
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = path;
                try
                {
                    // Used to open the application as per the extension.
                    proc.Start();
                }
                catch
                {
                    // Used to Show the OpenWith Dialog window.
                    OpenWith.OpenAs(path);
                }
                finally
                {
                    proc.Close();
                }
            }
            else if (programID == (int)ProgramType.OpenWithAspx)
            {
                program = program + "?CurrentFile=" + path;
                try
                {
                    // Used to open the image in Internet Explorer.
                    Process.Start("IEXPLORE.EXE", program);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // TODO
                    // proc.Close();
                }
            }
            else if (programID == (int)ProgramType.OpenWithExe)
            {
                if (System.IO.File.Exists(program))
                {
                    try
                    {
                        Process.Start(program, path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        // TODO

                    }
                }
                else
                {
                    ErrorEngine.ShowForm(5);
                }
            }
            //////Commented to implement TFS#20236
            //// System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //if (!string.IsNullOrEmpty(path.Trim()))
            //{
            //    validTempPath = TerraScanCommon.validatePathField(path.Trim());

            //}
            //if (!string.IsNullOrEmpty(validTempPath))
            //{
            //    if (programID == (int)ProgramType.OpenWithOS)
            //    {
            //        ////Commented to implement TFS#20236
            //        //proc.EnableRaisingEvents = false;
            //        //proc.StartInfo.FileName = validTempPath;
            //        try
            //        {
            //            SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorerClass();
            //            IWebBrowserApp wb = (IWebBrowserApp)ie;
            //            wb.Visible = true;
            //            object o = null;
            //            //wb.FullScreen = true;
            //            wb.Navigate(validTempPath, ref o, ref o, ref o, ref o);

            //            ////Commented to implement TFS#20236
            //            // Used to open the application as per the extension.
            //            //proc.Start();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Used to Show the OpenWith Dialog window.
            //            MessageBox.Show(ex.Message, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        finally
            //        {
            //            // proc.Close();
            //        }
            //    }
            //    else if (programID == (int)ProgramType.OpenWithAspx)
            //    {
            //        program = program + "?CurrentFile=" + validTempPath;
            //        try
            //        {

            //            SHDocVw.InternetExplorer ieObj = new SHDocVw.InternetExplorerClass();
            //            IWebBrowserApp wb = (IWebBrowserApp)ieObj;
            //            wb.Visible = true;
            //            object o = null;
            //            //wb.FullScreen = true;
            //            wb.Navigate(validTempPath, ref o, ref o, ref o, ref o);

            //            ////Commented to implement TFS#20236
            //            // Used to open the image in Internet Explorer.
            //            // Process.Start("IEXPLORE.EXE", program);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        finally
            //        {
            //            // TODO
            //            // proc.Close();
            //        }
            //    }
            //    else if (programID == (int)ProgramType.OpenWithExe)
            //    {
            //        if (System.IO.File.Exists(program))
            //        {
            //            try
            //            {
            //                SHDocVw.InternetExplorer ieObject = new SHDocVw.InternetExplorerClass();
            //                IWebBrowserApp wb = (IWebBrowserApp)ieObject;
            //                wb.Visible = true;
            //                object o = null;
            //                // wb.FullScreen = true;
            //                wb.Navigate(validTempPath, ref o, ref o, ref o, ref o);

            //                ////Commented to implement TFS#20236
            //                //Process.Start(program, validTempPath);
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //            finally
            //            {
            //                // TODO
            //            }
            //        }
            //        else
            //        {
            //            ErrorEngine.ShowForm(5);
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("An input parameter is invalid", "Terrascan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

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
        /// Check wheather the Datatable is empty or Not.
        /// </summary>
        /// <param name="attmntDataSet"> The dataset to check for table.</param>
        /// <returns> flag to indicate the presence of the table.</returns>
        private static bool VaildDataSet(DataSet attmntDataSet)
        {
            // Used to check whether Dataset is null or not.
            if (attmntDataSet != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  Gets the default path .
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>  the Default path  </returns>
        private static string GetDefaultPath(string fileName)
        {
            if (fileName.IndexOf("\\\\") == 0)
            {
                return fileName.Substring(0, fileName.LastIndexOf("\\\\"));
            }
            else
            {
                return fileName.Substring(0, fileName.LastIndexOf("\\"));
            }
        }

        /* /// <summary>
        /// Deletes the scan file.
        /// </summary>
        private void DeleteScanFile()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");

            if (!dirInfo.Exists)
            {
                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");
                ////centalFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "FinalImage" + DateTime.Now.Ticks + ".tif";
            }
            else
            {
                FileInfo[] fileList = dirInfo.GetFiles();

                if (dirInfo.Exists)
                {
                    foreach (FileInfo file in fileList)
                    {
                        if (file.Name != "Thumbs.db")
                        {
                            System.IO.File.Delete(file.FullName);
                        }
                    }
                }
                else
                {
                    ////Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");
                    ////centalFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "FinalImage.tif";
                }
            }
        } */

        /// <summary>
        /// Creates the temporary data table to store the file path. 
        /// </summary>
        private void CreateTmpDataTable()
        {
            DataColumn dataColumnFileId = new DataColumn("FileID");
            DataColumn dataColumnPath = new DataColumn("Path");
            DataColumn dataColumnDeletePath = new DataColumn("DeletePath");
            this.tmpDataTable.Columns.Add(dataColumnFileId);
            this.tmpDataTable.Columns.Add(dataColumnPath);
            this.tmpDataTable.Columns.Add(dataColumnDeletePath);
        }

        /// <summary>
        /// Clears the header fields.
        /// </summary>
        private void ClearHeader()
        {
            this.attachmentDateTextBox.Text = string.Empty;
            this.typeComboBox.Text = null;
            this.publicCheckBox.Checked = false;
            this.primaryCheckBox.Checked = false;
            //this.WillRollCheckBox.Checked = false;
            this.descriptionTextBox.Text = string.Empty;
            this.userTextBox.Text = string.Empty;
            this.fileTypeIDTextBox.Text = string.Empty;
            this.fileTextBox.Text = string.Empty;
            this.SourceTextBox.Text = string.Empty;
            this.linktypeTextBox.Text = string.Empty;
            this.auditLinkLabel.Text = "tTS_File[FileID]";
        }

        /// <summary>
        /// Sets the cancel button.
        /// </summary>
        private void SetCancelButton()
        {
            if (this.cancelAttachmentButton.Enabled == false)
            {
                this.CancelButton = this.CloseAttachmentButton;
            }
            else
            {
                this.CancelButton = this.cancelAttachmentButton;
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>Return RowIndex</returns>
        private int GetRowIndex()
        {
            if (this.attachmentDataSet.GetAttachmentItems.Rows.Count > 0)
            {
                if (this.AttachmentGridView.SelectedRows.Count > 0)
                {
                    this.selected = this.AttachmentGridView.SelectedRows[0].Index;
                }
                else if (this.AttachmentGridView.SelectedCells.Count > 0)
                {
                    this.selected = this.AttachmentGridView.CurrentCell.RowIndex;
                    this.selected = this.selectedRow;
                }
            }

            return this.selected;
        }

        /// <summary>
        /// Selects the file.
        /// </summary>
        private void SelectFile()
        {
            if (this.buttonOperation != (int)ButtonOperation.New)
            {
                this.tmpPath = this.GetSelectedFilePath();

                #region Wrong value in @FileID and @PFileID variables Bug
                this.RowNo = this.AttachmentGridView.CurrentRowIndex;
                if (this.attachmentDataSet.GetAttachmentItems.Rows.Count > 0)
                {
                    DataRow[] dr = this.attachmentDataSet.GetAttachmentItems.Select("FileID='" + Convert.ToInt32(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString()) + "'");
                    if (dr.Length > 0)
                    {
                        dr[0][18] = Convert.ToInt32(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString());//Datarow is reference to datatable it will automatically update the datatable values
                    }
                    this.attachmentDataSet.GetAttachmentItems.AcceptChanges();
                }
                if (this.recordCount > 0)
                {
                    this.tmpFileID = Convert.ToInt32(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString());
                }
                #endregion

                if (this.tmpPath.Length > 0)
                {
                    if (System.IO.File.Exists(this.tmpPath))
                    {
                        //// this.valueChanged = true;
                        //// AlertForm tmpAlert = new AlertForm("Terrascan : File Replace", "There is already an associated file for this attachment.", "This action will replace that file.");
                        if (MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("AttachmentFileMissing"), "\n", SharedFunctions.GetResourceString("AttachmentFileMissing1") }), SharedFunctions.GetResourceString("AttachmentFileMissing2"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            if (this.scanFile)
                            {
                                this.SetCurrentFormButtons(ButtonOperation.Scan);
                            }
                            else
                            {
                                this.SetCurrentFormButtons(ButtonOperation.Browse);
                            }

                            SetButtons(this, (int)TerraScanCommon.ButtonActionMode.NewMode);
                            ////this.browseButton.BackColor = Color.FromArgb(71, 133, 85);
                            ////this.browseButton.StatusOffColor = Color.FromArgb(71, 133, 85);
                            ////this.URLbutton.BackColor = Color.FromArgb(71, 133, 85);
                            ////this.URLbutton.StatusOffColor = Color.FromArgb(71, 133, 85);
                            ////this.scanButton.BackColor = Color.FromArgb(71, 133, 85);
                            ////this.scanButton.StatusOffColor = Color.FromArgb(71, 133, 85); 

                            this.browseSelected = true;
                            //// call the method to browse a file.
                            this.BrowseFilePath();
                        }
                        else
                        {
                            //// SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                            //// this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);

                            if (!this.valueChanged)
                            {
                                this.InitializeButton();
                            }

                            this.SetFocus();

                            // call the method to browse a file.
                            this.browseSelected = false;

                            // this.valueChanged = false;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("FileMissing"), "\n", SharedFunctions.GetResourceString("Replace") }), SharedFunctions.GetResourceString("FileMissing"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            SetButtons(this, (int)TerraScanCommon.ButtonActionMode.NewMode);
                            this.SetCurrentFormButtons(ButtonOperation.Browse);
                            this.browseSelected = true;

                            // call the method to browse a file.
                            this.BrowseFilePath();
                        }
                        else
                        {
                            // SetButtons(this, (int)TerraScanCommon.ButtonActionMode.EditMode);
                            // this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);

                            if (!this.valueChanged)
                            {
                                this.InitializeButton();
                            }

                            this.SetFocus();

                            // call the method to browse a file.
                            this.browseSelected = false;

                            // this.valueChanged = false;
                        }
                    }
                }
            }
            else
            {
                SetButtons(this, (int)TerraScanCommon.ButtonActionMode.NewMode);
                if (this.scanFile)
                {
                    this.SetCurrentFormButtons(ButtonOperation.Scan);
                }
                else
                {
                    this.SetCurrentFormButtons(ButtonOperation.Browse);
                }

                this.browseSelected = true;

                // call the method to browse a file.
                this.BrowseFilePath();
            }
        }

        /// <summary>
        /// Saves the records.
        /// </summary>
        private void SaveRecords()
        {
            // Save the Entry value in attachment and save the image to central location.
            if (this.RequiredField())
            {
                if (this.CheckDate())
                {

                    int foundIndex = 0;
                    string dateFileId = string.Empty;

                    if (this.recordCount == 0)
                    {
                        this.EnableNonEditableControl();
                    }

                    if (!this.emptyRecord)
                    {
                        int row = this.GetRowIndex();
                        dateFileId = this.AttachmentGridView.Rows[row].Cells["AttachmentFileID"].Value.ToString();
                    }

                    if (this.buttonOperation == (int)ButtonOperation.New)
                    {
                        ////-- start page
                        if (this.scanFile || this.tmpDataTable.Rows.Count > 0)
                        {
                            if (VaildDataSet(this.attachmentDataSet))
                            {
                                try
                                {
                                    this.Cursor = Cursors.WaitCursor;
                                    //// this.attachmentDataSet = F9005WorkItem.GetFilePath("TSFile", this.attachmentFormID, this.attachmentKeyID, this.browsePathExt);
                                    this.attachmentDataSet.GetFilePath.Clear();
                                    // code commented for CO
                                    /*if (!this.scanFile)
                                    {
                                        this.attachmentDataSet.GetFilePath.Merge(this.form9005Control.WorkItem.GetFilePath("TSFile", this.attachmentFormID, this.attachmentKeyID, this.browsePathExt, TerraScanCommon.UserId));
                                    }
                                    else
                                    {
                                        this.attachmentDataSet.GetFilePath.Merge(this.form9005Control.WorkItem.GetFilePath("TSFile", this.attachmentFormID, this.attachmentKeyID, this.scanFileExt, TerraScanCommon.UserId));
                                    }

                                    this.filePath = this.attachmentDataSet.GetFilePath.Rows[0]["FilePath"].ToString();
                                    this.fileID = this.attachmentDataSet.GetFilePath.Rows[0]["FileID"].ToString();
                                    this.currentRow = 0;
                                    ////Added on 8/5/2008
                                    this.fileurlpath = this.filePath;
                                    //// this.attachmentDataRow = this.attachmentDataSet.Tables[0].NewRow();
                                    //// this.FillDataSet();                                
                                    //// save the image to central location.
                                    this.SaveImage();*/

                                    //if (this.fileExist && this.validPermission)
                                    //{
                                    //// save the entry value to database.
                                    this.SaveAttachmentEntry();
                                    // If condition and else portion has been removed for grid focus issue fixing
                                    //if (this.dateChanged)
                                    //{
                                    ////string ss = this.attachmentGridView.Columns["AttachmentFileID"].Index.ToString();
                                    BindingSource source = new BindingSource();
                                    source.DataSource = this.attachmentDataSet.GetAttachmentItems;
                                    foundIndex = source.Find("FileID", this.fileID);
                                    this.SetDataGridViewPosition(foundIndex);
                                    this.SetDataBindingValue(foundIndex);
                                    //}
                                    //else
                                    //{
                                    //    this.SetDataGridViewPosition(0);
                                    //    this.SetDataBindingValue(0);
                                    //}
                                    this.SaveImage();
                                    //// Gets the value for auditLinkLabel.
                                    this.auditLinkLabel.Text = "tTS_File[FileID] " + this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString();
                                    ////this.scanForm.SuccesfullyScanned = false;
                                    this.scanFile = false;
                                    this.closingNow = true;
                                    this.dateChanged = false;


                                    int fileNewID = 0;
                                    int.TryParse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString(), out fileNewID);
                                    this.form9005Control.WorkItem.GenerateThumbnail(fileNewID, TerraScanCommon.UserId, null);

                                    //// Set the Button Mode after saving the Record.
                                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.SaveMode);
                                    if (TerraScanCommon.IsFieldUser)
                                    {
                                        this.deleteAttachmentButton.Enabled = false;
                                    }
                                    this.SetCurrentFormButtons(ButtonOperation.Save);
                                    this.buttonOperation = (int)ButtonOperation.Empty;
                                    this.descriptionTextBox.BackColor = System.Drawing.Color.White;
                                    this.localFilePath = string.Empty;
                                    this.SourceTextBox.ForeColor = System.Drawing.Color.FromArgb((int)(byte)(102), (int)(byte)(102), (int)(byte)(102));
                                    //}
                                    //else if (this.validPermission)
                                    //{
                                    //    //// shows the error message through Error form.
                                    //    //// ErrorForm errorForm = new ErrorForm(SharedFunctions.GetResourceString("FileMissing"), SharedFunctions.GetResourceString("FileNotExists"), SharedFunctions.GetResourceString("UnabletoSave"), "", string.Empty);
                                    //    //// errorForm.ShowDialog();

                                    //    MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("FileNotExists"), "\n", SharedFunctions.GetResourceString("UnabletoSave") }), SharedFunctions.GetResourceString("FileMissing"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    this.browseButton.ImageSelected = false;
                                    //    this.scanButton.ImageSelected = false;
                                    //    this.scanFile = false;
                                    //    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.SaveMode);
                                    //    this.SetCurrentFormButtons(ButtonOperation.Cancel);
                                    //}

                                    this.auditLinkLabel.Enabled = true;
                                }
                                catch (IOException ioException)
                                {
                                    ErrorEngine.ShowForm(3);
                                    // Update the isdeleted column as 'True'
                                    if (this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value != null)
                                    {
                                        this.form9005Control.WorkItem.DeleteAttachments(int.Parse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString()), TerraScanCommon.UserId);
                                        DataRow[] removeRow = this.attachmentDataSet.GetAttachmentItems.Select("FileID =" + this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString());
                                        if (removeRow.Length > 0)
                                        {
                                            this.attachmentDataSet.GetAttachmentItems.Rows.Remove(removeRow[0]);
                                        }

                                        this.AttachmentGridView.DataSource = this.attachmentDataSet.GetAttachmentItems.DefaultView;
                                    }

                                    this.attachmentCount = this.AttachmentGridView.OriginalRowCount;
                                    this.buttonOperation = (int)ButtonOperation.Cancel;
                                    this.valueChanged = false;
                                    this.Close();

                                    ////MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("ErrorAccessingPath"), "\n", SharedFunctions.GetResourceString("UnabletoSave") }), SharedFunctions.GetResourceString("ErrorOccured"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.validPermission = false;
                                    this.browseButton.ImageSelected = false;
                                    this.scanButton.ImageSelected = false;
                                    this.scanFile = false;
                                    this.valueChanged = false;

                                    if (!this.emptyRecord)
                                    {
                                        if (this.buttonOperation == (int)ButtonOperation.New)
                                        {
                                            this.SetDataBindingValue(0);
                                            this.SetDataGridViewPosition(0);
                                        }
                                        else
                                        {
                                            int rowIndex = 0;
                                            rowIndex = this.GetRowIndex();
                                            this.SetDataBindingValue(rowIndex);
                                            this.SetDataGridViewPosition(rowIndex);
                                        }
                                    }
                                    else
                                    {
                                        this.ClearHeader();
                                        this.DisableAttachmentControl();
                                    }

                                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.CancelMode);
                                    this.SetCurrentFormButtons(ButtonOperation.Cancel);
                                    this.buttonOperation = (int)ButtonOperation.Empty;
                                    this.AttachmentGridView.Enabled = true;
                                }
                                catch (SoapException ex)
                                {
                                    ////TODO : Need to find specific exception and handle it.
                                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                                }
                                catch (UnauthorizedAccessException)
                                {
                                    ErrorEngine.ShowForm(3);
                                    // Update the isdeleted column as 'True'
                                    if (this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value != null)
                                    {
                                        this.form9005Control.WorkItem.DeleteAttachments(int.Parse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString()), TerraScanCommon.UserId);
                                        DataRow[] removeRow = this.attachmentDataSet.GetAttachmentItems.Select("FileID =" + this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString());
                                        if (removeRow.Length > 0)
                                        {
                                            this.attachmentDataSet.GetAttachmentItems.Rows.Remove(removeRow[0]);
                                        }

                                        this.AttachmentGridView.DataSource = this.attachmentDataSet.GetAttachmentItems.DefaultView;
                                    }

                                    this.attachmentCount = this.AttachmentGridView.OriginalRowCount;
                                    this.buttonOperation = (int)ButtonOperation.Cancel;
                                    this.valueChanged = false;
                                    this.Close();
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                                }
                                finally
                                {
                                    this.Cursor = Cursors.Default;
                                }
                            }
                        }
                        else
                        {
                            // shows the error message through Error form.
                            // ErrorForm errorForm = new ErrorForm(SharedFunctions.GetResourceString("NoBrowseorScan"), string.Empty, SharedFunctions.GetResourceString("NoSelectedFile"), SharedFunctions.GetResourceString("SelectFile"), string.Empty);
                            // errorForm.ShowDialog();
                            // ok

                            MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoSelectedFile"), "\n", SharedFunctions.GetResourceString("SelectFile") }), SharedFunctions.GetResourceString("NoBrowseorScan"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.descriptionTextBox.Focus();
                            this.closingNow = false;
                        }
                    }
                    else if (string.Compare(this.fileTextBox.Text.Trim().ToString(), string.Empty) != 0)
                    {
                        /* int endIndex2;
                        endIndex2 = (this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().Length - this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().LastIndexOf("."));
                        int start2;
                        start2 = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().LastIndexOf(".");

                        int endIndex;
                        endIndex = (this.browseTmpPath.Length - this.browseTmpPath.LastIndexOf("."));
                        string tmp = string.Empty;
                        tmp = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().Substring(start2, endIndex2);
                        this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().Replace(tmp, this.browseTmpPath.Substring(this.browseTmpPath.LastIndexOf("."), endIndex)); */

                        try
                        {
                            int currentSelectedRow = 0;
                            currentSelectedRow = this.GetRowIndex();
                            this.saveFilePath = string.Empty;
                            // save the image to central location.

                            if (System.IO.File.Exists(this.scanTmpPath) || System.IO.File.Exists(this.browseTmpPath))
                            {
                                this.SaveImage();
                            }
                            else
                            {
                                if (System.IO.File.Exists(this.fileTextBox.Text))
                                {
                                    this.fileExist = true;
                                    this.validPermission = true;
                                }
                                else
                                {
                                    int fileOriginalID = 0;
                                    ////int.TryParse(this.attachmentDataSet.GetAttachmentItems.Rows[rowIndex][this.attachmentDataSet.GetAttachmentItems.FileIDColumn.ColumnName].ToString(), out fileOriginalID);
                                    int.TryParse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString(), out fileOriginalID);
                                    this.saveFilePath = this.form9005Control.WorkItem.GetOriginalFilePath(fileOriginalID, TerraScanCommon.UserId);

                                    if (System.IO.File.Exists(this.saveFilePath))
                                    {
                                        this.fileExist = true;
                                        this.validPermission = true;
                                    }
                                    else
                                    {
                                        //if (System.IO.File.Exists(this.SourceTextBox.Text.Trim()))
                                        //{
                                        //    this.fileExist = true;
                                        //    this.validPermission = true;
                                        //}
                                        //else
                                        //{
                                        // this.fileExist = false;
                                        this.validPermission = false;
                                        MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("FileNotExists"), "\n", SharedFunctions.GetResourceString("UnabletoSave") }), SharedFunctions.GetResourceString("FileMissing"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //}
                                    }
                                }
                            }

                            if (this.validPermission)
                            {
                                if (this.fileExist)
                                {
                                    //// Accept the Chages mode in the datagirdview.

                                    // this.attachmentDataSet.Tables[0].AcceptChanges();

                                    ////Coding Added for the Issue 5999 by Malliga on 20/4/2009
                                    if (string.IsNullOrEmpty(this.saveFilePath))
                                    {
                                        string destinationPath = "TSFile" + TerraScanCommon.ApplicationId.ToString();
                                        CommentsData fileattachment = new CommentsData();
                                        fileattachment = this.form9005Control.WorkItem.GetConfigDetails(destinationPath);
                                        if (fileattachment.GetCommentsConfigDetails.Rows.Count > 0)
                                        {
                                            string currentcountyconfig = fileattachment.GetCommentsConfigDetails.Rows[0]["ConfigurationValue"].ToString();
                                            // Code commented for testing
                                            //string[] currentcountyconfigarray = currentcountyconfig.Split('\\');
                                            //string[] oldcounty = this.fileTextBox.Text.Split('\\');
                                            //oldcounty[2] = currentcountyconfigarray[2];
                                            //oldcounty[3] = currentcountyconfigarray[3];
                                            //this.fileurlpath = String.Join("\\", oldcounty);

                                            if (!string.IsNullOrEmpty(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["FormID"].Value.ToString().Trim())
                                                && !string.IsNullOrEmpty(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString().Trim())
                                                && !string.IsNullOrEmpty(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["File"].Value.ToString().Trim()))
                                            {
                                                if (!currentcountyconfig.EndsWith(@"\"))
                                                {
                                                    currentcountyconfig = currentcountyconfig + @"\";
                                                }

                                                currentcountyconfig = currentcountyconfig
                                                    + this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["FormID"].Value.ToString().Trim() + @"\"
                                                    + this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString().Trim().Substring(0, 3) + @"\"
                                                    + this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString().Trim().Substring(3, this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString().Length - 3) + @"\"
                                                    + this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["File"].Value.ToString().Trim();
                                                this.fileurlpath = currentcountyconfig;
                                            }
                                            else
                                            {
                                                string[] currentcountyconfigarray = currentcountyconfig.Split('\\');
                                                string[] oldcounty = this.fileTextBox.Text.Split('\\');
                                                oldcounty[2] = currentcountyconfigarray[2];
                                                oldcounty[3] = currentcountyconfigarray[3];
                                                this.fileurlpath = String.Join("\\", oldcounty);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.fileurlpath = this.saveFilePath;
                                    }

                                    bool hasUploadCompleted = true;
                                    if (this.fileurlpath != this.fileTextBox.Text && string.IsNullOrEmpty(this.scanTmpPath))
                                    {
                                       
                                        if (!string.IsNullOrEmpty(this.SourceTextBox.Text))
                                        {
                                            string fileExtension = this.SourceTextBox.Text.Substring(this.SourceTextBox.Text.LastIndexOf(".")).ToString();
                                            this.fileurlpath = this.fileurlpath.Remove(this.fileurlpath.LastIndexOf("."));
                                            this.fileurlpath = this.fileurlpath + fileExtension;
                                        }
                                        ////Ends Here
                                        ////this.attachmentDataSet.GetFilePath.Clear();
                                        ////if (!this.scanFile)
                                        ////{
                                        ////    ////this.browsePathExt = this.SourceTextBox.Text
                                        ////    if (!string.IsNullOrEmpty(this.SourceTextBox.Text))
                                        ////    {
                                        ////        this.browsePathExt = this.SourceTextBox.Text.Substring(this.SourceTextBox.Text.LastIndexOf(".")).ToString();
                                        ////    }
                                        ////    this.attachmentDataSet.GetFilePath.Merge(this.form9005Control.WorkItem.GetFilePath("TSFile", this.attachmentFormID, this.attachmentKeyID, this.browsePathExt, TerraScanCommon.UserId));
                                        ////}
                                        ////else
                                        ////{
                                        ////    this.scanFileExt = "tiff";
                                        ////    this.attachmentDataSet.GetFilePath.Merge(this.form9005Control.WorkItem.GetFilePath("TSFile", this.attachmentFormID, this.attachmentKeyID, this.scanFileExt, TerraScanCommon.UserId));
                                        ////}

                                        ////this.filePath = this.attachmentDataSet.GetFilePath.Rows[0]["FilePath"].ToString();
                                        ////this.fileurlpath = this.filePath;

                                        // Code commented to test
                                        // if (System.IO.File.Exists(this.SourceTextBox.Text.Trim()))
                                         if (!TerraScanCommon.IsFieldUser)
                                        {
                                            if (!System.IO.File.Exists(this.fileurlpath))
                                            {
                                                try
                                                {
                                                    string validPath = this.fileurlpath.Remove(this.fileurlpath.LastIndexOf("\\"));
                                                    //if (System.IO.Directory.Exists(validPath.Trim()))
                                                    //{
                                                    #region TSBG - D9005.F9005 Attachments form - Incorrect @FileID and @PFileID variables at save
                                                    FileStream fs;
                                                    if (flagFile)
                                                    {
                                                        fs = new FileStream(this.fileTextBox.Text.Trim(), FileMode.Open);
                                                    }
                                                    else
                                                    {
                                                        fs = new FileStream(this.browseTmpPath.Trim(), FileMode.Open);
                                                    }
                                                    #endregion
                                                    BinaryReader bR = new BinaryReader(fs);
                                                    this.filePath = this.fileurlpath;
                                                    //// Upload the Image to the Central Location.
                                                    UpLoadImage(bR.ReadBytes((int)fs.Length), this.filePath);
                                                    bR.Close();
                                                    fs.Close();
                                                    //}
                                                    //else
                                                    //{
                                                    //    ErrorEngine.ShowForm(3);
                                                    //    hasUploadCompleted = false;
                                                    //}
                                                }
                                                catch (IOException)
                                                {
                                                    ErrorEngine.ShowForm(3);
                                                    hasUploadCompleted = false;
                                                }
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                          if (!this.scanFile)
                                           {
                                                if (!string.IsNullOrEmpty(this.SourceTextBox.Text))
                                                {
                                                    this.browsePathExt = this.SourceTextBox.Text.Substring(this.SourceTextBox.Text.LastIndexOf(".")).ToString();
                                                }
                                            }
                                        else
                                        {
                                            this.browsePathExt = ".tiff";
                                        }

                                        if (this.linktypeTextBox.Text.Equals("3") && string.IsNullOrEmpty(this.SourceTextBox.Text))
                                        {
                                            this.browsePathExt = ".tiff";
                                        }

                                        if (string.IsNullOrEmpty(this.saveFilePath))
                                        {
                                            this.fileurlpath = this.fileTextBox.Text.Remove(this.fileTextBox.Text.LastIndexOf("."));
                                        }
                                        else
                                        {
                                            this.fileurlpath = this.saveFilePath.Remove(this.saveFilePath.LastIndexOf("."));
                                        }

                                        this.fileurlpath = this.fileurlpath + this.browsePathExt;
                                        this.fileTextBox.Text = this.fileurlpath;
                                    }

                                    if (hasUploadCompleted)
                                    {
                                        this.SaveAttachmentEntry();

                                        int fileNewId = 0;
                                        int.TryParse(this.AttachmentGridView.Rows[this.RowNo].Cells["AttachmentFileID"].Value.ToString(), out fileNewId);
                                        this.form9005Control.WorkItem.GenerateThumbnail(fileNewId, TerraScanCommon.UserId, null);

                                        //// Set the Button Mode after saving the Record.
                                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.SaveMode);
                                        this.SetCurrentFormButtons(ButtonOperation.Save);
                                        this.buttonOperation = (int)ButtonOperation.Empty;
                                        this.descriptionTextBox.BackColor = System.Drawing.Color.White;
                                        this.localFilePath = string.Empty;
                                        this.SourceTextBox.ForeColor = System.Drawing.Color.FromArgb((int)(byte)(102), (int)(byte)(102), (int)(byte)(102));
                                        this.tmpDataTable.Clear();

                                        if (this.dateChanged)
                                        {
                                            // string ss = this.attachmentGridView.Columns["AttachmentFileID"].Index.ToString();

                                            BindingSource source = new BindingSource();
                                            source.DataSource = this.attachmentDataSet.GetAttachmentItems;
                                            foundIndex = source.Find("FileID", dateFileId);
                                            this.SetDataGridViewPosition(foundIndex);
                                            this.SetDataBindingValue(foundIndex);
                                        }
                                        else
                                        {
                                            this.SetDataGridViewPosition(currentSelectedRow);
                                        }

                                        this.dateChanged = false;
                                        ////this.scanForm.SuccesfullyScanned = false;
                                        this.scanFile = false;
                                        this.closingNow = true;
                                    }
                                }
                                else if (this.validPermission)
                                {
                                    // shows the error message through Error form.
                                    // ErrorForm errorForm = new ErrorForm(SharedFunctions.GetResourceString("FileMissing"), SharedFunctions.GetResourceString("FileNotExists"), SharedFunctions.GetResourceString("UnabletoSave"), "", string.Empty);
                                    // error Form.ShowDialog();

                                    MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("FileNotExists"), "\n", SharedFunctions.GetResourceString("UnabletoSave") }), SharedFunctions.GetResourceString("FileMissing"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.LoadAttachmentGrid();
                                    ////this.editOperation = true;
                                    this.SetDataGridViewPosition(currentSelectedRow);
                                    this.SetDataBindingValue(currentSelectedRow);
                                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                                    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                                    this.browseButton.ImageSelected = false;
                                    this.scanButton.ImageSelected = false;
                                    this.scanFile = false;
                                    this.SetFocus();
                                    this.closingNow = false;
                                    return;
                                }
                            }
                            else
                            {
                                ////this.LoadAttachmentGrid();
                                ////this.editOperation = true;
                                ////this.SetDataGridViewPosition(currentSelectedRow);
                            }

                            this.auditLinkLabel.Enabled = true;
                        }
                        catch (SoapException ex)
                        {
                            ////TODO : Need to find specific exception and handle it.
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        }
                    }
                }
                else
                {
                    this.attachmentDateTextBox.Focus();
                    this.closingNow = false;
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.attachmentDateTextBox.Focus();
                this.closingNow = false;
            }

            this.CheckEditPermission();

            //if (TerraScanCommon.IsFieldUser)
            //{
            //    this.deleteAttachmentButton.Enabled = false; 
            //}
        }


        /// <summary>
        /// Checks the date.
        /// </summary>
        private bool CheckDate()
        {
            System.Windows.Forms.DateTimePicker validDate = new System.Windows.Forms.DateTimePicker();
            validDate.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
            validDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (!string.IsNullOrEmpty(this.attachmentDateTextBox.Text.Trim()))
            {
                try
                {
                    validDate.Value = DateTime.Parse(this.attachmentDateTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("Please enter a valid date." + "\n" + "Allowed formats: m/d/yyyy." + "\n" + "Minimum value:" + "1 / 1 / 1900" + "\n" + "Maximum value:" + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    attachmentDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                    attachmentDateTextBox.Focus();
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Sets the data binding value.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void SetDataBindingValue(int rowId)
        {
            if (this.attachmentDataSet.GetAttachmentItems.Rows.Count > 0)
            {
                if (!this.emptyRecord)
                {
                    if (rowId >= 0)
                    {
                        this.attachmentDateTextBox.Text = this.AttachmentGridView.Rows[rowId].Cells["AttachmentDate"].Value.ToString();
                        this.tempDate = this.attachmentDateTextBox.Text;

                        this.typeComboBox.Text = this.AttachmentGridView.Rows[rowId].Cells["Type"].Value.ToString();

                        //if (!string.IsNullOrEmpty(this.AttachmentGridView.Rows[rowId].Cells["IsRoll"].Value.ToString()))
                        //{
                        //    this.WillRollCheckBox.Checked = Convert.ToBoolean(this.AttachmentGridView.Rows[rowId].Cells["IsRoll"].Value.ToString());
                        //}
                        //else
                        //{
                        //    this.WillRollCheckBox.Checked = false;
                        //}

                        if (!string.IsNullOrEmpty(this.AttachmentGridView.Rows[rowId].Cells["Public"].Value.ToString()))
                        {
                            this.publicCheckBox.Checked = Convert.ToBoolean(this.AttachmentGridView.Rows[rowId].Cells["Public"].Value.ToString());
                        }
                        else
                        {
                            this.publicCheckBox.Checked = false;
                        }

                        if (!string.IsNullOrEmpty(this.AttachmentGridView.Rows[rowId].Cells["Primary"].Value.ToString()))
                        {
                            this.primaryCheckBox.Checked = Convert.ToBoolean(this.AttachmentGridView.Rows[rowId].Cells["Primary"].Value.ToString());
                        }
                        else
                        {
                            this.primaryCheckBox.Checked = false;
                        }

                        this.descriptionTextBox.Text = this.AttachmentGridView.Rows[rowId].Cells["Description"].Value.ToString();
                        this.tempDescription = this.descriptionTextBox.Text;
                        this.userTextBox.Text = this.AttachmentGridView.Rows[rowId].Cells["AttachmentUser"].Value.ToString();
                        this.fileTypeIDTextBox.Text = this.AttachmentGridView.Rows[rowId].Cells["FileTypeID"].Value.ToString();
                        ////Added on 8/5/2008 by malliga
                        ////this.fileTextBox.Text = this.attachmentGridView.Rows[rowId].Cells["File"].Value.ToString().Trim();
                        this.fileTextBox.Text = this.AttachmentGridView.Rows[rowId].Cells["AURL"].Value.ToString().Trim();

                        this.linktypeTextBox.Text = this.AttachmentGridView.Rows[rowId].Cells["LinkType"].Value.ToString().Trim();
                        if (this.linktypeTextBox.Text != "3")
                        {
                            this.SourceTextBox.Text = this.AttachmentGridView.Rows[rowId].Cells["Source"].Value.ToString().Trim();
                        }
                        else
                        {
                            this.SourceTextBox.Text = string.Empty;
                        }
                        ////this.SetSourceFileName(this.attachmentGridView.Rows[rowId].Cells["Source"].Value.ToString().Trim());
                        int tempKeyId = 0;
                        int.TryParse(this.AttachmentGridView.Rows[rowId].Cells["KeyID"].Value.ToString(), out tempKeyId);
                    }
                }
            }
        }

        /// <summary>
        /// Clears the values in header fields.
        /// </summary>
        private void ClearValues()
        {
            this.attachmentDateTextBox.Text = string.Empty;
            this.typeComboBox.Text = string.Empty;
            this.descriptionTextBox.Text = string.Empty;
            this.primaryCheckBox.Checked = false;
            this.publicCheckBox.Checked = false;
            //this.WillRollCheckBox.Checked = false;
            this.userTextBox.Text = string.Empty;
            this.fileTypeIDTextBox.Text = string.Empty;
            this.fileTextBox.Text = string.Empty;
            this.SourceTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Gets the selected file path.
        /// </summary>
        /// <param name="tmpCurrentRow">The TMP current row.</param>
        /// <returns> The path of the file</returns>
        private string GetSelectedFilePath()
        {
            string tmpSource = string.Empty;
            if (this.CheckAttachmentGrid())
            {
                // Gets the File path of the selected row in the datagrid.               
                if (this.tempSourcePath.Rows.Count >= 0)
                {
                    int currentFileID = 0;
                    if (this.AttachmentGridView.CurrentRow != null)
                    {
                        int.TryParse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells[this.AttachmentFileID.Name].Value.ToString(), out currentFileID);
                    }

                    this.selectedFilePath = this.form9005Control.WorkItem.GetOriginalFilePath(currentFileID, TerraScanCommon.UserId);
                    tmpSource = this.form9005Control.WorkItem.GetOriginalFilePath(currentFileID, TerraScanCommon.UserId);
                }
            }
            else
            {
                tmpSource = string.Empty;
            }

            return tmpSource;
        }

        /// <summary>
        /// Used to check whether the selected row has any file . 
        /// </summary>
        /// <param name="fileId">The file ID.</param>
        /// <returns> Flag to wether browse button color set.</returns>
        private bool SetBrowseButtonColor(int fileId)
        {
            string stringExp = "FileID =" + fileId;
            DataRow[] foundRows;
            foundRows = this.tmpDataTable.Select(stringExp);
            if (foundRows.GetUpperBound(0) >= 0)
            {
                this.scanButton.ImageSelected = true;
                return true;
            }
            else
            {
                this.scanButton.ImageSelected = false;
                return false;
            }
        }

        /// <summary>
        /// Temps the data.
        /// </summary>
        private void TempData()
        {
            this.SetDataBindingValue(this.tempRowId);

            // this.SetDataGridViewPosition(this.tempRowId);
            if (!string.IsNullOrEmpty(this.tempDescription))
            {
                this.descriptionTextBox.Text = this.tempDescription;
            }

            if (!string.IsNullOrEmpty(this.tempDate))
            {
                this.attachmentDateTextBox.Text = this.tempDate;
            }

            if (!string.IsNullOrEmpty(this.tempFileType))
            {
                this.typeComboBox.Text = this.tempFileType;
            }

            this.publicCheckBox.Checked = this.tempPublic;
            this.primaryCheckBox.Checked = this.tempPrimary;
            //this.WillRollCheckBox.Checked = this.tempRoll;

            SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
            this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
            this.SetFocus();
        }

        /// <summary>
        /// Checks the attachment grid has Record or not.
        /// </summary>
        /// <returns> Flag to check wether it is attachment grid.</returns>
        private bool CheckAttachmentGrid()
        {
            // Checks whether the Datagridview has rows or not.
            if (this.AttachmentGridView.OriginalRowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the Requireds the field during Save Operation.
        /// </summary>
        /// <returns> Flag to specify wether required.</returns>
        private bool RequiredField()
        {
            /* // Checks whether DataTable is Null or Not.
            if (VaildDataSet(this.attachmentDataSet))
            {
                DataRowCollection dataRowCollection = this.attachmentDataSet.Tables[0].Rows;
                foreach (DataRow tempDataRow in dataRowCollection)
                {
                    if (String.IsNullOrEmpty(tempDataRow["Description"].ToString().Trim()))
                    {
                        this.validData = false;
                        break;
                    }
                    else
                    {
                        this.validData = true;
                    }
                }
            } */

            //// Checks all the Required Controls has value assigned. //  && this.keyIDTextBox.Text.Trim().Length > 0 -- this was removed for Roll correction
            ////if (this.attachmentDateTextBox.Text.Trim().Length > 0 && this.userTextBox.Text.Trim().Length > 0 && this.formIDTextBox.Text.Trim().Length > 0 && this.descriptionTextBox.Text.Trim().Length > 0 && this.validData && this.typeComboBox.SelectedIndex >= 0)
            ////if (this.attachmentDateTextBox.Text.Trim().Length > 0 && this.userTextBox.Text.Trim().Length > 0 && this.descriptionTextBox.Text.Trim().Length > 0 && this.validData && this.typeComboBox.SelectedIndex >= 0)
            if (this.attachmentDateTextBox.Text.Trim().Length > 0 && this.userTextBox.Text.Trim().Length > 0 && this.validData && this.typeComboBox.SelectedIndex >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the value of FormID and KeyID.
        /// </summary>
        private void GetValue()
        {
            this.userTextBox.Text = TerraScan.Common.TerraScanCommon.UserName;
        }

        /// <summary>
        /// Sets the focus.
        /// </summary>
        private void SetFocus()
        {
            ////Coding Commented and Added for the Issue 4237 by malliga on 2/3/2009
            //this.descriptionTextBox.Focus();
            //this.descriptionTextBox.SelectAll();
            this.attachmentDateTextBox.Focus();
            this.attachmentDateTextBox.SelectAll();
        }

        /// <summary>
        /// Sets the focus to the descriptionTextBox.
        /// </summary>
        private void SetLostFocus()
        {
            if (this.textBoxFocused == this.descriptionTextBox.Name)
            {
                this.descriptionTextBox.Focus();
            }
            else if (this.textBoxFocused == this.attachmentDateTextBox.Name)
            {
                this.attachmentDateTextBox.Focus();
            }

            // this.descriptionTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 121);
        }

        /// <summary>
        /// Initializes the button to Enable or Disable.
        /// </summary>
        private void InitializeButton()
        {
            // this.newAttachmentButton.Enabled = true;
            // this.saveAttachmentButton.Enabled = false;
            // this.cancelAttachmentButton.Enabled = false;

            this.SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.OpenMode);

            if (this.emptyRecord)
            {
                this.deleteAttachmentButton.Enabled = false;
                this.openButton.Enabled = false;
                this.browseButton.Enabled = false;
                this.scanButton.Enabled = false;
                this.URLbutton.Enabled = false;
            }
            else
            {
                if (!TerraScanCommon.IsFieldUser)
                {
                    this.deleteAttachmentButton.Enabled = true && this.deleteAttachmentButton.ActualPermission;
                }
                else
                {
                    this.deleteAttachmentButton.Enabled = false;
                }
                this.openButton.Enabled = true;
                this.browseButton.Enabled = true;
                this.scanButton.Enabled = true;
                this.URLbutton.Enabled = true;
            }

        }

        /// <summary>
        /// Discards the changes.
        /// </summary>
        private void DiscardChanges()
        {
            this.attachmentDataSet.GetAttachmentItems.RejectChanges();
            this.LoadAttachmentGrid();
            ////this.attachmentGridView.Enabled = true;
            this.currencyManager.Position = Convert.ToInt32("0");
            this.buttonOperation = (int)ButtonOperation.Empty;
            SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.CancelMode);
            this.SetCurrentFormButtons(ButtonOperation.Cancel);

            // SetButtonColor();
            this.browseButton.ImageSelected = false;
            this.scanButton.ImageSelected = false;
            this.scanFile = false;
            this.tmpDataTable.Rows.Clear();
            this.browseSelected = false;
            this.browseTmpPath = string.Empty;
            this.scanTmpPath = string.Empty;
            ////this.scanForm.SuccesfullyScanned = false;
            this.scanFile = false;
        }

        /// <summary>
        /// Disables the attachment control.
        /// </summary>
        private void DisableAttachmentControl()
        {
            this.descriptionTextBox.BackColor = System.Drawing.Color.White;
            this.descriptionTextBox.Enabled = false;
            this.attachmentDateTextBox.Enabled = false;
            this.publicCheckBox.Enabled = false;
            this.primaryCheckBox.Enabled = false;
            //this.WillRollCheckBox.Enabled = false;
            this.typeComboBox.Enabled = false;
            this.attachmentDatePictureBox.Enabled = false;
            this.attachmentMonthCalander.Visible = false;
            if (this.emptyRecord)
            {
                this.fileTypeIDTextBox.Enabled = false;
                this.fileTextBox.Enabled = false;
                this.SourceTextBox.Enabled = false;
                this.userTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// Enables the attachment control.
        /// </summary>
        private void EnableAttachmentControl()
        {
            this.descriptionTextBox.Enabled = true;
            this.publicCheckBox.Enabled = true;
            this.primaryCheckBox.Enabled = true;
            //this.WillRollCheckBox.Enabled = true;
            this.typeComboBox.Enabled = true;
            this.attachmentDateTextBox.Enabled = true;
            this.attachmentDatePictureBox.Enabled = true;
            this.fileTypeIDTextBox.Enabled = true;
            this.fileTextBox.Enabled = true;
            this.SourceTextBox.Enabled = true;
            //// this.userTextBox.Enabled = true;
        }

        /// <summary>
        /// Enables the non editable control.
        /// </summary>
        private void EnableNonEditableControl()
        {
            this.fileTypeIDTextBox.Enabled = true;
            this.fileTextBox.Enabled = true;
            this.SourceTextBox.Enabled = true;
            //// this.userTextBox.Enabled = true;
        }

        /*  /// <summary>
         /// Clears the data binding in Datagridview.
         /// </summary>
         private void ClearDataBinding()
         {
             this.attachmentDateTextBox.DataBindings.Clear();
             this.publicCheckBox.DataBindings.Clear();
             this.primaryCheckBox.DataBindings.Clear();
             this.descriptionTextBox.DataBindings.Clear();
             this.userTextBox.DataBindings.Clear();
             this.formIDTextBox.DataBindings.Clear();
             this.keyIDTextBox.DataBindings.Clear();
             this.fileTypeIDTextBox.DataBindings.Clear();
             this.fileTextBox.DataBindings.Clear();
             this.typeComboBox.DataBindings.Clear();
         } */

        /// <summary>
        /// Sets the buttons according to the status.
        /// </summary>
        /// <param name="buttonName">Name of the button.</param>
        private void SetCurrentFormButtons(ButtonOperation buttonName)
        {
            switch (buttonName)
            {
                case ButtonOperation.New:
                    {
                        this.openButton.Enabled = false;
                        this.browseButton.Enabled = true;
                        this.scanButton.Enabled = true;
                        this.URLbutton.Enabled = true;
                        this.SetCancelButton();
                        //// SetBackColor();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        if (this.emptyRecord)
                        {
                            this.deleteAttachmentButton.Enabled = false;
                            this.descriptionTextBox.Text = string.Empty;
                            this.browseButton.Enabled = false;
                            this.openButton.Enabled = false;
                            this.scanButton.Enabled = false;
                            this.URLbutton.Enabled = false;
                        }
                        else
                        {
                            if (!TerraScanCommon.IsFieldUser)
                            {
                                this.deleteAttachmentButton.Enabled = true && this.deleteAttachmentButton.ActualPermission;
                            }
                            else
                            {
                                this.deleteAttachmentButton.Enabled = false;
                            }
                            this.openButton.Enabled = true;
                            this.scanButton.Enabled = true;
                            this.browseButton.Enabled = true;
                            this.URLbutton.Enabled = true;
                            this.scanButton.ImageSelected = false;
                            this.browseButton.ImageSelected = false;


                        }

                        //// SetBackColor();
                        this.SetCancelButton();
                        break;
                    }

                case ButtonOperation.Delete:
                    {
                        this.openButton.Enabled = false;
                        this.browseButton.Enabled = false;
                        this.scanButton.Enabled = false;
                        this.URLbutton.Enabled = false;
                        this.SetCancelButton();
                        //// SetBackColor();
                        break;
                    }

                case ButtonOperation.Save:
                    {
                        this.openButton.Enabled = true;
                        this.scanButton.Enabled = true;
                        this.browseButton.Enabled = true;
                        this.URLbutton.Enabled = true;
                        this.SetCancelButton();
                        ////  SetBackColor();
                        break;
                    }

                case ButtonOperation.Scan:
                    {
                        this.openButton.Enabled = false;
                        this.browseButton.Enabled = false;
                        this.URLbutton.Enabled = false;
                        this.scanButton.Enabled = true;
                        this.SetCancelButton();
                        //// SetBackColor();
                        break;
                    }

                case ButtonOperation.Browse:
                    {
                        ////SetBackColor();
                        this.URLbutton.Enabled = false;
                        this.openButton.Enabled = false;
                        this.scanButton.Enabled = false;
                        this.SetCancelButton();

                        break;
                    }

                case ButtonOperation.AttachmentGrid:
                    {
                        if (this.browseButton.ImageSelected)
                        {
                            this.scanButton.Enabled = false;
                        }
                        else
                        {
                            this.scanButton.Enabled = true;
                        }

                        if (this.saveAttachmentButton.Enabled == false)
                        {
                            this.browseButton.Enabled = false;
                            this.URLbutton.Enabled = false;
                        }
                        else
                        {
                            this.browseButton.Enabled = true;
                            this.URLbutton.Enabled = true;
                        }

                        this.openButton.Enabled = false;
                        this.deleteAttachmentButton.Enabled = false;
                        this.SetCancelButton();
                        ////  SetBackColor();
                        break;
                    }

                case ButtonOperation.EmptyGrid:
                    {
                        this.newAttachmentButton.Enabled = true && this.newAttachmentButton.ActualPermission;
                        this.saveAttachmentButton.Enabled = false;
                        this.cancelAttachmentButton.Enabled = false;
                        this.deleteAttachmentButton.Enabled = false;
                        this.openButton.Enabled = false;
                        this.browseButton.Enabled = false;
                        this.scanButton.Enabled = false;
                        this.URLbutton.Enabled = false;
                        this.SetCancelButton();
                        ////  SetBackColor();
                        break;
                    }
            }


        }

        /// <summary>
        /// Sets the data grid view position to firstrow.
        /// </summary>
        /// <param name="firstRow">The first row.</param>
        private void SetDataGridViewPosition(int firstRow)
        {
            this.auditFileID = this.fileTextBox.Text;
            if (this.auditFileID.Length > 0)
            {
                //// Gets the value for auditLinkLabel.
                string tempfiletextboxvalue = string.Empty;
                string tempfiletextboxvalue2 = string.Empty;
                string tempfiletextboxvalue3 = string.Empty;

                tempfiletextboxvalue = this.fileTextBox.Text.Remove(this.fileTextBox.Text.Trim().LastIndexOf("."));
                tempfiletextboxvalue2 = tempfiletextboxvalue.Substring(tempfiletextboxvalue.ToString().Trim().LastIndexOf("\\"));
                int filelength = tempfiletextboxvalue2.Trim().Length;
                tempfiletextboxvalue3 = tempfiletextboxvalue2.Substring(1, filelength - 1);
                //// this.auditLinkLabel.Text = "tTS_File[FileID] " + this.auditFileID.Remove(this.auditFileID.LastIndexOf("."));
                this.auditLinkLabel.Text = "tTS_File[FileID] " + tempfiletextboxvalue3;
            }
            else
            {
                this.auditLinkLabel.Text = "tTS_File[FileID] ";
            }

            if (this.CheckAttachmentGrid() && firstRow >= 0)
            {
                /* if (this.editOperation)
                {
                    // Select the data grid view position to firstrow.
                    this.attachmentGridView.Rows[Convert.ToInt32(firstRow)].Selected = true;
                    this.attachmentGridView.CurrentCell = this.attachmentGridView[0, Convert.ToInt32(firstRow)];
                }
                else
                {
                    this.attachmentGridView.Rows[Convert.ToInt32(firstRow)].Selected = true;
                }  */

                this.AttachmentGridView.Rows[Convert.ToInt32(firstRow)].Selected = true;
                this.AttachmentGridView.CurrentCell = this.AttachmentGridView[0, Convert.ToInt32(firstRow)];
            }
            else
            {
                this.AttachmentGridView.CurrentCell = null;
            }

            ////this.editOperation = false;
        }

        /// <summary>
        /// Checks the edit permission.
        /// </summary>
        private void CheckEditPermission()
        {
            if (this.FormPermissionFields.editPermission)
            {
                ////this.EnableAttachmentControl();
                this.attachmentDateTextBox.LockKeyPress = false;
                this.attachmentDatePictureBox.Enabled = true;
                this.typeComboBox.Enabled = true;
                this.publicCheckBox.Enabled = true;
                this.primaryCheckBox.Enabled = true;
                //this.WillRollCheckBox.Enabled = true;
                this.descriptionTextBox.LockKeyPress = false;
            }
            else
            {
                ////this.DisableAttachmentControl();
                this.attachmentDateTextBox.LockKeyPress = true;
                this.attachmentDatePictureBox.Enabled = false;
                this.typeComboBox.Enabled = false;
                this.publicCheckBox.Enabled = false;
                this.primaryCheckBox.Enabled = false;
                //this.WillRollCheckBox.Enabled = false;
                this.descriptionTextBox.LockKeyPress = true;
                this.descriptionTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Customizes the data grid.
        /// </summary>
        private void CustomizeDataGrid()
        {
            this.AttachmentGridView.AllowUserToResizeColumns = false;
            this.AttachmentGridView.AutoGenerateColumns = false;
            this.AttachmentGridView.AllowUserToResizeRows = false;
            this.AttachmentGridView.StandardTab = true;
            this.commentHeader.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.commentHeader.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            this.commentHeader.BackColor = System.Drawing.Color.Silver;
            this.commentDefaultCell.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            this.AttachmentGridView.EnableHeadersVisualStyles = false;
            //// Assigning the AttachmentGridView Columns
            this.AttachmentGridView.Columns[0].DataPropertyName = "EventDate";
            this.AttachmentGridView.Columns[1].DataPropertyName = "Description";
            this.AttachmentGridView.Columns[2].DataPropertyName = "FunctionName";
            this.AttachmentGridView.Columns[3].DataPropertyName = "UserName";
            this.AttachmentGridView.Columns[4].DataPropertyName = "IsPublic";
            this.AttachmentGridView.Columns[5].DataPropertyName = "Primary";
            this.AttachmentGridView.Columns[6].DataPropertyName = "File";
            this.AttachmentGridView.Columns[7].DataPropertyName = "Source";
            this.AttachmentGridView.Columns[8].DataPropertyName = "KeyID";
            this.AttachmentGridView.Columns[9].DataPropertyName = "Form";
            this.AttachmentGridView.Columns[10].DataPropertyName = "FileTypeID";
            this.AttachmentGridView.Columns[11].DataPropertyName = "FileID";
            this.AttachmentGridView.Columns[12].DataPropertyName = "Extension";
            this.AttachmentGridView.Columns[13].DataPropertyName = "IsRoll";
            this.AttachmentGridView.Columns[14].DataPropertyName = "AURL";
            this.AttachmentGridView.Columns[15].DataPropertyName = "LinkType";
            this.AttachmentGridView.PrimaryKeyColumnName = "FileID";
        }

        /*  /// <summary>
        /// Sets the data binding to Datagridview.
        /// </summary>
        private void SetDataBinding()
        {
            this.attachmentDateTextBox.DataBindings.Add("Text", this.attachmentDataSet.Tables[0], "EventDate");
            this.typeComboBox.DataBindings.Add("Text", this.attachmentDataSet.Tables[0], "FunctionName");
            this.publicCheckBox.DataBindings.Add("Checked", this.attachmentDataSet.Tables[0], "IsPublic");
            this.primaryRadioButton.DataBindings.Add("Checked", this.attachmentDataSet.Tables[0], "Primary");
            this.descriptionTextBox.DataBindings.Add("Text", this.attachmentDataSet.Tables[0], "Description");
            this.fileTypeIDTextBox.DataBindings.Add("Text", this.attachmentDataSet.Tables[0], "FileTypeID");
            this.fileTextBox.DataBindings.Add("Text", this.attachmentDataSet.Tables[0], "File");
            this.userTextBox.DataBindings.Add("Text", this.attachmentDataSet.Tables[0], "UserName");
        } */

        /// <summary>
        /// Loads the attachment grid from database.
        /// </summary>
        private void LoadAttachmentGrid()
        {
            this.tempSourcePath.Rows.Clear();

            //// Loads Function Name to Populate Combo Box
            //// this.attachmentDataSet = F9005WorkItem.GetAttachmentItems(this.attachmentFormID, this.attachmentKeyID, TerraScan.Common.TerraScanCommon.UserId);
            this.attachmentDataSet.GetAttachmentItems.Clear();
            this.attachmentDataSet.GetAttachmentItems.Merge(this.form9005Control.WorkItem.GetAttachmentItems(this.attachmentFormID, this.attachmentKeyID, TerraScan.Common.TerraScanCommon.UserId));
            this.attachmentCount = this.attachmentDataSet.GetAttachmentItems.Rows.Count;

            this.recordCount = this.attachmentDataSet.GetAttachmentItems.Rows.Count;
            this.AttachmentGridView.EnableHeadersVisualStyles = true;

            if (VaildDataSet(this.attachmentDataSet))
            {
                if (this.attachmentDataSet.GetAttachmentItems.Rows.Count == 0)
                {
                    this.AttachmentGridView.RemoveDefaultSelection = true;
                    this.AttachmentGridView.Enabled = false;
                    this.emptyRecord = true;
                }
                else
                {
                    this.AttachmentGridView.RemoveDefaultSelection = false;
                    this.AttachmentGridView.Enabled = true;
                    this.emptyRecord = false;
                }

                //// Gets the Number of Records for Attachments.
                //// this.SetAttachmentCount(this.attachmentDataSet.GetAttachmentItems.Rows.Count);
                this.CustomizeDataGrid();

                //// Creates a empty row.
                //// this.attachmentDataSet.Tables[0].Merge(CreateEmptyRows(this.attachmentDataSet.Tables[0], 5));
                //// this.attachmentGridViewEmpty.DataSource = TerraScan.Common.TerraScanCommon.CreateEmptyRows(this.attachmentDataSet.Tables[0].Clone(), 5);
                //// this.attachmentGridViewEmpty.Enabled = false;

                //// Assign the Dataset to Grid.
                try
                {
                    //identify the WsHelper.IsOnline Mode - false
                    if (WSHelper.IsOnLineMode.Equals(false) && TerraScanCommon.IsFieldUser)
                    {
                        if (this.attachmentDataSet.GetAttachmentItems.Rows.Count > 0)
                        {

                            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            string tempPath1 = tempPath + "\\Terrascan Attachment";
                            //replace the Environment Special Folder
                            for (int i = 0; i < this.attachmentDataSet.GetAttachmentItems.Rows.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(this.attachmentDataSet.GetAttachmentItems.Rows[i]["AURL"].ToString()))
                                {
                                    string DestinationPath = this.attachmentDataSet.GetAttachmentItems.Rows[i]["AURL"].ToString();
                                    string s = Path.GetPathRoot(DestinationPath);
                                    string tempLocalpath = string.Empty;
                                    string tempPath2 = string.Empty;
                                    //if (s.Contains("IsFieldAdded"))
                                    //{
                                    //    tempLocalpath = DestinationPath.Remove(0, s.Length - 1);
                                    //}
                                    //else
                                    //{
                                    tempLocalpath = DestinationPath.Remove(0, s.Length);
                                    //}

                                    tempPath2 = tempPath1 + tempLocalpath;
                                    this.attachmentDataSet.GetAttachmentItems.Rows[i]["AURL"] = tempPath2;
                                    this.attachmentDataSet.GetAttachmentItems.AcceptChanges();
                                }
                            }
                        }
                    }

                    this.AttachmentGridView.DataSource = this.attachmentDataSet.GetAttachmentItems.DefaultView;
                }
                catch (Exception ex)
                {
                }

                if (this.recordCount > 0)
                {
                    //int foundIndex;
                    //DataView sortView = this.attachmentDataSet.GetAttachmentItems.DefaultView;
                    //sortView.Sort = "FileID DESC";
                    //DataSet attachmentIdDataset = new DataSet();
                    //DataRow[] newRowAttachmenttID;
                    //int dateFileId = 0;
                    //newRowAttachmenttID = this.attachmentDataSet.GetAttachmentItems.Select("FileID = MAX(FileID)");
                    //attachmentIdDataset.Merge(newRowAttachmenttID);
                    //dateFileId = Convert.ToInt32(attachmentIdDataset.Tables[0].Rows[0]["FileID"].ToString());
                    //BindingSource source = new BindingSource();
                    //source.DataSource = this.attachmentDataSet.GetAttachmentItems;
                    //foundIndex = source.Find("FileID", dateFileId);
                    this.SetDataGridViewPosition(0);
                }

                //// Assign the values in dataset to Datagridview

                this.currencyManager = (CurrencyManager)this.BindingContext[this.attachmentDataSet, this.attachmentDataSet.GetAttachmentItems.TableName];
                this.FillTempFilePath();

                //// TerraScan.Common.TerraScanCommon.SetGridHeight(this.attachmentGridView, 5);
            }
            else
            {
                //// this.ClearDataBinding();

                //// Assign the values in dataset to Datagridview
                this.CustomizeDataGrid();
            }

            if (this.recordCount == 0)
            {
                this.auditLinkLabel.Enabled = false;
            }
            else
            {
                this.auditLinkLabel.Enabled = true;
            }

            ////Coding Added For Issue Id 3905 by Malliga On 2/3/2009
            if (this.attachmentCount > 0)
            {
                this.EnableAttachmentControl();
                //// this.ClearHeader();
                this.AttachmentGridView.Rows[0].Selected = false;
            }
            else
            {
                this.DisableAttachmentControl();
                this.ClearHeader();
            }

            this.SetCancelButton();
            if (this.attachmentDataSet.GetAttachmentItems.Rows.Count > 8)
            {
                this.attachmentVScrollBar.Visible = false;
            }
            else
            {
                this.attachmentVScrollBar.Visible = true;
            }
            if (!this.emptyRecord)
            {
                this.AttachmentGridView.Focus();
            }
            this.textchange = false;
        }

        /// <summary>
        /// Fills the data set with changed value.
        /// </summary>
        private void FillDataSet()
        {
            this.attachmentDataRow["FormID"] = this.attachmentFormID;
            this.attachmentDataRow["KeyID"] = this.attachmentKeyID;
            this.attachmentDataRow["UserID"] = TerraScan.Common.TerraScanCommon.UserId;
            this.attachmentDataRow["EventDate"] = Convert.ToDateTime(this.attachmentDateTextBox.Text.ToString());
            this.attachmentDataRow["Source"] = this.filePath;
            this.attachmentDataRow["Extension"] = this.browsePathExt;
            this.attachmentDataRow["IsPublic"] = this.publicCheckBox.Checked;
            this.attachmentDataRow["Primary"] = this.primaryCheckBox.Checked;
            this.attachmentDataRow["Description"] = this.descriptionTextBox.Text.ToString();
            this.attachmentDataRow["FileTypeID"] = this.fileTypeIDTextBox.Text.ToString();
            this.attachmentDataRow["FileID"] = this.fileID;
            this.attachmentDataSet.GetAttachmentItems.Rows.Add(this.attachmentDataRow);
            this.currentRow = Convert.ToInt32(this.attachmentDataSet.GetAttachmentItems.Rows.Count.ToString()) - 1;
        }

        /// <summary>
        /// Saves the attachment form entry to database.
        /// </summary>
        private void SaveAttachmentEntry()
        {
            int primaryValue = 0;
            int publicValue = 0;
            int rollValue = 0;
            string fileExt = string.Empty;


       

            ////if (!string.IsNullOrEmpty(this.attachmentDateTextBox.Text.Trim()))
            ////{
            ////    try
            ////    {
            ////        DateTime attachmenttime = DateTime.Parse(this.attachmentDateTextBox.Text.Trim());
            ////    }
            ////    catch
            ////    {
            ////        MessageBox.Show("Please enter a valid date." + "\n" + "Allowed formats: m/d/yyyy." + "\n" + "Minimum value:" + "1 / 1 / 1900" + "\n" + "Maximum value:" + "6/6/2079", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ////        attachmentDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
            ////        attachmentDateTextBox.Focus();
            ////        return;
            ////    }
            ////}

            //if (this.WillRollCheckBox.Checked == true)
            //{
            //    rollValue = 1;
            //}
            //else
            //{
            //    rollValue = 0;
            //}

            if (this.primaryCheckBox.Checked == true)
            {
                primaryValue = 1;
            }
            else
            {
                primaryValue = 0;
            }

            if (this.publicCheckBox.Checked == true)
            {
                publicValue = 1;
            }
            else
            {
                publicValue = 0;
            }

            if (this.scanFile)
            {
                fileExt = this.scanFileExt;
            }
            else
            {
                fileExt = this.browsePathExt;
            }

            if (this.buttonOperation == (int)ButtonOperation.New)
            {
                ////this.editOperation = true;

                //// Save the Entries to Database.
                //int fileId = this.form9005Control.WorkItem.SaveAttachments(Convert.ToInt32(this.fileID), fileExt, this.attachmentFormID, this.attachmentKeyID, Convert.ToInt32(this.fileTypeIDTextBox.Text), this.localFilePath, primaryValue, this.descriptionTextBox.Text.ToString().Trim(), this.attachmentDateTextBox.Text.Trim(), TerraScan.Common.TerraScanCommon.UserId, publicValue, rollValue, this.linktypeid, this.fileurlpath, this.tmpFileID);

                AttachmentsData.SaveFilePathDataTable filePathData = this.form9005Control.WorkItem.SaveAttachments(null, fileExt, this.attachmentFormID, this.attachmentKeyID, Convert.ToInt32(this.fileTypeIDTextBox.Text), this.localFilePath, primaryValue, this.descriptionTextBox.Text.ToString().Trim(), this.attachmentDateTextBox.Text.Trim(), TerraScan.Common.TerraScanCommon.UserId, publicValue, rollValue, this.linktypeid, string.Empty, this.tmpFileID, "TSFile");

                if (filePathData.Rows.Count > 0)
                {
                    AttachmentsData.SaveFilePathRow fileRow = (AttachmentsData.SaveFilePathRow)filePathData.Rows[0];
                    //Modified for Save during Field Mode
                    if (WSHelper.IsOnLineMode.Equals(false) && TerraScanCommon.IsFieldUser)
                    {
                        if (!fileRow.IsFileIDNull())
                        {
                            this.fileID = fileRow.FileID;
                            TerraScanCommon.AddFieldUseValues(Convert.ToInt32(this.fileID), this.keyField, this.formNo, null, TerraScanCommon.UserId);
                        }
                        else
                        {
                            this.fileID = string.Empty;
                        }
                        if (!fileRow.IsFilePathNull())
                        {
                            string FilePath = fileRow.FilePath;
                            string SpecialPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            string tempPath1 = SpecialPath + @"\Terrascan Attachment";
                            string foldPath = Path.GetPathRoot(FilePath);
                            string tempLocalpath = FilePath.Remove(0, foldPath.Length + 1);
                            tempPath1 = tempPath1 + '\\' + tempLocalpath;
                            this.filePath = tempPath1;
                        }
                        else
                        {
                            this.filePath = string.Empty;
                        }
                    }
                    else
                    {
                        if (!fileRow.IsFileIDNull())
                        {
                            this.fileID = fileRow.FileID;
                        }
                        else
                        {
                            this.fileID = string.Empty;
                        }

                        if (!fileRow.IsFilePathNull())
                        {
                            this.filePath = fileRow.FilePath;
                        }
                        else
                        {
                            this.filePath = string.Empty;
                        }
                    }
                }
                else
                {
                    this.fileID = string.Empty;
                    this.filePath = string.Empty;
                }


                this.fileurlpath = this.filePath;
                // this.fileID = fileId.ToString();
            }
            else
            {
                ////this.editOperation = true;
                /* int fileId = 0;
                fileId = this.fileTextBox.Text.Trim().LastIndexOf(".");
                int length = 0;
                length = this.fileTextBox.Text.Trim().Length; */
                if (TerraScanCommon.IsFieldUser)
                {
                    string updatePath = this.fileTextBox.Text;
                    string[] s = updatePath.Split('\\');
                    if (s.Length > 0)
                    {
                        int len = s.Length - 5;
                        if (s[len].Equals("IsFieldAdded"))
                        {
                            string path1 = this.fileurlpath;
                            string[] path = path1.Split('\\');
                            string[] originalPath = new string[path.Length + 1];

                            for (int index = originalPath.Length; index > 0; index--)
                            {
                                if (index == 5)
                                {
                                    originalPath.SetValue("IsFieldAdded", index - 1);
                                }
                                else if (index > 5)
                                    originalPath.SetValue(path[index - 2], index - 1);
                                else if (index < 5)
                                    originalPath.SetValue(path[index - 1], index - 1);
                            }
                            string temppath = string.Empty;
                            int strIndex = 0;
                            foreach (string currentString in originalPath)
                            {
                                if (strIndex == originalPath.Length || strIndex == 0)
                                    temppath += currentString;
                                else
                                    temppath += "\\" + currentString;
                                strIndex++;
                            }
                            this.fileurlpath = temppath;
                        }
                    }
                }

                string updateExtension = string.Empty;

                //// string tempSource = this.selectedFilePath;
                ///// string tempSource = this.attachmentGridView.Rows[this.currencyManager.Position].Cells["Source"].Value.ToString();
                int rowIndex = 0;
                rowIndex = this.GetRowIndex();

                if (this.attachmentDataSet.GetAttachmentItems.Rows.Count > 0)
                {
                    ////if (!string.IsNullOrEmpty(this.SourceTextBox.Text))
                    ////{
                    string temp = this.SourceTextBox.Text;
                    //// string temp = this.sourceTextBox.Tag.ToString();
                    string tempExt = string.Empty;
                    if (!string.IsNullOrEmpty(this.SourceTextBox.Text))
                    {
                        tempExt = temp.Substring(temp.LastIndexOf(".")).ToString();
                    }
                    else
                    {
                        string tempfile = this.fileTextBox.Text;
                        tempExt = tempfile.Substring(tempfile.LastIndexOf(".")).ToString();
                    }
                    ////}
                    if (this.browseSelected)
                    {
                        try
                        {
                            this.selectedFilePath = temp.Replace(tempExt, this.browsePathExt);
                            updateExtension = this.browsePathExt;
                        }
                        catch (Exception)
                        {
                            this.selectedFilePath = temp + this.browsePathExt;
                        }
                    }
                    else if (this.scanFile)
                    {
                        try
                        {
                            ////tempExt = temp.Substring(temp.LastIndexOf(".")).ToString();
                            this.selectedFilePath = temp.Replace(tempExt, this.scanFileExt);
                            updateExtension = this.scanFileExt;
                        }
                        catch (Exception)
                        {
                            this.selectedFilePath = temp + this.scanFileExt;
                        }

                        ////this.selectedFilePath = temp;
                    }
                    else
                    {
                        updateExtension = tempExt;
                    }
                }

                /* if (this.scanFile)
                {
                    ////updateExtension = this.fileTextBox.Text.Trim().Substring(fileId, (length - fileId));
                    updateExtension = this.scanFileExt;
                }
                else
                {
                    updateExtension = this.browsePathExt;
                } */

                // Save the Entries to Database.
                string tempfiletextboxvalue = this.fileTextBox.Text.Remove(this.fileTextBox.Text.Trim().LastIndexOf("."));
                ////int tempfiletextboxvalue1 = tempfiletextboxvalue.ToString().Trim().LastIndexOf("\\");
                ////int length = tempfiletextboxvalue.Length;
                string tempfiletextboxvalue2 = tempfiletextboxvalue.Substring(tempfiletextboxvalue.ToString().Trim().LastIndexOf("\\"));
                int filelength = tempfiletextboxvalue2.Trim().Length;
                string tempfiletextboxvalue3 = tempfiletextboxvalue2.Substring(1, filelength - 1);
                this.linktypeid = Convert.ToInt32(this.linktypeTextBox.Text);
                #region Wrong value in @FileID and @PFileID variables Bug Fixing
                // Fixing issue Attachments
                if (this.attachmentDataSet.GetAttachmentItems.Rows.Count > 0)
                {
                    if (Convert.ToInt32(this.attachmentDataSet.GetAttachmentItems.Rows[rowIndex]["FileID"]) > 0)
                    {
                        tempfiletextboxvalue3 = this.attachmentDataSet.GetAttachmentItems.Rows[rowIndex]["FileID"].ToString();
                    }

                }
                //Wrong value in @FileID and @PFileID variables Bug Fixing
                DataRow[] dr = this.attachmentDataSet.GetAttachmentItems.Select("FileID='" + tempfiletextboxvalue3 + "'");
                if (dr.Length > 0)
                {
                    this.tmpFileID = Convert.ToInt32(dr[0][18]);//Datarow is reference to datatable it will automatically update the datatable values
                }
                #endregion

                AttachmentsData.SaveFilePathDataTable filePathData = this.form9005Control.WorkItem.SaveAttachments(Convert.ToInt32(tempfiletextboxvalue3), updateExtension, this.attachmentFormID, this.attachmentKeyID, Convert.ToInt32(this.fileTypeIDTextBox.Text), this.SourceTextBox.Text, primaryValue, this.descriptionTextBox.Text.Trim(), this.attachmentDateTextBox.Text.Trim().ToString(), TerraScan.Common.TerraScanCommon.UserId, publicValue, rollValue, this.linktypeid, this.fileurlpath, this.tmpFileID, "TSFile");
                if (filePathData.Rows.Count > 0)
                {
                    AttachmentsData.SaveFilePathRow fileRow = (AttachmentsData.SaveFilePathRow)filePathData.Rows[0];
                    if (!fileRow.IsFileIDNull())
                    {
                        this.fileID = fileRow.FileID;
                        TerraScanCommon.InsertFieldUseDetails(Convert.ToInt32(this.fileID), this.keyField, TerraScanCommon.UserId);
                    }
                    else
                    {
                        this.fileID = string.Empty;
                    }

                    if (!fileRow.IsFilePathNull())
                    {
                        this.filePath = fileRow.FilePath;
                    }
                    else
                    {
                        this.filePath = string.Empty;
                    }
                }
                else
                {
                    this.fileID = string.Empty;
                    this.filePath = string.Empty;
                }

                this.fileurlpath = this.filePath;
            }

            //// Loads the DataGrivView from Database
            this.LoadAttachmentGrid();
            int foundIndex;
            if (this.buttonOperation == (int)ButtonOperation.New)
            {
                //DataView sortView = this.attachmentDataSet.GetAttachmentItems.DefaultView;
                //sortView.Sort = "FileID DESC";
                //DataSet attachmentIdDataset = new DataSet();
                //DataRow[] newRowAttachmenttID;
                //int dateFileId = 0;
                //newRowAttachmenttID = this.attachmentDataSet.GetAttachmentItems.Select("FileID = MAX(FileID)");
                //attachmentIdDataset.Merge(newRowAttachmenttID);
                //dateFileId = Convert.ToInt32(attachmentIdDataset.Tables[0].Rows[0]["FileID"].ToString());
                //BindingSource source = new BindingSource();
                //source.DataSource = this.attachmentDataSet.GetAttachmentItems;
                //foundIndex = source.Find("FileID", dateFileId);
                this.SetDataGridViewPosition(0);
                this.SetDataBindingValue(0);

            }
            else if (this.buttonOperation == (int)ButtonOperation.Empty)
            {
                //DataView sortView = this.attachmentDataSet.GetAttachmentItems.DefaultView;
                //sortView.Sort = "FileID DESC";
                //DataSet attachmentIdDataset = new DataSet();
                //DataRow[] newRowAttachmenttID;
                //int dateFileId = 0;
                //newRowAttachmenttID = this.attachmentDataSet.GetAttachmentItems.Select("FileID = MAX(FileID)");
                //attachmentIdDataset.Merge(newRowAttachmenttID);
                //dateFileId = Convert.ToInt32(attachmentIdDataset.Tables[0].Rows[0]["FileID"].ToString());
                //BindingSource source = new BindingSource();
                //source.DataSource = this.attachmentDataSet.GetAttachmentItems;
                //foundIndex = source.Find("FileID", dateFileId);
                this.SetDataGridViewPosition(0);
                this.SetDataBindingValue(0);
            }

            ////this.attachmentGridView.Enabled = true;
            this.browseButton.ImageSelected = false;
            this.scanButton.ImageSelected = false;
            // code commented
            // this.scanFile = false;
            this.tmpDataTable.Rows.Clear();
            this.browseSelected = false;
            this.InitializeButton();

            //// this.DisableAttachmentControl();
            this.valueChanged = false;

            ////// Set the Button Mode after saving the Record.
            //SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.SaveMode);
            //this.SetCurrentFormButtons(ButtonOperation.Save);
            //this.buttonOperation = (int)ButtonOperation.Empty;
            //this.descriptionTextBox.BackColor = System.Drawing.Color.White;
            //this.localFilePath = string.Empty;
            //this.SourceTextBox.ForeColor = System.Drawing.Color.FromArgb((int)(byte)(102), (int)(byte)(102), (int)(byte)(102));
        }

        /*  /// <summary>
        /// Gets the attachment count to display in Attachment Button.
        /// </summary>
        /// <param name="attachmentCount">The attachment count.</param>
        private void SetAttachmentCount(int attachmentCount)
        {
            /* if ((this.attachmentFormID == 1000) && (attachmentCount > 0))
            {
                // Display the Attachment button text with number of records for Reciept Engine.
                ((ReceiptEngineUserControl)((Panel)((Panel)this.Owner.ActiveMdiChild.Controls["ContentPanel"]).Controls["ReceiptEngineControlPanel"]).Controls["ReceiptEngineUserControl"]).AttachmentsButtonControl.Text = "Attachment(" + attachmentCount.ToString() + ")";
            }
            else if (this.attachmentFormID == 1000)
            {
                // Display the Attachment button text with number of records for Reciept Engine.
                ((ReceiptEngineUserControl)((Panel)((Panel)this.Owner.ActiveMdiChild.Controls["ContentPanel"]).Controls["ReceiptEngineControlPanel"]).Controls["ReceiptEngineUserControl"]).AttachmentsButtonControl.Text = "Attachment";
            }

            if ((this.attachmentFormID == 1020) && (attachmentCount > 0))
            {
                // Display the Attachment button text with number of records for Real Estate.
                ((Panel)((Panel)this.Owner.ActiveMdiChild.Controls["ContentPanel"]).Controls["Panel3"]).Controls["AttachmentButton"].Text = "Attachment(" + attachmentCount.ToString() + ")";
            }
            else if (this.attachmentFormID == 1020)
            {
                // Display the Attachment button text with number of records for Real Estate.
                ///((Panel)((Panel)this.Owner.ActiveMdiChild.Controls["ContentPanel"]).Controls["Panel3"]).Controls["AttachmentButton"].Text = "Attachment";
            } 
        } */

        /// <summary>
        /// Saves the images to Central Location.
        /// </summary>
        private void SaveImage()
        {
            string tempFilePath = string.Empty;

            if (this.scanFile && this.scanSelected)
            {
                //// Gets the temporary scan Image filepath
                tempFilePath = this.scanTmpPath;
            }
            else if (this.browseSelected)
            {
                //// Gets the temporary Browse Image filepath
                tempFilePath = this.browseTmpPath;
            }
            else
            {
                //// Used to select the filepath which has no changes in images to store.
                //// tempFilePath = this.GetSelectedFilePath(this.currentRow);
                ////tempFilePath = this.sourceTextBox.Text;

                if (string.IsNullOrEmpty(this.SourceTextBox.Text))
                {
                    if (this.SourceTextBox.Tag != null)
                    {
                        tempFilePath = this.SourceTextBox.Tag.ToString();
                    }
                }
                else
                {
                    tempFilePath = this.SourceTextBox.Text;
                }
            }

            if (this.buttonOperation == (int)ButtonOperation.New)
            {
                try
                {
                    //// Checks whether the file exists.
                    if (System.IO.File.Exists(tempFilePath))
                    {
                        if (!string.IsNullOrEmpty(this.filePath))
                        {
                            FileStream fs = null;
                            //try
                            //{
                            fs = new FileStream(tempFilePath, FileMode.Open);
                            //}
                            //catch (Exception ex)
                            //{
                            //}
                            BinaryReader bR = new BinaryReader(fs);

                            //// Upload the Image to the Central Location.
                            UpLoadImage(bR.ReadBytes((int)fs.Length), this.filePath);
                            this.fileExist = true;
                            this.validPermission = true;
                            bR.Close();
                            fs.Close();

                            if (this.scanTmpPath.Length > 0)
                            {
                                //// Delete the Image in Temporary location.
                                ////System.IO.File.Delete(tempFilePath);

                                DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");

                                if (dirInfo.Exists)
                                {
                                    FileInfo[] fileList = dirInfo.GetFiles();

                                    foreach (FileInfo file in fileList)
                                    {
                                        if (file.Name != "Thumbs.db" && file.Extension != ".tif")
                                        {
                                            System.IO.File.Delete(file.FullName);
                                        }
                                    }
                                }
                                else
                                {
                                    ////Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");
                                    ////centalFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "FinalImage.tif";
                                }
                            }
                        }
                        else
                        {
                            throw new IOException();
                        }
                    }
                    else
                    {
                        this.fileExist = false;
                    }
                }
                catch (IOException)
                {
                    //// Calls the Errorform to display the Error.                    
                    //// ErrorForm showError = new ErrorForm(SharedFunctions.GetResourceString("TS_File"), SharedFunctions.GetResourceString("NoAccessFileLocation"), tempFilePath, " ", SharedFunctions.GetResourceString("ContactAdmin"));
                    //// showError.ShowDialog();
                    throw;
                    /* MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", tempFilePath, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.validPermission = false;
                    this.browseButton.ImageSelected = false;
                    this.scanButton.ImageSelected = false;
                    this.scanFile = false;
                    this.valueChanged = false;
                    this.browseTmpPath = string.Empty;
                    this.localFilePath = string.Empty;
                    this.browsePathExt = string.Empty;
                    this.browseSelected = false;
                    this.scanSelected = false;
                    this.scanButton.Enabled = true;
                    this.tmpDataTable.Clear();
                    ////this.SetDataGridViewPosition(0);
                    ////this.SetDataBindingValue(0);
                    ////SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.CancelMode);
                    ////this.SetCurrentFormButtons(ButtonOperation.Cancel);
                    ////this.buttonOperation = (int)ButtonOperation.Empty;
                    ////this.attachmentGridView.Enabled = true;
                    this.SetFocus();*/
                }
            }
            else
            {
                //// used to create dataset to store the images to be deleted
                string stringFileID;
                DataRow[] foundRows;
                string tempImageFile = string.Empty;
                string tempImageExt = string.Empty;

                if (this.scanFile)
                {
                    tempImageFile = this.scanTmpPath;
                    tempImageExt = this.scanFileExt;
                }
                else
                {
                    tempImageFile = this.browseTmpPath;
                    tempImageExt = this.browsePathExt;
                }

                ////if (string.IsNullOrEmpty(tempImageFile))
                ////{
                ////    tempImageFile = this.fileTextBox.Text;
                ////    tempImageExt = ".tiff";
                ////}

                //// Get the changes in the tmpDataTable.
                this.tmpImageTable = this.tmpDataTable.GetChanges();
                if (this.tmpDataTable.Rows.Count > 0)
                {
                    this.CheckPermission();
                }
                else
                {
                    this.validPermission = true;
                }

                if (this.validPermission)
                {
                    if (this.tmpImageTable != null && this.tmpImageTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in this.tmpImageTable.Rows)
                        {
                            try
                            {
                                stringFileID = "FileID =" + row[0];
                                foundRows = this.attachmentDataSet.GetAttachmentItems.Select(stringFileID);

                                string existsFilePath = string.Empty;
                                int currentFileID = 0;
                                if (this.AttachmentGridView.CurrentRow != null)
                                {
                                    int.TryParse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells[this.AttachmentFileID.Name].Value.ToString(), out currentFileID);
                                }

                                existsFilePath = this.form9005Control.WorkItem.GetOriginalFilePath(currentFileID, TerraScanCommon.UserId);

                                #region TSBG - D9005.F9005 Attachments form - Incorrect @FileID and @PFileID variables at save
                                string validPath = this.fileTextBox.Text.Substring(this.fileTextBox.Text.LastIndexOf("\\"));
                                validPath = validPath.Replace("\\", "");
                                validPath = validPath.Remove(validPath.LastIndexOf("."));
                                string AttachmentFileID = this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString();
                                if (validPath != AttachmentFileID)
                                {
                                    flagFile = false;
                                }
                                #endregion
                                if (TerraScanCommon.IsFieldUser)
                                {
                                    string FilePath = existsFilePath;
                                    string SpecialPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                                    string tempPath1 = SpecialPath + @"\Terrascan Attachment";
                                    string foldPath = Path.GetPathRoot(FilePath);
                                    string tempLocalpath = FilePath.Remove(0, foldPath.Length + 1);
                                    tempPath1 = tempPath1 + '\\' + tempLocalpath;

                                    string path1 = tempPath1;
                                    string[] path = path1.Split('\\');
                                    string[] originalPath = new string[path.Length + 1];

                                    for (int index = originalPath.Length; index > 0; index--)
                                    {
                                        if (index == 7)
                                        {
                                            originalPath.SetValue("IsFieldAdded", index - 1);
                                        }
                                        else if (index > 7)
                                            originalPath.SetValue(path[index - 2], index - 1);
                                        else if (index < 7)
                                            originalPath.SetValue(path[index - 1], index - 1);
                                    }
                                    string temppath = string.Empty;
                                    int strIndex = 0;
                                    foreach (string currentString in originalPath)
                                    {
                                        if (strIndex == originalPath.Length || strIndex == 0)
                                            temppath += currentString;
                                        else
                                            temppath += "\\" + currentString;
                                        strIndex++;
                                    }
                                    existsFilePath = temppath;
                                }

                                if (System.IO.File.Exists(tempImageFile))
                                {
                                   
                                    if (!string.IsNullOrEmpty(this.fileTextBox.Text) && !string.IsNullOrEmpty(existsFilePath))
                                    {
                                        #region TSBG - D9005.F9005 Attachments form - Incorrect @FileID and @PFileID variables at save
                                        if (flagFile)
                                        {
                                            if (System.IO.File.Exists(this.fileTextBox.Text))
                                            {
                                                    //// Delete the Image in Temporary location.
                                                System.IO.File.Delete(this.fileTextBox.Text);
                                            }
                                        }


                                        FileStream fs = new FileStream(tempImageFile, FileMode.Open);
                                        BinaryReader bR = new BinaryReader(fs);
                                        #endregion

                                        string temp = string.Empty;
                                        try
                                        {
                                            temp = this.fileTextBox.Text.Substring(this.fileTextBox.Text.LastIndexOf("."));
                                        }
                                        catch
                                        {
                                            temp = this.fileTextBox.Text + tempImageExt;
                                        }

                                      

                                        //// string temp = this.attachmentGridView.Rows[this.selectedRow].Cells["Extension"].Value.ToString();

                                        //// Upload the Image to the Central Location.  

                                        if (flagFile)
                                        {
                                            UpLoadImage(bR.ReadBytes((int)fs.Length), this.fileTextBox.Text.Replace(temp, tempImageExt));
                                        }

                                        this.fileExist = true;
                                        this.validPermission = true;
                                        bR.Close();
                                        fs.Close();
                                        if (this.scanTmpPath.Length > 0)
                                        {
                                            // Delete the Image in Temporary location.
                                            ////System.IO.File.Delete(tempFilePath);
                                        }
                                    }
                                    else
                                    {
                                        throw new UnauthorizedAccessException();
                                    }
                                }
                                else
                                {
                                    this.validPermission = true;
                                    this.fileExist = false;
                                    this.tmpDataTable.Clear();
                                    break;
                                }
                            } ////For Try           
                            catch (FieldAccessException ex)
                            {
                                MessageBox.Show(ex.Message, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                //// Gets the Errorform to display the Error.
                                //// ErrorForm showError = new ErrorForm(SharedFunctions.GetResourceString("TS_File"), SharedFunctions.GetResourceString("NoAccessFileLocation"), tempFilePath, " ", SharedFunctions.GetResourceString("ContactAdmin"));
                                //// showError.ShowDialog();

                                MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", tempFilePath, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.tmpDataTable.Clear();
                                this.browseButton.ImageSelected = false;
                                this.scanButton.ImageSelected = false;
                                this.scanFile = false;
                                this.validPermission = false;
                                this.browseButton.Enabled = true;
                                this.scanButton.Enabled = true;
                                this.browseTmpPath = string.Empty;
                                this.localFilePath = string.Empty;
                                this.browsePathExt = string.Empty;
                                this.browseSelected = false;
                                this.scanSelected = false;
                                this.scanButton.Enabled = true;
                                this.SetFocus();
                                break;
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(tempFilePath))
                    {
                        if (System.IO.File.Exists(tempFilePath))
                        {
                            //// Delete the Image in Temporary location.
                            this.validPermission = true;
                            this.fileExist = true;
                            this.tmpDataTable.Clear();
                        }
                        else
                        {
                            this.validPermission = true;
                            this.fileExist = false;
                            this.tmpDataTable.Clear();
                        }
                    }
                }
                else
                {
                    //// ErrorForm showError = new ErrorForm(SharedFunctions.GetResourceString("TS_File"), SharedFunctions.GetResourceString("NoAccessFileLocation"), tempFilePath, " ", SharedFunctions.GetResourceString("ContactAdmin"));
                    //// showError.ShowDialog();

                    MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", tempFilePath, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tmpDataTable.Clear();
                    this.browseButton.ImageSelected = false;
                    this.scanButton.ImageSelected = false;
                    this.scanFile = false;
                    this.validPermission = false;
                    this.browseSelected = false;
                    this.scanSelected = false;
                    this.browseTmpPath = string.Empty;
                    this.localFilePath = string.Empty;
                    this.browsePathExt = string.Empty;
                    this.scanButton.Enabled = true;
                    this.SetFocus();
                }
            }
        }

        /// <summary>
        /// Checks the permission for the file.
        /// </summary>
        private void CheckPermission()
        {
            try
            {
                if (this.scanFile)
                {
                    FileStream fs = new FileStream(this.scanTmpPath, FileMode.Open);
                    fs.Close();
                }
                else
                {
                    FileStream fs = new FileStream(this.browseTmpPath, FileMode.Open);
                    fs.Close();
                }

                this.validPermission = true;
            }
            catch
            {
                this.validPermission = false;
            }
        }

        /// <summary>
        /// Creates the scan folder.
        /// </summary>
        private void CreateScanFolder()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");

            if (dirInfo.Exists)
            {
                ////Do Nothing
            }
            else
            {
                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");
            }
        }

        /// <summary>
        /// Gets the Scan image file path.
        /// </summary>
        private void ScanFilePath()
        {
            string finalImage = string.Empty;
            this.CreateScanFolder();
            F9006 scanForm = new F9006();
            //// Shows the AttachmentScanning for Scanning.
            if (scanForm.ShowDialog() == DialogResult.Yes)
            {
                //// Gets the Scaned Images from Temporary Location.

                finalImage = scanForm.ScanFilePath;
                ////this.scanTmpPath = centalFilePath;
                this.scanTmpPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "ScanFile" + DateTime.Now.Ticks + ".tiff";

                if (System.IO.File.Exists(finalImage))
                {
                    ////File.Delete(newfilepath);
                    System.IO.File.Copy(finalImage, this.scanTmpPath);
                }
                else
                {
                    this.scanTmpPath = finalImage;
                }

                if (scanForm.SuccesfullyScanned)
                {
                    if (System.IO.File.Exists(this.scanTmpPath))
                    {
                        FileInfo file = new FileInfo(this.scanTmpPath);
                        this.scanFileExt = file.Extension;
                        this.tmpDataRow = this.tmpDataTable.NewRow();
                        this.tmpDataRow["FileID"] = this.tmpFileID;
                        this.tmpDataRow["Path"] = this.scanTmpPath;
                        this.tmpDataTable.Rows.Add(this.tmpDataRow);
                        this.scanButton.ImageSelected = true;
                        this.scanSelected = true;
                        this.scanFile = true;
                        this.valueChanged = true;
                        ////Added by malliga on 2/5/2008
                        this.linkedfileidTextBox.Text = this.tmpFileID.ToString();
                        ////End of Added

                        //// Set the Button mode after the scan operation.
                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                        this.SetCurrentFormButtons(ButtonOperation.Scan);
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ScanFileNotFound"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                this.scanButton.ImageSelected = false;
                scanForm.SuccesfullyScanned = false;
                this.scanFile = false;
            }
        }

        /// <summary>
        /// Browses the file path and save it temp file path.
        /// </summary>
        private void BrowseFilePath()
        {
            //// Filter all other files except the file with the extension given below.
            this.browseOpenDialog.Filter = "Windows Bitmaps(*.bmp)|*.bmp|JPEG Files(*.jpg)|*.jpg|GIF Files(*.gif)|*.gif|TIFF Files(*.tif)|*.tif|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            this.browseOpenDialog.FilterIndex = 6;
            this.browseOpenDialog.Multiselect = false;
            this.browseOpenDialog.FileName = string.Empty;

            // First approach
            // Before Selecting file from openfiledialog box, default path from tts_cfg will be consider as initial directory
            if (!TerraScanCommon.HasFilePathChanged)
            {
                string defaultPath = "TS_Attachment_DefaultFileLocation";
                CommentsData defaultDirectoryData = new CommentsData();
                defaultDirectoryData = this.form9005Control.WorkItem.GetConfigDetails(defaultPath);
                if (defaultDirectoryData.GetCommentsConfigDetails.Rows.Count > 0 && !string.IsNullOrEmpty(defaultDirectoryData.GetCommentsConfigDetails.Rows[0][0].ToString()))
                {
                    string rootPath = defaultDirectoryData.GetCommentsConfigDetails.Rows[0][0].ToString().Trim();

                    try
                    {
                        // Validate wheather its a system special folder
                        bool isSpecialFolder = false;
                        System.Environment.SpecialFolder typeSpecialFolder;
                        foreach (System.Environment.SpecialFolder atypeSpecialFolder in
                                          System.Enum.GetValues(typeof(Environment.SpecialFolder)))
                        {
                            if (atypeSpecialFolder.ToString().Equals(rootPath))
                            {
                                isSpecialFolder = true;
                                // Get the full path of special folder and assign as initial directory
                                browseOpenDialog.InitialDirectory = System.Environment.GetFolderPath(atypeSpecialFolder);
                                break;
                            }
                        }

                        // If the folder is not a system special folder
                        if (!isSpecialFolder)
                        {
                            if (!rootPath.EndsWith(@"\"))
                            {
                                rootPath = rootPath + @"\";
                            }

                            // If the directory exists in the system assign that as initial directory
                            if (System.IO.Directory.Exists(rootPath))
                            {
                                browseOpenDialog.InitialDirectory = rootPath;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // TO DO:
                    }
                }
            }

            // Second approach
            // C: will be the initial directory while open filedialog box
            // browseOpenDialog.InitialDirectory = "C:\\";

            //// Shows the OpenDialog window.
            if (this.browseOpenDialog.ShowDialog(this) != DialogResult.Cancel)
            {
                this.valueChanged = true;
                TerraScanCommon.HasFilePathChanged = true;
                //// Gets the Browse file path.
                this.browseTmpPath = this.browseOpenDialog.FileName.ToString();
                this.localFilePath = this.browseOpenDialog.FileName.ToString();
                this.SourceTextBox.Text = this.browseTmpPath;

                ////this.SetSourceFileName(this.browseTmpPath);
                this.SourceTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                //// Gets the extension of the image.
                this.browsePathExt = Path.GetExtension(browseTmpPath).ToLower();
                ////Added for bugid-2503
                if (!string.IsNullOrEmpty(this.fileTextBox.Text))
                {
                    if (!string.IsNullOrEmpty(this.browsePathExt))
                    {
                        string filext;
                        filext = Path.GetExtension(this.fileTextBox.Text);

                        if (filext != this.browsePathExt)
                        {
                            string tempExt = string.Empty;
                            tempExt = this.fileTextBox.Text.Substring(this.fileTextBox.Text.LastIndexOf(".")).ToString();
                            this.fileTextBox.Text = this.fileTextBox.Text.Replace(tempExt, this.browsePathExt);
                        }
                    }
                }

                //// Checks for Extension.
                if (!String.IsNullOrEmpty(this.browsePathExt.ToString().Trim()))
                {
                    this.tmpDataRow = this.tmpDataTable.NewRow();

                    //// Gets the fileID
                    this.tmpDataRow["FileID"] = this.tmpFileID;

                    /*  // Gets the Path to stroe
                    this.tmpDataRow["Path"] = this.browseTmpPath;
                    if (this.buttonOperation != (int)ButtonOperation.New)
                    {
                        // Gets the Path to stroe
                        this.tmpDataRow["DeletePath"] = this.attachmentGridView.Rows[this.currentRow].Cells["source"].Value.ToString();

                        // Used to get the Extension of the file from Browse dialog window.
                        int endIndex;
                        endIndex = (this.browseTmpPath.Length - this.browseTmpPath.LastIndexOf("."));
                        int endIndex1;
                        endIndex1 = (this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().Length - this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().LastIndexOf("."));

                        int endIndex2;
                        endIndex2 = (this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().Length - this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().LastIndexOf("."));
                        int start2;
                        start2 = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().LastIndexOf(".");

                        // attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value = attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().Replace(attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().Substring(attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().LastIndexOf("."), endIndex1).ToString(), this.browseTmpPath.Substring(this.browseTmpPath.LastIndexOf("."), endIndex)); 

                    // Used to get the Extension of the file from Browse dialog window.
                    string tmpValue = string.Empty;
                    string tmpReplace = string.Empty;
                    int startIndex;
                    startIndex = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().LastIndexOf(".");
                    tmpReplace = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().Substring(startIndex, endIndex1).ToString();
                    tmpValue = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value.ToString().Replace(tmpReplace, this.browseTmpPath.Substring(this.browseTmpPath.LastIndexOf("."), endIndex));
                    this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["File"].Value = tmpValue;

                    string tmp = string.Empty;
                    tmp = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().Substring(start2, endIndex2);
                    this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Source"].Value.ToString().Replace(tmp, this.browseTmpPath.Substring(this.browseTmpPath.LastIndexOf("."), endIndex));
                    this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Extension"].Value = this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Extension"].Value.ToString().Replace(this.attachmentGridView.Rows[Convert.ToInt32(this.currentRow)].Cells["Extension"].Value.ToString(), this.browseTmpPath.Substring(this.browseTmpPath.LastIndexOf("."), endIndex)); 
                }
                else
                {
                    this.tmpDataRow["DeletePath"] = string.Empty;
                }  */

                    this.tmpDataTable.Rows.Add(this.tmpDataRow);
                    this.SetFocus();
                    if (this.scanFile)
                    {
                        this.scanButton.ImageSelected = true;
                        this.SetCurrentFormButtons(ButtonOperation.Scan);
                    }
                    else
                    {
                        this.browseButton.ImageSelected = true;
                        this.SetCurrentFormButtons(ButtonOperation.Browse);
                    }

                    ////this.openButton.Enabled = false;
                    ////this.scanButton.Enabled = false;
                    //// this.currencyManager.EndCurrentEdit();
                }
                else
                {
                    //// Show the Message through Error Form.
                    //// ErrorForm errorForm = new ErrorForm(SharedFunctions.GetResourceString("ExtensionMissing"), SharedFunctions.GetResourceString("FileExtensionMissing"), SharedFunctions.GetResourceString("UnabletoSave"), "", string.Empty);
                    //// errorForm.ShowDialog();

                    MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("FileExtensionMissing"), "\n", SharedFunctions.GetResourceString("UnabletoSave") }), SharedFunctions.GetResourceString("ExtensionMissing"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.scanButton.Enabled = true;
                }
            }
            else
            {
                //// SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.CancelMode);
                //// this.SetCurrentFormButtons(ButtonOperation.Cancel);
                //// SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                //// this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);

                if (!this.browseButton.ImageSelected)
                {
                    if (this.emptyRecord)
                    {
                        //// this.ClearHeader();
                        //// this.InitializeButton();

                        //// this.DisableAttachmentControl();
                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                        this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                        this.SetFocus();

                        //// this.buttonOperation = (int)ButtonOperation.Empty;
                    }

                    if (this.buttonOperation != (int)ButtonOperation.New)
                    {
                        if (!this.valueChanged)
                        {
                            this.InitializeButton();
                        }

                        //// call the method to browse a file.
                        this.browseSelected = false;
                        this.browseButton.ImageSelected = false;
                        this.scanButton.ImageSelected = false;
                        this.scanFile = false;
                        this.scanButton.Enabled = true;
                        this.URLbutton.Enabled = true;
                        this.SetFocus();
                    }
                    else
                    {
                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.NewMode);
                        this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                        this.SetLostFocus();
                    }
                }
                else
                {
                    this.descriptionTextBox.Focus();
                    this.descriptionTextBox.SelectAll();
                }
            }
        }

        /// <summary>
        /// Selects the open file path.
        /// </summary>
        private void SelectOpenFilePath()
        {
            if (this.CheckAttachmentGrid())
            {
                this.selectedFilePath = this.GetSelectedFilePath();
            }
        }

        /// <summary>
        /// Attachments form close.
        /// </summary>
        private void AttachmentClose()
        {
            if (VaildDataSet(this.attachmentDataSet))
            {
                /* int attachmentCounts = 0;
                try
                {
                    attachmentCounts = this.form9005Control.WorkItem.GetAttachmentCount(this.attachmentFormID, this.attachmentKeyID, TerraScan.Common.TerraScanCommon.UserId);
                }
                catch (SoapException ex)
                {
                    ////TODO : Need to find specific exception and handle it.
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                }  */

                //// Gets number of record for the attachment.
                //// this.SetAttachmentCount(attachmentCounts);

                //// Checks whether there is any changes in local dataset.
                if (this.valueChanged || this.buttonOperation == (int)ButtonOperation.New)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.closeButton = true;
                        this.Close();
                    }
                    else
                    {
                        this.SetFocus();
                    }
                }
                else
                {
                    this.closeButton = true;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Shows the attachment calender in particular location.
        /// </summary>
        private void ShowAttachmentCalender()
        {
            this.attachmentMonthCalander.Visible = true;
            this.attachmentMonthCalander.ScrollChange = 1;

            //// Display the Calender control near the Calender Picture box.
            this.attachmentMonthCalander.Left = this.datePanel.Left + this.attachmentDatePictureBox.Left + this.attachmentDatePictureBox.Width;
            this.attachmentMonthCalander.Top = this.datePanel.Top + this.attachmentDatePictureBox.Top;
            this.attachmentMonthCalander.Tag = this.attachmentDatePictureBox.Tag;
            this.attachmentMonthCalander.Focus();

            if (!string.IsNullOrEmpty(this.attachmentDateTextBox.Text))
            {
                this.attachmentMonthCalander.SetDate(Convert.ToDateTime(this.attachmentDateTextBox.Text));
            }
        }

        /// <summary>
        /// Creates the source data table.
        /// </summary>
        private void CreateSourceDataTable()
        {
            DataColumn dataColumnRowId = new DataColumn("RowID");
            DataColumn dataColumnOldPath = new DataColumn("Path");
            this.tempSourcePath.Columns.Add(dataColumnRowId);
            this.tempSourcePath.Columns.Add(dataColumnOldPath);
        }

        /// <summary>
        /// Fills the temp file path.
        /// </summary>
        private void FillTempFilePath()
        {
            DataRow filePathRow;
            int rowID = 0;
            foreach (DataRow row in this.attachmentDataSet.GetAttachmentItems.Rows)
            {
                filePathRow = this.tempSourcePath.NewRow();
                filePathRow["RowID"] = rowID;
                filePathRow["Path"] = row["Source"];
                rowID++;
                this.tempSourcePath.Rows.Add(filePathRow);
            }
        }

        /// <summary>
        /// Checks the office scanner.
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckOfficeScanner()
        {
            ////string path = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + SharedFunctions.GetResourceString("MSOfficeFilePath") + "\\" + SharedFunctions.GetResourceString("MSOFFiceExe");

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + SharedFunctions.GetResourceString("MSOfficeFilePath");
            string[] modiFolder = Directory.GetDirectories(path);

            if (modiFolder.Length > 0)
            {
                foreach (string folder in modiFolder)
                {
                    string modiPath = folder + "\\" + SharedFunctions.GetResourceString("MSOFFiceExe");

                    if (System.IO.File.Exists(modiPath))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #region Issue Fixed - BugID #1409 (Source File field should be right justified)
        ////Added by Latha

        /// <summary>
        /// Sets the name of the source file with Right Alignment.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void SetSourceFileName(string fileName)
        {
            this.SourceTextBox.Tag = fileName;

            if (fileName.Length > 38)
            {
                fileName = fileName.Substring(fileName.Length - 38);
            }
            else
            {
                fileName = this.SourceTextBox.Tag.ToString();
            }

            Graphics graphics = this.CreateGraphics();
            ////Get the width of the string
            SizeF sizeF = graphics.MeasureString(fileName, this.Font);
            float preferredwidth = sizeF.Width;
            int decrement = 37;
            ////Compare the string width and required width of textbox
            while (preferredwidth > 196)
            {
                ////If string width is greater than required width, remove the first character from the string
                if (preferredwidth > 196)
                {
                    fileName = fileName.Substring(fileName.Length - decrement);
                    sizeF = graphics.MeasureString(fileName, this.Font);
                    preferredwidth = sizeF.Width;
                    decrement = decrement - 1;
                }
            }

            this.SourceTextBox.Text = fileName;
            graphics.Dispose();
        }

        #endregion Issue Fixed - BugID #1409 (Source File field should be right justified)

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the DescriptionTextPanel control and focus the cursor to Description textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextPanel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.descriptionTextBox.Enabled)
                {
                    this.descriptionTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NewAttachmentButton control and clear all fields.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewAttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.newAttachmentButton.Enabled)
                {
                    //// Clears all the field and load the value to the know entry
                    this.EnableAttachmentControl();
                    this.buttonOperation = (int)ButtonOperation.New;
                    this.attachmentDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                    this.userTextBox.Text = TerraScan.Common.TerraScanCommon.UserName;
                    this.AttachmentGridView.Enabled = false;
                    this.publicCheckBox.Checked = false;
                    this.primaryCheckBox.Checked = false;
                    //this.WillRollCheckBox.Checked = false;
                    this.descriptionTextBox.Text = string.Empty;
                    this.fileTextBox.Text = string.Empty;
                    this.fileurlpath = string.Empty;
                    this.SourceTextBox.Text = string.Empty;
                    this.linktypeTextBox.Text = string.Empty;

                    if (this.attachmentDataSet.GetAttachementFunctionName.Rows.Count > 0)
                    {
                        //// Loads the Data to Combo box.
                        this.typeComboBox.DataSource = this.attachmentDataSet.GetAttachementFunctionName;
                        this.typeComboBox.DisplayMember = "FunctionName";
                        this.typeComboBox.ValueMember = "FileTypeID";
                        this.typeComboBox.SelectedIndex = 0;
                        this.fileTypeIDTextBox.Text = this.typeComboBox.SelectedValue.ToString();
                    }

                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.NewMode);
                    this.SetCurrentFormButtons(ButtonOperation.New);
                    this.currentRow = 0;
                    this.attachmentDataRow = this.attachmentDataSet.GetAttachmentItems.NewRow();
                    this.attachmentDateTextBox.Focus();

                    //// SetFocus();
                    if (this.CheckAttachmentGrid())
                    {
                        this.AttachmentGridView.Rows[0].Selected = false;
                    }

                    this.attachmentDateTextBox.LockKeyPress = false;
                    this.descriptionTextBox.LockKeyPress = false;
                    this.AttachmentGridView.CurrentCell = null;
                    this.newAttachmentButton.Focus();
                    this.descriptionTextBox.Text = string.Empty;

                    if (this.auditLinkLabel.Text.Length > 0)
                    {
                        this.auditLinkLabel.Text = "tTS_File[FileID]";
                        this.auditLinkLabel.Enabled = false;
                    }
                    //Assigned value to tmpFileID to fix issue 21084 by purushotham
                    this.tmpFileID = 0;
                    this.browseButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.browseButton.StatusOffColor = Color.FromArgb(28, 81, 128);
                    this.URLbutton.BackColor = Color.FromArgb(28, 81, 128);
                    this.URLbutton.StatusOffColor = Color.FromArgb(28, 81, 128);
                    this.scanButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.scanButton.StatusOffColor = Color.FromArgb(28, 81, 128);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelAttachmentButton control and cancel the current operation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelAttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////if (MessageBox.Show(SharedFunctions.GetResourceString("AttachmentCancel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                ////{
                this.DialogResult = DialogResult.None;
                int rowSelected = 0;
                rowSelected = this.GetRowIndex();
                this.DiscardChanges();
                this.valueChanged = false;

                this.SetDataGridViewPosition(rowSelected);
                this.SetDataBindingValue(rowSelected);
                this.auditLinkLabel.Enabled = true;
                this.localFilePath = string.Empty;

                if (this.emptyRecord)
                {
                    //// if (this.attachmentGridViewEmpty.Rows.Count > 0)
                    //// {
                    //// this.attachmentGridViewEmpty.Rows[0].Selected = false;
                    //// }

                    this.DisableAttachmentControl();
                    this.ClearHeader();
                    this.AttachmentGridView.Rows[0].Selected = false;
                    this.auditLinkLabel.Enabled = false;
                }
                else
                {
                    this.EnableAttachmentControl();
                    this.CheckEditPermission();
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.CancelMode);
                    this.SetCurrentFormButtons(ButtonOperation.Cancel);

                }

                this.SourceTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
                ////Coding Commeted and added for the Issue 4237
                ////this.AttachmentGridView.Focus();
                this.attachmentDateTextBox.Focus();
                ////}
                ////else
                ////{
                ////    this.SetFocus(); 
                ////}
                ////Ends here
                ////Coding Commented for 4238
                ////this.linktypeTextBox.Text = "";
                ////Ends Here

                /* *******************************
                this.DialogResult = DialogResult.None;

                // Checks wheather there is any change .If it has no changes, closes the current operation. 
                if (this.attachmentDataSet.HasChanges() || this.valueChanged || this.buttonOperation == (int)ButtonOperation.New || this.browseSelected || this.scanSelected)
                {       
                    if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), SharedFunctions.GetAppConfigString("ApplicationName"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.DiscardChanges();                       
                        this.valueChanged = false;
                        this.SetDataGridViewPosition(0);
                        this.SetDataBindingValue(0);                        
                        this.auditLinkLabel.Enabled = true;

                        if (this.emptyRecord)
                        {
                            // if (this.attachmentGridViewEmpty.Rows.Count > 0)
                            // {
                                // this.attachmentGridViewEmpty.Rows[0].Selected = false;
                            // }

                            this.DisableAttachmentControl();
                            this.ClearHeader();
                        }

                        this.attachmentGridView.Focus();                        
                    }
                    else
                    {
                        if (this.buttonOperation == (int)ButtonOperation.New)
                        {
                            this.attachmentDateTextBox.Focus();
                            this.attachmentDateTextBox.SelectAll();
                        }
                        else
                        {
                            this.descriptionTextBox.Focus();
                            this.descriptionTextBox.SelectAll();
                        }
                    }
                }  
                ****************** */
                this.textchange = false;
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the AttachmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AttachmentGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.emptyRecord)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (!this.valueChanged)
                        {
                            this.selectedRow = e.RowIndex;
                        }

                        this.selectedRow = e.RowIndex;
                        this.currentRow = e.RowIndex;
                        //// this.attachmentGridView.CurrentCell.Selected = true;

                        if (!string.IsNullOrEmpty(this.AttachmentGridView.Rows[e.RowIndex].Cells["AttachmentFileID"].Value.ToString()))
                        {
                            if (e.ColumnIndex >= -1)
                            {
                                this.EnableAttachmentControl();
                            }

                            if (this.currencyManager != null && this.currencyManager.Position >= 0)
                            {
                                this.currencyManager.Position = e.RowIndex;
                                this.currencyManager.EndCurrentEdit();
                            }

                            //// this.SetDataBindingValue(e.RowIndex);

                            if (e.RowIndex >= 0 && e.ColumnIndex >= -1)
                            {
                                //// Checks the value chaged in Datagrid.
                                if (this.valueChanged)
                                {
                                    if (this.tempRowId != e.RowIndex)
                                    {
                                        ////this.editOperation = true;

                                        switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                        {
                                            case DialogResult.Yes:
                                                {
                                                    this.SaveRecords();

                                                    if (this.closingNow)
                                                    {
                                                        this.LoadAttachmentGrid();
                                                        this.tempRowId = e.RowIndex;
                                                        this.DiscardChanges();
                                                        this.valueChanged = false;
                                                        this.EnableAttachmentControl();
                                                        this.SetFocus();
                                                        this.SetDataBindingValue(e.RowIndex);
                                                        this.SetDataGridViewPosition(e.RowIndex);
                                                    }
                                                    else
                                                    {
                                                        this.SetDataGridViewPosition(this.tempRowId);
                                                        this.descriptionTextBox.Focus();
                                                    }

                                                    break;
                                                }

                                            case DialogResult.No:
                                                {
                                                    this.tempRowId = e.RowIndex;
                                                    this.DiscardChanges();
                                                    this.valueChanged = false;
                                                    this.EnableAttachmentControl();
                                                    this.SetDataBindingValue(e.RowIndex);
                                                    this.SetDataGridViewPosition(e.RowIndex);
                                                    this.AttachmentGridView.Focus();
                                                    break;
                                                }

                                            case DialogResult.Cancel:
                                                {
                                                    this.TempData();
                                                    this.SetDataGridViewPosition(this.tempRowId);
                                                    this.SetFocus();
                                                    break;
                                                }
                                        }

                                        /************
                                        if (MessageBox.Show(SharedFunctions.GetResourceString("UtilityCancel"), SharedFunctions.GetAppConfigString("ApplicationName"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            this.tempRowId = e.RowIndex;
                                            this.DiscardChanges();
                                            this.valueChanged = false;
                                            this.EnableAttachmentControl();
                                            this.SetFocus();                                          
                                            this.SetDataBindingValue(e.RowIndex);
                                            this.SetDataGridViewPosition(this.selectedRow);

                                            //// SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                                            //// this.SetCurrentFormButtons(ButtonOperation.QueryUtilityDataGrid);
                                        }
                                        else
                                        {
                                            this.TempData();
                                            this.SetDataGridViewPosition(this.tempRowId);

                                            // this.SetFocus();
                                        }
                                         * ***************/
                                    }
                                    else
                                    {
                                        this.TempData();

                                        if (this.browseButton.ImageSelected)
                                        {
                                            this.browseButton.Enabled = true;
                                        }
                                    }
                                }
                                else
                                {
                                    this.tempRowId = e.RowIndex;
                                    //// this.attachmentGridView.CurrentCell.Selected = true;
                                    this.SetDataBindingValue(this.tempRowId);
                                    this.AttachmentGridView.Rows[e.RowIndex].Selected = true;
                                }
                            }

                            if (e.RowIndex >= 0)
                            {
                                this.auditFileID = this.fileTextBox.Text;
                                if (this.auditFileID.Length > 0)
                                {
                                    //// Gets the value for auditLinkLabel.
                                    string tempfiletextboxvalue = string.Empty;
                                    string tempfiletextboxvalue2 = string.Empty;
                                    string tempfiletextboxvalue3 = string.Empty;
                                    tempfiletextboxvalue = this.fileTextBox.Text.Trim().Remove(this.fileTextBox.Text.Trim().LastIndexOf("."));
                                    tempfiletextboxvalue2 = tempfiletextboxvalue.Substring(tempfiletextboxvalue.ToString().Trim().LastIndexOf("\\"));
                                    int filelength = tempfiletextboxvalue2.Trim().Length;
                                    tempfiletextboxvalue3 = tempfiletextboxvalue2.Substring(1, filelength - 1);
                                    //// this.auditLinkLabel.Text = "tTS_File[FileID] " + this.auditFileID.Remove(this.auditFileID.LastIndexOf("."));
                                    this.auditLinkLabel.Text = "tTS_File[FileID] " + tempfiletextboxvalue3;
                                }
                                else
                                {
                                    this.auditLinkLabel.Text = "tTS_File[FileID] ";
                                }

                                //// if (this.SetBrowseButtonColor(Convert.ToInt32(this.attachmentGridView.Rows[e.RowIndex].Cells["FileID"].Value.ToString())))
                                ////if (this.SetBrowseButtonColor(Convert.ToInt32(this.auditFileID.Remove(this.auditFileID.LastIndexOf(".")))))
                                if (this.browseSelected)
                                {
                                    if (!this.scanFile)
                                    {
                                        this.browseButton.ImageSelected = true;
                                        this.scanButton.ImageSelected = false;
                                        this.scanButton.Enabled = false;
                                        this.openButton.Enabled = false;
                                    }
                                    else
                                    {
                                        ////this.browseButton.ImageSelected = false;
                                        ////this.scanButton.ImageSelected = false;
                                        ////this.scanFile = false;

                                        this.browseButton.ImageSelected = false;
                                        this.scanButton.ImageSelected = true;
                                        this.browseButton.Enabled = false;
                                        this.openButton.Enabled = false;
                                    }
                                }
                            }

                            if (!this.valueChanged)
                            {
                                this.InitializeButton();
                            }
                        }
                        else
                        {
                            this.DisableAttachmentControl();
                            this.ClearHeader();
                            this.SetCurrentFormButtons(ButtonOperation.EmptyGrid);

                            /* if (this.tempRowId > -1)
                            {
                                this.SetDataGridViewPosition(this.tempRowId);
                                this.SetDataBindingValue(this.tempRowId);
                            }
                            else
                            {
                                this.SetDataGridViewPosition(0);
                                this.SetDataBindingValue(0);
                            }

                            this.EnableAttachmentControl();
                            this.SetFocus(); */
                        }

                        this.CheckEditPermission();
                    }

                    //// this.attachmentGridView.CurrentCell.Selected = true;
                }
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the AttachmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AttachmentGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*   try
              {
                  if (e.RowIndex >= -1)
                  {
                      if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                      {
                          this.SetEnableControls(e.ColumnIndex);

                          // Used to mark the check box as selected.
                          if (string.Compare(this.attachmentGridView.Columns[e.ColumnIndex].HeaderText, "Primary") == 0)
                          {
                              DataGridViewRowCollection rowCollection = this.attachmentGridView.Rows;
                              foreach (DataGridViewRow rows in rowCollection)
                              {
                                  if (e.RowIndex == rows.Index)
                                  {
                                      this.attachmentGridView.Rows[rows.Index].Cells[e.ColumnIndex].Value = true;
                                  }
                                  else
                                  {
                                      this.attachmentGridView.Rows[rows.Index].Cells[e.ColumnIndex].Value = false;
                                  }
                              }
                          }

                          // Used to mark the check box as selected.
                          if (string.Compare(this.attachmentGridView.Columns[e.ColumnIndex].HeaderText, "Public") == 0)
                          {
                              if (Convert.ToBoolean(this.attachmentGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                              {
                                  this.attachmentGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                              }
                              else
                              {
                                  this.attachmentGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                              }
                          }
                      }

                      if (e.RowIndex >= 0)
                      {
                          this.selectedFilePath = this.GetSelectedFilePath(e.RowIndex);
                      }

                      if (this.currencyManager != null && this.currencyManager.Position >= 0)
                      {
                          this.currencyManager.Position = e.RowIndex;
                          this.currentRow = e.RowIndex;
                          this.currencyManager.EndCurrentEdit();
                          this.currentRow = this.currencyManager.Position;
                      }

                      this.SetDataGridViewPosition(this.currentRow);
                      SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                      this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                  }
              }
              catch (SoapException ex)
              {
                  ////TODO : Need to find specific exception and handle it.
                  ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
              }
              catch (Exception ex)
              {
                  ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
              } */
        }

        /// <summary>
        /// Handles the Click event of the SaveAttachmentButton control and save the image to central location and data to database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveAttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.saveAttachmentButton.Enabled)
                {
                    this.saveAttachmentButton.Focus();
                    this.SaveRecords();
                    this.browseTmpPath = string.Empty;
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
        /// Handles the Click event of the ScanButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScanButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CheckOfficeScanner())
                {

                    this.SourceTextBox.Text = string.Empty;

                    this.linktypeid = 3;
                    this.linktypeTextBox.Text = this.linktypeid.ToString();
                    if (this.scanFile != true)
                    {
                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            this.tmpPath = this.GetSelectedFilePath();
                            if (this.AttachmentGridView.Rows.Count > 0)
                            {
                                this.tmpFileID = Convert.ToInt32(this.AttachmentGridView.Rows[currentRow].Cells["AttachmentFileID"].Value.ToString());
                            }

                            if (this.tmpPath.Length > 0)
                            {
                                if (MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("AttachmentFileMissing"), "\n", SharedFunctions.GetResourceString("AttachmentFileMissing1") }), SharedFunctions.GetResourceString("AttachmentFileMissing2"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    this.AttachmentGridView.Enabled = false;
                                    this.ScanFilePath();

                                    if ((!string.IsNullOrEmpty(this.fileTextBox.Text)) && this.scanFile)
                                    {
                                        string filext;
                                        filext = Path.GetExtension(this.fileTextBox.Text);
                                        if (filext != ".tiff")
                                        {
                                            string tempExt = string.Empty;
                                            tempExt = this.fileTextBox.Text.Substring(this.fileTextBox.Text.LastIndexOf(".")).ToString();
                                            this.fileTextBox.Text = this.fileTextBox.Text.Replace(tempExt, ".tiff");
                                        }
                                    }
                                    //// this.valueChanged = true;
                                    this.linktypeid = 3;
                                    if (this.scanFile == true)
                                    {
                                        this.linktypeTextBox.Text = this.linktypeid.ToString();
                                    }
                                    else
                                    {
                                        this.linktypeTextBox.Text = this.AttachmentGridView.Rows[currentRow].Cells["LinkType"].Value.ToString();
                                        this.SourceTextBox.Text = this.AttachmentGridView.Rows[currentRow].Cells["Source"].Value.ToString();
                                    }
                                }
                                else
                                {
                                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.CancelMode);
                                    this.SetCurrentFormButtons(ButtonOperation.Cancel);
                                    this.AttachmentGridView.Focus();
                                }
                            }
                        }
                        else
                        {
                            ////DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI");

                            //// if (dirInfo.Exists)
                            //// {
                            ////     // Calls the method to Scan.
                            ////     string[] deleteImageFiles = null;
                            ////     string dire = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI";
                            ////     deleteImageFiles = Directory.GetFiles(dire);
                            ////     foreach (string dfilePath in deleteImageFiles)
                            ////     {
                            ////         if (System.IO.File.Exists(dfilePath))
                            ////         {
                            ////             try
                            ////             {
                            ////                 System.IO.File.Delete(dfilePath);
                            ////             }
                            ////             catch (Exception ex)
                            ////             {
                            ////                 ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                            ////             }
                            ////         }
                            ////     }
                            //// }

                            this.ScanFilePath();
                            this.linktypeid = 3;
                            ////this.localFilePath = this.scanTmpPath;
                            ////this.SourceTextBox.Text = this.scanTmpPath;
                            if (!string.IsNullOrEmpty(this.SourceTextBox.Text) && this.scanFile == true)
                            {
                                this.linktypeTextBox.Text = this.linktypeid.ToString();
                            }
                        }
                    }
                    else if (MessageBox.Show(SharedFunctions.GetResourceString("ScanFileLost"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //// Calls the method to Scan.
                        this.ScanFilePath();
                        this.linktypeid = 3;
                        if (!string.IsNullOrEmpty(this.SourceTextBox.Text) && this.scanFile == true)
                        {
                            this.linktypeTextBox.Text = this.linktypeid.ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MSOfficeNotFound"), SharedFunctions.GetResourceString("ScanInterface"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ////this.browseButton.BackColor = Color.FromArgb(71, 133, 85);
                ////this.browseButton.StatusOffColor = Color.FromArgb(71, 133, 85);
                ////this.URLbutton.BackColor = Color.FromArgb(71, 133, 85);
                ////this.URLbutton.StatusOffColor = Color.FromArgb(71, 133, 85);
                ////this.scanButton.BackColor = Color.FromArgb(71, 133, 85);
                ////this.scanButton.StatusOffColor = Color.FromArgb(71, 133, 85); 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the willRollCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WillRollCheckBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.valueChanged = true;
                //this.tempRoll = this.WillRollCheckBox.Checked;

                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                }

                if (this.currencyManager != null && this.currencyManager.Position >= 0)
                {
                    this.currencyManager.EndCurrentEdit();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the BrowseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.scanTmpPath = string.Empty;
                this.linktypeid = 0;
                this.Cursor = Cursors.WaitCursor;

                this.SelectFile();
                this.linktypeid = 1;
                if (!string.IsNullOrEmpty(this.SourceTextBox.Text))
                {
                    this.linktypeTextBox.Text = this.linktypeid.ToString();
                }
                ////this.browseButton.BackColor = Color.FromArgb(71, 133, 85);
                ////this.browseButton.StatusOffColor = Color.FromArgb(71, 133, 85);
                ////this.URLbutton.BackColor = Color.FromArgb(71, 133, 85);
                ////this.URLbutton.StatusOffColor = Color.FromArgb(71, 133, 85);
                ////this.scanButton.BackColor = Color.FromArgb(71, 133, 85);
                ////this.scanButton.StatusOffColor = Color.FromArgb(71, 133, 85); 
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteAttachmentButton control which delete a record in datagrid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteAttachmentButton_Click(object sender, EventArgs e)
        {
            // Delete the row which is selected in the Datagridview.
            filePathDelete = string.Empty;
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.CheckAttachmentGrid())
                    {
                        try
                        {
                            this.griddelete();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            // ErrorForm showError = new ErrorForm(SharedFunctions.GetResourceString("TS_File"), SharedFunctions.GetResourceString("NoAccessFileLocation"), filePathDelete, " ", SharedFunctions.GetResourceString("ContactAdmin"));
                            // showError.ShowDialog();

                            MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", filePathDelete, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.browseButton.ImageSelected = false;
                            this.scanButton.ImageSelected = false;
                            this.scanFile = false;
                            SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.DeleteMode);
                            this.SetCurrentFormButtons(ButtonOperation.Cancel);
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }

                        this.AttachmentGridView.Focus();
                    }
                }
                else
                {
                    this.SetFocus();
                }
            }
            catch (SoapException ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }


        private void griddelete()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////this.editOperation = true;
                int row = this.GetRowIndex();
                int currentFileID = 0;
                if (this.AttachmentGridView.CurrentRow != null)
                {
                    currentFileID = Convert.ToInt32(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells[this.AttachmentFileID.Name].Value.ToString());

                    ////int.TryParse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells[this.AttachmentFileID.Name].Value, out currentFileID);
                }

                ////Commented the code to implement #4954
                ////filePathDelete = this.form9005Control.WorkItem.GetOriginalFilePath(currentFileID, TerraScanCommon.UserId);

                ////if (System.IO.File.Exists(filePathDelete))
                ////{
                ////    // Delete the image in the central location.
                ////    System.IO.File.Delete(filePathDelete);
                ////}
                ////till here

                // Delete the row in the database.
                this.form9005Control.WorkItem.DeleteAttachments(Convert.ToInt32(this.AttachmentGridView.Rows[row].Cells["AttachmentFileID"].Value), TerraScanCommon.UserId);

                this.LoadAttachmentGrid();
                this.InitializeButton();

                if (row == this.recordCount)
                {
                    row--;
                }

                ////this.SetDataGridViewPosition(0);

                if (this.emptyRecord)
                {
                    // this.fileTextBox.Text = string.Empty;
                    // this.descriptionTextBox.Text = string.Empty;
                    this.ClearHeader();
                    this.DisableAttachmentControl();
                    this.auditLinkLabel.Text = "tTS_File[FileID]";
                    this.browseButton.ImageSelected = false;
                    this.scanButton.ImageSelected = false;
                    this.scanFile = false;
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.DeleteMode);
                    this.SetCurrentFormButtons(ButtonOperation.Delete);
                    this.deleteAttachmentButton.Enabled = false;
                    this.newAttachmentButton.Focus();

                    if (this.AttachmentGridView.Rows.Count > 0)
                    {
                        this.AttachmentGridView.Rows[0].Selected = false;
                        this.AttachmentGridView.Enabled = false;
                    }
                }
                else
                {
                    this.CheckEditPermission();
                }
            }
            catch (UnauthorizedAccessException)
            {
                // ErrorForm showError = new ErrorForm(SharedFunctions.GetResourceString("TS_File"), SharedFunctions.GetResourceString("NoAccessFileLocation"), filePathDelete, " ", SharedFunctions.GetResourceString("ContactAdmin"));
                // showError.ShowDialog();

                MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", filePathDelete, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.browseButton.ImageSelected = false;
                this.scanButton.ImageSelected = false;
                this.scanFile = false;
                SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.DeleteMode);
                this.SetCurrentFormButtons(ButtonOperation.Cancel);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            this.AttachmentGridView.Focus();
        }

        /// <summary>
        /// Handles the Click event of the OpenButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OpenButton_Click(object sender, EventArgs e)
        {
            try
            {
                //// SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                //// this.SetCurrentFormButtons(ButtonOperation.Save);
                //// int rowIndex = 0;
                //// rowIndex = this.GetRowIndex();               

                //// if (this.attachmentDataSet.Tables[0].Rows.Count > 0)
                //// {
                ////     this.selectedFilePath = this.attachmentGridView.Rows[rowIndex].Cells["Source"].Value.ToString();
                //// }

                this.Cursor = Cursors.WaitCursor;
                this.InitializeButton();
                ////DataSet tmpDataSet = new DataSet();
                //// this.GetSelectedFilePath(this.currentRow);

                int rowIndex = 0;
                rowIndex = this.GetRowIndex();

                //// this.selectedFilePath = this.attachmentDataSet.GetAttachmentItems.Rows[rowIndex][this.attachmentDataSet.GetAttachmentItems.SourceColumn].ToString();

                int fileOriginalID = 0;
                ////int.TryParse(this.attachmentDataSet.GetAttachmentItems.Rows[rowIndex][this.attachmentDataSet.GetAttachmentItems.FileIDColumn.ColumnName].ToString(), out fileOriginalID);
                int.TryParse(this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AttachmentFileID"].Value.ToString(), out fileOriginalID);

                //Offline Mode Open the Local Path instead of General Path
                if (WSHelper.IsOnLineMode.Equals(false) && TerraScanCommon.IsFieldUser)
                {
                    this.selectedFilePath = this.AttachmentGridView.Rows[this.AttachmentGridView.CurrentRowIndex].Cells["AURL"].Value.ToString();
                }
                else
                {
                    this.selectedFilePath = this.form9005Control.WorkItem.GetOriginalFilePath(fileOriginalID, TerraScanCommon.UserId);
                }

                ////this.selectedFilePath = Path.Combine(Environment.CurrentDirectory, @"D:\Kuppu\tele.Doc");
                if (this.selectedFilePath.Length > 0)
                {
                    // this.attachmentDataSet = F9005WorkItem.GetProgramId(Convert.ToInt16(this.fileTypeIDTextBox.Text));

                    this.attachmentDataSet.GetProgramId.Clear();
                    this.attachmentDataSet.GetProgramId.Merge(this.form9005Control.WorkItem.GetProgramId(Convert.ToInt16(this.fileTypeIDTextBox.Text)));

                    if (VaildDataSet(this.attachmentDataSet))
                    {
                        // Opens the image according to the Program type which is send from database.
                        OpenFile(Convert.ToInt32(this.attachmentDataSet.GetProgramId.Rows[0][1].ToString()), this.selectedFilePath, this.attachmentDataSet.GetProgramId.Rows[0][2].ToString());
                    }
                }

                ////this.openButton.Focus();
            }
            catch (SoapException ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the AttachmentDatePictureBox control and shows the calander.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentDatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowAttachmentCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Leave event of the AttachmentMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentMonthCalander_Leave(object sender, EventArgs e)
        {
            this.attachmentMonthCalander.Visible = false;
        }

        /// <summary>
        /// Handles the KeyPress event of the AttachmentDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void AttachmentDateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.valueChanged = true;
                this.tempDescription = this.descriptionTextBox.Text;
                if (this.FormPermissionFields.editPermission)
                {
                    switch (e.KeyChar)
                    {
                        case (char)13:
                            {
                                // Calls the method to show the calender control.
                                this.ShowAttachmentCalender();
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }


            //try
            //{
            //    if (this.FormPermissionFields.editPermission)
            //    {

            //        switch (e.KeyChar)
            //        {
            //            case (char)3:
            //                {
            //                    e.Handled = true;
            //                    break;
            //                }
            //            case (char)13:
            //                {
            //                    // Calls the method to show the calender control.
            //                    this.ShowAttachmentCalender();
            //                    break;
            //                }

            //            default:
            //                {
            //                    this.valueChanged = true;
            //                    this.tempDescription = this.descriptionTextBox.Text;

            //                    if (this.buttonOperation != (int)ButtonOperation.New)
            //                    {
            //                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
            //                        this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
            //                    }

            //                    break;
            //                }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        /// <summary>
        /// Handles the DateSelected event of the AttachmentMonthCalander control and assign the value to date textbox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void AttachmentMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                // Assign the selected date to the DateTextbox.
                this.attachmentDateTextBox.Text = e.Start.ToShortDateString();
                this.tempDate = e.Start.ToShortDateString();

                this.valueChanged = true;
                this.dateChanged = true;

                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                }

                /* if (this.buttonOperation != (int)ButtonOperation.New || this.buttonOperation == (int)ButtonOperation.Empty)
                {
                    if (this.currencyManager != null && this.currencyManager.Position >= 0 && this.buttonOperation != (int)ButtonOperation.New)
                    {
                        this.attachmentGridView.Rows[currencyManager.Position].Cells["AttachmentDate"].Value = this.attachmentDateTextBox.Text;
                    }

                    if (this.currencyManager != null && this.currencyManager.Position >= 0)
                    {
                        this.currencyManager.EndCurrentEdit();
                    }
                } */

                this.attachmentMonthCalander.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the TypeComboBox control and assign the selected value to grid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                /* // Update the value in the datagridview according to the selected value in the combo box.
                if (this.currencyManager != null && this.currencyManager.Position >= 0 && this.buttonOperation != (int)ButtonOperation.New)
                {
                    System.Data.DataRowView typeDRV = (System.Data.DataRowView)this.typeComboBox.Items[this.typeComboBox.SelectedIndex];
                    this.attachmentGridView.Rows[currencyManager.Position].Cells["Type"].Value = typeDRV["FunctionName"].ToString();
                    this.attachmentGridView.Rows[currencyManager.Position].Cells["FileTypeID"].Value = this.typeComboBox.SelectedValue;
                } */

                if (this.currencyManager != null && this.currencyManager.Position >= 0)
                {
                    this.currencyManager.EndCurrentEdit();
                }

                this.valueChanged = true;

                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                }

                if (this.typeComboBox.SelectedIndex >= 0)
                {
                    this.fileTypeIDTextBox.Text = this.typeComboBox.SelectedValue.ToString();
                    this.tempFileType = this.fileTypeIDTextBox.Text;
                }
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseAttachmentButton control and closes the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseAttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to close the attachments form.
                // this.AttachmentClose();

                // Checks whether there is any changes in local dataset.
                if (this.valueChanged || this.buttonOperation == (int)ButtonOperation.New)
                {
                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            {
                                try
                                {
                                    this.SaveRecords();
                                }
                                catch (SoapException ex)
                                {
                                    ////TODO : Need to find specific exception and handle it.
                                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                                }

                                if (this.closingNow)
                                {
                                    this.closeButton = true;
                                    this.Close();
                                }
                                else
                                {
                                    this.DialogResult = DialogResult.None;
                                }

                                this.closingNow = true;
                                break;
                            }

                        case DialogResult.No:
                            {
                                this.closeButton = true;
                                this.Close();
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                this.DialogResult = DialogResult.None;
                                this.SetFocus();
                                break;
                            }
                    }

                    /* if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), SharedFunctions.GetAppConfigString("ApplicationName"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.closeButton = true;
                        this.Close();
                    }
                    else
                    {
                        this.SetFocus();
                    } */
                }
                else
                {
                    this.closeButton = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the AttachmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AttachmentGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /* if (this.valueChanged)
            {
                if (this.saveAttachmentButton.Enabled == true)
                {
                    this.browseButton.Focus();
                }
                else
                {
                    this.descriptionTextBox.Focus();
                }
            }
            else
            { */

            try
            {
                if (!this.valueChanged)
                {
                    if (e.RowIndex >= -1 && e.ColumnIndex >= 0)
                    {
                        if (!string.IsNullOrEmpty(this.AttachmentGridView.Rows[e.RowIndex].Cells["AttachmentFileID"].Value.ToString()))
                        {
                            this.selectedRow = e.RowIndex;
                            this.tempRowId = e.RowIndex;
                            this.SetDataBindingValue(e.RowIndex);

                            if (!this.valueChanged)
                            {
                                this.InitializeButton();
                            }
                        }
                        else
                        {
                            this.DisableAttachmentControl();
                            this.ClearHeader();
                            this.SetCurrentFormButtons(ButtonOperation.EmptyGrid);
                        }
                    }

                    if (e.RowIndex >= 0)
                    {
                        this.auditFileID = this.fileTextBox.Text;
                        if (this.auditFileID.Length > 0)
                        {
                            // Gets the value for auditLinkLabel.
                            string tempfiletextboxvalue = string.Empty;
                            string tempfiletextboxvalue2 = string.Empty;
                            string tempfiletextboxvalue3 = string.Empty;

                            tempfiletextboxvalue = this.fileTextBox.Text.Remove(this.fileTextBox.Text.Trim().LastIndexOf("."));
                            tempfiletextboxvalue2 = tempfiletextboxvalue.Substring(tempfiletextboxvalue.ToString().Trim().LastIndexOf("\\"));
                            int filelength = tempfiletextboxvalue2.Trim().Length;
                            tempfiletextboxvalue3 = tempfiletextboxvalue2.Substring(1, filelength - 1);

                            ////this.auditLinkLabel.Text = "tTS_File[FileID] " + this.auditFileID.Remove(this.auditFileID.LastIndexOf("."));
                            this.auditLinkLabel.Text = "tTS_File[FileID] " + tempfiletextboxvalue3;
                        }
                        else
                        {
                            this.auditLinkLabel.Text = "tTS_File[FileID] ";
                        }
                    }
                }
                this.textchange = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the AttachmentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void AttachmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (this.closeButton != true)
                        {
                            if (this.valueChanged || this.buttonOperation == (int)ButtonOperation.New)
                            {
                                switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    case DialogResult.Yes:
                                        {
                                            try
                                            {
                                                this.SaveRecords();
                                            }
                                            catch (Exception)
                                            {
                                                // MessageBox.Show(ex.Message);
                                            }

                                            if (this.closingNow)
                                            {
                                                this.closeButton = true;
                                                this.Close();
                                            }
                                            else
                                            {
                                                this.DialogResult = DialogResult.None;
                                            }

                                            this.closingNow = true;
                                            break;
                                        }

                                    case DialogResult.No:
                                        {
                                            this.closeButton = true;
                                            this.Close();
                                            break;
                                        }

                                    case DialogResult.Cancel:
                                        {
                                            this.DialogResult = DialogResult.None;
                                            this.SetFocus();
                                            break;
                                        }
                                }

                                /* if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), SharedFunctions.GetAppConfigString("ApplicationName"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    e.Cancel = false;
                                }
                                else
                                {
                                    e.Cancel = true;
                                } */
                            }
                            else
                            {
                                e.Cancel = false;
                            }
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
        /// Handles the FormClosing event of the AttachmentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9005_FormClosing(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
                {
                    if (e.CloseReason.Equals(CloseReason.UserClosing))
                    {
                        if (this.closeButton != true)
                        {
                            if (this.valueChanged || this.buttonOperation == (int)ButtonOperation.New)
                            {
                                switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    case DialogResult.Yes:
                                        {
                                            try
                                            {
                                                this.SaveRecords();
                                            }
                                            catch (SoapException ex)
                                            {
                                                ////TODO : Need to find specific exception and handle it.
                                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                                            }
                                            catch (Exception ex)
                                            {
                                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                                            }

                                            if (this.closingNow)
                                            {
                                                this.closeButton = true;
                                                this.Close();
                                            }
                                            else
                                            {
                                                this.DialogResult = DialogResult.None;
                                            }

                                            this.closingNow = true;
                                            break;
                                        }

                                    case DialogResult.No:
                                        {
                                            this.closeButton = true;
                                            this.Close();
                                            break;
                                        }

                                    case DialogResult.Cancel:
                                        {
                                            this.DialogResult = DialogResult.None;
                                            this.SetFocus();
                                            break;
                                        }
                                }

                                //// if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), SharedFunctions.GetAppConfigString("ApplicationName"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                                ////{
                                ////    e.Cancel = false;
                                ////}
                                ////else
                                ////{
                                ////    e.Cancel = true;
                                ////} 
                            }
                            else
                            {
                                ////todo :  e.Cancel = false;
                            }
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
        /// Handles the Enter event of the AttachmentDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentDateTextBox_Enter(object sender, EventArgs e)
        {
            this.textBoxFocused = this.attachmentDateTextBox.Name;
        }

        /// <summary>
        /// Handles the Enter event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_Enter(object sender, EventArgs e)
        {
            this.textBoxFocused = this.descriptionTextBox.Name;
        }

        private void descriptionTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }


        /// <summary>
        /// Handles the KeyUp event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.editPermission)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Tab:
                            {
                                // SendKeys.Send ("{TAB}");
                                break;
                            }

                        case Keys.Delete:
                            {
                                //// SendKeys.Send ("{TAB}");
                                if (this.textchange)
                                {
                                    this.valueChanged = true;
                                    this.tempDescription = this.descriptionTextBox.Text;
                                    if (this.buttonOperation != (int)ButtonOperation.New)
                                    {
                                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                                        this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                                    }
                                    this.textchange = false;
                                }

                                break;
                            }
                    }

                    if (this.textchange)
                    {
                        this.valueChanged = true;
                        this.tempDescription = this.descriptionTextBox.Text;
                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                            this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                        }
                        this.textchange = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the AttachmentDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AttachmentDateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.FormPermissionFields.editPermission)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Tab:
                            {
                                break;
                            }

                        case Keys.Delete:
                            {
                                if (this.textchange)
                                {
                                    this.valueChanged = true;
                                    this.dateChanged = true;
                                    this.tempDate = this.attachmentDateTextBox.Text;

                                    if (this.buttonOperation != (int)ButtonOperation.New)
                                    {
                                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                                        this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                                    }
                                    this.textchange = false;
                                }

                                break;
                            }
                    }

                    if (this.textchange)
                    {
                        this.valueChanged = true;
                        this.dateChanged = true;
                        this.tempDate = this.attachmentDateTextBox.Text;

                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                            this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                        }
                        this.textchange = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the AttachmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AttachmentGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.saveAttachmentButton.Enabled == false)
                {
                    if (this.tempRowId >= 0)
                    {
                        this.AttachmentGridView.Rows[this.tempRowId].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the 1 event of the TypeComboBox_SelectionChangeCommitted control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeComboBox_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            try
            {
                /* // Update the value in the datagridview according to the selected value in the combo box.
                if (this.currencyManager != null && this.currencyManager.Position >= 0 && this.buttonOperation != (int)ButtonOperation.New)
                {
                    System.Data.DataRowView typeDRV = (System.Data.DataRowView)this.typeComboBox.Items[this.typeComboBox.SelectedIndex];
                    this.attachmentGridView.Rows[currencyManager.Position].Cells["Type"].Value = typeDRV["FunctionName"].ToString();
                    this.attachmentGridView.Rows[currencyManager.Position].Cells["FileTypeID"].Value = this.typeComboBox.SelectedValue;
                } */

                if (this.currencyManager != null && this.currencyManager.Position >= 0)
                {
                    this.currencyManager.EndCurrentEdit();
                }

                this.valueChanged = true;

                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                }

                if (this.typeComboBox.SelectedIndex >= 0)
                {
                    this.fileTypeIDTextBox.Text = this.typeComboBox.SelectedValue.ToString();
                    this.tempFileType = this.fileTypeIDTextBox.Text;
                }
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the publicCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PublicCheckBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.valueChanged = true;
                this.tempPublic = this.publicCheckBox.Checked;

                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                }

                if (this.currencyManager != null && this.currencyManager.Position >= 0)
                {
                    this.currencyManager.EndCurrentEdit();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the primaryCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrimaryCheckBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.valueChanged = true;
                this.tempPrimary = this.primaryCheckBox.Checked;

                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                }

                if (this.currencyManager != null && this.currencyManager.Position >= 0)
                {
                    this.currencyManager.EndCurrentEdit();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the TypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeComboBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.valueChanged = true;

                ////if (this.buttonOperation != (int)ButtonOperation.New)
                ////{
                ////    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                ////    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                ////}

                if (this.typeComboBox.SelectedIndex >= 0)
                {
                    this.fileTypeIDTextBox.Text = this.typeComboBox.SelectedValue.ToString();
                    this.tempFileType = this.fileTypeIDTextBox.Text;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AuditLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AuditLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string reportAuditId = string.Empty;
                int auditLinkKeyId = 0;
                reportAuditId = this.auditLinkLabel.Text.Substring(17);
                int.TryParse(reportAuditId, out auditLinkKeyId);

                if (auditLinkKeyId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = auditLinkKeyId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                #region Commented Part
                ////string reportAuditId = string.Empty;
                ////this.Cursor = Cursors.WaitCursor;
                ////if (this.auditLinkLabel.Text.Length > 17)
                ////{
                ////    reportAuditId = this.auditLinkLabel.Text.Substring(17);

                ////    this.reportFileIdHashTable.Clear();
                ////    this.reportFileIdHashTable.Add("KeyName", "ReportFileID");
                ////    this.reportFileIdHashTable.Add("KeyValue", reportAuditId);

                ////    // Shows the report form.
                ////    // changed the parameter type from string to int
                ////    TerraScanCommon.ShowReport(90131, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
                ////}
                ////else
                ////{
                ////    MessageBox.Show(SharedFunctions.GetResourceString("+"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}

                #endregion Commented Part
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the AttachmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AttachmentGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.valueChanged)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                this.GridCancel(e);
                                break;
                            }

                        case Keys.Up:
                            {
                                this.GridCancel(e);
                                break;
                            }
                    }
                }

                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        {
                            // Delete the row which is selected in the Datagridview.
                            filePathDelete = string.Empty;
                            try
                            {
                                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    if (this.CheckAttachmentGrid())
                                    {
                                        try
                                        {
                                            this.griddelete();
                                        }
                                        catch (UnauthorizedAccessException)
                                        {
                                            // ErrorForm showError = new ErrorForm(SharedFunctions.GetResourceString("TS_File"), SharedFunctions.GetResourceString("NoAccessFileLocation"), filePathDelete, " ", SharedFunctions.GetResourceString("ContactAdmin"));
                                            // showError.ShowDialog();

                                            MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", filePathDelete, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            this.browseButton.ImageSelected = false;
                                            this.scanButton.ImageSelected = false;
                                            this.scanFile = false;
                                            SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.DeleteMode);
                                            this.SetCurrentFormButtons(ButtonOperation.Cancel);
                                        }
                                        finally
                                        {
                                            this.Cursor = Cursors.Default;
                                        }

                                        this.AttachmentGridView.Focus();
                                    }
                                }
                                else
                                {
                                    this.SetFocus();
                                }
                            }
                            catch (SoapException ex)
                            {
                                //////TODO : Need to find specific exception and handle it.
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                            }
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Grids the cancel.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GridCancel(KeyEventArgs e)
        {
            try
            {
                switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.Text, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            try
                            {
                                this.SaveRecords();
                            }
                            catch (SoapException ex)
                            {
                                ////TODO : Need to find specific exception and handle it.
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                            }

                            if (this.closingNow)
                            {
                                this.SetDataBindingValue(this.tempRowId);
                                e.Handled = false;
                                this.valueChanged = false;
                            }
                            else
                            {
                                e.Handled = true;
                            }

                            break;
                        }

                    case DialogResult.No:
                        {
                            this.SetDataBindingValue(this.tempRowId);
                            SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.CancelMode);
                            this.SetCurrentFormButtons(ButtonOperation.Cancel);
                            this.AttachmentGridView.Focus();
                            e.Handled = false;
                            this.valueChanged = false;
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            this.SetFocus();
                            e.Handled = true;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the Attachment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Attachment_Load(object sender, EventArgs e)
        {
            try
            {
                this.SourceTextBox.TextAlign = HorizontalAlignment.Left;
                this.NewMenu.Click += new EventHandler(this.NewAttachmentButton_Click);
                this.SaveMenu.Click += new EventHandler(this.SaveAttachmentButton_Click);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Load event of the Attachment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9005_Load(object sender, EventArgs e)
        {
            try
            {
                this.SourceTextBox.TextAlign = HorizontalAlignment.Left;
                this.NewMenu.Click += new EventHandler(this.NewAttachmentButton_Click);
                this.SaveMenu.Click += new EventHandler(this.SaveAttachmentButton_Click);
                ////this.browseButton.BackColor = Color.FromArgb(71, 133, 85); 
                ////this.URLbutton.BackColor = Color.FromArgb(71, 133, 85);
                ////this.scanButton.BackColor = Color.FromArgb(71, 133, 85);

                this.keyField = "FileID";
                this.formNo = 9005;

                this.auditLinkLabel.Enabled = false;
                //// this.DeleteScanFile(); 
                ////Ends here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Deletes the scan file.
        /// </summary>
        private void DeleteScanFile()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");

                if (dirInfo.Exists)
                {
                    FileInfo[] fileList = dirInfo.GetFiles();

                    if (fileList.Length > 0)
                    {
                        foreach (FileInfo file in fileList)
                        {
                            if (file.Name != "Thumbs.db")
                            {
                                System.IO.File.Delete(file.FullName);
                            }
                        }
                    }
                    else
                    {
                        ////Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI");
                        ////centalFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "FinalImage.tif";
                    }
                }

                ////Final MODI

                DirectoryInfo dirInfor = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI");

                if (dirInfor.Exists)
                {
                    // Calls the method to Scan.
                    string[] deleteImageFiles = null;
                    string dire = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI";
                    deleteImageFiles = Directory.GetFiles(dire);
                    foreach (string dfilePath in deleteImageFiles)
                    {
                        if (System.IO.File.Exists(dfilePath))
                        {
                            try
                            {
                                System.IO.File.Delete(dfilePath);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }


        /// <summary>
        /// Handles the KeyPress event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////if (e.KeyChar == (char)13)
            ////{
            ////    e.Handled = true;
            ////}

            //try
            //{
            //    if (this.FormPermissionFields.editPermission)
            //    {
            //        switch (e.KeyChar)
            //        {
            //            case (char)13:
            //                {
            //                    // SendKeys.Send ("{TAB}");
            //                    e.Handled = true;
            //                    break;
            //                }

            //            case (char)10:
            //                {
            //                    e.Handled = true;
            //                    break;
            //                }

            //            case (char)9:
            //                {
            //                    e.Handled = true;
            //                    break;
            //                }
            //            case (char)3:
            //                {
            //                    e.Handled = true;
            //                    break;
            //                }

            //            default:
            //                 {
            //                     this.valueChanged = true;
            //                     this.tempDescription = this.descriptionTextBox.Text;

            //                     if (this.buttonOperation != (int)ButtonOperation.New)
            //                     {
            //                         SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
            //                         this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
            //                     }

            //                    break;
            //                }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        /// <summary>
        /// Handles the KeyDown event of the AttachmentMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AttachmentMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.attachmentDateTextBox.Text = this.attachmentMonthCalander.SelectionStart.ToShortDateString();
                    this.valueChanged = true;
                    this.attachmentMonthCalander.Visible = false;
                    if (this.buttonOperation != (int)ButtonOperation.New)
                    {
                        SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                        this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
                    }
                    this.attachmentDateTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Issue Fixed - BugID #1409 (Source File field should be right justified)
        //// Added by Latha

        /// <summary>
        /// Handles the KeyDown event of the sourceTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SourceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.newAttachmentButton.Enabled)
                {
                    // this.sourceTextBox.Text = this.sourceTextBox.Tag.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the sourceTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SourceTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.SourceTextBox.Tag != null)
                {
                    //// this.SetSourceFileName(this.sourceTextBox.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the sourceTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SourceTextBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.SourceTextBox.Tag != null)
                {
                    Graphics graphics = this.CreateGraphics();
                    SizeF sizeF = graphics.MeasureString(this.SourceTextBox.Tag.ToString(), this.Font);
                    float preferredwidth = sizeF.Width;
                    if (preferredwidth > this.SourceTextBox.Width)
                    {
                        this.toolTip1.RemoveAll();
                        this.toolTip1.SetToolTip(this.SourceTextBox, this.SourceTextBox.Tag.ToString());
                    }
                    else
                    {
                        this.toolTip1.RemoveAll();
                    }
                }
                else
                {
                    this.toolTip1.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Issue Fixed - BugID #1409 (Source File field should be right justified)

        /// <summary>
        /// Handles the Click event of the URLbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void URLbutton_Click(object sender, EventArgs e)
        {
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(9004);
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

            ////this.browseButton.BackColor = Color.FromArgb(71, 133, 85);
            ////this.browseButton.StatusOffColor = Color.FromArgb(71, 133, 85);
            ////this.URLbutton.BackColor = Color.FromArgb(71, 133, 85);
            ////this.URLbutton.StatusOffColor = Color.FromArgb(71, 133, 85);
            ////this.scanButton.BackColor = Color.FromArgb(71, 133, 85);
            ////this.scanButton.StatusOffColor = Color.FromArgb(71, 133, 85);
            /*To be included later
            linktypeid = 2;
            if (this.sourceTextBox.Text != "")
            {
                this.linktypeTextBox.Text = linktypeid.ToString();
            }*/
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the attachmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void AttachmentGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.rowIndex = -1;
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the attachmentGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AttachmentGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //// SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                //// this.SetCurrentFormButtons(ButtonOperation.Save);
                //// int rowIndex = 0;
                //// rowIndex = this.GetRowIndex();               

                //// if (this.attachmentDataSet.Tables[0].Rows.Count > 0)
                //// {
                ////     this.selectedFilePath = this.attachmentGridView.Rows[rowIndex].Cells["Source"].Value.ToString();
                //// }

                this.Cursor = Cursors.WaitCursor;
                this.InitializeButton();
                ////DataSet tmpDataSet = new DataSet();
                //// this.GetSelectedFilePath(this.currentRow);

                if (e.RowIndex != -1)
                {
                    //// this.rowIndex = this.GetRowIndex();

                    this.rowIndex = e.RowIndex;
                    //// this.selectedFilePath = this.attachmentDataSet.GetAttachmentItems.Rows[rowIndex][this.attachmentDataSet.GetAttachmentItems.SourceColumn].ToString();

                    int fileOriginalID = 0;
                    ////if (!string.IsNullOrEmpty(this.attachmentDataSet.GetAttachmentItems.Rows[this.rowIndex][this.attachmentDataSet.GetAttachmentItems.UserIDColumn.ColumnName].ToString()))
                    if (!string.IsNullOrEmpty(this.AttachmentGridView.Rows[this.rowIndex].Cells["AttachmentUser"].Value.ToString()))
                    {
                        int.TryParse(this.AttachmentGridView.Rows[this.rowIndex].Cells["AttachmentFileID"].Value.ToString(), out fileOriginalID);
                        this.selectedFilePath = this.form9005Control.WorkItem.GetOriginalFilePath(fileOriginalID, TerraScanCommon.UserId);

                        ////this.selectedFilePath = Path.Combine(Environment.CurrentDirectory, @"D:\Kuppu\tele.Doc");
                        if (this.selectedFilePath.Length > 0)
                        {
                            //// this.attachmentDataSet = F9005WorkItem.GetProgramId(Convert.ToInt16(this.fileTypeIDTextBox.Text));

                            this.attachmentDataSet.GetProgramId.Clear();
                            this.attachmentDataSet.GetProgramId.Merge(this.form9005Control.WorkItem.GetProgramId(Convert.ToInt16(this.fileTypeIDTextBox.Text)));

                            if (VaildDataSet(this.attachmentDataSet))
                            {
                                //// Opens the image according to the Program type which is send from database.
                                OpenFile(Convert.ToInt32(this.attachmentDataSet.GetProgramId.Rows[0][1].ToString()), this.selectedFilePath, this.attachmentDataSet.GetProgramId.Rows[0][2].ToString());
                            }
                        }
                    }
                }

                this.rowIndex = 0;
                ////this.openButton.Focus();
            }
            catch (SoapException ex)
            {
                //////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    ////Commented by Biju on 02/Dec/2009 to fix #5021
                    ////HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                    ////Added by Biju on 02/Dec/2009 to fix #5021
                    HelpEngine.Show(this.AccessibleName, "9005");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion

        /// <summary>
        /// Handles the TextChanged event of the descriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {

            this.textchange = true;
            if (!this.tempDescription.Equals(this.descriptionTextBox.Text))
            {
                SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
            }

            //if (this.buttonOperation != (int)ButtonOperation.New)
            //{
            //    SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
            //    this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
            //}
        }

        private void attachmentDateTextBox_TextChanged(object sender, EventArgs e)
        {
            this.textchange = true;
            if (!this.tempDate.Equals(this.attachmentDateTextBox.Text))
            {
                SetButtons(this, (int)TerraScan.Common.TerraScanCommon.ButtonActionMode.EditMode);
                this.SetCurrentFormButtons(ButtonOperation.AttachmentGrid);
            }
        }
    }
}
