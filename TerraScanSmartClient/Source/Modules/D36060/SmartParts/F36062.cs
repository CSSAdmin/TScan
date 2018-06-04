
//--------------------------------------------------------------------------------------------
// <copyright file="F36062.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36062 FS Land Influences Control Tables.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			  Author		       Description
// ----------	  ---------		       ---------------------------------------------------------
//  06-JAN-11       ManojKumar          New Slice Form Created
//***********************************************************************************************/

namespace D36060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infrastructure.Interface;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;


    /// <summary>
    /// F36062 class file
    /// </summary>
    [SmartPart]
    public partial class F36062 : BaseSmartPart
    {

        #region variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// used to store the keyId
        /// </summary>
        private int keyId;


        ///<summary>
        /// used to identify form Load
        /// </summary>
        private bool formLoad;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to set current ColumnIndex
        /// </summary>
        private int columnNo;

        /// <summary>
        /// Used to set GridView Original RowCount.
        /// </summary>
        private int rowNo;
     
        public int temp1;

        ///<summary>
        ///Used for arrest cell formatting for the delte the record.
        ///</summary>
       private bool rowDel=false;


        ///<summary>
        ///Used fpr arrest cell BeginEdit for delete the record.
        /// </summary>
       private bool cellform = false;

        /// <summary>
        /// Used to Identify the RowIndex
        /// </summary>
        private int rowIn;

        /// <summary>
        /// USed to arrest Cell Formatting after Cell Enter
        /// </summary>
        private bool msg;

        /// <summary>
        /// used for flag during key Down
        /// </summary>
        private bool flagondelete;


        /// <summary>
        /// Used for identify RowAdded.
        /// </summary>
        private bool isRowAdded=false;
        
        ///<summary>
        ///used for identify changes in combo selection and text changed
        ///</summary>
        private bool isChange=false;


        ///<sumary>
        ///used for identify the selection change commited
        /// </sumary>
        private bool isSelectChange=false;
         
   

        /// <summary>
        /// Used to store the LandInfluence data
        /// </summary>
        private F36062LandInfluenceData landInfluenceData = new F36062LandInfluenceData();

        ///<summary>
        /// Used to hold the temp DataTable
        /// </summary>
        private DataTable tempData = new DataTable();

        private DataRow [] DelRow;  
        
        
        /// <summary>
        /// Used to store the listLandInfluenceTable will be used 
        /// </summary>
        private F36062LandInfluenceData.ListLandInfluenceTableDataTable listLandInfluenceData = new F36062LandInfluenceData.ListLandInfluenceTableDataTable();

        private F36062LandInfluenceData.GetLandDescriptionTitleDataTable GetLandDescriptionData = new F36062LandInfluenceData.GetLandDescriptionTitleDataTable();

       

        /// <summary>
        /// controller F36062
        /// </summary>
        private F36062Controller form36062Control;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

         /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// To check whether the key id valid are not
        /// </summary>
        private bool iskeyidValid;

        ///<summary>
        ///To identify the delete position
        /// </summary>
        private int isDelId;

        ///<summary>
        ///Used for identify delete row for unsaved records
        /// </summary>
        private bool isDel;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        #endregion variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36061"/> class.
        /// </summary>
        public F36062()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36033"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36062(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.InfluenceTablePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.InfluenceTablePictureBox.Height, this.InfluenceTablePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion

        #region Event Publication
        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;


        #endregion Event Publication

        #region property
        /// <summary>
        /// For F36062Control
        /// </summary>

        [CreateNew]
        public F36062Controller Form36062Control
        {
            get { return this.form36062Control as F36062Controller; }
            set { this.form36062Control = value; }
        }

        #endregion property

        #region Event Subscription

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this != null && this.IsDisposed != true && this.Visible && this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                /////to check whether the key id is valid are not
                if (this.iskeyidValid)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                }
            }
        }

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this != null && this.IsDisposed != true )
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.LandInfluenceLabel.Text = string.Empty;
                this.ClearLandInfluenceGrid(); 
                //this.LoadLandInfluenceControl(); 
                this.landInfluenceData.ListLandInfluenceTable.Clear();  
                this.LandInflueneDataGridView.DataSource = this.landInfluenceData.ListLandInfluenceTable.DefaultView;
                this.LandInflueneDataGridView.Rows[0].Selected = false;
                this.LandInfluenceGridVerticalScroll.Visible = true;

            }
        }

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this != null && this.IsDisposed != true )
            {   this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.LoadLandInfluenceControl();

            }
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            }
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                //used to sent the values used for saving the details
                this.saveLandInfluences();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                //int saveDeprValue = this.form36061Control.WorkItem.F36061_SaveDeprControlItems(this.keyId, TerraScanCommon.GetXmlString(this.listDeprControlItemsDataTable.Copy()), TerraScanCommon.UserId);

                ///used to reload the record after save.
                //if (saveDeprValue > 0)
                //{
                //SliceReloadActiveRecord currentSliceInfo;
                //currentSliceInfo.MasterFormNo = this.masterFormNo;
                ////currentSliceInfo.SelectedKeyId = saveDeprValue;
                //this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                //}
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                if (this.keyId != -99)
                {
                    this.LandInflueneDataGridView.Enabled = true;
                }
                this.LoadLandInfluenceControl();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }


        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            }
        }

        #endregion Protected Methods

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36062 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36062_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.formLoad = true; 
            this.CustomizeLandInfluenceGrid();
            this.LoadLandInfluenceControl();
            this.formLoad = false;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }
        #endregion

        #region Methods


        /// <summary>
        /// Clears the neighborhood config grid.
        /// </summary>
        private void ClearLandInfluenceGrid()
        {
            try
            {
                this.landInfluenceData.ListLandInfluenceTable.Clear();
                this.LandInflueneDataGridView.NumRowsVisible = 11;
                //this.LandInflueneDataGridView.DataSource = this.landInfluenceData.ListLandInfluenceTable;
                this.LandInflueneDataGridView.Enabled = false;
                //this.SetSmartPartHeight(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        ///<summary>
        /// Slice validation for check errors before saving the record.
        ///</summary>
        ////// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (this.LandInflueneDataGridView.OriginalRowCount > 0)
            {
                string land = string.Empty;
                try
                {

                    string filterQuery = '(' + landInfluenceData.ListLandInfluenceTable.TypeColumn.ColumnName + " IS NULL or " + landInfluenceData.ListLandInfluenceTable.InfluenceTypeColumn.ColumnName + " IS NULL or " + landInfluenceData.ListLandInfluenceTable.InfluenceColumn.ColumnName + " IS NULL or " + landInfluenceData.ListLandInfluenceTable.InfluenceTypeColumn.ColumnName + " ='') AND EmptyRecord$=False";
                    DataRow[] landRow = this.landInfluenceData.ListLandInfluenceTable.Select(filterQuery);
                    DataRow[] landerow = this.landInfluenceData.ListLandInfluenceTable.Select('('+landInfluenceData.ListLandInfluenceTable.InfluenceColumn.ColumnName + " ='') AND EmptyRecord$=False");
                    if (landRow.Length > 0)
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");

                        //for (int i = 0; i < this.LandInflueneDataGridView.OriginalRowCount; i++)
                        //{
                        //    if (string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[i]["influenceType"].ToString()))
                        //    {
                        //        this.LandInflueneDataGridView.Rows[i].Cells["InfluenceType"].Selected = true;
                        //    }
                        //}
                        //for (int i = 0; i < this.LandInflueneDataGridView.OriginalRowCount; i++)
                        //{

                        //    if (string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[i]["influence"].ToString()))
                        //    {
                        //        this.LandInflueneDataGridView.Rows[i].Cells["Influence"].Selected = true;
                        //    }
                        //}
                        //for (int i = 0; i < this.LandInflueneDataGridView.OriginalRowCount; i++)
                        //{
                        //    if (string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[i]["Type"].ToString()))
                        //    {
                        //        this.LandInflueneDataGridView.Rows[i].Cells["Type"].Selected = true;
                        //    }
                        //}

                        return sliceValidationFields;
                    }
                    if(landerow.Length >0)
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                        return sliceValidationFields;
                    }
                    //for (int j = 0; j < this.LandInflueneDataGridView.OriginalRowCount; j++)
                    //{
                    //    if ((string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[j]["influenceType"].ToString()))&& this.landInfluenceData.ListLandInfluenceTable.Rows[j]["EmptyRecord$"].ToString()=="False"); 
                    //    {
                    //        sliceValidationFields.RequiredFieldMissing = true;
                    //        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                    //        this.LandInflueneDataGridView.Rows[j].Cells["InfluenceType"].Selected = true;
                    //        return sliceValidationFields;
                    //    }
                    //}
                    //for (int k = 0; k < this.LandInflueneDataGridView.OriginalRowCount; k++)
                    //{

                    //    if ((string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[k]["influence"].ToString())) && this.landInfluenceData.ListLandInfluenceTable.Rows[k]["EmptyRecord$"].ToString()=="False"); 
                    //    {
                    //        sliceValidationFields.RequiredFieldMissing = true;
                    //        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                    //        this.LandInflueneDataGridView.Rows[k].Cells["Influence"].Selected = true;
                    //        return sliceValidationFields;
                    //    }
                    //}
                    //for (int l = 0; l < this.LandInflueneDataGridView.OriginalRowCount; l++)
                    //{
                    //    if (string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[l]["Type"].ToString())&& this.landInfluenceData.ListLandInfluenceTable.Rows[l]["EmptyRecord$"].ToString()=="false"); 
                    //    {
                    //        sliceValidationFields.RequiredFieldMissing = true;
                    //        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                    //        this.LandInflueneDataGridView.Rows[l].Cells["Type"].Selected = true;
                    //        return sliceValidationFields;
                    //    }
                    //}

                    if (this.IsDuplicateNameExist())
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = "Influence Type must be unique.";
                        return sliceValidationFields;

                    }
                    ////MessageBox.Show("This record cannot be saved because no Owner has been given an Order of 1 (one).", SharedFunctions.GetResourceString("TerraScanValidation"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
                catch (Exception ex)
                {

                }

            }
            return sliceValidationFields;
        }
        /// <summary>
        /// Determines whether [is duplicate name exist].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is duplicate name exist]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsDuplicateNameExist()
        {
            // Create a dataview from the source GetTrendDetails 
            DataView deprView = new DataView(this.landInfluenceData.ListLandInfluenceTable);

            // Filter view
            deprView.RowFilter = "EmptyRecord$ = false";

            // set the output column array of the destination table 
            string[] strColumns = { this.landInfluenceData.ListLandInfluenceTable.InfluenceTypeColumn.ColumnName };

            // true = yes, to get distinct values. 
            DataTable distinctTable = deprView.ToTable(true, strColumns);

            // If distinct values count lesser than original row count return 'true'
            if (distinctTable.Rows.Count < deprView.Count)
            {
                return true;
            }

            return false;
        }

        #region SetScrollBarVisibility
        private void SetScrollBarVisibility()
        {
            if (this.landInfluenceData.ListLandInfluenceTable.Rows.Count <= this.LandInflueneDataGridView.NumRowsVisible)
            {
                this.LandInfluenceGridVerticalScroll.Visible = true;
            }
            else
            {
                this.LandInfluenceGridVerticalScroll.Visible = false;

            }
        }
        #endregion SetScrollBarVisibility
        ///<summary>
        ///Handles the Load event of the Grid.
        ///</summary>
        private void LoadLandInfluenceControl()
        {
            this.FlagSliceForm = true;
            //this.listLandInfluenceData.Clear();
            //this.GetLandDescriptionData.Clear();
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.landInfluenceData = this.form36062Control.WorkItem.F36062_LandInfluenceItems(this.keyId);
                if (this.keyId != -99)
                {
                    this.LandInflueneDataGridView.Enabled = true; 
                }
                //this.listLandInfluenceData = this.landInfluenceData.ListLandInfluenceTable;
                if (this.landInfluenceData.GetLandDescriptionTitle.Rows.Count > 0)
                {
                    this.LandInfluenceLabel.Text = this.landInfluenceData.GetLandDescriptionTitle.Rows[0][this.landInfluenceData.GetLandDescriptionTitle.LandTitleColumn].ToString();
                }
                
                if (this.landInfluenceData.TypeComboboxTable.Rows.Count > 0)
                {
                    (this.Type as DataGridViewComboBoxColumn).DataSource = this.landInfluenceData.TypeComboboxTable;
                    (this.Type as DataGridViewComboBoxColumn).DisplayMember = this.landInfluenceData.TypeComboboxTable.TypeNameColumn.ColumnName;
                    (this.Type as DataGridViewComboBoxColumn).ValueMember = this.landInfluenceData.TypeComboboxTable.TypeColumn.ColumnName;
                }

            }
            this.SetScrollBarVisibility();
            this.landInfluenceData = this.form36062Control.WorkItem.F36062_LandInfluenceItems(this.keyId);
            this.LandInflueneDataGridView.DataSource = this.landInfluenceData.ListLandInfluenceTable.DefaultView;
            this.tempData = this.landInfluenceData.ListLandInfluenceTable.Clone();
            this.tempData.Clear();  
            if (this.flagondelete)
            {
               // TerraScanCommon.SetDataGridViewPosition(this.LandInflueneDataGridView, this.isDelId);
                this.LandInfluenceGridVerticalScroll.Visible = true;
                this.flagondelete = false;
            }
            if (this.LandInflueneDataGridView.OriginalRowCount >= this.LandInflueneDataGridView.NumRowsVisible)
            {
                F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                newRow["EmptyRecord$"] = "True";
                this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                this.LandInfluenceGridVerticalScroll.Visible = false;

            }

        }
        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        private void saveLandInfluences()
        {
            string landInfluenceItems;
            DataTable changeSet = null;
            this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
            changeSet = this.landInfluenceData.ListLandInfluenceTable.Copy();
            if (this.tempData.Rows.Count > 0)
            {
                changeSet.Merge(this.tempData);
            }
            if (changeSet != null)
            {
                //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                changeSet.DefaultView.RowFilter = "EmptyRecord$ = False";
                DataTable changesToSave = changeSet.DefaultView.ToTable();
                landInfluenceItems = TerraScanCommon.GetXmlString(changesToSave);
            }
            else
            {
                landInfluenceItems = null;
            }

            int returnValue = this.form36062Control.WorkItem.F36062_SaveInfluenceControl(this.keyId, landInfluenceItems, TerraScanCommon.UserId);
            this.tempData.Clear(); 
            this.keyId = returnValue;
            // Reload form after save
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));


        }

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "selected Index  Changed Events In Combo Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton()
        {
            try
            {
                this.EditEnabled();

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void CustomizeLandInfluenceGrid()
        {
            this.LandInflueneDataGridView.AutoGenerateColumns = false;
            this.InfluenceType.DataPropertyName = this.landInfluenceData.ListLandInfluenceTable.InfluenceTypeColumn.ColumnName;
            this.Influence.DataPropertyName = this.landInfluenceData.ListLandInfluenceTable.InfluenceColumn.ColumnName;
            this.Type.DataPropertyName = this.landInfluenceData.ListLandInfluenceTable.TypeColumn.ColumnName;
            this.Description.DataPropertyName = this.landInfluenceData.ListLandInfluenceTable.DescriptionColumn.ColumnName;
            this.Isdeleted.DataPropertyName = this.landInfluenceData.ListLandInfluenceTable.IsDeletedColumn.ColumnName;
            this.InfluenceTypeID.DataPropertyName = this.landInfluenceData.ListLandInfluenceTable.InfluenceTypeIDColumn.ColumnName;
            this.IsModified.DataPropertyName = this.landInfluenceData.ListLandInfluenceTable.IsModifiedColumn.ColumnName;
            this.LandInflueneDataGridView.PrimaryKeyColumnName = this.landInfluenceData.ListLandInfluenceTable.InfluenceTypeIDColumn.ColumnName;
        }


        #endregion

        private void LandInflueneDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (this.flagcelledit)
                //{
                //    //this.LandInflueneDataGridView[e.ColumnIndex, e.RowIndex].Selected = true;
                //    this.LandInflueneDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

                //    this.LandInflueneDataGridView.Focus();
                //    this.LandInflueneDataGridView.CurrentCell = this.LandInflueneDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //    this.LandInflueneDataGridView.CurrentCell.Selected = true; 
                //     this.flagcelledit = false; 
                //}
                //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
                if (this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex].RowState != DataRowState.Deleted)
                {
                int j = this.LandInflueneDataGridView.OriginalRowCount;
                for (int i = 0; i < this.LandInflueneDataGridView.OriginalRowCount; i++)
                {
                   
                        if (string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[i]["InfluenceTypeID"].ToString()))
                        {
                            this.landInfluenceData.ListLandInfluenceTable.Rows[i]["IsDeleted"] = false;
                            this.landInfluenceData.ListLandInfluenceTable.Rows[i]["IsModified"] = false;
                        }
                 }
                

                if (this.isChange)
                {
                    decimal outTaxDecimal = 0;
                    if (!String.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString().Trim()) && (this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.Equals(1)))
                    {
                        string val = this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {

                            if (outTaxDecimal.ToString().Contains("-"))
                            {
                                if (outTaxDecimal < -100)
                                {
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                    //e.Value = "0.00 ";
                                    //e.ParsingApplied = true;

                                }

                            }
                            else
                            {

                                if (outTaxDecimal > 100 || outTaxDecimal == 0)
                                {
                                    //e.Value = "0.00"; 
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   


                                }


                            }

                        }
                        else
                        {
                            this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                        }
                    }
                    else if (!String.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString().Trim()) && this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.Equals(2))
                    {
                        string val = this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {

                            if (outTaxDecimal.ToString().Contains("-"))
                            {
                                if (outTaxDecimal <= -1000000)
                                {
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                    //e.Value = "0.00 ";
                                    //e.ParsingApplied = true;

                                }

                            }
                            else
                            {

                                if (outTaxDecimal > 1000000 || outTaxDecimal == 1000000)
                                {

                                    //e.Value = "0.00"; 
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   


                                }


                            }

                        }
                        else
                        {
                            this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                        }
                        //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
                      
                    }
                    //else if(string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString()))  
                    //{
                    //    string val = this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                    //    if (Decimal.TryParse(val, out outTaxDecimal))
                    //    {
                    //        if (outTaxDecimal == 0)
                    //        {

                    //            this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                    //            this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                    //        }
                    //    }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }



        private void LandInflueneDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                this.rowIn = e.RowIndex;
                this.columnNo = e.ColumnIndex;
                if (e.ColumnIndex == 2 && (this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Type"].Equals(1)))
                {
                    decimal outTaxDecimal = 0;
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value)) //removd bcoz value shows empty does not affect.  //&& !String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {

                            if (outTaxDecimal.ToString().Contains("-"))
                            {
                                if (outTaxDecimal < -100)
                                {
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                                    this.isChange = true;
                                   // this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                    //e.Value = "0.00 ";
                                    //e.ParsingApplied = true;

                                }

                            }
                            else
                            {

                                if (outTaxDecimal > 100 || outTaxDecimal == 0)
                                {
                                    //e.Value = "0.00"; 
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
                                    this.isChange = true;
                                    e.ParsingApplied = true;
                                }


                            }

                        }
                        else
                        {
                            this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                            //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
                            this.isChange = true;
                        }



                    }
                    //  this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   

                }
                else if (e.ColumnIndex == 2 && (this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Type"].Equals(2)))
                {
                    decimal outTaxDecimal = 0;
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value)) //removd bcoz value shows empty does not affect.  //&& !String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {
                            if (outTaxDecimal > 1000000 || outTaxDecimal == 1000000 || outTaxDecimal <= -1000000)
                            {
                                this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                                //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                this.isChange = true;
                                e.ParsingApplied = true;
                            }



                        }
                        else
                        {
                            this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                            //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
                            this.isChange = true;
                        }

                    }
                }
                //else if (e.ColumnIndex == 2)
                //{
                //    decimal outTaxDecimal = 0;
                //    string val = e.Value.ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                //    if (Decimal.TryParse(val,out outTaxDecimal))
                //    {
                //        if (outTaxDecimal == 0)
                //        {
                //            this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
                //            this.isChange = true;
                //            e.ParsingApplied = true;
                //        }
                //    }
                         
                //}
                
            }

            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LandInflueneDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try{


                if (e.KeyValue == 46 && this.LandInflueneDataGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                       && this.LandInflueneDataGridView.OriginalRowCount > 0 && this.LandInflueneDataGridView.SelectedRows.Count > 0)
                {
                    this.EnableSaveCancelButton();
                    int i = this.LandInflueneDataGridView.CurrentRowIndex;
                    this.isDelId = this.LandInflueneDataGridView.CurrentRowIndex - 1;
                    string InfluenceID = this.LandInflueneDataGridView.Rows[i].Cells["InfluenceTypeID"].Value.ToString();
                    if (!string.IsNullOrEmpty(InfluenceID))
                    {

                        this.landInfluenceData.ListLandInfluenceTable.Rows[i]["IsDeleted"] = true;
                        this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                        DataRow[] DelRow = this.landInfluenceData.ListLandInfluenceTable.Select("IsDeleted=True");
                        if (DelRow.Length >= 1)
                        {
                            if (this.LandInflueneDataGridView.Rows.Count <= 11)
                            {
                                F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                                newRow["EmptyRecord$"] = "True";
                                this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                                this.LandInfluenceGridVerticalScroll.Visible = false;
                            }
                            this.tempData.ImportRow(DelRow[0]);
                            this.landInfluenceData.ListLandInfluenceTable.Rows.Remove(DelRow[0]);
                            if (this.LandInflueneDataGridView.Rows.Count.Equals(11))
                            {
                                this.LandInfluenceGridVerticalScroll.Visible = true;
                            }
                        }




                        //this.LandInflueneDataGridView.Rows.Remove(this.LandInflueneDataGridView.CurrentRow);

                        // DataRow[] DelRow = this.landInfluenceData.ListLandInfluenceTable.Select("InfluenceTypeID=" + InfluenceID);
                        /*
                        int index = this.landInfluenceData.ListLandInfluenceTable.Rows.IndexOf(DelRow[0]);
                        this.landInfluenceData.ListLandInfluenceTable.Rows[index]["IsDeleted"] = true;
                        //this.LandInflueneDataGridView.Rows[this.LandInflueneDataGridView.CurrentRowIndex].Cells["IsDeleted"].Value = true;     
                            F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                            newRow["EmptyRecord$"] = "True";
                            this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                            //DataView tempview;
                            //tempview = this.landInfluenceData.ListLandInfluenceTable.DefaultView;
                            //tempview.RowFilter = "IsDeleted=false OR EmptyRecord$=true";
                            //if (tempview.Count < 11)
                            //{
                            //    F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                            //    newRow["EmptyRecord$"] = "True";
                            //    this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                            //    this.LandInfluenceGridVerticalScroll.Visible = false;
                            //}
                            this.landInfluenceData.AcceptChanges();
                            this.landInfluenceData.ListLandInfluenceTable.DefaultView.RowFilter = ("IsDeleted=false OR EmptyRecord$=true");
                            //DataView dv = this.landInfluenceData.ListLandInfluenceTable.DefaultView;
                            //this.LandInflueneDataGridView.DataSource = dv;

                             //BindingSource LandInfluenceDataView = new BindingSource();
                             //LandInfluenceDataView.DataSource = this.landInfluenceData.ListLandInfluenceTable.DefaultView;
                             //LandInfluenceDataView.Filter = "IsDeleted=false OR EmptyRecord$=true";
                             this.LandInflueneDataGridView.DataSource = this.landInfluenceData.ListLandInfluenceTable.DefaultView;  */
                        ////   this.LandInflueneDataGridView.AllowUserToAddRows = true;


                    }
                    else
                    {

                        //this.landInfluenceData.ListLandInfluenceTable.Rows[i]["IsDeleted"] = true;
                        //this.landInfluenceData.ListLandInfluenceTable.Rows[i].Delete();
                        this.rowDel = true;
                        this.cellform = true;
                        //this.landInfluenceData.ListLandInfluenceTable.Rows[i].Delete();  
                        //F36062LandInfluenceData.ListLandInfluenceTableRow newRow1 = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                        //newRow1["EmptyRecord$"] = "True";
                        //this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow1);
                        this.LandInflueneDataGridView.NumRowsVisible = 11;
                        int delrow = this.LandInflueneDataGridView.CurrentRow.Index;
                        this.LandInflueneDataGridView.Rows.Remove(this.LandInflueneDataGridView.CurrentRow);
                        if ((this.landInfluenceData.ListLandInfluenceTable.Rows.Count < 11))
                        {
                            F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                            newRow["EmptyRecord$"] = "True";
                            this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                            this.LandInfluenceGridVerticalScroll.Visible = false;
                            if (this.LandInflueneDataGridView.Rows.Count.Equals(11))
                            {
                                this.LandInfluenceGridVerticalScroll.Visible = true;
                            }
                        }
                        else
                        {
                            if (this.LandInflueneDataGridView.Rows.Count < 11)
                            {
                                F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                                newRow["EmptyRecord$"] = "True";
                                this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                                this.LandInfluenceGridVerticalScroll.Visible = false;
                            }
                            if (this.LandInflueneDataGridView.Rows.Count.Equals(11))
                            {
                                this.LandInfluenceGridVerticalScroll.Visible = true;
                            }
                            if (this.LandInflueneDataGridView.Rows.Count != this.landInfluenceData.ListLandInfluenceTable.Rows.Count)
                            {
                                this.landInfluenceData.ListLandInfluenceTable.Rows.RemoveAt(delrow);
                            }

                        }

                        //this.LandInflueneDataGridView.Rows[0].Selected = true;

                        this.landInfluenceData.AcceptChanges();
                    }
                    if (i > 0)
                    {
                        TerraScanCommon.SetDataGridViewPosition(this.LandInflueneDataGridView, i - 1);
                    }
                    else
                    {
                        TerraScanCommon.SetDataGridViewPosition(this.LandInflueneDataGridView, 0);
                    }
                    this.flagondelete = true;
                    this.rowDel = false;
                }
            }
                catch(Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                }
            
        }

       




        /// <summary>
        /// Handles the Validating event of the DistributionGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void LandInfluenceGrid_Validating(object sender, EventArgs e)
        {
            try
            {

              this.LandInflueneDataGridView.EditingControl.TextChanged -= new EventHandler(this.LandInflueneDataGrid_TextChanged);
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the KeyDown event of the DistributionGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandInflueneDataGridView_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int j = this.landInfluenceData.ListLandInfluenceTable.Rows.Count;
                if ((j - 1).Equals(this.rowNo) && ((j - 1).Equals(this.LandInflueneDataGridView.OriginalRowCount)) && (this.columnNo ==1)&& (this.rowIn.Equals(this.rowNo)) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                    newRow["EmptyRecord$"] = "True";
                    this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                    this.LandInfluenceGridVerticalScroll.Visible = false;
                    //this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowNo]["Type"].Equals(1);     
                    this.EditEnabled();
                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["IsModified"]=true;   
                }
                //this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["IsModified"].Equals(true);
                if (((ComboBox)sender).Text == "Factor")
                {
                    this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Type"].Value = ((ComboBox)sender).SelectedValue;
                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Type"] = (this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Type"].Value.ToString());
                    if (!string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].ToString()))
                    {

                        decimal outTaxDecimal = 0;
                        string val = this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].ToString();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {

                            if (outTaxDecimal.ToString().Contains("-"))
                            {
                                if (outTaxDecimal < -100)
                                {
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = "0.00";
                                    this.isChange = true;
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                    //e.Value = "0.00 ";
                                    //e.ParsingApplied = true;

                                }

                            }
                            else
                            {

                                if (outTaxDecimal > 100 || outTaxDecimal == 0)
                                {
                                    //e.Value = "0.00"; 
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
                                    this.isChange = true;

                                }


                            }

                        }
                       

                    }
                    this.isChange = true;
                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = (this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Influence"].Value.ToString());
                    //this.LandInflueneDataGridView.CommitEdit(0); 
                     this.landInfluenceData.ListLandInfluenceTable.AcceptChanges(); 
                }
                else
                {
                    this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Type"].Value = 2;
                    this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Type"].Value = ((ComboBox)sender).SelectedValue;
                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Type"] = this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Type"].Value.ToString();
                    //this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Type"].ToString().Equals(2);   
                    this.isChange = true;
                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = (this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Influence"].Value.ToString());
                    //this.LandInflueneDataGridView.CommitEdit(this.rowIn);
                    this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                }
                if (this.isChange)
                {
                    decimal outTaxDecimal = 0;
                    if (!String.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].ToString().Trim()) && (this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Type"].Value.Equals(1)))
                    {
                        string val = this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {

                            if (outTaxDecimal.ToString().Contains("-"))
                            {
                                if (outTaxDecimal < -100)
                                {
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                    //e.Value = "0.00 ";
                                    //e.ParsingApplied = true;

                                }

                            }
                            else
                            {

                                if (outTaxDecimal > 100 || outTaxDecimal == 0)
                                {
                                    //e.Value = "0.00"; 
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   

                                 }


                            }

                        }
                        this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                        this.isChange = false;

                    }
                    else if (!String.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].ToString().Trim()) && this.LandInflueneDataGridView.Rows[this.rowIn].Cells["Type"].Value.Equals(2))
                    {
                        string val = this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                        if (Decimal.TryParse(val, out outTaxDecimal))
                        {

                            if (outTaxDecimal.ToString().Contains("-"))
                            {
                                if (outTaxDecimal <= -1000000)
                                {
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                    //e.Value = "0.00 ";
                                    //e.ParsingApplied = true;

                                }

                            }
                            else
                            {

                                if (outTaxDecimal > 1000000 || outTaxDecimal == 1000000)
                                {
                                    //e.Value = "0.00"; 
                                    this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"] = "0.00";
                                    //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   


                                }


                            }

                        }
                        //this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowIn]["Influence"].Equals(this.LandInflueneDataGridView.Rows[57].Cells["Influence"].Value.ToString());        
                        this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                        this.isChange = false;
                    }
                   
                }
                //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();  
                this.isSelectChange = true; 
                this.EnableSaveCancelButton();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the TextChanged event of the DistributionGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandInflueneDataGrid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as TextBox).Text == " ")
                {
                    (sender as TextBox).Text = string.Empty;
                }
                this.EditEnabled();

                // Add New Row 
                //int rowNo = this.LandInflueneDataGridView.OriginalRowCount; 

                if (!this.isRowAdded)
                {
                    this.EditEnabled();

                    int j = this.landInfluenceData.ListLandInfluenceTable.Rows.Count;
                    this.rowNo = this.rowIn;
                    if ((this.rowIn >= 10) && (this.columnNo == 0 || this.columnNo == 1 || this.columnNo == 2 || this.columnNo == 3) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        F36062LandInfluenceData.ListLandInfluenceTableRow newRow = this.landInfluenceData.ListLandInfluenceTable.NewListLandInfluenceTableRow();
                        newRow["EmptyRecord$"] = "True";
                        this.landInfluenceData.ListLandInfluenceTable.AddListLandInfluenceTableRow(newRow);
                        this.LandInfluenceGridVerticalScroll.Visible = false;
                        //this.landInfluenceData.ListLandInfluenceTable.Rows[this.rowNo]["Type"].Equals(1);     
                        this.EditEnabled();
                        this.isRowAdded = true;
                    }
                   
                }   
                    //ohthis.landInfluenceData.ListLandInfluenceTable.Rows[this.rowNo]["IsModified"].Equals(true);

                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LandInflueneDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.LandInflueneDataGridView_SelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.LandInflueneDataGridView_SelectionChangeCommitted);
                    ////((ComboBox)e.Control).KeyDown += new KeyEventHandler(this.LandInfluenceGrid_KeyDown);
                }

                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged -= new EventHandler(this.LandInflueneDataGrid_TextChanged);
                    e.Control.TextChanged += new EventHandler(this.LandInflueneDataGrid_TextChanged);
                    e.Control.Validated  -= new EventHandler(this.LandInfluenceGrid_Validating);
                    e.Control.Validated += new EventHandler(this.LandInfluenceGrid_Validating);
                    e.Control.KeyDown -= new KeyEventHandler(this.LandInflueneDataGridView_KeyDown);

                }
                if (this.LandInflueneDataGridView.CurrentCell.ColumnIndex == 2)
                {

                    if (e.Control is TextBox)
                    {
                        TextBox tb = e.Control as TextBox;
                        tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                        tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                    }

                }
                if (this.LandInflueneDataGridView.CurrentCell.ColumnIndex != 2)
                {
                    if (e.Control is TextBox)
                    {
                        TextBox tb = e.Control as TextBox;
                        tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

      private   void tb_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsDigit(e.KeyChar)) &&e.KeyChar !='-')
            {

                Keys key = (Keys)e.KeyChar;

              

                if (!(key == Keys.Back || key == Keys.Delete ))
                {

                    e.Handled = true;

                }

            }

        }

      private void LandInflueneDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
      {

          if (!this.msg && !this.cellform)
          {
              if (this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value!=null)
              {
                  if (e.ColumnIndex == 2 && (this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.Equals(1)))
                  {
                      decimal outTaxDecimal = 0;
                      if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value.ToString().Trim()))
                      {
                          string val = e.Value.ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim();
                          //string val = e.Value.ToString().Replace("%", "").Trim();
                          if (Decimal.TryParse(val, out outTaxDecimal))
                          {

                              if (outTaxDecimal.ToString().Contains("-"))
                              {
                                  if (outTaxDecimal < -100)
                                  {

                                      outTaxDecimal = 0;

                                      e.Value = "0.00%";

                                  }
                                  else
                                  {
                                      decimal a = outTaxDecimal / 100;
                                      e.Value = String.Concat("(", Decimal.Negate(a).ToString("0.00%"), ")");
                                      e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                                  }
                              }
                              else
                              {
                                  if (outTaxDecimal > 100 || outTaxDecimal == 0)
                                  {
                                      // this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex][e.ColumnIndex].ToString().Equals(0);
                                      e.Value = "0.00%";
                                  }
                                  else
                                  {
                                      decimal a = outTaxDecimal / 100;
                                      e.Value = a.ToString("0.00%");
                                      e.FormattingApplied = true;
                                  }

                                  if (outTaxDecimal == 100)
                                  {
                                      e.Value = "100.00%";
                                  }

                                  e.FormattingApplied = true;
                              }

                          }
                          else
                          {
                              //this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex][e.ColumnIndex].Equals(0);
                              e.Value = "0.00%";
                          }
                      }
                  }
              }
                  
                  if (e.ColumnIndex == 2 &&(e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value.ToString().Trim())))
                  {
                      decimal outTaxDecimal;
                      if ((this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.Equals(2)))
                      {
                          if (e.Value.ToString().Contains("-"))
                          {
                              e.Value.ToString().Replace("%", "").Replace("(", "-").Replace(")", "").Trim().ToString();
                          }

                      string val = e.Value.ToString();

                      if (Decimal.TryParse(val, out outTaxDecimal))
                      {
                          if (outTaxDecimal == 1000000 || outTaxDecimal > 1000000||  outTaxDecimal <=-1000000)
                          {
                              outTaxDecimal = 0;
                              //this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].Equals(0);
                              e.Value = "0.00";
                          }
                          else if (outTaxDecimal > 0)
                          {

                              e.Value = outTaxDecimal.ToString("#,##,##0.00");

                          }
                          else if (outTaxDecimal.ToString().Contains("-"))
                          {

                              e.Value = String.Concat("(", Decimal.Negate(outTaxDecimal).ToString("#,##,##0.00"), ")");
                              e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                          }
                      }
                  }
              }
                  

          }
          this.cellform = false;
          this.msg = false;
      }
       
        private void LandInflueneDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.columnNo = e.ColumnIndex;
            this.rowIn = e.RowIndex; 
              int j = this.landInfluenceData.ListLandInfluenceTable.Rows.Count;
  

              if (string.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString()))
              {


                  /////to avoid new row added set Factor if IsRowadded false.
                  if (!this.formLoad && this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex].RowState != DataRowState.Deleted)
                  {
                      if (!this.rowDel)
                      {
                          if (this.columnNo == 0 && e.RowIndex == j - 1 && this.LandInflueneDataGridView.OriginalRowCount <= this.LandInflueneDataGridView.NumRowsVisible)
                          {
                              this.landInfluenceData.ListLandInfluenceTable.Rows[j - 1]["Type"] = 1;
                              this.isRowAdded = false;
                          }
                          else if (this.columnNo == 0 && String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim()))
                          {
                              this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Type"] = 1;
                              this.isRowAdded = false;
                          }
                          else if ((this.columnNo == 1 && String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim())))
                          {
                              this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Type"] = 1;
                              this.isRowAdded = false;
                          }
                          else if ((this.columnNo == 2 && String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim())))
                          {
                              this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Type"] = 1;
                              this.isRowAdded = false;
                          }
                          else if ((this.columnNo == 3 && String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim())))
                          {
                              this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Type"] = 1;
                              this.isRowAdded = false;
                          }
                      }

                     

                  }
              }
                  //}
              //}
            
        }

        private void LandInflueneDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 2 && !String.IsNullOrEmpty(this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value.ToString().Trim()))
            {

                this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value = this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value.ToString();

                //this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex][e.ColumnIndex].ToString();

                //this.landInfluenceData.ListLandInfluenceTable.Rows[e.ColumnIndex][e.RowIndex-1].ToString().Equals(a);       
                DataGridViewCell currentCell = (DataGridViewCell)this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"];
                //string x = this.LandInflueneDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                currentCell.Value = this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value;
                //currentCell -= new DataGridViewCellFormattingEventHandler(currentCell, DataGridViewCellFormattingEventArgs()); 
                this.msg = true;
                return;
            }

        }

        private void LandInflueneDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            ///used for performing selection change commited change in the Influence value

            //if (e.ColumnIndex == 1 && this.isSelectChange)
            //{
            //    if (!string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString()))
            //    {
            //        if (this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Type"].Value.Equals(1))
            //        {
            //            decimal outTaxDecimal = 0;
            //            string val = this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString();
            //            if (Decimal.TryParse(val, out outTaxDecimal))
            //            {

            //                if (outTaxDecimal.ToString().Contains("-"))
            //                {
            //                    if (outTaxDecimal < -100)
            //                    {
            //                        this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
            //                        this.isChange = true;
            //                        //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
            //                        //e.Value = "0.00 ";
            //                        //e.ParsingApplied = true;

            //                    }

            //                }
            //                else
            //                {

            //                    if (outTaxDecimal > 100 || outTaxDecimal == 0)
            //                    {
            //                        //e.Value = "0.00"; 
            //                        this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
            //                        //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
            //                        this.isChange = true;

            //                    }


            //                }

            //            }
            //            else
            //            {
            //                this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
            //                //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();   
            //                this.isChange = true;
            //            }

            //        }
            //    }
            //   // this.isSelectChange = false;

            //}
            //if (e.ColumnIndex == 2)
            //{
            //    if (!string.IsNullOrEmpty(this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].ToString()))
            //    {
            //        decimal outTaxDecimal = 0;
            //        string val = this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value.ToString();
            //        if (Decimal.TryParse(val, out outTaxDecimal))
            //        {
            //            if (outTaxDecimal == 0)
            //            {
            //                this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
            //                this.LandInflueneDataGridView.Rows[e.RowIndex].Cells["Influence"].Value = "0.00";
            //                //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
            //                this.isChange = true;
            //            }
            //            else
            //            {

            //            }
            //        }
            //        else
            //        {
            //            this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"] = "0.00";
            //            this.isChange = true;
            //        }
            //    }
            //}

            //if (this.columnNo == 2 || this.columnNo == 3 || this.columnNo == 1 || this.columnNo == 0)
            //{
            //    decimal outTaxDecimal = 0;
            //    if (this.LandInflueneDataGridView.Rows[this.rowvalid].Cells["Type"].Value.Equals(1))
            //    {
            //        string val = this.LandInflueneDataGridView.Rows[this.rowvalid].Cells["influence"].Value.ToString();
            //        if (Decimal.TryParse(val, out outTaxDecimal))
            //        {


            //            if (outTaxDecimal <= -100 || outTaxDecimal > 100)
            //            {
            //                this.LandInflueneDataGridView.CurrentCell.Value = 0.00;


            //            }


            //        }
            //    }
            //}      
        }

        private void LandInflueneDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 2)
            //{
              //  this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
            //}
        }


        /// <summary>
        /// Handles the Click event of the DeprecationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InfluenceTablePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the MouseHover event of the DeprecationPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InfluenceTablePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.F36062InfluenceFormToolTip.SetToolTip(this.InfluenceTablePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }


        }
    }
