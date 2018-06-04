// -------------------------------------------------------------------------------------------------
// <copyright file="TerraScanCommon.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// Saves the Filter Information
// </summary>
// -------------------------------------------------------------------------------------------------
namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.Utilities;
    using System.Configuration;
    using System.Windows.Forms;
    using System.Reflection;
    using System.Data;
    using System.Text.RegularExpressions;
    using TerraScan.Helper;
    using System.Web.Services.Protocols;
    using System.Collections;
    using TerraScan.Common.Reports;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;

    ///<summary>
    ///class TerraScancommon
    ///</summary>
    public static class TerraScanCommon
    {
        #region fields

        /// <summary>
        /// Used to store current CamaSketchForm Details
        /// </summary>
        public static string[] CAMASketchFormDetails = new string[] { "3200" };

        /// <summary>
        /// Form mdiparent
        /// </summary>
        public static Form mdiparent;

        /// <summary>
        /// Used to Store UserID
        /// </summary>
        ////public static Guid UserId = new Guid("6c1c0017-62f8-4e79-bb7f-4212c6aa486e");

        /// <summary>
        /// DataSet which holds the 
        /// </summary>
        public static DataSet terraScanFormItems = new DataSet();

        /// <summary>
        /// DataSet Which Holds the Permissions for User
        /// </summary>
        public static DataSet formPermissionsDataSet = new DataSet();

        private static DataSet terraScanCachedData = new DataSet();

        /// <summary>
        /// Declaring Userid as integer 
        /// </summary>
        private static int userId;

        /// <summary>
        /// validateUserId
        /// </summary>
        private static int validationUserId;

        /// <summary>
        /// Used to Store SupportForm Call UserId
        /// </summary>
        private static int supportFormUserId = -1;

        /// <summary>
        /// Used to Store UserName
        /// </summary>
        ////public static string UserName = "System";
        private static string userName = "System";

        /// <summary>
        /// Used to Store ApplicationID
        /// </summary>
        ////public static string UserName = "System";
        private static int applicationId = 1;

        /// <summary>
        ///  Used to Create Reports
        /// </summary>
        private static TerraScan.Common.Reports.Report report = new TerraScan.Common.Reports.Report();

        /// <summary>
        /// used to stroe report details
        /// </summary>
        private static DataSet reportDetailsDataSet = new DataSet();

        /// <summary>
        /// used to store a dll Details
        /// </summary>
        private static DataRow[] formRows;

        /// <summary>
        /// formName
        /// </summary>
        private static string formName = string.Empty;

        /// <summary>
        /// Used to Check Valid Email
        /// </summary>
        private static string validEmailFormat = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

        /// <summary>
        /// Used to Check DebugMode
        /// </summary>
        public static bool activateDebugMode = false;

        /// <summary>
        /// Used to Check debug Configuration mode
        /// </summary>
        public static bool debugConfiguration = false;

        /// <summary>
        /// Used to Check debug Configuration mode
        /// </summary>
        public static bool debugSliceConfiguration = false;

        /// <summary>
        /// Used to Check debug Configuration mode
        /// </summary>
        public static int barCodeSessionTimeOut;

        /// <summary>
        /// Used to store current User's Adminstrators status
        /// </summary>
        public static bool isadministrator;

        /// <summary>
        /// fieldServerName
        /// </summary>
        private static string fieldServerName;

        /// <summary>
        /// fieldDataBaseName
        /// </summary>
        private static string fieldDataBaseName;

        /// <summary>
        /// Used to store checkedOutStatus 
        /// </summary>
        private static bool checkedOutStatus;

        /// <summary>
        /// Used to store rollYear for Form 35076
        /// </summary>
        private static string getRollYear;

        /// <summary>
        /// instance variable to hold the interest date throught the application.
        /// </summary>
        private static DateTime interestDate;

        /// <summary>
        /// instance variable to hold the receipt date throught the application.
        /// </summary>
        private static DateTime receiptDate;

        /// <summary>
        /// instance variable to hold Activity Queue form
        /// </summary>
        public static Form activityForm = null;

        /// <summary>
        /// Flag for identify any file has been selected through F9005 Attachment form.
        /// </summary>
        private static bool isFilePathChanged = false;

        /// <summary>
        ///  Used to identify the Apex Available
        /// </summary>
        private static bool IsApexAvailable = false;

        /// <summary>
        ///  Used to set isFieldUser
        /// </summary>
        private static bool isFieldUser = false;

        /// <summary>
        /// Used to set isDataBaseAvailable
        /// </summary>
        private static bool isDataBaseAvailable = false; 

        #endregion fields

        #region enum

        /// <summary>
        /// Enumerator sorttype
        /// </summary>
        public enum FormSortType
        {
            /// <summary>
            /// FormSortType
            /// </summary>
            NormalMode = 0,

            /// <summary>
            /// Filtered Mode
            /// </summary>
            FilteredMode
        }

        /// <summary>
        /// Enumerator ButtonActionMode
        /// </summary>
        public enum ButtonActionMode
        {
            /// <summary>
            /// New Mode
            /// </summary>
            NewMode = 0,

            /// <summary>
            /// Edit Mode
            /// </summary>
            EditMode = 1,

            /// <summary>
            /// Save
            /// </summary>
            SaveMode = 2,

            /// <summary>
            /// Cancel Mode
            /// </summary>
            CancelMode = 3,

            /// <summary>
            /// Delete Mode
            /// </summary>
            DeleteMode = 4,

            /// <summary>
            /// Open Mode
            /// </summary>
            OpenMode = 5,

            /// <summary>
            /// Null Record Mode
            /// </summary>
            NullRecordMode = 6,

            /// <summary>
            /// Null Record Mode
            /// </summary>
            DisableAllContorlsMode = 7
        }

        /// <summary>
        /// Enumerator ButtonActionType
        /// </summary>
        public enum ButtonActionType
        {
            /// <summary>
            /// Other
            /// </summary>
            Other = 0,

            /// <summary>
            /// New
            /// </summary>
            New = 1,

            /// <summary>
            /// Save
            /// </summary>
            Save = 2,

            /// <summary>
            /// Edit
            /// </summary>
            Edit = 3,

            /// <summary>
            /// Delete
            /// </summary>
            Delete = 4,

            /// <summary>
            /// Cancel
            /// </summary>
            Cancel = 5,

            /// <summary>
            /// Open
            /// </summary>
            Open = 6
        }

        /// <summary>
        /// Enum for ButtonTag
        /// </summary>
        public enum ButtonTag
        {
            /// <summary>
            /// NEW
            /// </summary>
            NEW = 0,

            /// <summary>
            /// SAVE
            /// </summary>
            SAVE = 1,

            /// <summary>
            /// CANCEL
            /// </summary>
            CANCEL = 2,

            /// <summary>
            /// DELETE
            /// </summary>
            DELETE = 3
        }

        /// <summary>
        /// Enum for ButtonEnabled
        /// </summary>
        public enum BUTTONENABLED
        {
            /// <summary>
            /// Enabled = 0
            /// </summary>
            ENABLED = 0,

            /// <summary>
            /// Disabled = 1
            /// </summary>
            DISABLED = 1
        }

        /// <summary>
        /// Enumerator Navigation Type
        /// </summary>
        public enum NavigationType
        {
            /// <summary>
            /// None  = 0.
            /// </summary>
            None = 0,

            /// <summary>
            /// Front  = 1.
            /// </summary>
            Front = 1,

            /// <summary>
            /// SnapShot = 2.
            /// </summary>
            Back = 2
        }

        /// <summary>
        /// Enumerator PageMode
        /// </summary>
        public enum PageModeTypes
        {
            /// <summary>
            /// View  = 0.
            /// </summary>
            View = 0,

            /// <summary>
            /// New Mode = 1.
            /// </summary>
            New = 1,

            /// <summary>
            /// Edit Mode = 2.
            /// </summary>
            Edit = 2,

            /// <summary>
            /// Save Mode = 2.
            /// </summary>
            Save = 3
        }

        /// <summary>
        /// Enumerator FilterType
        /// </summary>
        public enum FilterType
        {
            /// <summary>
            /// None  = 0.
            /// </summary>
            None = 0,

            /// <summary>
            /// Query  = 0.
            /// </summary>
            Query,

            /// <summary>
            /// SnapShot = 1.
            /// </summary>
            SnapShot
        }

        /// <summary>
        /// Enumerator PageStauts
        /// </summary>
        public enum PageStatus
        {
            /// <summary>
            /// Normal Mode  = 0.
            /// </summary>
            NormalMode = 0,

            /// <summary>
            /// Query By Form = 1.
            /// </summary>
            QueryByForm,

            /// <summary>
            /// Filtered = 2.
            /// </summary>
            FilteredMode,

            /// <summary>
            /// Filtered Query By Form = 3.
            /// </summary>
            FilteredQueryByForm
        }

        /// <summary>
        /// Enumerator CheckDetail - Status Order
        /// </summary>
        public enum CheckStatusOrder
        {
            /// <summary>
            /// Cleared  = 1.
            /// </summary>
            Cleared = 1,

            /// <summary>
            /// Printed = 2.
            /// </summary>
            Printed,

            /// <summary>
            /// Mailed = 3.
            /// </summary>
            Mailed,

            /// <summary>
            /// Void = 4.
            /// </summary>
            Void
        }

        /// <summary>
        /// Import File Source Type
        /// </summary>
        public enum ImportFileSourceType
        {
            /// <summary>
            /// Fixed Width  = 0.
            /// </summary>
            FixedWidth = 0,

            /// <summary>
            /// CommaDelimited = 1.
            /// </summary>
            CommaDelimited = 1
        }

        /// <summary>
        /// Enumerator Status Action
        /// </summary>
        public enum StatusAction
        {
            /// <summary>
            /// Before Status = 0
            /// </summary>
            BeforeStatus = 0,

            /// <summary>
            /// Process Status = 1
            /// </summary>
            ProcessStatus = 1,

            /// <summary>
            /// After Status = 2
            /// </summary>
            AfterStatus = 2
        }

        /// <summary>
        /// Enumerator Status Action
        /// </summary>
        public enum GridSortOrder
        {
            /// <summary>
            /// ASC = 0
            /// </summary>
            Asc = 0,

            /// <summary>
            /// DESC = 1
            /// </summary>
            Desc = 1
        }

        /// <summary>
        /// ErrorEngineType
        /// </summary>
        public enum ErrorEngineType
        {
            /// <summary>
            /// One
            /// </summary>
            One = 1,

            /// <summary>
            /// Two
            /// </summary>
            Two = 2,

            /// <summary>
            /// Three
            /// </summary>
            Three = 3,

            /// <summary>
            /// Four
            /// </summary>
            Four = 4,

            /// <summary>
            /// Five
            /// </summary>
            Five = 5,

            /// <summary>
            /// Six
            /// </summary>
            Six = 6,

            /// <summary>
            /// Seven
            /// </summary>
            Seven = 7,

            /// <summary>
            /// Eight
            /// </summary>
            Eight = 8,

            /// <summary>
            /// Nine
            /// </summary>
            Nine = 9
        }

        #endregion enum

        #region property

        /// <summary>
        /// Property for User Id
        /// </summary>
        public static int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        ///<summary>
        ///
        /// </summary>
        public static bool IsApexAvail
        {
            get { return IsApexAvailable; }
            set { IsApexAvailable = value; }
        }

        /// <summary>
        /// Gets or sets the support form user id.
        /// </summary>
        /// <value>The support form user id.</value>
        public static int SupportFormUserId
        {
            get { return supportFormUserId; }
            set { supportFormUserId = value; }
        }

        /// <summary>
        /// property for UserName
        /// </summary>
        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Property for User Id
        /// </summary>
        public static int ApplicationId
        {
            get { return applicationId; }
            set { applicationId = value; }
        }

        /// <summary>
        /// Property for FormPermissionsDataSet
        /// </summary>
        public static DataSet FormPermissionsDataSet
        {
            get { return formPermissionsDataSet; }
            set { formPermissionsDataSet = value; }
        }

        /// <summary>
        /// Gets or sets the terra scan form items.
        /// </summary>
        /// <value>The terra scan form items.</value>
        public static DataSet TerraScanFormItems
        {
            get { return terraScanFormItems; }
            set { terraScanFormItems = value; }
        }

        public static DataSet TerraScanCachedData
        {
            get { return terraScanCachedData; }
            set { terraScanCachedData = value; }
        }

        /// <summary>
        /// Gets or sets the name of the form.
        /// </summary>
        /// <value>The name of the form.</value>
        public static string FormName
        {
            get { return formName; }
            set { formName = value; }
        }

        /// <summary>
        /// Get and Set the Administrator Status
        /// </summary>
        public static bool Administrator
        {
            get { return isadministrator; }
            set { isadministrator = value; }
        }

        /// <summary>
        /// Gets or sets the get roll year.
        /// </summary>
        /// <value>The get roll year.</value>
        public static string GetRollYear
        {
            get { return getRollYear; }
            set { getRollYear = value; }
        }

        /// <summary>
        /// Get and Set the CheckOutStatus Status
        /// </summary>
        public static bool CheckOutStatus
        {
            get { return checkedOutStatus; }
            set { checkedOutStatus = value; }
        }

        /// <summary>
        /// Gets or sets the name of the field server.
        /// </summary>
        /// <value>The name of the field server.</value>
        public static string FieldServerName
        {
            get { return fieldServerName; }
            set { fieldServerName = value; }
        }

        /// <summary>
        /// Gets or sets the name of the field data base.
        /// </summary>
        /// <value>The name of the field data base.</value>
        public static string FieldDataBaseName
        {
            get { return fieldDataBaseName; }
            set { fieldDataBaseName = value; }
        }

        /// <summary>
        /// Gets or sets the validation user id.
        /// </summary>
        /// <value>The validation user id.</value>
        public static int ValidationUserId
        {
            get { return validationUserId; }
            set { validationUserId = value; }
        }

        /// <summary>
        /// Gets or sets the interest date.
        /// </summary>
        /// <value>The interest date.</value>
        public static DateTime InterestDate
        {
            get
            {
                if (interestDate.Equals(DateTime.MinValue))
                {
                    interestDate = WSHelper.F9001_GetNextWorkingDay();
                }

                return interestDate;
            }
            set { interestDate = value; }
        }

        /// <summary>
        /// Gets or sets the receipt date.
        /// </summary>
        /// <value>The receipt date.</value>
        public static DateTime ReceiptDate
        {
            get
            {
                if (receiptDate.Equals(DateTime.MinValue))
                {
                    receiptDate = WSHelper.F9001_GetNextWorkingDay();
                }

                return receiptDate;

            }
            set { receiptDate = value; }
        }

        /// <summary>
        /// Property to identify whether any file has been selected through opendialog from F9005 Attachment form
        /// </summary>
        /// <value>Flag for file selection</value>
        public static bool HasFilePathChanged
        {
            get
            {
                return isFilePathChanged;
            }
            set
            {
                isFilePathChanged = value;
            }
        }

        /// <summary>
        /// Get and Set the IsFieldUser
        /// </summary>
        public static bool IsFieldUser
        {
            get { return isFieldUser; }
            set { isFieldUser = value; }
        }

        /// <summary>
        /// Get and Set the IsFieldUser
        /// </summary>
        public static bool IsDataBaseAvailable
        {
            get { return isDataBaseAvailable; }
            set { isDataBaseAvailable = value; }
        }

        #endregion property

        #region public members

        #region Report

        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="reportID">The report ID.</param>
        /// <param name="reportType">Type of the report.</param>
        public static void ShowReport(int reportID, Reports.Report.ReportType reportType)
        {
            ShowReport(reportID, reportType, null, null);
        }

        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="reportID">The report ID.</param>
        /// <param name="reportType">Type of the report.</param>
        /// <param name="optionalParameters">The optional parameters.</param>
        public static void ShowReport(int reportID, Reports.Report.ReportType reportType, Hashtable optionalParameters)
        {
            ShowReport(reportID, reportType, optionalParameters, null);
        }

        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="reportID">The report ID.</param>
        /// <param name="reportType">Type of the report.</param>
        /// <param name="optionalParameters">The optional parameters.</param>
        public static void ShowReport(int reportID, Reports.Report.ReportType reportType, Hashtable optionalParameters, string printerName)
        {
            ////this.reportDetailsDataSet = WSHelper.GetReportDetails("10202", TerraScanCommon.UserId);
            Hashtable tempTable;
            reportDetailsDataSet = WSHelper.GetReportDetails(Convert.ToInt32(reportID), TerraScanCommon.UserId);
            if (reportDetailsDataSet != null && reportDetailsDataSet.Tables.Count > 0 && reportDetailsDataSet.Tables[0].Rows.Count > 0)
            {
                // Asigning Default Value for Reports
                Report.ReportDetails reportDetails = new Report.ReportDetails();
                //// reportDetails.FormId = this.Tag.ToString();
                reportDetails.Mode = reportType;
                //// reportDetails.ReportName = "10202";
                reportDetails.ReportName = reportID;
                ////reportDetails.KeyParameterName = "ReceiptID"; 

                if (optionalParameters != null)
                {
                    tempTable = new Hashtable(optionalParameters.Keys.Count);
                    foreach (object keyValue in optionalParameters.Keys)
                    {
                        if (keyValue != null)
                        {
                            if (keyValue.ToString() != "KeyID")
                            {
                                tempTable.Add(keyValue.ToString(), optionalParameters[keyValue]);
                            }
                            else
                            {
                                tempTable.Add(reportDetailsDataSet.Tables[0].Rows[0]["KeyName"].ToString(), optionalParameters[keyValue]);
                            }
                        }

                    }
                    reportDetails.OptionalReportParameter = tempTable;
                }
                ////reportDetails.KeyId = optionalParameters["parameterValue"].ToString() ;


                if (reportDetailsDataSet.Tables[0].Rows.Count > 0)
                {
                    reportDetails.ReportFile = reportDetailsDataSet.Tables[0].Rows[0]["ReportFile"].ToString();
                    reportDetails.ReportDescription = reportDetailsDataSet.Tables[0].Rows[0]["Description"].ToString();
                    reportDetails.ApplicationPathforPdf = Application.StartupPath;
                    GenerateReport(reportDetailsDataSet, reportDetails, printerName);
                }
                else
                {
                    ErrorEngine.ShowForm(2, reportID);
                }
            }
            else
            {
                // MessageBox.Show(SharedFunctions.GetResourceString("ErrorEngine"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ErrorEngine.ShowForm(1, reportID);
            }
        }

        /// <summary>
        /// Shows the audit report.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="keyValue">The key value.</param>
        public static void ShowAuditReport(string key, string keyValue)
        {
            // TODO : Genralized 
            Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                reportOptionalParameter.Add(key, keyValue);
                TerraScanCommon.ShowReport(17001, TerraScan.Common.Reports.Report.ReportType.Preview, reportOptionalParameter);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, null);
            }
        }

        #endregion

        #region AttachmentComment

        /// <summary>
        /// Sets the record count for Comment and Attachment Buttons.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="commentButton">The comment button.</param>
        /// <param name="attachmentButton">The attachment button.</param>
        /// <param name="currentForm">The current form.</param>
        public static void SetRecordCount(int formId, int keyId, Button commentButton, Button attachmentButton, Form currentForm)
        {
            ////Display Comments Count in the Comments Buttons
            int commentCounts = 0;
            try
            {
                ////todo: commentCounts = WSHelper.GetCommentsCount(formId, keyId, TerraScanCommon.UserId);
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, currentForm);
            }

            if (commentCounts > 0)
            {
                commentButton.Text = "Comment" + "(" + commentCounts + ")";
            }
            else
            {
                commentButton.Text = "Comment";
            }
            ////Display Attachment Count in the Attachment Buttons
            int attachmentCounts = 0;
            try
            {
                attachmentCounts = WSHelper.GetAttachmentCount(formId, keyId, TerraScanCommon.UserId);
            }
            catch (SoapException ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, currentForm);
            }

            if (attachmentCounts > 0)
            {
                attachmentButton.Text = "Attachment" + "(" + attachmentCounts + ")";
            }
            else
            {
                attachmentButton.Text = "Attachment";
            }
        }

        #endregion

        #region General

        /// <summary>
        /// Checks the valid email ID.
        /// </summary>
        /// <param name="sourceEmailId">The source email id.</param>
        /// <returns>true if valid email id else false</returns>
        public static bool CheckValidEmailID(string sourceEmailId)
        {
            if (!String.IsNullOrEmpty(sourceEmailId))
            {
                if (Regex.IsMatch(sourceEmailId, validEmailFormat))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Searches the control with key.
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        /// <param name="key">The key.</param>
        /// <returns>the Searched Control</returns>
        public static Control SearchControlWithKey(Control sourceControl, string key)
        {
            Control requiredControl = sourceControl;

            if (sourceControl != null)
            {
                if (sourceControl.Controls.ContainsKey(key))
                {
                    return sourceControl.Controls[key];
                }

                foreach (Control sampControl in sourceControl.Controls)
                {
                    if (sampControl.Controls.Count > 0)
                    {
                        requiredControl = SearchControlWithKey(sampControl, key);
                        if (requiredControl.Name.Equals(key))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                requiredControl = new Control();
            }

            return requiredControl;
        }

        #endregion

        #region Show Form
        public static Form ShowSketchForm(int formNo)
        {
            FormInfo formInfo;

            Form childForm = new Form();
            formInfo = GetFormInfo(formNo);
            string dllName = formInfo.formFile;
            string formName = string.Empty;
            string frmName = string.Empty;
            // This gets the Form name that needs to be displayed from the configuration file

            if (!string.IsNullOrEmpty(dllName))
            {
                try
                {
                    // System.Reflection.Assembly assembly = new System.Reflection.Assembly();
                    // Loads the current assembly
                    string dllName1 = dllName.Remove(dllName.LastIndexOf("."));

                    System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(dllName1);
                    frmName = dllName.Substring((dllName.LastIndexOf(".") + 1), (dllName.Length - dllName.LastIndexOf(".") - 1));
                    if (MdiWrapper.HasChild(formName) == true)
                    {
                        Type formType = mdiparent.GetType();
                        MethodInfo methodInfo = formType.GetMethod("GetChild");
                        childForm = (Form)methodInfo.Invoke(mdiparent, new object[] { formName });
                        //// childForm = md.GetChild(frmName);
                    }
                    else
                    {
                        formName = dllName;//+ dllName.Substring(dllName.LastIndexOf("."), (dllName.Length - dllName.LastIndexOf(".")));
                        //formName = "D3200.F3200";
                        //if (optionalParameter != null)
                        //{
                        //    childForm = (Form)assembly.CreateInstance(formName, true, BindingFlags.CreateInstance, null, optionalParameter, System.Globalization.CultureInfo.CurrentUICulture, null);
                        //}
                        //else
                        //{
                        childForm = (Form)assembly.CreateInstance(formName);
                        //  }
                    }


                    return childForm;
                }
                catch (Exception)
                {
                    ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
                    return null;
                }
            }
            else
            {
                ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
                return null;
            }

        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <returns>the Form Instance</returns>
        public static Form ShowForm(int formName, object[] optionalParameter)
        {
            // terraScanFormItems = WSHelper.GetMenuItems(TerraScan.Common.TerraScanCommon.UserId, 1);
            Form childForm = new Form();
            string findExp = "Form =" + formName;
            bool validMenu = false;
            int tableCount = terraScanFormItems.Tables.Count;
            for (int tables = 0; tables < tableCount; tables++)
            {
                formRows = terraScanFormItems.Tables[tables].Select(findExp);
                if (formRows.Length > 0)
                {
                    validMenu = true;
                    break;
                }
            }

            if (validMenu)
            {
                childForm = ShowForm(formName, formRows[0]["FormFile"].ToString(), formRows[0]["MenuName"].ToString(), optionalParameter);
                return childForm;
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="formName">Name of the form. which has to open has mdichild.</param>
        /// <returns>Show Form with the Parameter FormName</returns>
        public static Form ShowForm(int formName)
        {
            return ShowForm(formName, null);
        }

        /// <summary>
        /// Shows the form validation.
        /// </summary>
        /// <param name="FormNo">The form no.</param>
        /// <returns>newChildForm</returns>
        public static Form ShowFormValidation(int FormNo)
        {
            Form newChildForm = new Form();
            int formnumber = FormNo;
            newChildForm.Text = Convert.ToString(WSHelper.F9025FormValidationDetails(formnumber, TerraScanCommon.userId)); // "true"; // WSHelper.UserValidation(formnumber, TerraScanCommon.userId);
            if (newChildForm.Text == "1")
            {
                newChildForm.Text = "true";
                return newChildForm;
            }
            else
            {
                newChildForm.Text = "false";
                return newChildForm;
            }
        }

        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="modalWindow">if set to <c>true</c> [modal window].</param>
        /// <returns>the FormInstance</returns>
        public static Form GetForm(string formName, object[] optionalParameter, bool modalWindow)
        {
            string dllName = formName;
            string frmName = string.Empty;

            Form childForm = new Form();
            if (modalWindow)
            {
                string findExp = "Form =" + formName;
                /////                bool validMenu = false;
                int tableCount = terraScanFormItems.Tables.Count;
                for (int tables = 0; tables < tableCount; tables++)
                {
                    formRows = terraScanFormItems.Tables[tables].Select(findExp);
                    if (formRows.Length > 0)
                    {
                        //////                   validMenu = true;
                        dllName = formRows[0]["FormFile"].ToString();
                        break;
                    }
                }
            }

            // This gets the Form name that needs to be displayed from the configuration file

            if (!string.IsNullOrEmpty(dllName))
            {
                try
                {
                    // System.Reflection.Assembly assembly = new System.Reflection.Assembly();
                    // Loads the current assembly
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(dllName);

                    frmName = dllName.Substring((dllName.LastIndexOf(".") + 1), (dllName.Length - dllName.LastIndexOf(".") - 1));
                    if (MdiWrapper.HasChild(formName) == true)
                    {
                        Type formType = mdiparent.GetType();
                        MethodInfo methodInfo = formType.GetMethod("GetChild");
                        childForm = (Form)methodInfo.Invoke(mdiparent, new object[] { formName });
                        //// childForm = md.GetChild(frmName);
                    }
                    else
                    {
                        formName = dllName + dllName.Substring(dllName.LastIndexOf("."), (dllName.Length - dllName.LastIndexOf(".")));
                        if (optionalParameter != null)
                        {
                            childForm = (Form)assembly.CreateInstance(formName, true, BindingFlags.CreateInstance, null, optionalParameter, System.Globalization.CultureInfo.CurrentUICulture, null);
                        }
                        else
                        {
                            childForm = (Form)assembly.CreateInstance(formName);
                        }
                    }

                    return childForm;
                }
                catch (Exception)
                {
                    ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
                    return null;
                }
            }
            else
            {
                ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
                return null;
            }
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="formID">The form ID.</param>
        /// <param name="formName">Name of the form.</param>
        /// <param name="visibleFormName">Name of the visible form.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <returns>the Form Instance</returns>
        public static Form ShowForm(int formID, string formName, string visibleFormName, object[] optionalParameter)
        {
            Form childForm;
            childForm = null;
            try
            {
                ////TODO: This methodology needs to be validated for performance
                string dllName = formName;
                string frmName = string.Empty;

                //// This gets the Form name that needs to be displayed from the configuration file
                if (!string.IsNullOrEmpty(formName))
                {
                    ////childForm = GetForm(formID.ToString(), optionalParameter, true);
                    if (childForm != null)
                    {
                        Type formType = childForm.GetType();
                        PropertyInfo propertyInfo = formType.GetProperty("CurrentFormName");
                        propertyInfo.SetValue(childForm, visibleFormName, null);
                        childForm.Name = frmName.Trim();
                        ((MdiWrapper)childForm).FormID = formID;
                        ((MdiWrapper)childForm).FormDLLName = formName;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                return childForm;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return childForm;
            }
            catch (Exception)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return childForm;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns Form Name</returns>
        public static string GetValue(Form formName, string propertyName)
        {
            Type formType = formName.GetType();
            PropertyInfo propertyInfo = formType.GetProperty(propertyName);
            return propertyInfo.GetValue(formName, null).ToString();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns Form Name</returns>
        public static object GetObject(Form formName, string propertyName)
        {
            Type formType = formName.GetType();
            PropertyInfo propertyInfo = formType.GetProperty(propertyName);
            return propertyInfo.GetValue(formName, null);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public static void SetValue(Form formName, string propertyName, object value)
        {
            Type formType = formName.GetType();
            PropertyInfo propertyInfo = formType.GetProperty(propertyName);
            propertyInfo.SetValue(formName, value, null);
        }

        /// <summary>
        /// Gets the XML string.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>the XML String</returns>
        public static string GetXmlString(DataTable dt)
        {
            DataSet ds = new DataSet("Root");
            DataTable tempDt = new DataTable();
            if (dt != null)
            {
                tempDt = dt.Copy();
            }

            tempDt.TableName = "Table";
            ds.Tables.Add(tempDt);
            return ds.GetXml();
        }

        #endregion Show Form

        #region DataGrid

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="tempDataGridview">The temp data gridview.</param>
        /// <param name="commentRow">The comment row.</param>
        public static void SetDataGridViewPosition(DataGridView tempDataGridview, int commentRow)
        {
            int i = 0;
            try
            {
                if (tempDataGridview.Rows.Count > 0 && commentRow >= 0)
                {
                    tempDataGridview.Rows[Convert.ToInt32(commentRow)].Selected = true;
                    for (i = 0; i <= tempDataGridview.ColumnCount; i++)
                    {
                        if (tempDataGridview[i, Convert.ToInt32(commentRow)].Visible == true)
                        {
                            break;
                        }
                    }

                    tempDataGridview.CurrentCell = tempDataGridview[i, Convert.ToInt32(commentRow)];
                }
            }
            catch (IndexOutOfRangeException rangeException)
            {
                ExceptionManager.ManageException(rangeException, ExceptionManager.ActionType.CloseCurrentForm, mdiparent);
            }
            catch (SystemException dataGridPositionException)
            {
                ExceptionManager.ManageException(dataGridPositionException, ExceptionManager.ActionType.CloseCurrentForm, mdiparent);
            }
        }

        /// <summary>
        /// Sets the same property.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="empty">The empty.</param>
        /// <param name="maxRow">The max row.</param>
        public static void SetSameProperty(DataGridView source, DataGridView empty, int maxRow)
        {
            empty.Location = source.Location;
            empty.Parent = source.Parent;
            SetEmptyGridHeight(empty, maxRow);

            if (empty.Rows.Count > 0)
            {
                empty.Rows[0].Selected = false;
            }
        }

        /// <summary>
        /// Used to create a empty row in a Datagrid.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>returns datatable</returns>
        public static DataTable CreateEmptyRows(DataTable sourceDataTable, int maxRowCount)
        {
            DataRow tempRow;
            if (sourceDataTable != null)
            {
                if (sourceDataTable.Rows.Count < maxRowCount)
                {
                    for (int i = 0; i < maxRowCount; i++)
                    {
                        tempRow = sourceDataTable.NewRow();
                        sourceDataTable.Rows.Add(tempRow);
                    }
                }
            }

            return sourceDataTable;
        }

        /// <summary>
        /// Sets the height of the grid.
        /// </summary>
        /// <param name="dataGridView">The data grid view.</param>
        /// <param name="maxRow">The max row.</param>
        public static void SetGridHeight(DataGridView dataGridView, int maxRow)
        {
            if (dataGridView != null)
            {
                if (((DataTable)dataGridView.DataSource) != null)
                {
                    if (((DataTable)dataGridView.DataSource).Rows.Count <= maxRow)
                    {
                        dataGridView.Height = ((dataGridView.ColumnHeadersHeight - 1) * (((DataTable)dataGridView.DataSource).Rows.Count + 1)) + 1;
                    }
                    else
                    {
                        dataGridView.Height = ((dataGridView.ColumnHeadersHeight - 1) * (maxRow + 1)) + 1;
                    }
                }
                else
                {
                    dataGridView.Height = ((dataGridView.ColumnHeadersHeight - 1) * 1) + 1;
                }
            }
        }

        /// <summary>
        /// Sets the data grid view cell position.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="commentRow">The comment row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        public static void SetDataGridViewCellPosition(Control controlName, int commentRow, int columnIndex)
        {
            DataGridView tempDataGridview = (DataGridView)controlName;
            try
            {
                tempDataGridview.Rows[Convert.ToInt32(commentRow)].Selected = false;
                tempDataGridview.CurrentCell = tempDataGridview[columnIndex, Convert.ToInt32(commentRow)];
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, mdiparent);
            }
        }

        #endregion DataGrid

        #region Form Related Methods

        /// <summary>
        /// Mothod will get the Form Information
        /// </summary>
        /// <param name="formId">formId to get form Details</param>
        /// <returns>formInfo Struct</returns>
        public static FormInfo GetFormInfo(int formId)
        {
            ////return GetFormInfo(formId, TerraScanCommon.TerraScanFormItems);
            FormInfo formInfo;
            formInfo.form = formId;
            formInfo.formFile = string.Empty;
            formInfo.openPermission = 0;
            formInfo.visibleName = string.Empty;
            formInfo.optionalParameters = null;
            formInfo.addPermission = 0;
            formInfo.editPermission = 0;
            formInfo.deletePermission = 0;
            SupportFormData.GetFormDetailsDataTable permissionDataTable = new SupportFormData.GetFormDetailsDataTable();
            permissionDataTable = TerraScanCommon.GetFormPermissionDetails(formId, TerraScanCommon.UserId);
            if (permissionDataTable.Rows.Count > 0)
            {
                formInfo.formFile = permissionDataTable.Rows[0]["FormFile"].ToString();
                formInfo.visibleName = permissionDataTable.Rows[0]["MenuName"].ToString();
                formInfo.openPermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionOpen"]));
                formInfo.addPermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionAdd"]));
                formInfo.editPermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionEdit"]));
                formInfo.deletePermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionDelete"]));
            }

            return formInfo;
        }

        /// <summary>
        /// Mothod will get the Form Information
        /// </summary>
        /// <param name="formId">formId to get form Details</param>
        /// <param name="tempDataSet">dataset which holds all form details</param>
        /// <returns>formInfo Array</returns>
        public static FormInfo GetFormInfo(int formId, DataSet tempDataSet)
        {
            FormInfo formInfo;
            formInfo.form = formId;
            formInfo.formFile = string.Empty;
            formInfo.openPermission = 0;
            formInfo.visibleName = string.Empty;
            formInfo.optionalParameters = null;
            formInfo.addPermission = 0;
            formInfo.editPermission = 0;
            formInfo.deletePermission = 0;
            string findExp = "Form =" + formId.ToString();
            int tableCount = tempDataSet.Tables.Count;
            for (int tables = 0; tables < tableCount; tables++)
            {
                formRows = tempDataSet.Tables[tables].Select(findExp);
                if (formRows.Length > 0)
                {
                    formInfo.formFile = formRows[0]["FormFile"].ToString();
                    formInfo.visibleName = formRows[0]["MenuName"].ToString();
                    formInfo.openPermission = Convert.ToInt16(formRows[0]["IsPermissionOpen"]);
                    formInfo.addPermission = Convert.ToInt16(formRows[0]["IsPermissionAdd"]);
                    formInfo.editPermission = Convert.ToInt16(formRows[0]["IsPermissionEdit"]);
                    formInfo.deletePermission = Convert.ToInt16(formRows[0]["IsPermissionDelete"]);
                    break;
                }
            }

            return formInfo;
        }

        /// <summary>
        /// Gets the form information.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>formId</returns>
        public static FormInfo GetFormInformation(int formId)
        {
            FormInfo formInfo;
            formInfo.form = formId;
            formInfo.formFile = string.Empty;
            formInfo.openPermission = 0;
            formInfo.visibleName = string.Empty;
            formInfo.optionalParameters = null;
            formInfo.addPermission = 0;
            formInfo.editPermission = 0;
            formInfo.deletePermission = 0;
            SupportFormData.GetFormDetailsDataTable permissionDataTable = new SupportFormData.GetFormDetailsDataTable();
            permissionDataTable = TerraScanCommon.GetFormPermissionDetails(formId, TerraScanCommon.UserId);
            if (permissionDataTable.Rows.Count > 0)
            {
                formInfo.formFile = permissionDataTable.Rows[0]["FormFile"].ToString();
                formInfo.visibleName = permissionDataTable.Rows[0]["MenuName"].ToString();
                formInfo.openPermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionOpen"]));
                formInfo.addPermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionAdd"]));
                formInfo.editPermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionEdit"]));
                formInfo.deletePermission = Convert.ToInt32(Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionDelete"]));
            }

            return formInfo;
        }

        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>the FormInstance</returns>
        public static Form GetForm(int formId, object[] optionalParameter, WorkItem parentWorkItem)
        {
            string dllName = string.Empty;
            string currentWorkItemName = string.Empty;
            FormInfo formInfo;
            Form childForm = new Form();
            WorkItem currentWorkItem = new WorkItem();

            formInfo = GetFormInfo(formId);

            if (!string.IsNullOrEmpty(formInfo.formFile))
            {
                try
                {
                    currentWorkItemName = formInfo.formFile.Trim() + "WorkItem";
                    if (!parentWorkItem.Items.Contains(currentWorkItemName))
                    {
                        childForm = CreateFormInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                    }
                    else
                    {
                        currentWorkItem = (WorkItem)parentWorkItem.Items.Get(currentWorkItemName);
                        childForm = (Form)currentWorkItem.Items.Get(formInfo.formFile);
                        currentWorkItem.Terminate();
                        childForm.Dispose();
                        childForm = CreateFormInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                    }

                    return childForm;
                }
                catch (Exception ex1)
                {
                    ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six, formId);
                    return null;
                }
            }
            else
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six, formId);
                ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        /// <summary>
        /// Gets the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="optionalParameter">The optional parameter.</param>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>the FormInstance</returns>
        public static UserControl GetSmartPart(int formId, object[] optionalParameter, WorkItem parentWorkItem)
        {
            string dllName = string.Empty;
            string currentWorkItemName = string.Empty;
            FormInfo formInfo;
            UserControl childForm = new UserControl();
            WorkItem currentWorkItem = new WorkItem();

            ////formInfo = GetFormInfo(formId);

            formInfo = GetFormInformation(formId);

            if (!string.IsNullOrEmpty(formInfo.formFile))
            {
                currentWorkItemName = formInfo.formFile.Trim() + "WorkItem";
                if (!parentWorkItem.Items.Contains(currentWorkItemName))
                {
                    childForm = CreateSmartPartInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                }
                else
                {
                    currentWorkItem = (WorkItem)parentWorkItem.Items.Get(currentWorkItemName);
                    childForm = (UserControl)currentWorkItem.Items.Get(formInfo.formFile);
                    currentWorkItem.Terminate();
                    childForm.Dispose();
                    childForm = CreateSmartPartInstance(formInfo.formFile, optionalParameter, currentWorkItemName, parentWorkItem);
                }

                return childForm;
            }
            else
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
                ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public static SupportFormData.GetFormDetailsDataTable GetFormPermissionDetails(int form, int userId)
        {
            if (TerraScanCommon.userId == userId)
            {
                DataRow[] selectedRow;
                selectedRow = TerraScanCommon.TerraScanCachedData.Tables[0].Select("Form =" + form);
                SupportFormData.GetFormDetailsDataTable currentTable = new SupportFormData.GetFormDetailsDataTable();
                if (selectedRow != null && selectedRow.Length > 0)
                {
                    SupportFormData.GetFormDetailsRow temprow = currentTable.NewGetFormDetailsRow();
                    temprow.USER_NAMES = TerraScanCommon.UserName;
                    temprow.FormFile = selectedRow[0]["FormFile"].ToString();
                    temprow.Description = selectedRow[0]["Description"].ToString();
                    temprow.MenuName = selectedRow[0]["MenuName"].ToString();
                    temprow.MenuOrder = Convert.ToByte(selectedRow[0]["MenuOrder"]);
                    temprow.MenuGroupID = Convert.ToInt16(selectedRow[0]["MenuGroupID"]);
                    temprow.IsPermissionMenu = Convert.ToBoolean(selectedRow[0]["IsPermissionMenu"]).ToString();
                    temprow.IsPermissionOpen = Convert.ToBoolean(selectedRow[0]["IsPermissionOpen"]).ToString();
                    temprow.IsPermissionEdit = Convert.ToBoolean(selectedRow[0]["IsPermissionEdit"]).ToString();
                    temprow.IsPermissionAdd = Convert.ToBoolean(selectedRow[0]["IsPermissionAdd"]).ToString();
                    temprow.IsPermissionDelete = Convert.ToBoolean(selectedRow[0]["IsPermissionDelete"]).ToString();
                    temprow.IsSlice = selectedRow[0]["IsSlice"].ToString();
                    currentTable.Rows.Add(temprow);
                }
                return currentTable;
            }
            else
            {
                return WSHelper.GetFormDetails(form, userId).GetFormDetails;
            }
        }

        #endregion

        #region ComboBoxDefaultValues

        #region TrueFalseValue
        /// <summary>
        /// Sets the data grid view cell position.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>DataSet with the value</returns>
        public static CommonData SetComboBoxDefaultValue(object[] values)
        {
            CommonData comboBoxTrueFalseValue = new CommonData();
            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    DataRow comboBoxRow = comboBoxTrueFalseValue.ComboBoxDataTable.NewRow();
                    comboBoxRow[comboBoxTrueFalseValue.ComboBoxDataTable.KeyIdColumn.ColumnName] = i;
                    comboBoxRow[comboBoxTrueFalseValue.ComboBoxDataTable.KeyNameColumn.ColumnName] = values[i].ToString();
                    comboBoxTrueFalseValue.ComboBoxDataTable.Rows.Add(comboBoxRow);
                }

                return comboBoxTrueFalseValue;
            }
            catch (Exception)
            {
                ////MessageBox.Show(SharedFunctions.GetResourceString("ErrorLoadingForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.Six);
                return null;
            }
        }
        #endregion

        #endregion  ComboBoxDefaultValues

        #region Custom Decimal Format (TextBox)

        /// <summary>
        /// Custom Format For Penalty Percent Field
        /// </summary>
        public static string CustomDecimalFormat(string textBoxValue)
        {
            int stringLength = textBoxValue.Length;

            // Get Deimal position of the value
            int decPosition = textBoxValue.IndexOf(".");
            string customFormat = string.Empty;

            // Get Precision value
            if (decPosition != -1)
            {
                if (stringLength - (decPosition + 1) > 0)
                {
                    textBoxValue = textBoxValue.Substring(decPosition + 1, stringLength - (decPosition + 1)).Trim();
                }
            }

            int nonzerocount = 0;

            // Get number of precision in the value
            for (int i = textBoxValue.Length; i >= 1; i--)
            {
                string arrChar = Convert.ToString(textBoxValue[i - 1]);
                if (arrChar.Equals("0"))
                {
                    if (nonzerocount >= 1)
                    {
                        nonzerocount++;
                    }
                }
                else
                {
                    nonzerocount++;
                }
            }

            // Based on the precision presents in the textbox value, set the textbox format
            if (decPosition != -1)
            {
                switch (nonzerocount)
                {
                    case 0:
                        customFormat = "#,##0.0";
                        break;
                    case 1:
                        customFormat = "#,##0.0";
                        break;
                    case 2:
                        customFormat = "#,##0.00";
                        break;
                    case 3:
                        customFormat = "#,##0.000";
                        break;
                    case 4:
                        customFormat = "#,##0.0000";
                        break;
                    case 5:
                        customFormat = "#,##0.00000";
                        break;
                    case 6:
                        customFormat = "#,##0.000000";
                        break;
                }
            }
            else
            {
                customFormat = "#,##0.0";
            }

            return customFormat;
        }

        #endregion Custom Decimal Format (TextBox)

        #region F3230 FieldUse

        /// <summary>
        /// AddFieldUseValues
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyName"></param>
        /// <param name="Form"></param>
        /// <param name="ModuleID"></param>
        /// <param name="InsertedBy"></param>
        /// <returns></returns>
        public static int AddFieldUseValues(int KeyID, string KeyName, int Form, int? ModuleID, int InsertedBy)
        {
            return WSHelper.InsertFieldUseAddDetails(KeyID, KeyName, Form, ModuleID, InsertedBy);
        }

        /// <summary>
        /// InsertFieldUseDetails
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyField"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static int InsertFieldUseDetails(int KeyID, string KeyField, int UserID)
        {
            return WSHelper.InsertFieldUseDetails(KeyID, KeyField, UserID);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the empty height of the grid.
        /// </summary>
        /// <param name="dataGridView">The data grid view.</param>
        /// <param name="maxRow">The max row.</param>
        private static void SetEmptyGridHeight(DataGridView dataGridView, int maxRow)
        {
            dataGridView.Height = ((dataGridView.ColumnHeadersHeight - 1) * (maxRow + 1)) + 1;
        }

        /// <summary>
        /// Admins the user validation form.
        /// </summary>
        /// <param name="parentWorkItem">The parent work item.</param>
        /// <returns>true</returns>
        public static bool AdminUserValidationForm(WorkItem parentWorkItem)
        {
            Form LoginValidationForm = new Form();
            LoginValidationForm = TerraScanCommon.GetForm(9025, null, parentWorkItem);
            if (LoginValidationForm != null)
            {
                DialogResult dr = LoginValidationForm.ShowDialog();
                if (dr.Equals(DialogResult.OK))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to Handle the GenerateReport
        /// </summary>
        /// <param name="tmpReportDetailsDataSet">The TMP report details data set.</param>
        /// <param name="reportDetails">reportDetails</param>
        private static void GenerateReport(DataSet tmpReportDetailsDataSet, Report.ReportDetails reportDetails, string printerName)
        {
            if (tmpReportDetailsDataSet.Tables[0].Rows.Count > 1)
            {
                /////MultipleReports multipleReports = new MultipleReports(this.reportDetailsDataSet, "1020", this.ReceiptEngineUserControl.PreviousReceiptId, "10202", reportType);
                MultipleReports multipleReports = new MultipleReports(reportDetails, tmpReportDetailsDataSet, printerName);
                multipleReports.ShowDialog();
            }
            else
            {
                report.OpenReport(reportDetails, printerName);
            }
        }

        /// <summary>
        /// Added for F9070 Report listing form
        /// </summary>
        public static void ShowReportPreview(int reportID, Reports.Report.ReportType reportType, string reportFile, string reportDescription)
        {
            Report.ReportDetails reportDetails = new Report.ReportDetails();
            reportDetails.Mode = reportType;
            reportDetails.ReportName = reportID;
            reportDetails.ReportFile = reportFile;
            reportDetails.ReportDescription = reportDescription;
            reportDetails.ApplicationPathforPdf = Application.StartupPath;
            report.OpenReport(reportDetails, null);
        }


        /// <summary>
        /// Method to Create the Instance of the Form for given formId
        /// </summary>
        /// <param name="formFile">formId to get the Form</param>
        /// <param name="optionalParameter">optionalParameter when constructing the form</param>
        /// <param name="currentWorkItemName">Name of the Current WorkItem</param>
        /// <param name="parentWorkItem">ParentWorkItem to Add ChildWorkItem</param>
        /// <returns>Form Instance</returns>
        private static Form CreateFormInstance(string formFile, object[] optionalParameter, string currentWorkItemName, WorkItem parentWorkItem)
        {
            string dllName = string.Empty;
            WorkItem currentWorkItem = new WorkItem();
            Form childForm = new Form();
            if (!string.IsNullOrEmpty(formFile))
            {
                dllName = formFile.Remove(formFile.LastIndexOf("."));
                System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(dllName);
                currentWorkItem = (WorkItem)assembly.CreateInstance(currentWorkItemName);
                if (optionalParameter != null)
                {
                    childForm = (Form)assembly.CreateInstance(formFile, true, BindingFlags.CreateInstance, null, optionalParameter, System.Globalization.CultureInfo.CurrentUICulture, null);
                }
                else
                {
                    childForm = (Form)assembly.CreateInstance(formFile);
                }

                parentWorkItem.Items.Add(currentWorkItem, currentWorkItemName);
                currentWorkItem.Items.Add(childForm, formFile);
            }

            return childForm;
        }

        /// <summary>
        /// Method to Create the Instance of the Form for given formId
        /// </summary>
        /// <param name="formFile">formId to get the Form</param>
        /// <param name="optionalParameter">optionalParameter when constructing the form</param>
        /// <param name="currentWorkItemName">Name of the Current WorkItem</param>
        /// <param name="parentWorkItem">ParentWorkItem to Add ChildWorkItem</param>
        /// <returns>Form Instance</returns>
        private static UserControl CreateSmartPartInstance(string formFile, object[] optionalParameter, string currentWorkItemName, WorkItem parentWorkItem)
        {
            string dllName = string.Empty;
            WorkItem currentWorkItem = new WorkItem();
            UserControl childForm = new UserControl();
            if (!string.IsNullOrEmpty(formFile))
            {
                dllName = formFile.Remove(formFile.LastIndexOf("."));
                System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(dllName);
                currentWorkItem = (WorkItem)assembly.CreateInstance(currentWorkItemName);
                if (optionalParameter != null)
                {
                    childForm = (UserControl)assembly.CreateInstance(formFile, true, BindingFlags.CreateInstance, null, optionalParameter, System.Globalization.CultureInfo.CurrentUICulture, null);
                }
                else
                {
                    childForm = (UserControl)assembly.CreateInstance(formFile);
                }

                parentWorkItem.Items.Add(currentWorkItem, currentWorkItemName);
                currentWorkItem.Items.Add(childForm, formFile);
            }

            return childForm;
        }

        //private void ShowFormSlice()
        //{
        //    string formFile = string.Empty;
        //    string visibleName = string.Empty;
        //    FormInfo getPermissionForm = new FormInfo();
        //    bool isSlice;
        //    PermissionFields permissions;
        //    SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        //    Form camaForm = null;
        //    #region Debug Mode Code //Added by Jyothi

        //    if (TerraScanCommon.activateDebugMode.Equals(true) && TerraScanCommon.debugConfiguration.Equals(true))
        //    {
        //        SupportFormData.GetFormManagementDetailsDataTable getFormSlicesDetails = new SupportFormData.GetFormManagementDetailsDataTable();
        //        FormInfo getSliceFormPermission = new FormInfo();
        //        string optionalValues = "";
        //        string formFileSlice = string.Empty;
        //        PermissionFields permissions1;

        //        getFormSlicesDetails = F9017Controll.WorkItem.F9002_GetFormManagementDetails(Convert.ToInt32(FormIDTextBox.Text.Trim()), Convert.ToInt32(UserListCombo.SelectedValue.ToString()));

        //        ////if (getFormSlicesDetails.Rows.Count == 3)
        //        ////{
        //        for (int count = 0; count < getFormSlicesDetails.Rows.Count; count++)
        //        {
        //            permissions1.newPermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionAddColumn.ColumnName].ToString());
        //            getSliceFormPermission.addPermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionAddColumn.ColumnName]));

        //            permissions1.openPermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionOpenColumn.ColumnName].ToString());
        //            getSliceFormPermission.openPermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionOpenColumn.ColumnName]));

        //            permissions1.editPermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionEditColumn.ColumnName].ToString());
        //            getSliceFormPermission.editPermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionEditColumn.ColumnName]));

        //            permissions1.deletePermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionDeleteColumn.ColumnName].ToString());
        //            getSliceFormPermission.deletePermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionDeleteColumn.ColumnName]));

        //            formFileSlice = getFormSlicesDetails.Rows[count][getFormSlicesDetails.FormFileColumn.ColumnName].ToString();
        //            getSliceFormPermission.formFile = formFileSlice;

        //            getSliceFormPermission.form = Convert.ToInt32(FormIDTextBox.Text.Trim());

        //            if (getSliceFormPermission.optionalParameters != null)
        //            {
        //                if (getSliceFormPermission.optionalParameters[0] != null)
        //                {
        //                    optionalValues = optionalValues + "Key ID = " + getSliceFormPermission.optionalParameters[0] + "\n";
        //                }

        //                for (int iCount = 0; count < 3; iCount++)
        //                {
        //                    if (getSliceFormPermission.optionalParameters[iCount] != null)
        //                    {
        //                        optionalValues = optionalValues + "Other Parameter " + iCount + " = " + getSliceFormPermission.optionalParameters[iCount] + "\n";
        //                    }
        //                }
        //            }

        //            MessageBox.Show("Form: " + getSliceFormPermission.form + "\n" + "FormFile: " + getSliceFormPermission.formFile + "\n" + "Open Permission: " + Convert.ToBoolean(getSliceFormPermission.openPermission) + "\n" + "Add Permission: " + Convert.ToBoolean(getSliceFormPermission.addPermission) + "\n" + "Edit Permission: " + Convert.ToBoolean(getSliceFormPermission.editPermission) + "\n" + "Delete Permission: " + Convert.ToBoolean(getSliceFormPermission.deletePermission) + "\n" + optionalValues, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }

        //        TerraScanCommon.debugSliceConfiguration = true;
        //        ////}
        //    }

        //    #endregion Debug Mode Code ////Ended by Jyothi

        //    this.getFormDetailsDataDetails = this.F9017Controll.WorkItem.GetFormDetails(Convert.ToInt32(FormIDTextBox.Text.Trim()), Convert.ToInt32(UserListCombo.SelectedValue.ToString()));
        //    if (this.getFormDetailsDataDetails.Rows.Count > 0)
        //    {
        //        this.permissions.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
        //        getPermissionForm.addPermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName]));

        //        this.permissions.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
        //        getPermissionForm.openPermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName]));

        //        this.permissions.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
        //        getPermissionForm.editPermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName]));

        //        this.permissions.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
        //        getPermissionForm.deletePermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName]));

        //        formFile = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.FormFileColumn.ColumnName].ToString();
        //        getPermissionForm.formFile = formFile;

        //        visibleName = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.MenuNameColumn.ColumnName].ToString();
        //        getPermissionForm.visibleName = visibleName;
        //        getPermissionForm.form = Convert.ToInt32(FormIDTextBox.Text.Trim());

        //        isSlice = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsSliceColumn.ColumnName].ToString());

        //        if (!isSlice)
        //        {
        //            if (this.permissions.openPermission && Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionMenuColumn.ColumnName].ToString()))
        //            {
        //                ////FormInfo getPermissionForm = TerraScan.Common.TerraScanCommon.GetFormInfo(Convert.ToInt32(FormIDTextBox.Text.Trim())); ////.GetForm(9002, optionalParameter, this.form9016Control.WorkItem);
        //                getPermissionForm.optionalParameters = new object[6];
        //                ////getPermissionForm.optionalParameters[0] = Convert.ToInt32(this.FormIDTextBox.Text.Trim());
        //                if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                {
        //                    ////string str1 = (str.Trim() = string.Empty ? "Hi" : "Bye");
        //                    getPermissionForm.optionalParameters[0] = this.Parameter1TextBox.Text.Trim();

        //                    if (!string.IsNullOrEmpty(this.Parameter2TextBox.Text.Trim()))
        //                    {
        //                        if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                        {
        //                            getPermissionForm.optionalParameters[1] = this.Parameter2TextBox.Text.Trim();
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            this.Parameter1TextBox.Focus();
        //                            return;
        //                        }
        //                    }

        //                    if (!string.IsNullOrEmpty(this.Parameter3TextBox.Text.Trim()))
        //                    {
        //                        if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                        {
        //                            getPermissionForm.optionalParameters[2] = this.Parameter3TextBox.Text.Trim();
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            this.Parameter1TextBox.Focus();
        //                            return;
        //                        }
        //                    }

        //                    if (!string.IsNullOrEmpty(this.Parameter4Textbox.Text.Trim()))
        //                    {
        //                        if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                        {
        //                            getPermissionForm.optionalParameters[3] = this.Parameter4Textbox.Text.Trim();
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            this.Parameter1TextBox.Focus();
        //                            return;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(this.Parameter2TextBox.Text.Trim()))
        //                    {
        //                        if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                        {
        //                            getPermissionForm.optionalParameters[1] = this.Parameter2TextBox.Text.Trim();
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            this.Parameter1TextBox.Focus();
        //                            return;
        //                        }
        //                    }
        //                    else if (!string.IsNullOrEmpty(this.Parameter3TextBox.Text.Trim()))
        //                    {
        //                        if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                        {
        //                            getPermissionForm.optionalParameters[2] = this.Parameter3TextBox.Text.Trim();
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            this.Parameter1TextBox.Focus();
        //                            return;
        //                        }
        //                    }
        //                    else if (!string.IsNullOrEmpty(this.Parameter4Textbox.Text.Trim()))
        //                    {
        //                        if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                        {
        //                            getPermissionForm.optionalParameters[3] = this.Parameter4Textbox.Text.Trim();
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            this.Parameter1TextBox.Focus();
        //                            return;
        //                        }
        //                    }
        //                }

        //                getPermissionForm.optionalParameters[4] = this.permissions;
        //                getPermissionForm.optionalParameters[5] = this.UserListCombo.SelectedValue.ToString();
        //                ////getPermissionForm.formFile = formFile;
        //                ////getPermissionForm.visibleName = visibleName;
        //                ////getPermissionForm.openPermission = Convert.ToInt32(this.permissions.openPermission);
        //                TerraScanCommon.SupportFormUserId = Convert.ToInt32(this.UserListCombo.SelectedValue);

        //                /* Code For CAMA Sketh*/
        //                Boolean CAMAForm = false;
        //                int objectID = 0;
        //                for (int CamaFormNo = 0; CamaFormNo < TerraScanCommon.CAMASketchFormDetails.Length; CamaFormNo++)
        //                {
        //                    if (TerraScanCommon.CAMASketchFormDetails[CamaFormNo].Contains(FormIDTextBox.Text.Trim()))
        //                    {
        //                        CAMAForm = true;
        //                    }

        //                }
        //                if (CAMAForm)
        //                {
        //                    if (!String.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
        //                    {
        //                        Int32.TryParse(this.Parameter1TextBox.Text.Trim(), out objectID);
        //                    }
        //                    if (objectID > 0)
        //                    {
        //                        if (!formOpened)
        //                        {
        //                            camaForm = new Form();
        //                            camaForm = TerraScanCommon.ShowSketchForm(3200);
        //                            Form mainFrm = new Form();
        //                            mainFrm = (Form)((Form)this.ParentForm).ParentForm;
        //                            foreach (Form f in mainFrm.MdiChildren)
        //                            {
        //                                if (f.Name == "CAMASketch")
        //                                {
        //                                    f.Close();
        //                                    f.Dispose();
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (formOpened)
        //                            {
        //                                //camaForm.Dispose(); 
        //                                //camaForm.Close(); 
        //                                camaForm = new Form();
        //                                camaForm = TerraScanCommon.ShowSketchForm(3200);
        //                                Form formAlreadyOpened = (Form)TerraScanCommon.GetObject(camaForm, "GetOpenedForm");
        //                                formAlreadyOpened.Close();
        //                                Boolean formClosed = (Boolean)TerraScanCommon.GetObject(camaForm, "FormOpened");
        //                                formOpened = formClosed;
        //                                if (!formClosed)
        //                                {
        //                                    camaForm = TerraScanCommon.ShowSketchForm(3200);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Form mainFrm = new Form();
        //                                mainFrm = (Form)((Form)this.ParentForm).ParentForm;
        //                                foreach (Form f in mainFrm.MdiChildren)
        //                                {
        //                                    if (f.Name == "CAMASketch")
        //                                    {
        //                                        f.Dispose();
        //                                    }

        //                                }
        //                            }
        //                        }
        //                        if (!formOpened)
        //                        {
        //                            TerraScanCommon.SetValue(camaForm, "SetObjectID", objectID);
        //                            TerraScanCommon.SetValue(camaForm, "SetMDI", ((Form)((Form)this.ParentForm).ParentForm));
        //                        }
        //                        formOpened = (Boolean)TerraScanCommon.GetObject(camaForm, "FormOpened");
        //                    }
        //                }
        //                else
        //                {   ////Endshere                                     
        //                    this.ShowForm(this, new DataEventArgs<FormInfo>(getPermissionForm));
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show(SharedFunctions.GetResourceString("PermissionCheck"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                this.PreviewPanel.Visible = false;
        //                this.UserListCombo.Focus();
        //                ////this.FormIDTextBox.Text = string.Empty;                            
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Slice Form cannot be opened", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.PreviewPanel.Visible = false;
        //            this.FormIDTextBox.Focus();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(SharedFunctions.GetResourceString("InvalidForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        this.PreviewPanel.Visible = false;
        //        this.FormIDTextBox.Focus();
        //        ////this.FormIDTextBox.Text = string.Empty;                            
        //    }
        //}

        #endregion
    }
}
