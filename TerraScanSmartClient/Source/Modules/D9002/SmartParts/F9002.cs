//--------------------------------------------------------------------------------------------
// <copyright file="F9002.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created// 
//*********************************************************************************/

namespace D9002
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    using TerraScan.Helper;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F9002
    /// </summary>
    [SmartPart]
    public partial class F9002 : BaseSmartPart
    {
        #region Readonly Variables

        #region  userTab

        /// <summary>
        /// to Find The Grid
        /// </summary>
        private readonly string groupGrid = "Group";

        /// <summary>
        /// to Find The Grid
        /// </summary>
        private readonly string userGrid = "User";

        /// <summary>
        /// to assing Temp wrong id so 
        /// </summary>
        private readonly string emptyKeyID = "-1";

        /// <summary>
        /// store the user audit link text
        /// </summary>
        private readonly string userAuditLink = "tTS_User [UserID]: ";

        /// <summary>
        /// store the user Group link text
        /// </summary>
        private readonly string groupLink = "tTS_UserGroup [GroupID]: ";

        #endregion

        #region

        #endregion

        #endregion

        #region Common

        /// <summary>
        /// used  to indicate form is loaded or not
        /// </summary>
        private bool formLoaded;

        /// <summary>
        /// Created Instance for f1105Controller
        /// </summary>
        private F9002Controller form9002Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// rowCount
        /// </summary>
        ////  private int rowCount;

        #endregion

        #region Variable For User Tab

        /// <summary>
        /// BusinessEntities.UserManagement  userManagement = new BusinessEntities.UserManagement(); 
        /// </summary>
        private UserManagementData userManagement = new UserManagementData();

        /// <summary>
        ///  keep a track on a user Grid Click
        /// </summary>
        private bool userGridClick;

        /// <summary>
        /// indicates user grid sorted 
        /// </summary>
        private bool sortingUserGrid;

        /// <summary>
        /// Used to check valid Dataset or not
        /// </summary>
        private bool validDataSet;

        /// <summary>
        /// used to store button operation
        /// </summary>
        private int buttonOperation;

        /// <summary>
        /// used to store button operation
        /// </summary>
        private bool keyPressed;

        /////// <summary>
        ///////  used to check user dataset
        /////// </summary> 
        ////private bool userValidDataSet;

        /// <summary>
        ///  Used To keep Track Of user dataGridview
        /// </summary>
        private int userGridRowIndex;

        /// <summary>
        /// Used to Check Valid Email
        /// </summary>
        ///  private string validEmail = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
        private string validEmail = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

        /// <summary>
        /// this variable used to set postion userdatagrid view 
        /// </summary>
        private int tempRowId;

        /// <summary>
        /// used for keep track of new 
        /// </summary>
        private int userRecord;

        /// <summary>
        /// used for keep track of new 
        /// </summary>
        private int userUpdateRecord;

        /// <summary>
        /// Used To Store USer total
        /// </summary>
        private int userGroupRowCount;

        /// <summary>
        /// user grid Empty Row
        /// </summary>
        private bool userGridEmptyRow;

        /// <summary>
        /// to Keep Track empty row cliick grouplist grid
        /// </summary>
        private int userGroupListingRow;

        /// <summary>
        /// to Keep Track empty row cliick grouplist grid
        /// </summary>
        private int userInGridRow;

        /// <summary>
        /// to Keep Track empty row cliick grouplist grid
        /// </summary>
        private int userNotInGridRow;

        /// <summary>
        /// to Keep Track Prevous row
        /// </summary>
        private int tempGroupRowId;

        /// <summary>
        /// To Keep the Column Clicked
        /// </summary>
        private int userClickColumnId;

        /// <summary>
        /// used to keep trakc of user
        /// </summary>
        private BindingSource userSource = new BindingSource();

        /// <summary>
        /// to find a user
        /// </summary>
        private string findUserID;

        #endregion

        #region Variable For Group Tab

        /// <summary>
        /// Used to maitain row id for selected Row
        /// </summary>
        private int selectedGroupRow;

        /// <summary>
        /// Used to indicate Group Grid Clicked or not
        /// </summary>
        private bool groupGridClick;

        /// <summary>
        /// Used to indicate Group Loaded
        /// </summary>
        private bool groupLoaded;

        /// <summary>
        /// BusinessEntities.UserManagement  userManagement = new BusinessEntities.UserManagement(); 
        /// </summary>
        private UserManagementData groupManagement = new UserManagementData();

        /// <summary>
        /// Used For Swap users
        /// </summary>
        private DataSet swampInDataSet = new DataSet();

        /// <summary>
        /// Used For Swap users
        /// </summary>
        private DataSet swampOutDataSet = new DataSet();

        /// <summary>
        /// Used For Swap users
        /// </summary>
        private DataTable swampInDataTable = new DataTable();

        /// <summary>
        /// Used For Swap users
        /// </summary>
        private DataTable swampOutDataTable = new DataTable();

        /// <summary>
        /// Used For Temp Store users / Group Details
        /// </summary>
        private DataSet userIDDataSet = new DataSet();

        /// <summary>
        /// Used to store groupID
        /// </summary>
        private int groupID;

        /// <summary>
        /// userInRowID
        /// </summary>
        private int userInRowID;

        /// <summary>
        /// To Track GroupDataSet RowID
        /// </summary>
        private int userOutRowID;

        /////// <summary>
        /////// Used to check DataSet for Group
        /////// </summary>
        ////private bool groupValidDataSet;

        /// <summary>
        ///  checks changes
        /// </summary>
        private bool changeInGroupDataSet;

        /// <summary>
        /// Keeks Track of Row For GroupsListDataGridview;
        /// </summary>
        private int groupsListRowID;

        /// <summary>
        ///  used to keep track of ButtonOpertion
        /// </summary>
        private int groupButtonOpertion;

        /// <summary>
        /// Used To check that user clicks validRow
        /// </summary>
        private bool groupValidRow;

        /// <summary>
        /// Used To check that user clicks validRow
        /// </summary>
        private int userInTotalCount;

        /// <summary>
        /// Used To check that user clicks validRow
        /// </summary>
        private int userOutTotalCount;

        /// <summary>
        ///  Used To Store userInCount
        /// </summary>
        private int userInCount;

        /// <summary>
        ///  Used To Store userNotInRowCount
        /// </summary>
        private int userNotInRowCount;

        /// <summary>
        ///  Used To Store userInRowCount
        /// </summary>
        private int userInRowCount;

        /// <summary>
        /// to sorted Userin datas
        /// </summary>
        private DataTable userInSortDataTable = new DataTable();

        /// <summary>
        /// to sorted Userin datas
        /// </summary>
        private DataTable userNotInSortDataTable = new DataTable();

        /// <summary>
        /// Usernot in grid sort order
        /// </summary>
        private string userNotInSortedOrder = "ASC";

        /// <summary>
        /// User in grid sort order
        /// </summary>
        private string userInSortedOrder = "ASC";

        /// <summary>
        /// used to store group grid coulmn index
        /// </summary>
        private int tempGroupColumId;

        #endregion

        #region Variable For Permissions Tab

        /// <summary>
        /// Used to Store permission Column Index
        /// </summary>
        private int permissionColumnIndex;

        /// <summary>
        /// Used to store permissions details
        /// </summary>
        private UserManagementData permissionManagement = new UserManagementData();

        /// <summary>
        /// Create an instance of CurrencyManager
        /// </summary>
        private CurrencyManager permissionsCm;

        /// <summary>
        /// Used to Store Permission Grid Row Index
        /// </summary>
        private int permissionGridRowIndex;

        /// <summary>
        /// Used to store permissions details
        /// </summary>
        private DataSet permissionSelected = new DataSet();

        /// <summary>
        /// Used to Check the 
        /// </summary>
        private bool permissionChanges;

        /// <summary>
        /// Used to groupsTotalRecordCount
        /// </summary>
        private int groupsTotalRecordCount;

        /// <summary>
        /// Set Style to DataGrid Header
        /// </summary>
        ////  private System.Windows.Forms.DataGridViewCellStyle commentHeader = new System.Windows.Forms.DataGridViewCellStyle();

        /// <summary>
        /// used to permissionPreviousRowId
        /// </summary>
        private int permissionPreviousRowId;

        /// <summary>
        /// to sorted Form Permission datas
        /// </summary>
        private DataTable permissionInSortDataTable = new DataTable();

        /// <summary>
        /// Used To Store Permission Count
        /// </summary>
        private int permissionCount;

        #endregion

        #region Variable For all Tab

        /// <summary>
        /// used to stroe expressions 
        /// </summary>
        private string stringExp = string.Empty;

        /// <summary>
        /// check to close the form
        /// </summary>
        private bool closingForm;

        /// <summary>
        /// terrscanUserid
        /// </summary>
        private int terrscanUserid;

        /// <summary>
        /// testUserid
        /// </summary>
        private int testUserid;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UserManagementForm"/> class.
        /// </summary>
        public F9002()
        {
            this.InitializeComponent();
            this.pictureBox3.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox3.Height, this.pictureBox3.Width, "Groups", 0, 51, 0);
            this.pictureBox2.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox2.Height, this.pictureBox2.Width, "User List", 28, 81, 128);

            #region Code For User Tab
            //// set the Lable to Selected Index

            this.userLabel.Text = this.UserManagementTab.SelectedTab.Text;

            this.UserManagementTab.SelectedIndex = 0;
            this.NewUser.Click += new EventHandler(this.UserNewButton_Click);
            this.SaveUser.Click -= new EventHandler(this.UserSaveButton_Click);
            this.SaveUser.Click += new EventHandler(this.UserSaveButton_Click);
            this.LoadUserList();
            //// Initialize User Control
            this.SetButton(ButtonOperation.Empty);
            ///// Intialise swap tables
            this.ConstructSwapTable();
            #endregion
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;        

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Enum For User Tab

        #region enumeratorButtonOperation

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
            /// Cancel = 5.
            /// </summary>
            GridOperation = 5,

            /// <summary>
            /// Update = 6.
            /// </summary>
            Update = 6,

            /// <summary>
            /// InValidRow = 7.
            /// </summary>
            InValidRow = 7,

            /// <summary>
            /// InValidRow = 8.
            /// </summary>
            FromOtherTab = 8
        }

        #endregion

        #endregion

        #region Enum For Permission Tab
        /// <summary>
        /// Button Functionality
        /// </summary>
        public enum PermissionButtonOperation
        {
            /// <summary>
            /// Empty = 0.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// Save = 2.
            /// </summary>
            Save = 1,

            /// <summary>
            /// Cancel = 3.
            /// </summary>
            Cancel = 3,
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F9002Controller Form9002Controll
        {
            get { return this.form9002Control as F9002Controller; }
            set { this.form9002Control = value; }
        }

        #endregion

        #region Event Subcription

        /// <summary>
        /// Sets the form cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetFormCancelButton, Thread = ThreadOption.UserInterface)]
        public void SetFormCancelButton(object sender, DataEventArgs<string> e)
        {
            this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
        }

        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form9002Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        #endregion

        #region StaticMethods

        #region IsDup
        /// <summary>
        /// Determines whether the specified source row is dup.
        /// </summary>
        /// <param name="sourceRow">The source row.</param>
        /// <param name="targetRow">The target row.</param>
        /// <param name="keyColumns">The key columns.</param>
        /// <returns>
        /// 	<c>true</c> if the specified source row is dup; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsDup(DataRow sourceRow, DataRow targetRow, DataColumn[] keyColumns)
        {
            bool retVal = true;
            foreach (DataColumn column in keyColumns)
            {
                retVal = retVal && sourceRow[column].Equals(targetRow[column]);
                if (!retVal)
                {
                    break;
                }
            }

            return retVal;
        }
        #endregion

        #region Finds the dups

        /// <summary>
        /// Finds the dups.
        /// </summary>
        /// <param name="tbl">The TBL.</param>
        /// <param name="sourceNdx">The source NDX.</param>
        /// <param name="keyColumns">The key columns.</param>
        /// <returns>returns duplicate rows </returns>
        private static DataRow[] FindDups(DataTable tbl, int sourceNdx, DataColumn[] keyColumns)
        {
            ArrayList retVal = new ArrayList();
            DataRow sourceRow = tbl.Rows[sourceNdx];
            for (int i = sourceNdx + 1; i < tbl.Rows.Count; i++)
            {
                DataRow targetRow = tbl.Rows[i];
                if (IsDup(sourceRow, targetRow, keyColumns))
                {
                    retVal.Add(targetRow);
                }
            }

            return (DataRow[])retVal.ToArray(typeof(DataRow));
        }

        #endregion

        #region Removes duplicate rows from given DataTable

        /// <summary>
        /// Removes duplicate rows from given DataTable
        /// </summary>
        /// <param name="tbl">The TBL.</param>
        /// <param name="keyColumns">The key columns.</param>
        private static void RemoveDuplicates(ref DataSet tbl, DataColumn[] keyColumns)
        {
            int rowNdx = 0;
            while (rowNdx < tbl.Tables[0].Rows.Count - 1)
            {
                DataRow[] dups = FindDups(tbl.Tables[0], rowNdx, keyColumns);
                if (dups.Length > 0)
                {
                    foreach (DataRow dup in dups)
                    {
                        tbl.Tables[0].Rows.Remove(dup);
                    }
                }
                else
                {
                    rowNdx++;
                }
            }
        }

        #endregion

        #region RemoveDuplicateUserID

        /// <summary>
        /// Removes the duplicate user ID.
        /// </summary>
        /// <param name="outUserDetails">The out user details.</param>
        /// <param name="userInDetails">The user in details.</param>
        /// <returns>Unique Row DataSet </returns>
        private static DataSet RemoveDuplicateUserID(DataSet outUserDetails, DataSet userInDetails)
        {
            DataSet ds = new DataSet();
            int dataRowCount = 0;
            StringBuilder uniqueExpression = new StringBuilder();
            DataRow[] userUniqueRows;
            DataRowCollection dataRowCollection = userInDetails.Tables[0].Rows;
            foreach (DataRow userRow in dataRowCollection)
            {
                dataRowCount++;
                if (dataRowCount == dataRowCollection.Count)
                {
                    if (!string.IsNullOrEmpty(userRow["UserID"].ToString()))
                    {
                        uniqueExpression = uniqueExpression.Append("UserID <> " + userRow["UserID"].ToString());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(userRow["UserID"].ToString()))
                    {
                        uniqueExpression = uniqueExpression.Append("UserID <> " + userRow["UserID"].ToString() + " And ");
                    }
                }
            }

            if (uniqueExpression.ToString().LastIndexOf("And") >= 0)
            {
                string tempExpression;
                tempExpression = uniqueExpression.ToString().Substring(0, (uniqueExpression.ToString().LastIndexOf("And")));
                userUniqueRows = outUserDetails.Tables[0].Select(tempExpression);
                ds.Merge(userUniqueRows);
            }
            else if (!string.IsNullOrEmpty(uniqueExpression.ToString()))
            {
                userUniqueRows = outUserDetails.Tables[0].Select(uniqueExpression.ToString());
                ds.Merge(userUniqueRows);
            }

            return ds;
        }

        #endregion

        #region CheckValidDataSet
        /// <summary>
        /// Determines whether  DataTable is Exist or not in  [the specified CMNT data set].
        /// </summary>
        /// <param name="userDataSet">The user data set.</param>
        /// <returns>retrun true if valid dataset else false </returns>
        /// 
        private static bool CheckValidDataSet(DataSet userDataSet)
        {
            bool validUserDataSet;
            if (userDataSet.Tables.Count > 0 && userDataSet != null)
            {
                validUserDataSet = true;
            }
            else
            {
                validUserDataSet = false;
            }

            return validUserDataSet;
        }
        #endregion

        #region CreateEmptyRows

        /// <summary>
        /// Creates the empty rows.
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="maxRowCount">The max row count.</param>
        /// <returns>DataTable</returns>
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
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Int64")
                        {
                            tempRow[j] = DBNull.Value;
                        }
                        else if (sourceDataTable.Columns[j].DataType.ToString() == "System.Boolean")
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

        #endregion

        #region CheckValidDomainName
        /// <summary>
        /// Checks the name of the valid domain.
        /// </summary>
        /// <param name="netName">Name of the net.</param>
        /// <returns> True when valid domain name else False</returns>
        private static bool CheckValidDomainName(string netName)
        {
            int startPosition = 0;
            int endPosition = 0;
            startPosition = netName.IndexOf("\\");
            endPosition = netName.LastIndexOf("\\");
            if (startPosition > 0 && endPosition > 0 && startPosition == endPosition && startPosition != netName.Length - 1)
            {
                return false;
            }
            else if (startPosition == netName.Length - 1)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region SetSwapDataGridRowPosition
        /// <summary>
        /// Sets the swap data grid row position.
        /// </summary>
        /// <param name="rowtobeSet">The rowtobe set.</param>
        /// <returns> int postion to set</returns>
        private static int SetSwapDataGridRowPosition(DataSet rowtobeSet)
        {
            int rowsCount = 0;
            DataRowCollection tempPositionRow;
            if (rowtobeSet.Tables.Count > 0)
            {
                tempPositionRow = rowtobeSet.Tables[0].Rows;
                foreach (DataRow tempRowPos in tempPositionRow)
                {
                    if (!string.IsNullOrEmpty(tempRowPos["UserID"].ToString()))
                    {
                        rowsCount++;
                    }
                }
            }

            return rowsCount++;
        }
        #endregion

        #region CheckEmptyRecords

        /// <summary>
        /// Checks the empty records.
        /// </summary>
        /// <param name="emptyRow">The empty row.</param>
        /// <returns>true if empty records else false</returns>
        private static bool CheckEmptyRecords(DataSet emptyRow)
        {
            DataRowCollection emptyRowDataTable = emptyRow.Tables[0].Rows;

            bool validRow = false;
            foreach (DataRow userRow in emptyRowDataTable)
            {
                if (!string.IsNullOrEmpty(userRow["UserID"].ToString()))
                {
                    validRow = true;
                    break;
                }
                else
                {
                    validRow = false;
                }
            }

            return validRow;
        }
        #endregion

        #region ResetBrowseControls

        /// <summary>
        /// Resets the browse controls.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <param name="resetButton">The reset button.</param>
        private static void ResetBrowseControls(int rowCount, Control resetButton)
        {
            PictureBox tempPictBox = (PictureBox)resetButton;
            if (rowCount == 0)
            {
                tempPictBox.Enabled = false;
            }
            else
            {
                tempPictBox.Enabled = true;
            }
        }
        #endregion

        #region SetTextBox

        /// <summary>
        /// Sets the focusc to the  comment textbox.
        /// </summary>
        /// <param name="tempTextBox">The temp text box.</param>
        private static void SetTextBoxFocus(TextBox tempTextBox)
        {
            tempTextBox.Focus();
            tempTextBox.SelectAll();
        }
        #endregion

        #region Group Tab Static Methods

        /// <summary>
        /// Sets the data grid Cell position.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="commentRow">The comment row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        private void SetDataGridViewCellPosition(Control controlName, int commentRow, int columnIndex)
        {
            DataGridView tempDataGridview = (DataGridView)controlName;
            try
            {
                if (columnIndex == -1)
                {
                    //// tempDataGridview.Rows[Convert.ToInt32(commentRow)].Selected = true;
                    tempDataGridview.CurrentCell = tempDataGridview[0, commentRow];
                }
                else
                {
                    tempDataGridview.Rows[commentRow].Selected = false;
                    tempDataGridview.CurrentCell = tempDataGridview[columnIndex, commentRow];
                }
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the data grid view position.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="commentRow">The comment row.</param>
        private void SetDataGridViewPosition(Control controlName, int commentRow)
        {
            DataGridView tempDataGridview = (DataGridView)controlName;
            try
            {
                if (tempDataGridview.Rows.Count > 0 && commentRow >= 0)
                {
                    tempDataGridview.Rows[commentRow].Selected = true;
                    tempDataGridview.CurrentCell = tempDataGridview[0, commentRow];
                }
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Group Tab Static Methods

        #endregion

        #region LoadWorkSpace

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            try
            {
                if (this.form9002Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.FormHeaderWorkSpace.Show(this.form9002Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.FormHeaderWorkSpace.Show(this.form9002Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                this.formLabelInfo[0] = SharedFunctions.GetResourceString("9002UserManagement");
                this.formLabelInfo[1] = string.Empty;

                this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        /// <summary>
        /// Handles the SelectedIndexChanged event of the userManagementTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserManagementTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            //// Codeing for User Tab
            if (this.UserManagementTab.SelectedIndex == 0)
            {
                #region USer
                if (this.changeInGroupDataSet)
                {
                    ///// If Changes in USer Tab Then
                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            {
                                this.SaveGroupDatas();
                                if (!this.closingForm)
                                {
                                    //// it is not saved assing tab privious id
                                    this.UserManagementTab.SelectedIndex = 1;
                                }

                                this.UserTabInit();
                                break;
                            }

                        case DialogResult.No:
                            {
                                this.changeInGroupDataSet = false;
                                this.UserTabInit();
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                this.UserManagementTab.SelectedIndex = 1;
                                break;
                            }
                    }
                }
                else if (this.permissionChanges)
                {
                    ///// If Changes in USer Tab Then
                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName ,"?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            {
                                this.SavePermissionData();
                                if (!this.closingForm)
                                {
                                    //// it is not saved assing tab privious id
                                    this.UserManagementTab.SelectedIndex = 2;
                                }

                                this.UserTabInit();
                                this.buttonOperation = (int)ButtonOperation.FromOtherTab;
                                break;
                            }

                        case DialogResult.No:
                            {
                                this.permissionChanges = false;
                                this.UserTabInit();
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                this.UserManagementTab.SelectedIndex = 2;
                                break;
                            }
                    }
                }

                // else if (this.groupButtonOpertion == (int)ButtonOperation.New)
                // {
                //    //// If buttonOperation is new Then reset To userTab
                //    if (MessageBox.Show(SharedFunctions.GetResourceString("DiscardUser"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        this.permissionChanges = false;
                //        this.UserTabInit();
                //        this.groupButtonOpertion = (int)ButtonOperation.Empty;
                //    }
                //    else
                //    {
                //        this.UserManagementTab.SelectedIndex = 1;
                //    }
                // }
                else if (this.keyPressed != true)
                {
                    this.UserTabInit();
                }

                #endregion
            }
            //// Codeing for Group Tab
            else if (this.UserManagementTab.SelectedIndex == 1)
            {
                #region  Group
                if (this.keyPressed)
                {
                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            {
                                this.SaveUserData();
                                if (!this.closingForm)
                                {
                                    //// it is not saved assing tab privious id
                                    this.UserManagementTab.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.keyPressed = false;
                                    this.GroupTabInit();
                                }

                                this.buttonOperation = (int)ButtonOperation.FromOtherTab;
                                break;
                            }

                        case DialogResult.No:
                            {
                                if (this.buttonOperation == (int)ButtonOperation.New)
                                {
                                    this.buttonOperation = (int)ButtonOperation.Empty;
                                    this.SetButton(ButtonOperation.Cancel);
                                }

                                this.buttonOperation = (int)ButtonOperation.FromOtherTab;
                                this.keyPressed = false;
                                this.UserTabInit();
                                this.GroupTabInit();
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                this.UserManagementTab.SelectedIndex = 0;
                                break;
                            }
                    }
                }
                else if (this.permissionChanges)
                {
                    ///// If Changes in permission Tab Then
                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName ,"?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            {
                                this.SavePermissionData();
                                if (!this.closingForm)
                                {
                                    //// it is not saved assing tab privious id
                                    this.UserManagementTab.SelectedIndex = 2;
                                }

                                this.GroupTabInit();
                                break;
                            }

                        case DialogResult.No:
                            {
                                this.permissionChanges = false;
                                this.GroupTabInit();
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                this.UserManagementTab.SelectedIndex = 2;
                                break;
                            }
                    }
                }
                else //// Default Load Data
                {
                    this.GroupTabInit();
                    this.buttonOperation = (int)ButtonOperation.FromOtherTab;
                }

                #endregion Group
            }

                //// Codeing for Permissions Tab
            else if (this.UserManagementTab.SelectedIndex == 2)
            {
                #region USer
                if (this.keyPressed)
                {
                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            {
                                this.SaveUserData();
                                if (!this.closingForm)
                                {
                                    //// it is not saved assing tab privious id
                                    this.UserManagementTab.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.keyPressed = false;
                                }

                                break;
                            }

                        case DialogResult.No:
                            {
                                if (this.buttonOperation == (int)ButtonOperation.New)
                                {
                                    this.buttonOperation = (int)ButtonOperation.Empty;
                                    this.SetButton(ButtonOperation.Cancel);
                                }

                                this.buttonOperation = (int)ButtonOperation.FromOtherTab;
                                this.keyPressed = false;
                                this.PermissionTabInit();
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                this.UserManagementTab.SelectedIndex = 0;
                                break;
                            }
                    }
                }

                #endregion

                #region Group
                else if (this.changeInGroupDataSet)
                {
                    switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            {
                                this.SaveGroupDatas();
                                if (!this.closingForm)
                                {
                                    //// it is not saved assing tab privious id
                                    this.UserManagementTab.SelectedIndex = 1;
                                }

                                this.PermissionTabInit();
                                break;
                            }

                        case DialogResult.No:
                            {
                                this.changeInGroupDataSet = false;
                                this.PermissionTabInit();
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                this.UserManagementTab.SelectedIndex = 1;
                                break;
                            }
                    }
                }

                #endregion

                #region Permission
                else if (!this.permissionChanges)
                {
                    this.PermissionDataset.EnforceConstraints = false; 
                    this.PermissionTabInit();
                    this.buttonOperation = (int)ButtonOperation.FromOtherTab;
                }
                #endregion
            }
        }

        #region  Coding For User

        #region  UserTabEvents
        /// <summary>
        /// Handles the CellContentClick event of the UserGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ////Avoid RowHeader and Column Header 
            if (e.RowIndex >= 0)
            {
                //// Only Row With USerID Shoud Execute Below
                if (!string.IsNullOrEmpty(this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString()))
                {
                    this.userGridRowIndex = e.RowIndex;
                    if (!this.keyPressed)
                    {
                        this.tempRowId = this.userGridRowIndex;
                        this.userUpdateRecord = this.tempRowId;
                        this.SetButton(ButtonOperation.Empty);
                    }

                    if (this.keyPressed)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("DiscardUser"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.userManagement.RejectChanges();
                            this.keyPressed = false;
                            this.tempRowId = e.RowIndex;
                            this.SetButton(ButtonOperation.Empty);
                            if (this.buttonOperation != (int)ButtonOperation.New)
                            {
                                if (this.UserGridView.Rows.Count >= 0 && e.RowIndex >= 0)
                                {
                                    ////create empty rows
                                    this.userManagement.Tables[0].Merge(CreateEmptyRows(this.userManagement.Tables[0], 16));

                                    this.LoadGroupDataGridView(this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString(), "Group");
                                }
                            }

                            this.GroupDataGrid.Enabled = true;
                        }
                        else
                        {
                            this.SetDataGridViewPosition(this.UserGridView, this.tempRowId);
                            this.SetUserFormTextBoxes(this.tempRowId);
                            this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                        }
                    }
                    else
                    {
                        this.EnableControl();
                        this.GroupDataGrid.Enabled = true;
                        this.SetUserFormTextBoxes(this.tempRowId);
                    }

                    if (this.tempRowId >= 0)
                    {
                        this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                        if (this.userGroupRowCount > 0)
                        {
                            this.GroupDataGrid.Enabled = true;
                        }
                        else
                        {
                            this.GroupDataGrid.Enabled = false;
                        }

                        SetTextBoxFocus(this.UserNameTextBox);
                    }
                }
                else
                {
                    this.LoadGroupDataGridView(this.emptyKeyID, "Group");
                    this.SetButton(ButtonOperation.InValidRow);
                    this.DisableControl();
                    this.GroupDataGrid.Enabled = false;
                    this.SetUserFormTextBoxes(e.RowIndex);
                }
            }
        }

        /// <summary>
        /// Disables the group data grid.
        /// </summary>
        private void DisableGroupDataGrid()
        {
            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ID", System.Type.GetType("System.Int64")), new DataColumn("GroupName", System.Type.GetType("System.String")) });
            tempDataTable = CreateEmptyRows(tempDataTable, 5);
            this.GroupDataGrid.DataSource = null;
            this.GroupDataGrid.DataSource = tempDataTable;
            this.GroupDataGrid.Rows[0].Selected = false;
            this.GroupDataGrid.CurrentCell = this.UserGridView[0, 0];
            this.GroupDataGrid.Enabled = false;
        }

        /// <summary>
        /// Handles the Click event of the UserNewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserNewButton_Click(object sender, EventArgs e)
        {
            if (this.UserNewButton.Enabled)
            {
                //// Added By Guhan  - on 23 jan 2007
                this.PermissionAuditLink.Text = this.userAuditLink;
                this.PermissionAuditLink.Enabled = false;
                //// Till Here
                this.userGridRowIndex = 0;
                this.buttonOperation = (int)ButtonOperation.New;
                this.LoadUserList();
                this.ClearUserTabControl();
                this.SetButton(ButtonOperation.New);
                this.EnableControl();
                ///// this.GroupControlClearDataBinding();
                this.tempRowId = 0;
                this.UserNameTextBox.Focus();
                this.keyPressed = true;
                ////if (this.userValidDataSet)
                ////{
                ////    if (this.userRecord > 0)
                ////    {
                ////        this.UserGridView.Rows[0].Selected = false;
                ////        this.UserGridView.CurrentCell = this.UserGridView[0, 0];
                ////    }
                ////}

                this.InitGroupsInUser();
                this.DisableGroupDataGrid();
            }
        }

        /// <summary>
        /// Checks the valid email ID.
        /// </summary>
        /// <param name="sourceEmailId">The source email id.</param>
        /// <returns>true if valid email id else false</returns>
        private bool CheckValidEmailID(string sourceEmailId)
        {
            if (!String.IsNullOrEmpty(sourceEmailId))
            {
                if (Regex.IsMatch(sourceEmailId, this.validEmail))
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
        /// Handles the Click event of the UserSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserSaveButton_Click(object sender, EventArgs e)
        {
            if (this.UserSaveButton.Enabled)
            {
                this.SaveUserData();
            }
        }

        /// <summary>
        /// Saves the user data.
        /// </summary>
        private void SaveUserData()
        {
            this.Cursor = Cursors.WaitCursor;
            //// UserManagementData tempUser = new UserManagementData();

            //// Checks Required Fields are Filled
            if (this.CheckRequiredField())
            {
                if (!CheckValidDomainName(this.NetNameTextBox.Text.Trim()))
                {
                    if (this.CheckValidEmailID(this.EmailTextBox.Text.Trim()))
                    {
                        try
                        {
                            this.userManagement.GetErrorMessage.Clear();
                            ////  into user Table 
                            if (string.IsNullOrEmpty(this.UserIDTextBox.Text))
                            {
                                this.userManagement.GetErrorMessage.Merge(F9002WorkItem.SaveUserDetails(0, this.DisplayNameTextBox.Text.Trim(), this.UserNameTextBox.Text.Trim(), this.NetNameTextBox.Text.Trim(), this.EmailTextBox.Text.Trim(), this.ActiveList.SelectedIndex, this.AdminList.SelectedIndex,this.AppraiserComboBox.SelectedIndex, TerraScanCommon.ApplicationId,TerraScanCommon.UserId));
                            }
                            else
                            {
                                ////Update The The User Details 
                                this.userManagement.GetErrorMessage.Merge(F9002WorkItem.SaveUserDetails(Convert.ToInt32(this.UserIDTextBox.Text.Trim()), this.DisplayNameTextBox.Text.Trim(), this.UserNameTextBox.Text.Trim(), this.NetNameTextBox.Text.Trim(), this.EmailTextBox.Text.Trim(), this.ActiveList.SelectedIndex, this.AdminList.SelectedIndex,this.AppraiserComboBox.SelectedIndex, TerraScanCommon.ApplicationId, TerraScanCommon.UserId));
                            }

                            if (this.userManagement.Tables.Count > 0)
                            {
                                if (this.userManagement.GetErrorMessage.Rows[0][this.userManagement.GetErrorMessage.ErrorMsgColumn].ToString() == "1")
                                {
                                    this.closingForm = false;
                                   //// MessageBox.Show(SharedFunctions.GetResourceString("NetNameExist"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ////this.NetNameTextBox.Focus();
                                    ////Modified BY Ramya.D
                                    MessageBox.Show(SharedFunctions.GetResourceString("UserNameExist"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.UserNameTextBox.Focus();
                                }
                                else
                                {
                                    this.EnableUserGridSorting();

                                    //// Used To Load the user DataGrid view
                                    this.LoadUserGroupDetails();

                                    ////Assing To find close the form in closing event
                                    this.closingForm = true;

                                    //// Enable the Buttons to Normal
                                    this.SetButton(ButtonOperation.Save);

                                    //// set UserGrid To Enable
                                    this.UserGridView.Enabled = true;

                                    //// ResetTheFlag
                                    this.keyPressed = false;

                                    // this.EnableUserGridSort();

                                    if (this.buttonOperation == (int)ButtonOperation.New && this.userManagement.ListUserDetail.Rows[0][this.userManagement.ListUserDetail.UserIDColumn].ToString() != "1")
                                    {
                                        this.UserGridView.Focus();
                                        this.userUpdateRecord = 0;
                                        this.SetUserFormTextBoxes(this.userUpdateRecord);
                                        this.findUserID = this.UserIDTextBox.Text.Trim();
                                    }
                                    else
                                    {
                                        int itemFound = this.userSource.Find("UserID", this.findUserID);
                                        this.userUpdateRecord = itemFound;

                                        this.SetUserFormTextBoxes(this.userUpdateRecord);
                                        this.LoadGroupDataGridView(this.UserGridView.Rows[this.userUpdateRecord].Cells["UserID"].Value.ToString(), "Group");
                                        TerraScanCommon.SetDataGridViewPosition(this.UserGridView, this.userUpdateRecord);
                                        ////this.UserGridView.CurrentCell = this.UserGridView[this.userClickColumnId , Convert.ToInt32(this.userUpdateRecord)]; 
                                        this.UserGridView.Focus();
                                    }

                                    this.buttonOperation = (int)ButtonOperation.Empty;
                                }
                            }
                        }
                        catch (Exception arException)
                        {
                            ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                        ////catch (Exception)
                        ////{
                        ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                        ////}
                    } //// Checks valid EmailID
                    else
                    {
                        this.closingForm = false;
                        MessageBox.Show(SharedFunctions.GetResourceString("EmailValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.EmailTextBox.Focus();
                    }
                }  //// Checks Valid Net Address 
                else
                {
                    this.closingForm = false;
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidNetName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.NetNameTextBox.Focus();
                }
            } //// Required Field
            else
            {
                this.closingForm = false;
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the Click event of the UserDeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserDeleteButton_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteUser"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               
                    try
                    {
                        if (this.UserIDTextBox.Text.Trim().Length > 0)
                        {
                            this.Cursor = Cursors.WaitCursor;

                            // delete the user details
                            F9002WorkItem.DeleteUserDetails(Convert.ToInt32(this.UserIDTextBox.Text), TerraScanCommon.UserId);
                            this.buttonOperation = (int)ButtonOperation.Delete;
                            ////Used To Load the user DataGrid view
                            this.LoadUserGroupDetails();
                            this.UserDeleteButton.Enabled = false;
                            this.buttonOperation = (int)ButtonOperation.Empty;
                            this.keyPressed = false;

                            // this.EnableUserGridSort();

                            if (this.userRecord > 0)
                            {
                                /*if (this.userGridRowIndex < this.userRecord)
                                {
                                   this.SetDataGridViewPosition(this.UserGridView, this.userGridRowIndex);
                                    this.LoadGroupDataGridView(this.UserGridView.Rows[this.userGridRowIndex].Cells["UserID"].Value.ToString(), "Group");
                                    this.SetUserFormTextBoxes(this.userGridRowIndex);
                                    this.tempRowId = this.userGridRowIndex;
                                }
                                else if (this.userGridRowIndex == this.userRecord)
                                {
                                   this.SetDataGridViewPosition(this.UserGridView, this.userRecord - 1);
                                    this.LoadGroupDataGridView(this.UserGridView.Rows[this.userRecord - 1].Cells["UserID"].Value.ToString(), "Group");
                                    this.SetUserFormTextBoxes(this.userRecord - 1);
                                    this.tempRowId = this.userRecord - 1;
                                }
                                else
                                {
                                   this.SetDataGridViewPosition(this.UserGridView, this.userRecord - 1);
                                    this.LoadGroupDataGridView(this.UserGridView.Rows[this.userRecord - 1].Cells["UserID"].Value.ToString(), "Group");
                                    this.SetUserFormTextBoxes(this.userRecord - 1);
                                    this.tempRowId = this.userRecord - 1;
                                }*/
                                this.SetDataGridViewPosition(this.UserGridView, 0);
                                this.LoadGroupDataGridView(this.UserGridView.Rows[0].Cells["UserID"].Value.ToString(), "Group");
                                this.SetUserFormTextBoxes(0);
                                this.tempRowId = 0;
                            }
                            else
                            {
                                //// Set the Control to disable
                                this.ClearUserTabControl();
                            this.DisableControl();

                                this.InitGroupsInUser();
                                DataTable tempDataTable = new DataTable();
                                ////tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("GroupID"), new DataColumn("GroupName") });
                                tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ID", System.Type.GetType("System.Int64")), new DataColumn("GroupName", System.Type.GetType("System.String")) });
                                this.GroupDataGrid.DataSource = tempDataTable;
                                this.GroupDataGrid.Enabled = false;
                            }

                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("UnableDelete"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception arException)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                    ////catch (Exception ex)
                    ////{
                    ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                    ////}
               
            }
            else
            {
                this.UserNameTextBox.Focus();
            }
        
            }
        //}
        /// <summary>
        /// Inits the groups in user.
        /// </summary>
        private void InitGroupsInUser()
        {
            DataTable tempDataTable = new DataTable();
            ////tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("GroupID"), new DataColumn("GroupName") });
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ID", System.Type.GetType("System.Int64")), new DataColumn("GroupName", System.Type.GetType("System.String")) });
            this.GroupDataGrid.DataSource = tempDataTable;
        }

        /// <summary>
        /// Handles the KeyPress event of the EmailTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.Update && this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab && this.formLoaded && !this.UserGridView.IsSorted && !this.userGridClick)
            {
                if (this.keyPressed)
                {
                    this.keyPressed = true;
                    this.SetButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableUserGridSorting();
                }
                else
                {
                    this.keyPressed = true;
                    this.SetButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableUserGridSorting();
                }
            }

            this.UserGridView.IsSorted = false;
        }

        /// <summary>
        /// Handles the Click event of the UserCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserCancelButton_Click(object sender, EventArgs e)
        {
            this.LoadUserGroupDetails();

            //// Set KeyPressed to false
            this.keyPressed = false;

            // this.EnableUserGridSort();
            this.GroupDataGrid.Enabled = true;
            //// Set Button
            this.SetButton(ButtonOperation.Cancel);

            ////Set TextBox To Previous Value
            this.LoadGroupDataGridView(this.UserIDTextBox.Text, "User");

            this.userGridRowIndex = this.tempRowId;
            if (this.buttonOperation == (int)ButtonOperation.New)
            {
                if (this.tempRowId >= 0)
                {
                    this.SetDataGridViewPosition(this.UserGridView, 0);
                    this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                }
            }
            else
            {
                if (this.tempRowId >= 0)
                {
                    this.SetDataGridViewPosition(this.UserGridView, this.tempRowId);
                    this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                }
            }

            this.tempRowId = this.userGridRowIndex;
            this.buttonOperation = (int)ButtonOperation.Empty;
            this.UserGridView.Focus();
            this.EnableUserGridSorting();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the user form text boxes.
        /// </summary>
        /// <param name="userRowId">The user row id.</param>
        private void SetUserFormTextBoxes(int userRowId)
        {
            //// Added By Guhan - on Jan 23
            this.PermissionAuditLink.Enabled = true;
            this.UserIDTextBox.Text = this.UserGridView.Rows[userRowId].Cells["UserID"].Value.ToString();
            this.PermissionAuditLink.Text = this.userAuditLink + this.UserIDTextBox.Text;
            //// Till here
            this.UserNameTextBox.Text = this.UserGridView.Rows[userRowId].Cells["FullName"].Value.ToString();
            if (String.Compare(this.UserGridView.Rows[userRowId].Cells["IsActive"].Value.ToString().ToLower(), "yes") == 0)
            {
                this.ActiveList.SelectedIndex = 1;
            }
            else
            {
                this.ActiveList.SelectedIndex = 0;
            }

            this.DisplayNameTextBox.Text = this.UserGridView.Rows[userRowId].Cells["DisplayName"].Value.ToString();
            if (String.Compare(this.UserGridView.Rows[userRowId].Cells["IsAdministrator"].Value.ToString().ToLower(), "yes") == 0)
            {
                this.AdminList.SelectedIndex = 1;
            }
            else
            {
                this.AdminList.SelectedIndex = 0;
            }

            if (String.Compare(this.UserGridView.Rows[userRowId].Cells["Appraiser"].Value.ToString().ToLower(), "yes") == 0)
            {
                this.AppraiserComboBox.SelectedIndex = 1;
            }
            else
            {
                this.AppraiserComboBox.SelectedIndex = 0;
            }

            this.NetNameTextBox.Text = this.UserGridView.Rows[userRowId].Cells["NetName"].Value.ToString();
            this.EmailTextBox.Text = this.UserGridView.Rows[userRowId].Cells["Email"].Value.ToString();


            this.terrscanUserid = TerraScanCommon.UserId;
            if(!string.IsNullOrEmpty(this.UserIDTextBox.Text))
            {
                this.testUserid = Convert.ToInt32(this.UserIDTextBox.Text);
            }
            else
            {
                this.testUserid=0;
            }

            if (this.terrscanUserid == this.testUserid)
            {
                UserDeleteButton.Enabled = false;

            }
            else
            {
                UserDeleteButton.Enabled = true;
            }

            this.keyPressed = false;
            /*columns["UserID"].DisplayIndex = 0;
            //columns["FullName"].DisplayIndex = 1;
            //columns["IsAdministrator"].DisplayIndex = 2;
            //columns["IsActive"].DisplayIndex = 3;
            //columns["DisplayName"].DataPropertyName = 4;
            //columns["NetName"].DataPropertyName = 5;
            //columns["Email"].DataPropertyName = 6;*/
        }

        /// <summary>
        /// Users the init.
        /// </summary>
        private void UserTabInit()
        {
            //// TODO : Coding For User
            this.userLabel.Text = this.UserManagementTab.SelectedTab.Text;
            this.ParentForm.CancelButton = this.UserCancelButton;
            if (this.buttonOperation != (int)ButtonOperation.New && this.keyPressed != true)
            {
                // Initialize User Control
                this.SetButton(ButtonOperation.Empty);
                //// used assing Property To Both grid Same 

                // used to Load the User DataGridview
                this.LoadUserGroupDetails();

                this.SetUserButtonEvent();

                this.DisableUserInSorting();

                /* Clear the DataBinding
   //this.UserControlClearDataBinding();

   //// Set the DataBinding
   //this.UserControlSetDataBinding();*/
            }

            if (this.buttonOperation == (int)ButtonOperation.FromOtherTab && this.UserManagementTab.SelectedIndex == 0)
            {
                this.buttonOperation = (int)ButtonOperation.Empty;
            }
        }

        /// <summary>
        /// Loads the user group details.
        /// </summary> 
        private void LoadUserGroupDetails()
        {
            this.Cursor = Cursors.WaitCursor;
            this.CustomiseUserManagementGrid();
            this.CustomiseGroupManagementGrid();
            try
            {
                this.userManagement = this.form9002Control.WorkItem.GetUserGroupDetails; ///// F9002WorkItem.GetUserGroupDetails;
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            ////catch (Exception ex)
            ////{
            ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            ////}
            //// this.userValidDataSet = this.CheckValidRowCount(this.userManagement);

            if (this.userManagement != null)
            {

                
                this.userRecord = this.userManagement.ListUserDetail.Rows.Count;
                this.UserGridView.DataSource = null;

                this.UserGridView.DataSource = this.userManagement.ListUserDetail;
                this.userSource.DataSource = this.userManagement.ListUserDetail.DefaultView;
                this.EnableUserGridSorting();
                if (this.userRecord > 0)
                {
                    this.LoadGroupDataGridView(this.userManagement.ListUserDetail.Rows[0][this.userManagement.ListUserDetail.UserIDColumn].ToString(), "Group");
                    this.EnableControl();
                    this.SetUserFormTextBoxes(0);
                    this.UserGridView.CurrentCell = this.UserGridView[0, 0];
                    this.UserGridView.Rows[0].Selected = true;
                    if (this.userRecord > UserGridView.NumRowsVisible)
                    {
                        this.UserGridVerticalScrollBar.Enabled = true;
                        this.UserGridVerticalScrollBar.Visible = false;
                    }
                    else
                    {
                        this.UserGridVerticalScrollBar.BringToFront();
                        this.UserGridVerticalScrollBar.Enabled = false;
                        this.UserGridVerticalScrollBar.Visible = true;
                    }

                    this.UserGridView.Rows[0].Selected = true;
                    ///// code added by thilak for avoid save alert while panel navigation, when no change had made
                    this.keyPressed = false;
                }
                else
                {
                }
                //// initally select full row
                ////   this.UserGridView.Rows[0].Selected = true; 
            }
            else
            {
                this.DisableControl();

                /*DataTable tempDataTable = new DataTable();
                //tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("GroupID"), new DataColumn("GroupName") });
                //this.GroupDataGrid.DataSource = tempDataTable;
                //this.UserGropEmptyDataGridView.DataSource = TerraScanCommon.CreateEmptyRows(tempDataTable.Clone(), 5);
                //this.UserGropEmptyDataGridView.Enabled = false;*/
            }

            this.Cursor = Cursors.Default;
        }
    
        /// <summary>
        /// Loads the user list.
        /// </summary>
        private void LoadUserList()
        {
            this.ActiveList.Items.Clear();
            this.AdminList.Items.Clear();
            this.AppraiserComboBox.Items.Clear();
            this.ActiveList.Items.Insert(0, "NO");
            this.ActiveList.Items.Insert(1, "YES");
            this.AdminList.Items.Insert(0, "NO");
            this.AdminList.Items.Insert(1, "YES");
            this.AppraiserComboBox.Items.Insert(0, "NO");
            this.AppraiserComboBox.Items.Insert(1, "YES");
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomiseUserManagementGrid()
        {
            this.UserGridView.AllowUserToResizeColumns = false;
            this.UserGridView.AutoGenerateColumns = false;
            this.UserGridView.AllowUserToResizeRows = false;
            this.UserGridView.StandardTab = true;
            DataGridViewColumnCollection columns = this.UserGridView.Columns;
            columns["UserID"].Resizable = DataGridViewTriState.False;
            columns["UserID"].DataPropertyName = "UserID";
            columns["FullName"].DataPropertyName = "FullName";
            columns["IsAdministrator"].DataPropertyName = "IsAdministrator";
            columns["IsActive"].DataPropertyName = "Active";
            columns["Appraiser"].DataPropertyName = "Appraiser";
            columns["DisplayName"].DataPropertyName = "DisplayName";
            columns["NetName"].DataPropertyName = "NetName";
            columns["Email"].DataPropertyName = "Email";
            columns["UserID"].DisplayIndex = 0;
            columns["FullName"].DisplayIndex = 1;
            columns["DisplayName"].DisplayIndex = 2;
            columns["IsAdministrator"].DisplayIndex = 3;
            columns["IsActive"].DisplayIndex = 4;
            columns["Appraiser"].DisplayIndex = 5;
            columns["NetName"].DisplayIndex = 6;
            columns["Email"].DisplayIndex = 7;
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomiseGroupManagementGrid()
        {
            this.GroupDataGrid.AllowUserToResizeColumns = false;
            this.GroupDataGrid.AutoGenerateColumns = false;
            this.GroupDataGrid.AllowUserToResizeRows = false;
            this.GroupDataGrid.StandardTab = true;
            this.GroupDataGrid.ScrollBars = ScrollBars.Vertical;

            DataGridViewColumnCollection columns = this.GroupDataGrid.Columns;
            columns["ID"].Resizable = DataGridViewTriState.False;
            columns["ID"].DataPropertyName = "ID";
            columns["GroupName"].DataPropertyName = "GroupName";
            columns["ID"].DisplayIndex = 0;
            columns["GroupName"].DisplayIndex = 1;
        }

        /// <summary>
        /// Sets the data binding.
        /// </summary>
        private void UserControlSetDataBinding()
        {
            this.UserIDTextBox.DataBindings.Add("Text", this.userManagement.Tables[0], "UserID");
            this.UserNameTextBox.DataBindings.Add("Text", this.userManagement.Tables[0], "FullName");
            this.EmailTextBox.DataBindings.Add("Text", this.userManagement.Tables[0], "Email");
            this.DisplayNameTextBox.DataBindings.Add("Text", this.userManagement.Tables[0], "DisplayName");
            this.NetNameTextBox.DataBindings.Add("Text", this.userManagement.Tables[0], "NetName");
            this.ActiveList.DataBindings.Add("Text", this.userManagement.Tables[0], "Active");
            this.AdminList.DataBindings.Add("Text", this.userManagement.Tables[0], "isAdministrator");
            this.AppraiserComboBox.DataBindings.Add("Text", this.userManagement.Tables[0], "Appraiser");
        }

        /// <summary>
        /// Clears the data binding.
        /// </summary> 
        private void UserControlClearDataBinding()
        {
            this.UserIDTextBox.DataBindings.Clear();
            this.UserNameTextBox.DataBindings.Clear();
            this.EmailTextBox.DataBindings.Clear();
            this.NetNameTextBox.DataBindings.Clear();
            this.DisplayNameTextBox.DataBindings.Clear();
            this.ActiveList.DataBindings.Clear();
            this.AdminList.DataBindings.Clear();
            this.AppraiserComboBox.DataBindings.Clear();
        }

        /// <summary>
        /// Loads the group data grid view.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="keyName">Name of the key.</param>
        private void LoadGroupDataGridView(string userID, string keyName)
        {
            this.stringExp = "UserID =" + userID;
            if (string.Compare(keyName, this.groupGrid) == 0)
            {
                DataTable tempDataTable = new DataTable();
                tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ID", System.Type.GetType("System.Int64")), new DataColumn("GroupName", System.Type.GetType("System.String")) });
                ////tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("GroupID" ,System), new DataColumn("GroupName") });
                DataRow[] emptyRow;
                DataRow tempRow;
                this.stringExp = "UserID =" + userID;
                emptyRow = this.userManagement.ListUserGroupDetail.Select(this.stringExp);
                this.userGroupRowCount = emptyRow.Length;
                foreach (DataRow userRow in emptyRow)
                {
                    tempRow = tempDataTable.NewRow();
                    tempRow["Id"] = userRow["GroupId"];
                    tempRow["GroupName"] = userRow["GroupName"];
                    tempDataTable.Rows.Add(tempRow);
                }

                //// tempDataTable = CreateEmptyRows(tempDataTable, 5);

                this.GroupDataGrid.DataSource = tempDataTable;
                if (this.userGroupRowCount > this.GroupDataGrid.NumRowsVisible)
                {
                    this.MapGridVerticalScrollBar.Enabled = true;
                    this.MapGridVerticalScrollBar.Visible = false;
                }
                else
                {
                    this.MapGridVerticalScrollBar.BringToFront();
                    this.MapGridVerticalScrollBar.Enabled = false;
                    this.MapGridVerticalScrollBar.Visible = true;
                }

                if (this.userGroupRowCount == 0)
                {
                    //// if no record Disable the groupDataGrid
                    this.GroupDataGrid.Enabled = false;

                    //// if no record remove the selecteion
                    this.GroupDataGrid.Rows[0].Selected = false;
                }
                else
                {
                    //// if record Enable the groupDataGrid
                    this.GroupDataGrid.Enabled = true;
                    //// Selects the first Record 
                    this.SetDataGridViewPosition(this.GroupDataGrid, 0);
                    //// and set the LinkColor
                    if (tempDataTable.Rows.Count > 0)
                    {
                        //// this.SetDataGridLinkForeColor(0);
                    }
                }
            }
            else if (string.Compare(keyName, this.userGrid) == 0)
            {
                DataView prodView = new DataView(this.userManagement.ListUserDetail);
                if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    prodView.RowFilter = this.stringExp;
                }
                else
                {
                    this.stringExp = "UserID =" + this.userManagement.ListUserDetail.Rows[0][this.userManagement.ListUserDetail.UserIDColumn];
                    prodView.RowFilter = this.stringExp;
                }

                this.UserNameTextBox.Text = prodView[0]["FullName"].ToString();
                this.EmailTextBox.Text = prodView[0]["Email"].ToString();
                this.DisplayNameTextBox.Text = prodView[0]["DisplayName"].ToString();
                this.NetNameTextBox.Text = prodView[0]["NetName"].ToString();
                this.ActiveList.Text = prodView[0]["Active"].ToString();
                this.AdminList.Text = prodView[0]["IsAdministrator"].ToString();
                this.AppraiserComboBox.Text = prodView[0]["Appraiser"].ToString();
            }
       }

        /// <summary>
        /// Clears the control.
        /// </summary>
        private void ClearUserTabControl()
        {
            this.UserIDTextBox.Text = string.Empty;
            this.UserNameTextBox.Text = string.Empty;
            this.EmailTextBox.Text = string.Empty;
            this.NetNameTextBox.Text = string.Empty;
            this.DisplayNameTextBox.Text = string.Empty;
            this.ActiveList.SelectedIndex = 1;
            this.AdminList.SelectedIndex = 0;
            this.AppraiserComboBox.SelectedIndex = 0;
            this.GroupDataGrid.DataSource = null;
            this.UserControlClearDataBinding();
            this.UserGridView.Enabled = false;
        }

        /// <summary>
        /// Checks the required field.
        /// </summary>
        /// <returns> Retruns true if all field are filled otherwise false</returns>
        private bool CheckRequiredField()
        {
            if (this.UserNameTextBox.Text.Trim().Length > 0 && this.NetNameTextBox.Text.Trim().Length > 0 && this.ActiveList.SelectedIndex >= 0 && this.AdminList.SelectedIndex >= 0 && this.AppraiserComboBox.SelectedIndex >=0 && this.DisplayNameTextBox.Text.Trim().Length > 0)
            {
                if (this.buttonOperation != (int)ButtonOperation.New && this.UserIDTextBox.Text.Trim().Length < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether  DataTable is Exist or not in  [the specified CMNT data set].
        /// </summary>
        /// <param name="userDataSet">The user data set.</param>
        /// <returns>retrun true if valid dataset else false </returns>
        private bool CheckValidRowCount(DataSet userDataSet)
        {
            if (userDataSet != null)
            {
                foreach (DataTable checkDataTable in userDataSet.Tables)
                {
                    if (checkDataTable.Rows.Count > 0)
                    {
                        this.validDataSet = true;
                    }
                    else
                    {
                        this.validDataSet = false;
                        break;
                    }
                }
            }
            else
            {
                this.validDataSet = false;
            }

            return this.validDataSet;
        }

        /// <summary>
        /// Sets the button.
        /// </summary>
        /// <param name="buttonName">Name of the button.</param>
        private void SetButton(ButtonOperation buttonName)
        {
            switch (buttonName)
            {
                case ButtonOperation.New:
                    {
                        this.UserNewButton.Enabled = false;
                        this.UserSaveButton.Enabled = true;
                        this.UserDeleteButton.Enabled = false;
                        this.UserCancelButton.Enabled = true;
                        break;
                    }

                case ButtonOperation.Save:
                    {
                        this.UserNewButton.Enabled = true;
                        this.UserSaveButton.Enabled = false;
                        this.UserDeleteButton.Enabled = true;
                        this.UserCancelButton.Enabled = false;
                        break;
                    }

                case ButtonOperation.GridOperation:
                    {
                        if (this.keyPressed == true)
                        {
                            this.UserNewButton.Enabled = false;

                            this.UserDeleteButton.Enabled = false;
                            this.UserSaveButton.Enabled = true;
                            this.UserCancelButton.Enabled = true;
                        }
                        else
                        {
                            this.UserNewButton.Enabled = true;
                            if (this.userRecord > 0)
                            {
                                this.UserDeleteButton.Enabled = true;
                            }
                            else
                            {
                                this.UserDeleteButton.Enabled = false;
                            }

                            this.UserSaveButton.Enabled = false;
                            this.UserCancelButton.Enabled = false;
                        }

                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        this.UserNewButton.Enabled = true;
                        this.UserSaveButton.Enabled = false;
                        this.UserCancelButton.Enabled = false;
                        if (this.userRecord > 0)
                        {
                            this.UserDeleteButton.Enabled = true;
                        }
                        else
                        {
                            this.UserDeleteButton.Enabled = false;
                        }

                        this.UserGridView.Enabled = true;
                        break;
                    }

                case ButtonOperation.Empty:
                    {
                        this.UserNewButton.Enabled = true;
                        this.UserSaveButton.Enabled = false;
                        this.UserCancelButton.Enabled = false;
                        if (this.userRecord > 0)
                        {

                            this.UserDeleteButton.Enabled = true;
                        }
                        else
                        {
                            this.UserDeleteButton.Enabled = false;
                        }
                        this.terrscanUserid = TerraScanCommon.UserId;
                        if (!string.IsNullOrEmpty(this.UserIDTextBox.Text))
                        {
                            this.testUserid = Convert.ToInt32(this.UserIDTextBox.Text);
                        }
                        else
                        {
                            this.testUserid = 0;
                        }

                        if (this.terrscanUserid == this.testUserid)
                        {
                            UserDeleteButton.Enabled = false;

                        }
                        else
                        {
                            UserDeleteButton.Enabled = true;
                        }


                        this.UserGridView.Enabled = true;
                        if (this.userGroupRowCount > 0)
                        {
                            this.GroupDataGrid.Enabled = true;
                        }

                        break;
                    }

                case ButtonOperation.Update:
                    {
                        if (this.keyPressed)
                        {
                            this.UserNewButton.Enabled = false;
                            this.UserSaveButton.Enabled = true;
                            this.UserCancelButton.Enabled = true;
                            this.UserDeleteButton.Enabled = false;
                            this.UserGridView.Enabled = true;
                            this.GroupDataGrid.Enabled = false;
                        }

                        break;
                    }

                case ButtonOperation.InValidRow:
                    {
                        this.UserNewButton.Enabled = true;
                        this.UserSaveButton.Enabled = false;
                        this.UserCancelButton.Enabled = false;
                        this.UserGridView.Enabled = true;
                        if (this.buttonOperation != (int)ButtonOperation.Delete)
                        {
                             this.UserDeleteButton.Enabled = false;
                        }
                        
                      
                        break;
                    }
            }
        }

        /// <summary>
        /// Disables the control.
        /// </summary>
        private void DisableControl()
        {
            //// this.UserIDTextBox.Enabled = false;
            this.UserNameTextBox.Enabled = false;
            this.EmailTextBox.Enabled = false;
            this.NetNameTextBox.Enabled = false;
            this.DisplayNameTextBox.Enabled = false;

            this.ActiveList.Enabled = false;
            this.AdminList.Enabled = false;
            this.AppraiserComboBox.Enabled = false;
            this.ActiveList.BackColor = System.Drawing.Color.White;
            this.AdminList.BackColor = System.Drawing.Color.White;
            this.AppraiserComboBox.BackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// Enables the control.
        /// </summary>
        private void EnableControl()
        {
            ////this.UserIDTextBox.Enabled = false;
            this.UserNameTextBox.Enabled = true;
            this.EmailTextBox.Enabled = true;
            this.NetNameTextBox.Enabled = true;
            this.DisplayNameTextBox.Enabled = true;
            this.ActiveList.Enabled = true;
            this.AdminList.Enabled = true;
            this.AppraiserComboBox.Enabled = true;
            if (this.userGroupRowCount > 0)
            {
                this.GroupDataGrid.Enabled = true;
            }
        }

        /// <summary>
        /// Sets the group name with left alignment.
        /// </summary>
        /// <param name="Label">The label.</param>
        private void SetGroupNameWithLeftAlignment(Label Label)
        {
            //string groupHeaderLabel;
            //groupHeaderLabel = Label.Text;
            //Graphics graphics = this.CreateGraphics();
            //////Get the width of the string
            //SizeF sizeF = graphics.MeasureString(groupHeaderLabel, this.Font);
            //float preferredwidth = sizeF.Width;
            //int decrement = 37;
            //////Compare the string width and required width of textbox
            //if (groupHeaderLabel.Length > decrement)
            //{
            //    while (preferredwidth > 203)
            //    {
            //        ////If string width is greater than required width, remove the first character from the string
            //        if (preferredwidth > 203)
            //        {
            //            groupHeaderLabel = groupHeaderLabel.Substring(groupHeaderLabel.Length - decrement);
            //            sizeF = graphics.MeasureString(groupHeaderLabel, this.Font);
            //            preferredwidth = sizeF.Width;
            //            decrement = decrement - 1;
            //         }
            //     }
            //}

            //Label.Text = groupHeaderLabel;
            //this.GroupHeaderLabel.Location = new System.Drawing.Point(353, 147);
            //graphics.Dispose();
        }
        #endregion

        #endregion

        #region Coding For Group Tab

        #region Group Tab Methods

        ///<summary> 
        /// Sets the data binding.
        /// </summary>
        private void GroupControl1SetDataBinding()
        {
            this.GroupNameTextBox.DataBindings.Add("Text", this.groupManagement.Tables[0], "GroupID");
            this.GroupDescTextBox.DataBindings.Add("Text", this.groupManagement.Tables[0], "Description");
            this.GroupIDTextBox.DataBindings.Add("Text", this.groupManagement.Tables[0], "Email");
        }

        /// <summary>
        /// Clears the data binding.
        /// </summary> 
        private void GroupControlClearDataBinding1()
        {
            this.GroupNameTextBox.DataBindings.Clear();
            this.GroupDescTextBox.DataBindings.Clear();
            this.GroupIDTextBox.DataBindings.Clear();
        }

        /// <summary>
        /// Sets the grop text boxes.
        /// </summary>
        /// <param name="gropRowId">The grop row id.</param>
        private void SetGropTextBoxes(int gropRowId)
        {
            /*this.GroupDescTextBox.Text = this.groupManagement.Tables[0].Rows[gropRowId]["Description"].ToString();
            this.GroupIDTextBox.Text = this.groupManagement.Tables[0].Rows[gropRowId]["GroupID"].ToString();
            this.GroupNameTextBox.Text = this.groupManagement.Tables[0].Rows[gropRowId]["GroupName"].ToString();*/
            if (this.groupsTotalRecordCount > 0)
            {
                this.GroupDescTextBox.Text = this.GroupTabGroupGrid.Rows[gropRowId].Cells["Description"].Value.ToString();
                this.GroupIDTextBox.Text = this.GroupTabGroupGrid.Rows[gropRowId].Cells["GroupID"].Value.ToString();
                this.GroupNameTextBox.Text = this.GroupTabGroupGrid.Rows[gropRowId].Cells["GroupListName"].Value.ToString();
                //// Added By Guhan on - 23jan07
                this.GroupLinkLabel.Text = this.groupLink + this.GroupIDTextBox.Text;
                //// Till here
            }
            else
            {
                //// Added By Guhan on - 23jan07
                this.GroupLinkLabel.Text = this.groupLink;
                //// Till here
            }
        }

        /// <summary>
        /// Groups the tab init.
        /// </summary>
        private void GroupTabInit()
        {
            this.ParentForm.CancelButton = this.GroupCancelbutton;
            if (this.buttonOperation == (int)ButtonOperation.New)
            {
                this.UserManagementTab.SelectedIndex = 0;
            }
            else
            {
                if (this.changeInGroupDataSet != true)
                {
                    //// TODO : Coding For Group
                    this.groupLabel.Text = this.UserManagementTab.SelectedTab.Text;
                    this.CustomiseGroupListGrid();
                    this.CustomiseGrid();
                    this.LoadGropListDataGridView();

                    this.EnableUserInSorting();
                    ///// Assing ContrlKey + Eevnet and Remove for other Tabs

                    this.SetGroupButtonEvent();
                    this.SetGroupButton(ButtonOperation.Empty);
                    //// if (buttonOperation != (int) ButtonOperation.Delete)
                    ////{
                    this.userGridRowIndex = 0;
                    this.tempGroupRowId = 0;
                    //// }
                }
            }
        }

        /// <summary>
        /// Sets the group button.
        /// </summary>
        /// <param name="groupButton">The group button.</param>
        private void SetGroupButton(ButtonOperation groupButton)
        {
            switch (groupButton)
            {
                case ButtonOperation.Empty:
                    {
                        this.GroupNewButton.Enabled = true;
                        this.GroupSaveButton.Enabled = false;
                        this.GroupCancelbutton.Enabled = false;
                        if (this.groupsTotalRecordCount > 0)
                        {
                            this.GroupDeleteButton.Enabled = true;
                        }
                        else
                        {
                            this.GroupDeleteButton.Enabled = false;
                        }

                        this.GroupTabGroupGrid.Enabled = true;
                        /*if (this.userInTotalCount > 0)
                        //{
                        //    this.UserInButton.Enabled = true; 
                        //}
                        //else
                        //{
                        //    this.UserInButton.Enabled = false;
                        //}

                        //if (this.userOutTotalCount > 0)
                        //{
                        //    this.UserOutButton.Enabled = true;
                        //}
                        //else
                        //{
                        //    this.UserOutButton.Enabled = false;
                        //}*/

                        break;
                    }

                case ButtonOperation.New:
                    {
                        this.GroupNewButton.Enabled = false;
                        this.GroupSaveButton.Enabled = true;
                        this.GroupCancelbutton.Enabled = true;
                        this.GroupDeleteButton.Enabled = false;
                        this.GroupTabGroupGrid.Enabled = false;
                        break;
                    }

                case ButtonOperation.Save:
                    {
                        this.GroupNewButton.Enabled = true;
                        this.GroupSaveButton.Enabled = false;
                        this.GroupCancelbutton.Enabled = false;
                        this.GroupTabGroupGrid.Enabled = true;
                        if (this.groupManagement.Tables.Count > 0 && this.groupManagement != null)
                        {
                            this.GroupDeleteButton.Enabled = true;
                        }
                        else
                        {
                            this.GroupDeleteButton.Enabled = false;
                        }

                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        this.GroupNewButton.Enabled = true;
                        this.GroupSaveButton.Enabled = false;
                        this.GroupCancelbutton.Enabled = false;
                        ////if (this.groupValidDataSet)
                        ////{
                        ////    this.GroupDeleteButton.Enabled = true;
                        ////}
                        ////else
                        ////{
                        ////    this.GroupDeleteButton.Enabled = false;
                        ////}

                        break;
                    }

                case ButtonOperation.Update:
                    {
                        this.GroupNewButton.Enabled = false;
                        this.GroupSaveButton.Enabled = true;
                        this.GroupCancelbutton.Enabled = true;
                        this.GroupDeleteButton.Enabled = false;
                        this.GroupsDataGridView.Enabled = true;
                        this.groupButtonOpertion = (int)ButtonOperation.Update;  
                        break;
                    }

                case ButtonOperation.InValidRow:
                    {
                        this.GroupNewButton.Enabled = true;
                        this.GroupSaveButton.Enabled = false;
                        this.GroupCancelbutton.Enabled = false;
                        this.GroupDeleteButton.Enabled = false;
                        this.GroupsDataGridView.Enabled = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Sets the swap button.
        /// </summary>
        /// <param name="userInRowsCount">The user in rows count.</param>
        /// <param name="userNotInCount">The user not in count.</param>
        private void SetSwapButton(int userInRowsCount, int userNotInCount)
        {
            if (userNotInCount > 0)
            {
                this.UserInButton.Enabled = true;
            }
            else
            {
                this.UserInButton.Enabled = false;
            }

            if (userInRowsCount > 0)
            {
                this.UserOutButton.Enabled = true;
            }
            else
            {
                this.UserOutButton.Enabled = false;
            }
        }

        /// <summary> 
        /// Loads the grop list data grid view.
        /// </summary>
        private void LoadGropListDataGridView()
        {
            int selectedMaxGroupID = 0;
            try
            {
                this.groupManagement.ListUserDetail.Clear();
                this.groupManagement.ListUserGroupDetail.Clear();
                this.groupManagement = this.form9002Control.WorkItem.GetGroupDetails;     //// F9002WorkItem.GetGroupDetails();
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            ////catch (Exception ex)
            ////{
            ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            ////}

            // this.groupValidDataSet = CheckValidDataSet(this.groupManagement);

            if (this.groupManagement != null)
            {
                this.groupsTotalRecordCount = this.groupManagement.ListGroupsGroupDetail.Rows.Count;
               
                if (this.groupsTotalRecordCount > 0)
                {
                    if (this.groupButtonOpertion == (int)ButtonOperation.New)
                    {
                        DataView groupIDDataView = new DataView(this.groupManagement.ListGroupsGroupDetail);
                        groupIDDataView.Sort = this.groupManagement.ListGroupsGroupDetail.GroupIDColumn.ColumnName;

                        DataRow[] selectedRow = this.groupManagement.ListGroupsGroupDetail.Select(this.groupManagement.ListGroupsGroupDetail.GroupIDColumn.ColumnName + "= MAX(" + this.groupManagement.ListGroupsGroupDetail.GroupIDColumn.ColumnName + ")");

                        int newRowId;

                        if (selectedRow.Length > 0)
                        {
                            newRowId = Convert.ToInt32(selectedRow[0][0].ToString());
                        }
                        else
                        {
                            newRowId = 0;
                        }

                       selectedMaxGroupID = groupIDDataView.Find(newRowId);
                    }
                    else if (this.groupButtonOpertion != (int)ButtonOperation.Delete)
                    {
                        selectedMaxGroupID = this.tempGroupRowId;
                    }

                    this.EnableGroupGridSorting();
                    this.GroupTabGroupGrid.DataSource = this.groupManagement.ListGroupsGroupDetail;
                    this.groupValidRow = true;

                    if (this.groupsTotalRecordCount > GroupTabGroupGrid.NumRowsVisible)
                    {
                        this.GroupGridVerticalScrollBar.Enabled = true;
                        this.GroupGridVerticalScrollBar.Visible = false;
                    }
                    else
                    {
                        this.GroupGridVerticalScrollBar.BringToFront();
                        this.GroupGridVerticalScrollBar.Enabled = false;
                        this.GroupGridVerticalScrollBar.Visible = true;
                    }

                    if (this.groupButtonOpertion == (int)ButtonOperation.New || this.groupButtonOpertion != (int)ButtonOperation.Delete)
                    {
                        this.SetDataGridViewPosition(this.GroupTabGroupGrid, selectedMaxGroupID);
                        this.SetGropTextBoxes(selectedMaxGroupID);
                        this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[selectedMaxGroupID].Cells["GroupID"].Value.ToString());
                        this.GroupHeaderLabel.Text = this.GroupTabGroupGrid.Rows[selectedMaxGroupID].Cells["GroupListName"].Value.ToString() + "  Group";
                        this.SetSwapButton(this.userInTotalCount, this.userOutTotalCount);
                        this.SetGroupControlEnable();
                       //// this.SetGroupNameWithLeftAlignment(this.GroupHeaderLabel);  
                    }
                }
                else
                {
                    this.swampInDataSet.Clear();
                    this.SetUserInGridVScrollBar(this.swampInDataSet.Tables[0].Rows.Count);
                    this.UserInGrid.DataSource = this.swampInDataSet.Tables[0];

                    DataSet tempEmptyUserDataSet1 = new DataSet();
                    DataTable tempEmptTable = new DataTable();
                    tempEmptyUserDataSet1.Tables.Add(tempEmptTable);
                    tempEmptyUserDataSet1.Tables[0].Merge(this.groupManagement.ListGroupDetail);

                    DataColumn[] keyColumns = new DataColumn[] { tempEmptyUserDataSet1.Tables[0].Columns["UserID"] };
                    RemoveDuplicates(ref tempEmptyUserDataSet1, keyColumns);
                    this.swampOutDataSet.Clear();
                    this.swampOutDataSet.Merge(tempEmptyUserDataSet1.Tables[0]);
                    DataView userNotInSortRowInit = new DataView(this.swampOutDataSet.Tables[0]);

                    //// userNotInSortRowInit.Sort = "FullName" + this.userNotInSortedOrder;
                    //// userNotInSortRowInit.Sort = "FullName ASC";
                    this.userNotInSortDataTable = userNotInSortRowInit.ToTable();
                    this.SetUserNotInGridVScrollBar(this.userNotInSortDataTable.Rows.Count);
                    this.userNotIngrid.DataSource = this.userNotInSortDataTable;
                    this.SetUserInGridVScrollBar(this.swampInDataSet.Tables[0].Rows.Count);
                    this.UserInGrid.DataSource = this.swampOutDataSet.Tables[0];
                    this.SetGroupControlDisable();
                    this.userNotIngrid.Enabled = false;
                    this.UserInGrid.Enabled = false;
                    this.SetSwapButton(0, 0);
                }

                this.SetGroupButton(ButtonOperation.Empty);
            }
            else
            {
                this.SetGroupControlDisable();
                this.UserInButton.Enabled = false;
                this.UserOutButton.Enabled = false;
            }
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomiseGroupListGrid()
        {
            this.GroupTabGroupGrid.AllowUserToResizeColumns = false;
            this.GroupTabGroupGrid.AutoGenerateColumns = false;
            this.GroupTabGroupGrid.AllowUserToResizeRows = false;
            this.GroupTabGroupGrid.StandardTab = true;
            DataGridViewColumnCollection columns = this.GroupTabGroupGrid.Columns;
            columns["GroupID"].DataPropertyName = "GroupID";
            columns["GroupListName"].DataPropertyName = "GroupName";
            columns["Description"].DataPropertyName = "Description";
            columns["GroupID"].DisplayIndex = 0;
            columns["GroupListName"].DisplayIndex = 1;
            columns["Description"].DisplayIndex = 2;
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomiseGrid()
        {
            this.UserInGrid.AllowUserToResizeColumns = false;
            this.UserInGrid.AutoGenerateColumns = false;
            this.UserInGrid.AllowUserToResizeRows = false;
            this.UserInGrid.StandardTab = true;
            this.UserInGrid.MultiSelect = true;
            DataGridViewColumnCollection userInColumns = this.UserInGrid.Columns;
            userInColumns["UserFullName"].DataPropertyName = "FullName";
            userInColumns["UserFullName"].DisplayIndex = 0;
            userInColumns["InUserId"].DataPropertyName = "UserID";
            userInColumns["InUserId"].DisplayIndex = 1;
            userInColumns["InUserId"].Visible = false;
            userInColumns["InGroupId"].DataPropertyName = "GroupID";
            userInColumns["InGroupId"].DisplayIndex = 2;
            userInColumns["InGroupId"].Visible = false;

            this.userNotIngrid.AllowUserToResizeColumns = false;
            this.userNotIngrid.AutoGenerateColumns = false;
            this.userNotIngrid.AllowUserToResizeRows = false;
            this.userNotIngrid.StandardTab = true;
            this.userNotIngrid.MultiSelect = true;
            DataGridViewColumnCollection userNotInColumns = this.userNotIngrid.Columns;
            userNotInColumns["UserNotInFullName"].DataPropertyName = "FullName";
            userNotInColumns["UserNotInFullName"].DisplayIndex = 0;
            userNotInColumns["OutUserId"].DataPropertyName = "UserID";
            userNotInColumns["OutUserId"].DisplayIndex = 1;
            //// userNotInColumns["OutUserId"].Visible = false;
            userNotInColumns["OutGroupId"].DataPropertyName = "GroupID";
            userNotInColumns["OutGroupId"].DisplayIndex = 2;
            //// userNotInColumns["OutGroupId"].Visible = false;
        }

        /// <summary>
        /// Construcs the swap table.
        /// </summary>
        private void ConstructSwapTable()
        {
            this.swampInDataSet.Tables.Add(this.swampInDataTable);
            this.swampOutDataSet.Tables.Add(this.swampOutDataTable);
            ////this.swampOutDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("FullName"), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("GroupName") });
            ////this.swampOutDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("FullName") });
            this.swampInDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("FullName"), new DataColumn("Admin") });
            this.swampOutDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("FullName"), new DataColumn("Admin") });
        }

        /// <summary>
        /// Swamps the users.
        /// </summary>
        /// <param name="statusSwap">The status swap.</param>
        /// <param name="findSwampUserID">The find swamp user ID.</param>
        private void SwapUsers(string statusSwap, ArrayList findSwampUserID)
        {
            DataTable tempStoreUserInDetails = new DataTable();
            tempStoreUserInDetails.Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("FullName") });
            DataRow tempStoreDetails;
            DataRow[] userInRows;
            this.userInRowCount = 0;
            this.stringExp = string.Empty;

            //// inorder to keep the cursour position in both user/not in grid

            for (int i = 0; i <= findSwampUserID.Count - 1; i++)
            {
                if (i != findSwampUserID.Count - 1)
                {
                    this.stringExp = this.stringExp + "userId =" + findSwampUserID[i].ToString() + " or ";
                }
                else
                {
                    this.stringExp = this.stringExp + "userId =" + findSwampUserID[i].ToString();
                }
            }

            switch (statusSwap)
            {
                case "UserIn":
                    {
                        userInRows = this.swampOutDataSet.Tables[0].Select(this.stringExp);

                        for (int rowCount = 0; rowCount < this.swampInDataSet.Tables[0].Rows.Count; rowCount++)
                        {
                            if (!string.IsNullOrEmpty(this.swampInDataSet.Tables[0].Rows[rowCount]["UserID"].ToString()))
                            {
                                tempStoreDetails = tempStoreUserInDetails.NewRow();
                                tempStoreDetails["UserID"] = this.swampInDataSet.Tables[0].Rows[rowCount]["UserID"].ToString();
                                tempStoreDetails["GroupID"] = this.swampInDataSet.Tables[0].Rows[rowCount]["GroupID"].ToString();
                                tempStoreDetails["FullName"] = this.swampInDataSet.Tables[0].Rows[rowCount]["FullName"].ToString();
                                tempStoreUserInDetails.Rows.Add(tempStoreDetails);
                                userInCount++;
                            }
                        }

                        foreach (DataRow dup in userInRows)
                        {
                            tempStoreDetails = tempStoreUserInDetails.NewRow();
                            tempStoreDetails["UserID"] = dup["UserID"].ToString();
                            tempStoreDetails["GroupID"] = dup["GroupID"].ToString();
                            tempStoreDetails["FullName"] = dup["FullName"].ToString();
                            tempStoreUserInDetails.Rows.Add(tempStoreDetails);
                        }

                        this.swampInDataSet.Clear();
                        this.userInCount = tempStoreUserInDetails.Rows.Count;
                        this.SetUserInGridVScrollBar(0);
                        this.UserInGrid.DataSource = null;
                        ////* this.swampInDataSet.Tables[0].Merge(CreateEmptyRows(tempStoreUserInDetails, 16));
                        this.swampInDataSet.Tables[0].Merge(tempStoreUserInDetails);
                        
                        //// Sort the record and store
                        DataView userInSortRow = new DataView(this.swampInDataSet.Tables[0]);
                        userInSortRow.Sort = "FullName " + this.userInSortedOrder;
                        ////userInSortRow.Sort = "FullName ASC";
                        this.userInSortDataTable = userInSortRow.ToTable();
                        this.SetUserInGridVScrollBar(this.userInSortDataTable.Rows.Count);
                        this.UserInGrid.DataSource = this.userInSortDataTable;
                        foreach (DataRow dup in userInRows)
                        {
                            this.swampOutDataSet.Tables[0].Rows.Remove(dup);
                        }

                        tempStoreUserInDetails.Clear();

                        for (int rowCount = 0; rowCount < this.swampOutDataSet.Tables[0].Rows.Count; rowCount++)
                        {
                            if (!string.IsNullOrEmpty(this.swampOutDataSet.Tables[0].Rows[rowCount]["UserID"].ToString()))
                            {
                                tempStoreDetails = tempStoreUserInDetails.NewRow();
                                tempStoreDetails["UserID"] = this.swampOutDataSet.Tables[0].Rows[rowCount]["UserID"].ToString();
                                tempStoreDetails["GroupID"] = this.swampOutDataSet.Tables[0].Rows[rowCount]["GroupID"].ToString();
                                tempStoreDetails["FullName"] = this.swampOutDataSet.Tables[0].Rows[rowCount]["FullName"].ToString();
                                tempStoreUserInDetails.Rows.Add(tempStoreDetails);
                                userNotInRowCount++;
                            }
                        }

                        ////   this.userOutRowID = this.userNotInRowCount;

                        if (this.userNotInRowCount == 0)
                        {
                            ResetBrowseControls(0, this.UserInButton);
                            this.UserInGrid.Enabled = false;
                        }
                        else
                        {
                            ResetBrowseControls(1, this.UserInButton);
                            this.UserInGrid.Enabled = true;
                        }

                        ResetBrowseControls(1, this.UserOutButton);
                        this.swampOutDataSet.Clear();

                        /*DataView dv = new DataView(this.swampOutDataSet.Tables[0]);
                        dv.Sort = "FullName" + " ASC";
                        this.swampOutDataSet.Clear();
                        DataTable Dt = new DataTable();
                        Dt =  dv.Table.Clone() ;
                        this.swampOutDataSet.Tables[0].Merge(Dt); */

                        //// this.swampOutDataSet.Tables[0].Merge(CreateEmptyRows(tempStoreUserInDetails, 16));
                        this.swampOutDataSet.Tables[0].Merge(tempStoreUserInDetails);
                        DataView userInSortRow1 = new DataView(this.swampOutDataSet.Tables[0]);
                        //// userInSortRow1.Sort = "FullName ASC";
                        userInSortRow1.Sort = "FullName " + this.userNotInSortedOrder;
                        this.userNotInSortDataTable = userInSortRow1.ToTable();

                        this.userNotIngrid.DataSource = null;
                        this.SetUserNotInGridVScrollBar(this.userNotInSortDataTable.Rows.Count);

                        this.userNotIngrid.DataSource = this.userNotInSortDataTable;
                        this.changeInGroupDataSet = true;
                        break;
                    }

                case "UserOut":
                    {
                        userInRows = this.swampInDataSet.Tables[0].Select(this.stringExp);

                        for (int rowCount = 0; rowCount < this.swampOutDataSet.Tables[0].Rows.Count; rowCount++)
                        {
                            if (!string.IsNullOrEmpty(this.swampOutDataSet.Tables[0].Rows[rowCount]["UserID"].ToString()))
                            {
                                tempStoreDetails = tempStoreUserInDetails.NewRow();
                                tempStoreDetails["UserID"] = this.swampOutDataSet.Tables[0].Rows[rowCount]["UserID"].ToString();
                                tempStoreDetails["GroupID"] = this.swampOutDataSet.Tables[0].Rows[rowCount]["GroupID"].ToString();
                                tempStoreDetails["FullName"] = this.swampOutDataSet.Tables[0].Rows[rowCount]["FullName"].ToString();
                                tempStoreUserInDetails.Rows.Add(tempStoreDetails);
                            }
                        }

                        foreach (DataRow dup in userInRows)
                        {
                            if (!string.IsNullOrEmpty(dup["UserID"].ToString()))
                            {
                                tempStoreDetails = tempStoreUserInDetails.NewRow();
                                tempStoreDetails["UserID"] = dup["UserID"].ToString();
                                tempStoreDetails["GroupID"] = dup["GroupID"].ToString();
                                tempStoreDetails["FullName"] = dup["FullName"].ToString();
                                tempStoreUserInDetails.Rows.Add(tempStoreDetails);
                            }
                        }

                        this.userNotInRowCount = tempStoreUserInDetails.Rows.Count;

                        //// if there is user not then disable grdi else enable
                        if (this.userNotInRowCount == 0)
                        {
                            this.userNotIngrid.Enabled = false;
                        }
                        else
                        {
                            this.userNotIngrid.Enabled = true;
                        }

                        this.userOutRowID = this.userNotInRowCount;
                        this.swampOutDataSet.Clear();

                        ///// * this.swampOutDataSet.Tables[0].Merge(CreateEmptyRows(tempStoreUserInDetails, 16));

                        this.swampOutDataSet.Tables[0].Merge(tempStoreUserInDetails);

                        this.userNotIngrid.DataSource = null;

                        //// Sort the record and store
                        DataView userNotInSortRow = new DataView(this.swampOutDataSet.Tables[0]);
                        userNotInSortRow.Sort = "FullName  " + this.userNotInSortedOrder;
                        //// userNotInSortRow.Sort = "FullName ASC";
                        this.userNotInSortDataTable = userNotInSortRow.ToTable();
                        this.SetUserNotInGridVScrollBar(this.userNotInSortDataTable.Rows.Count);

                        this.userNotIngrid.DataSource = this.userNotInSortDataTable;
                        foreach (DataRow dup in userInRows)
                        {
                            this.swampInDataSet.Tables[0].Rows.Remove(dup);
                        }

                        tempStoreUserInDetails.Clear();

                        for (int rowCount = 0; rowCount < this.swampInDataSet.Tables[0].Rows.Count; rowCount++)
                        {
                            if (!string.IsNullOrEmpty(this.swampInDataSet.Tables[0].Rows[rowCount]["UserID"].ToString()))
                            {
                                tempStoreDetails = tempStoreUserInDetails.NewRow();
                                tempStoreDetails["UserID"] = this.swampInDataSet.Tables[0].Rows[rowCount]["UserID"].ToString();
                                tempStoreDetails["GroupID"] = this.swampInDataSet.Tables[0].Rows[rowCount]["GroupID"].ToString();
                                tempStoreDetails["FullName"] = this.swampInDataSet.Tables[0].Rows[rowCount]["FullName"].ToString();
                                tempStoreUserInDetails.Rows.Add(tempStoreDetails);
                                userInRowCount++;
                                userInCount++;
                            }
                        }

                        if (this.userInRowCount == 0)
                        {
                            ResetBrowseControls(0, this.UserOutButton);
                            this.UserInGrid.Enabled = false;
                        }
                        else
                        {
                            ResetBrowseControls(1, this.UserOutButton);
                            this.UserInGrid.Enabled = true;
                        }

                        ResetBrowseControls(1, this.UserInButton);

                        this.swampInDataSet.Clear();
                        ///// * this.swampInDataSet.Tables[0].Merge(CreateEmptyRows(tempStoreUserInDetails, 16));

                        this.swampInDataSet.Tables[0].Merge(tempStoreUserInDetails);
                        this.UserInGrid.DataSource = null;

                        //// Sort the record and store
                        DataView userNotInSortRow1 = new DataView(this.swampInDataSet.Tables[0]);
                        userNotInSortRow1.Sort = "FullName " + this.userInSortedOrder;
                        /////  userNotInSortRow1.Sort = "FullName ASC";
                        this.userInSortDataTable = userNotInSortRow1.ToTable();

                        this.SetUserInGridVScrollBar(this.userInSortDataTable.Rows.Count);
                        this.UserInGrid.DataSource = this.userInSortDataTable;

                        this.changeInGroupDataSet = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Sets the data binding.
        /// </summary>
        private void GroupControlSetDataBinding()
        {
            this.GroupIDTextBox.DataBindings.Add("Text", this.groupManagement.Tables[0], "GroupID");
            this.GroupNameTextBox.DataBindings.Add("Text", this.groupManagement.Tables[0], "GroupName");
            this.GroupDescTextBox.DataBindings.Add("Text", this.groupManagement.Tables[0], "Description");
        }

        /// <summary>
        /// Clears the data binding.
        /// </summary> 
        private void GroupControlClearDataBinding()
        {
            this.GroupIDTextBox.DataBindings.Clear();
            this.GroupNameTextBox.DataBindings.Clear();
            this.GroupDescTextBox.DataBindings.Clear();
        }

        /// <summary>
        /// Finds the user from group list.
        /// </summary>
        /// <param name="findGroupID">The find group ID.</param>
        private void FindUserFromGroupList(string findGroupID)
        {
            bool validInRecord;
            ////bool validOutRecord;
            //// string tempUserId = string.Empty;
            StringBuilder tempUserId = new StringBuilder();

            DataRow[] userInRows;
            this.stringExp = "GroupId =" + findGroupID;
            userInRows = this.groupManagement.ListGroupDetail.Select(this.stringExp);
            this.swampInDataSet.Tables.Clear();
            this.swampInDataSet.Merge(userInRows);
            ////this.CheckValidRowCount(this.swampInDataSet);
            if (userInRows.Length <= 0)
            {
                this.swampInDataTable.Clear();
                this.swampInDataSet.Tables.Add(this.swampInDataTable);
                validInRecord = true;
                ////this.swampInDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("FullName"), new DataColumn("Admin") });
            }
            else
            {
                validInRecord = false;
            }

            if (this.swampInDataSet.Tables[0].Rows.Count > 0)
            {
                this.userInTotalCount = this.swampInDataSet.Tables[0].Rows.Count;
            }
            else
            {
                this.userInTotalCount = 0;
            }

            ////this.swampInDataSet.Merge(CreateEmptyRows(this.swampInDataSet.Tables[0], 0));

            ////  validInRecord = CheckEmptyRecords(this.swampInDataSet);

            DataView tempUserSort = new DataView(this.swampInDataSet.Tables[0]);
            tempUserSort.Sort = "FullName ASC";
            this.userInSortDataTable.Rows.Clear();
            this.userInSortDataTable = tempUserSort.ToTable();
            this.SetUserInGridVScrollBar(this.userInSortDataTable.Rows.Count);
            this.UserInGrid.DataSource = this.userInSortDataTable;

            if (this.userInTotalCount > this.UserInGrid.NumRowsVisible)
            {
                this.UserInVscrollBar.Enabled = true;
                this.UserInVscrollBar.Visible = false;
            }
            else
            {
                this.UserInVscrollBar.BringToFront();
                this.UserInVscrollBar.Enabled = false;
                this.UserInVscrollBar.Visible = true;
            }

            DataRow[] userNotRows;
            for (int i = 0; i <= userInRows.Length - 1; i++)
            {
                if (i != userInRows.Length - 1)
                {
                    //// tempUserId = tempUserId + " userId <>" + userInRows[i][0].ToString() + " And ";
                    tempUserId.Append(" userId <>" + userInRows[i][0].ToString() + " And ");
                }
                else
                {
                    //// tempUserId = tempUserId + "userId <>" + userInRows[i][0].ToString();
                    tempUserId.Append("userId <>" + userInRows[i][0].ToString());
                }
            }

            if (tempUserId.Length > 0)
            {
                userNotRows = this.groupManagement.ListGroupDetail.Select(tempUserId.ToString());
            }
            else
            {
                tempUserId.Append(" userId <>" + 0);
                userNotRows = this.groupManagement.ListGroupDetail.Select(tempUserId.ToString());
            }

            this.swampOutDataSet.Tables.Clear();
            if (userNotRows.Length <= 0)
            {
                DataTable tempDataTable = new DataTable();

                this.swampOutDataSet.Tables.Add(tempDataTable);
                this.swampOutDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("FullName"), new DataColumn("Admin") });
                validInRecord = true;
            }
            else
            {
                validInRecord = false;
            }

            this.swampOutDataSet.Merge(userNotRows);

            ////userNotRows = this.groupManagement.ListGroupDetail.Select(this.stringExp);

            if (this.swampOutDataSet.Tables[0].Rows.Count > 0)
            {
                this.userOutTotalCount = this.swampOutDataSet.Tables[0].Rows.Count;
            }
            else
            {
                this.userOutTotalCount = 0;
            }

            ////  this.swampOutDataSet.Merge(CreateEmptyRows(this.swampOutDataSet.Tables[0], 10));
            //  this.swampOutDataSet.Merge(this.swampOutDataSet.Tables[0]);

            /*Create an array of DataColumns to compare
            If these columns all match we consider the 
            rows duplicate.*/
            DataColumn[] keyColumns1 = new DataColumn[] { this.swampOutDataSet.Tables[0].Columns["UserID"] };
            RemoveDuplicates(ref this.swampOutDataSet, keyColumns1);
            if (this.swampInDataSet.Tables.Count > 0 && validInRecord)
            {
                this.swampOutDataSet = RemoveDuplicateUserID(this.swampOutDataSet, this.swampInDataSet);
            }

            if (this.swampOutDataSet.Tables.Count > 0)
            {
                //// DataView myDVM = new DataView(this.swampOutDataSet.Tables[0]);
                //// myDVM.Sort = "FullName ASC";
                //// * this.swampOutDataSet.Merge(CreateEmptyRow(this.swampOutDataSet.Tables[0], 16));

                DataView dv = new DataView(this.swampOutDataSet.Tables[0]);
                dv.Sort = "FullName" + " ASC";
                DataTable sortedDatas = dv.ToTable();
                this.SetUserNotInGridVScrollBar(sortedDatas.Rows.Count);                
                this.userNotIngrid.DataSource = sortedDatas;
            }
            else
            {
                DataTable tempUserOutTable = new DataTable();
                this.swampOutDataSet.Tables.Add(tempUserOutTable);
                ////this.swampOutDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("FullName") });

                /////* this.swampOutDataSet.Merge(CreateEmptyRows(this.swampOutDataSet.Tables[0], 16));
                this.swampOutDataSet.Tables[0].Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")), new DataColumn("FullName"), new DataColumn("GroupID", System.Type.GetType("System.Int16")), new DataColumn("GroupName") });
                this.SetUserNotInGridVScrollBar(this.swampOutDataSet.Tables[0].Rows.Count);
                this.userNotIngrid.DataSource = this.swampOutDataSet.Tables[0];
            }

            this.SetSwapButton(this.userInTotalCount, this.userOutTotalCount);
        }

        /// <summary> 
        /// Sets the group control disable.
        /// </summary>
        private void SetGroupControlDisable()
        {
            this.GroupNameTextBox.Enabled = false;
            this.GroupDescTextBox.Enabled = false;
            this.GroupNameTextBox.BackColor = Color.White;
            this.GroupDescTextBox.BackColor = Color.White;
            this.GroupIDTextBox.BackColor = Color.White;
        }

        /// <summary>
        /// Sets the group control enable.
        /// </summary>
        private void SetGroupControlEnable()
        {
            this.GroupNameTextBox.Enabled = true;
            this.GroupDescTextBox.Enabled = true;
        }

        #endregion

        /// <summary>
        /// Handles the CellContentClick event of the GroupTabGroupGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDetailsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.groupValidRow = true;
                this.UserInGrid.Enabled = true;
                this.userNotIngrid.Enabled = true;
                if (!this.changeInGroupDataSet)
                {
                    this.tempGroupRowId = e.RowIndex;
                    if (e.ColumnIndex >= 0)
                    {
                        this.tempGroupColumId = e.ColumnIndex;
                    }
                    else
                    {
                        this.tempGroupColumId = 0;
                    }
                    //// this.SetButton(ButtonOperation.Empty);
                }

                if (this.changeInGroupDataSet)
                {
                    if (this.tempGroupRowId != e.RowIndex)
                    {
                        switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName ,"?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                {                                    
                                    this.SaveGroupDatas();
                                       
                                    if (this.closingForm)
                                    {
                                        this.changeInGroupDataSet = false;
                                        this.SetGropTextBoxes(e.RowIndex);
                                        this.SetDataGridViewPosition(this.GroupTabGroupGrid, e.RowIndex);
                                       //// this.SetGroupNameWithLeftAlignment(this.GroupHeaderLabel);
                                        this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupID"].Value.ToString());
                                        this.EnableGroupGridSorting();
                                    }
                                    else
                                    {
                                        if (e.ColumnIndex >= 0)
                                        {
                                            this.GroupTabGroupGrid.CurrentCell = this.GroupTabGroupGrid[e.ColumnIndex, this.tempGroupRowId];
                                        }
                                        else
                                        {
                                            this.SetDataGridViewPosition(this.GroupTabGroupGrid, this.tempGroupRowId);
                                        }
                                    }
                                    
                                    this.SetGroupButton(ButtonOperation.Empty);
                                    this.changeInGroupDataSet = false;
                                    break;
                                }

                            case DialogResult.No:
                                {
                                    this.SetDataGridViewPosition(this.GroupTabGroupGrid, e.RowIndex);
                                    this.SetGropTextBoxes(e.RowIndex);
                                   //// this.SetGroupNameWithLeftAlignment(this.GroupHeaderLabel);
                                    this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupID"].Value.ToString());
                                    this.EnableGroupGridSorting();
                                    this.changeInGroupDataSet = false;
                                    this.GroupHeaderLabel.Text = this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupListName"].Value.ToString() + "  Group";
                                    this.SetGroupButton(ButtonOperation.Empty);
                                  ////  this.SetGroupNameWithLeftAlignment(this.GroupHeaderLabel);  
                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    if (e.ColumnIndex >= 0)
                                    {
                                        this.GroupTabGroupGrid.CurrentCell = this.GroupTabGroupGrid[this.tempGroupColumId, this.tempGroupRowId];
                                    }
                                    else
                                    {
                                        this.SetDataGridViewPosition(this.GroupTabGroupGrid, this.tempGroupRowId);
                                    }

                                    ////    this.SetGropTextBoxes(this.tempGroupRowId);

                                    break;
                                }
                        }
                    }
                }
                else
                {
                    this.SetGroupButton(ButtonOperation.Empty);
                    this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupID"].Value.ToString());
                    this.GroupHeaderLabel.Text = this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupListName"].Value.ToString() + "  Group";
                    this.SetGroupControlEnable();
                    this.groupsListRowID = e.RowIndex;
                    this.SetGropTextBoxes(e.RowIndex);
                  ////  this.SetGroupNameWithLeftAlignment(this.GroupHeaderLabel);
                }
            }
        }

        /// <summary>
        /// Handles the CellClick event of the UserInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserInGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.userInRowID = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                if (!string.IsNullOrEmpty(this.UserInGrid.Rows[e.RowIndex].Cells["InUserID"].Value.ToString()))
                {
                    this.userInGridRow = e.RowIndex;
                }
                else
                {
                    this.SetDataGridViewPosition(this.UserInGrid, this.userInGridRow);
                }
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the UserNotInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserNotInGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.userOutRowID = e.RowIndex;
        }

        /// <summary>
        /// Groupses the required field.
        /// </summary>
        /// <returns>boolean</returns>
        private bool GroupsRequiredField()
        {
            if (this.GroupNameTextBox.Text.Trim().Length > 0 && this.GroupDescTextBox.Text.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the groups exist.
        /// </summary>
        /// <returns>Check if user User Exists Boolean</returns>
        private bool CheckGroupsExist()
        {
            bool userExist = true;
            DataGridViewRowCollection rowCollection = this.GroupTabGroupGrid.Rows;
            foreach (DataGridViewRow rows in rowCollection)
            {
                if (string.Compare(rowCollection[rows.Index].Cells["GroupListName"].Value.ToString().ToLower(), this.GroupNameTextBox.Text.Trim().ToLower()) == 0)
                {
                    userExist = false;
                    break;
                }
            }

            return userExist;
        }

        /// <summary>
        /// Handles the Click event of the GroupSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupSaveButton_Click(object sender, EventArgs e)
        {
            if (this.GroupSaveButton.Enabled)
            {
                this.SaveGroupDatas();
                this.groupButtonOpertion = (int)ButtonOperation.Empty;  
            }
        }

        /// <summary>
        /// Saves the group datas.
        /// </summary>
        private void SaveGroupDatas()
        {
            DataTable tempStoreUserId = new DataTable();
            //// Added By guhan.s on 23 jan 07
            this.GroupLinkLabel.Enabled = true;
            //// Till here
            bool groupSaved = false;
            DataRow tempUserIdDetails;
            UserManagementData tempDataSet = new UserManagementData();
            tempStoreUserId.Columns.AddRange(new DataColumn[] { new DataColumn("UserID", System.Type.GetType("System.Int32")) });
            if (this.GroupsRequiredField())
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    int saveGroupID;
                    this.userIDDataSet.Tables.Clear();
                    this.userIDDataSet.Merge(this.swampInDataSet.Tables[0]);
                    saveGroupID = this.GetGroupID();
                    for (int rowCount = 0; rowCount < this.userIDDataSet.Tables[0].Rows.Count; rowCount++)
                    {
                        if (!string.IsNullOrEmpty(this.userIDDataSet.Tables[0].Rows[rowCount]["UserID"].ToString()))
                        {
                            tempUserIdDetails = tempStoreUserId.NewRow();
                            tempUserIdDetails["UserID"] = this.userIDDataSet.Tables[0].Rows[rowCount]["UserID"].ToString();
                            tempStoreUserId.Rows.Add(tempUserIdDetails);
                        }
                    }

                    // this.userIDDataSet.Tables[0]

                    if (saveGroupID == 0)
                    {
                        if (this.CheckGroupsExist())
                        {
                            tempDataSet.GetErrorMessage.Merge(F9002WorkItem.InsertGroupDetails(saveGroupID, this.GroupNameTextBox.Text.Trim(), this.GroupDescTextBox.Text.Trim(), TerraScan.Utilities.Utility.GetXmlString(tempStoreUserId), TerraScanCommon.UserId));
                            groupSaved = true;
                            this.closingForm = true;
                        }
                        else
                        {
                            groupSaved = false;
                            this.closingForm = false;
                            MessageBox.Show(SharedFunctions.GetResourceString("AlreadyExist"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        tempDataSet.GetErrorMessage.Merge(F9002WorkItem.InsertGroupDetails(saveGroupID, this.GroupNameTextBox.Text.Trim(), this.GroupDescTextBox.Text.Trim(), TerraScan.Utilities.Utility.GetXmlString(tempStoreUserId), TerraScanCommon.UserId));
                        if (tempDataSet.GetErrorMessage.Rows[0][tempDataSet.GetErrorMessage.ErrorMsgColumn].ToString() != "1")
                        {
                            groupSaved = true;
                            this.closingForm = true;
                        }
                        else
                        {
                            groupSaved = false;
                            this.closingForm = false;
                            MessageBox.Show(SharedFunctions.GetResourceString("AlreadyExist"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    if (groupSaved)
                    {
                        this.LoadGropListDataGridView();
                        this.SetGroupButton(ButtonOperation.Save);

                        //// Enable Group Grid Sorting
                        ////this.EnableGroupGridSort();
                        this.SetGroupControlEnable();

                        /*if (this.groupsTotalRecordCount > 0)
                        {
                            if (this.groupButtonOpertion == (int)ButtonOperation.New)
                            {
                                this.SetDataGridViewPosition(this.GroupTabGroupGrid, this.groupsTotalRecordCount);
                                this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[this.groupsTotalRecordCount].Cells["GroupID"].Value.ToString());
                                this.groupButtonOpertion = (int)ButtonOperation.Empty;
                                this.GroupHeaderLabel.Text = this.GroupTabGroupGrid.Rows[this.groupsTotalRecordCount].Cells["GroupListName"].Value.ToString() + "  Group";
                                this.SetGropTextBoxes(this.groupsTotalRecordCount);
                                this.GroupTabGroupGrid.Focus();
                            }
                            else
                            {
                                this.SetDataGridViewPosition(this.GroupTabGroupGrid, this.tempGroupRowId);
                                this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[this.tempGroupRowId].Cells["GroupID"].Value.ToString());
                                this.GroupHeaderLabel.Text = this.GroupTabGroupGrid.Rows[this.tempGroupRowId].Cells["GroupListName"].Value.ToString() + "  Group";
                                this.SetGropTextBoxes(this.tempGroupRowId);
                                this.GroupTabGroupGrid.Focus();
                            }
                        }*/

                        this.changeInGroupDataSet = false;
                    }
                    else
                    {
                        this.GroupNameTextBox.Focus();
                    }

                    this.Cursor = Cursors.Default;
                }
                catch (Exception arException)
                {
                    ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                this.closingForm = false;
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the GroupNewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupNewButton_Click(object sender, EventArgs e)
        {
            if (this.GroupNewButton.Enabled)
            {
                //// added by guhan.s on 23 jan 07
                this.GroupLinkLabel.Enabled = false;
                this.GroupLinkLabel.Text = this.groupLink;
                ///// till here
               //// this.groupButtonOpertion = (int)ButtonOperation.New;
                this.groupsListRowID = 0;
                this.changeInGroupDataSet = true;
                /*if (this.swampInDataSet.Tables.Count > 0)
                //{
                //    this.swampInDataSet.Clear();
                //    this.swampInDataSet.Tables[0].Merge(CreateEmptyRows(this.swampInDataSet.Tables[0], 16));
                //}*/

                this.GroupHeaderLabel.Text = "  Group";
                DataRow[] userNotRows;
                this.stringExp = "GroupId <>" + -1;
                userNotRows = this.groupManagement.ListGroupDetail.Select(this.stringExp);

                this.swampOutDataSet.Tables.Clear();
                this.swampOutDataSet.Merge(userNotRows);
                /*Create an array of DataColumns to compare
                If these columns all match we consider the 
                rows duplicate.*/
                if (this.swampOutDataSet.Tables.Count > 0)
                {
                    DataColumn[] keyColumns = new DataColumn[] { this.swampOutDataSet.Tables[0].Columns["UserID"] };
                    RemoveDuplicates(ref this.swampOutDataSet, keyColumns);
                    //// this.swampOutDataSet.Tables[0].Merge(CreateEmptyRows(this.swampOutDataSet.Tables[0], 16));
                    this.SetUserNotInGridVScrollBar(this.swampOutDataSet.Tables[0].Rows.Count);
                    DataView sortedView = new DataView(this.swampOutDataSet.Tables[0]);
                    sortedView.Sort = "FullName ASC";
                    this.userNotIngrid.DataSource = sortedView;  ////this.swampOutDataSet.Tables[0];

                    TerraScanCommon.SetDataGridViewPosition(this.userNotIngrid, 0);
                    this.userNotIngrid.Enabled = true;
                }

                this.SetUserInGridVScrollBar(0);
                this.swampInDataSet.Clear();
                this.UserInGrid.DataSource = this.swampInDataSet.Tables[0];
                this.SetGroupButton(ButtonOperation.New);
                this.SetGroupControlEnable();
                this.ClearGroupControls();
                this.GroupNameTextBox.Focus();
                /* //this.UserInGrid.Rows[0].Selected = false;
                 //this.UserInGrid.CurrentCell = this.UserInGrid[0, 0];
                 //this.userNotIngrid.Rows[0].Selected = false;
                 //this.userNotIngrid.CurrentCell = this.userNotIngrid[0, 0];*/
                this.SetSwapButton(0, this.swampOutDataSet.Tables[0].Rows.Count);
                if (this.groupsTotalRecordCount > 0)
                {
                    this.GroupTabGroupGrid.Rows[0].Selected = false;
                    this.GroupTabGroupGrid.CurrentCell = this.GroupTabGroupGrid[0, 0];
                }

                //// this.GroupControlClearDataBinding();
                this.ClearGroupControls();
                this.groupButtonOpertion = (int)ButtonOperation.New;
            }
        }

        /// <summary>
        /// Clears the group controls.
        /// </summary>
        private void ClearGroupControls()
        {
            this.GroupNameTextBox.Text = string.Empty;
            this.GroupDescTextBox.Text = string.Empty;
            this.GroupIDTextBox.Text = string.Empty;
            this.UserManagementToolTip.RemoveAll();
        }

        /// <summary>
        /// Handles the Click event of the GroupDeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupDeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteGroup"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    this.groupButtonOpertion = (int)ButtonOperation.Delete;  
                    this.selectedGroupRow = this.GroupTabGroupGrid.CurrentRowIndex; 
                    this.Cursor = Cursors.WaitCursor;
                    this.groupID = this.GetGroupID();
                    F9002WorkItem.DeleteGroupDetails(this.groupID,TerraScanCommon.UserId);
                    this.keyPressed = false;
                    this.LoadGropListDataGridView();

                    if (this.groupsTotalRecordCount > 0)
                    {
                        if (this.selectedGroupRow < this.groupsTotalRecordCount)
                        {
                            this.SetDataGridViewPosition(this.GroupTabGroupGrid, this.selectedGroupRow);
                            this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[this.selectedGroupRow].Cells["GroupID"].Value.ToString());
                            this.groupsListRowID = this.selectedGroupRow;
                            this.tempGroupRowId = this.selectedGroupRow;
                        }
                        else if (this.selectedGroupRow == this.groupsTotalRecordCount)
                        {
                            this.SetDataGridViewPosition(this.GroupTabGroupGrid, 0);
                            this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[0].Cells["GroupID"].Value.ToString());
                            this.groupsListRowID = 0;
                            this.tempGroupRowId = 0;
                        }
                    }
                    else
                    {
                        this.SetGroupControlDisable();
                        this.ClearGroupControls();
                        this.GroupHeaderLabel.Text = "Group";
                    }

                    this.GroupTabGroupGrid.Focus();
                    this.Cursor = Cursors.Default;
                    this.groupButtonOpertion = (int) ButtonOperation.Empty;  
                }
                catch (Exception arException)
                {
                    ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                ////catch (Exception ex)
                ////{
                ////    this.Cursor = Cursors.Default;
                ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                ////}
            }
            else
            {
                this.GroupNameTextBox.Focus();
            }
        }

        /// <summary>
        /// Gets the group ID.
        /// </summary>
        /// <returns>returns the group ID</returns>
        private int GetGroupID()
        {
            int tempgroupID = 0;
            if (this.GroupIDTextBox.Text.Length > 0)
            {
                tempgroupID = Convert.ToInt32(this.GroupIDTextBox.Text.Trim());
            }
            else
            {
                tempgroupID = 0;
            }

            return tempgroupID;
        }

        /// <summary>
        /// Sets the user in grid V scroll bar.
        /// </summary>
        /// <param name="userINRowCount">The user IN row count.</param>
        private void SetUserInGridVScrollBar(int userINRowCount)
        {
            if (userINRowCount > 16)
            {
                this.UserInVscrollBar.Enabled = true;
                this.UserInVscrollBar.Visible = false;
            }
            else
            {
                this.UserInVscrollBar.BringToFront();
                this.UserInVscrollBar.Enabled = false;
                this.UserInVscrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Sets the user not in grid V scroll bar.
        /// </summary>
        /// <param name="userNotINRowCount">The user not IN row count.</param>
        private void SetUserNotInGridVScrollBar(int userNotINRowCount)
        {
            if (userNotINRowCount > this.userNotIngrid.NumRowsVisible)
            {
                this.UserNotInVScrollBar.Enabled = true;
                this.UserNotInVScrollBar.Visible = false;
            }
            else
            {
                this.UserNotInVScrollBar.BringToFront();
                this.UserNotInVScrollBar.Enabled = false;
                this.UserNotInVScrollBar.Visible = true;
            }
        }

        #endregion

        #region Coding for permissions Tab

        #region Methods

        /// <summary>
        /// Permissions the tab init.
        /// </summary>
        private void PermissionTabInit()
        {
            this.ParentForm.CancelButton = this.PermissionCancelButton;

            //// TODO : Coding For Permissions
            this.permissionsLabel.Text = this.UserManagementTab.SelectedTab.Text;
            if (this.permissionChanges != true)
            {
                this.SetPermissionButtons(PermissionButtonOperation.Empty);
                this.CustomisePermissionGroupsListGrid();
                this.SetPermissionButtonEvent();
                ///// Assing ContrlKey + Eevnet and Remove for other Tabs
                this.DisableUserInSorting();
                this.LoadPermissionsDataGrid();
            }
        }

        /// <summary>
        /// Sets the permission buttons.
        /// </summary>
        /// <param name="permissionButtonName">Name of the permission button.</param>
        private void SetPermissionButtons(PermissionButtonOperation permissionButtonName)
        {
            switch (permissionButtonName)
            {
                case PermissionButtonOperation.Empty:
                    {
                        this.PermissionSaveButton.Enabled = false;
                        this.PermissionCancelButton.Enabled = false;
                        break;
                    }

                case PermissionButtonOperation.Save:
                    {
                        this.PermissionSaveButton.Enabled = true;
                        this.PermissionCancelButton.Enabled = true;
                        break;
                    }

                case PermissionButtonOperation.Cancel:
                    {
                        this.PermissionSaveButton.Enabled = true;
                        this.PermissionCancelButton.Enabled = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Loads the permissions data grid.
        /// </summary>
        private void LoadPermissionsDataGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.permissionManagement = this.form9002Control.WorkItem.GetGroupPermissionDetails; ////F9002WorkItem.GetGroupPermissionDetails;
                this.Cursor = Cursors.Default;
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            ////catch (Exception ex)
            ////{
            ////    this.Cursor = Cursors.Default;
            ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            ////}

            
            ///// this.validPermissionDataset = this.CheckValidRowCount(this.permissionManagement);
            this.CustomisePermissionGroupsListGrid();

            if (this.permissionManagement != null)
            {
                this.GroupsDataGridView.Enabled = true;
                this.PermissionsDataGridView.Enabled = true;

                if (this.permissionManagement.ListPermissionGroupDetail.Rows.Count > this.GroupsDataGridView.NumRowsVisible)
                {
                    this.PermissionVScroll.Enabled = true;
                    this.PermissionVScroll.Visible = false;
                }
                else
                {
                    this.PermissionVScroll.BringToFront();
                    this.PermissionVScroll.Enabled = false;
                    this.PermissionVScroll.Visible = true;
                }

                this.permissionCount = this.permissionManagement.ListPermissionGroupDetail.Rows.Count;
                this.GroupsDataGridView.DataSource = this.permissionManagement.ListPermissionGroupDetail;
                this.SetDataGridViewPosition(this.GroupsDataGridView, 0);
                this.permissionsCm = (CurrencyManager)this.BindingContext[this.permissionManagement.ListPermissionGroupDetail];
                if (this.permissionsCm != null && this.permissionGridRowIndex >= 0)
                {
                    this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsId"].Value.ToString());
                    this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                 ////   this.SetGroupNameWithLeftAlignment(this.PermissionHeaderLable);
                }
            }
            else
            {
                //// No Record Disable
                this.GroupsDataGridView.Enabled = false;
                this.PermissionsDataGridView.Enabled = false;
            }

            this.CustomisePermissionGroupsListGrid();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Customises the permission groups list grid.
        /// </summary>
        private void CustomisePermissionGroupsListGrid()
        {
            this.GroupsDataGridView.AllowUserToResizeColumns = false;
            this.GroupsDataGridView.EnableHeadersVisualStyles = false;
            this.GroupsDataGridView.AutoGenerateColumns = false;
            this.GroupsDataGridView.AllowUserToResizeRows = false;
            this.GroupsDataGridView.StandardTab = true;
            DataGridViewColumnCollection columns = this.GroupsDataGridView.Columns;
            columns["GroupsID"].DataPropertyName = "GroupsID";
            columns["PermissionsGroupsName"].DataPropertyName = "GroupName";
            columns["GroupsID"].DisplayIndex = 0;
            columns["PermissionsGroupsName"].DisplayIndex = 1;
            ////this.commentHeader.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ////this.commentHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            ////this.commentHeader.BackColor = System.Drawing.Color.FromArgb(132, 130, 132);
            ////this.commentHeader.ForeColor = System.Drawing.Color.WhiteSmoke;

            ////this.GroupsDataGridView.ColumnHeadersDefaultCellStyle = this.commentHeader;

            this.PermissionsDataGridView.AllowUserToResizeColumns = false;
            this.PermissionsDataGridView.EnableHeadersVisualStyles = false;
            this.PermissionsDataGridView.AutoGenerateColumns = false;
            this.PermissionsDataGridView.AllowUserToResizeRows = false;
            this.PermissionsDataGridView.EnableHeadersVisualStyles = false;
            this.PermissionsDataGridView.AllowUserToResizeColumns = false;
            this.PermissionsDataGridView.AutoGenerateColumns = false;
            this.PermissionsDataGridView.AllowUserToResizeRows = false;
            this.PermissionsDataGridView.StandardTab = true;
            DataGridViewColumnCollection permissionColumns = this.PermissionsDataGridView.Columns;
            permissionColumns["GridEntry"].DataPropertyName = "FormName";
            permissionColumns["FormID"].DataPropertyName = "FormID";
            permissionColumns["isPermissionMenu"].DataPropertyName = "isPermissionMenu";
            permissionColumns["isPermissionOpen"].DataPropertyName = "isPermissionOpen";
            permissionColumns["isPermissionAdd"].DataPropertyName = "isPermissionAdd";
            permissionColumns["isPermissionEdit"].DataPropertyName = "isPermissionEdit";
            permissionColumns["isPermissionDelete"].DataPropertyName = "isPermissionDelete";
            permissionColumns["FormID"].DisplayIndex = 0;
            permissionColumns["GridEntry"].DisplayIndex = 1;
            permissionColumns["isPermissionMenu"].DisplayIndex = 2;
            permissionColumns["isPermissionOpen"].DisplayIndex = 3;
            permissionColumns["isPermissionAdd"].DisplayIndex = 4;
            permissionColumns["isPermissionEdit"].DisplayIndex = 5;
            permissionColumns["isPermissionDelete"].DisplayIndex = 6;

            ////  this.PermissionsDataGridView.ColumnHeadersDefaultCellStyle = this.commentHeader;

            ///// this.PermissionsDataGridView.ColumnHeadersDefaultCellStyle = this.commentHeader;
        }

        /// <summary>
        /// Loads the permission.
        /// </summary>
        /// <param name="groupsID">The groups ID.</param>
        private void LoadPermission(string groupsID)
        {
            if ((this.permissionManagement != null) && (this.permissionsCm != null) && (this.permissionGridRowIndex >= 0) && (!this.permissionChanges) && this.permissionCount > 0)
            {
                DataRow[] permissionRows;
                this.PermissionsDataGridView.DataSource = null;
                this.stringExp = "GroupsID = " + groupsID;
                permissionRows = this.permissionManagement.ListPermissionDetail.Select(this.stringExp);
                this.permissionSelected.Tables.Clear();
                this.permissionSelected.Merge(permissionRows);
                if (groupsID != this.emptyKeyID)
                {
                    if (this.permissionSelected.Tables.Count > 0)
                    {
                        this.PermissionsDataGridView.Enabled = true;
                        //// this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                        DataView permissionSorted = new DataView(this.permissionSelected.Tables[0]);
                        permissionSorted.Sort = "FormName ASC";
                        this.permissionInSortDataTable = permissionSorted.ToTable();
                        this.permissionSelected.Clear();
                        this.permissionSelected.Tables[0].Merge(this.permissionInSortDataTable);

                        if (this.permissionSelected.Tables[0].Rows.Count > this.PermissionsDataGridView.NumRowsVisible)
                        {
                            this.PermissionAssignVScrollBar.Enabled = true;
                            this.PermissionAssignVScrollBar.Visible = false;
                        }
                        else
                        {
                            this.PermissionAssignVScrollBar.BringToFront();
                            this.PermissionAssignVScrollBar.Enabled = false;
                            this.PermissionAssignVScrollBar.Visible = true;
                        }

                        this.PermissionsDataGridView.DataSource = this.permissionSelected.Tables[0];
                    }
                }
                else
                {
                    if (this.permissionSelected.Tables[0].Rows.Count > 16)
                    {
                        this.PermissionAssignVScrollBar.Enabled = true;
                        this.PermissionAssignVScrollBar.Visible = false;
                    }
                    else
                    {
                        this.PermissionAssignVScrollBar.BringToFront();
                        this.PermissionAssignVScrollBar.Enabled = false;
                        this.PermissionAssignVScrollBar.Visible = true;
                    }

                    this.PermissionsDataGridView.DataSource = this.permissionSelected.Tables[0];
                    this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                    ////this.SetGroupNameWithLeftAlignment(this.PermissionHeaderLable);
                    this.PermissionsDataGridView.Enabled = false;
                }
            }
            else
            {
                this.PermissionsDataGridView.DataSource = null;
                this.PermissionAssignVScrollBar.BringToFront();
                this.PermissionAssignVScrollBar.Enabled = false;
                this.PermissionAssignVScrollBar.Visible = true;
            }
        }

        #endregion

        /// <summary>
        /// Handles the CellContentClick event of the GroupsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!string.IsNullOrEmpty(this.GroupsDataGridView.Rows[e.RowIndex].Cells["GroupsID"].Value.ToString()))
                {
                    ////if (this.validPermissionDataset && this.permissionsCm != null && this.permissionGridRowIndex >= 0)
                    ////{
                    if (this.permissionChanges)
                    {
                        if (this.permissionPreviousRowId != e.RowIndex)
                        {
                            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        //// Changes in UserGrid

                                        this.SavePermissionData();
                                        if (this.permissionsCm != null && this.permissionGridRowIndex >= 0)
                                        {
                                            this.permissionGridRowIndex = e.RowIndex;
                                        }
                                        //// changed dataset to datagrid
                                        this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                                        this.permissionChanges = false;
                                        this.GroupsDataGridView.Rows[e.RowIndex].Selected = true;
                                        this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, e.RowIndex];
                                        this.permissionPreviousRowId = e.RowIndex;
                                        //// this enable sort
                                        this.EnablePermissionGroupGridSorting();
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.SetPermissionButtons(PermissionButtonOperation.Empty);
                                        this.permissionSelected.RejectChanges();
                                        //// changed dataset to datagrid
                                        /////this.LoadPermission(this.GroupsDataGridView.Rows[e.RowIndex].Cells["GroupsID"].Value.ToString());
                                        this.permissionChanges = false;
                                        this.GroupsDataGridView.Rows[e.RowIndex].Selected = true;
                                        this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, e.RowIndex];
                                        this.permissionPreviousRowId = e.RowIndex;
                                        //// this enable sort
                                        this.EnablePermissionGroupGridSorting();
                                        this.LoadPermission(this.GroupsDataGridView.Rows[e.RowIndex].Cells["GroupsID"].Value.ToString());
                                        this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[e.RowIndex].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                                       //// this.SetGroupNameWithLeftAlignment(this.PermissionHeaderLable); 
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        this.SetPermissionButtons(PermissionButtonOperation.Cancel);

                                        //// changed dataset to datagrid
                                        ////this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionPreviousRowId].Cells["GroupsID"].Value.ToString());
                                        this.permissionChanges = true;
                                        ////this.GroupsDataGridView.Rows[Convert.ToInt32(this.permissionPreviousRowId)].Selected = true;
                                        /////this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, Convert.ToInt32(this.permissionPreviousRowId)];
                                        if (e.ColumnIndex >= 0)
                                        {
                                            this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[this.permissionColumnIndex, this.permissionPreviousRowId];
                                        }
                                        else
                                        {
                                            this.SetDataGridViewPosition(this.GroupsDataGridView, this.permissionPreviousRowId);
                                        }
                                        //// this enable sort
                                        break;
                                    }
                            }
                        }

                        /*    if (MessageBox.Show(SharedFunctions.GetResourceString("DiscardUser"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.SetPermissionButtons(PermissionButtonOperation.Cancel);
                                this.permissionSelected.RejectChanges();

                                if (this.permissionsCm != null && this.permissionGridRowIndex >= 0)
                                {
                                    this.permissionGridRowIndex = this.permissionGridRowIndex;
                                }
                                //// changed dataset to datagrid
                                this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                                this.permissionChanges = false;
                                this.GroupsDataGridView.Rows[Convert.ToInt32(e.RowIndex)].Selected = true;
                                this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, Convert.ToInt32(e.RowIndex)];
                                this.permissionPreviousRowId = e.RowIndex;
                                //// this enable sort
                                this.EnablePermissionGridSort();
                            }
                            else
                            {
                                this.GroupsDataGridView.Rows[Convert.ToInt32(this.permissionGridRowIndex)].Selected = true;
                                this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, Convert.ToInt32(this.permissionGridRowIndex)];
                                ////  this.permissionPreviousRowId = e.RowIndex;
                            }
                        }*/
                    }
                    else
                    {
                        this.permissionGridRowIndex = e.RowIndex;
                        this.permissionPreviousRowId = e.RowIndex;

                        if (e.ColumnIndex >= 0)
                        {
                            this.permissionColumnIndex = e.ColumnIndex;
                        }
                        else
                        {
                            this.permissionColumnIndex = 0;
                        }

                        this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsId"].Value.ToString());

                        this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                       //// this.SetGroupNameWithLeftAlignment(this.PermissionHeaderLable); 
                    }
                }
                ////}
                else
                {   
                    this.LoadPermission(this.GroupsDataGridView.Rows[0].Cells["GroupsId"].Value.ToString());
                    this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[0].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                   //// this.SetGroupNameWithLeftAlignment(this.PermissionHeaderLable); 
                    this.SetDataGridViewPosition(this.GroupsDataGridView, 0);
                }
            }
        }

        /// <summary>
        /// Sets the color of the data grid link fore.
        /// </summary>
        /// <param name="linkRowId">The link row id.</param>
        private void SetDataGridLinkForeColor(int linkRowId)
        {
            foreach (DataGridViewRow forecolorRow in this.GroupDataGrid.Rows)
            {
                if (forecolorRow.Index == linkRowId)
                {
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.White;
                }
                else
                {
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Blue;
                }
            }
        }

        /// <summary>
        /// Sets the color of the data grid link fore.
        /// </summary>
        /// <param name="linkRowId">The link row id.</param>
        /// <param name="linkColumnId">The link column id.</param>
        private void SetDataGridLinkForeColor(int linkRowId, int linkColumnId)
        {
            foreach (DataGridViewRow forecolorRow in this.GroupDataGrid.Rows)
            {
                if (forecolorRow.Index == linkRowId)
                {
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.White;
                    if (linkColumnId == 0)
                    {
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                    }
                    else if (linkColumnId == 1)
                    {
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                    }
                    else if (linkColumnId == -1)
                    {
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                    ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Blue;
                }
            }
        }

        /// <summary>
        /// Sets the color of the data grid link fore.
        /// </summary>
        /// <param name="linkRowId">The link row id.</param>
        /// <param name="linkColumnId">The link column id.</param>
        private void SetDataGridLinkForeColor1(int linkRowId, int linkColumnId)
        {
            try
            {
                foreach (DataGridViewRow forecolorRow in this.GroupDataGrid.Rows)
                {
                    if (forecolorRow.Index == linkRowId)
                    {
                        if (linkColumnId == 0)
                        {
                            if (this.GroupDataGrid.Rows[linkRowId].Selected)
                            {
                                ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                                ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                                ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                            }
                        }
                        else if (linkColumnId == 1)
                        {
                            ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                            ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Blue;
                    }
                }
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the color of the data grid link fore.
        /// </summary>
        /// <param name="linkRowId">The link row id.</param>
        /// <param name="linkColumnId">The link column id.</param>
        private void SetDataGridLinkForeColorCellEnter(int linkRowId, int linkColumnId)
        {
            try
            {
                foreach (DataGridViewRow forecolorRow in this.GroupDataGrid.Rows)
                {
                    if (forecolorRow.Index == linkRowId)
                    {
                        if (linkColumnId == 0)
                        {
                            ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                            ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                        }
                        else if (linkColumnId == 1)
                        {
                            ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).LinkColor = System.Drawing.Color.White;
                            ((DataGridViewLinkCell)GroupDataGrid.Rows[linkRowId].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Blue;
                    }
                }
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //// Only Row With USerID Shoud Execute Below
                if (!string.IsNullOrEmpty(this.GroupDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
                {
                    //////// this.SetDataGridLinkForeColor(e.RowIndex, e.ColumnIndex);
                    this.userGroupListingRow = e.RowIndex;
                }
                else
                {
                    this.SetDataGridViewPosition(this.GroupDataGrid, this.userGroupListingRow);
                }
            }
            else
            {
                //// this.GroupDataGrid.CurrentCell = GroupDataGrid[e.RowIndex, 0];
                //////// this.SetDataGridLinkForeColor(e.RowIndex, e.ColumnIndex);
            }
        }

        /// <summary>
        /// Sets the group tab.
        /// </summary>
        /// <param name="gridRowNo">The grid row no.</param>
        private void SetGroupTab(int gridRowNo)
        {
            this.UserManagementTab.SelectedIndex = 1;
            this.SetGroupDetailsGridView(this.GroupDataGrid.Rows[gridRowNo].Cells["ID"].Value.ToString());
        }

        /// <summary>
        /// Sets the group details grid view.
        /// </summary>
        /// <param name="setGroupID">The set group ID.</param>
        private void SetGroupDetailsGridView(string setGroupID)
        {
            int setGridRow = 0;
            foreach (DataRow row in this.groupManagement.ListGroupsGroupDetail.Rows)
            {
                if (setGroupID == row["groupID"].ToString())
                {
                    setGridRow = setGridRow + 1;
                    break;
                }
                else
                {
                    setGridRow = setGridRow + 1;
                }
            }

            if (setGridRow > 0)
            {
                setGridRow--;
            }

            this.tempGroupRowId = setGridRow;
            this.groupsListRowID = setGridRow;
            this.SetDataGridViewPosition(this.GroupTabGroupGrid, setGridRow);
            this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[setGridRow].Cells["GroupID"].Value.ToString());
        }

        /// <summary>
        /// Handles the CellContentClick event of the PermissionsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PermissionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex >= 0 && e.ColumnIndex > 1)
            {
                if (!string.IsNullOrEmpty(this.PermissionsDataGridView.Rows[e.RowIndex].Cells["GridEntry"].Value.ToString()))
                {
                    this.SetPermissionButtons(PermissionButtonOperation.Save);
                    this.permissionChanges = true;
                    //// Disable sort evet in data grid
                    this.DisablePermissionGridSorting();
                }
            }

            this.ParentForm.CancelButton = this.PermissionCancelButton;
            this.PermissionCancelButton.Focus();
            this.PermissionsDataGridView.Focus();
        }

        /// <summary>
        /// Handles the Click event of the PermissionSaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PermissionSaveButton_Click(object sender, EventArgs e)
        {
            this.PermissionSaveButton.Focus();
            this.SavePermissionData();
        }

        /// <summary>
        /// Saves the permission data.
        /// </summary>
        private void SavePermissionData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////Update The The User Details 
                F9002WorkItem.SaveGroupPermissionDetails(Convert.ToInt32(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value), TerraScan.Utilities.Utility.GetXmlString(this.permissionSelected.Tables[0]), TerraScanCommon.UserId);
                this.EnablePermissionGroupGridSorting();
                this.SetPermissionButtons(PermissionButtonOperation.Empty);
                this.LoadPermissionsDataGrid();
                this.closingForm = true;
                this.permissionChanges = false;
                this.SetDataGridViewPosition(this.GroupsDataGridView, this.permissionGridRowIndex);
                if (this.permissionsCm != null && this.permissionGridRowIndex >= 0)
                {
                    this.LoadPermission(this.permissionManagement.Tables[0].Rows[this.permissionGridRowIndex]["GroupsId"].ToString());
                }

                //// this enable sort

                this.Cursor = Cursors.Default;
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            ////catch (Exception ex)
            ////{
            ////    this.Cursor = Cursors.Default;
            ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            ////}
        }

        /// <summary>
        /// Handles the Click event of the PermissionCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PermissionCancelButton_Click(object sender, EventArgs e)
        {
            if (this.permissionChanges)
            {  
                if (this.permissionCount > 0)
                {
                    this.permissionGridRowIndex = this.GroupsDataGridView.CurrentRow.Index; 
                    this.permissionSelected.RejectChanges();
                    this.permissionChanges = false;
                    if (this.permissionCount == this.permissionGridRowIndex)
                    {
                        this.permissionGridRowIndex = 0;
                    }

                    this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                    this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Selected = true;
                    this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, this.permissionGridRowIndex];
                    this.permissionPreviousRowId = this.permissionGridRowIndex;
                    this.SetPermissionButtons(PermissionButtonOperation.Empty);
                    //// this enable sort
                    this.EnablePermissionGroupGridSorting();
                }
            }
        }

        /// <summary>
        /// Handles the Leave event of the EmailTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EmailTextBox_Leave(object sender, EventArgs e)
        {
            if (this.EmailTextBox.Text.Trim().Length > 0)
            {
                if (!Regex.IsMatch(this.EmailTextBox.Text.ToLower(), this.validEmail))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("EmailValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.EmailTextBox.Focus();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the GroupCancelbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupCancelbutton_Click(object sender, EventArgs e)
        {
            int selectedGropRow;
            selectedGropRow = this.GroupTabGroupGrid.CurrentRow.Index;  
            /*
            if(MessageBox.Show(SharedFunctions.GetResourceString("DiscardUser"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.SetGroupButton(ButtonOperation.Cancel);
                this.SetGroupControlDisable();
                this.GroupTabGroupGrid.Enabled = true;
                this.groupManagement.RejectChanges();
                this.changeInGroupDataSet = false;
                this.LoadGropListDataGridView();
                this.groupButtonOpertion = (int)ButtonOperation.Empty;

                //// Enable Group Grid
                this.EnableGroupGridSort();
                if (this.groupsListRowID >= 0)
                {
                    if (this.GroupTabGroupGrid.Rows.Count > 0)
                    {
                        this.SetDataGridViewPosition(this.GroupTabGroupGrid, 0);
                        if (!string.IsNullOrEmpty(this.GroupTabGroupGrid.Rows[0].Cells["GroupID"].Value.ToString()))
                        {
                            this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[0].Cells["GroupID"].Value.ToString());
                        }
                    }
                    //// this.SetGropTextBoxes(this.groupsListRowID);
                }

                this.GroupNewButton.Focus();
            }
            else
            {
                this.GroupNameTextBox.Focus();
            }
             */
            this.GroupLinkLabel.Enabled = true;

            this.SetGroupButton(ButtonOperation.Cancel);
            this.SetGroupControlDisable();
            this.GroupTabGroupGrid.Enabled = true;
            this.groupManagement.RejectChanges();

            this.EnableGroupGridSorting();
            this.LoadGropListDataGridView();
            this.groupButtonOpertion = (int)ButtonOperation.Empty;
            this.changeInGroupDataSet = false;
            //// Enable Group Grid
            ////this.EnableGroupGridSort();

            ////if (this.groupsListRowID >= 0)
            ////{
                if (this.GroupTabGroupGrid.Rows.Count > 0)
                {
                    this.SetDataGridViewPosition(this.GroupTabGroupGrid, selectedGropRow);
                    if (!string.IsNullOrEmpty(this.GroupTabGroupGrid.Rows[selectedGropRow].Cells["GroupID"].Value.ToString()))
                    {
                        this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[selectedGropRow].Cells["GroupID"].Value.ToString());
                    }
                }
            ////}
        }

        /// <summary>
        /// Handles the Click event of the ActiveList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ActiveList_Click(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab)
            {
                this.keyPressed = true;
                this.SetButton(ButtonOperation.Update);
                this.DisableUserGridSorting();
            }
        }

        /// <summary>
        /// Handles the Click event of the AdminList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AdminList_Click(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab)
            {
                this.keyPressed = true;
                this.SetButton(ButtonOperation.Update);
                this.DisableUserGridSorting();
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AdminList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AdminList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab)
            {
                this.keyPressed = true;
                this.SetButton(ButtonOperation.Update);
                this.DisableUserGridSorting();
                /*  //if (this.userGridRowIndex >= 0)
                  //{
                  //  ////  this.UserGridView.Rows[this.userGridRowIndex].Cells["IsAdministrator"].Value = this.AdminList.SelectedItem;
                  //}*/
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ActiveList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ActiveList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab)
            {
                this.keyPressed = true;
                this.SetButton(ButtonOperation.Update);
                this.DisableUserGridSorting();

                /*  //if (this.userGridRowIndex >= 0)
                  //{
                  //   //// this.UserGridView.Rows[this.userGridRowIndex].Cells["IsActive"].Value = this.ActiveList.SelectedItem;
                  //}*/
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the UserInButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserInButton_Click(object sender, EventArgs e)
        {
            //// Added By Guhan 
            //// Converted Whole Code into a method
            this.UserMoveIn();
        }

        /// <summary>
        /// Users the move in.
        /// </summary>
        private void UserMoveIn()
        {
            if (this.groupValidRow)
            {
                ArrayList selectedUser = new ArrayList();
                DataGridViewRowCollection rowCollection = this.userNotIngrid.Rows;
                int tempRow = 0;
                string tempUserID = string.Empty;
                this.DisableGroupGridSorting();
                foreach (DataGridViewRow rows in rowCollection)
                {
                    tempUserID = rows.Cells["OutUserId"].Value.ToString();
                    if (!string.IsNullOrEmpty(tempUserID))
                    {
                        if (rows.Cells[0].Selected)
                        {
                            selectedUser.Add(tempUserID);
                        }
                    }
                }

                if (selectedUser.Count > 0)
                {
                    //// Calling Function to push the selected user to  userin grid
                    this.SwapUsers("UserIn", selectedUser);

                    //// disable Group DataGrid Sort 
                    ////this.DisableGroupGridSort();

                    this.SetGroupButton(ButtonOperation.Update);
                    tempRow = SetSwapDataGridRowPosition(this.swampOutDataSet);

                    //// SET UserIN GRid Position 

                    if (selectedUser.Count == 1)
                    {
                        if (tempRow > 0)
                        {
                            if (tempRow == this.userOutRowID || tempRow < this.userOutRowID)
                            {
                                this.userOutRowID = tempRow - 1;
                            }

                            this.SetDataGridViewPosition(this.userNotIngrid, this.userOutRowID);
                        }
                        else
                        {
                            //// this.userNotIngrid.Rows[0].Selected = false;
                        }

                        /* //if (tempRow > 0)
                         //{
                         //    if (tempRow == this.userOutRowID || tempRow < this.userOutRowID)
                         //    {
                         //        this.userOutRowID = tempRow - 1;
                         //    }

                         //    this.SetDataGridViewPosition(this.userNotIngrid, this.userOutRowID);
                         //}
                         //else
                         //{
                         //    this.userNotIngrid.Rows[0].Selected = false;
                         //}*/
                    }
                    else
                    {
                        this.SetDataGridViewPosition(this.userNotIngrid, tempRow - 1);
                    }

                    if (this.userInCount > 0)
                    {
                        this.userInRowID = this.userInCount - 1;
                        this.SetDataGridViewPosition(this.UserInGrid, this.userInCount - 1);
                        this.UserInGrid.Enabled = true;
                    }

                    selectedUser.Clear();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the UserOutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserOutButton_Click(object sender, EventArgs e)
        {
            //// Added By Guhan on 23 jan 06
            //// Converted into one method 
            this.UserMoveOut();
        }

        /// <summary>
        /// Users the move out.
        /// </summary>
        private void UserMoveOut()
        {
            if (this.groupValidRow)
            {
                ArrayList selectedUser = new ArrayList();
                selectedUser.Clear();
                int tempRow = 0;
                string tempUserID;
                this.DisableGroupGridSorting();
                DataGridViewRowCollection rowCollection = this.UserInGrid.Rows;
                foreach (DataGridViewRow rows in rowCollection)
                {
                    tempUserID = rows.Cells["InUserID"].Value.ToString();

                    if (rows.Cells[0].Selected)
                    {
                        if (!string.IsNullOrEmpty(tempUserID))
                        {
                            selectedUser.Add(tempUserID);
                        }
                    }
                }

                if (selectedUser.Count > 0)
                {
                    this.SwapUsers("UserOut", selectedUser);

                    //// disable Group DataGrid Sort 
                    ////this.DisableGroupGridSort();

                    //// Used To Set The DataGrid Poistion For this.UserInGrid.
                    tempRow = SetSwapDataGridRowPosition(this.swampInDataSet);

                    if (selectedUser.Count == 1)
                    {
                        if (tempRow > 0)
                        {
                            if (tempRow == this.userInRowID || tempRow < this.userInRowID)
                            {
                                this.userInRowID = tempRow - 1;
                            }

                            this.SetDataGridViewPosition(this.UserInGrid, this.userInRowID);
                        }
                        else
                        {
                            ////// this.UserInGrid.Rows[0].Selected = false;
                        }
                    }
                    else
                    {
                        this.SetDataGridViewPosition(this.UserInGrid, tempRow - 1);
                    }

                    selectedUser.Clear();
                    if (this.userNotInRowCount > 0)
                    {
                        this.SetDataGridViewPosition(this.userNotIngrid, this.userNotInRowCount - 1);
                    }

                    this.SetGroupButton(ButtonOperation.Update);
                }
            }
        }

        /// <summary>
        /// Handles the Leave event of the NetNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NetNameTextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.NetNameTextBox.Text))
            {
                if (CheckValidDomainName(this.NetNameTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidNetName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.NetNameTextBox.Focus();
                }
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!string.IsNullOrEmpty(this.GroupDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
                {
                    if (e.ColumnIndex == 1)
                    {
                        this.GroupHeaderLabel.Text = this.GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"].Value.ToString() + "  Group";
                        this.SetGroupTab(e.RowIndex);
                       //// this.SetGroupNameWithLeftAlignment(this.GroupHeaderLabel);  
                    }
                }

                foreach (DataGridViewRow forecolorRow in this.GroupDataGrid.Rows)
                {
                    if (forecolorRow.Index == e.RowIndex)
                    {
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).LinkColor = System.Drawing.Color.Blue;
                        ((DataGridViewLinkCell)GroupDataGrid.Rows[forecolorRow.Index].Cells["GroupName"]).VisitedLinkColor = System.Drawing.Color.Blue;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the UserGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.userGridClick = true;
            if (this.keyPressed)
            {
                ////  this.UserSaveButton.Focus();
            }
            else
            {
                if (!string.IsNullOrEmpty(this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString()) && this.buttonOperation != (int)ButtonOperation.New)
                {
                    if (this.buttonOperation != (int)ButtonOperation.Delete)
                    {
                        this.userGridRowIndex = e.RowIndex;
                    }

                    this.tempRowId = e.RowIndex;
                    if (e.ColumnIndex >= 0)
                    {
                        this.userClickColumnId = e.ColumnIndex;
                    }
                    else
                    {
                        this.userClickColumnId = 0;
                    }

                    this.findUserID = this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString();
                    this.LoadGroupDataGridView(this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString(), "Group");
                    this.userUpdateRecord = e.RowIndex;
                    this.userGridEmptyRow = false;
                    this.SetUserFormTextBoxes(this.userUpdateRecord);
                    this.SetButton(ButtonOperation.Empty);
                    this.EnableControl();
                }
                else if (this.buttonOperation != (int)ButtonOperation.New)
                {
                    this.userGridEmptyRow = true;
                    this.SetUserFormTextBoxes(e.RowIndex);
                    this.DisableGroupDataGrid();
                    if (this.buttonOperation == (int)ButtonOperation.FromOtherTab)
                    {
                        this.SetButton(ButtonOperation.Empty);
                    }
                    else
                    {
                        this.SetButton(ButtonOperation.InValidRow);
                       // this.UserDeleteButton.Enabled = false;
                    }

                    this.DisableControl();
                    this.AdminList.SelectedIndex = 0;
                    this.ActiveList.SelectedIndex = 1;
                    this.AppraiserComboBox.SelectedIndex = 0;
                   
                }
            }

            this.userGridClick = false;
        }

        /// <summary>
        /// Handles the RowEnter event of the GroupTabGroupGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDetailsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.groupGridClick = true;
            if (e.RowIndex >= 0)
            {
                if (!this.changeInGroupDataSet)
                {
                    this.tempGroupRowId = e.RowIndex;
                    if (!string.IsNullOrEmpty(this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupID"].Value.ToString()) && this.groupButtonOpertion != (int)ButtonOperation.New)
                    {
                        this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupID"].Value.ToString());
                        this.GroupHeaderLabel.Text = this.GroupTabGroupGrid.Rows[e.RowIndex].Cells["GroupListName"].Value.ToString() + "  Group";
                        this.SetGropTextBoxes(e.RowIndex);
                        this.SetGroupButton(ButtonOperation.Empty);
                        this.SetGroupControlEnable();
                        this.changeInGroupDataSet = false;
                        this.groupValidRow = true;
                       //// this.SetGroupNameWithLeftAlignment(this.GroupHeaderLabel);  
                    }
                    else if (this.groupButtonOpertion != (int)ButtonOperation.New)
                    {
                        this.groupValidRow = false;
                        this.FindUserFromGroupList(this.emptyKeyID);
                        this.UserInGrid.Rows[0].Selected = false;
                        this.userNotIngrid.Rows[0].Selected = false;
                        this.SetGroupButton(ButtonOperation.InValidRow);
                        this.SetGropTextBoxes(e.RowIndex);
                        this.SetGroupControlDisable();
                    }
                }
                else
                {
                    ////  this.tempGroupRowId = e.RowIndex; 
                }
            }

            this.groupGridClick = false;
        }

        /// <summary>
        /// Handles the RowEnter event of the GroupsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.permissionChanges)
                {
                    ////this.PermissionsDataGridView.Focus();
                }
                else
                {
                    this.permissionGridRowIndex = e.RowIndex;
                    ////this.permissionsCm.Position = this.permissionGridRowIndex;
                    this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                    this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                   //// this.SetGroupNameWithLeftAlignment(this.PermissionHeaderLable); 
                }
                ////}
                ////else
                ////{
                ////    this.permissionGridRowIndex = e.RowIndex;
                ////    this.permissionGridRowIndex = this.permissionGridRowIndex;
                ////    this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                ////    this.PermissionHeaderLable.Text = this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["PermissionsGroupsName"].Value.ToString() + " Group";
                //// }               
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the NetNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NetNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.Update && this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab && this.formLoaded)
            {
                if (this.keyPressed)
                {
                    this.keyPressed = true;
                    this.SetButton(ButtonOperation.Update);
                    this.DisableUserGridSorting();
                }
                ////else
                ////{
                ////    this.SetButton(ButtonOperation.Update);
                ////    this.DisableUserGridSorting();
                ////}
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the GroupNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GroupNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.changeInGroupDataSet && this.groupLoaded && !this.GroupTabGroupGrid.IsSorted && !this.groupGridClick)
            {
                this.changeInGroupDataSet = true;
                this.SetGroupButton(ButtonOperation.Update);
                this.DisableUserGridSorting();
            }

            this.GroupTabGroupGrid.IsSorted = false;
            this.groupLoaded = true;
        }

        /// <summary>
        /// Handles the KeyDown event of the UserGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void UserGridView_KeyDown(object sender, KeyEventArgs e)
        {
            this.userGridRowIndex = ((DataGridView)sender).CurrentCell.RowIndex;
            if (this.userGridEmptyRow)
            {
                ////if (e.KeyCode.ToString() == "Down")
                ////{   
                ////    this.SetGroupButton(ButtonOperation.InValidRow);
                ////    e.Handled = true;
                ////}
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GroupDataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString()))
            {
                //////// this.SetDataGridLinkForeColor(e.RowIndex);
                ////  this.userGroupGridEmptyRow = false;
            }
            else
            {
                ////  this.userGroupGridEmptyRow = true;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the UserNotInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserNotInGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!string.IsNullOrEmpty(this.userNotIngrid.Rows[e.RowIndex].Cells["OutUserId"].Value.ToString()))
                {
                    this.userNotInGridRow = e.RowIndex;
                    this.userOutRowID = e.RowIndex;
                }
                else
                {
                    this.SetDataGridViewPosition(this.userNotIngrid, this.userNotInGridRow);
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the UserInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void UserInGrid_KeyDown(object sender, KeyEventArgs e)
        {
            ////if (this.userGridInRowCount)
            ////{
            ////    switch (e.KeyCode)
            ////    {
            ////        case Keys.Down:
            ////            {
            ////                e.Handled = true;
            ////                break;
            ////            }
            ////    }
            ////}
        }

        /// <summary>
        /// Handles the KeyDown event of the UserNotInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void UserNotInGrid_KeyDown(object sender, KeyEventArgs e)
        {
            ////if (this.userNotInGridEmptyRow)
            ////{
            ////    switch (e.KeyCode)
            ////    {
            ////        case Keys.Down:
            ////            {
            ////                e.Handled = true;
            ////                break;
            ////            }
            ////    }
            ////}
        }

        /// <summary>
        /// Handles the KeyDown event of the GroupsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GroupsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            ////    if (this.permissionGridEmptyRow)
            ////    {
            ////        switch (e.KeyCode)
            ////        {
            ////            case Keys.Down:
            ////                {
            ////                    e.Handled = true;
            ////                    break;
            ////                }
            ////        }
            ////    }
        }

        /// <summary>
        /// Handles the RowEnter event of the PermissionsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PermissionsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //// TODO
        }

        /// <summary>
        /// Handles the KeyDown event of the PermissionsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PermissionsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            ////if (this.permissionGridGroupEmptyRow)
            ////{
            ////    switch (e.KeyCode)
            ////    {
            ////        case Keys.Down:
            ////            {
            ////                e.Handled = true;
            ////                break;
            ////            }
            ////    }
            ////}*/
        }

        /// <summary>
        /// Handles the 1 event of the UserGridView_CellClick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////Avoid RowHeader and Column Header 
            if (e.RowIndex >= 0)
            {
                //// Only Row With USerID Shoud Execute Below
                if (!string.IsNullOrEmpty(this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString()))
                {
                    this.userGridRowIndex = e.RowIndex;
                    if (!this.keyPressed)
                    {
                        this.tempRowId = this.userGridRowIndex;
                        this.userUpdateRecord = this.tempRowId;
                        this.SetButton(ButtonOperation.Empty);
                        if (e.ColumnIndex >= 0)
                        {
                            this.userClickColumnId = e.ColumnIndex;
                        }
                        else
                        {
                            this.userClickColumnId = 0;
                        }
                    }

                    if (this.keyPressed)
                    {
                        if (this.tempRowId != e.RowIndex)
                        {
                            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        this.SaveUserData();
                                        //// Saved Then
                                        if (this.closingForm)
                                        {
                                            this.tempRowId = e.RowIndex;
                                            this.SetButton(ButtonOperation.Empty);
                                            if (this.buttonOperation != (int)ButtonOperation.New)
                                            {
                                                if (this.UserGridView.Rows.Count >= 0 && e.RowIndex >= 0)
                                                {
                                                    this.LoadGroupDataGridView(this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString(), "Group");
                                                }
                                            }

                                            if (e.ColumnIndex >= 0)
                                            {
                                                this.UserGridView.CurrentCell = this.UserGridView[e.ColumnIndex, e.RowIndex];
                                            }
                                            else
                                            {
                                                this.SetDataGridViewPosition(this.UserGridView, e.RowIndex);
                                            }

                                            this.SetUserFormTextBoxes(e.RowIndex);
                                            this.GroupDataGrid.Enabled = true;
                                            this.keyPressed = false;
                                            this.EnableUserGridSorting();
                                            this.UserGridView.Enabled = true;
                                        }
                                        else
                                        {
                                            if (e.ColumnIndex >= 0)
                                            {
                                                this.UserGridView.CurrentCell = this.UserGridView[e.ColumnIndex, this.tempRowId];
                                            }
                                            else
                                            {
                                                this.SetDataGridViewPosition(this.UserGridView, this.tempRowId);
                                            }
                                        }

                                        this.buttonOperation = (int)ButtonOperation.Empty;
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        this.LoadUserGroupDetails();

                                        this.tempRowId = e.RowIndex;
                                        this.SetButton(ButtonOperation.Empty);
                                        if (this.buttonOperation != (int)ButtonOperation.New)
                                        {
                                            if (this.UserGridView.Rows.Count >= 0 && e.RowIndex >= 0)
                                            {
                                                this.LoadGroupDataGridView(this.UserGridView.Rows[e.RowIndex].Cells["UserID"].Value.ToString(), "Group");
                                            }
                                        }

                                        if (e.ColumnIndex >= 0)
                                        {
                                            this.UserGridView.CurrentCell = this.UserGridView[e.ColumnIndex, e.RowIndex];
                                        }
                                        else
                                        {
                                            this.SetDataGridViewPosition(this.UserGridView, e.RowIndex);
                                        }

                                        this.SetUserFormTextBoxes(e.RowIndex);
                                        this.GroupDataGrid.Enabled = true;
                                        this.EnableUserGridSorting();
                                        this.keyPressed = false;
                                        ////this.EnableUserGridSort();
                                        this.buttonOperation = (int)ButtonOperation.Empty; 
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        if (e.ColumnIndex >= 0)
                                        {
                                            this.UserGridView.CurrentCell = this.UserGridView[this.userClickColumnId, this.tempRowId];
                                        }
                                        else
                                        {
                                            this.SetDataGridViewPosition(this.UserGridView, this.tempRowId);
                                        }

                                        //// this.SetDataGridViewPosition(this.UserGridView, this.tempRowId);
                                        this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                                        this.GroupDataGrid.Enabled = false;

                                        ////this.UserNameTextBox.Focus();
                                        break;
                                    }
                            } ////For Switch Case
                        }
                    }
                    else
                    {
                        this.EnableControl();
                        ////this.GroupDataGrid.Enabled = true;
                        this.SetUserFormTextBoxes(this.tempRowId);
                        this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                        ////this.GroupDataGrid.Enabled = false;
                    }

                    if (this.tempRowId >= 0 && !this.keyPressed)
                    {
                        this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                        if (this.userGroupRowCount > 0)
                        {
                            this.GroupDataGrid.Enabled = true;
                        }
                        else
                        {
                            this.GroupDataGrid.Enabled = false;
                        }

                        //// SetTextBoxFocus(this.UserNameTextBox);
                    }
                }
                else
                {
                    this.SetDataGridViewPosition(this.UserGridView, this.tempRowId);
                }
            }
        }

        /// <summary>
        /// Handles the 1 event of the GroupDescTextBox_TextChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GroupDescTextBox_TextChanged(object sender, EventArgs e)
        {
            //&& !this.GroupTabGroupGrid.IsSorted
            if (!this.changeInGroupDataSet && this.groupLoaded  && !this.groupGridClick)
            {
                this.changeInGroupDataSet = true;
                this.SetGroupButton(ButtonOperation.Update);
                this.DisableGroupGridSorting();
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the UserManagementForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void UserManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.CloseReason.Equals(CloseReason.ApplicationExitCall))
            {
                if (e.CloseReason.Equals(CloseReason.UserClosing))
                {
                    if (this.keyPressed || this.changeInGroupDataSet || this.permissionChanges)
                    {
                        switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName ,"?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            case DialogResult.Yes:
                                {
                                    //// Changes in UserGrid
                                    if (this.UserManagementTab.SelectedIndex == 0)
                                    {
                                        this.SaveUserData();

                                        if (this.closingForm)
                                        {
                                            // this.DialogResult = DialogResult.No;
                                            e.Cancel = false;
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            this.UserManagementTab.SelectedIndex = 0;
                                        }
                                    }  //// Any Group
                                    else if (this.UserManagementTab.SelectedIndex == 1)
                                    {
                                        this.SaveGroupDatas();

                                        if (this.closingForm)
                                        {
                                            // this.DialogResult = DialogResult.No;
                                            e.Cancel = false;
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            this.UserManagementTab.SelectedIndex = 1;
                                        }
                                    }  //// Permission
                                    else if (this.UserManagementTab.SelectedIndex == 2)
                                    {
                                        this.SavePermissionData();
                                        if (this.closingForm)
                                        {
                                            // this.DialogResult = DialogResult.No;
                                            e.Cancel = false;
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            this.UserManagementTab.SelectedIndex = 2;
                                        }
                                    }

                                    break;
                                }

                            case DialogResult.No:
                                {
                                    // this.DialogResult = DialogResult.No;
                                    e.Cancel = false;
                                    break;
                                }

                            case DialogResult.Cancel:
                                {
                                    e.Cancel = true;
                                    break;
                                }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the 1 event of the GroupDataGrid_RowEnter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            //////// this.SetDataGridLinkForeColor(e.RowIndex);
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the UserGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void UserGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //// TODO
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the GroupTabGroupGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GroupDetailsGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //// this.GroupTabGroupGrid.Rows[e.RowIndex].Selected = true;
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the GroupsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GroupsDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ///// TODO
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the UserGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void UserGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //// TODO
            if (this.userRecord > 0)
            {
                this.UserGridView.Rows[0].Selected = true;
                this.UserGridView.CurrentCell = this.UserGridView[0, 0];
                this.UserGridView.Focus();
            }
        }

        /*  /// <summary>
         /// Disables the User grid sort.
         /// </summary>
         private void DisableUserGridSort()
         {
             ////    DataGridViewColumnCollection disableUserSortColumn = this.UserGridView.Columns;
             ////    disableUserSortColumn["UserId"].SortMode = DataGridViewColumnSortMode.NotSortable;
             ////    disableUserSortColumn["FullName"].SortMode = DataGridViewColumnSortMode.NotSortable;
             ////    disableUserSortColumn["IsAdministrator"].SortMode = DataGridViewColumnSortMode.NotSortable;
             ////    disableUserSortColumn["IsActive"].SortMode = DataGridViewColumnSortMode.NotSortable;
         }

         /// <summary>
         /// Disables the User grid sort.
         /// </summary>
         private void EnableUserGridSort()
         {
             ////DataGridViewColumnCollection enableUserSortColumn = this.UserGridView.Columns;
             ////enableUserSortColumn["UserId"].SortMode = DataGridViewColumnSortMode.Automatic;
             ////enableUserSortColumn["FullName"].SortMode = DataGridViewColumnSortMode.Automatic;
             ////enableUserSortColumn["IsAdministrator"].SortMode = DataGridViewColumnSortMode.Automatic;
             ////enableUserSortColumn["IsActive"].SortMode = DataGridViewColumnSortMode.Automatic;
         }

         /// <summary>
         /// Disables the group grid sort.
         /// </summary>
         private void DisableGroupGridSort()
         {
             ////DataGridViewColumnCollection disableUserSortColumn = this.GroupTabGroupGrid.Columns;
             ////disableUserSortColumn["GroupID"].SortMode = DataGridViewColumnSortMode.NotSortable;
             ////disableUserSortColumn["GroupListName"].SortMode = DataGridViewColumnSortMode.NotSortable;
         }

         /// <summary>
         /// Enable the group grid sort.
         /// </summary>
         private void EnableGroupGridSort()
         {
             ////DataGridViewColumnCollection enableUserSortColumn = this.GroupTabGroupGrid.Columns;
             ////enableUserSortColumn["GroupID"].SortMode = DataGridViewColumnSortMode.Automatic;
             ////enableUserSortColumn["GroupListName"].SortMode = DataGridViewColumnSortMode.Automatic;
         }

         /// <summary>
         /// Disables the permission grid sort.
         /// </summary>
         private void DisablePermissionGridSort()
         {
             ////    DataGridViewColumnCollection enablePermissionsSortColumn = this.GroupsDataGridView.Columns;
             ////    enablePermissionsSortColumn["GroupsID"].SortMode = DataGridViewColumnSortMode.NotSortable;
             ////    enablePermissionsSortColumn["PermissionsGroupsName"].SortMode = DataGridViewColumnSortMode.NotSortable;
         }

         /// <summary>
         /// Enables the permission grid sort.
         /// </summary>
         private void EnablePermissionGridSort()
         {
             ////    DataGridViewColumnCollection enablePermissionsSortColumn = this.GroupsDataGridView.Columns;
             ////  enablePermissionsSortColumn["GroupsID"].SortMode = DataGridViewColumnSortMode.Automatic;
             //// enablePermissionsSortColumn["PermissionsGroupsName"].SortMode = DataGridViewColumnSortMode.Automatic;
         }  */

        /// <summary>
        /// Handles the MouseEnter event of the UserNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserNameTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.UserNameTextBox.Text))
            {
                this.UserManagementToolTip.SetToolTip(this.UserNameTextBox, this.UserNameTextBox.Text);
            }
            else
            {
                this.UserManagementToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the DisplayNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DisplayNameTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.DisplayNameTextBox.Text))
            {
                this.UserManagementToolTip.SetToolTip(this.DisplayNameTextBox, this.DisplayNameTextBox.Text);
            }
            else
            {
                this.UserManagementToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the NetNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NetNameTextBox_MouseEnter(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(this.NetNameTextBox.Text))
            //{
            //    this.UserManagementToolTip.SetToolTip(this.NetNameTextBox, this.NetNameTextBox.Text);
            //}
            //else
            //{
            //    this.UserManagementToolTip.RemoveAll();
            //}
        }

        /// <summary>
        /// Handles the MouseEnter event of the EmailTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EmailTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.EmailTextBox.Text))
            {
                this.UserManagementToolTip.SetToolTip(this.EmailTextBox, this.EmailTextBox.Text);
            }
            else
            {
                this.UserManagementToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the RowValidating event of the UserGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void UserGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.tempRowId != e.RowIndex && this.keyPressed)
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.SavePermissionData();
                    this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                    this.permissionChanges = false;
                    this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Selected = true;
                    this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, this.permissionGridRowIndex];
                    this.permissionPreviousRowId = e.RowIndex;
                    ////this.EnablePermissionGridSort();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Handles the 1 event of the UserGridView_KeyDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void UserGridView_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (this.keyPressed)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            this.SaveUserTab(e);

                            break;
                        }

                    case Keys.Up:
                        {
                            this.SaveUserTab(e);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Saves the user tab.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SaveUserTab(KeyEventArgs e)
        {
            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        this.SaveUserData();
                        //// checks the required field it its then save
                        if (this.closingForm)
                        {
                            this.EnableUserGridSorting();
                            this.keyPressed = false;
                            this.SetButton(ButtonOperation.Empty);
                            if (this.buttonOperation != (int)ButtonOperation.New)
                            {
                                if (this.UserGridView.Rows.Count >= 0 && this.tempRowId >= 0)
                                {
                                    this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                                }
                            }

                            this.UserGridView.CurrentCell = this.UserGridView[this.userClickColumnId, this.tempRowId];
                            this.SetUserFormTextBoxes(this.tempRowId);
                            this.GroupDataGrid.Enabled = true;

                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }

                        break;
                    }

                case DialogResult.No:
                    {
                        this.LoadUserGroupDetails();
                        this.keyPressed = false;
                        //// this.tempRowId = e.RowIndex;
                        this.SetButton(ButtonOperation.Empty);
                        this.EnableUserGridSorting();
                        if (this.buttonOperation != (int)ButtonOperation.New)
                        {
                            if (this.UserGridView.Rows.Count >= 0 && this.tempRowId >= 0)
                            {
                                this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                            }
                        }

                        this.UserGridView.CurrentCell = this.UserGridView[this.userClickColumnId, this.tempRowId];
                        this.SetUserFormTextBoxes(this.tempRowId);
                        this.GroupDataGrid.Enabled = true;
                        ////this.EnableUserGridSort();
                        e.Handled = false;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        ////this.UserGridView.CurrentCell = this.UserGridView[e.ColumnIndex, this.tempRowId];
                        /////  this.SetDataGridViewPosition(this.UserGridView, this.tempRowId);
                        ////this.LoadGroupDataGridView(this.UserGridView.Rows[this.tempRowId].Cells["UserID"].Value.ToString(), "Group");
                        ////this.GroupDataGrid.Enabled = false;
                        ////this.UserNameTextBox.Focus();
                        e.Handled = true;
                        break;
                    }
            } //// 
        }

        /// <summary>
        /// Handles the KeyDown event of the GroupTabGroupGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GroupTabGroupGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.changeInGroupDataSet)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            ////this.FindUserFromGroupList(this.GroupTabGroupGrid.Rows[0].Cells["GroupID"].Value.ToString());
                            this.SaveGroupTab(e);

                            break;
                        }

                    case Keys.Up:
                        {
                            this.SaveGroupTab(e);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Saves the group tab.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SaveGroupTab(KeyEventArgs e)
        {
            int saveRowId;
            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        saveRowId = this.tempGroupRowId;
                        this.changeInGroupDataSet = false;
                        this.SaveGroupDatas();
                        this.SetGropTextBoxes(saveRowId);
                        this.SetDataGridViewPosition(this.GroupTabGroupGrid, saveRowId);
                        e.Handled = false;
                        break;
                    }

                case DialogResult.No:
                    {
                        this.EnableGroupGridSorting();
                        this.SetDataGridViewCellPosition(this.GroupTabGroupGrid, this.tempGroupRowId, this.tempGroupColumId);
                        this.SetGropTextBoxes(this.tempGroupRowId);
                        this.changeInGroupDataSet = false;
                        e.Handled = false;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        e.Handled = true;
                        break;
                    }
            } //// For Switch Case
        }

        /// <summary>
        /// Handles the 1 event of the GroupsDataGridView_KeyDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GroupsDataGridView_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (this.permissionChanges)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            this.SavePermissionsTab(e);

                            break;
                        }

                    case Keys.Up:
                        {
                            this.SavePermissionsTab(e);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Saves the permissions tab.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SavePermissionsTab(KeyEventArgs e)
        {
            switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName , "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        //// Changes in UserGrid

                        this.SavePermissionData();
                        //// changed dataset to datagrid
                        this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                        this.permissionChanges = false;
                        this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Selected = true;
                        this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, this.permissionGridRowIndex];
                        this.permissionPreviousRowId = this.permissionGridRowIndex;
                        //// this enable sort
                        this.EnablePermissionGroupGridSorting();
                        e.Handled = false;
                        break;
                    }

                case DialogResult.No:
                    {
                        this.SetPermissionButtons(PermissionButtonOperation.Cancel);
                        this.permissionSelected.RejectChanges();

                        //// changed dataset to datagrid
                        this.LoadPermission(this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Cells["GroupsID"].Value.ToString());
                        this.permissionChanges = false;
                        this.GroupsDataGridView.Rows[this.permissionGridRowIndex].Selected = true;
                        this.GroupsDataGridView.CurrentCell = this.GroupsDataGridView[0, this.permissionGridRowIndex];
                        this.permissionPreviousRowId = this.permissionGridRowIndex;
                        //// this enable sort
                        this.EnablePermissionGroupGridSorting();
                        e.Handled = false;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        e.Handled = true;

                        break;
                    }
            } //// For Switch Case
        }

        /// <summary>
        /// Handles the RowLeave event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// Handles the 2 event of the GroupDataGrid_RowEnter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_RowEnter_2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                //////// this.SetDataGridLinkForeColor1(e.RowIndex, e.ColumnIndex);
            }
        }

        /// <summary>
        /// Handles the Sorted event of the UserInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserInGrid_Sorted(object sender, EventArgs e)
        {
            if (string.Compare(this.UserInGrid.SortOrder.ToString(), "Descending") == 0)
            {
                this.userInSortedOrder = "DESC";
            }
            else
            {
                this.userInSortedOrder = "ASC";
            }
        }

        /// <summary>
        /// Handles the SortCompare event of the UserInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewSortCompareEventArgs"/> instance containing the event data.</param>
        private void UserInGrid_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (string.Compare(this.UserInGrid.SortOrder.ToString(), "Descending") == 0)
            {
                this.userInSortedOrder = "DESC";
            }
            else
            {
                this.userInSortedOrder = "ASC";
            }
        }

        /*  /// <summary>
          /// Sets the cancel button.
          /// </summary>
          private void SetUserCancelButton()
          {
              ////    if (this.UserCancelButton.Enabled == false)
              ////    {
              ////         this.Close();
              ////    }
              ////    else
              ////    {
              ////       this.CancelButton = this.UserCancelButton;
              ////    }
          }  */

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void DisableUserGridSorting()
        {
            if (!this.UserGridView.IsSorted && this.keyPressed)
            {
                DataGridViewColumnCollection disableSortColumn = this.UserGridView.Columns;
                disableSortColumn["UserId"].SortMode = DataGridViewColumnSortMode.NotSortable;
                disableSortColumn["FullName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                disableSortColumn["DisplayName"].SortMode = DataGridViewColumnSortMode.NotSortable;
                disableSortColumn["IsAdministrator"].SortMode = DataGridViewColumnSortMode.NotSortable;
                disableSortColumn["IsActive"].SortMode = DataGridViewColumnSortMode.NotSortable;
                disableSortColumn["Appraiser"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void EnableUserGridSorting()
        {
            DataGridViewColumnCollection enableSortColumn = this.UserGridView.Columns;
            enableSortColumn["UserId"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["FullName"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["DisplayName"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["IsAdministrator"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["IsActive"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["Appraiser"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void DisableGroupGridSorting()
        {
            DataGridViewColumnCollection disableGroupSortColumn = this.GroupTabGroupGrid.Columns;
            disableGroupSortColumn["GroupID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGroupSortColumn["GroupListName"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void EnableGroupGridSorting()
        {
            DataGridViewColumnCollection enableSortColumn = this.GroupTabGroupGrid.Columns;
            enableSortColumn["GroupID"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableSortColumn["GroupListName"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void DisablePermissionGridSorting()
        {
            DataGridViewColumnCollection disablePermissionSortColumn = this.GroupsDataGridView.Columns;
            disablePermissionSortColumn["GroupsID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disablePermissionSortColumn["PermissionsGroupsName"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void EnablePermissionGroupGridSorting()
        {
            DataGridViewColumnCollection enablePermissionColumn = this.GroupsDataGridView.Columns;
            enablePermissionColumn["GroupsID"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enablePermissionColumn["PermissionsGroupsName"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Sets the user button event.
        /// </summary>
        private void SetUserButtonEvent()
        {
            this.NewUser.Click += new EventHandler(this.UserNewButton_Click);
            this.NewUser.Click -= new EventHandler(this.GroupNewButton_Click);
            this.SaveUser.Click -= new EventHandler(this.UserSaveButton_Click);
            this.SaveUser.Click -= new EventHandler(this.GroupSaveButton_Click);
            this.SaveUser.Click -= new EventHandler(this.PermissionSaveButton_Click);
            this.SaveUser.Click += new EventHandler(this.UserSaveButton_Click);
            //// this.CancelButton = this.UserCancelButton;
        }

        /// <summary>
        /// SetGroupButtonEvent
        /// </summary>
        private void SetGroupButtonEvent()
        {
            this.NewUser.Click -= new EventHandler(this.UserNewButton_Click);
            this.NewUser.Click += new EventHandler(this.GroupNewButton_Click);
            this.SaveUser.Click -= new EventHandler(this.UserSaveButton_Click);
            this.SaveUser.Click -= new EventHandler(this.GroupSaveButton_Click);
            this.SaveUser.Click -= new EventHandler(this.PermissionSaveButton_Click);
            this.SaveUser.Click += new EventHandler(this.GroupSaveButton_Click);
            //// this.CancelButton = this.GroupCancelbutton; 
        }

        /// <summary>
        /// Sets the permission button event.
        /// </summary>
        private void SetPermissionButtonEvent()
        {
            this.NewUser.Click -= new EventHandler(this.UserNewButton_Click);
            this.NewUser.Click -= new EventHandler(this.GroupNewButton_Click);
            this.SaveUser.Click -= new EventHandler(this.UserSaveButton_Click);
            this.SaveUser.Click -= new EventHandler(this.GroupSaveButton_Click);
            this.SaveUser.Click -= new EventHandler(this.PermissionSaveButton_Click);
            this.SaveUser.Click += new EventHandler(this.PermissionSaveButton_Click);
            this.ParentForm.CancelButton = this.PermissionCancelButton;
            this.PermissionCancelButton.Focus();
        }

        /// <summary>
        /// Savepers the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Saveper(object sender, EventArgs e)
        {
            this.PermissionSaveButton_Click(this.PermissionSaveButton, e);
        }

        /// <summary>
        /// Handles the Click event of the PermissionPrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PermissionPrintButton_Click(object sender, EventArgs e)
        {
            Hashtable permissionTabReport = new Hashtable();
            permissionTabReport.Add("ReportNumber", "90023");
            /////changed the parameter type from string to int
            TerraScanCommon.ShowReport(900230, TerraScan.Common.Reports.Report.ReportType.Print, permissionTabReport);
        }

        /// <summary>
        /// Handles the Click event of the PermissionPreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PermissionPreviewButton_Click(object sender, EventArgs e)
        {
            ////Hashtable permissionTabPrint = new Hashtable();
            ////permissionTabPrint.Add("ReportNumber", "90023");
            ////changed the parameter type from string to int
            TerraScanCommon.ShowReport(900230, TerraScan.Common.Reports.Report.ReportType.Preview);
        }

        /// <summary>
        /// Handles the Click event of the groupPreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupPreviewButton_Click(object sender, EventArgs e)
        {
            ////Hashtable groupTabReport = new Hashtable();
            ////groupTabReport.Add("ReportNumber", "90022");
            ////changed the parameter type from string to int
            TerraScanCommon.ShowReport(900220, TerraScan.Common.Reports.Report.ReportType.Preview);
        }

        /// <summary>
        /// Handles the Click event of the groupPrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupPrintButton_Click(object sender, EventArgs e)
        {
            Hashtable groupTabReport = new Hashtable();
            groupTabReport.Add("ReportNumber", "90022");
            ////changed the parameter type from string to int
            TerraScanCommon.ShowReport(900220, TerraScan.Common.Reports.Report.ReportType.Print, groupTabReport);
        }

        /// <summary>
        /// Handles the Click event of the userPreivewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserPreivewButton_Click(object sender, EventArgs e)
        {
            ////Hashtable userTabReport = new Hashtable();
            ////userTabReport.Add("ReportNumber", "90022");
            ////userTabReport.Add("UserID", TerraScanCommon.UserId);
            ////changed the parameter type from string to int
            TerraScanCommon.ShowReport(900210, TerraScan.Common.Reports.Report.ReportType.Preview);
        }

        /// <summary>
        /// Handles the Click event of the userPrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserPrintButton_Click(object sender, EventArgs e)
        {
            Hashtable userTabReport = new Hashtable();
            userTabReport.Add("ReportNumber", "90022");
            userTabReport.Add("UserID", TerraScanCommon.UserId);
            //// changed the parameter type from string to int
            TerraScanCommon.ShowReport(900210, TerraScan.Common.Reports.Report.ReportType.Print, userTabReport);
        }

        /// <summary>
        /// Disables the user in sorting.
        /// </summary>
        private void DisableUserInSorting()
        {
            this.UserInGrid.RemainSortFields = false;
            this.userNotIngrid.RemainSortFields = false;
        }

        /// <summary>
        /// Enables the user in sorting.
        /// </summary>
        private void EnableUserInSorting()
        {
            this.UserInGrid.RemainSortFields = true;
            this.userNotIngrid.RemainSortFields = true;
        }

        /// <summary>
        /// Handles the Load event of the F9002 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9002_Load(object sender, EventArgs e)
        {
            this.LoadWorkSpace();
            this.UserGridView.IsSorted = false;
            this.LoadUserGroupDetails();
            this.ParentForm.CancelButton = this.UserCancelButton;
            this.formLoaded = true;
            this.UserGridView.IsSorted = false;
            this.groupLoaded = false;
            this.ParentForm.ActiveControl = UserNameTextBox;
            this.UserNameTextBox.Focus();
        }

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            bool formStatus = true;
            if (this.keyPressed || this.changeInGroupDataSet || this.permissionChanges)
            {
                switch (MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
           //// switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName  "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                {
                    case DialogResult.Yes:
                        {
                            //// Changes in UserGrid
                            if (this.UserManagementTab.SelectedIndex == 0)
                            {
                                this.SaveUserData();

                                if (this.closingForm)
                                {
                                    // this.DialogResult = DialogResult.No;
                                    formStatus = true;
                                }
                                else
                                {
                                    formStatus = false;
                                    this.UserManagementTab.SelectedIndex = 0;
                                }
                            }  //// Any Group
                            else if (this.UserManagementTab.SelectedIndex == 1)
                            {
                                this.SaveGroupDatas();

                                if (this.closingForm)
                                {
                                    // this.DialogResult = DialogResult.No;
                                    formStatus = true;
                                }
                                else
                                {
                                    formStatus = false;
                                    this.UserManagementTab.SelectedIndex = 1;
                                }
                            }  //// Permission
                            else if (this.UserManagementTab.SelectedIndex == 2)
                            {
                                this.SavePermissionData();
                                if (this.closingForm)
                                {
                                    // this.DialogResult = DialogResult.No;
                                    formStatus = true;
                                }
                                else
                                {
                                    formStatus = false;
                                    this.UserManagementTab.SelectedIndex = 2;
                                }
                            }

                            break;
                        }

                    case DialogResult.No:
                        {
                            // this.DialogResult = DialogResult.No;
                            formStatus = true;
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            formStatus = false;
                            break;
                        }
                }
            }

            return formStatus;
        }

        /// <summary>
        /// Handles the KeyPress event of the UserNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void UserNameTextBox_TextChanged(object sender, EventArgs e)
        {
            ////if (this.buttonOperation != (int)ButtonOperation.Update && this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab && this.formLoaded)
            ////{
            ////    if (this.keyPressed)
            ////    {
            ////     // this.UserGridView.BaseSortedColumn  
            ////        this.keyPressed = true;
            ////        ////this.DisableUserGridSort();
            ////        this.SetButton(ButtonOperation.Update);
            ////        this.DisableUserGridSorting();
            ////    }
            ////    else
            ////    {
            ////        this.SetButton(ButtonOperation.Update);
            ////        this.DisableUserGridSorting();
            ////    }
            ////}
            //&& !this.UserGridView.IsSorted
            if (this.buttonOperation != (int)ButtonOperation.Update && this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab && this.formLoaded  && !this.userGridClick)
            {
                if (this.keyPressed)
                {
                    this.keyPressed = true;
                    this.SetButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableUserGridSorting();
                }
                else
                {
                    this.keyPressed = true;
                    this.SetButton(ButtonOperation.Update);
                    this.buttonOperation = (int)ButtonOperation.Update;
                    this.DisableUserGridSorting();
                }
            }
        }

        /// <summary>
        /// Handles the CellEnter event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                //////// this.SetDataGridLinkForeColorCellEnter(e.RowIndex, e.ColumnIndex);
            }
        }

        /// <summary>
        /// Handles the CellLeave event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                //////// this.SetDataGridLinkForeColorCellEnter(e.RowIndex, e.ColumnIndex);
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (this.GroupDataGrid.SelectedRows.Count == 1)
            {
                //// this.SetDataGridLinkForeColor(GroupDataGrid.CurrentCell.RowIndex, 1);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DisplayNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DisplayNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.Update && this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab && this.formLoaded)
            {
                if (this.keyPressed)
                {
                    this.keyPressed = true;
                    this.SetButton(ButtonOperation.Update);
                    this.DisableUserGridSorting();
                }
                ////else
                ////{
                ////    this.SetButton(ButtonOperation.Update);
                ////    this.DisableUserGridSorting();
                ////}
            }
        }

        /// <summary>
        /// Handles the DoubleClick event of the userNotIngrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserNotIngrid_DoubleClick(object sender, EventArgs e)
        {
            this.UserMoveIn();
        }

        /// <summary>
        /// Handles the DoubleClick event of the UserInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UserInGrid_DoubleClick(object sender, EventArgs e)
        {
            ////this.UserMoveOut(); 
        }

        /// <summary>
        /// Handles the LinkClicked event of the GroupLinkLabel control.
        /// Added by guhan on 23 jan 07
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GroupLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int auditLinkKeyId = 0;
                int.TryParse(this.GroupIDTextBox.Text.Trim(), out auditLinkKeyId);

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

                ////this.Cursor = Cursors.WaitCursor;
                ////Hashtable eventEngineDetails = new Hashtable();
                ////eventEngineDetails.Add("GroupID", this.GroupIDTextBox.Text.Trim());
                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(900291, TerraScan.Common.Reports.Report.ReportType.Preview, eventEngineDetails);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the PermissionAuditLink control.
        /// Added by guhan on 23 jan 07
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void PermissionAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(90101);
                formInfo.optionalParameters = new object[2];
                formInfo.optionalParameters[0] = this.Tag;
                formInfo.optionalParameters[1] = TerraScanCommon.UserId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

                ////////changed the parameter type from string to int
                ////TerraScanCommon.ShowReport(900290, TerraScan.Common.Reports.Report.ReportType.Preview, null);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the GroupDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void GroupDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.GroupDataGrid.Columns["GroupName"].Index)
            {
                if (this.GroupDataGrid.Rows[e.RowIndex].Selected || this.GroupDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                {
                    (this.GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"] as DataGridViewLinkCell).LinkColor = Color.White;
                    (this.GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                    (this.GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                }
                else
                {
                    (this.GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"] as DataGridViewLinkCell).LinkColor = Color.Blue;
                    (this.GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                    (this.GroupDataGrid.Rows[e.RowIndex].Cells["GroupName"] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the UserInGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserInGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!String.IsNullOrEmpty(this.UserInGrid.Rows[e.RowIndex].Cells["InGroupID"].Value.ToString()))
                {
                    this.UserMoveOut();
                }
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the userNotIngrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void UserNotIngrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!String.IsNullOrEmpty(this.userNotIngrid.Rows[e.RowIndex].Cells["OutGroupId"].Value.ToString()))
                {
                    this.UserMoveIn();
                }
            }
        }

        /// <summary>
        /// Handles the CellMouseDoubleClick event of the PermissionsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void PermissionsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.PermissionsDataGridView.Rows[e.RowIndex].Cells["GridEntry"].RowIndex > -1))
            //{
            if (e.RowIndex >= 0 && e.RowIndex < this.PermissionsDataGridView.OriginalRowCount)
            {
                if (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["GridEntry"].Selected.Equals(true))
                {
                    if ((this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionMenu"].Value.Equals(true)) && (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionOpen"].Value.Equals(true)) && (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionAdd"].Value.Equals(true)) && (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionEdit"].Value.Equals(true)) && (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionDelete"].Value.Equals(true)))
                    {
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionMenu"].Value = false;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionOpen"].Value = false;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionAdd"].Value = false;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionEdit"].Value = false;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionDelete"].Value = false;
                    }
                    else if ((this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionMenu"].Value.Equals(false)) || (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionOpen"].Value.Equals(false)) || (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionAdd"].Value.Equals(false)) || (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionEdit"].Value.Equals(false)) || (this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionDelete"].Value.Equals(false)))
                    {
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionMenu"].Value = true;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionOpen"].Value = true;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionAdd"].Value = true;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionEdit"].Value = true;
                        this.PermissionsDataGridView.Rows[e.RowIndex].Cells["isPermissionDelete"].Value = true;
                    }
                    this.PermissionSaveButton.Enabled = true;
                    this.PermissionCancelButton.Enabled = true;
                    this.permissionChanges = true;
                }
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
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    //HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Appraiser ComboBox Click Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppraiserComboBox_Click(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab)
            {
                this.keyPressed = true;
                this.SetButton(ButtonOperation.Update);
                this.DisableUserGridSorting();
            }
        }

        /// <summary>
        /// Appraiser ComboBox Selection Change Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppraiserComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.buttonOperation != (int)ButtonOperation.New && this.buttonOperation != (int)ButtonOperation.FromOtherTab)
            {
                this.keyPressed = true;
                this.SetButton(ButtonOperation.Update);
                this.DisableUserGridSorting();
            }
        }

        ////private void GroupNameTextBox_MouseEnter(object sender, EventArgs e)
        ////{
        ////    if (!String.IsNullOrEmpty(this.GroupNameTextBox.Text))
        ////    {
        ////        this.UserManagementToolTip.SetToolTip(this.GroupNameTextBox, this.GroupNameTextBox.Text);
        ////    }
        ////    else
        ////    {
        ////        this.UserManagementToolTip.RemoveAll();
        ////    }
        ////}       
        
    }
}
